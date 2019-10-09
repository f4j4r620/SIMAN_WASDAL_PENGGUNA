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
    public delegate void SimpanDruanganBangunan(SvcDruanganBangunanCrud.InputParameters parIn);
    public partial class PuDruanganBangunan : Form
    {

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public SimpanDruanganBangunan simpanDruanganBangunan;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        private char modeCrud = 'A';
        Thread myThread = null;
        public decimal? ID_KBDG;
        public decimal? ID_SATKER;
        public decimal? ID_KBDG_DETAIL;
        public decimal? ID_SATKER_PMK;
        private FrmPUSatker PuSatker;
       
          SvcDruanganBangunanCrud.call_pttClient svcFasBangunanCrud = null;

          private string JNS_DOK;
          public string STATUS;

          
          public PuDruanganBangunan( string status,decimal? id_satker, decimal? id_kbdg, decimal? id_kbdg_detail)
        {
            InitializeComponent();
            this.ID_SATKER = id_satker;
            this.ID_KBDG = id_kbdg;
            this.teIdKbdg.Text = id_kbdg.ToString();
            this.ID_KBDG_DETAIL = id_kbdg_detail;
            this.STATUS = status;
            if (STATUS == "detail" || STATUS == "input") 
            {
              this.teKodePemakai.Text = konfigApp.kodeSatker;
              this.teNamaPemakai.Text = konfigApp.namaSatker;
            }

        }

          public PuDruanganBangunan(string status, decimal? id_satker, SvcDruanganBangunanSelect.BPSIMANSROW_M_KBDG_DETAIL selectedData)
          {
              InitializeComponent();
              this.STATUS = status;
              this.ID_SATKER = id_satker;
              this.ID_KBDG = selectedData.ID_KBDG;
              this.teIdKbdg.Text = selectedData.ID_KBDG.ToString();
              this.ID_KBDG_DETAIL = selectedData.ID_KBDG_DETAIL;
              //this.ID_SATKER_PMK = selectedData.ID_SATKER_PMK;
              
              this.teLantai.Text = selectedData.LANTAI.ToString();
              this.teLuas.Text = selectedData.LUAS.ToString();
              //this.teKodePemakai.Text = selectedData.KD_SATKER_PMK;
              ////this.teNamaPemakai.Text = selectedData.UR_SATKER_PMK;
              ////this.teTipeRuangan.Text = selectedData.TYPE_RUANG;
              this.teUrRuangan.Text = selectedData.UR_RUANG;
              this.teKet.Text = selectedData.KET;
              this.teKD_LokRuangan.Text = selectedData.KD_LOKRUANG;
              this.teKodePemakai.Text = Convert.ToString(selectedData.ID_SATKER_PMK);
              this.teNamaPemakai.Text = selectedData.UR_SATKER_PMK;
          }
   

        private void PuDruanganBangunan_Load(object sender, EventArgs e)
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
                    string pesan="";
                    try 
	                {	        
                        SvcDruanganBangunanCrud.InputParameters parInp = new SvcDruanganBangunanCrud.InputParameters();
                        
                        parInp.P_ID_KBDG = this.ID_KBDG;
                        parInp.P_ID_KBDGSpecified = true;
                        parInp.P_ID_KBDG_DETAIL = this.ID_KBDG_DETAIL;
                        parInp.P_ID_KBDG_DETAILSpecified = true;

                        parInp.P_LANTAI =(float?) this.teLantai.Value;
                        parInp.P_LANTAISpecified = true;
                        parInp.P_LUAS = (float?)this.teLuas.Value;
                        parInp.P_LUASSpecified = true;
                        //parInp.P_ID_SATKER_PMK = this.ID_SATKER_PMK;
                        parInp.P_ID_SATKER_PMKSpecified = true;
                        //parInp.P_TYPE_RUANG = this.teTipeRuangan.Text.Trim();
                        parInp.P_ID_SATKER_PMK = (this.ID_SATKER_PMK == null)? konfigApp.idSatker : this.ID_SATKER_PMK;
                        parInp.P_KD_LOKRUANG = konfigApp.StringtoNull(this.teKD_LokRuangan.Text.Trim());
                        parInp.P_KET = this.teKet.Text.Trim();
                        
                        if (this.STATUS == "input")
                        {
                            parInp.P_SELECT = "C";
                        }
                        else
                        {
                            parInp.P_SELECT = "U";
                        }
                        
                        simpanDruanganBangunan(parInp);
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
            this.teKD_LokRuangan.Text = "";
            this.teLantai.Text = "";
            this.teUrRuangan.Text = "";
        }

        private void btnSatker_Click(object sender, EventArgs e)
        {
            PuSatker = new FrmPUSatker();
            PuSatker.ambilSatker = new AmbilSatker(this.ambilSatker);
            PuSatker.ShowDialog();
        }

        private void ambilSatker(decimal? id, string kode, string nama)
        {
            this.ID_SATKER_PMK = id;
            this.teKodePemakai.Text = kode;
            this.teNamaPemakai.Text = nama;
        }

        PU.FrmPURuang tipRuang;
        private void Ruangan_Click(object sender, EventArgs e)
        {
            string _where = "ID_SATKER = " + this.ID_SATKER;
            tipRuang = new PU.FrmPURuang(_where);
            tipRuang.ambilRuang = new PU.AmbilRuang(ambilTipRuang);
            tipRuang.ShowDialog();
        }

        private void ambilTipRuang(string kd, string ur)
        {
            teKD_LokRuangan.Text = kd;
            teUrRuangan.Text = ur;
        }

    }
}