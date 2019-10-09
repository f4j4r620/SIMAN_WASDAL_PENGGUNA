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
    public delegate void AmbilJnsBmn(decimal? kd, string ur);
    public partial class frmJnsBmn : DevExpress.XtraEditors.XtraForm
    {
        private SvcJnsBmnSelect.call_pttClient svcCaller;
        private SvcJnsBmnSelect.InputParameters inputPar;
        private SvcJnsBmnSelect.OutputParameters outPar;
        private SvcJnsBmnSelect.BPSIMANSROW_R_JNS_BMN rowData;
        

        public AmbilJnsBmn ambilJnsBmn;

        private decimal? kdJnsBmn { get; set; }
        private string urJnsBmn { get; set; }

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

        public frmJnsBmn()
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

            this.addColumn("KD_JNS_BMN", "KODE JENIS BMN");
            this.addColumn("NM_JNS_BMN", "URAIAN JENIS BMN");


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
                inputPar = new SvcJnsBmnSelect.InputParameters();

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
                inputPar.P_SORT = "DESC";
                inputPar.STR_WHERE = this.strCari;
                svcCaller = new SvcJnsBmnSelect.call_pttClient();
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
                this.Invoke(new ShowData(this.showData), this.outPar);
                this.setThread(false);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcJnsBmnSelect.OutputParameters dataOut);

        public void showData(SvcJnsBmnSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_R_JNS_BMN.Count();
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
                    this.bbiMore.Enabled = false;
                    this.masihAdaData = false;
                    this.sbCariOnline.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.bbiMore.Enabled = false;
                    this.masihAdaData = true;
                    this.sbCariOnline.Enabled = false;
                }
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }

            for (int i = 0; i < jmlData; i++)
            {

                dsGrid.Add(serviceOutPut.SF_ROW_R_JNS_BMN[i]);

            }

            if (dataInisial == true)
            {
                gcJnsBmn.DataSource = null;
                gcJnsBmn.DataSource = dsGrid;
            }
            else
            {
                gcJnsBmn.RefreshDataSource();
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

        private void gvJnsBmn_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {

                    rowData = (SvcJnsBmnSelect.BPSIMANSROW_R_JNS_BMN)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvJnsBmn.GetFocusedDataSourceRowIndex();

                    this.kdJnsBmn = rowData.KD_JNS_BMN;
                    this.urJnsBmn = rowData.NM_JNS_BMN;
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
            this.ambilJnsBmn(this.kdJnsBmn, this.urJnsBmn);
            this.Close();
        }
        private void gvJnsBmn_DoubleClick(object sender, EventArgs e)
        {
            this.ambilJnsBmn(this.kdJnsBmn, this.urJnsBmn);
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