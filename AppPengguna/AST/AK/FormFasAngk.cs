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
using System.Reflection;
using AppPengguna.PU;


namespace AppPengguna.AST.AK
{
    public partial class FormFasAngk : Form
    {
        private SvcFasAngkCrud.call_pttClient crudCaller;
        private SvcFasAngkCrud.InputParameters crudInput;
        private SvcFasAngkCrud.OutputParameters crudOut;
        private SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG selectedRow;
        private Thread myThread;
        private char modeCrud;
        private string operation;
        private bool edited = false;
        public bool Edited
        {
            get
            {
                return this.edited;
            }
            set
            {
                this.edited = value;
            }
        }
        public SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG SelectedRow
        {
            get
            {
                return selectedRow;
            }
        }


        public string STATUS;
        public UcFasAngk ucFasAngk;
        public FormFasAngk(Char _modeCrud, SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG _selectedRow, UcFasAngk _ucFasAngk)
        {
            InitializeComponent();
            this.ucFasAngk = _ucFasAngk;
            this.modeCrud = _modeCrud;
            if (this.modeCrud == 'U') {
                this.selectedRow = _selectedRow;
                this.showDetailFas();
                this.operation = "U";
                this.STATUS = "edit";
            }
            else if (this.modeCrud == 'D') {
                this.operation = "D";
                this.STATUS = "hapus";
                this.selectedRow = _selectedRow;
                this.bbFasAngkSimpan_ItemClick(null, null);
            }
            else if (this.modeCrud == 'C') {
                this.operation = "C";
                this.selectedRow = _selectedRow;
                this.STATUS = "input";
            }
            else if (this.modeCrud == 'V')
            {
                this.selectedRow = _selectedRow;
                this.showDetailFas();
                this.operation = "V";
                this.STATUS = "detail";
                this.bbFasAngkSimpan.Enabled = false;
            }
            
        }

        public void showDetailFas()
        {
            teNmFas.Text = selectedRow.NM_FASILITAS;
            teIsiFas.Text = selectedRow.ISI_FASILITAS;
            meKetFas.Text = selectedRow.KET;
        }

        private SvcFasAngkCrud.InputParameters parseParam(string _crudOperation)
        {
            crudInput = new SvcFasAngkCrud.InputParameters();

            if (_crudOperation == "U" || _crudOperation == "D")
            {

                crudInput.P_ID_KANGK_FAS_PENUNJANG = selectedRow.ID_KANGK_FAS_PENUNJANG;
                crudInput.P_ID_KANGK_FAS_PENUNJANGSpecified = true;
            }

            crudInput.P_ID_KANGK = this.ucFasAngk.ID_KANGK;
            crudInput.P_ID_KANGKSpecified = true;


            crudInput.P_NM_FASILITAS = (teNmFas.Text == "-" ? null : teNmFas.Text);
            crudInput.P_ISI_FASILITAS = (teIsiFas.Text == "-" ? null : teIsiFas.Text);
            crudInput.P_KET = (meKetFas.Text == "-" ? null : meKetFas.Text);

            crudInput.P_SELECT = _crudOperation;
            this.modeCrud = Convert.ToChar(_crudOperation);

            return crudInput;
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
                this.beMarqueBar.Visibility = str;
            }

        }
        private ThreadStart beMarqueeBar(BarItemVisibility barItemVisibility)
        {
            throw new NotImplementedException();
        }
        public void ShowProgresBar()
        {
            this.progBar(BarItemVisibility.Always);
        }


        #region Modul Crud
        public void crudOperation(string _crudOperation)
        {
            myThread = new Thread(new ThreadStart(ShowProgresBar));
            myThread.Start();

            crudCaller = new SvcFasAngkCrud.call_pttClient(konfigApp.SvcFasAngkCrud_name,konfigApp.SvcFasAngkCrud_address);
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
        public delegate void UbahDsDetail(SvcFasAngkCrud.OutputParameters outCrud);
        public decimal? ID_KANGK_FAS_PENUNJANG;
        public void ubahDsDetail(SvcFasAngkCrud.OutputParameters outCrud)
        {
            SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG dataPenyama = new SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KANGK_FAS_PENUNJANG = outCrud.PO_ID_KANGK_FAS_PENUNJANG;
            this.ID_KANGK_FAS_PENUNJANG = outCrud.PO_ID_KANGK_FAS_PENUNJANG;

            switch (this.modeCrud)
            {
                case 'C':

                    this.ucFasAngk.dataInisial = false;
                    this.ucFasAngk.getById = true;
                    this.ucFasAngk.getInitFasAngk(" ID_KANGK_FAS_PENUNJANG = " + this.ID_KANGK_FAS_PENUNJANG.ToString());

                    this.Close();

                    break;
                case 'U':
                    ucFasAngk.binder.Remove(this.ucFasAngk.selectedData);
                    this.ucFasAngk.dataInisial = false;
                    this.ucFasAngk.getById = true;
                    this.ucFasAngk.getInitFasAngk(" ID_KANGK_FAS_PENUNJANG = " + this.ID_KANGK_FAS_PENUNJANG.ToString());

                    this.Close();
                    break;
                case 'D':
                    ucFasAngk.binder.Remove(this.ucFasAngk.selectedData);
                    ucFasAngk.gvUcDtl.RefreshData();
                    ucFasAngk.StrTotalGrid.Caption = (Convert.ToInt64(ucFasAngk.StrTotalGrid.Caption) - 1).ToString();
                    ucFasAngk.StrTotalDb.Caption = (Convert.ToInt64(ucFasAngk.StrTotalDb.Caption) - 1).ToString();

                    this.Close();
                    break;
            }
        }
        #endregion

        #region Controller/Event Form
        private void bbFasAngkSimpan_ItemClick(object sender, ItemClickEventArgs e)
        {
            crudOperation(this.operation);
        }

        private void bbFasAngkRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            teNmFas.Text = "";
            teIsiFas.Text = "";
            meKetFas.Text = "";
        }

        private void bbFasAngkTutup_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
