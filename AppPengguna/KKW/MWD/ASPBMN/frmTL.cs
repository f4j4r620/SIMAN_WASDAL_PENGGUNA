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

namespace AppPengguna.KKW.MWD.ASPBMN
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
        SvcWasdalOtherDetailCud.execute_pttClient svcWlPSPBMNLAINDetailCrud = null;
        SvcWasdalOtherDetailCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud = null;

        ucASPBMN ucPBMNS;
        private Thread myThread;

        #region SET & GET DATA FORM

        public string skKeputusan { get; set; }
        public string kdStatus { get; set; }

        public decimal? luas { get; set; }

        public decimal? KUANTITAS { get; set; }


        public string KD_SATKER
        {
            set { teKdSatker.Text = value; }
            get { return teKdSatker.Text; }
        }
        public string UR_SATKER
        {
            set { teNmSatker.Text = value; }
            get { return teNmSatker.Text; }
        }
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
        public string NUP
        {
            set { teNoAset.Text = value; }
            get { return teNoAset.Text; }
        }
        public string NO_SK
        {
            set { teNoSK.Text = value; }
            get { return teNoSK.Text; }
        }
        public DateTime? TGL_mulai
        {
            set;
            get;
        }
        public DateTime? TGL_selesai
        {
            set;
            get;
        }
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
        public string file
        {
            set { teFile.Text = value; }
            get { return teFile.Text; }
        }
        public decimal? JangkaWaktu
        {
            set;
            get;
        }
        public string Periode
        {
            set;
            get;
        }
        public decimal? NILAI_PELAKSANAAN
        {
            set;
            get;
        }
        public string pihakketiga
        {
            set { teNamaPihakKetiga.Text = value; }
            get { return teNamaPihakKetiga.Text; }
        }

        //public void SETPERIODE(string kd)
        //{
        //    if (kd.Equals("H"))
        //        cbPeriode.SelectedIndex = 0;
        //    else if (kd.Equals("M"))
        //        cbPeriode.SelectedIndex = 1;
        //    else if (kd.Equals("B"))
        //        cbPeriode.SelectedIndex = 2;
        //    else if (kd.Equals("T"))
        //        cbPeriode.SelectedIndex = 3;
        //}

        //private string GETPERIODE()
        //{
        //    string kode = null;
        //    if (cbPeriode.SelectedIndex == 0)
        //    {
        //        kode = "H";
        //    }
        //    else if (cbPeriode.SelectedIndex == 1)
        //    {
        //        kode = "M";
        //    }
        //    else if (cbPeriode.SelectedIndex == 2)
        //    {
        //        kode = "B";
        //    }
        //    else if (cbPeriode.SelectedIndex == 3)
        //    {
        //        kode = "T";
        //    }
        //    return kode;
        //}

        #endregion

        public frmTL(ucASPBMN _ucPSPBMNLAIN)
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            this.ucPBMNS = _ucPSPBMNLAIN;
        }

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
            this.getStatus();
        }

        #region LOAD STATUS
        private void getStatus()
        {
            try
            {
                svcStatus = new SvcStatusSelect.svcDsRStatusSelect_pttClient();
                SvcStatusSelect.InputParameters input = new SvcStatusSelect.InputParameters();
                input.P_MINSpecified = true;
                input.P_MIN = konfigApp.currentMin;
                input.P_MAXSpecified = true;
                input.P_MAX = konfigApp.maksReferensi;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = " (KD_STATUS = '06')";
                outputStatus = new SvcStatusSelect.OutputParameters();
                svcStatus.Beginexecute(input, new AsyncCallback(getDataStatus), null);
            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
            }

        }
        private void getDataStatus(IAsyncResult result)
        {
            try
            {
                outputStatus = svcStatus.Endexecute(result);
                svcStatus.Close();
                this.setThread(false);
                this.Invoke(new LoadDataStatus(this.loadDataStatus), outputStatus);
            }
            catch
            {
                this.setThread(false);
            }
        }
        private delegate void LoadDataStatus(SvcStatusSelect.OutputParameters outputStatus);
        private void loadDataStatus(SvcStatusSelect.OutputParameters outputStatus)
        {
            leStatus.Properties.DataSource = outputStatus.SF_ROW_R_STATUS;
            if (kdStatus == "-")
            {
                leStatus.EditValue = "06";
            }
            else
            {
                leStatus.EditValue = kdStatus;
                //teGunaWasdal.Text = gunaWasdal;
            }
        }
        #endregion


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

                SvcWasdalOtherDetailCud.InputParameters parInp = new SvcWasdalOtherDetailCud.InputParameters();

                parInp.P_DARI_TGL = konfigApp.DateToString(TGL_mulai);
                //parInp.P_FILE_BUKTI = FILE_BUKTI;
                parInp.P_ID_ASET = NUP.ToString();
                parInp.P_JANGKA_WAKTU = JangkaWaktu;
                parInp.P_JANGKA_WAKTUSpecified = true;
                parInp.P_JNS_BUKTI_LAKSANA = JNS_BUKTI_LAKSANA;
                parInp.P_KD_STATUS = kdStatus;
                parInp.P_KET = KETERANGAN;
                parInp.P_KUANTITAS = KUANTITAS;
                parInp.P_KUANTITASSpecified = true;
                parInp.P_NILAI_PERSETUJUAN = NILAI_PELAKSANAAN;
                parInp.P_NILAI_PERSETUJUANSpecified = true;
                parInp.P_NM_PHK_LAIN = pihakketiga;
                parInp.P_NO_BUKTI_LAKSANA = NO_BUKTI_LAKSANA;
                parInp.P_SD_TGL = konfigApp.DateToString(TGL_selesai);
                parInp.P_SK_KEPUTUSAN = teNoSK.Text;
                parInp.P_TGL_BUKTI_LAKSANA = konfigApp.DateToString(TGL_BUKTI_LAKSANA);
                parInp.P_SELECT = "U";

                svcWlPSPBMNLAINDetailCrud = new SvcWasdalOtherDetailCud.execute_pttClient();
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

        private delegate void ResponWlPSPBMNLAINDetailCrud(SvcWasdalOtherDetailCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud);
        private void responWlPSPBMNLAINDetailCrud(SvcWasdalOtherDetailCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud)
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