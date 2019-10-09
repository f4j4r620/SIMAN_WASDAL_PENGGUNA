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
    public partial class ucDokumenAnalisis : UserControl
    {
        public ucDokumenAnalisis()
        {
            InitializeComponent();
        }

        public bool dataInisial = true;
        ArrayList arrayList;

        SvcKelDokAnalisis.selectDokAnalisis_pttClient fetchdata;
        SvcKelDokAnalisis.OutputParameters output;

        public void load()
        {
            try
            {
                SvcKelDokAnalisis.InputParameters parInp = new SvcKelDokAnalisis.InputParameters();
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

                fetchdata = new SvcKelDokAnalisis.selectDokAnalisis_pttClient();
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

        private delegate void DsRskPspBmn(SvcKelDokAnalisis.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelDokAnalisis.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_ASET_DOK_ANALISIS.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelDokAnalisis.DBKELOLASROW_ASET_KELOLA_DOK_ANALISIS item in dataOut.SF_ROW_ASET_DOK_ANALISIS)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            gridView1.BestFitColumns();
        }
    }
}
