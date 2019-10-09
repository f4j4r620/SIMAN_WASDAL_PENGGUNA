using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KES1.WL.PENGGUNAAN.DETAILSATKER
{
    public partial class ucPihakLain : ucMain
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm form;
        public bool dataInisial = true;
        ArrayList data;
        public bool moreButton = true;
        string kode = "";
        public PihakLainDetail gotoOperasiPihakLain;
        public PihakLainDetail gotoPenggunaanSementara;

        /// <summary>
        ///  Service
        /// </summary>
        SvcMonSatkerGunaA24.InputParameters input;
        SvcMonSatkerGunaA24.OutputParameters output;
        SvcMonSatkerGunaA24.execute_pttClient client;
        SvcMonSatkerGunaA24.WASDALSROW_MON_GUNA_SATKER_A24 rowData;

        public ucPihakLain()
        {
            InitializeComponent();
        }

        public ucPihakLain(string label, string kode)
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
                input = new SvcMonSatkerGunaA24.InputParameters();
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

                client = new SvcMonSatkerGunaA24.execute_pttClient();
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

        private delegate void ShowData(SvcMonSatkerGunaA24.OutputParameters output);

        private void ShowDataGrid(SvcMonSatkerGunaA24.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_GUNA_SATKER_A24.Count();
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
                data.AddRange(output.SF_MON_GUNA_SATKER_A24);
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

        private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                rowData = new SvcMonSatkerGunaA24.WASDALSROW_MON_GUNA_SATKER_A24();
                rowData = (SvcMonSatkerGunaA24.WASDALSROW_MON_GUNA_SATKER_A24)bandedGridView1.GetFocusedRow();
                string bentuk = "BENTUK\t :";
                string surat_persetujuan = "SURAT PERSETUJUAN : " + rowData.SK_KEPUTUSAN;
                
                if (rowData.KD_PELAYANAN == "03")
                {
                    gotoOperasiPihakLain(rowData.SK_KEPUTUSAN + "", bentuk + " DIPERGUNAKAN ORANG LAIN \n" + surat_persetujuan);
                }
                else
                {
                    gotoPenggunaanSementara(rowData.SK_KEPUTUSAN + "", bentuk + " PENGGUNAAN SEMENTARA \n" + surat_persetujuan);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Silahkan Pilih Data Terlebih dahulu");
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
