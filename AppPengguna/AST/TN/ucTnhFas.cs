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

namespace AppPengguna.AST.TN
{
    class ucTnhFas : UserControlDetail
    {
     
        private ucTanahForm frmTanah;
        private frmTnhFasPenunjang frmTnhFas;

        public ArrayList dataGrid;
        private Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private String Status;
        public decimal? IdKtnh;
        private string search = "";

        private SvcTnhFasPenunjangSelect.call_pttClient fetchData;
        private SvcTnhFasPenunjangSelect.InputParameters parInp;
        private SvcTnhFasPenunjangSelect.OutputParameters outData;
        public SvcTnhFasPenunjangSelect.BPSIMANSROW_M_KTNH_FAS_PENUNJANG selectedData;
        public string StatusCrud = "";
        private decimal? NUM;
        public ucTnhFas(decimal? _ID_KTNH, String _status)
            : base()
        {
            this.initTnhFas();
            this.Status = _status;
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

        private void initTnhFas()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcTnhFasPenunjangSelect.BPSIMANSROW_M_KTNH_FAS_PENUNJANG);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, true);
            this.setKolom("Jaringan Listrik", "LISTRIK", "LISTRIK", 1, true);
            this.setKolom("Air Bersih", "PAM", "PAM", 2, true);
            this.setKolom("Jaringan Telpon", "TELPON", "TELPON", 3, true);
            this.setKolom("Jaringan Gas", "GAS", "GAS", 4, true);
            this.setKolom("Saluran Limbah", "SAL_LIMBAH", "SAL_LIMBAH", 5, true);
            this.setKolom("Fasilitas Lainnya", "LAINNYA", "LAINNYA", 6, true);
            int width = this.setKolom("Keterangan", "KET", "KET", 7, true);
            this.gridDoubleClickDetail = true;
            this.show_record = true;
        }

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.initGrid();
            this.getInitTnhFas();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;

        }


        #region load data
        public  void getInitTnhFas(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcTnhFasPenunjangSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KTNH = {0} {1}", this.IdKtnh, _where);
            fetchData = new SvcTnhFasPenunjangSelect.call_pttClient(konfigApp.SvcTnhFasSelect_name, konfigApp.SvcTnhFasSelect_address);
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
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowData(this.showData), this.outData);
            }
            catch
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcTnhFasPenunjangSelect.OutputParameters dataOut);

        public void showData(SvcTnhFasPenunjangSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG.Count();
            
            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KTNH_FAS_PENUNJANG[i]);
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
            this.frmTnhFas = new frmTnhFasPenunjang( this, "input");
            this.StatusCrud = this.frmTnhFas.status;
            this.frmTnhFas.ShowDialog();
        }
        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    this.frmTnhFas = new frmTnhFasPenunjang(this, "detail");
                }
                else
                {
                    this.frmTnhFas = new frmTnhFasPenunjang(this, "edit");
                }
                StatusCrud = this.frmTnhFas.status;
                this.frmTnhFas.ShowDialog();
            }
        }
        
        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                StatusCrud = "hapus";
                    this.frmTnhFas = new frmTnhFasPenunjang(this, "hapus");
                    this.StatusCrud = this.frmTnhFas.status;
                    this.frmTnhFas.Size = new System.Drawing.Size(0, 0);
                    this.frmTnhFas.Opacity = 0;
                    this.frmTnhFas.Show();
                    this.frmTnhFas.Hide();
                    this.frmTnhFas.hapusData();
               
            }
        }
        
        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitTnhFas();
        }
        
        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.initGrid();
            this.getInitTnhFas();
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
                this.getInitTnhFas(get_where_clause(nama_kolom, opr, parameter, parameter_2));

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
                case "Jaringan Listrik":
                    where = "Upper(LISTRIK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Air Bersih":
                    where = "Upper(PAM) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jaringan Telpon":
                    where = "Upper(TELPON) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jaringan Gas":
                    where = "Upper(GAS) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Saluran Limbah":
                    where = "Upper(SAL_LIMBAH) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Fasilitas Lainnya":
                    where = "Upper(LAINNYA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }
        #endregion
        protected override void gvUcDtl_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            viewTerpilih = sender as GridView;
            if (viewTerpilih.SelectedRowsCount > 0)
            {
                selectedData = (SvcTnhFasPenunjangSelect.BPSIMANSROW_M_KTNH_FAS_PENUNJANG)viewTerpilih.GetRow(e.FocusedRowHandle);
                if (viewTerpilih.IsLastRow)
                {
                    this.rowTerakhir = true;
                }
                else
                {
                    this.rowTerakhir = false;
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

                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }
        #region Teu Di Pake
        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {

        }

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }
        #endregion
    }
}
