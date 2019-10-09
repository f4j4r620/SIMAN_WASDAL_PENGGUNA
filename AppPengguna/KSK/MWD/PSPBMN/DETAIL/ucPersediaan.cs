using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengelola.KKPKNL.MWD.PSPBMN.DETAIL1
{
    public partial class ucPersediaan : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcPersediaanRwytPengguna.call_pttClient svcPersediaanSelect = null;
        SvcPersediaanRwytPengguna.OutputParameters outPersediaanSelect = null;
        SvcPersediaanRwytPengguna.BPSIMANSROW_SEDIA_RWYT_PENGGUNA dataTerpilih = null;

        public int posisiRow = 0;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ArrayList dsGrid = null;
        public bool rowTerakhir = false;
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        public bool initModeLoad = true;
        public string strCari= "";
        public bool masihAdaData;
        public bool adaData;
      
        public decimal currentMin = konfigApp.currentMin;
        public decimal currentMaks = konfigApp.currentMaks;

        ucPSPBMN PSPBMN = null;
        FrmKoorKpknl frmKpknl = null;

        public ucPersediaan(FrmKoorKpknl _frmKpknl, ucPSPBMN _ucPspBmn)
        {
            InitializeComponent();
            this.frmKpknl = _frmKpknl;
            PSPBMN = _ucPspBmn;
        }

        #region LOAD DATA //================================

        public void getPersediaan()
        {
            frmKpknl.Enabled = false;
            try
            {
                frmKpknl.fToggleProgressBar("start");

                SvcPersediaanRwytPengguna.InputParameters parInp = new SvcPersediaanRwytPengguna.InputParameters();

                if (dataInisial == true)
                {
                    this.currentMaks = konfigApp.dataAkhir;
                    this.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    this.currentMin = this.currentMaks + 1;
                    this.currentMaks = this.currentMaks + konfigApp.dataAkhir;
                }

                parInp.P_MAX = this.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = this.currentMin;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = this.strCari;
                svcPersediaanSelect = new SvcPersediaanRwytPengguna.call_pttClient();
                svcPersediaanSelect.Beginexecute(parInp, new AsyncCallback(getDataPersediaan), null);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataPersediaan(IAsyncResult result)
        {
            try
            {
                outPersediaanSelect = svcPersediaanSelect.Endexecute(result);
                svcPersediaanSelect.Close();
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                this.Invoke(new LoadDataPersediaan(this.loadDataPersediaan), outPersediaanSelect);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataPersediaan(SvcPersediaanRwytPengguna.OutputParameters dataOut);

        private void loadDataPersediaan(SvcPersediaanRwytPengguna.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_SEDIA_RWYT_PENGGUNA.Count();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmKpknl.bbiMWasdalMore.Enabled = true;
                PSPBMN.sbCari.Enabled = true;
            }
            else
            {
                if (this.modeLoadData == "normal")
                {
                    this.masihAdaData = false;
                    frmKpknl.bbiMWasdalMore.Enabled = false;
                    PSPBMN.sbCari.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                    PSPBMN.sbCari.Enabled = false;
                }
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_ROW_SEDIA_RWYT_PENGGUNA[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_ROW_SEDIA_RWYT_PENGGUNA[i].TGL_SK);
                //SvcSskel.call_pttClient svcSskel = new SvcSskel.call_pttClient();
               
                dsGrid.Add(  dataOut.SF_ROW_SEDIA_RWYT_PENGGUNA[i]);
                this.adaData = true;
            }
            displayData();
            if (this.modeLoadData == "cari")
            {
                string xSatu = PSPBMN.cbNamaKolom1.Text.Trim();
                string xDua = PSPBMN.teCari1.Text.Trim();
                string xTiga = PSPBMN.fieldDicari;
                gvPersediaan.ClearColumnsFilter();
                PSPBMN.cbNamaKolom1.Text = xSatu;
                PSPBMN.teCari1.Text = xDua;
                PSPBMN.fieldDicari = xTiga;
            }
        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcPersediaan.DataSource = null;
                gcPersediaan.DataSource = dsGrid;
            }
            else
            {
                gcPersediaan.RefreshDataSource();
            }
        }

        private void gvPersediaan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcPersediaanRwytPengguna.BPSIMANSROW_SEDIA_RWYT_PENGGUNA)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvPersediaan.GetFocusedDataSourceRowIndex();

                    if (rowTerpilih.IsLastRow)
                    {
                        rowTerakhir = true;
                    }
                    else
                    {
                        rowTerakhir = false;
                    }
                }
            }
            catch
            {
            }
        }




    }
}
