using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppPengguna.KEL
{
    public partial class ucDetailGrid : UserControl
    {
        public ucDetailGrid()
        {
            InitializeComponent();
        }

        public ucPilihAset pilihAset;
        public ucSuratPermohonan suratPermohonan;
        public ucKelengkapanDokumen kelengkapanDokumen;
        public ucDokumenAnalisis dokumenAnalisis;
        public ucAgenda agenda;
        public ucFaktorPengurang faktorPengurang;
        public ucNotifEmail notifEmail;


        public SvcKelMainSelect.DBKELOLASROW_GRID_KELOLA_ALL dataterpilih;

        public void inisialisasiForm()
        {
            konfigApp.noTiketKelola = dataterpilih.NO_TIKET_KELOLA;
            teNoTiket.Text = dataterpilih.NO_TIKET_KELOLA;
            teTglTiket.EditValue = dataterpilih.TGL_PROSES;
            teTujuanTiket.Text = dataterpilih.TUJUAN_SURAT;
            teThnAng.EditValue = dataterpilih.THN_ANGGARAN.ToString();
            rgJenisAset.SelectedIndex = (dataterpilih.IS_TB == "Y") ? 0 : 1;
            if (rgJenisAset.SelectedIndex == 0 || rgJenisAset.SelectedIndex == -1)
                lciPenetapan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            else
                lciPenetapan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            rgPenetapan.SelectedIndex = (dataterpilih.TETAPKAN_PENGGUNA_YN == "Y") ? 0 : 1;
            tePemohon.Text = dataterpilih.TIPE_PEMOHON;
            teTermohon.Text = dataterpilih.TIPE_PENGELOLA;
            teNilaiPerolehan.Text = dataterpilih.NILAI_PENETAPAN.ToString();
            teStatus.Text = dataterpilih.STATUS_MODE;
            teNoSurat.Text = dataterpilih.SK_KEPUTUSAN;
            teTglSurat.EditValue = dataterpilih.TGL_SK;
            teInstansiPenerbit.Text = dataterpilih.NM_INSTANSI;
            teUraian.Text = dataterpilih.URAIAN_KEPUTUSAN;
            teKuantitas.Text = dataterpilih.KUANTITAS.ToString();
            teLuas.Text = dataterpilih.LUAS_ASET.ToString();
            teNilaiPenetapan.Text = dataterpilih.NILAI_PENETAPAN.ToString();
            xtcDetail.SelectedTabPageIndex = 0;
        }

        public void setPanel(PanelControl pc, UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pc.Controls.Clear();
            pc.Controls.Add(uc);
        }

        private void xtcDetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == xtpPilihAset)
            {
                pilihAset = new ucPilihAset();
                setPanel(pcPilihAset, pilihAset);
                pilihAset.dataInisial = true;
                pilihAset.load();
            }
            else if (e.Page == xtpSuratPermohonan)
            {
                suratPermohonan = new ucSuratPermohonan();
                setPanel(pcSuratPermohonan, suratPermohonan);
                suratPermohonan.dataInisial = true;
                suratPermohonan.load();
            }
            else if (e.Page == xtpKelengkapanDokumen)
            {
                kelengkapanDokumen = new ucKelengkapanDokumen();
                setPanel(pcKelengkapanDokumen, kelengkapanDokumen);
                kelengkapanDokumen.dataInisial = true;
                kelengkapanDokumen.load();
            }
            else if (e.Page == xtpDokumenAnalisis)
            {
                dokumenAnalisis = new ucDokumenAnalisis();
                setPanel(pcDokumenAnalisis, dokumenAnalisis);
                dokumenAnalisis.dataInisial = true;
                dokumenAnalisis.load();
            }
            else if (e.Page == xtpWaktuPenyelesaian)
            {
                agenda = new ucAgenda();
                setPanel(pcAgenda, agenda);
                agenda.dataInisial = true;
                agenda.load();

                faktorPengurang = new ucFaktorPengurang();
                setPanel(pcFaktorPengurang, faktorPengurang);
                faktorPengurang.dataInisial = true;
                faktorPengurang.load();
            }
            else if (e.Page == xtpNotifikasi)
            {
                notifEmail = new ucNotifEmail();
                setPanel(pcNotifikasi, notifEmail);
                notifEmail.dataInisial = true;
                notifEmail.load();
            }

        }

        private void rgJenisAset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgJenisAset.SelectedIndex == 0 || rgJenisAset.SelectedIndex == -1)
                lciPenetapan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            else
                lciPenetapan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
    }
}
