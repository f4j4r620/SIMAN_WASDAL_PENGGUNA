namespace AppPengguna.AST.AK
{
    partial class UcNmFasAngk
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.GcUcDtl = new DevExpress.XtraGrid.GridControl();
            this.gvUcDtl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NM_FASILITAS = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcUcDtl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUcDtl)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.GcUcDtl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(412, 235);
            this.panelControl1.TabIndex = 0;
            // 
            // GcUcDtl
            // 
            this.GcUcDtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GcUcDtl.Location = new System.Drawing.Point(2, 2);
            this.GcUcDtl.MainView = this.gvUcDtl;
            this.GcUcDtl.Name = "GcUcDtl";
            this.GcUcDtl.Size = new System.Drawing.Size(408, 231);
            this.GcUcDtl.TabIndex = 0;
            this.GcUcDtl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUcDtl});
            // 
            // gvUcDtl
            // 
            this.gvUcDtl.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NUM,
            this.NM_FASILITAS});
            this.gvUcDtl.GridControl = this.GcUcDtl;
            this.gvUcDtl.Name = "gvUcDtl";
            // 
            // NUM
            // 
            this.NUM.Caption = "NUM";
            this.NUM.FieldName = "NUM";
            this.NUM.Name = "NUM";
            this.NUM.Visible = true;
            this.NUM.VisibleIndex = 0;
            // 
            // NM_FASILITAS
            // 
            this.NM_FASILITAS.Caption = "NM_FASILITAS";
            this.NM_FASILITAS.FieldName = "NM_FASILITAS";
            this.NM_FASILITAS.Name = "NM_FASILITAS";
            this.NM_FASILITAS.Visible = true;
            this.NM_FASILITAS.VisibleIndex = 1;
            // 
            // UcNmFasAngk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UcNmFasAngk";
            this.Size = new System.Drawing.Size(412, 235);
            this.Load += new System.EventHandler(this.UcNmFasAngk_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcUcDtl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUcDtl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl GcUcDtl;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUcDtl;
        private DevExpress.XtraGrid.Columns.GridColumn NUM;
        private DevExpress.XtraGrid.Columns.GridColumn NM_FASILITAS;
    }
}
