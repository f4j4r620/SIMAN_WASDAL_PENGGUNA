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
    class ucLokasiBangunan : UserControlDetail
    {
        private SvcLokasiBangunanSelect.call_pttClient fetchData;
        private SvcLokasiBangunanSelect.InputParameters parInp;
        private SvcLokasiBangunanSelect.OutputParameters outDat;
        private SvcLokasiBangunanSelect.BPSIMANSROW_M_KBDG_LOKASI selectedData;

        SvcLokasiBangunanCrud.call_pttClient svcLokasiBangunanCrud = null;
        SvcLokasiBangunanCrud.OutputParameters doutCrud = null;

        
        SvcLokasiBangunanSelect.BPSIMANSROW_M_KBDG_LOKASI newRow;
        private ColumnView View;
        private PuLokasiBangunan PU;
         
        private decimal? ID_KBDG;
        public string nama_aset;
        private decimal? jmlData = 0;
        private string Status;
        public decimal? NUM;
        private string StatusCrud = "";
        private void initDokBangunan()
        {


            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcLokasiBangunanSelect.BPSIMANSROW_M_KBDG_LOKASI);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            this.setKolom("No", "NUM", "NUM", 0,false,90);
            this.setKolom("Batas Utara", "LOKBATAS_U", "LOKBATAS_U", 3, true, 210);
            this.setKolom("Batas Selatan", "LOKBATAS_S", "LOKBATAS_S", 4, true, 210);
            this.setKolom("Batas Timur", "LOKBATAS_T", "LOKBATAS_T", 5, true);
            this.setKolom("Batas Barat", "LOKBATAS_B", "LOKBATAS_B", 6, true);
            this.setKolom("Bentuk", "LOKBENTUK", "LOKBENTUK", 7, true);
            this.setKolom("Peruntukan Tanah", "LOKPERUNTUKAN", "LOKPERUNTUKAN", 8,true,200);
            this.setKolom("Topografi-Kontur", "LOKKONTUR", "LOKKONTUR", 9,true);
            this.setKolom("Topografi-Elevasi", "LOKELEVASI", "LOKELEVASI", 10,true);
            int width= this.setKolom("Aksesbilitas", "LOK_AKSES", "LOK_AKSES", 11,true);
            this.setKolom("GPS", "GPS", "GPS", 12, true);
           // int width= this.setKolom("Longitude", "LOK_AKSES", "LOK_AKSES", 13, true);

           this.show_record = true;

            this.gridDoubleClickDetail = true;
            this.gvUcDtl.BestFitColumns();
           
        }



        //bila di pasang pada detail Master
        public ucLokasiBangunan(decimal? id_kbdg = null, string _status = "edit")
            : base()
        {

            //this.targetPanel = _targetPanel;
            this.ID_KBDG = id_kbdg;
            this.Status = _status;
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


        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            
            this.gvUcDtl.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvUcDtl_InitNewRow);

           
            this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tampilMap);
            this.bbTambah.Caption = konfigApp.labelTambah;
            this.initGrid();
            this.getinitDokBangunan();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
          if(jmlData == 0)
          {
            this.btnMap.Enabled = false;
          }

        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcLokasiBangunanSelect.BPSIMANSROW_M_KBDG_LOKASI)selectedView.GetRow(e.RowHandle);


        }

        protected void tampilMap(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string latitude = "-1.406109";
            string longitude = "115.473631";
            string deskripsi;
            try
            {
                string[] gps = selectedData.GPS.Split(',');
                latitude = gps[0];
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

                PU = new PuLokasiBangunan("input", this.ID_KBDG, null);
                this.StatusCrud = "input";
                PU.simpanLokasiBangunan = new SimpanLokasiBangunan(this.simpanLokasiBangunan);
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
                    PU = new PuLokasiBangunan("detail", selectedData);
                    this.StatusCrud = "detail";
                }
                else
                {
                    PU = new PuLokasiBangunan("edit", selectedData);
                    this.StatusCrud = "edit";
                }
                PU.simpanLokasiBangunan = new SimpanLokasiBangunan(this.simpanLokasiBangunan);

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
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);

            }

        }



        private string get_where_clause(string nama_kolom, string opr, string parameter, string parameter2)
        {
            string where = "";
            
            
            switch (nama_kolom)
            {
                case "ID Lokasi Bangunan":
                    where = "ID_M_KBDG_LOKASI " + get_operator("Number", opr, parameter, parameter2);
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
                    SvcLokasiBangunanCrud.InputParameters parInp = new SvcLokasiBangunanCrud.InputParameters();
                    parInp.P_ID_KBDG = selectedData.ID_KBDG;
                    parInp.P_ID_KBDGSpecified = true;
                    parInp.P_ID_M_KBDG_LOKASI = selectedData.ID_M_KBDG_LOKASI;
                    parInp.P_ID_M_KBDG_LOKASISpecified = true;
                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcLokasiBangunanCrud = new SvcLokasiBangunanCrud.call_pttClient(konfigApp.SvcLokasiBangunanCrud_name, konfigApp.SvcLokasiBangunanCrud_address);
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
                selectedData = (SvcLokasiBangunanSelect.BPSIMANSROW_M_KBDG_LOKASI)selectedView.GetRow(e.FocusedRowHandle);
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

                if (selectedData.GPS != "-")
                {
                  jmlData = selectedView.SelectedRowsCount;
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
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcLokasiBangunanSelect.InputParameters parInp = new SvcLokasiBangunanSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format(" ID_KBDG = {0} {1}", this.ID_KBDG, _where);
            fetchData = new SvcLokasiBangunanSelect.call_pttClient(konfigApp.SvcLokasiBangunanSelect_name, konfigApp.SvcLokasiBangunanSelect_address);
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

        private delegate void ShowData(SvcLokasiBangunanSelect.OutputParameters dataOut);

        public void showData(SvcLokasiBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_M_KBDG_LOKASI.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_M_KBDG_LOKASI[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_M_KBDG_LOKASI[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_LOKASI[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_M_KBDG_LOKASI[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_M_KBDG_LOKASI[i]);
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

        private void simpanLokasiBangunan(SvcLokasiBangunanCrud.InputParameters parInp)
        {
            try
            {
                myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                myThread.Start();

                this.nonAktifForm("");
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                svcLokasiBangunanCrud = new SvcLokasiBangunanCrud.call_pttClient(konfigApp.SvcLokasiBangunanCrud_name, konfigApp.SvcLokasiBangunanCrud_address);
                svcLokasiBangunanCrud.Open();
                svcLokasiBangunanCrud.Beginexecute(parInp, new AsyncCallback(prosesCrud), "");
            }
            catch (Exception)
            {

                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }


        }
        private void prosesCrud(IAsyncResult result)
        {
            try
            {
                doutCrud = svcLokasiBangunanCrud.Endexecute(result);
                svcLokasiBangunanCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
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

        private delegate void ShowDataCrud(SvcLokasiBangunanCrud.OutputParameters dataOut);

        private delegate void TutupPopUp(string data);

        public void tutupPopUp(string data)
        {
            this.PU.Close();
        }
        public void showDataCrud(SvcLokasiBangunanCrud.OutputParameters dataOut)
        {
            SvcLokasiBangunanSelect.BPSIMANSROW_M_KBDG_LOKASI dataPenyama = new SvcLokasiBangunanSelect.BPSIMANSROW_M_KBDG_LOKASI();
            
            switch (this.modeCrud)
            {
                case 'C':
                    this.dataInisial = false;
                    this.getById = true;
                    this.getinitDokBangunan(" ID_M_KBDG_LOKASI = " + dataOut.PO_ID_M_KBDG_LOKASI.ToString());
                    break;
                case 'U':
                    this.binder.Remove(selectedData);
                    this.dataInisial = false;
                    this.getById = true;
                    this.getinitDokBangunan(" ID_M_KBDG_LOKASI = " + dataOut.PO_ID_M_KBDG_LOKASI.ToString());
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
