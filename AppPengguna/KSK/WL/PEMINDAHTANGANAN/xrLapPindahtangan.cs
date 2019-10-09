using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace AppPengguna.KSK.WL.PEMINDAHTANGANAN
{
    public partial class xrLapPindahtangan : DevExpress.XtraReports.UI.XtraReport
    {
        SvcSatkerSelect.dsRSatkerSelect_pttClient svcSatker;
        SvcSatkerSelect.OutputParameters outputSatker;
        string koneksiSqlServer = String.Format(@"Data Source=localhost\SQLBPSIMAN;Initial Catalog=LOCALBPSIMAN;User Id=sa;Password=SHTbpD*1");
        SqlConnection connection = null;
        public xrLapPindahtangan()
        {
            
            InitializeComponent();
        }

        private void xrLapWasdalBMN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //getInitWasdalBMN();
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SvcSatkerSelect.InputParameters inputSat = new SvcSatkerSelect.InputParameters();
            inputSat.P_MINSpecified = true;
            inputSat.P_MIN = 1;
            inputSat.P_MAXSpecified = true;
            inputSat.P_MAX = 1;
            inputSat.P_KD_WILESELON = null;
            inputSat.STR_WHERE = "KD_SATKER='" + konfigApp.kodeSatker + "'";
            svcSatker = new SvcSatkerSelect.dsRSatkerSelect_pttClient();
            outputSatker = svcSatker.execute(inputSat);
            int jmlData = outputSatker.SF_ROW_R_SATKER.Count();
            if (jmlData > 0)
            {
                xrKota.Text = outputSatker.SF_ROW_R_SATKER[0].NM_KAB_KOTA + "," + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
                xrJabatan.Text = outputSatker.SF_ROW_R_SATKER[0].JABATAN;
                xrNamaPenanggunaJawab.Text = outputSatker.SF_ROW_R_SATKER[0].NAMA;
                xrLabelNip.Text = outputSatker.SF_ROW_R_SATKER[0].NIP;
            }
            
        }

        private void DetailReport_BeforePrint(object sender, PrintEventArgs e)
        {
            
        }

        

        //private void PageHeader_AfterPrint(object sender, PrintEventArgs e)
        //{
        //    //e.Cancel = true;
        //}

        
    }
}
