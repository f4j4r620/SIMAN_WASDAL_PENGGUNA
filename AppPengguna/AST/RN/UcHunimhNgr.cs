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

namespace AppPengguna.AST.RN
{
    class ucPenghuniRumahNegara : UserControlDetail
    {
        private SvcHuniRmhngrSelect.call_pttClient fetchData;
        private SvcHuniRmhngrSelect.InputParameters parInp;
        private SvcHuniRmhngrSelect.OutputParameters outDat;
        private SvcHuniRmhngrSelect.BPSIMANSROW_KRMH_NEG_RWYT_PENGHUNI selectedData;
        GridRow grow;
        private bool isSearch = false;
        private bool initiated = false;
        private FrmPemakai PU;
        private decimal? id_penyama;

        public decimal? ID_KRMH_NEG;
        public string Status;
        public decimal? ID_KRMH_RWYT_PEMAKAI;
        private decimal? NUM;
        private string StatusCrud = "";
        public ucPenghuniRumahNegara(decimal? _ID_KRMH_NEG, string _status)
            : base()
        {
            this.ID_KRMH_NEG = _ID_KRMH_NEG;
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
            this.btnMap.Enabled = false;
        }

        public void getInitPemakaiAngk(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcHuniRmhngrSelect.InputParameters parInp = new SvcHuniRmhngrSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KRMH_NEG = {0} {1}", this.ID_KRMH_NEG, _where);
            fetchData = new SvcHuniRmhngrSelect.call_pttClient();
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

        private delegate void ShowData(SvcHuniRmhngrSelect.OutputParameters dataOut);

        public void showData(SvcHuniRmhngrSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_BUKU).Substring(0, 10);
              string date_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_BUKU).Substring(0, 8);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_LAHIR).Substring(0, 10);
              string date2_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_LAHIR).Substring(0, 8);
              string date3 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_MULAI).Substring(0, 10);
              string date3_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_MULAI).Substring(0, 8);
              string date4 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_PENSIUN).Substring(0, 10);
              string date4_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_PENSIUN).Substring(0, 8);
              string date5 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SELESAI).Substring(0, 10);
              string date5_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SELESAI).Substring(0, 8);
              string date6 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SIP).Substring(0, 10);
              string date6_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SIP).Substring(0, 8);
              string date7 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SK).Substring(0, 10);
              string date7_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SK).Substring(0, 8);
              string date8 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TMT_JAB).Substring(0, 10);
              string date8_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TMT_JAB).Substring(0, 8);


              if (date == "11/11/1000" || date_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_BUKU = null;
              }          
              if (date2 == "11/11/1000" || date2_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_LAHIR = null;
              }
              if (date3 == "11/11/1000" || date3_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_MULAI = null;
              }
              if (date4 == "11/11/1000" || date4_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_PENSIUN = null;
              }
              if (date5 == "11/11/1000" || date5_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SELESAI = null;
              }
              if (date6 == "11/11/1000" || date6_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SIP = null;
              }
              if (date7 == "11/11/1000" || date7_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TGL_SK = null;
              }
              if (date8 == "11/11/1000" || date8_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].TMT_JAB = null;
              }
              if (Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].PHOTO_EXISTS) == "-")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].PHOTO = "Tidak Ada";
              }
              else
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i].PHOTO = "Ada";
              }
                binder.Add(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_PENGHUNI[i]);
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
            this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.gvUcDtl.OptionsBehavior.Editable = false;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcHuniRmhngrSelect.BPSIMANSROW_KRMH_NEG_RWYT_PENGHUNI);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("NIP", "NIP_PMK", "NIP_PMK", 1, true);
            this.setKolom("Nama Pegawai", "NM_PMK", "NM_PMK", 2, true);
            this.setKolom("No. KTP", "NO_KTP", "NO_KTP", 3, true);
            this.setKolom("Unit Pegawai", "UR_SATKER", "UR_SATKER", 4, true);
            this.setKolom("Tgl Surat Ijin Pegawai (SIP)", "TGL_SIP", "TGL_SIP", 5, true, 120, "date");
            this.setKolom("Tgl Mulai Menghuni", "TGL_MULAI", "TGL_MULAI", 6, true, 120, "date");
            this.setKolom("Tgl Selesai Menghuni", "TGL_SELESAI", "TGL_SELESAI", 7, true, 120, "date");
            this.setKolom("Keterangan", "KET", "KET", 8, true);
            this.setKolom("Foto Pegawai", "PHOTO", "PHOTO", 9, false);
            this.setKolom("File (Dok)", "NMFILE", "NMFILE", 10, true);


            this.show_record = true;
            this.gridDoubleClickDetail = true;

            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);


            this.initGrid();
            this.getInitPemakaiAngk();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.btnMap.Enabled = false;
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
                parInp.P_ID = selectedData.ID_KRMH_RWYT_PEMAKAI;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_KRMH_RWYT_PEMAKAI";
                parInp.P_TABLE = "M_KRMH_NEG_RWYT_PENGHUNI";

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
            PU = new FrmPemakai("input");
            
            this.PU.saveForm = new SaveFrmPemakai(this.saveFrmPemakai);
            this.PU.Table_foto = "M_KRMH_NEG_RWYT_PENGHUNI_FOTO";
            this.PU.P_ID_TABLE = "ID_KRMH_RWYT_PEMAKAI";
            StatusCrud = "input";
            PU.ShowDialog();

        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    PU = new FrmPemakai("detail");
                    StatusCrud = "detail";
                }
                else
                {
                    PU = new FrmPemakai("edit");
                    StatusCrud = "edit";
                }
                this.PU.resetForm = new ResetFrmPemakai(this.resetFrmPemakai);
                this.PU.saveForm = new SaveFrmPemakai(this.saveFrmPemakai);
                this.PU.Table_foto = "M_KRMH_NEG_RWYT_PENGHUNI_FOTO";
                this.PU.P_ID_TABLE = "ID_KRMH_RWYT_PEMAKAI";
                this.PU.ID_DETAIL = this.selectedData.ID_KRMH_RWYT_PEMAKAI;
                this.resetFrmPemakai("reset");
                PU.ShowDialog();
            }
        }

        private void resetFrmPemakai(string text)
        {
            this.PU.teFileName.Text = selectedData.NMFILE;
            this.PU.teKET.Text = selectedData.KET;
            this.PU.teNamaPMK.Text = selectedData.NM_PMK;
            this.PU.teNIP.Text = selectedData.NIP_PMK;
            this.PU.teTgl_SIP.Text = konfigApp.DateToString(selectedData.TGL_SIP);
            this.PU.teTglMulai.Text = konfigApp.DateToString(selectedData.TGL_MULAI);
            this.PU.teTglSelesai.Text = konfigApp.DateToString(selectedData.TGL_SELESAI);
            this.PU.teUr_Satker.Text = selectedData.UR_SATKER;
            this.PU.teJabFungsional.Text = selectedData.JAB_FUNGSIONAL;
            this.PU.teJabStr.Text = selectedData.JAB_STR;
            this.PU.teNilaiSewa.Value = (selectedData.NILAI == null) ? 0 : (decimal)selectedData.NILAI;
            this.PU.teNoKTP.Text = selectedData.NO_KTP;
            this.PU.teNomorSuratIjin.Text = selectedData.NO_SIP;
            this.PU.teTglLahir.Text = konfigApp.DateToString(selectedData.TGL_LAHIR);
            this.PU.teTglPensiun.Text = konfigApp.DateToString(selectedData.TGL_PENSIUN);
            this.PU.teTglSK.Text = konfigApp.DateToString(selectedData.TGL_SK);
            this.PU.teTmptLahir.Text = selectedData.TMP_LAHIR;
            this.PU.teTmtJab.Text = konfigApp.DateToString(selectedData.TMT_JAB);
            this.PU.ID_SATKER = selectedData.ID_SATKER;
            this.PU.teGolongan.Text = selectedData.GOLONGAN;
            this.PU.teKodeUnitSatker.Text = selectedData.KD_SATKER;
            this.PU.teAlamat.Text = selectedData.ALM_PMK;
            this.PU.ID_DETAIL = selectedData.ID_KRMH_RWYT_PEMAKAI;
            this.PU.ID_PHOTO = selectedData.PHOTO_EXISTS;
            this.PU.ResetFoto();
          

        }

        SvcHuniRmhngrCrud.call_pttClient svcRiwayatPemakaiCrud;
        SvcHuniRmhngrCrud.OutputParameters outRiwayatPemakaiCrud;

        private void saveFrmPemakai(string text)
        {
            this.PU.nonAktifkanForm("nonaktif");
            try
            {
                if (this.modeCrud != 'D')
                {
                    myThread = new Thread(new ThreadStart(this.PU.ShowProgresBar));
                }
                else
                {
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                }

                myThread.Start();
          
                SvcHuniRmhngrCrud.InputParameters parInp = new SvcHuniRmhngrCrud.InputParameters();
                parInp.P_ID_KRMH_NEG = this.ID_KRMH_NEG;
                parInp.P_ID_KRMH_RWYT_PEMAKAI = (this.PU.STATUS == "input") ? 0 : this.selectedData.ID_KRMH_RWYT_PEMAKAI;
                parInp.P_ID_KRMH_RWYT_PEMAKAISpecified = true;
                parInp.P_ID_KRMH_NEGSpecified = true;
                parInp.P_ID_MUTASI_DTL = null;
                parInp.P_ID_MUTASI_DTLSpecified = true;
                
                  parInp.P_ID_SATKER = konfigApp.DecimaltoNull(this.PU.ID_SATKER);
                
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_JAB_FUNGSIONAL = this.PU.teJabFungsional.Text;
                parInp.P_ALM_PMK = this.PU.teAlamat.Text;
                parInp.P_GOLONGAN = this.PU.teGolongan.Text;
                parInp.P_JAB_STR = this.PU.teJabStr.Text;
                parInp.P_KET = this.PU.teKET.Text;
                parInp.P_NILAI = this.PU.teNilaiSewa.Value;
                parInp.P_NILAISpecified = true;
                parInp.P_NIP_PMK = this.PU.teNIP.Text;
                parInp.P_NM_PMK = this.PU.teNamaPMK.Text;
                parInp.P_NO_KTP = this.PU.teNoKTP.Text;
                parInp.P_NO_SIP = this.PU.teNomorSuratIjin.Text;
                parInp.P_TGL_LAHIR = this.PU.teTglLahir.Text;
                parInp.P_TGL_MULAI = this.PU.teTglMulai.Text;
                parInp.P_TGL_PENSIUN = this.PU.teTglPensiun.Text;
                parInp.P_TGL_SELESAI = this.PU.teTglSelesai.Text;
                parInp.P_TGL_SIP = this.PU.teTgl_SIP.Text;
                parInp.P_TGL_SK = this.PU.teTglSK.Text;
                parInp.P_TMP_LAHIR = this.PU.teTmptLahir.Text;
                parInp.P_TMT_JAB = this.PU.teTmtJab.Text;
              
                parInp.P_NMFILE = this.PU.teFileName.Text;
                

                if (this.PU.STATUS == "input")
                {
                    parInp.P_SELECT = "C";
                }
                else
                {
                    parInp.P_SELECT = "U";
                }


                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                this.svcRiwayatPemakaiCrud = new SvcHuniRmhngrCrud.call_pttClient();
                svcRiwayatPemakaiCrud.Open();
                this.svcRiwayatPemakaiCrud.Beginexecute(parInp, new AsyncCallback(crudRiwayatPemakai), "");
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
        public void crudRiwayatPemakai(IAsyncResult result)
        {
            try
            {
                outRiwayatPemakaiCrud = svcRiwayatPemakaiCrud.Endexecute(result);
                svcRiwayatPemakaiCrud.Close();
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
                if (String.Compare(outRiwayatPemakaiCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    this.Invoke(new UbahDsDetail(this.ubahDsDetail), outRiwayatPemakaiCrud);
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

        public delegate void UbahDsDetail(SvcHuniRmhngrCrud.OutputParameters outCrud);
  
        public void ubahDsDetail(SvcHuniRmhngrCrud.OutputParameters outCrud)
        {
            SvcHuniRmhngrSelect.BPSIMANSROW_KRMH_NEG_RWYT_PENGHUNI dataPenyama = new SvcHuniRmhngrSelect.BPSIMANSROW_KRMH_NEG_RWYT_PENGHUNI();

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_ASET = (this.modeCrud == 'U') ? selectedData.ID_ASET : 0;
            dataPenyama.ID_ASETSpecified = true;
            dataPenyama.ID_KRMH_NEG = (this.modeCrud == 'U') ? selectedData.ID_KRMH_NEG : 0;
            dataPenyama.ID_KRMH_RWYT_PEMAKAI = outCrud.PO_ID_KRMH_RWYT_PEMAKAI;
            dataPenyama.ID_KRMH_RWYT_PEMAKAISpecified = true;
            dataPenyama.ID_KRMH_NEGSpecified = true;
            dataPenyama.ID_MUTASI_DTL = (this.modeCrud == 'U') ? selectedData.ID_MUTASI_DTL : 0;
            dataPenyama.ID_MUTASI_DTLSpecified = true;
            dataPenyama.KD_BRG = (this.modeCrud == 'U') ? selectedData.KD_BRG : "";
            id_penyama = dataPenyama.ID_KRMH_RWYT_PEMAKAI;

            if (this.modeCrud != 'D')
            {
                this.PU.ID_SATKER = outCrud.PO_ID_KRMH_RWYT_PEMAKAI;
                
            }
            this.ID_KRMH_RWYT_PEMAKAI = (this.StatusCrud == "edit")? selectedData.ID_KRMH_RWYT_PEMAKAI: outCrud.PO_ID_KRMH_RWYT_PEMAKAI;
            if (this.selectedData != null)
            {
                this.NUM = selectedData.NUM;
            }
            switch (this.modeCrud)
            {
                case 'C':


                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.PU.FilePath != null)
                    {
                        string filePath = this.PU.FilePath;
                        Foto_disimpan = false;
                        simpanFile("ID_KRMH_RWYT_PEMAKAI", dataPenyama.ID_KRMH_RWYT_PEMAKAI, "M_KRMH_NEG_RWYT_PENGHUNI", filePath, "C", PU.ID_JNSDOK);

                    }
                    else if (this.PU.FileFotoPath != null)
                    {
                      string filePath = this.PU.FileFotoPath;
                      string SELECT = "C";

                      this.Foto_disimpan = true;
                      simpanFile("ID_KRMH_RWYT_PEMAKAI", dataPenyama.ID_KRMH_RWYT_PEMAKAI, "M_KRMH_NEG_RWYT_PENGHUNI_FOTO", filePath, SELECT, PU.ID_JNSDOK);
                    }
                    else
                    {
                        this.PU.Close();
                        this.dataInisial = false;
                        this.getById = true;
                        this.getInitPemakaiAngk(" ID_KRMH_RWYT_PEMAKAI = " + this.ID_KRMH_RWYT_PEMAKAI.ToString());
                    }
                    //this.binder.Add(dataPenyama);
                    break;
                case 'U':

                    this.binder.Remove(this.selectedData);
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.PU.FilePath != null)
                    {

                        string filePath = this.PU.FilePath;
                        string SELECT = "C";
                        if (selectedData.FILE_EXISTS != 0)
                        {
                            SELECT = "U";
                        }
                        Foto_disimpan = false;
                        simpanFile("ID_KRMH_RWYT_PEMAKAI", dataPenyama.ID_KRMH_RWYT_PEMAKAI, "M_KRMH_NEG_RWYT_PENGHUNI", filePath, SELECT, PU.ID_JNSDOK);
                       
                    }
                    else if (this.PU.FileFotoPath != null)
                    {
                      string filePath = this.PU.FileFotoPath;
                      string SELECT = "C";
                      if (this.PU.DaftarFoto.PHOTO.Length > 0)
                      {
                        SELECT = "U";
                      }
                      Foto_disimpan = true;
                      simpanFile("ID_KRMH_RWYT_PEMAKAI", dataPenyama.ID_KRMH_RWYT_PEMAKAI, "M_KRMH_NEG_RWYT_PENGHUNI_FOTO", filePath, SELECT, PU.ID_JNSDOK);

                    }
                    else
                    {
                        this.PU.Close();
                        this.dataInisial = false;
                        this.getById = true;
                        this.getInitPemakaiAngk(" ID_KRMH_RWYT_PEMAKAI = " + this.ID_KRMH_RWYT_PEMAKAI.ToString());
                    }
                    
                    break;
                case 'D':
                     this.binder.Remove(selectedData);
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
        private bool Foto_disimpan;
        private void crudDokAset(SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud)
        {
            if (dataoutDokAsetCrud.PO_RESULT == "Y")
            {

                if (this.modeCrud != 'D')
                {
                    if (this.PU.FileFotoPath != null && Foto_disimpan == false)
                    {
                        string filePath = this.PU.FileFotoPath;
                        string SELECT = "C";
                        //if (this.PU.DaftarFoto.PHOTO.Length > 0)
                        if (this.PU.DaftarFoto.PHOTO != null)
                        {
                            SELECT = "U";
                        }
                        Foto_disimpan = true;
                        simpanFile("ID_KRMH_RWYT_PEMAKAI", this.id_penyama, "M_KRMH_NEG_RWYT_PENGHUNI_FOTO", filePath, SELECT);
                        
                    }
                    else
                    {
                        MessageBox.Show(dataoutDokAsetCrud.PO_RESULT_MESSAGE);
                        this.PU.Close();
                        this.Foto_disimpan = false; 
                        this.dataInisial = false;
                        this.getById = true;
                        this.getInitPemakaiAngk(" ID_KRMH_RWYT_PEMAKAI = " + this.ID_KRMH_RWYT_PEMAKAI.ToString());
                    }

                }
                else
                {
                    MessageBox.Show(dataoutDokAsetCrud.PO_RESULT_MESSAGE);
                    this.Foto_disimpan = false; 
                }
            }

        }
        #endregion

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            initGrid();
            getInitPemakaiAngk();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            getInitPemakaiAngk();
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
                    SvcHuniRmhngrCrud.InputParameters parInp = new SvcHuniRmhngrCrud.InputParameters();
                    parInp.P_ID_KRMH_NEG = selectedData.ID_KRMH_NEG;
                    parInp.P_ID_KRMH_RWYT_PEMAKAI = selectedData.ID_KRMH_RWYT_PEMAKAI;
                    parInp.P_ID_KRMH_RWYT_PEMAKAISpecified = true;
                    parInp.P_ID_KRMH_NEGSpecified = true;

                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcRiwayatPemakaiCrud = new SvcHuniRmhngrCrud.call_pttClient();
                    svcRiwayatPemakaiCrud.Open();
                    svcRiwayatPemakaiCrud.Beginexecute(parInp, new AsyncCallback(crudRiwayatPemakai), "");
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
                this.getInitPemakaiAngk(get_where_clause(nama_kolom, opr, parameter, parameter_2));

               
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
                case "NIP":
                    where = "Upper(NIP_PMK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nama Pegawai":
                    where = "Upper(NM_PMK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Unit Pegawai":
                    where = "Upper(UR_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl Surat Ijin Pegawai (SIP)":
                    where = "Upper(TGL_SIP) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Tgl Mulai Menghuni":
                    where = "Upper(TGL_MULAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Tgl Selesai Menghuni":
                    where = "Upper(TGL_SELESAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "File (Dok)":
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
                selectedData = (SvcHuniRmhngrSelect.BPSIMANSROW_KRMH_NEG_RWYT_PENGHUNI)selectedView.GetRow(e.FocusedRowHandle);
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
                case "Tgl Surat Pemakai":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Tgl Mulai":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Tgl Selesai":
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
