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


    public partial class FrmDetRmhNgr : Form
    {

        private SvcDetRmhNgrCrud.call_pttClient crudCaller;
        private SvcDetRmhNgrCrud.InputParameters crudInput;
        private SvcDetRmhNgrCrud.OutputParameters crudOut;
        private SvcDetRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DETAIL rowData;
        private Thread myThread;
        public string STATUS;
        private decimal? ID_KRMH_NEG;
        private decimal? ID_KRMH_DETAIL;
        public decimal? NUM;
        public string KD_TIPE_RUANG;
        private char modeCrud;

        private UcDetRmhNgr ucDetRmhNgr;



        public FrmDetRmhNgr(UcDetRmhNgr _ucDetRmhNgr, string _operation)
        {
            this.ucDetRmhNgr = _ucDetRmhNgr;
            this.ID_KRMH_NEG = _ucDetRmhNgr.ID_KRMH_NEG;
            this.STATUS = _operation;
            InitializeComponent();
            if (STATUS == "U")
            {
                rowData = ucDetRmhNgr.SelectedData;
                this.ID_KRMH_DETAIL = ucDetRmhNgr.SelectedData.ID_KRMH_DETAIL;
                this.NUM = ucDetRmhNgr.SelectedData.NUM;
                showData();
            }
            if (this.STATUS == "detail")
            {
                this.bbSave.Enabled = false;
            }
        }

 
        private void showData()
        {
            KD_TIPE_RUANG = rowData.KD_TIPE_RUANG;
            teFile.Text = rowData.NMFILE;
            teNmRuang.Text = rowData.UR_TIPE_RUANG;// teUrUAKPB.Properties.ReadOnly = true;

            seJml.Value = (decimal)rowData.JUMLAH;
            meKet.Text = rowData.KET;
           
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

            crudCaller = new SvcDetRmhNgrCrud.call_pttClient(konfigApp.SvcDetRmhNgrCrud_name,konfigApp.SvcDetRmhNgrCrud_address);
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
                switch (this.modeCrud)
                {
                    case 'C':
                        konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
                        //this.Invoke(MessageBox.Show(konfigApp.teksDialog, konfigApp.judulBerhasilAmbil),new String[]{""});
                        break;
                    case 'U':
                        konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;

                        break;
                    case 'D':
                        konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;

                        break;
                    default:
                        konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
                        break;
                }

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

        public delegate void UbahDsDetail(SvcDetRmhNgrCrud.OutputParameters outCrud);

        public void ubahDsDetail(SvcDetRmhNgrCrud.OutputParameters outCrud)
        {
            SvcDetRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DETAIL dataPenyama = new SvcDetRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_DETAIL();

            dataPenyama.NUM = 99;
            dataPenyama.ID_KRMH_DETAIL = outCrud.PO_ID_KRMH_DETAIL;
            this.ID_KRMH_DETAIL = outCrud.PO_ID_KRMH_DETAIL;
            if (this.ucDetRmhNgr.SelectedData != null)
            {
                this.NUM = ucDetRmhNgr.SelectedData.NUM;
            }
            switch (this.modeCrud)
            {
                case 'C':


                        this.ucDetRmhNgr.dataInisial = false;
                        this.ucDetRmhNgr.getById = true;
                        this.Close();
                        //this.ucDetRmhNgr.getInitDetRmh(" ID_KRMH_DETAIL = " + this.ID_KRMH_DETAIL.ToString());
                        this.ucDetRmhNgr.search = "";
                        this.ucDetRmhNgr.initGrid();
                        this.ucDetRmhNgr.getInitDetRmh();
                        
                    
                    break;
                case 'U':
                    ucDetRmhNgr.binder.Remove(this.ucDetRmhNgr.SelectedData);
                   
                        this.ucDetRmhNgr.dataInisial = false;
                        this.ucDetRmhNgr.getById = true;
                        //this.ucDetRmhNgr.getInitDetRmh(" ID_KRMH_DETAIL = " + this.ID_KRMH_DETAIL.ToString());
                        
                        this.Close();
                        this.ucDetRmhNgr.search = "";
                        this.ucDetRmhNgr.initGrid();
                        this.ucDetRmhNgr.getInitDetRmh();
                   
                    break;
                case 'D':
                    ucDetRmhNgr.binder.Remove(this.ucDetRmhNgr.SelectedData);
                    ucDetRmhNgr.gvUcDtl.RefreshData();
                    ucDetRmhNgr.StrTotalGrid.Caption = (Convert.ToInt64(ucDetRmhNgr.StrTotalGrid.Caption) - 1).ToString();
                    ucDetRmhNgr.StrTotalDb.Caption = (Convert.ToInt64(ucDetRmhNgr.StrTotalDb.Caption) - 1).ToString();
                    
                    this.Close();
                      this.ucDetRmhNgr.search = "";
                        this.ucDetRmhNgr.initGrid();
                        this.ucDetRmhNgr.getInitDetRmh();
                    break;
            }
        }

        private SvcDetRmhNgrCrud.InputParameters parseParam(string _crudOperation)
        {
            crudInput = new SvcDetRmhNgrCrud.InputParameters();

            if (_crudOperation == "U" || _crudOperation =="D")
            {
                crudInput.P_ID_KRMH_DETAIL = ucDetRmhNgr.SelectedData.ID_KRMH_DETAIL;
                crudInput.P_ID_KRMH_DETAILSpecified = true; 
            }

            crudInput.P_ID_KRMH_NEG = ucDetRmhNgr.ID_KRMH_NEG;
            crudInput.P_ID_KRMH_NEGSpecified = true;
            crudInput.P_KD_TIPE_RUANG = this.KD_TIPE_RUANG;
            crudInput.P_JUMLAH = ((decimal?)seJml.Value == 0 ? null : (decimal?)seJml.Value);
            crudInput.P_JUMLAHSpecified = true;
            crudInput.P_KET = (meKet.Text == "-" ? null : meKet.Text);
            //crudInput. = (teNmRuang.Text == "-" ? null : teNmRuang.Text);
            //crudInput.P_FILE = (teFile.Text == "-" ? null : teFile.Text);
            //crudInput.P_TYPE_DOC = null;

            crudInput.P_SELECT = _crudOperation;
            this.modeCrud = Convert.ToChar(_crudOperation);

            return crudInput;
        }

        private void FrmDetRmhNgr_Load(object sender, EventArgs e)
        {
            this.progBar(BarItemVisibility.Never);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            crudOperation(this.STATUS);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            teFile.Text = "";
            meKet.Text = "";
            teNmRuang.Text = "";
            seJml.Value = 0;
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void sbFile_Click(object sender, EventArgs e)
        {
            ofpFile.InitialDirectory = "C:";
            ofpFile.Title = "Pilih File";
            ofpFile.FileName = "";
            ofpFile.ShowDialog();

            teFile.Text = ofpFile.FileName;
        }

        PU.FrmPUTipeRuang tipRuang;
        private void btnTipeRuangan_Click(object sender, EventArgs e)
        {
            tipRuang = new PU.FrmPUTipeRuang() {
                ambilTipeRuang = new PU.AmbilTipeRuang(ambilTipRuang) 
            };
            tipRuang.ShowDialog();
        }

        private void ambilTipRuang(string kd, string ur)
        {
            KD_TIPE_RUANG = kd;
            teNmRuang.Text = ur;
        }

     

       

    }
}
