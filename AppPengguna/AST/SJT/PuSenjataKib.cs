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

namespace AppPengguna.AST.SJT
{
    internal partial class PuSenjataKib : Form
    {
        private ucRKibSenjata UcRKibSenjata = null;

        private string status;

        private SvcSenjataRkibSelect.BPSIMANSROW_M_KSENJ_DOK_KIB selectedData;
        private SvcSenjataRkibCrud.InputParameters parInp;
        private SvcSenjataRkibCrud.OutputParameters outCrud;
        private SvcSenjataRkibCrud.call_pttClient crudData;

        private decimal? idKsjt;
        private decimal? idKsjtKib;
        public decimal? NUM;
        public string path;

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';

        string FilePath;

        //public DateTime? tglDok
        //{
        //    set { teTglDok.EditValue = value; }
        //    get { return Convert.ToDateTime(teTglDok.EditValue); }
        //}

        public PuSenjataKib(ucRKibSenjata _UcRKibSenjata, string status)
        {
            InitializeComponent();
            this.UcRKibSenjata = _UcRKibSenjata;
            this.status = status;
            this.idKsjt = _UcRKibSenjata.IdKSjt;
            // this.idKsjt = _UcRKibSenjata.selectedData.ID_KANGK;
            this.selectedData = _UcRKibSenjata.selectedData;

            if (_UcRKibSenjata.selectedData != null)
            {
                this.NUM = _UcRKibSenjata.selectedData.NUM;
            }
            if (status == "detail")
            {
                this.bbKibSimpan.Enabled = false;
                this.sbNamaFile.Enabled = false;
            }

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
                this.beMarqueBar.Visibility = str;
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

        private void bbKibSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string noDokKib = (teNoDokKib.Text != "-") ? teNoDokKib.Text : null;
            string tglDokKib = teTglDok.Text;
            string ketKib = meKet.Text;
            string nmFileKib = teNmFile.Text;
            if (teNmFile.Text != "")
            {

                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcSenjataRkibCrud.InputParameters parInp = new SvcSenjataRkibCrud.InputParameters();
                    parInp.P_ID_KSENJSpecified = true;
                    parInp.P_ID_KSENJ_DOK_KIBSpecified = true;

                    parInp.P_ID_KSENJ_DOK_KIB = idKsjtKib;
                    parInp.P_ID_KSENJ = idKsjt;
                    parInp.P_NO_DOK_KIB = noDokKib;
                    //parInp.P_TGL_DOK_KIB = tglDokKib;
                    parInp.P_KET_DOK = ketKib;
                    parInp.P_NMFILE = nmFileKib;

                    if (this.status == "input")
                    {
                        parInp.P_SELECT = "C";
                    }
                    else
                    {
                        parInp.P_SELECT = "U";
                    }

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcSenjataRkibCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibSjt), "");

                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
            else
            {
                MessageBox.Show("Dokumen KIB harus terisi!", konfigApp.judulGagalSimpan);
            }
        }

        private void bbKibRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.getInitialDataDokKibBg();
        }

        private void bbKibKembali_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void sbNamaFile_Click(object sender, EventArgs e)
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
                        teNmFile.Text = fileName;

                        //teNmPath.Text = FilePath;
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

        private SvcSenjataRkibSelect.call_pttClient svcCaller;
        private SvcSenjataRkibSelect.InputParameters inputPar;
        private SvcSenjataRkibSelect.OutputParameters outputPar;
        private SvcSenjataRkibSelect.BPSIMANSROW_M_KSENJ_DOK_KIB rowData;

        protected ArrayList dataGrid;
        protected bool dataInisial;

        public void getInitialDataDokKibBg()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcSenjataRkibSelect.InputParameters();
                inputPar.P_COL = "";
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcSenjataRkibSelect.call_pttClient();
                svcCaller.Open();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataDokKibSjt), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataDokKibSjt(IAsyncResult result)
        {
            try
            {
                this.outputPar = svcCaller.Endexecute(result);
                svcCaller.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataDokKibAngk(this.showDataDokKibSjt), this.outputPar);
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

        private delegate void ShowDataDokKibAngk(SvcSenjataRkibSelect.OutputParameters dataOut);

        public void showDataDokKibSjt(SvcSenjataRkibSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KSENJ_DOK_KIB.Count();
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
                this.gcDokKibAngk.Text = "Input Data Dokumen KIB Senjata";
                this.idKsjtKib = 0;
                teNoDokKib.ResetText();
                teTglDok.ResetText();
                meKet.ResetText();
                teNmFile.ResetText();
                
            }
            else if (this.status == "edit" || this.status == "detail")
            {
                try
                {
                    if (this.status == "detail")
                    {
                        this.gcDokKibAngk.Text = "Detail Data Dokumen KIB Senjata";
                    }
                    else
                    {
                        this.gcDokKibAngk.Text = "Edit Data Dokumen KIB Senjata";
                    }
                    this.idKsjtKib = selectedData.ID_KSENJ_DOK_KIB;
                    //this.teKdSmilik.Text = selectedData.KD_SMILIK;
                    //this.teUrSmilik.Text = selectedData.UR_SMILIK;

                    this.teNoDokKib.Text = selectedData.NO_DOK_KIB;
                    this.teTglDok.Text = konfigApp.DateToString(selectedData.TGL_DOK_KIB);
                    this.teNmFile.Text = selectedData.NMFILE;
                    this.meKet.Text = selectedData.KET_DOK;

                }
                catch (Exception)
                {

                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }

            }
            else
            {
                this.idKsjtKib = selectedData.ID_KSENJ_DOK_KIB;
            }
            this.Focus();

        }

        #region crud
        public void crudDokKibSjt(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcSenjataRkibCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcSenjataRkibCrud.OutputParameters outCrud)
        {
            SvcSenjataRkibSelect.BPSIMANSROW_M_KSENJ_DOK_KIB dataPenyama = new SvcSenjataRkibSelect.BPSIMANSROW_M_KSENJ_DOK_KIB();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KSENJ_DOK_KIB = outCrud.PO_ID_KSENJ_DOK_KIB;
            this.idKsjtKib = outCrud.PO_ID_KSENJ_DOK_KIB;

            switch (this.modeCrud)
            {
                case 'C':


                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;

                        simpanFile("ID_KSENJ_DOK_KIB", dataPenyama.ID_KSENJ_DOK_KIB, "M_KSENJ_DOK_KIB", Path, "C");
                        this.UcRKibSenjata.search = "";
                        this.UcRKibSenjata.pencarian = false;
                        this.UcRKibSenjata.initGrid();
                        this.UcRKibSenjata.getInitDokKib();
                    }
                    else
                    {
                        this.UcRKibSenjata.dataInisial = false;
                        this.UcRKibSenjata.getById = true;
                        this.UcRKibSenjata.getInitDokKib(" ID_KSENJ_DOK_KIB = " + this.idKsjtKib.ToString());
                        this.init();
                        this.Close();
                    }
                    break;
                case 'U':
                    UcRKibSenjata.binder.Remove(this.selectedData);
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                            simpanFile("ID_KSENJ_DOK_KIB", dataPenyama.ID_KSENJ_DOK_KIB, "M_KSENJ_DOK_KIB", Path, "U");
                        }
                        else
                        {
                            simpanFile("ID_KSENJ_DOK_KIB", dataPenyama.ID_KSENJ_DOK_KIB, "M_KSENJ_DOK_KIB", Path, "C");
                            //UcRKibSenjata.gvUcDtl.RefreshData();
                        }
                    }
                    else
                    {
                        //this.UcRKibSenjata.dataInisial = false;
                        //this.UcRKibSenjata.getById = true;
                        // this.UcRKibSenjata.getInitDokKib(" ID_KSENJ_DOK_KIB = " + this.idKsjtKib.ToString());

                        UcRKibSenjata.gvUcDtl.RefreshData();
                        this.init();
                        this.Close();
                        this.UcRKibSenjata.search = "";
                        this.UcRKibSenjata.pencarian = false;
                        this.UcRKibSenjata.initGrid();
                        this.UcRKibSenjata.getInitDokKib();
                    }
                    break;
                case 'D':
                    UcRKibSenjata.binder.Remove(this.selectedData);
                    UcRKibSenjata.gvUcDtl.RefreshData();
                    UcRKibSenjata.StrTotalGrid.Caption = (Convert.ToInt64(UcRKibSenjata.StrTotalGrid.Caption) - 1).ToString();
                    UcRKibSenjata.StrTotalDb.Caption = (Convert.ToInt64(UcRKibSenjata.StrTotalDb.Caption) - 1).ToString();
                    this.init();
                    this.Close();
                    this.UcRKibSenjata.search = "";
                    this.UcRKibSenjata.pencarian = false;
                    this.UcRKibSenjata.initGrid();
                    this.UcRKibSenjata.getInitDokKib();
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
                        this.UcRKibSenjata.dataInisial = false;
                    }
                    this.UcRKibSenjata.getById = true;
                    //this.UcRKibSenjata.getInitDokKib(" ID_KSENJ_DOK_KIB = " + this.idKsjtKib.ToString());

                }

                this.Close();
                this.UcRKibSenjata.search = "";
                this.UcRKibSenjata.pencarian = false;
                this.UcRKibSenjata.initGrid();
                this.UcRKibSenjata.getInitDokKib();
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

                    SvcSenjataRkibCrud.InputParameters parInp = new SvcSenjataRkibCrud.InputParameters();
                    parInp.P_ID_KSENJ_DOK_KIBSpecified = true;
                    parInp.P_ID_KSENJ_DOK_KIB = this.idKsjtKib;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcSenjataRkibCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibSjt), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }

        private void PuSenjataKib_Load(object sender, EventArgs e)
        {
            this.getInitialDataDokKibBg();
         
        }
    }
}