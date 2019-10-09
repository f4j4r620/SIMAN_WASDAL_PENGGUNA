using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.AST.BS
{
    public partial class ucIdentitasSejarah : DevExpress.XtraEditors.XtraUserControl
    {
        public ucIdentitasSejarah()
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

   
    }
}
