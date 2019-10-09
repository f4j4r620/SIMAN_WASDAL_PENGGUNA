using System.Windows.Forms;

namespace AppPengguna.KSK.WL.MONINSIDENTIL
{
    public partial class ucMainInsidentil : UserControl
    {
        // Delegate Method
        public ToggleProgressBar toggleProgressBar;
        //public SetTombolMoreData setTombolMoreData;

        // 
        int levelDetail = 0;

        public ucMainInsidentil()
        {
            InitializeComponent();
        }

        public void inisialilasiform()
        {
            ViewMonitoringInsidentil();
        }

        #region Property Method
        private void SetPanel(UserControl uc)
        {
            pcMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pcMain.Controls.Add(uc);
        }

        public void MoreData()
        {
            LoadData(false);
        }

        public void RefreshData()
        {
            LoadData(true);
        }

        private void LoadData(bool state)
        {
            ucmonitoring.LoadData(state);
        }

        #endregion
            
        #region View Menu
        ucMonitoringInsidentil ucmonitoring;
        private void ViewMonitoringInsidentil()
        {
            ucmonitoring = new ucMonitoringInsidentil();
            ucmonitoring.toggleProgressBar = new ToggleProgressBar(this.toggleProgressBar);
            //ucmonitoring.setTombolMoreData = new SetTombolMoreData(this.setTombolMoreData);
            //ucmonitoring.gotToDetail = new InvokeHandler(this.ViewMonitoringInsidentilDetail);
            SetPanel(ucmonitoring);
            ucmonitoring.LoadData(true);
        }

        private void ViewMonitoringInsidentilDetail(object o)
        {

        }

        #endregion
    }
}
