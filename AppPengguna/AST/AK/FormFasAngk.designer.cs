namespace AppPengguna.AST.AK
{
    partial class FormFasAngk
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
          this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
          this.bar2 = new DevExpress.XtraBars.Bar();
          this.bbFasAngkSimpan = new DevExpress.XtraBars.BarButtonItem();
          this.bbFasAngkRefresh = new DevExpress.XtraBars.BarButtonItem();
          this.bbFasAngkTutup = new DevExpress.XtraBars.BarButtonItem();
          this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
          this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
          this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
          this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
          this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
          this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
          this.teIsiFas = new DevExpress.XtraEditors.TextEdit();
          this.teNmFas = new DevExpress.XtraEditors.TextEdit();
          this.meKetFas = new DevExpress.XtraEditors.MemoEdit();
          this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
          this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
          this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
          this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
          this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
          this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
          this.bar5 = new DevExpress.XtraBars.Bar();
          this.beMarqueBar = new DevExpress.XtraBars.BarEditItem();
          this.repositoryItemMarqueeProgressBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
          this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
          this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
          this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
          this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
          ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
          this.layoutControl1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.teIsiFas.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.teNmFas.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.meKetFas.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar2)).BeginInit();
          this.SuspendLayout();
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
            this.bbFasAngkSimpan,
            this.bbFasAngkRefresh,
            this.bbFasAngkTutup});
          this.barManager1.MainMenu = this.bar2;
          this.barManager1.MaxItemId = 4;
          this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMarqueeProgressBar1});
          // 
          // bar2
          // 
          this.bar2.BarName = "Main menu";
          this.bar2.DockCol = 0;
          this.bar2.DockRow = 0;
          this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
          this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbFasAngkSimpan),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbFasAngkRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbFasAngkTutup)});
          this.bar2.OptionsBar.MultiLine = true;
          this.bar2.OptionsBar.UseWholeRow = true;
          this.bar2.Text = "Main menu";
          // 
          // bbFasAngkSimpan
          // 
          this.bbFasAngkSimpan.Caption = "Simpan";
          this.bbFasAngkSimpan.Glyph = global::AppPengguna.Properties.Resources.tombol_simpan;
          this.bbFasAngkSimpan.Id = 0;
          this.bbFasAngkSimpan.Name = "bbFasAngkSimpan";
          this.bbFasAngkSimpan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
          this.bbFasAngkSimpan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbFasAngkSimpan_ItemClick);
          // 
          // bbFasAngkRefresh
          // 
          this.bbFasAngkRefresh.Caption = "Refresh";
          this.bbFasAngkRefresh.Glyph = global::AppPengguna.Properties.Resources.tombol_refresh;
          this.bbFasAngkRefresh.Id = 1;
          this.bbFasAngkRefresh.Name = "bbFasAngkRefresh";
          this.bbFasAngkRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
          this.bbFasAngkRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbFasAngkRefresh_ItemClick);
          // 
          // bbFasAngkTutup
          // 
          this.bbFasAngkTutup.Caption = "Kembali";
          this.bbFasAngkTutup.Glyph = global::AppPengguna.Properties.Resources.tombol_kembali;
          this.bbFasAngkTutup.Id = 2;
          this.bbFasAngkTutup.Name = "bbFasAngkTutup";
          this.bbFasAngkTutup.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
          this.bbFasAngkTutup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbFasAngkTutup_ItemClick);
          // 
          // barDockControlTop
          // 
          this.barDockControlTop.CausesValidation = false;
          this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
          this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
          this.barDockControlTop.Size = new System.Drawing.Size(424, 40);
          // 
          // barDockControlBottom
          // 
          this.barDockControlBottom.CausesValidation = false;
          this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
          this.barDockControlBottom.Location = new System.Drawing.Point(0, 190);
          this.barDockControlBottom.Size = new System.Drawing.Size(424, 0);
          // 
          // barDockControlLeft
          // 
          this.barDockControlLeft.CausesValidation = false;
          this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
          this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
          this.barDockControlLeft.Size = new System.Drawing.Size(0, 150);
          // 
          // barDockControlRight
          // 
          this.barDockControlRight.CausesValidation = false;
          this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
          this.barDockControlRight.Location = new System.Drawing.Point(424, 40);
          this.barDockControlRight.Size = new System.Drawing.Size(0, 150);
          // 
          // repositoryItemMarqueeProgressBar1
          // 
          this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
          // 
          // layoutControl1
          // 
          this.layoutControl1.Controls.Add(this.teIsiFas);
          this.layoutControl1.Controls.Add(this.teNmFas);
          this.layoutControl1.Controls.Add(this.meKetFas);
          this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.layoutControl1.Location = new System.Drawing.Point(0, 40);
          this.layoutControl1.Name = "layoutControl1";
          this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
          this.layoutControl1.Root = this.layoutControlGroup1;
          this.layoutControl1.Size = new System.Drawing.Size(424, 150);
          this.layoutControl1.TabIndex = 4;
          this.layoutControl1.Text = "layoutControl1";
          // 
          // teIsiFas
          // 
          this.teIsiFas.Location = new System.Drawing.Point(111, 36);
          this.teIsiFas.MenuManager = this.barManager1;
          this.teIsiFas.Name = "teIsiFas";
          this.teIsiFas.Size = new System.Drawing.Size(301, 20);
          this.teIsiFas.StyleController = this.layoutControl1;
          this.teIsiFas.TabIndex = 5;
          // 
          // teNmFas
          // 
          this.teNmFas.Location = new System.Drawing.Point(111, 12);
          this.teNmFas.MenuManager = this.barManager1;
          this.teNmFas.Name = "teNmFas";
          this.teNmFas.Size = new System.Drawing.Size(301, 20);
          this.teNmFas.StyleController = this.layoutControl1;
          this.teNmFas.TabIndex = 4;
          // 
          // meKetFas
          // 
          this.meKetFas.Location = new System.Drawing.Point(111, 60);
          this.meKetFas.MenuManager = this.barManager1;
          this.meKetFas.Name = "meKetFas";
          this.meKetFas.Size = new System.Drawing.Size(301, 68);
          this.meKetFas.StyleController = this.layoutControl1;
          this.meKetFas.TabIndex = 6;
          // 
          // layoutControlGroup1
          // 
          this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
          this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
          this.layoutControlGroup1.GroupBordersVisible = false;
          this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
          this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
          this.layoutControlGroup1.Name = "layoutControlGroup1";
          this.layoutControlGroup1.Size = new System.Drawing.Size(424, 150);
          this.layoutControlGroup1.Text = "layoutControlGroup1";
          this.layoutControlGroup1.TextVisible = false;
          // 
          // layoutControlItem1
          // 
          this.layoutControlItem1.Control = this.teNmFas;
          this.layoutControlItem1.CustomizationFormText = "Nama Fasilitas";
          this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
          this.layoutControlItem1.Name = "layoutControlItem1";
          this.layoutControlItem1.Size = new System.Drawing.Size(404, 24);
          this.layoutControlItem1.Text = "Nama Perlengkapan";
          this.layoutControlItem1.TextSize = new System.Drawing.Size(95, 13);
          // 
          // layoutControlItem2
          // 
          this.layoutControlItem2.Control = this.teIsiFas;
          this.layoutControlItem2.CustomizationFormText = "Isi Fasilitas";
          this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
          this.layoutControlItem2.Name = "layoutControlItem2";
          this.layoutControlItem2.Size = new System.Drawing.Size(404, 24);
          this.layoutControlItem2.Text = "Isi Fasilitas";
          this.layoutControlItem2.TextSize = new System.Drawing.Size(95, 13);
          this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
          // 
          // layoutControlItem3
          // 
          this.layoutControlItem3.Control = this.meKetFas;
          this.layoutControlItem3.CustomizationFormText = "Keterangan";
          this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
          this.layoutControlItem3.Name = "layoutControlItem3";
          this.layoutControlItem3.Size = new System.Drawing.Size(404, 72);
          this.layoutControlItem3.Text = "Keterangan";
          this.layoutControlItem3.TextSize = new System.Drawing.Size(95, 13);
          // 
          // emptySpaceItem1
          // 
          this.emptySpaceItem1.AllowHotTrack = false;
          this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
          this.emptySpaceItem1.Location = new System.Drawing.Point(0, 120);
          this.emptySpaceItem1.Name = "emptySpaceItem1";
          this.emptySpaceItem1.Size = new System.Drawing.Size(404, 10);
          this.emptySpaceItem1.Text = "emptySpaceItem1";
          this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
          // 
          // barManager2
          // 
          this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar5});
          this.barManager2.DockControls.Add(this.barDockControl1);
          this.barManager2.DockControls.Add(this.barDockControl2);
          this.barManager2.DockControls.Add(this.barDockControl3);
          this.barManager2.DockControls.Add(this.barDockControl4);
          this.barManager2.Form = this;
          this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.beMarqueBar});
          this.barManager2.MaxItemId = 1;
          this.barManager2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMarqueeProgressBar2});
          this.barManager2.StatusBar = this.bar5;
          // 
          // bar5
          // 
          this.bar5.BarName = "Status bar";
          this.bar5.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
          this.bar5.DockCol = 0;
          this.bar5.DockRow = 0;
          this.bar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
          this.bar5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.beMarqueBar)});
          this.bar5.OptionsBar.AllowQuickCustomization = false;
          this.bar5.OptionsBar.DrawDragBorder = false;
          this.bar5.OptionsBar.UseWholeRow = true;
          this.bar5.Text = "Status bar";
          // 
          // beMarqueBar
          // 
          this.beMarqueBar.Caption = "barEditItem1";
          this.beMarqueBar.Edit = this.repositoryItemMarqueeProgressBar2;
          this.beMarqueBar.Id = 0;
          this.beMarqueBar.Name = "beMarqueBar";
          this.beMarqueBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
          this.beMarqueBar.Width = 385;
          // 
          // repositoryItemMarqueeProgressBar2
          // 
          this.repositoryItemMarqueeProgressBar2.Name = "repositoryItemMarqueeProgressBar2";
          // 
          // barDockControl1
          // 
          this.barDockControl1.CausesValidation = false;
          this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
          this.barDockControl1.Location = new System.Drawing.Point(0, 0);
          this.barDockControl1.Size = new System.Drawing.Size(424, 0);
          // 
          // barDockControl2
          // 
          this.barDockControl2.CausesValidation = false;
          this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
          this.barDockControl2.Location = new System.Drawing.Point(0, 190);
          this.barDockControl2.Size = new System.Drawing.Size(424, 23);
          // 
          // barDockControl3
          // 
          this.barDockControl3.CausesValidation = false;
          this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
          this.barDockControl3.Location = new System.Drawing.Point(0, 0);
          this.barDockControl3.Size = new System.Drawing.Size(0, 190);
          // 
          // barDockControl4
          // 
          this.barDockControl4.CausesValidation = false;
          this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
          this.barDockControl4.Location = new System.Drawing.Point(424, 0);
          this.barDockControl4.Size = new System.Drawing.Size(0, 190);
          // 
          // FormFasAngk
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(424, 213);
          this.Controls.Add(this.layoutControl1);
          this.Controls.Add(this.barDockControlLeft);
          this.Controls.Add(this.barDockControlRight);
          this.Controls.Add(this.barDockControlBottom);
          this.Controls.Add(this.barDockControlTop);
          this.Controls.Add(this.barDockControl3);
          this.Controls.Add(this.barDockControl4);
          this.Controls.Add(this.barDockControl2);
          this.Controls.Add(this.barDockControl1);
          this.Name = "FormFasAngk";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Perlengkapan Angkutan";
          ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
          this.layoutControl1.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.teIsiFas.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.teNmFas.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.meKetFas.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar2)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbFasAngkSimpan;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbFasAngkRefresh;
        private DevExpress.XtraBars.BarButtonItem bbFasAngkTutup;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit teIsiFas;
        private DevExpress.XtraEditors.TextEdit teNmFas;
        private DevExpress.XtraEditors.MemoEdit meKetFas;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar5;
        private DevExpress.XtraBars.BarEditItem beMarqueBar;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar2;
    }
}