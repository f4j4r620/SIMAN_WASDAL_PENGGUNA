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

namespace AppPengguna.AST.RN
{
    internal partial class FrmKibRmhNgr : Form
    {
        private UcDokKibRmhNgr ucKibAngk = null;
        private string status;
        public string ID_JNSDOK;
        private SvcKibRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK_KIB selectedData;
        private SvcKibRmhNgrCrud.InputParameters parInp;
        private SvcKibRmhNgrCrud.OutputParameters outCrud;
        private SvcKibRmhNgrCrud.call_pttClient crudData;

        private decimal? idKangk;
        private decimal? idKangkKib;
        public decimal? NUM;

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';

        string FilePath;

        //public DateTime? tglDok
        //{
        //    set { teTglDok.EditValue = value; }
        //    get { return Convert.ToDateTime(teTglDok.EditValue); }
        //}

        public FrmKibRmhNgr(UcDokKibRmhNgr _ucKibAngk, string status)
        {
            InitializeComponent();
            this.ucKibAngk = _ucKibAngk;
            this.status = status;
            this.idKangk = _ucKibAngk.IdKangk;
            // this.idKangk = _ucKibAngk.selectedData.ID_KANGK;
            this.selectedData = _ucKibAngk.selectedData;

            if (_ucKibAngk.selectedData != null)
            {
                this.NUM = _ucKibAngk.selectedData.NUM;
            }
            if (status == "edit" || status == "detail")
            {
                this.bbKibSimpan.Enabled = false;
                this.sbNamaFile.Enabled = false;
                this.sbJnsDok.Enabled = false;
                
            }
            init();
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

                    SvcKibRmhNgrCrud.InputParameters parInp = new SvcKibRmhNgrCrud.InputParameters();
                    parInp.P_ID_KRMH_NEGSpecified = true;
                    parInp.P_ID_KRMH_NEG_DOK_KIBSpecified = true;

                    parInp.P_ID_KRMH_NEG_DOK_KIB = idKangkKib;
                    parInp.P_ID_KRMH_NEG = idKangk;
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
                    this.crudData = new SvcKibRmhNgrCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibAngk), "");

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
            this.getInitialDataDokKibAngk();
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

                       // teNmPath.Text = FilePath;
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

        private SvcKibRmhNgrSelect.call_pttClient svcCaller;
        private SvcKibRmhNgrSelect.InputParameters inputPar;
        private SvcKibRmhNgrSelect.OutputParameters outputPar;
        private SvcKibRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK_KIB rowData;

        protected ArrayList dataGrid;
        protected bool dataInisial;

        public void getInitialDataDokKibAngk()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcKibRmhNgrSelect.InputParameters();
                inputPar.P_COL = "";
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcKibRmhNgrSelect.call_pttClient();
                svcCaller.Open();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataDokKibAngk), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataDokKibAngk(IAsyncResult result)
        {
            try
            {
                this.outputPar = svcCaller.Endexecute(result);
                svcCaller.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataDokKibAngk(this.showDataDokKibAngk), this.outputPar);
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

        private delegate void ShowDataDokKibAngk(SvcKibRmhNgrSelect.OutputParameters dataOut);

        public void showDataDokKibAngk(SvcKibRmhNgrSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KRMH_NEG_DOK_KIB.Count();
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
                this.gcDokKibAngk.Text = "Input Data Dokumen KIB Rumah Negara";
                this.idKangkKib = 0;
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
                        this.gcDokKibAngk.Text = "Detail Data Dokumen KIB Rumah Negara";
                    }
                    else
                    {
                        this.gcDokKibAngk.Text = "Edit Data Dokumen KIB Angkutan";
                    }
                    this.idKangkKib = selectedData.ID_KRMH_NEG_DOK_KIB;
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
                this.idKangkKib = selectedData.ID_KRMH_NEG_DOK_KIB;
            }
            this.Focus();

        }

        #region crud
        public void crudDokKibAngk(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcKibRmhNgrCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcKibRmhNgrCrud.OutputParameters outCrud)
        {
            SvcKibRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK_KIB dataPenyama = new SvcKibRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK_KIB();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KRMH_NEG_DOK_KIB = outCrud.PO_ID_KRMH_NEG_DOK_KIB;
            this.idKangkKib = outCrud.PO_ID_KRMH_NEG_DOK_KIB;

            switch (this.modeCrud)
            {
                case 'C':


                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;

                        simpanFile("ID_KRMH_NEG_DOK_KIB", dataPenyama.ID_KRMH_NEG_DOK_KIB, "M_KRMH_NEG_DOK_KIB", Path, "C", this.ID_JNSDOK);
                       
                    }
                    else
                    {
                        this.ucKibAngk.dataInisial = false;
                        this.ucKibAngk.getById = true;
                        this.ucKibAngk.getInitDokKib(" ID_KRMH_NEG_DOK_KIB = " + this.idKangkKib.ToString());
                        this.init();
                        this.Close();
                    }
                    break;
                case 'U':
                    ucKibAngk.binder.Remove(this.selectedData);
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                          simpanFile("ID_KRMH_NEG_DOK_KIB", dataPenyama.ID_KRMH_NEG_DOK_KIB, "M_KRMH_NEG_DOK_KIB", Path, "U", this.ID_JNSDOK);
                        }
                        else
                        {
                          simpanFile("ID_KRMH_NEG_DOK_KIB", dataPenyama.ID_KRMH_NEG_DOK_KIB, "M_KRMH_NEG_DOK_KIB", Path, "C", this.ID_JNSDOK);
                            //ucKibAngk.gvUcDtl.RefreshData();
                        }
                    }
                    else
                    {
                        //this.ucKibAngk.dataInisial = false;
                        //this.ucKibAngk.getById = true;
                        // this.ucKibAngk.getInitDokKib(" ID_KRMH_NEG_DOK_KIB = " + this.idKangkKib.ToString());

                        ucKibAngk.gvUcDtl.RefreshData();
                        this.init();
                        this.Close();
                        this.ucKibAngk.search = "";
                        this.ucKibAngk.pencarian = false;
                        this.ucKibAngk.initGrid();
                        this.ucKibAngk.getInitDokKib();
                    }
                    break;
                case 'D':
                    ucKibAngk.binder.Remove(this.selectedData);
                    ucKibAngk.gvUcDtl.RefreshData();
                    ucKibAngk.StrTotalGrid.Caption = (Convert.ToInt64(ucKibAngk.StrTotalGrid.Caption) - 1).ToString();
                    ucKibAngk.StrTotalDb.Caption = (Convert.ToInt64(ucKibAngk.StrTotalDb.Caption) - 1).ToString();
                    this.init();
                    this.Close();
                    this.ucKibAngk.search = "";
                    this.ucKibAngk.pencarian = false;
                    this.ucKibAngk.initGrid();
                    this.ucKibAngk.getInitDokKib();
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
                  inputData.P_KD_DOK = id_jnsDok ;
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
                        this.ucKibAngk.dataInisial = false;
                    }
                    this.ucKibAngk.getById = true;
                    //this.ucKibAngk.getInitDokKib(" ID_KRMH_NEG_DOK_KIB = " + this.idKangkKib.ToString());

                }

                this.Close();
                this.ucKibAngk.search = "";
                this.ucKibAngk.pencarian = false;
                this.ucKibAngk.initGrid();
                this.ucKibAngk.getInitDokKib();
            }

        }
        #endregion




        private void FrmKibRmhNgr_Load(object sender, EventArgs e)
        {
            this.getInitialDataDokKibAngk();
            this.teJnsDok.Properties.ReadOnly = true;
            this.teNmFile.Properties.ReadOnly = true;
        }


        public void hapusData()
        {
            if (MessageBox.Show(konfigApp.teksHapusData + " No " + this.selectedData.NUM.ToString() + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcKibRmhNgrCrud.InputParameters parInp = new SvcKibRmhNgrCrud.InputParameters();
                    parInp.P_ID_KRMH_NEG_DOK_KIBSpecified = true;
                    parInp.P_ID_KRMH_NEG_DOK_KIB = this.idKangkKib;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcKibRmhNgrCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibAngk), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
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