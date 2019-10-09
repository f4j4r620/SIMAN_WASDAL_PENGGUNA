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
    public delegate void AmbilCari(string kolom, string kata_kunci);
    public partial class frmCari : DevExpress.XtraEditors.XtraForm
    {
        public AmbilCari ambilCari;
        private string kolom;
        private string kata_kunci;
        public frmCari()
        {
            InitializeComponent();
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
           kolom= this.LuKolom.Text;
           if (this.layoutText.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
           {
               kata_kunci = this.teSearch1.Text;
           }
           else if (this.layoutDate.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
           {
               kata_kunci = this.teSearch2.Text;
           }
           else
           {
               kata_kunci = this.teNumber.Value.ToString();
           }
           
           if (kolom.Trim().Length < 1 || kata_kunci.Trim().Length < 1)
           {
               MessageBox.Show("Parameter pencarian harus diisi.", "Perhatian");
               return;
           }

           ambilCari(kolom, kata_kunci);
           this.Close();
        }

        private void LuKolom_EditValueChanged(object sender, EventArgs e)
        {
            string nama_kolom = this.LuKolom.EditValue.ToString();
            this.LuKolom.Width = 40 + (((int)nama_kolom.Length) * 5);
            this.teSearch2.ResetText();
            this.teSearch1.ResetText();
            nama_kolom = nama_kolom.Trim().Substring(0,3).ToUpper();
            switch (nama_kolom)
            {
                case "TGL":
                    this.layoutDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.layoutText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.LayoutNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case "NIL":
                    this.layoutDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.layoutText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.LayoutNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    break;
                default:
                    this.layoutDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.LayoutNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.layoutText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    break;
            }
        }

       

     
      

    }
}