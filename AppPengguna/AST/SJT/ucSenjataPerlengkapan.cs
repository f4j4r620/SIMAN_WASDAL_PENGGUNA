using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.SJT
{
    class ucSenjataPerlengkapan : UserControlDetail
    {
        private SvcSenjataPerlengkapanSelect.call_pttClient fetchData;
        private SvcSenjataPerlengkapanSelect.InputParameters parInp;
        private SvcSenjataPerlengkapanSelect.OutputParameters outDat;
        private SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP selectedData;

        SvcSenjataPerlengkapanCrud.call_pttClient svcSenjataPerlengkapanCrud = null;
        SvcSenjataPerlengkapanCrud.OutputParameters doutCrud = null;

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;
        SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP newRow;
        private ColumnView View;
        private PuSenjataPerlengkapan PU;
        private decimal? ID_KSENJ;

   

        private string Status;
        private string StatusCrud = "";
        private decimal? NUM;

        private void initSenjataPerlengkapan()
        {


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            this.setKolom("Nomor", "NUM", "NUM", 0);
            this.setKolom("Nama Perlengkapan", "NM_PERLENGKAP", "NM_PERLENGKAP", 1, true, 210);
            int width=this.setKolom("Keterangan", "KET", "KET", 2, true);

           this.gridDoubleClickDetail = true;
           this.show_record = true;
        }


       

        //bila di pasang pada detail Master
        public ucSenjataPerlengkapan(decimal? id_kbdg = null, string status= "edit")
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KSENJ = id_kbdg;
            this.Status = status;

            initSenjataPerlengkapan();
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

           
            this.initGrid();
            this.getinitSenjataPerlengkapan();


        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP)selectedView.GetRow(e.RowHandle);


        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bbTambah.Caption == konfigApp.labelTambah)
            {
                StatusCrud = "input";
                PU = new PuSenjataPerlengkapan("input", this.ID_KSENJ, null);
                PU.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                PU.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                PU.simpanSenjataPerlengkapan = new SimpanSenjataPerlengkapan(this.simpanSenjataPerlengkapan);
                PU.ShowDialog();

            }
            else
            {
                this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                this.gvUcDtl.OptionsBehavior.Editable = false;
                this.bbTambah.Glyph = AppPengguna.Properties.Resources.tombol_tambah;
                this.bbTambah.Caption = konfigApp.labelTambah;


            }

        }
        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    PU = new PuSenjataPerlengkapan("detail", selectedData);
                    StatusCrud = "detail";
                }
                else
                {

                    PU = new PuSenjataPerlengkapan("edit", selectedData);
                    StatusCrud = "edit";
                }
                PU.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                PU.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                PU.simpanSenjataPerlengkapan = new SimpanSenjataPerlengkapan(this.simpanSenjataPerlengkapan);

                PU.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getinitSenjataPerlengkapan();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getinitSenjataPerlengkapan();
        }

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
                this.getinitSenjataPerlengkapan(get_where_clause(nama_kolom, opr, parameter, parameter_2));
               

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

                case "Nama Perlengkapan":
                    where = "Upper(NM_PERLENGKAP) " + get_operator("String", opr, parameter, parameter2);
                    break;

                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
               
                default:
                    break;
            }
            return where;
        }

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {


        }


        protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData == null)
            {
                return;
            }
            if (MessageBox.Show(konfigApp.teksHapusData + "No " + this.selectedData.NUM + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    this.StatusCrud = "Hapus";
                    this.nonAktifForm("");
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    SvcSenjataPerlengkapanCrud.InputParameters parInp = new SvcSenjataPerlengkapanCrud.InputParameters();
                    parInp.P_ID_KSENJ = selectedData.ID_KSENJ;
                    parInp.P_ID_KSENJSpecified = true;
                    parInp.P_ID_KSENJ_PERLENGKAP = selectedData.ID_KSENJ_PERLENGKAP;
                    parInp.P_ID_KSENJ_PERLENGKAPSpecified = true;
                    
                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcSenjataPerlengkapanCrud = new SvcSenjataPerlengkapanCrud.call_pttClient(konfigApp.SvcSenjataPerlengkapanCrud_name, konfigApp.SvcSenjataPerlengkapanCrud_address);
                    svcSenjataPerlengkapanCrud.Open();
                    svcSenjataPerlengkapanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                }
            }
        }


        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP)selectedView.GetRow(e.FocusedRowHandle);
                if (selectedView.IsLastRow)
                {
                    LastRow = true;
                }
                else
                {
                    LastRow = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
                //this.ID_ESELON1 = SelectedData.ID_ESELON1;
                //this.DataName = SelectedData.UR_ESELON1;
                // System.Diagnostics.Debug.WriteLine("hahahah", this.KdKorWil);
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

        protected void getinitSenjataPerlengkapan(string _where = null)
        {
            decimal Max, Min;
            
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcSenjataPerlengkapanSelect.InputParameters parInp = new SvcSenjataPerlengkapanSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KSENJ = {0} {1}", this.ID_KSENJ, _where);
            fetchData = new SvcSenjataPerlengkapanSelect.call_pttClient(konfigApp.SvcSenjataPerlengkapanSelect_name, konfigApp.SvcSenjataPerlengkapanSelect_address);
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outDat = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
               
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);

                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcSenjataPerlengkapanSelect.OutputParameters dataOut);

        public void showData(SvcSenjataPerlengkapanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KSENJ_PERLENGKAP.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KSENJ_PERLENGKAP[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KSENJ_PERLENGKAP[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KSENJ_PERLENGKAP[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KSENJ_PERLENGKAP[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_KSENJ_PERLENGKAP[i]);
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

        private void simpanSenjataPerlengkapan(SvcSenjataPerlengkapanCrud.InputParameters parInp)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();

                this.nonAktifForm("");
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                svcSenjataPerlengkapanCrud = new SvcSenjataPerlengkapanCrud.call_pttClient(konfigApp.SvcSenjataPerlengkapanCrud_name, konfigApp.SvcSenjataPerlengkapanCrud_address);
                svcSenjataPerlengkapanCrud.Open();
                svcSenjataPerlengkapanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
            }
            catch (Exception)
            {

                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }


        }
        private void prosesCrud(IAsyncResult result)
        {
            try
            {
                doutCrud = svcSenjataPerlengkapanCrud.Endexecute(result);
                svcSenjataPerlengkapanCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                bool berhasil = this.msg_get_database(doutCrud.PO_RESULT, "Y", 0);
                if (berhasil == true)
                {
                    this.Invoke(new ShowDataCrud(this.showDataCrud), doutCrud);
                    if (this.modeCrud == 'U')
                    {
                        this.Invoke(new TutupPopUp(this.tutupPopUp), "");
                    }
                }

            }
            catch
            {
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void ShowDataCrud(SvcSenjataPerlengkapanCrud.OutputParameters dataOut);

        private delegate void TutupPopUp(string data);

        public void tutupPopUp(string data)
        {
            this.PU.Close();
            this.bbRefresh.PerformClick();
        }
        public void showDataCrud(SvcSenjataPerlengkapanCrud.OutputParameters outCrud)
        {
            SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP dataPenyama = new SvcSenjataPerlengkapanSelect.BPSIMANSROW_KSENJ_PERLENGKAP();
            dataPenyama.ID_KSENJ_PERLENGKAP = outCrud.PO_ID_KSENJ_PERLENGKAP;

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            switch (this.modeCrud)
            {
                case 'C':
                     
                        this.dataInisial = false;
                        this.getById = true;
                        this.getinitSenjataPerlengkapan(" ID_KSENJ_PERLENGKAP = " + outCrud.PO_ID_KSENJ_PERLENGKAP.ToString());
                    
                    break;
                case 'U':
                    this.binder.Remove(selectedData);
                    this.dataInisial = false;
                        this.getById = true;
                        this.getinitSenjataPerlengkapan(" ID_KSENJ_PERLENGKAP = " + selectedData.ID_KSENJ_PERLENGKAP.ToString());
                    break;
                case 'D':
                    this.binder.Remove(selectedData);
                    this.gvUcDtl.RefreshData();
                    this.StrTotalGrid.Caption = (Convert.ToInt64(this.StrTotalGrid.Caption) - 1).ToString();
                    this.StrTotalDb.Caption = (Convert.ToInt64(this.StrTotalDb.Caption) - 1).ToString();
                    break;
            }

        }

        

    }
}
