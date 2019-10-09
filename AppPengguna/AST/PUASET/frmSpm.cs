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
  public partial class frmSpm : DevExpress.XtraEditors.XtraForm
  {
    string status;
    public frmSpm(SvcRmhNgrSpmSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_SPM selectedData, string _status)
    {
      InitializeComponent();
      this.status = _status;
      this.teBkpk.Text = selectedData.BKPK;
      this.teJmlSpm.Text =Convert.ToString(selectedData.JML_SPM);
      this.teKdTransaksi.Text = selectedData.UR_TRN;
      this.teNoSp2d.Text = selectedData.NO_SP2D;
      this.teNoSppa.Text = selectedData.NO_SPPA;
      this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
      this.teTglSp2d.Text = konfigApp.DateToString(selectedData.TGL_SP2D);
    }

    public frmSpm(SvcBangunanSpmSelect.BPSIMANSROW_M_KBDG_RWYT_SPM selectedData, string _status)
    {
      InitializeComponent();
      this.status = _status;
      this.teBkpk.Text = selectedData.BKPK;
      this.teJmlSpm.Text = Convert.ToString(selectedData.JML_SPM);
      this.teKdTransaksi.Text = selectedData.UR_TRN;
      this.teNoSp2d.Text = selectedData.NO_SP2D;
      this.teNoSppa.Text = selectedData.NO_SPPA;
      this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
      this.teTglSp2d.Text = konfigApp.DateToString(selectedData.TGL_SP2D);
    }

    public frmSpm(SvcBangunanAirSpmSelect.BPSIMANSROW_M_KBAIR_RWYT_SPM selectedData, string _status) 
    {
      InitializeComponent();
      this.status = _status;
      this.teBkpk.Text = selectedData.BKPK;
      this.teJmlSpm.Text = Convert.ToString(selectedData.JML_SPM);
      this.teKdTransaksi.Text = selectedData.UR_TRN;
      this.teNoSp2d.Text = selectedData.NO_SP2D;
      this.teNoSppa.Text = selectedData.NO_SPPA;
      this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
      this.teTglSp2d.Text = konfigApp.DateToString(selectedData.TGL_SP2D);
    }

    public frmSpm(SvcPropertiKhususSpmSelect.BPSIMANSROW_M_KPROK_RWYT_SPM selectedData, string _status)
    {
        InitializeComponent();
        this.status = _status;
        this.teBkpk.Text = selectedData.BKPK;
        this.teJmlSpm.Text = Convert.ToString(selectedData.JML_SPM);
        this.teKdTransaksi.Text = selectedData.UR_TRN;
        this.teNoSp2d.Text = selectedData.NO_SP2D;
        this.teNoSppa.Text = selectedData.NO_SPPA;
        this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
        this.teTglSp2d.Text = konfigApp.DateToString(selectedData.TGL_SP2D);
    }

    private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.Close();
    }
  }
}