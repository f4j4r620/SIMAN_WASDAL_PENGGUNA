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
    public partial class ucPilihAset : UserControl
    {
        public ucPilihAset()
        {
            InitializeComponent();
        }

        public bool dataInisial = true;
        ArrayList arrayList;

        SvcKelPilihAset.execute_pttClient fetchdata;
        SvcKelPilihAset.OutputParameters output;

        public void load()
        {
            try
            {
                SvcKelPilihAset.InputParameters parInp = new SvcKelPilihAset.InputParameters();
                parInp.P_NO_TIKET_KELOLA = konfigApp.noTiketKelola;
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
                parInp.STR_WHERE = "IS_DELETED != 'Y' ";

                fetchdata = new SvcKelPilihAset.execute_pttClient();
                fetchdata.Beginexecute(parInp, new AsyncCallback(getRskPspBmn), null);
            }
            catch
            {

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

        private delegate void DsRskPspBmn(SvcKelPilihAset.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelPilihAset.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_ASET_STAT_GUNA.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelPilihAset.DBKELOLASROW_ASET_STAT_GUNA item in dataOut.SF_ROW_ASET_STAT_GUNA)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            bandedGridView1.BestFitColumns();
        }
    }
}
