using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.KES1.RSK.PU
{
    public partial class frmPuNilaiPersetujuan : DevExpress.XtraEditors.XtraForm
    {
        public frmPuNilaiPersetujuan()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_2016;
        }

        private void setUpForm()
        {
            this.teKdBrg.Properties.ReadOnly = true;
            this.teNmBrg.Properties.ReadOnly = true;
            this.teNup.Properties.ReadOnly = true;
        }

        private void frmPuNilaiPersetujuan_Load(object sender, EventArgs e)
        {
            this.setUpForm();
        }


    }
}