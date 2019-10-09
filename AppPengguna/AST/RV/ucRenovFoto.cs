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
using AppPengguna.PU;
using AppPengguna.AST.RV;

namespace AppPengguna.AST.RV
{
    class ucRenovFoto : UserControlDetail
    {

        private SvcRenovasiFotoSelect.photoSelectGrid_pttClient fetchData;
        private SvcRenovasiFotoSelect.InputParameters parInp;
        private SvcRenovasiFotoSelect.OutputParameters outData;
        public SvcRenovasiFotoSelect.BPSIMANSROW_M_ASET_PHOTO_GRID selectedData;

        private String Status;
        public decimal? IdAset;
        private decimal? jmlData = 0;
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;
        public int urutan;
        public int JumlahFoto = 1;
        private frmRenovFoto frmRenovFoto;
        public List<datafoto> DafarFoto = new List<datafoto>();

        protected frmFoto PuFoto;



        public ucRenovFoto(decimal? _ID_ASET, String status)
            : base()
        {
            this.InitFoto();
            this.Status = Status;
            this.IdAset = _ID_ASET;

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
            this.getInitFoto();

            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        public void InitFoto()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcRenovasiFotoSelect.BPSIMANSROW_M_ASET_PHOTO_GRID);
            this.gcUcDtl.DataSource = binder;

            this.setKolom("No", "NUM", "NUM", 1, false);
            this.setKolom("Tanggal Upload", "CREATED", "CREATED", 2, true);
            this.setKolom("Keterangan", "KET_PHOTO", "KET_PHOTO", 3, true);
            this.setKolom("Nama File", "NM_PHOTO", "NM_PHOTO", 4, true);
            this.setKolom("Ukuran File (Mb)", "FILE_SIZE", "FILE_SIZE", 5, true);

            gridDoubleClickDetail = true;

            this.btnMap.Caption = "Lihat Foto";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.image_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_foto);
            this.show_record = true;
        }


        #region View Foto
        protected void view_foto(object sender, ItemClickEventArgs e)
        {

            this.PuFoto = new frmFoto(this.IdAset, this.urutan, this.JumlahFoto, this.DafarFoto, "singleImage", false, selectedData.ID_PHOTO);
            this.PuFoto.btnHapusFoto.Enabled = false;
            this.PuFoto.BtnTampilkanFoto.Enabled = false;
            this.PuFoto.BtnUnggahFoto.Enabled = false;
            this.PuFoto.ShowDialog();
        }


        #endregion



        #region load data
        public void getInitFoto(string _where = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcRenovasiFotoSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_ASET = {0} {1}", this.IdAset, _where);
            parInp.P_COUNT = "Y";
            Console.WriteLine(parInp.STR_WHERE);
            fetchData = new SvcRenovasiFotoSelect.photoSelectGrid_pttClient();
            fetchData.Open();
            fetchData.BeginphotoSelectGrid(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outData = fetchData.EndphotoSelectGrid(result);
                fetchData.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowData(this.showData), this.outData);
            }
            catch (Exception ex)
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show("Load data Foto gagal", konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcRenovasiFotoSelect.OutputParameters dataOut);
        public decimal? NUM;
        private string StatusCrud = "";
        public void showData(SvcRenovasiFotoSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID.Count();
            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    //serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[0].TOTAL_DATASpecified = true;
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                string date = Convert.ToString(serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[i].CREATED).Substring(0, 10);
                if (date == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[i].CREATED = null;
                }

                float? size = serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[i].FILE_SIZE;

                serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[i].FILE_SIZE = konfigApp.convertToMb(size);
                binder.Add(serviceOutPut.SF_ROW_M_ASET_PHOTO_GRID[i]);
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
            this.frmRenovFoto = new frmRenovFoto(this, "input");
            StatusCrud = "input";
            this.frmRenovFoto.ShowDialog();
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
                this.frmRenovFoto = new frmRenovFoto(this, "detail");
                StatusCrud = "detail";
            }
            else
            {
                this.frmRenovFoto = new frmRenovFoto(this, "edit");
                StatusCrud = "edit";
            }
            this.frmRenovFoto.ShowDialog();
        }
        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData == null)
            {
                return;
            }
            StatusCrud = "hapus";
            this.frmRenovFoto = new frmRenovFoto(this, "hapus");
            this.frmRenovFoto.Size = new System.Drawing.Size(0, 0);
            this.frmRenovFoto.Opacity = 0;
            this.frmRenovFoto.Show();
            this.frmRenovFoto.Hide();
            this.frmRenovFoto.hapusData();

        }
        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitFoto();
        }
        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.pencarian = false;
            this.initGrid();
            this.getInitFoto();
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
                this.getInitFoto(get_where_clause(nama_kolom, opr, parameter, parameter_2));

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

                case "Tanggal Update":
                    where = "UPPER(CREATED) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET_PHOTO) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nama File":
                    where = "Upper(NM_PHOTO) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Ukuran File (Mb)":
                    where = "Upper(FILE_SIZE) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcRenovasiFotoSelect.BPSIMANSROW_M_ASET_PHOTO_GRID)viewTerpilih.GetRow(e.FocusedRowHandle);
                if (viewTerpilih.IsLastRow)
                {
                    this.rowTerakhir = true;
                }
                else
                {
                    this.rowTerakhir = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();

                if (selectedData.FILE_SIZE != 0)
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
                case "Tanggal Upload":
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
