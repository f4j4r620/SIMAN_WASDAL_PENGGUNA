using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;


namespace AppPengguna.KKW.PENERTIBAN
{
    public partial class ucPenertibanForm : UserControl
    {
        public ToggleProgressBar toggleProgressBar;
        public string statusForm = null;
        public SimpanDataPenertiban simpanDataPenertiban;
        private Thread myThread;
        GridView rowTerpilih = null;
        public String JenisPenertiban = null;
        public String UraianPenertiban = null;
        public String KuasaPenggunaBarang = null;
        public String Keterangan = null;
        private char modeCrud = 'A';
        public SimpanDaftarAset3Terpilih simpan3AsetTerpilih = null;
        
        public SvcMonKorwilPenertibanA1.WASDALSROW_MON_KORWIL_PENERTIBAN dataTerpilih;
        //public SvcMonPenertibanKoEsKl.WASDALSROW_MON_KOR_ES_KL_PENERTIBAN dataTerpilih;
        
        private bool PenertibanLoaded = false;
        private decimal? idSatker = null;
        private string noSkLama = null;
        private string noSkBaru = null;
        private int indexAset;
        public string filePath = "";
        public string noReg = "";
        public string stsBntkPenertiban = "";
        //private string strCari = "";

        public string urSatker = "";
        public decimal? idPemohon = null;
        public decimal? _idPenertiban = null;  
        public string kodePenerbitSkDetail = null;
        public string namaPenerbitSkDetail = null;
        public string kodePenerbitSk = null;
        public decimal? _idAset = null;
        KKW.PENERTIBAN.ucPenertibanGrid gridPenertiban;
        

        public ucPenertibanForm(string _status)
        {
            InitializeComponent();
            statusForm = _status;
        }
        public decimal? idAset
        {
            get { return _idAset; }
            set { _idAset = value; }   
        }
        #region Thread

        private void toggleProgBarPu(string kondisi)
        {
            if (kondisi == "start") this.aktifkanProgresBar();
            else this.nonAktifkanprogressBar();
        }



        private void aktifkanProgresBar()
        {
            toggleProgressBar("start");
        }

        private void nonAktifkanprogressBar()
        {
            toggleProgressBar("finish");
        }

        public delegate void AktifkanForm(string str);

        public void aktifkanForm(string str)
        {
            this.Enabled = true;
        }

        #endregion Thread


        
        
        #region Ambil Data Jenis Penertiban
        
        SvcWasdalAsetSelect.call_pttClient svcAset;
        SvcWasdalAsetSelect.OutputParameters outputAset;
        KKW.PENERTIBAN.PU.frmPuAset formDaftarAsetSatker;
        //SvcWasdalPenertibanCud.OutputParameters outputAset;
        

        private void sbPilihAset_LinkClicked(object sender, EventArgs e)    //manggil Pop Up gimana?    //level satker aja
        {
            getPenertiban();
                if (formDaftarAsetSatker == null)
                {
                    formDaftarAsetSatker = new PU.frmPuAset()
                    {
                        toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                        
                    };
                }
                //formDaftarAsetSatker.isTb = rgJenisAset.EditValue.ToString();
                
                formDaftarAsetSatker.idPemohonBaru = idPemohon;
                formDaftarAsetSatker.idSatker = idPemohon;
                formDaftarAsetSatker.kodeSatker = konfigApp.kodeSatker; //kodenya satker
                formDaftarAsetSatker.namaSatker = konfigApp.namaSatker;
                formDaftarAsetSatker.teSatker.Properties.Items.Clear();
                formDaftarAsetSatker.teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", formDaftarAsetSatker.kodeSatker, formDaftarAsetSatker.namaSatker));
                formDaftarAsetSatker.teSatker.SelectedIndex = 0;
                formDaftarAsetSatker.levelPenggunaBarang = konfigApp.levelSatker;
                _idPenertiban = (dataTerpilih != null) ? dataTerpilih.ID_PENERTIBAN : null;
                formDaftarAsetSatker.ShowDialog();
                teKodeBarang.Text = konfigApp.kodebarang ;
                teNup.Text = konfigApp.nup.ToString();
                teUraianBarang.Text = konfigApp.uraianbarang;
                this.urSatker = formDaftarAsetSatker._urSatker;
                this.idAset = formDaftarAsetSatker._idAset;
                this.noReg = formDaftarAsetSatker._noReg;

        }

        private void getPenertiban()
        {
            //KKW.PENERTIBAN.PU.frmPuAset gcDaftarAset;
            //gcDaftarAset.DataSource = null;
            //gcDaftarAset.RefreshDataSource();
            PenertibanLoaded = false;
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                svcAset = new SvcWasdalAsetSelect.call_pttClient();
                SvcWasdalAsetSelect.InputParameters parInput = new SvcWasdalAsetSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format(" ID_SATKER = {0} AND IN_KD_PELAYANAN =  {1}", konfigApp.idSatker, konfigApp.kdPelayanan);
                //parInput.STR_WHERE = String.Format("ID_SATKER = {0}", konfigApp.idSatker, this.strCari);
                ambilDaftarAset = new SvcWasdalAsetSelect.call_pttClient();
                ambilDaftarAset.Open();
                ambilDaftarAset.Beginexecute(parInput, new AsyncCallback(goGetDaftarAset), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }

        }
        private void getDataAset(IAsyncResult result)
        {
            try
            {
                outputAset = svcAset.Endexecute(result);
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataAset(this.loadDataAset), outputAset);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAset(SvcWasdalAsetSelect.OutputParameters outputAset);

        private void loadDataAset(SvcWasdalAsetSelect.OutputParameters outputAset)
        {
            int jmlData = 3;
            if (jmlData >= 0)
            {
                PenertibanLoaded = true;

                cbJenisPenertiban.SelectedIndex = 0;
                //teNamaInstansi.Properties.DataSource = outputInstansi.SF_ROW_R_INSTANSI;
                //teNamaInstansi.EditValue = outputInstansi.SF_ROW_R_INSTANSI[0].KD_INSTANSI;
                if (dataTerpilih != null)
                {
                    
                    cbJenisPenertiban.Text = dataTerpilih.BENTUK_PENERTIBAN;
                    cbJenisPenertiban.EditValue = dataTerpilih.BENTUK_PENERTIBAN;
                }
                if (statusForm != "C")
                    getDaftarAset();
            }
            else PenertibanLoaded = false;
        }
        #endregion
        

        #region Inisialisasi
        private void resetFormPenertiban()
        {
            cbJenisPenertiban.SelectedIndex = -1;
            teUraianPenertiban.Text = "";
            KuasaPenggunaBarang = "";
            teKeterangan.Text = "";
            teKodeBarang.Text = "";
            teNup.Text = "";
            teBtkTertibGunaNoSrt.Text = "";
            teDsrNoSurat.Text = "";
            deDsrTglSurat.Text = "";
            teThnAnggaran.Text = "";
            teUraianBarang.Text = "";
            teBntkTertibGunaTglSrt.Text = "";
            cbJenisPenertiban.Text = "";
            cbBtkTertibGuna.Text = "";
            cbDsrPenertiban.Text = "";
        }

        public void inisialisasiForm()
        {
            resetFormPenertiban();

            if (statusForm != "C" && statusForm != "CU")
            {
                cbJenisPenertiban.EditValue = dataTerpilih.BENTUK_PENERTIBAN;    //dataterpilih
                //teUraianPenertiban.Text = dataTerpilih.UR_PENERTIBAN;
                KuasaPenggunaBarang = dataTerpilih.KUASA_PENGGUNA_BRG;    //belum nemu
                teKodeBarang.Text = dataTerpilih.KD_BRG;
                teNup.Text = Convert.ToString(dataTerpilih.NUP);
                //teKeterangan.Text = dataTerpilih.DASAR;
                teUraianBarang.Text = dataTerpilih.UR_SSKEL;

                konfigApp.kodebarang = dataTerpilih.KD_BRG;
                this._idAset = dataTerpilih.ID_ASET;
                this._idPenertiban = dataTerpilih.ID_PENERTIBAN;
                this.noReg = dataTerpilih.NOREG;
                this.urSatker = dataTerpilih.UR_SATKER;
                teThnAnggaran.Text = konfigApp.tahunAnggaran.ToString();
            }
            if (PenertibanLoaded == false)
                getPenertiban();
            else
            {
                if (statusForm != "C" && statusForm != "CU")
                {
                    if (noSkLama != noSkBaru)
                        getDaftarAset();
                    else
                    {
                        //gcDaftarAset.DataSource = dsGridDaftarAset;
                        //gcDaftarAset.RefreshDataSource();
                    }
                }
            }
            cbJenisPenertiban.Properties.ReadOnly = false;
            teUraianPenertiban.Properties.ReadOnly = false;
            teKeterangan.Properties.ReadOnly = false;
            sbSimpan.Enabled = true;
            sbPilihAset.Enabled = true;
            //sbTambah.Enabled = false;
            //sbHapus.Enabled = false;
            //sbRefresh.Enabled = false;
            //gcDaftarAset.Enabled = false;
            //cePilihSemua.Enabled = false;
            switch (statusForm)
            {
                case "C":
                    teUraianPenertiban.Properties.ReadOnly = false;
                    teKeterangan.Properties.ReadOnly = false;
                    sbSimpan.Enabled = true;
                    teKodeBarang.Properties.ReadOnly = true;
                    teNup.Properties.ReadOnly = true;
                    teUraianBarang.Properties.ReadOnly = true;
                    KuasaPenggunaBarang = konfigApp.namaUser;
                    teThnAnggaran.Text = konfigApp.tahunAnggaran.ToString();
                    break;
                case "U":
                //    cbJenisPenertiban.Text = dataTerpilih.BENTUK_PENERTIBAN;    //dataterpilih
                //teUraianPenertiban.Text = dataTerpilih.BENTUK_PENERTIBAN;
                //teKuasaPenggunaBarang.Text = dataTerpilih.KUASA_PENGGUNA_BRG;    //belum nemu
                //teKeterangan.Text = dataTerpilih.KET;
                //teKodeBarang.Text = konfigApp.kodebarang;
                //teNup.Text = konfigApp.nup;
                //teUraianBarang.Text = konfigApp.uraianbarang;
                //    cbJenisPenertiban.Properties.ReadOnly = false;
                    teUraianPenertiban.Properties.ReadOnly = false;
                    teKeterangan.Properties.ReadOnly = false;
                    sbSimpan.Enabled = true;
                    teKodeBarang.Properties.ReadOnly = true;
                    teNup.Properties.ReadOnly = true;
                    teUraianBarang.Properties.ReadOnly = true;
                    break;
                case "A":
                    cbJenisPenertiban.Properties.ReadOnly = true;
                    teUraianPenertiban.Properties.ReadOnly = true;
                    teKeterangan.Properties.ReadOnly = true;
                    sbSimpan.Enabled = false;
                    teKodeBarang.Properties.ReadOnly = true;
                    teNup.Properties.ReadOnly = true;
                    teUraianBarang.Properties.ReadOnly = true;
                    
                    break;
            }
           
        }

        private void cbJenisPenertiban_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbJenisPenertiban.SelectedIndex == 0)
            {
                cbJenisPenertiban.Text = "PENGGUNAAN";
                
            }
            else if (cbJenisPenertiban.SelectedIndex == 1)
            {
                cbJenisPenertiban.Text = "PEMANFAATAN";
            }
            else if (cbJenisPenertiban.SelectedIndex == 2)
            {
                cbJenisPenertiban.Text = "PEMINDAHTANGAN";
            }
            
        }

 
        #endregion

        
        #region Bagian Header
        
        private void sbSimpan_Click(object sender, EventArgs e)
        {
            if (cbJenisPenertiban.Text != "" && teUraianPenertiban.Text != "" &&  teKeterangan.Text != "" && teKodeBarang.Text != "" && teUraianBarang.Text != "" && teNup.Text != "")
            {
                if (statusForm == "C" || statusForm == "U" || statusForm == "CU")
                //gcDaftarAset.DataSource = null;
                //gcDaftarAset.RefreshDataSource();
                {
                    simpanDataPenertiban(statusForm);
                }
              
            }
            else MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulKonfirmasi);
        }

        #endregion
        

        #region Bagian Detail: Aset
        #region --++ Ambil Daftar Aset dalam SK
        SvcWasdalAsetSelect.OutputParameters dOutDaftarAset;
        SvcWasdalAsetSelect.call_pttClient ambilDaftarAset;
        SvcWasdalAsetSelect.WASDALSROW_WASDAL_M_ASET dataAsetTerpilih;
        private ArrayList dsGridDaftarAset;

        private void getDaftarAset()
        {
            //gcDaftarAset.DataSource = null;
            //gcDaftarAset.RefreshDataSource();
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                svcAset = new SvcWasdalAsetSelect.call_pttClient();
                SvcWasdalAsetSelect.InputParameters parInput = new SvcWasdalAsetSelect.InputParameters();
                parInput.P_COL = "";
                parInput.P_COUNT = "N";
                parInput.P_MAX = 0;
                parInput.P_MAXSpecified = true;
                parInput.P_MIN = 0;
                parInput.P_MINSpecified = true;
                parInput.P_SORT = "";
                parInput.STR_WHERE = String.Format(" ID_SATKER = {0} AND IN_KD_PELAYANAN =  {1}", konfigApp.idSatker, konfigApp.kdPelayanan);
                //parInput.STR_WHERE = String.Format("ID_SATKER = {0}", konfigApp.idSatker, this.strCari);
                ambilDaftarAset = new SvcWasdalAsetSelect.call_pttClient();
                ambilDaftarAset.Open();
                ambilDaftarAset.Beginexecute(parInput, new AsyncCallback(goGetDaftarAset), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void goGetDaftarAset(IAsyncResult result)
        {
            try
            {
                dOutDaftarAset = ambilDaftarAset.Endexecute(result);
                ambilDaftarAset.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new LoadDataDaftarAset(loadDataDaftarAset), dOutDaftarAset);
            }
            catch   //bug disini
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataDaftarAset(SvcWasdalAsetSelect.OutputParameters outputDaftarAset);

        private void loadDataDaftarAset(SvcWasdalAsetSelect.OutputParameters outputDaftarAset)
        {
            int jmlData = outputDaftarAset.SF_ROW_WASDAL_M_ASET.Count();
            decimal? nilaiSblmSusut = 0;
            decimal? jmlKuantitas = 0;
            dsGridDaftarAset = new ArrayList();
            if (jmlData > 1 )//|| outputDaftarAset.SF_ROW_WASDAL_M_ASET[0].KD_BRG != "-") kal di cyber gagal pakai ini 
            {
                for (int i = 0; i < jmlData; i++)
                {
                    //outputDaftarAset.SF_ROW_WASDAL_M_ASET[i].IS_TB = (outputDaftarAset.SF_ROW_WASDAL_M_ASET[i].IS_TB == "Y" ? konfigApp.namaTb : konfigApp.namaNonTb);
                    outputDaftarAset.SF_ROW_WASDAL_M_ASET[i].NUMSpecified = false;
                    dsGridDaftarAset.Add(outputDaftarAset.SF_ROW_WASDAL_M_ASET[i]);
                    nilaiSblmSusut += outputDaftarAset.SF_ROW_WASDAL_M_ASET[i].NILAI_SBLM_SUSUT;
                    jmlKuantitas += outputDaftarAset.SF_ROW_WASDAL_M_ASET[i].KUANTITAS;
                    
                }
                //gcDaftarAset.DataSource = null;
                //gcDaftarAset.DataSource = dsGridDaftarAset;
            }
            noSkLama = noSkBaru;


        }

        private void sbRefresh_Click(object sender, EventArgs e)
        {
            getDaftarAset();
        }
        #endregion
        /*
        #region --++ Tambah Daftar Aset dalam SK
        PENERTIBAN.frmPuAset formDaftarAsetSatker;
        
        private void sbTambah_Click(object sender, EventArgs e)
        {
            getDaftarAset();
            
                if (formDaftarAsetSatker == null)
                {
                    formDaftarAsetSatker = new PENERTIBAN.frmPuAset()
                    {
                        toggleProgressBar = new ToggleProgressBar(toggleProgBarPu),
                        simpanAsetTerpilih = new SimpanDaftarAsetTerpilih(simpanDaftarAset)
                    };
                }
                formDaftarAsetSatker.isTb = cbJenisPenertiban.ToString();
                formDaftarAsetSatker.idPemohonBaru = idPemohon;
                formDaftarAsetSatker.idSatker = idPemohon;
                //formDaftarAsetSatker.kodeSatker = teKodePemohon.Text;
                //formDaftarAsetSatker.namaSatker = teNamaPemohon.Text;
                formDaftarAsetSatker.teSatker.Properties.Items.Clear();
                //formDaftarAsetSatker.teSatker.Properties.Items.Insert(0, String.Format("[{0}] {1}", teKodePemohon.Text, teNamaPemohon.Text));
                formDaftarAsetSatker.teSatker.SelectedIndex = 0;
                formDaftarAsetSatker.levelPenggunaBarang = konfigApp.levelSatker;
                formDaftarAsetSatker.ShowDialog();
            
        }
        #endregion*/

        #region --++ Simpan Daftar Aset Terpilih
        SvcWasdalPenertibanCud.OutputParameters dOutSimpanAset;
        SvcWasdalPenertibanCud.execute_pttClient simpanDftrAset;
        private char modeCudAset = 'A';
        private decimal? nilaiPenetapanLama = null;
        private decimal? jmlKuantitasLama = null;

        private void simpanDaftarAset(string _daftarIdAset, char _modeCudAset)
        {
            try
            {
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcWasdalPenertibanCud.InputParameters parInp = new SvcWasdalPenertibanCud.InputParameters();
                parInp.P_THN_ANG = teThnAnggaran.Text;
                parInp.P_BENTUK_PENERTIBAN = Convert.ToString(cbJenisPenertiban.EditValue);
                parInp.P_UR_SATKER = gridPenertiban.dataTerpilih.UR_SATKER;
                parInp.P_KUASA_PENGGUNA_BRG = konfigApp.namaUser;
                parInp.P_KET = teKeterangan.Text.Trim();
                //parInp.P_ID_ASET = gridPenertiban.dataTerpilih.ID_ASET;
                parInp.P_ID_ASETSpecified = true;
               // parInp.P_ID_PENERTIBAN = gridPenertiban.dataTerpilih.ID_PENERTIBAN;
                parInp.P_ID_PENERTIBANSpecified = true;
                parInp.P_ID_SATKER = konfigApp.idSatker;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_ID_USER = konfigApp.idUser;
                parInp.P_ID_USERSpecified = true;
                parInp.P_KD_BRG = teKodeBarang.Text;
                parInp.P_KD_SATKER = konfigApp.kodeSatker;
                parInp.P_NM_PENGGUNA = konfigApp.namaPengguna;
               // parInp.P_NOREG = gridPenertiban.dataTerpilih.NOREG;
                parInp.P_NUP = teNup.Text;
                parInp.P_UR_SSKEL = teUraianBarang.Text.Trim();
                
                 
                modeCudAset = _modeCudAset;
                parInp.P_SELECT = Convert.ToString(modeCudAset);
                simpanDftrAset = new SvcWasdalPenertibanCud.execute_pttClient();
                simpanDftrAset.Open();
                simpanDftrAset.Beginexecute(parInp, new AsyncCallback(goSimpanDftrAset), "");
            }
            catch
            {
                modeCudAset = 'A';
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulGagalLain);
            }
        }
        #endregion

        #region --++ CUD Daftar Aset
        ArrayList daftarAsetSalinan;

        private void goSimpanDftrAset(IAsyncResult result)
        {
            try
            {
                dOutSimpanAset = simpanDftrAset.Endexecute(result);
                simpanDftrAset.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsDaftarAset(dsDaftarAset), dOutSimpanAset);
            }
            catch (Exception e)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                switch (modeCudAset)
                {
                    case 'C':
                    case 'U':
                        konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                        break;
                    case 'D':
                        konfigApp.teksDialog = konfigApp.teksGagalHapus;
                        break;
                    default:
                        konfigApp.teksDialog = konfigApp.teksGagalLain;
                        break;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulKonfirmasi);
            }
        }

        private delegate void DsDaftarAset(SvcWasdalPenertibanCud.OutputParameters dataOut);

        private void dsDaftarAset(SvcWasdalPenertibanCud.OutputParameters dataOut)
        {
            if (dataOut.PO_RESULT == "Y")
            {
                decimal? _krgNilTetap = null;
                decimal? _krgKuantitas = null;
                if (dsGridDaftarAset == null) dsGridDaftarAset = new ArrayList();
                switch (modeCudAset)
                {
                    case 'C':
                        string _kodeSatker = "";
                        string _namaSatker = "";
                        
                            daftarAsetSalinan = formDaftarAsetSatker.daftarTerpilih;
                            _kodeSatker = formDaftarAsetSatker.kodeSatker;
                            _namaSatker = formDaftarAsetSatker.namaSatker;
                        
                        
                        SvcWasdalAsetSelect.WASDALSROW_WASDAL_M_ASET dataPenyama;
                        for (int i = 0; i < daftarAsetSalinan.Count; i++)
                        {
                            SvcWasdalAsetSelect.WASDALSROW_WASDAL_M_ASET daftarAsetTerpilih = (SvcWasdalAsetSelect.WASDALSROW_WASDAL_M_ASET)daftarAsetSalinan[i];
                            dataPenyama = null;
                            dataPenyama = new SvcWasdalAsetSelect.WASDALSROW_WASDAL_M_ASET();
                            dataPenyama.ID_ASET = daftarAsetTerpilih.ID_ASET;
                            dataPenyama.ID_ASETSpecified = true;
                           
                            dataPenyama.ID_SATKER = idSatker;
                            dataPenyama.ID_SATKERSpecified = true;
                            dataPenyama.KD_BRG = daftarAsetTerpilih.KD_BRG;
                            dataPenyama.KD_PELAYANAN = konfigApp.kdPelayanan;
                            dataPenyama.KD_SATKER = _kodeSatker;
                            dataPenyama.KD_STATUS = daftarAsetTerpilih.KD_STATUS;
                            dataPenyama.KUANTITAS = daftarAsetTerpilih.KUANTITAS;
                            dataPenyama.KUANTITASSpecified = true;
                            dataPenyama.NO_ASET = daftarAsetTerpilih.NO_ASET;
                            dataPenyama.NO_ASETSpecified = true;
                            dataPenyama.NOREG = daftarAsetTerpilih.NOREG;
                            dataPenyama.NUM = (dsGridDaftarAset == null ? 1 : dsGridDaftarAset.Count + 1);
                            dataPenyama.NUMSpecified = false;
                            dataPenyama.TOTAL_DATA = daftarAsetTerpilih.TOTAL_DATA;
                            dataPenyama.TOTAL_DATASpecified = daftarAsetTerpilih.TOTAL_DATASpecified;
                            dataPenyama.UR_SATKER = _namaSatker;
                            dataPenyama.UR_SSKEL = daftarAsetTerpilih.UR_SSKEL;
                            dataPenyama.UR_STATUS = daftarAsetTerpilih.UR_STATUS;
                            dataPenyama.THN_ANG = daftarAsetTerpilih.THN_ANG;
                            _krgNilTetap = _krgNilTetap + daftarAsetTerpilih.RPH_ASET;
                            _krgKuantitas = _krgKuantitas + daftarAsetTerpilih.KUANTITAS;
                            dsGridDaftarAset.Add(dataPenyama);
                        }
                        //teNilaiPenetapan.Text = (Convert.ToDecimal(_krgNilTetap)).ToString("n0");
                        //teKuantitas.Text = Convert.ToDecimal(_krgKuantitas).ToString("n0");
                        break;
                 }
                //gcDaftarAset.DataSource = dsGridDaftarAset;
                //gcDaftarAset.RefreshDataSource();
            }
            else MessageBox.Show(dataOut.PO_RESULT_MESSAGE, konfigApp.judulGagal);
        }
        #endregion

        private void cbJenisPenertiban_EditValueChanged(object sender, EventArgs e)
        {
            if (cbJenisPenertiban.Text == "PENGGUNAAN")
            {
                stsBntkPenertiban = "PENGGUNAAN";
                cbBtkTertibGuna.Properties.Items.Clear();
                cbBtkTertibGuna.Properties.Items.Add("Mengajukan Usul Penetapan Status Penggunaan Ke Pengelola Barang");
                cbBtkTertibGuna.Properties.Items.Add("Menetapkan Status Penggunaan Sesuai Batas Kewenangan Pengguna Barang");
                cbBtkTertibGuna.Properties.Items.Add("Menyerahkan BMN Yang Tidak Digunakan Kepada Pengelola Barang");
                cbBtkTertibGuna.Properties.Items.Add("Penertiban Lainnya");
            }
            else if (cbJenisPenertiban.Text == "PEMANFAATAN")
            {
                stsBntkPenertiban = "PEMANFAATAN";
                cbBtkTertibGuna.Properties.Items.Clear();
                cbBtkTertibGuna.Properties.Items.Add("Mengajukan Psulan Pemanfaatan Pada Pengelola Barang");
                cbBtkTertibGuna.Properties.Items.Add("Penertiban Lainnya");
            }
            else
            {
                stsBntkPenertiban = "PEMINDAHTANGANAN";
                cbBtkTertibGuna.Properties.Items.Clear();
                cbBtkTertibGuna.Properties.Items.Add("Pembatalan Pelaksanaan Pemindahtanganan");
                cbBtkTertibGuna.Properties.Items.Add("Penertiban Lainnya");
            }

        }

 
        #endregion




    }
}
