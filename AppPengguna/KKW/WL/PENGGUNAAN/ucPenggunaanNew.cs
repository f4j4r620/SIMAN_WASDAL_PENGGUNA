using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using AppPengguna.KSK.WL.PENGGUNAAN;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.Data;

namespace AppPengguna.KKW.WL.PENGGUNAAN
{

    public partial class ucPenggunaanNew : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;
        SvcMonGunaKorEsKlA1.selectMonGunaKorEsKlA1_pttClient SvcMonKorwilGunaA1Select = null;
        SvcMonGunaKorEsKlA1.OutputParameters outLapGunaSkSelect = null;
        SvcMonGunaKorEsKlA1.WASDALSROW_MON_GUNA_KOR_ES_KL_A1 dataTerpilih = null;

        public int posisiRow = 0;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ArrayList dsGrid = null;
        public bool rowTerakhir = false;
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        public bool initModeLoad = true;
        public string strCari= "";
        public bool masihAdaData;
        public bool adaData;
        public string kdPelayanan = "";
        public decimal currentMin = konfigApp.currentMin;
        public decimal currentMaks = konfigApp.currentMaks;

        ucLapWasdal PBMNS = null;
        FrmKoorKorwil frmKorwil = null;

        public ucPenggunaanNew(FrmKoorKorwil _frmKorwil, ucLapWasdal _ucASPBMN)
        {
            InitializeComponent();
            this.frmKorwil = _frmKorwil;
            PBMNS = _ucASPBMN;
        }

        #region LOAD DATA//================================
        int max = 100;
        public void getTindakLanjut()
        {
            frmKorwil.Enabled = false;
            try
            {
                frmKorwil.fToggleProgressBar("start");

                SvcMonGunaKorEsKlA1.InputParameters parInp = new SvcMonGunaKorEsKlA1.InputParameters();


                if (dataInisial == true)
                {
                    this.currentMaks = konfigApp.dataAkhir;
                    this.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    this.currentMin = this.currentMaks + 1;
                    this.currentMaks = this.currentMaks + konfigApp.dataAkhir;
                }

                parInp.P_MAX = this.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = this.currentMin;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = "ID_KORWIL = "+konfigApp.idKorwil + this.strCari;
                parInp.P_COL = "";
                parInp.P_SORT = "";
                SvcMonKorwilGunaA1Select = new SvcMonGunaKorEsKlA1.selectMonGunaKorEsKlA1_pttClient();
                SvcMonKorwilGunaA1Select.Beginexecute(parInp, new AsyncCallback(getDataAngkutan), null);
            }
            catch
            {
                frmKorwil.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKorwil.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataAngkutan(IAsyncResult result)
        {
            try
            {
                outLapGunaSkSelect = SvcMonKorwilGunaA1Select.Endexecute(result);
                SvcMonKorwilGunaA1Select.Close();
                frmKorwil.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKorwil.aktifkanForm), "");
                this.Invoke(new LoadDataAngkutan(this.loadDataAngkutan), outLapGunaSkSelect);
            }
            catch
            {
                frmKorwil.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKorwil.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAngkutan(SvcMonGunaKorEsKlA1.OutputParameters dataOut);

        private void loadDataAngkutan(SvcMonGunaKorEsKlA1.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_GUNA_KOR_ES_KL_A1.Count();
            decimal jmlCurrent = 0;
            string totalData = ""; //(jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_GUNA_SATKER_A1[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmKorwil.bbiMWasdalMore.Enabled = true;
                PBMNS.sbCari.Enabled = true;
                jmlCurrent = max;
            }
            else
            {
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = max;
                else
                    jmlCurrent = jmlData;
                if (this.modeLoadData == "normal")
                {
                    this.masihAdaData = false;
                    frmKorwil.bbiMWasdalMore.Enabled = false;
                    PBMNS.sbCari.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                    PBMNS.sbCari.Enabled = false;
                }
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dsGrid.Add(dataOut.SF_MON_GUNA_KOR_ES_KL_A1[i]);
                this.adaData = true;
            }

            labelTotData.Text = "";
            labelTotData.Text = "Menampilkan " + jmlCurrent + " dari total " + totalData + " data";
            displayData();

        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcAngkutan.DataSource = null;
                gcAngkutan.DataSource = dsGrid;
            }
            else
            {
                gcAngkutan.RefreshDataSource();
            }
            bandedGridView1.BestFitColumns();
        }

        private void gvAngkutan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonGunaKorEsKlA1.WASDALSROW_MON_GUNA_KOR_ES_KL_A1)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = bandedGridView1.GetFocusedDataSourceRowIndex();

                    if (rowTerpilih.IsLastRow)
                    {
                        rowTerakhir = true;
                    }
                    else
                    {
                        rowTerakhir = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void gvAngkutan_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                dataTerpilih = new SvcMonGunaKorEsKlA1.WASDALSROW_MON_GUNA_KOR_ES_KL_A1();
                dataTerpilih = (SvcMonGunaKorEsKlA1.WASDALSROW_MON_GUNA_KOR_ES_KL_A1)bandedGridView1.GetFocusedRow();
                string label = "SATKER : " + dataTerpilih.KD_SATKER + " - " + dataTerpilih.UR_SATKER;
                FormDtlSatker form = new FormDtlSatker(label, dataTerpilih.ID_SATKER.ToString());
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                dataTerpilih = null;
            }
        }

        public void ShowGridPreview()
        {
            PrintableComponentLink componenlink = new PrintableComponentLink(new PrintingSystem());
            gcAngkutan.DataSource = dsGrid;
            componenlink.Component = gcAngkutan;
            componenlink.Landscape = true;
            componenlink.VerticalContentSplitting = VerticalContentSplitting.Smart;
            componenlink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            componenlink.Margins.Bottom = 3;
            componenlink.Margins.Top = 3;
            componenlink.Margins.Right = 3;
            componenlink.Margins.Left = 3;

            componenlink.CreateDocument();

            PrintTool pt = new PrintTool(componenlink.PrintingSystemBase);
            pt.ShowPreviewDialog();


        }

        double sum_kuantitas = 0;
        double sum_luas = 0;
        double sum_nilai_perolehan = 0;
        
        double psp_sum_kuantitas = 0;
        double psp_sum_luas = 0;
        double psp_sum_nilai_perolehan = 0;
        
        double belum_psp_sum_kuantitas = 0;
        double belum_psp_sum_luas = 0;
        double belum_psp_sum_nilai_perolehan = 0;
        
        double persentase_kuantitas_psp = 0;
        double persentase_luas_psp = 0;
        double persentase_nilai_perolehan_psp = 0;
        double persentase_kuantitas_belum_psp = 0;
        double persentase_luas_belum_psp = 0;
        double persentase_nilai_perolehan_belum_psp = 0;

        double total_value = 0;

        private void bandedGridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if ((e.Item as GridSummaryItem).Tag != null)
                {
                    GridView view = sender as GridView;
                    string TagID = (e.Item as GridSummaryItem).Tag.ToString();

                    if (e.SummaryProcess == CustomSummaryProcess.Start)
                    {
                        total_value = 0;
                    }

                    if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                    {

                        switch (TagID)
                        {
                            case "pKuantitasPspY":
                                total_value = (Convert.ToDouble(kuantitasPspY.Summary["sKuantitasPspY"].SummaryValue) / Convert.ToDouble(kuantitasTot.Summary["sKuantitasTot"].SummaryValue)) * 100;
                                break;
                            case "pLuasPspY":
                                total_value = (Convert.ToDouble(luasPspY.Summary["sLuasPspY"].SummaryValue) / Convert.ToDouble(luasTot.Summary["sLuasTot"].SummaryValue)) * 100;
                                break;
                            case "pNilPerlhPspY":
                                total_value = (Convert.ToDouble(nilPerlhPspY.Summary["sNilPerlhPspY"].SummaryValue) / Convert.ToDouble(nilPerlhTot.Summary["sNilPerlhTot"].SummaryValue)) * 100;
                                break;
                            case "pKuantitasPspN":
                                total_value = (Convert.ToDouble(kuantitasPspN.Summary["sKuantitasPspN"].SummaryValue) / Convert.ToDouble(kuantitasTot.Summary["sKuantitasTot"].SummaryValue)) * 100;
                                break;
                            case "pLuasPspN":
                                total_value = (Convert.ToDouble(luasPspN.Summary["sLuasPspN"].SummaryValue) / Convert.ToDouble(luasTot.Summary["sLuasTot"].SummaryValue)) * 100;
                                break;
                            case "pNilPerlhPspN":
                                total_value = (Convert.ToDouble(nilPerlhPspN.Summary["sNilPerlhPspN"].SummaryValue) / Convert.ToDouble(nilPerlhTot.Summary["sNilPerlhTot"].SummaryValue)) * 100;
                                break;

                        }
                    }

                    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        e.TotalValue = String.Format("{0:0.##} %", total_value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


    }
}
