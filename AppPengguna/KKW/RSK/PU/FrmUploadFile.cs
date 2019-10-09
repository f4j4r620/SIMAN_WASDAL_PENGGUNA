using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AppPengguna.KKW.RSK.PU
{
    public partial class FrmUploadFile : Form
    {
        
        public ToggleProgressBar toggleProgressBar;
        private SvcFileRekamSkCru.execute_pttClient fetchingFile;
        private SvcFileRekamSkCru.OutputParameters dataOutFile;
        Thread myThread;


        #region VARIABLE ==================
        private string _statusCrud;
        private string _sk_keputusan;
        private string _kd_pelayanan;
        private string _tgl_upload;
        private string _nama_file;

        public string sk_keputusan
        {
            get { return _sk_keputusan; }
            set { _sk_keputusan = value; }
        }

        public string kd_pelayanan
        {
            get { return _kd_pelayanan; }
            set { _kd_pelayanan = value; }
        }

        public string tgl_upload
        {
            get { return _tgl_upload; }
            set { _tgl_upload = value; }
        }

        public string PathFile
        {
            get { return tePathFile.Text; }
            set { tePathFile.Text = value; }
        }

        public string statusCrud
        {
            get { return _statusCrud; }
            set { _statusCrud = value; }
        }

        public string nama_file
        {
            get { return _nama_file; }
            set { _nama_file = value; }
        }

        #endregion

        #region Thread
        private void toggleProgBarPu(string kondisi)
        {
            if (kondisi == "start") this.aktifkanProgresBar();
            else this.nonAktifkanprogressBar();
        }

        private void aktifkanProgresBar()
        {
            toggleProgressBar("start");
        }

        private void nonAktifkanprogressBar()
        {
            toggleProgressBar("finish");
        }

        public delegate void AktifkanForm(string str);

        public void aktifkanForm(string str)
        {
            this.Enabled = true;
        }
        #endregion

        public FrmUploadFile()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        private void sbBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdDok = new OpenFileDialog();
            ofdDok.Title = "Pilih File";
            ofdDok.FileName = "File PDF";
            ofdDok.Filter = "(File PDF)|*.pdf";
            ofdDok.Multiselect = false;

            if (ofdDok.ShowDialog() == DialogResult.OK && ofdDok.FileName != "File PDF")
            {
                PathFile = ofdDok.FileName;
                tePathFile.Text = ofdDok.InitialDirectory + ofdDok.FileName;
                sbUpload.Enabled = true;
            }
        }

        private void FrmUploadFile_Load(object sender, EventArgs e)
        {
            tePathFile.Properties.ReadOnly = true;
            sbUpload.Enabled = false;
        }

        #region UPLOAD FILE ==================================
        private void getInitFileUpload()
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcFileRekamSkCru.InputParameters parInp = new SvcFileRekamSkCru.InputParameters();

                if (_statusCrud == "Upload")
                {
                    parInp.P_SELECT = "C";
                    tgl_upload = konfigApp.DateToDb(DateTime.Now.ToShortDateString());
                    parInp.P_FILE_DOK = konfigApp.FileToByteArray(PathFile);
                    parInp.P_SK_KEPUTUSAN = sk_keputusan;
                    parInp.P_KD_PELAYANAN = kd_pelayanan;
                    parInp.P_NM_FILE = "DOK_REKAM_SK.pdf";
                    
                }
                else
                {
                    parInp.P_SK_KEPUTUSAN = sk_keputusan;
                    parInp.P_KD_PELAYANAN = kd_pelayanan;
                    parInp.P_SELECT = "R";
                }
                parInp.P_TGL_UPLOAD = tgl_upload;
                fetchingFile = new SvcFileRekamSkCru.execute_pttClient();
                fetchingFile.Beginexecute(parInp, new AsyncCallback(getResultFileUploadRead),"");

            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm));
                MessageBox.Show(konfigApp.teksGagalSimpan);
            }
        }

        private void getResultFileUploadRead(IAsyncResult result)
        {
            try
            {
                dataOutFile = fetchingFile.Endexecute(result);
                fetchingFile.Close();
                nonAktifkanprogressBar();
                //this.Invoke(new AktifkanForm(aktifkanForm));
                this.Invoke(new dsFileUploadRead(dsfileUploadRead), dataOutFile);
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
               // this.Invoke(new AktifkanForm(aktifkanForm));
                MessageBox.Show(dataOutFile.PO_RESULT_MESSAGE +" ");
            }
        }

        private delegate void dsFileUploadRead(SvcFileRekamSkCru.OutputParameters dataout);

        private void dsfileUploadRead(SvcFileRekamSkCru.OutputParameters dataout)
        {
            if (dataout.PO_RESULT == "Y")
            {
                if (_statusCrud == "Upload")
                {
                    MessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulsukses);
                    sbView.Enabled = true;
                }
                else
                {
                    KSK.TL.PU.FrmPuViewPdf FrmPuViewPdf = new KSK.TL.PU.FrmPuViewPdf();

                    System.IO.File.WriteAllBytes("DOK_BUKTI.pdf", dataout.PO_FILE_DOK);
                    FrmPuViewPdf.display("DOK_BUKTI.pdf");
                    FrmPuViewPdf.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(dataout.PO_RESULT_MESSAGE);
            }
        }

        #endregion

        private void sbView_Click(object sender, EventArgs e)
        {
            if (PathFile.Trim() != null || PathFile.Trim() != "")
            {
                _statusCrud = "View";
                getInitFileUpload();
            }
            else
            {
                MessageBox.Show("Maaf, File tidak ditemukan");
            }
            
        }

        private void sbUpload_Click(object sender, EventArgs e)
        {
            if (PathFile.Trim() != null || PathFile.Trim() != "")
            {
                _statusCrud = "Upload";
                getInitFileUpload();

            }
            else
            {
                MessageBox.Show("File belum dipilih");
            }
        }


        
    }
}
