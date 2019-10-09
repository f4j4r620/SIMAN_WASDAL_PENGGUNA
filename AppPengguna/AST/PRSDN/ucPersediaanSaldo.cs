using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.PRSDN
{
    class ucPersediaanSaldo : UserControlDetail
    {
        private SvcPersediaanSaldo.call_pttClient fetchData;
        private SvcPersediaanSaldo.InputParameters parInp;
        private SvcPersediaanSaldo.OutputParameters outDat;
        private SvcPersediaanSaldo.BPSIMANSROW_SEDIA_SALDO selectedData;
        //public NonAktifkanFormSatker nonAktifForm;
        //public AktifkanFormSatker aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        SvcPersediaanSaldo.BPSIMANSROW_SEDIA_SALDO newRow;
        private ColumnView View;

        private decimal? ID_SEDIA;

        private FrmKoorSatker targetPanel;

        private string coba;
        //child form customization 
        private void initPersediaanSaldo()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcPersediaanSaldo.BPSIMANSROW_SEDIA_SALDO);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            this.setKolom("No", "NUM", "NUM", 0, false, 80);
            this.setKolom("Tahun Anggaran", "THN_ANG", "THN_ANG", 1,true);
            this.setKolom("Periode", "PERIODE", "PERIODE", 2, true);
            this.setKolom("Saldo Awal", "SALDO_AWAL", "SALDO_AWAL", 3, true, 120, "string",false);

            this.setKolom("Mutasi Masuk", "MUTASI_MASUK", "MUTASI_MASUK", 4, true, 100, "integer", false);
            this.setKolom("Mutasi Keluar", "MUTASI_KELUAR", "MUTASI_KELUAR", 5, true, 100, "integer", false);
            this.setKolom("Saldo Akhir", "SALDO_AKHIR", "SALDO_AKHIR", 6, true, 120, "integer", false);

            this.gridDoubleClickDetail = true;
            this.show_record = true;
        }




    

        //bila di pasang pada detail Master
        public string Status;
        public ucPersediaanSaldo(decimal? _ID_SEDIA = null, string _status = "detail")
            : base()
        {


            this.ID_SEDIA = _ID_SEDIA;
            this.Status = _status;
            initPersediaanSaldo();
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
        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.gvUcDtl.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvUcDtl_InitNewRow);
           
            this.initGrid();
            this.getInitPersediaanSaldo();

        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcPersediaanSaldo.BPSIMANSROW_SEDIA_SALDO)selectedView.GetRow(e.RowHandle);

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getInitPersediaanSaldo();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitPersediaanSaldo();
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
                this.getInitPersediaanSaldo(get_where_clause(nama_kolom, opr, parameter, parameter_2));
            
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
                case "Tahun Anggaran":
                    where = "Upper(THN_ANG) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Periode":
                    where = "Upper(PERIODE) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Saldo Awal":
                    where = "Upper(SALDO_AWAL) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Mutasi Masuk":
                    where = "Upper(MUTASI_MASUK) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Mutasi Keluar":
                    where = "Upper(MUTASI_KELUAR) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Saldo Akhir":
                    where = "Upper(SALDO_AKHIR) " + get_operator("Number", opr, parameter, parameter2);
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

        }


        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcPersediaanSaldo.BPSIMANSROW_SEDIA_SALDO)selectedView.GetRow(e.FocusedRowHandle);
                if (selectedView.IsLastRow)
                {
                    LastRow = true;
                }
                else
                {
                    LastRow = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
                //this.ID_ESELON1 = SelectedData.ID_ESELON1;
                //this.DataName = SelectedData.UR_ESELON1;
                // System.Diagnostics.Debug.WriteLine("hahahah", this.KdKorWil);
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

        protected void getInitPersediaanSaldo(string _where = null)
        {
          
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcPersediaanSaldo.InputParameters parInp = new SvcPersediaanSaldo.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_SEDIA = {0} {1}", this.ID_SEDIA, _where);

            fetchData = new SvcPersediaanSaldo.call_pttClient(konfigApp.SvcPersediaanSaldo_name,konfigApp.SvcPersediaanSaldo_address);
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

        private delegate void ShowData(SvcPersediaanSaldo.OutputParameters dataOut);
        private string StatusCrud = "";
        private decimal? NUM;
        public void showData(SvcPersediaanSaldo.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_SEDIA_SALDO.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_SEDIA_SALDO[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_SEDIA_SALDO[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_SEDIA_SALDO[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_SEDIA_SALDO[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_SEDIA_SALDO[i]);
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
