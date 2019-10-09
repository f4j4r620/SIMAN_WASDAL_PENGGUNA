using System;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Threading;
using System.IO;

namespace AppPengguna.KKL.WL.MONLAPWASDAL
{
    public partial class ucMONLAPWASDALGrid : UserControl
    {
        public SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL dataTerpilih;
        
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
        public DateTime batasWaktu;
        public DetailDataSanksi detailSanksi;
        public DetailDataCatatan detailCatatan;
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm aktifkanForm;

        public ucMONLAPWASDALGrid()
        {
            InitializeComponent();
            jumlahKolom();
            konfigApp.setTahunAnggaran();
            batasWaktu = new DateTime(Convert.ToInt32(konfigApp.tahunAnggaran), 3, 31);
        }
          public delegate void AktifkanForm(string str);
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
            gvGridMONLAPWASDAL.BestFitColumns();
        }

        private void gvGridSk_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            viewTerpilih = sender as GridView;
            if (viewTerpilih.SelectedRowsCount > 0)
                dataTerpilih = (SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL)viewTerpilih.GetRow(e.FocusedRowHandle);
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

        public void AKtifkanForm(string str)
        {
            this.Enabled = true;
        }
        #endregion Thread



        #region Pencarian Data
        private string getFieldKolom(string judulKolom)
        {
            string kembalian = "";
            int jmlKolom = gvGridMONLAPWASDAL.Columns.Count;
            int indeksBaris = 0;
            for (int i = 0; i < jmlKolom; i++)
            {
                if (gvGridMONLAPWASDAL.Columns[i].Caption == judulKolom)
                {
                    kembalian = gvGridMONLAPWASDAL.Columns[i].FieldName;
                    break;
                }
                indeksBaris++;
            }
            return kembalian;
        }

        private void jumlahKolom()
        {
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvGridMONLAPWASDAL.Columns.Count;
            int indeksBaris = 0;
            for (int i = 0; i < jmlKolom; i++)
            {
                if (gvGridMONLAPWASDAL.Columns[i].FieldName != "NUM")
                {
                    teNamaKolom.Properties.Items.Insert(indeksBaris, gvGridMONLAPWASDAL.Columns[i].Caption);
                    indeksBaris++;
                }
            }
            teNamaKolom.Text = "";
        }

        private void gvGridSk_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gvGridMONLAPWASDAL.FocusedColumn.FieldName != "NUM")
            {
                teCari.Text = gvGridMONLAPWASDAL.GetFocusedDisplayText();
                if (teCari.Text.Trim() != "")
                {
                    teNamaKolom.Text = gvGridMONLAPWASDAL.FocusedColumn.ToString();
                    fieldDicari = gvGridMONLAPWASDAL.FocusedColumn.FieldName;
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
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '{1}%' ", this.fieldDicari, yangDicari);
                }
                else if (fieldDicari.Substring(0, 2) == "KD")
                {
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                }
                else
                    this.strCari = String.Format(" AND UPPER({0}) LIKE '{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                this.cariDataOnline(this.strCari, initModeLoad);
            }
        }
        #endregion

        #region Detail Data Grid
        private void gvGridSk_DoubleClick(object sender, EventArgs e)
        {
            var row = (SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL)gvGridMONLAPWASDAL.GetFocusedRow();
            if (gvGridMONLAPWASDAL.FocusedColumn == colCatatan || gvGridMONLAPWASDAL.FocusedColumn == colJML_SANKSI)
            {
                if (gvGridMONLAPWASDAL.FocusedColumn == colCatatan)
                {
                    detailCatatan(row.ID_MON_LAP + "", "SATKER : " + row.UR_SATKER);
                }
                if (gvGridMONLAPWASDAL.FocusedColumn == colJML_SANKSI)
                {
                    detailSanksi(row.ID_MON_LAP + "", "SATKER : " + row.UR_SATKER);
                }
                //}else if (gvGridMONLAPWASDAL.FocusedColumn == colSTATUS_KIRIM)
                //{
                //    detailDataTerpilih();
                //}
            }
            else
            {
                detailDataTerpilih();
            }
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

        private void gvGridMONLAPWASDAL_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL)e.Row;
            if (e.IsGetData && e.Column == colTGL_DITERIMA)
            {
                if (!row.TGL_DITERIMA.ToString().Contains("1000"))
                {
                    e.Value = row.TGL_DITERIMA;
                }
            }
            if (e.IsGetData && e.Column == colTGL_KIRIM)
            {
                if (!row.TGL_KIRIM.ToString().Contains("1000"))
                {
                    e.Value = row.TGL_KIRIM;
                }
            
            }
            if (e.IsGetData && e.Column == colTGL_SURAT)
            {
                if (!row.TGL_SURAT.ToString().Contains("1000"))
                {
                    e.Value = row.TGL_SURAT;
                }
            }
            if (e.IsGetData && e.Column == colIS_TEPAT_WAKTU)
            {
                // Ketepatan Waktu di cek dari tanggal kirim 
                // -1 < 31 Maret YYYY > 1
                //           0
                int daysMore = row.TGL_KIRIM.Value.CompareTo(batasWaktu);


                if (daysMore <= 0)
                {
                    e.Value = "TEPAT WAKTU";
                }
                if (daysMore > 0)
                {
                    e.Value = "TIDAK TEPAT WAKTU";
                }
                if (!row.TGL_DITERIMA.ToString().Contains("1000") && !row.TGL_KIRIM.ToString().Contains("1000"))
                {
                    DateTime date1 = Convert.ToDateTime(row.TGL_KIRIM);
                    DateTime date2 = new DateTime(row.TGL_KIRIM.Value.Year, 3, 31);
                    int result = date1.Date.CompareTo(date2.Date);
                    if (result <= 0) e.Value = "TEPAT WAKTU";
                    else e.Value = "TIDAK TEPAT WAKTU";
                }
                else
                {
                    e.Value = "";
                }
                #region old

                //if (row.IS_TEPAT_WAKTU != "-")
                //{
                //    if (row.IS_TEPAT_WAKTU == "Y")
                //    {
                //        e.Value = "TEPAT WAKTU";
                //    }
                //    if (row.IS_TEPAT_WAKTU == "N")
                //    {
                //        e.Value = "TIDAK TEPAT WAKTU";
                //    }
                //}
                //else
                //{
                //    e.Value = "";
                //}
                #endregion
            }

            if (e.IsGetData && e.Column == colSTATUS_KIRIM)
            {
                if (row.STATUS_KIRIM == "Y")
                {
                    e.Value = "Sudah";
                }
                else
                {
                    e.Value = "Belum";
                }
            }
        }

        private void repoView_Click(object sender, EventArgs e)
        {
            SetupFile("R");
        }

        #region File Upload and Download
        Thread myThread;
        SvcLapMonWasdalUploadFile.OutputParameters outputFile;
        SvcLapMonWasdalUploadFile.execute_pttClient clientFile;
        string statusFile = "";
        // statusFile == "R" , Download
        // statusFile == "C" , Upload

        private void MessageFail(string ex)
        {
            nonAktifkanprogressBar();
            this.Invoke(new AktifkanForm(aktifkanForm), "");
            MessageBox.Show(konfigApp.teksGagalAmbil + ":" + ex, konfigApp.judulGagalAmbil);

        }

        public void SetupFile(string mode)
        {
            try
            {
                this.Enabled = false;
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                statusFile = mode;
                SvcLapMonWasdalUploadFile.InputParameters parInp = new SvcLapMonWasdalUploadFile.InputParameters();
                parInp.P_SELECT = mode;
                parInp.P_ID_MON_LAP = dataTerpilih.ID_MON_LAP.ToString();
                clientFile = new SvcLapMonWasdalUploadFile.execute_pttClient();
                clientFile.Beginexecute(parInp, new AsyncCallback(getResultFile), null);
            }
            catch (Exception ex)
            {
                MessageFail(ex.Message);
            }
        }

        private void getResultFile(IAsyncResult result)
        {
            try
            {
                outputFile = clientFile.Endexecute(result);
                clientFile.Close();
                this.Invoke(new AktifkanForm(AKtifkanForm), "");
                this.Invoke(new ToggleProgressBar(toggleProgBarPu), "finisih");
                this.Invoke(new viewfile(ViewFile), outputFile);
            }
            catch (Exception ex)
            {
                MessageFail(ex.Message);
            }

        }
        AppPengguna.KKL.RSK.PU.FrmPuViewPdf frmPuViewpdf;
        private delegate void viewfile(SvcLapMonWasdalUploadFile.OutputParameters itemResult);

        private void ViewFile(SvcLapMonWasdalUploadFile.OutputParameters itemResult)
        {
            this.Invoke(new AktifkanForm(AKtifkanForm), "");
            if (itemResult.PO_RESULT == "Y")
            {
                    string appPath = AppDomain.CurrentDomain.BaseDirectory;
                    string nameFile = (itemResult.PO_NM_FILE.Contains(".pdf")) ? itemResult.PO_NM_FILE : itemResult.PO_NM_FILE + ".pdf";
                    string fileSave =     System.IO.Path.Combine(appPath, nameFile.Replace('/', '-'));
                    System.IO.File.WriteAllBytes(fileSave, itemResult.PO_FILE_DOK);
                    frmPuViewpdf = new RSK.PU.FrmPuViewPdf();
                    frmPuViewpdf.display(fileSave);
                    frmPuViewpdf.ShowDialog();
            }
            else
            {
                MessageBox.Show("File tidak ditemukan dalam data yang dipilih");
            }
        }

        #endregion

        public void ExportToExcel()
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

        


    }
}
