using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Data;
using System.Linq;

namespace AppPengguna.KSK.WL.PENGGUNAAN
{
    public partial class xrLapWasdalBMN : DevExpress.XtraReports.UI.XtraReport
    {

        SvcSatkerSelect.dsRSatkerSelect_pttClient svcSatker;
        SvcSatkerSelect.OutputParameters outputSatker;
        string istb = "y";
        decimal num = 0;
        public xrLapWasdalBMN()
        {
            InitializeComponent();
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SvcSatkerSelect.InputParameters inputSat = new SvcSatkerSelect.InputParameters();
            inputSat.P_MINSpecified = true;
            inputSat.P_MIN = 1;
            inputSat.P_MAXSpecified = true;
            inputSat.P_MAX = 1;
            inputSat.P_KD_WILESELON = null;
            inputSat.STR_WHERE = "KD_SATKER = '" + konfigApp.kodeSatker + "'";
            svcSatker = new SvcSatkerSelect.dsRSatkerSelect_pttClient();
            outputSatker = svcSatker.execute(inputSat);
            int jmlData = outputSatker.SF_ROW_R_SATKER.Count();
            if (jmlData > 0)
            {
                xrKota.Text = outputSatker.SF_ROW_R_SATKER[0].NM_KAB_KOTA + ", " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
                xrJabatan.Text = outputSatker.SF_ROW_R_SATKER[0].JABATAN;
                xrNamaPenanggunaJawab.Text = outputSatker.SF_ROW_R_SATKER[0].NAMA;
                //xrLabelNip.Text = outputSatker.SF_ROW_R_SATKER[0].NIP.Substring(4, 18);
                xrLabelNip.Text = outputSatker.SF_ROW_R_SATKER[0].NIP;
                
            }
        }

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTableCell1.Text == istb)
            {
                istb = xrTableCell1.Text;
            }
            else
                num = 0;

            if (xrTableCell1.Text.Equals("Y") || xrTableCell1.Text.Equals("y"))
            {
                xrTableCell1.Text = "I. Tanah dan / atau bangunan";
            }
            else
            {
                xrTableCell1.Text = "II. Selain Tanah dan / atau bangunan";
            }
        }

        private void xrTableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            num += 1;
            xrTableCell5.Text = Convert.ToString(num);
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrKdSatker.Text = konfigApp.kodeSatker;
            xrUrSatker.Text = konfigApp.namaSatker;
        }

        
    }
}
