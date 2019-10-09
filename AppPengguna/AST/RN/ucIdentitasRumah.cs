using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.AST.RN
{
    public partial class ucIdentitasRumah : DevExpress.XtraEditors.XtraUserControl
    {
        public ucIdentitasRumah()
        {
            InitializeComponent();
        }

        private void splitContainerControl1_Resize(object sender, EventArgs e)
        {
            if (this.Width > splitContainerControl1.MinimumSize.Width)
            {
                splitContainerControl1.Width = this.Width - 10;
            }
            if (this.Height > splitContainerControl1.MinimumSize.Height)
            {
                splitContainerControl1.Height = this.Height - 10;
            }
        }

        private void layoutControlGroup1_Click(object sender, EventArgs e)
        {
            this.Parent.Parent.Focus();
        }

        private void teNilaiPerolehan_Properties_ValueChanged(object sender, EventArgs e)
        {
            this.teNilaiSebelumPenyusutan.Value = this.teNilaiPerolehan.Value + this.teNilaiMutasi.Value;
        }

        private void teNilaiMutasi_Properties_ValueChanged(object sender, EventArgs e)
        {
            this.teNilaiSebelumPenyusutan.Value = this.teNilaiPerolehan.Value + this.teNilaiMutasi.Value;
        }

        private void teNilaiSebelumPenyusutan_Properties_ValueChanged(object sender, EventArgs e)
        {
            this.teNilaiBuku.Value = this.teNilaiSebelumPenyusutan.Value - this.teNilaiPenyusutan.Value;
        }

        private void teNilaiPenyusutan_Properties_ValueChanged(object sender, EventArgs e)
        {
            this.teNilaiBuku.Value = this.teNilaiSebelumPenyusutan.Value - this.teNilaiPenyusutan.Value;
        }
    }
}
