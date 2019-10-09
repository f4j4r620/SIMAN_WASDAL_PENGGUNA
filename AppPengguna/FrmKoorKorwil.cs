using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Threading;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars.Helpers;
using DevExpress.LookAndFeel;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using AppPengguna.KKW.WL.PEMINDAHTANGANAN;
using AppPengguna.KKW.WL.PENGHAPUSAN;
using AppPengguna.KKW.PENERTIBAN;
using AppPengguna.PU;
using System.Data.SqlClient;
using AppPengguna.KKW.WL;
using AppPengguna.KKW.RSK;
using System.Data;
using System.IO;
using AppPengguna.KSK.WL;
using System.Diagnostics;


namespace AppPengguna
{
    public delegate void NonAktifkanFormKorwil(string str);
    public delegate void AktifkanFormKorwil(string str);
    public delegate void ProgBarKKW(BarItemVisibility str);
    
    
    
    public partial class FrmKoorKorwil : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int lebarNavigasi = 0;

        //public SvcAssetTanahSelect.BPSIMANSROW_M_KTNH assetTanahPilihan;
        //public SvcAssetTanahSelect.call_pttClient ambilAssetTanah = null;
        //public SvcAssetTanahSelect.OutputParameters dataOutAssetTanah = null;
        //public SvcAssetTanahCrud.call_pttClient svcAssetTanahCrud = null;
        //public SvcAssetTanahCrud.OutputParameters dataOutAssetTanahCrud = null;
        //private SvcTnhDokSelect.call_pttClient fetchData;
        //private SvcTnhDokSelect.InputParameters parInp;
        //private SvcTnhDokSelect.OutputParameters outData;
        //public SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK selectedData;
        //private SvcTnhLokSelect.call_pttClient fetchDataTanahLok;
        //private SvcTnhLokSelect.InputParameters parInpTanahLok;
        //private SvcTnhLokSelect.OutputParameters outDataTanahLok;
        //public SvcTnhLokSelect.BPSIMANSROW_M_KTNH_LOKASI selectedDataTanahLok;
        //private SvcTnhBangunanSelect.call_pttClient fetchDataTnhBangunan;
        //private SvcTnhBangunanSelect.InputParameters parInpTnhBangunan;
        //private SvcTnhBangunanSelect.OutputParameters outDataTnhBangunan;
        //public SvcTnhBangunanSelect.BPSIMANSROW_M_KTNH_BANGUNAN selectedDataTnhBangunan;
        //private SvcTnhFasPenunjangSelect.call_pttClient fetchDataTnhFas;
        //private SvcTnhFasPenunjangSelect.InputParameters parInTnhFasp;
        //private SvcTnhFasPenunjangSelect.OutputParameters outDataTnhFas;
        //public SvcTnhFasPenunjangSelect.BPSIMANSROW_M_KTNH_FAS_PENUNJANG selectedDataTnhFas;
        //private SvcTnhRwyPenilaianSelect.call_pttClient fetchDataRPenilaian;
        //private SvcTnhRwyPenilaianSelect.InputParameters parInpRPenilaian;
        //private SvcTnhRwyPenilaianSelect.OutputParameters outDataRPenilaian;
        //private SvcTnhRwyPenilaianSelect.BPSIMANSROW_M_KTNH_RWYT_NILAI selectedDataRPenilaian;
        //private SvcTnhNjopSelect.call_pttClient fetchDataNJOP;
        //private SvcTnhNjopSelect.InputParameters parInpNJOP;
        //private SvcTnhNjopSelect.OutputParameters outDataNJOP;
        //public SvcTnhNjopSelect.BPSIMANSROW_M_KTNH_NJOP selectedDataNJOP;
        //private SvcTnhRwyPenggunaSelect.call_pttClient fetchDataRwytPengguna;
        //private SvcTnhRwyPenggunaSelect.InputParameters parInpRwytPengguna;
        //private SvcTnhRwyPenggunaSelect.OutputParameters outDataRwytPengguna;
        //public SvcTnhRwyPenggunaSelect.BPSIMANSROW_M_KTNH_RWYT_PENGGUNA selectedDataRwytPengguna;
        //private SvcTnhRwyMutasiSelect.call_pttClient fetchDataRwyMutasi;
        //private SvcTnhRwyMutasiSelect.InputParameters parInpRwyMutasi;
        //private SvcTnhRwyMutasiSelect.OutputParameters outDataRwyMutasi;
        //public SvcTnhRwyMutasiSelect.BPSIMANSROW_M_KTNH_RWYT_MUTASI selectedDataRwyMutasi;
        //private SvcTnhRwyPeliharaSelect.call_pttClient fetchDataRwytPelihara;
        //private SvcTnhRwyPeliharaSelect.InputParameters parInpRwytPelihara;
        //private SvcTnhRwyPeliharaSelect.OutputParameters outDataRwytPelihara;
        //private SvcTnhRwyPeliharaSelect.BPSIMANSROW_M_KTNH_RWYT_PELIHARA selectedDataRwytPelihara;

        private SvcWasdalPSPBMNSelect.execute_pttClient svcWasdalPspSelect = null;
        private SvcWasdalPSPBMNSelect.OutputParameters outWasdalPspSelect = null;

        SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP wasdalSelect;
        SvcWasdalPSPBMNSelect.execute_pttClient ambilWasdal;
        SvcWasdalPSPBMNSelect.OutputParameters dataOutWasdal;

        private SvcLapPemindahTanganan.execute_pttClient svcLapPemindahtanganan = null;
        private SvcLapPemindahTanganan.OutputParameters outLapPemindahtanganan = null;

        private SvcWasdalHapusLapWasdalMSK.lapWasdalMSK_pttClient svcLapPenghapusan = null;
        private SvcWasdalHapusLapWasdalMSK.OutputParameters outLapPenghapusan = null;

        private SvcLapWasdalManfaat.execute_pttClient svcLapWasdalManfaatSelect = null;
        private SvcLapWasdalManfaat.OutputParameters outLapWasdalManfaatSelect = null;


        Thread myThread = null;
        private char modeCrud = 'A';
        private string teksDialog = "";
        private bool modeCari = false;
        GridControl gridControl;
        ArrayList dataGrid;
        private decimal dataAwal = 1;
        private decimal dataAkhir = 20;
        private decimal currentMaks = 20;
        private decimal currentMin = 1;
        public bool dataInisial = true;
        private bool adaData = false;
        private bool masihAdaData = true;
        private int? nilaiProgress = 0;
        private string strCari = "";
        private string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private string bySatker = " (ID_SATKER = " + konfigApp.idSatker + " OR ID_SATKER_PARENT = " + konfigApp.idSatker + ") ";
        private string kdPelayanan = "";
        private string kdMenu = "";
        private decimal? tahunAnggaran;
       // xrLapWasdalBMN xrlapwasdal;
        private string subMenuLapWasdal = "Laporan Wasdal";
        private string subMenuMonWasdal = "Monitoring Wasdal";

        public FrmKoorKorwil()
        {
            InitializeComponent();
            lebarNavigasi = sccSatker.SplitterPosition;
            bsiMenu.Caption = "";
            Icon = Properties.Resources.logo_2016;
        }

        private void inisialisasiForm()
        {
            this.Text = "Sistem Informasi Manajemen Aset Negara | Koordinator KorWil";
            bsiUser.Caption = "User: " + konfigApp.namaUser;
            bsiGroup.Caption = "Group: " + konfigApp.namaGroup;
            bsiMenu.Caption = "Menu: " + konfigApp.strMenu + ">" + konfigApp.strSubMenu;
            bsiUser.Caption = "Login sbg: " + konfigApp.namaUser + " ";
            bsiNmSatker.Caption = "Korwil : [" + konfigApp.kodeKorwil + "]" + konfigApp.namaKorwil + " ";
            konfigApp.kdMenu = "011";

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Text += " | v" + fvi.FileVersion;

            rpMonitoringWasdal.Visible = false;
            rpPspBmn.Visible = false;
            rpPspBmnDpl.Visible = false;
            rpRkmTindakLanjut.Visible = false;
            rpPenertibanBMN.Visible = false;
            rpPnbp.Visible = false;
            rpRekamSK.Visible = false;
            panelKoorSatker.Controls.Clear();
        }

        public void setPanel(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            panelKoorSatker.Controls.Clear();
            panelKoorSatker.Controls.Add(uc);
        }

        #region Pengaturan Thread untuk kesetabilan Aplikasi
        public void progBar(BarItemVisibility str)
        {
            if (this.InvokeRequired)
            {
                ProgBarKKW d = new ProgBarKKW(progBar);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                this.beMarqueeBar.Visibility = str;
            }
            if (str == DevExpress.XtraBars.BarItemVisibility.Never)
            {
                try
                {
                    this.Enabled = true;
                }
                catch (Exception)
                {

                }
            }
        }

        public void ShowProgresBar()
        {
            this.progBar(BarItemVisibility.Always);
        }


        public delegate void AktifkanForm(string str);

        public void aktifkanForm(string str)
        {
            this.Enabled = true;
            try
            {
                this.Cursor = Cursors.Default;
            }
            catch (Exception)
            {

            }
        }
        public void fToggleProgressBar(string kondisi)
        {
            if (kondisi == "start")
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
            }
            else
            {
                this.Invoke(new ProgBarKKW(this.progBar), BarItemVisibility.Never);
            }
        }
        #endregion

        private void FrmKoorKorwil1_Load(object sender, EventArgs e)
        {
            this.inisialisasiForm();
            //this.tampilkanDashboard();
            this.Invoke(new ProgBarKKW(this.progBar), BarItemVisibility.Never);
            UserLookAndFeel.Default.SetSkinStyle(this.GetSkin().Name.ToString());
            this.nbiPersediaan.Enabled = false;
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            SkinHelper.InitSkinGallery(ribbonGallerySkins, true);

            if (konfigApp.initRef == "Y")
            {
                Reference Ref = new Reference();
                Ref.Size = new System.Drawing.Size(0, 0);
                Ref.Opacity = 0;
                Ref.Show();
                Ref.Hide();
                //Ref.getinitKondisi();
                konfigApp.initRef = "N";
            }
        }

        private void nbcNavigasi_NavPaneStateChanged(object sender, EventArgs e)
        {
            if (nbcNavigasi.OptionsNavPane.NavPaneState.ToString() == "Expanded")
            {
                sccSatker.SplitterPosition = lebarNavigasi;
                sccSatker.IsSplitterFixed = false;
            }
            else
            {
                sccSatker.SplitterPosition = 55;
                sccSatker.IsSplitterFixed = true;
            }
        }

        private void bbiKeluarAplikasi_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        public void nonAktifkanForm(string str)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
        }

        #region Report

        #region  Show Data Wasdal

        public void getInitWasdalBMN()
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNSelect.InputParameters inPar = new SvcWasdalPSPBMNSelect.InputParameters();
                inPar.P_MAX = konfigApp.currentMaks;
                inPar.P_MAXSpecified = true;
                inPar.P_MIN = konfigApp.currentMin;
                inPar.P_MINSpecified = true;
                inPar.P_COL = "IS_TB";
                inPar.P_SORT = "DESC";
                ambilWasdal = new SvcWasdalPSPBMNSelect.execute_pttClient();
                ambilWasdal.Beginexecute(inPar, new AsyncCallback(getDataWasdalBMN), null);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataWasdalBMN(IAsyncResult result)
        {
            try
            {
                dataOutWasdal = ambilWasdal.Endexecute(result);
                ambilWasdal.Close();
                this.Invoke(new SimpanDataWasdalBMN(this.simpanDataWasdalBMN), dataOutWasdal);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void SimpanDataWasdalBMN(SvcWasdalPSPBMNSelect.OutputParameters dataOut);

        private void simpanDataWasdalBMN(SvcWasdalPSPBMNSelect.OutputParameters dataOut)
        {
            //xrlapwasdal.bsLapWasdalBMN.DataSource = dataOutWasdal.SF_ROW_WASDAL_PSP;
            int jmlData = dataOutWasdal.SF_ROW_WASDAL_PSP.Count();
            for (int i = 0; i < jmlData; i++)
            {
                if (dataOutWasdal.SF_ROW_WASDAL_PSP[i].IS_TB == "Y")
                {
                    dataOutWasdal.SF_ROW_WASDAL_PSP[i].IS_TB = "I. Tanah dan / atau bangunan";

                }
                else//if (dataOutWasdal.SF_ROW_WASDAL_PSP[i].IS_TB == "N")
                {
                    dataOutWasdal.SF_ROW_WASDAL_PSP[i].IS_TB = "II. Selain Tanah dan / atau bangunan 5)";
                }
                if (dataOutWasdal.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL == "Dipergunakan sesuai TUSI")
                {
                    dataOutWasdal.SF_ROW_WASDAL_PSP[i].NIP_PENANDATANGAN = "Ya"; //dipake untuk TUSI yang "YA"
                    dataOutWasdal.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL = "";
                }
                else
                {
                    dataOutWasdal.SF_ROW_WASDAL_PSP[i].NIP_PENANDATANGAN = ""; //dipake untuk TUSI yang "YA"
                    dataOutWasdal.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL = "Tidak";
                }
            }
           // xrlapwasdal.bsLapWasdalBMN.DataSource = dataOutWasdal.SF_ROW_WASDAL_PSP;

            //xrlapwasdal.bsLapWasdalBMN.DataSource = dataOut.SF_ROW_WASDAL_PSP;
            Parameter param = new Parameter();
            param.Name = "tahunAnggaran";
            param.Type = typeof(System.String);
            param.Value = tahunAnggaran;
            param.Description = "";
            param.Visible = false;
            //AppPengguna.KKW.WL.PSPBMN.LP.xrLapWasdalBMN xrlapwasdalbmn = new AppPengguna.KKW.WL.PSPBMN.LP.xrLapWasdalBMN();

           // xrlapwasdal.Parameters.Add(param);
            //xrlapwasdal.ShowRibbonPreviewDialog();
        }

        #endregion

        #endregion

        // Komen dulu yg laporan wasdal, monitoring wasdal dan monitoring tindak lanjut soalnya folder KKW msh suka ke hidden
      
        #region MONITORING TINDAK LANJUT

        #region Button & Label
        private string menuMonWasdal = "Monitoring Pengawasan dan Pengendalian";
        private string menuLapWasdal = "Laporan Pengawasan dan Pengendalian";
        private string menuRekamTlWasdal = "Perekaman Tindak Lanjut Pengawasan dan Pengendalian";
        private string menuRekamPnbpWasdal = "Perekaman PNBP Pengawasan dan Pengendalian";
        private string subMenuMonPSPBMN = "Penetapan Status Penggunaan BMN";
        private string subMenuMonPSPBMNLAIN = "Penetapan Status Penggunaan BMN yang dioperasikan Pihak Lain";
        private string subMenuMonASPBMN = "Alih Status Penggunaan BMN";
        private string subMenuMonPBMNS = "Penggunaan Sementara BMN";
        private string subMenuMonSWBMN = "Sewa BMN";
        private string subMenuMonPPBMN = "Pakai Pinjam BMN";
        private string subMenuMonKPBMN = "Kerjasama Pemanfaatan BMN";
        private string subMenuMonBSG = "KSPI";
        private string subMenuMonBGS = "KSPI";
        private string subMenuMonPenjualan = "Penjualan BMN";
        private string subMenuMonHibah = "Hibah BMN";
        private string subMenuMonTukar = "Tukar BMN";
        private string subMenuMonMusnah = "Pemusnahan BMN";
        private string subMenuMonPengadilan = "Penghapusan BMN karena Putusan Pengadilan";
        private string subMenuMonHapusLain = "Penghapusan BMN karena Sebab-sebab Lain";
        private string subMenuMonModal = "Penyertaan Modal Pemerintah";
        private string subMenuMonGuna = "Monitoring Penggunaan BMN";
        private string subMenuMonManfaat = "Monitoring Pemanfaatan BMN";
        private string subMenuMonPindahTgn = "Monitoring Pemindahtanganan BMN";
        private string subMenuMonHapus = "Monitoring Penghapusan BMN";

        private void resetEventButtonMonWasdal()
        {
            konfigApp.RemoveClickEvent(this.bbiMWasdalTutup);
            konfigApp.RemoveClickEvent(this.bbiMWasdalPrint);
            konfigApp.RemoveClickEvent(this.bbiMWasdalRefresh);
        }

        private void setGridMonWasdal(string menu, string submenu)
        {


            konfigApp.strMenu = menu;
            konfigApp.strSubMenu = submenu;
            this.inisialisasiForm();
            this.rpMonitoringWasdal.Text = konfigApp.strSubMenu;
            this.rpgMonitoringWasdal.Text = konfigApp.strSubMenu;
            this.rpMonitoringWasdal.Visible = true;
            this.ribbon.SelectedPage = this.rpMonitoringWasdal;
            this.modeLoadData = "normal";
            this.strCari = "";
            this.dataInisial = true;
            this.adaData = false;
            this.masihAdaData = true;
        }

        public void setEnabledButtonMWasdal(string item, bool enabled)
        {
            BarButtonItem btnMonWl = null;
            switch (item)
            {
                case "Refresh":
                    btnMonWl = bbiMWasdalRefresh;
                    break;
                case "More":
                    btnMonWl = bbiMWasdalMore;
                    break;
                case "Print":
                    btnMonWl = bbiMWasdalPrint;
                    break;
                case "Tutup":
                    btnMonWl = bbiMWasdalTutup;
                    break;
            }
            btnMonWl.Enabled = enabled;
        }

        public void setTombolMWasdal(bool status)
        {
            this.bbiMWasdalTutup.Enabled = false;
        }
        #endregion

        KKW.MWD.PSPBMN.ucPSPBMN MonPSPBMN;
        //KKW.MWD.PSPBMN.ucMonLAIN MonLain;

        #region PSP BMN
        private void setEventButtonMWasdalPSPBMN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlPSPBMNRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlPSPBMNMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlPSPBMNPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlPSPBMNTutup);
        }

        public void initGridMonWlPSPBMN()
        {
            inisialisasiForm();

            this.setEventButtonMWasdalPSPBMN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonPSPBMN);

            this.MonPSPBMN = new KKW.MWD.PSPBMN.ucPSPBMN(this);
            MonPSPBMN.nameTab1 = "BMN Sudah PSP";
            MonPSPBMN.nameTab2 = "BMN Belum PSP";
            MonPSPBMN.strKdPelayanan = "AND (KD_PELAYANAN = '02')";
            this.setPanel(this.MonPSPBMN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLPSPBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlPSPBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        private void mWlPSPBMNRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlPSPBMN();
        }

        private void mWlPSPBMNMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MonPSPBMN.xtcPSP.SelectedTabPageIndex == 0)
            {

                #region PSP SUDAH JADI

                if (MonPSPBMN.xtbDetail1.SelectedTabPageIndex == 0)
                {
                    if (MonPSPBMN.tindakLanjut1.modeLoadData == "normal" || MonPSPBMN.tindakLanjut1.modeLoadData == "cari")
                        MonPSPBMN.tindakLanjut1.dataInisial = false;

                    MonPSPBMN.tindakLanjut1.getTindakLanjut();
                }
                #endregion
            }
            else
            {
                #region PSP BELUM JADI
                if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 0)
                {
                    if (MonPSPBMN.tanah2.modeLoadData == "normal" || MonPSPBMN.tanah2.modeLoadData == "cari")
                        MonPSPBMN.tanah2.dataInisial = false;

                    MonPSPBMN.tanah2.getTanah();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 1)
                {
                    if (MonPSPBMN.bangunan2.modeLoadData == "normal" || MonPSPBMN.bangunan2.modeLoadData == "cari")
                        MonPSPBMN.bangunan2.dataInisial = false;

                    MonPSPBMN.bangunan2.getBangunan();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 2)
                {
                    if (MonPSPBMN.rmhNgr2.modeLoadData == "normal" || MonPSPBMN.rmhNgr2.modeLoadData == "cari")
                        MonPSPBMN.rmhNgr2.dataInisial = false;

                    MonPSPBMN.rmhNgr2.getRmhNgr();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 3)
                {
                    if (MonPSPBMN.angkutan2.modeLoadData == "normal" || MonPSPBMN.angkutan2.modeLoadData == "cari")
                        MonPSPBMN.angkutan2.dataInisial = false;

                    MonPSPBMN.angkutan2.getAngkutan();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 4)
                {
                    if (MonPSPBMN.bangunanAir2.modeLoadData == "normal" || MonPSPBMN.bangunanAir2.modeLoadData == "cari")
                        MonPSPBMN.bangunanAir2.dataInisial = false;

                    MonPSPBMN.bangunanAir2.getBangunanAir();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 5)
                {
                    if (MonPSPBMN.senjata2.modeLoadData == "normal" || MonPSPBMN.senjata2.modeLoadData == "cari")
                        MonPSPBMN.senjata2.dataInisial = false;

                    MonPSPBMN.senjata2.getSenjata();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 6)
                {
                    if (MonPSPBMN.mesinNonTik2.modeLoadData == "normal" || MonPSPBMN.mesinNonTik2.modeLoadData == "cari")
                        MonPSPBMN.mesinNonTik2.dataInisial = false;

                    MonPSPBMN.mesinNonTik2.getMesinNonTik();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 7)
                {
                    if (MonPSPBMN.jlnJmbtn2.modeLoadData == "normal" || MonPSPBMN.jlnJmbtn2.modeLoadData == "cari")
                        MonPSPBMN.jlnJmbtn2.dataInisial = false;

                    MonPSPBMN.jlnJmbtn2.getJlnJmbtn();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 8)
                {
                    if (MonPSPBMN.propSus2.modeLoadData == "normal" || MonPSPBMN.propSus2.modeLoadData == "cari")
                        MonPSPBMN.propSus2.dataInisial = false;

                    MonPSPBMN.propSus2.getPropSus();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 9)
                {
                    if (MonPSPBMN.ATL2.modeLoadData == "normal" || MonPSPBMN.ATL2.modeLoadData == "cari")
                        MonPSPBMN.ATL2.dataInisial = false;

                    MonPSPBMN.ATL2.getATL();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 10)
                {
                    if (MonPSPBMN.mesinTik2.modeLoadData == "normal" || MonPSPBMN.mesinTik2.modeLoadData == "cari")
                        MonPSPBMN.mesinTik2.dataInisial = false;

                    MonPSPBMN.mesinTik2.getMesinTik();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 11)
                {
                    if (MonPSPBMN.ATB2.modeLoadData == "normal" || MonPSPBMN.ATB2.modeLoadData == "cari")
                        MonPSPBMN.ATB2.dataInisial = false;

                    MonPSPBMN.ATB2.getATB();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 12)
                {
                    if (MonPSPBMN.KDP2.modeLoadData == "normal" || MonPSPBMN.ATB2.modeLoadData == "cari")
                        MonPSPBMN.KDP2.dataInisial = false;

                    MonPSPBMN.KDP2.getKDP();
                }
                else if (MonPSPBMN.xtbDetail2.SelectedTabPageIndex == 13)
                {
                    if (MonPSPBMN.renovasi2.modeLoadData == "normal" || MonPSPBMN.ATB2.modeLoadData == "cari")
                        MonPSPBMN.renovasi2.dataInisial = false;

                    MonPSPBMN.renovasi2.getRenovasi();
                }
                #endregion
            }



        }

        private void mWlPSPBMNPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlPSPBMNTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion 

        #region PSP BMN LAIN
        KKW.MWD.PSPBMNLAIN.ucPSPBMNLAIN MonPSPBMNLAIN;

        private void setEventButtonMWasdalPSPBMNLAIN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlPSPBMNLAINRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlPSPBMNLAINMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlPSPBMNLAINPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlPSPBMNLAINTutup);
        }

        public void initGridMonWlPSPBMNLAIN()
        {
            inisialisasiForm();

            this.setEventButtonMWasdalPSPBMNLAIN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonPSPBMNLAIN);

            this.MonPSPBMNLAIN = new KKW.MWD.PSPBMNLAIN.ucPSPBMNLAIN(this);
            MonPSPBMNLAIN.strKdPelayanan = "AND (KD_PELAYANAN = '03')";
            //MonPSPBMNLAIN.strKdPelayanan = "AND ((KD_STATUS <> '01') OR (KD_STATUS <> '02'))";
            MonPSPBMNLAIN.nameTab1 = "BMN Sudah PSP";
            MonPSPBMNLAIN.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonPSPBMNLAIN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }
        private void nbiTLPSPBMNLAIN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlPSPBMNLAIN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalPSPBMNLAIN()
        {

        }

        private void mWlPSPBMNLAINRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlPSPBMNLAIN();
        }

        private void mWlPSPBMNLAINMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonPSPBMNLAIN.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonPSPBMNLAIN.tindakLanjut1.modeLoadData == "normal" || MonPSPBMNLAIN.tindakLanjut1.modeLoadData == "cari")
                    MonPSPBMNLAIN.tindakLanjut1.dataInisial = false;

                MonPSPBMNLAIN.tindakLanjut1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlPSPBMNLAINPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlPSPBMNLAINTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region ASP BMN

        KKW.MWD.ASPBMN.ucASPBMN MonASPBMN;

        private void setEventButtonMWasdalASPBMN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlASPBMNRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlASPBMNMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlASPBMNPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlASPBMNTutup);
        }

        public void initGridMonWlASPBMN()
        {
            inisialisasiForm();

            this.setEventButtonMWasdalASPBMN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonASPBMN);

            this.MonASPBMN = new KKW.MWD.ASPBMN.ucASPBMN(this);
            MonASPBMN.strKdPelayanan = "AND (KD_PELAYANAN = '04')";
            //MonASPBMN.strKdPelayanan = "";
            MonASPBMN.nameTab1 = "BMN Sudah PSP";
            MonASPBMN.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonASPBMN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLASPBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlASPBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalASPBMN()
        {

        }

        private void mWlASPBMNRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlASPBMN();
        }

        private void mWlASPBMNMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonASPBMN.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonASPBMN.tindakLanjut1.modeLoadData == "normal" || MonASPBMN.tindakLanjut1.modeLoadData == "cari")
                    MonASPBMN.tindakLanjut1.dataInisial = false;

                MonASPBMN.tindakLanjut1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlASPBMNPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlASPBMNTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region PBMNS

        KKW.MWD.PBMNS.ucPBMNS MonPBMNS;

        private void setEventButtonMWasdalPBMNS()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlPBMNSRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlPBMNSMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlPBMNSPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlPBMNSTutup);
        }

        public void initGridMonWlPBMNS()
        {
            inisialisasiForm();

            this.setEventButtonMWasdalPBMNS();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonPBMNS);

            this.MonPBMNS = new KKW.MWD.PBMNS.ucPBMNS(this);
            MonPBMNS.strKdPelayanan = "AND (KD_PELAYANAN = '05')";
            //MonPBMNS.strKdPelayanan = "AND ((KD_STATUS = '03') OR (KD_STATUS = '04') OR (KD_STATUS = '99'))";
            MonPBMNS.nameTab1 = "BMN Sudah PSP";
            MonPBMNS.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonPBMNS);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLPBMNS_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlPBMNS();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        private void kembalikeGridMWasdalPBMNS()
        {

        }

        private void mWlPBMNSRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlPBMNS();
        }

        private void mWlPBMNSMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonPBMNS.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonPBMNS.tindakLanjut1.modeLoadData == "normal" || MonPBMNS.tindakLanjut1.modeLoadData == "cari")
                    MonPBMNS.tindakLanjut1.dataInisial = false;

                MonPBMNS.tindakLanjut1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlPBMNSPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlPBMNSTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region SEWA BMN
        KKW.MWD.SWBMN.ucSWBMN MonSWBMN;

        private void setEventButtonMWasdalSWBMN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlSWBMNRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlSWBMNMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlSWBMNPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlSWBMNTutup);
        }

        private void initGridMonWlSWBMN()
        {
            this.setEventButtonMWasdalSWBMN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonSWBMN);

            MonSWBMN = new KKW.MWD.SWBMN.ucSWBMN(this);
            MonSWBMN.strKdPelayanan = "AND (KD_PELAYANAN = '06')";
            MonSWBMN.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);

            MonSWBMN.nameTab1 = "BMN Sudah PSP";
            MonSWBMN.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonSWBMN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLSWBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlSWBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalSWBMN()
        {

        }

        private void mWlSWBMNRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlSWBMN();
        }

        private void mWlSWBMNMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI

            if (MonSWBMN.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonSWBMN.tindakLanjut1.modeLoadData == "normal" || MonSWBMN.tindakLanjut1.modeLoadData == "cari")
                    MonSWBMN.tindakLanjut1.dataInisial = false;

                MonSWBMN.tindakLanjut1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlSWBMNPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlSWBMNTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region PINJAM PAKAI
        KKW.MWD.PPBMN.ucPPBMN MonPPBMN;

        private void setEventButtonMWasdalPPBMN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlPPBMNRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlPPBMNMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlPPBMNPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlPPBMNTutup);
        }

        private void initGridMonWlPPBMN()
        {
            this.setEventButtonMWasdalPPBMN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonPPBMN);

            this.MonPPBMN = new KKW.MWD.PPBMN.ucPPBMN(this);
            MonPPBMN.strKdPelayanan = "AND (KD_PELAYANAN = '07')";
            MonPPBMN.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);

            MonPPBMN.nameTab1 = "BMN Sudah PSP";
            MonPPBMN.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonPPBMN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLPPBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlPPBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalPPBMN()
        {

        }

        private void mWlPPBMNRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlPPBMN();
        }

        private void mWlPPBMNMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonPPBMN.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonPPBMN.tindakLanjut1.modeLoadData == "normal" || MonPPBMN.tindakLanjut1.modeLoadData == "cari")
                    MonPPBMN.tindakLanjut1.dataInisial = false;

                MonPPBMN.tindakLanjut1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlPPBMNPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlPPBMNTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region KERJASAMA PEMANFAATAN
        KKW.MWD.KPBMN.ucKPBMN MonKPBMN;

        private void setEventButtonMWasdalKPBMN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlKPBMNRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlKPBMNMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlKPBMNPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlKPBMNTutup);
        }

        private void initGridMonWlKPBMN()
        {
            this.setEventButtonMWasdalKPBMN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonKPBMN);

            this.MonKPBMN = new KKW.MWD.KPBMN.ucKPBMN(this);
            MonKPBMN.strKdPelayanan = "AND (KD_PELAYANAN = '08')";
            MonKPBMN.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonKPBMN.nameTab1 = "BMN Sudah PSP";
            MonKPBMN.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonKPBMN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLKPBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlKPBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalKPBMN()
        {

        }

        private void mWlKPBMNRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlKPBMN();
        }

        private void mWlKPBMNMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonKPBMN.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonKPBMN.tindakLanjut1.modeLoadData == "normal" || MonKPBMN.tindakLanjut1.modeLoadData == "cari")
                    MonKPBMN.tindakLanjut1.dataInisial = false;

                MonKPBMN.tindakLanjut1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlKPBMNPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlKPBMNTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region KSPI
        KKW.MWD.BGS.ucBGS MonBGS;

        private void setEventButtonMWasdalBGS()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlBGSRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlBGSMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlBGSPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlBGSTutup);
        }

        private void initGridMonWlBGS()
        {
            this.setEventButtonMWasdalBGS();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonBGS);

            this.MonBGS = new KKW.MWD.BGS.ucBGS(this);
            MonBGS.strKdPelayanan = "AND (KD_PELAYANAN = '09' OR KD_PELAYANAN = '10')";
            MonBGS.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonBGS.nameTab1 = "BMN Sudah PSP";
            MonBGS.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonBGS);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLBGS_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlBGS();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalBGS()
        {

        }

        private void mWlBGSRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlBGS();
        }

        private void mWlBGSMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonBGS.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonBGS.tindakLanjut1.modeLoadData == "normal" || MonBGS.tindakLanjut1.modeLoadData == "cari")
                    MonBGS.tindakLanjut1.dataInisial = false;

                MonBGS.tindakLanjut1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlBGSPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlBGSTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region BSG

        KKW.MWD.BSG.ucBSG MonBSG;

        private void setEventButtonMWasdalBSG()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlBSGRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlBSGMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlBSGPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlBSGTutup);
        }

        private void initGridMonWlBSG()
        {
            this.setEventButtonMWasdalBSG();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonBSG);

            this.MonBSG = new KKW.MWD.BSG.ucBSG(this);
            MonBSG.strKdPelayanan = "AND (KD_PELAYANAN = '10')";
            MonBSG.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonBSG.nameTab1 = "BMN Sudah PSP";
            MonBSG.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonBSG);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }


        private void nbiTLBSG_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlBSG();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalBSG()
        {

        }

        private void mWlBSGRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlBSG();
        }

        private void mWlBSGMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonBSG.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonBSG.tindakLanjut1.modeLoadData == "normal" || MonBSG.tindakLanjut1.modeLoadData == "cari")
                    MonKPBMN.tindakLanjut1.dataInisial = false;

                MonBSG.tindakLanjut1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlBSGPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlBSGTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region PENJUALAN
        KKW.MWD.PENJUALAN.ucPENJUALAN MonPENJUALAN;

        private void setEventButtonMWasdalPENJUALAN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlPENJUALANRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlPENJUALANMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlPENJUALANPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlPENJUALANTutup);
        }

        private void initGridMonWlPENJUALAN()
        {
            this.setEventButtonMWasdalPENJUALAN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonPenjualan);

            this.MonPENJUALAN = new KKW.MWD.PENJUALAN.ucPENJUALAN(this);
            MonPENJUALAN.strKdPelayanan = "AND (KD_PELAYANAN = '11')";
            MonPENJUALAN.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonPENJUALAN.nameTab1 = "BMN Sudah PSP";
            MonPENJUALAN.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonPENJUALAN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLPENJUALAN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlPENJUALAN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalPENJUALAN()
        {

        }

        private void mWlPENJUALANRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlPENJUALAN();
        }

        private void mWlPENJUALANMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonPENJUALAN.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonPENJUALAN.tindakLanjut1.modeLoadData == "normal" || MonPENJUALAN.tindakLanjut1.modeLoadData == "cari")
                    MonPENJUALAN.tindakLanjut1.dataInisial = false;

                MonPENJUALAN.tindakLanjut1.getTindakLanjut();
            }

            #endregion
        }

        private void mWlPENJUALANPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlPENJUALANTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region TUKAR MENUKAR
        KKW.MWD.TUKAR.ucTukar MonTUKAR;

        private void setEventButtonMWasdalTUKAR()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlTUKARRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlTUKARMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlTUKARPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlTUKARTutup);
        }

        private void initGridMonWlTUKAR()
        {
            this.setEventButtonMWasdalTUKAR();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonTukar);

            this.MonTUKAR = new KKW.MWD.TUKAR.ucTukar(this);
            MonTUKAR.strKdPelayanan = "AND (KD_PELAYANAN = '12')";
            MonTUKAR.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonTUKAR.nameTab1 = "BMN Sudah PSP";
            MonTUKAR.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonTUKAR);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLTMBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlTUKAR();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalTUKAR()
        {

        }

        private void mWlTUKARRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlTUKAR();
        }

        private void mWlTUKARMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonTUKAR.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonTUKAR.tindakLanjut1.modeLoadData == "normal" || MonTUKAR.tindakLanjut1.modeLoadData == "cari")
                    MonTUKAR.tindakLanjut1.dataInisial = false;

                MonTUKAR.tindakLanjut1.getTindakLanjut();
            }

            #endregion
        }

        private void mWlTUKARPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlTUKARTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region HIBAH
        KKW.MWD.HIBAH.ucHibah MonHIBAH;

        private void setEventButtonMWasdalHIBAH()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlHIBAHRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlHIBAHMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlHIBAHPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlHIBAHTutup);
        }

        private void initGridMonWlHIBAH()
        {
            this.setEventButtonMWasdalHIBAH();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonHibah);

            this.MonHIBAH = new KKW.MWD.HIBAH.ucHibah(this);
            MonHIBAH.strKdPelayanan = "AND (KD_PELAYANAN = '13')";
            MonHIBAH.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonHIBAH.nameTab1 = "BMN Sudah PSP";
            MonHIBAH.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonHIBAH);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLHIBAH_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlHIBAH();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalHIBAH()
        {

        }

        private void mWlHIBAHRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlHIBAH();
        }

        private void mWlHIBAHMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonHIBAH.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonHIBAH.tindakLanjut1.modeLoadData == "normal" || MonHIBAH.tindakLanjut1.modeLoadData == "cari")
                    MonHIBAH.tindakLanjut1.dataInisial = false;

                MonHIBAH.tindakLanjut1.getTindakLanjut();
            }

            #endregion
        }

        private void mWlHIBAHPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlHIBAHTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region PMP
        KKW.MWD.PMBMN.ucPMBMN MonPMBMN;

        private void setEventButtonMWasdalPMBMN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlPMBMNRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlPMBMNMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlPMBMNPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlPMBMNTutup);
        }

        private void initGridMonWlPMBMN()
        {
            this.setEventButtonMWasdalPMBMN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonModal);

            this.MonPMBMN = new KKW.MWD.PMBMN.ucPMBMN(this);
            MonPMBMN.strKdPelayanan = "AND (KD_PELAYANAN = '14')";
            MonPMBMN.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonPMBMN.nameTab1 = "BMN Sudah PSP";
            MonPMBMN.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonPMBMN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLPMP_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlPMBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalPMBMN()
        {

        }

        private void mWlPMBMNRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlPMBMN();
        }

        private void mWlPMBMNMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonPMBMN.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonPMBMN.tindakLanjut1.modeLoadData == "normal" || MonPMBMN.tindakLanjut1.modeLoadData == "cari")
                    MonPMBMN.tindakLanjut1.dataInisial = false;

                MonPMBMN.tindakLanjut1.getTindakLanjut();
            }

            #endregion
        }

        private void mWlPMBMNPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlPMBMNTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region PEMUSNAHAN
        KKW.MWD.MusnahBMN.ucMusnahBMN MonMusnahBMN;

        private void setEventButtonMWasdalMusnahBMN()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlMusnahBMNRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlMusnahBMNMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlMusnahBMNPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlMusnahBMNTutup);
        }

        private void initGridMonWlMusnahBMN()
        {
            this.setEventButtonMWasdalMusnahBMN();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonMusnah);

            this.MonMusnahBMN = new KKW.MWD.MusnahBMN.ucMusnahBMN(this);
            MonMusnahBMN.strKdPelayanan = "AND (KD_PELAYANAN = '15')";
            MonMusnahBMN.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);

            //MonMusnahBMN.strKdPelayanan = "AND ((KD_STATUS = '03') OR (KD_STATUS = '04') OR (KD_STATUS = '99'))";
            MonMusnahBMN.nameTab1 = "BMN Sudah PSP";
            MonMusnahBMN.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonMusnahBMN);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLPMBMN_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlMusnahBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalMusnahBMN()
        {

        }

        private void mWlMusnahBMNRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlMusnahBMN();
        }

        private void mWlMusnahBMNMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonMusnahBMN.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonMusnahBMN.tindakLanjut1.modeLoadData == "normal" || MonMusnahBMN.tindakLanjut1.modeLoadData == "cari")
                    MonMusnahBMN.tindakLanjut1.dataInisial = false;

                MonMusnahBMN.tindakLanjut1.getTindakLanjut();
            }

            #endregion
        }

        private void mWlMusnahBMNPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlMusnahBMNTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region PENGHAPUSAN KARENA PUTUSAN PENGADILAN
        KKW.MWD.HapusBmnPengadilan.ucHapusBmnPengadilan MonHapusBmnPengadilan;

        private void setEventButtonMWasdalHapusBmnPengadilan()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlHapusBmnPengadilanRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlHapusBmnPengadilanMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlHapusBmnPengadilanPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlHapusBmnPengadilanTutup);
        }

        private void initGridMonWlHapusBmnPengadilan()
        {
            this.setEventButtonMWasdalHapusBmnPengadilan();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonPengadilan);

            this.MonHapusBmnPengadilan = new KKW.MWD.HapusBmnPengadilan.ucHapusBmnPengadilan(this);
            MonHapusBmnPengadilan.strKdPelayanan = "AND (KD_PELAYANAN = '16')";
            MonHapusBmnPengadilan.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonHapusBmnPengadilan.nameTab1 = "BMN Sudah PSP";
            MonHapusBmnPengadilan.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonHapusBmnPengadilan);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLPBKPP_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlHapusBmnPengadilan();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalHapusBmnPengadilan()
        {

        }

        private void mWlHapusBmnPengadilanRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlHapusBmnPengadilan();
        }

        private void mWlHapusBmnPengadilanMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonHapusBmnPengadilan.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonHapusBmnPengadilan.tindakLanjut1.modeLoadData == "normal" || MonHapusBmnPengadilan.tindakLanjut1.modeLoadData == "cari")
                    MonHapusBmnPengadilan.tindakLanjut1.dataInisial = false;

                MonHapusBmnPengadilan.tindakLanjut1.getTindakLanjut();
            }

            #endregion
        }

        private void mWlHapusBmnPengadilanPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlHapusBmnPengadilanTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region PNGHAPUSAN KARENA SEBAB LAIN
        KKW.MWD.HapusBmnLain.ucHapusBmnLain MonHapusBmnLain;

        private void setEventButtonMWasdalHapusBmnLain()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlHapusBmnLainRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlHapusBmnLainMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlHapusBmnLainPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlHapusBmnLainTutup);
        }

        private void initGridMonWlHapusBmnLain()
        {
            this.setEventButtonMWasdalHapusBmnLain();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonHapusLain);

            this.MonHapusBmnLain = new KKW.MWD.HapusBmnLain.ucHapusBmnLain(this);
            MonHapusBmnLain.strKdPelayanan = "AND (KD_PELAYANAN = '17')";
            MonHapusBmnLain.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            MonHapusBmnLain.nameTab1 = "BMN Sudah PSP";
            MonHapusBmnLain.nameTab2 = "BMN Belum PSP";
            this.setPanel(this.MonHapusBmnLain);

            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void nbiTLPBKSL_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlHapusBmnLain();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void kembalikeGridMWasdalHapusBmnLain()
        {

        }

        private void mWlHapusBmnLainRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlHapusBmnLain();
        }

        private void mWlHapusBmnLainMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region PSP SUDAH JADI
            if (MonHapusBmnLain.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (MonHapusBmnLain.tindakLanjut1.modeLoadData == "normal" || MonHapusBmnLain.tindakLanjut1.modeLoadData == "cari")
                    MonHapusBmnLain.tindakLanjut1.dataInisial = false;

                MonHapusBmnLain.tindakLanjut1.getTindakLanjut();
            }

            #endregion
        }

        private void mWlHapusBmnLainPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void mWlHapusBmnLainTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion


        #endregion MONITORING


        #region MONITORING PNBP

        KKW.MONPNBP.SSBP.ucGridPnbp gridPnbp;
        ArrayList dsGridLaporanPnbp;
        private void setTombolPnbpGrid()
        {
            bbiPnbpRefresh.Enabled = true;
            bbiPnbpMore.Enabled = false;
        }
        private void resetEventTombolPnbp()
        {
            konfigApp.RemoveClickEvent(bbiPnbpRefresh);
            konfigApp.RemoveClickEvent(bbiPnbpMore);
            konfigApp.RemoveClickEvent(bbiPnbpKeluar);
        }

        #region PNBP per BMN
        private void setEventTombolLaporanPnbp()
        {
            resetEventTombolPnbp();
            bbiPnbpRefresh.ItemClick += new ItemClickEventHandler(bbiLapPnbpRefreshKlik);
            bbiPnbpMore.ItemClick += new ItemClickEventHandler(bbiLapPnbpMoreDataKlik);
            bbiPnbpBack.ItemClick += new ItemClickEventHandler(bbiPnbpTutup_ItemClick);
            bbiPnbpKeluar.ItemClick += new ItemClickEventHandler(bbiPnbpKeluarKlik);
            rpPnbp.Text = "Laporan PNBP";
        }

        private void initGridLapPnbp()
        {
            if (gridPnbp == null)
            {
                gridPnbp = new KKW.MONPNBP.SSBP.ucGridPnbp();
                gridPnbp.cariDataOnline = new CariDataOnline(setCariDataLapPnbp);
            }
            setTombolPnbpGrid();
            setEventTombolLaporanPnbp();
            setPanel(gridPnbp);
        }

        private void bbiLapPnbpRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridPnbp.teNamaKolom.Text = "";
            gridPnbp.teCari.Text = "";
            gridPnbp.fieldDicari = "";
            gridPnbp.dataInisial = true;
            this.dataInisial = true;
            this.getInitLaporanPnbp();
        }

        private void bbiLapPnbpMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitLaporanPnbp();
            }
        }

        private void bbiExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintableComponentLink componenlink = new PrintableComponentLink(new PrintingSystem());
            componenlink.Component = gridPnbp.gcPnbp;
            componenlink.CreateDocument();
            PrintTool pt = new PrintTool(componenlink.PrintingSystemBase);
            pt.ShowRibbonPreview();
        }

        private void bbiPnbpKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Application.Exit();
            inisialisasiForm();
        }

        private void nbiMonitoringPnbp_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Laporan PNBP";
            konfigApp.strSubMenu = "Laporan PNBP";
            this.Enabled = false;
            this.inisialisasiForm();
            rpPnbp.Visible = true;
            rpPnbp.Text = "Laporan PNBP";

            //rpTindakLanjut.Visible = true;
            //rpTindakLanjut.Text = "Laporan PNBP";
            ribbon.SelectedPage = rpPnbp;
            initGridLapPnbp();
            modeCari = false;
            gridPnbp.teNamaKolom.Text = "";
            gridPnbp.teCari.Text = "";
            gridPnbp.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitLaporanPnbp();
        }
        private void bbiPnbpTutup_ItemClick(object sender, ItemClickEventArgs e)
        {
            //inisialisasiForm();
            Application.Exit();
        }

        #region --++ Ambil Data
        SvcWasdalManfaatMonPnbp.call_pttClient ambilDataLapPnbp;
        SvcWasdalManfaatMonPnbp.OutputParameters dOutLapPnbp;

        private void getInitLaporanPnbp()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatMonPnbp.InputParameters parInp = new SvcWasdalManfaatMonPnbp.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                parInp.STR_WHERE = String.Format(" ID_KORWIL = "+konfigApp.idKorwil +" "+ this.strCari);
                ambilDataLapPnbp = new SvcWasdalManfaatMonPnbp.call_pttClient();
                ambilDataLapPnbp.Open();
                ambilDataLapPnbp.Beginexecute(parInp, new AsyncCallback(getLaporanPnbp), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getLaporanPnbp(IAsyncResult result)
        {
            try
            {
                dOutLapPnbp = ambilDataLapPnbp.Endexecute(result);
                ambilDataLapPnbp.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsLaporanPnbp(dsLaporanPnbp), dOutLapPnbp);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsLaporanPnbp(SvcWasdalManfaatMonPnbp.OutputParameters dataOut);

        private void dsLaporanPnbp(SvcWasdalManfaatMonPnbp.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_PNBP.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_PNBP[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiPnbpMore.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
                this.masihAdaData = false;
                bbiPnbpMore.Enabled = false;
            }
            if (dataInisial == true)
            {
                dsGridLaporanPnbp = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_MON_PNBP[i].TGL_BUKTI_LAKSANA = (dataOut.SF_MON_PNBP[i].TGL_BUKTI_LAKSANA.ToString() == "11/11/1000" ? null : dataOut.SF_MON_PNBP[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_MON_PNBP[i].TGL_SETOR = (dataOut.SF_MON_PNBP[i].TGL_SETOR.ToString() == "11/11/1000" ? null : dataOut.SF_MON_PNBP[i].TGL_SETOR);
                dataOut.SF_MON_PNBP[i].TGL_SK = (dataOut.SF_MON_PNBP[i].TGL_SK.ToString() == "11/11/1000" ? null : dataOut.SF_MON_PNBP[i].TGL_SK);
                dataOut.SF_MON_PNBP[i].TGL_TRANSAKSI = (dataOut.SF_MON_PNBP[i].TGL_TRANSAKSI.ToString() == "11/11/1000" ? null : dataOut.SF_MON_PNBP[i].TGL_TRANSAKSI);
                dsGridLaporanPnbp.Add(dataOut.SF_MON_PNBP[i]);
            }

            gridPnbp.sbCariOnline.Enabled = !modeCari;
            gridPnbp.dsDataSource = dsGridLaporanPnbp;
            gridPnbp.displayData();
            if (modeCari == true)
            {
                string xSatu = gridPnbp.teNamaKolom.Text.Trim();
                string xDua = gridPnbp.teCari.Text.Trim();
                string xTiga = gridPnbp.fieldDicari;
                gridPnbp.gvPnbp.ClearColumnsFilter();
                gridPnbp.teNamaKolom.Text = xSatu;
                gridPnbp.teCari.Text = xDua;
                gridPnbp.fieldDicari = xTiga;
            }
            else
                gridPnbp.gvPnbp.ClearColumnsFilter();
            gridPnbp.labelTotData.Text = "";
            gridPnbp.labelTotData.Text = "Menampilkan " + jmlCurrent + " dari total " + totalData + " data";
        }

        private void setCariDataLapPnbp(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitLaporanPnbp();
        }
        #endregion

        #endregion

        #region PNBP perSSBP

        KKW.MONPNBP.SWBMN.ucSewaBmnGrid gridPnbpSSBP;
        //ucPNBPJualForm ucPnbpJual;
        //ucPNBPSewaBmnForm ucPnbpSewa;
        ArrayList dsGridMonPnbpSSBP;

        private void nbiMonPnbpSSBP_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Monitoring PNBP";
            konfigApp.strSubMenu = " per SSBP";
            this.Enabled = false;
            this.inisialisasiForm();
            rpPnbp.Visible = true;
            rpPnbp.Text = "Monitoring PNBP per SSBP";

            ribbon.SelectedPage = rpPnbp;
            initGridMonPnbpSSBP();
            modeCari = false;
            gridPnbpSSBP.teNamaKolom.Text = "";
            gridPnbpSSBP.teCari.Text = "";
            gridPnbpSSBP.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitMonPnbpSSBP();
        }

        private void setEventTombolMonPnbpSSBP()
        {
            resetEventTombolPnbp();
            bbiPnbpRefresh.ItemClick += new ItemClickEventHandler(bbiMonPnbpSSBPRefreshKlik);
            bbiPnbpMore.ItemClick += new ItemClickEventHandler(bbiMonPnbpSSBPMoreDataKlik);
            bbiPnbpBack.ItemClick += new ItemClickEventHandler(bbiPnbpTutup_ItemClick);
            bbiPnbpKeluar.ItemClick += new ItemClickEventHandler(bbiMonPnbpSSBPKeluarKlik);
            rpPnbp.Text = "Monitoring PNBP per SSBP";
        }

        private void initGridMonPnbpSSBP()
        {
            if (gridPnbpSSBP == null)
            {
                gridPnbpSSBP = new KKW.MONPNBP.SWBMN.ucSewaBmnGrid();
                gridPnbpSSBP.cariDataOnline = new CariDataOnline(setCariDataMonPnbpSSBP);
                //PERLUDICEK//gridPnbpSSBP.detailDataGrid = new DetailDataGrid(ambilDatadariGridPnbpSSBP);
            }
            setTombolPnbpGrid();
            setEventTombolLaporanPnbp();
            setPanel(gridPnbpSSBP);
        }

        #region ACTION BUTTON
        private void bbiMonPnbpSSBPRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridPnbpSSBP.teNamaKolom.Text = "";
            gridPnbpSSBP.teCari.Text = "";
            gridPnbpSSBP.fieldDicari = "";
            gridPnbpSSBP.dataInisial = true;
            this.dataInisial = true;
            this.getInitMonPnbpSSBP();
        }

        private void bbiMonPnbpSSBPMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitMonPnbpSSBP();
            }
        }

        private void bbiSSBPExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintableComponentLink componenlink = new PrintableComponentLink(new PrintingSystem());
            componenlink.Component = gridPnbpSSBP.gcGridSk;
            componenlink.CreateDocument();
            PrintTool pt = new PrintTool(componenlink.PrintingSystemBase);
            pt.ShowRibbonPreview();
        }

        private void bbiMonPnbpSSBPKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            inisialisasiForm();
        }
        #endregion

        #region --++ Ambil Data
        SvcWasdalMonPnbpSsbp.call_pttClient ambilDataMonPnbpSSBP;
        SvcWasdalMonPnbpSsbp.OutputParameters dOutMonPnbpSSBP;

        private void getInitMonPnbpSSBP()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalMonPnbpSsbp.InputParameters parInp = new SvcWasdalMonPnbpSsbp.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format(" NTPN IS NOT NULL AND ID_PEMOHON = {0} {1}", konfigApp.idSatker, this.strCari);
                parInp.STR_WHERE = String.Format(" NTPN IS NOT NULL AND ID_PEMOHON = {0} {1}", konfigApp.idKorwil, this.strCari);
                ambilDataMonPnbpSSBP = new SvcWasdalMonPnbpSsbp.call_pttClient();
                ambilDataMonPnbpSSBP.Open();
                ambilDataMonPnbpSSBP.Beginexecute(parInp, new AsyncCallback(getMonPnbpSSBP), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getMonPnbpSSBP(IAsyncResult result)
        {
            try
            {
                dOutMonPnbpSSBP = ambilDataMonPnbpSSBP.Endexecute(result);
                ambilDataMonPnbpSSBP.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsMonPnbpSSBP(dsMonPnbpSSBP), dOutMonPnbpSSBP);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsMonPnbpSSBP(SvcWasdalMonPnbpSsbp.OutputParameters dataOut);

        private void dsMonPnbpSSBP(SvcWasdalMonPnbpSsbp.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_WASDAL_PNBP_SSBP.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_WASDAL_PNBP_SSBP[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiPnbpMore.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
                this.masihAdaData = false;
                bbiPnbpMore.Enabled = false;
            }
            if (dataInisial == true)
            {
                dsGridMonPnbpSSBP = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                //dataOut.SF_MON_PNBP[i].TGL_BUKTI_LAKSANA = (dataOut.SF_MON_PNBP[i].TGL_BUKTI_LAKSANA.ToString() == "11/11/1000" ? "-" : dataOut.SF_MON_PNBP[i].TGL_BUKTI_LAKSANA);
                //dataOut.SF_MON_PNBP[i].TGL_SETOR = (dataOut.SF_MON_PNBP[i].TGL_SETOR.ToString() == "11/11/1000" ? "" : dataOut.SF_MON_PNBP[i].TGL_SETOR);
                //dataOut.SF_MON_PNBP[i].TGL_SK = (dataOut.SF_MON_PNBP[i].TGL_SK.ToString() == "11/11/1000" ? "" : dataOut.SF_MON_PNBP[i].TGL_SK);
                //dataOut.SF_MON_PNBP[i].TGL_TRANSAKSI = (dataOut.SF_MON_PNBP[i].TGL_TRANSAKSI.ToString() == "11/11/1000" ? "" : dataOut.SF_MON_PNBP[i].TGL_TRANSAKSI);
                dsGridMonPnbpSSBP.Add(dataOut.SF_MON_WASDAL_PNBP_SSBP[i]);
            }

            gridPnbpSSBP.sbCariOnline.Enabled = !modeCari;
            gridPnbpSSBP.dsDataSource = dsGridMonPnbpSSBP;
            gridPnbpSSBP.displayData();
            if (modeCari == true)
            {
                string xSatu = gridPnbpSSBP.teNamaKolom.Text.Trim();
                string xDua = gridPnbpSSBP.teCari.Text.Trim();
                string xTiga = gridPnbpSSBP.fieldDicari;
                gridPnbpSSBP.gvGridSk.ClearColumnsFilter();
                gridPnbpSSBP.teNamaKolom.Text = xSatu;
                gridPnbpSSBP.teCari.Text = xDua;
                gridPnbpSSBP.fieldDicari = xTiga;
            }
            else
                gridPnbpSSBP.gvGridSk.ClearColumnsFilter();
            gridPnbpSSBP.labelTotData.Text = "";
            gridPnbpSSBP.labelTotData.Text = "Menampilkan " + jmlCurrent + " dari total " + totalData + " data";
        }

        private void setCariDataMonPnbpSSBP(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitMonPnbpSSBP();
        }

        

        #endregion


        #endregion

        #endregion

        #region Report

        #region  Show Data Wasdal penggunaan
        KKW.WL.PENGGUNAAN.xrLapWasdalBMN xrlapwasdal;
        public void getInitWasdalBMNLap()
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNSelect.InputParameters inPar = new SvcWasdalPSPBMNSelect.InputParameters();
                inPar.P_MAX = 0;
                inPar.P_MAXSpecified = true;
                inPar.P_MIN = 0;
                inPar.P_MINSpecified = true;
                inPar.STR_WHERE = "ID_KORWIL = " + konfigApp.idKorwil;
                inPar.P_COL = "IS_TB";
                inPar.P_SORT = "DESC";
                ambilWasdal = new SvcWasdalPSPBMNSelect.execute_pttClient();
                ambilWasdal.Beginexecute(inPar, new AsyncCallback(getDataWasdalBMNLap), null);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataWasdalBMNLap(IAsyncResult result)
        {
            try
            {
                dataOutWasdal = ambilWasdal.Endexecute(result);
                ambilWasdal.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ToggleProgressBar(this.fToggleProgressBar), "");
                this.Invoke(new SimpanDataWasdalBMNLap(this.simpanDataWasdalBMNLap), dataOutWasdal);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ToggleProgressBar(this.fToggleProgressBar), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void SimpanDataWasdalBMNLap(SvcWasdalPSPBMNSelect.OutputParameters dataOut);

        private void simpanDataWasdalBMNLap(SvcWasdalPSPBMNSelect.OutputParameters dataOut)
        {
            xrlapwasdal = new KKW.WL.PENGGUNAAN.xrLapWasdalBMN();
            //xrlapwasdal = new penggunaan();
            int jmlData = dataOut.SF_ROW_WASDAL_PSP.Count();
            for (int i = 0; i < jmlData; i++)
            {
                if (dataOut.SF_ROW_WASDAL_PSP[i].IS_TB == "Y")
                {
                    dataOut.SF_ROW_WASDAL_PSP[i].IS_TB = "I. Tanah dan / atau bangunan";

                }
                else
                {
                    dataOut.SF_ROW_WASDAL_PSP[i].IS_TB = "II. Selain Tanah dan / atau bangunan 5)";
                }

                if (dataOut.SF_ROW_WASDAL_PSP[i].KD_STATUS == "01" || dataOut.SF_ROW_WASDAL_PSP[i].KD_STATUS == "02" || dataOut.SF_ROW_WASDAL_PSP[i].KD_STATUS == "03")
                {
                    dataOut.SF_ROW_WASDAL_PSP[i].NIP_PENANDATANGAN = "Ya";
                    dataOut.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL = "";
                    dataOut.SF_ROW_WASDAL_PSP[i].NM_PHK_LAIN = "";
                }
                else if (dataOut.SF_ROW_WASDAL_PSP[i].KD_STATUS == "99")
                {
                    dataOut.SF_ROW_WASDAL_PSP[i].NIP_PENANDATANGAN = "";
                    dataOut.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL = "Ya";
                    dataOut.SF_ROW_WASDAL_PSP[i].NM_PHK_LAIN = "";
                }
                else if (dataOut.SF_ROW_WASDAL_PSP[i].KD_STATUS == "04" || dataOut.SF_ROW_WASDAL_PSP[i].KD_STATUS == "05" || dataOut.SF_ROW_WASDAL_PSP[i].KD_STATUS == "06" || dataOut.SF_ROW_WASDAL_PSP[i].KD_STATUS == "07")
                {
                    dataOut.SF_ROW_WASDAL_PSP[i].NIP_PENANDATANGAN = "";
                    dataOut.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL = "";
                    dataOut.SF_ROW_WASDAL_PSP[i].NM_PHK_LAIN = "Ya";
                }
                dataOut.SF_ROW_WASDAL_PSP[i].TGL_CREATED = konfigApp.setDefaultDate(dataOut.SF_ROW_WASDAL_PSP[i].TGL_CREATED);
                dataOut.SF_ROW_WASDAL_PSP[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_ROW_WASDAL_PSP[i].TGL_SK);
            }
            xrlapwasdal.bsLapWasdalBMN.DataSource = dataOutWasdal.SF_ROW_WASDAL_PSP;

            xrlapwasdal.bsLapWasdalBMN.DataSource = dataOut.SF_ROW_WASDAL_PSP;
            xrlapwasdal.xrlKodeKPB.Text = konfigApp.kodeKorwil;
            xrlapwasdal.xrlNamaKPB.Text = konfigApp.namaKorwil;
            Parameter param = new Parameter();
            param.Name = "tahunAnggaran";
            param.Type = typeof(System.String);
            param.Value = tahunAnggaran;
            param.Description = "";
            param.Visible = false;

            Parameter param1 = new Parameter();
            param1.Name = "tanggal";
            param1.Type = typeof(System.String);
            param1.Value = konfigApp.setDefaultDate(DateTime.Now);
            param1.Description = "";
            param1.Visible = false;
            //AppPengguna.KKW.WL.PSPBMN.LP.xrLapWasdalBMN xrlapwasdalbmn = new AppPengguna.KKW.WL.PSPBMN.LP.xrLapWasdalBMN();

            xrlapwasdal.Parameters.Add(param);
            //xrlapwasdal.ShowPreviewDialog();
            #region setting margin, paperkind
            DevExpress.XtraPrinting.Preview.PrintPreviewFormEx form = new DevExpress.XtraPrinting.Preview.PrintPreviewFormEx();
            xrlapwasdal.PaperKind = System.Drawing.Printing.PaperKind.A4; //paperkind
            xrlapwasdal.Margins = new System.Drawing.Printing.Margins(75, 75, 75, 0); //margin
            form.PrintingSystem = xrlapwasdal.PrintingSystem;
            xrlapwasdal.CreateDocument();
            form.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Scale, new object[] { 1 }); //autofit
            form.ShowDialog();
            #endregion
        }

        #endregion

        #region Init Data Lap Manfaat
        private void GetIniLapManfaat()
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcLapWasdalManfaat.InputParameters parInp = new SvcLapWasdalManfaat.InputParameters();

                if (dataInisial == true)
                {
                    this.currentMaks = konfigApp.dataAkhir;
                    this.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    this.currentMin = this.currentMaks + 1;
                    this.currentMaks = this.currentMaks + konfigApp.dataAkhir;
                }

                parInp.P_MAX = 0;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = "ID_KORWIL = " + konfigApp.idKorwil + " AND THN_ANG='" + tahunAnggaran + "' ";
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "n";
                svcLapWasdalManfaatSelect = new SvcLapWasdalManfaat.execute_pttClient();
                svcLapWasdalManfaatSelect.Beginexecute(parInp, new AsyncCallback(getDataLapManfaat), null);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ToggleProgressBar(this.fToggleProgressBar), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataLapManfaat(IAsyncResult result)
        {
            try
            {
                outLapWasdalManfaatSelect = svcLapWasdalManfaatSelect.Endexecute(result);
                svcLapWasdalManfaatSelect.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ToggleProgressBar(this.fToggleProgressBar), "");
                this.Invoke(new LoadDataLapManfaat(this.loadDataLapManfaat), outLapWasdalManfaatSelect);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ToggleProgressBar(this.fToggleProgressBar), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataLapManfaat(SvcLapWasdalManfaat.OutputParameters dataOut);

        private void loadDataLapManfaat(SvcLapWasdalManfaat.OutputParameters dataOut)
        {
            ReportPrintTool pt;
            int jmlData = dataOut.SF_LAP_WASDAL_MANFAAT_SK.Count();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                this.bbiMWasdalMore.Enabled = true;
            }
            else
            {
                if (this.modeLoadData == "normal")
                {
                    this.masihAdaData = false;
                    this.bbiMWasdalMore.Enabled = false;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                }

            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_LAP_WASDAL_MANFAAT_SK[i].TGL_SETOR = konfigApp.setDefaultDate(dataOut.SF_LAP_WASDAL_MANFAAT_SK[i].TGL_SETOR);
                dataOut.SF_LAP_WASDAL_MANFAAT_SK[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_LAP_WASDAL_MANFAAT_SK[i].TGL_SK);

            }

            KKW.WL.PEMANFAATAN.xrPemanfaatanBMN xrpemanfaatanbmn = new KKW.WL.PEMANFAATAN.xrPemanfaatanBMN(); 
            //KSK.WL.PEMANFAATAN.pemanfaatan xrpemanfaatanbmn = new KSK.WL.PEMANFAATAN.pemanfaatan();
            xrpemanfaatanbmn.bsLapWasdalBMN.DataSource = dataOut.SF_LAP_WASDAL_MANFAAT_SK;
            pt = new ReportPrintTool(xrpemanfaatanbmn);
            pt.AutoShowParametersPanel = true;
            //pt.ShowPreviewDialog();
            Parameter param = new Parameter();
            param.Name = "tahunAnggaran";
            param.Type = typeof(System.String);
            param.Value = tahunAnggaran;
            param.Description = "";
            param.Visible = false;

            #region setting margin, paperkind
            DevExpress.XtraPrinting.Preview.PrintPreviewFormEx form = new DevExpress.XtraPrinting.Preview.PrintPreviewFormEx();
            xrpemanfaatanbmn.PaperKind = System.Drawing.Printing.PaperKind.A4; //paperkind
            xrpemanfaatanbmn.Margins = new System.Drawing.Printing.Margins(75, 75, 75, 0); //margin
            form.PrintingSystem = xrpemanfaatanbmn.PrintingSystem;
            xrpemanfaatanbmn.CreateDocument();
            form.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Scale, new object[] { 1 }); //autofit
            form.ShowDialog();
            #endregion
        }
        #endregion

        #region Init Data Lap Pemindahtanganan
        //KKW.WL.PEMINDAHTANGANAN.xrPemindahtangananBMN xrpemindahtangananBMN;
        private void GetIniLapPemindahtanganan()
        {
            try
            {
                Thread mythread = new Thread(new ThreadStart(ShowProgresBar));
                mythread.Start();
                SvcLapPemindahTanganan.InputParameters parInp = new SvcLapPemindahTanganan.InputParameters();

                parInp.P_MAX = 0;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = "ID_KORWIL = " + konfigApp.idKorwil+" AND THN_ANG='" + tahunAnggaran+"' ";
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "n";
                svcLapPemindahtanganan = new SvcLapPemindahTanganan.execute_pttClient();
                svcLapPemindahtanganan.Beginexecute(parInp, new AsyncCallback(getDataLapPemindahtanganan), null);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataLapPemindahtanganan(IAsyncResult result)
        {
            try
            {
                outLapPemindahtanganan = svcLapPemindahtanganan.Endexecute(result);
                svcLapPemindahtanganan.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new LoadDataLapPemindahtanganan(this.loadDataLapPemindahtanganan), outLapPemindahtanganan);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataLapPemindahtanganan(SvcLapPemindahTanganan.OutputParameters dataOut);

        private void loadDataLapPemindahtanganan(SvcLapPemindahTanganan.OutputParameters dataOut)
        {
            ReportPrintTool pt;
            int jmlData = dataOut.SF_LAP_WASDAL_PT_SK.Count();

            for (int i = 0; i <= jmlData - 1; i++)
            {
                dataOut.SF_LAP_WASDAL_PT_SK[i].TGL_SETOR = konfigApp.setDefaultDate(dataOut.SF_LAP_WASDAL_PT_SK[i].TGL_SETOR);
                dataOut.SF_LAP_WASDAL_PT_SK[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_LAP_WASDAL_PT_SK[i].TGL_SK);
            }

            xrPemindahtangananBMN xrpemindahtangananBMN = new xrPemindahtangananBMN(); 
            xrpemindahtangananBMN.bsLapWasdalBMN.DataSource = dataOut.SF_LAP_WASDAL_PT_SK;
            #region setting margin, paperkind
            DevExpress.XtraPrinting.Preview.PrintPreviewFormEx form = new DevExpress.XtraPrinting.Preview.PrintPreviewFormEx();
            xrpemindahtangananBMN.PaperKind = System.Drawing.Printing.PaperKind.A4; //paperkind
            xrpemindahtangananBMN.Margins = new System.Drawing.Printing.Margins(75, 75, 75, 0); //margin
            form.PrintingSystem = xrpemindahtangananBMN.PrintingSystem;
            xrpemindahtangananBMN.CreateDocument();
            form.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Scale, new object[] { 1 }); //autofit
            form.ShowDialog();
            #endregion
            fToggleProgressBar("stop");
        }
        #endregion

        #region Init Data Lap Penghapusan
        //KKW.WL.PEMINDAHTANGANAN.xrPemindahtangananBMN xrpemindahtangananBMN;
        private void GetIniLapPenghapusan()
        {
            try
            {
                Thread mythread = new Thread(new ThreadStart(ShowProgresBar));
                mythread.Start();
                SvcWasdalHapusLapWasdalMSK.InputParameters parInp = new SvcWasdalHapusLapWasdalMSK.InputParameters();

                parInp.P_MAX = 0;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = "ID_KORWIL = " + konfigApp.idKorwil+"  AND THN_ANG='" + tahunAnggaran+"' ";
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "n";
                svcLapPenghapusan = new SvcWasdalHapusLapWasdalMSK.lapWasdalMSK_pttClient();
                svcLapPenghapusan.Beginexecute(parInp, new AsyncCallback(getDataLapPenghapusan), null);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataLapPenghapusan(IAsyncResult result)
        {
            try
            {
                outLapPenghapusan = svcLapPenghapusan.Endexecute(result);
                svcLapPenghapusan.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new LoadDataLapPenghapusan(this.loadDataLapPenghapusan), outLapPenghapusan);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataLapPenghapusan(SvcWasdalHapusLapWasdalMSK.OutputParameters dataOut);

        private void loadDataLapPenghapusan(SvcWasdalHapusLapWasdalMSK.OutputParameters dataOut)
        {
            ReportPrintTool pt;
            int jmlData = dataOut.SF_LAP_WASDAL_HAPUS_SK.Count();

            for (int i = 0; i <= jmlData - 1; i++)
            {
                dataOut.SF_LAP_WASDAL_HAPUS_SK[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_LAP_WASDAL_HAPUS_SK[i].TGL_SK);
            }

            xrPenghapusanBMN xrpemindahtangananBMN = new xrPenghapusanBMN(); 
            xrpemindahtangananBMN.bsLapWasdalBMN.DataSource = dataOut.SF_LAP_WASDAL_HAPUS_SK;
            pt = new ReportPrintTool(xrpemindahtangananBMN);
            pt.AutoShowParametersPanel = true;
            pt.ShowPreviewDialog();
            fToggleProgressBar("stop");
        }
        #endregion

        #region Init Lap Data Penertiban
        private void getInitLapPenertiban()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcMonPenertibanKoEsKl.InputParameters parInp = new SvcMonPenertibanKoEsKl.InputParameters();
                parInp.P_COL = "";
                parInp.P_MAX = 0;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
               // parInp.P_COUNT = "N";
                parInp.STR_WHERE = String.Format("ID_KORWIL = {0} AND THN_ANG='" + tahunAnggaran+"' ", konfigApp.idUser, this.strCari);  //ini diganti jadi gini
                ambilPenertiban = new SvcMonPenertibanKoEsKl.selectMonPenertibanKorEsKl_pttClient();
                ambilPenertiban.Open();
                ambilPenertiban.Beginexecute(parInp, new AsyncCallback(getLapPenertiban), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getLapPenertiban(IAsyncResult result)
        {
            try
            {
                dOutPenertiban = ambilPenertiban.Endexecute(result);    //dari sini langsung ke catch
                ambilPenertiban.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsLapPenertiban(dsLapPenertiban), dOutPenertiban);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);   //disini
            }
        }

        private delegate void DsLapPenertiban(SvcMonPenertibanKoEsKl.OutputParameters dataOut);

        private void dsLapPenertiban(SvcMonPenertibanKoEsKl.OutputParameters dataOut)
        {
            ReportPrintTool pt;
            xrPenertiban xrTertib = new xrPenertiban();
            xrTertib.bsLapPenertiban.DataSource = dataOut.SF_MON_KOR_ES_KL_PENERTIBAN;

            #region setting margin, paperkind
            DevExpress.XtraPrinting.Preview.PrintPreviewFormEx form = new DevExpress.XtraPrinting.Preview.PrintPreviewFormEx();
            xrTertib.PaperKind = System.Drawing.Printing.PaperKind.A4; //paperkind
            xrTertib.Margins = new System.Drawing.Printing.Margins(75, 75, 75, 0); //margin
            form.PrintingSystem = xrTertib.PrintingSystem;
            xrTertib.CreateDocument();
            form.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Scale, new object[] { 1 }); //autofit
            form.ShowDialog();
            #endregion

            fToggleProgressBar("stop");
        }

        #endregion
        #endregion

        #region LAPORAN WASDAL
        public KKW.WL.ucLapWasdal lapWasdal;

        #region PENGGUNAAN BMN


        private void initGridMonWlGunaBMN()
        {
            inisialisasiForm();

            this.setEventButtonMWasdalGuna();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonGuna);

            this.lapWasdal = new KKW.WL.ucLapWasdal(this);
            lapWasdal.strKdPelayanan = "AND ((KD_PELAYANAN = '02') OR (KD_PELAYANAN = '03') OR (KD_PELAYANAN = '04') OR (KD_PELAYANAN = '05'))";
            lapWasdal.namaModul = "Penggunaan BMN";
            this.setPanel(this.lapWasdal);
            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void setEventButtonMWasdalGuna()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlGunaRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlGunaMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlGunaPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlGunaTutup);
        }

        private void nbiMonitoringWasdalGunaBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlGunaBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void mWlGunaRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlGunaBMN();
        }

        private void mWlGunaMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lapWasdal.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (lapWasdal.penggunaan1.modeLoadData == "normal" || lapWasdal.penggunaan1.modeLoadData == "cari")
                    lapWasdal.penggunaan1.dataInisial = false;

                lapWasdal.penggunaan1.getTindakLanjut();
            }
        }

        private void mWlGunaPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            puParameterLaporan puParam = new puParameterLaporan();
            puParam.ShowDialog();
            tahunAnggaran = puParam.thnAng;
            //lapWasdal.print("penggunaan");
            getInitWasdalBMNLap();
        }

        private void mWlGunaTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }


        #endregion

        #region PEMANFAATAN BMN

        private void initGridMonWlManfaatBMN()
        {
            this.setEventButtonMWasdalManfaat();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonManfaat);

            this.lapWasdal = new KKW.WL.ucLapWasdal(this);
            lapWasdal.strKdPelayanan = "AND ((KD_PELAYANAN = '06')OR(KD_PELAYANAN = '07')OR(KD_PELAYANAN = '08')OR(KD_PELAYANAN = '09')OR(KD_PELAYANAN = '10'))";
            lapWasdal.namaModul = "Pemanfaatan BMN";
            this.setPanel(this.lapWasdal);
            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void setEventButtonMWasdalManfaat()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlManfaatRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlManfaatMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlManfaatPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlManfaatTutup);
        }

        private void nbiMonitoringWasdalManfaatBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlManfaatBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void mWlManfaatRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlManfaatBMN();
        }

        private void mWlManfaatMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Manfaat
            if (lapWasdal.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (lapWasdal.pemanfaatan1.modeLoadData == "normal" || lapWasdal.pemanfaatan1.modeLoadData == "cari")
                    lapWasdal.pemanfaatan1.dataInisial = false;

                lapWasdal.pemanfaatan1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlManfaatPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //lapWasdal.print("pemanfaatan");
            GetIniLapManfaat();
        }

        private void mWlManfaatTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }


        #endregion

        #region PEMINDAHTANGANAN BMN
        private void initGridMonWlPndhTgnBMN()
        {
            inisialisasiForm();

            this.setEventButtonMWasdalPndhTgn();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonPindahTgn);

            this.lapWasdal = new KKW.WL.ucLapWasdal(this);
            lapWasdal.strKdPelayanan = "AND ((KD_PELAYANAN = '11')OR(KD_PELAYANAN = '12')OR(KD_PELAYANAN = '13')OR(KD_PELAYANAN = '14'))";
            lapWasdal.namaModul = "Pemindatanganan BMN";
            this.setPanel(this.lapWasdal);
            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }


        private void setEventButtonMWasdalPndhTgn()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlPndhTgnRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlPndhTgnMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlPndhTgnPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlPndhTgnTutup);
        }

        private void nbiMonitoringWasdalPndhTgnBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlPndhTgnBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void mWlPndhTgnRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlPndhTgnBMN();
        }

        private void mWlPndhTgnMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Pemindahtangan
            if (lapWasdal.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (lapWasdal.pemindahtanganan1.modeLoadData == "normal" || lapWasdal.pemindahtanganan1.modeLoadData == "cari")
                    lapWasdal.pemindahtanganan1.dataInisial = false;

                lapWasdal.pemindahtanganan1.getTindakLanjut();
            }

            #endregion
        }

        private void mWlPndhTgnPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            puParameterLaporan puParam = new puParameterLaporan();
            puParam.ShowDialog();
            tahunAnggaran = puParam.thnAng;
            GetIniLapPemindahtanganan();
        }

        private void mWlPndhTgnTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region PENGHAPUSAN BMN
        private void initGridMonWlHapusBMN()
        {
            inisialisasiForm();

            this.setEventButtonMWasdalHapus();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonHapus);

            this.lapWasdal = new KKW.WL.ucLapWasdal(this);
            lapWasdal.strKdPelayanan = "AND ((KD_PELAYANAN = '15')OR(KD_PELAYANAN = '16')OR(KD_PELAYANAN = '17'))";
            lapWasdal.namaModul = "Penghapusan BMN";
            this.setPanel(this.lapWasdal);
            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", true);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void setEventButtonMWasdalHapus()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlHapusRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlHapusMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlHapusPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlHapusTutup);
        }

        private void nbiMonitoringWasdalHapusBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlHapusBMN();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void mWlHapusRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlHapusBMN();
        }

        private void mWlHapusMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Penghapusan
            if (lapWasdal.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (lapWasdal.penghapusan1.modeLoadData == "normal" || lapWasdal.penghapusan1.modeLoadData == "cari")
                    lapWasdal.penghapusan1.dataInisial = false;

                lapWasdal.penghapusan1.getTindakLanjut();
            }
            #endregion
        }

        private void mWlHapusPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            puParameterLaporan puParam = new puParameterLaporan();
            puParam.ShowDialog();
            tahunAnggaran = puParam.thnAng;
            GetIniLapPenghapusan();
        }

        private void mWlHapusTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        

        private void ShowGridPreview(GridControl grid)
        {
            // Check whether the GridControl can be previewed.
            if (!grid.IsPrintingAvailable)
            {
                MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error");
                return;
            }

            // Open the Preview window.
            grid.ShowPrintPreview();
        }

        #endregion

        #region Penertiban BMN


        #region -->> Setting Sifat Tombol
        private void setTombolPenertibanGrid()
        {
            bbiPenertibanTambah.Enabled = true;
            bbiPenertibanUbah.Enabled = true;
            bbiPenertibanHapus.Enabled = true;
            bbiPenertibanDetail.Enabled = true;
            bbiPenertibanKembali.Enabled = false;
            bbiPenertibanRefresh.Enabled = true;
            bbiPenertibanMoreData.Enabled = false;
        }

        private void setTombolPenertibanForm()
        {
            bbiPenertibanTambah.Enabled = false;
            bbiPenertibanUbah.Enabled = false;
            bbiPenertibanHapus.Enabled = false;
            bbiPenertibanDetail.Enabled = false;
            bbiPenertibanKembali.Enabled = true;
            bbiPenertibanRefresh.Enabled = false;
            bbiPenertibanMoreData.Enabled = false;
        }

        private void resetEventTombolPenertiban()
        {
            konfigApp.RemoveClickEvent(bbiPenertibanDetail);
            konfigApp.RemoveClickEvent(bbiPenertibanHapus);
            konfigApp.RemoveClickEvent(bbiPenertibanKembali);
            konfigApp.RemoveClickEvent(bbiPenertibanMoreData);
            konfigApp.RemoveClickEvent(bbiPenertibanRefresh);
            konfigApp.RemoveClickEvent(bbiPenertibanTambah);
            konfigApp.RemoveClickEvent(bbiPenertibanUbah);
        }
        #endregion

        private bool modeCariPenertiban = false;
        KKW.PENERTIBAN.ucPenertibanGrid gridPenertiban;
        KKW.PENERTIBAN.ucPenertibanForm formPenertibanTambah;
        KKW.PENERTIBAN.ucPenertibanForm formPenertibanUbah;
        KKW.PENERTIBAN.ucPenertibanForm formPenertibanDetail;
        private ArrayList dsGridPenertiban;
        // Sebelumnya
        //SvcWasdalPenertibanSelect.OutputParameters dOutPenertiban;
        //SvcWasdalPenertibanSelect.execute_pttClient ambilPenertiban;
        SvcMonPenertibanKoEsKl.OutputParameters dOutPenertiban;
        SvcMonPenertibanKoEsKl.selectMonPenertibanKorEsKl_pttClient ambilPenertiban;

        private void setEventTombolPenertiban()
        {
            resetEventTombolPenertiban();
            //bbiPenertibanTambah.ItemClick += new ItemClickEventHandler(bbiPenertibanTambahKlik);
            //bbiPenertibanUbah.ItemClick += new ItemClickEventHandler(bbiPenertibanUbahKlik);
            //bbiPenertibanHapus.ItemClick += new ItemClickEventHandler(bbiPenertibanHapusKlik);
            //bbiPenertibanDetail.ItemClick += new ItemClickEventHandler(bbiPenertibanDetailKlik);
            bbiPenertibanKembali.ItemClick += new ItemClickEventHandler(bbiPenertibanKembaliKlik);
            bbiPenertibanRefresh.ItemClick += new ItemClickEventHandler(bbiPenertibanRefreshKlik);
            bbiPenertibanMoreData.ItemClick += new ItemClickEventHandler(bbiPenertibanMoreDataKlik);
            bbiPenertibanKeluar.ItemClick += new ItemClickEventHandler(bbiPenertibanKeluarKlik);
        }

        private void initGridPenertiban()
        {
            if (gridPenertiban == null)
            {
                gridPenertiban = new KKW.PENERTIBAN.ucPenertibanGrid()
                {
                    cariDataOnline = new CariDataOnline(cariDataPenertiban),
                    //detailDataGrid = new DetailDataGrid(bbiPenertibanUbahKlik)
                };
            }
            setTombolPenertibanGrid();
            setEventTombolPenertiban();
            setPanel(gridPenertiban);
        }

        #region --++ Tombol Ribbon
        #region Button CRUD
        //private void bbiPenertibanTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{

        //    if (formPenertibanTambah == null)
        //    {
        //        formPenertibanTambah = new KKW.PENERTIBAN.ucPenertibanForm("C") 
        //        {
        //            toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
        //            //simpanDataPenertiban = new SimpanDataPenertiban(simpanDataPenertiban)
        //        };
        //    }
        //    formPenertibanTambah.inisialisasiForm();
        //    setPanel(formPenertibanTambah);
        //    setTombolPenertibanForm();

        //}

        //private void bbiPenertibanUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (gridPenertiban.dataTerpilih != null)
        //    {
        //        if (formPenertibanUbah == null)
        //        {
        //            formPenertibanUbah = new KKW.PENERTIBAN.ucPenertibanForm("U")
        //            {
        //                toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
        //                simpanDataPenertiban = new SimpanDataPenertiban(simpanDataPenertiban)
        //            };
        //        }

        //        formPenertibanUbah.dataTerpilih = gridPenertiban.dataTerpilih;
        //        setPanel(formPenertibanUbah);
        //        formPenertibanUbah.inisialisasiForm();
        //        setTombolPenertibanForm();
        //    }
        //    else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        //}

        //private void bbiPenertibanHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (gridPenertiban.dataTerpilih != null)
        //    {
        //        if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusPenertiban, gridPenertiban.dataTerpilih.ID_PENERTIBAN), konfigApp.judulKonfirmasi,
        //            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                myThread = new Thread(new ThreadStart(ShowProgresBar));
        //                myThread.Start();
        //                SvcWasdalPenertibanCud.InputParameters parInp = new SvcWasdalPenertibanCud.InputParameters();
        //                parInp.P_ID_PENERTIBAN = gridPenertiban.dataTerpilih.ID_PENERTIBAN;
        //                parInp.P_ID_PENERTIBANSpecified = true;
        //                parInp.P_ID_USER = gridPenertiban.dataTerpilih.ID_USER;
        //                parInp.P_ID_USERSpecified = true;
        //                parInp.P_ID_SATKER = gridPenertiban.dataTerpilih.ID_SATKER;
        //                parInp.P_BENTUK_PENERTIBAN = gridPenertiban.dataTerpilih.BENTUK_PENERTIBAN;
        //                parInp.P_ID_ASET = gridPenertiban.dataTerpilih.ID_ASET;
        //                parInp.P_ID_ASETSpecified = true;
        //                parInp.P_KD_BRG = gridPenertiban.dataTerpilih.KD_BRG;
        //                parInp.P_KD_SATKER = gridPenertiban.dataTerpilih.KD_SATKER;
        //                parInp.P_KET = gridPenertiban.dataTerpilih.KET;
        //                parInp.P_KUASA_PENGGUNA_BRG = gridPenertiban.dataTerpilih.KUASA_PENGGUNA_BRG;
        //                parInp.P_NM_PENGGUNA = gridPenertiban.dataTerpilih.NM_PENGGUNA;
        //                parInp.P_NOREG = gridPenertiban.dataTerpilih.NOREG;
        //                parInp.P_NUP = gridPenertiban.dataTerpilih.NUP.ToString();
        //                parInp.P_SELECT = "D";
        //                modeCrud = Convert.ToChar(parInp.P_SELECT);
        //                parInp.P_UR_SATKER = gridPenertiban.dataTerpilih.UR_SATKER;
        //                parInp.P_UR_SSKEL = gridPenertiban.dataTerpilih.UR_SSKEL;
        //                ambilDataPenertiban = new SvcWasdalPenertibanCud.execute_pttClient();
        //                ambilDataPenertiban.Open();
        //                ambilDataPenertiban.Beginexecute(parInp, new AsyncCallback(cudPenertiban), "");
        //            }
        //            catch
        //            {
        //                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        //                this.Invoke(new AktifkanForm(aktifkanForm), "");
        //                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
        //            }
        //        }
        //    }
        //}

        //private void bbiPenertibanDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (gridPenertiban.dataTerpilih != null)
        //    {
        //        if (formPenertibanDetail == null)
        //        {
        //            formPenertibanDetail = new KKW.PENERTIBAN.ucPenertibanForm("A");
        //            formPenertibanDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
        //        }
        //        formPenertibanDetail.dataTerpilih = gridPenertiban.dataTerpilih;
        //        setPanel(formPenertibanDetail);
        //        formPenertibanDetail.inisialisasiForm();
        //        setTombolPenertibanForm();
        //    }
        //    else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        //}
        #endregion

        private void bbiPenertibanKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridPenertiban);
            setTombolPenertibanGrid();
            gridPenertiban.dsDataSource = dsGridPenertiban;
            gridPenertiban.displayData();
        }

        private void bbiPenertibanRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridPenertiban.teNamaKolom.Text = "";
            gridPenertiban.teCari.Text = "";
            gridPenertiban.fieldDicari = "";
            gridPenertiban.dataInisial = true;
            this.dataInisial = true;
            this.getInitPenertiban();
        }

        private void bbiPenertibanMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitPenertiban();
            }
        }

        private void bbiPenertibanKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        #endregion --++ Tombol Ribbon

        private void nbiMonitoringWasdalPenertibanBMN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Penertiban BMN";
            konfigApp.strSubMenu = konfigApp.namaLayanan18;
            konfigApp.kdPelayanan = "02";
            konfigApp.namaPelayanan = konfigApp.namaLayanan18;
            this.Enabled = false;
            this.inisialisasiForm();
            rpPenertibanBMN.Visible = true;
            ribbon.SelectedPage = rpPenertibanBMN;
            initGridPenertiban();
            modeCariPenertiban = false;
            gridPenertiban.teNamaKolom.Text = "";
            gridPenertiban.teCari.Text = "";
            gridPenertiban.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitPenertiban();
        }


        #region --++ Ambil Data Penertiban
        private void getInitPenertiban()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcMonPenertibanKoEsKl.InputParameters parInp = new SvcMonPenertibanKoEsKl.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
                {
                    konfigApp.currentMaks = konfigApp.dataMaks;
                    konfigApp.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    konfigApp.currentMin = konfigApp.currentMaks + 1;
                    konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataMaks;
                }
                parInp.P_MAX = konfigApp.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = konfigApp.currentMin;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
                parInp.STR_WHERE = String.Format("ID_KORWIL = {0}", konfigApp.idUser, this.strCari);  //ini diganti jadi gini
                ambilPenertiban = new SvcMonPenertibanKoEsKl.selectMonPenertibanKorEsKl_pttClient();
                ambilPenertiban.Open();
                ambilPenertiban.Beginexecute(parInp, new AsyncCallback(getPenertiban), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getPenertiban(IAsyncResult result)
        {
            try
            {
                dOutPenertiban = ambilPenertiban.Endexecute(result);    //dari sini langsung ke catch
                ambilPenertiban.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsPenertiban(dsPenertiban), dOutPenertiban);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);   //disini
            }
        }

        private delegate void DsPenertiban(SvcMonPenertibanKoEsKl.OutputParameters dataOut);

        private void dsPenertiban(SvcMonPenertibanKoEsKl.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_MON_KOR_ES_KL_PENERTIBAN.Count();
            decimal jmlCurrent = 0;
            // Field TOTAL Data tidak ada
            //string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_MON_KOR_ES_KL_PENERTIBAN[jmlDataKl - 1].TOTAL_DATA.ToString();
            string totalData = "";
            if (jmlDataKl == konfigApp.dataMaks)
            {
                this.masihAdaData = true;
                bbiPenertibanMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataMaks;
            }
            else
            {
                if (jmlDataKl > konfigApp.dataMaks)
                    jmlCurrent = konfigApp.dataMaks;
                else
                    jmlCurrent = jmlDataKl;
                this.masihAdaData = false;
                bbiPenertibanMoreData.Enabled = false;
            }
            if (dataInisial == true)
            {
                dsGridPenertiban = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                //dataOut.SF_ROW_WASDAL_PENERTIBAN[i] = (dataOut.SF_ROW_WASDAL_PENERTIBAN[i] == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridPenertiban.Add(dataOut.SF_MON_KOR_ES_KL_PENERTIBAN[i]);
            }

            gridPenertiban.sbCariOnline.Enabled = !modeCari;
            gridPenertiban.dsDataSource = dsGridPenertiban;
            gridPenertiban.displayData();
            if (modeCariPenertiban == true)
            {
                string xSatu = gridPenertiban.teNamaKolom.Text.Trim();
                string xDua = gridPenertiban.teCari.Text.Trim();
                string xTiga = gridPenertiban.fieldDicari;
                gridPenertiban.gvGridPenertiban.ClearColumnsFilter();
                gridPenertiban.teNamaKolom.Text = xSatu;
                gridPenertiban.teCari.Text = xDua;
                gridPenertiban.fieldDicari = xTiga;
            }
            else
                gridPenertiban.gvGridPenertiban.ClearColumnsFilter();
            gridPenertiban.labelTotData.Text = "";
            gridPenertiban.labelTotData.Text = "Menampilkan " + jmlCurrent + " dari total " + totalData + " data";
        }

        private void cariDataPenertiban(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCariPenertiban = true;
            dataInisial = initModeCari;
            getInitPenertiban();
        }
        #endregion Ambil Penertiban

        #region --++ Simpan Data Penertiban
        //SvcWasdalPenertibanCud.OutputParameters dOutAmbilDataPenertiban;
        //SvcWasdalPenertibanCud.execute_pttClient ambilDataPenertiban;

        //private void simpanDataPenertiban(string _mode)
        //{

        //    try
        //    {
        //        myThread = new Thread(new ThreadStart(ShowProgresBar));
        //        myThread.Start();
        //        SvcWasdalPenertibanCud.InputParameters parInp = new SvcWasdalPenertibanCud.InputParameters();
        //        parInp.P_ID_SATKER = konfigApp.idSatker;
        //        parInp.P_ID_SATKERSpecified = (konfigApp.idSatker == 0 ? false : true);
        //        parInp.P_ID_PENERTIBAN = ((_mode == "C" || _mode == "CU") ? null : formPenertibanUbah._idPenertiban);
        //        parInp.P_ID_PENERTIBANSpecified = true;
        //        parInp.P_ID_USER = konfigApp.idUser;
        //        parInp.P_ID_USERSpecified = true;
        //        parInp.P_BENTUK_PENERTIBAN = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.cbJenisPenertiban.Text : formPenertibanUbah.cbJenisPenertiban.Text);
        //        parInp.P_KET = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.teKeterangan.Text : formPenertibanUbah.teKeterangan.Text);
        //        parInp.P_NUP = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.teUraianPenertiban.Text : formPenertibanUbah.teUraianPenertiban.Text);
        //        parInp.P_KUASA_PENGGUNA_BRG = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.KuasaPenggunaBarang : formPenertibanUbah.KuasaPenggunaBarang);
        //        parInp.P_UR_SATKER = ((_mode == "C" || _mode == "CU") ? konfigApp.namaSatker : konfigApp.namaSatker);
        //        parInp.P_KET = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.teKeterangan.Text : formPenertibanUbah.teKeterangan.Text);
        //        parInp.P_ID_ASET = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah._idAset : formPenertibanUbah._idAset);
        //        parInp.P_ID_ASETSpecified = true;
        //        parInp.P_ID_SATKER = konfigApp.idSatker;
        //        parInp.P_ID_SATKERSpecified = true;
        //        parInp.P_ID_USER = konfigApp.idUser;
        //        parInp.P_ID_USERSpecified = true;
        //        parInp.P_KD_BRG = ((_mode == "C" || _mode == "CU") ? konfigApp.kodebarang : konfigApp.kodebarang);
        //        parInp.P_KD_SATKER = konfigApp.kodeSatker;
        //        parInp.P_NM_PENGGUNA = konfigApp.namaPengguna;
        //        parInp.P_NOREG = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.noReg.ToString() : formPenertibanUbah.noReg.ToString());
        //        parInp.P_NUP = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.teNup.Text : formPenertibanUbah.teNup.Text);
        //        parInp.P_UR_SSKEL = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.teUraianBarang.Text : formPenertibanUbah.teUraianBarang.Text);

        //        parInp.P_URAIAN_PENERTIBAN_LAIN = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.teUraianPenertiban.Text : formPenertibanUbah.teUraianPenertiban.Text);
        //        parInp.P_BENTUK_PENERTIBAN = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.cbJenisPenertiban.Text : formPenertibanUbah.cbJenisPenertiban.Text);
        //        parInp.P_NO_SURAT_PENERTIBAN = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.teDsrNoSurat.Text : formPenertibanUbah.teDsrNoSurat.Text);
        //        //parInp.P_TGL_SURAT_PENERTIBAN = ((_mode == "C" || _mode == "CU") ? konfigApp.ToDate(formPenertibanTambah.deDsrTglSurat.Text) : konfigApp.ToDate(formPenertibanUbah.deDsrTglSurat.Text));
        //        parInp.P_DASAR_PENERTIBAN = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.cbDsrPenertiban.Text : formPenertibanUbah.cbDsrPenertiban.Text);
        //        parInp.P_NO_LAPORAN = ((_mode == "C" || _mode == "CU") ? formPenertibanTambah.teBtkTertibGunaNoSrt.Text : formPenertibanUbah.teBtkTertibGunaNoSrt.Text);
        //        //parInp.P_TGL_LAPORAN = ((_mode == "C" || _mode == "CU") ? konfigApp.ToDate(formPenertibanTambah.teBntkTertibGunaTglSrt.Text) : konfigApp.ToDate(formPenertibanUbah.teBntkTertibGunaTglSrt.Text));

        //        string _penggantiChar = (_mode == "CU" ? "U" : _mode);
        //        string _charSementara = (_mode == "CU" ? "Z" : _mode);
        //        parInp.P_SELECT = _penggantiChar;
        //        modeCrud = Convert.ToChar(_charSementara);
        //        ambilDataPenertiban = new SvcWasdalPenertibanCud.execute_pttClient();
        //        ambilDataPenertiban.Beginexecute(parInp, new AsyncCallback(cudPenertiban), "");
        //    }
        //    catch
        //    {
        //        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        //        this.Invoke(new AktifkanForm(aktifkanForm), "");
        //        MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
        //    }
        //}

        //private void cudPenertiban(IAsyncResult result)
        //{
        //    try
        //    {
        //        dOutAmbilDataPenertiban = ambilDataPenertiban.Endexecute(result);
        //        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        //        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
        //        this.Invoke(new UbahDsPenertiban(this.ubahDsPenertiban), dOutAmbilDataPenertiban);

        //    }
        //    catch
        //    {
        //        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        //        this.Invoke(new AktifkanForm(aktifkanForm), "");
        //        if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
        //        {
        //            konfigApp.teksDialog = konfigApp.teksGagalSimpan;
        //        }
        //        else if (this.modeCrud == 'D')
        //        {
        //            konfigApp.teksDialog = konfigApp.teksGagalHapus;
        //        }
        //        else
        //        {
        //            konfigApp.teksDialog = konfigApp.teksGagalLain;
        //        }
        //        MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
        //    }
        //}

        //private delegate void UbahDsPenertiban(SvcWasdalPenertibanCud.OutputParameters dataOutKpknlCrud);

        //int _keBerapa = 0;

        //private void ubahDsPenertiban(SvcWasdalPenertibanCud.OutputParameters dataOutKpknlCrud)
        //{
        //    if (dataOutKpknlCrud.PO_RESULT == "Y")  //disini masih N
        //    {
        //        int _indeksData = 0;
        //        SvcMonKorwilPenertibanA1.WASDALSROW_MON_KORWIL_PENERTIBAN dataPenyama = new SvcMonKorwilPenertibanA1.WASDALSROW_MON_KORWIL_PENERTIBAN();
        //        dataPenyama.ID_ASETSpecified = true;
        //        dataPenyama.ID_SATKERSpecified = true;
        //        dataPenyama.ID_USER = konfigApp.idUser;
        //        dataPenyama.ID_USERSpecified = true;
        //        dataPenyama.ID_PENERTIBANSpecified = true;
        //        dataPenyama.NUPSpecified = true;
        //        dataPenyama.TOTAL_DATASpecified = true;
        //        switch (modeCrud.ToString())
        //        {
        //            case "C":
        //            case "Z":
        //                dataPenyama.ID_SATKER = konfigApp.idSatker;
        //                dataPenyama.KD_SATKER = konfigApp.kodeSatker;
        //                dataPenyama.ID_USER = konfigApp.idUser;
        //                dataPenyama.BENTUK_PENERTIBAN = formPenertibanTambah.cbJenisPenertiban.Text;
        //                dataPenyama.KET = formPenertibanTambah.teKeterangan.Text;
        //                dataPenyama.UR_BENTUK_PENERTIBAN = formPenertibanTambah.teUraianPenertiban.Text;
        //                dataPenyama.KUASA_PENGGUNA_BRG = formPenertibanTambah.KuasaPenggunaBarang;
        //                dataPenyama.NM_PENGGUNA = konfigApp.namaPengguna;
        //                dataPenyama.NUM = (dsGridPenertiban == null ? 1 : dsGridPenertiban.Count + 1);
        //                dataPenyama.UR_SATKER = konfigApp.namaSatker;
        //                //dataPenyama.ID_ASET = formPenertibanTambah.idAset;
        //                dataPenyama.ID_ASETSpecified = true;
        //                dataPenyama.ID_PENERTIBAN = null; 
        //                dataPenyama.ID_PENERTIBANSpecified = true;
        //                dataPenyama.ID_SATKERSpecified = true;
        //                dataPenyama.ID_USERSpecified = true;
        //                dataPenyama.KD_BRG = formPenertibanTambah.teKodeBarang.Text;
        //                dataPenyama.NOREG = formPenertibanTambah.noReg;
        //                dataPenyama.NUMSpecified = true;
        //                dataPenyama.NUP = Convert.ToDecimal(konfigApp.nup);
        //                dataPenyama.NUPSpecified = true;
        //                dataPenyama.TOTAL_DATA = (dsGridPenertiban == null ? 1 : dsGridPenertiban.Count + 1);
        //                dataPenyama.TOTAL_DATASpecified = true;
        //                dataPenyama.UR_SSKEL = formPenertibanTambah.teUraianBarang.Text;
        //                //dataPenyama.UR_SSKEL = formPenertibanTambah.teUraianPenertiban.Text;

        //                dsGridPenertiban.Add(dataPenyama);
        //                if (modeCrud == 'C')
        //                {
        //                    gridPenertiban.dataTerpilih = dataPenyama;
        //                    _keBerapa = 1;
        //                    //formPenertibanTambah.gcDaftarAset.Enabled = true;
        //                    //formPenertibanTambah.sbTambah.Enabled = true;
        //                    //formPenertibanTambah.sbHapus.Enabled = true;
        //                    //formPenertibanTambah.rgJenisAset.Properties.ReadOnly = true;
        //                    //formPenertibanTambah.sbRefresh.Enabled = true;
        //                    //formPenertibanTambah.cePilihSemua.Enabled = true;
        //                    //formSkPspBmnTambah.statusForm = "CU";
        //                    formPenertibanTambah.sbSimpan.Enabled = true;
        //                }
        //                else if (modeCrud == 'Z')
        //                {
        //                    //if (_keBerapa == 1)
        //                    //{
        //                    //    _keBerapa++;
        //                    //    _indeksData = dsGridRskPspBmn.IndexOf(gridSkPspBmn.dataTerpilih);
        //                    //}
        //                    //else
        //                    //{
        //                    //    gridSkPspBmn.dataTerpilih = gridSkPspBmn.dataTerpilih;
        //                    //}
        //                    _indeksData = dsGridPenertiban.IndexOf(gridPenertiban.dataTerpilih);
        //                    _indeksData = (_indeksData < 0 ? 0 : _indeksData);
        //                    dsGridPenertiban.Remove(gridPenertiban.dataTerpilih);
        //                    dsGridPenertiban.Insert(_indeksData, dataPenyama);
        //                }
        //                break;
        //            case "U":
        //                dataPenyama.ID_SATKER = konfigApp.idSatker;
        //                dataPenyama.KD_SATKER = konfigApp.kodeSatker;
        //                dataPenyama.ID_USER = konfigApp.idUser;
        //                dataPenyama.BENTUK_PENERTIBAN = formPenertibanUbah.cbJenisPenertiban.Text; //udah di ubah
        //                dataPenyama.KET = formPenertibanUbah.Keterangan;
        //                //dataPenyama.UR_PENERTIBAN = formPenertibanTambah.teUraianPenertiban.Text;
        //                dataPenyama.KUASA_PENGGUNA_BRG = formPenertibanUbah.KuasaPenggunaBarang;
        //                dataPenyama.NM_PENGGUNA = konfigApp.namaPengguna;
        //                dataPenyama.NUM = (dsGridPenertiban == null ? 1 : dsGridPenertiban.Count + 1);
        //                dataPenyama.UR_SATKER = konfigApp.namaSatker;
        //                dataPenyama.ID_ASET = formPenertibanUbah.idAset;
        //                dataPenyama.ID_ASETSpecified = true;
        //                dataPenyama.ID_PENERTIBAN = null;
        //                dataPenyama.ID_PENERTIBANSpecified = true;
        //                dataPenyama.ID_SATKERSpecified = true;
        //                dataPenyama.ID_USERSpecified = true;
        //                dataPenyama.KD_BRG = formPenertibanUbah.teKodeBarang.Text;
        //                dataPenyama.NOREG = formPenertibanUbah.noReg;
        //                dataPenyama.NUMSpecified = true;
        //                dataPenyama.NUP = Convert.ToDecimal(konfigApp.nup);
        //                dataPenyama.NUPSpecified = true;
        //                dataPenyama.TOTAL_DATA = (dsGridPenertiban == null ? 1 : dsGridPenertiban.Count + 1);
        //                dataPenyama.TOTAL_DATASpecified = true;
        //                dataPenyama.UR_SSKEL = formPenertibanUbah.teUraianBarang.Text;
        //                _indeksData = dsGridPenertiban.IndexOf(gridPenertiban.dataTerpilih);
        //                _indeksData = (_indeksData < 0 ? 0 : _indeksData);
        //                dsGridPenertiban.Remove(gridPenertiban.dataTerpilih);
        //                dsGridPenertiban.Insert(_indeksData, dataPenyama);
        //                break;
        //            case "D":
        //                dsGridPenertiban.Remove(gridPenertiban.dataTerpilih);
        //                break;
        //        }
        //        gridPenertiban.dsDataSource = dsGridPenertiban;
        //        gridPenertiban.displayData();

        //        this.initGridPenertiban();

        //        //if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z') || (this.modeCrud == 'D'))
        //        //{
        //        //    initGridSkPspBmn();
        //        //}
        //    }
        //    else MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulKonfirmasi);
        //}
        #endregion Simpan Data Penertiban

        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpPenertibanBMN.Visible = false;
        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            puParameterLaporan puParam = new puParameterLaporan();
            puParam.ShowDialog();
            tahunAnggaran = puParam.thnAng;
            getInitLapPenertiban();
        }

        #endregion

        #region MONITORING WASDAL NEW
        string koneksiSqlServer = String.Format(@"Data Source=localhost\SQLBPSIMAN;Initial Catalog=LOCALBPSIMAN;User Id=sa;Password=SHTbpD*1");
        SqlConnection connection = null;
        KKW.WL.PENGGUNAAN.xrLapWasdalGuna xrlapGuna;
        string fileteks;
        FrmProgressBar frmProbar;
        
        #region Pengguna BMN
        private void initGridMonWlGunaBMNNew()
        {
            inisialisasiForm();
            this.setEventButtonMWasdalGunaMon();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonGuna);
            this.lapWasdal = new KKW.WL.ucLapWasdal(this);
            lapWasdal.strKdPelayanan = "AND ((KD_PELAYANAN = '02') OR (KD_PELAYANAN = '03') OR (KD_PELAYANAN = '04') OR (KD_PELAYANAN = '05'))";
            lapWasdal.namaModul = "Penggunaan BMN Monitoring";
            this.setPanel(this.lapWasdal);
            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", false);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void setEventButtonMWasdalGunaMon()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlGunaMonRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlGunaMonMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlGunaMonPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlGunaMonTutup);
        }

        private void mWlGunaMonRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlGunaBMNNew();
        }

        private void mWlGunaMonMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lapWasdal.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (lapWasdal.penggunaan2.modeLoadData == "normal" || lapWasdal.penggunaan1.modeLoadData == "cari")
                    lapWasdal.penggunaan2.dataInisial = false;

                lapWasdal.penggunaan2.getTindakLanjut();
            }
        }

        private void mWlGunaMonPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //puParameterLaporan puParam = new puParameterLaporan();
            //puParam.ShowDialog();
            //tahunAnggaran = puParam.thnAng;
            ////lapWasdal.print("penggunaan");
            //getInitWasdalBMNLap();
        }

        private void mWlGunaMonTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void nbiPenggunaanBMNMonWasdal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlGunaBMNNew();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        #endregion

        #region Pemanfaatan BMN
        private void initGridMonWlManfaatBMNNew()
        {
            inisialisasiForm();
            this.setEventButtonMWasdalManfaatMon();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonManfaat);
            this.lapWasdal = new KKW.WL.ucLapWasdal(this);
            lapWasdal.strKdPelayanan = "AND ((KD_PELAYANAN = '02') OR (KD_PELAYANAN = '03') OR (KD_PELAYANAN = '04') OR (KD_PELAYANAN = '05'))";
            lapWasdal.namaModul = "Pemanfaatan BMN Monitoring";
            this.setPanel(this.lapWasdal);
            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", false);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void setEventButtonMWasdalManfaatMon()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlManfaatMonRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlManfaatMonMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlManfaatMonPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlManfaatMonTutup);
        }

        private void mWlManfaatMonRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlManfaatBMNNew();
        }

        private void mWlManfaatMonMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lapWasdal.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (lapWasdal.pemanfaatan2.modeLoadData == "normal" || lapWasdal.pemanfaatan2.modeLoadData == "cari")
                    lapWasdal.pemanfaatan2.dataInisial = false;

                lapWasdal.pemanfaatan2.getTindakLanjut();
            }
        }

        private void mWlManfaatMonPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //puParameterLaporan puParam = new puParameterLaporan();
            //puParam.ShowDialog();
            //tahunAnggaran = puParam.thnAng;
            ////lapWasdal.print("penggunaan");
            //getInitWasdalBMNLap();
        }

        private void mWlManfaatMonTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        private void nbiPemanfaatanBMNMonWasdal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            initGridMonWlManfaatBMNNew();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        #endregion

        #region Pemindahtanganan BMN
   
        private void initGridMonWlPindahBMNNew()
        {
            inisialisasiForm();
            this.setEventButtonMWasdalPindahMon();
            this.setGridMonWasdal(this.menuMonWasdal, this.subMenuMonManfaat);
            this.lapWasdal = new KKW.WL.ucLapWasdal(this);
            lapWasdal.strKdPelayanan = "AND ((KD_PELAYANAN = '02') OR (KD_PELAYANAN = '03') OR (KD_PELAYANAN = '04') OR (KD_PELAYANAN = '05'))";
            lapWasdal.namaModul = "Pemindahtanganan BMN Monitoring";
            this.setPanel(this.lapWasdal);
            setEnabledButtonMWasdal("Refresh", true);
            setEnabledButtonMWasdal("More", true);
            setEnabledButtonMWasdal("Print", false);
            setEnabledButtonMWasdal("Tutup", true);
        }

        private void setEventButtonMWasdalPindahMon()
        {
            this.resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlPindahMonRefresh);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlPindahMonMore);
            this.bbiMWasdalPrint.ItemClick += new ItemClickEventHandler(this.mWlPindahMonPrint);
            this.bbiMWasdalTutup.ItemClick += new ItemClickEventHandler(this.mWlPindahMonTutup);
        }

        private void mWlPindahMonRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataInisial = true;
            initGridMonWlPindahBMNNew();
        }

        private void mWlPindahMonMore(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lapWasdal.xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (lapWasdal.pemindahtanganan2.modeLoadData == "normal" || lapWasdal.pemindahtanganan2.modeLoadData == "cari")
                    lapWasdal.pemindahtanganan2.dataInisial = false;

                lapWasdal.pemanfaatan2.getTindakLanjut();
            }
        }

        private void mWlPindahMonPrint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //puParameterLaporan puParam = new puParameterLaporan();
            //puParam.ShowDialog();
            //tahunAnggaran = puParam.thnAng;
            ////lapWasdal.print("penggunaan");
            //getInitWasdalBMNLap();
        }

        private void mWlPindahMonTutup(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        
        private void nbiPemindahtanganBMNMonWasdal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.initGridMonWlPindahBMNNew();
            bbiMWasdalPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        #endregion

        private void nbiPenertibanBMNMonWasdal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Penertiban BMN Monitoring";
            konfigApp.strSubMenu = konfigApp.namaLayanan18;
            konfigApp.kdPelayanan = "02";
            konfigApp.namaPelayanan = konfigApp.namaLayanan18;
            this.Enabled = false;
            this.inisialisasiForm();
            rpPenertibanBMN.Visible = true;
            ribbon.SelectedPage = rpPenertibanBMN;
            initGridPenertiban();
            modeCariPenertiban = false;
            gridPenertiban.teNamaKolom.Text = "";
            gridPenertiban.teCari.Text = "";
            gridPenertiban.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitPenertiban();
        }

        #region Pengiriman Laporan Wasdal
        int statusMonLapWasdal = 0;
        KKW.WL.MONLAPWASDAL.ucMONLAPWASDALGrid gridMONLAPWASDAL;
        KKW.WL.MONLAPWASDAL.ucMONLAPWASDALFormNew formMONLAPWASDALDetail;
        private ArrayList dsGridMONLAPWASDAL;
        SvcLapMonWasdalSelect.OutputParameters dOutMONLAPWASDAL;
        SvcLapMonWasdalSelect.execute_pttClient ambilMONLAPWASDAL;
        KKW.WL.MONLAPWASDAL.xrMonLapWasdal1 xrmonLapWasdal;
        public string _closeMode;
        string activeMenu = "P";
        private bool modeCariMONLAPWASDAL = false;

        private void mainLapMonWasdal()
        {
            konfigApp.strMenu = "Laporan Wasdal";
            konfigApp.strSubMenu = "Monitoring Laporan Wasdal";
            konfigApp.kdPelayanan = "";
            konfigApp.namaPelayanan = "";
            this.Enabled = false;
            this.inisialisasiForm();
            rpPenertibanBMN.Visible = true;
            ribbon.SelectedPage = rpPenertibanBMN;
            rpgPenertibanBMN.Text = this.subMenuLapWasdal;
            rpPenertibanBMN.Text = this.subMenuLapWasdal;
            initGridMONLAPWASDAL_RPMK();
            modeCariMONLAPWASDAL = false;
            gridMONLAPWASDAL.teNamaKolom.Text = "";
            gridMONLAPWASDAL.teCari.Text = "";
            gridMONLAPWASDAL.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitMONLAPWASDAL_RPMK();
            activeMenu = "LW";
        }

        private void nbiKirimLapWasdal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            mainLapMonWasdal();
        }

        private void setTombolMONLAPWASDALGrid()
        {
            bbiPenertibanTambah.Enabled = true;
            bbiPenertibanUbah.Enabled = true;
            bbiPenertibanHapus.Enabled = true;
            bbiPenertibanDetail.Enabled = true;
            bbiPenertibanKembali.Enabled = false;
            bbiPenertibanRefresh.Enabled = true;
            bbiPenertibanMoreData.Enabled = false;
            barButtonItem10.Enabled = true;
        }

        private void setTombolMONLAPWASDALForm()
        {
            bbiPenertibanTambah.Enabled = false;
            bbiPenertibanUbah.Enabled = false;
            bbiPenertibanHapus.Enabled = false;
            bbiPenertibanDetail.Enabled = false;
            bbiPenertibanKembali.Enabled = true;
            bbiPenertibanRefresh.Enabled = false;
            bbiPenertibanMoreData.Enabled = false;
            barButtonItem10.Enabled = false;
        }

        private void setEventTombolMONLAPWASDAL_RPMK()
        {
            resetEventTombolPenertiban();
            bbiPenertibanTambah.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALTambahKlik_RPMK);
            bbiPenertibanUbah.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALUbahKlik_RPMK);
            bbiPenertibanHapus.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALHapusKlik_RPMK);
            bbiPenertibanDetail.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALDetailKlik_RPMK);
            bbiPenertibanKembali.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALKembaliKlik_RPMK);
            bbiPenertibanRefresh.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALRefreshKlik_RPMK);
            barButtonItem10.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALPRINT_ItemClick);
            bbiPenertibanMoreData.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALMoreDataKlik_RPMK);
            bbiPenertibanKeluar.ItemClick += new ItemClickEventHandler(bbiMONLAPWASDALKeluarKlik_RPMK);

            BbiPenertibanExcel.Visibility = BarItemVisibility.Never;
            barButtonItem10.Visibility = BarItemVisibility.Always;

        }

        private void initGridMONLAPWASDAL_RPMK()
        {
            statusMonLapWasdal = 0;
            if (gridMONLAPWASDAL == null)
            {
                gridMONLAPWASDAL = new KKW.WL.MONLAPWASDAL.ucMONLAPWASDALGrid()
                {
                    cariDataOnline = new CariDataOnline(cariDataMONLAPWASDAL_RPMK),
                    detailDataGrid = new DetailDataGrid(bbiMONLAPWASDALUbahKlik_RPMK),
                    detailSanksi = new DetailDataSanksi(initMonLapWasdalSanksiGrid),
                    detailCatatan = new DetailDataCatatan(initLaporanWasdalCatatan),
                    toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                    aktifkanForm = new KKW.WL.MONLAPWASDAL.ucMONLAPWASDALGrid.AktifkanForm(this.aktifkanForm)

                };
            }
            setTombolMONLAPWASDALGrid();
            setEventTombolMONLAPWASDAL_RPMK();
            barButtonItem10.Visibility = BarItemVisibility.Never;
            setPanel(gridMONLAPWASDAL);
        }

        #region --++ Tombol Ribbon
        private void bbiMONLAPWASDALTambahKlik_RPMK(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //if (formMONLAPWASDALTambah == null)
            //{
            //    formMONLAPWASDALTambah = new KSK.WL.MONLAPWASDAL.ucMONLAPWASDALFormNew("C")
            //    {
            //        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
            //        simpanDataMONLAPWASDAL = new SimpanDataMonLapWasdal(simpanDataMONLAPWASDAL_RPMK),
            //        aktifkanForm = new KSK.WL.MONLAPWASDAL.ucMONLAPWASDALFormNew.AktifkanForm(this.aktifkanForm),
            //    };
            //}
            //formMONLAPWASDALTambah.statusForm = "C";
            //formMONLAPWASDALTambah.inisialisasiForm();
            //setPanel(formMONLAPWASDALTambah);
            //setTombolMONLAPWASDALForm();

        }

        private void bbiMONLAPWASDALUbahKlik_RPMK(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridMONLAPWASDAL.dataTerpilih != null)
            //{
            //    if (formMONLAPWASDALUbah == null)
            //    {
            //        formMONLAPWASDALUbah = new KSK.WL.MONLAPWASDAL.ucMONLAPWASDALFormNew("U")
            //        {
            //            toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
            //            simpanDataMONLAPWASDAL = new SimpanDataMonLapWasdal(simpanDataMONLAPWASDAL_RPMK),
            //            aktifkanForm = new KSK.WL.MONLAPWASDAL.ucMONLAPWASDALFormNew.AktifkanForm(this.aktifkanForm),
            //        };
            //    }
            //    formMONLAPWASDALUbah.statusForm = "U";
            //    formMONLAPWASDALUbah.dataTerpilih = gridMONLAPWASDAL.dataTerpilih;
            //    formMONLAPWASDALUbah._idMONLAPWASDAL = gridMONLAPWASDAL.dataTerpilih.ID_MON_LAP;
            //    setPanel(formMONLAPWASDALUbah);
            //    formMONLAPWASDALUbah.inisialisasiForm();
            //    setTombolMONLAPWASDALForm();
            //}
            //else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiMONLAPWASDALHapusKlik_RPMK(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridMONLAPWASDAL.dataTerpilih != null)
            //{
            //    //if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusMONLAPWASDAL, gridMONLAPWASDAL.dataTerpilih.), konfigApp.judulKonfirmasi,
            //    //MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusMonLapWasdal, gridMONLAPWASDAL.dataTerpilih.NO_SURAT), konfigApp.judulKonfirmasi,
            //        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        try
            //        {
            //            myThread = new Thread(new ThreadStart(ShowProgresBar));
            //            myThread.Start();
            //            SvcLapMonWasdalCud.InputParameters parInp = new SvcLapMonWasdalCud.InputParameters();
            //            parInp.P_ID_MON_LAP = gridMONLAPWASDAL.dataTerpilih.ID_MON_LAP;
            //            parInp.P_ID_MON_LAPSpecified = true;
            //            parInp.P_ID_SATKER = konfigApp.idSatker;
            //            parInp.P_ID_SATKERSpecified = (konfigApp.idSatker == 0 ? false : true);
            //            parInp.P_KD_SATKER = konfigApp.kodeSatker;
            //            parInp.P_UR_SATKER = konfigApp.namaSatker;

            //            parInp.P_SELECT = "D";
            //            modeCrudUpload = "D";
            //            ambilDataMONLAPWASDAL = new SvcLapMonWasdalCud.execute_pttClient();
            //            ambilDataMONLAPWASDAL.Open();
            //            ambilDataMONLAPWASDAL.Beginexecute(parInp, new AsyncCallback(cudMONLAPWASDAL_RPMK), "");
            //        }
            //        catch
            //        {
            //            this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
            //            this.Invoke(new AktifkanForm(aktifkanForm), "");
            //            MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            //        }
            //    }
            //}
        }

        private void bbiMONLAPWASDALDetailKlik_RPMK(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridMONLAPWASDAL.dataTerpilih != null)
            {
                if (formMONLAPWASDALDetail == null)
                {
                    formMONLAPWASDALDetail = new KKW.WL.MONLAPWASDAL.ucMONLAPWASDALFormNew("A");
                    formMONLAPWASDALDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                    formMONLAPWASDALDetail.aktifkanForm = new KKW.WL.MONLAPWASDAL.ucMONLAPWASDALFormNew.AktifkanForm(this.aktifkanForm);
                }
                formMONLAPWASDALDetail.statusForm = "A";
                formMONLAPWASDALDetail.dataTerpilih = gridMONLAPWASDAL.dataTerpilih;
                setPanel(formMONLAPWASDALDetail);
                formMONLAPWASDALDetail.inisialisasiForm();
                setTombolMONLAPWASDALForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiMONLAPWASDALKembaliKlik_RPMK(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridMONLAPWASDAL);
            setTombolMONLAPWASDALGrid();
            gridMONLAPWASDAL.dsDataSource = dsGridMONLAPWASDAL;
            gridMONLAPWASDAL.displayData();
        }

        private void bbiMONLAPWASDALRefreshKlik_RPMK(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (statusMonLapWasdal == 0) // grid pemantauan laporan wasdal
            {
                strCari = "";
                modeCari = false;
                gridMONLAPWASDAL.teNamaKolom.Text = "";
                gridMONLAPWASDAL.teCari.Text = "";
                gridMONLAPWASDAL.fieldDicari = "";
                gridMONLAPWASDAL.dataInisial = true;
                this.dataInisial = true;
                this.getInitMONLAPWASDAL_RPMK();
            }
            else if (statusMonLapWasdal == 1) // grid laporan wasdal sanksi
            {
                this.dataInisial = true;
                this.GetDataSanksi("");
            }
            else if (statusMonLapWasdal == 2) // grid laporan wasdal catatan
            {
                this.dataInisial = true;
                this.GetDataCatatan("");
            }

        }

        private void bbiMONLAPWASDALMoreDataKlik_RPMK(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (statusMonLapWasdal == 0) // grid pemantauan laporan wasdal
            {
                if (this.masihAdaData == true)
                {
                    this.dataInisial = false;
                    this.getInitMONLAPWASDAL_RPMK();
                }
            }
            else if (statusMonLapWasdal == 1) // grid laporan wasdal sanksi
            {
                if (this.moreSanksi == true)
                {
                    this.dataInisial = false;
                    this.GetDataSanksi("");
                }
            }
            else if (statusMonLapWasdal == 2) // grid laporan wasdal catatan
            {
                if (this.moreCatatan == true)
                {
                    this.dataInisial = false;
                    this.GetDataCatatan("");
                }
            }
        }

        private void bbiMONLAPWASDALKeluarKlik_RPMK(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiMONLAPWASDALPRINT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            puParameterLaporan puParam = new puParameterLaporan();
            puParam.ShowDialog();
            tahunAnggaran = puParam.thnAng;
            this._closeMode = puParam._closeMode;
            if (this._closeMode == "Y")
            {

                if (activeMenu == "LW")
                {
                    if (pernahUpdateMONLAPWASDAL())
                    {
                        DialogResult dialogResult = MessageBox.Show("Data sudah tersedia di komputer lokal. Apakah Anda ingin mengambil Data terbaru dari Server?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            getInitMonLapWasdal();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            this.dataLokalMonLapWasdalAda(); //data lokal
                        }
                    }
                    else
                    {
                        getInitMonLapWasdal(); //ambil data lagi dari server
                    }
                }
                else
                {
                    // Mencetak Laporan Wasdal
                    //getInitLaporanWasdalRpt();
                    getInitMonLapWasdal();
                }

            }
        }

        #region Init Lap Data Monitoring Lap Wasdal

        #region Cek Data Lokal
        private bool pernahUpdateMONLAPWASDAL()
        {
            SqlConnection connection = null;
            bool pernahApdet = false;
            DataTable dtData = new DataTable();
            using (connection = new SqlConnection(koneksiSqlServer))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM RPT_MON_LAP_WASDAL";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                    pernahApdet = true;
            }
            return pernahApdet;
        }
        #endregion cek data lokal

        #region Get Data Lokal
        private void dataLokalMonLapWasdalAda()
        {
            ReportPrintTool pt;
            try
            {
                DataTable dtMonLapWasdal = new DataTable();
                xrmonLapWasdal = null;
                if (xrmonLapWasdal == null)
                    xrmonLapWasdal = new KKW.WL.MONLAPWASDAL.xrMonLapWasdal1();
                using (connection = new SqlConnection(koneksiSqlServer))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT NUM,ID_MON_LAP,ID_SATKER,KD_SATKER,UR_SATKER,ID_KL ,KD_KL,UR_KL,ID_KPKNL,KD_KPKNL,UR_KPKNL,ID_KANWIL,KD_KANWIL, UR_KANWIL, TGL_REKAM,THN_ANG, NO_SURAT,TGL_SURAT,STATUS_KIRIM,TGL_KIRIM,TGL_DITERIMA,IS_TEPAT_WAKTU,JML_SANKSI,JML_CAT,TOTAL_DATA FROM RPT_MON_LAP_WASDAL";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dtMonLapWasdal);

                    DataTable DTMonLapWasdal = new DataTable("DTMonLapWasdal");
                    DTMonLapWasdal.Columns.Add("NUM", typeof(string));
                    DTMonLapWasdal.Columns.Add("ID_MON_LAP", typeof(string));
                    DTMonLapWasdal.Columns.Add("ID_SATKER", typeof(string));
                    DTMonLapWasdal.Columns.Add("KD_SATKER", typeof(string));
                    DTMonLapWasdal.Columns.Add("UR_SATKER", typeof(string));//4
                    DTMonLapWasdal.Columns.Add("ID_KL", typeof(string));
                    DTMonLapWasdal.Columns.Add("KD_KL", typeof(string));
                    DTMonLapWasdal.Columns.Add("UR_KL", typeof(string));
                    DTMonLapWasdal.Columns.Add("ID_KPKNL", typeof(string));
                    DTMonLapWasdal.Columns.Add("KD_KPKNL", typeof(string));
                    DTMonLapWasdal.Columns.Add("UR_KPKNL", typeof(string));//10
                    DTMonLapWasdal.Columns.Add("ID_KANWIL", typeof(string));
                    DTMonLapWasdal.Columns.Add("KD_KANWIL", typeof(string));
                    DTMonLapWasdal.Columns.Add("UR_KANWIL", typeof(string));
                    DTMonLapWasdal.Columns.Add("TGL_REKAM", typeof(string));//14
                    DTMonLapWasdal.Columns.Add("THN_ANG", typeof(string));
                    DTMonLapWasdal.Columns.Add("NO_SURAT", typeof(string));
                    DTMonLapWasdal.Columns.Add("TGL_SURAT", typeof(string));//17
                    DTMonLapWasdal.Columns.Add("STATUS_KIRIM", typeof(string));
                    DTMonLapWasdal.Columns.Add("TGL_KIRIM", typeof(string));//19
                    DTMonLapWasdal.Columns.Add("TGL_DITERIMA", typeof(string));//20
                    DTMonLapWasdal.Columns.Add("IS_TEPAT_WAKTU", typeof(string));
                    DTMonLapWasdal.Columns.Add("JML_SANKSI", typeof(string));//22
                    DTMonLapWasdal.Columns.Add("JML_CAT", typeof(string));//23
                    DTMonLapWasdal.Columns.Add("TOTAL_DATA", typeof(string));//24

                    if (dtMonLapWasdal.Rows.Count > 0)
                    {
                        int _no = 1;
                        foreach (DataRow dr in dtMonLapWasdal.Rows)
                        {
                            string tglRekam = (Convert.ToDateTime(dr[14]).Date.ToString("dd/MM/yyyy") == "11/11/2000" || Convert.ToDateTime(dr[14]).Date.ToString("dd/MM/yyyy") == "11/11/2011" || Convert.ToDateTime(dr[14]).Date.ToString("dd/MM/yyyy") == "11/11/1111" || Convert.ToDateTime(dr[14]).Date.ToString("dd/MM/yyyy") == "11/11/1000" || Convert.ToDateTime(dr[14]).Date.ToString("dd/MM/yyyy") == "1/1/0001" || Convert.ToDateTime(dr[14]).Date.ToString("dd/MM/yyyy") == "01/01/0001") ? "-" : Convert.ToDateTime(dr[14]).Date.ToString("dd/MM/yyyy");
                            string tglSrt = (Convert.ToDateTime(dr[17]).Date.ToString("dd/MM/yyyy") == "11/11/2000" || Convert.ToDateTime(dr[17]).Date.ToString("dd/MM/yyyy") == "11/11/2011" || Convert.ToDateTime(dr[17]).Date.ToString("dd/MM/yyyy") == "11/11/1111" || Convert.ToDateTime(dr[17]).Date.ToString("dd/MM/yyyy") == "11/11/1000" || Convert.ToDateTime(dr[17]).Date.ToString("dd/MM/yyyy") == "1/1/0001" || Convert.ToDateTime(dr[17]).Date.ToString("dd/MM/yyyy") == "01/01/0001") ? "-" : Convert.ToDateTime(dr[17]).Date.ToString("dd/MM/yyyy");
                            string tglKirim = (Convert.ToDateTime(dr[19]).Date.ToString("dd/MM/yyyy") == "11/11/2000" || Convert.ToDateTime(dr[19]).Date.ToString("dd/MM/yyyy") == "11/11/2011" || Convert.ToDateTime(dr[19]).Date.ToString("dd/MM/yyyy") == "11/11/1111" || Convert.ToDateTime(dr[19]).Date.ToString("dd/MM/yyyy") == "11/11/1000" || Convert.ToDateTime(dr[19]).Date.ToString("dd/MM/yyyy") == "1/1/0001" || Convert.ToDateTime(dr[19]).Date.ToString("dd/MM/yyyy") == "01/01/0001") ? "-" : Convert.ToDateTime(dr[19]).Date.ToString("dd/MM/yyyy");
                            string tglDiterima = (Convert.ToDateTime(dr[20]).Date.ToString("dd/MM/yyyy") == "11/11/2000" || Convert.ToDateTime(dr[20]).Date.ToString("dd/MM/yyyy") == "11/11/2011" || Convert.ToDateTime(dr[20]).Date.ToString("dd/MM/yyyy") == "11/11/1111" || Convert.ToDateTime(dr[20]).Date.ToString("dd/MM/yyyy") == "11/11/1000" || Convert.ToDateTime(dr[20]).Date.ToString("dd/MM/yyyy") == "1/1/0001" || Convert.ToDateTime(dr[20]).Date.ToString("dd/MM/yyyy") == "01/01/0001") ? "-" : Convert.ToDateTime(dr[20]).Date.ToString("dd/MM/yyyy");
                            decimal jmlSanksi = Convert.ToDecimal(dr[22]);
                            decimal jmlCat = Convert.ToDecimal(dr[23]);
                            decimal jmlTotData = Convert.ToDecimal(dr[24]);
                            DTMonLapWasdal.Rows.Add(Convert.ToString(dr[0]), Convert.ToString(dr[1]), Convert.ToString(dr[2]), Convert.ToString(dr[3]), Convert.ToString(dr[4]), Convert.ToString(dr[5]), Convert.ToString(dr[6]), Convert.ToString(dr[7]), Convert.ToString(dr[8]), Convert.ToString(dr[9]), Convert.ToString(dr[10]), Convert.ToString(dr[11]), Convert.ToString(dr[12]), Convert.ToString(dr[13]), tglRekam, Convert.ToString(dr[15]), Convert.ToString(dr[16]), tglSrt, Convert.ToString(dr[18]), tglKirim, tglDiterima, Convert.ToString(dr[21]), jmlSanksi.ToString("n0"), jmlCat.ToString("n0"), jmlTotData.ToString("n0"));
                            _no++;
                        }
                        xrmonLapWasdal.DataSource = DTMonLapWasdal;
                        xrmonLapWasdal.DataMember = "DTMonLapWasdal";
                    }
                }
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulKonfirmasi);
            }
            finally
            {
                connection.Close();
            }



            //konfigApp.setTahunAnggaran();
            Parameter param = new Parameter();
            param.Name = "tahunAnggaran";
            param.Type = typeof(System.String);
            param.Value = tahunAnggaran;
            param.Description = "";
            param.Visible = false;
            pt = new ReportPrintTool(xrmonLapWasdal);
            pt.AutoShowParametersPanel = false;
            pt.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            pt.ShowPreviewDialog();

        }
        #endregion Get Data Lokal

        #region Get Data From Server
        SvcGnrtLapMonWasdal.execute_pttClient ambilGnrtMonLapWasdal;
        SvcGnrtLapMonWasdal.OutputParameters outputGnrtMonLapWasdal;
        private void getInitMonLapWasdal()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcGnrtLapMonWasdal.InputParameters parInp = new SvcGnrtLapMonWasdal.InputParameters();
                parInp.P_COL = "";
                parInp.P_MAX = 0;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
                parInp.P_COUNT = "N";
                //parInp.STR_WHERE = String.Format("ID_USER = {0} AND THN_ANG='{1}' {2}", konfigApp.idUser, (konfigApp.tahunAnggaran - 1), this.strCari);  //ini diganti jadi gini
                parInp.STR_WHERE = String.Format("KD_KORWIL LIKE '{0}%' AND THN_ANG='{1}' {2}", konfigApp.kodeKorwil.Substring(0, 15), (tahunAnggaran), this.strCari);  //ini diganti jadi gini                
                ambilGnrtMonLapWasdal = new SvcGnrtLapMonWasdal.execute_pttClient();
                ambilGnrtMonLapWasdal.Open();
                ambilGnrtMonLapWasdal.Beginexecute(parInp, new AsyncCallback(getMonLapWasdal), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getMonLapWasdal(IAsyncResult result)
        {
            try
            {
                outputGnrtMonLapWasdal = ambilGnrtMonLapWasdal.Endexecute(result);    //dari sini langsung ke catch
                ambilGnrtMonLapWasdal.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsMonLapWasdal(dsMonLapWasdal), outputGnrtMonLapWasdal);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);   //disini
            }
        }

        private delegate void DsMonLapWasdal(SvcGnrtLapMonWasdal.OutputParameters dataOut);

        private void dsMonLapWasdal(SvcGnrtLapMonWasdal.OutputParameters dataOut)
        {
            /*laporan untuk pengembangan*/
            ReportPrintTool pt;
            string namaFileDownload = String.Format("MonLapWasdal.zip");
            string lokasiDiFile = String.Format("{0}\\Penggunaan\\{1}", System.IO.Directory.GetCurrentDirectory(), namaFileDownload);
            string FolderFile = String.Format("{0}\\Penggunaan", System.IO.Directory.GetCurrentDirectory());
            if (Directory.Exists(FolderFile))
            {
                System.IO.File.WriteAllBytes(lokasiDiFile, dataOut.PO_FILE_BLOB);
            }
            else
            {
                Directory.CreateDirectory(FolderFile);
                System.IO.File.WriteAllBytes(lokasiDiFile, dataOut.PO_FILE_BLOB);
            }

            DataLokal dl = new DataLokal();
            dl.pilihLaporan("monLapWasdal");
            this.dataLokalMonLapWasdalAda();

            //xrPenertiban xrTertib = new xrPenertiban();
            //xrTertib.bsLapPenertiban.DataSource = DataLokal.dt;


            ////ReportPrintTool pt;
            ////xrPenertiban1 xrTertib = new xrPenertiban1();
            ////xrTertib.bsLapPenertiban.DataSource = dataOut.SF_ROW_WASDAL_PENERTIBAN;

            //konfigApp.setTahunAnggaran();
            //Parameter param = new Parameter();
            //param.Name = "tahunAnggaran";
            //param.Type = typeof(System.String);
            ////param.Value = konfigApp.tahunAnggaran-1;
            //param.Value = tahunAnggaran;
            //param.Description = "";
            //param.Visible = false;

            //xrTertib.Parameters.Add(param);

            //#region setting margin, paperkind
            //DevExpress.XtraPrinting.Preview.PrintPreviewFormEx form = new DevExpress.XtraPrinting.Preview.PrintPreviewFormEx();
            //xrTertib.PaperKind = System.Drawing.Printing.PaperKind.A4; //paperkind
            //xrTertib.Margins = new System.Drawing.Printing.Margins(75, 75, 75, 75); //margin
            //form.PrintingSystem = xrTertib.PrintingSystem;
            //xrTertib.CreateDocument();
            //form.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Scale, new object[] { 1 }); //autofit
            //form.ShowDialog();
            //#endregion

            //fToggleProgressBar("stop");
        }
        #endregion Get Data From Server

        #endregion

        #endregion --++ Tombol Ribbon

        #region --++ Ambil Data MONLAPWASDAL
        private void getInitMONLAPWASDAL_RPMK()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcLapMonWasdalSelect.InputParameters parInp = new SvcLapMonWasdalSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = String.Format("ID_KORWIL = {0} {1}", konfigApp.idKorwil, this.strCari);  //ini diganti jadi gini
                ambilMONLAPWASDAL = new SvcLapMonWasdalSelect.execute_pttClient();
                ambilMONLAPWASDAL.Open();
                ambilMONLAPWASDAL.Beginexecute(parInp, new AsyncCallback(getMONLAPWASDAL_RPMK), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getMONLAPWASDAL_RPMK(IAsyncResult result)
        {
            try
            {
                dOutMONLAPWASDAL = ambilMONLAPWASDAL.Endexecute(result);    //dari sini langsung ke catch
                ambilMONLAPWASDAL.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsMONLAPWASDAL_RPMK(dsMONLAPWASDAL_RPMK), dOutMONLAPWASDAL);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);   //disini
            }
        }

        private delegate void DsMONLAPWASDAL_RPMK(SvcLapMonWasdalSelect.OutputParameters dataOut);

        private void dsMONLAPWASDAL_RPMK(SvcLapMonWasdalSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_MON_LAP_WASDAL.Count();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiPenertibanMoreData.Enabled = true;
            }
            else
            {
                this.masihAdaData = false;
                bbiPenertibanMoreData.Enabled = false;
            }
            if (dataInisial == true)
            {
                dsGridMONLAPWASDAL = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                //dataOut.SF_ROW_WASDAL_PENERTIBAN[i] = (dataOut.SF_ROW_WASDAL_PENERTIBAN[i] == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridMONLAPWASDAL.Add(dataOut.SF_MON_LAP_WASDAL[i]);
            }


            gridMONLAPWASDAL.sbCariOnline.Enabled = !modeCari;
            gridMONLAPWASDAL.dsDataSource = dsGridMONLAPWASDAL;
            gridMONLAPWASDAL.displayData();
            if (modeCariMONLAPWASDAL == true)
            {
                string xSatu = gridMONLAPWASDAL.teNamaKolom.Text.Trim();
                string xDua = gridMONLAPWASDAL.teCari.Text.Trim();
                string xTiga = gridMONLAPWASDAL.fieldDicari;
                gridMONLAPWASDAL.gvGridMONLAPWASDAL.ClearColumnsFilter();
                gridMONLAPWASDAL.teNamaKolom.Text = xSatu;
                gridMONLAPWASDAL.teCari.Text = xDua;
                gridMONLAPWASDAL.fieldDicari = xTiga;
            }
            else
                gridMONLAPWASDAL.gvGridMONLAPWASDAL.ClearColumnsFilter();
        }

        private void cariDataMONLAPWASDAL_RPMK(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCariMONLAPWASDAL = true;
            dataInisial = initModeCari;
            getInitMONLAPWASDAL_RPMK();
        }
        #endregion Ambil Mon laporan wasdal

        #region MONLAPWASDAL - SANKSI

        #region Grid Sanksi

        private void setTombolSanksiGrid()
        {
            bbiPenertibanTambah.Enabled = false;
            bbiPenertibanUbah.Enabled = false;
            bbiPenertibanHapus.Enabled = false;
            bbiPenertibanDetail.Enabled = false;
            bbiPenertibanKembali.Enabled = false;
            barButtonItem10.Enabled = false;
        }

        KKW.WL.MONLAPWASDAL.ucLapMonSatkerGridSanksi ucSanksiGrid;

        public void initMonLapWasdalSanksiGrid(string kode, string label)
        {
            setTombolSanksiGrid();
            barButtonItem10.Visibility = BarItemVisibility.Never;
            kodeSanksi = kode;
            ucSanksiGrid = new KKW.WL.MONLAPWASDAL.ucLapMonSatkerGridSanksi(kode, label);
            ucSanksiGrid.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            ucSanksiGrid.back = new backLapMonSatker(this.mainLapMonWasdal);
            ucSanksiGrid.rowDataTerpilih = gridMONLAPWASDAL.dataTerpilih;
            dataInisial = true;
            GetDataSanksi("");
            setPanel(ucSanksiGrid);
        }

        #region --++ Load Data Sanksi Grid
        string kodeSanksi = "";
        SvcLapMonWasdalSanksiSelect.execute_pttClient client;
        SvcLapMonWasdalSanksiSelect.InputParameters input;
        SvcLapMonWasdalSanksiSelect.OutputParameters output;

        int maxDataSanksi = 0;
        ArrayList dataSanksi;
        bool moreSanksi = true;
        public void GetDataSanksi(string where)
        {
            this.Enabled = false;

            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();

                input = new SvcLapMonWasdalSanksiSelect.InputParameters();
                if (dataInisial)
                {
                    input.P_MIN = konfigApp.dataAwal;
                    input.P_MAX = konfigApp.dataAkhir;
                }
                else
                {
                    input.P_MIN = maxDataSanksi + 1;
                    input.P_MAX = maxDataSanksi + konfigApp.dataAkhir;
                }
                input.P_MINSpecified = true;
                input.P_MAXSpecified = true;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = "ID_MON_LAP = " + kodeSanksi;

                client = new SvcLapMonWasdalSanksiSelect.execute_pttClient();
                client.Open();
                client.Beginexecute(input, new AsyncCallback(getResultSanksi), null);

            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);

            }


        }

        private void getResultSanksi(IAsyncResult result)
        {
            try
            {
                output = client.Endexecute(result);
                client.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataSanksi(this.showDataGridSanksi), output);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);

            }
        }

        private delegate void ShowDataSanksi(SvcLapMonWasdalSanksiSelect.OutputParameters output);

        private void showDataGridSanksi(SvcLapMonWasdalSanksiSelect.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_LAP_WASDAL_SANKSI.Length;
                if (jmlData == konfigApp.dataAkhir)
                {
                    // set tombol more  = true  
                    bbiPenertibanMoreData.Enabled = true;
                    moreSanksi = true;
                }
                else
                {
                    // set tombol more false
                    bbiPenertibanMoreData.Enabled = false;
                    moreSanksi = false;
                }
                maxDataSanksi += jmlData;

                if (dataInisial)
                {
                    dataSanksi = new ArrayList();
                }

                dataSanksi.AddRange(output.SF_MON_LAP_WASDAL_SANKSI);
                ucSanksiGrid.bsPgnBrg.DataSource = dataSanksi;
                ucSanksiGrid.gcPgnBrg.RefreshDataSource();

            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }

        }

        #endregion

        #endregion

        #region --++ Form Sanksi
        //KSK.WL.MONLAPWASDAL.ucLapMonSatkerCudSanksi ucSanksiForm;

        public void initMonLapWasdalSanksiForm()
        {
            setTombolMONLAPWASDALForm();


        }

        #endregion

        #endregion

        #region MONLAPWASDAL - CATATAN
        KKW.WL.MONLAPWASDAL.ucLapMonSatkerGridCatatan ucCatatanGrid;
        SvcLapMonWasdalCatatanSelect.execute_pttClient clientCatatan;
        SvcLapMonWasdalCatatanSelect.InputParameters inputCatatan;
        SvcLapMonWasdalCatatanSelect.OutputParameters outputCatatan;

        private void initLaporanWasdalCatatan(string kode, string label)
        {
            setTombolSanksiGrid();
            barButtonItem10.Visibility = BarItemVisibility.Never;
            kodeSanksi = kode;
            ucCatatanGrid = new KKW.WL.MONLAPWASDAL.ucLapMonSatkerGridCatatan(kode, label);
            ucCatatanGrid.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
            ucCatatanGrid.back = new backLapMonSatker(this.mainLapMonWasdal);
            ucCatatanGrid.rowDataTerpilih = gridMONLAPWASDAL.dataTerpilih;
            dataInisial = true;
            GetDataCatatan("");
            setPanel(ucCatatanGrid);
        }


        #region Load Data Catatan
        int maxDataCatatan = 0;
        ArrayList dataCatatan;
        bool moreCatatan = true;
        public void GetDataCatatan(string where)
        {
            try
            {
                this.Enabled = false;
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();

                inputCatatan = new SvcLapMonWasdalCatatanSelect.InputParameters();
                if (dataInisial)
                {
                    inputCatatan.P_MIN = konfigApp.dataAwal;
                    inputCatatan.P_MAX = konfigApp.dataAkhir;
                }
                else
                {
                    inputCatatan.P_MIN = maxDataCatatan + 1;
                    input.P_MAX = maxDataCatatan + konfigApp.dataAkhir;
                }
                inputCatatan.P_MINSpecified = true;
                inputCatatan.P_MAXSpecified = true;
                inputCatatan.P_COL = "";
                inputCatatan.P_SORT = "";
                inputCatatan.STR_WHERE = "ID_MON_LAP = " + kodeSanksi;

                clientCatatan = new SvcLapMonWasdalCatatanSelect.execute_pttClient();
                clientCatatan.Open();
                clientCatatan.Beginexecute(inputCatatan, new AsyncCallback(getResultCatatan), null);

            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);

            }


        }

        private void getResultCatatan(IAsyncResult result)
        {
            try
            {
                outputCatatan = clientCatatan.Endexecute(result);
                clientCatatan.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataCatatan(this.ShowDataGridCatatan), outputCatatan);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);

            }
        }

        private delegate void ShowDataCatatan(SvcLapMonWasdalCatatanSelect.OutputParameters output);

        private void ShowDataGridCatatan(SvcLapMonWasdalCatatanSelect.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_LAP_WASDAL_CATATAN.Count();
                if (jmlData == konfigApp.dataAkhir)
                {
                    bbiPenertibanMoreData.Enabled = true;
                    moreCatatan = true;
                }
                else
                {
                    bbiPenertibanMoreData.Enabled = false;
                    moreSanksi = true;
                }
                maxDataCatatan += jmlData;

                if (dataInisial)
                {
                    dataCatatan = new ArrayList();
                }

                dataCatatan.AddRange(output.SF_MON_LAP_WASDAL_CATATAN);
                ucCatatanGrid.bsPgnBrg.DataSource = dataCatatan;
                ucCatatanGrid.gcPgnBrg.RefreshDataSource();

            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);

            }

        }


        #endregion

        #endregion

        #endregion Pengiriman Laporan Wasdal

        #endregion

        #region VIEW PEREKAMAN SK

        #region pspbmn

        #region -->> Setting Sifat Tombol
        private void setTombolRskGrid()
        {
            bbiRskTambah.Enabled = true;
            bbiRskUbah.Enabled = true;
            bbiRskHapus.Enabled = true;
            bbiRskDetail.Enabled = true;
            bbiRskKembali.Enabled = true;
            bbiRskRefresh.Enabled = true;
            bbiRskMoreData.Enabled = false;
            bbiRskKembaliGrid.Enabled = false;
        }

        private void setTombolRskForm()
        {
            bbiRskTambah.Enabled = false;
            bbiRskUbah.Enabled = false;
            bbiRskHapus.Enabled = false;
            bbiRskDetail.Enabled = false;
            bbiRskKembali.Enabled = true;
            bbiRskRefresh.Enabled = false;
            bbiRskMoreData.Enabled = false;
            bbiRskKembaliGrid.Enabled = true;
        }

        private void resetEventTombolRsk()
        {
            konfigApp.RemoveClickEvent(bbiRskDetail);
            konfigApp.RemoveClickEvent(bbiRskHapus);
            konfigApp.RemoveClickEvent(bbiRskKembali);
            konfigApp.RemoveClickEvent(bbiRskMoreData);
            konfigApp.RemoveClickEvent(bbiRskRefresh);
            konfigApp.RemoveClickEvent(bbiRskTambah);
            konfigApp.RemoveClickEvent(bbiRskUbah);
            konfigApp.RemoveClickEvent(bbiRskKembaliGrid);
        }
        #endregion

        private bool modeCariRSK = false;
        ucRskPspBmnGrid gridSkPspBmn;
        ucRskPspBmnForm formSkPspBmnTambah;
        ucRskPspBmnForm formSkPspBmnUbah;
        ucRskPspBmnForm formSkPspBmnDetail;
        private ArrayList dsGridRskPspBmn;
        SvcWasdalPSPBMNSkSelect.OutputParameters dOutRskPspBmn;
        SvcWasdalPSPBMNSkSelect.execute_pttClient ambilRskPspBmn;

        private void setEventTombolRskPspBmn()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskPspBmnTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskPspBmnUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskPspBmnHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskPspBmnDetailKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKembaliKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKeluarKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskPspBmnRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskPspBmnMoreDataKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKembaliGridKlik);
        }

        private void initGridSkPspBmn()
        {
            //if (gridSkPspBmn == null)
            //{
            gridSkPspBmn = new KKW.RSK.ucRskPspBmnGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskPspBmn),
                detailDataGrid = new DetailDataGrid(bbiRskPspBmnUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskPspBmn();
            setPanel(gridSkPspBmn);
        }

        #region --++ Tombol Ribbon
        private void bbiRskPspBmnTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan02), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkPspBmnTambah == null)
                {
                    formSkPspBmnTambah = new KKW.RSK.ucRskPspBmnForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPspBmn)
                    };
                }
                formSkPspBmnTambah.inisialisasiForm();
                setPanel(formSkPspBmnTambah);
                setTombolRskForm();
            }
        }

        private void bbiRskPspBmnUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPspBmn.dataTerpilih != null)
            {
                if (formSkPspBmnUbah == null)
                {
                    formSkPspBmnUbah = new KKW.RSK.ucRskPspBmnForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPspBmn)
                    };
                }
                formSkPspBmnUbah.dataTerpilih = gridSkPspBmn.dataTerpilih;
                setPanel(formSkPspBmnUbah);
                formSkPspBmnUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPspBmnHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPspBmn.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkPspBmn.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalPSPBMNCrud.InputParameters parInp = new SvcWasdalPSPBMNCrud.InputParameters();
                        parInp.P_ID_SK_WASDALSpecified = true;
                        parInp.P_ID_SK_WASDAL = gridSkPspBmn.dataTerpilih.ID_SK_WASDAL;
                        parInp.P_ID_PEMOHON = gridSkPspBmn.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkPspBmn.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkPspBmn.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkPspBmn.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkPspBmn.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkPspBmn.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkPspBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NILAI_PENETAPAN = gridSkPspBmn.dataTerpilih.NILAI_PENETAPAN;
                        parInp.P_NILAI_PENETAPANSpecified = true;
                        parInp.P_NIP_PENANDATANGAN = gridSkPspBmn.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkPspBmn.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkPspBmn.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkPspBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkPspBmn.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TGL_SK = konfigApp.DateToString(gridSkPspBmn.dataTerpilih.TGL_SK);
                        parInp.P_THN_ANG = (gridSkPspBmn.dataTerpilih.THN_ANG == "-") ? konfigApp.tahunAnggaran.ToString() : gridSkPspBmn.dataTerpilih.THN_ANG;
                        parInp.P_TIPE_PEMOHON = gridSkPspBmn.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkPspBmn.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkPspBmn.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkPspBmn.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkPspBmn.dataTerpilih.UR_SATKER;

                        ambilDataPspBmn = new SvcWasdalPSPBMNCrud.execute_pttClient();
                        ambilDataPspBmn.Open();
                        ambilDataPspBmn.Beginexecute(parInp, new AsyncCallback(cudRskPspBmn), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskPspBmnDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPspBmn.dataTerpilih != null)
            {
                if (formSkPspBmnDetail == null)
                {
                    formSkPspBmnDetail = new KKW.RSK.ucRskPspBmnForm("A");
                    formSkPspBmnDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkPspBmnDetail.dataTerpilih = gridSkPspBmn.dataTerpilih;
                setPanel(formSkPspBmnDetail);
                formSkPspBmnDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPspBmnKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //panelKoorSatker.Controls.Clear();
            //rpRekamSK.Visible = false;
            setPanel(gridSkPspBmn);
            setTombolRskGrid();
            gridSkPspBmn.dsDataSource = dsGridRskPspBmn;
            gridSkPspBmn.displayData();
        }

        private void bbiRskPspBmnRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkPspBmn.teNamaKolom.Text = "";
            gridSkPspBmn.teCari.Text = "";
            gridSkPspBmn.fieldDicari = "";
            gridSkPspBmn.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskPspBmn();
        }

        private void bbiRskPspBmnMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskPspBmn();
            }
        }

        private void bbiRskPspBmnKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskPspBmnKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = "Perekaman " + konfigApp.namaLayanan02;
            konfigApp.kdPelayanan = "02";
            konfigApp.namaPelayanan = konfigApp.namaLayanan02;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkPspBmn();
            modeCariRSK = false;
            gridSkPspBmn.teNamaKolom.Text = "";
            gridSkPspBmn.teCari.Text = "";
            gridSkPspBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskPspBmn();
        }

        #endregion --++ Tombol Ribbon

        private void nbiPerekamanSK_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = "Perekaman " + konfigApp.namaLayanan02;
            konfigApp.kdPelayanan = "02";
            konfigApp.namaPelayanan = konfigApp.namaLayanan02;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkPspBmn();
            modeCariRSK = false;
            gridSkPspBmn.teNamaKolom.Text = "";
            gridSkPspBmn.teCari.Text = "";
            gridSkPspBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskPspBmn();
        }

        #region --++ Ambil Data PSP BMN
        private void getInitRskPspBmn()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNSkSelect.InputParameters parInp = new SvcWasdalPSPBMNSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
               // parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
               //" (ID_USER={1} " +
               // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
               // "OR ID_SATKER = {2} " +
               // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskPspBmn = new SvcWasdalPSPBMNSkSelect.execute_pttClient();
                //ambilRskPspBmn.Open();
                ambilRskPspBmn.Beginexecute(parInp, new AsyncCallback(getRskPspBmn), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskPspBmn(IAsyncResult result)
        {
            try
            {
                dOutRskPspBmn = ambilRskPspBmn.Endexecute(result);
                ambilRskPspBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskPspBmn(dsRskPspBmn), dOutRskPspBmn);
            }
            catch (Exception ex)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskPspBmn(SvcWasdalPSPBMNSkSelect.OutputParameters dataOut);

        private void dsRskPspBmn(SvcWasdalPSPBMNSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_PSP.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_READ_WASDAL_PSP[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
            }
            if (dataInisial == true)
            {
                dsGridRskPspBmn = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_PSP[i].IS_TB = (dataOut.SF_READ_WASDAL_PSP[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskPspBmn.Add(dataOut.SF_READ_WASDAL_PSP[i]);
            }

            gridSkPspBmn.labelTotData.Text = "";
            gridSkPspBmn.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " data dari " + totalData + " data";
            gridSkPspBmn.sbCariOnline.Enabled = !modeCari;
            gridSkPspBmn.dsDataSource = dsGridRskPspBmn;
            gridSkPspBmn.displayData();
            if (modeCariRSK == true)
            {
                string xSatu = gridSkPspBmn.teNamaKolom.Text.Trim();
                string xDua = gridSkPspBmn.teCari.Text.Trim();
                string xTiga = gridSkPspBmn.fieldDicari;
                gridSkPspBmn.gvGridPenertiban.ClearColumnsFilter();
                gridSkPspBmn.teNamaKolom.Text = xSatu;
                gridSkPspBmn.teCari.Text = xDua;
                gridSkPspBmn.fieldDicari = xTiga;
            }
            else
                gridSkPspBmn.gvGridPenertiban.ClearColumnsFilter();
        }

        private void cariDataRskPspBmn(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCariRSK = true;
            dataInisial = initModeCari;
            getInitRskPspBmn();
        }
        #endregion Ambil PSP BMN

        #region --++ Simpan Data PSP BMN
        SvcWasdalPSPBMNCrud.OutputParameters dOutAmbilDataPspBmn;
        SvcWasdalPSPBMNCrud.execute_pttClient ambilDataPspBmn;

        private void simpanDataRskPspBmn(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNCrud.InputParameters parInp = new SvcWasdalPSPBMNCrud.InputParameters();
                parInp.P_ID_SK_WASDALSpecified = true;
                parInp.P_ID_SK_WASDAL = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.idSkWasdal : formSkPspBmnUbah.idSkWasdal);
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.idPemohon : formSkPspBmnUbah.idPemohon);
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.rgJenisAset.EditValue.ToString() : formSkPspBmnUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teJabatan.Text.Trim() : formSkPspBmnUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.kodePenerbitSk : formSkPspBmnUbah.kodePenerbitSk);
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.kodePenerbitSkDetail : formSkPspBmnUbah.kodePenerbitSkDetail);
                parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkPspBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnTambah.teNilaiPenetapan.Text)) : (formSkPspBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnUbah.teNilaiPenetapan.Text)));
                parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teNipPenandaTangan.Text : formSkPspBmnUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teNamaPenandaTangan.Text : formSkPspBmnUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teNmPenerbitSk.Text : formSkPspBmnUbah.teNmPenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teNmPenerbitSkDetail.Text : formSkPspBmnUbah.teNmPenerbitSkDetail.Text);
                //parInp.P_FILE_SK = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teFileSk.Text : formSkPspBmnUbah.teFileSk.Text);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkPspBmnTambah.teFileSk.Text) : konfigApp.FileToByteArray(formSkPspBmnUbah.teFileSk.Text ));
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teNomorSk.Text.Trim() : formSkPspBmnUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teTanggalSk.EditValue : formSkPspBmnUbah.teTanggalSk.EditValue)));
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teTahunAnggaran.Text : formSkPspBmnUbah.teTahunAnggaran.Text);
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teJenisPemohon.Text : formSkPspBmnUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.tipePengelola : formSkPspBmnUbah.tipePengelola); ;//konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnTambah.teUraianKeputusan.Text : formSkPspBmnUbah.teUraianKeputusan.Text);


                ambilDataPspBmn = new SvcWasdalPSPBMNCrud.execute_pttClient();
                ambilDataPspBmn.Beginexecute(parInp, new AsyncCallback(cudRskPspBmn), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void cudRskPspBmn(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataPspBmn = ambilDataPspBmn.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsPspBmn(this.ubahDsPspBmn), dOutAmbilDataPspBmn);

            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsPspBmn(SvcWasdalPSPBMNCrud.OutputParameters dataOutKpknlCrud);

        int _yangkeBerapa = 0;

        private void ubahDsPspBmn(SvcWasdalPSPBMNCrud.OutputParameters dataOutKpknlCrud)
        {
            if (dataOutKpknlCrud.PO_RESULT == "Y")
            {
                int _indeksData = 0;
                SvcWasdalPSPBMNSkSelect.WASDALSROW_READ_WASDAL_PSP dataPenyama = new SvcWasdalPSPBMNSkSelect.WASDALSROW_READ_WASDAL_PSP();
                dataPenyama.ID_PEMOHONSpecified = true;
                //dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ID_PEMOHON = formSkPspBmnTambah.idPemohon;
                        //dataPenyama.ID_SATKER = konfigApp.idSatker;//formSkPspBmnTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkPspBmnTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPspBmnTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkPspBmnTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPspBmnTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPspBmnTambah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPspBmnTambah.kodePenerbitSkDetail;
                        //dataPenyama.KD_SATKER = konfigApp.kodeSatker;//formSkPspBmnTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPspBmnTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPspBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPspBmnTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkPspBmnTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPspBmnTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPspBmnTambah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPspBmnTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskPspBmn == null ? 1 : dsGridRskPspBmn.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkPspBmnTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPspBmnTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPspBmnTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.THN_ANG = formSkPspBmnTambah.teTahunAnggaran.Text;
                        dataPenyama.UR_KL = formSkPspBmnTambah.teNamaKl.Text;
                        //dataPenyama.UR_SATKER = konfigApp.namaSatker;//formSkPspBmnTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPspBmnTambah.teUraianKeputusan.Text;
                        dsGridRskPspBmn.Add(dataPenyama);
                        if (modeCrud == 'C')
                        {
                            gridSkPspBmn.dataTerpilih = dataPenyama;
                            _yangkeBerapa = 1;
                            formSkPspBmnTambah.gcDaftarAset.Enabled = true;
                            formSkPspBmnTambah.sbCari.Enabled = true;
                            formSkPspBmnTambah.sbTambah.Enabled = true;
                            formSkPspBmnTambah.sbHapus.Enabled = true;
                            formSkPspBmnTambah.rgJenisAset.Properties.ReadOnly = true;
                            formSkPspBmnTambah.teNomorSk.Properties.ReadOnly = false;
                            formSkPspBmnTambah.teTanggalSk.Properties.ReadOnly = false;
                            formSkPspBmnTambah.teJenisPemohon.Properties.ReadOnly = true;
                            formSkPspBmnTambah.sbCariPemohon.Enabled = false;
                            formSkPspBmnTambah.sbRefresh.Enabled = true;
                            formSkPspBmnTambah.teNmPenerbitSk.Properties.ReadOnly = true;
                            formSkPspBmnTambah.cePilihSemua.Enabled = true;
                            formSkPspBmnTambah.teKodePemohon.Properties.ReadOnly = true;
                            //formSkPspBmnTambah.statusForm = "CU";
                        }
                        else if (modeCrud == 'Z')
                        {
                            /*if (_keBerapa == 1)
                            {
                                _keBerapa++;
                                _indeksData = dsGridRskPspBmn.IndexOf(gridSkPspBmn.dataTerpilih);
                            }
                            else
                            {
                                gridSkPspBmn.dataTerpilih = gridSkPspBmn.dataTerpilih;
                            }*/
                            _indeksData = dsGridRskPspBmn.IndexOf(gridSkPspBmn.dataTerpilih);
                            _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                            dsGridRskPspBmn.Remove(gridSkPspBmn.dataTerpilih);
                            dsGridRskPspBmn.Insert(_indeksData, dataPenyama);
                        }
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL = formSkPspBmnUbah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkPspBmnUbah.idPemohon;
                        //dataPenyama.ID_SATKER = konfigApp.idSatker;//formSkPspBmnUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkPspBmnUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPspBmnUbah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkPspBmnUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPspBmnUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPspBmnUbah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPspBmnUbah.kodePenerbitSkDetail;
                        //dataPenyama.KD_SATKER = konfigApp.kodeSatker;//formSkPspBmnUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPspBmnUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPspBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPspBmnUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkPspBmn.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkPspBmnUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPspBmnUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPspBmnUbah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPspBmnUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkPspBmn.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkPspBmn.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkPspBmnUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkPspBmn.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkPspBmn.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPspBmnUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPspBmnUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkPspBmn.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkPspBmn.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkPspBmn.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkPspBmnUbah.teNamaKl.Text;
                        //dataPenyama.UR_SATKER = konfigApp.namaSatker;//formSkPspBmnUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPspBmnUbah.teUraianKeputusan.Text;
                        dataPenyama.THN_ANG = formSkPspBmnUbah.teTahunAnggaran.Text;
                        _indeksData = dsGridRskPspBmn.IndexOf(gridSkPspBmn.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskPspBmn.Remove(gridSkPspBmn.dataTerpilih);
                        dsGridRskPspBmn.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskPspBmn.Remove(gridSkPspBmn.dataTerpilih);
                        break;
                }
                gridSkPspBmn.dsDataSource = dsGridRskPspBmn;
                gridSkPspBmn.displayData();

                //if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z') || (this.modeCrud == 'D'))
                //{
                //    initGridSkPspBmn();
                //}
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data PSP BMN

        #endregion

        #region pspbmndpl

        ucRskPspBmnDplGrid gridSkPspBmnDpl;
        ucRskPspBmnDplForm formSkPspBmnDplTambah;
        ucRskPspBmnDplForm formSkPspBmnDplUbah;
        ucRskPspBmnDplForm formSkPspBmnDplDetail;
        private ArrayList dsGridRskPspBmnDpl;
        SvcWasdalPSPBMNLAINSkSelect.OutputParameters dOutRskPspBmnDpl;
        SvcWasdalPSPBMNLAINSkSelect.execute_pttClient ambilRskPspBmnDpl;

        private void setEventTombolRskPspBmnDpl()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskPspBmnDplTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskPspBmnDplUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskPspBmnDplHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskPspBmnDplDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskPspBmnDplKembaliKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskPspBmnDplRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskPspBmnDplMoreDataKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKembaliKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKeluarKlik);
        }

        private void initGridSkPspBmnDpl()
        {
            //if (gridSkPspBmnDpl == null)
            //{
            gridSkPspBmnDpl = new AppPengguna.KKW.RSK.ucRskPspBmnDplGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskPspBmnDpl),
                detailDataGrid = new DetailDataGrid(bbiRskPspBmnDplUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskPspBmnDpl();
            setPanel(gridSkPspBmnDpl);
        }

        #region --++ Tombol Ribbon
        private void bbiRskPspBmnDplTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan03), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkPspBmnDplTambah == null)
                {
                    formSkPspBmnDplTambah = new AppPengguna.KKW.RSK.ucRskPspBmnDplForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmnDpl = new SimpanDataRsk(simpanDataRskPspBmnDpl)
                    };
                }
                setPanel(formSkPspBmnDplTambah);
                formSkPspBmnDplTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskPspBmnDplUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPspBmnDpl.dataTerpilih != null)
            {
                if (formSkPspBmnDplUbah == null)
                {
                    formSkPspBmnDplUbah = new AppPengguna.KKW.RSK.ucRskPspBmnDplForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmnDpl = new SimpanDataRsk(simpanDataRskPspBmnDpl)
                    };
                }
                formSkPspBmnDplUbah.dataTerpilih = gridSkPspBmnDpl.dataTerpilih;
                setPanel(formSkPspBmnDplUbah);
                formSkPspBmnDplUbah.inisialisasiForm();
                setEventTombolRskPspBmnDpl();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPspBmnDplHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPspBmnDpl.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkPspBmnDpl.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalPSPBMNLAINCrud.InputParameters parInp = new SvcWasdalPSPBMNLAINCrud.InputParameters();
                        parInp.P_ID_SK_WASDALSpecified = true;
                        parInp.P_ID_SK_WASDAL = gridSkPspBmnDpl.dataTerpilih.ID_SK_WASDAL;

                        parInp.P_ID_PEMOHON = gridSkPspBmnDpl.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkPspBmnDpl.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkPspBmnDpl.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkPspBmnDpl.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkPspBmnDpl.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkPspBmnDpl.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkPspBmnDpl.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NILAI_PENETAPAN = gridSkPspBmnDpl.dataTerpilih.NILAI_PENETAPAN;
                        parInp.P_NILAI_PENETAPANSpecified = true;
                        parInp.P_NIP_PENANDATANGAN = gridSkPspBmnDpl.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkPspBmnDpl.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkPspBmnDpl.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkPspBmnDpl.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkPspBmnDpl.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkPspBmnDpl.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkPspBmnDpl.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkPspBmnDpl.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkPspBmnDpl.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkPspBmnDpl.dataTerpilih.UR_SATKER;
                        simpanDataPspBmnDpl = new SvcWasdalPSPBMNLAINCrud.execute_pttClient();
                        simpanDataPspBmnDpl.Open();
                        simpanDataPspBmnDpl.Beginexecute(parInp, new AsyncCallback(cudRskPspBmnDpl), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskPspBmnDplDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPspBmnDpl.dataTerpilih != null)
            {
                if (formSkPspBmnDplDetail == null)
                {
                    formSkPspBmnDplDetail = new AppPengguna.KKW.RSK.ucRskPspBmnDplForm("A")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar)
                    };
                }
                formSkPspBmnDplDetail.dataTerpilih = gridSkPspBmnDpl.dataTerpilih;
                setPanel(formSkPspBmnDplDetail);
                formSkPspBmnDplDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPspBmnDplKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkPspBmnDpl);
            setTombolRskGrid();
            gridSkPspBmnDpl.dsDataSource = dsGridRskPspBmnDpl;
            gridSkPspBmnDpl.displayData();
        }

        private void bbiRskPspBmnDplRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkPspBmnDpl.teNamaKolom.Text = "";
            gridSkPspBmnDpl.teCari.Text = "";
            gridSkPspBmnDpl.fieldDicari = "";
            gridSkPspBmnDpl.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskPspBmnDpl();
        }

        private void bbiRskPspBmnDplMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskPspBmnDpl();
            }
        }
        #endregion --++ Tombol Ribbon

        private void nbiRskPspBmnDpl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan03;
            konfigApp.kdPelayanan = "03";
            konfigApp.namaPelayanan = konfigApp.namaLayanan03;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkPspBmnDpl();
            modeCari = false;
            gridSkPspBmnDpl.teNamaKolom.Text = "";
            gridSkPspBmnDpl.teCari.Text = "";
            gridSkPspBmnDpl.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskPspBmnDpl();
        }

        #region --++ Ambil Data PSP BMN DPL
        private void getInitRskPspBmnDpl()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNLAINSkSelect.InputParameters parInp = new SvcWasdalPSPBMNLAINSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
             //   parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
             //" (ID_USER={1} " +
             // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
             // "OR ID_SATKER = {2} " +
             // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskPspBmnDpl = new SvcWasdalPSPBMNLAINSkSelect.execute_pttClient();
                ambilRskPspBmnDpl.Open();
                ambilRskPspBmnDpl.Beginexecute(parInp, new AsyncCallback(getRskPspBmnDpl), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskPspBmnDpl(IAsyncResult result)
        {
            try
            {
                dOutRskPspBmnDpl = ambilRskPspBmnDpl.Endexecute(result);
                ambilRskPspBmnDpl.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskPspBmnDpl(dsRskPspBmnDpl), dOutRskPspBmnDpl);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskPspBmnDpl(SvcWasdalPSPBMNLAINSkSelect.OutputParameters dataOut);

        private void dsRskPspBmnDpl(SvcWasdalPSPBMNLAINSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_PSP_LAIN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_READ_WASDAL_PSP_LAIN[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
            }
            if (dataInisial == true)
            {
                dsGridRskPspBmnDpl = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_PSP_LAIN[i].IS_TB = (dataOut.SF_READ_WASDAL_PSP_LAIN[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dataOut.SF_READ_WASDAL_PSP_LAIN[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_READ_WASDAL_PSP_LAIN[i].TGL_SK);

                dsGridRskPspBmnDpl.Add(dataOut.SF_READ_WASDAL_PSP_LAIN[i]);
            }
            gridSkPspBmnDpl.labelTotData.Text = "";
            gridSkPspBmnDpl.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";

            gridSkPspBmnDpl.sbCariOnline.Enabled = !modeCari;
            gridSkPspBmnDpl.dsDataSource = dsGridRskPspBmnDpl;
            gridSkPspBmnDpl.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkPspBmnDpl.teNamaKolom.Text.Trim();
                string xDua = gridSkPspBmnDpl.teCari.Text.Trim();
                string xTiga = gridSkPspBmnDpl.fieldDicari;
                gridSkPspBmnDpl.gvGridSk.ClearColumnsFilter();
                gridSkPspBmnDpl.teNamaKolom.Text = xSatu;
                gridSkPspBmnDpl.teCari.Text = xDua;
                gridSkPspBmnDpl.fieldDicari = xTiga;
            }
            else
                gridSkPspBmnDpl.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskPspBmnDpl(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskPspBmnDpl();
        }
        #endregion Ambil PSP BMN DPL

        #region --++ Simpan Data PSP BMN DPL
        SvcWasdalPSPBMNLAINCrud.OutputParameters dOutPspBmnDpl;
        SvcWasdalPSPBMNLAINCrud.execute_pttClient simpanDataPspBmnDpl;

        private void simpanDataRskPspBmnDpl(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNLAINCrud.InputParameters parInp = new SvcWasdalPSPBMNLAINCrud.InputParameters()
                {
                    P_ID_SK_WASDALSpecified = true,
                    P_ID_SK_WASDAL = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.idSkWasdal : formSkPspBmnDplUbah.idSkWasdal),
                    P_ID_KPKNL = konfigApp.idKpknl,
                    P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true),
                    P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.idPemohon : formSkPspBmnDplUbah.idPemohon),
                    P_ID_PEMOHONSpecified = true,
                    P_ID_USER = konfigApp.idUser,
                    P_ID_USERSpecified = true,
                    P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.rgJenisAset.EditValue.ToString() : formSkPspBmnDplUbah.rgJenisAset.EditValue.ToString()),
                    P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teJabatan.Text.Trim() : formSkPspBmnDplUbah.teJabatan.Text.Trim()),
                    P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teTahunAnggaran.Text.Trim() : formSkPspBmnDplUbah.teTahunAnggaran.Text.Trim()),
                    P_KD_PELAYANAN = konfigApp.kdPelayanan,
                    P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teNmPenerbitSk.EditValue.ToString() : formSkPspBmnDplUbah.teNmPenerbitSk.EditValue.ToString()),
                    P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.kodePenerbitSkDetail : formSkPspBmnDplUbah.kodePenerbitSkDetail),
                    P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkPspBmnDplTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplTambah.teNilaiPenetapan.Text)) : (formSkPspBmnDplUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplUbah.teNilaiPenetapan.Text))),
                    P_NILAI_PENETAPANSpecified = true,
                    P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teNipPenandaTangan.Text : formSkPspBmnDplUbah.teNipPenandaTangan.Text),
                    P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teNamaPenandaTangan.Text : formSkPspBmnDplUbah.teNamaPenandaTangan.Text),
                    P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teNmPenerbitSk.Text : formSkPspBmnDplUbah.teNmPenerbitSk.Text),
                    P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teNamaInstansi.Text : formSkPspBmnDplUbah.namaPenerbitSkDetail),
                    P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teNomorSk.Text.Trim() : formSkPspBmnDplUbah.teNomorSk.Text.Trim()),
                    P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null),
                    P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teTanggalSk.EditValue : formSkPspBmnDplUbah.teTanggalSk.EditValue))),
                    P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teJenisPemohon.Text : formSkPspBmnDplUbah.teJenisPemohon.Text),
                    P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.tipePengelola : formSkPspBmnDplUbah.tipePengelola),
                    P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teUraianKeputusan.Text : formSkPspBmnDplUbah.teUraianKeputusan.Text),
                    P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teAlamatPihakLain.Text : formSkPspBmnDplUbah.teAlamatPihakLain.Text),
                    //P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkPspBmnDplTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplTambah.teJangkaWaktu.Text)) : (formSkPspBmnDplUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplUbah.teJangkaWaktu.Text))),
                    P_JANGKA_WAKTUSpecified = true,
                    P_KD_KL = konfigApp.kodeKl,
                    P_ID_SATKER = konfigApp.idSatker,
                    P_ID_SATKERSpecified = true,
                    P_KD_SATKER = konfigApp.kodeSatker,
                    P_UR_SATKER = konfigApp.namaSatker,
                    P_UR_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teNamaPihakLain.Text : formSkPspBmnDplUbah.teNamaPihakLain.Text),

                    //P_PERIODE = ((_mode=="C" || _mode=="CU") ? formSkPspBmnDplTambah.konversiPeriode():formSkPspBmnDplUbah.konversiPeriode()),
                    //P_FILE_SK = ((_mode == "C" || _mode == "CU") ? formSkPspBmnDplTambah.teFileSk.Text : formSkPspBmnDplUbah.teFileSk.Text),
                    //P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkPspBmnDplTambah.filePath) : konfigApp.FileToByteArray(formSkPspBmnDplUbah.filePath))

                };
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_penggantiChar);
                modeCrud = Convert.ToChar(_mode);
                simpanDataPspBmnDpl = new SvcWasdalPSPBMNLAINCrud.execute_pttClient();
                simpanDataPspBmnDpl.Open();
                simpanDataPspBmnDpl.Beginexecute(parInp, new AsyncCallback(cudRskPspBmnDpl), "");
            }
            catch (Exception e)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulKonfirmasi);
            }
        }

        private void cudRskPspBmnDpl(IAsyncResult result)
        {
            try
            {
                dOutPspBmnDpl = simpanDataPspBmnDpl.Endexecute(result);
                simpanDataPspBmnDpl.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsPspBmnDpl(ubahDsPspBmnDpl), dOutPspBmnDpl);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsPspBmnDpl(SvcWasdalPSPBMNLAINCrud.OutputParameters dataOutPspBmnDpl);

        private void ubahDsPspBmnDpl(SvcWasdalPSPBMNLAINCrud.OutputParameters dataOutPspBmnDpl)
        {
            if (dataOutPspBmnDpl.PO_RESULT == "Y")
            {
                SvcWasdalPSPBMNLAINSkSelect.WASDALSROW_READ_WASDAL_PSP_LAIN dataPenyama = new SvcWasdalPSPBMNLAINSkSelect.WASDALSROW_READ_WASDAL_PSP_LAIN();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                dataPenyama.JANGKA_WAKTUSpecified = true;
                if (dsGridRskPspBmnDpl == null) dsGridRskPspBmnDpl = new ArrayList();
                switch (modeCrud.ToString())
                {
                    case "C":
                        dataPenyama.ID_PEMOHON = formSkPspBmnDplTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPspBmnDplTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkPspBmnDplTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPspBmnDplTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkPspBmnDplTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPspBmnDplTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPspBmnDplTambah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPspBmnDplTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPspBmnDplTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = (formSkPspBmnDplTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplTambah.teKuantitas.Text));
                        dataPenyama.NILAI_PENETAPAN = (formSkPspBmnDplTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplTambah.teNilaiPenetapan.Text));
                        dataPenyama.NIP_PENANDATANGAN = formSkPspBmnDplTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkPspBmnDplTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPspBmnDplTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPspBmnDplTambah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPspBmnDplTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskPspBmnDpl == null ? 1 : dsGridRskPspBmnDpl.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkPspBmnDplTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPspBmnDplTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPspBmnDplTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.THN_ANG = formSkPspBmnDplTambah.teTahunAnggaran.Text;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkPspBmnDplTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPspBmnDplTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPspBmnDplTambah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkPspBmnDplTambah.teAlamatPihakLain.Text;
                        //dataPenyama.JANGKA_WAKTU = (formSkPspBmnDplTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplTambah.teJangkaWaktu.Text));
                        dataPenyama.UR_PHK_LAIN = formSkPspBmnDplTambah.teNamaPihakLain.Text;
                        dsGridRskPspBmnDpl.Add(dataPenyama);
                        formSkPspBmnDplTambah.gcDaftarAset.Enabled = true;
                        formSkPspBmnDplTambah.sbTambah.Enabled = true;
                        formSkPspBmnDplTambah.sbHapus.Enabled = true;
                        formSkPspBmnDplTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkPspBmnDplTambah.teNomorSk.Properties.ReadOnly = false;
                        formSkPspBmnDplTambah.teTanggalSk.Properties.ReadOnly = false;
                        formSkPspBmnDplTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkPspBmnDplTambah.sbCariPemohon.Enabled = false;
                        formSkPspBmnDplTambah.sbRefresh.Enabled = true;
                        formSkPspBmnDplTambah.sbValidasi.Enabled = true;
                        formSkPspBmnDplTambah.teNmPenerbitSk.Properties.ReadOnly = true;
                        formSkPspBmnDplTambah.cePilihSemua.Enabled = true;
                        formSkPspBmnDplTambah.teKodePemohon.Properties.ReadOnly = true;

                        //formSkPspBmnDplTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL = formSkPspBmnDplUbah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkPspBmnDplUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPspBmnDplUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkPspBmnDplUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPspBmnDplUbah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkPspBmnDplUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPspBmnDplUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPspBmnDplUbah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPspBmnDplUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPspBmnDplUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = (formSkPspBmnDplUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplUbah.teKuantitas.Text));
                        dataPenyama.NILAI_PENETAPAN = (formSkPspBmnDplUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplUbah.teNilaiPenetapan.Text));
                        dataPenyama.NIP_PENANDATANGAN = formSkPspBmnDplUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkPspBmnDplUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPspBmnDplUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPspBmnDplUbah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPspBmnDplUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkPspBmnDpl.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkPspBmnDpl.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkPspBmnDplUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkPspBmnDpl.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkPspBmnDpl.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPspBmnDplUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPspBmnDplUbah.teJenisPemohon.Text;
                        dataPenyama.THN_ANG = formSkPspBmnDplUbah.teTahunAnggaran.Text;
                        dataPenyama.TOT_BMN = gridSkPspBmnDpl.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkPspBmnDpl.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkPspBmnDpl.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkPspBmnDplUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPspBmnDplUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPspBmnDplUbah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkPspBmnDplUbah.teAlamatPihakLain.Text;
                        //dataPenyama.JANGKA_WAKTU = (formSkPspBmnDplUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPspBmnDplUbah.teJangkaWaktu.Text));
                        dataPenyama.UR_PHK_LAIN = formSkPspBmnDplUbah.teNamaPihakLain.Text;
                        int _indeksData = dsGridRskPspBmnDpl.IndexOf(gridSkPspBmnDpl.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskPspBmnDpl.Remove(gridSkPspBmnDpl.dataTerpilih);
                        dsGridRskPspBmnDpl.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskPspBmnDpl.Remove(gridSkPspBmnDpl.dataTerpilih);
                        break;
                }
                gridSkPspBmnDpl.dsDataSource = dsGridRskPspBmnDpl;
                gridSkPspBmnDpl.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data PSP BMN

        #endregion

        #region aspbmn
        ucRskAspBmnGrid gridSkAspBmn;
        ucRskAspBmnForm formSkAspBmnTambah;
        ucRskAspBmnForm formSkAspBmnUbah;
        ucRskAspBmnForm formSkAspBmnDetail;
        private ArrayList dsGridRskAspBmn;
        SvcWasdalPSPBMNLAINSkSelect.OutputParameters dOutRskAspBmn;
        SvcWasdalPSPBMNLAINSkSelect.execute_pttClient ambilRskAspBmn;


        private void setEventTombolRskAspBmn()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskAspBmnTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskAspBmnUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskAspBmnHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskAspBmnDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskAspBmnKembaliKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskAspBmnRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskAspBmnMoreDataKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKembaliKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKeluarKlik);
        }

        private void initGridSkAspBmn()
        {
            //if (gridSkAspBmn == null)
            //{
            gridSkAspBmn = new AppPengguna.KKW.RSK.ucRskAspBmnGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskAspBmn),
                detailDataGrid = new DetailDataGrid(bbiRskAspBmnUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskAspBmn();
            setPanel(gridSkAspBmn);
        }

        #region --++ Tombol Ribbon
        private void bbiRskAspBmnTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan04), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkAspBmnTambah == null)
                {
                    formSkAspBmnTambah = new AppPengguna.KKW.RSK.ucRskAspBmnForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskAspBmn)
                    };
                }
                setPanel(formSkAspBmnTambah);
                formSkAspBmnTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskAspBmnUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkAspBmn.dataTerpilih != null)
            {
                if (formSkAspBmnUbah == null)
                {
                    formSkAspBmnUbah = new AppPengguna.KKW.RSK.ucRskAspBmnForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskAspBmn)
                    };
                }
                formSkAspBmnUbah.dataTerpilih = gridSkAspBmn.dataTerpilih;
                setPanel(formSkAspBmnUbah);
                formSkAspBmnUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskAspBmnHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (gridSkAspBmn.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkAspBmn.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalPSPBMNLAINCrud.InputParameters parInp = new SvcWasdalPSPBMNLAINCrud.InputParameters();
                        parInp.P_ID_SK_WASDALSpecified = true;
                        parInp.P_ID_SK_WASDAL = gridSkAspBmn.dataTerpilih.ID_SK_WASDAL;
                        parInp.P_ID_PEMOHON = gridSkAspBmn.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkAspBmn.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkAspBmn.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkAspBmn.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkAspBmn.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkAspBmn.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkAspBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NILAI_PENETAPAN = gridSkAspBmn.dataTerpilih.NILAI_PENETAPAN;
                        parInp.P_NILAI_PENETAPANSpecified = true;
                        parInp.P_NIP_PENANDATANGAN = gridSkAspBmn.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkAspBmn.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkAspBmn.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkAspBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_JANGKA_WAKTUSpecified = true;
                        parInp.P_JANGKA_WAKTU = gridSkAspBmn.dataTerpilih.JANGKA_WAKTU;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkAspBmn.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkAspBmn.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkAspBmn.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkAspBmn.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkAspBmn.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkAspBmn.dataTerpilih.UR_SATKER;
                        ambilDataRskAspBmn = new SvcWasdalPSPBMNLAINCrud.execute_pttClient();
                        ambilDataRskAspBmn.Open();
                        ambilDataRskAspBmn.Beginexecute(parInp, new AsyncCallback(cudRskAspBmn), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskAspBmnDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkAspBmn.dataTerpilih != null)
            {
                if (formSkAspBmnDetail == null)
                {
                    formSkAspBmnDetail = new AppPengguna.KKW.RSK.ucRskAspBmnForm("A");
                    formSkAspBmnDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkAspBmnDetail.dataTerpilih = gridSkAspBmn.dataTerpilih;
                setPanel(formSkAspBmnDetail);
                formSkAspBmnDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskAspBmnKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkAspBmn);
            setTombolRskGrid();
            gridSkAspBmn.dsDataSource = dsGridRskAspBmn;
            gridSkAspBmn.displayData();
        }

        private void bbiRskAspBmnRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkAspBmn.teNamaKolom.Text = "";
            gridSkAspBmn.teCari.Text = "";
            gridSkAspBmn.fieldDicari = "";
            gridSkAspBmn.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskAspBmn();
        }

        private void bbiRskAspBmnMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskAspBmn();
            }
        }
        #endregion --++ Tombol Ribbon

        private void nbiRskAspBmn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan04;
            konfigApp.kdPelayanan = "04";
            konfigApp.namaPelayanan = konfigApp.namaLayanan04;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkAspBmn();
            modeCari = false;
            gridSkAspBmn.teNamaKolom.Text = "";
            gridSkAspBmn.teCari.Text = "";
            gridSkAspBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskAspBmn();
        }

        #region --++ Ambil Data ASP BMN
        private void getInitRskAspBmn()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNLAINSkSelect.InputParameters parInp = new SvcWasdalPSPBMNLAINSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
             //   parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
             //" (ID_USER={1} " +
             // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
             // "OR ID_SATKER = {2} " +
             // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskAspBmn = new SvcWasdalPSPBMNLAINSkSelect.execute_pttClient();
                ambilRskAspBmn.Open();
                ambilRskAspBmn.Beginexecute(parInp, new AsyncCallback(getRskAspBmn), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskAspBmn(IAsyncResult result)
        {
            try
            {
                dOutRskAspBmn = ambilRskAspBmn.Endexecute(result);
                ambilRskAspBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskAspBmn(dsRskAspBmn), dOutRskAspBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskAspBmn(SvcWasdalPSPBMNLAINSkSelect.OutputParameters dataOut);

        private void dsRskAspBmn(SvcWasdalPSPBMNLAINSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_PSP_LAIN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_READ_WASDAL_PSP_LAIN[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
            }

            if (dataInisial == true)
            {
                dsGridRskAspBmn = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_PSP_LAIN[i].IS_TB = (dataOut.SF_READ_WASDAL_PSP_LAIN[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskAspBmn.Add(dataOut.SF_READ_WASDAL_PSP_LAIN[i]);
            }
            gridSkAspBmn.labelTotData.Text = "";
            gridSkAspBmn.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkAspBmn.sbCariOnline.Enabled = !modeCari;
            gridSkAspBmn.dsDataSource = dsGridRskAspBmn;
            gridSkAspBmn.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkAspBmn.teNamaKolom.Text.Trim();
                string xDua = gridSkAspBmn.teCari.Text.Trim();
                string xTiga = gridSkAspBmn.fieldDicari;
                gridSkAspBmn.gvGridSk.ClearColumnsFilter();
                gridSkAspBmn.teNamaKolom.Text = xSatu;
                gridSkAspBmn.teCari.Text = xDua;
                gridSkAspBmn.fieldDicari = xTiga;
            }
            else
                gridSkAspBmn.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskAspBmn(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskAspBmn();
        }
        #endregion Ambil ASP BMN

        #region --++ Simpan Data ASP BMN
        //SvcWasdalLAINCrud.OutputParameters dOutAmbilDataRskAspBmn;
        //SvcWasdalLAINCrud.call_pttClient ambilDataRskAspBmn;
        SvcWasdalPSPBMNLAINCrud.OutputParameters dOutAmbilDataRskAspBmn;
        SvcWasdalPSPBMNLAINCrud.execute_pttClient ambilDataRskAspBmn;

        private void simpanDataRskAspBmn(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNLAINCrud.InputParameters parInp = new SvcWasdalPSPBMNLAINCrud.InputParameters();
                parInp.P_ID_SK_WASDALSpecified = true;
                parInp.P_ID_SK_WASDAL = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.idSkWasdal : formSkAspBmnUbah.idSkWasdal);
                parInp.P_ID_KPKNL = konfigApp.idKpknl;
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.idPemohon : formSkAspBmnUbah.idPemohon);
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.rgJenisAset.EditValue.ToString() : formSkAspBmnUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teJabatan.Text.Trim() : formSkAspBmnUbah.teJabatan.Text.Trim());
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teTahunAnggaran.Text : formSkAspBmnUbah.teTahunAnggaran.Text);
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teNmPenerbitSk.EditValue.ToString() : formSkAspBmnUbah.teNmPenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.kodePenerbitSkDetail : formSkAspBmnUbah.kodePenerbitSkDetail);
                parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkAspBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnTambah.teNilaiPenetapan.Text)) : (formSkAspBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnUbah.teNilaiPenetapan.Text)));
                parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teNipPenandaTangan.Text : formSkAspBmnUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teNamaPenandaTangan.Text : formSkAspBmnUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teNmPenerbitSk.Text : formSkAspBmnUbah.teNmPenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teNamaInstansi.Text : formSkAspBmnUbah.teNamaInstansi.Text);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_penggantiChar);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teNomorSk.Text.Trim() : formSkAspBmnUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teTanggalSk.EditValue : formSkAspBmnUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teJenisPemohon.Text : formSkAspBmnUbah.teJenisPemohon.Text);
                //parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_JANGKA_WAKTUSpecified = true;
                //parInp.P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkAspBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnTambah.teJangkaWaktu.Text)) : (formSkAspBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnUbah.teJangkaWaktu.Text))); ;
                //parInp.P_FILE_SK = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teFileSk.Text : formSkAspBmnUbah.teFileSk.Text);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkAspBmnTambah.filePath) : konfigApp.FileToByteArray(formSkAspBmnUbah.filePath));
                //parInp.P_PERUNTUKAN = "";
                //parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teTahunAnggaran.Text : formSkAspBmnUbah.teTahunAnggaran.Text);
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teUraianKeputusan.Text : formSkAspBmnUbah.teUraianKeputusan.Text);
                //KD_KL, UR_KL, ALAMAT_KL --> belum masuk ke parameter input
                parInp.P_KD_KL = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.teKodeKl.Text : formSkAspBmnUbah.teKodeKl.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkAspBmnTambah.tipePengelola : formSkAspBmnUbah.tipePengelola);


                ambilDataRskAspBmn = new SvcWasdalPSPBMNLAINCrud.execute_pttClient();
                ambilDataRskAspBmn.Open();
                ambilDataRskAspBmn.Beginexecute(parInp, new AsyncCallback(cudRskAspBmn), "");
            }
            catch (Exception e)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void cudRskAspBmn(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataRskAspBmn = ambilDataRskAspBmn.Endexecute(result);
                ambilDataRskAspBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsAspBmn(ubahDsAspBmn), dOutAmbilDataRskAspBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsAspBmn(SvcWasdalPSPBMNLAINCrud.OutputParameters dataOutAspBmnCrud);

        private void ubahDsAspBmn(SvcWasdalPSPBMNLAINCrud.OutputParameters dataOutAspBmnCrud)
        {
            if (dataOutAspBmnCrud.PO_RESULT == "Y")
            {
                SvcWasdalPSPBMNLAINSkSelect.WASDALSROW_READ_WASDAL_PSP_LAIN dataPenyama = new SvcWasdalPSPBMNLAINSkSelect.WASDALSROW_READ_WASDAL_PSP_LAIN();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                if (dsGridRskAspBmn == null) dsGridRskAspBmn = new ArrayList();
                switch (modeCrud.ToString())
                {
                    case "C":
                        dataPenyama.ID_PEMOHON = formSkAspBmnTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkAspBmnTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkAspBmnTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkAspBmnTambah.teJabatan.Text;
                        //dataPenyama.KD_KL = formSkAspBmnTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkAspBmnTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkAspBmnTambah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkAspBmnTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkAspBmnTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkAspBmnTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkAspBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkAspBmnTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkAspBmnTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkAspBmnTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkAspBmnTambah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkAspBmnTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskAspBmn == null ? 1 : dsGridRskAspBmn.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkAspBmnTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkAspBmnTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkAspBmnTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.KD_KL = formSkAspBmnTambah.teKodeKl.Text;
                        dataPenyama.UR_KL = formSkAspBmnTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkAspBmnTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkAspBmnTambah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkAspBmnTambah.teAlamatPihakLain.Text;
                        //dataPenyama.JANGKA_WAKTU = (formSkAspBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnTambah.teJangkaWaktu.Text));
                        //dataPenyama.NM_PHK_LAIN = formSkAspBmnTambah.teNamaInstansi.Text;
                        dsGridRskAspBmn.Add(dataPenyama);
                        formSkAspBmnTambah.gcDaftarAset.Enabled = true;
                        formSkAspBmnTambah.sbTambah.Enabled = true;
                        formSkAspBmnTambah.sbHapus.Enabled = true;
                        formSkAspBmnTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkAspBmnTambah.teNomorSk.Properties.ReadOnly = false;
                        formSkAspBmnTambah.teTanggalSk.Properties.ReadOnly = false;
                        formSkAspBmnTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkAspBmnTambah.sbCariPemohon.Enabled = false;
                        formSkAspBmnTambah.sbRefresh.Enabled = true;
                        formSkAspBmnTambah.sbValidasi.Enabled = true;
                        formSkAspBmnTambah.teNmPenerbitSk.Properties.ReadOnly = true;
                        formSkAspBmnTambah.cePilihSemua.Enabled = true;
                        formSkAspBmnTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkAspBmnTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL = formSkAspBmnUbah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkAspBmnUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkAspBmnUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkAspBmnUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkAspBmnUbah.teJabatan.Text;
                        //dataPenyama.KD_KL = formSkAspBmnUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkAspBmnUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkAspBmnUbah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkAspBmnUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkAspBmnUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkAspBmnUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkAspBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkAspBmnUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkAspBmn.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkAspBmnUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkAspBmnUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkAspBmnUbah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkAspBmnUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkAspBmn.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkAspBmn.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkAspBmnUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkAspBmn.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkAspBmn.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkAspBmnUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkAspBmnUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkAspBmn.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkAspBmn.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkAspBmn.dataTerpilih.TOTAL_DATA;
                        //dataPenyama.UR_KL = formSkAspBmnUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkAspBmnUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkAspBmnUbah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkAspBmnUbah.teAlamatPihakLain.Text;
                        //dataPenyama.JANGKA_WAKTU = (formSkAspBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkAspBmnUbah.teJangkaWaktu.Text));
                        //dataPenyama.NM_PHK_LAIN = formSkAspBmnUbah.teNamaKl.Text;
                        int _indeksData = dsGridRskAspBmn.IndexOf(gridSkAspBmn.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskAspBmn.Remove(gridSkAspBmn.dataTerpilih);
                        dsGridRskAspBmn.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskAspBmn.Remove(gridSkAspBmn.dataTerpilih);
                        break;
                }
                gridSkAspBmn.dsDataSource = dsGridRskAspBmn;
                gridSkAspBmn.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data ASP BMN


        #endregion

        #region psbmn
        ucRskPsBmnGrid gridSkPsBmn;
        ucRskPsBmnForm formSkPsBmnTambah;
        ucRskPsBmnForm formSkPsBmnUbah;
        ucRskPsBmnForm formSkPsBmnDetail;
        private ArrayList dsGridRskPsBmn;
        SvcWasdalPSPBMNLAINSkSelect.OutputParameters dOutRskPsBmn;
        SvcWasdalPSPBMNLAINSkSelect.execute_pttClient ambilRskPsBmn;
        //SvcWasdalLAINCrud.OutputParameters dOutAmbilDataPsBmn;
        //SvcWasdalLAINCrud.call_pttClient ambilDataPsBmn;
        SvcWasdalPSPBMNLAINCrud.OutputParameters dOutAmbilDataPsBmn;
        SvcWasdalPSPBMNLAINCrud.execute_pttClient ambilDataPsBmn;

        private void setEventTombolRskPsBmn()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskPsBmnTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskPsBmnUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskPsBmnHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskPsBmnDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskPsBmnKembaliKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskPsBmnRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskPsBmnMoreDataKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKembaliKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKeluarKlik);
        }

        private void initGridSkPsBmn()
        {
            //if (gridSkPsBmn == null)
            //{
            gridSkPsBmn = new AppPengguna.KKW.RSK.ucRskPsBmnGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskPsBmn),
                detailDataGrid = new DetailDataGrid(bbiRskPsBmnUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskPsBmn();
            setPanel(gridSkPsBmn);
        }

        #region --++ Tombol Ribbon
        private void bbiRskPsBmnTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan05), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkPsBmnTambah == null)
                {
                    formSkPsBmnTambah = new AppPengguna.KKW.RSK.ucRskPsBmnForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPsBmn)
                    };
                }
                setPanel(formSkPsBmnTambah);
                formSkPsBmnTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskPsBmnUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPsBmn.dataTerpilih != null)
            {
                if (formSkPsBmnUbah == null)
                {
                    formSkPsBmnUbah = new AppPengguna.KKW.RSK.ucRskPsBmnForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPsBmn)
                    };
                }
                formSkPsBmnUbah.dataTerpilih = gridSkPsBmn.dataTerpilih;
                setPanel(formSkPsBmnUbah);
                formSkPsBmnUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPsBmnHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPsBmn.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkPsBmn.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        //SvcWasdalPSPBMNCrud.InputParameters parInp = new SvcWasdalPSPBMNCrud.InputParameters();
                        SvcWasdalPSPBMNLAINCrud.InputParameters parInp = new SvcWasdalPSPBMNLAINCrud.InputParameters();
                        parInp.P_ID_SK_WASDALSpecified = true;
                        parInp.P_ID_SK_WASDAL = gridSkPsBmn.dataTerpilih.ID_SK_WASDAL;
                        parInp.P_ID_PEMOHON = gridSkPsBmn.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkPsBmn.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkPsBmn.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkPsBmn.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkPsBmn.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkPsBmn.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkPsBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NILAI_PENETAPAN = gridSkPsBmn.dataTerpilih.NILAI_PENETAPAN;
                        parInp.P_NILAI_PENETAPANSpecified = true;
                        parInp.P_NIP_PENANDATANGAN = gridSkPsBmn.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkPsBmn.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkPsBmn.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkPsBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkPsBmn.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkPsBmn.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkPsBmn.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkPsBmn.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkPsBmn.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkPsBmn.dataTerpilih.UR_SATKER;
                        ambilDataPsBmn = new SvcWasdalPSPBMNLAINCrud.execute_pttClient();
                        ambilDataPsBmn.Open();
                        ambilDataPsBmn.Beginexecute(parInp, new AsyncCallback(cudRskPsBmn), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskPsBmnDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPsBmn.dataTerpilih != null)
            {
                if (formSkPsBmnDetail == null)
                {
                    formSkPsBmnDetail = new AppPengguna.KKW.RSK.ucRskPsBmnForm("A");
                    formSkPsBmnDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkPsBmnDetail.dataTerpilih = gridSkPsBmn.dataTerpilih;
                setPanel(formSkPsBmnDetail);
                formSkPsBmnDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPsBmnKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkPsBmn);
            setTombolRskGrid();
            gridSkPsBmn.dsDataSource = dsGridRskPsBmn;
            gridSkPsBmn.displayData();
        }

        private void bbiRskPsBmnRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkPsBmn.teNamaKolom.Text = "";
            gridSkPsBmn.teCari.Text = "";
            gridSkPsBmn.fieldDicari = "";
            gridSkPsBmn.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskPsBmn();
        }

        private void bbiRskPsBmnMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskPsBmn();
            }
        }
        #endregion --++ Tombol Ribbon

        private void nbiRskPsBmn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan05;
            konfigApp.kdPelayanan = "05";
            konfigApp.namaPelayanan = konfigApp.namaLayanan05;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkPsBmn();
            modeCari = false;
            gridSkPsBmn.teNamaKolom.Text = "";
            gridSkPsBmn.teCari.Text = "";
            gridSkPsBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskPsBmn();
        }

        #region --++ Ambil Data PS BMN
        private void getInitRskPsBmn()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNLAINSkSelect.InputParameters parInp = new SvcWasdalPSPBMNLAINSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskPsBmn = new SvcWasdalPSPBMNLAINSkSelect.execute_pttClient();
                ambilRskPsBmn.Open();
                ambilRskPsBmn.Beginexecute(parInp, new AsyncCallback(getRskPsBmn), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskPsBmn(IAsyncResult result)
        {
            try
            {
                dOutRskPsBmn = ambilRskPsBmn.Endexecute(result);
                ambilRskPsBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskPsBmn(dsRskPsBmn), dOutRskPsBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskPsBmn(SvcWasdalPSPBMNLAINSkSelect.OutputParameters dataOut);

        private void dsRskPsBmn(SvcWasdalPSPBMNLAINSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_PSP_LAIN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_READ_WASDAL_PSP_LAIN[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
            }

            if (dataInisial == true)
            {
                dsGridRskPsBmn = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_PSP_LAIN[i].IS_TB = (dataOut.SF_READ_WASDAL_PSP_LAIN[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskPsBmn.Add(dataOut.SF_READ_WASDAL_PSP_LAIN[i]);
            }
            gridSkPsBmn.labelTotData.Text = "";
            gridSkPsBmn.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkPsBmn.sbCariOnline.Enabled = !modeCari;
            gridSkPsBmn.dsDataSource = dsGridRskPsBmn;
            gridSkPsBmn.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkPsBmn.teNamaKolom.Text.Trim();
                string xDua = gridSkPsBmn.teCari.Text.Trim();
                string xTiga = gridSkPsBmn.fieldDicari;
                gridSkPsBmn.gvGridSk.ClearColumnsFilter();
                gridSkPsBmn.teNamaKolom.Text = xSatu;
                gridSkPsBmn.teCari.Text = xDua;
                gridSkPsBmn.fieldDicari = xTiga;
            }
            else
                gridSkPsBmn.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskPsBmn(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskPsBmn();
        }
        #endregion Ambil PSP BMN

        #region --++ Simpan Data PS BMN


        private void simpanDataRskPsBmn(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPSPBMNLAINCrud.InputParameters parInp = new SvcWasdalPSPBMNLAINCrud.InputParameters();
                parInp.P_ID_SK_WASDALSpecified = true;
                parInp.P_ID_SK_WASDAL = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.idSkWasdal : formSkPsBmnUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.idPemohon : formSkPsBmnUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.rgJenisAset.EditValue.ToString() : formSkPsBmnUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teJabatan.Text.Trim() : formSkPsBmnUbah.teJabatan.Text.Trim());
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teTahunAnggaran.Text : formSkPsBmnUbah.teTahunAnggaran.Text);
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teNmPenerbitSk.Properties.GetDataSourceValue("KD_INSTANSI", formSkPsBmnTambah.teNmPenerbitSk.ItemIndex).ToString() : formSkPsBmnTambah.teNmPenerbitSk.Properties.GetDataSourceValue("KD_INSTANSI", formSkPsBmnUbah.teNmPenerbitSk.ItemIndex).ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.kodePenerbitSkDetail : formSkPsBmnUbah.kodePenerbitSkDetail);
                parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkPsBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPsBmnTambah.teNilaiPenetapan.Text)) : (formSkPsBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPsBmnUbah.teNilaiPenetapan.Text)));
                parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teNipPenandaTangan.Text : formSkPsBmnUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teNamaPenandaTangan.Text : formSkPsBmnUbah.teNamaPenandaTangan.Text);
                //parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.tePeruntukan.Text : formSkPsBmnUbah.tePeruntukan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teNmPenerbitSk.Text : formSkPsBmnUbah.teNmPenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teNamaInstansi.Text : formSkPsBmnUbah.teNamaInstansi.Text);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_penggantiChar);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teNomorSk.Text.Trim() : formSkPsBmnUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teTanggalSk.EditValue : formSkPsBmnUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teJenisPemohon.Text : formSkPsBmnUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.tipePengelola : formSkPsBmnUbah.tipePengelola);
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teUraianKeputusan.Text : formSkPsBmnUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teTahunAnggaran.Text.Trim() : formSkPsBmnUbah.teTahunAnggaran.Text.Trim());
                parInp.P_KD_KL = ((_mode == "C" || _mode == "CU") ? formSkPsBmnTambah.teKodeKl.Text.Trim() : formSkPsBmnUbah.teKodeKl.Text.Trim());
                ambilDataPsBmn = new SvcWasdalPSPBMNLAINCrud.execute_pttClient();
                ambilDataPsBmn.Open();
                ambilDataPsBmn.Beginexecute(parInp, new AsyncCallback(cudRskPsBmn), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskPsBmn(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataPsBmn = ambilDataPsBmn.Endexecute(result);
                ambilDataPsBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsPsBmn(this.ubahDsPsBmn), dOutAmbilDataPsBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsPsBmn(SvcWasdalPSPBMNLAINCrud.OutputParameters dataOutKpknlCrud);

        private void ubahDsPsBmn(SvcWasdalPSPBMNLAINCrud.OutputParameters dataOutKpknlCrud)
        {
            if (dataOutKpknlCrud.PO_RESULT == "Y")
            {
                SvcWasdalPSPBMNLAINSkSelect.WASDALSROW_READ_WASDAL_PSP_LAIN dataPenyama = new SvcWasdalPSPBMNLAINSkSelect.WASDALSROW_READ_WASDAL_PSP_LAIN();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                if (dsGridRskPsBmn == null) dsGridRskPsBmn = new ArrayList();
                switch (modeCrud.ToString())
                {
                    case "C":
                        dataPenyama.ID_PEMOHON = formSkPsBmnTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPsBmnTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkPsBmnTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPsBmnTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkPsBmnTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPsBmnTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPsBmnTambah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPsBmnTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPsBmnTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPsBmnTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPsBmnTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPsBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPsBmnTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPsBmnTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkPsBmnTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPsBmnTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPsBmnTambah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPsBmnTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskPsBmn == null ? 1 : dsGridRskPsBmn.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkPsBmnTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPsBmnTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPsBmnTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkPsBmnTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPsBmnTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPsBmnTambah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkPsBmnTambah.teAlamatPihakLain.Text;
                        //dataPenyama.JANGKA_WAKTU = (formSkPsBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPsBmnTambah.teJangkaWaktu.Text));
                        //dataPenyama.NM_PHK_LAIN = formSkPsBmnTambah.teNamaKl.Text;
                        dsGridRskPsBmn.Add(dataPenyama);
                        formSkPsBmnTambah.gcDaftarAset.Enabled = true;
                        formSkPsBmnTambah.sbTambah.Enabled = true;
                        formSkPsBmnTambah.sbHapus.Enabled = true;
                        formSkPsBmnTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkPsBmnTambah.teNomorSk.Properties.ReadOnly = false;
                        formSkPsBmnTambah.teTanggalSk.Properties.ReadOnly = false;
                        formSkPsBmnTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkPsBmnTambah.sbCariPemohon.Enabled = false;
                        formSkPsBmnTambah.sbRefresh.Enabled = true;
                        formSkPsBmnTambah.sbValidasi.Enabled = true;
                        formSkPsBmnTambah.teNmPenerbitSk.Properties.ReadOnly = true;
                        formSkPsBmnTambah.cePilihSemua.Enabled = true;
                        formSkPsBmnTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkPsBmnTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL = formSkPsBmnUbah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkPsBmnUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPsBmnUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkPsBmnUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPsBmnUbah.teJabatan.Text;
                        //dataPenyama.KD_KL = formSkPsBmnUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPsBmnUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPsBmnUbah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPsBmnUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPsBmnUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPsBmnUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPsBmnUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPsBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPsBmnUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPsBmnUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkPsBmn.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkPsBmnUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPsBmnUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPsBmnUbah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPsBmnUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkPsBmn.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkPsBmn.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkPsBmnUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkPsBmn.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkPsBmn.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPsBmnUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPsBmnUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkPsBmn.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkPsBmn.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkPsBmn.dataTerpilih.TOTAL_DATA;
                        //dataPenyama.UR_KL = formSkPsBmnUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPsBmnUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPsBmnUbah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkPsBmnUbah.teAlamatPihakLain.Text;
                        //dataPenyama.JANGKA_WAKTU = (formSkPsBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPsBmnUbah.teJangkaWaktu.Text));
                        //dataPenyama.NM_PHK_LAIN = formSkPsBmnUbah.teNamaKl.Text;
                        int _indeksData = dsGridRskPsBmn.IndexOf(gridSkPsBmn.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskPsBmn.Remove(gridSkPsBmn.dataTerpilih);
                        dsGridRskPsBmn.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskPsBmn.Remove(gridSkPsBmn.dataTerpilih);
                        break;
                }
                gridSkPsBmn.dsDataSource = dsGridRskPsBmn;
                gridSkPsBmn.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data PSP BMN

        #endregion

        #region -->> [06] Sub Menu SEWA BMN
        ucRskSewaBmnGrid gridSkSewaBmn;
        ucRskSewaBmnForm formSkSewaBmnTambah;
        ucRskSewaBmnForm formSkSewaBmnUbah;
        ucRskSewaBmnForm formSkSewaBmnDetail;
        private ArrayList dsGridRskSewaBmn;
        SvcWasdalManfaatSkSelect.OutputParameters dOutRskSewaBmn;
        SvcWasdalManfaatSkSelect.execute_pttClient ambilRskSewaBmn;

        private void setEventTombolRskSewaBmn()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnMoreDataKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnKeluarKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskSewaBmnKembaliKlik);
        }

        private void initGridSkSewaBmn()
        {
            //if (gridSkSewaBmn == null)
            //{
            gridSkSewaBmn = new ucRskSewaBmnGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskSewaBmn),
                detailDataGrid = new DetailDataGrid(bbiRskSewaBmnUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskSewaBmn();
            setPanel(gridSkSewaBmn);
        }

        #region --++ Tombol Ribbon Sewa BMN
        private void bbiRskSewaBmnTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan06), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkSewaBmnTambah == null)
                {
                    formSkSewaBmnTambah = new ucRskSewaBmnForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskSewaBmn)
                    };
                }
                setPanel(formSkSewaBmnTambah);
                formSkSewaBmnTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskSewaBmnUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkSewaBmn.dataTerpilih != null)
            {
                if (formSkSewaBmnUbah == null)
                {
                    formSkSewaBmnUbah = new ucRskSewaBmnForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskSewaBmn)
                    };
                }
                formSkSewaBmnUbah.dataTerpilih = gridSkSewaBmn.dataTerpilih;
                setPanel(formSkSewaBmnUbah);
                formSkSewaBmnUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskSewaBmnHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkSewaBmn.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkSewaBmn.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                        parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                        parInp.P_ID_SK_WASDAL_MANFAAT = gridSkSewaBmn.dataTerpilih.ID_SK_WASDAL_MANFAAT;
                        parInp.P_ID_PEMOHON = gridSkSewaBmn.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkSewaBmn.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_ID_SATKER = konfigApp.idSatker;
                        parInp.P_KD_SATKER = konfigApp.kodeSatker;
                        parInp.P_UR_SATKER = konfigApp.namaSatker;
                        parInp.P_IS_TB = gridSkSewaBmn.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkSewaBmn.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkSewaBmn.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkSewaBmn.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkSewaBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkSewaBmn.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkSewaBmn.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkSewaBmn.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkSewaBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkSewaBmn.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TGL_SK = konfigApp.DateToString(gridSkSewaBmn.dataTerpilih.TGL_SK);
                        parInp.P_THN_ANG = gridSkSewaBmn.dataTerpilih.THN_ANG;
                        parInp.P_ID_KPKNL = konfigApp.idKpknl;
                        parInp.P_KD_KL = konfigApp.kodeKl;
                        parInp.P_TIPE_PEMOHON = gridSkSewaBmn.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkSewaBmn.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_DARI_TGL = konfigApp.DateToString(gridSkSewaBmn.dataTerpilih.DARI_TGL);
                        parInp.P_SD_TGL = konfigApp.DateToString(gridSkSewaBmn.dataTerpilih.SD_TGL);
                        parInp.P_NM_PHK_LAIN = gridSkSewaBmn.dataTerpilih.NM_PHK_LAIN;
                        parInp.P_ALAMAT_PHK_LAIN = gridSkSewaBmn.dataTerpilih.ALAMAT_PHK_LAIN;
                        parInp.P_PERUNTUKAN = gridSkSewaBmn.dataTerpilih.PERUNTUKAN;
                        parInp.P_PERIODE = gridSkSewaBmn.dataTerpilih.PERIODE;
                        parInp.P_JNS_MITRA = "-";
                        parInp.P_KETERANGAN = "-";
                        //parInp.P_TIPE_PENGELOLA = "03";
                        parInp.P_TGL_CREATED = null;
                        ambilDataSewaBmn = new SvcWasdalManfaatCud.execute_pttClient();
                        ambilDataSewaBmn.Open();
                        ambilDataSewaBmn.Beginexecute(parInp, new AsyncCallback(cudRskSewaBmn), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskSewaBmnDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkSewaBmn.dataTerpilih != null)
            {
                if (formSkSewaBmnDetail == null)
                {
                    formSkSewaBmnDetail = new ucRskSewaBmnForm("A");
                    formSkSewaBmnDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkSewaBmnDetail.dataTerpilih = gridSkSewaBmn.dataTerpilih;
                setPanel(formSkSewaBmnDetail);
                formSkSewaBmnDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskSewaBmnKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkSewaBmn);
            setTombolRskGrid();
            gridSkSewaBmn.dsDataSource = dsGridRskSewaBmn;
            gridSkSewaBmn.displayData();
        }

        private void bbiRskSewaBmnRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkSewaBmn.teNamaKolom.Text = "";
            gridSkSewaBmn.teCari.Text = "";
            gridSkSewaBmn.fieldDicari = "";
            gridSkSewaBmn.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskSewaBmn();
        }

        private void bbiRskSewaBmnMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskSewaBmn();
            }
        }

        private void bbiRskSewaBmnKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskSewaBmnKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }

        #endregion --++ Tombol Ribbon

        private void nbiRskSewaBmn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan06;
            konfigApp.kdPelayanan = "06";
            konfigApp.namaPelayanan = konfigApp.namaLayanan06;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkSewaBmn();
            modeCari = false;
            gridSkSewaBmn.teNamaKolom.Text = "";
            gridSkSewaBmn.teCari.Text = "";
            gridSkSewaBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskSewaBmn();
        }

        #region --++ Ambil Data Sewa BMN
        private void getInitRskSewaBmn()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatSkSelect.InputParameters parInp = new SvcWasdalManfaatSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskSewaBmn = new SvcWasdalManfaatSkSelect.execute_pttClient();
                ambilRskSewaBmn.Open();
                ambilRskSewaBmn.Beginexecute(parInp, new AsyncCallback(getRskSewaBmn), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskSewaBmn(IAsyncResult result)
        {
            try
            {
                dOutRskSewaBmn = ambilRskSewaBmn.Endexecute(result);
                ambilRskSewaBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskSewaBmn(dsRskSewaBmn), dOutRskSewaBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskSewaBmn(SvcWasdalManfaatSkSelect.OutputParameters dataOut);

        private void dsRskSewaBmn(SvcWasdalManfaatSkSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_READ_WASDAL_MANFAAT.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_READ_WASDAL_MANFAAT[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRskSewaBmn = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB = (dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dataOut.SF_READ_WASDAL_MANFAAT[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_READ_WASDAL_MANFAAT[i].TGL_SK);
                dataOut.SF_READ_WASDAL_MANFAAT[i].PERIODE = konfigApp.setPeriode(dataOut.SF_READ_WASDAL_MANFAAT[i].PERIODE);
                dsGridRskSewaBmn.Add(dataOut.SF_READ_WASDAL_MANFAAT[i]);
            }
            gridSkSewaBmn.labelTotData.Text = "";
            gridSkSewaBmn.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkSewaBmn.sbCariOnline.Enabled = !modeCari;
            gridSkSewaBmn.dsDataSource = dsGridRskSewaBmn;
            gridSkSewaBmn.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkSewaBmn.teNamaKolom.Text.Trim();
                string xDua = gridSkSewaBmn.teCari.Text.Trim();
                string xTiga = gridSkSewaBmn.fieldDicari;
                gridSkSewaBmn.gvGridSk.ClearColumnsFilter();
                gridSkSewaBmn.teNamaKolom.Text = xSatu;
                gridSkSewaBmn.teCari.Text = xDua;
                gridSkSewaBmn.fieldDicari = xTiga;
            }
            else
                gridSkSewaBmn.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskSewaBmn(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskSewaBmn();
        }
        #endregion Ambil Sewa BMN

        #region --++ Simpan Data Sewa BMN
        SvcWasdalManfaatCud.OutputParameters dOutAmbilDataSewaBmn;
        SvcWasdalManfaatCud.execute_pttClient ambilDataSewaBmn;

        private void simpanDataRskSewaBmn(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                parInp.P_ID_SK_WASDAL_MANFAAT = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.idSkWasdal : formSkSewaBmnUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.idPemohon : formSkSewaBmnUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.rgJenisAset.EditValue.ToString() : formSkSewaBmnUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teJabatan.Text.Trim() : formSkSewaBmnUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.lePenerbitSk.EditValue.ToString() : formSkSewaBmnUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.kodePenerbitSkDetail : formSkSewaBmnUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkSewaBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnTambah.teNilaiPenetapan.Text)) : (formSkSewaBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teNipPenandaTangan.Text : formSkSewaBmnUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teNamaPenandaTangan.Text : formSkSewaBmnUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.lePenerbitSk.Text : formSkSewaBmnUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teNmPenerbitSkDetail.Text : formSkSewaBmnUbah.teNmPenerbitSkDetail.Text);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teNomorSk.Text.Trim() : formSkSewaBmnUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teTanggalSk.EditValue : formSkSewaBmnUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teJenisPemohon.Text : formSkSewaBmnUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.tipePengelola : formSkSewaBmnUbah.tipePengelola);
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teUraianKeputusan.Text : formSkSewaBmnUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teTahunAnggaran.Text : formSkSewaBmnUbah.teTahunAnggaran.Text);
                parInp.P_DARI_TGL = (_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teDariTgl.Text : formSkSewaBmnUbah.teDariTgl.Text;
                parInp.P_SD_TGL = (_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teSampaiTgl.Text : formSkSewaBmnUbah.teSampaiTgl.Text;

                parInp.P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teAlamatPihakLain.Text : formSkSewaBmnUbah.teAlamatPihakLain.Text);
                parInp.P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkSewaBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnTambah.teJangkaWaktu.Text)) : (formSkSewaBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnUbah.teJangkaWaktu.Text)));
                parInp.P_JNS_MITRA = null;
                parInp.P_NM_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.teNamaPihakLain.Text : formSkSewaBmnUbah.teNamaPihakLain.Text);
                parInp.P_PERIODE = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.konversiPeriode() : formSkSewaBmnUbah.konversiPeriode());
                parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.tePeruntukan.Text : formSkSewaBmnUbah.tePeruntukan.Text);
                parInp.P_JANGKA_WAKTUSpecified = true;
                parInp.P_KETERANGAN = null;
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkSewaBmnTambah.tipePengelola : formSkSewaBmnUbah.tipePengelola);
                ambilDataSewaBmn = new SvcWasdalManfaatCud.execute_pttClient();
                ambilDataSewaBmn.Open();
                ambilDataSewaBmn.Beginexecute(parInp, new AsyncCallback(cudRskSewaBmn), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskSewaBmn(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataSewaBmn = ambilDataSewaBmn.Endexecute(result);
                ambilDataSewaBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsSewaBmn(this.ubahDsSewaBmn), dOutAmbilDataSewaBmn);
            }
            catch (Exception ex)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsSewaBmn(SvcWasdalManfaatCud.OutputParameters dataOutSewaBmnCrud);

        private void ubahDsSewaBmn(SvcWasdalManfaatCud.OutputParameters dataOutSewaBmnCrud)
        {
            if (dataOutSewaBmnCrud.PO_RESULT == "Y")
            {
                SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT dataPenyama = new SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.JANGKA_WAKTUSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.DARI_TGLSpecified = true;
                dataPenyama.SD_TGLSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                if (dsGridRskSewaBmn == null) dsGridRskSewaBmn = new ArrayList();
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ALAMAT_PHK_LAIN = formSkSewaBmnTambah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkSewaBmnTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkSewaBmnTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkSewaBmnTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkSewaBmnTambah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkSewaBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnTambah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkSewaBmnTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkSewaBmnTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkSewaBmnTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkSewaBmnTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkSewaBmnTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkSewaBmnTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkSewaBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkSewaBmnTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkSewaBmnTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkSewaBmnTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkSewaBmnTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkSewaBmnTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskSewaBmn == null ? 1 : dsGridRskSewaBmn.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkSewaBmnTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.SD_TGL = konfigApp.ToDate(formSkSewaBmnTambah.teSampaiTgl.Text);
                        dataPenyama.DARI_TGL = konfigApp.ToDate(formSkSewaBmnTambah.teDariTgl.Text);
                        //dataPenyama.PERIODE = formSkSewaBmnTambah.konversiPeriode();
                        //dataPenyama.PERUNTUKAN = formSkSewaBmnTambah.tePeruntukan.Text;
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = konfigApp.ToDate(formSkSewaBmnTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkSewaBmnTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkSewaBmnTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkSewaBmnTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkSewaBmnTambah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkSewaBmnTambah.teAlamatPihakLain.Text;
                        dataPenyama.NM_PHK_LAIN = formSkSewaBmnTambah.teNamaPihakLain.Text;
                        dataPenyama.PERIODE = formSkSewaBmnTambah.konversiPeriode();
                        dataPenyama.THN_ANG = formSkSewaBmnTambah.teTahunAnggaran.Text;
                        dsGridRskSewaBmn.Add(dataPenyama);
                        formSkSewaBmnTambah.gcDaftarAset.Enabled = true;
                        formSkSewaBmnTambah.sbTambah.Enabled = true;
                        formSkSewaBmnTambah.sbHapus.Enabled = true;
                        formSkSewaBmnTambah.sbValidasi.Enabled = true;
                        formSkSewaBmnTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkSewaBmnTambah.teNomorSk.Properties.ReadOnly = false;
                        formSkSewaBmnTambah.teTanggalSk.Properties.ReadOnly = false;
                        formSkSewaBmnTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkSewaBmnTambah.sbCariPemohon.Enabled = false;
                        formSkSewaBmnTambah.sbRefresh.Enabled = true;
                        formSkSewaBmnTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkSewaBmnTambah.cePilihSemua.Enabled = true;
                        formSkSewaBmnTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkSewaBmnTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_MANFAAT = formSkSewaBmnUbah.idSkWasdal;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkSewaBmnUbah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkSewaBmnUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkSewaBmnUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkSewaBmnUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkSewaBmnUbah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkSewaBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnUbah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkSewaBmnUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkSewaBmnUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkSewaBmnUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkSewaBmnUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkSewaBmnUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkSewaBmnUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkSewaBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkSewaBmnUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkSewaBmnUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkSewaBmn.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkSewaBmnUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkSewaBmnUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkSewaBmnUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkSewaBmnUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkSewaBmn.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkSewaBmn.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkSewaBmnUbah.teNomorSk.Text;
                        dataPenyama.DARI_TGL = formSkSewaBmnUbah.teDariTgl.DateTime;
                        dataPenyama.SD_TGL = formSkSewaBmnUbah.teSampaiTgl.DateTime;
                        //dataPenyama.PERIODE = formSkSewaBmnUbah.konversiPeriode();
                        //dataPenyama.PERUNTUKAN = formSkSewaBmnUbah.tePeruntukan.Text;
                        dataPenyama.STATUS_BMN = gridSkSewaBmn.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkSewaBmn.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = formSkSewaBmnUbah.teTanggalSk.DateTime;
                        dataPenyama.TIPE_PEMOHON = formSkSewaBmnUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkSewaBmn.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkSewaBmn.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkSewaBmn.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkSewaBmnUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkSewaBmnUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkSewaBmnUbah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkSewaBmnUbah.teAlamatPihakLain.Text;
                        dataPenyama.NM_PHK_LAIN = formSkSewaBmnUbah.teNamaPihakLain.Text;
                        dataPenyama.THN_ANG = formSkSewaBmnUbah.teTahunAnggaran.Text;
                        dataPenyama.PERIODE = formSkSewaBmnUbah.konversiPeriode();
                        dataPenyama.PERUNTUKAN = formSkSewaBmnUbah.tePeruntukan.Text;
                        int _indeksData = dsGridRskSewaBmn.IndexOf(gridSkSewaBmn.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskSewaBmn.Remove(gridSkSewaBmn.dataTerpilih);
                        dsGridRskSewaBmn.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskSewaBmn.Remove(gridSkSewaBmn.dataTerpilih);
                        break;
                }
                gridSkSewaBmn.dsDataSource = dsGridRskSewaBmn;
                gridSkSewaBmn.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data PSP BMN

        #endregion

        #region -->> [07] Sub Menu Pinjam Pakai BMN
        ucRskPpBmnGrid gridSkPpBmn;
        ucRskPpBmnForm formSkPpBmnTambah;
        ucRskPpBmnForm formSkPpBmnUbah;
        ucRskPpBmnForm formSkPpBmnDetail;
        private ArrayList dsGridRskPpBmn;
        SvcWasdalManfaatSkSelect.OutputParameters dOutRskPpBmn;
        SvcWasdalManfaatSkSelect.execute_pttClient ambilRskPpBmn;

        private void setEventTombolRskPpBmn()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskPpBmnTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskPpBmnUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskPpBmnHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskPpBmnDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskPpBmnKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskPpBmnRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskPpBmnMoreDataKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPpBmnKeluarKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPpBmnKembaliKlik);
        }

        private void initGridSkPpBmn()
        {
            //if (gridSkPpBmn == null)
            //{
            gridSkPpBmn = new ucRskPpBmnGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskPpBmn),
                detailDataGrid = new DetailDataGrid(bbiRskPpBmnUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskPpBmn();
            setPanel(gridSkPpBmn);
        }

        #region --++ Tombol Ribbon
        private void bbiRskPpBmnTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan07), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkPpBmnTambah == null)
                {
                    formSkPpBmnTambah = new ucRskPpBmnForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPpBmn)
                    };
                }
                setPanel(formSkPpBmnTambah);
                formSkPpBmnTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskPpBmnUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPpBmn.dataTerpilih != null)
            {
                if (formSkPpBmnUbah == null)
                {
                    formSkPpBmnUbah = new ucRskPpBmnForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPpBmn)
                    };
                }
                formSkPpBmnUbah.dataTerpilih = gridSkPpBmn.dataTerpilih;
                setPanel(formSkPpBmnUbah);
                formSkPpBmnUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPpBmnHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPpBmn.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkPpBmn.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                        parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                        parInp.P_ID_SK_WASDAL_MANFAAT = gridSkPpBmn.dataTerpilih.ID_SK_WASDAL_MANFAAT;
                        parInp.P_ID_PEMOHON = gridSkPpBmn.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkPpBmn.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_ID_SATKER = konfigApp.idSatker;
                        parInp.P_KD_SATKER = konfigApp.kodeSatker;
                        parInp.P_UR_SATKER = konfigApp.namaSatker;
                        parInp.P_IS_TB = gridSkPpBmn.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkPpBmn.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkPpBmn.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkPpBmn.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkPpBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkPpBmn.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkPpBmn.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkPpBmn.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkPpBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkPpBmn.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TGL_SK = konfigApp.DateToString(gridSkPpBmn.dataTerpilih.TGL_SK);
                        parInp.P_THN_ANG = gridSkPpBmn.dataTerpilih.THN_ANG;
                        parInp.P_ID_KPKNL = konfigApp.idKpknl;
                        parInp.P_KD_KL = konfigApp.kodeKl;
                        parInp.P_TIPE_PEMOHON = gridSkPpBmn.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkPpBmn.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_DARI_TGL = konfigApp.DateToString(gridSkPpBmn.dataTerpilih.DARI_TGL);
                        parInp.P_SD_TGL = konfigApp.DateToString(gridSkPpBmn.dataTerpilih.SD_TGL);
                        parInp.P_NM_PHK_LAIN = gridSkPpBmn.dataTerpilih.NM_PHK_LAIN;
                        parInp.P_ALAMAT_PHK_LAIN = gridSkPpBmn.dataTerpilih.ALAMAT_PHK_LAIN;
                        parInp.P_PERUNTUKAN = gridSkPpBmn.dataTerpilih.PERUNTUKAN;
                        parInp.P_PERIODE = gridSkPpBmn.dataTerpilih.PERIODE;
                        parInp.P_JNS_MITRA = "-";
                        parInp.P_KETERANGAN = "-";
                        //parInp.P_TIPE_PENGELOLA = "03";
                        parInp.P_TGL_CREATED = null;
                        ambilDataPpBmn1 = new SvcWasdalManfaatCud.execute_pttClient();
                        ambilDataPpBmn1.Open();
                        ambilDataPpBmn1.Beginexecute(parInp, new AsyncCallback(cudRskPpBmn), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskPpBmnDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPpBmn.dataTerpilih != null)
            {
                if (formSkPpBmnDetail == null)
                {
                    formSkPpBmnDetail = new ucRskPpBmnForm("A");
                    formSkPpBmnDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkPpBmnDetail.dataTerpilih = gridSkPpBmn.dataTerpilih;
                setPanel(formSkPpBmnDetail);
                formSkPpBmnDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPpBmnKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkPpBmn);
            setTombolRskGrid();
            gridSkPpBmn.dsDataSource = dsGridRskPpBmn;
            gridSkPpBmn.displayData();
        }

        private void bbiRskPpBmnRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkPpBmn.teNamaKolom.Text = "";
            gridSkPpBmn.teCari.Text = "";
            gridSkPpBmn.fieldDicari = "";
            gridSkPpBmn.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskPpBmn();
        }

        private void bbiRskPpBmnMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskPpBmn();
            }
        }

        private void bbiRskPpBmnKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskPpBmnKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }

        #endregion --++ Tombol Ribbon Pinjam Pakai BMN

        private void nbiRskPpBmn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan07;
            konfigApp.kdPelayanan = "07";
            konfigApp.namaPelayanan = konfigApp.namaLayanan07;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkPpBmn();
            modeCari = false;
            gridSkPpBmn.teNamaKolom.Text = "";
            gridSkPpBmn.teCari.Text = "";
            gridSkPpBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskPpBmn();
        }

        #region --++ Ambil Data PP BMN
        private void getInitRskPpBmn()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatSkSelect.InputParameters parInp = new SvcWasdalManfaatSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
             //   parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
             //" (ID_USER={1} " +
             // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
             // "OR ID_SATKER = {2} " +
             // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskPpBmn = new SvcWasdalManfaatSkSelect.execute_pttClient();
                ambilRskPpBmn.Open();
                ambilRskPpBmn.Beginexecute(parInp, new AsyncCallback(getRskPpBmn), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskPpBmn(IAsyncResult result)
        {
            try
            {
                dOutRskPpBmn = ambilRskPpBmn.Endexecute(result);
                ambilRskPpBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskPpBmn(dsRskPpBmn), dOutRskPpBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskPpBmn(SvcWasdalManfaatSkSelect.OutputParameters dataOut);

        private void dsRskPpBmn(SvcWasdalManfaatSkSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_READ_WASDAL_MANFAAT.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_READ_WASDAL_MANFAAT[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRskPpBmn = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB = (dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);

                dsGridRskPpBmn.Add(dataOut.SF_READ_WASDAL_MANFAAT[i]);
            }
            gridSkPpBmn.labelTotData.Text = "";
            gridSkPpBmn.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkPpBmn.sbCariOnline.Enabled = !modeCari;
            gridSkPpBmn.dsDataSource = dsGridRskPpBmn;
            gridSkPpBmn.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkPpBmn.teNamaKolom.Text.Trim();
                string xDua = gridSkPpBmn.teCari.Text.Trim();
                string xTiga = gridSkPpBmn.fieldDicari;
                gridSkPpBmn.gvGridSk.ClearColumnsFilter();
                gridSkPpBmn.teNamaKolom.Text = xSatu;
                gridSkPpBmn.teCari.Text = xDua;
                gridSkPpBmn.fieldDicari = xTiga;
            }
            else
                gridSkPpBmn.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskPpBmn(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskPpBmn();
        }
        #endregion Ambil PP BMN

        #region --++ Simpan Data PP BMN
        SvcWasdalManfaatCud.OutputParameters dOutAmbilDataPpBmn1;
        SvcWasdalManfaatCud.execute_pttClient ambilDataPpBmn1;

        private void simpanDataRskPpBmn(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                parInp.P_ID_SK_WASDAL_MANFAAT = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.idSkWasdal : formSkPpBmnUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.idPemohon : formSkPpBmnUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.rgJenisAset.EditValue.ToString() : formSkPpBmnUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teJabatan.Text.Trim() : formSkPpBmnUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.lePenerbitSk.EditValue.ToString() : formSkPpBmnUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.kodePenerbitSkDetail : formSkPpBmnUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkPpBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnTambah.teNilaiPenetapan.Text)) : (formSkPpBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teNipPenandaTangan.Text : formSkPpBmnUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teNamaPenandaTangan.Text : formSkPpBmnUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.lePenerbitSk.Text : formSkPpBmnUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teNmPenerbitSkDetail.Text : formSkPpBmnUbah.namaPenerbitSkDetail);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teNomorSk.Text.Trim() : formSkPpBmnUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teTanggalSk.EditValue : formSkPpBmnUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teJenisPemohon.Text : formSkPpBmnUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teUraianKeputusan.Text : formSkPpBmnUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teTahunAnggaran.Text : formSkPpBmnUbah.teTahunAnggaran.Text);

                parInp.P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teAlamatPihakLain.Text : formSkPpBmnUbah.teAlamatPihakLain.Text);
                parInp.P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkPpBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnTambah.teJangkaWaktu.Text)) : (formSkPpBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnUbah.teJangkaWaktu.Text)));
                parInp.P_JNS_MITRA = null;
                parInp.P_NM_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.teNamaPihakLain.Text : formSkPpBmnUbah.teNamaPihakLain.Text);
                parInp.P_PERIODE = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.konversiPeriode() : formSkPpBmnUbah.konversiPeriode());
                parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.tePeruntukan.Text : formSkPpBmnUbah.tePeruntukan.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkPpBmnTambah.tipePengelola : formSkPpBmnUbah.tipePengelola);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkPpBmnTambah.filePath) : konfigApp.FileToByteArray(formSkPpBmnUbah.filePath));
                parInp.P_JANGKA_WAKTUSpecified = true;
                parInp.P_KETERANGAN = null;
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                ambilDataPpBmn1 = new SvcWasdalManfaatCud.execute_pttClient();
                ambilDataPpBmn1.Open();
                ambilDataPpBmn1.Beginexecute(parInp, new AsyncCallback(cudRskPpBmn), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskPpBmn(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataPpBmn1 = ambilDataPpBmn1.Endexecute(result);
                ambilDataPpBmn1.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsPpBmn(this.ubahDsPpBmn), dOutAmbilDataPpBmn1);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsPpBmn(SvcWasdalManfaatCud.OutputParameters dataOutPpBmnCrud);

        private void ubahDsPpBmn(SvcWasdalManfaatCud.OutputParameters dataOutPpBmnCrud)
        {
            if (dataOutPpBmnCrud.PO_RESULT == "Y")
            {
                SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT dataPenyama = new SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.JANGKA_WAKTUSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                if (dsGridRskPpBmn == null) dsGridRskPpBmn = new ArrayList();
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ALAMAT_PHK_LAIN = formSkPpBmnTambah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkPpBmnTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPpBmnTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkPpBmnTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPpBmnTambah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkPpBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnTambah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkPpBmnTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPpBmnTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPpBmnTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPpBmnTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPpBmnTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPpBmnTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPpBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPpBmnTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkPpBmnTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPpBmnTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPpBmnTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPpBmnTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskPpBmn == null ? 1 : dsGridRskPpBmn.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkPpBmnTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPpBmnTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPpBmnTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkPpBmnTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPpBmnTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPpBmnTambah.teUraianKeputusan.Text;
                        dataPenyama.PERIODE = formSkPpBmnTambah.konversiPeriode();
                        dataPenyama.PERUNTUKAN = formSkPpBmnTambah.tePeruntukan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkPpBmnTambah.teNamaPihakLain.Text;
                        dataPenyama.THN_ANG = formSkPpBmnTambah.teTahunAnggaran.Text;
                        dsGridRskPpBmn.Add(dataPenyama);
                        formSkPpBmnTambah.gcDaftarAset.Enabled = true;
                        formSkPpBmnTambah.sbTambah.Enabled = true;
                        formSkPpBmnTambah.sbHapus.Enabled = true;
                        formSkPpBmnTambah.sbValidasi.Enabled = true;
                        formSkPpBmnTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkPpBmnTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkPpBmnTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkPpBmnTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkPpBmnTambah.sbCariPemohon.Enabled = false;
                        formSkPpBmnTambah.sbRefresh.Enabled = true;
                        formSkPpBmnTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkPpBmnTambah.cePilihSemua.Enabled = true;
                        formSkPpBmnTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkPpBmnTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_MANFAAT = formSkPpBmnUbah.idSkWasdal;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkPpBmnUbah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkPpBmnUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPpBmnUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkPpBmnUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPpBmnUbah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkPpBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnUbah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkPpBmnUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPpBmnUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPpBmnUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPpBmnUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPpBmnUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPpBmnUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPpBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPpBmnUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPpBmnUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkPpBmn.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkPpBmnUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPpBmnUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPpBmnUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPpBmnUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkPpBmn.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkPpBmn.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkPpBmnUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkPpBmn.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkPpBmn.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPpBmnUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPpBmnUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkPpBmn.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkPpBmn.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkPpBmn.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkPpBmnUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPpBmnUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPpBmnUbah.teUraianKeputusan.Text;
                        dataPenyama.THN_ANG = formSkPpBmnUbah.teTahunAnggaran.Text;
                        dataPenyama.PERUNTUKAN = formSkPpBmnUbah.tePeruntukan.Text;
                        dataPenyama.PERIODE = formSkPpBmnUbah.konversiPeriode();
                        dataPenyama.NM_PHK_LAIN = formSkPpBmnUbah.teNamaPihakLain.Text;
                        int _indeksData = dsGridRskPpBmn.IndexOf(gridSkPpBmn.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskPpBmn.Remove(gridSkPpBmn.dataTerpilih);
                        dsGridRskPpBmn.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskPpBmn.Remove(gridSkPpBmn.dataTerpilih);
                        break;
                }
                gridSkPpBmn.dsDataSource = dsGridRskPpBmn;
                gridSkPpBmn.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data PP BMN

        #endregion

        #region -->> [08] Sub Menu KERJA SAMA PEMANFAATAN
        ucRskKspBmnGrid gridSkKspBmn;
        ucRskKspBmnForm formSkKspBmnTambah;
        ucRskKspBmnForm formSkKspBmnUbah;
        ucRskKspBmnForm formSkKspBmnDetail;
        private ArrayList dsGridRskKspBmn;
        SvcWasdalManfaatSkSelect.OutputParameters dOutRskKspBmn;
        SvcWasdalManfaatSkSelect.execute_pttClient ambilRskKspBmn;

        private void setEventTombolRskKspBmn()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskKspBmnTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskKspBmnUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskKspBmnHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskKspBmnDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskKspBmnKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskKspBmnRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskKspBmnMoreDataKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPpBmnKeluarKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPpBmnKembaliKlik);
        }

        private void initGridSkKspBmn()
        {
            //if (gridSkKspBmn == null)
            //{
            gridSkKspBmn = new ucRskKspBmnGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskKspBmn),
                detailDataGrid = new DetailDataGrid(bbiRskKspBmnUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskKspBmn();
            setPanel(gridSkKspBmn);
        }

        #region --++ Tombol Ribbon KSP BMN
        private void bbiRskKspBmnTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan08), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkKspBmnTambah == null)
                {
                    formSkKspBmnTambah = new ucRskKspBmnForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskKspBmn)
                    };
                }
                setPanel(formSkKspBmnTambah);
                formSkKspBmnTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskKspBmnUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkKspBmn.dataTerpilih != null)
            {
                if (formSkKspBmnUbah == null)
                {
                    formSkKspBmnUbah = new ucRskKspBmnForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskKspBmn)
                    };
                }
                formSkKspBmnUbah.dataTerpilih = gridSkKspBmn.dataTerpilih;
                setPanel(formSkKspBmnUbah);
                formSkKspBmnUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskKspBmnHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkKspBmn.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkKspBmn.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                        parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                        parInp.P_ID_SK_WASDAL_MANFAAT = gridSkKspBmn.dataTerpilih.ID_SK_WASDAL_MANFAAT;
                        parInp.P_ID_PEMOHON = gridSkKspBmn.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkKspBmn.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkKspBmn.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkKspBmn.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkKspBmn.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkKspBmn.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkKspBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkKspBmn.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkKspBmn.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkKspBmn.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkKspBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkKspBmn.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TGL_SK = konfigApp.DateToString(gridSkKspBmn.dataTerpilih.TGL_SK);
                        parInp.P_THN_ANG = gridSkKspBmn.dataTerpilih.THN_ANG;
                        parInp.P_ID_KPKNL = konfigApp.idKpknl;
                        parInp.P_KD_KL = konfigApp.kodeKl;
                        parInp.P_TIPE_PEMOHON = gridSkKspBmn.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkKspBmn.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_DARI_TGL = konfigApp.DateToString(gridSkKspBmn.dataTerpilih.DARI_TGL);
                        parInp.P_SD_TGL = konfigApp.DateToString(gridSkKspBmn.dataTerpilih.SD_TGL);
                        parInp.P_NM_PHK_LAIN = gridSkKspBmn.dataTerpilih.NM_PHK_LAIN;
                        parInp.P_ALAMAT_PHK_LAIN = gridSkKspBmn.dataTerpilih.ALAMAT_PHK_LAIN;
                        parInp.P_PERUNTUKAN = gridSkKspBmn.dataTerpilih.PERUNTUKAN;
                        parInp.P_PERIODE = gridSkKspBmn.dataTerpilih.PERIODE;
                        parInp.P_JNS_MITRA = "-";
                        parInp.P_KETERANGAN = "-";
                        //parInp.P_TIPE_PENGELOLA = "03";
                        parInp.P_TGL_CREATED = null;
                        parInp.P_ID_SATKER = gridSkKspBmn.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkKspBmn.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkKspBmn.dataTerpilih.UR_SATKER;
                        ambilDataKspBmn = new SvcWasdalManfaatCud.execute_pttClient();
                        ambilDataKspBmn.Open();
                        ambilDataKspBmn.Beginexecute(parInp, new AsyncCallback(cudRskKspBmn), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskKspBmnDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkKspBmn.dataTerpilih != null)
            {
                if (formSkKspBmnDetail == null)
                {
                    formSkKspBmnDetail = new ucRskKspBmnForm("A");
                    formSkKspBmnDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkKspBmnDetail.dataTerpilih = gridSkKspBmn.dataTerpilih;
                setPanel(formSkKspBmnDetail);
                formSkKspBmnDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskKspBmnKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkKspBmn);
            setTombolRskGrid();
            gridSkKspBmn.dsDataSource = dsGridRskKspBmn;
            gridSkKspBmn.displayData();
        }

        private void bbiRskKspBmnRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkKspBmn.teNamaKolom.Text = "";
            gridSkKspBmn.teCari.Text = "";
            gridSkKspBmn.fieldDicari = "";
            gridSkKspBmn.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskKspBmn();
        }

        private void bbiRskKspBmnMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskKspBmn();
            }
        }

        private void bbiRskKspBmnKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskKspBmnKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }

        #endregion --++ Tombol Ribbon

        private void nbiRskKsp_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan08;
            konfigApp.kdPelayanan = "08";
            konfigApp.namaPelayanan = konfigApp.namaLayanan08;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkKspBmn();
            modeCari = false;
            gridSkKspBmn.teNamaKolom.Text = "";
            gridSkKspBmn.teCari.Text = "";
            gridSkKspBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskKspBmn();
        }

        #region --++ Ambil Data KSP BMN
        private void getInitRskKspBmn()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatSkSelect.InputParameters parInp = new SvcWasdalManfaatSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND ID_KPKNL = {1} AND THN_ANG='{2}' {3}", konfigApp.kdPelayanan, konfigApp.idKpknl, konfigApp.tahunAnggaran, this.strCari);
                // parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
             //   parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
             //" (ID_USER={1} " +
             // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
             // "OR ID_SATKER = {2} " +
             // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskKspBmn = new SvcWasdalManfaatSkSelect.execute_pttClient();
                ambilRskKspBmn.Open();
                ambilRskKspBmn.Beginexecute(parInp, new AsyncCallback(getRskKspBmn), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskKspBmn(IAsyncResult result)
        {
            try
            {
                dOutRskKspBmn = ambilRskKspBmn.Endexecute(result);
                ambilRskKspBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskKspBmn(dsRskKspBmn), dOutRskKspBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskKspBmn(SvcWasdalManfaatSkSelect.OutputParameters dataOut);

        private void dsRskKspBmn(SvcWasdalManfaatSkSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_READ_WASDAL_MANFAAT.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_READ_WASDAL_MANFAAT[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRskKspBmn = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB = (dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);

                dsGridRskKspBmn.Add(dataOut.SF_READ_WASDAL_MANFAAT[i]);
            }
            gridSkKspBmn.labelTotData.Text = "";
            gridSkKspBmn.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkKspBmn.sbCariOnline.Enabled = !modeCari;
            gridSkKspBmn.dsDataSource = dsGridRskKspBmn;
            gridSkKspBmn.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkKspBmn.teNamaKolom.Text.Trim();
                string xDua = gridSkKspBmn.teCari.Text.Trim();
                string xTiga = gridSkKspBmn.fieldDicari;
                gridSkKspBmn.gvGridSk.ClearColumnsFilter();
                gridSkKspBmn.teNamaKolom.Text = xSatu;
                gridSkKspBmn.teCari.Text = xDua;
                gridSkKspBmn.fieldDicari = xTiga;
            }
            else
                gridSkKspBmn.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskKspBmn(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskKspBmn();
        }
        #endregion Ambil KSP BMN

        #region --++ Simpan Data KSP BMN
        SvcWasdalManfaatCud.OutputParameters dOutAmbilDataKspBmn;
        SvcWasdalManfaatCud.execute_pttClient ambilDataKspBmn;

        private void simpanDataRskKspBmn(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                parInp.P_ID_SK_WASDAL_MANFAAT = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.idSkWasdal : formSkKspBmnUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.idPemohon : formSkKspBmnUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.rgJenisAset.EditValue.ToString() : formSkKspBmnUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teJabatan.Text.Trim() : formSkKspBmnUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.lePenerbitSk.EditValue.ToString() : formSkKspBmnUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.kodePenerbitSkDetail : formSkKspBmnUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkKspBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnTambah.teNilaiPenetapan.Text)) : (formSkKspBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teNipPenandaTangan.Text : formSkKspBmnUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teNamaPenandaTangan.Text : formSkKspBmnUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.lePenerbitSk.Text : formSkKspBmnUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teNmPenerbitSkDetail.Text : formSkKspBmnUbah.namaPenerbitSkDetail);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teNomorSk.Text.Trim() : formSkKspBmnUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teTanggalSk.EditValue : formSkKspBmnUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teJenisPemohon.Text : formSkKspBmnUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teUraianKeputusan.Text : formSkKspBmnUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teTahunAnggaran.Text : formSkKspBmnUbah.teTahunAnggaran.Text);

                parInp.P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teAlamatPihakLain.Text : formSkKspBmnUbah.teAlamatPihakLain.Text);
                parInp.P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkKspBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnTambah.teJangkaWaktu.Text)) : (formSkKspBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnUbah.teJangkaWaktu.Text)));
                parInp.P_JNS_MITRA = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teJenisMitra.EditValue.ToString() : formSkKspBmnUbah.teJenisMitra.EditValue.ToString());
                parInp.P_NM_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.teNamaPihakLain.Text : formSkKspBmnUbah.teNamaPihakLain.Text);
                parInp.P_PERIODE = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.konversiPeriode() : formSkKspBmnUbah.konversiPeriode());
                parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.tePeruntukan.Text : formSkKspBmnUbah.tePeruntukan.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkKspBmnTambah.tipePengelola : formSkKspBmnUbah.tipePengelola);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkKspBmnTambah.filePath) : konfigApp.FileToByteArray(formSkKspBmnUbah.filePath));
                parInp.P_JANGKA_WAKTUSpecified = true;
                parInp.P_KETERANGAN = null;
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                ambilDataKspBmn = new SvcWasdalManfaatCud.execute_pttClient();
                ambilDataKspBmn.Open();
                ambilDataKspBmn.Beginexecute(parInp, new AsyncCallback(cudRskKspBmn), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskKspBmn(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataKspBmn = ambilDataKspBmn.Endexecute(result);
                ambilDataKspBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsKspBmn(this.ubahDsKspBmn), dOutAmbilDataKspBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsKspBmn(SvcWasdalManfaatCud.OutputParameters dataOutKpknlCrud);

        private void ubahDsKspBmn(SvcWasdalManfaatCud.OutputParameters dataOutKpknlCrud)
        {
            if (dataOutKpknlCrud.PO_RESULT == "Y")
            {
                SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT dataPenyama = new SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.JANGKA_WAKTUSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                if (dsGridRskKspBmn == null) dsGridRskKspBmn = new ArrayList();
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ALAMAT_PHK_LAIN = formSkKspBmnTambah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkKspBmnTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkKspBmnTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkKspBmnTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkKspBmnTambah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkKspBmnTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnTambah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkKspBmnTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkKspBmnTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkKspBmnTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkKspBmnTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkKspBmnTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkKspBmnTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkKspBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkKspBmnTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkKspBmnTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkKspBmnTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkKspBmnTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkKspBmnTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskKspBmn == null ? 1 : dsGridRskKspBmn.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkKspBmnTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkKspBmnTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkKspBmnTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkKspBmnTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkKspBmnTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkKspBmnTambah.teUraianKeputusan.Text;
                        dataPenyama.PERIODE = formSkKspBmnTambah.konversiPeriode();
                        dataPenyama.PERUNTUKAN = formSkKspBmnTambah.tePeruntukan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkKspBmnTambah.teNamaPihakLain.Text;
                        dataPenyama.THN_ANG = formSkKspBmnTambah.teTahunAnggaran.Text;
                        dsGridRskKspBmn.Add(dataPenyama);
                        formSkKspBmnTambah.gcDaftarAset.Enabled = true;
                        formSkKspBmnTambah.sbTambah.Enabled = true;
                        formSkKspBmnTambah.sbHapus.Enabled = true;
                        formSkKspBmnTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkKspBmnTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkKspBmnTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkKspBmnTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkKspBmnTambah.sbCariPemohon.Enabled = false;
                        formSkKspBmnTambah.sbRefresh.Enabled = true;
                        formSkKspBmnTambah.sbValidasi.Enabled = true;
                        formSkKspBmnTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkKspBmnTambah.cePilihSemua.Enabled = true;
                        //formSkKspBmnTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_MANFAAT = formSkKspBmnUbah.idSkWasdal;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkKspBmnUbah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkKspBmnUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkKspBmnUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkKspBmnUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkKspBmnUbah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkKspBmnUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnUbah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkKspBmnUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkKspBmnUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkKspBmnUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkKspBmnUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkKspBmnUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkKspBmnUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkKspBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkKspBmnUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkKspBmnUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkKspBmn.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkKspBmnUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkKspBmnUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkKspBmnUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkKspBmnUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkKspBmn.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkKspBmn.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkKspBmnUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkKspBmn.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkKspBmn.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkKspBmnUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkKspBmnUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkKspBmn.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkKspBmn.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkKspBmn.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkKspBmnUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkKspBmnUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkKspBmnUbah.teUraianKeputusan.Text;
                        dataPenyama.PERIODE = formSkKspBmnUbah.konversiPeriode();
                        dataPenyama.PERUNTUKAN = formSkKspBmnUbah.tePeruntukan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkKspBmnUbah.teNamaPihakLain.Text;
                        dataPenyama.THN_ANG = formSkKspBmnUbah.teTahunAnggaran.Text;
                        int _indeksData = dsGridRskKspBmn.IndexOf(gridSkKspBmn.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskKspBmn.Remove(gridSkKspBmn.dataTerpilih);
                        dsGridRskKspBmn.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskKspBmn.Remove(gridSkKspBmn.dataTerpilih);
                        break;
                }
                gridSkKspBmn.dsDataSource = dsGridRskKspBmn;
                gridSkKspBmn.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data PSP BMN

        #endregion

        #region -->> [09] Sub Menu KSPI
        ucRskBgsGrid gridSkBgs;
        ucRskBgsForm formSkBgsTambah;
        ucRskBgsForm formSkBgsUbah;
        ucRskBgsForm formSkBgsDetail;
        private ArrayList dsGridRskBgs;
        SvcWasdalManfaatSkSelect.OutputParameters dOutRskBgs;
        SvcWasdalManfaatSkSelect.execute_pttClient ambilRskBgs;

        private void setEventTombolRskBgs()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskBgsTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskBgsUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskBgsHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskBgsDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskBgsKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskBgsRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskBgsMoreDataKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskBgsKeluarKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskBgsKembaliKlik);
        }

        private void initGridSkBgs()
        {
            //if (gridSkBgs == null)
            //{
            gridSkBgs = new ucRskBgsGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskBgs),
                detailDataGrid = new DetailDataGrid(bbiRskBgsUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskBgs();
            setPanel(gridSkBgs);
        }

        #region --++ Tombol Ribbon KSPI
        private void bbiRskBgsTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan09), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkBgsTambah == null)
                {
                    formSkBgsTambah = new ucRskBgsForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskBgs)
                    };
                }
                setPanel(formSkBgsTambah);
                formSkBgsTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskBgsUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkBgs.dataTerpilih != null)
            {
                if (formSkBgsUbah == null)
                {
                    formSkBgsUbah = new ucRskBgsForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskBgs)
                    };
                }
                formSkBgsUbah.dataTerpilih = gridSkBgs.dataTerpilih;
                setPanel(formSkBgsUbah);
                formSkBgsUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskBgsHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkBgs.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkBgs.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                        parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                        parInp.P_ID_SK_WASDAL_MANFAAT = gridSkBgs.dataTerpilih.ID_SK_WASDAL_MANFAAT;

                        parInp.P_ID_PEMOHON = gridSkBgs.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkBgs.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkBgs.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkBgs.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkBgs.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkBgs.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkBgs.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkBgs.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkBgs.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkBgs.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkBgs.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkBgs.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TGL_SK = konfigApp.DateToString(gridSkBgs.dataTerpilih.TGL_SK);
                        parInp.P_THN_ANG = gridSkBgs.dataTerpilih.THN_ANG;
                        parInp.P_ID_KPKNL = konfigApp.idKpknl;
                        parInp.P_KD_KL = konfigApp.kodeKl;
                        parInp.P_TIPE_PEMOHON = gridSkBgs.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkBgs.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_DARI_TGL = konfigApp.DateToString(gridSkBgs.dataTerpilih.DARI_TGL);
                        parInp.P_SD_TGL = konfigApp.DateToString(gridSkBgs.dataTerpilih.SD_TGL);
                        parInp.P_NM_PHK_LAIN = gridSkBgs.dataTerpilih.NM_PHK_LAIN;
                        parInp.P_ALAMAT_PHK_LAIN = gridSkBgs.dataTerpilih.ALAMAT_PHK_LAIN;
                        parInp.P_PERUNTUKAN = gridSkBgs.dataTerpilih.PERUNTUKAN;
                        parInp.P_PERIODE = gridSkBgs.dataTerpilih.PERIODE;
                        parInp.P_JNS_MITRA = "-";
                        parInp.P_KETERANGAN = "-";
                        //parInp.P_TIPE_PENGELOLA = "03";
                        parInp.P_TGL_CREATED = null;
                        parInp.P_ID_SATKER = gridSkBgs.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkBgs.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkBgs.dataTerpilih.UR_SATKER;
                        ambilDataBgs = new SvcWasdalManfaatCud.execute_pttClient();
                        ambilDataBgs.Open();
                        ambilDataBgs.Beginexecute(parInp, new AsyncCallback(cudRskBgs), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskBgsDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkBgs.dataTerpilih != null)
            {
                if (formSkBgsDetail == null)
                {
                    formSkBgsDetail = new ucRskBgsForm("A");
                    formSkBgsDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkBgsDetail.dataTerpilih = gridSkBgs.dataTerpilih;
                setPanel(formSkBgsDetail);
                formSkBgsDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskBgsKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkBgs);
            setTombolRskGrid();
            gridSkBgs.dsDataSource = dsGridRskBgs;
            gridSkBgs.displayData();
        }

        private void bbiRskBgsRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkBgs.teNamaKolom.Text = "";
            gridSkBgs.teCari.Text = "";
            gridSkBgs.fieldDicari = "";
            gridSkBgs.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskBgs();
        }

        private void bbiRskBgsMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskBgs();
            }
        }

        private void bbiRskBgsKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskBgsKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }

        #endregion --++ Tombol Ribbon KSPI

        private void nbiRskBgs_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan09;
            konfigApp.kdPelayanan = "09";
            konfigApp.namaPelayanan = konfigApp.namaLayanan09;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkBgs();
            modeCari = false;
            gridSkBgs.teNamaKolom.Text = "";
            gridSkBgs.teCari.Text = "";
            gridSkBgs.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskBgs();
        }

        #region --++ Ambil Data KSPI
        private void getInitRskBgs()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatSkSelect.InputParameters parInp = new SvcWasdalManfaatSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format(" KD_PELAYANAN = '{0}' AND ID_KPKNL = {1} AND THN_ANG='{2}' {3}", konfigApp.kdPelayanan, konfigApp.idKpknl, konfigApp.tahunAnggaran, this.strCari);
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '09'  AND (ID_USER={0} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {1})) AND (ID_SATKER = {1} OR ID_SATKER_PARENT= {1}) {2}", konfigApp.idUser, konfigApp.idSatker, this.strCari);
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '09' AND " +
            //" (ID_USER={0} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={1}) " +
            // "OR ID_SATKER = {1} " +
            // "OR ID_SATKER_PARENT= {1}) {2}", konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskBgs = new SvcWasdalManfaatSkSelect.execute_pttClient();
                ambilRskBgs.Open();
                ambilRskBgs.Beginexecute(parInp, new AsyncCallback(getRskBgs), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskBgs(IAsyncResult result)
        {
            try
            {
                dOutRskBgs = ambilRskBgs.Endexecute(result);
                ambilRskBgs.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskBgs(dsRskBgs), dOutRskBgs);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskBgs(SvcWasdalManfaatSkSelect.OutputParameters dataOut);

        private void dsRskBgs(SvcWasdalManfaatSkSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_READ_WASDAL_MANFAAT.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_READ_WASDAL_MANFAAT[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRskBgs = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB = (dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskBgs.Add(dataOut.SF_READ_WASDAL_MANFAAT[i]);
            }
            gridSkBgs.labelTotData.Text = "";
            gridSkBgs.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkBgs.sbCariOnline.Enabled = !modeCari;
            gridSkBgs.dsDataSource = dsGridRskBgs;
            gridSkBgs.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkBgs.teNamaKolom.Text.Trim();
                string xDua = gridSkBgs.teCari.Text.Trim();
                string xTiga = gridSkBgs.fieldDicari;
                gridSkBgs.gvGridSk.ClearColumnsFilter();
                gridSkBgs.teNamaKolom.Text = xSatu;
                gridSkBgs.teCari.Text = xDua;
                gridSkBgs.fieldDicari = xTiga;
            }
            else
                gridSkBgs.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskBgs(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskBgs();
        }
        #endregion Ambil BGS/BSG

        #region --++ Simpan Data KSPI
        SvcWasdalManfaatCud.OutputParameters dOutAmbilDataBgs;
        SvcWasdalManfaatCud.execute_pttClient ambilDataBgs;

        private void simpanDataRskBgs(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                parInp.P_ID_SK_WASDAL_MANFAAT = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.idSkWasdal : formSkBgsUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.idPemohon : formSkBgsUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.rgJenisAset.EditValue.ToString() : formSkBgsUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teJabatan.Text.Trim() : formSkBgsUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.lePenerbitSk.EditValue.ToString() : formSkBgsUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.kodePenerbitSkDetail : formSkBgsUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkBgsTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkBgsTambah.teNilaiPenetapan.Text)) : (formSkBgsUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkBgsUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teNipPenandaTangan.Text : formSkBgsUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teNamaPenandaTangan.Text : formSkBgsUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.lePenerbitSk.Text : formSkBgsUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teNmPenerbitSkDetail.Text : formSkBgsUbah.namaPenerbitSkDetail);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teNomorSk.Text.Trim() : formSkBgsUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teTanggalSk.EditValue : formSkBgsUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teJenisPemohon.Text : formSkBgsUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teUraianKeputusan.Text : formSkBgsUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teTahunAnggaran.Text : formSkBgsUbah.teTahunAnggaran.Text);

                parInp.P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teAlamatPihakLain.Text : formSkBgsUbah.teAlamatPihakLain.Text);
                parInp.P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkBgsTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkBgsTambah.teJangkaWaktu.Text)) : (formSkBgsUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkBgsUbah.teJangkaWaktu.Text)));
                parInp.P_JNS_MITRA = null;
                parInp.P_NM_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.teNamaPihakLain.Text : formSkBgsUbah.teNamaPihakLain.Text);
                parInp.P_PERIODE = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.konversiPeriode() : formSkBgsUbah.konversiPeriode());
                parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.tePeruntukan.Text : formSkBgsUbah.tePeruntukan.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkBgsTambah.tipePengelola : formSkBgsUbah.tipePengelola);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkBgsTambah.filePath) : konfigApp.FileToByteArray(formSkBgsUbah.filePath));
                parInp.P_JANGKA_WAKTUSpecified = true;
                //parInp.P_KETERANGAN = null;
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                ambilDataBgs = new SvcWasdalManfaatCud.execute_pttClient();
                ambilDataBgs.Open();
                ambilDataBgs.Beginexecute(parInp, new AsyncCallback(cudRskBgs), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskBgs(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataBgs = ambilDataBgs.Endexecute(result);
                ambilDataBgs.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsBgs(this.ubahDsBgs), dOutAmbilDataBgs);
            }
            catch (Exception e)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsBgs(SvcWasdalManfaatCud.OutputParameters dataOutBgsCrud);

        private void ubahDsBgs(SvcWasdalManfaatCud.OutputParameters dataOutBgsCrud)
        {
            if (dataOutBgsCrud.PO_RESULT == "Y")
            {
                SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT dataPenyama = new SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.JANGKA_WAKTUSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                if (dsGridRskBgs == null) dsGridRskBgs = new ArrayList();
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ALAMAT_PHK_LAIN = formSkBgsTambah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkBgsTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkBgsTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkBgsTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkBgsTambah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkBgsTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkBgsTambah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkBgsTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkBgsTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkBgsTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkBgsTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkBgsTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkBgsTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkBgsTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkBgsTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkBgsTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkBgsTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkBgsTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkBgsTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkBgsTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkBgsTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskBgs == null ? 1 : dsGridRskBgs.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkBgsTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkBgsTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkBgsTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkBgsTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkBgsTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkBgsTambah.teUraianKeputusan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkBgsTambah.teNamaPihakLain.Text;
                        dataPenyama.PERUNTUKAN = formSkBgsTambah.tePeruntukan.Text;
                        dataPenyama.THN_ANG = formSkBgsTambah.teTahunAnggaran.Text;
                        dataPenyama.PERIODE = formSkBgsTambah.konversiPeriode();
                        dsGridRskBgs.Add(dataPenyama);
                        formSkBgsTambah.gcDaftarAset.Enabled = true;
                        formSkBgsTambah.sbTambah.Enabled = true;
                        formSkBgsTambah.sbHapus.Enabled = true;
                        formSkBgsTambah.sbValidAset.Enabled = true;
                        formSkBgsTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkBgsTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkBgsTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkBgsTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkBgsTambah.sbCariPemohon.Enabled = false;
                        formSkBgsTambah.sbRefresh.Enabled = true;
                        formSkBgsTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkBgsTambah.cePilihSemua.Enabled = true;
                        formSkBgsTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkBgsTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_MANFAAT = formSkBgsUbah.idSkWasdal;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkBgsUbah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkBgsUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkBgsUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkBgsUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkBgsUbah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkBgsUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkBgsUbah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkBgsUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkBgsUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkBgsUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkBgsUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkBgsUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkBgsUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkBgsUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkBgsUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkBgsUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkBgsUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkBgs.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkBgsUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkBgsUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkBgsUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkBgsUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkBgs.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkBgs.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkBgsUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkBgs.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkBgs.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkBgsUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkBgsUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkBgs.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkBgs.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkBgs.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkBgsUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkBgsUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkBgsUbah.teUraianKeputusan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkBgsUbah.teNamaPihakLain.Text;
                        dataPenyama.PERUNTUKAN = formSkBgsUbah.tePeruntukan.Text;
                        dataPenyama.THN_ANG = formSkBgsUbah.teTahunAnggaran.Text;
                        dataPenyama.PERIODE = formSkBgsUbah.konversiPeriode();
                        int _indeksData = dsGridRskBgs.IndexOf(gridSkBgs.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskBgs.Remove(gridSkBgs.dataTerpilih);
                        dsGridRskBgs.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskBgs.Remove(gridSkBgs.dataTerpilih);
                        break;
                }
                gridSkBgs.dsDataSource = dsGridRskBgs;
                gridSkBgs.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data KSPI

        #endregion

        #region -->> [10] Sub Menu BGS/BSG
        ucRskBsgGrid gridSkBsg;
        ucRskBsgForm formSkBsgTambah;
        ucRskBsgForm formSkBsgUbah;
        ucRskBsgForm formSkBsgDetail;
        private ArrayList dsGridRskBsg;
        SvcWasdalManfaatSkSelect.OutputParameters dOutRskBsg;
        SvcWasdalManfaatSkSelect.execute_pttClient ambilRskBsg;

        private void setEventTombolRskBsg()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskBsgTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskBsgUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskBsgHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskBsgDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskBsgKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskBsgRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskBsgMoreDataKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskBsgKeluarKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskBsgKembaliKlik);
        }

        private void initGridSkBsg()
        {
            //if (gridSkBsg == null)
            //{
            gridSkBsg = new ucRskBsgGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskBsg),
                detailDataGrid = new DetailDataGrid(bbiRskBsgUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskBsg();
            setPanel(gridSkBsg);
        }

        #region --++ Tombol Ribbon BGS/BSG
        private void bbiRskBsgTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan10), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkBsgTambah == null)
                {
                    formSkBsgTambah = new ucRskBsgForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskBsg)
                    };
                }
                setPanel(formSkBsgTambah);
                formSkBsgTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskBsgUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkBsg.dataTerpilih != null)
            {
                if (formSkBsgUbah == null)
                {
                    formSkBsgUbah = new ucRskBsgForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskBsg)
                    };
                }
                formSkBsgUbah.dataTerpilih = gridSkBsg.dataTerpilih;
                setPanel(formSkBsgUbah);
                formSkBsgUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskBsgHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkBsg.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkBsg.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                        parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                        parInp.P_ID_SK_WASDAL_MANFAAT = gridSkBsg.dataTerpilih.ID_SK_WASDAL_MANFAAT;
                        parInp.P_ID_PEMOHON = gridSkBsg.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkBsg.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkBsg.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkBsg.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkBsg.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkBsg.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkBsg.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkBsg.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkBsg.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkBsg.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkBsg.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkBsg.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TGL_SK = konfigApp.DateToString(gridSkBsg.dataTerpilih.TGL_SK);
                        parInp.P_THN_ANG = gridSkBsg.dataTerpilih.THN_ANG;
                        parInp.P_ID_KPKNL = konfigApp.idKpknl;
                        parInp.P_KD_KL = konfigApp.kodeKl;
                        parInp.P_TIPE_PEMOHON = gridSkBsg.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkBsg.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_DARI_TGL = konfigApp.DateToString(gridSkBsg.dataTerpilih.DARI_TGL);
                        parInp.P_SD_TGL = konfigApp.DateToString(gridSkBsg.dataTerpilih.SD_TGL);
                        parInp.P_NM_PHK_LAIN = gridSkBsg.dataTerpilih.NM_PHK_LAIN;
                        parInp.P_ALAMAT_PHK_LAIN = gridSkBsg.dataTerpilih.ALAMAT_PHK_LAIN;
                        parInp.P_PERUNTUKAN = gridSkBsg.dataTerpilih.PERUNTUKAN;
                        parInp.P_PERIODE = gridSkBsg.dataTerpilih.PERIODE;
                        parInp.P_JNS_MITRA = "-";
                        parInp.P_KETERANGAN = "-";
                        //parInp.P_TIPE_PENGELOLA = "03";
                        parInp.P_TGL_CREATED = null;
                        parInp.P_ID_SATKER = gridSkBsg.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkBsg.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkBsg.dataTerpilih.UR_SATKER;
                        ambilDataBsg = new SvcWasdalManfaatCud.execute_pttClient();
                        ambilDataBsg.Open();
                        ambilDataBsg.Beginexecute(parInp, new AsyncCallback(cudRskBsg), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskBsgDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkBsg.dataTerpilih != null)
            {
                if (formSkBsgDetail == null)
                {
                    formSkBsgDetail = new ucRskBsgForm("A");
                    formSkBsgDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkBsgDetail.dataTerpilih = gridSkBsg.dataTerpilih;
                setPanel(formSkBsgDetail);
                formSkBsgDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskBsgKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkBsg);
            setTombolRskGrid();
            gridSkBsg.dsDataSource = dsGridRskBsg;
            gridSkBsg.displayData();
        }

        private void bbiRskBsgRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkBsg.teNamaKolom.Text = "";
            gridSkBsg.teCari.Text = "";
            gridSkBsg.fieldDicari = "";
            gridSkBsg.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskBsg();
        }

        private void bbiRskBsgMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskBsg();
            }
        }

        private void bbiRskBsgKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskBsgKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }

        #endregion --++ Tombol Ribbon BGS/BSG

        private void nbiRskBsg_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan10;
            konfigApp.kdPelayanan = "10";
            konfigApp.namaPelayanan = konfigApp.namaLayanan10;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkBsg();
            modeCari = false;
            gridSkBsg.teNamaKolom.Text = "";
            gridSkBsg.teCari.Text = "";
            gridSkBsg.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskBsg();
        }

        #region --++ Ambil Data BGS/BSG
        private void getInitRskBsg()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatSkSelect.InputParameters parInp = new SvcWasdalManfaatSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND ID_KPKNL = {1} AND THN_ANG='{2}' {3}", konfigApp.kdPelayanan, konfigApp.idKpknl, konfigApp.tahunAnggaran, this.strCari);
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskBsg = new SvcWasdalManfaatSkSelect.execute_pttClient();
                ambilRskBsg.Open();
                ambilRskBsg.Beginexecute(parInp, new AsyncCallback(getRskBsg), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskBsg(IAsyncResult result)
        {
            try
            {
                dOutRskBsg = ambilRskBsg.Endexecute(result);
                ambilRskBsg.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskBsg(dsRskBsg), dOutRskBsg);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskBsg(SvcWasdalManfaatSkSelect.OutputParameters dataOut);

        private void dsRskBsg(SvcWasdalManfaatSkSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_READ_WASDAL_MANFAAT.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_READ_WASDAL_MANFAAT[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRskBsg = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB = (dataOut.SF_READ_WASDAL_MANFAAT[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskBsg.Add(dataOut.SF_READ_WASDAL_MANFAAT[i]);
            }
            gridSkBsg.labelTotData.Text = "";
            gridSkBsg.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkBsg.sbCariOnline.Enabled = !modeCari;
            gridSkBsg.dsDataSource = dsGridRskBsg;
            gridSkBsg.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkBsg.teNamaKolom.Text.Trim();
                string xDua = gridSkBsg.teCari.Text.Trim();
                string xTiga = gridSkBsg.fieldDicari;
                gridSkBsg.gvGridSk.ClearColumnsFilter();
                gridSkBsg.teNamaKolom.Text = xSatu;
                gridSkBsg.teCari.Text = xDua;
                gridSkBsg.fieldDicari = xTiga;
            }
            else
                gridSkBsg.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskBsg(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskBsg();
        }
        #endregion Ambil BGS/BSG

        #region --++ Simpan Data BGS/BSG
        SvcWasdalManfaatCud.OutputParameters dOutAmbilDataBsg;
        SvcWasdalManfaatCud.execute_pttClient ambilDataBsg;

        private void simpanDataRskBsg(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalManfaatCud.InputParameters parInp = new SvcWasdalManfaatCud.InputParameters();
                parInp.P_ID_SK_WASDAL_MANFAATSpecified = true;
                parInp.P_ID_SK_WASDAL_MANFAAT = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.idSkWasdal : formSkBsgUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.idPemohon : formSkBsgUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.rgJenisAset.EditValue.ToString() : formSkBsgUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teJabatan.Text.Trim() : formSkBsgUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.lePenerbitSk.EditValue.ToString() : formSkBsgUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.kodePenerbitSkDetail : formSkBsgUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkBsgTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkBsgTambah.teNilaiPenetapan.Text)) : (formSkBsgUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkBsgUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teNipPenandaTangan.Text : formSkBsgUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teNamaPenandaTangan.Text : formSkBsgUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.lePenerbitSk.Text : formSkBsgUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teNmPenerbitSkDetail.Text : formSkBsgUbah.namaPenerbitSkDetail);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teNomorSk.Text.Trim() : formSkBsgUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teTanggalSk.EditValue : formSkBsgUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teJenisPemohon.Text : formSkBsgUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teUraianKeputusan.Text : formSkBsgUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teTahunAnggaran.Text : formSkBsgUbah.teTahunAnggaran.Text);

                parInp.P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teAlamatPihakLain.Text : formSkBsgUbah.teAlamatPihakLain.Text);
                parInp.P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkBsgTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkBsgTambah.teJangkaWaktu.Text)) : (formSkBsgUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkBsgUbah.teJangkaWaktu.Text)));
                parInp.P_JNS_MITRA = null;
                parInp.P_NM_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.teNamaPihakLain.Text : formSkBsgUbah.teNamaPihakLain.Text);
                parInp.P_PERIODE = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.konversiPeriode() : formSkBsgUbah.konversiPeriode());
                parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.tePeruntukan.Text : formSkBsgUbah.tePeruntukan.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkBsgTambah.tipePengelola : formSkBsgUbah.tipePengelola);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkBsgTambah.filePath) : konfigApp.FileToByteArray(formSkBsgUbah.filePath));
                parInp.P_JANGKA_WAKTUSpecified = true;
                parInp.P_KETERANGAN = null;
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                ambilDataBsg = new SvcWasdalManfaatCud.execute_pttClient();
                ambilDataBsg.Open();
                ambilDataBsg.Beginexecute(parInp, new AsyncCallback(cudRskBsg), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskBsg(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataBsg = ambilDataBsg.Endexecute(result);
                ambilDataBsg.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsBsg(this.ubahDsBsg), dOutAmbilDataBsg);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsBsg(SvcWasdalManfaatCud.OutputParameters dataOutBsgCrud);

        private void ubahDsBsg(SvcWasdalManfaatCud.OutputParameters dataOutBsgCrud)
        {
            if (dataOutBsgCrud.PO_RESULT == "Y")
            {
                SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT dataPenyama = new SvcWasdalManfaatSkSelect.WASDALSROW_READ_WASDAL_MANFAAT();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.JANGKA_WAKTUSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                if (dsGridRskBsg == null) dsGridRskBsg = new ArrayList();
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ALAMAT_PHK_LAIN = formSkBsgTambah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkBsgTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkBsgTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkBsgTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkBsgTambah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkBsgTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkBsgTambah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkBsgTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkBsgTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkBsgTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkBsgTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkBsgTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkBsgTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkBsgTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkBsgTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkBsgTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkBsgTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkBsgTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkBsgTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkBsgTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkBsgTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskBsg == null ? 1 : dsGridRskBsg.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkBsgTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkBsgTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkBsgTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkBsgTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkBsgTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkBsgTambah.teUraianKeputusan.Text;
                        dataPenyama.THN_ANG = formSkBsgTambah.teTahunAnggaran.Text;
                        dataPenyama.PERUNTUKAN = formSkBsgTambah.tePeruntukan.Text;
                        dataPenyama.PERIODE = formSkBsgTambah.konversiPeriode();
                        dataPenyama.NM_PHK_LAIN = formSkBsgTambah.teNamaPihakLain.Text;
                        dsGridRskBsg.Add(dataPenyama);
                        formSkBsgTambah.gcDaftarAset.Enabled = true;
                        formSkBsgTambah.sbTambah.Enabled = true;
                        formSkBsgTambah.sbHapus.Enabled = true;
                        formSkBsgTambah.sbValidAset.Enabled = true;
                        formSkBsgTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkBsgTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkBsgTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkBsgTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkBsgTambah.sbCariPemohon.Enabled = false;
                        formSkBsgTambah.sbRefresh.Enabled = true;
                        formSkBsgTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkBsgTambah.cePilihSemua.Enabled = true;
                        formSkBsgTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkBsgTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_MANFAAT = formSkBgsUbah.idSkWasdal;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkBsgUbah.teAlamatPihakLain.Text;
                        dataPenyama.ID_PEMOHON = formSkBsgUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkBsgUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkBsgUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkBsgUbah.teJabatan.Text;
                        dataPenyama.JANGKA_WAKTU = formSkBsgUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkBsgUbah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkBsgUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkBsgUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkBsgUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkBsgUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkBsgUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkBsgUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkBsgUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkBsgUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkBsgUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkBsgUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkBsg.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkBsgUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkBsgUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkBsgUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkBsgUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkBsg.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkBsg.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkBsgUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkBsg.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkBsg.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkBsgUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkBsgUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkBsg.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkBsg.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkBsg.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkBsgUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkBsgUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkBsgUbah.teUraianKeputusan.Text;
                        dataPenyama.PERIODE = formSkBsgUbah.konversiPeriode();
                        dataPenyama.PERUNTUKAN = formSkBsgUbah.tePeruntukan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkBsgUbah.teNamaPihakLain.Text;
                        dataPenyama.THN_ANG = formSkBsgUbah.teTahunAnggaran.Text;
                        int _indeksData = dsGridRskBsg.IndexOf(gridSkBsg.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskBsg.Remove(gridSkBsg.dataTerpilih);
                        dsGridRskBsg.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskBsg.Remove(gridSkBsg.dataTerpilih);
                        break;
                }
                gridSkBsg.dsDataSource = dsGridRskBsg;
                gridSkBsg.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data BGS/BSG

        #endregion

        #region penjualan
        ucRskPenjualanGrid gridSkPenjualan;
        ucRskPenjualanForm formSkPenjualanTambah;
        ucRskPenjualanForm formSkPenjualanUbah;
        ucRskPenjualanForm formSkPenjualanDetail;
        private ArrayList dsGridRskPenjualan;
        SvcWasdalJualSkSelect.OutputParameters dOutRskPenjualan;
        SvcWasdalJualSkSelect.execute_pttClient ambilRskPenjualan;

        private void setEventTombolRskPenjualan()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskPenjualanTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskPenjualanUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskPenjualanHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskPenjualanDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskPenjualanKembaliKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskPenjualanRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskPenjualanMoreDataKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKembaliKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKeluarKlik);
        }

        private void initGridSkPenjualan()
        {
            //if (gridSkPenjualan == null)
            //{
            gridSkPenjualan = new AppPengguna.KKW.RSK.ucRskPenjualanGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskPenjualan),
                detailDataGrid = new DetailDataGrid(bbiRskPenjualanUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskPenjualan();
            setPanel(gridSkPenjualan);
        }

        #region --++ Tombol Ribbon Penjualan
        private void bbiRskPenjualanTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan11), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkPenjualanTambah == null)
                {
                    formSkPenjualanTambah = new AppPengguna.KKW.RSK.ucRskPenjualanForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPenjualan)
                    };
                }
                setPanel(formSkPenjualanTambah);
                formSkPenjualanTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskPenjualanUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPenjualan.dataTerpilih != null)
            {
                if (formSkPenjualanUbah == null)
                {
                    formSkPenjualanUbah = new AppPengguna.KKW.RSK.ucRskPenjualanForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPenjualan)
                    };
                }
                formSkPenjualanUbah.dataTerpilih = gridSkPenjualan.dataTerpilih;
                setPanel(formSkPenjualanUbah);
                formSkPenjualanUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPenjualanHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPenjualan.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkPenjualan.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalJualCrud.InputParameters parInp = new SvcWasdalJualCrud.InputParameters();
                        parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                        parInp.P_ID_SK_WASDAL_PINDAHTANGAN = gridSkPenjualan.dataTerpilih.ID_SK_WASDAL_PINDAHTANGAN;
                        parInp.P_ID_PEMOHON = gridSkPenjualan.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkPenjualan.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkPenjualan.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkPenjualan.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkPenjualan.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkPenjualan.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkPenjualan.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkPenjualan.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkPenjualan.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkPenjualan.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkPenjualan.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkPenjualan.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkPenjualan.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkPenjualan.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkPenjualan.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkPenjualan.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkPenjualan.dataTerpilih.UR_SATKER;
                        ambilDataPenjualan = new SvcWasdalJualCrud.execute_pttClient();
                        ambilDataPenjualan.Open();
                        ambilDataPenjualan.Beginexecute(parInp, new AsyncCallback(cudRskPenjualan), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskPenjualanDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPenjualan.dataTerpilih != null)
            {
                if (formSkPenjualanDetail == null)
                {
                    formSkPenjualanDetail = new AppPengguna.KKW.RSK.ucRskPenjualanForm("A");
                    formSkPenjualanDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkPenjualanDetail.dataTerpilih = gridSkPenjualan.dataTerpilih;
                setPanel(formSkPenjualanDetail);
                formSkPenjualanDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPenjualanKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkPenjualan);
            setTombolRskGrid();
            gridSkPenjualan.dsDataSource = dsGridRskPenjualan;
            gridSkPenjualan.displayData();
        }

        private void bbiRskPenjualanRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkPenjualan.teNamaKolom.Text = "";
            gridSkPenjualan.teCari.Text = "";
            gridSkPenjualan.fieldDicari = "";
            gridSkPenjualan.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskPenjualan();
        }

        private void bbiRskPenjualanMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskPenjualan();
            }
        }
        #endregion --++ Tombol Ribbon Penjualan

        private void nbiRskPenjualan_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan11;
            konfigApp.kdPelayanan = "11";
            konfigApp.namaPelayanan = konfigApp.namaLayanan11;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkPenjualan();
            modeCari = false;
            gridSkPenjualan.teNamaKolom.Text = "";
            gridSkPenjualan.teCari.Text = "";
            gridSkPenjualan.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskPenjualan();
        }

        #region --++ Ambil Data Penjualan
        private void getInitRskPenjualan()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalJualSkSelect.InputParameters parInp = new SvcWasdalJualSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                // parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON={2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskPenjualan = new SvcWasdalJualSkSelect.execute_pttClient();
                ambilRskPenjualan.Open();
                ambilRskPenjualan.Beginexecute(parInp, new AsyncCallback(getRskPenjualan), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskPenjualan(IAsyncResult result)
        {
            try
            {
                dOutRskPenjualan = ambilRskPenjualan.Endexecute(result);
                ambilRskPenjualan.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskPenjualan(dsRskPenjualan), dOutRskPenjualan);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskPenjualan(SvcWasdalJualSkSelect.OutputParameters dataOut);

        private void dsRskPenjualan(SvcWasdalJualSkSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_READ_WASDAL_PT_JUAL.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_READ_WASDAL_PT_JUAL[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRskPenjualan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dataOut.SF_READ_WASDAL_PT_JUAL[i].IS_TB = (dataOut.SF_READ_WASDAL_PT_JUAL[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskPenjualan.Add(dataOut.SF_READ_WASDAL_PT_JUAL[i]);
            }
            gridSkPenjualan.labelTotData.Text = "";
            gridSkPenjualan.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkPenjualan.sbCariOnline.Enabled = !modeCari;
            gridSkPenjualan.dsDataSource = dsGridRskPenjualan;
            gridSkPenjualan.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkPenjualan.teNamaKolom.Text.Trim();
                string xDua = gridSkPenjualan.teCari.Text.Trim();
                string xTiga = gridSkPenjualan.fieldDicari;
                gridSkPenjualan.gvGridSk.ClearColumnsFilter();
                gridSkPenjualan.teNamaKolom.Text = xSatu;
                gridSkPenjualan.teCari.Text = xDua;
                gridSkPenjualan.fieldDicari = xTiga;
            }
            else
                gridSkPenjualan.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskPenjualan(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskPenjualan();
        }
        #endregion Ambil Penjualan

        #region --++ Simpan Data Penjualan
        SvcWasdalJualCrud.OutputParameters dOutAmbilDataPenjualan;
        SvcWasdalJualCrud.execute_pttClient ambilDataPenjualan;

        private void simpanDataRskPenjualan(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalJualCrud.InputParameters parInp = new SvcWasdalJualCrud.InputParameters();
                parInp.P_ID_SK_WASDAL_PINDAHTANGAN = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.idSkWasdal : formSkPenjualanUbah.idSkWasdal);//
                parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.idPemohon : formSkPenjualanUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.rgJenisAset.EditValue.ToString() : formSkPenjualanUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teJabatan.Text.Trim() : formSkPenjualanUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teNmPenerbitSk.Properties.GetDataSourceValue("KD_INSTANSI", formSkPenjualanTambah.teNmPenerbitSk.ItemIndex).ToString() : formSkPenjualanUbah.teNmPenerbitSk.Properties.GetDataSourceValue("KD_INSTANSI", formSkPenjualanUbah.teNmPenerbitSk.ItemIndex).ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.kodePenerbitSkDetail : formSkPenjualanUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkPenjualanTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPenjualanTambah.teNilaiPenetapan.Text)) : (formSkPenjualanUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPenjualanUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teNipPenandaTangan.Text : formSkPenjualanUbah.teNipPenandaTangan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teTahunAnggaran.Text : formSkPenjualanUbah.teTahunAnggaran.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teNamaPenandaTangan.Text : formSkPenjualanUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teNmPenerbitSk.Text : formSkPenjualanUbah.teNmPenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teNamaInstansi.Text : formSkPenjualanUbah.teNamaInstansi.Text);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teNomorSk.Text.Trim() : formSkPenjualanUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teTanggalSk.EditValue : formSkPenjualanUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teJenisPemohon.Text : formSkPenjualanUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.tipePengelola : formSkPenjualanUbah.tipePengelola);
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teUraianKeputusan.Text : formSkPenjualanUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkPenjualanTambah.teTahunAnggaran.Text : formSkPenjualanUbah.teTahunAnggaran.Text);
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                ambilDataPenjualan = new SvcWasdalJualCrud.execute_pttClient();
                ambilDataPenjualan.Open();
                ambilDataPenjualan.Beginexecute(parInp, new AsyncCallback(cudRskPenjualan), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void cudRskPenjualan(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataPenjualan = ambilDataPenjualan.Endexecute(result);
                ambilDataPenjualan.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsPenjualan(this.ubahDsPenjualan), dOutAmbilDataPenjualan);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsPenjualan(SvcWasdalJualCrud.OutputParameters dataOutPenjualanCrud);

        private void ubahDsPenjualan(SvcWasdalJualCrud.OutputParameters dataOutPenjualanCrud)
        {
            if (dataOutPenjualanCrud.PO_RESULT == "Y")
            {
                SvcWasdalJualSkSelect.WASDALSROW_READ_WASDAL_PT_JUAL dataPenyama = new SvcWasdalJualSkSelect.WASDALSROW_READ_WASDAL_PT_JUAL();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ID_SK_WASDAL_PINDAHTANGAN = formSkPenjualanTambah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkPenjualanTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPenjualanTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkPenjualanTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPenjualanTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkPenjualanTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPenjualanTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPenjualanTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPenjualanTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPenjualanTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPenjualanTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPenjualanTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPenjualanTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPenjualanTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPenjualanTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkPenjualanTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPenjualanTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPenjualanTambah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPenjualanTambah.teNamaInstansi.Text;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskPenjualan == null ? 1 : dsGridRskPenjualan.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkPenjualanTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPenjualanTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPenjualanTambah.teJenisPemohon.Text;
                        //dataPenyama.THN_ANG = formSkPenjualanTambah.teTahunAnggaran.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkPenjualanTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPenjualanTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPenjualanTambah.teUraianKeputusan.Text;
                        if (dsGridRskPenjualan == null) dsGridRskPenjualan = new ArrayList();
                        dsGridRskPenjualan.Add(dataPenyama);
                        formSkPenjualanTambah.gcDaftarAset.Enabled = true;
                        formSkPenjualanTambah.sbTambah.Enabled = true;
                        formSkPenjualanTambah.sbHapus.Enabled = true;
                        formSkPenjualanTambah.sbValidasi.Enabled = true;
                        formSkPenjualanTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkPenjualanTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkPenjualanTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkPenjualanTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkPenjualanTambah.sbCariPemohon.Enabled = false;
                        formSkPenjualanTambah.sbRefresh.Enabled = true;
                        formSkPenjualanTambah.teNamaInstansi.Properties.ReadOnly = true;
                        formSkPenjualanTambah.cePilihSemua.Enabled = true;
                        formSkPenjualanTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkPenjualanTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_PINDAHTANGAN = formSkPenjualanUbah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkPenjualanUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPenjualanUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkPenjualanUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPenjualanUbah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkPenjualanUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPenjualanUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPenjualanUbah.teNamaInstansi.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPenjualanUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPenjualanUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPenjualanUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPenjualanUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPenjualanUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPenjualanUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPenjualanUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkPenjualan.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkPenjualanUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPenjualanUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPenjualanUbah.teNamaInstansi.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPenjualanUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkPenjualan.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkPenjualan.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkPenjualanUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkPenjualan.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkPenjualan.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPenjualanUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkPenjualanUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkPenjualan.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkPenjualan.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkPenjualan.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkPenjualanUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPenjualanUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPenjualanUbah.teUraianKeputusan.Text;
                        //dataPenyama.THN_ANG = formSkPenjualanUbah.teTahunAnggaran.Text;
                        int _indeksData = dsGridRskPenjualan.IndexOf(gridSkPenjualan.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskPenjualan.Remove(gridSkPenjualan.dataTerpilih);
                        dsGridRskPenjualan.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskPenjualan.Remove(gridSkPenjualan.dataTerpilih);
                        break;
                }
                gridSkPenjualan.dsDataSource = dsGridRskPenjualan;
                gridSkPenjualan.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data Penjualan

        #endregion Penjualan

        #region tukarmenukar
        ucRskTmBmnGrid gridSkTmBmn;
        ucRskTmBmnForm formSkTmBmnTambah;
        ucRskTmBmnForm formSkTmBmnUbah;
        ucRskTmBmnForm formSkTmBmnDetail;
        private ArrayList dsGridRskTmBmn;
        SvcWasdalTmSkSelect.OutputParameters dOutRskTmBmn;
        SvcWasdalTmSkSelect.execute_pttClient ambilRskTmBmn;

        private void setEventTombolRskTmBmn()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskTmBmnTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskTmBmnUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskTmBmnHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskTmBmnDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskTmBmnKembaliKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskTmBmnRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskTmBmnMoreDataKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKembaliKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKeluarKlik);
        }

        private void initGridSkTmBmn()
        {
            //if (gridSkTmBmn == null)
            //{
            gridSkTmBmn = new AppPengguna.KKW.RSK.ucRskTmBmnGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskTmBmn),
                detailDataGrid = new DetailDataGrid(bbiRskTmBmnUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskTmBmn();
            setPanel(gridSkTmBmn);
        }

        #region --++ Tombol Ribbon Tukar Menukar
        private void bbiRskTmBmnTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan12), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkTmBmnTambah == null)
                {
                    formSkTmBmnTambah = new AppPengguna.KKW.RSK.ucRskTmBmnForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskTmBmn)
                    };
                }
                setPanel(formSkTmBmnTambah);
                formSkTmBmnTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskTmBmnUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkTmBmn.dataTerpilih != null)
            {
                if (formSkTmBmnUbah == null)
                {
                    formSkTmBmnUbah = new AppPengguna.KKW.RSK.ucRskTmBmnForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskTmBmn)
                    };
                }
                formSkTmBmnUbah.dataTerpilih = gridSkTmBmn.dataTerpilih;
                setPanel(formSkTmBmnUbah);
                formSkTmBmnUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskTmBmnHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkTmBmn.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkTmBmn.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalTmCrud.InputParameters parInp = new SvcWasdalTmCrud.InputParameters();
                        parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                        parInp.P_ID_SK_WASDAL_PINDAHTANGAN = gridSkTmBmn.dataTerpilih.ID_SK_WASDAL_PINDAHTANGAN;
                        parInp.P_ID_PEMOHON = gridSkTmBmn.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkTmBmn.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkTmBmn.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkTmBmn.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkTmBmn.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkTmBmn.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkTmBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkTmBmn.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkTmBmn.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkTmBmn.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkTmBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkTmBmn.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkTmBmn.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkTmBmn.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkTmBmn.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkTmBmn.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkTmBmn.dataTerpilih.UR_SATKER;
                        ambilDataTmBmn = new SvcWasdalTmCrud.execute_pttClient();
                        ambilDataTmBmn.Open();
                        ambilDataTmBmn.Beginexecute(parInp, new AsyncCallback(cudRskTmBmn), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskTmBmnDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkTmBmn.dataTerpilih != null)
            {
                if (formSkTmBmnDetail == null)
                {
                    formSkTmBmnDetail = new AppPengguna.KKW.RSK.ucRskTmBmnForm("A");
                    formSkTmBmnDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkTmBmnDetail.dataTerpilih = gridSkTmBmn.dataTerpilih;
                setPanel(formSkTmBmnDetail);
                formSkTmBmnDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskTmBmnKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkTmBmn);
            setTombolRskGrid();
            gridSkTmBmn.dsDataSource = dsGridRskTmBmn;
            gridSkTmBmn.displayData();
        }

        private void bbiRskTmBmnRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkTmBmn.teNamaKolom.Text = "";
            gridSkTmBmn.teCari.Text = "";
            gridSkTmBmn.fieldDicari = "";
            gridSkTmBmn.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskTmBmn();
        }

        private void bbiRskTmBmnMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskTmBmn();
            }
        }
        #endregion --++ Tombol Ribbon  Tukar Menukar

        private void nbiRskTmBmn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan12;
            konfigApp.kdPelayanan = "12";
            konfigApp.namaPelayanan = konfigApp.namaLayanan12;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkTmBmn();
            modeCari = false;
            gridSkTmBmn.teNamaKolom.Text = "";
            gridSkTmBmn.teCari.Text = "";
            gridSkTmBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskTmBmn();
        }

        #region --++ Ambil Data Tukar Menukar
        private void getInitRskTmBmn()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalTmSkSelect.InputParameters parInp = new SvcWasdalTmSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskTmBmn = new SvcWasdalTmSkSelect.execute_pttClient();
                ambilRskTmBmn.Open();
                ambilRskTmBmn.Beginexecute(parInp, new AsyncCallback(getRskTmBmn), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskTmBmn(IAsyncResult result)
        {
            try
            {
                dOutRskTmBmn = ambilRskTmBmn.Endexecute(result);
                ambilRskTmBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskTmBmn(dsRskTmBmn), dOutRskTmBmn);
            }
            catch (Exception ex)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskTmBmn(SvcWasdalTmSkSelect.OutputParameters dataOut);

        private void dsRskTmBmn(SvcWasdalTmSkSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_READ_WASDAL_PT_TUKAR.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_READ_WASDAL_PT_TUKAR[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRskTmBmn = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dataOut.SF_READ_WASDAL_PT_TUKAR[i].IS_TB = (dataOut.SF_READ_WASDAL_PT_TUKAR[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskTmBmn.Add(dataOut.SF_READ_WASDAL_PT_TUKAR[i]);
            }
            gridSkTmBmn.labelTotData.Text = "";
            gridSkTmBmn.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkTmBmn.sbCariOnline.Enabled = !modeCari;
            gridSkTmBmn.dsDataSource = dsGridRskTmBmn;
            gridSkTmBmn.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkTmBmn.teNamaKolom.Text.Trim();
                string xDua = gridSkTmBmn.teCari.Text.Trim();
                string xTiga = gridSkTmBmn.fieldDicari;
                gridSkTmBmn.gvGridSk.ClearColumnsFilter();
                gridSkTmBmn.teNamaKolom.Text = xSatu;
                gridSkTmBmn.teCari.Text = xDua;
                gridSkTmBmn.fieldDicari = xTiga;
            }
            else
                gridSkTmBmn.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskTmBmn(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskTmBmn();
        }
        #endregion Ambil TmBmn

        #region --++ Simpan Data Tukar Menukar
        SvcWasdalTmCrud.OutputParameters dOutAmbilDataTmBmn;
        SvcWasdalTmCrud.execute_pttClient ambilDataTmBmn;

        private void simpanDataRskTmBmn(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalTmCrud.InputParameters parInp = new SvcWasdalTmCrud.InputParameters();
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_SK_WASDAL_PINDAHTANGAN = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.idSkWasdal : formSkTmBmnUbah.idSkWasdal);
                parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.idPemohon : formSkTmBmnUbah.idPemohon);
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.rgJenisAset.EditValue.ToString() : formSkTmBmnUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teJabatan.Text.Trim() : formSkTmBmnUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teNmPenerbitSk.Properties.GetDataSourceValue("KD_INSTANSI", formSkTmBmnTambah.teNmPenerbitSk.ItemIndex).ToString() : formSkTmBmnUbah.teNmPenerbitSk.Properties.GetDataSourceValue("KD_INSTANSI", formSkTmBmnUbah.teNmPenerbitSk.ItemIndex).ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.kodePenerbitSkDetail : formSkTmBmnUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkTmBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkTmBmnTambah.teNilaiPenetapan.Text)) : (formSkTmBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkTmBmnUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teNipPenandaTangan.Text : formSkTmBmnUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teNamaPenandaTangan.Text : formSkTmBmnUbah.teNamaPenandaTangan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teTahunAnggaran.Text : formSkTmBmnUbah.teTahunAnggaran.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teNmPenerbitSk.Text : formSkTmBmnUbah.teNmPenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teNamaInstansi.Text : formSkTmBmnUbah.teNamaInstansi.Text);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teNomorSk.Text.Trim() : formSkTmBmnUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teTanggalSk.EditValue : formSkTmBmnUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teJenisPemohon.Text : formSkTmBmnUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.tipePengelola : formSkTmBmnUbah.tipePengelola);
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teUraianKeputusan.Text : formSkTmBmnUbah.teUraianKeputusan.Text);

                parInp.P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teAlamatPihakLain.Text : formSkTmBmnUbah.teAlamatPihakLain.Text);

                parInp.P_NM_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teNamaPihakLain.Text : formSkTmBmnUbah.teNamaPihakLain.Text);
                parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.tePeruntukan.Text : formSkTmBmnUbah.tePeruntukan.Text);
                parInp.P_ASET_PENGGANTI_JENIS = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teJnsAsetPengganti.Text : formSkTmBmnUbah.teJnsAsetPengganti.Text);
                parInp.P_ASET_PENGGANTI_KUANTITASSpecified = true;
                parInp.P_ASET_PENGGANTI_KUANTITAS = ((_mode == "C" || _mode == "CU") ? Convert.ToDecimal(formSkTmBmnTambah.teKuantitasAsetPengganti.Text) : Convert.ToDecimal(formSkTmBmnUbah.teKuantitasAsetPengganti.Text));
                parInp.P_ASET_PENGGANTI_LUASSpecified = true;
                parInp.P_ASET_PENGGANTI_LUAS = ((_mode == "C" || _mode == "CU") ? Convert.ToDecimal(formSkTmBmnTambah.teLuasAsetPengganti.Text) : Convert.ToDecimal(formSkTmBmnUbah.teLuasAsetPengganti.Text));
                parInp.P_ASET_PENGGANTI_ALAMAT = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teAlamatAsetPengganti.Text : formSkTmBmnUbah.teAlamatAsetPengganti.Text);
                //parInp.P_FILE_SK = ((_mode == "C" || _mode == "CU") ? formSkTmBmnTambah.teFileSk.Text : formSkTmBmnUbah.teFileSk.Text);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkTmBmnTambah.filePath) : konfigApp.FileToByteArray(formSkTmBmnUbah.filePath));
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;
                ambilDataTmBmn = new SvcWasdalTmCrud.execute_pttClient();
                ambilDataTmBmn.Open();
                ambilDataTmBmn.Beginexecute(parInp, new AsyncCallback(cudRskTmBmn), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskTmBmn(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataTmBmn = ambilDataTmBmn.Endexecute(result);
                ambilDataTmBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsTmBmn(this.ubahDsTmBmn), dOutAmbilDataTmBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsTmBmn(SvcWasdalTmCrud.OutputParameters dataOutTmBmnCrud);

        private void ubahDsTmBmn(SvcWasdalTmCrud.OutputParameters dataOutTmBmnCrud)
        {
            if (dataOutTmBmnCrud.PO_RESULT == "Y")
            {
                SvcWasdalTmSkSelect.WASDALSROW_READ_WASDAL_PT_TUKAR dataPenyama = new SvcWasdalTmSkSelect.WASDALSROW_READ_WASDAL_PT_TUKAR();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ID_SK_WASDAL_PINDAHTANGAN = formSkTmBmnTambah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkTmBmnTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkTmBmnTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkTmBmnTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkTmBmnTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkTmBmnTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkTmBmnTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkTmBmnTambah.teNamaInstansi.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkTmBmnTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkTmBmnTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkTmBmnTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkTmBmnTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkTmBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkTmBmnTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkTmBmnTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkTmBmnTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkTmBmnTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkTmBmnTambah.teNamaInstansi.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkTmBmnTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskTmBmn == null ? 1 : dsGridRskTmBmn.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkTmBmnTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkTmBmnTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkTmBmnTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkTmBmnTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkTmBmnTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkTmBmnTambah.teUraianKeputusan.Text;
                        //dataPenyama.THN_ANG = formSkTmBmnTambah.teTahunAnggaran.Text;
                        dataPenyama.NM_PHK_LAIN = formSkTmBmnTambah.teNamaPihakLain.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkTmBmnTambah.teAlamatPihakLain.Text;

                        dsGridRskTmBmn.Add(dataPenyama);
                        formSkTmBmnTambah.gcDaftarAset.Enabled = true;
                        formSkTmBmnTambah.sbTambah.Enabled = true;
                        formSkTmBmnTambah.sbHapus.Enabled = true;
                        formSkTmBmnTambah.sbValidasi.Enabled = true;
                        formSkTmBmnTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkTmBmnTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkTmBmnTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkTmBmnTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkTmBmnTambah.sbCariPemohon.Enabled = false;
                        formSkTmBmnTambah.sbRefresh.Enabled = true;
                        formSkTmBmnTambah.teNamaInstansi.Properties.ReadOnly = true;
                        formSkTmBmnTambah.cePilihSemua.Enabled = true;
                        formSkTmBmnTambah.teKodePemohon.Properties.ReadOnly = true;
                        formSkTmBmnTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_PINDAHTANGAN = formSkTmBmnUbah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkTmBmnUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkTmBmnUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkTmBmnUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkTmBmnUbah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkTmBmnUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkTmBmnUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkTmBmnUbah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkTmBmnUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkTmBmnUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkTmBmnUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkTmBmnUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkTmBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkTmBmnUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkTmBmnUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkTmBmn.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkTmBmnUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkTmBmnUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkTmBmnUbah.teNmPenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkTmBmnUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkTmBmn.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkTmBmn.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkTmBmnUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkTmBmn.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkTmBmn.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkTmBmnUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkTmBmnUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkTmBmn.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkTmBmn.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkTmBmn.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkTmBmnUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkTmBmnUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkTmBmnUbah.teUraianKeputusan.Text;
                        //dataPenyama.THN_ANG = formSkTmBmnUbah.teTahunAnggaran.Text;
                        //dataPenyama.JNS_ASET_PENGGANTI = formSkTmBmnUbah.teJnsAsetPengganti.Text;
                        //dataPenyama.ALAMAT_ASET_PENGGANTI = formSkTmBmnUbah.teAlamatAsetPengganti.Text;
                        //dataPenyama.PERUNTUKAN = formSkTmBmnUbah.tePeruntukan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkTmBmnUbah.teAlamatPihakLain.Text;
                        dataPenyama.NM_PHK_LAIN = formSkTmBmnUbah.teNamaPihakLain.Text;

                        int _indeksData = dsGridRskTmBmn.IndexOf(gridSkTmBmn.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskTmBmn.Remove(gridSkTmBmn.dataTerpilih);
                        dsGridRskTmBmn.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskTmBmn.Remove(gridSkTmBmn.dataTerpilih);
                        break;
                }
                gridSkTmBmn.dsDataSource = dsGridRskTmBmn;
                gridSkTmBmn.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data TmBmn

        #endregion tukarmenukar

        #region hibah
        ucRskHibahGrid gridSkHibah;
        ucRskHibahForm formSkHibahTambah;
        ucRskHibahForm formSkHibahUbah;
        ucRskHibahForm formSkHibahDetail;
        private ArrayList dsGridRskHibah;
        SvcWasdalHibahSkSelect.OutputParameters dOutRskHibah;
        SvcWasdalHibahSkSelect.execute_pttClient ambilRskHibah;

        private void setEventTombolRskHibah()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskHibahTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskHibahUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskHibahHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskHibahDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskHibahKembaliKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskHibahRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskHibahMoreDataKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKembaliKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPspBmnKeluarKlik);
        }

        private void initGridSkHibah()
        {
            //if (gridSkHibah == null)
            //{
            gridSkHibah = new ucRskHibahGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskHibah),
                detailDataGrid = new DetailDataGrid(bbiRskHibahUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskHibah();
            setPanel(gridSkHibah);
        }

        #region --++ Tombol Ribbon Tukar Menukar
        private void bbiRskHibahTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan13), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkHibahTambah == null)
                {
                    formSkHibahTambah = new ucRskHibahForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskHibah)
                    };
                }
                setPanel(formSkHibahTambah);
                formSkHibahTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskHibahUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHibah.dataTerpilih != null)
            {
                if (formSkHibahUbah == null)
                {
                    formSkHibahUbah = new ucRskHibahForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskHibah)
                    };
                }
                formSkHibahUbah.dataTerpilih = gridSkHibah.dataTerpilih;
                setPanel(formSkHibahUbah);
                formSkHibahUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskHibahHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHibah.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkHibah.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalHibahCrud.InputParameters parInp = new SvcWasdalHibahCrud.InputParameters();
                        parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                        parInp.P_ID_SK_WASDAL_PINDAHTANGAN = gridSkHibah.dataTerpilih.ID_SK_WASDAL_PINDAHTANGAN;
                        parInp.P_ID_PEMOHON = gridSkHibah.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkHibah.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkHibah.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkHibah.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkHibah.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkHibah.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkHibah.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkHibah.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkHibah.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkHibah.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkHibah.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkHibah.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkHibah.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkHibah.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkHibah.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkHibah.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkHibah.dataTerpilih.UR_SATKER;
                        simpanDataHibah = new SvcWasdalHibahCrud.execute_pttClient();
                        simpanDataHibah.Open();
                        simpanDataHibah.Beginexecute(parInp, new AsyncCallback(cudRskHibah), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskHibahDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHibah.dataTerpilih != null)
            {
                if (formSkHibahDetail == null)
                {
                    formSkHibahDetail = new ucRskHibahForm("A");
                    formSkHibahDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkHibahDetail.dataTerpilih = gridSkHibah.dataTerpilih;
                setPanel(formSkHibahDetail);
                formSkHibahDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskHibahKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkHibah);
            setTombolRskGrid();
            gridSkHibah.dsDataSource = dsGridRskHibah;
            gridSkHibah.displayData();
        }

        private void bbiRskHibahRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkHibah.teNamaKolom.Text = "";
            gridSkHibah.teCari.Text = "";
            gridSkHibah.fieldDicari = "";
            gridSkHibah.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskHibah();
        }

        private void bbiRskHibahMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskHibah();
            }
        }
        #endregion --++ Tombol Ribbon Hibah

        private void nbiRskHibah_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan13;
            konfigApp.kdPelayanan = "13";
            konfigApp.namaPelayanan = konfigApp.namaLayanan13;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkHibah();
            modeCari = false;
            gridSkHibah.teNamaKolom.Text = "";
            gridSkHibah.teCari.Text = "";
            gridSkHibah.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskHibah();
        }

        #region --++ Ambil Data Hibah
        private void getInitRskHibah()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalHibahSkSelect.InputParameters parInp = new SvcWasdalHibahSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                // parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
             //   parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
             //" (ID_USER={1} " +
             // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
             // "OR ID_SATKER = {2} " +
             // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskHibah = new SvcWasdalHibahSkSelect.execute_pttClient();
                ambilRskHibah.Open();
                ambilRskHibah.Beginexecute(parInp, new AsyncCallback(getRskHibah), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskHibah(IAsyncResult result)
        {
            try
            {
                dOutRskHibah = ambilRskHibah.Endexecute(result);
                ambilRskHibah.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskHibah(dsRskHibah), dOutRskHibah);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskHibah(SvcWasdalHibahSkSelect.OutputParameters dataOut);

        private void dsRskHibah(SvcWasdalHibahSkSelect.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_READ_WASDAL_PT_HIBAH.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_READ_WASDAL_PT_HIBAH[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRskHibah = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dataOut.SF_READ_WASDAL_PT_HIBAH[i].IS_TB = (dataOut.SF_READ_WASDAL_PT_HIBAH[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskHibah.Add(dataOut.SF_READ_WASDAL_PT_HIBAH[i]);
            }
            gridSkHibah.labelTotData.Text = "";
            gridSkHibah.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkHibah.sbCariOnline.Enabled = !modeCari;
            gridSkHibah.dsDataSource = dsGridRskHibah;
            gridSkHibah.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkHibah.teNamaKolom.Text.Trim();
                string xDua = gridSkHibah.teCari.Text.Trim();
                string xTiga = gridSkHibah.fieldDicari;
                gridSkHibah.gvGridSk.ClearColumnsFilter();
                gridSkHibah.teNamaKolom.Text = xSatu;
                gridSkHibah.teCari.Text = xDua;
                gridSkHibah.fieldDicari = xTiga;
            }
            else
                gridSkHibah.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskHibah(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskHibah();
        }
        #endregion Ambil Hibah

        #region --++ Simpan Data Hibah
        SvcWasdalHibahCrud.OutputParameters dOutAmbilDataHibah;
        SvcWasdalHibahCrud.execute_pttClient simpanDataHibah;

        private void simpanDataRskHibah(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalHibahCrud.InputParameters parInp = new SvcWasdalHibahCrud.InputParameters();
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_SK_WASDAL_PINDAHTANGAN = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.idSkWasdal : formSkHibahUbah.idSkWasdal);//
                parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.idPemohon : formSkHibahUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.rgJenisAset.EditValue.ToString() : formSkHibahUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teJabatan.Text.Trim() : formSkHibahUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teNmPenerbitSk.Properties.GetDataSourceValue("KD_INSTANSI", formSkHibahTambah.teNmPenerbitSk.ItemIndex).ToString() : formSkHibahUbah.teNmPenerbitSk.Properties.GetDataSourceValue("KD_INSTANSI", formSkHibahUbah.teNmPenerbitSk.ItemIndex).ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.kodePenerbitSkDetail : formSkHibahUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkHibahTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHibahTambah.teNilaiPenetapan.Text)) : (formSkHibahUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHibahUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teNipPenandaTangan.Text : formSkHibahUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teNamaPenandaTangan.Text : formSkHibahUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teNmPenerbitSk.Text : formSkHibahUbah.teNmPenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teNamaInstansi.Text : formSkHibahUbah.teNamaInstansi.Text);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teNomorSk.Text.Trim() : formSkHibahUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teTanggalSk.EditValue : formSkHibahUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teJenisPemohon.Text : formSkHibahUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.tipePengelola : formSkHibahUbah.tipePengelola);
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teUraianKeputusan.Text : formSkHibahUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teTahunAnggaran.Text : formSkHibahUbah.teTahunAnggaran.Text);

                parInp.P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teAlamatPihakLain.Text : formSkHibahUbah.teAlamatPihakLain.Text);
                //parInp.P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkHibahTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkHibahTambah.teJangkaWaktu.Text)) : (formSkHibahUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkHibahUbah.teJangkaWaktu.Text)));
                //parInp.P_JNS_MITRA = "";
                parInp.P_NM_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teNamaPihakLain.Text : formSkHibahUbah.teNamaPihakLain.Text);
                //parInp.P_PERIODE = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.tePeriode.Text : formSkHibahUbah.tePeriode.Text);
                parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.tePeruntukan.Text : formSkHibahUbah.tePeruntukan.Text);
                //parInp.P_FILE_SK = ((_mode == "C" || _mode == "CU") ? formSkHibahTambah.teFileSk.Text : formSkHibahUbah.teFileSk.Text);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkHibahTambah.filePath) : konfigApp.FileToByteArray(formSkHibahUbah.filePath));
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;

                simpanDataHibah = new SvcWasdalHibahCrud.execute_pttClient();
                simpanDataHibah.Open();
                simpanDataHibah.Beginexecute(parInp, new AsyncCallback(cudRskHibah), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskHibah(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataHibah = simpanDataHibah.Endexecute(result);
                simpanDataHibah.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsHibah(this.ubahDsHibah), dOutAmbilDataHibah);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsHibah(SvcWasdalHibahCrud.OutputParameters dataOutHibahCrud);

        private void ubahDsHibah(SvcWasdalHibahCrud.OutputParameters dataOutHibahCrud)
        {
            if (dataOutHibahCrud.PO_RESULT == "Y")
            {
                SvcWasdalHibahSkSelect.WASDALSROW_READ_WASDAL_PT_HIBAH dataPenyama = new SvcWasdalHibahSkSelect.WASDALSROW_READ_WASDAL_PT_HIBAH();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ID_SK_WASDAL_PINDAHTANGAN = formSkHibahTambah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkHibahTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkHibahTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkHibahTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkHibahTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkHibahTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkHibahTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkHibahTambah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkHibahTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkHibahTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkHibahTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkHibahTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkHibahTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHibahTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkHibahTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkHibahTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkHibahTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkHibahTambah.teNamaInstansi.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkHibahTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskHibah == null ? 1 : dsGridRskHibah.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkHibahTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkHibahTambah.teTanggalSk.Text);
                        dataPenyama.THN_ANG = formSkHibahTambah.teTahunAnggaran.Text;
                        dataPenyama.TIPE_PEMOHON = formSkHibahTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkHibahTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkHibahTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkHibahTambah.teUraianKeputusan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkHibahTambah.teNamaPihakLain.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkHibahTambah.teAlamatPihakLain.Text;
                        if (dsGridRskHibah == null) dsGridRskHibah = new ArrayList();
                        dsGridRskHibah.Add(dataPenyama);
                        formSkHibahTambah.gcDaftarAset.Enabled = true;
                        formSkHibahTambah.sbTambah.Enabled = true;
                        formSkHibahTambah.sbHapus.Enabled = true;
                        formSkHibahTambah.sbValidasi.Enabled = true;
                        formSkHibahTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkHibahTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkHibahTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkHibahTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkHibahTambah.sbCariPemohon.Enabled = false;
                        formSkHibahTambah.sbRefresh.Enabled = true;
                        formSkHibahTambah.teNamaInstansi.Properties.ReadOnly = true;
                        formSkHibahTambah.cePilihSemua.Enabled = true;
                        formSkHibahTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkHibahTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_PINDAHTANGAN = formSkHibahUbah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkHibahUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkHibahUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkHibahUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkHibahUbah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkHibahUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkHibahUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkHibahUbah.teNmPenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkHibahUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkHibahUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkHibahUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkHibahUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkHibahUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHibahUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkHibahUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkHibah.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkHibahUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkHibahUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkHibahUbah.teNamaInstansi.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkHibahUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkHibah.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkHibah.dataTerpilih.NUM;
                        //dataPenyama.PERUNTUKAN = formSkHibahUbah.tePeruntukan.Text;
                        dataPenyama.SK_KEPUTUSAN = formSkHibahUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkHibah.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkHibah.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkHibahUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkHibahUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkHibah.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkHibah.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkHibah.dataTerpilih.TOTAL_DATA;
                        //dataPenyama.THN_ANG = formSkHibahUbah.teTahunAnggaran.Text;
                        dataPenyama.UR_KL = formSkHibahUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkHibahUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkHibahUbah.teUraianKeputusan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkHibahUbah.teNamaPihakLain.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkHibahUbah.teAlamatPihakLain.Text;


                        int _indeksData = dsGridRskHibah.IndexOf(gridSkHibah.dataTerpilih);
                        _indeksData = (_indeksData < 0) ? 0 : _indeksData;
                        dsGridRskHibah.Remove(gridSkHibah.dataTerpilih);
                        dsGridRskHibah.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskHibah.Remove(gridSkHibah.dataTerpilih);
                        break;
                }
                gridSkHibah.dsDataSource = dsGridRskHibah;
                gridSkHibah.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data Hibah

        #endregion

        #region pmp
        ucRskPmpGrid gridSkPmp;
        ucRskPmpForm formSkPmpTambah;
        ucRskPmpForm formSkPmpUbah;
        ucRskPmpForm formSkPmpDetail;
        private ArrayList dsGridRskPmp;
        SvcWasdalPmpSkSelect.OutputParameters dOutRskPmp;
        SvcWasdalPmpSkSelect.execute_pttClient ambilRskPmp;

        private void setEventTombolRskPmp()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskPmpTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskPmpUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskPmpHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskPmpDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskPmpKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskPmpRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskPmpMoreDataKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskPmpKembaliKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskPmpKeluarKlik);
        }

        private void initGridSkPmp()
        {
            //if (gridSkPmp == null)
            //{
            gridSkPmp = new ucRskPmpGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskPmp),
                detailDataGrid = new DetailDataGrid(bbiRskPmpUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskPmp();
            setPanel(gridSkPmp);
        }

        #region --++ Tombol Ribbon PMP
        private void bbiRskPmpTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan14), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkPmpTambah == null)
                {
                    formSkPmpTambah = new ucRskPmpForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPmp)
                    };
                }
                setPanel(formSkPmpTambah);
                formSkPmpTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskPmpUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPmp.dataTerpilih != null)
            {
                if (formSkPmpUbah == null)
                {
                    formSkPmpUbah = new ucRskPmpForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskPmp)
                    };
                }
                formSkPmpUbah.dataTerpilih = gridSkPmp.dataTerpilih;
                setPanel(formSkPmpUbah);
                formSkPmpUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPmpHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPmp.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkPmp.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalPmpCrud.InputParameters parInp = new SvcWasdalPmpCrud.InputParameters();
                        parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                        parInp.P_ID_SK_WASDAL_PINDAHTANGAN = gridSkPmp.dataTerpilih.ID_SK_WASDAL_PINDAHTANGAN;
                        parInp.P_ID_PEMOHON = gridSkPmp.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkPmp.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkPmp.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkPmp.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkPmp.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkPmp.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkPmp.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkPmp.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkPmp.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkPmp.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkPmp.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkPmp.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkPmp.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkPmp.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkPmp.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkPmp.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkPmp.dataTerpilih.UR_SATKER;
                        ambilDataPmp = new SvcWasdalPmpCrud.execute_pttClient();
                        ambilDataPmp.Open();
                        ambilDataPmp.Beginexecute(parInp, new AsyncCallback(cudRskPmp), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskPmpDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkPmp.dataTerpilih != null)
            {
                if (formSkPmpDetail == null)
                {
                    formSkPmpDetail = new ucRskPmpForm("A");
                    formSkPmpDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkPmpDetail.dataTerpilih = gridSkPmp.dataTerpilih;
                setPanel(formSkPmpDetail);
                formSkPmpDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskPmpKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkPmp);
            setTombolRskGrid();
            gridSkPmp.dsDataSource = dsGridRskPmp;
            gridSkPmp.displayData();
        }

        private void bbiRskPmpRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkPmp.teNamaKolom.Text = "";
            gridSkPmp.teCari.Text = "";
            gridSkPmp.fieldDicari = "";
            gridSkPmp.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskPmp();
        }

        private void bbiRskPmpMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskPmp();
            }
        }

        private void bbiRskPmpKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskPmpKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }
        #endregion --++ Tombol Ribbon  Tukar Menukar

        private void nbiRskPmp_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan14;
            konfigApp.kdPelayanan = "14";
            konfigApp.namaPelayanan = konfigApp.namaLayanan14;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkPmp();
            modeCari = false;
            gridSkPmp.teNamaKolom.Text = "";
            gridSkPmp.teCari.Text = "";
            gridSkPmp.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskPmp();
        }

        #region --++ Ambil Data PMP
        private void getInitRskPmp()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPmpSkSelect.InputParameters parInp = new SvcWasdalPmpSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskPmp = new SvcWasdalPmpSkSelect.execute_pttClient();
                ambilRskPmp.Open();
                ambilRskPmp.Beginexecute(parInp, new AsyncCallback(getRskPmp), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskPmp(IAsyncResult result)
        {
            try
            {
                dOutRskPmp = ambilRskPmp.Endexecute(result);
                ambilRskPmp.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskPmp(dsRskPmp), dOutRskPmp);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskPmp(SvcWasdalPmpSkSelect.OutputParameters dataOut);

        private void dsRskPmp(SvcWasdalPmpSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_PT_MODAL.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_READ_WASDAL_PT_MODAL[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
            }

            if (dataInisial == true)
            {
                dsGridRskPmp = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_PT_MODAL[i].IS_TB = (dataOut.SF_READ_WASDAL_PT_MODAL[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskPmp.Add(dataOut.SF_READ_WASDAL_PT_MODAL[i]);
            }
            gridSkPmp.labelTotData.Text = "";
            gridSkPmp.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkPmp.sbCariOnline.Enabled = !modeCari;
            gridSkPmp.dsDataSource = dsGridRskPmp;
            gridSkPmp.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkPmp.teNamaKolom.Text.Trim();
                string xDua = gridSkPmp.teCari.Text.Trim();
                string xTiga = gridSkPmp.fieldDicari;
                gridSkPmp.gvGridSk.ClearColumnsFilter();
                gridSkPmp.teNamaKolom.Text = xSatu;
                gridSkPmp.teCari.Text = xDua;
                gridSkPmp.fieldDicari = xTiga;
            }
            else
                gridSkPmp.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskPmp(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskPmp();
        }
        #endregion Ambil Pmp

        #region --++ Simpan Data PMP
        SvcWasdalPmpCrud.OutputParameters dOutAmbilDataPmp;
        SvcWasdalPmpCrud.execute_pttClient ambilDataPmp;

        private void simpanDataRskPmp(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalPmpCrud.InputParameters parInp = new SvcWasdalPmpCrud.InputParameters();
                parInp.P_ID_SK_WASDAL_PINDAHTANGAN = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.idSkWasdal : formSkPmpUbah.idSkWasdal);//
                parInp.P_ID_SK_WASDAL_PINDAHTANGANSpecified = true;
                parInp.P_ID_KPKNL = konfigApp.idKpknl;//mngapa diisi dengan ID_KANWIL
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.idPemohon : formSkPmpUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.rgJenisAset.EditValue.ToString() : formSkPmpUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teJabatan.Text.Trim() : formSkPmpUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.lePenerbitSk.EditValue.ToString() : formSkPmpUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.kodePenerbitSkDetail : formSkPmpUbah.kodePenerbitSkDetail);
                //parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkPmpTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPmpTambah.teNilaiPenetapan.Text)) : (formSkPmpUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPmpUbah.teNilaiPenetapan.Text)));
                //parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teNipPenandaTangan.Text : formSkPmpUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teNamaPenandaTangan.Text : formSkPmpUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.lePenerbitSk.Text : formSkPmpUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teNmPenerbitSkDetail.Text : formSkPmpUbah.namaPenerbitSkDetail);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teNomorSk.Text.Trim() : formSkPmpUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teTanggalSk.EditValue : formSkPmpUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teJenisPemohon.Text : formSkPmpUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teUraianKeputusan.Text : formSkPmpUbah.teUraianKeputusan.Text);
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teTahunAnggaran.Text : formSkPmpUbah.teTahunAnggaran.Text);
                parInp.P_ALAMAT_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teAlamatPihakLain.Text : formSkPmpUbah.teAlamatPihakLain.Text);
                //parInp.P_JANGKA_WAKTU = ((_mode == "C" || _mode == "CU") ? (formSkPmpTambah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPmpTambah.teJangkaWaktu.Text)) : (formSkPmpUbah.teJangkaWaktu.Text == "" ? 0 : Convert.ToDecimal(formSkPmpUbah.teJangkaWaktu.Text)));
                //parInp.P_JNS_MITRA = "";
                parInp.P_NM_PHK_LAIN = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.teNamaPihakLain.Text : formSkPmpUbah.teNamaPihakLain.Text);
                //parInp.P_PERIODE = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.tePeriode.Text : formSkPmpUbah.tePeriode.Text);
                parInp.P_PERUNTUKAN = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.tePeruntukan.Text : formSkPmpUbah.tePeruntukan.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkPmpTambah.tipePengelola : formSkPmpUbah.tipePengelola);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkPmpTambah.filePath) : konfigApp.FileToByteArray(formSkPmpUbah.filePath));
                parInp.P_KD_KL = konfigApp.kodeKl;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_UR_SATKER = konfigApp.namaSatker;

                ambilDataPmp = new SvcWasdalPmpCrud.execute_pttClient();
                ambilDataPmp.Open();
                ambilDataPmp.Beginexecute(parInp, new AsyncCallback(cudRskPmp), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskPmp(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataPmp = ambilDataPmp.Endexecute(result);
                ambilDataPmp.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsPmp(this.ubahDsPmp), dOutAmbilDataPmp);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsPmp(SvcWasdalPmpCrud.OutputParameters dataOutPmpCrud);

        private void ubahDsPmp(SvcWasdalPmpCrud.OutputParameters dataOutPmpCrud)
        {
            if (dataOutPmpCrud.PO_RESULT == "Y")
            {
                SvcWasdalPmpSkSelect.WASDALSROW_READ_WASDAL_PT_MODAL dataPenyama = new SvcWasdalPmpSkSelect.WASDALSROW_READ_WASDAL_PT_MODAL();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ID_SK_WASDAL_PINDAHTANGAN = formSkPmpTambah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkPmpTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPmpTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkPmpTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPmpTambah.teJabatan.Text;
                        //dataPenyama.JANGKA_WAKTU = Convert.ToDecimal(formSkPmpTambah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkPmpTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPmpTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPmpTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPmpTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPmpTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPmpTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPmpTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPmpTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPmpTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPmpTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkPmpTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPmpTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PHK_LAIN = formSkPmpTambah.teNamaPihakLain.Text;
                        dataPenyama.NIP_PENANDATANGAN = formSkPmpTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPmpTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPmpTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskPmp == null ? 1 : dsGridRskPmp.Count + 1);
                        dataPenyama.PERUNTUKAN = formSkPmpTambah.tePeruntukan.Text;
                        dataPenyama.SK_KEPUTUSAN = formSkPmpTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPmpTambah.teTanggalSk.Text);
                        dataPenyama.THN_ANG = formSkPmpTambah.teTahunAnggaran.Text;
                        dataPenyama.TIPE_PEMOHON = formSkPmpTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkPmpTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPmpTambah.teNamaPemohon.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkPmpTambah.teAlamatPihakLain.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPmpTambah.teUraianKeputusan.Text;
                        if (dsGridRskPmp == null) dsGridRskPmp = new ArrayList();
                        dsGridRskPmp.Add(dataPenyama);
                        formSkPmpTambah.gcDaftarAset.Enabled = true;
                        formSkPmpTambah.sbTambah.Enabled = true;
                        formSkPmpTambah.sbHapus.Enabled = true;
                        formSkPmpTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkPmpTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkPmpTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkPmpTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkPmpTambah.sbCariPemohon.Enabled = false;
                        formSkPmpTambah.sbRefresh.Enabled = true;
                        formSkPmpTambah.sbValidasi.Enabled = true;
                        formSkPmpTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkPmpTambah.cePilihSemua.Enabled = true;
                        formSkPmpTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkPmpTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_SK_WASDAL_PINDAHTANGAN = formSkPmpUbah.idSkWasdal;
                        dataPenyama.ID_PEMOHON = formSkPmpUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkPmpUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkPmpUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkPmpUbah.teJabatan.Text;
                        //dataPenyama.JANGKA_WAKTU = Convert.ToDecimal(formSkPmpUbah.teJangkaWaktu.Text);
                        dataPenyama.KD_KL = formSkPmpUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkPmpUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkPmpUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkPmpUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkPmpUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkPmpUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkPmpUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkPmpUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkPmpUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkPmpUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkPmp.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkPmpUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkPmpUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkPmpUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkPmpUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkPmp.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NM_PHK_LAIN = formSkPmpUbah.teNamaPihakLain.Text;
                        dataPenyama.NUM = gridSkPmp.dataTerpilih.NUM;
                        dataPenyama.PERUNTUKAN = formSkPmpUbah.tePeruntukan.Text;
                        dataPenyama.SK_KEPUTUSAN = formSkPmpUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkPmp.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkPmp.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkPmpUbah.teTanggalSk.Text);
                        dataPenyama.THN_ANG = formSkPmpUbah.teTahunAnggaran.Text;
                        dataPenyama.TIPE_PEMOHON = formSkPmpUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkPmp.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkPmp.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkPmp.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkPmpUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkPmpUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkPmpUbah.teUraianKeputusan.Text;
                        dataPenyama.ALAMAT_PHK_LAIN = formSkPmpUbah.teAlamatPihakLain.Text;
                        int _indeksData = dsGridRskPmp.IndexOf(gridSkPmp.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskPmp.Remove(gridSkPmp.dataTerpilih);
                        dsGridRskPmp.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskPmp.Remove(gridSkPmp.dataTerpilih);
                        break;
                }
                gridSkPmp.dsDataSource = dsGridRskPmp;
                gridSkPmp.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data PMP

        #endregion PMP

        #region penghapusan
        #region -->> [15] Pemusnahan BMN
        ucRskMusnahBmnGrid gridSkMusnahBmn;
        ucRskMusnahBmnForm formSkMusnahBmnTambah;
        ucRskMusnahBmnForm formSkMusnahBmnUbah;
        ucRskMusnahBmnForm formSkMusnahBmnDetail;
        private ArrayList dsGridRskMusnahBmn;
        SvcWasdalHapusSkSelect.OutputParameters dOutRskMusnahBmn;
        SvcWasdalHapusSkSelect.execute_pttClient ambilRskMusnahBmn;

        private void setEventTombolRskMusnahBmn()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnMoreDataKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnKeluarKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskMusnahBmnKembaliKlik);
        }

        private void initGridSkMusnahBmn()
        {
            //if (gridSkMusnahBmn == null)
            //{
            gridSkMusnahBmn = new ucRskMusnahBmnGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskMusnahBmn),
                detailDataGrid = new DetailDataGrid(bbiRskMusnahBmnUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskMusnahBmn();
            setPanel(gridSkMusnahBmn);
        }

        #region --++ Tombol Ribbon Pemusnahan BMN
        private void bbiRskMusnahBmnTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan15), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkMusnahBmnTambah == null)
                {
                    formSkMusnahBmnTambah = new ucRskMusnahBmnForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskMusnahBmn),
                        //delegateDuaParameter = new DelegateDuaParameter(refreshDsGridMusnahBmn)
                    };
                }
                setPanel(formSkMusnahBmnTambah);
                formSkMusnahBmnTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskMusnahBmnUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkMusnahBmn.dataTerpilih != null)
            {
                if (formSkMusnahBmnUbah == null)
                {
                    formSkMusnahBmnUbah = new ucRskMusnahBmnForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskMusnahBmn),
                        //delegateDuaParameter = new DelegateDuaParameter(refreshDsGridMusnahBmn)
                    };
                }
                formSkMusnahBmnUbah.dataTerpilih = gridSkMusnahBmn.dataTerpilih;
                setPanel(formSkMusnahBmnUbah);
                formSkMusnahBmnUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskMusnahBmnHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkMusnahBmn.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkMusnahBmn.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalHapusCud.InputParameters parInp = new SvcWasdalHapusCud.InputParameters();
                        parInp.P_ID_SK_WASDAL_HAPUS = gridSkMusnahBmn.dataTerpilih.ID_SK_WASDAL_HAPUS;
                        parInp.P_ID_SK_WASDAL_HAPUSSpecified = true;
                        parInp.P_ID_PEMOHON = gridSkMusnahBmn.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkMusnahBmn.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkMusnahBmn.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkMusnahBmn.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkMusnahBmn.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkMusnahBmn.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkMusnahBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkMusnahBmn.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkMusnahBmn.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkMusnahBmn.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkMusnahBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkMusnahBmn.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkMusnahBmn.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkMusnahBmn.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkMusnahBmn.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkMusnahBmn.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkMusnahBmn.dataTerpilih.UR_SATKER;
                        simpanDataMusnahBmn = new SvcWasdalHapusCud.execute_pttClient();
                        simpanDataMusnahBmn.Open();
                        simpanDataMusnahBmn.Beginexecute(parInp, new AsyncCallback(cudRskMusnahBmn), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskMusnahBmnDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkMusnahBmn.dataTerpilih != null)
            {
                if (formSkMusnahBmnDetail == null)
                {
                    formSkMusnahBmnDetail = new ucRskMusnahBmnForm("A");
                    formSkMusnahBmnDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkMusnahBmnDetail.dataTerpilih = gridSkMusnahBmn.dataTerpilih;
                setPanel(formSkMusnahBmnDetail);
                formSkMusnahBmnDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskMusnahBmnKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkMusnahBmn);
            setTombolRskGrid();
            gridSkMusnahBmn.dsDataSource = dsGridRskMusnahBmn;
            gridSkMusnahBmn.displayData();
        }

        private void bbiRskMusnahBmnRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkMusnahBmn.teNamaKolom.Text = "";
            gridSkMusnahBmn.teCari.Text = "";
            gridSkMusnahBmn.fieldDicari = "";
            gridSkMusnahBmn.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskMusnahBmn();
        }

        private void bbiRskMusnahBmnMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskMusnahBmn();
            }
        }

        private void bbiRskMusnahBmnKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskMusnahBmnKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }

        #endregion --++ Tombol Ribbon  Pemusnahan BMN

        private void nbiRskPemusnahanBmn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan15;
            konfigApp.kdPelayanan = "15";
            konfigApp.namaPelayanan = konfigApp.namaLayanan15;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkMusnahBmn();
            modeCari = false;
            gridSkMusnahBmn.teNamaKolom.Text = "";
            gridSkMusnahBmn.teCari.Text = "";
            gridSkMusnahBmn.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskMusnahBmn();
        }

        #region --++ Ambil Data Pemusnahan BMN
        private void getInitRskMusnahBmn()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalHapusSkSelect.InputParameters parInp = new SvcWasdalHapusSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND ID_KPKNL = {1} AND THN_ANG='{2}' {3}", konfigApp.kdPelayanan, konfigApp.idKpknl, konfigApp.tahunAnggaran, this.strCari);
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskMusnahBmn = new SvcWasdalHapusSkSelect.execute_pttClient();
                ambilRskMusnahBmn.Open();
                ambilRskMusnahBmn.Beginexecute(parInp, new AsyncCallback(getRskMusnahBmn), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskMusnahBmn(IAsyncResult result)
        {
            try
            {
                dOutRskMusnahBmn = ambilRskMusnahBmn.Endexecute(result);
                ambilRskMusnahBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskMusnahBmn(dsRskMusnahBmn), dOutRskMusnahBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskMusnahBmn(SvcWasdalHapusSkSelect.OutputParameters dataOut);

        private void dsRskMusnahBmn(SvcWasdalHapusSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_HAPUS.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_READ_WASDAL_HAPUS[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
            }

            if (dataInisial == true)
            {
                dsGridRskMusnahBmn = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_HAPUS[i].IS_TB = (dataOut.SF_READ_WASDAL_HAPUS[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskMusnahBmn.Add(dataOut.SF_READ_WASDAL_HAPUS[i]);
            }
            gridSkMusnahBmn.labelTotData.Text = "";
            gridSkMusnahBmn.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkMusnahBmn.sbCariOnline.Enabled = !modeCari;
            gridSkMusnahBmn.dsDataSource = dsGridRskMusnahBmn;
            gridSkMusnahBmn.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkMusnahBmn.teNamaKolom.Text.Trim();
                string xDua = gridSkMusnahBmn.teCari.Text.Trim();
                string xTiga = gridSkMusnahBmn.fieldDicari;
                gridSkMusnahBmn.gvGridSk.ClearColumnsFilter();
                gridSkMusnahBmn.teNamaKolom.Text = xSatu;
                gridSkMusnahBmn.teCari.Text = xDua;
                gridSkMusnahBmn.fieldDicari = xTiga;
            }
            else
                gridSkMusnahBmn.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskMusnahBmn(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskMusnahBmn();
        }
        #endregion Ambil Pemusnahan BMN

        #region --++ Simpan Data Pemusnahan BMN
        SvcWasdalHapusCud.OutputParameters dOutAmbilDataMusnahBmn;
        SvcWasdalHapusCud.execute_pttClient simpanDataMusnahBmn;

        private void simpanDataRskMusnahBmn(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalHapusCud.InputParameters parInp = new SvcWasdalHapusCud.InputParameters();
                parInp.P_ID_SK_WASDAL_HAPUSSpecified = true;
                parInp.P_ID_SK_WASDAL_HAPUS = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.idSkWasdal : formSkMusnahBmnUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.idPemohon : formSkMusnahBmnUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_SATKER = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.idPemohon : formSkMusnahBmnUbah.idPemohon);//
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teKodePemohon.Text : formSkMusnahBmnUbah.teKodePemohon.Text);//
                parInp.P_UR_SATKER = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teNamaPemohon.Text : formSkMusnahBmnUbah.teNamaPemohon.Text);//

                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.rgJenisAset.EditValue.ToString() : formSkMusnahBmnUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teJabatan.Text.Trim() : formSkMusnahBmnUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.lePenerbitSk.EditValue.ToString() : formSkMusnahBmnUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.kodePenerbitSkDetail : formSkMusnahBmnUbah.kodePenerbitSkDetail);
                parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkMusnahBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkMusnahBmnTambah.teNilaiPenetapan.Text)) : (formSkMusnahBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkMusnahBmnUbah.teNilaiPenetapan.Text)));
                parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teNipPenandaTangan.Text : formSkMusnahBmnUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teNamaPenandaTangan.Text : formSkMusnahBmnUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.lePenerbitSk.Text : formSkMusnahBmnUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teNmPenerbitSkDetail.Text : formSkMusnahBmnUbah.namaPenerbitSkDetail);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teNomorSk.Text.Trim() : formSkMusnahBmnUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teTanggalSk.EditValue : formSkMusnahBmnUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teJenisPemohon.Text : formSkMusnahBmnUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teUraianKeputusan.Text : formSkMusnahBmnUbah.teUraianKeputusan.Text);
                parInp.P_ALASAN = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teAlasan.Text : formSkMusnahBmnUbah.teAlasan.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.tipePengelola : formSkMusnahBmnUbah.tipePengelola);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkMusnahBmnTambah.filePath) : konfigApp.FileToByteArray(formSkMusnahBmnUbah.filePath));
                parInp.P_NAMA_PENGADILAN = null;
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teTahunAnggaran.Text : formSkMusnahBmnUbah.teTahunAnggaran.Text);
                parInp.P_KD_KL = ((_mode == "C" || _mode == "CU") ? formSkMusnahBmnTambah.teKodeKl.Text : formSkMusnahBmnUbah.teKodeKl.Text);//

                simpanDataMusnahBmn = new SvcWasdalHapusCud.execute_pttClient();
                simpanDataMusnahBmn.Open();
                simpanDataMusnahBmn.Beginexecute(parInp, new AsyncCallback(cudRskMusnahBmn), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskMusnahBmn(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataMusnahBmn = simpanDataMusnahBmn.Endexecute(result);
                simpanDataMusnahBmn.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsMusnahBmn(this.ubahDsMusnahBmn), dOutAmbilDataMusnahBmn);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsMusnahBmn(SvcWasdalHapusCud.OutputParameters dataOutMusnahBmnCrud);

        private void ubahDsMusnahBmn(SvcWasdalHapusCud.OutputParameters dataOutMusnahBmnCrud)
        {
            if (dataOutMusnahBmnCrud.PO_RESULT == "Y")
            {
                SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS dataPenyama = new SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ALASAN = formSkMusnahBmnTambah.teAlasan.Text;
                        dataPenyama.ID_PEMOHON = formSkMusnahBmnTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkMusnahBmnTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkMusnahBmnTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkMusnahBmnTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkMusnahBmnTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkMusnahBmnTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkMusnahBmnTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkMusnahBmnTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkMusnahBmnTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkMusnahBmnTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkMusnahBmnTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkMusnahBmnTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkMusnahBmnTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkMusnahBmnTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkMusnahBmnTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkMusnahBmnTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkMusnahBmnTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkMusnahBmnTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskMusnahBmn == null ? 1 : dsGridRskMusnahBmn.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkMusnahBmnTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkMusnahBmnTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkMusnahBmnTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkMusnahBmnTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkMusnahBmnTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkMusnahBmnTambah.teUraianKeputusan.Text;
                        dataPenyama.NAMA_PENGADILAN = null;
                        if (dsGridRskMusnahBmn == null) dsGridRskMusnahBmn = new ArrayList();
                        dsGridRskMusnahBmn.Add(dataPenyama);
                        gridSkMusnahBmn.dataTerpilih = dataPenyama;
                        formSkMusnahBmnTambah.gcDaftarAset.Enabled = true;
                        formSkMusnahBmnTambah.sbTambah.Enabled = true;
                        formSkMusnahBmnTambah.sbHapus.Enabled = true;
                        formSkMusnahBmnTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkMusnahBmnTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkMusnahBmnTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkMusnahBmnTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkMusnahBmnTambah.sbCariPemohon.Enabled = false;
                        formSkMusnahBmnTambah.sbRefresh.Enabled = true;
                        formSkMusnahBmnTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkMusnahBmnTambah.cePilihSemua.Enabled = true;
                        formSkMusnahBmnTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkMusnahBmnTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ID_PEMOHON = formSkMusnahBmnUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkMusnahBmnUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkMusnahBmnUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkMusnahBmnUbah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkMusnahBmnUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkMusnahBmnUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkMusnahBmnUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkMusnahBmnUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkMusnahBmnUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkMusnahBmnUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkMusnahBmnUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkMusnahBmnUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkMusnahBmnUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkMusnahBmnUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkMusnahBmn.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkMusnahBmnUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkMusnahBmnUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkMusnahBmnUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkMusnahBmnUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkMusnahBmn.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkMusnahBmn.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkMusnahBmnUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkMusnahBmn.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkMusnahBmn.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkMusnahBmnUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkMusnahBmnUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkMusnahBmn.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkMusnahBmn.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkMusnahBmn.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkMusnahBmnUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkMusnahBmnUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkMusnahBmnUbah.teUraianKeputusan.Text;
                        int _indeksData = dsGridRskMusnahBmn.IndexOf(gridSkMusnahBmn.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskMusnahBmn.Remove(gridSkMusnahBmn.dataTerpilih);
                        dsGridRskMusnahBmn.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskMusnahBmn.Remove(gridSkMusnahBmn.dataTerpilih);
                        break;
                }
                gridSkMusnahBmn.dsDataSource = dsGridRskMusnahBmn;
                gridSkMusnahBmn.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data Pemusnahan BMN

        #region --++ Refresh Grid Karena Perubahan Tindak Lanjut
        private void refreshDsGridMusnahBmn(string _nilaiPenetapan = "0", string _kuantitas = "0")
        {
            SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS dataPenyama = new SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS();
            dataPenyama.ALASAN = gridSkMusnahBmn.dataTerpilih.ALASAN;
            dataPenyama.ID_PEMOHON = gridSkMusnahBmn.dataTerpilih.ID_PEMOHON;
            dataPenyama.ID_PEMOHONSpecified = gridSkMusnahBmn.dataTerpilih.ID_PEMOHONSpecified;
            dataPenyama.ID_SATKER = gridSkMusnahBmn.dataTerpilih.ID_SATKER;
            dataPenyama.ID_SATKERSpecified = gridSkMusnahBmn.dataTerpilih.ID_SATKERSpecified;
            dataPenyama.ID_USER = gridSkMusnahBmn.dataTerpilih.ID_USER;
            dataPenyama.ID_USERSpecified = gridSkMusnahBmn.dataTerpilih.ID_USERSpecified;
            dataPenyama.IS_TB = gridSkMusnahBmn.dataTerpilih.IS_TB;
            dataPenyama.JABATAN_TTD = gridSkMusnahBmn.dataTerpilih.JABATAN_TTD;
            dataPenyama.KD_KL = gridSkMusnahBmn.dataTerpilih.KD_KL;
            dataPenyama.KD_PELAYANAN = gridSkMusnahBmn.dataTerpilih.KD_PELAYANAN;
            dataPenyama.KD_PEMOHON = gridSkMusnahBmn.dataTerpilih.KD_PEMOHON;
            dataPenyama.KD_PENERBIT_SK = gridSkMusnahBmn.dataTerpilih.KD_PENERBIT_SK;
            dataPenyama.KD_PENERBIT_SK_DTL = gridSkMusnahBmn.dataTerpilih.KD_PENERBIT_SK_DTL;
            dataPenyama.KD_SATKER = gridSkMusnahBmn.dataTerpilih.KD_SATKER;
            dataPenyama.KUANTITAS_SK = Convert.ToDecimal(_kuantitas);
            dataPenyama.KUANTITAS_SKSpecified = gridSkMusnahBmn.dataTerpilih.KUANTITAS_SKSpecified;
            dataPenyama.NAMA_PENGADILAN = gridSkMusnahBmn.dataTerpilih.NAMA_PENGADILAN;
            dataPenyama.NILAI_PENETAPAN = Convert.ToDecimal(_nilaiPenetapan);
            dataPenyama.NILAI_PENETAPANSpecified = gridSkMusnahBmn.dataTerpilih.NILAI_PENETAPANSpecified;
            dataPenyama.NIP_PENANDATANGAN = gridSkMusnahBmn.dataTerpilih.NIP_PENANDATANGAN;
            dataPenyama.NM_PELAYANAN = gridSkMusnahBmn.dataTerpilih.NM_PELAYANAN;
            dataPenyama.NM_PEMOHON = gridSkMusnahBmn.dataTerpilih.NM_PEMOHON;
            dataPenyama.NM_PENANDATANGAN = gridSkMusnahBmn.dataTerpilih.NM_PENANDATANGAN;
            dataPenyama.NM_PENERBIT_SK = gridSkMusnahBmn.dataTerpilih.NM_PENERBIT_SK;
            dataPenyama.NM_PENERBIT_SK_DTL = gridSkMusnahBmn.dataTerpilih.NM_PENERBIT_SK_DTL;
            dataPenyama.NM_PENGGUNA = gridSkMusnahBmn.dataTerpilih.NM_PENGGUNA;
            dataPenyama.NUM = gridSkMusnahBmn.dataTerpilih.NUM;
            dataPenyama.NUMSpecified = gridSkMusnahBmn.dataTerpilih.NUMSpecified;
            dataPenyama.SK_KEPUTUSAN = gridSkMusnahBmn.dataTerpilih.SK_KEPUTUSAN;
            dataPenyama.STATUS_BMN = gridSkMusnahBmn.dataTerpilih.STATUS_BMN;
            dataPenyama.TGL_CREATED = gridSkMusnahBmn.dataTerpilih.TGL_CREATED;
            dataPenyama.TGL_CREATEDSpecified = gridSkMusnahBmn.dataTerpilih.TGL_CREATEDSpecified;
            dataPenyama.TGL_SK = gridSkMusnahBmn.dataTerpilih.TGL_SK;
            dataPenyama.TGL_SKSpecified = gridSkMusnahBmn.dataTerpilih.TGL_SKSpecified;
            dataPenyama.TIPE_PEMOHON = gridSkMusnahBmn.dataTerpilih.TIPE_PEMOHON;
            dataPenyama.TOT_BMN = gridSkMusnahBmn.dataTerpilih.TOT_BMN;
            dataPenyama.TOT_BMNSpecified = gridSkMusnahBmn.dataTerpilih.TOT_BMNSpecified;
            dataPenyama.TOT_STATUS = gridSkMusnahBmn.dataTerpilih.TOT_STATUS;
            dataPenyama.TOT_STATUSSpecified = gridSkMusnahBmn.dataTerpilih.TOT_STATUSSpecified;
            dataPenyama.TOTAL_DATA = gridSkMusnahBmn.dataTerpilih.TOTAL_DATA;
            dataPenyama.TOTAL_DATASpecified = gridSkMusnahBmn.dataTerpilih.TOTAL_DATASpecified;
            dataPenyama.UR_KL = gridSkMusnahBmn.dataTerpilih.UR_KL;
            dataPenyama.UR_SATKER = gridSkMusnahBmn.dataTerpilih.UR_SATKER;
            dataPenyama.URAIAN_KEPUTUSAN = gridSkMusnahBmn.dataTerpilih.URAIAN_KEPUTUSAN;
            int _indeksData = dsGridRskMusnahBmn.IndexOf(gridSkMusnahBmn.dataTerpilih);
            _indeksData = (_indeksData < 0 ? 0 : _indeksData);
            dsGridRskMusnahBmn.Remove(gridSkMusnahBmn.dataTerpilih);
            dsGridRskMusnahBmn.Insert(_indeksData, dataPenyama);
            gridSkMusnahBmn.dsDataSource = dsGridRskMusnahBmn;
            gridSkMusnahBmn.displayData();
        }
        #endregion

        #endregion Pemusnahan BMN

        #region -->> [16] Hapus BMN KPP
        ucRskHapusBmnKppGrid gridSkHapusBmnKpp;
        ucRskHapusBmnKppForm formSkHapusBmnKppTambah;
        ucRskHapusBmnKppForm formSkHapusBmnKppUbah;
        ucRskHapusBmnKppForm formSkHapusBmnKppDetail;
        private ArrayList dsGridRskHapusBmnKpp;
        SvcWasdalHapusSkSelect.OutputParameters dOutRskHapusBmnKpp;
        SvcWasdalHapusSkSelect.execute_pttClient ambilRskHapusBmnKpp;

        private void setEventTombolRskHapusBmnKpp()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppMoreDataKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppKeluarKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKppKembaliKlik);
        }

        private void initGridSkHapusBmnKpp()
        {
            //if (gridSkHapusBmnKpp == null)
            //{
            gridSkHapusBmnKpp = new ucRskHapusBmnKppGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskHapusBmnKpp),
                detailDataGrid = new DetailDataGrid(bbiRskHapusBmnKppUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskHapusBmnKpp();
            setPanel(gridSkHapusBmnKpp);
        }

        #region --++ Tombol Ribbon Hapus BMN KPP
        private void bbiRskHapusBmnKppTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan16), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkHapusBmnKppTambah == null)
                {
                    formSkHapusBmnKppTambah = new ucRskHapusBmnKppForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskHapusBmnKpp),
                        //delegateDuaParameter = new DelegateDuaParameter(refreshDsGridHapusBmnKpp)
                    };
                }
                setPanel(formSkHapusBmnKppTambah);
                formSkHapusBmnKppTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskHapusBmnKppUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHapusBmnKpp.dataTerpilih != null)
            {
                if (formSkHapusBmnKppUbah == null)
                {
                    formSkHapusBmnKppUbah = new ucRskHapusBmnKppForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskHapusBmnKpp),
                        //delegateDuaParameter = new DelegateDuaParameter(refreshDsGridHapusBmnKpp)
                    };
                }
                formSkHapusBmnKppUbah.dataTerpilih = gridSkHapusBmnKpp.dataTerpilih;
                setPanel(formSkHapusBmnKppUbah);
                formSkHapusBmnKppUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskHapusBmnKppHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHapusBmnKpp.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkHapusBmnKpp.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalHapusCud.InputParameters parInp = new SvcWasdalHapusCud.InputParameters();
                        parInp.P_ID_SK_WASDAL_HAPUS = gridSkHapusBmnKpp.dataTerpilih.ID_SK_WASDAL_HAPUS;
                        parInp.P_ID_SK_WASDAL_HAPUSSpecified = true;
                        parInp.P_ID_PEMOHON = gridSkHapusBmnKpp.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkHapusBmnKpp.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkHapusBmnKpp.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkHapusBmnKpp.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkHapusBmnKpp.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkHapusBmnKpp.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkHapusBmnKpp.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkHapusBmnKpp.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkHapusBmnKpp.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkHapusBmnKpp.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkHapusBmnKpp.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkHapusBmnKpp.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkHapusBmnKpp.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkHapusBmnKpp.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkHapusBmnKpp.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkHapusBmnKpp.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkHapusBmnKpp.dataTerpilih.UR_SATKER;
                        simpanDataHapusBmnKpp = new SvcWasdalHapusCud.execute_pttClient();
                        simpanDataHapusBmnKpp.Open();
                        simpanDataHapusBmnKpp.Beginexecute(parInp, new AsyncCallback(cudRskHapusBmnKpp), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskHapusBmnKppDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHapusBmnKpp.dataTerpilih != null)
            {
                if (formSkHapusBmnKppDetail == null)
                {
                    formSkHapusBmnKppDetail = new ucRskHapusBmnKppForm("A");
                    formSkHapusBmnKppDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkHapusBmnKppDetail.dataTerpilih = gridSkHapusBmnKpp.dataTerpilih;
                setPanel(formSkHapusBmnKppDetail);
                formSkHapusBmnKppDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskHapusBmnKppKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkHapusBmnKpp);
            setTombolRskGrid();
            gridSkHapusBmnKpp.dsDataSource = dsGridRskHapusBmnKpp;
            gridSkHapusBmnKpp.displayData();
        }

        private void bbiRskHapusBmnKppRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkHapusBmnKpp.teNamaKolom.Text = "";
            gridSkHapusBmnKpp.teCari.Text = "";
            gridSkHapusBmnKpp.fieldDicari = "";
            gridSkHapusBmnKpp.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskHapusBmnKpp();
        }

        private void bbiRskHapusBmnKppMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskHapusBmnKpp();
            }
        }

        private void bbiRskHapusBmnKppKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskHapusBmnKppKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }

        #endregion --++ Tombol Ribbon Hapus BMN KPP

        private void nbiRskHapusBmnKpp_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan16;
            konfigApp.kdPelayanan = "16";
            konfigApp.namaPelayanan = konfigApp.namaLayanan16;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkHapusBmnKpp();
            modeCari = false;
            gridSkHapusBmnKpp.teNamaKolom.Text = "";
            gridSkHapusBmnKpp.teCari.Text = "";
            gridSkHapusBmnKpp.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskHapusBmnKpp();
        }

        #region --++ Ambil Data Hapus BMN KPP
        private void getInitRskHapusBmnKpp()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalHapusSkSelect.InputParameters parInp = new SvcWasdalHapusSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND ID_KPKNL = {1} AND THN_ANG='{2}' {3}", konfigApp.kdPelayanan, konfigApp.idKpknl, konfigApp.tahunAnggaran, this.strCari);
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskHapusBmnKpp = new SvcWasdalHapusSkSelect.execute_pttClient();
                ambilRskHapusBmnKpp.Open();
                ambilRskHapusBmnKpp.Beginexecute(parInp, new AsyncCallback(getRskHapusBmnKpp), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskHapusBmnKpp(IAsyncResult result)
        {
            try
            {
                dOutRskHapusBmnKpp = ambilRskHapusBmnKpp.Endexecute(result);
                ambilRskHapusBmnKpp.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskHapusBmnKpp(dsRskHapusBmnKpp), dOutRskHapusBmnKpp);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskHapusBmnKpp(SvcWasdalHapusSkSelect.OutputParameters dataOut);

        private void dsRskHapusBmnKpp(SvcWasdalHapusSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_HAPUS.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_READ_WASDAL_HAPUS[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
            }

            if (dataInisial == true)
            {
                dsGridRskHapusBmnKpp = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_HAPUS[i].IS_TB = (dataOut.SF_READ_WASDAL_HAPUS[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskHapusBmnKpp.Add(dataOut.SF_READ_WASDAL_HAPUS[i]);
            }
            gridSkHapusBmnKpp.labelTotData.Text = "";
            gridSkHapusBmnKpp.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkHapusBmnKpp.sbCariOnline.Enabled = !modeCari;
            gridSkHapusBmnKpp.dsDataSource = dsGridRskHapusBmnKpp;
            gridSkHapusBmnKpp.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkHapusBmnKpp.teNamaKolom.Text.Trim();
                string xDua = gridSkHapusBmnKpp.teCari.Text.Trim();
                string xTiga = gridSkHapusBmnKpp.fieldDicari;
                gridSkHapusBmnKpp.gvGridSk.ClearColumnsFilter();
                gridSkHapusBmnKpp.teNamaKolom.Text = xSatu;
                gridSkHapusBmnKpp.teCari.Text = xDua;
                gridSkHapusBmnKpp.fieldDicari = xTiga;
            }
            else
                gridSkHapusBmnKpp.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskHapusBmnKpp(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskHapusBmnKpp();
        }
        #endregion Ambil Hapus BMN KPP

        #region --++ Simpan Data Hapus BMN KPP
        SvcWasdalHapusCud.OutputParameters dOutAmbilDataHapusBmnKpp;
        SvcWasdalHapusCud.execute_pttClient simpanDataHapusBmnKpp;

        private void simpanDataRskHapusBmnKpp(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalHapusCud.InputParameters parInp = new SvcWasdalHapusCud.InputParameters();
                parInp.P_ID_SK_WASDAL_HAPUSSpecified = true;
                parInp.P_ID_SK_WASDAL_HAPUS = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.idSkWasdal : formSkHapusBmnKppUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.idPemohon : formSkHapusBmnKppUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_SATKER = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.idPemohon : formSkHapusBmnKppUbah.idPemohon);//
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teKodePemohon.Text : formSkHapusBmnKppUbah.teKodePemohon.Text);//
                parInp.P_UR_SATKER = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teNamaPemohon.Text : formSkHapusBmnKppUbah.teNamaPemohon.Text);//

                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.rgJenisAset.EditValue.ToString() : formSkHapusBmnKppUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teJabatan.Text.Trim() : formSkHapusBmnKppUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.lePenerbitSk.EditValue.ToString() : formSkHapusBmnKppUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.kodePenerbitSkDetail : formSkHapusBmnKppUbah.kodePenerbitSkDetail);
                parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkHapusBmnKppTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKppTambah.teNilaiPenetapan.Text)) : (formSkHapusBmnKppUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKppUbah.teNilaiPenetapan.Text)));
                parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teNipPenandaTangan.Text : formSkHapusBmnKppUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teNamaPenandaTangan.Text : formSkHapusBmnKppUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.lePenerbitSk.Text : formSkHapusBmnKppUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teNmPenerbitSkDetail.Text : formSkHapusBmnKppUbah.namaPenerbitSkDetail);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teNomorSk.Text.Trim() : formSkHapusBmnKppUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teTanggalSk.EditValue : formSkHapusBmnKppUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teJenisPemohon.Text : formSkHapusBmnKppUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teUraianKeputusan.Text : formSkHapusBmnKppUbah.teUraianKeputusan.Text);
                parInp.P_ALASAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teAlasan.Text : formSkHapusBmnKppUbah.teAlasan.Text);
                parInp.P_NAMA_PENGADILAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teNamaPengadilan.Text.Trim() : formSkHapusBmnKppUbah.teNamaPengadilan.Text.Trim());
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.tipePengelola : formSkHapusBmnKppUbah.tipePengelola);
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkHapusBmnKppTambah.filePath) : konfigApp.FileToByteArray(formSkHapusBmnKppUbah.filePath));
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teTahunAnggaran.Text : formSkHapusBmnKppUbah.teTahunAnggaran.Text);
                parInp.P_KD_KL = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKppTambah.teKodeKl.Text : formSkHapusBmnKppUbah.teKodeKl.Text);//
                simpanDataHapusBmnKpp = new SvcWasdalHapusCud.execute_pttClient();
                simpanDataHapusBmnKpp.Open();
                simpanDataHapusBmnKpp.Beginexecute(parInp, new AsyncCallback(cudRskHapusBmnKpp), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskHapusBmnKpp(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataHapusBmnKpp = simpanDataHapusBmnKpp.Endexecute(result);
                simpanDataHapusBmnKpp.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsHapusBmnKpp(this.ubahDsHapusBmnKpp), dOutAmbilDataHapusBmnKpp);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsHapusBmnKpp(SvcWasdalHapusCud.OutputParameters dataOutHapusBmnKppCrud);

        private void ubahDsHapusBmnKpp(SvcWasdalHapusCud.OutputParameters dataOutHapusBmnKppCrud)
        {
            if (dataOutHapusBmnKppCrud.PO_RESULT == "Y")
            {
                SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS dataPenyama = new SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ALASAN = formSkHapusBmnKppTambah.teAlasan.Text;
                        dataPenyama.ID_PEMOHON = formSkHapusBmnKppTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkHapusBmnKppTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkHapusBmnKppTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkHapusBmnKppTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkHapusBmnKppTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkHapusBmnKppTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkHapusBmnKppTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkHapusBmnKppTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkHapusBmnKppTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkHapusBmnKppTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKppTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkHapusBmnKppTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKppTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkHapusBmnKppTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkHapusBmnKppTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkHapusBmnKppTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkHapusBmnKppTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkHapusBmnKppTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskHapusBmnKpp == null ? 1 : dsGridRskHapusBmnKpp.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkHapusBmnKppTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkHapusBmnKppTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkHapusBmnKppTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkHapusBmnKppTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkHapusBmnKppTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkHapusBmnKppTambah.teUraianKeputusan.Text;
                        dataPenyama.NAMA_PENGADILAN = formSkHapusBmnKppTambah.teNamaPengadilan.Text;
                        if (dsGridRskHapusBmnKpp == null) dsGridRskHapusBmnKpp = new ArrayList();
                        dsGridRskHapusBmnKpp.Add(dataPenyama);
                        gridSkHapusBmnKpp.dataTerpilih = dataPenyama;
                        formSkHapusBmnKppTambah.gcDaftarAset.Enabled = true;
                        formSkHapusBmnKppTambah.sbTambah.Enabled = true;
                        formSkHapusBmnKppTambah.sbHapus.Enabled = true;
                        formSkHapusBmnKppTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkHapusBmnKppTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkHapusBmnKppTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkHapusBmnKppTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkHapusBmnKppTambah.sbCariPemohon.Enabled = false;
                        formSkHapusBmnKppTambah.sbRefresh.Enabled = true;
                        formSkHapusBmnKppTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkHapusBmnKppTambah.cePilihSemua.Enabled = true;
                        formSkHapusBmnKppTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkHapusBmnKppTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ALASAN = formSkHapusBmnKppUbah.teAlasan.Text;
                        dataPenyama.ID_PEMOHON = formSkHapusBmnKppUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkHapusBmnKppUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkHapusBmnKppUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkHapusBmnKppUbah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkHapusBmnKppUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkHapusBmnKppUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkHapusBmnKppUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkHapusBmnKppUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkHapusBmnKppUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkHapusBmnKppUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKppUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkHapusBmnKppUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKppUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkHapusBmnKppUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkHapusBmnKpp.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkHapusBmnKppUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkHapusBmnKppUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkHapusBmnKppUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkHapusBmnKppUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkHapusBmnKpp.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkHapusBmnKpp.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkHapusBmnKppUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkHapusBmnKpp.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkHapusBmnKpp.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkHapusBmnKppUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkHapusBmnKppUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkHapusBmnKpp.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkHapusBmnKpp.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkHapusBmnKpp.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkHapusBmnKppUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkHapusBmnKppUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkHapusBmnKppUbah.teUraianKeputusan.Text;
                        dataPenyama.NAMA_PENGADILAN = formSkHapusBmnKppUbah.teNamaPengadilan.Text;
                        int _indeksData = dsGridRskHapusBmnKpp.IndexOf(gridSkHapusBmnKpp.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskHapusBmnKpp.Remove(gridSkHapusBmnKpp.dataTerpilih);
                        dsGridRskHapusBmnKpp.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskHapusBmnKpp.Remove(gridSkHapusBmnKpp.dataTerpilih);
                        break;
                }
                gridSkHapusBmnKpp.dsDataSource = dsGridRskHapusBmnKpp;
                gridSkHapusBmnKpp.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data Hapus BMN KPP

        #region --++ Refresh Grid Karena Perubahan Tindak Lanjut
        private void refreshDsGridHapusBmnKpp(string _nilaiPenetapan = "0", string _kuantitas = "0")
        {
            SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS dataPenyama = new SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS();
            dataPenyama.ALASAN = gridSkHapusBmnKpp.dataTerpilih.ALASAN;
            dataPenyama.ID_PEMOHON = gridSkHapusBmnKpp.dataTerpilih.ID_PEMOHON;
            dataPenyama.ID_PEMOHONSpecified = gridSkHapusBmnKpp.dataTerpilih.ID_PEMOHONSpecified;
            dataPenyama.ID_SATKER = gridSkHapusBmnKpp.dataTerpilih.ID_SATKER;
            dataPenyama.ID_SATKERSpecified = gridSkHapusBmnKpp.dataTerpilih.ID_SATKERSpecified;
            dataPenyama.ID_USER = gridSkHapusBmnKpp.dataTerpilih.ID_USER;
            dataPenyama.ID_USERSpecified = gridSkHapusBmnKpp.dataTerpilih.ID_USERSpecified;
            dataPenyama.IS_TB = gridSkHapusBmnKpp.dataTerpilih.IS_TB;
            dataPenyama.JABATAN_TTD = gridSkHapusBmnKpp.dataTerpilih.JABATAN_TTD;
            dataPenyama.KD_KL = gridSkHapusBmnKpp.dataTerpilih.KD_KL;
            dataPenyama.KD_PELAYANAN = gridSkHapusBmnKpp.dataTerpilih.KD_PELAYANAN;
            dataPenyama.KD_PEMOHON = gridSkHapusBmnKpp.dataTerpilih.KD_PEMOHON;
            dataPenyama.KD_PENERBIT_SK = gridSkHapusBmnKpp.dataTerpilih.KD_PENERBIT_SK;
            dataPenyama.KD_PENERBIT_SK_DTL = gridSkHapusBmnKpp.dataTerpilih.KD_PENERBIT_SK_DTL;
            dataPenyama.KD_SATKER = gridSkHapusBmnKpp.dataTerpilih.KD_SATKER;
            dataPenyama.KUANTITAS_SK = Convert.ToDecimal(_kuantitas);
            dataPenyama.KUANTITAS_SKSpecified = gridSkHapusBmnKpp.dataTerpilih.KUANTITAS_SKSpecified;
            dataPenyama.NAMA_PENGADILAN = gridSkHapusBmnKpp.dataTerpilih.NAMA_PENGADILAN;
            dataPenyama.NILAI_PENETAPAN = Convert.ToDecimal(_nilaiPenetapan);
            dataPenyama.NILAI_PENETAPANSpecified = gridSkHapusBmnKpp.dataTerpilih.NILAI_PENETAPANSpecified;
            dataPenyama.NIP_PENANDATANGAN = gridSkHapusBmnKpp.dataTerpilih.NIP_PENANDATANGAN;
            dataPenyama.NM_PELAYANAN = gridSkHapusBmnKpp.dataTerpilih.NM_PELAYANAN;
            dataPenyama.NM_PEMOHON = gridSkHapusBmnKpp.dataTerpilih.NM_PEMOHON;
            dataPenyama.NM_PENANDATANGAN = gridSkHapusBmnKpp.dataTerpilih.NM_PENANDATANGAN;
            dataPenyama.NM_PENERBIT_SK = gridSkHapusBmnKpp.dataTerpilih.NM_PENERBIT_SK;
            dataPenyama.NM_PENERBIT_SK_DTL = gridSkHapusBmnKpp.dataTerpilih.NM_PENERBIT_SK_DTL;
            dataPenyama.NM_PENGGUNA = gridSkHapusBmnKpp.dataTerpilih.NM_PENGGUNA;
            dataPenyama.NUM = gridSkHapusBmnKpp.dataTerpilih.NUM;
            dataPenyama.NUMSpecified = gridSkHapusBmnKpp.dataTerpilih.NUMSpecified;
            dataPenyama.SK_KEPUTUSAN = gridSkHapusBmnKpp.dataTerpilih.SK_KEPUTUSAN;
            dataPenyama.STATUS_BMN = gridSkHapusBmnKpp.dataTerpilih.STATUS_BMN;
            dataPenyama.TGL_CREATED = gridSkHapusBmnKpp.dataTerpilih.TGL_CREATED;
            dataPenyama.TGL_CREATEDSpecified = gridSkHapusBmnKpp.dataTerpilih.TGL_CREATEDSpecified;
            dataPenyama.TGL_SK = gridSkHapusBmnKpp.dataTerpilih.TGL_SK;
            dataPenyama.TGL_SKSpecified = gridSkHapusBmnKpp.dataTerpilih.TGL_SKSpecified;
            dataPenyama.TIPE_PEMOHON = gridSkHapusBmnKpp.dataTerpilih.TIPE_PEMOHON;
            dataPenyama.TOT_BMN = gridSkHapusBmnKpp.dataTerpilih.TOT_BMN;
            dataPenyama.TOT_BMNSpecified = gridSkHapusBmnKpp.dataTerpilih.TOT_BMNSpecified;
            dataPenyama.TOT_STATUS = gridSkHapusBmnKpp.dataTerpilih.TOT_STATUS;
            dataPenyama.TOT_STATUSSpecified = gridSkHapusBmnKpp.dataTerpilih.TOT_STATUSSpecified;
            dataPenyama.TOTAL_DATA = gridSkHapusBmnKpp.dataTerpilih.TOTAL_DATA;
            dataPenyama.TOTAL_DATASpecified = gridSkHapusBmnKpp.dataTerpilih.TOTAL_DATASpecified;
            dataPenyama.UR_KL = gridSkHapusBmnKpp.dataTerpilih.UR_KL;
            dataPenyama.UR_SATKER = gridSkHapusBmnKpp.dataTerpilih.UR_SATKER;
            dataPenyama.URAIAN_KEPUTUSAN = gridSkHapusBmnKpp.dataTerpilih.URAIAN_KEPUTUSAN;
            int _indeksData = dsGridRskHapusBmnKpp.IndexOf(gridSkHapusBmnKpp.dataTerpilih);
            _indeksData = (_indeksData < 0 ? 0 : _indeksData);
            dsGridRskHapusBmnKpp.Remove(gridSkHapusBmnKpp.dataTerpilih);
            dsGridRskHapusBmnKpp.Insert(_indeksData, dataPenyama);
            gridSkHapusBmnKpp.dsDataSource = dsGridRskHapusBmnKpp;
            gridSkHapusBmnKpp.displayData();
        }
        #endregion

        #endregion Hapus BMN KPP

        #region -->> [17] Hapus BMN KSL
        ucRskHapusBmnKslGrid gridSkHapusBmnKsl;
        ucRskHapusBmnKslForm formSkHapusBmnKslTambah;
        ucRskHapusBmnKslForm formSkHapusBmnKslUbah;
        ucRskHapusBmnKslForm formSkHapusBmnKslDetail;
        private ArrayList dsGridRskHapusBmnKsl;
        SvcWasdalHapusSkSelect.OutputParameters dOutRskHapusBmnKsl;
        SvcWasdalHapusSkSelect.execute_pttClient ambilRskHapusBmnKsl;

        private void setEventTombolRskHapusBmnKsl()
        {
            resetEventTombolRsk();
            bbiRskTambah.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslTambahKlik);
            bbiRskUbah.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslUbahKlik);
            bbiRskHapus.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslHapusKlik);
            bbiRskDetail.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslDetailKlik);
            bbiRskKembaliGrid.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslKembaliGridKlik);
            bbiRskRefresh.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslRefreshKlik);
            bbiRskMoreData.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslMoreDataKlik);
            bbiRskKeluar.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslKeluarKlik);
            bbiRskKembali.ItemClick += new ItemClickEventHandler(bbiRskHapusBmnKslKembaliKlik);
        }

        private void initGridSkHapusBmnKsl()
        {
            //if (gridSkHapusBmnKsl == null)
            //{
            gridSkHapusBmnKsl = new ucRskHapusBmnKslGrid()
            {
                cariDataOnline = new CariDataOnline(cariDataRskHapusBmnKsl),
                detailDataGrid = new DetailDataGrid(bbiRskHapusBmnKslUbahKlik)
            };
            //}
            setTombolRskGrid();
            setEventTombolRskHapusBmnKsl();
            setPanel(gridSkHapusBmnKsl);
        }

        #region --++ Tombol Ribbon  Hapus BMN KSL
        private void bbiRskHapusBmnKslTambahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(String.Format("Apakah anda ingin membuat Surat Keputusan Baru \n{0}?", konfigApp.namaLayanan15), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formSkHapusBmnKslTambah == null)
                {
                    formSkHapusBmnKslTambah = new ucRskHapusBmnKslForm("C")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskHapusBmnKsl),
                        //delegateDuaParameter = new DelegateDuaParameter(refreshDsGridHapusBmnKsl)
                    };
                }
                setPanel(formSkHapusBmnKslTambah);
                formSkHapusBmnKslTambah.inisialisasiForm();
                setTombolRskForm();
            }
        }

        private void bbiRskHapusBmnKslUbahKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHapusBmnKsl.dataTerpilih != null)
            {
                if (formSkHapusBmnKslUbah == null)
                {
                    formSkHapusBmnKslUbah = new ucRskHapusBmnKslForm("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRskHapusBmnKsl),
                        //delegateDuaParameter = new DelegateDuaParameter(refreshDsGridHapusBmnKsl)
                    };
                }
                formSkHapusBmnKslUbah.dataTerpilih = gridSkHapusBmnKsl.dataTerpilih;
                setPanel(formSkHapusBmnKslUbah);
                formSkHapusBmnKslUbah.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskHapusBmnKslHapusKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHapusBmnKsl.dataTerpilih != null)
            {
                if (MessageBox.Show(String.Format(konfigApp.teksKonfHapusSk, gridSkHapusBmnKsl.dataTerpilih.SK_KEPUTUSAN), konfigApp.judulKonfirmasi,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcWasdalHapusCud.InputParameters parInp = new SvcWasdalHapusCud.InputParameters();
                        parInp.P_ID_SK_WASDAL_HAPUS = gridSkHapusBmnKsl.dataTerpilih.ID_SK_WASDAL_HAPUS;
                        parInp.P_ID_SK_WASDAL_HAPUSSpecified = true;
                        parInp.P_ID_PEMOHON = gridSkHapusBmnKsl.dataTerpilih.ID_PEMOHON;
                        parInp.P_ID_PEMOHONSpecified = true;
                        parInp.P_ID_USER = gridSkHapusBmnKsl.dataTerpilih.ID_USER;
                        parInp.P_ID_USERSpecified = true;
                        parInp.P_IS_TB = gridSkHapusBmnKsl.dataTerpilih.IS_TB;
                        parInp.P_JABATAN_TTD = gridSkHapusBmnKsl.dataTerpilih.JABATAN_TTD;
                        parInp.P_KD_PELAYANAN = gridSkHapusBmnKsl.dataTerpilih.KD_PELAYANAN;
                        parInp.P_KD_PENERBIT_SK = gridSkHapusBmnKsl.dataTerpilih.KD_PENERBIT_SK;
                        parInp.P_KD_PENERBIT_SK_DTL = gridSkHapusBmnKsl.dataTerpilih.KD_PENERBIT_SK_DTL;
                        parInp.P_NIP_PENANDATANGAN = gridSkHapusBmnKsl.dataTerpilih.NIP_PENANDATANGAN;
                        parInp.P_NM_PENANDATANGAN = gridSkHapusBmnKsl.dataTerpilih.NM_PENANDATANGAN;
                        parInp.P_NM_PENERBIT_SK = gridSkHapusBmnKsl.dataTerpilih.NM_PENERBIT_SK;
                        parInp.P_NM_PENERBIT_SK_DTL = gridSkHapusBmnKsl.dataTerpilih.NM_PENERBIT_SK_DTL;
                        parInp.P_SELECT = "D";
                        modeCrud = Convert.ToChar(parInp.P_SELECT);
                        parInp.P_SK_KEPUTUSAN = gridSkHapusBmnKsl.dataTerpilih.SK_KEPUTUSAN;
                        parInp.P_TIPE_PEMOHON = gridSkHapusBmnKsl.dataTerpilih.TIPE_PEMOHON;
                        parInp.P_URAIAN_KEPUTUSAN = gridSkHapusBmnKsl.dataTerpilih.URAIAN_KEPUTUSAN;
                        parInp.P_ID_SATKER = gridSkHapusBmnKsl.dataTerpilih.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KD_SATKER = gridSkHapusBmnKsl.dataTerpilih.KD_SATKER;
                        parInp.P_UR_SATKER = gridSkHapusBmnKsl.dataTerpilih.UR_SATKER;
                        simpanDataHapusBmnKsl = new SvcWasdalHapusCud.execute_pttClient();
                        simpanDataHapusBmnKsl.Open();
                        simpanDataHapusBmnKsl.Beginexecute(parInp, new AsyncCallback(cudRskHapusBmnKsl), "");
                    }
                    catch
                    {
                        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                    }
                }
            }
        }

        private void bbiRskHapusBmnKslDetailKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSkHapusBmnKsl.dataTerpilih != null)
            {
                if (formSkHapusBmnKslDetail == null)
                {
                    formSkHapusBmnKslDetail = new ucRskHapusBmnKslForm("A");
                    formSkHapusBmnKslDetail.toggleProgressBar = new ToggleProgressBar(fToggleProgressBar);
                }
                formSkHapusBmnKslDetail.dataTerpilih = gridSkHapusBmnKsl.dataTerpilih;
                setPanel(formSkHapusBmnKslDetail);
                formSkHapusBmnKslDetail.inisialisasiForm();
                setTombolRskForm();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void bbiRskHapusBmnKslKembaliGridKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setPanel(gridSkHapusBmnKsl);
            setTombolRskGrid();
            gridSkHapusBmnKsl.dsDataSource = dsGridRskHapusBmnKsl;
            gridSkHapusBmnKsl.displayData();
        }

        private void bbiRskHapusBmnKslRefreshKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCari = "";
            modeCari = false;
            gridSkHapusBmnKsl.teNamaKolom.Text = "";
            gridSkHapusBmnKsl.teCari.Text = "";
            gridSkHapusBmnKsl.fieldDicari = "";
            gridSkHapusBmnKsl.dataInisial = true;
            this.dataInisial = true;
            this.getInitRskHapusBmnKsl();
        }

        private void bbiRskHapusBmnKslMoreDataKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.masihAdaData == true)
            {
                this.dataInisial = false;
                this.getInitRskHapusBmnKsl();
            }
        }

        private void bbiRskHapusBmnKslKeluarKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void bbiRskHapusBmnKslKembaliKlik(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelKoorSatker.Controls.Clear();
            rpRekamSK.Visible = false;
        }

        #endregion --++ Tombol Ribbon  Hapus BMN KSL

        private void nbiRskHapusBmnKsl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perekaman SK";
            konfigApp.strSubMenu = konfigApp.namaLayanan17;
            konfigApp.kdPelayanan = "17";
            konfigApp.namaPelayanan = konfigApp.namaLayanan17;
            this.Enabled = false;
            this.inisialisasiForm();
            rpRekamSK.Visible = true;
            ribbon.SelectedPage = rpRekamSK;
            initGridSkHapusBmnKsl();
            modeCari = false;
            gridSkHapusBmnKsl.teNamaKolom.Text = "";
            gridSkHapusBmnKsl.teCari.Text = "";
            gridSkHapusBmnKsl.fieldDicari = "";
            strCari = "";
            this.dataInisial = true;
            this.getInitRskHapusBmnKsl();
        }

        #region --++ Ambil Data  Hapus BMN KSL
        private void getInitRskHapusBmnKsl()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalHapusSkSelect.InputParameters parInp = new SvcWasdalHapusSkSelect.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "y";
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND ID_KPKNL = {1} AND THN_ANG='{2}' {3}", konfigApp.kdPelayanan, konfigApp.idKpknl, konfigApp.tahunAnggaran, this.strCari);
                //parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON = {2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
            //    parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND " +
            //" (ID_USER={1} " +
            // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
            // "OR ID_SATKER = {2} " +
            // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);

                parInp.STR_WHERE = konfigApp.wherePerekamanSKnonSatker() + this.strCari;

                ambilRskHapusBmnKsl = new SvcWasdalHapusSkSelect.execute_pttClient();
                ambilRskHapusBmnKsl.Open();
                ambilRskHapusBmnKsl.Beginexecute(parInp, new AsyncCallback(getRskHapusBmnKsl), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskHapusBmnKsl(IAsyncResult result)
        {
            try
            {
                dOutRskHapusBmnKsl = ambilRskHapusBmnKsl.Endexecute(result);
                ambilRskHapusBmnKsl.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsRskHapusBmnKsl(dsRskHapusBmnKsl), dOutRskHapusBmnKsl);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskHapusBmnKsl(SvcWasdalHapusSkSelect.OutputParameters dataOut);

        private void dsRskHapusBmnKsl(SvcWasdalHapusSkSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_READ_WASDAL_HAPUS.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_READ_WASDAL_HAPUS[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiRskMoreData.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiRskMoreData.Enabled = false;
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
            }

            if (dataInisial == true)
            {
                dsGridRskHapusBmnKsl = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_READ_WASDAL_HAPUS[i].IS_TB = (dataOut.SF_READ_WASDAL_HAPUS[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRskHapusBmnKsl.Add(dataOut.SF_READ_WASDAL_HAPUS[i]);
            }
            gridSkHapusBmnKsl.labelTotData.Text = "";
            gridSkHapusBmnKsl.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridSkHapusBmnKsl.sbCariOnline.Enabled = !modeCari;
            gridSkHapusBmnKsl.dsDataSource = dsGridRskHapusBmnKsl;
            gridSkHapusBmnKsl.displayData();
            if (modeCari == true)
            {
                string xSatu = gridSkHapusBmnKsl.teNamaKolom.Text.Trim();
                string xDua = gridSkHapusBmnKsl.teCari.Text.Trim();
                string xTiga = gridSkHapusBmnKsl.fieldDicari;
                gridSkHapusBmnKsl.gvGridSk.ClearColumnsFilter();
                gridSkHapusBmnKsl.teNamaKolom.Text = xSatu;
                gridSkHapusBmnKsl.teCari.Text = xDua;
                gridSkHapusBmnKsl.fieldDicari = xTiga;
            }
            else
                gridSkHapusBmnKsl.gvGridSk.ClearColumnsFilter();
        }

        private void cariDataRskHapusBmnKsl(string _strWhere, bool initModeCari)
        {
            strCari = _strWhere;
            modeCari = true;
            dataInisial = initModeCari;
            getInitRskHapusBmnKsl();
        }
        #endregion Ambil  Hapus BMN KSL

        #region --++ Simpan Data  Hapus BMN KSL
        SvcWasdalHapusCud.OutputParameters dOutAmbilDataHapusBmnKsl;
        SvcWasdalHapusCud.execute_pttClient simpanDataHapusBmnKsl;

        private void simpanDataRskHapusBmnKsl(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcWasdalHapusCud.InputParameters parInp = new SvcWasdalHapusCud.InputParameters();
                parInp.P_ID_SK_WASDAL_HAPUSSpecified = true;
                parInp.P_ID_SK_WASDAL_HAPUS = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.idSkWasdal : formSkHapusBmnKslUbah.idSkWasdal);//
                parInp.P_ID_KPKNL = konfigApp.idKpknl;
                parInp.P_ID_KPKNLSpecified = (konfigApp.idKpknl == 0 ? false : true);
                parInp.P_ID_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.idPemohon : formSkHapusBmnKslUbah.idPemohon);//
                parInp.P_ID_PEMOHONSpecified = true;
                parInp.P_ID_SATKER = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.idPemohon : formSkHapusBmnKslUbah.idPemohon);//
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_SATKER = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teKodePemohon.Text : formSkHapusBmnKslUbah.teKodePemohon.Text);//
                parInp.P_UR_SATKER = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teNamaPemohon.Text : formSkHapusBmnKslUbah.teNamaPemohon.Text);//


                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_IS_TB = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.rgJenisAset.EditValue.ToString() : formSkHapusBmnKslUbah.rgJenisAset.EditValue.ToString());
                parInp.P_JABATAN_TTD = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teJabatan.Text.Trim() : formSkHapusBmnKslUbah.teJabatan.Text.Trim());
                parInp.P_KD_PELAYANAN = konfigApp.kdPelayanan;
                parInp.P_KD_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.lePenerbitSk.EditValue.ToString() : formSkHapusBmnKslUbah.lePenerbitSk.EditValue.ToString());
                parInp.P_KD_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.kodePenerbitSkDetail : formSkHapusBmnKslUbah.kodePenerbitSkDetail);
                parInp.P_NILAI_PENETAPAN = ((_mode == "C" || _mode == "CU") ? (formSkHapusBmnKslTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKslTambah.teNilaiPenetapan.Text)) : (formSkHapusBmnKslUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKslUbah.teNilaiPenetapan.Text)));
                parInp.P_NILAI_PENETAPANSpecified = true;
                parInp.P_NIP_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teNipPenandaTangan.Text : formSkHapusBmnKslUbah.teNipPenandaTangan.Text);
                parInp.P_NM_PENANDATANGAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teNamaPenandaTangan.Text : formSkHapusBmnKslUbah.teNamaPenandaTangan.Text);
                parInp.P_NM_PENERBIT_SK = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.lePenerbitSk.Text : formSkHapusBmnKslUbah.lePenerbitSk.Text);
                parInp.P_NM_PENERBIT_SK_DTL = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teNmPenerbitSkDetail.Text : formSkHapusBmnKslUbah.namaPenerbitSkDetail);
                string _penggantiChar = (_mode == "CU" ? "U" : _mode);
                string _charSementara = (_mode == "CU" ? "Z" : _mode);
                parInp.P_SELECT = _penggantiChar;
                modeCrud = Convert.ToChar(_charSementara);
                parInp.P_SK_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teNomorSk.Text.Trim() : formSkHapusBmnKslUbah.teNomorSk.Text.Trim());
                parInp.P_TGL_CREATED = ((_mode == "C" || _mode == "CU") ? konfigApp.DateToString(DateTime.Now) : null);
                parInp.P_TGL_SK = konfigApp.DateToString(Convert.ToDateTime(((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teTanggalSk.EditValue : formSkHapusBmnKslUbah.teTanggalSk.EditValue)));
                parInp.P_TIPE_PEMOHON = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teJenisPemohon.Text : formSkHapusBmnKslUbah.teJenisPemohon.Text);
                parInp.P_TIPE_PENGELOLA = konfigApp.tipePengelola;
                parInp.P_URAIAN_KEPUTUSAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teUraianKeputusan.Text : formSkHapusBmnKslUbah.teUraianKeputusan.Text);
                parInp.P_ALASAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teAlasan.Text : formSkHapusBmnKslUbah.teAlasan.Text);
                parInp.P_TIPE_PENGELOLA = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.tipePengelola : formSkHapusBmnKslUbah.tipePengelola);
                parInp.P_NAMA_PENGADILAN = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teNamaPengadilan.Text.Trim() : formSkHapusBmnKslUbah.teNamaPengadilan.Text.Trim());
                //parInp.P_FILE_DOK = ((_mode == "C" || _mode == "CU") ? konfigApp.FileToByteArray(formSkHapusBmnKslTambah.filePath) : konfigApp.FileToByteArray(formSkHapusBmnKslUbah.filePath));
                parInp.P_THN_ANG = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teTahunAnggaran.Text : formSkHapusBmnKslUbah.teTahunAnggaran.Text);
                parInp.P_KD_KL = ((_mode == "C" || _mode == "CU") ? formSkHapusBmnKslTambah.teKodeKl.Text : formSkHapusBmnKslUbah.teKodeKl.Text);//

                simpanDataHapusBmnKsl = new SvcWasdalHapusCud.execute_pttClient();
                simpanDataHapusBmnKsl.Open();
                simpanDataHapusBmnKsl.Beginexecute(parInp, new AsyncCallback(cudRskHapusBmnKsl), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void cudRskHapusBmnKsl(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataHapusBmnKsl = simpanDataHapusBmnKsl.Endexecute(result);
                simpanDataHapusBmnKsl.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsHapusBmnKsl(this.ubahDsHapusBmnKsl), dOutAmbilDataHapusBmnKsl);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsHapusBmnKsl(SvcWasdalHapusCud.OutputParameters dataOutHapusBmnKslCrud);

        private void ubahDsHapusBmnKsl(SvcWasdalHapusCud.OutputParameters dataOutHapusBmnKslCrud)
        {
            if (dataOutHapusBmnKslCrud.PO_RESULT == "Y")
            {
                SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS dataPenyama = new SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS();
                dataPenyama.ID_PEMOHONSpecified = true;
                dataPenyama.ID_SATKERSpecified = true;
                dataPenyama.ID_USER = konfigApp.idUser;
                dataPenyama.ID_USERSpecified = true;
                dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                dataPenyama.KUANTITAS_SKSpecified = true;
                dataPenyama.NILAI_PENETAPANSpecified = true;
                dataPenyama.NUMSpecified = true;
                dataPenyama.TGL_CREATEDSpecified = true;
                dataPenyama.TGL_SKSpecified = true;
                dataPenyama.TOT_BMNSpecified = true;
                dataPenyama.TOT_STATUSSpecified = true;
                dataPenyama.TOTAL_DATASpecified = true;
                switch (modeCrud.ToString())
                {
                    case "C":
                    case "Z":
                        dataPenyama.ALASAN = formSkHapusBmnKslTambah.teAlasan.Text;
                        dataPenyama.ID_PEMOHON = formSkHapusBmnKslTambah.idPemohon;
                        dataPenyama.ID_SATKER = formSkHapusBmnKslTambah.idPemohon;
                        dataPenyama.IS_TB = (formSkHapusBmnKslTambah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkHapusBmnKslTambah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkHapusBmnKslTambah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkHapusBmnKslTambah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkHapusBmnKslTambah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkHapusBmnKslTambah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkHapusBmnKslTambah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkHapusBmnKslTambah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKslTambah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkHapusBmnKslTambah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKslTambah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkHapusBmnKslTambah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = konfigApp.namaPelayanan;
                        dataPenyama.NM_PEMOHON = formSkHapusBmnKslTambah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkHapusBmnKslTambah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkHapusBmnKslTambah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkHapusBmnKslTambah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = "";
                        dataPenyama.NUM = (dsGridRskHapusBmnKsl == null ? 1 : dsGridRskHapusBmnKsl.Count + 1);
                        dataPenyama.SK_KEPUTUSAN = formSkHapusBmnKslTambah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = "";
                        dataPenyama.TGL_CREATED = DateTime.Now;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkHapusBmnKslTambah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkHapusBmnKslTambah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = 0;
                        dataPenyama.TOT_STATUS = null;
                        dataPenyama.TOTAL_DATA = null;
                        dataPenyama.UR_KL = formSkHapusBmnKslTambah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkHapusBmnKslTambah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkHapusBmnKslTambah.teUraianKeputusan.Text;
                        dataPenyama.NAMA_PENGADILAN = formSkHapusBmnKslTambah.teNamaPengadilan.Text;
                        dsGridRskHapusBmnKsl.Add(dataPenyama);
                        gridSkHapusBmnKsl.dataTerpilih = dataPenyama;
                        formSkHapusBmnKslTambah.gcDaftarAset.Enabled = true;
                        formSkHapusBmnKslTambah.sbTambah.Enabled = true;
                        formSkHapusBmnKslTambah.sbHapus.Enabled = true;
                        formSkHapusBmnKslTambah.rgJenisAset.Properties.ReadOnly = true;
                        formSkHapusBmnKslTambah.teNomorSk.Properties.ReadOnly = true;
                        formSkHapusBmnKslTambah.teTanggalSk.Properties.ReadOnly = true;
                        formSkHapusBmnKslTambah.teJenisPemohon.Properties.ReadOnly = true;
                        formSkHapusBmnKslTambah.sbCariPemohon.Enabled = false;
                        formSkHapusBmnKslTambah.sbRefresh.Enabled = true;
                        formSkHapusBmnKslTambah.lePenerbitSk.Properties.ReadOnly = true;
                        formSkHapusBmnKslTambah.cePilihSemua.Enabled = true;
                        formSkHapusBmnKslTambah.teKodePemohon.Properties.ReadOnly = true;
                        //formSkHapusBmnKslTambah.statusForm = "CU";
                        break;
                    case "U":
                        dataPenyama.ALASAN = formSkHapusBmnKslUbah.teAlasan.Text;
                        dataPenyama.ID_PEMOHON = formSkHapusBmnKslUbah.idPemohon;
                        dataPenyama.ID_SATKER = formSkHapusBmnKslUbah.idPemohon;
                        dataPenyama.IS_TB = (formSkHapusBmnKslUbah.rgJenisAset.SelectedIndex == 0 ? konfigApp.namaTb : konfigApp.namaNonTb);
                        dataPenyama.JABATAN_TTD = formSkHapusBmnKslUbah.teJabatan.Text;
                        dataPenyama.KD_KL = formSkHapusBmnKslUbah.teKodeKl.Text;
                        dataPenyama.KD_PEMOHON = formSkHapusBmnKslUbah.teKodePemohon.Text;
                        dataPenyama.KD_PENERBIT_SK = formSkHapusBmnKslUbah.lePenerbitSk.EditValue.ToString();
                        dataPenyama.KD_PENERBIT_SK_DTL = formSkHapusBmnKslUbah.kodePenerbitSkDetail;
                        dataPenyama.KD_SATKER = formSkHapusBmnKslUbah.teKodePemohon.Text;
                        dataPenyama.KUANTITAS_SK = formSkHapusBmnKslUbah.teKuantitas.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKslUbah.teKuantitas.Text);
                        dataPenyama.NILAI_PENETAPAN = formSkHapusBmnKslUbah.teNilaiPenetapan.Text == "" ? 0 : Convert.ToDecimal(formSkHapusBmnKslUbah.teNilaiPenetapan.Text);
                        dataPenyama.NIP_PENANDATANGAN = formSkHapusBmnKslUbah.teNipPenandaTangan.Text;
                        dataPenyama.NM_PELAYANAN = gridSkHapusBmnKsl.dataTerpilih.NM_PELAYANAN;
                        dataPenyama.NM_PEMOHON = formSkHapusBmnKslUbah.teNamaPemohon.Text;
                        dataPenyama.NM_PENANDATANGAN = formSkHapusBmnKslUbah.teNamaPenandaTangan.Text;
                        dataPenyama.NM_PENERBIT_SK = formSkHapusBmnKslUbah.lePenerbitSk.Text;
                        dataPenyama.NM_PENERBIT_SK_DTL = formSkHapusBmnKslUbah.namaPenerbitSkDetail;
                        dataPenyama.NM_PENGGUNA = gridSkHapusBmnKsl.dataTerpilih.NM_PENGGUNA;
                        dataPenyama.NUM = gridSkHapusBmnKsl.dataTerpilih.NUM;
                        dataPenyama.SK_KEPUTUSAN = formSkHapusBmnKslUbah.teNomorSk.Text;
                        dataPenyama.STATUS_BMN = gridSkHapusBmnKsl.dataTerpilih.STATUS_BMN;
                        dataPenyama.TGL_CREATED = gridSkHapusBmnKsl.dataTerpilih.TGL_CREATED;
                        dataPenyama.TGL_SK = Convert.ToDateTime(formSkHapusBmnKslUbah.teTanggalSk.Text);
                        dataPenyama.TIPE_PEMOHON = formSkHapusBmnKslUbah.teJenisPemohon.Text;
                        dataPenyama.TOT_BMN = gridSkHapusBmnKsl.dataTerpilih.TOT_BMN;
                        dataPenyama.TOT_STATUS = gridSkHapusBmnKsl.dataTerpilih.TOT_STATUS;
                        dataPenyama.TOTAL_DATA = gridSkHapusBmnKsl.dataTerpilih.TOTAL_DATA;
                        dataPenyama.UR_KL = formSkHapusBmnKslUbah.teNamaKl.Text;
                        dataPenyama.UR_SATKER = formSkHapusBmnKslUbah.teNamaPemohon.Text;
                        dataPenyama.URAIAN_KEPUTUSAN = formSkHapusBmnKslUbah.teUraianKeputusan.Text;
                        dataPenyama.NAMA_PENGADILAN = formSkHapusBmnKslUbah.teNamaPengadilan.Text;
                        int _indeksData = dsGridRskHapusBmnKsl.IndexOf(gridSkHapusBmnKsl.dataTerpilih);
                        _indeksData = (_indeksData < 0 ? 0 : _indeksData);
                        dsGridRskHapusBmnKsl.Remove(gridSkHapusBmnKsl.dataTerpilih);
                        dsGridRskHapusBmnKsl.Insert(_indeksData, dataPenyama);
                        break;
                    case "D":
                        dsGridRskHapusBmnKsl.Remove(gridSkHapusBmnKsl.dataTerpilih);
                        break;
                }
                gridSkHapusBmnKsl.dsDataSource = dsGridRskHapusBmnKsl;
                gridSkHapusBmnKsl.displayData();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data  Hapus BMN KSL

        #region --++ Refresh Grid Karena Perubahan Tindak Lanjut
        private void refreshDsGridHapusBmnKsl(string _nilaiPenetapan = "0", string _kuantitas = "0")
        {
            SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS dataPenyama = new SvcWasdalHapusSkSelect.WASDALSROW_READ_WASDAL_HAPUS();
            dataPenyama.ALASAN = gridSkHapusBmnKsl.dataTerpilih.ALASAN;
            dataPenyama.ID_PEMOHON = gridSkHapusBmnKsl.dataTerpilih.ID_PEMOHON;
            dataPenyama.ID_PEMOHONSpecified = gridSkHapusBmnKsl.dataTerpilih.ID_PEMOHONSpecified;
            dataPenyama.ID_SATKER = gridSkHapusBmnKsl.dataTerpilih.ID_SATKER;
            dataPenyama.ID_SATKERSpecified = gridSkHapusBmnKsl.dataTerpilih.ID_SATKERSpecified;
            dataPenyama.ID_USER = gridSkHapusBmnKsl.dataTerpilih.ID_USER;
            dataPenyama.ID_USERSpecified = gridSkHapusBmnKsl.dataTerpilih.ID_USERSpecified;
            dataPenyama.IS_TB = gridSkHapusBmnKsl.dataTerpilih.IS_TB;
            dataPenyama.JABATAN_TTD = gridSkHapusBmnKsl.dataTerpilih.JABATAN_TTD;
            dataPenyama.KD_KL = gridSkHapusBmnKsl.dataTerpilih.KD_KL;
            dataPenyama.KD_PELAYANAN = gridSkHapusBmnKsl.dataTerpilih.KD_PELAYANAN;
            dataPenyama.KD_PEMOHON = gridSkHapusBmnKsl.dataTerpilih.KD_PEMOHON;
            dataPenyama.KD_PENERBIT_SK = gridSkHapusBmnKsl.dataTerpilih.KD_PENERBIT_SK;
            dataPenyama.KD_PENERBIT_SK_DTL = gridSkHapusBmnKsl.dataTerpilih.KD_PENERBIT_SK_DTL;
            dataPenyama.KD_SATKER = gridSkHapusBmnKsl.dataTerpilih.KD_SATKER;
            dataPenyama.KUANTITAS_SK = Convert.ToDecimal(_kuantitas);
            dataPenyama.KUANTITAS_SKSpecified = gridSkHapusBmnKsl.dataTerpilih.KUANTITAS_SKSpecified;
            dataPenyama.NAMA_PENGADILAN = gridSkHapusBmnKsl.dataTerpilih.NAMA_PENGADILAN;
            dataPenyama.NILAI_PENETAPAN = Convert.ToDecimal(_nilaiPenetapan);
            dataPenyama.NILAI_PENETAPANSpecified = gridSkHapusBmnKsl.dataTerpilih.NILAI_PENETAPANSpecified;
            dataPenyama.NIP_PENANDATANGAN = gridSkHapusBmnKsl.dataTerpilih.NIP_PENANDATANGAN;
            dataPenyama.NM_PELAYANAN = gridSkHapusBmnKsl.dataTerpilih.NM_PELAYANAN;
            dataPenyama.NM_PEMOHON = gridSkHapusBmnKsl.dataTerpilih.NM_PEMOHON;
            dataPenyama.NM_PENANDATANGAN = gridSkHapusBmnKsl.dataTerpilih.NM_PENANDATANGAN;
            dataPenyama.NM_PENERBIT_SK = gridSkHapusBmnKsl.dataTerpilih.NM_PENERBIT_SK;
            dataPenyama.NM_PENERBIT_SK_DTL = gridSkHapusBmnKsl.dataTerpilih.NM_PENERBIT_SK_DTL;
            dataPenyama.NM_PENGGUNA = gridSkHapusBmnKsl.dataTerpilih.NM_PENGGUNA;
            dataPenyama.NUM = gridSkHapusBmnKsl.dataTerpilih.NUM;
            dataPenyama.NUMSpecified = gridSkHapusBmnKsl.dataTerpilih.NUMSpecified;
            dataPenyama.SK_KEPUTUSAN = gridSkHapusBmnKsl.dataTerpilih.SK_KEPUTUSAN;
            dataPenyama.STATUS_BMN = gridSkHapusBmnKsl.dataTerpilih.STATUS_BMN;
            dataPenyama.TGL_CREATED = gridSkHapusBmnKsl.dataTerpilih.TGL_CREATED;
            dataPenyama.TGL_CREATEDSpecified = gridSkHapusBmnKsl.dataTerpilih.TGL_CREATEDSpecified;
            dataPenyama.TGL_SK = gridSkHapusBmnKsl.dataTerpilih.TGL_SK;
            dataPenyama.TGL_SKSpecified = gridSkHapusBmnKsl.dataTerpilih.TGL_SKSpecified;
            dataPenyama.TIPE_PEMOHON = gridSkHapusBmnKsl.dataTerpilih.TIPE_PEMOHON;
            dataPenyama.TOT_BMN = gridSkHapusBmnKsl.dataTerpilih.TOT_BMN;
            dataPenyama.TOT_BMNSpecified = gridSkHapusBmnKsl.dataTerpilih.TOT_BMNSpecified;
            dataPenyama.TOT_STATUS = gridSkHapusBmnKsl.dataTerpilih.TOT_STATUS;
            dataPenyama.TOT_STATUSSpecified = gridSkHapusBmnKsl.dataTerpilih.TOT_STATUSSpecified;
            dataPenyama.TOTAL_DATA = gridSkHapusBmnKsl.dataTerpilih.TOTAL_DATA;
            dataPenyama.TOTAL_DATASpecified = gridSkHapusBmnKsl.dataTerpilih.TOTAL_DATASpecified;
            dataPenyama.UR_KL = gridSkHapusBmnKsl.dataTerpilih.UR_KL;
            dataPenyama.UR_SATKER = gridSkHapusBmnKsl.dataTerpilih.UR_SATKER;
            dataPenyama.URAIAN_KEPUTUSAN = gridSkHapusBmnKsl.dataTerpilih.URAIAN_KEPUTUSAN;
            int _indeksData = dsGridRskHapusBmnKsl.IndexOf(gridSkHapusBmnKsl.dataTerpilih);
            _indeksData = (_indeksData < 0 ? 0 : _indeksData);
            dsGridRskHapusBmnKsl.Remove(gridSkHapusBmnKsl.dataTerpilih);
            dsGridRskHapusBmnKsl.Insert(_indeksData, dataPenyama);
            gridSkHapusBmnKsl.dsDataSource = dsGridRskHapusBmnKsl;
            gridSkHapusBmnKsl.displayData();
        }
        #endregion


        #endregion Hapus BMNKSL

        #endregion

        #endregion

        #region PERBANDINGAN PNBP SIMAN DAN SPAN
        string cariPnbp = "";

        string cariPnbpLap1 = "";
        string cariPnbpLap2 = "";
        string cariPnbpLap3 = "";
        string cariPnbpLap4 = "";

        #region REKAP PNBP

        AppPengguna.KSK.SIMANSPAN.ucRekapPNBP gridRekapPNBP;
        AppPengguna.KSK.SIMANSPAN.FrmRekapPNBP formRekapPNBPTambah;
        AppPengguna.KSK.SIMANSPAN.FrmRekapPNBP formRekapPNBPUbah;
        AppPengguna.KSK.SIMANSPAN.FrmRekapPNBP formRekapPNBPDetail;
        private ArrayList dsGridRekapPNBP;
        SvcGridPnbpRekap.OutputParameters dOutGridPnbpRekap;
        SvcGridPnbpRekap.execute_pttClient ambilGridPnbpRekap;

        private void initGridRekapPNBP()
        {
            //if (gridSkBongkaran == null)
            //{
            gridRekapPNBP = new AppPengguna.KSK.SIMANSPAN.ucRekapPNBP()
            {
                //detailDataGrid = new DetailDataGrid(bbiRskBongkaranUbahKlik),
                filter = new detail(getInitGridRekapPNBP)
            };
            //}
            setEventTombolGridRekapPNBP();
            setPanel(gridRekapPNBP);
        }

        private void setEventTombolGridRekapPNBP()
        {
            resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlRefreshRekapPNBP);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlMoreRekapPNBP);
            this.barMonWasdalExcel.ItemClick += new ItemClickEventHandler(this.mWlExportRekapPNBP);
            this.bbiMWasdalPrint.Visibility = BarItemVisibility.Never;
            this.bbiMWasdalRefresh.Visibility = BarItemVisibility.Always;
            this.bbiMWasdalMore.Visibility = BarItemVisibility.Always;
        }

        public void detailDataRekapPNBP()
        {
            if (gridRekapPNBP.dataTerpilih != null)
            {
                if (formRekapPNBPUbah == null)
                {
                    formRekapPNBPUbah = new AppPengguna.KSK.SIMANSPAN.FrmRekapPNBP("U")
                    {
                        toggleProgressBar = new ToggleProgressBar(fToggleProgressBar),
                        simpanDataRskPspBmn = new SimpanDataRsk(simpanDataRekapPNBP)
                    };
                }
                formRekapPNBPUbah.dataTerpilih = gridRekapPNBP.dataTerpilih;
                formRekapPNBPUbah.ShowDialog();
            }
            else MessageBox.Show(konfigApp.teksPilihanKosong, konfigApp.judulKonfirmasi);
        }

        private void nbiPNBPRekap_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            cariPnbp = "";
            konfigApp.strMenu = "Perbandingan Data PNBP SIMAN dan SPAN";
            konfigApp.strSubMenu = "Data Rekap PNBP";
            this.Enabled = false;
            this.inisialisasiForm();
            rpMonitoringWasdal.Visible = true;
            ribbon.SelectedPage = rpMonitoringWasdal;
            initGridRekapPNBP();
            this.dataInisial = true;
            this.getInitGridRekapPNBP();
        }

        #region Tombol Rekap PNBP
        private void mWlRefreshRekapPNBP(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = true;
            this.getInitGridRekapPNBP();
        }

        private void mWlMoreRekapPNBP(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitGridRekapPNBP();
        }

        private void mWlExportRekapPNBP(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls, *.xlsx";
            DialogResult dr = sfd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                gridRekapPNBP.gridView1.BestFitColumns();
                gridRekapPNBP.gridControl1.ExportToXlsx(sfd.FileName);
            }

        }

        #endregion Tombol Rekap PNBP

        #region --++ Ambil Data Bongkaran
        private void getInitGridRekapPNBP(string cari = null)
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcGridPnbpRekap.InputParameters parInp = new SvcGridPnbpRekap.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = String.Format("ID_KORWIL = {0}", konfigApp.idKorwil);
                // parInp.STR_WHERE = String.Format("KD_PELAYANAN = '{0}' AND (ID_USER={1} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON={2})) AND (ID_SATKER = {2} OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
                //    parInp.str_where = String.Format("KD_PELAYANAN = '{0}' AND " +
                //" (ID_USER={1} " +
                // "OR (TIPE_PEMOHON = 'SATKER' AND ID_PEMOHON={2}) " +
                // "OR ID_SATKER = {2} " +
                // "OR ID_SATKER_PARENT= {2}) {3}", konfigApp.kdPelayanan, konfigApp.idUser, konfigApp.idSatker, this.strCari);
                ambilGridPnbpRekap = new SvcGridPnbpRekap.execute_pttClient();
                ambilGridPnbpRekap.Open();
                ambilGridPnbpRekap.Beginexecute(parInp, new AsyncCallback(getRekapPNBP), "");
                //ambilRskBongkaran.Beginexecute(parInp, new AsyncCallback(getRskBongkaran), null);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRekapPNBP(IAsyncResult result)
        {
            try
            {
                dOutGridPnbpRekap = ambilGridPnbpRekap.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutGridRekapPNBP(dsOutGridRekapPNBP), dOutGridPnbpRekap);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutGridRekapPNBP(SvcGridPnbpRekap.OutputParameters dataOut);

        private void dsOutGridRekapPNBP(SvcGridPnbpRekap.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_GRID_REKAP_PNBP_SPAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_GRID_REKAP_PNBP_SPAN[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiMWasdalMore.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiMWasdalMore.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridRekapPNBP = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                //dataOut.SF_GRID_REKAP_PNBP_SPAN[i].IS_TB = (dataOut.SF_GRID_REKAP_PNBP_SPAN[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                dsGridRekapPNBP.Add(dataOut.SF_GRID_REKAP_PNBP_SPAN[i]);
            }
            gridRekapPNBP.labelTotData.Text = "";
            gridRekapPNBP.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridRekapPNBP.dsDataSource = dsGridRekapPNBP;
            gridRekapPNBP.displayData();
            gridRekapPNBP.gridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #region --++ Simpan Data RekapPNBP
        SvcUpdateKetRekapPNBP.OutputParameters dOutAmbilDataRekapPNBP;
        SvcUpdateKetRekapPNBP.execute_pttClient ambilDataRekapPNBP;

        private void simpanDataRekapPNBP(string _mode)
        {
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcUpdateKetRekapPNBP.InputParameters parInp = new SvcUpdateKetRekapPNBP.InputParameters();
                parInp.P_ID_SATKER = gridRekapPNBP.dataTerpilih.ID_SATKER;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KET = ((_mode == "C" || _mode == "CU") ? formRekapPNBPTambah.teKeterangan.Text : formRekapPNBPUbah.teKeterangan.Text).ToString();
                parInp.P_PERIODE = gridRekapPNBP.dataTerpilih.PERIODE;

                ambilDataRekapPNBP = new SvcUpdateKetRekapPNBP.execute_pttClient();
                ambilDataRekapPNBP.Open();
                ambilDataRekapPNBP.Beginexecute(parInp, new AsyncCallback(cudRekapPNBP), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void cudRekapPNBP(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataRekapPNBP = ambilDataRekapPNBP.Endexecute(result);
                ambilDataRekapPNBP.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new UbahDsRekapPNBP(this.ubahDsRekapPNBP), dOutAmbilDataRekapPNBP);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U') || (this.modeCrud == 'Z'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsRekapPNBP(SvcUpdateKetRekapPNBP.OutputParameters dataOutBongkaranCrud);

        private void ubahDsRekapPNBP(SvcUpdateKetRekapPNBP.OutputParameters dataOutBongkaranCrud)
        {
            if (dataOutBongkaranCrud.PO_RESULT == "Y")
            {
                getInitGridRekapPNBP();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data RekapPNBP



        #endregion REKAP PNBP

        #region REKAP PNBP SPAN

        AppPengguna.KSK.SIMANSPAN.ucPNBPSPAN gridRekapPNBPSpan;
        private ArrayList dsGridPNBPSpan;
        SvcGridPnbpSpan.OutputParameters dOutGridPnbpSpan;
        SvcGridPnbpSpan.execute_pttClient ambilGridPnbpSpan;

        private void initGridPNBPSPAN()
        {
            gridRekapPNBPSpan = new AppPengguna.KSK.SIMANSPAN.ucPNBPSPAN();
            setEventTombolGridRekapPNBPSPAN();
            setPanel(gridRekapPNBPSpan);
        }

        private void setEventTombolGridRekapPNBPSPAN()
        {
            resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlRefreshRekapPNBPSpan);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlMoreRekapPNBPSpan);
            this.barMonWasdalExcel.ItemClick += new ItemClickEventHandler(this.mWlExportRekapPNBPSpan);
            this.bbiMWasdalPrint.Visibility = BarItemVisibility.Never;
            this.bbiMWasdalRefresh.Visibility = BarItemVisibility.Always;
            this.bbiMWasdalMore.Visibility = BarItemVisibility.Always;
        }

        private void nbiPNBPSPAN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perbandingan Data PNBP SIMAN dan SPAN";
            konfigApp.strSubMenu = "Data Rekap PNBP SPAN";
            this.Enabled = false;
            this.inisialisasiForm();
            rpMonitoringWasdal.Visible = true;
            ribbon.SelectedPage = rpMonitoringWasdal;
            initGridPNBPSPAN();
            this.dataInisial = true;
            this.getInitGridRekapPNBPSPAN();
        }

        #region Tombol Rekap SPAN
        private void mWlRefreshRekapPNBPSpan(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = true;
            this.getInitGridRekapPNBP();
        }

        private void mWlMoreRekapPNBPSpan(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitGridRekapPNBPSPAN();
        }

        private void mWlExportRekapPNBPSpan(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls, *.xlsx";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                gridRekapPNBPSpan.gridView1.BestFitColumns();
                gridRekapPNBPSpan.gridControl1.ExportToXlsx(sfd.FileName);
            }

        }

        #endregion Tombol Rekap PNBP

        #region --++ Ambil Data rekap SPAN
        private void getInitGridRekapPNBPSPAN()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcGridPnbpSpan.InputParameters parInp = new SvcGridPnbpSpan.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = String.Format("FLAG = 'N' AND ID_KORWIL = {0}", konfigApp.idKorwil);
                ambilGridPnbpSpan = new SvcGridPnbpSpan.execute_pttClient();
                ambilGridPnbpSpan.Open();
                ambilGridPnbpSpan.Beginexecute(parInp, new AsyncCallback(getRekapPNBPSPAN), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRekapPNBPSPAN(IAsyncResult result)
        {
            try
            {
                dOutGridPnbpSpan = ambilGridPnbpSpan.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutGridRekapPNBPSPAN(dsOutGridRekapPNBPSPAN), dOutGridPnbpSpan);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutGridRekapPNBPSPAN(SvcGridPnbpSpan.OutputParameters dataOut);

        private void dsOutGridRekapPNBPSPAN(SvcGridPnbpSpan.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_GRID_PNBP_SPAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_GRID_PNBP_SPAN[0].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiMWasdalMore.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiMWasdalMore.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSpan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dsGridPNBPSpan.Add(dataOut.SF_GRID_PNBP_SPAN[i]);
            }
            gridRekapPNBPSpan.labelTotData.Text = "";
            string maxRow = (jmlDataKl == 0) ? "0" : dataOut.SF_GRID_PNBP_SPAN[0].TOTAL_DATA.ToString();
            gridRekapPNBPSpan.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + maxRow + " data";
            gridRekapPNBPSpan.dsDataSource = dsGridPNBPSpan;
            gridRekapPNBPSpan.displayData();
            gridRekapPNBPSpan.gridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #endregion REKAP PNBP SPAN

        #region REKAP PNBP SIMAN

        AppPengguna.KSK.SIMANSPAN.ucPNBPSIMAN gridRekapPNBPSiman;
        private ArrayList dsGridPNBPSiman;
        SvcGridPnbpSiman.OutputParameters dOutGridPnbpSiman;
        SvcGridPnbpSiman.execute_pttClient ambilGridPnbpSiman;

        private void initGridPNBPSIMAN()
        {
            gridRekapPNBPSiman = new AppPengguna.KSK.SIMANSPAN.ucPNBPSIMAN();
            setEventTombolGridRekapPNBPSIMAN();
            setPanel(gridRekapPNBPSiman);
        }

        private void setEventTombolGridRekapPNBPSIMAN()
        {
            resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlRefreshRekapPNBPSiman);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlMoreRekapPNBPSiman);
            this.barMonWasdalExcel.ItemClick += new ItemClickEventHandler(this.mWlExportRekapPNBPSiman);
            this.bbiMWasdalPrint.Visibility = BarItemVisibility.Never;
            this.bbiMWasdalRefresh.Visibility = BarItemVisibility.Always;
            this.bbiMWasdalMore.Visibility = BarItemVisibility.Always;
        }

        private void nbiPNBPSIMAN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perbandingan Data PNBP SIMAN dan SPAN";
            konfigApp.strSubMenu = "Data Rekap PNBP SIMAN";
            this.Enabled = false;
            this.inisialisasiForm();
            rpMonitoringWasdal.Visible = true;
            ribbon.SelectedPage = rpMonitoringWasdal;
            initGridPNBPSIMAN();
            this.dataInisial = true;
            this.getInitGridRekapPNBPSIMAN();
        }

        #region Tombol Rekap SPAN
        private void mWlRefreshRekapPNBPSiman(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = true;
            this.getInitGridRekapPNBPSIMAN();
        }

        private void mWlMoreRekapPNBPSiman(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitGridRekapPNBPSIMAN();
        }

        private void mWlExportRekapPNBPSiman(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls, *.xlsx";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                gridRekapPNBPSiman.gridView1.BestFitColumns();
                gridRekapPNBPSiman.gridControl1.ExportToXlsx(sfd.FileName);
            }

        }

        #endregion Tombol Rekap PNBP

        #region --++ Ambil Data Rekap SIMAN
        private void getInitGridRekapPNBPSIMAN()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcGridPnbpSiman.InputParameters parInp = new SvcGridPnbpSiman.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = String.Format("ID_KORWIL = {0}", konfigApp.idKorwil);
                ambilGridPnbpSiman = new SvcGridPnbpSiman.execute_pttClient();
                ambilGridPnbpSiman.Open();
                ambilGridPnbpSiman.Beginexecute(parInp, new AsyncCallback(getRekapPNBPSIMAN), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRekapPNBPSIMAN(IAsyncResult result)
        {
            try
            {
                dOutGridPnbpSiman = ambilGridPnbpSiman.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutGridRekapPNBPSIMAN(dsOutGridRekapPNBPSIMAN), dOutGridPnbpSiman);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutGridRekapPNBPSIMAN(SvcGridPnbpSiman.OutputParameters dataOut);

        private void dsOutGridRekapPNBPSIMAN(SvcGridPnbpSiman.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_GRID_PNBP_SIMAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_GRID_PNBP_SIMAN[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiMWasdalMore.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiMWasdalMore.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSiman = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dsGridPNBPSiman.Add(dataOut.SF_GRID_PNBP_SIMAN[i]);
            }
            gridRekapPNBPSiman.labelTotData.Text = "";
            gridRekapPNBPSiman.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridRekapPNBPSiman.dsDataSource = dsGridPNBPSiman;
            gridRekapPNBPSiman.displayData();
            gridRekapPNBPSiman.gridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #endregion REKAP PNBP SIMAN

        #region REKAP PNBP BANDING

        AppPengguna.KSK.SIMANSPAN.ucLapBandingSimanSpan gridBanding;
        private ArrayList dsGridBanding;
        SvcGridPnbpSpan.OutputParameters dOutGridBanding;
        SvcGridPnbpSpan.execute_pttClient ambilGridBanding;

        private void initGridBanding()
        {
            gridBanding = new AppPengguna.KSK.SIMANSPAN.ucLapBandingSimanSpan();
            gridBanding.refreshgc1 = new detail(getInitGridBandingSpanSimanAtas);
            gridBanding.refreshgc2 = new detail(getInitGridBandingSpanSiman);
            setEventTombolGridBanding();
            setPanel(gridBanding);
        }

        private void setEventTombolGridBanding()
        {
            resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlRefreshBanding);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlMoreBanding);
            this.barMonWasdalExcel.ItemClick += new ItemClickEventHandler(this.mWlExportBanding);
            this.bbiMWasdalPrint.Visibility = BarItemVisibility.Never;
        }

        private void nbiPerbandingan_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perbandingan Data PNBP SIMAN dan SPAN";
            konfigApp.strSubMenu = "Data Rekap PNBP SPAN";
            this.Enabled = false;
            this.inisialisasiForm();
            rpMonitoringWasdal.Visible = true;
            ribbon.SelectedPage = rpMonitoringWasdal;
            initGridBanding();
            this.dataInisial = true;
            getInitGridBandingSpanSimanAtas();
            getInitGridBandingSpanSiman();
        }

        #region Tombol Rekap SPAN
        private void mWlRefreshBanding(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadBanding(true);
        }

        private void mWlMoreBanding(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadBanding(false);
        }

        private void mWlExportBanding(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls, *.xlsx";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                gridRekapPNBPSpan.gridView1.BestFitColumns();
                gridRekapPNBPSpan.gridControl1.ExportToXlsx(sfd.FileName);
            }

        }

        #endregion Tombol Rekap PNBP

        private void loadBanding(bool initData)
        {
            //this.dataInisial = initData;

            //if (dataInisial == true)
            //{
            //    konfigApp.currentMaks = konfigApp.dataAkhir;
            //    konfigApp.currentMin = konfigApp.dataAwal;
            //}
            //else
            //{
            //    konfigApp.currentMin = konfigApp.currentMaks + 1;
            //    konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataAkhir;
            //}

            //getInitGridBandingSpanSimanAtas();
            //getInitGridBandingSpanSiman();
        }

        #region --++ Ambil Data rekap SPAN
        private void getInitGridBanding()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcGridPnbpSpan.InputParameters parInp = new SvcGridPnbpSpan.InputParameters();
                parInp.P_COL = "";
                //if (dataInisial == true)
                //{
                //    konfigApp.currentMaks = konfigApp.dataAkhir;
                //    konfigApp.currentMin = konfigApp.dataAwal;
                //}
                //else
                //{
                //    konfigApp.currentMin = konfigApp.currentMaks + 1;
                //    konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataAkhir;
                //}
                parInp.P_MAX = konfigApp.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = konfigApp.currentMin;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = String.Format("FLAG = 'Y' AND ID_SATKER = {0}", konfigApp.idSatker);
                ambilGridPnbpSpan = new SvcGridPnbpSpan.execute_pttClient();
                ambilGridPnbpSpan.Open();
                ambilGridPnbpSpan.Beginexecute(parInp, new AsyncCallback(getRekapBanding), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRekapBanding(IAsyncResult result)
        {
            try
            {
                dOutGridBanding = ambilGridPnbpSpan.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutGridBanding(dsOutGridBanding), dOutGridBanding);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutGridBanding(SvcGridPnbpSpan.OutputParameters dataOut);

        private void dsOutGridBanding(SvcGridPnbpSpan.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_GRID_PNBP_SPAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_GRID_PNBP_SPAN[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiMWasdalMore.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                this.masihAdaData = false;
                bbiMWasdalMore.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSpan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dsGridPNBPSpan.Add(dataOut.SF_GRID_PNBP_SPAN[i]);
            }
            gridBanding.labelTotData.Text = "";
            gridBanding.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridBanding.dsDataSource = dsGridPNBPSpan;
            gridBanding.displayData();
            gridBanding.gridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #region --++ Ambil Data rekap BandingSpanSiman
        SvcGridBandingSpanSiman.OutputParameters dOutGridBandingSpanSiman;
        SvcGridBandingSpanSiman.execute_pttClient ambilGridBandingSpanSiman;
        decimal minBawah = 0, maxBawah = 0;
        private void getInitGridBandingSpanSiman(string cari = null)
        {
            this.Enabled = false;
            try
            {
                cariPnbp = cari;
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcGridBandingSpanSiman.InputParameters parInp = new SvcGridBandingSpanSiman.InputParameters();
                parInp.P_COL = "";

                if (String.IsNullOrEmpty(cari))
                {
                    dataInisial = true;
                    maxBawah = konfigApp.dataAkhir;
                    minBawah = konfigApp.dataAwal;
                }
                else
                {
                    dataInisial = false;
                    minBawah = maxBawah + 1;
                    maxBawah = maxBawah + konfigApp.dataAkhir;
                }
                parInp.P_MAX = maxBawah;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = minBawah;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
                parInp.P_COUNT = "Y";
                //parInp.STR_WHERE = String.Format("FLAG = 'N' AND KD_SATKER LIKE '%{0}%' {1}", konfigApp.kodeSatker.Substring(10, 6), (string.IsNullOrEmpty(cariPnbp) ? "" : "AND " + cariPnbp));
                parInp.STR_WHERE = String.Format("FLAG = 'N' AND KD_SATKER LIKE '%{0}%'", konfigApp.kodeSatker.Substring(10, 6));
                ambilGridBandingSpanSiman = new SvcGridBandingSpanSiman.execute_pttClient();
                ambilGridBandingSpanSiman.Open();
                ambilGridBandingSpanSiman.Beginexecute(parInp, new AsyncCallback(getRekapBandingSpanSiman), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRekapBandingSpanSiman(IAsyncResult result)
        {
            try
            {
                dOutGridBandingSpanSiman = ambilGridBandingSpanSiman.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutGridBandingSpanSiman(dsOutGridBandingSpanSiman), dOutGridBandingSpanSiman);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutGridBandingSpanSiman(SvcGridBandingSpanSiman.OutputParameters dataOut);

        private void dsOutGridBandingSpanSiman(SvcGridBandingSpanSiman.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_PERBANDINGAN_SPAN_SIMAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_PERBANDINGAN_SPAN_SIMAN[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl >= maxBawah || jmlDataKl % konfigApp.dataAkhir == 0)
            {
                gridBanding.sbMore2.Enabled = true;
                jmlCurrent = maxBawah;
            }
            else
            {
                gridBanding.sbMore2.Enabled = false;
                jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSpan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                if (konfigApp.DateToString(dataOut.SF_PERBANDINGAN_SPAN_SIMAN[i].TANGGAL) == "11/11/1000") dataOut.SF_PERBANDINGAN_SPAN_SIMAN[i].TANGGAL = null;
                dsGridPNBPSpan.Add(dataOut.SF_PERBANDINGAN_SPAN_SIMAN[i]);
            }
            gridBanding.labelTotData2.Text = "";
            gridBanding.labelTotData2.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridBanding.dsDataSource2 = dsGridPNBPSpan;
            gridBanding.displayData2();
            gridBanding.gridView2.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #region --++ Ambil Data rekap BandingSpanSimanAtas
        SvcGridPnbpSpanSimanValid.OutputParameters dOutGridBandingSpanSimanAtas;
        SvcGridPnbpSpanSimanValid.execute_pttClient ambilGridBandingSpanSimanAtas;
        decimal minAtas = 0, maxAtas = 0;
        private void getInitGridBandingSpanSimanAtas(string cari = null)
        {
            this.Enabled = false;
            try
            {
                cariPnbp = cari;
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcGridPnbpSpanSimanValid.InputParameters parInp = new SvcGridPnbpSpanSimanValid.InputParameters();
                parInp.P_COL = "";

                if (String.IsNullOrEmpty(cari))
                {
                    dataInisial = true;
                    maxAtas = konfigApp.dataAkhir;
                    minAtas = konfigApp.dataAwal;
                }
                else
                {
                    dataInisial = false;
                    minAtas = maxAtas + 1;
                    maxAtas = maxAtas + konfigApp.dataAkhir;
                }
                parInp.P_MAX = maxAtas;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = minAtas;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
                parInp.P_COUNT = "Y";
                //parInp.STR_WHERE = String.Format("FLAG = 'Y' AND KD_SATKER LIKE '%{0}%' {1}", konfigApp.kodeSatker.Substring(10, 6), (string.IsNullOrEmpty(cariPnbp) ? "" : "AND " + cariPnbp));
                parInp.STR_WHERE = String.Format("KD_SATKER LIKE '%{0}%'", konfigApp.kodeSatker.Substring(10, 6));
                ambilGridBandingSpanSimanAtas = new SvcGridPnbpSpanSimanValid.execute_pttClient();
                ambilGridBandingSpanSimanAtas.Open();
                ambilGridBandingSpanSimanAtas.Beginexecute(parInp, new AsyncCallback(getRekapBandingSpanSimanAtas), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRekapBandingSpanSimanAtas(IAsyncResult result)
        {
            try
            {
                dOutGridBandingSpanSimanAtas = ambilGridBandingSpanSimanAtas.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutGridBandingSpanSimanAtas(dsOutGridBandingSpanSimanAtas), dOutGridBandingSpanSimanAtas);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutGridBandingSpanSimanAtas(SvcGridPnbpSpanSimanValid.OutputParameters dataOut);

        private void dsOutGridBandingSpanSimanAtas(SvcGridPnbpSpanSimanValid.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_GRID_PNBP_SPAN_SIMAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_GRID_PNBP_SPAN_SIMAN[jmlDataKl - 1].TOTAL_DATA.ToString();

            if (jmlDataKl >= maxAtas || jmlDataKl % konfigApp.dataAkhir == 0)
            {
                gridBanding.sbMore1.Enabled = true;
                jmlCurrent = maxAtas;
            }
            else
            {
                gridBanding.sbMore1.Enabled = false;
                jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSpan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                if (konfigApp.DateToString(dataOut.SF_GRID_PNBP_SPAN_SIMAN[i].TANGGAL) == "11/11/1000") dataOut.SF_GRID_PNBP_SPAN_SIMAN[i].TANGGAL = null;
                dsGridPNBPSpan.Add(dataOut.SF_GRID_PNBP_SPAN_SIMAN[i]);
            }
            gridBanding.labelTotData.Text = "";
            gridBanding.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            gridBanding.dsDataSource = dsGridPNBPSpan;
            gridBanding.displayData();
            gridBanding.gridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #endregion REKAP PNBP BANDING

        #region LAPORAN PNBP SIMAN SPAN
        //parent
        AppPengguna.KSK.SIMANSPAN.ucLapSIMANSPAN ucLapSimanSpanParent;

        //tabs
        AppPengguna.KSK.SIMANSPAN.LAPSIMANSPAN.ucLapSimanDanSpan ucLapSimanDanSpan;
        AppPengguna.KSK.SIMANSPAN.LAPSIMANSPAN.ucLapSama ucLapSama;
        AppPengguna.KSK.SIMANSPAN.LAPSIMANSPAN.ucLapNonSiman ucLapNonSiman;
        AppPengguna.KSK.SIMANSPAN.LAPSIMANSPAN.ucLapNonSpan ucLapNonSpan;

        #region Tombol Rekap PNBP
        private void setEventTombolGridPNBPSimanSpan()
        {
            resetEventButtonMonWasdal();
            this.bbiMWasdalRefresh.ItemClick += new ItemClickEventHandler(this.mWlRefreshPNBPSimanSpan);
            this.bbiMWasdalMore.ItemClick += new ItemClickEventHandler(this.mWlMorePNBPSimanSpan);
            this.barMonWasdalExcel.ItemClick += new ItemClickEventHandler(this.mWlExportPNBPSimanSpan);
            this.bbiMWasdalPrint.Visibility = BarItemVisibility.Never;
            this.bbiMWasdalRefresh.Visibility = BarItemVisibility.Always;
            this.bbiMWasdalMore.Visibility = BarItemVisibility.Always;
        }

        private void mWlRefreshPNBPSimanSpan(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = true;
            switch (ucLapSimanSpanParent.xtraTabControl1.SelectedTabPageIndex.ToString())
            {
                case "0": getInitLapRekapPnbpSpanSiman(); break;
                case "1": getInitLapSama(); break;
                case "2": getInitLapPnbpBlmRekamSiman(); break;
                case "3": getInitLapPnbpBlmRekamSpan(); break;
            }

        }

        private void mWlMorePNBPSimanSpan(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            switch (ucLapSimanSpanParent.xtraTabControl1.SelectedTabPageIndex.ToString())
            {
                case "0": getInitLapRekapPnbpSpanSiman(); break;
                case "1": getInitLapSama(); break;
                case "2": getInitLapPnbpBlmRekamSiman(); break;
                case "3": getInitLapPnbpBlmRekamSpan(); break;
            }
        }

        private void mWlExportPNBPSimanSpan(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls, *.xlsx";
            DialogResult dr = sfd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                switch (ucLapSimanSpanParent.xtraTabControl1.SelectedTabPageIndex.ToString())
                {
                    case "0":
                        ucLapSimanDanSpan.advBandedGridView1.BestFitColumns();
                        ucLapSimanDanSpan.gridControl1.ExportToXlsx(sfd.FileName);
                        break;
                    case "1":
                        ucLapSama.gridView1.BestFitColumns();
                        ucLapSama.gridControl1.ExportToXlsx(sfd.FileName);
                        break;
                    case "2":
                        ucLapNonSiman.gridView1.BestFitColumns();
                        ucLapNonSiman.gridControl1.ExportToXlsx(sfd.FileName);
                        break;
                    case "3":
                        ucLapNonSpan.gridView1.BestFitColumns();
                        ucLapNonSpan.gridControl1.ExportToXlsx(sfd.FileName);
                        break;
                }

            }

        }

        #endregion Tombol Rekap PNBP

        public void perbandinganSimanSpanPanel()
        {
            ucLapSimanSpanParent = new KSK.SIMANSPAN.ucLapSIMANSPAN();

            //tab 1
            ucLapSimanDanSpan = new KSK.SIMANSPAN.LAPSIMANSPAN.ucLapSimanDanSpan();
            ucLapSimanDanSpan.Dock = DockStyle.Fill;
            ucLapSimanSpanParent.panelControl1.Controls.Clear();
            ucLapSimanSpanParent.panelControl1.Controls.Add(ucLapSimanDanSpan);

            //tab 1
            ucLapSama = new KSK.SIMANSPAN.LAPSIMANSPAN.ucLapSama();
            ucLapSama.Dock = DockStyle.Fill;
            ucLapSimanSpanParent.panelControl2.Controls.Clear();
            ucLapSimanSpanParent.panelControl2.Controls.Add(ucLapSama);

            //tab 1
            ucLapNonSiman = new KSK.SIMANSPAN.LAPSIMANSPAN.ucLapNonSiman();
            ucLapNonSiman.Dock = DockStyle.Fill;
            ucLapSimanSpanParent.panelControl3.Controls.Clear();
            ucLapSimanSpanParent.panelControl3.Controls.Add(ucLapNonSiman);

            //tab 1
            ucLapNonSpan = new KSK.SIMANSPAN.LAPSIMANSPAN.ucLapNonSpan();
            ucLapNonSpan.Dock = DockStyle.Fill;
            ucLapSimanSpanParent.panelControl4.Controls.Clear();
            ucLapSimanSpanParent.panelControl4.Controls.Add(ucLapNonSpan);

            ucLapSimanSpanParent.Dock = DockStyle.Fill;
            setPanel(ucLapSimanSpanParent);
        }

        private void nbiSIMANSPAN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            konfigApp.strMenu = "Perbandingan PNBP SIMAN dan SPAN";
            konfigApp.strSubMenu = "Perbandingan PNBP SIMAN dan SPAN";
            this.inisialisasiForm();
            rpMonitoringWasdal.Visible = true;
            ribbon.SelectedPage = rpMonitoringWasdal;
            this.dataInisial = true;

            setEventTombolGridPNBPSimanSpan();
            perbandinganSimanSpanPanel();

            getInitLapSama();
            getInitLapRekapPnbpSpanSiman();
            getInitLapPnbpBlmRekamSiman();
            getInitLapPnbpBlmRekamSpan();
        }

        #region --++ Ambil Data rekap LapRekapPnbpSpanSiman
        SvcLapRekapPnbpSpanSiman.OutputParameters dOutLapRekapPnbpSpanSiman;
        SvcLapRekapPnbpSpanSiman.execute_pttClient ambilLapRekapPnbpSpanSiman;

        private void getInitLapRekapPnbpSpanSiman()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcLapRekapPnbpSpanSiman.InputParameters parInp = new SvcLapRekapPnbpSpanSiman.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = string.Format("ID_KORWIL = {0}", konfigApp.idKorwil);
                ambilLapRekapPnbpSpanSiman = new SvcLapRekapPnbpSpanSiman.execute_pttClient();
                ambilLapRekapPnbpSpanSiman.Open();
                ambilLapRekapPnbpSpanSiman.Beginexecute(parInp, new AsyncCallback(getLapRekapPnbpSpanSiman), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getLapRekapPnbpSpanSiman(IAsyncResult result)
        {
            try
            {
                dOutLapRekapPnbpSpanSiman = ambilLapRekapPnbpSpanSiman.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutLapRekapPnbpSpanSiman(dsOutLapRekapPnbpSpanSiman), dOutLapRekapPnbpSpanSiman);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutLapRekapPnbpSpanSiman(SvcLapRekapPnbpSpanSiman.OutputParameters dataOut);

        private void dsOutLapRekapPnbpSpanSiman(SvcLapRekapPnbpSpanSiman.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_LAP_REKAP_SPAN_SIMAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_LAP_REKAP_SPAN_SIMAN[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiMWasdalMore.Enabled = true;
                jmlCurrent = konfigApp.currentMaks;
            }
            else
            {
                this.masihAdaData = false;
                bbiMWasdalMore.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSpan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dsGridPNBPSpan.Add(dataOut.SF_LAP_REKAP_SPAN_SIMAN[i]);
            }
            ucLapSimanDanSpan.labelTotData.Text = "";
            ucLapSimanDanSpan.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            ucLapSimanDanSpan.dsDataSource = dsGridPNBPSpan;
            ucLapSimanDanSpan.displayData();
            ucLapSimanDanSpan.advBandedGridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #region --++ Ambil Data rekap SPAN SIMAN sama
        SvcLapPnbpSama.OutputParameters dOutLapPnbpSama;
        SvcLapPnbpSama.execute_pttClient ambilLapPnbpSama;

        private void getInitLapSama()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcLapPnbpSama.InputParameters parInp = new SvcLapPnbpSama.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = string.Format("ID_KORWIL = {0}", konfigApp.idKorwil);
                ambilLapPnbpSama = new SvcLapPnbpSama.execute_pttClient();
                ambilLapPnbpSama.Open();
                ambilLapPnbpSama.Beginexecute(parInp, new AsyncCallback(getRekapPNBPSIMANSPAN), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRekapPNBPSIMANSPAN(IAsyncResult result)
        {
            try
            {
                dOutLapPnbpSama = ambilLapPnbpSama.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutLapPnbpSama(dsOutLapPnbpSama), dOutLapPnbpSama);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutLapPnbpSama(SvcLapPnbpSama.OutputParameters dataOut);

        private void dsOutLapPnbpSama(SvcLapPnbpSama.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_LAP_PNBP_SAMA.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_LAP_PNBP_SAMA[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiMWasdalMore.Enabled = true;
                jmlCurrent = konfigApp.currentMaks;
            }
            else
            {
                this.masihAdaData = false;
                bbiMWasdalMore.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSpan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dsGridPNBPSpan.Add(dataOut.SF_LAP_PNBP_SAMA[i]);
            }
            ucLapSama.labelTotData.Text = "";
            ucLapSama.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            ucLapSama.dsDataSource = dsGridPNBPSpan;
            ucLapSama.displayData();
            ucLapSama.gridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #region --++ Ambil Data Lap Pnbp Blm Rekam Siman
        SvcLapPnbpBlmRekamSiman.OutputParameters dOutLapPnbpBlmRekamSiman;
        SvcLapPnbpBlmRekamSiman.execute_pttClient ambilLapPnbpBlmRekamSiman;

        private void getInitLapPnbpBlmRekamSiman()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcLapPnbpBlmRekamSiman.InputParameters parInp = new SvcLapPnbpBlmRekamSiman.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = String.Format("ID_KORWIL = {0} AND STATUS = 'TIDAK ADA DI SIMAN'", konfigApp.idKorwil);
                ambilLapPnbpBlmRekamSiman = new SvcLapPnbpBlmRekamSiman.execute_pttClient();
                ambilLapPnbpBlmRekamSiman.Open();
                ambilLapPnbpBlmRekamSiman.Beginexecute(parInp, new AsyncCallback(getLapPnbpBlmRekamSiman), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getLapPnbpBlmRekamSiman(IAsyncResult result)
        {
            try
            {
                dOutLapPnbpBlmRekamSiman = ambilLapPnbpBlmRekamSiman.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutLapPnbpBlmRekamSiman(dsOutLapPnbpBlmRekamSiman), dOutLapPnbpBlmRekamSiman);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutLapPnbpBlmRekamSiman(SvcLapPnbpBlmRekamSiman.OutputParameters dataOut);

        private void dsOutLapPnbpBlmRekamSiman(SvcLapPnbpBlmRekamSiman.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_LAP_SPAN_BLMREKAM_SIMAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_LAP_SPAN_BLMREKAM_SIMAN[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiMWasdalMore.Enabled = true;
                jmlCurrent = konfigApp.currentMaks;
            }
            else
            {
                this.masihAdaData = false;
                bbiMWasdalMore.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSpan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dsGridPNBPSpan.Add(dataOut.SF_LAP_SPAN_BLMREKAM_SIMAN[i]);
            }

            ucLapNonSiman.labelTotData.Text = "";
            ucLapNonSiman.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            ucLapNonSiman.dsDataSource = dsGridPNBPSpan;
            ucLapNonSiman.displayData();
            ucLapNonSiman.gridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #region --++ Ambil Data Lap Pnbp Blm Rekam Span
        SvcLapPnbpBlmRekamSpan.OutputParameters dOutLapPnbpBlmRekamSpan;
        SvcLapPnbpBlmRekamSpan.execute_pttClient ambilLapPnbpBlmRekamSpan;

        private void getInitLapPnbpBlmRekamSpan()
        {
            this.Enabled = false;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcLapPnbpBlmRekamSpan.InputParameters parInp = new SvcLapPnbpBlmRekamSpan.InputParameters();
                parInp.P_COL = "";
                if (dataInisial == true)
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
                parInp.P_COUNT = "Y";
                parInp.STR_WHERE = String.Format("ID_KORWIL = {0} AND STATUS = 'TIDAK ADA DI SPAN'", konfigApp.idKorwil);
                ambilLapPnbpBlmRekamSpan = new SvcLapPnbpBlmRekamSpan.execute_pttClient();
                ambilLapPnbpBlmRekamSpan.Open();
                ambilLapPnbpBlmRekamSpan.Beginexecute(parInp, new AsyncCallback(getLapPnbpBlmRekamSpan), "");
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getLapPnbpBlmRekamSpan(IAsyncResult result)
        {
            try
            {
                dOutLapPnbpBlmRekamSpan = ambilLapPnbpBlmRekamSpan.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new DsOutLapPnbpBlmRekamSpan(dsOutLapPnbpBlmRekamSpan), dOutLapPnbpBlmRekamSpan);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsOutLapPnbpBlmRekamSpan(SvcLapPnbpBlmRekamSpan.OutputParameters dataOut);

        private void dsOutLapPnbpBlmRekamSpan(SvcLapPnbpBlmRekamSpan.OutputParameters dataOut)
        {
            int jmlDataKl = dataOut.SF_LAP_SIMAN_BLMREKAM_SPAN.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlDataKl == 0) ? jmlDataKl.ToString() : dataOut.SF_LAP_SIMAN_BLMREKAM_SPAN[jmlDataKl - 1].TOTAL_DATA.ToString();
            if (jmlDataKl == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                bbiMWasdalMore.Enabled = true;
                jmlCurrent = konfigApp.currentMaks;
            }
            else
            {
                this.masihAdaData = false;
                bbiMWasdalMore.Enabled = false;
                if (jmlDataKl > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlDataKl;
            }

            if (dataInisial == true)
            {
                dsGridPNBPSpan = new ArrayList();
            }
            for (int i = 0; i < jmlDataKl; i++)
            {
                dsGridPNBPSpan.Add(dataOut.SF_LAP_SIMAN_BLMREKAM_SPAN[i]);
            }

            ucLapNonSpan.labelTotData.Text = "";
            ucLapNonSpan.labelTotData.Text = "Menampilkan " + jmlCurrent.ToString() + " dari total " + totalData + " data";
            ucLapNonSpan.dsDataSource = dsGridPNBPSpan;
            ucLapNonSpan.displayData();
            ucLapNonSpan.gridView1.BestFitColumns();
        }
        #endregion Ambil Bongkaran

        #endregion REKAP PERBANDINGAN PNBP SIMAN SPAN

        #endregion PERBANDINGAN PNBP SIMAN DAN SPAN

    }

}