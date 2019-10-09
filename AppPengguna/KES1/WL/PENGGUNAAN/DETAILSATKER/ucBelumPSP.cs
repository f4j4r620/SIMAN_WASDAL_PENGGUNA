using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using AppPengguna.KES1.WL.PENGGUNAAN;
using AppPengguna.KSK.WL.PENGGUNAAN.DETAILSATKER;

namespace AppPengguna.KES1.WL.PENGGUNAAN.DETAILSATKER
{
    public partial class ucBelumPSP : ucMain
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm form;
        public BelumPspDetail detailSTBNonDokPengguna;
        public BelumPspDetail detailSTBNonDokPengelola;
        public BelumPspDetail detailSTBDokPengguna;
        public BelumPspDetail detailSTBDokPengelola;
        public BelumPspDetail detailTanahPengguna;
        public BelumPspDetail detailTanahPengelola;
        public BelumPspDetail detailBangunanPengguna;
        public BelumPspDetail detailBangunanPengelola;

        public bool dataInisial = true;
        ArrayList data;
        public bool moreButton = true;
        string kode = "";

        /// <summary>
        ///  Service
        /// </summary>
        /// 
        SvcMonSatkerGunaA22.InputParameters input;
        SvcMonSatkerGunaA22.OutputParameters output;
        SvcMonSatkerGunaA22.execute_pttClient client;
        BelumPSPItem rowData;

        public ucBelumPSP()
        {
            InitializeComponent();
        }

        public ucBelumPSP(string label, string kode)
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
        private delegate void ShowData(SvcMonSatkerGunaA22.OutputParameters output);
        
        private void GetData(string where)
        {
            try
            {
                FormNonAktif();
                input = new SvcMonSatkerGunaA22.InputParameters();
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

                client = new SvcMonSatkerGunaA22.execute_pttClient();
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
        private void ShowDataGrid(SvcMonSatkerGunaA22.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_GUNA_SATKER_A22.Count();
                if (jmlData == konfigApp.dataMaks)
                {
                    //moreButton = true;
                    //setTombolMoreData(true);
                    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    dataCount += jmlData;
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


                BelumPSPItem[] dataTemp = BelumPSPItem.GetPSP();

                foreach (var item in output.SF_MON_GUNA_SATKER_A22)
                {
                    foreach (var item2 in dataTemp)
                    {
                        if (item.KD_JNS_BMN == item2.KD_JENIS_BMN)
                        {
                            item2.ID_SATKER = item.ID_SATKER;
                            item2.KD_SATKER = item.KD_SATKER;
                            item2.PENGELOLA_KUANTITAS_PSP_N = item.PENGELOLA_KUANTITAS_PSP_N;
                            item2.PENGELOLA_LUAS_PSP_N = item.PENGELOLA_LUAS_PSP_N;
                            item2.PENGELOLA_NIL_PERLH_PSP_N = item.PENGELOLA_NIL_PERLH_PSP_N;
                            item2.PENGGUNA_KUANTITAS_PSP_N = item.PENGGUNA_KUANTITAS_PSP_N;
                            item2.PENGGUNA_LUAS_PSP_N = item.PENGGUNA_LUAS_PSP_N;
                            item2.PENGGUNA_NIL_PERLH_PSP_N = item.PENGGUNA_NIL_PERLH_PSP_N;
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
        private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                rowData = new BelumPSPItem();
                rowData = (BelumPSPItem)bandedGridView1.GetFocusedRow();
                if (rowData.KD_JENIS_BMN == 1)
                {
                    // untuk WHERE diisi ID_SATKER = kode pada saat di ucBelumPSPTanah
                    //detailSTBNonDokPengelola(rowData.KD_JENIS_BMN + "", rowData.NM_JENIS_BMN);
                    showKewenanganBarang();
                    if (isPengguna) detailTanahPengguna(kode, rowData.NM_JENIS_BMN);
                    else detailTanahPengelola(kode, rowData.NM_JENIS_BMN);
                }
                else if (rowData.KD_JENIS_BMN == 2)
                {
                    // untuk WHERE diisi ID_SATKER = kode pada saat di ucBelumPSPBangunan
                    //detailSTBDokPengelola(rowData.KD_JENIS_BMN + "", rowData.NM_JENIS_BMN);
                    showKewenanganBarang();
                    if (isPengguna) detailBangunanPengguna(kode, rowData.NM_JENIS_BMN);
                    else detailBangunanPengelola(kode, rowData.NM_JENIS_BMN);
                }
                else if (rowData.KD_JENIS_BMN == 42)
                {
                    // untuk WHERE diisi ID_SATKER = kode pada saat di ucBelumPSPPengelola 
                    //detailSTBNonDokPengelola(rowData.KD_JENIS_BMN + "", rowData.NM_JENIS_BMN);
                    showKewenanganBarang();
                    if (isPengguna) detailSTBNonDokPengguna(kode, rowData.NM_JENIS_BMN);
                    else detailSTBNonDokPengelola(kode, rowData.NM_JENIS_BMN);
                }
                else if (rowData.KD_JENIS_BMN == 41)
                {
                    // untuk WHERE diisi ID_SATKER = kode pada saat di ucBelumPSPPengguna
                    //detailSTBDokPengelola(rowData.KD_JENIS_BMN + "", rowData.NM_JENIS_BMN);
                    showKewenanganBarang();
                    if (isPengguna) detailSTBDokPengguna(kode, rowData.NM_JENIS_BMN);
                    else detailSTBDokPengelola(kode, rowData.NM_JENIS_BMN);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
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


        bool isPengguna = true;
        public void setKewenanganBarang(bool status)
        {
            isPengguna = status;
            frm.Dispose();
        }

        FrmPuJenisUser frm = null;
        private void showKewenanganBarang()
        {
            frm = new FrmPuJenisUser();
            frm.milik = new KewenanganBarang(setKewenanganBarang);
            frm.ShowDialog();
        }
    }
}
