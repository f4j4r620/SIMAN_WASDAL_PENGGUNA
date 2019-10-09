using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KES1.WL.PENGGUNAAN.DETAILSATKER
{
    public partial class ucSengketa : ucMain
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm form;
        public bool dataInisial = true;
        public BelumPspDetail detail;
        ArrayList data;
        public bool moreButton = true;
        string kode = "";

        /// <summary>
        ///  Service
        /// </summary>
        SvcMonSatkerGunaA25.InputParameters input;
        SvcMonSatkerGunaA25.OutputParameters output;
        SvcMonSatkerGunaA25.execute_pttClient client;
        SvcMonSatkerGunaA25.WASDALSROW_MON_GUNA_SATKER_A25 rowData;
        SengketaItem rowDataItem;
        public ucSengketa()
        {
            InitializeComponent();
        }

        public ucSengketa(string label, string kode)
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
                input = new SvcMonSatkerGunaA25.InputParameters();
                if (dataInisial)
                {
                    input.P_MIN = konfigApp.dataAwal;
                    input.P_MAX = konfigApp.dataMaks;
                    dataCount = 0;
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

                client = new SvcMonSatkerGunaA25.execute_pttClient();
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

        private delegate void ShowData(SvcMonSatkerGunaA25.OutputParameters output);

        private void ShowDataGrid(SvcMonSatkerGunaA25.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_GUNA_SATKER_A25.Count();
                dataCount += jmlData;
                if (jmlData == konfigApp.dataAkhir)
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
                SengketaItem[] dataTemp = SengketaItem.GetPSP();

                foreach (var item in output.SF_MON_GUNA_SATKER_A25)
                {
                    foreach (var item2 in dataTemp)
                    {
                        if (item.KD_JNS_BMN == item2.KD_JENIS_BMN)
                        {
                            item2.ID_SATKER = item.ID_SATKER;
                            item2.KD_SATKER = item.KD_SATKER;
                            item2.JML_TK_BANDING = item.JML_TK_BANDING;
                            item2.JML_TK_KASASI = item.JML_TK_KASASI;
                            item2.JML_TK_PENINJAUAN_KEMBALI = item.JML_TK_PENINJAUAN_KEMBALI;
                            item2.JML_TK_PERTAMA = item.JML_TK_PERTAMA;
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

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    rowDataItem = new SengketaItem();
            //    rowDataItem = (SengketaItem)gridView.GetFocusedRow();
            //    if (rowDataItem.ID_SATKER != null)
            //    {
            //        detail(rowDataItem.ID_SATKER + "", "");
            //    }
            //}
            //catch (Exception)
            //{

            //}
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
