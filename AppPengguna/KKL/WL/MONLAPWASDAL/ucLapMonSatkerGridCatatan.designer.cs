namespace AppPengguna.KKL.WL.MONLAPWASDAL
{
    partial class ucLapMonSatkerGridCatatan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLapMonSatkerGridCatatan));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gcPgnBrg = new DevExpress.XtraGrid.GridControl();
            this.bsPgnBrg = new System.Windows.Forms.BindingSource();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTHN_ANG1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNO_SURAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTGL_SURAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDokumen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoNilai = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repoButtonView = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPgnBrg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPgnBrg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoNilai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoButtonView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.BtnBack);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.gcPgnBrg);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1058, 512);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(283, 16);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "LAPORAN MONITORING WASDAL - CATATAN ";
            // 
            // BtnBack
            // 
            this.BtnBack.Image = ((System.Drawing.Image)(resources.GetObject("BtnBack.Image")));
            this.BtnBack.Location = new System.Drawing.Point(12, 52);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(88, 22);
            this.BtnBack.StyleController = this.layoutControl1;
            this.BtnBack.TabIndex = 9;
            this.BtnBack.Text = "Kembali";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl1.Location = new System.Drawing.Point(12, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 16);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "labelControl1";
            // 
            // gcPgnBrg
            // 
            this.gcPgnBrg.DataSource = this.bsPgnBrg;
            this.gcPgnBrg.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcPgnBrg.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcPgnBrg.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcPgnBrg.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcPgnBrg.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcPgnBrg.Location = new System.Drawing.Point(12, 78);
            this.gcPgnBrg.MainView = this.gridView1;
            this.gcPgnBrg.Name = "gcPgnBrg";
            this.gcPgnBrg.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoNilai,
            this.repoButtonView});
            this.gcPgnBrg.Size = new System.Drawing.Size(1034, 422);
            this.gcPgnBrg.TabIndex = 4;
            this.gcPgnBrg.UseEmbeddedNavigator = true;
            this.gcPgnBrg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTHN_ANG1,
            this.colNO_SURAT,
            this.colTGL_SURAT,
            this.colDokumen});
            this.gridView1.GridControl = this.gcPgnBrg;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // colTHN_ANG1
            // 
            this.colTHN_ANG1.AppearanceCell.Options.UseTextOptions = true;
            this.colTHN_ANG1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTHN_ANG1.AppearanceHeader.Options.UseTextOptions = true;
            this.colTHN_ANG1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTHN_ANG1.Caption = "Tahun Anggaran";
            this.colTHN_ANG1.FieldName = "THN_ANG";
            this.colTHN_ANG1.Name = "colTHN_ANG1";
            this.colTHN_ANG1.OptionsColumn.AllowEdit = false;
            this.colTHN_ANG1.Visible = true;
            this.colTHN_ANG1.VisibleIndex = 0;
            this.colTHN_ANG1.Width = 160;
            // 
            // colNO_SURAT
            // 
            this.colNO_SURAT.AppearanceHeader.Options.UseTextOptions = true;
            this.colNO_SURAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNO_SURAT.Caption = "No. Surat";
            this.colNO_SURAT.FieldName = "NO_SURAT";
            this.colNO_SURAT.Name = "colNO_SURAT";
            this.colNO_SURAT.OptionsColumn.AllowEdit = false;
            this.colNO_SURAT.Visible = true;
            this.colNO_SURAT.VisibleIndex = 1;
            this.colNO_SURAT.Width = 105;
            // 
            // colTGL_SURAT
            // 
            this.colTGL_SURAT.AppearanceCell.Options.UseTextOptions = true;
            this.colTGL_SURAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTGL_SURAT.AppearanceHeader.Options.UseTextOptions = true;
            this.colTGL_SURAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTGL_SURAT.Caption = "Tanggal Surat";
            this.colTGL_SURAT.FieldName = "TGL_SURAT";
            this.colTGL_SURAT.Name = "colTGL_SURAT";
            this.colTGL_SURAT.OptionsColumn.AllowEdit = false;
            this.colTGL_SURAT.Visible = true;
            this.colTGL_SURAT.VisibleIndex = 2;
            this.colTGL_SURAT.Width = 175;
            // 
            // colDokumen
            // 
            this.colDokumen.AppearanceHeader.Options.UseTextOptions = true;
            this.colDokumen.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDokumen.Caption = "Dokumen";
            this.colDokumen.FieldName = "CATATAN";
            this.colDokumen.Name = "colDokumen";
            this.colDokumen.OptionsColumn.AllowEdit = false;
            this.colDokumen.Visible = true;
            this.colDokumen.VisibleIndex = 3;
            this.colDokumen.Width = 69;
            // 
            // repoNilai
            // 
            this.repoNilai.AutoHeight = false;
            this.repoNilai.Mask.EditMask = "n0";
            this.repoNilai.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repoNilai.Name = "repoNilai";
            // 
            // repoButtonView
            // 
            this.repoButtonView.AutoHeight = false;
            this.repoButtonView.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "View", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repoButtonView.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.repoButtonView.Name = "repoButtonView";
            this.repoButtonView.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repoButtonView.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repoButtonView_ButtonClick);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1058, 512);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcPgnBrg;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 66);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1038, 426);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1038, 20);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.BtnBack;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(92, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(92, 40);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(946, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.labelControl2;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(1038, 20);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // ucLapMonSatkerGridCatatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucLapMonSatkerGridCatatan";
            this.Size = new System.Drawing.Size(1058, 512);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPgnBrg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPgnBrg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoNilai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoButtonView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repoNilai;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colTHN_ANG1;
        private DevExpress.XtraGrid.Columns.GridColumn colNO_SURAT;
        private DevExpress.XtraGrid.Columns.GridColumn colTGL_SURAT;
        private DevExpress.XtraGrid.Columns.GridColumn colDokumen;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoButtonView;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton BtnBack;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        public DevExpress.XtraGrid.GridControl gcPgnBrg;
        public System.Windows.Forms.BindingSource bsPgnBrg;
    }
}
