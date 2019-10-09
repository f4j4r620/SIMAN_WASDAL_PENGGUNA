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
namespace AppPengguna.AST.TN
{
    public delegate void UpdateTanah(SvcAssetTanahCrud.OutputParameters dataOutTanahCrud);
    public  class ucTanahForm : ucDetailAsetForm
    {
      
        public ucIdentitasTanah identitas = new ucIdentitasTanah();
        private SvcAssetTanahSelect.BPSIMANSROW_M_KTNH selectedData;

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public UpdateTanah updateTanah;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        public ProgBar progressbar;
        public FrmKoorSatker MainFrm;

        TNH_5109 upg1205;
        TNH_5110 tnh5110;

        private Thread myThread;
        private Thread myThread_;
        private char modeCrud;
        private string KD_KONDISI;
        private string KD_KAB;
        private string KD_PROV;
        private decimal? ID_SATKER;
        public decimal? ID_KTNH;
     
        
        private decimal? ID_SATKER_PMK;
        private string KD_STATUS;
        private decimal? ID_KANWIL;
        private decimal? ID_KORWIL;
        private decimal? ID_ESELON1;
        private decimal? ID_KL;
        private decimal? ID_PENGADAAN;
        private decimal? idDokumen;
        private decimal? idDokumen_;
        
        private int ulang = 0;
        private SvcAssetTanahCrud.call_pttClient svcbangunanCrud;
        private SvcAssetTanahCrud.OutputParameters outDataCrud;
        private SvcAssetTanahSelect.call_pttClient fetchDataTanah;
        private SvcAssetTanahSelect.OutputParameters outDataTanahSelect;

        //------------- GET DOKUMEN & KIB -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        private SvcTnhDokKibSelect.execute_pttClient fetchDataKib;
        private SvcTnhDokKibSelect.InputParameters parInpKib;
        private SvcTnhDokKibSelect.OutputParameters outDataKib;
        public SvcTnhDokKibSelect.BPSIMANSROW_M_KTNH_DOK_KIB selectedDataKib;

        private SvcTnhDokSelect.call_pttClient fetchDataDok;
        private SvcTnhDokSelect.InputParameters parInpDok;
        private SvcTnhDokSelect.OutputParameters outDataDok;
        public SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK selectedDataDok;
        //-------------------------------------------------------

        // ------------------------ get Lap KIB tanah -----------------------------
        #region svc lap kib 
        public SvcAssetTanahSelect.BPSIMANSROW_M_KTNH assetTanahPilihan;
        public SvcAssetTanahSelect.call_pttClient ambilAssetTanah = null;
        public SvcAssetTanahSelect.OutputParameters dataOutAssetTanah = null;
        public SvcAssetTanahCrud.call_pttClient svcAssetTanahCrud = null;
        public SvcAssetTanahCrud.OutputParameters dataOutAssetTanahCrud = null;
        private SvcTnhDokSelect.call_pttClient fetchData;
        private SvcTnhDokSelect.InputParameters parInp;
        private SvcTnhDokSelect.OutputParameters outData;
        public SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK selectedDataTnh;
        private SvcTnhLokSelect.call_pttClient fetchDataTanahLok;
        private SvcTnhLokSelect.InputParameters parInpTanahLok;
        private SvcTnhLokSelect.OutputParameters outDataTanahLok;
        public SvcTnhLokSelect.BPSIMANSROW_M_KTNH_LOKASI selectedDataTanahLok;
        private SvcTnhBangunanSelect.call_pttClient fetchDataTnhBangunan;
        private SvcTnhBangunanSelect.InputParameters parInpTnhBangunan;
        private SvcTnhBangunanSelect.OutputParameters outDataTnhBangunan;
        public SvcTnhBangunanSelect.BPSIMANSROW_M_KTNH_BANGUNAN selectedDataTnhBangunan;
        private SvcTnhFasPenunjangSelect.call_pttClient fetchDataTnhFas;
        private SvcTnhFasPenunjangSelect.InputParameters parInTnhFasp;
        private SvcTnhFasPenunjangSelect.OutputParameters outDataTnhFas;
        public SvcTnhFasPenunjangSelect.BPSIMANSROW_M_KTNH_FAS_PENUNJANG selectedDataTnhFas;
        private SvcTnhRwyPenilaianSelect.call_pttClient fetchDataRPenilaian;
        private SvcTnhRwyPenilaianSelect.InputParameters parInpRPenilaian;
        private SvcTnhRwyPenilaianSelect.OutputParameters outDataRPenilaian;
        private SvcTnhRwyPenilaianSelect.BPSIMANSROW_M_KTNH_RWYT_NILAI selectedDataRPenilaian;
        private SvcTnhNjopSelect.call_pttClient fetchDataNJOP;
        private SvcTnhNjopSelect.InputParameters parInpNJOP;
        private SvcTnhNjopSelect.OutputParameters outDataNJOP;
        public SvcTnhNjopSelect.BPSIMANSROW_M_KTNH_NJOP selectedDataNJOP;
        private SvcTnhRwyPenggunaSelect.call_pttClient fetchDataRwytPengguna;
        private SvcTnhRwyPenggunaSelect.InputParameters parInpRwytPengguna;
        private SvcTnhRwyPenggunaSelect.OutputParameters outDataRwytPengguna;
        public SvcTnhRwyPenggunaSelect.BPSIMANSROW_M_KTNH_RWYT_PENGGUNA selectedDataRwytPengguna;
        private SvcTnhRwyMutasiSelect.call_pttClient fetchDataRwyMutasi;
        private SvcTnhRwyMutasiSelect.InputParameters parInpRwyMutasi;
        private SvcTnhRwyMutasiSelect.OutputParameters outDataRwyMutasi;
        public SvcTnhRwyMutasiSelect.BPSIMANSROW_M_KTNH_RWYT_MUTASI selectedDataRwyMutasi;
        private SvcTnhRwyPeliharaSelect.call_pttClient fetchDataRwytPelihara;
        private SvcTnhRwyPeliharaSelect.InputParameters parInpRwytPelihara;
        private SvcTnhRwyPeliharaSelect.OutputParameters outDataRwytPelihara;
        private SvcTnhRwyPeliharaSelect.BPSIMANSROW_M_KTNH_RWYT_PELIHARA selectedDataRwytPelihara;
        #endregion

        #region Variable Master Aset Tanah
        ucTanah uctanah;
        ucTanahForm TanahForm;
        public bool dataInisial = true;
        private decimal dataAwal = 1;
        private decimal dataAkhir = 20;
        private decimal currentMaks = 20;
        private decimal currentMin = 1;
        #endregion
        //- ------------------------------------------------------------------------

        ucTnhDok ucdokTanah;
        ucTnhLok ucLokTanah;
        ucTnhFas ucTnhFas;
        ucTnhRPemeliharaan ucrPemeliharaanTanah;
        ucTnhRPenilaian ucrNilaiTanah;
        ucTnhRMutasi ucrMutasiTanah;
        ucTnhRPengguna ucrPenggunaTanah;
        ucTnhBangunan ucriwayatTanah;

        #region PopUp Form
        private FrmPUSatker PuSatker;
        private FrmPUKondisi PuKondisi;
        private FrmPUSskel PuSskel;
        #endregion

        #region Variable Aktif Tab
        private bool status_FasPenunjang;
        private bool status_DokTanah;
        private bool status_LokTanah;
        private bool status_GPS;
        private bool status_DetailRuangan;
        private bool status_KonstruksiTanah;
        private bool status_RiwayatPenilaian;
        private bool status_NJOP;
        private bool status_RiwayatTanah;
        private bool status_RiwayatPengguna;
        private bool status_RiwayatMutasi;
        private bool status_RiwayatPemeliharaan;
        private bool status_PermasalahanHukum;
        private bool status_DokKib;
        private bool status_Spm;
        private bool status_Asuransi;
        private bool status_Foto;
        #endregion


        public ucTanahForm(string status)
        {
           
            this.status = status;
          
        }

        public bool IsiForm(SvcAssetTanahSelect.BPSIMANSROW_M_KTNH Data)
        {
            this.selectedData = Data;
            try
            {
                this.ID_SATKER = selectedData.ID_SATKER;
                this.ID_KTNH = selectedData.ID_KTNH;
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
               this.identitas.teJalan.Text = selectedData.LOKASI;
               this.identitas.teRTRW.Text = selectedData.KD_RTRW;
               //this.identitas.teKomplek.Text = selectedData.KOMPLEK;
               this.identitas.teKelurahan.Text = selectedData.KD_KEL;
               this.identitas.teKecamatan.Text = selectedData.KD_KEC;
               this.identitas.teAlamat.Text = selectedData.ALAMAT;
               //-----KODE----
               this.KD_KAB = selectedData.KD_KAB;
               this.KD_PROV = selectedData.KD_PROV;
               this.identitas.teKabupaten.Text = selectedData.UR_KAB;
               this.identitas.teProvinsi.Text = selectedData.UR_PROV;
               this.identitas.teKodePos.Text = selectedData.KD_POS;

               // //-----------------  Dokumen -----------------------------------
               this.identitas.teStatusDokumen.Text = selectedData.UR_DOK;
               this.identitas.teJenisDokumen.Text = selectedData.UR_SMILIK;
               this.identitas.teJns_serti.Text = selectedData.NM_JNS_SERTI;
               this.identitas.teNomorDokumen.Text = selectedData.NO_SERTIFIKAT;
               this.identitas.teTanggalDokumen.Text = konfigApp.DateToString(selectedData.TGL_DOK);
               //this.identitas.teTanggalGuna.Text = konfigApp.DateToString(selectedData.TGL_GUNA);
               //this.identitas.teTanggalRenovasi.Text = konfigApp.DateToString(selectedData.TGL_RENOV);
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
               this.identitas.teLuasBangunan.Text = selectedData.LUAS_TNHB.ToString();
               this.identitas.teLuasLingkungan.Text = selectedData.LUAS_TNHL.ToString();
               this.identitas.teLuasKosong.Text = selectedData.LUAS_TNHK.ToString();
               this.identitas.teLuasSeluruhnya.Text = selectedData.LUAS_TNHS.ToString();
               this.identitas.teJumlahBanguan.Text = selectedData.JML_BANGUNAN.ToString();

               // //----- NILAI(DALAM RUPIAH) -----
               this.identitas.teNilaiPerolehan.Text = selectedData.RPH_ASET.ToString();
               this.identitas.teNilaiMutasi.Text = selectedData.RPH_MUTASI.ToString();
               try
               {
                   this.identitas.teNilaiSebelumPenyusutan.Text = (selectedData.RPH_ASET + Math.Abs(selectedData.RPH_MUTASI.Value)).ToString();
               }
               catch (Exception)
               {
                   this.identitas.teNilaiSebelumPenyusutan.Value = 0;
               }
               this.identitas.teNilaiPenyusutan.Text = selectedData.RPH_SUSUT.ToString();
               try
               {
                   this.identitas.teNilaiBuku.Text = (selectedData.RPH_ASET + Math.Abs(selectedData.RPH_MUTASI.Value) - selectedData.RPH_SUSUT).ToString();
               }
               catch (Exception)
               {
                   this.identitas.teNilaiBuku.Value = 0;
               }

               //this.identitas.teTanggalBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU); //dihapus
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
                        SvcAssetTanahCrud.InputParameters parInp = new SvcAssetTanahCrud.InputParameters();

                        

                        #region Data yang tidak dapat diubah
                        parInp.P_ALAMAT = selectedData.ALAMAT;
                        parInp.P_FOTO = selectedData.FOTO;
                        parInp.P_FLAG_KOR = selectedData.FLAG_KOR;
                        parInp.P_FLAG_KRM = selectedData.FLAG_KRM;
                        parInp.P_FLAG_TTP = selectedData.FLAG_TTP;
                        parInp.P_ID_ASET = selectedData.ID_ASET;
                        parInp.P_ID_PENGADAAN = selectedData.ID_PENGADAAN;
                        parInp.P_ID_PENGADAANSpecified = true;
                        parInp.P_ID_ASETSpecified = true;
                        parInp.P_ID_SATKER = selectedData.ID_SATKER;
                        parInp.P_JML_BANGUNAN = selectedData.JML_BANGUNAN;
                        parInp.P_ID_SATKERSpecified = true;
                        parInp.P_JML_BANGUNANSpecified = true;
                   
                        parInp.P_KD_BRG = selectedData.KD_BRG;
                        parInp.P_KD_DATA = selectedData.KD_DATA;
                        parInp.P_KD_DSR_HRG = selectedData.KD_DSR_HRG;
                        parInp.P_KD_JNS_BMN = selectedData.KD_JNS_BMN;
                        parInp.P_KD_JNS_BMNSpecified = true;
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
                        parInp.P_LOKASI = selectedData.LOKASI;
                        parInp.P_LUAS_TNHB = selectedData.LUAS_TNHB;
                        parInp.P_LUAS_TNHBSpecified = true;
                        parInp.P_LUAS_TNHK = selectedData.LUAS_TNHK;
                        parInp.P_LUAS_TNHKSpecified = true;
                        parInp.P_LUAS_TNHL = selectedData.LUAS_TNHL;
                        parInp.P_LUAS_TNHLSpecified = true;
                        parInp.P_LUAS_TNHS = selectedData.LUAS_TNHS;
                        parInp.P_LUAS_TNHSSpecified = true;
                       
                        parInp.P_MERK = selectedData.MERK;
                        parInp.P_NO_ASET = selectedData.NO_ASET;
                        parInp.P_NO_KIB = selectedData.NO_KIB;
                        parInp.P_NOREG = selectedData.NOREG;
                        parInp.P_PERIODE = selectedData.PERIODE;
                        parInp.P_RPH_ASET = selectedData.RPH_ASET;
                        parInp.P_RPH_ASETSpecified = true;
                        parInp.P_RPH_M2Specified = true;
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
                        parInp.P_RPH_M2 = selectedData.RPH_M2;
                        
                        parInp.P_STATUS_BMN_YN = selectedData.STATUS_BMN_YN;
                        parInp.P_TERCATAT = selectedData.TERCATAT;
                        //parInp.P_TGL_REKAM = konfigApp.DateToString(selectedData.TGL_REKAM);

                        parInp.P_THN_ANG = selectedData.THN_ANG;
                        parInp.P_TIPE = selectedData.TIPE;
                        parInp.P_UMEKO = selectedData.UMEKO;
                        parInp.P_UMEKOSpecified = true;
                        parInp.P_KUANTITAS = selectedData.KUANTITAS;
                        parInp.P_KUANTITASSpecified = true;
                        #endregion

                        #region Data isi
                        parInp.P_CATATAN = this.identitas.teCatatan.Text.Trim();
                        parInp.P_KD_POS = this.identitas.teKodePos.Text.Trim();
                        parInp.P_LOKASI = this.identitas.teJalan.Text.Trim();
                        
                        parInp.P_ALAMAT = this.identitas.teAlamat.Text.Trim();

                        #endregion


                        parInp.P_SELECT = "U";
                        this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                        svcbangunanCrud = new SvcAssetTanahCrud.call_pttClient(konfigApp.SvcAssetTanahCrud_name,konfigApp.SvcAssetTanahCrud_address);
                        svcbangunanCrud.Open();
                        svcbangunanCrud.Beginexecute(parInp, new AsyncCallback(crudTanah), "");
                        
                        
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

        private void crudTanah(IAsyncResult result)
        {
            try
            {
                outDataCrud = svcbangunanCrud.Endexecute(result);
                svcbangunanCrud.Close();
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
        private delegate void SimpanData(SvcAssetTanahCrud.OutputParameters dataOutTanahCrud);

        private void simpanData(SvcAssetTanahCrud.OutputParameters dataOutTanahCrud)
        {
            this.updateTanah(dataOutTanahCrud);
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
            this.TabRiwayatBangunan.PageVisible = false;
            this.TabRiwayatPenilaian.Text = "Penilaian";
            this.TabPermasalahanHukum.Text = "Status Hukum";
            this.TabRiwayatMutasi.Text = "Mutasi";
            this.TabRiwayatNJOP.Text = "Dokumen NJOP";
            this.TabRiwayatPemeliharaan.Text = "Pemeliharaan";
            this.TabRiwayatPengelolaan.Text = "Pengelolaan";
            
            this.TabDaftarBangunan.Text = "Bangunan";
            this.TabPemakai.PageVisible = false;
            this.TabKonstruksi.PageVisible = false;
            this.TabSusut.PageVisible = false;
            this.TabAsuransi.PageVisible = false;

           
            //sbLapKIB.Click += new DevExpress.XtraBars.ItemClickEventHandler(this.test);

            Foto.AnimationTime = 500;

            Foto.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            if (this.status == "edit" || this.status == "input")
            {

                this.FormReadOnly(true);
                this.identitas.teKodePos.Properties.ReadOnly = false;
                //this.identitas.teCatatan.Properties.ReadOnly = false;
                this.identitas.teAlamat.Properties.ReadOnly = false;
                this.Foto.Enabled = true;
                
            }
            else
            {

                this.FormReadOnly(true);
              
            }
            this.Foto.Enabled = true;
            this.btnTampilkanFoto.Enabled = true;
            this.getInitPhoto(1);
            this.btnHapusFoto.Enabled = false;
            this.BtnUnggahFoto.Enabled = false;
        }

        private void test() 
        {
            MessageBox.Show("check button laporan KIB", "TESTING...");
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
           
            if (TabDetail.SelectedTabPage == this.TabDokumen && status_DokTanah == false)
            {
                
                status_DokTanah = true;
                ucdokTanah = new ucTnhDok(this.ID_KTNH,this.status);
                ucdokTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucdokTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucdokTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucdokTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                
                ucdokTanah.Dock = DockStyle.Fill;
                TabDokumen.Controls.Clear();
                TabDokumen.Controls.Add(ucdokTanah);
            }
            else if (TabDetail.SelectedTabPage == this.TabLokasi && status_LokTanah == false)
            {
                status_LokTanah = true;
                ucLokTanah = new ucTnhLok(this.ID_KTNH, this.status);
                ucLokTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucLokTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucLokTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucLokTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucLokTanah.nama_aset = this.teNamaBarang.Text ;
                ucLokTanah.Dock = DockStyle.Fill;
                TabLokasi.Controls.Clear();
                TabLokasi.Controls.Add(ucLokTanah);
            }
           
            else if (TabDetail.SelectedTabPage == this.TabDaftarBangunan && status_DetailRuangan == false)
            {
                status_DetailRuangan = true;
                ucTnhBangunan ucdRuanganTanah = new ucTnhBangunan(this.ID_KTNH, this.status);
           
                ucdRuanganTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucdRuanganTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucdRuanganTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucdRuanganTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucdRuanganTanah.Dock = DockStyle.Fill;
                TabDaftarBangunan.Controls.Clear();
                TabDaftarBangunan.Controls.Add(ucdRuanganTanah);
            }
            else if (TabDetail.SelectedTabPage == this.TabFasilitasPenunjang && status_FasPenunjang == false)
            {
                status_FasPenunjang = true;
                ucTnhFas = new ucTnhFas(this.ID_KTNH, this.status);
                ucTnhFas.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucTnhFas.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucTnhFas.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucTnhFas.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucTnhFas.Dock = DockStyle.Fill;
                TabFasilitasPenunjang.Controls.Clear();
                TabFasilitasPenunjang.Controls.Add(ucTnhFas);
            }
            
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPemeliharaan && status_RiwayatPemeliharaan == false)
            {
                status_RiwayatPemeliharaan = true;
                ucrPemeliharaanTanah = new ucTnhRPemeliharaan(this.ID_KTNH, this.status);
                ucrPemeliharaanTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrPemeliharaanTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrPemeliharaanTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrPemeliharaanTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucrPemeliharaanTanah.Dock = DockStyle.Fill;
                TabRiwayatPemeliharaan.Controls.Clear();
                TabRiwayatPemeliharaan.Controls.Add(ucrPemeliharaanTanah);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPenilaian && status_RiwayatPenilaian == false)
            {
                status_RiwayatPenilaian = true;
                ucrNilaiTanah = new ucTnhRPenilaian(this.ID_KTNH, this.status);
                ucrNilaiTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrNilaiTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrNilaiTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrNilaiTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrNilaiTanah.Dock = DockStyle.Fill;
                TabRiwayatPenilaian.Controls.Clear();
                TabRiwayatPenilaian.Controls.Add(ucrNilaiTanah);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatNJOP && status_NJOP == false)
            {
                status_NJOP = true;
                ucTnhNjop ucnJopTanah = new ucTnhNjop(this.ID_KTNH, this.status);
                ucnJopTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucnJopTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucnJopTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucnJopTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucnJopTanah.Dock = DockStyle.Fill;
                TabRiwayatNJOP.Controls.Clear();
                TabRiwayatNJOP.Controls.Add(ucnJopTanah);
            }
            else if (TabDetail.SelectedTabPage == this.TabRiwayatMutasi && status_RiwayatMutasi == false)
            {
                status_RiwayatMutasi = true;
                ucrMutasiTanah = new ucTnhRMutasi(this.ID_KTNH, this.status);
                ucrMutasiTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrMutasiTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrMutasiTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrMutasiTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrMutasiTanah.Dock = DockStyle.Fill;
                TabRiwayatMutasi.Controls.Clear();
                TabRiwayatMutasi.Controls.Add(ucrMutasiTanah);
            }
           
            else if (TabDetail.SelectedTabPage == this.TabRiwayatPengelolaan && status_RiwayatPengguna == false)
            {
                status_RiwayatPengguna = true;
                ucrPenggunaTanah = new ucTnhRPengguna(this.ID_KTNH,this.status);
                ucrPenggunaTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucrPenggunaTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucrPenggunaTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucrPenggunaTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                ucrPenggunaTanah.Dock = DockStyle.Fill;
                TabRiwayatPengelolaan.Controls.Clear();
                TabRiwayatPengelolaan.Controls.Add(ucrPenggunaTanah);
            }
            else if (TabDetail.SelectedTabPage == this.TabPermasalahanHukum && status_PermasalahanHukum == false)
            {
                status_PermasalahanHukum = true;
                ucTnhHukum ucpHukumTanah = new ucTnhHukum(this.ID_KTNH, this.status);
                ucpHukumTanah.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucpHukumTanah.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucpHukumTanah.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucpHukumTanah.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
              
                ucpHukumTanah.Dock = DockStyle.Fill;
                TabPermasalahanHukum.Controls.Clear();
                TabPermasalahanHukum.Controls.Add(ucpHukumTanah);
            }
            else if (TabDetail.SelectedTabPage == this.TabDokumenKib && status_DokKib == false)
            {
                status_DokKib = true;
                ucTnhDokKib ucpTnhDokKib = new ucTnhDokKib(this.ID_KTNH, this.status);
                ucpTnhDokKib.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                ucpTnhDokKib.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                ucpTnhDokKib.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                ucpTnhDokKib.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                ucpTnhDokKib.Dock = DockStyle.Fill;
                TabDokumenKib.Controls.Clear();
                TabDokumenKib.Controls.Add(ucpTnhDokKib);
            }
            else if (TabDetail.SelectedTabPage == this.TabSpm && status_Spm == false)
            {
                status_Spm = true;
                ucTnhSpm uctnhSpm = new ucTnhSpm(this.ID_KTNH, this.status);
                uctnhSpm.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                uctnhSpm.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                uctnhSpm.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                uctnhSpm.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                uctnhSpm.Dock = DockStyle.Fill;
                TabSpm.Controls.Clear();
                TabSpm.Controls.Add(uctnhSpm);
            }
            else if (TabDetail.SelectedTabPage == this.TabFoto && status_Foto == false)
            {
                status_Foto = true;
                ucTnhFoto uctnhFoto = new ucTnhFoto(this.ID_ASET, this.status);
                uctnhFoto.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                uctnhFoto.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                uctnhFoto.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                uctnhFoto.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);

                uctnhFoto.Dock = DockStyle.Fill;
                TabFoto.Controls.Clear();
                TabFoto.Controls.Add(uctnhFoto);
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
            //this.teKdPenggunaTanah.Text = kode;
            //this.teNamaPenggunaTanah.Text = nama;
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

        //private void btnTanah_Click(object sender, EventArgs e)
        //{
        //    FrmPUTanah PuTanah = new FrmPUTanah(this.MainFrm);
        //    PuTanah.ambilTanah = new AmbilTanah(this.ambilTanah);
        //    PuTanah.ShowDialog();
        //}

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
                this.btnHapusFoto.Enabled = true;
                this.BtnUnggahFoto.Enabled = true;
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
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                
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



        private void getDataKib()
        {
            ucTnhDokKib ucpTnhDokKib = new ucTnhDokKib(this.ID_KTNH, this.status);
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInpKib = new SvcTnhDokKibSelect.InputParameters();
            parInpKib.P_COL = "";

            parInpKib.P_MAX = 1;
            parInpKib.P_MAXSpecified = true;
            parInpKib.P_MIN = 0;
            parInpKib.P_MINSpecified = true;
            parInpKib.P_SORT = "DESC";
            parInpKib.STR_WHERE = String.Format(" ID_KTNH = {0} {1}", this.ID_KTNH, "");
            parInpKib.P_COUNT = "Y";
            Console.WriteLine(parInpKib.STR_WHERE);
            fetchDataKib = new SvcTnhDokKibSelect.execute_pttClient();
            fetchDataKib.Open();
            fetchDataKib.Beginexecute(parInpKib, new AsyncCallback(this.getResultKib), null);
           // outDataKib = fetchDataKib.execute(parInpKib);
           // idDokumen = this.outDataKib.SF_ROW_M_KTNH_DOK_KIB[0].ID_KTNH_DOK_KIB;
        }

        private void getResultKib(IAsyncResult result)
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
        private delegate void ShowData(SvcTnhDokKibSelect.OutputParameters dataOut);
        public void showData(SvcTnhDokKibSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_DOK_KIB.Count();
            if (jmlDataGroup <= 0)
            {
                MessageBox.Show("Dokumen tidak ada", "Perhatian!!!");
            }
            else
            {
                 idDokumen = this.outDataKib.SF_ROW_M_KTNH_DOK_KIB[0].ID_KTNH_DOK_KIB;
                 try
                 {
                     myThread_ = new Thread(new ThreadStart(this.ShowProgresBar));
                     myThread_.Start();
                     SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                     parInp.P_ID = this.idDokumen;
                     parInp.P_ID_TABLE = "ID_KTNH_DOK_KIB";
                     parInp.P_IDSpecified = true;
                     parInp.P_TABLE = "M_KTNH_DOK_KIB";
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


        #region View KIB
       
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
                System.IO.File.WriteAllBytes(this.outDataKib.SF_ROW_M_KTNH_DOK_KIB[0].NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(this.outDataKib.SF_ROW_M_KTNH_DOK_KIB[0].NMFILE);
                PuPdf.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Dokumen KIB terakhir tidak ada", "Perhatian!!");
            }
        }


//-------------------------------------- lihat tab dokumen ---------------------------------------------

        private void getDataDok()
        {
            ucdokTanah = new ucTnhDok(this.ID_KTNH, this.status);
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInpDok = new SvcTnhDokSelect.InputParameters();
            parInpDok.P_COL = "";

            parInpDok.P_MAX = 1;
            parInpDok.P_MAXSpecified = true;
            parInpDok.P_MIN = 0;
            parInpDok.P_MINSpecified = true;
            parInpDok.P_SORT = "DESC";
            parInpDok.STR_WHERE = String.Format(" ID_KTNH = {0} {1}", this.ID_KTNH, "");
            parInpDok.P_COUNT = "Y";
            Console.WriteLine(parInpDok.STR_WHERE);
            fetchDataDok = new SvcTnhDokSelect.call_pttClient();
            fetchDataDok.Open();
            fetchDataDok.Beginexecute(parInpDok, new AsyncCallback(this.getResultDokumen), null);
        }

        protected void getResultDokumen(IAsyncResult result)
        {
            try
            {
                this.outDataDok = fetchDataDok.Endexecute(result);
                fetchDataDok.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowDataDok(this.showDataDok), this.outDataDok);
            }
            catch (Exception ex)
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show("Load data Dokumen gagal", konfigApp.judulGagalAmbil);
            }
        }
        private delegate void ShowDataDok(SvcTnhDokSelect.OutputParameters dataOut);
        public void showDataDok(SvcTnhDokSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_ASET_M_KTNH_DOK.Count();
            if (jmlDataGroup <= 0)
            {
                MessageBox.Show("Dokumen tidak ada", "Perhatian!!!");
            }
            else
            {
                idDokumen_ = this.outDataKib.SF_ROW_M_KTNH_DOK_KIB[0].ID_KTNH_DOK_KIB;
                try
                {
                    myThread_ = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread_.Start();
                    SvcAsetGetDokSelect.InputParameters parInpDok = new SvcAsetGetDokSelect.InputParameters();
                    parInpDok.P_ID = idDokumen_;
                    parInpDok.P_ID_TABLE = "ID_KTNH_DOK";
                    parInpDok.P_IDSpecified = true;
                    parInpDok.P_TABLE = "M_KTNH_DOK";
                    svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient();
                    svcAsetGetDokSelect.Open();
                    svcAsetGetDokSelect.Beginexecute(parInpDok, new AsyncCallback(this.getResultDoku), null);
                }
                catch (Exception E)
                {
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                    
                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }
            }

        }

       

        #endregion//ViewDokumen

        protected override void sbLihatDok_Click(object sender, EventArgs e)
        {
            this.getDataDok();
        }

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
                System.IO.File.WriteAllBytes(this.outDataDok.SF_ROW_ASET_M_KTNH_DOK[0].NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(this.outDataDok.SF_ROW_ASET_M_KTNH_DOK[0].NMFILE);
                PuPdf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Dokumen terakhir tidak ada", "Perhatian!!");
            }
        }


        #endregion//ViewDokumen



        //---------------------------------------------- Laporan KIB Tanah -----------------------------------------
        protected override void sbLapKIB_Click(object sender, EventArgs e) 
        {
            //
            upg1205 = new TNH_5109();
            upg1205.bsTanah.DataSource = this.selectedData;
            tnh5110 = new TNH_5110();
            getInitTnhDok();
            getInitTnhLok();
            getInitTnhBgn();
            getInitTnhFas();
            getInitTnhRPnl();
            getInitTnhNjop();
            getInitTnhRPgn();
            getInitTnhRMutasi();
            getInitTnhRPml();
            tnh5110.bsTanah.DataSource = this.selectedData;
            upg1205.bsFoto.DataSource = this.DafarFoto;
            ReportPrintTool pt;
            pt = new ReportPrintTool(upg1205);
            pt.AutoShowParametersPanel = true;
            pt.ShowPreviewDialog();
            pt = new ReportPrintTool(tnh5110);
            pt.AutoShowParametersPanel = true;
            pt.ShowPreviewDialog();
        }

        #region load data Dokumen Tanah
        public void getInitTnhDok(string _search = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            parInp = new SvcTnhDokSelect.InputParameters();
            parInp.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            if (_search != null)
            {
                //this.search = _search;
            }
            else
            {
                //this.search = "";
            }
            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_SORT = "";
            parInp.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            Console.WriteLine(parInp.STR_WHERE);
            fetchData = new SvcTnhDokSelect.call_pttClient(konfigApp.SvcTnhDokSelect_name, konfigApp.SvcTnhDokSelect_address);
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResultTanahDok), null);
        }

        protected void getResultTanahDok(IAsyncResult result)
        {
            try
            {
                this.outData = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new ShowDataThn(this.showDataTnh), this.outData);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataThn(SvcTnhDokSelect.OutputParameters dataOut);

        public void showDataTnh(SvcTnhDokSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_ASET_M_KTNH_DOK.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[i]);
            }

            if (jmlDataGroup < konfigApp.dataAkhir)
            {
                //this.loadmore = false;
            }
            else
            {
                //this.loadMore = true;                
            }
            tnh5110.bsDokTanah.DataSource = serviceOutPut.SF_ROW_ASET_M_KTNH_DOK;
        }

        #endregion

        #region load data Lokasi
        public void getInitTnhLok()
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            parInpTanahLok = new SvcTnhLokSelect.InputParameters();
            parInpTanahLok.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            parInpTanahLok.P_MAX = this.currentMaks;
            parInpTanahLok.P_MAXSpecified = true;
            parInpTanahLok.P_MIN = this.currentMin;
            parInpTanahLok.P_MINSpecified = true;
            parInpTanahLok.P_SORT = "";
            parInpTanahLok.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            fetchDataTanahLok = new SvcTnhLokSelect.call_pttClient(konfigApp.SvcTnhLokasiSelect_name, konfigApp.SvcTnhLokasiSelect_address);
            fetchDataTanahLok.Open();
            fetchDataTanahLok.Beginexecute(parInpTanahLok, new AsyncCallback(this.getResultTanahLok), null);
        }

        protected void getResultTanahLok(IAsyncResult result)
        {
            try
            {
                this.outDataTanahLok = fetchDataTanahLok.Endexecute(result);
                fetchDataTanahLok.Close();
                this.Invoke(new ShowDataTanahLok(this.showDataTanahLok), this.outDataTanahLok);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataTanahLok(SvcTnhLokSelect.OutputParameters dataOut);

        public void showDataTanahLok(SvcTnhLokSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_LOKASI.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_M_KTNH_LOKASI[i]);
            }

            if (jmlDataGroup < 5)
            {
                //this.loadMore = false;
            }
            tnh5110.bsLokTanah.DataSource = serviceOutPut.SF_ROW_M_KTNH_LOKASI;
        }
        #endregion

        #region load data Tanah Bangunan
        public void getInitTnhBgn()
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();

            parInpTnhBangunan = new SvcTnhBangunanSelect.InputParameters();
            parInpTnhBangunan.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            parInpTnhBangunan.P_MAX = this.currentMaks;
            parInpTnhBangunan.P_MAXSpecified = true;
            parInpTnhBangunan.P_MIN = this.currentMin;
            parInpTnhBangunan.P_MINSpecified = true;
            parInpTnhBangunan.P_SORT = "";
            parInpTnhBangunan.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            fetchDataTnhBangunan = new SvcTnhBangunanSelect.call_pttClient(konfigApp.SvcTnhBangunanSelect_name, konfigApp.SvcTnhBangunanSelect_address);
            fetchDataTnhBangunan.Open();
            fetchDataTnhBangunan.Beginexecute(parInpTnhBangunan, new AsyncCallback(this.getResultTanahBangunan), null);
        }

        protected void getResultTanahBangunan(IAsyncResult result)
        {
            try
            {
                this.outDataTnhBangunan = fetchDataTnhBangunan.Endexecute(result);
                fetchDataTnhBangunan.Close();
                this.Invoke(new ShowDataTnhBangunan(this.showDataTnhBangunan), this.outDataTnhBangunan);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataTnhBangunan(SvcTnhBangunanSelect.OutputParameters dataOut);

        public void showDataTnhBangunan(SvcTnhBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_BANGUNAN.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_M_KTNH_BANGUNAN[i]);
            }

            if (jmlDataGroup < 5)
            {
                //this.loadMore = false;
            }
            tnh5110.bsBangunan.DataSource = serviceOutPut.SF_ROW_M_KTNH_BANGUNAN;
        }
        #endregion

        #region load data Fasilitas Penunjang
        public void getInitTnhFas()
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            parInTnhFasp = new SvcTnhFasPenunjangSelect.InputParameters();
            parInTnhFasp.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            parInTnhFasp.P_MAX = this.currentMaks;
            parInTnhFasp.P_MAXSpecified = true;
            parInTnhFasp.P_MIN = this.currentMin;
            parInTnhFasp.P_MINSpecified = true;
            parInTnhFasp.P_SORT = "";
            parInTnhFasp.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            fetchDataTnhFas = new SvcTnhFasPenunjangSelect.call_pttClient(konfigApp.SvcTnhFasSelect_name, konfigApp.SvcTnhFasSelect_address);
            fetchDataTnhFas.Open();
            fetchDataTnhFas.Beginexecute(parInTnhFasp, new AsyncCallback(this.getResultTnhFas), null);
        }

        protected void getResultTnhFas(IAsyncResult result)
        {
            try
            {
                this.outDataTnhFas = fetchDataTnhFas.Endexecute(result);
                fetchDataTnhFas.Close();
                this.Invoke(new ShowDataTanahFas(this.showDataTanahFas), this.outDataTnhFas);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataTanahFas(SvcTnhFasPenunjangSelect.OutputParameters dataOut);

        public void showDataTanahFas(SvcTnhFasPenunjangSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG[i]);
            }

            if (jmlDataGroup < 5)
            {
                //this.loadMore = false;
            }
            tnh5110.bsFasPenunjang.DataSource = serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG;
        }
        #endregion

        #region load data Riwayat Penilaian
        public void getInitTnhRPnl(string _search = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            parInpRPenilaian = new SvcTnhRwyPenilaianSelect.InputParameters();
            parInpRPenilaian.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            if (_search != null)
            {
                //this.search = _search;
            }
            else
            {
                //this.search = "";
            }
            parInpRPenilaian.P_MAX = this.currentMaks;
            parInpRPenilaian.P_MAXSpecified = true;
            parInpRPenilaian.P_MIN = this.currentMin;
            parInpRPenilaian.P_MINSpecified = true;
            parInpRPenilaian.P_SORT = "";
            parInpRPenilaian.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            fetchDataRPenilaian = new SvcTnhRwyPenilaianSelect.call_pttClient(konfigApp.SvcTnhRwyPenilaianSelect_name, konfigApp.SvcTnhRwyPenilaianSelect_address);
            fetchDataRPenilaian.Open();
            fetchDataRPenilaian.Beginexecute(parInpRPenilaian, new AsyncCallback(this.getResultRPenilaian), null);
        }

        protected void getResultRPenilaian(IAsyncResult result)
        {
            try
            {
                this.outDataRPenilaian = fetchDataRPenilaian.Endexecute(result);
                fetchDataRPenilaian.Close();
                this.Invoke(new ShowDataRPenilaian(this.showDataRPenilaian), this.outDataRPenilaian);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataRPenilaian(SvcTnhRwyPenilaianSelect.OutputParameters dataOut);

        public void showDataRPenilaian(SvcTnhRwyPenilaianSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_RWYT_NILAI.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_M_KTNH_RWYT_NILAI[i]);
            }

            if (jmlDataGroup < konfigApp.dataAkhir)
            {
                //this.loadMore = false;
                //this.bbMore.Enabled = false;
            }
            else
            {
                //this.loadMore = true;
                //this.bbMore.Enabled = true;
            }
            tnh5110.bsRiwayatPenilaian.DataSource = serviceOutPut.SF_ROW_M_KTNH_RWYT_NILAI;
        }
        #endregion

        #region load data Riwayat NJOP
        public void getInitTnhNjop()
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            parInpNJOP = new SvcTnhNjopSelect.InputParameters();
            parInpNJOP.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            parInpNJOP.P_MAX = this.currentMaks;
            parInpNJOP.P_MAXSpecified = true;
            parInpNJOP.P_MIN = this.currentMin;
            parInpNJOP.P_MINSpecified = true;
            parInpNJOP.P_SORT = "";
            parInpNJOP.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            fetchDataNJOP = new SvcTnhNjopSelect.call_pttClient(konfigApp.SvcTnhNjopSelect_name, konfigApp.SvcTnhNjopSelect_address);
            fetchDataNJOP.Open();
            fetchDataNJOP.Beginexecute(parInpNJOP, new AsyncCallback(this.getResultNJOP), null);
        }

        protected void getResultNJOP(IAsyncResult result)
        {
            try
            {
                this.outDataNJOP = fetchDataNJOP.Endexecute(result);
                fetchDataNJOP.Close();
                this.Invoke(new ShowDataNJOP(this.showDataNJOP), this.outDataNJOP);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataNJOP(SvcTnhNjopSelect.OutputParameters dataOut);

        public void showDataNJOP(SvcTnhNjopSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_NJOP.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_M_KTNH_NJOP[i]);
            }

            if (jmlDataGroup < 5)
            {
                //this.loadMore = false;
            }
            tnh5110.bsRiwayatNJOP.DataSource = serviceOutPut.SF_ROW_M_KTNH_NJOP;
        }
        #endregion

        #region load data Riwayat Pengguna
        public void getInitTnhRPgn(string _search = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();

            parInpRwytPengguna = new SvcTnhRwyPenggunaSelect.InputParameters();
            parInpRwytPengguna.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            if (_search != null)
            {
                //this.search = _search;
            }
            else
            {
                //this.search = "";
            }
            parInpRwytPengguna.P_MAX = this.currentMaks;
            parInpRwytPengguna.P_MAXSpecified = true;
            parInpRwytPengguna.P_MIN = this.currentMin;
            parInpRwytPengguna.P_MINSpecified = true;
            parInpRwytPengguna.P_SORT = "";
            parInpRwytPengguna.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            fetchDataRwytPengguna = new SvcTnhRwyPenggunaSelect.call_pttClient(konfigApp.SvcTnhRwyPenggunaSelect_name, konfigApp.SvcTnhRwyPenggunaSelect_address);
            fetchDataRwytPengguna.Open();
            fetchDataRwytPengguna.Beginexecute(parInpRwytPengguna, new AsyncCallback(this.getResultRwytPengguna), null);
        }

        protected void getResultRwytPengguna(IAsyncResult result)
        {
            try
            {
                this.outDataRwytPengguna = fetchDataRwytPengguna.Endexecute(result);
                fetchDataRwytPengguna.Close();
                this.Invoke(new ShowDataRwytPengguna(this.showDataRwytPengguna), this.outDataRwytPengguna);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataRwytPengguna(SvcTnhRwyPenggunaSelect.OutputParameters dataOut);

        public void showDataRwytPengguna(SvcTnhRwyPenggunaSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_RWYT_PENGGUNA.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_M_KTNH_RWYT_PENGGUNA[i]);
            }

            if (jmlDataGroup < konfigApp.dataAkhir)
            {
                //this.loadMore = false;
                //this.bbMore.Enabled = false;
            }
            else
            {
                //this.loadMore = true;
                //this.bbMore.Enabled = true;
            }
            //this.gvUcDtl.BestFitColumns();
            tnh5110.bsRiwayatPenggunaTanah.DataSource = serviceOutPut.SF_ROW_M_KTNH_RWYT_PENGGUNA;
        }
        #endregion

        #region load data Mutasi
        public void getInitTnhRMutasi()
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();

            parInpRwyMutasi = new SvcTnhRwyMutasiSelect.InputParameters();
            parInpRwyMutasi.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            parInpRwyMutasi.P_MAX = this.currentMaks;
            parInpRwyMutasi.P_MAXSpecified = true;
            parInpRwyMutasi.P_MIN = this.currentMin;
            parInpRwyMutasi.P_MINSpecified = true;
            parInpRwyMutasi.P_SORT = "";
            parInpRwyMutasi.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            fetchDataRwyMutasi = new SvcTnhRwyMutasiSelect.call_pttClient(konfigApp.SvcTnhRwyMutasiSelect_name, konfigApp.SvcTnhRwyMutasiSelect_address);
            fetchDataRwyMutasi.Open();
            fetchDataRwyMutasi.Beginexecute(parInpRwyMutasi, new AsyncCallback(this.getResultRwytMutasi), null);
        }

        protected void getResultRwytMutasi(IAsyncResult result)
        {
            try
            {
                this.outDataRwyMutasi = fetchDataRwyMutasi.Endexecute(result);
                fetchDataRwyMutasi.Close();
                this.Invoke(new ShowDataRwytMutasi(this.showDataRwytMutasi), this.outDataRwyMutasi);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataRwytMutasi(SvcTnhRwyMutasiSelect.OutputParameters dataOut);

        public void showDataRwytMutasi(SvcTnhRwyMutasiSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_RWYT_MUTASI.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_M_KTNH_RWYT_MUTASI[i]);
            }

            if (jmlDataGroup < konfigApp.dataAkhir)
            {
                //this.loadMore = false;
                //this.bbMore.Enabled = false;
            }
            else
            {
                //this.loadMore = true;
                //this.bbMore.Enabled = true;
            }
            tnh5110.bsRiwayatMutasi.DataSource = serviceOutPut.SF_ROW_M_KTNH_RWYT_MUTASI;
        }
        #endregion

        #region load data Riwayat Pemelihara
        public void getInitTnhRPml(string _search = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();

            parInpRwytPelihara = new SvcTnhRwyPeliharaSelect.InputParameters();
            parInpRwytPelihara.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            if (_search != null)
            {
                //this.search = " AND " + _search;
            }
            else
            {
                //this.search = "";
            }
            parInpRwytPelihara.P_MAX = this.currentMaks;
            parInpRwytPelihara.P_MAXSpecified = true;
            parInpRwytPelihara.P_MIN = this.currentMin;
            parInpRwytPelihara.P_MINSpecified = true;
            parInpRwytPelihara.P_SORT = "";
            parInpRwytPelihara.STR_WHERE = " ID_KTNH = " + this.ID_KTNH;
            fetchDataRwytPelihara = new SvcTnhRwyPeliharaSelect.call_pttClient(konfigApp.SvcTnhRwyPeliharaSelect_name, konfigApp.SvcTnhRwyPeliharaSelect_address);
            fetchDataRwytPelihara.Open();
            fetchDataRwytPelihara.Beginexecute(parInpRwytPelihara, new AsyncCallback(this.getResultRwytPelihara), null);
        }

        protected void getResultRwytPelihara(IAsyncResult result)
        {
            try
            {
                this.outDataRwytPelihara = fetchDataRwytPelihara.Endexecute(result);
                fetchDataRwytPelihara.Close();
                this.Invoke(new ShowDataRwytPelihara(this.showDataRwytPelihara), this.outDataRwytPelihara);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataRwytPelihara(SvcTnhRwyPeliharaSelect.OutputParameters dataOut);

        public void showDataRwytPelihara(SvcTnhRwyPeliharaSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_RWYT_PELIHARA.Count();

            if (this.dataInisial == true)
            {
                //binder.Clear();
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                //binder.Add(serviceOutPut.SF_ROW_M_KTNH_RWYT_PELIHARA[i]);
            }

            if (jmlDataGroup < konfigApp.dataAkhir)
            {
                //this.loadMore = false;
                //this.bbMore.Enabled = false;
            }
            else
            {
                //this.loadMore = true;
                //this.bbMore.Enabled = true;
            }
            tnh5110.bsRiwayatPemeliharaan.DataSource = serviceOutPut.SF_ROW_M_KTNH_RWYT_PELIHARA;
        }
        #endregion
        // -------------------------------------------------- end lap ----------------------------------------------
    }
}
