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

namespace AppPengguna.AST.TN
{
    internal partial class frmTnhFoto : Form
    {
        private ucTnhFoto ucTnhFoto = null;

        private string status;

        private SvcTanahRiwayatFotoSelect.BPSIMANSROW_M_ASET_PHOTO_GRID selectedData;
        private SvcTanahRiwayatFotoCrud.InputParameters parInp;
        private SvcTanahRiwayatFotoCrud.OutputParameters outCrud;
        private SvcTanahRiwayatFotoCrud.call_pttClient crudData;

        public SvcAsetPhotoCrud.call_pttClient svcAsetPhotoCrud;
        public SvcAsetPhotoCrud.OutputParameters outFotoCrud;

        private decimal? idAset;
        private decimal? idFoto;
        public decimal? NUM;
        public string path;
        public int JumlahFoto = 0;

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';

        string FilePath;


        public frmTnhFoto(ucTnhFoto _ucTnhFoto, string status)
        {
            InitializeComponent();
            this.ucTnhFoto = _ucTnhFoto;
            this.status = status;
            this.idAset = _ucTnhFoto.IdAset;
            
           
            this.selectedData = _ucTnhFoto.selectedData;

            if (_ucTnhFoto.selectedData != null)
            {
                this.NUM = _ucTnhFoto.selectedData.NUM;
            }
            if (status == "detail")
            {
                this.bbiSimpan.Enabled = false;
                this.sbUpload.Enabled = false;
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


        private void bbKibKembali_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private SvcTanahRiwayatFotoSelect.photoSelectGrid_pttClient svcCaller;
        private SvcTanahRiwayatFotoSelect.InputParameters inputPar;
        private SvcTanahRiwayatFotoSelect.OutputParameters outputPar;
        private SvcTanahRiwayatFotoSelect.BPSIMANSROW_M_ASET_PHOTO_GRID rowData;

        protected ArrayList dataGrid;
        protected bool dataInisial;

        public void getInitFoto()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcTanahRiwayatFotoSelect.InputParameters();
                inputPar.P_COL = "";
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcTanahRiwayatFotoSelect.photoSelectGrid_pttClient();
                svcCaller.Open();
                svcCaller.BeginphotoSelectGrid(inputPar, new AsyncCallback(this.getDataDokKibTnh), null);
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
                this.outputPar = svcCaller.EndphotoSelectGrid(result);
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

        private delegate void ShowDataDokKibAngk(SvcTanahRiwayatFotoSelect.OutputParameters dataOut);

        public void showDataDokKibTnh(SvcTanahRiwayatFotoSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID.Count();
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
               
                this.idFoto = 0;
               
                meKet.ResetText();
                teNmFile.ResetText();
                
            }
            else if (this.status == "edit" || this.status == "detail")
            {
                try
                {
                    
                    this.idFoto = selectedData.ID_PHOTO;
                    this.teNmFile.Text = selectedData.NM_PHOTO;
                    this.meKet.Text = selectedData.KET_PHOTO;
                }
                catch (Exception)
                {

                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }

            }
            else
            {
                this.idFoto = selectedData.ID_PHOTO;
            }
            this.Focus();

        }

        #region crud
        public void crudFoto(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcTanahRiwayatFotoCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcTanahRiwayatFotoCrud.OutputParameters outCrud)
        {
            SvcTanahRiwayatFotoSelect.BPSIMANSROW_M_ASET_PHOTO_GRID dataPenyama = new SvcTanahRiwayatFotoSelect.BPSIMANSROW_M_ASET_PHOTO_GRID();

            dataPenyama.NUM = 99;
            dataPenyama.ID_PHOTO = outCrud.PO_ID_PHOTO;
            this.idFoto = outCrud.PO_ID_PHOTO;

            switch (this.modeCrud)
            {
                case 'C':
                        this.ucTnhFoto.dataInisial = false;
                        this.ucTnhFoto.getById = true;
                        //this.ucTnhFoto.getInitFoto(" ID_PHOTO = " + this.idFoto.ToString());
                        this.init();
                        this.Close();
                        this.ucTnhFoto.search = "";
                        this.ucTnhFoto.pencarian = false;
                        this.ucTnhFoto.initGrid();
                        this.ucTnhFoto.getInitFoto();
                    break;
                case 'U':
                        ucTnhFoto.binder.Remove(this.selectedData);
                        ucTnhFoto.gvUcDtl.RefreshData();
                        this.init();
                        this.Close();
                        this.ucTnhFoto.search = "";
                        this.ucTnhFoto.pencarian = false;
                        this.ucTnhFoto.initGrid();
                        this.ucTnhFoto.getInitFoto();
        
                    break;
                case 'D':
                        ucTnhFoto.binder.Remove(this.selectedData);
                        ucTnhFoto.gvUcDtl.RefreshData();
                        ucTnhFoto.StrTotalGrid.Caption = (Convert.ToInt64(ucTnhFoto.StrTotalGrid.Caption) - 1).ToString();
                        ucTnhFoto.StrTotalDb.Caption = (Convert.ToInt64(ucTnhFoto.StrTotalDb.Caption) - 1).ToString();
                        this.ucTnhFoto.search = "";
                        this.ucTnhFoto.pencarian = false;
                        this.ucTnhFoto.initGrid();
                        this.ucTnhFoto.getInitFoto();
                        this.Close();
                    break;
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

                    SvcTanahRiwayatFotoCrud.InputParameters parInp = new SvcTanahRiwayatFotoCrud.InputParameters();
                    parInp.P_ID_PHOTOSpecified = true;
                    parInp.P_ID_PHOTO = this.idFoto;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcTanahRiwayatFotoCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudFoto), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }

        private void frmTnhFoto_Load(object sender, EventArgs e)
        {
            this.getInitFoto();
            this.teNmFile.Properties.ReadOnly = true;
            
        }

        private void sbUpload_Click(object sender, EventArgs e)
        {

            try
            {
                string fileName = "";
                string filePath;
                long fileSize = 0;
                string creationTime = "";

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Dispose();
                dialog.Title = "Open Photo File";
                dialog.Filter = "(*.bmp, *.jpg, *.gif, *.png)|*.bmp;*.jpg;*.gif;*.png";
                dialog.Multiselect = false;


                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    fileName = dialog.SafeFileName;
                    fileSize = new System.IO.FileInfo(dialog.FileName).Length;
                    creationTime = new System.IO.FileInfo(dialog.FileName).CreationTime.ToString();

                    if (fileSize < konfigApp.maksSizeFoto)
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
                System.Console.WriteLine("gagal load upload");
            }
        }

        private void bbiSimpan_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            string ketKib = meKet.Text;
            string nmFileKib = teNmFile.Text;
            if (teNmFile.Text != "")
            {

                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcTanahRiwayatFotoCrud.InputParameters parInp = new SvcTanahRiwayatFotoCrud.InputParameters();
                    parInp.P_ID_ASETSpecified = true;
                    parInp.P_ID_PHOTOSpecified = true;

                    parInp.P_ID_PHOTO = idFoto;
                    parInp.P_ID_ASET = idAset;
                    parInp.P_KET_PHOTO = ketKib;
                    parInp.P_NM_PHOTO = nmFileKib;
                    parInp.P_PHOTO = konfigApp.FileToByteArray(FilePath);

                    if (this.status == "input")
                    {
                        parInp.P_SELECT = "C";
                    }
                    else
                    {
                        parInp.P_SELECT = "U";
                    }

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcTanahRiwayatFotoCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudFoto), "");

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
                MessageBox.Show("File foto harus terisi!", konfigApp.judulGagalSimpan);
            }
        }

        private void bbiRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.getInitFoto();
        }

   
    }
}