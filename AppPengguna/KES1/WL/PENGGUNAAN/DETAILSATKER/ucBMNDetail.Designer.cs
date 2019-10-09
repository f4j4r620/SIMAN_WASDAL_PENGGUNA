namespace AppPengguna.KES1.WL.PENGGUNAAN.DETAILSATKER
{
    partial class ucBMNDetail
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.colKD_BRG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUR_SSKEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNO_ASET = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNIL_PEROLEHAN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNIL_SBLM_SUSUT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVERIFIKASI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.beVerif = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMasterAset = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colNUM = new DevExpress.XtraGrid.Columns.GridColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.beVerif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colMasterAset)).BeginInit();
            this.SuspendLayout();
            this.layoutControl1.Controls.SetChildIndex(this.gridControl, 0);
            this.layoutControl1.Controls.SetChildIndex(this.BtnMore, 0);
            this.layoutControl1.Controls.SetChildIndex(this.BtnRefresh, 0);
            this.layoutControl1.Controls.SetChildIndex(this.labelControl1, 0);
            this.layoutControl1.Controls.SetChildIndex(this.BtnBack, 0);
            // 
            // gridControl
            // 
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.colMasterAset,
            this.beVerif});
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNUM,
            this.colKD_BRG,
            this.colUR_SSKEL,
            this.colNO_ASET,
            this.colNIL_PEROLEHAN,
            this.colNIL_SBLM_SUSUT,
            this.colVERIFIKASI,
            this.gridColumn1});
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnMore
            // 
            this.BtnMore.Click += new System.EventHandler(this.BtnMore_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BsMain
            // 
            this.BsMain.DataSource = typeof(AppPengguna.SvcSelectGunaA211.WASDALSROW_MON_GUNA_A211);
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
            this.colNO_ASET.AppearanceCell.Options.UseTextOptions = true;
            this.colNO_ASET.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNO_ASET.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
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
            this.colNIL_PEROLEHAN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNIL_PEROLEHAN.AppearanceHeader.Options.UseTextOptions = true;
            this.colNIL_PEROLEHAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNIL_PEROLEHAN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNIL_PEROLEHAN.Caption = "NILAI PEROLEHAN";
            this.colNIL_PEROLEHAN.DisplayFormat.FormatString = "{0:n0}";
            this.colNIL_PEROLEHAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNIL_PEROLEHAN.FieldName = "NIL_PEROLEHAN";
            this.colNIL_PEROLEHAN.Name = "colNIL_PEROLEHAN";
            this.colNIL_PEROLEHAN.OptionsColumn.AllowEdit = false;
            this.colNIL_PEROLEHAN.Visible = true;
            this.colNIL_PEROLEHAN.VisibleIndex = 4;
            this.colNIL_PEROLEHAN.Width = 100;
            // 
            // colNIL_SBLM_SUSUT
            // 
            this.colNIL_SBLM_SUSUT.AppearanceCell.Options.UseTextOptions = true;
            this.colNIL_SBLM_SUSUT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNIL_SBLM_SUSUT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNIL_SBLM_SUSUT.AppearanceHeader.Options.UseTextOptions = true;
            this.colNIL_SBLM_SUSUT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNIL_SBLM_SUSUT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNIL_SBLM_SUSUT.Caption = "NILAI SEBELUM PENYUSUTAN";
            this.colNIL_SBLM_SUSUT.DisplayFormat.FormatString = "{0:n0}";
            this.colNIL_SBLM_SUSUT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNIL_SBLM_SUSUT.FieldName = "NIL_SBLM_SUSUT";
            this.colNIL_SBLM_SUSUT.Name = "colNIL_SBLM_SUSUT";
            this.colNIL_SBLM_SUSUT.OptionsColumn.AllowEdit = false;
            this.colNIL_SBLM_SUSUT.Visible = true;
            this.colNIL_SBLM_SUSUT.VisibleIndex = 5;
            this.colNIL_SBLM_SUSUT.Width = 104;
            // 
            // colVERIFIKASI
            // 
            this.colVERIFIKASI.AppearanceHeader.Options.UseTextOptions = true;
            this.colVERIFIKASI.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVERIFIKASI.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colVERIFIKASI.Caption = "VERIFIKASI";
            this.colVERIFIKASI.ColumnEdit = this.beVerif;
            this.colVERIFIKASI.FieldName = "VERIFIKASI";
            this.colVERIFIKASI.Name = "colVERIFIKASI";
            this.colVERIFIKASI.Visible = true;
            this.colVERIFIKASI.VisibleIndex = 6;
            // 
            // beVerif
            // 
            this.beVerif.AutoHeight = false;
            this.beVerif.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.beVerif.Name = "beVerif";
            this.beVerif.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.beVerif.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beVerif_ButtonClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridColumn1.Caption = "MASTER ASET";
            this.gridColumn1.ColumnEdit = this.colMasterAset;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 7;
            this.gridColumn1.Width = 87;
            // 
            // colMasterAset
            // 
            this.colMasterAset.AutoHeight = false;
            this.colMasterAset.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "View", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.colMasterAset.Name = "colMasterAset";
            this.colMasterAset.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.colMasterAset.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.colMasterAset_ButtonClick);
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
            // ucBMNDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucBMNDetail";
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
            ((System.ComponentModel.ISupportInitialize)(this.beVerif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colMasterAset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit colMasterAset;
        private DevExpress.XtraGrid.Columns.GridColumn colNUM;
        private DevExpress.XtraGrid.Columns.GridColumn colKD_BRG;
        private DevExpress.XtraGrid.Columns.GridColumn colUR_SSKEL;
        private DevExpress.XtraGrid.Columns.GridColumn colNO_ASET;
        private DevExpress.XtraGrid.Columns.GridColumn colNIL_PEROLEHAN;
        private DevExpress.XtraGrid.Columns.GridColumn colNIL_SBLM_SUSUT;
        private DevExpress.XtraGrid.Columns.GridColumn colVERIFIKASI;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit beVerif;


    }
}
