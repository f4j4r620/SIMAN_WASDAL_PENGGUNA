using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraSplashScreen;
using AppPengguna.AST.PUASET;

namespace AppPengguna.AST.SJT
{
    class ucRmutasiSenjata : UserControlDetail
    {


        private frmMutasi puMutasi;
        public ArrayList dataGrid;
        private Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private String Status;
        public decimal? ID_KSENJ;



        private SvcSenjataRmutasiSelect.call_pttClient fetchData;
        private SvcSenjataRmutasiSelect.InputParameters parInp;
        private SvcSenjataRmutasiSelect.OutputParameters outData;
        public SvcSenjataRmutasiSelect.BPSIMANSROW_KSENJ_RWYT_MUTASI selectedData;



        public ucRmutasiSenjata(decimal? _ID_KSENJ, String Status)
            : base()
        {
            this.initTnhRMutasi();
            this.Status = Status;
            this.ID_KSENJ = _ID_KSENJ;


            this.bbEdit.Caption = "Detail";
            this.bbEdit.Glyph = global::AppPengguna.Properties.Resources.tombol_detail16;
            this.bbTambah.Enabled = false;
            this.bbHapus.Enabled = false;
            this.bbRefresh.Enabled = true;
            this.bbMore.Enabled = true;
        }

        private void initTnhRMutasi()
        {

            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcSenjataRmutasiSelect.BPSIMANSROW_KSENJ_RWYT_MUTASI);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("Tgl. Buku", "TGL_BUKU", "TGL_BUKU", 1, true, 120, "date");
            this.setKolom("Jns. Trans", "JNS_TRN", "JNS_TRN", 2, true);
            this.setKolom("Uraian Transaksi", "UR_TRN", "UR_TRN", 3, true);
            this.setKolom("Kuantitas", "KUANTITAS", "KUANTITAS", 4, true);
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
        }

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.search = "";
            this.initGrid();
            this.getInitTnhRMutasi();
        }

        #region load data
        public void getInitTnhRMutasi(string _where = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");

            parInp = new SvcSenjataRmutasiSelect.InputParameters();
            parInp.P_COL = "";
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
            if (this.dataInisial == true)
            {
                this.search = (_where != null) ? " AND " + _where : "";
            }

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = String.Format(" ID_KSENJ = {0} {1}", this.ID_KSENJ, this.search);
            fetchData = new SvcSenjataRmutasiSelect.call_pttClient();
            fetchData.Open();
         
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outData = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowData(this.showData), this.outData);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcSenjataRmutasiSelect.OutputParameters dataOut);

        public void showData(SvcSenjataRmutasiSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KSENJ_RWYT_MUTASI.Count();

            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KSENJ_RWYT_MUTASI[0].TOTAL_DATA.ToString();
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
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KSENJ_RWYT_MUTASI[0].TOTAL_DATA.ToString();
                }
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_KSENJ_RWYT_MUTASI[i]);
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
        #endregion

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {

        }

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        protected override void gvUcDtl_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcSenjataRmutasiSelect.BPSIMANSROW_KSENJ_RWYT_MUTASI)selectedView.GetRow(e.FocusedRowHandle);
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
            string nama_kolom = this.LuKolom.EditValue.ToString();
            this.LuKolom.Width = 40 + (((int)nama_kolom.Length) * 5);
            switch (nama_kolom)
            {
                case "Tgl. Buku":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Tgl. Dasar Mutasi":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }

        #region button
        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                puMutasi = new frmMutasi(selectedData, "detail");
                puMutasi.ShowDialog();
            }

        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitTnhRMutasi();
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.pencarian = false;
            this.initGrid();
            this.getInitTnhRMutasi();
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
                this.pencarian = true;
                this.dataInisial = true;
                this.getInitTnhRMutasi(get_where_clause(nama_kolom, opr, parameter, parameter_2));

            }
            catch (Exception)
            {

                MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
                this.aktifkanForm("");
            }
        }
        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion
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
    }
}
