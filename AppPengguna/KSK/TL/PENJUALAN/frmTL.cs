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

namespace AppPengguna.KSK.TL.PENJUALAN  
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

        SvcWasdalManfaatDetailCrud.execute_pttClient svcWlPSPBMNLAINDetailCrud = null;
        SvcWasdalManfaatDetailCrud.OutputParameters dataoutWlPSPBMNLAINDetailCrud = null;

        ucRTLJUALForm ucPBMNS;
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
            set { teFile.Text = value; }
            get { return teFile.Text; }
        }
        public string NO_BUKTI_LAKSANA
        {
            set { teFile.Text = value; }
            get { return teFile.Text; }
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
            set { teNilaiPenetapan.EditValue = value; }
            get { return Convert.ToDecimal(teNilaiPenetapan.EditValue); }
        }
        public string file
        {
            set { teFile.Text = value; }
            get { return teFile.Text; }
        }

        public frmTL(ucRTLJUALForm _ucPSPBMNLAIN)
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

        private void frmTL_Load(object sender, EventArgs e)
        {
            //this.getJnsDok();
            this.getStatus();

            teNoAset.Text = ""+0;
            teKdBrg.Text = KD_BRG;
            teUrBrg.Text = UR_SSKEL;
            //teNilaiPersetujuan = null;
            teTglSK.Text = ""+0;
            //teNmPhkLn.Text = pihakLain;
            //teJnkWkt.Text = JANGKA_WAKTU.ToString();
            //deMulai.EditValue = DARI_TGL;
            //deSampai.EditValue = SD_TGL;
            teKet.Text = null;
            teFile.Text = null;
        }

        private void getStatus()
        {

            svcStatus = new SvcStatusSelect.svcDsRStatusSelect_pttClient();
            SvcStatusSelect.InputParameters input = new SvcStatusSelect.InputParameters();
            input.P_MINSpecified = true;
            input.P_MIN = konfigApp.currentMin;
            input.P_MAXSpecified = true;
            input.P_MAX = konfigApp.maksReferensi;
            input.P_COL = "";
            input.P_SORT = "";
            input.STR_WHERE = "((KD_STATUS = '03')OR(KD_STATUS='04')OR(KD_STATUS='99'))";
            outputStatus = new SvcStatusSelect.OutputParameters();
            outputStatus = svcStatus.execute(input);
            //leStatus.Properties.DataSource = outputStatus.SF_ROW_R_STATUS;
        }

        private void getJnsDok()
        {

            svcJnsDok = new SvcJnsDokSelect.dsRJnsDokSelect_pttClient();
            SvcJnsDokSelect.InputParameters input = new SvcJnsDokSelect.InputParameters();
            input.P_MINSpecified = true;
            input.P_MIN = konfigApp.currentMin;
            input.P_MAXSpecified = true;
            input.P_MAX = konfigApp.maksReferensi;
            input.P_COL = "";
            input.P_SORT = "";
            outputJnsDok = new SvcJnsDokSelect.OutputParameters();
            outputJnsDok = svcJnsDok.execute(input);
     //       teDokPelaksana.Properties.DataSource = outputJnsDok.SF_ROW_R_JNS_DOK;

        }

        #region CRUD ASET
        public void crudWlDetailPSPBMNLAIN()
        {
           try
            {
                this.setThread(true);

                SvcWasdalManfaatDetailCrud.InputParameters parInp = new SvcWasdalManfaatDetailCrud.InputParameters();

                parInp.P_DARI_TGL = konfigApp.DateToString(DARI_TGL);
                parInp.P_ID_ASET = idAset.ToString();
                parInp.P_JANGKA_WAKTU = JANGKA_WAKTU;
                parInp.P_JANGKA_WAKTUSpecified = true;
                parInp.P_JNS_BUKTI_LAKSANA = JNS_BUKTI_LAKSANA;
                parInp.P_KD_AKUN = KD_AKUN;
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
                parInp.P_PERIODE = PERIODE;
                parInp.P_SD_TGL = konfigApp.DateToString(SD_TGL);
                parInp.P_SK_KEPUTUSAN = teNoSK.Text;
                parInp.P_TGL_BUKTI_LAKSANA = konfigApp.DateToString(TGL_BUKTI_LAKSANA);
                parInp.P_TGL_SETOR = konfigApp.DateToString(TGL_SETOR);
                parInp.P_TGL_TRANSAKSI = konfigApp.DateToString(TGL_TRANSAKSI);
                
                parInp.P_SELECT = "U";

                svcWlPSPBMNLAINDetailCrud = new SvcWasdalManfaatDetailCrud.execute_pttClient();
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

        private delegate void ResponWlPSPBMNLAINDetailCrud(SvcWasdalManfaatDetailCrud.OutputParameters dataoutWlDetailCrud);
        private void responWlPSPBMNLAINDetailCrud(SvcWasdalManfaatDetailCrud.OutputParameters dataoutWlDetailCrud)
        {
            if (dataoutWlDetailCrud.PO_RESULT == "Y")
            {

               // ucPBMNS.tabSelect();
                this.Close();
            }
            else
            {
                this.setThread(false);
                MessageBox.Show(dataoutWlDetailCrud.PO_RESULT_MESSAGE, konfigApp.judulGagal);
            }
        }
        
        #endregion

        private void sbSave_Click(object sender, EventArgs e)
        {

            crudWlDetailPSPBMNLAIN();
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void leStatus_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //teGunaWasdal.Text = outputStatus.SF_ROW_R_STATUS[leStatus.ItemIndex].GUNA_WASDAL;
            }
            catch(Exception exp){
            
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {

                file = openFileDialog1.FileName;
            }
        }


    }
}