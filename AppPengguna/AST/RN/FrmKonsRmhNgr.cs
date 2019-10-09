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
using AppPengguna.PU;

namespace AppPengguna.AST.RN
{


    public partial class FrmKonsRmhNgr : Form
    {
        private SvcKonsRmhNgrCrud.call_pttClient crudCaller;
        private SvcKonsRmhNgrCrud.InputParameters crudInput;
        private SvcKonsRmhNgrCrud.OutputParameters crudOut;
        private SvcKonsRmhNgrSelect.BPSIMANSROW_KRMH_NEG_KONS_BDG rowData;

        private SvcKondisiSelect.call_pttClient svcCaller;
        private SvcKondisiSelect.InputParameters inputPar;
        private SvcKondisiSelect.OutputParameters outPar;
        private SvcKondisiSelect.BPSIMANSROW_R_KONDISI SelectData;


        private Thread myThread;
        private char modeCrud;
        public string STATUS;
        public string FilePath;
        public string ID_JNSDOK;
        private string KD_KONDISI;
        private UcKonsRmhNgr ucKonsRmhNgr;
        public decimal? ID_M_KBDG_KONS_BDG;
        public FrmKonsRmhNgr(UcKonsRmhNgr _ucKonsRmhNgr, string _operation)
        {
            this.ucKonsRmhNgr = _ucKonsRmhNgr;
            this.STATUS = _operation;
            InitializeComponent();
            if (STATUS == "U")
            {
              this.sbJnsDok.Enabled = false;
                rowData = ucKonsRmhNgr.SelectedData;
                showData();
            }
            else if (this.STATUS == "detail")
            {
              this.barSimpan.Enabled = false;
              this.sbFile.Enabled = false;
              //this.sbKondisi.Enabled = false;
            }
            


        }


        public void aktifkanForm(string str)
        {
            this.Enabled = true;

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
                this.beMarqueeBar.Visibility = str;
            }

        }

        public void ShowProgresBar()
        {
            this.progBar(BarItemVisibility.Always);
        }


        public void getinitdatakondisi() 
        {
          try
          {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            inputPar = new SvcKondisiSelect.InputParameters();
            inputPar.P_COL = "";
            inputPar.P_MAX = 100;
            inputPar.P_MAXSpecified = true;
            inputPar.P_MIN = 0;
            inputPar.P_MINSpecified = true;
            inputPar.P_SORT = "DESC";
            svcCaller = new SvcKondisiSelect.call_pttClient();
            svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getdatakondisi), null);
          }
          catch
          {
            this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
            this.Invoke(new AktifkanForm(this.aktifkanForm), "");
            MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
          }
        }

        public void getdatakondisi(IAsyncResult result) 
        {
          try 
          {
            outPar = svcCaller.Endexecute(result);
            svcCaller.Close();
            this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
            this.Invoke(new AktifkanForm(this.aktifkanForm), "");
            this.Invoke(new ShowDataKondisi(this.showDataKondisi), this.outPar);
          }
          catch 
          {
            this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
            this.Invoke(new AktifkanForm(this.aktifkanForm), "");
            MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
          }
        }

        private delegate void ShowDataKondisi(SvcKondisiSelect.OutputParameters dataOutPut);

        public void showDataKondisi(SvcKondisiSelect.OutputParameters svcOutput) 
        {
          int jmlData = svcOutput.SF_ROW_R_KONDISI.Count();
          
          DataRow dtRow;
          for (int i = 0; i < jmlData; i++ )
          {
            dtRow = dataTable1.NewRow();
            dtRow["KD_KONDISI"] = svcOutput.SF_ROW_R_KONDISI[i].KD_KONDISI;
            dtRow["UR_KONDISI"] = svcOutput.SF_ROW_R_KONDISI[i].UR_KONDISI;
            dataTable1.Rows.Add(dtRow);

          }

          teKondisi.Properties.DataSource = dataTable1;
          teKondisi.Properties.DisplayMember = "UR_KONDISI";
          teKondisi.Properties.ValueMember = "KD_KONDISI";
          teKondisi.Properties.ShowHeader = false;
          teKondisi.Properties.ShowFooter = false;
        }

        public void crudOperation(string _crudOperation)
        {
            myThread = new Thread(new ThreadStart(ShowProgresBar));
            myThread.Start();

            crudCaller = new SvcKonsRmhNgrCrud.call_pttClient(konfigApp.SvcKonsRmhNgrCrud_name,konfigApp.SvcKonsRmhNgrCrud_address);
            crudCaller.Open();
            crudCaller.Beginexecute(parseParam(_crudOperation), new AsyncCallback(this.crudResult), "");
        }

        private string kdKondisi;
        private void showData()
        {

          
            kdKondisi = rowData.KD_KONDISI;
            teStrLangit.Text = rowData.STR_RANGKA;
            teStrAtap.Text = rowData.STR_ATAP;
            tePrkrsn.Text = rowData.PERKERASAN;
            tePagar.Text = rowData.PAGAR;
            teLantai.Text = rowData.LANTAI;
            teLangit.Text = rowData.MATERIAL_LANGIT;
            //teKondisi.Text = rowData.UR_KONDISI;
            teDndLuar.Text = rowData.PELAPIS_DINDIN_LR;
            teDndDlm.Text = rowData.PELAPIS_DINDIN_DLM;
            teAtap.Text = rowData.MATERIAL_ATAP;
            deInv.Text = konfigApp.DateToString(rowData.TGL_INV);
            this.teFileName.Text = rowData.NMFILE;
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
                    MessageBox.Show(konfigApp.teksDialog, konfigApp.judulBerhasilAmbil);
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

        public delegate void UbahDsDetail(SvcKonsRmhNgrCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcKonsRmhNgrCrud.OutputParameters outCrud)
        {
            SvcKonsRmhNgrSelect.BPSIMANSROW_KRMH_NEG_KONS_BDG dataPenyama = new SvcKonsRmhNgrSelect.BPSIMANSROW_KRMH_NEG_KONS_BDG();

            dataPenyama.NUM = 99;
            dataPenyama.ID_M_KBDG_KONS_BDG = outCrud.PO_ID_M_KBDG_KONS_BDG;
            this.ID_M_KBDG_KONS_BDG = outCrud.PO_ID_M_KBDG_KONS_BDG;
            if (this.rowData != null)
            {
                ucKonsRmhNgr.NUM = rowData.NUM;
            }
            switch (this.modeCrud)
            {
                case 'C':


                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                        simpanFile("ID_M_KBDG_KONS_BDG", dataPenyama.ID_M_KBDG_KONS_BDG, "M_KRMH_NEG_KONS_BDG", Path, "C", this.ID_JNSDOK);
                    }
                    else
                    {
                        this.ucKonsRmhNgr.dataInisial = false;
                        this.ucKonsRmhNgr.getById = true;
                        this.ucKonsRmhNgr.getInitKonsRmh(" ID_M_KBDG_KONS_BDG = " + this.ID_M_KBDG_KONS_BDG.ToString());
                
                        this.Close();
                    }
                    break;
                case 'U':
                    ucKonsRmhNgr.binder.Remove(this.rowData);
                    if (this.FilePath != null)
                    {
                        string Path = this.FilePath;
                          if (rowData.FILE_EXISTS != 0)
                            {
                              simpanFile("ID_M_KBDG_KONS_BDG", dataPenyama.ID_M_KBDG_KONS_BDG, "M_KRMH_NEG_KONS_BDG", Path, "U", this.ID_JNSDOK);
                            }
                            else
                            {
                              simpanFile("ID_M_KBDG_KONS_BDG", dataPenyama.ID_M_KBDG_KONS_BDG, "M_KRMH_NEG_KONS_BDG", Path, "C", this.ID_JNSDOK);
                            }
                    }
                    else
                    {
                        this.ucKonsRmhNgr.dataInisial = false;
                        this.ucKonsRmhNgr.getById = true;
                        this.ucKonsRmhNgr.getInitKonsRmh(" ID_M_KBDG_KONS_BDG = " + this.ID_M_KBDG_KONS_BDG.ToString());
                 
                        this.Close();
                    }
                    break;
                case 'D':
                    ucKonsRmhNgr.binder.Remove(rowData);
                    ucKonsRmhNgr.gvUcDtl.RefreshData();
                    this.ucKonsRmhNgr.search = "";
                    this.ucKonsRmhNgr.initGrid();
                    this.ucKonsRmhNgr.getInitKonsRmh();
                    ucKonsRmhNgr.StrTotalGrid.Caption = (Convert.ToInt64(ucKonsRmhNgr.StrTotalGrid.Caption) - 1).ToString();
                    ucKonsRmhNgr.StrTotalDb.Caption = (Convert.ToInt64(ucKonsRmhNgr.StrTotalDb.Caption) - 1).ToString();
                     this.Close();
                    break;
            }
        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string FilePath, string SELECT , string id_jnsDok = null)
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
              if(id_jnsDok != null)
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
                    this.ucKonsRmhNgr.dataInisial = false;
                    this.ucKonsRmhNgr.getById = true;
                    this.ucKonsRmhNgr.getInitKonsRmh(" ID_M_KBDG_KONS_BDG = " + this.ID_M_KBDG_KONS_BDG.ToString());
                }

                this.Close();
            }

        }

        private SvcKonsRmhNgrCrud.InputParameters parseParam(string _crudOperation)
        {
            crudInput = new SvcKonsRmhNgrCrud.InputParameters();

            if (_crudOperation == "U" || _crudOperation == "D")
            {
                crudInput.P_ID_M_KBDG_KONS_BDG = ucKonsRmhNgr.SelectedData.ID_M_KBDG_KONS_BDG;
                crudInput.P_ID_M_KBDG_KONS_BDGSpecified = true;
            }

            crudInput.P_ID_KRMH_NEG = ucKonsRmhNgr.ID_KRMH_NEG;
            crudInput.P_ID_KRMH_NEGSpecified = true;

            crudInput.P_KD_KONDISI = (kdKondisi == "-" ? null : kdKondisi);
            crudInput.P_LANTAI = (teLantai.Text == "-" ? null : teLantai.Text);
            crudInput.P_MATERIAL_ATAP = (teAtap.Text == "-" ? null : teAtap.Text);
            crudInput.P_MATERIAL_LANGIT = (teLangit.Text == "-" ? null : teLangit.Text);
            crudInput.P_PAGAR = (tePagar.Text == "-" ? null : tePagar.Text);
            crudInput.P_PELAPIS_DINDIN_DLM = (teDndDlm.Text == "-" ? null : teDndDlm.Text);
            crudInput.P_PELAPIS_DINDIN_LR = (teDndLuar.Text == "-" ? null : teDndLuar.Text);
            crudInput.P_PERKERASAN = (tePrkrsn.Text == "-" ? null : tePrkrsn.Text);
            crudInput.P_STR_ATAP = (teStrAtap.Text == "-" ? null : teStrAtap.Text);
            crudInput.P_STR_RANGKA = (teStrLangit.Text == "-" ? null : teStrLangit.Text);

            crudInput.P_NMFILE = this.teFileName.Text;
            crudInput.P_TGL_INV = deInv.Text;
            //MessageBox.Show(deInv.Text);

            crudInput.P_SELECT = _crudOperation;
            this.modeCrud = Convert.ToChar(_crudOperation);

            return crudInput;
        }

        private void bbSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbTutup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            crudOperation(this.STATUS);
           // parseParam("H");
        }

        public void clearText()
        {
            teStrLangit.Text = "";
            teStrAtap.Text = "";
            tePrkrsn.Text = "";
            tePagar.Text = "";
            teLantai.Text = "";
            teLangit.Text = "";
            teKondisi.Text = "";
            teDndLuar.Text = "";
            teDndDlm.Text = "";
            teAtap.Text = "";
            deInv.Text = "";
            this.teFileName.Text = "";
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            clearText();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void sbFile_Click(object sender, EventArgs e)
        {
            try{
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
                        FilePath = dialog.FileName;
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

        private FrmPUKondisi puKondisi;
        private void sbKondisi_Click(object sender, EventArgs e)
        {
            puKondisi = new FrmPUKondisi();
            puKondisi.ambilKondisi = new AmbilKondisi(ambilKondisi);
            puKondisi.ShowDialog();
        }

        private void ambilKondisi(string kd, string vals)
        {
            kdKondisi = kd;
            teKondisi.Text = vals;
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

        private void FrmKonsRmhNgr_Load(object sender, EventArgs e)
        {
          this.getinitdatakondisi();
          if (STATUS == "U")
          {
            if (rowData.KD_KONDISI != "-")
            {
              this.teKondisi.EditValue = rowData.KD_KONDISI;
            }
          }
        }

        private void teKondisi_EditValueChanged(object sender, EventArgs e)
        {
          this.kdKondisi = teKondisi.GetColumnValue("KD_KONDISI").ToString();
          teKondisi.Text = teKondisi.GetColumnValue("UR_KONDISI").ToString();
        }
    }
}
