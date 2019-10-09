using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Threading;

namespace AppPengguna.AST.AK
{
    public partial class UcNmFasAngk : UserControl
    {
        private GridColumn kolom;
        private BindingSource binder;
        private GridView selectedView = null;
        private GridView newRow = null;
        private bool LastRow = false;
        private int posisiRow = 0;
        private bool initialData = true;
        private bool initialDataSearch = true;
        private bool isDataInserted = false;

        public NonAktifkanFormSatker nonAktifForm;
        public AktifkanFormSatker aktifkanForm;
        public showProgresBar ShowProgresBar;
        public closeProgresBar CloseProgresBar;

        private static ArrayList dataGrid = new ArrayList();
        private bool dataInisial;
        private decimal dataAwal;
        private decimal dataAkhir;
        private decimal currentMaks;
        private decimal currentMin;
        private bool loadMore = true;
        private Thread myThread;

        private char modeCrud;

        private SvcFasAngkSelect.call_pttClient fetchData;
        private SvcFasAngkSelect.InputParameters parInp;
        public SvcFasAngkSelect.OutputParameters outDat;
        public SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG selectedData;
        GridRow grow;
        private bool isSearch = false;
        private bool initiated = false;
        //private FrmKoorSatker gpForm;
        public ucAngkutanForm frmMaster;
        public FormFasAngk formFasAngk;

        public ArrayList dataNamaFasilitas;

        public decimal? ID_KANGK;
        public string Status;
        public UcNmFasAngk(decimal? _ID_KANGK, string _status, ucAngkutanForm _frmMaster)
        {
            //this.gpForm = _gpForm;
            this.frmMaster = _frmMaster;
            this.ID_KANGK = _ID_KANGK;
            this.Status = _status;
            dataNamaFasilitas = new ArrayList();
        }

        public void initGrid()
        {
            this.dataInisial = true;
            this.dataAwal = 1;
            this.dataAkhir = 15;
            this.currentMaks = 15;
            this.currentMin = 1;
            this.loadMore = true;

        }

        private void UcNmFasAngk_Load(object sender, EventArgs e)
        {
            this.binder = new BindingSource();
            this.binder.DataSource = typeof(SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG);
            this.GcUcDtl.DataSource = binder;
        }

        public void getInitFasAngk(string where = "")
        {
            myThread = new Thread(new ThreadStart(this.ShowProgresBar));
            myThread.Start();
            SvcFasAngkSelect.InputParameters parInp = new SvcFasAngkSelect.InputParameters();
            parInp.P_COL = "";
            if (this.dataInisial == true)
            {
                konfigApp.currentMaks = konfigApp.dataAkhir;
                konfigApp.currentMin = konfigApp.dataAwal;
            }
            else
            {
                konfigApp.currentMin = konfigApp.currentMaks + 1;
                konfigApp.currentMaks = konfigApp.currentMaks + konfigApp.dataAkhir;
            }
            //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);

            parInp.P_MAX = konfigApp.currentMaks;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = konfigApp.currentMin;
            parInp.P_MINSpecified = true;
            parInp.STR_WHERE = where;
            parInp.P_SORT = "DESC";

            fetchData = new SvcFasAngkSelect.call_pttClient(konfigApp.SvcFasAngkSelect_name,konfigApp.SvcFasAngkSelect_address);
            fetchData.Open();
            fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
        }

        protected void getResult(IAsyncResult result)
        {
            try
            {
                this.outDat = fetchData.Endexecute(result);
                fetchData.Close();
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                this.Invoke(new ShowData(this.showData), this.outDat);
            }
            catch
            {
                this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
                this.Invoke(new AktifkanForm(this.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
                
            }
        }


        private delegate void ShowData(SvcFasAngkSelect.OutputParameters dataOut);

        public void showData(SvcFasAngkSelect.OutputParameters serviceOutPut)
        {
            int jmlDataGroup = serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG.Count();
            if (binder == null)
            {
                binder = new BindingSource();
           
                binder.DataSource = typeof(SvcFasAngkSelect.BPSIMANSROW_KANGK_FAS_PENUNJANG);
                //GcUcDtl.DataSource = "";

            }


            for (int i = 0; i < jmlDataGroup; i++)
            {

                dataNamaFasilitas.Add(serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG[i].NM_FASILITAS);
                frmMaster.identitas.teperlengkapan.Items.Add(serviceOutPut.SF_ROW_KANGK_FAS_PENUNJANG[i].NM_FASILITAS);   
            }

            if (jmlDataGroup < 5)
            {
                this.loadMore = false;
               
                if (isSearch)
                {
                    //this.bbSearch.Enabled = false; 
                }
            }
            else
            {
                this.loadMore = true;
            }

        }
        
    }
}
