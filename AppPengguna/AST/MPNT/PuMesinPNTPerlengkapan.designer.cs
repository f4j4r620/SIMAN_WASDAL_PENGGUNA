namespace AppPengguna.AST.MPNT
{
    partial class PuMesinPNTPerlengkapan
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
            this.bbKibSimpan = new DevExpress.XtraBars.BarButtonItem();
            this.bbKibRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbKibKembali = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.beMarqueBar = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.teNmPerlengkapan = new DevExpress.XtraEditors.TextEdit();
            this.meKet = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcDokKibAngk = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teNmPerlengkapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meKet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDokKibAngk)).BeginInit();
            this.gcDokKibAngk.SuspendLayout();
            this.SuspendLayout();
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
            this.bbKibSimpan,
            this.bbKibRefresh,
            this.bbKibKembali,
            this.beMarqueBar});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bbKibSimpan),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbKibRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbKibKembali)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbKibSimpan
            // 
            this.bbKibSimpan.Caption = "Simpan";
            this.bbKibSimpan.Glyph = global::AppPengguna.Properties.Resources.tombol_simpan;
            this.bbKibSimpan.Id = 0;
            this.bbKibSimpan.Name = "bbKibSimpan";
            this.bbKibSimpan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbKibSimpan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbKibSimpan_ItemClick);
            // 
            // bbKibRefresh
            // 
            this.bbKibRefresh.Caption = "Refresh";
            this.bbKibRefresh.Glyph = global::AppPengguna.Properties.Resources.tombol_refresh;
            this.bbKibRefresh.Id = 1;
            this.bbKibRefresh.Name = "bbKibRefresh";
            this.bbKibRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbKibRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbKibRefresh_ItemClick);
            // 
            // bbKibKembali
            // 
            this.bbKibKembali.Caption = "Kembali";
            this.bbKibKembali.Glyph = global::AppPengguna.Properties.Resources.tombol_kembali;
            this.bbKibKembali.Id = 2;
            this.bbKibKembali.Name = "bbKibKembali";
            this.bbKibKembali.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbKibKembali.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbKibKembali_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.beMarqueBar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // beMarqueBar
            // 
            this.beMarqueBar.Caption = "barEditItem1";
            this.beMarqueBar.Edit = this.repositoryItemMarqueeProgressBar1;
            this.beMarqueBar.Id = 3;
            this.beMarqueBar.Name = "beMarqueBar";
            this.beMarqueBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.beMarqueBar.Width = 211;
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
            this.barDockControlTop.Size = new System.Drawing.Size(373, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 197);
            this.barDockControlBottom.Size = new System.Drawing.Size(373, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 157);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(373, 40);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 157);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teNmPerlengkapan);
            this.layoutControl1.Controls.Add(this.meKet);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 21);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(369, 134);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teNmPerlengkapan
            // 
            this.teNmPerlengkapan.Location = new System.Drawing.Point(111, 12);
            this.teNmPerlengkapan.MenuManager = this.barManager1;
            this.teNmPerlengkapan.Name = "teNmPerlengkapan";
            this.teNmPerlengkapan.Size = new System.Drawing.Size(246, 20);
            this.teNmPerlengkapan.StyleController = this.layoutControl1;
            this.teNmPerlengkapan.TabIndex = 7;
            // 
            // meKet
            // 
            this.meKet.Location = new System.Drawing.Point(111, 36);
            this.meKet.MenuManager = this.barManager1;
            this.meKet.Name = "meKet";
            this.meKet.Size = new System.Drawing.Size(246, 86);
            this.meKet.StyleController = this.layoutControl1;
            this.meKet.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(369, 134);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.meKet;
            this.layoutControlItem3.CustomizationFormText = "Keterangan";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(349, 90);
            this.layoutControlItem3.Text = "Keterangan";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teNmPerlengkapan;
            this.layoutControlItem1.CustomizationFormText = "Nama Perlengkapan";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(349, 24);
            this.layoutControlItem1.Text = "Nama Perlengkapan";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(95, 13);
            // 
            // gcDokKibAngk
            // 
            this.gcDokKibAngk.Controls.Add(this.layoutControl1);
            this.gcDokKibAngk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDokKibAngk.Location = new System.Drawing.Point(0, 40);
            this.gcDokKibAngk.Name = "gcDokKibAngk";
            this.gcDokKibAngk.Size = new System.Drawing.Size(373, 157);
            this.gcDokKibAngk.TabIndex = 16;
            // 
            // PuMesinPNTPerlengkapan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 220);
            this.Controls.Add(this.gcDokKibAngk);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PuMesinPNTPerlengkapan";
            this.Text = "PuMesinPNTPerlengkapan";
            this.Load += new System.EventHandler(this.FormRwyDokKib_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teNmPerlengkapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meKet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDokKibAngk)).EndInit();
            this.gcDokKibAngk.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbKibSimpan;
        private DevExpress.XtraBars.BarButtonItem bbKibRefresh;
        private DevExpress.XtraBars.BarButtonItem bbKibKembali;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarEditItem beMarqueBar;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl gcDokKibAngk;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.MemoEdit meKet;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit teNmPerlengkapan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}