using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace AppPengguna.KSK.WL
{
    delegate void SetTextCallback(Int32 ix);
    delegate void SetProgressCallback(Int32 e);
    public partial class FrmProgressBar : Form
    {
        public Int32 total = 0;
        public string[] cacahBaris=null;
        SqlConnection connection = null;
        bool adaGagal = false;
        string daftarBarisGagal = "";
        int noBaris = 0;
        
        public FrmProgressBar()
        {
            InitializeComponent();
            
            progressBar1.Properties.Maximum = total;
            //backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            //backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            //backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
        }
        
        public void IncreaseProgressBar(object sender, EventArgs e)
        {
            //// Increment the value of the ProgressBar a value of one each time.
            //progressBar1.Increment(1);
            //// Display the textual value of the ProgressBar in the StatusBar control's first panel.
            //progressPanel1.Description = "Processing "+progressBar1.Position + " data";
            //// Determine if we have completed by comparing the value of the Value property to the Maximum value.
            //if (progressBar1.Position == progressBar1.Properties.Maximum)
            //{   // Stop the timer.
            //    timer1.Stop();
            //    this.Close();
            //}
        }

        private void FrmProgressBar_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void ReportProgressText(Int32 ix)
        {
            
            if (!progressPanel1.InvokeRequired)
            {
                if (ix<total)
                {
                    progressPanel1.Description = "Processing " + ix.ToString() + " data";
                    progressPanel1.Update();
                }
            }
            else {
                SetTextCallback d = new SetTextCallback(ReportProgressText);
                this.Invoke(d, new object[] { ix });
            }
            
           
        }

        private void ReportProgressBar(Int32 e)
        {
            //if (!progressBar1.InvokeRequired)
            //{
            //    progressBar1.Position = e;
            //    progressBar1.Update();
            //}
            //else
            //{
            //    SetProgressCallback s = new SetProgressCallback(ReportProgressBar);
            //    this.Invoke(s, new object[] { e });
            //}
        }

        
        private void startInsert()
        {
            int ix;
            ix = 1;
            for (int i = 0; i < cacahBaris.Count(); i++)
            {
                DataLokal dl = new DataLokal();
                
                if (cacahBaris[i] != "")
                {
                    
                    try
                    {
                        using (connection = new SqlConnection(dl.koneksiSqlServer))
                        {

                            connection.Open();
                            SqlCommand cmd = connection.CreateCommand();
                            SqlTransaction transaksi = connection.BeginTransaction();
                            cmd.Connection = connection;
                            cmd.Transaction = transaksi;
                            string perintahSql;

                            perintahSql = cacahBaris[i];

                            perintahSql = dl.CacahFile(perintahSql).Replace("\n", "").Replace("\t", "").Replace("\r", "").Trim();
                            if (perintahSql == "") return;
                            try
                            {
                                cmd.CommandText = perintahSql;//.Replace("\n","");
                                cmd.ExecuteNonQuery();
                                transaksi.Commit();

                                    ReportProgressText(ix);

                                    Thread.Sleep(10);
                                    ++ix;
                                
                            }
                            catch (Exception ex)
                            {
                                transaksi.Rollback();
                                MessageBox.Show("File Patch Gagal");
                                //perintahSql = CacahFile(perintahSql);
                                //try
                                //{
                                //    cmd.CommandText = perintahSql;
                                //    cmd.ExecuteNonQuery();
                                //    transaksi.Commit();
                                //}
                                //catch
                                //{
                                //    transaksi.Rollback();
                                //    MessageBox.Show("File Patch Gagal");
                                //}
                            }



                        }
                    }
                    catch
                    {
                        adaGagal = true;
                        daftarBarisGagal = noBaris.ToString() + "; ";

                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            startInsert();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
            progressPanel1.Description = "inserting " + e.ProgressPercentage.ToString() + " data";
            progressBar1.Position = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "Completed!";
            this.Close();
        }

        
    }
}
