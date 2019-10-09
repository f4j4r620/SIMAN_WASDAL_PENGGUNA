using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using System.IO;

namespace AppPengguna.PU
{
    public partial class frmPuCariAset : Form
    {
        ArrayList daftarKolom;
        ArrayList daftarFieldKolom;
        public ArrayList datasource;
        public ArrayList datasourcesearch;
        private string fieldDicari = "";

        public frmPuCariAset()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        private string getFieldKolom(string judulKolom)
        {
            string kembalian = "";
            if (judulKolom != "" && judulKolom.Trim() != "System.Collections.ArrayList")
            {
                for (int i = 0; i < gvAset.Columns.Count; i++)
                {
                    if (gvAset.Columns[i].Caption == judulKolom)
                    {
                        kembalian = gvAset.Columns[i].FieldName;
                    }
                }
            }
            return kembalian;
        }

        private void jumlahKolom()
        {
            daftarKolom = new ArrayList();
            daftarFieldKolom = new ArrayList();
            teNamaKolom.Properties.Items.Clear();
            int jmlKolom = gvAset.Columns.Count;
            int indeksBaris = 0;
            for (int i = 0; i < jmlKolom; i++)
            {
                if (gvAset.Columns[i].FieldName != "NUM" && gvAset.Columns[i].FieldName != "NUMSpecified" && gvAset.Columns[i].FieldName != "KD_SATKER" && gvAset.Columns[i].FieldName != "UR_SATKER" && gvAset.Columns[i].FieldName != "NO_ASET")
                {
                    daftarKolom.Add(gvAset.Columns[i].Caption);
                    teNamaKolom.Properties.Items.Insert(indeksBaris, gvAset.Columns[i].Caption);
                    daftarFieldKolom.Add(gvAset.Columns[i].FieldName);
                    indeksBaris++;
                }
            }
            teNamaKolom.EditValue = daftarFieldKolom;
            teNamaKolom.Text = "";
        }

        private void frmPuAset_Load(object sender, EventArgs e)
        {
            jumlahKolom();
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcAset.DataSource = null;
            gcAset.DataSource = datasource;
        }

        private void bbiTutup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        string fileExtenstion = new FileInfo(exportFilePath).Extension;

                        switch (fileExtenstion)
                        {
                            case ".xls":
                                gcAset.ExportToXls(exportFilePath);
                                break;
                            case ".xlsx":
                                gcAset.ExportToXlsx(exportFilePath);
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
                MessageBox.Show(this, "Eksport grid gagal");
                throw;
            }
            
        }

        private void teNamaKolom_EditValueChanged(object sender, EventArgs e)
        {
            sbCariOnline.Enabled = true;
            this.fieldDicari = this.getFieldKolom(teNamaKolom.Text);
            teNupDari.Text = "";
            teNupSampai.Text = "";
            if (this.fieldDicari == "KD_BRG" || this.fieldDicari == "UR_SSKEL")
            {
                this.lciNupDari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciNupSampai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.lciNupDari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupSampai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void gvAset_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gvAset.FocusedColumn.FieldName != "NUM" && gvAset.FocusedColumn.FieldName != "NUMSpecified" && gvAset.FocusedColumn.FieldName != "KD_SATKER" && gvAset.FocusedColumn.FieldName != "UR_SATKER" && gvAset.FocusedColumn.FieldName != "NO_ASET")
            {
                teCari.Text = gvAset.GetFocusedDisplayText();
                if (teCari.Text.Trim() != "")
                {
                    teNamaKolom.Text = gvAset.FocusedColumn.ToString();
                    fieldDicari = gvAset.FocusedColumn.FieldName;
                }
                else
                {
                    teNamaKolom.Text = "";
                    fieldDicari = "";
                }
            }
        }

        private void sbCariOnline_Click(object sender, EventArgs e)
        {
            gvAset.ActiveFilterEnabled = true;
            //var test = gvAset.RowFilter.Where(x => x. >= 173 && x. <= 193).ToList();
            string rowfilter = string.Format("[{0}] = '{1}'", fieldDicari, teCari.Text);
            string rowfilterNUP = string.Format(" And [NO_ASET] Between('{0}','{1}')", teNupDari.Text, teNupSampai.Text);
            if (teNupDari.Text == "" && teNupSampai.Text == "")
            {
                gvAset.ActiveFilterString = rowfilter;
            }
            else
            {
                gvAset.ActiveFilterString = rowfilter + rowfilterNUP;
            }
            
        }

    }
}
