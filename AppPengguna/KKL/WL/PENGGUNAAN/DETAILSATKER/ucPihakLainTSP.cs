﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AppPengguna.KKL.WL.PENGGUNAAN.DETAILSATKER
{
    public partial class ucPihakLainTSP : ucMain
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm form;
        public bool dataInisial = true;
        ArrayList data;
        public bool moreButton = true;
        string kode = "";

        public detail detail;

        SvcSelectGunaA22.InputParameters input;
        SvcSelectGunaA22.OutputParameters output;
        SvcSelectGunaA22.execute_pttClient client;
        SvcSelectGunaA22.WASDALSROW_MON_GUNA_A22 rowData;


        public ucPihakLainTSP()
        {
            InitializeComponent();
        }

        public ucPihakLainTSP(string label, string kode)
        {
            InitializeComponent();
            labelControl1.Text = label;
            this.kode = kode;
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

        #region Load Data
        int dataCount = 0;
        private void GetData(string where)
        {
            try
            {
                FormNonAktif();
                input = new SvcSelectGunaA22.InputParameters();
                if (dataInisial)
                {
                    input.P_MIN = konfigApp.dataAwal;
                    input.P_MAX = konfigApp.dataMaks;
                }
                else
                {
                    input.P_MIN = dataCount + 1;
                    input.P_MAX = dataCount + konfigApp.dataMaks;
                }
                input.P_MINSpecified = true;
                input.P_MAXSpecified = true;
                input.P_COL = "";
                input.P_SORT = "";
                input.STR_WHERE = "ID_SATKER = " + kode;

                client = new SvcSelectGunaA22.execute_pttClient();
                client.Open();
                client.Beginexecute(input, new AsyncCallback(getResult), null);
            }
            catch (Exception ex)
            {
                FormAktif();
                MessageError(ex.Message);
            }


        }
        private void getResult(IAsyncResult result)
        {
            try
            {
                output = client.Endexecute(result);
                client.Close();
                this.Invoke(new ShowData(this.ShowDataGrid), output);
                FormAktif();
            }
            catch (Exception ex)
            {
                FormAktif();
                MessageError(ex.Message);
            }
        }

        private delegate void ShowData(SvcSelectGunaA22.OutputParameters output);

        private void ShowDataGrid(SvcSelectGunaA22.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_GUNA_A22.Count();
                dataCount += jmlData;
                if (jmlData == konfigApp.dataMaks)
                {
                    moreButton = true;
                    setTombolMoreData(true);
                }
                else
                {
                    moreButton = false;
                    setTombolMoreData(false);
                }

                if (dataInisial)
                {
                    data = new ArrayList();
                }
                //SudahPSPItem[] dataTemp = SudahPSPItem.GetPSP();

                //foreach (var item in output.SF_MON_GUNA_A21)
                //{
                //    foreach (var item2 in data)
                //    {
                //        if (item.KD_JNS_BMN == item2.KD_JENIS_BMN)
                //        {
                //            item2.ID_SATKER = item.ID_SATKER;
                //            item2.KD_SATKER = item.KD_SATKER;
                //            item2.KUANTITAS_PSP_Y = item.KUANTITAS_PSP_Y;
                //            item2.LUAS_PSP_Y = item.LUAS_PSP_Y;
                //            item2.NIL_PERLH_Y = item.NIL_PERLH_PSP_Y;
                //            item2.NUM = item.NUM;
                //            item2.UR_SATKER = item.UR_SATKER;
                //            break;
                //        }
                //    }
                //}
                data.AddRange(output.SF_MON_GUNA_A22.ToList());
                this.BsMain.DataSource = data;
                this.gridControl.RefreshDataSource();
                gridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                FormAktif();
                MessageError(ex.Message);
            }

        }

        private void setTombolMoreData(bool p)
        {
            if (p)
            {
                BtnMore.Enabled = true;
            }
            else
            {
                BtnMore.Enabled = false;
            }
        }

        #endregion

        public void LoadData(bool state)
        {
            dataInisial = state;
            this.GetData("");
        }

        private void BtnMore_Click(object sender, EventArgs e)
        {
            LoadData(false);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(true);
        }

    }
}
