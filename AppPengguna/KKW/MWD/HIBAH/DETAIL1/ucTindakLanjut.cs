﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKW.MWD.HIBAH.DETAIL1
{

    public partial class ucTindakLanjut : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalPTMonBMNAll.execute_pttClient svcMonWasdalAllSelect = null;
        SvcWasdalPTMonBMNAll.OutputParameters outMonAllSelect = null;
        SvcWasdalPTMonBMNAll.WASDALSROW_MON_ALL_PT dataTerpilih = null;


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

        ucHibah HIBAH = null;
        FrmKoorKorwil frmSatker = null;

        public ucTindakLanjut(FrmKoorKorwil _frmSatker, ucHibah _ucHIBAH)
        {
            InitializeComponent();
            this.frmSatker = _frmSatker;
            HIBAH = _ucHIBAH;
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

                SvcWasdalPTMonBMNAll.InputParameters parInp = new SvcWasdalPTMonBMNAll.InputParameters();

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
                parInp.STR_WHERE = " ID_KORWIL = "+ konfigApp.idKorwil + " " + HIBAH.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcMonWasdalAllSelect = new SvcWasdalPTMonBMNAll.execute_pttClient();
                svcMonWasdalAllSelect.Beginexecute(parInp, new AsyncCallback(getDataTLHIBAH), null);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataTLHIBAH(IAsyncResult result)
        {
            try
            {
                outMonAllSelect = svcMonWasdalAllSelect.Endexecute(result);
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new LoadDataTLHIBAH(this.loadDataTLHIBAH), outMonAllSelect);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataTLHIBAH(SvcWasdalPTMonBMNAll.OutputParameters dataOut);

        private void loadDataTLHIBAH(SvcWasdalPTMonBMNAll.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_ALL_PT.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_ALL_PT[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmSatker.bbiMWasdalMore.Enabled = true;
                HIBAH.sbCari.Enabled = true;
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
                    HIBAH.sbCari.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                    HIBAH.sbCari.Enabled = false;
                }
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_MON_ALL_PT[i].DARI_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].DARI_TGL);
                dataOut.SF_MON_ALL_PT[i].SD_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].SD_TGL);
                dataOut.SF_MON_ALL_PT[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_MON_ALL_PT[i].TGL_PEROLEHAN = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_PEROLEHAN);
                //dataOut.SF_MON_ALL_PT[i].TGL_PP = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_PP);
                //dataOut.SF_MON_ALL_PT[i].TGL_SETOR = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_SETOR);
                //dataOut.SF_MON_ALL_PT[i].TGL_TRANSAKSI = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TGL_TRANSAKSI);
                dataOut.SF_MON_ALL_PT[i].TANGGAL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PT[i].TANGGAL);
                if (dataOut.SF_MON_ALL_PT[i].PERIODE == "H")
                {
                    dataOut.SF_MON_ALL_PT[i].PERIODE = "Harian";
                }
                else if (dataOut.SF_MON_ALL_PT[i].PERIODE == "M")
                {
                    dataOut.SF_MON_ALL_PT[i].PERIODE = "Mingguan";
                }
                else if (dataOut.SF_MON_ALL_PT[i].PERIODE == "B")
                {
                    dataOut.SF_MON_ALL_PT[i].PERIODE = "Bulanan";
                }
                else if (dataOut.SF_MON_ALL_PT[i].PERIODE == "T")
                {
                    dataOut.SF_MON_ALL_PT[i].PERIODE = "Tahunan";
                }
                dsGrid.Add(dataOut.SF_MON_ALL_PT[i]);
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
                gcTLHIBAH.DataSource = null;
                gcTLHIBAH.DataSource = dsGrid;
            }
            else
            {
                gcTLHIBAH.RefreshDataSource();
            }
        }

        private void gvTLHIBAH_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcWasdalPTMonBMNAll.WASDALSROW_MON_ALL_PT)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvTLHIBAH.GetFocusedDataSourceRowIndex();

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
        //private void gvTLHIBAH_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (frmTL == null)
        //        {
        //            frmTL = new frmTL(HIBAH);
        //            frmTL.toggleProgressBar = new ToggleProgressBar(toggleProgBarPu);
        //        }
        //        frmTL.teKdBrg.Text = dataTerpilih.KD_BRG;
        //        //frmTL.teKdSatker.Text = dataTerpilih.KD_SATKER;
        //        //frmTL.teNmSatker.Text = dataTerpilih.UR_SATKER;
        //        frmTL.teUrBrg.Text = dataTerpilih.UR_SSKEL;
        //        frmTL.teNoAset.Text = dataTerpilih.NUP.ToString();
        //        frmTL.teNoSK.Text = dataTerpilih.NO_SURAT;
        //        frmTL.teTglSK.Text = konfigApp.DateToString(dataTerpilih.TANGGAL);

        //        frmTL.idAset = dataTerpilih.ID_ASET;
        //        frmTL.kdStatus = dataTerpilih.KD_STATUS;
        //        frmTL.KUANTITAS = dataTerpilih.KUANTITAS;
        //        frmTL.luas = dataTerpilih.LUAS_LAYANAN;
        //        frmTL.FILE_BUKTI = dataTerpilih.FILE_BUKTI;
        //        frmTL.teJnsBukti.Text = dataTerpilih.JNS_BUKTI_LAKSANAAN;
        //        frmTL.teKet.Text = dataTerpilih.KET;
        //        frmTL.teNamaPihakKetiga.Text = dataTerpilih.NM_PHK_LAIN;
        //        frmTL.teNilaiPenetapan.Text = dataTerpilih.NILAI_PENETAPAN.ToString();
        //        frmTL.teNoBukti.Text = dataTerpilih.NO_BUKTI_LAKSANA;
        //        frmTL.TGL_BUKTI_LAKSANA = dataTerpilih.TGL_BUKTI_LAKSANA;
        //        frmTL.ShowDialog();
        //    }
        //    catch
        //    {
        //    }
        //}




    }
}
