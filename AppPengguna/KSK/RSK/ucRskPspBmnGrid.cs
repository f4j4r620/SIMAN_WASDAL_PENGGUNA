﻿using System;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;

namespace AppPengguna.KSK.RSK
{
    public partial class ucRskPspBmnGrid : UserControl
    {
        public SvcWasdalPSPBMNSkSelect.WASDALSROW_READ_WASDAL_PSP dataTerpilih;
        GridView viewTerpilih = null;
        public bool dataInisial = true;
        public ArrayList dsDataSource = null;
        public string fieldDicari = "";
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private bool initModeLoad = true;
        private string cariSebelumnya = "";
        private string strCari = "";
        public CariDataOnline cariDataOnline;
        public DetailDataGrid detailDataGrid;

        public ucRskPspBmnGrid()
        {
            InitializeComponent();
            jumlahKolom();
        }

        public void displayData()
        {
            if (dataInisial == true)
            {
                gcGridSk.DataSource = null;
                gcGridSk.DataSource = dsDataSource;
            }
            else
            {
                gcGridSk.RefreshDataSource();
            }
            gvGridPenertiban.BestFitColumns();
        }

        private void gvGridSk_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            viewTerpilih = sender as GridView;
            if (viewTerpilih.SelectedRowsCount > 0)
                dataTerpilih = (SvcWasdalPSPBMNSkSelect.WASDALSROW_READ_WASDAL_PSP)viewTerpilih.GetRow(e.FocusedRowHandle);
            
        }

        #region Pencarian Data
        private string getFieldKolom(string judulKolom)
        {
            judulKolom = judulKolom.Replace(' ', '_');
            judulKolom = judulKolom.Replace('/', '_');
            string kembalian = "";
            if (judulKolom != "" && judulKolom.Trim() != "System.Collections.ArrayList")
                kembalian = gvGridPenertiban.Columns.ColumnByName(judulKolom).FieldName;
            return kembalian;
        }

        private void jumlahKolom()
        {
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvGridPenertiban.Columns.Count;
            int indeksBaris = 0;
            for (int i = 0; i < jmlKolom; i++)
            {
                if (gvGridPenertiban.Columns[i].FieldName != "NUM")
                {
                    teNamaKolom.Properties.Items.Insert(indeksBaris, gvGridPenertiban.Columns[i].Caption);
                    indeksBaris++;
                }
            }
            teNamaKolom.Text = "";
        }

        private void gvGridSk_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gvGridPenertiban.FocusedColumn.FieldName != "NUM")
            {
                teCari.Text = gvGridPenertiban.GetFocusedDisplayText();
                if (teCari.Text.Trim() != "")
                {
                    teNamaKolom.Text = gvGridPenertiban.FocusedColumn.ToString();
                    fieldDicari = gvGridPenertiban.FocusedColumn.FieldName;
                }
                else
                {
                    teNamaKolom.Text = "";
                    fieldDicari = "";
                    this.strCari = "";
                    if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
                }
            }
        }

        private void teNamaKolom_EditValueChanged(object sender, EventArgs e)
        {
            sbCariOnline.Enabled = true;
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            this.fieldDicari = this.getFieldKolom(teNamaKolom.Text);
        }

        private void teCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            sbCariOnline.Enabled = true;
            if (teCari.Text.Trim() != "" && teNamaKolom.SelectedIndex == -1) teNamaKolom.SelectedIndex = 0;
        }

        private void sbCariOnline_Click(object sender, EventArgs e)
        {
            if (teCari.Text.Trim() != "" && teNamaKolom.Text != "")
            {
                if ((this.modeLoadData != "cari") || (cariSebelumnya != teCari.Text.Trim()))
                {
                    //this.dataInisial = true;
                    this.modeLoadData = "cari";
                    cariSebelumnya = teCari.Text.Trim();
                    this.initModeLoad = true;
                }
                else
                {
                    //this.dataInisial = false;
                    this.initModeLoad = false;
                }
                if (fieldDicari == "IS_TB")
                {
                    string yangDicari = "";
                    if (teCari.Text[0].ToString().ToUpper() == "T") yangDicari = "Y";
                    else if (teCari.Text[0].ToString().ToUpper() == "N") yangDicari = "N";
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '%{1}%' ", this.fieldDicari, yangDicari);
                }
                else if (fieldDicari.Substring(0, 2) == "KD")
                {
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                }
                else
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '%{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                this.cariDataOnline(this.strCari, initModeLoad);
            }
        }
        #endregion

        #region Detail Data Grid
        private void gvGridSk_DoubleClick(object sender, EventArgs e)
        {
            detailDataTerpilih();
        }

        private void gvGridSk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                detailDataTerpilih();
            }
        }

        private void detailDataTerpilih()
        {
            if (dataTerpilih != null)
            {
                detailDataGrid(null, null);
            }
        }
        #endregion

        public void ExportToExcel()
        {
            try
            {

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        string fileExtenstion = new FileInfo(exportFilePath).Extension;

                        switch (fileExtenstion)
                        {
                            case ".xls":
                                gcGridSk.ExportToXls(exportFilePath);
                                break;
                            case ".xlsx":
                                gcGridSk.ExportToXlsx(exportFilePath);
                                break;
                            case ".rtf":
                                gcGridSk.ExportToRtf(exportFilePath);
                                break;
                            case ".pdf":
                                gcGridSk.ExportToPdf(exportFilePath);
                                break;
                            case ".html":
                                gcGridSk.ExportToHtml(exportFilePath);
                                break;
                            case ".mht":
                                gcGridSk.ExportToMht(exportFilePath);
                                break;
                            default:
                                break;
                        }

                        if (File.Exists(exportFilePath))
                        {
                            try
                            {
                                //Try to open the file and let windows decide how to open it.
                                System.Diagnostics.Process.Start(exportFilePath);
                            }
                            catch
                            {
                                String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                                MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}