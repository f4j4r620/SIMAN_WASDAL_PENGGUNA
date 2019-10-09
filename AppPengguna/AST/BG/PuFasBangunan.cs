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

namespace AppPengguna.AST.BG
{
    public delegate void SimpanFasBangunan(SvcFasBangunanCrud.InputParameters parIn);
    public partial class PuFasBangunan : Form
    {

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public SimpanFasBangunan simpanFasBangunan;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        private char modeCrud = 'A';
        Thread myThread = null;
        public decimal? ID_KBDG;
        public decimal? ID_KBDG_FAS_PENUNJANG;
        public string STATUS;
          SvcFasBangunanCrud.call_pttClient svcFasBangunanCrud = null;
          SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG selectedData;
          public PuFasBangunan(string status, decimal? id_kbdg)
        {
            

            InitializeComponent();
            this.ID_KBDG = id_kbdg;
            this.teIdKbdg.Text = id_kbdg.ToString();
            this.STATUS = status;
            this.Init();
        }

          public PuFasBangunan(string status, SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG _Data)
          {
              InitializeComponent();
              this.STATUS = status;
              this.selectedData = _Data;
              this.Init();
          }
   

        private void PuFasBangunan_Load(object sender, EventArgs e)
        {
            if (this.STATUS == "detail")
            {
                this.barSimpan.Enabled = false;
            }
            this.barSimpan.Caption = konfigApp.labelSimpan;
        }

        private bool cek_input()
        {
            return true;
            /*
            string listrik = this.teListrik.Text.Trim();
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
            if (this.barSimpan.Caption == konfigApp.labelSimpan)
            {
                if(this.cek_input())
                {
                    try 
	                {	        
                        SvcFasBangunanCrud.InputParameters parInp = new SvcFasBangunanCrud.InputParameters();
                        parInp.P_ID_KBDG = this.ID_KBDG;
                        parInp.P_ID_KBDGSpecified = true;
                        parInp.P_ID_KBDG_FAS_PENUNJANG = this.ID_KBDG_FAS_PENUNJANG;
                        parInp.P_ID_KBDG_FAS_PENUNJANGSpecified = true;
                        parInp.P_LISTRIK = this.teListrik.Text.Trim();
                        parInp.P_PAM = this.tePAM.Text.Trim();
                        parInp.P_TELPON = this.teTelpon.Text.Trim();
                        parInp.P_GAS = this.cbGas.Text;
                        parInp.P_KET = this.teKet.Text.Trim();
                        parInp.P_LAINNYA = this.teFasLainnya.Text;
                        parInp.P_SAL_LIMBAH = this.cbSaluranLimbah.Text;

                        if (this.STATUS == "input")
                        {
                            parInp.P_SELECT = "C";
                        }
                        else
                        {
                            parInp.P_SELECT = "U";
                        }
                        
                        simpanFasBangunan(parInp);
	                }
	                catch (Exception E)
	                {

                        //this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                        MessageBox.Show(E.ToString(), "Error");
	                }
                }else
                {
                    MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
                }
            }
            else //Edit
            {
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

        private void Init()
        {
            if (this.STATUS == "input")
            {
                this.teListrik.Text = "";
                this.tePAM.Text = "";
                this.teTelpon.Text = "";
                this.cbGas.SelectedIndex = -1;
                this.cbSaluranLimbah.SelectedIndex = -1;
                this.teFasLainnya.Text = "";
                this.teKet.Text = "";
            }
            else
            {
                this.ID_KBDG = selectedData.ID_KBDG;
                this.ID_KBDG_FAS_PENUNJANG = selectedData.ID_KBDG_FAS_PENUNJANG;
                this.teIdKbdg.Text = selectedData.ID_KBDG.ToString();
                this.teListrik.Text = selectedData.LISTRIK;
                this.tePAM.Text = selectedData.PAM;
                this.teTelpon.Text = selectedData.TELPON;
                this.cbGas.Text = selectedData.GAS;
                this.cbSaluranLimbah.Text = selectedData.SAL_LIMBAH;
                this.teFasLainnya.Text = selectedData.LAINNYA;
                this.teKet.Text = selectedData.KET;
            }
        }

    }
}