using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KKW.WL.PENGGUNAAN.DETAILSATKER
{
    public partial class ucSudahPSP : ucMain
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm form;
        public bool dataInisial = true;
        ArrayList data;
        public bool moreButton = true;
        string kode = "";

        /// <summary>
        ///  Service
        /// </summary>
        SvcMonSatkerGunaA21.InputParameters input;
        SvcMonSatkerGunaA21.OutputParameters output;
        SvcMonSatkerGunaA21.execute_pttClient client;
        SvcMonSatkerGunaA21.WASDALSROW_MON_GUNA_SATKER_A21 rowData;

        public ucSudahPSP()
        {
            InitializeComponent();
        }

        public ucSudahPSP(string label, string kode)
        {
            InitializeComponent();
            labelControl1.Text = label;
            this.kode = kode;
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

        #region Load Data
        int dataCount = 0;
        private void GetData(string where)
        {
            try
            {
                FormNonAktif();
                input = new SvcMonSatkerGunaA21.InputParameters();
                if (dataInisial)
                {
                    input.P_MIN = konfigApp.dataAwal;
                    input.P_MAX = konfigApp.dataMaks;
                }
                else
                {
                    input.P_MIN = dataCount + 1;
                    input.P_MAX = dataCount + konfigApp.dataMaks;
                }
                input.P_MINSpecified = true;
                input.P_MAXSpecified = true;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = "ID_SATKER = " + kode;

                client = new SvcMonSatkerGunaA21.execute_pttClient();
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

        private delegate void ShowData(SvcMonSatkerGunaA21.OutputParameters output);

        private void ShowDataGrid(SvcMonSatkerGunaA21.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_GUNA_SATKER_A21.Count();
                dataCount += jmlData;
                if (jmlData == konfigApp.dataMaks)
                {
                    //moreButton = true;
                    //setTombolMoreData(true);
                    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    //moreButton = false;
                    //setTombolMoreData(false);
                    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                if (dataInisial)
                {
                    data = new ArrayList();
                }
                SudahPSPItem[] dataTemp = SudahPSPItem.GetPSP();

                foreach (var item in output.SF_MON_GUNA_SATKER_A21)
                {
                    foreach (var item2 in dataTemp)
                    {
                        if (item.KD_JNS_BMN == item2.KD_JENIS_BMN)
                        {
                            item2.ID_SATKER = item.ID_SATKER;
                            item2.KD_SATKER = item.KD_SATKER;
                            item2.KUANTITAS_PSP_Y = item.KUANTITAS_PSP_Y;
                            item2.LUAS_PSP_Y = item.LUAS_PSP_Y;
                            item2.NIL_PERLH_Y = item.NIL_PERLH_PSP_Y;
                            item2.NUM = item.NUM;
                            item2.UR_SATKER = item.UR_SATKER;
                            break;
                        }
                    }
                }
                data.AddRange(dataTemp);
                this.BsMain.DataSource = data;
                this.gridControl.RefreshDataSource();

            }
            catch (Exception ex)
            {
                FormAktif();
                MessageError(ex.Message);
            }

        }

        private void setTombolMoreData(bool p)
        {
            if (p)
            {
                BtnMore.Enabled = true;
            }
            else
            {
                BtnMore.Enabled = false;
            }
        }

        #endregion

        #region Button
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(true);
        }

        private void BtnMore_Click(object sender, EventArgs e)
        {
            LoadData(false);
        }
        #endregion

        public void LoadData(bool state)
        {
            dataInisial = state;
            this.GetData("");
        }

        private void BtnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string pathToSave = "";
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Excel files (*.xls)|*.xls";
                    dialog.FilterIndex = 2;
                    dialog.RestoreDirectory = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pathToSave = dialog.FileName;
                    }
                }


                if (pathToSave == "") return;
                this.gridView.ExportToXls(pathToSave);
                System.Diagnostics.Process.Start(pathToSave);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
