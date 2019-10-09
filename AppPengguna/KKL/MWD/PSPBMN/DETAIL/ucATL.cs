using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKL.MWD.PSPBMN.DETAIL
{

    public partial class ucATL : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcMonATLSelect.execute_pttClient svcATLSelect = null;
        SvcMonATLSelect.OutputParameters outATLSelect = null;
        SvcMonATLSelect.WASDALSROW_MON_BMN_PSP2 dataTerpilih = null;

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

        public ucATL(FrmKoorKL _frmKoorKL, ucMonLAIN _ucPspBmn)
        {
            InitializeComponent();
            this.frmKpknl = _frmKoorKL;
            PSPBMN = _ucPspBmn;
        }

        #region LOAD DATA //================================

        public void getATL()
        {
            frmKpknl.Enabled = false;
            try
            {
                frmKpknl.fToggleProgressBar("start");

                SvcMonATLSelect.InputParameters parInp = new SvcMonATLSelect.InputParameters();

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
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcATLSelect = new SvcMonATLSelect.execute_pttClient();
                svcATLSelect.Beginexecute(parInp, new AsyncCallback(getDataATL), null);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataATL(IAsyncResult result)
        {
            try
            {
                outATLSelect = svcATLSelect.Endexecute(result);
                svcATLSelect.Close();
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                this.Invoke(new LoadDataATL(this.loadDataATL), outATLSelect);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataATL(SvcMonATLSelect.OutputParameters dataOut);

        private void loadDataATL(SvcMonATLSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_KLAIN_PSP2.Count();
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
                //dataOut.SF_MON_KLAIN_PSP2[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_MON_KLAIN_PSP2[i].TGL_SK);
                dsGrid.Add(  dataOut.SF_MON_KLAIN_PSP2[i]);
                this.adaData = true;
            }
            displayData();
          
        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcATL.DataSource = null;
                gcATL.DataSource = dsGrid;
            }
            else
            {
                gcATL.RefreshDataSource();
            }
        }

        private void gvATL_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonATLSelect.WASDALSROW_MON_BMN_PSP2)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvATL.GetFocusedDataSourceRowIndex();

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

        private void gvATL_DoubleClick(object sender, EventArgs e)
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
