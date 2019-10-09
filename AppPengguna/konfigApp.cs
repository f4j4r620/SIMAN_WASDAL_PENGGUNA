
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DevExpress.XtraBars;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

//using BitMiracle.Docotic.Pdf;

//using Spire.Pdf;
//using Spire.Pdf.Graphics;

namespace AppPengguna
{
    public delegate void TutupProgresBar(Thread str);
    public delegate void ShowProgresBar(BarItemVisibility str);
    public delegate void closeProgresBar(BarItemVisibility str);
    public delegate void showProgresBar();
    public delegate void AktifkanForm(string str);
    public delegate void SetPanel(UserControl uc);
    public delegate void MoreControl(int count);
    public delegate void SimpanDaftarAsetPSPTerpilih(string _daftarIsAset, char _modeCudAset);
    public delegate void SimpanDaftarAsetTerpilih(string _daftarIsAset, char _modeCudAset);
    public delegate void SimpanDaftarAset3Terpilih (string kodebarang, string nup, string uraianbarang);
    public delegate void SimpanDaftarAsetPnbpTerpilih(string _daftarIsAset,decimal? _nilaiTotalAset, char _modeCudAset);
    public delegate void SimpanDaftarAsetPnbpTerpilihManfaat(string _daftarIsAset, decimal? _nilaiTotalAset, char _modeCudAset,decimal? _id_tl_wasdal_manfaat);
    public delegate void SimpanNoSkTerpilih(string _noSk, char _modeCudSk);
    public delegate void PilihSkManfaatPnbp(SvcWasdalManfaatPnbpSelectSk.WASDALSROW_READ_WASDAL_MANFAAT_PNBP _skTerpilih);
    public delegate void PilihSk(SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT _skTerpilih);
    public delegate void PilihSkJualPnbp(SvcWasdalPTJualPnbpSelectSk.WASDALSROW_READ_WASDAL_PT_JUAL_PNBP _skTerpilih);
    public delegate void PilihSkJual(SvcWasdalJualSkSelect.WASDALSROW_READ_WASDAL_PT_JUAL _skTerpilih);
    public delegate void PilihSkBOngkaran(SvcWasdalRSKBongkaranSkSelect.WASDALSROW_SK_BONGKARAN_GRID _skTerpilih);
    public delegate void PilihSkBOngkaranPnbp(SvcTLBongkaranSelect.WASDALSROW_TL_BONGKARAN_GRID _skTerpilih);
    public delegate void DetailDataSanksi(string kode, string label);
    public delegate void DetailDataCatatan(string kode, string label);
    public delegate void backLapMonSatker();

    public delegate void PilihSkTm(SvcWasdalTmSkSelect.WASDALSROW_READ_WASDAL_PT_TUKAR _skTerpilih);
    public delegate void PilihSkTmPnbp(SvcWasdalTukarPnbpSelectSK.WASDALSROW_READ_WASDAL_PT_TUKAR_PNBP _skTerpilih);
    public delegate void PilihSkDpl(SvcWasdalPSPBMNLAINSkSelect.WASDALSROW_READ_WASDAL_PSP_LAIN _skTerpilih);
    public delegate void PilihSkDplPnbp(SvcWasdalLainPnbpSelectSK.WASDALSROW_READ_WASDAL_PSP_LAIN_PNBP _skTerpilih);
    public delegate void PilihSkHibah(SvcWasdalHibahSkSelect.WASDALSROW_READ_WASDAL_PT_HIBAH _skTerpilih);
    public delegate void PilihSkPmp(SvcWasdalPmpSkSelect.WASDALSROW_READ_WASDAL_PT_MODAL _skTerpilih);
    public delegate void PilihSkHapus(SvcWasdalHapusSkTLSelect.WASDALSROW_SK_WASDAL_HAPUS_TL _skTerpilih);  
    public delegate void PilihSkPengguna(SvcWasdalPSPBMNLAINSkSelect.WASDALSROW_READ_WASDAL_PSP_LAIN _skTerpilih);
    public delegate void KelolaAksesMenu(string modeAkses);
    //public delegate void CariDataOnline(string strCariData);
    public delegate void SimpanDataRsk(string _mode);
    public delegate void SimpanDataPenertiban(string _mode);
    public delegate void SimpanDataMonLapWasdal(string _mode);
    public delegate void SimpanDataRtl(string _mode);
    public delegate void SimpanDataRPNBP(string _mode);
    public delegate void CariDataOnline(string strWhere, bool initCari);
    public delegate void ToggleProgressBar(string kondisi);
    public delegate void DetailDataGrid(object sender, DevExpress.XtraBars.ItemClickEventArgs e);
    public delegate void SelectDataUnitKerja(decimal? idUnitKerja, string kodeUnitKerja, string namaUnitKerja, string kodeKl, string namaKl);
    public delegate void SelectSatkerPerbandingan(SvcSatkerSelect.BPSIMANSROW_R_SATKER satker_terpilih);
    public delegate void StartBackgroundWorker(object sender, EventArgs e);
    public delegate void kembali();
    public delegate void detail(string kode);
    public delegate void detail1(string kode, string idsatker);
    public delegate void ValidasiSatkerSpan(SvcGridBandingSpanSiman.WASDALSROW_PNBP_SPAN_SIMAN rowTerpilih, string kd_satker, string ur_satker, string cat);
    public delegate void KewenanganBarang(Boolean IsPengguna);
    //perbandingan pnbp
    public delegate void loadPnbp(string cari);

    public delegate void detailPengelolaan(object sender, DevExpress.XtraBars.ItemClickEventArgs e);

    class konfigApp
    {

        public static string args;
        public static string noTiketKelola;

        #region reference
        public static string initRef = "Y";
        public static System.Data.DataTable t_kondisi = null;
        public static System.Data.DataTable VAR_DS_R_JNS_SERTI = null;
        public static System.Data.DataTable VAR_DS_R_SMILIK = null;
        public static System.Data.DataTable VAR_DS_R_JNS_DOK = null;
        public static System.Data.DataTable VAR_DS_R_JNS_DOK_BDG = null;
        public static System.Data.DataTable VAR_DS_R_LOK_AKSES = null;
        public static System.Data.DataTable VAR_DS_R_LOK_BENTUK = null;
        public static System.Data.DataTable VAR_DS_R_LOK_ELEVASI = null;
        public static System.Data.DataTable VAR_DS_R_LOK_KONTUR = null;
        public static System.Data.DataTable VAR_DS_R_LOK_PERUNTUKAN = null;
        public static System.Data.DataTable VAR_DS_R_PELAYANAN = null;
        public static System.Data.DataTable VAR_DS_R_JNS_PMK = null;
        public static System.Data.DataTable VAR_DS_R_STATUS_HUKUM = null;
        public static System.Data.DataTable VAR_DS_R_SATKER = null;
        #endregion

        #region sum
        public static decimal? TNH_SUM_RPH_ASET = 0;
        public static decimal? TNH_SUM_RPH_MUTASI = 0;
        public static decimal? TNH_SUM_RPH_SUSUT = 0;
        public static decimal? TNH_TOTAL_DATA = 0;
        public static decimal? temp = 0;

        #endregion

        #region max min data
        public static decimal maksReferensi = 50;
        public static double MaksHours = 24;
        public static decimal dataAwal = 1;
        public static decimal dataAkhir = 500;
        public static decimal dataMaks = 500;
        public static decimal currentMaks = 500;
        public static decimal currentMin = 1;
        #endregion

        public static int thnPeriodeWasdal = 2018;

        #region statusbar
        public static string strMenu = "";
        public static string strSubMenu = "";
        
        #endregion

        public static decimal? getGlobalId(string _nmPk)
        {
            decimal? idPk=0;
            SvcGetId.InputParameters input = new SvcGetId.InputParameters();
            input.P_NM_PK = _nmPk;
            input.P_SELECT = "C";
            SvcGetId.execute_pttClient svcGetId = new SvcGetId.execute_pttClient();
            SvcGetId.OutputParameters output = svcGetId.execute(input);
            if (output.PO_RESULT == "Y")
            {
                idPk = output.PO_ID_PK;
            }
            else {
                idPk = 0;
            }
           
            return idPk;
        }

        #region Konfigurasi label Tombol
        public static string labelMore = "Lebih Banyak";
        public static string labelRefresh = "Refresh";
        public static string labelTambah = "Tambah Data";
        public static string labelUbah = "Ubah Data";
        public static string labelHapus = "Hapus Data";
        public static string labelSimpan = "Simpan Data";
        public static string labelAmbil = "Ambil Data";
        public static string labelPilih = "Pilih Data";
        public static string labelPrev = "Sebelumnya";
        public static string labelNext = "Berikutnya";
        public static string labelBatal = "Batal";
        public static string labelDetail = "Detail";
        public static string labelSubmit = "Ajukan RKBMN KPB";
        public static string labelTutup = "Tutup";
        public static string labelKeDashboard = "ke Dashboard";
        public static string labelKembali = "Kembali";
        public static string labelUpdate = "Update";
        public static string levelSatker = "SATKER";
        public static string levelKorwil = "KORWIL";
        public static string levelEselon1 = "ESELON I";
        public static string levelKl = "KL";
        public static string levelKpknl = "KPKNL";
        public static string levelKanwil = "KANWIL";
        public static string levelKpdjkn = "KPDJKN";

		#region khusus perencanaan
        public static string labelAdd = "Tambah";
        public static string labelEdit = "Ubah";
        public static string labelDelete = "Hapus";
        public static string labelSave = "Simpan";
        public static string labelGet = "Ambil";
        public static string labelSelect = "Pilih";
        public static string labelPrint = "Cetak";
        #endregion

        public static string labelCariData = "Cari Data";
        public static string labelPilihProgram = "Pilih Program";
        public static string labelPilihKegiatan = "Pilih Kegiatan";
        public static string labelPilihOutput = "Pilih Output";
        public static string labelPilihBarang = "Pilih Barang";
        public static string labelPilihAset = "Pilih Aset";
        public static string labelPilihBas = "Pilih BAS";
        public static string labelAjukanPbKeDjkn = "Ajukan RPBMN PB";

        public static string labelAjukanManual = "Ajukan Manual";
        public static string labelCetakKpb = "Cetak Form 1A";
        public static string labelDisposisi = "Disposisi";
        public static string labelTerimaRkbmnPb = "Terima RKBMN PB";
        public static string labelTerimaTelaahAnalis = "Terima Telaah Analis";
        public static string labelKembalikankeKl = "Kembalikan ke K/L";
        public static string labelKembalikankeKd = "Kembalikan ke KP";
        public static string labelTerimaDisposisi = "Terima Disposisi";
        public static string labelCetakRkbmnPb = "Cetak RKBMN PB";
        public static string labelTerimaHasilTelaah = "Terima Hasil Telaah";

        public static string labelTelusuriAset = "Telusuri Aset";
        public static string labelOptionTelusur = "Filter Telusur";
        #endregion

        #region konfigurasi Kotak Dialog
        public static string teksDialog = null;
        public static string teksIsianKosong = "Ada kotak isian yang masih Kosong. Silakan dilengkapi dulu";
        public static string judulIsianKosong = "Perhatian....";
        public static string teksGagalSimpan = "Penyimpanan Data gagal dilakukan. Silakan coba lagi";
        public static string teksGagalSimpanFileDok = "Penyimpanan file dokumen gagal dilakukan. Silakan coba lagi";
        public static string judulGagalSimpan = "Perhatian....";
        public static string teksHapusData = "Apakah Anda ingin menghapus data ";
        public static string judulHapusData = "Konfirmasi Penghapusan";
        public static string teksGagalHapus = "Penghapusan data gagal dilakukan. Silakan coba lagi";
        public static string judulGagalHapus = "Perhatian";
        public static string teksGagalAmbil = "Pengambilan Data gagal dilakukan. Silakan coba lagi";
        public static string judulGagalAmbil = "Perhatian";
        public static string teksGagalLain = "Operasi yang diminta gagal dilakukan. Silakan coba lagi";
        public static string judulGagalLain = "Perhatian";
        public static string teksGagalCancel = "Operasi pembatalan tiket gagal dilakukan. Silahkan coba lagi";
        public static string teksKosong = "Nama dan Password tidak boleh kosong";
        public static string teksGagalTerima = "Receive Tiket Pengajuan gagal";
        public static string teksGagalKirim = "Submit Tiket Pengajuan gagal dilakukan. Silakan coba lagi";
        public static string teksPilihanKosong = "Belum ada data yang dipilih";
        public static string teksPilihanLebih = "Data Hanya Boleh Satu";
        public static string teksGagalDisposisi = "Mengirim RKBMN PB (Disposisi) ke Analis gagal dilakukan. Silakan coba lagi";
        public static string teksFotoKosong = "Aset yang dipilih belum memiliki foto";
        public static string teksDokumenKosong = "Dokumen tidak bisa ditampilkan. Silakan coba lagi";
        public static string teksDokumenUploadKosong = "Dokumen yang akan diupload belum dipilih";
        public static string teksDokumenDownloadKosong = "Dokumen yang akan dilihat tidak tersedia";
		public static string teksAjuanTakLengkap = "Perencanaan yang diajukan belum Lengkap datanya";
		public static string teksDataCetakKosong = "Daya yang maun Dicetak tidak tersedia";
		public static string teksPegawaiKosong = "Skema Pegawai yang dipilih belum dibuat. Silakan buat dulu skemanya di Tab Komposisi Pegawai atau memilih skema yang lain";
		public static string teksGagalBuatTiket = "Pembuatan Tiket gagal dilakukan. Silakan coba lagi";
        public static string teksKonfHapusSk = "Apakah Anda ingin Menghapus Surat Keputusan: {0} ?";
        public static string teksSimpanData = "Penyimpanan data Berhasil.";
        public static string teksUbahData = "Perubahan data Berhasil.";
        //public static string teksKonfHapusPenertiban = "Apakah Anda ingin Menghapus Penertiban: {0} ?";
        public static string teksKonfHapusPenertiban = "Menghapus Penertiban: no.{0} ?";
        public static string teksKonfHapusMonLapWasdal = "Menghapus Laporan Wasdal: no.{0} ?";
        public static string teksNilaiMinus = "Nilai Tidak Boleh dalam Keadaan Minus";
		#endregion
		
        #region -- Konfigurasi Kotak DIalog Konfirmasi Pengajuan
        public static string judulKonfirmasi = "Konfirmasi";
        public static string teksBuatTiket = "Apakah Anda ingin Membuat Tiket Pengajuan ";
        public static string teksSubmitTiket = "Apakah Anda ingin Mengirimkan Pengajuan ";
        public static string teksTerimaTiket = "Apakah Anda ingin Menerima Tiket Pengajuan ";
        public static string teksKembalikanTiket = "Apakah Anda ingin Kembalikan Tiket Pengajuan ";
        public static string teksKompilasiTiket = "Apakah Anda ingin Mengkompilasi Tiket Pengajuan ";
        public static string teksTerimaTiketTelaah = "Apakah Anda ingin Menerima RKBMN PB Telaah ";
        public static string teksBuatRevisi = "Apakah Anda ingin Membuat Revisi RKBMN KPB";
        public static string teksHapusPengadaan = "Apakah Anda ingin menghapus Pengadaan ";
        public static string teksHapusPemeliharaan = "Apakah Anda ingin menghapus Pemeliharaan ";
        public static string teksKembalikanTelaahKoord = "Apakah Anda ingin Mengirimkan RKBMN PB hasil telaah ke K/L ";
        public static string teksKirimDisposisi = "Apakah Anda ingin mengirim RKBMN PB (Disposisi) ke Analis ";
        public static string teksKirimHasilTelaah = "Apakah Anda ingin mengirimkan RKBMN PB Hasil Telaahan ke KP ";
        public static string kodebarang = "";
        public static string nup = "";
        public static string uraianbarang = "";
        #endregion

        //public static 
        #region
        //==============  30-10-2013 ========================
        public static string teksBerhasilAmbil = "Pengambilan data berhasil dilakukan";
        public static string judulBerhasilAmbil = "Sukses";
        public static string teksBerhasilSimpan = "Data berhasil disimpan.";
        public static string teksBerhasilHapus = "Data berhasil dihapus.";
        public static string judulsukses = "Sukses";
        public static string judulGagal = "Gagal";
        public static string teksTidakKetemu = "Data tidak ketemu.";
        public static string judulTidakKetemu = "Tidak Ketemu";
        public static string warning_isi_gol = "Golongan Barang Belum diisi";
        public static string warning_isi_bidang = "Bidang Barang Belum diisi";
        public static string warning_isi_kelBrg = "Kelompok Barang Belum diisi";
        public static string warning_isi_parameter = "Parameter Pencarian Belum diisi";
        public static string warning = "Perhatian";

        public static string levelUser = null;
        public static decimal? idUser = null;
        public static decimal? idGroup = null;
        public static decimal? idSatker = null;
        public static decimal? idKorwil = null;
        public static decimal? idEselon1 = null;
        public static decimal? idKl = null;
        public static decimal? idKpknl = null;
        public static decimal? idKanwil = null;
        public static string kodeKanwil = null;
        public static string namaKanwil = null;
        public static string kodeDitPknsi = null;
        public static string namaDitPknsi = null;
        public static string kodeKpDjkn = null;
        public static string namaKpDjkn = null;
        public static string kodeKpknl = null;
        public static string namaKpknl = null;
        public static string kodeKorwil = null;
        public static string namaKorwil = null;
        public static string namaUser = null;
        public static string namaGroup = null;
        public static string kodePengguna = null;
        public static string namaPengguna = null;
        public static string nipPemohon = null;
        public static string emailPemohon = null;
        public static string noTelpPemohon = null;
        public static string namaOrganisasi = null;
        public static string kodeSatker = null;
        public static string namaSatker = null;
        public static string kodeEselon1 = null;
        public static string namaEselon1 = null;
        public static string kodeKl = null;
        public static string namaKl = null;
        public static string idMenu = "003";
        public static decimal? tahunAnggaran = DateTime.Now.Year;
        public static string namaPassword = null;
        public static string kdPelayanan = "";
        public static string kdMenu = "";
        public static string alamatsatker = "";
        public static string alamatkorwil = "";
        public static string alamatkpknl = "";
        public static string alamatkl = "";
        public static string alamateselon = "";
        public static string alamatkanwil = "";
        public static string kotasatker = "";
        public static string kotakorwil = "";
        //FrmMainAdmin frmAdmin = new FrmMainAdmin();

        public static string kodeInstansi = "";
        public static string tipePengelola = null;
        public static string namaPelayanan = null;
        public static string namaTb = "Tanah dan Bangunan";
        public static string namaNonTb = "Non-Tanah dan Bangunan";
        public static string namaLayanan02 = "Penetapan Status Penggunaan BMN";
        public static string namaLayanan03 = "Dioperasikan Pihak Lain";
        public static string namaLayanan04 = "Alih Status Penggunaan BMN";
        public static string namaLayanan05 = "Penggunaan BMN Sementara";
        public static string namaLayanan06 = "Sewa BMN";
        public static string namaLayanan07 = "Pinjam Pakai BMN";
        public static string namaLayanan08 = "Kerja Sama Pemanfaatan";
        public static string namaLayanan09 = "KSPI";
        public static string namaLayanan10 = "BGS/BSG";
        public static string namaLayanan11 = "Penjualan";
        public static string namaLayanan12 = "Tukar-Menukar";
        public static string namaLayanan13 = "Hibah";
        public static string namaLayanan14 = "Penyertaan Modal Pemerintah";
        public static string namaLayanan15 = "Pemusnahan BMN";
        public static string namaLayanan16 = "Penghapusan BMN karena Putusan Pengadilan";
        public static string namaLayanan17 = "Penghapusan BMN karena Sebab-sebab Lain";
        public static string namaLayanan18 = "Penertiban BMN";
        public static string namaLayanan22 = "Bongkaran";

        //-------------------------------------------------------
        #endregion

        #region Pengelolaan Hak Akses Data Pengajuan
        public static bool hak00 = false;
        public static bool hak0a = false;
        public static bool hak0b = false;
        public static bool hak0c = false;
        public static bool hak0d = false;
        public static bool hak0e = false;
        public static bool hak0f = false;
        public static bool hak0g = false;
        public static bool hak0h = false;
        public static bool hak0i = false;
        public static bool hak0j = false;
        public static bool hak0k = false;
        public static bool hak01 = false;
        public static bool hak02 = false;
        public static bool hak2a = false;
        public static bool hak2b = false;
        public static bool hak2c = false;
        public static bool hak03 = false;
        public static bool hak04 = false;
        public static bool hak05 = false;
        public static bool hak5a = false;
        public static bool hak5b = false;
        public static bool hak06 = false;
        public static bool hak07 = false;
        public static bool hak08 = false;
        public static bool hak09 = false;
        public static bool hak10 = false;
        public static bool hak11 = false;
        public static bool hak12 = false;
        public static bool hak13 = false;
        public static bool hak14 = false;
        public static bool hak15 = false;
        public static bool hak16 = false;
        public static bool hak17 = false;
        public static bool hak18 = false;
        public static bool hak19 = false;
        public static bool hak20 = false;
        public static bool hak21 = false;
        public static void setHakAkses(string statusMode)
        {
            hak00 = false;
            hak0a = false;
            hak0b = false;
            hak0c = false;
            hak0d = false;
            hak0e = false;
            hak0f = false;
            hak0g = false;
            hak0h = false;
            hak0i = false;
            hak0j = false;
            hak0k = false;
            hak01 = false;
            hak02 = false;
            hak2a = false;
            hak2b = false;
            hak2c = false;
            hak03 = false;
            hak04 = false;
            hak05 = false;
            hak5a = false;
            hak5b = false;
            hak06 = false;
            hak07 = false;
            hak08 = false;
            hak09 = false;
            hak10 = false;
            hak11 = false;
            hak12 = false;
            hak13 = false;
            hak14 = false;
            hak15 = false;
            hak16 = false;
            hak17 = false;
            hak18 = false;
            hak19 = false;
            hak20 = false;
            hak21 = false;
            string[] modeAkses = statusMode.Split('.');
            foreach (string akses in modeAkses)
            {
                switch (akses)
                {
                    case "0":
                        hak00 = true;
                        break;
                    case "0a":
                        hak0a = true;
                        break;
                    case "0b":
                        hak0b = true;
                        break;
                    case "0c":
                        hak0c = true;
                        break;
                    case "0d":
                        hak0d = true;
                        break;
                    case "0e":
                        hak0e = true;
                        break;
                    case "0f":
                        hak0f = true;
                        break;
                    case "0g":
                        hak0g = true;
                        break;
                    case "0h":
                        hak0h = true;
                        break;
                    case "0i":
                        hak0i = true;
                        break;
                    case "0j":
                        hak0j = true;
                        break;
                    case "0k":
                        hak0k = true;
                        break;
                    case "1":
                        hak01 = true;
                        break;
                    case "2":
                        hak02 = true;
                        break;
                    case "2a":
                        hak2a = true;
                        break;
                    case "2b":
                        hak2b = true;
                        break;
                    case "2c":
                        hak2c = true;
                        break;
                    case "3":
                        hak03 = true;
                        break;
                    case "4":
                        hak04 = true;
                        break;
                    case "5":
                        hak05 = true;
                        break;
                    case "5a":
                        hak5a = true;
                        break;
                    case "5b":
                        hak5b = true;
                        break;
                    case "6":
                        hak06 = true;
                        break;
                    case "7":
                        hak07 = true;
                        break;
                    case "8":
                        hak08 = true;
                        break;
                    case "9":
                        hak09 = true;
                        break;
                    case "10":
                        hak10 = true;
                        break;
                    case "11":
                        hak11 = true;
                        break;
                    case "12":
                        hak12 = true;
                        break;
                    case "13":
                        hak13 = true;
                        break;
                    case "14":
                        hak14 = true;
                        break;
                    case "15":
                        hak15 = true;
                        break;
                    case "16":
                        hak16 = true;
                        break;
                    case "17":
                        hak17 = true;
                        break;
                    case "18":
                        hak18 = true;
                        break;
                    case "19":
                        hak19 = true;
                        break;
                    case "20":
                        hak20 = true;
                        break;
                    case "21":
                        hak21 = true;
                        break;
                }
            }
        }
        #endregion
        

        #region Konfigurasi Navigasi Menu
        public static string rcMenu = "Perencanaan";
        public static string rcAjuanKpbSatker = "Pengajuan RKBMN KPB Aktif";
        public static string rcAjuanKpbSelesaiSatker = "Pengajuan RKBMN KPB Selesai";
        public static string rcAjuanKpbKl = "Pengajuan RKBMN KPB Aktif";
        public static string rcAjuanKpbSelesaiKl = "Pengajuan RKBMN KPB Selesai";
        public static string rcAjuanPbKl = "Pengajuan RKBMN PB Aktif";
        public static string rcAjuanPbSelesaiKl = "Pengajuan RKBMN PB Selesai";
        public static string rcAjuanPbDjkn = "Pengajuan RKBMN PB Aktif";
        public static string rcAjuanSelesaiPbDjkn = "Pengajuan RKBMN PB Selesai";
        public static string rcAjuanPbAnalis = "Pengajuan RKBMN PB Aktif";
        public static string rcAjuanPbSelesaiAnalis = "Pengajuan RKBMN PB Aktif";
        #endregion konf navigasi menu


        #region config IP service references

        //public static string IP = "http://139.192.151.25:8001/soa-infra/services/";
        //public static string IP = "http://10.10.1.65:8001/soa-infra/services/";
        public static string IP = "http://api.siman.djkn.depkeu.go.id:8001/soa-infra/services/";
        //public static string IP = "http://10.11.1.111:8001/soa-infra/services/";
        
        //public static string SimanAPI = "siman_api";
        public static string SimanAPI = "siman_api_dev";

        #region SvcLogin
        public static string loginConfigName = "call_pt67";
        public static string loginAddress = IP + SimanAPI + "/tsUserManagement/doLogin";
        #endregion
        #region SvcGroup
        public static string groupSelectEpName = "call_pt86";
        public static string groupSelectAddress = IP + SimanAPI + "/dsGgroup/dsGroupSelect";
        public static string groupCrudEpName = "call_pt87";
        public static string groupCrudAddress = IP + SimanAPI + "/dsGgroup/dsGroupCrud";
        #endregion

        #region SvcUser
        public static string userSelectEpName = "call_pt88";
        public static string userSelectAddress = IP + SimanAPI + "/dsUser/dsUserSelect";
        public static string userCrudEpName = "call_pt89";
        public static string userCrudAddress = IP + SimanAPI + "/dsUser/dsUserCrud";
        #endregion

        #region SvcSertifikat
        public static string sertifikatSelectEpName = "dsSertifikatSelect_pt";
        public static string sertifikatSelectAddress = IP + SimanAPI + "/dsSertifikat/dsSertifikatSelect";
        public static string sertifikatCrudEpName = "dsSertifikatCrud_pt";
        public static string sertifikatCrudAddress = IP + SimanAPI + "/dsSertifikat/dsSertifikatCrud";
        public static string sertifikatUEpName = "dsSertifikatU_pt";
        public static string sertifikatUAddress = IP + SimanAPI + "/dsSertifikat/dsSertifikatU";
        #endregion

        #region SvcKl
        public static string klSelectEpName = "call_pt20";
        public static string klSelectAddress = IP + SimanAPI + "/dsRKl/dsRKlSelect";
        public static string klCrudEpName = "call_pt21";
        public static string klCrudAddress = IP + SimanAPI + "/dsRKl/dsRKlCrud";
        #endregion

        #region SvcEselon1
        public static string eselon1SelectEpName = "call_pt22";
        public static string eselon1SelectAddress = IP + SimanAPI + "/dsREselon1/dsREselon1Select";
        public static string eselon1CrudEpName = "call_pt23";
        public static string eselon1CrudAddress = IP + SimanAPI + "/dsREselon1/dsREselon1Crud";
        #endregion

        #region SvcKorwil
        public static string korwilSelectEpName = "call_pt24";
        public static string korwilSelectAddress = IP + SimanAPI + "/dsRKorwil/dsRKorwilSelect";
        public static string korwilCrudEpName = "call_pt25";
        public static string korwilCrudAddress = IP + SimanAPI + "/dsRKorwil/dsRKorwilCrod";
        #endregion

        #region SvcSatker
        public static string satkerSelectEpName = "call_pt27";
        public static string satkerSelectAddress = IP + SimanAPI + "/dsRSatker/dsRSatkerSelect";
        public static string satkerCrudEpName = "call_pt26";
        public static string satkerCrudAddress = IP + SimanAPI + "/dsRSatker/dsRSatkerCrud";
        #endregion

        #region SvcKppnwil
        public static string kppnwilSelectEpName = "call_pt30";
        public static string kppnwilSelectAddress = IP + SimanAPI + "/dsRKppnwil/dsRKppnwilSelect";
        public static string kppnwilCrudEpName = "call_pt31";
        public static string kppnwilCrudAddress = IP + SimanAPI + "/dsRKppnwil/dsRKppnwilCrud";
        #endregion

        #region SvcKppn
        public static string kppnSelectEpName = "call_pt32";
        public static string kppnSelectAddress = IP + SimanAPI + "/dsRKppn/dsRKppnSelect";
        public static string kppnCrudEpName = "call_pt33";
        public static string kppnCrudAddress = IP + SimanAPI + "/dsRKppn/dsRKppnCrud";
        #endregion

        #region SvcKknl
        public static string kpknlSelectEpName = "call_pt34";
        public static string kpknlSelectAddress = IP + SimanAPI + "/dsRKpknl/dsRKpknlSelect";
        public static string kpknlCrudEpName = "call_pt35";
        public static string kpknlCrudAddress = IP + SimanAPI + "/dsRKpknl/dsRKpknlCrud";
        #endregion

        #region SvcKanwil
        public static string kanwilSelectEpName = "call_pt64";
        public static string kanwilSelectAddress = IP + SimanAPI + "/dsRKanwil/doSelect";
        public static string kanwilCrudEpName = "call_pt65";
        public static string kanwilCrudAddress = IP + SimanAPI + "/dsRKanwil/doCrud";
        #endregion

        #region SvcJnsBmn
        public static string jnsBmnSelectEpName = "call_pt36";
        public static string jnsBmnSelectAddress = IP + SimanAPI + "/dsRJnsBmn/svcDsRJnsBmnSelect";
        public static string jnsBmnCrudEpName = "call_pt37";
        public static string jnsBmnCrudAddress = IP + SimanAPI + "/dsRJnsBmn/svcDsRJnsBmnCrud";
        #endregion

        #region SvcGolBrg
        public static string golBrgSelectEpName = "call_pt38";
        public static string golBrgSelectAddress = IP + SimanAPI + "/dsRGol/doSelect";
        public static string golBrgCrudEpName = "call_pt39";
        public static string golBrgCrudAddress = IP + SimanAPI + "/dsRGol/doCrud";
        #endregion

        #region SvcBidBrg
        public static string bidBrgSelectEpName = "call_pt40";
        public static string bidBrgSelectAddress = IP + SimanAPI + "/dsRBid/svcDsRBidSelect";
        public static string bidBrgCrudEpName = "call_pt41";
        public static string bidBrgCrudAddress = IP + SimanAPI + "/dsRBid/svcDsRBidCrud";
        #endregion

        #region SvcKelBrg
        public static string kelBrgSelectEpName = "call_pt42";
        public static string kelBrgSelectAddress = IP + SimanAPI + "/dsRKel/svcDsRKelSelect";
        public static string kelBrgCrudEpName = "call_pt43";
        public static string kelBrgCrudAddress = IP + SimanAPI + "/dsRKel/svcDsRKelCrud";
        #endregion
        //SimanAPI
        #region SvcSkelBrg
        public static string skelBrgSelectEpName = "call_pt44";
        public static string skelBrgSelectAddress = IP + SimanAPI + "/dsRSkel/svcDsRSkelSelect";
        public static string skelBrgCrudEpName = "call_pt45";
        public static string skelBrgCrudAddress = IP + SimanAPI + "/dsRSkel/svcDsRSkelCrud";
        #endregion

        #region SvcSskelBrg
        public static string sskelBrgSelectEpName = "call_pt46";
        public static string sskelBrgSelectAddress = IP + SimanAPI + "/dsRSskel/doSelect";
        public static string sskelBrgCrudEpName = "call_pt47";
        public static string sskelBrgCrudAddress = IP + SimanAPI + "/dsRSskel/doCrud";
        #endregion

        #region SvcMapBmn
        public static string mapBmnSelectEpName = "executecall_pt";
        public static string mapBmnSelectAddress = IP + SimanAPI + "/dsRMapbmn12/svcDsRMapbmn12Select";
        public static string mapBmnCrudEpName = "call_pt48";
        public static string mapBmnCrudAddress = IP + SimanAPI + "/dsRMapbmn12/svcDsRMapbmn12Crud";
        #endregion

        #region SvcAkun
        public static string akunSelectEpName = "call_pt49";
        public static string akunSelectAddress = IP + SimanAPI + "/dsRAkun/svcDsRAkunSelect";
        public static string akunCrudEpName = "call_pt50";
        public static string akunCrudAddress = IP + SimanAPI + "/dsRAkun/svcDsRAkunCrud";
        #endregion

        #region SvcPostAs
        public static string postAsSelectEpName = "call_pt51";
        public static string postAstSelectAddress = IP + SimanAPI + "/dsRPostas/svcDsRPostasSelect";
        public static string postAsCrudEpName = "call_pt52";
        public static string postAsCrudAddress = IP + SimanAPI + "/dsRPostas/svcDsRPostasSelectCrud";
        #endregion

        #region SvcJnsTransaksi
        public static string jnsTransaksiSelectEpName = "call_pt53";
        public static string jnsTransaksiSelectAddress = IP + SimanAPI + "/dsRJnstransaksi/svcDsRJnstransaksiSelect";
        public static string jnsTransaksiCrudEpName = "call_pt54";
        public static string jnsTransaksiCrudAddress = IP + SimanAPI + "/dsRJnstransaksi/svcDsRJnstransaksiCrud";
        #endregion

        #region SvcPelayanan
        public static string pelayananSelectEpName = "call_pt55";
        public static string pelayananSelectAddress = IP + SimanAPI + "/dsRPelayanan/dsRPelayananSelect";
        public static string pelayananCrudEpName = "call_pt56";
        public static string pelayananCrudAddress = IP + SimanAPI + "/dsRPelayanan/dsRPelayananCrud";
        #endregion

        #region SvcProses
        public static string prosesSelectEpName = "call_pt57";
        public static string prosesSelectAddress = IP + SimanAPI + "/dsRProses/dsRProsesSelect";
        public static string prosesCrudEpName = "call_pt58";
        public static string prosesCrudAddress = IP + SimanAPI + "/dsRProses/dsRProsesCrud";
        #endregion

        #region SvcJnsDok
        public static string jnsDokSelectEpName = "call_pt59";
        public static string jnsDokSelectAddress = IP + SimanAPI + "/dsRJnsDok/dsRJnsDokSelect";
        public static string jnsDokCrudEpName = "call_pt60";
        public static string jnsDokCrudAddress = IP + SimanAPI + "/dsRJnsDok/dsRJnsDokCrud";
        #endregion

        #region SvcSyaratDok
        public static string syaratDokSelectEpName = "call_pt61";
        public static string syaratDokSelectAddress = IP + SimanAPI + "/dsRSyaratDok/dsRSyaratDokSelect";
        public static string syaratDokCrudEpName = "call_pt62";
        public static string syaratDokCrudAddress = IP + SimanAPI + "/dsRSyaratDok/dsRSyaratDokCrud";
        #endregion

        #region SvcRulePelayanan
        public static string rulePelayananSelectEpName = "call_pt55";
        public static string rulePelayananSelectAddress = IP + SimanAPI + "/dsRPelayanan/dsRPelayananSelect";
        public static string rulePelayananCrudEpName = "call_pt56";
        public static string rulePelayananCrudAddress = IP + SimanAPI + "/dsRPelayanan/dsRPelayananCrud";
        #endregion

        #region SvcSusut
        public static string susutSelectEpName = "call_pt2";
        public static string susutSelectAddress = IP + SimanAPI + "/dsRSusut/svcDsRSusutSelect";
        public static string susutCrudEpName = "call_pt19";
        public static string susutCrudAddress = IP + SimanAPI + "/dsRSusut/svcDsRSusutCrud";
        #endregion

        #region SvcData
        public static string dataSelectEpName = "call_pt3";
        public static string dataSelectAddress = IP + SimanAPI + "/dsRData/svcDsRDataSelect";
        public static string dataCrudEpName = "call_pt18";
        public static string dataCrudAddress = IP + SimanAPI + "/dsRData/svcDsRDataCrud";
        #endregion

        #region SvcJnsKirim
        public static string jnsKirimSelectEpName = "call_pt4";
        public static string jnsKirimSelectAddress = IP + SimanAPI + "/dsRJnsKirim/svcDsRJnsKirimSelect";
        public static string jnsKirimCrudEpName = "call_pt17";
        public static string jnsKirimCrudAddress = IP + SimanAPI + "/dsRJnsKirim/svcDsRJnsKirimCrud";
        #endregion

        #region SvcPemilik
        public static string pemilikSelectEpName = "call_pt5";
        public static string pemilikSelectAddress = IP + SimanAPI + "/dsRPemilik/svcDsRPemilikSelect";
        public static string pemilikCrudEpName = "call_pt16";
        public static string pemilikCrudAddress = IP + SimanAPI + "/dsRPemilik/svcDsRPemilikCrud";
        #endregion

        #region SvcSmilik
        public static string smilikSelectEpName = "call_pt6";
        public static string smilikSelectAddress = IP + SimanAPI + "/dsRSmilik/svcDsRSmilikSelect";
        public static string smilikCrudEpName = "call_pt15";
        public static string smilikCrudAddress = IP + SimanAPI + "/dsRSmilik/svcDsRSmilikCrud";
        #endregion

        #region SvcJnsSertifikatSelect
        public static string SvcJnsSertifikatSelect_name = "call_pt279";
        public static string SvcJnsSertifikatSelect_address = IP + SimanAPI + "/referensi/jnsSertiSelect";
        #endregion

        #region SvcSmilikSelect
        public static string SvcSmilikSelect_name = "call_pt6";
        public static string SvcSmilikSelect_address = IP + SimanAPI + "/dsRSmilik/svcDsRSmilikSelect";
        public static string SvcSmilikCrud_name = "call_pt15";
        public static string SvcSmilikCrud_address = IP + SimanAPI + "/dsRSmilik/svcDsRSmilikCrud";
        #endregion

        #region SvcWilayah
        public static string wilayahSelectEpName = "call_pt7";
        public static string wilayahSelectAddress = IP + SimanAPI + "/dsRWilayah/svcDsRWilayahSelect";
        public static string wilayahCrudEpName = "call_pt14";
        public static string wilayahCrudAddress = IP + SimanAPI + "/dsRWilayah/svcDsRWilayahCrud";
        #endregion

        #region SvcStatus
        public static string statusSelectEpName = "call_pt8";
        public static string statusSelectAddress = IP + SimanAPI + "/dsRStatus/svcDsRStatusSelect";
        public static string statusCrudEpName = "call_pt13";
        public static string statusCrudAddress = IP + SimanAPI + "/dsRStatus/svcDsRStatusCrud";
        #endregion

        #region SvcJnsRuangan
        public static string jnsRuanganSelectEpName = "call_pt9";
        public static string jnsRuanganSelectAddress = IP + SimanAPI + "/dsRRuang/svcDsRRuangSelect";
        public static string jnsRuanganCrudEpName = "call_pt12";
        public static string jnsRuanganCrudAddress = IP + SimanAPI + "/dsRRuang/svcDsRRuangCrud";
        #endregion

        #region SvcDasarHarga
        public static string dasarHargaSelectEpName = "call_pt10";
        public static string dasarHarganSelectAddress = IP + SimanAPI + "/dsRDsrHarga/svcDsRDsrHargaSelect";
        public static string dasarHargaCrudEpName = "call_pt11";
        public static string dasarHargaCrudAddress = IP + SimanAPI + "/dsRDsrHarga/svcDsRDsrHargaCrud";
        #endregion

        #region SvcKondisi
        public static string kondisiSelectEpName = "call_pt";
        public static string kondisiSelectAddress = IP + SimanAPI + "/dsRKondisi/svcDsRKondisiSelect";
        public static string kondisiCrudEpName = "call_pt1";
        public static string kondisiCrudAddress = IP + SimanAPI + "/dsRKondisi/svcDsRKondisiCrud";
        #endregion

        #region SvcJenisDokumenBangunanSelect
        public static string SvcJenisDokumenBangunanSelect_name = "call_pt230";
        public static string SvcJenisDokumenBangunanSelect_address = IP + SimanAPI + "/referensi/jnsDokBdgSelect";
        #endregion

        #region SvcStatusHukumSelect
        public static string SvcStatusHukumSelect_name = "call_pt231";
        public static string SvcStatusHukumSelect_address = IP + SimanAPI + "/referensi/statusHukumSelect";
        #endregion

        #region SvcTipeBangunanSelect
        public static string SvcTipeBangunanSelect_name = "call_pt163";
        public static string SvcTipeBangunanSelect_address = IP + SimanAPI + "/referensi/asetTnhBangunan/select";
        #endregion

        #region SvcTipeRuanganSelect
        public static string tipeRuanganSelectEpName = "call_pt232";
        public static string tipeRuanganSelectAddres = IP + SimanAPI + "/referensi/tipeRuangSelect";
        #endregion

        #region MASTER ASET
        #region Aset Rumah


        #region SvcHuniRmhNgrSelect

        public static string SvcHuniRmhNgrSelect_name = "call_pt229"; //fix
        public static string SvcHuniRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/rwytPenghuniSelect"; 
        #endregion

        #region SvcDetRmhNgrCrud

        public static string SvcDetRmhNgrCrud_name = "call_pt109"; //fix
        public static string SvcDetRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/detailCud"; 
        #endregion

        #region SvcDetRmhNgrSelect
        public static string SvcDetRmhNgrSelect_name = "call_pt106";
        public static string SvcDetRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/detailSelect"; //fix
        #endregion

        #region SvcDokRmhNgrCrud

        public static string SvcDokRmhNgrCrud_name = "call_pt110";
        public static string SvcDokRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/dokCud"; //fix
        #endregion

        #region SvcDokRmhNgrSelect
        public static string SvcDokRmhNgrSelect_name = "call_pt107";
        public static string SvcDokRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/dokSelect"; //fix
        #endregion

        #region SvcFasRmhNgrCrud

        public static string SvcFasRmhNgrCrud_name = "call_pt111";
        public static string SvcFasRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/fasPenunjangCud"; //fix
        #endregion

        #region SvcFasRmhNgrSelect
        public static string SvcFasRmhNgrSelect_name = "call_pt108";
        public static string SvcFasRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/fasPenunjangSelect"; //fix
        #endregion

        #region SvcGPSRmhNgrCrud

        public static string SvcGPSRmhNgrCrud_name = "call_pt112";
        public static string SvcGPSRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/gpsCud"; //fix
        #endregion

        #region SvcGPSRmhNgrSelect
        public static string SvcGPSRmhNgrSelect_name = "call_pt113";
        public static string SvcGPSRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/gpsSelect"; //fix
        #endregion

        #region SvcHukRmhNgrCrud

        public static string SvcHukRmhNgrCrud_name = "call_pt114";
        public static string SvcHukRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/hukumCud"; //fix
        #endregion

        #region SvcHukRmhNgrSelect
        public static string SvcMslHukRmhNgrSelect_name = "call_pt115";
        public static string SvcMslHukRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/hukumSelect"; //fix
        #endregion

        #region SvcKonsRmhNgrCrud

        public static string SvcKonsRmhNgrCrud_name = "call_pt116";
        public static string SvcKonsRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/konsBdgCud"; //fix
        #endregion

        #region SvcKonsRmhNgrSelect
        public static string SvcKonsRmhNgrSelect_name = "call_pt117";
        public static string SvcKonsRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/konsBdgSelect"; //fix
        #endregion

        #region SvcLokRmhNgrCrud

        public static string SvcLokRmhNgrCrud_name = "call_pt118";
        public static string SvcLokRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/lokasiCud"; //fix
        #endregion

        #region SvcLokRmhNgrSelect
        public static string SvcLokRmhNgrSelect_name = "call_pt119";
        public static string SvcLokRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/lokasiSelect"; //fix
        #endregion

        #region SvcNJOPRmhNgrCrud

        public static string SvcNJOPRmhNgrCrud_name = "call_pt120";
        public static string SvcNJOPRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/njopCud"; //fix
        #endregion

        #region SvcNJOPRmhNgrSelect
        public static string SvcNJOPRmhNgrSelect_name = "call_pt121";
        public static string SvcNJOPRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/njopSelect"; //fix
        #endregion

        #region SvcRwyMutRmhNgrSelect
        public static string SvcRwyMutRmhNgrSelect_name = "call_pt122";
        public static string SvcRwyMutRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/rwytMutasiSelect"; //fix
        #endregion


        #region SvcRwyNilRmhNgrSelect
        public static string SvcRwyNilRmhNgrSelect_name = "call_pt123";
        public static string SvcRwyNilRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/rwytNilai"; //fix
        #endregion

        #region SvcRwyPggRmhNgrSelect
        public static string SvcRwyPggRmhNgrSelect_name = "call_pt124";
        public static string SvcRwyPggRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/rwytPenggunaSelect"; //fix
        #endregion

        #region SvcRwyPlhrRmhNgrSelect
        public static string SvcRwyPlhrRmhNgrSelect_name = "call_pt125";
        public static string SvcRwyPlhrRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/rwytPeliharaSelect"; //fix
        #endregion


        #region SvcRwyRmhNgrSelect
        public static string SvcRwyRmhNgrSelect_name = "call_pt126";
        public static string SvcRwyRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/rwytRmhSelect"; //fix
        #endregion 

        #region SvcRmhNgrSelect
        public static string SvcRmhNgrSelect_name = "call_pt127";
        public static string SvcRmhNgrSelect_address = IP + SimanAPI + "/asetKrmh/select"; //fix
        #endregion

        #region SvcRmhNgrCud
        public static string SvcRmhNgrCrud_name = "call_pt128";
        public static string SvcRmhNgrCrud_address = IP + SimanAPI + "/asetKrmh/cud"; //fix
        #endregion

        #endregion
            
        #region Aset Bangunan
        public static string MasterAset = "Master Aset";
        public static string MenuBangunan = " > Bangunan";


        #region SvcBangunanSelect

        public static string SvcBangunanSelect_name = "call_pt137";
        public static string SvcBangunanSelect_address =IP + SimanAPI + "/asetBdg/select";

        #endregion


        #region SvcBangunanCrud

        public static string SvcBangunanCrud_name = "call_pt138";
        public static string SvcBangunanCrud_address = IP + SimanAPI + "/asetBdg/cud";

        #endregion

        #region SvcFasBangunanSelect

        public static string SvcFasBangunanSelect_name = "call_pt143";
        public static string SvcFasBangunanSelect_address = IP + SimanAPI + "/asetBdg/fasPenunjangSelect";

        #endregion

        #region SvcFasBangunanCrud

        public static string SvcFasBangunanCrud_name = "call_pt144";
        public static string SvcFasBangunanCrud_address = IP + SimanAPI + "/asetBdg/fasPenunjangCud";

        #endregion

        #region SvcRpemeliharaanBangunanSelect

        public static string SvcRpemeliharaanBangunanSelect_name = "call_pt147";
        public static string SvcRpemeliharaanBangunanSelect_address = IP + SimanAPI + "/asetBdg/rwytPeliharaSelect";

        #endregion

        #region SvcRnilaiBangunanSelect

        public static string SvcRnilaiBangunanSelect_name = "call_pt145";
        public static string SvcRnilaiBangunanSelect_address = IP + SimanAPI + "/asetBdg/rwytNilaiSelect";

        #endregion

        #region SvcRmutasiBangunanSelect

        public static string SvcRmutasiBangunanSelect_name = "call_pt149";
        public static string SvcRmutasiBangunanSelect_address = IP + SimanAPI + "/asetBdg/rwytMutasiSelect";

        #endregion

        #region SvcRpenggunaBangunanSelect

        public static string SvcRpenggunaBangunanSelect_name = "call_pt146";
        public static string SvcRpenggunaBangunanSelect_address = IP + SimanAPI + "/asetBdg/rwytPenggunaSelect";

        #endregion

        #region SvcRiwayatBangunanSelect

        public static string SvcRiwayatBangunanSelect_name = "call_pt148";
        public static string SvcRiwayatBangunanSelect_address = IP + SimanAPI + "/asetBdg/rwytBdgSelect";

        #endregion

        #region SvcDokBangunanSelect

        public static string SvcDokBangunanSelect_name = "call_pt139";
        public static string SvcDokBangunanSelect_address = IP + SimanAPI + "/asetBdg/dokSelect";

        #endregion

        #region SvcDokBangunanCrud

        public static string SvcDokBangunanCrud_name = "call_pt140";
        public static string SvcDokBangunanCrud_address = IP + SimanAPI + "/asetBdg/dokCud";

        #endregion

        #region SvcLokasiBangunanCrud

        public static string SvcLokasiBangunanCrud_name = "call_pt142";
        public static string SvcLokasiBangunanCrud_address = IP + SimanAPI + "/asetBdg/lokasiCud";

        #endregion

        #region SvcLokasiBangunanSelect

        public static string SvcLokasiBangunanSelect_name = "call_pt141";
        public static string SvcLokasiBangunanSelect_address = IP + SimanAPI + "/asetBdg/lokasiSelect";

        #endregion

        #region SvcDruanganBangunanSelect

        public static string SvcDruanganBangunanSelect_name = "call_pt183";
        public static string SvcDruanganBangunanSelect_address = IP + SimanAPI + "/asetBdg/detailBdgSelect";

        #endregion

        #region SvcDruanganBangunanCrud

        public static string SvcDruanganBangunanCrud_name = "call_pt184";
        public static string SvcDruanganBangunanCrud_address = IP + SimanAPI + "/asetBdg/detailCud";

        #endregion

        #region SvcGPSBangunanSelect

        public static string SvcGPSBangunanSelect_name = "call_pt181";
        public static string SvcGPSBangunanSelect_address = IP + SimanAPI + "/asetBdg/gpsSelect";

        #endregion

        #region SvcGPSBangunanCrud

        public static string SvcGPSBangunanCrud_name = "call_pt182";
        public static string SvcGPSBangunanCrud_address = IP + SimanAPI + "/asetBdg/gpsCud";

        #endregion

        #region SvcKonstruksiBangunanSelect

        public static string SvcKonstruksiBangunanSelect_name = "call_pt179";
        public static string SvcKonstruksiBangunanSelect_address = IP + SimanAPI + "/asetBdg/konsBdgSelect";

        #endregion

        #region SvcKonstruksiBangunanCrud

        public static string SvcKonstruksiBangunanCrud_name = "call_pt180";
        public static string SvcKonstruksiBangunanCrud_address = IP + SimanAPI + "/asetBdg/konsBdgCud";

        #endregion

        #region SvcNjopBangunanSelect

        public static string SvcNjopBangunanSelect_name = "call_pt177";
        public static string SvcNjopBangunanSelect_address = IP + SimanAPI + "/asetBdg/njopBdgSelect";

        #endregion

        #region SvcNjopBangunanCrud

        public static string SvcNjopBangunanCrud_name = "call_pt178";
        public static string SvcNjopBangunanCrud_address = IP + SimanAPI + "/asetBdg/njopCud";

        #endregion


        #region SvcPhukumBangunanSelect

        public static string SvcPhukumBangunanSelect_name = "call_pt175";
        public static string SvcPhukumBangunanSelect_address = IP + SimanAPI + "/asetBdg/hukumSelect";

        #endregion

        #region SvcPhukumBangunanCrud

        public static string SvcPhukumBangunanCrud_name = "call_pt176";
        public static string SvcPhukumBangunanCrud_address = IP + SimanAPI + "/asetBdg/hukumCud";

        #endregion

        #endregion

        #region Aset Tanah

        #region SvcAssetTanahSelect
        public static string SvcAssetTanahSelect_name = "call_pt129";
        public static string SvcAssetTanahSelect_address = IP + SimanAPI + "/asetTnh/select";
        #endregion

        #region SvcAssetTanahCrud
        public static string SvcAssetTanahCrud_name = "call_pt130";
        public static string SvcAssetTanahCrud_address = IP + SimanAPI + "/asetTnh/crud";
        #endregion


        #region SvcTnhBangunanSelect
        public static string SvcTnhBangunanSelect_name = "call_pt163";
        public static string SvcTnhBangunanSelect_address = IP + SimanAPI + "/asetTnhBangunan/select";
        #endregion

        #region SvcTnhBangunanCrud
        public static string SvcTnhBangunanCrud_name = "call_pt164";
        public static string SvcTnhBangunanCrud_address = IP + SimanAPI + "/asetTnhBangunan/cud";
        #endregion

        #region SvcTnhDokSelect
        public static string SvcTnhDokSelect_name = "call_pt131";
        public static string SvcTnhDokSelect_address = IP + SimanAPI + "/asetTnhDok/select";
        #endregion

        #region SvcTnhDokCrud
        public static string SvcTnhDokCrud_name = "call_pt132";
        public static string SvcTnhDokCrud_address = IP + SimanAPI + "/asetTnhDok/cud";
        #endregion

        #region SvcTnhFasSelect
        public static string SvcTnhFasSelect_name = "call_pt165";
        public static string SvcTnhFasSelect_address = IP + SimanAPI + "/asetTnhFasPenunjang/select";
        #endregion

        #region SvcTnhFasCrud
        public static string SvcTnhFasCrud_name = "call_pt166";
        public static string SvcTnhFasCrud_address = IP + SimanAPI + "/asetTnhFasPenunjang/cud";
        #endregion

        #region SvcTnhHukumSelect
        public static string SvcTnhHukumSelect_name = "call_pt135";
        public static string SvcTnhHukumSelect_address = IP + SimanAPI + "/asetTnhHukum/select";
        #endregion

        #region SvcTnhHukumCrud
        public static string SvcTnhHukumCrud_name = "call_pt136";
        public static string SvcTnhHukumCrud_address = IP + SimanAPI + "/asetTnhHukum/cud";
        #endregion

        #region SvcTnhLokasiSelect
        public static string SvcTnhLokasiSelect_name = "call_pt133";
        public static string SvcTnhLokasiSelect_address = IP + SimanAPI + "/asetTnhLoc/select";
        #endregion

        #region SvcTnhLokasiCrud
        public static string SvcTnhLokasiCrud_name = "call_pt134";
        public static string SvcTnhLokasiCrud_address = IP + SimanAPI + "/asetTnhLoc/cud";
        #endregion

        #region SvcTnhNjopSelect
        public static string SvcTnhNjopSelect_name = "call_pt167";
        public static string SvcTnhNjopSelect_address = IP + SimanAPI + "/asetTnhNjop/select";
        #endregion

        #region SvcTnhNjopCrud
        public static string SvcTnhNjopCrud_name = "call_pt168";
        public static string SvcTnhNjopCrud_address = IP + SimanAPI + "/asetTnhNjop/cud";
        #endregion

        #region SvcTnhRwyMutasiSelect
        public static string SvcTnhRwyMutasiSelect_name = "call_pt169";
        public static string SvcTnhRwyMutasiSelect_address = IP + SimanAPI + "/asetTnh/rwytMutasiSelect";
        #endregion

        #region SvcTnhRwyPeliharaSelect
        public static string SvcTnhRwyPeliharaSelect_name = "call_pt170";
        public static string SvcTnhRwyPeliharaSelect_address = IP + SimanAPI + "/asetTnh/rwytPeliharaSelect";
        #endregion

        #region SvcTnhRwyPenggunaSelect
        public static string SvcTnhRwyPenggunaSelect_name = "call_pt171";
        public static string SvcTnhRwyPenggunaSelect_address = IP + SimanAPI + "/asetTnh/rwytPenggunaSelect";
        #endregion

        #region SvcTnhRwyPenilaianSelect
        public static string SvcTnhRwyPenilaianSelect_name = "call_pt172";
        public static string SvcTnhRwyPenilaianSelect_address = IP + SimanAPI + "/asetTnh/rwytNilaiSelect";
        #endregion

        

        #endregion

        #region ASET SENJATA
        public static string MenuSenjata = " > Senjata";

        #region SvcSenjataSelect

        public static string SvcSenjataSelect_name = "call_pt186";
        public static string SvcSenjataSelect_address = IP + SimanAPI + "/asetSenj/select";

        #endregion

        #region SvcSenjataCrud

        public static string SvcSenjataCrud_name = "call_pt187";
        public static string SvcSenjataCrud_address = IP + SimanAPI + "/asetSenj/cud";

        #endregion

        #region SvcSenjataPerlengkapanCrud

        public static string SvcSenjataPerlengkapanCrud_name = "call_pt189";
        public static string SvcSenjataPerlengkapanCrud_address = IP + SimanAPI + "/asetSenj/perlengkapCud";

        #endregion

        #region SvcSenjataPerlengkapanSelect

        public static string SvcSenjataPerlengkapanSelect_name = "call_pt188";
        public static string SvcSenjataPerlengkapanSelect_address = IP + SimanAPI + "/asetSenj/perlengkapSelect";

        #endregion

        #region SvcSenjataRmutasiSelect

        public static string SvcSenjataRmutasiSelect_name = "call_pt190";
        public static string SvcSenjataRmutasiSelect_address = IP + SimanAPI + "/asetSenj/rwytMutasiSelect";

        #endregion


        #region SvcSenjataRnilaiSelect

        public static string SvcSenjataRnilaiSelect_name = "call_pt194";
        public static string SvcSenjataRnilaiSelect_address = IP + SimanAPI + "/asetSenj/rwytNilai";

        #endregion

        #region SvcSenjataRpenggunaSelect

        public static string SvcSenjataRpenggunaSelect_name = "call_pt193";
        public static string SvcSenjataRpenggunaSelect_address = IP + SimanAPI + "/asetSenj/rwytPenggunaSelect";

        #endregion

        #region SvcSenjataRpemakaiSelect

        public static string SvcSenjataRpemakaiSelect_name = "call_pt192";
        public static string SvcSenjataRpemakaiSelect_address = IP + SimanAPI + "/asetSenj/rwytPemakaiSelect";

        #endregion
        
        #region SvcSenjataRpemeliharaanSelect

        public static string SvcSenjataRpemeliharaanSelect_name = "call_pt191";
        public static string SvcSenjataRpemeliharaanSelect_address = IP + SimanAPI + "/asetSenj/rwytPeliharaSelect";

        #endregion

        #endregion //ASET SENJATA

        #region ASET MESIN dan PERALATAN NON TIK
        
            #region SvcMesinPntSelect

            public static string SvcMesinPntSelect_name = "call_pt195";
            public static string SvcMesinPntSelect_address = IP + SimanAPI + "/asetKalb/select";

            #endregion

            #region SvcMesinPntCrud

            public static string SvcMesinPntCrud_name = "call_pt196";
            public static string SvcMesinPntCrud_address = IP + SimanAPI + "/asetKalb/cud";

            #endregion

            #region SvcMesinPntRmutasiSelect
                public static string SvcMesinPntRmutasiSelect_name = "call_pt197";
                public static string SvcMesinPntRmutasiSelect_address = IP + SimanAPI + "/asetKalb/rwytMutasiSelect";
            #endregion

            #region SvcMesinPntRnilaiSelect
                public static string SvcMesinPntRnilaiSelect_name = "call_pt198";
                public static string SvcMesinPntRnilaiSelect_address = IP + SimanAPI + "/asetKalb/rwytNilaiSelect";
            #endregion

            #region SvcMesinPntRpemeliharaanSelect
                public static string SvcMesinPntRpemeliharaanSelect_name = "call_pt199";
                public static string SvcMesinPntRpemeliharaanSelect_address = IP + SimanAPI + "/asetKalb/rwytPeliharaSelect";
            #endregion

            #region SvcMesinPntRpenggunaSelect
                public static string SvcMesinPntRpenggunaSelect_name = "call_pt200";
                public static string SvcMesinPntRpenggunaSelect_address = IP + SimanAPI + "/asetKalb/rwytPenggunaSelect";
            #endregion
        #endregion //Mesin dan PNT

                #region ASET TETAP LAINNYA


                #region SvcLnySelect
                public static string SvcLnySelect_name = "call_pt201";
                public static string SvcLnySelect_address = IP + SimanAPI + "/asetKlain/select"; //fix
                #endregion

                #region SvcLnyCrud
                public static string SvcLnyCrud_name = "call_pt202";
                public static string SvcLnyCrud_address = IP + SimanAPI + "/asetKlain/cud"; //fix
                #endregion

                #region SvcLnyMutSelect
                public static string SvcLnyMutSelect_name = "call_pt203";
                public static string SvcLnyMutSelect_address = IP + SimanAPI + "/asetKlain/rwytMutasiSelect"; //fix
                #endregion

                #region SvcLnyNilSelect
                public static string SvcLnyNilSelect_name = "call_pt204";
                public static string SvcLnyNilSelect_address = IP + SimanAPI + "/asetKlain/rwytNilaiSelect"; //fix
                #endregion

                #region SvcLnyPggSelect
                public static string SvcLnyPggSelect_name = "call_pt206";
                public static string SvcLnyPggSelect_address = IP + SimanAPI + "/asetKlain/rwytPenggunaSelect"; //fix
                #endregion

                #region SvcLnyPlrSelect
                public static string SvcLnyPlrSelect_name = "call_pt205";
                public static string SvcLnyPlrSelect_address = IP + SimanAPI + "/asetKlain/rwytPeliharaSelect"; //fix
                #endregion

                #endregion

                #region ASET KONSTRUKSI DALAM PENGERJAAN


                #region SvcKDPSelect
                public static string SvcKDPSelect_name = "call_pt207";
                public static string SvcKDPSelect_address = IP + SimanAPI + "/asetKdp/select"; //fix
                #endregion

                #region SvcKDPCrud
                public static string SvcKDPCrud_name = "call_pt208";
                public static string SvcKDPCrud_address = IP + SimanAPI + "/asetKdp/cud"; //fix
                #endregion

                #region SvcKDPMutasiSelect
                public static string SvcKDPMutasiSelect_name = "call_pt209";
                public static string SvcKDPMutasiSelect_address = IP + SimanAPI + "/asetKdp/rwytMutasiSelect"; //fix
                #endregion

                #region SvcNilaiKDPSelect
                public static string SvcNilaiKDPSelect_name = "call_pt210";
                public static string SvcNilaiKDPSelect_address = IP + SimanAPI + "/asetKdp/rwytNilaiSelect"; //fix
                #endregion

                #region SvcKDPPggSelect
                public static string SvcKDPPggSelect_name = "call_pt211";
                public static string SvcKDPPggSelect_address = IP + SimanAPI + "/asetKdp/rwytPenggunaSelect"; //fix
                #endregion



                #endregion


                #region ASET TAK BERWUJUD


                #region SvcATBSelect
                public static string SvcATBSelect_name = "call_pt212";
                public static string SvcATBSelect_address = IP + SimanAPI + "/asetKtwjd/select"; //fix
                #endregion

                #region SvcATBCrud
                public static string SvcATBCrud_name = "call_pt213";
                public static string SvcATBCrud_address = IP + SimanAPI + "/asetKtwjd/cud"; //fix
                #endregion

                #region SvcATBMutSelect
                public static string SvcATBMutSelect_name = "call_pt218";
                public static string SvcATBMutSelect_address = IP + SimanAPI + "/asetKtwjd/rwytMutasiSelect"; //fix
                #endregion

                #region SvcATBNilSelect
                public static string SvcATBNilSelect_name = "call_pt219";
                public static string SvcATBNilSelect_address = IP + SimanAPI + "/asetKtwjd/rwytNilaiSelect"; //fix
                #endregion

                #region SvcATBPggSelect
                public static string SvcATBPggSelect_name = "call_pt216";
                public static string SvcATBPggSelect_address = IP + SimanAPI + "/asetKtwjd/rwytPenggunaSelect"; //fix
                #endregion

                #region SvcATBPlrSelect
                public static string SvcATBPlrSelect_name = "call_pt217";
                public static string SvcATBPlrSelect_address = IP + SimanAPI + "/asetKtwjd/rwytPeliharaSelect"; //fix
                #endregion

                #region SvcATBPerkapSelect
                public static string SvcATBPerkapSelect_name = "call_pt214";
                public static string SvcATBPerkapSelect_address = IP + SimanAPI + "/asetKtwjd/perlengkapSelect"; //fix
                #endregion

                #region SvcATBPerkapCrud
                public static string SvcATBPerkapCrud_name = "call_pt215";
                public static string SvcATBPerkapCrud_address = IP + SimanAPI + "/asetKtwjd/perlengkapCud"; //fix
                #endregion
                #endregion

                #region Penelusuran Aset

                #region SvcPenelusuranAsetSelect
                public static string SvcPenelusuranAsetSelect_name = "call_pt228";
                public static string SvcPenelusuranAsetSelect_address = IP + SimanAPI + "/aset/trackingSelect"; //fix
                #endregion

                #endregion

                #region Bangunan Air
                public static string SvcBangunanAirSelect_name = "call_pt220";
                public static string SvcBangunanAirSelect_address = IP + SimanAPI + "/asetBair/select";

                #region Bangunan Air Crud
                public static string SvcBangunanAirCrud_name = "call_pt221";
                public static string SvcBangunanAirCrud_address = IP + SimanAPI + "/asetBair/cud";
                #endregion

                #region Bangunan Air Lokasi Select
                public static string SvcBgnAirLokSelect_name = "call_pt222";
                public static string SvcBgnAirLokSelect_address = IP + SimanAPI + "/asetBair/lokasiSelect";
                #endregion

                #region Bangunan Air Lokasi Crud
                public static string SvcBgnAirLokCrud_name = "call_pt223";
                public static string SvcBgnAirLokCrud_address = IP + SimanAPI + "/asetBair/lokasiCud";
                #endregion

                #region Bangunan Air Mutasi Select
                public static string SvcBgnAirMutasiSelect_name = "call_pt224";
                public static string SvcBgnAirMutasiSelect_address = IP + SimanAPI + "/asetBair/rwytMutasiSelect";
                #endregion

                #region Bangunan Air Nilai Select
                public static string SvcBgnAirNilaiSelect_name = "call_pt225";
                public static string SvcBgnAirNilaiSelect_address = IP + SimanAPI + "/asetBair/rwytNilaiSelect";
                #endregion

                #region Bangunan Air Pemelihara Select
                public static string SvcBgnAirPemeliharaSelect_name = "call_pt226";
                public static string SvcBgnAirPemeliharaSelect_address = IP + SimanAPI + "/asetBair/rwytPeliharaSelect";
                #endregion

                #region Bangunan Air Pengguna Select
                public static string SvcBgnAirPenggunaSelect_name = "call_pt227";
                public static string SvcBgnAirPenggunaSelect_address = IP + SimanAPI + "/asetBair/rwytPenggunaSelect";
                #endregion

                #endregion

                #region Properti Khusus
                public static string SvcProKSelect_name = "call_pt233";
                public static string SvcProKSelect_address = IP + SimanAPI + "/asetKprok/select";

                #region Properti Khusus Crud
                public static string SvcProKCrud_name = "call_pt234";
                public static string SvcProKCrud_address = IP + SimanAPI + "/asetKprok/cud";
                #endregion

                #region Properti Khusus Lokasi Select
                public static string SvcProKLokSelect_name = "call_pt235";
                public static string SvcProKLokSelect_address = IP + SimanAPI + "/asetKprok/lokasiSelect";
                #endregion

                #region Properti Khusus Lokasi Crud
                public static string SvcProKLokCrud_name = "call_pt236";
                public static string SvcProKLokCrud_address = IP + SimanAPI + "/asetKprok/lokasiCud";
                #endregion

                #region Properti Khusus Pengguna
                public static string SvcProKPenggunaSelect_name = "call_pt240";
                public static string SvcProKPenggunaSelect_address = IP + SimanAPI + "/asetKprok/rwytPenggunaSelect";
                #endregion

                #region Properti Khusus Mutasi
                public static string SvcProKMutasiSelect_name = "call_pt237";
                public static string SvcProKMutasiSelect_address = IP + SimanAPI + "/asetKprok/rwytMutasiSelect";
                #endregion

                #region Properti Khusus Pemelihara
                public static string SvcProKPemeliharaSelect_name = "call_pt239";
                public static string SvcProKPemeliharaSelect_address = IP + SimanAPI + "/asetKprok/rwytPeliharaSelect";
                #endregion

                #region Properti Khusus Nilai
                public static string SvcProKNilaiSelect_name = "call_pt238";
                public static string SvcProKNilaiSelect_address = IP + SimanAPI + "/asetKprok/rwytNilaiSelect";
                #endregion

                #endregion

                #region Master dan Peralatan Khusus TIK
                public static string SvcMPKTSelect_name = "call_pt241";
                public static string SvcMPKTSelect_address = IP + SimanAPI + "/asetKtik/select";

                #region Master dan Peralatan Khusus TIK Crud
                public static string SvcMPKTCrud_name = "call_pt242";
                public static string SvcMPKTCrud_address = IP + SimanAPI + "/asetKtik/cud";
                #endregion

                #region Master dan Peralatan Khusus TIK Mutasi
                public static string SvcMPKTMutasi_name = "call_pt243";
                public static string SvcMPKTMutasi_address = IP + SimanAPI + "/asetKtik/rwytMutasiSelect";
                #endregion

                #region Master dan Peralatan Khusus TIK Nilai
                public static string SvcMPKTNilai_name = "call_pt244";
                public static string SvcMPKTNilai_address = IP + SimanAPI + "/asetKtik/rwytNilaiSelect";
                #endregion

                #region Master dan Peralatan Khusus TIK Pemeliharaan
                public static string SvcMPKTPemeliharaan_name = "call_pt245";
                public static string SvcMPKTPemeliharaan_address = IP + SimanAPI + "/asetKtik/rwytPeliharaSelect";
                #endregion

                #region Master dan Peralatan Khusus TIK Pengguna
                public static string SvcMPKTPengguna_name = "call_pt246";
                public static string SvcMPKTPengguna_address = IP + SimanAPI + "/asetKtik/rwytPenggunaSelect";
                #endregion

                #endregion

                #region PERSEDIAAN
                public static string SvcPersediaanSelect_name = "call_pt272";
                public static string SvcPersediaanSelect_address = IP + SimanAPI + "/asetSedia/select";

                #region PERSEDIAAN CRUD
                public static string SvcPersediaanCrud_name = "call_pt273";
                public static string SvcPersediaanCrud_address = IP + SimanAPI + "/asetSedia/cud";
                #endregion

                #region PERSEDIAAN SALDO
                public static string SvcPersediaanSaldo_name = "call_pt274";
                public static string SvcPersediaanSaldo_address = IP + SimanAPI + "/asetSedia/saldoSelect";
                #endregion

                #region PERSEDIAAN MUTASI
                public static string SvcPersediaanMutasi_name = "call_pt275";
                public static string SvcPersediaanMutasi_address = IP + SimanAPI + "/asetSedia/rwytMutasiSelect";
                #endregion

                #region PERSEDIAAN PENGGUNA
                public static string SvcPersediaanPengguna_name = "call_pt276";
                public static string SvcPersediaanPengguna_address = IP + SimanAPI + "/asetSedia/rwytPenggunaSelect";
                #endregion

                #endregion

                #region ASET ANGKUTAN


                #region SvcAngkSelect
                public static string SvcAngkSelect_name = "call_pt247";
                public static string SvcAngkSelect_address = IP + SimanAPI + "/asetAngk/select";
                #endregion

                #region SvcAngkCrud
                public static string SvcAngkCrud_name = "call_pt248";
                public static string SvcAngkCrud_address = IP + SimanAPI + "/asetAngk/cud";
                #endregion

                #region SvcFasAngkSelect
                public static string SvcFasAngkSelect_name = "call_pt249";
                public static string SvcFasAngkSelect_address = IP + SimanAPI + "/asetAngk/fasPenunjangSelect";
                #endregion

                #region SvcFasAngkCrud
                public static string SvcFasAngkCrud_name = "call_pt250";
                public static string SvcFasAngkCrud_address = IP + SimanAPI + "/asetAngk/fasPenunjangCud";
                #endregion

                #region SvcNoPolAngkSelect
                public static string SvcNoPolAngkSelect_name = "call_pt251";
                public static string SvcNoPolAngkSelect_address = IP + SimanAPI + "/asetAngk/nopolSelect";
                #endregion

                #region SvcNoPolAngkCrud
                public static string SvcNoPolAngkCrud_name = "call_pt252";
                public static string SvcNoPolAngkCrud_address = IP + SimanAPI + "/asetAngk/nopolCud";

                #endregion

                #region SvcBpkbAngkSelect
                public static string SvcBpkbAngkSelect_name = "call_pt253";
                public static string SvcBpkbAngkSelect_address = IP + SimanAPI + "/asetAngk/bpkbSelect";
                #endregion

                #region SvcBpkbAngkCrud
                public static string SvcBpkbAngkCrud_name = "call_pt254";
                public static string SvcBpkbAngkCrud_address = IP + SimanAPI + "/asetAngk/bpkbCud";
                #endregion

                #region SvcRwyMutAngkSelect
                public static string SvcRwyMutAngkSelect_name = "call_pt255";
                public static string SvcRwyMutAngkSelect_address = IP + SimanAPI + "/asetAngk/rwytMutasiSelect";
                #endregion

                #region SvcRwyNilaiAngkSelect
                public static string SvcRwyNilaiAngkSelect_name = "call_pt256";
                public static string SvcRwyNilaiAngkSelect_address = IP + SimanAPI + "/asetAngk/rwytNilaiSelect";
                #endregion

                #region SvcRwyPemeliharaanAngkSelect
                public static string SvcRwyPemeliharaanAngkSelect_name = "call_pt257";
                public static string SvcRwyPemeliharaanAngkSelect_address = IP + SimanAPI + "/asetAngk/rwytPeliharaSelect";
                #endregion

                #region SvcRwyPemakaiAngkSelect
                public static string SvcRwyPemakaiAngkSelect_name = "call_pt258";
                public static string SvcRwyPemakaiAngkSelect_address = IP + SimanAPI + "/asetAngk/rwytPemakaiSelect";
                #endregion

                #region SvcRwyPenggunaAngkSelect
                public static string SvcRwyPenggunaAngkSelect_name = "call_pt259";
                public static string SvcRwyPenggunaAngkSelect_address = IP + SimanAPI + "/asetAngk/rwytPenggunaSelect";
                #endregion

                #endregion

                #region ASET JALAN DAN JEMBATAN

                #region SvcJlnJmbtnSelect
                public static string SvcJlnJmbtnSelect_name = "call_pt260";
                public static string SvcJlnJmbtnSelect_address = IP + SimanAPI + "/asetKjalj/select";
                #endregion

                #region SvcJlnJmbtnCrud
                public static string SvcJlnJmbtnCrud_name = "call_pt261";
                public static string SvcJlnJmbtnCrud_address = IP + SimanAPI + "/asetKjalj/cud";
                #endregion


                #region SvcLokasiJlnJmbtnSelect
                public static string SvcLokasiJlnJmbtnSelect_name = "call_pt262";
                public static string SvcLokasiJlnJmbtnSelect_address = IP + SimanAPI + "/asetKjalj/lokasiSelect";
                #endregion

                #region SvcLokasiJlnJmbtnCrud
                public static string SvcLokasiJlnJmbtnCrud_name = "call_pt263";
                public static string SvcLokasiJlnJmbtnCrud_address = IP + SimanAPI + "/asetKjalj/lokasiCud";
                #endregion

                #region SvcRwyMutJlnJmbtnSelect
                public static string SvcRwyMutJlnJmbtnSelect_name = "call_pt264";
                public static string SvcRwyMutJlnJmbtnSelect_address = IP + SimanAPI + "/asetKjalj/rwytMutasiSelect";
                #endregion

                #region SvcRwyNilaiJlnJmbtnSelect
                public static string SvcRwyNilaiJlnJmbtnSelect_name = "call_pt265";
                public static string SvcRwyNilaiJlnJmbtnSelect_address = IP + SimanAPI + "/asetKjalj/rwytNilaiSelect";
                #endregion

                #region SvcRwyPemeliharaanJlnJmbtnSelect
                public static string SvcRwyPemeliharaanJlnJmbtnSelect_name = "call_pt266";
                public static string SvcRwyPemeliharaanJmbtnSelect_address = IP + SimanAPI + "/asetKjalj/rwytPeliharaSelect";
                #endregion

                #region SvcRwyPenggunaJlnJmbtnSelect
                public static string SvcRwyPenggunaJlnJmbtnSelect_name = "call_pt267";
                public static string SvcRwyPenggunaJlnJmbtnSelect_address = IP + SimanAPI + "/asetKjalj/rwytPenggunaSelect";
                #endregion

                #endregion //JALAN dan JEMBATAN

                #region ASET RENOVASI
                #region SvcRenovasiSelect
                public static string SvcRenovasiSelect_name = "call_pt268";
                public static string SvcRenovasiSelect_address = IP + SimanAPI + "/asetKrnv/select";
                #endregion

                #region SvcRenovasiCrud
                public static string SvcRenovasiCrud_name = "call_pt269";
                public static string SvcRenovasiCrud_address = IP + SimanAPI + "/asetKrnv/cud";
                #endregion

                #region SvcRwyMutRenovSelect
                public static string SvcRwyMutRenovSelect_name = "call_pt270";
                public static string SvcRwyMutRenovSelect_address = IP + SimanAPI + "/asetKrnv/rwytMutasiSelect";
                #endregion

                #region SvcRwyPenggunaRenovSelect
                public static string SvcRwyPenggunaRenovSelect_name = "call_pt271";
                public static string SvcRwyPenggunaRenovSelect_address = IP + SimanAPI + "/asetKrnv/rwytPenggunaSelect";
                #endregion
                #endregion//ASET RENOVASI

                #region SvcJnsDokSelect

                public static string SvcJnsDokSelect_name = "call_pt59";
        public static string SvcJnsDokSelect_address = IP + SimanAPI + "/dsRJnsDok/dsRJnsDokSelect";

        #endregion

        #region SvcSatkerSelect

        public static string SvcSatkerSelect_name = "call_pt27";
        public static string SvcSatkerSelect_address = IP + SimanAPI + "/dsRSatker/dsRSatkerSelect";

        #endregion

        #region SvcKondisiSelect

        public static string SvcKondisiSelect_name = "call_pt";
        public static string SvcKondisiSelect_address = IP + SimanAPI + "/dsRKondisi/svcDsRKondisiSelect";

        #endregion

        #region SvcStatusSelect

        public static string SvcStatusSelect_name = "call_pt8";
        public static string SvcStatusSelect_address = IP + SimanAPI + "/dsRStatus/svcDsRStatusSelect";

        #endregion

        #region SvcAsetPhotoSelect
        public static string SvcAsetPhotoSelect_name = "call_pt277";
        public static string SvcAsetPhotoSelect_address = IP + SimanAPI + "/aset/photoSelect";
        #endregion

        #region SvcAsetPhotoCrud
        public static string SvcAsetPhotoCrud_name = "call_pt278";
        public static string SvcAsetPhotoCrud_address = IP + SimanAPI + "/aset/photoCud";
        #endregion

        #region Barang Bersejarah

        #region SvcSjrhSelect

        public static string SvcSjrhSelect_name = "call_pt288";
        public static string SvcSjrhSelect_address = IP + SimanAPI + "/asetSejarah/select";

        #endregion

        #region SvcSjrhCrud

        public static string SvcSjrhCrud_name = "call_pt289";
        public static string SvcSjrhCrud_address = IP + SimanAPI + "/asetSejarah/cud";

        #endregion

        #region SvcSjrhRwyMutasiSelect

        public static string SvcSjrhRwyMutasiSelect_name = "call_pt290";
        public static string SvcSjrhRwyMutasiSelect_address = IP + SimanAPI + "/asetSejarah/rwytMutasiSelect";
        #endregion

        #region SvcSjrhRwyPeliharaSelect
        public static string SvcSjrhRwyPeliharaSelect_name = "call_pt291";
        public static string SvcSjrhRwyPeliharaSelect_address = IP + SimanAPI + "/asetSejarah/rwytPeliharaSelect";
        #endregion

        #region SvcSjrhRwyPenggunaSelect
        public static string SvcSjrhRwyPenggunaSelect_name = "call_pt292";
        public static string SvcSjrhRwyPenggunaSelect_address = IP + SimanAPI + "/asetSejarah/rwytPenggunaSelect";
        #endregion


        #endregion

        #region Variable ASET PHOTO
        public static string konfirmasiAmbilFoto = " Anda yakin akan mengambil foto dari database?";
        public static string konfirmasiHapusFoto = "Anda yakin akan menghapus foto yang sedang ditampilkan?";

        public static string konfirmasiHapusFotoDb = "Anda yakin akan menghapus foto yang sedang ditampilkan ?";
        public static string konfirmasiMaksimalFoto = "Foto tidak boleh lebih dari 5 dan tidak boleh lebih besar dari 2 MB";
        public static int maksFoto = 5;
        public static int maksSizeFoto = 2097152; //2MB
        #endregion//ASET PHOTO

        #region SvcAsetGetDokSelect
        public static string SvcAsetGetDokSelect_name = "call_pt287";
        public static string SvcAsetGetDokSelect_address = IP + SimanAPI + "/aset/isiFileSelect";
        #endregion//ASET DOKUMEN

        public static int maksSizeFile = 2097152; //2MB
        public static string konfirmasiMaksimalFile = "Ukuran file tidak bisa lebih dari 2MB";

        #endregion


        #region PENGELOLAAN
        #region SvcSelectTiketBySatker
        public static string SvcSelectTiketBySatker_name = "call_pt154";
        public static string SvcSelectTiketBySatker_address = IP + SimanAPI + "/PenggunaStatGuna/selectTiketBySatker";
        #endregion

        #region SvcTiketPStatGunaDetail
        public static string SvcTiketPStatGunaDetail_name = "call_pt156";
        public static string SvcTiketPStatGunaDetail_address = IP + SimanAPI + "/PenggunaStatGuna/getDetailTiket";
        #endregion

        #region SvcTiketPStatGunaCreate
        public static string SvcTiketPStatGunaCreate_name = "call_pt155";
        public static string SvcTiketPStatGunaCreate_address = IP + SimanAPI + "/PenggunaStatGuna/createTiket";
        #endregion

        #region SvcTiketPStatGunaCancel
        public static string SvcTiketPStatGunaCancel_name = "call_pt158";
        public static string SvcTiketPStatGunaCancel_address = IP + SimanAPI + "/PenggunaStatGuna/cancelTiket";
        #endregion

        #region SvcTiketPStatGunaSubmit
        public static string SvcTiketPStatGunaSubmit_name = "call_pt162";
        public static string SvcTiketPStatGunaSubmit_address = IP + SimanAPI + "/PenggunaStatGuna/submitTiketSatker";
        #endregion

        #region SvcTiketPStatGunaUpdate
        public static string SvcTiketPStatGunaUpdate_name = "call_pt157";
        public static string SvcTiketPStatGunaUpdate_address = IP + SimanAPI + "/PenggunaStatGuna/updateTiketSurat";
        #endregion

        #region SvcTiketPStatGunaSelectByKorwil
        public static string SvcTiketPStatGunaSelectByKorwil_name = "selectTiketByKorwil_pt";
        public static string SvcTiketPStatGunaSelectByKorwil_address = IP + SimanAPI + "/PenggunaStatGuna/selectTiketByKorwil";
        #endregion

        #region SvcTiketPStatGunaReceiveKorwil
        public static string SvcTiketPStatGunaReceiveKorwil_name = "receiveTiketByKorwil_pt";
        public static string SvcTiketPStatGunaReceiveKorwil_address = IP + SimanAPI + "/PenggunaStatGuna/receiveTiketByKorwil";
        #endregion

        #region SvcAsetPStatGunaSelect
        public static string SvcAsetPStatGunaSelect_name = "call_pt159";
        public static string SvcAsetPStatGunaSelect_address = IP + SimanAPI + "/PenggunaStatGuna/selectItemAset";
        #endregion

        #region SvcAsetPStatGunaHapus
        public static string SvcAsetPStatGunaHapus_name = "hapusItemAset_pt";
        public static string SvcAsetPStatGunaHapus_address = IP + SimanAPI + "/PenggunaStatGuna/hapusItemAset";
        #endregion

        #region SvcAsetPStatGunaTambah
        public static string SvcAsetPStatGunaTambah_name = "call_pt161";
        public static string SvcAsetPStatGunaTambah_address = IP + SimanAPI + "/PenggunaStatGuna/tambahItemAset";
        #endregion
       
        #region aset select
        public static string SvcAsetSelect_name = "call_pt160";
        public static string SvcAsetSelect_address = IP + SimanAPI + "/aset/select";
        #endregion

        #region SvcSelectTiketByKorwil
        public static string SvcSelectTiketByKorwil_name = "selectTiketByKorwil_pt1";
        public static string SvcSelectTiketByKorwil_address = IP + SimanAPI + "/PenggunaStatGuna/selectTiketByKorwil";
        #endregion

        #region SvcDokItemAsetPStatGunaSelect
        public static string SvcDokItemAsetPStatGunaSelect_name = "selectDokItemAset_pt";
        public static string SvcDokItemAsetPStatGunaSelect_address = IP + SimanAPI + "/PenggunaStatGuna/selectDokItemAset";
        #endregion

        #region SvcDokItemAsetPStatGunaHapus
        public static string SvcDokItemAsetPStatGunaHapus_name = "deleteDokItemAset_pt";
        public static string SvcDokItemAsetPStatGunaHapus_address = IP + SimanAPI + "/PenggunaStatGuna/deleteDokItemAset";
        #endregion

        #region SvcTiketPStatGunaSubmitByKorwil
        public static string SvcTiketPStatGunaSubmitByKorwil_name = "submitTiketByKorwil_pt";
        public static string SvcTiketPStatGunaSubmitByKorwil_address = IP + SimanAPI + "/PenggunaStatGuna/submitTiketByKorwil";
        #endregion

        #region SvcTiketPStatGunaSelectByKl
        public static string SvcTiketPStatGunaSelectByKl_name = "selectTiketByKl_pt";
        public static string SvcTiketPStatGunaSelectByKl_address = IP + SimanAPI + "/PenggunaStatGuna/selectTiketByKl";
        #endregion

        #region SvcTiketPStatGunaReceiveKl
        public static string SvcTiketPStatGunaReceiveKl_name = "receiveTiketByKl_pt";
        public static string SvcTiketPStatGunaReceiveKl_address = IP + SimanAPI + "/PenggunaStatGuna/receiveTiketByKl";
        #endregion

        #region SvcTiketPStatGunaSubmitKl
        public static string SvcTiketPStatGunaSubmitKl_name = "submitTiketByKl_pt";
        public static string SvcTiketPStatGunaSubmitKl_address = IP + SimanAPI + "/PenggunaStatGuna/submitTiketByKl";
        #endregion

        #region SvcTiketPStatGunaWPengajuan
        public static string SvcTiketPStatGunaWPengajuan_name = "wewenangPengajuan_pt";
        public static string SvcTiketPStatGunaWPengajuan_address = IP + SimanAPI + "/PengelolaStatGuna/wewenangPengajuan";
        #endregion

        #region SvcDokItemAsetCrud
        public static string SvcDokItemAsetCrud_name = "crudDokItemAset_pt";
        public static string SvcDokItemAsetCrud_address = IP + SimanAPI + "/PenggunaStatGuna/crudDokItemAset";
        #endregion

        #region SvcTiketPStatGunaUpdateIsTb
        public static string SvcTiketPStatGunaUpdateIsTb_name = "updateTiket_pt";
        public static string SvcTiketPStatGunaUpdateIsTb_address = IP + SimanAPI + "/PenggunaStatGuna/updateTiket";
        #endregion

        #region SvcDokPengajuanFileUpdate
        public static string SvcDokPengajuanFileUpdate_name = "call_pt280";
        public static string SvcDokPengajuanFileUpdate_address = IP + SimanAPI + "/PenggunaStatGuna/updateTiketSurat";
        #endregion

        #region SvcGetFileDokPengajuan
        public static string SvcGetFileDokPengajuan_name = "getBlobTiketSurat_pt";
        public static string SvcGetFileDokPengajuan_address = IP + SimanAPI + "/PenggunaStatGuna/getBlobTiketSurat";
        #endregion

        #region SvcTiketPengelolaStatGunaSelect
        public static string SvcTiketPengelolaStatGunaSelect_name = "selectTiketByPengelola_pt";
        public static string SvcTiketPengelolaStatGunaSelect_address = IP + SimanAPI + "/PengelolaStatGuna/selectTiketByPengelola";
        #endregion

        #region SvcTiketPengelolaStatGunaReceive
        public static string SvcTiketPengelolaStatGunaReceive_name = "receiveTiket_pt";
        public static string SvcTiketPengelolaStatGunaReceive_address = IP + SimanAPI + "/PengelolaStatGuna/receiveTiket";
        #endregion
        
        #region SvcTiketPengelolaStatGunaDisposisi
        public static string SvcTiketPengelolaStatGunaDisposisi_name = "disposisi_pt";
        public static string SvcTiketPengelolaStatGunaDisposisi_address = IP + SimanAPI + "/PengelolaStatGuna/disposisi";
        #endregion

        #region SvcPengelolaStatGunaSelectAnalis
        public static string SvcPengelolaStatGunaSelectAnalis_name = "selectAnalis_pt";
        public static string SvcPengelolaStatGunaSelectAnalis_address = IP + SimanAPI + "/PengelolaStatGuna/selectAnalis";
        #endregion

        #region SvcPengelolaStatGunaReceiveDisposisi
        public static string SvcPengelolaStatGunaReceiveDisposisi_name = "receiveDisposisi_pt";
        public static string SvcPengelolaStatGunaReceiveDisposisi_address = IP + SimanAPI + "/PengelolaStatGuna/receiveDisposisi";
        #endregion

        #region SvcTiketPengelolaStatGunaSelectByAnalis
        public static string SvcTiketPengelolaStatGunaSelectByAnalis_name = "selectTiketByAnalis_ptt";
        public static string SvcTiketPengelolaStatGunaSelectByAnalis_address = IP + SimanAPI + "/PengelolaStatGuna/selectTiketByAnalis";
        #endregion

        #region SvcPengelolaStatGunaUpdateSk
        public static string SvcPengelolaStatGunaUpdateSk_name = "updateSk_pt";
        public static string SvcPengelolaStatGunaUpdateSk_address = IP + SimanAPI + "/PengelolaStatGuna/updateSk";
        #endregion

        #region SvcPengelolaStatGunaUpdateAnalisa
        public static string SvcPengelolaStatGunaUpdateAnalisa_name = "updateAnalisa_pt1";
        public static string SvcPengelolaStatGunaUpdateAnalisa_address = IP + SimanAPI + "/PengelolaStatGuna/updateAnalisa";
        #endregion

        #region SvcPengelolaStatGunaUpdateNotifikasi
        public static string SvcPengelolaStatGunaUpdateNotifikasi_name = "updateNotifikasi_pt";
        public static string SvcPengelolaStatGunaUpdateNotifikasi_address = IP + SimanAPI + "/PengelolaStatGuna/updateNotifikasi";
        #endregion

        #region SvcPengelolaStatGunaSendNotifByAnalis
        public static string SvcPengelolaStatGunaSendNotifByAnalis_name = "sendNotifikasiByAnalis_pt";
        public static string SvcPengelolaStatGunaSendNotifByAnalis_address = IP + SimanAPI + "/PengelolaStatGuna/sendNotifikasiByAnalis";
        #endregion

        #region SvcPengelolaStatGunaStateLengkap
        public static string SvcPengelolaStatGunaStateLengkap_name = "stateLengkap_pt";
        public static string SvcPengelolaStatGunaStateLengkap_address = IP + SimanAPI + "/PengelolaStatGuna/stateLengkap";
        #endregion

        #region SvcPengelolaStatGunaSendAnalisSk
        public static string SvcPengelolaStatGunaSendAnalisSk_name = "sendAnalisaSk_pt";
        public static string SvcPengelolaStatGunaSendAnalisSk_address = IP + SimanAPI + "/PengelolaStatGuna/sendAnalisaSk";
        #endregion

        #region SvcPengelolaStatGunaReceiveSkByPengelola
        public static string SvcPengelolaStatGunaReceiveSkByPengelola_name = "receiveSkByPengelola_pt";
        public static string SvcPengelolaStatGunaReceiveSkByPengelola_address = IP + SimanAPI + "/PengelolaStatGuna/receiveSkByPengelola";
        #endregion

        #region SvcPengelolaStatGunaSendSkByPengelola
        public static string SvcPengelolaStatGunaSendSkByPengelola_name = "sendSkByPengelola_pt";
        public static string SvcPengelolaStatGunaSendSkByPengelola_address = IP + SimanAPI + "/PengelolaStatGuna/sendSkByPengelola";
        #endregion

        #region SvcPsgReceiveSkByPemohonKl
        public static string SvcPsgReceiveSkByPemohonKl_name = "receiveKeputusanByPemohonKl_pt";
        public static string SvcPsgReceiveSkByPemohonKl_address = IP + SimanAPI + "/PenggunaStatGuna/receiveKeputusanByPemohonKl";
        #endregion

        #region SvcPsgReceiveSkByPemohonKorwil
        public static string SvcPsgReceiveSkByPemohonKorwil_name = "receiveKeputusanByPemohonKorwil_pt";
        public static string SvcPsgReceiveSkByPemohonKorwil_address = IP + SimanAPI + "/PenggunaStatGuna/receiveKeputusanByPemohonKorwil";
        #endregion

        #region SvcPsgReceiveSkByPemohonSatker
        public static string SvcPsgReceiveSkByPemohonSatker_name = "receiveKeputusanByPemohonSatker_pt";
        public static string SvcPsgReceiveSkByPemohonSatker_address = IP + SimanAPI + "/PenggunaStatGuna/receiveKeputusanByPemohonSatker";
        #endregion

        #region SvcPsgReceiveSkBySatker
        public static string SvcPsgReceiveSkBySatker_name = "receiveKeputusanBySatker_pt";
        public static string SvcPsgReceiveSkBySatker_address = IP + SimanAPI + "/PenggunaStatGuna/receiveKeputusanBySatker";
        #endregion

        #region SvcPengelolaStatGunaDetailSk
        public static string SvcPengelolaStatGunaDetailSk_name = "detailSk_pt";
        public static string SvcPengelolaStatGunaDetailSk_address = IP + SimanAPI + "/PengelolaStatGuna/detailSk";
        #endregion

        #region SvcPengelolaStatGunaKelolaKelengkapan
        public static string SvcPengelolaStatGunaKelolaKelengkapan_name = "kelolaKelengkapan_pt";
        public static string SvcPengelolaStatGunaKelolaKelengkapan_address = IP + SimanAPI + "/PengelolaStatGuna/kelolaKelengkapan";
        #endregion


       #region Pinjam Pakai BMN
        #region SvcPinjamPakaiAsetSelect
        public static string SvcPinjamPakaiAsetSelect_name = "selectItemAset_pt";
        public static string SvcPinjamPakaiAsetSelect_address = IP + SimanAPI + "/PenggunaPinjamPakai/selectItemAset";

        #endregion

        #region SvcPinjamPakaiAsetTambah
        public static string SvcPinjamPakaiAsetTambah_name = "tambahItemAset_pt";
        public static string SvcPinjamPakaiAsetTambah_address = IP + SimanAPI + "/PenggunaPinjamPakai/tambahItemAset";

        #endregion

        #region SvcPinjamPakaiAsetUpdate
        public static string SvcPinjamPakaiAsetUpdate_name = "updateAnalisa_pt";
        public static string SvcPinjamPakaiAsetUpdate_address = IP + SimanAPI + "/PenggunaPinjamPakai/updateAnalisa";

        #endregion

        #region SvcPinjamPakaiAsetHapus
        public static string SvcPinjamPakaiAsetHapus_name = "hapusItemAset_pt1";
        public static string SvcPinjamPakaiAsetHapus_address = IP + SimanAPI + "/PenggunaPinjamPakai/hapusItemAset";

        #endregion

        #endregion//Pinjam Pakai BMN

        #region Asp BMN

        #region SvcAsetAStatGunaSelect
        public static string SvcAsetAStatGunaSelect_name = "selectItemAset_pt2";
        public static string SvcAsetAStatGunaSelect_address = IP + SimanAPI + "/PenggunaStatGunaAlihStatus/selectItemAset";
        #endregion

        #region SvcAsetAStatGunaTambah
        public static string SvcAsetAStatGunaTambah_name = "tambahItemAset_pt2";
        public static string SvcAsetAStatGunaTambah_address = IP + SimanAPI + "/PenggunaStatGunaAlihStatus/tambahItemAset";
        #endregion

        #region SvcAsetAStatGunaHapus
        public static string SvcAsetAStatGunaHapus_name = "hapusItemAset_pt3";
        public static string SvcAsetAStatGunaHapus_address = IP + SimanAPI + "/PenggunaStatGunaAlihStatus/hapusItemAset";
        #endregion

        #region SvcAsetAStatGunaUpdate
        public static string SvcAsetAStatGunaUpdate_name = "updateAnalisa_pt3";
        public static string SvcAsetAStatGunaUpdate_address = IP + SimanAPI + "/PenggunaStatGunaAlihStatus/updateAnalisa";
        #endregion

        #endregion

        #region Sewa BMN

        #region SvcAsetSwStatGunaSelect
        public static string SvcAsetSwStatGunaSelect_name = "selectItemAset_pt3";
        public static string SvcAsetSwStatGunaSelect_address = IP + SimanAPI + "/PenggunaSewaBmn/selectItemAset";
        #endregion

        #region SvcAsetSwStatGunaTambah
        public static string SvcAsetSwStatGunaTambah_name = "tambahItemAset_pt3";
        public static string SvcAsetSwStatGunaTambah_address = IP + SimanAPI + "/PenggunaSewaBmn/tambahItemAset";
        #endregion

        #region SvcAsetSwStatGunaHapus
        public static string SvcAsetSwStatGunaHapus_name = "hapusItemAset_pt4";
        public static string SvcAsetSwStatGunaHapus_address = IP + SimanAPI + "/PenggunaSewaBmn/hapusItemAset";
        #endregion

        #region SvcAsetSwStatGunaUpdateAnalisa
        public static string SvcAsetSwStatGunaUpdateAnalisa_name = "updateAnalisa_pt4";
        public static string SvcAsetSwStatGunaUpdateAnalisa_address = IP + SimanAPI + "/PenggunaSewaBmn/updateAnalisa";
        #endregion

        #endregion

        #region Tukar Menukar

        #region SvcAsetTMStatGunaSelect
        public static string SvcAsetTMStatGunaSelect_name = "selectItemAset_pt4";
        public static string SvcAsetTMStatGunaSelect_address = IP + SimanAPI + "/PenggunaTukarMenukar/selectItemAset";
        #endregion

        #region SvcAsetTMStatGunaTambah
        public static string SvcAsetTMStatGunaTambah_name = "tambahItemAset_pt4";
        public static string SvcAsetTMStatGunaTambah_address = IP + SimanAPI + "/PenggunaTukarMenukar/tambahItemAset";
        #endregion

        #region SvcAsetTMStatGunaHapus
        public static string SvcAsetTMStatGunaHapus_name = "hapusItemAset_pt5";
        public static string SvcAsetTMStatGunaHapus_address = IP + SimanAPI + "/PenggunaTukarMenukar/hapusItemAset";
        #endregion

        #region SvcAsetTMStatGunaUpdate
        public static string SvcAsetTMStatGunaUpdateAnalisa_name = "updateAnalisa_pt5";
        public static string SvcAsetTMStatGunaUpdateAnalisa_address = IP + SimanAPI + "/PenggunaTukarMenukar/updateAnalisa";
        #endregion



        #endregion

        #region Pemusnahan Bmn

        #region SvcPemusnahanAsetSelect
        public static string SvcPemusnahanAsetSelect_name = "selectItemAset_pt16";
        public static string SvcPemusnahanAsetSelect_address = IP + SimanAPI + "/PenggunaPemusnahan/selectItemAset";
        #endregion

        #region SvcPemusnahanAsetTambah
        public static string SvcPemusnahanAsetTambah_name = "tambahItemAset_pt16";
        public static string SvcPemusnahanAsetTambah_address = IP + SimanAPI + "/PenggunaPemusnahan/tambahItemAset";
        #endregion

        #region SvcPemusnahanAsetHapus
        public static string SvcPemusnahanAsetHapus_name = "hapusItemAset_pt17";
        public static string SvcPemusnahanAsetHapus_address = IP + SimanAPI + "/PenggunaPemusnahan/hapusItemAset";
        #endregion

        #region SvcPemusnahanAsetUpdateAnalisa
        public static string SvcPemusnahanAsetUpdateAnalisa_name = "upadateAnalisa_pt17";
        public static string SvcPemusnahanAsetUpdateAnalisa_address = IP + SimanAPI + "/PenggunaPemusnahan/updateAnalisa";
        #endregion

        #endregion

        #region Penghapusan Bmn Karena Sebab Lainnya

        #region SvcHapusBmnLainnyaSelect
        public static string SvcHapusBmnLainnyaSelect_name = "selectItemAset_pt6";
        public static string SvcHapusBmnLainnyaSelect_address = IP + SimanAPI + "/PenggunaHapusSebabLain/selectItemAset";

        #endregion

        #region SvcHapusBmnLainnyaTambah
        public static string SvcHapusBmnLainnyaTambah_name = "tambahItemAset_pt5";
        public static string SvcHapusBmnLainnyaTambah_address = IP + SimanAPI + "/PenggunaHapusSebabLain/tambahItemAset";

        #endregion

        #region SvcHapusBmnLainnyaUpdateAnalisa
        public static string SvcHapusBmnLainnyaUpdateAnalisa_name = "updateAnalisa_pt8";
        public static string SvcHapusBmnLainnyaUpdateAnalisa_address = IP + SimanAPI + "/PenggunaHapusSebabLain/updateAnalisa";

        #endregion

        #region SvcHapusBmnLainnyaHapus
        public static string SvcHapusBmnLainnyaHapus_name = "hapusItemAset_pt6";
        public static string SvcHapusBmnLainnyaHapus_address = IP + SimanAPI + "/PenggunaHapusSebabLain/hapusItemAset";

        #endregion
        #endregion

        #region Penghapusan BMN karena Putusan Pengadilan
        #region SvcHapusBmnPutusanHapus
        public static string SvcHapusBmnPutusanHapus_name = "hapusItemAset_pt10";
        public static string SvcHapusBmnPutusanHapus_address = IP + SimanAPI + "/PenggunaHapusPutusan/hapusItemAset";
        #endregion
        #region SvcHapusBmnPutusanSelect
        public static string SvcHapusBmnPutusanSelect_name = "selectItemAset_pt12";
        public static string SvcHapusBmnPutusanSelect_address = IP + SimanAPI + "/PenggunaHapusPutusan/selectItemAset";
        #endregion
        #region SvcHapusBmnPutusanTambah
        public static string SvcHapusBmnPutusanTambah_name = "tambahItemAset_pt9";
        public static string SvcHapusBmnPutusanTambah_address = IP + SimanAPI + "/PenggunaHapusPutusan/tambahItemAset";
        #endregion
        #region SvcHapusBmnPutusanUpdateAnalisa
        public static string SvcHapusBmnPutusanUpdateAnalisa_name = "updateAnalisa_pt9";
        public static string SvcHapusBmnPutusanUpdateAnalisa_address = IP + SimanAPI + "/PenggunaHapusPutusan/updateAnalisa";
        #endregion
        #endregion//PBKPP

        #region Penyertaan Modal Pemerintah
        #region SvcSertaModalHapus
        public static string SvcSertaModalHapus_name = "hapusItemAset_pt9";
        public static string SvcSertaModalHapus_address = IP + SimanAPI + "/PenggunaSertaModal/hapusItemAset";
        #endregion
        #region SvcSertaModalSelect
        public static string SvcSertaModalSelect_name = "selectItemAset_pt9";
        public static string SvcSertaModalSelect_address = IP + SimanAPI + "/PenggunaSertaModal/selectItemAset";
        #endregion
        #region SvcSertaModalTambah
        public static string SvcSertaModalTambah_name = "tambahItemAset_pt8";
        public static string SvcSertaModalTambah_address = IP + SimanAPI + "/PenggunaSertaModal/tambahItemAset";
        #endregion
        #region SvcSertaModalUpdateAnalisa
        public static string SvcSertaModalUpdateAnalisa_name = "updateAnalisa_pt12";
        public static string SvcSertaModalUpdateAnalisa_address = IP + SimanAPI + "/PenggunaSertaModal/updateAnalisa";
        #endregion
        #endregion//Penyertaan Modal Pemerintah

        #region Penjualan//========================================================================
        #region SvcPenjualanHapus
        public static string SvcPenjualanHapus_name = "hapusItemAset_pt13";
        public static string SvcPenjualanHapus_address = IP + SimanAPI + "/PenggunaPenjualan/hapusItemAset";
        #endregion
        #region SvcPenjualanSelect
        public static string SvcPenjualanSelect_name = "selectItemAset_pt12";
        public static string SvcPenjualanSelect_address = IP + SimanAPI + "/PenggunaPenjualan/selectItemAset";
        #endregion
        #region SvcPenjualanTambah
        public static string SvcPenjualanTambah_name = "tambahItemAset_pt12";
        public static string SvcPenjualanTambah_address = IP + SimanAPI + "/PenggunaPenjualan/tambahItemAset";
        #endregion
        #region SvcPenjualanUpdateAnalisa
        public static string SvcPenjualanUpdateAnalisa_name = "updateAnalisa_pt13";
        public static string SvcPenjualanUpdateAnalisa_address = IP + SimanAPI + "/PenggunaPenjualan/updateAnalisa";
        #endregion
        #endregion//Penjualan

        #region Kerjasama Pemanfaatan//========================================================================
        #region SvcKerjasamaPemanfaatanHapus
        public static string SvcKerjasamaPemanfaatanHapus_name = "hapusItemAset_pt14";
        public static string SvcKerjasamaPemanfaatanHapus_address = IP + SimanAPI + "/PenggunaKerjasamaManfaat/hapusItemAset";
        #endregion
        #region SvcKerjasamaPemanfaatanSelect
        public static string SvcKerjasamaPemanfaatanSelect_name = "selectItemAset_pt7";
        public static string SvcKerjasamaPemanfaatanSelect_address = IP + SimanAPI + "/PenggunaKerjasamaManfaat/selectItemAset";
        #endregion
        #region SvcKerjasamaPemanfaatanTambah
        public static string SvcKerjasamaPemanfaatanTambah_name = "tambahItemAset_pt13";
        public static string SvcKerjasamaPemanfaatanTambah_address = IP + SimanAPI + "/PenggunaKerjasamaManfaat/tambahItemAset";
        #endregion
        #region SvcKerjasamaPemanfaatanUpdateAnalisa
        public static string SvcKerjasamaPemanfaatanUpdateAnalisa_name = "updateAnalisa_pt14";
        public static string SvcKerjasamaPemanfaatanUpdateAnalisa_address = IP + SimanAPI + "/PenggunaKerjasamaManfaat/updateAnalisa";
        #endregion
        #endregion//Kerjasama Pemanfaatan


        #region BSG//========================================================================
        #region SvcBSGselect
        public static string SvcBSGSelect_name = "selectItemAset_pt10";
        public static string SvcBSGSelect_address = IP + SimanAPI + "/PenggunaBsg/selectItemAset";
        #endregion
        #endregion

        #region BGS//========================================================================
        #region SvcBGSselect
        public static string SvcBGSSelect_name = "selectItemAset_pt11";
        public static string SvcBGSSelect_address = IP + SimanAPI + "/PenggunaBgs/selectItemAset";
        #endregion

        #region SvcBGSUpdate
        public static string SvcBGSUpdateAnalisa_name = "updateAnalisa_pt16";
        public static string SvcBGSUpdateAnalisa_address = IP + SimanAPI + "/PenggunaBgs/updateAnalisa";
        #endregion

        #endregion

        #endregion

        public static string formatTanggal = "mm/dd/yyyy HH:MI:SS AM";

        #region Public Procedure
        public static void RemoveClickEvent(DevExpress.XtraBars.BarButtonItem btn)
        {
            FieldInfo itemClickInfo = (typeof(DevExpress.XtraBars.BarItem)).GetField("itemClick",
                BindingFlags.Static | BindingFlags.NonPublic);

            FieldInfo eventsInfo = (typeof(System.ComponentModel.Component)).GetField("events",
                BindingFlags.Instance | BindingFlags.NonPublic);

            object itemClick = itemClickInfo.GetValue(btn);
            //object itemClick = itemClickInfo.GetValue(null);

            System.ComponentModel.EventHandlerList events = eventsInfo.GetValue(btn) as System.ComponentModel.EventHandlerList;

            if (events == null) return;

            ItemClickEventHandler handler = events[itemClick] as ItemClickEventHandler;

            Delegate d = events[itemClick];

            if (d != null)
            {
                events.RemoveHandler(itemClick, d);
            }

        }

        public static List<string> teksGagal(char mode)
        {
            List<string> teks = new string[] { "Array", "Teks" }.ToList();
            switch (mode)
            {
                case 'C': teks.Insert(0, konfigApp.teksGagalSimpan);
                    teks.Insert(1, konfigApp.judulGagalSimpan);

                    break;
                case 'U':
                    teks.Insert(0, konfigApp.teksGagalSimpan);
                    teks.Insert(1, konfigApp.judulGagalSimpan);

                    break;
                case 'D':
                    teks.Insert(0, konfigApp.teksGagalHapus);
                    teks.Insert(1, konfigApp.judulGagalHapus);

                    break;
                case 'R':
                    teks.Insert(0, konfigApp.teksTidakKetemu);
                    teks.Insert(1, konfigApp.judulTidakKetemu);

                    break;
                default:
                    teks.Insert(0, "");
                    teks.Insert(1, "");
                    break;

            }
            return teks;
        }

        public static List<string> teksBerhasil(char mode)
        {
            List<string> teks = new string[] { "Hello", "world" }.ToList();
            switch (mode)
            {
                case 'C': teks.Insert(0, konfigApp.teksBerhasilSimpan);
                    teks.Insert(1, konfigApp.judulsukses);

                    break;
                case 'U':
                    teks.Insert(0, konfigApp.teksBerhasilSimpan);
                    teks.Insert(1, konfigApp.judulsukses);

                    break;
                case 'D':
                    teks.Insert(0, konfigApp.teksBerhasilHapus);
                    teks.Insert(1, konfigApp.judulsukses);

                    break;
                default:
                    teks.Insert(0, "");
                    teks.Insert(1, "");
                    break;

            }
            return teks;
        }

        public static float? convertToMb(float? val) 
        {
            val = val / 1024;
            val = val / 1024;
            return val;
        }
        public static string StringtoNull(string val)
        {

          if (val == "-")
          {
            val = null;
          }
          else
          {
            val = val;
          }
          return val;
        }
        public static decimal? DecimaltoNull(decimal? val)
        {

          if (val == 0)
          {
            val = null;
          }
          else
          {
            val = val;
          }
          return val;
        }

        public static DateTime? ToDate(string Teks)
        {
            DateTime? tanggal;
            
            try
            {
                tanggal = (DateTime?)DateTime.ParseExact(Teks, "dd/MM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

                Teks = null;
                tanggal = Convert.ToDateTime(Teks);
            }
            return tanggal;
        }

        public static string DateToDb(string Teks)
        {
            string tanggal;
            try
            {
                DateTime date;
                date = Convert.ToDateTime(Teks);
                tanggal = date.ToString("dd/MM/yyyy");
               // tanggal = Teks;
            }
            catch (Exception)
            {
                tanggal = Teks;
            }

            return tanggal;
        }

        public static string DateToString(DateTime? date)
        {
            string tanggal;
            try
            {

                tanggal = date.Value.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                tanggal = date.ToString();
            }
            return tanggal;
        }

        public static string DateToYear(DateTime? date)
        {
            string tanggal;
            try
            {

                tanggal = date.Value.ToString("yyyy");
            }
            catch (Exception)
            {
                tanggal = date.ToString();
            }
            return tanggal;
        }

        public static byte[] convert2byte(string file)
        {
            FileStream fs = new FileStream(file,
                                           FileMode.Open,
                                           FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return filebytes;
        }
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image convert2bytmap(byte[] filebytes)
        {
            MemoryStream ms = new MemoryStream(filebytes, 0, filebytes.Length);
            // Convert byte[] to Image
            ms.Write(filebytes, 0, filebytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public static string convert2String(string file)
        {
            FileStream fs = new FileStream(file,
                                           FileMode.Open,
                                           FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            string encodedData =
                Convert.ToBase64String(filebytes,
                                       Base64FormattingOptions.InsertLineBreaks);
            return encodedData;
        }


        public static bool IsPDFHeader(string fileName)
        {
            byte[] buffer = null;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            //buffer = br.ReadBytes((int)numBytes);
            buffer = br.ReadBytes(5);

            var enc = new ASCIIEncoding();
            var header = enc.GetString(buffer);

            //%PDF−1.0
            // If you are loading it into a long, this is (0x04034b50).
            if (buffer[0] == 0x25 && buffer[1] == 0x50
                && buffer[2] == 0x44 && buffer[3] == 0x46)
            {
                return true;
            }
            return false;

        }


        /*
        public static PdfDocument compressPdf(string filename)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string originalFile = filename;
            string compressedFile = "C://CompressAllTechniques.pdf";
            BitMiracle.Docotic.LicenseManager.AddLicenseData("65812-88GT7-RS7JG-TL9I9-Q067Q");
            PdfDocument pdf = new PdfDocument(originalFile);
              try
                {
                    // 1. Recompress images
                    foreach (PdfPage page in pdf.Pages)
                    {
                        foreach (PdfPaintedImage painted in page.GetPaintedImages())
                        {
                            PdfImage image = painted.Image;
                            // image that is used as mask or image with attached mask are 
                            // not good candidates for recompression 
                            if (image.IsMask || image.Mask != null || image.Width < 8 || image.Height < 8)
                                continue;

                            // get size of the painted image
                            int width = Math.Max(1, (int)painted.Bounds.Width);
                            int height = Math.Max(1, (int)painted.Bounds.Height);
                            if (width >= image.Width || height >= image.Height)
                            {
                                if (image.ComponentCount == 1 && image.BitsPerComponent == 1 &&
                                    image.Compression != PdfImageCompression.Group4Fax)
                                {
                                    image.RecompressWithGroup4Fax();
                                }
                                else if (image.BitsPerComponent == 8 &&
                                    image.ComponentCount >= 3 &&
                                    image.Compression != PdfImageCompression.Jpeg &&
                                    image.Compression != PdfImageCompression.Jpeg2000)
                                {
                                    image.RecompressWithJpeg2000(10);
                                    // or image.RecompressWithJpeg();
                                }
                            }
                            else
                            {
                                // NOTE: PdfImage.ResizeTo() method is not supported in version for .NET Standard 
                                if (image.Compression == PdfImageCompression.Group4Fax ||
                                    image.Compression == PdfImageCompression.Group3Fax)
                                {
                                    // Fax documents usually looks better if integer-ratio scaling is used
                                    // Fractional-ratio scaling introduces more artifacts
                                    int ratio = Math.Min(image.Width / width, image.Height / height);
                                    image.ResizeTo(image.Width / ratio, image.Height / ratio, PdfImageCompression.Group4Fax);
                                }
                                else if (image.ComponentCount >= 3 && image.BitsPerComponent == 8)
                                {
                                    image.ResizeTo(width, height, PdfImageCompression.Jpeg, 90);
                                }
                                else
                                {
                                    image.ResizeTo(width, height, PdfImageCompression.Flate, 9);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error "+ex);
                } 

                // 2 Setup save options
                pdf.SaveOptions.Compression = PdfCompression.Flate;
                pdf.SaveOptions.UseObjectStreams = true;
                pdf.SaveOptions.RemoveUnusedObjects = true;
                pdf.SaveOptions.OptimizeIndirectObjects = true;
                pdf.SaveOptions.WriteWithoutFormatting = true;

                // 3. Remove structure information
                pdf.RemoveStructureInformation();

                // 4. Flatten form fields 
                // Controls become uneditable after that
                pdf.FlattenControls();

                // 5. Clear metadata
                pdf.Metadata.Basic.Clear();
                pdf.Metadata.DublinCore.Clear();
                pdf.Metadata.MediaManagement.Clear();
                pdf.Metadata.Pdf.Clear();
                pdf.Metadata.RightsManagement.Clear();
                pdf.Metadata.Custom.Properties.Clear();

                foreach (XmpSchema schema in pdf.Metadata.Schemas)
                    schema.Properties.Clear();

                pdf.Info.Clear(false);

                // 6. Remove font duplicates
                pdf.ReplaceDuplicateFonts();

                // 7. Unembed fonts
                foreach (PdfFont font in pdf.GetFonts())
                {
                    // Only unembed popular fonts installed in the typical OS. You can extend 
                    // the list of such fonts in the "if" statement below. 
                    if (font.Name == "Arial" || font.Name == "Verdana")
                        font.Unembed();
                }

             //   pdf.Save(compressedFile);
            
            return pdf;
            //Process.Start(compressedFile);
        }
    
        */
            

        public static byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;
            //PdfDocument pdf = null;
            if (IsPDFHeader(_FileName))
            {

              //  pdf = compressPdf(_FileName);
               
                try
                {
                    // Open file for reading
                    System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    // attach filestream to binary reader
                    System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                    // get total byte length of the file
                    long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
                    // read entire file into buffer
                    _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                    // close file reader
                    _FileStream.Close();
                    _FileStream.Dispose();
                    _BinaryReader.Close();
                }
                catch (Exception _Exception)
                {
                    // Error
                    Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                }
                
            }
            return _Buffer;
        }
        #endregion
        #region ++ Pengaturan alamat IP Servis Perencanaan
        #region -- SvcRcAsetSelect
        public static string rcAsetSelectEpName = "call_pt185";
        public static string rcAsetSelectAddress = String.Format("{0}{1}/aset/select", IP, SimanAPI);
        #endregion

        #region -- SvcRcKegiatanKpb
        public static string rcKegiatanKpbCrudEpName = "call_pt66";
        public static string rcKegiatanKpbCrudAddress = String.Format("{0}{1}/dsKegiatanKpb/doCrud", IP, SimanAPI);
        public static string rcKegiatanKpbSelectEpName = "call_pt69";
        public static string rcKegiatanKpbSelectAddress = String.Format("{0}{1}/dsKegiatanKpb/doSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcOutputKpb
        public static string rcOutputKpbCrudEpName = "call_pt70";
        public static string rcOutputKpbCrudAddress = String.Format("{0}{1}/dsOutputKpb/dsCrud", IP, SimanAPI);
        public static string rcOutputKpbSelectEpName = "call_pt71";
        public static string rcOutputKpbSelectAddress = String.Format("{0}{1}/dsOutputKpb/dsSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcProgramKpb
        public static string rcProgramKpbCrudEpName = "call_pt73";
        public static string rcProgramKpbCrudAddress = String.Format("{0}{1}/dsProgramKpb/dsCrud", IP, SimanAPI);
        public static string rcProgramKpbSelectEpName = "call_pt72";
        public static string rcProgramKpbSelectAddress = String.Format("{0}{1}/dsProgramKpb/dsSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcRkbmnPemeliharaan
        public static string rcRkbmnKpbPemeliharaanCrudEpName = "call_pt75";
        public static string rcRkbmnKpbPemeliharaanCrudAddress = String.Format("{0}{1}/dsRkbmnPemeliharaanDtlKpb/doCrud", IP, SimanAPI);
        public static string rcRkbmnKpbPemeliharaanSelectEpName = "call_pt76";
        public static string rcRkbmnKpbPemeliharaanSelectAddress = String.Format("{0}{1}/dsRkbmnPemeliharaanDtlKpb/doSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcRkbmnPengadaan
        public static string rcRkbmnKpbPengadaanCrudEpName = "call_pt77";
        public static string rcRkbmnKpbPengadaanCrudAddress = String.Format("{0}{1}/dsRkbmnPengadaanDtlKpb/doCrud", IP, SimanAPI);
        public static string rcRkbmnKpbPengadaanSelectEpName = "call_pt78";
        public static string rcRkbmnKpbPengadaanSelectAddress = String.Format("{0}{1}/dsRkbmnPengadaanDtlKpb/doSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcSskelSelect
        public static string rcSskelSelectEpName = "call_pt80";
        public static string rcSskelSelectAddress = String.Format("{0}{1}/dsRSskel/doSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnKpb
        //SvcRcTiketRkbmnCreateRevisi
        public static string rcTiketRkbmnCreateRevisitEpName = "call_pt74";
        public static string rcTiketRkbmnCreateRevisiAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doCreateRevisi", IP, SimanAPI);
        //SvcRcTiketRkbmnCrud
        public static string rcTiketRkbmnCrudEpName = "call_pt79";
        public static string rcTiketRkbmnCrudAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doCrd", IP, SimanAPI);
        //SvcRcTiketRkbmnKompilasi
        public static string rcTiketRkbmnKompilasiEpName = "call_pt84";
        public static string rcTiketRkbmnKompilasiAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doCompile", IP, SimanAPI);
        //SvcRcTiketRkbmnOpenReceive
        public static string rcTiketRkbmnOpenReceiveEpName = "call_pt90";
        public static string rcTiketRkbmnOpenReceiveAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doOpenReceive", IP, SimanAPI);
        //SvcRcTiketRkbmnReceive
        public static string rcTiketRkbmnReceiveEpName = "call_pt81";
        public static string rcTiketRkbmnReceiveAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doReceive", IP, SimanAPI);
        //SvcRcTiketRkbmnReceiveRevisi
        public static string rcTiketRkbmnReceiveRevisiEpName = "call_pt152";
        public static string rcTiketRkbmnReceiveRevisiAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doReceiveRevisi", IP, SimanAPI);
        //SvcRcTiketRkbmnReceiveRevisiKl
        public static string rcTiketRkbmnReceiveRevisiKlEpName = "call_pt151";
        public static string rcTiketRkbmnReceiveRevisiKlAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doReceiveRevisiKl", IP, SimanAPI);
        //SvcRcTiketRkbmnReturnSubmit
        public static string rcTiketRkbmnReturnSubmitEpName = "call_pt83";
        public static string rcTiketRkbmnReturnSubmitAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doReturnSubmit", IP, SimanAPI);
        //SvcRcTiketRkbmnSelect
        public static string rcTiketRkbmnSelectEpName = "call_pt68";
        public static string rcTiketRkbmnSelectAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doSelect", IP, SimanAPI);
        //SvcRcTiketRkbmnSendRevisiKl
        public static string rcTiketRkbmnSendRevisiKlEpName = "call_pt153";
        public static string rcTiketRkbmnSendRevisiKlAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doSendRevisiKl", IP, SimanAPI);
        //SvcRcTiketRkbmnSubmit
        public static string rcTiketRkbmnSubmitEpName = "executecall_pt1";
        public static string rcTiketRkbmnSubmitAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doSubmit", IP, SimanAPI);
        //SvcRcTiketRkbmnSubmitRevisi
        public static string rcTiketRkbmnSubmitRevisiEpName = "call_pt150";
        public static string rcTiketRkbmnSubmitRevisiAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doSubmitRevisi", IP, SimanAPI);
        //SvcRcTiketRkbmnTelaah
        public static string rcTiketRkbmnTelaahEpName = "call_pt85";
        public static string rcTiketRkbmnTelaahAddress = String.Format("{0}{1}/doTiketRkbmnPb/doReceiveTelaahKoord", IP, SimanAPI);
        #endregion

        #region -- SvcRcSskelSelect
        //SvcRcTiketRkbmnPbDisposisi
        public static string rcRkbmnPbDisposisiEpName = "call_pt91";
        public static string rcRkbmnPbDisposisiAddress = String.Format("{0}{1}/doTiketRkbmnPb/doDisposisi", IP, SimanAPI);
        //SvcRcTiketRkbmnPbKirimTelaahAnalis
        public static string rcRkbmnPbKirimTelaahAnalisEpName = "call_pt92";
        public static string rcRkbmnPbKirimTelaahAnalisAddress = String.Format("{0}{1}/doTiketRkbmnPb/doSendTelaahAnalis", IP, SimanAPI);
        //SvcRcTiketRkbmnPbKirimTelaahKoord
        public static string rcRkbmnPbKirimTelaahKoordEpName = "call_pt93";
        public static string rcRkbmnPbKirimTelaahKoordAddress = String.Format("{0}{1}/doTiketRkbmnPb/doSendTelaahKoord", IP, SimanAPI);
        //SvcRcTiketRkbmnPbPemeliharaanCrud
        public static string rcRkbmnPbPemeliharaanCrudEpName = "call_pt94";
        public static string rcRkbmnPbPemeliharaanCrudAddress = String.Format("{0}{1}/dsRkbmnPemeliharaanDtlPb/doRud", IP, SimanAPI);
        //SvcRcTiketRkbmnPbPemeliharaanSelect
        public static string rcRkbmnPbPemeliharaanSelectEpName = "call_pt105";
        public static string rcRkbmnPbPemeliharaanSelectAddress = String.Format("{0}{1}/dsRkbmnPemeliharaanDtlPb/doSelect", IP, SimanAPI);
        //SvcRcTiketRkbmnPbPengadaanCrud
        public static string rcRkbmnPbPengadaanCrudEpName = "call_pt104";
        public static string rcRkbmnPbPengadaanCrudAddress = String.Format("{0}{1}/dsRkbmnPengadaanDtlPb/doRud", IP, SimanAPI);
        //SvcRcTiketRkbmnPbPengadaanSelect
        public static string rcRkbmnPbPengadaanSelectEpName = "call_pt103";
        public static string rcRkbmnPbPengadaanSelectAddress = String.Format("{0}{1}/dsRkbmnPengadaanDtlPb/doSelect", IP, SimanAPI);
        //SvcRcTiketRkbmnPbReceive
        public static string rcRkbmnPbReceiveEpName = "call_pt102";
        public static string rcRkbmnPbReceiveAddress = String.Format("{0}{1}/doTiketRkbmnPb/doReceive", IP, SimanAPI);
        //SvcRcTiketRkbmnPbSelect
        public static string rcRkbmnPbSelectEpName = "call_pt101";
        public static string rcRkbmnPbSelectAddress = String.Format("{0}{1}/doTiketRkbmnPb/doSelect", IP, SimanAPI);
        //SvcRcTiketRkbmnPbSubmit
        public static string rcRkbmnPbSubmitEpName = "call_pt100";
        public static string rcRkbmnPbSubmitAddress = String.Format("{0}{1}/doTiketRkbmnPb/doSubmit", IP, SimanAPI);
        //SvcRcTiketRkbmnPbTelaahCrud
        public static string rcRkbmnPbTelaahCrudEpName = "call_pt96";
        public static string rcRkbmnPbTelaahCrudAddress = String.Format("{0}{1}/dsTiketRkbmnPbTelaah/doCrud", IP, SimanAPI);
        //SvcRcTiketRkbmnPbTelaahSelect
        public static string rcRkbmnPbTelaahSelectEpName = "call_pt97";
        public static string rcRkbmnPbTelaahSelectAddress = String.Format("{0}{1}/dsTiketRkbmnPbTelaah/doSelect", IP, SimanAPI);
        //SvcRcTiketRkbmnPbTelaahTerimaKl
        public static string rcRkbmnPbTelaahTerimaKlEpName = "call_pt98";
        public static string rcRkbmnPbTelaahTerimaKlAddress = String.Format("{0}{1}/doTiketRkbmnPb/doReceiveTelaahKoord", IP, SimanAPI);
        //SvcRcTiketRkbmnPbTerimaDisposisi
        public static string rcRkbmnPbTerimaDisposisiEpName = "call_pt99";
        public static string rcRkbmnPbTerimaDisposisiAddress = String.Format("{0}{1}/doTiketRkbmnPb/doReceiveDisposisi", IP, SimanAPI);
        //SvcRcTiketRkbmnPbTerimaTelaahAnalis
        public static string rcRkbmnPbTerimaTelaahAnalisEpName = "call_pt95";
        public static string rcRkbmnPbTerimaTelaahAnalisAddress = String.Format("{0}{1}/doTiketRkbmnPb/doReveiceTelaahAnalis", IP, SimanAPI);
        #endregion

        #region -- SvcRcGetSbskBrutto
        public static string rcGetSbskBruttoEpName = "dsGetSbskBrutto_pt";
        public static string rcGetSbskBruttoAddress = String.Format("{0}{1}/dsSBSK/dsGetSbskBrutto", IP, SimanAPI);
        #endregion

        #region -- Setting Perencaan
        public static string tglSekarang = DateTime.Now.ToString("dd/MM/yyyy");
        public Array daftarTahun()
        {
            int thnIni = Convert.ToInt16(DateTime.Now.Year);
            string[] thnAnggaran = new string[3];
            thnAnggaran[0] = thnIni.ToString();
            thnAnggaran[1] = (thnIni + 1).ToString();
            thnAnggaran[2] = (thnIni + 2).ToString();
            return thnAnggaran;
        }
        #endregion

        #region -- SvcRcProgramSelect
        public static string SvcRcProgramSelect_name = "doProgramSelect_pt";
        public static string SvcRcProgramSelect_address = String.Format("{0}{1}/dsPerencanaan/doProgramSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcKegiatanSelect
        public static string SvcRcKegiatanSelect_name = "doKegiatanSelect_pt";
        public static string SvcRcKegiatanSelect_address = String.Format("{0}{1}/dsPerencanaan/doKegiatanSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcBasSelect
        public static string SvcRcBasSelect_name = "doBasSelect_pt";
        public static string SvcRcBasSelect_address = String.Format("{0}{1}/dsPerencanaan/doBasSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcKomposisiPegawaiSelect
        public static string SvcRcKomposisiPegawaiSelect_name = "selectKomPeg_pt";
        public static string SvcRcKomposisiPegawaiSelect_address = String.Format("{0}{1}/dsRkbmnKomPeg/selectKomPeg", IP, SimanAPI);
        #endregion

        #region -- SvcRcKomposisiPegawaiCrud
        public static string SvcRcKomposisiPegawaiCrud_name = "crudKomPeg_pt";
        public static string SvcRcKomposisiPegawaiCrud_address = String.Format("{0}{1}/dsRkbmnKomPeg/crudKomPeg", IP, SimanAPI);
        #endregion

        #region -- SvcRcKomposisiPegawaiSelect
        public static string rcKomposisiPegawaiSelectEpName = "selectKomPeg_pt";
        public static string rcKomposisiPegawaiSelectAddress = String.Format("{0}{1}/dsRkbmnKomPeg/selectKomPeg", IP, SimanAPI);
        #endregion

        #region -- SvcRcKomposisiPegawaiCrud
        public static string rcKomposisiPegawaiCrudEpName = "crudKomPeg_pt";
        public static string rcKomposisiPegawaiCrudAddress = String.Format("{0}{1}/dsRkbmnKomPeg/crudKomPeg", IP, SimanAPI);
        #endregion
        #region -- SvcRcPhotoAset
        public static string rcPhotoAsetEpName = "call_pt281";
        public static string rcPhotoAsetAddress = String.Format("{0}{1}/aset/photoSelect", IP, SimanAPI);
        #endregion

        #region -- SvcSysWarningSatker
        public static string sysWarningSatkerEpName = "satker_pt";
        public static string sysWarningSatkerAddress = String.Format("{0}{1}/SysWarning/satker", IP, SimanAPI);
        #endregion

        #region -- SvcSysWarningKl
        public static string sysWarningKlEpName = "kl_pt";
        public static string sysWarningKlAddress = String.Format("{0}{1}/SysWarning/kl", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnPbDokCrud
        public static string rcTiketRkbmnPbDokCrudEpName = "doPbDokCrud_pt";
        public static string rcTiketRkbmnPbDokCrudAddress = String.Format("{0}{1}/doTiketRkbmnPb/doPbDokCrud", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnPbDokSelect
        public static string rcTiketRkbmnPbDokSelectEpName = "doPbDokSelect_pt";
        public static string rcTiketRkbmnPbDokSelectAddress = String.Format("{0}{1}/doTiketRkbmnPb/doPbDokSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnPbSkmUpdate
        public static string rcTiketRkbmnPbSkmUpdateEpName = "doPbSkmUpdate_pt";
        public static string rcTiketRkbmnPbSkmUpdateAddress = String.Format("{0}{1}/doTiketRkbmnPb/doPbSkmUpdate", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnPbSkmDetail
        public static string rcTiketRkbmnPbSkmDetailEpName = "doPbSkmDetail_pt";
        public static string rcTiketRkbmnPbSkmDetailAddress = String.Format("{0}{1}/doTiketRkbmnPb/doPbSkmDetail", IP, SimanAPI);
        #endregion

        #region -- SvcRcRkbmnPbPengadaanAnalisa
        public static string rcRkbmnPbPengadaanAnalisaEpName = "analisa_pt";
        public static string rcRkbmnPbPengadaanAnalisaAddress = String.Format("{0}{1}/dsRkbmnPengadaanDtlPb/analisa", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnKpbDokCrud
        public static string rcTiketRkbmnKpbDokCrudEpName = "doKpbDokCrud_pt";
        public static string rcTiketRkbmnKpbDokCrudAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doKpbDokCrud", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnKpbDokSelect
        public static string rcTiketRkbmnKpbDokSelectEpName = "doKpbDokSelect_pt";
        public static string rcTiketRkbmnKpbDokSelectAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doKpbDokSelect", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnKpbDokGFile
        public static string rcTiketRkbmnKpbDokGFileEpName = "doKpbDokGFile_pt";
        public static string rcTiketRkbmnKpbDokGFileAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doKpbDokGFile", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnPbDokGFile
        public static string rcTiketRkbmnPbDokGFileEpName = "doPbDokGFile_pt";
        public static string rcTiketRkbmnPbDokGFileAddress = String.Format("{0}{1}/doTiketRkbmnPb/doPbDokGFile", IP, SimanAPI);
        #endregion

        #region -- SvcRcTiketRkbmnPbSkmGFile
        public static string rcTiketRkbmnPbSkmGFileEpName = "doPbSkmDetailGFile_pt";
        public static string rcTiketRkbmnPbSkmGFileAddress = String.Format("{0}{1}/doTiketRkbmnPb/doPbSkmDetailGFile", IP, SimanAPI);
        #endregion

        #region -- SvcWewenangKelolaCrud
        public static string wewenangKelolaCrudEpName = "dsWewenangKelolaCrud_pt";
        public static string wewenangKelolaCrudAddress = String.Format("{0}{1}/dsWewenangKelola/dsWewenangKelolaCrud", IP, SimanAPI);
        #endregion

        #region -- SvcWewenangKelolaSelect
        public static string wewenangKelolaSelectEpName = "dsWewenangKelolaSelect_pt";
        public static string wewenangKelolaSelectAddress = String.Format("{0}{1}/dsWewenangKelola/dsWewenangKelolaSelect", IP, SimanAPI);
        #endregion
		
		#region -- SvcRcTiketRkbmnKpbByPbSelect
        public static string rcTiketRkbmnKpbByPbSelectEpName = "select_pt";
        public static string rcTiketRkbmnKpbByPbSelectAddress = String.Format("{0}{1}/dsRkbmnKpbByPb/select", IP, SimanAPI);
        #endregion
		
		#region -- SvcRcTiketRkbmnUpdateSurat
        public static string rcTiketRkbmnUpdateSuratEpName = "doUpdateSurat_pt";
        public static string rcTiketRkbmnUpdateSuratAddress = String.Format("{0}{1}/dsTiketRkbmnKpb/doUpdateSurat", IP, SimanAPI);
        #endregion
		
		#region -- SvcRcAsetPerencanaanSelect
        public static string rcAsetPerencanaanSelectEpName = "doAsetPerencanaan_pt";
        public static string rcAsetPerencanaanSelectAddress = String.Format("{0}{1}/dsPerencanaan/doAsetPerencanaan", IP, SimanAPI);
        #endregion
		
		#region -- SvcRcDaftarPenandaTangan
        public static string rcDaftarPenandaTanganName = "doSatkerByNip_pt";
        public static string rcDaftarPenandaTanganAddress = String.Format("{0}{1}/dsPerencanaan/doSatkerByNip", IP, SimanAPI);
        #endregion
		
        #endregion
        #endregion


        public static TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        #region default date
        public static DateTime? setDefaultDate(DateTime? tgl)
        {

            if (DateToString(tgl) == "11/11/2000" || DateToString(tgl) == "11/11/2011" || DateToString(tgl) == "11/11/1111" || DateToString(tgl) == "11/11/1000" || DateToString(tgl) == "1/1/0001" || DateToString(tgl) == "01/01/0001" || DateToString(tgl) == "1000/11/11")
            { tgl = null; }
            else { return tgl; }
            return tgl;
                
        }
        #endregion

        #region FormatDate

        public static DateTime? toDateTime(object input)
        {
            DateTime? output;
            try
            {
                if ((input != null) && (!input.Equals(DBNull.Value)))
                {
                    string str_date = String.Format("{0:yyyy-MM-dd}", input);
                    if (str_date == "2011-11-11")
                    {
                        output = null;
                    }
                    else
                    {
                        if (str_date.Length > 8)
                        {
                            str_date = str_date.Substring(0, 8);
                        }
                        if (str_date != "0/0/0000")
                        {
                            output = Convert.ToDateTime(input);
                        }
                        else
                        {
                            output = null;
                        }
                    }


                }
                else
                {
                    output = null;
                }

            }
            catch (Exception e)
            {
                output = null;
                Console.WriteLine(e.Message);
            }
            return output;
        }
        public static String formatDate(object input, string format = "MM/dd/yyyy")
        {
            string output;
            DateTime? toDateTime;
            try
            {
                if ((input != null) && (!input.Equals(DBNull.Value)))
                {
                    toDateTime = konfigApp.toDateTime(input);
                    if (toDateTime != null)
                    {
                        output = toDateTime.Value.ToString(format);
                    }
                    else
                    {
                        output = "";
                    }
                }
                else
                {
                    output = "";
                }
            }
            catch (Exception e)
            {
                output = "";
                Console.WriteLine(e.Message);
            }
            return output;
        }
        public static TimeSpan toTimeSpan(object input)
        {
            TimeSpan output;
            try
            {
                if ((input != null) && (!input.Equals(DBNull.Value)))
                {
                    TimeSpan.TryParse(input.ToString(), out output);
                }
                else
                {
                    output = new TimeSpan(00, 00, 00);
                }

            }
            catch (Exception e)
            {
                output = output = new TimeSpan(00, 00, 00);
                Console.WriteLine(e.Message);
            }
            return output;
        }

        #endregion FormatDate

        public static void setTahunAnggaran()
        {
            try
            {


                SvcThnAnggaranCrud.dsTThnAngaranCrud_pttClient svcthnanggarancrud = new SvcThnAnggaranCrud.dsTThnAngaranCrud_pttClient();
                SvcThnAnggaranCrud.InputParameters inputData = new SvcThnAnggaranCrud.InputParameters();

                inputData.P_SELECT = "R";
                svcthnanggarancrud = new SvcThnAnggaranCrud.dsTThnAngaranCrud_pttClient();
                SvcThnAnggaranCrud.OutputParameters outputThnAnggaran = svcthnanggarancrud.execute(inputData);
                konfigApp.tahunAnggaran = outputThnAnggaran.PO_THN_ANGGARAN;
            }
            catch 
            {

            }
        }
		public static string setPeriode(string _periode)
        {
            string periode;

            switch (_periode)
            {
                case "T" :
                    periode = "Tahunan";
                    break;
                case "B" :
                    periode = "Bulanan";
                    break;
                case "M":
                    periode = "Mingguan";
                    break;
                case "H" :
                    periode = "Harian";
                    break;
                case "J" :
                    periode = "Jam";
                    break;
                default: periode = "";
                    break;


            }

            return periode;
        }

        #region Cek nilai Minus
        public static bool cekMinus(Decimal? nilai)
        {
            bool status = false;
            Match match = Regex.Match(nilai.ToString(), @"-\d+");
            if (match.Success)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        #endregion cek nilai minus

        public static string wherePerekamanSKnonSatker()
        {
            string result = "KD_PELAYANAN = '" + kdPelayanan + "' AND (ID_USER = "+idUser+" OR ";
            if (idGroup == 3)
            {
                result += "ID_KORWIL = " + idKorwil + " )";
            }
            else if (idGroup == 11)
            {
                result += "ID_ESELON1 = " + idEselon1 + " )";
            }
            else if (idGroup == 4 || idGroup == 23)
            {
                result += "ID_KL =  " + idKl + " )";
            }

            return result;
        }

        public static void getDetailAset(string id_aset)
        {
            try
            {
                var directory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string arguments = konfigApp.args + "#" + id_aset + ";" + konfigApp.idGroup+";detail";
                var newProcess = new ProcessStartInfo(string.Format(@"{0}/DetailAset.exe",directory));

                if (!string.IsNullOrEmpty(arguments))
                    newProcess.Arguments = arguments;
                newProcess.CreateNoWindow = false;
                newProcess.ErrorDialog = true;
                newProcess.UseShellExecute = true;
                using (var proc = new Process())
                {
                    proc.StartInfo = newProcess;
                    proc.Start();
                    //Dialog.info(proc.StandardOutput.ReadToEnd());
                    //Console.WriteLine(proc.StandardOutput.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }

    
    class IsiDropDown
    {

        public IsiDropDown(string kd, string nama)
        {
            this.KD_BMN = kd;
            this.Nama_BMN = nama;
        }

        public IsiDropDown(DevExpress.XtraEditors.ComboBoxEdit cbe, string kd, string nama)
        {
            this.KD_BMN = kd;
            this.Nama_BMN = nama;
            cbe.Properties.Items.Add(Nama_BMN);
        }

        public string KD_BMN { get; set; }
        public string Nama_BMN { get; set; }

        public void addToCombo(DevExpress.XtraEditors.ComboBoxEdit cbe, string _kd, string _nama)
        {
            this.KD_BMN = _kd;
            this.Nama_BMN = _kd;
            cbe.Properties.Items.Add(_nama);
        }



    }

}


public class datafoto
{
    private string filename;
    private string nama_foto;
    private string ket_photo;
    private int index;
    private decimal? id_foto;
    private decimal? id_aset;
    private byte[] foto;
    public string KET_PHOTO
    {
        get { return ket_photo; }
        set { ket_photo = value; }
    }
    public string NM_PHOTO
    {
        get { return nama_foto; }
        set { nama_foto = value; }
    }
    public string Filename
    {
        get { return filename; }
        set { filename = value; }
    }
    public int Index
    {
        get { return index; }
        set { index = value; }
    }
    public decimal? ID_PHOTO
    {
        get { return id_foto; }
        set { id_foto = value; }
    }
    public decimal? ID_ASET
    {
        get { return id_aset; }
        set { id_aset = value; }
    }
    public byte[] PHOTO
    {
        get
        {
            return this.foto;
        }
        set
        {
            this.foto = value;
        }
    }
};

public class AutoClosingMessageBox
{
    System.Threading.Timer _timeoutTimer;
    string _caption;
    AutoClosingMessageBox(string text, string caption, int timeout)
    {
        _caption = caption;
        _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
            null, timeout, System.Threading.Timeout.Infinite);
        MessageBox.Show(text, caption);
    }
    public static void Show(string text, string caption, int timeout)
    {
        new AutoClosingMessageBox(text, caption, timeout);
    }
    void OnTimerElapsed(object state)
    {
        IntPtr mbWnd = FindWindow(null, _caption);
        if (mbWnd != IntPtr.Zero)
            SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        _timeoutTimer.Dispose();
    }
    const int WM_CLOSE = 0x0010;
    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
};

class Test
{
    public decimal? _a { get; set; }
    public decimal? _b { get; set; }

};
