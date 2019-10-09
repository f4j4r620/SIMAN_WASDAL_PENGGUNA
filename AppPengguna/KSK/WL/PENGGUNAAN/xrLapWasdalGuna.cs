using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Globalization;

namespace AppPengguna.KSK.WL.PENGGUNAAN
{
    public partial class xrLapWasdalGuna : DevExpress.XtraReports.UI.XtraReport
    {

        SvcSatkerSelect.dsRSatkerSelect_pttClient svcSatker;
        SvcSatkerSelect.OutputParameters outputSatker;
        string istb = "y";
        decimal num = 0;
        string koneksiSqlServer = String.Format(@"Data Source=localhost\SQLBPSIMAN;Initial Catalog=LOCALBPSIMAN;User Id=sa;Password=SHTbpD*1");
        SqlConnection connection = null;
        public xrLapWasdalGuna()
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

        

        private void DetailLapGuna_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand subReport = sender as DetailReportBand;
            XtraReportBase parentReport = subReport.Report;
            if (parentReport.RowCount > 0)
            {
                DetailLapGuna.Visible = true;
                string strIsTb = parentReport.GetCurrentColumnValue("IS_TB").ToString();
                DataTable dtLapGuna = new DataTable();
                using (connection = new SqlConnection(koneksiSqlServer))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = string.Format("SELECT NUM,KD_BRG,NO_ASET,UR_SSKEL,KUANTITAS,NILAI_PERLH,NO_SK,TGL_SK,NM_PENERBIT_SK_DTL,KD_STATUS KD_STATUS1,KD_STATUS KD_STATUS2,KD_STATUS KD_STATUS3, KET FROM RPT_WASDAL_GUNA_SATKER WHERE IS_TB='{0}'", strIsTb);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dtLapGuna);

                    DataTable DTLapGuna = new DataTable("DTLapGuna");
                    DTLapGuna.Columns.Add("NUM", typeof(string));
                    DTLapGuna.Columns.Add("KD_BRG", typeof(string));
                    DTLapGuna.Columns.Add("NO_ASET", typeof(string));
                    DTLapGuna.Columns.Add("UR_SSKEL", typeof(string));
                    DTLapGuna.Columns.Add("KUANTITAS", typeof(string));//4
                    DTLapGuna.Columns.Add("NILAI_PERLH", typeof(string));
                    DTLapGuna.Columns.Add("NO_SK", typeof(string));
                    DTLapGuna.Columns.Add("TGL_SK", typeof(string));
                    DTLapGuna.Columns.Add("NM_PENERBIT_SK_DTL", typeof(string));//8
                    DTLapGuna.Columns.Add("KD_STATUS1", typeof(bool));
                    DTLapGuna.Columns.Add("KD_STATUS2", typeof(bool));//10
                    DTLapGuna.Columns.Add("KD_STATUS3", typeof(bool));
                    DTLapGuna.Columns.Add("KET", typeof(string));

                    if (dtLapGuna.Rows.Count > 0)
                    {
                        char _no = 'A';
                        foreach (DataRow dr in dtLapGuna.Rows)
                        {
                            decimal nilaiPerlh = (dr[5] == "" ? 0 : Convert.ToDecimal(dr[5]));
                            string tglSk = (Convert.ToDateTime(dr[7]).ToShortDateString() == "11/11/2000" || Convert.ToDateTime(dr[7]).ToShortDateString() == "11/11/2011" || Convert.ToDateTime(dr[7]).ToShortDateString() == "11/11/1111" || Convert.ToDateTime(dr[7]).ToShortDateString() == "11/11/1000" || Convert.ToDateTime(dr[7]).ToShortDateString() == "1/1/0001" || Convert.ToDateTime(dr[7]).ToShortDateString() == "01/01/0001") ? "-" : Convert.ToDateTime(dr[7]).Date.ToString("dd/MM/yyyy") ;
                            bool gunaWasdal1 = (dr[9].ToString() == "01" || dr[9].ToString() == "02" || dr[9].ToString() == "03") ? true : false;
                            bool gunaWasdal2 = (dr[10].ToString() == "99") ? true : false;
                            bool gunaWasdal3 = (dr[11].ToString() == "04" || dr[11].ToString() == "05") ? true : false;
                            DTLapGuna.Rows.Add(Convert.ToString(dr[0]), Convert.ToString(dr[1]), Convert.ToString(dr[2]), Convert.ToString(dr[3]), Convert.ToString(dr[4]), nilaiPerlh.ToString("n0"), Convert.ToString(dr[6]),tglSk, Convert.ToString(dr[8]), gunaWasdal1, gunaWasdal2, gunaWasdal3, Convert.ToString(dr[12]));
                            _no++;
                        }
                        DetailLapGuna.DataSource = DTLapGuna;
                        DetailLapGuna.DataMember = "DTLapGuna";
                    }
                }
            }
            else DetailLapGuna.Visible = false;
        }

        

        
    }
}
