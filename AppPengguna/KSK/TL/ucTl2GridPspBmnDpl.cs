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
    public partial class ucTl2GridPspBmnDpl : UserControl
    {
        GridView viewTerpilih = null;
        public bool dataInisial = true;
        public ArrayList dsEselon1 = null;
        public string fieldDicari = "";
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private bool initModeLoad = true;
        private string cariSebelumnya = "";
        private string strCari = "";
        private ArrayList daftarKolom = null;
        private ArrayList daftarFieldKolom = null;
        public CariDataOnline cariDataOnline;

        public ucTl2GridPspBmnDpl()
        {
            InitializeComponent();
            jumlahKolom();
        }

        public void displayData()
        {
            if (dataInisial == true)
            {
                gcPspBmnDpl.DataSource = null;
                gcPspBmnDpl.DataSource = dsEselon1;
            }
            else
            {
                gcPspBmnDpl.RefreshDataSource();
            }
        }

        private void gvPspBmnDpl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        #region Pencarian Data
        private string getFieldKolom(int indeksKolom)
        {
            if (indeksKolom > -1) return gvPspBmnDpl.Columns[indeksKolom].FieldName;
            else return "";
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvPspBmnDpl.Columns.Count;
            for (int i = 0; i < jmlKolom; i++)
            {
                daftarKolom.Add(gvPspBmnDpl.Columns[i].Caption);
                teNamaKolom.Properties.Items.Insert(i, gvPspBmnDpl.Columns[i].Caption);
                daftarFieldKolom.Add(gvPspBmnDpl.Columns[i].FieldName);
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void gvPspBmnDpl_ColumnFilterChanged(object sender, EventArgs e)
        {
            teCari.Text = gvPspBmnDpl.GetFocusedDisplayText();
            if (teCari.Text.Trim() != "")
            {
                teNamaKolom.Text = gvPspBmnDpl.FocusedColumn.ToString();
                fieldDicari = gvPspBmnDpl.FocusedColumn.FieldName;
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
