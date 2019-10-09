using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KES1.MWD.PSPBMN.DETAIL
{
    public partial class ucBangunan : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcMonBgnSelect.execute_pttClient svcBangunanSelect = null;
        SvcMonBgnSelect.OutputParameters outBangunanSelect = null;
        SvcMonBgnSelect.WASDALSROW_MON_BMN_PSP2 dataTerpilih = null;

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

        ucMonLAIN PSPBMN = null;
        FrmKoorKL frmKpknl = null;

        public ucBangunan(FrmKoorKL _frmKoorKL, ucMonLAIN _ucPspBmn)
        {
            InitializeComponent();
            this.frmKpknl = _frmKoorKL;
            PSPBMN = _ucPspBmn;
        }

        #region LOAD DATA 

        public void getBangunan()
        {
            frmKpknl.Enabled = false;
            try
            {
                frmKpknl.fToggleProgressBar("start");

                SvcMonBgnSelect.InputParameters parInp = new SvcMonBgnSelect.InputParameters();

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
                parInp.STR_WHERE = " (NO_SURAT != '-')" + PSPBMN.strKdPelayanan + this.strCari; 
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcBangunanSelect = new SvcMonBgnSelect.execute_pttClient();
                svcBangunanSelect.Beginexecute(parInp, new AsyncCallback(getDataBangunan), null);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataBangunan(IAsyncResult result)
        {
            try
            {
                outBangunanSelect = svcBangunanSelect.Endexecute(result);
                svcBangunanSelect.Close();
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                this.Invoke(new LoadDataBangunan(this.loadDataBangunan), outBangunanSelect);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataBangunan(SvcMonBgnSelect.OutputParameters dataOut);

        private void loadDataBangunan(SvcMonBgnSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_KBDG_PSP2.Count();
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
                //dataOut.SF_MON_KBDG_PSP2[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_MON_KBDG_PSP2[i].TGL_SK);
                dsGrid.Add(  dataOut.SF_MON_KBDG_PSP2[i]);
                this.adaData = true;
            }
            displayData();
           
        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcBangunan.DataSource = null;
                gcBangunan.DataSource = dsGrid;
            }
            else
            {
                gcBangunan.RefreshDataSource();
            }
        }

        private void gvBangunan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonBgnSelect.WASDALSROW_MON_BMN_PSP2)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvBangunan.GetFocusedDataSourceRowIndex();

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

        private void gvBangunan_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    frmTL frmTL = new frmTL(PSPBMN);
            //    frmTL.idAset = dataTerpilih.ID_ASET.ToString();
            //    frmTL.skKeputusan = dataTerpilih.NO_SURAT;
            //    frmTL.teKdBrg.Text = dataTerpilih.KD_BRG;
            //    frmTL.teNoAset.Text = dataTerpilih.NUP.ToString();
            //    frmTL.teUrBrg.Text = dataTerpilih.UR_SSKEL;
            //    frmTL.teKuantitas.EditValue = dataTerpilih.KUANTITAS;
            //    frmTL.teNilaiPersetujuan.EditValue = dataTerpilih.NILAI_PENETAPAN;
            //    frmTL.kdStatus = (dataTerpilih.KD_STATUS == "") ? "02" : dataTerpilih.KD_STATUS;
            //    frmTL.gunaWasdal = dataTerpilih.GUNA_WASDAL;
            //    frmTL.ShowDialog();
            //}
            //catch
            //{
            //}
        }




    }
}
