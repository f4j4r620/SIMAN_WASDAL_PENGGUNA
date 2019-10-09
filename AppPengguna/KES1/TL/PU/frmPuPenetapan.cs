using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPengguna.KES1.TL.PU
{
    public partial class frmPuPenetapan : Form
    {
        public SimpanDaftarAsetTerpilih simpanDataPenetapan;
        public string kodeStatus = null;
        public decimal? idAset = null;

        public frmPuPenetapan()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        private void sbSimpan_Click(object sender, EventArgs e)
        {
            Enabled = false;
            simpanDataPenetapan(idAset.ToString(), 'U');
            Enabled = true;
        }

        private void sbBatal_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
