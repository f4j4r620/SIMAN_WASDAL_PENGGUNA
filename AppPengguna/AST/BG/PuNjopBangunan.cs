using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using AppPengguna.PU;
using DevExpress.XtraBars;
namespace AppPengguna.AST.BG
{
    public delegate void SimpanNjopBangunan(SvcNjopBangunanCrud.InputParameters parIn);
    public partial class PuNjopBangunan : Form
    {

        public SimpanNjopBangunan simpanNjopBangunan;
    
        private char modeCrud = 'A';
        Thread myThread = null;
        public decimal? ID_KBDG;
        public decimal? ID_M_KBDG_NJOP;
          SvcNjopBangunanCrud.call_pttClient svcNjopBangunanCrud = null;
          SvcNjopBangunanSelect.BPSIMANSROW_M_KBDG_NJOP selectedData;
          private string KD_KONDISI;
          public string STATUS;
          public string FilePath;
          public string ID_JNSDOK;

          #region Progress Bar
          public void aktifkanForm(string str)
          {
              this.Enabled = true;
              try
              {
                  this.Cursor = Cursors.Default;
              }
              catch (Exception)
              {

              }

          }
          public void nonAktifkanForm(string str)
          {
              this.Enabled = false;
              this.Cursor = Cursors.WaitCursor;
          }
          public void progBar(BarItemVisibility str)
          {

              if (this.InvokeRequired)
              {
                  ProgBar d = new ProgBar(progBar);
                  this.Invoke(d, new object[] { str });
              }
              else
              {
                  this.beMarqueeBar.Visibility = str;
              }

          }

          public void ShowProgresBar()
          {
              this.progBar(BarItemVisibility.Always);
          }
          public void ShowProgresBarDelete()
          {

          }
          #endregion

          public PuNjopBangunan( string status, decimal? id_kbdg, decimal? id_m_kbdg_njop)
        {

            InitializeComponent();
            this.ID_KBDG = id_kbdg;
            this.teIdKbdg.Text = id_kbdg.ToString();
            this.ID_M_KBDG_NJOP = id_m_kbdg_njop;
            this.STATUS = status;

        }

          public PuNjopBangunan(string status, SvcNjopBangunanSelect.BPSIMANSROW_M_KBDG_NJOP _Data)
          {
              InitializeComponent();
              this.STATUS = status;
              this.selectedData = _Data;
             
              //this.teTERAKHIR_YN.Text = selectedData.TERAKHIR_YN;
          }


          private void Init()
          {
              if (this.STATUS == "input")
              {
                  this.teKELAS.Text = "";
                  this.teLUAS.Value = 0;
                  this.teNJOP_METER.Value = 0;
                  this.teNJOP_NILAI.Value = 0;
                  this.teNOP.Text = "";
                  //this.teNPWP.Text = "";
                  this.teTAHUN.Text = "";
                  this.teTERAKHIR_YN.Text = "";
              }
              else
              {
                  this.ID_KBDG = selectedData.ID_KBDG;
                  this.teIdKbdg.Text = selectedData.ID_KBDG.ToString();
                  this.ID_M_KBDG_NJOP = selectedData.ID_M_KBDG_NJOP;
                  this.teNOP.Text = selectedData.NOP;
                  //this.teNPWP.Text = selectedData.NPWP;
                  this.teTAHUN.Text = selectedData.TAHUN;
                  this.teLUAS.Text = selectedData.LUAS.ToString();
                  this.teKELAS.Text = selectedData.KELAS;
                  this.teNJOP_METER.Text = selectedData.NJOP_METER.ToString();
                  this.teNJOP_NILAI.Text = selectedData.NJOP_NILAI.ToString();
                  this.teFileName.Text = selectedData.NMFILE;
              }
          }

        private void PuNjopBangunan_Load(object sender, EventArgs e)
        {
            this.barSimpan.Caption = konfigApp.labelSimpan;
            Init();
        }

        private bool cek_input()
        {
            return true;
            /*
            string listrik = this.teJnsDok.Text.Trim();
            if (listrik == "")
            {
                return false;
            }else{
                return true;
            }
             */ 
        }
        private void barSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                if(this.cek_input())
                {
                    string pesan = konfigApp.teksGagalSimpan;

                    try 
	                {	        
                        SvcNjopBangunanCrud.InputParameters parInp = new SvcNjopBangunanCrud.InputParameters();
                        
                        parInp.P_ID_KBDG = this.ID_KBDG;
                        parInp.P_ID_M_KBDG_NJOPSpecified = true;
                        parInp.P_ID_M_KBDG_NJOP = this.ID_M_KBDG_NJOP;
                        parInp.P_ID_KBDGSpecified = true;

                        parInp.P_KELAS = this.teKELAS.Text;
                        parInp.P_LUAS = (float?)this.teLUAS.Value;
                        parInp.P_LUASSpecified = true;
                        parInp.P_NJOP_METER = (decimal?) this.teNJOP_METER.Value;
                        parInp.P_NJOP_METERSpecified = true;
                        parInp.P_NJOP_NILAI = (decimal?)this.teNJOP_NILAI.Value;
                        parInp.P_NJOP_NILAISpecified = true;
                        parInp.P_NOP = this.teNOP.Text;
                        //parInp.P_NPWP = this.teNPWP.Text;
                        parInp.P_TAHUN = this.teTAHUN.Text;
                        parInp.P_NMFILE = this.teFileName.Text;
                      
                        
                        if (this.STATUS == "input")
                        {
                            parInp.P_SELECT = "C";
                        }
                        else
                        {
                            parInp.P_SELECT = "U";
                        }
                        
                        simpanNjopBangunan(parInp);
	                }
	                catch (Exception E)
	                {

                        MessageBox.Show(pesan.ToString(), konfigApp.judulGagalSimpan);
	                }
                }else
                {
                    MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
                }
            
        }

       

      

        private void barBatal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barBersih_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Init();
        }

       
        private void btnPilihFoto_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                string filePath = "";
                long fileSize = 0;
                string creationTime = "";

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Dispose();
                dialog.Title = "Open PDF Files";
                dialog.Filter = "PDF Files(*.pdf)|*.pdf";
                dialog.Multiselect = false;


                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    fileName = dialog.SafeFileName;
                    fileSize = new System.IO.FileInfo(dialog.FileName).Length;
                    creationTime = new System.IO.FileInfo(dialog.FileName).CreationTime.ToString();

                    if (fileSize < konfigApp.maksSizeFile)
                    {
                        FilePath = filePath;
                        teFileName.Text = fileName;
                      
                    }
                    else
                    {
                        MessageBox.Show(konfigApp.konfirmasiMaksimalFile, konfigApp.judulGagalLain);
                    }


                    Console.WriteLine(fileSize + creationTime);

                }
            }
            catch
            {
                System.Console.WriteLine("gagal");
            }
        }

        #region jenis dokumen
        private AppPengguna.PU.FrmPuJnsDok jnsDok;
        private void sbJnsDok_Click(object sender, EventArgs e)
        {
          jnsDok = new AppPengguna.PU.FrmPuJnsDok();
          jnsDok.ambilJnsDok = new AppPengguna.PU.AmbilJnsDok(this.ambilJnsDok);
          jnsDok.ShowDialog();
        }
        private void ambilJnsDok(string id, string nama)
        {
          this.ID_JNSDOK = id;
          this.teJnsDok.Text = nama;
        }
        #endregion


    }
}