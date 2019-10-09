using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.RN
{
    public class UcKonsRmhNgr : UserControlDetail
    {
        private SvcKonsRmhNgrSelect.call_pttClient fetchData;
        private SvcKonsRmhNgrSelect.InputParameters parInp;
        private SvcKonsRmhNgrSelect.OutputParameters outDat;
        private SvcKonsRmhNgrSelect.BPSIMANSROW_KRMH_NEG_KONS_BDG selectedData;
        

        private GridView selectedView;

        private ucRumahNegaraForm frmMaster;
        private FrmKonsRmhNgr PU;
        public decimal? NUM;
        private string StatusCrud = "";
        private bool isSearch = false;
        private bool initiated = false;
        public decimal? ID_KRMH_NEG;
        public bool Initiated
        {
            get 
            {
                return initiated; 
            }

            set
            {
                initiated = value;
            }
        }

        public SvcKonsRmhNgrSelect.BPSIMANSROW_KRMH_NEG_KONS_BDG SelectedData
        {
            get
            {
                return selectedData;
            }
        }

        public ucRumahNegaraForm FrmMaster
        {
            get
            {
                return frmMaster;
            }
        }



        
       
        public string Status;
        public UcKonsRmhNgr(decimal? _ID_KRMH_NEG, string _status)
            : base()
        {

            this.Status = _status;
            this.ID_KRMH_NEG = _ID_KRMH_NEG;
            this.InitKonsRmhNgr();
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
            this.bbTambah.Caption = konfigApp.labelTambah;
            this.btnMap.Enabled = false;
           
        }

        private void InitKonsRmhNgr()
        {
            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("Tgl Inventarisasi", "TGL_INV", "TGL_INV", 1, true);
            this.setKolom("Struktur Atap", "STR_ATAP", "STR_ATAP", 2, true);
            this.setKolom("Struktur Rangka", "STR_RANGKA", "STR_RANGKA", 3, true);
            this.setKolom("Material Atap", "MATERIAL_ATAP", "MATERIAL_ATAP", 4, true);
            this.setKolom("Material Dinding", "MATERIAL_ATAP", "MATERIAL_ATAP", 5, true);
            this.setKolom("Material Langit-langit", "MATERIAL_LANGIT", "MATERIAL_LANGIT", 6, true);
            this.setKolom("Lantai", "LANTAI", "LANTAI", 7, true);
            this.setKolom("Pelapis Dinding Dalam", "PELAPIS_DINDIN_DLM", "PELAPIS_DINDIN_DLM", 8, true);
            this.setKolom("Pelapis Dinding Luar", "PELAPIS_DINDIN_LR", "PELAPIS_DINDIN_LR", 9, true);
            this.setKolom("Perkerasan", "PERKERASAN", "PERKERASAN", 9, true);
            this.setKolom("Pagar", "PAGAR", "PAGAR", 10, true);
            this.setKolom("Kondisi", "UR_KONDISI", "UR_KONDISI", 11, true);
            this.setKolom("File", "NMFILE", "NMFILE", 12);


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcKonsRmhNgrSelect.BPSIMANSROW_KRMH_NEG_KONS_BDG);
            this.gcUcDtl.DataSource = binder;
            this.gridDoubleClickDetail = true;



            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);
            this.show_record = true;
        }

        #region View Dokumen
        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------
        protected void view_dokumen(object sender, ItemClickEventArgs e)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = selectedData.ID_M_KBDG_KONS_BDG;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_M_KBDG_KONS_BDG";
                parInp.P_TABLE = "M_KRMH_NEG_KONS_BDG";

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
            int jmlData = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

            if (jmlData > 0)
            {
                SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
                System.IO.File.WriteAllBytes("dok.pdf", dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display("dok.pdf");
                PuPdf.ShowDialog();
            }
        }


        #endregion//ViewDokumen

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            
            this.initGrid();
            this.getInitKonsRmh();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        private bool moreCari = true;
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
                this.getInitKonsRmh(get_where_clause(nama_kolom, opr, parameter, parameter_2));


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

                case "Tgl Inventarisasi":
                    where = "Upper(TGL_INV) " + get_operator("Date", opr, parameter, parameter2);
                    break;

                case "Struktur Atap":
                    where = "Upper(STR_ATAP) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Material Atap":
                    where = "Upper(MATERIAL_ATAP) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Struktur Ruangan":
                    where = "Upper(STR_RANGKA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Material Langit-langit":
                    where = "Upper(MATERIAL_LANGIT) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Lantai":
                    where = "Upper(LANTAI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Pelapis Dinding Dalam":
                    where = "Upper(PELAPIS_DINDIN_DLM) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Pelapis Dinding Luar":
                    where = "Upper(PELAPIS_DINDIN_LR) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Perkerasan":
                    where = "Upper(PERKERASAN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Pagar":
                    where = "Upper(PAGAR) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Kondisi":
                    where = "Upper(UR_KONDISI) " + get_operator("String", opr, parameter, parameter2);
                    break;

                case "File":
                    where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }

        public void getInitKonsRmh(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start(); 
            this.nonAktifForm("");
            SvcKonsRmhNgrSelect.InputParameters parInp = new SvcKonsRmhNgrSelect.InputParameters();
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
     
            parInp.STR_WHERE = String.Format("ID_KRMH_NEG = {0} {1}", this.ID_KRMH_NEG, _where);
            parInp.P_COL = "TGL_INV";
            parInp.P_SORT = "DESC";
            fetchData = new SvcKonsRmhNgrSelect.call_pttClient();
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outDat = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcKonsRmhNgrSelect.OutputParameters dataOut);

        public void showData(SvcKonsRmhNgrSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG[i].TGL_INV).Substring(0, 10);
              string date_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG[i].TGL_INV).Substring(0, 8);

              if (date == "11/11/1000" || date_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG[i].TGL_INV = null;
              }
                binder.Add(serviceOutPut.SF_ROW_KRMH_NEG_KONS_BDG[i]);
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

       

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        { 
        }

        
        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PU = new FrmKonsRmhNgr(this,"C");
            this.StatusCrud = "input";
            PU.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    PU = new FrmKonsRmhNgr(this, "detail");
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new FrmKonsRmhNgr(this, "U");
                    this.StatusCrud = "edit";
                }
                PU.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            initGrid();
            moreCari = false;
            getInitKonsRmh();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            getInitKonsRmh();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.selectedData.NUM + " ?", konfigApp.judulHapusData,
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.StatusCrud = "hapus";
                    PU = new FrmKonsRmhNgr(this, "D");
                    this.PU.Size = new System.Drawing.Size(0, 0);
                    this.PU.Opacity = 0;
                    this.PU.Show();
                    this.PU.Hide();
                    PU.crudOperation("D");
                }
            }
        }

        

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
        }

        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcKonsRmhNgrSelect.BPSIMANSROW_KRMH_NEG_KONS_BDG)selectedView.GetRow(e.FocusedRowHandle);
                if (selectedView.IsLastRow)
                {
                    LastRow = true;
                }
                else
                {
                    LastRow = false;
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

       
        protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
        {
            moreCari = false;
        }
    }
}
