using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace AppPengguna.KSK.WL
{
    public partial class WaitForm1 : WaitForm
    {
        Int32 jml = 0;
        Int32 currData = 0;
        public WaitForm1(Int32 jmlData, Int32 curr)
        {

            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
            jml = jmlData;
            currData = curr;
            progressBar1.Properties.Maximum = jmlData;
            progressBar1.Properties.Minimum = 0;
            progressBar1.Properties.Step = 1;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum WaitFormCommand
        {
        }

        private void WaitForm1_Load(object sender, EventArgs e)
        {
            
                progressBar1.Position = currData;
                if (progressBar1.Position == jml)
                {
                    this.Close();
                }
            
        }
    }
}