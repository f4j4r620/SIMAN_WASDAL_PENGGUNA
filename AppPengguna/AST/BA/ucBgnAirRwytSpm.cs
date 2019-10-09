using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Threading;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using AppPengguna.AST.PUASET;

namespace AppPengguna.AST.BA
{
    class ucBgnAirRwytSpm : UserControlDetail
    {
        private SvcBangunanAirSpmSelect.call_pttClient fetchData;
        private SvcBangunanAirSpmSelect.InputParameters parInp;
        private SvcBangunanAirSpmSelect.OutputParameters outData;
        public SvcBangunanAirSpmSelect.BPSIMANSROW_M_KBAIR_RWYT_SPM selectedData;

        private frmSpm puSpm;
        private String Status;
        decimal? IdKspm;
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        public ucBgnAirRwytSpm(decimal? _ID_KBAIR, String status)
            : base()
        {
            this.initSpm();
            this.Status = Status;
            this.IdKspm = _ID_KBAIR;

          
            this.bbEdit.Caption = "Detail";
            this.bbEdit.Glyph = global::AppPengguna.Properties.Resources.tombol_detail16;
           
            this.bbEdit.Enabled = true;
            this.bbRefresh.Enabled = true;
            this.bbMore.Enabled = true;
            this.bbTambah.Enabled = false;
            this.bbHapus.Enabled = false;
        }

        public void initSpm()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcBangunanAirSpmSelect.BPSIMANSROW_M_KBAIR_RWYT_SPM);
            this.gcUcDtl.DataSource = binder;

            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("No SPPA", "NO_SPPA", "NO_SPPA", 1, true);
            this.setKolom("Tanggal Buku", "TGL_BUKU", "TGL_BUKU", 2, true);
            this.setKolom("No SP2D", "NO_SP2D", "NO_SP2D", 3, true);
            this.setKolom("Tanggal SP2D", "TGL_SP2D", "TGL_SP2D", 4, true);
            this.setKolom("BKPK", "BKPK", "BKPK", 5, true);
            this.setKolom("Jumlah SPM", "JML_SPM", "JML_SPM", 6, true);
            this.setKolom("Kode Transaksi", "UR_TRN", "UR_TRN", 7, true);
            gridDoubleClickDetail = true;

        }


        protected override void ucDetail_Load(object sender, EventArgs e)
        {

            this.initGrid();
            this.getInitSpm();
        }
        #region load data
        public void getInitSpm(string _where = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcBangunanAirSpmSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KBAIR = {0} {1}", this.IdKspm, _where);
            parInp.P_COUNT = "Y";
            Console.WriteLine(parInp.STR_WHERE);
            fetchData = new SvcBangunanAirSpmSelect.call_pttClient();
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
            catch (Exception ex)
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show("Load data SPM gagal", konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcBangunanAirSpmSelect.OutputParameters dataOut);
        public decimal? NUM;
        private string StatusCrud = "";
        public void showData(SvcBangunanAirSpmSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM.Count();
            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    //serviceOutPut.SF_ROW_M_KBDG_DOK_KIB[0].TOTAL_DATASpecified = true;
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[i].TGL_BUKU).Substring(0, 10);
              string date_ = Convert.ToString(serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[i].TGL_BUKU).Substring(0, 8);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[i].TGL_SP2D).Substring(0, 10);
              string date2_ = Convert.ToString(serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[i].TGL_SP2D).Substring(0, 8);

              if (date == "11/11/1000" || date_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[i].TGL_BUKU = null;
              }
              if (date2 == "11/11/1000" || date2_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[i].TGL_SP2D = null;
              }
                binder.Add(serviceOutPut.SF_ROW_M_KBAIR_RWYT_SPM[i]);
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

        }
        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          if (selectedData != null) 
          {
            puSpm = new frmSpm(selectedData, "detail");
            puSpm.ShowDialog();
          }
        }
        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitSpm();
        }
        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.pencarian = false;
            this.initGrid();
            this.getInitSpm();
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
                this.getInitSpm(get_where_clause(nama_kolom, opr, parameter, parameter_2));

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

                case "No SPPA":
                    where = "UPPER(NO_SPPA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tanggal Buku":
                    where = "Upper(TGL_BUKU) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "No SP2D":
                    where = "Upper(NO_SP2D) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tanggal SP2D":
                    where = "Upper(TGL_SP2D) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "BKPK":
                    where = "Upper(BKPK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jumlah SPM":
                    where = "Upper(JML_SPM) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Kode Transaksi":
                    where = "Upper(UR_TRN) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcBangunanAirSpmSelect.BPSIMANSROW_M_KBAIR_RWYT_SPM)viewTerpilih.GetRow(e.FocusedRowHandle);
                if (viewTerpilih.IsLastRow)
                {
                    this.rowTerakhir = true;
                }
                else
                {
                    this.rowTerakhir = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();

                this.btnMap.Enabled = false;

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
                case "Tanggal Buku":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                case "Tanggal SP2D":
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
