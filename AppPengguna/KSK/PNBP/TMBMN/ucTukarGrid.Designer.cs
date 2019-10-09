namespace AppPengguna.KSK.PNBP.TMBMN
{
    partial class ucTukarGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTukarGrid));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelTotData = new DevExpress.XtraEditors.LabelControl();
            this.sbCariOnline = new DevExpress.XtraEditors.SimpleButton();
            this.teNamaKolom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.teCari = new DevExpress.XtraEditors.TextEdit();
            this.gcGridSk = new DevExpress.XtraGrid.GridControl();
            this.gvGridSk = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NO_SK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TGL_SK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NTPN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TGL_SETOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NTB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KD_BILLING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TGL_TRANSAKSI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NM_PENYETOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teNamaKolom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCari.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGridSk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridSk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelTotData);
            this.layoutControl1.Controls.Add(this.sbCariOnline);
            this.layoutControl1.Controls.Add(this.teNamaKolom);
            this.layoutControl1.Controls.Add(this.teCari);
            this.layoutControl1.Controls.Add(this.gcGridSk);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(688, 392);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelTotData
            // 
            this.labelTotData.Location = new System.Drawing.Point(4, 375);
            this.labelTotData.Name = "labelTotData";
            this.labelTotData.Size = new System.Drawing.Size(61, 13);
            this.labelTotData.StyleController = this.layoutControl1;
            this.labelTotData.TabIndex = 8;
            this.labelTotData.Text = "Menampilkan";
            // 
            // sbCariOnline
            // 
            this.sbCariOnline.Image = ((System.Drawing.Image)(resources.GetObject("sbCariOnline.Image")));
            this.sbCariOnline.Location = new System.Drawing.Point(588, 4);
            this.sbCariOnline.Name = "sbCariOnline";
            this.sbCariOnline.Size = new System.Drawing.Size(96, 22);
            this.sbCariOnline.StyleController = this.layoutControl1;
            this.sbCariOnline.TabIndex = 7;
            this.sbCariOnline.Text = "Cari Online";
            this.sbCariOnline.Click += new System.EventHandler(this.sbCariOnline_Click);
            // 
            // teNamaKolom
            // 
            this.teNamaKolom.Location = new System.Drawing.Point(249, 4);
            this.teNamaKolom.Name = "teNamaKolom";
            this.teNamaKolom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teNamaKolom.Size = new System.Drawing.Size(135, 20);
            this.teNamaKolom.StyleController = this.layoutControl1;
            this.teNamaKolom.TabIndex = 6;
            this.teNamaKolom.EditValueChanged += new System.EventHandler(this.teNamaKolom_EditValueChanged);
            // 
            // teCari
            // 
            this.teCari.Location = new System.Drawing.Point(449, 4);
            this.teCari.Name = "teCari";
            this.teCari.Size = new System.Drawing.Size(135, 20);
            this.teCari.StyleController = this.layoutControl1;
            this.teCari.TabIndex = 5;
            this.teCari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teCari_KeyPress);
            // 
            // gcGridSk
            // 
            this.gcGridSk.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcGridSk.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcGridSk.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcGridSk.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcGridSk.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcGridSk.Location = new System.Drawing.Point(4, 30);
            this.gcGridSk.MainView = this.gvGridSk;
            this.gcGridSk.Name = "gcGridSk";
            this.gcGridSk.Size = new System.Drawing.Size(680, 341);
            this.gcGridSk.TabIndex = 4;
            this.gcGridSk.UseEmbeddedNavigator = true;
            this.gcGridSk.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGridSk});
            // 
            // gvGridSk
            // 
            this.gvGridSk.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NO,
            this.NO_SK,
            this.TGL_SK,
            this.NTPN,
            this.TGL_SETOR,
            this.NTB,
            this.KD_BILLING,
            this.TGL_TRANSAKSI,
            this.NM_PENYETOR,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gvGridSk.GridControl = this.gcGridSk;
            this.gvGridSk.Name = "gvGridSk";
            this.gvGridSk.OptionsBehavior.Editable = false;
            this.gvGridSk.OptionsBehavior.ReadOnly = true;
            this.gvGridSk.OptionsView.ColumnAutoWidth = false;
            this.gvGridSk.OptionsView.ShowAutoFilterRow = true;
            this.gvGridSk.OptionsView.ShowFooter = true;
            this.gvGridSk.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvGridSk_FocusedRowChanged);
            this.gvGridSk.ColumnFilterChanged += new System.EventHandler(this.gvGridSk_ColumnFilterChanged);
            this.gvGridSk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvGridSk_KeyPress);
            this.gvGridSk.DoubleClick += new System.EventHandler(this.gvGridSk_DoubleClick);
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
            this.NO.Width = 43;
            // 
            // NO_SK
            // 
            this.NO_SK.AppearanceHeader.Options.UseTextOptions = true;
            this.NO_SK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NO_SK.Caption = "NO SK";
            this.NO_SK.FieldName = "SK_KEPUTUSAN";
            this.NO_SK.Name = "NO_SK";
            this.NO_SK.Visible = true;
            this.NO_SK.VisibleIndex = 1;
            this.NO_SK.Width = 120;
            // 
            // TGL_SK
            // 
            this.TGL_SK.AppearanceHeader.Options.UseTextOptions = true;
            this.TGL_SK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TGL_SK.Caption = "TGL SK";
            this.TGL_SK.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.TGL_SK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TGL_SK.FieldName = "TGL_SK";
            this.TGL_SK.Name = "TGL_SK";
            this.TGL_SK.Visible = true;
            this.TGL_SK.VisibleIndex = 2;
            this.TGL_SK.Width = 109;
            // 
            // NTPN
            // 
            this.NTPN.AppearanceHeader.Options.UseTextOptions = true;
            this.NTPN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NTPN.Caption = "NTPN";
            this.NTPN.FieldName = "NTPN";
            this.NTPN.Name = "NTPN";
            this.NTPN.Visible = true;
            this.NTPN.VisibleIndex = 3;
            this.NTPN.Width = 119;
            // 
            // TGL_SETOR
            // 
            this.TGL_SETOR.AppearanceHeader.Options.UseTextOptions = true;
            this.TGL_SETOR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TGL_SETOR.Caption = "TGL SETOR";
            this.TGL_SETOR.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.TGL_SETOR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TGL_SETOR.FieldName = "TGL_SETOR";
            this.TGL_SETOR.Name = "TGL_SETOR";
            this.TGL_SETOR.Visible = true;
            this.TGL_SETOR.VisibleIndex = 4;
            this.TGL_SETOR.Width = 119;
            // 
            // NTB
            // 
            this.NTB.AppearanceHeader.Options.UseTextOptions = true;
            this.NTB.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NTB.Caption = "NTB";
            this.NTB.FieldName = "NTB";
            this.NTB.Name = "NTB";
            this.NTB.Visible = true;
            this.NTB.VisibleIndex = 5;
            this.NTB.Width = 111;
            // 
            // KD_BILLING
            // 
            this.KD_BILLING.AppearanceHeader.Options.UseTextOptions = true;
            this.KD_BILLING.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KD_BILLING.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.KD_BILLING.Caption = "KODE BILLING";
            this.KD_BILLING.FieldName = "KD_BILLING";
            this.KD_BILLING.Name = "KD_BILLING";
            this.KD_BILLING.OptionsColumn.AllowEdit = false;
            this.KD_BILLING.Visible = true;
            this.KD_BILLING.VisibleIndex = 6;
            // 
            // TGL_TRANSAKSI
            // 
            this.TGL_TRANSAKSI.AppearanceHeader.Options.UseTextOptions = true;
            this.TGL_TRANSAKSI.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TGL_TRANSAKSI.Caption = "TGL TRANSAKSI";
            this.TGL_TRANSAKSI.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.TGL_TRANSAKSI.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TGL_TRANSAKSI.FieldName = "TGL_TRANSAKSI";
            this.TGL_TRANSAKSI.Name = "TGL_TRANSAKSI";
            this.TGL_TRANSAKSI.Visible = true;
            this.TGL_TRANSAKSI.VisibleIndex = 7;
            this.TGL_TRANSAKSI.Width = 209;
            // 
            // NM_PENYETOR
            // 
            this.NM_PENYETOR.AppearanceHeader.Options.UseTextOptions = true;
            this.NM_PENYETOR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NM_PENYETOR.Caption = "NAMA PENYETOR";
            this.NM_PENYETOR.FieldName = "NM_PENYETOR";
            this.NM_PENYETOR.Name = "NM_PENYETOR";
            this.NM_PENYETOR.Visible = true;
            this.NM_PENYETOR.VisibleIndex = 8;
            this.NM_PENYETOR.Width = 215;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "KODE AKUN";
            this.gridColumn1.FieldName = "KD_AKUN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 9;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "AKUN";
            this.gridColumn2.FieldName = "UR_AKUN";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 10;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "NILAI PNBP";
            this.gridColumn3.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "NILAI_PNBP";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NILAI_PNBP", "{0:n0}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 11;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "KETERANGAN";
            this.gridColumn4.FieldName = "KET";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 12;
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
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(688, 392);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(184, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcGridSk;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(684, 345);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teCari;
            this.layoutControlItem2.CustomizationFormText = "Kata Kunci";
            this.layoutControlItem2.Location = new System.Drawing.Point(384, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(250, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(200, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Kata Kunci";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNamaKolom;
            this.layoutControlItem3.CustomizationFormText = "Nama Kolom";
            this.layoutControlItem3.Location = new System.Drawing.Point(184, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(250, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(200, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Nama Kolom";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbCariOnline;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(584, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labelTotData;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 371);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(684, 17);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // ucTukarGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucTukarGrid";
            this.Size = new System.Drawing.Size(688, 392);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teNamaKolom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCari.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGridSk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridSk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.GridControl gcGridSk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraEditors.SimpleButton sbCariOnline;
        public DevExpress.XtraEditors.ComboBoxEdit teNamaKolom;
        public DevExpress.XtraEditors.TextEdit teCari;
        public DevExpress.XtraGrid.Views.Grid.GridView gvGridSk;
        private DevExpress.XtraGrid.Columns.GridColumn NO;
        private DevExpress.XtraGrid.Columns.GridColumn NO_SK;
        private DevExpress.XtraGrid.Columns.GridColumn TGL_SK;
        private DevExpress.XtraGrid.Columns.GridColumn NTPN;
        private DevExpress.XtraGrid.Columns.GridColumn TGL_SETOR;
        private DevExpress.XtraGrid.Columns.GridColumn NTB;
        private DevExpress.XtraGrid.Columns.GridColumn TGL_TRANSAKSI;
        private DevExpress.XtraGrid.Columns.GridColumn NM_PENYETOR;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        public DevExpress.XtraEditors.LabelControl labelTotData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn KD_BILLING;
    }
}
