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

namespace AppPengguna.KSK.TL.PU
{
    public partial class frmPuEselon1 : Form
    {
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ToggleProgressBar toggleProgressBar;
        private SvcEselon1Select.OutputParameters dOutEselon1Select;
        private SvcEselon1Select.dsREselon1Select_pttClient ambilEselon1;
        private SvcEselon1Select.BPSIMANSROW_R_ESELON1 dataTerpilih;
        private ArrayList dsGridData = null;
        public string whereAwal = null;
        private string strCari = "";
        private string cariSebelumnya = "";
        private string fieldDicari = "";
        private string modeLoadData = "normal";
        private bool initModeLoad = true;
        public SelectDataUnitKerja selectUnitKerja;
        public string kodeKl = null;
        public string idManualLama = null;
        public string idManualBaru = null;

        public frmPuEselon1()
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
                SvcEselon1Select.InputParameters parInp = new SvcEselon1Select.InputParameters();
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
                ambilEselon1 = new SvcEselon1Select.dsREselon1Select_pttClient();
                ambilEselon1.Open();
                ambilEselon1.Beginexecute(parInp, new AsyncCallback(this.getData), null);
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
                dOutEselon1Select = ambilEselon1.Endexecute(result);
                ambilEselon1.Close();
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsSimpanData(this.dsSimpanData), dOutEselon1Select);
            }
            catch
            {
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsSimpanData(SvcEselon1Select.OutputParameters dataOut);

        private void dsSimpanData(SvcEselon1Select.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_R_ESELON1.Count();

            if (this.dataInisial == true)
            {
                dsGridData = new ArrayList();
                gcEselon1.DataSource = null;
            }
            for (int i = 0; i < jmlData; i++)
            {
                dsGridData.Add(dataOut.SF_ROW_R_ESELON1[i]);
            }
            gcEselon1.DataSource = dsGridData;
            gcEselon1.RefreshDataSource();
            if (jmlData < konfigApp.dataAkhir)
                bbiMoreData.Enabled = false;
            else
                bbiMoreData.Enabled = true;
            if (this.modeLoadData == "cari")
            {
                string xSatu = teNamaKolom.Text.Trim();
                string xDua = teCari.Text.Trim();
                string xTiga = this.fieldDicari;
                gvEselon1.ClearColumnsFilter();
                teNamaKolom.Text = xSatu;
                teCari.Text = xDua;
                this.fieldDicari = xTiga;
                idManualLama = idManualBaru;
            }
            else idManualLama = "";
        }
        #endregion

        private void frmPuEselon1_Load(object sender, EventArgs e)
        {
            if ((idManualBaru == "" && dsGridData == null) || (idManualBaru == "" && idManualLama != ""))
            {
                strCari = "";
                gcEselon1.DataSource = null;
                getInitialData();
            }
            else if (idManualBaru != "")
            {
                if (idManualBaru != idManualLama)
                {
                    teNamaKolom.Text = gvEselon1.Columns.ColumnByFieldName("KD_ESELONKL").Caption;
                    teCari.Text = idManualBaru;
                    gcEselon1.DataSource = null;
                    lakukanCariCata();
                }
            }
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = true;
            this.fieldDicari = "";
            this.strCari = "";
            gvEselon1.ClearColumnsFilter();
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

        private void gvEselon1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataTerpilih = null;
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0) dataTerpilih = (SvcEselon1Select.BPSIMANSROW_R_ESELON1)rowTerpilih.GetRow(e.FocusedRowHandle);
        }

        #region Pencarian Data
        private string getFieldKolom(int indeksKolom)
        {
            if (indeksKolom > -1) return gvEselon1.Columns[indeksKolom].FieldName;
            else return "";
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvEselon1.Columns.Count;
            for (int i = 0; i < jmlKolom; i++)
            {
                daftarKolom.Add(gvEselon1.Columns[i].Caption);
                teNamaKolom.Properties.Items.Insert(i, gvEselon1.Columns[i].Caption);
                daftarFieldKolom.Add(gvEselon1.Columns[i].FieldName);
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvEselon1_ColumnFilterChanged(object sender, EventArgs e)
        {
            teCari.Text = gvEselon1.GetFocusedDisplayText();
            if (teCari.Text.Trim() != "")
            {
                teNamaKolom.Text = gvEselon1.FocusedColumn.ToString();
                fieldDicari = gvEselon1.FocusedColumn.FieldName;
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
                {
                    switch (fieldDicari)
                    {
                        case "KD_KL":
                            fieldDicari = "b.KD_KL";
                            break;
                        case "KD_ESELONKL":
                            fieldDicari = "a.KD_ESELONKL";
                            break;
                    }
                    this.strCari = String.Format(" UPPER({0}) LIKE '{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                }
                else
                {
                    switch (fieldDicari)
                    {
                        case "UR_KL":
                            fieldDicari = "b.UR_KL";
                            break;
                        case "UR_ESELON1":
                            fieldDicari = "a.UR_ESELON1";
                            break;
                    }
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
        private void gvEselon1_DoubleClick(object sender, EventArgs e)
        {
            selectData();
        }

        private void gvEselon1_KeyPress(object sender, KeyPressEventArgs e)
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
                selectUnitKerja(dataTerpilih.ID_ESELON1, dataTerpilih.KD_ESELONKL, dataTerpilih.UR_ESELON1, dataTerpilih.KD_KL, dataTerpilih.UR_KL);
                Close();
            }
        }
        #endregion
    }
}
