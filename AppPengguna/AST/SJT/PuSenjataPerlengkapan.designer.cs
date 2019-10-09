namespace AppPengguna.AST.SJT
{
    partial class PuSenjataPerlengkapan
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
            this.barSimpan = new DevExpress.XtraBars.BarButtonItem();
            this.barBersih = new DevExpress.XtraBars.BarButtonItem();
            this.barBatal = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.teId_Ksenj = new DevExpress.XtraEditors.TextEdit();
            this.teKet = new DevExpress.XtraEditors.MemoEdit();
            this.teNamaPerlengkapan = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ofdFoto = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teId_Ksenj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNamaPerlengkapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
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
            this.barSimpan,
            this.barBatal,
            this.barBersih});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(56, 145);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSimpan),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBersih),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBatal)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DisableCustomization = true;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSimpan
            // 
            this.barSimpan.Caption = "Simpan";
            this.barSimpan.Glyph = global::AppPengguna.Properties.Resources.tombol_simpan;
            this.barSimpan.Id = 0;
            this.barSimpan.Name = "barSimpan";
            this.barSimpan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSimpan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSimpan_ItemClick);
            // 
            // barBersih
            // 
            this.barBersih.Caption = "Reset";
            this.barBersih.Glyph = global::AppPengguna.Properties.Resources.tombol_refresh;
            this.barBersih.Id = 2;
            this.barBersih.Name = "barBersih";
            this.barBersih.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBersih.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBersih_ItemClick);
            // 
            // barBatal
            // 
            this.barBatal.Caption = "Kembali";
            this.barBatal.Glyph = global::AppPengguna.Properties.Resources.tombol_kembali;
            this.barBatal.Id = 1;
            this.barBatal.Name = "barBatal";
            this.barBatal.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBatal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBatal_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(371, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 209);
            this.barDockControlBottom.Size = new System.Drawing.Size(371, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 169);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(371, 40);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 169);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teId_Ksenj);
            this.layoutControl1.Controls.Add(this.teKet);
            this.layoutControl1.Controls.Add(this.teNamaPerlengkapan);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(371, 169);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teId_Ksenj
            // 
            this.teId_Ksenj.Location = new System.Drawing.Point(110, 12);
            this.teId_Ksenj.MenuManager = this.barManager1;
            this.teId_Ksenj.Name = "teId_Ksenj";
            this.teId_Ksenj.Properties.ReadOnly = true;
            this.teId_Ksenj.Size = new System.Drawing.Size(249, 20);
            this.teId_Ksenj.StyleController = this.layoutControl1;
            this.teId_Ksenj.TabIndex = 4;
            // 
            // teKet
            // 
            this.teKet.Location = new System.Drawing.Point(110, 60);
            this.teKet.MenuManager = this.barManager1;
            this.teKet.Name = "teKet";
            this.teKet.Properties.MaxLength = 100;
            this.teKet.Size = new System.Drawing.Size(249, 65);
            this.teKet.StyleController = this.layoutControl1;
            this.teKet.TabIndex = 12;
            // 
            // teNamaPerlengkapan
            // 
            this.teNamaPerlengkapan.Location = new System.Drawing.Point(110, 36);
            this.teNamaPerlengkapan.MenuManager = this.barManager1;
            this.teNamaPerlengkapan.Name = "teNamaPerlengkapan";
            this.teNamaPerlengkapan.Properties.MaxLength = 50;
            this.teNamaPerlengkapan.Size = new System.Drawing.Size(249, 20);
            this.teNamaPerlengkapan.StyleController = this.layoutControl1;
            this.teNamaPerlengkapan.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(371, 169);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teId_Ksenj;
            this.layoutControlItem1.CustomizationFormText = "ID Tanah";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(351, 24);
            this.layoutControlItem1.Text = "ID Senjata*";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(95, 13);
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teNamaPerlengkapan;
            this.layoutControlItem4.CustomizationFormText = "Air Bersih";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(351, 24);
            this.layoutControlItem4.Text = "Nama Perlengkapan";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teKet;
            this.layoutControlItem8.CustomizationFormText = "Material Dinding";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(351, 69);
            this.layoutControlItem8.Text = "Keterangan";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(95, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 117);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(351, 32);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ofdFoto
            // 
            this.ofdFoto.FileName = "openFileDialog1";
            this.ofdFoto.Title = "Pilih Gambar";
            // 
            // PuSenjataPerlengkapan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 209);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PuSenjataPerlengkapan";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Perlengkapan Senjata";
            this.Load += new System.EventHandler(this.PuSenjataPerlengkapan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teId_Ksenj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNamaPerlengkapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSimpan;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBatal;
        private DevExpress.XtraBars.BarButtonItem barBersih;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraEditors.TextEdit teId_Ksenj;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private System.Windows.Forms.OpenFileDialog ofdFoto;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.MemoEdit teKet;
        private DevExpress.XtraEditors.TextEdit teNamaPerlengkapan;
    }
}