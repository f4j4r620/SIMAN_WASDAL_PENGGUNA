using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using AppPengguna.AST.PUASET;

namespace AppPengguna.AST.BG
{
    class ucRmutasiBangunan : UserControlDetail
    {
        private SvcRmutasiBangunanSelect.call_pttClient fetchData;
        private SvcRmutasiBangunanSelect.InputParameters parInp;
        private SvcRmutasiBangunanSelect.OutputParameters outDat;
        private SvcRmutasiBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_MUTASI selectedData;
        //public NonAktifkanFormSatker nonAktifForm;
        //public AktifkanFormSatker aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        SvcRmutasiBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_MUTASI newRow;
        private ColumnView View;

        private decimal? ID_KBDG;
        private frmMutasi puMutasi;
        private string Status;
        public decimal? NUM;
        private string StatusCrud = "";
        private string coba;
        //child form customization 
        private void initRmutasiBangunan()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcRmutasiBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_MUTASI);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
          
            this.setKolom("Nomor", "NUM", "NUM", 0);
            this.setKolom("Tgl. Buku", "TGL_BUKU", "TGL_BUKU", 1, true, 120, "date");
            this.setKolom("Jns. Trans", "JNS_TRN", "JNS_TRN", 2, true);
            this.setKolom("Uraian Transaksi", "UR_TRN", "UR_TRN", 3, true);
            this.setKolom("Kuantitas", "KUANTITAS", "KUANTITAS", 4, true, 100, "string", true, "right");
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




        //bila di pasang pada detail Master
        public ucRmutasiBangunan(decimal? id_kbdg = null, string _status = "edit")
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KBDG = id_kbdg;
            this.Status = _status;
            initRmutasiBangunan();

            this.bbEdit.Caption = "Detail";
            this.bbEdit.Glyph = global::AppPengguna.Properties.Resources.tombol_detail16;
            this.bbTambah.Enabled = false;
            this.bbHapus.Enabled = false;
            this.bbRefresh.Enabled = true;
            this.bbMore.Enabled = true;
        }


        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.gvUcDtl.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvUcDtl_InitNewRow);
            

            this.initGrid();
            this.getInitRmutasiTanah();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcRmutasiBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_MUTASI)selectedView.GetRow(e.RowHandle);

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
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
            this.initGrid();
            this.getInitRmutasiTanah();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitRmutasiTanah();
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
                this.getInitRmutasiTanah(get_where_clause(nama_kolom, opr, parameter, parameter_2));
               
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
                case "ID Riwayat Mutasi":
                    where = "ID_KBDG_RWYT_MUTASI " + get_operator("Number", opr, parameter, parameter2);
                    break;
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
                selectedData = (SvcRmutasiBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_MUTASI)selectedView.GetRow(e.FocusedRowHandle);
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

        protected void getInitRmutasiTanah(string _where = null)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcRmutasiBangunanSelect.InputParameters parInp = new SvcRmutasiBangunanSelect.InputParameters();
            parInp.P_COL = " TGL_BUKU, TGL_DSR_MUTASI ";
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
            parInp.P_SORT = "ASC";
            parInp.STR_WHERE = "ID_KBDG = " + this.ID_KBDG + " " + this.search;

            fetchData = new SvcRmutasiBangunanSelect.call_pttClient(konfigApp.SvcRmutasiBangunanSelect_name,konfigApp.SvcRmutasiBangunanSelect_address);
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

        private delegate void ShowData(SvcRmutasiBangunanSelect.OutputParameters dataOut);

        public void showData(SvcRmutasiBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBDG_RWYT_MUTASI.Count();

            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_RWYT_MUTASI[0].TOTAL_DATA.ToString();
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
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_RWYT_MUTASI[0].TOTAL_DATA.ToString();
                }
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_MUTASI[i].TGL_BUKU).Substring(0, 10);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_RWYT_MUTASI[i].TGL_DSR_MUTASI).Substring(0, 10);

              if (date == "11/11/1000")
              {
                serviceOutPut.SF_ROW_M_KBDG_RWYT_MUTASI[i].TGL_BUKU = null;
              }
              if (date2 == "11/11/1000")
              {
                serviceOutPut.SF_ROW_M_KBDG_RWYT_MUTASI[i].TGL_DSR_MUTASI = null;
              }
                this.binder.Add(serviceOutPut.SF_ROW_M_KBDG_RWYT_MUTASI[i]);
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


        
    }
}
