using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.PK
{
    class ucPropKhusus : UserControlDetail
    {
        private SvcPropertiKhususSelect.call_pttClient fetchData;
        public SvcPropertiKhususSelect.OutputParameters outDat;
        public SvcPropertiKhususSelect.BPSIMANSROW_KPROK selectedData;
        //public NonAktifkanFormSatker nonAktifForm;
        //public AktifkanFormSatker aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        private Thread myThread_;
        SvcPropertiKhususSelect.BPSIMANSROW_KPROK newRow;
        private SvcPropKhususCrud.call_pttClient SvcPropKhususCrud;
        private SvcPropKhususCrud.OutputParameters outDataCrud;
        private string strCari = "";
        private string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private ColumnView View;
        private bool FROM_TRACKING;
        public SetPanel setPanel;

        private decimal? ID_ASET;

        

        private string coba;


        public ucPropKhusus(bool tracking = false)
        {
            FROM_TRACKING = tracking;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcPropertiKhususSelect.BPSIMANSROW_KPROK);
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
            this.setKolom("Sts Pelayanan", "STATUS_KELOLA", "STATUS_KELOLA", 17, true, 120, "string");
            gridDoubleClick = true;
            show_record = true;
            this.ShowFooter(true);
            this.SetSummary(9, "TGL_PERLH", "Total", "T O T A L");
            if (Count == "y")
            {
                this.SetSummary(10, "SUM_RPH_ASET", "SumTotal");
                this.SetSummary(11, "SUM_RPH_MUTASI", "SumTotal");
                this.SetSummary(12, "SUM_NILAI_SBLM_SUSUT", "SumTotal");
                this.SetSummary(13, "SUM_RPH_SUSUT", "SumTotal");
            }
            else 
            {  
                this.SetSummary(10, "SUM_RPH_ASET", "Judul", string.Format("{0:0,0.00}", this.SUM_RPH_ASET));
                this.SetSummary(11, "SUM_RPH_MUTASI", "Judul", string.Format("{0:0,0.00}", this.SUM_RPH_MUTASI));
                this.SetSummary(12, "SUM_NILAI_SBLM_SUSUT", "Judul", string.Format("{0:0,0.00}", this.SUM_NILAI_SBLM_SUSUT));
                this.SetSummary(13, "SUM_RPH_SUSUT", "Judul", string.Format("{0:0,0.00}", this.SUM_RPH_SUSUT));
            }
            //this.SetSummary(11, "NILAI_BUKU", "Sum"); // SUM_RPH_ASET - SUM_RPH_MUTASI
            
            this.SetSummary(14, "NILAI_BUKU", "TOT_NILAI_BUKU"); // SUM_RPH_ASET + SUM_RPH_MUTASI + SUM_RPH_SUSUT
            
        }

        protected override void ucDetail_Load(object sender, EventArgs e)
        {

            this.bbEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbHapus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbTambah.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbMore.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

           
            //this.gvUcDtl.DoubleClick += new System.EventHandler(this.gvUcDtl_DoubleClick);
            //this.initGrid();
            //this.getinitPropKhusus();

            
        }
        

        public void hapusData(SvcPropertiKhususSelect.BPSIMANSROW_KPROK selectedData)
        {
            
            try
            {
                this.nonAktifForm("");
                //MessageBox.Show(selectedData.ID_ASET.ToString(),selectedData.ID_KPROK.ToString());
              //  myThread = new Thread(new ThreadStart(ShowProgresBar),"");
               // myThread.Start();
                SvcPropKhususCrud.InputParameters parInp = new SvcPropKhususCrud.InputParameters();
                parInp.P_SELECT = "D";
                parInp.P_ID_ASET = selectedData.ID_ASET;
                parInp.P_ID_ASETSpecified = true;
                parInp.P_ID_ASET_KTNH = selectedData.ID_ASET_KTNH;
                parInp.P_ID_ASET_KTNHSpecified = true;
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                SvcPropKhususCrud = new SvcPropKhususCrud.call_pttClient();
                SvcPropKhususCrud.Open();
                SvcPropKhususCrud.Beginexecute(parInp, new AsyncCallback(getResultCrud), "");
            }
            catch
            {
                this.modeCrud = 'A';
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
            }
        }

        private void getResultCrud(IAsyncResult result)
        {
            try
            {
                outDataCrud = SvcPropKhususCrud.Endexecute(result);
                SvcPropKhususCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.msg_get_database(outDataCrud.PO_RESULT, "Y", 0);
                if (String.Compare(outDataCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    this.Invoke(new UbahPropKhusus(this.ubahPropKhusus), outDataCrud);
                }
            }
            catch
            {
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
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

        private delegate void UbahPropKhusus(SvcPropKhususCrud.OutputParameters dataOutCrud);

        private void ubahPropKhusus(SvcPropKhususCrud.OutputParameters dataOutCrud)
        {
            SvcPropertiKhususSelect.BPSIMANSROW_KPROK dataPenyama = new SvcPropertiKhususSelect.BPSIMANSROW_KPROK();
            dataPenyama.ID_KPROK = dataOutCrud.PO_ID_KPROK;
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
            newRow = (SvcPropertiKhususSelect.BPSIMANSROW_KPROK)selectedView.GetRow(e.RowHandle);

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
            this.getinitPropKhusus(BySatker);
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initialData = false;
            this.dataInisial = false;
            this.getinitPropKhusus(BySatker);
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
                this.getinitPropKhusus(get_where_clause(nama_kolom, opr, parameter, parameter_2));
                
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
                    where = "Upper(NO_ASET) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcPropertiKhususSelect.BPSIMANSROW_KPROK)selectedView.GetRow(e.FocusedRowHandle);
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
        public string Count = "y";
        public void getinitPropKhusus(string strwhere = null)
        {
            this.nonAktifForm("");
            myThread_ = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread_.Start();
            SvcPropertiKhususSelect.InputParameters parInp = new SvcPropertiKhususSelect.InputParameters();

            string where = "";
            if (FROM_TRACKING == false)
            {
                where = BySatker;
                if (strwhere != null)
                {
                    where = BySatker + " AND " + strwhere;
                }
                else
                {
                    where = BySatker;// BySatker;
                }
            }
            else
            {
                if (strwhere != null)
                {
                    where = strwhere;
                }
                else
                {
                    where = "";
                }
            }
            if (this.dataInisial == true)
            {
                this.currentMaks = this.dataAkhir;
                this.currentMin = this.dataAwal;
                parInp.P_COUNT = "y";                
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
                dataInisial = false;
                parInp.P_COUNT = "n";

                this.gvUcDtl.Columns[10].Summary.Clear();
                this.gvUcDtl.Columns[11].Summary.Clear();
                this.gvUcDtl.Columns[12].Summary.Clear();
                this.gvUcDtl.Columns[13].Summary.Clear();
                //this.SetSummary(12, "SUM_NILAI_SBLM_SUSUT", "Judul", Convert.ToString(this.SUM_NILAI_SBLM_SUSUT));
                //this.SetSummary(12, "SUM_NILAI_SBLM_SUSUT", "TOT_NILAI_BUKU");
                this.SetSummary(10, "SUM_RPH_ASET", "Judul", string.Format("{0:0,0.00}", this.SUM_RPH_ASET));
                this.SetSummary(11, "SUM_RPH_MUTASI", "Judul", string.Format("{0:0,0.00}", this.SUM_RPH_MUTASI));
                this.SetSummary(12, "SUM_NILAI_SBLM_SUSUT", "Judul", string.Format("{0:0,0.00}", this.SUM_NILAI_SBLM_SUSUT));
                this.SetSummary(13, "SUM_RPH_SUSUT", "Judul", string.Format("{0:0,0.00}", this.SUM_RPH_SUSUT));
            }
            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);
            if (this.dataInisial == true)
            {
                this.search = " STATUS_BMN_YN = 'Y' AND BPYBDS_YN = 'N' " + ((where.Length > 1) ? " AND " + where : "");
            }
            parInp.P_MAX = this.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = this.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "KD_SATKER";
            
            parInp.P_SORT = ",KD_BRG,TO_NUMBER(NO_ASET) DESC";
            parInp.STR_WHERE = this.search;
            fetchData = new SvcPropertiKhususSelect.call_pttClient();
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outDat = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                //if(!FROM_TRACKING)
                this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)
                    this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
               //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
               MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcPropertiKhususSelect.OutputParameters dataOut);

        ucPropKhususForm PropKhususForm;
        
        public void showData(SvcPropertiKhususSelect.OutputParameters serviceOutPut)
        {
            if (!FROM_TRACKING)
            {
                int jmlData = serviceOutPut.SF_ROW_MKPROK.Count();

                if (this.dataInisial == true)
                {
                    this.binder.Clear();
                    if (jmlData > 0)
                    {
                        StrTotalGrid.Caption = jmlData.ToString();
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_MKPROK[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalGrid.Caption = "0";
                        StrTotalDb.Caption = "0";
                    }
                }
                else
                {
                    if (jmlData > 0)
                    {

                        StrTotalGrid.Caption = (Convert.ToInt64(StrTotalGrid.Caption) + jmlData).ToString();
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_MKPROK[0].TOTAL_DATA.ToString();
                    }
                }
                if (jmlData > 0 && this.dataInisial == true)
                {
                    this.SUM_RPH_ASET = serviceOutPut.SF_ROW_MKPROK[0].SUM_RPH_ASET;
                    this.SUM_RPH_MUTASI = serviceOutPut.SF_ROW_MKPROK[0].SUM_RPH_MUTASI;
                    this.SUM_RPH_SUSUT = serviceOutPut.SF_ROW_MKPROK[0].SUM_RPH_SUSUT;
                    this.SUM_NILAI_SBLM_SUSUT = serviceOutPut.SF_ROW_MKPROK[0].SUM_NILAI_SBLM_SUSUT;
                    
                   
                }

                for (int i = 0; i < jmlData; i++)
                {
                    //string a = Convert.ToString(i);
                    //string c = Convert.ToString(serviceOutPut.SF_ROW_MKPROK[i].SUM_NILAI_SBLM_SUSUT);
                    //MessageBox.Show(c, a);
                    
                    if (this.dataInisial == true)
                    {
                        serviceOutPut.SF_ROW_MKPROK[i].SUM_NILAI_SBLM_SUSUT = this.SUM_NILAI_SBLM_SUSUT;
                        serviceOutPut.SF_ROW_MKPROK[i].SUM_RPH_SUSUT = this.SUM_RPH_ASET;
                        serviceOutPut.SF_ROW_MKPROK[i].SUM_RPH_MUTASI = this.SUM_RPH_MUTASI;
                        serviceOutPut.SF_ROW_MKPROK[i].SUM_RPH_ASET = this.SUM_RPH_ASET;                        
                        //this.binder.Add(serviceOutPut.SF_ROW_MKPROK[i]);
                    }
                    else {
                        //serviceOutPut.SF_ROW_MKPROK[i].SUM_NILAI_SBLM_SUSUT = 0;
                        //serviceOutPut.SF_ROW_MKPROK[i].SUM_RPH_SUSUT = 0;
                        //serviceOutPut.SF_ROW_MKPROK[i].SUM_RPH_MUTASI = 0;
                        //serviceOutPut.SF_ROW_MKPROK[i].SUM_RPH_ASET = 0;
                        ///*this.SUM_RPH_ASET = serviceOutPut.SF_ROW_MKPROK[0].SUM_RPH_ASET;
                        //this.SUM_RPH_MUTASI = serviceOutPut.SF_ROW_MKPROK[0].SUM_RPH_MUTASI;
                        //this.SUM_RPH_SUSUT = serviceOutPut.SF_ROW_MKPROK[0].SUM_RPH_SUSUT;
                        //this.SUM_NILAI_SBLM_SUSUT = serviceOutPut.SF_ROW_MKPROK[0].SUM_NILAI_SBLM_SUSUT;*/
                        //this.binder.Add(serviceOutPut.SF_ROW_MKPROK[i]);
                    }
                    //this.binder.Remove(serviceOutPut.SF_ROW_MKPROK[i]);
                    //this.binder.ResumeBinding();
                    this.binder.Add(serviceOutPut.SF_ROW_MKPROK[i]);
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
                int jmlData = serviceOutPut.SF_ROW_MKPROK.Count();

                if (jmlData > 0)
                {
                    PropKhususForm = new ucPropKhususForm("detail");
                    PropKhususForm.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                    PropKhususForm.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);

                    PropKhususForm.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                    PropKhususForm.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                    PropKhususForm.Dock = System.Windows.Forms.DockStyle.Fill;
                    PropKhususForm.IsiForm(serviceOutPut.SF_ROW_MKPROK[0]);
                }
                else
                {
                    MessageBox.Show("Data aset tidak ditemukan", "Perhatian");
                }
                setPanel(PropKhususForm);
               
                //this.frmKoorSatker.panelKoorSatker.Controls.Clear();
                //this.frmKoorSatker.panelKoorSatker.Controls.Add(PropKhususForm);
            }

            this.gvUcDtl.BestFitColumns();
        }

        
    }
}
