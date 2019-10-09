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
    class ucKonstruksiBangunan : UserControlDetail
    {
        private SvcKonstruksiBangunanSelect.call_pttClient fetchData;
        private SvcKonstruksiBangunanSelect.InputParameters parInp;
        private SvcKonstruksiBangunanSelect.OutputParameters outDat;
        private SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG selectedData;

        SvcKonstruksiBangunanCrud.call_pttClient svcGPSBangunanCrud = null;
        SvcKonstruksiBangunanCrud.OutputParameters doutCrud = null;

        //public NonAktifkanFormSatker nonAktifForm;
        //public AktifkanFormSatker aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG newRow;
        private ColumnView View;
        private PuKonstruksiBangunan PU;
        private decimal? ID_KBDG;
        private decimal? ID_M_KBDG_KONS_BDG;
   

        private string Status;
        private string StatusCrud = "";
        private decimal? NUM;
        private void initDokBangunan()
        {


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            this.setKolom("Nomor", "NUM", "NUM", 0);
            this.setKolom("Tgl Inventarisasi", "TGL_INV", "TGL_INV", 1, true, 120,"date");
            this.setKolom("Struktur Atap", "STR_ATAP", "STR_ATAP", 2, true);
            this.setKolom("Struktur Ruangan", "STR_RANGKA", "STR_RANGKA", 3, true,210);
            this.setKolom("Material Atap", "MATERIAL_ATAP", "MATERIAL_ATAP", 4, true);
            this.setKolom("Material Dinding", "MATRIAL_DINDING", "MATRIAL_DINDING", 5, true);
            this.setKolom("Material Langit", "MATERIAL_LANGIT", "MATERIAL_LANGIT", 6, true);
            this.setKolom("Lantai", "LANTAI", "LANTAI", 7, true);
            this.setKolom("Pelapis Dinding Dalam", "PELAPIS_DINDIN_DLM", "PELAPIS_DINDIN_DLM", 8, true);
            this.setKolom("Pelapis Dinding Luar", "PELAPIS_DINDIN_LR", "PELAPIS_DINDIN_LR", 9, true);
            this.setKolom("Perkerasan", "PERKERASAN", "PERKERASAN", 10, true);
            this.setKolom("Pagar", "PAGAR", "PAGAR", 11, true);
            this.setKolom("Kondisi", "UR_KONDISI", "UR_KONDISI", 12, true);
            this.setKolom("File (Dok)", "NMFILE", "NMFILE", 13, true);
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
                parInp.P_TABLE = "M_KBDG_KONS_BDG";

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


        //bila di pasang pada FormAsset

        //bila di pasang pada detail Master
        public ucKonstruksiBangunan(decimal? id_kbdg = null, string status = "edit")
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KBDG = id_kbdg;
            this.Status = status;
            initDokBangunan();
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
            this.btnMap.Enabled = false;
        }

        //bila di pasang pada detail Master

        protected override void ucDetail_Load(object sender, EventArgs e)
        {

            this.gvUcDtl.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvUcDtl_InitNewRow);

           
            this.bbTambah.Caption = konfigApp.labelTambah;
            this.initGrid();
            this.getinitDokBangunan();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;

        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG)selectedView.GetRow(e.RowHandle);


        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bbTambah.Caption == konfigApp.labelTambah)
            {

                PU = new PuKonstruksiBangunan("input", this.ID_KBDG, null);
                this.StatusCrud = "input";
                PU.simpanKonstruksiBangunan = new SimpanKonstruksiBangunan(this.simpanKonstruksiBangunan);
                
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
                this.NUM = this.selectedData.NUM;
                if (this.Status == "detail")
                {
                    PU = new PuKonstruksiBangunan("detail", selectedData);
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new PuKonstruksiBangunan("edit", selectedData);
                    this.StatusCrud = "edit";
                }
                PU.simpanKonstruksiBangunan = new SimpanKonstruksiBangunan(this.simpanKonstruksiBangunan);

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
                case "Material Langit":
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

                case "File (Dok)":
                    where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
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
                    SvcKonstruksiBangunanCrud.InputParameters parInp = new SvcKonstruksiBangunanCrud.InputParameters();
                    parInp.P_ID_KBDG = selectedData.ID_KBDG;
                    parInp.P_ID_KBDGSpecified = true;
                    parInp.P_ID_M_KBDG_KONS_BDG = selectedData.ID_M_KBDG_KONS_BDG;
                    parInp.P_ID_M_KBDG_KONS_BDGSpecified = true;

                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcGPSBangunanCrud = new SvcKonstruksiBangunanCrud.call_pttClient(konfigApp.SvcKonstruksiBangunanCrud_name, konfigApp.SvcKonstruksiBangunanCrud_address);
                    svcGPSBangunanCrud.Open();
                    svcGPSBangunanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
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
                selectedData = (SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG)selectedView.GetRow(e.FocusedRowHandle);
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
            string nama_kolom = this.LuKolom.EditValue.ToString();
            this.LuKolom.Width = 40 + (((int)nama_kolom.Length) * 5);
            switch (nama_kolom)
            {
                case "Tgl Inventarisasi":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
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
            SvcKonstruksiBangunanSelect.InputParameters parInp = new SvcKonstruksiBangunanSelect.InputParameters();
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
            if (this.dataInisial == true )
            {
                this.search = (_where != null) ? " AND " + _where : "";
                _where = this.search;
            }
            else if (getById == true)
            {
              _where = (_where != null) ? " AND " + _where : "";
            }
            else{
                _where = this.search;
            }
            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = String.Format("ID_KBDG = {0} {1}", this.ID_KBDG, _where);
            fetchData = new SvcKonstruksiBangunanSelect.call_pttClient(konfigApp.SvcKonstruksiBangunanSelect_name, konfigApp.SvcKonstruksiBangunanSelect_address);
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

        private delegate void ShowData(SvcKonstruksiBangunanSelect.OutputParameters dataOut);
  
        public void showData(SvcKonstruksiBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBDG_KONS_BDG.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KBDG_KONS_BDG[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KBDG_KONS_BDG[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_KONS_BDG[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_KONS_BDG[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_KONS_BDG[i].TGL_INV).Substring(0, 10);
              string date_ = Convert.ToString(serviceOutPut.SF_ROW_M_KBDG_KONS_BDG[i].TGL_INV).Substring(0, 8);

              if (date == "11/11/1000" || date_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KBDG_KONS_BDG[i].TGL_INV = null;
              }
                binder.Add(serviceOutPut.SF_ROW_M_KBDG_KONS_BDG[i]);
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

        private void simpanKonstruksiBangunan(SvcKonstruksiBangunanCrud.InputParameters parInp)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();

                this.nonAktifForm("");
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                svcGPSBangunanCrud = new SvcKonstruksiBangunanCrud.call_pttClient(konfigApp.SvcKonstruksiBangunanCrud_name, konfigApp.SvcKonstruksiBangunanCrud_address);
                svcGPSBangunanCrud.Open();
                svcGPSBangunanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
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
                doutCrud = svcGPSBangunanCrud.Endexecute(result);
                svcGPSBangunanCrud.Close();
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

        private delegate void ShowDataCrud(SvcKonstruksiBangunanCrud.OutputParameters dataOut);

        private delegate void TutupPopUp(string data);

        public void tutupPopUp(string data)
        {
            this.PU.Close();
        }
        public void showDataCrud(SvcKonstruksiBangunanCrud.OutputParameters dataOut)
        {
            SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG dataPenyama = new SvcKonstruksiBangunanSelect.BPSIMANSROW_M_KBDG_KONS_BDG();
            dataPenyama.ID_M_KBDG_KONS_BDG = dataOut.PO_ID_M_KBDG_KONS_BDG;

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            this.ID_M_KBDG_KONS_BDG = (this.StatusCrud == "edit")? selectedData.ID_M_KBDG_KONS_BDG: dataOut.PO_ID_M_KBDG_KONS_BDG ;
           
            switch (this.modeCrud)
            {
                case 'C':
                    if (this.PU.FilePath != null)
                    {
                        string Path = this.PU.FilePath;
                        simpanFile("ID_M_KBDG_KONS_BDG", this.ID_M_KBDG_KONS_BDG, "M_KBDG_KONS_BDG", Path, "C", PU.ID_JNSDOK);
                    }
                    else
                    {
                        this.dataInisial = false;
                        this.getById = true;
                        this.getinitDokBangunan(" ID_M_KBDG_KONS_BDG = " + this.ID_M_KBDG_KONS_BDG.ToString());

                    }
                    break;
                case 'U':
                    this.binder.Remove(this.selectedData);
                    if (this.PU.FilePath != null)
                    {
                        string Path = this.PU.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                          simpanFile("ID_M_KBDG_KONS_BDG", this.ID_M_KBDG_KONS_BDG, "M_KBDG_KONS_BDG", Path, "U", PU.ID_JNSDOK);
                        }
                        else
                        {
                          simpanFile("ID_M_KBDG_KONS_BDG", this.ID_M_KBDG_KONS_BDG, "M_KBDG_KONS_BDG", Path, "C", PU.ID_JNSDOK);
                        }
                    }
                    else
                    {
                        this.dataInisial = false;
                        this.getById = true;
                        this.getinitDokBangunan(" ID_M_KBDG_KONS_BDG = " + this.ID_M_KBDG_KONS_BDG.ToString());
                    
                    }
                    break;
                case 'D':
                    this.binder.Remove(selectedData);
                    gvUcDtl.RefreshData();
                    this.StrTotalGrid.Caption = (Convert.ToInt64(this.StrTotalGrid.Caption) - 1).ToString();
                    this.StrTotalDb.Caption = (Convert.ToInt64(this.StrTotalDb.Caption) - 1).ToString();
                    break;
            }

        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string FilePath, string SELECT, string id_jnsDok = null)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            try
            {
                SvcAsetDokCrud.InputParameters inputData = new SvcAsetDokCrud.InputParameters();
                inputData.P_ID_DOK = 1;
                inputData.P_ID_DOKSpecified = true;
                inputData.P_ID_TYPE = ID_TYPE;
                inputData.P_ID_VALUE = ID_VALUE;
                if(id_jnsDok != null)
                {
                  inputData.P_KD_DOK = id_jnsDok;
                }
                inputData.P_ID_VALUESpecified = true;
                inputData.P_ISI_FILE = konfigApp.FileToByteArray(FilePath);
                inputData.P_TABLE_TYPE = TABLE_TYPE;
                inputData.P_SELECT = SELECT;

                svcDokCrud = new SvcAsetDokCrud.call_pttClient();
                svcDokCrud.Beginexecute(inputData, new AsyncCallback(getCrudDokASet), "");
            }
            catch (Exception E)
            {

                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalSimpan);
            }
        }
        private void getCrudDokASet(IAsyncResult result)
        {
            try
            {
                dataoutDokAsetCrud = svcDokCrud.Endexecute(result);
                svcDokCrud.Close();
                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                if (dataoutDokAsetCrud.PO_RESULT == "Y")
                {
                    this.Invoke(new CrudDokAset(this.crudDokAset), dataoutDokAsetCrud);
                }
                else
                {
                    MessageBox.Show(dataoutDokAsetCrud.PO_RESULT_MESSAGE, konfigApp.judulGagalLain);
                }
            }
            catch (Exception e)
            {
                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalLain);
            }
        }

        private delegate void CrudDokAset(SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud);

        private void crudDokAset(SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud)
        {
            if (dataoutDokAsetCrud.PO_RESULT == "Y")
            {
                MessageBox.Show(dataoutDokAsetCrud.PO_RESULT_MESSAGE);
                if (this.modeCrud != 'D')
                {
                    this.dataInisial = false;
                    this.getById = true;
                    this.getinitDokBangunan(" ID_M_KBDG_KONS_BDG = " + this.ID_M_KBDG_KONS_BDG.ToString());
                }

            }

        }

    }
}
