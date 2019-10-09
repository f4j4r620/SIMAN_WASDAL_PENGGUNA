﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using AppPengguna.KSK.WL.PEMANFAATAN;

namespace AppPengguna.KKL.WL.PEMANFAATAN
{

    public partial class ucPemanfaatanNew : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcMonManfaatKorEsKlA1.selectMonManfaatKorEsKlA1_pttClient SvcMonManfaatKorEsKlA1Select = null;
        SvcMonManfaatKorEsKlA1.OutputParameters outLapGunaSkSelect = null;
        SvcMonManfaatKorEsKlA1.WASDALSROW_MON_MANFAAT_KOR_ES_KL_A1 dataTerpilih = null;

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

        public decimal? thnAng
        {
            set { cbThnAng.EditValue = this; }
            get
            {
                if (cbThnAng.SelectedIndex != 0)
                    return Convert.ToDecimal(cbThnAng.EditValue);
                else
                    return null;
                ;
            }
        }

        decimal? thnAnggaran;
        string strThnAng = "";

        ucLapWasdal PBMNS = null;
        FrmKoorKL frmSatker = null;

        public ucPemanfaatanNew(FrmKoorKL _frmSatker, ucLapWasdal _ucASPBMN)
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

                SvcMonManfaatKorEsKlA1.InputParameters parInp = new SvcMonManfaatKorEsKlA1.InputParameters();
               
                if (dataInisial == true)
                {
                    this.currentMaks = konfigApp.dataMaks;
                    this.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    this.currentMin = this.currentMaks + 1;
                    this.currentMaks = this.currentMaks + konfigApp.dataMaks;
                }
                if (thnAng != null)
                {
                    strThnAng = " AND THN_ANG = '" + this.thnAng + "'";
                }
                else
                {
                    strThnAng = "";
                }

                parInp.P_MAX = 0;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = 0;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = "ID_KL = "+konfigApp.idKl + this.strCari + strThnAng;
                parInp.P_COL = "";
                parInp.P_SORT = "";
                SvcMonManfaatKorEsKlA1Select = new SvcMonManfaatKorEsKlA1.selectMonManfaatKorEsKlA1_pttClient();
                SvcMonManfaatKorEsKlA1Select.Beginexecute(parInp, new AsyncCallback(getDataAngkutan), null);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                //this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataAngkutan(IAsyncResult result)
        {
            try
            {
                outLapGunaSkSelect = SvcMonManfaatKorEsKlA1Select.Endexecute(result);
                SvcMonManfaatKorEsKlA1Select.Close();
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                this.Invoke(new LoadDataAngkutan(this.loadDataAngkutan), outLapGunaSkSelect);
            }
            catch
            {
                frmSatker.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmSatker.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataAngkutan(SvcMonManfaatKorEsKlA1.OutputParameters dataOut);

        private void loadDataAngkutan(SvcMonManfaatKorEsKlA1.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_MANFAAT_KOR_ES_KL_A1.Count();
            decimal jmlCurrent = 0;
            string totalData = ""; //(jmlData == 0) ? jmlData.ToString() : dataOut.SF_MON_GUNA_SATKER_A1[jmlData - 1].TOTAL_DATA.ToString();
            if (jmlData == konfigApp.dataMaks)
            {
                this.masihAdaData = true;
                frmSatker.bbiMWasdalMore.Enabled = true;
                PBMNS.sbCari.Enabled = true;
                jmlCurrent = konfigApp.dataMaks;
            }
            else
            {
                if (jmlData > konfigApp.dataMaks)
                    jmlCurrent = konfigApp.dataMaks;
                else
                    jmlCurrent = jmlData;
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
                dsGrid.Add(dataOut.SF_MON_MANFAAT_KOR_ES_KL_A1[i]);
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
                gcAngkutan.DataSource = null;
                gcAngkutan.DataSource = dsGrid;
            }
            else
            {
                gcAngkutan.RefreshDataSource();
            }
            bandedGridView1.BestFitColumns();
        }

        private void gvAngkutan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonManfaatKorEsKlA1.WASDALSROW_MON_MANFAAT_KOR_ES_KL_A1)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = bandedGridView1.GetFocusedDataSourceRowIndex();

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
                dataTerpilih = new SvcMonManfaatKorEsKlA1.WASDALSROW_MON_MANFAAT_KOR_ES_KL_A1();
                dataTerpilih = (SvcMonManfaatKorEsKlA1.WASDALSROW_MON_MANFAAT_KOR_ES_KL_A1)bandedGridView1.GetFocusedRow();
                string label = "SATKER : " + dataTerpilih.KD_SATKER + " - " + dataTerpilih.UR_SATKER;
                FormDtlSatker form = new FormDtlSatker(label, dataTerpilih.ID_SATKER.ToString());
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                dataTerpilih = null;
            }
        }

        public void ShowGridPreview()
        {
            PrintableComponentLink componenlink = new PrintableComponentLink(new PrintingSystem());
            gcAngkutan.DataSource = dsGrid;
            componenlink.Component = gcAngkutan;
            componenlink.Landscape = true;
            componenlink.VerticalContentSplitting = VerticalContentSplitting.Smart;
            componenlink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            componenlink.Margins.Bottom = 3;
            componenlink.Margins.Top = 3;
            componenlink.Margins.Right = 3;
            componenlink.Margins.Left = 3;

            componenlink.CreateDocument();

            PrintTool pt = new PrintTool(componenlink.PrintingSystemBase);
            pt.ShowPreviewDialog();


        }
        public void ExportToExcel()
        {
          try
            {

                string pathToSave = "";
                string[] ext = null;
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Excel 97-2003 WorkBook|*.xls|Excel WorkBook|*.xlsx";
                    //dialog.FilterIndex = 2;
                    dialog.RestoreDirectory = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pathToSave = dialog.FileName;
                        ext = pathToSave.Split('.');
                    }
                }


                if (pathToSave == "") return;
                if (ext[1] == "xls")
                    bandedGridView1.ExportToXls(pathToSave);
                else if (ext[1] == "xlsx")
                    bandedGridView1.ExportToXlsx(pathToSave);

                System.Diagnostics.Process.Start(pathToSave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void sbThnAng_Click(object sender, EventArgs e)
        {
           // dataInisial = true;
           // getTindakLanjut();
        }

    }
}