using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;

namespace AppPengguna.KSK.WL.PENGGUNAAN
{

    public partial class ucPenggunaanRpmk : DevExpress.XtraEditors.XtraUserControl
    {
        /* Update    : 24 Maret  2017
         * Task      : 1015
         * Deskripsi : Hide label untuk menampilkan jumlah total data
         * Kegiatan  : - mengubah visibility label menjadi false (nama control yang di hide : emptySpaceItem1, layoutControlItem2)
         *             - menon-aktifkan pemanggilan service untuk mendapatkan total data dengan mengubah nya menjadi comment
         *
         * Update   : 17 Mei 2015 
         * Task     : 1308
         * Kegiatan : Update Service untuk load data
         * 
         */

        public CariDataOnline cariDataOnline;

        /// <summary>
        /// Service untuk Mengambil Data
        /// </summary>
        SvcWasdalLapGunaPmkNew.execute_pttClient svcWasdalLapGunaPmk = null;
        SvcWasdalLapGunaPmkNew.OutputParameters outLapGunaPmk = null;
        SvcWasdalLapGunaPmkNew.WASDALSROW_LAP_GUNA_PMK dataTerpilih = null;


        svcCountLapGuna.execute_pttClient CountExecute = null;
        svcCountLapGuna.InputParameters CountInput = null;
        svcCountLapGuna.OutputParameters CountOutput = null;

        public int posisiRow = 0;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ArrayList dsGrid = null;
        public bool rowTerakhir = false;
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        public bool initModeLoad = true;
        public string strCari = "";
        public bool masihAdaData;
        public bool adaData;
        public string kdPelayanan = "";
        public decimal currentMin = konfigApp.currentMin;
        public decimal currentMaks = konfigApp.currentMaks;
        decimal? totalData = 0;

        ucLapWasdal PBMNS = null;
        FrmKoorSatker frmSatker = null;

        public ucPenggunaanRpmk(FrmKoorSatker _frmSatker, ucLapWasdal _ucASPBMN)
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

                SvcWasdalLapGunaPmkNew.InputParameters parInp = new SvcWasdalLapGunaPmkNew.InputParameters();

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
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_MAX = this.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = this.currentMin;
                parInp.P_MINSpecified = true;
                //parInp.STR_WHERE = "KD_SATKER LIKE '" + konfigApp.kodeSatker.Substring(0, 15) + "%' " + PBMNS.strKdPelayanan + this.strCari;
                parInp.STR_WHERE = "";
                parInp.P_COL = "KD_BRG";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcWasdalLapGunaPmk = new SvcWasdalLapGunaPmkNew.execute_pttClient();
                svcWasdalLapGunaPmk.Beginexecute(parInp, new AsyncCallback(getDataAngkutan), null);
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
                outLapGunaPmk = svcWasdalLapGunaPmk.Endexecute(result);
                svcWasdalLapGunaPmk.Close();
                //if (!dataInisial)
                //{
                //    frmSatker.fToggleProgressBar("finish");
                //    this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                //}
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new LoadDataAngkutan(this.loadDataAngkutan), outLapGunaPmk);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAngkutan(SvcWasdalLapGunaPmkNew.OutputParameters dataOut);

        private void loadDataAngkutan(SvcWasdalLapGunaPmkNew.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_LAP_GUNA_PMK_NEW.Count();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmSatker.bbiMWasdalMore.Enabled = true;
                PBMNS.sbCari.Enabled = true;
           }
            else
            {
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
                if (dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE == "h")
                {
                    dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE = "Harian";
                }
                else if (dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE == "m")
                {
                    dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE = "Minggu";
                }
                else if (dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE == "b")
                {
                    dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE = "Bulanan";
                }
                else if (dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE == "t")
                {
                    dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE = "Tahunan";
                }
                else
                {
                    dataOut.SF_LAP_GUNA_PMK_NEW[i].PERIODE = null;
                }

                dataOut.SF_LAP_GUNA_PMK_NEW[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_PMK_NEW[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_LAP_GUNA_PMK_NEW[i].TGL_PERLH = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_PMK_NEW[i].TGL_PERLH);
                dataOut.SF_LAP_GUNA_PMK_NEW[i].SD_TGL = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_PMK_NEW[i].SD_TGL);
                dataOut.SF_LAP_GUNA_PMK_NEW[i].DARI_TGL = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_PMK_NEW[i].DARI_TGL);
                dataOut.SF_LAP_GUNA_PMK_NEW[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_LAP_GUNA_PMK_NEW[i].TGL_SK);
                dsGrid.Add(dataOut.SF_LAP_GUNA_PMK_NEW[i]);
                this.adaData = true;
                
            }

            displayData();
            //if (dataInisial == true)
            //{
            //    getTotalData();
            //}
            //else
            //{
            //    labelTotData.Text = "";
            //    labelTotData.Text = "Total " + totalData + " data";
            //}
        }

        #endregion

        #region LOAD TOTAL DATA
        public void getTotalData()
        {
            //frmSatker.Enabled = false;
            try
            {
                //frmSatker.fToggleProgressBar("start");

                CountInput = new svcCountLapGuna.InputParameters();
                CountInput.STR_WHERE = "ID_SATKER= " + konfigApp.idSatker + " OR ID_SATKER_PARENT= " + konfigApp.idSatker;
                CountExecute = new svcCountLapGuna.execute_pttClient();

                CountOutput = CountExecute.execute(CountInput);
                totalData = CountOutput.SF_LAP_GUNA_PMK_COUNT[0].TOTAL_DATA;
                labelTotData.Text = "";
                labelTotData.Text = "Total " + totalData + " data";
            }
            catch
            {
                //frmSatker.fToggleProgressBar("finish");
                //this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        //private void getTotalDataOutput(IAsyncResult result)
        //{
        //    try
        //    {
        //        CountOutput = CountExecute.Endexecute(result);
        //        CountExecute.Close();
        //        frmSatker.fToggleProgressBar("finish");
        //        this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
        //        this.Invoke(new LoadDataTotal(this.loadDataTotal), CountOutput);

        //    }
        //    catch (Exception ex)
        //    {
        //        frmSatker.fToggleProgressBar("finish");
        //        this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
        //        MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
        //    }
        //}

        //private delegate void LoadDataTotal(svcCountLapGuna.OutputParameters dataOut);

        //private void loadDataTotal(svcCountLapGuna.OutputParameters dataOut)
        //{
        //    //totalData = dataOut.SF_LAP_GUNA_PMK_COUNT[0].TOTAL_DATA;
        //    //labelTotData.Text = "Menampilkan " + dsGrid.Count + " dari total " + totalData + " data";
        //}

        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                //gcAngkutan.DataSource = null;
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
                    dataTerpilih = (SvcWasdalLapGunaPmkNew.WASDALSROW_LAP_GUNA_PMK)rowTerpilih.GetRow(e.FocusedRowHandle);
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
