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
    public partial class ucRekapPNBP : UserControl
    {
        public SvcGridPnbpRekap.WASDALSROW_GRID_REKAP_PNBP_SPAN dataTerpilih;
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

        public ucRekapPNBP()
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

        #region Detail Data Grid
        private void gvGridSk_DoubleClick(object sender, EventArgs e)
        {
            detailDataTerpilih();
        }

        private void gvGridSk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                detailDataTerpilih();
            }
        }

        private void detailDataTerpilih()
        {
            if (dataTerpilih != null)
            {
                detailDataGrid(null, null);
            }
        }
        #endregion

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataTerpilih = (SvcGridPnbpRekap.WASDALSROW_GRID_REKAP_PNBP_SPAN)gridView1.GetRow(e.FocusedRowHandle);
        }

        private void sbFilter_Click(object sender, EventArgs e)
        {
            string periode = " AND periode = '" + seTahun.Text + rgSemester.EditValue.ToString()+"'";
            filter(periode);
        }


    }
}
