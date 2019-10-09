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
using System.IO;

namespace AppPengguna.AST.TN
{
    class ucTnhBangunan : UserControlDetail
    {

        private ucTanahForm frmTanah;
        private frmTnhBangunan frmTnhBgn;

        public ArrayList dataGrid;
        private Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private String Status;
        public decimal? IdKtnh;
        private string search = "";

        private SvcTnhBangunanSelect.call_pttClient fetchData;
        private SvcTnhBangunanSelect.InputParameters parInp;
        private SvcTnhBangunanSelect.OutputParameters outData;
        public SvcTnhBangunanSelect.BPSIMANSROW_M_KTNH_BANGUNAN selectedData;
        
     
        public ucTnhBangunan(decimal? _ID_KTNH, String _status)
            : base()
        {
            this.initTnhBangunan();
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

        private void initTnhBangunan()
        {
            
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcTnhBangunanSelect.BPSIMANSROW_M_KTNH_BANGUNAN);
            this.gcUcDtl.DataSource = binder;

            this.setKolom("No", "NUM_KTNH", "NUM_KTNH", 0, false);
            this.setKolom("Kode Barang", "KD_BRG", "KD_BRG", 1, true);
            this.setKolom("Nama Barang", "UR_SSKEL", "UR_SSKEL", 2, true);
            this.setKolom("Merk/Type", "MERK", "MERK", 3, true);
            this.setKolom("NUP", "NO_ASET", "NO_ASET", 4, true);
            this.setKolom("KIB", "NO_KIB", "NO_KIB", 5, true);
            this.setKolom("Jumlah Lantai", "JML_LT", "JML_LT", 6, true,100,"number");
            this.setKolom("Luas Dasar Bangunan", "LUAS_DSR", "LUAS_DSR", 7, true,120, "number");
            this.setKolom("Luas Bangunan", "LUAS_BDG", "LUAS_BDG", 8, true, 120, "number");
            this.setKolom("Nilai Buku", "NILAI_BUKU", "NILAI_BUKU", 9, true, 120, "number");
            this.setKolom("Jenis Pengguna", "JNS_PMK", "JNS_PMK", 10, true);
            this.setKolom("Unit Pengguna", "KD_SATKER", "KD_SATKER", 11, true);
            this.setKolom("Uraian Unit Pengguna", "UR_SATKER", "UR_SATKER", 12, true);
            int width = this.setKolom("Keterangan", "CATATAN", "CATATAN", 13, true);
            this.SetGridSize(width, 0);
            this.gridDoubleClickDetail = true;
            this.show_record = true;

           
        }
     
        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.initGrid();
            this.getInitTnhBgn();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }


        #region load data
        public void getInitTnhBgn(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcTnhBangunanSelect.InputParameters();
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
            fetchData = new SvcTnhBangunanSelect.call_pttClient();
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
            catch (Exception E)
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcTnhBangunanSelect.OutputParameters dataOut);
        public decimal? NUM;
        private string StatusCrud = "";
        public void showData(SvcTnhBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_BANGUNAN.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KTNH_BANGUNAN[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KTNH_BANGUNAN[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KTNH_BANGUNAN[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KTNH_BANGUNAN[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KTNH_BANGUNAN[i]);
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
            this.frmTnhBgn = new frmTnhBangunan(this, "input");
            StatusCrud = "input";
            this.frmTnhBgn.ShowDialog();
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
                this.frmTnhBgn = new frmTnhBangunan( this, "detail");
                StatusCrud = "detail";
            }
            else
            {
                this.frmTnhBgn = new frmTnhBangunan( this, "edit");
                StatusCrud = "edit";
            }
            this.frmTnhBgn.ShowDialog();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.selectedData.NUM + " ?", konfigApp.judulHapusData,
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    StatusCrud = "hapus";
                    this.frmTnhBgn = new frmTnhBangunan(this, "hapus");
                    this.frmTnhBgn.Size = new System.Drawing.Size(0, 0);
                    this.frmTnhBgn.Opacity = 0;
                    this.frmTnhBgn.Show();
                    this.frmTnhBgn.Hide();
                    this.frmTnhBgn.hapusData();
                }
            }
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitTnhBgn();
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.initGrid();
            this.getInitTnhBgn();
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
                this.getInitTnhBgn(get_where_clause(nama_kolom, opr, parameter, parameter_2));

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

                case "Kode Barang":
                    where = "UPPER(KD_BRG) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nama Barang":
                    where = "Upper(UR_SSKEL) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Merk/Type":
                    where = "Upper(MERK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "NUP":
                    where = "Upper(NO_ASET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "KIB":
                    where = "Upper(NO_KIB) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jumlah Lantai":
                    where = "Upper(JML_LT) " + get_operator("Integer", opr, parameter, parameter2);
                    break;
                case "Luas Dasar Bangunan":
                    where = "Upper(LUAS_DSR) " + get_operator("Integer", opr, parameter, parameter2);
                    break;
                case "Nilai Buku":
                    where = "Upper(NILAI_BUKU) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Jenis Pengguna":
                    where = "Upper(JNS_PMK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Unit Pengguna":
                    where = "Upper(KD_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Uraian Unit Pengguna":
                    where = "Upper(UR_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(CATATAN) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcTnhBangunanSelect.BPSIMANSROW_M_KTNH_BANGUNAN)viewTerpilih.GetRow(e.FocusedRowHandle);
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
