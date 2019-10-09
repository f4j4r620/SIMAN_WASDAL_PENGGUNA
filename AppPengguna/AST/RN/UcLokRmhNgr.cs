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
    class UcLokRmhNgr : UserControlDetail
    {
        private SvcLokRmhNgrSelect.call_pttClient fetchData;
        private SvcLokRmhNgrSelect.InputParameters parInp;
        private SvcLokRmhNgrSelect.OutputParameters outDat;
        private SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI selectedData;

        SvcLokRmhNgrCrud.call_pttClient svcLokasiBangunanCrud = null;
        SvcLokRmhNgrCrud.OutputParameters doutCrud = null;


        SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI newRow;
        private ColumnView View;
        private FrmLokRmhNgr PU;

        private decimal? ID_KRMH_NEG;
        private decimal? ID_KRMH_LOKASI;
        private decimal? NUM;
        private string StatusCrud = "";
        public string nama_aset;
        private string Status;
        //------------- GET DOKUMEN -------------------------------

        SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
        SvcAsetGetDokSelect.OutputParameters outFileDok;
        //-------------------------------------------------------

        private void initDokBangunan()
        {


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("Batas Utara", "LOKBATAS_U", "LOKBATAS_U", 1, true);
            this.setKolom("Batas Barat", "LOKBATAS_B", "LOKBATAS_B", 2, true);
            this.setKolom("Batas Selatan", "LOKBATAS_S", "LOKBATAS_S", 3, true);
            this.setKolom("Batas Timur", "LOKBATAS_T", "LOKBATAS_T", 4, true);
            this.setKolom("Bentuk", "LOKBENTUK", "LOKBENTUK", 5, true);
            this.setKolom("Peruntukan Tanah", "LOKPERUNTUKAN", "LOKPERUNTUKAN", 6, true);
            this.setKolom("Topografi Kontur", "LOKKONTUR", "LOKKONTUR", 7, true);
            this.setKolom("Topografi Elevasi", "LOKELEVASI", "LOKELEVASI", 8, true);
            this.setKolom("Aksesbilitas", "LOK_AKSES", "LOK_AKSES", 9, true);
            this.setKolom("GPS", "GPS", "GPS", 10, true);
            this.setKolom("File Denah", "NMFILE", "NMFILE", 11, true);

            this.btnMap.Visibility = BarItemVisibility.Always;

            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMap_ItemClick);
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI);
            this.gcUcDtl.DataSource = binder;

            this.btnView.Caption = "Lihat Denah";
            this.btnView.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

            this.btnView.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);
            this.show_record = true;
        }

        #region View Dokumen
        protected void view_dokumen(object sender, ItemClickEventArgs e)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();
                SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
                parInp.P_ID = selectedData.ID_KRMH_LOKASI;
                parInp.P_ID_TABLE = "ID_KRMH_LOKASI";
                parInp.P_IDSpecified = true;
                parInp.P_TABLE = "M_KRMH_LOKASI";
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
                System.IO.File.WriteAllBytes("dok.pdf", dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display("dok.pdf");
                PuPdf.ShowDialog();
            }
        }


        #endregion//ViewDokumen



        //bila di pasang pada detail Master
        public UcLokRmhNgr(decimal? id_kbdg = null, string status = "edit")
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KRMH_NEG = id_kbdg;
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


        protected override void ucDetail_Load(object sender, EventArgs e)
        {

            this.gvUcDtl.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvUcDtl_InitNewRow);

            
            this.bbTambah.Caption = konfigApp.labelTambah;
            this.initGrid();
            this.getinitDokBangunan();
            this.btnMap.Enabled = false;
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI)selectedView.GetRow(e.RowHandle);


        }

        protected void tampilMap(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            string latitude;//= "-1.406109";
            string longitude;//= "115.473631";
            string deskripsi;
            try
            {
                string[] gps = selectedData.GPS.Split(',');
                latitude =gps[0];
                longitude = gps[1];
                deskripsi = this.nama_aset;
                AppPengguna.PU.Map map = new AppPengguna.PU.Map(latitude, longitude, deskripsi);
                map.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Alamat lokasi GPS salah", konfigApp.judulGagalAmbil);
            }

        }
        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bbTambah.Caption == konfigApp.labelTambah)
            {

                PU = new FrmLokRmhNgr("input", this.ID_KRMH_NEG, null);
                this.StatusCrud = "input";
                PU.simpanLokasiRumahNegara = new SimpanLokasiRumahNegara(this.simpanLokasiRumahNegara);
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
                    PU = new FrmLokRmhNgr("detail", selectedData);
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new FrmLokRmhNgr("edit", selectedData);
                    this.StatusCrud = "edit";
                }

                PU.simpanLokasiRumahNegara = new SimpanLokasiRumahNegara(this.simpanLokasiRumahNegara);

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
                case "ID Lokasi Bangunan":
                    where = "ID_KRMH_LOKASI " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "Batas Utara":
                    where = "Upper(LOKBATAS_U) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Selatan":
                    where = "Upper(LOKBATAS_S) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Timur":
                    where = "Upper(LOKBATAS_T) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Batas Barat":
                    where = "Upper(LOKBATAS_B) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Bentuk":
                    where = "Upper(LOKBENTUK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Peruntukan Tanah":
                    where = "Upper(LOKPERUNTUKAN) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Topografi-Kontur":
                    where = "Upper(LOKKONTUR) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Topografi-Elevasi":
                    where = "Upper(LOKELEVASI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Aksesbilitas":
                    where = "Upper(LOK_AKSES) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "GPS":
                    where = "Upper(GPS) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "File Denah":
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
                    SvcLokRmhNgrCrud.InputParameters parInp = new SvcLokRmhNgrCrud.InputParameters();
                    parInp.P_ID_KRMH_NEG = selectedData.ID_KRMH_NEG;
                    parInp.P_ID_KRMH_NEGSpecified = true;
                    parInp.P_ID_KRMH_LOKASI = selectedData.ID_KRMH_LOKASI;
                    parInp.P_ID_KRMH_LOKASISpecified = true;
                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcLokasiBangunanCrud = new SvcLokRmhNgrCrud.call_pttClient();
                    svcLokasiBangunanCrud.Open();
                    svcLokasiBangunanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
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
                selectedData = (SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI)selectedView.GetRow(e.FocusedRowHandle);
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

                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }

        protected void getinitDokBangunan(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcLokRmhNgrSelect.InputParameters parInp = new SvcLokRmhNgrSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KRMH_NEG = {0} {1}", this.ID_KRMH_NEG, _where);
            fetchData = new SvcLokRmhNgrSelect.call_pttClient();
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

        private delegate void ShowData(SvcLokRmhNgrSelect.OutputParameters dataOut);

        public void showData(SvcLokRmhNgrSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KRMH_NEG_LOKASI.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KRMH_NEG_LOKASI[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KRMH_NEG_LOKASI[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRMH_NEG_LOKASI[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRMH_NEG_LOKASI[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KRMH_NEG_LOKASI[i]);
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

        private void simpanLokasiRumahNegara(SvcLokRmhNgrCrud.InputParameters parInp)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();

                this.nonAktifForm("");
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                svcLokasiBangunanCrud = new SvcLokRmhNgrCrud.call_pttClient();
                svcLokasiBangunanCrud.Open();
                svcLokasiBangunanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
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
                doutCrud = svcLokasiBangunanCrud.Endexecute(result);
                svcLokasiBangunanCrud.Close();
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                bool berhasil = this.msg_get_database(doutCrud.PO_RESULT, "Y", 0);
                if (berhasil == true)
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

        private delegate void ShowDataCrud(SvcLokRmhNgrCrud.OutputParameters dataOut);

        private delegate void TutupPopUp(string data);

        public void tutupPopUp(string data)
        {
            this.PU.Close();
        }
        public void showDataCrud(SvcLokRmhNgrCrud.OutputParameters dataOut)
        {
            SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI dataPenyama = new SvcLokRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_LOKASI();
            dataPenyama.ID_KRMH_LOKASI = dataOut.PO_ID_KRMH_LOKASI;
            dataPenyama.ID_KRMH_NEG = this.ID_KRMH_NEG;
            this.ID_KRMH_LOKASI = (this.StatusCrud == "edit")? selectedData.ID_KRMH_LOKASI : dataOut.PO_ID_KRMH_LOKASI;
            switch (this.modeCrud)
            {
                case 'C':
                    if (this.PU.FilePath != null)
                    {
                        string Path = this.PU.FilePath;
                        simpanFile("ID_KRMH_LOKASI", dataPenyama.ID_KRMH_LOKASI, "M_KRMH_LOKASI", Path, "C");
                    }
                    else
                    {
                        this.dataInisial = false;
                        this.getById = true;
                        this.getinitDokBangunan(" ID_KRMH_LOKASI = " + dataOut.PO_ID_KRMH_LOKASI.ToString());

                    }
                    
                    break;
                case 'U':
                     this.binder.Remove(this.selectedData);
                    if (this.PU.FilePath != null)
                    {
                        string Path = this.PU.FilePath;
                        if (this.selectedData.FILE_EXISTS != 0)
                        {
                            simpanFile("ID_KRMH_LOKASI", dataPenyama.ID_KRMH_LOKASI, "M_KRMH_LOKASI", Path, "U");
                        }
                        else
                        {
                           
                            simpanFile("ID_KRMH_LOKASI", dataPenyama.ID_KRMH_LOKASI, "M_KRMH_LOKASI", Path, "C");
                       }
                    }
                    else
                    {
                        this.dataInisial = false;
                        this.getById = true;
                        this.getinitDokBangunan(" ID_KRMH_LOKASI = " + dataOut.PO_ID_KRMH_LOKASI.ToString());
                    
                    }
                    
                    break;
                case 'D':
                    this.binder.Remove(selectedData);
                    
                    this.StrTotalGrid.Caption = (Convert.ToInt64(this.StrTotalGrid.Caption) - 1).ToString();
                    this.StrTotalDb.Caption = (Convert.ToInt64(this.StrTotalDb.Caption) - 1).ToString();
                    this.gvUcDtl.RefreshData();
                    break;
            }

        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string FilePath, string SELECT)
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
                    //this.getinitDokBangunan(" ID_KRMH_LOKASI = " + this.ID_KRMH_LOKASI.ToString());
                }
                this.search = "";
                this.initGrid();
                this.getinitDokBangunan();

            }

        }

    }
}
