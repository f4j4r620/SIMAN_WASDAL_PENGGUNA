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
namespace AppPengguna.AST.SJT
{
    public delegate void SimpanSenjataPerlengkapan(SvcSenjataPerlengkapanCrud.InputParameters parIn);
    public partial class PuSenjataPerlengkapan : Form
    {

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public SimpanSenjataPerlengkapan simpanSenjataPerlengkapan;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        private char modeCrud = 'A';
        Thread myThread = null;
        public decimal? ID_KSENJ;
        public decimal? ID_KSENJ_PERLENGKAP;
          SvcSenjataPerlengkapanCrud.call_pttClient svcNjopBangunanCrud = null;

         
          private string STATUS;

          
          public PuSenjataPerlengkapan( string status, decimal? id_kbdg, decimal? id_ksenj_perlengkap)
        {
            InitializeComponent();
            this.ID_KSENJ = id_kbdg;
            this.teId_Ksenj.Text = id_kbdg.ToString();
            this.ID_KSENJ_PERLENGKAP = id_ksenj_perlengkap;
            this.STATUS = status;
            if (this.STATUS == "detail")
            {
                this.barSimpan.Enabled = false;
            }

        }

          public PuSenjataPerlengkapan(string status, SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP selectedData)
          {
              InitializeComponent();
              this.STATUS = status;

              this.ID_KSENJ = selectedData.ID_KSENJ;
              this.teId_Ksenj.Text = selectedData.ID_KSENJ.ToString();
              this.ID_KSENJ_PERLENGKAP = selectedData.ID_KSENJ_PERLENGKAP;
              this.teNamaPerlengkapan.Text = selectedData.NM_PERLENGKAP;
              this.teKet.Text = selectedData.KET;
              
          }
   

        private void PuSenjataPerlengkapan_Load(object sender, EventArgs e)
        {
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
                    string pesan = konfigApp.teksGagalSimpan;

                    try 
	                {	        
                        SvcSenjataPerlengkapanCrud.InputParameters parInp = new SvcSenjataPerlengkapanCrud.InputParameters();
                        
                        parInp.P_ID_KSENJ = this.ID_KSENJ;
                        parInp.P_ID_KSENJ_PERLENGKAPSpecified = true;
                        parInp.P_ID_KSENJ_PERLENGKAP = this.ID_KSENJ_PERLENGKAP;
                        parInp.P_ID_KSENJSpecified = true;
                        parInp.P_KET = this.teKet.Text.Trim();
                        parInp.P_NM_PERLENGKAP = this.teNamaPerlengkapan.Text.Trim();
                        if (this.STATUS == "input")
                        {
                            parInp.P_SELECT = "C";
                        }
                        else
                        {
                            parInp.P_SELECT = "U";
                        }
                        
                        simpanSenjataPerlengkapan(parInp);
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
            this.teKet.Text = "";
            this.teNamaPerlengkapan.Text = "";
        }


    }
}