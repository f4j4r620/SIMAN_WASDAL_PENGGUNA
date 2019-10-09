using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.AK
{
    public class UcBpkbAngk:UserControlDetail
    {   
        private SvcBpkbAngkSelect.call_pttClient fetchData;
        private SvcBpkbAngkSelect.InputParameters parInp;
        private SvcBpkbAngkSelect.OutputParameters outDat;
        public SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB selectedData;
        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------

        //public AktifkanForm aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        private bool isSearch = false;
        private bool initiated = false;

        private FormBpkbAngk PU;
  
        private ArrayList colSearch;
        private decimal? jmlData = 0;
        public decimal? ID_KANGK;
        public string Status;
        public string StatusCrud = "";
        private decimal? NUM;

        public UcBpkbAngk(decimal? _ID_KANGK, string _status) : base()
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

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
          // MessageBox.Show("ucBpkbAngk.ucDetail_Load > ID_KANGK = " + this.ID_KANGK, "debug message");
          
          colSearch = new ArrayList();
            
            this.setKolom("No", "NUM", "NUM", 0, false,50);
            this.setKolom("No BPKB", "NO_BPKB", "NO_BPKB", 1, true,100);
            this.setKolom("Berlaku Mulai", "TGL_KELUAR", "TGL_KELUAR", 2, true,150,"date");
            this.setKolom("Berlaku Sampai Dengan", "TGL_SD_BERLAKU", "TGL_SD_BERLAKU", 3, true,150,"date");
            this.setKolom("Keterangan", "KET", "KET", 4, false,200);
            this.setKolom("Terakhir(Y/T)", "TERAKHIR_YN", "TERAKHIR_YN", 5, false,100,"string",true);
            this.setKolom("File", "NMFILE", "NMFILE", 7, true, 100, "string");
           
            this.gridDoubleClickDetail = true;

            this.gvUcDtl.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.gvUcDtl.OptionsBehavior.Editable = false;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB);
            this.gcUcDtl.DataSource = binder;
            
            this.show_record = true;
            this.initGrid();
            this.getInitBpkbAngk();



            this.btnMap.Enabled = false;

            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);

            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        protected void gvUcDtl_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        public void getInitBpkbAngk(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcBpkbAngkSelect.InputParameters parInp = new SvcBpkbAngkSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format("ID_KANGK = {0} {1}", this.ID_KANGK, _where);
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

            // Debug.WriteLine("jmlDataGroup = " + jmlDataGroup);
            // jmlData = jmlDataGroup;
            // Debug.WriteLine("jmlData = " + jmlDataGroup);



            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KANGK_BPKB[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KANGK_BPKB[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KANGK_BPKB[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KANGK_BPKB[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date1 = Convert.ToString(serviceOutPut.SF_ROW_KANGK_BPKB[i].TGL_SD_BERLAKU).Substring(0, 10)  ;
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_KANGK_BPKB[i].TGL_KELUAR).Substring(0, 10) ;
              if (date1 == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_KANGK_BPKB[i].TGL_SD_BERLAKU = null;
                }
              if (date2 == "11/11/1000")
                {
                    serviceOutPut.SF_ROW_KANGK_BPKB[i].TGL_KELUAR = null;
                }
                binder.Add(serviceOutPut.SF_ROW_KANGK_BPKB[i]);
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
       
        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e) { 
            
        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            PU = new FormBpkbAngk('C', this.ID_KANGK, selectedData,this);
            StatusCrud = "input";
            PU.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    PU = new FormBpkbAngk('V', this.ID_KANGK, selectedData,this);
                    StatusCrud = "detail";
                }
                else
                {
                    PU = new FormBpkbAngk('U', this.ID_KANGK, selectedData,this);
                    StatusCrud = "edit";
                }
                PU.ShowDialog();
            }
        
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            initGrid();
            getInitBpkbAngk();
            
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.dataInisial = false;
            getInitBpkbAngk();
        }

        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (this.selectedData != null)
            {
                if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.selectedData.NUM + " ?", konfigApp.judulHapusData,
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    StatusCrud = "hapus";
                    PU = new FormBpkbAngk('D', this.ID_KANGK, selectedData, this);
                    this.PU.Show();
                    this.PU.Hide();
                    this.PU.Size = new System.Drawing.Size(0, 0);
                    this.PU.Opacity = 0;                    
                    
                }
            }
        }

        protected override void bbSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
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
                this.getInitBpkbAngk(get_where_clause(nama_kolom, opr, parameter, parameter_2));

                
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
                case "Terakhir(Y/T)":
                    where = "Upper(TERAKHIR_YN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e) { 
        }

        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) 
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcBpkbAngkSelect.BPSIMANSROW_KANGK_BPKB)selectedView.GetRow(e.FocusedRowHandle);

                /*
                 * revised by rfydh @ 20140418
                 * note : aktif tidaknya button lihat dokumen, berdasarkan check per row record, validasi col FILE_EXISTS
                 */

                if (selectedData.FILE_EXISTS > 0)
                {
                    this.btnMap.Enabled = true;
                }
                else
                {
                    this.btnMap.Enabled = false;
                }
            }
        }

        protected override void LuKolom_EditValueChanged(object sender, EventArgs e) {
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

        #region View Dokumen
        protected void view_dokumen(object sender, ItemClickEventArgs e)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = selectedData.ID_KANGK_BPKB;
                parInp.P_ID_TABLE = "ID_KANGK_BPKB";
                parInp.P_IDSpecified = true;
                parInp.P_TABLE = "M_KANGK_BPKB";
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

    }
}
