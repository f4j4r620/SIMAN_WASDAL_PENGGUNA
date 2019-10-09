using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.AST.ATB
{
    public partial class ucIdentitasATB : DevExpress.XtraEditors.XtraUserControl
    {
        protected DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn kolomBanded;
        protected DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand;
        public BindingSource binder;
        private int jml_width;
        public ucIdentitasATB()
        {
            InitializeComponent();
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcATBPerkapSelect.BPSIMANSROW_KTWJD_PERLENGKAP);
            this.gcPerlengkapan.DataSource = binder;
           
            this.setKolom("No", "NUM", "NUM", 0, false, 80);
            this.setKolom("Perlengkapan", "NM_PERLENGKAP", "NM_PERLENGKAP", 1, false);
            this.setKolom("Jumlah", "JUMLAH", "JUMLAH", 2, false);
            this.gvPerlengkapan.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.setKolom("Ket", "KET", "KET", 3, false);
            
        }

        private void splitContainerControl1_Resize(object sender, EventArgs e)
        {
            this.splitContainerControl1.SplitterPosition = (int)((this.Width / 2) + 20);
        }

        private void layoutControlGroup1_Click(object sender, EventArgs e)
        {
            this.Parent.Parent.Focus();
        }

        private void teNilaiPerolehan_Properties_ValueChanged(object sender, EventArgs e)
        {
            this.teNilaiSebelumPenyusutan.Value = this.teNilaiPerolehan.Value + this.teNilaiMutasi.Value;
        }

        private void teNilaiMutasi_Properties_ValueChanged(object sender, EventArgs e)
        {
            this.teNilaiSebelumPenyusutan.Value = this.teNilaiPerolehan.Value + this.teNilaiMutasi.Value;
        }

        private void teNilaiSebelumPenyusutan_Properties_ValueChanged(object sender, EventArgs e)
        {
            this.teNilaiBuku.Value = this.teNilaiSebelumPenyusutan.Value - this.teNilaiPenyusutan.Value;
        }

        private void teNilaiPenyusutan_Properties_ValueChanged(object sender, EventArgs e)
        {
            this.teNilaiBuku.Value = this.teNilaiSebelumPenyusutan.Value - this.teNilaiPenyusutan.Value;
        }


        protected int setKolom(string caption, string fieldName, string Name, Int32 urutan, bool setDdItem = false, int _width = 120, string type = "string", bool hide = false, int? idxParent = null)
        {
            //this.kolom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.kolomBanded = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.kolomBanded.AutoFillDown = true;
            this.kolomBanded.Caption = caption;
            this.kolomBanded.FieldName = fieldName;
            this.kolomBanded.Name = Name;
            this.kolomBanded.OptionsColumn.AllowFocus = false;

            //this.kolom.Width = _width;
            if (hide == true)
                this.kolomBanded.Visible = false;
            else
                this.kolomBanded.Visible = true;
            this.kolomBanded.AppearanceHeader.Options.UseTextOptions = true;
            this.kolomBanded.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.kolomBanded.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            if (type == "date")
            {
                this.kolomBanded.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                this.kolomBanded.DisplayFormat.FormatString = "dd/MM/yyyy";
                this.kolomBanded.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            else if (type == "number")
            {
                this.kolomBanded.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.kolomBanded.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                this.kolomBanded.DisplayFormat.FormatString = "n";
            }
            else if (type == "integer")
            {
                this.kolomBanded.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.kolomBanded.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                this.kolomBanded.DisplayFormat.FormatString = "n0";
            }
            else
            {
                this.kolomBanded.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }

            this.jml_width += this.kolomBanded.Width;
            this.gvPerlengkapan.Columns.Add(this.kolomBanded);




            if (setDdItem)
            {
                //this.LuKolomItems.Items.Add(caption);

            }

            this.gvPerlengkapan.Columns[Name].BestFit();

            if (idxParent == null)
            {


                this.gridBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                this.gridBand.Caption = caption.ToUpper();

                this.gridBand.Name = "gb" + caption;
                this.gridBand.VisibleIndex = urutan;
                this.gridBand.Columns.Add(this.kolomBanded);
                this.gridBand.Visible = this.kolomBanded.Visible;

                this.gvPerlengkapan.Bands.Add(this.gridBand);
            }
            else
            {
                this.kolomBanded.RowIndex = 1;
                this.gvPerlengkapan.Bands[(int)idxParent].Columns.Add(this.kolomBanded);

            }
            return jml_width;

        }







    }



}
