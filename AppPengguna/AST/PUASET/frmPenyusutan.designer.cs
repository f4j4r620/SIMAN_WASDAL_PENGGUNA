namespace AppPengguna.AST.PUASET
{
  partial class frmPenyusutan
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
      this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
      this.bar3 = new DevExpress.XtraBars.Bar();
      this.beMarqueeBar = new DevExpress.XtraBars.BarEditItem();
      this.repositoryItemMarqueeProgressBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
      this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
      this.LayoutData = new DevExpress.XtraLayout.LayoutControl();
      this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
      this.teJnsSusut = new DevExpress.XtraEditors.TextEdit();
      this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
      this.teTglSusut = new DevExpress.XtraEditors.TextEdit();
      this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
      this.teNoSppa = new DevExpress.XtraEditors.TextEdit();
      this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
      this.teJnsTransaksi = new DevExpress.XtraEditors.TextEdit();
      this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.LayoutData)).BeginInit();
      this.LayoutData.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.teJnsSusut.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.teTglSusut.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.teNoSppa.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.teJnsTransaksi.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
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
            this.bbiClose,
            this.beMarqueeBar});
      this.barManager1.MainMenu = this.bar2;
      this.barManager1.MaxItemId = 4;
      this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMarqueeProgressBar1,
            this.repositoryItemTextEdit1,
            this.repositoryItemMarqueeProgressBar2});
      this.barManager1.StatusBar = this.bar3;
      // 
      // bar2
      // 
      this.bar2.BarName = "Main menu";
      this.bar2.DockCol = 0;
      this.bar2.DockRow = 0;
      this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
      this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClose)});
      this.bar2.OptionsBar.MultiLine = true;
      this.bar2.OptionsBar.UseWholeRow = true;
      this.bar2.Text = "Main menu";
      // 
      // bbiClose
      // 
      this.bbiClose.Caption = "Kembali";
      this.bbiClose.Glyph = global::AppPengguna.Properties.Resources.tombol_kembali;
      this.bbiClose.Id = 2;
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
      this.beMarqueeBar.Caption = "Progress";
      this.beMarqueeBar.Edit = this.repositoryItemMarqueeProgressBar2;
      this.beMarqueeBar.EditHeight = 20;
      this.beMarqueeBar.Id = 3;
      this.beMarqueeBar.Name = "beMarqueeBar";
      this.beMarqueeBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
      this.beMarqueeBar.Width = 334;
      // 
      // repositoryItemMarqueeProgressBar2
      // 
      this.repositoryItemMarqueeProgressBar2.Name = "repositoryItemMarqueeProgressBar2";
      // 
      // barDockControlTop
      // 
      this.barDockControlTop.CausesValidation = false;
      this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
      this.barDockControlTop.Size = new System.Drawing.Size(382, 40);
      // 
      // barDockControlBottom
      // 
      this.barDockControlBottom.CausesValidation = false;
      this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.barDockControlBottom.Location = new System.Drawing.Point(0, 174);
      this.barDockControlBottom.Size = new System.Drawing.Size(382, 25);
      // 
      // barDockControlLeft
      // 
      this.barDockControlLeft.CausesValidation = false;
      this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
      this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
      this.barDockControlLeft.Size = new System.Drawing.Size(0, 134);
      // 
      // barDockControlRight
      // 
      this.barDockControlRight.CausesValidation = false;
      this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
      this.barDockControlRight.Location = new System.Drawing.Point(382, 40);
      this.barDockControlRight.Size = new System.Drawing.Size(0, 134);
      // 
      // repositoryItemMarqueeProgressBar1
      // 
      this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
      // 
      // repositoryItemTextEdit1
      // 
      this.repositoryItemTextEdit1.AutoHeight = false;
      this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
      // 
      // LayoutData
      // 
      this.LayoutData.Controls.Add(this.teJnsTransaksi);
      this.LayoutData.Controls.Add(this.teNoSppa);
      this.LayoutData.Controls.Add(this.teTglSusut);
      this.LayoutData.Controls.Add(this.teJnsSusut);
      this.LayoutData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.LayoutData.Location = new System.Drawing.Point(0, 40);
      this.LayoutData.Name = "LayoutData";
      this.LayoutData.OptionsView.UseDefaultDragAndDropRendering = false;
      this.LayoutData.Root = this.layoutControlGroup1;
      this.LayoutData.Size = new System.Drawing.Size(382, 134);
      this.LayoutData.TabIndex = 4;
      this.LayoutData.Text = "layoutControl1";
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
            this.layoutControlItem4});
      this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
      this.layoutControlGroup1.Name = "layoutControlGroup1";
      this.layoutControlGroup1.Size = new System.Drawing.Size(382, 134);
      this.layoutControlGroup1.Text = "layoutControlGroup1";
      this.layoutControlGroup1.TextVisible = false;
      // 
      // teJnsSusut
      // 
      this.teJnsSusut.Location = new System.Drawing.Point(88, 12);
      this.teJnsSusut.MenuManager = this.barManager1;
      this.teJnsSusut.Name = "teJnsSusut";
      this.teJnsSusut.Properties.ReadOnly = true;
      this.teJnsSusut.Size = new System.Drawing.Size(282, 20);
      this.teJnsSusut.StyleController = this.LayoutData;
      this.teJnsSusut.TabIndex = 4;
      // 
      // layoutControlItem1
      // 
      this.layoutControlItem1.Control = this.teJnsSusut;
      this.layoutControlItem1.CustomizationFormText = "Jenis Susut";
      this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
      this.layoutControlItem1.Name = "layoutControlItem1";
      this.layoutControlItem1.Size = new System.Drawing.Size(362, 24);
      this.layoutControlItem1.Text = "Jenis Susut";
      this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 13);
      // 
      // teTglSusut
      // 
      this.teTglSusut.Location = new System.Drawing.Point(88, 36);
      this.teTglSusut.MenuManager = this.barManager1;
      this.teTglSusut.Name = "teTglSusut";
      this.teTglSusut.Properties.ReadOnly = true;
      this.teTglSusut.Size = new System.Drawing.Size(282, 20);
      this.teTglSusut.StyleController = this.LayoutData;
      this.teTglSusut.TabIndex = 5;
      // 
      // layoutControlItem2
      // 
      this.layoutControlItem2.Control = this.teTglSusut;
      this.layoutControlItem2.CustomizationFormText = "Tanggal Susut";
      this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
      this.layoutControlItem2.Name = "layoutControlItem2";
      this.layoutControlItem2.Size = new System.Drawing.Size(362, 24);
      this.layoutControlItem2.Text = "Tanggal Susut";
      this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 13);
      // 
      // teNoSppa
      // 
      this.teNoSppa.Location = new System.Drawing.Point(88, 60);
      this.teNoSppa.MenuManager = this.barManager1;
      this.teNoSppa.Name = "teNoSppa";
      this.teNoSppa.Properties.ReadOnly = true;
      this.teNoSppa.Size = new System.Drawing.Size(282, 20);
      this.teNoSppa.StyleController = this.LayoutData;
      this.teNoSppa.TabIndex = 6;
      // 
      // layoutControlItem3
      // 
      this.layoutControlItem3.Control = this.teNoSppa;
      this.layoutControlItem3.CustomizationFormText = "No SPPA";
      this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
      this.layoutControlItem3.Name = "layoutControlItem3";
      this.layoutControlItem3.Size = new System.Drawing.Size(362, 24);
      this.layoutControlItem3.Text = "No SPPA";
      this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 13);
      // 
      // teJnsTransaksi
      // 
      this.teJnsTransaksi.Location = new System.Drawing.Point(88, 84);
      this.teJnsTransaksi.MenuManager = this.barManager1;
      this.teJnsTransaksi.Name = "teJnsTransaksi";
      this.teJnsTransaksi.Properties.ReadOnly = true;
      this.teJnsTransaksi.Size = new System.Drawing.Size(282, 20);
      this.teJnsTransaksi.StyleController = this.LayoutData;
      this.teJnsTransaksi.TabIndex = 7;
      // 
      // layoutControlItem4
      // 
      this.layoutControlItem4.Control = this.teJnsTransaksi;
      this.layoutControlItem4.CustomizationFormText = "Jenis Transaksi";
      this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
      this.layoutControlItem4.Name = "layoutControlItem4";
      this.layoutControlItem4.Size = new System.Drawing.Size(362, 42);
      this.layoutControlItem4.Text = "Jenis Transaksi";
      this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 13);
      // 
      // frmPenyusutan
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(382, 199);
      this.Controls.Add(this.LayoutData);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.Name = "frmPenyusutan";
      this.Text = "frmPenyusutan";
      
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.LayoutData)).EndInit();
      this.LayoutData.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.teJnsSusut.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.teTglSusut.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.teNoSppa.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.teJnsTransaksi.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraBars.BarManager barManager1;
    public DevExpress.XtraBars.Bar bar2;
    public DevExpress.XtraBars.BarButtonItem bbiClose;
    private DevExpress.XtraBars.Bar bar3;
    private DevExpress.XtraBars.BarEditItem beMarqueeBar;
    private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar2;
    private DevExpress.XtraBars.BarDockControl barDockControlTop;
    private DevExpress.XtraBars.BarDockControl barDockControlBottom;
    private DevExpress.XtraBars.BarDockControl barDockControlLeft;
    private DevExpress.XtraBars.BarDockControl barDockControlRight;
    private DevExpress.XtraLayout.LayoutControl LayoutData;
    private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
    private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    private DevExpress.XtraEditors.TextEdit teTglSusut;
    private DevExpress.XtraEditors.TextEdit teJnsSusut;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    private DevExpress.XtraEditors.TextEdit teJnsTransaksi;
    private DevExpress.XtraEditors.TextEdit teNoSppa;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
  }
}