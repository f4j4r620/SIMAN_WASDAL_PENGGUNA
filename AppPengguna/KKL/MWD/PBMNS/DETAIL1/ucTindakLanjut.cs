﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKL.MWD.PBMNS.DETAIL1
{

    public partial class ucTindakLanjut : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalPSPAllSelect.execute_pttClient svcMonWasdalAllSelect = null;
        SvcWasdalPSPAllSelect.OutputParameters outMonAllSelect = null;
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

        ucPBMNS PBMNS = null;
        FrmKoorKL frmKl = null;

        public ucTindakLanjut(FrmKoorKL _frmKl, ucPBMNS _ucASPBMN)
        {
            InitializeComponent();
            this.frmKl = _frmKl;
            PBMNS = _ucASPBMN;
        }

        #region LOAD DATA//================================

        public void getTindakLanjut()
        {
            frmKl.Enabled = false;
            try
            {
                frmKl.fToggleProgressBar("start");

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
                parInp.STR_WHERE = " (NO_SURAT <> '-') AND KD_KL = '" + konfigApp.kodeKl + "' " + PBMNS.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcMonWasdalAllSelect = new SvcWasdalPSPAllSelect.execute_pttClient();
                svcMonWasdalAllSelect.Beginexecute(parInp, new AsyncCallback(getDataTLPBMNS), null);
            }
            catch
            {
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataTLPBMNS(IAsyncResult result)
        {
            try
            {
                outMonAllSelect = svcMonWasdalAllSelect.Endexecute(result);
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                this.Invoke(new LoadDataTLPBMNS(this.loadDataTLPBMNS), outMonAllSelect);
            }
            catch
            {
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataTLPBMNS(SvcWasdalPSPAllSelect.OutputParameters dataOut);

        private void loadDataTLPBMNS(SvcWasdalPSPAllSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_ALL_PSP.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_ALL_PSP[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmKl.bbiMWasdalMore.Enabled = true;
                PBMNS.sbCari.Enabled = true;
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
                    frmKl.bbiMWasdalMore.Enabled = false;
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

                dataOut.SF_MON_ALL_PSP[i].PERIODE = konfigApp.setPeriode(dataOut.SF_MON_ALL_PSP[i].PERIODE);
                dataOut.SF_MON_ALL_PSP[i].TGL_BUKTI_LAKSANA = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].TGL_BUKTI_LAKSANA);
                dataOut.SF_MON_ALL_PSP[i].TGL_PEROLEHAN = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].TGL_PEROLEHAN);
                dataOut.SF_MON_ALL_PSP[i].SD_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].SD_TGL);
                dataOut.SF_MON_ALL_PSP[i].DARI_TGL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].DARI_TGL);
                dataOut.SF_MON_ALL_PSP[i].TANGGAL = konfigApp.setDefaultDate(dataOut.SF_MON_ALL_PSP[i].TANGGAL);


                dsGrid.Add(dataOut.SF_MON_ALL_PSP[i]);
                this.adaData = true;
            }
            labelTotData.Text = "";
            labelTotData.Text = "Menampilkan " + jmlCurrent + " dari total " + totalData + " data";
            displayData();

        }
        #endregion

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

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcTLPBMNS.DataSource = null;
                gcTLPBMNS.DataSource = dsGrid;
            }
            else
            {
                gcTLPBMNS.RefreshDataSource();
            }
            gvTLPBMNS.BestFitColumns();
        }

        private void gvTLPBMNS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcWasdalPSPAllSelect.WASDALSROW_MON_ALL_PSP)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvTLPBMNS.GetFocusedDataSourceRowIndex();

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
        //private void gvTLPBMNS_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (frmTL == null)
        //        {
        //            frmTL = new frmTL(PBMNS);
        //            frmTL.toggleProgressBar = new ToggleProgressBar(toggleProgBarPu);
        //        }
        //        frmTL.KD_SATKER = dataTerpilih.KD_SATKER;
        //        frmTL.UR_SATKER = dataTerpilih.UR_SATKER;
        //        frmTL.KD_BRG = dataTerpilih.KD_BRG;
        //        frmTL.UR_SSKEL = dataTerpilih.UR_SSKEL;
        //        frmTL.NUP = Convert.ToString(dataTerpilih.ID_ASET);
        //        frmTL.NO_SK = dataTerpilih.NO_SURAT;
        //        frmTL.TGL_SK = Convert.ToDateTime(dataTerpilih.TANGGAL);
        //        frmTL.JNS_BUKTI_LAKSANA = dataTerpilih.JNS_BUKTI_LAKSANAAN;
        //        frmTL.NO_BUKTI_LAKSANA = dataTerpilih.NO_BUKTI_LAKSANA;
        //        frmTL.TGL_BUKTI_LAKSANA = Convert.ToDateTime(dataTerpilih.TGL_BUKTI_LAKSANA);
        //        frmTL.pihakketiga = dataTerpilih.NM_PHK_LAIN;
        //        frmTL.JangkaWaktu = dataTerpilih.JANGKA_WAKTU;
        //        frmTL.SETPERIODE(dataTerpilih.PERIODE);
        //        frmTL.TGL_mulai = Convert.ToDateTime(dataTerpilih.DARI_TGL);
        //        frmTL.TGL_selesai = Convert.ToDateTime(dataTerpilih.SD_TGL);
        //        frmTL.kdStatus = dataTerpilih.KD_STATUS;
        //        frmTL.KETERANGAN = dataTerpilih.KET;
        //        frmTL.FILE_BUKTI = dataTerpilih.FILE_BUKTI;
        //        frmTL.KUANTITAS = dataTerpilih.KUANTITAS;
        //        frmTL.NILAI_PELAKSANAAN = dataTerpilih.NILAI_PENETAPAN;
        //        frmTL.ShowDialog();
        //    }
        //    catch
        //    {
        //    }
        //}




    }
}