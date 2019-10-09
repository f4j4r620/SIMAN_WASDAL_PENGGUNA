using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.BPYBDS
{
    class ucBPYBDS : UserControlDetail
    {
        private SvcAsetSelect.call_pttClient fetchData;
        public SvcAsetSelect.OutputParameters outDat;
        public SvcAsetSelect.BPSIMANSROW_M_ASET selectedData;
        //public NonAktifkanForm nonAktifForm;
        //public AktifkanForm aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public ProgBar CloseProgresBar;
        SvcAsetSelect.BPSIMANSROW_M_ASET newRow;
        private SvcJlnJmbtnCrud.call_pttClient svcJlnJmbtnCrud;
        private SvcJlnJmbtnCrud.OutputParameters outDataCrud;
        private string strCari = "";
        private string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private ColumnView View;
        private bool FROM_TRACKING;
        public SetPanel setPanel;
        
        private decimal? ID_ASET;

        

        private string coba;


        public ucBPYBDS(bool tracking = false)
        {
            FROM_TRACKING = tracking;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcAsetSelect.BPSIMANSROW_M_ASET);
            this.gcUcDtl.DataSource = binder;
            this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            this.setKolom("No", "NUM", "NUM", 0, false, 80);
            this.setKolom("Kode Satker", "KD_SATKER", "KD_SATKER", 1, true);
            this.setKolom("Nama Satker", "UR_SATKER", "UR_SATKER", 2, true);
            this.setKolom("Kode Barang", "KD_BRG", "KD_BRG", 3, true, 120, "string", false);

            this.setKolom("NUP", "NO_ASET", "NO_ASET", 4, true, 100, "integer", false);
            this.setKolom("KIB", "NO_KIB", "NO_KIB", 5, true, 100, "integer", false);
            this.setKolom("Nama Barang", "UR_SSKEL", "UR_SSKEL", 6, true, 120, "string", false);
            this.setKolom("Kondisi", "UR_KONDISI", "UR_KONDISI", 7, true, 120, "string", false);
            this.setKolom("Merk/Tipe", "MERK", "MERK", 8, true);
            this.setKolom("Tgl Perolehan", "TGL_PERLH", "TGL_PERLH", 9, true, 120, "date");
            this.setKolom("Nilai Perolehan", "RPH_ASET", "RPH_ASET", 10, true, 120, "number");
            this.setKolom("Nilai Mutasi", "RPH_MUTASI", "RPH_MUTASI", 11, true, 120, "number");
            this.setKolom("Nilai Sebelum Penyusutan", "NILAI_SBLM_SUSUT", "NILAI_SBLM_SUSUT", 12, true, 120, "number");
            this.setKolom("Nilai Penyusutan", "RPH_SUSUT", "RPH_SUSUT", 13, true, 120, "number");
            this.setKolom("Nilai Buku", "NILAI_BUKU", "NILAI_BUKU", 14, true, 120, "number", false);
            this.setKolom("Kuantitas", "KUANTITAS", "KUANTITAS", 15, true, 120, "integer");
            this.setKolom("Jml Foto", "JML_PHOTO", "JML_PHOTO", 16, true, 120, "integer");
            gridDoubleClick = true;
            show_record = true;
            this.ShowFooter(true);
            this.SetSummary(9, "TGL_PERLH", "Total", "T O T A L");
            this.SetSummary(10, "SUM_RPH_ASET", "SumTotal");
            this.SetSummary(11, "SUM_RPH_MUTASI", "SumTotal");
            //this.SetSummary(11, "NILAI_BUKU", "Sum"); // SUM_RPH_ASET - SUM_RPH_MUTASI
            this.SetSummary(13, "SUM_RPH_SUSUT", "SumTotal");
            this.SetSummary(14, "NILAI_BUKU", "TOT_NILAI_BUKU"); // SUM_RPH_ASET + SUM_RPH_MUTASI + SUM_RPH_SUSUT
        }

        protected override void ucDetail_Load(object sender, EventArgs e)
        {

            this.bbEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbHapus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbTambah.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbMore.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

           
            this.gvUcDtl.DoubleClick += new System.EventHandler(this.gvUcDtl_DoubleClick);
            //this.initGrid();
            //this.getinitBPYBDS();

            
        }
        

        public void hapusData(SvcAsetSelect.BPSIMANSROW_M_ASET selectedData)
        {
            
            try
            {
                this.nonAktifForm("");
                //MessageBox.Show(selectedData.ID_ASET.ToString(),selectedData.ID_KJALJ.ToString());
              //  myThread = new Thread(new ThreadStart(ShowProgresBar),"");
               // myThread.Start();
                SvcJlnJmbtnCrud.InputParameters parInp = new SvcJlnJmbtnCrud.InputParameters();
                parInp.P_SELECT = "D";
                parInp.P_ID_ASET = selectedData.ID_ASET;
                parInp.P_ID_ASETSpecified = true;
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                svcJlnJmbtnCrud = new SvcJlnJmbtnCrud.call_pttClient();
                svcJlnJmbtnCrud.Open();
                svcJlnJmbtnCrud.Beginexecute(parInp, new AsyncCallback(getResultCrud), "");
            }
            catch
            {
                this.modeCrud = 'A';
                this.Invoke(new ProgBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void getResultCrud(IAsyncResult result)
        {
            try
            {
                outDataCrud = svcJlnJmbtnCrud.Endexecute(result);
                svcJlnJmbtnCrud.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.msg_get_database(outDataCrud.PO_RESULT, "Y", 0);
                if (String.Compare(outDataCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    this.Invoke(new UbahBPYBDS_YN(this.ubahBPYBDS_YN), outDataCrud);
                }
            }
            catch
            {
                this.Invoke(new ProgBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
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

        private delegate void UbahBPYBDS_YN(SvcJlnJmbtnCrud.OutputParameters dataOutCrud);

        private void ubahBPYBDS_YN(SvcJlnJmbtnCrud.OutputParameters dataOutCrud)
        {
            SvcAsetSelect.BPSIMANSROW_M_ASET dataPenyama = new SvcAsetSelect.BPSIMANSROW_M_ASET();
           
            dataPenyama.ID_ASET = dataOutCrud.PO_ID_ASET;

            dataPenyama.NUM = 99;
            dataPenyama.NUMSpecified = true;
            switch (this.modeCrud)
            {
                case 'C':
                    this.binder.Add(dataPenyama);
                    break;
                case 'U':
                    this.binder.Remove(this.selectedData);
                    this.binder.Add(dataPenyama);
                    this.dataInisial = true;
                    this.initGrid();
                    break;
                case 'D':
                    this.binder.Remove(this.selectedData);
                    break;
            }
        }

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            selectedView = sender as GridView;
            newRow = (SvcAsetSelect.BPSIMANSROW_M_ASET)selectedView.GetRow(e.RowHandle);

        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initGrid();
            this.getinitBPYBDS();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initialData = false;
            this.dataInisial = false;
            this.getinitBPYBDS();
        }

        protected override void bbSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            /// TES
           
            
            //-----------------------
            try
            {
                string nama_kolom = this.LuKolom.EditValue.ToString();
                string opr = this.barOperator.EditValue.ToString().ToUpper();
                string parameter = this.teSearch.EditValue.ToString().ToUpper();
                string parameter_2="";
                if (opr == "ANTARA")
                {
                    parameter_2 = this.teSearch2.EditValue.ToString().ToUpper();
                }
                this.dataInisial = true;
                this.getinitBPYBDS(get_where_clause(nama_kolom, opr, parameter, parameter_2));
                
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
                case "Kode Satker":
                    where = "Upper(KD_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nama Satker":
                    where = "Upper(UR_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Kode Barang":
                    where = "Upper(KD_BRG) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "NUP":
                    where = "Upper(NO_ASET) " + get_operator("Integer", opr, parameter, parameter2);
                    break;
                case "KIB":
                    where = "Upper(NO_KIB) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nama Barang":
                    where = "Upper(UR_SSKEL) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Kondisi":
                    where = "Upper(UR_KONDISI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Merk/Tipe":
                    where = "Upper(MERK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Nilai Perolehan":
                    where = "Upper(RPH_ASET) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Tgl Perolehan":
                    where = "Upper(TGL_PERLH) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Nilai Buku":
                    where = "Upper(NILAI_BUKU) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Tgl Buku":
                    where = "Upper(TGL_BUKU) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Nilai Mutasi":
                    where = "Upper(RPH_MUTASI) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Nilai Sebelum Penyusutan":
                    where = "Upper(NILAI_SBLM_SUSUT) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Nilai Penyusutan":
                    where = "Upper(RPH_SUSUT) " + get_operator("Float", opr, parameter, parameter2);
                    break;
                case "Kuantitas":
                    where = "Upper(KUANTITAS) " + get_operator("Integer", opr, parameter, parameter2);
                    break;
                case "Jml Foto":
                    where = "Upper(JML_PHOTO) " + get_operator("Integer", opr, parameter, parameter2);
                    break;
                case "Sts Pelayanan":
                    where = "Upper(STATUS_KELOLA) " + get_operator("String", opr, parameter, parameter2);
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

        }


        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcAsetSelect.BPSIMANSROW_M_ASET)selectedView.GetRow(e.FocusedRowHandle);
                if (selectedView.IsLastRow)
                {
                    LastRow = true;
                }
                else
                {
                    LastRow = false;
                }
                posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
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

        public string BySatker;
        public void getinitBPYBDS(string strwhere = null)
        {
            
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcAsetSelect.InputParameters parInp = new SvcAsetSelect.InputParameters();

            string where = "";
            if (FROM_TRACKING == false)
            {
                where = BySatker;
                if (strwhere != null)
                {
                    where = "BPYBDS_YN = 'Y' AND " + BySatker + " AND " + strwhere;
                }
                else
                {
                    where = "BPYBDS_YN = 'Y' AND " + BySatker;// BySatker;
                }
            }
            else
            {
               
                if (strwhere != null)
                {
                    where = "BPYBDS_YN = 'Y' AND " + BySatker + " AND " + strwhere;
                }
                else
                {
                    where = "BPYBDS_YN = 'Y'";
                }
            }
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
                dataInisial = false;
            }
            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);
            if (this.dataInisial == true)
            {
                this.search = " STATUS_BMN_YN = 'Y' " + ((where.Length > 1) ? " AND " + where : "");
            }
            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "KD_SATKER";
            parInp.P_SORT = ",KD_BRG,NO_ASET DESC";
            parInp.STR_WHERE = this.search;
            fetchData = new SvcAsetSelect.call_pttClient();
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
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
               //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
               MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcAsetSelect.OutputParameters dataOut);

 


        public void showData(SvcAsetSelect.OutputParameters serviceOutPut)
        {
            if (!FROM_TRACKING)
            {
                int jmlData = serviceOutPut.SF_ROW_M_ASET.Count();

                if (this.dataInisial == true)
                {
                    this.binder.Clear();
                }

                for (int i = 0; i < jmlData; i++)
                {
                    this.binder.Add(serviceOutPut.SF_ROW_M_ASET[i]);
                }

                

                if (jmlData < konfigApp.dataAkhir)
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
            else
            {
               
               
                //this.frmKoorSatker.panelKoorSatker.Controls.Clear();
                //this.frmKoorSatker.panelKoorSatker.Controls.Add(BPYBDS_YNForm);
            }

            this.gvUcDtl.BestFitColumns();
        }

        #region ASET Tanah
        SvcAssetTanahSelect.call_pttClient fetchDataTanah = null;
        SvcAssetTanahSelect.OutputParameters outDatTanah;
        public void getAsetTanah(decimal? _ID_ASET)
          {
              
              myThread = new Thread(new ThreadStart(this.ShowProgresBar));
              myThread.Start();
              this.nonAktifForm("");
              SvcAssetTanahSelect.InputParameters parInp = new SvcAssetTanahSelect.InputParameters();

                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
              
              //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

              parInp.P_MAX = this.currentMaks;
              parInp.P_MAXSpecified = true;
              parInp.P_MIN = this.currentMin;
              parInp.P_MINSpecified = true;
              parInp.P_COL = "";
              parInp.P_SORT = "DESC";
              parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
              fetchDataTanah = new SvcAssetTanahSelect.call_pttClient();
              fetchDataTanah.Open();
              fetchDataTanah.Beginexecute(parInp, new AsyncCallback(this.getResultTanah), null);
          }
           protected void getResultTanah(IAsyncResult result)
        {
            try
            {
                this.outDatTanah = fetchDataTanah.Endexecute(result);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                
                this.Invoke(new ShowDataTanah(this.showDataTanah), this.outDatTanah);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
               //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
               MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataTanah(SvcAssetTanahSelect.OutputParameters dataOutTanah);



        public void showDataTanah(SvcAssetTanahSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_ASET_M_KTNH.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanTanah(serviceOutPut.SF_ROW_ASET_M_KTNH[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                   //  this.frmKoorKorwil.tampilkanTanah(serviceOutPut.SF_ROW_ASET_M_KTNH[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanTanah(serviceOutPut.SF_ROW_ASET_M_KTNH[0]);
                }
            }
        }
        #endregion //ASET TAnah

        #region ASET Bangunan
        SvcBangunanSelect.call_pttClient fetchDataBangunan = null;
        SvcBangunanSelect.OutputParameters outDatBangunan;
        public void getAsetBangunan(decimal? _ID_ASET)
        {
            
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcBangunanSelect.InputParameters parInp = new SvcBangunanSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataBangunan = new SvcBangunanSelect.call_pttClient();
            fetchDataBangunan.Open();
            fetchDataBangunan.Beginexecute(parInp, new AsyncCallback(this.getResultBangunan), null);
        }
        protected void getResultBangunan(IAsyncResult result)
        {
            try
            {
                this.outDatBangunan = fetchDataBangunan.Endexecute(result);
                fetchDataBangunan.Close();
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                
                this.Invoke(new ShowDataBangunan(this.showDataBangunan), this.outDatBangunan);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataBangunan(SvcBangunanSelect.OutputParameters dataOut);



        public void showDataBangunan(SvcBangunanSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_M_KBDG.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanBangunan(serviceOutPut.SF_ROW_M_KBDG[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                     this.frmKoorKorwil.tampilkanBangunan(serviceOutPut.SF_ROW_M_KBDG[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanBangunan(serviceOutPut.SF_ROW_M_KBDG[0]);
                }
            }
        }
        #endregion //ASET Bangunan

        #region ASET RumahNegara
        SvcRmhNgrSelect.call_pttClient fetchDataRumahNegara = null;
        SvcRmhNgrSelect.OutputParameters outDatRumahNegara;
        public void getAsetRumahNegara(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcRmhNgrSelect.InputParameters parInp = new SvcRmhNgrSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataRumahNegara = new SvcRmhNgrSelect.call_pttClient();
            fetchDataRumahNegara.Open();
            fetchDataRumahNegara.Beginexecute(parInp, new AsyncCallback(this.getResultRumahNegara), null);
        }
        protected void getResultRumahNegara(IAsyncResult result)
        {
            try
            {
                this.outDatRumahNegara = fetchDataRumahNegara.Endexecute(result);
                fetchDataRumahNegara.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataRumahNegara(this.showDataRumahNegara), this.outDatRumahNegara);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataRumahNegara(SvcRmhNgrSelect.OutputParameters dataOut);



        public void showDataRumahNegara(SvcRmhNgrSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_M_KRMH_NEG.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanRumahNegara(serviceOutPut.SF_ROW_M_KRMH_NEG[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanRumahNegara(serviceOutPut.SF_ROW_M_KRMH_NEG[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanRumahNegara(serviceOutPut.SF_ROW_M_KRMH_NEG[0]);
                }
            }
        }
        #endregion //ASET Rumah Negara

        #region ASET Angkutan
        SvcAngkSelect.call_pttClient fetchDataAngkutan = null;
        SvcAngkSelect.OutputParameters outDatAngkutan;
        public void getAsetAngkutan(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcAngkSelect.InputParameters parInp = new SvcAngkSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataAngkutan = new SvcAngkSelect.call_pttClient();
            fetchDataAngkutan.Open();
            fetchDataAngkutan.Beginexecute(parInp, new AsyncCallback(this.getResultAngkutan), null);
        }
        protected void getResultAngkutan(IAsyncResult result)
        {
            try
            {
                this.outDatAngkutan = fetchDataAngkutan.Endexecute(result);
                fetchDataAngkutan.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataAngkutan(this.showDataAngkutan), this.outDatAngkutan);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataAngkutan(SvcAngkSelect.OutputParameters dataOut);



        public void showDataAngkutan(SvcAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_KANGK.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanAngkutan(serviceOutPut.SF_ROW_KANGK[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanAngkutan(serviceOutPut.SF_ROW_KANGK[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanAngkutan(serviceOutPut.SF_ROW_KANGK[0]);
                }
            }
        }
        #endregion //ASET Alat Angkutan

        #region ASET MPNT
        SvcMesinPntSelect.call_pttClient fetchDataMPNT = null;
        SvcMesinPntSelect.OutputParameters outDatMPNT;
        public void getAsetMPNT(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcMesinPntSelect.InputParameters parInp = new SvcMesinPntSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataMPNT = new SvcMesinPntSelect.call_pttClient();
            fetchDataMPNT.Open();
            fetchDataMPNT.Beginexecute(parInp, new AsyncCallback(this.getResultMPNT), null);
        }
        protected void getResultMPNT(IAsyncResult result)
        {
            try
            {
                this.outDatMPNT = fetchDataMPNT.Endexecute(result);
                fetchDataMPNT.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataMPNT(this.showDataMPNT), this.outDatMPNT);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataMPNT(SvcMesinPntSelect.OutputParameters dataOut);



        public void showDataMPNT(SvcMesinPntSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKALB.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanMPNT(serviceOutPut.SF_ROW_MKALB[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanMPNT(serviceOutPut.SF_ROW_MKALB[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanMPNT(serviceOutPut.SF_ROW_MKALB[0]);
                }
            }
        }
        #endregion //ASET Alat MPNT

        #region ASET MPKT
        SvcMPKTSelect.call_pttClient fetchDataMPKT = null;
        SvcMPKTSelect.OutputParameters outDatMPKT;
        public void getAsetMPKT(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcMPKTSelect.InputParameters parInp = new SvcMPKTSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataMPKT = new SvcMPKTSelect.call_pttClient();
            fetchDataMPKT.Open();
            fetchDataMPKT.Beginexecute(parInp, new AsyncCallback(this.getResultMPKT), null);
        }
        protected void getResultMPKT(IAsyncResult result)
        {
            try
            {
                this.outDatMPKT = fetchDataMPKT.Endexecute(result);
                fetchDataMPKT.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataMPKT(this.showDataMPKT), this.outDatMPKT);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataMPKT(SvcMPKTSelect.OutputParameters dataOut);



        public void showDataMPKT(SvcMPKTSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKKTIK.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanMPKT(serviceOutPut.SF_ROW_MKKTIK[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanMPKT(serviceOutPut.SF_ROW_MKKTIK[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanMPKT(serviceOutPut.SF_ROW_MKKTIK[0]);
                }
            }
        }
        #endregion //ASET Alat MPKT

        #region ASET Senjata
        SvcSenjataSelect.call_pttClient fetchDataSenjata = null;
        SvcSenjataSelect.OutputParameters outDatSenjata;
        public void getAsetSenjata(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcSenjataSelect.InputParameters parInp = new SvcSenjataSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataSenjata = new SvcSenjataSelect.call_pttClient();
            fetchDataSenjata.Open();
            fetchDataSenjata.Beginexecute(parInp, new AsyncCallback(this.getResultSenjata), null);
        }
        protected void getResultSenjata(IAsyncResult result)
        {
            try
            {
                this.outDatSenjata = fetchDataSenjata.Endexecute(result);
                fetchDataSenjata.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataSenjata(this.showDataSenjata), this.outDatSenjata);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataSenjata(SvcSenjataSelect.OutputParameters dataOut);



        public void showDataSenjata(SvcSenjataSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKSENJ.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanSenjata(serviceOutPut.SF_ROW_MKSENJ[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanSenjata(serviceOutPut.SF_ROW_MKSENJ[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanSenjata(serviceOutPut.SF_ROW_MKSENJ[0]);
                }
            }
        }
        #endregion //ASET Alat Senjata

        #region ASET JalanJembatan
        SvcJlnJmbtnSelect.call_pttClient fetchDataJalanJembatan = null;
        SvcJlnJmbtnSelect.OutputParameters outDatJalanJembatan;
        public void getAsetJalanJembatan(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcJlnJmbtnSelect.InputParameters parInp = new SvcJlnJmbtnSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataJalanJembatan = new SvcJlnJmbtnSelect.call_pttClient();
            fetchDataJalanJembatan.Open();
            fetchDataJalanJembatan.Beginexecute(parInp, new AsyncCallback(this.getResultJalanJembatan), null);
        }
        protected void getResultJalanJembatan(IAsyncResult result)
        {
            try
            {
                this.outDatJalanJembatan = fetchDataJalanJembatan.Endexecute(result);
                fetchDataJalanJembatan.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataJalanJembatan(this.showDataJalanJembatan), this.outDatJalanJembatan);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataJalanJembatan(SvcJlnJmbtnSelect.OutputParameters dataOut);



        public void showDataJalanJembatan(SvcJlnJmbtnSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKJALJ.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanJalanJembatan(serviceOutPut.SF_ROW_MKJALJ[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanJalanJembatan(serviceOutPut.SF_ROW_MKJALJ[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanJalanJembatan(serviceOutPut.SF_ROW_MKJALJ[0]);
                }
            }
        }
        #endregion //ASET Alat JalanJembatan

        #region ASET BangunanAir
        SvcBangunanAirSelect.call_pttClient fetchDataBangunanAir = null;
        SvcBangunanAirSelect.OutputParameters outDatBangunanAir;
        public void getAsetBangunanAir(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcBangunanAirSelect.InputParameters parInp = new SvcBangunanAirSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataBangunanAir = new SvcBangunanAirSelect.call_pttClient();
            fetchDataBangunanAir.Open();
            fetchDataBangunanAir.Beginexecute(parInp, new AsyncCallback(this.getResultBangunanAir), null);
        }
        protected void getResultBangunanAir(IAsyncResult result)
        {
            try
            {
                this.outDatBangunanAir = fetchDataBangunanAir.Endexecute(result);
                fetchDataBangunanAir.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataBangunanAir(this.showDataBangunanAir), this.outDatBangunanAir);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataBangunanAir(SvcBangunanAirSelect.OutputParameters dataOut);



        public void showDataBangunanAir(SvcBangunanAirSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKBAIR.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanBangunanAir(serviceOutPut.SF_ROW_MKBAIR[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanBangunanAir(serviceOutPut.SF_ROW_MKBAIR[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanBangunanAir(serviceOutPut.SF_ROW_MKBAIR[0]);
                }
            }
        }
        #endregion //ASET Alat BangunanAir

        #region ASET PropertiKhusus
        SvcPropertiKhususSelect.call_pttClient fetchDataPropertiKhusus = null;
        SvcPropertiKhususSelect.OutputParameters outDatPropertiKhusus;
        public void getAsetPropertiKhusus(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcPropertiKhususSelect.InputParameters parInp = new SvcPropertiKhususSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataPropertiKhusus = new SvcPropertiKhususSelect.call_pttClient();
            fetchDataPropertiKhusus.Open();
            fetchDataPropertiKhusus.Beginexecute(parInp, new AsyncCallback(this.getResultPropertiKhusus), null);
        }
        protected void getResultPropertiKhusus(IAsyncResult result)
        {
            try
            {
                this.outDatPropertiKhusus = fetchDataPropertiKhusus.Endexecute(result);
                fetchDataPropertiKhusus.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataPropertiKhusus(this.showDataPropertiKhusus), this.outDatPropertiKhusus);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataPropertiKhusus(SvcPropertiKhususSelect.OutputParameters dataOut);



        public void showDataPropertiKhusus(SvcPropertiKhususSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKPROK.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanPropertiKhusus(serviceOutPut.SF_ROW_MKPROK[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanPropertiKhusus(serviceOutPut.SF_ROW_MKPROK[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanPropertiKhusus(serviceOutPut.SF_ROW_MKPROK[0]);
                }
            }
        }
        #endregion //ASET PropertiKhusus

        #region ASET AsetLainnya
        SvcLnySelect.call_pttClient fetchDataAsetLainnya = null;
        SvcLnySelect.OutputParameters outDatAsetLainnya;
        public void getAsetAsetLainnya(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcLnySelect.InputParameters parInp = new SvcLnySelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataAsetLainnya = new SvcLnySelect.call_pttClient();
            fetchDataAsetLainnya.Open();
            fetchDataAsetLainnya.Beginexecute(parInp, new AsyncCallback(this.getResultAsetLainnya), null);
        }
        protected void getResultAsetLainnya(IAsyncResult result)
        {
            try
            {
                this.outDatAsetLainnya = fetchDataAsetLainnya.Endexecute(result);
                fetchDataAsetLainnya.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataAsetLainnya(this.showDataAsetLainnya), this.outDatAsetLainnya);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataAsetLainnya(SvcLnySelect.OutputParameters dataOut);



        public void showDataAsetLainnya(SvcLnySelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKLAIN.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanAsetLainnya(serviceOutPut.SF_ROW_MKLAIN[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanAsetLainnya(serviceOutPut.SF_ROW_MKLAIN[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanAsetLainnya(serviceOutPut.SF_ROW_MKLAIN[0]);
                }
            }
        }
        #endregion //ASET AsetLainnya

        #region ASET KDP
        SvcKDPSelect.call_pttClient fetchDataKDP = null;
        SvcKDPSelect.OutputParameters outDatKDP;
        public void getAsetKDP(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcKDPSelect.InputParameters parInp = new SvcKDPSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataKDP = new SvcKDPSelect.call_pttClient();
            fetchDataKDP.Open();
            fetchDataKDP.Beginexecute(parInp, new AsyncCallback(this.getResultKDP), null);
        }
        protected void getResultKDP(IAsyncResult result)
        {
            try
            {
                this.outDatKDP = fetchDataKDP.Endexecute(result);
                fetchDataKDP.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataKDP(this.showDataKDP), this.outDatKDP);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataKDP(SvcKDPSelect.OutputParameters dataOut);



        public void showDataKDP(SvcKDPSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKDP.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanKDP(serviceOutPut.SF_ROW_MKDP[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanKDP(serviceOutPut.SF_ROW_MKDP[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanKDP(serviceOutPut.SF_ROW_MKDP[0]);
                }
            }
        }
        #endregion //ASET KDP

        #region ASET ATB
        SvcATBSelect.call_pttClient fetchDataATB = null;
        SvcATBSelect.OutputParameters outDatATB;
        public void getAsetATB(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcATBSelect.InputParameters parInp = new SvcATBSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataATB = new SvcATBSelect.call_pttClient();
            fetchDataATB.Open();
            fetchDataATB.Beginexecute(parInp, new AsyncCallback(this.getResultATB), null);
        }
        protected void getResultATB(IAsyncResult result)
        {
            try
            {
                this.outDatATB = fetchDataATB.Endexecute(result);
                fetchDataATB.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataATB(this.showDataATB), this.outDatATB);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataATB(SvcATBSelect.OutputParameters dataOut);



        public void showDataATB(SvcATBSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKTWJD.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanATB(serviceOutPut.SF_ROW_MKTWJD[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanATB(serviceOutPut.SF_ROW_MKTWJD[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanATB(serviceOutPut.SF_ROW_MKTWJD[0]);
                }
            }
        }
        #endregion //ASET ATB

        #region ASET Renovasi
        SvcRenovasiSelect.call_pttClient fetchDataRenovasi = null;
        SvcRenovasiSelect.OutputParameters outDatRenovasi;
        public void getAsetRenovasi(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcRenovasiSelect.InputParameters parInp = new SvcRenovasiSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataRenovasi = new SvcRenovasiSelect.call_pttClient();
            fetchDataRenovasi.Open();
            fetchDataRenovasi.Beginexecute(parInp, new AsyncCallback(this.getResultRenovasi), null);
        }
        protected void getResultRenovasi(IAsyncResult result)
        {
            try
            {
                this.outDatRenovasi = fetchDataRenovasi.Endexecute(result);
                fetchDataRenovasi.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataRenovasi(this.showDataRenovasi), this.outDatRenovasi);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataRenovasi(SvcRenovasiSelect.OutputParameters dataOut);



        public void showDataRenovasi(SvcRenovasiSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MKRNV.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanRenovasi(serviceOutPut.SF_ROW_MKRNV[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanRenovasi(serviceOutPut.SF_ROW_MKRNV[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanRenovasi(serviceOutPut.SF_ROW_MKRNV[0]);
                }
            }
        }
        #endregion //ASET Renovasi

        #region ASET Sejarah
        SvcSjrhSelect.call_pttClient fetchDataSejarah = null;
        SvcSjrhSelect.OutputParameters outDatSejarah;
        public void getAsetSejarah(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcSjrhSelect.InputParameters parInp = new SvcSjrhSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataSejarah = new SvcSjrhSelect.call_pttClient();
            fetchDataSejarah.Open();
            fetchDataSejarah.Beginexecute(parInp, new AsyncCallback(this.getResultSejarah), null);
        }
        protected void getResultSejarah(IAsyncResult result)
        {
            try
            {
                this.outDatSejarah = fetchDataSejarah.Endexecute(result);
                fetchDataSejarah.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataSejarah(this.showDataSejarah), this.outDatSejarah);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataSejarah(SvcSjrhSelect.OutputParameters dataOut);



        public void showDataSejarah(SvcSjrhSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_M_SEJARAH.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanSejarah(serviceOutPut.SF_ROW_M_SEJARAH[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanSejarah(serviceOutPut.SF_ROW_M_SEJARAH[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanSejarah(serviceOutPut.SF_ROW_M_SEJARAH[0]);
                }
            }
        }
        #endregion //ASET Sejarah

        #region ASET Persediaan
        SvcPersediaanSelect.call_pttClient fetchDataPersediaan = null;
        SvcPersediaanSelect.OutputParameters outDatPersediaan;
        public void getAsetPersediaan(decimal? _ID_ASET)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcPersediaanSelect.InputParameters parInp = new SvcPersediaanSelect.InputParameters();

            this.currentMaks = this.dataAkhir;
            this.currentMin = this.dataAwal;

            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "";
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "ID_ASET = " + _ID_ASET;
            fetchDataPersediaan = new SvcPersediaanSelect.call_pttClient();
            fetchDataPersediaan.Open();
            fetchDataPersediaan.Beginexecute(parInp, new AsyncCallback(this.getResultPersediaan), null);
        }
        protected void getResultPersediaan(IAsyncResult result)
        {
            try
            {
                this.outDatPersediaan = fetchDataPersediaan.Endexecute(result);
                fetchDataPersediaan.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowDataPersediaan(this.showDataPersediaan), this.outDatPersediaan);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowDataPersediaan(SvcPersediaanSelect.OutputParameters dataOut);



        public void showDataPersediaan(SvcPersediaanSelect.OutputParameters serviceOutPut)
        {
            int jmlData = serviceOutPut.SF_ROW_MSEDIA.Count();
            if (jmlData > 0)
            {
                if (this.FormUtama == "satker")
                {
                    this.frmKoorSatker.tampilkanPersediaan(serviceOutPut.SF_ROW_MSEDIA[0]);
                }
                else if (this.FormUtama == "korwil")
                {
                    //  this.frmKoorKorwil.tampilkanPersediaan(serviceOutPut.SF_ROW_MSEDIA[0]);
                }
                else if (this.FormUtama == "kl")
                {
                    //  this.frmKoorKl.tampilkanPersediaan(serviceOutPut.SF_ROW_MSEDIA[0]);
                }
            }
        }
        #endregion //ASET Persediaan


    }

}
