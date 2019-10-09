using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.AST.TN
{
    public partial class ucIdentitasTanah : DevExpress.XtraEditors.XtraUserControl
    {
        public ucIdentitasTanah()
        {
            InitializeComponent();
        }

        private void splitContainerControl1_Resize(object sender, EventArgs e)
        {
            this.splitContainerControl1.SplitterPosition =(int) ((this.Width/2) +20);
        }
    }
}
