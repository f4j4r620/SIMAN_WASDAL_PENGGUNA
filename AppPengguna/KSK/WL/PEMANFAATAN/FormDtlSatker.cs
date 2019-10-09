using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraBars;
using AppPengguna.KSK.WL.PEMANFAATAN.DETAILSATKER;
using DevExpress.LookAndFeel;

namespace AppPengguna.KSK.WL.PEMANFAATAN
{
    public delegate void DetailSatker(string kode,string label);
    public delegate void Back();

    /// <summary>
    /// Author             : Taufiq
    /// Analis             : Benny
    /// Date               : 05/12/2016
    /// Last-Modified Date : 1. 05/12/2016
    /// </summary>
    public partial class FormDtlSatker : DevExpress.XtraEditors.XtraForm
    {
        Thread myThread;

        /// <summary>
        /// First View parameter untuk tab
        /// I.S : ketika tab sebelum dibuka/diklik(dengan kondisi sebelumnya belum pernah terbuka) maka nilai paramater = true
        /// F.S : ketika tab terbuka/terklik(dengan kondisi focus terhadap tab yang dipilih) maka nilai paramater = false
        /// Note : parameter ini mencegah untuk reload terjadi setiap kali tab dipilih
        /// </summary>
        bool firstSewa = true;
        bool firstPinjamPakai = true;
        bool firstKSP = true;
        bool firstBGS = true;
        bool firstBSG = true;
        bool firstKSPI = true;
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
        ///  Set Panel untuk tab Sewa
        /// </summary>
        /// <param name="uc"></param>
        private void SetSewa(UserControl uc)
        {
            SetPanel(pnlSewa, uc);
        }

        /// <summary>
        /// Set Panel untuk tab PinjamPakai
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelPinjamPakai(UserControl uc)
        {
            SetPanel(pnlPinjamPakai, uc);
        }

        /// <summary>
        /// Set Panel untuk tab KSP
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelKSP(UserControl uc)
        {
            SetPanel(pnlKSP, uc);
        }

        
        /// <summary>
        ///  Set Panel untuk tab BSG
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelBSG(UserControl uc)
        {
            SetPanel(pnlBSG, uc);
        }

        /// <summary>
        /// Set Panel untuk Tab KSPI
        /// </summary>
        /// <param name="uc"></param>
        private void SetPanelKSPI(UserControl uc)
        {
            SetPanel(pnlKSPI, uc);
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
                case "tabSewa":
                    SetViewSewa();
                    break;
                case "tabPinjamPakai":
                     SetViewPinjamPakai();
                    break;
                case "tabKSP":
                     SetViewKSP();
                    break;
                case "tabBSG":
                     SetViewBSG();
                    break;
                case "tabKSPI":
                     SetViewKSPI();
                    break;
            }
        }

        #endregion

        #region Tab Sewa
        /// <summary>
        /// Level detail untuk view
        /// detail dilakukan ketika di double klik
        /// </summary>
        int levelTabSewa = 0;

        ucSewa ucsewa;
        ucSewaBmn ucsewabmn;

        private void SetViewSewa()
        {
            if (firstSewa)
            {
                firstSewa = false;
                ViewSewa();  
            }
        }

        private void ViewSewa()
        {
            levelTabSewa = 0;
            ucsewa = new ucSewa("", kode);
            ucsewa.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucsewa.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucsewa.GoTo = new DetailSatker(this.ViewSewaBMN);
            SetSewa(ucsewa);
            ucsewa.LoadData(true);
        }

        private void ViewSewaBMN(string kode,string jns_bmn)
        {
            levelTabSewa = 1;
            ucsewabmn = new ucSewaBmn(jns_bmn, kode);
            ucsewabmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucsewabmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucsewabmn.backToView = new Back(this.ViewSewa);
            SetSewa(ucsewabmn);
            ucsewabmn.LoadData(true);
            
        }
        #endregion
        
        #region Tab Pinjam Pakai
        /// <summary>
        /// Level detail untuk view
        /// detail dilakukan ketika di double klik
        /// </summary>
        int levelTabPinjamPakai = 0;
        
        // object view
        ucPinjamPakai ucpinjampakai;
        ucPinjamPakaiBmn ucpinjampakaibmn;

        private void SetViewPinjamPakai()
        {
            if (firstPinjamPakai)
            {
                firstPinjamPakai = false;
                ViewPinjamPakai();
            }
        }

        private void ViewPinjamPakai()
        {
            // Menandai bahwa tab pinjam pakai berada di level detail 0 (view awal)
            levelTabPinjamPakai = 0;
            ucpinjampakai = new ucPinjamPakai("", kode);
            ucpinjampakai.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpinjampakai.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpinjampakai.GoTo = new DetailSatker(this.ViewPinjamPakaiBMN);
            SetPanelPinjamPakai(ucpinjampakai);
            ucpinjampakai.LoadData(true);

        }

        private void ViewPinjamPakaiBMN(string kode,string jns_bmn)
        {
            levelTabPinjamPakai = 1;
            ucpinjampakaibmn = new ucPinjamPakaiBmn(jns_bmn, kode);
            ucpinjampakaibmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucpinjampakaibmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucpinjampakaibmn.backToView = new Back(this.ViewPinjamPakai);
            SetPanelPinjamPakai(ucpinjampakaibmn);
            ucpinjampakaibmn.LoadData(true);
        }


        #endregion
        
        #region Tab KSP
        /// <summary>
        /// Level detail untuk view
        /// detail dilakukan ketika di double klik
        /// </summary>
        int levelTabKSP = 0;

        // object view
        ucKSP ucksp;
        ucKSPBmn uckspbmn;

        private void SetViewKSP()
        {
            if (firstKSP)
            {
                firstKSP = false;
                ViewKSP();
            }
        }

        private void ViewKSP()
        {
            // Menandai bahwa tab KSP berada di level detail 0 (view awal)
            levelTabKSP = 0;
            ucksp = new ucKSP("", kode);
            ucksp.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucksp.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucksp.GoTo = new DetailSatker(this.ViewKSPBMN);
            SetPanelKSP(ucksp);
            ucksp.LoadData(true);
        }

        private void ViewKSPBMN(string kode,string jns_bmn)
        {
            levelTabKSP = 1;
            uckspbmn = new ucKSPBmn(jns_bmn, kode);
            uckspbmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            uckspbmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            uckspbmn.backToView = new Back(this.ViewKSP);
            SetPanelKSP(uckspbmn);
            uckspbmn.LoadData(true);

        }

        #endregion

        #region Tab BSG
        /// <summary>
        /// Level detail untuk view
        /// detail dilakukan ketika di double klik
        /// </summary>
        int levelTabBSG = 0;

        // object view
        ucBSG ucbsg;
        ucBSGBmn ucbsgbmn;
        private void SetViewBSG()
        {
            if (firstBSG)
            {
                firstBSG = false;
                ViewBSG();
            }
        }

        private void ViewBSG()
        {
            // Menandai bahwa tab BGS berada di level detail 0 (view awal)
            levelTabBSG = 0;
            ucbsg = new ucBSG("", kode);
            ucbsg.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbsg.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbsg.GoTo = new DetailSatker(this.ViewBSGBMN);
            SetPanelBSG(ucbsg);
            ucbsg.LoadData(true);
        }

        private void ViewBSGBMN(string kode,string jns_bmn)
        {
            levelTabBSG = 1;
            ucbsgbmn = new ucBSGBmn(jns_bmn, kode);
            ucbsgbmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            ucbsgbmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            ucbsgbmn.backToView = new Back(this.ViewBSG);
            SetPanelBSG(ucbsgbmn);
            ucbsgbmn.LoadData(true);
        }


        #endregion

        #region Tab KSPI
        /// <summary>
        /// Level detail untuk view
        /// detail dilakukan ketika di double klik
        /// </summary>
        int levelTabKSPI = 0;

        // object view
        ucKSPI uckspi;
        ucKSPIBmn uckspibmn;

        private void SetViewKSPI()
        {
            if (firstKSPI)
            {
                firstKSPI = false;
                ViewKSPI();
            }
        }

        private void ViewKSPI()
        {
            // Menandai bahwa tab KSPI berada di level detail 0 (view awal)
            levelTabKSPI = 0;
            uckspi = new ucKSPI("", kode);
            uckspi.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            uckspi.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            SetPanelKSPI(uckspi);
            uckspi.LoadData(true);
        }

        private void ViewKSPIBMN(string kode, string jns_bmn)
        {
            levelTabKSPI = 1;
            uckspibmn = new ucKSPIBmn(jns_bmn, kode);
            uckspibmn.form = new AppPengguna.AktifkanForm(this.aktifkanForm);
            uckspibmn.toggleProgressBar = new ToggleProgressBar(this.fToggleProgressBar);
            uckspibmn.backToView = new Back(this.ViewKSPI);
            SetPanelKSPI(uckspibmn);
            uckspibmn.LoadData(true);
        }


        #endregion

        private void FormDtlSatker_Load(object sender, System.EventArgs e)
        {
            SetViewSewa();
        }

    }
}
