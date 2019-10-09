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

namespace AppPengguna.AST.ATB
{
    public class UcATBPerlengkapan:UserControlDetail
    {
        private SvcATBPerkapSelect.call_pttClient fetchData;
        private SvcATBPerkapSelect.InputParameters parInp;
        private SvcATBPerkapSelect.OutputParameters outDat;
        public SvcATBPerkapSelect.BPSIMANSROW_KTWJD_PERLENGKAP selectedData;


       //private GridView selectedView;
        
        private UcATBForm frmMaster;

        private bool isSearch = false;

        public SvcATBPerkapSelect.BPSIMANSROW_KTWJD_PERLENGKAP SelectedData
        {
            get
            {
                return selectedData;
            }
        }

        public UcATBForm FrmMaster
        {
            get
            {
                return frmMaster;
            }
        }

  

        public string Status;
        public decimal? ID_KTWJD;
        public UcATBPerlengkapan(decimal? _ID_KTWJD, string _status)
            : base()
        {
            this.ID_KTWJD = _ID_KTWJD;
            this.Status = _status;
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
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcATBPerkapSelect.BPSIMANSROW_KTWJD_PERLENGKAP);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("Jenis Perlengkapan", "NM_PERLENGKAP", "NM_PERLENGKAP", 1, true);
            this.setKolom("Jumlah", "JUMLAH", "JUMLAH", 2, true, 120, "integer");
            this.setKolom("Keterangan", "KET", "KET", 3, true);

            
            this.gridDoubleClickDetail = true;
            this.show_record = true;
            this.initGrid();
            this.getInitMutLny();
        }

        public bool moreCari = false;
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
                this.getInitMutLny(get_where_clause(nama_kolom, opr, parameter, parameter_2));


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
                
                case "Jenis Perlengkapan":
                    where = "Upper(NM_PERLENGKAP) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jumlah":
                    where = "JUMLAH " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
               
                default:
                    break;
            }
            return where;
        }
        public void getInitMutLny(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcATBPerkapSelect.InputParameters parInp = new SvcATBPerkapSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KTWJD = {0} {1}", this.ID_KTWJD, _where);
            fetchData = new SvcATBPerkapSelect.call_pttClient();
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
                
            }
        }

        private delegate void ShowData(SvcATBPerkapSelect.OutputParameters dataOut);
        private string StatusCrud = "";
        private decimal? NUM;
        public void showData(SvcATBPerkapSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KTWJD_PERLENGKAP.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KTWJD_PERLENGKAP[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KTWJD_PERLENGKAP[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KTWJD_PERLENGKAP[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KTWJD_PERLENGKAP[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_KTWJD_PERLENGKAP[i]);
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
        private UcATBPerlengakapanForm frmPerkap;
        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            frmPerkap = new UcATBPerlengakapanForm(this, "C");
            StatusCrud = "input";
            frmPerkap.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            if (this.selectedData != null)
            {
                if (this.Status == "detail")
                {
                    frmPerkap = new UcATBPerlengakapanForm(this, "detail");
                    StatusCrud = "detail";
                }
                else
                {
                    frmPerkap = new UcATBPerlengakapanForm(this, "U");
                    StatusCrud = "edit";
                }
                frmPerkap.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            initGrid();
            moreCari = false;
            getInitMutLny();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            getInitMutLny();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            if (this.selectedData != null)
            {
                if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.selectedData.NUM + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.StatusCrud = "hapus";
                    frmPerkap = new UcATBPerlengakapanForm(this, "D");
                    frmPerkap.ShowDialog();
                    frmPerkap.Opacity = 30;
                    frmPerkap.bbiSave.Enabled = false;
                    frmPerkap.crudOperation("D");
              
                }
            }

        }

        

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e) { }

        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) 
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
                selectedData = (SvcATBPerkapSelect.BPSIMANSROW_KTWJD_PERLENGKAP)selectedView.GetRow(e.FocusedRowHandle);
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
    }
}
