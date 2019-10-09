namespace AppPengguna.KSK.TL
{
    partial class ucRTLPBKPPGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRTLPBKPPGrid));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbCariOnline = new DevExpress.XtraEditors.SimpleButton();
            this.teNamaKolom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.teCari = new DevExpress.XtraEditors.TextEdit();
            this.gcGridPBKPPSk = new DevExpress.XtraGrid.GridControl();
            this.gvGridPBKPPSk = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NO_SK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TGL_SK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.JENIS_ASET = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TIPE_PEMOHON = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KODE_PEMOHON = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NAMA_PEMOHON = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NAMA_PENANDA_TANGAN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NIP_PENANDA_TANGAN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NILAI_PENETAPAN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KUANTITAS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.URAIAN_KEPUTUSAN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TINDAK_LANJUT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PENERBIT_SK = new DevExpress.XtraGrid.Columns.GridColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcGridPBKPPSk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridPBKPPSk)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.gcGridPBKPPSk);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(688, 392);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
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
            this.teNamaKolom.Location = new System.Drawing.Point(250, 4);
            this.teNamaKolom.Name = "teNamaKolom";
            this.teNamaKolom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teNamaKolom.Size = new System.Drawing.Size(134, 20);
            this.teNamaKolom.StyleController = this.layoutControl1;
            this.teNamaKolom.TabIndex = 6;
            this.teNamaKolom.EditValueChanged += new System.EventHandler(this.teNamaKolom_EditValueChanged);
            // 
            // teCari
            // 
            this.teCari.Location = new System.Drawing.Point(450, 4);
            this.teCari.Name = "teCari";
            this.teCari.Size = new System.Drawing.Size(134, 20);
            this.teCari.StyleController = this.layoutControl1;
            this.teCari.TabIndex = 5;
            this.teCari.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teCari_KeyPress);
            // 
            // gcGridPBKPPSk
            // 
            this.gcGridPBKPPSk.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcGridPBKPPSk.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcGridPBKPPSk.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcGridPBKPPSk.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcGridPBKPPSk.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcGridPBKPPSk.Location = new System.Drawing.Point(4, 30);
            this.gcGridPBKPPSk.MainView = this.gvGridPBKPPSk;
            this.gcGridPBKPPSk.Name = "gcGridPBKPPSk";
            this.gcGridPBKPPSk.Size = new System.Drawing.Size(680, 358);
            this.gcGridPBKPPSk.TabIndex = 4;
            this.gcGridPBKPPSk.UseEmbeddedNavigator = true;
            this.gcGridPBKPPSk.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGridPBKPPSk});
            // 
            // gvGridPBKPPSk
            // 
            this.gvGridPBKPPSk.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NO,
            this.NO_SK,
            this.TGL_SK,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn4,
            this.gridColumn3,
            this.JENIS_ASET,
            this.TIPE_PEMOHON,
            this.KODE_PEMOHON,
            this.NAMA_PEMOHON,
            this.NAMA_PENANDA_TANGAN,
            this.NIP_PENANDA_TANGAN,
            this.NILAI_PENETAPAN,
            this.KUANTITAS,
            this.gridColumn2,
            this.gridColumn1,
            this.URAIAN_KEPUTUSAN,
            this.TINDAK_LANJUT,
            this.PENERBIT_SK});
            this.gvGridPBKPPSk.GridControl = this.gcGridPBKPPSk;
            this.gvGridPBKPPSk.Name = "gvGridPBKPPSk";
            this.gvGridPBKPPSk.OptionsBehavior.Editable = false;
            this.gvGridPBKPPSk.OptionsBehavior.ReadOnly = true;
            this.gvGridPBKPPSk.OptionsView.ColumnAutoWidth = false;
            this.gvGridPBKPPSk.OptionsView.ShowAutoFilterRow = true;
            this.gvGridPBKPPSk.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvGridSk_FocusedRowChanged);
            this.gvGridPBKPPSk.ColumnFilterChanged += new System.EventHandler(this.gvGridSk_ColumnFilterChanged);
            this.gvGridPBKPPSk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvGridSk_KeyPress);
            this.gvGridPBKPPSk.DoubleClick += new System.EventHandler(this.gvGridSk_DoubleClick);
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
            // gridColumn6
            // 
            this.gridColumn6.Caption = "KODE SATKER";
            this.gridColumn6.FieldName = "KD_SATKER";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "NAMA SATKER";
            this.gridColumn5.FieldName = "UR_SATKER";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "ALASAN";
            this.gridColumn4.FieldName = "ALASAN";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "NAMA PENGADILAN";
            this.gridColumn3.FieldName = "NAMA_PENGADILAN";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            // 
            // JENIS_ASET
            // 
            this.JENIS_ASET.AppearanceHeader.Options.UseTextOptions = true;
            this.JENIS_ASET.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.JENIS_ASET.Caption = "JENIS ASET";
            this.JENIS_ASET.FieldName = "IS_TB";
            this.JENIS_ASET.Name = "JENIS_ASET";
            this.JENIS_ASET.Visible = true;
            this.JENIS_ASET.VisibleIndex = 7;
            this.JENIS_ASET.Width = 119;
            // 
            // TIPE_PEMOHON
            // 
            this.TIPE_PEMOHON.AppearanceHeader.Options.UseTextOptions = true;
            this.TIPE_PEMOHON.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TIPE_PEMOHON.Caption = "TIPE PEMOHON";
            this.TIPE_PEMOHON.FieldName = "TIPE_PEMOHON";
            this.TIPE_PEMOHON.Name = "TIPE_PEMOHON";
            this.TIPE_PEMOHON.Visible = true;
            this.TIPE_PEMOHON.VisibleIndex = 8;
            this.TIPE_PEMOHON.Width = 119;
            // 
            // KODE_PEMOHON
            // 
            this.KODE_PEMOHON.AppearanceHeader.Options.UseTextOptions = true;
            this.KODE_PEMOHON.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KODE_PEMOHON.Caption = "KODE PEMOHON";
            this.KODE_PEMOHON.FieldName = "KD_PEMOHON";
            this.KODE_PEMOHON.Name = "KODE_PEMOHON";
            this.KODE_PEMOHON.Visible = true;
            this.KODE_PEMOHON.VisibleIndex = 9;
            this.KODE_PEMOHON.Width = 111;
            // 
            // NAMA_PEMOHON
            // 
            this.NAMA_PEMOHON.AppearanceHeader.Options.UseTextOptions = true;
            this.NAMA_PEMOHON.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NAMA_PEMOHON.Caption = "NAMA PEMOHON";
            this.NAMA_PEMOHON.FieldName = "NM_PEMOHON";
            this.NAMA_PEMOHON.Name = "NAMA_PEMOHON";
            this.NAMA_PEMOHON.Visible = true;
            this.NAMA_PEMOHON.VisibleIndex = 10;
            this.NAMA_PEMOHON.Width = 209;
            // 
            // NAMA_PENANDA_TANGAN
            // 
            this.NAMA_PENANDA_TANGAN.AppearanceHeader.Options.UseTextOptions = true;
            this.NAMA_PENANDA_TANGAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NAMA_PENANDA_TANGAN.Caption = "NAMA PENANDA TANGAN";
            this.NAMA_PENANDA_TANGAN.FieldName = "NM_PENANDATANGAN";
            this.NAMA_PENANDA_TANGAN.Name = "NAMA_PENANDA_TANGAN";
            this.NAMA_PENANDA_TANGAN.Visible = true;
            this.NAMA_PENANDA_TANGAN.VisibleIndex = 11;
            this.NAMA_PENANDA_TANGAN.Width = 215;
            // 
            // NIP_PENANDA_TANGAN
            // 
            this.NIP_PENANDA_TANGAN.AppearanceHeader.Options.UseTextOptions = true;
            this.NIP_PENANDA_TANGAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NIP_PENANDA_TANGAN.Caption = "NIP PENANDA TANGAN";
            this.NIP_PENANDA_TANGAN.FieldName = "NIP_PENANDATANGAN";
            this.NIP_PENANDA_TANGAN.Name = "NIP_PENANDA_TANGAN";
            this.NIP_PENANDA_TANGAN.Visible = true;
            this.NIP_PENANDA_TANGAN.VisibleIndex = 12;
            this.NIP_PENANDA_TANGAN.Width = 162;
            // 
            // NILAI_PENETAPAN
            // 
            this.NILAI_PENETAPAN.AppearanceHeader.Options.UseTextOptions = true;
            this.NILAI_PENETAPAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NILAI_PENETAPAN.Caption = "NILAI PENETAPAN";
            this.NILAI_PENETAPAN.DisplayFormat.FormatString = "n0";
            this.NILAI_PENETAPAN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.NILAI_PENETAPAN.FieldName = "NILAI_PENETAPAN";
            this.NILAI_PENETAPAN.Name = "NILAI_PENETAPAN";
            this.NILAI_PENETAPAN.Visible = true;
            this.NILAI_PENETAPAN.VisibleIndex = 13;
            this.NILAI_PENETAPAN.Width = 161;
            // 
            // KUANTITAS
            // 
            this.KUANTITAS.AppearanceHeader.Options.UseTextOptions = true;
            this.KUANTITAS.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KUANTITAS.Caption = "KUANTITAS";
            this.KUANTITAS.DisplayFormat.FormatString = "n0";
            this.KUANTITAS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.KUANTITAS.FieldName = "KUANTITAS_SK";
            this.KUANTITAS.Name = "KUANTITAS";
            this.KUANTITAS.Visible = true;
            this.KUANTITAS.VisibleIndex = 14;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "KODE PELAYANAN";
            this.gridColumn2.FieldName = "KD_PELAYANAN";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 15;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "NAMA PELAYANAN";
            this.gridColumn1.FieldName = "NM_PELAYANAN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 16;
            // 
            // URAIAN_KEPUTUSAN
            // 
            this.URAIAN_KEPUTUSAN.AppearanceHeader.Options.UseTextOptions = true;
            this.URAIAN_KEPUTUSAN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.URAIAN_KEPUTUSAN.Caption = "URAIAN KEPUTUSAN";
            this.URAIAN_KEPUTUSAN.FieldName = "URAIAN_KEPUTUSAN";
            this.URAIAN_KEPUTUSAN.Name = "URAIAN_KEPUTUSAN";
            this.URAIAN_KEPUTUSAN.Visible = true;
            this.URAIAN_KEPUTUSAN.VisibleIndex = 17;
            this.URAIAN_KEPUTUSAN.Width = 307;
            // 
            // TINDAK_LANJUT
            // 
            this.TINDAK_LANJUT.AppearanceHeader.Options.UseTextOptions = true;
            this.TINDAK_LANJUT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TINDAK_LANJUT.Caption = "TINDAK LANJUT";
            this.TINDAK_LANJUT.FieldName = "STATUS_BMN";
            this.TINDAK_LANJUT.Name = "TINDAK_LANJUT";
            this.TINDAK_LANJUT.Visible = true;
            this.TINDAK_LANJUT.VisibleIndex = 18;
            this.TINDAK_LANJUT.Width = 217;
            // 
            // PENERBIT_SK
            // 
            this.PENERBIT_SK.AppearanceHeader.Options.UseTextOptions = true;
            this.PENERBIT_SK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PENERBIT_SK.Caption = "PENERBIT SK";
            this.PENERBIT_SK.FieldName = "NM_PENERBIT_SK";
            this.PENERBIT_SK.Name = "PENERBIT_SK";
            this.PENERBIT_SK.Visible = true;
            this.PENERBIT_SK.VisibleIndex = 19;
            this.PENERBIT_SK.Width = 203;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(688, 392);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(184, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcGridPBKPPSk;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(684, 362);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
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
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // ucRTLPBKPPGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucRTLPBKPPGrid";
            this.Size = new System.Drawing.Size(688, 392);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teNamaKolom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCari.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGridPBKPPSk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridPBKPPSk)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcGridPBKPPSk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraEditors.SimpleButton sbCariOnline;
        public DevExpress.XtraEditors.ComboBoxEdit teNamaKolom;
        public DevExpress.XtraEditors.TextEdit teCari;
        public DevExpress.XtraGrid.Views.Grid.GridView gvGridPBKPPSk;
        private DevExpress.XtraGrid.Columns.GridColumn NO;
        private DevExpress.XtraGrid.Columns.GridColumn NO_SK;
        private DevExpress.XtraGrid.Columns.GridColumn TGL_SK;
        private DevExpress.XtraGrid.Columns.GridColumn JENIS_ASET;
        private DevExpress.XtraGrid.Columns.GridColumn TIPE_PEMOHON;
        private DevExpress.XtraGrid.Columns.GridColumn KODE_PEMOHON;
        private DevExpress.XtraGrid.Columns.GridColumn NAMA_PEMOHON;
        private DevExpress.XtraGrid.Columns.GridColumn NAMA_PENANDA_TANGAN;
        private DevExpress.XtraGrid.Columns.GridColumn NIP_PENANDA_TANGAN;
        private DevExpress.XtraGrid.Columns.GridColumn NILAI_PENETAPAN;
        private DevExpress.XtraGrid.Columns.GridColumn KUANTITAS;
        private DevExpress.XtraGrid.Columns.GridColumn URAIAN_KEPUTUSAN;
        private DevExpress.XtraGrid.Columns.GridColumn TINDAK_LANJUT;
        private DevExpress.XtraGrid.Columns.GridColumn PENERBIT_SK;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}
