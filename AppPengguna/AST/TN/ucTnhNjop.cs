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
    class ucTnhNjop : UserControlDetail
    {

        private ucTanahForm frmTanah;
        private frmTnhNjop frmTnhNjop;

        public ArrayList dataGrid;
        private Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;
        public decimal? jmlData = 0;
        private String Status;
        public decimal? IdKtnh;
 

        private SvcTnhNjopSelect.call_pttClient fetchData;
        private SvcTnhNjopSelect.InputParameters parInp;
        private SvcTnhNjopSelect.OutputParameters outData;
        public SvcTnhNjopSelect.BPSIMANSROW_M_KTNH_NJOP selectedData;

        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-----------------------------------------------------------------
        public ucTnhNjop(decimal? _ID_KTNH, String Status)
            : base()
        {
            this.initTnhNjop();
            this.Status = Status;
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

        private void initTnhNjop()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcTnhNjopSelect.BPSIMANSROW_M_KTNH_NJOP);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, true);
            this.setKolom("Nomor Objek Pajak", "NOP", "NOP", 1, true);
            //this.setKolom("NPWP", "NPWP", "NPWP", 2, true);
            this.setKolom("Tahun", "TAHUN", "TAHUN", 2, true);
            this.setKolom("Luas", "LUAS", "LUAS", 3, true,120,"number");
            this.setKolom("Kelas", "KELAS", "KELAS", 4, true);
            this.setKolom("NJOP Per Meter (Rp)", "NJOP_METER", "NJOP_METER", 5, true, 120, "number");
            this.setKolom("Nilai Total NJOP (Rp)", "NJOP_NILAI", "NJOP_NILAI", 6, true, 120, "number");
            this.setKolom("File (SPPT/PBB)", "NMFILE", "NMFILE", 7, true);
            this.gridDoubleClickDetail = true;
            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);
            this.show_record = true;
        }

        #region View Dokumen
        protected void view_dokumen(object sender, ItemClickEventArgs e)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = selectedData.ID_M_KTNH_NJOP;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_M_KTNH_NJOP";
                parInp.P_TABLE = "M_KTNH_NJOP";
                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient(konfigApp.SvcAsetGetDokSelect_name, konfigApp.SvcAsetGetDokSelect_address);
                svcAsetGetDokSelect.Open();
                svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show(E.Message);
                //MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getResultDok(IAsyncResult result)
        {
            try
            {
                this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
                svcAsetGetDokSelect.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowFileDok(this.showFileDok), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowFileDok(SvcAsetGetDokSelect.OutputParameters dataOut);

        public void showFileDok(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlData > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
                System.IO.File.WriteAllBytes(selectedData.NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(selectedData.NMFILE);
                PuPdf.ShowDialog();
            }
        }


        #endregion//ViewDokumen

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.initGrid();
            this.getInitTnhNjop();
            if (jmlData == 0)
            {
                this.btnMap.Enabled = false;
            }
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        

        #region load data
        public void getInitTnhNjop(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcTnhNjopSelect.InputParameters();
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
            fetchData = new SvcTnhNjopSelect.call_pttClient(konfigApp.SvcTnhNjopSelect_name, konfigApp.SvcTnhNjopSelect_address);
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
            catch(Exception e)
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcTnhNjopSelect.OutputParameters dataOut);
        public decimal? NUM;
        private string StatusCrud = "";
        public void showData(SvcTnhNjopSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KTNH_NJOP.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KTNH_NJOP[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KTNH_NJOP[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KTNH_NJOP[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KTNH_NJOP[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KTNH_NJOP[i]);
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

       

        protected override void gvUcDtl_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            viewTerpilih = sender as GridView;
            if (viewTerpilih.SelectedRowsCount > 0)
            {
                selectedData = (SvcTnhNjopSelect.BPSIMANSROW_M_KTNH_NJOP)viewTerpilih.GetRow(e.FocusedRowHandle);
                if (viewTerpilih.IsLastRow)
                {
                    this.rowTerakhir = true;
                }
                else
                {
                    this.rowTerakhir = false;
                }
                
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
                if (selectedData.FILE_EXISTS != 0)
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

        

        #region button
        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.frmTnhNjop = new frmTnhNjop(this, "input");
            StatusCrud = "input";
            this.frmTnhNjop.ShowDialog();
        }
        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    this.frmTnhNjop = new frmTnhNjop(this, "detail");
                    this.StatusCrud = "detail";
                }
                else
                {
                    this.frmTnhNjop = new frmTnhNjop(this, "edit");
                    this.StatusCrud = "edit";
                }
                this.frmTnhNjop.ShowDialog();
            }
        }
        
        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                    this.StatusCrud = "hapus";
                    this.frmTnhNjop = new frmTnhNjop(this, "hapus");
                    this.frmTnhNjop.Size = new System.Drawing.Size(0, 0);
                    this.frmTnhNjop.Opacity = 0;
                    this.frmTnhNjop.Show();
                    this.frmTnhNjop.Hide();
                    this.frmTnhNjop.hapusData();
                
            }
        }
        
        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitTnhNjop();
        }
        
        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.initGrid();
            this.getInitTnhNjop();
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
                this.getInitTnhNjop(get_where_clause(nama_kolom, opr, parameter, parameter_2));

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

                case "Nomor Objek Pajak":
                    where = "UPPER(NOP) " + get_operator("String", opr, parameter, parameter2);
                    break;
                //case "NPWP":
                //    where = "Upper(NPWP) " + get_operator("String", opr, parameter, parameter2);
                //    break;
                case "Tahun":
                    where = "Upper(STATUS_HUKUM) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Luas":
                    where = "Upper(LUAS) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Kelas":
                    where = "Upper(KELAS) " + get_operator("String", opr, parameter, parameter2);
                    break;

                case "NJOP Per Meter (Rp)":
                    where = "Upper(NJOP_METER) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Nilai Total NJOP (Rp)":
                    where = "Upper(NJOP_NILAI) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "File (SPPT/PBB)":
                    where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }
        #endregion


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
