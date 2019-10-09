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
    class ucFasBangunan : UserControlDetail
    {
        private SvcFasBangunanSelect.call_pttClient fetchData;
        private SvcFasBangunanSelect.InputParameters parInp;
        private SvcFasBangunanSelect.OutputParameters outDat;
        private SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG selectedData;

        SvcFasBangunanCrud.call_pttClient svcFasBangunanCrud = null;
        SvcFasBangunanCrud.OutputParameters doutFasBangunanCrud = null;

        //public NonAktifkanFormSatker nonAktifForm;
        //public AktifkanFormSatker aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG newRow;
        private ColumnView View;
        private PuFasBangunan PU;
        private decimal? ID_KBDG;
        private string StatusCrud = "";
        private decimal? NUM;

        private string Status;
        //child form customization 
        private void initFasBangunan()
        {
            

            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
           
            
            this.RepoComboBox.Items.AddRange(new object[] {
                                                                      "Ada"
                                                                      ,"Tidak Ada"
                                                            });          
            
            this.setKolom("Nomor", "NUM", "NUM", 0);
          
            this.setKolom("Listrik", "LISTRIK", "LISTRIK", 3,true);
            this.setKolom("Air Bersih", "PAM", "PAM", 4, true);
            this.setKolom("Telpon", "TELPON", "TELPON", 5, true);
            this.setKolom("Gas", "GAS", "GAS", 6, true);
            this.setKolom("Saluran Limbah", "SAL_LIMBAH", "SAL_LIMBAH", 7, true);
            this.setKolom("Fasilitas Lainnya", "LAINNYA", "LAINNYA", 8, true);
            int width = this.setKolom("Keterangan ", "KET", "KET", 9, true);
            this.SetGridSize(width, 0);
            this.gridDoubleClickDetail = true;
            this.show_record = true;
        }


  
        //bila di pasang pada detail Master
        public ucFasBangunan(decimal? id_kbdg = null, string status="edit")
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KBDG = id_kbdg;
            this.Status = status;
            initFasBangunan();
        }


        protected override void ucDetail_Load(object sender, EventArgs e)
        {

            this.gvUcDtl.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvUcDtl_InitNewRow);

            if (this.Status == "detail")
            {
                this.bbEdit.Enabled = false;
                this.bbHapus.Enabled = false;
                this.bbTambah.Enabled = false;
            }
            else
            {
                this.bbEdit.Enabled = true;
                this.bbHapus.Enabled = true;
                this.bbTambah.Enabled = true;
            }
            this.bbTambah.Caption = konfigApp.labelTambah;
            this.initGrid();
            this.getinitFasBangunan();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;

        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG)selectedView.GetRow(e.RowHandle);
            SvcFasBangunanCrud.InputParameters parInp = new SvcFasBangunanCrud.InputParameters();
           
            parInp.P_LISTRIK = newRow.LISTRIK;
            parInp.P_PAM = newRow.PAM;
            parInp.P_TELPON = newRow.TELPON;
            parInp.P_GAS = newRow.GAS;
            parInp.P_SAL_LIMBAH = newRow.SAL_LIMBAH;
            parInp.P_LAINNYA = newRow.LAINNYA;
            parInp.P_KET = newRow.KET;
            MessageBox.Show("", "");

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bbTambah.Caption == konfigApp.labelTambah)
            {
                this.StatusCrud = "input";
                PU = new PuFasBangunan("input", this.ID_KBDG);
                PU.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                PU.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                PU.simpanFasBangunan = new SimpanFasBangunan(this.simpanFasBangunan);
                PU.ShowDialog();
                /*
                this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                this.gvUcDtl.OptionsBehavior.Editable = true;
                this.bbTambah.Glyph = AppPengguna.Properties.Resources.tombol_simpan;
                this.bbTambah.Caption = konfigApp.labelSimpan;
                 */
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
                    PU = new PuFasBangunan("detail", this.selectedData);
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new PuFasBangunan("edit", this.selectedData);
                    this.StatusCrud = "edit";
                }
                PU.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);
                PU.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                PU.simpanFasBangunan = new SimpanFasBangunan(this.simpanFasBangunan);
                PU.ShowDialog();
            }
        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getinitFasBangunan();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initialData = false;
            this.getinitFasBangunan();
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
                this.dataInisial = true;
                this.getinitFasBangunan(get_where_clause(nama_kolom, opr, parameter, parameter_2));
                
            }
            catch (Exception)
            {

                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
                this.aktifkanForm("");
            }

        }



        private string get_where_clause(string nama_kolom, string opr, string parameter, string parameter2)
        {
            string where = "";

            switch (nama_kolom)
            {
                case "ID Fasilitas Bangunan":
                    where = "ID_KBDG_FAS_PENUNJANG " + get_operator("Number",opr, parameter, parameter2);
                    break;
                case "Nama Fasilitas":

                    where = "Upper(NM_FASILITAS) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Listrik":
                    where = "Upper(LISTRIK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Air Bersih":
                    where = "Upper(PAM) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Telpon":
                    where = "Upper(TELPON) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Gas":
                    where = "Upper(Gas) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Saluran Limbah":
                    where = "Upper(SAL_LIMBAH) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Fasilitas Lainnya":
                    where = "Upper(LAINNYA) " + get_operator("String", opr, parameter, parameter2);
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
                    SvcFasBangunanCrud.InputParameters parInp = new SvcFasBangunanCrud.InputParameters();
                    parInp.P_ID_KBDG = selectedData.ID_KBDG;
                    parInp.P_ID_KBDGSpecified = true;
                    parInp.P_ID_KBDG_FAS_PENUNJANG = selectedData.ID_KBDG_FAS_PENUNJANG;
                    parInp.P_ID_KBDG_FAS_PENUNJANGSpecified = true;

                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcFasBangunanCrud = new SvcFasBangunanCrud.call_pttClient(konfigApp.SvcFasBangunanCrud_name, konfigApp.SvcFasBangunanCrud_address);
                    svcFasBangunanCrud.Open();
                    svcFasBangunanCrud.Beginexecute(parInp, new AsyncCallback(crudFasBangunan), "");
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
                selectedData = (SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG)selectedView.GetRow(e.FocusedRowHandle);
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

        protected void getinitFasBangunan(string _where = null)
        {
            decimal Max, Min;
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcFasBangunanSelect.InputParameters parInp = new SvcFasBangunanSelect.InputParameters();
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
            fetchData = new SvcFasBangunanSelect.call_pttClient(konfigApp.SvcFasBangunanSelect_name,konfigApp.SvcFasBangunanSelect_address);
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
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch
            {
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcFasBangunanSelect.OutputParameters dataOut);
        
        public void showData(SvcFasBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBDG_FAS_PENUNJANG.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KBDG_FAS_PENUNJANG[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KBDG_FAS_PENUNJANG[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_FAS_PENUNJANG[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_FAS_PENUNJANG[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KBDG_FAS_PENUNJANG[i]);
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

        private void simpanFasBangunan(SvcFasBangunanCrud.InputParameters parInp)
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            
            this.modeCrud = Convert.ToChar(parInp.P_SELECT);
            svcFasBangunanCrud = new SvcFasBangunanCrud.call_pttClient(konfigApp.SvcFasBangunanCrud_name, konfigApp.SvcFasBangunanCrud_address);
            svcFasBangunanCrud.Open();
            svcFasBangunanCrud.Beginexecute(parInp, new AsyncCallback(crudFasBangunan), "");
	                
        }
        private void crudFasBangunan(IAsyncResult result)
        {
            try
            {
                doutFasBangunanCrud = svcFasBangunanCrud.Endexecute(result);
                svcFasBangunanCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                MessageBox.Show(doutFasBangunanCrud.PO_RESULT_MESSAGE, konfigApp.judulKonfirmasi);
                //bool berhasil = this.msg_get_database(doutFasBangunanCrud.PO_RESULT, "Y", 0);
                if (String.Compare(doutFasBangunanCrud.PO_RESULT, "Y", true) == 0 )
                {
                    this.Invoke(new TutupPopUp(this.tutupPopUp), "");
                    this.Invoke(new ShowDataCrud(this.showDataCrud), doutFasBangunanCrud);
                }
             
            }
            catch
            {
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
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

        private delegate void TutupPopUp(string data);
        public void tutupPopUp(string data)
        {
            this.PU.Close();
           
            
        }
        private delegate void ShowDataCrud(SvcFasBangunanCrud.OutputParameters dataOut);

        public void showDataCrud(SvcFasBangunanCrud.OutputParameters dataOut)
        {
            SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG dataPenyama = new SvcFasBangunanSelect.BPSIMANSROW_M_KBDG_FAS_PENUNJANG();
            dataPenyama.ID_KBDG_FAS_PENUNJANG = dataOut.PO_ID_KBDG_FAS_PENUNJANG;

            dataPenyama.NUM = 99;
            dataPenyama.NUMSpecified = true;
            switch (this.modeCrud)
            {
                case 'C':
                  
                     this.dataInisial = false;
                    this.getById = true;
                    this.getinitFasBangunan(" ID_KBDG_FAS_PENUNJANG = " + dataOut.PO_ID_KBDG_FAS_PENUNJANG.ToString());
                    break;
                case 'U':
                    this.binder.Remove(selectedData);
                     this.dataInisial = false;
                    this.getById = true;
                    this.getinitFasBangunan(" ID_KBDG_FAS_PENUNJANG = " + dataOut.PO_ID_KBDG_FAS_PENUNJANG.ToString());
                  
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
