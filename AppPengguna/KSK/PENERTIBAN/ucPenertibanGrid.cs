using System;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace AppPengguna.KSK.PENERTIBAN
{
    public partial class ucPenertibanGrid : UserControl
    {
        public SvcWasdalPenertibanSelect.WASDALSROW_WASDAL_PENERTIBAN dataTerpilih;
        //public SvcMonKorwilPenertibanA1.WASDALSROW_MON_KORWIL_PENERTIBAN dataTerpilih;
        
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

        public ucPenertibanGrid()
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
            //bandedGridView1.BestFitColumns();
        }

        private void gvGridSk_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            viewTerpilih = sender as GridView;
            if (e.FocusedRowHandle > -1)
            {
                dataTerpilih = (SvcWasdalPenertibanSelect.WASDALSROW_WASDAL_PENERTIBAN)viewTerpilih.GetRow(e.FocusedRowHandle);
            }
            else
            {
                dataTerpilih = null;
            }
        }

        #region Pencarian Data
        private string getFieldKolom(string judulKolom)
        {
            string kembalian = "";
           
            try
            {

            judulKolom = judulKolom.Replace(' ', '_');
            judulKolom = judulKolom.Replace('/', '_');
            if (judulKolom != "" && judulKolom.Trim() != "System.Collections.ArrayList")
            {
                kembalian = bandedGridView1.Columns.ColumnByName(judulKolom).FieldName;
            }
            return kembalian;
           
            }
            catch (Exception)
            {
                return kembalian;
           
            }
        }

        private void jumlahKolom()
        {
            teNamaKolom.Properties.Items.Clear();

            foreach (var item in bandedGridView1.Columns)
            {
                string caption = ((BandedGridColumn)item).Caption;
                if (caption != "NO")
                {
                    teNamaKolom.Properties.Items.Add(caption);
                }
            }
            teNamaKolom.Text = "";
        }

        private void gvGridSk_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (bandedGridView1.FocusedColumn.FieldName != "NUM")
            {
                teCari.Text = bandedGridView1.GetFocusedDisplayText();
                if (teCari.Text.Trim() != "")
                {
                    teNamaKolom.Text = bandedGridView1.FocusedColumn.ToString();
                    fieldDicari = bandedGridView1.FocusedColumn.FieldName;
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

        private void bandedGridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                var row = (SvcWasdalPenertibanSelect.WASDALSROW_WASDAL_PENERTIBAN)e.Row;
                if (e.IsGetData && e.Column == colPengunaan)
                {
                    if (row.BENTUK_PENERTIBAN.ToUpper() == "PENGGUNAAN")
                    {
                        e.Value = true;
                    }
                    else
                    {
                        e.Value = false;
                     }
               }else if (e.IsGetData && e.Column == colPemanfattan)
                {
                    if (row.BENTUK_PENERTIBAN.ToUpper() == "PEMANFAATAN")
                    {
                        e.Value = true;
                    }
                    else
                    {
                        e.Value = false;
                    }
                }else if (e.IsGetData && e.Column == colPemindatangan)
                {
                    if (row.BENTUK_PENERTIBAN.ToUpper() == "PEMINDAHTANGANAN")
                    {
                        e.Value = true;
                    }
                    else
                    {
                        e.Value = false;
                    }
                }
                //else if (e.IsGetData && e.Column == colTGL_LAPORAN)
                //{
                //    if (!row.TGL_LAPORAN.ToString().Contains("11/11/00"))
                //    {
                //        e.Value = row.TGL_LAPORAN;
                //    }
                
                //}
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
