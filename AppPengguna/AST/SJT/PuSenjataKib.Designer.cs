namespace AppPengguna.AST.SJT
{
    partial class PuSenjataKib
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
            this.teNoDokKib = new DevExpress.XtraEditors.TextEdit();
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
            this.sbNamaFile = new DevExpress.XtraEditors.SimpleButton();
            this.teNmFile = new DevExpress.XtraEditors.TextEdit();
            this.teTglDok = new DevExpress.XtraEditors.DateEdit();
            this.meKet = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcDokKibAngk = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.teNoDokKib.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teNmFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTglDok.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTglDok.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meKet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDokKibAngk)).BeginInit();
            this.gcDokKibAngk.SuspendLayout();
            this.SuspendLayout();
            // 
            // teNoDokKib
            // 
            this.teNoDokKib.Location = new System.Drawing.Point(101, 12);
            this.teNoDokKib.MenuManager = this.barManager1;
            this.teNoDokKib.Name = "teNoDokKib";
            this.teNoDokKib.Size = new System.Drawing.Size(234, 20);
            this.teNoDokKib.StyleController = this.layoutControl1;
            this.teNoDokKib.TabIndex = 4;
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
            this.barDockControlTop.Size = new System.Drawing.Size(351, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 266);
            this.barDockControlBottom.Size = new System.Drawing.Size(351, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 226);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(351, 40);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 226);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbNamaFile);
            this.layoutControl1.Controls.Add(this.teNmFile);
            this.layoutControl1.Controls.Add(this.teNoDokKib);
            this.layoutControl1.Controls.Add(this.teTglDok);
            this.layoutControl1.Controls.Add(this.meKet);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 21);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(347, 203);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbNamaFile
            // 
            this.sbNamaFile.Location = new System.Drawing.Point(258, 169);
            this.sbNamaFile.Name = "sbNamaFile";
            this.sbNamaFile.Size = new System.Drawing.Size(77, 22);
            this.sbNamaFile.StyleController = this.layoutControl1;
            this.sbNamaFile.TabIndex = 8;
            this.sbNamaFile.Text = "Unggah File";
            this.sbNamaFile.Click += new System.EventHandler(this.sbNamaFile_Click);
            // 
            // teNmFile
            // 
            this.teNmFile.Location = new System.Drawing.Point(101, 169);
            this.teNmFile.MenuManager = this.barManager1;
            this.teNmFile.Name = "teNmFile";
            this.teNmFile.Size = new System.Drawing.Size(153, 20);
            this.teNmFile.StyleController = this.layoutControl1;
            this.teNmFile.TabIndex = 7;
            // 
            // teTglDok
            // 
            this.teTglDok.EditValue = null;
            this.teTglDok.Location = new System.Drawing.Point(101, 36);
            this.teTglDok.MenuManager = this.barManager1;
            this.teTglDok.Name = "teTglDok";
            this.teTglDok.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teTglDok.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teTglDok.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.teTglDok.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.teTglDok.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.teTglDok.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.teTglDok.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.teTglDok.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.teTglDok.Properties.Mask.EditMask = "";
            this.teTglDok.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.teTglDok.Size = new System.Drawing.Size(234, 20);
            this.teTglDok.StyleController = this.layoutControl1;
            this.teTglDok.TabIndex = 5;
            // 
            // meKet
            // 
            this.meKet.Location = new System.Drawing.Point(101, 60);
            this.meKet.MenuManager = this.barManager1;
            this.meKet.Name = "meKet";
            this.meKet.Size = new System.Drawing.Size(234, 105);
            this.meKet.StyleController = this.layoutControl1;
            this.meKet.TabIndex = 6;
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
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(347, 203);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teNoDokKib;
            this.layoutControlItem1.CustomizationFormText = "No Dokumen Kib";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(327, 24);
            this.layoutControlItem1.Text = "No Dokumen Kib";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teTglDok;
            this.layoutControlItem2.CustomizationFormText = "Tanggal Dokumen";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(327, 24);
            this.layoutControlItem2.Text = "Tanggal Dokumen";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.meKet;
            this.layoutControlItem3.CustomizationFormText = "Keterangan";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(327, 109);
            this.layoutControlItem3.Text = "Keterangan";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teNmFile;
            this.layoutControlItem4.CustomizationFormText = "File";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 157);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(246, 26);
            this.layoutControlItem4.Text = "File";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbNamaFile;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(246, 157);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(81, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // gcDokKibAngk
            // 
            this.gcDokKibAngk.Controls.Add(this.layoutControl1);
            this.gcDokKibAngk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDokKibAngk.Location = new System.Drawing.Point(0, 40);
            this.gcDokKibAngk.Name = "gcDokKibAngk";
            this.gcDokKibAngk.Size = new System.Drawing.Size(351, 226);
            this.gcDokKibAngk.TabIndex = 16;
            // 
            // PuSenjataKib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 289);
            this.Controls.Add(this.gcDokKibAngk);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PuSenjataKib";
            this.Text = "PuSenjataKib";
            this.Load += new System.EventHandler(this.PuSenjataKib_Load);
            ((System.ComponentModel.ISupportInitialize)(this.teNoDokKib.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teNmFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTglDok.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTglDok.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meKet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDokKibAngk)).EndInit();
            this.gcDokKibAngk.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit teNoDokKib;
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
        private DevExpress.XtraEditors.SimpleButton sbNamaFile;
        private DevExpress.XtraEditors.TextEdit teNmFile;
        private DevExpress.XtraEditors.DateEdit teTglDok;
        private DevExpress.XtraEditors.MemoEdit meKet;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}