using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using DevExpress.XtraBars;
//using AppPengguna.DM.GA;
using DevExpress.XtraGrid.Views.Grid;
using System.Threading;
using DevExpress.XtraGrid.Columns;


namespace AppPengguna.AST.TRK
{
    public partial class UcPenelusuranForm : UserControl
    {

        private SvcPenelusuranAsetSelect.call_pttClient trakCaller;
        private SvcPenelusuranAsetSelect.InputParameters inputPar;
        private SvcPenelusuranAsetSelect.OutputParameters OutPar;
        public  SvcPenelusuranAsetSelect.BPSIMANSROW_ASET_TRACKING_RESULT rowData;

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        public SetPanel setPanel;

        private GridView selectedView = null;
        private bool LastRow = false;
        private int posisiRow = 0;
        private bool initialData = true;
        private GridColumn kolom;

        private ArrayList dataGrid;
        private bool dataInisial;
        private decimal dataAwal;
        private decimal dataAkhir;
        private decimal currentMaks;
        private decimal currentMin;
        private bool loadMore = true;
        private FrmKoorSatker FrmKoorSatker;

        private BindingSource binder;
        Thread myThread;

        public UcPenelusuranForm()
        {
            InitializeComponent();
        }

        public FrmKoorSatker docked;

        public UcPenelusuranForm(FrmKoorSatker FrmKoorSatker)
        {
            this.FrmKoorSatker = FrmKoorSatker;
            InitializeComponent();
            
            
        }

        #region THREAD
        public delegate void SetEnabledForm(bool enabled);
       
        public void setEnabledForm(bool enabled)
        {
            this.groupControl.Enabled = enabled;
        }
     
        public void setThread(bool start)
        {
            try
            {
                if (start == true)
                {
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new SetEnabledForm(this.setEnabledForm), false);
                        }
                        else
                        {
                            this.setEnabledForm(false);
                        }
                        this.FrmKoorSatker.fToggleProgressBar("start");
                    }
                }
                else
                {
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new SetEnabledForm(this.setEnabledForm), true);
                        }
                    }
                    else
                    {
                        this.setEnabledForm(true);
                    }
                    this.FrmKoorSatker.fToggleProgressBar("finisih");
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Ambil Data Telusur
        public SvcPenelusuranAsetSelect.InputParameters parseParam()
        {

            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }

            inputPar = new SvcPenelusuranAsetSelect.InputParameters();
            inputPar.P_MAX = currentMaks;
            inputPar.P_MAXSpecified = true;
            inputPar.P_MIN = currentMin;
            inputPar.P_MINSpecified = true;

            inputPar.P_ALAMAT = teAlamat.Text.Replace(' ','%');
            inputPar.P_ID_SATKER = idSatker;
            inputPar.P_ID_SATKERSpecified = true;

            inputPar.P_KD_KAB = kdKabKota;
            inputPar.P_KD_PROV = kdProvinsi;
            inputPar.P_KONDISI = idKondisi;
            inputPar.P_STATUS_HUKUM = kdHukum;
            inputPar.P_NAMA_BARANG = teNmBrg.Text.Replace(' ', '%');
            inputPar.P_KD_JNS_BMN = kdJnsBmn;
            inputPar.P_KD_JNS_BMNSpecified = true;
            inputPar.P_NILAI_ASET = preRange+rangRph;

            return inputPar;
        }

        public void getInitLnyData(string strWhere = "")
        {
            this.setThread(true);

            trakCaller = new SvcPenelusuranAsetSelect.call_pttClient();
            trakCaller.Beginexecute(parseParam(), new AsyncCallback(this.getResult), null);
        }

        private void getResult(IAsyncResult result)
        {
            try
            {
                this.OutPar = trakCaller.Endexecute(result);
                this.Invoke(new ShowData(this.showData), this.OutPar);
                this.setThread(false);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcPenelusuranAsetSelect.OutputParameters dataOut);


        GridRow row;
        private void showData(SvcPenelusuranAsetSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_ASET_TRACKING_RESULT.Count();

            if (dataInisial == true)
            {
                dataGrid = new ArrayList();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {

                dataGrid.Add(serviceOutPut.SF_ROW_ASET_TRACKING_RESULT[i]);
            }

            if (dataInisial == true)
            {
                gcTelusur.DataSource = null;
                gcTelusur.DataSource = dataGrid;
            }
            else
            {
                gcTelusur.RefreshDataSource();
            }
            dataInisial = false;

        }

        public void initGrid()
        {
            this.dataInisial = true;
            this.dataAwal = 1;
            this.dataAkhir = 100;
            this.currentMaks = 100;
            this.currentMin = 1;
            this.loadMore = true;

        }
        
        #endregion

        #region GET SET
        private frmKl frmKl = null;
        private frmSatker frmSatker = null;
        private frmJnsBmn frmJnsBmn = null;
        private frmProvinsi frmProvinsi = null;
        private frmKabKota frmKabKota = null;

        private decimal? idKl { get; set; }
        private string kdKl { get; set; }
        private string urKl { get { return this.teKLMain.Text; } set { this.teKLMain.Text = value; } }

        private decimal? idKlOpt { get; set; }
        private string kdKlOpt { get; set; }
        private string urKlOpt { get { return this.teKLOp.Text; } set { this.teKLOp.Text = value; } }

        private decimal? idSatker { get; set; }
        private string kdSatker { get; set; }
        private string urSatker { get { return this.teSatker.Text; } set { this.teSatker.Text = value; } }

        private decimal? kdJnsBmn { 
            get {
                if (leJnsBmn.EditValue != null)
                    return Convert.ToDecimal(leJnsBmn.EditValue);
                else
                    return null;
            } 
            set 
            { 
                leJnsBmn.EditValue = value; } 
        }
       

        private string kdProvinsi{ get; set; }
        private string urProvinsi { get { return this.teProv.Text; } set { this.teProv.Text = value; } }

        private string kdKabKota { get; set; }
        private string urKabKota { get { return this.teKota.Text; } set { this.teKota.Text = value; } }

        private string idKondisi
        {
            get
            {
                if (leKondisi.EditValue != null)
                    return leKondisi.EditValue.ToString();
                else
                    return "";
                
            }
            set
            {
                leKondisi.EditValue = value;
            }
        }

        private string kdHukum
        {
            get
            {
                if (leStatusHukum.EditValue != null)
                    return leStatusHukum.EditValue.ToString();
                else
                    return "";
            }
            set
            {
                leStatusHukum.EditValue = value;
            }
        }
        #endregion

        #region Button PopUp


        private void sbKL_Click(object sender, EventArgs e)
        {
            if (this.frmKl == null)
            {
                this.frmKl = new frmKl();
            }
            frmKl.ambilKl = new AmbilKl(ambilKl);
            frmKl.ShowDialog();

        }
        private void sbKlPgg_Click(object sender, EventArgs e)
        {
            if (this.frmKl == null)
            {
                this.frmKl = new frmKl();
            }
            frmKl.ambilKl = new AmbilKl(ambilKlOpt);
            frmKl.ShowDialog();
        }
        private void sbSatker_Click(object sender, EventArgs e)
        {

            string strWhere = "ID_KL = " + this.idKl + " ";
            
            if (this.frmSatker == null)
            {
                this.frmSatker = new frmSatker();
            }

            frmSatker.ambilSatker = new AmbilSatker(ambilSatker);
            if (frmSatker.strWhere != strWhere)
            {
                if (this.urKl != "")
                {
                    frmSatker.strWhere = strWhere;
                }
                frmSatker.refreshData();
            }
            frmSatker.ShowDialog();

            
        }
       
        private void sbProv_Click(object sender, EventArgs e)
        {
            if (this.frmProvinsi == null)
            {
                this.frmProvinsi = new frmProvinsi();
            }
            frmProvinsi.ambilProvinsi = new AmbilProvinsi(ambilProvinsi);
            frmProvinsi.ShowDialog();
        }
        private void sbKota_Click(object sender, EventArgs e)
        {
            if (this.frmKabKota == null)
            {
                this.frmKabKota = new frmKabKota();
            }
            frmKabKota.ambilKabKota = new AmbilKabKota(ambilKabKota);
            frmKabKota.ShowDialog();
        }

        private void ambilKl(decimal? id, string kd, string ur)
        {
            this.idKl = id;
            this.kdKl = kd;
            this.urKl = ur;
        }
        private void ambilKlOpt(decimal? id, string kd, string ur)
        {
            this.idKlOpt = id;
            this.kdKlOpt = kd;
            this.urKlOpt = ur;
        }
        private void ambilSatker(decimal? id, string kd, string ur)
        {
            this.idSatker = id;
            this.kdSatker = kd;
            this.urSatker = ur;
        }
        
        private void ambilProvinsi(string kd, string ur)
        {
            kdProvinsi = kd;
            urProvinsi = ur;
        }
        private void ambilKabKota(string kd, string ur)
        {
            kdKabKota = kd;
            urKabKota = ur;
        }

        

        
        

  



        #endregion

        #region CheckBox and Radio Button

        private void ceLtkGeo_CheckedChanged(object sender, EventArgs e)
        {
            if (ceLtkGeo.Checked == true)
            {
                teProv.Enabled = true;
                teProv.Properties.ReadOnly = true;
                teKota.Enabled = true;
                teKota.Properties.ReadOnly = true;
                sbProv.Enabled = true;
                sbKota.Enabled = true;
            }
            else
            {
                teProv.Enabled = false;
                teProv.Properties.ReadOnly = false;
                teKota.Enabled = false;
                teKota.Properties.ReadOnly = false;
                sbProv.Enabled = false;
                sbKota.Enabled = false;
                kdKabKota = "";
                
                teProv.Text = "";
                teKota.Text = "";
                

            }
        }

        private void ceKondisi_CheckedChanged(object sender, EventArgs e)
        {
            if (ceKondisi.Checked == true)
            {
                leKondisi.Properties.ReadOnly = false; 
            }
            else
            {
                leKondisi.Properties.ReadOnly = true;
                leKondisi.EditValue = null;
            }
        }

        private void cePengguna_CheckedChanged(object sender, EventArgs e)
        {
            if (cePengguna.Checked == true)
            {
                teKLOp.Enabled = true;
                teKLOp.Properties.ReadOnly = true;

                sbKlPgg.Enabled = true;
            }
            else
            {
                teKLOp.Enabled = false;
                teKLOp.Properties.ReadOnly = false;
                teKLOp.Text = "";
                idKlOpt = null;
                sbKlPgg.Enabled = false;
            }
        }

        private void ceNilai_CheckedChanged(object sender, EventArgs e)
        {
            if (ceNilai.Checked)
            {
                ceNilPer.Enabled = true;
                ceSusut.Enabled = true;
                ceBuku.Enabled = true;

            }
            else
            {
                ceNilPer.Enabled = false;
                ceSusut.Enabled = false;
                ceBuku.Enabled = false;

                ceNilPer.Checked = false;
                ceSusut.Checked = false;
                ceBuku.Checked = false;

            }
        }

        private void activeRadio(bool active)
        {
            if (active)
            {
                rb1T.Enabled = true;
                rb100M.Enabled = true;
                rb500M.Enabled = true;
                rb50M.Enabled = true;
                rbU25M.Enabled = true;
                rb25M.Enabled = true;

            }
            else
            {
                rb1T.Enabled = false;
                rb100M.Enabled = false;
                rb500M.Enabled = false;
                rb50M.Enabled = false;
                rbU25M.Enabled = false;
                rb25M.Enabled = false;

                rb1T.Checked = false;
                rb100M.Checked = false;
                rb500M.Checked = false;
                rb50M.Checked = false;
                rbU25M.Checked = false;
                rb25M.Checked = false;
            }
        }

        private void ceHukum_CheckedChanged(object sender, EventArgs e)
        {
            if (ceHukum.Checked == true)
            {
                leStatusHukum.Properties.ReadOnly = false;
            }
            else
            {
                leStatusHukum.Properties.ReadOnly = true;
                leStatusHukum.EditValue = null;
            }
        }


        private string rangRph;
        private void rb1T_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1T.Checked)
                rangRph = "A";
        }

        private void rb500M_CheckedChanged(object sender, EventArgs e)
        {
            if (rb500M.Checked)
                rangRph = "B";
        }

        private void rb100M_CheckedChanged(object sender, EventArgs e)
        {
            if (rb100M.Checked)
                rangRph = "C";
        }

        private void rb50M_CheckedChanged(object sender, EventArgs e)
        {
            if (rb50M.Checked)
                rangRph = "D";
        }

        private void rb25M_CheckedChanged(object sender, EventArgs e)
        {
            if (rb25M.Checked)
                rangRph = "E";

        }

        private void rbU25M_CheckedChanged(object sender, EventArgs e)
        {
            if (rbU25M.Checked)
                rangRph = "F";
        }

        String preRange = "";
        private void ceNilPer_CheckedChanged(object sender, EventArgs e)
        {
            if (ceNilPer.Checked)
            {
                ceSusut.Checked = false;
                ceBuku.Checked = false;

                activeRadio(true);
                preRange = "1";
            }
            else
            {
                activeRadio(false);
            }
        }

        private void ceSusut_CheckedChanged(object sender, EventArgs e)
        {
            if (ceSusut.Checked)
            {
                ceNilPer.Checked = false;
                ceBuku.Checked = false;
                activeRadio(true);
                preRange = "2";
            }
            else
            {
                activeRadio(false);
            }
        }

        private void ceBuku_CheckedChanged(object sender, EventArgs e)
        {
            if (ceBuku.Checked)
            {
                ceNilPer.Checked = false;
                ceSusut.Checked = false;
                activeRadio(true);
                preRange = "3";

            }
            else
            {
                activeRadio(false);
            }
        }

        #endregion

        private void UcPenelusuranForm_Load(object sender, EventArgs e)
        {
            if (leJnsBmn.Properties.DataSource == null)
            {
                getJenisBMN();
            }
            if (leKondisi.Properties.DataSource == null)
            {
                getKondisi();
            }
            if (leStatusHukum.Properties.DataSource == null)
            {
                getStatusHukum();
            }
            //gmAsetTlsr.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            //gmAsetTlsr.Position = new GMap.NET.PointLatLng(-6.9147, 107.6098);

            //GMapOverlay markersOverlay = new GMapOverlay("account");
            //GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-6.9147, 107.6098),
            //  GMarkerGoogleType.blue_pushpin);
            //marker.ToolTip = new GMapRoundedToolTip(marker);
            //marker.ToolTipText = "Hai";
           
            

            //markersOverlay.Markers.Add(marker);
            //gmAsetTlsr.Overlays.Add(markersOverlay);

           
        }

        private void setKoordinat(double Lat, double longt)
        {
            gmAsetTlsr.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmAsetTlsr.Position = new GMap.NET.PointLatLng(Lat, longt);

            GMapOverlay markersOverlay = new GMapOverlay("marker");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(Lat, longt),
              GMarkerGoogleType.arrow);
            marker.ToolTip = new GMapToolTip(marker);
            string  deskripsi = rowData.DESKRIPSI;

            //deskripsi.Replace(" ","\r\n");
            //int deskLength=deskripsi.Length;
            //for (int i = 0; i > deskLength; i++)
            //{
                
            //    if(deskripsi[i]=' ' &&  == 0)
            //}

            marker.ToolTipText = deskripsi + "\r\n" + rowData.PENGGUNA + "\r\n" + rowData.LOKASI;
            markersOverlay.Markers.Add(marker);
            gmAsetTlsr.Overlays.Add(markersOverlay);
        }

       

        private void gvTelusur_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                rowData = (SvcPenelusuranAsetSelect.BPSIMANSROW_ASET_TRACKING_RESULT)selectedView.GetRow(e.FocusedRowHandle);
            }
        }

        private int setKolom(string caption, string fieldName, string Name, Int32 urutan, bool setDdItem = true, int _kolSize = 200)
        {
            this.kolom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvTelusur.Columns.Add(this.kolom);

            this.kolom.Caption = caption;
            this.kolom.FieldName = fieldName;
            this.kolom.Name = Name;
            this.kolom.Visible = true;
            this.kolom.VisibleIndex = urutan;
            this.kolom.MinWidth = _kolSize;



            return _kolSize;
        }

        public void showMap()
        {
            if (rowData != null)
            {
                string kordinat = rowData.GPS_POINT;
                if (kordinat != "")
                {
                    string[] arrayPoint = rowData.GPS_POINT.Trim().Split(',');


                    Console.WriteLine(Convert.ToDouble(arrayPoint[0].Replace('.', ',')));


                    setKoordinat(Convert.ToDouble(arrayPoint[0].Replace('.', ',')), Convert.ToDouble(arrayPoint[1].Replace('.', ',')));//(Convert.ToDouble(arrayPoint[0]),Convert.ToDouble(arrayPoint[1]));
                }

                

            }
            else
            {
                MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulGagal);
            }
        }

        private void gvTelusur_DoubleClick(object sender, EventArgs e)
        {
            showMap();
        }

        public void showDetail()
        {

            string where = "ID_ASET = 717'";// +Convert.ToString(id_aset) + "'";
            switch(4)
            {
                case 1:
                    TN.ucTanah tanah = new TN.ucTanah(true);
                    tanah.nonAktifForm = nonAktifForm;
                    tanah.aktifkanForm = aktifkanForm;
                    tanah.ShowProgresBar = ShowProgresBar;
                    tanah.CloseProgresBar = CloseProgresBar;
                    tanah.setPanel = setPanel;
               
                    setPanel(tanah);
                    tanah.dataInisial=true;
                  
                    tanah.getinitTanah(where);

                    break;
                case 2:
                    BG.ucBangunan bangunan = new BG.ucBangunan(true);
                    bangunan.nonAktifForm = nonAktifForm;
                    bangunan.aktifkanForm = aktifkanForm;
                    bangunan.ShowProgresBar = ShowProgresBar;
                    bangunan.CloseProgresBar = CloseProgresBar;
                    bangunan.setPanel = setPanel;
                   // frmTarget.panelKoorSatker.Controls.Clear();
                    setPanel(bangunan);
                    bangunan.initGrid();
                    where = "tu1." + where;
                    bangunan.getinitBangunan(where);

                    break;
                case 3:
                    RN.UcRmhMster rmh = new RN.UcRmhMster(true);
                    //frmTarget.panelKoorSatker.Controls.Clear();
                    rmh.nonAktifForm = nonAktifForm;
                    rmh.aktifkanForm = aktifkanForm;
                    rmh.ShowProgresBar = ShowProgresBar;
                    rmh.CloseProgresBar = CloseProgresBar;
                   // frmTarget.panelKoorSatker.Controls.Add(rmh);
                    rmh.setPanel = setPanel;
                    rmh.initGrid();
                    rmh.getinitRumahNegara(where);
                    setPanel(rmh);
                    break;
                case 4:
                    AK.ucAngkutan angk = new AK.ucAngkutan(true);
                    angk.nonAktifForm = nonAktifForm;
                    angk.aktifkanForm = aktifkanForm;
                    angk.ShowProgresBar = ShowProgresBar;
                    angk.CloseProgresBar = CloseProgresBar;
                    angk.setPanel = setPanel;
                    //frmTarget.panelKoorSatker.Controls.Clear
                    setPanel(angk);
                    angk.initGrid();
                    angk.getinitAngkutan(where);
                    break;
                case 5:
                    MPNT.ucMesinPNT nonTik = new MPNT.ucMesinPNT(true);
                    nonTik.nonAktifForm = nonAktifForm;
                    nonTik.aktifkanForm = aktifkanForm;
                    nonTik.ShowProgresBar = ShowProgresBar;
                    nonTik.CloseProgresBar = CloseProgresBar;
                    nonTik.setPanel = setPanel;
                    setPanel(nonTik);
                    nonTik.initGrid();
                    nonTik.getinitMPNT(where);
                    break;
                case 6:
                    MPKT.ucMPKT khususTik = new MPKT.ucMPKT(true);
                    khususTik.nonAktifForm = nonAktifForm;
                    khususTik.aktifkanForm = aktifkanForm;
                    khususTik.ShowProgresBar = ShowProgresBar;
                    khususTik.CloseProgresBar = CloseProgresBar;
                    khususTik.setPanel = setPanel;
                    setPanel(khususTik);
                    khususTik.initGrid();
                    khususTik.getinitMPKT(where);
                    break;
                case 7:
                    SJT.ucSenjata senjata = new SJT.ucSenjata(true);
                    senjata.nonAktifForm = nonAktifForm;
                    senjata.aktifkanForm = aktifkanForm;
                    senjata.ShowProgresBar = ShowProgresBar;
                    senjata.CloseProgresBar = CloseProgresBar;
                    senjata.setPanel = setPanel;
                    setPanel(senjata);
                    senjata.initGrid();
                    senjata.getinitSenjata(where);
                    break;
                case 8:
                    JJ.ucJalanJembatan jalanJembatan = new JJ.ucJalanJembatan(true);
                    jalanJembatan.nonAktifForm = nonAktifForm;
                    jalanJembatan.aktifkanForm = aktifkanForm;
                    jalanJembatan.ShowProgresBar = ShowProgresBar;
                    jalanJembatan.CloseProgresBar = CloseProgresBar;
                    jalanJembatan.setPanel = setPanel;
                    jalanJembatan.initGrid();
                    setPanel(jalanJembatan);
                    jalanJembatan.getinitJalanJembatan(where);
                    //setPanel(jalanJembatan);
                    
                    break;
                case 9:
                    BA.UcBgnAir Bair = new BA.UcBgnAir(true);
                    Bair.nonAktifForm = nonAktifForm;
                    Bair.aktifkanForm = aktifkanForm;
                    Bair.ShowProgresBar = ShowProgresBar;
                    Bair.CloseProgresBar = CloseProgresBar;
                    Bair.setPanel = setPanel;
                    
                    setPanel(Bair);
                    Bair.initGrid();
                    Bair.getinitBangunanAir(where);
                    break;
                case 10:
                    PK.ucPropKhusus proKhus = new PK.ucPropKhusus(true);
                    proKhus.nonAktifForm = nonAktifForm;
                    proKhus.aktifkanForm = aktifkanForm;
                    proKhus.ShowProgresBar = ShowProgresBar;
                    proKhus.CloseProgresBar = CloseProgresBar;
                    proKhus.setPanel = setPanel;

                    setPanel(proKhus);
                    proKhus.initGrid();
                    proKhus.getinitPropKhusus(where);
                    break;
                case 11:
                    LNY.UcAstTtpLny lny = new LNY.UcAstTtpLny(true);
                    lny.nonAktifForm = nonAktifForm;
                    lny.aktifkanForm = aktifkanForm;
                    lny.ShowProgresBar = ShowProgresBar;
                    lny.CloseProgresBar = CloseProgresBar;
                    lny.setPanel = setPanel;

                    setPanel(lny);
                    lny.initGrid();
                    lny.getinitLainnya(where);
                    break;
                case 12:
                    KDP.UcKDBGrid kpd = new KDP.UcKDBGrid(true);
                    kpd.nonAktifForm = nonAktifForm;
                    kpd.aktifkanForm = aktifkanForm;
                    kpd.ShowProgresBar = ShowProgresBar;
                    kpd.CloseProgresBar = CloseProgresBar;
                    kpd.setPanel = setPanel;

                    setPanel(kpd);
                    kpd.initGrid();
                    kpd.getinitKDP(where);
                    break;
                case 13:
                    ATB.UcATBGrid atb = new ATB.UcATBGrid(true);
                    atb.nonAktifForm = nonAktifForm;
                    atb.aktifkanForm = aktifkanForm;
                    atb.ShowProgresBar = ShowProgresBar;
                    atb.CloseProgresBar = CloseProgresBar;
                    atb.setPanel = setPanel;

                    setPanel(atb);
                    atb.initGrid();
                    atb.getinitATB(where);
                    break;
                case 14:
                    RV.UcRenovMstr rnv = new RV.UcRenovMstr(true);
                    rnv.nonAktifForm = nonAktifForm;
                    rnv.aktifkanForm = aktifkanForm;
                    rnv.ShowProgresBar = ShowProgresBar;
                    rnv.CloseProgresBar = CloseProgresBar;
                    rnv.setPanel = setPanel;
                    setPanel(rnv);
                    rnv.initGrid();
                    rnv.getinitRenovasi(where);
                    break;
                case 15:
                    BS.ucSjrh sjrh = new BS.ucSjrh(true);
                    sjrh.nonAktifForm = nonAktifForm;
                    sjrh.aktifkanForm = aktifkanForm;
                    sjrh.ShowProgresBar = ShowProgresBar;
                    sjrh.CloseProgresBar = CloseProgresBar;
                    sjrh.setPanel = setPanel;
                    setPanel(sjrh);
                    sjrh.getinitSejarah(where);
                    break;
                case 16:
                    PRSDN.ucPersediaan prsd = new PRSDN.ucPersediaan(true);
                    prsd.nonAktifForm = nonAktifForm;
                    prsd.aktifkanForm = aktifkanForm;
                    prsd.ShowProgresBar = ShowProgresBar;
                    prsd.CloseProgresBar = CloseProgresBar;
                    prsd.setPanel = setPanel;
                    setPanel(prsd);
                    prsd.initGrid();
                    prsd.getinitPersediaan(where);
                    break;
                

                default:
                    break;
            }
        }

        PU.FrmDetailAset detailAset = new PU.FrmDetailAset();

        public void setPanelDetailAset(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            detailAset.panelDetailAset.Controls.Clear();
            detailAset.panelDetailAset.Controls.Add(uc);
        }

        public void klikDetail()
        {
            string where = "ID_ASET = " + rowData.ID_ASET.ToString() + "";

            switch (rowData.KD_JNS_BMN.ToString())
            {
                case "1": //tanah

                    AST.TN.ucTanah tanah = new AST.TN.ucTanah(true);


                    tanah.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    tanah.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    tanah.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    tanah.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    tanah.initGrid();
                    tanah.BySatker = "1=1";
                    tanah.setPanel = this.setPanelDetailAset; //masih masalah
                    this.setPanelDetailAset(tanah);
                    tanah.dataInisial = true;
                    tanah.initGrid();

                    tanah.getinitTanah(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "2": //bangunan
                    AST.BG.ucBangunan ucBangunan = new AST.BG.ucBangunan(true);
                    ucBangunan.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    ucBangunan.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    ucBangunan.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    ucBangunan.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    ucBangunan.initGrid();
                    ucBangunan.BySatker = "1=1";
                    ucBangunan.setPanel = this.setPanelDetailAset; //masih masalah
                    this.setPanelDetailAset(ucBangunan);

                    ucBangunan.dataInisial = true;
                    ucBangunan.getinitBangunan(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "3"://rumah
                    AST.RN.UcRmhMster rumah = new AST.RN.UcRmhMster(true);


                    rumah.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    rumah.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    rumah.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    rumah.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    rumah.initGrid();
                    rumah.BySatker = "1=1";
                    rumah.setPanel = this.setPanelDetailAset; //masih masalah
                    this.setPanelDetailAset(rumah);
                    rumah.dataInisial = true;
                    rumah.initGrid();

                    rumah.getinitRumahNegara(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "4": //angkutan
                    AST.AK.ucAngkutan angkutan = new AST.AK.ucAngkutan(true);


                    angkutan.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    angkutan.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    angkutan.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    angkutan.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    angkutan.initGrid();
                    angkutan.BySatker = "1=1";
                    angkutan.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(angkutan);
                    angkutan.dataInisial = true;
                    angkutan.initGrid();

                    angkutan.getinitAngkutan(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "5"://mesin non tik
                    AST.MPNT.ucMesinPNT MPNT = new AST.MPNT.ucMesinPNT(true);


                    MPNT.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    MPNT.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    MPNT.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    MPNT.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    MPNT.initGrid();
                    MPNT.BySatker = "1=1";
                    MPNT.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(MPNT);
                    MPNT.dataInisial = true;
                    MPNT.initGrid();

                    MPNT.getinitMPNT(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "6"://mesin tik
                    AST.MPKT.ucMPKT MPKT = new AST.MPKT.ucMPKT(true);


                    MPKT.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    MPKT.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    MPKT.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    MPKT.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    MPKT.initGrid();
                    MPKT.BySatker = "1=1";
                    MPKT.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(MPKT);
                    MPKT.dataInisial = true;
                    MPKT.initGrid();

                    MPKT.getinitMPKT(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "7"://senjata
                    AST.SJT.ucSenjata senjata = new AST.SJT.ucSenjata(true);


                    senjata.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    senjata.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    senjata.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    senjata.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    senjata.initGrid();
                    senjata.BySatker = "1=1";
                    senjata.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(senjata);
                    senjata.dataInisial = true;
                    senjata.initGrid();

                    senjata.getinitSenjata(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "8"://jalan jembatan
                    AST.JJ.ucJalanJembatan jalanJembatan = new AST.JJ.ucJalanJembatan(true);


                    jalanJembatan.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    jalanJembatan.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    jalanJembatan.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    jalanJembatan.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    jalanJembatan.initGrid();
                    jalanJembatan.BySatker = "1=1";
                    jalanJembatan.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(jalanJembatan);
                    jalanJembatan.dataInisial = true;
                    jalanJembatan.initGrid();

                    jalanJembatan.getinitJalanJembatan(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "9"://bangunan air
                    AST.BA.UcBgnAir bangunanAir = new AST.BA.UcBgnAir(true);


                    bangunanAir.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    bangunanAir.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    bangunanAir.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    bangunanAir.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    bangunanAir.initGrid();
                    bangunanAir.BySatker = "1=1";
                    bangunanAir.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(bangunanAir);
                    bangunanAir.dataInisial = true;
                    bangunanAir.initGrid();

                    bangunanAir.getinitBangunanAir(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "10"://properti khusus
                    AST.PK.ucPropKhusus propertiKhusus = new AST.PK.ucPropKhusus(true);


                    propertiKhusus.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    propertiKhusus.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    propertiKhusus.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    propertiKhusus.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    propertiKhusus.initGrid();
                    propertiKhusus.BySatker = "1=1";
                    propertiKhusus.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(propertiKhusus);
                    propertiKhusus.dataInisial = true;
                    propertiKhusus.initGrid();

                    propertiKhusus.getinitPropKhusus(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "11"://aset tetap lainnya
                    AST.LNY.UcAstTtpLny asetTetapLainnya = new AST.LNY.UcAstTtpLny(true);


                    asetTetapLainnya.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    asetTetapLainnya.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    asetTetapLainnya.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    asetTetapLainnya.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    asetTetapLainnya.initGrid();
                    asetTetapLainnya.BySatker = "1=1";
                    asetTetapLainnya.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(asetTetapLainnya);
                    asetTetapLainnya.dataInisial = true;
                    asetTetapLainnya.initGrid();

                    asetTetapLainnya.getinitLainnya(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "12"://konstruksi dalam pembangunan
                    AST.KDP.UcKDBGrid KDP = new AST.KDP.UcKDBGrid(true);


                    KDP.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    KDP.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    KDP.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    KDP.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    KDP.initGrid();
                    KDP.BySatker = "1=1";
                    KDP.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(KDP);
                    KDP.dataInisial = true;
                    KDP.initGrid();

                    KDP.getinitKDP(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "13"://aset tak berwujud
                    AST.ATB.UcATBGrid ATB = new AST.ATB.UcATBGrid(true);


                    ATB.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    ATB.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    ATB.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    ATB.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    ATB.initGrid();
                    ATB.BySatker = "1=1";
                    ATB.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(ATB);
                    ATB.dataInisial = true;
                    ATB.initGrid();

                    ATB.getinitATB(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "14"://renovasi
                    AST.RV.UcRenovMstr renovasi = new AST.RV.UcRenovMstr(true);


                    renovasi.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    renovasi.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    renovasi.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    renovasi.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    renovasi.initGrid();
                    renovasi.BySatker = "1=1";
                    renovasi.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(renovasi);
                    renovasi.dataInisial = true;
                    renovasi.initGrid();

                    renovasi.getinitRenovasi(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "15"://bangunan bersejarah
                    AST.BS.ucSjrh sejarah = new AST.BS.ucSjrh(true);


                    sejarah.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    sejarah.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    sejarah.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    sejarah.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    sejarah.initGrid();
                    sejarah.BySatker = "1=1";
                    sejarah.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(sejarah);
                    sejarah.dataInisial = true;
                    sejarah.initGrid();

                    sejarah.getinitSejarah(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
                case "16"://persediaan
                    AST.PRSDN.ucPersediaan persediaan = new AST.PRSDN.ucPersediaan(true);


                    persediaan.nonAktifForm = new NonAktifkanFormSatker(this.detailAset.nonAktifkanFormDetail);
                    persediaan.aktifkanForm = new AktifkanFormSatker(this.detailAset.aktifkanFormDetail);
                    persediaan.ShowProgresBar = new showProgresBar(this.detailAset.ShowProgresBar);
                    persediaan.CloseProgresBar = new closeProgresBar(this.detailAset.progBar);
                    persediaan.initGrid();
                    persediaan.BySatker = "1=1";
                    persediaan.setPanel = this.setPanelDetailAset;
                    this.setPanelDetailAset(persediaan);
                    persediaan.dataInisial = true;
                    persediaan.initGrid();

                    persediaan.getinitPersediaan(where);
                    detailAset.Enabled = true;
                    detailAset.ShowDialog();
                    break;
            }
        }

        private void sbTlsr_Click(object sender, EventArgs e)
        {
            if (preRange == "2")
            {
                RPH_NILAI_PENYUSUTAN.Visible = true;
                RPH_ASET.Visible = false;
                RPH_NILAI_BUKU.Visible = false;
            }
            else if (preRange == "3")
            {
                RPH_NILAI_PENYUSUTAN.Visible = false;
                RPH_ASET.Visible = false;
                RPH_NILAI_BUKU.Visible = true;

            }
            else
            {
                RPH_NILAI_PENYUSUTAN.Visible = false;
                RPH_ASET.Visible = true;
                RPH_NILAI_BUKU.Visible = false;
            }

            initGrid();
            getInitLnyData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            leJnsBmn.EditValue = null;
            leKondisi.EditValue = null;
            leStatusHukum.EditValue = null;
            teKLMain.Text = "";
            idKl = null;
            teSatker.Text = "";
            idSatker = null;
            kdJnsBmn = null;
            teNmBrg.Text = "";
            teAlamat.Text = "";
        }



        #region GET DATA JENIS BMN
        private SvcJnsBmnSelect.call_pttClient svcCallerJnsBmn;
        private SvcJnsBmnSelect.InputParameters inputParJnsBmn;
        private SvcJnsBmnSelect.OutputParameters outParJnsBmn;
        private SvcJnsBmnSelect.BPSIMANSROW_R_JNS_BMN rowDataJnsBmn;
        
        public void getJenisBMN()
        {
            try
            {
                this.setThread(true);
                inputParJnsBmn = new SvcJnsBmnSelect.InputParameters();

                
                inputParJnsBmn.P_MIN = konfigApp.dataAwal;
                inputParJnsBmn.P_MINSpecified = true;
                inputParJnsBmn.P_MAX = konfigApp.maksReferensi;
                inputParJnsBmn.P_MAXSpecified = true;
                inputParJnsBmn.P_SORT = "ASC";
                inputParJnsBmn.STR_WHERE = "";
                svcCallerJnsBmn = new SvcJnsBmnSelect.call_pttClient();
                svcCallerJnsBmn.Beginexecute(inputParJnsBmn, new AsyncCallback(this.getDataJnsBmn), null);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataJnsBmn(IAsyncResult result)
        {
            try
            {
                this.outParJnsBmn = svcCallerJnsBmn.Endexecute(result);
                this.Invoke(new ShowDataJnsBmn(this.showDataJnsBmn), this.outParJnsBmn);
                this.setThread(false);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataJnsBmn(SvcJnsBmnSelect.OutputParameters dataOut);
        public void showDataJnsBmn(SvcJnsBmnSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_R_JNS_BMN.Count();
            leJnsBmn.Properties.DataSource = serviceOutPut.SF_ROW_R_JNS_BMN; 
            
        }
        
        #endregion

        #region GET DATA KONDISI
        private SvcKondisiSelect.call_pttClient svcCallerKondisi;
        private SvcKondisiSelect.InputParameters inputParKondisi;
        private SvcKondisiSelect.OutputParameters outParKondisi;
        private SvcKondisiSelect.BPSIMANSROW_R_KONDISI rowDataKondisi;

        public void getKondisi()
        {
            try
            {
                this.setThread(true);
                inputParKondisi = new SvcKondisiSelect.InputParameters();

                inputParKondisi.P_MIN = konfigApp.dataAwal;
                inputParKondisi.P_MINSpecified = true;
                inputParKondisi.P_MAX = konfigApp.maksReferensi;
                inputParKondisi.P_MAXSpecified = true;
                inputParKondisi.P_SORT = "ASC";
                inputParKondisi.STR_WHERE = "";
                svcCallerKondisi = new SvcKondisiSelect.call_pttClient();
                svcCallerKondisi.Beginexecute(inputParKondisi, new AsyncCallback(this.getDataKondisi), null);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataKondisi(IAsyncResult result)
        {
            try
            {
                this.outParKondisi = svcCallerKondisi.Endexecute(result);
                this.Invoke(new ShowDataKondisi(this.showDataKondisi), this.outParKondisi);
                this.setThread(false);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataKondisi(SvcKondisiSelect.OutputParameters dataOut);
        public void showDataKondisi(SvcKondisiSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_R_KONDISI.Count();
            leKondisi.Properties.DataSource = serviceOutPut.SF_ROW_R_KONDISI;

        }
        #endregion

        #region GET DATA STATUS HUKUM
        private SvcStatusHukumSelect.call_pttClient svcCallerStatusHukum;
        private SvcStatusHukumSelect.InputParameters inputParStatusHukum;
        private SvcStatusHukumSelect.OutputParameters outParStatusHukum;
        private SvcStatusHukumSelect.BPSIMANSROW_STATUS_HUKUM rowDataStatusHukum;

        public void getStatusHukum()
        {
            try
            {
                this.setThread(true);
                inputParStatusHukum = new SvcStatusHukumSelect.InputParameters();

                inputParStatusHukum.P_MIN = konfigApp.dataAwal;
                inputParStatusHukum.P_MINSpecified = true;
                inputParStatusHukum.P_MAX = konfigApp.maksReferensi;
                inputParStatusHukum.P_MAXSpecified = true;
                inputParStatusHukum.P_SORT = "ASC";
                inputParStatusHukum.STR_WHERE = "";
                svcCallerStatusHukum = new SvcStatusHukumSelect.call_pttClient();
                svcCallerStatusHukum.Beginexecute(inputParStatusHukum, new AsyncCallback(this.getDataStatusHukum), null);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataStatusHukum(IAsyncResult result)
        {
            try
            {
                this.outParStatusHukum = svcCallerStatusHukum.Endexecute(result);
                this.Invoke(new ShowDataStatusHukum(this.showDataStatusHukum), this.outParStatusHukum);
                this.setThread(false);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataStatusHukum(SvcStatusHukumSelect.OutputParameters dataOut);
        public void showDataStatusHukum(SvcStatusHukumSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_R_STATUS_HUKUM.Count();
            leStatusHukum.Properties.DataSource = serviceOutPut.SF_ROW_R_STATUS_HUKUM;

        }
        #endregion

    }
}
