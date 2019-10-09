using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using AppPengguna.KSK.MWD.ASPBMN;

namespace AppPengguna.KKW.MWD.KPBMN
{
    public delegate void SetEnabledForm(bool enabled);
    public delegate void SetProgressBar(BarItemVisibility str);
    public delegate void SetProgressBarImage(LayoutVisibility str);
    public partial class frmTL : DevExpress.XtraEditors.XtraForm

    {
        SvcJnsDokSelect.dsRJnsDokSelect_pttClient svcJnsDok;
        SvcJnsDokSelect.OutputParameters outputJnsDok;
        SvcStatusSelect.svcDsRStatusSelect_pttClient svcStatus;
        SvcStatusSelect.OutputParameters outputStatus;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalManfaatAllCud.call_pttClient svcWlPSPBMNLAINDetailCrud = null;
        SvcWasdalManfaatAllCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud = null;

        ucKPBMN ucPBMNS;
        private Thread myThread;
        
        #region SET & GET DATA FORM

        public decimal? idAset { get; set; }
        public string skKeputusan { get; set; }
        public string kdStatus { get; set; }
        public decimal? luas { get; set; }

        public decimal? KUANTITAS { get; set; }
        

        public string KD_SATKER                               { get; set; }
        public string UR_SATKER                               { get; set; }
        public string KD_BRG
        {
            set { teKdBrg.Text = value; }
            get { return teKdBrg.Text; }
        }
        public string UR_SSKEL
        {
            set { teUrBrg.Text = value; }
            get { return teUrBrg.Text; }
        }
        public string NUP                                     { get; set; }
        public string NO_SK                                   { get; set; }
        public DateTime? TGL_SK
        {
            set { teTglSK.EditValue = value; }
            get { return Convert.ToDateTime(teTglSK.EditValue); }
        }
        public string JNS_BUKTI_LAKSANA
        {
            set { teJnsBukti.Text = value; }
            get { return teJnsBukti.Text; }
        }
        public string NO_BUKTI_LAKSANA
        {
            set { teNoBukti.Text = value; }
            get { return teNoBukti.Text; }
        }
        public DateTime? TGL_BUKTI_LAKSANA
        {
            set { deTglBukti.EditValue = value; }
            get { return Convert.ToDateTime(deTglBukti.EditValue); }
        }
        public string NM_PHK_KETIGA
        {
            set { teNamaPihakKetiga.Text = value; }
            get { return teNamaPihakKetiga.Text; }
        }
        public string NPWP_PHK_KETIGA
        {
            set { teNpwp.Text = value; }
            get { return teNpwp.Text; }
        }
        public decimal? JANGKA_WAKTU
        {
            set { teJangkaWaktu.EditValue = value; }
            get { return Convert.ToDecimal(teJangkaWaktu.EditValue); }
        }
        public string PERIODE
        {
            set { tePeriode.Text = value; }
            get { return tePeriode.Text; }
        }
        public DateTime? DARI_TGL
        {
            set { deMulai.EditValue = value; }
            get { return Convert.ToDateTime(deMulai.EditValue); }
        }
        public DateTime? SD_TGL
        {
            set { deSelesai.EditValue = value; }
            get { return Convert.ToDateTime(deSelesai.EditValue); }
        }
        public Decimal? NILAI_PELAKSANAAN
        {
            set { teNilaiPenetapan.EditValue = value; }
            get { return Convert.ToDecimal(teNilaiPenetapan.EditValue); }
        }
        public string KETERANGAN
        {
            set { teKet.Text = value; }
            get { return teKet.Text; }
        }
        public string FILE_BUKTI
        {
            set { teFile.Text = value; }
            get { return teFile.Text; }
        }
        public string NTPN
        {
            set { teNTPN.Text = value; }
            get { return teNTPN.Text; }
        }
        public DateTime? TGL_SETOR
        {
            set { deTglSetor.EditValue = value; }
            get { return Convert.ToDateTime(deTglSetor.EditValue); }
        }
        public string NTB
        {
            set { teNTB.Text = value; }
            get { return teNTB.Text; }
        }
        public DateTime? TGL_TRANSAKSI
        {
            set { deTglTransaksi.EditValue = value; }
            get { return Convert.ToDateTime(deTglTransaksi.EditValue); }
        }
        public string NM_PENYETOR
        {
            set { teNmPenyetor.Text = value; }
            get { return teNmPenyetor.Text; }
        }
        public string KD_AKUN
        {
            set { teKdAkun.Text = value; }
            get { return teKdAkun.Text; }
        }
        public string UR_AKUN
        {
            set { teNmAkun.Text = value; }
            get { return teNmAkun.Text; }
        }
        public decimal? NILAI_PNBP
        {
            set { teNilaiPNBP.EditValue = value; }
            get { return Convert.ToDecimal(teNilaiPNBP.EditValue); }
        }
        public string file
        {
            set { teFile.Text = value; }
            get { return teFile.Text; }
        }
        
        public frmTL(ucKPBMN _ucPSPBMNLAIN)
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            this.ucPBMNS = _ucPSPBMNLAIN;
        }
        
        #endregion
        
        #region THREAD

        public void setProgressBar(BarItemVisibility str)
        {
            if (this.InvokeRequired)
            {
                SetProgressBar p = new SetProgressBar(setProgressBar);
                this.Invoke(p, new object[] { str });
            }
            else
            {
                this.beMarqueeBar.Visibility = str;
            }
        }
        public void setEnabledForm(bool enabled)
        {
            this.Enabled = enabled;
        }
        public void showProgress()
        {
            if (this.IsHandleCreated)
            {
                this.setProgressBar(BarItemVisibility.Always);
            }
        }
        public void setThread(bool start)
        {
            try
            {
                if (start == true)
                {
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new SetEnabledForm(this.setEnabledForm), false);
                        }
                        else
                        {
                            this.setEnabledForm(false);
                        }
                    }
                    myThread = new Thread(new ThreadStart(showProgress));
                    myThread.Start();
                }
                else
                {
                    myThread.Abort();
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new SetProgressBar(this.setProgressBar), BarItemVisibility.Never);
                            this.Invoke(new SetEnabledForm(this.setEnabledForm), true);
                        }
                    }
                    else
                    {
                        this.setProgressBar(BarItemVisibility.Never);
                        this.setEnabledForm(true);
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void setProgressBarImage(LayoutVisibility str)
        {
            if (this.InvokeRequired)
            {
                SetProgressBarImage p = new SetProgressBarImage(setProgressBarImage);
                this.Invoke(p, new object[] { str });
            }
            else
            {
            }
        }
        public void showProgressImage()
        {
            if (this.IsHandleCreated)
            {
                this.setProgressBarImage(LayoutVisibility.Always);
            }
        }
        public void setThreadImage(bool start)
        {
            try
            {
                if (start == true)
                {
                    myThread = new Thread(new ThreadStart(showProgressImage));
                    myThread.Start();
                }
                else
                {
                    myThread.Abort();
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new SetProgressBarImage(this.setProgressBarImage), LayoutVisibility.Never);
                        }
                    }
                    else
                    {
                        this.setProgressBarImage(LayoutVisibility.Never);
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        #endregion
        


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


        private void frmTL_Load(object sender, EventArgs e)
        {
            //teNoAset.Text = ""+0;
            teKdBrg.Text = KD_BRG;
            teUrBrg.Text = UR_SSKEL;
            //teNilaiPersetujuan = null;
            //teTglSK.Text = ""+0;
            //teNmPhkLn.Text = pihakLain;
            //teJnkWkt.Text = JANGKA_WAKTU.ToString();
            //deMulai.EditValue = DARI_TGL;
            //deSampai.EditValue = SD_TGL;
            //teKet.Text = null;
            //teFile.Text = null;
        }

        private string periode()
        {
            string per = null;

            if (tePeriode.SelectedIndex == 0)
            {
                per = "H";
            }
            else if (tePeriode.SelectedIndex == 1)
            {
                per = "M";
            } if (tePeriode.SelectedIndex == 2)
            {
                per = "B";
            } if (tePeriode.SelectedIndex == 3)
            {
                per = "T";
            }

            return per;
        }

        #region CRUD ASET
        public void crudWlDetailPSPBMNLAIN()
        {
            try
            {
                this.setThread(true);

                SvcWasdalManfaatAllCud.InputParameters parInp = new SvcWasdalManfaatAllCud.InputParameters();

                parInp.P_DARI_TGL = konfigApp.DateToString(DARI_TGL);
                parInp.P_FILE_BUKTI = FILE_BUKTI;
                parInp.P_ID_ASET = idAset.ToString();
                parInp.P_JANGKA_WAKTU = JANGKA_WAKTU;
                parInp.P_JANGKA_WAKTUSpecified = true;
                parInp.P_JNS_BUKTI_LAKSANA = JNS_BUKTI_LAKSANA;
                parInp.P_KD_AKUN = (KD_AKUN != "-") ? KD_AKUN : null;
                parInp.P_KD_STATUS = kdStatus;
                parInp.P_KET = KETERANGAN;
                parInp.P_KUANTITAS = KUANTITAS ;
                parInp.P_KUANTITASSpecified = true;
                parInp.P_LUAS_LAYANAN = luas;
                parInp.P_LUAS_LAYANANSpecified = true;
                parInp.P_NILAI_PERSETUJUAN = NILAI_PELAKSANAAN;
                parInp.P_NILAI_PERSETUJUANSpecified = true;
                parInp.P_NILAI_PNBP = NILAI_PNBP;
                parInp.P_NILAI_PNBPSpecified = true;
                parInp.P_NM_PENYETOR = NM_PENYETOR;
                parInp.P_NM_PHK_LAIN = NM_PHK_KETIGA;
                parInp.P_NO_BUKTI_LAKSANA = NO_BUKTI_LAKSANA;
                parInp.P_NPWP_PHK_LAIN = NPWP_PHK_KETIGA;
                parInp.P_NTB = NTB;
                parInp.P_NTPN = NTPN;
                parInp.P_PERIODE = this.periode(); 
                parInp.P_SD_TGL = konfigApp.DateToString(SD_TGL);
                parInp.P_SK_KEPUTUSAN = teNoSK.Text;
                parInp.P_TGL_BUKTI_LAKSANA = konfigApp.DateToString(TGL_BUKTI_LAKSANA);
                parInp.P_TGL_SETOR = konfigApp.DateToString(TGL_SETOR);
                parInp.P_TGL_TRANSAKSI = konfigApp.DateToString(TGL_TRANSAKSI);
                
                parInp.P_SELECT = "U";

                svcWlPSPBMNLAINDetailCrud = new SvcWasdalManfaatAllCud.call_pttClient();
                svcWlPSPBMNLAINDetailCrud.Beginexecute(parInp, new AsyncCallback(resultPSPBMNLAINDetailCrud), "");
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulGagalLain);
            }
        }

        private void resultPSPBMNLAINDetailCrud(IAsyncResult result)
        {
            try
            {
                dataoutWlPSPBMNLAINDetailCrud = svcWlPSPBMNLAINDetailCrud.Endexecute(result);
                svcWlPSPBMNLAINDetailCrud.Close();
                this.setThread(false);
                this.Invoke(new ResponWlPSPBMNLAINDetailCrud(this.responWlPSPBMNLAINDetailCrud), dataoutWlPSPBMNLAINDetailCrud);
            }
            catch (Exception e)
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalLain);
            }
        }

        private delegate void ResponWlPSPBMNLAINDetailCrud(SvcWasdalManfaatAllCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud);
        private void responWlPSPBMNLAINDetailCrud(SvcWasdalManfaatAllCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud)
        {
            if (dataoutWlPSPBMNLAINDetailCrud.PO_RESULT == "Y")
            {

                ucPBMNS.tabSelect();
                this.Close();
            }
            else
            {
                this.setThread(false);
                MessageBox.Show(dataoutWlPSPBMNLAINDetailCrud.PO_RESULT_MESSAGE, konfigApp.judulGagal);
            }
        }

        #endregion

        private void sbSave_Click(object sender, EventArgs e)
        {
            //if (teNmAkun.Text.Trim() != "")
                crudWlDetailPSPBMNLAIN();
            //else
                //MessageBox.Show("Kode dan Nama Akun harus diisi !");
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
            }
        }

        #region Cari Akun
        PU.frmPuAkun formAkun;

        private void sbCariAkun_Click(object sender, EventArgs e)
        {
            if (formAkun == null)
            {
                formAkun = new PU.frmPuAkun();
                formAkun.toggleProgressBar = new ToggleProgressBar(toggleProgBarPu);
                formAkun.selectUnitKerja = new SelectDataUnitKerja(setAkun);
                formAkun.whereAwal = " UPPER(KDMAKMAP) LIKE '4%' ";
            }
            formAkun.ShowDialog();
        }

        private void setAkun(decimal? _idAkun, string _kodeAkun, string _namaAkun, string _takAda1, string _takAda2)
        {
            teKdAkun.Text = _kodeAkun;
            teNmAkun.Text = _namaAkun;
        }
        #endregion

        private void teKdAkun_EditValueChanged(object sender, EventArgs e)
        {
            teNmAkun.Text = "";
        }
    }
}