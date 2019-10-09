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
using System.Collections.Generic;

namespace AppPengguna.KKL.TL.TMBMN  
{
    public partial class frmPuAset : Form
    {
        public ToggleProgressBar toggleProgressBar;
        public SimpanDaftarAsetTerpilih simpanAsetTerpilih = null;
        SvcWasdalManfaatSelect.OutputParameters dOutDaftarAset;
        SvcWasdalManfaatSelect.execute_pttClient ambilDaftarAset;
        SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT dataTerpilih;
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        private ArrayList dsGridData = null;
        public string whereAwal = null;
        private string strCari = "";
        private string cariSebelumnya = "";
        private string fieldDicari = "";
        private string modeLoadData = "normal";
        private bool initModeLoad = true;
        public decimal? idSatker = null;
        public string kodeSatker = null;
        public string namaSatker = null;
        public string isTb = null;
        public string skKeputusan=null;
        private decimal? idPemohonLama = null;
        public decimal? idPemohonBaru = null;
        public string levelPenggunaBarang = null;

        public frmPuAset()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            jumlahKolom();
        }

        #region Progress Bar
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

        private void frmPuAset_Load(object sender, EventArgs e)
        {
            cePilihSemua.Checked = true;
            cePilihSemua.Checked = false;
            if (levelPenggunaBarang == konfigApp.levelSatker)
            {
                bbiMoreData.Enabled = true;
                bbiRefresh.Enabled = true;
                bbiSimpan.Enabled = true;
                sbCariSatker.Enabled = false;
            }
            else
            {
                bbiMoreData.Enabled = true;
                bbiRefresh.Enabled = true;
                bbiSimpan.Enabled = true;
                sbCariSatker.Enabled = true;
                if (idPemohonLama != idPemohonBaru)
                {
                    bbiMoreData.Enabled = false;
                    bbiRefresh.Enabled = false;
                    bbiSimpan.Enabled = false;
                }
            }
            //if (idPemohonLama != idPemohonBaru && levelPenggunaBarang == konfigApp.levelSatker)
            //{
            //    dataInisial = true;
            //    gcAset.DataSource = null;
            //    getInitialData();
            //}
            dataInisial = true;
            gcAset.DataSource = null;
            getInitialData();
        }

        #region Populate Data dari Servis
        public void getInitialData()
        {
            try
            {
                this.Enabled = false;
                myThread = new Thread(new ThreadStart(this.aktifkanProgresBar));
                myThread.Start();
                //SvcWasdalManfaatSelect.InputParameters parInp = new SvcWasdalManfaatSelect.InputParameters();
                SvcWasdalManfaatSelect.InputParameters parInp = new SvcWasdalManfaatSelect.InputParameters();
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
                
                string whereTb = "";
               
                parInp.STR_WHERE = String.Format(" NO_BUKTI_LAKSANA IS NULL AND ID_SATKER = {0} AND SK_KEPUTUSAN= '{1}' {2}", idSatker,skKeputusan, strCari);
                ambilDaftarAset = new SvcWasdalManfaatSelect.execute_pttClient();
                ambilDaftarAset.Open();
                ambilDaftarAset.Beginexecute(parInp, new AsyncCallback(this.getData), null);
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
                dOutDaftarAset = ambilDaftarAset.Endexecute(result);
                ambilDaftarAset.Close();
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsSimpanData(this.dsSimpanData), dOutDaftarAset);
            }
            catch
            {
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsSimpanData(SvcWasdalManfaatSelect.OutputParameters dataOut);

        private void dsSimpanData(SvcWasdalManfaatSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_WASDAL_MANFAAT.Count();
            idPemohonLama = idPemohonBaru;
            if (this.dataInisial == true)
            {
                dsGridData = new ArrayList();
                gcAset.DataSource = null;
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_ROW_WASDAL_MANFAAT[i].NUMSpecified = false;
                dsGridData.Add(dataOut.SF_ROW_WASDAL_MANFAAT[i]);
            }
            gcAset.DataSource = dsGridData;
            gcAset.RefreshDataSource();
            if (jmlData < konfigApp.dataAkhir)
                bbiMoreData.Enabled = false;
            else
                bbiMoreData.Enabled = true;
            if (this.modeLoadData == "cari")
            {
                string xSatu = teNamaKolom.Text.Trim();
                string xDua = teCari.Text.Trim();
                string xTiga = this.fieldDicari;
                gvAset.ClearColumnsFilter();
                teNamaKolom.Text = xSatu;
                teCari.Text = xDua;
                this.fieldDicari = xTiga;
            }
        }
        #endregion

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = true;
            this.fieldDicari = "";
            this.strCari = "";
            gvAset.ClearColumnsFilter();
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

        public ArrayList daftarTerpilih = null;
        public SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT daftarAsetTerpilih;

        private void bbiSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rowTerpilih != null)
            {
                int posisiRow = gvAset.GetFocusedDataSourceRowIndex();
                if (rowTerpilih.IsLastRow) gvAset.FocusedRowHandle = posisiRow - 1;
                else gvAset.FocusedRowHandle = posisiRow + 1;

                daftarTerpilih = null;
                daftarTerpilih = new ArrayList();
                string daftarIdAset = "";
                for (int i = 0; i < gvAset.RowCount; i++)
                {
                    string status = Convert.ToString(gvAset.GetRowCellValue(i, "NUMSpecified"));
                    if (status == "True")
                    {
                        daftarTerpilih.Add(gvAset.GetRow(i));
                    }
                }
                if (daftarTerpilih.Count == 0) MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
                else
                {
                    for (int i = 0; i < daftarTerpilih.Count; i++)
                    {
                        daftarAsetTerpilih = (SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT)daftarTerpilih[i];
                        daftarIdAset = String.Format("{0}{1};", daftarIdAset, daftarAsetTerpilih.ID_ASET);
                    }
                    daftarIdAset = daftarIdAset.TrimEnd(';');
                    this.simpanAsetTerpilih(daftarIdAset, 'C');
                    this.Close();
                }
            }
            else MessageBox.Show("Silakan pilih Data Aset terlebih dulu", konfigApp.judulKonfirmasi);
        }

        private void bbiTutup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void cePilihSemua_CheckedChanged(object sender, EventArgs e)
        {
            if (cePilihSemua.Checked == true)
            {
                for (int i = 0; i < gvAset.RowCount; i++)
                {
                    rowTerpilih.FocusedRowHandle = i;
                    gvAset.SetFocusedRowCellValue(gridColCek, true);
                }
            }
            else
            {
                for (int i = 0; i < gvAset.RowCount; i++)
                {
                    rowTerpilih.FocusedRowHandle = i;
                    gvAset.SetFocusedRowCellValue(gridColCek, false);
                }
            }
        }

        private void gvAset_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataTerpilih = null;
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0) dataTerpilih = (SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT)rowTerpilih.GetRow(e.FocusedRowHandle);
        }

        #region Pencarian Data
        private string getFieldKolom(string judulKolom)
        {
            judulKolom = judulKolom.Replace(' ', '_');
            judulKolom = judulKolom.Replace('/', '_');
            string kembalian = "";
            if (judulKolom != "" && judulKolom.Trim() != "System.Collections.ArrayList") 
                kembalian = gvAset.Columns.ColumnByName(judulKolom).FieldName;
            return kembalian;
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvAset.Columns.Count;
            int indeksBaris = 0;
            for (int i = 0; i < jmlKolom; i++)
            {
                if (gvAset.Columns[i].FieldName != "NUM" && gvAset.Columns[i].FieldName != "NUMSpecified" && gvAset.Columns[i].FieldName != "KD_SATKER" && gvAset.Columns[i].FieldName != "UR_SATKER" && gvAset.Columns[i].FieldName != "NO_ASET")
                {
                    daftarKolom.Add(gvAset.Columns[i].Caption);
                    teNamaKolom.Properties.Items.Insert(indeksBaris, gvAset.Columns[i].Caption);
                    daftarFieldKolom.Add(gvAset.Columns[i].FieldName);
                    indeksBaris++;
                }
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvAset_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gvAset.FocusedColumn.FieldName != "NUM" && gvAset.FocusedColumn.FieldName != "NUMSpecified" && gvAset.FocusedColumn.FieldName != "KD_SATKER" && gvAset.FocusedColumn.FieldName != "UR_SATKER" && gvAset.FocusedColumn.FieldName != "NO_ASET")
            {
                teCari.Text = gvAset.GetFocusedDisplayText();
                if (teCari.Text.Trim() != "")
                {
                    teNamaKolom.Text = gvAset.FocusedColumn.ToString();
                    fieldDicari = gvAset.FocusedColumn.FieldName;
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
            teNupDari.Text = "";
            teNupSampai.Text = "";
            if (this.fieldDicari == "KD_BRG" || this.fieldDicari == "UR_SSKEL")
            {
                this.lciNupDari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciNupSampai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.lciNupDari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupSampai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void teCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            sbCariOnline.Enabled = true;
            if (teCari.Text.Trim() != "" && teNamaKolom.SelectedIndex == -1) teNamaKolom.SelectedIndex = 0;
        }

        private void sbCariOnline_Click(object sender, EventArgs e)
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
                this.strCari = String.Format(" AND UPPER({0}) LIKE '%{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                if (fieldDicari == "KD_BRG")
                {
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                }
                if (teNupDari.Text.Trim() != "" && teNupSampai.Text.Trim() != "")
                {
                    this.strCari += String.Format(" AND UPPER(NO_ASET) BETWEEN {0} AND {1} ", teNupDari.Text.Trim().ToUpper(), teNupSampai.Text.Trim().ToUpper());
                }
                this.getInitialData();
            }
        }
        #endregion

        #region Pilih Satker yang akan diambil Asetnya
        PU.frmPuSatker formPopUpSatker;

        private void sbCariSatker_Click(object sender, EventArgs e)
        {
            string preWhere = "";
            if (levelPenggunaBarang == konfigApp.levelKorwil)
                preWhere = " ID_KORWIL = " + idPemohonBaru;
            else if (levelPenggunaBarang == konfigApp.levelEselon1)
                preWhere = " ID_ESELON1 = " + idPemohonBaru;
            else if (levelPenggunaBarang == konfigApp.levelKl)
                preWhere = "ID_KL= " + idPemohonBaru;
            if (formPopUpSatker == null)
            {
                formPopUpSatker = new PU.frmPuSatker()
                {
                    toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                    selectUnitKerja = new SelectDataUnitKerja(setUnitKerja)
                };
            }
            formPopUpSatker.whereAwal = preWhere;
            formPopUpSatker.idPemohonBaru = idPemohonBaru;
            formPopUpSatker.ShowDialog();
        }

        private void setUnitKerja(decimal? _idUnitKerja, string _kodeUnitKerja, string _namaUnitKerja, string _kodeKl, string _namaKl)
        {
            idSatker = _idUnitKerja;
            kodeSatker = _kodeUnitKerja;
            namaSatker = _namaUnitKerja;
            teSatker.Properties.Items.Clear();
            teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", _kodeUnitKerja, _namaUnitKerja));
            teSatker.SelectedIndex = 0;
            bbiMoreData.Enabled = true;
            bbiRefresh.Enabled = true;
            bbiSimpan.Enabled = true;
            sbCariSatker.Enabled = true;
            dataInisial = true;
            getInitialData();
        }
        #endregion

        private void gcAset_Click(object sender, EventArgs e)
        {

        }
    }
}
