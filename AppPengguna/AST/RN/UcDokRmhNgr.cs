using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.RN
{
    class UcDokRmhNgr : UserControlDetail
    {
        private SvcDokRmhNgrSelect.call_pttClient fetchData;
        private SvcDokRmhNgrSelect.InputParameters parInp;
        private SvcDokRmhNgrSelect.OutputParameters outDat;
        private SvcDokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK selectedData;

        SvcDokRmhNgrCrud.call_pttClient svcDokRmhNgrCrud = null;
        SvcDokRmhNgrCrud.OutputParameters doutCrud = null;


        SvcDokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK newRow;
        private ColumnView View;
        private PuDokRumahNegara PU;
        private decimal? ID_KRMH_NEG;
        private decimal? ID_KRMH_DOK;
        public decimal? NUM;
        private string StatusCrud = "";

        private string Status;
        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------
        //child form customization 
        private void initDokBangunan()
        {


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcDokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            this.setKolom("Nomor", "NUM", "NUM", 0);
            this.setKolom("Jenis Dokumen", "KD_JNS_DOK_BDG", "KD_JNS_DOK_BDG", 3, true, 210);
            this.setKolom("Nomor", "NO_DOK", "NO_DOK", 4, true);
            this.setKolom("Tanggal Dokumen", "TGL_DOK", "TGL_DOK", 5, true, 110, "date");
            this.setKolom("Berlaku Sampai", "TGL_BERLAKU", "TGL_BERLAKU", 6, true, 110, "date");
            this.setKolom("Instansi Penerbit", "PENERBIT", "PENERBIT", 7, true);
            int width = this.setKolom("File", "NMFILE", "NMFILE", 8);
            this.SetGridSize(width, 0);

            this.btnMap.Caption = "Lihat Dokumen";
            this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnMap.Name = "btnMap";
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);
            this.show_record = true;
            this.gridDoubleClickDetail = true;
            this.gvUcDtl.BestFitColumns();
        }

        #region View Dokumen
        protected void view_dokumen(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = selectedData.ID_KRMH_DOK;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_KRMH_DOK";
                parInp.P_TABLE = "M_KRMH_NEG_DOK";
                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient();
                svcAsetGetDokSelect.Open();
                svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
            }
            catch (Exception E)
            {
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
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
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new ShowFileDok(this.showFileDok), this.outFileDok);
            }
            catch (Exception E)
            {
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
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
                System.IO.File.WriteAllBytes(selectedData.NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(selectedData.NMFILE);
                PuPdf.ShowDialog();
            }
        }


        #endregion//ViewDokumen




        //bila di pasang pada detail Master
        public UcDokRmhNgr(decimal? id_krmh_neg = null, string status = "edit")
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KRMH_NEG = id_krmh_neg;
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
            this.btnMap.Enabled = false;
        }


        protected override void ucDetail_Load(object sender, EventArgs e)
        {


            this.bbTambah.Caption = konfigApp.labelTambah;
            this.initGrid();
            this.getinitDokRumahNegara();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;

        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcDokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK)selectedView.GetRow(e.RowHandle);


        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bbTambah.Caption == konfigApp.labelTambah)
            {

                PU = new PuDokRumahNegara("input", this.ID_KRMH_NEG, null);
                this.StatusCrud = "input";
                PU.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                PU.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                PU.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                PU.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                PU.simpanDokRumahNegara = new SimpanDokRumahNegara(this.simpanDokRumahNegara);

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
                    PU = new PuDokRumahNegara("detail", selectedData);
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new PuDokRumahNegara("edit", selectedData);
                    this.StatusCrud = "edit";
                }
                PU.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                PU.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                PU.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                PU.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                PU.simpanDokRumahNegara = new SimpanDokRumahNegara(this.simpanDokRumahNegara);

                PU.ShowDialog();
            }

        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getinitDokRumahNegara();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getinitDokRumahNegara();
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
                this.getinitDokRumahNegara(get_where_clause(nama_kolom, opr, parameter, parameter_2)); ;


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
                case "ID Dokumen Bangunan":
                    where = "ID_KRMH_DOK " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Jenis Dokumen":
                    where = "Upper(KD_JNS_DOK_BDG) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nomor":
                    where = "Upper(NO_DOK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tanggal Dokumen":
                    where = "Upper(TGL_DOK) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Berlaku Sampai":
                    where = "Upper(TGL_BERLAKU) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Instansi Penerbit":
                    where = "Upper(PENERBIT) " + get_operator("String", opr, parameter, parameter2);
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
                    SvcDokRmhNgrCrud.InputParameters parInp = new SvcDokRmhNgrCrud.InputParameters();
                    parInp.P_ID_KRMH_NEG = selectedData.ID_KRMH_NEG;
                    parInp.P_ID_KRMH_DOKSpecified = true;
                    parInp.P_ID_KRMH_NEGSpecified = true;
                    parInp.P_ID_KRMH_DOK = selectedData.ID_KRMH_DOK;
                    parInp.P_NO_DOK = selectedData.NO_DOK;
                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcDokRmhNgrCrud = new SvcDokRmhNgrCrud.call_pttClient();
                    svcDokRmhNgrCrud.Open();
                    svcDokRmhNgrCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new closeProgresBar(this.CloseProgresBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                }
            }
        }


        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcDokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK)selectedView.GetRow(e.FocusedRowHandle);
                if (selectedView.IsLastRow)
                {
                    LastRow = true;
                }
                else
                {
                    LastRow = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
                if (selectedData.FILE_EXISTS == 0)
                {
                    this.btnMap.Enabled = false;
                }
                else
                {
                    this.btnMap.Enabled = true;
                }
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
                case "Tanggal Dokumen":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                case "Berlaku Sampai":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }

        protected void getinitDokRumahNegara(string _where = null)
        {

            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcDokRmhNgrSelect.InputParameters parInp = new SvcDokRmhNgrSelect.InputParameters();
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
            fetchData = new SvcDokRmhNgrSelect.call_pttClient();
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

        private delegate void ShowData(SvcDokRmhNgrSelect.OutputParameters dataOut);

        public void showData(SvcDokRmhNgrSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KRMH_NEG_DOK.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[i].TGL_BERLAKU).Substring(0, 10);
              string date_ = Convert.ToString(serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[i].TGL_BERLAKU).Substring(0, 8);
              string date2 = Convert.ToString(serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[i].TGL_DOK).Substring(0, 10);
              string date2_ = Convert.ToString(serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[i].TGL_DOK).Substring(0, 8);

              if (date == "11/11/1000" || date_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[i].TGL_BERLAKU = null;
              }
              if (date2 == "11/11/1000" || date2_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[i].TGL_DOK = null;
              }
                binder.Add(serviceOutPut.SF_ROW_M_KRMH_NEG_DOK[i]);
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

        private void simpanDokRumahNegara(SvcDokRmhNgrCrud.InputParameters parInp)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();

                this.nonAktifForm("");
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                svcDokRmhNgrCrud = new SvcDokRmhNgrCrud.call_pttClient();
                svcDokRmhNgrCrud.Open();
                svcDokRmhNgrCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
            }
            catch (Exception E)
            {

                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(konfigApp.teksGagalSimpan,konfigApp.judulGagalSimpan);
                MessageBox.Show("Service tidak dapat diakses.", konfigApp.judulGagalSimpan);
            }


        }
        private void prosesCrud(IAsyncResult result)
        {
            try
            {
                doutCrud = svcDokRmhNgrCrud.Endexecute(result);
                svcDokRmhNgrCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                MessageBox.Show(doutCrud.PO_RESULT_MESSAGE.ToString(), "Status");
                // bool berhasil = this.msg_get_database(doutCrud.PO_RESULT, "Y", 0);
                if (String.Compare(doutCrud.PO_RESULT, "Y", true) == 0)
                {
                    this.Invoke(new ShowDataCrud(this.showDataCrud), doutCrud);
                    if (this.StatusCrud != "hapus")
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

        private delegate void ShowDataCrud(SvcDokRmhNgrCrud.OutputParameters dataOut);

        private delegate void TutupPopUp(string data);

        public void tutupPopUp(string data)
        {
            this.PU.Close();
        }
        public void showDataCrud(SvcDokRmhNgrCrud.OutputParameters dataOut)
        {
            SvcDokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK dataPenyama = new SvcDokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DOK();
            dataPenyama.ID_KRMH_NEG = this.ID_KRMH_NEG;
            dataPenyama.ID_KRMH_DOK = dataOut.PO_ID_KRMH_DOK;
            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            this.ID_KRMH_DOK = (this.StatusCrud == "edit")? selectedData.ID_KRMH_DOK : dataOut.PO_ID_KRMH_DOK;
            if (this.selectedData != null)
            {
                this.NUM = selectedData.NUM;
            }
            switch (this.modeCrud)
            {
                case 'C':
                    if (this.PU.FilePath != null)
                    {
                        string Path = this.PU.FilePath;
                        simpanFile("ID_KRMH_DOK", dataPenyama.ID_KRMH_DOK, "M_KRMH_NEG_DOK", Path, "C", PU.ID_JNSDOK);
                    }
                    else
                    {
                        this.dataInisial = false;
                        this.getById = true;
                        this.getinitDokRumahNegara(" ID_KRMH_DOK = " + this.ID_KRMH_DOK.ToString());

                    }
                    break;
                case 'U':

                    this.binder.Remove(this.selectedData);
                    if (this.PU.FilePath != null)
                    {
                        string Path = this.PU.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                          simpanFile("ID_KRMH_DOK", this.ID_KRMH_DOK, "M_KRMH_NEG_DOK", Path, "U", PU.ID_JNSDOK);
                        }
                        else
                        {
                          simpanFile("ID_KRMH_DOK", dataPenyama.ID_KRMH_DOK, "M_KRMH_NEG_DOK", Path, "C", PU.ID_JNSDOK);
                        }
                    }
                    else
                    {
                        this.dataInisial = false;
                        this.getById = true;
                        //this.getinitDokRumahNegara(" ID_KRMH_DOK = " + this.ID_KRMH_DOK.ToString());
                        this.search = "";
                        this.initGrid();
                        this.getinitDokRumahNegara();

                    }

                    break;
                case 'D':
                    this.binder.Remove(selectedData);
                    gvUcDtl.RefreshData();
                    this.search = "";
                    this.initGrid();
                    this.getinitDokRumahNegara();
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
                inputData.P_ID_VALUESpecified = true;
                if (id_jnsDok != null)
                {
                inputData.P_KD_DOK = id_jnsDok;
                }
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
                if (this.modeCrud == 'D')
                {
                    this.dataInisial = false;
                    this.getById = true;
                    //this.getinitDokRumahNegara(" ID_KRMH_DOK = " + this.ID_KRMH_DOK.ToString());
                }
                this.search = "";
                this.initGrid();
                this.getinitDokRumahNegara();
            }

        }

    }
}
