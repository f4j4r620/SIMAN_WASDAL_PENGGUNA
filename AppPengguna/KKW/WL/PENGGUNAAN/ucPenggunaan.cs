using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;

namespace AppPengguna.KKW.WL.PENGGUNAAN
{

    public partial class ucPenggunaan : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcWasdalPSPAllSelect.execute_pttClient svcWasdalPSPAllSelect = null;
        SvcWasdalPSPAllSelect.OutputParameters outPSPAllSelect = null;
        SvcWasdalPSPAllSelect.WASDALSROW_MON_ALL_PSP dataTerpilih = null;



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
        FrmKoorKorwil frmSatker = null;

        public ucPenggunaan(FrmKoorKorwil _frmSatker, ucLapWasdal _ucASPBMN)
        {
            InitializeComponent();
            this.frmSatker = _frmSatker;
            PBMNS = _ucASPBMN;
        }

        #region LOAD DATA//================================

        public void getTindakLanjut()
        {
            frmSatker.Enabled = false;
            try
            {
                frmSatker.fToggleProgressBar("start");

                SvcWasdalPSPAllSelect.InputParameters parInp = new SvcWasdalPSPAllSelect.InputParameters();

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
                parInp.STR_WHERE = " ID_KORWIL = " + konfigApp.idKorwil + " AND (NO_SURAT <> '-')" + PBMNS.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcWasdalPSPAllSelect = new SvcWasdalPSPAllSelect.execute_pttClient();
                svcWasdalPSPAllSelect.Beginexecute(parInp, new AsyncCallback(getDataAngkutan), null);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataAngkutan(IAsyncResult result)
        {
            try
            {
                outPSPAllSelect = svcWasdalPSPAllSelect.Endexecute(result);
                svcWasdalPSPAllSelect.Close();
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new LoadDataAngkutan(this.loadDataAngkutan), outPSPAllSelect);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAngkutan(SvcWasdalPSPAllSelect.OutputParameters dataOut);

        private void loadDataAngkutan(SvcWasdalPSPAllSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_ALL_PSP.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_ALL_PSP[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmSatker.bbiMWasdalMore.Enabled = true;
                PBMNS.sbCari.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
                if (this.modeLoadData == "normal")
                {
                    this.masihAdaData = false;
                    frmSatker.bbiMWasdalMore.Enabled = false;
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
                if (dataOut.SF_MON_ALL_PSP[i].PERIODE == "h")
                {
                    dataOut.SF_MON_ALL_PSP[i].PERIODE = "Harian";
                }
                else if (dataOut.SF_MON_ALL_PSP[i].PERIODE == "m")
                {
                    dataOut.SF_MON_ALL_PSP[i].PERIODE = "Minggu";
                }
                else if (dataOut.SF_MON_ALL_PSP[i].PERIODE == "b")
                {
                    dataOut.SF_MON_ALL_PSP[i].PERIODE = "Bulanan";
                }
                else if (dataOut.SF_MON_ALL_PSP[i].PERIODE == "t")
                {
                    dataOut.SF_MON_ALL_PSP[i].PERIODE = "Tahunan";
                }
                else
                {
                    dataOut.SF_MON_ALL_PSP[i].PERIODE = null;
                }

                dataOut.SF_MON_ALL_PSP[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_MON_ALL_PSP[i].TGL_PEROLEHAN = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].TGL_PEROLEHAN);
                dataOut.SF_MON_ALL_PSP[i].SD_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].SD_TGL);
                dataOut.SF_MON_ALL_PSP[i].DARI_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].DARI_TGL);
                dataOut.SF_MON_ALL_PSP[i].TANGGAL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].TANGGAL);
                dsGrid.Add(dataOut.SF_MON_ALL_PSP[i]);
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
            gvAngkutan.BestFitColumns();
        }

        private void gvAngkutan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcWasdalPSPAllSelect.WASDALSROW_MON_ALL_PSP)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvAngkutan.GetFocusedDataSourceRowIndex();

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

        public void ShowGridPreview()
        {
            PrintableComponentLink componenlink = new PrintableComponentLink(new PrintingSystem());
            gcAngkutan.DataSource = dsGrid;
            componenlink.Component = gcAngkutan;
            componenlink.Landscape = true;
            componenlink.VerticalContentSplitting = VerticalContentSplitting.Exact;
            componenlink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            componenlink.Margins.Bottom = 5;
            componenlink.Margins.Top = 5;
            componenlink.Margins.Right = 5;
            componenlink.Margins.Left = 5;
            componenlink.CreateDocument();

            PrintTool pt = new PrintTool(componenlink.PrintingSystemBase);
            pt.ShowPreviewDialog();
            //composLink.CreateDocument();

        }

        



    }
}
