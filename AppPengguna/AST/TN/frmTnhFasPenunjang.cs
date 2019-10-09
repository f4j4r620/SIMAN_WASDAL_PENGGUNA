using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.AST.TN
{
    internal partial class frmTnhFasPenunjang : Form
    {
      
        private ucTnhFas uctnhfas;
        public string status;
        public decimal? NUM;
        private decimal? idKtnh;
        private decimal? idKtnhFas;

        public Thread myThread = null;
        private frmProgres progresBar = null;
        public char modeCrud = 'A';
        GridView viewTerpilih = null;
        public bool rowTerakhir = false;


        private SvcTnhFasPenunjangSelect.BPSIMANSROW_M_KTNH_FAS_PENUNJANG selectedData;
        private SvcTnhFasPenunjangCrud.call_pttClient crudData;
        private SvcTnhFasPenunjangCrud.InputParameters inputCrud;
        private SvcTnhFasPenunjangCrud.OutputParameters outCrud;


        public frmTnhFasPenunjang(ucTnhFas uctnhfas, string status)
        {
            InitializeComponent();
          
            this.uctnhfas = uctnhfas;
            this.status = status;
            this.idKtnh = uctnhfas.IdKtnh;
            this.selectedData = uctnhfas.selectedData;
            if (uctnhfas.selectedData != null)
            {
                this.NUM = uctnhfas.selectedData.NUM;
            }
            
            this.init();
            if (this.status == "detail")
            {
                this.bbiSave.Enabled = false;
            }
            

        }
        private void init()
        {
            if (this.status == "input")
            {
                this.gcTnhFas.Text = "Input Fasilitas Penunjang";
                this.idKtnhFas = 0;
                this.teNmFas.ResetText();
                this.teListrik.ResetText();
                this.tePam.ResetText();
                this.teTelepon.ResetText();
                this.teGas.ResetText();
                this.teSalLimbah.ResetText();
                this.teLainnya.ResetText();
                this.meKet.ResetText();
            }
            else if (this.status == "edit")
            {

                this.gcTnhFas.Text = "Edit Fasilitas Penunjang";
                this.idKtnhFas = selectedData.ID_KTNH_FAS_PENUNJANG;
                this.teNmFas.Text = selectedData.NM_FASILITAS;
                this.teListrik.Text = selectedData.LISTRIK;
                this.tePam.Text = selectedData.PAM;
                this.teTelepon.Text = selectedData.TELPON;
                this.teGas.Text = selectedData.GAS;
                this.teSalLimbah.Text = selectedData.SAL_LIMBAH;
                this.teLainnya.Text = selectedData.LAINNYA;
                this.meKet.Text = selectedData.KET;
            }
            else
            {
                this.gcTnhFas.Text = "Detail Fasilitas Penunjang";
                this.idKtnhFas = selectedData.ID_KTNH_FAS_PENUNJANG;
                this.teNmFas.Text = selectedData.NM_FASILITAS;
                this.teListrik.Text = selectedData.LISTRIK;
                this.tePam.Text = selectedData.PAM;
                this.teTelepon.Text = selectedData.TELPON;
                this.teGas.Text = selectedData.GAS;
                this.teSalLimbah.Text = selectedData.SAL_LIMBAH;
                this.teLainnya.Text = selectedData.LAINNYA;
                this.meKet.Text = selectedData.KET;
            }
        }


        #region Progress Bar
        public void progBar(BarItemVisibility str)
        {

            if (this.InvokeRequired)
            {
                ProgBar d = new ProgBar(progBar);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                this.beMarqueeBar.Visibility = str;
            }

        }

        public void ShowProgresBar()
        {
            this.progBar(BarItemVisibility.Always);
        }
        public void ShowProgresBarDelete()
        {

        }
        #endregion

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string nmFas = teNmFas.Text;
            string listrik = teListrik.Text;
            string pam = tePam.Text;
            string telepon = teTelepon.Text;
            string gas = teGas.Text;
            string salLimbah = teSalLimbah.Text;
            string lainnya = teLainnya.Text;
            string ket = meKet.Text;
            try
            {
                myThread = new Thread(new ThreadStart(ShowProgresBar));
                myThread.Start();
                SvcTnhFasPenunjangCrud.InputParameters parInp = new SvcTnhFasPenunjangCrud.InputParameters();
                parInp.P_ID_KTNHSpecified = true;
                parInp.P_ID_KTNH_FAS_PENUNJANGSpecified = true;
                parInp.P_ID_KTNH_FAS_PENUNJANG = idKtnhFas;
                parInp.P_ID_KTNH = idKtnh;
                parInp.P_NM_FASILITAS = nmFas;
                parInp.P_LISTRIK = listrik;
                parInp.P_PAM = pam;
                parInp.P_TELPON = telepon;
                parInp.P_GAS = gas;
                parInp.P_SAL_LIMBAH = salLimbah;
                parInp.P_LAINNYA = lainnya;
                parInp.P_KET = ket;



  
                if (this.status == "input")
                {
                    parInp.P_SELECT = "C";
                }
                else
                {
                    parInp.P_SELECT = "U";
                }

                this.modeCrud = Convert.ToChar(parInp.P_SELECT);
                this.crudData = new SvcTnhFasPenunjangCrud.call_pttClient(konfigApp.SvcTnhFasCrud_name,konfigApp.SvcTnhFasCrud_address);
                crudData.Open();
                this.crudData.Beginexecute(parInp, new AsyncCallback(crudFasTnh), "");
            }
            catch
            {
                this.modeCrud = 'A';
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.init();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        public void hapusData()
        {
            if (MessageBox.Show(konfigApp.teksHapusData + " No " +this.selectedData.NUM.ToString() + " ?", konfigApp.judulHapusData,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    myThread = new Thread(new ThreadStart(ShowProgresBar));
                    myThread.Start();

                    SvcTnhFasPenunjangCrud.InputParameters parInp = new SvcTnhFasPenunjangCrud.InputParameters();
                    parInp.P_ID_KTNH_FAS_PENUNJANGSpecified = true;
                    parInp.P_ID_KTNH_FAS_PENUNJANG = idKtnhFas;
                    parInp.P_SELECT = "D";

                    this.modeCrud = Convert.ToChar(parInp.P_SELECT);

                    this.crudData = new SvcTnhFasPenunjangCrud.call_pttClient(konfigApp.SvcTnhFasCrud_name, konfigApp.SvcTnhFasCrud_address);
                    crudData.Open();
                    this.crudData.Beginexecute(parInp, new AsyncCallback(crudFasTnh), "");
                }
                catch
                {
                    this.modeCrud = 'A';
                    this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                    MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
                }
            }
        }

        #region crud
        public void crudFasTnh(IAsyncResult result)
        {
            try
            {
                outCrud = crudData.Endexecute(result);
                crudData.Close();
                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
                this.Invoke(new UbahDsDetail(this.ubahDsDetail), outCrud);
            }
            catch
            {

                this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);

                if ((this.modeCrud == 'C') || (this.modeCrud == 'U'))
                {
                    konfigApp.teksDialog = konfigApp.teksGagalSimpan;
                }
                else if (this.modeCrud == 'D')
                {
                    konfigApp.teksDialog = konfigApp.teksGagalHapus;
                }
                else
                {
                    konfigApp.teksDialog = konfigApp.teksGagalLain;
                }

                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);

            }
        }

        public delegate void UbahDsDetail(SvcTnhFasPenunjangCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcTnhFasPenunjangCrud.OutputParameters outCrud)
        {
            SvcTnhFasPenunjangSelect.BPSIMANSROW_M_KTNH_FAS_PENUNJANG dataPenyama = new SvcTnhFasPenunjangSelect.BPSIMANSROW_M_KTNH_FAS_PENUNJANG();

            dataPenyama.NUM = -999;
            dataPenyama.NUMSpecified = true;
            dataPenyama.ID_KTNH = idKtnh;
            dataPenyama.ID_KTNH_FAS_PENUNJANG = outCrud.PO_ID_KTNH_FAS_PENUNJANG;
            dataPenyama.NM_FASILITAS = this.teNmFas.Text;
            dataPenyama.LISTRIK = teListrik.Text;
            dataPenyama.PAM = tePam.Text;
            dataPenyama.TELPON = teTelepon.Text;
            dataPenyama.GAS = teGas.Text;
            dataPenyama.SAL_LIMBAH = teSalLimbah.Text;
            dataPenyama.LAINNYA = teLainnya.Text;
            dataPenyama.KET = meKet.Text;

            switch (this.modeCrud)
            {
                case 'C':
                    
                     dataPenyama.NUM =  uctnhfas.binder.Count + 1;
                    uctnhfas.binder.Add(dataPenyama);
                    uctnhfas.gvUcDtl.RefreshData();
                    uctnhfas.StrTotalGrid.Caption = (Convert.ToInt64(uctnhfas.StrTotalGrid.Caption) + 1).ToString();
                    uctnhfas.StrTotalDb.Caption = (Convert.ToInt64(uctnhfas.StrTotalDb.Caption) + 1).ToString();
                    this.Close();
                    break;
                case 'U':
                   
                    dataPenyama.NUM = selectedData.NUM;
                    uctnhfas.binder.Remove(this.selectedData);
                    uctnhfas.binder.Add(dataPenyama);
                    uctnhfas.gvUcDtl.RefreshData();
                    this.Close();

                    break;
                case 'D':
                    uctnhfas.binder.Remove(this.selectedData);
                     uctnhfas.gvUcDtl.RefreshData();
                    uctnhfas.StrTotalGrid.Caption = (Convert.ToInt64(uctnhfas.StrTotalGrid.Caption) - 1).ToString();
                    uctnhfas.StrTotalDb.Caption = (Convert.ToInt64(uctnhfas.StrTotalDb.Caption) - 1).ToString();
                    this.Close();
                    break;
            }
        }

        #endregion
    }
}
