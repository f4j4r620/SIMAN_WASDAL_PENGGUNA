using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KEL
{
    public partial class ucMainGrid : UserControl
    {
        public ucMainGrid()
        {
            InitializeComponent();
        }

        public detailPengelolaan detail;

        public bool dataInisial = true;
        public string noTiket = "";
        ArrayList arrayList;

        public SvcKelMainSelect.DBKELOLASROW_GRID_KELOLA_ALL dataTerpilih;
        SvcKelMainSelect.gridKelolaSelect_pttClient fetchdata;
        SvcKelMainSelect.OutputParameters output;

        public void load()
        {
            try
            {
                SvcKelMainSelect.InputParameters parInp = new SvcKelMainSelect.InputParameters();
                parInp.P_LEVEL_AKTIF = "SATKER";
                if (dataInisial == true)
                {
                    konfigApp.currentMaks = konfigApp.dataAkhir;
                    konfigApp.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    konfigApp.currentMin = konfigApp.currentMaks + 1;
                    konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataAkhir;
                }
                parInp.P_MAX = konfigApp.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = konfigApp.currentMin;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = string.Format("SK_KEPUTUSAN IS NOT NULL AND KD_PELAYANAN = '{0}' AND ID_SATKER = {1} ", konfigApp.kdPelayanan, konfigApp.idSatker);

                fetchdata = new SvcKelMainSelect.gridKelolaSelect_pttClient();
                fetchdata.Beginexecute(parInp, new AsyncCallback(getRskPspBmn), null);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskPspBmn(IAsyncResult result)
        {
            try
            {
                output = fetchdata.Endexecute(result);
                fetchdata.Close();
                this.Invoke(new DsRskPspBmn(dsRskPspBmn), output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskPspBmn(SvcKelMainSelect.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelMainSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_GRID_KELOLA_ALL.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelMainSelect.DBKELOLASROW_GRID_KELOLA_ALL item in dataOut.SF_ROW_GRID_KELOLA_ALL)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            gridView1.BestFitColumns();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            detailDataTerpilih();
        }

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
                detail(null, null);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView viewTerpilih = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (viewTerpilih.SelectedRowsCount > 0)
                    dataTerpilih = (SvcKelMainSelect.DBKELOLASROW_GRID_KELOLA_ALL)viewTerpilih.GetRow(e.FocusedRowHandle);
            }
            catch (Exception ex)
            {

            }
            
        }
        
    }
}
