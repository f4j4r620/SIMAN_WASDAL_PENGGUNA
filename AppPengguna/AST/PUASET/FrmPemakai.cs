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
    public delegate void ResetFrmPemakai(string text);
    public delegate void SaveFrmPemakai(string text);
    public partial class FrmPemakai : DevExpress.XtraEditors.XtraForm
    {
        public string FilePath;
        public string FileFotoPath;
        public string STATUS;
        public decimal? ID_SATKER;
        public decimal? ID_DETAIL;
        public string Table_foto;
        public string P_ID_TABLE;
        public string ID_JNSDOK;
        public decimal? ID_PHOTO;
        public string fotoName;
        public ResetFrmPemakai resetForm;
        public SaveFrmPemakai saveForm;
        private FrmPUSatker puSatker;
        public Thread myThread = null;
        private frmProgres progresBar = null;
        public bool FotoBaru = false;
        public datafoto DaftarFoto = new datafoto();
        public FrmPemakai(string _status)
        {
            InitializeComponent();
            this.STATUS = _status;
            if (STATUS == "detail")
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

        #region View Foto
        //------------- GET Foto di DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------
        protected void view_Foto()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = this.ID_DETAIL;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = this.P_ID_TABLE;
                parInp.P_TABLE = this.Table_foto;
                
                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient(konfigApp.SvcAsetGetDokSelect_name, konfigApp.SvcAsetGetDokSelect_address);
                svcAsetGetDokSelect.Open();
                svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getResultDok(IAsyncResult result)
        {
            try
            {
                this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
                svcAsetGetDokSelect.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowFileDok(this.showFileDok), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowFileDok(SvcAsetGetDokSelect.OutputParameters dataOut);

        public void showFileDok(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlData > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
               
               int idx = Foto.Images.Add(konfigApp.convert2bytmap(dok.ISI_FILE));
               DaftarFoto.PHOTO = dok.ISI_FILE;

            }
        }


        #endregion//ViewFoto
        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            
        }

        private void btnUploadDok_Click(object sender, EventArgs e)
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

        private void bbiRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.STATUS == "input")
            {

                this.FilePath = null;
                this.ID_SATKER = null;
                FotoBaru = false;
                Foto.Images.Clear();
                this.teFileName.ResetText();
                this.teKET.ResetText();
                this.teNamaPMK.ResetText();
                this.teNIP.ResetText();
                this.teTgl_SIP.ResetText();
                this.teTglMulai.ResetText();
                this.teTglSelesai.ResetText();
                this.teUr_Satker.ResetText();
                this.teJabFungsional.ResetText();
                this.teJabStr.ResetText();
                this.teNilaiSewa.ResetText();
                this.teNoKTP.ResetText();
                this.teNomorSuratIjin.ResetText();
                this.teTglLahir.ResetText();
                this.teTglPensiun.ResetText();
                this.teTglSK.ResetText();
                this.teTmptLahir.ResetText();
                this.teTmtJab.ResetText();
                this.ID_SATKER = null;
                this.teGolongan.ResetText();
                this.teAlamat.ResetText();
                this.teKodeUnitSatker.ResetText();
            }
            else
            {
                this.ResetFoto();
                this.resetForm("reset");
            }
        }

        public void ResetFoto()
        {
            Foto.Images.Clear();
            FotoBaru = false;
            if (DaftarFoto.PHOTO != null)
            {

               int idx = Foto.Images.Add(konfigApp.convert2bytmap(DaftarFoto.PHOTO));

            }

        }
        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
         
            saveForm("save");
        }

        private void btnCariSatker_Click(object sender, EventArgs e)
        {
            puSatker = new FrmPUSatker();
            puSatker.ambilSatker = new AmbilSatker(this.ambilSatker);
            puSatker.ShowDialog();
        }
        private void ambilSatker(decimal? id, string kode, string nama)
        {
            this.ID_SATKER = id;
          
            this.teKodeUnitSatker.Text = kode;
            this.teUr_Satker.Text = nama;
        }

        private void btnUploadFoto_Click(object sender, EventArgs e)
        {
            ofdFoto.InitialDirectory = "C:";
            ofdFoto.Filter = "(*.bmp, *.jpg, *.gif, *.png)|*.bmp;*.jpg;*.gif;*.png";
            ofdFoto.Multiselect = false;
            if (ofdFoto.ShowDialog() == DialogResult.OK)
            {
                    string fileName = "";
                    string filePath = "";
                    long fileSize = 0;
                    string creationTime = "";
                var size = new FileInfo(ofdFoto.FileName).Length;
                if (size > konfigApp.maksSizeFoto)
                {
                    MessageBox.Show(konfigApp.konfirmasiMaksimalFoto, konfigApp.judulKonfirmasi);
                    return;
                }
                else
                {
                     
                     FileFotoPath = ofdFoto.FileName;
                    fileName = ofdFoto.SafeFileName;
                    fotoName = ofdFoto.SafeFileName;
                    fileSize = new System.IO.FileInfo(ofdFoto.FileName).Length;
                    creationTime = new System.IO.FileInfo(ofdFoto.FileName).CreationTime.ToString();
                    Foto.Images.Clear();
                    int idx = Foto.Images.Add(Image.FromFile(ofdFoto.FileName));
                    Foto.SetCurrentImageIndex(idx);
                    FotoBaru = true;
                    Console.WriteLine(fileSize + creationTime);
                }
                    

                    
            }
        }

        private void FrmPemakai_Load(object sender, EventArgs e)
        {
            if (this.STATUS == "edit" || this.STATUS == "detail")
            {
                this.view_Foto();
                this.sbJnsDok.Enabled = false;  
            }
            if (this.STATUS == "input") 
            {
              this.teKodeUnitSatker.Text = konfigApp.kodeSatker;
              this.teUr_Satker.Text = konfigApp.namaSatker;
              this.ID_SATKER = konfigApp.idSatker;
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
          this.teKodeUnitSatker.Text = id;
          this.teJnsDok.Text = nama;
        }
        #endregion

       





    }
}