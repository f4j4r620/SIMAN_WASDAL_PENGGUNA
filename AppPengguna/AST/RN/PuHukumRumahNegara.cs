﻿using System;
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
namespace AppPengguna.AST.RN
{
    public delegate void SimpanPhukumBangunan(string text);
    public partial class PuHukumRumahNegara : Form
    {

    
        public SimpanPhukumBangunan simpanPhukumBangunan;


        public string ID_JNSDOK;
        private char modeCrud = 'A';
        Thread myThread = null;
        public decimal? ID_KRMH_NEG;
        public decimal? ID_KRMH_HUKUM;
        public string FilePath;
        SvcStatusHukumSelect.call_pttClient svcStatusHukumSelect;
        SvcStatusHukumSelect.OutputParameters outParHukum;
        SvcHukRmhNgrCrud.call_pttClient svcNjopBangunanCrud = null;
        SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM selectedData;
        public string KD_STATUS_HUKUM;
        public string STATUS;

          #region Progress Bar
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
          public void aktifkanForm(string text)
          {
              this.Enabled = true;
          }
          #endregion
         
          public PuHukumRumahNegara( string STATUS, decimal? id_krmhneg, decimal? id_m_krmhneg_hukum)
        {
            InitializeComponent();
            this.ID_KRMH_NEG = id_krmhneg;
            this.teIdKbdg.Text = id_krmhneg.ToString();
            this.ID_KRMH_HUKUM = id_m_krmhneg_hukum;
            this.STATUS = STATUS;
            if (this.STATUS == "edit" || this.STATUS == "detail")
            {
              this.teFileName.Properties.ReadOnly = true;
              this.teJnsDok.Properties.ReadOnly = true;
              this.sbJnsDok.Enabled = false;
            }
            //init();
        }

          public PuHukumRumahNegara(string STATUS, SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM Data)
          {
            InitializeComponent();
            this.STATUS = STATUS;
            this.selectedData = Data;

          }
   

        private void PuPhukumBangunan_Load(object sender, EventArgs e)
        {
            this.barSimpan.Caption = konfigApp.labelSimpan;
            if (this.STATUS == "detail")
            {
                //this.barSimpan.Enabled = false;
                //this.btnUploadFile.Enabled = false;
            }
            else if(this.STATUS == "edit")
            {
              
              this.sbJnsDok.Enabled = false;
            }
            this.teFileName.Properties.ReadOnly = true;
            this.teJnsDok.Properties.ReadOnly = true;
            
            this.getInitialStatusukum();
        }

        private void init()
        {
            if (this.STATUS == "input")
            {
                //this.ID_KRMH_NEG = null;
                this.teIdKbdg.ResetText();
                this.ID_KRMH_HUKUM = null;
                this.teTGL.ResetText();
                int idx = this.teJNS_STATUS_HKM.Properties.GetDataSourceRowIndex("KD_STATUS_HUKUM", "201");
                this.teJNS_STATUS_HKM.EditValue = this.teJNS_STATUS_HKM.Properties.GetDataSourceValue("KD_STATUS_HUKUM", idx);
                this.KD_STATUS_HUKUM = null;
                this.teSTATUS_HUKUM.ResetText();
                this.tePHK_SENGKETA.ResetText();
                this.teUR_MASALAH.ResetText();
                this.teFileName.ResetText();
               
               

            }
            else if (this.STATUS == "edit" || this.STATUS == "detail")
            {


                this.ID_KRMH_NEG = selectedData.ID_KRMH_NEG;
                this.teIdKbdg.Text = selectedData.ID_KRMH_NEG.ToString();
                this.ID_KRMH_HUKUM = selectedData.ID_KRMH_HUKUM;
                this.teTGL.Text = konfigApp.DateToString(selectedData.TGL);
                this.teJNS_STATUS_HKM.Text = selectedData.JNS_STATUS_HUKUM;
                this.KD_STATUS_HUKUM = selectedData.KD_STATUS_HUKUM;
                this.teSTATUS_HUKUM.Text = selectedData.STATUS_HUKUM;
                this.tePHK_SENGKETA.Text = selectedData.PHK_SENGKETA;
                this.teUR_MASALAH.Text = selectedData.UR_MASALAH;
                this.teFileName.Text = selectedData.NMFILE;
                //this.teTERAKHIR_YN.Text = selectedData.TERAKHIR_YN;
                try
                {
                    if (selectedData.KD_STATUS_HUKUM != "-")
                    {

                        // this.teUrDok.Properties.ValueMember = selectedData.KD_SMILIK;
                        int idx = this.teJNS_STATUS_HKM.Properties.GetDataSourceRowIndex("KD_STATUS_HUKUM", selectedData.KD_STATUS_HUKUM);
                        this.teJNS_STATUS_HKM.EditValue = this.teJNS_STATUS_HKM.Properties.GetDataSourceValue("KD_STATUS_HUKUM", idx);
                        idx = this.teJNS_STATUS_HKM.Properties.GetDataSourceRowIndex("KD_STATUS_HUKUM", selectedData.KD_STATUS_HUKUM);
                    }


                }
                catch (Exception)
                {

                }
               
            }
            this.teJNS_STATUS_HKM.ClosePopup();
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
     
             simpanPhukumBangunan("save");     
            
        }

       

      

        private void barBatal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barBersih_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.init();
        }

      
    
      

        private void teJNS_STATUS_HKM_EditValueChanged(object sender, EventArgs e)
        {
            this.teJNS_STATUS_HKM.EditValueChanged -= new System.EventHandler(this.teJNS_STATUS_HKM_EditValueChanged);
            this.KD_STATUS_HUKUM = teJNS_STATUS_HKM.GetColumnValue("KD_STATUS_HUKUM").ToString();
           // this.teJNS_STATUS_HKM.Text = teJNS_STATUS_HKM.GetColumnValue("JNS_STATUS_HUKUM").ToString();
            this.teSTATUS_HUKUM.Text = teJNS_STATUS_HKM.GetColumnValue("STATUS_HUKUM").ToString();
            this.teJNS_STATUS_HKM.EditValueChanged += new System.EventHandler(this.teJNS_STATUS_HKM_EditValueChanged);
            this.teJNS_STATUS_HKM.ClosePopup();
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
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

        public void getInitialStatusukum()
        {
            try
            {


                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcStatusHukumSelect.InputParameters inputParHukum = new SvcStatusHukumSelect.InputParameters();
                inputParHukum.P_COL = "";

              
                //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

                inputParHukum.P_MAX = 100;
                inputParHukum.P_MAXSpecified = true;
                inputParHukum.P_MIN = 0;
                inputParHukum.P_MINSpecified = true;
                inputParHukum.P_SORT = "";
                svcStatusHukumSelect = new SvcStatusHukumSelect.call_pttClient();
                svcStatusHukumSelect.Beginexecute(inputParHukum, new AsyncCallback(this.getStatusHukum), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }


        public void getStatusHukum(IAsyncResult result)
        {
            try
            {
                this.outParHukum = svcStatusHukumSelect.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowData(this.showData), this.outParHukum);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcStatusHukumSelect.OutputParameters dataOut);

        public void showData(SvcStatusHukumSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_STATUS_HUKUM.Count();

            DataRow dtRow;
            for (int i = 0; i < jmlDataGroup; i++)
            {
                //this.dataGrid.Add(serviceOutPut.SF_ROW_R_SMILIK[i]);
                dtRow = dataTable1.NewRow();
                dtRow["JNS_STATUS_HUKUM"] = serviceOutPut.SF_ROW_R_STATUS_HUKUM[i].JNS_STATUS_HUKUM;
                dtRow["KD_STATUS_HUKUM"] = serviceOutPut.SF_ROW_R_STATUS_HUKUM[i].KD_STATUS_HUKUM;
                dtRow["STATUS_HUKUM"] = serviceOutPut.SF_ROW_R_STATUS_HUKUM[i].STATUS_HUKUM;
                dataTable1.Rows.Add(dtRow);
            }

            teJNS_STATUS_HKM.Properties.DataSource = dataTable1;
            teJNS_STATUS_HKM.Properties.DisplayMember = "JNS_STATUS_HUKUM";
            teJNS_STATUS_HKM.Properties.ValueMember = "KD_STATUS_HUKUM";
            teJNS_STATUS_HKM.Properties.ShowHeader = false;
            teJNS_STATUS_HKM.Properties.ShowFooter = false;

            this.init();

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