using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;

namespace AppPengguna.KSK.WL.PENGGUNAAN
{

    public partial class ucPenggunaan : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        //SvcLapGunaSk.execute_pttClient svcLapGunaSkSelect = null;
        //SvcLapGunaSk.OutputParameters outLapGunaSkSelect = null;
        //SvcLapGunaSk.WASDALSROW_MON_ALL_PSP dataTerpilih = null;
        SvcLapGunaSk.execute_pttClient svcLapGunaSkSelect = null;
        SvcLapGunaSk.OutputParameters outLapGunaSkSelect = null;
        SvcLapGunaSk.WASDALSROW_LAP_GUNA_SK dataTerpilih = null;

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
        FrmKoorSatker frmSatker = null;

        public ucPenggunaan(FrmKoorSatker _frmSatker, ucLapWasdal _ucASPBMN)
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

                SvcLapGunaSk.InputParameters parInp = new SvcLapGunaSk.InputParameters();


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
                parInp.STR_WHERE = "KD_SATKER LIKE '" + konfigApp.kodeSatker.Substring(0, 15) + "%' AND KD_PENERBIT_SK!='05' " + PBMNS.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NO_ASET";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcLapGunaSkSelect = new SvcLapGunaSk.execute_pttClient();
                svcLapGunaSkSelect.Beginexecute(parInp, new AsyncCallback(getDataAngkutan), null);
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
                outLapGunaSkSelect = svcLapGunaSkSelect.Endexecute(result);
                svcLapGunaSkSelect.Close();
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new LoadDataAngkutan(this.loadDataAngkutan), outLapGunaSkSelect);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAngkutan(SvcLapGunaSk.OutputParameters dataOut);

        private void loadDataAngkutan(SvcLapGunaSk.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_LAP_GUNA_SK.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_LAP_GUNA_SK[jmlData-1].TOTAL_DATA.ToString();
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
                if (dataOut.SF_LAP_GUNA_SK[i].PERIODE == "h")
                {
                    dataOut.SF_LAP_GUNA_SK[i].PERIODE = "Harian";
                }
                else if (dataOut.SF_LAP_GUNA_SK[i].PERIODE == "m")
                {
                    dataOut.SF_LAP_GUNA_SK[i].PERIODE = "Minggu";
                }
                else if (dataOut.SF_LAP_GUNA_SK[i].PERIODE == "b")
                {
                    dataOut.SF_LAP_GUNA_SK[i].PERIODE = "Bulanan";
                }
                else if (dataOut.SF_LAP_GUNA_SK[i].PERIODE == "t")
                {
                    dataOut.SF_LAP_GUNA_SK[i].PERIODE = "Tahunan";
                }
                else
                {
                    dataOut.SF_LAP_GUNA_SK[i].PERIODE = null;
                }

                dataOut.SF_LAP_GUNA_SK[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_SK[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_LAP_GUNA_SK[i].TGL_PERLH = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_SK[i].TGL_PERLH);
                dataOut.SF_LAP_GUNA_SK[i].SD_TGL = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_SK[i].SD_TGL);
                dataOut.SF_LAP_GUNA_SK[i].DARI_TGL = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_SK[i].DARI_TGL);
                dataOut.SF_LAP_GUNA_SK[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_SK[i].TGL_SK);
                dsGrid.Add(dataOut.SF_LAP_GUNA_SK[i]);
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
                    dataTerpilih = (SvcLapGunaSk.WASDALSROW_LAP_GUNA_SK)rowTerpilih.GetRow(e.FocusedRowHandle);
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

        private void gvAngkutan_DoubleClick(object sender, EventArgs e)
        {
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


    }
}
