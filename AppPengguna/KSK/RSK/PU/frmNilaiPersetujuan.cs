using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPengguna.KSK.RSK.PU
{
    public delegate void simpanNilaiPersetujuan(decimal? nilai);

    public partial class frmNilaiPersetujuan : Form
    {
        public simpanNilaiPersetujuan simpanNP;
        public frmNilaiPersetujuan()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        public decimal? np
        {
            get { return Convert.ToDecimal(teNP.Text); }
            set { teNP.Text = value.ToString(); }
        }

        private void sbSimpan_Click(object sender, EventArgs e)
        {
            simpanNP(np);
            this.Close();
        }
    }
}
