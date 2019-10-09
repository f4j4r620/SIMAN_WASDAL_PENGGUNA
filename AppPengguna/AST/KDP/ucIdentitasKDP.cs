using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.AST.KDP
{
    public partial class ucIdentitasKDP : DevExpress.XtraEditors.XtraUserControl
    {
        public ucIdentitasKDP()
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

        }
        private void teNilaiMutasi_Properties_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void teNilaiSebelumPenyusutan_Properties_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void teNilaiPenyusutan_Properties_ValueChanged(object sender, EventArgs e)
        {
            
        }

       

       
    }
}
