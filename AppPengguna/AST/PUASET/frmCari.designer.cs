namespace AppPengguna.AST.PUASET
{
    partial class frmCari
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
          DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
          this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
          this.teSearch1 = new DevExpress.XtraEditors.TextEdit();
          this.btnBatal = new DevExpress.XtraEditors.SimpleButton();
          this.btnCari = new DevExpress.XtraEditors.SimpleButton();
          this.LuKolom = new DevExpress.XtraEditors.ComboBoxEdit();
          this.teSearch2 = new DevExpress.XtraEditors.DateEdit();
          this.teNumber = new DevExpress.XtraEditors.SpinEdit();
          this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
          this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
          this.layoutDate = new DevExpress.XtraLayout.LayoutControlItem();
          this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
          this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
          this.layoutText = new DevExpress.XtraLayout.LayoutControlItem();
          this.LayoutNumber = new DevExpress.XtraLayout.LayoutControlItem();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
          this.layoutControl1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.teSearch1.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.LuKolom.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.teSearch2.Properties.CalendarTimeProperties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.teSearch2.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.teNumber.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutDate)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutText)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.LayoutNumber)).BeginInit();
          this.SuspendLayout();
          // 
          // layoutControl1
          // 
          this.layoutControl1.Controls.Add(this.teSearch1);
          this.layoutControl1.Controls.Add(this.btnBatal);
          this.layoutControl1.Controls.Add(this.btnCari);
          this.layoutControl1.Controls.Add(this.LuKolom);
          this.layoutControl1.Controls.Add(this.teSearch2);
          this.layoutControl1.Controls.Add(this.teNumber);
          this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.layoutControl1.Location = new System.Drawing.Point(0, 0);
          this.layoutControl1.Name = "layoutControl1";
          this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
          this.layoutControl1.Root = this.layoutControlGroup1;
          this.layoutControl1.Size = new System.Drawing.Size(428, 140);
          this.layoutControl1.TabIndex = 0;
          this.layoutControl1.Text = "layoutControl1";
          // 
          // teSearch1
          // 
          this.teSearch1.Location = new System.Drawing.Point(74, 36);
          this.teSearch1.Name = "teSearch1";
          this.teSearch1.Size = new System.Drawing.Size(326, 20);
          this.teSearch1.StyleController = this.layoutControl1;
          this.teSearch1.TabIndex = 8;
          // 
          // btnBatal
          // 
          this.btnBatal.Location = new System.Drawing.Point(207, 108);
          this.btnBatal.Name = "btnBatal";
          this.btnBatal.Size = new System.Drawing.Size(193, 22);
          this.btnBatal.StyleController = this.layoutControl1;
          this.btnBatal.TabIndex = 7;
          this.btnBatal.Text = "Batal";
          this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
          // 
          // btnCari
          // 
          this.btnCari.Location = new System.Drawing.Point(12, 108);
          this.btnCari.Name = "btnCari";
          this.btnCari.Size = new System.Drawing.Size(191, 22);
          this.btnCari.StyleController = this.layoutControl1;
          this.btnCari.TabIndex = 6;
          this.btnCari.Text = "Cari";
          this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
          // 
          // LuKolom
          // 
          this.LuKolom.Location = new System.Drawing.Point(74, 12);
          this.LuKolom.Name = "LuKolom";
          this.LuKolom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.LuKolom.Properties.PopupSizeable = true;
          this.LuKolom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
          this.LuKolom.Size = new System.Drawing.Size(326, 20);
          this.LuKolom.StyleController = this.layoutControl1;
          this.LuKolom.TabIndex = 4;
          //this.LuKolom.SelectedIndexChanged += new System.EventHandler(this.LuKolom_SelectedIndexChanged);
          this.LuKolom.EditValueChanged += new System.EventHandler(this.LuKolom_EditValueChanged);
          // 
          // teSearch2
          // 
          this.teSearch2.EditValue = null;
          this.teSearch2.Location = new System.Drawing.Point(74, 60);
          this.teSearch2.Name = "teSearch2";
          this.teSearch2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.teSearch2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.teSearch2.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
          this.teSearch2.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
          this.teSearch2.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
          this.teSearch2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
          this.teSearch2.Properties.EditFormat.FormatString = "dd/MM/yyyy";
          this.teSearch2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
          this.teSearch2.Properties.Mask.EditMask = "dd/MM/yyyy";
          this.teSearch2.Size = new System.Drawing.Size(326, 20);
          this.teSearch2.StyleController = this.layoutControl1;
          this.teSearch2.TabIndex = 5;
          // 
          // teNumber
          // 
          this.teNumber.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
          this.teNumber.Location = new System.Drawing.Point(74, 84);
          this.teNumber.Name = "teNumber";
          this.teNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
          this.teNumber.Properties.DisplayFormat.FormatString = "n0";
          this.teNumber.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
          this.teNumber.Properties.EditFormat.FormatString = "n0";
          this.teNumber.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
          this.teNumber.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
          this.teNumber.Properties.Mask.EditMask = "n0";
          this.teNumber.Size = new System.Drawing.Size(326, 20);
          this.teNumber.StyleController = this.layoutControl1;
          this.teNumber.TabIndex = 9;
          // 
          // layoutControlGroup1
          // 
          this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
          this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
          this.layoutControlGroup1.GroupBordersVisible = false;
          this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutDate,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutText,
            this.LayoutNumber});
          this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
          this.layoutControlGroup1.Name = "layoutControlGroup1";
          this.layoutControlGroup1.Size = new System.Drawing.Size(412, 142);
          this.layoutControlGroup1.Text = "layoutControlGroup1";
          this.layoutControlGroup1.TextVisible = false;
          // 
          // layoutControlItem1
          // 
          this.layoutControlItem1.Control = this.LuKolom;
          this.layoutControlItem1.CustomizationFormText = "Nama Kolom";
          this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
          this.layoutControlItem1.Name = "layoutControlItem1";
          this.layoutControlItem1.Size = new System.Drawing.Size(392, 24);
          this.layoutControlItem1.Text = "Nama Kolom";
          this.layoutControlItem1.TextSize = new System.Drawing.Size(58, 13);
          // 
          // layoutDate
          // 
          this.layoutDate.Control = this.teSearch2;
          this.layoutDate.CustomizationFormText = "Kata Kunci";
          this.layoutDate.Location = new System.Drawing.Point(0, 48);
          this.layoutDate.Name = "layoutDate";
          this.layoutDate.Size = new System.Drawing.Size(392, 24);
          this.layoutDate.Text = "Kata Kunci";
          this.layoutDate.TextSize = new System.Drawing.Size(58, 13);
          this.layoutDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
          // 
          // layoutControlItem3
          // 
          this.layoutControlItem3.Control = this.btnCari;
          this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
          this.layoutControlItem3.Location = new System.Drawing.Point(0, 96);
          this.layoutControlItem3.Name = "layoutControlItem3";
          this.layoutControlItem3.Size = new System.Drawing.Size(195, 26);
          this.layoutControlItem3.Text = "layoutControlItem3";
          this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
          this.layoutControlItem3.TextToControlDistance = 0;
          this.layoutControlItem3.TextVisible = false;
          // 
          // layoutControlItem4
          // 
          this.layoutControlItem4.Control = this.btnBatal;
          this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
          this.layoutControlItem4.Location = new System.Drawing.Point(195, 96);
          this.layoutControlItem4.Name = "layoutControlItem4";
          this.layoutControlItem4.Size = new System.Drawing.Size(197, 26);
          this.layoutControlItem4.Text = "layoutControlItem4";
          this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
          this.layoutControlItem4.TextToControlDistance = 0;
          this.layoutControlItem4.TextVisible = false;
          // 
          // layoutText
          // 
          this.layoutText.Control = this.teSearch1;
          this.layoutText.CustomizationFormText = "Kata Kunci";
          this.layoutText.Location = new System.Drawing.Point(0, 24);
          this.layoutText.Name = "layoutText";
          this.layoutText.Size = new System.Drawing.Size(392, 24);
          this.layoutText.Text = "Kata Kunci";
          this.layoutText.TextSize = new System.Drawing.Size(58, 13);
          // 
          // LayoutNumber
          // 
          this.LayoutNumber.Control = this.teNumber;
          this.LayoutNumber.CustomizationFormText = "Kata Kunci";
          this.LayoutNumber.Location = new System.Drawing.Point(0, 72);
          this.LayoutNumber.Name = "LayoutNumber";
          this.LayoutNumber.Size = new System.Drawing.Size(392, 24);
          this.LayoutNumber.Text = "Kata Kunci";
          this.LayoutNumber.TextSize = new System.Drawing.Size(58, 13);
          this.LayoutNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
          // 
          // frmCari
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(428, 140);
          this.Controls.Add(this.layoutControl1);
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "frmCari";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Pencarian";
          ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
          this.layoutControl1.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.teSearch1.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.LuKolom.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.teSearch2.Properties.CalendarTimeProperties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.teSearch2.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.teNumber.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutDate)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.layoutText)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.LayoutNumber)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControl layoutControl1;
        public DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        public DevExpress.XtraEditors.SimpleButton btnBatal;
        public DevExpress.XtraEditors.SimpleButton btnCari;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraLayout.LayoutControlItem layoutDate;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraEditors.ComboBoxEdit LuKolom;
        private DevExpress.XtraEditors.DateEdit teSearch2;
        private DevExpress.XtraEditors.TextEdit teSearch1;
        private DevExpress.XtraLayout.LayoutControlItem layoutText;
        private DevExpress.XtraEditors.SpinEdit teNumber;
        public DevExpress.XtraLayout.LayoutControlItem LayoutNumber;

    }
}