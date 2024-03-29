﻿using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using AppPengguna.KKW.RSK.PU;
using AppPengguna.KKW.RSK.PU;

namespace AppPengguna.KKW.RSK
{
    public partial class ucRskSewaBmnForm : UserControl
    {
        public ToggleProgressBar toggleProgressBar;
        public SimpanDataRsk simpanDataRskPspBmn;
        public string statusForm = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public decimal? idPemohon = null;
        public string kodePenerbitSkDetail = null;
        public string namaPenerbitSkDetail = null;
        public string kodePenerbitSk = null;
        private char modeCrud = 'A';
        public SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT dataTerpilih;
        private bool instansiLoaded = false;
        private bool jnsMitraLoaded = false;
        private decimal? idSatker = null;
        private string noSkLama = null;
        private string noSkBaru = null;
        private int indexInstansi;
        public string tipePengelola = null;
        public string namaFileSk = null;
        public decimal? idSkWasdal = null;
        private bool canUpload = false;

        public ucRskSewaBmnForm(string _status)
        {
            InitializeComponent();
            statusForm = _status;
            daftarGambar = new Hashtable();
            idSatker = konfigApp.idSatker;
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
                input.P_MIN = 0;
                input.P_MAXSpecified = true;
                input.P_MAX = 0;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = "";
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
                lePenerbitSk.Properties.DataSource = outputInstansi.SF_ROW_R_INSTANSI;
                lePenerbitSk.EditValue = outputInstansi.SF_ROW_R_INSTANSI[0].KD_INSTANSI;
                if (dataTerpilih != null)
                {
                    lePenerbitSk.Text = dataTerpilih.NM_PENERBIT_SK;
                    lePenerbitSk.EditValue = dataTerpilih.KD_PENERBIT_SK;
                }
                if (statusForm != "C")
                    getDaftarAset();
            }
            else instansiLoaded = false;
        }
        #endregion

        #region Inisialisasi
        private void resetFormRskPspBmn()
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
            teNamaPenandaTangan.Text = "";
            teNipPenandaTangan.Text = "";
            teJabatan.Text = "";
            teUraianKeputusan.Text = "";
            gcDaftarAset.DataSource = null;
            teAlamatPihakLain.Text = "";
            //teJenisMitra.Text = "";
            teNamaPihakLain.Text = "";
            teJangkaWaktu.Text = "";
            tePeruntukan.Text = "";
            cbPeriode.SelectedText = "";
            tePeruntukan.Text = "";
            teKuantitas.Text = "";
            teNilaiPenetapan.Text = "";
            teFileSk.Text = "";
            teDariTgl.Text = "";
            teSampaiTgl.Text = "";
            teTahunAnggaran.Text = konfigApp.tahunAnggaran.ToString();
            filePath = null;
            dsGridDaftarAset = null;
            //lciKosong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lciKodeKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lciNamaKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public void inisialisasiForm()
        {
            resetFormRskPspBmn();
            if (statusForm != "C" && statusForm != "CU")
            {
                rgJenisAset.SelectedIndex = (dataTerpilih.IS_TB[0] == 'T' ? 0 : 1);
                teNomorSk.Text = dataTerpilih.SK_KEPUTUSAN;
                noSkBaru = dataTerpilih.SK_KEPUTUSAN.Trim();
                idSkWasdal = dataTerpilih.ID_SK_WASDAL_MANFAAT;
                DateTime tanggalSurat = Convert.ToDateTime(dataTerpilih.TGL_SK);
                teTanggalSk.EditValue = tanggalSurat;
                teTahunAnggaran.Text = dataTerpilih.THN_ANG;
                teJenisPemohon.Text = dataTerpilih.TIPE_PEMOHON;
                idPemohon = dataTerpilih.ID_PEMOHON;
                teKodePemohon.Text = dataTerpilih.KD_PEMOHON;
                teNamaPemohon.Text = dataTerpilih.NM_PEMOHON;
                teKodeKl.Text = dataTerpilih.KD_KL;
                teNamaKl.Text = dataTerpilih.UR_KL;
                lePenerbitSk.EditValue = dataTerpilih.KD_PENERBIT_SK;
                teNilaiPenetapan.Text = Convert.ToDecimal(dataTerpilih.NILAI_PENETAPAN).ToString("n2");
                teKuantitas.Text = dataTerpilih.KUANTITAS_SK.ToString();
                teNamaPenandaTangan.Text = dataTerpilih.NM_PENANDATANGAN;
                teNipPenandaTangan.Text = dataTerpilih.NIP_PENANDATANGAN;
                teJabatan.Text = dataTerpilih.JABATAN_TTD;
                teUraianKeputusan.Text = dataTerpilih.URAIAN_KEPUTUSAN;
                teNamaPihakLain.Text = dataTerpilih.NM_PHK_LAIN;
                teJangkaWaktu.Text = Convert.ToDecimal(dataTerpilih.JANGKA_WAKTU).ToString("n0");
                teAlamatPihakLain.Text = dataTerpilih.ALAMAT_PHK_LAIN;
                tePeruntukan.Text = dataTerpilih.PERUNTUKAN;
                teDariTgl.EditValue = Convert.ToDateTime(dataTerpilih.DARI_TGL);
                teSampaiTgl.EditValue = Convert.ToDateTime(dataTerpilih.SD_TGL);
                teFileSk.Text = dataTerpilih.NM_FILE;
                kodePenerbitSkDetail = dataTerpilih.KD_PENERBIT_SK_DTL;
                namaPenerbitSkDetail = dataTerpilih.NM_PENERBIT_SK_DTL;

                switch (dataTerpilih.PERIODE)
                {
                    case "Harian":
                        cbPeriode.SelectedIndex = 0;
                        break;
                    case "Mingguan":
                        cbPeriode.SelectedIndex = 1;
                        break;
                    case "Bulanan":
                        cbPeriode.SelectedIndex = 2;
                        break;
                    case "Tahunan":
                        cbPeriode.SelectedIndex = 3;
                        break;
                }
            }
            

            if (instansiLoaded == false)
                getInstansi();
            else
            {
                if (statusForm != "C" && statusForm != "CU")
                {
                    //if (noSkLama != noSkBaru)
                        getDaftarAset();
                    //else
                    //{
                        gcDaftarAset.DataSource = dsGridDaftarAset;
                        gcDaftarAset.RefreshDataSource();
                    //}
                }
            }
            rgJenisAset.Properties.ReadOnly = false;
            teNomorSk.Properties.ReadOnly = false;
            teTanggalSk.Properties.ReadOnly = false;
            teJenisPemohon.Properties.ReadOnly = false;
            lePenerbitSk.Properties.ReadOnly = false;
            teNamaPenandaTangan.Properties.ReadOnly = false;
            teNipPenandaTangan.Properties.ReadOnly = false;
            teJabatan.Properties.ReadOnly = false;
            teUraianKeputusan.Properties.ReadOnly = false;
            sbSimpan.Enabled = true;
            sbValidasi.Enabled = false;
            sbCariPemohon.Enabled = false;
            sbTambah.Enabled = false;
            sbHapus.Enabled = false;
            sbRefresh.Enabled = false;
            gcDaftarAset.Enabled = false;
            cePilihSemua.Enabled = false;
            teKodePemohon.Properties.ReadOnly = true;
            teAlamatPihakLain.Properties.ReadOnly = false;
            teJangkaWaktu.Properties.ReadOnly = false;
            tePeruntukan.Properties.ReadOnly = false;
            cbPeriode.Properties.ReadOnly = false;
            teNamaPihakLain.Properties.ReadOnly = false;
            if (statusForm == "A")
            {
                teTahunAnggaran.Properties.ReadOnly = true;
                teFileSk.Properties.ReadOnly = true;
                teSampaiTgl.Properties.ReadOnly = true;
                teDariTgl.Properties.ReadOnly = true;
            }

            switch (statusForm)
            {
                case "C":
                    //idSkWasdal = konfigApp.getGlobalId("ID_SK_WASDAL_MANFAAT");
                    idSkWasdal = null;
                    break;
                case "CU":
                    teKodePemohon.Properties.ReadOnly = true;
                    teTahunAnggaran.Text = konfigApp.tahunAnggaran.ToString();
                    teNomorSk.Focus();
                    teKodePemohon.Properties.ReadOnly = true;
                    teFileSk.Text = null;
                    break;
                case "U":
                    gcDaftarAset.Enabled = true;
                    rgJenisAset.Properties.ReadOnly = true;
                    teNomorSk.Properties.ReadOnly = false;
                    teTanggalSk.Properties.ReadOnly = false;
                    teKodePemohon.Properties.ReadOnly = true;
                    teJenisPemohon.Properties.ReadOnly = true;
                    sbCariPemohon.Enabled = false;
                    sbTambah.Enabled = true;
                    sbHapus.Enabled = true;
                    sbRefresh.Enabled = true;
                    cePilihSemua.Enabled = true;
                    sbValidasi.Enabled = true;
                    break;
                case "A":
                    rgJenisAset.Properties.ReadOnly = true;
                    teNomorSk.Properties.ReadOnly = true;
                    teTanggalSk.Properties.ReadOnly = true;
                    teKodePemohon.Properties.ReadOnly = true;
                    teJenisPemohon.Properties.ReadOnly = true;
                    lePenerbitSk.Properties.ReadOnly = true;
                    teNamaPenandaTangan.Properties.ReadOnly = true;
                    teNipPenandaTangan.Properties.ReadOnly = true;
                    teJabatan.Properties.ReadOnly = true;
                    teUraianKeputusan.Properties.ReadOnly = true;
                    sbSimpan.Enabled = false;
                    sbCariPemohon.Enabled = false;
                    sbSimpan.Enabled = false;
                    sbHapus.Enabled = false;
                    gcDaftarAset.Enabled = true;
                    sbRefresh.Enabled = true;
                    gridColPilih.OptionsColumn.AllowEdit = false;
                    gridColPilih.OptionsColumn.ReadOnly = true;
                    teAlamatPihakLain.Properties.ReadOnly = true;
                    teJangkaWaktu.Properties.ReadOnly = true;
                    tePeruntukan.Properties.ReadOnly = true;
                    cbPeriode.Properties.ReadOnly = true;
                    teNamaPihakLain.Properties.ReadOnly = true;
                    break;
            }
            
            xtpIdentitas.Text = "SATKER: " + konfigApp.kodeSatker + konfigApp.namaSatker;
            lciLoadingGambar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            foreach (Control c in layoutControl1.Controls)
            {
                this.PropertyChangeState(c, true);
            }
        }
        #endregion

        private void teJenisPemohon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (teJenisPemohon.SelectedIndex == 0)
            {
                teKodePemohon.Text = konfigApp.kodeSatker;
                teNamaPemohon.Text = konfigApp.namaSatker;
                idPemohon = konfigApp.idSatker;
            }
            else if (teJenisPemohon.SelectedIndex == 1)
            {
                teKodePemohon.Text = konfigApp.kodeKorwil;
                teNamaPemohon.Text = konfigApp.namaKorwil;
                idPemohon = konfigApp.idKorwil;
            }
            else if (teJenisPemohon.SelectedIndex == 2)
            {
                teKodePemohon.Text = konfigApp.kodeEselon1;
                teNamaPemohon.Text = konfigApp.namaEselon1;
                idPemohon = konfigApp.idEselon1;
            }
            else if (teJenisPemohon.SelectedIndex == 3)
            {
                teKodePemohon.Text = konfigApp.kodeKl;
                teNamaPemohon.Text = konfigApp.namaKl;
                idPemohon = konfigApp.idKl;
            }
            teKodeKl.Text = konfigApp.kodeKl;
            teNamaKl.Text = konfigApp.namaKl;
            
        }

        private void teNmPenerbitSk_EditValueChanged(object sender, EventArgs e)
        {
            if (lePenerbitSk.ItemIndex > -1)
            {
                indexInstansi = Convert.ToInt16(lePenerbitSk.ItemIndex.ToString());
                if (outputInstansi.SF_ROW_R_INSTANSI[indexInstansi].KD_INSTANSI == "01")
                {
                    kodePenerbitSk = "01";
                    kodePenerbitSkDetail = konfigApp.kodeKpknl;
                    teNmPenerbitSkDetail.Text = konfigApp.namaKpknl;
                    tipePengelola = "3";
                }
                else if (outputInstansi.SF_ROW_R_INSTANSI[lePenerbitSk.ItemIndex].KD_INSTANSI == "02") // atau 8
                {
                    kodePenerbitSk = "02";
                    kodePenerbitSkDetail = konfigApp.kodeKanwil;
                    teNmPenerbitSkDetail.Text = konfigApp.namaKanwil;
                    tipePengelola = "2";
                }
                else if (outputInstansi.SF_ROW_R_INSTANSI[lePenerbitSk.ItemIndex].KD_INSTANSI == "03")
                { //dit pknsi
                    kodePenerbitSk = "03";
                    kodePenerbitSkDetail = konfigApp.kodeDitPknsi;
                    teNmPenerbitSkDetail.Text = "DIT PKNSI";
                    tipePengelola = "1";
                }
                else if (outputInstansi.SF_ROW_R_INSTANSI[lePenerbitSk.ItemIndex].KD_INSTANSI == "04")
                { //djkn
                    kodePenerbitSk = "04";
                    kodePenerbitSkDetail = konfigApp.kodeKpDjkn;
                    teNmPenerbitSkDetail.Text = "Kantor Pusat DJKN";
                    tipePengelola = "1";
                }
                else if (outputInstansi.SF_ROW_R_INSTANSI[lePenerbitSk.ItemIndex].KD_INSTANSI == "05")
                {
                    kodePenerbitSk = "05";
                    kodePenerbitSkDetail = konfigApp.kodeKl;
                    teNmPenerbitSkDetail.Text = konfigApp.namaKl;
                    tipePengelola = "3";
                }
                else if (outputInstansi.SF_ROW_R_INSTANSI[lePenerbitSk.ItemIndex].KD_INSTANSI == "06")
                {
                    kodePenerbitSk = "06";
                    kodePenerbitSkDetail = konfigApp.kodeEselon1;
                    teNmPenerbitSkDetail.Text = konfigApp.namaEselon1;
                    tipePengelola = "3";
                }
                else if (outputInstansi.SF_ROW_R_INSTANSI[lePenerbitSk.ItemIndex].KD_INSTANSI == "07")
                {
                    kodePenerbitSk = "07";
                    kodePenerbitSkDetail = konfigApp.kodeEselon1;
                    teNmPenerbitSkDetail.Text = konfigApp.namaEselon1;
                    this.tipePengelola = "1";
                }
            }
        }

        private void PropertyChangeState(Control c, bool state)
        {
            if (c is DevExpress.XtraEditors.TextEdit) ((DevExpress.XtraEditors.TextEdit)c).Properties.ReadOnly = state;
            else if (c is DevExpress.XtraEditors.SpinEdit) ((DevExpress.XtraEditors.SpinEdit)c).Properties.ReadOnly = state;
            else if (c is DevExpress.XtraEditors.LookUpEdit) ((DevExpress.XtraEditors.LookUpEdit)c).Properties.ReadOnly = state;
            else if (c is DevExpress.XtraEditors.DateEdit) ((DevExpress.XtraEditors.DateEdit)c).Properties.ReadOnly = state;
        }


        public string konversiPeriode() 
        {
            string per = "";

            if (cbPeriode.SelectedIndex == 0)
            {
                per = "H";
            }
            else if (cbPeriode.SelectedIndex == 1)
            {
                per = "M";
            } if (cbPeriode.SelectedIndex == 2)
            {
                per = "B";
            } if (cbPeriode.SelectedIndex == 3)
            {
                per = "T";
            }

            return per;
        }

        #region Bagian Header
        frmPuSatker formPuSatker;
        frmPuKorwil formPuKorwil;
        frmPuEselon1 formPuEselon1;
        frmPuKl formPuKl;

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
                    //if (formPuSatker == null)
                    //{
                        formPuSatker = new frmPuSatker()
                        {
                            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                            selectUnitKerja = new SelectDataUnitKerja(setUnitKerja),
                            whereAwal = preWhere
                        };
                    //}
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
                        formPuKorwil = new frmPuKorwil()
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
                        formPuEselon1 = new frmPuEselon1()
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
                        formPuKl = new frmPuKl()
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
            if (konfigApp.cekMinus(Convert.ToDecimal(teJangkaWaktu.Text)) == true)
            {
                MessageBox.Show("Jangka Waktu tidak boleh negatif !");
            }
            else
            {
                if (rgJenisAset.SelectedIndex != -1 && teNomorSk.Text.Trim() != "" && teNamaPemohon.Text != "" && teTanggalSk.Text != "" && lePenerbitSk.Text.Trim() != "")
                {
                    if (statusForm == "C" || statusForm == "U" || statusForm == "CU")
                    {
                        try
                        {
                            if (statusForm == "C")
                            {
                                idSkWasdal = konfigApp.getGlobalId("ID_SK_WASDAL_MANFAAT");
                            }
                            simpanDataRskPspBmn(statusForm);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
               }
                else MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulKonfirmasi);
            }
        }

        private void teKodePemohon_EditValueChanged(object sender, EventArgs e)
        {
            teNamaPemohon.Text = "";
            teKodeKl.Text = "";
            teNamaKl.Text = "";
            //idPemohon = null;
        }

        #region upload and download file

        public string filePath;
        
        OpenFileDialog ofdDok = new OpenFileDialog();
        
        private SvcFileRekamSkCru.execute_pttClient fetchingFile;
        private SvcFileRekamSkCru.OutputParameters dataOutFile;
        private string _statusCrud=null;

        private void teInputFile_Click(object sender, EventArgs e)
        {
            ofdDok.Title = "Pilih File";
            ofdDok.FileName = "File PDF";
            ofdDok.Filter = "(File PDF)|*.pdf";
            ofdDok.Multiselect = false;

            if (ofdDok.ShowDialog() == DialogResult.OK && ofdDok.FileName != "File PDF")
            {
                
                teFileSk.Text = ofdDok.InitialDirectory + ofdDok.FileName;
                namaFileSk = ofdDok.SafeFileName;
                
            }
         
        }

        private void sbUpload_Click(object sender, EventArgs e)
        {
            if (idSkWasdal == null)
            {
                MessageBox.Show("Silahkan Simpan data terlebih dahulu", "Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if (teFileSk.Text.Trim() != null && teFileSk.Text.Trim() != "")
            {
                _statusCrud = "Upload";
                getInitFileUpload();

            }
            else
            {
                MessageBox.Show("File belum dipilih");
            }
        }

        #region UPLOAD FILE ==================================
        private void getInitFileUpload( )
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcFileRekamSkCru.InputParameters parInp = new SvcFileRekamSkCru.InputParameters();

                if (_statusCrud == "Upload")
                {
                    parInp.P_SELECT = "C";

                    parInp.P_ID_SK_WASDAL = this.idSkWasdal.ToString();
                    
                    parInp.P_FILE_DOK = konfigApp.FileToByteArray(teFileSk.Text);
                    parInp.P_SK_KEPUTUSAN = teNomorSk.Text;
                    parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                    parInp.P_NM_FILE = ofdDok.SafeFileName;

                }
                else
                {
                    parInp.P_ID_SK_WASDAL = this.idSkWasdal.ToString();

                    parInp.P_SK_KEPUTUSAN = teNomorSk.Text;
                    parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                    parInp.P_SELECT = "R";
                }
                parInp.P_TGL_UPLOAD = konfigApp.DateToDb(DateTime.Now.ToShortDateString());
                fetchingFile = new SvcFileRekamSkCru.execute_pttClient();
                fetchingFile.Beginexecute(parInp, new AsyncCallback(getResultFileUploadRead), "");

            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm));
                MessageBox.Show(konfigApp.teksGagalSimpanFileDok,konfigApp.judulGagalSimpan);
            }
        }

        private void getResultFileUploadRead(IAsyncResult result)
        {
            try
            {
                dataOutFile = fetchingFile.Endexecute(result);
                fetchingFile.Close();
                nonAktifkanprogressBar();
                //this.Invoke(new AktifkanForm(aktifkanForm));
                this.Invoke(new dsFileUploadRead(dsfileUploadRead), dataOutFile);
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                // this.Invoke(new AktifkanForm(aktifkanForm));
                MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalSimpan);
            }
        }

        private delegate void dsFileUploadRead(SvcFileRekamSkCru.OutputParameters dataout);

        private void dsfileUploadRead(SvcFileRekamSkCru.OutputParameters dataout)
        {
            if (dataout.PO_RESULT == "Y")
            {
                if (_statusCrud == "Upload")
                {
                    MessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulsukses);
                    sbView.Enabled = true;
                }
                else
                {
                    KSK.TL.PU.FrmPuViewPdf frmPuViewPdf = new KSK.TL.PU.FrmPuViewPdf();
                    
                    
                    System.IO.File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +dataOutFile.PO_NM_FILE +".pdf", dataout.PO_FILE_DOK);
                    frmPuViewPdf.display(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + dataOutFile.PO_NM_FILE + ".pdf");
                    frmPuViewPdf.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(dataout.PO_RESULT_MESSAGE);
            }
        }
        #endregion

        SvcFileRekamSkCru.execute_pttClient getFileWasdal;
        SvcFileRekamSkCru.OutputParameters outFileWasdal;

        private void sbView_Click(object sender, EventArgs e)
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcFileRekamSkCru.InputParameters parInp = new SvcFileRekamSkCru.InputParameters();
                parInp.P_ID_SK_WASDAL = this.idSkWasdal.ToString();
                parInp.P_SK_KEPUTUSAN = teNomorSk.Text;
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_SELECT = "R";

                getFileWasdal = new SvcFileRekamSkCru.execute_pttClient();
                getFileWasdal.Beginexecute(parInp, new AsyncCallback(getResultFileWasdal), "");
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
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
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsFileWasdal(SvcFileRekamSkCru.OutputParameters outFileWasdal);
        private void dsFileWasdal(SvcFileRekamSkCru.OutputParameters outFileWasdal)
        {
            if (outFileWasdal.PO_RESULT == "Y")
            {
                FrmPuViewPdf FrmPuViewPdf = new FrmPuViewPdf();

                System.IO.File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +outFileWasdal.PO_NM_FILE +".pdf", outFileWasdal.PO_FILE_DOK);
                FrmPuViewPdf.display(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + outFileWasdal.PO_NM_FILE + ".pdf");
                FrmPuViewPdf.ShowDialog();
            }
            else
            {
                MessageBox.Show(outFileWasdal.PO_RESULT_MESSAGE);
            }
        }
        #endregion

        #region Tanggal Otomatis
        private void TanggalOtomatis()
        {
            if (teJangkaWaktu.EditValue != "")
            {
                string _per = cbPeriode.Text;
                DateTime setTgl = DateTime.Now;
                DateTime sekarang = DateTime.Now;
            
                switch (_per)
                {
                    case "Tahunan":
                        setTgl = sekarang.AddYears(Convert.ToInt16(teJangkaWaktu.EditValue));
                        break;
                    case "Bulanan":
                        setTgl = sekarang.AddMonths(Convert.ToInt16(teJangkaWaktu.EditValue));
                        break;
                    case "Mingguan":
                        setTgl = sekarang.AddDays(Convert.ToInt16(teJangkaWaktu.EditValue) * 7);
                        break;
                    case "Harian":
                        setTgl = sekarang.AddDays(Convert.ToInt16(teJangkaWaktu.EditValue));
                        break;
                    default: _per = "Harian";
                        break;
                }

                teDariTgl.Text = konfigApp.DateToString(DateTime.Now);
                teSampaiTgl.Text = konfigApp.DateToString(setTgl);
            }
        }

        private void teJangkaWaktu_EditValueChanged(object sender, EventArgs e)
        {
            this.TanggalOtomatis();
        }

        private void cbPeriode_EditValueChanged(object sender, EventArgs e)
        {
            this.TanggalOtomatis();
        }

        #endregion Tanggal Otomatis
        
        #endregion

        #region Bagian Detail: Aset
        
        #region --++ Ambil Daftar Aset dalam SK
        SvcWasdalManfaatSelect.OutputParameters dOutDaftarAset;
        SvcWasdalManfaatSelect.execute_pttClient ambilDaftarAset;
        SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT dataAsetTerpilih;
        private ArrayList dsGridDaftarAset;

        private void getDaftarAset()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                //svcInstansi = new SvcInstansiSelect.dsSelect_pttClient();
                SvcWasdalManfaatSelect.InputParameters parInput = new SvcWasdalManfaatSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format(" SK_KEPUTUSAN = '{0}' AND ID_SK_WASDAL_MANFAAT={1}", teNomorSk.Text.Trim(),idSkWasdal);
                ambilDaftarAset = new SvcWasdalManfaatSelect.execute_pttClient();
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

        private delegate void LoadDataDaftarAset(SvcWasdalManfaatSelect.OutputParameters outputDaftarAset);

        private void loadDataDaftarAset(SvcWasdalManfaatSelect.OutputParameters outputDaftarAset)
        {
            int jmlData = outputDaftarAset.SF_ROW_WASDAL_MANFAAT.Count();
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData >= 1 && outputDaftarAset.SF_ROW_WASDAL_MANFAAT[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAset.SF_ROW_WASDAL_MANFAAT[i].IS_TB = (outputDaftarAset.SF_ROW_WASDAL_MANFAAT[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAset.SF_ROW_WASDAL_MANFAAT[i].NUMSpecified = false;
                    dsGridDaftarAset.Add(outputDaftarAset.SF_ROW_WASDAL_MANFAAT[i]);
                    nilaiPersetujuan = nilaiPersetujuan + outputDaftarAset.SF_ROW_WASDAL_MANFAAT[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + outputDaftarAset.SF_ROW_WASDAL_MANFAAT[i].KUANTITAS;
                }
                gcDaftarAset.DataSource = null;
                gcDaftarAset.DataSource = dsGridDaftarAset;
            }
            teNilaiPenetapan.Text = Convert.ToDecimal(nilaiPersetujuan).ToString("n0");
            teKuantitas.Text = Convert.ToDecimal(jmlKuantitas).ToString("n0");
            noSkLama = noSkBaru;
        }

        private void sbRefresh_Click(object sender, EventArgs e)
        {
            getDaftarAset();
        }
        #endregion

        #region --++ Tambah Daftar Aset dalam SK
        frmPuAsetNonPsp formDaftarAsetSatker;
        frmPuAsetNonPsp formDaftarAsetKorwil;
        frmPuAsetNonPsp formDaftarAsetEselon1;
        frmPuAsetNonPsp formDaftarAsetKl;
        
        private void sbTambah_Click(object sender, EventArgs e)
        {
            if (teJenisPemohon.Text == konfigApp.levelSatker)
            {
                if (formDaftarAsetSatker == null)
                {
                    formDaftarAsetSatker = new frmPuAsetNonPsp()
                    {
                        toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                        simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset),
                        //isTb = (dataTerpilih.IS_TB == "Tanah dan Bangunan" ? "Y" : "N")
                        isTb = this.rgJenisAset.EditValue.ToString()
                    };
                }
                formDaftarAsetSatker.isTb = rgJenisAset.EditValue.ToString();
                formDaftarAsetSatker.idPemohonBaru = idPemohon;
                formDaftarAsetSatker.idPemohonBaru = konfigApp.idSatker;
                formDaftarAsetSatker.idSatker = konfigApp.idSatker;
                formDaftarAsetSatker.kodeSatker = konfigApp.kodeSatker;
                formDaftarAsetSatker.namaSatker = konfigApp.namaSatker;
                formDaftarAsetSatker.teSatker.Properties.Items.Clear();
                formDaftarAsetSatker.teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", konfigApp.kodeSatker, konfigApp.namaSatker));
                formDaftarAsetSatker.teSatker.SelectedIndex = 0;
                formDaftarAsetSatker.statusBmnYn = "YN";
                formDaftarAsetSatker.levelPenggunaBarang = konfigApp.levelSatker;
                formDaftarAsetSatker.ShowDialog();
            }
            else if (teJenisPemohon.Text == konfigApp.levelKorwil)
            {
                if (formDaftarAsetKorwil == null)
                {
                    formDaftarAsetKorwil = new frmPuAsetNonPsp()
                    {
                        toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                        simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset),
                        isTb = (dataTerpilih.IS_TB == "Tanah dan Bangunan" ? "Y" : "N")
                    };
                }
                formDaftarAsetSatker.isTb = rgJenisAset.EditValue.ToString();
                formDaftarAsetSatker.idPemohonBaru = idPemohon;
                formDaftarAsetSatker.idPemohonBaru = konfigApp.idSatker;
                formDaftarAsetSatker.idSatker = konfigApp.idSatker;
                formDaftarAsetSatker.kodeSatker = konfigApp.kodeSatker;
                formDaftarAsetSatker.namaSatker = konfigApp.namaSatker;
                formDaftarAsetSatker.teSatker.Properties.Items.Clear();
                formDaftarAsetSatker.teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", konfigApp.idSatker, konfigApp.namaSatker));
                formDaftarAsetSatker.teSatker.SelectedIndex = 0;
                formDaftarAsetKorwil.levelPenggunaBarang = konfigApp.levelKorwil;
                formDaftarAsetKorwil.idPemohonBaru = idPemohon;
                formDaftarAsetKorwil.ShowDialog();
            }
            else if (teJenisPemohon.Text == konfigApp.levelEselon1)
            {
                if (formDaftarAsetEselon1 == null)
                {
                    formDaftarAsetEselon1 = new frmPuAsetNonPsp()
                    {
                        toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                        simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset),
                        isTb = (dataTerpilih.IS_TB == "Tanah dan Bangunan" ? "Y" : "N")
                    };
                }
                formDaftarAsetEselon1.isTb = rgJenisAset.EditValue.ToString();
                formDaftarAsetEselon1.idPemohonBaru = idPemohon;
                formDaftarAsetEselon1.idPemohonBaru = konfigApp.idSatker;
                formDaftarAsetEselon1.idSatker = konfigApp.idSatker;
                formDaftarAsetEselon1.kodeSatker = konfigApp.kodeSatker;
                formDaftarAsetEselon1.namaSatker = konfigApp.namaSatker;
                formDaftarAsetEselon1.teSatker.Properties.Items.Clear();
                formDaftarAsetEselon1.teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", konfigApp.idSatker, konfigApp.namaSatker));
                formDaftarAsetEselon1.teSatker.SelectedIndex = 0;
                formDaftarAsetEselon1.levelPenggunaBarang = konfigApp.levelEselon1;
                formDaftarAsetEselon1.idPemohonBaru = idPemohon;
                formDaftarAsetEselon1.ShowDialog();
            }
            else if (teJenisPemohon.Text == konfigApp.levelKl)
            {
                if (formDaftarAsetKl == null)
                {
                    formDaftarAsetKl = new frmPuAsetNonPsp()
                    {
                        toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                        simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset),
                        isTb = (dataTerpilih.IS_TB == "Tanah dan Bangunan" ? "Y" : "N")
                    };
                }
                formDaftarAsetKl.isTb = rgJenisAset.EditValue.ToString();
                formDaftarAsetKl.idPemohonBaru = idPemohon;
                formDaftarAsetKl.idPemohonBaru = konfigApp.idSatker;
                formDaftarAsetKl.idSatker = konfigApp.idSatker;
                formDaftarAsetKl.kodeSatker = konfigApp.kodeSatker;
                formDaftarAsetKl.namaSatker = konfigApp.namaSatker;
                formDaftarAsetKl.teSatker.Properties.Items.Clear();
                formDaftarAsetKl.teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", konfigApp.idSatker, konfigApp.namaSatker));
                formDaftarAsetKl.teSatker.SelectedIndex = 0;
                formDaftarAsetKl.levelPenggunaBarang = konfigApp.levelKl;
                formDaftarAsetKl.idPemohonBaru = idPemohon;
                formDaftarAsetKl.ShowDialog();
            }
        }
        #endregion

        #region --++ Simpan Daftar Aset Terpilih
        SvcWasdalManfaatDetailCrud.OutputParameters dOutSimpanAset;
        SvcWasdalManfaatDetailCrud.execute_pttClient simpanDftrAset;
        private char modeCudAset = 'A';
        private decimal? nilaiPenetapanLama = null;
        private decimal? jmlKuantitasLama = null;

        private void simpanDaftarAset(string _daftarIdAset, char _modeCudAset)
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalManfaatDetailCrud.InputParameters parInp = new SvcWasdalManfaatDetailCrud.InputParameters();
                parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                parInp.P_ID_SK_WASDAL_MANFAAT = idSkWasdal;
                parInp.P_SK_KEPUTUSAN = teNomorSk.Text.Trim();
                parInp.P_ID_ASET = _daftarIdAset;
                parInp.P_IS_VALID = (_modeCudAset == 'V') ? "Y" : "";
                parInp.P_TGL_SK = konfigApp.DateToString(teTanggalSk.DateTime);
                switch (_modeCudAset)
                {
                    case 'U':
                        parInp.P_NILAI_PERSETUJUANSpecified = true;
                        parInp.P_KUANTITASSpecified = true;
                        parInp.P_KUANTITAS = Convert.ToDecimal(formPuPenetapan.teKuantitas.Text);
                        parInp.P_NILAI_PERSETUJUAN = Convert.ToDecimal(formPuPenetapan.teNilaiPersetujuan.Text);
                        break;
                    case 'C':
                        parInp.P_NILAI_PERSETUJUANSpecified = false;
                        parInp.P_KUANTITASSpecified = false;
                        parInp.P_KD_PELAYANAN = "06";
                        break;
                    case 'D':
                        parInp.P_NILAI_PERSETUJUANSpecified = true;
                        parInp.P_KUANTITASSpecified = true;
                        parInp.P_KUANTITAS = 0;
                        parInp.P_NILAI_PERSETUJUAN = 0;
                        break;
                    case 'V':
                        parInp.P_NILAI_PERSETUJUANSpecified = false;
                        parInp.P_KUANTITASSpecified = false;
                        break;
                }
                parInp.P_JANGKA_WAKTU = (teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(teJangkaWaktu.Text));
                parInp.P_NM_PHK_LAIN = teNamaPihakLain.Text;
                parInp.P_PERIODE = this.konversiPeriode();
                parInp.P_KD_STATUS = "02";
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_TGL_SK = konfigApp.DateToString(teTanggalSk.DateTime);
                modeCudAset = _modeCudAset;
                parInp.P_SELECT = (modeCudAset=='V')?"V":modeCudAset.ToString();
                simpanDftrAset = new SvcWasdalManfaatDetailCrud.execute_pttClient();
                simpanDftrAset.Open();
                simpanDftrAset.Beginexecute(parInp, new AsyncCallback(goSimpanDftrAset), "");
            }
            catch (Exception e)
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

        private delegate void DsDaftarAset(SvcWasdalManfaatDetailCrud.OutputParameters dataOut);

        private void dsDaftarAset(SvcWasdalManfaatDetailCrud.OutputParameters dataOut)
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
                        SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT dataPenyama;
                        _krgNilTetap = (teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(teNilaiPenetapan.Text));
                        _krgKuantitas = (teKuantitas.Text == "" ? 0 : Convert.ToDecimal(teKuantitas.Text));
                        for (int i = 0; i < daftarAsetSalinan.Count; i++)
                        {
                            SvcWasdalAsetSelect.WASDALSROW_WASDAL_M_ASET daftarAsetTerpilih = (SvcWasdalAsetSelect.WASDALSROW_WASDAL_M_ASET)daftarAsetSalinan[i];
                            dataPenyama = null;
                            dataPenyama = new SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT();
                            dataPenyama.GUNA_WASDAL = "";
                            dataPenyama.ID_ASET = daftarAsetTerpilih.ID_ASET;
                            dataPenyama.ID_ASETSpecified = true;
                            dataPenyama.ID_PEMOHON = idPemohon;
                            dataPenyama.ID_PEMOHONSpecified = true;
                            dataPenyama.ID_SATKER = idSatker;
                            dataPenyama.ID_SATKERSpecified = true;
                            dataPenyama.ID_USER = konfigApp.idUser;
                            dataPenyama.ID_USERSpecified = true;
                            dataPenyama.IS_TB = rgJenisAset.EditValue.ToString();
                            dataPenyama.JABATAN_TTD = teJabatan.Text;
                            dataPenyama.KD_BRG = daftarAsetTerpilih.KD_BRG;
                            dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                            dataPenyama.KD_PEMOHON = teKodePemohon.Text;
                            dataPenyama.KD_PENERBIT_SK = "";
                            dataPenyama.KD_SATKER = _kodeSatker;
                            dataPenyama.KD_STATUS = daftarAsetTerpilih.KD_STATUS;
                            dataPenyama.KUANTITAS = daftarAsetTerpilih.KUANTITAS;
                            dataPenyama.KUANTITAS_SK = daftarAsetTerpilih.KUANTITAS;
                            dataPenyama.KUANTITAS_SKSpecified = true;
                            dataPenyama.KUANTITASSpecified = true;
                            dataPenyama.NILAI_PENETAPAN = null;
                            dataPenyama.NILAI_PENETAPANSpecified = daftarAsetTerpilih.NILAI_BUKUSpecified;
                            dataPenyama.NILAI_PERSETUJUAN = daftarAsetTerpilih.NILAI_SBLM_SUSUT;
                            dataPenyama.NILAI_PERSETUJUANSpecified = daftarAsetTerpilih.NILAI_SBLM_SUSUTSpecified;
                            dataPenyama.NIP_PENANDATANGAN = teNipPenandaTangan.Text;
                            dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                            dataPenyama.NM_PEMOHON = teNamaPemohon.Text;
                            dataPenyama.NM_PENANDATANGAN = teNamaPenandaTangan.Text;
                            dataPenyama.NM_PENERBIT_SK = lePenerbitSk.Text;
                            dataPenyama.NM_PENGGUNA = "";
                            dataPenyama.NO_ASET = daftarAsetTerpilih.NO_ASET;
                            dataPenyama.NO_ASETSpecified = true;
                            dataPenyama.NOREG = daftarAsetTerpilih.NOREG;
                            dataPenyama.NUM = (dsGridDaftarAset == null ? 1 : dsGridDaftarAset.Count + 1);
                            dataPenyama.NUMSpecified = false;
                            dataPenyama.SK_KEPUTUSAN = teNomorSk.Text;
                            dataPenyama.TGL_CREATED = DateTime.Now;
                            dataPenyama.TGL_CREATEDSpecified = true;
                            dataPenyama.TGL_SK = Convert.ToDateTime(teTanggalSk.EditValue);
                            dataPenyama.TGL_SKSpecified = true;
                            dataPenyama.TIPE_PEMOHON = teJenisPemohon.Text;
                            dataPenyama.TOT_BMN = null;// daftarAsetTerpilih.TOT_BMN;
                            dataPenyama.TOT_BMNSpecified = true;// daftarAsetTerpilih.TOT_BMNSpecified;
                            dataPenyama.TOT_STATUS = null;// daftarAsetTerpilih.TOT_STATUS;
                            dataPenyama.TOT_STATUSSpecified = true;// daftarAsetTerpilih.TOT_STATUSSpecified;
                            dataPenyama.TOTAL_DATA = daftarAsetTerpilih.TOTAL_DATA;
                            dataPenyama.TOTAL_DATASpecified = daftarAsetTerpilih.TOTAL_DATASpecified;
                            dataPenyama.UR_SATKER = _namaSatker;
                            dataPenyama.UR_SSKEL = daftarAsetTerpilih.UR_SSKEL;
                            dataPenyama.UR_STATUS = daftarAsetTerpilih.UR_STATUS;
                            dataPenyama.URAIAN_KEPUTUSAN = teUraianKeputusan.Text;
                            _krgNilTetap = _krgNilTetap + daftarAsetTerpilih.NILAI_SBLM_SUSUT;
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
                                SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT daftarAsetTerpilih = (SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT)daftarTerpilih[i];
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
                        dataPenyama = new SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT();
                        dataPenyama.GUNA_WASDAL = dataAsetTerpilih.GUNA_WASDAL;
                        dataPenyama.ID_ASET = dataAsetTerpilih.ID_ASET;
                        dataPenyama.ID_ASETSpecified = dataAsetTerpilih.ID_ASETSpecified;
                        dataPenyama.ID_PEMOHON = dataAsetTerpilih.ID_PEMOHON;
                        dataPenyama.ID_PEMOHONSpecified = dataAsetTerpilih.ID_PEMOHONSpecified;
                        dataPenyama.ID_SATKER = dataAsetTerpilih.ID_SATKER;
                        dataPenyama.ID_SATKERSpecified = dataAsetTerpilih.ID_SATKERSpecified;
                        dataPenyama.ID_USER = dataAsetTerpilih.ID_USER;
                        dataPenyama.ID_USERSpecified = dataAsetTerpilih.ID_USERSpecified;
                        dataPenyama.IS_TB = dataAsetTerpilih.IS_TB;
                        dataPenyama.JABATAN_TTD = dataAsetTerpilih.JABATAN_TTD;
                        dataPenyama.KD_BRG = dataAsetTerpilih.KD_BRG;
                        dataPenyama.KD_PELAYANAN = dataAsetTerpilih.KD_PELAYANAN;
                        dataPenyama.KD_PEMOHON = dataAsetTerpilih.KD_PEMOHON;
                        dataPenyama.KD_PENERBIT_SK = dataAsetTerpilih.KD_PENERBIT_SK;
                        dataPenyama.KD_SATKER = dataAsetTerpilih.KD_SATKER;
                        dataPenyama.KD_STATUS = dataAsetTerpilih.KD_STATUS;
                        dataPenyama.KUANTITAS = Convert.ToDecimal(formPuPenetapan.teKuantitas.Text);
                        dataPenyama.KUANTITAS_SK = dataAsetTerpilih.KUANTITAS_SK;
                        dataPenyama.KUANTITAS_SKSpecified = dataAsetTerpilih.KUANTITAS_SKSpecified;
                        dataPenyama.KUANTITASSpecified = dataAsetTerpilih.KUANTITASSpecified;
                        dataPenyama.NILAI_PENETAPAN = dataAsetTerpilih.NILAI_PENETAPAN;
                        dataPenyama.NILAI_PENETAPANSpecified = dataAsetTerpilih.NILAI_PENETAPANSpecified;
                        dataPenyama.NILAI_PERSETUJUAN = Convert.ToDecimal(formPuPenetapan.teNilaiPersetujuan.Text);
                        dataPenyama.NILAI_PERSETUJUANSpecified = dataAsetTerpilih.NILAI_PERSETUJUANSpecified;
                        dataPenyama.NIP_PENANDATANGAN = dataAsetTerpilih.NIP_PENANDATANGAN;
                        dataPenyama.NM_PELAYANAN = dataAsetTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = dataAsetTerpilih.NM_PEMOHON;
                        dataPenyama.NM_PENANDATANGAN = dataAsetTerpilih.NM_PENANDATANGAN;
                        dataPenyama.NM_PENERBIT_SK = dataAsetTerpilih.NM_PENERBIT_SK;
                        dataPenyama.NM_PENGGUNA = dataAsetTerpilih.NM_PENGGUNA;
                        dataPenyama.NO_ASET = dataAsetTerpilih.NO_ASET;
                        dataPenyama.NO_ASETSpecified = dataAsetTerpilih.NO_ASETSpecified;
                        dataPenyama.NOREG = dataAsetTerpilih.NOREG;
                        dataPenyama.NUM = dataAsetTerpilih.NUM;
                        dataPenyama.NUMSpecified = dataAsetTerpilih.NUMSpecified;
                        dataPenyama.SK_KEPUTUSAN = dataAsetTerpilih.SK_KEPUTUSAN;
                        dataPenyama.TGL_CREATED = dataAsetTerpilih.TGL_CREATED;
                        dataPenyama.TGL_CREATEDSpecified = dataAsetTerpilih.TGL_CREATEDSpecified;
                        dataPenyama.TGL_SK = dataAsetTerpilih.TGL_SK;
                        dataPenyama.TGL_SKSpecified = dataAsetTerpilih.TGL_SKSpecified;
                        dataPenyama.TIPE_PEMOHON = dataAsetTerpilih.TIPE_PEMOHON;
                        dataPenyama.TOT_BMN = dataAsetTerpilih.TOT_BMN;
                        dataPenyama.TOT_BMNSpecified = dataAsetTerpilih.TOT_BMNSpecified;
                        dataPenyama.TOT_STATUS = dataAsetTerpilih.TOT_STATUS;
                        dataPenyama.TOT_STATUSSpecified = dataAsetTerpilih.TOT_STATUSSpecified;
                        dataPenyama.TOTAL_DATA = dataAsetTerpilih.TOTAL_DATA;
                        dataPenyama.TOTAL_DATASpecified = dataAsetTerpilih.TOTAL_DATASpecified;
                        dataPenyama.UR_SATKER = dataAsetTerpilih.UR_SATKER;
                        dataPenyama.UR_SSKEL = dataAsetTerpilih.UR_SSKEL;
                        dataPenyama.UR_STATUS = dataAsetTerpilih.UR_STATUS;
                        dataPenyama.URAIAN_KEPUTUSAN = dataAsetTerpilih.URAIAN_KEPUTUSAN;
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
            SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT daftarAsetTerpilih;
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
                        daftarAsetTerpilih = (SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT)daftarTerpilih[i];
                        daftarIdAset = String.Format("{0}{1};", daftarIdAset, daftarAsetTerpilih.ID_ASET);
                    }
                    daftarIdAset = daftarIdAset.TrimEnd(';');
                    if (MessageBox.Show("Apakah Anda ingin menghapus Daftar Aset yang terpilih?", konfigApp.judulHapusData,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        simpanDaftarAset(daftarIdAset, 'D');
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
                dataAsetTerpilih = (SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT)rowTerpilih.GetRow(e.FocusedRowHandle);
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
        //private void gvDaftarAset_DoubleClick(object sender, EventArgs e)
        //{
        //    formUbahPenetapan();
        //}
        private void gvDaftarAset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //formUbahPenetapan();
            }
        }
        #endregion

        private void teTanggalSk_EditValueChanged(object sender, EventArgs e)
        {
            teTahunAnggaran.Text = konfigApp.DateToYear(teTanggalSk.DateTime);
        }
    

        #endregion

        private void sbExport_Click(object sender, EventArgs e)
        {
            frmPuCariAset frmcariaset = null;
            if (frmcariaset == null)
            {
                frmcariaset = new frmPuCariAset()
                {
                    toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                    nosk = teNomorSk.Text.Trim(),
                    menu = "sewa"
                };
            }
            frmcariaset.ShowDialog();
            //int jmlKolom = gvDaftarAset.Columns.Count;

            //for (int i = 0; i < jmlKolom; i++)
            //{
            //    if (gvDaftarAset.Columns[i].Visible == true)
            //    {
            //        frmcariaset.gvAset.Columns.AddVisible(gvDaftarAset.Columns[i].FieldName, gvDaftarAset.Columns[i].Caption);
            //        frmcariaset.gvAset.Columns[i].Caption = gvDaftarAset.Columns[i].Caption;
            //        frmcariaset.gvAset.Columns[i].FieldName = gvDaftarAset.Columns[i].FieldName;
            //    }
            //}

            //frmcariaset.gcAset.DataSource = null;
            //frmcariaset.gcAset.DataSource = dsGridDaftarAset;
            //frmcariaset.datasource = dsGridDaftarAset;
            //frmcariaset.gvAset.BestFitColumns();
            
            
        }

        private void sbValidasi_Click(object sender, EventArgs e)
        {
            AppPengguna.KKW.RSK.PU.frmPuCariAset frmcariaset = null;
            int jmlKolom = gvDaftarAset.Columns.Count;
            if (frmcariaset == null)
            {
                frmcariaset = new AppPengguna.KKW.RSK.PU.frmPuCariAset()
                {

                    toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                    simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset),
                    nosk = teNomorSk.Text.Trim(),
                    idSatker = konfigApp.idSatker,
                    idSkWasdal = this.idSkWasdal,
                    menu = "sewa"
                };
            }
            frmcariaset.ShowDialog();
            getDaftarAset();
        }

        private void gvDaftarAset_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT daftarAsetTerpilih;
                daftarAsetTerpilih = (SvcWasdalManfaatSelect.WASDALSROW_WASDAL_MANFAAT)view.GetRow(info.RowHandle);
                idasetnp = daftarAsetTerpilih.ID_ASET.ToString();
                frmNilaiPersetujuan frm = new frmNilaiPersetujuan();
                frm.np = daftarAsetTerpilih.NILAI_PERSETUJUAN;
                frm.simpanNP = new simpanNilaiPersetujuan(simpanNP);
                frm.ShowDialog();
            }
        }

        string idasetnp;
        public void simpanNP(decimal? nilai)
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalManfaatDetailCrud.InputParameters parInp = new SvcWasdalManfaatDetailCrud.InputParameters();
                parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                parInp.P_ID_SK_WASDAL_MANFAAT = idSkWasdal;
                parInp.P_SK_KEPUTUSAN = teNomorSk.Text.Trim();
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(teTanggalSk.Text));
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_ID_ASET = idasetnp;
                parInp.P_NILAI_PERSETUJUAN = nilai;
                parInp.P_NILAI_PERSETUJUANSpecified = true;
                modeCudAset = 'U';
                parInp.P_SELECT = (modeCudAset == 'V') ? "V" : Convert.ToString(modeCudAset);
                simpanDftrAset = new SvcWasdalManfaatDetailCrud.execute_pttClient();
                simpanDftrAset.Open();
                simpanDftrAset.Beginexecute(parInp, new AsyncCallback(goUpdateDftrAset), "");
            }
            catch
            {
                modeCudAset = 'A';
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulGagalLain);
            }
        }

        private void goUpdateDftrAset(IAsyncResult result)
        {
            try
            {
                dOutSimpanAset = simpanDftrAset.Endexecute(result);
                simpanDftrAset.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
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
            finally
            {
                getDaftarAset();
            }
        }
    }
}
