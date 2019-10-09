using System;
using System.Windows.Forms;
using AppPengguna;

namespace AppPengguna.KSK.WL.MONLAPWASDAL
{
    public partial class ucLapMonSatkerCudSanksi : UserControl
    {
        public DetailDataSanksi back;
        public ToggleProgressBar toggleProgressBar;
        string mode = "";
        // Service
        AppPengguna.SvcLapMonWasdalSanksiCud.execute_pttClient client;
        SvcLapMonWasdalSanksiCud.InputParameters input;
        SvcLapMonWasdalSanksiCud.OutputParameters output;

        // Item 
        public SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL rowDataTerpilih;
        public SvcLapMonWasdalSanksiSelect.WASDALSROW_MON_LAP_WASDAL_SANKSI rowData;
        decimal? idSatker;

        string modeFile = "";
        decimal? id_mon_lap_sanksi;
        
        public ucLapMonSatkerCudSanksi()
        {
            InitializeComponent();
        }
        
        public ucLapMonSatkerCudSanksi(string mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        #region Property
        private void AktifkanForm(string str)
        {
            if (str == "aktif")
                this.Enabled = true;
            else
                this.Enabled = false;

        }
        private void MessageError(string ex)
        {
            MessageBox.Show(konfigApp.teksGagalAmbil + ":" + ex, konfigApp.judulGagalAmbil);
            FormAktif();
        }
        private void FormAktif()
        {
            this.toggleProgressBar("finish");
            this.Invoke(new AktifkanForm(this.AktifkanForm), "aktif");
        }
        private void FormNonAktif()
        {
            this.toggleProgressBar("start");
            this.Invoke(new AktifkanForm(this.AktifkanForm), "nonaktif");
        }
        #endregion

        #region Settings Form
        public void SetLabel()
        {
            labelControl1.Text = "SATKER   : " + rowDataTerpilih.KD_SATKER + " - " + rowDataTerpilih.UR_SATKER + "\n" +
                                 "NO SURAT : " + rowDataTerpilih.NO_SURAT;
            SetMode(mode);
        }

        private void SetMode(string mode)
        {
            SetItemByMode(mode);
            if (mode == "V")
            {
                SetFormTransaction(false);
            }
            else
            {
                SetFormTransaction(true);
            }
        }

        private void SetItemByMode(string mode)
        {
           
            if (mode != "C")
            {
                SetItem();
            }
            else
            {
                teKodeSatker.Text = rowDataTerpilih.KD_SATKER;
                teUraianSatker.Text = rowDataTerpilih.UR_SATKER;
                id_mon_lap_sanksi = konfigApp.getGlobalId("ID_MON_LAP_SANKSI");
                ThnAnggaran.EditValue = konfigApp.tahunAnggaran;
            }
        }

        private void SetItem()
        {
            ThnAnggaran.EditValue = konfigApp.ToDate(rowData.THN_ANG);
            teKodeSatker.Text = rowData.KD_SATKER;
            teUraianSatker.Text = rowData.UR_SATKER;
            teTanggalSurat.EditValue = rowData.TGL_SURAT;
            teJenisSurat.Text = rowData.JNS_SURAT;
            teNomorSurat.Text = rowData.NO_SURAT_SANKSI;
            teFile.Text = "";
        }

        private void SetFormTransaction(bool transaction)
        {
            teNomorSurat.Properties.ReadOnly = !transaction;
            ThnAnggaran.Properties.ReadOnly = !transaction;
            teJenisSurat.Properties.ReadOnly = !transaction;
            teNomorSurat.Properties.ReadOnly = !transaction;
            teTanggalSurat.Properties.ReadOnly = !transaction;
            
            DevExpress.XtraLayout.Utils.LayoutVisibility temp = new DevExpress.XtraLayout.Utils.LayoutVisibility();
            
            if (transaction)
            {
                temp = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                
            }
            else
            {
                temp = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            layoutBtnCariFile.Visibility = temp;
            layoutBtnUploadFile.Visibility = temp;
            layoutBtnSave.Visibility = temp;
        }
        #endregion

        #region Button Events
        private void BtnSave_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            back(rowDataTerpilih.ID_MON_LAP + "", "SATKER : " + rowDataTerpilih.UR_SATKER);
        }

        public string filePath;
        OpenFileDialog openFileDialog1;
        private void BtnFindFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1 == null)
            {
                openFileDialog1 = new OpenFileDialog();
            }

            openFileDialog1.Filter = "(File PDF)|*.pdf";
            openFileDialog1.Multiselect = false;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                teFile.Text = filePath;
            }
        }

        private void BtnUploadFile_Click(object sender, EventArgs e)
        {
            if (teFile.Text.Trim() != null || teFile.Text.Trim() != "")
            {
                modeFile = "C";
                getInitFileUpload();

            }
            else
            {
                MessageBox.Show("File belum dipilih");
            }
        }

        private void BtnViewFile_Click(object sender, EventArgs e)
        {
            modeFile = "R";
            getInitFileUpload();
        }
        #endregion

        #region UPLOAD FILE
        AppPengguna.SvcLapMonWasdalSanksiFile.execute_pttClient clientFile;
        AppPengguna.SvcLapMonWasdalSanksiFile.OutputParameters outputFile;
        AppPengguna.SvcLapMonWasdalSanksiFile.InputParameters inputFile;

        private void getInitFileUpload()
        {
            try
            {
                FormNonAktif();
                inputFile = new AppPengguna.SvcLapMonWasdalSanksiFile.InputParameters();
                inputFile.P_SELECT = modeFile;

                inputFile.P_TGL_UPLOAD = konfigApp.DateToString(DateTime.Now);
                inputFile.P_FILE_DOK = konfigApp.FileToByteArray(teFile.Text);
                if (mode != "R")
                {
                    inputFile.P_ID_MON_LAP_SANKSI = (mode == "U") ? rowData.ID_MON_LAP_SANKSI.ToString() : id_mon_lap_sanksi.ToString();
                    inputFile.P_NM_FILE = openFileDialog1.SafeFileName;
                }
                clientFile = new AppPengguna.SvcLapMonWasdalSanksiFile.execute_pttClient();
                clientFile.Beginexecute(inputFile, new AsyncCallback(getResultFileUploadRead), "");

            }
            catch (Exception ex)
            {
                FormAktif();
                MessageBox.Show(konfigApp.teksGagalSimpan);
            }
        }

        private void getResultFileUploadRead(IAsyncResult result)
        {
            try
            {
                outputFile = clientFile.Endexecute(result);
                clientFile.Close();
                FormAktif();
                this.Invoke(new DsFileUploadRead(dsfileUploadRead), outputFile);
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private delegate void DsFileUploadRead(AppPengguna.SvcLapMonWasdalSanksiFile.OutputParameters dataout);

        private void dsfileUploadRead(AppPengguna.SvcLapMonWasdalSanksiFile.OutputParameters dataout)
        {
            if (dataout.PO_RESULT == "Y")
            {
                if (modeFile == "C")
                {
                    MessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulsukses);
                }
                else
                {
                    AppPengguna.KSK.RSK.PU.FrmPuViewPdf frmPuViewPdf = new AppPengguna.KSK.RSK.PU.FrmPuViewPdf();
                    System.IO.File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + dataout.PO_NM_FILE + ".pdf", dataout.PO_FILE_DOK);
                    frmPuViewPdf.display(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + dataout.PO_NM_FILE + ".pdf");
                    frmPuViewPdf.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(dataout.PO_RESULT_MESSAGE);
            }
        }
        #endregion
    }
}
