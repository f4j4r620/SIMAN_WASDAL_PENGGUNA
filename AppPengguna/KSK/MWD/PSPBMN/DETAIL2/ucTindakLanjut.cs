using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KSK.MWD.PSPBMN.DETAIL2
{

    public partial class ucTindakLanjut : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcWasdalPSPAllSelect.execute_pttClient svcWasdalPSPAllSelect = null;
        SvcWasdalPSPAllSelect.OutputParameters outPSPAllSelect = null;
        SvcWasdalPSPAllSelect.WASDALSROW_MON_ALL_PSP dataTerpilih = null;



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

        ucPSPBMN PBMNS = null;
        FrmKoorSatker frmSatker = null;

        public ucTindakLanjut(FrmKoorSatker _frmSatker, ucPSPBMN _ucASPBMN)
        {
            InitializeComponent();
            this.frmSatker = _frmSatker;
            PBMNS = _ucASPBMN;
        }

        #region LOAD DATA//================================

        public void getTindakLanjut()
        {
            frmSatker.Enabled = false;
            try
            {
                frmSatker.fToggleProgressBar("start");

                SvcWasdalPSPAllSelect.InputParameters parInp = new SvcWasdalPSPAllSelect.InputParameters();

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
                parInp.STR_WHERE = String.Format("(ID_SATKER={0} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON={1}))", konfigApp.idSatker, konfigApp.idSatker) + " AND (NO_SURAT is null)  " + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcWasdalPSPAllSelect = new SvcWasdalPSPAllSelect.execute_pttClient();
                svcWasdalPSPAllSelect.Beginexecute(parInp, new AsyncCallback(getDataAngkutan), null);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataAngkutan(IAsyncResult result)
        {
            try
            {
                outPSPAllSelect = svcWasdalPSPAllSelect.Endexecute(result);
                svcWasdalPSPAllSelect.Close();
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new LoadDataAngkutan(this.loadDataAngkutan), outPSPAllSelect);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAngkutan(SvcWasdalPSPAllSelect.OutputParameters dataOut);

        private void loadDataAngkutan(SvcWasdalPSPAllSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_ALL_PSP.Count();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmSatker.bbiMWasdalMore.Enabled = true;
                PBMNS.sbCari.Enabled = true;
            }
            else
            {
                if (this.modeLoadData == "normal")
                {
                    this.masihAdaData = false;
                    frmSatker.bbiMWasdalMore.Enabled = false;
                    PBMNS.sbCari.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                    PBMNS.sbCari.Enabled = false;
                }
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_MON_ALL_PSP[i].TANGGAL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].TANGGAL);

                dsGrid.Add(dataOut.SF_MON_ALL_PSP[i]);
                this.adaData = true;
            }
            displayData();

        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcAngkutan.DataSource = null;
                gcAngkutan.DataSource = dsGrid;
            }
            else
            {
                gcAngkutan.RefreshDataSource();
            }
        }

        private void gvAngkutan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcWasdalPSPAllSelect.WASDALSROW_MON_ALL_PSP)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvAngkutan.GetFocusedDataSourceRowIndex();

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

        private void gvAngkutan_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmTL frmTL = new frmTL(PBMNS);
                frmTL.idAset = dataTerpilih.ID_ASET.ToString();
                frmTL.skKeputusan = dataTerpilih.NO_SURAT;
                frmTL.teKdBrg.Text = dataTerpilih.KD_BRG;
                frmTL.teNoAset.Text = dataTerpilih.NUP.ToString();
                frmTL.teUrBrg.Text = dataTerpilih.UR_SSKEL;
                frmTL.teKuantitas.EditValue = dataTerpilih.KUANTITAS;
                frmTL.teNilaiPersetujuan.EditValue = dataTerpilih.NILAI_PENETAPAN;
                frmTL.kdStatus = (dataTerpilih.KD_STATUS == "") ? "03" : dataTerpilih.KD_STATUS;
                frmTL.gunaWasdal = dataTerpilih.GUNA_WASDAL;

                //frmTL.KD_SATKER = dataTerpilih.KD_SATKER;
                //frmTL.UR_SATKER = dataTerpilih.UR_SATKER;
                //frmTL.KD_BRG = dataTerpilih.KD_BRG;
                //frmTL.UR_SSKEL= dataTerpilih.UR_SSKEL;
                //frmTL.NUP = dataTerpilih.NUP.ToString();
                //frmTL.NO_SK = dataTerpilih.NO_SURAT;
                //frmTL.TGL_SK = dataTerpilih.TANGGAL;
                //frmTL.JNS_BUKTI_LAKSANA = "";
                //frmTL.NO_BUKTI_LAKSANA = "";
                //frmTL.TGL_BUKTI_LAKSANA = dataTerpilih.TANGGAL;
                //frmTL.NM_PHK_LAIN = dataTerpilih.NM_PHK_LAIN;
                //frmTL.JANGKA_WAKTU = dataTerpilih.JANGKA_WAKTU;
                //frmTL.PERIODE = "";
                //frmTL.DARI_TGL = dataTerpilih.TANGGAL;
                //frmTL.SD_TGL = dataTerpilih.TANGGAL;
                ////frmTL.kdStatus = (dataTerpilih.KD_STATUS == "") ? "03" : ((dataTerpilih.KD_STATUS == "") ? "04" : ((dataTerpilih.KD_STATUS == "") ? "99" : dataTerpilih.KD_STATUS));
                //frmTL.kdStatus = (dataTerpilih.KD_STATUS == "") ? "03" : ((dataTerpilih.KD_STATUS == "") ? "04" : ((dataTerpilih.KD_STATUS == "") ? "99" : dataTerpilih.KD_STATUS));

                frmTL.ShowDialog();
            }
            catch
            {
            }
        }




    }
}
