using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKL.MWD.HapusBmnPengadilan.DETAIL1
{

    public partial class ucTindakLanjut : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalHapusMonBMNAll.monBMNAll_pttClient svcMonWasdalAllSelect = null;
        SvcWasdalHapusMonBMNAll.OutputParameters outMonAllSelect = null;
        SvcWasdalHapusMonBMNAll.WASDALSROW_MON_ALL_HAPUS dataTerpilih = null;



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

        ucHapusBmnPengadilan HapusBmnPengadilan = null;
        FrmKoorKL frmKl = null;

        public ucTindakLanjut(FrmKoorKL _frmKl, ucHapusBmnPengadilan _ucHapusBmnPengadilan)
        {
            InitializeComponent();
            this.frmKl = _frmKl;
            HapusBmnPengadilan = _ucHapusBmnPengadilan;
        }

        #region Thread
        private void toggleProgBarPu(string kondisi)
        {
            if (kondisi == "start") this.aktifkanProgresBar();
            else this.nonAktifkanprogressBar();
        }

        private void aktifkanProgresBar()
        {
            toggleProgressBar("start");
        }

        private void nonAktifkanprogressBar()
        {
            toggleProgressBar("finish");
        }

        public delegate void AktifkanForm(string str);

        public void aktifkanForm(string str)
        {
            this.Enabled = true;
        }
        #endregion

        #region LOAD DATA//================================

        public void getTindakLanjut()
        {
            frmKl.Enabled = false;
            try
            {
                frmKl.fToggleProgressBar("start");

                SvcWasdalHapusMonBMNAll.InputParameters parInp = new SvcWasdalHapusMonBMNAll.InputParameters();

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
                parInp.STR_WHERE = " (NO_SURAT <> '-') AND KD_KL = '" + konfigApp.kodeKl + "' " + HapusBmnPengadilan.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcMonWasdalAllSelect = new SvcWasdalHapusMonBMNAll.monBMNAll_pttClient();
                svcMonWasdalAllSelect.Beginexecute(parInp, new AsyncCallback(getDataTLHapusPengadilan), null);
            }
            catch
            {
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataTLHapusPengadilan(IAsyncResult result)
        {
            try
            {
                outMonAllSelect = svcMonWasdalAllSelect.Endexecute(result);
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                this.Invoke(new LoadDataTLHapusPengadilan(this.loadDataTLHapusPengadilan), outMonAllSelect);
            }
            catch
            {
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataTLHapusPengadilan(SvcWasdalHapusMonBMNAll.OutputParameters dataOut);

        private void loadDataTLHapusPengadilan(SvcWasdalHapusMonBMNAll.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_ALL_HAPUS.Count();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmKl.bbiMWasdalMore.Enabled = true;
                HapusBmnPengadilan.sbCari.Enabled = true;
            }
            else
            {
                if (this.modeLoadData == "normal")
                {
                    this.masihAdaData = false;
                    frmKl.bbiMWasdalMore.Enabled = false;
                    HapusBmnPengadilan.sbCari.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                    HapusBmnPengadilan.sbCari.Enabled = false;
                }
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_MON_ALL_HAPUS[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_HAPUS[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_MON_ALL_HAPUS[i].TGL_PEROLEHAN = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_HAPUS[i].TGL_PEROLEHAN);
                dsGrid.Add(dataOut.SF_MON_ALL_HAPUS[i]);
                this.adaData = true;
            }
            displayData();

        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcTLHapusPengadilan.DataSource = null;
                gcTLHapusPengadilan.DataSource = dsGrid;
            }
            else
            {
                gcTLHapusPengadilan.RefreshDataSource();
            }
            gvTLHapusPengadilan.BestFitColumns();
        }

        private void gvTLHapusPengadilan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcWasdalHapusMonBMNAll.WASDALSROW_MON_ALL_HAPUS)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvTLHapusPengadilan.GetFocusedDataSourceRowIndex();

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

        //frmTL frmTL;
        //private void gvTLHapusPengadilan_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (frmTL == null)
        //        {
        //            frmTL = new frmTL(HapusBmnPengadilan);
        //            frmTL.toggleProgressBar = new ToggleProgressBar(toggleProgBarPu);
        //        }
        //        frmTL.teKdSatker.Text = dataTerpilih.KD_SATKER;
        //        frmTL.teNmSatker.Text = dataTerpilih.UR_SATKER;
        //        frmTL.teKdBrg.Text = dataTerpilih.KD_BRG;
        //        frmTL.teUrBrg.Text = dataTerpilih.UR_SSKEL;
        //        frmTL.teNoAset.Text = dataTerpilih.NUP.ToString();
        //        frmTL.teNoSK.Text = dataTerpilih.NO_SURAT;
        //        frmTL.teTglSK.Text = konfigApp.DateToString(dataTerpilih.TANGGAL);
        //        frmTL.idAset = dataTerpilih.ID_ASET;
        //        frmTL.KUANTITAS = dataTerpilih.KUANTITAS;
        //        frmTL.teJnsBukti.Text = dataTerpilih.JNS_BUKTI_LAKSANAAN;
        //        frmTL.teFile.Text = dataTerpilih.FILE_BUKTI;
        //        frmTL.teKeterangan.Text = dataTerpilih.KET;
        //        frmTL.teNoBukti.Text = dataTerpilih.NO_BUKTI_LAKSANA;
        //        frmTL.TGL_BUKTI_LAKSANA = dataTerpilih.TGL_BUKTI_LAKSANA;
        //        frmTL.NlaiPenetapan = dataTerpilih.NILAI_PENETAPAN;
        //        frmTL.ShowDialog();
        //    }
        //    catch
        //    {
        //    }
        //}




    }
}
