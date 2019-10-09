using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraBars;
using System.Collections;
using System.Globalization;

namespace AppPengguna.AST.RN
{
  internal partial class FrmRwyRmhNgr : Form
  {
    private UcRwyRmhNgr ucRwyRmhNgr = null;
    private string status;
    public string ID_JNSDOK;

    private SvcRwyRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_RMH selectedData;
    private SvcRwyRmhNgrCrud.InputParameters parInp;
    private SvcRwyRmhNgrCrud.OutputParameters outCrud;
    private SvcRwyRmhNgrCrud.call_pttClient crudData;

    private decimal? idRmhNgr;
    private decimal? idRmhNgrRwy;
    public decimal? NUM;

    public Thread myThread = null;
    private frmProgres progresBar = null;
    public char modeCrud = 'A';

    string FilePath;

    //public DateTime? tglDok
    //{
    //    set { teTglDok.EditValue = value; }
    //    get { return Convert.ToDateTime(teTglDok.EditValue); }
    //}

    public FrmRwyRmhNgr(UcRwyRmhNgr _ucRwyRmhNgr, string status)
    {
      InitializeComponent();
      this.ucRwyRmhNgr = _ucRwyRmhNgr;
      this.status = status;
      this.idRmhNgr = _ucRwyRmhNgr.IdRmhNgr;
      // this.idRmhNgr = _ucRwyRmhNgr.selectedData.ID_KANGK;
      this.selectedData = _ucRwyRmhNgr.selectedData;

      if (_ucRwyRmhNgr.selectedData != null)
      {
        this.NUM = _ucRwyRmhNgr.selectedData.NUM;
      }
      if (status == "edit" || status == "detail")
      {
        //this.bbiSave.Enabled = false;
        this.teJnsDok.Enabled = false;
        this.sbJnsDok.Enabled = false;

      }
      init();
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

    private void bbKibSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {      

        try
        {
          myThread = new Thread(new ThreadStart(ShowProgresBar));
          myThread.Start();

          SvcRwyRmhNgrCrud.InputParameters parInp = new SvcRwyRmhNgrCrud.InputParameters();

          parInp.P_ID_KRMH_NEG = this.idRmhNgr;
          parInp.P_ALAMAT_KONTRAKTOR = this.teAlamatKontraktor.Text;
          parInp.P_ID_KRMH_NEGSpecified = true;
          parInp.P_ID_KRMH_RWYT_RMH = this.idRmhNgrRwy;
          parInp.P_ID_KRMH_RWYT_RMHSpecified = true;

          parInp.P_NILAI_KONTRAK = (decimal?)this.teNilaiKontrak.Value;
          parInp.P_NILAI_KONTRAKSpecified = true;
          parInp.P_NM_KONTRAKTOR = this.teNamaKontraktor.Text;
          parInp.P_NO_KONTRAK = this.teNoKontrak.Text;
          parInp.P_NPWP_KONTRAKTOR = this.teNPWPKontraktor.Text;
          parInp.P_RWYT_RMH = this.teRiwayatBangunan.Text;
          parInp.P_TGL_PAKAI = this.teTglPakai.Text;
          parInp.P_TGL_KONTRAK = this.teTglKontrak.Text;
          parInp.P_TGL_MULAI = this.teTglMulaiKontrak.Text;
          parInp.P_TGL_SELESAI = this.teTglSelesai.Text;
          parInp.P_NMFILE = this.teFileName.Text;
         
          if (this.status == "input")
          {
            parInp.P_SELECT = "C";
          }
          else
          {
            parInp.P_SELECT = "U";
          }

          this.modeCrud = Convert.ToChar(parInp.P_SELECT);
          this.crudData = new SvcRwyRmhNgrCrud.call_pttClient();
          crudData.Open();
          this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibAngk), "");

        }
        catch
        {
          this.modeCrud = 'A';
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
          MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
        }
     
    }

    private void bbKibRefresh_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.getInitialDataDokKibAngk();
    }

    private void bbKibKembali_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.Close();
    }

    private void sbNamaFile_Click(object sender, EventArgs e)
    {
      try
      {
        string fileName = "";
        string filePath;
        long fileSize = 0;
        string creationTime = "";

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Dispose();
        dialog.Title = "Open PDF Files";
        dialog.Filter = "PDF Files(*.pdf)|*.pdf";
        dialog.Multiselect = false;


        if (dialog.ShowDialog() == DialogResult.OK)
        {
          filePath = dialog.FileName;
          fileName = dialog.SafeFileName;
          fileSize = new System.IO.FileInfo(dialog.FileName).Length;
          creationTime = new System.IO.FileInfo(dialog.FileName).CreationTime.ToString();

          if (fileSize < konfigApp.maksSizeFile)
          {
            this.FilePath = filePath;
            teFileName.Text = fileName;

            // teNmPath.Text = FilePath;
          }
          else
          {
            MessageBox.Show(konfigApp.konfirmasiMaksimalFile, konfigApp.judulGagalLain);
          }


          Console.WriteLine(fileSize + creationTime);

        }
      }
      catch
      {
        System.Console.WriteLine("gagal");
      }
    }

    private SvcRwyRmhNgrSelect.call_pttClient svcCaller;
    private SvcRwyRmhNgrSelect.InputParameters inputPar;
    private SvcRwyRmhNgrSelect.OutputParameters outputPar;
    private SvcRwyRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_RMH rowData;

    protected ArrayList dataGrid;
    protected bool dataInisial;

    public void getInitialDataDokKibAngk()
    {
      try
      {
        myThread = new Thread(new ThreadStart(this.ShowProgresBar));
        myThread.Start();
        inputPar = new SvcRwyRmhNgrSelect.InputParameters();
        inputPar.P_COL = "";
        inputPar.P_MAX = 100;
        inputPar.P_MAXSpecified = true;
        inputPar.P_MIN = 0;
        inputPar.P_MINSpecified = true;
        inputPar.P_SORT = "";
        svcCaller = new SvcRwyRmhNgrSelect.call_pttClient();
        svcCaller.Open();
        svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataDokKibAngk), null);
      }
      catch
      {

        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
        MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
      }
    }

    public void getDataDokKibAngk(IAsyncResult result)
    {
      try
      {
        this.outputPar = svcCaller.Endexecute(result);
        svcCaller.Close();
        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
        this.Invoke(new ShowDataDokKibAngk(this.showDataDokKibAngk), this.outputPar);
      }
      catch
      {
        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
        MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
      }
    }

    public void aktifkanForm(string str)
    {
      this.Enabled = true;

    }

    private delegate void ShowDataDokKibAngk(SvcRwyRmhNgrSelect.OutputParameters dataOut);

    public void showDataDokKibAngk(SvcRwyRmhNgrSelect.OutputParameters serviceOutPut)
    {
      int jmlDataGroup = serviceOutPut.SF_ROW_KRMH_NEG_RWYT_RMH.Count();
      if (this.dataInisial == true)
      {
        this.dataGrid = new ArrayList();
      }
      this.init();
    }

    public void init()
    {
      if (this.status == "input")
      {
        this.FilePath = null;
        this.teAlamatKontraktor.ResetText();
        this.teFileName.ResetText();


        this.teNamaKontraktor.ResetText();
        this.teNoKontrak.ResetText();
        this.teNilaiKontrak.ResetText();
        this.teNPWPKontraktor.ResetText();
        this.teRiwayatBangunan.ResetText();

        this.teTglKontrak.ResetText();
        this.teTglMulaiKontrak.ResetText();
        this.teTglSelesai.ResetText();
        this.teTglPakai.ResetText();
        
      }
      else if (this.status == "edit" || this.status == "detail")
      {
        try
        {
          
          this.idRmhNgrRwy = selectedData.ID_KRMH_RWYT_RMH;
          this.teAlamatKontraktor.Text = selectedData.ALAMAT_KONTRAKTOR;
          //this.teFileName.Text = selectedData.NMFILE;


          this.teNamaKontraktor.Text = selectedData.NM_KONTRAKTOR;
          this.teNoKontrak.Text = selectedData.NO_KONTRAK;
          this.teNilaiKontrak.Value = (decimal)selectedData.NILAI_KONTRAK;
          this.teNPWPKontraktor.Text = selectedData.NPWP_KONTRAKTOR;
          this.teRiwayatBangunan.Text = selectedData.RWYT_RMH;

          this.teTglKontrak.Text = konfigApp.DateToString(selectedData.TGL_KONTRAK);
          this.teTglMulaiKontrak.Text = konfigApp.DateToString(selectedData.TGL_MULAI);
          this.teTglSelesai.Text = konfigApp.DateToString(selectedData.TGL_SELESAI);
          this.teTglPakai.Text = konfigApp.DateToString(selectedData.TGL_PAKAI);
          this.teFileName.Text = selectedData.NMFILE;

        }
        catch (Exception)
        {

          MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
        }

      }
      else
      {
        this.idRmhNgrRwy = selectedData.ID_KRMH_RWYT_RMH;
      }
      this.Focus();

    }

    #region crud
    public void crudDokKibAngk(IAsyncResult result)
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

    public delegate void UbahDsDetail(SvcRwyRmhNgrCrud.OutputParameters outCrud);

    public void ubahDsDetail(SvcRwyRmhNgrCrud.OutputParameters outCrud)
    {
      SvcRwyRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_RMH dataPenyama = new SvcRwyRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_RMH();

      dataPenyama.NUM = 99;
      dataPenyama.ID_KRMH_RWYT_RMH = outCrud.PO_ID_KRMH_RWYT_RMH;
      this.idRmhNgrRwy = outCrud.PO_ID_KRMH_RWYT_RMH;

      switch (this.modeCrud)
      {
        case 'C':


          if (this.FilePath != null)
          {
            string Path = this.FilePath;

            simpanFile("ID_KRMH_RWYT_RMH", dataPenyama.ID_KRMH_RWYT_RMH, "M_KRMH_NEG_RWYT_RMH", Path, "C", this.ID_JNSDOK);

          }
          else
          {
            this.ucRwyRmhNgr.dataInisial = false;
            this.ucRwyRmhNgr.getById = true;
            this.ucRwyRmhNgr.getInitRwyRmhNgr(" ID_KRMH_RWYT_RMH = " + this.idRmhNgrRwy.ToString());
            this.init();
            this.Close();
          }
          break;
        case 'U':
          ucRwyRmhNgr.binder.Remove(this.selectedData);
          if (this.FilePath != null)
          {
            string Path = this.FilePath;
            if (this.selectedData.FILE_EXISTS != 0)
            {
              simpanFile("ID_KRMH_RWYT_RMH", dataPenyama.ID_KRMH_RWYT_RMH, "M_KRMH_NEG_RWYT_RMH", Path, "U", this.ID_JNSDOK);
            }
            else
            {
              simpanFile("ID_KRMH_RWYT_RMH", dataPenyama.ID_KRMH_RWYT_RMH, "M_KRMH_NEG_RWYT_RMH", Path, "C", this.ID_JNSDOK);
              //ucRwyRmhNgr.gvUcDtl.RefreshData();
            }
          }
          else
          {
            //this.ucRwyRmhNgr.dataInisial = false;
            //this.ucRwyRmhNgr.getById = true;
            // this.ucRwyRmhNgr.getInitDokKib(" ID_KRMH_RWYT_RMH = " + this.idRmhNgrRwy.ToString());

            ucRwyRmhNgr.gvUcDtl.RefreshData();
            this.init();
            this.Close();
            this.ucRwyRmhNgr.search = "";
            this.ucRwyRmhNgr.pencarian = false;
            this.ucRwyRmhNgr.initGrid();
            this.ucRwyRmhNgr.getInitRwyRmhNgr();
          }
          break;
        case 'D':
          ucRwyRmhNgr.binder.Remove(this.selectedData);
          ucRwyRmhNgr.gvUcDtl.RefreshData();
          this.ucRwyRmhNgr.search = "";
            this.ucRwyRmhNgr.pencarian = false;
            this.ucRwyRmhNgr.initGrid();
            this.ucRwyRmhNgr.getInitRwyRmhNgr();
          ucRwyRmhNgr.StrTotalGrid.Caption = (Convert.ToInt64(ucRwyRmhNgr.StrTotalGrid.Caption) - 1).ToString();
          ucRwyRmhNgr.StrTotalDb.Caption = (Convert.ToInt64(ucRwyRmhNgr.StrTotalDb.Caption) - 1).ToString();
          this.init();
          this.Close();
          
          break;
      }
    }

    SvcAsetDokCrud.call_pttClient svcDokCrud;
    SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud;
    public void simpanFile(string ID_TYPE, decimal? ID_VALUE, string TABLE_TYPE, string FilePath, string SELECT, string id_jnsDok = null)
    {
      myThread = new Thread(new ThreadStart(this.ShowProgresBar));
      myThread.Start();
      try
      {
        SvcAsetDokCrud.InputParameters inputData = new SvcAsetDokCrud.InputParameters();
        inputData.P_ID_DOK = 1;
        inputData.P_ID_DOKSpecified = true;
        inputData.P_ID_TYPE = ID_TYPE;
        inputData.P_ID_VALUE = ID_VALUE;
        inputData.P_ID_VALUESpecified = true;
        if (id_jnsDok != null)
        {
          inputData.P_KD_DOK = id_jnsDok;
        }
        inputData.P_ISI_FILE = konfigApp.FileToByteArray(FilePath);
        inputData.P_TABLE_TYPE = TABLE_TYPE;
        inputData.P_SELECT = SELECT;

        svcDokCrud = new SvcAsetDokCrud.call_pttClient();
        svcDokCrud.Beginexecute(inputData, new AsyncCallback(getCrudDokASet), "");

      }
      catch (Exception E)
      {

        if (this.modeCrud == 'D')
        {
          this.Invoke(new AktifkanForm(this.aktifkanForm), "");
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        }
        else
        {
          this.Invoke(new AktifkanForm(this.aktifkanForm), "");
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        }
        MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalSimpan);
      }
    }
    private void getCrudDokASet(IAsyncResult result)
    {
      try
      {
        dataoutDokAsetCrud = svcDokCrud.Endexecute(result);
        svcDokCrud.Close();
        if (this.modeCrud == 'D')
        {
          this.Invoke(new AktifkanForm(this.aktifkanForm), "");
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        }
        else
        {
          this.Invoke(new AktifkanForm(this.aktifkanForm), "");
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        }
        if (dataoutDokAsetCrud.PO_RESULT == "Y")
        {
          this.Invoke(new CrudDokAset(this.crudDokAset), dataoutDokAsetCrud);
        }
        else
        {
          MessageBox.Show(dataoutDokAsetCrud.PO_RESULT_MESSAGE, konfigApp.judulGagalLain);
        }
      }
      catch (Exception e)
      {
        if (this.modeCrud == 'D')
        {
          this.Invoke(new AktifkanForm(this.aktifkanForm), "");
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        }
        else
        {
          this.Invoke(new AktifkanForm(this.aktifkanForm), "");
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        }
        MessageBox.Show(konfigApp.teksGagalSimpanFileDok, konfigApp.judulGagalLain);
      }
    }

    private delegate void CrudDokAset(SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud);

    private void crudDokAset(SvcAsetDokCrud.OutputParameters dataoutDokAsetCrud)
    {
      if (dataoutDokAsetCrud.PO_RESULT == "Y")
      {
        MessageBox.Show(dataoutDokAsetCrud.PO_RESULT_MESSAGE);
        if (this.status != "hapus")
        {
          if (this.status != "input")
          {
            this.ucRwyRmhNgr.dataInisial = false;
          }
          this.ucRwyRmhNgr.getById = true;
          //this.ucRwyRmhNgr.getInitDokKib(" ID_KRMH_RWYT_RMH = " + this.idRmhNgrRwy.ToString());

        }

        this.Close();
        this.ucRwyRmhNgr.search = "";
        this.ucRwyRmhNgr.pencarian = false;
        this.ucRwyRmhNgr.initGrid();
        this.ucRwyRmhNgr.getInitRwyRmhNgr();
      }

    }
    #endregion




    private void FrmRwyRmhNgr_Load(object sender, EventArgs e)
    {
      this.getInitialDataDokKibAngk();
      this.teJnsDok.Properties.ReadOnly = true;
      this.teFileName.Properties.ReadOnly = true;
    }


    public void hapusData()
    {
      if (MessageBox.Show(konfigApp.teksHapusData + " No " + this.selectedData.NUM.ToString() + " ?", konfigApp.judulHapusData,
          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        try
        {
          myThread = new Thread(new ThreadStart(ShowProgresBar));
          myThread.Start();

          SvcRwyRmhNgrCrud.InputParameters parInp = new SvcRwyRmhNgrCrud.InputParameters();
          parInp.P_ID_KRMH_RWYT_RMHSpecified = true;
          parInp.P_ID_KRMH_RWYT_RMH = this.idRmhNgrRwy;
          parInp.P_SELECT = "D";

          this.modeCrud = Convert.ToChar(parInp.P_SELECT);
          this.crudData = new SvcRwyRmhNgrCrud.call_pttClient();
          crudData.Open();
          this.crudData.Beginexecute(parInp, new AsyncCallback(crudDokKibAngk), "");
        }
        catch
        {
          this.modeCrud = 'A';
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
          MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
        }
      }
    }


    #region jenis dokumen
    private AppPengguna.PU.FrmPuJnsDok jnsDok;
    private void sbJnsDok_Click(object sender, EventArgs e)
    {
      jnsDok = new AppPengguna.PU.FrmPuJnsDok();
      jnsDok.ambilJnsDok = new AppPengguna.PU.AmbilJnsDok(this.ambilJnsDok);
      jnsDok.ShowDialog();
    }
    private void ambilJnsDok(string id, string nama)
    {
      this.ID_JNSDOK = id;
      this.teJnsDok.Text = nama;
    }
    #endregion
  }
}