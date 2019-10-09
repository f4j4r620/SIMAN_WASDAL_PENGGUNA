using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraBars;
using AppPengguna.PU;
using System.IO;

namespace AppPengguna.AST.PUASET
{
    public delegate void ResetFrmPemeliharaan(string text);
    public delegate void SaveFrmPemeliharaan(string text);
    public partial class frmPemeliharaan : DevExpress.XtraEditors.XtraForm
    {
        public string FilePath;
        private string fileJNS_PMK = "JNS_PMK.txt";
      
        public decimal? ID_SATKER = konfigApp.idSatker;
        public string Status;
        public string ID_JNSDOK;

        public ResetFrmPemeliharaan resetForm;
        public SaveFrmPemeliharaan saveForm;
        private FrmPUSatker puSatker;
        private FrmPUAkun puAkun;
        public Thread myThread = null;
        private frmProgres progresBar = null;
        public frmPemeliharaan(string _status)
        {
            InitializeComponent();
            this.Status = _status;
            if (Status == "detail")
            {
                this.bbiSave.Enabled = false;
                //this.teKdSatker.Text = konfigApp.kodeSatker;
                //this.teUrSatker.Text = konfigApp.namaSatker;
            }
            else if (Status == "edit") 
            {
               
              this.sbJnsDok.Enabled = false;
              this.teJnsDok.Enabled = false;
            }
        }
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
        public void nonAktifkanForm(string str)
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
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

        public void ShowProgresBar()
        {
            this.progBar(BarItemVisibility.Always);
        }
        public void ShowProgresBarDelete()
        {

        }
        #endregion

        SvcJnsPmkSelect.call_pttClient fetchData;
        SvcJnsPmkSelect.OutputParameters outData;
        private void getInitForm()
        {
            if (!createResource(fileJNS_PMK))
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                this.nonAktifkanForm("");
                SvcJnsPmkSelect.InputParameters parInp = new SvcJnsPmkSelect.InputParameters();
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_MAX = konfigApp.dataAkhir;
                parInp.P_MAXSpecified = true;
                parInp.P_COL = "JNS_PMK";
                parInp.P_SORT = "ASC";
                fetchData = new SvcJnsPmkSelect.call_pttClient();
                fetchData.Open();
                fetchData.Beginexecute(parInp, new AsyncCallback(this.getJnsPmk), null);
            }
            else
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader("Resource/" + fileJNS_PMK, true);
                while ((line = file.ReadLine()) != null)
                {
                    this.tePemelihara.Properties.Items.Add(line);
                }

                file.Close();
            }


        }

        protected void getJnsPmk(IAsyncResult result)
        {
            try
            {
                this.outData = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowData(this.showData), this.outData);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcJnsPmkSelect.OutputParameters dataOut);

        public void showData(SvcJnsPmkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_JNS_PMK.Count();

            System.IO.StreamWriter data = new System.IO.StreamWriter("Resource/" + fileJNS_PMK, false);

            for (int i = 0; i < jmlDataGroup; i++)
            {
                data.WriteLine(serviceOutPut.SF_ROW_R_JNS_PMK[i].JNS_PMK);
            }
            data.Close();

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Resource/" + fileJNS_PMK, true);
            while ((line = file.ReadLine()) != null)
            {
                this.tePemelihara.Properties.Items.Add(line);
            }

            file.Close();

        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                string filePath = "";
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
                        teFileName.Text = fileName;
                        FilePath = filePath;
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

        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Status == "input")
            {
                this.teFileName.ResetText();
                this.teJenisPemeliharaan.ResetText();
                //this.teKdSatker.ResetText();
                this.teKdSatker.Text = konfigApp.kodeSatker;
                this.teUrSatker.Text = konfigApp.namaSatker;
                this.teKeteragan.ResetText();
                this.teMAP.ResetText();
                this.teUrAkun.ResetText();
                this.teNilai.ResetText();
                this.teNomorDipa.ResetText();
                this.teNomorSP2D.ResetText();
                this.tePemelihara.ResetText();
                this.teUraian.ResetText();
                this.teJnsDok.ResetText();
                //this.teUrSatker.ResetText();
                this.FilePath = null;
                this.ID_SATKER = null;
            }
            else
            {
                this.resetForm("reset");
                this.teJnsDok.Enabled = false;
                this.sbJnsDok.Enabled = false;
            }
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveForm("save");
        }

        private void btnPemelihara_Click(object sender, EventArgs e)
        {
            puSatker = new FrmPUSatker();
            puSatker.ambilSatker = new AmbilSatker(this.ambilSatker);
            puSatker.ShowDialog();
        }

        private void ambilSatker(decimal? id, string kode, string nama)
        {
            this.ID_SATKER = id;
            this.teKdSatker.Text = kode;
            this.teUrSatker.Text = nama;
        }

        private void btnAkun_Click(object sender, EventArgs e)
        {
            puAkun = new FrmPUAkun();
            puAkun.ambilAkun = new AmbilAkun(this.ambilAkun);
            puAkun.ShowDialog();
        }

        private void ambilAkun(string kode, string nama)
        {
          
            this.teMAP.Text = kode;
            this.teUrAkun.Text = nama;
        }

        private void tePemelihara_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tePemelihara.Text.Trim() == "K/L")
            {
               
                this.btnPemelihara.Enabled = true;
                
            }
            else
            {
                this.teKdSatker.ResetText();
                this.teUrSatker.ResetText();
                //this.teKdSatker.Text = konfigApp.kodeSatker;
                //this.teUrSatker.Text = konfigApp.namaSatker;

                this.ID_SATKER = null;
                this.btnPemelihara.Enabled = false;
            }
        }

        private void frmPemeliharaan_Load(object sender, EventArgs e)
        {
           
            getInitForm();
            if (this.Status == "input")
            {
              this.teKdSatker.Text = konfigApp.kodeSatker;
              this.teUrSatker.Text = konfigApp.namaSatker;
              this.tePemelihara.Text = "K/L";
            }
            else if (this.Status == "edit") 
            {
              if (this.tePemelihara.Text != "K/L") 
              {
                this.teKdSatker.Text = null;
                this.teUrSatker.Text = null;
                this.btnPemelihara.Enabled = false;
                this.ID_SATKER = null;
              }
              
            }
            this.teFileName.Properties.ReadOnly = true;
            this.teJnsDok.Properties.ReadOnly = true;
        }

        #region jenis dokumen
        private AppPengguna.PU.FrmPuJnsDok jnsDok;
        private void sbJnsDok_Click(object sender, EventArgs e)
        {
            jnsDok = new AppPengguna.PU.FrmPuJnsDok();
            jnsDok.ambilJnsDok = new AppPengguna.PU.AmbilJnsDok(this.ambilJnsDok);
            jnsDok.ShowDialog();
        }
        private void ambilJnsDok(string id, string nama)
        {
            this.ID_JNSDOK = id;
            this.teJnsDok.Text = nama;
        }
        #endregion
    }
}