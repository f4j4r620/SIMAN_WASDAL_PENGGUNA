using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPengelola.KKPKNL.LWD.LAPMONSATKER
{
    public partial class ucLapMonSatkerCudCatatan : UserControl
    {
        public detailLapmonSatker back;
        public ToggleProgressBar toggleProgressBar;
        string mode = "";
        // Service
        public SvcLapMonWasdalCatatanSelect.WASDALSROW_MON_LAP_WASDAL_CATATAN rowData;


        SvcLapMonWasdalCatatanCud.execute_pttClient client;
        SvcLapMonWasdalCatatanCud.InputParameters input;
        SvcLapMonWasdalCatatanCud.OutputParameters output;
        public SvcLapMonWasdalSelect.WASDALSROW_MON_LAP_WASDAL rowDataTerpilih;

        public ucLapMonSatkerCudCatatan()
        {
            InitializeComponent();
        }
        public ucLapMonSatkerCudCatatan(string mode)
        {
            InitializeComponent();
            this.mode = mode;
        }


        #region Property
        private void AktifkanForm(string str)
        {
            if (str == "aktif")
                this.Enabled = true;
            else
                this.Enabled = false;

        }
        private void MessageError(string ex)
        {
            MessageBox.Show(konfigApp.teksGagalAmbil + ":" + ex, konfigApp.judulGagalAmbil);
            FormAktif();
        }

        private void FormAktif()
        {
            this.toggleProgressBar("finish");
            this.Invoke(new AktifkanForm(this.AktifkanForm), "aktif");
        }

        private void FormNonAktif()
        {
            this.toggleProgressBar("start");
            this.Invoke(new AktifkanForm(this.AktifkanForm), "nonaktif");
        }

       
        #endregion

        public void SetLabel()
        {
            labelControl1.Text = "SATKER   : " + rowDataTerpilih.KD_SATKER + " - " + rowDataTerpilih.UR_SATKER + "\n" +
                                 "NO SURAT : " + rowDataTerpilih.NO_SURAT;
            SetMode(mode);
        }

        private void SetMode(string mode)
        {
            teCatatan.Properties.ReadOnly = false;
            layoutBtnSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            switch (mode)
            {
                case "V":
                    teCatatan.Text = rowData.CATATAN;
                    teCatatan.Properties.ReadOnly = true;
                    layoutBtnSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case "U":
                    teCatatan.Text = rowData.CATATAN;
                    break;
                default:
                    teCatatan.Text = "";
                    break;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            back(rowDataTerpilih.ID_MON_LAP + "", "SATKER : " + rowDataTerpilih.UR_SATKER);
        }

        private void SaveData()
        {
            try
            {
                FormNonAktif();
                input = new SvcLapMonWasdalCatatanCud.InputParameters();
                input.P_CATATAN = teCatatan.Text;
                input.P_ID_MON_LAP = rowDataTerpilih.ID_MON_LAP;
                input.P_ID_MON_LAP_CAT = (mode == "U")?rowData.ID_MON_LAP_CAT : konfigApp.getGlobalId("ID_MON_LAP_CAT");
                input.P_ID_MON_LAP_CATSpecified = true;
                input.P_ID_MON_LAPSpecified = true;
                input.P_SELECT = mode;
                client = new SvcLapMonWasdalCatatanCud.execute_pttClient();
                client.Open();
                client.Beginexecute(input, new AsyncCallback(getResult), null);

            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        
        }

        private void getResult(IAsyncResult result)
        {
            try
            {
                output = client.Endexecute(result);
                FormAktif();
                this.Invoke(new ShowResult(showResult), output);
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private delegate void ShowResult(SvcLapMonWasdalCatatanCud.OutputParameters output);

        private void showResult(SvcLapMonWasdalCatatanCud.OutputParameters output)
        {
            if (output.PO_RESULT == "Y")
            {
                Dialog.info(output.PO_RESULT_MESSAGE);
                back(rowDataTerpilih.ID_MON_LAP + "","SATKER : " + rowDataTerpilih.UR_SATKER);
            }
            else
            {
                MessageError(output.PO_RESULT_MESSAGE);
            }
        
        }
    }
}
