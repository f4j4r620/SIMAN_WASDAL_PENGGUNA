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
    public class UcMutRmhNgr:UserControlDetail
    {
        private SvcRwyMutRmhNgrSelect.call_pttClient fetchData;
        private SvcRwyMutRmhNgrSelect.InputParameters parInp;
        private SvcRwyMutRmhNgrSelect.OutputParameters outDat;
        private SvcRwyMutRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_MUTASI selectedData;
        //private SvcLokTnhSelect.BPSIMANSROW_M_KTNH_LOKASI newRow;
        //private SvcLokTnhCrud.call_pttClient crudData;
        //private SvcLokTnhCrud.InputParameters inputCrud;
        //private SvcLokTnhCrud.OutputParameters outCrud;

        private frmMutasi puMutasi;
        private bool isSearch = false;
        private bool initiated = false;

        public bool Initiated
        {
            get 
            {
                return initiated; 
            }

            set
            {
                initiated = value;
            }
        }


  
        public decimal? ID_KRMH_NEG;
        public string Status;
            public UcMutRmhNgr(decimal? _ID_KRMH_NEG, string _status)
            : base()
        {
            this.ID_KRMH_NEG =_ID_KRMH_NEG;
            this.Status = _status;
            this.bbEdit.Caption = "Detail";
            this.bbEdit.Glyph = global::AppPengguna.Properties.Resources.tombol_detail16;
            this.bbTambah.Enabled = false;
            this.bbHapus.Enabled = false;
            this.bbRefresh.Enabled = true;
            this.bbMore.Enabled = true;
        }
   
        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcRwyMutRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_MUTASI);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("Tgl. Buku", "TGL_BUKU", "TGL_BUKU", 1, true, 120, "date");
            this.setKolom("Jns. Trans", "JNS_TRN", "JNS_TRN", 2, true);
            this.setKolom("Uraian Transaksi", "UR_TRN", "UR_TRN", 3, true);
            this.setKolom("Kuantitas", "KUANTITAS", "KUANTITAS", 4, true, 100, "string", false, "right");
            this.setKolom("Satuan", "SATUAN", "SATUAN", 5, true);
            this.setKolom("Nilai", "NILAI", "NILAI", 6, true, 120, "number");
            this.setKolom("Intra/Extra", "INTRA_EXTRA", "INTRA_EXTRA", 7, true);
            this.setKolom("No Dasar Mutasi", "NO_DSR_MUTASI", "NO_DSR_MUTASI", 8, true);
            this.setKolom("Tgl. Dasar Mutasi", "TGL_DSR_MUTASI", "TGL_DSR_MUTASI", 9, true, 100, "date");
            this.setKolom("Keterangan", "KET", "KET", 10, true);
            this.setKolom("Terakhir", "TERAKHIR_YN", "TERAKHIR_YN", 10, false, 100, "string", true);

            this.gridDoubleClickDetail = true;
            this.ShowFooter(true);
            this.SetSummary(5, "", "Total", "T O T A L");

            this.SetSummary(6, "TOTAL_NILAI", "SumTotal");
            this.show_record = true;
            this.initGrid();
            this.getInitKonsRmh();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        private bool moreCari = false;
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
                this.getInitKonsRmh(get_where_clause(nama_kolom, opr, parameter, parameter_2));
               
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
                case "Jns. Trans":
                    where = "Upper(JNS_TRN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl. Buku":
                    where = "Upper(TGL_BUKU) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Kuantitas":
                    where = "Upper(KUANTITAS) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Satuan":
                    where = "Upper(SATUAN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Intra/Ekstra":
                    where = "Upper(INTRA_EXTRA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nilai":
                    where = "Upper(NILAI) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "No Dasar Mutasi":
                    where = "Upper(NO_DSR_MUTASI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl. Dasar Mutasi":
                    where = "Upper(TGL_DSR_MUTASI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Ket":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }

        private string Search;
        public void getInitKonsRmh(string _where = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcRwyMutRmhNgrSelect.InputParameters parInp = new SvcRwyMutRmhNgrSelect.InputParameters();
            parInp.P_COL = "TGL_BUKU, JNS_TRN";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            if (this.dataInisial == true)
            {
                this.search = (_where != null) ? " AND " + _where : "";
            }
           
            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.STR_WHERE = String.Format(" ID_KRMH_NEG = {0} {1}", this.ID_KRMH_NEG, this.Search);
            parInp.P_SORT = "ASC";
            fetchData = new SvcRwyMutRmhNgrSelect.call_pttClient();
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

        private delegate void ShowData(SvcRwyMutRmhNgrSelect.OutputParameters dataOut);

        public void showData(SvcRwyMutRmhNgrSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI.Count();

            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[0].TOTAL_DATA.ToString();
                }
                else
                {
                    StrTotalGrid.Caption = "0";
                    StrTotalDb.Caption = "0";
                }
            }
            else
            {
                if (jmlDataGroup > 0)
                {

                    StrTotalGrid.Caption = (Convert.ToInt64(StrTotalGrid.Caption) + jmlDataGroup).ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[0].TOTAL_DATA.ToString();
                }
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[i].TGL_BUKU).Substring(0, 10);
              string date_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[i].TGL_BUKU).Substring(0, 8);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[i].TGL_DSR_MUTASI).Substring(0, 10);
              string date2_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[i].TGL_DSR_MUTASI).Substring(0, 8);

              if (date == "11/11/1000" || date_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[i].TGL_BUKU = null;
              }
              if (date2 == "11/11/1000" || date2_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[i].TGL_DSR_MUTASI = null;
              }
                binder.Add(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_MUTASI[i]);
            }

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
            this.gvUcDtl.BestFitColumns();

        }

       

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        { 
        }

        private FrmMutRmhNgr frmMut;
        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmMut = new FrmMutRmhNgr(this);
            frmMut.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                puMutasi = new frmMutasi(selectedData, "detail");
                puMutasi.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            moreCari = false;
            initGrid();
            getInitKonsRmh();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.dataInisial = false;
            getInitKonsRmh();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

       

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
        }

        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcRwyMutRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_MUTASI)selectedView.GetRow(e.FocusedRowHandle);
                if (selectedView.IsLastRow)
                {
                    LastRow = true;
                }
                else
                {
                    LastRow = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();

            }
        }

       
        protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
        {
            moreCari = false;
        }
    }
}
