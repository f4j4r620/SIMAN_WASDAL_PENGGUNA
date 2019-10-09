using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraLayout;

namespace AppPengguna.KKW.WL.PENGGUNAAN.DETAILSATKER
{
    public partial class ucBMNDetail : ucMain
    {
        public ToggleProgressBar toggleProgressBar;
        public AktifkanForm form;
        public bool dataInisial = true;
        ArrayList data;
        public bool moreButton = true;
        string kode = "";

        public kembali back;

        SvcSelectGunaA211.InputParameters input;
        SvcSelectGunaA211.OutputParameters output;
        SvcSelectGunaA211.execute_pttClient client;
        SvcSelectGunaA211.WASDALSROW_MON_GUNA_A211 rowData;
        string idSatker = "";

        public ucBMNDetail()
        {
            InitializeComponent();
        }

        public ucBMNDetail(string label, string kode, string idsatker)
        {
            InitializeComponent();
            labelControl1.Text = label;
            this.kode = kode;
            idSatker = idsatker;
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
                input = new SvcSelectGunaA211.InputParameters();
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
                input.STR_WHERE = string.Format("KD_JNS_BMN = {0} AND ID_SATKER = {1}", kode, idSatker);

                client = new SvcSelectGunaA211.execute_pttClient();
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

        private delegate void ShowData(SvcSelectGunaA211.OutputParameters output);

        private void ShowDataGrid(SvcSelectGunaA211.OutputParameters output)
        {
            try
            {
                int jmlData = output.SF_MON_GUNA_A211.Count();
                dataCount += jmlData;


                if (dataInisial)
                {
                    data = new ArrayList();
                }
                data.AddRange(output.SF_MON_GUNA_A211.ToList());
                this.BsMain.DataSource = data;
                this.gridControl.RefreshDataSource();
                gridView.BestFitColumns();

                layoutControl1.BeginUpdate();
                if (jmlData == konfigApp.dataMaks)
                {
                    //moreButton = true;
                    //setTombolMoreData(BtnMore, true);
                    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //this.BtnMore.Enabled = true;
                }
                else
                {
                    //moreButton = false;
                    //setTombolMoreData(BtnMore, false);
                    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //this.BtnMore.Enabled = false;
                }
                layoutControl1.EndUpdate();
                layoutControl1.Refresh();
            }
            catch (Exception ex)
            {
                FormAktif();
                MessageError(ex.Message);
            }

        }

        private void setTombolMoreData(Control itemControl, bool p)
        {
            //if (p)
            //{
            //    BtnMore.Enabled = true;
            //}
            //else
            //{
            //    BtnMore.Enabled = false;
            //}

            LayoutControl lc = itemControl.Parent as LayoutControl;
            if (lc == null)
                return;

            LayoutControlItem item = lc.GetItemByControl(itemControl);
            if (item == null)
                return;

            LayoutGroup itemGroup = item.Parent;
            itemGroup.BeginUpdate();
            try
            {
                itemControl.Enabled = p;
            }
            finally
            {
                itemGroup.EndUpdate();
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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            back();
        }

        private void colMasterAset_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SvcSelectGunaA211.WASDALSROW_MON_GUNA_A211 row = (SvcSelectGunaA211.WASDALSROW_MON_GUNA_A211)gridView.GetRow(gridView.FocusedRowHandle);
            konfigApp.getDetailAset(row.ID_ASET.ToString());
        }

        private void beVerif_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SvcSelectGunaA211.WASDALSROW_MON_GUNA_A211 row = (SvcSelectGunaA211.WASDALSROW_MON_GUNA_A211)gridView.GetRow(gridView.FocusedRowHandle);
            DialogResult dialogResult = new DialogResult();
            if (row.VERIFIKASI == "N") dialogResult = MessageBox.Show("Apakah anda ingin memverifikasi aset " + row.UR_SSKEL, "Verifikasi Aset BMN", MessageBoxButtons.YesNo);
            else dialogResult = MessageBox.Show("Apakah anda ingin membatalkan verifikasi aset " + row.UR_SSKEL, "Verifikasi Aset BMN", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes) verifikasiBMN(row.ID_ASET, (row.VERIFIKASI == "Y") ? "N" : "Y" );
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == colVERIFIKASI)
            {
                if(e.CellValue != null)
                {
                    string result = (e.CellValue.ToString().ToUpper() == "Y") ? "Sudah" : "Belum";
                    if (e.RowHandle < gridView.RowCount) beVerif.Buttons[0].Caption = result;
                }
            }
        }

        #region verifikasi BMN
        private void verifikasiBMN(decimal? idAset, string verifYN)
        {
            try
            {
                FormNonAktif();
                SvcVerifikasiBmn.InputParameters input = new SvcVerifikasiBmn.InputParameters()
                {
                    P_ID_ASET = idAset,
                    P_ID_ASETSpecified = true,
                    P_YN = verifYN
                };
                SvcVerifikasiBmn.execute_pttClient fetch = new SvcVerifikasiBmn.execute_pttClient();
                SvcVerifikasiBmn.OutputParameters output = new SvcVerifikasiBmn.OutputParameters();
                output = fetch.execute(input);
                if (output.PO_RESULT == "Y") MessageBox.Show(konfigApp.judulsukses, konfigApp.teksBerhasilSimpan);
                else MessageBox.Show(konfigApp.judulGagalSimpan, konfigApp.teksGagalSimpan);
            }
            catch (Exception ex)
            {
                FormAktif();
                MessageError(ex.Message);
            }
            finally
            {
                LoadData(true);
            }
        }
        #endregion verifikasi BMN
    }
}
