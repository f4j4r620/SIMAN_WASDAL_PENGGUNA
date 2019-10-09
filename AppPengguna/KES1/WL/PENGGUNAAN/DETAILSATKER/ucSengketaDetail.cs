using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace AppPengguna.KES1.WL.PENGGUNAAN.DETAILSATKER
{
    public partial class ucSengketaDetail : ucMain
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm form;
        public bool dataInisial = true;
        public InvokeHandler back;
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

        public ucSengketaDetail()
        {
            InitializeComponent();
        }

        public ucSengketaDetail(string label, string kode)
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

        private void GetData(string where)
        {
            try
            {
                FormNonAktif();
                input = new SvcMonSatkerGunaA25.InputParameters();
                if (dataInisial)
                {
                    input.P_MIN = konfigApp.dataAwal;
                    input.P_MAX = konfigApp.dataAkhir;
                }
                else
                {
                    input.P_MIN = input.P_MAX + 1;
                    input.P_MAX = input.P_MAX + konfigApp.dataAkhir;
                }
                input.P_MINSpecified = true;
                input.P_MAXSpecified = true;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = "ID_SATKER = " + this.kode;

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

                data.AddRange(output.SF_MON_GUNA_SATKER_A25);
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
            //    detail(rowData.ID_SATKER + "", "");
            //}
            //catch (Exception)
            //{
            //}
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            back(null);
        }

        private void bandedGridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                var row = (SvcMonSatkerGunaA25.WASDALSROW_MON_GUNA_SATKER_A25)e.Row;

                //if (e.IsGetData)
                //{
                //    if (e.Column == colTGL_SENGKETA_TK_KSI)
                //    {
                //        if (!row.TGL_SENGKETA_TK_KSI.ToString().Contains("11/11/00"))
                //        {
                //            e.Value = row.TGL_SENGKETA_TK_KSI.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //        }
                //        else
                //        {
                //            e.Value = "-";
                //        }
                //    }

                //    if (e.Column == colTGL_SENGKETA_TK_PK)
                //    {
                //        if (!row.TGL_SENGKETA_TK_PK.ToString().Contains("11/11/00"))
                //        {
                //            e.Value = row.TGL_SENGKETA_TK_PK.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //        }
                //        else
                //        {
                //            e.Value = "-";
                //        }
                //    }

                //    if (e.Column == colTGL_SENGKETA_TK_TGI)
                //    {
                //        if (!row.TGL_SENGKETA_TK_TGI.ToString().Contains("11/11/00"))
                //        {
                //            e.Value = row.TGL_SENGKETA_TK_TGI.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //        }
                //        else
                //        {
                //            e.Value = "-";
                //        }

                //    }

                //    if (e.Column == colTGL_SENGKETA_TK1)
                //    {
                //        if (!row.TGL_SENGKETA_TK1.ToString().Contains("11/11/00"))
                //        {
                //            e.Value = row.TGL_SENGKETA_TK1.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //        }
                //        else
                //        {
                //            e.Value = "-";
                //        }
                //    }
                   
                //}
            }
            catch (Exception)
            {

            }
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
