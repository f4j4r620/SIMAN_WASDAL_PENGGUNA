using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.PU
{
    public partial class frmPuAkun : Form
    {
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ToggleProgressBar toggleProgressBar;
        private SvcAkun.OutputParameters dOutData;
        private SvcAkun.doBasSelect_pttClient ambilData;
        private SvcAkun.BPSIMANSROW_T_BAS dataTerpilih;
        private ArrayList dsGridData = null;
        public string whereAwal = "";
        private string strCari = "";
        private string cariSebelumnya = "";
        private string fieldDicari = "";
        private string modeLoadData = "normal";
        private bool initModeLoad = true;
        public SelectDataUnitKerja selectUnitKerja;
        public string kodeKl = null;
        public string idManualLama = "";
        public string idManualBaru = "";

        public frmPuAkun()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            jumlahKolom();
        }

        #region Threadnya
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

        #region Populate Data dari Servis
        public void getInitialData()
        {
            try
            {
                this.Enabled = false;
                myThread = new Thread(new ThreadStart(this.aktifkanProgresBar));
                myThread.Start();
                SvcAkun.InputParameters parInp = new SvcAkun.InputParameters();
                parInp.P_COL = "";
                if (this.dataInisial == true)
                {
                    konfigApp.currentMaks = konfigApp.dataAkhir;
                    konfigApp.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    konfigApp.currentMin = konfigApp.currentMaks + 1;
                    konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataAkhir;
                }
                parInp.P_MAX = konfigApp.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = konfigApp.currentMin;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
                parInp.STR_WHERE = whereAwal + strCari;
                ambilData = new SvcAkun.doBasSelect_pttClient();
                ambilData.Open();
                ambilData.Beginexecute(parInp, new AsyncCallback(this.getData), null);
            }
            catch
            {
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getData(IAsyncResult result)
        {
            try
            {
                dOutData = ambilData.Endexecute(result);
                ambilData.Close();
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsSimpanData(this.dsSimpanData), dOutData);
            }
            catch
            {
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsSimpanData(SvcAkun.OutputParameters dataOut);

        private void dsSimpanData(SvcAkun.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_T_BAS.Count();

            if (this.dataInisial == true)
            {
                dsGridData = new ArrayList();
                gcAkun.DataSource = null;
            }
            for (int i = 0; i < jmlData; i++)
            {
                dsGridData.Add(dataOut.SF_ROW_T_BAS[i]);
            }
            gcAkun.DataSource = dsGridData;
            gcAkun.RefreshDataSource();
            if (jmlData < konfigApp.dataAkhir)
                bbiMoreData.Enabled = false;
            else
                bbiMoreData.Enabled = true;
            if (this.modeLoadData == "cari")
            {
                string xSatu = teNamaKolom.Text.Trim();
                string xDua = teCari.Text.Trim();
                string xTiga = this.fieldDicari;
                gvAkun.ClearColumnsFilter();
                teNamaKolom.Text = xSatu;
                teCari.Text = xDua;
                this.fieldDicari = xTiga;
                idManualLama = idManualBaru;
            }
            else idManualLama = "";
        }
        #endregion

        private void frmPuAkun_Load(object sender, EventArgs e)
        {
            if ((idManualBaru == "" && dsGridData == null) || (idManualBaru == "" && idManualLama != ""))
            {
                strCari = "";
                gcAkun.DataSource = null;
                getInitialData();
            }
            else if (idManualBaru != "")
            {
                if (idManualBaru != idManualLama)
                {
                    teNamaKolom.Text = gvAkun.Columns.ColumnByFieldName("KDMAKMAP").Caption;
                    teCari.Text = idManualBaru;
                    gcAkun.DataSource = null;
                    lakukanCariCata();
                }
            }
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = true;
            this.fieldDicari = "";
            this.strCari = "";
            gvAkun.ClearColumnsFilter();
            teNamaKolom.Text = "";
            teCari.Text = "";
            this.modeLoadData = "normal";
            this.getInitialData();
        }

        private void bbiMoreData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitialData();
        }

        private void bbiTutup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void gvAkun_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataTerpilih = null;
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0) dataTerpilih = (SvcAkun.BPSIMANSROW_T_BAS)rowTerpilih.GetRow(e.FocusedRowHandle);
        }

        #region Pencarian Data
        private string getFieldKolom(int indeksKolom)
        {
            if (indeksKolom > -1) return gvAkun.Columns[indeksKolom].FieldName;
            else return "";
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvAkun.Columns.Count;
            for (int i = 0; i < jmlKolom; i++)
            {
                daftarKolom.Add(gvAkun.Columns[i].Caption);
                teNamaKolom.Properties.Items.Insert(i, gvAkun.Columns[i].Caption);
                daftarFieldKolom.Add(gvAkun.Columns[i].FieldName);
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvAkun_ColumnFilterChanged(object sender, EventArgs e)
        {
            teCari.Text = gvAkun.GetFocusedDisplayText();
            if (teCari.Text.Trim() != "")
            {
                teNamaKolom.Text = gvAkun.FocusedColumn.ToString();
                fieldDicari = gvAkun.FocusedColumn.FieldName;
            }
            else
            {
                teNamaKolom.Text = "";
                fieldDicari = "";
                this.strCari = "";
                if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            }
        }

        private void teNamaKolom_EditValueChanged(object sender, EventArgs e)
        {
            sbCariOnline.Enabled = true;
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            this.fieldDicari = this.getFieldKolom(teNamaKolom.SelectedIndex);
        }

        private void teCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            sbCariOnline.Enabled = true;
            if (teCari.Text.Trim() != "" && teNamaKolom.SelectedIndex == -1) teNamaKolom.SelectedIndex = 0;
        }

        private void lakukanCariCata()
        {
            if (teCari.Text.Trim() != "" && teNamaKolom.Text != "")
            {
                if ((this.modeLoadData != "cari") || (cariSebelumnya != teCari.Text.Trim()))
                {
                    this.dataInisial = true;
                    this.modeLoadData = "cari";
                    cariSebelumnya = teCari.Text.Trim();
                    this.initModeLoad = true;
                }
                else
                {
                    this.dataInisial = false;
                    this.initModeLoad = false;
                }
                if (fieldDicari.Substring(0, 2) == "KD")
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                else
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '%{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                this.getInitialData();
            }
        }

        private void sbCariOnline_Click(object sender, EventArgs e)
        {
            lakukanCariCata();
        }
        #endregion

        #region Ambil data yang dipilih
        private void gvAkun_DoubleClick(object sender, EventArgs e)
        {
            selectData();
        }

        private void gvAkun_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                selectData();
            }
        }

        private void selectData()
        {
            if (dataTerpilih != null)
            {
                selectUnitKerja(dataTerpilih.ID_BAS, dataTerpilih.KDMAKMAP, dataTerpilih.NMMAKMAP, null, null);
                Close();
            }
        }
        #endregion
    }
}
