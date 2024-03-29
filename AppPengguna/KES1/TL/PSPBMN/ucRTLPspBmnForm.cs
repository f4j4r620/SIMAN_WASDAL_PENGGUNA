﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KES1.TL.PSPBMN
{
    public partial class ucRTLPspBmnForm : UserControl
    {
        public ToggleProgressBar toggleProgressBar;
        public SimpanDataRtl simpanDataRtlPspBmn;
        public string statusForm = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public decimal? idPemohon = null;
        public string kodePenerbitSkDetail = null;
        public string namaPenerbitSkDetail = null;
        private char modeCrud = 'A';
        public SvcWasdalPSPBMNSkSelect.WASDALSROW_READ_WASDAL_PSP dataTerpilih;
        private bool instansiLoaded = false;
        private decimal? idSatker = null;
        private string noSkLama = null;
        private string noSkBaru = null;

        public ucRTLPspBmnForm(string _status)
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
                input.STR_WHERE = "";
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
            //teAlamatPihakLain.Text = "";
            //tePihakLainKodeKl.Text = "";
            //tePihakLainNamaKl.Text = "";
            //teJangkaWaktu.Text = "";
            //tePeruntukan.Text = "";
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
                DateTime tanggalSurat = Convert.ToDateTime(dataTerpilih.TGL_SK);
                teTanggalSk.EditValue = tanggalSurat;
                teTahunAnggaran.Text = tanggalSurat.Year.ToString();
                teJenisPemohon.Text = dataTerpilih.TIPE_PEMOHON;
                //if (teJenisPemohon.SelectedIndex != 3)
                //{
                //    lciKosong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //    lciKodeKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //    lciNamaKl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //}
                idPemohon = dataTerpilih.ID_PEMOHON;
                teKodePemohon.Text = dataTerpilih.KD_PEMOHON;
                teNamaPemohon.Text = dataTerpilih.NM_PEMOHON;
                teKodeKl.Text = dataTerpilih.KD_KL;
                teNamaKl.Text = dataTerpilih.UR_KL;
                //teNamaInstansi.Text = dataTerpilih.NM_PENERBIT_SK;
                //teNamaInstansi.EditValue = dataTerpilih.KD_PENERBIT_SK;
                teNilaiPenetapan.Text = Convert.ToDecimal(dataTerpilih.NILAI_PENETAPAN).ToString("n2");
                teKuantitas.Text = dataTerpilih.KUANTITAS_SK.ToString();
                teNamaPenandaTangan.Text = dataTerpilih.NM_PENANDATANGAN;
                teNipPenandaTangan.Text = dataTerpilih.NIP_PENANDATANGAN;
                teJabatan.Text = dataTerpilih.JABATAN_TTD;
                teUraianKeputusan.Text = dataTerpilih.URAIAN_KEPUTUSAN;
                //tePihakLainKodeKl.Text = "";
                //tePihakLainNamaKl.Text = "";
                //teJangkaWaktu.Text = "";
                //teAlamatPihakLain.Text = "";
            }
            if (instansiLoaded == false)
                getInstansi();
            else
            {
                if (statusForm != "C" && statusForm != "CU")
                {
                    if (noSkLama != noSkBaru)
                        getDaftarAset();
                    else
                    {
                        gcDaftarAset.DataSource = dsGridDaftarAset;
                        gcDaftarAset.RefreshDataSource();
                    }
                }
            }
            

            rgJenisAset.Properties.ReadOnly = false;
            teNomorSk.Properties.ReadOnly = false;
            teTanggalSk.Properties.ReadOnly = false;
            teJenisPemohon.Properties.ReadOnly = false;
            teNamaInstansi.Properties.ReadOnly = false;
            teNamaPenandaTangan.Properties.ReadOnly = false;
            teNipPenandaTangan.Properties.ReadOnly = false;
            teJabatan.Properties.ReadOnly = false;
            teUraianKeputusan.Properties.ReadOnly = false;
            sbSimpan.Enabled = false;
            sbCariPemohon.Enabled = true;
            
            sbRefresh.Enabled = false;
            gcDaftarAset.Enabled = false;
            cePilihSemua.Enabled = false;
            teKodePemohon.Properties.ReadOnly = true;
            switch (statusForm)
            {
                case "C":
                case "CU":
                    teTahunAnggaran.Text = konfigApp.tahunAnggaran.ToString();
                    teNomorSk.Focus();
                    teKodePemohon.Properties.ReadOnly = false;
                    break;
                case "U":
                    gcDaftarAset.Enabled = true;
                    rgJenisAset.Properties.ReadOnly = true;
                    teNomorSk.Properties.ReadOnly = true;
                    teTanggalSk.Properties.ReadOnly = true;
                    teJenisPemohon.Properties.ReadOnly = true;
                    teNamaInstansi.Properties.ReadOnly = true;
                    sbCariPemohon.Enabled = false;
                    sbRefresh.Enabled = true;
                    cePilihSemua.Enabled = true;
                    break;
                case "A":
                    rgJenisAset.Properties.ReadOnly = true;
                    teNomorSk.Properties.ReadOnly = true;
                    teTanggalSk.Properties.ReadOnly = true;
                    teJenisPemohon.Properties.ReadOnly = true;
                    teNamaInstansi.Properties.ReadOnly = true;
                    teNamaPenandaTangan.Properties.ReadOnly = true;
                    teNipPenandaTangan.Properties.ReadOnly = true;
                    teJabatan.Properties.ReadOnly = true;
                    teUraianKeputusan.Properties.ReadOnly = true;
                    sbSimpan.Enabled = false;
                    sbCariPemohon.Enabled = false;
                    sbSimpan.Enabled = false;
                    gcDaftarAset.Enabled = true;
                    sbRefresh.Enabled = true;
                    gridColPilih.OptionsColumn.AllowEdit = false;
                    gridColPilih.OptionsColumn.ReadOnly = true;
                    break;
            }
            switch (konfigApp.levelUser)
            {
                case "KPKNL":
                    teJenisPemohon.Properties.Items.Clear();
                    teJenisPemohon.Properties.Items.Insert(0, konfigApp.levelSatker);
                    xtpIdentitas.Text = String.Format("KPKNL: [{0}] {1}", konfigApp.kodeKpknl, konfigApp.namaKpknl);
                    kodePenerbitSkDetail = konfigApp.kodeKpknl;
                    namaPenerbitSkDetail = konfigApp.namaKpknl;
                    break;
                case "KANWIL":
                    teJenisPemohon.Properties.Items.Clear();
                    teJenisPemohon.Properties.Items.Insert(0, konfigApp.levelSatker);
                    teJenisPemohon.Properties.Items.Insert(1, konfigApp.levelKorwil);
                    xtpIdentitas.Text = String.Format("KANWIL: [{0}] {1}", konfigApp.kodeKanwil, konfigApp.namaKanwil);
                    kodePenerbitSkDetail = konfigApp.kodeKanwil;
                    namaPenerbitSkDetail = konfigApp.namaKanwil;
                    break;
                case "KPDJKN":
                    teJenisPemohon.Properties.Items.Clear();
                    teJenisPemohon.Properties.Items.Insert(0, konfigApp.levelSatker);
                    teJenisPemohon.Properties.Items.Insert(1, konfigApp.levelKorwil);
                    teJenisPemohon.Properties.Items.Insert(2, konfigApp.levelEselon1);
                    teJenisPemohon.Properties.Items.Insert(3, konfigApp.levelKl);
                    xtpIdentitas.Text = "KP DJKN: KANTOR PUSAT DJKN";
                    //kodePenerbitSkDetail = konfigApp.kodeKpDjkn;
                    //namaPenerbitSkDetail = konfigApp.namaKpDjkn;
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
        KSK.RSK.PU.frmPuSatker formPuSatker;
        KSK.RSK.PU.frmPuKorwil formPuKorwil;
        KSK.RSK.PU.frmPuEselon1 formPuEselon1;
        KSK.RSK.PU.frmPuKl formPuKl;

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
                        formPuSatker = new KSK.RSK.PU.frmPuSatker()
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
                        formPuKorwil = new KSK.RSK.PU.frmPuKorwil()
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
                        formPuEselon1 = new KSK.RSK.PU.frmPuEselon1()
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
                        formPuKl = new KSK.RSK.PU.frmPuKl()
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
            teKodePemohon.Text = _kodeUnitKerja;
            teNamaPemohon.Text = _namaUnitKerja;
            teKodeKl.Text = _kodeKl;
            teNamaKl.Text = _namaKl;
            idPemohon = _idUnitKerja;
            if (teJenisPemohon.Text == konfigApp.levelSatker) idSatker = _idUnitKerja;
        }

        private void sbSimpan_Click(object sender, EventArgs e)
        {
            if (rgJenisAset.SelectedIndex != -1 && teNomorSk.Text.Trim() != "" && teNamaPemohon.Text != "" && teTanggalSk.Text != "" && teNamaInstansi.Text.Trim() != "")
            {
                if (statusForm == "C" || statusForm == "U" || statusForm == "CU")
                    simpanDataRtlPspBmn(statusForm);
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

        #region Cari Pihak ketiga dari daftar K/L
        private void tePihakLainKodeKl_EditValueChanged(object sender, EventArgs e)
        {
            //tePihakLainNamaKl.Text = "";
            //teAlamatPihakLain.Text = "";
        }

        private void sbCariPihakLain_Click(object sender, EventArgs e)
        {
            string preWhere = "";
            if (konfigApp.levelUser == konfigApp.levelSatker)
            {
                preWhere = " ID_SATKER = " + konfigApp.idSatker;
            }
            else if (konfigApp.levelUser == konfigApp.levelKorwil)
            {
                preWhere = " ID_KORWIL = " + konfigApp.idKorwil;
            }
            else if (konfigApp.levelUser == konfigApp.levelEselon1)
            {
                preWhere = " ID_E1 = " + konfigApp.idEselon1;
            }
            else if (konfigApp.levelUser == konfigApp.levelKl)
            {
                preWhere = " ID_KL = " + konfigApp.idKl;
            }
            if (formPuKl == null)
            {
                formPuKl = new KSK.RSK.PU.frmPuKl()
                {
                    toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                    selectUnitKerja = new SelectDataUnitKerja(setUnitKerjaPihakLain),
                    whereAwal = preWhere
                };
            }
            //if (tePihakLainKodeKl.Text.Trim() != "")
            //    formPuKl.idManualBaru = tePihakLainKodeKl.Text.Trim();
            //else formPuKl.idManualBaru = "";
            formPuKl.idManualBaru = "";
            formPuKl.ShowDialog();
        }

        private void setUnitKerjaPihakLain(decimal? _idUnitKerja, string _kodeUnitKerja, string _namaUnitKerja, string _alamatKl, string _namaKl)
        {
            //tePihakLainKodeKl.Text = _kodeUnitKerja;
            //tePihakLainNamaKl.Text = _namaUnitKerja;
            //teAlamatPihakLain.Text = _alamatKl;
        }
        #endregion

        #endregion

        #region Bagian Detail: Aset
        #region --++ Ambil Daftar Aset dalam SK
        SvcWasdalPSPBMNSelect.OutputParameters dOutDaftarAset;
        SvcWasdalPSPBMNSelect.execute_pttClient ambilDaftarAset;
        SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP dataAsetTerpilih;
        private ArrayList dsGridDaftarAset;

        public void getDaftarAset()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                svcInstansi = new SvcInstansiSelect.dsSelect_pttClient();
                SvcWasdalPSPBMNSelect.InputParameters parInput = new SvcWasdalPSPBMNSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format(" (SK_KEPUTUSAN = '{0}') ", teNomorSk.Text.Trim());
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
            decimal? nilaiPersetujuan = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData > 1 || outputDaftarAset.SF_ROW_WASDAL_PSP[0].KD_BRG != "-")
            {
                for (int i = 0; i < jmlData; i++)
                {
                    outputDaftarAset.SF_ROW_WASDAL_PSP[i].IS_TB = (outputDaftarAset.SF_ROW_WASDAL_PSP[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAset.SF_ROW_WASDAL_PSP[i].NUMSpecified = false;
                    dsGridDaftarAset.Add(outputDaftarAset.SF_ROW_WASDAL_PSP[i]);
                    nilaiPersetujuan = nilaiPersetujuan + outputDaftarAset.SF_ROW_WASDAL_PSP[i].NILAI_PERSETUJUAN;
                    jmlKuantitas = jmlKuantitas + outputDaftarAset.SF_ROW_WASDAL_PSP[i].KUANTITAS;
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
        KES1.TL.PU.frmPuAset formDaftarAsetSatker;
        KES1.TL.PU.frmPuAset formDaftarAsetKorwil;
        KES1.TL.PU.frmPuAset formDaftarAsetEselon1;
        KES1.TL.PU.frmPuAset formDaftarAsetKl;

        private void sbTambah_Click(object sender, EventArgs e)
        {
            //if (teJenisPemohon.Text == konfigApp.levelSatker)
            //{
            //    if (formDaftarAsetSatker == null)
            //    {
            //        formDaftarAsetSatker = new KES1.TL.PU.frmPuAset()
            //        {
            //            toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
            //            simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset)
            //        };
            //    }
            //    formDaftarAsetSatker.isTb = rgJenisAset.EditValue.ToString();
            //    formDaftarAsetSatker.idPemohonBaru = idPemohon;
            //    formDaftarAsetSatker.idSatker = idPemohon;
            //    formDaftarAsetSatker.kodeSatker = teKodePemohon.Text;
            //    formDaftarAsetSatker.namaSatker = teNamaPemohon.Text;
            //    formDaftarAsetSatker.teSatker.Properties.Items.Clear();
            //    formDaftarAsetSatker.teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", teKodePemohon.Text, teNamaPemohon.Text));
            //    formDaftarAsetSatker.teSatker.SelectedIndex = 0;
            //    formDaftarAsetSatker.levelPenggunaBarang = konfigApp.levelSatker;
            //    formDaftarAsetSatker.ShowDialog();
            //}
            //else if (teJenisPemohon.Text == konfigApp.levelKorwil)
            //{
            //    if (formDaftarAsetKorwil == null)
            //    {
            //        formDaftarAsetKorwil = new KES1.TL.PU.frmPuAset()
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
            //        formDaftarAsetEselon1 = new KES1.TL.PU.frmPuAset()
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
            //        formDaftarAsetKl = new KES1.TL.PU.frmPuAset()
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
        SvcWasdalPSPBMNDetailCrud.OutputParameters dOutSimpanAset;
        SvcWasdalPSPBMNDetailCrud.execute_pttClient simpanDftrAset;

        private char modeCudAset = 'A';
        private decimal? nilaiPenetapanLama = null;
        private decimal? jmlKuantitasLama = null;

        //private void simpanDaftarAset(string _daftarIdAset, char _modeCudAset)
        //{
        //    try
        //    {
        //        myThread = new Thread(new ThreadStart(aktifkanProgresBar));
        //        myThread.Start();
        //        SvcWasdalPSPBMNDetailCrud.InputParameters parInp = new SvcWasdalPSPBMNDetailCrud.InputParameters();
        //        parInp.P_SK_KEPUTUSAN = teNomorSk.Text.Trim();
        //        parInp.P_ID_ASET = _daftarIdAset;
        //        parInp.P_NILAI_PERSETUJUANSpecified = false;
        //        parInp.P_KUANTITASSpecified = false;
        //        switch (_modeCudAset)
        //        {
        //            case 'U':
        //                //parInp.P_KUANTITAS = Convert.ToDecimal(formPuPenetapan.teKuantitas.Text);
        //                //parInp.P_NILAI_PERSETUJUAN = Convert.ToDecimal(formPuPenetapan.teNilaiPersetujuan.Text);
        //                break;
        //            case 'C':
        //                //parInp.P_DARI_TGL = null;
        //                //parInp.P_JANGKA_WAKTU = (teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(teJangkaWaktu.Text));
        //                //parInp.P_JANGKA_WAKTUSpecified = true;
        //                //parInp.P_JNS_BUKTI_LAKSANA = "";
        //                //parInp.P_NM_PHK_LAIN = tePihakLainNamaKl.Text;
        //                //parInp.P_NO_BUKTI_LAKSANA = "";
        //                //parInp.P_PERIODE = "";
        //                //parInp.P_SD_TGL = null;
        //                //parInp.P_TGL_BUKTI_LAKSANA = null;
        //                parInp.P_KUANTITAS = 0;
        //                parInp.P_NILAI_PERSETUJUAN = 0;
        //                break;
        //            case 'D':
        //                parInp.P_KUANTITAS = 0;
        //                parInp.P_NILAI_PERSETUJUAN = 0;
        //                break;
        //        }
        //        parInp.P_KD_STATUS = "04";
        //        modeCudAset = _modeCudAset;
        //        parInp.P_SELECT = Convert.ToString(modeCudAset);
        //        simpanDftrAset = new SvcWasdalPSPBMNDetailCrud.call_pttClient();
        //        simpanDftrAset.Open();
        //        simpanDftrAset.Beginexecute(parInp, new AsyncCallback(goSimpanDftrAset), "");
        //    }
        //    catch
        //    {
        //        modeCudAset = 'A';
        //        nonAktifkanprogressBar();
        //        this.Invoke(new AktifkanForm(aktifkanForm), "");
        //        MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulGagalLain);
        //    }
        //}

        private void simpanRkmTindakLanjut(string _daftarIdAset,string _kdStatus, char _modeCudAset)
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNDetailCrud.InputParameters parInp = new SvcWasdalPSPBMNDetailCrud.InputParameters();
                parInp.P_SK_KEPUTUSAN = teNomorSk.Text.Trim();
                parInp.P_ID_ASET = _daftarIdAset;
                parInp.P_NILAI_PERSETUJUANSpecified = false;
                parInp.P_KUANTITASSpecified = false;
                
                parInp.P_KD_STATUS = _kdStatus;
                modeCudAset = _modeCudAset;
                parInp.P_SELECT = Convert.ToString(modeCudAset);
                simpanDftrAset = new SvcWasdalPSPBMNDetailCrud.execute_pttClient();
                simpanDftrAset.Open();
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

        private delegate void DsDaftarAset(SvcWasdalPSPBMNDetailCrud.OutputParameters dataOut);

        private void dsDaftarAset(SvcWasdalPSPBMNDetailCrud.OutputParameters dataOut)
        {
            if (dataOut.PO_RESULT == "Y")
            {
                decimal? _krgNilTetap = null;
                decimal? _krgKuantitas = null;
                SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP dataPenyama;
                if (dsGridDaftarAset == null) dsGridDaftarAset = new ArrayList();
                switch (modeCudAset)
                {
                    //case 'C':
                    //    string _kodeSatker = "";
                    //    string _namaSatker = "";
                    //    if (teJenisPemohon.Text == konfigApp.levelSatker)
                    //    {
                    //        daftarAsetSalinan = formDaftarAsetSatker.daftarTerpilih;
                    //        _kodeSatker = formDaftarAsetSatker.kodeSatker;
                    //        _namaSatker = formDaftarAsetSatker.namaSatker;
                    //    }
                    //    else if (teJenisPemohon.Text == konfigApp.levelKorwil)
                    //    {
                    //        daftarAsetSalinan = formDaftarAsetKorwil.daftarTerpilih;
                    //        _kodeSatker = formDaftarAsetKorwil.kodeSatker;
                    //        _namaSatker = formDaftarAsetKorwil.namaSatker;
                    //    }
                    //    else if (teJenisPemohon.Text == konfigApp.levelEselon1)
                    //    {
                    //        daftarAsetSalinan = formDaftarAsetEselon1.daftarTerpilih;
                    //        _kodeSatker = formDaftarAsetEselon1.kodeSatker;
                    //        _namaSatker = formDaftarAsetEselon1.namaSatker;
                    //    }
                    //    else if (teJenisPemohon.Text == konfigApp.levelKl)
                    //    {
                    //        daftarAsetSalinan = formDaftarAsetKl.daftarTerpilih;
                    //        _kodeSatker = formDaftarAsetKl.kodeSatker;
                    //        _namaSatker = formDaftarAsetKl.namaSatker;
                    //    }
                    //    SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP dataPenyama;
                    //    _krgNilTetap = (teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(teNilaiPenetapan.Text));
                    //    _krgKuantitas = (teKuantitas.Text == "" ? 0 : Convert.ToDecimal(teKuantitas.Text));
                    //    for (int i = 0; i < daftarAsetSalinan.Count; i++)
                    //    {
                    //        SvcWasdalAsetSelect.SBSNSROW_SBSN_M_ASET daftarAsetTerpilih = (SvcWasdalAsetSelect.SBSNSROW_SBSN_M_ASET)daftarAsetSalinan[i];
                    //        dataPenyama = null;
                    //        dataPenyama = new SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP();
                    //        dataPenyama.ID_ASET = daftarAsetTerpilih.ID_ASET;
                    //        dataPenyama.ID_ASETSpecified = true;
                    //        dataPenyama.ID_PEMOHON = idPemohon;
                    //        dataPenyama.ID_PEMOHONSpecified = true;
                    //        dataPenyama.ID_SATKER = idSatker;
                    //        dataPenyama.ID_SATKERSpecified = true;
                    //        dataPenyama.ID_USER = konfigApp.idUser;
                    //        dataPenyama.ID_USERSpecified = true;
                    //        dataPenyama.IS_TB = rgJenisAset.EditValue.ToString();
                    //        dataPenyama.JABATAN_TTD = teJabatan.Text;
                    //        dataPenyama.KD_BRG = daftarAsetTerpilih.KD_BRG;
                    //        dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                    //        dataPenyama.KD_PEMOHON = teKodePemohon.Text;
                    //        dataPenyama.KD_PENERBIT_SK = "";
                    //        dataPenyama.KD_SATKER = _kodeSatker;
                    //        dataPenyama.KD_STATUS = "04";//daftarAsetTerpilih.KD_STATUS;
                    //        dataPenyama.KUANTITAS = daftarAsetTerpilih.KUANTITAS; //perlu dicek
                    //        dataPenyama.KUANTITAS_SK = daftarAsetTerpilih.KUANTITAS;
                    //        dataPenyama.KUANTITAS_SKSpecified = false;
                    //        dataPenyama.KUANTITASSpecified =false;
                    //        dataPenyama.NILAI_PENETAPAN = null;
                    //        dataPenyama.NILAI_PENETAPANSpecified = daftarAsetTerpilih.NILAI_BUKUSpecified;
                    //        dataPenyama.NILAI_PERSETUJUAN = daftarAsetTerpilih.NILAI_BUKU;
                    //        dataPenyama.NILAI_PERSETUJUANSpecified = daftarAsetTerpilih.NILAI_BUKUSpecified;
                    //        dataPenyama.NIP_PENANDATANGAN = teNipPenandaTangan.Text;
                    //        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                    //        dataPenyama.NM_PEMOHON = teNamaPemohon.Text;
                    //        dataPenyama.NM_PENANDATANGAN = teNamaPenandaTangan.Text;
                    //        dataPenyama.NM_PENERBIT_SK = teNamaInstansi.Text;
                    //        dataPenyama.NM_PENGGUNA = "";
                    //        dataPenyama.NO_ASET = daftarAsetTerpilih.NO_ASET;
                    //        dataPenyama.NO_ASETSpecified = true;
                    //        dataPenyama.NOREG = daftarAsetTerpilih.NOREG;
                    //        dataPenyama.NUM = (dsGridDaftarAset == null ? 1 : dsGridDaftarAset.Count + 1);
                    //        dataPenyama.NUMSpecified = false;
                    //        dataPenyama.SK_KEPUTUSAN = teNomorSk.Text;
                    //        dataPenyama.TGL_CREATED = DateTime.Now;
                    //        dataPenyama.TGL_CREATEDSpecified = true;
                    //        dataPenyama.TGL_SK = Convert.ToDateTime(teTanggalSk.EditValue);
                    //        dataPenyama.TGL_SKSpecified = true;
                    //        dataPenyama.TIPE_PEMOHON = teJenisPemohon.Text;
                    //        dataPenyama.TOT_BMN = null;// daftarAsetTerpilih.TOT_BMN;
                    //        dataPenyama.TOT_BMNSpecified = true;// daftarAsetTerpilih.TOT_BMNSpecified;
                    //        dataPenyama.TOT_STATUS = null;// daftarAsetTerpilih.TOT_STATUS;
                    //        dataPenyama.TOT_STATUSSpecified = true;// daftarAsetTerpilih.TOT_STATUSSpecified;
                    //        dataPenyama.TOTAL_DATA = daftarAsetTerpilih.TOTAL_DATA;
                    //        dataPenyama.TOTAL_DATASpecified = daftarAsetTerpilih.TOTAL_DATASpecified;
                    //        dataPenyama.UR_SATKER = _namaSatker;
                    //        dataPenyama.UR_SSKEL = daftarAsetTerpilih.UR_SSKEL;
                    //        dataPenyama.UR_STATUS = "Digunakan oleh satker lain diluar  Kementerian/ Lembaga (K/L)";//daftarAsetTerpilih.UR_STATUS;
                    //        dataPenyama.URAIAN_KEPUTUSAN = teUraianKeputusan.Text;
                    //        dataPenyama.GUNA_WASDAL = "Digunakan pihak Lain";
                    //        _krgNilTetap = _krgNilTetap + daftarAsetTerpilih.NILAI_BUKU;
                    //        _krgKuantitas = _krgKuantitas + daftarAsetTerpilih.KUANTITAS;
                    //        dsGridDaftarAset.Add(dataPenyama);
                    //    }
                    //    teNilaiPenetapan.Text = (Convert.ToDecimal(_krgNilTetap)).ToString("n0");
                    //    teKuantitas.Text = Convert.ToDecimal(_krgKuantitas).ToString("n0");
                    //    break;
                    //case 'D':
                    //    if (daftarTerpilih != null)
                    //    {
                    //        _krgNilTetap = (teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(teNilaiPenetapan.Text));
                    //        _krgKuantitas = (teKuantitas.Text == "" ? 0 : Convert.ToDecimal(teKuantitas.Text));
                    //        for (int i = 0; i < daftarTerpilih.Count; i++)
                    //        {
                    //            SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP daftarAsetTerpilih = (SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP)daftarTerpilih[i];
                    //            _krgNilTetap = _krgNilTetap - daftarAsetTerpilih.NILAI_PERSETUJUAN;
                    //            _krgKuantitas = _krgKuantitas - daftarAsetTerpilih.KUANTITAS;
                    //            dsGridDaftarAset.Remove(daftarAsetTerpilih);
                    //        }
                    //        teNilaiPenetapan.Text = Convert.ToDecimal(_krgNilTetap).ToString("n0");
                    //        teKuantitas.Text = Convert.ToDecimal(_krgKuantitas).ToString("n0");
                    //    }
                    //    break;
                    case 'U':
                        int _indeksData = dsGridDaftarAset.IndexOf(dataAsetTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dataPenyama = null;
                        dataPenyama = new SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP();
                        
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
                        dataPenyama.KD_STATUS = dataAsetTerpilih.KD_STATUS;//"04";
                        dataPenyama.KUANTITAS = dataAsetTerpilih.KUANTITAS ;
                        dataPenyama.KUANTITAS_SK = dataAsetTerpilih.KUANTITAS_SK;
                        dataPenyama.KUANTITAS_SKSpecified = false; //dataAsetTerpilih.KUANTITAS_SKSpecified;
                        dataPenyama.KUANTITASSpecified = false; //dataAsetTerpilih.KUANTITASSpecified;
                        dataPenyama.NILAI_PENETAPAN = dataAsetTerpilih.NILAI_PENETAPAN;
                        dataPenyama.NILAI_PENETAPANSpecified = false; //dataAsetTerpilih.NILAI_PENETAPANSpecified;
                        dataPenyama.NILAI_PERSETUJUAN = dataAsetTerpilih.NILAI_PERSETUJUAN;//Convert.ToDecimal(formPuPenetapan.teNilaiPersetujuan.Text);
                        dataPenyama.NILAI_PERSETUJUANSpecified = false; //dataAsetTerpilih.NILAI_PERSETUJUANSpecified;
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
                        dataPenyama.UR_STATUS = dataAsetTerpilih.UR_STATUS;//"Digunakan oleh satker lain diluar  Kementerian/ Lembaga (K/L)";
                        dataPenyama.URAIAN_KEPUTUSAN = dataAsetTerpilih.URAIAN_KEPUTUSAN;
                        
                        nilaiPenetapanLama = dataAsetTerpilih.NILAI_PERSETUJUAN;
                        jmlKuantitasLama = dataAsetTerpilih.KUANTITAS;
                        //_krgNilTetap = (Convert.ToDecimal(teNilaiPenetapan.Text) - nilaiPenetapanLama + Convert.ToDecimal(formPuPenetapan.teNilaiPersetujuan.Text));
                        //_krgKuantitas = (Convert.ToDecimal(teKuantitas.Text) - jmlKuantitasLama + Convert.ToDecimal(formPuPenetapan.teKuantitas.Text));
                        //teNilaiPenetapan.Text = Convert.ToDecimal(_krgNilTetap).ToString("n0");
                        //teKuantitas.Text = Convert.ToDecimal(_krgKuantitas).ToString("n0");
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
            //SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP daftarAsetTerpilih;
            //if (rowTerpilih != null)
            //{
            //    int posisiRow = gvDaftarAset.GetFocusedDataSourceRowIndex();
            //    if (rowTerpilih.IsLastRow) gvDaftarAset.FocusedRowHandle = posisiRow - 1;
            //    else gvDaftarAset.FocusedRowHandle = posisiRow + 1;

            //    daftarTerpilih = null;
            //    daftarTerpilih = new ArrayList();
            //    string daftarIdAset = "";
            //    for (int i = 0; i < gvDaftarAset.RowCount; i++)
            //    {
            //        string status = Convert.ToString(gvDaftarAset.GetRowCellValue(i, "NUMSpecified"));
            //        if (status == "True")
            //        {
            //            daftarTerpilih.Add(gvDaftarAset.GetRow(i));
            //        }
            //    }
            //    if (daftarTerpilih.Count == 0) MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
            //    else
            //    {
            //        for (int i = 0; i < daftarTerpilih.Count; i++)
            //        {
            //            daftarAsetTerpilih = (SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP)daftarTerpilih[i];
            //            daftarIdAset = String.Format("{0}{1};", daftarIdAset, daftarAsetTerpilih.ID_ASET);
            //        }
            //        daftarIdAset = daftarIdAset.TrimEnd(';');
            //        if (MessageBox.Show("Apakah Anda ingin menghapus Daftar Aset yang terpilih?", konfigApp.judulHapusData,
            //            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            simpanDaftarAset(daftarIdAset, 'D');
            //        }
            //    }
            //}
            //else MessageBox.Show("Silakan pilih Data Aset terlebih dulu", konfigApp.judulKonfirmasi);
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
                dataAsetTerpilih = (SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP)rowTerpilih.GetRow(e.FocusedRowHandle);
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

        #region --++ Edit Tindak Lanjut
        KES1.TL.PU.frmPuPenetapan formPuPenetapan;
        KES1.TL.PSPBMN.frmTL formTL; 

        private void formUbahPenetapan()
        {
            //if (statusForm != "A")
            //{
            //    if (formPuPenetapan == null)
            //    {
            //        formPuPenetapan = new KES1.TL.PU.frmPuPenetapan() { simpanDataPenetapan = new SimpanDaftarAsetTerpilih(simpanDaftarAset) };
            //    }
            //    formPuPenetapan.idAset = dataAsetTerpilih.ID_ASET;
            //    formPuPenetapan.teKodeBarang.Text = dataAsetTerpilih.KD_BRG;
            //    formPuPenetapan.teNamaBarang.Text = dataAsetTerpilih.UR_SSKEL;
            //    formPuPenetapan.teNup.Text = dataAsetTerpilih.NO_ASET.ToString();
            //    formPuPenetapan.teNilaiPersetujuan.Text = (Convert.ToDecimal(dataAsetTerpilih.NILAI_PERSETUJUAN)).ToString("n0");
            //    formPuPenetapan.teKuantitas.Text = (Convert.ToDecimal(dataAsetTerpilih.KUANTITAS)).ToString("n0");
            //    formPuPenetapan.kodeStatus = dataAsetTerpilih.KD_STATUS;
            //    formPuPenetapan.teStatus.Text = dataAsetTerpilih.UR_STATUS;
            //    formPuPenetapan.lciStatusBmn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    formPuPenetapan.tePenggunaanBmn.Text = dataAsetTerpilih.GUNA_WASDAL;
            //    formPuPenetapan.ShowDialog();
            //}
        }
        private void gvDaftarAset_DoubleClick(object sender, EventArgs e)
        {
            
            formTL = new KES1.TL.PSPBMN.frmTL(this, dataAsetTerpilih.ID_ASET.ToString());
            formTL.skKeputusan = dataAsetTerpilih.SK_KEPUTUSAN;
            formTL.kdStatus = dataAsetTerpilih.KD_STATUS;//formTL.leStatus.EditValue.ToString();
            formTL.ShowDialog();
        }
        private void gvDaftarAset_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("key press");
            //if (e.KeyChar == (char)13)
            //{
            //    formUbahPenetapan();
            //}
        }
        #endregion

        private void btnUpTindakLanjut_Click(object sender, EventArgs e)
        {
            //
            SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP daftarAsetTerpilih;
            if (rowTerpilih != null)
            {
                int posisiRow = gvDaftarAset.GetFocusedDataSourceRowIndex();
                if (rowTerpilih.IsLastRow) gvDaftarAset.FocusedRowHandle = posisiRow - 1;
                else gvDaftarAset.FocusedRowHandle = posisiRow + 1;

                daftarTerpilih = null;
                daftarTerpilih = new ArrayList();
                string daftarIdAset = "";
                string kdStatus = "";
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
                        daftarAsetTerpilih = (SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP)daftarTerpilih[i];
                        //kdStatus = formTL.leStatus.EditValue.ToString(); //dataAsetTerpilih.KD_STATUS;
                        daftarIdAset = String.Format("{0}{1};", daftarIdAset, daftarAsetTerpilih.ID_ASET);
                    }
                    daftarIdAset = daftarIdAset.TrimEnd(';');

                        formTL = new KES1.TL.PSPBMN.frmTL(this, daftarIdAset); 
                        formTL.skKeputusan = dataAsetTerpilih.SK_KEPUTUSAN;
                        formTL.ShowDialog();
                        //simpanRkmTindakLanjut(daftarIdAset,kdStatus, 'U');
                    //}
                }
            }
            else MessageBox.Show("Silakan pilih Data Aset terlebih dulu", konfigApp.judulKonfirmasi);
        }

        #endregion
    }
}
