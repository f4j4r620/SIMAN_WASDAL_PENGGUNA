using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPengguna.AST.AK
{
    public partial class FormRwyMutasiAngk : Form
    {
        public FormRwyMutasiAngk()
        {
            
            InitializeComponent();
        }

        private void bbRwyMutAngkSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ceIntra_Click(object sender, EventArgs e)
        {
            ceExtra.Checked = false;
        }

        private void ceExtra_Click(object sender, EventArgs e)
        {
            ceIntra.Checked = false;
        }

    }
}
