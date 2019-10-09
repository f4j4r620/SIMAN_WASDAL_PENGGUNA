using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraBars;
using System.Collections;
using System.Globalization;
using System.IO;

namespace AppPengguna.AST.SJT
{
    internal partial class PuSenjataPemakai : Form
    {
        private ucRpemakaiSenjata UcRPemakaiSenjata = null;

        private string status;

        private SvcSenjataRpemakaiSelect.BPSIMANSROW_M_KSENJ_RWYT_PEMAKAI selectedData;
        private SvcSenjataRpemakaiCrud.InputParameters parInp;
        private SvcSenjataRpemakaiCrud.OutputParameters outCrud;
        private SvcSenjataRpemakaiCrud.call_pttClient crudData;

        private decimal? idKSjt;
        private decimal? idKSjtPemakai;
        public decimal? NUM;
        public string path;
        private decimal? ID_SATKER;
        public string FileFotoPath;
        public bool FotoBaru = false;
        public string Table_foto;
        public string P_ID_TABLE;
        public datafoto DaftarFoto = new datafoto();
        public decimal? ID_DETAIL;
        public string ID_JNSDOK;

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';

        string FilePath;

      

        public PuSenjataPemakai(ucRpemakaiSenjata _UcRPemakaiSenjata, string status)
        {
            InitializeComponent();
            this.UcRPemakaiSenjata = _UcRPemakaiSenjata;
            this.status = status;
            this.idKSjt = _UcRPemakaiSenjata.IdKsjt;
           
            this.selectedData = _UcRPemakaiSenjata.selectedData;

            if (_UcRPemakaiSenjata.selectedData != null)
            {
                this.NUM = _UcRPemakaiSenjata.selectedData.NUM;
            }
            if (status == "detail")
            {
                this.bbiSave.Enabled = false;
                this.btnUploadDok.Enabled = false;
                this.btnUploadFoto.Enabled = false;
                this.btnCariSatker.Enabled = false;
            }
            //this.view_Foto();

        }

        #region Progress Bar
        public void progBar(BarItemVisibility str)
        {

            if (this.InvokeRequired)
            {
                ProgBar d = new ProgBar(progBar);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                this.beMarqueeBar.Visibility = str;
            }

        }

        public void ShowProgresBar()
        {
            this.progBar(BarItemVisibility.Always);
        }
        public void ShowProgresBarDelete()
        {

        }
        #endregion

        
        

        private SvcSenjataRpemakaiSelect.call_pttClient svcCaller;
        private SvcSenjataRpemakaiSelect.InputParameters inputPar;
        private SvcSenjataRpemakaiSelect.OutputParameters outputPar;
        private SvcSenjataRpemakaiSelect.BPSIMANSROW_M_KSENJ_RWYT_PEMAKAI rowData;

        protected ArrayList dataGrid;
        protected bool dataInisial;

        public void getInitialDataPemakaiMesinPnt()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcSenjataRpemakaiSelect.InputParameters();
                inputPar.P_COL = "";
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcSenjataRpemakaiSelect.call_pttClient();
                svcCaller.Open();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataDokKibMesinPnt), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataDokKibMesinPnt(IAsyncResult result)
        {
            try
            {
                this.outputPar = svcCaller.Endexecute(result);
                svcCaller.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataDokKibAngk(this.showDataDokKibBg), this.outputPar);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void aktifkanForm(string str)
        {
            this.Enabled = true;

        }

        private delegate void ShowDataDokKibAngk(SvcSenjataRpemakaiSelect.OutputParameters dataOut);

        public void showDataDokKibBg(SvcSenjataRpemakaiSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI.Count();
            if (this.dataInisial == true)
            {
                this.dataGrid = new ArrayList();
            }
            this.init();
        }

        public void init()
        {
            if (this.status == "input")
            {
                //this.gcDokKibAngk.Text = "Input Data Pemakai Mesin Peralatan Non TIK";
                this.idKSjtPemakai = 0;
                this.teFileName.ResetText();
                this.teKET.ResetText();
                this.teNamaPMK.ResetText();
                this.teNIP.ResetText();
                this.teTgl_SIP.ResetText();
                this.teTglMulai.ResetText();
                
                this.teUr_Satker.ResetText();
                this.teJabFungsional.ResetText();
                this.teJabStr.ResetText();
                //this.teNilaiSewa.ResetText();
                this.teNoKTP.ResetText();
                this.teNomorSuratIjin.ResetText();
                this.teTglLahir.ResetText();
                this.teTglPensiun.ResetText();
                this.teTglSK.ResetText();
                this.teTmptLahir.ResetText();
                this.teTmtJab.ResetText();
                //this.ID_SATKER = 0;
                this.teGolongan.ResetText();
                this.teKodeUnitSatker.ResetText();
                this.teAlamat.ResetText();
                this.teCheckTglSelesai.ResetText();

            }
            else if (this.status == "edit" || this.status == "detail")
            {
                try
                {
                    if (this.status == "detail")
                    {
                        //this.gcDokKibAngk.Text = "Detail Data Dokumen KIB Mesin Peralatan Non TIK";
                    }
                    else
                    {
                        //this.gcDokKibAngk.Text = "Edit Data Dokumen KIB Mesin Peralatan Non TIK";
                    }
                    this.idKSjtPemakai = selectedData.ID_KSENJ_RWYT_PEMAKAI;
                    

                    this.teFileName.Text = selectedData.NMFILE;
                    this.teKET.Text = selectedData.KET;
                    this.teNamaPMK.Text = selectedData.NM_PMK;
                    this.teNIP.Text = selectedData.NIP_PMK;
                    this.teTgl_SIP.Text = konfigApp.DateToString(selectedData.TGL_SIP);
                    this.teTglMulai.Text = konfigApp.DateToString(selectedData.TGL_MULAI);
                    this.teTgl_selesai.Text = konfigApp.DateToString(selectedData.TGL_SELESAI);
                    this.teUr_Satker.Text = selectedData.UR_SATKER;
                    this.teJabFungsional.Text = selectedData.JAB_FUNGSIONAL;
                    this.teJabStr.Text = selectedData.JAB_STR;
                   // this.teNilaiSewa.Value = (selectedData.NILAI == null) ? 0 : (decimal)selectedData.NILAI;
                    this.teNoKTP.Text = selectedData.NO_KTP;
                    this.teNomorSuratIjin.Text = selectedData.NO_SIP;
                    this.teTglLahir.Text = konfigApp.DateToString(selectedData.TGL_LAHIR);
                    this.teTglPensiun.Text = konfigApp.DateToString(selectedData.TGL_PENSIUN);
                    this.teTglSK.Text = konfigApp.DateToString(selectedData.TGL_SK);
                    this.teTmptLahir.Text = selectedData.TMP_LAHIR;
                    this.teTmtJab.Text = konfigApp.DateToString(selectedData.TMT_JAB);
                    this.ID_SATKER = selectedData.ID_SATKER;
                    this.teGolongan.Text = selectedData.GOLONGAN;
                    this.teKodeUnitSatker.Text = selectedData.KD_SATKER;
                    this.teAlamat.Text = selectedData.ALM_PMK;
                    this.teJnsDok.Text = selectedData.TYPE_DOC;
                    //this.view_Foto();

                }
                catch (Exception)
                {

                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }

            }
            else
            {
                this.idKSjtPemakai = selectedData.ID_KSENJ_RWYT_PEMAKAI;
            }
            this.Focus();

        }

        #region crud
        public void crudPemakai(IAsyncResult result)
        {
            try
            {
                outCrud = crudData.Endexecute(result);
                crudData.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new UbahDsDetail(this.ubahDsDetail), outCrud);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);

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
            }
        }

        public delegate void UbahDsDetail(SvcSenjataRpemakaiCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcSenjataRpemakaiCrud.OutputParameters outCrud)
        {
            SvcSenjataRpemakaiSelect.BPSIMANSROW_M_KSENJ_RWYT_PEMAKAI dataPenyama = new SvcSenjataRpemakaiSelect.BPSIMANSROW_M_KSENJ_RWYT_PEMAKAI();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KSENJ_RWYT_PEMAKAI = outCrud.PO_ID_KSENJ_RWYT_PEMAKAI;
            this.idKSjtPemakai = outCrud.PO_ID_KSENJ_RWYT_PEMAKAI;

            switch (this.modeCrud)
            {
                case 'C':


                    if (this.FilePath != null && this.FileFotoPath != null)
                    {
                        string Path = this.FilePath;
                        string idJnsDok = this.ID_JNSDOK;

                        simpanFile("ID_KSENJ_RWYT_PEMAKAI", dataPenyama.ID_KSENJ_RWYT_PEMAKAI, "M_KSENJ_RWYT_PEMAKAI", Path, "C");
                        string Path2 = this.FileFotoPath;
                        string idJnsDok2 = "";
                        simpanFile("ID_KSENJ_RWYT_PEMAKAI", dataPenyama.ID_KSENJ_RWYT_PEMAKAI, "M_KSENJ_RWYT_PEMAKAI_FOTO", Path2, "C");
                        this.UcRPemakaiSenjata.search = "";
                        this.UcRPemakaiSenjata.pencarian = false;
                        this.UcRPemakaiSenjata.initGrid();
                        this.UcRPemakaiSenjata.getInitPemakai();
                    }
                    else if (this.FileFotoPath == null && this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        string idJnsDok = this.ID_JNSDOK;

                        simpanFile("ID_KSENJ_RWYT_PEMAKAI", dataPenyama.ID_KSENJ_RWYT_PEMAKAI, "M_KSENJ_RWYT_PEMAKAI", Path, "C");
                        this.UcRPemakaiSenjata.search = "";
                        this.UcRPemakaiSenjata.pencarian = false;
                        this.UcRPemakaiSenjata.initGrid();
                        this.UcRPemakaiSenjata.getInitPemakai();
                    }
                    else if (this.FileFotoPath != null && this.FilePath == null)
                    {
                        string Path2 = this.FileFotoPath;
                        string idJnsDok2 = "";
                        simpanFile("ID_KSENJ_RWYT_PEMAKAI", dataPenyama.ID_KSENJ_RWYT_PEMAKAI, "M_KSENJ_RWYT_PEMAKAI_FOTO", Path2, "C");
                        this.UcRPemakaiSenjata.search = "";
                        this.UcRPemakaiSenjata.pencarian = false;
                        this.UcRPemakaiSenjata.initGrid();
                        this.UcRPemakaiSenjata.getInitPemakai();
                    }
                    else
                    {
                        this.UcRPemakaiSenjata.dataInisial = false;
                        this.UcRPemakaiSenjata.getById = true;
                        this.UcRPemakaiSenjata.getInitPemakai(" ID_KSENJ_RWYT_PEMAKAI = " + this.idKSjtPemakai.ToString());
                        this.init();
                        this.Close();
                    }
                    
                    break;
                case 'U':
                    UcRPemakaiSenjata.binder.Remove(this.selectedData);
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        string idJnsDok = this.ID_JNSDOK;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                            simpanFile("ID_KSENJ_RWYT_PEMAKAI", dataPenyama.ID_KSENJ_RWYT_PEMAKAI, "M_KALB_RWYT_PEMAKAI", Path, "U");
                        }
                        else
                        {
                            simpanFile("ID_KSENJ_RWYT_PEMAKAI", dataPenyama.ID_KSENJ_RWYT_PEMAKAI, "M_KALB_RWYT_PEMAKAI", Path, "C");
                            //UcRPemakaiSenjata.gvUcDtl.RefreshData();
                        }
                    }
                    else
                    {
                        //this.UcRPemakaiSenjata.dataInisial = false;
                        //this.UcRPemakaiSenjata.getById = true;
                        // this.UcRPemakaiSenjata.getInitPemakai(" ID_KSENJ_RWYT_PEMAKAI = " + this.idKSjtPemakai.ToString());

                        UcRPemakaiSenjata.gvUcDtl.RefreshData();
                        this.init();
                        this.Close();
                        this.UcRPemakaiSenjata.search = "";
                        this.UcRPemakaiSenjata.pencarian = false;
                        this.UcRPemakaiSenjata.initGrid();
                        this.UcRPemakaiSenjata.getInitPemakai();
                    }
                    break;
                case 'D':
                    UcRPemakaiSenjata.binder.Remove(this.selectedData);
                    UcRPemakaiSenjata.gvUcDtl.RefreshData();
                    UcRPemakaiSenjata.StrTotalGrid.Caption = (Convert.ToInt64(UcRPemakaiSenjata.StrTotalGrid.Caption) - 1).ToString();
                    UcRPemakaiSenjata.StrTotalDb.Caption = (Convert.ToInt64(UcRPemakaiSenjata.StrTotalDb.Caption) - 1).ToString();
                    this.init();
                    this.Close();
                    this.UcRPemakaiSenjata.search = "";
                    this.UcRPemakaiSenjata.pencarian = false;
                    this.UcRPemakaiSenjata.initGrid();
                    this.UcRPemakaiSenjata.getInitPemakai();
                    break;
            }
        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string FilePath, string SELECT)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            try
            {
                SvcAsetDokCrud.InputParameters inputData = new SvcAsetDokCrud.InputParameters();
                inputData.P_ID_DOK = 1;
                inputData.P_ID_DOKSpecified = true;
                inputData.P_ID_TYPE = ID_TYPE;
                inputData.P_ID_VALUE = ID_VALUE;
                //if (id_jnsDok != null)
                //{
                //inputData.P_KD_DOK = id_jnsDok;
                //}
                inputData.P_ID_VALUESpecified = true;
                inputData.P_ISI_FILE = konfigApp.FileToByteArray(FilePath);
                inputData.P_TABLE_TYPE = TABLE_TYPE;
                inputData.P_SELECT = SELECT;

                svcDokCrud = new SvcAsetDokCrud.call_pttClient();
                svcDokCrud.Beginexecute(inputData, new AsyncCallback(getCrudDokASet), "");

            }
            catch (Exception E)
            {

                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                }
                MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalSimpan);
            }
        }
        private void getCrudDokASet(IAsyncResult result)
        {
            try
            {
                dataoutDokAsetCrud = svcDokCrud.Endexecute(result);
                svcDokCrud.Close();
                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                }
                if (dataoutDokAsetCrud.PO_RESULT == "Y")
                {
                    this.Invoke(new CrudDokAset(this.crudDokAset), dataoutDokAsetCrud);
                }
                else
                {
                 
                    MessageBox.Show(dataoutDokAsetCrud.PO_RESULT_MESSAGE, konfigApp.judulGagalLain);
                }
            }
            catch (Exception e)
            {
                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                }
                MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalLain);
            }
        }

        private delegate void CrudDokAset(SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud);

        private void crudDokAset(SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud)
        {
            if (dataoutDokAsetCrud.PO_RESULT == "Y")
            {
                MessageBox.Show(dataoutDokAsetCrud.PO_RESULT_MESSAGE);
                if (this.status != "hapus")
                {
                    if (this.status != "input")
                    {
                        this.UcRPemakaiSenjata.dataInisial = false;
                    }
                    this.UcRPemakaiSenjata.getById = true;
                    //this.UcRPemakaiSenjata.getInitPemakai(" ID_KSENJ_RWYT_PEMAKAI = " + this.idKSjtPemakai.ToString());

                }

                this.Close();
                this.UcRPemakaiSenjata.search = "";
                this.UcRPemakaiSenjata.pencarian = false;
                this.UcRPemakaiSenjata.initGrid();
                this.UcRPemakaiSenjata.getInitPemakai();
            }

        }
        #endregion



        public void hapusData()
        {
            if (MessageBox.Show(konfigApp.teksHapusData + " No " + this.selectedData.NUM.ToString() + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcSenjataRpemakaiCrud.InputParameters parInp = new SvcSenjataRpemakaiCrud.InputParameters();
                    parInp.P_ID_KSENJ_RWYT_PEMAKAISpecified = true;
                    parInp.P_ID_KSENJ_RWYT_PEMAKAI = this.idKSjtPemakai;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcSenjataRpemakaiCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudPemakai), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }

        
        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tglSelesai = "";
            string nip = teNIP.Text;
            string nmPegawai = teNamaPMK.Text;
            string noKtp = teNoKTP.Text;
            string tmptLahir = teTmptLahir.Text;
            string tglLahir = teTglLahir.Text;
            string gol = teGolongan.Text;
            string alamat = teAlamat.Text;
            string jabStr = teJabStr.Text;
            string tmpJab = teTmtJab.Text;
            string tglPensiun = teTglPensiun.Text;
            string jabFungsional = teJabFungsional.Text;
            
            string tglSk = teTglSK.Text;
            string noSrtIjin = teNomorSuratIjin.Text;
            string tglSip = teTgl_SIP.Text;
            string tglMulai = teTglMulai.Text;
            if (teCheckTglSelesai.Checked && this.status == "edit")
            {
                tglSelesai = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else if (teCheckTglSelesai.Checked && this.status == "input")
            {
                tglSelesai = teTgl_selesai.Text;
            }
            
            //decimal nilaiSewa = teNilaiSewa.Value;
            string ket = teKET.Text;
            string fileName = teFileName.Text;
           

                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcSenjataRpemakaiCrud.InputParameters parInp = new SvcSenjataRpemakaiCrud.InputParameters();
                    parInp.P_ID_KSENJSpecified = true;
                    parInp.P_ID_KSENJ_RWYT_PEMAKAISpecified = true;
                    parInp.P_ID_SATKERSpecified = true;
                    parInp.P_NILAISpecified = true;

                    parInp.P_ID_KSENJ_RWYT_PEMAKAI = idKSjtPemakai;
                    parInp.P_ID_KSENJ = idKSjt;
                    parInp.P_NIP_PMK = nip;
                    parInp.P_NM_PMK = nmPegawai;
                    parInp.P_NO_KTP = noKtp;
                    parInp.P_TGL_LAHIR = tglLahir;
                    parInp.P_TMP_LAHIR = tmptLahir;
                    parInp.P_GOLONGAN = gol;
                    parInp.P_ALM_PMK = alamat;
                    parInp.P_JAB_STR = jabStr;
                    parInp.P_TMT_JAB = tmpJab;
                    parInp.P_TGL_PENSIUN = tglPensiun;
                    parInp.P_JAB_FUNGSIONAL = jabFungsional;
                    parInp.P_ID_SATKER = ID_SATKER;
                    parInp.P_TGL_SK = tglSk;
                    parInp.P_NO_SIP = noSrtIjin;
                    parInp.P_TGL_SIP = tglSip;
                    parInp.P_TGL_MULAI = tglMulai;
                    parInp.P_TGL_SELESAI = tglSelesai;
                    //parInp.P_NILAI = nilaiSewa;
                    parInp.P_KET = ket;
                    parInp.P_NMFILE = fileName;

                    if (this.status == "input")
                    {
                        parInp.P_SELECT = "C";
                    }
                    else
                    {
                        parInp.P_SELECT = "U";
                    }

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcSenjataRpemakaiCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudPemakai), "");

                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            
        }

        private void bbiRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.getInitialDataPemakaiMesinPnt();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnUploadDok_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                string filePath;
                long fileSize = 0;
                string creationTime = "";

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Dispose();
                dialog.Title = "Open PDF Files";
                dialog.Filter = "PDF Files(*.pdf)|*.pdf";
                dialog.Multiselect = false;


                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    fileName = dialog.SafeFileName;
                    fileSize = new System.IO.FileInfo(dialog.FileName).Length;
                    creationTime = new System.IO.FileInfo(dialog.FileName).CreationTime.ToString();

                    if (fileSize < konfigApp.maksSizeFile)
                    {
                        this.FilePath = filePath;
                        teFileName.Text = fileName;

                        this.path = FilePath;
                    }
                    else
                    {
                        MessageBox.Show(konfigApp.konfirmasiMaksimalFile, konfigApp.judulGagalLain);
                    }


                    Console.WriteLine(fileSize + creationTime);

                }
            }
            catch
            {
                System.Console.WriteLine("gagal");
            }
        }

        private void PuMesinPntPemakai_Load(object sender, EventArgs e)
        {
            this.getInitialDataPemakaiMesinPnt();
            if (this.status != "input")
            {
                this.view_Foto();
            }
            
            this.teTgl_selesai.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private AppPengguna.PU.FrmPUSatker puSatker;
        private void btnCariSatker_Click(object sender, EventArgs e)
        {
            puSatker = new AppPengguna.PU.FrmPUSatker();
            puSatker.ambilSatker = new AppPengguna.PU.AmbilSatker(this.ambilSatker);
            puSatker.ShowDialog();
        }

        private void ambilSatker(decimal? id, string kode, string nama)
        {
            this.ID_SATKER = id;
            this.teKodeUnitSatker.Text = kode;
            this.teUr_Satker.Text = nama;
        }


        #region View Foto
        //------------- GET Foto di DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------

        protected void view_Foto()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = this.ID_DETAIL;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = this.P_ID_TABLE;
                parInp.P_TABLE = this.Table_foto;

                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient(konfigApp.SvcAsetGetDokSelect_name, konfigApp.SvcAsetGetDokSelect_address);
                svcAsetGetDokSelect.Open();
                svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getResultDok(IAsyncResult result)
        {
            try
            {
                this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
                svcAsetGetDokSelect.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new ShowFileDok(this.showFileDok), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowFileDok(SvcAsetGetDokSelect.OutputParameters dataOut);

        public void showFileDok(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlData > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];

                int idx = Foto.Images.Add(konfigApp.convert2bytmap(dok.ISI_FILE));
                DaftarFoto.PHOTO = dok.ISI_FILE;
                
                Foto.SetCurrentImageIndex(idx);
            }
        }


        #endregion//ViewFoto

       


        #region jenis dokumen
        private AppPengguna.PU.FrmPuJnsDok jnsDok;
        private void sbJnsDok_Click(object sender, EventArgs e)
        {
            jnsDok = new AppPengguna.PU.FrmPuJnsDok();
            jnsDok.ambilJnsDok = new AppPengguna.PU.AmbilJnsDok(this.ambilJnsDok);
            jnsDok.ShowDialog();
        }

        private void ambilJnsDok(string id, string nama)
        {
            this.ID_JNSDOK = id;
            this.teJnsDok.Text = nama;
        }
        #endregion

        private void btnUploadFoto_Click(object sender, EventArgs e)
        {
            ofdFoto.InitialDirectory = "C:";
            ofdFoto.Filter = "(*.bmp, *.jpg, *.gif, *.png)|*.bmp;*.jpg;*.gif;*.png";
            ofdFoto.Multiselect = false;
            if (ofdFoto.ShowDialog() == DialogResult.OK)
            {
                string fileNameFoto = "";
                string filePathFoto = "";
                long fileSizeFoto = 0;
                string creationTimeFoto = "";
                var size = new FileInfo(ofdFoto.FileName).Length;
                if (size > konfigApp.maksSizeFoto)
                {
                    MessageBox.Show(konfigApp.konfirmasiMaksimalFoto, konfigApp.judulKonfirmasi);
                    return;
                }
                else
                {

                    FileFotoPath = ofdFoto.FileName;
                    fileNameFoto = ofdFoto.SafeFileName;
                    fileSizeFoto = new System.IO.FileInfo(ofdFoto.FileName).Length;
                    creationTimeFoto = new System.IO.FileInfo(ofdFoto.FileName).CreationTime.ToString();
                    Foto.Images.Clear();
                    int idx = Foto.Images.Add(Image.FromFile(ofdFoto.FileName));
                    Foto.SetCurrentImageIndex(idx);
                    FotoBaru = true;
                    Console.WriteLine(fileSizeFoto + creationTimeFoto);
                }

            }
        }

    }
}