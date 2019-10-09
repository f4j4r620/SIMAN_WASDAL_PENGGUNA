namespace AppPengguna.KKW.WL.PENGGUNAAN.DETAILSATKER
{
    partial class ucTidakDigunakan
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colNUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKD_BRG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUR_SSKEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNO_ASET = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNIL_PEROLEHAN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colALAMAT = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BsMain)).BeginInit();
            this.SuspendLayout();
            this.layoutControl1.Controls.SetChildIndex(this.gridControl, 0);
            this.layoutControl1.Controls.SetChildIndex(this.BtnMore, 0);
            this.layoutControl1.Controls.SetChildIndex(this.BtnRefresh, 0);
            this.layoutControl1.Controls.SetChildIndex(this.labelControl1, 0);
            this.layoutControl1.Controls.SetChildIndex(this.BtnBack, 0);
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNUM,
            this.colKD_BRG,
            this.colUR_SSKEL,
            this.colNO_ASET,
            this.colNIL_PEROLEHAN,
            this.colALAMAT});
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.ShowFooter = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnMore
            // 
            this.BtnMore.Click += new System.EventHandler(this.BtnMore_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.ShowInCustomizationForm = false;
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.ShowInCustomizationForm = false;
            this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.ShowInCustomizationForm = false;
            this.emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // BsMain
            // 
            this.BsMain.DataSource = typeof(AppPengguna.SvcSelectGunaA23.WASDALSROW_MON_GUNA_A23);
            // 
            // colNUM
            // 
            this.colNUM.AppearanceHeader.Options.UseTextOptions = true;
            this.colNUM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNUM.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNUM.Caption = "NO";
            this.colNUM.FieldName = "NUM";
            this.colNUM.Name = "colNUM";
            this.colNUM.OptionsColumn.AllowEdit = false;
            this.colNUM.Visible = true;
            this.colNUM.VisibleIndex = 0;
            // 
            // colKD_BRG
            // 
            this.colKD_BRG.AppearanceHeader.Options.UseTextOptions = true;
            this.colKD_BRG.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKD_BRG.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colKD_BRG.Caption = "KODE BARANG";
            this.colKD_BRG.FieldName = "KD_BRG";
            this.colKD_BRG.Name = "colKD_BRG";
            this.colKD_BRG.OptionsColumn.AllowEdit = false;
            this.colKD_BRG.Visible = true;
            this.colKD_BRG.VisibleIndex = 1;
            // 
            // colUR_SSKEL
            // 
            this.colUR_SSKEL.AppearanceHeader.Options.UseTextOptions = true;
            this.colUR_SSKEL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUR_SSKEL.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colUR_SSKEL.Caption = "URAIAN BARANG";
            this.colUR_SSKEL.FieldName = "UR_SSKEL";
            this.colUR_SSKEL.Name = "colUR_SSKEL";
            this.colUR_SSKEL.OptionsColumn.AllowEdit = false;
            this.colUR_SSKEL.Visible = true;
            this.colUR_SSKEL.VisibleIndex = 2;
            // 
            // colNO_ASET
            // 
            this.colNO_ASET.AppearanceHeader.Options.UseTextOptions = true;
            this.colNO_ASET.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNO_ASET.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNO_ASET.Caption = "NUP";
            this.colNO_ASET.FieldName = "NO_ASET";
            this.colNO_ASET.Name = "colNO_ASET";
            this.colNO_ASET.OptionsColumn.AllowEdit = false;
            this.colNO_ASET.Visible = true;
            this.colNO_ASET.VisibleIndex = 3;
            // 
            // colNIL_PEROLEHAN
            // 
            this.colNIL_PEROLEHAN.AppearanceCell.Options.UseTextOptions = true;
            this.colNIL_PEROLEHAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNIL_PEROLEHAN.AppearanceHeader.Options.UseTextOptions = true;
            this.colNIL_PEROLEHAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNIL_PEROLEHAN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNIL_PEROLEHAN.Caption = "NILAI PEROLEHAN";
            this.colNIL_PEROLEHAN.DisplayFormat.FormatString = "{0:n0}";
            this.colNIL_PEROLEHAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNIL_PEROLEHAN.FieldName = "NIL_PEROLEHAN";
            this.colNIL_PEROLEHAN.Name = "colNIL_PEROLEHAN";
            this.colNIL_PEROLEHAN.OptionsColumn.AllowEdit = false;
            this.colNIL_PEROLEHAN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NIL_PEROLEHAN", "{0:n0}")});
            this.colNIL_PEROLEHAN.Visible = true;
            this.colNIL_PEROLEHAN.VisibleIndex = 4;
            // 
            // colALAMAT
            // 
            this.colALAMAT.AppearanceHeader.Options.UseTextOptions = true;
            this.colALAMAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colALAMAT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colALAMAT.Caption = "ALAMAT";
            this.colALAMAT.FieldName = "ALAMAT";
            this.colALAMAT.Name = "colALAMAT";
            this.colALAMAT.OptionsColumn.AllowEdit = false;
            this.colALAMAT.Visible = true;
            this.colALAMAT.VisibleIndex = 5;
            // 
            // ucTidakDigunakan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucTidakDigunakan";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BsMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colNUM;
        private DevExpress.XtraGrid.Columns.GridColumn colKD_BRG;
        private DevExpress.XtraGrid.Columns.GridColumn colUR_SSKEL;
        private DevExpress.XtraGrid.Columns.GridColumn colNO_ASET;
        private DevExpress.XtraGrid.Columns.GridColumn colNIL_PEROLEHAN;
        private DevExpress.XtraGrid.Columns.GridColumn colALAMAT;

    }
}
