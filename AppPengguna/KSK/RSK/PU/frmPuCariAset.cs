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
using System.IO;

namespace AppPengguna.KSK.RSK.PU
{
    public partial class frmPuCariAset : Form
    {
        public ToggleProgressBar toggleProgressBar;

        public SimpanDaftarAsetTerpilih simpanAsetTerpilih = null;

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
        private decimal? idPemohonLama = null;
        public decimal? idPemohonBaru = null;
        public string levelPenggunaBarang = null;
        //public string statusBmnYn = "";
        public string nosk;
        public string menu="";
        public decimal? idSkWasdal = null;
        public frmPuCariAset()
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
            //if (levelPenggunaBarang == konfigApp.levelSatker)
            //{
            //    bbiMoreData.Enabled = true;
            //    bbiRefresh.Enabled = true;
            //    bbiSimpan.Enabled = true;
            //    sbCariSatker.Enabled = false;
            //}
            //else
            //{
                bbiMoreData.Enabled = true;
                bbiRefresh.Enabled = true;
                bbiSimpan.Enabled = true;
                sbCariSatker.Enabled = true;
                if (idPemohonLama != idPemohonBaru)
                {
                    bbiMoreData.Enabled = true;
                    bbiRefresh.Enabled = true;
                    bbiSimpan.Enabled = true;
                }
            //}
            //if (idPemohonLama != idPemohonBaru && levelPenggunaBarang == konfigApp.levelSatker)
            //if (idPemohonLama != idPemohonBaru)
            //{
                teSatker.Text = "[" + konfigApp.kodeSatker + "] " + konfigApp.namaSatker;
                dataInisial = true;
                gcAset.DataSource = null;
                switch (menu)
                {
                    case "psp": this.Enabled = false; getDaftarAset(); this.Enabled = true; break;
                    case "psplain":
                    case "aspbmn":
                    case "psbmn":
                        this.Enabled = false; getDaftarAsetPspLain(); this.Enabled = true; break;

                    case "sewa":
                    case "pinjampakai":
                    case "kerjasama":
                    case "bgs_bsg":
                    case "kspi":
                        this.Enabled = false; getDaftarAsetSewa(); this.Enabled = true; break;
                    case "jual": this.Enabled = false; getDaftarAsetJual(); this.Enabled = true; break;
                    case "tukar": this.Enabled = false; getDaftarAsetTukar(); this.Enabled = true; break;
                    case "hibah": this.Enabled = false; getDaftarAsetHibah(); this.Enabled = true; break;
                    case "pmp": this.Enabled = false; getDaftarAsetPmp(); this.Enabled = true; break;
                    case "bongkaran": this.Enabled = false; getDaftarAsetBongkaran(); this.Enabled = true; break;
                    default:
                        break;
                }
            //}
        }

        #region Populate Data dari Servis
        #region --++ Ambil Daftar Aset dalam SK PSP
        SvcWasdalPSPBMNSelect.OutputParameters dOutDaftarAset;
        SvcWasdalPSPBMNSelect.execute_pttClient ambilDaftarAset;
        SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP dataAsetTerpilih;
        private ArrayList dsGridDaftarAset;

        private void getDaftarAset()
        {
            //gcDaftarAset.DataSource = null;
            //gcDaftarAset.RefreshDataSource();
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNSelect.InputParameters parInput = new SvcWasdalPSPBMNSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format("IS_VALID IS NULL AND SK_KEPUTUSAN = '{0}' AND ID_SK_WASDAL={1} {2}", nosk.Trim(), idSkWasdal, strCari);
                ambilDaftarAset = new SvcWasdalPSPBMNSelect.execute_pttClient();
                ambilDaftarAset.Open();
                ambilDaftarAset.Beginexecute(parInput, new AsyncCallback(goGetDaftarAset), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAset(IAsyncResult result)
        {
            try
            {
                dOutDaftarAset = ambilDaftarAset.Endexecute(result);
                ambilDaftarAset.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAset(loadDataDaftarAset), dOutDaftarAset);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAset(SvcWasdalPSPBMNSelect.OutputParameters outputDaftarAset);

        private void loadDataDaftarAset(SvcWasdalPSPBMNSelect.OutputParameters outputDaftarAset)
        {
            int jmlData = outputDaftarAset.SF_ROW_WASDAL_PSP.Count();

            //var test = outputDaftarAset.SF_ROW_WASDAL_PSP.Where(x => x.NO_ASET >= 173 && x.NO_ASET <= 193).ToList();

            //outputDaftarAset.SF_ROW_WASDAL_PSP = test as;

            decimal? nilaiPenetapan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAset.SF_ROW_WASDAL_PSP[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAset.SF_ROW_WASDAL_PSP[i].IS_TB = (outputDaftarAset.SF_ROW_WASDAL_PSP[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAset.SF_ROW_WASDAL_PSP[i].NUMSpecified = false;
                    dsGridDaftarAset.Add(outputDaftarAset.SF_ROW_WASDAL_PSP[i]);
                    nilaiPenetapan += outputDaftarAset.SF_ROW_WASDAL_PSP[i].NILAI_PERSETUJUAN;
                    jmlKuantitas += outputDaftarAset.SF_ROW_WASDAL_PSP[i].KUANTITAS;
                }
                gcAset.DataSource = null;
                gcAset.DataSource = dsGridDaftarAset;
            }
            else
            {
                gcAset.DataSource = null;
            }
            //menampilkan jumlah data keseluruhan
            int jmlCurrent = this.dsGridDaftarAset.Count;
            string totalData = (jmlData == 0) ? jmlData.ToString() : outputDaftarAset.SF_ROW_WASDAL_PSP[jmlData - 1].TOTAL_DATA.ToString();
            labelControl1.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";

            gcAset.DataSource = dsGridDaftarAset;
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
            gvAset.BestFitColumns();
        }

        #endregion

        #region --++ Ambil Daftar Aset dalam SK PSPLAIN
        SvcWasdalPSPBMNLAINSelect.OutputParameters dOutDaftarAsetPspLain;
        SvcWasdalPSPBMNLAINSelect.call_pttClient ambilDaftarAsetPspLain;
        SvcWasdalPSPBMNLAINSelect.WASDALSROW_WASDAL_PSP_LAIN dataAsetTerpilihPspLain;
        private ArrayList dsGridDaftarAsetPspLain;

        private void getDaftarAsetPspLain()
        {
            //gcDaftarAset.DataSource = null;
            //gcDaftarAset.RefreshDataSource();
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNLAINSelect.InputParameters parInput = new SvcWasdalPSPBMNLAINSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format("IS_VALID IS NULL AND SK_KEPUTUSAN = '{0}' AND ID_SK_WASDAL={1} {2}", nosk.Trim(), idSkWasdal, strCari);
                ambilDaftarAsetPspLain = new SvcWasdalPSPBMNLAINSelect.call_pttClient();
                ambilDaftarAsetPspLain.Open();
                ambilDaftarAsetPspLain.Beginexecute(parInput, new AsyncCallback(goGetDaftarAsetPspLain), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAsetPspLain(IAsyncResult result)
        {
            try
            {
                dOutDaftarAsetPspLain = ambilDaftarAsetPspLain.Endexecute(result);
                ambilDaftarAsetPspLain.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAsetPspLain(loadDataDaftarAsetPspLain), dOutDaftarAsetPspLain);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAsetPspLain(SvcWasdalPSPBMNLAINSelect.OutputParameters dOutDaftarAsetPspLain);

        private void loadDataDaftarAsetPspLain(SvcWasdalPSPBMNLAINSelect.OutputParameters dOutDaftarAsetPspLain)
        {
            int jmlData = dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN.Count();

            //var test = outputDaftarAset.SF_ROW_WASDAL_PSP.Where(x => x.NO_ASET >= 173 && x.NO_ASET <= 193).ToList();

            //outputDaftarAset.SF_ROW_WASDAL_PSP = test as;

            decimal? nilaiPenetapan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN[i].IS_TB = (dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN[i].NUMSpecified = false;
                    dsGridDaftarAset.Add(dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN[i]);
                    nilaiPenetapan += dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN[i].NILAI_PERSETUJUAN;
                    jmlKuantitas += dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN[i].KUANTITAS;
                }
                gcAset.DataSource = null;
                gcAset.DataSource = dsGridDaftarAset;
            }
            else
            {
                gcAset.DataSource = null;
            }
            //menampilkan jumlah data keseluruhan
            int jmlCurrent = this.dsGridDaftarAset.Count;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dOutDaftarAsetPspLain.SF_ROW_WASDAL_PSP_LAIN[jmlData - 1].TOTAL_DATA.ToString();
            labelControl1.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";

            gcAset.DataSource = dsGridDaftarAset;
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
            gvAset.BestFitColumns();
        }

        #endregion
                
        #region --++ Ambil Daftar Aset dalam SK SEWA
        SvcWasdalManfaatSelect.OutputParameters dOutDaftarAsetSewa;
        SvcWasdalManfaatSelect.execute_pttClient ambilDaftarAsetSewa;
        SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT dataAsetTerpilihSewa;
        private ArrayList dsGridDaftarAsetSewa;

        private void getDaftarAsetSewa()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalManfaatSelect.InputParameters parInput = new SvcWasdalManfaatSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format("IS_VALID IS NULL AND SK_KEPUTUSAN = '{0}' AND ID_SK_WASDAL_MANFAAT={1} {2}", nosk.Trim(), idSkWasdal, strCari);
                ambilDaftarAsetSewa = new SvcWasdalManfaatSelect.execute_pttClient();
                ambilDaftarAsetSewa.Open();
                ambilDaftarAsetSewa.Beginexecute(parInput, new AsyncCallback(goGetDaftarAsetSewa), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAsetSewa(IAsyncResult result)
        {
            try
            {
                dOutDaftarAsetSewa = ambilDaftarAsetSewa.Endexecute(result);
                ambilDaftarAsetSewa.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAsetSewa(loadDataDaftarAsetSewa), dOutDaftarAsetSewa);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAsetSewa(SvcWasdalManfaatSelect.OutputParameters outputDaftarAsetSewa);

        private void loadDataDaftarAsetSewa(SvcWasdalManfaatSelect.OutputParameters outputDaftarAsetSewa)
        {
            int jmlData = outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT[i].IS_TB = (outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT[i].NUMSpecified = false;
                    dsGridDaftarAset.Add(outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT[i]);
                    nilaiPersetujuan = nilaiPersetujuan + outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT[i].KUANTITAS;
                }
                gcAset.DataSource = null;
                gcAset.DataSource = dsGridDaftarAset;
            }
            else
            {
                gcAset.DataSource = null;
            }
            //menampilkan jumlah data keseluruhan
            int jmlCurrent = this.dsGridDaftarAset.Count;
            string totalData = (jmlData == 0) ? jmlData.ToString() : outputDaftarAsetSewa.SF_ROW_WASDAL_MANFAAT[jmlData - 1].TOTAL_DATA.ToString();
            labelControl1.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";

            gcAset.DataSource = dsGridDaftarAset;
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
            gvAset.BestFitColumns();

        }

        private void sbRefresh_Click(object sender, EventArgs e)
        {
            switch (menu)
            {
                case "psp": this.Enabled = false; getDaftarAset(); this.Enabled = true; break;
                case "psplain":
                case "aspbmn":
                case "psbmn":
                            this.Enabled = false; getDaftarAsetPspLain(); this.Enabled = true; break;
                case "jual": this.Enabled = false; getDaftarAsetJual(); this.Enabled = true; break;
                case "sewa": 
                case "pinjampakai":
                case "kerjasama":
                    this.Enabled = false; getDaftarAsetSewa(); this.Enabled = true; break;
                default:
                    break;
            }
        }
        #endregion

        #region --++ Ambil Daftar Aset dalam SK Jual
        SvcWasdalJualSelect.OutputParameters dOutDaftarAsetJual;
        SvcWasdalJualSelect.execute_pttClient ambilDaftarAsetJual;
        SvcWasdalJualSelect.WASDALSROW_WASDAL_PT_JUAL dataAsetTerpilihJual;
        private ArrayList dsGridDaftarAsetJual;

        private void getDaftarAsetJual()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalJualSelect.InputParameters parInput = new SvcWasdalJualSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format("IS_VALID IS NULL AND (SK_KEPUTUSAN = '{0}') {1}", nosk.Trim(), strCari);
                ambilDaftarAsetJual = new SvcWasdalJualSelect.execute_pttClient();
                ambilDaftarAsetJual.Open();
                ambilDaftarAsetJual.Beginexecute(parInput, new AsyncCallback(goGetDaftarAsetJual), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAsetJual(IAsyncResult result)
        {
            try
            {
                dOutDaftarAsetJual = ambilDaftarAsetJual.Endexecute(result);
                ambilDaftarAsetJual.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAsetJual(loadDataDaftarAsetJual), dOutDaftarAsetJual);
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAsetJual(SvcWasdalJualSelect.OutputParameters outputDaftarAsetJual);

        private void loadDataDaftarAsetJual(SvcWasdalJualSelect.OutputParameters outputDaftarAsetJual)
        {
            int jmlData = outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL[i].IS_TB = (outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL[i].NUMSpecified = false;
                    //outputDaftarAset.SF_ROW_WASDAL_PT_JUAL[i].DARI_TGL
                    dsGridDaftarAset.Add(outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL[i]);
                    nilaiPersetujuan = nilaiPersetujuan + outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL[i].KUANTITAS;
                }
                gcAset.DataSource = null;
                gcAset.DataSource = dsGridDaftarAset;
            }
            else
            {
                gcAset.DataSource = null;
            }
            //menampilkan jumlah data keseluruhan
            int jmlCurrent = this.dsGridDaftarAset.Count;
            string totalData = (jmlData == 0) ? jmlData.ToString() : outputDaftarAsetJual.SF_ROW_WASDAL_PT_JUAL[jmlData - 1].TOTAL_DATA.ToString();
            labelControl1.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";

            gcAset.DataSource = dsGridDaftarAset;
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
            gvAset.BestFitColumns();

        }

        #endregion

        #region --++ Ambil Daftar Aset dalam SK Tukar
        SvcWasdalTmSelect.OutputParameters dOutDaftarAsetTukar;
        SvcWasdalTmSelect.execute_pttClient ambilDaftarAsetTukar;
        SvcWasdalTmSelect.WASDALSROW_WASDAL_PT_TUKAR dataAsetTerpilihTukar;
        private ArrayList dsGridDaftarAsetTukar;

        private void getDaftarAsetTukar()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalTmSelect.InputParameters parInput = new SvcWasdalTmSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format("IS_VALID IS NULL AND (SK_KEPUTUSAN = '{0}') {1}", nosk.Trim(), strCari);
                ambilDaftarAsetTukar = new SvcWasdalTmSelect.execute_pttClient();
                ambilDaftarAsetTukar.Open();
                ambilDaftarAsetTukar.Beginexecute(parInput, new AsyncCallback(goGetDaftarAsetTukar), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAsetTukar(IAsyncResult result)
        {
            try
            {
                dOutDaftarAsetTukar = ambilDaftarAsetTukar.Endexecute(result);
                ambilDaftarAsetTukar.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAsetTukar(loadDataDaftarAsetTukar), dOutDaftarAsetTukar);
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAsetTukar(SvcWasdalTmSelect.OutputParameters outputDaftarAsetTukar);

        private void loadDataDaftarAsetTukar(SvcWasdalTmSelect.OutputParameters outputDaftarAsetTukar)
        {
            int jmlData = outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR[i].IS_TB = (outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR[i].NUMSpecified = false;
                    //outputDaftarAset.SF_ROW_WASDAL_PT_JUAL[i].DARI_TGL
                    dsGridDaftarAset.Add(outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR[i]);
                    nilaiPersetujuan = nilaiPersetujuan + outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR[i].KUANTITAS;
                }
                gcAset.DataSource = null;
                gcAset.DataSource = dsGridDaftarAset;
            }
            else
            {
                gcAset.DataSource = null;
            }
            //menampilkan jumlah data keseluruhan
            int jmlCurrent = this.dsGridDaftarAset.Count;
            string totalData = (jmlData == 0) ? jmlData.ToString() : outputDaftarAsetTukar.SF_ROW_WASDAL_PT_TUKAR[jmlData - 1].TOTAL_DATA.ToString();
            labelControl1.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";

            gcAset.DataSource = dsGridDaftarAset;
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
            gvAset.BestFitColumns();

        }

        #endregion

        #region --++ Ambil Daftar Aset dalam SK Hibah
        SvcWasdalHibahSelect.OutputParameters dOutDaftarAsetHibah;
        SvcWasdalHibahSelect.execute_pttClient ambilDaftarAsetHibah;
        SvcWasdalHibahSelect.WASDALSROW_WASDAL_PT_HIBAH dataAsetTerpilihHibah;
        private ArrayList dsGridDaftarAsetHibah;

        private void getDaftarAsetHibah()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalHibahSelect.InputParameters parInput = new SvcWasdalHibahSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format("IS_VALID IS NULL AND (SK_KEPUTUSAN = '{0}') {1}", nosk.Trim(), strCari);
                ambilDaftarAsetHibah = new SvcWasdalHibahSelect.execute_pttClient();
                ambilDaftarAsetHibah.Open();
                ambilDaftarAsetHibah.Beginexecute(parInput, new AsyncCallback(goGetDaftarAsetHibah), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAsetHibah(IAsyncResult result)
        {
            try
            {
                dOutDaftarAsetHibah = ambilDaftarAsetHibah.Endexecute(result);
                ambilDaftarAsetHibah.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAsetHibah(loadDataDaftarAsetHibah), dOutDaftarAsetHibah);
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAsetHibah(SvcWasdalHibahSelect.OutputParameters outputDaftarAsetHibah);

        private void loadDataDaftarAsetHibah(SvcWasdalHibahSelect.OutputParameters outputDaftarAsetHibah)
        {
            int jmlData = outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH[i].IS_TB = (outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH[i].NUMSpecified = false;
                    //outputDaftarAset.SF_ROW_WASDAL_PT_JUAL[i].DARI_TGL
                    dsGridDaftarAset.Add(outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH[i]);
                    nilaiPersetujuan = nilaiPersetujuan + outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH[i].KUANTITAS;
                }
                gcAset.DataSource = null;
                gcAset.DataSource = dsGridDaftarAset;
            }
            else
            {
                gcAset.DataSource = null;
            }
            //menampilkan jumlah data keseluruhan
            int jmlCurrent = this.dsGridDaftarAset.Count;
            string totalData = (jmlData == 0) ? jmlData.ToString() : outputDaftarAsetHibah.SF_ROW_WASDAL_PT_HIBAH[jmlData - 1].TOTAL_DATA.ToString();
            labelControl1.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";

            gcAset.DataSource = dsGridDaftarAset;
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
            gvAset.BestFitColumns();

        }

        #endregion

        #region --++ Ambil Daftar Aset dalam SK PMP
        SvcWasdalPmpSelect.OutputParameters dOutDaftarAsetPmp;
        SvcWasdalPmpSelect.execute_pttClient ambilDaftarAsetPmp;
        SvcWasdalPmpSelect.WASDALSROW_WASDAL_PT_MODAL dataAsetTerpilihPmp;
        private ArrayList dsGridDaftarAsetPmp;

        private void getDaftarAsetPmp()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalPmpSelect.InputParameters parInput = new SvcWasdalPmpSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format("IS_VALID IS NULL AND (SK_KEPUTUSAN = '{0}') {1}", nosk.Trim(), strCari);
                ambilDaftarAsetPmp = new SvcWasdalPmpSelect.execute_pttClient();
                ambilDaftarAsetPmp.Open();
                ambilDaftarAsetPmp.Beginexecute(parInput, new AsyncCallback(goGetDaftarAsetPmp), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAsetPmp(IAsyncResult result)
        {
            try
            {
                dOutDaftarAsetPmp = ambilDaftarAsetPmp.Endexecute(result);
                ambilDaftarAsetPmp.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAsetPmp(loadDataDaftarAsetPmp), dOutDaftarAsetPmp);
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAsetPmp(SvcWasdalPmpSelect.OutputParameters outputDaftarAsetPmp);

        private void loadDataDaftarAsetPmp(SvcWasdalPmpSelect.OutputParameters outputDaftarAsetPmp)
        {
            int jmlData = outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL[i].IS_TB = (outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL[i].NUMSpecified = false;
                    //outputDaftarAset.SF_ROW_WASDAL_PT_JUAL[i].DARI_TGL
                    dsGridDaftarAset.Add(outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL[i]);
                    nilaiPersetujuan = nilaiPersetujuan + outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL[i].KUANTITAS;
                }
                gcAset.DataSource = null;
                gcAset.DataSource = dsGridDaftarAset;
            }
            else
            {
                gcAset.DataSource = null;
            }
            //menampilkan jumlah data keseluruhan
            int jmlCurrent = this.dsGridDaftarAset.Count;
            string totalData = (jmlData == 0) ? jmlData.ToString() : outputDaftarAsetPmp.SF_ROW_WASDAL_PT_MODAL[jmlData - 1].TOTAL_DATA.ToString();
            labelControl1.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";

            gcAset.DataSource = dsGridDaftarAset;
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
            gvAset.BestFitColumns();

        }

        #endregion

        #region --++ Ambil Daftar Aset dalam SK Bongkaran
        SvcWasdalRSKBongkaranDtlSelect.OutputParameters dOutDaftarAsetBongkaran;
        SvcWasdalRSKBongkaranDtlSelect.execute_pttClient ambilDaftarAsetBongkaran;
        SvcWasdalRSKBongkaranDtlSelect.WASDALSROW_BONGKARAN_GRID_DTL dataAsetTerpilihBongkaran;
        private ArrayList dsGridDaftarAsetBongkaran;

        private void getDaftarAsetBongkaran()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalRSKBongkaranDtlSelect.InputParameters parInput = new SvcWasdalRSKBongkaranDtlSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format("IS_VALID IS NULL AND (SK_KEPUTUSAN_BNK = '{0}') {1}", nosk.Trim(), strCari);
                ambilDaftarAsetBongkaran = new SvcWasdalRSKBongkaranDtlSelect.execute_pttClient();
                ambilDaftarAsetBongkaran.Open();
                ambilDaftarAsetBongkaran.Beginexecute(parInput, new AsyncCallback(goGetDaftarAsetBongkaran), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAsetBongkaran(IAsyncResult result)
        {
            try
            {
                dOutDaftarAsetBongkaran = ambilDaftarAsetBongkaran.Endexecute(result);
                ambilDaftarAsetBongkaran.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAsetBongkaran(loadDataDaftarAsetBongkaran), dOutDaftarAsetBongkaran);
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAsetBongkaran(SvcWasdalRSKBongkaranDtlSelect.OutputParameters outputDaftarAsetBongkaran);

        private void loadDataDaftarAsetBongkaran(SvcWasdalRSKBongkaranDtlSelect.OutputParameters outputDaftarAsetBongkaran)
        {
            int jmlData = outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL[i].IS_TB = (outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL[i].NUMSpecified = false;
                    //outputDaftarAset.SF_ROW_WASDAL_PT_JUAL[i].DARI_TGL
                    dsGridDaftarAset.Add(outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL[i]);
                    nilaiPersetujuan = nilaiPersetujuan + outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL[i].KUANTITAS;
                }
                gcAset.DataSource = null;
                gcAset.DataSource = dsGridDaftarAset;
            }
            else
            {
                gcAset.DataSource = null;
            }
            //menampilkan jumlah data keseluruhan
            int jmlCurrent = this.dsGridDaftarAset.Count;
            string totalData = (jmlData == 0) ? jmlData.ToString() : outputDaftarAsetBongkaran.SF_SK_BONGKARAN_GRID_DTL[jmlData - 1].TOTAL_DATA.ToString();
            labelControl1.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";

            gcAset.DataSource = dsGridDaftarAset;
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
            gvAset.BestFitColumns();

        }

        #endregion
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
            switch (menu)
            {
                case "psp": this.Enabled = false; getDaftarAset(); this.Enabled = true; break;
                case "psplain": 
                case "aspbmn":
                case "psbmn":
                    this.Enabled = false; getDaftarAsetPspLain(); this.Enabled = true; break;
                case "sewa": 
                case "pinjampakai":
                case "kerjasama":
                    this.Enabled = false; getDaftarAsetSewa(); this.Enabled = true; break;
                case "jual": this.Enabled = false; getDaftarAsetJual(); this.Enabled = true; break;
                case "tukar": this.Enabled = false; getDaftarAsetTukar(); this.Enabled = true; break;
                case "hibah": this.Enabled = false; getDaftarAsetHibah(); this.Enabled = true; break;
                case "pmp": this.Enabled = false; getDaftarAsetPmp(); this.Enabled = true; break;
                case "bongkaran": this.Enabled = false; getDaftarAsetBongkaran(); this.Enabled = true; break;
                default:
                    break;
            }
        }

        private void bbiMoreData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            switch (menu)
            {
                case "psp": this.Enabled = false; getDaftarAset(); this.Enabled = true; break;
                case "psplain": 
                case "aspbmn":
                case "psbmn":
                    this.Enabled = false; getDaftarAsetPspLain(); this.Enabled = true; break;
                
                case "sewa": 
                case "pinjampakai":
                case "kerjasama":
                    this.Enabled = false; getDaftarAsetSewa(); this.Enabled = true; break;
                case "jual": this.Enabled = false; getDaftarAsetJual(); this.Enabled = true; break;
                case "tukar": this.Enabled = false; getDaftarAsetTukar(); this.Enabled = true; break;
                case "hibah": this.Enabled = false; getDaftarAsetHibah(); this.Enabled = true; break;
                case "pmp": this.Enabled = false; getDaftarAsetPmp(); this.Enabled = true; break;
                case "bongkaran": this.Enabled = false; getDaftarAsetBongkaran(); this.Enabled = true; break;
                default:
                break;
            }
        }

        public ArrayList daftarTerpilih = null;
        public SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP daftarAsetTerpilih;

        private void bbiSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (rowTerpilih != null)
            //{
            //    int posisiRow = gvAset.GetFocusedDataSourceRowIndex();
            //    if (rowTerpilih.IsLastRow) gvAset.FocusedRowHandle = posisiRow - 1;
            //    else gvAset.FocusedRowHandle = posisiRow + 1;

            //    daftarTerpilih = null;
            //    daftarTerpilih = new ArrayList();
            //    string daftarIdAset = "";
            //    string daftarNilaiPerolehan = "";
            //    for (int i = 0; i < gvAset.RowCount; i++)
            //    {
            //        string status = Convert.ToString(gvAset.GetRowCellValue(i, "NUMSpecified"));
            //        if (status == "True")
            //        {
            //            daftarTerpilih.Add(gvAset.GetRow(i));
            //        }
            //    }
            //    if (daftarTerpilih.Count == 0) MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
            //    else
            //    {
            //        for (int i = 0; i < daftarTerpilih.Count; i++)
            //        {
            //            daftarAsetTerpilih = (SvcWasdalAsetSelect.WASDALSROW_WASDAL_M_ASET)daftarTerpilih[i];
            //            daftarIdAset = String.Format("{0}{1};", daftarIdAset, daftarAsetTerpilih.ID_ASET);
            //            daftarNilaiPerolehan = String.Format("{0}{1};", daftarNilaiPerolehan, daftarAsetTerpilih.NILAI_BUKU);
            //        }
            //        daftarIdAset = daftarIdAset.TrimEnd(';');
            //        string[] nilPerolehan = daftarNilaiPerolehan.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //        this.simpanAsetTerpilih(daftarIdAset, 'C');
            //        this.Close();
            //    }
            //}
            //else MessageBox.Show("Silakan pilih Data Aset terlebih dulu", konfigApp.judulKonfirmasi);


            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        string fileExtenstion = new FileInfo(exportFilePath).Extension;

                        switch (fileExtenstion)
                        {
                            case ".xls":
                                gcAset.ExportToXls(exportFilePath);
                                break;
                            case ".xlsx":
                                gcAset.ExportToXlsx(exportFilePath);
                                break;
                            default:
                                break;
                        }

                        if (File.Exists(exportFilePath))
                        {
                            try
                            {
                                //Try to open the file and let windows decide how to open it.
                                System.Diagnostics.Process.Start(exportFilePath);
                            }
                            catch
                            {
                                String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                                MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Eksport grid gagal");
                throw;
            }
            
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
            
            
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0)
            {
                switch (menu)
                { 
                    case "psp":
                        dataAsetTerpilih = null;
                        dataAsetTerpilih = (SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP)rowTerpilih.GetRow(e.FocusedRowHandle);
                        break;
                    case "psplain":
                    case "aspbmn":
                    case "psbmn":
                        dataAsetTerpilihPspLain = null;
                        dataAsetTerpilihPspLain = (SvcWasdalPSPBMNLAINSelect.WASDALSROW_WASDAL_PSP_LAIN)rowTerpilih.GetRow(e.FocusedRowHandle);
                        break;
                    case "sewa":
                    case "pinjampakai":
                    case "kerjasama":
                        dataAsetTerpilihSewa = null;
                        dataAsetTerpilihSewa = (SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT)rowTerpilih.GetRow(e.FocusedRowHandle);
                        break;
                    case "jual":
                        dataAsetTerpilihJual = null;
                        dataAsetTerpilihJual = (SvcWasdalJualSelect.WASDALSROW_WASDAL_PT_JUAL)rowTerpilih.GetRow(e.FocusedRowHandle);
                        break;
                    case "tukar":
                        dataAsetTerpilihTukar = null;
                        dataAsetTerpilihTukar = (SvcWasdalTmSelect.WASDALSROW_WASDAL_PT_TUKAR)rowTerpilih.GetRow(e.FocusedRowHandle);
                        break;
                    case "hibah":
                        dataAsetTerpilihHibah = null;
                        dataAsetTerpilihHibah = (SvcWasdalHibahSelect.WASDALSROW_WASDAL_PT_HIBAH)rowTerpilih.GetRow(e.FocusedRowHandle);
                        break;
                    case "pmp":
                        dataAsetTerpilihPmp = null;
                        dataAsetTerpilihPmp = (SvcWasdalPmpSelect.WASDALSROW_WASDAL_PT_MODAL)rowTerpilih.GetRow(e.FocusedRowHandle);
                        break;
                    case "bongkaran":
                        dataAsetTerpilihBongkaran = null;
                        dataAsetTerpilihBongkaran = (SvcWasdalRSKBongkaranDtlSelect.WASDALSROW_BONGKARAN_GRID_DTL)rowTerpilih.GetRow(e.FocusedRowHandle);
                        break;

                }
            }
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
                if (gvAset.Columns[i].FieldName != "NUM" && gvAset.Columns[i].FieldName != "NUMSpecified" && gvAset.Columns[i].FieldName != "KD_SATKER" && gvAset.Columns[i].FieldName != "UR_SATKER" && gvAset.Columns[i].FieldName != "NO_ASET" && gvAset.Columns[i].FieldName != "NILAI_SBLM_SUSUT" && gvAset.Columns[i].FieldName != "NOREG")
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
            if (teCari.Text.Trim() != "" && teNamaKolom.SelectedIndex == -1) teNamaKolom.SelectedIndex = 0;
            if (teNupSampai.Text.Trim() != "" && teNamaKolom.SelectedIndex == -1) teNamaKolom.SelectedIndex = 0;
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
                    this.dataInisial = true;
                    this.modeLoadData = "cari";
                    cariSebelumnya = teCari.Text.Trim();
                    this.initModeLoad = true;

                    this.strCari += String.Format(" AND UPPER(NO_ASET) BETWEEN {0} AND {1} ", teNupDari.Text.Trim().ToUpper(), teNupSampai.Text.Trim().ToUpper());
                }
                switch (menu)
                {
                    case "psp": this.Enabled = false; getDaftarAset(); this.Enabled = true; break;
                    case "psplain": 
                    case "aspbmn":
                    case "psbmn":
                                 this.Enabled = false; getDaftarAsetPspLain(); this.Enabled = true; break;
                    case "jual": this.Enabled = false; getDaftarAsetJual(); this.Enabled = true; break;
                    case "sewa":
                    case "pinjampakai":
                    case "kerjasama":
                        this.Enabled = false; getDaftarAsetSewa(); this.Enabled = true; break;
                    case "bongkaran": this.Enabled = false; getDaftarAsetBongkaran(); this.Enabled = true; break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Pilih Satker yang akan diambil Asetnya
        frmPuSatker formPopUpSatker;
        public string statusBmnYn;

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
                formPopUpSatker = new frmPuSatker()
                {
                    toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                    selectUnitKerja = new SelectDataUnitKerja(setUnitKerja)
                };
            }
            formPopUpSatker.whereAwal = " KD_SATKER LIKE '"+konfigApp.kodeSatker.Substring(0,15)+"%'";
            formPopUpSatker.idPemohonBaru = idPemohonBaru;
            //formPopUpSatker.getInitialData();
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
            switch (menu)
            {
                case "psp": this.Enabled = false; getDaftarAset(); this.Enabled = true; break;
                case "psplain": 
                case "aspbmn":
                case "psbmn":
                    this.Enabled = false; getDaftarAsetPspLain(); this.Enabled = true; break;
                
                case "sewa": 
                case "pinjampakai":
                case "kerjasama":
                    this.Enabled = false; getDaftarAsetSewa(); this.Enabled = true; break;
                case "jual": this.Enabled = false; getDaftarAsetJual(); this.Enabled = true; break;
                case "hibah": this.Enabled = false; getDaftarAsetHibah(); this.Enabled = true; break;
                case "tukar": this.Enabled = false; getDaftarAsetTukar(); this.Enabled = true; break;
                case "pmp": this.Enabled = false; getDaftarAsetPmp(); this.Enabled = true; break;
                default:
                    break;
            }
        }
        #endregion

        private void gcAset_Click(object sender, EventArgs e)
        {

        }

        private void bbiValidasi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rowTerpilih != null)
            {
                int posisiRow = gvAset.GetFocusedDataSourceRowIndex();
                if (rowTerpilih.IsLastRow) gvAset.FocusedRowHandle = posisiRow - 1;
                else gvAset.FocusedRowHandle = posisiRow + 1;

                daftarTerpilih = null;
                daftarTerpilih = new ArrayList();
                string daftarIdAset = "";
                string daftarIdWasdalBmn = "";
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
                        switch (menu)
                        {
                            case "psp":
                                dataAsetTerpilih = (SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP)daftarTerpilih[i];
                                daftarIdAset = String.Format("{0}{1};", daftarIdAset, dataAsetTerpilih.ID_ASET);
                                daftarIdWasdalBmn = String.Format("{0}{1};", daftarIdWasdalBmn, dataAsetTerpilih.ID_SK_WASDAL_BMN);
                                break;
                            case "psplain":
                            case "aspbmn":
                            case "psbmn":
                                dataAsetTerpilihPspLain = (SvcWasdalPSPBMNLAINSelect.WASDALSROW_WASDAL_PSP_LAIN)daftarTerpilih[i];
                                daftarIdAset = String.Format("{0}{1};", daftarIdAset, dataAsetTerpilihPspLain.ID_ASET);
                                break;
                            case "sewa":
                            case "pinjampakai":
                            case "kerjasama":
                            case "kspi":
                            case "bgs_bsg":
                                dataAsetTerpilihSewa = (SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT)daftarTerpilih[i];
                                daftarIdAset = String.Format("{0}{1};", daftarIdAset, dataAsetTerpilihSewa.ID_ASET);
                                break;
                            case "jual" :
                                dataAsetTerpilihJual = (SvcWasdalJualSelect.WASDALSROW_WASDAL_PT_JUAL)daftarTerpilih[i];
                                daftarIdAset = String.Format("{0}{1};", daftarIdAset, dataAsetTerpilihJual.ID_ASET);
                                break;
                            case "tukar":
                                dataAsetTerpilihTukar = (SvcWasdalTmSelect.WASDALSROW_WASDAL_PT_TUKAR)daftarTerpilih[i];
                                daftarIdAset = String.Format("{0}{1};", daftarIdAset, dataAsetTerpilihTukar.ID_ASET);
                                break;
                            case "hibah":
                                dataAsetTerpilihHibah = (SvcWasdalHibahSelect.WASDALSROW_WASDAL_PT_HIBAH)daftarTerpilih[i];
                                daftarIdAset = String.Format("{0}{1};", daftarIdAset, dataAsetTerpilihHibah.ID_ASET);
                                break;
                            case "pmp":
                                dataAsetTerpilihPmp = (SvcWasdalPmpSelect.WASDALSROW_WASDAL_PT_MODAL)daftarTerpilih[i];
                                daftarIdAset = String.Format("{0}{1};", daftarIdAset, dataAsetTerpilihPmp.ID_ASET);
                                break;
                            case "bongkaran":
                                dataAsetTerpilihBongkaran = (SvcWasdalRSKBongkaranDtlSelect.WASDALSROW_BONGKARAN_GRID_DTL)daftarTerpilih[i];
                                daftarIdAset = String.Format("{0}{1};", daftarIdAset, dataAsetTerpilihBongkaran.ID_ASET);
                                break;
                        }
                    }
                    daftarIdAset = daftarIdAset.TrimEnd(';');
                    if (MessageBox.Show("Apakah Anda ingin memvalidasi Daftar Aset yang terpilih?", "Validasi Aset",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.simpanAsetTerpilih(daftarIdAset, 'V');
                        this.Close();
                    }
                                    
                        
                    
                }
            }
            else MessageBox.Show("Silakan pilih Data Aset terlebih dulu", konfigApp.judulKonfirmasi);
        }
    }
}
