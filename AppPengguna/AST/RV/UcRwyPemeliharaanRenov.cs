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
using DevExpress.XtraGrid.Views.Base;
using AppPengguna.AST.PUASET;

namespace AppPengguna.AST.RV
{
    class UcRwyPemeliharaanRenov:UserControlDetail
    {
        private SvcRwyPemeliharaanRenovSelect.call_pttClient fetchData;
        private SvcRwyPemeliharaanRenovSelect.InputParameters parInp;
        private SvcRwyPemeliharaanRenovSelect.OutputParameters outDat;
        private SvcRwyPemeliharaanRenovSelect.BPSIMANSROW_M_KRNV_RWYT_PELIHARA selectedData;
        GridRow grow;
        private bool isSearch = false;
        private bool initiated = false;

        private frmPemeliharaan puPemeliharaan;
    
        
        public decimal? ID_RENOV;
        public string Status;
        private decimal? NUM;
        private string StatusCrud = "";
        public UcRwyPemeliharaanRenov(decimal? _ID_RENOV, string _status)
            : base()
        {
            this.ID_RENOV = _ID_RENOV;
            this.Status = _status;

            if (Status == "detail")
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
        public void getInitPemeliharaanAngk(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcRwyPemeliharaanRenovSelect.InputParameters parInp = new SvcRwyPemeliharaanRenovSelect.InputParameters();
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
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = String.Format(" ID_KRNV = {0} {1}", this.ID_RENOV, _where);
            fetchData = new SvcRwyPemeliharaanRenovSelect.call_pttClient();
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outDat = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                
                this.Invoke(new ShowData(this.showData), this.outDat);
                
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcRwyPemeliharaanRenovSelect.OutputParameters dataOut);

        public void showData(SvcRwyPemeliharaanRenovSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                string date1 = Convert.ToString(serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[i].TGL_BUKU).Substring(0, 10);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[i].TGL_SP2D).Substring(0, 10);
              if (date1 == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[i].TGL_BUKU = null;
                }
                if (date2 == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[i].TGL_SP2D = null;
                }
                binder.Add(serviceOutPut.SF_ROW_M_KRNV_RWYT_PELIHARA[i]);
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

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcRwyPemeliharaanRenovSelect.BPSIMANSROW_M_KRNV_RWYT_PELIHARA);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("No Dipa", "NO_DIPA", "NO_DIPA", 1, true);
            this.setKolom("No SP2D", "NO_SP2D", "NO_SP2D", 2, true);
            this.setKolom("Tgl SP2D", "TGL_SP2D", "TGL_SP2D", 3, true, 100, "date");
            this.setKolom("MAP", "KD_AKUN", "KD_AKUN", 4, true);
            this.setKolom("Uraian", "UR_AKUN", "UR_AKUN", 5, true);
            this.setKolom("Nilai", "NILAI", "NILAI", 6, true, 120, "number");
            this.setKolom("Jenis Pemeliharaan", "JNS_PELIHARA", "JNS_PELIHARA", 7, true);
            this.setKolom("Pihak yang melakukan pemeliharaan", "PHK_PELIHARA", "PHK_PELIHARA", 8, true);
            this.setKolom("Unit Pemelihara", "KD_SATKER", "KD_SATKER", 9, true);
            this.setKolom("Uraian Unit Pemelihara", "UR_SATKER", "UR_SATKER", 10, true);
            this.setKolom("Keterangan", "KET", "KET", 11, true);
            this.setKolom("File (DIPA/SP2D)", "NMFILE", "NMFILE", 12, true);
            this.setKolom("Terakhir", "TERAKHIR_YN", "TERAKHIR_YN", 13, false, 100, "string", true);
            this.gridDoubleClickDetail = true;

            this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.gvUcDtl.OptionsBehavior.Editable = false;
            
            

            this.ShowFooter(true);
            // Menampilkan jumlah data di grid
            this.SetSummary(6, "NILAI", "Sum");
  
            this.initGrid();
            this.getInitPemeliharaanAngk();

            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);

            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
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
                parInp.P_ID = selectedData.ID_M_KRNV_RWYT_PELIHARA;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_M_KRNV_RWYT_PELIHARA";
                parInp.P_TABLE = "M_KRNV_RWYT_PELIHARA";

                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient();
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

    

        protected void gvUcDtl_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

   
       
        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            puPemeliharaan = new frmPemeliharaan("input");
            this.puPemeliharaan.saveForm = new SaveFrmPemeliharaan(this.saveForm);
            StatusCrud = "input";
            puPemeliharaan.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    puPemeliharaan = new frmPemeliharaan("detail");
                    StatusCrud = "detail";
                }
                else
                {
                    puPemeliharaan = new frmPemeliharaan("edit");
                    StatusCrud = "edit";
                }
                this.puPemeliharaan.resetForm = new ResetFrmPemeliharaan(this.resetForm);
                this.puPemeliharaan.saveForm = new SaveFrmPemeliharaan(this.saveForm);
                this.resetForm("reset");
                puPemeliharaan.ShowDialog();
            }
        }

        private void resetForm(string text)
        {
            this.puPemeliharaan.teFileName.Text = selectedData.NMFILE;
            this.puPemeliharaan.teJenisPemeliharaan.Text = selectedData.JNS_PELIHARA;
            this.puPemeliharaan.teKdSatker.Text = konfigApp.StringtoNull(selectedData.KD_SATKER);
            this.puPemeliharaan.teKeteragan.Text = selectedData.KET;
            this.puPemeliharaan.teMAP.Text = konfigApp.StringtoNull(selectedData.KD_AKUN);
            this.puPemeliharaan.teNilai.Value = (decimal)selectedData.NILAI;
            this.puPemeliharaan.teNomorDipa.Text = selectedData.NO_DIPA;
            this.puPemeliharaan.teNomorSP2D.Text = selectedData.NO_SP2D;
            this.puPemeliharaan.teTglSP2D.Text = konfigApp.DateToString(selectedData.TGL_SP2D);
            this.puPemeliharaan.tePemelihara.Text = selectedData.PHK_PELIHARA;
            this.puPemeliharaan.teUraian.Text = selectedData.UR_TRN;
            this.puPemeliharaan.teUrSatker.Text = selectedData.UR_SATKER;

        }

        SvcRwyPemeliharaanRenovCrud.rwytPeliharaCud_pttClient svcRiwayatPemeliharaanCrud;
        SvcRwyPemeliharaanRenovCrud.OutputParameters outRiwayatPemeliharaanCrud;

        private void saveForm(string text)
        {
            this.puPemeliharaan.nonAktifkanForm("nonaktif");
            try
            {
                if (this.modeCrud != 'D')
                {
                    myThread = new Thread(new ThreadStart(this.puPemeliharaan.ShowProgresBar));
                }
                else
                {
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                }

                myThread.Start();
                this.nonAktifForm("");
                SvcRwyPemeliharaanRenovCrud.InputParameters parInp = new SvcRwyPemeliharaanRenovCrud.InputParameters();
                parInp.P_ID_KRNV = this.ID_RENOV;
                parInp.P_ID_M_KRNV_RWYT_PELIHARA = (this.puPemeliharaan.Status == "input") ? 0 : this.selectedData.ID_M_KRNV_RWYT_PELIHARA;
                parInp.P_ID_M_KRNV_RWYT_PELIHARASpecified = true;
                parInp.P_ID_KRNVSpecified = true;
                parInp.P_ID_MUTASI_DTL = null;
                parInp.P_ID_MUTASI_DTLSpecified = true;
                parInp.P_ID_SATKER = this.puPemeliharaan.ID_SATKER;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_JNS_PELIHARA = this.puPemeliharaan.teJenisPemeliharaan.Text;
                parInp.P_KD_AKUN = this.puPemeliharaan.teMAP.Text;
                parInp.P_KET = this.puPemeliharaan.teKeteragan.Text;
                parInp.P_NILAI = this.puPemeliharaan.teNilai.Value;
                parInp.P_NILAISpecified = true;
                parInp.P_NMFILE = this.puPemeliharaan.teFileName.Text;
                parInp.P_NO_DIPA = this.puPemeliharaan.teNomorDipa.Text;
                parInp.P_NO_SP2D = this.puPemeliharaan.teNomorSP2D.Text;
                parInp.P_PHK_PELIHARA = this.puPemeliharaan.tePemelihara.Text;
                parInp.P_TGL_SP2D = konfigApp.DateToDb(this.puPemeliharaan.teTglSP2D.Text);
                

                parInp.P_NMFILE = this.puPemeliharaan.teFileName.Text;
              

                if (this.puPemeliharaan.Status == "input")
                {
                    parInp.P_SELECT = "C";
                }
                else
                {
                    parInp.P_SELECT = "U";
                }


                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                this.svcRiwayatPemeliharaanCrud = new SvcRwyPemeliharaanRenovCrud.rwytPeliharaCud_pttClient();
                svcRiwayatPemeliharaanCrud.Open();
                this.svcRiwayatPemeliharaanCrud.BeginrwytPeliharaCud(parInp, new AsyncCallback(crudRiwayatPemeliharaan), "");
            }
            catch (Exception e)
            {
                this.modeCrud = 'A';
                this.Invoke(new AktifkanForm(this.puPemeliharaan.aktifkanForm), BarItemVisibility.Never);
                this.Invoke(new ProgBar(this.puPemeliharaan.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }
        #region crud
        public void crudRiwayatPemeliharaan(IAsyncResult result)
        {
            try
            {
                outRiwayatPemeliharaanCrud = svcRiwayatPemeliharaanCrud.EndrwytPeliharaCud(result);
                svcRiwayatPemeliharaanCrud.Close();
                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.puPemeliharaan.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPemeliharaan.progBar), BarItemVisibility.Never);
                }
                if (String.Compare(outRiwayatPemeliharaanCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    this.Invoke(new UbahDsDetail(this.ubahDsDetail), outRiwayatPemeliharaanCrud);
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
                    this.Invoke(new AktifkanForm(this.puPemeliharaan.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPemeliharaan.progBar), BarItemVisibility.Never);
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

        public delegate void UbahDsDetail(SvcRwyPemeliharaanRenovCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcRwyPemeliharaanRenovCrud.OutputParameters outCrud)
        {
            SvcRwyPemeliharaanRenovSelect.BPSIMANSROW_M_KRNV_RWYT_PELIHARA dataPenyama = new SvcRwyPemeliharaanRenovSelect.BPSIMANSROW_M_KRNV_RWYT_PELIHARA();

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_ASET = (this.modeCrud == 'U') ? selectedData.ID_ASET : 0;
            dataPenyama.ID_ASETSpecified = true;
            dataPenyama.ID_KRNV = (this.modeCrud == 'U') ? selectedData.ID_KRNV : 0;
            dataPenyama.ID_M_KRNV_RWYT_PELIHARA = (this.modeCrud == 'U') ? selectedData.ID_M_KRNV_RWYT_PELIHARA : outCrud.PO_ID_M_KRNV_RWYT_PELIHARA;
            dataPenyama.ID_M_KRNV_RWYT_PELIHARASpecified = true;
            dataPenyama.ID_KRNVSpecified = true;
            dataPenyama.ID_MUTASI_DTL = (this.modeCrud == 'U') ? selectedData.ID_MUTASI_DTL : 0;
            dataPenyama.ID_MUTASI_DTLSpecified = true;
            dataPenyama.KD_BRG = (this.modeCrud == 'U') ? selectedData.KD_BRG : "";



            switch (this.modeCrud)
            {
                case 'C':


                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.puPemeliharaan.FilePath != null)
                    {
                        string filePath = this.puPemeliharaan.FilePath;
                        simpanFile("ID_KANGK_RWYT_PELIHARA", dataPenyama.ID_M_KRNV_RWYT_PELIHARA, "M_KANGK_RWYT_PELIHARA", filePath, "C", puPemeliharaan.ID_JNSDOK);
                    }
                    else
                    {
                        this.puPemeliharaan.Close();
                    }
                    //this.binder.Add(dataPenyama);
                    break;
                case 'U':

                    this.binder.Remove(this.selectedData);
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.puPemeliharaan.FilePath != null)
                    {

                        string filePath = this.puPemeliharaan.FilePath;
                        string SELECT = "C";
                        if (selectedData.FILE_EXISTS != 0)
                        {
                            SELECT = "U";
                        }
                        simpanFile("ID_KANGK_RWYT_PELIHARA", dataPenyama.ID_M_KRNV_RWYT_PELIHARA, "M_KANGK_RWYT_PELIHARA", filePath, SELECT, puPemeliharaan.ID_JNSDOK);
                    }
                    else
                    {
                        this.puPemeliharaan.Close();
                    }
                    
                    break;
                case 'D':
                    this.binder.Remove(this.selectedData);
                     gvUcDtl.RefreshData();
                    this.StrTotalGrid.Caption = (Convert.ToInt64(this.StrTotalGrid.Caption) - 1).ToString();
                    this.StrTotalDb.Caption = (Convert.ToInt64(this.StrTotalDb.Caption) - 1).ToString();
                    break;
            }
        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string filePath, string SELECT, string id_jnsDok = null)
        {
            myThread = new Thread(new ThreadStart(this.puPemeliharaan.ShowProgresBar));
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
                    this.Invoke(new AktifkanForm(this.puPemeliharaan.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPemeliharaan.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new AktifkanForm(this.puPemeliharaan.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPemeliharaan.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new AktifkanForm(this.puPemeliharaan.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPemeliharaan.progBar), BarItemVisibility.Never);
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
                    this.puPemeliharaan.Close();
                }
                
            }
            search = "";
            pencarian = false;
            initGrid();
            getInitPemeliharaanAngk();
        }
        #endregion

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            initGrid();
            getInitPemeliharaanAngk();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            getInitPemeliharaanAngk();
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
                    SvcRwyPemeliharaanRenovCrud.InputParameters parInp = new SvcRwyPemeliharaanRenovCrud.InputParameters();
                    parInp.P_ID_KRNV = selectedData.ID_KRNV;
                    parInp.P_ID_KRNVSpecified = true;
                    parInp.P_ID_M_KRNV_RWYT_PELIHARA = selectedData.ID_M_KRNV_RWYT_PELIHARA;
                    parInp.P_ID_M_KRNV_RWYT_PELIHARASpecified = true;


                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcRiwayatPemeliharaanCrud = new SvcRwyPemeliharaanRenovCrud.rwytPeliharaCud_pttClient();
                    svcRiwayatPemeliharaanCrud.Open();
                    svcRiwayatPemeliharaanCrud.BeginrwytPeliharaCud(parInp, new AsyncCallback(crudRiwayatPemeliharaan), "");
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
                this.getInitPemeliharaanAngk(get_where_clause(nama_kolom, opr, parameter, parameter_2));

               
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

                case "No Dipa":
                    where = "UPPER(NO_DIPA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "No SP2D":
                    where = "Upper(NO_SP2D) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl SP2D":
                    where = "Upper(TGL_SP2D) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "MAP":
                    where = "Upper(KD_AKUN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Uraian":
                    where = "Upper(UR_AKUN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nilai":
                    where = "Upper(NILAI) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Jenis Pemeliharaan":
                    where = "Upper(JNS_PELIHARA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Pihak yang melakukan pemeliharaan":
                    where = "Upper(PHK_PELIHARA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Unit Pemelihara":
                    where = "Upper(KD_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Uraian Unit Pemelihara":
                    where = "Upper(UR_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "File (DIPA/SP2D)":
                    where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Terakhir":
                    where = "Upper(TERAKHIR_YN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
        }

        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcRwyPemeliharaanRenovSelect.BPSIMANSROW_M_KRNV_RWYT_PELIHARA)selectedView.GetRow(e.FocusedRowHandle);
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
                case "Tgl SP2D":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;

                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }
    }
}
