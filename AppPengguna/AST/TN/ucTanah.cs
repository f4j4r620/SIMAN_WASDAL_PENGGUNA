﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.TN
{
    class ucTanah : UserControlDetail
    {
        private SvcAssetTanahSelect.call_pttClient fetchData;
        public SvcAssetTanahSelect.OutputParameters outDat;
        public SvcAssetTanahSelect.BPSIMANSROW_M_KTNH selectedData;
        //public NonAktifkanForm nonAktifForm;
        //public AktifkanForm aktifkanForm;
        //public showProgresBar ShowProgresBar;
        //public closeProgresBar CloseProgresBar;
        SvcAssetTanahSelect.BPSIMANSROW_M_KTNH newRow;
        private SvcAssetTanahCrud.call_pttClient svctanahCrud;
        private SvcAssetTanahCrud.OutputParameters outDataCrud;
        private string strCari = "";
        private string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private ColumnView View;
        private bool FROM_TRACKING;
        public SetPanel setPanel;
        public string count = "y";
        private decimal? ID_ASET;

        private FrmKoorSatker targetPanel;

        private string coba;


        public ucTanah(bool tracking = false)
        {
            FROM_TRACKING = tracking;
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcAssetTanahSelect.BPSIMANSROW_M_KTNH);
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
            this.setKolom("Jenis Dokumen", "UR_DOK", "UR_DOK", 8, true, 120, "string", false);
            this.setKolom("Kepemilikan ", "UR_SMILIK", "UR_SMILIK", 9, true, 120, "string", false);
            this.setKolom("Jenis sertifikat", "KD_JNS_SERTI", "KD_JNS_SERTI", 10, true, 120, "string", false);
            this.setKolom("Merk/Tipe", "MERK", "MERK", 11, true);
            this.setKolom("Tgl Perolehan", "TGL_PERLH", "TGL_PERLH", 12, true, 120, "date");
            this.setKolom("Nilai Perolehan", "RPH_ASET", "RPH_ASET", 13, true, 120, "number");
            this.setKolom("Nilai Mutasi", "RPH_MUTASI", "RPH_MUTASI", 14, true, 120, "number");
            this.setKolom("Nilai Sebelum Penyusutan", "NILAI_SBLM_SUSUT", "NILAI_SBLM_SUSUT", 15, true, 120, "number");
            this.setKolom("Nilai Penyusutan", "RPH_SUSUT", "RPH_SUSUT", 16, true, 120, "number");
            this.setKolom("Nilai Buku", "NILAI_BUKU", "NILAI_BUKU", 17, true, 120, "number", false);
            this.setKolom("Kuantitas", "KUANTITAS", "KUANTITAS", 18, true, 120, "integer");
            this.setKolom("Jml Foto", "JML_PHOTO", "JML_PHOTO", 19, true, 120, "integer");
            this.setKolom("Sts Pelayanan", "STATUS_KELOLA", "STATUS_KELOLA", 20, true, 120, "string");
            this.setKolom("Jalan", "LOKASI", "LOKASI", 21, true, 100, "string");
            this.setKolom("RT/RW", "KD_RTRW", "KD_RTRW", 22, true, 50, "string");
            this.setKolom("Kelurahan/Desa", "UR_KEL", "UR_KEL", 23, true, 100, "string");
            this.setKolom("Kecamatan", "KD_KEC", "KD_KEC", 24, true, 100, "string");
            this.setKolom("Kota/Kabupaten", "UR_PROV", "UR_PROV", 25, true, 100, "string");
            this.setKolom("Provinsi", "UR_KAB", "UR_KAB", 26, true, 100, "string");
            this.setKolom("Kode Pos", "KD_POS", "KD_POS", 27, true, 50, "string");
            this.setKolom("Alamat Lainnya", "ALAMAT", "ALAMAT", 28, true, 100, "string");
            //this.setKolom("Jalan", "STATUS_KELOLA", "STATUS_KELOLA", 20, true, 120, "string");
            
            gridDoubleClick = true;
            show_record = true;
            this.ShowFooter(true); 
            this.SetSummary(12, "TGL_PERLH", "Total", "T O T A L");
            this.SetSummary(13, "SUM_RPH_ASET", "SumTotal");
            this.SetSummary(14, "SUM_RPH_MUTASI", "SumTotal");
            this.SetSummary(15, "SUM_NILAI_SBLM_SUSUT", "SumTotal");
            this.SetSummary(16, "SUM_RPH_SUSUT", "SumTotal");

            this.SetSummary(17, "NILAI_BUKU", "TOT_NILAI_BUKU"); // SUM_RPH_ASET + SUM_RPH_MUTASI + SUM_RPH_SUSUT
        }

        protected override void ucDetail_Load(object sender, EventArgs e)
        {

            this.bbEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbHapus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbTambah.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbMore.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            //this.initGrid();
            //this.getinitTanah();
            gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            
        }

    
        public void hapusData(SvcAssetTanahSelect.BPSIMANSROW_M_KTNH selectedData)
        {
            
            try
            {
                this.nonAktifForm("");
                //MessageBox.Show(selectedData.ID_ASET.ToString(),selectedData.ID_KBDG.ToString());
              //  myThread = new Thread(new ThreadStart(ShowProgresBar),"");
               // myThread.Start();
                SvcAssetTanahCrud.InputParameters parInp = new SvcAssetTanahCrud.InputParameters();
                parInp.P_SELECT = "D";
                parInp.P_ID_ASET = selectedData.ID_ASET;
                parInp.P_ID_ASETSpecified = true;
                parInp.P_ID_SATKER = selectedData.ID_SATKER;
                parInp.P_ID_SATKERSpecified = true;
                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                svctanahCrud = new SvcAssetTanahCrud.call_pttClient();
                svctanahCrud.Open();
                svctanahCrud.Beginexecute(parInp, new AsyncCallback(getResultCrud), "");
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
                outDataCrud = svctanahCrud.Endexecute(result);
                svctanahCrud.Close();
                this.Invoke(new closeProgresBar(this.CloseProgresBar),DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.msg_get_database(outDataCrud.PO_RESULT, "Y", 0);
                if (String.Compare(outDataCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    this.Invoke(new UbahTanah(this.ubahTanah), outDataCrud);
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

        private delegate void UbahTanah(SvcAssetTanahCrud.OutputParameters dataOutCrud);

        private void ubahTanah(SvcAssetTanahCrud.OutputParameters dataOutCrud)
        {
            SvcAssetTanahSelect.BPSIMANSROW_M_KTNH dataPenyama = new SvcAssetTanahSelect.BPSIMANSROW_M_KTNH();
            dataPenyama.ID_KTNH = dataOutCrud.PO_ID_KTNH;
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
            newRow = (SvcAssetTanahSelect.BPSIMANSROW_M_KTNH)selectedView.GetRow(e.RowHandle);

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
            this.getinitTanah();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.initialData = false;
            this.dataInisial = false;
            this.getinitTanah();
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
                this.pencarian = true;
                this.getinitTanah(get_where_clause(nama_kolom, opr, parameter, parameter_2));
               
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
                case "Jenis Dokumen":
                    where = "Upper(UR_DOK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Kepemilikan":
                    where = "Upper(UR_SMILIK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jenis sertifikat":
                    where = "Upper(KD_JNS_SERTI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Jalan":
                    where = "Upper(LOKASI) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "RT/RW":
                    where = "Upper(KD_RTRW) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Kelurahan/Desa":
                    where = "Upper(UR_KEL) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Kecamatan":
                    where = "Upper(KD_KEC) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Kota/Kabupaten":
                    where = "Upper(UR_PROV) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Provinsi":
                    where = "Upper(UR_KAB) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Kode Pos":
                    where = "Upper(KD_POS) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Alamat Lainnya":
                    where = "Upper(ALAMAT) " + get_operator("String", opr, parameter, parameter2);
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
                selectedData = (SvcAssetTanahSelect.BPSIMANSROW_M_KTNH)selectedView.GetRow(e.FocusedRowHandle);
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
        public void getinitTanah(String strwhere = null)
        {
            this.nonAktifForm("");
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcAssetTanahSelect.InputParameters parInp = new SvcAssetTanahSelect.InputParameters();

            string where = "";
            if (FROM_TRACKING == false)
            {
                where = BySatker;
                if (strwhere != null)
                {
                    where = where + " AND " + strwhere;
                }
                else
                {
                    where = where;
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
                count = "y";
            }
            else
            {
                this.currentMin = this.currentMaks + 1;
                this.currentMaks = this.currentMaks + this.dataAkhir;
                dataInisial = false;
                parInp.P_COUNT = "n";
                count = "n";
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
            parInp.P_SORT = ",KD_BRG,NO_ASET DESC";
            
            parInp.STR_WHERE = this.search;
            fetchData = new SvcAssetTanahSelect.call_pttClient(konfigApp.SvcAssetTanahSelect_name,konfigApp.SvcAssetTanahSelect_address);
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        public void getDataTanah(string strwhere = null)
        {

            this.Enabled = false;
            //myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            //myThread.Start();
            SvcAssetTanahSelect.InputParameters parInp = new SvcAssetTanahSelect.InputParameters();

            string where = "";
            if (FROM_TRACKING == false)
            {
                where = BySatker;
                if (strwhere != null)
                {
                    where = where + " AND " + strwhere;
                }
                else
                {
                    where = where;
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
                konfigApp.currentMaks = konfigApp.dataAkhir;
                konfigApp.currentMin = konfigApp.dataAwal;
            }
            else
            {
                konfigApp.currentMin = konfigApp.currentMaks + 1;
                konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataAkhir;
                dataInisial = false;
            }
            if (this.dataInisial == true)
            {
                this.search = " STATUS_BMN_YN = 'Y' AND BPYBDS_YN = 'N' " + ((where.Length > 1) ? " AND " + where : "");
            }
            parInp.P_MAX = konfigApp.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = konfigApp.currentMin;
            parInp.P_MINSpecified = true;
            parInp.P_COL = "KD_SATKER";
            parInp.P_SORT = ",KD_BRG,NO_ASET DESC";
            parInp.STR_WHERE = this.search;
            fetchData = new SvcAssetTanahSelect.call_pttClient();
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
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch (Exception E)
            {
                //if (!FROM_TRACKING)

                this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
               //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
               MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcAssetTanahSelect.OutputParameters dataOut);

        ucTanahForm TanahForm;


        public void showData(SvcAssetTanahSelect.OutputParameters serviceOutPut)
        {
            if (!FROM_TRACKING)
            {
                int jmlData = serviceOutPut.SF_ROW_ASET_M_KTNH.Count();

                if (this.dataInisial == true)
                {
                    this.binder.Clear();
                    if (jmlData > 0)
                    {
                        StrTotalGrid.Caption = jmlData.ToString();
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_ASET_M_KTNH[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_ASET_M_KTNH[0].TOTAL_DATA.ToString();
                    }
                }
                if (this.dataInisial == true)
                {
                    this.SUM_RPH_ASET = serviceOutPut.SF_ROW_ASET_M_KTNH[0].SUM_RPH_ASET;
                    this.SUM_RPH_MUTASI = serviceOutPut.SF_ROW_ASET_M_KTNH[0].SUM_RPH_MUTASI;
                    this.SUM_RPH_SUSUT = serviceOutPut.SF_ROW_ASET_M_KTNH[0].SUM_RPH_SUSUT;
                   //his.SUM_NILAI_SEBELUM_SUSUT = serviceOutPut.SF_ROW_ASET_M_KTNH[0].SUM_NILAI_SBLM_SUSUT;
                }
                for (int i = 0; i < jmlData; i++)
                {
                  string date1 = Convert.ToString(serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_BERLAKU).Substring(0,10);
                  string date2 = Convert.ToString(serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_BUKU).Substring(0, 10);
                  string date3 = Convert.ToString(serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_DANA).Substring(0, 10);
                  string date4 = Convert.ToString(serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_DOK).Substring(0, 10);
                  string date5 = Convert.ToString(serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_PERLH).Substring(0, 10);
                  string date6 = Convert.ToString(serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_REKAM).Substring(0, 10);
                  if (date1 == "11/11/1000")
                    {
                        serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_BERLAKU = null;
                    }
                   if (date2 == "11/11/1000")
                    {
                        serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_BUKU = null;
                    }
                   if (date3 == "11/11/1000")
                    {
                        serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_DANA = null;
                    }
                   if (date4 == "11/11/1000")
                    {
                        serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_DOK = null;
                    }
                   if (date5 == "11/11/1000")
                    {
                        serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_PERLH = null;
                    }
                   if (date6 == "11/11/1000")
                    {
                        serviceOutPut.SF_ROW_ASET_M_KTNH[i].TGL_REKAM = null;
                    }

                   //serviceOutPut.SF_ROW_ASET_M_KTNH[i].SUM_NILAI_SBLM_SUSUT = this.SUM_NILAI_SBLM_SUSUT;
                   serviceOutPut.SF_ROW_ASET_M_KTNH[i].SUM_RPH_SUSUT = this.SUM_RPH_ASET;
                   serviceOutPut.SF_ROW_ASET_M_KTNH[i].SUM_RPH_MUTASI = this.SUM_RPH_MUTASI;
                   serviceOutPut.SF_ROW_ASET_M_KTNH[i].SUM_RPH_ASET = this.SUM_RPH_ASET;    

                    this.binder.Add(serviceOutPut.SF_ROW_ASET_M_KTNH[i]);
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
                int jmlData = serviceOutPut.SF_ROW_ASET_M_KTNH.Count();

              
                if (jmlData > 0)
                {
                    TanahForm = new ucTanahForm("detail");

                    TanahForm.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
                    TanahForm.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);

                    TanahForm.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
                    TanahForm.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
                    TanahForm.Dock = System.Windows.Forms.DockStyle.Fill;

                    TanahForm.IsiForm(serviceOutPut.SF_ROW_ASET_M_KTNH[0]);
                }
                else
                {
                    MessageBox.Show("Data aset tidak ditemukan", "Perhatian");
                }
                

                setPanel(TanahForm);
               
                //this.targetPanel.panelKoorSatker.Controls.Clear();
                //this.targetPanel.panelKoorSatker.Controls.Add(TanahForm);
            }

            this.gvUcDtl.BestFitColumns();
        }

        public void ShowProgresBarKosong()
        {
           
        }
        
    }
}