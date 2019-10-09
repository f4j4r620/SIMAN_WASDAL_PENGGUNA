using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using AppPengguna.KKL.WL.PENGGUNAAN;
using DevExpress.XtraGrid;
using DevExpress.Data;
using System.IO;

namespace AppPengguna.KKL.WL.PENGGUNAAN
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
        FrmKoorKL frmKorwil = null;

        public ucPenggunaanNew(FrmKoorKL _frmKorwil, ucLapWasdal _ucASPBMN)
        {
            InitializeComponent();
            this.frmKorwil = _frmKorwil;
            PBMNS = _ucASPBMN;
        }

        #region LOAD DATA//================================
        
        public void getTindakLanjut()
        {
            frmKorwil.Enabled = false;
            try
            {
                frmKorwil.fToggleProgressBar("start");

                SvcMonGunaKorEsKlA1.InputParameters parInp = new SvcMonGunaKorEsKlA1.InputParameters();


                if (dataInisial == true)
                {
                    this.currentMaks = konfigApp.dataMaks;
                    this.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    this.currentMin = this.currentMaks + 1;
                    this.currentMaks = this.currentMaks + konfigApp.dataMaks;
                }

                parInp.P_MAX =0;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = "ID_KL = "+konfigApp.idKl + this.strCari;
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
            if (jmlData == konfigApp.dataMaks)
            {
                this.masihAdaData = true;
                frmKorwil.bbiMWasdalMore.Enabled = true;
                PBMNS.sbCari.Enabled = true;
                jmlCurrent = konfigApp.dataMaks;
            }
            else
            {
                if (jmlData > konfigApp.dataMaks)
                    jmlCurrent = konfigApp.dataMaks;
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
        public void ExportToExcel()
        {

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    switch (fileExtenstion)
                    {
                        case ".xls":
                            gcAngkutan.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            gcAngkutan.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            gcAngkutan.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            gcAngkutan.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            gcAngkutan.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            gcAngkutan.ExportToMht(exportFilePath);
                            break;
                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        
        }

    }
}
