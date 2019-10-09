using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KSK.PNBP.TMBMN
{
    public partial class ucPNBPTukarForm : UserControl
    {
        public ToggleProgressBar toggleProgressBar;
        public SimpanDataRPNBP simpanDataRkmPnbpTukar;
        public string statusForm = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public decimal? idPemohon = null;
        public string kodePenerbitSkDetail = null;
        public string namaPenerbitSkDetail = null;
        private char modeCrud = 'A';
        public SvcWasdalTukarPnbpSelect.WASDALSROW_SELECT_TUKAR_PNBP dataTerpilih;
        public SvcWasdalTukarPnbpSelectSK.WASDALSROW_READ_WASDAL_PT_TUKAR_PNBP skTerpilih;
        private bool instansiLoaded = false;
        private bool akunLoaded = false;
        public decimal? idSatker = null;
        private string noSkLama = null;
        private string noSkBaru = null;
        public string filePath = "";

        private decimal? idSkPnbpBaru = null;
        private decimal? idSkPnbpLama = null;
        public decimal? idSkPNBP = null;
        public decimal? idSkWasdal = null;
        public decimal? idTlSkWasdal = null;
        public string kd_pelayanan = "";
        public string idTlWasdal = null;

        public ucPNBPTukarForm(string _status)
        {
            InitializeComponent();
            statusForm = _status;
            daftarGambar = new Hashtable();
        }

        #region Thread
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

        #region Ambil Data Instansi
        SvcInstansiSelect.dsSelect_pttClient svcInstansi;
        SvcInstansiSelect.OutputParameters outputInstansi;

        private void getInstansi()
        {
            instansiLoaded = false;
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcInstansiSelect.InputParameters input = new SvcInstansiSelect.InputParameters();
                input.P_MINSpecified = true;
                input.P_MIN = 1;
                input.P_MAXSpecified = true;
                input.P_MAX = 5;
                input.P_COL = "";
                input.P_SORT = "";
                //input.STR_WHERE = String.Format(" KD_INSTANSI = '{0}' ", konfigApp.kodeInstansi);
                svcInstansi = new SvcInstansiSelect.dsSelect_pttClient();
                svcInstansi.Open();
                svcInstansi.Beginexecute(input, new AsyncCallback(getDataInstansi), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }

        }
        private void getDataInstansi(IAsyncResult result)
        {
            try
            {
                outputInstansi = svcInstansi.Endexecute(result);
                svcInstansi.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataInstansi(this.loadDataInstansi), outputInstansi);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataInstansi(SvcInstansiSelect.OutputParameters outputInstansi);
        
        private void loadDataInstansi(SvcInstansiSelect.OutputParameters outputInstansi)
        {
            int jmlData = outputInstansi.SF_ROW_R_INSTANSI.Count();
            if (jmlData > 0)
            {
                instansiLoaded = true;
                teNamaInstansi.Properties.DataSource = outputInstansi.SF_ROW_R_INSTANSI;
                teNamaInstansi.EditValue = outputInstansi.SF_ROW_R_INSTANSI[0].KD_INSTANSI;
                if (dataTerpilih != null)
                {
                    teNamaInstansi.Text = dataTerpilih.NM_PENERBIT_SK;
                    teNamaInstansi.EditValue = dataTerpilih.KD_PENERBIT_SK;
                }
                //if (statusForm != "C")
                //    getDaftarAset();
            }
            else instansiLoaded = false;
        }
        #endregion

        #region Ambil Data Akun
        SvcAkun.doBasSelect_pttClient svcAkun = new SvcAkun.doBasSelect_pttClient();
        SvcAkun.OutputParameters outputAkun = new SvcAkun.OutputParameters();

        private void getAkun()
        {
            akunLoaded = false;
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcAkun.InputParameters input = new SvcAkun.InputParameters();
                input.P_MINSpecified = true;
                input.P_MIN = 0;
                input.P_MAXSpecified = true;
                input.P_MAX = 0;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = " (KDMAKMAP BETWEEN '423121' AND '423127' OR KDMAKMAP LIKE '423129') OR (KDMAKMAP BETWEEN '423141' AND '423149') OR (KDMAKMAP like '42512%') OR (KDMAKMAP like '42513%')";
                svcAkun.Beginexecute(input, new AsyncCallback(getDataAkun), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }

        }
        private void getDataAkun(IAsyncResult result)
        {
            try
            {
                outputAkun = svcAkun.Endexecute(result);
                svcAkun.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataAkun(this.loadDataAkun), outputAkun);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAkun(SvcAkun.OutputParameters outputAkun);

        private void loadDataAkun(SvcAkun.OutputParameters outputAkun)
        {
            int jmlData = outputAkun.SF_ROW_T_BAS.Count();
            if (jmlData > 0)
            {
                akunLoaded = true;
                leAkun.Properties.DataSource = outputAkun.SF_ROW_T_BAS;
                leAkun.Properties.DisplayMember = "NMMAKMAP";
                leAkun.Properties.ValueMember = "KDMAKMAP";
                //leAkun.Properties.PopulateColumns();
                //leAkun.Properties.Columns.RemoveAt(0);
                //leAkun.Properties.Columns.RemoveAt(0);
                //leAkun.Properties.Columns.RemoveAt(0);
                //leAkun.Properties.Columns.RemoveAt(0);
                //leAkun.Properties.Columns.RemoveAt(0);
                //leAkun.Properties.Columns.RemoveAt(1);
                //leAkun.Properties.Columns.RemoveAt(2);
                //leAkun.Properties.Columns.RemoveAt(2);
                //leAkun.Properties.Columns.RemoveAt(1);
                ////leAkun.Properties.Columns.RemoveAt(2);

                if (dataTerpilih != null)
                {
                    leAkun.Text = dataTerpilih.UR_AKUN;
                    leAkun.EditValue = dataTerpilih.KD_AKUN;
                    //teNamaAkun.Text = leAkun.Text;
                }
                //if (statusForm != "C")
                //    getDaftarAset();
            }
            else akunLoaded = false;
        }
        #endregion


        #region Inisialisasi
        private void resetFormRtkSwBmn()
        {
            rgJenisAset.SelectedIndex = -1;
            teNomorSk.Text = "";
            teTanggalSk.EditValue = null;
            teJenisPemohon.Text = "";
            teKodePemohon.Text = "";
            teNamaPemohon.Text = "";
            teKodeKl.Text = "";
            teNamaKl.Text = "";
            //teNamaInstansi.Text = "";
            
            gcDaftarAset.DataSource = null;
            
            teFileBukti.Text = "";
            leAkun.EditValue = null;
            teKuantitas.Text = "";
            teNilaiPenetapan.Text = "";
            
        }

        public void inisialisasiForm()
        {
            DateTime tanggalSurat;
            resetFormRtkSwBmn();
            if (statusForm != "C" && statusForm != "CU")
            {
                rgJenisAset.SelectedIndex = (dataTerpilih.IS_TB == "Tanah dan Bangunan" ? 0 : 1);
                idSkPNBP = dataTerpilih.ID_WASDAL_PNBP;
                idSkWasdal = dataTerpilih.ID_SK_WASDAL;
                teNomorSk.Text = dataTerpilih.SK_KEPUTUSAN;
                noSkBaru = dataTerpilih.SK_KEPUTUSAN.Trim();
                tanggalSurat = Convert.ToDateTime(dataTerpilih.TGL_SK);
                teTanggalSk.EditValue = tanggalSurat;
                teTahunAnggaran.Text = tanggalSurat.Year.ToString();
                teJenisPemohon.Text = dataTerpilih.TIPE_PEMOHON;
                idPemohon = dataTerpilih.ID_PEMOHON;
                teKodePemohon.Text = dataTerpilih.KD_PEMOHON;
                teNamaPemohon.Text = dataTerpilih.NM_PEMOHON;
                teKodeKl.Text = dataTerpilih.KD_KL;
                teNamaKl.Text = dataTerpilih.UR_KL;
                teNamaInstansi.Text = dataTerpilih.NM_PENERBIT_SK;
                teNamaInstansi.EditValue = dataTerpilih.KD_PENERBIT_SK;
                teNilaiPenetapan.Text = Convert.ToDecimal(dataTerpilih.NILAI_PENETAPAN).ToString("n2");
                teKuantitas.Text = dataTerpilih.KUANTITAS_SK.ToString();
                teKdBilling.Text = dataTerpilih.KD_BILLING;

                teNTPN.Text = dataTerpilih.NTPN;
                deTglSetor.EditValue = Convert.ToDateTime(dataTerpilih.TGL_SETOR);
                teNTB.Text = dataTerpilih.NTB;
                deTglTransaksi.EditValue = Convert.ToDateTime(dataTerpilih.TGL_TRANSAKSI);
                teNamaPenyetor.Text = dataTerpilih.NM_PENYETOR;
                leAkun.EditValue = dataTerpilih.KD_AKUN;
                leAkun.Text = dataTerpilih.UR_AKUN;
                leAkun.ItemIndex = Convert.ToInt32(dataTerpilih.KD_AKUN);
                teNilaiPnbp.Text = dataTerpilih.NILAI_PNBP.ToString();
                teKet.Text = dataTerpilih.KET;

                teFileBukti.Text = dataTerpilih.NM_FILE;
                idTlSkWasdal = dataTerpilih.ID_TL_WASDAL_PINDAHTANGAN;
                idSatker = dataTerpilih.ID_SATKER;
                kd_pelayanan = dataTerpilih.KD_PELAYANAN;
                //if (idSkPnbpLama != idSkPnbpBaru)
                //{
                //    getDaftarAset();
                //    idSkPnbpLama = idSkPnbpBaru;
               // }
                //else
               // {
               //     gcDaftarAset.DataSource = dsGridDaftarAset;
               //     gcDaftarAset.RefreshDataSource();
               // }
                getDaftarAset();
            }
            else
            {
               
                rgJenisAset.SelectedIndex = (skTerpilih.IS_TB[0] == 'Y' ? 0 : 1);
                teNomorSk.Text = skTerpilih.SK_KEPUTUSAN;
                noSkBaru = skTerpilih.SK_KEPUTUSAN.Trim();
                tanggalSurat = Convert.ToDateTime(skTerpilih.TGL_SK);
                teTanggalSk.EditValue = tanggalSurat;
                teTahunAnggaran.Text = tanggalSurat.Year.ToString();
                teJenisPemohon.Text = skTerpilih.TIPE_PEMOHON;
                idPemohon = skTerpilih.ID_PEMOHON;
                teKodePemohon.Text = skTerpilih.KD_PEMOHON;
                teNamaPemohon.Text = skTerpilih.NM_PEMOHON;
                teKodeKl.Text = skTerpilih.KD_KL;
                teNamaKl.Text = skTerpilih.UR_KL;
                teNamaInstansi.Text = skTerpilih.NM_PENERBIT_SK;
                teNamaInstansi.EditValue = skTerpilih.KD_PENERBIT_SK;
                //teNilaiPenetapan.Text = Convert.ToDecimal(skTerpilih.NILAI_PENETAPAN).ToString("n2");
                teKuantitas.Text = skTerpilih.KUANTITAS_SK.ToString();

                idTlSkWasdal = skTerpilih.ID_TL_WASDAL_PINDAHTANGAN;
                idSatker = skTerpilih.ID_SATKER;
                kd_pelayanan = skTerpilih.KD_PELAYANAN;
            }
            if (akunLoaded == false)
            {
                getAkun();
            }
            if (instansiLoaded == false)
            {
                getInstansi();

            }
            //else
            //{
            //    if (statusForm != "C" && statusForm != "CU")
            //    {

            //    }
            //}
            rgJenisAset.Properties.ReadOnly = true;
            teNomorSk.Properties.ReadOnly = true;
            teTanggalSk.Properties.ReadOnly = true;
            teJenisPemohon.Properties.ReadOnly = true;
            teNamaInstansi.Properties.ReadOnly = true;
            sbSimpan.Enabled = true;
            sbCariPemohon.Enabled = false;
            sbTambah.Enabled = false;
            sbHapus.Enabled = false;
            sbRefresh.Enabled = false;
            gcDaftarAset.Enabled = false;
            cePilihSemua.Enabled = false;
            teKodePemohon.Properties.ReadOnly = true;
            teFileBukti.Properties.ReadOnly = false;
            
            switch (statusForm)
            {
                case "C":
                    idSkPNBP = konfigApp.getGlobalId("ID_WASDAL_PNBP");
                    idSkWasdal = skTerpilih.ID_SK_WASDAL_PINDAHTANGAN;
                    break;
                case "CU":
                    teTahunAnggaran.Text = konfigApp.tahunAnggaran.ToString();
                    teNomorSk.Focus();
                    teKodePemohon.Properties.ReadOnly = true;
                    sbHapus.Enabled = true;
                    break;
                case "U":
                    this.idSkWasdal = dataTerpilih.ID_SK_WASDAL;
                    this.idSkPNBP = dataTerpilih.ID_WASDAL_PNBP;
                    gcDaftarAset.Enabled = true;
                    rgJenisAset.Properties.ReadOnly = true;
                    teNomorSk.Properties.ReadOnly = true;
                    teTanggalSk.Properties.ReadOnly = true;
                    teJenisPemohon.Properties.ReadOnly = true;
                    teNamaInstansi.Properties.ReadOnly = true;
                    teNTPN.Properties.ReadOnly = true;
                    deTglSetor.Properties.ReadOnly = false;
                    deTglTransaksi.Properties.ReadOnly = false;
                    teNTB.Properties.ReadOnly = false;
                    teNamaInstansi.Properties.ReadOnly = false;
                    teNamaPenyetor.Properties.ReadOnly = false;
                    leAkun.Properties.ReadOnly = false;
                    teNilaiPnbp.Properties.ReadOnly = false;
                    teKet.Properties.ReadOnly = false;
                    teFileBukti.Properties.ReadOnly = false;
                    sbSimpan.Enabled = true;
                    sbCariPemohon.Enabled = false;
                    sbTambah.Enabled = true;
                    sbHapus.Enabled = true;
                    sbRefresh.Enabled = true;
                    cePilihSemua.Enabled = true;
                    break;
                case "A":
                    teKodePemohon.Properties.ReadOnly = true;
                    rgJenisAset.Properties.ReadOnly = true;
                    teNomorSk.Properties.ReadOnly = true;
                    teTanggalSk.Properties.ReadOnly = true;
                    teJenisPemohon.Properties.ReadOnly = true;
                    teNTPN.Properties.ReadOnly = true;
                    deTglSetor.Properties.ReadOnly = true;
                    deTglTransaksi.Properties.ReadOnly = true;
                    teNTB.Properties.ReadOnly = true;
                    teNamaInstansi.Properties.ReadOnly = true;
                    teNamaPenyetor.Properties.ReadOnly = true;
                    leAkun.Properties.ReadOnly = true;
                    teNilaiPnbp.Properties.ReadOnly = true;
                    teKet.Properties.ReadOnly = true;
                    teFileBukti.Properties.ReadOnly = true;
                    sbCariPemohon.Enabled = false;
                    sbSimpan.Enabled = false;
                    sbHapus.Enabled = false;
                    gcDaftarAset.Enabled = true;
                    sbRefresh.Enabled = true;
                    gridColPilih.OptionsColumn.AllowEdit = false;
                    gridColPilih.OptionsColumn.ReadOnly = true;
                    teFileBukti.Properties.ReadOnly = true;
                    sbTambah.Enabled = false;
                    break;
            }
            switch (konfigApp.levelUser)
            {
                case "KPKNL":
                    teJenisPemohon.Properties.Items.Clear();
                    teJenisPemohon.Properties.Items.Insert(0, konfigApp.levelSatker);
                    //xtpIdentitas.Text = String.Format("KPKNL: [{0}] {1}", konfigApp.kodeKpknl, konfigApp.namaKpknl);
                    break;
                case "KANWIL":
                    teJenisPemohon.Properties.Items.Clear();
                    teJenisPemohon.Properties.Items.Insert(0, konfigApp.levelSatker);
                    teJenisPemohon.Properties.Items.Insert(1, konfigApp.levelKorwil);
                    //xtpIdentitas.Text = String.Format("KANWIL: [{0}] {1}", konfigApp.kodeKanwil, konfigApp.namaKanwil);
                    break;
                case "KPDJKN":
                    teJenisPemohon.Properties.Items.Clear();
                    teJenisPemohon.Properties.Items.Insert(0, konfigApp.levelSatker);
                    teJenisPemohon.Properties.Items.Insert(1, konfigApp.levelKorwil);
                    teJenisPemohon.Properties.Items.Insert(2, konfigApp.levelEselon1);
                    teJenisPemohon.Properties.Items.Insert(3, konfigApp.levelKl);
                    //xtpIdentitas.Text = "KP DJKN: KANTOR PUSAT DJKN";
                    break;
            }
            lciLoadingGambar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        #endregion

        private void teJenisPemohon_SelectedIndexChanged(object sender, EventArgs e)
        {
            //idPemohon = null;
            teKodePemohon.Text = "";
            teNamaPemohon.Text = "";
            teKodeKl.Text = "";
            teNamaKl.Text = "";
            //lciKosong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lciKodeKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //lciNamaKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            switch (teJenisPemohon.SelectedIndex)
            {
                case 0:
                    teKodePemohon.Properties.MaxLength = 20;
                    break;
                case 1:
                    teKodePemohon.Properties.MaxLength = 9;
                    break;
                case 2:
                    teKodePemohon.Properties.MaxLength = 5;
                    break;
                case 3:
                    teKodePemohon.Properties.MaxLength = 3;
                    //lciKosong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //lciKodeKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //lciNamaKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                default:
                    teKodePemohon.Properties.MaxLength = 0;
                    //lciKosong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //lciKodeKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //lciNamaKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
            }
        }

        public string konversiPeriode()
        {
            string period="";
            //if (cbPeriode.EditValue == "Harian")
            //{
            //    period = "H";
            //}
            //else
            //if(cbPeriode.EditValue=="Mingguan")
            //{
            //    period = "M";
            //}
            //else
            //if (cbPeriode.EditValue == "Bulanan")
            //    {
            //        period = "B";
            //    }
            //else
            //    if (cbPeriode.EditValue == "Mingguan")
            //    {
            //        period = "M";
            //    }
            return period;
        }

        #region Bagian Header
        KSK.TL.PU.frmPuSatker formPuSatker;
        KSK.TL.PU.frmPuKorwil formPuKorwil;
        KSK.TL.PU.frmPuEselon1 formPuEselon1;
        KSK.TL.PU.frmPuKl formPuKl;

        private void sbCariPemohon_Click(object sender, EventArgs e)
        {
            string preWhere = "";
            switch(Convert.ToInt16(konfigApp.idGroup))
            {
                case 5:
                case 6:
                case 12:
                    preWhere = " ID_KPKNL = " + konfigApp.idKpknl;
                    break;
                case 7:
                case 8:
                case 13:
                    preWhere = " ID_KANWIL = " + konfigApp.idKanwil;
                    break;
            }

            if (teJenisPemohon.Text.Trim() != "")
            {
                if (teJenisPemohon.Text == konfigApp.levelSatker)
                {
                    if (konfigApp.levelUser == konfigApp.levelKpknl)
                    {
                        preWhere = " ID_KPKNL = " + konfigApp.idKpknl;
                    }
                    else if (konfigApp.levelUser == konfigApp.levelKanwil)
                    {
                        preWhere = " ID_KANWIL = " + konfigApp.idKanwil;
                    }
                    if (formPuSatker == null)
                    {
                        formPuSatker = new KSK.TL.PU.frmPuSatker()
                        {
                            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                            selectUnitKerja = new SelectDataUnitKerja(setUnitKerja),
                            whereAwal = preWhere
                        };
                    }
                    if (teKodePemohon.Text.Trim() != "")
                        formPuSatker.idManualBaru = teKodePemohon.Text.Trim();
                    else formPuSatker.idManualBaru = "";
                    formPuSatker.ShowDialog();
                }
                else if (teJenisPemohon.Text == konfigApp.levelKorwil)
                {
                    if (konfigApp.levelUser == konfigApp.levelKpknl)
                    {
                        preWhere = " ID_KPKNL = " + konfigApp.idKpknl;
                    }
                    else if (konfigApp.levelUser == konfigApp.levelKanwil)
                    {
                        preWhere = " a.ID_KANWIL = " + konfigApp.idKanwil;
                    }
                    if (formPuKorwil == null)
                    {
                        formPuKorwil = new KSK.TL.PU.frmPuKorwil()
                        {
                            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                            selectUnitKerja = new SelectDataUnitKerja(setUnitKerja),
                            whereAwal = preWhere
                        };
                    }
                    if (teKodePemohon.Text.Trim() != "")
                        formPuKorwil.idManualBaru = teKodePemohon.Text.Trim();
                    else formPuKorwil.idManualBaru = "";
                    formPuKorwil.ShowDialog();
                }
                else if (teJenisPemohon.Text == konfigApp.levelEselon1)
                {
                    if (konfigApp.levelUser == konfigApp.levelKpknl)
                    {
                        preWhere = " ID_KPKNL = " + konfigApp.idKpknl;
                    }
                    else if (konfigApp.levelUser == konfigApp.levelKanwil)
                    {
                        preWhere = " a.ID_KANWIL = " + konfigApp.idKanwil;
                    }
                    if (formPuEselon1 == null)
                    {
                        formPuEselon1 = new KSK.TL.PU.frmPuEselon1()
                        {
                            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                            selectUnitKerja = new SelectDataUnitKerja(setUnitKerja),
                            whereAwal = preWhere
                        };
                    }
                    if (teKodePemohon.Text.Trim() != "")
                        formPuEselon1.idManualBaru = teKodePemohon.Text.Trim();
                    else formPuEselon1.idManualBaru = "";
                    formPuEselon1.ShowDialog();
                }
                else if (teJenisPemohon.Text == konfigApp.levelKl)
                {
                    if (konfigApp.levelUser == konfigApp.levelKpknl)
                    {
                        preWhere = " ID_KPKNL = " + konfigApp.idKpknl;
                    }
                    else if (konfigApp.levelUser == konfigApp.levelKanwil)
                    {
                        preWhere = " a.ID_KANWIL = " + konfigApp.idKanwil;
                    }
                    if (formPuKl == null)
                    {
                        formPuKl = new KSK.TL.PU.frmPuKl()
                        {
                            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                            selectUnitKerja = new SelectDataUnitKerja(setUnitKerja),
                            whereAwal = preWhere
                        };
                    }
                    if (teKodePemohon.Text.Trim() != "")
                        formPuKl.idManualBaru = teKodePemohon.Text.Trim();
                    else formPuKl.idManualBaru = "";
                    formPuKl.ShowDialog();
                }
            }
            else MessageBox.Show("Anda belum memilih Jenis Pemohon", "Perhatian");
        }

        private void setUnitKerja(decimal? _idUnitKerja, string _kodeUnitKerja, string _namaUnitKerja, string _kodeKl, string _namaKl)
        {
            idPemohon = _idUnitKerja;
            if (teJenisPemohon.Text == konfigApp.levelSatker) idSatker = _idUnitKerja;
            teKodePemohon.Text = _kodeUnitKerja;
            teNamaPemohon.Text = _namaUnitKerja;
            teKodeKl.Text = _kodeKl;
            teNamaKl.Text = _namaKl;
        }

        private void sbSimpan_Click(object sender, EventArgs e)
        {
            if (teNomorSk.Text.Trim() != "" && teNTPN.Text != "" && teKet.Text != "" )
            {
                if (konfigApp.cekMinus(Convert.ToDecimal(teNilaiPnbp.Text)) == false)
                {
                    if (statusForm == "C" || statusForm == "U" || statusForm == "CU")
                        simpanDataRkmPnbpTukar(statusForm);
                    if (statusForm == "C")
                    {
                        sbTambah.Enabled = true;
                        sbHapus.Enabled = true;
                    }
                }
                else MessageBox.Show(konfigApp.teksNilaiMinus, konfigApp.judulKonfirmasi);
            }
            else MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulKonfirmasi);
        }

        private void teKodePemohon_EditValueChanged(object sender, EventArgs e)
        {
            teNamaPemohon.Text = "";
            teKodeKl.Text = "";
            teNamaKl.Text = "";
            //idPemohon = null;
        }

        #endregion

        #region Bagian Detail: Aset
        #region --++ Ambil Daftar Aset dalam SK
        SvcWasdalTukarPnbpSelectAset.OutputParameters dOutAsetSewaPnbp; 
        SvcWasdalTukarPnbpSelectAset.execute_pttClient ambilDaftarAsetPnbp;
        SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP dataAsetTerpilih;
        private ArrayList dsGridDaftarAset;

        private void getDaftarAset()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalTukarPnbpSelectAset.InputParameters parInput = new SvcWasdalTukarPnbpSelectAset.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format(" (SK_KEPUTUSAN = '{0}' AND NTPN = '{1}')", teNomorSk.Text.Trim(), teNTPN.Text);
                ambilDaftarAsetPnbp = new SvcWasdalTukarPnbpSelectAset.execute_pttClient();
                ambilDaftarAsetPnbp.Beginexecute(parInput, new AsyncCallback(resultDaftarAset), "");
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                //MessageBox.Show("Detil Aset", konfigApp.judulGagalAmbil);
            }
        }

        private void resultDaftarAset(IAsyncResult result)
        {
            try
            {
                dOutAsetSewaPnbp = ambilDaftarAsetPnbp.Endexecute(result);
                ambilDaftarAsetPnbp.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAset(loadDataDaftarAset), dOutAsetSewaPnbp);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                //MessageBox.Show("Detil Aset", konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAset(SvcWasdalTukarPnbpSelectAset.OutputParameters outputDaftarAset);

        private void loadDataDaftarAset(SvcWasdalTukarPnbpSelectAset.OutputParameters outputDaftarAset)
        {
            int jmlData = outputDaftarAset.SF_DTLSEL_WASDAL_TUKAR_PNBP.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAset.SF_DTLSEL_WASDAL_TUKAR_PNBP[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    dOutAsetSewaPnbp.SF_DTLSEL_WASDAL_TUKAR_PNBP[i].NUMSpecified = false;
                    dsGridDaftarAset.Add(dOutAsetSewaPnbp.SF_DTLSEL_WASDAL_TUKAR_PNBP[i]);
                    nilaiPersetujuan = nilaiPersetujuan + dOutAsetSewaPnbp.SF_DTLSEL_WASDAL_TUKAR_PNBP[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + dOutAsetSewaPnbp.SF_DTLSEL_WASDAL_TUKAR_PNBP[i].KUANTITAS;
                }
                gcDaftarAset.DataSource = null;
                gcDaftarAset.DataSource = dsGridDaftarAset;
                gcDaftarAset.RefreshDataSource();
            }
            teNilaiPenetapan.Text = Convert.ToDecimal(nilaiPersetujuan).ToString("n0");
            teKuantitas.Text = Convert.ToDecimal(jmlKuantitas).ToString("n0");
            noSkLama = noSkBaru;
            gvDaftarAset.BestFitColumns();
        }

        private void sbRefresh_Click(object sender, EventArgs e)
        {
            getDaftarAset();
            gvDaftarAset.BestFitColumns();
        }
        #endregion

        #region --++ Tambah Daftar Aset dalam SK
        TMBMN.frmPuAset formDaftarAsetSatker;
        TMBMN.frmPuAset formDaftarAsetKorwil;
        TMBMN.frmPuAset formDaftarAsetEselon1;
        TMBMN.frmPuAset formDaftarAsetKl;

        private void sbTambah_Click(object sender, EventArgs e)
        {
            if (teJenisPemohon.Text == konfigApp.levelSatker)
            {
                //if (formDaftarAsetSatker == null)
                //{
                    formDaftarAsetSatker = new TMBMN.frmPuAset()
                    {
                        toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                        simpanAsetTerpilih = new SimpanDaftarAsetPnbpTerpilih(simpanDaftarAset)
                    };
                //}
                formDaftarAsetSatker.isTb = rgJenisAset.EditValue.ToString();
                formDaftarAsetSatker.idPemohonBaru = idPemohon;
                formDaftarAsetSatker.idSatker = idPemohon;
                formDaftarAsetSatker.kodeSatker = teKodePemohon.Text;
                formDaftarAsetSatker.namaSatker = teNamaPemohon.Text;
                formDaftarAsetSatker.teSatker.Properties.Items.Clear();
                formDaftarAsetSatker.teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", teKodePemohon.Text, teNamaPemohon.Text));
                formDaftarAsetSatker.teSatker.SelectedIndex = 0;
                formDaftarAsetSatker.levelPenggunaBarang = konfigApp.levelSatker;
                formDaftarAsetSatker.skKeputusan = this.teNomorSk.Text;
                formDaftarAsetSatker.nilaiPnbp = Convert.ToDecimal(this.teNilaiPnbp.Text);
                formDaftarAsetSatker.ntpn = this.teNTPN.Text;
                formDaftarAsetSatker.idTLWasdal = (dataTerpilih == null) ? skTerpilih.ID_TL_WASDAL_PINDAHTANGAN.ToString() : dataTerpilih.ID_TL_WASDAL_PINDAHTANGAN.ToString();
                formDaftarAsetSatker.ShowDialog();
                //sbRefresh.PerformClick();
            }
            //else if (teJenisPemohon.Text == konfigApp.levelKorwil)
            //{
            //    if (formDaftarAsetKorwil == null)
            //    {
            //        formDaftarAsetKorwil = new PU.frmPuAset()
            //        {
            //            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
            //            simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset)
            //        };
            //    }
            //    formDaftarAsetKorwil.levelPenggunaBarang = konfigApp.levelKorwil;
            //    formDaftarAsetKorwil.idPemohonBaru = idPemohon;
            //    formDaftarAsetKorwil.ShowDialog();
            //}
            //else if (teJenisPemohon.Text == konfigApp.levelEselon1)
            //{
            //    if (formDaftarAsetEselon1 == null)
            //    {
            //        formDaftarAsetEselon1 = new PU.frmPuAset()
            //        {
            //            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
            //            simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset)
            //        };
            //    }
            //    formDaftarAsetEselon1.levelPenggunaBarang = konfigApp.levelEselon1;
            //    formDaftarAsetEselon1.idPemohonBaru = idPemohon;
            //    formDaftarAsetEselon1.ShowDialog();
            //}
            //else if (teJenisPemohon.Text == konfigApp.levelKl)
            //{
            //    if (formDaftarAsetKl == null)
            //    {
            //        formDaftarAsetKl = new PU.frmPuAset()
            //        {
            //            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
            //            simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset)
            //        };
            //    }
            //    formDaftarAsetKl.levelPenggunaBarang = konfigApp.levelKl;
            //    formDaftarAsetKl.idPemohonBaru = idPemohon;
            //    formDaftarAsetKl.ShowDialog();
            //}
        }
        #endregion

        #region --++ Simpan Daftar Aset Terpilih
        SvcWasdalTukarPnbpDetilCud.OutputParameters dOutSimpanAset;
        SvcWasdalTukarPnbpDetilCud.execute_pttClient simpanDftrAset;
        private char modeCudAset = 'A';
        private decimal? nilaiPenetapanLama = null;
        private decimal? jmlKuantitasLama = null;

        private void simpanDaftarAset(string _daftarIdAset,decimal? _nilaiTotalAset, char _modeCudAset)
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalTukarPnbpDetilCud.InputParameters parInp = new SvcWasdalTukarPnbpDetilCud.InputParameters();
                parInp.P_SK_KEPUTUSAN = teNomorSk.Text.Trim();
                parInp.P_ID_ASET = _daftarIdAset;
                parInp.P_NILAI_PNBPSpecified = true;
                parInp.P_NILAI_PNBP = Convert.ToDecimal(teNilaiPnbp.Text);
                parInp.P_ID_SK_WASDAL_PINDAHTANGAN = this.idSkWasdal;
                parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                parInp.P_ID_WASDAL_PNBP = this.idSkPNBP;
                parInp.P_ID_WASDAL_PNBPSpecified = true;
               
                if (Convert.ToString(_modeCudAset) == "C")
                {
                    parInp.P_ID_TL_WASDAL_PINDAHTANGAN = (dataTerpilih == null) ? skTerpilih.ID_TL_WASDAL_PINDAHTANGAN : dataTerpilih.ID_TL_WASDAL_PINDAHTANGAN;
                    parInp.P_ID_TL_WASDAL_PINDAHTANGANSpecified = true;
                    parInp.P_NILAI_TOTAL_ASETSpecified = true;
                    parInp.P_NILAI_TOTAL_ASET = _nilaiTotalAset;
                    parInp.P_NTPN = teNTPN.Text;
                }
                else if (Convert.ToString(_modeCudAset) == "D")
                {
                    parInp.P_ID_TL_WASDAL_PINDAHTANGAN = (dataTerpilih == null) ? skTerpilih.ID_TL_WASDAL_PINDAHTANGAN : dataTerpilih.ID_TL_WASDAL_PINDAHTANGAN;
                    parInp.P_ID_TL_WASDAL_PINDAHTANGANSpecified = true;
                    parInp.P_NILAI_TOTAL_ASETSpecified = false;
                    parInp.P_NTPN = teNTPN.Text;
                }
                modeCudAset = _modeCudAset;
                parInp.P_SELECT = Convert.ToString(modeCudAset);
                
                simpanDftrAset = new SvcWasdalTukarPnbpDetilCud.execute_pttClient();
                //simpanDftrAset.Open();
                simpanDftrAset.Beginexecute(parInp, new AsyncCallback(goSimpanDftrAset), "");
            }
            catch
            {
                modeCudAset = 'A';
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulGagalLain);
            }
        }
        #endregion

        #region --++ CUD Daftar Aset
        ArrayList daftarAsetSalinan;

        private void goSimpanDftrAset(IAsyncResult result)
        {
            try
            {
                dOutSimpanAset = simpanDftrAset.Endexecute(result);
                simpanDftrAset.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsDaftarAset(dsDaftarAset), dOutSimpanAset);
            }
            catch (Exception e)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                switch (modeCudAset)
                {
                    case 'C':
                    case 'U':
                        konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                        break;
                    case 'D':
                        konfigApp.teksDialog = konfigApp.teksGagalHapus;
                        break;
                    default:
                        konfigApp.teksDialog = konfigApp.teksGagalLain;
                        break;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulKonfirmasi);
            }
        }

        private delegate void DsDaftarAset(SvcWasdalTukarPnbpDetilCud.OutputParameters dataOut);

        private void dsDaftarAset(SvcWasdalTukarPnbpDetilCud.OutputParameters dataOut)
        {
            if (dataOut.PO_RESULT == "Y")
            {
                decimal? _krgNilTetap = null;
                decimal? _krgKuantitas = null;
                if (dsGridDaftarAset == null) dsGridDaftarAset = new ArrayList();
                switch (modeCudAset)
                {
                    case 'C':
                        string _kodeSatker = "";
                        string _namaSatker = "";
                        if (teJenisPemohon.Text == konfigApp.levelSatker)
                        {
                            daftarAsetSalinan = formDaftarAsetSatker.daftarTerpilih;
                            _kodeSatker = formDaftarAsetSatker.kodeSatker;
                            _namaSatker = formDaftarAsetSatker.namaSatker;
                        }
                        else if (teJenisPemohon.Text == konfigApp.levelKorwil)
                        {
                            daftarAsetSalinan = formDaftarAsetKorwil.daftarTerpilih;
                            _kodeSatker = formDaftarAsetKorwil.kodeSatker;
                            _namaSatker = formDaftarAsetKorwil.namaSatker;
                        }
                        else if (teJenisPemohon.Text == konfigApp.levelEselon1)
                        {
                            daftarAsetSalinan = formDaftarAsetEselon1.daftarTerpilih;
                            _kodeSatker = formDaftarAsetEselon1.kodeSatker;
                            _namaSatker = formDaftarAsetEselon1.namaSatker;
                        }
                        else if (teJenisPemohon.Text == konfigApp.levelKl)
                        {
                            daftarAsetSalinan = formDaftarAsetKl.daftarTerpilih;
                            _kodeSatker = formDaftarAsetKl.kodeSatker;
                            _namaSatker = formDaftarAsetKl.namaSatker;
                        }
                        SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP dataPenyama;
                        _krgNilTetap = (teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(teNilaiPenetapan.Text));
                        _krgKuantitas = (teKuantitas.Text == "" ? 0 : Convert.ToDecimal(teKuantitas.Text));
                        for (int i = 0; i < daftarAsetSalinan.Count; i++)
                        {
                            SvcWasdalTukarPnbpAset.WASDALSROW_ASET_WASDAL_TUKAR_PNBP daftarAsetTerpilih = (SvcWasdalTukarPnbpAset.WASDALSROW_ASET_WASDAL_TUKAR_PNBP)daftarAsetSalinan[i];
                            dataPenyama = null;
                            dataPenyama = new SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP();
                            dataPenyama.ID_ASET = daftarAsetTerpilih.ID_ASET;
                            dataPenyama.ID_ASETSpecified = true;
                            dataPenyama.ID_SATKER = idSatker;
                            dataPenyama.ID_SATKERSpecified = true;
                            dataPenyama.KD_BRG = daftarAsetTerpilih.KD_BRG;
                            dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                            dataPenyama.KD_SATKER = _kodeSatker;
                            dataPenyama.KUANTITAS = daftarAsetTerpilih.KUANTITAS;
                            dataPenyama.KUANTITASSpecified = true;
                            dataPenyama.NILAI_PERSETUJUANSpecified = daftarAsetTerpilih.NILAI_PERSETUJUANSpecified;
                            dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                            dataPenyama.NO_ASET = daftarAsetTerpilih.NO_ASET;
                            dataPenyama.NO_ASETSpecified = true;
                            dataPenyama.NUM = (dsGridDaftarAset == null ? 1 : dsGridDaftarAset.Count + 1);
                            dataPenyama.NUMSpecified = false;
                            dataPenyama.SK_KEPUTUSAN = teNomorSk.Text;
                            dataPenyama.TGL_SETOR = Convert.ToDateTime(deTglSetor.EditValue);
                            dataPenyama.TGL_SETORSpecified = true;
                            dataPenyama.NILAI_PNBPSpecified = true;
                            dataPenyama.NILAI_PNBP = daftarAsetTerpilih.NILAI_PNBP;
                            dataPenyama.TOTAL_DATA = daftarAsetTerpilih.TOTAL_DATA;
                            dataPenyama.TOTAL_DATASpecified = daftarAsetTerpilih.TOTAL_DATASpecified;
                            dataPenyama.UR_SATKER = _namaSatker;
                            dataPenyama.UR_SSKEL = daftarAsetTerpilih.UR_SSKEL;
                            dsGridDaftarAset.Add(dataPenyama);
                        }
                        teNilaiPenetapan.Text = (Convert.ToDecimal(_krgNilTetap)).ToString("n0");
                        teKuantitas.Text = Convert.ToDecimal(_krgKuantitas).ToString("n0");
                        break;
                    case 'D':
                        if (daftarTerpilih != null)
                        {
                            _krgNilTetap = (teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(teNilaiPenetapan.Text));
                            _krgKuantitas = (teKuantitas.Text == "" ? 0 : Convert.ToDecimal(teKuantitas.Text));
                            for (int i = 0; i < daftarTerpilih.Count; i++)
                            {
                                SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP daftarAsetTerpilih = (SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP)daftarTerpilih[i];
                                dsGridDaftarAset.Remove(daftarAsetTerpilih);
                            }
                            teNilaiPenetapan.Text = Convert.ToDecimal(_krgNilTetap).ToString("n0");
                            teKuantitas.Text = Convert.ToDecimal(_krgKuantitas).ToString("n0");
                        }
                        break;
                    case 'U':
                        int _indeksData = dsGridDaftarAset.IndexOf(dataAsetTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dataPenyama = null;
                        dataPenyama = new SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP();
                        dataPenyama.ID_ASET = dataAsetTerpilih.ID_ASET;
                        dataPenyama.ID_ASETSpecified = dataAsetTerpilih.ID_ASETSpecified;
                        dataPenyama.ID_SATKER = dataAsetTerpilih.ID_SATKER;
                        dataPenyama.ID_SATKERSpecified = dataAsetTerpilih.ID_SATKERSpecified;
                        dataPenyama.KD_BRG = dataAsetTerpilih.KD_BRG;
                        dataPenyama.KD_PELAYANAN = dataAsetTerpilih.KD_PELAYANAN;
                        dataPenyama.KD_SATKER = dataAsetTerpilih.KD_SATKER;
                        dataPenyama.KUANTITAS = null;//Convert.ToDecimal(formPuPenetapan.teKuantitas.Text);
                        dataPenyama.KUANTITASSpecified = dataAsetTerpilih.KUANTITASSpecified;
                        dataPenyama.NILAI_PERSETUJUAN = null;//Convert.ToDecimal(formPuPenetapan.teNilaiPersetujuan.Text);
                        dataPenyama.NILAI_PERSETUJUANSpecified = dataAsetTerpilih.NILAI_PERSETUJUANSpecified;
                        dataPenyama.NM_PELAYANAN = dataAsetTerpilih.NM_PELAYANAN;
                        dataPenyama.NO_ASET = dataAsetTerpilih.NO_ASET;
                        dataPenyama.NO_ASETSpecified = dataAsetTerpilih.NO_ASETSpecified;
                        dataPenyama.NUM = dataAsetTerpilih.NUM;
                        dataPenyama.NUMSpecified = dataAsetTerpilih.NUMSpecified;
                        dataPenyama.SK_KEPUTUSAN = dataAsetTerpilih.SK_KEPUTUSAN;
                        dataPenyama.TOTAL_DATA = dataAsetTerpilih.TOTAL_DATA;
                        dataPenyama.TOTAL_DATASpecified = dataAsetTerpilih.TOTAL_DATASpecified;
                        dataPenyama.UR_SATKER = dataAsetTerpilih.UR_SATKER;
                        dataPenyama.UR_SSKEL = dataAsetTerpilih.UR_SSKEL;
                        nilaiPenetapanLama = dataAsetTerpilih.NILAI_PERSETUJUAN;
                        jmlKuantitasLama = dataAsetTerpilih.KUANTITAS;
                        dsGridDaftarAset.Remove(dataAsetTerpilih);
                        dsGridDaftarAset.Insert(_indeksData, dataPenyama);
                        break;
                }
                gcDaftarAset.DataSource = dsGridDaftarAset;
                gcDaftarAset.RefreshDataSource();
            }
            else MessageBox.Show(dataOut.PO_RESULT_MESSAGE, konfigApp.judulGagal);
        }
        #endregion

        #region --++ Hapus Daftar Aset
        private void cePilihSemua_CheckedChanged(object sender, EventArgs e)
        {
            if (cePilihSemua.Checked == true)
            {
                for (int i = 0; i < gvDaftarAset.RowCount; i++)
                {
                    rowTerpilih.FocusedRowHandle = i;
                    gvDaftarAset.SetFocusedRowCellValue(gridColPilih, true);
                }
            }
            else
            {
                for (int i = 0; i < gvDaftarAset.RowCount; i++)
                {
                    rowTerpilih.FocusedRowHandle = i;
                    gvDaftarAset.SetFocusedRowCellValue(gridColPilih, false);
                }
            }
        }

        ArrayList daftarTerpilih = null;

        private void sbHapus_Click(object sender, EventArgs e)
        {
            SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP daftarAsetTerpilih;
            if (rowTerpilih != null)
            {
                int posisiRow = gvDaftarAset.GetFocusedDataSourceRowIndex();
                if (rowTerpilih.IsLastRow) gvDaftarAset.FocusedRowHandle = posisiRow - 1;
                else gvDaftarAset.FocusedRowHandle = posisiRow + 1;

                daftarTerpilih = null;
                daftarTerpilih = new ArrayList();
                string daftarIdAset = "";
                for (int i = 0; i < gvDaftarAset.RowCount; i++)
                {
                    string status = Convert.ToString(gvDaftarAset.GetRowCellValue(i, "NUMSpecified"));
                    if (status == "True")
                    {
                        daftarTerpilih.Add(gvDaftarAset.GetRow(i));
                    }
                }
                if (daftarTerpilih.Count == 0) MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
                else
                {
                    for (int i = 0; i < daftarTerpilih.Count; i++)
                    {
                        daftarAsetTerpilih = (SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP)daftarTerpilih[i];
                        daftarIdAset = String.Format("{0}{1};", daftarIdAset, daftarAsetTerpilih.ID_ASET);
                    }
                    daftarIdAset = daftarIdAset.TrimEnd(';');
                    if (MessageBox.Show("Apakah Anda ingin menghapus Daftar Aset yang terpilih?", konfigApp.judulHapusData,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        simpanDaftarAset(daftarIdAset,null, 'D');//d
                    }
                }
                sbRefresh.PerformClick();
            }
            else MessageBox.Show("Silakan pilih Data Aset terlebih dulu", konfigApp.judulKonfirmasi);
        }
        #endregion

        #region --++ Ambil Data Foto Aset
        public void setProgressBarImage(DevExpress.XtraLayout.Utils.LayoutVisibility str)
        {
            if (this.InvokeRequired)
            {
                SetProgressBarImage p = new SetProgressBarImage(setProgressBarImage);
                this.Invoke(p, new object[] { str });
            }
            else
            {
                this.lciLoadingGambar.Visibility = str;
            }
        }

        public delegate void SetProgressBarImage(DevExpress.XtraLayout.Utils.LayoutVisibility str);
        
        public void showProgressImage()
        {
            if (this.IsHandleCreated)
            {
                this.setProgressBarImage(DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            }
        }

        private void gvDaftarAset_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            rowTerpilih = sender as GridView;
            if (rowTerpilih.SelectedRowsCount > 0)
            {
                dataAsetTerpilih = (SvcWasdalTukarPnbpSelectAset.WASDALSROW_DTLSEL_TUKAR_PNBP)rowTerpilih.GetRow(e.FocusedRowHandle);
                if (dataAsetTerpilih != null)
                {
                    if (daftarGambar.ContainsKey(dataAsetTerpilih.ID_ASET))
                    {
                        namaGambar = (byte[])daftarGambar[dataAsetTerpilih.ID_ASET];
                        peGambarAset.Image = konfigApp.convert2bytmap(namaGambar);
                    }
                    else
                    {
                        gantiGambar = true;
                        peGambarAset.Image = null;
                    }
                }
            }
        }

        private void peGambarAset_Click(object sender, EventArgs e)
        {
            if (dataAsetTerpilih != null)
            {
                if (daftarGambar.ContainsKey(dataAsetTerpilih.ID_ASET))
                {
                    namaGambar = (byte[])daftarGambar[dataAsetTerpilih.ID_ASET];
                    peGambarAset.Image = konfigApp.convert2bytmap(namaGambar);
                }
                else
                {
                    this.getFotoAset(dataAsetTerpilih.ID_ASET);
                    this.gantiGambar = false;
                }
            }
        }

        SvcAsetPhotoSelect.OutputParameters dOutFoto;
        SvcAsetPhotoSelect.call_pttClient ambilFoto;
        private Hashtable daftarGambar;
        private byte[] namaGambar;
        private bool gantiGambar = false;

        private void getFotoAset(decimal? _idAset)
        {
            try
            {
                myThread = new Thread(new ThreadStart(showProgressImage));
                myThread.Start();
                SvcAsetPhotoSelect.InputParameters inputData = new SvcAsetPhotoSelect.InputParameters();
                inputData.P_MINSpecified = true;
                inputData.P_MIN = konfigApp.currentMin;
                inputData.P_MAXSpecified = true;
                inputData.P_MAX = konfigApp.currentMaks;
                inputData.STR_WHERE = " ID_ASET = " + _idAset;
                ambilFoto = new SvcAsetPhotoSelect.call_pttClient();
                ambilFoto.Open();
                ambilFoto.Beginexecute(inputData, new AsyncCallback(getDataFotoAset), null);
            }
            catch
            {
                this.Invoke(new SetProgressBarImage(this.setProgressBarImage), DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulGagalLain);
            }
        }

        private void getDataFotoAset(IAsyncResult result)
        {
            try
            {
                dOutFoto = ambilFoto.Endexecute(result);
                ambilFoto.Close();
                this.Invoke(new SetProgressBarImage(this.setProgressBarImage), DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                this.Invoke(new LoadFotoAset(this.loadFotoAset), dOutFoto);
            }
            catch
            {
                this.Invoke(new SetProgressBarImage(this.setProgressBarImage), DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadFotoAset(SvcAsetPhotoSelect.OutputParameters dataOutFotoAset);
        
        private void loadFotoAset(SvcAsetPhotoSelect.OutputParameters dataOutFotoAset)
        {
            peGambarAset.Image = null;
            int jmlGambar = dataOutFotoAset.SF_ROW_M_ASET_PHOTO.Count();
            if (jmlGambar > 0)
            {
                byte[] gbr = dataOutFotoAset.SF_ROW_M_ASET_PHOTO[jmlGambar - 1].PHOTO;
                if (gbr.Length > 0)
                {
                    peGambarAset.Image = konfigApp.convert2bytmap(dataOutFotoAset.SF_ROW_M_ASET_PHOTO[jmlGambar - 1].PHOTO);
                    namaGambar = dataOutFotoAset.SF_ROW_M_ASET_PHOTO[jmlGambar - 1].PHOTO;
                    daftarGambar.Add(dataAsetTerpilih.ID_ASET, namaGambar);
                }
            }
        }
        #endregion

        #region --++ Edit Penetapan Aset
        //PU.frmPuPenetapan formPuPenetapan;

        private void formUbahPenetapan()
        {
            //if (statusForm != "A")
            //{
            //    if (formPuPenetapan == null)
            //    {
            //        formPuPenetapan = new PU.frmPuPenetapan() { simpanDataPenetapan = new SimpanDaftarAsetTerpilih(simpanDaftarAset) };
            //    }
            //    formPuPenetapan.idAset = dataAsetTerpilih.ID_ASET;
            //    formPuPenetapan.teKodeBarang.Text = dataAsetTerpilih.KD_BRG;
            //    formPuPenetapan.teNamaBarang.Text = dataAsetTerpilih.UR_SSKEL;
            //    formPuPenetapan.teNup.Text = dataAsetTerpilih.NO_ASET.ToString();
            //    formPuPenetapan.teNilaiPersetujuan.Text = (Convert.ToDecimal(dataAsetTerpilih.NILAI_PERSETUJUAN)).ToString("n0");
            //    formPuPenetapan.teKuantitas.Text = (Convert.ToDecimal(dataAsetTerpilih.KUANTITAS)).ToString("n0");
            //    formPuPenetapan.kodeStatus = dataAsetTerpilih.KD_STATUS;
            //    formPuPenetapan.teStatus.Text = dataAsetTerpilih.UR_STATUS;
            //    formPuPenetapan.lciStatusBmn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    formPuPenetapan.tePenggunaanBmn.Text = dataAsetTerpilih.GUNA_WASDAL;
            //    formPuPenetapan.ShowDialog();
            //}
        }
        private void gvDaftarAset_DoubleClick(object sender, EventArgs e)
        {
            formUbahPenetapan();
        }
        private void gvDaftarAset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                formUbahPenetapan();
            }
        }
        #endregion

        private void btnUploadFileBukti_Click(object sender, EventArgs e)
        {
            uploadSelect = "CE";
            initDok(uploadSelect);
        }

        #endregion

        #region FILE (Upload / View)
        string uploadSelect = "CE";
        SvcPnbpGetFile.execute_pttClient getFileWasdal;
        SvcPnbpGetFile.OutputParameters outFileWasdal;
        private void btnViewDok_Click(object sender, EventArgs e)
        {
            uploadSelect = "R";
            initDok(uploadSelect);
        }


        private void initDok(string select)
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcPnbpGetFile.InputParameters parInp = new SvcPnbpGetFile.InputParameters();
                parInp.P_SELECT = select;
                parInp.P_SK_KEPUTUSAN = teNomorSk.Text;
                if (uploadSelect.Equals("CE"))
                {
                    parInp.P_FILE_DOK =  konfigApp.FileToByteArray(filePath);
                    parInp.P_NM_FILE = teFileBukti.Text;
                }
                parInp.P_NTPN = teNTPN.Text;
                parInp.P_ID_WASDAL_PNBP = idSkPNBP;
                parInp.P_ID_WASDAL_PNBPSpecified = true;
                getFileWasdal = new SvcPnbpGetFile.execute_pttClient();
                getFileWasdal.Beginexecute(parInp, new AsyncCallback(getResultFileWasdal), "");
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
            }
        }


        private void getResultFileWasdal(IAsyncResult result)
        {
            try
            {
                outFileWasdal = getFileWasdal.Endexecute(result);
                getFileWasdal.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsFileWasdal(dsFileWasdal), outFileWasdal);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
            }
        }

        private delegate void DsFileWasdal(SvcPnbpGetFile.OutputParameters outFileWasdal);
        private void dsFileWasdal(SvcPnbpGetFile.OutputParameters outFileWasdal)
        {
            if (outFileWasdal.PO_RESULT == "Y")
            {
                if (uploadSelect.Equals("CE"))
                {
                    AutoClosingMessageBox.Show("Upload Dokumen Berhasil", "Perhatian", 1000);
                }
                else
                {
                    KSK.TL.PU.FrmPuViewPdf FrmPuViewPdf = new KSK.TL.PU.FrmPuViewPdf();
                    string appPath = AppDomain.CurrentDomain.BaseDirectory;
                    System.IO.File.WriteAllBytes(System.IO.Path.Combine(appPath, "DOK_BUKTI_" + outFileWasdal.PO_NM_FILE.Replace('/', '-') + ".pdf"), outFileWasdal.PO_FILE_PNBP);
                    FrmPuViewPdf.displayFile(System.IO.Path.Combine(appPath, "DOK_BUKTI_" + outFileWasdal.PO_NM_FILE.Replace('/', '-') + ".pdf"));
                    FrmPuViewPdf.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(outFileWasdal.PO_RESULT_MESSAGE);
            }
        }
        #endregion

        private void sbPilih_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdDok = new OpenFileDialog();
            ofdDok.Title = "Pilih File";
            ofdDok.FileName = "File PDF";
            ofdDok.Filter = "(File PDF)|*.pdf";
            ofdDok.Multiselect = false;

            if (ofdDok.ShowDialog() == DialogResult.OK && ofdDok.FileName != "File PDF")
            {
                filePath = ofdDok.FileName;
                teFileBukti.Text = ofdDok.SafeFileName;

            }
        }
    }
}
