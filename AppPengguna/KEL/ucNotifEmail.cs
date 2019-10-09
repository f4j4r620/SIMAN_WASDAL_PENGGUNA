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
    public partial class ucNotifEmail : UserControl
    {
        public ucNotifEmail()
        {
            InitializeComponent();
        }

        public bool dataInisial = true;
        ArrayList arrayList;

        SvcKelNotifEmail.emailSelect_pttClient fetchdata;
        SvcKelNotifEmail.OutputParameters output;

        public void load()
        {
            try
            {
                SvcKelNotifEmail.InputParameters parInp = new SvcKelNotifEmail.InputParameters();
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
                parInp.STR_WHERE = "NO_TIKET_KELOLA = "+konfigApp.noTiketKelola;

                fetchdata = new SvcKelNotifEmail.emailSelect_pttClient();
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

        private delegate void DsRskPspBmn(SvcKelNotifEmail.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelNotifEmail.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_NOTIFIKASI_EMAIL.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelNotifEmail.DBKELOLASROW_NOTIFIKASI_EMAIL item in dataOut.SF_ROW_NOTIFIKASI_EMAIL)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            gridView1.BestFitColumns();
        }
    }
}
