using System;
using System.Collections;
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
using System.Globalization;
using System.IO;
using DevExpress.XtraEditors.Controls;

namespace AppPengguna.AST.TN
{
    internal partial class frmTnhDok : Form
    {
  
        public BindingSource binder;
        private ucTnhDok uctnhdok = null;
        public string status;
         string FilePath;
        public decimal? NUM;
        private PU.FrmPUSmilik popUpSmilik;
        private PU.FrmPUJnsSertifikat popUpSertifikat;

        private decimal? idKtnh;
        private decimal? idKtnhDok;
        public string ID_JNSDOK;
        public string NM_JNSDOK;
        public string statusJns = null;
        
        
        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK selectedData;
        private SvcTnhDokCrud.call_pttClient crudData;
        private SvcTnhDokCrud.InputParameters inputCrud;
        private SvcTnhDokCrud.OutputParameters outCrud;

        // ------------------------- jenis dok ------------------------
        private SvcAstJnsDok.call_pttClient svcCallerJns;
        private SvcAstJnsDok.InputParameters inputParJns;
        private SvcAstJnsDok.OutputParameters outParJns;
        private SvcAstJnsDok.BPSIMANSROW_R_JNS_DOK rowDataJns;

        // ------------------------------------------------------------

        #region set get
        public string kdSmilik
        {
            get { return this.teKdSmilik.Text; }
            set { this.teKdSmilik.Text = value; }
        }
        public string urSmilik
        {
            get { return this.teUrSmilik.Text; }
            set { this.teUrSmilik.Text = value; }
        }
        public string urDok
        {
            get { return this.teUrDok.Text; }
            set { this.teUrDok.Text = value; }
        }
        public string kdJnsSrtf
        {
            get { return this.teKdJnsSrtf.Text; }
            set { this.teKdJnsSrtf.Text = value; }
        }
        public string nmJnsSrtf
        {
            get { return this.teNmJnsSrtf.Text; }
            set { this.teNmJnsSrtf.Text = value; }
        }
        #endregion

        #region set popup
        public void setPopSmilik(string kdSmilik, string urSmilik, string urDok)
        {
            this.kdSmilik = kdSmilik;
            this.urSmilik = urSmilik;
            this.urDok = urDok;
        }
        public void setPopSertifikat(string kdJnsSrtf, string nmJnsSrtf)
        {
            this.kdJnsSrtf = kdJnsSrtf;
            this.nmJnsSrtf = nmJnsSrtf;
        }
        #endregion

        public frmTnhDok(ucTnhDok _uctnhdok, string status)
        {
            InitializeComponent();
            this.uctnhdok = _uctnhdok;
            this.status = status;
            this.idKtnh = _uctnhdok.IdKtnh;
            this.selectedData = _uctnhdok.selectedData;
            if (_uctnhdok.selectedData != null)
            {
                this.NUM = _uctnhdok.selectedData.NUM;
            }
            if (this.status == "detail")
            {
                this.bbiSave.Enabled = false;
                this.sbUpload.Enabled = false;
            }
            
                this.teFileName.Properties.ReadOnly = true;
           
        }
        public void aktifkanForm(string str)
        {
            this.Enabled = true;
            
        }
        public void init()
        {
           
            if (this.status == "input")
            {
                this.gcDokTnh.Text = "Input Data Dokumen Tanah";
                this.idKtnhDok = 0;
                teUrSmilik.ResetText();
                teUrDok.ResetText();
                teKdJnsSrtf.ResetText();
                teNoSertifikat.ResetText();
                deTglDok.ResetText();
                deTglBerlaku.ResetText();
                teAtasNama.ResetText();
                tePenerbit.ResetText();
                meKetDok.ResetText();
                teJnsDok.ResetText();
            }
            else if (this.status == "edit" || this.status == "detail")
            {
                try
                {
                    if (this.status == "detail")
                    {
                        this.gcDokTnh.Text = "Detail Data Dokumen Tanah";
                    }
                    else
                    {
                        this.gcDokTnh.Text = "Edit Data Dokumen Tanah";
                    }
                    this.idKtnhDok = selectedData.ID_KTNH_DOK;
                    //this.teKdSmilik.Text = selectedData.KD_SMILIK;
                    //this.teUrSmilik.Text = selectedData.UR_SMILIK;
                  
                    try
                    {
                        if (selectedData.KD_SMILIK != "-")
                        {
                           
                           // this.teUrDok.Properties.ValueMember = selectedData.KD_SMILIK;
                            int idx = this.teUrDok.Properties.GetDataSourceRowIndex("KD_SMILIK", selectedData.KD_SMILIK);
                            this.teUrDok.EditValue = this.teUrDok.Properties.GetDataSourceValue("KD_SMILIK",idx);
                            idx = this.teUrDok.Properties.GetDataSourceRowIndex("KD_SMILIK", selectedData.KD_SMILIK);
                        }
                        
                        
                    }
                    catch (Exception)
                    {
                        
                    }
                    try
                    {
                       
                        if (selectedData.KD_JNS_SERTI != "-")
                        {
                            int idx = this.teKdJnsSrtf.Properties.GetDataSourceRowIndex("KD_JNS_SERTI", selectedData.KD_JNS_SERTI);
                            this.teKdJnsSrtf.EditValue = this.teKdJnsSrtf.Properties.GetDataSourceValue("KD_JNS_SERTI", idx);
                            
                        }
                      
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                    this.teNoSertifikat.Text = selectedData.NO_SERTIFIKAT;
                    this.teAtasNama.Text = selectedData.ATAS_NAMA;
                    this.tePenerbit.Text = selectedData.PENERBIT;
                    this.meKetDok.Text = selectedData.KET_DOK;
                    this.deTglDok.Text = konfigApp.DateToString(selectedData.TGL_DOK);
                    this.deTglBerlaku.Text = konfigApp.DateToString(selectedData.TGL_BERLAKU);
                    this.teFileName.Text = selectedData.NMFILE;
                    
                    
                }
                catch (Exception)
                {

                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }
                
            }
            else
            {
                this.idKtnhDok = selectedData.ID_KTNH_DOK;
            }
            this.Focus();
            this.teKdJnsSrtf.ClosePopup();
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

        

        public void hapusData()
        {
            if (MessageBox.Show(konfigApp.teksHapusData + " No " + this.selectedData.NUM.ToString() + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcTnhDokCrud.InputParameters parInp = new SvcTnhDokCrud.InputParameters();
                    parInp.P_ID_KTNH_DOKSpecified = true;
                    parInp.P_ID_KTNH_DOK = this.idKtnhDok;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcTnhDokCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokTnh), "");
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
        public void crudDokTnh(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcTnhDokCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcTnhDokCrud.OutputParameters outCrud)
        {
            SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK dataPenyama = new SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KTNH_DOK = outCrud.PO_ID_KTNH_DOK;
            this.idKtnhDok = outCrud.PO_ID_KTNH_DOK;
            
            switch (this.modeCrud)
            {
                case 'C':

                   
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        simpanFile("ID_KTNH_DOK", dataPenyama.ID_KTNH_DOK, "M_KTNH_DOK", Path, "C", ID_JNSDOK);
                        
                    }
                    else
                    {
                        this.uctnhdok.dataInisial = false;
                        this.uctnhdok.getById = true;
                        this.uctnhdok.getInitTnhDok(" ID_KTNH_DOK = " + this.idKtnhDok.ToString());
                       
                        
                    }
                        
                       
                    break;
                case 'U':
                    uctnhdok.binder.Remove(this.selectedData);
                    if (this.FilePath != null && this.statusJns != "U")
                    {
                        string Path = this.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                            simpanFile("ID_KTNH_DOK", dataPenyama.ID_KTNH_DOK, "M_KTNH_DOK", Path, "U", ID_JNSDOK);
                            
                        }
                        else
                        {
                            simpanFile("ID_KTNH_DOK", dataPenyama.ID_KTNH_DOK, "M_KTNH_DOK", Path, "C", ID_JNSDOK);
                            
                        }
                    }
                    else if (statusJns == "U")
                    {
                        string Path = "";
                        simpanFile("ID_M_KTNH_NJOP", dataPenyama.ID_KTNH_DOK, "M_KTNH_DOK", Path, "U", ID_JNSDOK);
                        
                    }
                    else
                    {
                        this.uctnhdok.dataInisial = false;
                        this.uctnhdok.getById = true;
                        this.uctnhdok.getInitTnhDok(" ID_KTNH_DOK = " + this.idKtnhDok.ToString());
                        this.init();
                        this.Close();
                    }
                   
                       
                    break;
                case 'D':
                    uctnhdok.binder.Remove(this.selectedData);
                    uctnhdok.gvUcDtl.RefreshData();
                    uctnhdok.StrTotalGrid.Caption = (Convert.ToInt64(uctnhdok.StrTotalGrid.Caption) - 1).ToString();
                    uctnhdok.StrTotalDb.Caption = (Convert.ToInt64(uctnhdok.StrTotalDb.Caption) - 1).ToString();
                     this.init();
                      this.Close();
                        this.uctnhdok.search = "";
                        this.uctnhdok.pencarian = false;
                        this.uctnhdok.initGrid();
                        this.uctnhdok.getInitTnhDok();
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
                inputData.P_ISI_FILE = konfigApp.FileToByteArray(FilePath);
                if (id_jnsDok != null)
                {
                    inputData.P_KD_DOK = id_jnsDok;
                }
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
                    this.uctnhdok.dataInisial = false;
                    this.uctnhdok.getById = true;
                    //this.uctnhdok.getInitTnhDok(" ID_KTNH_DOK = " + this.idKtnhDok.ToString());
                    this.Close();
                    this.uctnhdok.search = "";
                    this.uctnhdok.pencarian = false;
                    this.uctnhdok.initGrid();
                    this.uctnhdok.getInitTnhDok();
                }

                
            }

        }
        #endregion

        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            string kdSmilik = (teKdSmilik.Text != "-") ? teKdSmilik.Text : null;
            string kdJnsSrtf = (teKdJnsSrtf.Text != "-") ? teKdJnsSrtf.Text : null;
            string noSrtf = teNoSertifikat.Text;

            string atasNama = teAtasNama.Text;
            string penerbit = tePenerbit.Text;
            string ketDok = meKetDok.Text;
            string tglDok = deTglDok.Text;
            string tglBerlaku = deTglBerlaku.Text;
          
            string nmFile = teFileName.Text;
            string file = teFilePath.Text;
            if (teFileName.Text != "")
            {

                try
                {

                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcTnhDokCrud.InputParameters parInp = new SvcTnhDokCrud.InputParameters();
                    parInp.P_ID_KTNHSpecified = true;
                    parInp.P_ID_KTNH_DOKSpecified = true;
             

                    parInp.P_ID_KTNH_DOK = konfigApp.DecimaltoNull(idKtnhDok);
                    parInp.P_ID_KTNH = idKtnh;
                    parInp.P_KD_SMILIK = konfigApp.StringtoNull(kdSmilik);
                    parInp.P_KD_JNS_SERTI = kdJnsSrtf;
                    parInp.P_NO_SERTIFIKAT = noSrtf;
                    parInp.P_TGL_DOK = tglDok;
                    parInp.P_TGL_BERLAKU = tglBerlaku;
                    parInp.P_ATAS_NAMA = atasNama;
                    parInp.P_PENERBIT = penerbit;
                    parInp.P_KET_DOK = ketDok;

                    parInp.P_NMFILE = nmFile;
              
                

                    if (this.status == "input")
                    {
                        parInp.P_SELECT = "C";
                    }
                    else
                    {
                        parInp.P_SELECT = "U";
                    }


                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcTnhDokCrud.call_pttClient();
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokTnh), "");
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
                MessageBox.Show("File dokumen harus terisi!", konfigApp.judulGagalSimpan);
            }
        }

        private void bbiReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.getInitialDataDokMilik();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void sbSmilik_Click(object sender, EventArgs e)
        {
            this.popUpSmilik.ShowDialog();
        }

        private void sbJnsSrtf_Click(object sender, EventArgs e)
        {
            this.popUpSertifikat.ShowDialog();
        }

        public byte[] convert2byte(string file)
        {
            FileStream fs = new FileStream(file,
                                           FileMode.Open,
                                           FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return filebytes;
        }
        public byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                // close file reader
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            return _Buffer;
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

        private SvcSmilikSelect.call_pttClient svcCaller;
        private SvcSmilikSelect.InputParameters inputPar;
        private SvcSmilikSelect.OutputParameters outPar;
        private SvcSmilikSelect.BPSIMANSROW_R_SMILIK rowData;
        
        protected ArrayList dataGrid;
        protected bool dataInisial;
        protected decimal dataAwal;
        protected decimal dataAkhir;
        protected decimal currentMaks;
        protected decimal currentMin;
        protected bool loadMore = true;

        public void getInitialDataDokMilik()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputPar = new SvcSmilikSelect.InputParameters();
                inputPar.P_COL = "";                
                inputPar.P_MAX = 100;
                inputPar.P_MAXSpecified = true;
                inputPar.P_MIN = 0;
                inputPar.P_MINSpecified = true;
                inputPar.P_SORT = "";
                svcCaller = new SvcSmilikSelect.call_pttClient(konfigApp.SvcSmilikSelect_name, konfigApp.SvcSmilikSelect_address);
                svcCaller.Open();
                svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataDokMilik), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataDokMilik(IAsyncResult result)
        {
            try
            {
                this.outPar = svcCaller.Endexecute(result);
                svcCaller.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataDokMilik(this.showDataDokMilik), this.outPar);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataDokMilik(SvcSmilikSelect.OutputParameters dataOut);

        public void showDataDokMilik(SvcSmilikSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_SMILIK.Count();

            if (this.dataInisial == true)
            {
                this.dataGrid = new ArrayList();
            }
            DataRow dtRow;
            dataTable1.Rows.Clear();
            for (int i = 0; i < jmlDataGroup; i++)
            {
                //this.dataGrid.Add(serviceOutPut.SF_ROW_R_SMILIK[i]);
                dtRow = dataTable1.NewRow();
                dtRow["KD_SMILIK"] = serviceOutPut.SF_ROW_R_SMILIK[i].KD_SMILIK;
                dtRow["UR_SMILIK"] = serviceOutPut.SF_ROW_R_SMILIK[i].UR_SMILIK;
                dtRow["UR_DOK"] = serviceOutPut.SF_ROW_R_SMILIK[i].UR_DOK;
                dataTable1.Rows.Add(dtRow);
            }

            teUrDok.Properties.DataSource = dataTable1;
            teUrDok.Properties.DisplayMember = "UR_DOK";
            teUrDok.Properties.ValueMember = "KD_SMILIK";
            teUrDok.Properties.ShowHeader = false;
            teUrDok.Properties.ShowFooter = false;

            if (jmlDataGroup < 5)
            {
                this.loadMore = false;
            }


            this.getInitialDataSertifikat();
           
        }

        private SvcJnsSertifikatSelect.call_pttClient svcCallerSertifikat;
        private SvcJnsSertifikatSelect.InputParameters inputParSertifikat;
        private SvcJnsSertifikatSelect.OutputParameters outParSertifikat;
        private SvcJnsSertifikatSelect.BPSIMANSROW_R_JNS_SERTI rowDataSertifikat;

        public void getInitialDataSertifikat()
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                inputParSertifikat = new SvcJnsSertifikatSelect.InputParameters();
                inputParSertifikat.P_COL = "";
                inputParSertifikat.P_MAX = 100;
                inputParSertifikat.P_MAXSpecified = true;
                inputParSertifikat.P_MIN = 0;
                inputParSertifikat.P_MINSpecified = true;
                inputParSertifikat.P_SORT = "";
                svcCallerSertifikat = new SvcJnsSertifikatSelect.call_pttClient(konfigApp.SvcJnsSertifikatSelect_name, konfigApp.SvcJnsSertifikatSelect_address);
                svcCallerSertifikat.Open();
                svcCallerSertifikat.Beginexecute(inputParSertifikat, new AsyncCallback(this.getDataSertifikat), null);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        public void getDataSertifikat(IAsyncResult result)
        {
            try
            {
                this.outParSertifikat = svcCallerSertifikat.Endexecute(result);
                svcCallerSertifikat.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataSertifikat(this.showDataSertifikat), this.outParSertifikat);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataSertifikat(SvcJnsSertifikatSelect.OutputParameters dataOut);

        public void showDataSertifikat(SvcJnsSertifikatSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_R_JNS_SERTI.Count();

            DataRow dtRow2;
            dataTable2.Rows.Clear();
            for (int i = 0; i < jmlDataGroup; i++)
            {
                //this.dataGrid.Add(serviceOutPut.SF_ROW_R_SMILIK[i]);
                dtRow2 = dataTable2.NewRow();
                dtRow2["KD_JNS_SERTI"] = serviceOutPut.SF_ROW_R_JNS_SERTI[i].KD_JNS_SERTI;
                dtRow2["NM_JNS_SERTI"] = serviceOutPut.SF_ROW_R_JNS_SERTI[i].NM_JNS_SERTI;
                dataTable2.Rows.Add(dtRow2);
            }
            dtRow2 = dataTable2.NewRow();
            dtRow2["KD_JNS_SERTI"] = "-";
            dtRow2["NM_JNS_SERTI"] = "-";
            dataTable2.Rows.Add(dtRow2);
            teKdJnsSrtf.Properties.DataSource = dataTable2;
            teKdJnsSrtf.Properties.DisplayMember = "KD_JNS_SERTI";
            teKdJnsSrtf.Properties.ValueMember = "KD_JNS_SERTI";
            teKdJnsSrtf.Properties.ShowHeader = false;
            teKdJnsSrtf.Properties.ShowFooter = false;

            this.init();
        }


        private void frmTnhDok_Load(object sender, EventArgs e)
        {
            this.getInitialDataDokMilik();
            string KdDok = Convert.ToString(selectedData.ID_KTNH_DOK);
            if (status == "edit")
            {
                this.teJnsDok.Enabled = false;
                this.sbJnsDok.Enabled = false;
                //try
                //{
                //    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                //    myThread.Start();
                //    inputParJns = new SvcAstJnsDok.InputParameters();
                //    inputParJns.P_COL = "";
                //    inputParJns.P_MAX = 1;
                //    inputParJns.P_MAXSpecified = true;
                //    inputParJns.P_MIN = 0;
                //    inputParJns.P_MINSpecified = true;
                //    inputParJns.P_SORT = "DESC";
                //    inputParJns.STR_WHERE = "KD_DOK=" + KdDok;
                //    svcCallerJns = new SvcAstJnsDok.call_pttClient();
                //    svcCallerJns.Beginexecute(inputParJns, new AsyncCallback(this.getDataJns), null);
                //}
                //catch
                //{

                //    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                //    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                //    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                //}
            }
            this.teFileName.Properties.ReadOnly = true;
          
        }

        private void teUrDok_EditValueChanged(object sender, EventArgs e)
        {
            //rowData = (SvcSmilikSelect.BPSIMANSROW_R_SMILIK)selectedView.GetRow(e.FocusedRowHandle);
            //teUrSmilik.Text = SvcSmilikSelect.BPSIMANSROW_R_SMILIK
            this.teKdSmilik.Text = teUrDok.GetColumnValue("KD_SMILIK").ToString(); 
            teUrSmilik.Text = teUrDok.GetColumnValue("UR_SMILIK").ToString();
            if (teUrDok.Text.ToUpper() == "SERTIFIKAT")
            {
                teKdJnsSrtf.Properties.ReadOnly = false;
                this.teNoSertifikat.Properties.ReadOnly = false;
                this.tePenerbit.Properties.ReadOnly = false;
                this.teAtasNama.Properties.ReadOnly = false;
                this.deTglBerlaku.Properties.ReadOnly = false;
                this.deTglDok.Properties.ReadOnly = false;
              
            }
           
            else
            {
                int idx = this.teKdJnsSrtf.Properties.GetDataSourceRowIndex("KD_JNS_SERTI", "-");
                this.teKdJnsSrtf.EditValue = this.teKdJnsSrtf.Properties.GetDataSourceValue("KD_JNS_SERTI", idx);
                teKdJnsSrtf.Properties.ReadOnly = true;
                this.teNoSertifikat.Text = "-";
                this.tePenerbit.Text = "-";
                this.teAtasNama.Text = "-";
                this.deTglBerlaku.Text = "";
                this.deTglDok.Text = "";
                this.teNoSertifikat.Properties.ReadOnly = true;
                this.tePenerbit.Properties.ReadOnly = true;
                if (teUrDok.Text.ToUpper() == "TIDAK ADA DOKUMEN KEPEMILIKAN")
                {
                    this.teAtasNama.Properties.ReadOnly = true;
                }
                else
                {
                    this.teAtasNama.Properties.ReadOnly = false;
                }
                
                this.deTglBerlaku.Properties.ReadOnly = true;
                this.deTglDok.Properties.ReadOnly = true;
              
            }
        }

        private void teKdJnsSrtf_EditValueChanged(object sender, EventArgs e)
        {

            this.teKdJnsSrtf.EditValueChanged -= new System.EventHandler(this.teKdJnsSrtf_EditValueChanged);
           // teKdJnsSrtf.Text = teKdJnsSrtf.GetColumnValue("KD_JNS_SERTI").ToString();
            teNmJnsSrtf.Text = teKdJnsSrtf.GetColumnValue("NM_JNS_SERTI").ToString();

            this.teKdJnsSrtf.EditValueChanged += new System.EventHandler(this.teKdJnsSrtf_EditValueChanged);
            this.teKdJnsSrtf.ClosePopup();
            if (this.teKdJnsSrtf.Text.Trim() == "SHGB" || this.teKdJnsSrtf.Text.Trim() == "SHP" || this.teKdJnsSrtf.Text.Trim() == "SHU")
            {
                this.deTglBerlaku.Properties.ReadOnly = false;
            }
            else
            {
                this.deTglBerlaku.Properties.ReadOnly = true;
                this.deTglBerlaku.Text = "";
            }
            if (this.teKdJnsSrtf.Text.Trim() == "SHP" && this.teUrDok.Text.ToUpper() == "SERTIFIKAT")
            {
                this.teAtasNama.Properties.ReadOnly = true;
            }
            else
            {
                this.teAtasNama.Properties.ReadOnly = false;
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

        #region init Data Jns Dok
        
        public void getDataJns(IAsyncResult result)
        {
            try
            {
                this.outParJns = svcCallerJns.Endexecute(result);
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataJns(this.showDataJns), this.outParJns);
            }
            catch
            {
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }
        private delegate void ShowDataJns(SvcAstJnsDok.OutputParameters dataOut);

        public void showDataJns(SvcAstJnsDok.OutputParameters serviceOutPut)
        {
            int jmlDataGroupJns = serviceOutPut.SF_ROW_R_JNS_DOK.Count();
            ID_JNSDOK = serviceOutPut.SF_ROW_R_JNS_DOK[0].KD_DOK;
            NM_JNSDOK = serviceOutPut.SF_ROW_R_JNS_DOK[0].NM_DOK;
            this.teJnsDok.Text = NM_JNSDOK;
            statusJns = "U";
        }
        #endregion
    }
}
