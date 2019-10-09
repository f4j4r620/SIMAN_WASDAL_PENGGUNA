using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.PK
{
    class ucPropKhususLok : UserControlDetail
    {

   
        private FrmProKLokasi FrmProKLokasi;

        ArrayList dataGrid;
        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private String Status;
        public decimal? ID_KPROK;
        public string nama_aset;

        private SvcPropKhususLokasiSelect.call_pttClient fetchData;
        private SvcPropKhususLokasiSelect.InputParameters parInp;
        private SvcPropKhususLokasiSelect.OutputParameters outData;
        public SvcPropKhususLokasiSelect.BPSIMANSROW_M_KPROK_LOKASI selectedData;






        public ucPropKhususLok(decimal? ID_KPROK, string status)
            : base()
        {
            this.initTnhLok();
       
            this.Status = Status;
            this.ID_KPROK = ID_KPROK;
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

        private void initTnhLok()
        {


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcPropKhususLokasiSelect.BPSIMANSROW_M_KPROK_LOKASI);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, true);
            this.setKolom("Batas Utara", "LOKBATAS_U", "LOKBATAS_U", 2, true);
            this.setKolom("Batas Barat", "LOKBATAS_B", "LOKBATAS_B", 3, true);
            this.setKolom("Batas Selatan", "LOKBATAS_S", "LOKBATAS_S", 4, true);
            this.setKolom("Batas Timur", "LOKBATAS_T", "LOKBATAS_T", 5, true);
            this.setKolom("Bentuk", "LOKBENTUK", "LOKBENTUK", 6, true);
            this.setKolom("Peruntukan Tanah", "LOKPERUNTUKAN", "LOKPERUNTUKAN", 7, true);
            this.setKolom("Topografi Kontur", "LOKKONTUR", "LOKKONTUR", 8, true);
            this.setKolom("Topografi Elevasi", "LOKELEVASI", "LOKELEVASI", 9, true);
            this.setKolom("Aksesbilitas", "LOK_AKSES", "LOK_AKSES", 10, true);
            this.setKolom("GPS", "GPS", "GPS", 11, true);
            this.gridDoubleClickDetail = true;
            this.show_record = true;
        }

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tampilMap);
            this.initGrid();
            this.getInitTnhLok();
        }



        #region load data
        public void getInitTnhLok(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcPropKhususLokasiSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KPROK = {0} {1}", this.ID_KPROK, _where);
            fetchData = new SvcPropKhususLokasiSelect.call_pttClient();
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

        private delegate void ShowData(SvcPropKhususLokasiSelect.OutputParameters dataOut);
        public decimal? NUM;
        private string StatusCrud = "";
        public void showData(SvcPropKhususLokasiSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KPROK_LOKASI.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KPROK_LOKASI[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KPROK_LOKASI[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KPROK_LOKASI[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KPROK_LOKASI[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KPROK_LOKASI[i]);
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
            this.FrmProKLokasi = new FrmProKLokasi(this, "input");
            StatusCrud = "input";
            this.FrmProKLokasi.ShowDialog();
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
                this.FrmProKLokasi = new FrmProKLokasi(this, "detail");
                this.StatusCrud = "detail";
            }
            else
            {
                this.FrmProKLokasi = new FrmProKLokasi(this, "edit");
                this.StatusCrud = "edit";
            }

            this.FrmProKLokasi.ShowDialog();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                this.StatusCrud = "hapus";
                this.FrmProKLokasi = new FrmProKLokasi(this, "hapus");
                this.FrmProKLokasi.Size = new System.Drawing.Size(0, 0);
                this.FrmProKLokasi.Opacity = 0;
                this.FrmProKLokasi.Show();
                this.FrmProKLokasi.Hide();
                this.FrmProKLokasi.hapusData();

            }
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitTnhLok();
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getInitTnhLok();
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
                this.getInitTnhLok(get_where_clause(nama_kolom, opr, parameter, parameter_2));

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
                case "Batas Utara":
                    where = "Upper(LOKBATAS_U) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Barat":
                    where = "Upper(LOKBATAS_B) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Selatan":
                    where = "Upper(LOKBATAS_S) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Timur":
                    where = "Upper(LOKBATAS_T) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Bentuk":
                    where = "Upper(LOKBENTUK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Peruntukan Tanah":
                    where = "Upper(LOKPERUNTUKAN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Topografi Kontur":
                    where = "Upper(LOKKONTUR) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Topografi Elevasi":
                    where = "Upper(LOKELEVASI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Aksesbilitas":
                    where = "Upper(LOK_AKSES) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "GPS":
                    where = "Upper(GPS) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcPropKhususLokasiSelect.BPSIMANSROW_M_KPROK_LOKASI)viewTerpilih.GetRow(e.FocusedRowHandle);

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

        protected void tampilMap(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string latitude;//= "-1.406109";
            string longitude;//= "115.473631";
            string deskripsi;
            try
            {
                string[] gps = selectedData.GPS.Split(',');
                latitude = gps[0];
                longitude = gps[1];
                deskripsi = this.nama_aset;
                PU.Map map = new PU.Map(latitude, longitude, deskripsi);
                map.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Alamat lokasi GPS salah", konfigApp.judulGagalAmbil);
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

        }

        #endregion


    }
}
