using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace AppPengguna.KSK.WL.PEMINDAHTANGAN.DETAILSATKER
{
    public partial class ucPenjualanBmn : ucMain
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm form;
        public bool dataInisial = true;
        public Detail detail;
        public Back back;
        ArrayList data;
        public bool moreButton = true;
        string kode = "";

        /// <summary>
        ///  Service
        /// </summary>
        SvcMonSatkerPindahA211.InputParameters input;
        SvcMonSatkerPindahA211.OutputParameters output;
        SvcMonSatkerPindahA211.execute_pttClient client;
        SvcMonSatkerPindahA211.WASDALSROW_MON_PINDAH_SATKER_A211 rowData;
        public ucPenjualanBmn()
        {
            InitializeComponent();
        }

        public ucPenjualanBmn(string label, string kode)
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
                input = new SvcMonSatkerPindahA211.InputParameters();
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
                input.STR_WHERE = "SK_KEPUTUSAN = '" + kode + "'";

                client = new SvcMonSatkerPindahA211.execute_pttClient();
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

        private delegate void ShowData(SvcMonSatkerPindahA211.OutputParameters output);

        private void ShowDataGrid(SvcMonSatkerPindahA211.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_PINDAH_SATKER_A211.Count();
                dataCount += jmlData;
                if (jmlData == konfigApp.dataMaks)
                {
                    moreButton = true;
                    setTombolMoreData(true);
                }
                else
                {
                    moreButton = false;
                    setTombolMoreData(false);
                }

                if (dataInisial)
                {
                    data = new ArrayList();
                }
               
                data.AddRange(output.SF_MON_PINDAH_SATKER_A211);
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
           
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            back();
        }

        private void bandedGridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                var row = (SvcMonSatkerPindahA211.WASDALSROW_MON_PINDAH_SATKER_A211)e.Row;
                if (e.IsGetData)
                {
                    string formatdate = "11/11/00";
                       
                    if (e.Column == colTGL_BAST)
                    {
                        if (!row.TGL_BAST.ToString().Contains(formatdate))
                        {
                            e.Value = row.TGL_BAST.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    if (e.Column == colTGL_RISALAH_LELANG)
                    {
                        if (!row.TGL_RISALAH_LELANG.ToString().Contains(formatdate))
                        {
                            e.Value = row.TGL_RISALAH_LELANG.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    if (e.Column == colTGL_SETOR)
                    {
                        if (!row.TGL_SETOR.ToString().Contains(formatdate))
                        {
                            e.Value = row.TGL_SETOR.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    if (e.Column == colTGL_SK)
                    {
                         if (!row.TGL_SK.ToString().Contains(formatdate))
                        {
                            e.Value = row.TGL_SK.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    if (e.Column == colTGL_SK_PENGHAPUSAN)
                    {
                        if (!row.TGL_SK_PENGHAPUSAN.ToString().Contains(formatdate))
                        {
                            e.Value = row.TGL_SK_PENGHAPUSAN.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                }


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
                this.bandedGridView1.ExportToXls(pathToSave);
                System.Diagnostics.Process.Start(pathToSave);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
