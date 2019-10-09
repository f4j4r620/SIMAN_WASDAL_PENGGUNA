using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppPengguna.PU;
using System.IO;
using DevExpress.XtraBars;
using AppPengguna.RPT;
using DevExpress.XtraReports.UI;
namespace AppPengguna.AST.BG
{
    public delegate void UpdateBangunan(SvcBangunanCrud.OutputParameters dataOutBangunanCrud);
    public  class ucBangunanForm : ucDetailAsetForm
    {
      
        public ucIdentitasBangunan identitas = new ucIdentitasBangunan();
        private SvcBangunanSelect.BPSIMANSROW_M_KBDG selectedData;
      
        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public UpdateBangunan updateBangunan;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        public ProgBar progressbar;
      

        private Thread myThread;
        private Thread myThread_;
        private char modeCrud;
        private string KD_KONDISI;
        private string KD_KAB;
        private string KD_PROV;
        private decimal? ID_SATKER;
        private decimal? ID_KBDG;
        private Boolean mulai = true;
        private decimal? ID_SATKER_PMK;
        private decimal? ID_KTNH; /// Just Test
        private string KD_STATUS;
        private decimal? ID_KANWIL;
        private decimal? ID_KORWIL;
        private decimal? ID_ESELON1;
        private decimal? ID_KL;
        private decimal? ID_PENGADAAN;
        
        private int ulang = 0;
        private SvcBangunanCrud.call_pttClient svcbangunanCrud;
        private SvcBangunanCrud.OutputParameters outDataCrud;
        private SvcAssetTanahSelect.call_pttClient fetchDataTanah;
        private SvcAssetTanahSelect.OutputParameters outDataTanahSelect;

        //------------- GET DOKUMEN & KIB-------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;

        private SvcKibBangunanSelect.call_pttClient fetchDataKib;
        private SvcKibBangunanSelect.InputParameters parInpKib;
        private SvcKibBangunanSelect.OutputParameters outDataKib;
        public SvcKibBangunanSelect.BPSIMANSROW_M_KBDG_DOK_KIB selectedDataKib;

        private SvcDokBangunanSelect.call_pttClient fetchDataDok;
        private SvcDokBangunanSelect.InputParameters parInpDok;
        private SvcDokBangunanSelect.OutputParameters outDatDok;
        private SvcDokBangunanSelect.BPSIMANSROW_M_KBDG_DOK selectedDataDok;

        //-------------------------------------------------------

        ucDokBangunan ucdokBangunan;
        ucLokasiBangunan ucLokBangunan;
        ucFasBangunan ucFasBangunan;
        ucRpemeliharaanBangunan ucrPemeliharaanBangunan;
        ucRnilaiBangunan ucrNilaiBangunan;
        ucRmutasiBangunan ucrMutasiBangunan;
        ucRpenggunaBangunan ucrPenggunaBangunan;
        ucRiwayatBangunan ucriwayatBangunan;

        #region PopUp Form
        private FrmPUSatker PuSatker;
        private FrmPUKondisi PuKondisi;
        private FrmPUSskel PuSskel;
        #endregion

        #region Variable Aktif Tab
        private bool status_FasPenunjang;
        private bool status_DokBangunan;
        private bool status_LokBangunan;
        private bool status_GPS;
        private bool status_DetailRuangan;
        private bool status_KonstruksiBangunan;
        private bool status_RiwayatPenilaian;
        private bool status_NJOP;
        private bool status_RiwayatBangunan;
        private bool status_RiwayatPengguna;
        private bool status_RiwayatMutasi;
        private bool status_RiwayatPemeliharaan;
        private bool status_PermasalahanHukum;
        private bool status_DokKib;
        private bool status_Susut;
        private bool status_Spm;
        private bool status_Asuransi;
        private bool status_Foto;
        #endregion

        UPG1206 upg1206;
        UPG1206_Foto upg1206foto;
   
        public ucBangunanForm(string status)
        {
           
            this.status = status;
          
        }

        public bool IsiForm(SvcBangunanSelect.BPSIMANSROW_M_KBDG Data)
        {
            this.selectedData = Data;
            try
            {
                this.ID_SATKER = selectedData.ID_SATKER;
                this.ID_KBDG = selectedData.ID_KBDG;
                this.ID_ASET = selectedData.ID_ASET;
                this.ID_KTNH = selectedData.ID_ASET_KTNH;
                #region LayoutKiri
                // -- IDENTITAS UAKPB --
                this.teKdUAKPB.Text = selectedData.KD_SATKER;
                this.teUrUAKPB.Text = selectedData.UR_SATKER;

                ///----------- IDENTITAS BANGUNAN ---------------------
                this.teNoReg.Text = selectedData.NOREG;
               this.teKdBrg.Text = selectedData.KD_BRG;
               this.teNUP.Text = selectedData.NO_ASET.ToString();
               this.teKIB.Text = selectedData.NO_KIB;
               this.teNamaBarang.Text = selectedData.UR_SSKEL;
               string tipe = (selectedData.TIPE != "-" && selectedData.TIPE != "") ? "/" + selectedData.TIPE : "";
               this.teMerk_tipe.Text = selectedData.MERK + tipe;
             
                // --- KODE---
               this.KD_KONDISI = selectedData.KD_KONDISI;
               this.teKondisi.Text = selectedData.UR_KONDISI;
               this.identitas.teJalan.Text = selectedData.LOKASI;
               this.identitas.teRTRW.Text = selectedData.KD_RTRW;
               this.identitas.teKomplek.Text = selectedData.KOMPLEK;
               this.identitas.teKelurahan.Text = selectedData.KD_KEL;
               this.identitas.teKecamatan.Text = selectedData.KD_KEC;
               this.identitas.teAlamat.Text = selectedData.ALAMAT;
               //-----KODE----
               this.KD_KAB = selectedData.KD_KAB;
               this.KD_PROV = selectedData.KD_PROV;
               this.identitas.teKabupaten.Text = selectedData.UR_KAB;
               this.identitas.teProvinsi.Text = selectedData.UR_PROV;
               this.identitas.teKodePos.Text = selectedData.KD_POS;

               // //-----------------  IMB -----------------------------------
               this.identitas.teNomorIMB.Text = selectedData.NO_IMB;
               this.identitas.teTglIMB.Text = konfigApp.DateToString(selectedData.TGL_IMB);
               this.identitas.teTanggalGuna.Text = konfigApp.DateToString(selectedData.TGL_GUNA);
               this.identitas.teTanggalRenovasi.Text = konfigApp.DateToString(selectedData.TGL_RENOV);
               // //---------- KODE -------------------
               this.KD_STATUS = selectedData.KD_STATUS;
               this.identitas.teStatusPengguna.Text = selectedData.UR_STATUS;
               this.identitas.teJenisPengguna.Text = selectedData.JNS_PMK;
               // //---------- KODE -------------------
               this.ID_SATKER_PMK = selectedData.ID_SATKER_PMK;
               this.identitas.teKodePengguna.Text = selectedData.KD_SATKER_PMK;
               this.identitas.teNamaPengguna.Text = selectedData.UR_SATKER_PMK;
               this.identitas.teStatusPengelolaan.Text = selectedData.STATUS_KELOLA;

               this.identitas.teStatusHukum.Text = selectedData.STATUS_HUKUM;
               this.identitas.teCatatan.Text = selectedData.CATATAN;


               // #endregion

               // #region layout Kanan
               // //-------- LUAS ----------
               this.identitas.teLuasBangunan.Text = selectedData.LUAS_BDG.ToString();
               this.identitas.teLuasDasarBangunan.Text = selectedData.LUAS_DSR.ToString();
               this.identitas.teJumlahLantai.Text = selectedData.JML_LT.ToString();

               // //----- NILAI(DALAM RUPIAH) -----
               this.identitas.teNilaiPerolehan.Text = selectedData.RPH_ASET.ToString();
               this.identitas.teNilaiMutasi.Text = selectedData.RPH_MUTASI.ToString();
               try
               {
                   this.identitas.teNilaiSebelumPenyusutan.Text = selectedData.NILAI_SBLM_SUSUT.ToString();
               }
               catch (Exception)
               {
                   this.identitas.teNilaiSebelumPenyusutan.Value = 0;
               }
               this.identitas.teNilaiPenyusutan.Text = selectedData.RPH_SUSUT.ToString();
               try
               {
                   this.identitas.teNilaiBuku.Text = selectedData.NILAI_BUKU.ToString();
               }
               catch (Exception)
               {
                   this.identitas.teNilaiBuku.Value = 0;
               }

               this.identitas.teTanggalPembukuan.Text = konfigApp.DateToString(selectedData.TGL_BUKU); //dihapus
               this.identitas.teTanggalRekam.Text = konfigApp.DateToString(selectedData.TGL_REKAM);

               //----- PENGADAAN -----
               //--------- kode -----------------
               this.ID_PENGADAAN = selectedData.ID_PENGADAAN;
               this.identitas.teNomorDana.Text = selectedData.NO_DANA;
               this.identitas.teTanggalDana.Text = konfigApp.DateToString(selectedData.TGL_DANA);
               this.identitas.teCaraPerolehan.Text = selectedData.DARI;
               this.identitas.teAsalPerolehan.Text = selectedData.ASL_PERLH;  // dihapus
               this.identitas.teTanggalPerolehan.Text = konfigApp.DateToString(selectedData.TGL_PERLH);
               


                #endregion

                #region Lain-lain
                this.ID_KANWIL = selectedData.ID_KANWIL;
                this.ID_KORWIL = selectedData.ID_KORWIL;
                this.ID_ESELON1 = selectedData.ID_ESELON1;
                this.ID_KL = selectedData.ID_KL;


                //this.teKd_JnsBmn.Text = selectedData.KD_JNS_BMN.ToString();
                //this.teNamaJnsBmn.Text = selectedData.NM_JNS_BMN;
                //this.teKdWilEselon.Text = selectedData.KD_WILESELON;
                //this.teKdKorwil.Text = selectedData.KD_KORWIL;
                //this.teKdEselonKl.Text = selectedData.KD_ESELONKL;
                //this.teUrWil.Text = selectedData.UR_WIL;
                //this.teKdEselon1.Text = selectedData.KD_ESELON1;
                //this.teKdKl.Text = selectedData.KD_KL;
                //this.teUrEselon1.Text = selectedData.UR_ESELON1;
                //this.teUrKl.Text = selectedData.UR_KL;
                //this.teKdKanwil.Text = selectedData.KD_KANWIL;
                //this.teUrKanwil.Text = selectedData.UR_KANWIL;
                //this.teThnAng.Text = selectedData.THN_ANG;
                //this.tePeriode.Text = selectedData.PERIODE;
                //this.teKdDsrHrg.Text = selectedData.KD_DSR_HRG;
                //this.teUrDsrHrg.Text = selectedData.UR_DSR_HRG;
                //this.teKdData.Text = selectedData.KD_DATA;
                //this.teUrData.Text = selectedData.UR_DATA;
                //this.teFlagKor.Text = selectedData.FLAG_KOR;
                //this.teKdBlu.Text = selectedData.KDBLU;
                //this.teFlagTtp.Text = selectedData.FLAG_TTP;
                //this.teFlagKrm.Text = selectedData.FLAG_KRM;
                //this.teNoReg.Text = selectedData.NOREG;
                //this.teKdBapel.Text = selectedData.KDBAPEL;
                //this.teKdKpknl.Text = selectedData.KDKPKNL;
                //this.teUrKpknl.Text = selectedData.URKPKNL;
                //this.teUmeko.Text = selectedData.UMEKO.ToString();
                //this.teRphRes.Text = selectedData.RPH_RES.ToString();
                //this.teKdKppn.Text = selectedData.KDKPPN;
                //this.teUrKppn.Text = selectedData.URKPPN;
  
                //this.teKonsSist.Text = selectedData.KONS_SIST;
                //this.teRphNJOP.Text = selectedData.RPHNJOP.ToString();
                //this.teRphWajar.Text = selectedData.RPHWAJAR.ToString();
                //this.teCad1.Text = selectedData.CAD1;
                //this.teSTATUS_BMN_YN.Text = selectedData.STATUS_BMN_YN;
                //this.teTERCATAT.Text = selectedData.TERCATAT;
                //this.teAlamat.Text = selectedData.ALAMAT;


                #endregion

                return true;

                
            
            }
            catch (Exception E)
            {
                this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                this.aktifkanForm("");
               // MessageBox.Show(konfigApp.teksGagalAmbil,konfigApp.judulGagalAmbil);
                
                MessageBox.Show("Data belum dipilih", konfigApp.judulGagalAmbil);
                return false;
            
                
                
            }
            
        }

        private void getInitData()
        {
            #region LookUp ID KTNH
            try
            {

                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAssetTanahSelect.InputParameters parInp = new SvcAssetTanahSelect.InputParameters();
                parInp.P_MAX = 1;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
                parInp.STR_WHERE = "ID_ASET = " + this.ID_KTNH;
                fetchDataTanah = new SvcAssetTanahSelect.call_pttClient(konfigApp.SvcAssetTanahSelect_name, konfigApp.SvcAssetTanahSelect_address);
                fetchDataTanah.Open();
                fetchDataTanah.Beginexecute(parInp, new AsyncCallback(this.getResultTanah), null);

               
            }
            catch (Exception E)
            {
                this.modeCrud = 'A';
                this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                this.aktifkanForm("");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
            
            }
            #endregion
        }

        protected void getResultTanah(IAsyncResult result)
        {
            try
            {
                this.outDataTanahSelect = fetchDataTanah.Endexecute(result);
                fetchDataTanah.Close();
                if (this.InvokeRequired)
                {
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                    this.Invoke(new ShowDataTanah(this.showDataTanah), this.outDataTanahSelect);
                }
                else
                {
                    this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                    this.aktifkanForm("");
                    this.showDataTanah(this.outDataTanahSelect);
                }
                
            }
            catch (Exception E)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    
                }
                else
                {
                    this.aktifkanForm("");
                    this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                    
                }
            
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataTanah(SvcAssetTanahSelect.OutputParameters dataOut);

        public void showDataTanah(SvcAssetTanahSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_ASET_M_KTNH.Count();

            for (int i = 0; i < jmlData; i++)
            {
                this.ID_KTNH = serviceOutPut.SF_ROW_ASET_M_KTNH[0].ID_KTNH;
                this.identitas.teJenisPemilikTanah.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].JNS_PMK;
                this.identitas.teKodePenggunaTanah.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].KD_SATKER;
                this.identitas.teNamaPenggunaTanah.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].UR_SATKER;
                this.identitas.teKodeBrgTanah.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].KD_BRG;
                this.identitas.teNUPTanah.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].NO_ASET.ToString();
                this.identitas.teKIBTanah.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].NO_KIB;
                this.identitas.teNamaBarangTanah.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].UR_SSKEL;
                this.identitas.teNomorDokumenTanah.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].KD_JNS_SERTI;
            }
            if (this.mulai == true)
            {
                this.getInitPhoto(1);
            }
        }

        

        public void simpanUpdate()
        {
            if (this.cek_input())
            {
               
                    try
                    {
                        this.nonAktifForm("");
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcBangunanCrud.InputParameters parInp = new SvcBangunanCrud.InputParameters();

                        

                        #region Data yang tidak dapat diubah
                        parInp.P_ALAMAT = selectedData.ALAMAT;
                        parInp.P_CAD1 = selectedData.CAD1;
                        parInp.P_FLAG_KOR = selectedData.FLAG_KOR;
                        parInp.P_FLAG_KRM = selectedData.FLAG_KRM;
                        parInp.P_FLAG_TTP = selectedData.FLAG_TTP;
                        parInp.P_ID_ASET = selectedData.ID_ASET;
                        parInp.P_ID_ASET_KTNH = selectedData.ID_ASET_KTNH;
                        parInp.P_ID_ASET_KTNHSpecified = true;
                        parInp.P_ID_ASETSpecified = true;
                        parInp.P_ID_PENGADAAN = selectedData.ID_PENGADAAN;
                        parInp.P_ID_PENGADAANSpecified = true;
                        parInp.P_ID_SATKER = selectedData.ID_SATKER;
                        parInp.P_ID_SATKER_PMK = selectedData.ID_SATKER_PMK;
                        parInp.P_ID_SATKER_PMKSpecified = true;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_JML_LT = selectedData.JML_LT;
                        parInp.P_JML_LTSpecified = true;
                        parInp.P_JNS_PMK = selectedData.JNS_PMK;
                        parInp.P_KD_BRG = selectedData.KD_BRG;
                        parInp.P_KD_DATA = selectedData.KD_DATA;
                        parInp.P_KD_DSR_HRG = selectedData.KD_DSR_HRG;
                        parInp.P_KD_JNS_BMN = selectedData.KD_JNS_BMN;
                        parInp.P_KD_JNS_BMNSpecified = true;
                        parInp.P_LOKASI = selectedData.LOKASI;
                        parInp.P_KD_KAB = selectedData.KD_KAB;
                        parInp.P_KD_KEC = selectedData.KD_KEC;
                        parInp.P_KD_KEL = selectedData.KD_KEL;
                        parInp.P_KD_KONDISI = selectedData.KD_KONDISI;
                        parInp.P_KD_PROV = selectedData.KD_PROV;
                        parInp.P_KD_RTRW = selectedData.KD_RTRW;
                        parInp.P_KD_STATUS = selectedData.KD_STATUS;
                        parInp.P_KDBAPEL = selectedData.KDBAPEL;
                        parInp.P_KDBLU = selectedData.KDBLU;
                        parInp.P_KDKPKNL = selectedData.KDKPKNL;
                        parInp.P_KDKPPN = selectedData.KDKPPN;
                        parInp.P_KOMPLEK = selectedData.KOMPLEK;
                        parInp.P_KONS_SIST = selectedData.KONS_SIST;
                        parInp.P_LUAS_BDG = selectedData.LUAS_BDG;
                        parInp.P_LUAS_BDGSpecified = true;
                        parInp.P_LUAS_DSR = selectedData.LUAS_DSR;
                        parInp.P_LUAS_DSRSpecified = true;
                        parInp.P_MERK = selectedData.MERK;
                        parInp.P_NO_ASET = selectedData.NO_ASET;
                        parInp.P_NO_IMB = selectedData.NO_IMB;
                        parInp.P_NO_KIB = selectedData.NO_KIB;
                        parInp.P_NO_KIBTNH = selectedData.NO_KIBTNH;
                        parInp.P_NOREG = selectedData.NOREG;
                        parInp.P_PERIODE = selectedData.PERIODE;
                        parInp.P_RPH_ASET = selectedData.RPH_ASET;
                        parInp.P_RPH_ASETSpecified = true;
                        parInp.P_RPH_MUTASI = selectedData.RPH_MUTASI;
                        parInp.P_RPH_MUTASISpecified = true;
                        parInp.P_RPH_RES = selectedData.RPH_RES;
                        parInp.P_RPH_RESSpecified = true;
                        parInp.P_RPH_SUSUT = selectedData.RPH_SUSUT;
                        parInp.P_RPH_SUSUTSpecified = true;
                        parInp.P_RPHNJOP = selectedData.RPHNJOP;
                        parInp.P_RPHNJOPSpecified = true;
                        parInp.P_RPHWAJAR = selectedData.RPHWAJAR;
                        parInp.P_RPHWAJARSpecified = true;
                        parInp.P_STAT_KELOLA = selectedData.STATUS_KELOLA;
                        parInp.P_STATUS_BMN_YN = selectedData.STATUS_BMN_YN;
                        parInp.P_TERCATAT = selectedData.TERCATAT;
                        parInp.P_TGL_GUNA = konfigApp.DateToString(selectedData.TGL_GUNA);
                        parInp.P_TGL_IMB = konfigApp.DateToString(selectedData.TGL_IMB);
                        parInp.P_TGL_REKAM = konfigApp.DateToString(selectedData.TGL_REKAM);
                        parInp.P_TGL_RENOV = konfigApp.DateToString(selectedData.TGL_RENOV);
                        parInp.P_THN_ANG = selectedData.THN_ANG;
                        parInp.P_THN_PAKAI = selectedData.THN_PAKAI;
                        parInp.P_THN_SLS = selectedData.THN_SLS;
                        parInp.P_TIPE = selectedData.TIPE;
                        parInp.P_TIPE_KBDG = selectedData.TIPE;//???????????
                        parInp.P_UMEKO = selectedData.UMEKO;
                        parInp.P_UMEKOSpecified = true;
                        parInp.P_KUANTITAS = selectedData.KUANTITAS;
                        parInp.P_KUANTITASSpecified = true;
                        #endregion

                        #region Data isi
                        parInp.P_CATATAN = this.identitas.teCatatan.Text.Trim();
                        parInp.P_KD_POS = this.identitas.teKodePos.Text.Trim();
                        parInp.P_LOKASI = this.identitas.teJalan.Text.Trim();
                        parInp.P_KOMPLEK = this.identitas.teKomplek.Text.Trim();
                        parInp.P_ALAMAT = this.identitas.teAlamat.Text.Trim();

                        #endregion


                        parInp.P_SELECT = "U";
                        this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                        svcbangunanCrud = new SvcBangunanCrud.call_pttClient(konfigApp.SvcBangunanCrud_name,konfigApp.SvcBangunanCrud_address);
                        svcbangunanCrud.Open();
                        svcbangunanCrud.Beginexecute(parInp, new AsyncCallback(crudBangunan), "");
                        
                        
                    }
                    catch (Exception E)
                    {
                        this.modeCrud = 'A';
                        try
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                            }
                            else
                            {
                                this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                                this.aktifkanForm("");
                            }
                            
                        }
                        catch 
                        {
                                                       
                        }
                        
                        MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                        //MessageBox.Show(E.ToString(), "Error");
                    }
            }
            else
            {
                MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
            }
        }

        private void crudBangunan(IAsyncResult result)
        {
            try
            {
                outDataCrud = svcbangunanCrud.Endexecute(result);
                svcbangunanCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.msg_get_database(outDataCrud.PO_RESULT, "Y", 0);
                if (String.Compare(outDataCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    
                        
                        this.Invoke(new SimpanData(this.simpanData), outDataCrud);
                  
                }
            }
            catch (Exception E)
            {
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalLain);
            }
        }

        private void crudFoto(IAsyncResult result)
        {
            try
            {
                outFotoCrud = svcAsetPhotoCrud.Endexecute(result);
                svcAsetPhotoCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                //MessageBox.Show(outFotoCrud.PO_RESULT_MESSAGE, "Pesan");
                if (String.Compare(outFotoCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    if (this.modeCrud == 'D')
                    {
                        this.Foto.Images.RemoveAt(urutan);
                        int idx = DafarFoto.FindIndex((datafoto foto) => foto.Index == urutan);
                        JumlahFoto--;
                        DafarFoto.RemoveAt(idx);
                        for (int i = 0; i < DafarFoto.Count(); i++)
                        {
                            if (DafarFoto[i].Index > urutan)
                            {
                                DafarFoto[i].Index--;
                            }
                        }
                        MessageBox.Show(konfigApp.teksBerhasilHapus, konfigApp.judulsukses);
                    }
                    else if (this.modeCrud == 'C')
                    {
                        this.Invoke(new ShowFoto(this.showFoto), outFotoCrud);
                    }

                }
                else
                {
                    if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                    {
                        konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                    }
                    else if (this.modeCrud == 'D')
                    {
                        konfigApp.teksDialog = konfigApp.teksGagalHapus;
                    }
                    else
                    {
                        konfigApp.teksDialog = konfigApp.teksGagalLain;
                    }
                    MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagal);
                }


            }
            catch (Exception E)
            {
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagal);
            }
        }

        private delegate void ShowFoto(SvcAsetPhotoCrud.OutputParameters dataOutBgn);

        public void showFoto(SvcAsetPhotoCrud.OutputParameters outFotoCrud)
        {
            int idx = Foto.Images.Add(Image.FromFile(ofdFoto.FileName));
            Foto.SetCurrentImageIndex(idx);

            Foto.SlidePrev();
            Foto.SetCurrentImageIndex(idx);
            DafarFoto.Add(new datafoto()
            {
                ID_ASET = this.ID_ASET,
                NM_PHOTO = Path.GetFileName(ofdFoto.FileName),
                Filename = Path.GetFileName(ofdFoto.FileName),
                ID_PHOTO = outFotoCrud.PO_ID_PHOTO,
                PHOTO = konfigApp.convert2byte(ofdFoto.FileName),
                Index = idx

            });

            JumlahFoto++;

        }
        private delegate void SimpanData(SvcBangunanCrud.OutputParameters dataOutBangunanCrud);

        private void simpanData(SvcBangunanCrud.OutputParameters dataOutBangunanCrud)
        {
            this.updateBangunan(dataOutBangunanCrud);
        }

        private void msg_get_database(string kode, string nilai, int status)
        {
            List<string> teks = new string[] { "Array", "teks" }.ToList();
            if (String.Compare(kode.Trim(), nilai, true) == status)
            {
                teks = konfigApp.teksBerhasil(this.modeCrud);
            }
            else
            {
                teks = konfigApp.teksGagal(this.modeCrud);
            }


            MessageBox.Show(teks[0], teks[1]);
        }
        private bool cek_input()
        {
            return true;
        }
        protected override void On_Load(object sender, EventArgs e)
        {
            
            identitas.Dock = DockStyle.Fill;
            this.TabIdentitas.Controls.Clear();
            this.TabIdentitas.Controls.Add(identitas);
            this.TabDetail.SelectedTabPage = this.TabIdentitas;
            this.TabDokumen.Text = "Dokumen";
            this.TabIdentitas.Text = "Detail";
            this.TabFasilitasPenunjang.Text = "Fasilitas";
            this.TabKonstruksi.Text = "Konstruksi";
            this.TabLokasi.Text = "Batas dan GPS";
            this.TabRiwayatBangunan.Text = "Riwayat";
            this.TabRiwayatPenilaian.Text = "Penilaian";
            this.TabPermasalahanHukum.Text = "Status Hukum";
            this.TabRiwayatMutasi.Text = "Mutasi";
            this.TabRiwayatNJOP.Text = "Dokumen NJOP";
            this.TabRiwayatPemeliharaan.Text = "Pemeliharaan";
            this.TabRiwayatPengelolaan.Text = "Pengelolaan";
            
            this.TabDaftarBangunan.Text = "Ruangan";
            this.TabPemakai.PageVisible = false;


            Foto.AnimationTime = 500;

            Foto.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            if (this.status == "edit" || this.status == "input")
            {

                this.FormReadOnly(true);
                this.identitas.teKodePos.Properties.ReadOnly = false;
                this.identitas.teKomplek.Properties.ReadOnly = false;
                this.identitas.teCatatan.Properties.ReadOnly = false;
                this.identitas.teAlamat.Properties.ReadOnly = false;
                
                
            }
            else
            {

                this.FormReadOnly(true);
              
            }
            this.Foto.Enabled = true;
            this.btnTampilkanFoto.Enabled = true;
            this.btnHapusFoto.Enabled = false;
            this.BtnUnggahFoto.Enabled = false;
            this.getInitData();
        }

        private void ReadOnly(Control c,bool state)
        {
            if (c is TextEdit)
                (c as TextEdit).Properties.ReadOnly = state;
            else if (c is SimpleButton)
                (c as SimpleButton).Enabled = !state;
            else if (c is PictureEdit)
                (c as PictureEdit).Properties.ReadOnly = state;
            else if (c is SpinEdit)
                (c as SpinEdit).Properties.ReadOnly = state;
        }
        private void FormReadOnly(bool state)
        {
            
                foreach (Control c in this.identitas.LayoutKiri.Controls)
                {
                    this.ReadOnly(c, state);
            
                }
                foreach (Control c in this.identitas.LayoutKanan.Controls)
                {
                    this.ReadOnly(c, state);
                }
                foreach (Control c in this.LayoutUAKPB.Controls)
                {
                    this.ReadOnly(c, state);
                }
            
        }



        protected override void TabDetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
           
            if (TabDetail.SelectedTabPage == this.TabDokumen && status_DokBangunan == false)
            {
                status_DokBangunan = true;
                ucdokBangunan = new ucDokBangunan(this.ID_KBDG,this.status);
                ucdokBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucdokBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucdokBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucdokBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
               
                ucdokBangunan.Dock = DockStyle.Fill;
                TabDokumen.Controls.Clear();
                TabDokumen.Controls.Add(ucdokBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabLokasi && status_LokBangunan == false)
            {
                status_LokBangunan = true;
                ucLokBangunan = new ucLokasiBangunan(this.ID_KBDG, this.status);
                ucLokBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucLokBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucLokBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucLokBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucLokBangunan.nama_aset = this.teNamaBarang.Text;
                ucLokBangunan.Dock = DockStyle.Fill;
                TabLokasi.Controls.Clear();
                TabLokasi.Controls.Add(ucLokBangunan);
            }
           
            else if (TabDetail.SelectedTabPage == this.TabDaftarBangunan && status_DetailRuangan == false)
            {
                status_DetailRuangan = true;
                ucDruanganBangunan ucdRuanganBangunan = new ucDruanganBangunan(this.ID_SATKER,this.ID_KBDG, this.status);
               
                ucdRuanganBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucdRuanganBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucdRuanganBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucdRuanganBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
             
                ucdRuanganBangunan.Dock = DockStyle.Fill;
                TabDaftarBangunan.Controls.Clear();
                TabDaftarBangunan.Controls.Add(ucdRuanganBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabFasilitasPenunjang && status_FasPenunjang == false)
            {
                status_FasPenunjang = true;
                ucFasBangunan = new ucFasBangunan(this.ID_KBDG, this.status);
                ucFasBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucFasBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucFasBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucFasBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucFasBangunan.Dock = DockStyle.Fill;
                TabFasilitasPenunjang.Controls.Clear();
                TabFasilitasPenunjang.Controls.Add(ucFasBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabKonstruksi && status_KonstruksiBangunan == false)
            {
                status_KonstruksiBangunan = true;
                ucKonstruksiBangunan ucKonsBangunan = new ucKonstruksiBangunan(this.ID_KBDG, this.status);
                ucKonsBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucKonsBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucKonsBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucKonsBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
               
                ucKonsBangunan.Dock = DockStyle.Fill;
                TabKonstruksi.Controls.Clear();
                TabKonstruksi.Controls.Add(ucKonsBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPemeliharaan && status_RiwayatPemeliharaan == false)
            {
                status_RiwayatPemeliharaan = true;
                ucrPemeliharaanBangunan = new ucRpemeliharaanBangunan(this.ID_KBDG, this.status);
                ucrPemeliharaanBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrPemeliharaanBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrPemeliharaanBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrPemeliharaanBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucrPemeliharaanBangunan.Dock = DockStyle.Fill;
                TabRiwayatPemeliharaan.Controls.Clear();
                TabRiwayatPemeliharaan.Controls.Add(ucrPemeliharaanBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPenilaian && status_RiwayatPenilaian == false)
            {
                status_RiwayatPenilaian = true;
                ucrNilaiBangunan = new ucRnilaiBangunan(this.ID_KBDG, this.status);
                ucrNilaiBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrNilaiBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrNilaiBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrNilaiBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrNilaiBangunan.Dock = DockStyle.Fill;
                TabRiwayatPenilaian.Controls.Clear();
                TabRiwayatPenilaian.Controls.Add(ucrNilaiBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatNJOP && status_NJOP == false)
            {
                status_NJOP = true;
                ucNjopBangunan ucnJopBangunan = new ucNjopBangunan(this.ID_KBDG, this.status);
                ucnJopBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucnJopBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucnJopBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucnJopBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucnJopBangunan.Dock = DockStyle.Fill;
                TabRiwayatNJOP.Controls.Clear();
                TabRiwayatNJOP.Controls.Add(ucnJopBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatMutasi && status_RiwayatMutasi == false)
            {
                status_RiwayatMutasi = true;
                ucrMutasiBangunan = new ucRmutasiBangunan(this.ID_KBDG, this.status);
                ucrMutasiBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrMutasiBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrMutasiBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrMutasiBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrMutasiBangunan.Dock = DockStyle.Fill;
                TabRiwayatMutasi.Controls.Clear();
                TabRiwayatMutasi.Controls.Add(ucrMutasiBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatBangunan && status_RiwayatBangunan == false)
            {
                status_RiwayatBangunan = true;
                ucriwayatBangunan = new ucRiwayatBangunan(this.ID_KBDG, this.status);
                ucriwayatBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucriwayatBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucriwayatBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucriwayatBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucriwayatBangunan.Dock = DockStyle.Fill;
                TabRiwayatBangunan.Controls.Clear();
                TabRiwayatBangunan.Controls.Add(ucriwayatBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPengelolaan && status_RiwayatPengguna == false)
            {
                status_RiwayatPengguna = true;
                ucrPenggunaBangunan = new ucRpenggunaBangunan(this.ID_KBDG, this.status);
                ucrPenggunaBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrPenggunaBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrPenggunaBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrPenggunaBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrPenggunaBangunan.Dock = DockStyle.Fill;
                TabRiwayatPengelolaan.Controls.Clear();
                TabRiwayatPengelolaan.Controls.Add(ucrPenggunaBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabPermasalahanHukum && status_PermasalahanHukum == false)
            {
                status_PermasalahanHukum = true;
                ucPhukumBangunan ucpHukumBangunan = new ucPhukumBangunan(this.ID_KBDG, this.status);
                ucpHukumBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucpHukumBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucpHukumBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucpHukumBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
             
                ucpHukumBangunan.Dock = DockStyle.Fill;
                TabPermasalahanHukum.Controls.Clear();
                TabPermasalahanHukum.Controls.Add(ucpHukumBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabDokumenKib && status_DokKib == false)
            {
                status_DokKib = true;
                ucKibBangunan ucKibBangunan = new ucKibBangunan(this.ID_KBDG, this.status);
                ucKibBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucKibBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucKibBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucKibBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucKibBangunan.Dock = DockStyle.Fill;
                TabDokumenKib.Controls.Clear();
                TabDokumenKib.Controls.Add(ucKibBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabSusut && status_Susut == false)
            {
                status_Susut = true;
                ucSusutBangunan ucsusutBangunan = new ucSusutBangunan(this.ID_KBDG, this.status);
                ucsusutBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucsusutBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucsusutBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucsusutBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucsusutBangunan.Dock = DockStyle.Fill;
                TabSusut.Controls.Clear();
                TabSusut.Controls.Add(ucsusutBangunan);
            }
            else if (TabDetail.SelectedTabPage == this.TabSpm && status_Spm == false)
            {
                status_Spm = true;
                ucSusutBangunanSpm ucsusutBangunanSpm = new ucSusutBangunanSpm(this.ID_KBDG, this.status);
                ucsusutBangunanSpm.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucsusutBangunanSpm.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucsusutBangunanSpm.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucsusutBangunanSpm.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucsusutBangunanSpm.Dock = DockStyle.Fill;
                TabSpm.Controls.Clear();
                TabSpm.Controls.Add(ucsusutBangunanSpm);
            }
            else if (TabDetail.SelectedTabPage == this.TabFoto && status_Foto == false)
            {
                status_Foto = true;
                ucBGFoto ucBGFoto = new ucBGFoto(this.ID_ASET, this.status);
                ucBGFoto.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucBGFoto.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucBGFoto.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucBGFoto.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucBGFoto.Dock = DockStyle.Fill;
                TabFoto.Controls.Clear();
                TabFoto.Controls.Add(ucBGFoto);
            }
            else if (TabDetail.SelectedTabPage == this.TabAsuransi && status_Asuransi == false)
            {
                status_Asuransi = true;
                ucAsuransiBangunan ucasuransiBangunan = new ucAsuransiBangunan(this.ID_KBDG, this.status);
                ucasuransiBangunan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucasuransiBangunan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucasuransiBangunan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucasuransiBangunan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucasuransiBangunan.Dock = DockStyle.Fill;
                TabAsuransi.Controls.Clear();
                TabAsuransi.Controls.Add(ucasuransiBangunan);
            }
            
        }

        private void btnSatker_Click(object sender, EventArgs e)
        {
            PuSatker = new FrmPUSatker();
            PuSatker.ambilSatker = new AmbilSatker(this.ambilSatker);
            PuSatker.ShowDialog();
        }

        private void ambilSatker(decimal?id, string kode, string nama)
        {
            this.ID_SATKER = id;
            this.teKdUAKPB.Text = kode;
            this.teUrUAKPB.Text = nama;
        }

        private void CariSatkerPengguna_Click(object sender, EventArgs e)
        {
            PuSatker = new FrmPUSatker();
            PuSatker.ambilSatker = new AmbilSatker(this.ambilSatkerPMK);
            PuSatker.ShowDialog();
        }

        private void ambilSatkerPMK(decimal? id, string kode, string nama)
        {
            this.ID_SATKER_PMK = id;
            //this.teKdPenggunaBangunan.Text = kode;
            //this.teNamaPenggunaBangunan.Text = nama;
        }

        private void btKondisi_Click(object sender, EventArgs e)
        {

            PuKondisi = new FrmPUKondisi();
            PuKondisi.ambilKondisi = new AmbilKondisi(this.ambilKondisi);
            PuKondisi.ShowDialog();
        }

        private void ambilKondisi(string idData, string dataName)
        {
            this.KD_KONDISI = idData;
            this.teKondisi.Text = dataName;
        }

        private void btnCariSskel_Click(object sender, EventArgs e)
        {
            PuSskel = new FrmPUSskel();
            PuSskel.ambilSskel = new AmbilSskel(this.ambilSskel);
            PuSskel.ShowDialog();
        }

        private void ambilSskel(string idData, string dataName)
        {
            teKdBrg.Text = idData;
            this.teNamaBarang.Text = dataName;
        }

        private void btnKota_Click(object sender, EventArgs e)
        {
            FrmPUKab PuKab = new FrmPUKab();
            PuKab.ambilKabupaten = new AmbilKabupaten(this.ambilKabupaten);
            PuKab.ShowDialog();
        }

        private void ambilKabupaten(string idData, string dataName)
        {
            this.KD_KAB = idData;
            //this.teKab.Text = dataName;
        }

        private void btnProvinsi_Click(object sender, EventArgs e)
        {
            FrmPUProv PuProv = new FrmPUProv();
            PuProv.ambilProvinsi = new AmbilProvinsi(this.ambilProvinsi);
            PuProv.ShowDialog();
        }

        private void ambilProvinsi(string idData, string dataName)
        {
            this.KD_PROV = idData;
            //this.teProvinsi.Text = dataName;
        }

        private void btnJenisPengguna_Click(object sender, EventArgs e)
        {
            FrmPUSatker PuKl = new FrmPUSatker();
            PuKl.ambilSatker = new AmbilSatker(this.ambilJenisPengguna);
            PuKl.ShowDialog();
        }

        private void ambilJenisPengguna(decimal? ID_SATKER, string kdSatker , string dataName)
        {
            this.ID_SATKER_PMK = ID_SATKER;
            //this.teJenisPengguna.Text = dataName;
        }

        private void ambilStatus(string idData, string dataName)
        {
            this.KD_STATUS = idData;
            //this.teStatusPengguna.Text = dataName;
        }

     

        private void ambilTanah(SvcAssetTanahSelect.BPSIMANSROW_M_KTNH data)
        {
            this.ID_KTNH = data.ID_KTNH;
            //this.teJenisPemilik.Text = data.JNS_PMK;
            //this.teKdPengguna.Text = data.KD_SATKER;
            //this.teNamaPengguna.Text = data.UR_SATKER;
            //this.teKdBrg.Text = data.KD_BRG;
            //this.teNUPbrg.Text = data.NO_ASET.ToString();
            //this.teKIBbrg.Text = data.NO_KIB;
            //this.teNamaBarang.Text = data.UR_SSKEL;
            //this.teNoDokumen.Text = data.KD_JNS_SERTI;
        }





        protected override void BtnUnggahFoto_Click(object sender, EventArgs e)
        {

            ofdFoto.InitialDirectory = "C:";
            ofdFoto.Filter = "(*.bmp, *.jpg, *.gif, *.png)|*.bmp;*.jpg;*.gif;*.png";
            ofdFoto.Multiselect = false;
            if (ofdFoto.ShowDialog() == DialogResult.OK)
            {
                var size = new FileInfo(ofdFoto.FileName).Length;
                if (size > konfigApp.maksSizeFoto)
                {
                    MessageBox.Show(konfigApp.konfirmasiMaksimalFoto, konfigApp.judulKonfirmasi);
                    return;
                }
                if (JumlahFoto < konfigApp.maksFoto)
                {

                    try
                    {
                        this.nonAktifForm("");
                        myThread = new Thread(new ThreadStart(ShowProgresBar));
                        myThread.Start();
                        SvcAsetPhotoCrud.InputParameters parfoto = new SvcAsetPhotoCrud.InputParameters();
                        parfoto.P_ID_ASET = this.ID_ASET;
                        parfoto.P_ID_ASETSpecified = true;
                        parfoto.P_PHOTO = konfigApp.convert2byte(ofdFoto.FileName);

                        parfoto.P_ID_PHOTOSpecified = true;
                        parfoto.P_KET_PHOTO = "keterangan";
                        parfoto.P_NM_PHOTO = Path.GetFileName(ofdFoto.FileName);

                        parfoto.P_SELECT = "C";
                        this.modeCrud = Convert.ToChar(parfoto.P_SELECT);
                        svcAsetPhotoCrud = new SvcAsetPhotoCrud.call_pttClient(konfigApp.SvcAsetPhotoCrud_name, konfigApp.SvcAsetPhotoCrud_address);
                        svcAsetPhotoCrud.Open();
                        svcAsetPhotoCrud.Beginexecute(parfoto, new AsyncCallback(crudFoto), "");
                    }
                    catch (Exception)
                    {
                        this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                        MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                    }
                }
                else
                {
                    MessageBox.Show(konfigApp.konfirmasiMaksimalFoto, konfigApp.judulKonfirmasi);
                }
            }
            

            
        }







        protected override void BtnTampilkanFoto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(konfigApp.konfirmasiAmbilFoto, konfigApp.judulKonfirmasi,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.getInitPhoto();
            }
        }

        #region load data Photo
        protected void getInitPhoto(int max=0)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");

            parInpPhoto = new SvcAsetPhotoSelect.InputParameters();
            parInpPhoto.P_COL = "TANGGAL";
            if (max != 0)
            {
                parInpPhoto.P_MAX = max;
            }
            else
            {
                parInpPhoto.P_MAX = konfigApp.maksFoto;
            }
            parInpPhoto.P_MAXSpecified = true;
            parInpPhoto.P_MIN = 0;
            parInpPhoto.P_MINSpecified = true;
            parInpPhoto.P_SORT = "DESC";
            parInpPhoto.STR_WHERE = "ID_ASET = " + this.ID_ASET + " ";
            fetchDataPhoto = new SvcAsetPhotoSelect.call_pttClient(konfigApp.SvcAsetPhotoSelect_name, konfigApp.SvcAsetPhotoSelect_address);
            fetchDataPhoto.Open();
            fetchDataPhoto.Beginexecute(parInpPhoto, new AsyncCallback(this.getResultPhoto), null);
        }

        protected void getResultPhoto(IAsyncResult result)
        {
            try
            {
                this.outDataPhoto = fetchDataPhoto.Endexecute(result);
                fetchDataPhoto.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ShowDataPhoto(this.showDataPhoto), this.outDataPhoto);
            }
            catch
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataPhoto(SvcAsetPhotoSelect.OutputParameters dataOutBgn);

        public void showDataPhoto(SvcAsetPhotoSelect.OutputParameters serviceOutPut)
        {
            if (this.mulai == true || this.status == "detail")
            {
                
                this.mulai = false;
                this.btnHapusFoto.Enabled = false;
                this.BtnUnggahFoto.Enabled = false;
            }else
            {
                this.btnHapusFoto.Enabled = true;
                this.BtnUnggahFoto.Enabled = true;
            }
            Foto.Images.Clear();
            DafarFotoDb = new SvcAsetPhotoSelect.OutputParameters();
            DafarFotoDb = serviceOutPut;
            JumlahFoto = 0;
            DafarFoto.Clear();
            datafoto data = new datafoto();
            int idx;
            int jmlDataPhoto = serviceOutPut.SF_ROW_M_ASET_PHOTO.Count();
            for (int i = 0; i < serviceOutPut.SF_ROW_M_ASET_PHOTO.Count(); i++)
            {
                if (serviceOutPut.SF_ROW_M_ASET_PHOTO[i].PHOTO.Length != 0)
                {

                    idx = Foto.Images.Add(konfigApp.convert2bytmap(serviceOutPut.SF_ROW_M_ASET_PHOTO[i].PHOTO));


                    DafarFoto.Add(new datafoto()
                    {
                        ID_ASET = serviceOutPut.SF_ROW_M_ASET_PHOTO[i].ID_ASET,
                        NM_PHOTO = serviceOutPut.SF_ROW_M_ASET_PHOTO[i].NM_PHOTO,
                        KET_PHOTO = serviceOutPut.SF_ROW_M_ASET_PHOTO[i].KET_PHOTO,
                        ID_PHOTO = serviceOutPut.SF_ROW_M_ASET_PHOTO[i].ID_PHOTO,
                        PHOTO = serviceOutPut.SF_ROW_M_ASET_PHOTO[i].PHOTO,
                        Index = idx

                    });

                    JumlahFoto++;
                }

            }


        }
        #endregion

        protected override void BtnHapusFoto_Click(object sender, EventArgs e)
        {

            urutan = Foto.GetCurrentImageIndex();
            int idx = DafarFoto.Count();
            idx = DafarFoto.FindIndex((datafoto foto) => foto.Index == urutan);
            if (idx >= 0)
            {
                if (MessageBox.Show(konfigApp.konfirmasiHapusFoto, konfigApp.judulHapusData,
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {



                    this.nonAktifForm("");
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();
                    SvcAsetPhotoCrud.InputParameters parfoto = new SvcAsetPhotoCrud.InputParameters();
                    parfoto.P_ID_ASET = DafarFoto[idx].ID_ASET;
                    parfoto.P_ID_ASETSpecified = true;
                    parfoto.P_PHOTO = DafarFoto[idx].PHOTO;

                    parfoto.P_ID_PHOTO = DafarFoto[idx].ID_PHOTO;
                    parfoto.P_ID_PHOTOSpecified = true;
                    parfoto.P_KET_PHOTO = DafarFoto[idx].KET_PHOTO;
                    parfoto.P_NM_PHOTO = DafarFoto[idx].NM_PHOTO;
                    parfoto.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parfoto.P_SELECT);
                    svcAsetPhotoCrud = new SvcAsetPhotoCrud.call_pttClient(konfigApp.SvcAsetPhotoCrud_name, konfigApp.SvcAsetPhotoCrud_address);
                    svcAsetPhotoCrud.Open();
                    svcAsetPhotoCrud.Beginexecute(parfoto, new AsyncCallback(crudFoto), "");

                }
            }
           
        }

        private int InArray(SvcAsetPhotoSelect.OutputParameters array, int idx)
        {
            int hasil = -1;
            try
            {
                for (int i = 0; i < array.SF_ROW_M_ASET_PHOTO.Count(); i++)
                {
                    if (array.SF_ROW_M_ASET_PHOTO[i].NUM == idx)
                    {
                        hasil = i;
                        break;
                    }
                }
            }
            catch (Exception)
            {

                hasil = -1;
            }
            
            return hasil;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //-------------- FOTO ---------------
           
            urutan = Foto.GetCurrentImageIndex();
            int idx = InArray(DafarFotoDb, urutan);
            if (idx < 0)
            {
                try 
	            {	        
		            this.nonAktifForm("");
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();
                    SvcAsetPhotoCrud.InputParameters parfoto = new SvcAsetPhotoCrud.InputParameters();
                    parfoto.P_ID_ASET = this.ID_ASET;
                    parfoto.P_ID_ASETSpecified = true;
                    parfoto.P_PHOTO = konfigApp.convert2byte(DafarFoto[urutan].Filename);

                    parfoto.P_ID_PHOTOSpecified = true;
                    parfoto.P_KET_PHOTO = "keterangan";
                    parfoto.P_NM_PHOTO = Path.GetFileName(DafarFoto[urutan].Filename);

                    parfoto.P_SELECT = "C";
                    this.modeCrud = Convert.ToChar(parfoto.P_SELECT);
                    svcAsetPhotoCrud = new SvcAsetPhotoCrud.call_pttClient(konfigApp.SvcAsetPhotoCrud_name, konfigApp.SvcAsetPhotoCrud_address);
                    svcAsetPhotoCrud.Open();
                    svcAsetPhotoCrud.Beginexecute(parfoto, new AsyncCallback(crudFoto), "");
	            }
	            catch (Exception)
	            {
		            this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
	            }
                
            }
                

        }

        private void Foto_CurrentImageIndexChanged(object sender, DevExpress.XtraEditors.Controls.ImageSliderCurrentImageIndexChangedEventArgs e)
        {
            // MessageBox.Show(Foto.GetCurrentImageIndex().ToString());
        }

       
        //====================================== lihat kib ===========================================================
        private void getDataKib()
        {
            ucKibBangunan ucKibBangunan = new ucKibBangunan(this.ID_KBDG, this.status);
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInpKib = new SvcKibBangunanSelect.InputParameters();
            parInpKib.P_COL = "";

            parInpKib.P_MAX = 1;
            parInpKib.P_MAXSpecified = true;
            parInpKib.P_MIN = 0;
            parInpKib.P_MINSpecified = true;
            parInpKib.P_SORT = "DESC";
            parInpKib.STR_WHERE = String.Format(" ID_KBDG = {0} {1}", this.ID_KBDG, "");
            parInpKib.P_COUNT = "Y";
            Console.WriteLine(parInpKib.STR_WHERE);
            fetchDataKib = new SvcKibBangunanSelect.call_pttClient();
            fetchDataKib.Open();
            fetchDataKib.Beginexecute(parInpKib, new AsyncCallback(this.getResultKib), null);
        }

        protected void getResultKib(IAsyncResult result)
        {
            try
            {
                this.outDataKib = fetchDataKib.Endexecute(result);
                fetchDataKib.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowData(this.showData), this.outDataKib);
            }
            catch (Exception ex)
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show("Load data Dokumen KIB gagal", konfigApp.judulGagalAmbil);
            }
        }
        private delegate void ShowData(SvcKibBangunanSelect.OutputParameters dataOut);
        public void showData(SvcKibBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBDG_DOK_KIB.Count();
            if (jmlDataGroup <= 0)
            {
                MessageBox.Show("Dokumen tidak ada", "Perhatian!!!");
            }
            else
            {
                decimal? idDokumen = this.outDataKib.SF_ROW_M_KBDG_DOK_KIB[0].ID_KBDG_DOK_KIB;
                try
                {
                    myThread_ = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread_.Start();
                    SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                    parInp.P_ID = idDokumen;
                    parInp.P_ID_TABLE = "ID_KBDG_DOK_KIB";
                    parInp.P_IDSpecified = true;
                    parInp.P_TABLE = "M_KBDG_DOK_KIB";
                    svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient();
                    svcAsetGetDokSelect.Open();
                    svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
                }
                catch (Exception E)
                {
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                    //MessageBox.Show(E.Message);
                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }
            }
        }
        

        protected override void sbLihatKib_Click(object sender, EventArgs e)
        {
            this.getDataKib();
            
        }


 


        #region View Dokumen KIB

        private void getResultDok(IAsyncResult result)
        {
            try
            {
                this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
                svcAsetGetDokSelect.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowFileDok(this.showFileDok), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowFileDok(SvcAsetGetDokSelect.OutputParameters dataOut);

        public void showFileDok(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlDataGroup > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
                System.IO.File.WriteAllBytes(this.outDataKib.SF_ROW_M_KBDG_DOK_KIB[0].NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(this.outDataKib.SF_ROW_M_KBDG_DOK_KIB[0].NMFILE);
                PuPdf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Dokumen KIB terakhir tidak ada", "Perhatian!!");
            }
        }



        //====================================== lihat dokumen ====================================================
        private void getDataDok()
        {
            ucdokBangunan = new ucDokBangunan(this.ID_KBDG, this.status);
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInpDok = new SvcDokBangunanSelect.InputParameters();
            parInpDok.P_COL = "";

            parInpDok.P_MAX = 1;
            parInpDok.P_MAXSpecified = true;
            parInpDok.P_MIN = 0;
            parInpDok.P_MINSpecified = true;
            parInpDok.P_SORT = "DESC";
            parInpDok.STR_WHERE = String.Format(" ID_KBDG = {0} {1}", this.ID_KBDG, "");
            parInpDok.P_COUNT = "Y";
            Console.WriteLine(parInpDok.STR_WHERE);
            fetchDataDok = new SvcDokBangunanSelect.call_pttClient();
            fetchDataDok.Open();
            fetchDataDok.Beginexecute(parInpDok, new AsyncCallback(this.getResultDokumen), null);
        }

        protected void getResultDokumen(IAsyncResult result)
        {
            try
            {
                this.outDatDok = fetchDataDok.Endexecute(result);
                fetchDataKib.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowDataDok(this.showDataDok), this.outDatDok);
            }
            catch (Exception ex)
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show("Load data Dokumen gagal", konfigApp.judulGagalAmbil);
            }
        }
        private delegate void ShowDataDok(SvcDokBangunanSelect.OutputParameters dataOut);
        public void showDataDok(SvcDokBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBDG_DOK.Count();
            if (jmlDataGroup <= 0)
            {
                MessageBox.Show("Dokumen tidak ada", "Perhatian!!!");
            }
            else
            {
                decimal? idDokumen = this.outDatDok.SF_ROW_M_KBDG_DOK[0].ID_KBDG_DOK;
                try
                {
                    myThread_ = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread_.Start();
                    SvcAsetGetDokSelect.InputParameters parInpDok = new SvcAsetGetDokSelect.InputParameters();
                    parInpDok.P_ID = idDokumen;
                    parInpDok.P_ID_TABLE = "ID_KBDG_DOK";
                    parInpDok.P_IDSpecified = true;
                    parInpDok.P_TABLE = "M_KBDG_DOK";
                    svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient();
                    svcAsetGetDokSelect.Open();
                    svcAsetGetDokSelect.Beginexecute(parInpDok, new AsyncCallback(this.getResultDoku), null);
                }
                catch (Exception E)
                {
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                    //MessageBox.Show(E.Message);
                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }
            }
        }

        protected override void sbLihatDok_Click(object sender, EventArgs e)
        {
            this.getDataDok();

        }

        #endregion//ViewDokumen

        #region View Dokumen

        private void getResultDoku(IAsyncResult result)
        {
            try
            {
                this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
                svcAsetGetDokSelect.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowFileDok(this.showFileDoku), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowFileDoku(SvcAsetGetDokSelect.OutputParameters dataOut);

        public void showFileDoku(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlDataGroup > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
                System.IO.File.WriteAllBytes(this.outDatDok.SF_ROW_M_KBDG_DOK[0].NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(this.outDatDok.SF_ROW_M_KBDG_DOK[0].NMFILE);
                PuPdf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Dokumen terakhir tidak ada", "Perhatian!!");
            }
        }


        #endregion//ViewDokumen

        //---------------------------------------------- Laporan KIB Gedung -----------------------------------------

        protected override void sbLapKIB_Click(object sender, EventArgs e) 
        {
          upg1206 = new UPG1206();
          upg1206.bsUPG1206.DataSource = this.selectedData;
          upg1206.bsFotoUPG1206.DataSource = this.DafarFoto;  
          
          ReportPrintTool pt;
          pt = new ReportPrintTool(upg1206);
          pt.AutoShowParametersPanel = true;
          pt.ShowPreviewDialog();
        }
    }
}
