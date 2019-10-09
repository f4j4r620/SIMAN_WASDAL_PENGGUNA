namespace AppPengguna.KSK.WL.PENERTIBAN
{
    partial class ucPenertiban
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
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.NO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.KODE_SATKER = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.URAIAN_SATKER = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.KODE_BARANG = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.NUP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.NO_REGISTRASI = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.URAIAN_SSKEL = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.JENIS_PENERTIBAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.KUASA_PENGGUNA_BARANG = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.KETERANGAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.THN_ANG = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTINDAK_LANJUT_HSL = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTGL_LAPORAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNO_LAPORAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDASAR_PENERTIBAN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPengunaan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPemindatangan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPemanfattan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand12 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand13 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
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
            this.gridBand2,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5,
            this.gridBand9,
            this.gridBand13});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.NO,
            this.KODE_SATKER,
            this.URAIAN_SATKER,
            this.KODE_BARANG,
            this.NUP,
            this.NO_REGISTRASI,
            this.URAIAN_SSKEL,
            this.JENIS_PENERTIBAN,
            this.KUASA_PENGGUNA_BARANG,
            this.KETERANGAN,
            this.THN_ANG,
            this.colTINDAK_LANJUT_HSL,
            this.colTGL_LAPORAN,
            this.colNO_LAPORAN,
            this.colDASAR_PENERTIBAN,
            this.colPengunaan,
            this.colPemindatangan,
            this.colPemanfattan});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.Editable = false;
            this.bandedGridView1.OptionsBehavior.ReadOnly = true;
            this.bandedGridView1.OptionsView.ColumnAutoWidth = false;
            this.bandedGridView1.OptionsView.ShowAutoFilterRow = true;
            this.bandedGridView1.OptionsView.ShowColumnHeaders = false;
            this.bandedGridView1.OptionsView.ShowFooter = true;
            this.bandedGridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.bandedGridView1_CustomUnboundColumnData);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand1.Caption = "NO";
            this.gridBand1.Columns.Add(this.NO);
            this.gridBand1.Columns.Add(this.KODE_SATKER);
            this.gridBand1.Columns.Add(this.URAIAN_SATKER);
            this.gridBand1.Columns.Add(this.NO_REGISTRASI);
            this.gridBand1.Columns.Add(this.JENIS_PENERTIBAN);
            this.gridBand1.Columns.Add(this.KUASA_PENGGUNA_BARANG);
            this.gridBand1.Columns.Add(this.KETERANGAN);
            this.gridBand1.Columns.Add(this.THN_ANG);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 50;
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
            // NO
            // 
            this.NO.AppearanceCell.Options.UseTextOptions = true;
            this.NO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NO.AppearanceHeader.Options.UseTextOptions = true;
            this.NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NO.Caption = "NO";
            this.NO.FieldName = "NUM";
            this.NO.Name = "NO";
            this.NO.Visible = true;
            this.NO.Width = 50;
            // 
            // KODE_SATKER
            // 
            this.KODE_SATKER.AppearanceCell.Options.UseTextOptions = true;
            this.KODE_SATKER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_SATKER.Caption = "KODE SATKER";
            this.KODE_SATKER.FieldName = "KD_SATKER";
            this.KODE_SATKER.Name = "KODE_SATKER";
            // 
            // URAIAN_SATKER
            // 
            this.URAIAN_SATKER.AppearanceCell.Options.UseTextOptions = true;
            this.URAIAN_SATKER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.URAIAN_SATKER.Caption = "URAIAN SATKER";
            this.URAIAN_SATKER.FieldName = "UR_SATKER";
            this.URAIAN_SATKER.Name = "URAIAN_SATKER";
            this.URAIAN_SATKER.Width = 88;
            // 
            // KODE_BARANG
            // 
            this.KODE_BARANG.AppearanceCell.Options.UseTextOptions = true;
            this.KODE_BARANG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_BARANG.Caption = "KODE BARANG";
            this.KODE_BARANG.FieldName = "KD_BRG";
            this.KODE_BARANG.Name = "KODE_BARANG";
            this.KODE_BARANG.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KD_BRG", "JUMLAH")});
            this.KODE_BARANG.Visible = true;
            this.KODE_BARANG.Width = 103;
            // 
            // NUP
            // 
            this.NUP.AppearanceCell.Options.UseTextOptions = true;
            this.NUP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NUP.Caption = "NUP";
            this.NUP.FieldName = "NUP";
            this.NUP.Name = "NUP";
            this.NUP.Visible = true;
            // 
            // NO_REGISTRASI
            // 
            this.NO_REGISTRASI.AppearanceCell.Options.UseTextOptions = true;
            this.NO_REGISTRASI.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NO_REGISTRASI.Caption = "NO REGISTRASI";
            this.NO_REGISTRASI.FieldName = "NOREG";
            this.NO_REGISTRASI.Name = "NO_REGISTRASI";
            this.NO_REGISTRASI.Width = 87;
            // 
            // URAIAN_SSKEL
            // 
            this.URAIAN_SSKEL.AppearanceCell.Options.UseTextOptions = true;
            this.URAIAN_SSKEL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.URAIAN_SSKEL.Caption = "URAIAN SSKEL";
            this.URAIAN_SSKEL.FieldName = "UR_SSKEL";
            this.URAIAN_SSKEL.Name = "URAIAN_SSKEL";
            this.URAIAN_SSKEL.Visible = true;
            this.URAIAN_SSKEL.Width = 119;
            // 
            // JENIS_PENERTIBAN
            // 
            this.JENIS_PENERTIBAN.AppearanceCell.Options.UseTextOptions = true;
            this.JENIS_PENERTIBAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JENIS_PENERTIBAN.Caption = "JENIS PENERTIBAN";
            this.JENIS_PENERTIBAN.FieldName = "BENTUK_PENERTIBAN";
            this.JENIS_PENERTIBAN.Name = "JENIS_PENERTIBAN";
            this.JENIS_PENERTIBAN.Width = 98;
            // 
            // KUASA_PENGGUNA_BARANG
            // 
            this.KUASA_PENGGUNA_BARANG.AppearanceCell.Options.UseTextOptions = true;
            this.KUASA_PENGGUNA_BARANG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KUASA_PENGGUNA_BARANG.Caption = "KUASA PENGGUNA BARANG";
            this.KUASA_PENGGUNA_BARANG.FieldName = "KUASA_PENGGUNA_BRG";
            this.KUASA_PENGGUNA_BARANG.Name = "KUASA_PENGGUNA_BARANG";
            // 
            // KETERANGAN
            // 
            this.KETERANGAN.AppearanceCell.Options.UseTextOptions = true;
            this.KETERANGAN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KETERANGAN.Caption = "KETERANGAN";
            this.KETERANGAN.FieldName = "KET";
            this.KETERANGAN.Name = "KETERANGAN";
            // 
            // THN_ANG
            // 
            this.THN_ANG.AppearanceCell.Options.UseTextOptions = true;
            this.THN_ANG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.THN_ANG.Caption = "TAHUN ANGGARAN";
            this.THN_ANG.FieldName = "THN_ANG";
            this.THN_ANG.Name = "THN_ANG";
            // 
            // colTINDAK_LANJUT_HSL
            // 
            this.colTINDAK_LANJUT_HSL.FieldName = "TINDAK_LANJUT_HSL";
            this.colTINDAK_LANJUT_HSL.Name = "colTINDAK_LANJUT_HSL";
            this.colTINDAK_LANJUT_HSL.Visible = true;
            this.colTINDAK_LANJUT_HSL.Width = 137;
            // 
            // colTGL_LAPORAN
            // 
            this.colTGL_LAPORAN.FieldName = "uTGL_LAPORAN";
            this.colTGL_LAPORAN.Name = "colTGL_LAPORAN";
            this.colTGL_LAPORAN.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colTGL_LAPORAN.Visible = true;
            // 
            // colNO_LAPORAN
            // 
            this.colNO_LAPORAN.FieldName = "NO_LAPORAN";
            this.colNO_LAPORAN.Name = "colNO_LAPORAN";
            this.colNO_LAPORAN.Visible = true;
            // 
            // colDASAR_PENERTIBAN
            // 
            this.colDASAR_PENERTIBAN.FieldName = "DASAR_PENERTIBAN";
            this.colDASAR_PENERTIBAN.Name = "colDASAR_PENERTIBAN";
            this.colDASAR_PENERTIBAN.Visible = true;
            this.colDASAR_PENERTIBAN.Width = 100;
            // 
            // colPengunaan
            // 
            this.colPengunaan.Caption = "Penggunaa";
            this.colPengunaan.FieldName = "uGuna";
            this.colPengunaan.Name = "colPengunaan";
            this.colPengunaan.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.colPengunaan.Visible = true;
            this.colPengunaan.Width = 89;
            // 
            // colPemindatangan
            // 
            this.colPemindatangan.Caption = "Pindah";
            this.colPemindatangan.FieldName = "uPindah";
            this.colPemindatangan.Name = "colPemindatangan";
            this.colPemindatangan.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.colPemindatangan.Visible = true;
            this.colPemindatangan.Width = 108;
            // 
            // colPemanfattan
            // 
            this.colPemanfattan.Caption = "manfaat";
            this.colPemanfattan.FieldName = "uManfaat";
            this.colPemanfattan.Name = "colPemanfattan";
            this.colPemanfattan.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.colPemanfattan.Visible = true;
            this.colPemanfattan.Width = 87;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand2.Caption = "KODE BARANG";
            this.gridBand2.Columns.Add(this.KODE_BARANG);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 103;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand3.Caption = "URAIAN BARANG";
            this.gridBand3.Columns.Add(this.URAIAN_SSKEL);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 2;
            this.gridBand3.Width = 119;
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand4.Caption = "NUP";
            this.gridBand4.Columns.Add(this.NUP);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 3;
            this.gridBand4.Width = 75;
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.Caption = "DASAR PENERTIBAN";
            this.gridBand5.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand6,
            this.gridBand7,
            this.gridBand8});
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.VisibleIndex = 4;
            this.gridBand5.Width = 250;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.Caption = "DOKUMEN SUMBER";
            this.gridBand6.Columns.Add(this.colDASAR_PENERTIBAN);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 0;
            this.gridBand6.Width = 100;
            // 
            // gridBand7
            // 
            this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand7.Caption = "NOMOR";
            this.gridBand7.Columns.Add(this.colNO_LAPORAN);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.VisibleIndex = 1;
            this.gridBand7.Width = 75;
            // 
            // gridBand8
            // 
            this.gridBand8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand8.Caption = "TANGGAL";
            this.gridBand8.Columns.Add(this.colTGL_LAPORAN);
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.VisibleIndex = 2;
            this.gridBand8.Width = 75;
            // 
            // gridBand9
            // 
            this.gridBand9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand9.Caption = "BENTUK PENERTIBAN";
            this.gridBand9.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand10,
            this.gridBand12,
            this.gridBand11});
            this.gridBand9.Name = "gridBand9";
            this.gridBand9.VisibleIndex = 5;
            this.gridBand9.Width = 284;
            // 
            // gridBand10
            // 
            this.gridBand10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand10.Caption = "PENGGUNAAN";
            this.gridBand10.Columns.Add(this.colPengunaan);
            this.gridBand10.Name = "gridBand10";
            this.gridBand10.VisibleIndex = 0;
            this.gridBand10.Width = 89;
            // 
            // gridBand12
            // 
            this.gridBand12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand12.Caption = "PEMANFAATAN";
            this.gridBand12.Columns.Add(this.colPemanfattan);
            this.gridBand12.Name = "gridBand12";
            this.gridBand12.VisibleIndex = 1;
            this.gridBand12.Width = 87;
            // 
            // gridBand11
            // 
            this.gridBand11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand11.Caption = "PEMINDAHTANGANAN";
            this.gridBand11.Columns.Add(this.colPemindatangan);
            this.gridBand11.Name = "gridBand11";
            this.gridBand11.VisibleIndex = 2;
            this.gridBand11.Width = 108;
            // 
            // gridBand13
            // 
            this.gridBand13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand13.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand13.Caption = "TINDAK LANJUT HASIL PEMANTAUAN DAN PENERTIBAN";
            this.gridBand13.Columns.Add(this.colTINDAK_LANJUT_HSL);
            this.gridBand13.Name = "gridBand13";
            this.gridBand13.VisibleIndex = 6;
            this.gridBand13.Width = 137;
            // 
            // ucPenertiban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucPenertiban";
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
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn NO;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn KODE_SATKER;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn URAIAN_SATKER;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn NO_REGISTRASI;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn JENIS_PENERTIBAN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn KUASA_PENGGUNA_BARANG;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn KETERANGAN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn THN_ANG;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn KODE_BARANG;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn URAIAN_SSKEL;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn NUP;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDASAR_PENERTIBAN;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNO_LAPORAN;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTGL_LAPORAN;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand9;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPengunaan;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand12;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPemanfattan;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPemindatangan;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand13;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTINDAK_LANJUT_HSL;
    }
}
