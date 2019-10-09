using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Data;
using System.Linq;

namespace AppPengguna.KKW.WL.PEMANFAATAN 
{
    public partial class xrPemanfaatanBMN : DevExpress.XtraReports.UI.XtraReport
    {
        SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP wasdalSelect;
        SvcWasdalPSPBMNSelect.execute_pttClient ambilWasdal;
        SvcWasdalPSPBMNSelect.OutputParameters dataOutWasdal;

        public xrPemanfaatanBMN()
        {
            InitializeComponent();
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrlKodeKPB.Text = konfigApp.kodeKorwil;
            xrlKodeKPB.Text = konfigApp.kodeKorwil;
        }



        
    }
}
