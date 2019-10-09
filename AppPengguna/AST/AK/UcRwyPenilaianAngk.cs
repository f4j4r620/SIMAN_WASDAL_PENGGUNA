﻿using System;
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

namespace AppPengguna.AST.AK
{
    class UcRwyPenilaianAngk : UserControlDetail
    {
        private SvcRwyNilaiAngkSelect.call_pttClient fetchData;
        private SvcRwyNilaiAngkSelect.InputParameters parInp;
        private SvcRwyNilaiAngkSelect.OutputParameters outDat;
        private SvcRwyNilaiAngkSelect.BPSIMANSROW_M_KANGK_RWYT_NILAI selectedData;
        GridRow grow;
        private bool isSearch = false;
        private bool initiated = false;
        private frmNilai puNilai;

    
        public decimal? ID_KANGK;
        public string Status;
        private decimal? jmlData = 0;
        public UcRwyPenilaianAngk(decimal? _ID_KANGK, string _status)
            : base()
        {
            this.ID_KANGK = _ID_KANGK;
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

        public void getInitPenilaianAngk(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcRwyNilaiAngkSelect.InputParameters parInp = new SvcRwyNilaiAngkSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KANGK = {0} {1}", this.ID_KANGK, _where);
            fetchData = new SvcRwyNilaiAngkSelect.call_pttClient();
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }


        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.gvUcDtl.OptionsBehavior.Editable = false;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcRwyNilaiAngkSelect.BPSIMANSROW_M_KANGK_RWYT_NILAI);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false, 80);
            this.setKolom("Nomor Penilaian", "NO_NILAI", "NO_NILAI", 1, true);
            this.setKolom("Tgl Penilaian", "TGL_NILAI", "TGL_NILAI", 2, true, 100, "date");
            this.setKolom("Nilai Buku Saat Penilaian", "NILAI_BUKU", "NILAI_BUKU", 3, true, 120, "number");
            this.setKolom("Nilai Wajar", "NILAI_WAJAR", "NILAI_WAJAR", 4, true, 120, "number");
            this.setKolom("Instansi Tim Penilai", "PENILAI", "PENILAI", 5, true);
            this.setKolom("File (Dok Penilaian)", "NMFILE", "NMFILE", 6, true);
            this.gridDoubleClickDetail = true;

            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);

            this.show_record = true;

            this.initGrid();
            this.getInitPenilaianAngk();
            
            if (jmlData == 0)
            {
                this.btnMap.Enabled = false;
            }
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
                parInp.P_ID = selectedData.ID_KANGK_RWYT_NILAI;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_KANGK_RWYT_NILAI";
                parInp.P_TABLE = "M_KANGK_RWYT_NILAI";

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

        protected void gvUcDtl_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

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

        private delegate void ShowData(SvcRwyNilaiAngkSelect.OutputParameters dataOut);
        private string StatusCrud = "";
        private decimal? NUM;
        public void showData(SvcRwyNilaiAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI.Count();
            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date1 = Convert.ToString(serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[i].TGL_NILAI).Substring(0, 10);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[i].TGL_BUKU).Substring(0, 10);
              if (date1 == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[i].TGL_NILAI = null;
                }
              if (date2 == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[i].TGL_BUKU = null;
                }
                binder.Add(serviceOutPut.SF_ROW_M_KANGK_RWYT_NILAI[i]);
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

        
        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            puNilai = new frmNilai("input");
            this.puNilai.saveForm = new SaveFrmNilai(this.saveFrmNilai);
            StatusCrud = "input";
            puNilai.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                if (this.Status == "detail")
                {
                    puNilai = new frmNilai("detail");
                    StatusCrud = "detail";
                }
                else
                {
                    puNilai = new frmNilai("edit");
                    StatusCrud = "edit";
                }
                this.NUM = selectedData.NUM;
                this.puNilai.resetForm = new ResetFrmNilai(this.resetFrmNilai);
                this.puNilai.saveForm = new SaveFrmNilai(this.saveFrmNilai);
                this.resetFrmNilai("reset");
                puNilai.ShowDialog();
            }
        }

        private void resetFrmNilai(string text)
        {
            this.puNilai.teNomorPenilaian.Text = selectedData.NO_NILAI;
            this.puNilai.teTglPenilaian.Text = konfigApp.DateToString(selectedData.TGL_NILAI);
            this.puNilai.teNilaiBuku.Value = (decimal)selectedData.NILAI_BUKU;
            this.puNilai.teNilaiWajar.Value = (decimal)selectedData.NILAI_WAJAR;
            this.puNilai.teInstansiPenilai.Text = selectedData.PENILAI;
            this.puNilai.teFileName.Text = selectedData.NMFILE;
            
        }

        SvcAngkutanRiwayatNilaiCrud.call_pttClient svcRiwayatNilaiCrud;
        SvcAngkutanRiwayatNilaiCrud.OutputParameters outRiwayatNilaiCrud;

        private void saveFrmNilai(string text)
        {
            
            try
            {
                if (this.modeCrud != 'D')
                {
                    myThread = new Thread(new ThreadStart(this.puNilai.ShowProgresBar));
                    this.puNilai.nonAktifkanForm("nonaktif");
                }
                else
                {
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    this.nonAktifForm("");
                }

                myThread.Start();
                SvcAngkutanRiwayatNilaiCrud.InputParameters parInp = new SvcAngkutanRiwayatNilaiCrud.InputParameters();
                parInp.P_ID_KANGK = this.ID_KANGK;
                parInp.P_ID_KANGK_RWYT_NILAI = (this.puNilai.Status == "input") ? 0 : this.selectedData.ID_KANGK_RWYT_NILAI;
                parInp.P_ID_KANGK_RWYT_NILAISpecified = true;
                parInp.P_ID_KANGKSpecified = true;
                parInp.P_ID_MUTASI_DTL = null;
                parInp.P_ID_MUTASI_DTLSpecified = true;
                parInp.P_NILAI_BUKU = this.puNilai.teNilaiBuku.Value;
                parInp.P_NILAI_BUKUSpecified = true;
                parInp.P_NILAI_WAJAR = this.puNilai.teNilaiWajar.Value;
                parInp.P_NILAI_WAJARSpecified = true;

                parInp.P_NO_NILAI = this.puNilai.teNomorPenilaian.Text;
                parInp.P_PENILAI = this.puNilai.teInstansiPenilai.Text;
                parInp.P_TGL_NILAI = konfigApp.DateToDb(this.puNilai.teTglPenilaian.Text);

                if (this.puNilai.FilePath != null)
                {
                    parInp.P_NMFILE = this.puNilai.teFileName.Text;
                }
                else
                {
                    parInp.P_NMFILE = (this.puNilai.Status == "input") ? null : selectedData.NMFILE;
                }

                if (this.puNilai.Status == "input")
                {
                    parInp.P_SELECT = "C";
                }
                else
                {
                    parInp.P_SELECT = "U";
                }


                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                this.svcRiwayatNilaiCrud = new SvcAngkutanRiwayatNilaiCrud.call_pttClient();
                svcRiwayatNilaiCrud.Open();
                this.svcRiwayatNilaiCrud.Beginexecute(parInp, new AsyncCallback(crudRiwayatNilai), "");
            }
            catch (Exception e)
            {
                this.modeCrud = 'A';
                this.Invoke(new AktifkanForm(this.puNilai.aktifkanForm), BarItemVisibility.Never);
                this.Invoke(new ProgBar(this.puNilai.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }
        #region crud
        public void crudRiwayatNilai(IAsyncResult result)
        {
            try
            {
                outRiwayatNilaiCrud = svcRiwayatNilaiCrud.Endexecute(result);
                svcRiwayatNilaiCrud.Close();
                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.puNilai.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puNilai.progBar), BarItemVisibility.Never);
                }
                if (String.Compare(outRiwayatNilaiCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    this.Invoke(new UbahDsDetail(this.ubahDsDetail), outRiwayatNilaiCrud);
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
                    this.Invoke(new AktifkanForm(this.puNilai.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puNilai.progBar), BarItemVisibility.Never);
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

        public delegate void UbahDsDetail(SvcAngkutanRiwayatNilaiCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcAngkutanRiwayatNilaiCrud.OutputParameters outCrud)
        {
            SvcRwyNilaiAngkSelect.BPSIMANSROW_M_KANGK_RWYT_NILAI dataPenyama = new SvcRwyNilaiAngkSelect.BPSIMANSROW_M_KANGK_RWYT_NILAI();

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_ASET = (this.modeCrud == 'U') ? selectedData.ID_ASET : 0;
            dataPenyama.ID_ASETSpecified = true;
            dataPenyama.ID_KANGK = (this.modeCrud == 'U') ? selectedData.ID_KANGK : 0;
            dataPenyama.ID_KANGK_RWYT_NILAI = (this.modeCrud == 'U') ? selectedData.ID_KANGK_RWYT_NILAI :  outCrud.PO_ID_KANGK_RWYT_NILAI;
            dataPenyama.ID_KANGK_RWYT_NILAISpecified = true;
            dataPenyama.ID_KANGKSpecified = true;
            dataPenyama.ID_MUTASI_DTL = (this.modeCrud == 'U') ? selectedData.ID_MUTASI_DTL : 0;
            dataPenyama.ID_MUTASI_DTLSpecified = true;
            dataPenyama.KD_BRG = (this.modeCrud == 'U') ? selectedData.KD_BRG : "";
            dataPenyama.NILAI_BUKU = (this.modeCrud != 'D') ? this.puNilai.teNilaiBuku.Value : 0;
            dataPenyama.NILAI_BUKUSpecified = true;
            dataPenyama.NILAI_WAJAR = (this.modeCrud != 'D') ? this.puNilai.teNilaiWajar.Value : 0;
            dataPenyama.NILAI_WAJARSpecified = true;
            dataPenyama.NMFILE = (this.modeCrud != 'D') ? this.puNilai.teFileName.Text : "";
            dataPenyama.NO_ASET = (this.modeCrud == 'U') ? this.selectedData.NO_ASET : "";
            dataPenyama.NO_NILAI = (this.modeCrud != 'D') ? this.puNilai.teNomorPenilaian.Text : "";
            dataPenyama.NOREG = (this.modeCrud == 'U') ? selectedData.NOREG : "";
            dataPenyama.PENILAI = (this.modeCrud != 'D') ? this.puNilai.teInstansiPenilai.Text : "";
            dataPenyama.TGL_NILAI = (this.modeCrud != 'D') ? konfigApp.ToDate(this.puNilai.teTglPenilaian.Text) : konfigApp.ToDate("01/01/0001");
            dataPenyama.TGL_NILAISpecified = true;


            switch (this.modeCrud)
            {
                case 'C':


                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.puNilai.FilePath != null)
                    {
                        string filePath = this.puNilai.FilePath;
                        simpanFile("ID_KANGK_RWYT_NILAI", dataPenyama.ID_KANGK_RWYT_NILAI, "M_KANGK_RWYT_NILAI", filePath, "C", puNilai.ID_JNSDOK);
                        this.search = "";
                        this.pencarian = false;
                        this.initGrid();
                        this.getInitPenilaianAngk();
                    }
                    else
                    {
                        this.puNilai.Close();
                    }
                    //this.binder.Add(dataPenyama);
                    break;
                case 'U':

                    //this.binder.Add(dataPenyama);
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.puNilai.FilePath != null)
                    {

                        string filePath = this.puNilai.FilePath;
                        string SELECT = "C";
                        if (selectedData.FILE_EXISTS != 0)
                        {
                            SELECT = "U";
                        }
                        simpanFile("ID_KANGK_RWYT_NILAI", dataPenyama.ID_KANGK_RWYT_NILAI, "M_KANGK_RWYT_NILAI", filePath, SELECT, puNilai.ID_JNSDOK);
                    }
                    else
                    {
                        this.puNilai.Close();
                    }
                    this.binder.Remove(this.selectedData);
                    this.search = "";
                    this.pencarian = false;
                    this.initGrid();
                    this.getInitPenilaianAngk();
                    break;
                case 'D':
                    this.binder.Remove(this.selectedData);
                     gvUcDtl.RefreshData();
                    this.StrTotalGrid.Caption = (Convert.ToInt64(this.StrTotalGrid.Caption) - 1).ToString();
                    this.StrTotalDb.Caption = (Convert.ToInt64(this.StrTotalDb.Caption) - 1).ToString();
                    this.search = "";
                    this.pencarian = false;
                    this.initGrid();
                    this.getInitPenilaianAngk();
                    break;
            }
        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string filePath, string SELECT, string id_jnsDok = null)
        {
            myThread = new Thread(new ThreadStart(this.puNilai.ShowProgresBar));
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
                    this.Invoke(new AktifkanForm(this.puNilai.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puNilai.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new AktifkanForm(this.puNilai.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puNilai.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new AktifkanForm(this.puNilai.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puNilai.progBar), BarItemVisibility.Never);
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
                    this.puNilai.Close();
                }
            }

        }
        #endregion

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            initGrid();
            getInitPenilaianAngk();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            getInitPenilaianAngk();
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
                    StatusCrud = "hapus";
                    this.nonAktifForm("");
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    SvcAngkutanRiwayatNilaiCrud.InputParameters parInp = new SvcAngkutanRiwayatNilaiCrud.InputParameters();
                    parInp.P_ID_KANGK = selectedData.ID_KANGK;
                    parInp.P_ID_KANGK_RWYT_NILAI = selectedData.ID_KANGK_RWYT_NILAI;
                    parInp.P_ID_KANGK_RWYT_NILAISpecified = true;
                    parInp.P_ID_KANGKSpecified = true;
                    parInp.P_ID_MUTASI_DTL = selectedData.ID_MUTASI_DTL;
                    parInp.P_ID_MUTASI_DTLSpecified = true;
                    parInp.P_NILAI_BUKU = selectedData.NILAI_BUKU;
                    parInp.P_NILAI_BUKUSpecified = true;
                    parInp.P_NILAI_WAJAR = selectedData.NILAI_WAJAR;
                    parInp.P_NILAI_WAJARSpecified = true;
                    parInp.P_NMFILE = selectedData.NMFILE;
                    parInp.P_NO_NILAI = selectedData.NO_NILAI;
                    parInp.P_PENILAI = selectedData.PENILAI;
                    parInp.P_TGL_NILAI = konfigApp.DateToString(selectedData.TGL_NILAI);

                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcRiwayatNilaiCrud = new SvcAngkutanRiwayatNilaiCrud.call_pttClient();
                    svcRiwayatNilaiCrud.Open();
                    svcRiwayatNilaiCrud.Beginexecute(parInp, new AsyncCallback(crudRiwayatNilai), "");
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
                this.getInitPenilaianAngk(get_where_clause(nama_kolom, opr, parameter, parameter_2));

               
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
                case "ID Riwayat Nilai":
                    where = "ID_KBDG_RWYT_NILAI " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Nomor Penilaian":
                    where = "Upper(NO_NILAI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl Penilaian":
                    where = "TGL_NILAI " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Nilai Buku Saat Penilaian":
                    where = "NILAI_BUKU " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Nilai Wajar":
                    where = "NILAI_WAJAR " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Instansi Tim Penilai":
                    where = "Upper(PENILAI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "File (Dok Penilaian)":
                    where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcRwyNilaiAngkSelect.BPSIMANSROW_M_KANGK_RWYT_NILAI)selectedView.GetRow(e.FocusedRowHandle);
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
                    jmlData = selectedView.SelectedRowsCount;
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
                case "Tgl Penilaian":
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