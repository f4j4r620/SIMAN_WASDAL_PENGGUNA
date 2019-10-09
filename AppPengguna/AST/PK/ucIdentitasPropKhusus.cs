using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.AST.PK
{
    public partial class ucIdentitasPropKhusus : DevExpress.XtraEditors.XtraUserControl
    {
        public ucIdentitasPropKhusus()
        {
            InitializeComponent();
        }

        private void splitContainerControl1_Resize(object sender, EventArgs e)
        {
            this.splitContainerControl1.SplitterPosition = (int)((this.Width / 2) + 20);
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
