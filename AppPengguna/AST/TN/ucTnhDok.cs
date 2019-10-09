﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraSplashScreen;
using AppPengguna.KSK.PL;

namespace AppPengguna.AST.TN
{
    class ucTnhDok : UserControlDetail
    {

        private ucTanahForm frmTanah;
        private frmTnhDok frmTnhDok;

        public ArrayList dataGrid;
        private Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;
       

        private String Status;
        public decimal? IdKtnh;
        private decimal? jmlData = 0;

        private SvcTnhDokSelect.call_pttClient fetchData;
        private SvcTnhDokSelect.InputParameters parInp;
        private SvcTnhDokSelect.OutputParameters outData;
        public SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK selectedData;

        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------


   
        public ucTnhDok(decimal? _ID_KTNH, String Status)
            : base()
        {
            this.initTnhDok();
            this.Status = Status;
            this.IdKtnh = _ID_KTNH;
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

        private void initTnhDok()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false);
           // this.setKolom("Kode", "KD_SMILIK", "KD_SMILIK", 1, true,100,"string",true);
            this.setKolom("Dokumen Kepemilikan", "UR_DOK", "UR_DOK", 2, true);
            this.setKolom("Jenis Dokumen Kepemilikan", "UR_SMILIK", "UR_SMILIK", 3, true);
            this.setKolom("Jenis Sertifikat", "NM_JNS_SERTI", "NM_JNS_SERTI", 4, true);
            this.setKolom("No. Dokumen", "NO_SERTIFIKAT", "NO_SERTIFIKAT", 5, true);
            this.setKolom("Tgl. Dokumen", "TGL_DOK", "TGL_DOK", 6, true, 100, "date");
            this.setKolom("Berlaku Sampai", "TGL_BERLAKU", "TGL_BERLAKU", 7, true, 100, "date");
            this.setKolom("Atas Nama", "ATAS_NAMA", "ATAS_NAMA", 8, true);
            this.setKolom("Instansi Penerbit", "PENERBIT", "PENERBIT", 9, true);
            this.setKolom("Keterangan", "KET_DOK", "KET_DOK", 10, true);
            this.setKolom("File", "NMFILE", "NMFILE", 12, false);
            //int width = this.setKolom("File", "NMFILE", "NMFILE", 12, false);
          
            //this.SetGridSize(width, 0);
            gridDoubleClickDetail = true;

            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);
            this.show_record = true;
        }

        #region View Dokumen
        protected void view_dokumen(object sender, ItemClickEventArgs e)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = selectedData.ID_KTNH_DOK;
                parInp.P_ID_TABLE = "ID_KTNH_DOK";
                parInp.P_IDSpecified = true;
                parInp.P_TABLE = "M_KTNH_DOK";
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
            int jmlDataGroup= serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlDataGroup > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
                System.IO.File.WriteAllBytes(selectedData.NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(selectedData.NMFILE);
                PuPdf.ShowDialog();
            }
        }
       

        #endregion//ViewDokumen

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            //this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tampilMap);
            this.initGrid();
            this.getInitTnhDok();
            if (jmlData == 0)
            {
                this.btnMap.Enabled = false;
            }
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }
 
        #region load data
        public void getInitTnhDok(string _where=null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcTnhDokSelect.InputParameters();
            parInp.P_COL = "";
            decimal Max, Min;
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
            parInp.STR_WHERE = String.Format(" ID_KTNH = {0} {1}", this.IdKtnh, _where);
            Console.WriteLine(parInp.STR_WHERE);
            fetchData = new SvcTnhDokSelect.call_pttClient(konfigApp.SvcTnhDokSelect_name,konfigApp.SvcTnhDokSelect_address);
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outData = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowData(this.showData), this.outData);
            }
            catch
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcTnhDokSelect.OutputParameters dataOut);
        public decimal? NUM;
        private string StatusCrud = "";
        public void showData(SvcTnhDokSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_ASET_M_KTNH_DOK.Count();
            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date1 = Convert.ToString(serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[i].TGL_DOK).Substring(0, 10);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[i].TGL_BERLAKU).Substring(0, 10);
              if (date1 == "11/11/1000" || date1 == "11/11/2011") 
                {
                    serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[i].TGL_DOK = null;
                }
              if (date2 == "11/11/1000" || date2 == "11/11/2011")
                {
                    serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[i].TGL_BERLAKU = null;
                }
                binder.Add(serviceOutPut.SF_ROW_ASET_M_KTNH_DOK[i]);
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
       
        #endregion

        #region button
        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.frmTnhDok = new frmTnhDok(this,"input");
            StatusCrud = "input";
            this.frmTnhDok.ShowDialog();
        }
        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData == null)
            {
                return;
            }
            this.NUM = selectedData.NUM;
            if (this.Status == "detail")
            {
                this.frmTnhDok = new frmTnhDok(this, "detail");
                StatusCrud = "detail";
            }
            else
            {
                this.frmTnhDok = new frmTnhDok(this, "edit");
                StatusCrud = "edit";
            }
            this.frmTnhDok.ShowDialog();
        }
        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData == null)
            {
                return;
            }
            StatusCrud = "hapus";
            this.frmTnhDok = new frmTnhDok(this, "hapus");
            this.frmTnhDok.Size = new System.Drawing.Size(0, 0);
            this.frmTnhDok.Opacity = 0;
            this.frmTnhDok.Show();
            this.frmTnhDok.Hide();
            this.frmTnhDok.hapusData();
              
        } 
        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitTnhDok();
        }
        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.pencarian = false;
            this.initGrid();
            this.getInitTnhDok();
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
                this.pencarian = true;
                this.getInitTnhDok(get_where_clause(nama_kolom, opr, parameter, parameter_2));

            }
            catch (Exception)
            {

                MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
                this.aktifkanForm("");
            }
            
            
        }
        #endregion
        private string get_where_clause(string nama_kolom, string opr, string parameter, string parameter2)
        {
            string where = "";
            switch (nama_kolom)
            {
               
                case "Kode":
                    where = "UPPER(KODE_SMILIK) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Dokumen Kepemilikan":
                    where = "Upper(UR_DOK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jenis Dokumen Kepemilikan":
                    where = "Upper(UR_SMILIK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jenis Sertifikat":
                    where = "Upper(NM_JNS_SERTI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "No. Dokumen":
                    where = "Upper(NO_SERTIFIKAT) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl. Dokumen":
                    where = "Upper(TGL_DOK) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Berlaku Sampai":
                    where = "Upper(TGL_BERLAKU) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Atas Nama":
                    where = "Upper(ATAS_NAMA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Instansi Penerbit":
                    where = "Upper(PENERBIT) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET_DOK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Dokumen Utama":
                    where = "Upper(DOK_UTAMA_YN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }

        protected override void gvUcDtl_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            viewTerpilih = sender as GridView;
            if (viewTerpilih.SelectedRowsCount > 0)
            {
                selectedData = (SvcTnhDokSelect.BPSIMANSROW_M_KTNH_DOK)viewTerpilih.GetRow(e.FocusedRowHandle);
                if (viewTerpilih.IsLastRow)
                {
                    this.rowTerakhir = true;
                }
                else
                {
                    this.rowTerakhir = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
                
                if (selectedData.FILE_EXISTS != 0)
                {
                    jmlData = viewTerpilih.SelectedRowsCount;
                    this.btnMap.Enabled = true;
                }
                else
                {
                    this.btnMap.Enabled = false;
                }
            }
        }


       
        #region Teu Di Pake
        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {

        }

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
        {
            string nama_kolom = this.LuKolom.EditValue.ToString();
            this.LuKolom.Width = 40 + (((int)nama_kolom.Length) * 5);
            switch (nama_kolom)
            {
                case "Tgl. Dokumen":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                case "Berlaku Sampai":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }
        #endregion
    }
}