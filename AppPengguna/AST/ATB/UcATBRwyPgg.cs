using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using AppPengguna.AST.PUASET;

namespace AppPengguna.AST.ATB
{
    class UcATBRwyPgg : UserControlDetail
    {
         private SvcATBPggSelect.call_pttClient fetchData;
         private SvcATBPggSelect.InputParameters parInp;
         private SvcATBPggSelect.OutputParameters outDat;
         private SvcATBPggSelect.BPSIMANSROW_KTWJD_RWYT_PENGGUNA selectedData;


       //private GridView selectedView;
        
        private UcATBForm frmMaster;
        private frmPengelola puPengelola;
        private bool isSearch = false;

        public SvcATBPggSelect.BPSIMANSROW_KTWJD_RWYT_PENGGUNA SelectedData
        {
            get
            {
                return selectedData;
            }
        }

        public UcATBForm FrmMaster
        {
            get
            {
                return frmMaster;
            }
        }


        public string Status;
        public decimal? ID_KTWJD;
        public UcATBRwyPgg(decimal? _ID_KTWJD, string _status)
            : base()
        {
            this.ID_KTWJD = _ID_KTWJD;
            this.Status = _status;

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
        }

        protected override void ucDetail_Load(object sender, EventArgs e)
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcATBPggSelect.BPSIMANSROW_KTWJD_RWYT_PENGGUNA);
            this.gcUcDtl.DataSource = binder;
            this.setKolom("No", "NUM", "NUM", 0, false);
            this.setKolom("Status Pengelolaan BMN", "STATUS_KELOLA", "STATUS_KELOLA", 1, true, 10);
            this.setKolom("No SK Penetapan", "NO_SK_PENETAPAN", "NO_SK_PENETAPAN", 2, true, 150);
            this.setKolom("Tgl. SK Penetapan", "TGL_SK", "TGL_SK", 3, true, 150, "date");
            this.setKolom("Nilai Penetapan", "NILAI", "NILAI", 4, true, 150, "number");
            this.setKolom("No Kontrak", "NO_KONTRAK", "NO_KONTRAK", 5, true, 150);
            this.setKolom("Tgl. Kontrak", "TGL_KONTRAK", "TGL_KONTRAK", 6, true, 150, "date");
            this.setKolom("Jangka Waktu", "JNK_WAKTU", "JNK_WAKTU", 7, true, 150);
            this.setKolom("Tgl. Mulai ", "TGL_MULAI", "TGL_MULAI", 8, true, 150, "date");
            this.setKolom("Tgl. Selesai", "TGL_SELESAI", "TGL_SELESAI", 9, true, 150, "date");
            this.setKolom("Pihak  Pengguna", "JNS_PMK", "JNS_PMK", 10, true, 150);
            this.setKolom("Unit Pengguna", "KD_SATKER", "KD_SATKER", 11, true, 150);
            this.setKolom("Uraian Unit Pengguna", "UR_SATKER", "UR_SATKER", 12, true, 150);
            this.setKolom("Keterangan", "KET", "KET", 13, true, 150);
            this.setKolom("File (BA Kontrak)", "NMFILE", "NMFILE", 14, true, 150);
            this.setKolom("Terakhir (Y/T)", "TERAKHIR_YN", "TERAKHIR_YN", 15, false, 150, "string", true);

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
                parInp.P_ID = selectedData.ID_KTWJD_RWYT_PENGGUNA;
                parInp.P_IDSpecified = true;
                parInp.P_ID_TABLE = "ID_KTWJD_RWYT_PENGGUNA";
                parInp.P_TABLE = "M_KTWJD_RWYT_PENGGUNA";

                svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient(konfigApp.SvcAsetGetDokSelect_name, konfigApp.SvcAsetGetDokSelect_address);
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

        private bool moreCari = false;
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
                this.getInitMutLny(get_where_clause(nama_kolom, opr, parameter, parameter_2));


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
                case "Status Pengelolaan BMN":
                    where = "Upper(STATUS_KELOLA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "No. SK Penetapan":
                    where = "Upper(NO_SK_PENETAPAN) " + get_operator("String", opr, parameter, parameter2);
                    break;

                case "Tgl. SK Penetapan":
                    where = "Upper(TGL_SK) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Luas Penetapan":
                    where = "Upper(LUAS) " + get_operator("Integer", opr, parameter, parameter2);
                    break;
                case "Nilai Penetapan":
                    where = "Upper(NILAI) " + get_operator("Number", opr, parameter, parameter2);
                    break;
                case "No. Kontrak":
                    where = "Upper(NO_KONTRAK) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Tgl. Kontrak":
                    where = "Upper(TGL_KONTRAK) " + get_operator("date", opr, parameter, parameter2);
                    break;
                case "Jangka Waktu":
                    where = "Upper(NO_DSR_MUTASI) " + get_operator("Integer", opr, parameter, parameter2);
                    break;
                case "Tgl. Mulai":
                    where = "Upper(TGL_MULAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Tgl. Selesai":
                    where = "Upper(TGL_SELESAI) " + get_operator("Date", opr, parameter, parameter2);
                    break;
                case "Pihak Pengguna":
                    where = "Upper(PHK_PENGGUNA) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Unit Pengguna":
                    where = "Upper(KD_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Uraian Unit Pengguna":
                    where = "Upper(UR_SATKER) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "Keterangan":
                    where = "Upper(KET) " + get_operator("String", opr, parameter, parameter2);
                    break;
                case "File (BA Kontrak)":
                    where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
                    break;
                default:
                    break;
            }
            return where;
        }

        public void getInitMutLny(string _where = null)
        {
            decimal Max, Min;
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            this.nonAktifForm("");
            SvcATBPggSelect.InputParameters parInp = new SvcATBPggSelect.InputParameters();
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
            parInp.STR_WHERE = String.Format("ID_KTWJD = {0} {1}", this.ID_KTWJD, _where);
            parInp.P_SORT = "DESC";
            fetchData = new SvcATBPggSelect.call_pttClient();
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
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch
            {
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
              
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void ShowData(SvcATBPggSelect.OutputParameters dataOut);
        private string StatusCrud = "";
        private decimal? NUM;
        public void showData(SvcATBPggSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KTWJD_RWYT_PENGGUNA.Count();

            if (this.getById == true && jmlDataGroup > 0)
            {
                if (this.StatusCrud == "edit")
                {
                    serviceOutPut.SF_ROW_KTWJD_RWYT_PENGGUNA[0].NUM = this.NUM;
                }
                else
                {
                    serviceOutPut.SF_ROW_KTWJD_RWYT_PENGGUNA[0].NUM = binder.Count + 1;
                }
            }
            if (this.dataInisial == true)
            {
                this.binder.Clear();
                if (jmlDataGroup > 0)
                {
                    StrTotalGrid.Caption = jmlDataGroup.ToString();
                    StrTotalDb.Caption = serviceOutPut.SF_ROW_KTWJD_RWYT_PENGGUNA[0].TOTAL_DATA.ToString();
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
                        StrTotalDb.Caption = serviceOutPut.SF_ROW_KTWJD_RWYT_PENGGUNA[0].TOTAL_DATA.ToString();
                    }
                    else
                    {
                        StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
                    }
                }
            }


            for (int i = 0; i < jmlDataGroup; i++)
            {
                binder.Add(serviceOutPut.SF_ROW_KTWJD_RWYT_PENGGUNA[i]);
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

        protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            puPengelola = new frmPengelola("input");
            StatusCrud = "input";
            this.puPengelola.resetForm = new ResetFrmPengelola(this.resetForm);
            this.puPengelola.saveForm = new SaveFrmPengelola(this.saveForm);
            this.puPengelola.LayoutLuasPenetapan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            puPengelola.Enabled = true;

            puPengelola.ShowDialog();
        }

        protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            if (selectedData != null)
            {
                this.NUM = selectedData.NUM;
                if (this.Status == "detail")
                {
                    puPengelola = new frmPengelola("detail");
                    this.StatusCrud = "detail";
                }
                else
                {
                    puPengelola = new frmPengelola("edit");
                    this.StatusCrud = "edit";
                }
                this.puPengelola.resetForm = new ResetFrmPengelola(this.resetForm);
                this.puPengelola.saveForm = new SaveFrmPengelola(this.saveForm);
                this.puPengelola.LayoutLuasPenetapan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.resetForm("reset");
                puPengelola.Enabled = true;

                puPengelola.ShowDialog();
            }
        }

        private void resetForm(string text)
        {
            this.puPengelola.teKDPengelola.Text = selectedData.KD_SATKER;
            this.puPengelola.teKeterangan.Text = selectedData.KET;


            this.puPengelola.teNilaiPenetapan.Value = (decimal)selectedData.NILAI;
            this.puPengelola.teNoKontrak.Text = selectedData.NO_KONTRAK;
            this.puPengelola.tePihakPengelola.Text = selectedData.PHK_PENGGUNA;
            this.puPengelola.teSKPenetapan.Text = selectedData.NO_SK_PENETAPAN;
            this.puPengelola.teJenisPemakai.Text = selectedData.JNS_PMK;
            this.puPengelola.teStatusPegelolaan.Text = selectedData.STATUS_KELOLA;
            this.puPengelola.teTglKontrak.Text = konfigApp.DateToString(selectedData.TGL_KONTRAK);
            this.puPengelola.teJangkaWaktu.Value = (decimal)selectedData.JNK_WAKTU;
            this.puPengelola.teTglMulai.Text = konfigApp.DateToString(selectedData.TGL_MULAI);
            this.puPengelola.teTglSelesai.Text = konfigApp.DateToString(selectedData.TGL_SELESAI);
            this.puPengelola.teTglSKPenetapan.Text = konfigApp.DateToString(selectedData.TGL_SK);
            this.puPengelola.teUr_Pengelola.Text = selectedData.UR_SATKER;
            this.puPengelola.ID_SATKER = selectedData.ID_SATKER;
            this.puPengelola.KD_PELAYANAN = selectedData.KD_PELAYANAN;


        }

        SvcATBRpengelolaCrud.call_pttClient svcRiwayatPengelolaCrud;
        SvcATBRpengelolaCrud.OutputParameters outRiwayatPengelolaCrud;

        private void saveForm(string text)
        {
            this.puPengelola.nonAktifkanForm("nonaktif");
            try
            {
                if (this.modeCrud != 'D')
                {
                    myThread = new Thread(new ThreadStart(this.puPengelola.ShowProgresBar));
                    myThread.Start();
                    this.puPengelola.nonAktifkanForm("");
                }
                else
                {
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    this.nonAktifForm("");
                }


                SvcATBRpengelolaCrud.InputParameters parInp = new SvcATBRpengelolaCrud.InputParameters();
                parInp.P_ID_KTWJD = this.ID_KTWJD;
                parInp.P_ID_KTWJD_RWYT_PENGGUNA = (this.puPengelola.Status == "input") ? 0 : this.selectedData.ID_KTWJD_RWYT_PENGGUNA;
                parInp.P_ID_KTWJD_RWYT_PENGGUNASpecified = true;
                parInp.P_ID_KTWJDSpecified = true;
                parInp.P_ID_MUTASI_DTL = null;
                parInp.P_ID_MUTASI_DTLSpecified = true;
                parInp.P_ID_SATKER = this.puPengelola.ID_SATKER;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_JNK_WAKTU = (float?)this.puPengelola.teJangkaWaktu.Value;
                parInp.P_JNK_WAKTUSpecified = true;
                parInp.P_JNS_PMK = this.puPengelola.tePihakPengelola.Text;
                parInp.P_KD_PELAYANAN = this.puPengelola.KD_PELAYANAN;

                parInp.P_KET = this.puPengelola.teKeterangan.Text.ToString();

                parInp.P_NILAI = (decimal?)this.puPengelola.teNilaiPenetapan.Value;
                parInp.P_NILAISpecified = true;
                parInp.P_NO_KONTRAK = this.puPengelola.teNoKontrak.Text;
                parInp.P_NO_SK_PENETAPAN = this.puPengelola.teSKPenetapan.Text;
                parInp.P_PHK_PENGGUNA = this.puPengelola.tePihakPengelola.Text;
                parInp.P_TGL_KONTRAK = konfigApp.DateToDb(this.puPengelola.teTglKontrak.Text);
                parInp.P_TGL_MULAI = konfigApp.DateToDb(this.puPengelola.teTglMulai.Text);
                parInp.P_TGL_SELESAI = konfigApp.DateToDb(this.puPengelola.teTglSelesai.Text);

                if (this.puPengelola.FilePath != null)
                {
                    parInp.P_NMFILE = this.puPengelola.teFileName.Text;
                }
                else
                {
                    parInp.P_NMFILE = (this.puPengelola.Status == "input") ? null : selectedData.NMFILE;
                }

                if (this.puPengelola.Status == "input")
                {
                    parInp.P_SELECT = "C";
                }
                else
                {
                    parInp.P_SELECT = "U";
                }


                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                this.svcRiwayatPengelolaCrud = new SvcATBRpengelolaCrud.call_pttClient();
                svcRiwayatPengelolaCrud.Open();
                this.svcRiwayatPengelolaCrud.Beginexecute(parInp, new AsyncCallback(crudRiwayatPengelola), "");
            }
            catch (Exception e)
            {
                this.modeCrud = 'A';
                this.Invoke(new AktifkanForm(this.puPengelola.aktifkanForm), BarItemVisibility.Never);
                this.Invoke(new ProgBar(this.puPengelola.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        #region crud
        public void crudRiwayatPengelola(IAsyncResult result)
        {
            try
            {
                outRiwayatPengelolaCrud = svcRiwayatPengelolaCrud.Endexecute(result);
                svcRiwayatPengelolaCrud.Close();
                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.puPengelola.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPengelola.progBar), BarItemVisibility.Never);
                }
                if (String.Compare(outRiwayatPengelolaCrud.PO_RESULT.Trim(), "Y", true) == 0)
                {
                    this.Invoke(new UbahDsDetail(this.ubahDsDetail), outRiwayatPengelolaCrud);
                }
                else
                {
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
            catch
            {

                if (this.modeCrud == 'D')
                {
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                }
                else
                {
                    this.Invoke(new AktifkanForm(this.puPengelola.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPengelola.progBar), BarItemVisibility.Never);
                }

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

        public delegate void UbahDsDetail(SvcATBRpengelolaCrud.OutputParameters outCrud);
        private decimal? ID_KTWJD_RWYT_PENGGUNA;
        public void ubahDsDetail(SvcATBRpengelolaCrud.OutputParameters outCrud)
        {
            SvcATBPggSelect.BPSIMANSROW_KTWJD_RWYT_PENGGUNA dataPenyama = new SvcATBPggSelect.BPSIMANSROW_KTWJD_RWYT_PENGGUNA();

            dataPenyama.NUM = 999;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_ASET = (this.modeCrud == 'U') ? selectedData.ID_ASET : 0;
            dataPenyama.ID_ASETSpecified = true;
            dataPenyama.ID_KTWJD = (this.modeCrud == 'U') ? selectedData.ID_KTWJD : 0;
            dataPenyama.ID_KTWJD_RWYT_PENGGUNA = (this.modeCrud == 'U') ? selectedData.ID_KTWJD_RWYT_PENGGUNA : outCrud.PO_ID_KTWJD_RWYT_PENGGUNA;
            dataPenyama.ID_KTWJD_RWYT_PENGGUNASpecified = true;
            dataPenyama.ID_KTWJDSpecified = true;
            dataPenyama.ID_MUTASI_DTL = (this.modeCrud == 'U') ? selectedData.ID_MUTASI_DTL : 0;
            dataPenyama.ID_MUTASI_DTLSpecified = true;
            dataPenyama.KD_BRG = (this.modeCrud == 'U') ? selectedData.KD_BRG : "";

            this.ID_KTWJD_RWYT_PENGGUNA = (this.StatusCrud == "input") ? outCrud.PO_ID_KTWJD_RWYT_PENGGUNA : selectedData.ID_KTWJD_RWYT_PENGGUNA;


            switch (this.modeCrud)
            {
                case 'C':


                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.puPengelola.FilePath != null)
                    {
                        string filePath = this.puPengelola.FilePath;
                        simpanFile("ID_KTWJD_RWYT_PENGGUNA", dataPenyama.ID_KTWJD_RWYT_PENGGUNA, "M_KTWJD_RWYT_PENGGUNA", filePath, "C");
                    }
                    else
                    {
                        this.puPengelola.Close();
                        this.dataInisial = false;
                        this.getById = true;
                        this.getInitMutLny(" ID_M_KTNH_RWYT_PENGGUNA = " + this.ID_KTWJD_RWYT_PENGGUNA.ToString());
                    }
                    //this.binder.Add(dataPenyama);
                    break;
                case 'U':

                    this.binder.Remove(this.selectedData);
                    AutoClosingMessageBox.Show(konfigApp.teksBerhasilSimpan, konfigApp.judulGagalSimpan, 1000);
                    if (this.puPengelola.FilePath != null)
                    {

                        string filePath = this.puPengelola.FilePath;
                        string SELECT = "C";
                        if (selectedData.NMFILE != "-")
                        {
                            SELECT = "U";
                        }
                        simpanFile("ID_KTWJD_RWYT_PENGGUNA", dataPenyama.ID_KTWJD_RWYT_PENGGUNA, "M_KTWJD_RWYT_PENGGUNA", filePath, SELECT);
                    }
                    else
                    {
                        this.puPengelola.Close();
                        this.dataInisial = false;
                        this.getById = true;
                        this.getInitMutLny(" ID_M_KTNH_RWYT_PENGGUNA = " + this.ID_KTWJD_RWYT_PENGGUNA.ToString());
                    }
                   
                    break;
                case 'D':
                    this.binder.Remove(this.selectedData);
                     gvUcDtl.RefreshData();
                    this.StrTotalGrid.Caption = (Convert.ToInt64(this.StrTotalGrid.Caption) - 1).ToString();
                    this.StrTotalDb.Caption = (Convert.ToInt64(this.StrTotalDb.Caption) - 1).ToString();
                    break;
            }
        }

        SvcAsetDokCrud.call_pttClient svcDokCrud;
        SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
        public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string filePath, string SELECT)
        {
            myThread = new Thread(new ThreadStart(this.puPengelola.ShowProgresBar));
            myThread.Start();
            try
            {
                SvcAsetDokCrud.InputParameters inputData = new SvcAsetDokCrud.InputParameters();
                inputData.P_ID_DOK = 1;
                inputData.P_ID_DOKSpecified = true;
                inputData.P_ID_TYPE = ID_TYPE;
                inputData.P_ID_VALUE = ID_VALUE;
                inputData.P_ID_VALUESpecified = true;
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
                    this.Invoke(new AktifkanForm(this.puPengelola.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPengelola.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new AktifkanForm(this.puPengelola.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPengelola.progBar), BarItemVisibility.Never);
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
                    this.Invoke(new AktifkanForm(this.puPengelola.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.puPengelola.progBar), BarItemVisibility.Never);
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
                    this.puPengelola.Close();
                }
            }

        }
        #endregion


        protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            initGrid();
            moreCari = false;
            getInitMutLny();
        }

        protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            this.dataInisial = false;
            getInitMutLny();
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
                    this.StatusCrud = "bbHapus";
                    this.nonAktifForm("");
                    myThread = new Thread(new ThreadStart(this.ShowProgresBar));
                    myThread.Start();
                    SvcATBRpengelolaCrud.InputParameters parInp = new SvcATBRpengelolaCrud.InputParameters();
                    parInp.P_ID_KTWJD = selectedData.ID_KTWJD;
                    parInp.P_ID_KTWJDSpecified = true;
                    parInp.P_ID_KTWJD_RWYT_PENGGUNA = selectedData.ID_KTWJD_RWYT_PENGGUNA;
                    parInp.P_ID_KTWJD_RWYT_PENGGUNASpecified = true;


                    parInp.P_SELECT = "D";
                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                    svcRiwayatPengelolaCrud = new SvcATBRpengelolaCrud.call_pttClient();
                    svcRiwayatPengelolaCrud.Open();
                    svcRiwayatPengelolaCrud.Beginexecute(parInp, new AsyncCallback(crudRiwayatPengelola), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                    this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
                }
            }
        }

 

        protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e) { }

        protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) 
        {
            selectedView = sender as GridView;
            if (selectedView.SelectedRowsCount > 0)
            {
                selectedData = (SvcATBPggSelect.BPSIMANSROW_KTWJD_RWYT_PENGGUNA)selectedView.GetRow(e.FocusedRowHandle);
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
                case "Tgl. SK Penetapan":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Tgl. Kontrak":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Tgl. Mulai":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                case "Tgl. Selesai":
                    this.teSearch.Edit = this.ItemDateTime;
                    this.teSearch2.Edit = this.ItemDateTime;
                    break;
                default:
                    this.teSearch.Edit = this.ItemText;
                    this.teSearch2.Edit = this.ItemText;
                    break;
            }
        }
    }
}
