using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;

namespace AppPengguna.KSK.SIMANSPAN
{
    public partial class ucPNBPSPAN : UserControl
    {
        public SvcGridPnbpSpan.WASDALSROW_GRID_PNBP_SPAN dataTerpilih;
        GridView viewTerpilih = null;
        public bool dataInisial = true;
        public ArrayList dsDataSource = null;
        public string fieldDicari = "";
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private bool initModeLoad = true;
        private string cariSebelumnya = "";
        private string strCari = "";
        public CariDataOnline cariDataOnline;
        public DetailDataGrid detailDataGrid;
        public detail filter;

        public ucPNBPSPAN()
        {
            InitializeComponent();
            seTahun.EditValue = konfigApp.tahunAnggaran;
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

        private void sbFilter_Click(object sender, EventArgs e)
        {
            string periode = " AND periode = '" + seTahun.Text + rgSemester.EditValue.ToString() + "'";
            filter(periode);
        }

      

    }
}
