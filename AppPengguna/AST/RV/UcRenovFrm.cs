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
namespace AppPengguna.AST.RV
{
    public delegate void UpdateRenovasi(SvcRenovasiCrud.OutputParameters dataOutRenovasiCrud);
    public  class UcRenovFrm : ucDetailAsetForm
    {
      
        public ucIdentitasRenovasi identitas = new ucIdentitasRenovasi();
        private SvcRenovasiSelect.BPSIMANSROW_KRNV selectedData;
   
        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public UpdateRenovasi updateRenovasi;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        public ProgBar progressbar;
      

        private Thread myThread;
        private char modeCrud;
        private string KD_KONDISI;
        private string KD_KAB;
        private string KD_PROV;
        private decimal? ID_SATKER;
        private decimal? ID_KRNV;
   
        private decimal? ID_SATKER_PMK;
        private decimal? ID_ASET_RENOV; /// Just Test
        private string KD_STATUS;
        private decimal? ID_KANWIL;
        private decimal? ID_KORWIL;
        private decimal? ID_ESELON1;
        private decimal? ID_KL;
        private decimal? ID_PENGADAAN;
        
        private int ulang = 0;
        private SvcRenovasiCrud.call_pttClient svcRenovasiCrud;
        private SvcRenovasiCrud.OutputParameters outDataCrud;
        private SvcAsetSelect.call_pttClient fetchDataAset;
        private SvcAsetSelect.OutputParameters outDataAsetSelect;

     

    

        #region PopUp Form
        private FrmPUSatker PuSatker;
        private FrmPUKondisi PuKondisi;
        private FrmPUSskel PuSskel;
        #endregion

        #region Variable Aktif Tab
        private bool status_FasPenunjang;
        private bool status_DokRenovasi;
        private bool status_LokRenovasi;
        private bool status_GPS;
        private bool status_DetailRuangan;
        private bool status_KonstruksiRenovasi;
        private bool status_RiwayatPenilaian;
        private bool status_NJOP;
        private bool status_RiwayatRenovasi;
        private bool status_RiwayatPengguna;
        private bool status_RiwayatMutasi;
        private bool status_RiwayatPemeliharaan;
        private bool status_PermasalahanHukum;
        private bool status_Susut;
        private bool status_Spm;
        private bool status_Foto;
        #endregion



        public UcRenovFrm(string status)
        {
           
            this.status = status;
          
        }

        public bool IsiForm(SvcRenovasiSelect.BPSIMANSROW_KRNV Data)
        {
            this.selectedData = Data;
            try
            {
                this.ID_SATKER = selectedData.ID_SATKER;
                this.ID_KRNV = selectedData.ID_KRNV;
                this.ID_ASET = selectedData.ID_ASET;
                this.ID_ASET_RENOV = selectedData.ID_ASET_RENOV;
                #region LayoutKiri
                // -- IDENTITAS UAKPB --
                this.teKdUAKPB.Text = selectedData.KD_SATKER;
                this.teUrUAKPB.Text = selectedData.UR_SATKER;

                ///----------- IDENTITAS BANGUNAN ---------------------
                this.teNoReg.Text = selectedData.NOREG;
               this.teKdBrg.Text = selectedData.KD_BRG;
               this.teNUP.Text = selectedData.NO_ASET;
               this.teKIB.Text = selectedData.NO_KIB;
               this.teNamaBarang.Text = selectedData.UR_SSKEL;
               string tipe = (selectedData.TIPE != "-" && selectedData.TIPE != "") ? "/" + selectedData.TIPE : "";
               this.teMerk_tipe.Text = selectedData.MERK + tipe;
             
                // --- KODE---
               this.KD_KONDISI = selectedData.KD_KONDISI;
               this.teKondisi.Text = selectedData.UR_KONDISI;
            
               this.identitas.teSubKelompok.Text = selectedData.UR_SKEL;
              
               this.identitas.teKelompok.Text = selectedData.UR_KEL;
        
               this.identitas.teBidang.Text = selectedData.UR_BID;
              

            
           
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

               this.identitas.teTanggalPembukuan.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
               if (this.identitas.teTanggalPembukuan.Text == "11/11/1000") { this.identitas.teTanggalPembukuan.Text = null; }
               this.identitas.teTanggalRekam.Text = konfigApp.DateToString(selectedData.TGL_REKAM);
               if (this.identitas.teTanggalRekam.Text == "11/11/1000") { this.identitas.teTanggalRekam.Text = null; }

               //----- PENGADAAN -----
               //--------- kode -----------------
               this.ID_PENGADAAN = selectedData.ID_PENGADAAN;
               this.identitas.teNomorDana.Text = selectedData.NO_DANA;
               this.identitas.teTanggalDana.Text = konfigApp.DateToString(selectedData.TGL_DANA);
               if (this.identitas.teTanggalDana.Text == "11/11/1000") { this.identitas.teTanggalDana.Text = null; }
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
                SvcAsetSelect.InputParameters parInp = new SvcAsetSelect.InputParameters();
                parInp.P_MAX = 1;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.P_SORT = "";
                parInp.STR_WHERE = "ID_ASET = " + this.ID_ASET;
                fetchDataAset = new SvcAsetSelect.call_pttClient();
                fetchDataAset.Open();
                fetchDataAset.Beginexecute(parInp, new AsyncCallback(this.getResultTanah), null);

               
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
                this.outDataAsetSelect = fetchDataAset.Endexecute(result);
                fetchDataAset.Close();
                if (this.InvokeRequired)
                {
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                    this.Invoke(new ShowDataTanah(this.showDataTanah), this.outDataAsetSelect);
                }
                else
                {
                    this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                    this.aktifkanForm("");
                    this.showDataTanah(this.outDataAsetSelect);
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

        private delegate void ShowDataTanah(SvcAsetSelect.OutputParameters dataOut);
     
        public void showDataTanah(SvcAsetSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_M_ASET.Count();

            for (int i = 0; i < jmlData; i++)
            {
                this.ID_ASET_RENOV = serviceOutPut.SF_ROW_M_ASET[0].ID_ASET;
                //this.identitas.teJenisPemilikAset.Text = serviceOutPut.SF_ROW_M_ASET[0].JNS_PMK;
                this.identitas.teKodePenggunaAset.Text = serviceOutPut.SF_ROW_M_ASET[0].KD_SATKER;
                this.identitas.teNamaPenggunaAset.Text = serviceOutPut.SF_ROW_M_ASET[0].UR_SATKER;
                this.identitas.teKodeBrgAset.Text = serviceOutPut.SF_ROW_M_ASET[0].KD_BRG;
                this.identitas.teNUPAset.Text = serviceOutPut.SF_ROW_M_ASET[0].NO_ASET.ToString();
               // this.identitas.teKIBAset.Text = serviceOutPut.SF_ROW_M_ASET[0].NO_KIB;
                this.identitas.teNamaBarangAset.Text = serviceOutPut.SF_ROW_M_ASET[0].UR_SSKEL;
               // this.identitas.teNomorDokumenAset.Text = serviceOutPut.SF_ROW_M_ASET[0].KD_JNS_SERTI;
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
                        SvcRenovasiCrud.InputParameters parInp = new SvcRenovasiCrud.InputParameters();

                        #region Data isi
                       

                        #endregion

                        #region Data yang tidak dapat diubah
                        parInp.P_CATATAN = selectedData.CATATAN;
                        

                        parInp.P_FLAG_KOR = selectedData.FLAG_KOR;
                        parInp.P_FLAG_KRM = selectedData.FLAG_KRM;
                        parInp.P_FLAG_TTP = selectedData.FLAG_TTP;
                        parInp.P_ID_ASET = selectedData.ID_ASET;

                        parInp.P_ID_ASET_RENOV = selectedData.ID_ASET_RENOV;
                        parInp.P_ID_ASET_RENOVSpecified = true;
                       

                        parInp.P_ID_ASETSpecified = true;
                        parInp.P_ID_PENGADAAN = selectedData.ID_PENGADAAN;
                        parInp.P_ID_PENGADAANSpecified = true;
                        parInp.P_ID_SATKER = selectedData.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                       
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
                       
                        parInp.P_MERK = selectedData.MERK;
                        parInp.P_NO_ASET = selectedData.NO_ASET;
                        parInp.P_NO_KIB = selectedData.NO_KIB;
                        
                     
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
                        
                        parInp.P_STATUS_BMN_YN = selectedData.STATUS_BMN_YN;
                        parInp.P_TERCATAT = selectedData.TERCATAT;
                        parInp.P_TGL_REKAM = konfigApp.DateToString(selectedData.TGL_REKAM);
                       
                        parInp.P_THN_ANG = selectedData.THN_ANG;
                       
                        parInp.P_TIPE = selectedData.TIPE;
                        parInp.P_UMEKO = selectedData.UMEKO;
                        parInp.P_UMEKOSpecified = true;
                       
                        #endregion




                        parInp.P_SELECT = "U";
                        this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                        svcRenovasiCrud = new SvcRenovasiCrud.call_pttClient(konfigApp.SvcRenovasiCrud_name,konfigApp.SvcRenovasiCrud_address);
                        svcRenovasiCrud.Open();
                        svcRenovasiCrud.Beginexecute(parInp, new AsyncCallback(crudRenovasi), "");
                        
                        
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

        private void crudRenovasi(IAsyncResult result)
        {
            try
            {
                outDataCrud = svcRenovasiCrud.Endexecute(result);
                svcRenovasiCrud.Close();
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
        private delegate void SimpanData(SvcRenovasiCrud.OutputParameters dataOutRenovasiCrud);

        private void simpanData(SvcRenovasiCrud.OutputParameters dataOutRenovasiCrud)
        {
            this.updateRenovasi(dataOutRenovasiCrud);
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
            this.TabLokasi.PageVisible = false;
            this.TabRiwayatBangunan.PageVisible = false;
            this.TabRiwayatPenilaian.PageVisible = false;
            this.TabPermasalahanHukum.PageVisible = false;
            this.TabRiwayatMutasi.Text = "Mutasi";
            this.TabRiwayatNJOP.PageVisible = false;
            this.TabRiwayatPemeliharaan.PageVisible = false;
            this.TabDokumenKib.PageVisible = false;
            this.TabRiwayatPengelolaan.PageVisible = false;
            this.TabRiwayatPemeliharaan.PageVisible = true;
            this.TabAsuransi.PageVisible = false;
            this.TabDaftarBangunan.PageVisible = false;
            this.TabPemakai.PageVisible = false;
            this.sbLihatDok.Enabled = false;
            this.sbLihatKib.Enabled = false;

            Foto.AnimationTime = 500;

            Foto.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            if (this.status == "edit" || this.status == "input")
            {

                this.FormReadOnly(true);
              
                this.btnTampilkanFoto.Enabled = true;
                this.Foto.Enabled = true;
                
            }
            else
            {

                this.FormReadOnly(true);
              
            }

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
           
         if (TabDetail.SelectedTabPage == this.TabRiwayatMutasi && status_RiwayatMutasi == false)
            {
                status_RiwayatMutasi = true;
                UcRwyMutRenov ucRwyMutRenov = new UcRwyMutRenov(this.ID_KRNV, this.status);
                ucRwyMutRenov.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyMutRenov.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyMutRenov.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyMutRenov.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucRwyMutRenov.Dock = DockStyle.Fill;
                TabRiwayatMutasi.Controls.Clear();
                TabRiwayatMutasi.Controls.Add(ucRwyMutRenov);
            }
           
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPengelolaan && status_RiwayatPengguna == false)
            {
                status_RiwayatPengguna = true;
                UcRwyPenggunaRenov ucRwyPenggunaRenov = new UcRwyPenggunaRenov(this.ID_KRNV, this.status);
                ucRwyPenggunaRenov.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyPenggunaRenov.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyPenggunaRenov.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyPenggunaRenov.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucRwyPenggunaRenov.Dock = DockStyle.Fill;
                TabRiwayatPengelolaan.Controls.Clear();
                TabRiwayatPengelolaan.Controls.Add(ucRwyPenggunaRenov);
            }
         else if (TabDetail.SelectedTabPage == this.TabSusut && status_Susut == false)
         {
             status_Susut = true;
             UcRwyPenggunaRenovSusut ucRwyPenggunaRenovSusut = new UcRwyPenggunaRenovSusut(this.ID_KRNV, this.status);
             ucRwyPenggunaRenovSusut.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
             ucRwyPenggunaRenovSusut.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
             ucRwyPenggunaRenovSusut.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
             ucRwyPenggunaRenovSusut.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
             ucRwyPenggunaRenovSusut.Dock = DockStyle.Fill;
             TabSusut.Controls.Clear();
             TabSusut.Controls.Add(ucRwyPenggunaRenovSusut);
         }
         else if (TabDetail.SelectedTabPage == this.TabSpm && status_Spm == false)
         {
             status_Spm = true;
             UcRwyPenggunaRenovSpm ucRwyPenggunaRenovSpm = new UcRwyPenggunaRenovSpm(this.ID_KRNV, this.status);
             ucRwyPenggunaRenovSpm.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
             ucRwyPenggunaRenovSpm.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
             ucRwyPenggunaRenovSpm.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
             ucRwyPenggunaRenovSpm.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
             ucRwyPenggunaRenovSpm.Dock = DockStyle.Fill;
             TabSpm.Controls.Clear();
             TabSpm.Controls.Add(ucRwyPenggunaRenovSpm);
         }
         else if (TabDetail.SelectedTabPage == this.TabRiwayatPemeliharaan && status_RiwayatPemeliharaan == false)
         {
             status_RiwayatPemeliharaan = true;
             UcRwyPemeliharaanRenov ucRwyPemeliharaanRenov = new UcRwyPemeliharaanRenov(this.ID_KRNV, this.status);
             ucRwyPemeliharaanRenov.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
             ucRwyPemeliharaanRenov.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
             ucRwyPemeliharaanRenov.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
             ucRwyPemeliharaanRenov.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
             ucRwyPemeliharaanRenov.Dock = DockStyle.Fill;
             TabRiwayatPemeliharaan.Controls.Clear();
             TabRiwayatPemeliharaan.Controls.Add(ucRwyPemeliharaanRenov);
         }
         else if (TabDetail.SelectedTabPage == this.TabFoto && status_Foto == false)
         {
             status_Foto = true;
             ucRenovFoto UcRenovFoto = new ucRenovFoto(this.ID_ASET, this.status);
             UcRenovFoto.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
             UcRenovFoto.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
             UcRenovFoto.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
             UcRenovFoto.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
             UcRenovFoto.Dock = DockStyle.Fill;
             TabFoto.Controls.Clear();
             TabFoto.Controls.Add(UcRenovFoto);
         }


            
            
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
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                
                this.Invoke(new ShowDataPhoto(this.showDataPhoto), this.outDataPhoto);
            }
            catch
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                
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

        protected override void sbLihatDok_Click(object sender, EventArgs e) { }

        protected override void sbLihatKib_Click(object sender, EventArgs e) { }

        protected override void sbLapKIB_Click(object sender, EventArgs e) { }

    }
}
