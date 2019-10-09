using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.BG
{
    class ucDruanganBangunan : UserControlDetail
    {
        private SvcDruanganBangunanSelect.call_pttClient fetchData;
        private SvcDruanganBangunanSelect.InputParameters parInp;
        private SvcDruanganBangunanSelect.OutputParameters outDat;
        private SvcDruanganBangunanSelect.BPSIMANSROW_M_KBDG_DETAIL selectedData;

        SvcDruanganBangunanCrud.call_pttClient svcDruanganBangunanCrud = null;
        SvcDruanganBangunanCrud.OutputParameters doutCrud = null;

        //public NonAktifkanFormSatker nonAktifForm;
        //public AktifkanFormSatker aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        SvcDruanganBangunanSelect.BPSIMANSROW_M_KBDG_DETAIL newRow;
        private ColumnView View;
        private PuDruanganBangunan PU;
        private decimal? ID_KBDG;
        private decimal? ID_SATKER;
        private string StatusCrud = "";
        private decimal? NUM;
        private string Status;
        //child form customization 
        private void initDokBangunan()
        {


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcDruanganBangunanSelect.BPSIMANSROW_M_KBDG_DETAIL);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            this.setKolom("Nomor", "NUM", "NUM", 0);
            this.setKolom("Lantai", "LANTAI", "LANTAI", 1, true, 140);
            this.setKolom("Kode Ruangan", "KD_LOKRUANG", "KD_LOKRUANG", 2, true);
            this.setKolom("Tipe Ruangan", "TYPE_RUANG", "TYPE_RUANG", 3, true);
            this.setKolom("Luas", "LUAS", "LUAS", 4, true,120, "integer");
            int width= this.setKolom("Keterangan", "KET", "KET", 5, true,200);
            this.SetGridSize(width, 0);
            this.gridDoubleClickDetail = true;
            this.show_record = true;
            this.gvUcDtl.BestFitColumns();
            
        }


        //bila di pasang pada FormAsset


        //bila di pasang pada detail Master
        public ucDruanganBangunan(decimal? id_satker,decimal? id_kbdg, string status)
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_SATKER = id_satker;
            this.ID_KBDG = id_kbdg;
            this.Status = status;
            initDokBangunan();
            if (this.Status == "detail")
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

        //bila di pasang pada detail Master
     
        protected override void ucDetail_Load(object sender, EventArgs e)
        {

         
            this.bbTambah.Caption = konfigApp.labelTambah;
            this.initGrid();
            this.getinitDokBangunan();

            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcDruanganBangunanSelect.BPSIMANSROW_M_KBDG_DETAIL)selectedView.GetRow(e.RowHandle);


        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


                PU = new PuDruanganBangunan("input", this.ID_SATKER, this.ID_KBDG,null);
                StatusCrud = "input";
                PU.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                PU.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                PU.simpanDruanganBangunan = new SimpanDruanganBangunan(this.simpanDruanganBangunan);
              
                PU.ShowDialog();

       

        }
        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    PU = new PuDruanganBangunan("detail",this.ID_SATKER, selectedData);
                    StatusCrud = "detail";
                }
                else
                {
                    PU = new PuDruanganBangunan("edit", this.ID_SATKER, selectedData);
                    StatusCrud = "edit";
                }
                PU.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                PU.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                PU.simpanDruanganBangunan = new SimpanDruanganBangunan(this.simpanDruanganBangunan);

                PU.ShowDialog();
            }
            
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getinitDokBangunan();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getinitDokBangunan();
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
                this.getinitDokBangunan(get_where_clause(nama_kolom, opr, parameter, parameter_2));
             

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

                case "Lantai":
                    where = "Upper(LANTAI) " + get_operator("Float", opr, parameter, parameter2);
                    break;

                case "Kode Ruangan":
                    where = "Upper(KD_LOKRUANG) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Luas":
                    where = "Upper(LUAS) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Tipe Ruangan":
                    where = "Upper(TYPE_RUANG) " + get_operator("String", opr, parameter, parameter2);
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
                    this.StatusCrud = "hapus";
                    this.nonAktifForm("");
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    SvcDruanganBangunanCrud.InputParameters parInp = new SvcDruanganBangunanCrud.InputParameters();
                    parInp.P_ID_KBDG = selectedData.ID_KBDG;
                    parInp.P_ID_KBDGSpecified = true;
                    parInp.P_ID_KBDG_DETAIL = selectedData.ID_KBDG_DETAIL;
                    parInp.P_ID_KBDG_DETAILSpecified = true;
                    
                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcDruanganBangunanCrud = new SvcDruanganBangunanCrud.call_pttClient(konfigApp.SvcDruanganBangunanCrud_name, konfigApp.SvcDruanganBangunanCrud_address);
                    svcDruanganBangunanCrud.Open();
                    svcDruanganBangunanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
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
                selectedData = (SvcDruanganBangunanSelect.BPSIMANSROW_M_KBDG_DETAIL)selectedView.GetRow(e.FocusedRowHandle);
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

        protected void getinitDokBangunan(string _where = null)
        {
            decimal Max, Min;
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcDruanganBangunanSelect.InputParameters parInp = new SvcDruanganBangunanSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format("ID_KBDG = {0} {1}", this.ID_KBDG, _where);
            fetchData = new SvcDruanganBangunanSelect.call_pttClient(konfigApp.SvcDruanganBangunanSelect_name, konfigApp.SvcDruanganBangunanSelect_address);
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

        private delegate void ShowData(SvcDruanganBangunanSelect.OutputParameters dataOut);

        public void showData(SvcDruanganBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBDG_DETAIL.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KBDG_DETAIL[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KBDG_DETAIL[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_DETAIL[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_DETAIL[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KBDG_DETAIL[i]);
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

        private void simpanDruanganBangunan(SvcDruanganBangunanCrud.InputParameters parInp)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();

                this.nonAktifForm("");
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                svcDruanganBangunanCrud = new SvcDruanganBangunanCrud.call_pttClient(konfigApp.SvcDruanganBangunanCrud_name, konfigApp.SvcDruanganBangunanCrud_address);
                svcDruanganBangunanCrud.Open();
                svcDruanganBangunanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
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
                doutCrud = svcDruanganBangunanCrud.Endexecute(result);
                svcDruanganBangunanCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                bool berhasil = this.msg_get_database(doutCrud.PO_RESULT, "Y", 0);
                if (berhasil == true)
                {
                    this.Invoke(new ShowDataCrud(this.showDataCrud), doutCrud);
                    this.Invoke(new TutupPopUp(this.tutupPopUp), "");
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

        private delegate void ShowDataCrud(SvcDruanganBangunanCrud.OutputParameters dataOut);

        private delegate void TutupPopUp(string data);

        public void tutupPopUp(string data)
        {
            this.PU.Close();
        }
        public void showDataCrud(SvcDruanganBangunanCrud.OutputParameters dataOut)
        {
            SvcDruanganBangunanSelect.BPSIMANSROW_M_KBDG_DETAIL dataPenyama = new SvcDruanganBangunanSelect.BPSIMANSROW_M_KBDG_DETAIL();
            dataPenyama.ID_KBDG_DETAIL = dataOut.PO_ID_KBDG_DETAIL;

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            switch (this.modeCrud)
            {
                case 'C':
                    this.dataInisial = false;
                    this.getById = true;
                    this.getinitDokBangunan(" ID_KBDG_DETAIL = " + dataOut.PO_ID_KBDG_DETAIL.ToString());
                    break;
                case 'U':
                    this.binder.Remove(selectedData);
                    this.dataInisial = false;
                    this.getById = true;
                    this.search = "";
                    this.initGrid();
                    this.getinitDokBangunan();
                    //this.getinitDokBangunan(" ID_KBDG_DETAIL = " + dataOut.PO_ID_KBDG_DETAIL.ToString());
                    this.dataInisial = true;
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
