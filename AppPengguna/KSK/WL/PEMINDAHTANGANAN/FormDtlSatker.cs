using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraBars;
using AppPengguna.KSK.WL.PEMINDAHTANGAN.DETAILSATKER;
using DevExpress.LookAndFeel;

namespace AppPengguna.KSK.WL.PEMINDAHTANGAN
{
    public delegate void BelumPspDetail(string kode,string jns_bmn);
    public delegate void Detail(string kode,string Label);
    public delegate void Back();

    public partial class FormDtlSatker : DevExpress.XtraEditors.XtraForm
    {
        Thread myThread;

        /// <summary>
        /// First View parameter untuk tab
        /// I.S : ketika tab sebelum dibuka/diklik(dengan kondisi sebelumnya belum pernah terbuka) maka nilai paramater = true
        /// F.S : ketika tab terbuka/terklik(dengan kondisi focus terhadap tab yang dipilih) maka nilai paramater = false
        /// Note : parameter ini mencegah untuk reload terjadi setiap kali tab dipilih
        /// </summary>
        bool firstTukarMenukar = true;
        bool firstPenjualan = true;
        bool firstHibah = true;
        bool firstPMPP = true;
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
        ///  Set Panel untuk tab Penjualan
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelPenjualan(UserControl uc)
        {
            SetPanel(pnlPenjualan, uc);
        }

        /// <summary>
        /// Set Panel untuk tab TukarMenukar
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelTukarMenukar(UserControl uc)
        {
            SetPanel(pnlTukarMenukar, uc);
        }

        /// <summary>
        /// Set Panel untuk tab Hibah
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelHibah(UserControl uc)
        {
            SetPanel(pnlHibah, uc);
        }

        /// <summary>
        /// Set Panel untuk tab PMPP
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelPMPP(UserControl uc)
        {
            SetPanel(pnlPMPP, uc);
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
                case "tabPenjualan":
                    SetViewPenjualan();
                    break;
                case "tabTukarMenukar":
                    SetViewTukarMenukar();
                    break;
                case "tabHibah":
                    SetViewHibah();
                    break;
                case "tabPMPP":
                    SetViewPMPP();
                    break;
            }
        }

        #endregion

        #region Tab Sudah PENJUALAN
        /// <summary>
        /// Level detail untuk view
        /// detail dilakukan ketika di double klik
        /// </summary>
        int levelPenjualan = 0;
        ucPenjualan ucpenjualan;
        ucPenjualanBmn ucpenjualanbmn;

        private void SetViewPenjualan()
        {
            if (firstPenjualan)
            {
                firstPenjualan = false;
                ViewPenjualan();  
            }
        }

        private void ViewPenjualan()
        {
            levelPenjualan = 0;
            ucpenjualan = new ucPenjualan("", kode);
            ucpenjualan.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpenjualan.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpenjualan.detail = new Detail(this.ViewPenjualanDetail);
            SetPanelPenjualan(ucpenjualan);
            ucpenjualan.LoadData(true);
        }

        private void ViewPenjualanDetail(string kode,string label)
        {
            levelPenjualan = 1;
            ucpenjualanbmn = new ucPenjualanBmn(label,kode);
            ucpenjualanbmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpenjualanbmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpenjualanbmn.back = new Back(this.ViewPenjualan);
            SetPanelPenjualan(ucpenjualanbmn);
            ucpenjualanbmn.LoadData(true);
        }

        #endregion

        #region Tab Tukar Menukar
        int levelTukarMenukar = 0;
        ucTukarMenukar uctukarmenukar;
        ucTukarMenukarBmn uctukarmenukarbmn;

        private void SetViewTukarMenukar()
        {
            if (firstTukarMenukar)
            {
                firstTukarMenukar = false;
                ViewTukarMenukar();
            }
        }

        private void ViewTukarMenukar()
        {
            levelTukarMenukar = 0;
            uctukarmenukar = new ucTukarMenukar("", kode);
            uctukarmenukar.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            uctukarmenukar.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            uctukarmenukar.detail = new Detail(this.ViewTukarMenukarDetail);
            SetPanelTukarMenukar(uctukarmenukar);
            uctukarmenukar.LoadData(true);
        }

        private void ViewTukarMenukarDetail(string kode, string label)
        {
            uctukarmenukarbmn = new ucTukarMenukarBmn(label, kode);
            uctukarmenukarbmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            uctukarmenukarbmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            uctukarmenukarbmn.back = new Back(this.ViewTukarMenukar);
            SetPanelTukarMenukar(uctukarmenukarbmn);
            uctukarmenukarbmn.LoadData(true);
        }
        

        #endregion

        #region Tab Hibah
        ucHibah uchibah;
        ucHibahBmn uchibahbmn;

        private void SetViewHibah()
        {
            if (firstHibah)
            {
                firstHibah = false;
                ViewHibah();
            }
        }
        private void ViewHibah()
        {
            uchibah = new ucHibah("", kode);
            uchibah.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            uchibah.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            uchibah.detail = new Detail(this.ViewHibahDetail);
            SetPanelHibah(uchibah);
            uchibah.LoadData(true);
        }

        private void ViewHibahDetail(string kode, string label) 
        {
            uchibahbmn = new ucHibahBmn(label, kode);
            uchibahbmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            uchibahbmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            uchibahbmn.back = new Back(this.ViewHibah);
            SetPanelHibah(uchibahbmn);
            uchibahbmn.LoadData(true);
        }

        #endregion

        #region Tab PMPPP
        ucPMPP ucpmpp;
        ucPMPPBmn ucpmppbmn;

        private void SetViewPMPP()
        {
            if (firstPMPP)
            {
                firstPMPP = false;
                ViewPMPP();
            }
            
        }
        int levelPMPP = 0;
       
        private void ViewPMPP()
        {
            levelPMPP = 0;
            ucpmpp = new ucPMPP("", kode);
            ucpmpp.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpmpp.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpmpp.detail = new Detail(this.ViewPMPPBmn);
            SetPanelPMPP(ucpmpp);
            ucpmpp.LoadData(true);
        }


        private void ViewPMPPBmn(string kode, string label)
        {
            ucpmppbmn = new ucPMPPBmn(label, kode);
            ucpmppbmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpmppbmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpmppbmn.back = new Back(this.ViewPMPP);
            SetPanelPMPP(ucpmppbmn);
            ucpmppbmn.LoadData(true);
        }

       

        #endregion

        private void FormDtlSatker_Load(object sender, System.EventArgs e)
        {
            SetViewPenjualan();
        }

    }
}
