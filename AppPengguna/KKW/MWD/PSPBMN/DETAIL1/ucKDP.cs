﻿using System;
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
    public partial class ucKDP : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcMonKDPSelect.execute_pttClient svcKDPSelect = null;
        SvcMonKDPSelect.OutputParameters outKDPSelect = null;
        SvcMonKDPSelect.WASDALSROW_MON_BMN_PSP dataTerpilih = null;

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

        public ucKDP(FrmKoorSatker _frmKpknl, ucPSPBMN _ucPspBmn)
        {
            InitializeComponent();
            this.frmKpknl = _frmKpknl;
            PSPBMN = _ucPspBmn;
        }

        #region LOAD DATA//================================

        public void getKDP()
        {
            frmKpknl.Enabled = false;
            try
            {
                frmKpknl.fToggleProgressBar("start");

                SvcMonKDPSelect.InputParameters parInp = new SvcMonKDPSelect.InputParameters();

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
                svcKDPSelect = new SvcMonKDPSelect.execute_pttClient();
                svcKDPSelect.Beginexecute(parInp, new AsyncCallback(getDataKDP), null);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataKDP(IAsyncResult result)
        {
            try
            {
                outKDPSelect = svcKDPSelect.Endexecute(result);
                svcKDPSelect.Close();
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                this.Invoke(new LoadDataKDP(this.loadDataKDP), outKDPSelect);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataKDP(SvcMonKDPSelect.OutputParameters dataOut);

        private void loadDataKDP(SvcMonKDPSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_KDP_PSP.Count();
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
                dataOut.SF_MON_KDP_PSP[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_MON_KDP_PSP[i].TGL_SK);
          
                dsGrid.Add(  dataOut.SF_MON_KDP_PSP[i]);
                this.adaData = true;
            }
            displayData();
    
        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcKDP.DataSource = null;
                gcKDP.DataSource = dsGrid;
            }
            else
            {
                gcKDP.RefreshDataSource();
            }
        }

        private void gvKDP_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonKDPSelect.WASDALSROW_MON_BMN_PSP)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvKDP.GetFocusedDataSourceRowIndex();

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

        private void gvKDP_DoubleClick(object sender, EventArgs e)
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
