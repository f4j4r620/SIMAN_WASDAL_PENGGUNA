using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Linq;

namespace AppPengguna.KSK.PENERTIBAN
{
    public partial class xrPenertiban : DevExpress.XtraReports.UI.XtraReport
    {
        public xrPenertiban()
        {
            InitializeComponent();
            
        }

        SvcSatkerSelect.dsRSatkerSelect_pttClient penandatangan;
        SvcSatkerSelect.OutputParameters outPenandatangan;
        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            SvcSatkerSelect.InputParameters inputsatker = new SvcSatkerSelect.InputParameters();
            inputsatker.P_MIN = 1;
            inputsatker.P_MINSpecified = true;
            inputsatker.P_MAX = 1;
            inputsatker.P_MAXSpecified = true;
            inputsatker.P_KD_WILESELON = null;
            
            inputsatker.STR_WHERE = "KD_SATKER='" + konfigApp.kodeSatker+"'";
            penandatangan = new SvcSatkerSelect.dsRSatkerSelect_pttClient();
            outPenandatangan = penandatangan.execute(inputsatker);
            int jmlData = outPenandatangan.SF_ROW_R_SATKER.Count();
            

            if (jmlData > 0)
                {
                    //xrKepala.Text = konfigApp.levelSatker;
                    xrKota.Text = outPenandatangan.SF_ROW_R_SATKER[0].NM_KAB_KOTA + "," + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year; 
                    xrNama.Text = outPenandatangan.SF_ROW_R_SATKER[0].NAMA;
                    xrNip.Text = outPenandatangan.SF_ROW_R_SATKER[0].NIP;
                    xrJabatan.Text = outPenandatangan.SF_ROW_R_SATKER[0].JABATAN;
                }
           
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrKdSatkerJudul.Text = konfigApp.kodeSatker;
            xrUrSatkerJudul.Text = konfigApp.namaSatker;
        }

        

        

       

    }
}
