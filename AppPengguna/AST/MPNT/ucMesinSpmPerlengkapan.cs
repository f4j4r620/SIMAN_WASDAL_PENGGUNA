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

namespace AppPengguna.AST.MPNT
{
    class ucMesinSpmPerlengkapan : UserControlDetail
    {

        private SvcMesinPntPerlengkapanSelect.call_pttClient fetchData;
        private SvcMesinPntPerlengkapanSelect.InputParameters parInp;
        private SvcMesinPntPerlengkapanSelect.OutputParameters outData;
        public SvcMesinPntPerlengkapanSelect.BPSIMANSROW_KALB_FAS_PENUNJANG selectedData;

        private String Status;
        public decimal? IdKmpnt;

        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private PuMesinPNTPerlengkapan PuMesinPNTPerlengkapan;

        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------

        public ucMesinSpmPerlengkapan(decimal? _ID_KALB, String status)
            : base()
        {
            this.InitDokKib();
            this.Status = Status;
            this.IdKmpnt = _ID_KALB;

            if (Status == "detail")
            {
                this.bbTambah.Enabled = true;
                this.bbHapus.Enabled = true;
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
            this.getInitPerlengkapan();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        public void InitDokKib()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcMesinPntPerlengkapanSelect.BPSIMANSROW_KALB_FAS_PENUNJANG);
            this.gcUcDtl.DataSource = binder;

            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("Nama Fasilitas", "NM_FASILITAS", "NM_FASILITAS", 1, true);
            this.setKolom("Keterangan", "KET", "KET", 2, true);
            
            //this.SetGridSize(width, 0);
            gridDoubleClickDetail = true;

           
            this.show_record = true;
        }



        #region load data
        public void getInitPerlengkapan(string _where = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcMesinPntPerlengkapanSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KALB = {0} {1}", this.IdKmpnt, _where);
            parInp.P_COUNT = "Y";
            Console.WriteLine(parInp.STR_WHERE);
            fetchData = new SvcMesinPntPerlengkapanSelect.call_pttClient();
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
                MessageBox.Show("Load data Perlengkapan gagal", konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcMesinPntPerlengkapanSelect.OutputParameters dataOut);
        public decimal? NUM;
        private string StatusCrud = "";
        public void showData(SvcMesinPntPerlengkapanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KALB_FAS_PENUNJANG.Count();
            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KALB_FAS_PENUNJANG[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KALB_FAS_PENUNJANG[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    //serviceOutPut.SF_ROW_KALB_FAS_PENUNJANG[0].TOTAL_DATASpecified = true;
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KALB_FAS_PENUNJANG[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KALB_FAS_PENUNJANG[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_KALB_FAS_PENUNJANG[i]);
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
            this.PuMesinPNTPerlengkapan = new PuMesinPNTPerlengkapan(this, "input");
            StatusCrud = "input";
            this.PuMesinPNTPerlengkapan.ShowDialog();
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
                this.PuMesinPNTPerlengkapan = new PuMesinPNTPerlengkapan(this, "detail");
                StatusCrud = "detail";
            }
            else
            {
                this.PuMesinPNTPerlengkapan = new PuMesinPNTPerlengkapan(this, "edit");
                StatusCrud = "edit";
            }
            this.PuMesinPNTPerlengkapan.ShowDialog();
        }
        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData == null)
            {
                return;
            }
            StatusCrud = "hapus";
            this.PuMesinPNTPerlengkapan = new PuMesinPNTPerlengkapan(this, "hapus");
            this.PuMesinPNTPerlengkapan.Size = new System.Drawing.Size(0, 0);
            this.PuMesinPNTPerlengkapan.Opacity = 0;
            this.PuMesinPNTPerlengkapan.Show();
            this.PuMesinPNTPerlengkapan.Hide();
            this.PuMesinPNTPerlengkapan.hapusData();

        }
        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitPerlengkapan();
        }
        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.pencarian = false;
            this.initGrid();
            this.getInitPerlengkapan();
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
                this.getInitPerlengkapan(get_where_clause(nama_kolom, opr, parameter, parameter_2));

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

                case "Nama Fasilitas":
                    where = "UPPER(NM_FASILITAS) " + get_operator("String", opr, parameter, parameter2);
                    break;
               
                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcMesinPntPerlengkapanSelect.BPSIMANSROW_KALB_FAS_PENUNJANG)viewTerpilih.GetRow(e.FocusedRowHandle);
                if (viewTerpilih.IsLastRow)
                {
                    this.rowTerakhir = true;
                }
                else
                {
                    this.rowTerakhir = false;
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
                case "Tanggal Dokumen":
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
