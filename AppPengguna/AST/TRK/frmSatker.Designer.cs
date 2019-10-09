namespace AppPengguna.AST.TRK
{
    partial class frmSatker
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSatker));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcSatker = new DevExpress.XtraGrid.GridControl();
            this.gvSatker = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoUrSatker = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiPilih = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMore = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.beMarqueeBar = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.sbCariOnline = new DevExpress.XtraEditors.SimpleButton();
            this.teKeyword = new DevExpress.XtraEditors.TextEdit();
            this.leColumn = new DevExpress.XtraEditors.LookUpEdit();
            this.controlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSatker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSatker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoUrSatker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKeyword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leColumn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcSatker);
            this.layoutControl1.Controls.Add(this.sbCariOnline);
            this.layoutControl1.Controls.Add(this.teKeyword);
            this.layoutControl1.Controls.Add(this.leColumn);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.controlGroup;
            this.layoutControl1.Size = new System.Drawing.Size(681, 341);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcSatker
            // 
            this.gcSatker.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.gcSatker.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcSatker.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.gcSatker.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcSatker.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcSatker.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcSatker.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcSatker.Location = new System.Drawing.Point(12, 38);
            this.gcSatker.MainView = this.gvSatker;
            this.gcSatker.MenuManager = this.barManager1;
            this.gcSatker.Name = "gcSatker";
            this.gcSatker.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoUrSatker});
            this.gcSatker.Size = new System.Drawing.Size(657, 291);
            this.gcSatker.TabIndex = 7;
            this.gcSatker.UseEmbeddedNavigator = true;
            this.gcSatker.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSatker});
            // 
            // gvSatker
            // 
            this.gvSatker.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvSatker.GridControl = this.gcSatker;
            this.gvSatker.Name = "gvSatker";
            this.gvSatker.OptionsBehavior.Editable = false;
            this.gvSatker.OptionsBehavior.ReadOnly = true;
            this.gvSatker.OptionsView.RowAutoHeight = true;
            this.gvSatker.OptionsView.ShowAutoFilterRow = true;
            this.gvSatker.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvSatker_FocusedRowChanged);
            this.gvSatker.DoubleClick += new System.EventHandler(this.gvSatker_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "NO";
            this.gridColumn1.FieldName = "NUM";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 33;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "KODE SATKER";
            this.gridColumn2.FieldName = "KD_SATKER";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 148;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "URAIAN SATKER";
            this.gridColumn3.ColumnEdit = this.repoUrSatker;
            this.gridColumn3.FieldName = "UR_SATKER";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 351;
            // 
            // repoUrSatker
            // 
            this.repoUrSatker.Name = "repoUrSatker";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.beMarqueeBar,
            this.bbiRefresh,
            this.bbiPilih,
            this.bbiMore,
            this.bbiClose});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 7;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemMarqueeProgressBar1});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPilih),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMore),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClose)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbiPilih
            // 
            this.bbiPilih.Caption = "Pilih";
            this.bbiPilih.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiPilih.Glyph")));
            this.bbiPilih.Id = 3;
            this.bbiPilih.Name = "bbiPilih";
            this.bbiPilih.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiPilih.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPilih_ItemClick);
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "Refresh";
            this.bbiRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.Glyph")));
            this.bbiRefresh.Id = 2;
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
            // 
            // bbiMore
            // 
            this.bbiMore.Caption = "Ambil Data";
            this.bbiMore.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiMore.Glyph")));
            this.bbiMore.Id = 4;
            this.bbiMore.Name = "bbiMore";
            this.bbiMore.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiMore.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMore_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "Tutup";
            this.bbiClose.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiClose.Glyph")));
            this.bbiClose.Id = 5;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.beMarqueeBar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // beMarqueeBar
            // 
            this.beMarqueeBar.Caption = "barEditItem1";
            this.beMarqueeBar.Edit = this.repositoryItemMarqueeProgressBar1;
            this.beMarqueeBar.Id = 1;
            this.beMarqueeBar.Name = "beMarqueeBar";
            this.beMarqueeBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.beMarqueeBar.Width = 125;
            // 
            // repositoryItemMarqueeProgressBar1
            // 
            this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(681, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 381);
            this.barDockControlBottom.Size = new System.Drawing.Size(681, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 341);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(681, 40);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 341);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // sbCariOnline
            // 
            this.sbCariOnline.Image = ((System.Drawing.Image)(resources.GetObject("sbCariOnline.Image")));
            this.sbCariOnline.Location = new System.Drawing.Point(573, 12);
            this.sbCariOnline.Name = "sbCariOnline";
            this.sbCariOnline.Size = new System.Drawing.Size(96, 22);
            this.sbCariOnline.StyleController = this.layoutControl1;
            this.sbCariOnline.TabIndex = 6;
            this.sbCariOnline.Text = "Cari Online";
            this.sbCariOnline.Click += new System.EventHandler(this.sbCariOnline_Click);
            // 
            // teKeyword
            // 
            this.teKeyword.Location = new System.Drawing.Point(359, 12);
            this.teKeyword.MenuManager = this.barManager1;
            this.teKeyword.Name = "teKeyword";
            this.teKeyword.Size = new System.Drawing.Size(210, 20);
            this.teKeyword.StyleController = this.layoutControl1;
            this.teKeyword.TabIndex = 5;
            this.teKeyword.EditValueChanged += new System.EventHandler(this.teKeyword_EditValueChanged);
            // 
            // leColumn
            // 
            this.leColumn.Location = new System.Drawing.Point(87, 12);
            this.leColumn.MenuManager = this.barManager1;
            this.leColumn.Name = "leColumn";
            this.leColumn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leColumn.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VALUE", "VALUE", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DISPLAY", 40, "NAMA KOLOM")});
            this.leColumn.Properties.DisplayMember = "DISPLAY";
            this.leColumn.Properties.NullText = "";
            this.leColumn.Properties.ValueMember = "VALUE";
            this.leColumn.Size = new System.Drawing.Size(207, 20);
            this.leColumn.StyleController = this.layoutControl1;
            this.leColumn.TabIndex = 4;
            this.leColumn.EditValueChanged += new System.EventHandler(this.leColumn_EditValueChanged);
            // 
            // controlGroup
            // 
            this.controlGroup.CustomizationFormText = "layoutControlGroup1";
            this.controlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.controlGroup.GroupBordersVisible = false;
            this.controlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem4});
            this.controlGroup.Location = new System.Drawing.Point(0, 0);
            this.controlGroup.Name = "controlGroup";
            this.controlGroup.Size = new System.Drawing.Size(681, 341);
            this.controlGroup.Text = "controlGroup";
            this.controlGroup.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.leColumn;
            this.layoutControlItem1.CustomizationFormText = "Kolom Pencarian";
            this.layoutControlItem1.Location = new System.Drawing.Point(14, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(272, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(272, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(272, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Nama Kolom";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teKeyword;
            this.layoutControlItem2.CustomizationFormText = "Kata Kunci";
            this.layoutControlItem2.Location = new System.Drawing.Point(286, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(275, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(275, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(275, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Kata Kunci";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbCariOnline;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(561, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(14, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcSatker;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(661, 295);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // frmSatker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 404);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimizeBox = false;
            this.Name = "frmSatker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Satker";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSatker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSatker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoUrSatker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKeyword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leColumn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup controlGroup;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem beMarqueeBar;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem bbiRefresh;
        private DevExpress.XtraBars.BarButtonItem bbiPilih;
        private DevExpress.XtraBars.BarButtonItem bbiMore;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraEditors.TextEdit teKeyword;
        private DevExpress.XtraEditors.LookUpEdit leColumn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbCariOnline;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.GridControl gcSatker;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSatker;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repoUrSatker;
    }
}