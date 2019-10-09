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

namespace AppPengguna.KKL.TL.PU
{
    public partial class frmPuKl : Form
    {
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ToggleProgressBar toggleProgressBar;
        private SvcKlSelect.OutputParameters dOutKlSelect;
        private SvcKlSelect.dsRKlSelect_pttClient ambilKl;
        private SvcKlSelect.BPSIMANSROW_R_KL dataTerpilih;
        private ArrayList dsGridData = null;
        public string whereAwal = null;
        private string strCari = "";
        private string cariSebelumnya = "";
        private string fieldDicari = "";
        private string modeLoadData = "normal";
        private bool initModeLoad = true;
        public SelectDataUnitKerja selectUnitKerja;
        public string idManualLama = null;
        public string idManualBaru = null;

        public frmPuKl()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            jumlahKolom();
        }

        private void frmPuKl_Load(object sender, EventArgs e)
        {
            if ((idManualBaru == "" && dsGridData == null) || (idManualBaru == "" && idManualLama != ""))
            {
                strCari = "";
                gcKl.DataSource = null;
                getInitialData();
            }
            else if (idManualBaru != "")
            {
                if (idManualBaru != idManualLama)
                {
                    teNamaKolom.Text = gvKl.Columns.ColumnByFieldName("KD_KL").Caption;
                    teCari.Text = idManualBaru;
                    gcKl.DataSource = null;
                    lakukanCariCata();
                }
            }
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
                SvcKlSelect.InputParameters parInp = new SvcKlSelect.InputParameters();
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
                parInp.STR_WHERE = whereAwal + (whereAwal.Trim() == "" ? strCari : " AND " + strCari);
                ambilKl = new SvcKlSelect.dsRKlSelect_pttClient();
                ambilKl.Open();
                ambilKl.Beginexecute(parInp, new AsyncCallback(this.getData), null);
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
                dOutKlSelect = ambilKl.Endexecute(result);
                ambilKl.Close();
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsSimpanData(this.dsSimpanData), dOutKlSelect);
            }
            catch
            {
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsSimpanData(SvcKlSelect.OutputParameters dataOut);

        private void dsSimpanData(SvcKlSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_R_KL.Count();

            if (this.dataInisial == true)
            {
                dsGridData = new ArrayList();
                gcKl.DataSource = null;
            }
            for (int i = 0; i < jmlData; i++)
            {
                dsGridData.Add(dataOut.SF_ROW_R_KL[i]);
            }
            gcKl.DataSource = dsGridData;
            gcKl.RefreshDataSource();
            if (jmlData < konfigApp.dataAkhir)
                bbiMoreData.Enabled = false;
            else
                bbiMoreData.Enabled = true;
            if (this.modeLoadData == "cari")
            {
                string xSatu = teNamaKolom.Text.Trim();
                string xDua = teCari.Text.Trim();
                string xTiga = this.fieldDicari;
                gvKl.ClearColumnsFilter();
                teNamaKolom.Text = xSatu;
                teCari.Text = xDua;
                this.fieldDicari = xTiga;
                //sbCariOnline.Enabled = false;
                idManualLama = idManualBaru;
            }
            else idManualLama = "";
        }
        #endregion

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = true;
            this.fieldDicari = "";
            this.strCari = "";
            gvKl.ClearColumnsFilter();
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

        private void gvKl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataTerpilih = null;
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0) dataTerpilih = (SvcKlSelect.BPSIMANSROW_R_KL)rowTerpilih.GetRow(e.FocusedRowHandle);
        }

        #region Pencarian Data
        private string getFieldKolom(string judulKolom)
        {
            judulKolom = judulKolom.Replace(' ', '_');
            judulKolom = judulKolom.Replace('/', '_');
            string kembalian = "";
            if (judulKolom != "" && judulKolom.Trim() != "System.Collections.ArrayList")
                kembalian = gvKl.Columns.ColumnByName(judulKolom).FieldName;
            return kembalian;
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvKl.Columns.Count;
            int indeksBaris = 0;
            for (int i = 0; i < jmlKolom; i++)
            {
                if (gvKl.Columns[i].FieldName != "NUM")
                {
                    daftarKolom.Add(gvKl.Columns[i].Caption);
                    teNamaKolom.Properties.Items.Insert(indeksBaris, gvKl.Columns[i].Caption);
                    daftarFieldKolom.Add(gvKl.Columns[i].FieldName);
                }
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvKl_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gvKl.FocusedColumn.FieldName != "NUM")
            {
                teCari.Text = gvKl.GetFocusedDisplayText();
                if (teCari.Text.Trim() != "")
                {
                    teNamaKolom.Text = gvKl.FocusedColumn.ToString();
                    fieldDicari = gvKl.FocusedColumn.FieldName;
                }
                else
                {
                    teNamaKolom.Text = "";
                    fieldDicari = "";
                    this.strCari = "";
                    if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
                }
            }
        }

        private void teNamaKolom_EditValueChanged(object sender, EventArgs e)
        {
            sbCariOnline.Enabled = true;
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            this.fieldDicari = this.getFieldKolom(teNamaKolom.Text);
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
                if (fieldDicari == "KD_KL")
                    this.strCari = String.Format(" UPPER({0}) LIKE '{1}%' ", "a." + fieldDicari, teCari.Text.Trim().ToUpper());
                else if (fieldDicari == "UR_KL")
                    this.strCari = String.Format(" UPPER({0}) LIKE '%{1}%' ", "a." + fieldDicari, teCari.Text.Trim().ToUpper());
                this.getInitialData();
            }
        }

        private void sbCariOnline_Click(object sender, EventArgs e)
        {
            lakukanCariCata();
        }
        #endregion

        #region Ambil data yang dipilih
        private void gvKl_DoubleClick(object sender, EventArgs e)
        {
            selectData();
        }

        private void gvKl_KeyPress(object sender, KeyPressEventArgs e)
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
                selectUnitKerja(dataTerpilih.ID_KL, dataTerpilih.KD_KL, dataTerpilih.UR_KL, dataTerpilih.ALAMAT_KL, dataTerpilih.UR_KL);
                Close();
            }
        }
        #endregion
    }
}
