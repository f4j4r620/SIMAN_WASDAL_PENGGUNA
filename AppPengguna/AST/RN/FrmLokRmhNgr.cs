using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using AppPengguna.PU;
using DevExpress.XtraBars;
using System.IO;
namespace AppPengguna.AST.RN
{
    public delegate void SimpanLokasiRumahNegara(SvcLokRmhNgrCrud.InputParameters parIn);
    public partial class FrmLokRmhNgr : Form
    {

        public SimpanLokasiRumahNegara simpanLokasiRumahNegara;
        private FrmPuJenisDokumenBangunan pUJenisDokumen = new FrmPuJenisDokumenBangunan();
        private char modeCrud = 'A';
        Thread myThread = null;
        public decimal? ID_KRMH_NEG;
        public decimal? ID_KRMH_LOKASI;
        SvcLokRmhNgrCrud.call_pttClient svcFasBangunanCrud = null;

        private string JNS_DOK;
        public string STATUS;
        public string FilePath;
        SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI selectedData;
        public FrmLokRmhNgr(string status, decimal? id_KRMH_NEG, decimal? id_KRMH_LOKASI)
        {
            InitializeComponent();
            this.ID_KRMH_NEG = id_KRMH_NEG;
            this.teIdKbdg.Text = id_KRMH_NEG.ToString();
            this.ID_KRMH_LOKASI = id_KRMH_LOKASI;
            this.STATUS = status;
            if (this.STATUS == "detail")
            {
                this.barSimpan.Enabled = false;
            }
            this.teFileName.Properties.ReadOnly = false;
            
        }

        public FrmLokRmhNgr(string status, SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI _Data)
        {
            InitializeComponent();
            this.STATUS = status;
            if (this.STATUS == "detail")
            {
                this.barSimpan.Enabled = false;
            }
            this.selectedData = _Data;
            this.barBersih_ItemClick(null, null);

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
        private void PuLokasiBangunan_Load(object sender, EventArgs e)
        {
            this.barSimpan.Caption = konfigApp.labelSimpan;
            this.getInitRefBentuk();
        }

        private bool cek_input()
        {
            return true;
            /*
            string listrik = this.teJnsDok.Text.Trim();
            if (listrik == "")
            {
                return false;
            }else{
                return true;
            }
             */
        }
        private void barSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.cek_input())
            {
                string pesan = "";
                try
                {
                    SvcLokRmhNgrCrud.InputParameters parInp = new SvcLokRmhNgrCrud.InputParameters();

                    parInp.P_ID_KRMH_NEG = this.ID_KRMH_NEG;
                    parInp.P_ID_KRMH_NEGSpecified = true;
                    parInp.P_ID_KRMH_LOKASISpecified = true;
                    parInp.P_ID_KRMH_LOKASI = this.ID_KRMH_LOKASI;
                    parInp.P_LOK_AKSES = this.teAksesbilitas.Text.Trim();
                    parInp.P_LOKBATAS_B = this.teBatasBarat.Text.Trim();
                    parInp.P_LOKBATAS_S = this.teBatasSelatan.Text.Trim();
                    parInp.P_LOKBATAS_T = this.teBatasTimur.Text.Trim();
                    parInp.P_LOKBATAS_U = this.teBatasUtara.Text.Trim();
                    parInp.P_LOKBENTUK = this.teBentuk.Text.Trim();
                    parInp.P_LOKELEVASI = this.teElevasi.Text.Trim();
                    parInp.P_LOKKONTUR = this.teKontur.Text.Trim();
                    parInp.P_LOKPERUNTUKAN = this.tePeruntukan.Text.Trim();
                    parInp.P_GPS = this.teGPS.Text;
                    parInp.P_NMFILE = this.teFileName.Text;

                    if (this.STATUS == "input")
                    {
                        parInp.P_SELECT = "C";
                    }
                    else
                    {
                        parInp.P_SELECT = "U";
                    }

                    simpanLokasiRumahNegara(parInp);
                }
                catch (Exception E)
                {

                    MessageBox.Show(pesan.ToString(), konfigApp.judulGagalSimpan);
                }
            }
            else
            {
                MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
            }

        }





        private void barBatal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barBersih_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.STATUS == "input")
            {
                this.teBatasUtara.ResetText();
                this.teBatasBarat.ResetText();
                this.teAksesbilitas.ResetText();
                this.teBatasSelatan.ResetText();
                this.teBatasTimur.ResetText();
                this.teBentuk.ResetText();
                this.teElevasi.ResetText();
                this.teKontur.ResetText();
                this.tePeruntukan.ResetText();
                this.teFileName.ResetText();
            }
            else
            {
                this.ID_KRMH_NEG = selectedData.ID_KRMH_NEG;
                this.teIdKbdg.Text = selectedData.ID_KRMH_NEG.ToString();
                this.ID_KRMH_LOKASI = selectedData.ID_KRMH_LOKASI;
                this.teBatasUtara.Text = selectedData.LOKBATAS_U;
                this.teBatasBarat.Text = selectedData.LOKBATAS_B;

                this.teAksesbilitas.Text = selectedData.LOK_AKSES;
                this.teBatasSelatan.Text = selectedData.LOKBATAS_S;
                this.teBatasTimur.Text = selectedData.LOKBATAS_T;
                this.teBentuk.Text = selectedData.LOKBENTUK;
                this.teElevasi.Text = selectedData.LOKELEVASI;
                this.teKontur.Text = selectedData.LOKKONTUR;
                this.tePeruntukan.Text = selectedData.LOKPERUNTUKAN;
                this.teGPS.Text = selectedData.GPS;
                this.teFileName.Text = selectedData.NMFILE;
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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                string filePath;
                long fileSize = 0;
                string creationTime = "";
                
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Dispose();
                dialog.Title = "Open PDF Files";
                dialog.Filter = "PDF Files(*.pdf)|*.pdf";
                dialog.Multiselect = false;


                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    fileName = dialog.SafeFileName;
                    fileSize = new System.IO.FileInfo(dialog.FileName).Length;
                    creationTime = new System.IO.FileInfo(dialog.FileName).CreationTime.ToString();

                    if (fileSize < konfigApp.maksSizeFile)
                    {
                        this.FilePath = filePath;
                        teFileName.Text = fileName;

                    }
                    else
                    {
                        MessageBox.Show(konfigApp.konfirmasiMaksimalFile, konfigApp.judulGagalLain);
                    }
                    

                    Console.WriteLine(fileSize + creationTime);
                    
                }
            }
            catch
            {
                System.Console.WriteLine("gagal");
            }
        }

       

    }
}