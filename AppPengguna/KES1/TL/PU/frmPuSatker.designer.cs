namespace AppPengguna.KES1.TL.PU
{
    partial class frmPuSatker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPuSatker));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.teCari = new DevExpress.XtraEditors.TextEdit();
            this.sbCariOnline = new DevExpress.XtraEditors.SimpleButton();
            this.teNamaKolom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gcSatker = new DevExpress.XtraGrid.GridControl();
            this.gvSatker = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KODE_KL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NAMA_KL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KODE_ESELON_I = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NAMA_ESELON_I = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KODE_KORWIL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NAMA_KORWIL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KODE_SATKER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NAMA_SATKER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMoreData = new DevExpress.XtraBars.BarButtonItem();
            this.bbiTutup = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teCari.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNamaKolom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSatker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSatker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teCari);
            this.layoutControl1.Controls.Add(this.sbCariOnline);
            this.layoutControl1.Controls.Add(this.teNamaKolom);
            this.layoutControl1.Controls.Add(this.gcSatker);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(742, 355);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teCari
            // 
            this.teCari.Location = new System.Drawing.Point(453, 4);
            this.teCari.Name = "teCari";
            this.teCari.Size = new System.Drawing.Size(185, 20);
            this.teCari.StyleController = this.layoutControl1;
            this.teCari.TabIndex = 7;
            this.teCari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teCari_KeyPress);
            // 
            // sbCariOnline
            // 
            this.sbCariOnline.Image = ((System.Drawing.Image)(resources.GetObject("sbCariOnline.Image")));
            this.sbCariOnline.Location = new System.Drawing.Point(642, 4);
            this.sbCariOnline.Name = "sbCariOnline";
            this.sbCariOnline.Size = new System.Drawing.Size(96, 22);
            this.sbCariOnline.StyleController = this.layoutControl1;
            this.sbCariOnline.TabIndex = 6;
            this.sbCariOnline.Text = "Cari Online";
            this.sbCariOnline.Click += new System.EventHandler(this.sbCariOnline_Click);
            // 
            // teNamaKolom
            // 
            this.teNamaKolom.Location = new System.Drawing.Point(203, 4);
            this.teNamaKolom.Name = "teNamaKolom";
            this.teNamaKolom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teNamaKolom.Size = new System.Drawing.Size(185, 20);
            this.teNamaKolom.StyleController = this.layoutControl1;
            this.teNamaKolom.TabIndex = 5;
            this.teNamaKolom.EditValueChanged += new System.EventHandler(this.teNamaKolom_EditValueChanged);
            // 
            // gcSatker
            // 
            this.gcSatker.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcSatker.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcSatker.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcSatker.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcSatker.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcSatker.Location = new System.Drawing.Point(4, 30);
            this.gcSatker.MainView = this.gvSatker;
            this.gcSatker.Name = "gcSatker";
            this.gcSatker.Size = new System.Drawing.Size(734, 321);
            this.gcSatker.TabIndex = 4;
            this.gcSatker.UseEmbeddedNavigator = true;
            this.gcSatker.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSatker});
            // 
            // gvSatker
            // 
            this.gvSatker.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NO,
            this.KODE_KL,
            this.NAMA_KL,
            this.KODE_ESELON_I,
            this.NAMA_ESELON_I,
            this.KODE_KORWIL,
            this.NAMA_KORWIL,
            this.KODE_SATKER,
            this.NAMA_SATKER});
            this.gvSatker.GridControl = this.gcSatker;
            this.gvSatker.Name = "gvSatker";
            this.gvSatker.OptionsBehavior.Editable = false;
            this.gvSatker.OptionsBehavior.ReadOnly = true;
            this.gvSatker.OptionsView.ColumnAutoWidth = false;
            this.gvSatker.OptionsView.ShowAutoFilterRow = true;
            this.gvSatker.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvSatker_FocusedRowChanged);
            this.gvSatker.ColumnFilterChanged += new System.EventHandler(this.gvSatker_ColumnFilterChanged);
            this.gvSatker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvSatker_KeyPress);
            this.gvSatker.DoubleClick += new System.EventHandler(this.gvSatker_DoubleClick);
            // 
            // NO
            // 
            this.NO.AppearanceHeader.Options.UseTextOptions = true;
            this.NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NO.Caption = "NO";
            this.NO.FieldName = "NUM";
            this.NO.Name = "NO";
            this.NO.Visible = true;
            this.NO.VisibleIndex = 0;
            this.NO.Width = 51;
            // 
            // KODE_KL
            // 
            this.KODE_KL.AppearanceCell.Options.UseTextOptions = true;
            this.KODE_KL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_KL.AppearanceHeader.Options.UseTextOptions = true;
            this.KODE_KL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_KL.Caption = "KODE KL";
            this.KODE_KL.FieldName = "KD_KL";
            this.KODE_KL.Name = "KODE_KL";
            this.KODE_KL.Visible = true;
            this.KODE_KL.VisibleIndex = 1;
            // 
            // NAMA_KL
            // 
            this.NAMA_KL.AppearanceHeader.Options.UseTextOptions = true;
            this.NAMA_KL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NAMA_KL.Caption = "NAMA KL";
            this.NAMA_KL.FieldName = "UR_KL";
            this.NAMA_KL.Name = "NAMA_KL";
            this.NAMA_KL.Visible = true;
            this.NAMA_KL.VisibleIndex = 2;
            this.NAMA_KL.Width = 235;
            // 
            // KODE_ESELON_I
            // 
            this.KODE_ESELON_I.AppearanceCell.Options.UseTextOptions = true;
            this.KODE_ESELON_I.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_ESELON_I.AppearanceHeader.Options.UseTextOptions = true;
            this.KODE_ESELON_I.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_ESELON_I.Caption = "KODE ESELON I";
            this.KODE_ESELON_I.FieldName = "KD_ESELONKL";
            this.KODE_ESELON_I.Name = "KODE_ESELON_I";
            this.KODE_ESELON_I.Visible = true;
            this.KODE_ESELON_I.VisibleIndex = 3;
            this.KODE_ESELON_I.Width = 85;
            // 
            // NAMA_ESELON_I
            // 
            this.NAMA_ESELON_I.AppearanceHeader.Options.UseTextOptions = true;
            this.NAMA_ESELON_I.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NAMA_ESELON_I.Caption = "NAMA ESELON I";
            this.NAMA_ESELON_I.FieldName = "UR_ESELON1";
            this.NAMA_ESELON_I.Name = "NAMA_ESELON_I";
            this.NAMA_ESELON_I.Visible = true;
            this.NAMA_ESELON_I.VisibleIndex = 4;
            this.NAMA_ESELON_I.Width = 194;
            // 
            // KODE_KORWIL
            // 
            this.KODE_KORWIL.AppearanceCell.Options.UseTextOptions = true;
            this.KODE_KORWIL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_KORWIL.AppearanceHeader.Options.UseTextOptions = true;
            this.KODE_KORWIL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_KORWIL.Caption = "KODE KORWIL";
            this.KODE_KORWIL.FieldName = "KD_WILESELON";
            this.KODE_KORWIL.Name = "KODE_KORWIL";
            this.KODE_KORWIL.Visible = true;
            this.KODE_KORWIL.VisibleIndex = 5;
            this.KODE_KORWIL.Width = 84;
            // 
            // NAMA_KORWIL
            // 
            this.NAMA_KORWIL.AppearanceHeader.Options.UseTextOptions = true;
            this.NAMA_KORWIL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NAMA_KORWIL.Caption = "NAMA KORWIL";
            this.NAMA_KORWIL.FieldName = "UR_KORWIL";
            this.NAMA_KORWIL.Name = "NAMA_KORWIL";
            this.NAMA_KORWIL.Visible = true;
            this.NAMA_KORWIL.VisibleIndex = 6;
            this.NAMA_KORWIL.Width = 199;
            // 
            // KODE_SATKER
            // 
            this.KODE_SATKER.AppearanceCell.Options.UseTextOptions = true;
            this.KODE_SATKER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_SATKER.AppearanceHeader.Options.UseTextOptions = true;
            this.KODE_SATKER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_SATKER.Caption = "KODE SATKER";
            this.KODE_SATKER.FieldName = "KD_SATKER";
            this.KODE_SATKER.Name = "KODE_SATKER";
            this.KODE_SATKER.Visible = true;
            this.KODE_SATKER.VisibleIndex = 7;
            this.KODE_SATKER.Width = 180;
            // 
            // NAMA_SATKER
            // 
            this.NAMA_SATKER.AppearanceHeader.Options.UseTextOptions = true;
            this.NAMA_SATKER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NAMA_SATKER.Caption = "NAMA SATKER";
            this.NAMA_SATKER.FieldName = "UR_SATKER";
            this.NAMA_SATKER.Name = "NAMA_SATKER";
            this.NAMA_SATKER.Visible = true;
            this.NAMA_SATKER.VisibleIndex = 8;
            this.NAMA_SATKER.Width = 485;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(742, 355);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(138, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcSatker;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(738, 325);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teNamaKolom;
            this.layoutControlItem2.CustomizationFormText = "Nama Kolom";
            this.layoutControlItem2.Location = new System.Drawing.Point(138, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(250, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(250, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Nama Kolom";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbCariOnline;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(638, 0);
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
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teCari;
            this.layoutControlItem4.CustomizationFormText = "Kata Kunci";
            this.layoutControlItem4.Location = new System.Drawing.Point(388, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(250, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(250, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Kata Kunci";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(58, 13);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiRefresh,
            this.bbiMoreData,
            this.bbiTutup});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMoreData),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiTutup, true)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "Refresh";
            this.bbiRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.Glyph")));
            this.bbiRefresh.Id = 0;
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
            // 
            // bbiMoreData
            // 
            this.bbiMoreData.Caption = "Lebih Banyak";
            this.bbiMoreData.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiMoreData.Glyph")));
            this.bbiMoreData.Id = 1;
            this.bbiMoreData.Name = "bbiMoreData";
            this.bbiMoreData.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiMoreData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMoreData_ItemClick);
            // 
            // bbiTutup
            // 
            this.bbiTutup.Caption = "Tutup";
            this.bbiTutup.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiTutup.Glyph")));
            this.bbiTutup.Id = 2;
            this.bbiTutup.Name = "bbiTutup";
            this.bbiTutup.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiTutup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiTutup_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(742, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 395);
            this.barDockControlBottom.Size = new System.Drawing.Size(742, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 355);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(742, 40);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 355);
            // 
            // frmPuSatker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 395);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmPuSatker";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daftar Satuan Kerja";
            this.Load += new System.EventHandler(this.frmPuSatker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teCari.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNamaKolom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSatker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSatker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.GridControl gcSatker;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSatker;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.ComboBoxEdit teNamaKolom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbCariOnline;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit teCari;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiRefresh;
        private DevExpress.XtraBars.BarButtonItem bbiMoreData;
        private DevExpress.XtraBars.BarButtonItem bbiTutup;
        private DevExpress.XtraGrid.Columns.GridColumn NO;
        private DevExpress.XtraGrid.Columns.GridColumn KODE_SATKER;
        private DevExpress.XtraGrid.Columns.GridColumn NAMA_SATKER;
        private DevExpress.XtraGrid.Columns.GridColumn KODE_KL;
        private DevExpress.XtraGrid.Columns.GridColumn NAMA_KL;
        private DevExpress.XtraGrid.Columns.GridColumn KODE_ESELON_I;
        private DevExpress.XtraGrid.Columns.GridColumn NAMA_ESELON_I;
        private DevExpress.XtraGrid.Columns.GridColumn KODE_KORWIL;
        private DevExpress.XtraGrid.Columns.GridColumn NAMA_KORWIL;
    }
}