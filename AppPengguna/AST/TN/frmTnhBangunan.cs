using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.AST.TN
{
    internal partial class frmTnhBangunan : Form
    {
      
        private ucTnhBangunan uctnhbgn;
        private PU.FrmPUAsetBangunan astBgn;
        private PU.FrmPUSatker PuSatker;

        public string status;
        public decimal? NUM;
        private decimal? idKtnh;
        private decimal? idKtnhBgn;
        private decimal? setIdKbdg;
        private decimal? ID_SATKER;
        //ID_KTNH_BANGUNAN

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private SvcTnhBangunanSelect.BPSIMANSROW_M_KTNH_BANGUNAN selectedData;
        private SvcTnhBangunanCrud.call_pttClient crudData;
        private SvcTnhBangunanCrud.InputParameters inputCrud;
        private SvcTnhBangunanCrud.OutputParameters outCrud;

        private SvcSatkerSelect.call_pttClient svcSatkerSelect;
        private SvcSatkerSelect.OutputParameters outParSatker;

        private SvcBangunanSelect.call_pttClient svcCaller;
        private SvcBangunanSelect.InputParameters inputPar;
        private SvcBangunanSelect.OutputParameters outPar;

        public frmTnhBangunan(ucTnhBangunan uctnhbgn, string status)
        {
            InitializeComponent();
           
            this.uctnhbgn = uctnhbgn;
            this.status = status;
            this.idKtnh = uctnhbgn.IdKtnh;
            this.selectedData = uctnhbgn.selectedData;
            if (uctnhbgn.selectedData != null)
            {
                NUM = uctnhbgn.selectedData.NUM_KTNH;
            }
            this.init();

            this.astBgn = new PU.FrmPUAsetBangunan();

            this.astBgn.ambilLengkap = true;
            this.astBgn.ambilAsetBangunanLengkap = new PU.AmbilAsetBangunanLengkap(this.setKdKbdg);

            PuSatker = new PU.FrmPUSatker();
            this.PuSatker.ambilSatker = new PU.AmbilSatker(this.ambilsatker);

            if (this.status == "detail")
            {
                this.bbiSave.Enabled = false;
                this.btnSatker.Enabled = false;
                this.sbPilihKbdg.Enabled = false;
            }
        }

        private void ambilsatker(decimal? id, string kode, string nama)
        {
            this.ID_SATKER = id;
            this.teKodeSatker.Text = kode;
            this.teNamaSatker.Text = nama;
            if (this.ID_SATKER == konfigApp.idSatker)
            {
                this.sbPilihKbdg.Enabled = true;
            }
            else
            {
                this.sbPilihKbdg.Enabled = false;
            }
            this.teKD_BRG.Properties.ReadOnly = false;
            this.teNUP.Properties.ReadOnly = false;
            this.teKIB.Properties.ReadOnly = false;
        }

        public decimal? idKbdg
        {
            get { return this.setIdKbdg; }
            set { this.setIdKbdg = value; }
        }
        public string urKbdg
        {
            get { return this.teIdKbdg.Text; }
            set { this.teIdKbdg.Text = value; }
        }

        public void setKdKbdg(decimal? idData, string DataName, string KD_BRG, decimal? NUP, string KIB)
        {
            this.idKbdg = idData;
            this.urKbdg = DataName;
            this.teKD_BRG.Text = KD_BRG;
            this.teNUP.Text = NUP.ToString();
            this.teKIB.Text = KIB;
        }


        private void init()
        {
            if (this.status == "input")
            {
                this.gcTngBgn.Text = "Input Data Bangunan dan Gedung";
                this.idKtnhBgn = 0;
                this.teIdKbdg.ResetText();
                //this.teKodeSatker.ResetText();
                //this.teNamaSatker.ResetText();
                this.teNUP.ResetText();
                this.ID_SATKER = null;
                this.teKIB.ResetText();
                this.teKD_BRG.ResetText();
                this.teKodeSatker.Text = konfigApp.kodeSatker;
                this.teNamaSatker.Text = konfigApp.namaSatker;
            }
            else if (this.status == "edit")
            {

                this.gcTngBgn.Text = "Edit Data Bangunan dan Gedung";
                this.idKtnhBgn = selectedData.ID_KTNH_BANGUNAN;
                this.teIdKbdg.Text = selectedData.ID_KBDG.ToString();
                if (selectedData.KD_SATKER == "" && selectedData.UR_SATKER=="")
                {
                    this.teKodeSatker.Text = konfigApp.kodeSatker;
                    this.teNamaSatker.Text = konfigApp.namaSatker;
                }
                else
                {
                    this.teKodeSatker.Text = selectedData.KD_SATKER;
                    this.teNamaSatker.Text = selectedData.UR_SATKER;
                }
                this.teNUP.Text = selectedData.NO_ASET.ToString();
                this.ID_SATKER = selectedData.ID_SATKER;
                this.teKIB.Text = selectedData.NO_KIB;
                this.teKD_BRG.Text = selectedData.KD_BRG;
            }
            else
            {
                this.gcTngBgn.Text = "Detail Data Bangunan dan Gedung";
                this.idKtnhBgn = selectedData.ID_KTNH_BANGUNAN;
                this.teIdKbdg.Text = selectedData.ID_KBDG.ToString();
                this.teKodeSatker.Text = selectedData.KD_SATKER;
                this.teNamaSatker.Text = selectedData.UR_SATKER;
                this.teNUP.Text = selectedData.NO_ASET.ToString();
                this.ID_SATKER = selectedData.ID_SATKER;
                this.teKIB.Text = selectedData.NO_KIB;
                this.teKD_BRG.Text = selectedData.KD_BRG;
            }
        }

        #region Progress Bar
        public void progBar(BarItemVisibility str)
        {

            if (this.InvokeRequired)
            {
                ProgBar d = new ProgBar(progBar);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                this.beMarqueeBar.Visibility = str;
            }

        }

        public void ShowProgresBar()
        {
            this.progBar(BarItemVisibility.Always);
        }
        public void ShowProgresBarDelete()
        {

        }
        #endregion

        private void save_daftarBangunan()
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcTnhBangunanCrud.InputParameters parInp = new SvcTnhBangunanCrud.InputParameters();
                parInp.P_ID_KTNHSpecified = true;
                parInp.P_ID_KTNH_BANGUNANSpecified = true;
                parInp.P_ID_KBDGSpecified = true;

                parInp.P_ID_KTNH_BANGUNAN = idKtnhBgn;
                parInp.P_ID_KTNH = idKtnh;
                parInp.P_ID_KBDG = this.idKbdg;


                if (this.status == "input")
                {
                    parInp.P_SELECT = "C";
                }
                else
                {
                    parInp.P_SELECT = "U";
                }

                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                this.crudData = new SvcTnhBangunanCrud.call_pttClient(konfigApp.SvcTnhBangunanCrud_name, konfigApp.SvcTnhBangunanCrud_address);
                crudData.Open();
                this.crudData.Beginexecute(parInp, new AsyncCallback(crudBgnTnh), "");
            }
            catch
            {
                this.modeCrud = 'A';
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void cekBangunan(string kdsatker)
        {
            try
            {


                
                this.Enabled = false;
                inputPar = new SvcBangunanSelect.InputParameters();
                inputPar.P_COL = "";

                string where_satker = String.Format("UPPER(KD_SATKER) LIKE '{0}%' AND SUBSTR(UPPER(KD_SATKER),-2) = '{1}'", kdsatker.ToUpper().Substring(0, 15), kdsatker.ToUpper().Substring(18, 2));
                inputPar.P_MAX = 3;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                inputPar.STR_WHERE = where_satker +
                                        " AND UPPER(KD_BRG) = '" + this.teKD_BRG.Text.Trim().ToUpper() + "' " +
                                        " AND UPPER(NO_KIB) = '" + this.teKIB.Text.Trim().ToUpper() + "' " +
                                         " AND UPPER(NO_ASET) = '" + this.teNUP.Text.Trim().ToUpper() + "' "
                                         ;
                svcCaller = new SvcBangunanSelect.call_pttClient(konfigApp.SvcBangunanSelect_name, konfigApp.SvcBangunanSelect_address);
                svcCaller.Open();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getData), null);
            }
            catch
            {

                this.modeCrud = 'A';

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new Aktifkanform(this.aktifkanform), "");
                MessageBox.Show("Pengecekan bangunan gagal.", konfigApp.judulGagalAmbil);
            }
        }
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {


                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                this.Enabled = false;
                SvcSatkerSelect.InputParameters parinput = new SvcSatkerSelect.InputParameters();
                parinput.P_COL = "";


                parinput.P_MAX = 3;
                parinput.P_MAXSpecified = true;
                parinput.P_MIN = 0;
                parinput.P_MINSpecified = true;
                parinput.P_SORT = "";
                parinput.STR_WHERE = "UPPER(KD_SATKER) = '" + this.teKodeSatker.Text.Trim().ToUpper() + "'";
                svcSatkerSelect = new SvcSatkerSelect.call_pttClient(konfigApp.SvcSatkerSelect_name, konfigApp.SvcSatkerSelect_address);
                svcSatkerSelect.Open();
                svcSatkerSelect.Beginexecute(parinput, new AsyncCallback(this.getDataSatker), null);
            }
            catch
            {

                this.modeCrud = 'A';
                this.Invoke(new Aktifkanform(this.aktifkanform),"");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show("Pengecekan satker gagal.", konfigApp.judulGagalAmbil);
            }

        }


        public void getDataSatker(IAsyncResult result)
        { 
            try
            {
                this.outParSatker = svcSatkerSelect.Endexecute(result);
                svcSatkerSelect.Close();
                int jml = this.outParSatker.SF_ROW_R_SATKER.Count();
                if (jml == 1)
                {
                    this.ID_SATKER = this.outParSatker.SF_ROW_R_SATKER[0].ID_SATKER;
                    this.cekBangunan(this.teKodeSatker.Text.Trim().ToUpper());
                    //this.save_daftarBangunan();
                }
                else
                {

                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    this.Invoke(new Aktifkanform(this.aktifkanform), "");
                    MessageBox.Show("Satker dengan kode:"+this.teKodeSatker.Text +" tidak terdaftar", "Perhatian");

                }
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new Aktifkanform(this.aktifkanform), "");
                MessageBox.Show("Pengecekan satker gagal.", konfigApp.judulGagalAmbil);
            }
        }


        public  void getData(IAsyncResult result)
        {
            try
            {
                this.outPar = svcCaller.Endexecute(result);
                svcCaller.Close();

                this.Invoke(new Aktifkanform(this.aktifkanform), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);

                int jml = this.outPar.SF_ROW_M_KBDG.Count();
                if (jml == 1)
                {
                    this.idKbdg = this.outPar.SF_ROW_M_KBDG[0].ID_KBDG;
                    this.save_daftarBangunan();
                }
                else
                {
                    MessageBox.Show("Data Bangunan tidak terdaftar di satker dengan kode :"+this.teKodeSatker.Text, "Perhatian");
                   
                }
            }
            catch
            {
                this.Invoke(new Aktifkanform(this.aktifkanform), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show("Pengecekan bangunan gagal.", konfigApp.judulGagalAmbil);
            }
        }

        private delegate void Aktifkanform(string text);
        private void aktifkanform(string text)
        {
            this.Enabled = true;
        }
        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.init();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
       

        public void hapusData()
        {
            if (MessageBox.Show(konfigApp.teksHapusData + this.selectedData.UR_SSKEL.Trim() + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcTnhBangunanCrud.InputParameters parInp = new SvcTnhBangunanCrud.InputParameters();
     
                    parInp.P_ID_KTNH_BANGUNANSpecified = true;
                    parInp.P_ID_KTNH_BANGUNAN = idKtnhBgn;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcTnhBangunanCrud.call_pttClient(konfigApp.SvcTnhBangunanCrud_name, konfigApp.SvcTnhBangunanCrud_address);
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudBgnTnh), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }

        #region crud
        public void crudBgnTnh(IAsyncResult result)
        {
            try
            {
                outCrud = crudData.Endexecute(result);
                crudData.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new UbahDsDetail(this.ubahDsDetail), outCrud);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);

                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }

                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
               
            }
        }

        public delegate void UbahDsDetail(SvcTnhBangunanCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcTnhBangunanCrud.OutputParameters outCrud)
        {
            SvcTnhBangunanSelect.BPSIMANSROW_M_KTNH_BANGUNAN dataPenyama = new SvcTnhBangunanSelect.BPSIMANSROW_M_KTNH_BANGUNAN();

            dataPenyama.NUM = 99;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_KTNH = idKtnh;
            dataPenyama.ID_KTNH_BANGUNAN = outCrud.PO_ID_KTNH_BANGUNAN;

            switch (this.modeCrud)
            {
                case 'C':
                    //this.uctnhbgn.dataInisial = false;
                    //this.uctnhbgn.getById = true;
                    //this.uctnhbgn.getInitTnhBgn(" ID_KTNH_BANGUNAN = " + outCrud.PO_ID_KTNH_BANGUNAN.ToString());
                    this.uctnhbgn.search = "";
                    this.uctnhbgn.initGrid();
                    this.uctnhbgn.getInitTnhBgn();
                    this.Close();                    
                    break;
                case 'U':
                    uctnhbgn.binder.Remove(this.selectedData);
                    //this.uctnhbgn.dataInisial = false;
                    //this.uctnhbgn.getById = true;
                    //this.uctnhbgn.getInitTnhBgn(" ID_KTNH_BANGUNAN = " + outCrud.PO_ID_KTNH_BANGUNAN.ToString());
                    this.uctnhbgn.search = "";
                    this.uctnhbgn.initGrid();
                    this.uctnhbgn.getInitTnhBgn();
                    this.Close();

                    break;
                case 'D':
                    uctnhbgn.binder.Remove(this.selectedData);
                    uctnhbgn.gvUcDtl.RefreshData();
                    uctnhbgn.StrTotalGrid.Caption = (Convert.ToInt64(uctnhbgn.StrTotalGrid.Caption) - 1).ToString();
                    uctnhbgn.StrTotalDb.Caption = (Convert.ToInt64(uctnhbgn.StrTotalDb.Caption) - 1).ToString();
                    this.uctnhbgn.search = "";
                    this.uctnhbgn.initGrid();
                    this.uctnhbgn.getInitTnhBgn();
                    this.Close();
                    
                    break;
            }
        }

        #endregion

        private void sbPilihKbdg_Click(object sender, EventArgs e)
        {
            this.astBgn.ShowDialog();
        }

        private void btnSatker_Click(object sender, EventArgs e)
        {
            this.PuSatker.ShowDialog();
        }

        private void teKodeSatker_TextChanged(object sender, EventArgs e)
        {
            if (this.teKodeSatker.Text.Trim().ToUpper() == konfigApp.kodeSatker.ToUpper())
            {
                sbPilihKbdg.Enabled = true;
            }
            else
            {
                sbPilihKbdg.Enabled = false;
            }
        }

      
      

        
      

    }
}
