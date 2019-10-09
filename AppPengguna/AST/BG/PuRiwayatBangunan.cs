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

namespace AppPengguna.AST.BG
{
    public delegate void ResetFrmRiwayatBangunan(string text);
    public delegate void SaveFrmRiwayatBangunan(string text);
    public partial class PuRiwayatBangunan : DevExpress.XtraEditors.XtraForm
    {
        public ResetFrmRiwayatBangunan resetForm;
        public SaveFrmRiwayatBangunan saveForm;
        public Thread myThread = null;
        public string Status;
        public string FilePath;
        public string ID_JNSDOK;
        public string path;
        public PuRiwayatBangunan(string _status)
        {
            InitializeComponent();
            this.Status = _status;
            if (Status == "detail")
            {
                this.bbiSave.Enabled = false;
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

                this.FilePath = null;
                this.teAlamatKontraktor.ResetText();
                this.teFileName.ResetText();


                this.teNamaKontraktor.ResetText();
                this.teNoKontrak.ResetText();
                this.teNilaiKontrak.ResetText();
                this.teNPWPKontraktor.ResetText();
                this.teRiwayatBangunan.ResetText();

                this.teTglKontrak.ResetText();
                this.teTglMulaiKontrak.ResetText();
                this.teTglSelesai.ResetText();
                this.teTglPakai.ResetText();
            }
            else
            {
                this.FilePath = null;
                this.resetForm("reset");
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