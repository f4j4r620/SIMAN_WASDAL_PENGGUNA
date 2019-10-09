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
    public partial class ucAgenda : UserControl
    {
        public ucAgenda()
        {
            InitializeComponent();
        }

        public bool dataInisial = true;
        ArrayList arrayList;

        SvcKelAgenda.agendaSelect_pttClient fetchdata;
        SvcKelAgenda.OutputParameters output;

        public void load()
        {
            try
            {
                SvcKelAgenda.InputParameters parInp = new SvcKelAgenda.InputParameters();
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
                parInp.STR_WHERE = "";

                fetchdata = new SvcKelAgenda.agendaSelect_pttClient();
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

        private delegate void DsRskPspBmn(SvcKelAgenda.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelAgenda.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_T_AGENDA.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelAgenda.DBKELOLASROW_T_AGENDA item in dataOut.SF_ROW_T_AGENDA)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            gridView1.BestFitColumns();
        }
    }
}
