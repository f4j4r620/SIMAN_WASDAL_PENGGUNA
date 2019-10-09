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
    public class UcDetRmhNgr : UserControlDetail
    {
        private SvcDetRmhNgrSelect.call_pttClient fetchData;
        private SvcDetRmhNgrSelect.InputParameters parInp;
        private SvcDetRmhNgrSelect.OutputParameters outDat;
        private SvcDetRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DETAIL selectedData;
        //private SvcLokTnhSelect.BPSIMANSROW_M_KTNH_LOKASI newRow;
        //private SvcLokTnhCrud.call_pttClient crudData;
        //private SvcLokTnhCrud.InputParameters inputCrud;
        //private SvcLokTnhCrud.OutputParameters outCrud;

        
        private GridView selectedView ;

        private ucRumahNegaraForm frmMaster;
        private FrmDetRmhNgr PU;
        private bool isSearch = false;
        private bool initiated = false;

        public SvcDetRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DETAIL SelectedData
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

      
        public decimal? ID_KRMH_NEG;
        public string Status;
        public decimal? NUM;
        private string StatusCrud = "";

        public UcDetRmhNgr(decimal? _ID_KRMH_NEG, string _status)
            : base()
        {

            this.Status = _status;
            this.ID_KRMH_NEG = _ID_KRMH_NEG;

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

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("Nama Ruangan", "UR_TIPE_RUANG", "UR_TIPE_RUANG", 1, true);
            this.setKolom("Jumlah", "JUMLAH", "JUMLAH", 2, true,120,"integer");
            this.setKolom("Keterangan", "KET", "KET", 3, true);
          
            this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.gvUcDtl.OptionsBehavior.Editable = false;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcDetRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DETAIL);
            this.gcUcDtl.DataSource = binder;
            this.gridDoubleClickDetail = true;
            this.show_record = true;

            this.initGrid();
            this.getInitDetRmh();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        public bool moreCari = false;
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
                this.getInitDetRmh(get_where_clause(nama_kolom, opr, parameter, parameter_2)); ;


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

                case "Nama Ruangan":
                    where = "Upper(NM_RUANGAN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jumlah":
                    where = "Upper(JUMLAH) " + get_operator("Number", opr, parameter, parameter2);
                    break;

                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }


        public void getInitDetRmh(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcDetRmhNgrSelect.InputParameters parInp = new SvcDetRmhNgrSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format("ID_KRMH_NEG = {0} {1}", this.ID_KRMH_NEG, _where);
            parInp.P_SORT = "DESC";

            fetchData = new SvcDetRmhNgrSelect.call_pttClient();
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

        private delegate void ShowData(SvcDetRmhNgrSelect.OutputParameters dataOut);

        public void showData(SvcDetRmhNgrSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KRMH_NEG_DETAIL.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KRMH_NEG_DETAIL[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KRMH_NEG_DETAIL[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRMH_NEG_DETAIL[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRMH_NEG_DETAIL[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KRMH_NEG_DETAIL[i]);
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
           PU= new FrmDetRmhNgr(this,"C");
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
                    PU = new FrmDetRmhNgr(this, "detail");
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new FrmDetRmhNgr(this, "U");
                    this.StatusCrud = "edit";
                }
                PU.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
             moreCari = false;
            this.getInitDetRmh();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.dataInisial = false;
             this.getInitDetRmh();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.selectedData.NUM + " ?", konfigApp.judulHapusData,
             MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PU = new FrmDetRmhNgr(this, "D");
                    this.StatusCrud = "hapus";
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
            if(selectedView.SelectedRowsCount>0)
                selectedData = (SvcDetRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DETAIL)selectedView.GetRow(e.FocusedRowHandle);
        }

       
        protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
        {
            moreCari = false;
        }
    }
}
