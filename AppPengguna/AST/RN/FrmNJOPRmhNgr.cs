using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
namespace AppPengguna.AST.RN
{


    public partial class FrmNJOPRmhNgr : Form
    {
        private SvcNJOPRmhNgrCrud.call_pttClient crudCaller = null;
        private SvcNJOPRmhNgrCrud.InputParameters crudInput;
        private SvcNJOPRmhNgrCrud.OutputParameters crudOut;
        private SvcNJOPRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_NJOP rowData;
        private Thread myThread;
        private char modeCrud;
        public string STATUS;
        public string FilePath;
        public decimal? ID_KRMH_NJOP;
        private UcNJOPRmhNgr ucNJOPRmhNgr;
        public decimal? NUM;
        public string ID_JNSDOK;
        public decimal? ID_KRMH_NEG;

        public FrmNJOPRmhNgr(UcNJOPRmhNgr _ucNJOPRmhNgr, string _operation)
        {
            this.ucNJOPRmhNgr = _ucNJOPRmhNgr;
            this.STATUS = _operation;
            //this.ID_KRMH_NJOP = ucNJOPRmhNgr.ID_KRMH_NEG;
            InitializeComponent();
            if (STATUS == "U")
            {
                rowData = ucNJOPRmhNgr.SelectedData;
                this.sbJnsDok.Enabled = false;
               // this.teJnsDok.Enabled = false;
                
            }
            else if (this.STATUS == "detail")
            {
                this.barSimpan.Enabled = false;
                this.btnUpload.Enabled = false;
                this.teJnsDok.Enabled = false;
            }
            
          
            showData();
            
        }

   

        private void showData()
        {
            
            if (this.STATUS == "input" || this.STATUS == "C")
            {
                seNilai.Value = 0;
                seMeter.Value = 0;
                seLuas.Value = 0;
                teTahun.Text = "";
                //teNPWP.Text = "";
                teNOP.Text = "";
                teKelas.Text = "";
            }
            else if(this.STATUS == "edit" || this.STATUS == "U")
            {
              seNilai.Value = (decimal)rowData.NJOP_NILAI;
              seMeter.Value = (decimal)rowData.NJOP_METER;
              seLuas.Value = (decimal)rowData.LUAS;
                
                teTahun.Text = rowData.TAHUN;
                //teNPWP.Text = rowData.NPWP;
                teNOP.Text = rowData.NOP;
                teKelas.Text = rowData.KELAS;
                teFileName.Text = rowData.NMFILE;
            }
            this.ShowProgresBarDelete();
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
          this.progBar(BarItemVisibility.Never);
        }
        public void aktifkanForm(string str)
        {
            this.Enabled = true;

        }
        #endregion

        public void crudOperation(string _crudOperation)
        {
            myThread = new Thread(new ThreadStart(ShowProgresBar));
            myThread.Start();

            crudCaller = new SvcNJOPRmhNgrCrud.call_pttClient(konfigApp.SvcNJOPRmhNgrCrud_name,konfigApp.SvcNJOPRmhNgrCrud_address);
            crudCaller.Open();
            crudCaller.Beginexecute(parseParam(_crudOperation), new AsyncCallback(this.crudResult), "");
        }

        public void crudResult(IAsyncResult result)
        {
            try
            {
                crudOut = crudCaller.Endexecute(result);
                crudCaller.Close();
                this.Invoke(new ProgBar(progBar), BarItemVisibility.Never);
                konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
             

                if (crudOut.PO_RESULT == "Y")
                {
                    //MessageBox.Show(konfigApp.teksDialog, konfigApp.judulBerhasilAmbil);
                    this.Invoke(new ShowDataCrud(this.showDataCrud), crudOut);
                }
                else
                {
                    MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
                }

                // this.Invoke(new AktifkanForm(konfigApp.aktifkanForm), "");
            }
            catch
            {

                this.Invoke(new ProgBar(progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
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

        private delegate void ShowDataCrud(SvcNJOPRmhNgrCrud.OutputParameters dataOut);

        private delegate void TutupPopUp(string data);

        public void tutupPopUp(string data)
        {
            this.Close();
        }
        public void showDataCrud(SvcNJOPRmhNgrCrud.OutputParameters dataOut)
        {
            SvcNJOPRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_NJOP dataPenyama = new SvcNJOPRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_NJOP();
            dataPenyama.ID_KRMH_NJOP = dataOut.PO_ID_KRMH_NJOP;

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            ID_KRMH_NJOP = dataOut.PO_ID_KRMH_NJOP;
            if (this.rowData != null)
            {
                this.NUM = rowData.NUM;
            }
            switch (this.modeCrud)
            {
                case 'C':
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.FilePath != null)
                    {
                        string filePath = this.FilePath;
                        simpanFile("ID_KRMH_NJOP", dataPenyama.ID_KRMH_NJOP, "M_KRMH_NEG_NJOP", filePath, "C", this.ID_JNSDOK);
                    }
                    else
                    {
                        
                        this.ucNJOPRmhNgr.dataInisial = false;
                        this.ucNJOPRmhNgr.getById = true;
                        this.ucNJOPRmhNgr.getInitKonsRmh(" ID_KRMH_NJOP = " + dataOut.PO_ID_KRMH_NJOP.ToString());
                        this.Close();
                    }
                    break;
                case 'U':
                    this.ucNJOPRmhNgr.binder.Remove(this.rowData);
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.FilePath != null)
                    {

                        string filePath = this.FilePath;
                        string SELECT = "C";
                        if (rowData.FILE_EXISTS != 0)
                        {
                            SELECT = "U";
                        }
                        simpanFile("ID_KRMH_NJOP", dataPenyama.ID_KRMH_NJOP, "M_KRMH_NEG_NJOP", filePath, SELECT, this.ID_JNSDOK);
                    }
                    else
                    {

                        this.ucNJOPRmhNgr.dataInisial = false;
                        this.ucNJOPRmhNgr.getById = true;
                        this.ucNJOPRmhNgr.getInitKonsRmh(" ID_KRMH_NJOP = " + dataOut.PO_ID_KRMH_NJOP.ToString());
                        this.Close();
                    }
                    break;
                case 'D':
                    this.ucNJOPRmhNgr.binder.Remove(this.rowData);
                    this.ucNJOPRmhNgr.gvUcDtl.RefreshData();
                    this.ucNJOPRmhNgr.StrTotalGrid.Caption = (Convert.ToInt64(this.ucNJOPRmhNgr.StrTotalGrid.Caption) - 1).ToString();
                    this.ucNJOPRmhNgr.StrTotalDb.Caption = (Convert.ToInt64(this.ucNJOPRmhNgr.StrTotalDb.Caption) - 1).ToString();
                    this.Close();
                    
                    this.ucNJOPRmhNgr.search = "";
                    this.ucNJOPRmhNgr.pencarian = false;
                    this.ucNJOPRmhNgr.initGrid();
                    this.ucNJOPRmhNgr.getInitKonsRmh();
                    break;
            }

        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string filePath, string SELECT, string id_jnsDok = null)
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
                inputData.P_ISI_FILE = konfigApp.FileToByteArray(filePath);
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
                if (this.modeCrud != 'D')
                {

                    this.ucNJOPRmhNgr.dataInisial = false;
                    this.ucNJOPRmhNgr.getById = true;
                    this.ucNJOPRmhNgr.getInitKonsRmh(" ID_KRMH_NJOP = " + this.ID_KRMH_NJOP.ToString());
                    this.Close();
                }
            }

        }

        private SvcNJOPRmhNgrCrud.InputParameters parseParam(string _crudOperation)
        {
            crudInput = new SvcNJOPRmhNgrCrud.InputParameters();

            if (_crudOperation == "U" || _crudOperation == "D")
            {
              crudInput.P_ID_KRMH_NJOP = ucNJOPRmhNgr.SelectedData.ID_KRMH_NJOP;
              crudInput.P_ID_KRMH_NJOPSpecified = true;
            }
           
            crudInput.P_ID_KRMH_NEG = ucNJOPRmhNgr.ID_KRMH_NEG;
            crudInput.P_ID_KRMH_NEGSpecified = true;
            
          

            crudInput.P_KELAS = (teKelas.Text == "-" ? null : teKelas.Text);
            crudInput.P_LUAS = ((decimal?)seLuas.Value == null ? null : (decimal?)seLuas.Value);
            crudInput.P_LUASSpecified = true;
            crudInput.P_NJOP_METER = ((decimal?)seMeter.Value == null ? null : (decimal?)seMeter.Value);
            crudInput.P_NJOP_METERSpecified = true;
            crudInput.P_NJOP_NILAI = ((decimal?)seNilai.Value == null ? null : (decimal?)seNilai.Value);
            crudInput.P_NJOP_NILAISpecified = true;
            crudInput.P_NOP = (teNOP.Text == "-" ? null : teNOP.Text);
           // crudInput.P_NPWP = (teNPWP.Text == "-" ? null : teNPWP.Text);
            crudInput.P_TAHUN = (teTahun.Text == "-" ? null : teTahun.Text);
            crudInput.P_NMFILE = teFileName.Text;
            

            crudInput.P_SELECT = _crudOperation;
            this.modeCrud = Convert.ToChar(_crudOperation);

            return crudInput;
        }

  

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            crudOperation(this.STATUS);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.showData();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
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


        public void HapusData() 
        {
          try
          {
            myThread = new Thread(new ThreadStart(ShowProgresBar));
            myThread.Start();

            SvcNJOPRmhNgrCrud.InputParameters parInp = new SvcNJOPRmhNgrCrud.InputParameters();
            parInp.P_ID_KRMH_NJOPSpecified = true;    
            parInp.P_ID_KRMH_NJOP = this.ID_KRMH_NJOP;
            parInp.P_SELECT = "D";

            this.modeCrud = Convert.ToChar(parInp.P_SELECT);
            crudCaller = new SvcNJOPRmhNgrCrud.call_pttClient();
            crudCaller.Open();
            crudCaller.Beginexecute(parInp, new AsyncCallback(crudResult), "");
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
