using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;

namespace AppPengguna.KSK.WL.PEMANFAATAN
{

    public partial class ucPemanfaatanNew : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        //SvcMonSatkerManfaatA1.execute_pttClient SvcMonSatkerManfaatA1Select = null;
        //SvcMonSatkerManfaatA1.OutputParameters outLapGunaSkSelect = null;
        //SvcMonSatkerManfaatA1.WASDALSROW_MON_GUNA_SATKER_A1 dataTerpilih = null;

        SvcMonSatkerManfaatA1.execute_pttClient SvcMonSatkerManfaatA1Select = null;
        SvcMonSatkerManfaatA1.OutputParameters outLapGunaSkSelect = null;
        SvcMonSatkerManfaatA1.WASDALSROW_MON_MANFAAT_SATKER_A1 dataTerpilih = null;

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

        public ucPemanfaatanNew(FrmKoorSatker _frmSatker, ucLapWasdal _ucASPBMN)
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

                SvcMonSatkerManfaatA1.InputParameters parInp = new SvcMonSatkerManfaatA1.InputParameters();


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

                parInp.P_MAX = 0;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = "ID_SATKER = "+konfigApp.idSatker + this.strCari;
                parInp.P_COL = "";
                parInp.P_SORT = "";
                SvcMonSatkerManfaatA1Select = new SvcMonSatkerManfaatA1.execute_pttClient();
                SvcMonSatkerManfaatA1Select.Beginexecute(parInp, new AsyncCallback(getDataAngkutan), null);
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
                outLapGunaSkSelect = SvcMonSatkerManfaatA1Select.Endexecute(result);
                SvcMonSatkerManfaatA1Select.Close();
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

        private delegate void LoadDataAngkutan(SvcMonSatkerManfaatA1.OutputParameters dataOut);

        private void loadDataAngkutan(SvcMonSatkerManfaatA1.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_MANFAAT_SATKER_A1.Count();
            decimal jmlCurrent = 0;
            string totalData = ""; //(jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_GUNA_SATKER_A1[jmlData - 1].TOTAL_DATA.ToString();
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
                dsGrid.Add(dataOut.SF_MON_MANFAAT_SATKER_A1[i]);
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
                    dataTerpilih = (SvcMonSatkerManfaatA1.WASDALSROW_MON_MANFAAT_SATKER_A1)rowTerpilih.GetRow(e.FocusedRowHandle);
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
                if (bandedGridView1.RowCount > 0)
                {
                    var row = (SvcMonSatkerManfaatA1.WASDALSROW_MON_MANFAAT_SATKER_A1)bandedGridView1.GetFocusedRow();
                    string label = "SATKER :" + konfigApp.kodeSatker + " - " + konfigApp.namaSatker;
                    FormDtlSatker form = new FormDtlSatker(label, konfigApp.idSatker.ToString() + " AND ID_SK_WASDAL_MANFAAT = " + row.ID_SK_WASDAL_MANFAAT);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                
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

        public void ExportToExcel(string path)
        {
            bandedGridView1.ExportToXls(path);
        }

        private void bandedGridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                var row = (SvcMonSatkerManfaatA1.WASDALSROW_MON_MANFAAT_SATKER_A1)e.Row;
                if (e.IsGetData && e.Column == tgl_sk)
                {
                    if (!row.TGL_SK.ToString().Contains("11/11/00"))
                    {
                        e.Value = row.TGL_SK;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

     

    }
}
