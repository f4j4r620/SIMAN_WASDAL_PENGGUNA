using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;

namespace AppPengguna.AST.RN
{
    public partial class ShowPicture : Form
    {
        public ShowPicture()
        {
            InitializeComponent();
            isPict.Width = this.Width;

        }

        Image curImage;
        ucRumahNegaraForm ucRmhNgr;
        public ShowPicture(ucRumahNegaraForm _ucRmhNgr,Image _curImage)
        {
            curImage = _curImage;
            ucRmhNgr = _ucRmhNgr;
            InitializeComponent();
            //isPict.Width = this.Width;           

        }

        private void ShowPicture_Load(object sender, EventArgs e)
        {
            //if (ucRmhNgr.listImage != null)
            //{
            //    for (int i = 0; i < ucRmhNgr.listImage.LongCount(); i++)
            //    {
            //        isPict.Images.Add(ucRmhNgr.listImage[i]);
            //    }

            //    SetImageIndex(isPict.Images.IndexOf(curImage));
            //    Image img = curImage;
            //    isPict.Width = img.Width;
            //    isPict.Height = img.Height;
            //}
        }

        private void SetImageIndex(int imageIndex)
        {
            // MessageBox.Show(imageIndex.ToString());
            PropertyInfo pi = isPict.GetType().GetProperty("CurrentImageIndex", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(isPict, imageIndex, null);
            pi = isPict.GetType().GetProperty("CurrentImage", BindingFlags.Instance | BindingFlags.Public);
            pi.SetValue(isPict, isPict.Images[imageIndex], null);
        }

        private void isPict_CurrentImageIndexChanged(object sender, DevExpress.XtraEditors.Controls.ImageSliderCurrentImageIndexChangedEventArgs e)
        {
            Image img = isPict.CurrentImage;
            isPict.Width = img.Width;
            isPict.Height = img.Height;
        }
    }
}
