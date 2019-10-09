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
  public partial class FormNoPolAngk : Form
  {
    private SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL selectedRow;
    private SvcNoPolAngkCrud.call_pttClient crudCaller;
    private SvcNoPolAngkCrud.InputParameters crudInput;
    private SvcNoPolAngkCrud.OutputParameters crudOut;
    private Thread myThread;
    private string operation;
    private char modeCrud;

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
    public SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL SelectedRow
    {
      get
      {
        return selectedRow;
      }
    }

    public decimal? ID_KANGK;

    private UcNoPolAngk ucNoPolAngk;
    public FormNoPolAngk(Char _modeCrud, SvcNoPolAngkSelect.BPSIMANSROW_KANGK_NOPOL _selectedRow, decimal? _ID_KANGK)
    {
      InitializeComponent();
      this.ID_KANGK = _ID_KANGK;
      //MessageBox.Show("dapet id_kangk ? " + this.ID_KANGK, "debug");
      this.modeCrud = _modeCrud;

      if (this.modeCrud == 'U')
      {
        this.selectedRow = _selectedRow;
        this.showDetailNoPol();
        this.operation = "U";
      }
      else if (this.modeCrud == 'C')
      {
        this.operation = "C";
        this.selectedRow = _selectedRow;
      }
      else if (this.modeCrud == 'D')
      {
        this.operation = "D";
        this.selectedRow = _selectedRow;
        this.bbNoPolAngkSimpan_ItemClick(null, null);
      }
      else if (this.modeCrud == 'V')
      {
        this.selectedRow = _selectedRow;
        this.showDetailNoPol();
        this.operation = "V";
        this.bbNoPolAngkSimpan.Enabled = false;
      }

    }
    private void showDetailNoPol()
    {
      teNoPol.Text = selectedRow.NO_POLISI;
      deAwlBrlkuNoPol.EditValue = Convert.ToDateTime(selectedRow.TGL_KELUAR);
      deAkhrBrlkuNoPol.EditValue = Convert.ToDateTime(selectedRow.TGL_SD_BERLAKU);
      meKetNopol.Text = selectedRow.KET;
    }
    private SvcNoPolAngkCrud.InputParameters parseParam(string _crudOperation)
    {
      crudInput = new SvcNoPolAngkCrud.InputParameters();

      if (_crudOperation == "U" || _crudOperation == "D")
      {
        crudInput.P_ID_KANGK_NOPOL = selectedRow.ID_KANGK_NOPOL;
        crudInput.P_ID_KANGK_NOPOLSpecified = true;
      }

      crudInput.P_ID_KANGK = this.ID_KANGK;

      // MessageBox.Show("_crudOperation = " + _crudOperation + ", ID_KANGK = " + this.ID_KANGK, "debug");  

      crudInput.P_ID_KANGKSpecified = true;

      crudInput.P_NO_POLISI = (teNoPol.Text == "-" ? "" : teNoPol.Text);
      crudInput.P_TGL_KELUAR = Convert.ToDateTime(deAwlBrlkuNoPol.EditValue);
      crudInput.P_TGL_KELUARSpecified = true;
      crudInput.P_TGL_SD_BERLAKU = Convert.ToDateTime(deAkhrBrlkuNoPol.EditValue);
      crudInput.P_TGL_SD_BERLAKUSpecified = true;
      crudInput.P_KET = (meKetNopol.Text == "-" ? "" : meKetNopol.Text);
      crudInput.P_SELECT = operation;
      crudInput.P_TERAKHIR_YN = "Y";
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
    public void ShowProgresBar()
    {
      this.progBar(BarItemVisibility.Always);
    }

    public void crudOperation(string _crudOperation)
    {
      // myThread = new Thread(new ThreadStart(ShowProgresBar));
      // myThread.Start();

      crudCaller = new SvcNoPolAngkCrud.call_pttClient(konfigApp.SvcNoPolAngkCrud_name, konfigApp.SvcNoPolAngkCrud_address);
      crudCaller.Open();
      crudCaller.Beginexecute(parseParam(_crudOperation), new AsyncCallback(this.crudResult), "");
    }
    public void crudResult(IAsyncResult result)
    {
      try
      {
        crudOut = crudCaller.Endexecute(result);
        crudCaller.Close();
        // this.Invoke(new ProgBar(progBar), BarItemVisibility.Always);
        switch (this.modeCrud)
        {
          case 'C':
            konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
            this.Close();
            
            //this.Invoke(MessageBox.Show(konfigApp.teksDialog, konfigApp.judulBerhasilAmbil),new String[]{""});
            break;
          case 'U':
            konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
            this.Close();
            break;
          case 'D':
            konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
            this.Close();
            break;
          default:
            konfigApp.teksDialog = crudOut.PO_RESULT_MESSAGE;
            break;
        }

        if (crudOut.PO_RESULT == "Y")
        {
          MessageBox.Show(konfigApp.teksDialog, konfigApp.judulBerhasilAmbil);
          //this.edited = true;
          this.ucNoPolAngk.search = "";
          this.ucNoPolAngk.initGrid();
          this.ucNoPolAngk.getInitNoPolAngk();
        }
        else
        {
          MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
        }

        // this.Invoke(new AktifkanForm(konfigApp.aktifkanForm), "");
      }
      catch
      {

        //this.Invoke(new ProgBar(progBar), BarItemVisibility.Never);
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

    private ThreadStart beMarqueeBar(BarItemVisibility barItemVisibility)
    {
      throw new NotImplementedException();
    }

    private void bbNoPolAngkSimpan_ItemClick(object sender, ItemClickEventArgs e)
    {
      crudOperation(this.operation);
    }

    private void bbNoPolAngkRefresh_ItemClick(object sender, ItemClickEventArgs e)
    {
      teNoPol.Text = "";
      deAwlBrlkuNoPol.Text = "";
      deAkhrBrlkuNoPol.Text = "";
      meKetNopol.Text = "";
    }

    private void bbNoPolAngkTutup_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.Close();
    }
  }
}
