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
    public partial class frmPuSatker : Form
    {
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ToggleProgressBar toggleProgressBar;
        private SvcSatkerSelect.OutputParameters dOutSatkerSelect;
        private SvcSatkerSelect.dsRSatkerSelect_pttClient ambilSatker;
        private SvcSatkerSelect.BPSIMANSROW_R_SATKER dataTerpilih;
        private ArrayList dsGridData = null;
        public string whereAwal = null;
        private string strCari = "";
        private string cariSebelumnya = "";
        private string fieldDicari = "";
        private string modeLoadData = "normal";
        private bool initModeLoad = true;
        public decimal? idPemohonLama = null;
        public decimal? idPemohonBaru = null;
        public SelectDataUnitKerja selectUnitKerja;
        public string idManualLama = null;
        public string idManualBaru = null;

        public frmPuSatker()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            jumlahKolom();
        }

        private void frmPuSatker_Load(object sender, EventArgs e)
        {
            if ((idManualBaru == "" && dsGridData == null) || (idManualBaru == "" && idManualLama != "") || (idManualBaru == "" && teCari.Text != "" && teNamaKolom.Text != ""))
            {
                strCari = "";
                teCari.Text = "";
                teNamaKolom.Text = "";
                gcSatker.DataSource = null;
                getInitialData();
            }
            else if (idManualBaru != "")
            {
                if (idManualBaru != idManualLama)
                {
                    teNamaKolom.Text = gvSatker.Columns.ColumnByFieldName("KD_SATKER").Caption;
                    teCari.Text = idManualBaru;
                    gcSatker.DataSource = null;
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
                SvcSatkerSelect.InputParameters parInp = new SvcSatkerSelect.InputParameters();
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
                string _strWhere = "";
                if (whereAwal != "" && strCari != "") _strWhere = String.Format(" {0} AND {1} ", whereAwal, strCari);
                else if (whereAwal != "" && strCari == "") _strWhere = whereAwal;
                else if (whereAwal == "" && strCari != "") _strWhere = strCari;
                parInp.STR_WHERE = _strWhere;
                ambilSatker = new SvcSatkerSelect.dsRSatkerSelect_pttClient();
                ambilSatker.Open();
                ambilSatker.Beginexecute(parInp, new AsyncCallback(this.getData), null);
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
                dOutSatkerSelect = ambilSatker.Endexecute(result);
                ambilSatker.Close();
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsSimpanData(this.dsSimpanData), dOutSatkerSelect);
            }
            catch
            {
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsSimpanData(SvcSatkerSelect.OutputParameters dataOut);

        private void dsSimpanData(SvcSatkerSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_R_SATKER.Count();
            if (this.dataInisial == true)
            {
                dsGridData = new ArrayList();
                gcSatker.DataSource = null;
            }
            for (int i = 0; i < jmlData; i++)
            {
                dsGridData.Add(dataOut.SF_ROW_R_SATKER[i]);
            }
            gcSatker.DataSource = dsGridData;
            gcSatker.RefreshDataSource();
            if (jmlData < konfigApp.dataAkhir)
                bbiMoreData.Enabled = false;
            else
                bbiMoreData.Enabled = true;
            if (this.modeLoadData == "cari")
            {
                string _strCari = strCari;
                string xSatu = teNamaKolom.Text.Trim();
                string xDua = teCari.Text.Trim();
                string xTiga = this.fieldDicari;
                gvSatker.ClearColumnsFilter();
                teNamaKolom.Text = xSatu;
                teCari.Text = xDua;
                strCari = _strCari;
                this.fieldDicari = xTiga;
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
            gvSatker.ClearColumnsFilter();
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

        private void gvSatker_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataTerpilih = null;
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0) dataTerpilih = (SvcSatkerSelect.BPSIMANSROW_R_SATKER)rowTerpilih.GetRow(e.FocusedRowHandle);
        }

        #region Pencarian Data
        private string getFieldKolom(string judulKolom)
        {
            judulKolom = judulKolom.Replace(' ', '_');
            judulKolom = judulKolom.Replace('/', '_');
            string kembalian = "";
            if (judulKolom != "" && judulKolom.Trim() != "System.Collections.ArrayList")
                kembalian = gvSatker.Columns.ColumnByName(judulKolom).FieldName;
            return kembalian;
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvSatker.Columns.Count;
            int indeksBaris = 0;
            for (int i = 0; i < jmlKolom; i++)
            {
                if (gvSatker.Columns[i].FieldName != "NUM")
                {
                    daftarKolom.Add(gvSatker.Columns[i].Caption);
                    teNamaKolom.Properties.Items.Insert(indeksBaris, gvSatker.Columns[i].Caption);
                    daftarFieldKolom.Add(gvSatker.Columns[i].FieldName);
                }
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvSatker_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gvSatker.FocusedColumn.FieldName != "NUM")
            {
                teCari.Text = gvSatker.GetFocusedDisplayText();
                if (teCari.Text.Trim() != "")
                {
                    teNamaKolom.Text = gvSatker.FocusedColumn.ToString();
                    fieldDicari = gvSatker.FocusedColumn.FieldName;
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
                if (fieldDicari.Substring(0, 2) == "KD")
                {
                    /*switch(fieldDicari)
                    {
                        case "KD_KL":
                            fieldDicari = "d.KD_KL";
                            break;
                        case "KD_ESELONKL":
                            fieldDicari = "c.KD_ESELONKL";
                            break;
                        case "KD_WILESELON":
                            fieldDicari = "b.KD_WILESELON";
                            break;
                        case "KD_SATKER":
                            fieldDicari = "a.KD_SATKER";
                            break;
                    }*/
                    this.strCari = String.Format(" UPPER({0}) LIKE '{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                }
                else
                {
                    /*switch(fieldDicari)
                    {
                        case "UR_KL":
                            fieldDicari = "d.UR_KL";
                            break;
                        case "UR_ESELON1":
                            fieldDicari = "c.UR_ESELON1";
                            break;
                        case "UR_KORWIL":
                            fieldDicari = "b.UR_KORWIL";
                            break;
                        case "UR_SATKER":
                            fieldDicari = "a.UR_SATKER";
                            break;
                    }*/
                    this.strCari = String.Format(" UPPER({0}) LIKE '%{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                }
                this.getInitialData();
            }
        }

        private void sbCariOnline_Click(object sender, EventArgs e)
        {
            lakukanCariCata();
        }
        #endregion

        #region Ambil data yang dipilih
        private void gvSatker_DoubleClick(object sender, EventArgs e)
        {
            selectData();
        }

        private void gvSatker_KeyPress(object sender, KeyPressEventArgs e)
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
                selectUnitKerja(dataTerpilih.ID_SATKER, dataTerpilih.KD_SATKER, dataTerpilih.UR_SATKER, dataTerpilih.KD_KL, dataTerpilih.UR_KL);
                Close();
            }
        }
        #endregion
    }
}
