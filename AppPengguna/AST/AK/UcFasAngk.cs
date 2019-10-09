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

namespace AppPengguna.AST.AK
{
    public class UcFasAngk:UserControlDetail
    {
        private SvcFasAngkSelect.call_pttClient fetchData;
        private SvcFasAngkSelect.InputParameters parInp;
        public SvcFasAngkSelect.OutputParameters outDat;
        public SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG selectedData;
        GridRow grow;
        private bool isSearch = false;
        private bool initiated = false;
        public FormFasAngk PU;
        public decimal? ID_KANGK;
        public string Status;
        private decimal? NUM;
        private string StatusCrud = "";

        public UcFasAngk(decimal? _ID_KANGK, string _status)
            : base()
        {
            this.ID_KANGK = _ID_KANGK;
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
            this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.gvUcDtl.OptionsBehavior.Editable = false;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false,50);
            this.setKolom("Nama Perlengkapan", "NM_FASILITAS", "NM_FASILITAS", 1, true,150);
           // this.setKolom("Isi Fasilitas", "ISI_FASILITAS", "ISI_FASILITAS", 2, true,150);
            this.setKolom("Keterangan", "KET", "KET", 3, false,200);
            this.gridDoubleClickDetail = true;

            
            this.show_record = true;
            this.initGrid();
            this.getInitFasAngk();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }


         

        protected void gvUcDtl_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

  

        public void getInitFasAngk(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcFasAngkSelect.InputParameters parInp = new SvcFasAngkSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format("ID_KANGK = {0} {1}", this.ID_KANGK, _where);

            fetchData = new SvcFasAngkSelect.call_pttClient(konfigApp.SvcFasAngkSelect_name,konfigApp.SvcFasAngkSelect_address);
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

        private delegate void ShowData(SvcFasAngkSelect.OutputParameters dataOut);
        
        public void showData(SvcFasAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG.Count();
            if (binder == null) {
                this.binder = new BindingSource();
                this.binder.DataSource = typeof(SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG);
                this.gcUcDtl.DataSource = binder;

            }

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {

                binder.Add(serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG[i]);
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
            PU = new FormFasAngk('C', selectedData,this);
            PU.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    PU = new FormFasAngk('V', selectedData, this);
                }
                else
                {
                    PU = new FormFasAngk('U', selectedData, this);
                }
                PU.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            initGrid();
            getInitFasAngk();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            getInitFasAngk();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                PU = new FormFasAngk('D', selectedData, this);
                StatusCrud = "hapus";
                this.PU.Show();
                this.PU.Hide();
                this.PU.Size = new System.Drawing.Size(0, 0);
                this.PU.Opacity = 0; 
            }
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
                this.dataInisial = false;
                parInp.STR_WHERE = get_where_clause(nama_kolom, opr, parameter, parameter_2) + " AND ID_KANGK = " + this.ID_KANGK;

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
                case "Nama Perlengkapan":
                    where = "Upper(NM_FASILITAS) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Isi Fasilitas":
                    where = "Upper(ISI_FASILITAS) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
        }

        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG)selectedView.GetRow(e.FocusedRowHandle);
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
    }
}
