using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKW.MWD.PSPBMN.DETAIL1
{
    public partial class ucRenovasi : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcMonRenovSelect.execute_pttClient svcRenovasiSelect = null;
        SvcMonRenovSelect.OutputParameters outRenovasiSelect = null;
        SvcMonRenovSelect.WASDALSROW_MON_BMN_PSP dataTerpilih = null;

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
        FrmKoorSatker frmKpknl = null;

        public ucRenovasi(FrmKoorSatker _frmKpknl, ucPSPBMN _ucPspBmn)
        {
            InitializeComponent();
            this.frmKpknl = _frmKpknl;
            PSPBMN = _ucPspBmn;
        }

        #region LOAD DATA//================================

        public void getRenovasi()
        {
            frmKpknl.Enabled = false;
            try
            {
                frmKpknl.fToggleProgressBar("start");

                SvcMonRenovSelect.InputParameters parInp = new SvcMonRenovSelect.InputParameters();

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
                parInp.STR_WHERE = String.Format("(UPPER({0}) LIKE '%{1}%') ", "ID_SATKER", konfigApp.idSatker) + "AND (NO_SURAT <> '-')" + PSPBMN.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcRenovasiSelect = new SvcMonRenovSelect.execute_pttClient();
                svcRenovasiSelect.Beginexecute(parInp, new AsyncCallback(getDataRenovasi), null);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataRenovasi(IAsyncResult result)
        {
            try
            {
                outRenovasiSelect = svcRenovasiSelect.Endexecute(result);
                svcRenovasiSelect.Close();
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                this.Invoke(new LoadDataRenovasi(this.loadDataRenovasi), outRenovasiSelect);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataRenovasi(SvcMonRenovSelect.OutputParameters dataOut);

        private void loadDataRenovasi(SvcMonRenovSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_KRNV_PSP.Count();
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
                dataOut.SF_MON_KRNV_PSP[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_MON_KRNV_PSP[i].TGL_SK);
                
                dsGrid.Add(  dataOut.SF_MON_KRNV_PSP[i]);
                this.adaData = true;
            }
            displayData();
   
        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcRenovasi.DataSource = null;
                gcRenovasi.DataSource = dsGrid;
            }
            else
            {
                gcRenovasi.RefreshDataSource();
            }
        }

        private void gvRenovasi_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonRenovSelect.WASDALSROW_MON_BMN_PSP)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvRenovasi.GetFocusedDataSourceRowIndex();

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

        private void gvRenovasi_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //frmTL frmTL = new frmTL(PSPBMN);
                //frmTL.idAset = dataTerpilih.ID_ASET.ToString();
                //frmTL.skKeputusan = dataTerpilih.NO_SURAT;
                //frmTL.teKdBrg.Text = dataTerpilih.KD_BRG;
                //frmTL.teNoAset.Text = dataTerpilih.NUP.ToString();
                //frmTL.teUrBrg.Text = dataTerpilih.UR_SSKEL;
                //frmTL.teKuantitas.EditValue = dataTerpilih.KUANTITAS;
                //frmTL.teNilaiPersetujuan.EditValue = dataTerpilih.NILAI_PENETAPAN;
                //frmTL.kdStatus = (dataTerpilih.KD_STATUS == "") ? "02" : dataTerpilih.KD_STATUS;
                //frmTL.gunaWasdal = dataTerpilih.GUNA_WASDAL;
                //frmTL.ShowDialog();
            }
            catch
            {
            }
        }




    }
}
