﻿namespace AppPengguna.KES1.TL.PBMNS
{
    partial class ucRTLPBMNSGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRTLPBMNSGrid));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbCariOnline = new DevExpress.XtraEditors.SimpleButton();
            this.teNamaKolom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.teCari = new DevExpress.XtraEditors.TextEdit();
            this.gcGridPSPBMNLAINSk = new DevExpress.XtraGrid.GridControl();
            this.gvGridPSPBMNLAINSk = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NO_SK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TGL_SK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NO_BUKTI_LAKSANA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.JNS_BUKTI_LAKSANA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TGL_BUKTI_LAKSANA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KD_SATKER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UR_SATKER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teNamaKolom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCari.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGridPSPBMNLAINSk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridPSPBMNLAINSk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbCariOnline);
            this.layoutControl1.Controls.Add(this.teNamaKolom);
            this.layoutControl1.Controls.Add(this.teCari);
            this.layoutControl1.Controls.Add(this.gcGridPSPBMNLAINSk);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(774, 392);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbCariOnline
            // 
            this.sbCariOnline.Image = ((System.Drawing.Image)(resources.GetObject("sbCariOnline.Image")));
            this.sbCariOnline.Location = new System.Drawing.Point(674, 4);
            this.sbCariOnline.Name = "sbCariOnline";
            this.sbCariOnline.Size = new System.Drawing.Size(96, 22);
            this.sbCariOnline.StyleController = this.layoutControl1;
            this.sbCariOnline.TabIndex = 7;
            this.sbCariOnline.Text = "Cari Online";
            this.sbCariOnline.Click += new System.EventHandler(this.sbCariOnline_Click);
            // 
            // teNamaKolom
            // 
            this.teNamaKolom.Location = new System.Drawing.Point(273, 4);
            this.teNamaKolom.Name = "teNamaKolom";
            this.teNamaKolom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teNamaKolom.Size = new System.Drawing.Size(159, 20);
            this.teNamaKolom.StyleController = this.layoutControl1;
            this.teNamaKolom.TabIndex = 6;
            this.teNamaKolom.EditValueChanged += new System.EventHandler(this.teNamaKolom_EditValueChanged);
            // 
            // teCari
            // 
            this.teCari.Location = new System.Drawing.Point(498, 4);
            this.teCari.Name = "teCari";
            this.teCari.Size = new System.Drawing.Size(172, 20);
            this.teCari.StyleController = this.layoutControl1;
            this.teCari.TabIndex = 5;
            this.teCari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teCari_KeyPress);
            // 
            // gcGridPSPBMNLAINSk
            // 
            this.gcGridPSPBMNLAINSk.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcGridPSPBMNLAINSk.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcGridPSPBMNLAINSk.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcGridPSPBMNLAINSk.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcGridPSPBMNLAINSk.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcGridPSPBMNLAINSk.Location = new System.Drawing.Point(4, 30);
            this.gcGridPSPBMNLAINSk.MainView = this.gvGridPSPBMNLAINSk;
            this.gcGridPSPBMNLAINSk.Name = "gcGridPSPBMNLAINSk";
            this.gcGridPSPBMNLAINSk.Size = new System.Drawing.Size(766, 358);
            this.gcGridPSPBMNLAINSk.TabIndex = 4;
            this.gcGridPSPBMNLAINSk.UseEmbeddedNavigator = true;
            this.gcGridPSPBMNLAINSk.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGridPSPBMNLAINSk});
            // 
            // gvGridPSPBMNLAINSk
            // 
            this.gvGridPSPBMNLAINSk.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NO,
            this.NO_SK,
            this.TGL_SK,
            this.NO_BUKTI_LAKSANA,
            this.JNS_BUKTI_LAKSANA,
            this.TGL_BUKTI_LAKSANA,
            this.KD_SATKER,
            this.UR_SATKER});
            this.gvGridPSPBMNLAINSk.GridControl = this.gcGridPSPBMNLAINSk;
            this.gvGridPSPBMNLAINSk.Name = "gvGridPSPBMNLAINSk";
            this.gvGridPSPBMNLAINSk.OptionsBehavior.Editable = false;
            this.gvGridPSPBMNLAINSk.OptionsBehavior.ReadOnly = true;
            this.gvGridPSPBMNLAINSk.OptionsView.ColumnAutoWidth = false;
            this.gvGridPSPBMNLAINSk.OptionsView.ShowAutoFilterRow = true;
            this.gvGridPSPBMNLAINSk.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvGridSk_FocusedRowChanged);
            this.gvGridPSPBMNLAINSk.ColumnFilterChanged += new System.EventHandler(this.gvGridSk_ColumnFilterChanged);
            this.gvGridPSPBMNLAINSk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvGridSk_KeyPress);
            this.gvGridPSPBMNLAINSk.DoubleClick += new System.EventHandler(this.gvGridSk_DoubleClick);
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
            this.TGL_SK.FieldName = "TGL_CREATED";
            this.TGL_SK.Name = "TGL_SK";
            this.TGL_SK.Visible = true;
            this.TGL_SK.VisibleIndex = 2;
            this.TGL_SK.Width = 109;
            // 
            // NO_BUKTI_LAKSANA
            // 
            this.NO_BUKTI_LAKSANA.AppearanceHeader.Options.UseTextOptions = true;
            this.NO_BUKTI_LAKSANA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NO_BUKTI_LAKSANA.Caption = "NO BUKTI";
            this.NO_BUKTI_LAKSANA.FieldName = "NO_BUKTI_LAKSANA";
            this.NO_BUKTI_LAKSANA.Name = "NO_BUKTI_LAKSANA";
            this.NO_BUKTI_LAKSANA.Visible = true;
            this.NO_BUKTI_LAKSANA.VisibleIndex = 3;
            this.NO_BUKTI_LAKSANA.Width = 134;
            // 
            // JNS_BUKTI_LAKSANA
            // 
            this.JNS_BUKTI_LAKSANA.Caption = "JENIS BUKTI";
            this.JNS_BUKTI_LAKSANA.FieldName = "JNS_BUKTI_LAKSANA";
            this.JNS_BUKTI_LAKSANA.Name = "JNS_BUKTI_LAKSANA";
            this.JNS_BUKTI_LAKSANA.Visible = true;
            this.JNS_BUKTI_LAKSANA.VisibleIndex = 4;
            this.JNS_BUKTI_LAKSANA.Width = 121;
            // 
            // TGL_BUKTI_LAKSANA
            // 
            this.TGL_BUKTI_LAKSANA.Caption = "TGL BUKTI";
            this.TGL_BUKTI_LAKSANA.FieldName = "TGL_BUKTI_LAKSANA";
            this.TGL_BUKTI_LAKSANA.Name = "TGL_BUKTI_LAKSANA";
            this.TGL_BUKTI_LAKSANA.Visible = true;
            this.TGL_BUKTI_LAKSANA.VisibleIndex = 5;
            this.TGL_BUKTI_LAKSANA.Width = 120;
            // 
            // KD_SATKER
            // 
            this.KD_SATKER.AppearanceHeader.Options.UseTextOptions = true;
            this.KD_SATKER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KD_SATKER.Caption = "KODE SATKER";
            this.KD_SATKER.FieldName = "KD_SATKER";
            this.KD_SATKER.Name = "KD_SATKER";
            this.KD_SATKER.Visible = true;
            this.KD_SATKER.VisibleIndex = 6;
            this.KD_SATKER.Width = 273;
            // 
            // UR_SATKER
            // 
            this.UR_SATKER.AppearanceHeader.Options.UseTextOptions = true;
            this.UR_SATKER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.UR_SATKER.Caption = "NAMA SATKER";
            this.UR_SATKER.FieldName = "UR_SATKER";
            this.UR_SATKER.Name = "UR_SATKER";
            this.UR_SATKER.Visible = true;
            this.UR_SATKER.VisibleIndex = 7;
            this.UR_SATKER.Width = 324;
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
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(774, 392);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(207, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcGridPSPBMNLAINSk;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(770, 362);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teCari;
            this.layoutControlItem2.CustomizationFormText = "Kata Kunci";
            this.layoutControlItem2.Location = new System.Drawing.Point(432, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(250, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(238, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Kata Kunci";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNamaKolom;
            this.layoutControlItem3.CustomizationFormText = "Nama Kolom";
            this.layoutControlItem3.Location = new System.Drawing.Point(207, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(250, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(225, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Nama Kolom";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbCariOnline;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(670, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // ucRTLPBMNSGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucRTLPBMNSGrid";
            this.Size = new System.Drawing.Size(774, 392);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teNamaKolom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCari.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGridPSPBMNLAINSk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridPSPBMNLAINSk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.GridControl gcGridPSPBMNLAINSk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraEditors.SimpleButton sbCariOnline;
        public DevExpress.XtraEditors.ComboBoxEdit teNamaKolom;
        public DevExpress.XtraEditors.TextEdit teCari;
        public DevExpress.XtraGrid.Views.Grid.GridView gvGridPSPBMNLAINSk;
        private DevExpress.XtraGrid.Columns.GridColumn NO;
        private DevExpress.XtraGrid.Columns.GridColumn NO_SK;
        private DevExpress.XtraGrid.Columns.GridColumn TGL_SK;
        private DevExpress.XtraGrid.Columns.GridColumn NO_BUKTI_LAKSANA;
        private DevExpress.XtraGrid.Columns.GridColumn KD_SATKER;
        private DevExpress.XtraGrid.Columns.GridColumn UR_SATKER;
        private DevExpress.XtraGrid.Columns.GridColumn JNS_BUKTI_LAKSANA;
        private DevExpress.XtraGrid.Columns.GridColumn TGL_BUKTI_LAKSANA;
    }
}
