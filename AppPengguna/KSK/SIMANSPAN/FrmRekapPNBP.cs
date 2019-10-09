using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace AppPengguna.KSK.SIMANSPAN
{
    public partial class FrmRekapPNBP : Form
    {
        public ToggleProgressBar toggleProgressBar;
        public SimpanDataRsk simpanDataRskPspBmn;
        public string statusForm = null;
        private Thread myThread;
        GridView rowTerpilih = null;
        public decimal? idPemohon = null;
        public string kodePenerbitSkDetail = null;
        public string namaPenerbitSkDetail = null;
        private char modeCrud = 'A';
        public SvcGridPnbpRekap.WASDALSROW_GRID_REKAP_PNBP_SPAN dataTerpilih;

        public FrmRekapPNBP(string _status)
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        #region Thread
        private void toggleProgBarPu(string kondisi)
        {
            if (kondisi == "start") this.aktifkanProgresBar();
            else this.nonAktifkanprogressBar();
        }

        private void aktifkanProgresBar()
        {
            toggleProgressBar("start");
        }

        private void nonAktifkanprogressBar()
        {
            toggleProgressBar("finish");
        }

        public delegate void AktifkanForm(string str);

        public void aktifkanForm(string str)
        {
            this.Enabled = true;
        }
        #endregion

        private void bbSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            simpanDataRskPspBmn("U");
            this.Dispose();
        }

    }
}
