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
    public partial class ucFaktorPengurang : UserControl
    {
        public ucFaktorPengurang()
        {
            InitializeComponent();
        }

        public bool dataInisial = true;
        ArrayList arrayList;

        SvcKelFaktorPengurang.faktorPengurangSelect_pttClient fetchdata;
        SvcKelFaktorPengurang.OutputParameters output;

        public void load()
        {
            try
            {
                SvcKelFaktorPengurang.InputParameters parInp = new SvcKelFaktorPengurang.InputParameters();
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

                fetchdata = new SvcKelFaktorPengurang.faktorPengurangSelect_pttClient();
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

        private delegate void DsRskPspBmn(SvcKelFaktorPengurang.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelFaktorPengurang.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_T_FAKTOR_PENGURANG.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelFaktorPengurang.DBKELOLASROW_T_FAKTOR_PENGURANG_HKERJA item in dataOut.SF_ROW_T_FAKTOR_PENGURANG)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            gridView1.BestFitColumns();
        }
    }
}
