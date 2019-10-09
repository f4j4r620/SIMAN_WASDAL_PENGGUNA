namespace AppPengguna.KKW.WL.PENGGUNAAN.DETAILSATKER
{
    partial class ucBMN
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
            this.colKD_JNS_BMN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNIL_SBLM_SUSUT = new DevExpress.XtraGrid.Columns.GridColumn();
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
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKD_JNS_BMN,
            this.colNIL_SBLM_SUSUT});
            this.gridView.OptionsView.ShowFooter = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
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
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // BsMain
            // 
            this.BsMain.DataSource = typeof(AppPengguna.SvcSelectGunaA21.WASDALSROW_MON_GUNA_A21);
            // 
            // colKD_JNS_BMN
            // 
            this.colKD_JNS_BMN.AppearanceHeader.Options.UseTextOptions = true;
            this.colKD_JNS_BMN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKD_JNS_BMN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colKD_JNS_BMN.Caption = "JENIS BMN";
            this.colKD_JNS_BMN.FieldName = "NM_JNS_BMN";
            this.colKD_JNS_BMN.Name = "colKD_JNS_BMN";
            this.colKD_JNS_BMN.OptionsColumn.AllowEdit = false;
            this.colKD_JNS_BMN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "NM_JNS_BMN", "JUMLAH", "JUMLAH")});
            this.colKD_JNS_BMN.Visible = true;
            this.colKD_JNS_BMN.VisibleIndex = 0;
            // 
            // colNIL_SBLM_SUSUT
            // 
            this.colNIL_SBLM_SUSUT.AppearanceCell.Options.UseTextOptions = true;
            this.colNIL_SBLM_SUSUT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNIL_SBLM_SUSUT.AppearanceHeader.Options.UseTextOptions = true;
            this.colNIL_SBLM_SUSUT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNIL_SBLM_SUSUT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNIL_SBLM_SUSUT.Caption = "NILAI SEBELUM PENYUSUTAN";
            this.colNIL_SBLM_SUSUT.DisplayFormat.FormatString = "{0:n0}";
            this.colNIL_SBLM_SUSUT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNIL_SBLM_SUSUT.FieldName = "NIL_SBLM_SUSUT";
            this.colNIL_SBLM_SUSUT.Name = "colNIL_SBLM_SUSUT";
            this.colNIL_SBLM_SUSUT.OptionsColumn.AllowEdit = false;
            this.colNIL_SBLM_SUSUT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NIL_SBLM_SUSUT", "{0:n0}")});
            this.colNIL_SBLM_SUSUT.Visible = true;
            this.colNIL_SBLM_SUSUT.VisibleIndex = 1;
            // 
            // ucBMN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucBMN";
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

        private DevExpress.XtraGrid.Columns.GridColumn colKD_JNS_BMN;
        private DevExpress.XtraGrid.Columns.GridColumn colNIL_SBLM_SUSUT;

    }
}
