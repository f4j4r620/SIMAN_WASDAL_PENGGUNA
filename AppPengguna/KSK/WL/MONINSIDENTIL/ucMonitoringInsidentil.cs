using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KSK.WL.MONINSIDENTIL
{
    public partial class ucMonitoringInsidentil : UserControl
    {
       
       // Delegate Method
        public ToggleProgressBar toggleProgressBar;
    
        // Service Method
        SvcMonSatkerInsidentilA1.execute_pttClient client;
        SvcMonSatkerInsidentilA1.InputParameters input;
        SvcMonSatkerInsidentilA1.OutputParameters output;
        public SvcMonSatkerInsidentilA1.WASDALSROW_MON_SATKER_INSIDENTIL rowData;

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

        ucLapWasdal PBMNS = null;
        FrmKoorSatker frmSatker = null;

        private delegate void ShowData(SvcMonSatkerInsidentilA1.OutputParameters output);

        // Control Data
        ArrayList data;

        public ucMonitoringInsidentil(FrmKoorSatker _frmSatker, ucLapWasdal _ucASPBMN)
        {
            InitializeComponent();
            this.frmSatker = _frmSatker;
            PBMNS = _ucASPBMN;
        }

        public ucMonitoringInsidentil()
        {
            InitializeComponent();
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
            //FormAktif();
        }
        #endregion
  
        int maxData = 20;
        public void LoadData(bool state)
        {
            dataInisial = state;
            getDataMonitoring();
        }

        public void getDataMonitoring()
        {
            try
            {
                //FormNonAktif();
                input = new SvcMonSatkerInsidentilA1.InputParameters();

                if (dataInisial)
                {
                    maxData = 0;
                    input.P_MIN = konfigApp.dataAwal;
                    input.P_MAX = konfigApp.dataAkhir;
                }
                else
                {
                    input.P_MIN = maxData + 1;
                    input.P_MAX = maxData + konfigApp.dataAkhir;
                }
                
                input.P_MAXSpecified = true;
                input.P_MINSpecified = true;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = "ID_SATKER = " + konfigApp.idSatker + this.strCari;
                
                client = new SvcMonSatkerInsidentilA1.execute_pttClient();
                client.Open();
                client.Beginexecute(input, new AsyncCallback(GetResult), null);


            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void GetResult(IAsyncResult result)
        {
            try
            {
                output = client.Endexecute(result);
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new ShowData(this.ShowDataGrid), output);
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void ShowDataGrid(SvcMonSatkerInsidentilA1.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_SATKER_INSIDENTIL.Count();
                if (jmlData == konfigApp.dataAkhir)
                {
                    this.masihAdaData = true;
                    frmSatker.bbiMWasdalMore.Enabled = true;
                    PBMNS.sbCari.Enabled = true;
                    maxData += jmlData;
                }
                else
                {
                    if (jmlData > konfigApp.dataAkhir)
                        maxData += Convert.ToInt32(konfigApp.dataAkhir);
                    else
                        maxData += jmlData;

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

                dsGrid.AddRange(output.SF_MON_SATKER_INSIDENTIL);
                this.bsMnt.DataSource = dsGrid;
                this.gridControl1.RefreshDataSource();

            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }

        }

        public void ExportToExcel(string pathToSave)
        {
            bandedGridView1.ExportToXls(pathToSave);
        }

        private void bandedGridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                var row = (SvcMonSatkerInsidentilA1.WASDALSROW_MON_SATKER_INSIDENTIL)e.Row;
                if (e.IsGetData && e.Column == colTGL_LAP_HSL_PEMANTAUAN)
                {
                    if (!row.TGL_LAP_HSL_PEMANTAUAN.ToString().Contains("11/11/00")) {
                        e.Value = row.TGL_LAP_HSL_PEMANTAUAN;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
