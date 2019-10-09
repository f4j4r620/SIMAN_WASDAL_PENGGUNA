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

namespace AppPengguna.AST.SJT
{
    class ucRpemakaiSenjata : UserControlDetail
    {

        private SvcSenjataRpemakaiSelect.call_pttClient fetchData;
        private SvcSenjataRpemakaiSelect.InputParameters parInp;
        private SvcSenjataRpemakaiSelect.OutputParameters outData;
        public SvcSenjataRpemakaiSelect.BPSIMANSROW_M_KSENJ_RWYT_PEMAKAI selectedData;

        private String Status;
        public decimal? IdKsjt;

        GridView viewTerpilih = null;
        public bool rowTerakhir = false;

        private PuSenjataPemakai PuSenjataPemakai;

        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------

        public ucRpemakaiSenjata(decimal? _ID_KSENJ, String status)
            : base()
        {
            this.InitPemakai();
            this.Status = Status;
            this.IdKsjt = _ID_KSENJ;

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
            this.getInitPemakai();
        }

        public void InitPemakai()
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(AppPengguna.SvcSenjataRpemakaiSelect.BPSIMANSROW_M_KSENJ_RWYT_PEMAKAI);
            this.gcUcDtl.DataSource = binder;

            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("NIP", "NIP_PMK", "NIP_PMK", 1, true);
            this.setKolom("Nama Pegawai", "NM_PMK", "NM_PMK", 2, true);
            this.setKolom("Alamat Pegawai", "ALM_PMK", "ALM_PMK", 3, true);
            this.setKolom("Unit Pegawai", "UR_SATKER", "UR_SATKER", 4, true);
            this.setKolom("Tgl Surat Pemakai", "TGL_SIP", "TGL_SIP", 5, true);
            this.setKolom("Tgl Mulai", "TGL_MULAI", "TGL_MULAI", 6, true);
            this.setKolom("Tgl Selesai", "TGL_SELESAI", "TGL_SELESAI", 7, true);
            this.setKolom("Keterangan", "KET", "KET", 8, true);
            this.setKolom("File", "NMFILE", "NMFILE", 9, true);
            //this.SetGridSize(width, 0);
            gridDoubleClickDetail = true;

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
                parInp.P_ID = selectedData.ID_KSENJ_RWYT_PEMAKAI;
                parInp.P_ID_TABLE = "ID_KSENJ_RWYT_PEMAKAI";
                parInp.P_IDSpecified = true;
                parInp.P_TABLE = "M_KSENJ_RWYT_PEMAKAI";
                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient();
                svcAsetGetDokSelect.Open();
                svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
            }
            catch (Exception E)
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                //MessageBox.Show(E.Message);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
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
            int jmlDataGroup = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlDataGroup > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
                System.IO.File.WriteAllBytes(selectedData.NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(selectedData.NMFILE);
                PuPdf.ShowDialog();
            }
        }


        #endregion//ViewDokumen



        #region load data
        public void getInitPemakai(string _where = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            parInp = new SvcSenjataRpemakaiSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KSENJ = {0} {1}", this.IdKsjt, _where);
            parInp.P_COUNT = "Y";
            Console.WriteLine(parInp.STR_WHERE);
            fetchData = new SvcSenjataRpemakaiSelect.call_pttClient();
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
                MessageBox.Show("Load data Pemakai", konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcSenjataRpemakaiSelect.OutputParameters dataOut);
        public decimal? NUM;
        private string StatusCrud = "";
        public void showData(SvcSenjataRpemakaiSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI.Count();
            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    //serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].TOTAL_DATASpecified = true;
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KSENJ_RWYT_PEMAKAI[i]);
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
            this.PuSenjataPemakai = new PuSenjataPemakai(this, "input");
            StatusCrud = "input";
            this.PuSenjataPemakai.ShowDialog();
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
                this.PuSenjataPemakai = new PuSenjataPemakai(this, "detail");
                StatusCrud = "detail";
            }
            else
            {
                this.PuSenjataPemakai = new PuSenjataPemakai(this, "edit");
                StatusCrud = "edit";
            }
            this.PuSenjataPemakai.ShowDialog();
        }
        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData == null)
            {
                return;
            }
            StatusCrud = "hapus";
            this.PuSenjataPemakai = new PuSenjataPemakai(this, "hapus");
            this.PuSenjataPemakai.Size = new System.Drawing.Size(0, 0);
            this.PuSenjataPemakai.Opacity = 0;
            this.PuSenjataPemakai.Show();
            this.PuSenjataPemakai.Hide();
            this.PuSenjataPemakai.hapusData();

        }
        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getInitPemakai();
        }
        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.search = "";
            this.pencarian = false;
            this.initGrid();
            this.getInitPemakai();
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
                this.getInitPemakai(get_where_clause(nama_kolom, opr, parameter, parameter_2));

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

                case "NIP":
                    where = "Upper(NIP_PMK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nama Pegawai":
                    where = "Upper(NM_PMK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Alamat Pegawai":
                    where = "Upper(ALM_PMK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Unit Pegawai":
                    where = "Upper(UR_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl Surat Pemakai":
                    where = "Upper(TGL_SIP) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Tgl Mulai":
                    where = "Upper(TGL_MULAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Tgl Selesai":
                    where = "Upper(TGL_SELESAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "File":
                    where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcSenjataRpemakaiSelect.BPSIMANSROW_M_KSENJ_RWYT_PEMAKAI)viewTerpilih.GetRow(e.FocusedRowHandle);
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
                case "Tgl Surat Pemakai":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Tgl Mulai":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Tgl Selesai":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
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
