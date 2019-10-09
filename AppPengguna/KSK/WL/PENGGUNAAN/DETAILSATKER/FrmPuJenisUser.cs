using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPengguna.KSK.WL.PENGGUNAAN.DETAILSATKER
{
    public partial class FrmPuJenisUser : Form
    {
        public KewenanganBarang milik;

        public FrmPuJenisUser()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        private void sbPengguna_Click(object sender, EventArgs e)
        {
            milik(true);
        }

        private void sbPengelola_Click(object sender, EventArgs e)
        {
            milik(false);
        }
    }
}
