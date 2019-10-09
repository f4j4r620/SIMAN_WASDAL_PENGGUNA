using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KSK.WL.PEMINDAHTANGANAN
{

    public partial class ucPemindahtanganRpmk : DevExpress.XtraEditors.XtraUserControl
    {

        /* Update    : 24 Maret  2017
         * Task      : 1015
         * Deskripsi : Hide label untuk menampilkan jumlah total data
         * Kegiatan  : - mengubah visibility label menjadi false (nama control yang di hide : emptySpaceItem1, layoutControlItem2)
         *             - menon-aktifkan pemanggilan service untuk mendapatkan total data dengan mengubah nya menjadi comment
         */

        public CariDataOnline cariDataOnline;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalLapPindahtgnPmkSkNew.execute_pttClient svcMonWasdalAllSelect = null;
        SvcWasdalLapPindahtgnPmkSkNew.OutputParameters outMonAllSelect = null;
        SvcWasdalLapPindahtgnPmkSkNew.WASDALSROW_PINDAHTGN_PMK_SK dataTerpilih = null;

        svcCountLapPindahTangan.execute_pttClient CountExecute = null;
        svcCountLapPindahTangan.InputParameters CountInput = null;
        svcCountLapPindahTangan.OutputParameters CountOutput = null;
        decimal? totalData = 0;

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

        public ucPemindahtanganRpmk(FrmKoorSatker _frmSatker, ucLapWasdal _ucASPBMN)
        {
            InitializeComponent();
            this.frmSatker = _frmSatker;
            PBMNS = _ucASPBMN;
        }

        #region Thread
        private void toggleProgBarPu(string kondisi)
        {
            if (kondisi == "start") this.aktifkanProgresBar();
            else this.nonAktifkanprogressBar();
        }

        private void aktifkanProgresBar()
        {
            toggleProgressBar("start");
        }

        private void nonAktifkanprogressBar()
        {
            toggleProgressBar("finish");
        }

        public delegate void AktifkanForm(string str);

        public void aktifkanForm(string str)
        {
            this.Enabled = true;
        }
        #endregion

        #region LOAD DATA//================================

        public void getTindakLanjut()
        {
            frmSatker.Enabled = false;
            try
            {
                frmSatker.fToggleProgressBar("start");

                SvcWasdalLapPindahtgnPmkSkNew.InputParameters parInp = new SvcWasdalLapPindahtgnPmkSkNew.InputParameters();

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
                //parInp.STR_WHERE = "KD_SATKER LIKE '" + konfigApp.kodeSatker.Substring(0, 15) + "%' " + PBMNS.strKdPelayanan + this.strCari;
                parInp.STR_WHERE = "";
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcMonWasdalAllSelect = new SvcWasdalLapPindahtgnPmkSkNew.execute_pttClient();
                svcMonWasdalAllSelect.Beginexecute(parInp, new AsyncCallback(getDataAngkutan), null);
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
                outMonAllSelect = svcMonWasdalAllSelect.Endexecute(result);
                svcMonWasdalAllSelect.Close();
                //if (!dataInisial)
                //{
                //    frmSatker.fToggleProgressBar("finish");
                //    this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                //}
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new LoadDataAngkutan(this.loadDataAngkutan), outMonAllSelect);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAngkutan(SvcWasdalLapPindahtgnPmkSkNew.OutputParameters dataOut);

        private void loadDataAngkutan(SvcWasdalLapPindahtgnPmkSkNew.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW.Count();
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

                switch (dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].PERIODE)
                {
                    case "H":
                        dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].PERIODE = "Harian";
                        break;
                    case "M":
                        dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].PERIODE = "Mingguan";
                        break;
                    case "B":
                        dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].PERIODE = "Bulanan";
                        break;
                    case "T":
                        dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].PERIODE = "Tahunan";
                        break;
                }
                dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].TGL_SK);
                dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].TGL_BUKTI_LAKSANA);
                
                dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].TGL_SETOR = konfigApp.setDefaultDate(dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].TGL_SETOR);
                dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].TGL_TRANSAKSI = konfigApp.setDefaultDate(dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].TGL_TRANSAKSI);
                dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].DARI_TGL = konfigApp.setDefaultDate(dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].DARI_TGL);
                dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].SD_TGL = konfigApp.setDefaultDate(dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i].SD_TGL);
                dsGrid.Add(dataOut.SF_LAP_PINDAHTGN_PMK_SKNEW[i]);
                this.adaData = true;
            }
            displayData();
            //if (dataInisial)
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
        private void getTotalData()
        {
            //frmSatker.Enabled = false;
            try
            {
                //frmSatker.fToggleProgressBar("start");

                CountInput = new svcCountLapPindahTangan.InputParameters();
                CountInput.STR_WHERE = "ID_SATKER= " + konfigApp.idSatker + " OR ID_SATKER_PARENT= " + konfigApp.idSatker;
                CountExecute = new svcCountLapPindahTangan.execute_pttClient();
                CountOutput =  CountExecute.execute(CountInput);
                totalData = CountOutput.SF_LAP_PINDAHTGN_PMK_SK_COUNT[0].TOTAL_DATA;
                labelTotData.Text = "";
                labelTotData.Text = "Total " + totalData + " data";
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
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

        //private delegate void LoadDataTotal(svcCountLapPindahTangan.OutputParameters dataOut);

        //private void loadDataTotal(svcCountLapPindahTangan.OutputParameters dataOut)
        //{
        //    totalData = dataOut.SF_LAP_PINDAHTGN_PMK_SK_COUNT[0].TOTAL_DATA;
        //    labelTotData.Text = "Menampilkan " + dsGrid.Count + " dari total " + totalData + " data";
        //}

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
                    dataTerpilih = (SvcWasdalLapPindahtgnPmkSkNew.WASDALSROW_PINDAHTGN_PMK_SK)rowTerpilih.GetRow(e.FocusedRowHandle);
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

    }
}
