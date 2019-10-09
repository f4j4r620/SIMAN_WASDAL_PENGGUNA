﻿using System;
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

namespace AppPengguna.AST.BG
{
  internal partial class PuAsuransiBangunan : Form
  {
    private ucAsuransiBangunan ucAsuransiBangunan = null;

    private string status;

    private SvcAsuransiBangunanSelect.BPSIMANSROW_KBDG_RWYT_ASURANSI selectedData;
    private SvcAsuransiBangunanCrud.InputParameters parInp;
    private SvcAsuransiBangunanCrud.OutputParameters outCrud;
    private SvcAsuransiBangunanCrud.rwytAsuransiCud_pttClient crudData;

    private decimal? idKbdg;
    private decimal? idKbdgAsuransi;
    public decimal? NUM;
    public string path;
    public string ID_JNSDOK;
    private decimal? ID_SATKER = null;

    public Thread myThread = null;
    private frmProgres progresBar = null;
    public char modeCrud = 'A';

    string FilePath;

    //public DateTime? tglDok
    //{
    //    set { teTglDok.EditValue = value; }
    //    get { return Convert.ToDateTime(teTglDok.EditValue); }
    //}

    public PuAsuransiBangunan(ucAsuransiBangunan _ucAsuransiBangunan, string status)
    {
      InitializeComponent();
      this.ucAsuransiBangunan = _ucAsuransiBangunan;
      this.status = status;
      this.idKbdg = _ucAsuransiBangunan.IdKbg;
      // this.idKbdg = _ucAsuransiBangunan.selectedData.ID_KANGK;
      this.selectedData = _ucAsuransiBangunan.selectedData;

      if (_ucAsuransiBangunan.selectedData != null)
      {
        this.NUM = _ucAsuransiBangunan.selectedData.NUM;
      }
      if (status == "detail")
      {
        this.bbKibSimpan.Enabled = false;
        this.sbNamaFile.Enabled = false;
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
        this.beMarqueBar.Visibility = str;
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
      string noPolis = (teNoPolis.Text != "-") ? teNoPolis.Text : null;
      string tglPolis = teTglPolis.Text;
      string tglJthTmp = teTglJthTempo.Text;
      string jgkWaktu = teJangkaWkt.Text;
      string jnsAsuransi = teJnsAsuransi.Text;
      string kdJnsAsuransi = null;
      if (jnsAsuransi == "Gabungan (All Risk)")
      {
        kdJnsAsuransi = "01";
      }
      else if (jnsAsuransi == "Kerugian Total (TLO)")
      {
        kdJnsAsuransi = "02";
      }
      string atsNama = teKodeUnitSatker.Text;
      string jmlPremi = teJmlPremi.Text;
      string nilaiPert = teNilaiPertanggunan.Text;
      string ket = meKet.Text;
      string jnsDok = teJnsDok.Text;


      decimal? jw = konfigApp.DecimaltoNull(Convert.ToDecimal(jgkWaktu));
      decimal? jp = konfigApp.DecimaltoNull(Convert.ToDecimal(jmlPremi));
      decimal? np = konfigApp.DecimaltoNull(Convert.ToDecimal(nilaiPert));
      string nmFile = teNmFile.Text;
      //if (teNmFile.Text != "")
      //{

      try
      {
        myThread = new Thread(new ThreadStart(ShowProgresBar));
        myThread.Start();

        SvcAsuransiBangunanCrud.InputParameters parInp = new SvcAsuransiBangunanCrud.InputParameters();
        parInp.P_ID_KBDGSpecified = true;
        parInp.P_ID_KBDG_RWYT_ASURANSISpecified = true;

        parInp.P_ID_KBDG_RWYT_ASURANSI = idKbdgAsuransi;
        parInp.P_ID_KBDG = idKbdg;
        parInp.P_NO_POLIS = noPolis;
        parInp.P_TGL_POLIS = tglPolis;
        parInp.P_TGL_JATUH_TEMPO = tglJthTmp;
        parInp.P_JANGKA_WAKTU = jw;
        parInp.P_KD_JNS_ASURANSI = kdJnsAsuransi;
        parInp.P_ID_SATKER = konfigApp.DecimaltoNull(ID_SATKER);
        parInp.P_JMLH_PREMI = jp;
        parInp.P_NILAI_PERTANGGUNGAN = np;
        parInp.P_KETERANGAN = ket;


        parInp.P_NMFILE = nmFile;

        if (this.status == "input")
        {
          parInp.P_SELECT = "C";
        }
        else
        {
          parInp.P_SELECT = "U";
        }

        this.modeCrud = Convert.ToChar(parInp.P_SELECT);
        this.crudData = new SvcAsuransiBangunanCrud.rwytAsuransiCud_pttClient();
        crudData.Open();
        this.crudData.Beginexecute(parInp, new AsyncCallback(crudAsuransi), "");

      }
      catch
      {
        this.modeCrud = 'A';
        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
      }
      //}
      //else
      //{
      //  MessageBox.Show("Dokumen Polis harus terisi!", konfigApp.judulGagalSimpan);
      //}
    }

    private void bbKibRefresh_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.getInitialDataAsuransi();
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
            teNmFile.Text = fileName;

            //teNmPath.Text = FilePath;
            this.path = FilePath;
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

    private SvcAsuransiBangunanSelect.rwytAsuransiSelect_pttClient svcCaller;
    private SvcAsuransiBangunanSelect.InputParameters inputPar;
    private SvcAsuransiBangunanSelect.OutputParameters outputPar;
    private SvcAsuransiBangunanSelect.BPSIMANSROW_KBDG_RWYT_ASURANSI rowData;

    protected ArrayList dataGrid;
    protected bool dataInisial;

    public void getInitialDataAsuransi()
    {
      try
      {
        myThread = new Thread(new ThreadStart(this.ShowProgresBar));
        myThread.Start();
        inputPar = new SvcAsuransiBangunanSelect.InputParameters();
        inputPar.P_COL = "";
        inputPar.P_MAX = 100;
        inputPar.P_MAXSpecified = true;
        inputPar.P_MIN = 0;
        inputPar.P_MINSpecified = true;
        inputPar.P_SORT = "";
        svcCaller = new SvcAsuransiBangunanSelect.rwytAsuransiSelect_pttClient();
        svcCaller.Open();
        svcCaller.Beginexecute(inputPar, new AsyncCallback(this.getDataDokKibTnh), null);
      }
      catch
      {

        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
        MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
      }
    }

    public void getDataDokKibTnh(IAsyncResult result)
    {
      try
      {
        this.outputPar = svcCaller.Endexecute(result);
        svcCaller.Close();
        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        this.Invoke(new AktifkanForm(this.aktifkanForm), "");
        this.Invoke(new ShowDataDokKibAngk(this.showDataDokKibTnh), this.outputPar);
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

    private delegate void ShowDataDokKibAngk(SvcAsuransiBangunanSelect.OutputParameters dataOut);

    public void showDataDokKibTnh(SvcAsuransiBangunanSelect.OutputParameters serviceOutPut)
    {
      int jmlDataGroup = serviceOutPut.SF_ROW_KBDG_RWYT_ASURANSI.Count();
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
        this.gcDokKibAngk.Text = "Input Data Asuransi Angkutan";
        this.idKbdgAsuransi = 0;
        teNoPolis.ResetText();
        teTglPolis.ResetText();
        meKet.ResetText();
        teNmFile.ResetText();
        teJnsDok.ResetText();
        teTglJthTempo.ResetText();
        teNilaiPertanggunan.ResetText();
        teJnsAsuransi.ResetText();
        teJmlPremi.ResetText();
        teJangkaWkt.ResetText();
        teKodeUnitSatker.ResetText();
        teUr_Satker.ResetText();
      }
      else if (this.status == "edit" || this.status == "detail")
      {
        try
        {
          if (this.status == "detail")
          {
            this.gcDokKibAngk.Text = "Detail Data Asuransi Angkutan";
          }
          else
          {
            this.gcDokKibAngk.Text = "Edit Data Asuransi Angkutan";
          }
          this.idKbdgAsuransi = selectedData.ID_KBDG_RWYT_ASURANSI;
          //this.teKdSmilik.Text = selectedData.KD_SMILIK;
          //this.teUrSmilik.Text = selectedData.UR_SMILIK;

          this.teNoPolis.Text = selectedData.NO_POLIS;
          this.teTglPolis.Text = konfigApp.DateToString(selectedData.TGL_POLIS);
          this.teTglJthTempo.Text = konfigApp.DateToString(selectedData.TGL_JATUH_TEMPO);
          this.teNilaiPertanggunan.Text = Convert.ToString(selectedData.NILAI_PERTANGGUNGAN);
          this.teJnsAsuransi.Text = konfigApp.StringtoNull(selectedData.UR_JNS_ASURANSI);
          this.teJmlPremi.Text = Convert.ToString(selectedData.JMLH_PREMI);
          this.teJangkaWkt.Text = Convert.ToString(selectedData.JANGKA_WAKTU);
          this.teUr_Satker.Text = selectedData.UR_SATKER;
          this.teKodeUnitSatker.Text = konfigApp.StringtoNull(selectedData.KD_SATKER);
          this.teNmFile.Text = selectedData.NMFILE;
          this.meKet.Text = selectedData.KETERANGAN;

          this.teJnsDok.Enabled = false;
          this.sbJnsDok.Enabled = false;

        }
        catch (Exception)
        {

          MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
        }

      }
      else
      {
        this.idKbdgAsuransi = selectedData.ID_KBDG_RWYT_ASURANSI;
      }
      this.Focus();

    }

    #region crud
    public void crudAsuransi(IAsyncResult result)
    {
      try
      {
        outCrud = crudData.Endexecute(result);
        crudData.Close();
        this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
        this.Invoke(new UbahDsDetail(this.ubahDsDetail), outCrud);
      }
      catch (Exception e)
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

    public delegate void UbahDsDetail(SvcAsuransiBangunanCrud.OutputParameters outCrud);

    public void ubahDsDetail(SvcAsuransiBangunanCrud.OutputParameters outCrud)
    {
      SvcAsuransiBangunanSelect.BPSIMANSROW_KBDG_RWYT_ASURANSI dataPenyama = new SvcAsuransiBangunanSelect.BPSIMANSROW_KBDG_RWYT_ASURANSI();

      dataPenyama.NUM = 99;
      dataPenyama.ID_KBDG_RWYT_ASURANSI = outCrud.PO_ID_KBDG_RWYT_ASURANSI;
      this.idKbdgAsuransi = outCrud.PO_ID_KBDG_RWYT_ASURANSI;

      switch (this.modeCrud)
      {
        case 'C':


          if (this.FilePath != null)
          {
            string Path = this.FilePath;

            simpanFile("ID_KBDG_RWYT_ASURANSI", dataPenyama.ID_KBDG_RWYT_ASURANSI, "M_KBDG_RWYT_ASURANSI", Path, "C", ID_JNSDOK);
            this.ucAsuransiBangunan.search = "";
            this.ucAsuransiBangunan.pencarian = false;
            this.ucAsuransiBangunan.initGrid();
            this.ucAsuransiBangunan.getInitAsuransi();
          }
          else
          {
            this.ucAsuransiBangunan.dataInisial = false;
            this.ucAsuransiBangunan.getById = true;
            this.ucAsuransiBangunan.getInitAsuransi(" ID_KBDG_RWYT_ASURANSI = " + this.idKbdgAsuransi.ToString());
            this.init();
            this.Close();
          }
          break;
        case 'U':
          ucAsuransiBangunan.binder.Remove(this.selectedData);
          if (this.FilePath != null)
          {
            string Path = this.FilePath;
            if (this.selectedData.FILE_EXISTS != 0)
            {
              simpanFile("ID_KBDG_RWYT_ASURANSI", dataPenyama.ID_KBDG_RWYT_ASURANSI, "M_KBDG_RWYT_ASURANSI", Path, "U", ID_JNSDOK);
            }
            else
            {
              simpanFile("ID_KBDG_RWYT_ASURANSI", dataPenyama.ID_KBDG_RWYT_ASURANSI, "M_KBDG_RWYT_ASURANSI", Path, "C", ID_JNSDOK);
              //ucAsuransiBangunan.gvUcDtl.RefreshData();
            }
          }
          else
          {
            //this.ucAsuransiBangunan.dataInisial = false;
            //this.ucAsuransiBangunan.getById = true;
            // this.ucAsuransiBangunan.getInitAsuransi(" ID_KBDG_RWYT_ASURANSI = " + this.idKbdgAsuransi.ToString());

            ucAsuransiBangunan.gvUcDtl.RefreshData();
            this.init();
            this.Close();
            this.ucAsuransiBangunan.search = "";
            this.ucAsuransiBangunan.pencarian = false;
            this.ucAsuransiBangunan.initGrid();
            this.ucAsuransiBangunan.getInitAsuransi();
          }
          break;
        case 'D':
          ucAsuransiBangunan.binder.Remove(this.selectedData);
          ucAsuransiBangunan.gvUcDtl.RefreshData();
          ucAsuransiBangunan.StrTotalGrid.Caption = (Convert.ToInt64(ucAsuransiBangunan.StrTotalGrid.Caption) - 1).ToString();
          ucAsuransiBangunan.StrTotalDb.Caption = (Convert.ToInt64(ucAsuransiBangunan.StrTotalDb.Caption) - 1).ToString();
          this.init();
          this.Close();
          this.ucAsuransiBangunan.search = "";
          this.ucAsuransiBangunan.pencarian = false;
          this.ucAsuransiBangunan.initGrid();
          this.ucAsuransiBangunan.getInitAsuransi();
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
            this.ucAsuransiBangunan.dataInisial = false;
          }
          this.ucAsuransiBangunan.getById = true;
          //this.ucAsuransiBangunan.getInitAsuransi(" ID_KBDG_RWYT_ASURANSI = " + this.idKbdgAsuransi.ToString());

        }

        this.Close();
        this.ucAsuransiBangunan.search = "";
        this.ucAsuransiBangunan.pencarian = false;
        this.ucAsuransiBangunan.initGrid();
        this.ucAsuransiBangunan.getInitAsuransi();
      }

    }
    #endregion




    public void hapusData()
    {
      if (MessageBox.Show(konfigApp.teksHapusData + " No " + this.selectedData.NUM.ToString() + " ?", konfigApp.judulHapusData,
          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        try
        {
          myThread = new Thread(new ThreadStart(ShowProgresBar));
          myThread.Start();

          SvcAsuransiBangunanCrud.InputParameters parInp = new SvcAsuransiBangunanCrud.InputParameters();
          parInp.P_ID_KBDG_RWYT_ASURANSISpecified = true;
          parInp.P_ID_KBDG_RWYT_ASURANSI = this.idKbdgAsuransi;
          parInp.P_SELECT = "D";

          this.modeCrud = Convert.ToChar(parInp.P_SELECT);
          this.crudData = new SvcAsuransiBangunanCrud.rwytAsuransiCud_pttClient();
          crudData.Open();
          this.crudData.Beginexecute(parInp, new AsyncCallback(crudAsuransi), "");
        }
        catch
        {
          this.modeCrud = 'A';
          this.Invoke(new ProgBar(this.progBar), BarItemVisibility.Never);
          MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
        }
      }
    }

    private void PuAsuransiBangunan_Load(object sender, EventArgs e)
    {
      this.getInitialDataAsuransi();
      this.teNmFile.Properties.ReadOnly = true;
      this.teJnsDok.Properties.ReadOnly = true;
      this.teKodeUnitSatker.Properties.ReadOnly = true;
      this.teUr_Satker.Properties.ReadOnly = true;
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


    private AppPengguna.PU.FrmPUSatker puSatker;
    private void btnCariSatker_Click(object sender, EventArgs e)
    {
      puSatker = new AppPengguna.PU.FrmPUSatker();
      puSatker.ambilSatker = new AppPengguna.PU.AmbilSatker(this.ambilSatker);
      puSatker.ShowDialog();
    }

    private void ambilSatker(decimal? id, string kode, string nama)
    {
      this.ID_SATKER = id;
      this.teKodeUnitSatker.Text = kode;
      this.teUr_Satker.Text = nama;
    }

  }
}