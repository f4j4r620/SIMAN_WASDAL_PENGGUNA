using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.AST.TN
{
    internal partial class frmTnhHukum : Form
    {
  
        private ucTnhHukum uctnhhkm;
        public string status;
        public decimal? NUM;
        public string FilePath;
        private decimal? idKtnh;
        private decimal? idKtnhHkm;
        public string ID_JNSDOK;
        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private SvcTnhHukumSelect.BPSIMANSROW_M_KTNH_HUKUM selectedData;
        private SvcTnhHukumCrud.call_pttClient crudData;
        private SvcTnhHukumCrud.InputParameters inputCrud;
        private SvcTnhHukumCrud.OutputParameters outCrud;

        private AppPengguna.PU.FrmPuStatusHukum frmStsHkm;

        protected decimal currentMaks;
        protected decimal currentMin;
        SvcStatusHukumSelect.call_pttClient svcStatusHukumSelect;
        SvcStatusHukumSelect.OutputParameters outParHukum;

        #region set get
        public string kdStsHkm
        {
            get { return this.teKdStsHkm.Text; }
            set { this.teKdStsHkm.Text = value; }
        }
        public string jnsStsHkm
        {
            get { return this.teJnsStsHkm.Text; }
            set { this.teJnsStsHkm.Text = value; }
        }
        public string stsHkm
        {
            get { return this.teStsHkm.Text; }
            set { this.teStsHkm.Text = value; }
        }
        #endregion

        #region set popup
        public void setPopUpStsHkm(string kdStsHkm, string jnsStsHkm, string stsHkm)
        {
            this.kdStsHkm = kdStsHkm;
            this.jnsStsHkm = jnsStsHkm;
            this.stsHkm = stsHkm;
        }
        #endregion

     
        public frmTnhHukum( ucTnhHukum uctnhhkm, string status)
        {
            InitializeComponent();
           
            this.uctnhhkm = uctnhhkm;
            this.status = status;
            this.idKtnh = uctnhhkm.IdKtnh;
            this.selectedData = uctnhhkm.selectedData;
            if (uctnhhkm.selectedData != null)
            {
                this.NUM = uctnhhkm.selectedData.NUM;
            }
            
            this.frmStsHkm = new PU.FrmPuStatusHukum();
            this.frmStsHkm.ambilStatusHukum = new PU.AmbilStatusHukum(setPopUpStsHkm);
            if (this.status == "detail")
            {
                this.bbiSave.Enabled = false;
                this.sbPilihFile.Enabled = false;
                this.sbStsHkm.Enabled = false;
            }
        }



        private void init()
        {
            if (this.status == "input")
            {
                this.gcTnhHkm.Text = "Input Data Permasalahan Hukum";
                this.idKtnhHkm = 0;
                this.deTgl.ResetText();
                this.teKdStsHkm.ResetText();
                int idx = this.teJnsStsHkm.Properties.GetDataSourceRowIndex("KD_STATUS_HUKUM", "201");
                this.teJnsStsHkm.EditValue = this.teJnsStsHkm.Properties.GetDataSourceValue("KD_STATUS_HUKUM", idx);
                this.teStsHkm.ResetText();
                this.tePhkSengketa.ResetText();
                this.meUrMslh.ResetText();
                this.teFileName.ResetText();
                this.ceTerakhir.Checked = false;
                this.teJnsDok.ResetText();

            }
            else if (this.status == "edit" || this.status == "detail")
            {

                if (this.status == "detail")
                {
                    this.gcTnhHkm.Text = "Detail Data Permasalahan Hukum";
                }
                else
                {
                    this.gcTnhHkm.Text = "Edit Data Permasalahan Hukum";
                }
                this.idKtnhHkm = selectedData.ID_M_KTNH_HUKUM;
                this.deTgl.Text = konfigApp.DateToString(selectedData.TGL);
                this.teKdStsHkm.Text = konfigApp.StringtoNull(selectedData.KD_STATUS_HUKUM);
                //this.teJnsStsHkm.Text = selectedData.JNS_STATUS_HUKUM;
                this.teStsHkm.Text = selectedData.STATUS_HUKUM;
                this.tePhkSengketa.Text = selectedData.PHK_SENGKETA;
                this.meUrMslh.Text = selectedData.UR_MASALAH;
                this.teFileName.Text = selectedData.NMFILE;
                string terakhir = selectedData.TERAKHIR_YN;
                this.teJnsDok.Enabled = false;
                this.sbJnsDok.Enabled = false;

                try
                {
                    if (selectedData.KD_STATUS_HUKUM != "-")
                    {

                        // this.teUrDok.Properties.ValueMember = selectedData.KD_SMILIK;
                        int idx = this.teJnsStsHkm.Properties.GetDataSourceRowIndex("KD_STATUS_HUKUM", selectedData.KD_STATUS_HUKUM);
                        this.teJnsStsHkm.EditValue = this.teJnsStsHkm.Properties.GetDataSourceValue("KD_STATUS_HUKUM", idx);
                        idx = this.teJnsStsHkm.Properties.GetDataSourceRowIndex("KD_STATUS_HUKUM", selectedData.KD_STATUS_HUKUM);
                    }


                }
                catch (Exception)
                {

                }
                if (terakhir == "Y")
                {
                    this.ceTerakhir.Checked = true;
                }
                else
                {
                    this.ceTerakhir.Checked = false;
                }

            }
            else
            {
                this.idKtnhHkm = selectedData.ID_M_KTNH_HUKUM;
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
        private void aktifkanForm(string text)
        {
            this.Enabled = true;
        }
        #endregion

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime tgl;
            DateTime.TryParse(deTgl.Text, out tgl);

            string kdSttsHkm = teKdStsHkm.Text;
            string phkBersengketa = tePhkSengketa.Text;
            string urMslh = meUrMslh.Text;
            string nmFile = teFileName.Text;
            string file = teFilePath.Text;

            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcTnhHukumCrud.InputParameters parInp = new SvcTnhHukumCrud.InputParameters();
                parInp.P_ID_KTNHSpecified = true;
                parInp.P_ID_M_KTNH_HUKUMSpecified = true;

                parInp.P_ID_KTNH = idKtnh;
                parInp.P_ID_M_KTNH_HUKUM = idKtnhHkm;
                parInp.P_TGL = konfigApp.DateToDb(this.deTgl.Text);
               
                parInp.P_KD_STATUS_HUKUM = kdSttsHkm;
                parInp.P_PHK_SENGKETA = phkBersengketa;
                parInp.P_UR_MASALAH = urMslh;

                if (FilePath != null)
                {
                    parInp.P_NMFILE = nmFile;

                }
                else
                {
                    if (this.status == "edit")
                    {
                        parInp.P_NMFILE = selectedData.NMFILE;
                    }
                }

                if (this.status == "input")
                {
                    parInp.P_SELECT = "C";
                }
                else
                {
                    parInp.P_SELECT = "U";
                }

                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                this.crudData = new SvcTnhHukumCrud.call_pttClient(konfigApp.SvcTnhHukumCrud_name, konfigApp.SvcTnhHukumCrud_address);
                crudData.Open();
                this.crudData.Beginexecute(parInp, new AsyncCallback(crudHkmTnh), "");
            }
            catch
            {
                this.modeCrud = 'A';
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.init();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        public void hapusData()
        {
            if (MessageBox.Show(String.Format("{0} No. {1} ?", konfigApp.teksHapusData, this.selectedData.NUM), konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcTnhHukumCrud.InputParameters parInp = new SvcTnhHukumCrud.InputParameters();

                    parInp.P_ID_M_KTNH_HUKUMSpecified = true;
                    parInp.P_ID_M_KTNH_HUKUM = idKtnhHkm;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcTnhHukumCrud.call_pttClient(konfigApp.SvcTnhHukumCrud_name, konfigApp.SvcTnhHukumCrud_address);
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudHkmTnh), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }

        #region crud
        public void crudHkmTnh(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcTnhHukumCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcTnhHukumCrud.OutputParameters outCrud)
        {
            SvcTnhHukumSelect.BPSIMANSROW_M_KTNH_HUKUM dataPenyama = new SvcTnhHukumSelect.BPSIMANSROW_M_KTNH_HUKUM();
            this.idKtnhHkm = (this.status == "input")? outCrud.PO_ID_M_KTNH_HUKUM : selectedData.ID_M_KTNH_HUKUM;
            dataPenyama.NUM = 99;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_KTNH = idKtnh;
            dataPenyama.ID_M_KTNH_HUKUM = (this.status == "input") ? outCrud.PO_ID_M_KTNH_HUKUM : selectedData.ID_M_KTNH_HUKUM;
            dataPenyama.TGL = deTgl.DateTime;
            dataPenyama.KD_STATUS_HUKUM = teKdStsHkm.Text;
            dataPenyama.JNS_STATUS_HUKUM = teJnsStsHkm.Text;
            dataPenyama.STATUS_HUKUM = teStsHkm.Text;
            dataPenyama.PHK_SENGKETA = tePhkSengketa.Text;
            dataPenyama.UR_MASALAH = meUrMslh.Text;
            dataPenyama.NMFILE = teFileName.Text;
            string terakhir;
            if (ceTerakhir.Checked) { terakhir = "Y"; } else { terakhir = "N"; }
            dataPenyama.TERAKHIR_YN = terakhir;
            switch (this.modeCrud)
            {
                case 'C':
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        simpanFile("ID_M_KTNH_HUKUM", dataPenyama.ID_M_KTNH_HUKUM, "M_KTNH_HUKUM", Path, "C", ID_JNSDOK);
                    }
                    else
                    {
                        this.uctnhhkm.dataInisial = false;
                        this.uctnhhkm.getById = true;
                        this.uctnhhkm.getInitTnhHkm(" ID_M_KTNH_HUKUM = " + this.idKtnhHkm.ToString());
                        this.Close();
                    }

                    break;
                case 'U':
                    uctnhhkm.binder.Remove(this.selectedData);
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                            simpanFile("ID_M_KTNH_HUKUM", dataPenyama.ID_M_KTNH_HUKUM, "M_KTNH_HUKUM", Path, "U", ID_JNSDOK);
                        }
                        else
                        {
                            simpanFile("ID_M_KTNH_HUKUM", dataPenyama.ID_M_KTNH_HUKUM, "M_KTNH_HUKUM", Path, "C", ID_JNSDOK);
                        }
                    }
                    else
                    {
                        this.uctnhhkm.dataInisial = false;
                        this.uctnhhkm.getById = true;
                        this.uctnhhkm.getInitTnhHkm(" ID_M_KTNH_HUKUM = " + this.idKtnhHkm.ToString());
                        this.Close();
                    }
               

                    break;
                case 'D':
                    uctnhhkm.binder.Remove(this.selectedData);
                     this.init();
                        this.Close();
                    break;
            }
        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string FilePath, string SELECT,string id_jnsDok = null)
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
                    this.uctnhhkm.dataInisial = false;
                    this.uctnhhkm.getById = true;
                    this.uctnhhkm.getInitTnhHkm(" ID_M_KTNH_HUKUM = " + this.idKtnhHkm);
                }

                this.Close();
            }

        }
        #endregion

        private void sbStsHkm_Click(object sender, EventArgs e)
        {
            this.frmStsHkm.ShowDialog();
        }

        private void sbPilihFile_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                string filePath = "";
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
                        FilePath = filePath;
                        teFileName.Text = fileName;
                        teFilePath.Text = FilePath;
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

        private void frmTnhHukum_Load(object sender, EventArgs e)
        {
            this.getInitialStatusukum();
            this.teFileName.Properties.ReadOnly = true;
        }
        public  void getInitialStatusukum()
        {
            try
            {


                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcStatusHukumSelect.InputParameters inputParHukum = new SvcStatusHukumSelect.InputParameters();
                inputParHukum.P_COL = "";
                
                 this.currentMaks = 80;
                 this.currentMin = 0;
               
                //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

                inputParHukum.P_MAX = this.currentMaks;
                inputParHukum.P_MAXSpecified = true;
                inputParHukum.P_MIN = this.currentMin;
                inputParHukum.P_MINSpecified = true;
                inputParHukum.P_SORT = "";
                svcStatusHukumSelect = new SvcStatusHukumSelect.call_pttClient();
                svcStatusHukumSelect.Beginexecute(inputParHukum, new AsyncCallback(this.getStatusHukum), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        
        public  void getStatusHukum(IAsyncResult result)
        {
            try
            {
                this.outParHukum = svcStatusHukumSelect.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowData(this.showData), this.outParHukum);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcStatusHukumSelect.OutputParameters dataOut);

        public void showData(SvcStatusHukumSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_STATUS_HUKUM.Count();

            DataRow dtRow;
            for (int i = 0; i < jmlDataGroup; i++)
            {
                //this.dataGrid.Add(serviceOutPut.SF_ROW_R_SMILIK[i]);
                dtRow = dataTable1.NewRow();
                dtRow["JNS_STATUS_HUKUM"] = serviceOutPut.SF_ROW_R_STATUS_HUKUM[i].JNS_STATUS_HUKUM;
                dtRow["KD_STATUS_HUKUM"] = serviceOutPut.SF_ROW_R_STATUS_HUKUM[i].KD_STATUS_HUKUM;
                dtRow["STATUS_HUKUM"] = serviceOutPut.SF_ROW_R_STATUS_HUKUM[i].STATUS_HUKUM;
                dataTable1.Rows.Add(dtRow);
            }

            teJnsStsHkm.Properties.DataSource = dataTable1;
            teJnsStsHkm.Properties.DisplayMember = "JNS_STATUS_HUKUM";
            teJnsStsHkm.Properties.ValueMember = "KD_STATUS_HUKUM";
            teJnsStsHkm.Properties.ShowHeader = false;
            teJnsStsHkm.Properties.ShowFooter = false;

            this.init();

        }

        private void teJnsStsHkm_EditValueChanged(object sender, EventArgs e)
        {
            this.teJnsStsHkm.EditValueChanged -= new System.EventHandler(this.teJnsStsHkm_EditValueChanged);

            this.kdStsHkm = teJnsStsHkm.GetColumnValue("KD_STATUS_HUKUM").ToString();
            //this.jnsStsHkm = teJnsStsHkm.GetColumnValue("JNS_STATUS_HUKUM").ToString();
            this.stsHkm = teJnsStsHkm.GetColumnValue("STATUS_HUKUM").ToString();

            this.teJnsStsHkm.EditValueChanged += new System.EventHandler(this.teJnsStsHkm_EditValueChanged);
            this.teJnsStsHkm.ClosePopup();
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
