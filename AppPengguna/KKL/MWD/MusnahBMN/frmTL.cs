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
using AppPengguna.KKL.MWD.PMBMN;

namespace AppPengguna.KKL.MWD.MusnahBMN
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
        SvcWasdalHapusDetilCud.execute_pttClient svcWasdalHapusDetilCud = null;
        SvcWasdalHapusDetilCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud = null;

        ucMusnahBMN ucmusnahBMN;
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
        public string KETERANGAN
        {
            set { teKeterangan.Text = value; }
            get { return teKeterangan.Text; }
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
        public decimal? NlaiPenetapan
        {
            set;
            get;
        }
        public frmTL(ucMusnahBMN _ucMusnahBMN)
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
            this.ucmusnahBMN = _ucMusnahBMN;
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
            
        }

        #region CRUD ASET
        public void crudWlDetailPSPBMNLAIN()
        {
            try
            {
                this.setThread(true);

                SvcWasdalHapusDetilCud.InputParameters parInp = new SvcWasdalHapusDetilCud.InputParameters();

                
                parInp.P_ID_ASET = idAset.ToString();
                parInp.P_JNS_BUKTI_LAKSANA = JNS_BUKTI_LAKSANA;
                parInp.P_KET = KETERANGAN;
                parInp.P_KUANTITAS = KUANTITAS;
                parInp.P_KUANTITASSpecified = true;
                parInp.P_NO_BUKTI_LAKSANA = NO_BUKTI_LAKSANA;
                parInp.P_SK_KEPUTUSAN = teNoSK.Text;
                parInp.P_TGL_BUKTI_LAKSANA = TGL_BUKTI_LAKSANA.ToString();
                
                parInp.P_NILAI_PENETAPAN = NlaiPenetapan;
                parInp.P_NILAI_PENETAPANSpecified = true;
                
                parInp.P_SELECT = "U";

                svcWasdalHapusDetilCud = new SvcWasdalHapusDetilCud.execute_pttClient();
                svcWasdalHapusDetilCud.Beginexecute(parInp, new AsyncCallback(resultPSPBMNLAINDetailCrud), "");
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
                dataoutWlPSPBMNLAINDetailCrud = svcWasdalHapusDetilCud.Endexecute(result);
                svcWasdalHapusDetilCud.Close();
                this.setThread(false);
                this.Invoke(new ResponWlPSPBMNLAINDetailCrud(this.responWlPSPBMNLAINDetailCrud), dataoutWlPSPBMNLAINDetailCrud);
            }
            catch (Exception e)
            {
                this.setThread(false);
                MessageBox.Show(konfigApp.teksGagalSimpan, konfigApp.judulGagalLain);
            }
        }

        private delegate void ResponWlPSPBMNLAINDetailCrud(SvcWasdalHapusDetilCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud);
        private void responWlPSPBMNLAINDetailCrud(SvcWasdalHapusDetilCud.OutputParameters dataoutWlPSPBMNLAINDetailCrud)
        {
            if (dataoutWlPSPBMNLAINDetailCrud.PO_RESULT == "Y")
            {
                ucmusnahBMN.tabSelect();
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
            //    MessageBox.Show(konfigApp.teksIsianKosong, konfigApp.judulKonfirmasi);
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
            //teKdAkun.Text = _kodeAkun;
            //teNmAkun.Text = _namaAkun;
        }
        #endregion

        private void teKdAkun_EditValueChanged(object sender, EventArgs e)
        {
            //teNmAkun.Text = "";
        }
    }
}