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
using System.Reflection;

using AppPengguna.PU;

namespace AppPengguna.AST.AK
{
    public partial class FormBpkbAngk : Form
    {
        private SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB selectedRow;
        private SvcBpkbAngkCrud.call_pttClient crudCaller;
        private SvcBpkbAngkCrud.InputParameters crudInput;
        private SvcBpkbAngkCrud.OutputParameters crudOut;
        private Thread myThread;
        private string operation;
        private char modeCrud;

        private bool edited = false;
        public bool Edited
        {
            get
            {
                return this.edited;
            }
            set
            {
                this.edited = value;
            }
        }
        public SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB SelectedRow
        {
            get
            {
                return selectedRow;
            }
        }


        public decimal? ID_KANGK;
        public string STATUS;
        public string ID_JNSDOK;
        public string path;
        string FilePath;

        private UcBpkbAngk ucBpkbAngk;
        public FormBpkbAngk(Char _modeCrud, decimal? _ID_KANGK, SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB _selectedRow, UcBpkbAngk _ucBpkbAngk)
        {
            InitializeComponent();
            this.ID_KANGK = _ID_KANGK;
            this.modeCrud = _modeCrud;
            this.ucBpkbAngk = _ucBpkbAngk;
            if (this.modeCrud == 'U') {
                this.selectedRow = _selectedRow;
                this.showDetailBpkb();
                this.operation = "U";
                this.STATUS = "edit";
            }
            else if (this.modeCrud == 'D') {
                this.operation = "D";
                this.STATUS = "hapus";
                this.selectedRow = _selectedRow;
                this.bbDetBpkbAngkSimpan_ItemClick(null, null);
            }
            else if (this.modeCrud == 'C') {
                this.operation = "C";
                this.STATUS = "input";
                this.selectedRow = _selectedRow;
            }
            else if (this.modeCrud == 'V')
            {
                this.selectedRow = _selectedRow;
                this.showDetailBpkb();
                this.operation = "V";
                this.STATUS = "detail";
                this.bbDetBpkbAngkSimpan.Enabled = false;

            }
            
        }
        
        private void showDetailBpkb()
        {
            teNoBpkb.Text = selectedRow.NO_BPKB;
            teNmFile.Text = selectedRow.NMFILE;
            deDariBpkbAngk.EditValue = Convert.ToDateTime(selectedRow.TGL_KELUAR);
            deSmpBpkbAngk.EditValue = Convert.ToDateTime(selectedRow.TGL_SD_BERLAKU);
            meKetBpkbAngk.Text = selectedRow.KET;
            this.teJnsDok.Enabled = false;
            this.sbJnsDok.Enabled = false;
        }

        private SvcBpkbAngkCrud.InputParameters parseParam(string _crudOperation)
        {
            crudInput = new SvcBpkbAngkCrud.InputParameters();
            string tglDari = konfigApp.DateToDb(deDariBpkbAngk.Text);
            string tglSampai = konfigApp.DateToDb(deSmpBpkbAngk.Text);
            if (_crudOperation == "U" || _crudOperation == "D")
            {

               crudInput.P_ID_KANGK_BPKB = selectedRow.ID_KANGK_BPKB;
                crudInput.P_ID_KANGK_BPKBSpecified = true;

            }
            string nmFileKib = teNmFile.Text;
            crudInput.P_ID_KANGK = this.ID_KANGK;
            crudInput.P_ID_KANGKSpecified = true;
            crudInput.P_NO_BPKB = (teNoBpkb.Text == "-" ? "" : teNoBpkb.Text);
            crudInput.P_TGL_KELUAR = tglDari;
            crudInput.P_TGL_SD_BERLAKU = tglSampai;
            crudInput.P_TERAKHIR_YN =  (_crudOperation == "C") ? "" : selectedRow.TERAKHIR_YN;
            crudInput.P_SELECT = _crudOperation;
            crudInput.P_NMFILE = nmFileKib;
            crudInput.P_KET = meKetBpkbAngk.Text;

            this.modeCrud = Convert.ToChar(_crudOperation);
            this.operation = _crudOperation;
            return crudInput;
        }
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

         public void crudOperation(string _crudOperation)
         {
             myThread = new Thread(new ThreadStart(ShowProgresBar));
             myThread.Start();

             crudCaller = new SvcBpkbAngkCrud.call_pttClient();
             crudCaller.Open();
             crudCaller.Beginexecute(parseParam(_crudOperation), new AsyncCallback(this.crudResult), "");
         }
         public void crudResult(IAsyncResult result)
         {
             try
             {
                 crudOut = crudCaller.Endexecute(result);
                 crudCaller.Close();
                 this.Invoke(new ProgBar(progBar), BarItemVisibility.Always);
                 
                  konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
                       

                 if (crudOut.PO_RESULT == "Y")
                 {
                     //MessageBox.Show(konfigApp.teksDialog, konfigApp.judulBerhasilAmbil);
                     this.Invoke(new UbahDsDetail(this.ubahDsDetail), crudOut);
                  
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
                 //this.Invoke(new AktifkanForm(konfigApp.aktifkanForm), "");
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
         public delegate void UbahDsDetail(SvcBpkbAngkCrud.OutputParameters outCrud);
         public decimal? ID_KANGK_BPKB;
         public void ubahDsDetail(SvcBpkbAngkCrud.OutputParameters outCrud)
         {
             SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB dataPenyama = new SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB();

             dataPenyama.NUM = 99;
             dataPenyama.ID_KANGK_BPKB = outCrud.PO_ID_KANGK_BPKB;
             this.ID_KANGK_BPKB = outCrud.PO_ID_KANGK_BPKB;

             switch (this.modeCrud)
             {
                 case 'C':

                     if (this.FilePath != null)
                     {
                         string Path = this.FilePath;

                         simpanFile("ID_KANGK_BPKB", dataPenyama.ID_KANGK_BPKB, "M_KANGK_BPKB", Path, "C", ID_JNSDOK);
                         this.ucBpkbAngk.search = "";
                         this.ucBpkbAngk.pencarian = false;
                         this.ucBpkbAngk.initGrid();
                         this.ucBpkbAngk.getInitBpkbAngk();
                     }
                     else
                     {
                         this.ucBpkbAngk.dataInisial = false;
                         this.ucBpkbAngk.getById = true;
                         this.ucBpkbAngk.getInitBpkbAngk(" ID_KANGK_BPKB = " + this.ID_KANGK_BPKB.ToString());

                         this.Close();
                     }
                     break;
                 case 'U':

                     ucBpkbAngk.binder.Remove(this.ucBpkbAngk.selectedData);
                     if (this.FilePath != null)
                     {
                       string Path = this.FilePath;
                       if (this.selectedRow.FILE_EXISTS != 0)
                       {
                         simpanFile("ID_KANGK_BPKB", dataPenyama.ID_KANGK_BPKB, "M_KANGK_BPKB", Path, "U", ID_JNSDOK);
                         this.ucBpkbAngk.search = "";
                         this.ucBpkbAngk.pencarian = false;
                         this.ucBpkbAngk.initGrid();
                         this.ucBpkbAngk.getInitBpkbAngk();
                       }
                       else
                       {
                         simpanFile("ID_KANGK_BPKB", dataPenyama.ID_KANGK_BPKB, "M_KANGK_BPKB", Path, "C", ID_JNSDOK);
                         this.ucBpkbAngk.search = "";
                         this.ucBpkbAngk.pencarian = false;
                         this.ucBpkbAngk.initGrid();
                         this.ucBpkbAngk.getInitBpkbAngk();
                         
                       }
                       
                     }
                     else
                     {
                       //this.ucBpkbAngk.dataInisial = false;
                       //this.ucBpkbAngk.getById = true;
                       //this.ucBpkbAngk.getInitBpkbAngk(" ID_KANGK_BPKB = " + this.ID_KANGK_BPKB.ToString());
                       this.ucBpkbAngk.search = "";
                       this.ucBpkbAngk.pencarian = false;
                       this.ucBpkbAngk.initGrid();
                       this.ucBpkbAngk.getInitBpkbAngk();
                     }
                         this.Close();
                     break;
                 case 'D':
                     ucBpkbAngk.binder.Remove(this.ucBpkbAngk.selectedData);
                     ucBpkbAngk.gvUcDtl.RefreshData();
                     ucBpkbAngk.StrTotalGrid.Caption = (Convert.ToInt64(ucBpkbAngk.StrTotalGrid.Caption) - 1).ToString();
                     ucBpkbAngk.StrTotalDb.Caption = (Convert.ToInt64(ucBpkbAngk.StrTotalDb.Caption) - 1).ToString();
  
                     this.Close();
                     break;
             }
         }
         private ThreadStart beMarqueeBar(BarItemVisibility barItemVisibility)
         {
             throw new NotImplementedException();
         }

        private void FormBpkbAngk_Load(object sender, EventArgs e)
        {
            this.teNmFile.Properties.ReadOnly = true;
        }

        private void bbDetBpkbAngkSimpan_ItemClick(object sender, ItemClickEventArgs e)
        {
            crudOperation(this.operation);
        }

        private void bbDetBpkbAngkRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            teNoBpkb.Text = "";
            deDariBpkbAngk.EditValue = "";
            deSmpBpkbAngk.EditValue = "";
            meKetBpkbAngk.Text = "";
            teJnsDok.Text = "";
        }

        private void bbDetBpkbTutup_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
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
                if (this.STATUS != "hapus")
                {
                    if (this.STATUS != "input")
                    {
                        this.ucBpkbAngk.dataInisial = false;
                    }
                    this.ucBpkbAngk.getById = true;
                    //this.ucKibAngk.getInitDokKib(" ID_KANGK_DOK_KIB = " + this.idKangkKib.ToString());

                }

                this.Close();
                this.ucBpkbAngk.search = "";
                this.ucBpkbAngk.pencarian = false;
                
                  this.ucBpkbAngk.initGrid();
                  this.ucBpkbAngk.getInitBpkbAngk();
               
            }

        }
        public void aktifkanForm(string str)
        {
            this.Enabled = true;

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
    }
}
