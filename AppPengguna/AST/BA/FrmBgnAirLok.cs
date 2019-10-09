using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.IO;


namespace AppPengguna.AST.BA
{
    public partial class FrmBgnAirLok : Form
    {
        private SvcBgnAirLokasiCrud.call_pttClient crudOutputBgnAirLok = null;
        private SvcBgnAirLokasiCrud.InputParameters dataInputBgnAirLok;
        private SvcBgnAirLokasiCrud.OutputParameters dataOutputBgnAirLok;

        private frmProgres progressForm;
        private frmProgres progresBar = null;
        Thread myThread = null;
        private char modeCrud = 'A';
        private string teksDialog = "";
        private decimal? idkbair;

        private void ShowData()
        {
            teBB.Text = ucbgnarilok_.SelectedData.LOKBATAS_B;
            teBS.Text = ucbgnarilok_.SelectedData.LOKBATAS_S;
            teBT.Text = ucbgnarilok_.SelectedData.LOKBATAS_T;
            teBU.Text = ucbgnarilok_.SelectedData.LOKBATAS_U;
            teBentuk.Text = ucbgnarilok_.SelectedData.LOKBENTUK;
            tePeruntukan.Text = ucbgnarilok_.SelectedData.LOKPERUNTUKAN;
            teKontur.Text = ucbgnarilok_.SelectedData.LOKKONTUR;
            teElevasi.Text = ucbgnarilok_.SelectedData.LOKELEVASI;
            teGPS.Text = ucbgnarilok_.SelectedData.GPS;
            teAksesbilitas.Text = ucbgnarilok_.SelectedData.LOK_AKSES;
        }
        public FrmBgnAirLok()
        {
            InitializeComponent();
        }

        private ucBgnAirLok ucbgnarilok_;
        private String status_;

        public FrmBgnAirLok(ucBgnAirLok ucbgnarilok, String Status)
        {
            InitializeComponent();
            this.ucbgnarilok_ = ucbgnarilok;
            this.status_ = Status;
            this.idkbair = ucbgnarilok_.ID_KBAIR;
            if (this.status_ == "edit") 
            {
                ShowData();
            }
            else if (this.status_ == "detail")
            {
                ShowData();
                this.bbsimpan.Enabled = false;
            }
        }

        

  
     

        public delegate void TutupProgresBar(string str);

        private void formProgresBar()
        {
            progresBar = new frmProgres();
            progresBar.Show();
        }
        
        public void tutupProgresBar(string str)
        {
            myThread.Abort();
        }

     


        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {            
            try
            {
                this.Enabled = false;
                myThread = new Thread(new ThreadStart(formProgresBar));
                myThread.Start();
                dataInputBgnAirLok = new SvcBgnAirLokasiCrud.InputParameters();
                dataInputBgnAirLok.P_ID_KBAIRSpecified = true;
                dataInputBgnAirLok.P_ID_KBAIR = ucbgnarilok_.SelectedData.ID_KBAIR;
                dataInputBgnAirLok.P_LOKBATAS_B = teBB.Text;
                dataInputBgnAirLok.P_LOKBATAS_S = teBS.Text;
                dataInputBgnAirLok.P_LOKBATAS_T = teBT.Text;
                dataInputBgnAirLok.P_LOKBATAS_U = teBU.Text;
                dataInputBgnAirLok.P_LOKBENTUK = teBentuk.Text;
                dataInputBgnAirLok.P_LOKELEVASI = teElevasi.Text;
                dataInputBgnAirLok.P_LOK_AKSES = teAksesbilitas.Text;
                dataInputBgnAirLok.P_LOKKONTUR = teKontur.Text;
                dataInputBgnAirLok.P_LOKPERUNTUKAN = tePeruntukan.Text;
                dataInputBgnAirLok.P_GPS = teGPS.Text;
                if (status_ == "edit")
                {
                    dataInputBgnAirLok.P_ID_KBAIR_LOKASISpecified = true;
                    dataInputBgnAirLok.P_ID_KBAIR_LOKASI = ucbgnarilok_.SelectedData.ID_KBAIR_LOKASI;
                    dataInputBgnAirLok.P_SELECT = "U";
                }
                else if (status_ == "input")
                {
                    dataInputBgnAirLok.P_SELECT = "C";
                }
                this.modeCrud = Convert.ToChar(dataInputBgnAirLok.P_SELECT);
                crudOutputBgnAirLok = new SvcBgnAirLokasiCrud.call_pttClient();
                crudOutputBgnAirLok.Open();
                crudOutputBgnAirLok.Beginexecute(dataInputBgnAirLok, new AsyncCallback(crudBangunanAirLokasi), "");
            }
            catch
            {
                this.modeCrud = 'A';
                this.Invoke(new TutupProgresBar(this.tutupProgresBar), "");
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            teAksesbilitas.Text = "";
            teBB.Text = "";
            teBentuk.Text = "";
            teBS.Text = "";
            teBT.Text = "";
            teBU.Text = "";
            teGPS.Text = "";
            tePeruntukan.Text = "";
        }

        #region simpan Data 
        private void crudBangunanAirLokasi(IAsyncResult result)
        {
            try
            {
                dataOutputBgnAirLok = crudOutputBgnAirLok.Endexecute(result);
                crudOutputBgnAirLok.Close();
                this.Invoke(new TutupProgresBar(this.tutupProgresBar), "");
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsBangunanAiLok(this.ubahDsBgnAirLok), dataOutputBgnAirLok);
                
            }
            catch
            {
                this.Invoke(new TutupProgresBar(this.tutupProgresBar), "");
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    this.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    this.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    this.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(this.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsBangunanAiLok(SvcBgnAirLokasiCrud.OutputParameters dataOutBgnAir);
        private decimal? ID_KBAIR_LOKASI;
        private decimal? NUM;
        private void ubahDsBgnAirLok(SvcBgnAirLokasiCrud.OutputParameters outCrud)
        {
            if (outCrud.PO_RESULT == "Y")
            {
                SvcBgnAirLokasiSelect.BPSIMANSROW_M_KBAIR_LOKASI dataPenyama = new SvcBgnAirLokasiSelect.BPSIMANSROW_M_KBAIR_LOKASI();

                dataPenyama.NUM = 99;
                dataPenyama.ID_KBAIR_LOKASI = (this.status_ == "input") ? outCrud.PO_ID_KBAIR_LOKASI : ucbgnarilok_.SelectedData.NUM;
                this.ID_KBAIR_LOKASI = outCrud.PO_ID_KBAIR_LOKASI;
                if (this.ucbgnarilok_.SelectedData != null)
                {
                    this.NUM = ucbgnarilok_.SelectedData.NUM;
                }
                switch (this.modeCrud)
                {
                    case 'C':


                        this.ucbgnarilok_.dataInisial = false;
                        this.ucbgnarilok_.getById = true;
                        this.ucbgnarilok_.getInitBgnAirLokasi(" ID_KBAIR_LOKASI = " + this.ID_KBAIR_LOKASI.ToString());

                        this.Close();

                        break;
                    case 'U':
                        ucbgnarilok_.binder.Remove(this.ucbgnarilok_.SelectedData);

                        this.ucbgnarilok_.dataInisial = false;
                        this.ucbgnarilok_.getById = true;
                        this.ucbgnarilok_.getInitBgnAirLokasi(" ID_KBAIR_LOKASI = " + this.ID_KBAIR_LOKASI.ToString());

                        this.Close();

                        break;
                    case 'D':
                        ucbgnarilok_.binder.Remove(this.ucbgnarilok_.SelectedData);
                        ucbgnarilok_.gvUcDtl.RefreshData();
                        ucbgnarilok_.StrTotalGrid.Caption = (Convert.ToInt64(ucbgnarilok_.StrTotalGrid.Caption) - 1).ToString();
                        ucbgnarilok_.StrTotalDb.Caption = (Convert.ToInt64(ucbgnarilok_.StrTotalDb.Caption) - 1).ToString();

                        this.Close();
                        break;
                }
            }
            else
            {
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalLain);
            }
        }
        #endregion
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
        private void getInitRefBentuk()
        {
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
                this.teBentuk.Properties.Items.Add(line);
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
                this.tePeruntukan.Properties.Items.Add(line);
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
                this.teKontur.Properties.Items.Add(line);
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
                this.teElevasi.Properties.Items.Add(line);
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
                this.teAksesbilitas.Properties.Items.Add(line);
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
                MessageBox.Show("Galgal mengambil data referensi aksesbilitas.", konfigApp.judulGagalAmbil);
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

        private void FrmBgnAirLok_Load(object sender, EventArgs e)
        {
            this.getInitRefBentuk();
        }

        public void hapusData()
        {
          
            try
            {
              myThread = new Thread(new ThreadStart(ShowProgresBar));
              myThread.Start();

              SvcBgnAirLokasiCrud.InputParameters parInp = new SvcBgnAirLokasiCrud.InputParameters();
              parInp.P_ID_KBAIR_LOKASISpecified = true;
              parInp.P_ID_KBAIR_LOKASI = this.ucbgnarilok_.SelectedData.ID_KBAIR_LOKASI;
              parInp.P_SELECT = "D";

              this.modeCrud = Convert.ToChar(parInp.P_SELECT);
              this.crudOutputBgnAirLok = new SvcBgnAirLokasiCrud.call_pttClient();
              crudOutputBgnAirLok.Open();
              this.crudOutputBgnAirLok.Beginexecute(parInp, new AsyncCallback(crudBangunanAirLokasi), "");
            }
            catch
            {
              this.modeCrud = 'A';
              this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
              MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
          
        }

    }
}
