using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KSK.SIMANSPAN.LAPSIMANSPAN
{
    public partial class ucLapSama : UserControl
    {
        public bool dataInisial = true;
        public ArrayList dsDataSource = null;

        public ucLapSama()
        {
            InitializeComponent();
        }

        public void displayData()
        {
            if (dataInisial == true)
            {
                gridControl1.DataSource = null;
                gridControl1.DataSource = dsDataSource;
            }
            else
            {
                gridControl1.RefreshDataSource();
            }
        }
    }
}
