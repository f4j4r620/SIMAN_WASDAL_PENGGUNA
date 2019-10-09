﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KES1.MWD.HapusBmnLain.DETAIL1
{

    public partial class ucTindakLanjut : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalHapusMonBMNAll.monBMNAll_pttClient SvcWasdalHapusMonBMNAll = null;
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

        ucHapusBmnLain HapusBmnLain = null;
        FrmKoorEselon1 frmKl = null;

        public ucTindakLanjut(FrmKoorEselon1 _frmKl, ucHapusBmnLain _ucHapusBmnLain)
        {
            InitializeComponent();
            this.frmKl = _frmKl;
            HapusBmnLain = _ucHapusBmnLain;
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
                parInp.STR_WHERE = "ID_ESELON1 = " + konfigApp.idEselon1 + " AND (NO_SURAT <> '-') " + HapusBmnLain.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                SvcWasdalHapusMonBMNAll = new SvcWasdalHapusMonBMNAll.monBMNAll_pttClient();
                SvcWasdalHapusMonBMNAll.Beginexecute(parInp, new AsyncCallback(getDataTLHapusLain), null);
            }
            catch
            {
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataTLHapusLain(IAsyncResult result)
        {
            try
            {
                outMonAllSelect = SvcWasdalHapusMonBMNAll.Endexecute(result);
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                this.Invoke(new LoadDataTLHapusLain(this.loadDataTLHapusLain), outMonAllSelect);
            }
            catch
            {
                frmKl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataTLHapusLain(SvcWasdalHapusMonBMNAll.OutputParameters dataOut);

        private void loadDataTLHapusLain(SvcWasdalHapusMonBMNAll.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_ALL_HAPUS.Count();
            decimal jmlCurrent = 0;
            string totalData = (jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_ALL_HAPUS[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmKl.bbiMWasdalMore.Enabled = true;
                HapusBmnLain.sbCari.Enabled = true;
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
                    HapusBmnLain.sbCari.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                    HapusBmnLain.sbCari.Enabled = false;
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
            labelTotData.Text = "";
            labelTotData.Text = "Menampilkan " + jmlCurrent + " dari total " + totalData + " data";
            displayData();

        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcTLHapusLain.DataSource = null;
                gcTLHapusLain.DataSource = dsGrid;
            }
            else
            {
                gcTLHapusLain.RefreshDataSource();
            }
        }

        private void gvTLHapusLain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcWasdalHapusMonBMNAll.WASDALSROW_MON_ALL_HAPUS)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvTLHapusLain.GetFocusedDataSourceRowIndex();

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
        //private void gvTLHapusLain_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (frmTL == null)
        //        {
        //            frmTL = new frmTL(HapusBmnLain);
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