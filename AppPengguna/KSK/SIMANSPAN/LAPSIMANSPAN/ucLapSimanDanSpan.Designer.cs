namespace AppPengguna.KSK.SIMANSPAN.LAPSIMANSPAN
{
    partial class ucLapSimanDanSpan
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.NUM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.TAHUN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.KD_SATKER = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.UR_SATKER = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.BULAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.TOT_TRANSAKSI_SAMA = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.TOT_PNBP_SAMA = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.TOT_TRANSAKSI_KOSONGDISPAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand12 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.NON_SPAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand13 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand14 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.TOT_TRANSAKSI_KOSONGDISIMAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand15 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.NON_SIMAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelTotData = new DevExpress.XtraLayout.SimpleLabelItem();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelTotData)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(981, 560);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.advBandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(957, 519);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView1});
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5,
            this.gridBand6,
            this.gridBand7,
            this.gridBand10,
            this.gridBand13});
            this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.NUM,
            this.TAHUN,
            this.KD_SATKER,
            this.UR_SATKER,
            this.BULAN,
            this.TOT_TRANSAKSI_SAMA,
            this.TOT_PNBP_SAMA,
            this.TOT_TRANSAKSI_KOSONGDISPAN,
            this.NON_SPAN,
            this.TOT_TRANSAKSI_KOSONGDISIMAN,
            this.NON_SIMAN});
            this.advBandedGridView1.GridControl = this.gridControl1;
            this.advBandedGridView1.Name = "advBandedGridView1";
            this.advBandedGridView1.OptionsView.ColumnAutoWidth = true;
            this.advBandedGridView1.OptionsView.ShowColumnHeaders = false;
            this.advBandedGridView1.OptionsView.ShowFooter = true;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand2.Caption = "NO";
            this.gridBand2.Columns.Add(this.NUM);
            this.gridBand2.MinWidth = 30;
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.OptionsBand.FixedWidth = true;
            this.gridBand2.VisibleIndex = 0;
            this.gridBand2.Width = 44;
            // 
            // NUM
            // 
            this.NUM.AppearanceCell.Options.UseTextOptions = true;
            this.NUM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NUM.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.NUM.AppearanceHeader.Options.UseTextOptions = true;
            this.NUM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NUM.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.NUM.Caption = "NO";
            this.NUM.FieldName = "NUM";
            this.NUM.Name = "NUM";
            this.NUM.OptionsColumn.AllowEdit = false;
            this.NUM.Visible = true;
            this.NUM.Width = 44;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand3.Caption = "TAHUN";
            this.gridBand3.Columns.Add(this.TAHUN);
            this.gridBand3.MinWidth = 50;
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.OptionsBand.FixedWidth = true;
            this.gridBand3.VisibleIndex = 1;
            // 
            // TAHUN
            // 
            this.TAHUN.AppearanceCell.Options.UseTextOptions = true;
            this.TAHUN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TAHUN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TAHUN.AppearanceHeader.Options.UseTextOptions = true;
            this.TAHUN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TAHUN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TAHUN.Caption = "TAHUN";
            this.TAHUN.FieldName = "TAHUN";
            this.TAHUN.Name = "TAHUN";
            this.TAHUN.OptionsColumn.AllowEdit = false;
            this.TAHUN.Visible = true;
            this.TAHUN.Width = 70;
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand4.Caption = "KODE SATKER";
            this.gridBand4.Columns.Add(this.KD_SATKER);
            this.gridBand4.MinWidth = 120;
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.OptionsBand.FixedWidth = true;
            this.gridBand4.VisibleIndex = 2;
            this.gridBand4.Width = 126;
            // 
            // KD_SATKER
            // 
            this.KD_SATKER.AppearanceHeader.Options.UseTextOptions = true;
            this.KD_SATKER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KD_SATKER.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.KD_SATKER.Caption = "KODE SATKER";
            this.KD_SATKER.FieldName = "KD_SATKER";
            this.KD_SATKER.Name = "KD_SATKER";
            this.KD_SATKER.OptionsColumn.AllowEdit = false;
            this.KD_SATKER.Visible = true;
            this.KD_SATKER.Width = 126;
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand5.Caption = "URAIAN SATKER";
            this.gridBand5.Columns.Add(this.UR_SATKER);
            this.gridBand5.MinWidth = 150;
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.VisibleIndex = 3;
            this.gridBand5.Width = 194;
            // 
            // UR_SATKER
            // 
            this.UR_SATKER.AppearanceHeader.Options.UseTextOptions = true;
            this.UR_SATKER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.UR_SATKER.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.UR_SATKER.Caption = "URAIAN SATKER";
            this.UR_SATKER.FieldName = "UR_SATKER";
            this.UR_SATKER.Name = "UR_SATKER";
            this.UR_SATKER.OptionsColumn.AllowEdit = false;
            this.UR_SATKER.Visible = true;
            this.UR_SATKER.Width = 194;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand6.Caption = "BULAN";
            this.gridBand6.Columns.Add(this.BULAN);
            this.gridBand6.MinWidth = 50;
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.OptionsBand.FixedWidth = true;
            this.gridBand6.VisibleIndex = 4;
            this.gridBand6.Width = 59;
            // 
            // BULAN
            // 
            this.BULAN.AppearanceCell.Options.UseTextOptions = true;
            this.BULAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BULAN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.BULAN.AppearanceHeader.Options.UseTextOptions = true;
            this.BULAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BULAN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.BULAN.Caption = "BULAN";
            this.BULAN.FieldName = "BULAN";
            this.BULAN.Name = "BULAN";
            this.BULAN.OptionsColumn.AllowEdit = false;
            this.BULAN.Visible = true;
            this.BULAN.Width = 59;
            // 
            // gridBand7
            // 
            this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand7.Caption = "SIMAN SPAN SAMA";
            this.gridBand7.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand8,
            this.gridBand9});
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.VisibleIndex = 5;
            this.gridBand7.Width = 278;
            // 
            // gridBand8
            // 
            this.gridBand8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand8.Caption = "Jumlah Transaksi";
            this.gridBand8.Columns.Add(this.TOT_TRANSAKSI_SAMA);
            this.gridBand8.MinWidth = 100;
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.OptionsBand.FixedWidth = true;
            this.gridBand8.VisibleIndex = 0;
            this.gridBand8.Width = 152;
            // 
            // TOT_TRANSAKSI_SAMA
            // 
            this.TOT_TRANSAKSI_SAMA.AppearanceCell.Options.UseTextOptions = true;
            this.TOT_TRANSAKSI_SAMA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TOT_TRANSAKSI_SAMA.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TOT_TRANSAKSI_SAMA.AppearanceHeader.Options.UseTextOptions = true;
            this.TOT_TRANSAKSI_SAMA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TOT_TRANSAKSI_SAMA.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TOT_TRANSAKSI_SAMA.Caption = "JUMLAH TRANSAKSI A";
            this.TOT_TRANSAKSI_SAMA.DisplayFormat.FormatString = "n0";
            this.TOT_TRANSAKSI_SAMA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TOT_TRANSAKSI_SAMA.FieldName = "TOT_TRANSAKSI_SAMA";
            this.TOT_TRANSAKSI_SAMA.Name = "TOT_TRANSAKSI_SAMA";
            this.TOT_TRANSAKSI_SAMA.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOT_TRANSAKSI_SAMA", "{0:n0}")});
            this.TOT_TRANSAKSI_SAMA.Visible = true;
            this.TOT_TRANSAKSI_SAMA.Width = 152;
            // 
            // gridBand9
            // 
            this.gridBand9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand9.Caption = "Nilai PNBP";
            this.gridBand9.Columns.Add(this.TOT_PNBP_SAMA);
            this.gridBand9.MinWidth = 100;
            this.gridBand9.Name = "gridBand9";
            this.gridBand9.OptionsBand.FixedWidth = true;
            this.gridBand9.VisibleIndex = 1;
            this.gridBand9.Width = 126;
            // 
            // TOT_PNBP_SAMA
            // 
            this.TOT_PNBP_SAMA.AppearanceCell.Options.UseTextOptions = true;
            this.TOT_PNBP_SAMA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TOT_PNBP_SAMA.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TOT_PNBP_SAMA.AppearanceHeader.Options.UseTextOptions = true;
            this.TOT_PNBP_SAMA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TOT_PNBP_SAMA.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TOT_PNBP_SAMA.Caption = "SIMAN SPAN SAMA";
            this.TOT_PNBP_SAMA.DisplayFormat.FormatString = "n0";
            this.TOT_PNBP_SAMA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TOT_PNBP_SAMA.FieldName = "TOT_PNBP_SAMA";
            this.TOT_PNBP_SAMA.Name = "TOT_PNBP_SAMA";
            this.TOT_PNBP_SAMA.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOT_PNBP_SAMA", "{0:n0}")});
            this.TOT_PNBP_SAMA.Visible = true;
            this.TOT_PNBP_SAMA.Width = 126;
            // 
            // gridBand10
            // 
            this.gridBand10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand10.Caption = "Tidak ada di SPAN";
            this.gridBand10.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand11,
            this.gridBand12});
            this.gridBand10.Name = "gridBand10";
            this.gridBand10.VisibleIndex = 6;
            this.gridBand10.Width = 246;
            // 
            // gridBand11
            // 
            this.gridBand11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand11.Caption = "Jumlah Transaksi";
            this.gridBand11.Columns.Add(this.TOT_TRANSAKSI_KOSONGDISPAN);
            this.gridBand11.MinWidth = 100;
            this.gridBand11.Name = "gridBand11";
            this.gridBand11.OptionsBand.FixedWidth = true;
            this.gridBand11.VisibleIndex = 0;
            this.gridBand11.Width = 146;
            // 
            // TOT_TRANSAKSI_KOSONGDISPAN
            // 
            this.TOT_TRANSAKSI_KOSONGDISPAN.AppearanceCell.Options.UseTextOptions = true;
            this.TOT_TRANSAKSI_KOSONGDISPAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TOT_TRANSAKSI_KOSONGDISPAN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TOT_TRANSAKSI_KOSONGDISPAN.AppearanceHeader.Options.UseTextOptions = true;
            this.TOT_TRANSAKSI_KOSONGDISPAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TOT_TRANSAKSI_KOSONGDISPAN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TOT_TRANSAKSI_KOSONGDISPAN.Caption = "JUMLAH TRANSAKSI B";
            this.TOT_TRANSAKSI_KOSONGDISPAN.DisplayFormat.FormatString = "n0";
            this.TOT_TRANSAKSI_KOSONGDISPAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TOT_TRANSAKSI_KOSONGDISPAN.FieldName = "TOT_TRANSAKSI_KOSONGDISPAN";
            this.TOT_TRANSAKSI_KOSONGDISPAN.Name = "TOT_TRANSAKSI_KOSONGDISPAN";
            this.TOT_TRANSAKSI_KOSONGDISPAN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOT_TRANSAKSI_KOSONGDISPAN", "{0:n0}")});
            this.TOT_TRANSAKSI_KOSONGDISPAN.Visible = true;
            this.TOT_TRANSAKSI_KOSONGDISPAN.Width = 146;
            // 
            // gridBand12
            // 
            this.gridBand12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand12.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand12.Caption = "Nilai PNBP";
            this.gridBand12.Columns.Add(this.NON_SPAN);
            this.gridBand12.MinWidth = 100;
            this.gridBand12.Name = "gridBand12";
            this.gridBand12.OptionsBand.FixedWidth = true;
            this.gridBand12.VisibleIndex = 1;
            this.gridBand12.Width = 100;
            // 
            // NON_SPAN
            // 
            this.NON_SPAN.AppearanceCell.Options.UseTextOptions = true;
            this.NON_SPAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.NON_SPAN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.NON_SPAN.AppearanceHeader.Options.UseTextOptions = true;
            this.NON_SPAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NON_SPAN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.NON_SPAN.Caption = "Tidak Ada di SPAN";
            this.NON_SPAN.DisplayFormat.FormatString = "n0";
            this.NON_SPAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.NON_SPAN.FieldName = "TOT_PNBP_KOSONGDISPAN";
            this.NON_SPAN.Name = "NON_SPAN";
            this.NON_SPAN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOT_PNBP_KOSONGDISPAN", "{0:n0}")});
            this.NON_SPAN.Visible = true;
            this.NON_SPAN.Width = 100;
            // 
            // gridBand13
            // 
            this.gridBand13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand13.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand13.Caption = "Tidak ada di SIMAN";
            this.gridBand13.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand14,
            this.gridBand15});
            this.gridBand13.Name = "gridBand13";
            this.gridBand13.VisibleIndex = 7;
            this.gridBand13.Width = 233;
            // 
            // gridBand14
            // 
            this.gridBand14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand14.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand14.Caption = "Jumlah Transaksi";
            this.gridBand14.Columns.Add(this.TOT_TRANSAKSI_KOSONGDISIMAN);
            this.gridBand14.MinWidth = 100;
            this.gridBand14.Name = "gridBand14";
            this.gridBand14.OptionsBand.FixedWidth = true;
            this.gridBand14.VisibleIndex = 0;
            this.gridBand14.Width = 133;
            // 
            // TOT_TRANSAKSI_KOSONGDISIMAN
            // 
            this.TOT_TRANSAKSI_KOSONGDISIMAN.AppearanceCell.Options.UseTextOptions = true;
            this.TOT_TRANSAKSI_KOSONGDISIMAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TOT_TRANSAKSI_KOSONGDISIMAN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TOT_TRANSAKSI_KOSONGDISIMAN.AppearanceHeader.Options.UseTextOptions = true;
            this.TOT_TRANSAKSI_KOSONGDISIMAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TOT_TRANSAKSI_KOSONGDISIMAN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TOT_TRANSAKSI_KOSONGDISIMAN.Caption = "JUMLAH TRANSAKSI C";
            this.TOT_TRANSAKSI_KOSONGDISIMAN.DisplayFormat.FormatString = "n0";
            this.TOT_TRANSAKSI_KOSONGDISIMAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TOT_TRANSAKSI_KOSONGDISIMAN.FieldName = "TOT_TRANSAKSI_KOSONGDISIMAN";
            this.TOT_TRANSAKSI_KOSONGDISIMAN.Name = "TOT_TRANSAKSI_KOSONGDISIMAN";
            this.TOT_TRANSAKSI_KOSONGDISIMAN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOT_TRANSAKSI_KOSONGDISIMAN", "{0:n0}")});
            this.TOT_TRANSAKSI_KOSONGDISIMAN.Visible = true;
            this.TOT_TRANSAKSI_KOSONGDISIMAN.Width = 133;
            // 
            // gridBand15
            // 
            this.gridBand15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand15.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridBand15.Caption = "Nilai PNBP";
            this.gridBand15.Columns.Add(this.NON_SIMAN);
            this.gridBand15.MinWidth = 100;
            this.gridBand15.Name = "gridBand15";
            this.gridBand15.VisibleIndex = 1;
            this.gridBand15.Width = 100;
            // 
            // NON_SIMAN
            // 
            this.NON_SIMAN.AppearanceCell.Options.UseTextOptions = true;
            this.NON_SIMAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.NON_SIMAN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.NON_SIMAN.AppearanceHeader.Options.UseTextOptions = true;
            this.NON_SIMAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NON_SIMAN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.NON_SIMAN.Caption = "Tidak Ada di SIMAN";
            this.NON_SIMAN.DisplayFormat.FormatString = "n0";
            this.NON_SIMAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.NON_SIMAN.FieldName = "TOT_PNBP_KOSONGDISIMAN";
            this.NON_SIMAN.Name = "NON_SIMAN";
            this.NON_SIMAN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOT_PNBP_KOSONGDISIMAN", "{0:n0}")});
            this.NON_SIMAN.Visible = true;
            this.NON_SIMAN.Width = 100;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.labelTotData});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(981, 560);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(961, 523);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // labelTotData
            // 
            this.labelTotData.AllowHotTrack = false;
            this.labelTotData.CustomizationFormText = "LabellabelTotData";
            this.labelTotData.Location = new System.Drawing.Point(0, 523);
            this.labelTotData.Name = "labelTotData";
            this.labelTotData.Size = new System.Drawing.Size(961, 17);
            this.labelTotData.Text = "LabellabelTotData";
            this.labelTotData.TextSize = new System.Drawing.Size(86, 13);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = -1;
            // 
            // ucLapSimanDanSpan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucLapSimanDanSpan";
            this.Size = new System.Drawing.Size(981, 560);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelTotData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraLayout.SimpleLabelItem labelTotData;
        public DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn NUM;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TAHUN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn KD_SATKER;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn UR_SATKER;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn BULAN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TOT_PNBP_SAMA;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn NON_SPAN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn NON_SIMAN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TOT_TRANSAKSI_SAMA;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand9;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand10;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TOT_TRANSAKSI_KOSONGDISPAN;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand12;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand13;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TOT_TRANSAKSI_KOSONGDISIMAN;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand15;
    }
}
