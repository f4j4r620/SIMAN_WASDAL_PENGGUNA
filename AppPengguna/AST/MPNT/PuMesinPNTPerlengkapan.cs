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
    internal partial class PuMesinPNTPerlengkapan : Form
    {
        private ucMesinSpmPerlengkapan UcMesinSpmPerlengkapan = null;

        private string status;

        private SvcMesinPntPerlengkapanSelect.BPSIMANSROW_KALB_FAS_PENUNJANG selectedData;
        private SvcMesinPntPerlengkapanCrud.InputParameters parInp;
        private SvcMesinPntPerlengkapanCrud.OutputParameters outCrud;
        private SvcMesinPntPerlengkapanCrud.call_pttClient crudData;

        private decimal? idKmpnt;
        private decimal? idKmpntPerlengkapan;
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

        public PuMesinPNTPerlengkapan(ucMesinSpmPerlengkapan _UcMesinSpmPerlengkapan, string status)
        {
            InitializeComponent();
            this.UcMesinSpmPerlengkapan = _UcMesinSpmPerlengkapan;
            this.status = status;
            this.idKmpnt = _UcMesinSpmPerlengkapan.IdKmpnt;
            // this.idKmpnt = _UcMesinSpmPerlengkapan.selectedData.ID_KANGK;
            this.selectedData = _UcMesinSpmPerlengkapan.selectedData;

            if (_UcMesinSpmPerlengkapan.selectedData != null)
            {
                this.NUM = _UcMesinSpmPerlengkapan.selectedData.NUM;
            }
            if (status == "detail")
            {
                this.bbKibSimpan.Enabled = false;
               
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
            string teNmP = teNmPerlengkapan.Text;
            string teKet = meKet.Text;
           

                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcMesinPntPerlengkapanCrud.InputParameters parInp = new SvcMesinPntPerlengkapanCrud.InputParameters();
                    parInp.P_ID_KALBSpecified = true;
                    parInp.P_ID_KALB_FAS_PENUNJANGSpecified = true;

                    parInp.P_ID_KALB_FAS_PENUNJANG = idKmpntPerlengkapan;
                    parInp.P_ID_KALB = idKmpnt;
                    parInp.P_NM_FASILITAS = teNmP;
                    parInp.P_KET = teKet;
                  

                    if (this.status == "input")
                    {
                        parInp.P_SELECT = "C";
                    }
                    else
                    {
                        parInp.P_SELECT = "U";
                    }

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcMesinPntPerlengkapanCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudPerlengkapan), "");

                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
           

        private void bbKibRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.getInitialDataPerlengkapan();
        }

        private void bbKibKembali_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        //private void sbNamaFile_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string fileName = "";
        //        string filePath;
        //        long fileSize = 0;
        //        string creationTime = "";

        //        OpenFileDialog dialog = new OpenFileDialog();
        //        dialog.Dispose();
        //        dialog.Title = "Open PDF Files";
        //        dialog.Filter = "PDF Files(*.pdf)|*.pdf";
        //        dialog.Multiselect = false;


        //        if (dialog.ShowDialog() == DialogResult.OK)
        //        {
        //            filePath = dialog.FileName;
        //            fileName = dialog.SafeFileName;
        //            fileSize = new System.IO.FileInfo(dialog.FileName).Length;
        //            creationTime = new System.IO.FileInfo(dialog.FileName).CreationTime.ToString();

        //            if (fileSize < konfigApp.maksSizeFile)
        //            {
        //                this.FilePath = filePath;
        //                teNmFile.Text = fileName;

        //                //teNmPath.Text = FilePath;
        //                this.path = FilePath;
        //            }
        //            else
        //            {
        //                MessageBox.Show(konfigApp.konfirmasiMaksimalFile, konfigApp.judulGagalLain);
        //            }


        //            Console.WriteLine(fileSize + creationTime);

        //        }
        //    }
        //    catch
        //    {
        //        System.Console.WriteLine("gagal");
        //    }
        //}

        private SvcMesinPntPerlengkapanSelect.call_pttClient svcCaller;
        private SvcMesinPntPerlengkapanSelect.InputParameters inputPar;
        private SvcMesinPntPerlengkapanSelect.OutputParameters outputPar;
        private SvcMesinPntPerlengkapanSelect.BPSIMANSROW_KALB_FAS_PENUNJANG rowData;

        protected ArrayList dataGrid;
        protected bool dataInisial;

        public void getInitialDataPerlengkapan()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcMesinPntPerlengkapanSelect.InputParameters();
                inputPar.P_COL = "";
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcMesinPntPerlengkapanSelect.call_pttClient();
                svcCaller.Open();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataPerlengkapan), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataPerlengkapan(IAsyncResult result)
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

        private delegate void ShowDataDokKibAngk(SvcMesinPntPerlengkapanSelect.OutputParameters dataOut);

        public void showDataDokKibBg(SvcMesinPntPerlengkapanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KALB_FAS_PENUNJANG.Count();
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
                this.gcDokKibAngk.Text = "Input Data Perlengkapan";
                this.idKmpntPerlengkapan = 0;
                teNmPerlengkapan.ResetText();   
                meKet.ResetText();
            }
            else if (this.status == "edit" || this.status == "detail")
            {
                try
                {
                    if (this.status == "detail")
                    {
                        this.gcDokKibAngk.Text = "Detail Data Perlengkapan";
                    }
                    else
                    {
                        this.gcDokKibAngk.Text = "Edit Data Perlengkapan";
                    }
                    this.idKmpntPerlengkapan = selectedData.ID_KALB_FAS_PENUNJANG;
                    //this.teKdSmilik.Text = selectedData.KD_SMILIK;
                    //this.teUrSmilik.Text = selectedData.UR_SMILIK;

                    this.teNmPerlengkapan.Text = selectedData.NM_FASILITAS;
                    this.meKet.Text = selectedData.KET;
                }
                catch (Exception)
                {

                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }

            }
            else
            {
                this.idKmpntPerlengkapan = selectedData.ID_KALB_FAS_PENUNJANG;
            }
            this.Focus();

        }

        #region crud
        public void crudPerlengkapan(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcMesinPntPerlengkapanCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcMesinPntPerlengkapanCrud.OutputParameters outCrud)
        {
            SvcMesinPntPerlengkapanSelect.BPSIMANSROW_KALB_FAS_PENUNJANG dataPenyama = new SvcMesinPntPerlengkapanSelect.BPSIMANSROW_KALB_FAS_PENUNJANG();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KALB_FAS_PENUNJANG = outCrud.PO_ID_KALB_FAS_PENUNJANG;
            this.idKmpntPerlengkapan = outCrud.PO_ID_KALB_FAS_PENUNJANG;

            switch (this.modeCrud)
            {
                case 'C':


                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;

                        simpanFile("ID_KALB_FAS_PENUNJANG", dataPenyama.ID_KALB_FAS_PENUNJANG, "M_KALB_FAS_PENUNJANG", Path, "C");
                        this.UcMesinSpmPerlengkapan.search = "";
                        this.UcMesinSpmPerlengkapan.pencarian = false;
                        this.UcMesinSpmPerlengkapan.initGrid();
                        this.UcMesinSpmPerlengkapan.getInitPerlengkapan();
                    }
                    else
                    {
                        this.UcMesinSpmPerlengkapan.dataInisial = false;
                        this.UcMesinSpmPerlengkapan.getById = true;
                        this.UcMesinSpmPerlengkapan.getInitPerlengkapan(" ID_KALB_FAS_PENUNJANG = " + this.idKmpntPerlengkapan.ToString());
                        this.init();
                        this.Close();
                    }
                    break;
                case 'U':
                    UcMesinSpmPerlengkapan.binder.Remove(this.selectedData);
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        //if (this.selectedData.FILE_EXISTS != 0)
                        //{
                        //    simpanFile("ID_KALB_FAS_PENUNJANG", dataPenyama.ID_KALB_FAS_PENUNJANG, "M_KBDG_DOK_KIB", Path, "U");
                        //}
                        //else
                        //{
                        simpanFile("ID_KALB_FAS_PENUNJANG", dataPenyama.ID_KALB_FAS_PENUNJANG, "M_KALB_FAS_PENUNJANG", Path, "C");
                            //UcMesinSpmPerlengkapan.gvUcDtl.RefreshData();
                        //}
                    }
                    else
                    {
                        //this.UcMesinSpmPerlengkapan.dataInisial = false;
                        //this.UcMesinSpmPerlengkapan.getById = true;
                        // this.UcMesinSpmPerlengkapan.getInitPerlengkapan(" ID_KALB_FAS_PENUNJANG = " + this.idKmpntPerlengkapan.ToString());

                        UcMesinSpmPerlengkapan.gvUcDtl.RefreshData();
                        this.init();
                        this.Close();
                        this.UcMesinSpmPerlengkapan.search = "";
                        this.UcMesinSpmPerlengkapan.pencarian = false;
                        this.UcMesinSpmPerlengkapan.initGrid();
                        this.UcMesinSpmPerlengkapan.getInitPerlengkapan();
                    }
                    break;
                case 'D':
                    UcMesinSpmPerlengkapan.binder.Remove(this.selectedData);
                    UcMesinSpmPerlengkapan.gvUcDtl.RefreshData();
                    UcMesinSpmPerlengkapan.StrTotalGrid.Caption = (Convert.ToInt64(UcMesinSpmPerlengkapan.StrTotalGrid.Caption) - 1).ToString();
                    UcMesinSpmPerlengkapan.StrTotalDb.Caption = (Convert.ToInt64(UcMesinSpmPerlengkapan.StrTotalDb.Caption) - 1).ToString();
                    this.init();
                    this.Close();
                    this.UcMesinSpmPerlengkapan.search = "";
                    this.UcMesinSpmPerlengkapan.pencarian = false;
                    this.UcMesinSpmPerlengkapan.initGrid();
                    this.UcMesinSpmPerlengkapan.getInitPerlengkapan();
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
                        this.UcMesinSpmPerlengkapan.dataInisial = false;
                    }
                    this.UcMesinSpmPerlengkapan.getById = true;
                    //this.UcMesinSpmPerlengkapan.getInitPerlengkapan(" ID_KALB_FAS_PENUNJANG = " + this.idKmpntPerlengkapan.ToString());

                }

                this.Close();
                this.UcMesinSpmPerlengkapan.search = "";
                this.UcMesinSpmPerlengkapan.pencarian = false;
                this.UcMesinSpmPerlengkapan.initGrid();
                this.UcMesinSpmPerlengkapan.getInitPerlengkapan();
            }

        }
        #endregion




        private void FormRwyDokKib_Load(object sender, EventArgs e)
        {
            this.getInitialDataPerlengkapan();

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

                    SvcMesinPntPerlengkapanCrud.InputParameters parInp = new SvcMesinPntPerlengkapanCrud.InputParameters();
                    parInp.P_ID_KALB_FAS_PENUNJANGSpecified = true;
                    parInp.P_ID_KALB_FAS_PENUNJANG = this.idKmpntPerlengkapan;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcMesinPntPerlengkapanCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudPerlengkapan), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }
    }
}