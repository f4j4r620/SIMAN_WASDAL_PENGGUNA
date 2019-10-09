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
    public partial class FrmLapBandingSimanSpan : Form
    {
        public SvcGridBandingSpanSiman.WASDALSROW_PNBP_SPAN_SIMAN dataTerpilih;
        public FrmLapBandingSimanSpan()
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
        }

        private void bbiSimpan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            simpanDataRekapPNBP();
        }

        #region --++ Simpan Data RekapPNBP
        SvcPnbpValidasiSatker.OutputParameters dOutAmbilDataRekapPNBP;
        SvcPnbpValidasiSatker.execute_pttClient ambilDataRekapPNBP;

        private void simpanDataRekapPNBP()
        {
            try
            {
                SvcPnbpValidasiSatker.InputParameters parInp = new SvcPnbpValidasiSatker.InputParameters();
                parInp.P_CATATAN = meCatatan.Text;
                parInp.P_ID_SATKER = dataTerpilih.ID_SATKER;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_AKUN = teUrAkun.Text;
                parInp.P_KD_BILLING = teKdBilling.Text;
                parInp.P_KD_SATKER = teValidasi.Text;
                parInp.P_KD_SATKER_SPAN = dataTerpilih.KD_SATKER_SPAN;
                parInp.P_NIL_PNBP_SIMAN = Convert.ToDecimal(tePnbpSiman.EditValue);
                parInp.P_NIL_PNBP_SIMANSpecified = true;
                parInp.P_NIL_PNBP_SPAN = Convert.ToDecimal(tePnbpSpan.EditValue);
                parInp.P_NIL_PNBP_SPANSpecified = true;
                parInp.P_NTB = teNTB.Text;
                parInp.P_NTPN = teNTPN.Text;
                parInp.P_PERIODE = tePeriode.Text;
                parInp.P_SELISIH = Convert.ToDecimal(teSelisih.Text);
                parInp.P_SELISIHSpecified = true;
                parInp.P_SK_KEPUTUSAN = dataTerpilih.SK_KEPUTUSAN;
                parInp.P_TANGGAL = deTgl.DateTime;
                parInp.P_TANGGALSpecified = true;
                parInp.P_UR_PENDAPATAN = dataTerpilih.UR_PENDAPATAN;
                parInp.P_UR_SATKER = dataTerpilih.UR_SATKER;

                ambilDataRekapPNBP = new SvcPnbpValidasiSatker.execute_pttClient();
                ambilDataRekapPNBP.Open();
                ambilDataRekapPNBP.Beginexecute(parInp, new AsyncCallback(cudRekapPNBP), "");
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }
        }

        private void cudRekapPNBP(IAsyncResult result)
        {
            try
            {
                dOutAmbilDataRekapPNBP = ambilDataRekapPNBP.Endexecute(result);
                ambilDataRekapPNBP.Close();
                this.Invoke(new UbahDsRekapPNBP(this.ubahDsRekapPNBP), dOutAmbilDataRekapPNBP);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsRekapPNBP(SvcPnbpValidasiSatker.OutputParameters dataOutBongkaranCrud);

        private void ubahDsRekapPNBP(SvcPnbpValidasiSatker.OutputParameters dataOutBongkaranCrud)
        {
            if (dataOutBongkaranCrud.PO_RESULT == "Y")
            {
                this.Close();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        #endregion Simpan Data RekapPNBP

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
        }

    }
}
