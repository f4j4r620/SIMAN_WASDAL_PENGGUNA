using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace AppPengguna.AST.PRSDN
{
  class ucMonitoring2 : UserControlDetail
  {
    private SvcPersediaanSelect.call_pttClient fetchData;
    public SvcPersediaanSelect.OutputParameters outDat;
    public SvcPersediaanSelect.BPSIMANSROW_SEDIA selectedData;
    //public NonAktifkanFormSatker nonAktifForm;
    //public AktifkanFormSatker aktifkanForm;
    //public showProgresBar ShowProgresBar;
    //public closeProgresBar CloseProgresBar;
    SvcPersediaanSelect.BPSIMANSROW_SEDIA newRow;
    private SvcPersediaanCrud.call_pttClient svcJlnJmbtnCrud;
    private SvcPersediaanCrud.OutputParameters outDataCrud;
    private string strCari = "";
    private string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
    private ColumnView View;
    private bool FROM_TRACKING;
    public SetPanel setPanel;

    private decimal? ID_ASET;



    private string coba;


    public ucMonitoring2(bool tracking = false)
    {
      FROM_TRACKING = tracking;
      this.binder = new BindingSource();
      this.binder.DataSource = typeof(SvcPersediaanSelect.BPSIMANSROW_SEDIA);
      this.gcUcDtl.DataSource = binder;
      this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
      FROM_TRACKING = tracking;
      this.binder = new BindingSource();
      this.binder.DataSource = typeof(SvcPersediaanSelect.BPSIMANSROW_SEDIA);
      this.gcUcDtl.DataSource = binder;
      this.gvUcDtl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
      this.setKolom("No", "NUM", "NUM", 0, false, 80);
      this.setKolom("Tahun", "THN_ANG", "THN_ANG", 1, true, 80);
      this.setKolom("Periode", "PERIODE", "PERIODE", 2, true, 100);
      this.setKolom("Kode Satker", "KD_SATKER", "KD_SATKER", 3, true, 140);

      this.setKolom("Nama Satker", "UR_SATKER", "UR_SATKER", 4, true, 140);
      this.setKolom("Kode Barang", "KD_BRG", "KD_BRG", 5, true, 140);
      this.setKolom("Nama Barang", "UR_SSKEL", "UR_SSKEL", 6, true, 140);
      this.setKolom("Kuantitas", "KUANTITAS", "KUANTITAS", 7, true, 100);
      this.setKolom("Nilai", "NILAI", "NILAI", 8, true, 100);
      this.setKolom("Kode KPKNL", "KDKPKNL", "KDKPKNL", 9, true, 140);
      gridDoubleClick = false;
      show_record = true;
      this.ShowFooter(true);
      this.SetSummary(6, "SUM_KUANTITAS", "Total", "T O T A L");
      this.SetSummary(7, "SUM_KUANTITAS", "SumTotal");
      this.SetSummary(8, "SUM_NILAI", "SumTotal");
    }

    protected override void ucDetail_Load(object sender, EventArgs e)
    {

      this.bbEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
      this.bbHapus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
      this.bbTambah.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
      this.bbMore.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
      this.bbRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


      this.gvUcDtl.DoubleClick += new System.EventHandler(this.gvUcDtl_DoubleClick);
      //this.initGrid();
      //this.getinitMonitoring();


    }


    public void hapusData(SvcPersediaanSelect.BPSIMANSROW_SEDIA selectedData)
    {

      try
      {
        this.nonAktifForm("");
        //MessageBox.Show(selectedData.ID_ASET.ToString(),selectedData.ID_KJALJ.ToString());
        //  myThread = new Thread(new ThreadStart(ShowProgresBar),"");
        // myThread.Start();
        SvcPersediaanCrud.InputParameters parInp = new SvcPersediaanCrud.InputParameters();
        parInp.P_SELECT = "D";
        parInp.P_ID_ASET = selectedData.ID_SEDIA;
        parInp.P_ID_ASETSpecified = true;
        parInp.P_ID_SATKER = selectedData.ID_SATKER;
        parInp.P_ID_SATKERSpecified = true;
        this.modeCrud = Convert.ToChar(parInp.P_SELECT);
        svcJlnJmbtnCrud = new SvcPersediaanCrud.call_pttClient();
        svcJlnJmbtnCrud.Open();
        svcJlnJmbtnCrud.Beginexecute(parInp, new AsyncCallback(getResultCrud), "");
      }
      catch
      {
        this.modeCrud = 'A';
        this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
        MessageBox.Show(konfigApp.teksGagalHapus, konfigApp.judulGagalHapus);
      }
    }

    private void getResultCrud(IAsyncResult result)
    {
      try
      {
        outDataCrud = svcJlnJmbtnCrud.Endexecute(result);
        svcJlnJmbtnCrud.Close();
        this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
        this.msg_get_database(outDataCrud.PO_RESULT, "Y", 0);
        if (String.Compare(outDataCrud.PO_RESULT.Trim(), "Y", true) == 0)
        {
          this.Invoke(new UbahJalanJembatan(this.ubahJalanJembatan), outDataCrud);
        }
      }
      catch
      {
        this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
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

    private delegate void UbahJalanJembatan(SvcPersediaanCrud.OutputParameters dataOutCrud);

    private void ubahJalanJembatan(SvcPersediaanCrud.OutputParameters dataOutCrud)
    {
      SvcPersediaanSelect.BPSIMANSROW_SEDIA dataPenyama = new SvcPersediaanSelect.BPSIMANSROW_SEDIA();
      dataPenyama.ID_SEDIA = dataOutCrud.PO_ID_SEDIA;
      dataPenyama.ID_SATKER = dataOutCrud.PO_ID_ASET;

      dataPenyama.NUM = 99;
      dataPenyama.NUMSpecified = true;
      switch (this.modeCrud)
      {
        case 'C':
          this.binder.Add(dataPenyama);
          break;
        case 'U':
          this.binder.Remove(this.selectedData);
          this.binder.Add(dataPenyama);
          this.dataInisial = true;
          this.initGrid();
          break;
        case 'D':
          this.binder.Remove(this.selectedData);
          break;
      }
    }

    protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
    {
      selectedView = sender as GridView;
      newRow = (SvcPersediaanSelect.BPSIMANSROW_SEDIA)selectedView.GetRow(e.RowHandle);

    }

    protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      //
    }

    protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }

    protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.initGrid();
      this.getinitMonitoring();
    }

    protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.initialData = false;
      this.dataInisial = false;
      this.getinitMonitoring();
    }

    protected override void bbSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

      /// TES


      //-----------------------
      try
      {
        string nama_kolom = this.LuKolom.EditValue.ToString();
        string opr = this.barOperator.EditValue.ToString().ToUpper();
        string parameter = this.teSearch.EditValue.ToString().ToUpper();
        string parameter_2 = "";
        if (opr == "ANTARA")
        {
          parameter_2 = this.teSearch2.EditValue.ToString().ToUpper();
        }
        this.dataInisial = true;
        this.getinitMonitoring(get_where_clause(nama_kolom, opr, parameter, parameter_2));

      }
      catch (Exception)
      {

        MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
        this.aktifkanForm("");
      }

    }

    private string get_where_clause(string nama_kolom, string opr, string parameter, string parameter2)
    {
      string where = "";

      switch (nama_kolom)
      {
        case "Tahun":
          where = "Upper(THN_ANG) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Periode":
          where = "Upper(PERIODE) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Kode Satker":
          where = "Upper(KD_SATKER) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Nama Satker":
          where = "Upper(UR_SATKER) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Kode Barang":
          where = "Upper(KD_BRG) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Nama Barang":
          where = "Upper(UR_SSKEL) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Kuantitas":
          where = "Upper(KUANTITAS) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Nilai":
          where = "Upper(NILAI) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Kode KPKNL":
          where = "Upper(KDKPKNL) " + get_operator("String", opr, parameter, parameter2);
          break;
        default:
          break;
      }
      return where;
    }

    protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
    {


    }


    protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }


    protected override void gvUcDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {
      selectedView = sender as GridView;
      if (selectedView.SelectedRowsCount > 0)
      {
        selectedData = (SvcPersediaanSelect.BPSIMANSROW_SEDIA)selectedView.GetRow(e.FocusedRowHandle);
        if (selectedView.IsLastRow)
        {
          LastRow = true;
        }
        else
        {
          LastRow = false;
        }
        posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
      }
    }

    protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
    {
      string nama_kolom = this.LuKolom.EditValue.ToString();
      this.LuKolom.Width = 40 + (((int)nama_kolom.Length) * 5);
      switch (nama_kolom)
      {

        default:
          this.teSearch.Edit = this.ItemText;
          this.teSearch2.Edit = this.ItemText;
          break;
      }
    }

    public string BySatker;
    public void getinitMonitoring(string strwhere = null)
    {
      this.nonAktifForm("");
      myThread = new Thread(new ThreadStart(this.ShowProgresBar));
      myThread.Start();
      SvcPersediaanSelect.InputParameters parInp = new SvcPersediaanSelect.InputParameters();

      string where = "";
      if (FROM_TRACKING == false)
      {
        where = BySatker;
        if (strwhere != null)
        {
          where = BySatker + " AND " + strwhere;
        }
        else
        {
          where = BySatker;// BySatker;
        }
      }
      else
      {
        if (strwhere != null)
        {
          where = strwhere;
        }
        else
        {
          where = "";
        }
      }
      if (this.dataInisial == true)
      {
        this.currentMaks = this.dataAkhir;
        this.currentMin = this.dataAwal;
      }
      else
      {
        this.currentMin = this.currentMaks + 1;
        this.currentMaks = this.currentMaks + this.dataAkhir;
        dataInisial = false;
      }
      //System.Diagnostics.Debug.WriteLine("hahahah", this.posisiRow);
      if (this.dataInisial == true)
      {
        this.search = " ID_SATKER = " + Convert.ToString(konfigApp.idSatker) + " AND KD_BRG LIKE '10105%'";
      }
      parInp.P_MAX = this.currentMaks;
      parInp.P_MAXSpecified = true;
      parInp.P_MIN = this.currentMin;
      parInp.P_MINSpecified = true;
      parInp.P_COL = "";
      parInp.P_SORT = "";
      parInp.STR_WHERE = this.search;
      fetchData = new SvcPersediaanSelect.call_pttClient();
      fetchData.Open();
      fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
    }

    protected void getResult(IAsyncResult result)
    {
      try
      {
        this.outDat = fetchData.Endexecute(result);
        fetchData.Close();
        this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
        //if(!FROM_TRACKING)
        this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
        this.Invoke(new ShowData(this.showData), this.outDat);
      }
      catch (Exception E)
      {
        //if (!FROM_TRACKING)
        this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
        this.Invoke(new closeProgresBar(this.CloseProgresBar), DevExpress.XtraBars.BarItemVisibility.Never);
        //MessageBox.Show(E.ToString(), konfigApp.judulGagalAmbil);
        MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
      }
    }

    private delegate void ShowData(SvcPersediaanSelect.OutputParameters dataOut);

    //ucMonitoring2Form JalanJembatanForm;


    public void showData(SvcPersediaanSelect.OutputParameters serviceOutPut)
    {
      if (!FROM_TRACKING)
      {
        int jmlData = serviceOutPut.SF_ROW_MSEDIA.Count();

        if (this.dataInisial == true)
        {
          this.binder.Clear();
          if (jmlData > 0)
          {
            StrTotalGrid.Caption = jmlData.ToString();
            StrTotalDb.Caption = serviceOutPut.SF_ROW_MSEDIA[0].TOTAL_DATA.ToString();
          }
          else
          {
            StrTotalGrid.Caption = "0";
            StrTotalDb.Caption = "0";
          }
        }
        else
        {
          if (jmlData > 0)
          {

            StrTotalGrid.Caption = (Convert.ToInt64(StrTotalGrid.Caption) + jmlData).ToString();
            StrTotalDb.Caption = serviceOutPut.SF_ROW_MSEDIA[0].TOTAL_DATA.ToString();
          }
        }
        if (jmlData > 0)
        {
          this.SUM_KUANTITAS = serviceOutPut.SF_ROW_MSEDIA[0].SUM_KUANTITAS;
          this.SUM_NILAI = serviceOutPut.SF_ROW_MSEDIA[0].SUM_NILAI;

        }

        for (int i = 0; i < jmlData; i++)
        {
          string date = Convert.ToString(serviceOutPut.SF_ROW_MSEDIA[i].TGL_BUKU).Substring(0, 10);
          string date_ = Convert.ToString(serviceOutPut.SF_ROW_MSEDIA[i].TGL_BUKU).Substring(0, 8);


          if (date == "11/11/1000" || date_ == "1/1/0001")
          {
            serviceOutPut.SF_ROW_MSEDIA[i].TGL_BUKU = null;
          }

          this.binder.Add(serviceOutPut.SF_ROW_MSEDIA[i]);
        }



        if (jmlData < konfigApp.dataAkhir)
        {
          this.loadMore = false;
          this.bbMore.Enabled = false;
        }
        else
        {
          this.loadMore = true;
          this.bbMore.Enabled = true;
        }

      }
      else
      {
        int jmlData = serviceOutPut.SF_ROW_MSEDIA.Count();

        if (jmlData > 0)
        {
          //JalanJembatanForm = new ucMonitoring2Form("detail");
          //JalanJembatanForm.nonAktifForm = new NonAktifkanFormSatker(this.nonAktifForm);
          //JalanJembatanForm.aktifkanForm = new AktifkanFormSatker(this.aktifkanForm);

          //JalanJembatanForm.ShowProgresBar = new showProgresBar(this.ShowProgresBar);
          //JalanJembatanForm.CloseProgresBar = new closeProgresBar(this.CloseProgresBar);
          //JalanJembatanForm.Dock = System.Windows.Forms.DockStyle.Fill;
          //JalanJembatanForm.IsiForm(serviceOutPut.SF_ROW_MSEDIA[0]);
        }
        else
        {
          MessageBox.Show("Data aset tidak ditemukan", "Perhatian");
        }
        //setPanel(JalanJembatanForm);

        //this.frmKoorSatker.panelKoorSatker.Controls.Clear();
        //this.frmKoorSatker.panelKoorSatker.Controls.Add(JalanJembatanForm);
      }

      this.gvUcDtl.BestFitColumns();
    }


  }
}
