using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AppPengguna.KKW.RSK.PU
{
    public partial class FrmPuViewPdf : Form
    {
        string FilePath;
        public FrmPuViewPdf()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }
        public void display(string file)
        {
            FilePath = file;
            pdfViewer1.DocumentFilePath = file;
        }

        private void btnSavePdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFile.Filter = "PDF Files|*.pdf|All files (*.*)|*.*";
            SaveFile.Title = "Save As...";
            SaveFile.DefaultExt = "*.pdf";
            SaveFile.FilterIndex = 1;
            SaveFile.InitialDirectory = @"C:\";

            //SaveFile.CheckFileExists = true;
            //SaveFile.CheckPathExists = true;
            //SaveFile.DefaultExt = "pdf";
            SaveFile.FileName = FilePath;
            //SaveFile.RestoreDirectory = true;

            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                //Stream input = System.IO.File.ReadAllBytes(FilePath);
                //Stream file = File.OpenWrite(SaveFile.FileName);
                //CopyStream(input, file);
                try
                {
                    System.IO.File.Copy(FilePath, SaveFile.FileName,true);
                }
                catch (Exception E)
                {

                    MessageBox.Show(E.ToString(), konfigApp.judulGagal);
                }
                
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

       
        private void FrmPuViewPdf_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            try
            {
                pdfViewer1.CloseDocument();
                System.IO.File.Delete(FilePath);
            }
            catch (Exception)
            {

            }
        }


    }
}
