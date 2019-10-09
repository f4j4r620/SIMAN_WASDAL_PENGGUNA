using System;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KSK.WL.MONLAPWASDAL
{
    public partial class ucLapMonSatkerGridSanksi : UserControl
    {
        // Delegate Method
        public ToggleProgressBar toggleProgressBar;
        public backLapMonSatker back;
        //public detailLapMonSatkerSanksidanCrud crud;
        // Service
        SvcLapMonWasdalSanksiSelect.execute_pttClient client;
        SvcLapMonWasdalSanksiSelect.InputParameters input;
        SvcLapMonWasdalSanksiSelect.OutputParameters output;
        public SvcLapMonWasdalSanksiSelect.WASDALSROW_MON_LAP_WASDAL_SANKSI rowData;
        public SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL rowDataTerpilih;

        // Control Data
        public bool dataInisial = true;
        ArrayList data;
        public bool moreButton = true;
        string kode = "";
        

        public ucLapMonSatkerGridSanksi()
        {
            InitializeComponent();
        }

        public ucLapMonSatkerGridSanksi(string kode, string label)
        {
            this.kode = kode;
            InitializeComponent();
            this.labelControl1.Text = label;
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

        //public void LoadData()
        //{
        //    dataInisial = true;
        //    this.GetData("");
        //}

        //public void MoreData()
        //{
        //    dataInisial = false;
        //    this.GetData("");
        //}
        #endregion

        //#region Load Data

        //int maxDataSanksi = 0;
        //public void GetDataSanksi(string where)
        //{
        //    try
        //    {
        //        FormNonAktif();
        //        input = new SvcLapMonWasdalSanksiSelect.InputParameters();
        //        if (dataInisial)
        //        {
        //            input.P_MIN = konfigApp.dataAwal;
        //            input.P_MAX = konfigApp.dataAkhir;
        //        }
        //        else
        //        {
        //            input.P_MIN = maxDataSanksi + 1;
        //            input.P_MAX = maxDataSanksi + konfigApp.dataAkhir;
        //        }
        //        input.P_MINSpecified = true;
        //        input.P_MAXSpecified = true;
        //        input.P_COL = "";
        //        input.P_SORT = "";
        //        input.STR_WHERE = "ID_MON_LAP = " + kode;

        //        client = new SvcLapMonWasdalSanksiSelect.execute_pttClient();
        //        client.Open();
        //        client.Beginexecute(input, new AsyncCallback(getResult), null);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageError(ex.Message);
        //    }
            
           
        //}

        //private void getResultSanksi(IAsyncResult result)
        //{
        //    try
        //    {
        //        output = client.Endexecute(result);
        //        client.Close();
        //        FormAktif();
        //        this.Invoke(new ShowDataSanksi(this.showDataGridSanksi), output);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageError(ex.Message);
        //    }
        //}

        //private delegate void ShowDataSanksi(SvcLapMonWasdalSanksiSelect.OutputParameters output);

        //private void showDataGridSanksi(SvcLapMonWasdalSanksiSelect.OutputParameters output) 
        //{
        //    try
        //    {
        //        int jmlData = output.SF_MON_LAP_WASDAL_SANKSI.Length;
        //        if (jmlData == konfigApp.dataAkhir)
        //        {
        //         // set tombol more  = true  
        //        }
        //        else
        //        {
        //          // set tombol more false
        //        }
        //        maxDataSanksi += jmlData;

        //        if (dataInisial)
        //        {
        //            data = new ArrayList();
        //        }

        //        data.AddRange(output.SF_MON_LAP_WASDAL_SANKSI);
        //        this.bsPgnBrg.DataSource = data;
        //        this.gcPgnBrg.RefreshDataSource();
               
        //    }
        //    catch (Exception ex)
        //    {
        //        FormAktif();
        //        MessageError(ex.Message);
        //    }
        
        //}

        //#endregion

        #region Grid Function

        private void repoButtonView_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //MessageBox.Show("Kampret");
            getDataFocus();
            if (rowData != null)
            {
                getInitFileUpload();
            }
        }
        
        #endregion

        #region CRUD
        private void getDataFocus()
        {
            rowData = new SvcLapMonWasdalSanksiSelect.WASDALSROW_MON_LAP_WASDAL_SANKSI();
            rowData = (SvcLapMonWasdalSanksiSelect.WASDALSROW_MON_LAP_WASDAL_SANKSI)gridView1.GetFocusedRow();
        }

        private void CRUD(string mode)
        {
            //getDataFocus();
            //if (rowData != null)
            //{
            //    crud();
            //}
            //else
            //{
            //    MessageBox.Show("Silahkan Pilih data terlebih dahulu","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //}
               
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            back();
        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            CRUD("V");
        }
        #endregion

        #region UPLOAD FILE
        SvcLapMonWasdalSanksiFile.execute_pttClient clientFile;
        SvcLapMonWasdalSanksiFile.OutputParameters outputFile;
        SvcLapMonWasdalSanksiFile.InputParameters inputFile;

        private void getInitFileUpload()
        {
            try
            {
                FormNonAktif();
                inputFile = new SvcLapMonWasdalSanksiFile.InputParameters();
                inputFile.P_SELECT = "R";
                inputFile.P_ID_MON_LAP_SANKSI = rowData.ID_MON_LAP_SANKSI.ToString();
                clientFile = new SvcLapMonWasdalSanksiFile.execute_pttClient();
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

        private delegate void DsFileUploadRead(SvcLapMonWasdalSanksiFile.OutputParameters dataout);

        private void dsfileUploadRead(SvcLapMonWasdalSanksiFile.OutputParameters dataout)
        {
            if (dataout.PO_RESULT == "Y")
            {
                AppPengguna.KSK.RSK.PU.FrmPuViewPdf frmPuViewPdf = new AppPengguna.KSK.RSK.PU.FrmPuViewPdf();
                System.IO.File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + dataout.PO_NM_FILE + ".pdf", dataout.PO_FILE_DOK);
                frmPuViewPdf.display(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + dataout.PO_NM_FILE + ".pdf");
                frmPuViewPdf.ShowDialog();
            }
            else
            {
                MessageBox.Show(dataout.PO_RESULT_MESSAGE);
            }
        }
        #endregion
    }
}
