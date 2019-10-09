namespace AppPengguna.AST.RN
{
    partial class ShowPicture
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
            this.isPict = new DevExpress.XtraEditors.Controls.ImageSlider();
            ((System.ComponentModel.ISupportInitialize)(this.isPict)).BeginInit();
            this.SuspendLayout();
            // 
            // isPict
            // 
            this.isPict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isPict.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.Stretch;
            this.isPict.Location = new System.Drawing.Point(0, 0);
            this.isPict.Name = "isPict";
            this.isPict.Size = new System.Drawing.Size(645, 498);
            this.isPict.TabIndex = 0;
            this.isPict.Text = "imageSlider1";
            this.isPict.CurrentImageIndexChanged += new DevExpress.XtraEditors.Controls.ImageSliderCurrentImageIndexChangedEventHandler(this.isPict_CurrentImageIndexChanged);
            // 
            // ShowPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 498);
            this.Controls.Add(this.isPict);
            this.Name = "ShowPicture";
            this.Text = "ShowPicture";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShowPicture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.isPict)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Controls.ImageSlider isPict;

    }
}