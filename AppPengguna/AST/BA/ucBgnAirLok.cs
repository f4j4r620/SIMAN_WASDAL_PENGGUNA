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

namespace AppPengguna.AST.BA
{
    public class ucBgnAirLok : UserControlDetail
    {

        private UcBgnAirFrm ucbgnairfrm;
        private FrmBgnAirLok frmbgnairlok;
        
        //public ArrayList dataGrid;
        //private Thread myThread = null;
        private frmProgres progresBar = null;
        //public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private String Status;
        private String Where;
        public decimal? ID_KBAIR;
        public string nama_aset;

        private SvcBgnAirLokasiSelect.call_pttClient fetchData;
        private SvcBgnAirLokasiSelect.InputParameters parInp;
        private SvcBgnAirLokasiSelect.OutputParameters outData;
        public SvcBgnAirLokasiSelect.BPSIMANSROW_M_KBAIR_LOKASI SelectedData;

        public decimal? NUM;
        private string StatusCrud = "";
        public ucBgnAirLok(decimal? _ID_KBAIR, String _status)
            : base()
        {
            this.initBgnAirLok();
            this.Status = _status;
            this.ID_KBAIR = _ID_KBAIR;
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



        private void initBgnAirLok()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcBgnAirLokasiSelect.BPSIMANSROW_M_KBAIR_LOKASI);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false, 90);
            this.setKolom("Batas Utara", "LOKBATAS_U", "LOKBATAS_U", 1, true, 210);
            this.setKolom("Batas Selatan", "LOKBATAS_S", "LOKBATAS_S", 2, true, 210);
            this.setKolom("Batas Timur", "LOKBATAS_T", "LOKBATAS_T", 3, true);
            this.setKolom("Batas Barat", "LOKBATAS_B", "LOKBATAS_B", 4, true);
            this.setKolom("Bentuk", "LOKBENTUK", "LOKBENTUK", 5, true);
            this.setKolom("Peruntukan Tanah", "LOKPERUNTUKAN", "LOKPERUNTUKAN", 6, true, 200);
            this.setKolom("Topografi-Kontur", "LOKKONTUR", "LOKKONTUR", 7, true);
            this.setKolom("Topografi-Elevasi", "LOKELEVASI", "LOKELEVASI", 8, true);
            int width = this.setKolom("Aksesbilitas", "LOK_AKSES", "LOK_AKSES", 9, true);
            this.setKolom("GPS", "GPS", "GPS", 12, true);
            this.gridDoubleClickDetail = true;
            this.gvUcDtl.BestFitColumns();
            this.show_record = true;
        }

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.initGrid();
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tampilMap);
            this.getInitBgnAirLokasi();
            this.btnMap.Enabled = false;
        }


        

        #region load data
        public void getInitBgnAirLokasi(string _where = null)
        {
            
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcBgnAirLokasiSelect.InputParameters();
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
            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);
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
            parInp.STR_WHERE = String.Format(" ID_KBAIR = {0} {1}", this.ID_KBAIR, _where);
            fetchData = new SvcBgnAirLokasiSelect.call_pttClient();
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
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new ShowData(this.showData), this.outData);
            }
            catch
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcBgnAirLokasiSelect.OutputParameters dataOut);

        public void showData(SvcBgnAirLokasiSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBAIR_LOKASI.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KBAIR_LOKASI[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KBAIR_LOKASI[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBAIR_LOKASI[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBAIR_LOKASI[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KBAIR_LOKASI[i]);
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
            this.frmbgnairlok = new FrmBgnAirLok(this, "input");
            this.StatusCrud = "input";
            this.frmbgnairlok.ShowDialog();
        }
        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SelectedData != null)
            {
                this.NUM = SelectedData.NUM;
                if (this.Status == "detail")
                {
                    this.frmbgnairlok = new FrmBgnAirLok(this, "detail");
                    this.StatusCrud = "detail";
                }
                else
                {
                    this.frmbgnairlok = new FrmBgnAirLok(this, "edit");
                    this.StatusCrud = "edit";
                }
                this.frmbgnairlok.ShowDialog();
            }
        }
   

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SelectedData == null)
            {
                return;
            }
            if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.SelectedData.NUM + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.StatusCrud = "hapus";
                this.frmbgnairlok = new FrmBgnAirLok(this, "hapus");
                this.frmbgnairlok.Size = new System.Drawing.Size(0, 0);
                this.frmbgnairlok.Opacity = 0;
                this.frmbgnairlok.Show();
                this.frmbgnairlok.Hide();
                this.frmbgnairlok.hapusData();
            }
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitBgnAirLokasi();
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getInitBgnAirLokasi();
        }

        protected override void bbSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.nonAktifForm("");
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                string nama_kolom = this.LuKolom.EditValue.ToString();
                string opr = this.barOperator.EditValue.ToString().ToUpper();
                string parameter = this.teSearch.EditValue.ToString().ToUpper();
                string parameter_2 = "";
                if (opr == "ANTARA")
                {
                    parameter_2 = this.teSearch2.EditValue.ToString().ToUpper();
                }
                this.dataInisial = true;
                this.getInitBgnAirLokasi(get_where_clause(nama_kolom, opr, parameter, parameter_2));


            }
            catch (Exception)
            {

                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);

            }
        }
        private string get_where_clause(string nama_kolom, string opr, string parameter, string parameter2)
        {
            string where = "";


            switch (nama_kolom)
            {
                case "ID Lokasi Bangunan":
                    where = "ID_M_KBDG_LOKASI " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Batas Utara":
                    where = "Upper(LOKBATAS_U) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Selatan":
                    where = "Upper(LOKBATAS_S) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Timur":
                    where = "Upper(LOKBATAS_T) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Barat":
                    where = "Upper(LOKBATAS_B) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Bentuk":
                    where = "Upper(LOKBENTUK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Peruntukan Tanah":
                    where = "Upper(LOKPERUNTUKAN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Topografi-Kontur":
                    where = "Upper(LOKKONTUR) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Topografi-Elevasi":
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
                SelectedData = (SvcBgnAirLokasiSelect.BPSIMANSROW_M_KBAIR_LOKASI)viewTerpilih.GetRow(e.FocusedRowHandle);
                if (viewTerpilih.IsLastRow)
                {
                    this.rowTerakhir = true;
                }
                else
                {
                    this.rowTerakhir = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();

                if (SelectedData.GPS != "-")
                {
                  this.btnMap.Enabled = true;
                }
                else
                {
                  this.btnMap.Enabled = false;
                }
            }
        }

        protected void tampilMap(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string latitude = "-1.406109";
            string longitude = "115.473631";
            string deskripsi;
            try
            {
                string[] gps = SelectedData.GPS.Split(',');
                latitude = gps[0];
                longitude = gps[1];
                deskripsi = this.nama_aset;
                AppPengguna.PU.Map map = new AppPengguna.PU.Map(latitude, longitude, deskripsi);
                
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

        
        #endregion
    }
}
