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

namespace AppPengguna.AST.TN
{
    internal partial class frmTnhDokKib : Form
    {
        private ucTnhDokKib UcTnhDokKib = null;

        private string status;

        private SvcTnhDokKibSelect.BPSIMANSROW_M_KTNH_DOK_KIB selectedData;
        private SvcTnhDokKibCrud.InputParameters parInp;
        private SvcTnhDokKibCrud.OutputParameters outCrud;
        private SvcTnhDokKibCrud.execute_pttClient crudData;

        private decimal? idKtnh;
        private decimal? idKtnhKib;
        public decimal? NUM;
        public string path;
        public string ID_JNSDOK;

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';

        string FilePath;

        //public DateTime? tglDok
        //{
        //    set { teTglDok.EditValue = value; }
        //    get { return Convert.ToDateTime(teTglDok.EditValue); }
        //}

        public frmTnhDokKib(ucTnhDokKib _UcTnhDokKib, string status)
        {
            InitializeComponent();
            this.UcTnhDokKib = _UcTnhDokKib;
            this.status = status;
            this.idKtnh = _UcTnhDokKib.IdKtnh;
            // this.idKtnh = _UcTnhDokKib.selectedData.ID_KANGK;
            this.selectedData = _UcTnhDokKib.selectedData;

            if (_UcTnhDokKib.selectedData != null)
            {
                this.NUM = _UcTnhDokKib.selectedData.NUM;
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

                    SvcTnhDokKibCrud.InputParameters parInp = new SvcTnhDokKibCrud.InputParameters();
                    parInp.P_ID_KTNHSpecified = true;
                    parInp.P_ID_KTNH_DOK_KIBSpecified = true;

                    parInp.P_ID_KTNH_DOK_KIB = idKtnhKib;
                    parInp.P_ID_KTNH = idKtnh;
                    parInp.P_NO_DOK_KIB = noDokKib;
                    parInp.P_TGL_DOK_KIB = tglDokKib;
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
                    this.crudData = new SvcTnhDokKibCrud.execute_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibTnh), "");

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
            this.getInitialDataDokKibTnhKib();
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

        private SvcTnhDokKibSelect.execute_pttClient svcCaller;
        private SvcTnhDokKibSelect.InputParameters inputPar;
        private SvcTnhDokKibSelect.OutputParameters outputPar;
        private SvcTnhDokKibSelect.BPSIMANSROW_M_KTNH_DOK_KIB rowData;

        protected ArrayList dataGrid;
        protected bool dataInisial;

        public void getInitialDataDokKibTnhKib()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcTnhDokKibSelect.InputParameters();
                inputPar.P_COL = "";
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcTnhDokKibSelect.execute_pttClient();
                svcCaller.Open();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataDokKibTnh), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataDokKibTnh(IAsyncResult result)
        {
            try
            {
                this.outputPar = svcCaller.Endexecute(result);
                svcCaller.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataDokKibAngk(this.showDataDokKibTnh), this.outputPar);
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

        private delegate void ShowDataDokKibAngk(SvcTnhDokKibSelect.OutputParameters dataOut);

        public void showDataDokKibTnh(SvcTnhDokKibSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_DOK_KIB.Count();
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
                this.gcDokKibAngk.Text = "Input Data Dokumen KIB Tanah";
                this.idKtnhKib = 0;
                teNoDokKib.ResetText();
                teTglDok.ResetText();
                meKet.ResetText();
                teNmFile.ResetText();
                teJnsDok.ResetText();
            }
            else if (this.status == "edit" || this.status == "detail")
            {
                try
                {
                    if (this.status == "detail")
                    {
                        this.gcDokKibAngk.Text = "Detail Data Dokumen KIB Tanah";
                    }
                    else
                    {
                        this.gcDokKibAngk.Text = "Edit Data Dokumen KIB Tanah";
                    }
                    this.idKtnhKib = selectedData.ID_KTNH_DOK_KIB;
                    //this.teKdSmilik.Text = selectedData.KD_SMILIK;
                    //this.teUrSmilik.Text = selectedData.UR_SMILIK;

                    this.teNoDokKib.Text = selectedData.NO_DOK_KIB;
                    this.teTglDok.Text = konfigApp.DateToString(selectedData.TGL_DOK_KIB);
                    this.teNmFile.Text = selectedData.NMFILE;
                    this.meKet.Text = selectedData.KET_DOK;
                    this.teJnsDok.Enabled = false;
                    this.sbJnsDok.Enabled = false;

                }
                catch (Exception)
                {

                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }

            }
            else
            {
                this.idKtnhKib = selectedData.ID_KTNH_DOK_KIB;
            }
            this.Focus();

        }

        #region crud
        public void crudDokKibTnh(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcTnhDokKibCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcTnhDokKibCrud.OutputParameters outCrud)
        {
            SvcTnhDokKibSelect.BPSIMANSROW_M_KTNH_DOK_KIB dataPenyama = new SvcTnhDokKibSelect.BPSIMANSROW_M_KTNH_DOK_KIB();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KTNH_DOK_KIB = outCrud.PO_ID_KTNH_DOK_KIB;
            this.idKtnhKib = outCrud.PO_ID_KTNH_DOK_KIB;

            switch (this.modeCrud)
            {
                case 'C':


                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;

                        simpanFile("ID_KTNH_DOK_KIB", dataPenyama.ID_KTNH_DOK_KIB, "M_KTNH_DOK_KIB", Path, "C", ID_JNSDOK);
                        this.UcTnhDokKib.search = "";
                        this.UcTnhDokKib.pencarian = false;
                        this.UcTnhDokKib.initGrid();
                        this.UcTnhDokKib.getInitDokKib();
                    }
                    else
                    {
                        this.UcTnhDokKib.dataInisial = false;
                        this.UcTnhDokKib.getById = true;
                        this.UcTnhDokKib.getInitDokKib(" ID_KTNH_DOK_KIB = " + this.idKtnhKib.ToString());
                        this.init();
                        this.Close();
                    }
                    break;
                case 'U':
                    UcTnhDokKib.binder.Remove(this.selectedData);
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                            simpanFile("ID_KTNH_DOK_KIB", dataPenyama.ID_KTNH_DOK_KIB, "M_KTNH_DOK_KIB", Path, "U", ID_JNSDOK);
                        }
                        else
                        {
                            simpanFile("ID_KTNH_DOK_KIB", dataPenyama.ID_KTNH_DOK_KIB, "M_KTNH_DOK_KIB", Path, "C", ID_JNSDOK);
                            //UcTnhDokKib.gvUcDtl.RefreshData();
                        }
                    }
                    else
                    {
                        //this.UcTnhDokKib.dataInisial = false;
                        //this.UcTnhDokKib.getById = true;
                        // this.UcTnhDokKib.getInitDokKib(" ID_KTNH_DOK_KIB = " + this.idKtnhKib.ToString());

                        UcTnhDokKib.gvUcDtl.RefreshData();
                        this.init();
                        this.Close();
                        this.UcTnhDokKib.search = "";
                        this.UcTnhDokKib.pencarian = false;
                        this.UcTnhDokKib.initGrid();
                        this.UcTnhDokKib.getInitDokKib();
                    }
                    break;
                case 'D':
                    UcTnhDokKib.binder.Remove(this.selectedData);
                    UcTnhDokKib.gvUcDtl.RefreshData();
                    UcTnhDokKib.StrTotalGrid.Caption = (Convert.ToInt64(UcTnhDokKib.StrTotalGrid.Caption) - 1).ToString();
                    UcTnhDokKib.StrTotalDb.Caption = (Convert.ToInt64(UcTnhDokKib.StrTotalDb.Caption) - 1).ToString();
                    this.init();
                    this.Close();
                    this.UcTnhDokKib.search = "";
                    this.UcTnhDokKib.pencarian = false;
                    this.UcTnhDokKib.initGrid();
                    this.UcTnhDokKib.getInitDokKib();
                    break;
            }
        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string FilePath, string SELECT, string id_jnsDok = null)
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
                if (id_jnsDok != null)
                {
                    inputData.P_KD_DOK = id_jnsDok;
                }
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
                        this.UcTnhDokKib.dataInisial = false;
                    }
                    this.UcTnhDokKib.getById = true;
                    //this.UcTnhDokKib.getInitDokKib(" ID_KTNH_DOK_KIB = " + this.idKtnhKib.ToString());

                }

                this.Close();
                this.UcTnhDokKib.search = "";
                this.UcTnhDokKib.pencarian = false;
                this.UcTnhDokKib.initGrid();
                this.UcTnhDokKib.getInitDokKib();
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

                    SvcTnhDokKibCrud.InputParameters parInp = new SvcTnhDokKibCrud.InputParameters();
                    parInp.P_ID_KTNH_DOK_KIBSpecified = true;
                    parInp.P_ID_KTNH_DOK_KIB = this.idKtnhKib;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcTnhDokKibCrud.execute_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibTnh), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }

        private void frmTnhDokKib_Load(object sender, EventArgs e)
        {
            this.getInitialDataDokKibTnhKib();
            this.teNmFile.Properties.ReadOnly = true;
            this.teJnsDok.Properties.ReadOnly = true;
        }

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
    }
}