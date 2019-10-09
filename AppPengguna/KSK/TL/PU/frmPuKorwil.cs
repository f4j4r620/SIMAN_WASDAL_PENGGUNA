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
    public partial class frmPuKorwil : Form
    {
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ToggleProgressBar toggleProgressBar;
        private SvcKorwilSelect.OutputParameters dOutKorwilSelect;
        private SvcKorwilSelect.dsRKorwilSelect_pttClient ambilKorwil;
        private SvcKorwilSelect.BPSIMANSROW_R_KORWIL dataTerpilih;
        private ArrayList dsGridData = null;
        public string whereAwal = null;
        private string strCari = "";
        private string cariSebelumnya = "";
        private string fieldDicari = "";
        private string modeLoadData = "normal";
        private bool initModeLoad = true;
        public SelectDataUnitKerja selectUnitKerja;
        public string kodeEselonKl = null;
        public string idManualLama = null;
        public string idManualBaru = null;

        public frmPuKorwil()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            jumlahKolom();
        }

        private void frmPuKorwil_Load(object sender, EventArgs e)
        {
            if ((idManualBaru == "" && dsGridData == null) || (idManualBaru == "" && idManualLama != ""))
            {
                strCari = "";
                gcKorwil.DataSource = null;
                getInitialData();
            }
            else if (idManualBaru != "")
            {
                if (idManualBaru != idManualLama)
                {
                    teNamaKolom.Text = gvKorwil.Columns.ColumnByFieldName("KD_WILESELON").Caption;
                    teCari.Text = idManualBaru;
                    gcKorwil.DataSource = null;
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
                SvcKorwilSelect.InputParameters parInp = new SvcKorwilSelect.InputParameters();
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
                ambilKorwil = new SvcKorwilSelect.dsRKorwilSelect_pttClient();
                ambilKorwil.Open();
                ambilKorwil.Beginexecute(parInp, new AsyncCallback(this.getData), null);
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
                dOutKorwilSelect = ambilKorwil.Endexecute(result);
                ambilKorwil.Close();
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsSimpanData(this.dsSimpanData), dOutKorwilSelect);
            }
            catch
            {
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsSimpanData(SvcKorwilSelect.OutputParameters dataOut);

        private void dsSimpanData(SvcKorwilSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_R_KORWIL.Count();

            if (this.dataInisial == true)
            {
                dsGridData = new ArrayList();
                gcKorwil.DataSource = null;
            }
            for (int i = 0; i < jmlData; i++)
            {
                dsGridData.Add(dataOut.SF_ROW_R_KORWIL[i]);
            }
            gcKorwil.DataSource = dsGridData;
            gcKorwil.RefreshDataSource();
            if (jmlData < konfigApp.dataAkhir)
                bbiMoreData.Enabled = false;
            else
                bbiMoreData.Enabled = true;
            if (this.modeLoadData == "cari")
            {
                string xSatu = teNamaKolom.Text.Trim();
                string xDua = teCari.Text.Trim();
                string xTiga = this.fieldDicari;
                gvKorwil.ClearColumnsFilter();
                teNamaKolom.Text = xSatu;
                teCari.Text = xDua;
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
            gvKorwil.ClearColumnsFilter();
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

        private void gvKorwil_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataTerpilih = null;
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0) dataTerpilih = (SvcKorwilSelect.BPSIMANSROW_R_KORWIL)rowTerpilih.GetRow(e.FocusedRowHandle);
        }

        #region Pencarian Data
        private string getFieldKolom(int indeksKolom)
        {
            if (indeksKolom > -1) return gvKorwil.Columns[indeksKolom].FieldName;
            else return "";
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvKorwil.Columns.Count;
            for (int i = 0; i < jmlKolom; i++)
            {
                daftarKolom.Add(gvKorwil.Columns[i].Caption);
                teNamaKolom.Properties.Items.Insert(i, gvKorwil.Columns[i].Caption);
                daftarFieldKolom.Add(gvKorwil.Columns[i].FieldName);
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvKorwil_ColumnFilterChanged(object sender, EventArgs e)
        {
            teCari.Text = gvKorwil.GetFocusedDisplayText();
            if (teCari.Text.Trim() != "")
            {
                teNamaKolom.Text = gvKorwil.FocusedColumn.ToString();
                fieldDicari = gvKorwil.FocusedColumn.FieldName;
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
                            fieldDicari = "d.KD_KL";
                            break;
                        case "KD_ESELONKL":
                            fieldDicari = "c.KD_ESELONKL";
                            break;
                        case "KD_WILESELON":
                            fieldDicari = "a.KD_WILESELON";
                            break;
                    }
                    this.strCari = String.Format(" UPPER({0}) LIKE '{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                }
                else
                {
                    switch (fieldDicari)
                    {
                        case "UR_KL":
                            fieldDicari = "d.UR_KL";
                            break;
                        case "UR_ESELON1":
                            fieldDicari = "c.UR_ESELON1";
                            break;
                        case "UR_KORWIL":
                            fieldDicari = "a.UR_KORWIL";
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
        private void gvKorwil_DoubleClick(object sender, EventArgs e)
        {
            selectData();
        }

        private void gvKorwil_KeyPress(object sender, KeyPressEventArgs e)
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
                selectUnitKerja(dataTerpilih.ID_KORWIL, dataTerpilih.KD_WILESELON, dataTerpilih.UR_KORWIL, dataTerpilih.KD_KL, dataTerpilih.UR_KL);
                Close();
            }
        }
        #endregion
    }
}
