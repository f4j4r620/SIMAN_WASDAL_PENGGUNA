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

namespace AppPengguna.AST.PUASET
{
    public delegate void ResetFrmNilai(string text);
    public delegate void SaveFrmNilai(string text);
    public partial class frmNilai : DevExpress.XtraEditors.XtraForm
    {
        public string FilePath;
        public string Status;
        public ResetFrmNilai resetForm;
        public SaveFrmNilai saveForm;
        public string ID_JNSDOK;

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public frmNilai(string _status)
        {
            InitializeComponent();
            this.Status = _status;
            if (Status == "detail")
            {
                this.bbiSave.Enabled = false;
            }else if(Status == "edit")
            {
                
                this.teJnsDok.Enabled = false;
                this.sbJnsDok.Enabled = false; 
            }
            this.teFileName.Properties.ReadOnly = true;
            this.teJnsDok.Properties.ReadOnly = true;
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
                teNomorPenilaian.ResetText();
                this.teTglPenilaian.ResetText();
                this.teNilaiBuku.ResetText();
                this.teNilaiWajar.ResetText();
                this.teInstansiPenilai.ResetText();
                this.teFileName.ResetText();
                this.FilePath = null;
                this.teJnsDok.ResetText();
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