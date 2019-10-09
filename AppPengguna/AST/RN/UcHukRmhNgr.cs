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
    public class UcHukRmhNgr : UserControlDetail
    {
        private SvcMslHukRmhNgrSelect.call_pttClient fetchData;
        private SvcMslHukRmhNgrSelect.InputParameters parInp;
        private SvcMslHukRmhNgrSelect.OutputParameters outDat;
        private SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM selectedData;
        
        SvcHukRmhNgrCrud.call_pttClient svcPhukumBangunanCrud = null;
        SvcHukRmhNgrCrud.OutputParameters doutCrud = null;
        SvcHukRmhNgrCrud.InputParameters parInpCrud;
        //public NonAktifkanFormSatker nonAktifForm;
        //public AktifkanFormSatker aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM newRow;
        private ColumnView View;
        private PuHukumRumahNegara PU;
        private decimal? ID_KRMH_NEG;
        private decimal? NUM;
        private string StatusCrud = "";
        private decimal? ID_KRMH_HUKUM;
        private Thread myThread_ = null;

        private string Status;
        //child form customization 
        private void initPhukumBangunan()
        {


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            this.setKolom("Nomor", "NUM", "NUM", 0, false, 60);
            this.setKolom("Tanggal", "TGL", "TGL", 1, true, 110, "date");
            this.setKolom("Jenis Status Hukum", "JNS_STATUS_HUKUM", "JNS_STATUS_HUKUM", 2, true);
            this.setKolom("Status Hukum", "STATUS_HUKUM", "STATUS_HUKUM", 3, true);
            this.setKolom("Pihak yang Bersengketa", "PHK_SENGKETA", "PHK_SENGKETA", 4, true, 170);
            this.setKolom("Uraian Masalah", "UR_MASALAH", "UR_MASALAH", 5, true, 220);
            this.setKolom("File", "NMFILE", "NMFILE", 6, true);
          
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
                parInp.P_ID = selectedData.ID_KRMH_HUKUM;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_KRMH_HUKUM";
                parInp.P_TABLE = "M_KRMH_NEG_HUKUM";

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
                System.IO.File.WriteAllBytes(selectedData.NMFILE, dok.ISI_FILE);
                AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
                PuPdf.display(selectedData.NMFILE);
                PuPdf.ShowDialog();
            }
        }


        #endregion//ViewDokumen




        //bila di pasang pada detail Master
        public UcHukRmhNgr(decimal? id_kbdg = null, string status = "edit")
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KRMH_NEG = id_kbdg;
            this.Status = status;

            initPhukumBangunan();
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
            this.getinitPhukumBangunan();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;

        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM)selectedView.GetRow(e.RowHandle);


        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bbTambah.Caption == konfigApp.labelTambah)
            {

                PU = new PuHukumRumahNegara("input", this.ID_KRMH_NEG, null);

                this.StatusCrud = "input";
                PU.simpanPhukumBangunan = new SimpanPhukumBangunan(this.simpanPhukumBangunan);
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
                    PU = new PuHukumRumahNegara("detail", selectedData);
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new PuHukumRumahNegara("edit", selectedData);
                    this.StatusCrud = "edit";
                }
                PU.simpanPhukumBangunan = new SimpanPhukumBangunan(this.simpanPhukumBangunan);

                PU.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getinitPhukumBangunan();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dataInisial = false;
            this.getinitPhukumBangunan();
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
                this.getinitPhukumBangunan(get_where_clause(nama_kolom, opr, parameter, parameter_2));


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

                case "Tanggal":
                    where = "Upper(TGL) " + get_operator("Date", opr, parameter, parameter2);
                    break;

                case "Jenis Status Hukum":
                    where = "Upper(JNS_STATUS_HKM) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Pihak yang Bersengketa":
                    where = "Upper(PHK_SENGKETA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Status Hukum":
                    where = "Upper(STATUS_HUKUM) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Uraian Masalah":
                    where = "Upper(UR_MASALAH) " + get_operator("String", opr, parameter, parameter2);
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
                    this.Status = "hapus";
                    this.nonAktifForm("");
                    myThread_ = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread_.Start();
                    SvcHukRmhNgrCrud.InputParameters parInp = new SvcHukRmhNgrCrud.InputParameters();
                    parInp.P_ID_KRMH_NEG = selectedData.ID_KRMH_NEG;
                    parInp.P_ID_KRMH_NEGSpecified = true;
                    parInp.P_ID_KRMH_HUKUM = selectedData.ID_KRMH_HUKUM;
                    parInp.P_ID_KRMH_HUKUMSpecified = true;

                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcPhukumBangunanCrud = new SvcHukRmhNgrCrud.call_pttClient();
                    svcPhukumBangunanCrud.Open();
                    svcPhukumBangunanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
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
                selectedData = (SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM)selectedView.GetRow(e.FocusedRowHandle);
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

                case "Tanggal":
                    this.teSearch.Edit = this.ItemDate;
                    this.teSearch2.Edit = this.ItemDate;
                    break;
                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }

        protected void getinitPhukumBangunan(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcMslHukRmhNgrSelect.InputParameters parInp = new SvcMslHukRmhNgrSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format("ID_KRMH_NEG = {0} {1}", this.ID_KRMH_NEG, _where);
            fetchData = new SvcMslHukRmhNgrSelect.call_pttClient();
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

        private delegate void ShowData(SvcMslHukRmhNgrSelect.OutputParameters dataOut);

        public void showData(SvcMslHukRmhNgrSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
              string date = Convert.ToString(serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM[i].TGL).Substring(0, 10);
              string date_ = Convert.ToString(serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM[i].TGL).Substring(0, 8);

              if (date == "11/11/1000" || date_ == "1/1/0001")
              {
                serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM[i].TGL = null;
              }
                binder.Add(serviceOutPut.SF_ROW_M_KRMH_NEG_HUKUM[i]);
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

        private void simpanPhukumBangunan(string text)
        {
            try
            {

                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();

                this.nonAktifForm("");
                parInpCrud = new SvcHukRmhNgrCrud.InputParameters();

                parInpCrud.P_ID_KRMH_NEG = this.PU.ID_KRMH_NEG;
                parInpCrud.P_ID_KRMH_HUKUMSpecified = true;
                parInpCrud.P_ID_KRMH_HUKUM = (this.PU.ID_KRMH_HUKUM == null) ? 0 : this.PU.ID_KRMH_HUKUM;
                parInpCrud.P_ID_KRMH_NEGSpecified = true;

                //parInp.P_JNS_STATUS_HKM = this.teJNS_STATUS_HKM.Text;
                parInpCrud.P_PHK_SENGKETA = this.PU.tePHK_SENGKETA.Text;
                parInpCrud.P_KD_STATUS_HUKUM = this.PU.KD_STATUS_HUKUM;
                parInpCrud.P_TGL = this.PU.teTGL.Text;
                parInpCrud.P_UR_MASALAH = this.PU.teUR_MASALAH.Text;
                parInpCrud.P_FILE = this.PU.teFileName.Text;
                parInpCrud.P_TYPE_DOC = "-";
                
                // parInp.teTERAKHIR_YN = this.teTERAKHIR_YN.Text;
                if (this.PU.STATUS == "input")
                {
                  parInpCrud.P_SELECT = "C";
                }
                else
                {
                  parInpCrud.P_SELECT = "U";
                }

                this.modeCrud = Convert.ToChar(parInpCrud.P_SELECT);
                svcPhukumBangunanCrud = new SvcHukRmhNgrCrud.call_pttClient();
                svcPhukumBangunanCrud.Open();
                svcPhukumBangunanCrud.Beginexecute(parInpCrud, new AsyncCallback(prosesCrud), "");
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
                doutCrud = svcPhukumBangunanCrud.Endexecute(result);
                svcPhukumBangunanCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                bool berhasil = this.msg_get_database(doutCrud.PO_RESULT, "Y", 0);
                if (berhasil == true)
                {
                    this.Invoke(new ShowDataCrud(this.showDataCrud), doutCrud);
                    if (this.Status != "hapus")
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

        private delegate void ShowDataCrud(SvcHukRmhNgrCrud.OutputParameters dataOut);

        private delegate void TutupPopUp(string data);

        public void tutupPopUp(string data)
        {
            this.PU.Close();
            this.bbRefresh.PerformClick();
        }
        public void showDataCrud(SvcHukRmhNgrCrud.OutputParameters dataOut)
        {
            SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM dataPenyama = new SvcMslHukRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_HUKUM();
            dataPenyama.ID_KRMH_HUKUM = dataOut.PO_ID_KRMH_HUKUM;

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            this.ID_KRMH_HUKUM = (this.StatusCrud == "edit")? selectedData.ID_KRMH_HUKUM: dataOut.PO_ID_KRMH_HUKUM;
            if (this.selectedData != null)
            {
                this.NUM = this.selectedData.NUM;
            }
            switch (this.modeCrud)
            {
                case 'C':
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.PU.FilePath != null)
                    {
                        string filePath = this.PU.FilePath;
                        simpanFile("ID_KRMH_HUKUM", dataPenyama.ID_KRMH_HUKUM, "M_KRMH_NEG_HUKUM", filePath, "C", PU.ID_JNSDOK);
                    }
                    else
                    {
                        this.PU.Close();
                        this.dataInisial = false;
                        this.getById = true;
                        this.getinitPhukumBangunan(" ID_KRMH_HUKUM = " + dataOut.PO_ID_KRMH_HUKUM.ToString());
                    }
                    break;
                case 'U':
                    this.binder.Remove(this.selectedData);
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.PU.FilePath != null)
                    {

                        string filePath = this.PU.FilePath;
                        string SELECT = "C";
                        if (selectedData.NMFILE != "-")
                        {
                            SELECT = "U";
                        }
                        simpanFile("ID_KRMH_HUKUM", dataPenyama.ID_KRMH_HUKUM, "M_KRMH_NEG_HUKUM", filePath, SELECT, PU.ID_JNSDOK);
                    }
                    else
                    {
                        this.PU.Close();
                        this.dataInisial = false;
                        this.getById = true;
                        this.getinitPhukumBangunan(" ID_KRMH_HUKUM = " + dataOut.PO_ID_KRMH_HUKUM.ToString());
                    }
                    this.dataInisial = true;
                    break;
                case 'D':
                    this.binder.Remove(this.selectedData);
                    this.gvUcDtl.RefreshData();
                    this.search = "";
                    this.initGrid();
                    this.getinitPhukumBangunan();
                    this.StrTotalGrid.Caption = (Convert.ToInt64(this.StrTotalGrid.Caption) - 1).ToString();
                    this.StrTotalDb.Caption = (Convert.ToInt64(this.StrTotalDb.Caption) - 1).ToString();
                    break;
            }

        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string filePath, string SELECT, string id_jnsDok = null)
        {
            
            try
            {
                myThread_ = new Thread(new ThreadStart(this.PU.ShowProgresBar));
                myThread_.Start();

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
                inputData.P_ISI_FILE = konfigApp.FileToByteArray(filePath);
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
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.PU.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.PU.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.PU.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.PU.progBar), BarItemVisibility.Never);
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
                    this.PU.Close();
                    this.dataInisial = false;
                    this.getById = true;
                    //this.getinitPhukumBangunan(" ID_KRMH_HUKUM = " + this.ID_KRMH_HUKUM.ToString());
                }
                this.search = "";
                this.initGrid();
                this.getinitPhukumBangunan();
            }

        }

    }
}
