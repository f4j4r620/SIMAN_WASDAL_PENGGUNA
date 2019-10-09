using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;

namespace AppPengguna.AST.BG
{
    class ucRiwayatBangunan : UserControlDetail
    {
        private SvcRiwayatBangunanSelect.call_pttClient fetchData;
        private SvcRiwayatBangunanSelect.InputParameters parInp;
        private SvcRiwayatBangunanSelect.OutputParameters outDat;
        private SvcRiwayatBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_BDG selectedData;
        //public NonAktifkanFormSatker nonAktifForm;
        //public AktifkanFormSatker aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        SvcRiwayatBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_BDG newRow;
        private ColumnView View;

        private decimal? ID_KBDG;
        private decimal? ID_M_KBDG_RWYT_BDG;
        public decimal? NUM;
        private string StatusCrud = "";
        private string Status;
        private PuRiwayatBangunan PU;
        private string coba;
        //child form customization 
        private void initRiwayatBangunan()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcRiwayatBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_BDG);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
          
          
            this.setKolom("Nomor", "NUM", "NUM", 0);
           // this.setKolom("ID Riwayat Bangunan", "ID_M_KBDG_RWYT_BDG", "ID_M_KBDG_RWYT_BDG", 1);
           // this.setKolom("ID Bangunan", "ID_KBDG", "ID_KBDG", 2);
            this.setKolom("Riwayat Bangunan", "RWYT_BDG", "RWYT_BDG", 3,true);
            this.setKolom("No Kontrak", "NO_KONTRAK", "NO_KONTRAK", 4,true);
            this.setKolom("Tgl Kontrak", "TGL_KONTRAK", "TGL_KONTRAK", 5,true,110,"date");
            this.setKolom("Nilai Kontrak", "NILAI_KONTRAK", "NILAI_KONTRAK", 6,true,120,"number");
            this.setKolom("Nama Kontraktor", "NM_KONTRAKTOR", "NM_KONTRAKTOR", 7,true);
            this.setKolom("NPWP Kontraktor", "NPWP_KONTRAKTOR", "NPWP_KONTRAKTOR", 8, true);
            this.setKolom("Alamat Kontraktor", "ALAMAT_KONTRAKTOR", "ALAMAT_KONTRAKTOR", 9,true,170);
            this.setKolom("Tgl Mulai", "TGL_MULAI", "TGL_MULAI", 10, true, 110, "date");
            this.setKolom("Tgl Selesai", "TGL_SELESAI", "TGL_SELESAI", 11, true, 110, "date");
             this.setKolom("Tgl Digunakan", "TGL_PAKAI", "TGL_PAKAI", 12, true, 110, "date");
             this.setKolom("File (Dok)", "NMFILE", "NMFILE", 13, true, 110, "string");
            this.gridDoubleClickDetail = true;
            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);
            this.show_record = true;
        }

        #region View Dokumen
        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------
        protected void view_dokumen(object sender, ItemClickEventArgs e)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = selectedData.ID_M_KBDG_RWYT_BDG;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_M_KBDG_RWYT_BDG";
                parInp.P_TABLE = "M_KBDG_RWYT_BDG";

                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient(konfigApp.SvcAsetGetDokSelect_name, konfigApp.SvcAsetGetDokSelect_address);
                svcAsetGetDokSelect.Open();
                svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getResultDok(IAsyncResult result)
        {
            try
            {
                this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
                svcAsetGetDokSelect.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowFileDok(this.showFileDok), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowFileDok(SvcAsetGetDokSelect.OutputParameters dataOut);

        public void showFileDok(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlData > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
                System.IO.File.WriteAllBytes(selectedData.NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(selectedData.NMFILE);
                PuPdf.ShowDialog();
            }
        }


        #endregion//ViewDokumen

  

        //bila di pasang pada detail Master
        public ucRiwayatBangunan(decimal? id_kbdg, string _status)
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KBDG = id_kbdg;
            this.Status = _status;
            initRiwayatBangunan();

            if (this.Status == "detail")
            {
                this.bbTambah.Enabled = false;
                this.bbHapus.Enabled = false;
                this.bbEdit.Caption = "Detail";
                this.bbEdit.Glyph = global::AppPengguna.Properties.Resources.tombol_detail16;
            }
            else
            {
                this.bbTambah.Enabled = true;
                this.bbHapus.Enabled = true;
            }

            this.bbEdit.Enabled = true;
            this.bbRefresh.Enabled = true;
            this.bbMore.Enabled = true;
        }


        protected override void ucDetail_Load(object sender, EventArgs e)
        {
           
            this.initGrid();
            this.getInitRnilaiBangunan();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcRiwayatBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_BDG)selectedView.GetRow(e.RowHandle);

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PU = new PuRiwayatBangunan("input");
            this.StatusCrud = "input";
            this.PU.resetForm = new ResetFrmRiwayatBangunan(this.resetForm);
            this.PU.saveForm = new SaveFrmRiwayatBangunan(this.saveForm);
           
            PU.Enabled = true;

            PU.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    PU = new PuRiwayatBangunan("detail");
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new PuRiwayatBangunan("edit");
                    this.StatusCrud = "edit";
                }
                this.PU.resetForm = new ResetFrmRiwayatBangunan(this.resetForm);
                this.PU.saveForm = new SaveFrmRiwayatBangunan(this.saveForm);
               
                this.resetForm("reset");
                PU.Enabled = true;

                PU.ShowDialog();
            }
        }
        private void resetForm(string text)
        {
            this.PU.teAlamatKontraktor.Text = selectedData.ALAMAT_KONTRAKTOR;
            this.PU.teFileName.Text = selectedData.NMFILE;


            this.PU.teNamaKontraktor.Text = selectedData.NM_KONTRAKTOR;
            this.PU.teNoKontrak.Text = selectedData.NO_KONTRAK;
            this.PU.teNilaiKontrak.Value = (decimal)selectedData.NILAI_KONTRAK;
            this.PU.teNPWPKontraktor.Text = selectedData.NPWP_KONTRAKTOR;
            this.PU.teRiwayatBangunan.Text = selectedData.RWYT_BDG;
          
            this.PU.teTglKontrak.Text = konfigApp.DateToString(selectedData.TGL_KONTRAK);
            this.PU.teTglMulaiKontrak.Text = konfigApp.DateToString(selectedData.TGL_MULAI);
            this.PU.teTglSelesai.Text = konfigApp.DateToString(selectedData.TGL_SELESAI);
            this.PU.teTglPakai.Text = konfigApp.DateToString(selectedData.TGL_PAKAI);
 


        }

        SvcBangunanRiwayatCrud.call_pttClient svcRiwayatPengelolaCrud;
        SvcBangunanRiwayatCrud.OutputParameters outRiwayatPengelolaCrud;

        private void saveForm(string text)
        {
            this.PU.nonAktifkanForm("nonaktif");
            try
            {
                if (this.modeCrud != 'D')
                {
                    myThread = new Thread(new ThreadStart(this.PU.ShowProgresBar));
                    myThread.Start();
                    this.PU.nonAktifkanForm("");
                }
                else
                {
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    this.nonAktifForm("");
                }


                SvcBangunanRiwayatCrud.InputParameters parInp = new SvcBangunanRiwayatCrud.InputParameters();
                parInp.P_ID_KBDG = this.ID_KBDG;
                parInp.P_ALAMAT_KONTRAKTOR = Convert.ToString(selectedData.ALAMAT_KONTRAKTOR);
                parInp.P_ID_KBDGSpecified = true;
                parInp.P_ID_M_KBDG_RWYT_BDG = (this.PU.Status == "input") ? 0 : this.selectedData.ID_M_KBDG_RWYT_BDG;
                parInp.P_ID_M_KBDG_RWYT_BDGSpecified = true;
                parInp.P_ID_MUTASI_DTL = null;
                parInp.P_ID_MUTASI_DTLSpecified = true;
                parInp.P_NILAI_KONTRAK = (decimal?)this.PU.teNilaiKontrak.Value;
                parInp.P_NILAI_KONTRAKSpecified = true;
                parInp.P_NM_KONTRAKTOR = this.PU.teNamaKontraktor.Text;
                parInp.P_NO_KONTRAK = this.PU.teNoKontrak.Text;
                parInp.P_NPWP_KONTRAKTOR = this.PU.teNPWPKontraktor.Text;
                parInp.P_RWYT_BDG = this.PU.teRiwayatBangunan.Text;
                parInp.P_TGL_PAKAI = konfigApp.DateToDb(this.PU.teTglPakai.Text);
                parInp.P_TGL_KONTRAK = konfigApp.DateToDb(this.PU.teTglKontrak.Text);
                parInp.P_TGL_MULAI = konfigApp.DateToDb(this.PU.teTglMulaiKontrak.Text);
                parInp.P_TGL_SELESAI = konfigApp.DateToDb(this.PU.teTglSelesai.Text);

                parInp.P_NMFILE = this.PU.teFileName.Text;
                

                if (this.PU.Status == "input")
                {
                    parInp.P_SELECT = "C";
                }
                else
                {
                    parInp.P_SELECT = "U";
                }


                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                this.svcRiwayatPengelolaCrud = new SvcBangunanRiwayatCrud.call_pttClient();
                svcRiwayatPengelolaCrud.Open();
                this.svcRiwayatPengelolaCrud.Beginexecute(parInp, new AsyncCallback(crudRiwayatPengelola), "");
            }
            catch (Exception e)
            {
                this.modeCrud = 'A';
                this.Invoke(new AktifkanForm(this.PU.aktifkanForm), BarItemVisibility.Never);
                this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        #region crud
        public void crudRiwayatPengelola(IAsyncResult result)
        {
            try
            {
                outRiwayatPengelolaCrud = svcRiwayatPengelolaCrud.Endexecute(result);
                svcRiwayatPengelolaCrud.Close();
                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.PU.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
                }
                if (String.Compare(outRiwayatPengelolaCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    this.Invoke(new UbahDsDetail(this.ubahDsDetail), outRiwayatPengelolaCrud);
                }
                else
                {
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
            catch
            {

                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.PU.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
                }

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

        public delegate void UbahDsDetail(SvcBangunanRiwayatCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcBangunanRiwayatCrud.OutputParameters outCrud)
        {
            SvcRiwayatBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_BDG dataPenyama = new SvcRiwayatBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_BDG();

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_ASET = (this.modeCrud == 'U') ? selectedData.ID_ASET : 0;
            dataPenyama.ID_ASETSpecified = true;
            dataPenyama.ID_KBDG = (this.modeCrud == 'U') ? selectedData.ID_KBDG : 0;
            dataPenyama.ID_M_KBDG_RWYT_BDG = (this.modeCrud == 'U') ? selectedData.ID_M_KBDG_RWYT_BDG : outCrud.PO_ID_M_KBDG_RWYT_BDG;
            dataPenyama.ID_M_KBDG_RWYT_BDGSpecified = true;
            dataPenyama.ID_KBDGSpecified = true;
            dataPenyama.ID_MUTASI_DTL = (this.modeCrud == 'U') ? selectedData.ID_MUTASI_DTL : 0;
            dataPenyama.ID_MUTASI_DTLSpecified = true;
            dataPenyama.KD_BRG = (this.modeCrud == 'U') ? selectedData.KD_BRG : "";


            this.ID_M_KBDG_RWYT_BDG = (this.StatusCrud == "edit")? selectedData.ID_M_KBDG_RWYT_BDG: outCrud.PO_ID_M_KBDG_RWYT_BDG;
            if (this.selectedData != null)
            {
                this.NUM = this.selectedData.NUM;
            }
            switch (this.modeCrud)
            {
                case 'C':


                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.PU.FilePath != null)
                    {
                        string filePath = this.PU.FilePath;
                        simpanFile("ID_M_KBDG_RWYT_BDG", dataPenyama.ID_M_KBDG_RWYT_BDG, "M_KBDG_RWYT_BDG", filePath, "C", PU.ID_JNSDOK);
                    }
                    else
                    {
                        this.PU.Close();
                        this.dataInisial = false;
                        this.getById = true;
                        this.getInitRnilaiBangunan(" ID_M_KBDG_RWYT_BDG = " + outCrud.PO_ID_M_KBDG_RWYT_BDG.ToString());
                    }
                 
                    break;
                case 'U':

                    this.binder.Remove(this.selectedData);
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.PU.FilePath != null)
                    {

                        string filePath = this.PU.FilePath;
                        string SELECT = "C";
                        if (selectedData.NMFILE != "-")
                        {
                            SELECT = "U";
                        }
                        simpanFile("ID_M_KBDG_RWYT_BDG", dataPenyama.ID_M_KBDG_RWYT_BDG, "M_KBDG_RWYT_BDG", filePath, SELECT, PU.ID_JNSDOK);
                    }
                    else
                    {
                        this.PU.Close();
                        this.dataInisial = false;
                        this.getById = true;
                        this.getInitRnilaiBangunan(" ID_M_KBDG_RWYT_BDG = " + outCrud.PO_ID_M_KBDG_RWYT_BDG.ToString());
                    }
                    
                    break;
                case 'D':
                     this.binder.Remove(this.selectedData);
                     this.gvUcDtl.RefreshData();
                    this.StrTotalGrid.Caption = (Convert.ToInt64(this.StrTotalGrid.Caption) - 1).ToString();
                    this.StrTotalDb.Caption = (Convert.ToInt64(this.StrTotalDb.Caption) - 1).ToString();
                    break;
            }
        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string filePath, string SELECT, string id_jnsDok = null)
        {
            myThread = new Thread(new ThreadStart(this.PU.ShowProgresBar));
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
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.PU.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.PU.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.PU.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
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
                    this.PU.Close();
                    this.dataInisial = false;
                    this.getById = true;
                    this.getInitRnilaiBangunan(" ID_M_KBDG_RWYT_BDG = " + this.ID_M_KBDG_RWYT_BDG.ToString());
                }
            }

        }
        #endregion

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getInitRnilaiBangunan();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initialData = false;
            this.getInitRnilaiBangunan();
        }

        protected override void bbSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string nama_kolom = this.LuKolom.EditValue.ToString();
                string opr = this.barOperator.EditValue.ToString().ToUpper();
                string parameter = this.teSearch.EditValue.ToString().ToUpper();
                string parameter_2 = "";
                if (opr == "ANTARA")
                {
                    parameter_2 = this.teSearch2.EditValue.ToString().ToUpper();
                }
                this.dataInisial = true;
                this.getInitRnilaiBangunan(get_where_clause(nama_kolom, opr, parameter, parameter_2));
               
            }
            catch (Exception)
            {

                MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
                this.aktifkanForm("");
            }

        }

        private string get_where_clause(string nama_kolom, string opr, string parameter, string parameter2)
        {
            string where = "";
         
                                                             
            switch (nama_kolom)
            {
                case "ID Riwayat Bangunan":
                    where = "ID_M_KBDG_RWYT_BDG " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Riwayat Bangunan":
                    where = "Upper(RWYT_BDG) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "No Kontrak":
                    where = "Upper(NO_KONTRAK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl Kontrak":
                    where = "Upper(TGL_KONTRAK) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Nilai Kontrak":
                    where = "Upper(NILAI_KONTRAK) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Nama Kontraktor":
                    where = "Upper(NM_KONTRAKTOR) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "NPWP Kontraktor":
                    where = "NPWP_KONTRAKTOR " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Alamat Kontraktor":
                    where = "ALAMAT_KONTRAKTOR " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl Mulai":
                    where = "Upper(TGL_MULAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Tgl Selesai":
                    where = "Upper(TGL_SELESAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Tgl Digunakan":
                    where = "Upper(TGL_PAKAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
            
                case "File (Dok)":
                    where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {


        }


        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData == null)
            {
                return;
            }
            if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.selectedData.NUM + " ?", konfigApp.judulHapusData,
             MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    this.StatusCrud = "hapus";
                    this.nonAktifForm("");
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    SvcBangunanRiwayatCrud.InputParameters parInp = new SvcBangunanRiwayatCrud.InputParameters();
                    parInp.P_ID_KBDG = selectedData.ID_KBDG;
                    parInp.P_ID_KBDGSpecified = true;
                    parInp.P_ID_M_KBDG_RWYT_BDG = selectedData.ID_M_KBDG_RWYT_BDG;
                    parInp.P_ID_M_KBDG_RWYT_BDGSpecified = true;


                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcRiwayatPengelolaCrud = new SvcBangunanRiwayatCrud.call_pttClient();
                    svcRiwayatPengelolaCrud.Open();
                    svcRiwayatPengelolaCrud.Beginexecute(parInp, new AsyncCallback(crudRiwayatPengelola), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                }
            }
        }


        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcRiwayatBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_BDG)selectedView.GetRow(e.FocusedRowHandle);
                if (selectedView.IsLastRow)
                {
                    LastRow = true;
                }
                else
                {
                    LastRow = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
                if (selectedData.FILE_EXISTS != 0)
                {
                    this.btnMap.Enabled = true;
                }
                else
                {
                    this.btnMap.Enabled = false;
                }
            }
        }

        protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
        {
            string nama_kolom = this.LuKolom.EditValue.ToString();

            this.LuKolom.Width = 40 + (((int)nama_kolom.Length) * 5);
            switch (nama_kolom)
            {
                case "Tgl Kontrak":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                case "Tgl Mulai":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                case "Tgl Selesai":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                case "Tgl Digunakan":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }

        protected void getInitRnilaiBangunan(string _where = null)
        {
            decimal Max, Min;
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcRiwayatBangunanSelect.InputParameters parInp = new SvcRiwayatBangunanSelect.InputParameters();
            parInp.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
                Max = this.currentMaks;
                Min = this.currentMin;
            }
            else
            {
                if (getById == true)
                {
                    Max = 1;
                    Min = 0;
                }
                else
                {
                    this.currentMin = this.currentMaks + 1;
                    this.currentMaks = this.currentMaks + this.dataAkhir;
                    Max = this.currentMaks;
                    Min = this.currentMin;
                }
            }
            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);
            if (this.dataInisial == true)
            {
                this.search = (_where != null) ? " AND " + _where : "";
                _where = this.search;
            }
            else if (getById == true)
            {
                _where = (_where != null) ? " AND " + _where : "";
            }
            else
            {
                _where = this.search;
            }
            parInp.P_MAX = Max;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = Min;
            parInp.P_MINSpecified = true;
            parInp.STR_WHERE = String.Format(" ID_KBDG ={0} {1}", this.ID_KBDG, _where);
            parInp.P_SORT = "DESC";
            fetchData = new SvcRiwayatBangunanSelect.call_pttClient();
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outDat = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);

                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcRiwayatBangunanSelect.OutputParameters dataOut);

        public void showData(SvcRiwayatBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[0].TOTAL_DATA.ToString();
                }
                else
                {
                    StrTotalGrid.Caption = "0";
                    StrTotalDb.Caption = "0";
                }
            }
            else
            {
                if (jmlDataGroup > 0 && this.StatusCrud != "edit")
                {

                    StrTotalGrid.Caption = (Convert.ToInt64(StrTotalGrid.Caption) + jmlDataGroup).ToString();
                    if (this.StatusCrud != "input")
                    {
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_BUKU).Substring(0, 10);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_KONTRAK).Substring(0, 10);
              string date3 = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_MULAI).Substring(0, 10);
              string date4 = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_PAKAI).Substring(0, 10);
              string date5 = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_SELESAI).Substring(0, 10);

              string date_ = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_BUKU).Substring(0, 8);
              string date2_ = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_KONTRAK).Substring(0, 8);
              string date3_ = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_MULAI).Substring(0, 8);
              string date4_ = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_PAKAI).Substring(0, 8);
              string date5_ = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_SELESAI).Substring(0, 8);

              if (date == "11/11/1000" || date_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_BUKU = null;
              }
              if (date2 == "11/11/1000" || date2_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_KONTRAK = null;
              }
              if (date3 == "11/11/1000" || date3_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_MULAI = null;
              }
              if (date4 == "11/11/1000" || date4_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_PAKAI = null;
              }
              if (date5 == "11/11/1000" || date5_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i].TGL_SELESAI = null;
              }
                binder.Add(serviceOutPut.SF_ROW_M_KBDG_RWYT_BDG[i]);
            }

            if (this.getById == true)
            {
                this.getById = false;
            }
            else
            {
                if (jmlDataGroup < konfigApp.dataAkhir)
                {
                    this.loadMore = false;
                    this.bbMore.Enabled = false;
                }
                else
                {
                    this.loadMore = true;
                    this.bbMore.Enabled = true;
                }
            }
            StatusCrud = "";
            this.gvUcDtl.RefreshData();
            this.gvUcDtl.BestFitColumns();
        }

        

    }
}
