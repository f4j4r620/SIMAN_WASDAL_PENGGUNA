using System;
using System.Collections;
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
    public class UcNoPolisiAngk : UserControlDetail
    {
        private SvcBpkbAngkSelect.call_pttClient fetchData;
        private SvcBpkbAngkSelect.InputParameters parInp;
        private SvcBpkbAngkSelect.OutputParameters outDat;
        private SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB selectedData;
 
        private bool isSearch = false;
        private bool initiated = false;

        private FormNoPolAngk formNoPol;
  
        private ArrayList colSearch;

   
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

        public decimal? ID_KANGK;
        public string Status;
        private UcNoPolisiAngk(decimal? _ID_KANGK, string _status)
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
            this.setKolom("No", "NUM", "NUM", 0, false, 50);
            this.setKolom("No BPKB", "NO_BPKB", "NO_BPKB", 1, true, 100);
            this.setKolom("Berlaku Mulai", "TGL_KELUAR", "TGL_KELUAR", 2, true, 150,"date");
            this.setKolom("Berlaku Sampai Dengan", "TGL_SD_BERLAKU", "TGL_SD_BERLAKU", 3, true, 150,"date");
            this.setKolom("Keterangan", "KET", "KET", 4, true);
            this.gridDoubleClick = true;

            this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.gvUcDtl.OptionsBehavior.Editable = false;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB);
            this.gcUcDtl.DataSource = binder;

            this.initGrid();
            this.getInitNoPolisiAngk();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

    

        protected void gvUcDtl_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

   

        public void getInitNoPolisiAngk(string where = "")
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcBpkbAngkSelect.InputParameters parInp = new SvcBpkbAngkSelect.InputParameters();
            parInp.P_COL = "";
            if (this.dataInisial == true)
            {
                konfigApp.currentMaks = konfigApp.dataAkhir;
                konfigApp.currentMin = konfigApp.dataAwal;
            }
            else
            {
                konfigApp.currentMin = konfigApp.currentMaks + 1;
                konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataAkhir;
            }
            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = konfigApp.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = konfigApp.currentMin;
            parInp.P_MINSpecified = true;
            parInp.STR_WHERE = " ID_KANGK = " + this.ID_KANGK + " " + where;
            parInp.P_SORT = "DESC";
            fetchData = new SvcBpkbAngkSelect.call_pttClient(konfigApp.SvcBpkbAngkSelect_name,konfigApp.SvcBpkbAngkSelect_address);
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

        private delegate void ShowData(SvcBpkbAngkSelect.OutputParameters dataOut);

        public void showData(SvcBpkbAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KANGK_BPKB.Count();

            if (this.dataInisial == true)
            {
                binder.Clear();
                this.dataInisial = false;
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date1 = Convert.ToString(serviceOutPut.SF_ROW_KANGK_BPKB[i].TGL_KELUAR).Substring(0, 10);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_KANGK_BPKB[i].TGL_SD_BERLAKU).Substring(0, 10);
              if (date1 == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_KANGK_BPKB[i].TGL_KELUAR = null;
                }

              if (date2 == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_KANGK_BPKB[i].TGL_SD_BERLAKU = null;
                }

                binder.Add(serviceOutPut.SF_ROW_KANGK_BPKB[i]);
            }

            if (jmlDataGroup < konfigApp.dataAkhir)
            {
                this.loadMore = false;
                this.bbMore.Enabled = false;
                if (isSearch)
                {
                    //this.bbSearch.Enabled = false; 
                }
            }
            else
            {
                this.loadMore = true;
                this.bbMore.Enabled = true;
                if (isSearch)
                {
                    // this.bbSearch.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph"))); ;
                }
            }
            this.gvUcDtl.BestFitColumns();
        }

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            formNoPol = new FormNoPolAngk('C', null, this.ID_KANGK);
            formNoPol.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          //  formNoPol = new FormNoPolAngk('U',selectedData);
          //  formNoPol.ShowDialog();
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            initGrid();
            getInitNoPolisiAngk();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            getInitNoPolisiAngk();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          //  formNoPol = new FormNoPolAngk('D', selectedData);
          //  formNoPol.ShowDialog();
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
                SvcBpkbAngkSelect.InputParameters parInp = new SvcBpkbAngkSelect.InputParameters();
                parInp.STR_WHERE = get_where_clause(nama_kolom, opr, parameter, parameter_2) + " AND ID_KANGK = " + this.ID_KANGK;

                konfigApp.currentMaks = konfigApp.dataAkhir;
                konfigApp.currentMin = konfigApp.dataAwal;

                parInp.P_MAX = konfigApp.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = konfigApp.currentMin;
                parInp.P_MINSpecified = true;
                this.dataInisial = true;
                fetchData = new SvcBpkbAngkSelect.call_pttClient();
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
                selectedData = (SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB)selectedView.GetRow(e.FocusedRowHandle);
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
    }
}
