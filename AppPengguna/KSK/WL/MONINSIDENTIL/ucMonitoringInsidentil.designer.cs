namespace AppPengguna.KSK.WL.MONINSIDENTIL
{
    partial class ucMonitoringInsidentil
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bsMnt = new System.Windows.Forms.BindingSource(this.components);
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNUM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colKD_KL = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colUR_KL = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colKD_SATKER = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colUR_SATKER = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand12 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDASAR = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand13 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colKD_BRG = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand14 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colUR_SSKEL = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand15 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNUP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand16 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colHSL_PEMANTAUAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand17 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand18 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNO_LAP_HSL_PEMANTAUAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand19 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colTGL_LAP_HSL_PEMANTAUAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTGL_CETAK = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 476);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsMnt;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(680, 472);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bsMnt
            // 
            this.bsMnt.DataSource = typeof(AppPengguna.SvcMonSatkerInsidentilA1.WASDALSROW_MON_SATKER_INSIDENTIL);
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand6,
            this.gridBand11,
            this.gridBand17});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colNUM,
            this.colKD_KL,
            this.colUR_KL,
            this.colKD_SATKER,
            this.colUR_SATKER,
            this.colDASAR,
            this.colKD_BRG,
            this.colUR_SSKEL,
            this.colNUP,
            this.colHSL_PEMANTAUAN,
            this.colNO_LAP_HSL_PEMANTAUAN,
            this.colTGL_LAP_HSL_PEMANTAUAN,
            this.colTGL_CETAK});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.Editable = false;
            this.bandedGridView1.OptionsView.ColumnAutoWidth = false;
            this.bandedGridView1.OptionsView.ShowColumnHeaders = false;
            this.bandedGridView1.OptionsView.ShowFooter = true;
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            this.bandedGridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.bandedGridView1_CustomUnboundColumnData);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "NO";
            this.gridBand1.Columns.Add(this.colNUM);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 45;
            // 
            // colNUM
            // 
            this.colNUM.FieldName = "NUM";
            this.colNUM.Name = "colNUM";
            this.colNUM.Visible = true;
            this.colNUM.Width = 45;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.Caption = "Uraian Kementrian  / Lembaga";
            this.gridBand6.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand7,
            this.gridBand8,
            this.gridBand10,
            this.gridBand9});
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 1;
            this.gridBand6.Width = 374;
            // 
            // gridBand7
            // 
            this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand7.Caption = "Kode UAPB";
            this.gridBand7.Columns.Add(this.colKD_KL);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.VisibleIndex = 0;
            this.gridBand7.Width = 61;
            // 
            // colKD_KL
            // 
            this.colKD_KL.AppearanceCell.Options.UseTextOptions = true;
            this.colKD_KL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKD_KL.FieldName = "KD_KL";
            this.colKD_KL.Name = "colKD_KL";
            this.colKD_KL.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KD_KL", "JUMLAH")});
            this.colKD_KL.Visible = true;
            this.colKD_KL.Width = 61;
            // 
            // gridBand8
            // 
            this.gridBand8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand8.Caption = "Uraian UAPB";
            this.gridBand8.Columns.Add(this.colUR_KL);
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.VisibleIndex = 1;
            this.gridBand8.Width = 115;
            // 
            // colUR_KL
            // 
            this.colUR_KL.AppearanceCell.Options.UseTextOptions = true;
            this.colUR_KL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUR_KL.FieldName = "UR_KL";
            this.colUR_KL.Name = "colUR_KL";
            this.colUR_KL.Visible = true;
            this.colUR_KL.Width = 115;
            // 
            // gridBand10
            // 
            this.gridBand10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand10.Caption = "Kode Satker";
            this.gridBand10.Columns.Add(this.colKD_SATKER);
            this.gridBand10.Name = "gridBand10";
            this.gridBand10.VisibleIndex = 2;
            this.gridBand10.Width = 72;
            // 
            // colKD_SATKER
            // 
            this.colKD_SATKER.AppearanceCell.Options.UseTextOptions = true;
            this.colKD_SATKER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKD_SATKER.FieldName = "KD_SATKER";
            this.colKD_SATKER.Name = "colKD_SATKER";
            this.colKD_SATKER.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "KD_SATKER", "{0:n0} Satker")});
            this.colKD_SATKER.Visible = true;
            this.colKD_SATKER.Width = 72;
            // 
            // gridBand9
            // 
            this.gridBand9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand9.Caption = "Uraian Satker";
            this.gridBand9.Columns.Add(this.colUR_SATKER);
            this.gridBand9.Name = "gridBand9";
            this.gridBand9.VisibleIndex = 3;
            this.gridBand9.Width = 126;
            // 
            // colUR_SATKER
            // 
            this.colUR_SATKER.AppearanceCell.Options.UseTextOptions = true;
            this.colUR_SATKER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUR_SATKER.FieldName = "UR_SATKER";
            this.colUR_SATKER.Name = "colUR_SATKER";
            this.colUR_SATKER.Visible = true;
            this.colUR_SATKER.Width = 126;
            // 
            // gridBand11
            // 
            this.gridBand11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand11.Caption = "Pemantauan Insidentil";
            this.gridBand11.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand12,
            this.gridBand13,
            this.gridBand14,
            this.gridBand15,
            this.gridBand16});
            this.gridBand11.Name = "gridBand11";
            this.gridBand11.VisibleIndex = 2;
            this.gridBand11.Width = 422;
            // 
            // gridBand12
            // 
            this.gridBand12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand12.Caption = "Dasar";
            this.gridBand12.Columns.Add(this.colDASAR);
            this.gridBand12.Name = "gridBand12";
            this.gridBand12.VisibleIndex = 0;
            this.gridBand12.Width = 75;
            // 
            // colDASAR
            // 
            this.colDASAR.AppearanceCell.Options.UseTextOptions = true;
            this.colDASAR.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDASAR.FieldName = "DASAR";
            this.colDASAR.Name = "colDASAR";
            this.colDASAR.Visible = true;
            // 
            // gridBand13
            // 
            this.gridBand13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand13.Caption = "Kode Barang";
            this.gridBand13.Columns.Add(this.colKD_BRG);
            this.gridBand13.Name = "gridBand13";
            this.gridBand13.VisibleIndex = 1;
            this.gridBand13.Width = 75;
            // 
            // colKD_BRG
            // 
            this.colKD_BRG.AppearanceCell.Options.UseTextOptions = true;
            this.colKD_BRG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKD_BRG.FieldName = "KD_BRG";
            this.colKD_BRG.Name = "colKD_BRG";
            this.colKD_BRG.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "KD_BRG", "{0:n0}")});
            this.colKD_BRG.Visible = true;
            // 
            // gridBand14
            // 
            this.gridBand14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand14.Caption = "Uraian Barang";
            this.gridBand14.Columns.Add(this.colUR_SSKEL);
            this.gridBand14.Name = "gridBand14";
            this.gridBand14.VisibleIndex = 2;
            this.gridBand14.Width = 75;
            // 
            // colUR_SSKEL
            // 
            this.colUR_SSKEL.AppearanceCell.Options.UseTextOptions = true;
            this.colUR_SSKEL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUR_SSKEL.FieldName = "UR_SSKEL";
            this.colUR_SSKEL.Name = "colUR_SSKEL";
            this.colUR_SSKEL.Visible = true;
            // 
            // gridBand15
            // 
            this.gridBand15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand15.Caption = "NUP";
            this.gridBand15.Columns.Add(this.colNUP);
            this.gridBand15.Name = "gridBand15";
            this.gridBand15.VisibleIndex = 3;
            this.gridBand15.Width = 44;
            // 
            // colNUP
            // 
            this.colNUP.AppearanceCell.Options.UseTextOptions = true;
            this.colNUP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNUP.FieldName = "NUP";
            this.colNUP.Name = "colNUP";
            this.colNUP.Visible = true;
            this.colNUP.Width = 44;
            // 
            // gridBand16
            // 
            this.gridBand16.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand16.Caption = "Hasil Pemantauan";
            this.gridBand16.Columns.Add(this.colHSL_PEMANTAUAN);
            this.gridBand16.Name = "gridBand16";
            this.gridBand16.VisibleIndex = 4;
            this.gridBand16.Width = 153;
            // 
            // colHSL_PEMANTAUAN
            // 
            this.colHSL_PEMANTAUAN.AppearanceCell.Options.UseTextOptions = true;
            this.colHSL_PEMANTAUAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHSL_PEMANTAUAN.FieldName = "HSL_PEMANTAUAN";
            this.colHSL_PEMANTAUAN.Name = "colHSL_PEMANTAUAN";
            this.colHSL_PEMANTAUAN.Visible = true;
            this.colHSL_PEMANTAUAN.Width = 153;
            // 
            // gridBand17
            // 
            this.gridBand17.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand17.Caption = "Laporan Hasil Pemantauan";
            this.gridBand17.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand18,
            this.gridBand19});
            this.gridBand17.Name = "gridBand17";
            this.gridBand17.VisibleIndex = 3;
            this.gridBand17.Width = 150;
            // 
            // gridBand18
            // 
            this.gridBand18.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand18.Caption = "Nomor";
            this.gridBand18.Columns.Add(this.colNO_LAP_HSL_PEMANTAUAN);
            this.gridBand18.Name = "gridBand18";
            this.gridBand18.VisibleIndex = 0;
            this.gridBand18.Width = 75;
            // 
            // colNO_LAP_HSL_PEMANTAUAN
            // 
            this.colNO_LAP_HSL_PEMANTAUAN.AppearanceCell.Options.UseTextOptions = true;
            this.colNO_LAP_HSL_PEMANTAUAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNO_LAP_HSL_PEMANTAUAN.FieldName = "NO_LAP_HSL_PEMANTAUAN";
            this.colNO_LAP_HSL_PEMANTAUAN.Name = "colNO_LAP_HSL_PEMANTAUAN";
            this.colNO_LAP_HSL_PEMANTAUAN.Visible = true;
            // 
            // gridBand19
            // 
            this.gridBand19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand19.Caption = "Tanggal";
            this.gridBand19.Columns.Add(this.colTGL_LAP_HSL_PEMANTAUAN);
            this.gridBand19.Name = "gridBand19";
            this.gridBand19.VisibleIndex = 1;
            this.gridBand19.Width = 75;
            // 
            // colTGL_LAP_HSL_PEMANTAUAN
            // 
            this.colTGL_LAP_HSL_PEMANTAUAN.AppearanceCell.Options.UseTextOptions = true;
            this.colTGL_LAP_HSL_PEMANTAUAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTGL_LAP_HSL_PEMANTAUAN.FieldName = "uTGL_LAP_HSL_PEMANTAUAN";
            this.colTGL_LAP_HSL_PEMANTAUAN.Name = "colTGL_LAP_HSL_PEMANTAUAN";
            this.colTGL_LAP_HSL_PEMANTAUAN.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colTGL_LAP_HSL_PEMANTAUAN.Visible = true;
            // 
            // colTGL_CETAK
            // 
            this.colTGL_CETAK.AppearanceCell.Options.UseTextOptions = true;
            this.colTGL_CETAK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTGL_CETAK.FieldName = "TGL_CETAK";
            this.colTGL_CETAK.Name = "colTGL_CETAK";
            this.colTGL_CETAK.Visible = true;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 476);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(684, 476);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // ucMonitoringInsidentil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucMonitoringInsidentil";
            this.Size = new System.Drawing.Size(684, 476);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsMnt;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNUM;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKD_KL;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUR_KL;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKD_SATKER;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUR_SATKER;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand11;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand12;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDASAR;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand13;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKD_BRG;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUR_SSKEL;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand15;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNUP;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand16;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHSL_PEMANTAUAN;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand17;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand18;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNO_LAP_HSL_PEMANTAUAN;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand19;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTGL_LAP_HSL_PEMANTAUAN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTGL_CETAK;
    }
}
