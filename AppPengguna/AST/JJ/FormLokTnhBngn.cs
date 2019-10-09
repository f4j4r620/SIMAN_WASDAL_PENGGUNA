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
using System.IO;

namespace AppPengguna.AST.JJ
{
    internal partial class FormLokTnhBngn : Form
    {

        private UcLokasiJlnJmbtn uctnhlok = null;
        public string status;
        public decimal? NUM;
        public string FilePath;
        private decimal? ID_KJALJ;
        private decimal? ID_KJALJ_LOKASI;

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private SvcLokasiJlnJmbtnSelect.BPSIMANSROW_M_KJALJ_LOKASI selecetdData;
        private SvcLokasiJlnJmbtnCrud.call_pttClient crudData;
        private SvcLokasiJlnJmbtnCrud.InputParameters inputCrud;
        private SvcLokasiJlnJmbtnCrud.OutputParameters outCrud;

        public FormLokTnhBngn(UcLokasiJlnJmbtn uctnhlok, string status)
        {
            InitializeComponent();
            this.uctnhlok = uctnhlok;
            this.status = status;
            this.ID_KJALJ = uctnhlok.ID_KJALJ;
            this.selecetdData = uctnhlok.selectedData;
            if (uctnhlok.selectedData != null)
            {
                this.NUM = uctnhlok.selectedData.NUM;
            }
            //this.init();
            if (this.status == "detail")
            {
                this.bbiSave.Enabled = false;
            }
        }

        private void init()
        {
            if (this.status == "input")
            {
                this.gcLokTnh.Text = "Input Data Lokasi";
                this.ID_KJALJ_LOKASI = 0;
                this.teLokBatasU.ResetText();
                this.teLokBatasB.ResetText();
                this.teLokBatasS.ResetText();
                this.teLokBatasT.ResetText();
                this.teLokBentuk.ResetText();
                this.teLokPeruntukan.ResetText();
                this.teLokKontur.ResetText();
                this.teLokElevasi.ResetText();
                this.teLokAkses.ResetText();
                this.teGps.ResetText();
            }
            else
            {
               if (this.status == "edit"){
                this.gcLokTnh.Text = "Edit Data Lokasi";
               }
               else
               {
                 this.gcLokTnh.Text = "Detail Data Lokasi";
                 
               }
               this.ID_KJALJ_LOKASI = this.selecetdData.ID_KJALJ_LOKASI;
               this.teLokBatasU.Text = selecetdData.LOKBATAS_U;
               this.teLokBatasB.Text = selecetdData.LOKBATAS_B;
               this.teLokBatasS.Text = selecetdData.LOKBATAS_S;
               this.teLokBatasT.Text = selecetdData.LOKBATAS_T;
               this.teLokBentuk.Text = selecetdData.LOKBENTUK;
               this.teLokPeruntukan.Text = selecetdData.LOKPERUNTUKAN;
               this.teLokKontur.Text = selecetdData.LOKKONTUR;
               this.teLokElevasi.Text = selecetdData.LOKELEVASI;
               this.teLokAkses.Text = selecetdData.LOK_AKSES;
               this.teGps.Text = selecetdData.GPS;
            }
            
        }

        #region Progress Bar
        public void aktifkanForm(string str)
        {
            this.Enabled = true;
            try
            {
                this.Cursor = Cursors.Default;
            }
            catch (Exception)
            {

            }

        }
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
        public void nonAktifkanForm(string str)
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
        }
        public void ShowProgresBar()
        {
            this.progBar(BarItemVisibility.Always);
        }
        public void ShowProgresBarDelete()
        {

        }
        #endregion

        private void bbSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string batasBarat = teLokBatasB.Text;
            string batasTimur = teLokBatasT.Text;
            string batasSelatan = teLokBatasS.Text;
            string batasUtara = teLokBatasU.Text;
            string bentuk = teLokBentuk.Text;
            string elevasi = teLokElevasi.Text;
            string kontur = teLokKontur.Text;
            string peruntukan = teLokPeruntukan.Text;
            string akses = teLokAkses.Text;
            string gps = teGps.Text;

            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();

                inputCrud = new SvcLokasiJlnJmbtnCrud.InputParameters();
                inputCrud.P_ID_KJALJ = this.ID_KJALJ;
                inputCrud.P_ID_KJALJSpecified = true;

                inputCrud.P_ID_KJALJ_LOKASI = (this.status == "input") ? 0 : ID_KJALJ_LOKASI;
                inputCrud.P_ID_KJALJ_LOKASISpecified = true;
                inputCrud.P_LOKBATAS_B = batasBarat;
                inputCrud.P_LOKBATAS_T = batasTimur;
                inputCrud.P_LOKBATAS_S = batasSelatan;
                inputCrud.P_LOKBATAS_U = batasUtara;
                inputCrud.P_LOKBENTUK = bentuk;
                inputCrud.P_LOKELEVASI = elevasi;
                inputCrud.P_LOKKONTUR = kontur;
                inputCrud.P_LOKPERUNTUKAN = peruntukan;
                inputCrud.P_LOK_AKSES = akses;
                inputCrud.P_GPS = gps;
              

                if (this.status == "input")
                {
                  inputCrud.P_SELECT = "C";
                }
                else
                {
                  inputCrud.P_SELECT = "U";
                }

                this.modeCrud = Convert.ToChar(inputCrud.P_SELECT);
                this.crudData = new SvcLokasiJlnJmbtnCrud.call_pttClient();
                crudData.Open();
                this.crudData.Beginexecute(inputCrud, new AsyncCallback(crudLokTnh), "");
            }
            catch
            {
                this.modeCrud = 'A';
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
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
            if (MessageBox.Show(konfigApp.teksHapusData + " No. " + this.selecetdData.NUM.ToString() + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    inputCrud = new SvcLokasiJlnJmbtnCrud.InputParameters();
                    inputCrud.P_ID_KJALJ_LOKASISpecified = true;
                    inputCrud.P_ID_KJALJ_LOKASI = this.ID_KJALJ_LOKASI;
                    inputCrud.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(inputCrud.P_SELECT);
                    this.crudData = new SvcLokasiJlnJmbtnCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(inputCrud, new AsyncCallback(crudLokTnh), "");
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
        public void crudLokTnh(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcLokasiJlnJmbtnCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcLokasiJlnJmbtnCrud.OutputParameters outCrud)
        {
            SvcLokasiJlnJmbtnSelect.BPSIMANSROW_M_KJALJ_LOKASI dataPenyama = new SvcLokasiJlnJmbtnSelect.BPSIMANSROW_M_KJALJ_LOKASI();



            dataPenyama.NUM = -999;

            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_KJALJ = ID_KJALJ;
            dataPenyama.ID_KJALJ_LOKASI = outCrud.PO_ID_KJALJ_LOKASI;
            dataPenyama.LOKBATAS_U = teLokBatasU.Text;
            dataPenyama.LOKBATAS_B = teLokBatasB.Text;
            dataPenyama.LOKBATAS_S = teLokBatasS.Text;
            dataPenyama.LOKBATAS_T = teLokBatasT.Text;
            dataPenyama.LOKBENTUK = teLokBentuk.Text;
            dataPenyama.LOKPERUNTUKAN = teLokPeruntukan.Text;
            dataPenyama.LOKKONTUR = teLokKontur.Text;
            dataPenyama.LOKELEVASI = teLokElevasi.Text;
            dataPenyama.LOK_AKSES = teLokAkses.Text;
            dataPenyama.GPS = teGps.Text;

            switch (this.modeCrud)
            {
                case 'C':
                    dataPenyama.NUM = uctnhlok.binder.Count + 1;
                    uctnhlok.binder.Add(dataPenyama);
                    uctnhlok.gvUcDtl.RefreshData();
                    uctnhlok.StrTotalGrid.Caption = (Convert.ToInt64(uctnhlok.StrTotalGrid.Caption) + 1).ToString();
                    uctnhlok.StrTotalDb.Caption = (Convert.ToInt64(uctnhlok.StrTotalDb.Caption) + 1).ToString();
                    this.init();
                    this.Close();
                    break;
                case 'U':
                    dataPenyama.NUM = selecetdData.NUM;
                    uctnhlok.binder.Remove(this.selecetdData);
                    uctnhlok.binder.Add(dataPenyama);
                    uctnhlok.gvUcDtl.RefreshData();
                    this.init();
                    this.Close();
                    //uctnhdok.dataInisial = true;
                    //uctnhdok.initTnhDok();

                    break;
                case 'D':
                    uctnhlok.binder.Remove(this.selecetdData);
                    uctnhlok.gvUcDtl.RefreshData();
                    uctnhlok.StrTotalGrid.Caption = (Convert.ToInt64(uctnhlok.StrTotalGrid.Caption) - 1).ToString();
                    uctnhlok.StrTotalDb.Caption = (Convert.ToInt64(uctnhlok.StrTotalDb.Caption) - 1).ToString();
                    //ucgrup.dsGroup.Remove(ucgrup.groupPilihan);
                    //ucgrup.dataInisialGroup = false;
                    //ucgrup.displayData();
                    this.init();
                    this.Close();
                    break;
            }
        }

        #endregion

        private bool createResource(string fileName)
        {
            bool exist = false;
            fileName = "Resource/" + fileName;
            DirectoryInfo dir = new DirectoryInfo("Resource");
            if (!dir.Exists)
            {
                dir.Create();
                exist = false;
            }
            else if (!File.Exists(fileName))
            {
                //TextWriter tw = new StreamWriter(fileName, true);
                //tw.Close();
                exist = false;
            }
            else
            {
                DateTime fileCreatedDate = File.GetCreationTime(fileName);
                DateTime now;
                now = DateTime.Now;
                if ((now - fileCreatedDate).TotalHours > konfigApp.MaksHours)
                {
                    exist = false;
                }
                else
                {
                    exist = true;
                }
            }

            return exist;
        }

        private void frmTnhLok_Load(object sender, EventArgs e)
        {
          if (this.status != "hapus")
          {
            getInitRefBentuk();
          }
            
        }

        SvcRefBentukSelect.call_pttClient svcRefBentrukSelect;
        SvcRefBentukSelect.OutputParameters outRefBentrukSelect;
        SvcRefPeruntukanSelect.call_pttClient svcRefPeruntukanSelect;
        SvcRefPeruntukanSelect.OutputParameters outRefPeruntukanSelect;
        SvcRefKonturSelect.call_pttClient svcRefKonturSelect;
        SvcRefKonturSelect.OutputParameters outRefKonturSelect;
        SvcRefElevasiSelect.call_pttClient svcRefElevasiSelect;
        SvcRefElevasiSelect.OutputParameters outRefElevasiSelect;
        SvcRefAksesSelect.call_pttClient svcRefAksesSelect;
        SvcRefAksesSelect.OutputParameters outRefAksesSelect;
        private static string fileBENTUK = "RefBentuk.txt";
        private static string filePERUNTUKAN = "RefPeruntukan.txt";
        private static string fileKONTUR = "RefKontur.txt";
        private static string fileELEVASI = "RefElevasi.txt";
        private static string fileAKSESBILITAS = "RefAksesbilitas.txt";
        private void getInitRefBentuk()
        {
            this.init();
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifkanForm("");
            if (!createResource(fileBENTUK))
            {

                SvcRefBentukSelect.InputParameters parInp = new SvcRefBentukSelect.InputParameters();
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_MAX = konfigApp.maksReferensi;
                parInp.P_MAXSpecified = true;
                parInp.P_COL = "UR_BENTUK";
                parInp.P_SORT = "ASC";
                svcRefBentrukSelect = new SvcRefBentukSelect.call_pttClient();
                svcRefBentrukSelect.Open();
                svcRefBentrukSelect.Beginexecute(parInp, new AsyncCallback(this.getRefBentuk), null);
            }
            else
            {
                this.isiRefBentuk();
            }
        }

        private void isiRefBentuk()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Resource/" + fileBENTUK, true);
            while ((line = file.ReadLine()) != null)
            {
                this.teLokBentuk.Properties.Items.Add(line);
            }

            file.Close();
            getInitRefPeruntukan();
        }

        protected void getRefBentuk(IAsyncResult result)
        {
            try
            {
                this.outRefBentrukSelect = svcRefBentrukSelect.Endexecute(result);
                svcRefBentrukSelect.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowRefBentuk(this.showRefBentuk), this.outRefBentrukSelect);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show("Galgal mengambil data referensi bentuk tanah.", konfigApp.judulGagalAmbil);
                getInitRefPeruntukan();
            }
        }

        private delegate void ShowRefBentuk(SvcRefBentukSelect.OutputParameters dataOut);

        public void showRefBentuk(SvcRefBentukSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_LOK_BENTUK.Count();

            System.IO.StreamWriter data = new System.IO.StreamWriter("Resource/" + fileBENTUK, false);

            for (int i = 0; i < jmlDataGroup; i++)
            {
                data.WriteLine(serviceOutPut.SF_ROW_R_LOK_BENTUK[i].UR_BENTUK);
            }
            data.Close();

            this.isiRefBentuk();

        }

        private void getInitRefPeruntukan()
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifkanForm("");
            if (!createResource(filePERUNTUKAN))
            {

                SvcRefPeruntukanSelect.InputParameters parInp = new SvcRefPeruntukanSelect.InputParameters();
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_MAX = konfigApp.maksReferensi;
                parInp.P_MAXSpecified = true;
                parInp.P_COL = "UR_PERUNTUKAN";
                parInp.P_SORT = "ASC";
                svcRefPeruntukanSelect = new SvcRefPeruntukanSelect.call_pttClient();
                svcRefPeruntukanSelect.Open();
                svcRefPeruntukanSelect.Beginexecute(parInp, new AsyncCallback(this.getRefPeruntukan), null);
            }
            else
            {
                this.isiRefPeruntukan();
            }
        }

        private void isiRefPeruntukan()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Resource/" + filePERUNTUKAN, true);
            while ((line = file.ReadLine()) != null)
            {
                this.teLokPeruntukan.Properties.Items.Add(line);
            }

            file.Close();
            getInitRefKontur();
        }

        protected void getRefPeruntukan(IAsyncResult result)
        {
            try
            {
                this.outRefPeruntukanSelect = svcRefPeruntukanSelect.Endexecute(result);
                svcRefPeruntukanSelect.Close();
                //this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                //this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowRefPeruntukan(this.showRefPeruntukan), this.outRefPeruntukanSelect);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show("Galgal mengambil data referensi peruntukkan tanah.", konfigApp.judulGagalAmbil);
                //getInitRefPeruntukan();
            }
        }

        private delegate void ShowRefPeruntukan(SvcRefPeruntukanSelect.OutputParameters dataOut);

        public void showRefPeruntukan(SvcRefPeruntukanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_LOK_PERUNTUKAN.Count();

            System.IO.StreamWriter data = new System.IO.StreamWriter("Resource/" + filePERUNTUKAN, false);

            for (int i = 0; i < jmlDataGroup; i++)
            {
                data.WriteLine(serviceOutPut.SF_ROW_R_LOK_PERUNTUKAN[i].UR_PERUNTUKAN);
            }
            data.Close();

            this.isiRefPeruntukan();

        }

        private void getInitRefKontur()
        {
            //myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            //myThread.Start();
            this.nonAktifkanForm("");
            if (!createResource(fileKONTUR))
            {

                SvcRefKonturSelect.InputParameters parInp = new SvcRefKonturSelect.InputParameters();
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_MAX = konfigApp.maksReferensi;
                parInp.P_MAXSpecified = true;
                parInp.P_COL = "UR_KONTUR";
                parInp.P_SORT = "ASC";
                svcRefKonturSelect = new SvcRefKonturSelect.call_pttClient();
                svcRefKonturSelect.Open();
                svcRefKonturSelect.Beginexecute(parInp, new AsyncCallback(this.getRefKontur), null);
            }
            else
            {
                this.isiRefKontur();
            }
        }

        private void isiRefKontur()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Resource/" + fileKONTUR, true);
            while ((line = file.ReadLine()) != null)
            {
                this.teLokKontur.Properties.Items.Add(line);
            }

            file.Close();
            getInitRefElevasi();
        }

        protected void getRefKontur(IAsyncResult result)
        {
            try
            {
                this.outRefKonturSelect = svcRefKonturSelect.Endexecute(result);
                svcRefKonturSelect.Close();
                //this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                //this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowRefKontur(this.showRefKontur), this.outRefKonturSelect);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show("Galgal mengambil data referensi topografi kontur tanah.", konfigApp.judulGagalAmbil);

            }
        }

        private delegate void ShowRefKontur(SvcRefKonturSelect.OutputParameters dataOut);

        public void showRefKontur(SvcRefKonturSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_LOK_KONTUR.Count();

            System.IO.StreamWriter data = new System.IO.StreamWriter("Resource/" + fileKONTUR, false);

            for (int i = 0; i < jmlDataGroup; i++)
            {
                data.WriteLine(serviceOutPut.SF_ROW_R_LOK_KONTUR[i].UR_KONTUR);
            }
            data.Close();

            this.isiRefKontur();

        }

        private void getInitRefElevasi()
        {
            //myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            //myThread.Start();
            this.nonAktifkanForm("");
            if (!createResource(fileELEVASI))
            {

                SvcRefElevasiSelect.InputParameters parInp = new SvcRefElevasiSelect.InputParameters();
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_MAX = konfigApp.maksReferensi;
                parInp.P_MAXSpecified = true;
                parInp.P_COL = "UR_ELEVASI";
                parInp.P_SORT = "ASC";
                svcRefElevasiSelect = new SvcRefElevasiSelect.call_pttClient();
                svcRefElevasiSelect.Open();
                svcRefElevasiSelect.Beginexecute(parInp, new AsyncCallback(this.getRefElevasi), null);
            }
            else
            {
                this.isiRefElevasi();
            }
        }

        private void isiRefElevasi()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Resource/" + fileELEVASI, true);
            while ((line = file.ReadLine()) != null)
            {
                this.teLokElevasi.Properties.Items.Add(line);
            }

            file.Close();
            getInitRefAksesbilitas();
        }

        protected void getRefElevasi(IAsyncResult result)
        {
            try
            {
                this.outRefElevasiSelect = svcRefElevasiSelect.Endexecute(result);
                svcRefElevasiSelect.Close();
                //this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                //this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowRefElevasi(this.showRefElevasi), this.outRefElevasiSelect);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show("Galgal mengambil data referensi topografi elevasi tanah.", konfigApp.judulGagalAmbil);
                //getInitRefPeruntukan();
            }
        }

        private delegate void ShowRefElevasi(SvcRefElevasiSelect.OutputParameters dataOut);

        public void showRefElevasi(SvcRefElevasiSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_LOK_ELEVASI.Count();

            System.IO.StreamWriter data = new System.IO.StreamWriter("Resource/" + fileELEVASI, false);

            for (int i = 0; i < jmlDataGroup; i++)
            {
                data.WriteLine(serviceOutPut.SF_ROW_R_LOK_ELEVASI[i].UR_ELEVASI);
            }
            data.Close();

            this.isiRefElevasi();

        }

        private void getInitRefAksesbilitas()
        {
            //myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            //myThread.Start();
            this.nonAktifkanForm("");
            if (!createResource(fileAKSESBILITAS))
            {

                SvcRefAksesSelect.InputParameters parInp = new SvcRefAksesSelect.InputParameters();
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_MAX = konfigApp.maksReferensi;
                parInp.P_MAXSpecified = true;
                parInp.P_COL = "UR_AKSES";
                parInp.P_SORT = "ASC";
                svcRefAksesSelect = new SvcRefAksesSelect.call_pttClient();
                svcRefAksesSelect.Open();
                svcRefAksesSelect.Beginexecute(parInp, new AsyncCallback(this.getRefAksesbilitas), null);
            }
            else
            {
                this.isiRefAksesbilitas();
            }
        }

        private void isiRefAksesbilitas()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Resource/" + fileAKSESBILITAS, true);
            while ((line = file.ReadLine()) != null)
            {
                this.teLokAkses.Properties.Items.Add(line);
            }

            file.Close();
            if (this.InvokeRequired)
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
            }
            else
            {
                this.aktifkanForm("");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.progBar(BarItemVisibility.Never);

            }

        }

        protected void getRefAksesbilitas(IAsyncResult result)
        {
            try
            {
                this.outRefAksesSelect = svcRefAksesSelect.Endexecute(result);
                svcRefAksesSelect.Close();
                //this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                //this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowRefAksesbilitas(this.showRefAksesbilitas), this.outRefAksesSelect);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show("Galgal mengambil data referensi aksesbilitas tanah.", konfigApp.judulGagalAmbil);
                //getInitRefPeruntukan();
            }
        }

        private delegate void ShowRefAksesbilitas(SvcRefAksesSelect.OutputParameters dataOut);

        public void showRefAksesbilitas(SvcRefAksesSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_LOK_AKSES.Count();

            System.IO.StreamWriter data = new System.IO.StreamWriter("Resource/" + fileAKSESBILITAS, false);

            for (int i = 0; i < jmlDataGroup; i++)
            {
                data.WriteLine(serviceOutPut.SF_ROW_R_LOK_AKSES[i].UR_AKSES);
            }
            data.Close();

            this.isiRefAksesbilitas();

        }


    }
}
