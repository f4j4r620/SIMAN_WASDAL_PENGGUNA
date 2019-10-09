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
    public partial class ucKelengkapanDokumen : UserControl
    {
        public ucKelengkapanDokumen()
        {
            InitializeComponent();
        }

        public bool dataInisial = true;
        ArrayList arrayList;

        SvcKelDokLengkap.selectDokAset_pttClient fetchdata;
        SvcKelDokLengkap.OutputParameters output;

        public void load()
        {
            try
            {
                SvcKelDokLengkap.InputParameters parInp = new SvcKelDokLengkap.InputParameters();
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

                fetchdata = new SvcKelDokLengkap.selectDokAset_pttClient();
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

        private delegate void DsRskPspBmn(SvcKelDokLengkap.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelDokLengkap.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_ASET_KELOLA.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelDokLengkap.DBKELOLASROW_ASET_KELOLA item in dataOut.SF_ROW_ASET_KELOLA)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            gridView1.BestFitColumns();
        }
    }
}
