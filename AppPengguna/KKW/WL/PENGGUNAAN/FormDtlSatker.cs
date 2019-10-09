using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraBars;
using AppPengguna.KKW.WL.PENGGUNAAN.DETAILSATKER;

namespace AppPengguna.KKW.WL.PENGGUNAAN
{
    public delegate void BelumPspDetail(string kode,string jns_bmn);
    public delegate void PihakLainDetail(string kode,string Label);
    public delegate void InvokeHandler(object o);

    public partial class FormDtlSatker : DevExpress.XtraEditors.XtraForm
    {
        Thread myThread;

        /// <summary>
        /// First View parameter untuk tab
        /// I.S : ketika tab sebelum dibuka/diklik(dengan kondisi sebelumnya belum pernah terbuka) maka nilai paramater = true
        /// F.S : ketika tab terbuka/terklik(dengan kondisi focus terhadap tab yang dipilih) maka nilai paramater = false
        /// Note : parameter ini mencegah untuk reload terjadi setiap kali tab dipilih
        /// </summary>
        bool firstBelumPsp = true;
        bool firstSudahPsp = true;
        bool firstIdle = true;
        bool firstPihakLain = true;
        bool firstSengekat = true;
        string kode;


        public FormDtlSatker(string label,string kode)
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            this.Label.Text = label;
            this.kode = kode;
        }

        #region Property Method

        #region Panel
        /// <summary>
        /// Digunakan untuk mengatur suatu usercontrol
        /// untuk di tempatkan suatu panel 
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="uc"></param>
        private void SetPanel(PanelControl panel, UserControl uc)
        {
            panel.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel.Controls.Add(uc);
        }

        /// <summary>
        ///  Set Panel untuk tab Sudah PSP
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelSudahPSP(UserControl uc)
        {
            SetPanel(pnlSudahPSP, uc);
        }

        /// <summary>
        /// Set Panel untuk tab Belum PSP
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelBelumPsp(UserControl uc)
        {
            SetPanel(pnlBlmPSP, uc);
        }

        /// <summary>
        /// Set Panel untuk tab IDLE
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelIdle(UserControl uc)
        {
            SetPanel(pnlTeindikasiIdle, uc);
        }

        /// <summary>
        /// Set Panel untuk tab Pihak Lain
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelPihakLain(UserControl uc)
        {
            SetPanel(pnlPihakLain, uc);
        }

        /// <summary>
        ///  Set Panel untuk tab Sengketa
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelSengekta(UserControl uc)
        {
            SetPanel(pnlSengketa, uc);
        }

        #endregion

        #region Pengaturan Thread untuk kesetabilan Aplikasi
        public void fToggleProgressBar(string kondisi)
        {
            if (kondisi == "start")
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
            }
            else
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
            }
        }

        public delegate void ProgresBar(BarItemVisibility str);

        public void progBar(BarItemVisibility str)
        {
            if (this.InvokeRequired)
            {
                ProgBar d = new ProgBar(progBar);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                this.beMarqueeBar.Visibility = str;
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
            this.Cursor = Cursors.Arrow;
        }

        public void nonAktifkanForm(string str)
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
        }
        #endregion

        #endregion

        #region Pengaturan Tab
        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            switch (e.Page.Name)
            {
                case "tabBMN":
                    ViewBMN();
                    break;
                case "tabSudahPSP":
                    SetViewSudahPSP();
                    break;
                case "tabBelumPSP":
                    SetViewBelumPSP();
                    break;
                case "tabIDLE":
                    SetViewIdle();
                    break;
                case "tabPihakLain":
                    SetViewPihakLain();
                    break;
                case "tabSengketa":
                    SetViewSengketa();
                    break;
                case "tabTidakDipergunakan":
                    ViewTidakDigunakan();
                    break;
                case "tabPihakLainNonProsedur":
                    ViewPihakLainTSP();
                    break;
            }
        }

        #endregion

        #region Tab BMN
        ucBMN ucBmn;
        ucBMNDetail ucBmnDetail;
        public void ViewBMN()
        {
            ucBmn = new ucBMN("", kode);
            ucBmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucBmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucBmn.detail = new detail1(this.ViewBMNDetail);
            SetPanel(pnlBMN, ucBmn);
            ucBmn.LoadData(true);
        }

        public void ViewBMNDetail(string kode, string idsatker)
        {
            ucBmnDetail = new ucBMNDetail("", kode, idsatker);
            ucBmnDetail.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucBmnDetail.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucBmnDetail.back = new kembali(ViewBMN);
            SetPanel(pnlBMN, ucBmnDetail);
            ucBmnDetail.LoadData(true);
        }
        #endregion Tab BMN

        #region Tab Sudah PSP
        /// <summary>
        /// Level detail untuk view
        /// detail dilakukan ketika di double klik
        /// </summary>
        int levelSudahPSP = 0;

        ucSudahPSP ucsudahpsp;

        private void SetViewSudahPSP()
        {
            if (firstSudahPsp)
            {
                firstSudahPsp = false;
                ViewSudahPSP();  
            }
        }

        private void ViewSudahPSP()
        {
            levelSudahPSP = 0;
            ucsudahpsp = new ucSudahPSP("", kode);
            ucsudahpsp.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucsudahpsp.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            SetPanelSudahPSP(ucsudahpsp);
            ucsudahpsp.LoadData(true);
        }

        #endregion

        #region Tab Belum PSP
        int levelBelumPsp = 0;
        ucBelumPSP ucbelumpsp;
        ucBelumPSPSTBNonDokPengguna ucbelumpspSTBNonDokPengguna;
        ucBelumPSPSTBNonDokPengelola ucbelumpspSTBNonDokPengelola;
        ucBelumPSPSTBDokPengguna ucbelumpspSTBDokPengguna;
        ucBelumPSPSTBDokPengelola ucbelumpspSTBDokPengelola;
        ucBelumPSPTanahPengguna ucbelumpsptanahPengguna;
        ucBelumPSPTanahPengelola ucbelumpsptanahPengelola;
        ucBelumPSPBangunanPengguna ucbelumpspbangunanPengguna;
        ucBelumPSPBangunanPengelola ucbelumpspbangunanPengelola;

        private void SetViewBelumPSP()
        {
            if (firstBelumPsp)
            {
                firstBelumPsp = false;
                ViewBelumPsp(null);
            }
        }

        private void ViewBelumPsp(object o)
        {
            levelBelumPsp = 0;
            ucbelumpsp = new ucBelumPSP("", kode);
            ucbelumpsp.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpsp.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpsp.detailSTBNonDokPengguna = new BelumPspDetail(this.ViewBelumPspStbNonDokPengguna);
            ucbelumpsp.detailSTBNonDokPengelola = new BelumPspDetail(this.ViewBelumPspStbNonDokPengelola);
            ucbelumpsp.detailSTBDokPengguna = new BelumPspDetail(this.ViewBelumPspSTBDokPengguna);
            ucbelumpsp.detailSTBDokPengelola = new BelumPspDetail(this.ViewBelumPspSTBDokPengelola);
            ucbelumpsp.detailBangunanPengguna = new BelumPspDetail(this.ViewBelumPspBangunanPengguna);
            ucbelumpsp.detailBangunanPengelola = new BelumPspDetail(this.ViewBelumPspBangunanPengelola);
            ucbelumpsp.detailTanahPengguna = new BelumPspDetail(this.ViewBelumPspTanahPengguna);
            ucbelumpsp.detailTanahPengelola = new BelumPspDetail(this.ViewBelumPspTanahPengelola);
            SetPanelBelumPsp(ucbelumpsp);
            ucbelumpsp.LoadData(true);
        }

        private void ViewBelumPspStbNonDokPengguna(string kode, string jns_bmn)
        {
            ucbelumpspSTBNonDokPengguna = new ucBelumPSPSTBNonDokPengguna(jns_bmn, kode);
            ucbelumpspSTBNonDokPengguna.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpspSTBNonDokPengguna.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpspSTBNonDokPengguna.back = new InvokeHandler(this.ViewBelumPsp);
            SetPanelBelumPsp(ucbelumpspSTBNonDokPengguna);
            ucbelumpspSTBNonDokPengguna.LoadData(true);
        }

        private void ViewBelumPspStbNonDokPengelola(string kode, string jns_bmn)
        {
            ucbelumpspSTBNonDokPengelola = new ucBelumPSPSTBNonDokPengelola(jns_bmn, kode);
            ucbelumpspSTBNonDokPengelola.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpspSTBNonDokPengelola.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpspSTBNonDokPengelola.back = new InvokeHandler(this.ViewBelumPsp);
            SetPanelBelumPsp(ucbelumpspSTBNonDokPengelola);
            ucbelumpspSTBNonDokPengelola.LoadData(true);
        }

        private void ViewBelumPspSTBDokPengguna(string kode, string jns_bmn)
        {
            ucbelumpspSTBDokPengguna = new ucBelumPSPSTBDokPengguna(jns_bmn, kode);
            ucbelumpspSTBDokPengguna.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpspSTBDokPengguna.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpspSTBDokPengguna.back = new InvokeHandler(this.ViewBelumPsp);
            SetPanelBelumPsp(ucbelumpspSTBDokPengguna);
            ucbelumpspSTBDokPengguna.LoadData(true);
        }

        private void ViewBelumPspSTBDokPengelola(string kode, string jns_bmn)
        {
            ucbelumpspSTBDokPengelola = new ucBelumPSPSTBDokPengelola(jns_bmn, kode);
            ucbelumpspSTBDokPengelola.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpspSTBDokPengelola.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpspSTBDokPengelola.back = new InvokeHandler(this.ViewBelumPsp);
            SetPanelBelumPsp(ucbelumpspSTBDokPengelola);
            ucbelumpspSTBDokPengelola.LoadData(true);
        }

        private void ViewBelumPspTanahPengguna(string kode, string jns_bmn)
        {
            ucbelumpsptanahPengguna = new ucBelumPSPTanahPengguna(jns_bmn, kode);
            ucbelumpsptanahPengguna.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpsptanahPengguna.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpsptanahPengguna.back = new InvokeHandler(this.ViewBelumPsp);
            SetPanelBelumPsp(ucbelumpsptanahPengguna);
            ucbelumpsptanahPengguna.LoadData(true);
        }

        private void ViewBelumPspTanahPengelola(string kode, string jns_bmn)
        {
            ucbelumpsptanahPengelola = new ucBelumPSPTanahPengelola(jns_bmn, kode);
            ucbelumpsptanahPengelola.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpsptanahPengelola.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpsptanahPengelola.back = new InvokeHandler(this.ViewBelumPsp);
            SetPanelBelumPsp(ucbelumpsptanahPengelola);
            ucbelumpsptanahPengelola.LoadData(true);
        }

        private void ViewBelumPspBangunanPengguna(string kode, string jns_bmn)
        {
            ucbelumpspbangunanPengguna = new ucBelumPSPBangunanPengguna(jns_bmn, kode);
            ucbelumpspbangunanPengguna.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpspbangunanPengguna.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpspbangunanPengguna.back = new InvokeHandler(this.ViewBelumPsp);
            SetPanelBelumPsp(ucbelumpspbangunanPengguna);
            ucbelumpspbangunanPengguna.LoadData(true);
        }

        private void ViewBelumPspBangunanPengelola(string kode, string jns_bmn)
        {
            ucbelumpspbangunanPengelola = new ucBelumPSPBangunanPengelola(jns_bmn, kode);
            ucbelumpspbangunanPengelola.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbelumpspbangunanPengelola.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbelumpspbangunanPengelola.back = new InvokeHandler(this.ViewBelumPsp);
            SetPanelBelumPsp(ucbelumpspbangunanPengelola);
            ucbelumpspbangunanPengelola.LoadData(true);
        }

        #endregion

        #region Tab IDLE
        ucIndikasiIdle ucindikasiidle;

        private void SetViewIdle()
        {
            if (firstIdle)
            {
                firstIdle = false;
                ViewTerindikasiIdle();
            }
        }
        private void ViewTerindikasiIdle()
        {
            ucindikasiidle = new ucIndikasiIdle("", kode);
            ucindikasiidle.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucindikasiidle.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            //ucindikasiidle.detail = new BelumPspDetail(this.ViewDetailSengketa);
            SetPanelIdle(ucindikasiidle);
            ucindikasiidle.LoadData(true);
        }
        #endregion

        #region Tab Pihak Lain
        private void SetViewPihakLain()
        {
            if (firstPihakLain)
            {
                firstPihakLain = false;
                ViewPihakLain(null);
            }
            
        }
        int levelpihaklainview = 0;
        ucPihakLain ucpihaklain;
        ucPihakLainOperasiPihakLain ucpihaklaindipergunakanorglain;
        ucPihakLainPenggunaanSementara ucpihaklainpenggunaansementara;

        private void ViewPihakLain(object o)
        {
            levelpihaklainview = 0;
            ucpihaklain = new ucPihakLain("", kode);
            ucpihaklain.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpihaklain.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpihaklain.gotoOperasiPihakLain = new PihakLainDetail(this.ViewPihakLainDipergunakanOrangLain);
            ucpihaklain.gotoPenggunaanSementara = new PihakLainDetail(this.ViewPihakLainPenggunaanSementara);
            SetPanelPihakLain(ucpihaklain);
            ucpihaklain.LoadData(true);
        }

        private void ViewPihakLainDipergunakanOrangLain(string kode, string Label)
        {
            levelpihaklainview = 1;
             
            ucpihaklaindipergunakanorglain = new ucPihakLainOperasiPihakLain(Label, kode);
            ucpihaklaindipergunakanorglain.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpihaklaindipergunakanorglain.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpihaklaindipergunakanorglain.back = new InvokeHandler(this.ViewPihakLain);
            SetPanelPihakLain(ucpihaklaindipergunakanorglain);
            ucpihaklaindipergunakanorglain.LoadData(true);
        }

        private void ViewPihakLainPenggunaanSementara(string kode, string Label)
        {
            levelpihaklainview = 2;
            ucpihaklainpenggunaansementara = new ucPihakLainPenggunaanSementara(Label, kode);
            ucpihaklainpenggunaansementara.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpihaklainpenggunaansementara.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpihaklainpenggunaansementara.back = new InvokeHandler(this.ViewPihakLain);
            SetPanelPihakLain(ucpihaklainpenggunaansementara);
            ucpihaklainpenggunaansementara.LoadData(true);
        }

        #endregion

        #region Tab Sengketa
        int levelViewSengketa = 0;

        ucSengketa ucsengketa;
        ucSengketaDetail ucsengketadetail;
        private void SetViewSengketa()
        {
            if (firstSengekat)
            {
                firstSengekat = false;
                ViewSengketa(null);
            }
        }

        private void ViewSengketa(object o)
        {
            levelViewSengketa = 0;
            ucsengketa = new ucSengketa("", kode);
            ucsengketa.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucsengketa.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucsengketa.detail = new BelumPspDetail(this.ViewDetailSengketa);
            SetPanelSengekta(ucsengketa);
            ucsengketa.LoadData(true);
        }

        private void ViewDetailSengketa(string kode, string label)
        {
            levelViewSengketa = 1;
            ucsengketadetail = new ucSengketaDetail("", kode);
            ucsengketadetail.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucsengketadetail.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucsengketadetail.back = new InvokeHandler(this.ViewSengketa);
            SetPanelSengekta(ucsengketadetail);
            ucsengketadetail.LoadData(true);
        }

        #endregion

        #region Tab Pihak lain TSP
        ucPihakLainTSP ucPihakLainTSP;
        public void ViewPihakLainTSP()
        {
            ucPihakLainTSP = new ucPihakLainTSP("", kode);
            ucPihakLainTSP.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucPihakLainTSP.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            //ucPihakLainTSP.detail = new detail(this.ViewBMNDetail);
            SetPanel(pnlPihakLainNonProsedur, ucPihakLainTSP);
            ucPihakLainTSP.LoadData(true);
        }
        #endregion Tab BMN

        #region Tab Tidak Digunakan
        ucTidakDigunakan ucTidakDigunakan;
        public void ViewTidakDigunakan()
        {
            ucTidakDigunakan = new ucTidakDigunakan("", kode);
            ucTidakDigunakan.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucTidakDigunakan.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            //ucTidakDigunakan.detail = new detail(this.ViewBMNDetail);
            SetPanel(pnlTidakDigunakan, ucTidakDigunakan);
            ucTidakDigunakan.LoadData(true);
        }
        #endregion Tab BMN

        private void FormDtlSatker_Load(object sender, System.EventArgs e)
        {
            ViewBMN();
        }

    }
}

