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
    public partial class frmMutasi : DevExpress.XtraEditors.XtraForm
    {
        
        private string Status;
        public frmMutasi(SvcSjrhRwyMutasiSelect.BPSIMANSROW_SEJARAH_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcKDPMutasiSelect.BPSIMANSROW_KDP_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcRwyMutRenovSelect.BPSIMANSROW_KRNV_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcPersediaanRwytMutasi.BPSIMANSROW_SEDIA_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcATBMutSelect.BPSIMANSROW_KTWJD_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcLnyMutSelect.BPSIMANSROW_KLAIN_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcProkRwytMutasi.BPSIMANSROW_KPROK_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcBgnAirMutasi.BPSIMANSROW_M_KBAIR_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcRwyMutJlnJmbtnSelect.BPSIMANSROW_KJALJ_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcRwyMutRmhNgrSelect.BPSIMANSROW_M_KRMH_NEG_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcRmutasiBangunanSelect.BPSIMANSROW_M_KBDG_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcSenjataRmutasiSelect.BPSIMANSROW_KSENJ_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcMPKTRwytMutasi.BPSIMANSROW_KKTIK_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcMesinPntRmutasiSelect.BPSIMANSROW_KALB_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcTnhRwyMutasiSelect.BPSIMANSROW_M_KTNH_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        public frmMutasi(SvcRwyMutAngkSelect.BPSIMANSROW_KANGK_RWYT_MUTASI selectedData, string _status)
        {
            InitializeComponent();
            this.Status = _status;
            this.teIntra_Ekstra.Text = selectedData.INTRA_EXTRA;
            this.teJNS_Transaksi.Text = selectedData.JNS_TRN;
            this.teKeterangan.Text = selectedData.KET;
            this.teKuantitas.Value = (decimal)((selectedData.KUANTITAS == null) ? 0 : selectedData.KUANTITAS);
            this.teNilai.Value = (decimal)((selectedData.NILAI == null) ? 0 : selectedData.NILAI);
            this.teNoDasar.Text = selectedData.NO_DSR_MUTASI;
            this.teSatuan.Text = selectedData.SATUAN;
            this.teTglBuku.Text = konfigApp.DateToString(selectedData.TGL_BUKU);
            this.teTglMutasi.Text = konfigApp.DateToString(selectedData.TGL_DSR_MUTASI);
            this.teUraianTransaksi.Text = selectedData.UR_TRN;
        }
        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmMutasi_Load(object sender, EventArgs e)
        {
            if (this.Status == "edit" || this.Status == "input")
            {

                this.FormReadOnly(true);

                this.bbiClose.Enabled = true;

            }
            else
            {
                
                this.FormReadOnly(true);

            }
        }

        private void ReadOnly(Control c, bool state)
        {
            if (c is TextEdit)
                (c as TextEdit).Properties.ReadOnly = state;
            else if (c is SimpleButton)
                (c as SimpleButton).Enabled = !state;
            else if (c is PictureEdit)
                (c as PictureEdit).Properties.ReadOnly = state;
            else if (c is SpinEdit)
                (c as SpinEdit).Properties.ReadOnly = state;
        }
        private void FormReadOnly(bool state)
        {

            foreach (Control c in this.LayoutData.Controls)
            {
                this.ReadOnly(c, state);

            }
            

        }
    }
}