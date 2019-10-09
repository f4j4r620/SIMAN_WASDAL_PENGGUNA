using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKW.MWD.PSPBMN.DETAIL2
{

    public partial class ucMesinNonTik : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcMonMesinNonTikSelect.execute_pttClient svcMesinNonTikSelect = null;
        SvcMonMesinNonTikSelect.OutputParameters outMesinNonTikSelect = null;
        SvcMonMesinNonTikSelect.WASDALSROW_MON_BMN_PSP2 dataTerpilih = null;

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
        public string kdPelayanan = "";
        public decimal currentMin = konfigApp.currentMin;
        public decimal currentMaks = konfigApp.currentMaks;

        ucPSPBMN PSPBMN = null;
        FrmKoorKorwil frmKpknl = null;

        public ucMesinNonTik(FrmKoorKorwil _frmKpknl, ucPSPBMN _ucPspBmn)
        {
            InitializeComponent();
            this.frmKpknl = _frmKpknl;
            PSPBMN = _ucPspBmn;
        }

        #region LOAD DATA //================================

        public void getMesinNonTik()
        {
            frmKpknl.Enabled = false;
            try
            {
                frmKpknl.fToggleProgressBar("start");

                SvcMonMesinNonTikSelect.InputParameters parInp = new SvcMonMesinNonTikSelect.InputParameters();

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
                parInp.STR_WHERE = String.Format("ID_KORWIL = {0}", konfigApp.idKorwil) + " AND (NO_SURAT is null)" + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcMesinNonTikSelect = new SvcMonMesinNonTikSelect.execute_pttClient();
                svcMesinNonTikSelect.Beginexecute(parInp, new AsyncCallback(getDataMesinNonTik), null);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataMesinNonTik(IAsyncResult result)
        {
            try
            {
                outMesinNonTikSelect = svcMesinNonTikSelect.Endexecute(result);
                svcMesinNonTikSelect.Close();
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                this.Invoke(new LoadDataMesinNonTik(this.loadDataMesinNonTik), outMesinNonTikSelect);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataMesinNonTik(SvcMonMesinNonTikSelect.OutputParameters dataOut);

        private void loadDataMesinNonTik(SvcMonMesinNonTikSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_KALB_PSP2.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_KALB_PSP2[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmKpknl.bbiMWasdalMore.Enabled = true;
                PSPBMN.sbCari.Enabled = true;
                jmlCurrent = konfigApp.dataAkhir;
            }
            else
            {
                if (jmlData > konfigApp.dataAkhir)
                    jmlCurrent = konfigApp.dataAkhir;
                else
                    jmlCurrent = jmlData;
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
                //dataOut.SF_MON_KALB_PSP2[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_MON_KALB_PSP2[i].TGL_SK);
               
                dsGrid.Add(  dataOut.SF_MON_KALB_PSP2[i]);
                this.adaData = true;
            }
            labelTotData.Text = "";
            labelTotData.Text = "Menampilkan " + jmlCurrent + " dari total " + totalData + " data";
            displayData();
          
        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcMesinNonTik.DataSource = null;
                gcMesinNonTik.DataSource = dsGrid;
            }
            else
            {
                gcMesinNonTik.RefreshDataSource();
            }
            gvMesinNonTik.BestFitColumns();
        }

        private void gvMesinNonTik_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonMesinNonTikSelect.WASDALSROW_MON_BMN_PSP2)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvMesinNonTik.GetFocusedDataSourceRowIndex();

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

        private void gvMesinNonTik_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmTL frmTL = new frmTL(PSPBMN);
                frmTL.idAset = dataTerpilih.ID_ASET.ToString();
                frmTL.skKeputusan = dataTerpilih.NO_SURAT;
                frmTL.teKdBrg.Text = dataTerpilih.KD_BRG;
                frmTL.teNoAset.Text = dataTerpilih.NUP.ToString();
                frmTL.teUrBrg.Text = dataTerpilih.UR_SSKEL;
                frmTL.teKuantitas.EditValue = dataTerpilih.KUANTITAS;
                //frmTL.teNilaiPersetujuan.EditValue = dataTerpilih.NILAI_PENETAPAN;
                //frmTL.kdStatus = (dataTerpilih.KD_STATUS == "") ? "02" : dataTerpilih.KD_STATUS;
                //frmTL.gunaWasdal = dataTerpilih.GUNA_WASDAL;
                frmTL.ShowDialog();
            }
            catch
            {
            }
        }




    }
}
