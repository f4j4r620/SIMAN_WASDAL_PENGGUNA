using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.AST.PUASET
{
  public partial class frmPenyusutan : DevExpress.XtraEditors.XtraForm
  {
    string status;
    public frmPenyusutan(SvcSusutRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_SUSUT selectedData, string _status)
    {
      InitializeComponent();
      string status = _status;
      this.teJnsSusut.Text = selectedData.UR_SUSUT;
      this.teTglSusut.Text = konfigApp.DateToString(selectedData.TGL_SUSUT);
      this.teJnsTransaksi.Text = selectedData.JNS_TRN;
      this.teNoSppa.Text = selectedData.NO_SPPA;
    }
    public frmPenyusutan(SvcSusutBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_SUSUT selectedData, string _status)
    {
      InitializeComponent();
      string status = _status;
      this.teJnsSusut.Text = selectedData.UR_SUSUT;
      this.teTglSusut.Text = konfigApp.DateToString(selectedData.TGL_SUSUT);
      this.teJnsTransaksi.Text = selectedData.JNS_TRN;
      this.teNoSppa.Text = selectedData.NO_SPPA;
    }
    public frmPenyusutan(SvcBangunanAirSusutSelect.BPSIMANSROW_M_KBAIR_RWYT_SUSUT selectedData, string _status) 
    {
      InitializeComponent();
      string status = _status;
      this.teJnsSusut.Text = selectedData.UR_SUSUT;
      this.teTglSusut.Text = konfigApp.DateToString(selectedData.TGL_SUSUT);
      this.teJnsTransaksi.Text = selectedData.JNS_TRN;
      this.teNoSppa.Text = selectedData.NO_SPPA;
    }
    public frmPenyusutan(SvcPropertiKhususSusutSelect.BPSIMANSROW_M_KPROK_RWYT_SUSUT selectedData, string _status)
    {
        InitializeComponent();
        string status = _status;
        this.teJnsSusut.Text = selectedData.UR_SUSUT;
        this.teTglSusut.Text = konfigApp.DateToString(selectedData.TGL_SUSUT);
        this.teJnsTransaksi.Text= (selectedData.JNS_TRN == "-")? null :selectedData.JNS_TRN;
        this.teNoSppa.Text = selectedData.NO_SPPA;
    }
    private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.Close();
    }

    private void ReadOnly(Control c, bool state)
    {
      if (c is TextEdit)
        (c as TextEdit).Properties.ReadOnly = state;
      else if (c is SimpleButton)
        (c as SimpleButton).Enabled = !state;
      else if (c is PictureEdit)
        (c as PictureEdit).Properties.ReadOnly = state;
      else if (c is SpinEdit)
        (c as SpinEdit).Properties.ReadOnly = state;
    }
    private void FormReadOnly(bool state)
    {

      foreach (Control c in this.LayoutData.Controls)
      {
        this.ReadOnly(c, state);

      }


    }
  }
}