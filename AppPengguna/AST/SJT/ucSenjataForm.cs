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

namespace AppPengguna.AST.SJT
{
    public delegate void UpdateSenjata(SvcSenjataCrud.OutputParameters dataOutPropKhususCrud);
    public  class ucSenjataForm : ucDetailAsetForm
    {
      
        public ucIdentitasSenjata identitas = new ucIdentitasSenjata();
        private SvcSenjataSelect.BPSIMANSROW_KSENJ selectedData;
    
        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public UpdateSenjata updateSenjata;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        public ProgBar progressbar;
        

        private Thread myThread;
        private char modeCrud;
        private string KD_KONDISI;
        private string KD_KAB;
        private string KD_PROV;
        private decimal? ID_SATKER;
        private decimal? ID_KSENJ;
        
        private decimal? ID_SATKER_PMK;
        private string KD_STATUS;
        private decimal? ID_KANWIL;
        private decimal? ID_KORWIL;
        private decimal? ID_ESELON1;
        private decimal? ID_KL;
        private decimal? ID_PENGADAAN;
        
        private int ulang = 0;
        private SvcSenjataCrud.call_pttClient svcJlnJmbtnCrud;
        private SvcSenjataCrud.OutputParameters outDataCrud;
        private SvcSenjataPerlengkapanSelect.call_pttClient fetchDataPerlengkapan;
        private SvcSenjataPerlengkapanSelect.OutputParameters outDataPerlengkapanSelect;

        //------------- REPORT ------------------------------------
        UPG1210 upg1210;
        //---------------------------------------------------------

        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        private SvcSenjataRkibSelect.call_pttClient fetchDataKib;
        private SvcSenjataRkibSelect.InputParameters parInpKib;
        private SvcSenjataRkibSelect.OutputParameters outDataKib;
        public SvcSenjataRkibSelect.BPSIMANSROW_M_KSENJ_DOK_KIB selectedDataKib;
        //-------------------------------------------------------
    

        #region PopUp Form
        private FrmPUSatker PuSatker;
        private FrmPUKondisi PuKondisi;
        private FrmPUSskel PuSskel;
        #endregion

        #region Variable Aktif Tab
        private bool status_FasPenunjang;
        private bool status_Pemakai;
        private bool status_Perlengkapan;
        private bool status_GPS;
        private bool status_DetailRuangan;
        private bool status_KonstruksiPropKhusus;
        private bool status_RiwayatPenilaian;
        private bool status_NJOP;
        private bool status_RiwayatPropKhusus;
        private bool status_RiwayatPengguna;
        private bool status_RiwayatMutasi;
        private bool status_RiwayatPemeliharaan;
        private bool status_PermasalahanHukum;
        private bool status_DokKib;
        private bool status_Susut;
        private bool status_Spm;
        #endregion

     

        public ucSenjataForm(string status)
        {
           
            this.status = status;
          
        }

        public bool IsiForm(SvcSenjataSelect.BPSIMANSROW_KSENJ Data)
        {
            this.selectedData = Data;
            try
            {
                this.ID_SATKER = selectedData.ID_SATKER;
                this.ID_KSENJ = selectedData.ID_KSENJ;
                this.ID_ASET = selectedData.ID_ASET;
               
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

               this.identitas.teKaliber.Text = selectedData.KALIBER;
               this.identitas.teNoPabrik.Text = selectedData.NO_PABRIK;
               this.identitas.teTahunBuat.Text = selectedData.THN_BUAT;
               this.identitas.teNamaPabrik.Text = selectedData.NAMA_PABRIK;
               this.identitas.teNegaraPembuat.Text = selectedData.NEGARA_PEMBUAT;
               this.identitas.teSurat.Text = selectedData.SURAT;
               this.identitas.teTanggalSurat.Text =konfigApp.DateToString(selectedData.TGL_SURAT);
               // //-----------------  IMB -----------------------------------
              
              // this.identitas.teTahunDigunakan.Text = selectedData.THN_PAKAI;
                //this.identitas.teTahunSelesai.Text = selectedData.THN_SLS;
               //this.identitas.teTanggalRenovasi.Text = konfigApp.DateToString(selectedData.TGL_RENOV);
               // //---------- KODE -------------------
               this.KD_STATUS = selectedData.KD_STATUS;
               this.identitas.teStatusPengguna.Text = selectedData.UR_STATUS;
               this.identitas.teJenisPengguna.Text = selectedData.JNS_PMK;
               // //---------- KODE -------------------
               this.ID_SATKER_PMK = selectedData.ID_SATKER_PENGGUNA;
               this.identitas.teKodePengguna.Text = selectedData.KD_SATKER_PENGGUNA;
               this.identitas.teNamaPengguna.Text = selectedData.UR_SATKER_PENGGUNA;
               this.identitas.teStatusPengelolaan.Text = selectedData.STATUS_KELOLA;

  
               this.identitas.teCatatan.Text = selectedData.CATATAN;


               // #endregion

               // #region layout Kanan
              

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

                this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Always);

                SvcSenjataPerlengkapanSelect.InputParameters parInp = new SvcSenjataPerlengkapanSelect.InputParameters();
                parInp.P_MAX = 1;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "DESC";
                parInp.STR_WHERE = "ID_KSENJ = " + this.ID_KSENJ;
                fetchDataPerlengkapan = new SvcSenjataPerlengkapanSelect.call_pttClient(konfigApp.SvcSenjataPerlengkapanSelect_name, konfigApp.SvcSenjataPerlengkapanSelect_address);
                fetchDataPerlengkapan.Open();
                fetchDataPerlengkapan.Beginexecute(parInp, new AsyncCallback(this.getResultPerlengkapan), null);

            
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
        protected void getResultPerlengkapan(IAsyncResult result)
        {
            try
            {
                this.outDataPerlengkapanSelect = fetchDataPerlengkapan.Endexecute(result);
                fetchDataPerlengkapan.Close();
                if (this.InvokeRequired)
                {
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                    this.Invoke(new ShowDataTanah(this.showDataTanah), this.outDataPerlengkapanSelect);
                }
                else
                {
                    this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                    this.aktifkanForm("");
                    this.showDataTanah(this.outDataPerlengkapanSelect);
                }
                
            }
            catch (Exception E)
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
            
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataTanah(SvcSenjataPerlengkapanSelect.OutputParameters dataOut);

        public void showDataTanah(SvcSenjataPerlengkapanSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_KSENJ_PERLENGKAP.Count();

            for (int i = 0; i < jmlData; i++)
            {
                this.identitas.tePerlengkapan.Items.Add(serviceOutPut.SF_ROW_KSENJ_PERLENGKAP[i].NM_PERLENGKAP);
            }

            if (mulai == true)
            {

                this.getPemakai();
            }
            
        }
        private SvcSenjataRpemakaiSelect.call_pttClient fetchDataPemakai;
        private SvcSenjataRpemakaiSelect.OutputParameters outDatPemakai;
        private void getPemakai()
        {
            SvcSenjataRpemakaiSelect.InputParameters parInp = new SvcSenjataRpemakaiSelect.InputParameters();
            parInp.P_MAX = 1;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = 0;
            parInp.P_MINSpecified = true;
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_KSENJ = " + this.ID_KSENJ;
            fetchDataPemakai = new SvcSenjataRpemakaiSelect.call_pttClient();
            fetchDataPemakai.Open();
            fetchDataPemakai.Beginexecute(parInp, new AsyncCallback(this.getResultPemakai), null);
        }

        protected void getResultPemakai(IAsyncResult result)
        {
            try
            {
                this.outDatPemakai = fetchDataPemakai.Endexecute(result);
                fetchDataPemakai.Close();
                if (this.InvokeRequired)
                {
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                    this.Invoke(new ShowDataPemakai(this.showDataPemakai), this.outDatPemakai);
                }
                else
                {
                    this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                    this.aktifkanForm("");
                    this.showDataPemakai(this.outDatPemakai);
                }

            }
            catch (Exception E)
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

                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataPemakai(SvcSenjataRpemakaiSelect.OutputParameters dataOut);

        public void showDataPemakai(SvcSenjataRpemakaiSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI.Count();

            for (int i = 0; i < jmlData; i++)
            {
                this.identitas.teNIPPemakai.Text = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].NIP_PMK;
                this.identitas.teNamaPemakai.Text = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].NM_PMK;
                this.identitas.teKodeUnitPemakai.Text = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].KD_SATKER;
                this.identitas.teNamaUnitPemakai.Text = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].UR_SATKER;
                this.identitas.teAlamatPemakai.Text = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].ALM_PMK;
            }

            if (mulai == true)
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
                        SvcSenjataCrud.InputParameters parInp = new SvcSenjataCrud.InputParameters();

                        

                        #region Data yang tidak dapat diubah
                        parInp.P_CATATAN = selectedData.CATATAN;
                        parInp.P_FLAG_KOR = selectedData.FLAG_KOR;
                        parInp.P_FLAG_KRM = selectedData.FLAG_KRM;
                        parInp.P_FLAG_TTP = selectedData.FLAG_TTP;
                        parInp.P_ID_ASET = selectedData.ID_ASET;
                      
                        parInp.P_ID_ASETSpecified = true;
                        parInp.P_ID_PENGADAAN = selectedData.ID_PENGADAAN;
                        parInp.P_ID_PENGADAANSpecified = true;
                        parInp.P_ID_SATKER = selectedData.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_KALIBER = selectedData.KALIBER;
                      

                        parInp.P_KD_BRG = selectedData.KD_BRG;
                        parInp.P_KD_DATA = selectedData.KD_DATA;
                        parInp.P_KD_DSR_HRG = selectedData.KD_DSR_HRG;
                        parInp.P_KD_JNS_BMN = selectedData.KD_JNS_BMN;
                        parInp.P_KD_JNS_BMNSpecified = true;
                        parInp.P_LOKASI = selectedData.LOKASI;
                       
                        parInp.P_KD_KONDISI = selectedData.KD_KONDISI;
                        parInp.P_KD_STATUS = selectedData.KD_STATUS;
                        parInp.P_KDBAPEL = selectedData.KDBAPEL;
                        parInp.P_KDBLU = selectedData.KDBLU;
                        parInp.P_KDKPKNL = selectedData.KDKPKNL;
                        parInp.P_KDKPPN = selectedData.KDKPPN;
                        parInp.P_NAMA = selectedData.NAMA;
                        
                        parInp.P_NO_ASET = selectedData.NO_ASET;
                        parInp.P_NO_KIB = selectedData.NO_KIB;
                        parInp.P_MERK = selectedData.MERK;
                        parInp.P_NO_PABRIK = selectedData.NO_PABRIK;
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
                        parInp.P_SURAT = selectedData.SURAT;
                        parInp.P_STATUS_BMN_YN = selectedData.STATUS_BMN_YN;
                        parInp.P_TERCATAT = selectedData.TERCATAT;
                        parInp.P_TGL_REKAM = konfigApp.DateToString(selectedData.TGL_REKAM);
                        parInp.P_TGL_SURAT = konfigApp.DateToString(selectedData.TGL_SURAT);
                        parInp.P_THN_ANG = selectedData.THN_ANG;
                       
                        parInp.P_TIPE = selectedData.TIPE;
                        parInp.P_UMEKO = selectedData.UMEKO;
                        parInp.P_UMEKOSpecified = true;
                        parInp.P_KUANTITAS = selectedData.KUANTITAS;
                        parInp.P_KUANTITASSpecified = true;
                        #endregion

                        #region Data isi
                        parInp.P_CATATAN = this.identitas.teCatatan.Text.Trim();
                        parInp.P_NAMA_PABRIK = this.identitas.teNamaPabrik.Text.Trim();
                        parInp.P_NEGARA_PEMBUAT = this.identitas.teNegaraPembuat.Text.Trim();


                        #endregion


                        parInp.P_SELECT = "U";
                        this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                        svcJlnJmbtnCrud = new SvcSenjataCrud.call_pttClient();
                        svcJlnJmbtnCrud.Open();
                        svcJlnJmbtnCrud.Beginexecute(parInp, new AsyncCallback(crudPropKhusus), "");
                        
                        
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

        private void crudPropKhusus(IAsyncResult result)
        {
            try
            {
                outDataCrud = svcJlnJmbtnCrud.Endexecute(result);
                svcJlnJmbtnCrud.Close();
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
        private delegate void SimpanData(SvcSenjataCrud.OutputParameters dataOutPropKhususCrud);

        private void simpanData(SvcSenjataCrud.OutputParameters dataOutPropKhususCrud)
        {
            this.updateSenjata(dataOutPropKhususCrud);
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
            this.TabDokumen.PageVisible = false;
            this.TabIdentitas.Text = "Detail";
            this.TabFasilitasPenunjang.PageVisible = false;
            this.TabKonstruksi.PageVisible = false;
            this.TabLokasi.Text = "Perlengkapan";
            this.TabRiwayatBangunan.PageVisible = false;
            this.TabRiwayatPenilaian.Text = "Penilaian";
            this.TabPermasalahanHukum.PageVisible = false;
            this.TabRiwayatMutasi.Text = "Mutasi";
            this.TabRiwayatNJOP.PageVisible = false;
            this.TabRiwayatPemeliharaan.Text = "Pemeliharaan";
            this.TabRiwayatPengelolaan.Text = "Pengelolaan";
            this.TabAsuransi.PageVisible = false;
            this.TabDaftarBangunan.PageVisible = false;
            this.TabPemakai.Text = "Pemakai";
            this.sbLihatDok.Visible = false;
            this.sbLihatDok.Enabled = false;

            Foto.AnimationTime = 500;

            Foto.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            if (this.status == "edit" || this.status == "input")
            {

                this.FormReadOnly(true);

                this.identitas.teNamaPabrik.Properties.ReadOnly = false;
                this.identitas.teNegaraPembuat.Properties.ReadOnly = false;
            }
            else
            {

                this.FormReadOnly(true);
              
            }
            this.btnTampilkanFoto.Enabled = true;
            this.Foto.Enabled = true;
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
           
            if (TabDetail.SelectedTabPage == this.TabLokasi && status_Perlengkapan == false)
            {
                status_Perlengkapan = true;
                ucSenjataPerlengkapan ucsenjataPerlengkapan = new ucSenjataPerlengkapan(this.ID_KSENJ, this.status);
                ucsenjataPerlengkapan.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucsenjataPerlengkapan.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucsenjataPerlengkapan.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucsenjataPerlengkapan.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucsenjataPerlengkapan.Dock = DockStyle.Fill;
                TabLokasi.Controls.Clear();
                TabLokasi.Controls.Add(ucsenjataPerlengkapan);
            }
          
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPemeliharaan && status_RiwayatPemeliharaan == false)
            {
                status_RiwayatPemeliharaan = true;
                ucRpemeliharaanSenjata ucrPemeliharaanSenjata = new ucRpemeliharaanSenjata(this.ID_KSENJ, this.status);
                ucrPemeliharaanSenjata.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrPemeliharaanSenjata.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrPemeliharaanSenjata.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrPemeliharaanSenjata.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrPemeliharaanSenjata.Dock = DockStyle.Fill;
                TabRiwayatPemeliharaan.Controls.Clear();
                TabRiwayatPemeliharaan.Controls.Add(ucrPemeliharaanSenjata);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPenilaian && status_RiwayatPenilaian == false)
            {
                status_RiwayatPenilaian = true;
                ucRnilaiSenjata ucrNilaiSenjata = new ucRnilaiSenjata(this.ID_KSENJ, this.status);
                ucrNilaiSenjata.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrNilaiSenjata.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrNilaiSenjata.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrNilaiSenjata.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrNilaiSenjata.Dock = DockStyle.Fill;
               
                TabRiwayatPenilaian.Controls.Clear();
                TabRiwayatPenilaian.Controls.Add(ucrNilaiSenjata);
            }
            
            else if (TabDetail.SelectedTabPage == this.TabRiwayatMutasi && status_RiwayatMutasi == false)
            {
                status_RiwayatMutasi = true;
                ucRmutasiSenjata ucrMutasiSenjata = new ucRmutasiSenjata(this.ID_KSENJ, this.status);
                ucrMutasiSenjata.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrMutasiSenjata.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrMutasiSenjata.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrMutasiSenjata.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrMutasiSenjata.Dock = DockStyle.Fill;
                TabRiwayatMutasi.Controls.Clear();
                TabRiwayatMutasi.Controls.Add(ucrMutasiSenjata);
            }
           
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPengelolaan && status_RiwayatPengguna == false)
            {
                status_RiwayatPengguna = true;
                ucRpenggunaSenjata ucrPenggunaSenjata = new ucRpenggunaSenjata(this.ID_KSENJ, this.status);
                ucrPenggunaSenjata.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrPenggunaSenjata.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrPenggunaSenjata.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrPenggunaSenjata.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrPenggunaSenjata.Dock = DockStyle.Fill;
                TabRiwayatPengelolaan.Controls.Clear();
                TabRiwayatPengelolaan.Controls.Add(ucrPenggunaSenjata);
            }
            else if (this.TabDetail.SelectedTabPage == this.TabPemakai && status_Pemakai == false)
            {
                status_Pemakai = true;
                ucRpemakaiSenjata ucrPemakaiSenjata = new ucRpemakaiSenjata(this.ID_KSENJ, this.status);
                ucrPemakaiSenjata.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrPemakaiSenjata.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrPemakaiSenjata.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrPemakaiSenjata.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrPemakaiSenjata.Dock = DockStyle.Fill;
                TabPemakai.Controls.Clear();
                TabPemakai.Controls.Add(ucrPemakaiSenjata);
            }
            else if (this.TabDetail.SelectedTabPage == this.TabDokumenKib && status_DokKib == false)
            {
                status_DokKib = true;
                ucRKibSenjata ucrKibSenjata = new ucRKibSenjata(this.ID_KSENJ, this.status);
                ucrKibSenjata.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrKibSenjata.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrKibSenjata.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrKibSenjata.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrKibSenjata.Dock = DockStyle.Fill;
                TabDokumenKib.Controls.Clear();
                TabDokumenKib.Controls.Add(ucrKibSenjata);
            }
            else if (this.TabDetail.SelectedTabPage == this.TabSusut && status_Susut == false)
            {
                status_Susut = true;
                ucRsusutSenjata ucrsusutSenjata = new ucRsusutSenjata(this.ID_KSENJ, this.status);
                ucrsusutSenjata.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrsusutSenjata.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrsusutSenjata.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrsusutSenjata.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrsusutSenjata.Dock = DockStyle.Fill;
                TabSusut.Controls.Clear();
                TabSusut.Controls.Add(ucrsusutSenjata);
            }
            else if (this.TabDetail.SelectedTabPage == this.TabSpm && status_Spm == false)
            {
                status_Spm = true;
                ucRspmSenjata ucrspmSenjata = new ucRspmSenjata(this.ID_KSENJ, this.status);
                ucrspmSenjata.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrspmSenjata.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrspmSenjata.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrspmSenjata.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrspmSenjata.Dock = DockStyle.Fill;
                TabSpm.Controls.Clear();
                TabSpm.Controls.Add(ucrspmSenjata);
            }
            
        }


        private void ambilTanah(SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP data)
        {
            this.ID_KSENJ = data.ID_KSENJ;
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
            }
            else
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

        //===================================================== lihat kib ================================================
        private void getDataKib()
        {
            ucRKibSenjata ucrKibSenjata = new ucRKibSenjata(this.ID_KSENJ, this.status);
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInpKib = new SvcSenjataRkibSelect.InputParameters();
            parInpKib.P_COL = "";

            parInpKib.P_MAX = 1;
            parInpKib.P_MAXSpecified = true;
            parInpKib.P_MIN = 0;
            parInpKib.P_MINSpecified = true;
            parInpKib.P_SORT = "DESC";
            parInpKib.STR_WHERE = String.Format(" ID_KSENJ = {0} {1}", this.ID_KSENJ, "");
            parInpKib.P_COUNT = "Y";
            Console.WriteLine(parInpKib.STR_WHERE);
            fetchDataKib = new SvcSenjataRkibSelect.call_pttClient();
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

        private delegate void ShowData(SvcSenjataRkibSelect.OutputParameters dataOut);
        public void showData(SvcSenjataRkibSelect.OutputParameters serviceOutPut)
        {
            int jumlahData = serviceOutPut.SF_ROW_M_KSENJ_DOK_KIB.Count();
            if (jumlahData <= 0)
            {
                MessageBox.Show("Dokumen tidak ada", "Perhatian!!!");
            }
            else
            {
                decimal? idDokumen = this.outDataKib.SF_ROW_M_KSENJ_DOK_KIB[0].ID_KSENJ_DOK_KIB;
                try
                {
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                    parInp.P_ID = idDokumen;
                    parInp.P_ID_TABLE = "ID_KSENJ_DOK_KIB";
                    parInp.P_IDSpecified = true;
                    parInp.P_TABLE = "M_KSENJ_DOK_KIB";
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

        protected override void sbLihatDok_Click(object sender, EventArgs e)
        {
        }

        protected override void sbLihatKib_Click(object sender, EventArgs e)
        {
            this.getDataKib();
            
        }

        #region View Dokumen

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
                System.IO.File.WriteAllBytes(this.outDataKib.SF_ROW_M_KSENJ_DOK_KIB[0].NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(this.outDataKib.SF_ROW_M_KSENJ_DOK_KIB[0].NMFILE);
                PuPdf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Dokumen KIB terakhir tidak ada", "Perhatian!!");
            }
        }


        #endregion//ViewDokumen

        protected override void sbLapKIB_Click(object sender, EventArgs e) 
        {
            upg1210 = new UPG1210();
            upg1210.bsUPG1210.DataSource = this.selectedData;
            upg1210.bsFotoUPG1206.DataSource = this.DafarFoto;

            ReportPrintTool pt;
            pt = new ReportPrintTool(upg1210);
            pt.AutoShowParametersPanel = true;
            pt.ShowPreviewDialog();
        }

    }
}
