using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace AppPengguna.KEL
{
    public partial class ucPilihAsetKp : UserControl
    {
        public ucPilihAsetKp()
        {
            InitializeComponent();
        }

        public bool dataInisial = true;
        ArrayList arrayList;

        public ToggleProgressBar toggleProgressBar;
        private Thread myThread;

        SvcKelPilihAsetKp.execute_pttClient fetchdata;
        SvcKelPilihAsetKp.OutputParameters output;

        #region Thread
        private void toggleProgBarPu(string kondisi)
        {
            if (kondisi == "start") this.aktifkanProgresBar();
            else this.nonAktifkanprogressBar();
        }

        private void aktifkanProgresBar()
        {
            toggleProgressBar("start");
        }

        private void nonAktifkanprogressBar()
        {
            toggleProgressBar("finish");
        }

        public delegate void AktifkanForm(string str);

        public void aktifkanForm(string str)
        {
            this.Enabled = true;
        }
        #endregion

        public void load()
        {
            try
            {
                this.Enabled = false;
                myThread = new Thread(new ThreadStart(aktifkanProgresBar));
                myThread.Start();
                SvcKelPilihAsetKp.InputParameters parInp = new SvcKelPilihAsetKp.InputParameters();
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

                fetchdata = new SvcKelPilihAsetKp.execute_pttClient();
                fetchdata.Beginexecute(parInp, new AsyncCallback(getRskPspBmn), null);
            }
            catch
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getRskPspBmn(IAsyncResult result)
        {
            try
            {
                output = fetchdata.Endexecute(result);
                fetchdata.Close();
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                this.Invoke(new DsRskPspBmn(dsRskPspBmn), output);
            }
            catch (Exception ex)
            {
                nonAktifkanprogressBar();
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void DsRskPspBmn(SvcKelPilihAsetKp.OutputParameters dataOut);

        private void dsRskPspBmn(SvcKelPilihAsetKp.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_ROW_ASET_KERJASAMA_MANFAAT.Count();
            if (dataInisial) arrayList = new ArrayList();

            foreach (SvcKelPilihAsetKp.DBKELOLASROW_ASET_KERJASAMA_MANFAAT item in dataOut.SF_ROW_ASET_KERJASAMA_MANFAAT)
                arrayList.Add(item);

            bindingSource1.DataSource = arrayList;
            bandedGridView1.BestFitColumns();
        }
    }
}
