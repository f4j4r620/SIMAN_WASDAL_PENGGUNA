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

namespace AppPengguna.AST.PUASET
{
    public delegate void ResetFrmDok(string text);
    public delegate void SaveFrmDok(string text);
    public partial class frmDok : Form
    {

        public string FilePath;
        public string Status;
        public ResetFrmNilai resetForm;
        public SaveFrmNilai saveForm;
        private PU.FrmPUSmilik popUpSmilik;
        private PU.FrmPUJnsSertifikat popUpSertifikat;
        public string KD_SMILIK;
        public string KD_JNS_SERTI;
        
        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        

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

        public frmDok(string Status)
        {
            InitializeComponent();
            this.Status = Status;
            
  
        }
        public void aktifkanForm(string str)
        {
            this.Enabled = true;
            
        }
        public void init()
        {
           
            if (this.Status == "input")
            {
                this.gcDokTnh.Text = "Input Data Dokumen";
                teUrSmilik.ResetText();
                teUrDok.ResetText();
                teKdJnsSrtf.ResetText();
                teNoSertifikat.ResetText();
                deTglDok.ResetText();
                deTglBerlaku.ResetText();
                teAtasNama.ResetText();
                tePenerbit.ResetText();
                meKetDok.ResetText();
            }
            else if (this.Status == "edit" || this.Status == "detail")
            {
                try
                {
                    this.gcDokTnh.Text = "Edit Data Dokumen";
                 
                  
                    try
                    {
                        if (this.KD_SMILIK != "-")
                        {
                           
                           // this.teUrDok.Properties.ValueMember = this.KD_SMILIK;
                            int idx = this.teUrDok.Properties.GetDataSourceRowIndex("KD_SMILIK", this.KD_SMILIK);
                            this.teUrDok.EditValue = this.teUrDok.Properties.GetDataSourceValue("KD_SMILIK",idx);
                            idx = this.teUrDok.Properties.GetDataSourceRowIndex("KD_SMILIK", this.KD_SMILIK);
                        }
                        
                        
                    }
                    catch (Exception)
                    {
                        
                    }
                    try
                    {
                       
                        if (this.KD_JNS_SERTI != "-")
                        {
                            int idx = this.teKdJnsSrtf.Properties.GetDataSourceRowIndex("KD_JNS_SERTI", this.KD_JNS_SERTI);
                            this.teKdJnsSrtf.EditValue = this.teKdJnsSrtf.Properties.GetDataSourceValue("KD_JNS_SERTI", idx);
                            
                        }
                      
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                    
                }
                catch (Exception)
                {

                    MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                }
                
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
           
        }




        
        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            
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
                        teFileName.Text = fileName;
                        teFilePath.Text = filePath;
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


        private void frmDok_Load(object sender, EventArgs e)
        {
            this.getInitialDataDokMilik();
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
                this.deTglBerlaku.Text = "01/01/0001";
                this.deTglDok.Text = "01/01/0001";
                this.teNoSertifikat.Properties.ReadOnly = true;
                this.tePenerbit.Properties.ReadOnly = true;
                this.teAtasNama.Properties.ReadOnly = true;
                this.deTglBerlaku.Properties.ReadOnly = true;
                this.deTglDok.Properties.ReadOnly = true;
              
            }
        }

        private void teKdJnsSrtf_EditValueChanged(object sender, EventArgs e)
        {

            this.teKdJnsSrtf.EditValueChanged -= new System.EventHandler(this.teKdJnsSrtf_EditValueChanged);
            teKdJnsSrtf.Text = teKdJnsSrtf.GetColumnValue("KD_JNS_SERTI").ToString();
            teNmJnsSrtf.Text = teKdJnsSrtf.GetColumnValue("NM_JNS_SERTI").ToString();

            this.teKdJnsSrtf.EditValueChanged += new System.EventHandler(this.teKdJnsSrtf_EditValueChanged);
        }

    



    }
}
