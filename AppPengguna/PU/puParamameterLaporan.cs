using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPengguna.PU
{
    public partial class puParameterLaporan : Form
    {
        public string _closeMode; 
        public decimal _thnAng;
        public decimal thnAng
        {
            get { return _thnAng; }
            set { _thnAng = value; }
        }
        public string closeMode
        {
            get { return _closeMode; }
            set { _closeMode = value; }
        }

        public puParameterLaporan()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        private void sbCetak_Click(object sender, EventArgs e)
        {
            //tahunAng(thnAng);
            if (deTahunAng.Text == "")
            {
                MessageBox.Show("Tahun Anggaran Masih Kosong !");
            }
            else
            {
                thnAng = Convert.ToDecimal(deTahunAng.Text);
                if (thnAng != null)
                {
                    this.closeMode = "Y";
                    this.Close();
                }
            }
        }

        private void puParamameterLaporan_Load(object sender, EventArgs e)
        {
            this.thnAng = Convert.ToDecimal(konfigApp.tahunAnggaran);
        }

        private void deTahunAng_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.closeMode = "N";
            this.Close();
        }
    }
}
