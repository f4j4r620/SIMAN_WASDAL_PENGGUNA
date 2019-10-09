using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KSK.TL.BONGKARAN  
{
    public partial class ucRTLBONGKARANForm : UserControl
    {
        public ToggleProgressBar toggleProgressBar;
        public SimpanDataRtl simpanDataRtlBONGKARAN;   
        public string statusForm = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public decimal? idPemohon = null;
        public string kodePenerbitSkDetail = null;
        public string namaPenerbitSkDetail = null;
        private char modeCrud = 'A';
        public SvcTLBongkaranSelect.WASDALSROW_TL_BONGKARAN_GRID dataTerpilih;
        public SvcWasdalRSKBongkaranSkSelect.WASDALSROW_SK_BONGKARAN_GRID skTerpilih;
        private bool instansiLoaded = false;
        private bool jnsMitraLoaded = false;
        private decimal? idSatker = null;
        private string noSkLama = null;
        private string noSkBaru = null;
        public string filePath = "";
        public decimal? idSkTL = null;
        public decimal? idSkWasdal = null;
        public string nmFileTL = null;

        public ucRTLBONGKARANForm(string _status)
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
               // MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                MessageBox.Show("Gagal mengambil data instansi");
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
              //  MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                MessageBox.Show("Gagal mengambil data instansi");
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
                if (statusForm != "C")
                    getDaftarAset();
            }
            else instansiLoaded = false;
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
            cbJnsDokLaksana.SelectedText = "";
            teKuantitas.Text = "";
            teNilaiPenetapan.Text = "";
            
        }

        public int jnsBuktiLaksana()
        {
            int jnsBuktiLaksana = cbJnsDokLaksana.SelectedIndex = 0; 
            switch (dataTerpilih.JNS_BUKTI_LAKSANA)  
            {
                case "Risalah Lelang":
                    cbJnsDokLaksana.SelectedIndex = 0;
                    break;
                case "Berita Acara Serah Terima":
                    cbJnsDokLaksana.SelectedIndex = 1;
                    break;
                case "SK Penghapusan":
                    cbJnsDokLaksana.SelectedIndex = 2;
                    break;
                case "Dokumen Lainnya":
                    cbJnsDokLaksana.SelectedIndex = 3;
                    break;
                default:
                    cbJnsDokLaksana.SelectedIndex = 0;;
                    break;
            }
            return jnsBuktiLaksana = cbJnsDokLaksana.SelectedIndex;
        }

        public string jnsBuktiStr(int jns)
        {
            string jnsBuktiStr = "";
            switch (jns)
            {
                case 0:
                    jnsBuktiStr = "Risalah Lelang";
                    break;
                case 1:
                    jnsBuktiStr = "Berita Acara Serah Terima";
                    break;
                case 2:
                    jnsBuktiStr = "SK Penghapusan";
                    break;
                case 3:
                    jnsBuktiStr = "Dokumen Lainnya";
                    break;
                default:
                    jnsBuktiStr = "Keputusan Pelaksanaan";
                    break;
            }
            return jnsBuktiStr;
        }

        public void inisialisasiForm()
        {
            DateTime tanggalSurat;
            resetFormRtkSwBmn();
            if (statusForm != "C" && statusForm != "CU")
            {
                rgJenisAset.SelectedIndex = (dataTerpilih.IS_TB == "Tanah dan Bangunan") ? 0 : 1;
                idSkTL = dataTerpilih.ID_TL_WASDAL_BNK;
                idSkWasdal = dataTerpilih.ID_SK_WASDAL_PINDAHTANGAN;
                teNomorSk.Text = dataTerpilih.SK_KEPUTUSAN;
                noSkBaru = dataTerpilih.SK_KEPUTUSAN.Trim();
                tanggalSurat = Convert.ToDateTime(dataTerpilih.TGL_SK);
                teTanggalSk.EditValue = tanggalSurat;
                teTahunAnggaran.Text = dataTerpilih.THN_ANG;
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

                teNoBuktiLaksana.Text = dataTerpilih.NO_BUKTI_LAKSANA;
                deTglBuktiLaksana.EditValue = Convert.ToDateTime(dataTerpilih.TGL_BUKTI_LAKSANA);
                cbJnsDokLaksana.SelectedIndex = jnsBuktiLaksana();
                teKetBukti.Text = dataTerpilih.KET;
                teFileBukti.Text = dataTerpilih.NM_FILE;
                //if (noSkLama != noSkBaru)
                //    getDaftarAset();
                //else
                //{
                //    gcDaftarAset.DataSource = dsGridDaftarAset;
                //    gcDaftarAset.RefreshDataSource();
                //}
                getDaftarAset();
            }
            else
            {
                rgJenisAset.SelectedIndex = (skTerpilih.IS_TB == "Y") ? 0 : 1;
                teNomorSk.Text = skTerpilih.SK_KEPUTUSAN_BNK;
                noSkBaru = skTerpilih.SK_KEPUTUSAN_BNK.Trim();
                tanggalSurat = Convert.ToDateTime(skTerpilih.TGL_SK_BNK);
                teTanggalSk.EditValue = tanggalSurat;
                teTahunAnggaran.Text = (dataTerpilih == null ? konfigApp.tahunAnggaran.ToString() : dataTerpilih.THN_ANG);
                teJenisPemohon.Text = skTerpilih.TIPE_PEMOHON;
                idPemohon = skTerpilih.ID_PEMOHON;
                teKodePemohon.Text = skTerpilih.KD_PEMOHON;
                teNamaPemohon.Text = skTerpilih.NM_PEMOHON;
                teKodeKl.Text = skTerpilih.KD_KL;
                teNamaKl.Text = skTerpilih.UR_KL;
                teNamaInstansi.Text = skTerpilih.NM_PENERBIT_SK;
                teNamaInstansi.EditValue = skTerpilih.KD_PENERBIT_SK;
                teNilaiPenetapan.Text = Convert.ToDecimal(skTerpilih.NILAI_PENETAPAN).ToString("n2");
                teKuantitas.Text = skTerpilih.KUANTITAS_SK.ToString();
                //getDaftarAset();
            }
            if (instansiLoaded == false)
                getInstansi();
            else
            {
                if (statusForm != "C" && statusForm != "CU")
                {
                    
                }
            }
            rgJenisAset.Properties.ReadOnly = true;
            teNomorSk.Properties.ReadOnly = false;
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
                    idSkWasdal = skTerpilih.ID_SK_BNK;
                    break;
                case "CU":
                    //teTahunAnggaran.Text = konfigApp.tahunAnggaran.ToString();
                    teNomorSk.Focus();
                    teKodePemohon.Properties.ReadOnly = false;
                    break;
                case "U":
                    idSkTL = dataTerpilih.ID_TL_WASDAL_BNK;
                    idSkWasdal = dataTerpilih.ID_SK_WASDAL_PINDAHTANGAN;
                    gcDaftarAset.Enabled = true;
                    rgJenisAset.Properties.ReadOnly = true;
                    teNomorSk.Properties.ReadOnly = true;
                    teTanggalSk.Properties.ReadOnly = true;
                    teJenisPemohon.Properties.ReadOnly = true;
                    teNamaInstansi.Properties.ReadOnly = true;
                    teFileBukti.Properties.ReadOnly = false;
                    teNoBuktiLaksana.Properties.ReadOnly = true;
                    deTglBuktiLaksana.Properties.ReadOnly = false;
                    cbJnsDokLaksana.Properties.ReadOnly = false;
                    teKetBukti.Properties.ReadOnly = false;
                    sbCariPemohon.Enabled = false;
                    sbTambah.Enabled = true;
                    sbHapus.Enabled = true;
                    sbRefresh.Enabled = true;
                    cePilihSemua.Enabled = true;
                    break;
                case "A":
                    rgJenisAset.Properties.ReadOnly = true;
                    teNomorSk.Properties.ReadOnly = true;
                    teTanggalSk.Properties.ReadOnly = true;
                    teJenisPemohon.Properties.ReadOnly = true;
                    teNamaInstansi.Properties.ReadOnly = true;
                    teFileBukti.Properties.ReadOnly = true;
                    teNoBuktiLaksana.Properties.ReadOnly = true;
                    deTglBuktiLaksana.Properties.ReadOnly = true;
                    cbJnsDokLaksana.Properties.ReadOnly = true;
                    teKetBukti.Properties.ReadOnly = true;
                    sbTambah.Enabled = false;
                    sbCariPemohon.Enabled = false;
                    sbSimpan.Enabled = false;
                    sbHapus.Enabled = false;
                    gcDaftarAset.Enabled = true;
                    sbRefresh.Enabled = true;
                    gridColPilih.OptionsColumn.AllowEdit = false;
                    gridColPilih.OptionsColumn.ReadOnly = true;
                    teFileBukti.Properties.ReadOnly = true;
                    
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

        #region Bagian Header
        PU.frmPuSatker formPuSatker;
        PU.frmPuKorwil formPuKorwil;
        PU.frmPuEselon1 formPuEselon1;
        PU.frmPuKl formPuKl;

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
                        formPuSatker = new PU.frmPuSatker()
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
                        formPuKorwil = new PU.frmPuKorwil()
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
                        formPuEselon1 = new PU.frmPuEselon1()
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
                        formPuKl = new PU.frmPuKl()
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
            if (cbJnsDokLaksana.SelectedIndex != -1 && teNomorSk.Text.Trim() != "" && teNoBuktiLaksana.Text != "" && teKetBukti.Text != "" && filePath != null && teFileBukti.Text.Trim() != "")
            {
                if (statusForm == "C" || statusForm == "U" || statusForm == "CU")
                {
                    try
                    {
                        if (statusForm == "C")
                        {
                            idSkTL = konfigApp.getGlobalId("ID_TL_WASDAL_BNK");
                        }
                        simpanDataRtlBONGKARAN(statusForm);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
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
        SvcTLBongkaranDtlSelect.OutputParameters dOutAsetSewa;
        SvcTLBongkaranDtlSelect.execute_pttClient ambilDaftarAset;
        SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN dataAsetTerpilih;
        private ArrayList dsGridDaftarAset;

        private void getDaftarAset()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                svcInstansi = new SvcInstansiSelect.dsSelect_pttClient();
                SvcTLBongkaranDtlSelect.InputParameters parInput = new SvcTLBongkaranDtlSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                
                //parInput.STR_WHERE = String.Format(" (SK_KEPUTUSAN_BNK = '{0}' AND NO_BUKTI_LAKSANA = '{1}' ) ", teNomorSk.Text.Trim(), teNoBuktiLaksana.Text);
                parInput.STR_WHERE = String.Format(" (ID_SK_BNK = '{0}' AND NO_BUKTI_LAKSANA = '{1}' ) ", idSkWasdal, teNoBuktiLaksana.Text);
                ambilDaftarAset = new SvcTLBongkaranDtlSelect.execute_pttClient();
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
                dOutAsetSewa = ambilDaftarAset.Endexecute(result);
                ambilDaftarAset.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAset(loadDataDaftarAset), dOutAsetSewa);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
             //   MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAset(SvcTLBongkaranDtlSelect.OutputParameters outputDaftarAset);

        private void loadDataDaftarAset(SvcTLBongkaranDtlSelect.OutputParameters outputDaftarAset)
        {
            int jmlData = outputDaftarAset.SF_TL_DTL_BONGKARAN.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAset.SF_TL_DTL_BONGKARAN[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    dOutAsetSewa.SF_TL_DTL_BONGKARAN[i].NUMSpecified = false;
                    dsGridDaftarAset.Add(dOutAsetSewa.SF_TL_DTL_BONGKARAN[i]);
                    nilaiPersetujuan = nilaiPersetujuan + dOutAsetSewa.SF_TL_DTL_BONGKARAN[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + dOutAsetSewa.SF_TL_DTL_BONGKARAN[i].KUANTITAS;
                }
                gcDaftarAset.DataSource = null;
                gcDaftarAset.DataSource = dsGridDaftarAset;
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
        BONGKARAN.frmPuAset formDaftarAsetSatker;
        PU.frmPuAset formDaftarAsetKorwil;
        PU.frmPuAset formDaftarAsetEselon1;
        PU.frmPuAset formDaftarAsetKl;

        private void sbTambah_Click(object sender, EventArgs e)
        {
            //if (teJenisPemohon.Text == konfigApp.levelSatker)
            //{
                if (formDaftarAsetSatker == null)
                {
                    formDaftarAsetSatker = new BONGKARAN.frmPuAset() 
                    {
                        toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                        simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset)
                    };
                }
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
                formDaftarAsetSatker.noBuktiLaksana = teNoBuktiLaksana.Text;
                formDaftarAsetSatker.ShowDialog();
            //}
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
        SvcTLBongkaranDtlCud.OutputParameters dOutSimpanAset;
        SvcTLBongkaranDtlCud.execute_pttClient simpanDftrAset;
        private char modeCudAset = 'A';
        private decimal? nilaiPenetapanLama = null;
        private decimal? jmlKuantitasLama = null;

        private void simpanDaftarAset(string _daftarIdAset, char _modeCudAset)
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcTLBongkaranDtlCud.InputParameters parInp = new SvcTLBongkaranDtlCud.InputParameters();
                parInp.P_SK_KEPUTUSAN = teNomorSk.Text.Trim();
                parInp.P_ID_ASET = _daftarIdAset;
                parInp.P_ID_SK_BNK = this.idSkWasdal;
                parInp.P_ID_SK_BNKSpecified = true;
                parInp.P_ID_TL_WASDAL_BNK = this.idSkTL;
                parInp.P_ID_TL_WASDAL_BNKSpecified = true;
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                if (Convert.ToString(_modeCudAset) == "C"||Convert.ToString(_modeCudAset)=="D")
                {
                    parInp.P_NO_BUKTI_LAKSANA = teNoBuktiLaksana.Text;
                }
               
                modeCudAset = _modeCudAset;
                parInp.P_SELECT = Convert.ToString(modeCudAset);
                simpanDftrAset = new SvcTLBongkaranDtlCud.execute_pttClient();
                simpanDftrAset.Beginexecute(parInp, new AsyncCallback(goSimpanDftrAset), "");
            }
            catch
            {
                modeCudAset = 'A';
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
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
                MessageBox.Show(konfigApp.teksDialog+"\r\n"+e.Message, konfigApp.judulKonfirmasi);
            }
        }

        private delegate void DsDaftarAset(SvcTLBongkaranDtlCud.OutputParameters dataOut);

        private void dsDaftarAset(SvcTLBongkaranDtlCud.OutputParameters dataOut)
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
                        SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN dataPenyama;
                        _krgNilTetap = (teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(teNilaiPenetapan.Text));
                        _krgKuantitas = (teKuantitas.Text == "" ? 0 : Convert.ToDecimal(teKuantitas.Text));
                        for (int i = 0; i < daftarAsetSalinan.Count; i++)
                        {
                            SvcWasdalRSKBongkaranDtlSelect.WASDALSROW_BONGKARAN_GRID_DTL daftarAsetTerpilih = (SvcWasdalRSKBongkaranDtlSelect.WASDALSROW_BONGKARAN_GRID_DTL)daftarAsetSalinan[i];
                            dataPenyama = null;
                            dataPenyama = new SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN();
                            dataPenyama.GUNA_WASDAL = "";
                            dataPenyama.ID_ASET = daftarAsetTerpilih.ID_ASET;
                            dataPenyama.ID_ASETSpecified = true;
                            dataPenyama.ID_SATKER = idSatker;
                            dataPenyama.ID_SATKERSpecified = true;
                            dataPenyama.KD_BRG = daftarAsetTerpilih.KD_BRG;
                            dataPenyama.KD_SATKER = _kodeSatker;
                            dataPenyama.KD_STATUS = daftarAsetTerpilih.KD_STATUS;
                            dataPenyama.KUANTITAS = daftarAsetTerpilih.KUANTITAS;
                            dataPenyama.KUANTITASSpecified = true;
                            dataPenyama.NILAI_PERSETUJUAN = daftarAsetTerpilih.NILAI_PERSETUJUAN;
                            dataPenyama.NILAI_PERSETUJUANSpecified = daftarAsetTerpilih.NILAI_PERSETUJUANSpecified;
                            dataPenyama.NO_ASET = daftarAsetTerpilih.NO_ASET;
                            dataPenyama.NO_ASETSpecified = true;
                            dataPenyama.NUM = (dsGridDaftarAset == null ? 1 : dsGridDaftarAset.Count + 1);
                            dataPenyama.NUMSpecified = false;
                            dataPenyama.SK_KEPUTUSAN_BNK = teNomorSk.Text;
                            dataPenyama.TOTAL_DATA = daftarAsetTerpilih.TOTAL_DATA;
                            dataPenyama.TOTAL_DATASpecified = daftarAsetTerpilih.TOTAL_DATASpecified;
                            dataPenyama.UR_SATKER = _namaSatker;
                            dataPenyama.UR_SSKEL = daftarAsetTerpilih.UR_SSKEL;
                            dataPenyama.UR_STATUS = daftarAsetTerpilih.UR_STATUS;
                            _krgNilTetap = _krgNilTetap + daftarAsetTerpilih.NILAI_PERSETUJUAN;
                            _krgKuantitas = _krgKuantitas + daftarAsetTerpilih.KUANTITAS;
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
                                SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN daftarAsetTerpilih = (SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN)daftarTerpilih[i];
                                _krgNilTetap = _krgNilTetap - daftarAsetTerpilih.NILAI_PERSETUJUAN;
                                _krgKuantitas = _krgKuantitas - daftarAsetTerpilih.KUANTITAS;
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
                        dataPenyama = new SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN();
                        dataPenyama.GUNA_WASDAL = dataAsetTerpilih.GUNA_WASDAL;
                        dataPenyama.ID_ASET = dataAsetTerpilih.ID_ASET;
                        dataPenyama.ID_ASETSpecified = dataAsetTerpilih.ID_ASETSpecified;
                        dataPenyama.ID_SATKER = dataAsetTerpilih.ID_SATKER;
                        dataPenyama.ID_SATKERSpecified = dataAsetTerpilih.ID_SATKERSpecified;
                        dataPenyama.KD_BRG = dataAsetTerpilih.KD_BRG;
                        dataPenyama.KD_SATKER = dataAsetTerpilih.KD_SATKER;
                        dataPenyama.KD_STATUS = dataAsetTerpilih.KD_STATUS;
                        dataPenyama.KUANTITAS = Convert.ToDecimal(formPuPenetapan.teKuantitas.Text);
                        dataPenyama.KUANTITASSpecified = dataAsetTerpilih.KUANTITASSpecified;
                        dataPenyama.NILAI_PERSETUJUAN = Convert.ToDecimal(formPuPenetapan.teNilaiPersetujuan.Text);
                        dataPenyama.NILAI_PERSETUJUANSpecified = dataAsetTerpilih.NILAI_PERSETUJUANSpecified;
                        dataPenyama.NO_ASET = dataAsetTerpilih.NO_ASET;
                        dataPenyama.NO_ASETSpecified = dataAsetTerpilih.NO_ASETSpecified;
                        dataPenyama.NUM = dataAsetTerpilih.NUM;
                        dataPenyama.NUMSpecified = dataAsetTerpilih.NUMSpecified;
                        dataPenyama.SK_KEPUTUSAN_BNK = dataAsetTerpilih.SK_KEPUTUSAN_BNK;
                        dataPenyama.TOTAL_DATA = dataAsetTerpilih.TOTAL_DATA;
                        dataPenyama.TOTAL_DATASpecified = dataAsetTerpilih.TOTAL_DATASpecified;
                        dataPenyama.UR_SATKER = dataAsetTerpilih.UR_SATKER;
                        dataPenyama.UR_SSKEL = dataAsetTerpilih.UR_SSKEL;
                        dataPenyama.UR_STATUS = dataAsetTerpilih.UR_STATUS;
                        nilaiPenetapanLama = dataAsetTerpilih.NILAI_PERSETUJUAN;
                        jmlKuantitasLama = dataAsetTerpilih.KUANTITAS;
                        _krgNilTetap = (Convert.ToDecimal(teNilaiPenetapan.Text) - nilaiPenetapanLama + Convert.ToDecimal(formPuPenetapan.teNilaiPersetujuan.Text));
                        _krgKuantitas = (Convert.ToDecimal(teKuantitas.Text) - jmlKuantitasLama + Convert.ToDecimal(formPuPenetapan.teKuantitas.Text));
                        teNilaiPenetapan.Text = Convert.ToDecimal(_krgNilTetap).ToString("n0");
                        teKuantitas.Text = Convert.ToDecimal(_krgKuantitas).ToString("n0");
                        dsGridDaftarAset.Remove(dataAsetTerpilih);
                        dsGridDaftarAset.Insert(_indeksData, dataPenyama);
                        try
                        {
                            formPuPenetapan.Close();
                        }
                        catch { }
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
            SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN daftarAsetTerpilih;
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
                        daftarAsetTerpilih = (SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN)daftarTerpilih[i];
                        daftarIdAset = String.Format("{0}{1};", daftarIdAset, daftarAsetTerpilih.ID_ASET);
                    }
                    daftarIdAset = daftarIdAset.TrimEnd(';');
                    if (MessageBox.Show("Apakah Anda ingin menghapus Daftar Aset yang terpilih?", konfigApp.judulHapusData,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        simpanDaftarAset(daftarIdAset, 'D');//d
                    }
                }
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
                dataAsetTerpilih = (SvcTLBongkaranDtlSelect.WASDALSROW_TL_DTL_BONGKARAN)rowTerpilih.GetRow(e.FocusedRowHandle);
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
        PU.frmPuPenetapan formPuPenetapan;

        private void formUbahPenetapan()
        {
            if (statusForm != "A")
            {
                if (formPuPenetapan == null)
                {
                    formPuPenetapan = new PU.frmPuPenetapan() { simpanDataPenetapan = new SimpanDaftarAsetTerpilih(simpanDaftarAset) };
                }
                formPuPenetapan.idAset = dataAsetTerpilih.ID_ASET;
                formPuPenetapan.teKodeBarang.Text = dataAsetTerpilih.KD_BRG;
                formPuPenetapan.teNamaBarang.Text = dataAsetTerpilih.UR_SSKEL;
                formPuPenetapan.teNup.Text = dataAsetTerpilih.NO_ASET.ToString();
                formPuPenetapan.teNilaiPersetujuan.Text = (Convert.ToDecimal(dataAsetTerpilih.NILAI_PERSETUJUAN)).ToString("n0");
                formPuPenetapan.teKuantitas.Text = (Convert.ToDecimal(dataAsetTerpilih.KUANTITAS)).ToString("n0");
                formPuPenetapan.kodeStatus = dataAsetTerpilih.KD_STATUS;
                formPuPenetapan.teStatus.Text = dataAsetTerpilih.UR_STATUS;
                formPuPenetapan.lciStatusBmn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                formPuPenetapan.tePenggunaanBmn.Text = dataAsetTerpilih.GUNA_WASDAL;
                formPuPenetapan.ShowDialog();
            }
        }
        private void gvDaftarAset_DoubleClick(object sender, EventArgs e)
        {
            //formUbahPenetapan();
        }
        private void gvDaftarAset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
              //  formUbahPenetapan();
            }
        }
        #endregion

        private void btnUploadFileBukti_Click(object sender, EventArgs e)
        {
            uploadSelect = "CE";
            initDok(uploadSelect);
        }

        #endregion

        string uploadSelect = "CE";
        SvcWasdalManfaatTdLanjutFile2Bnk.execute_pttClient getFileWasdal;
        SvcWasdalManfaatTdLanjutFile2Bnk.OutputParameters outFileWasdal;
        private void btnViewDok_Click(object sender, EventArgs e)
        {
            uploadSelect = "R";
            initDok(uploadSelect);
        }

        private void initDok(string select)
        {
            if (idSkTL == null)
            {
                MessageBox.Show("Silahkan simpan data terlebih dahulu", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalManfaatTdLanjutFile2Bnk.InputParameters parInp = new SvcWasdalManfaatTdLanjutFile2Bnk.InputParameters();

                parInp.P_SELECT = select;
                //parInp.P_NO_BUKTI_LAKSANA = teNoBuktiLaksana.Text;
                parInp.P_SK_KEPUTUSAN = teNomorSk.Text;
                //parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                if (uploadSelect.Equals("CE"))
                {
                    parInp.P_FILE_DOK = konfigApp.FileToByteArray(filePath);
                    parInp.P_NM_FILE = nmFileTL;
                    parInp.P_TGL_UPLOAD = DateTime.Now;
                }
                parInp.P_ID_SK_WASDAL_BNK = idSkWasdal;
                parInp.P_ID_SK_WASDAL_BNKSpecified = true;
                parInp.P_ID_TL_WASDAL_BNK = idSkTL;
                parInp.P_ID_TL_WASDAL_BNKSpecified = true;
                getFileWasdal = new SvcWasdalManfaatTdLanjutFile2Bnk.execute_pttClient();
                getFileWasdal.Beginexecute(parInp, new AsyncCallback(getResultFileWasdal), "");
            }
            catch (Exception ex)
            {
                this.Invoke(new AktifkanForm(toggleProgBarPu), new object[] { "finisih" });
                if (uploadSelect == "CE")
                {
                    MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalSimpan);
                }
                else
                {
                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }
            }
        }

        private void getResultFileWasdal(IAsyncResult result)
        {
            try
            {
                outFileWasdal = getFileWasdal.Endexecute(result);
                getFileWasdal.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(toggleProgBarPu), new object[] { "finisih" });
                this.Invoke(new DsFileWasdal(dsFileWasdal), outFileWasdal);
            }
            catch
            {
                this.Invoke(new AktifkanForm(toggleProgBarPu), new object[] { "finisih" });
                if (uploadSelect == "CE")
                {
                    MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalSimpan);
                }
                else
                {
                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }
            }
        }

        private delegate void DsFileWasdal(SvcWasdalManfaatTdLanjutFile2Bnk.OutputParameters outFileWasdal);
        private void dsFileWasdal(SvcWasdalManfaatTdLanjutFile2Bnk.OutputParameters outFileWasdal)
        {
            if (outFileWasdal.PO_RESULT == "Y")
            {
                if (uploadSelect.Equals("CE"))
                {
                    AutoClosingMessageBox.Show("Upload Dokumen Berhasil", "Perhatian", 1000);
                }
                else
                {
                    PU.FrmPuViewPdf FrmPuViewPdf = new PU.FrmPuViewPdf();
                    string appPath = AppDomain.CurrentDomain.BaseDirectory;
                    System.IO.File.WriteAllBytes(System.IO.Path.Combine(appPath, "DOK_BUKTI_" + outFileWasdal.PO_NM_FILE.Replace('/', '-') + ".pdf"), outFileWasdal.PO_FILE_BUKTI);
                    FrmPuViewPdf.displayFile(System.IO.Path.Combine(appPath, "DOK_BUKTI_" + outFileWasdal.PO_NM_FILE.Replace('/', '-') + ".pdf"));
                    FrmPuViewPdf.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(outFileWasdal.PO_RESULT_MESSAGE);
            }
        }

        private void teTanggalSk_EditValueChanged(object sender, EventArgs e)
        {
            teTahunAnggaran.Text = teTanggalSk.DateTime.ToString("yyyy");
        }

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
                nmFileTL = ofdDok.SafeFileName;
            }
        }
    }
}
