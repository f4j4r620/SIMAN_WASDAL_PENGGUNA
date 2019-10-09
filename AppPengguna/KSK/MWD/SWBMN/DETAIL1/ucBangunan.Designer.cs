namespace AppPengguna.KSK.MWD.SWBMN.DETAIL1
{
    partial class ucBangunan
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcBangunan = new DevExpress.XtraGrid.GridControl();
            this.gvBangunan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcThnAng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBangunan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBangunan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcBangunan);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1039, 406);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcBangunan
            // 
            gridLevelNode1.RelationName = "Level1";
            this.gcBangunan.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcBangunan.Location = new System.Drawing.Point(12, 12);
            this.gcBangunan.MainView = this.gvBangunan;
            this.gcBangunan.Name = "gcBangunan";
            this.gcBangunan.Size = new System.Drawing.Size(1015, 382);
            this.gcBangunan.TabIndex = 4;
            this.gcBangunan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBangunan});
            // 
            // gvBangunan
            // 
            this.gvBangunan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn14,
            this.gridColumn1,
            this.gridColumn12,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn13,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gcThnAng});
            this.gvBangunan.GridControl = this.gcBangunan;
            this.gvBangunan.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvBangunan.Name = "gvBangunan";
            this.gvBangunan.OptionsBehavior.Editable = false;
            this.gvBangunan.OptionsBehavior.ReadOnly = true;
            this.gvBangunan.OptionsView.ColumnAutoWidth = false;
            this.gvBangunan.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvBangunan_FocusedRowChanged);
            this.gvBangunan.DoubleClick += new System.EventHandler(this.gvBangunan_DoubleClick);
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "NO";
            this.gridColumn11.FieldName = "NUM";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            this.gridColumn11.Width = 45;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "KODE BARANG";
            this.gridColumn1.FieldName = "KD_BRG";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 137;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "NUP";
            this.gridColumn12.FieldName = "NUP";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            this.gridColumn12.Width = 45;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "NAMA BARANG";
            this.gridColumn2.FieldName = "UR_SSKEL";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 208;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "KUANTITAS";
            this.gridColumn3.FieldName = "KUANTITAS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 5;
            this.gridColumn3.Width = 78;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "NILAI PERSETUJUAN";
            this.gridColumn4.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "NILAI_PENETAPAN";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            this.gridColumn4.Width = 141;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "KODE STATUS";
            this.gridColumn5.FieldName = "KD_STATUS";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 7;
            this.gridColumn5.Width = 91;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "NAMA STATUS";
            this.gridColumn6.FieldName = "NM_STATUS";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 8;
            this.gridColumn6.Width = 118;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "PENGGUNAAN BMN";
            this.gridColumn13.FieldName = "GUNA_WASDAL";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 9;
            this.gridColumn13.Width = 161;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "NO SK";
            this.gridColumn7.FieldName = "NO_SURAT";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 10;
            this.gridColumn7.Width = 57;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "TANGGAL SK";
            this.gridColumn8.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn8.FieldName = "TANGGAL";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 11;
            this.gridColumn8.Width = 83;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "KODE LAYANAN";
            this.gridColumn9.FieldName = "KD_PELAYANAN";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 12;
            this.gridColumn9.Width = 96;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "NAMA LAYANAN";
            this.gridColumn10.FieldName = "NM_PELAYANAN";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 13;
            this.gridColumn10.Width = 155;
            // 
            // gcThnAng
            // 
            this.gcThnAng.Caption = "TAHUN ANGGARAN";
            this.gcThnAng.FieldName = "THN_ANG";
            this.gcThnAng.Name = "gcThnAng";
            this.gcThnAng.Visible = true;
            this.gcThnAng.VisibleIndex = 14;
            this.gcThnAng.Width = 137;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(1039, 406);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcBangunan;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1019, 386);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "KODE SATKER";
            this.gridColumn14.FieldName = "KD_SATKER";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 1;
            this.gridColumn14.Width = 159;
            // 
            // ucBangunan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucBangunan";
            this.Size = new System.Drawing.Size(1039, 406);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBangunan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBangunan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcBangunan;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBangunan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gcThnAng;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
    }
}
