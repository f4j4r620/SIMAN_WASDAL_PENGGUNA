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
using System.IO;
using System.Reflection;
using AppPengguna.PU;

namespace AppPengguna.AST.PUASET
{
    public delegate void ResetFrmPengelola(string text);
    public delegate void SaveFrmPengelola(string text);
    public partial class frmPengelola : DevExpress.XtraEditors.XtraForm
    {
        public string FilePath;
        public string Status;
        public decimal? ID_SATKER;
        public string KD_PELAYANAN;
        public string ID_JNSDOK;
        private string fileJNS_PMK = "JNS_PMK.txt";
        public ResetFrmPengelola resetForm;
        public SaveFrmPengelola saveForm;
        private FrmPuPelayanan puPelayanan;
        private FrmPUSatker puSatker;
        public Thread myThread = null;
        private frmProgres progresBar = null;
        public frmPengelola(string _status)
        {
            InitializeComponent();

            this.Status = _status;
            if (Status == "detail")
            {
                this.bbiSave.Enabled = false;
                this.btnCariPengguna.Enabled = false;
                this.btnStatusPengelolaan.Enabled = false;
                this.btnUpload.Enabled = false;
            }
            else if (Status == "edit")
            {
                this.sbJnsDok.Enabled = false;
                this.teJnsDok.Enabled = false;
                if (teJenisPemakai.Text.Trim() == "K/L")
                {
                    this.tePihakPengelola.Properties.ReadOnly = true;
                    this.tePihakPengelola.Text = "";
                    this.btnCariPengguna.Enabled = true;
                }
                else
                {
                    this.tePihakPengelola.Properties.ReadOnly = true;
                    this.ID_SATKER = null;
                    this.btnCariPengguna.Enabled = false;
                }
            }

            this.teFileName.Properties.ReadOnly = true;
            this.teJnsDok.Properties.ReadOnly = true;

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
                        this.FilePath = filePath;
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
              
                this.FilePath = null;
                this.ID_SATKER = null;
                this.teJnsDok.ResetText();
            }
            else if(this.Status == "edit")
            {
              this.sbJnsDok.Enabled = false;
            }
            else
            {
                this.resetForm("reset");
            }
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveForm("save");
        }

        private void frmPengelola_Load(object sender, EventArgs e)
        {
            getInitForm();
            this.teFileName.Properties.ReadOnly = true;
            this.teJnsDok.Properties.ReadOnly = true;
            if (this.Status == "input")
            {
              if (this.teJenisPemakai.Text == "K/L")
              {
                this.tePihakPengelola.Properties.ReadOnly = true;
              }
            }
            if (this.Status == "edit") 
            {
              if (this.teJenisPemakai.Text == "K/L")
              {
                this.btnCariPengguna.Enabled = true;
                this.tePihakPengelola.Text = null;
              }
              else 
              {
                this.tePihakPengelola.Properties.ReadOnly = false;
                this.teKDPengelola.Text = null;
                this.teUr_Pengelola.Text = null;
                this.btnCariPengguna.Enabled = false;
              }
            }
            
        }
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
                    this.teJenisPemakai.Properties.Items.Add(line);
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

            System.IO.StreamWriter data = new System.IO.StreamWriter("Resource/" + fileJNS_PMK,false);

            for (int i = 0; i < jmlDataGroup; i++)
            {
                data.WriteLine(serviceOutPut.SF_ROW_R_JNS_PMK[i].JNS_PMK);
            }
            data.Close();
            
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Resource/" + fileJNS_PMK, true);
            while ((line = file.ReadLine()) != null)
            {
                this.teJenisPemakai.Properties.Items.Add(line);
            }

            file.Close();

        }

        private void btnStatusPengelolaan_Click(object sender, EventArgs e)
        {
            puPelayanan =new FrmPuPelayanan();
            puPelayanan.AmbilPelayanan = new AmbilPelayanan(this.AmbilPelayanan);
            puPelayanan.ShowDialog();
        }

        private void AmbilPelayanan(string _KD_PELAYANAN, string _NM_PELAYANAN)
        {
            this.KD_PELAYANAN = _KD_PELAYANAN;
            this.teStatusPegelolaan.Text = _NM_PELAYANAN;
        }

        private void btnCariPengguna_Click(object sender, EventArgs e)
        {
            puSatker = new FrmPUSatker();
            puSatker.ambilSatker = new AmbilSatker(this.ambilSatker);
            puSatker.ShowDialog();
        }

        private void ambilSatker(decimal? id, string kode, string nama){
            this.ID_SATKER = id;
            this.teKDPengelola.Text = kode;
            this.teUr_Pengelola.Text = nama;
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

        private void teJenisPemakai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (teJenisPemakai.Text.Trim() == "K/L")
            {
                this.tePihakPengelola.Properties.ReadOnly = true;
                this.btnCariPengguna.Enabled = true;
                this.teKDPengelola.ResetText();
                this.teUr_Pengelola.ResetText();
                this.tePihakPengelola.ResetText();
            }
            else
            {
                this.tePihakPengelola.Properties.ReadOnly = false;
                this.btnCariPengguna.Enabled = false;
                this.ID_SATKER = null;
                this.teKDPengelola.ResetText();
                this.teUr_Pengelola.ResetText();
            }
        }

    }
}