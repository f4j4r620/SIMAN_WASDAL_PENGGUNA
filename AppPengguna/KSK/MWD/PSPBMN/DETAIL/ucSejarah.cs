using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KSK.MWD.PSPBMN.DETAIL
{
  

    public partial class ucSejarah : DevExpress.XtraEditors.XtraUserControl
    {
        public CariDataOnline cariDataOnline;

        SvcMonSjrhSelect.execute_pttClient svcSejarahSelect = null;
        SvcMonSjrhSelect.OutputParameters outSejarahSelect = null;
        SvcMonSjrhSelect.WASDALSROW_MON_BMN_PSP dataTerpilih = null;

        public int posisiRow = 0;
        GridView rowTerpilih = null;
        public bool dataInisial = true;
        public ArrayList dsGrid = null;
        public bool rowTerakhir = false;
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        public bool initModeLoad = true;
        public string strCari= "";
        public bool masihAdaData;
        public bool adaData;
        public string kdPelayanan = "";
        public decimal currentMin = konfigApp.currentMin;
        public decimal currentMaks = konfigApp.currentMaks;

        ucMonLAIN PSPBMN = null;
        FrmKoorSatker frmKpknl = null;

        public ucSejarah(FrmKoorSatker _frmKpknl, ucMonLAIN _ucPspBmn)
        {
            InitializeComponent();
            this.frmKpknl = _frmKpknl;
            PSPBMN = _ucPspBmn;
        }

        #region LOAD DATA//================================

        public void getSejarah()
        {
            frmKpknl.Enabled = false;
            try
            {
                frmKpknl.fToggleProgressBar("start");

                SvcMonSjrhSelect.InputParameters parInp = new SvcMonSjrhSelect.InputParameters();

                if (dataInisial == true)
                {
                    this.currentMaks = konfigApp.dataAkhir;
                    this.currentMin = konfigApp.dataAwal;
                }
                else
                {
                    this.currentMin = this.currentMaks + 1;
                    this.currentMaks = this.currentMaks + konfigApp.dataAkhir;
                }

                parInp.P_MAX = this.currentMaks;
                parInp.P_MAXSpecified = true;
                parInp.P_MIN = this.currentMin;
                parInp.P_MINSpecified = true;
                parInp.STR_WHERE = String.Format("ID_SATKER={0} OR (TIPE_PEMOHON='SATKER' AND ID_PEMOHON={1})", konfigApp.idSatker, konfigApp.idSatker) + " AND NO_SURAT IS NOT NULL" + PSPBMN.strKdPelayanan + this.strCari;
                parInp.P_COL = "KD_BRG,NUP";
                parInp.P_SORT = "ASC";
                parInp.P_COUNT = "y";
                svcSejarahSelect = new SvcMonSjrhSelect.execute_pttClient();
                svcSejarahSelect.Beginexecute(parInp, new AsyncCallback(getDataSejarah), null);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private void getDataSejarah(IAsyncResult result)
        {
            try
            {
                outSejarahSelect = svcSejarahSelect.Endexecute(result);
                svcSejarahSelect.Close();
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                this.Invoke(new LoadDataSejarah(this.loadDataSejarah), outSejarahSelect);
            }
            catch
            {
                frmKpknl.fToggleProgressBar("finish");
                this.Invoke(new AktifkanForm(frmKpknl.aktifkanForm), "");
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }
        }

        private delegate void LoadDataSejarah(SvcMonSjrhSelect.OutputParameters dataOut);

        private void loadDataSejarah(SvcMonSjrhSelect.OutputParameters dataOut)
        {
            int jmlData = dataOut.SF_MON_SEJARAH_PSP.Count();
            if (jmlData == konfigApp.dataAkhir)
            {
                this.masihAdaData = true;
                frmKpknl.bbiMWasdalMore.Enabled = true;
                PSPBMN.sbCari.Enabled = true;
            }
            else
            {
                if (this.modeLoadData == "normal")
                {
                    this.masihAdaData = false;
                    frmKpknl.bbiMWasdalMore.Enabled = false;
                    PSPBMN.sbCari.Enabled = true;
                }
                else if (this.modeLoadData == "cari")
                {
                    this.masihAdaData = true;
                    PSPBMN.sbCari.Enabled = false;
                }
            }
            if (dataInisial == true)
            {
                dsGrid = new ArrayList();
            }
            for (int i = 0; i < jmlData; i++)
            {
                dataOut.SF_MON_SEJARAH_PSP[i].TGL_SK = konfigApp.setDefaultDate(dataOut.SF_MON_SEJARAH_PSP[i].TGL_SK);
               
                dsGrid.Add(  dataOut.SF_MON_SEJARAH_PSP[i]);
                this.adaData = true;
            }
            displayData();
        
        }
        #endregion

        public void displayData()
        {

            if (dataInisial == true)
            {
                gcSejarah.DataSource = null;
                gcSejarah.DataSource = dsGrid;
            }
            else
            {
                gcSejarah.RefreshDataSource();
            }
        }

        private void gvSejarah_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                rowTerpilih = sender as GridView;
                if (rowTerpilih.SelectedRowsCount > 0)
                {
                    dataTerpilih = (SvcMonSjrhSelect.WASDALSROW_MON_BMN_PSP)rowTerpilih.GetRow(e.FocusedRowHandle);
                    posisiRow = gvSejarah.GetFocusedDataSourceRowIndex();

                    if (rowTerpilih.IsLastRow)
                    {
                        rowTerakhir = true;
                    }
                    else
                    {
                        rowTerakhir = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void gvSejarah_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    frmTL frmTL = new frmTL(PSPBMN);
            //    frmTL.idAset = dataTerpilih.ID_ASET.ToString();
            //    frmTL.skKeputusan = dataTerpilih.NO_SURAT;
            //    frmTL.teKdBrg.Text = dataTerpilih.KD_BRG;
            //    frmTL.teNoAset.Text = dataTerpilih.NUP.ToString();
            //    frmTL.teUrBrg.Text = dataTerpilih.UR_SSKEL;
            //    frmTL.teKuantitas.EditValue = dataTerpilih.KUANTITAS;
            //    frmTL.teNilaiPersetujuan.EditValue = dataTerpilih.NILAI_PENETAPAN;
            //    frmTL.kdStatus = (dataTerpilih.KD_STATUS == "") ? "02" : dataTerpilih.KD_STATUS;
            //    frmTL.gunaWasdal = dataTerpilih.GUNA_WASDAL;
            //    frmTL.ShowDialog();
            //}
            //catch
            //{
            //}
        }




    }
}
