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

namespace AppPengguna.KSK.TL.PPBMN
{

    public delegate void SetEnabledForm(bool enabled);
    public delegate void SetProgressBar(BarItemVisibility str);
    public delegate void SetProgressBarImage(LayoutVisibility str);

    public partial class frmTL : DevExpress.XtraEditors.XtraForm
    {
        SvcStatusSelect.svcDsRStatusSelect_pttClient svcStatus;
        SvcStatusSelect.OutputParameters outputStatus;
        public ToggleProgressBar toggleProgressBar;
        SvcWasdalOtherDetailCud.execute_pttClient svcWasdalOtherDetailCud = null;
        SvcWasdalOtherDetailCud.OutputParameters dataoutWlPBMNSDetailCrud = null;


        ucRTLPpBmnForm ucPPBMN;
        private Thread myThread;
       
        public string idAset { get; set; }
        public string skKeputusan { get; set; }
        public string kdStatus { get; set; }
        public string gunaWasdal { get; set; }

        public string KD_SATKER { get; set; }
        public string UR_SATKER { get; set; }
        public string KD_BRG { get; set; }
        public string UR_SSKEL { get; set; }
        public string NUP { get; set; }
        public string NO_SK { get; set; }
        public DateTime? TGL_SK { get; set; }
        public string JNS_BUKTI_LAKSANA { get; set; }
        public string NO_BUKTI_LAKSANA { get; set; }
        public DateTime? TGL_BUKTI_LAKSANA { get; set; }
        public string NM_PHK_LAIN { get; set; }
        public decimal? JANGKA_WAKTU { get; set; }
        public string PERIODE { get; set; }
        public DateTime? DARI_TGL { get; set; }
        public DateTime? SD_TGL { get; set; }
        private string daftarAset = "";

        public frmTL(ucRTLPpBmnForm ucPPBMN, string _daftarAset)
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            this.ucPPBMN = ucPPBMN;
            this.daftarAset = _daftarAset;

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
                //input.STR_WHERE = "((KD_STATUS = '02')OR(KD_STATUS='01'))";
                input.STR_WHERE = " ((KD_STATUS = '01') OR (KD_STATUS = '02') OR (KD_STATUS = '03') OR (KD_STATUS = '07') OR (KD_STATUS = '99'))";
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
                leStatus.EditValue = "02";
            }
            else
            {
                leStatus.EditValue = kdStatus;
                teGunaWasdal.Text = gunaWasdal;
            }
        }
        #endregion


        //private void getStatus()
        //{

        //    svcStatus = new SvcStatusSelect.call_pttClient();
        //    SvcStatusSelect.InputParameters input = new SvcStatusSelect.InputParameters();
        //    input.P_MINSpecified = true;
        //    input.P_MIN = konfigApp.currentMin;
        //    input.P_MAXSpecified = true;
        //    input.P_MAX = konfigApp.maksReferensi;
        //    input.P_COL = "";
        //    input.P_SORT = "";
        //    input.STR_WHERE = "((KD_STATUS = '02')OR(KD_STATUS='01')OR(KD_STATUS='99'))";
        //    outputStatus = new SvcStatusSelect.OutputParameters();
        //    outputStatus = svcStatus.execute(input);
        //    leStatus.Properties.DataSource = outputStatus.SF_ROW_R_STATUS;

        //}

        #region CRUD ASET
        public void crudWlDetailPSPBMN()
        {
            try
            {
                this.setThread(true);

                SvcWasdalOtherDetailCud.InputParameters parInp = new SvcWasdalOtherDetailCud.InputParameters();
                parInp.P_SK_KEPUTUSAN = this.skKeputusan;
                parInp.P_ID_ASET = idAset;
                //parInp.P_KUANTITAS = Convert.ToDecimal(teKuantitas.EditValue);
                parInp.P_KUANTITASSpecified = false;
                parInp.P_NILAI_PERSETUJUANSpecified = false;
                //parInp.P_NILAI_PERSETUJUAN = Convert.ToDecimal(teNilaiPersetujuan.EditValue); ;
                parInp.P_KD_STATUS = leStatus.EditValue.ToString();
                parInp.P_SELECT = "U";

                svcWasdalOtherDetailCud = new SvcWasdalOtherDetailCud.execute_pttClient();
                if (this.skKeputusan != "-")
                {
                    svcWasdalOtherDetailCud.Beginexecute(parInp, new AsyncCallback(resultPSPBMNDetailCrud), "");
                }
                else
                {
                    this.setThread(false);
                    MessageBox.Show("Gagal update sk karena aset belum memiliki sk keputusan");

                }

            }
            catch
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulGagalLain);
            }
        }

        private void resultPSPBMNDetailCrud(IAsyncResult result)
        {
            try
            {
                dataoutWlPBMNSDetailCrud = svcWasdalOtherDetailCud.Endexecute(result);
                svcWasdalOtherDetailCud.Close();
                this.setThread(false);
                this.Invoke(new ResponWlPSPBMNDetailCrud(this.responWlPSPBMNDetailCrud), dataoutWlPBMNSDetailCrud);

            }
            catch (Exception e)
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalLain);
            }
        }

        private delegate void ResponWlPSPBMNDetailCrud(SvcWasdalOtherDetailCud.OutputParameters dataoutWlPBMNSDetailCrud);
        private void responWlPSPBMNDetailCrud(SvcWasdalOtherDetailCud.OutputParameters dataoutWlPBMNSDetailCrud)
        {
            if (dataoutWlPBMNSDetailCrud.PO_RESULT == "Y")
            {

                //ucPPBMN.tabSelect();
                
                this.Close();
            }
            else
            {
                this.setThread(false);
                MessageBox.Show(dataoutWlPBMNSDetailCrud.PO_RESULT_MESSAGE, konfigApp.judulGagal);
            }
        }

        #endregion

        private void sbSave_Click(object sender, EventArgs e)
        {

            //crudWlDetailPSPBMN();
            if (MessageBox.Show(String.Format("Apakah anda ingin menindaklanjuti aset BMN terpilih?", ""), konfigApp.judulKonfirmasi,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    this.setThread(true);

                    SvcWasdalOtherDetailCud.InputParameters parInp = new SvcWasdalOtherDetailCud.InputParameters();
                    parInp.P_SK_KEPUTUSAN = this.skKeputusan;
                    parInp.P_ID_ASET = daftarAset;
                    parInp.P_KUANTITASSpecified = false;
                    parInp.P_NILAI_PERSETUJUANSpecified = false;
                    parInp.P_KD_STATUS = leStatus.EditValue.ToString();
                    parInp.P_SELECT = "U";
                    //parInp.P_IS_ALLBMN = "N";
                    
                    svcWasdalOtherDetailCud = new SvcWasdalOtherDetailCud.execute_pttClient();
                    if (this.skKeputusan != "-")
                    {
                        svcWasdalOtherDetailCud.Beginexecute(parInp, new AsyncCallback(resultPSPBMNDetailCrud), "");
                    }
                    else
                    {
                        this.setThread(false);
                        MessageBox.Show("Gagal update sk karena aset belum memiliki sk keputusan");

                    }

                }
                catch
                {
                    this.setThread(false);
                    MessageBox.Show(konfigApp.teksGagalLain, konfigApp.judulGagalLain);
                }
            }
          
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void leStatus_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                teGunaWasdal.Text = outputStatus.SF_ROW_R_STATUS[leStatus.ItemIndex].GUNA_WASDAL;
            }
            catch(Exception exp){
            
            }
        }


    }
}