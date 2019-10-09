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

namespace AppPengguna.KKL.PNBP.KPBMN
{
    public partial class frmPuSk : Form
    {
        public ToggleProgressBar toggleProgressBar;
        public PilihSk pilihSk = null;

        SvcWasdalManfaatSkSelect.OutputParameters dOutDaftarSk;
        SvcWasdalManfaatSkSelect.execute_pttClient ambilDaftarSk;
        SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT dataTerpilih;
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

        public frmPuSk()
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

        private void frmPuSk_Load(object sender, EventArgs e)
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
           
                dataInisial = true;
                gcSk.DataSource = null;
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
                SvcWasdalManfaatSkSelect.InputParameters parInp = new SvcWasdalManfaatSkSelect.InputParameters();
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
                
                parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND ID_PEMOHON = {1} {2}", konfigApp.kdPelayanan, konfigApp.idSatker, this.strCari);
                ambilDaftarSk = new SvcWasdalManfaatSkSelect.execute_pttClient();
                ambilDaftarSk.Open();
                ambilDaftarSk.Beginexecute(parInp, new AsyncCallback(this.getData), null);
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
                dOutDaftarSk = ambilDaftarSk.Endexecute(result);
                ambilDaftarSk.Close();
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsSimpanData(this.dsSimpanData), dOutDaftarSk);
            }
            catch
            {
                this.nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsSimpanData(SvcWasdalManfaatSkSelect.OutputParameters dataOut);

        private void dsSimpanData(SvcWasdalManfaatSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_MANFAAT.Count();
            idPemohonLama = idPemohonBaru;
            if (this.dataInisial == true)
            {
                dsGridData = new ArrayList();
                gcSk.DataSource = null;
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_MANFAAT[i].NUMSpecified = false;
                dsGridData.Add(dataOut.SF_READ_WASDAL_MANFAAT[i]);
            }
            gcSk.DataSource = dsGridData;
            gcSk.RefreshDataSource();
            if (jmlData < konfigApp.dataAkhir)
                bbiMoreData.Enabled = false;
            else
                bbiMoreData.Enabled = true;
            if (this.modeLoadData == "cari")
            {
                string xSatu = teNamaKolom.Text.Trim();
                string xDua = teCari.Text.Trim();
                string xTiga = this.fieldDicari;
                gvSk.ClearColumnsFilter();
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
            gvSk.ClearColumnsFilter();
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
        public SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT daftarSkTerpilih;

        private void bbiSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dataTerpilih != null)
            {
                this.pilihSk(dataTerpilih);
                this.Close();
           
            }
            else MessageBox.Show("Silakan pilih Data No SK terlebih dulu", konfigApp.judulKonfirmasi);
        }

        private void bbiTutup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void cePilihSemua_CheckedChanged(object sender, EventArgs e)
        {
            //if (cePilihSemua.Checked == true)
            //{
            //    for (int i = 0; i < gvAset.RowCount; i++)
            //    {
            //        rowTerpilih.FocusedRowHandle = i;
            //        gvAset.SetFocusedRowCellValue(gridColCek, true);
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < gvAset.RowCount; i++)
            //    {
            //        rowTerpilih.FocusedRowHandle = i;
            //        gvAset.SetFocusedRowCellValue(gridColCek, false);
            //    }
            //}
        }

        private void gvAset_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataTerpilih = null;
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0) dataTerpilih = (SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT)rowTerpilih.GetRow(e.FocusedRowHandle);
        }

        #region Pencarian Data
        private string getFieldKolom(string judulKolom)
        {
            judulKolom = judulKolom.Replace(' ', '_');
            judulKolom = judulKolom.Replace('/', '_');
            string kembalian = "";
            if (judulKolom != "" && judulKolom.Trim() != "System.Collections.ArrayList") 
                kembalian = gvSk.Columns.ColumnByName(judulKolom).FieldName;
            return kembalian;
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvSk.Columns.Count;
            int indeksBaris = 0;
            for (int i = 0; i < jmlKolom; i++)
            {
                if (gvSk.Columns[i].FieldName != "NUM" && gvSk.Columns[i].FieldName != "NUMSpecified" && gvSk.Columns[i].FieldName != "KD_SATKER" && gvSk.Columns[i].FieldName != "UR_SATKER" && gvSk.Columns[i].FieldName != "NO_ASET")
                {
                    daftarKolom.Add(gvSk.Columns[i].Caption);
                    teNamaKolom.Properties.Items.Insert(indeksBaris, gvSk.Columns[i].Caption);
                    daftarFieldKolom.Add(gvSk.Columns[i].FieldName);
                    indeksBaris++;
                }
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvAset_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gvSk.FocusedColumn.FieldName != "NUM" && gvSk.FocusedColumn.FieldName != "NUMSpecified" && gvSk.FocusedColumn.FieldName != "KD_SATKER" && gvSk.FocusedColumn.FieldName != "UR_SATKER" && gvSk.FocusedColumn.FieldName != "NO_ASET")
            {
                teCari.Text = gvSk.GetFocusedDisplayText();
                if (teCari.Text.Trim() != "")
                {
                    teNamaKolom.Text = gvSk.FocusedColumn.ToString();
                    fieldDicari = gvSk.FocusedColumn.FieldName;
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
        TL.PU.frmPuSatker formPopUpSatker;

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
                formPopUpSatker = new TL.PU.frmPuSatker()
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
