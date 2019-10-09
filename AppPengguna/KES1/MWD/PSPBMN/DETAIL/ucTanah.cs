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
    public delegate void CariDataOnline(string strCariData);

    public partial class ucTanah : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcMonTanahSelect.execute_pttClient svcTanahSelect = null;
        SvcMonTanahSelect.OutputParameters outTanahSelect = null;
        SvcMonTanahSelect.WASDALSROW_MON_BMN_PSP2 dataTerpilih = null;

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


        public ucTanah(FrmKoorKL _frmKoorKL, ucMonLAIN _ucPspBmn)
        {
            InitializeComponent();
            this.frmKpknl = _frmKoorKL;
            PSPBMN = _ucPspBmn;
        }

        #region LOAD DATA TANAH//================================

        public void getTanah()
        {
            frmKpknl.Enabled = false;
            try
            {
                frmKpknl.fToggleProgressBar("start");

                SvcMonTanahSelect.InputParameters parInp = new SvcMonTanahSelect.InputParameters();

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
                parInp.STR_WHERE = " (NO_SURAT <> '-') " + PSPBMN.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcTanahSelect = new SvcMonTanahSelect.execute_pttClient();
                svcTanahSelect.Beginexecute(parInp, new AsyncCallback(getDataTanah), null);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataTanah(IAsyncResult result)
        {
            try
            {
                outTanahSelect = svcTanahSelect.Endexecute(result);
                svcTanahSelect.Close();
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                this.Invoke(new LoadDataTanah(this.loadDataTanah), outTanahSelect);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataTanah(SvcMonTanahSelect.OutputParameters dataOut);

        private void loadDataTanah(SvcMonTanahSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_KTNH_PSP2.Count();
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
                //dataOut.SF_MON_KTNH_PSP2[i].TANGGAL = konfigApp.setDefaultDate(dataOut.SF_MON_KTNH_PSP2[i].TANGGAL);
               
                dsGrid.Add(  dataOut.SF_MON_KTNH_PSP2[i]);
                this.adaData = true;
            }
            displayData();
           
        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcTanah.DataSource = null;
                gcTanah.DataSource = dsGrid;
            }
            else
            {
                gcTanah.RefreshDataSource();
            }
        }

        private void gvTanah_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonTanahSelect.WASDALSROW_MON_BMN_PSP2)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvTanah.GetFocusedDataSourceRowIndex();

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

        private void gvTanah_DoubleClick(object sender, EventArgs e)
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
            //    //frmTL.teNilaiPersetujuan.EditValue = dataTerpilih.NILAI_PENETAPAN;
            //    //frmTL.kdStatus = (dataTerpilih.KD_STATUS == "") ? "02" : dataTerpilih.KD_STATUS;
            //    //frmTL.gunaWasdal = dataTerpilih.GUNA_WASDAL;
            //    frmTL.ShowDialog();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("");
            //}
        }




    }
}
