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
    public partial class ucSuratPermohonan : UserControl
    {
        public ucSuratPermohonan()
        {
            InitializeComponent();
        }

        public bool dataInisial = true;
        ArrayList arrayList;

        SvcKelSuratPermohonan.selectPermohonan_pttClient fetchdata;
        SvcKelSuratPermohonan.OutputParameters output;

        public void load()
        {
            try
            {
                SvcKelSuratPermohonan.InputParameters parInp = new SvcKelSuratPermohonan.InputParameters();
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

                fetchdata = new SvcKelSuratPermohonan.selectPermohonan_pttClient();
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

        private delegate void DsRskPspBmn(SvcKelSuratPermohonan.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelSuratPermohonan.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_ASET_KELOLA_SURAT.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelSuratPermohonan.DBKELOLASROW_ASET_KELOLA_SURAT item in dataOut.SF_ROW_ASET_KELOLA_SURAT)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            gridView1.BestFitColumns();
        }
    }
}
