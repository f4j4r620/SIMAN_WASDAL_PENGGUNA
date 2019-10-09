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

namespace AppPengguna.AST.JJ
{
    public class UcGpsJlnJmbtn:UserControlDetail
    {
        private SvcNoPolAngkSelect.call_pttClient fetchData;
        private SvcNoPolAngkSelect.InputParameters parInp;
        private SvcNoPolAngkSelect.OutputParameters outDat;
        private SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL selectedData;
        private bool isSearch = false;
        private bool initiated = false;
        private FrmKoorSatker gpForm;
         private FormLokTnhBngn frmLokTnhBngunan;

        public UcGpsJlnJmbtn(FrmKoorSatker _gpForm) {
            this.gpForm = _gpForm;
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

        private UcGpsJlnJmbtn()
            :base()
        {}

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.setKolom("No", "NUM", "NUM", 0, false,50);
            this.setKolom("No Polisi", "NO_POLISI", "NO_POLISI", 1, true,150);
            this.setKolom("Berlaku Mulai", "TGL_KELUAR", "TGL_KELUAR", 2, true,150);
            this.setKolom("Berlaku Sampai Dengan", "TGL_SD_BERLAKU", "TGL_SD_BERLAKU", 3, true,150);
            this.setKolom("Keterangan", "KET", "KET", 4, true,200);
            this.setKolom("Terakhir(Y/T)", "TERAKHIR_YN", "TERAKHIR_YN", 5, true,150);
            this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.gvUcDtl.OptionsBehavior.Editable = false;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL);
            this.gcUcDtl.DataSource = binder;
        }

        private string getConvCol(string colSearch)
        {
            string kolom = "";
            if (colSearch == "No Polisi")
            {
                kolom = "NO_POLISI";
            }
            else if (colSearch == "Berlaku Mulai")
            {
                kolom = "TGL_KELUAR";
            }
            else if (colSearch == "Berlaku Sampai Dengan")
            {
                kolom = "TGL_SD_BERLAKU";
            }

            return kolom;
        }

        protected void gvUcDtl_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        protected void setKolom(string caption, string fieldName, string Name, Int32 urutan, bool setDdItem = false, bool readOnly = false, bool visible = true, int switcher = 0)
        {
            this.kolom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvUcDtl.Columns.Add(this.kolom);
            switch (switcher)
            {
                case 0:
                    break;
                case 1:
                    this.kolom.ColumnEdit = this.repositoryItemButtonEdit1;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }

            this.kolom.Visible = visible;
            this.kolom.OptionsColumn.ReadOnly = readOnly;
            this.kolom.Caption = caption;
            this.kolom.FieldName = fieldName;
            this.kolom.Name = Name;
            this.kolom.Visible = true;
            this.kolom.VisibleIndex = urutan;
            if (setDdItem)
            {
                this.LuKolomItems.Items.Add(caption);

            }
        }

        public void getInitNoPolAngk(string where = "")
        {
            myThread = new Thread(new ThreadStart(gpForm.ShowProgresBar));
            myThread.Start();
            SvcNoPolAngkSelect.InputParameters parInp = new SvcNoPolAngkSelect.InputParameters();
            parInp.P_COL = "";
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
            }
            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.STR_WHERE = where;
            parInp.P_SORT = "DESC";
            fetchData = new SvcNoPolAngkSelect.call_pttClient();
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outDat = fetchData.Endexecute(result);
                fetchData.Close();
               // this.Invoke(new TutupProgresBar(konfigApp.tutupProgresBar), myThread);
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new AktifkanForm(gpForm.aktifkanForm), "");
            }
        }

        private delegate void ShowData(SvcNoPolAngkSelect.OutputParameters dataOut);

        public void showData(SvcNoPolAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KANGK_NOPOL.Count();

            if (this.dataInisial == true)
            {
                binder.Clear();
                this.dataInisial = false;
            }

            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_KANGK_NOPOL[i]);
            }

            if (jmlDataGroup < 5)
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

        }
       
        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmLokTnhBngunan = new FormLokTnhBngn('C', null);
            //frmLokTnhBngunan.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmLokTnhBngunan = new FormLokTnhBngn('U', selectedData);
            //frmLokTnhBngunan.ShowDialog();
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
            //formNopol = new FormNoPolAngk('D', selectedData);
            //formNopol.ShowDialog();
        }

        protected override void bbSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string where = "";
            string kolom = "";
            string textSearch1 = "";
            string textSearch2 = "";

            kolom = getConvCol((string)LuKolom.EditValue);
            textSearch1 = (string)teSearch.EditValue;

            if ((textSearch1 != "") && (kolom != ""))
            {
                if ((kolom == "TGL_KELUAR") || (kolom == "TGL_SD_BERLAKU"))
                {
                    where = "(" + kolom + ">=" + "'" + textSearch1 + "'" + ")";
                    if (textSearch2 != "")
                    {
                        textSearch2 = (string)teSearch2.EditValue;
                        where += "AND(" + kolom + "<=" + "'" + textSearch2 + "'" + ")";
                    }
                }
                else
                {
                    where = "UPPER(" + kolom + ") LIKE " + "'%" + textSearch1.ToUpper() + "%'";
                }

                initGrid();
                this.getInitNoPolAngk(where);
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
                selectedData = (SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL)selectedView.GetRow(e.FocusedRowHandle);
            }
        }

        protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
        {
            if (((string)LuKolom.EditValue == "Berlaku Mulai") || ((string)LuKolom.EditValue == "Berlaku Sampai Dengan"))
            {
                teSearch2.Visibility = BarItemVisibility.Always;
            }
            else
            {
                teSearch2.Visibility = BarItemVisibility.Never;
            }
        }
    }
}
