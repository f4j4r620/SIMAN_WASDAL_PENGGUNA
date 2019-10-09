using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Data;
using System.Linq;

namespace AppPengguna.KKW.WL.PENGGUNAAN 
{
    public partial class xrLapWasdalBMN : DevExpress.XtraReports.UI.XtraReport
    {
        
        public xrLapWasdalBMN()
        {
            InitializeComponent();
        }

        string istb = "y";
        decimal num = 0;

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTableCell1.Text == istb)
                istb = xrTableCell1.Text;
            else
                num = 0;
        }

        private void xrTableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            num += 1;
            xrTableCell5.Text = Convert.ToString(num);
        }

        
    }
}
