using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace AppPengguna.AST.RN
{


    public partial class FrmMutRmhNgr : Form
    {

        private UcMutRmhNgr ucMutRmhNgr;

        public FrmMutRmhNgr(UcMutRmhNgr _ucMutRmhNgr)
        {
            this.ucMutRmhNgr = _ucMutRmhNgr;
            InitializeComponent();
        }

        public FrmMutRmhNgr()
        {
            InitializeComponent();
        }

        private void bbSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbTutup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
