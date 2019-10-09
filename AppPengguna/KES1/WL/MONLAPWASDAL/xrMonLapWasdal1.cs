using System;
using System.Linq;
using System.Data.SqlClient;

namespace AppPengguna.KES1.WL.MONLAPWASDAL
{
    public partial class xrMonLapWasdal1 : DevExpress.XtraReports.UI.XtraReport
    {
        SvcSatkerSelect.dsRSatkerSelect_pttClient svcSatker;
        SvcSatkerSelect.OutputParameters outputSatker;
        string koneksiSqlServer = String.Format(@"Data Source=localhost\SQLBPSIMAN;Initial Catalog=LOCALBPSIMAN;User Id=sa;Password=SHTbpD*1");
        SqlConnection connection = null;
        public xrMonLapWasdal1()
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
            inputSat.STR_WHERE = "KD_ESELON1='" + konfigApp.kodeEselon1 + "'";
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

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand subReport = sender as DetailReportBand;
            //XtraReportBase parentReport = subReport.Report;
            //if (parentReport.RowCount > 0)
            //{
            //    DetailReport.Visible = true;
            //    string strNum = parentReport.GetCurrentColumnValue("NUM").ToString();
            //    DataTable dtLapManfaat = new DataTable();
            //    using (connection = new SqlConnection(koneksiSqlServer))
            //    {
            //        connection.Open();
            //        SqlCommand cmd = connection.CreateCommand();
            //        cmd.CommandText = string.Format("SELECT NUM,KD_BRG,NUP,UR_SSKEL,NM_LAYANAN, NM_PHK_LAIN,NO_SK,TGL_SK,NM_PENERBIT_SK_DTL,NILAI_PNBP,TGL_SETOR,KET FROM RPT_WASDAL_MANFAAT_SATKER WHERE NUM={0}", strNum);
            //        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            //        dataAdapter.Fill(dtLapManfaat);

            //        DataTable DTLapManfaat = new DataTable("DTLapManfaat");
            //        DTLapManfaat.Columns.Add("NUM", typeof(string));
            //        DTLapManfaat.Columns.Add("KD_BRG", typeof(string));
            //        DTLapManfaat.Columns.Add("NUP", typeof(string));
            //        DTLapManfaat.Columns.Add("UR_SSKEL", typeof(string));
            //        DTLapManfaat.Columns.Add("NM_LAYANAN", typeof(string));//5
            //        DTLapManfaat.Columns.Add("NM_PHK_LAIN", typeof(string));
            //        DTLapManfaat.Columns.Add("NO_SK", typeof(string));
            //        DTLapManfaat.Columns.Add("TGL_SK", typeof(string));
            //        DTLapManfaat.Columns.Add("NM_PENERBIT_SK_DTL", typeof(string));
            //        DTLapManfaat.Columns.Add("NILAI_PNBP", typeof(string));
            //        DTLapManfaat.Columns.Add("TGL_SETOR", typeof(string));//10
            //        DTLapManfaat.Columns.Add("KET", typeof(string));

            //        if (dtLapManfaat.Rows.Count > 0)
            //        {
            //            char _no = 'A';
            //            foreach (DataRow dr in dtLapManfaat.Rows)
            //            {
            //                DTLapManfaat.Rows.Add(Convert.ToString(dr[0]), Convert.ToString(dr[1]), Convert.ToString(dr[2]), Convert.ToString(dr[3]), Convert.ToString(dr[4]), Convert.ToString(dr[5]), Convert.ToString(dr[6]), Convert.ToString(dr[7]), Convert.ToString(dr[8]), Convert.ToString(dr[9]), Convert.ToString(dr[10]));
            //                _no++;
            //            }
            //            DetailReport.DataSource = DTLapManfaat;
            //            DetailReport.DataMember = "DTLapManfaat";
            //        }
            //    }
            //}
            //else DetailReport.Visible = false;
        }

        


        
    }
}
