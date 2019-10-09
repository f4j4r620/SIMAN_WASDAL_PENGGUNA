using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKW.MWD.PPBMN.DETAIL1
{

    public partial class ucTindakLanjut : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalManfaatAllSelect.execute_pttClient svcMonWasdalAllSelect = null;
        SvcWasdalManfaatAllSelect.OutputParameters outMonAllSelect = null; 
        SvcWasdalManfaatAllSelect.WASDALSROW_MON_ALL_MANFAAT dataTerpilih = null;



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

        ucPPBMN PPBMN = null;
        FrmKoorKorwil frmSatker = null;

        public ucTindakLanjut(FrmKoorKorwil _frmSatker, ucPPBMN _ucPPBMN)
        {
            InitializeComponent();
            this.frmSatker = _frmSatker;
            PPBMN = _ucPPBMN;
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
            frmSatker.Enabled = false;
            try
            {
                frmSatker.fToggleProgressBar("start");

                SvcWasdalManfaatAllSelect.InputParameters parInp = new SvcWasdalManfaatAllSelect.InputParameters();

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
                parInp.STR_WHERE = " ID_KORWIL = " + konfigApp.idKorwil + " AND (NO_SURAT <> '-')" + PPBMN.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcMonWasdalAllSelect = new SvcWasdalManfaatAllSelect.execute_pttClient();
                svcMonWasdalAllSelect.Beginexecute(parInp, new AsyncCallback(getDataTLPPBMN), null);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataTLPPBMN(IAsyncResult result)
        {
            try
            {
                outMonAllSelect = svcMonWasdalAllSelect.Endexecute(result);
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new LoadDataTLPPBMN(this.loadDataTLPPBMN), outMonAllSelect);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataTLPPBMN(SvcWasdalManfaatAllSelect.OutputParameters dataOut);

        private void loadDataTLPPBMN(SvcWasdalManfaatAllSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_ALL_MANFAAT.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_ALL_MANFAAT[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmSatker.bbiMWasdalMore.Enabled = true;
                PPBMN.sbCari.Enabled = true;
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
                    frmSatker.bbiMWasdalMore.Enabled = false;
                    PPBMN.sbCari.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                    PPBMN.sbCari.Enabled = false;
                }
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_MON_ALL_MANFAAT[i].PERIODE = konfigApp.setPeriode(dataOut.SF_MON_ALL_MANFAAT[i].PERIODE);
                dataOut.SF_MON_ALL_MANFAAT[i].TANGGAL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_MANFAAT[i].TANGGAL);
                switch (dataOut.SF_MON_ALL_MANFAAT[i].PERIODE)
                {
                    case "0":
                        dataOut.SF_MON_ALL_MANFAAT[i].PERIODE = "Harian";
                        break;
                    case "1":
                        dataOut.SF_MON_ALL_MANFAAT[i].PERIODE = "Mingguan";
                        break;
                    case "2":
                        dataOut.SF_MON_ALL_MANFAAT[i].PERIODE = "Bulanan";
                        break;
                    case "3":
                        dataOut.SF_MON_ALL_MANFAAT[i].PERIODE = "Tahunan";
                        break;
                }

                dataOut.SF_MON_ALL_MANFAAT[i].DARI_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_MANFAAT[i].DARI_TGL);
                dataOut.SF_MON_ALL_MANFAAT[i].SD_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_MANFAAT[i].SD_TGL);
                dataOut.SF_MON_ALL_MANFAAT[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_MANFAAT[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_MON_ALL_MANFAAT[i].TGL_PEROLEHAN = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_MANFAAT[i].TGL_PEROLEHAN);
                dataOut.SF_MON_ALL_MANFAAT[i].TGL_SETOR = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_MANFAAT[i].TGL_SETOR);
                dataOut.SF_MON_ALL_MANFAAT[i].TGL_TRANSAKSI = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_MANFAAT[i].TGL_TRANSAKSI);
                
                dsGrid.Add(dataOut.SF_MON_ALL_MANFAAT[i]);
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
                gcTLPPBMN.DataSource = null;
                gcTLPPBMN.DataSource = dsGrid;
            }
            else
            {
                gcTLPPBMN.RefreshDataSource();
            }
        }

        private void gvTLPPBMN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcWasdalManfaatAllSelect.WASDALSROW_MON_ALL_MANFAAT)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvTLPPBMN.GetFocusedDataSourceRowIndex();

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
        //private void gvTLPPBMN_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (frmTL == null)
        //        {
        //            frmTL = new frmTL(PPBMN);
        //            frmTL.toggleProgressBar = new ToggleProgressBar(toggleProgBarPu);
        //        }
        //        frmTL.teKdBrg.Text = dataTerpilih.KD_BRG;
        //        frmTL.teKdSatker.Text = dataTerpilih.KD_SATKER;
        //        frmTL.teNmSatker.Text = dataTerpilih.UR_SATKER;
        //        frmTL.teUrBrg.Text = dataTerpilih.UR_SSKEL;
        //        frmTL.teNoAset.Text = dataTerpilih.NUP.ToString();
        //        frmTL.teNoSK.Text = dataTerpilih.NO_SURAT;
        //        frmTL.teTglSK.Text = konfigApp.DateToString(dataTerpilih.TANGGAL); 
        //        frmTL.idAset = dataTerpilih.ID_ASET;
        //        frmTL.kdStatus = dataTerpilih.KD_STATUS;
        //        frmTL.KUANTITAS = dataTerpilih.KUANTITAS;
        //        frmTL.luas = dataTerpilih.LUAS_LAYANAN;
        //        frmTL.NILAI_PELAKSANAAN = dataTerpilih.NILAI_PENETAPAN;
        //        frmTL.NILAI_PNBP = dataTerpilih.NILAI_PNBP;
        //        frmTL.TGL_SETOR = dataTerpilih.TGL_SETOR;
        //        frmTL.NTB = dataTerpilih.NTB;
        //        frmTL.TGL_TRANSAKSI = dataTerpilih.TGL_TRANSAKSI;
        //        frmTL.NM_PENYETOR = dataTerpilih.NM_PENYETOR;
        //        frmTL.KD_AKUN = dataTerpilih.KD_AKUN;
        //        frmTL.UR_AKUN = dataTerpilih.UR_AKUN;
        //        frmTL.NM_PHK_KETIGA = dataTerpilih.NM_PHK_LAIN;
        //        frmTL.NPWP_PHK_KETIGA = dataTerpilih.NPWP_PHK_LAIN;
        //        frmTL.JANGKA_WAKTU = dataTerpilih.JANGKA_WAKTU;
        //        frmTL.PERIODE = dataTerpilih.PERIODE;
        //        frmTL.DARI_TGL = dataTerpilih.DARI_TGL;
        //        frmTL.SD_TGL = dataTerpilih.SD_TGL;
        //        frmTL.teFile.Text = dataTerpilih.FILE_BUKTI;
        //        frmTL.teJnsBukti.Text = dataTerpilih.JNS_BUKTI_LAKSANAAN;
        //        frmTL.teNoBukti.Text = dataTerpilih.NO_BUKTI_LAKSANA;
        //        frmTL.teKeterangan.Text = dataTerpilih.KET;
        //        frmTL.TGL_BUKTI_LAKSANA = dataTerpilih.TGL_BUKTI_LAKSANA; 
        //        frmTL.ShowDialog();
        //    }
        //    catch
        //    {
        //    }
        //}

        private void gcAngkutan_Click(object sender, EventArgs e)
        {

        }




    }
}
