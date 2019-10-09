using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KKW.WL.MONLAPWASDAL
{
    public partial class ucLapMonSatkerGridCatatan : UserControl
    {
        // Delegate Method
        public ToggleProgressBar toggleProgressBar;
        public backLapMonSatker back;
        // Service
        SvcLapMonWasdalCatatanSelect.execute_pttClient client;
        SvcLapMonWasdalCatatanSelect.InputParameters input;
        SvcLapMonWasdalCatatanSelect.OutputParameters output;
        public SvcLapMonWasdalCatatanSelect.WASDALSROW_MON_LAP_WASDAL_CATATAN rowData;
        public SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL rowDataTerpilih;

        // Control Data
        public bool dataInisial = true;
       
        public bool moreButton = true;
        string kode = "";
        

        public ucLapMonSatkerGridCatatan()
        {
            InitializeComponent();
        }

        public ucLapMonSatkerGridCatatan(string kode, string label)
        {
            InitializeComponent();
            this.kode = kode;
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

        public void LoadData()
        {
            dataInisial = true;
            this.GetData("");
        }

        public void MoreData()
        {
            dataInisial = false;
            this.GetData("");
        }
        #endregion

        #region Load Data
        int maxDataCatatan = 0;
        ArrayList dataCatatan;
        bool moreCatatan = true;
        public void GetData(string where)
        {
            try
            {
                FormNonAktif();
                input = new SvcLapMonWasdalCatatanSelect.InputParameters();
                if (dataInisial)
                {
                    input.P_MIN = konfigApp.dataAwal;
                    input.P_MAX = konfigApp.dataAkhir;
                }
                else
                {
                    input.P_MIN = maxDataCatatan + 1;
                    input.P_MAX = maxDataCatatan + konfigApp.dataAkhir;
                }
                input.P_MINSpecified = true;
                input.P_MAXSpecified = true;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = "ID_MON_LAP = " + kode;

                client = new SvcLapMonWasdalCatatanSelect.execute_pttClient();
                client.Open();
                client.Beginexecute(input, new AsyncCallback(getResult), null);

            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
            
           
        }

        private void getResult(IAsyncResult result)
        {
            try
            {
                output = client.Endexecute(result);
                client.Close();
                FormAktif();
                this.Invoke(new ShowData(this.ShowDataGrid), output);
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private delegate void ShowData(SvcLapMonWasdalCatatanSelect.OutputParameters output);

        private void ShowDataGrid(SvcLapMonWasdalCatatanSelect.OutputParameters output) 
        {
            try
            {
                int jmlData = output.SF_MON_LAP_WASDAL_CATATAN.Count();
                if (jmlData == konfigApp.dataAkhir)
                {
                    
                }
                else
                {
                    
                }
                maxDataCatatan += jmlData;

                if (dataInisial)
                {
                    dataCatatan = new ArrayList();
                }

                dataCatatan.AddRange(output.SF_MON_LAP_WASDAL_CATATAN);
                this.bsPgnBrg.DataSource = dataCatatan;
                this.gcPgnBrg.RefreshDataSource();
               
            }
            catch (Exception ex)
            {
                FormAktif();
                MessageError(ex.Message);
            }
        
        }

        #endregion

        #region Grid Function

        private void repoButtonView_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //MessageBox.Show("Kampret");
        }
        
        #endregion

        #region CRUD
        private void getDataFocus()
        {
            rowData = new SvcLapMonWasdalCatatanSelect.WASDALSROW_MON_LAP_WASDAL_CATATAN();
            rowData = (SvcLapMonWasdalCatatanSelect.WASDALSROW_MON_LAP_WASDAL_CATATAN)gridView1.GetFocusedRow();
        }

        private void CRUD(string mode)
        {
            //if (mode != "D")
            //{
            //    if (mode == "C")
            //    {
            //        rowData = null;
            //        Crud(mode, rowData, "SATKER : ");
            //    }
            //    else
            //    {
            //        getDataFocus();
            //        if (rowData != null)
            //        {
            //            Crud(mode, rowData, "SATKER : ");
            //        }
            //        else
            //        {
            //            Dialog.info("Silahkan Pilih data terlebih dahulu");
            //        }
            //    }
                
            //}
            //else
            //{
            //    getDataFocus();
            //    if (rowData != null)
            //    {
            //        DeleteCatatan();
            //    }
            //    else
            //    {
            //        Dialog.info("Silahkan Pilih data terlebih dahulu");
            //    }
            //}
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            back();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            CRUD("C");
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            CRUD("U");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            CRUD("D");
        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            CRUD("V");
        }

        #endregion
    }
}
