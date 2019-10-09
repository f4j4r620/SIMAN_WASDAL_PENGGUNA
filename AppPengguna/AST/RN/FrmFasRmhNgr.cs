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

namespace AppPengguna.AST.RN
{


    public partial class FrmFasRmhNgr : Form
    {
        private SvcFasRmhNgrCrud.call_pttClient crudCaller;
        private SvcFasRmhNgrCrud.InputParameters crudInput;
        private SvcFasRmhNgrCrud.OutputParameters crudOut;
        private SvcFasRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_FAS_PENUNJANG rowData;
        private Thread myThread;
        public string STATUS;
        private char modeCrud;

        private UcFasRmhNgr ucFasRmhNgr;

    

        public FrmFasRmhNgr(UcFasRmhNgr _ucFasRmhNgr, string _operation)
        {
            this.ucFasRmhNgr = _ucFasRmhNgr;
            this.STATUS = _operation;
            InitializeComponent();
            if (STATUS == "U")
            {
                rowData = ucFasRmhNgr.SelectedData;
                Init();
            }
            else if (STATUS == "detail")
            {
                this.barSimpan.Enabled = false;
            }
        }

  


        private void Init()
        {
          if (this.STATUS != "U")
            {
                teLain.Text = "";
                teGas.Text = "";
                teLimbh.Text = "";
                teListrik.Text = "";
                tePam.Text = "";
                teTelp.Text = "";
            }
            else
            {
                teLain.Text = rowData.LAINNYA;
                teGas.Text = rowData.GAS;
                teLimbh.Text = rowData.SAL_LIMBAH;
                teListrik.Text = rowData.LISTRIK;
                tePam.Text = rowData.PAM;
                teTelp.Text = rowData.TELPON;
                meKet.Text = rowData.KET;
            }

        }

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

        public void crudOperation(string _crudOperation)
        {
            myThread = new Thread(new ThreadStart(ShowProgresBar));
            myThread.Start();

            crudCaller = new SvcFasRmhNgrCrud.call_pttClient();
            crudCaller.Open();
            crudCaller.Beginexecute(parseParam(_crudOperation), new AsyncCallback(this.crudResult), "");
        }

        public void crudResult(IAsyncResult result)
        {
            try
            {
                crudOut = crudCaller.Endexecute(result);
                crudCaller.Close();
                this.Invoke(new ProgBar(progBar), BarItemVisibility.Never);
                konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
               

                if (crudOut.PO_RESULT == "Y")
                {
                    MessageBox.Show(konfigApp.teksDialog, konfigApp.judulBerhasilAmbil);
                    this.Invoke(new ShowDataCrud(this.showDataCrud), crudOut);
                }
                else
                {
                    MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
                }

                // this.Invoke(new AktifkanForm(konfigApp.aktifkanForm), "");
            }
            catch
            {

                this.Invoke(new ProgBar(progBar), BarItemVisibility.Never);
                //this.Invoke(new AktifkanForm(konfigApp.aktifkanForm), "");
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

        private delegate void ShowDataCrud(SvcFasRmhNgrCrud.OutputParameters dataOut);

        public void showDataCrud(SvcFasRmhNgrCrud.OutputParameters dataOut)
        {
            SvcFasRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_FAS_PENUNJANG dataPenyama = new SvcFasRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_FAS_PENUNJANG();
            dataPenyama.ID_KRMH_FAS_PENUNJANG = dataOut.PO_ID_KRMH_FAS_PENUNJANG;

            dataPenyama.NUM = 99;
            dataPenyama.NUMSpecified = true;
            switch (this.modeCrud)
            {
                case 'C':

                    this.ucFasRmhNgr.dataInisial = false;
                    this.ucFasRmhNgr.getById = true;
                    //this.ucFasRmhNgr.getInitFasRmh(" ID_KRMH_FAS_PENUNJANG = " + dataOut.PO_ID_KRMH_FAS_PENUNJANG.ToString());
                 
                    this.Close();
                    this.ucFasRmhNgr.search = "";
                    this.ucFasRmhNgr.initGrid();
                    this.ucFasRmhNgr.getInitFasRmh();
                    break;
                case 'U':
                    this.ucFasRmhNgr.binder.Remove(rowData);
                    this.ucFasRmhNgr.dataInisial = false;
                    this.ucFasRmhNgr.getById = true;
                    //this.ucFasRmhNgr.getInitFasRmh(" ID_KRMH_FAS_PENUNJANG = " + dataOut.PO_ID_KRMH_FAS_PENUNJANG.ToString());
                    
                    this.Close();
                    this.ucFasRmhNgr.search = "";
                    this.ucFasRmhNgr.initGrid();
                    this.ucFasRmhNgr.getInitFasRmh();
                    break;
                case 'D':

                    this.ucFasRmhNgr.binder.Remove(rowData);
                    this.ucFasRmhNgr.gvUcDtl.RefreshData();
                    this.ucFasRmhNgr.StrTotalGrid.Caption = (Convert.ToInt64(this.ucFasRmhNgr.StrTotalGrid.Caption) - 1).ToString();
                    this.ucFasRmhNgr.StrTotalDb.Caption = (Convert.ToInt64(this.ucFasRmhNgr.StrTotalDb.Caption) - 1).ToString();
                    
                    this.Close();
                    this.ucFasRmhNgr.search = "";
                    this.ucFasRmhNgr.initGrid();
                    this.ucFasRmhNgr.getInitFasRmh();
                    break;
            }

        }
        private SvcFasRmhNgrCrud.InputParameters parseParam(string _crudOperation)
        {
            crudInput = new SvcFasRmhNgrCrud.InputParameters();

            if (_crudOperation == "U" || _crudOperation == "D")
            {
                crudInput.P_ID_KRMH_FAS_PENUNJANG = ucFasRmhNgr.SelectedData.ID_KRMH_FAS_PENUNJANG;
                crudInput.P_ID_KRMH_FAS_PENUNJANGSpecified = true;
            }

            crudInput.P_ID_KRMH_NEG = ucFasRmhNgr.ID_KRMH_NEG;
            crudInput.P_ID_KRMH_NEGSpecified = true;

            crudInput.P_LISTRIK = (teListrik.Text == "-" ? null : teListrik.Text);
            crudInput.P_GAS = (teGas.Text == "-" ? null : teGas.Text);
            crudInput.P_PAM = (tePam.Text == "-" ? null : tePam.Text);
            crudInput.P_SAL_LIMBAH = (teLimbh.Text == "-" ? null : teLimbh.Text);
            crudInput.P_LAINNYA = (teLain.Text == "-" ? null : teLain.Text);
            crudInput.P_KET = (meKet.Text == "-" ? null : meKet.Text);
            crudInput.P_TELPON = teTelp.Text;
            crudInput.P_SELECT = _crudOperation;
            this.modeCrud = Convert.ToChar(_crudOperation);

            return crudInput;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            crudOperation(this.STATUS);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Init();
           
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmFasRmhNgr_Load(object sender, EventArgs e)
        {
            this.progBar(BarItemVisibility.Never);
        }
    }
}
