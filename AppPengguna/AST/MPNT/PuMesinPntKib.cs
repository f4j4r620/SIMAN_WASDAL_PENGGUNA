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

namespace AppPengguna.AST.MPNT
{
    internal partial class PuMesinPntKib : Form
    {
        private ucMesinPntKib UcMesinPntKib = null;

        private string status;

        private SvcMesinPntKibSelect.BPSIMANSROW_M_KALB_DOK_KIB selectedData;
        private SvcMesinPntKibCrud.InputParameters parInp;
        private SvcMesinPntKibCrud.OutputParameters outCrud;
        private SvcMesinPntKibCrud.call_pttClient crudData;

        private decimal? idKmesinPnt;
        private decimal? idKmesinPntKib;
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

        public PuMesinPntKib(ucMesinPntKib _UcMesinPntKib, string status)
        {
            InitializeComponent();
            this.UcMesinPntKib = _UcMesinPntKib;
            this.status = status;
            this.idKmesinPnt = _UcMesinPntKib.IdKmPnt;
            // this.idKmesinPnt = _UcMesinPntKib.selectedData.ID_KANGK;
            this.selectedData = _UcMesinPntKib.selectedData;

            if (_UcMesinPntKib.selectedData != null)
            {
                this.NUM = _UcMesinPntKib.selectedData.NUM;
            }
            if (status == "detail")
            {
                this.bbKibSimpan.Enabled = false;
                this.sbNamaFile.Enabled = false;
            }
            else if(status == "edit")
          {
            this.sbJnsDok.Enabled = false;
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

                    SvcMesinPntKibCrud.InputParameters parInp = new SvcMesinPntKibCrud.InputParameters();
                    parInp.P_ID_KALBSpecified = true;
                    parInp.P_ID_KALB_DOK_KIBSpecified = true;

                    parInp.P_ID_KALB_DOK_KIB = idKmesinPntKib;
                    parInp.P_ID_KALB = idKmesinPnt;
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
                    this.crudData = new SvcMesinPntKibCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibMesinPnt), "");

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
            this.getInitialDataDokKibMesinPnt();
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

        private SvcMesinPntKibSelect.call_pttClient svcCaller;
        private SvcMesinPntKibSelect.InputParameters inputPar;
        private SvcMesinPntKibSelect.OutputParameters outputPar;
        private SvcMesinPntKibSelect.BPSIMANSROW_M_KALB_DOK_KIB rowData;

        protected ArrayList dataGrid;
        protected bool dataInisial;

        public void getInitialDataDokKibMesinPnt()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcMesinPntKibSelect.InputParameters();
                inputPar.P_COL = "";
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcMesinPntKibSelect.call_pttClient();
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

        private delegate void ShowDataDokKibAngk(SvcMesinPntKibSelect.OutputParameters dataOut);

        public void showDataDokKibBg(SvcMesinPntKibSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KALB_DOK_KIB.Count();
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
                this.gcDokKibAngk.Text = "Input Data Dokumen KIB Mesin Peralatan Non TIK";
                this.idKmesinPntKib = 0;
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
                        this.gcDokKibAngk.Text = "Detail Data Dokumen KIB Mesin Peralatan Non TIK";
                    }
                    else
                    {
                        this.gcDokKibAngk.Text = "Edit Data Dokumen KIB Mesin Peralatan Non TIK";
                    }
                    this.idKmesinPntKib = selectedData.ID_KALB_DOK_KIB;
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
                this.idKmesinPntKib = selectedData.ID_KALB_DOK_KIB;
            }
            this.Focus();

        }

        #region crud
        public void crudDokKibMesinPnt(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcMesinPntKibCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcMesinPntKibCrud.OutputParameters outCrud)
        {
            SvcMesinPntKibSelect.BPSIMANSROW_M_KALB_DOK_KIB dataPenyama = new SvcMesinPntKibSelect.BPSIMANSROW_M_KALB_DOK_KIB();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KALB_DOK_KIB = outCrud.PO_ID_KALB_DOK_KIB;
            this.idKmesinPntKib = outCrud.PO_ID_KALB_DOK_KIB;

            switch (this.modeCrud)
            {
                case 'C':


                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;

                        simpanFile("ID_KALB_DOK_KIB", dataPenyama.ID_KALB_DOK_KIB, "M_KALB_DOK_KIB", Path, "C", ID_JNSDOK);
                        this.UcMesinPntKib.search = "";
                        this.UcMesinPntKib.pencarian = false;
                        this.UcMesinPntKib.initGrid();
                        this.UcMesinPntKib.getInitDokKib();
                    }
                    else
                    {
                        this.UcMesinPntKib.dataInisial = false;
                        this.UcMesinPntKib.getById = true;
                        this.UcMesinPntKib.getInitDokKib(" ID_KALB_DOK_KIB = " + this.idKmesinPntKib.ToString());
                        this.init();
                        this.Close();
                    }
                    break;
                case 'U':
                    UcMesinPntKib.binder.Remove(this.selectedData);
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                          simpanFile("ID_KALB_DOK_KIB", dataPenyama.ID_KALB_DOK_KIB, "M_KALB_DOK_KIB", Path, "U", ID_JNSDOK);
                          this.UcMesinPntKib.search = "";
                          this.UcMesinPntKib.pencarian = false;
                          this.UcMesinPntKib.initGrid();
                          this.UcMesinPntKib.getInitDokKib();
                        }
                        else
                        {
                          simpanFile("ID_KALB_DOK_KIB", dataPenyama.ID_KALB_DOK_KIB, "M_KALB_DOK_KIB", Path, "C", ID_JNSDOK);
                          this.UcMesinPntKib.search = "";
                          this.UcMesinPntKib.pencarian = false;
                          this.UcMesinPntKib.initGrid();
                          this.UcMesinPntKib.getInitDokKib();
                            //UcMesinPntKib.gvUcDtl.RefreshData();
                        }
                    }
                    else
                    {
                        //this.UcMesinPntKib.dataInisial = false;
                        //this.UcMesinPntKib.getById = true;
                        // this.UcMesinPntKib.getInitDokKib(" ID_KALB_DOK_KIB = " + this.idKmesinPntKib.ToString());

                        UcMesinPntKib.gvUcDtl.RefreshData();
                        this.init();
                        this.Close();
                        this.UcMesinPntKib.search = "";
                        this.UcMesinPntKib.pencarian = false;
                        this.UcMesinPntKib.initGrid();
                        this.UcMesinPntKib.getInitDokKib();
                    }
                    break;
                case 'D':
                    UcMesinPntKib.binder.Remove(this.selectedData);
                    UcMesinPntKib.gvUcDtl.RefreshData();
                    UcMesinPntKib.StrTotalGrid.Caption = (Convert.ToInt64(UcMesinPntKib.StrTotalGrid.Caption) - 1).ToString();
                    UcMesinPntKib.StrTotalDb.Caption = (Convert.ToInt64(UcMesinPntKib.StrTotalDb.Caption) - 1).ToString();
                    this.init();
                    this.Close();
                    this.UcMesinPntKib.search = "";
                    this.UcMesinPntKib.pencarian = false;
                    this.UcMesinPntKib.initGrid();
                    this.UcMesinPntKib.getInitDokKib();
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
                        this.UcMesinPntKib.dataInisial = false;
                    }
                    this.UcMesinPntKib.getById = true;
                    //this.UcMesinPntKib.getInitDokKib(" ID_KALB_DOK_KIB = " + this.idKmesinPntKib.ToString());

                }

                this.Close();
                this.UcMesinPntKib.search = "";
                this.UcMesinPntKib.pencarian = false;
                this.UcMesinPntKib.initGrid();
                this.UcMesinPntKib.getInitDokKib();
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

                    SvcMesinPntKibCrud.InputParameters parInp = new SvcMesinPntKibCrud.InputParameters();
                    parInp.P_ID_KALB_DOK_KIBSpecified = true;
                    parInp.P_ID_KALB_DOK_KIB = this.idKmesinPntKib;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcMesinPntKibCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibMesinPnt), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }

        private void PuMesinPntKib_Load(object sender, EventArgs e)
        {
            this.getInitialDataDokKibMesinPnt();
            this.teJnsDok.Properties.ReadOnly = true;
            this.teNmFile.Properties.ReadOnly = true;
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