namespace AppPengguna.AST.BG
{
    partial class PuFasBangunan
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
            this.teKet = new DevExpress.XtraEditors.TextEdit();
            this.teFasLainnya = new DevExpress.XtraEditors.TextEdit();
            this.cbSaluranLimbah = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbGas = new DevExpress.XtraEditors.ComboBoxEdit();
            this.teTelpon = new DevExpress.XtraEditors.TextEdit();
            this.tePAM = new DevExpress.XtraEditors.TextEdit();
            this.teListrik = new DevExpress.XtraEditors.TextEdit();
            this.teIdKbdg = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teKet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFasLainnya.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSaluranLimbah.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbGas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTelpon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePAM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teListrik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIdKbdg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
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
            this.barSimpan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSimpan_ItemClick);
            // 
            // barBersih
            // 
            this.barBersih.Caption = "Bersihkan";
            this.barBersih.Glyph = global::AppPengguna.Properties.Resources.tombol_refresh;
            this.barBersih.Id = 2;
            this.barBersih.Name = "barBersih";
            this.barBersih.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBersih_ItemClick);
            // 
            // barBatal
            // 
            this.barBatal.Caption = "Batal";
            this.barBatal.Glyph = global::AppPengguna.Properties.Resources.tombol_kembali;
            this.barBatal.Id = 1;
            this.barBatal.Name = "barBatal";
            this.barBatal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBatal_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(324, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 345);
            this.barDockControlBottom.Size = new System.Drawing.Size(324, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 305);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(324, 40);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 305);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teKet);
            this.layoutControl1.Controls.Add(this.teFasLainnya);
            this.layoutControl1.Controls.Add(this.cbSaluranLimbah);
            this.layoutControl1.Controls.Add(this.cbGas);
            this.layoutControl1.Controls.Add(this.teTelpon);
            this.layoutControl1.Controls.Add(this.tePAM);
            this.layoutControl1.Controls.Add(this.teListrik);
            this.layoutControl1.Controls.Add(this.teIdKbdg);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(324, 305);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teKet
            // 
            this.teKet.Location = new System.Drawing.Point(93, 180);
            this.teKet.MenuManager = this.barManager1;
            this.teKet.Name = "teKet";
            this.teKet.Properties.MaxLength = 255;
            this.teKet.Size = new System.Drawing.Size(219, 20);
            this.teKet.StyleController = this.layoutControl1;
            this.teKet.TabIndex = 12;
            // 
            // teFasLainnya
            // 
            this.teFasLainnya.Location = new System.Drawing.Point(93, 156);
            this.teFasLainnya.MenuManager = this.barManager1;
            this.teFasLainnya.Name = "teFasLainnya";
            this.teFasLainnya.Properties.MaxLength = 50;
            this.teFasLainnya.Size = new System.Drawing.Size(219, 20);
            this.teFasLainnya.StyleController = this.layoutControl1;
            this.teFasLainnya.TabIndex = 11;
            // 
            // cbSaluranLimbah
            // 
            this.cbSaluranLimbah.Location = new System.Drawing.Point(93, 132);
            this.cbSaluranLimbah.MenuManager = this.barManager1;
            this.cbSaluranLimbah.Name = "cbSaluranLimbah";
            this.cbSaluranLimbah.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSaluranLimbah.Properties.Items.AddRange(new object[] {
            "Ada",
            "Tidak Ada"});
            this.cbSaluranLimbah.Properties.MaxLength = 50;
            this.cbSaluranLimbah.Size = new System.Drawing.Size(219, 20);
            this.cbSaluranLimbah.StyleController = this.layoutControl1;
            this.cbSaluranLimbah.TabIndex = 10;
            // 
            // cbGas
            // 
            this.cbGas.Location = new System.Drawing.Point(93, 108);
            this.cbGas.MenuManager = this.barManager1;
            this.cbGas.Name = "cbGas";
            this.cbGas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbGas.Properties.Items.AddRange(new object[] {
            "Ada",
            "Tidak Ada"});
            this.cbGas.Properties.MaxLength = 50;
            this.cbGas.Size = new System.Drawing.Size(219, 20);
            this.cbGas.StyleController = this.layoutControl1;
            this.cbGas.TabIndex = 9;
            // 
            // teTelpon
            // 
            this.teTelpon.Location = new System.Drawing.Point(93, 84);
            this.teTelpon.MenuManager = this.barManager1;
            this.teTelpon.Name = "teTelpon";
            this.teTelpon.Properties.MaxLength = 50;
            this.teTelpon.Size = new System.Drawing.Size(219, 20);
            this.teTelpon.StyleController = this.layoutControl1;
            this.teTelpon.TabIndex = 8;
            // 
            // tePAM
            // 
            this.tePAM.Location = new System.Drawing.Point(93, 60);
            this.tePAM.MenuManager = this.barManager1;
            this.tePAM.Name = "tePAM";
            this.tePAM.Properties.MaxLength = 50;
            this.tePAM.Size = new System.Drawing.Size(219, 20);
            this.tePAM.StyleController = this.layoutControl1;
            this.tePAM.TabIndex = 7;
            // 
            // teListrik
            // 
            this.teListrik.Location = new System.Drawing.Point(93, 36);
            this.teListrik.MenuManager = this.barManager1;
            this.teListrik.Name = "teListrik";
            this.teListrik.Properties.MaxLength = 50;
            this.teListrik.Size = new System.Drawing.Size(219, 20);
            this.teListrik.StyleController = this.layoutControl1;
            this.teListrik.TabIndex = 6;
            // 
            // teIdKbdg
            // 
            this.teIdKbdg.Location = new System.Drawing.Point(93, 12);
            this.teIdKbdg.MenuManager = this.barManager1;
            this.teIdKbdg.Name = "teIdKbdg";
            this.teIdKbdg.Properties.ReadOnly = true;
            this.teIdKbdg.Size = new System.Drawing.Size(219, 20);
            this.teIdKbdg.StyleController = this.layoutControl1;
            this.teIdKbdg.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(324, 305);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teIdKbdg;
            this.layoutControlItem1.CustomizationFormText = "ID Tanah";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(304, 24);
            this.layoutControlItem1.Text = "ID Bangunan*";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(78, 13);
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teListrik;
            this.layoutControlItem3.CustomizationFormText = "Listrik";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(304, 24);
            this.layoutControlItem3.Text = "Listrik";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tePAM;
            this.layoutControlItem4.CustomizationFormText = "Air Bersih";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(304, 24);
            this.layoutControlItem4.Text = "Air Bersih";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teTelpon;
            this.layoutControlItem5.CustomizationFormText = "Telpon";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(304, 24);
            this.layoutControlItem5.Text = "Telpon";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cbGas;
            this.layoutControlItem6.CustomizationFormText = "Gas";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(304, 24);
            this.layoutControlItem6.Text = "Gas";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cbSaluranLimbah;
            this.layoutControlItem7.CustomizationFormText = "Saluran Limbah";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(304, 24);
            this.layoutControlItem7.Text = "Saluran Limbah";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teFasLainnya;
            this.layoutControlItem8.CustomizationFormText = "Fasilitas Lainnya";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(304, 24);
            this.layoutControlItem8.Text = "Fasilitas Lainnya";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teKet;
            this.layoutControlItem9.CustomizationFormText = "Keterangan";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 168);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(304, 117);
            this.layoutControlItem9.Text = "Keterangan";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(78, 13);
            // 
            // PuFasBangunan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 345);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(340, 380);
            this.MinimizeBox = false;
            this.Name = "PuFasBangunan";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fasilitas Bangunan";
            this.Load += new System.EventHandler(this.PuFasBangunan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teKet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFasLainnya.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSaluranLimbah.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbGas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTelpon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePAM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teListrik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIdKbdg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit teKet;
        private DevExpress.XtraEditors.TextEdit teFasLainnya;
        private DevExpress.XtraEditors.ComboBoxEdit cbSaluranLimbah;
        private DevExpress.XtraEditors.ComboBoxEdit cbGas;
        private DevExpress.XtraEditors.TextEdit teTelpon;
        private DevExpress.XtraEditors.TextEdit tePAM;
        private DevExpress.XtraEditors.TextEdit teListrik;
        private DevExpress.XtraEditors.TextEdit teIdKbdg;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}