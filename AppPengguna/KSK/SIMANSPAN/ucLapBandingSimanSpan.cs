using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KSK.SIMANSPAN
{
    public partial class ucLapBandingSimanSpan : UserControl
    {
        public ToggleProgressBar toggleProgressBar;
        public bool dataInisial = true;
        public ArrayList dsDataSource = null;
        public ArrayList dsDataSource2 = null;

        public detail refreshgc1;
        public detail refreshgc2;

        public ucLapBandingSimanSpan()
        {
            InitializeComponent();
            if (konfigApp.idGroup == 2)
            {
                gridColumn20.Visible = true;
                gridColumn21.Visible = true;
            }
            else
            {
                gridColumn20.Visible = false;
                gridColumn21.Visible = false;
            }
        }

        public void displayData()
        {
            if (dataInisial == true)
            {
                gridControl1.DataSource = null;
                gridControl1.DataSource = dsDataSource;
            }
            else
            {
                gridControl1.RefreshDataSource();
            }
            gridView1.BestFitColumns();
        }

        public void displayData2()
        {
            if (dataInisial == true)
            {
                gridControl2.DataSource = null;
                gridControl2.DataSource = dsDataSource2;
            }
            else
            {
                gridControl2.RefreshDataSource();
            }
            gridView2.BestFitColumns();
        }

        SvcPnbpValidasiSatker.OutputParameters dOutAmbilValidasiPNBP;
        SvcPnbpValidasiSatker.execute_pttClient ambilValidasiPNBP;

        public void ValidasiSatker(SvcGridBandingSpanSiman.WASDALSROW_PNBP_SPAN_SIMAN rowTerpilih, string kd_satker = "", string ur_satker = "", string cat = "")
        {
            try
            {
                SvcPnbpValidasiSatker.InputParameters parInp = new SvcPnbpValidasiSatker.InputParameters();
                parInp.P_CATATAN = cat;
                parInp.P_ID_SATKER = rowTerpilih.ID_SATKER;
                parInp.P_ID_SATKERSpecified = true;
                parInp.P_KD_AKUN = rowTerpilih.KD_AKUN;
                parInp.P_KD_BILLING = rowTerpilih.KD_BILLING;
                parInp.P_KD_SATKER = (kd_satker == "") ? rowTerpilih.KD_SATKER : kd_satker;
                parInp.P_KD_SATKER_SPAN = rowTerpilih.KD_SATKER_SPAN;
                parInp.P_NIL_PNBP_SIMAN = rowTerpilih.PNBP_SIMAN;
                parInp.P_NIL_PNBP_SIMANSpecified = true;
                parInp.P_NIL_PNBP_SPAN = rowTerpilih.PNBP_SPAN;
                parInp.P_NIL_PNBP_SPANSpecified = true;
                parInp.P_NTB = rowTerpilih.NTB;
                parInp.P_NTPN = rowTerpilih.NTPN;
                parInp.P_PERIODE = rowTerpilih.PERIODE;
                parInp.P_SELISIH = rowTerpilih.SELISIH;
                parInp.P_SELISIHSpecified = true;
                parInp.P_SK_KEPUTUSAN = rowTerpilih.SK_KEPUTUSAN;
                parInp.P_TANGGAL = rowTerpilih.TANGGAL;
                parInp.P_TANGGALSpecified = true;
                parInp.P_UR_PENDAPATAN = rowTerpilih.UR_PENDAPATAN;
                parInp.P_UR_SATKER = (ur_satker == "") ? rowTerpilih.UR_SATKER : ur_satker;

                ambilValidasiPNBP = new SvcPnbpValidasiSatker.execute_pttClient();
                ambilValidasiPNBP.Open();
                ambilValidasiPNBP.Beginexecute(parInp, new AsyncCallback(cudValidasiSatker), "");
            }
            catch
            {
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalSimpan);
            }

        }

        private void cudValidasiSatker(IAsyncResult result)
        {
            try
            {
                dOutAmbilValidasiPNBP = ambilValidasiPNBP.Endexecute(result);
                ambilValidasiPNBP.Close();
                this.Invoke(new UbahDsValidasi(this.ubahDsValidasi), dOutAmbilValidasiPNBP);
            }
            catch
            {
                MessageBox.Show(konfigApp.teksDialog, konfigApp.judulGagalLain);
            }
        }

        private delegate void UbahDsValidasi(SvcPnbpValidasiSatker.OutputParameters dOutAmbilValidasiPNBP);

        private void ubahDsValidasi(SvcPnbpValidasiSatker.OutputParameters dOutAmbilValidasiPNBP)
        {
            if (dOutAmbilValidasiPNBP.PO_RESULT == "Y")
            {
                dataInisial = true;
                //displayData();
                //displayData2();
                refreshgc1("");
                refreshgc2("");
                frm.Dispose();
            }
            else MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulKonfirmasi);
        }
        FrmValidasiSatkerSpan frm;
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SvcGridBandingSpanSiman.WASDALSROW_PNBP_SPAN_SIMAN rowTerpilih = (SvcGridBandingSpanSiman.WASDALSROW_PNBP_SPAN_SIMAN)gridView2.GetRow(gridView2.FocusedRowHandle);
            frm = new FrmValidasiSatkerSpan()
            {
                dataTerpilih = rowTerpilih,
                validasiSatkerSpan = new ValidasiSatkerSpan(ValidasiSatker)
            };
            if (rowTerpilih.STATUS.Contains("TIDAK ADA DI SIMAN"))
            {
                AutoClosingMessageBox.Show("Silahkan lengkapi data di menu Perekaman PNBP", "Data Tidak Ada di SIMAN", 3000);
            }
            else if (rowTerpilih.STATUS.Contains("TIDAK ADA DI SPAN"))
            {
                AutoClosingMessageBox.Show("Data PNBP SPAN belum lengkap", "Data Tidak Ada di SPAN", 3000);
            }
            else if (rowTerpilih.STATUS.Contains("SAMA"))
            {
                frm.init();
                frm.ShowDialog();
            }
            
        }

        private void sbExport1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls, *.xlsx";
            //DialogResult dr = sfd.ShowDialog();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gridView1.BestFitColumns();
                gridControl1.ExportToXlsx(sfd.FileName);
            }
        }

        private void sbExport2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls, *.xlsx";
            //DialogResult dr = sfd.ShowDialog();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gridView2.BestFitColumns();
                gridControl2.ExportToXlsx(sfd.FileName);
            }
        }

        private void sbRefresh1_Click(object sender, EventArgs e)
        {
            refreshgc1("");
        }

        private void sbRefresh2_Click(object sender, EventArgs e)
        {
            refreshgc2("");
        }

        private void sbMore1_Click(object sender, EventArgs e)
        {
            refreshgc1("false");
        }

        private void sbMore2_Click(object sender, EventArgs e)
        {
            refreshgc2("false");
        }

        
    }
}
