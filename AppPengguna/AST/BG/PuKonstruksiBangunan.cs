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
using System.Collections.Generic;
using DevExpress.XtraBars;
namespace AppPengguna.AST.BG
{
    public delegate void SimpanKonstruksiBangunan(SvcKonstruksiBangunanCrud.InputParameters parIn);
    public partial class PuKonstruksiBangunan : Form
    {

     
        public SimpanKonstruksiBangunan simpanKonstruksiBangunan;
        public datafoto DaftarFoto = new datafoto();
      
        private FrmPUKondisi PuKondisi;
        private char modeCrud = 'A';
        Thread myThread = null;
        public decimal? ID_KBDG;
        public decimal? ID_M_KBDG_KONS_BDG;
        public string ID_JNSDOK;
        public string FilePath;
          SvcKonstruksiBangunanCrud.call_pttClient svcFasBangunanCrud = null;
          SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG DataAwal;
          private string KD_KONDISI;
          public string STATUS;
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
          
          public PuKonstruksiBangunan( string status, decimal? id_kbdg, decimal? id_m_kbdg_kons_bdg)
        {
            InitializeComponent();
            this.ID_KBDG = id_kbdg;
            this.teIdKbdg.Text = id_kbdg.ToString();
            this.ID_M_KBDG_KONS_BDG = id_m_kbdg_kons_bdg;
            this.STATUS = status;
    

        }

          public PuKonstruksiBangunan(string status, SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG selectedData)
          {
              InitializeComponent();
              this.STATUS = status;

              DataAwal = selectedData;
              this.KD_KONDISI = selectedData.KD_KONDISI;
              this.ID_KBDG = selectedData.ID_KBDG;
              this.teIdKbdg.Text = selectedData.ID_KBDG.ToString();
              this.ID_M_KBDG_KONS_BDG = selectedData.ID_M_KBDG_KONS_BDG;
              this.teKondisi.Text = selectedData.UR_KONDISI;
              this.teTgl_Inv.Text = konfigApp.DateToString(selectedData.TGL_INV);
              this.teMaterial_Atap.Text = selectedData.MATERIAL_ATAP;
              this.teMaterial_Dinding.Text = selectedData.MATRIAL_DINDING;
              this.teMaterial_Langit.Text = selectedData.MATERIAL_LANGIT;
              this.teStr_Atap.Text = selectedData.STR_ATAP;
              this.teStr_Rangka.Text = selectedData.STR_RANGKA;
              this.tePerkerasan.Text = selectedData.PERKERASAN;
              this.tePagar.Text = selectedData.PAGAR;
              this.tePelapis_Dinding_Dlm.Text = selectedData.PELAPIS_DINDIN_DLM;
              this.tePelapis_Dinding_Luar.Text = selectedData.PELAPIS_DINDIN_LR;
              this.teLantai.Text = selectedData.LANTAI;
              this.teFileName.Text = selectedData.NMFILE;
             
          }
   

        private void PuKonstruksiBangunan_Load(object sender, EventArgs e)
        {
            if (this.STATUS == "detail")
            {
                this.barSimpan.Enabled = false;
                this.btnUpload.Enabled = false;
                this.BtnJenisDokumen.Enabled = false;
               
            }
            this.barSimpan.Caption = konfigApp.labelSimpan;
          
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
                    string pesan="";
                    try 
	                {	        
                        SvcKonstruksiBangunanCrud.InputParameters parInp = new SvcKonstruksiBangunanCrud.InputParameters();
                        
                        parInp.P_ID_KBDG = this.ID_KBDG;
                        parInp.P_ID_M_KBDG_KONS_BDGSpecified = true;
                        parInp.P_ID_M_KBDG_KONS_BDG = this.ID_M_KBDG_KONS_BDG;
                        parInp.P_ID_KBDGSpecified = true;
                      
                        parInp.P_KD_KONDISI = konfigApp.StringtoNull(this.KD_KONDISI);
                        pesan = "Format tanggal.";
                        parInp.P_TGL_INV = konfigApp.DateToDb(this.teTgl_Inv.Text);
                        pesan = konfigApp.teksGagalSimpan;
                        parInp.P_LANTAI = this.teLantai.Text;
                        parInp.P_MATERIAL_ATAP = this.teMaterial_Atap.Text;
                        parInp.P_MATERIAL_LANGIT = this.teMaterial_Langit.Text;
                        parInp.P_PAGAR = this.tePagar.Text;
                        parInp.P_PELAPIS_DINDIN_DLM = this.tePelapis_Dinding_Dlm.Text;
                        parInp.P_PELAPIS_DINDIN_LR = this.tePelapis_Dinding_Luar.Text;
                        parInp.P_PERKERASAN = this.tePerkerasan.Text;
                        parInp.P_STR_ATAP = this.teStr_Atap.Text;
                        parInp.P_STR_RANGKA = this.teStr_Rangka.Text;
                        parInp.P_MATRIAL_DINDING = this.teMaterial_Dinding.Text;
                        parInp.P_NMFILE = this.teFileName.Text;
                        parInp.P_PAGAR = this.tePagar.Text;

                        if (this.STATUS == "input")
                        {
                            parInp.P_SELECT = "C";
                        }
                        else
                        {
                            parInp.P_SELECT = "U";
                        }
                        
                        simpanKonstruksiBangunan(parInp);
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
            if (this.STATUS == "edit")
            {
                this.KD_KONDISI = DataAwal.KD_KONDISI;
                this.ID_KBDG = DataAwal.ID_KBDG;
                this.teIdKbdg.Text = DataAwal.ID_KBDG.ToString();
                this.ID_M_KBDG_KONS_BDG = DataAwal.ID_M_KBDG_KONS_BDG;
                this.teKondisi.Text = DataAwal.UR_KONDISI;
                this.teTgl_Inv.Text = konfigApp.DateToString(DataAwal.TGL_INV);
                this.teMaterial_Atap.Text = DataAwal.MATERIAL_ATAP;
                this.teMaterial_Dinding.Text = DataAwal.MATRIAL_DINDING;
                this.teMaterial_Langit.Text = DataAwal.MATERIAL_LANGIT;
                this.teStr_Atap.Text = DataAwal.STR_ATAP;
                this.teStr_Rangka.Text = DataAwal.STR_RANGKA;
                this.tePerkerasan.Text = DataAwal.PERKERASAN;
                this.tePagar.Text = DataAwal.PAGAR;
                this.tePelapis_Dinding_Dlm.Text = DataAwal.PELAPIS_DINDIN_DLM;
                this.tePelapis_Dinding_Luar.Text = DataAwal.PELAPIS_DINDIN_LR;
                this.teLantai.Text = DataAwal.LANTAI;
                this.teFileName.Text = DataAwal.NMFILE;
                this.ResetFoto();
            }
            else
            {
                this.teKondisi.Text = "";
                this.teTgl_Inv.Text = "";
                this.teLantai.Text = "";
                this.teMaterial_Atap.Text = "";
                this.teMaterial_Langit.Text = "";
                this.tePagar.Text = "";
                this.tePelapis_Dinding_Dlm.Text = "";
                this.tePelapis_Dinding_Luar.Text = "";
                this.tePerkerasan.Text = "";
                this.teStr_Atap.Text = "";
                this.teStr_Rangka.Text = "";
                this.teMaterial_Dinding.Text = "";
                this.teFileName.ResetText();
                this.FilePath = null;
                this.ResetFoto();
            }
        }
        public void ResetFoto()
        {
           

        }
        private void BtnJenisDokumen_Click(object sender, EventArgs e)
        {
            PuKondisi = new FrmPUKondisi();
            PuKondisi.ambilKondisi = new AmbilKondisi(this.Set_KD_KONDISI);
            PuKondisi.ShowDialog();
        }

        private void Set_KD_KONDISI(string id, string NM_DOK)
        {
            this.KD_KONDISI = id;
            this.teKondisi.Text = NM_DOK;
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
                        teFileName.Text = fileName;
                        FilePath = dialog.FileName;
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

        private void btnResetFoto_Click(object sender, EventArgs e)
        {
            this.ResetFoto();
            FilePath = null;
        }

        #region View Foto
        //------------- GET Foto di DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------
        protected void view_Foto()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = this.ID_M_KBDG_KONS_BDG;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_M_KBDG_KONS_BDG";
                parInp.P_TABLE = "M_KBDG_KONS_BDG_FOTO";

                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient(konfigApp.SvcAsetGetDokSelect_name, konfigApp.SvcAsetGetDokSelect_address);
                svcAsetGetDokSelect.Open();
                svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getResultDok(IAsyncResult result)
        {
            try
            {
                this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
                svcAsetGetDokSelect.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowFileDok(this.showFileDok), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowFileDok(SvcAsetGetDokSelect.OutputParameters dataOut);

        public void showFileDok(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlData > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];

               // int idx = Foto.Images.Add(konfigApp.convert2bytmap(dok.ISI_FILE));
                DaftarFoto.PHOTO = dok.ISI_FILE;

            }
        }


        #endregion//ViewFoto

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