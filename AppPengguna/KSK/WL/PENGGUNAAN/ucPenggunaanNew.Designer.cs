namespace AppPengguna.KSK.WL.PENGGUNAAN
{
    partial class ucPenggunaanNew
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcAngkutan = new DevExpress.XtraGrid.GridControl();
            this.BS = new System.Windows.Forms.BindingSource(this.components);
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colJNSBMN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.kuantitasTot = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.luasTot = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.nilPerlhTot = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.kuantitasPspY = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.luasPspY = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.nilPerlhPspY = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.kuantitaPspN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colKD_JNS_BMN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelTotData = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAngkutan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcAngkutan;
            this.gridView1.Name = "gridView1";
            // 
            // gcAngkutan
            // 
            this.gcAngkutan.DataSource = this.BS;
            this.gcAngkutan.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcAngkutan.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcAngkutan.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcAngkutan.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcAngkutan.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridLevelNode1.RelationName = "Level1";
            this.gcAngkutan.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcAngkutan.Location = new System.Drawing.Point(12, 12);
            this.gcAngkutan.MainView = this.bandedGridView1;
            this.gcAngkutan.Name = "gcAngkutan";
            this.gcAngkutan.Size = new System.Drawing.Size(1015, 365);
            this.gcAngkutan.TabIndex = 4;
            this.gcAngkutan.UseEmbeddedNavigator = true;
            this.gcAngkutan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1,
            this.gridView1});
            // 
            // BS
            // 
            this.BS.DataSource = typeof(AppPengguna.SvcMonSatkerGunaA1.WASDALSROW_MON_GUNA_SATKER_A1);
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3,
            this.gridBand8,
            this.gridBand4});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumn11,
            this.bandedGridColumn7,
            this.gridColumn14,
            this.kuantitasTot,
            this.luasTot,
            this.nilPerlhTot,
            this.kuantitasPspY,
            this.luasPspY,
            this.nilPerlhPspY,
            this.kuantitaPspN,
            this.bandedGridColumn2,
            this.bandedGridColumn3,
            this.bandedGridColumn1,
            this.bandedGridColumn4,
            this.bandedGridColumn5,
            this.bandedGridColumn6,
            this.colJNSBMN,
            this.colKD_JNS_BMN});
            this.bandedGridView1.GridControl = this.gcAngkutan;
            this.bandedGridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.Editable = false;
            this.bandedGridView1.OptionsBehavior.ReadOnly = true;
            this.bandedGridView1.OptionsPrint.AutoWidth = false;
            this.bandedGridView1.OptionsView.ColumnAutoWidth = false;
            this.bandedGridView1.OptionsView.ShowFooter = true;
            this.bandedGridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvAngkutan_FocusedRowChanged);
            this.bandedGridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.bandedGridView1_CustomUnboundColumnData);
            this.bandedGridView1.DoubleClick += new System.EventHandler(this.gvAngkutan_DoubleClick);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Columns.Add(this.colJNSBMN);
            this.gridBand1.Columns.Add(this.gridColumn11);
            this.gridBand1.Columns.Add(this.bandedGridColumn7);
            this.gridBand1.Columns.Add(this.gridColumn14);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 151;
            // 
            // colJNSBMN
            // 
            this.colJNSBMN.AppearanceHeader.Options.UseTextOptions = true;
            this.colJNSBMN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colJNSBMN.Caption = "JENIS BMN";
            this.colJNSBMN.FieldName = "uJNS_BMN";
            this.colJNSBMN.Name = "colJNSBMN";
            this.colJNSBMN.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colJNSBMN.Visible = true;
            this.colJNSBMN.Width = 151;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "NO";
            this.gridColumn11.FieldName = "NUM";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Width = 47;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn7.Caption = "KODE SATKER";
            this.bandedGridColumn7.FieldName = "KD_SATKER";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.Width = 87;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "URAIAN SATKER";
            this.gridColumn14.FieldName = "UR_SATKER";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Width = 159;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "BMN";
            this.gridBand2.Columns.Add(this.kuantitasTot);
            this.gridBand2.Columns.Add(this.luasTot);
            this.gridBand2.Columns.Add(this.nilPerlhTot);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 282;
            // 
            // kuantitasTot
            // 
            this.kuantitasTot.AppearanceHeader.Options.UseTextOptions = true;
            this.kuantitasTot.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.kuantitasTot.Caption = "KUANTITAS";
            this.kuantitasTot.DisplayFormat.FormatString = "n0";
            this.kuantitasTot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.kuantitasTot.FieldName = "KUANTITAS_TOT";
            this.kuantitasTot.Name = "kuantitasTot";
            this.kuantitasTot.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KUANTITAS_TOT", "{0:n0}")});
            this.kuantitasTot.Visible = true;
            this.kuantitasTot.Width = 79;
            // 
            // luasTot
            // 
            this.luasTot.AppearanceHeader.Options.UseTextOptions = true;
            this.luasTot.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.luasTot.Caption = "LUAS(M2) ";
            this.luasTot.DisplayFormat.FormatString = "n0";
            this.luasTot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.luasTot.FieldName = "LUAS_TOT";
            this.luasTot.Name = "luasTot";
            this.luasTot.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LUAS_TOT", "{0:n0}")});
            this.luasTot.Visible = true;
            this.luasTot.Width = 66;
            // 
            // nilPerlhTot
            // 
            this.nilPerlhTot.AppearanceHeader.Options.UseTextOptions = true;
            this.nilPerlhTot.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nilPerlhTot.Caption = "NILAI PEROLEHAN ";
            this.nilPerlhTot.DisplayFormat.FormatString = "n0";
            this.nilPerlhTot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nilPerlhTot.FieldName = "NIL_PERLH_TOT";
            this.nilPerlhTot.Name = "nilPerlhTot";
            this.nilPerlhTot.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NIL_PERLH_TOT", "{0:n0}")});
            this.nilPerlhTot.Visible = true;
            this.nilPerlhTot.Width = 137;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "SUDAH PSP";
            this.gridBand3.Columns.Add(this.kuantitasPspY);
            this.gridBand3.Columns.Add(this.luasPspY);
            this.gridBand3.Columns.Add(this.nilPerlhPspY);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 2;
            this.gridBand3.Width = 293;
            // 
            // kuantitasPspY
            // 
            this.kuantitasPspY.AppearanceHeader.Options.UseTextOptions = true;
            this.kuantitasPspY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.kuantitasPspY.Caption = "KUANTITAS";
            this.kuantitasPspY.DisplayFormat.FormatString = "n0";
            this.kuantitasPspY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.kuantitasPspY.FieldName = "KUANTITAS_PSP_Y";
            this.kuantitasPspY.Name = "kuantitasPspY";
            this.kuantitasPspY.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KUANTITAS_PSP_Y", "{0:n0}")});
            this.kuantitasPspY.Visible = true;
            this.kuantitasPspY.Width = 109;
            // 
            // luasPspY
            // 
            this.luasPspY.AppearanceHeader.Options.UseTextOptions = true;
            this.luasPspY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.luasPspY.Caption = "LUAS(M2)";
            this.luasPspY.DisplayFormat.FormatString = "n0";
            this.luasPspY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.luasPspY.FieldName = "LUAS_PSP_Y";
            this.luasPspY.Name = "luasPspY";
            this.luasPspY.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LUAS_PSP_Y", "{0:n0}")});
            this.luasPspY.Visible = true;
            this.luasPspY.Width = 77;
            // 
            // nilPerlhPspY
            // 
            this.nilPerlhPspY.AppearanceHeader.Options.UseTextOptions = true;
            this.nilPerlhPspY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nilPerlhPspY.Caption = "NILAI PEROLEHAN";
            this.nilPerlhPspY.DisplayFormat.FormatString = "n0";
            this.nilPerlhPspY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nilPerlhPspY.FieldName = "NIL_PERLH_PSP_Y";
            this.nilPerlhPspY.GroupFormat.FormatString = "n0";
            this.nilPerlhPspY.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nilPerlhPspY.Name = "nilPerlhPspY";
            this.nilPerlhPspY.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NIL_PERLH_PSP_Y", "{0:n0}")});
            this.nilPerlhPspY.Visible = true;
            this.nilPerlhPspY.Width = 107;
            // 
            // gridBand8
            // 
            this.gridBand8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand8.Caption = "BELUM PSP";
            this.gridBand8.Columns.Add(this.kuantitaPspN);
            this.gridBand8.Columns.Add(this.bandedGridColumn2);
            this.gridBand8.Columns.Add(this.bandedGridColumn3);
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.VisibleIndex = 3;
            this.gridBand8.Width = 286;
            // 
            // kuantitaPspN
            // 
            this.kuantitaPspN.AppearanceHeader.Options.UseTextOptions = true;
            this.kuantitaPspN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.kuantitaPspN.Caption = "KUANTITAS";
            this.kuantitaPspN.DisplayFormat.FormatString = "n0";
            this.kuantitaPspN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.kuantitaPspN.FieldName = "KUANTITAS_PSP_N";
            this.kuantitaPspN.Name = "kuantitaPspN";
            this.kuantitaPspN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KUANTITAS_PSP_N", "{0:n0}")});
            this.kuantitaPspN.Visible = true;
            this.kuantitaPspN.Width = 102;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn2.Caption = "LUAS(M2)";
            this.bandedGridColumn2.DisplayFormat.FormatString = "n0";
            this.bandedGridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn2.FieldName = "LUAS_PSP_N";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LUAS_PSP_N", "{0:n0}")});
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 81;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn3.Caption = "NILAI PEROLEHAN";
            this.bandedGridColumn3.DisplayFormat.FormatString = "n0";
            this.bandedGridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn3.FieldName = "NIL_PERLH_PSP_N";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NIL_PERLH_PSP_N", "{0:n0}")});
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.Width = 103;
            // 
            // gridBand4
            // 
            this.gridBand4.Columns.Add(this.bandedGridColumn1);
            this.gridBand4.Columns.Add(this.bandedGridColumn4);
            this.gridBand4.Columns.Add(this.bandedGridColumn5);
            this.gridBand4.Columns.Add(this.bandedGridColumn6);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 4;
            this.gridBand4.Width = 563;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn1.Caption = "DIPERGUNAKAN SESUAI TUSI";
            this.bandedGridColumn1.DisplayFormat.FormatString = "n0";
            this.bandedGridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn1.FieldName = "JML_GUNA_TUSI";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "JML_GUNA_TUSI", "{0:n0}")});
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 181;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn4.Caption = "TERINDIKASI IDLE";
            this.bandedGridColumn4.DisplayFormat.FormatString = "n0";
            this.bandedGridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn4.FieldName = "JML_IDLE";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "JML_IDLE", "{0:n0}")});
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 153;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn5.Caption = "DIPERGUNAKAN PIHAK LAIN";
            this.bandedGridColumn5.DisplayFormat.FormatString = "n0";
            this.bandedGridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn5.FieldName = "JML_GUNA_LAIN";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "JML_GUNA_LAIN", "{0:n0}")});
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.Width = 154;
            // 
            // bandedGridColumn6
            // 
            this.bandedGridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn6.Caption = "SENGKETA";
            this.bandedGridColumn6.DisplayFormat.FormatString = "n0";
            this.bandedGridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn6.FieldName = "JML_SENGKETA";
            this.bandedGridColumn6.Name = "bandedGridColumn6";
            this.bandedGridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "JML_SENGKETA", "{0:n0}")});
            this.bandedGridColumn6.Visible = true;
            // 
            // colKD_JNS_BMN
            // 
            this.colKD_JNS_BMN.FieldName = "KD_JNS_BMN";
            this.colKD_JNS_BMN.Name = "colKD_JNS_BMN";
            this.colKD_JNS_BMN.Visible = true;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelTotData);
            this.layoutControl1.Controls.Add(this.gcAngkutan);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1039, 406);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelTotData
            // 
            this.labelTotData.Location = new System.Drawing.Point(12, 381);
            this.labelTotData.Name = "labelTotData";
            this.labelTotData.Size = new System.Drawing.Size(61, 13);
            this.labelTotData.StyleController = this.layoutControl1;
            this.labelTotData.TabIndex = 5;
            this.labelTotData.Text = "Menampilkan";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1039, 406);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAngkutan;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1019, 369);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(65, 369);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(954, 17);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelTotData;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 369);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(65, 17);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // ucPenggunaanNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucPenggunaanNew";
            this.Size = new System.Drawing.Size(1039, 406);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAngkutan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcAngkutan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraEditors.LabelControl labelTotData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn kuantitasTot;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn luasTot;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn nilPerlhTot;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn kuantitasPspY;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn luasPspY;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn nilPerlhPspY;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn kuantitaPspN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colJNSBMN;
        private System.Windows.Forms.BindingSource BS;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKD_JNS_BMN;
    }
}
