using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KKW.MONPNBP.SSBP
{
    public partial class ucGridPnbp : UserControl
    {
        SvcWasdalManfaatMonPnbp.WASDALSROW_MON_PNBP dataTerpilih;
        GridView viewTerpilih = null;
        public bool dataInisial = true;
        public ArrayList dsDataSource = null;
        public string fieldDicari = "";
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private bool initModeLoad = true;
        private string cariSebelumnya = "";
        private string strCari = "";
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        public CariDataOnline cariDataOnline;
        public DetailDataGrid detailDataGrid;
        FrmKoorKorwil frmkoorsatker;
             
        public ucGridPnbp()
        {
            InitializeComponent();
            jumlahKolom();
            frmkoorsatker = new FrmKoorKorwil();
        }

        public void displayData()
        {
            if (dataInisial == true)
            {
                gcPnbp.DataSource = null;
                gcPnbp.DataSource = dsDataSource;
            }
            else
            {
                gcPnbp.RefreshDataSource();
            }
        }

        private void gvPnbp_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            viewTerpilih = sender as GridView;
            if (viewTerpilih.SelectedRowsCount > 0)
                dataTerpilih = (SvcWasdalManfaatMonPnbp.WASDALSROW_MON_PNBP)viewTerpilih.GetRow(e.FocusedRowHandle);
            
        }

        #region Pencarian Data
        private string getFieldKolom(int indeksKolom)
        {
            if (indeksKolom > -1) return gvPnbp.Columns[indeksKolom].FieldName;
            else return "";
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvPnbp.Columns.Count;
            for (int i = 0; i < jmlKolom; i++)
            {
                daftarKolom.Add(gvPnbp.Columns[i].Caption);
                teNamaKolom.Properties.Items.Insert(i, gvPnbp.Columns[i].Caption);
                daftarFieldKolom.Add(gvPnbp.Columns[i].FieldName);
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvPnbp_ColumnFilterChanged(object sender, EventArgs e)
        {
            teCari.Text = gvPnbp.GetFocusedDisplayText();
            if (teCari.Text.Trim() != "")
            {
                teNamaKolom.Text = gvPnbp.FocusedColumn.ToString();
                fieldDicari = gvPnbp.FocusedColumn.FieldName;
            }
            else
            {
                teNamaKolom.Text = "";
                fieldDicari = "";
                this.strCari = "";
                if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            }
        }

        private void teNamaKolom_EditValueChanged(object sender, EventArgs e)
        {
            sbCariOnline.Enabled = true;
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            this.fieldDicari = this.getFieldKolom(teNamaKolom.SelectedIndex);
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
                this.strCari = String.Format(" AND UPPER({0}) LIKE '%{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                this.cariDataOnline(this.strCari, initModeLoad);
            }
        }
        #endregion

        private void gvPnbp_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gvPnbp_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

    }
}
