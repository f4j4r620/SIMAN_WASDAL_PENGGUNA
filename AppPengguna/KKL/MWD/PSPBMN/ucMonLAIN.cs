using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKL.MWD.PSPBMN
{
//    public delegate void CariDataOnline(string strCariData);

    public partial class ucMonLAIN : DevExpress.XtraEditors.XtraUserControl
    {

        public CariDataOnline cariDataOnline;
        public string fieldDicari = "";
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private bool initModeLoad = true;
        private string cariSebelumnya = "";
        private string strCari = "";
        private ArrayList daftarFieldJenis = null;
        private ArrayList daftarFieldKolom = null;
        public FrmKoorKL FrmKoorKL;
        public string namaModul = null;


        #region Inisialisasi tab aset
        public bool tabPemanfaataan1Open = false;
        public bool tabPenggunaan1Open = false;
        public bool tabPemanfaatan1Open = false; 
        public bool tabTanah1Open = false;
        public bool tabBangunan1Open = false;
        public bool tabRmhNgr1Open = false;
        public bool tabAngkutan1Open = false;
        public bool tabBgnAir1Open = false;
        public bool tabSenjata1Open = false;
        public bool tabMsnNTik1Open = false;
        public bool tabKdp1Open = false;
        public bool tabJlnJmbtn1Open = false;
        public bool tabPropSus1Open = false;
        public bool tabAtl1Open = false;
        public bool tabMsnTik1Open = false;
        public bool tabAtb1Open = false;
        public bool tabRenov1Open = false;
        public bool tabPersediaan1Open = false;
        public bool tabSejrh1Open = false;

        public bool tabTanah2Open = false;
        public bool tabBangunan2Open = false;
        public bool tabRmhNgr2Open = false;
        public bool tabAngkutan2Open = false;
        public bool tabBgnAir2Open = false;
        public bool tabSenjata2Open = false;
        public bool tabMsnNTik2Open = false;
        public bool tabKdp2Open = false;
        public bool tabJlnJmbtn2Open = false;
        public bool tabPropSus2Open = false;
        public bool tabAtl2Open = false;
        public bool tabMsnTik2Open = false;
        public bool tabAtb2Open = false;
        public bool tabRenov2Open = false;
        public bool tabPersediaan2Open = false;
        public bool tabSejrh2Open = false;
        public string strKdPelayanan = "";


        public DETAIL.ucTanah tanah1 = null;
        public DETAIL.ucBangunan bangunan1 = null;
        public DETAIL.ucRmhNgr rmhNgr1 = null;
        public DETAIL.ucAngkutan angkutan1 = null;
        public DETAIL.ucBangunanAir bangunanAir1 = null;
        public DETAIL.ucSenjata senjata1 = null;
        public DETAIL.ucMesinNonTik mesinNonTik1 = null;
        public DETAIL.ucKDP KDP1 = null;
        public DETAIL.ucJlnJmbtn jlnJmbtn1 = null;
        public DETAIL.ucPropSus propSus1 = null;
        public DETAIL.ucATL ATL1 = null;
        public DETAIL.ucMesinTik mesinTik1 = null;
        public DETAIL.ucATB ATB1 = null;
        public DETAIL.ucRenovasi renovasi1 = null;
       // public DETAIL2.ucPersediaan persediaan1 = null;
        public DETAIL.ucSejarah sejarah1 = null;

        #endregion


        public ucMonLAIN(FrmKoorKL _frmKpknl)
        {
            InitializeComponent();
            this.FrmKoorKL = _frmKpknl;
            this.initSearch();
            
        }

        #region SET TAB
        public void tabSelect()
        {
            initSearch();
            #region DETAIL PSP SUDAH JADI
            if (xtbDetail1.SelectedTabPageIndex == 0)
            {
                xtpPenggunaan.Text = this.namaModul;
                if (!tabPenggunaan1Open)
                {
                    //if (namaModul.Equals("Penggunaan BMN"))
                    //{
                    //    penggunaan1 = new DETAIL.ucPenggunaan(FrmKoorKL, this);
                    //    penggunaan1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    //    penggunaan1.Dock = DockStyle.Fill;
                    //    panelPenggunaan.Controls.Clear();
                    //    panelPenggunaan.Controls.Add(penggunaan1);
                    //    penggunaan1.getTindakLanjut();
                    //    tabPenggunaan1Open = true;
                    //}
                    //else if (namaModul.Equals("Pemanfaatan BMN"))
                    //{
                    //    pemanfaatan1 = new DETAIL.ucPemanfaatan(FrmKoorKL, this);
                    //    pemanfaatan1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    //    pemanfaatan1.Dock = DockStyle.Fill;
                    //    panelPenggunaan.Controls.Clear();
                    //    panelPenggunaan.Controls.Add(pemanfaatan1);
                    //    pemanfaatan1.getTindakLanjut();
                    //    tabPemanfaataan1Open = true;
                    //}
                    //else if (namaModul.Equals("Pemindatanganan BMN"))
                    //{
                    //    pemanfaatan1 = new DETAIL.ucPemanfaatan(FrmKoorKL, this);
                    //    pemanfaatan1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    //    pemanfaatan1.Dock = DockStyle.Fill;
                    //    panelPenggunaan.Controls.Clear();
                    //    panelPenggunaan.Controls.Add(pemanfaatan1);
                    //    pemanfaatan1.getTindakLanjut();
                    //    tabPemanfaataan1Open = true;
                    //}
                    
                }
            }
            if (xtbDetail1.SelectedTabPageIndex == 1)
            {
                if (!tabTanah1Open)
                {
                    tanah1 = new DETAIL.ucTanah(FrmKoorKL, this);
                    tanah1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    tanah1.Dock = DockStyle.Fill;
                    panelTanah.Controls.Clear();
                    panelTanah.Controls.Add(tanah1);
                    tanah1.getTanah();
                    tabTanah1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 2)
            {
                if (!tabBangunan1Open)
                {
                    bangunan1 = new DETAIL.ucBangunan(FrmKoorKL, this);
                    bangunan1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    bangunan1.Dock = DockStyle.Fill;
                    panelBangunan.Controls.Clear();
                    panelBangunan.Controls.Add(bangunan1);
                    bangunan1.getBangunan();
                    tabBangunan1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 3)
            {
                if (!tabRmhNgr1Open)
                {
                    rmhNgr1 = new DETAIL.ucRmhNgr(FrmKoorKL, this);
                    rmhNgr1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    rmhNgr1.Dock = DockStyle.Fill;
                    panelRmhNgr.Controls.Clear();
                    panelRmhNgr.Controls.Add(rmhNgr1);
                    rmhNgr1.getRmhNgr();
                    tabRmhNgr1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 4)
            {
                if (!tabAngkutan1Open)
                {
                    angkutan1 = new DETAIL.ucAngkutan(FrmKoorKL, this);
                    angkutan1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    angkutan1.Dock = DockStyle.Fill;
                    panelAngkutan.Controls.Clear();
                    panelAngkutan.Controls.Add(angkutan1);
                    angkutan1.getAngkutan();
                    tabAngkutan1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 5)
            {
                if (!tabBgnAir1Open)
                {
                    bangunanAir1 = new DETAIL.ucBangunanAir(FrmKoorKL, this);
                    bangunanAir1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    bangunanAir1.Dock = DockStyle.Fill;
                    panelBangunanAir.Controls.Clear();
                    panelBangunanAir.Controls.Add(bangunanAir1);
                    bangunanAir1.getBangunanAir();
                    tabBgnAir1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 6)
            {
                if (!tabSenjata1Open)
                {
                    senjata1 = new DETAIL.ucSenjata(FrmKoorKL, this);
                    senjata1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    senjata1.Dock = DockStyle.Fill;
                    panelSenjata.Controls.Clear();
                    panelSenjata.Controls.Add(senjata1);
                    senjata1.getSenjata();
                    tabSenjata1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 7)
            {
                if (!tabMsnNTik1Open)
                {
                    mesinNonTik1 = new DETAIL.ucMesinNonTik(FrmKoorKL, this);
                    mesinNonTik1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    mesinNonTik1.Dock = DockStyle.Fill;
                    panelMesinNNonTik.Controls.Clear();
                    panelMesinNNonTik.Controls.Add(mesinNonTik1);
                    mesinNonTik1.getMesinNonTik();
                    tabMsnNTik1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 8)
            {
                if (!tabJlnJmbtn1Open)
                {
                    jlnJmbtn1 = new DETAIL.ucJlnJmbtn(FrmKoorKL, this);
                    jlnJmbtn1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    jlnJmbtn1.Dock = DockStyle.Fill;
                    panelJlnJmbtn.Controls.Clear();
                    panelJlnJmbtn.Controls.Add(jlnJmbtn1);
                    jlnJmbtn1.getJlnJmbtn();
                    tabJlnJmbtn1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 9)
            {
                if (!tabPropSus1Open)
                {
                    propSus1 = new DETAIL.ucPropSus(FrmKoorKL, this);
                    propSus1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    propSus1.Dock = DockStyle.Fill;
                    panelPropSus.Controls.Clear();
                    panelPropSus.Controls.Add(propSus1);
                    propSus1.getPropSus();
                    tabPropSus1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 10)
            {
                if (!tabAtl1Open)
                {
                    ATL1 = new DETAIL.ucATL(FrmKoorKL, this);
                    ATL1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    ATL1.Dock = DockStyle.Fill;
                    panelATL.Controls.Clear();
                    panelATL.Controls.Add(ATL1);
                    ATL1.getATL();
                    tabAtl1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 11)
            {
                if (!tabMsnTik1Open)
                {
                    mesinTik1 = new DETAIL.ucMesinTik(FrmKoorKL, this);
                    mesinTik1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    mesinTik1.Dock = DockStyle.Fill;
                    panelMesinTik.Controls.Clear();
                    panelMesinTik.Controls.Add(mesinTik1);
                    mesinTik1.getMesinTik();
                    tabMsnTik1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 12)
            {
                if (!tabAtb1Open)
                {
                    ATB1 = new DETAIL.ucATB(FrmKoorKL, this);
                    ATB1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    ATB1.Dock = DockStyle.Fill;
                    panelATB.Controls.Clear();
                    panelATB.Controls.Add(ATB1);
                    ATB1.getATB();
                    tabAtb1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 13)
            {
                if (!tabKdp1Open)
                {
                    KDP1 = new DETAIL.ucKDP(FrmKoorKL, this);
                    KDP1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    KDP1.Dock = DockStyle.Fill;
                    panelKDP.Controls.Clear();
                    panelKDP.Controls.Add(KDP1);
                    KDP1.getKDP();
                    tabKdp1Open = true;
                }
            }
            else if (xtbDetail1.SelectedTabPageIndex == 14)
            {
                if (!tabRenov1Open)
                {
                    renovasi1 = new DETAIL.ucRenovasi(FrmKoorKL, this);
                    renovasi1.cariDataOnline = new DETAIL.CariDataOnline(this.pencarianData);
                    renovasi1.Dock = DockStyle.Fill;
                    panelRenovasi.Controls.Clear();
                    panelRenovasi.Controls.Add(renovasi1);
                    renovasi1.getRenovasi();
                    tabRenov1Open = true;
                }
            }
            #endregion
            
        }
        #endregion

        #region PENCARIAN

        private DataTable dtTable = new DataTable();
        private DataColumn dtColumn;
        private DataRow dtRow;
        private ArrayList dtParams1;
        private ArrayList dtParams2;

        public void initSearch()
        {

            #region KOLOM PENCARIAN 1
            /*** KOLOM PENCARIAN 1 ***/
            dtTable = new DataTable();
            dtColumn = new DataColumn();
            dtParams1 = new ArrayList();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "DISPLAY";
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "VALUE";
            dtTable.Columns.Add(dtColumn);

            this.addColumn("KD_BRG", "KODE BARANG");
            this.dtParams1.Add("{1}%");
            //this.addColumn("NUP", "NUP");
            //this.dtParams1.Add("{1}%");
            this.addColumn("UR_SSKEL", "NAMA BARANG");
            this.dtParams1.Add("%{1}%");
            this.addColumn("KD_PEMOHON", "KODE PEMOHON");
            this.dtParams1.Add("{1}%");
            this.addColumn("NM_PEMOHON", "NAMA PEMOHON");
            this.dtParams1.Add("{1}%");
            this.addColumn("KUANTITAS", "KUANTITAS");
            this.dtParams1.Add("{1}%");
            this.addColumn("NILAI_PENETAPAN", "NILAI PERSETUJUAN");
            this.dtParams1.Add("{1}%");
            this.addColumn("KD_STATUS", "KODE STATUS");
            this.dtParams1.Add("{1}%");
            this.addColumn("NM_STATUS", "NAMA STATUS");
            this.dtParams1.Add("%{1}%");
            this.addColumn("GUNA_WASDAL", "PENGGUNAAN BMN");
            this.dtParams1.Add("%{1}%");
            this.addColumn("NO_SURAT", "NO SK");
            this.dtParams1.Add("{1}%");
            //this.addColumn("KD_PELAYANAN", "KODE PELAYANAN");
            //this.dtParams1.Add("{1}%");
            //this.addColumn("NM_PELAYANAN", "NAMA PELAYANAN");
            //this.dtParams1.Add("%{1}%");
            this.addColumn("THN_ANG", "TAHUN ANGGARAN");
            this.dtParams1.Add("{1}%");
            

            cbNamaKolom1.Properties.DataSource = dtTable;
            cbNamaKolom1.Properties.DisplayMember = "DISPLAY";
            cbNamaKolom1.Properties.ValueMember = "VALUE";
            #endregion

            #region KOLOM PENCARIAN 2
            /*** KOLOM PENCARIAN 1 ***/
            dtTable = new DataTable();
            dtColumn = new DataColumn();
            dtParams1 = new ArrayList();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "DISPLAY";
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "VALUE";
            dtTable.Columns.Add(dtColumn);

            this.addColumn("UR_SSKEL", "NAMA BARANG");
            this.dtParams2.Add("%{1}%");
            this.addColumn("KD_PEMOHON", "KODE PEMOHON");
            this.dtParams2.Add("{1}%");
            this.addColumn("NM_PEMOHON", "NAMA PEMOHON");
            this.dtParams2.Add("{1}%");
            this.addColumn("KUANTITAS", "KUANTITAS");
            this.dtParams2.Add("{1}%");
            this.addColumn("NILAI_PENETAANPAN", "NILAI PERSETUJUAN");
            this.dtParams2.Add("{1}%");
            this.addColumn("KD_STATUS", "KODE STATUS");
            this.dtParams2.Add("{1}%");
            this.addColumn("NM_STATUS", "NAMA STATUS");
            this.dtParams2.Add("%{1}%");
            this.addColumn("GUNA_WASDAL", "PENGGUNAAN BMN");
            this.dtParams2.Add("%{1}%");
            this.addColumn("NO_SURAT", "NO SK");
            this.dtParams2.Add("{1}%");
            //this.addColumn("KD_PELAYANAN", "KODE PELAYANAN");
            //this.dtParams2.Add("{1}%");
            //this.addColumn("NM_PELAYANAN", "NAMA PELAYANAN");
            //this.dtParams2.Add("%{1}%");
            this.addColumn("THN_ANG", "TAHUN ANGGARAN");
            this.dtParams2.Add("{1}%");

            cbNamaKolom2.Properties.DataSource = dtTable;
            cbNamaKolom2.Properties.DisplayMember = "DISPLAY";
            cbNamaKolom2.Properties.ValueMember = "VALUE";
            #endregion

            cbNamaKolom1.EditValue=null;
            cbNamaKolom2.EditValue = null;
            teCari1.Text = "";
            teCari2.Text = "";
            teNupDari.Text = "";
            teNupSampai.Text = "";
            teCari1.Focus();

        }

        public void addColumn(string value, string display)
        {
            dtRow = dtTable.NewRow();
            dtRow["DISPLAY"] = display;
            dtRow["VALUE"] = value;
            dtTable.Rows.Add(dtRow);
        }

        private void setVisibleKolom2(bool set)
        {
            if (set)
            {
                emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciNamaKolom2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciKataKunci2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else {
                emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNamaKolom2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciKataKunci2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void setVisibleKolomNup(bool set)
        {
            if (set)
            {
                this.emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupDari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupSampai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                this.emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupDari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupSampai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void cbNamaKolom_EditValueChanged(object sender, EventArgs e)
        {
            sbCari.Enabled = true;
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            this.fieldDicari = (cbNamaKolom1.Text.Trim() != "") ? cbNamaKolom1.EditValue.ToString() : "";

            if (fieldDicari != "")
            {
                switch (this.fieldDicari)
                {
                    case "KD_SATKER":
                        setVisibleKolomNup(false);
                        setVisibleKolom2(true);
                        break;
                    case "KD_BRG":
                    case "UR_SSKEL":
                        setVisibleKolom2(false);
                        setVisibleKolomNup(true);
                        break;
                    default:
                        setVisibleKolom2(false);
                        setVisibleKolomNup(false);
                        break;

                }
            }
            else {
                setVisibleKolom2(false);
                setVisibleKolomNup(false);
            }

            cbNamaKolom2.EditValue = null;
            teCari2.ResetText();
            teNupDari.ResetText();
            teNupSampai.ResetText();
        }

        private void cbNamaKolom2_EditValueChanged(object sender, EventArgs e)
        {
            sbCari.Enabled = true;
            this.modeLoadData = "ganti_kiword";
            this.fieldDicari = (cbNamaKolom2.Text.Trim() != "") ? cbNamaKolom2.EditValue.ToString() : "";
            if (fieldDicari != "")
            {
                switch (this.fieldDicari)
                {

                    case "KD_BRG":
                    case "UR_SSKEL":
                        setVisibleKolomNup(true);
                        break;
                    default:
                        setVisibleKolomNup(false);
                        setVisibleKolom2(true);
                        break;
                }
            }
            teNupDari.ResetText();
            teNupSampai.ResetText();
        }

        private string getFieldKolom(int indeksKolom)
        {
            if (indeksKolom > -1) return daftarFieldKolom[indeksKolom].ToString();
            else return "";
        }

        private void teCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region DETAIL PSP SUDAH JADI
                if (xtbDetail1.SelectedTabPageIndex == 0)
                {
                    //if (xtpPenggunaan.Text.Equals("Penggunaan BMN"))
                    //{
                    //    if (penggunaan1.initModeLoad == false) penggunaan1.modeLoadData = "ganti_kiword";
                    //}
                    //else if (xtpPenggunaan.Text.Equals("Pemanfaatan BMN"))
                    //{
                    //    if (pemanfaatan1.initModeLoad == false) pemanfaatan1.modeLoadData = "ganti_kiword";
                    //}
                    
                }
                if (xtbDetail1.SelectedTabPageIndex == 1)
                {
                    if (tanah1.initModeLoad == false) tanah1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 2)
                {
                    if (bangunan1.initModeLoad == false) bangunan1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 3)
                {
                    if (rmhNgr1.initModeLoad == false) rmhNgr1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 4)
                {
                    if (angkutan1.initModeLoad == false) angkutan1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 5)
                {
                    if (bangunanAir1.initModeLoad == false) bangunanAir1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 6)
                {
                    if (senjata1.initModeLoad == false) senjata1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 7)
                {
                    if (mesinNonTik1.initModeLoad == false) mesinNonTik1.modeLoadData = "ganti_kiword";
                }

                else if (xtbDetail1.SelectedTabPageIndex == 8)
                {
                    if (jlnJmbtn1.initModeLoad == false) jlnJmbtn1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 9)
                {
                    if (propSus1.initModeLoad == false) propSus1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 10)
                {
                    if (ATL1.initModeLoad == false) ATL1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 11)
                {
                    if (mesinTik1.initModeLoad == false) mesinTik1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 12)
                {
                    if (ATB1.initModeLoad == false) ATB1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 13)
                {
                    if (KDP1.initModeLoad == false) KDP1.modeLoadData = "ganti_kiword";
                }
                else if (xtbDetail1.SelectedTabPageIndex == 14)
                {
                    if (renovasi1.initModeLoad == false) renovasi1.modeLoadData = "ganti_kiword";
                }
                #endregion
           
            sbCari.Enabled = true;
        }

        private void setStrCari()
        {
            this.strCari = string.Empty;
            string namaKolom1 = string.Empty;
            string namaKolom2 = string.Empty;
            string kataKunci1 = string.Empty;
            string kataKunci2 = string.Empty;

            namaKolom1 = (cbNamaKolom1.Text.Trim() != "") ? cbNamaKolom1.EditValue.ToString() : "";
            namaKolom2 = (cbNamaKolom2.Text.Trim() != "") ? cbNamaKolom2.EditValue.ToString() : "";
            kataKunci1 = teCari1.Text.Trim().ToUpper();
            kataKunci2 = teCari2.Text.Trim().ToUpper();

            if (namaKolom1 != "" && kataKunci1 != "")
            {
                this.strCari = String.Format("AND (UPPER({0}) LIKE '" + dtParams1[cbNamaKolom1.ItemIndex].ToString() + "') ", namaKolom1, kataKunci1);
            }
            if (namaKolom1 != "" && kataKunci1 != "")
            {
                this.strCari += String.Format(" AND( UPPER({0}) LIKE '" + dtParams2[cbNamaKolom2.ItemIndex].ToString() + "') ", namaKolom2, kataKunci2);
            }
            if (teNupDari.Text.Trim() != "" && teNupSampai.Text.Trim() != "")
            {
                this.strCari += String.Format("  AND (UPPER({0}) BETWEEN {1} AND {2} )", "NUP", teNupDari.Text.Trim().ToUpper(), teNupSampai.Text.Trim().ToUpper());
            }

        }

        private void sbCariOnline_Click(object sender, EventArgs e)
        {

            if ((teCari1.Text.Trim() != ""))
            {
                if (xtbDetail1.SelectedTabPageIndex == 0)
                {
                    if (xtpPenggunaan.Text.Equals("Penggunaan BMN"))
                    {
                        //#region penggunaan
                        //if ((penggunaan1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        //{
                        //    penggunaan1.dataInisial = true;
                        //    penggunaan1.modeLoadData = "cari";
                        //    cariSebelumnya = teCari1.Text.Trim();
                        //    penggunaan1.initModeLoad = true;
                        //}
                        //else
                        //{
                        //    penggunaan1.dataInisial = false;
                        //    penggunaan1.initModeLoad = false;
                        //}
                        //this.setStrCari();
                        //penggunaan1.cariDataOnline(this.strCari);

                        //#endregion
                    }
                    if (xtpPenggunaan.Text.Equals("Pemanfaatan BMN"))
                    {
                        //#region pemanfaatan
                        //if ((pemanfaatan1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        //{
                        //    pemanfaatan1.dataInisial = true;
                        //    pemanfaatan1.modeLoadData = "cari";
                        //    cariSebelumnya = teCari1.Text.Trim();
                        //    pemanfaatan1.initModeLoad = true;
                        //}
                        //else
                        //{
                        //    pemanfaatan1.dataInisial = false;
                        //    pemanfaatan1.initModeLoad = false;
                        //}
                        //this.setStrCari();
                        //pemanfaatan1.cariDataOnline(this.strCari);

                        //#endregion
                    }
                }
                if (xtbDetail1.SelectedTabPageIndex == 1)
                {
                    #region tanah
                    if ((tanah1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        tanah1.dataInisial = true;
                        tanah1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        tanah1.initModeLoad = true;
                    }
                    else
                    {
                        tanah1.dataInisial = false;
                        tanah1.initModeLoad = false;
                    }
                    this.setStrCari();
                    tanah1.cariDataOnline(this.strCari);

                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 2)
                {
                    #region bangunan
                    if ((bangunan1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        bangunan1.dataInisial = true;
                        bangunan1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        bangunan1.initModeLoad = true;
                    }
                    else
                    {
                        bangunan1.dataInisial = false;
                        bangunan1.initModeLoad = false;
                    }
                    setStrCari();
                    bangunan1.cariDataOnline(this.strCari);

                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 3)
                {
                    #region rmhNgr
                    if ((rmhNgr1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        rmhNgr1.dataInisial = true;
                        rmhNgr1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        rmhNgr1.initModeLoad = true;
                    }
                    else
                    {
                        rmhNgr1.dataInisial = false;
                        rmhNgr1.initModeLoad = false;
                    }
                    setStrCari();
                    rmhNgr1.cariDataOnline(this.strCari);
                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 4)
                {
                    #region angkutan
                    if ((angkutan1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        angkutan1.dataInisial = true;
                        angkutan1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        angkutan1.initModeLoad = true;
                    }
                    else
                    {
                        angkutan1.dataInisial = false;
                        angkutan1.initModeLoad = false;
                    }
                    setStrCari();
                    angkutan1.cariDataOnline(this.strCari);

                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 5)
                {
                    #region bangunanAir
                    if ((bangunanAir1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        bangunanAir1.dataInisial = true;
                        bangunanAir1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        bangunanAir1.initModeLoad = true;
                    }
                    else
                    {
                        bangunanAir1.dataInisial = false;
                        bangunanAir1.initModeLoad = false;
                    }
                    setStrCari();
                    bangunanAir1.cariDataOnline(this.strCari);

                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 6)
                {
                    #region senjata
                    if ((senjata1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        senjata1.dataInisial = true;
                        senjata1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        senjata1.initModeLoad = true;
                    }
                    else
                    {
                        senjata1.dataInisial = false;
                        senjata1.initModeLoad = false;
                    }
                    setStrCari();
                    senjata1.cariDataOnline(this.strCari);

                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 7)
                {
                    #region mesinNonTik
                    if ((mesinNonTik1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        mesinNonTik1.dataInisial = true;
                        mesinNonTik1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        mesinNonTik1.initModeLoad = true;
                    }
                    else
                    {
                        mesinNonTik1.dataInisial = false;
                        mesinNonTik1.initModeLoad = false;
                    }
                    setStrCari();
                    mesinNonTik1.cariDataOnline(this.strCari);
                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 8)
                {
                    #region jlnJmbtn
                    if ((jlnJmbtn1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        jlnJmbtn1.dataInisial = true;
                        jlnJmbtn1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        jlnJmbtn1.initModeLoad = true;
                    }
                    else
                    {
                        jlnJmbtn1.dataInisial = false;
                        jlnJmbtn1.initModeLoad = false;
                    }
                    setStrCari();
                    jlnJmbtn1.cariDataOnline(this.strCari);
                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 9)
                {
                    #region propSus
                    if ((propSus1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        propSus1.dataInisial = true;
                        propSus1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        propSus1.initModeLoad = true;
                    }
                    else
                    {
                        propSus1.dataInisial = false;
                        propSus1.initModeLoad = false;
                    }
                    setStrCari();
                    propSus1.cariDataOnline(this.strCari);
                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 10)
                {
                    #region ATL
                    if ((ATL1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        ATL1.dataInisial = true;
                        ATL1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        ATL1.initModeLoad = true;
                    }
                    else
                    {
                        ATL1.dataInisial = false;
                        ATL1.initModeLoad = false;
                    }
                    setStrCari();
                    ATL1.cariDataOnline(this.strCari);
                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 11)
                {
                    #region mesinTik
                    if ((mesinTik1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        mesinTik1.dataInisial = true;
                        mesinTik1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        mesinTik1.initModeLoad = true;
                    }
                    else
                    {
                        mesinTik1.dataInisial = false;
                        mesinTik1.initModeLoad = false;
                    }
                    setStrCari();
                    mesinTik1.cariDataOnline(this.strCari);
                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 12)
                {
                    #region ATB
                    if ((ATB1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        ATB1.dataInisial = true;
                        ATB1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        ATB1.initModeLoad = true;
                    }
                    else
                    {
                        ATB1.dataInisial = false;
                        ATB1.initModeLoad = false;
                    }
                    setStrCari();
                    ATB1.cariDataOnline(this.strCari);
                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 13)
                {
                    #region KDP
                    if ((KDP1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        KDP1.dataInisial = true;
                        KDP1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        KDP1.initModeLoad = true;
                    }
                    else
                    {
                        KDP1.dataInisial = false;
                        KDP1.initModeLoad = false;
                    }
                    setStrCari();
                    KDP1.cariDataOnline(this.strCari);
                    #endregion
                }
                else if (xtbDetail1.SelectedTabPageIndex == 14)
                {
                    #region Renovasi
                    if ((renovasi1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                    {
                        renovasi1.dataInisial = true;
                        renovasi1.modeLoadData = "cari";
                        cariSebelumnya = teCari1.Text.Trim();
                        renovasi1.initModeLoad = true;
                    }
                    else
                    {
                        renovasi1.dataInisial = false;
                        renovasi1.initModeLoad = false;
                    }
                    setStrCari();
                    renovasi1.cariDataOnline(this.strCari);
                    #endregion
                }

              
            }
        }

        private void pencarianData(string kataCari)
        {
            if (xtbDetail1.SelectedTabPageIndex == 0)
            {
                //if (xtpPenggunaan.Text.Equals("Penggunaan BMN"))
                //{
                //    penggunaan1.strCari = kataCari;
                //    penggunaan1.getTindakLanjut();
                //}
                //else if (xtpPenggunaan.Text.Equals("Pemanfaatan BMN"))
                //{
                //    pemanfaatan1.strCari = kataCari;
                //    pemanfaatan1.getTindakLanjut();
                //}
            }
            if (xtbDetail1.SelectedTabPageIndex == 1)
            {
                tanah1.strCari = kataCari;
                tanah1.getTanah();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 2)
            {
                bangunan1.strCari = kataCari;
                bangunan1.getBangunan();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 3)
            {
                rmhNgr1.strCari = kataCari;
                rmhNgr1.getRmhNgr();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 4)
            {
                angkutan1.strCari = kataCari;
                angkutan1.getAngkutan();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 5)
            {
                bangunanAir1.strCari = kataCari;
                bangunanAir1.getBangunanAir();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 6)
            {
                senjata1.strCari = kataCari;
                senjata1.getSenjata();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 7)
            {
                mesinNonTik1.strCari = kataCari;
                mesinNonTik1.getMesinNonTik();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 8)
            {
                jlnJmbtn1.strCari = kataCari;
                jlnJmbtn1.getJlnJmbtn();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 9)
            {
                propSus1.strCari = kataCari;
                propSus1.getPropSus();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 10)
            {
                ATL1.strCari = kataCari;
                ATL1.getATL();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 11)
            {
                mesinTik1.strCari = kataCari;
                mesinTik1.getMesinTik();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 12)
            {
                ATB1.strCari = kataCari;
                ATB1.getATB();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 13)
            {
                KDP1.strCari = kataCari;
                KDP1.getKDP();
            }
            else if (xtbDetail1.SelectedTabPageIndex == 14)
            {
                renovasi1.strCari = kataCari;
                renovasi1.getRenovasi();
            }

            
        }

        #endregion


        private void gvMWasdal_ColumnFilterChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    teCari.Text = gvMWasdal.GetFocusedDisplayText();
            //    if (teCari.Text.Trim() != "")
            //    {
            //        cbNamaKolom.Text = gvMWasdal.FocusedColumn.ToString();
            //        fieldDicari = gvMWasdal.FocusedColumn.FieldName;
            //    }
            //    else
            //    {
            //        cbNamaKolom.Text = "";
            //        fieldDicari = "";
            //        this.strCari = "";
            //        if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            //    }
            //}
            //catch
            //{
            //}
        }

        public string nameTab1 = "";
        public string nameTab2 = "";
        private void ucMtrWasdal_Load(object sender, EventArgs e)
        {
            tabSelect();
            xtpPenggunaan.Text = namaModul;
            setVisibleKolom2(false);
            setVisibleKolomNup(false);
        }

        private void xtbDetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabSelect();
        }

        private void xtcPSP_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabSelect();
        }

        private void xtbDetail2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabSelect();
        }

       
    }



}
