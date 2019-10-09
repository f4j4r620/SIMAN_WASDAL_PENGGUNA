using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace AppPengguna.AST.ATB
{
    public partial class UcATBPerlengakapanForm : Form
    {
         private SvcATBPerkapCrud.call_pttClient crudCaller;
         private SvcATBPerkapCrud.InputParameters crudInput;
         private SvcATBPerkapCrud.OutputParameters crudOut;
        private SvcATBPerkapSelect.BPSIMANSROW_KTWJD_PERLENGKAP rowData;
        private Thread myThread;
        private string operation;
        private char modeCrud;
        private FrmKoorSatker frmKoorsSatker;

        private UcATBPerlengkapan ucPerlengkapan;


        public UcATBPerlengakapanForm(UcATBPerlengkapan _ucPerlengkapan)
        {
            this.ucPerlengkapan = _ucPerlengkapan;
            InitializeComponent();
        }

        public UcATBPerlengakapanForm(UcATBPerlengkapan _ucPerlengkapan, string _operation)
        {
            this.ucPerlengkapan = _ucPerlengkapan;
            this.operation = _operation;
            InitializeComponent();
            if (operation == "U")
            {
                rowData = _ucPerlengkapan.SelectedData;
                showData();
            }
            else if (operation == "detail")
            {
                this.bbiSave.Enabled = false;
            }

        }

        public UcATBPerlengakapanForm()
        {
            InitializeComponent();
        }

        private void showData()
        {
            //teFile.Text = rowData.FILE;
            teNmPerkap.Text = rowData.NM_PERLENGKAP;
            meKet.Text = rowData.KET;
            

        }


        public void crudOperation(string _crudOperation)
        {
            this.Enabled = false;
            myThread = new Thread(new ThreadStart(frmKoorsSatker.ShowProgresBar));
            myThread.Start();

            crudCaller = new SvcATBPerkapCrud.call_pttClient();
            crudCaller.Open();
            crudCaller.Beginexecute(parseParam(_crudOperation), new AsyncCallback(this.crudResult), "");
        }

        public void crudResult(IAsyncResult result)
        {
            try
            {
                crudOut = crudCaller.Endexecute(result);
                crudCaller.Close();
                this.Invoke(new ProgBar(frmKoorsSatker.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
                
                  konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
                      

                if (crudOut.PO_RESULT == "Y")
                {
                    MessageBox.Show(konfigApp.teksDialog, konfigApp.judulBerhasilAmbil);
                    this.Invoke(new UbahDsDetail(this.ubahDsDetail), crudOut);
                }
                else
                {
                    MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
                }

                // this.Invoke(new AktifkanForm(konfigApp.aktifkanForm), "");
            }
            catch
            {

                this.Invoke(new ProgBar(frmKoorsSatker.progBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(aktifkanForm), "");
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
        public delegate void UbahDsDetail(SvcATBPerkapCrud.OutputParameters outCrud);
        public decimal? ID_KTWJD_PERLENGKAP;
        public void ubahDsDetail(SvcATBPerkapCrud.OutputParameters outCrud)
        {
            SvcATBPerkapSelect.BPSIMANSROW_KTWJD_PERLENGKAP dataPenyama = new SvcATBPerkapSelect.BPSIMANSROW_KTWJD_PERLENGKAP();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KTWJD_PERLENGKAP = outCrud.PO_ID_KTWJD_PERLENGKAP;
            this.ID_KTWJD_PERLENGKAP = outCrud.PO_ID_KTWJD_PERLENGKAP;

            switch (this.modeCrud)
            {
                case 'C':

                    this.ucPerlengkapan.dataInisial = false;
                    this.ucPerlengkapan.getById = true;
                    this.ucPerlengkapan.getInitMutLny(" ID_KTWJD_PERLENGKAP = " + this.ID_KTWJD_PERLENGKAP.ToString());

                    this.Close();

                    break;
                case 'U':
                    ucPerlengkapan.binder.Remove(this.ucPerlengkapan.selectedData);
                    this.ucPerlengkapan.dataInisial = false;
                    this.ucPerlengkapan.getById = true;
                    this.ucPerlengkapan.getInitMutLny(" ID_KTWJD_PERLENGKAP = " + this.ID_KTWJD_PERLENGKAP.ToString());

                    this.Close();
                    break;
                case 'D':
                    ucPerlengkapan.binder.Remove(this.ucPerlengkapan.selectedData);
                    ucPerlengkapan.gvUcDtl.RefreshData();
                    ucPerlengkapan.StrTotalGrid.Caption = (Convert.ToInt64(ucPerlengkapan.StrTotalGrid.Caption) - 1).ToString();
                    ucPerlengkapan.StrTotalDb.Caption = (Convert.ToInt64(ucPerlengkapan.StrTotalDb.Caption) - 1).ToString();

                    this.Close();
                    break;
            }
        }

        public void aktifkanForm(string str)
        {
            this.Enabled = true;
        }

        private SvcATBPerkapCrud.InputParameters parseParam(string _crudOperation)
        {
            crudInput = new SvcATBPerkapCrud.InputParameters();

            if (_crudOperation == "U" || _crudOperation == "D")
            {
                crudInput.P_ID_KTWJD_PERLENGKAP = ucPerlengkapan.SelectedData.ID_KTWJD_PERLENGKAP;
                crudInput.P_ID_KTWJD_PERLENGKAPSpecified = true;
            }

            crudInput.P_ID_KTWJD = ucPerlengkapan.ID_KTWJD;
            crudInput.P_ID_KTWJDSpecified = true;


            crudInput.P_NM_PERLENGKAP = (teNmPerkap.Text == "-" ? null : teNmPerkap.Text);
            crudInput.P_KET = meKet.Text;

            crudInput.P_SELECT = _crudOperation;
            this.modeCrud = Convert.ToChar(_crudOperation);

            return crudInput;
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            crudOperation(this.operation);
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            teNmPerkap.Text = "";
            meKet.Text = "";
        }

        private void bbiTutup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
