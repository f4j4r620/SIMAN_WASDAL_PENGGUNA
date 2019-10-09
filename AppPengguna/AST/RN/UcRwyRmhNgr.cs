using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Threading;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;

namespace AppPengguna.AST.RN
{
  class UcRwyRmhNgr : UserControlDetail
  {

    private SvcRwyRmhNgrSelect.call_pttClient fetchData;
    private SvcRwyRmhNgrSelect.InputParameters parInp;
    private SvcRwyRmhNgrSelect.OutputParameters outData;
    public SvcRwyRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_RMH selectedData;

    private String Status;
    public decimal? IdRmhNgr;

    GridView viewTerpilih = null;
    public bool rowTerakhir = false;

    private FrmRwyRmhNgr FrmRwyRmhNgr;

    //------------- GET DOKUMEN -------------------------------

    SvcAsetGetDokSelect.call_pttClient svcAsetGetDokSelect;
    SvcAsetGetDokSelect.OutputParameters outFileDok;
    //-------------------------------------------------------

    public UcRwyRmhNgr(decimal? _ID_KRMH_NEG, String status)
      : base()
    {
      this.InitRwyRmhNgr();
      this.Status = Status;
      this.IdRmhNgr = _ID_KRMH_NEG;

      if (Status == "detail")
      {
        this.bbTambah.Enabled = true;
        this.bbHapus.Enabled = true;
        this.bbEdit.Caption = "Detail";
        this.bbEdit.Glyph = global::AppPengguna.Properties.Resources.tombol_detail16;
      }
      else
      {
        this.bbTambah.Enabled = true;
        this.bbHapus.Enabled = true;
      }
      this.bbEdit.Enabled = true;
      this.bbRefresh.Enabled = true;
      this.bbMore.Enabled = true;
      this.btnMap.Enabled = false;
    }

    protected override void ucDetail_Load(object sender, EventArgs e)
    {

      this.initGrid();
      this.getInitRwyRmhNgr();
      gvUcDtl.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
    }

    public void InitRwyRmhNgr()
    {
      this.binder = new BindingSource();
      this.binder.DataSource = typeof(AppPengguna.SvcRwyRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_RMH);
      this.gcUcDtl.DataSource = binder;

      this.setKolom("No", "NUM", "NUM", 0, false);
      this.setKolom("Riwayat Bangunan", "RWYT_RMH", "RWYT_RMH", 1, true);
      this.setKolom("Nomor Kontrak", "NO_KONTRAK", "NO_KONTRAK", 2, true);
      this.setKolom("Tgl Kontrak", "TGL_KONTRAK", "TGL_KONTRAK", 3, true, 120, "date");
      this.setKolom("Nama Kontraktor", "NM_KONTRAKTOR", "NM_KONTRAKTOR", 4, true);
      this.setKolom("Nilai Kontrak", "NILAI_KONTRAK", "NILAI_KONTRAK", 5, true, 120, "number");
      this.setKolom("NPWP Kontraktor", "NPWP_KONTRAKTOR", "NPWP_KONTRAKTOR", 6, true);
      this.setKolom("Alamat Kontraktor", "ALAMAT_KONTRAKTOR", "ALAMAT_KONTRAKTOR", 7, true);
      this.setKolom("Tgl Mulai", "TGL_MULAI", "TGL_MULAI", 8, true, 120, "date");
      this.setKolom("Tgl Selesai", "TGL_SELESAI", "TGL_SELESAI", 9, true, 120, "date");
      this.setKolom("Tgl Digunakan", "TGL_PAKAI", "TGL_PAKAI", 10, true, 120, "date");
      this.setKolom("File (Dok/Foto)", "NMFILE", "NMFILE", 11, true);

      //this.SetGridSize(width, 0);
      gridDoubleClickDetail = true;

      this.btnMap.Caption = "Lihat Dokumen";
      this.btnMap.Glyph = global::AppPengguna.Properties.Resources.textbox_16x16;

      this.btnMap.Name = "btnMap";
      this.btnMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
      this.btnMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.view_dokumen);
      this.show_record = true;
    }


    #region View Dokumen
    protected void view_dokumen(object sender, ItemClickEventArgs e)
    {
      try
      {
        myThread = new Thread(new ThreadStart(this.ShowProgresBar));
        myThread.Start();
        SvcAsetGetDokSelect.InputParameters parInp = new SvcAsetGetDokSelect.InputParameters();
        parInp.P_ID = selectedData.ID_KRMH_RWYT_RMH;
        parInp.P_ID_TABLE = "ID_KRMH_RWYT_RMH";
        parInp.P_IDSpecified = true;
        parInp.P_TABLE = "M_KRMH_NEG_RWYT_RMH";
        svcAsetGetDokSelect = new SvcAsetGetDokSelect.call_pttClient();
        svcAsetGetDokSelect.Open();
        svcAsetGetDokSelect.Beginexecute(parInp, new AsyncCallback(this.getResultDok), null);
      }
      catch (Exception E)
      {
        this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
        //MessageBox.Show(E.Message);
        MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
      }
    }

    private void getResultDok(IAsyncResult result)
    {
      try
      {
        this.outFileDok = svcAsetGetDokSelect.Endexecute(result);
        svcAsetGetDokSelect.Close();
        this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
        this.Invoke(new ShowFileDok(this.showFileDok), this.outFileDok);
      }
      catch (Exception E)
      {
        this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
        //MessageBox.Show(E.Message);
        MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
      }
    }

    private delegate void ShowFileDok(SvcAsetGetDokSelect.OutputParameters dataOut);

    public void showFileDok(SvcAsetGetDokSelect.OutputParameters serviceOutPut)
    {
      int jmlDataGroup = serviceOutPut.SF_ROW_GET_ISI_DOK.Count();

      if (jmlDataGroup > 0)
      {
        SvcAsetGetDokSelect.BPSIMANSROW_GET_ISI_DOK dok = serviceOutPut.SF_ROW_GET_ISI_DOK[0];
        System.IO.File.WriteAllBytes(selectedData.NMFILE, dok.ISI_FILE);
        AppPengguna.PU.FrmPuViewPdf PuPdf = new AppPengguna.PU.FrmPuViewPdf();
        PuPdf.display(selectedData.NMFILE);
        PuPdf.ShowDialog();
      }
    }


    #endregion//ViewDokumen



    #region load data
    public void getInitRwyRmhNgr(string _where = null)
    {
      myThread = new Thread(new ThreadStart(this.ShowProgresBar));
      myThread.Start();
      this.nonAktifForm("");
      parInp = new SvcRwyRmhNgrSelect.InputParameters();
      parInp.P_COL = "";
      decimal Max, Min;
      if (this.dataInisial == true)
      {
        this.currentMaks = this.dataAkhir;
        this.currentMin = this.dataAwal;
        Max = this.currentMaks;
        Min = this.currentMin;
      }
      else
      {
        if (getById == true)
        {
          Max = 1;
          Min = 0;
        }
        else
        {
          this.currentMin = this.currentMaks + 1;
          this.currentMaks = this.currentMaks + this.dataAkhir;
          Max = this.currentMaks;
          Min = this.currentMin;
        }
      }
      if (this.dataInisial == true)
      {
        this.search = (_where != null) ? " AND " + _where : "";
        _where = this.search;
      }
      else if (getById == true)
      {
        _where = (_where != null) ? " AND " + _where : "";
      }
      else
      {
        _where = this.search;
      }
      parInp.P_MAX = Max;
      parInp.P_MAXSpecified = true;
      parInp.P_MIN = Min;
      parInp.P_MINSpecified = true;
      parInp.P_SORT = "DESC";
      parInp.STR_WHERE = String.Format(" ID_KRMH_NEG = {0} {1}", this.IdRmhNgr, _where);
      parInp.P_COUNT = "Y";
      Console.WriteLine(parInp.STR_WHERE);
      fetchData = new SvcRwyRmhNgrSelect.call_pttClient();
      fetchData.Open();
      fetchData.Beginexecute(parInp, new AsyncCallback(this.getResult), null);
    }

    protected void getResult(IAsyncResult result)
    {
      try
      {
        this.outData = fetchData.Endexecute(result);
        fetchData.Close();
        this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
        this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
        this.Invoke(new ShowData(this.showData), this.outData);
      }
      catch (Exception ex)
      {
        this.Invoke(new AktifkanFormSatker(this.aktifkanForm), "");
        this.Invoke(new ProgBar(this.CloseProgresBar), BarItemVisibility.Never);
        MessageBox.Show("Load data riwayat gagal", konfigApp.judulGagalAmbil);
      }
    }

    private delegate void ShowData(SvcRwyRmhNgrSelect.OutputParameters dataOut);
    public decimal? NUM;
    private string StatusCrud = "";
    public void showData(SvcRwyRmhNgrSelect.OutputParameters serviceOutPut)
    {
      int jmlDataGroup = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH.Count();
      if (this.getById == true && jmlDataGroup > 0)
      {
        if (this.StatusCrud == "edit")
        {
          serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[0].NUM = this.NUM;
        }
        else
        {
          serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[0].NUM = binder.Count + 1;
        }
      }
      if (this.dataInisial == true)
      {
        this.binder.Clear();
        if (jmlDataGroup > 0)
        {
          StrTotalGrid.Caption = jmlDataGroup.ToString();
          //serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[0].TOTAL_DATASpecified = true;
          StrTotalDb.Caption = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[0].TOTAL_DATA.ToString();
        }
        else
        {
          StrTotalGrid.Caption = "0";
          StrTotalDb.Caption = "0";
        }
      }
      else
      {
        if (jmlDataGroup > 0 && this.StatusCrud != "edit")
        {

          StrTotalGrid.Caption = (Convert.ToInt64(StrTotalGrid.Caption) + jmlDataGroup).ToString();
          if (this.StatusCrud != "input")
          {
            StrTotalDb.Caption = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[0].TOTAL_DATA.ToString();
          }
          else
          {
            StrTotalDb.Caption = (Convert.ToInt64(StrTotalDb.Caption) + 1).ToString();
          }
        }
      }


      for (int i = 0; i < jmlDataGroup; i++)
      {
        string date = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_KONTRAK).Substring(0, 10);
        string date_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_KONTRAK).Substring(0, 8);
        string date2 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_MULAI).Substring(0, 10);
        string date2_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_MULAI).Substring(0, 8);
        string date3 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_PAKAI).Substring(0, 10);
        string date3_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_PAKAI).Substring(0, 8);
        string date4 = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_SELESAI).Substring(0, 10);
        string date4_ = Convert.ToString(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_SELESAI).Substring(0, 8);

        if (date == "11/11/1000" || date_ == "1/1/0001")
        {
          serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_KONTRAK = null;
        }
        if (date2 == "11/11/1000" || date2_ == "1/1/0001")
        {
          serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_MULAI = null;
        }
        if (date3 == "11/11/1000" || date3_ == "1/1/0001")
        {
          serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_PAKAI = null;
        }
        if (date4 == "11/11/1000" || date4_ == "1/1/0001")
        {
          serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i].TGL_SELESAI = null;
        }

        binder.Add(serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH[i]);
      }

      if (this.getById == true)
      {
        this.getById = false;
      }
      else
      {
        if (jmlDataGroup < konfigApp.dataAkhir)
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
      StatusCrud = "";
      this.gvUcDtl.RefreshData();
      this.gvUcDtl.BestFitColumns();
    }

    #endregion



    #region button
    protected override void bbTambah_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.FrmRwyRmhNgr = new FrmRwyRmhNgr(this, "input");
      StatusCrud = "input";
      this.FrmRwyRmhNgr.ShowDialog();
    }
    protected override void bbEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      if (this.selectedData == null)
      {
        return;
      }
      this.NUM = selectedData.NUM;
      if (this.Status == "detail")
      {
        this.FrmRwyRmhNgr = new FrmRwyRmhNgr(this, "detail");
        StatusCrud = "detail";
      }
      else
      {
        this.FrmRwyRmhNgr = new FrmRwyRmhNgr(this, "edit");
        StatusCrud = "edit";
      }
      this.FrmRwyRmhNgr.ShowDialog();
    }
    protected override void bbHapus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      if (this.selectedData == null)
      {
        return;
      }
      StatusCrud = "hapus";
      this.FrmRwyRmhNgr = new FrmRwyRmhNgr(this, "hapus");
      this.FrmRwyRmhNgr.Size = new System.Drawing.Size(0, 0);
      this.FrmRwyRmhNgr.Opacity = 0;
      this.FrmRwyRmhNgr.Show();
      this.FrmRwyRmhNgr.Hide();
      this.FrmRwyRmhNgr.hapusData();

    }
    protected override void bbMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.dataInisial = false;
      this.getInitRwyRmhNgr();
    }
    protected override void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.search = "";
      this.pencarian = false;
      this.initGrid();
      this.getInitRwyRmhNgr();
    }
    protected override void bbSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
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
        this.pencarian = true;
        this.getInitRwyRmhNgr(get_where_clause(nama_kolom, opr, parameter, parameter_2));

      }
      catch (Exception)
      {

        MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulIsianKosong);
        this.aktifkanForm("");
      }


    }
    #endregion


    private string get_where_clause(string nama_kolom, string opr, string parameter, string parameter2)
    {
      string where = "";
      switch (nama_kolom)
      {

        case "Riwayat Bangunan":
          where = "Upper(RWYT_RMH) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Nomor Kontrak":
          where = "Upper(NO_KONTRAK) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Tgl Kontrak":
          where = "Upper(TGL_KONTRAK) " + get_operator("Date", opr, parameter, parameter2);
          break;
        case "Nilai Kontrak":
          where = "Upper(NILAI_KONTRAK) " + get_operator("Number", opr, parameter, parameter2);
          break;
        case "Nama Kontraktor":
          where = "Upper(NM_KONTRAKTOR) " + get_operator("String", opr, parameter, parameter2);
          break;
        case "NPWP Kontraktor":
          where = "NPWP_KONTRAKTOR " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Alamat Kontraktor":
          where = "ALAMAT_KONTRAKTOR " + get_operator("String", opr, parameter, parameter2);
          break;
        case "Tgl Mulai":
          where = "Upper(TGL_MULAI) " + get_operator("Date", opr, parameter, parameter2);
          break;
        case "Tgl Selesai":
          where = "Upper(TGL_SELESAI) " + get_operator("Date", opr, parameter, parameter2);
          break;
        case "Tgl Digunakan":
          where = "Upper(TGL_PAKAI) " + get_operator("Date", opr, parameter, parameter2);
          break;
        case "File (Dok)":
          where = "Upper(NMFILE) " + get_operator("String", opr, parameter, parameter2);
          break;
        default:
          break;
      }
      return where;
    }

    protected override void gvUcDtl_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
      viewTerpilih = sender as GridView;
      if (viewTerpilih.SelectedRowsCount > 0)
      {
        selectedData = (SvcRwyRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_RMH)viewTerpilih.GetRow(e.FocusedRowHandle);
        if (viewTerpilih.IsLastRow)
        {
          this.rowTerakhir = true;
        }
        else
        {
          this.rowTerakhir = false;
        }
        posisiRow = gvUcDtl.GetFocusedDataSourceRowIndex();
        if (selectedData.FILE_EXISTS != 0)
        {
          this.btnMap.Enabled = true;
        }
        else
        {
          this.btnMap.Enabled = false;
        }
      }
    }

    #region Teu Di Pake
    protected override void gvUcDtl_InitNewRow(object sender, InitNewRowEventArgs e)
    {

    }

    protected override void gvUcDtl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
    {

    }

    protected override void LuKolom_EditValueChanged(object sender, EventArgs e)
    {
      string nama_kolom = this.LuKolom.EditValue.ToString();
      this.LuKolom.Width = 40 + (((int)nama_kolom.Length) * 5);
      switch (nama_kolom)
      {
        //case "Tanggal Dokumen":
        //  this.teSearch.Edit = this.ItemDate;
        //  this.teSearch2.Edit = this.ItemDate;
        //  break;
        default:
          this.teSearch.Edit = this.ItemText;
          this.teSearch2.Edit = this.ItemText;
          break;
      }
    }
    #endregion
  }
}
