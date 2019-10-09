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
namespace AppPengguna.AST.AK
{
    public delegate void UpdateAngkutan(SvcAngkCrud.OutputParameters dataOutBangunanCrud);
    public  class ucAngkutanForm : ucDetailAsetForm
    {
      
        public ucIdentitasAngkutan identitas = new ucIdentitasAngkutan();
        private SvcAngkSelect.BPSIMANSROW_KANGK selectedData;

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public UpdateAngkutan updateAngkutan;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        public ProgBar progressbar;
     

        private Thread myThread;
        private Thread myThread_;
        private Boolean mulai = true;
        private char modeCrud;
        private string KD_KONDISI;
        private string KD_KAB;
        private string KD_PROV;
        private decimal? ID_SATKER;
        private decimal? ID_KANGK;
 
        private decimal? ID_SATKER_PMK;
        private decimal? ID_KTNH; /// Just Test
        private string KD_STATUS;
        private decimal? ID_KANWIL;
        private decimal? ID_KORWIL;
        private decimal? ID_ESELON1;
        private decimal? ID_KL;
        private decimal? ID_PENGADAAN;
        private decimal? idDokumen;
        
        private int ulang = 0;
        private SvcAngkCrud.call_pttClient svcangkutanCrud;
        private SvcAngkCrud.OutputParameters outDataCrud;
        private SvcFasAngkSelect.call_pttClient fetchDataPerlengkapan;
        private SvcFasAngkSelect.OutputParameters outDataPerlengkapanSelect;

        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;

        private SvcKibAngkSelect.call_pttClient fetchDataKib;
        private SvcKibAngkSelect.InputParameters parInpKib;
        public SvcKibAngkSelect.OutputParameters outDataKib;
        public SvcKibAngkSelect.BPSIMANSROW_M_KANGK_DOK_KIB selectedDataKib;

        private SvcBpkbAngkSelect.call_pttClient fetchDataDok;
        private SvcBpkbAngkSelect.InputParameters parInpDok;
        private SvcBpkbAngkSelect.OutputParameters outDatDok;
        public SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB selectedDataDok;

        //-------------------------------------------------------

        //----------------- GET lap KIB Angk -------------------------
        private decimal dataAwal = 1;
        private decimal dataAkhir = 20;
        private decimal currentMaks = 20;
        private decimal currentMin = 1;
        public bool dataInisial = true;
        ANG_5101 ang_5101;
        //-----------------------------------------------------------

        private UcFasAngk ucFasAngk;
        private UcNoPolAngk ucNoPolAngk;
        
        private UcBpkbAngk ucBpkbAngk;
        private UcRwyPenggunaAngk ucRwyPenggunaAngk;
        private UcRwyMutAngk ucRwyMutAngk;
        private UcRwyPemakaiAngk ucRwyPemakaiAngk;
        private UcRwyPemeliharaanAngk ucRwyPemeliharaanAngk;
        private UcRwyPenilaianAngk ucRwyPenilaianAngk;
        private UcNmFasAngk ucNmFasAngktn;
        private UcKibAngk ucKibAngk;

        #region PopUp Form
        private FrmPUSatker PuSatker;
        private FrmPUKondisi PuKondisi;
        private FrmPUSskel PuSskel;
        #endregion

        #region Variable Aktif Tab
        private bool status_FasPenunjang;
        private bool status_BPKB;
        private bool status_NoPolisi;
        private bool status_GPS;
        private bool status_DetailRuangan;
        private bool status_KonstruksiBangunan;
        private bool status_RiwayatPenilaian;
        private bool status_NJOP;
        private bool status_RiwayatBangunan;
        private bool status_RiwayatPengguna;
        private bool status_RiwayatMutasi;
        private bool status_RiwayatPemeliharaan;
        private bool status_Pemakai;
        private bool status_DokKib; 
        private bool status_Susut;
        private bool status_Spm;
        private bool status_Asuransi;
        #endregion

       

        public ucAngkutanForm(string status)
        {           
            this.status = status;
          
        }

        public bool IsiForm(SvcAngkSelect.BPSIMANSROW_KANGK Data)
        {
            this.selectedData = Data;
            try
            {
                this.ID_SATKER = selectedData.ID_SATKER;
                this.ID_KANGK = selectedData.ID_KANGK;
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
               string tipe = (selectedData.MERK.Trim() != "" && selectedData.TIPE != "") ? "/" + selectedData.TIPE : "";
               this.teMerk_tipe.Text = selectedData.MERK + tipe;
             
                // --- KODE---
               this.KD_KONDISI = selectedData.KD_KONDISI;
               this.teKondisi.Text = selectedData.UR_KONDISI;
               this.identitas.teTahunBuat.Text = selectedData.THN_BUAT;
               this.identitas.teTahunRakit.Text = selectedData.THN_RAKIT;
               string negara = (selectedData.PABRIK.Trim() != "" && selectedData.NEGARA.Trim() != "") ? "/" + selectedData.NEGARA : "";
               this.identitas.tePabrik_negara.Text = selectedData.PABRIK + negara;
               this.identitas.teBobot.Text = selectedData.BOBOT;
               this.identitas.teDayaAngkut.Text = selectedData.MUAT;
               //-----KODE----
             
               this.identitas.teDayaMesin.Text = selectedData.DAYA;
               this.identitas.teMesinGerak.Text = selectedData.MSN_GERAK;
               this.identitas.teBahanBakar.Text = selectedData.BHN_BAKAR;

               // //-----------------  IMB -----------------------------------
               this.identitas.teJmlMesin.Text = selectedData.JML_MSN.ToString();
               this.identitas.teNomorMesin.Text = selectedData.NO_MESIN;
               this.identitas.teNoBPKB.Text = selectedData.NO_BPKB;
               this.identitas.teNoPOLISI.Text = selectedData.NO_POLISI;
               // //---------- KODE -------------------
               this.KD_STATUS = selectedData.KD_STATUS;
               this.identitas.teStatusPengguna.Text = selectedData.UR_STATUS;
               this.identitas.teJenisPengguna.Text = selectedData.JNS_PMK;
               // //---------- KODE -------------------
               this.ID_SATKER_PMK = selectedData.ID_SATKER_PMK;
               this.identitas.teKodePengguna.Text = selectedData.KD_SATKER_PENGGUNA;
               this.identitas.teNamaPengguna.Text = selectedData.UR_SATKER_PENGGUNA;
               this.identitas.teKET.Text = selectedData.KET_PENGGUNA;
               this.identitas.teStatusPengelolaan.Text = selectedData.STATUS_KELOLA;

               this.identitas.teNIPPemakai.Text = selectedData.NIP_PMK;
               this.identitas.teNamaPemakai.Text = selectedData.NM_PMK;
               this.identitas.teKodeUnitPemakai.Text = selectedData.KD_SATKER_PMK;
               this.identitas.teNamaUnitPemakai.Text = selectedData.UR_SATKER_PMK;
               this.identitas.teAlamatPemakai.Text = selectedData.ALM_PMK;
               //this.identitas.teStatusHukum.Text = selectedData.STATUS_HUKUM;
               this.identitas.teCatatan.Text = selectedData.CATATAN;


               #endregion

               #region layout Kanan
               

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

               this.identitas.teTanggalBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU); 
               this.identitas.teTanggalRekam.Text = konfigApp.DateToString(selectedData.TGL_REKAM);

               //----- PENGADAAN -----
               //--------- kode -----------------
               this.ID_PENGADAAN = selectedData.ID_PENGADAAN;
               this.identitas.teNomorDana.Text = selectedData.NO_DANA;
               this.identitas.teTanggalDana.Text = konfigApp.DateToString(selectedData.TGL_DANA);
               this.identitas.teCaraPerolehan.Text = selectedData.DARI;
               this.identitas.teAsalPerolehan.Text = selectedData.ASL_PERLH;  // dihapus
               this.identitas.teTanggalPerolehan.Text = konfigApp.DateToString(selectedData.TGL_PERLH);
               #endregion Layout Kanan

                return true;
            
            }
            catch (Exception E)
            {
                this.aktifkanForm("");
                this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                
               // MessageBox.Show(konfigApp.teksGagalAmbil,konfigApp.judulGagalAmbil);
                
                MessageBox.Show("Data belum dipilih", konfigApp.judulGagalAmbil);
                return false;
            
                
                
            }
            
        }

        private void getInitData()
        {
          // MessageBox.Show("call getInitData > getInitPhoto","debug.message");
          getInitPhoto(1);
          #region LookUp PERLENGKAPAN
          try
          {
            this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Always);
            SvcFasAngkSelect.InputParameters parInp = new SvcFasAngkSelect.InputParameters();
            
            parInp.P_MAX = konfigApp.dataAkhir;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = 0;
            parInp.P_MINSpecified = true;
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_KANGK = " + this.ID_KANGK;
            fetchDataPerlengkapan = new SvcFasAngkSelect.call_pttClient();
            fetchDataPerlengkapan.Open();
            fetchDataPerlengkapan.Beginexecute(parInp, new AsyncCallback(this.getResultPerlengkapan), null);    
          }
          catch (Exception E)
          {
            this.modeCrud = 'A';
            this.aktifkanForm("");
            this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
            MessageBox.Show("Gagal ambil data perlengkapan Angkutan (ucAngkutanForm.getInitData).", konfigApp.judulGagalAmbil);

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
                    this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    this.Invoke(new ShowDataPerlengkapan(this.showDataPerlengkapan), outDataPerlengkapanSelect);
                }
                else
                {
                    this.aktifkanForm("");
                    this.CloseProgresBar(DevExpress.XtraBars.BarItemVisibility.Never);
                    
                    this.showDataPerlengkapan(outDataPerlengkapanSelect);
                }


            }
            catch (Exception E)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    
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


        private delegate void ShowDataPerlengkapan(SvcFasAngkSelect.OutputParameters dataOut);

        public void showDataPerlengkapan(SvcFasAngkSelect.OutputParameters serviceOutPut)
        {
          int jmlDataGroup = serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG.Count();
          for (int i = 0; i < jmlDataGroup; i++)
          {
              this.identitas.teperlengkapan.Items.Add(serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG[i].NM_FASILITAS);
          }

          //if (mulai == true) { getInitPhoto(1); }
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
                        SvcAngkCrud.InputParameters parInp = new SvcAngkCrud.InputParameters();

                        #region Data isi
                       


                        #endregion

                        #region Data yang tidak dapat diubah
                        parInp.P_BHN_BAKAR = selectedData.BHN_BAKAR;
                        parInp.P_BOBOT = selectedData.BOBOT;
                        parInp.P_CAD1 = selectedData.CAD1;
                        parInp.P_CATATAN = selectedData.CATATAN;
                        parInp.P_DAYA = selectedData.DAYA;
                        parInp.P_FLAG_KOR = selectedData.FLAG_KOR;
                        parInp.P_FLAG_KRM = selectedData.FLAG_KRM;
                        parInp.P_FLAG_TTP = selectedData.FLAG_TTP;
                        parInp.P_ID_ASET = selectedData.ID_ASET;
                        parInp.P_ID_ASETSpecified = true;
                        parInp.P_ID_PENGADAAN = selectedData.ID_PENGADAAN;
                        parInp.P_ID_PENGADAANSpecified = true;
                        parInp.P_ID_SATKER = selectedData.ID_SATKER;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_JML_MSN = selectedData.JML_MSN;
                        parInp.P_JML_MSNSpecified = true;
                        parInp.P_KD_BRG = selectedData.KD_BRG;
                        parInp.P_KD_DATA = selectedData.KD_DATA;
                        parInp.P_KD_DSR_HRG = selectedData.KD_DSR_HRG;
                        parInp.P_KD_JNS_BMN = selectedData.KD_JNS_BMN;
                        parInp.P_KD_JNS_BMNSpecified = true;
                        parInp.P_KD_KONDISI = selectedData.KD_KONDISI;
                        parInp.P_KD_STATUS = selectedData.KD_STATUS;
                        parInp.P_KDBAPEL = selectedData.KDBAPEL;
                        parInp.P_KDBLU = selectedData.KDBLU;
                        parInp.P_KDKPKNL = selectedData.KDKPKNL;
                        parInp.P_KDKPPN = selectedData.KDKPPN;
                        parInp.P_LOKASI = selectedData.LOKASI;
                        parInp.P_MERK = selectedData.MERK;
                        parInp.P_MSN_GERAK = selectedData.MSN_GERAK;
                        parInp.P_MUAT = selectedData.MUAT;
                        parInp.P_NEGARA = selectedData.NEGARA;
                        parInp.P_NO_ASET = selectedData.NO_ASET.ToString();
                        parInp.P_NO_KIB = selectedData.NO_KIB;
                        parInp.P_NO_MESIN = selectedData.NO_MESIN;
                        parInp.P_NO_RANGKA = selectedData.NO_RANGKA;
                        parInp.P_NOREG = selectedData.NOREG;
                        parInp.P_PABRIK = selectedData.PABRIK;
                        parInp.P_PERIODE = selectedData.PERIODE;
                        parInp.P_RPH_ASET = selectedData.RPH_ASET;
                        parInp.P_RPH_ASETSpecified = true;
                        parInp.P_RPH_MUTASI = selectedData.RPH_MUTASI;
                        parInp.P_RPH_MUTASISpecified = true;
                        parInp.P_RPH_RES = selectedData.RPH_RES;
                        parInp.P_RPH_RESSpecified = true;
                        parInp.P_RPH_SUSUT = selectedData.RPH_SUSUT;
                        parInp.P_RPH_SUSUTSpecified = true;
                        parInp.P_RPHWAJAR = selectedData.RPHWAJAR;
                        parInp.P_RPHWAJARSpecified = true;
                        parInp.P_STATUS_BMN_YN = selectedData.STATUS_BMN_YN;
                        parInp.P_TERCATAT = selectedData.TERCATAT;
                        parInp.P_TGL_REKAM = selectedData.TGL_REKAM;
                        parInp.P_TGL_REKAMSpecified = true;
                        parInp.P_THN_ANG = selectedData.THN_ANG;
                        parInp.P_THN_BUAT = selectedData.THN_BUAT;
                        parInp.P_THN_RAKIT = selectedData.THN_RAKIT;
                        parInp.P_TIPE = selectedData.TIPE;
                        parInp.P_UMEKO = selectedData.UMEKO;
                        parInp.P_UMEKOSpecified = true;
                        parInp.P_KUANTITAS = selectedData.KUANTITAS;
                        parInp.P_KUANTITASSpecified = true;
                        #endregion




                        parInp.P_SELECT = "U";
                        this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                        svcangkutanCrud = new SvcAngkCrud.call_pttClient(konfigApp.SvcAngkCrud_name,konfigApp.SvcAngkCrud_address);
                        svcangkutanCrud.Open();
                        svcangkutanCrud.Beginexecute(parInp, new AsyncCallback(crudBangunan), "");
                        
                        
                    }
                    catch (Exception E)
                    {
                        this.modeCrud = 'A';
                        try
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
                outDataCrud = svcangkutanCrud.Endexecute(result);
                svcangkutanCrud.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                
                this.msg_get_database(outDataCrud.PO_RESULT, "Y", 0);
                if (String.Compare(outDataCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    
                        
                        this.Invoke(new SimpanData(this.simpanData), outDataCrud);
                  
                }
            }
            catch (Exception E)
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                
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
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                
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
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                
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
        private delegate void SimpanData(SvcAngkCrud.OutputParameters dataOutBangunanCrud);

        private void simpanData(SvcAngkCrud.OutputParameters dataOutBangunanCrud)
        {
            this.updateAngkutan(dataOutBangunanCrud);
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
            this.TabDokumen.Text = "Dokumen Kepemilikan";
            this.TabIdentitas.Text = "Detail";
            this.TabFasilitasPenunjang.Text = "Perlengkapan";
            this.TabKonstruksi.PageVisible = false;
            this.TabLokasi.Text = "No Polisi";
            this.TabRiwayatBangunan.PageVisible = false;
            this.TabRiwayatPenilaian.Text = "Penilaian";
            this.TabPermasalahanHukum.PageVisible = false;
            this.TabRiwayatMutasi.Text = "Mutasi";
            this.TabRiwayatNJOP.PageVisible = false;
            this.TabRiwayatPemeliharaan.Text = "Pemeliharaan";
            this.TabRiwayatPengelolaan.Text = "Pengelolaan";

            this.TabDaftarBangunan.PageVisible = false;
            this.TabPemakai.Text = "Pemakai";
            


            Foto.AnimationTime = 500;

            Foto.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            if (this.status == "edit" || this.status == "input")
            {

                this.FormReadOnly(true);
                this.identitas.teBahanBakar.Properties.ReadOnly = false;
                this.identitas.tePabrik_negara.Properties.ReadOnly = false;
                this.identitas.teCatatan.Properties.ReadOnly = false;
                
                
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
           
            if (TabDetail.SelectedTabPage == this.TabDokumen && status_BPKB == false)
            {
              status_BPKB = true;
              UcBpkbAngk ucBpkbAngk = new UcBpkbAngk(this.ID_KANGK, this.status);
              ucBpkbAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
              ucBpkbAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
              ucBpkbAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
              ucBpkbAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
              
              ucBpkbAngk.Dock = DockStyle.Fill;
              TabDokumen.Controls.Clear();
              TabDokumen.Controls.Add(ucBpkbAngk);
            }
            else if (TabDetail.SelectedTabPage == this.TabLokasi && status_NoPolisi == false)
            {
              status_NoPolisi = true;
              UcNoPolAngk ucNoPolAngk = new UcNoPolAngk(this.ID_KANGK, this.status);
              ucNoPolAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
              ucNoPolAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
              ucNoPolAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
              ucNoPolAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

              ucNoPolAngk.Dock = DockStyle.Fill;
              TabLokasi.Controls.Clear();
              TabLokasi.Controls.Add(ucNoPolAngk);

            }
            else if (TabDetail.SelectedTabPage == this.TabFasilitasPenunjang && status_FasPenunjang == false)
            {
                status_FasPenunjang = true;
                UcFasAngk ucFasAngk = new UcFasAngk(this.ID_KANGK, this.status);
                ucFasAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucFasAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucFasAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucFasAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucFasAngk.Dock = DockStyle.Fill;
                TabFasilitasPenunjang.Controls.Clear();
                TabFasilitasPenunjang.Controls.Add(ucFasAngk);
            }
           
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPemeliharaan && status_RiwayatPemeliharaan == false)
            {
                status_RiwayatPemeliharaan = true;
                UcRwyPemeliharaanAngk ucRwyPemeliharaanAngk = new UcRwyPemeliharaanAngk(this.ID_KANGK, this.status);
                ucRwyPemeliharaanAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyPemeliharaanAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyPemeliharaanAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyPemeliharaanAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucRwyPemeliharaanAngk.Dock = DockStyle.Fill;
                TabRiwayatPemeliharaan.Controls.Clear();
                TabRiwayatPemeliharaan.Controls.Add(ucRwyPemeliharaanAngk);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPenilaian && status_RiwayatPenilaian == false)
            {
                status_RiwayatPenilaian = true;
                UcRwyPenilaianAngk ucRwyPenilaianAngk = new UcRwyPenilaianAngk(this.ID_KANGK, this.status);
                ucRwyPenilaianAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyPenilaianAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyPenilaianAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyPenilaianAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucRwyPenilaianAngk.Dock = DockStyle.Fill;
                TabRiwayatPenilaian.Controls.Clear();
                TabRiwayatPenilaian.Controls.Add(ucRwyPenilaianAngk);
            }
          
            else if (TabDetail.SelectedTabPage == this.TabRiwayatMutasi && status_RiwayatMutasi == false)
            {
                status_RiwayatMutasi = true;
                UcRwyMutAngk ucRwyMutAngk = new UcRwyMutAngk(this.ID_KANGK, this.status);
                ucRwyMutAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyMutAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyMutAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyMutAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucRwyMutAngk.Dock = DockStyle.Fill;
                TabRiwayatMutasi.Controls.Clear();
                TabRiwayatMutasi.Controls.Add(ucRwyMutAngk);
            }
            
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPengelolaan && status_RiwayatPengguna == false)
            {
                status_RiwayatPengguna = true;
                UcRwyPenggunaAngk ucRwyPenggunaAngk = new UcRwyPenggunaAngk(this.ID_KANGK, this.status);
                ucRwyPenggunaAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyPenggunaAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyPenggunaAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyPenggunaAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucRwyPenggunaAngk.Dock = DockStyle.Fill;
                TabRiwayatPengelolaan.Controls.Clear();
                TabRiwayatPengelolaan.Controls.Add(ucRwyPenggunaAngk);
            }
            else if (TabDetail.SelectedTabPage == this.TabPemakai && status_Pemakai == false)
            {
                status_Pemakai = true;
                UcRwyPemakaiAngk ucRwyPemakaiAngk = new UcRwyPemakaiAngk(this.ID_KANGK, this.status);
                ucRwyPemakaiAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucRwyPemakaiAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucRwyPemakaiAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucRwyPemakaiAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
             
                ucRwyPemakaiAngk.Dock = DockStyle.Fill;
                TabPemakai.Controls.Clear();
                TabPemakai.Controls.Add(ucRwyPemakaiAngk);
            }
            else if (TabDetail.SelectedTabPage == this.TabDokumenKib && status_DokKib == false)
            {
                status_DokKib = true;
                UcKibAngk ucKibAngk = new UcKibAngk(this.ID_KANGK, this.status);
                ucKibAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucKibAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucKibAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucKibAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucKibAngk.Dock = DockStyle.Fill;
                TabDokumenKib.Controls.Clear();
                TabDokumenKib.Controls.Add(ucKibAngk);
            }
            else if (TabDetail.SelectedTabPage == this.TabSusut && status_Susut == false)
            {
                status_Susut = true;
                UcSusutAngk ucSusutAngk = new UcSusutAngk(this.ID_KANGK, this.status);
                ucSusutAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucSusutAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucSusutAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucSusutAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucSusutAngk.Dock = DockStyle.Fill;
                TabSusut.Controls.Clear();
                TabSusut.Controls.Add(ucSusutAngk);
            }
            else if (TabDetail.SelectedTabPage == this.TabSpm && status_Spm == false)
            {
                status_Spm = true;
                UcSusutAngkSpm ucSusutAngkSpm = new UcSusutAngkSpm(this.ID_KANGK, this.status);
                ucSusutAngkSpm.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucSusutAngkSpm.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucSusutAngkSpm.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucSusutAngkSpm.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucSusutAngkSpm.Dock = DockStyle.Fill;
                TabSpm.Controls.Clear();
                TabSpm.Controls.Add(ucSusutAngkSpm);
            }
            else if (TabDetail.SelectedTabPage == this.TabAsuransi && status_Asuransi == false)
            {
                status_Asuransi = true;
                ucRwytAsuransiAngk ucrwytAsuransiAngk = new ucRwytAsuransiAngk(this.ID_KANGK, this.status);
                ucrwytAsuransiAngk.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrwytAsuransiAngk.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrwytAsuransiAngk.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrwytAsuransiAngk.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucrwytAsuransiAngk.Dock = DockStyle.Fill;
                TabAsuransi.Controls.Clear();
                TabAsuransi.Controls.Add(ucrwytAsuransiAngk);
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
                        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                        this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                        
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
        protected void getInitPhoto(int max= 0)
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
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
		            this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                    
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
	            }
                
            }
                

        }

        private void Foto_CurrentImageIndexChanged(object sender, DevExpress.XtraEditors.Controls.ImageSliderCurrentImageIndexChangedEventArgs e)
        {
            // MessageBox.Show(Foto.GetCurrentImageIndex().ToString());
        }
        
        // -------------------------------------------- lihat KIB ---------------------------------------------
        private void getDataKib()
        {
            ucKibAngk = new UcKibAngk(this.ID_KANGK, this.status);
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInpKib = new SvcKibAngkSelect.InputParameters();
            parInpKib.P_COL = "";
            
            parInpKib.P_MAX = 1;
            parInpKib.P_MAXSpecified = true;
            parInpKib.P_MIN = 0;
            parInpKib.P_MINSpecified = true;
            parInpKib.P_SORT = "DESC";
            parInpKib.STR_WHERE = String.Format(" ID_KANGK = {0} {1}", this.ID_KANGK, "");
            parInpKib.P_COUNT = "Y";
            Console.WriteLine(parInpKib.STR_WHERE);
            fetchDataKib = new SvcKibAngkSelect.call_pttClient();
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
        private delegate void ShowData(SvcKibAngkSelect.OutputParameters dataOut);
        public void showData(SvcKibAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KANGK_DOK_KIB.Count();
            if (jmlDataGroup <= 0)
            {
                MessageBox.Show("Dokumen tidak ada", "Perhatian!!!");
            }
            else
            {
                idDokumen = this.outDataKib.SF_ROW_M_KANGK_DOK_KIB[0].ID_KANGK_DOK_KIB;
                try
                {
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                    parInp.P_ID = this.idDokumen;
                    parInp.P_ID_TABLE = "ID_KANGK_DOK_KIB";
                    parInp.P_IDSpecified = true;
                    parInp.P_TABLE = "M_KANGK_DOK_KIB";
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
                System.IO.File.WriteAllBytes(this.outDataKib.SF_ROW_M_KANGK_DOK_KIB[0].NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(this.outDataKib.SF_ROW_M_KANGK_DOK_KIB[0].NMFILE);
                PuPdf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Dokumen KIB terakhir tidak ada", "Perhatian!!");
            }
        }
        #endregion ViewDokumen

        // --------------------------------------------------------------------------




        // --------------------------------- lihat dokumen ----------------------------
        protected override void sbLihatDok_Click(object sender, EventArgs e)
        {
            this.getDataDok();
        }

        private void getDataDok()
        {
            ucBpkbAngk = new UcBpkbAngk(this.ID_KANGK, this.status);
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInpDok = new SvcBpkbAngkSelect.InputParameters();
            parInpDok.P_COL = "";

            parInpDok.P_MAX = 1;
            parInpDok.P_MAXSpecified = true;
            parInpDok.P_MIN = 0;
            parInpDok.P_MINSpecified = true;
            parInpDok.P_SORT = "DESC";
            parInpDok.STR_WHERE = String.Format(" ID_KANGK = {0} {1}", this.ID_KANGK, "");
            parInpDok.P_COUNT = "Y";
            Console.WriteLine(parInpDok.STR_WHERE);
            fetchDataDok = new SvcBpkbAngkSelect.call_pttClient();
            fetchDataDok.Open();
            fetchDataDok.Beginexecute(parInpDok, new AsyncCallback(this.getResultDokumen), null);
        }

        protected void getResultDokumen(IAsyncResult result)
        {
            try
            {
                this.outDatDok = fetchDataDok.Endexecute(result);
                fetchDataDok.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowDataDok(this.showDataDok), this.outDatDok);
            }
            catch (Exception ex)
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show("Load data Dokumen Kepemilikan gagal", konfigApp.judulGagalAmbil);
            }
        }
        private delegate void ShowDataDok(SvcBpkbAngkSelect.OutputParameters dataOut);
        public void showDataDok(SvcBpkbAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KANGK_BPKB.Count();
            if (jmlDataGroup <= 0)
            {
                MessageBox.Show("Dokumen tidak ada", "Perhatian!!!");
            }
            else
            {
                idDokumen = this.outDatDok.SF_ROW_KANGK_BPKB[0].ID_KANGK_BPKB;
                try
                {
                    myThread_ = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread_.Start();
                    SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                    parInp.P_ID = this.idDokumen;
                    parInp.P_ID_TABLE = "ID_KANGK_BPKB";
                    parInp.P_IDSpecified = true;
                    parInp.P_TABLE = "M_KANGK_BPKB";
                    svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient();
                    svcAsetGetDokSelect.Open();
                    svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDokumenKep), null);
                }
                catch (Exception E)
                {
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                    //MessageBox.Show(E.Message);
                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }
            }

        }

        #region View Dokumen Kepemilikan


        private void getResultDokumenKep(IAsyncResult result)
        {
            try
            {
                this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
                svcAsetGetDokSelect.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowFileDokumen(this.showFileDokumen), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowFileDokumen(SvcAsetGetDokSelect.OutputParameters dataOut);

        public void showFileDokumen(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlDataGroup > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
                System.IO.File.WriteAllBytes(this.outDatDok.SF_ROW_KANGK_BPKB[0].NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(this.outDatDok.SF_ROW_KANGK_BPKB[0].NMFILE);
                PuPdf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Dokumen Kepemilikan terakhir tidak ada", "Perhatian!!");
            }
        }


        #endregion//ViewDokumen


        // ---------------------------------------- end lihat dokumen -------------------------------------------


        //-------------------------------------------lap kib ----------------------------------------------------
        protected override void sbLapKIB_Click(object sender, EventArgs e) {
            //
            ang_5101 = new ANG_5101();
            ang_5101.bsAngkutan.DataSource = this.selectedData;
            ang_5101.bsAngkutanFoto.DataSource=this.DafarFoto;
            ReportPrintTool pt;
            pt = new ReportPrintTool(ang_5101);
            pt.AutoShowParametersPanel = true;
            pt.ShowPreviewDialog();
        }
        //------------------------------------------- end lap kib ----------------------------------------------
    }
}
