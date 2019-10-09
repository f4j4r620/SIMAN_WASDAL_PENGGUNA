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
namespace AppPengguna.AST.BG
{
    public delegate void SimpanDokBangunan(SvcDokBangunanCrud.InputParameters parIn);
    public partial class PuDokBangunan : Form
    {

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public SimpanDokBangunan simpanDokBangunan;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;

   
         private FrmPuJenisDokumenBangunan pUJenisDokumen;
        private char modeCrud = 'A';
        Thread myThread = null;
        public decimal? ID_KBDG;
        public decimal? ID_KBDG_DOK;
        public string ID_JNSDOK;
        public string FilePath;
          SvcDokBangunanCrud.call_pttClient svcFasBangunanCrud = null;

          private string KD_JNS_SERTI;
          private string KD_JNS_DOK_BDG;
          private string KD_SMILIK;
          public string STATUS;

          private SvcSmilikSelect.call_pttClient svcCaller;
          private SvcSmilikSelect.InputParameters inputPar;
          private SvcSmilikSelect.OutputParameters outPar;
          private SvcSmilikSelect.BPSIMANSROW_R_SMILIK rowData;

          private SvcJnsSertifikatSelect.call_pttClient svcCallerSertifikat;
          private SvcJnsSertifikatSelect.InputParameters inputParSertifikat;
          private SvcJnsSertifikatSelect.OutputParameters outParSertifikat;
          private SvcJnsSertifikatSelect.BPSIMANSROW_R_JNS_SERTI rowDataSertifikat;

          private SvcDokBangunanSelect.BPSIMANSROW_M_KBDG_DOK selectedData;
          public PuDokBangunan( string status, decimal? id_kbdg, decimal? id_kbdg_dok)
        {
            InitializeComponent();
            this.ID_KBDG = id_kbdg;
            this.teIdKbdg.Text = id_kbdg.ToString();
            this.ID_KBDG_DOK = id_kbdg_dok;
            this.STATUS = status;
            if (this.STATUS == "detail")
            {
                this.sbUpload.Enabled = false;
                this.BtnJenisDokumen.Enabled = false;
                this.barSimpan.Enabled = false;
            }
            
        }

          public PuDokBangunan(string status, SvcDokBangunanSelect.BPSIMANSROW_M_KBDG_DOK selectedData)
          {
              InitializeComponent();
              this.STATUS = status;
              this.selectedData = selectedData;
              if (this.STATUS == "detail")
              {
                  this.sbUpload.Enabled = false;
                  this.BtnJenisDokumen.Enabled = false;
                  this.barSimpan.Enabled = false;
              }
              else if (this.STATUS == "edit")
              {
                this.sbJnsDok.Enabled = false;
              }
              this.teJnsDok.Properties.ReadOnly = true;
              this.teFileName.Properties.ReadOnly = true;
          }
   

        private void PuDokBangunan_Load(object sender, EventArgs e)
        {
            this.barSimpan.Caption = konfigApp.labelSimpan;
            this.getInitialDataDokMilik();
        }

        private delegate void AktifkanFormPu(string data);
        private void aktifkanFormPu(string data)
        {
            this.Enabled = true;
        }

        #region LookUp Edit
        public void getInitialDataDokMilik()
        {
            try
            {
                this.Enabled = false;
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcSmilikSelect.InputParameters();
                inputPar.P_COL = "";
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcSmilikSelect.call_pttClient(konfigApp.SvcSmilikSelect_name, konfigApp.SvcSmilikSelect_address);
                svcCaller.Open();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataDokMilik), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new AktifkanFormPu(this.aktifkanFormPu), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataDokMilik(IAsyncResult result)
        {
            try
            {
                this.outPar = svcCaller.Endexecute(result);
                svcCaller.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ShowDataDokMilik(this.showDataDokMilik), this.outPar);
            }
            catch
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new AktifkanFormPu(this.aktifkanFormPu), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataDokMilik(SvcSmilikSelect.OutputParameters dataOut);

        public void showDataDokMilik(SvcSmilikSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_SMILIK.Count();

            
            DataRow dtRow;
            for (int i = 0; i < jmlDataGroup; i++)
            {
                //this.dataGrid.Add(serviceOutPut.SF_ROW_R_SMILIK[i]);
                dtRow = dataTable1.NewRow();
                dtRow["KD_SMILIK"] = serviceOutPut.SF_ROW_R_SMILIK[i].KD_SMILIK;
                dtRow["UR_SMILIK"] = serviceOutPut.SF_ROW_R_SMILIK[i].UR_SMILIK;
                dtRow["UR_DOK"] = serviceOutPut.SF_ROW_R_SMILIK[i].UR_DOK;
                dataTable1.Rows.Add(dtRow);
            }

            teUrDok.Properties.DataSource = dataTable1;
            teUrDok.Properties.DisplayMember = "UR_DOK";
            teUrDok.Properties.ValueMember = "KD_SMILIK";
            teUrDok.Properties.ShowHeader = false;
            teUrDok.Properties.ShowFooter = false;

            


            this.getInitialDataSertifikat();
        }

        

        public void getInitialDataSertifikat()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputParSertifikat = new SvcJnsSertifikatSelect.InputParameters();
                inputParSertifikat.P_COL = "";
                inputParSertifikat.P_MAX = 100;
                inputParSertifikat.P_MAXSpecified = true;
                inputParSertifikat.P_MIN = 0;
                inputParSertifikat.P_MINSpecified = true;
                inputParSertifikat.P_SORT = "";
                svcCallerSertifikat = new SvcJnsSertifikatSelect.call_pttClient(konfigApp.SvcJnsSertifikatSelect_name, konfigApp.SvcJnsSertifikatSelect_address);
                svcCallerSertifikat.Open();
                svcCallerSertifikat.Beginexecute(inputParSertifikat, new AsyncCallback(this.getDataSertifikat), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataSertifikat(IAsyncResult result)
        {
            try
            {
                this.outParSertifikat = svcCallerSertifikat.Endexecute(result);
                svcCallerSertifikat.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new AktifkanFormPu(this.aktifkanFormPu), "");
                this.Invoke(new ShowDataSertifikat(this.showDataSertifikat), this.outParSertifikat);
            }
            catch
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new AktifkanFormPu(this.aktifkanFormPu), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataSertifikat(SvcJnsSertifikatSelect.OutputParameters dataOut);

        public void showDataSertifikat(SvcJnsSertifikatSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_JNS_SERTI.Count();

            DataRow dtRow2;
            for (int i = 0; i < jmlDataGroup; i++)
            {
                //this.dataGrid.Add(serviceOutPut.SF_ROW_R_SMILIK[i]);
                dtRow2 = dataTable2.NewRow();
                dtRow2["KD_JNS_SERTI"] = serviceOutPut.SF_ROW_R_JNS_SERTI[i].KD_JNS_SERTI;
                dtRow2["NM_JNS_SERTI"] = serviceOutPut.SF_ROW_R_JNS_SERTI[i].NM_JNS_SERTI;
                dataTable2.Rows.Add(dtRow2);
            }
            dtRow2 = dataTable2.NewRow();
            dtRow2["KD_JNS_SERTI"] = "-";
            dtRow2["NM_JNS_SERTI"] = "-";
            dataTable2.Rows.Add(dtRow2);
            teKdJnsSrtf.Properties.DataSource = dataTable2;
            teKdJnsSrtf.Properties.DisplayMember = "KD_JNS_SERTI";
            teKdJnsSrtf.Properties.ValueMember = "KD_JNS_SERTI";
            teKdJnsSrtf.Properties.ShowHeader = false;
            teKdJnsSrtf.Properties.ShowFooter = false;
            this.teKdJnsSrtf.ClosePopup();
            this.init();
        }
        public void init()
        {
            if (this.STATUS == "input")
            {
                this.Text = "Input Data Dokumen Bangunan";
                this.ID_KBDG_DOK = 0;
                teUrSmilik.ResetText();
                teUrDok.ResetText();
                teKdJnsSrtf.ResetText();
                this.KD_JNS_DOK_BDG = null;
                this.KD_JNS_SERTI = null;
                this.KD_SMILIK = null;
                this.teKetDok.ResetText();
                this.teNamaDok.Text = "";
                this.teNoDok.Text = "";
                this.teTglDok.Text = "";
                this.teTglBerlaku.Text = "";
                tePenerbit.ResetText();
             
            }
            else if (this.STATUS == "edit")
            {
                try
                {
                    this.Text = "Edit Data Dokumen Bangunan";
                    this.ID_KBDG_DOK = selectedData.ID_KBDG_DOK;
                    this.KD_JNS_DOK_BDG = selectedData.KD_JNS_DOK_BDG;
                    this.KD_JNS_SERTI = selectedData.KD_JNS_SERTI;
                    this.KD_SMILIK = selectedData.KD_SMILIK;
                    this.ID_KBDG = selectedData.ID_KBDG;
                    this.teIdKbdg.Text = selectedData.ID_KBDG.ToString();
                    this.ID_KBDG_DOK = selectedData.ID_KBDG_DOK;
                    this.teNamaDok.Text = selectedData.KD_JNS_DOK_BDG;
                    this.teNoDok.Text = selectedData.NO_DOK;
                    this.teTglDok.Text = konfigApp.DateToString(selectedData.TGL_DOK);
                    this.teTglBerlaku.Text = konfigApp.DateToString(selectedData.TGL_BERLAKU);
                    this.tePenerbit.Text = selectedData.PENERBIT;
                    this.teKetDok.Text = selectedData.KET_DOK;
                    this.teFileName.Text = selectedData.NMFILE;
                    //this.teKdSmilik.Text = selectedData.KD_SMILIK;
                 
                    //this.teUrSmilik.Text = selectedData.UR_SMILIK;

                    try
                    {
                        if (selectedData.KD_SMILIK != "-")
                        {

                            // this.teUrDok.Properties.ValueMember = selectedData.KD_SMILIK;
                            int idx = this.teUrDok.Properties.GetDataSourceRowIndex("KD_SMILIK", selectedData.KD_SMILIK);
                            this.teUrDok.EditValue = this.teUrDok.Properties.GetDataSourceValue("KD_SMILIK", idx);
                           
                        }


                    }
                    catch (Exception)
                    {

                    }
                    try
                    {

                        if (selectedData.KD_JNS_SERTI != "-")
                        {
                            int idx = this.teKdJnsSrtf.Properties.GetDataSourceRowIndex("KD_JNS_SERTI", selectedData.KD_JNS_SERTI);
                            this.teKdJnsSrtf.EditValue = this.teKdJnsSrtf.Properties.GetDataSourceValue("KD_JNS_SERTI", idx);

                        }

                    }
                    catch (Exception)
                    {

                    }

                   
                    
                }
                catch (Exception)
                {

                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }

            }
            else
            {
                this.ID_KBDG_DOK = selectedData.ID_KBDG_DOK;
            }
        }

        #endregion //LookUp Edit
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
                        SvcDokBangunanCrud.InputParameters parInp = new SvcDokBangunanCrud.InputParameters();
                        
                        parInp.P_ID_KBDG = this.ID_KBDG;
                        parInp.P_ID_KBDG_DOKSpecified = true;
                        parInp.P_ID_KBDG_DOK = this.ID_KBDG_DOK;
                        parInp.P_ID_KBDGSpecified = true;

                        parInp.P_KD_JNS_DOK_BDG = konfigApp.StringtoNull(this.KD_JNS_DOK_BDG);
                        parInp.P_KD_JNS_SERTI = konfigApp.StringtoNull(this.KD_JNS_SERTI);
                        parInp.P_KD_SMILIK = konfigApp.StringtoNull(this.KD_SMILIK);
                        parInp.P_NO_DOK = this.teNoDok.Text.Trim();
                        pesan = "Format tanggal dokumen salah.";
                        parInp.P_TGL_DOK = konfigApp.DateToDb(this.teTglDok.Text);
                        pesan = "Format tanggal berlaku salah.";
                        parInp.P_TGL_BERLAKU = konfigApp.DateToDb(this.teTglBerlaku.Text);
                        parInp.P_KET_DOK = this.teKetDok.Text.Trim();
                        parInp.P_NMFILE = this.teFileName.Text;
                         
                        //if (teFilePath.Text != null)
                        //{
                        //    parInp.P_NMFILE = this.teFileName.Text;

                        //}
                        //else
                        //{
                        //    if (selectedData.FILE_EXISTS != 0 )
                        //    {
                        //        parInp.P_NMFILE = selectedData.NMFILE;
                        //    }
                        //}

                        pesan = konfigApp.teksGagalSimpan;
                        parInp.P_PENERBIT = this.tePenerbit.Text.Trim();
                        if (this.STATUS == "input")
                        {
                            parInp.P_SELECT = "C";
                        }
                        else
                        {
                            parInp.P_SELECT = "U";
                        }
                        
                        simpanDokBangunan(parInp);
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
            this.getInitialDataDokMilik();
        }

        private void BtnJenisDokumen_Click(object sender, EventArgs e)
        {
            pUJenisDokumen = new FrmPuJenisDokumenBangunan();
            pUJenisDokumen.ambilJenisDokumenBangunan = new AmbilJenisDokumenBangunan(this.Set_KD_JNS_DOK_BDG);
           
            pUJenisDokumen.ShowDialog();
        }

        private void Set_KD_JNS_DOK_BDG(string KD_DOK, string NM_DOK)
        {
            this.KD_JNS_DOK_BDG = KD_DOK;
            //this.teNamaDok.Text = NM_DOK;
            this.teNamaDok.Text = KD_DOK;
        }

        private void teUrDok_EditValueChanged(object sender, EventArgs e)
        {

            this.KD_SMILIK = teUrDok.GetColumnValue("KD_SMILIK").ToString();
            teUrSmilik.Text = teUrDok.GetColumnValue("UR_SMILIK").ToString();
            if (teUrDok.Text.ToUpper() == "SERTIFIKAT")
            {
               
                this.teKdJnsSrtf.Properties.ReadOnly = false;
               
                this.teNoDok.Properties.ReadOnly = false;
                this.teTglDok.Properties.ReadOnly = false;
                this.tePenerbit.Properties.ReadOnly = false;
                this.teTglBerlaku.Properties.ReadOnly = false;

            }
            else
            {
                int idx = this.teKdJnsSrtf.Properties.GetDataSourceRowIndex("KD_JNS_SERTI", "-");
                this.teKdJnsSrtf.EditValue = this.teKdJnsSrtf.Properties.GetDataSourceValue("KD_JNS_SERTI", idx);
                teKdJnsSrtf.Properties.ReadOnly = true;
                this.teNoDok.Text = "-";
                this.tePenerbit.Text = "-";
                this.teTglBerlaku.Text = "";
                this.teTglDok.Text = "";
                this.teKdJnsSrtf.Properties.ReadOnly = true;
                this.tePenerbit.Properties.ReadOnly = true;
                this.teNoDok.Properties.ReadOnly = true;
                this.teTglDok.Properties.ReadOnly = true;
                this.teTglBerlaku.Properties.ReadOnly = true;
               
            }
        }

        private void teKdJnsSrtf_EditValueChanged(object sender, EventArgs e)
        {

            this.teKdJnsSrtf.EditValueChanged -= new System.EventHandler(this.teKdJnsSrtf_EditValueChanged);
            this.KD_JNS_SERTI = teKdJnsSrtf.GetColumnValue("KD_JNS_SERTI").ToString();
            //teKdJnsSrtf.Text = teKdJnsSrtf.GetColumnValue("KD_JNS_SERTI").ToString();
            teNmJnsSrtf.Text = teKdJnsSrtf.GetColumnValue("NM_JNS_SERTI").ToString();

            this.teKdJnsSrtf.EditValueChanged += new System.EventHandler(this.teKdJnsSrtf_EditValueChanged);
            this.teKdJnsSrtf.ClosePopup();
            if (this.teKdJnsSrtf.Text.Trim() == "SHGB" || this.teKdJnsSrtf.Text.Trim() == "SHP" || this.teKdJnsSrtf.Text.Trim() == "SHU")
            {
                this.teTglBerlaku.Properties.ReadOnly = false;
            }
            else
            {
                this.teTglBerlaku.Properties.ReadOnly = true;
                this.teTglBerlaku.Text = null;
            }
        }

        private void sbUpload_Click(object sender, EventArgs e)
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
                        teFilePath.Text = filePath;
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