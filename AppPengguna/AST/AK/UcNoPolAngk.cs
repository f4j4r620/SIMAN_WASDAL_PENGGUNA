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
    class UcNoPolAngk:UserControlDetail
    {
        #region Inisialisasi Parameter

        private SvcNoPolAngkSelect.call_pttClient fetchData;
        private SvcNoPolAngkSelect.InputParameters parInp;
        private SvcNoPolAngkSelect.OutputParameters outDat;
        public SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL selectedData;
        private bool isSearch = false;

        private FormNoPolAngk PU;

       
        #endregion

        #region Inisialisasi Constructor
        private decimal? jmlData = 0;
        public decimal? ID_KANGK;
        public string Status;
        private decimal? NUM;
        private string StatusCrud = "";
        public UcNoPolAngk(decimal? _ID_KANGK, string _status)
            : base()
        {
            this.ID_KANGK = _ID_KANGK;

            // MessageBox.Show("ID_KANGK di UcNoPolAngk.construct = " + this.ID_KANGK, "debug message");

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
        } 
   
        #endregion

        #region Set Colom Grid dan Pencarian
        protected override void ucDetail_Load(object sender, EventArgs e)
        {
          this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
          this.gvUcDtl.OptionsBehavior.Editable = false;
          this.binder = new BindingSource();
          this.binder.DataSource = typeof(SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL);
          this.gcUcDtl.DataSource = binder;
          this.setKolom("No", "NUM", "NUM", 0, false,50);
          this.setKolom("No Polisi", "NO_POLISI", "NO_POLISI", 1, true,100);
          this.setKolom("Berlaku Mulai", "TGL_KELUAR", "TGL_KELUAR", 2, true,150, "date");
          this.setKolom("Berlaku Sampai Dengan", "TGL_SD_BERLAKU", "TGL_SD_BERLAKU", 3, true,150 , "date");
          this.setKolom("Keterangan", "KET", "KET", 4, true,200);
          this.gridDoubleClickDetail = true;

          this.show_record = true;
          this.initGrid();
          this.getInitNoPolAngk();
          gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }
        #endregion

        #region Crud Operation

     

        public void getInitNoPolAngk(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcNoPolAngkSelect.InputParameters parInp = new SvcNoPolAngkSelect.InputParameters();
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
            fetchData = new SvcNoPolAngkSelect.call_pttClient(konfigApp.SvcNoPolAngkSelect_name,konfigApp.SvcNoPolAngkSelect_address);
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

        private delegate void ShowData(SvcNoPolAngkSelect.OutputParameters dataOut);

        public void showData(SvcNoPolAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KANGK_NOPOL.Count();


            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KANGK_NOPOL[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KANGK_NOPOL[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KANGK_NOPOL[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KANGK_NOPOL[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date1 = Convert.ToString(serviceOutPut.SF_ROW_KANGK_NOPOL[i].TGL_SD_BERLAKU).Substring(0, 10);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_KANGK_NOPOL[i].TGL_KELUAR).Substring(0, 10);
              string date1_ = Convert.ToString(serviceOutPut.SF_ROW_KANGK_NOPOL[i].TGL_SD_BERLAKU).Substring(0, 8);
              string date2_ = Convert.ToString(serviceOutPut.SF_ROW_KANGK_NOPOL[i].TGL_KELUAR).Substring(0, 8);
              if (date1 == "11/11/1000" || date1_ == "1/1/0001")
                {
                    serviceOutPut.SF_ROW_KANGK_NOPOL[i].TGL_SD_BERLAKU = null;
                }

              if (date2 == "11/11/1000" || date2_ == "1/1/0001")
                {
                    serviceOutPut.SF_ROW_KANGK_NOPOL[i].TGL_KELUAR = null;
                }

                binder.Add(serviceOutPut.SF_ROW_KANGK_NOPOL[i]);
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
                       
        #region Controller 
        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          PU = new FormNoPolAngk('C', selectedData, this.ID_KANGK);
          StatusCrud = "input";
          PU.ShowDialog();
          // MessageBox.Show("nilai ID_KANGK di form input " + this.ID_KANGK, "debug_message");
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                if (this.Status == "detail")
                {
                  // PU = new FormNoPolAngk('V', this.ID_KANGK, selectedData, this);
                  PU = new FormNoPolAngk('V', selectedData, this.ID_KANGK);
                    StatusCrud = "detail";
                }
                else
                {
                  PU = new FormNoPolAngk('U', selectedData, this.ID_KANGK);
                    StatusCrud = "edit";
                }
                PU.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            initGrid();
            getInitNoPolAngk();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            getInitNoPolAngk();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.selectedData != null)
            {
                if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.selectedData.NUM + " ?", konfigApp.judulHapusData,
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                  PU = new FormNoPolAngk('D', selectedData, this.ID_KANGK);
                    StatusCrud = "hapus";
                    this.PU.Show();
                    this.PU.Hide();
                    this.PU.Size = new System.Drawing.Size(0, 0);
                    this.PU.Opacity = 0; 
                }
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
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcNoPolAngkSelect.InputParameters parInp = new SvcNoPolAngkSelect.InputParameters();
                parInp.STR_WHERE = get_where_clause(nama_kolom, opr, parameter, parameter_2) + " AND ID_KANGK = " + this.ID_KANGK;

                konfigApp.currentMaks = konfigApp.dataAkhir;
                konfigApp.currentMin = konfigApp.dataAwal;

                parInp.P_MAX = konfigApp.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = konfigApp.currentMin;
                parInp.P_MINSpecified = true;
                this.dataInisial = true;
                fetchData = new SvcNoPolAngkSelect.call_pttClient();
                fetchData.Open();
                fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
                this.nonAktifForm("");
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
                case "No BPKB":
                    where = "Upper(NO_BPKB) " + get_operator("String", opr, parameter, parameter2);
                    break;

                case "Berlaku Mulai":
                    where = "Upper(TGL_KELUAR) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Berlaku Sampai Dengan":
                    where = "Upper(TGL_SD_BERLAKU) " + get_operator("Date", opr, parameter, parameter2);
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
                selectedData = (SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL)selectedView.GetRow(e.FocusedRowHandle);
            }
        }

        protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
        {
            string nama_kolom = this.LuKolom.EditValue.ToString();
            this.LuKolom.Width = 40 + (((int)nama_kolom.Length) * 5);

            switch (nama_kolom)
            {
                case "Berlaku Mulai":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Berlaku Sampai Dengan":
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


        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        protected void gvUcDtl_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }


    }
}
