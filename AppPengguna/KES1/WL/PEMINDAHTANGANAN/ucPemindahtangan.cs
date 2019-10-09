using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KES1.WL.PEMINDAHTANGANAN
{

    public partial class ucPemindahtangan : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalPTMonBMNAll.execute_pttClient svcMonWasdalAllSelect = null;
        SvcWasdalPTMonBMNAll.OutputParameters outMonAllSelect = null;
        SvcWasdalPTMonBMNAll.WASDALSROW_MON_ALL_PT dataTerpilih = null;


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
        FrmKoorEselon1 frmSatker = null;

        public ucPemindahtangan(FrmKoorEselon1 _frmSatker, ucLapWasdal _ucASPBMN)
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

                SvcWasdalPTMonBMNAll.InputParameters parInp = new SvcWasdalPTMonBMNAll.InputParameters();

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
                parInp.STR_WHERE = "ID_ESELON1 = " + konfigApp.idEselon1 + " AND (NO_SURAT <> '-')" + PBMNS.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcMonWasdalAllSelect = new SvcWasdalPTMonBMNAll.execute_pttClient();
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

        private delegate void LoadDataAngkutan(SvcWasdalPTMonBMNAll.OutputParameters dataOut);

        private void loadDataAngkutan(SvcWasdalPTMonBMNAll.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_ALL_PT.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_ALL_PT[jmlData - 1].TOTAL_DATA.ToString();
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

                switch (dataOut.SF_MON_ALL_PT[i].PERIODE)
                {
                    case "H":
                        dataOut.SF_MON_ALL_PT[i].PERIODE = "Harian";
                        break;
                    case "M":
                        dataOut.SF_MON_ALL_PT[i].PERIODE = "Mingguan";
                        break;
                    case "B":
                        dataOut.SF_MON_ALL_PT[i].PERIODE = "Bulanan";
                        break;
                    case "T":
                        dataOut.SF_MON_ALL_PT[i].PERIODE = "Tahunan";
                        break;
                }
                dataOut.SF_MON_ALL_PT[i].TANGGAL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TANGGAL); 
                dataOut.SF_MON_ALL_PT[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_MON_ALL_PT[i].TGL_PEROLEHAN = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_PEROLEHAN);
                dataOut.SF_MON_ALL_PT[i].TGL_SETOR = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_SETOR);
                dataOut.SF_MON_ALL_PT[i].TGL_TRANSAKSI = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_TRANSAKSI);
                dataOut.SF_MON_ALL_PT[i].DARI_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].DARI_TGL);
                dataOut.SF_MON_ALL_PT[i].SD_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].SD_TGL);
                dsGrid.Add(dataOut.SF_MON_ALL_PT[i]);
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
        }

        private void gvAngkutan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcWasdalPTMonBMNAll.WASDALSROW_MON_ALL_PT)rowTerpilih.GetRow(e.FocusedRowHandle);
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
