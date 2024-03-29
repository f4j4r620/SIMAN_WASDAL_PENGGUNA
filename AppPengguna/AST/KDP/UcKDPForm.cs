﻿using System;
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
namespace AppPengguna.AST.KDP
{
    public delegate void UpdateKDP(SvcKDPCrud.OutputParameters dataOutKDPCrud);
    public  class UcKDPForm : ucDetailAsetForm
    {
      
        public ucIdentitasKDP identitas = new ucIdentitasKDP();
        private SvcKDPSelect.BPSIMANSROW_KDP selectedData;
  
        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public UpdateKDP updateKDP;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        public ProgBar progressbar;
      

        private Thread myThread;
        private char modeCrud;
        private string KD_KONDISI;
        private string KD_KAB;
        private string KD_PROV;
        private decimal? ID_SATKER;
        private decimal? ID_KDP;
    
        private decimal? ID_SATKER_PMK;
        private decimal? ID_KTNH; /// Just Test
        private string KD_STATUS;
        private decimal? ID_KANWIL;
        private decimal? ID_KORWIL;
        private decimal? ID_ESELON1;
        private decimal? ID_KL;
        private decimal? ID_PENGADAAN;
        
        private int ulang = 0;
        private SvcKDPCrud.call_pttClient svcKDPCrud;
        private SvcKDPCrud.OutputParameters outDataCrud;
        private SvcAssetTanahSelect.call_pttClient fetchDataTanah;
        private SvcAssetTanahSelect.OutputParameters outDataTanahSelect;

  
    

        #region PopUp Form
        private FrmPUSatker PuSatker;
        private FrmPUKondisi PuKondisi;
        private FrmPUSskel PuSskel;
        #endregion

        #region Variable Aktif Tab
        private bool status_FasPenunjang;
        private bool status_DokKDP;
        private bool status_LokKDP;
        private bool status_GPS;
        private bool status_DetailRuangan;
        private bool status_KonstruksiKDP;
        private bool status_RiwayatPenilaian;
        private bool status_NJOP;
        private bool status_RiwayatKDP;
        private bool status_RiwayatPengguna;
        private bool status_RiwayatMutasi;
        private bool status_RiwayatPemeliharaan;
        private bool status_PermasalahanHukum;
        private bool status_Spm;
        #endregion



        public UcKDPForm(string _status)
        {

            this.status = _status;
          
        }

        public bool IsiForm(SvcKDPSelect.BPSIMANSROW_KDP Data)
        {
            this.selectedData = Data;
            try
            {
                this.ID_SATKER = selectedData.ID_SATKER;
                this.ID_KDP = selectedData.ID_KDP;
                this.ID_ASET = selectedData.ID_ASET;
                #region LayoutKiri
                // -- IDENTITAS UAKPB --
                this.teKdUAKPB.Text = selectedData.KD_SATKER;
                this.teUrUAKPB.Text = selectedData.UR_SATKER;

                ///----------- IDENTITAS BANGUNAN ---------------------
                this.teNoReg.Text = selectedData.NOREG;
               this.teKdBrg.Text = selectedData.KD_BRG;
               this.teNUP.Text = selectedData.NO_ASET.ToString();
               this.teKIB.Visible = false;
               this.LCKodeBarang.Text = "Kode Barang-NUP";
               this.teNamaBarang.Text = selectedData.UR_SSKEL;
               string tipe = (selectedData.TIPE != "-" && selectedData.TIPE != "") ? "/" + selectedData.TIPE : "";
               this.teMerk_tipe.Text = selectedData.MERK + tipe;
             
                // --- KODE---
               this.KD_KONDISI = selectedData.KD_KONDISI;
               this.teKondisi.Text = selectedData.UR_KONDISI;
               this.identitas.teSubKelompok.Text = selectedData.UR_SKEL;
               this.identitas.teFungsi.Text = selectedData.FUNGSI;
               this.identitas.teSFUNGSI.Text = selectedData.SFUNGSI;
               this.identitas.teKD_PROGRAM.Text = selectedData.KDPROG;
               this.identitas.teKD_GIAT.Text = selectedData.KDGIAT;
               this.identitas.teCaraPembangunan.Text = selectedData.CR_BANGUN;
               //-----KODE----
            
        
               this.identitas.teNO_KONTRAK.Text = selectedData.NO_KONTRAK;
               this.identitas.teAlamatKontraktor.Text = selectedData.ALM_KONTRAK;
               this.identitas.teNM_KONTRAKTOR.Text = selectedData.NM_KONTRAK;
                this.identitas.teTGL_MULAI_KONTRAK.Text = konfigApp.DateToString(selectedData.TGL_MULAI);
                this.identitas.teTGL_AKHIR_KONTRAK.Text = konfigApp.DateToString(selectedData.TGL_AKHIR);
               // //-----------------  IMB -----------------------------------
              
             
              
              
               // //---------- KODE -------------------
               this.KD_STATUS = selectedData.KD_STATUS;
               this.identitas.teStatusPengguna.Text = selectedData.UR_STATUS;
               this.identitas.teJenisPengguna.Text = selectedData.JNS_PMK;
               // //---------- KODE -------------------
               this.ID_SATKER_PMK = selectedData.ID_SATKER_PENGGUNA;
               this.identitas.teKodePengguna.Text = selectedData.KD_SATKER_PENGGUNA;
               this.identitas.teNamaPengguna.Text = selectedData.UR_SATKER_PENGGUNA;
               this.identitas.teStatusPengelolaan.Text = selectedData.STATUS_KELOLA;

  
               //this.identitas.teCatatan.Text = selectedData.CATATAN;


               // #endregion

               // #region layout Kanan
           
              

               // //----- NILAI(DALAM RUPIAH) -----
               this.identitas.teNilaiKontrak.Text = selectedData.RPH_KONTRAK.ToString();
               this.identitas.teNilaiMutasi.Text = selectedData.RPH_KDP.ToString();
             
              
               try
               {
                   this.identitas.teNilaiKDP.Text = (selectedData.RPH_KONTRAK + selectedData.RPH_KDP).ToString();
               }
               catch (Exception)
               {
                   this.identitas.teNilaiKDP.Value = 0;
               }
               this.identitas.teValutaAsing.Text = selectedData.VALAS_KDP.ToString();

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

        private delegate void ShowDataTanah(SvcAssetTanahSelect.OutputParameters dataOut);

        public void showDataTanah(SvcAssetTanahSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_ASET_M_KTNH.Count();

            for (int i = 0; i < jmlData; i++)
            {
                //this.ID_KTNH = serviceOutPut.SF_ROW_ASET_M_KTNH[0].ID_KTNH;
                //this.teJenisPemilik.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].JNS_PMK;
                //this.teKdPengguna.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].KD_SATKER;
                //this.teNamaPengguna.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].UR_SATKER;
                //this.teKdBrg.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].KD_BRG;
                //this.teNUPbrg.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].NO_ASET.ToString();
                //this.teKIBbrg.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].NO_KIB;
                //this.teNamaBarang.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].UR_SSKEL;
                // this.teNoDokumen.Text = serviceOutPut.SF_ROW_ASET_M_KTNH[0].KD_JNS_SERTI;
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
                        SvcKDPCrud.InputParameters parInp = new SvcKDPCrud.InputParameters();

                        

                        #region Data yang tidak dapat diubah
                        parInp.P_ALM_KONTRAK = selectedData.ALM_KONTRAK;
                        parInp.P_CR_BANGUN = selectedData.CR_BANGUN;
                        

                        parInp.P_FLAG_KOR = selectedData.FLAG_KOR;
                        parInp.P_FLAG_KRM = selectedData.FLAG_KRM;
                        parInp.P_FLAG_TTP = selectedData.FLAG_TTP;
                        parInp.P_FUNGSI = selectedData.FUNGSI;
                   
                        parInp.P_ID_ASET = selectedData.ID_ASET;
              
                        parInp.P_ID_ASETSpecified = true;
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
                        parInp.P_KDGIAT = selectedData.KDGIAT;
                        parInp.P_KDKPKNL = selectedData.KDKPKNL;
                        parInp.P_KDKPPN = selectedData.KDKPPN;
                        parInp.P_KDPROG = selectedData.KDPROG;
                        parInp.P_KET = selectedData.KET;
                        parInp.P_LOKASI_KDP = selectedData.LOKASI_KDP;
                        parInp.P_NM_KONTRAK = selectedData.NM_KONTRAK;
                        parInp.P_MERK = selectedData.MERK;
                        parInp.P_NO_KONTRAK = selectedData.NO_KONTRAK;
                        parInp.P_NO_SPPA = selectedData.NO_SPPA;
                        parInp.P_NO_ASET = selectedData.NO_ASET;
                        
                        parInp.P_NOREG = selectedData.NOREG;
                        parInp.P_PERSEN = selectedData.PERSEN;
                        parInp.P_PERSENSpecified = true;
                        
                        
                        parInp.P_PERIODE = selectedData.PERIODE;
                        parInp.P_RPH_ASET = selectedData.RPH_ASET;
                        parInp.P_RPH_ASETSpecified = true;
                        parInp.P_RPH_KDP = selectedData.RPH_KDP;
                        parInp.P_RPH_KDPSpecified = true;
                        parInp.P_RPH_MUTASI = selectedData.RPH_MUTASI;
                        parInp.P_RPH_MUTASISpecified = true;
                        parInp.P_RPH_RES = selectedData.RPH_RES;
                        parInp.P_RPH_RESSpecified = true;
                        parInp.P_RPH_SUSUT = selectedData.RPH_SUSUT;
                        parInp.P_RPH_SUSUTSpecified = true;
                        parInp.P_RPH_KONTRAK = selectedData.RPH_KONTRAK;
                        parInp.P_RPH_KONTRAKSpecified = true;
                        parInp.P_SFUNGSI = selectedData.SFUNGSI;
                        parInp.P_SPPA_ASET = selectedData.SPPA_ASET;
                       
                        parInp.P_STATUS_BMN_YN = selectedData.STATUS_BMN_YN;
                        parInp.P_TERCATAT = selectedData.TERCATAT;
                        parInp.P_TGL_REKAM = konfigApp.DateToString(selectedData.TGL_REKAM);
                        parInp.P_TGL_AKHIR = konfigApp.DateToString(selectedData.TGL_AKHIR);
                        parInp.P_TGL_MULAI = konfigApp.DateToString(selectedData.TGL_MULAI);
                        parInp.P_THP_KDP = selectedData.THP_KDP;
                        parInp.P_THN_ANG = selectedData.THN_ANG;
                      
                        parInp.P_TIPE = selectedData.TIPE;
                        parInp.P_UMEKO = selectedData.UMEKO;
                        parInp.P_UMEKOSpecified = true;
                        parInp.P_VALAS_KDP = selectedData.VALAS_KDP;
                        parInp.P_VALAS_KDPSpecified = true;
                        parInp.P_KUANTITAS = selectedData.KUANTITAS;
                        parInp.P_KUANTITASSpecified = true;
                        #endregion

                        #region Data isi


                        #endregion


                        parInp.P_SELECT = "U";
                        this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                        svcKDPCrud = new SvcKDPCrud.call_pttClient();
                        svcKDPCrud.Open();
                        svcKDPCrud.Beginexecute(parInp, new AsyncCallback(crudKDP), "");
                        
                        
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

        private void crudKDP(IAsyncResult result)
        {
            try
            {
                outDataCrud = svcKDPCrud.Endexecute(result);
                svcKDPCrud.Close();
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
            if (this.mulai == true || this.status == "detail")
            {
                this.mulai = false;
                this.BtnUnggahFoto.Enabled = false;
                this.btnHapusFoto.Enabled = false;
            }
            else{
                this.BtnUnggahFoto.Enabled = true;
                this.btnHapusFoto.Enabled = true;
            }
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
        private delegate void SimpanData(SvcKDPCrud.OutputParameters dataOutKDPCrud);

        private void simpanData(SvcKDPCrud.OutputParameters dataOutKDPCrud)
        {
            this.updateKDP(dataOutKDPCrud);
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
            this.TabLokasi.Text = "Batas dan GPS";
            this.TabRiwayatBangunan.PageVisible = false;
            this.TabRiwayatPenilaian.Text = "Penilaian";
            this.TabPermasalahanHukum.PageVisible = false;
            this.TabRiwayatMutasi.Text = "Mutasi";
            this.TabRiwayatNJOP.PageVisible = false;
            this.TabRiwayatPemeliharaan.PageVisible = false;// "Pemeliharaan";
            this.TabRiwayatPengelolaan.Text = "Pengelolaan";
            this.TabAsuransi.PageVisible = false;
            this.TabDaftarBangunan.PageVisible = false;
            this.TabPemakai.PageVisible = false;
            this.TabSusut.PageVisible = false;
            this.TabDokumenKib.PageVisible = false;
            this.sbLihatDok.Enabled = false;
            this.sbLihatKib.Enabled = false;

            Foto.AnimationTime = 500;

            Foto.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            if (this.status == "edit" || this.status == "input")
            {

                this.FormReadOnly(true);
                this.identitas.teKD_PROGRAM.Properties.ReadOnly = false;
                this.identitas.teFungsi.Properties.ReadOnly = false;
                this.identitas.teCatatan.Properties.ReadOnly = false;
                this.btnTampilkanFoto.Enabled = true;
                this.Foto.Enabled = true;
                
            }
            else
            {

                this.FormReadOnly(true);
              
            }

            this.btnHapusFoto.Enabled = false;
            this.BtnUnggahFoto.Enabled = false;
            this.getInitPhoto(1);
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
           
        if (TabDetail.SelectedTabPage == this.TabRiwayatPenilaian && status_RiwayatPenilaian == false)
            {
                status_RiwayatPenilaian = true;
                UcKDPRwyNil ucRwyNil = new UcKDPRwyNil(this.ID_KDP, this.status);
                ucRwyNil.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyNil.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyNil.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyNil.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucRwyNil.Dock = DockStyle.Fill;
                TabRiwayatPenilaian.Controls.Clear();
                TabRiwayatPenilaian.Controls.Add(ucRwyNil);
            }
            
            else if (TabDetail.SelectedTabPage == this.TabRiwayatMutasi && status_RiwayatMutasi == false)
            {
                status_RiwayatMutasi = true;
                UcKDPRwyMut ucRwyMut = new UcKDPRwyMut(this.ID_KDP, this.status);
                ucRwyMut.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyMut.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyMut.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyMut.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucRwyMut.Dock = DockStyle.Fill;
                TabRiwayatMutasi.Controls.Clear();
                TabRiwayatMutasi.Controls.Add(ucRwyMut);
            }
           
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPengelolaan && status_RiwayatPengguna == false)
            {
                status_RiwayatPengguna = true;
                UcKDPRwyPgg ucRwyPgg = new UcKDPRwyPgg(this.ID_KDP, this.status);
                ucRwyPgg.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyPgg.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyPgg.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyPgg.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucRwyPgg.Dock = DockStyle.Fill;
                TabRiwayatPengelolaan.Controls.Clear();
                TabRiwayatPengelolaan.Controls.Add(ucRwyPgg);
            }
        else if (TabDetail.SelectedTabPage == this.TabSpm && status_Spm == false)
        {
            status_Spm = true;
            UcKDPRwySpm ucKDPRwySpm = new UcKDPRwySpm(this.ID_KDP, this.status);
            ucKDPRwySpm.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
            ucKDPRwySpm.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
            ucKDPRwySpm.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
            ucKDPRwySpm.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
            ucKDPRwySpm.Dock = DockStyle.Fill;
            TabSpm.Controls.Clear();
            TabSpm.Controls.Add(ucKDPRwySpm);
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
            //this.teKdPenggunaKDP.Text = kode;
            //this.teNamaPenggunaKDP.Text = nama;
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
                this.BtnUnggahFoto.Enabled = false;
                this.btnHapusFoto.Enabled = false;
            }
            else
            {
                this.BtnUnggahFoto.Enabled = true;
                this.btnHapusFoto.Enabled = true;
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
