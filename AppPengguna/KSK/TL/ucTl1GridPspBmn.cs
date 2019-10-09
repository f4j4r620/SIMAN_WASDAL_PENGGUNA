using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KSK.TL
{
    public partial class ucTl1GridPspBmn : UserControl
    {
        GridView viewTerpilih = null;
        public bool dataInisialEselon1 = true;
        public ArrayList dsEselon1 = null;
        public string fieldDicari = "";
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private bool initModeLoad = true;
        private string cariSebelumnya = "";
        private string strCari = "";
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        public CariDataOnline cariDataOnline;

        public ucTl1GridPspBmn()
        {
            InitializeComponent();
            jumlahKolom();
        }

        public void displayData()
        {
            if (dataInisialEselon1 == true)
            {
                gcTanah.DataSource = null;
                gcTanah.DataSource = dsEselon1;
            }
            else
            {
                gcTanah.RefreshDataSource();
            }
        }

        private void gvTanah_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            viewTerpilih = sender as GridView;
            //if (viewTerpilih.SelectedRowsCount > 0) e1Pilihan = (SvcEselon1Select.BPSIMANSROW_R_ESELON1)viewTerpilih.GetRow(e.FocusedRowHandle);
        }

        #region Pencarian Data
        private string getFieldKolom(int indeksKolom)
        {
            if (indeksKolom > -1) return gvTanah.Columns[indeksKolom].FieldName;
            else return "";
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvTanah.Columns.Count;
            for (int i = 0; i < jmlKolom; i++)
            {
                daftarKolom.Add(gvTanah.Columns[i].Caption);
                teNamaKolom.Properties.Items.Insert(i, gvTanah.Columns[i].Caption);
                daftarFieldKolom.Add(gvTanah.Columns[i].FieldName);
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvTanah_ColumnFilterChanged(object sender, EventArgs e)
        {
            teCari.Text = gvTanah.GetFocusedDisplayText();
            if (teCari.Text.Trim() != "")
            {
                teNamaKolom.Text = gvTanah.FocusedColumn.ToString();
                fieldDicari = gvTanah.FocusedColumn.FieldName;
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
            if (teCari.Text.Trim() != "")
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
                this.strCari = String.Format(" UPPER({0}) LIKE '%{1}%' ", this.fieldDicari, teCari.Text.Trim().ToUpper());
                this.cariDataOnline(this.strCari, initModeLoad);
            }
        }
        #endregion
    }
}
