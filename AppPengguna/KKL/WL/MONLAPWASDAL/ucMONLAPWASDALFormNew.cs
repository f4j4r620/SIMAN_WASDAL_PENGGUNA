using System;
using System.Windows.Forms;
using System.Threading;

namespace AppPengguna.KKL.WL.MONLAPWASDAL
{
    public partial class ucMONLAPWASDALFormNew : DevExpress.XtraEditors.XtraUserControl
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm aktifkanForm;
        public string statusForm = null;
        public SimpanDataMonLapWasdal simpanDataMONLAPWASDAL;
        private Thread myThread;
        public decimal? _idMONLAPWASDAL;
        public SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL dataTerpilih;
        public string lokasiFile = "";
        string oldNamaFile = "";
        string statusDownload = "I";
        
        #region Thread

        private void toggleProgBarPu(string kondisi)
        {
            if (kondisi == "start") this.aktifkanProgresBar();
            else this.nonAktifkanprogressBar();
        }

        private void aktifkanProgresBar()
        {
            toggleProgressBar("start");
        }

        private void nonAktifkanprogressBar()
        {
            toggleProgressBar("finish");
        }

        public delegate void AktifkanForm(string str);

        public void AKtifkanForm(string str)
        {
            this.Enabled = true;
        }
        #endregion Thread

        public ucMONLAPWASDALFormNew(string _status)
        {
            InitializeComponent();
            statusForm = _status;
            lokasiFile = "";
        }

        public void inisialisasiForm()
        {
            // Setting Form
            switch (statusForm)
            {
                case "C":
                    teNomor.Text = "";
                    teNamaFile.Text = "";
                    teThnAng.Text = ""+(konfigApp.tahunAnggaran -1);
                    teKdSatker.Text = konfigApp.kodeSatker;
                    teUrSatker.Text = konfigApp.namaSatker;
                    deTanggal.EditValue = DateTime.Now;
                    deTanggalKirim.EditValue = DateTime.Now;
                    oldNamaFile = "";
                    break;
                case "U":
                case "A":
                    teNomor.Text = dataTerpilih.NO_SURAT;
                    teNamaFile.Text = "";
                    oldNamaFile = "";
                    teThnAng.Text = dataTerpilih.THN_ANG;
                    teKdSatker.Text = dataTerpilih.KD_SATKER;
                    teUrSatker.Text = dataTerpilih.UR_SATKER;
                    deTanggal.EditValue = dataTerpilih.TGL_SURAT;
                    deTanggalKirim.EditValue = dataTerpilih.TGL_KIRIM;
                    break;
                case "D":
                    break;
            }

            if (statusForm == "U")
            {
               SetupFile("R");
            }
        }

        #region Button Simpan dan Kirim
        private void sbSimpan_Click(object sender, EventArgs e)
        {
            if (statusForm == "C")
            {
                try
                {
                    dataTerpilih = new SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL();
                    simpanDataMONLAPWASDAL("S");
                    dataTerpilih.ID_SATKER = konfigApp.idSatker;
                    dataTerpilih.KD_SATKER = konfigApp.kodeSatker;
                    dataTerpilih.UR_SATKER = konfigApp.namaSatker;
                    dataTerpilih.NO_SURAT = teNomor.Text;
                    dataTerpilih.TGL_SURAT = deTanggal.DateTime;
                    dataTerpilih.THN_ANG = teThnAng.Text;
                    dataTerpilih.STATUS_KIRIM = "N";
                    dataTerpilih.ID_MON_LAP = _idMONLAPWASDAL;
                    dataTerpilih.TGL_KIRIM = deTanggalKirim.DateTime;
                    statusForm = "U";
                }
                catch
                {
                    
                }
            }
            else
            {
                simpanDataMONLAPWASDAL("U");
            }
        }

        private void sbUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(File PDF)|*.pdf";
            ofd.Multiselect = false;
            ofd.Title = "Pilih Dokumen";
            ofd.FileName = "File PDF";
            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName != "")
            {
                lokasiFile = ofd.FileName;
                teNamaFile.Text = ofd.SafeFileName;
                //if (statusForm == "C")
                //{
                //    SetupFile("C");
                //}
            }
        }

        private void sbKirim_Click(object sender, EventArgs e)
        {
            if (this.statusForm == "U")
            {
                simpanDataMONLAPWASDAL("UK");
            }
            else
            {
                dataTerpilih = new SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL();
                simpanDataMONLAPWASDAL("C");
                dataTerpilih.ID_SATKER = konfigApp.idSatker;
                dataTerpilih.KD_SATKER = konfigApp.kodeSatker;
                dataTerpilih.UR_SATKER = konfigApp.namaSatker;
                dataTerpilih.NO_SURAT = teNomor.Text;
                dataTerpilih.TGL_SURAT = deTanggal.DateTime;
                dataTerpilih.THN_ANG = teThnAng.Text;
                dataTerpilih.STATUS_KIRIM = "N";
                dataTerpilih.ID_MON_LAP = _idMONLAPWASDAL;
                dataTerpilih.TGL_KIRIM = deTanggalKirim.DateTime;
                statusForm = "U";
            }
        }
        #endregion

        private void sbView_Click(object sender, EventArgs e)
        {
            if (teNamaFile.Text != "")
            {
                #region old
                //if (lokasiFile == "")
                //{
                //    // Download File 
                //    // Buka File
                //    // Hapus File
                //    //SetupFile("R");
                //}
                //else
                //{
                //    // lokasiFile yang tidak kosong dapat diartikan
                //    // bahwa file pertama kali di upload
                //    // Buka File
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                KSK.TL.PU.FrmPuViewPdf FrmPuViewPdf = new KSK.TL.PU.FrmPuViewPdf();
                FrmPuViewPdf.displayFile(System.IO.Path.Combine(appPath, teNamaFile.Text));
                FrmPuViewPdf.ShowDialog();
                //}
                #endregion
                
                //SetupFile("R");
            }
        }

        #region File Upload and Download
       
        SvcLapMonWasdalUploadFile.OutputParameters outputFile;
        SvcLapMonWasdalUploadFile.execute_pttClient clientFile;
        string statusFile = "";
        // statusFile == "R" , Download
        // statusFile == "C" , Upload

        private void MessageFail(string ex) 
        {
            nonAktifkanprogressBar();
            this.Invoke(new AktifkanForm(aktifkanForm), "");
            MessageBox.Show(konfigApp.teksGagalAmbil + ":" + ex, konfigApp.judulGagalAmbil);
        
        }

        public void UploadFile() 
        {
            if (teNamaFile.Text != "")
            {
                if (statusForm == "C")
                {
                    SetupFile("C");
                }
                else
                {
                    if (teNamaFile.Text != oldNamaFile)
                    {
                        SetupFile("C");
                    }
                }
            }
           
        }

        public void SetupFile(string mode) 
        {
            try
            {
                this.Enabled = false;
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                statusFile = mode;
                SvcLapMonWasdalUploadFile.InputParameters parInp = new SvcLapMonWasdalUploadFile.InputParameters();
                parInp.P_SELECT = mode;
                parInp.P_ID_MON_LAP = dataTerpilih.ID_MON_LAP.ToString();

                if (mode.Equals("C"))
                {
                    parInp.P_FILE_DOK = System.IO.File.ReadAllBytes(lokasiFile);
                    parInp.P_NM_FILE = teNamaFile.Text;
                    parInp.P_TGL_UPLOAD = DateTime.Now;
                    parInp.P_TGL_UPLOADSpecified = true;
                }
                clientFile = new SvcLapMonWasdalUploadFile.execute_pttClient();
                clientFile.Beginexecute(parInp, new AsyncCallback(getResultFile), null);
            }
            catch (Exception ex)
            {
                MessageFail(ex.Message); 
            }
        }

        private void getResultFile(IAsyncResult result)
        {
            try
            {
                outputFile = clientFile.Endexecute(result);
                clientFile.Close();
                this.Invoke(new AktifkanForm(AKtifkanForm), "");
                this.Invoke(new ToggleProgressBar(toggleProgBarPu), "finish");
                this.Invoke(new viewfile(ViewFile), outputFile);
            }
            catch (Exception ex)
            {
                MessageFail(ex.Message);
            }
        
        }

        private delegate void viewfile(SvcLapMonWasdalUploadFile.OutputParameters itemResult);
        AppPengguna.KSK.RSK.PU.FrmPuViewPdf frmPuViewpdf;
       
        private void ViewFile(SvcLapMonWasdalUploadFile.OutputParameters itemResult)
        {
            this.Invoke(new AktifkanForm(AKtifkanForm), "");
            if (itemResult.PO_RESULT == "Y") 
            {
                if (statusFile.Equals("C"))
                {
                    MessageBox.Show("File Berhasil di upload", "Informasi");
                    oldNamaFile = teNamaFile.Text;
                }
                else
                {
                    string appPath = AppDomain.CurrentDomain.BaseDirectory;
                    string nameFile = (itemResult.PO_NM_FILE.Contains(".pdf")) ? itemResult.PO_NM_FILE : itemResult.PO_NM_FILE + ".pdf";
                    string fileSave = System.IO.Path.Combine(appPath, nameFile.Replace('/', '-'));
                    System.IO.File.WriteAllBytes(fileSave, itemResult.PO_FILE_DOK);
                    teNamaFile.Text = nameFile.Replace('/', '-');
                    oldNamaFile = nameFile.Replace('/', '-');
                    //if (justLoadFile == false)
                    //{
                    //    frmPuViewpdf = new RSK.PU.FrmPuViewPdf();
                    //    frmPuViewpdf.display(fileSave);
                    //    frmPuViewpdf.ShowDialog();
                    //}
                }

            }
            else
            {
                teNamaFile.Text = "";
                oldNamaFile = "";
               // MessageBox.Show(itemResult.PO_RESULT_MESSAGE);
            }
        }

        #endregion

    }
}
