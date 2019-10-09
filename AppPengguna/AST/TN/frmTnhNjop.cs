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
using System.IO;
namespace AppPengguna.AST.TN
{
    internal partial class frmTnhNjop : Form
    {
        
        private ucTnhNjop uctnhnjop;

        public string status;
        public decimal? NUM;
        public string FilePath;
        private decimal? idKtnh;
        private decimal? idKtnhNjop;
        public string ID_JNSDOK;
        public string NM_JNSDOK;
        public string statusJns = null;

        public Thread myThread = null;
        public Thread myThread_ = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private SvcTnhNjopSelect.BPSIMANSROW_M_KTNH_NJOP selectedData;
        private SvcTnhNjopCrud.call_pttClient crudData;
        private SvcTnhNjopCrud.InputParameters inputCrud;
        private SvcTnhNjopCrud.OutputParameters outCrud;

        // ------------------------- jenis dok ------------------------
        private SvcAstJnsDok.call_pttClient svcCallerJns;
        private SvcAstJnsDok.InputParameters inputParJns;
        private SvcAstJnsDok.OutputParameters outParJns;
        private SvcAstJnsDok.BPSIMANSROW_R_JNS_DOK rowDataJns;

        // -------------------------dokumen---------------------------
        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        // -----------------------------------------------------------

        public frmTnhNjop( ucTnhNjop uctnhnjop, string status)
        {
            InitializeComponent();
        
            this.uctnhnjop = uctnhnjop;
            this.status = status;
            this.idKtnh = uctnhnjop.IdKtnh;
            this.selectedData = uctnhnjop.selectedData;
            this.teFileName.Properties.ReadOnly = true;
            this.teJnsDok.Properties.ReadOnly = true;
            if (uctnhnjop.selectedData != null)
            {
               this.NUM=  uctnhnjop.selectedData.NUM;
            }
            this.init();
            if (this.status == "detail")
            {
                this.bbiSave.Enabled = false;
                this.sbUpload.Enabled = false;
            }
            //string KdDok = Convert.ToString(selectedData.ID_M_KTNH_NJOP);
            
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
        }
        private void init()
        {
            if (this.status == "input")
            {
                this.gcTnhNjop.Text = "Input Data NJOP";
                this.idKtnhNjop = 0;
                this.teNOP.ResetText();
                //this.teNPWP.ResetText();
                this.deTahun.ResetText();
                this.seLuas.ResetText();
                this.teKelas.ResetText();
                this.seNjopMeter.ResetText();
                this.seNjopNilai.ResetText();
                this.ceTerakhir.Checked = false;
            }
            else if (this.status == "edit")
            {
                this.gcTnhNjop.Text = "Edit Data NJOP";
                this.idKtnhNjop = selectedData.ID_M_KTNH_NJOP;
                this.teNOP.Text = selectedData.NOP;
                //this.teNPWP.Text = selectedData.NPWP;
                this.deTahun.Text = selectedData.TAHUN;
                this.seLuas.Text = selectedData.LUAS.ToString();
                this.teKelas.Text = selectedData.KELAS;
                this.seNjopMeter.Text = selectedData.NJOP_METER.ToString();
                this.seNjopNilai.Text = selectedData.NJOP_NILAI.ToString();
                string terakhir = selectedData.TERAKHIR_YN;
                this.teFileName.Text = selectedData.NMFILE;
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
                this.gcTnhNjop.Text = "Detail Data NJOP";
                this.idKtnhNjop = selectedData.ID_M_KTNH_NJOP;
                this.teNOP.Text = selectedData.NOP;
                //this.teNPWP.Text = selectedData.NPWP;
                this.deTahun.Text = selectedData.TAHUN;
                this.seLuas.Text = selectedData.LUAS.ToString();
                this.teKelas.Text = selectedData.KELAS;
                this.seNjopMeter.Text = selectedData.NJOP_METER.ToString();
                this.seNjopNilai.Text = selectedData.NJOP_NILAI.ToString();
                string terakhir = selectedData.TERAKHIR_YN;
                if (terakhir == "Y")
                {
                    this.ceTerakhir.Checked = true;
                }
                else
                {
                    this.ceTerakhir.Checked = false;
                }
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
        public void aktifkanForm(string str)
        {
            this.Enabled = true;

        }
        #endregion

        public byte[] convert2byte(string file)
        {
            FileStream fs = new FileStream(file,
                                           FileMode.Open,
                                           FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return filebytes;
        }

        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string nop = teNOP.Text;
                //string npwp = teNPWP.Text;
                string tahun = deTahun.Text;
                string sluas = seLuas.Text;
                float luas;
                float.TryParse(sluas, out luas);
                string kelas = teKelas.Text;

                string sNjopMeter = seNjopMeter.Text;
                decimal njopMeter;
                decimal.TryParse(sNjopMeter, out njopMeter);

                string sNjopNilai = seNjopNilai.Text;
                decimal njopNilai;
                decimal.TryParse(sNjopNilai, out njopNilai);
                string nmFile =  teFileName.Text;
                string file = teFilePath.Text;

                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcTnhNjopCrud.InputParameters parInp = new SvcTnhNjopCrud.InputParameters();
                parInp.P_ID_KTNHSpecified = true;
                parInp.P_ID_M_KTNH_NJOPSpecified = true;
                parInp.P_LUASSpecified = true;
                parInp.P_NJOP_METERSpecified = true;
                parInp.P_NJOP_NILAISpecified = true;
               
                parInp.P_ID_KTNH = idKtnh;
                parInp.P_ID_M_KTNH_NJOP = idKtnhNjop;
                parInp.P_NOP = nop;
                //parInp.P_NPWP = npwp;
                parInp.P_TAHUN = tahun;
                parInp.P_LUAS = luas;
                parInp.P_KELAS = kelas;
                parInp.P_NJOP_METER = njopMeter;
                parInp.P_NJOP_NILAI = njopNilai;

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
                this.crudData = new SvcTnhNjopCrud.call_pttClient(konfigApp.SvcTnhNjopCrud_name, konfigApp.SvcTnhNjopCrud_address);
                crudData.Open();
                this.crudData.Beginexecute(parInp, new AsyncCallback(crudNjopTnh), "");
            }
            catch
            {
                this.modeCrud = 'A';
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void bbiReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.init();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
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

                    SvcTnhNjopCrud.InputParameters parInp = new SvcTnhNjopCrud.InputParameters();

                    parInp.P_ID_M_KTNH_NJOPSpecified = true;
                    parInp.P_ID_M_KTNH_NJOP = selectedData.ID_M_KTNH_NJOP;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    this.crudData = new SvcTnhNjopCrud.call_pttClient(konfigApp.SvcTnhNjopCrud_name, konfigApp.SvcTnhNjopCrud_address);
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudNjopTnh), "");
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
        public void crudNjopTnh(IAsyncResult result)
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

        public delegate void UbahDsDetail(SvcTnhNjopCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcTnhNjopCrud.OutputParameters outCrud)
        {


            SvcTnhNjopSelect.BPSIMANSROW_M_KTNH_NJOP dataPenyama = new SvcTnhNjopSelect.BPSIMANSROW_M_KTNH_NJOP();
            this.idKtnhNjop = (this.status == "edit") ? selectedData.ID_M_KTNH_NJOP : outCrud.PO_ID_M_KTNH_NJOP;
            dataPenyama.NUM = 99;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_KTNH = idKtnh;
            dataPenyama.ID_M_KTNH_NJOP = (this.status == "edit")?  selectedData.ID_M_KTNH_NJOP : outCrud.PO_ID_M_KTNH_NJOP ;
            dataPenyama.NOP = teNOP.Text;
            //dataPenyama.NPWP = teNPWP.Text;
            dataPenyama.TAHUN = deTahun.Text;
            dataPenyama.LUAS = float.Parse(seLuas.Text);
            dataPenyama.KELAS = teKelas.Text;
            dataPenyama.NJOP_METER = seNjopMeter.Value;
            dataPenyama.NJOP_NILAI = seNjopNilai.Value;
            string terakhir;
            if (ceTerakhir.Checked)
            {
                terakhir = "Y";
            }
            else
            {
                terakhir = "N";
            }
            dataPenyama.TERAKHIR_YN = terakhir;

            switch (this.modeCrud)
            {
                case 'C':
                  
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        simpanFile("ID_M_KTNH_NJOP", dataPenyama.ID_M_KTNH_NJOP, "M_KTNH_NJOP", Path, "C", ID_JNSDOK);
                        this.Close();
                        
                    }
                    else
                    {
                        this.uctnhnjop.dataInisial = false;
                        this.uctnhnjop.getById = true;
                        this.uctnhnjop.getInitTnhNjop(" ID_M_KTNH_NJOP = " + outCrud.PO_ID_M_KTNH_NJOP.ToString());
                    
                        this.Close();
                    }
                    break;
                case 'U':
                   
                     uctnhnjop.binder.Remove(this.selectedData);
                     if (this.FilePath != null && this.statusJns != "U")
                    {
                        string Path = this.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                            simpanFile("ID_M_KTNH_NJOP", dataPenyama.ID_M_KTNH_NJOP, "M_KTNH_NJOP", Path, "U", ID_JNSDOK);
                            
                        }
                        else
                        {
                            simpanFile("ID_M_KTNH_NJOP", dataPenyama.ID_M_KTNH_NJOP, "M_KTNH_NJOP", Path, "C", ID_JNSDOK);
                            
                        }
                    }
                     else if (this.statusJns == "U")
                    {
                        string Path = "";
                        simpanFile("ID_M_KTNH_NJOP", dataPenyama.ID_M_KTNH_NJOP, "M_KTNH_NJOP", Path, "U", ID_JNSDOK);
                        this.Close();
                    }
                    else
                    {
                        this.uctnhnjop.dataInisial = false;
                        this.uctnhnjop.getById = true;
                        //this.uctnhnjop.getInitTnhNjop(" ID_M_KTNH_NJOP = " + outCrud.PO_ID_M_KTNH_NJOP.ToString());
                       
                        this.Close();
                        this.uctnhnjop.search = "";
                        this.uctnhnjop.pencarian = false;
                        this.uctnhnjop.initGrid();
                        this.uctnhnjop.getInitTnhNjop();
                    }
                     
                      

                    break;
                case 'D':
                    uctnhnjop.binder.Remove(this.selectedData);
                    uctnhnjop.gvUcDtl.RefreshData();
                    uctnhnjop.StrTotalGrid.Caption = (Convert.ToInt64(uctnhnjop.StrTotalGrid.Caption) - 1).ToString();
                    uctnhnjop.StrTotalDb.Caption = (Convert.ToInt64(uctnhnjop.StrTotalDb.Caption) - 1).ToString();
                    this.Close();
                    this.uctnhnjop.search = "";
                    this.uctnhnjop.pencarian = false;
                    this.uctnhnjop.initGrid();
                    this.uctnhnjop.getInitTnhNjop();
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
                    this.uctnhnjop.dataInisial = false;
                    this.uctnhnjop.getById = true;
                    this.uctnhnjop.getInitTnhNjop(" ID_M_KTNH_NJOP = " + this.idKtnhNjop.ToString());
                }

                this.Close();
                this.uctnhnjop.search = "";
                this.uctnhnjop.pencarian = false;
                this.uctnhnjop.initGrid();
                this.uctnhnjop.getInitTnhNjop();
              
            }

        }
        #endregion

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
                        FilePath = filePath;
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
            statusJns = "U";
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
        }
        #endregion

    }
}
