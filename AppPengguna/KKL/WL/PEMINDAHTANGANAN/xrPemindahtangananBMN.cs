using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Data;
using System.Linq;

namespace AppPengguna.KKL.WL.PEMINDAHTANGANAN
{
    public partial class xrPemindahtangananBMN : DevExpress.XtraReports.UI.XtraReport
    {
        SvcWasdalPSPBMNSelect.WASDALSROW_WASDAL_PSP wasdalSelect;
        SvcWasdalPSPBMNSelect.execute_pttClient ambilWasdal;
        SvcWasdalPSPBMNSelect.OutputParameters dataOutWasdal;

        public xrPemindahtangananBMN()
        {
            //getInitWasdalBMN();
            InitializeComponent();
        }

        //#region  Show Data Wasdal

        //public void getInitWasdalBMN()
        //{
        //    try 
        //        {	        
        //            SvcWasdalPSPBMNSelect.InputParameters inPar = new SvcWasdalPSPBMNSelect.InputParameters();
        //            inPar.P_MAX = konfigApp.currentMaks;
        //            inPar.P_MAXSpecified = true;
        //            inPar.P_MIN = konfigApp.currentMin;
        //            inPar.P_MINSpecified = true;
        //            inPar.P_COL = "IS_TB";
        //            inPar.P_SORT = "DESC";
        //            ambilWasdal = new SvcWasdalPSPBMNSelect.call_pttClient();
        //            ambilWasdal.Beginexecute(inPar, new AsyncCallback(getDataWasdalBMN),null);
        //        }
        //    catch
        //        {
        //            MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
        //        }
        //}

        //private void getDataWasdalBMN(IAsyncResult result)
        //{
        //    try
        //    {
        //        dataOutWasdal = ambilWasdal.Endexecute(result);
        //        this.Invoke(new SimpanDataWasdalBMN(this.simpanDataWasdalBMN), dataOutWasdal);
        //    }
        //    catch 
        //    {
        //        MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulGagalAmbil);
        //    }
        //}

        //private void Invoke(SimpanDataWasdalBMN simpanDataWasdal, SvcWasdalPSPBMNSelect.OutputParameters dataOutWasdal)
        //{
        //    bsLapWasdalBMN.DataSource = dataOutWasdal.SF_ROW_WASDAL_PSP;
        //    int jmlData = dataOutWasdal.SF_ROW_WASDAL_PSP.Count();
        //    for (int i = 0; i < jmlData; i++)
        //    {
        //        if (dataOutWasdal.SF_ROW_WASDAL_PSP[i].IS_TB == "Y")
        //        {
        //            dataOutWasdal.SF_ROW_WASDAL_PSP[i].IS_TB = "I. Tanah dan / atau bangunan";
                    
        //        }
        //        else//if (dataOutWasdal.SF_ROW_WASDAL_PSP[i].IS_TB == "N")
        //        {
        //            dataOutWasdal.SF_ROW_WASDAL_PSP[i].IS_TB = "II. Selain Tanah dan / atau bangunan 5)";
        //        }
        //        if (dataOutWasdal.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL == "Dipergunakan sesuai TUSI")
        //        {
        //            dataOutWasdal.SF_ROW_WASDAL_PSP[i].NIP_PENANDATANGAN = "Ya"; //dipake untuk TUSI yang "YA"
        //            dataOutWasdal.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL = "";
        //        }
        //        else
        //        {
        //            dataOutWasdal.SF_ROW_WASDAL_PSP[i].NIP_PENANDATANGAN = ""; //dipake untuk TUSI yang "YA"
        //            dataOutWasdal.SF_ROW_WASDAL_PSP[i].GUNA_WASDAL = "Tidak";
        //        }
        //    }
        //    bsLapWasdalBMN.DataSource = dataOutWasdal.SF_ROW_WASDAL_PSP;
        //}

        //private delegate void SimpanDataWasdalBMN(SvcWasdalPSPBMNSelect.OutputParameters dataOut);

        //private void simpanDataWasdalBMN(SvcWasdalPSPBMNSelect.OutputParameters dataOut)
        //{
        //    bsLapWasdalBMN.DataSource = dataOut.SF_ROW_WASDAL_PSP;
        //}

        //#endregion

        private void xrLapWasdalBMN_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //getInitWasdalBMN();
        }

        
    }
}
