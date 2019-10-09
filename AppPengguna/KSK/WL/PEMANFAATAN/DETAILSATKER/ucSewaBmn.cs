using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace AppPengguna.KSK.WL.PEMANFAATAN.DETAILSATKER
{
    public partial class ucSewaBmn : ucMain
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
        SvcMonSatkerManfaatA211.InputParameters input;
        SvcMonSatkerManfaatA211.OutputParameters output;
        SvcMonSatkerManfaatA211.execute_pttClient client;
        SvcMonSatkerManfaatA211.WASDALSROW_MON_MANFAAT_SATKER_A211 rowData;

        public ucSewaBmn()
        {
            InitializeComponent();
        }

        public ucSewaBmn(string label, string kode)
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
                input = new SvcMonSatkerManfaatA211.InputParameters();
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
                //input.STR_WHERE = "ID_SATKER = " + kode;
                input.STR_WHERE = "SK_KEPUTUSAN = '" + kode + "'";

                client = new SvcMonSatkerManfaatA211.execute_pttClient();
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

        private delegate void ShowData(SvcMonSatkerManfaatA211.OutputParameters output);

        private void ShowDataGrid(SvcMonSatkerManfaatA211.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_MANFAAT_SATKER_A211.Count();
                if (jmlData == konfigApp.dataAkhir)
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
                data.AddRange(output.SF_MON_MANFAAT_SATKER_A211);
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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            backToView();
        }

        private void bandedGridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                var row = (SvcMonSatkerManfaatA211.WASDALSROW_MON_MANFAAT_SATKER_A211)e.Row;
                if (e.IsGetData)
                {
                    string formatDate = "11/11/00";
                    if (e.Column == colTGL_BAST)
                    {

                        if (!row.TGL_BAST.ToString().Contains(formatDate))
                        {
                            e.Value = row.TGL_BAST.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    if (e.Column == colTGL_BUKTI_LAKSANA)
                    {

                        if (!row.TGL_BUKTI_LAKSANA.ToString().Contains(formatDate))
                        {
                            e.Value = row.TGL_BUKTI_LAKSANA.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    if (e.Column == colTGL_LAP_AKHIR)
                    {

                        if (!row.TGL_LAP_AKHIR.ToString().Contains(formatDate))
                        {
                            e.Value = row.TGL_LAP_AKHIR.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }

                    if (e.Column == colTGL_REKAM_PERJANJIAN)
                    {

                        if (!row.TGL_REKAM_PERJANJIAN.ToString().Contains(formatDate))
                        {
                            e.Value = row.TGL_REKAM_PERJANJIAN.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }

                    if (e.Column == colTGL_SK)
                    {

                        if (!row.TGL_SK.ToString().Contains(formatDate))
                        {
                            e.Value = row.TGL_SK.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        
                    }

                    if (e.Column == colTGL_SK2)
                    {
                        if (!row.TGL_SK.ToString().Contains(formatDate))
                        {
                            e.Value = row.TGL_SK.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
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
