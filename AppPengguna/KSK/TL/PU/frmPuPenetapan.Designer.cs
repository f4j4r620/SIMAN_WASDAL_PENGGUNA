namespace AppPengguna.KSK.TL.PU
{
    partial class frmPuPenetapan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPuPenetapan));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbSimpan = new DevExpress.XtraEditors.SimpleButton();
            this.sbBatal = new DevExpress.XtraEditors.SimpleButton();
            this.teStatus = new DevExpress.XtraEditors.TextEdit();
            this.tePenggunaanBmn = new DevExpress.XtraEditors.TextEdit();
            this.teKuantitas = new DevExpress.XtraEditors.TextEdit();
            this.teNilaiPersetujuan = new DevExpress.XtraEditors.TextEdit();
            this.teNamaBarang = new DevExpress.XtraEditors.TextEdit();
            this.teNup = new DevExpress.XtraEditors.TextEdit();
            this.teKodeBarang = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciStatusBmn = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePenggunaanBmn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKuantitas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNilaiPersetujuan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNamaBarang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKodeBarang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStatusBmn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbSimpan);
            this.layoutControl1.Controls.Add(this.sbBatal);
            this.layoutControl1.Controls.Add(this.teStatus);
            this.layoutControl1.Controls.Add(this.tePenggunaanBmn);
            this.layoutControl1.Controls.Add(this.teKuantitas);
            this.layoutControl1.Controls.Add(this.teNilaiPersetujuan);
            this.layoutControl1.Controls.Add(this.teNamaBarang);
            this.layoutControl1.Controls.Add(this.teNup);
            this.layoutControl1.Controls.Add(this.teKodeBarang);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(365, 193);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbSimpan
            // 
            this.sbSimpan.Image = ((System.Drawing.Image)(resources.GetObject("sbSimpan.Image")));
            this.sbSimpan.Location = new System.Drawing.Point(162, 164);
            this.sbSimpan.Name = "sbSimpan";
            this.sbSimpan.Size = new System.Drawing.Size(96, 22);
            this.sbSimpan.StyleController = this.layoutControl1;
            this.sbSimpan.TabIndex = 12;
            this.sbSimpan.Text = "Simpan";
            this.sbSimpan.Click += new System.EventHandler(this.sbSimpan_Click);
            // 
            // sbBatal
            // 
            this.sbBatal.Image = ((System.Drawing.Image)(resources.GetObject("sbBatal.Image")));
            this.sbBatal.Location = new System.Drawing.Point(262, 164);
            this.sbBatal.Name = "sbBatal";
            this.sbBatal.Size = new System.Drawing.Size(96, 22);
            this.sbBatal.StyleController = this.layoutControl1;
            this.sbBatal.TabIndex = 11;
            this.sbBatal.Text = "Batal";
            this.sbBatal.Click += new System.EventHandler(this.sbBatal_Click);
            // 
            // teStatus
            // 
            this.teStatus.Location = new System.Drawing.Point(100, 94);
            this.teStatus.Name = "teStatus";
            this.teStatus.Properties.ReadOnly = true;
            this.teStatus.Size = new System.Drawing.Size(259, 20);
            this.teStatus.StyleController = this.layoutControl1;
            this.teStatus.TabIndex = 10;
            // 
            // tePenggunaanBmn
            // 
            this.tePenggunaanBmn.Location = new System.Drawing.Point(100, 72);
            this.tePenggunaanBmn.Name = "tePenggunaanBmn";
            this.tePenggunaanBmn.Properties.ReadOnly = true;
            this.tePenggunaanBmn.Size = new System.Drawing.Size(259, 20);
            this.tePenggunaanBmn.StyleController = this.layoutControl1;
            this.tePenggunaanBmn.TabIndex = 9;
            // 
            // teKuantitas
            // 
            this.teKuantitas.Location = new System.Drawing.Point(301, 50);
            this.teKuantitas.Name = "teKuantitas";
            this.teKuantitas.Properties.Mask.EditMask = "n0";
            this.teKuantitas.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.teKuantitas.Size = new System.Drawing.Size(58, 20);
            this.teKuantitas.StyleController = this.layoutControl1;
            this.teKuantitas.TabIndex = 8;
            // 
            // teNilaiPersetujuan
            // 
            this.teNilaiPersetujuan.Location = new System.Drawing.Point(100, 50);
            this.teNilaiPersetujuan.Name = "teNilaiPersetujuan";
            this.teNilaiPersetujuan.Properties.Mask.EditMask = "n0";
            this.teNilaiPersetujuan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.teNilaiPersetujuan.Size = new System.Drawing.Size(149, 20);
            this.teNilaiPersetujuan.StyleController = this.layoutControl1;
            this.teNilaiPersetujuan.TabIndex = 7;
            // 
            // teNamaBarang
            // 
            this.teNamaBarang.Location = new System.Drawing.Point(100, 28);
            this.teNamaBarang.Name = "teNamaBarang";
            this.teNamaBarang.Properties.ReadOnly = true;
            this.teNamaBarang.Size = new System.Drawing.Size(259, 20);
            this.teNamaBarang.StyleController = this.layoutControl1;
            this.teNamaBarang.TabIndex = 6;
            // 
            // teNup
            // 
            this.teNup.Location = new System.Drawing.Point(266, 6);
            this.teNup.Name = "teNup";
            this.teNup.Properties.ReadOnly = true;
            this.teNup.Size = new System.Drawing.Size(93, 20);
            this.teNup.StyleController = this.layoutControl1;
            this.teNup.TabIndex = 5;
            // 
            // teKodeBarang
            // 
            this.teKodeBarang.Location = new System.Drawing.Point(100, 6);
            this.teKodeBarang.Name = "teKodeBarang";
            this.teKodeBarang.Properties.ReadOnly = true;
            this.teKodeBarang.Size = new System.Drawing.Size(155, 20);
            this.teKodeBarang.StyleController = this.layoutControl1;
            this.teKodeBarang.TabIndex = 4;
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
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.lciStatusBmn,
            this.emptySpaceItem2,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(365, 193);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 110);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(355, 47);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teKodeBarang;
            this.layoutControlItem1.CustomizationFormText = "Kode Barang - NUP";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem1.Size = new System.Drawing.Size(251, 22);
            this.layoutControlItem1.Text = "Kode Barang - NUP";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(91, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teNup;
            this.layoutControlItem2.CustomizationFormText = "-";
            this.layoutControlItem2.Location = new System.Drawing.Point(251, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem2.Size = new System.Drawing.Size(104, 22);
            this.layoutControlItem2.Text = "-";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(4, 13);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNamaBarang;
            this.layoutControlItem3.CustomizationFormText = "Nama Barang";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 22);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem3.Size = new System.Drawing.Size(355, 22);
            this.layoutControlItem3.Text = "Nama Barang";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(91, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teNilaiPersetujuan;
            this.layoutControlItem4.CustomizationFormText = "Nilai Persetujuan";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem4.Size = new System.Drawing.Size(245, 22);
            this.layoutControlItem4.Text = "Nilai Persetujuan";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(91, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teKuantitas;
            this.layoutControlItem5.CustomizationFormText = "Kuantitas";
            this.layoutControlItem5.Location = new System.Drawing.Point(245, 44);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem5.Size = new System.Drawing.Size(110, 22);
            this.layoutControlItem5.Text = "Kuantitas";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(45, 13);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.tePenggunaanBmn;
            this.layoutControlItem6.CustomizationFormText = "Penggunaan BMN";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 66);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem6.Size = new System.Drawing.Size(355, 22);
            this.layoutControlItem6.Text = "Penggunaan BMN";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(91, 13);
            // 
            // lciStatusBmn
            // 
            this.lciStatusBmn.Control = this.teStatus;
            this.lciStatusBmn.CustomizationFormText = "Status BMN";
            this.lciStatusBmn.Location = new System.Drawing.Point(0, 88);
            this.lciStatusBmn.Name = "lciStatusBmn";
            this.lciStatusBmn.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lciStatusBmn.Size = new System.Drawing.Size(355, 22);
            this.lciStatusBmn.Text = "Status BMN";
            this.lciStatusBmn.TextSize = new System.Drawing.Size(91, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 157);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(155, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.sbBatal;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(255, 157);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.sbSimpan;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(155, 157);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // frmPuPenetapan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 193);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPuPenetapan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Penetapan";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePenggunaanBmn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKuantitas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNilaiPersetujuan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNamaBarang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKodeBarang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStatusBmn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton sbSimpan;
        private DevExpress.XtraEditors.SimpleButton sbBatal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        public DevExpress.XtraEditors.TextEdit teNup;
        public DevExpress.XtraEditors.TextEdit teKodeBarang;
        public DevExpress.XtraEditors.TextEdit teNamaBarang;
        public DevExpress.XtraEditors.TextEdit teKuantitas;
        public DevExpress.XtraEditors.TextEdit teNilaiPersetujuan;
        public DevExpress.XtraEditors.TextEdit tePenggunaanBmn;
        public DevExpress.XtraEditors.TextEdit teStatus;
        public DevExpress.XtraLayout.LayoutControlItem lciStatusBmn;
    }
}