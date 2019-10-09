using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.AST.TRK 
{
    public delegate void AmbilKl(decimal? id, string kode, string nama);
    public partial class frmKl : DevExpress.XtraEditors.XtraForm
    {
        private SvcKlSelect.call_pttClient svcCaller;
        private SvcKlSelect.InputParameters inputPar;
        private SvcKlSelect.OutputParameters outPar;
        private SvcKlSelect.BPSIMANSROW_R_KL rowData;
        

        public AmbilKl ambilKl;

        public decimal? idKpknl { get; set; }
        private decimal? idKl { get; set; }
        private string kdKl { get; set; }
        private string urKl { get; set; }

        private Thread myThread;

        private int posisiRow = 0;
        GridView rowTerpilih = null;
        private ArrayList dsGrid = null;
        private bool dataInisial = true;
        
        private bool rowTerakhir = false;
        private string fieldDicari = "";
        private string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private bool initModeLoad = true;
        private string cariSebelumnya = "";
        private string strCari = "";

        private bool masihAdaData;

        public frmKl()
        {
            InitializeComponent();
            initSearch();
            this.dataInisial = true;
            getInitialData();
        }

        #region THREAD
        public delegate void SetEnabledForm(bool enabled);
        public delegate void SetProgressBar(BarItemVisibility str);
        public void setProgressBar(BarItemVisibility str)
        {
            if (this.InvokeRequired)
            {
                SetProgressBar p = new SetProgressBar(setProgressBar);
                this.Invoke(p, new object[] { str });
            }
            else
            {
                this.beMarqueeBar.Visibility = str;
            }
        }
        public void setEnabledForm(bool enabled)
        {
            this.controlGroup.Enabled = enabled;
        }
        public void showProgress()
        {
            if (this.IsHandleCreated)
            {
                this.setProgressBar(BarItemVisibility.Always);
            }
        }
        public void setThread(bool start)
        {
            try
            {
                if (start == true)
                {
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new SetEnabledForm(this.setEnabledForm), false);
                        }
                        else
                        {
                            this.setEnabledForm(false);
                        }
                    }
                    myThread = new Thread(new ThreadStart(showProgress));
                    myThread.Start();
                }
                else
                {
                    myThread.Abort();
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new SetProgressBar(this.setProgressBar), BarItemVisibility.Never);
                            this.Invoke(new SetEnabledForm(this.setEnabledForm), true);
                        }
                    }
                    else
                    {
                        this.setProgressBar(BarItemVisibility.Never);
                        this.setEnabledForm(true);
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region SEARCHING
        private DataTable dtTable = new DataTable();
        private DataColumn dtColumn;
        private DataRow dtRow;


        public void initSearch()
        {
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "DISPLAY";
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "VALUE";
            dtTable.Columns.Add(dtColumn);

            this.addColumn("KD_KL", "KODE KL");
            this.addColumn("UR_KL", "URAIAN KL");


            leColumn.Properties.DataSource = dtTable;
            leColumn.Properties.DisplayMember = "DISPLAY";
            leColumn.Properties.ValueMember = "VALUE";

        }
        public void addColumn(string value, string display)
        {
            dtRow = dtTable.NewRow();
            dtRow["DISPLAY"] = display;
            dtRow["VALUE"] = value;
            dtTable.Rows.Add(dtRow);
        }

        #endregion

        #region GET DATA
        public void getInitialData()
        {
            try
            {
                this.setThread(true);
                inputPar = new SvcKlSelect.InputParameters();

                if (dataInisial == true)
                {
                    konfigApp.currentMaks = konfigApp.dataAkhir;
                    konfigApp.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    konfigApp.currentMin = konfigApp.currentMaks + 1;
                    konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataAkhir;
                }

                inputPar.P_MIN = konfigApp.currentMin;
                inputPar.P_MINSpecified = true;
                inputPar.P_MAX = konfigApp.currentMaks;
                inputPar.P_MAXSpecified = true;
                inputPar.P_SORT = "ASC";
                inputPar.STR_WHERE = this.strCari;
                svcCaller = new SvcKlSelect.call_pttClient();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getData), null);

            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }
        
        public void getData(IAsyncResult result)
        {
            try
            {
                this.outPar = svcCaller.Endexecute(result);
                this.setThread(false);
                this.Invoke(new ShowData(this.showData), this.outPar);
                
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcKlSelect.OutputParameters dataOut);

        public void showData(SvcKlSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_R_KL.Count();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.bbiMore.Enabled = true;
                this.masihAdaData = true;
                this.sbCariOnline.Enabled = true;
            }
            else
            {
                if (this.modeLoadData == "normal")
                {
                    this.masihAdaData = false;
                    this.sbCariOnline.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    
                    this.masihAdaData = true;
                    this.sbCariOnline.Enabled = false;
                }
                this.bbiMore.Enabled = false;
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }

            for (int i = 0; i < jmlData; i++)
            {

                dsGrid.Add(serviceOutPut.SF_ROW_R_KL[i]);
            }

            if (dataInisial == true)
            {
                gcKl.DataSource = null;
                gcKl.DataSource = dsGrid;
            }
            else
            {
                gcKl.RefreshDataSource();
            }

        }
        #endregion

        private void leColumn_EditValueChanged(object sender, EventArgs e)
        {
            this.modeLoadData = "ganti_kiword";
            sbCariOnline.Enabled = true;
        }

        private void teKeyword_EditValueChanged(object sender, EventArgs e)
        {
            this.modeLoadData = "ganti_kiword";
            sbCariOnline.Enabled = true;
        }

        private void sbCariOnline_Click(object sender, EventArgs e)
        {
            if (teKeyword.Text.Trim() != "")
            {
                if ((this.modeLoadData != "cari") || (cariSebelumnya != teKeyword.Text.Trim()))
                {
                    this.dataInisial = true;
                    this.modeLoadData = "cari";
                }
                else
                {
                    this.dataInisial = false;
                }


                this.strCari = String.Format(" UPPER({0}) LIKE '%{1}%' ", this.leColumn.EditValue, teKeyword.Text.Trim().ToUpper());

                this.getInitialData();
            }
        }

        private void gvKl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {

                    rowData = (SvcKlSelect.BPSIMANSROW_R_KL)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvKl.GetFocusedDataSourceRowIndex();
                    this.idKl = rowData.ID_KL;
                    this.kdKl = rowData.KD_KL;
                    this.urKl = rowData.UR_KL;
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

        private void bbiPilih_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ambilKl(this.idKl, this.kdKl, this.urKl);
            this.Close();
        }
        private void gvKl_DoubleClick(object sender, EventArgs e)
        {
            this.ambilKl(this.idKl, this.kdKl, this.urKl);
            this.Close();
        }

        private void bbiRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.leColumn.EditValue = null;
            this.teKeyword.Text = "";
            this.strCari = "";
            this.dataInisial = true;
            this.getInitialData();
        }

        private void bbiMore_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitialData();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        
    }
}