using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPengguna.KSK.SIMANSPAN
{
    public partial class FrmValidasiSatkerSpan : Form
    {
        public SvcGridBandingSpanSiman.WASDALSROW_PNBP_SPAN_SIMAN dataTerpilih = null;
        public string kd_satker_pilih = "";
        decimal? id_satker_pilih = 0;
        public string ur_satker_pilih = "";
        public string skKeputusan = "";
        public string urPendapatan = "";
        public ValidasiSatkerSpan validasiSatkerSpan = null;

        public FrmValidasiSatkerSpan()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        public void init()
        {
            tePeriode.Text = dataTerpilih.PERIODE;
            teNTPN.Text = dataTerpilih.NTPN;
            teNTB.Text = dataTerpilih.NTB;
            teKdBilling.Text = dataTerpilih.KD_BILLING;
            deTgl.EditValue = dataTerpilih.TANGGAL;
            tePnbpSiman.EditValue = dataTerpilih.PNBP_SIMAN;
            tePnbpSpan.EditValue = dataTerpilih.PNBP_SPAN;
            teSelisih.EditValue = dataTerpilih.SELISIH;
            teUrAkun.Text = dataTerpilih.KD_AKUN;
            teSpan.Text = dataTerpilih.KD_SATKER_SPAN;
            teValidasi.Text = dataTerpilih.KD_SATKER;
        }

       

        public string periode(string tanggal)
        {
            string tgl = "";
            try
            {
                if (!string.IsNullOrEmpty(tanggal))
                {
                    if (!tanggal.Contains('-'))
                    {
                        tgl = tanggal.Substring(6);
                        int bulan = Convert.ToInt16(tanggal.Substring(3, 2));
                        if (bulan <= 6) tgl += "0" + bulan;
                        else tgl += bulan;

                    }
                    else tgl = "";
                }
                else tgl = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Konversi Periode Belum Berhasil");
            }
            return tgl;
        }

       
        private void sbPilihSatker_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(teSpan.Text))
            {
                frmPuSatker frm = new frmPuSatker()
                {
                    idManualBaru = "",
                    kode_satker_span = teSpan.Text,
                    selectUnitKerja = new SelectSatkerPerbandingan(satkerValidasi)
                };
                frm.ShowDialog();
            }
            else MessageBox.Show(null,"Kode Satker SPAN belum terisi","Perhatian");
        }

        public void satkerValidasi(SvcSatkerSelect.BPSIMANSROW_R_SATKER satker_terpilih){
            teValidasi.Text = satker_terpilih.KD_SATKER;
            kd_satker_pilih = satker_terpilih.KD_SATKER;
            ur_satker_pilih = satker_terpilih.UR_SATKER;
            id_satker_pilih = satker_terpilih.ID_SATKER;
        }

        private void bbiSimpan_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadingWait = new DevExpress.XtraSplashScreen.SplashScreenManager();
            LoadingWait.ShowWaitForm();
            this.validasiSatkerSpan(dataTerpilih, kd_satker_pilih, ur_satker_pilih, meCatatan.Text);
            LoadingWait.CloseWaitForm();
            //this.Close();
        }

    }
}
