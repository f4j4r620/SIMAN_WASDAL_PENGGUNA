using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using AppPengguna.KSK.WL.PENGGUNAAN;
using AppPengguna.KSK.WL.PEMINDAHTANGANAN;
using AppPengguna.KSK.WL.PENGHAPUSAN;
using AppPengguna.KSK.WL.PENERTIBAN;
using AppPengguna.KSK.WL.PEMINDAHTANGAN;
using AppPengguna.KSK.WL.PEMANFAATAN;
using AppPengguna.KSK.WL.MONINSIDENTIL;

namespace AppPengguna.KSK.WL
{
//    public delegate void CariDataOnline(string strCariData);

    public partial class ucLapWasdal : DevExpress.XtraEditors.XtraUserControl
    {

        public CariDataOnline cariDataOnline;
        
        public string fieldDicari = "";
        public string modeLoadData = "normal"; //normal atau cari atau ganti_kiword
        private bool initModeLoad = true;
        private string cariSebelumnya = "";
        private string strCari = "";
        private ArrayList daftarFieldJenis = null;
        private ArrayList daftarFieldKolom = null;
        public FrmKoorSatker FrmKoorSatker;
        public string namaModul = null;
        private bool initCari = true;
        public Int32 currentLoop = 0;

        #region Inisialisasi tab aset
        
        public string strKdPelayanan = "";
        public bool tabPenggunaan1Open;
        public bool tabPemanfaataan1Open;
        public bool tabPemindahtanganan1Open;
        public bool tabPenghapusan1Open;
        public bool tabPenggunaanRpmkOpen;
        public bool tabPemanfaatanRpmkOpen;
        public bool tabPemindahtanganRpmkOpen;
        public bool tabMonitoringInsidentilOpen;
        //public ucPenggunaan penggunaan1 = null;
        public ucPenggunaanNew penggunaan1 = null;
        public ucPemanfaatanNew pemanfaatan1 = null;

        public ucPemindahtanganNew pemindahtanganan1 = null;
        public ucPenghapusan penghapusan1 = null;
        public ucPenggunaanRpmk penggunaanRpmk = null;
        public ucPemanfaatanRpmk pemanfaatanRpmk = null;
        public ucPemindahtanganRpmk pemindahtanganRpmk = null;
        public ucMonitoringInsidentil monitoringInsidentil = null;
        public ucPenertiban monitoringPenertiban = null;
        #endregion


        public ucLapWasdal(FrmKoorSatker _frmKpknl,string _namaModul)
        {
            InitializeComponent();
            this.FrmKoorSatker = _frmKpknl;
            this.namaModul = _namaModul;
            this.initSearch();
            
        }

        #region SET TAB
        public void tabSelect()
        {
            initSearch();
            #region DETAIL PSP SUDAH JADI
            if (xtbDetail1.SelectedTabPageIndex == 0)
            {
                FiturSearch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                xtpPenggunaan.Text = this.namaModul;
                if (!tabPenggunaan1Open)
                {
                    #region Penggunaan BMN
                    if (namaModul.Equals("Penggunaan BMN"))
                    {
                        penggunaan1 = new ucPenggunaanNew(FrmKoorSatker, this);
                        penggunaan1.cariDataOnline = new CariDataOnline(this.pencarianData);
                        penggunaan1.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(penggunaan1);
                        penggunaan1.getTindakLanjut();
                        tabPenggunaan1Open = true;
                    }
                    #endregion
                    #region Pemanfaatan BMN
                    else if (namaModul.Equals("Pemanfaatan BMN"))
                    {
                        pemanfaatan1 = new ucPemanfaatanNew(FrmKoorSatker, this);
                        pemanfaatan1.cariDataOnline = new CariDataOnline(this.pencarianData);
                        pemanfaatan1.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(pemanfaatan1);
                        pemanfaatan1.getTindakLanjut();
                        tabPemanfaataan1Open = true;
                    }
                    #endregion 
                    #region Pemindahtanganan BMN
                    else if (namaModul.Equals("Pemindahtanganan BMN"))
                    {
                        pemindahtanganan1 = new ucPemindahtanganNew(FrmKoorSatker, this);
                        pemindahtanganan1.cariDataOnline = new CariDataOnline(this.pencarianData);
                        pemindahtanganan1.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(pemindahtanganan1);
                        pemindahtanganan1.getTindakLanjut();
                        tabPemanfaataan1Open = true;
                    }
                    #endregion
                    #region Penghapusan BMN
                    else if (namaModul.Equals("Penghapusan BMN"))
                    {
                        penghapusan1 = new ucPenghapusan(FrmKoorSatker, this);
                        penghapusan1.cariDataOnline = new CariDataOnline(this.pencarianData);
                        penghapusan1.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(penghapusan1);
                        penghapusan1.getTindakLanjut();
                        tabPenghapusan1Open = true;
                    }
                    #endregion 

                    else if (namaModul.Equals("Penggunaan BMN RPMK"))
                    {
                        penggunaanRpmk = new ucPenggunaanRpmk(FrmKoorSatker, this);
                        penggunaanRpmk.cariDataOnline = new CariDataOnline(this.pencarianData);
                        penggunaanRpmk.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(penggunaanRpmk);
                        penggunaanRpmk.getTindakLanjut();
                        //penggunaanRpmk.getTotalData();
                        tabPenggunaanRpmkOpen = true;
                        FiturSearch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else if (namaModul.Equals("Pemanfaatan BMN RPMK"))
                    {
                        pemanfaatanRpmk = new ucPemanfaatanRpmk(FrmKoorSatker, this);
                        pemanfaatanRpmk.cariDataOnline = new CariDataOnline(this.pencarianData);
                        pemanfaatanRpmk.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(pemanfaatanRpmk);
                        pemanfaatanRpmk.getTindakLanjut();
                        //pemanfaatanRpmk.getTotalData();
                        tabPemanfaatanRpmkOpen = true;
                        FiturSearch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else if (namaModul.Equals("Pemindahtanganan BMN RPMK"))
                    {
                        pemindahtanganRpmk = new ucPemindahtanganRpmk(FrmKoorSatker, this);
                        pemindahtanganRpmk.cariDataOnline = new CariDataOnline(this.pencarianData);
                        pemindahtanganRpmk.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(pemindahtanganRpmk);
                        pemindahtanganRpmk.getTindakLanjut();
                        tabPemindahtanganRpmkOpen = true;
                        FiturSearch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always ;
                    }
                    else if (namaModul.Equals("Monitoring Insidentil"))
                    {
                        monitoringInsidentil = new ucMonitoringInsidentil(FrmKoorSatker, this);
                        monitoringInsidentil.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(monitoringInsidentil);
                        monitoringInsidentil.getDataMonitoring();
                        tabMonitoringInsidentilOpen = true;

                        FiturSearch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else if (namaModul.Equals("Penertiban BMN"))
                    {
                        monitoringPenertiban = new ucPenertiban(FrmKoorSatker, this);
                        monitoringPenertiban.Dock = DockStyle.Fill;
                        panelPenggunaan.Controls.Clear();
                        panelPenggunaan.Controls.Add(monitoringPenertiban);
                        monitoringPenertiban.getDataMonitoring();
                        tabMonitoringInsidentilOpen = true;
                    }
                }
            }
            
            #endregion
            
        }
        #endregion

        #region PENCARIAN

        private DataTable dtTable = new DataTable();
        private DataColumn dtColumn;
        private DataRow dtRow;
        private ArrayList dtParams1;
        private ArrayList dtParams2;

        public void initSearch()
        {

            #region KOLOM PENCARIAN 1
            /*** KOLOM PENCARIAN 1 ***/
            dtTable = new DataTable();
            dtColumn = new DataColumn();
            dtParams1 = new ArrayList();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "DISPLAY";
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "VALUE";
            dtTable.Columns.Add(dtColumn);

            if (!namaModul.Equals("Monitoring Insidentil"))
            {
                this.addColumn("KD_BRG", "KODE BARANG");
                this.dtParams1.Add("{1}%");
                //this.addColumn("NUP", "NUP");
                //this.dtParams1.Add("{1}%");
                this.addColumn("UR_SSKEL", "NAMA BARANG");
                this.dtParams1.Add("%{1}%");
                this.addColumn("KD_PEMOHON", "KODE PEMOHON");
                this.dtParams1.Add("{1}%");
                this.addColumn("NM_PEMOHON", "NAMA PEMOHON");
                this.dtParams1.Add("{1}%");
                this.addColumn("KUANTITAS", "KUANTITAS");
                this.dtParams1.Add("{1}%");
                this.addColumn("NILAI_PENETAPAN", "NILAI PERSETUJUAN");
                this.dtParams1.Add("{1}%");
                this.addColumn("KD_STATUS", "KODE STATUS");
                this.dtParams1.Add("{1}%");
                this.addColumn("NM_STATUS", "NAMA STATUS");
                this.dtParams1.Add("%{1}%");
                this.addColumn("GUNA_WASDAL", "PENGGUNAAN BMN");
                this.dtParams1.Add("%{1}%");
                this.addColumn("NO_SURAT", "NO SK");
                this.dtParams1.Add("{1}%");
                //this.addColumn("KD_PELAYANAN", "KODE PELAYANAN");
                //this.dtParams1.Add("{1}%");
                //this.addColumn("NM_PELAYANAN", "NAMA PELAYANAN");
                //this.dtParams1.Add("%{1}%");
                this.addColumn("THN_ANG", "TAHUN ANGGARAN");
                this.dtParams1.Add("{1}%");
                this.addColumn("KD_SATKER", "KODE SATKER");
                this.dtParams1.Add("{1}%");
                this.addColumn("UR_SATKER", "NAMA SATKER");
                this.dtParams1.Add("{1}%");
            }

            cbNamaKolom1.Properties.DataSource = dtTable;
            cbNamaKolom1.Properties.DisplayMember = "DISPLAY";
            cbNamaKolom1.Properties.ValueMember = "VALUE";
            #endregion

            #region KOLOM PENCARIAN 2
            /*** KOLOM PENCARIAN 2 ***/
            dtTable = new DataTable();
            dtColumn = new DataColumn();
            dtParams2 = new ArrayList();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "DISPLAY";
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "VALUE";
            dtTable.Columns.Add(dtColumn);
            if (!namaModul.Equals("Monitoring Insidentil"))
            {
                this.addColumn("UR_SSKEL", "NAMA BARANG");
                this.dtParams2.Add("%{1}%");
                this.addColumn("KD_PEMOHON", "KODE PEMOHON");
                this.dtParams2.Add("{1}%");
                this.addColumn("NM_PEMOHON", "NAMA PEMOHON");
                this.dtParams2.Add("{1}%");
                this.addColumn("KUANTITAS", "KUANTITAS");
                this.dtParams2.Add("{1}%");
                this.addColumn("NILAI_PENETAANPAN", "NILAI PERSETUJUAN");
                this.dtParams2.Add("{1}%");
                this.addColumn("KD_STATUS", "KODE STATUS");
                this.dtParams2.Add("{1}%");
                this.addColumn("NM_STATUS", "NAMA STATUS");
                this.dtParams2.Add("%{1}%");
                this.addColumn("GUNA_WASDAL", "PENGGUNAAN BMN");
                this.dtParams2.Add("%{1}%");
                this.addColumn("NO_SURAT", "NO SK");
                this.dtParams2.Add("{1}%");
                //this.addColumn("KD_PELAYANAN", "KODE PELAYANAN");
                //this.dtParams2.Add("{1}%");
                //this.addColumn("NM_PELAYANAN", "NAMA PELAYANAN");
                //this.dtParams2.Add("%{1}%");
                this.addColumn("THN_ANG", "TAHUN ANGGARAN");
                this.dtParams2.Add("{1}%");
            }
            cbNamaKolom2.Properties.DataSource = dtTable;
            cbNamaKolom2.Properties.DisplayMember = "DISPLAY";
            cbNamaKolom2.Properties.ValueMember = "VALUE";
            #endregion

            cbNamaKolom1.EditValue=null;
            cbNamaKolom2.EditValue = null;
            teCari1.Text = "";
            teCari2.Text = "";
            teNupDari.Text = "";
            teNupSampai.Text = "";
            teCari1.Focus();

        }

        public void addColumn(string value, string display)
        {
            dtRow = dtTable.NewRow();
            dtRow["DISPLAY"] = display;
            dtRow["VALUE"] = value;
            dtTable.Rows.Add(dtRow);
        }

        private void setVisibleKolom2(bool set)
        {
            if (set)
            {
                emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciNamaKolom2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciKataKunci2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else {
                emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNamaKolom2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciKataKunci2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void setVisibleKolomNup(bool set)
        {
            if (set)
            {
                this.emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupDari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupSampai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                this.emptySpaceItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupDari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciNupSampai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.emptySpaceItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void cbNamaKolom_EditValueChanged(object sender, EventArgs e)
        {
            sbCari.Enabled = true;
            if (this.initModeLoad == false) this.modeLoadData = "ganti_kiword";
            this.fieldDicari = (cbNamaKolom1.Text.Trim() != "") ? cbNamaKolom1.EditValue.ToString() : "";

            if (fieldDicari != "")
            {
                switch (this.fieldDicari)
                {
                    case "KD_SATKER":
                        setVisibleKolomNup(false);
                        setVisibleKolom2(true);
                        break;
                    case "KD_BRG":
                    case "UR_SSKEL":
                        setVisibleKolom2(false);
                        setVisibleKolomNup(true);
                        break;
                    default:
                        setVisibleKolom2(false);
                        setVisibleKolomNup(false);
                        break;

                }
            }
            else {
                setVisibleKolom2(false);
                setVisibleKolomNup(false);
            }

            cbNamaKolom2.EditValue = null;
            teCari2.ResetText();
            teNupDari.ResetText();
            teNupSampai.ResetText();
        }

        private void cbNamaKolom2_EditValueChanged(object sender, EventArgs e)
        {
            sbCari.Enabled = true;
            this.modeLoadData = "ganti_kiword";
            this.fieldDicari = (cbNamaKolom2.Text.Trim() != "") ? cbNamaKolom2.EditValue.ToString() : "";
            if (fieldDicari != "")
            {
                switch (this.fieldDicari)
                {

                    case "KD_BRG":
                    case "UR_SSKEL":
                        setVisibleKolomNup(true);
                        break;
                    default:
                        setVisibleKolomNup(false);
                        setVisibleKolom2(true);
                        break;
                }
            }
            teNupDari.ResetText();
            teNupSampai.ResetText();
        }

        private string getFieldKolom(int indeksKolom)
        {
            if (indeksKolom > -1) return daftarFieldKolom[indeksKolom].ToString();
            else return "";
        }

        private void teCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (xtpPenggunaan.Text.Equals("Penggunaan BMN"))
                {
                    if (penggunaan1.initModeLoad == false) penggunaan1.modeLoadData = "ganti_kiword";
                }
                else if (xtpPenggunaan.Text.Equals("Pemanfaatan BMN"))
                {
                    if (pemanfaatan1.initModeLoad == false) pemanfaatan1.modeLoadData = "ganti_kiword";
                }
                else if (xtpPenggunaan.Text.Equals("Pemindahtanganan BMN"))
                {
                    if (pemindahtanganan1.initModeLoad == false) pemindahtanganan1.modeLoadData = "ganti_kiword";
                }
            }
            sbCari.Enabled = true;
        }

        private void setStrCari()
        {
            this.strCari = string.Empty;
            string namaKolom1 = string.Empty;
            string namaKolom2 = string.Empty;
            string kataKunci1 = string.Empty;
            string kataKunci2 = string.Empty;

            namaKolom1 = (cbNamaKolom1.Text.Trim() != "") ? cbNamaKolom1.EditValue.ToString() : "";
            namaKolom2 = (cbNamaKolom2.Text.Trim() != "") ? cbNamaKolom2.EditValue.ToString() : "";
            kataKunci1 = teCari1.Text.Trim().ToUpper();
            kataKunci2 = teCari2.Text.Trim().ToUpper();

            if (namaKolom1 != "" && kataKunci1 != "")
            {
                this.strCari = String.Format("AND (UPPER({0}) LIKE '" + dtParams1[cbNamaKolom1.ItemIndex].ToString() + "') ", namaKolom1, kataKunci1);
            }
            if (namaKolom2 != "" && kataKunci2 != "")
            {
                this.strCari += String.Format(" AND( UPPER({0}) LIKE '" + dtParams2[cbNamaKolom2.ItemIndex].ToString() + "') ", namaKolom2, kataKunci2);
            }
            if (teNupDari.Text.Trim() != "" && teNupSampai.Text.Trim() != "")
            {
                this.strCari += String.Format("  AND (UPPER({0}) BETWEEN {1} AND {2} )", "NUP", teNupDari.Text.Trim().ToUpper(), teNupSampai.Text.Trim().ToUpper());
            }

        }

        private void sbCariOnline_Click(object sender, EventArgs e)
        {

            if ((teCari1.Text.Trim() != ""))
            {
                if (xtbDetail1.SelectedTabPageIndex == 0)
                {
                    #region Penggunaan
                    if (xtpPenggunaan.Text.Equals("Penggunaan BMN"))
                    {
                        if ((penggunaan1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        {
                            penggunaan1.dataInisial = true;
                            penggunaan1.modeLoadData = "cari";
                            cariSebelumnya = teCari1.Text.Trim();
                            penggunaan1.initModeLoad = true;
                        }
                        else
                        {
                            penggunaan1.dataInisial = false;
                            penggunaan1.initModeLoad = false;
                        }
                        this.setStrCari();
                        penggunaan1.cariDataOnline(this.strCari,true);

                    }
                    #endregion

                    #region Pemanfaatan
                    else if (xtpPenggunaan.Text.Equals("Pemanfaatan BMN"))
                    {
                        if ((pemanfaatan1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        {
                            pemanfaatan1.dataInisial = true;
                            pemanfaatan1.modeLoadData = "cari";
                            cariSebelumnya = teCari1.Text.Trim();
                            pemanfaatan1.initModeLoad = true;
                        }
                        else
                        {
                            pemanfaatan1.dataInisial = false;
                            pemanfaatan1.initModeLoad = false;
                        }
                        this.setStrCari();
                        pemanfaatan1.cariDataOnline(this.strCari,true);
                    }
                    #endregion

                    #region Pemindahtanganan
                    else if (xtpPenggunaan.Text.Equals("Pemindahtanganan BMN"))
                    {
                        if ((pemindahtanganan1.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        {
                            pemindahtanganan1.dataInisial = true;
                            pemindahtanganan1.modeLoadData = "cari";
                            cariSebelumnya = teCari1.Text.Trim();
                            pemindahtanganan1.initModeLoad = true;
                        }
                        else
                        {
                            pemindahtanganan1.dataInisial = false;
                            pemindahtanganan1.initModeLoad = false;
                        }
                        this.setStrCari();
                        pemindahtanganan1.cariDataOnline(this.strCari, true);
                    }
                    #endregion

                    #region Penggunaan BMN RPMK
                    else if (xtpPenggunaan.Text.Equals("Penggunaan BMN RPMK"))
                    {
                        if ((penggunaanRpmk.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        {
                            penggunaanRpmk.dataInisial = true;
                            penggunaanRpmk.modeLoadData = "cari";
                            cariSebelumnya = teCari1.Text.Trim();
                            penggunaanRpmk.initModeLoad = true;
                        }
                        else
                        {
                            penggunaanRpmk.dataInisial = false;
                            penggunaanRpmk.initModeLoad = false;
                        }
                        this.setStrCari();
                        penggunaanRpmk.cariDataOnline(this.strCari, true);

                    }
                    #endregion

                    #region Pemanfaatan BMN RPMK
                    else if (namaModul.Equals("Pemanfaatan BMN RPMK"))
                    {
                        if ((pemanfaatanRpmk.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        {
                            pemanfaatanRpmk.dataInisial = true;
                            pemanfaatanRpmk.modeLoadData = "cari";
                            cariSebelumnya = teCari1.Text.Trim();
                            pemanfaatanRpmk.initModeLoad = true;
                        }
                        else
                        {
                            pemanfaatanRpmk.dataInisial = false;
                            pemanfaatanRpmk.initModeLoad = false;
                        }
                        this.setStrCari();
                        pemanfaatanRpmk.cariDataOnline(this.strCari, true);
                    }
                    #endregion

                    #region Pemindahtanganan BMN RPMK
                    else if (namaModul.Equals("Pemindahtanganan BMN RPMK"))
                    {
                        if ((pemindahtanganRpmk.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        {
                            pemindahtanganRpmk.dataInisial = true;
                            pemindahtanganRpmk.modeLoadData = "cari";
                            cariSebelumnya = teCari1.Text.Trim();
                            pemindahtanganRpmk.initModeLoad = true;
                        }
                        else
                        {
                            pemindahtanganRpmk.dataInisial = false;
                            pemindahtanganRpmk.initModeLoad = false;
                        }
                        this.setStrCari();
                        pemindahtanganRpmk.cariDataOnline(this.strCari, true);
                    }
                    #endregion

                    #region Monitoring Insidentil
                    else if (namaModul.Equals("Monitoring Insidentil"))
                    {
                        if ((monitoringInsidentil.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        {
                            monitoringInsidentil.dataInisial = true;
                            monitoringInsidentil.modeLoadData = "cari";
                            cariSebelumnya = teCari1.Text.Trim();
                            monitoringInsidentil.initModeLoad = true;
                        }
                        else
                        {
                            monitoringInsidentil.dataInisial = false;
                            monitoringInsidentil.initModeLoad = false;
                        }
                        this.setStrCari();
                        this.pencarianData(this.strCari, true);
                    }
                    #endregion
   
                    #region Penertiban
                    else if (namaModul.Equals("Penertiban BMN"))
                    {
                        if ((monitoringPenertiban.modeLoadData != "cari") || (cariSebelumnya != teCari1.Text.Trim()))
                        {
                            monitoringPenertiban.dataInisial = true;
                            monitoringPenertiban.modeLoadData = "cari";
                            cariSebelumnya = teCari1.Text.Trim();
                            monitoringPenertiban.initModeLoad = true;
                        }
                        else
                        {
                            monitoringPenertiban.dataInisial = false;
                            monitoringPenertiban.initModeLoad = false;
                        }
                        this.setStrCari();
                        this.pencarianData(this.strCari, true);
                    }
                    #endregion
                }
                
              
            }
        }

        private void pencarianData(string kataCari, bool initcari)
        {
            if (xtbDetail1.SelectedTabPageIndex == 0)
            {
                if (xtpPenggunaan.Text.Equals("Penggunaan BMN"))
                {
                    penggunaan1.strCari = kataCari;
                    penggunaan1.getTindakLanjut();
                }
                else if (xtpPenggunaan.Text.Equals("Pemanfaatan BMN"))
                {
                    pemanfaatan1.strCari = kataCari;
                    pemanfaatan1.getTindakLanjut();
                }
                else if (xtpPenggunaan.Text.Equals("Pemindahtanganan BMN"))
                {
                    pemindahtanganan1.strCari = kataCari;
                    pemindahtanganan1.getTindakLanjut();
                }
                else if (xtpPenggunaan.Text.Equals("Penghapusan BMN"))
                {
                    penghapusan1.strCari = kataCari;
                    penghapusan1.getTindakLanjut();
                }

                if (xtpPenggunaan.Text.Equals("Penggunaan BMN RPMK"))
                {
                    penggunaanRpmk.strCari = kataCari;
                    penggunaanRpmk.getTindakLanjut();
                }
                #region Pemanfaatan BMN RPMK
                else if (namaModul.Equals("Pemanfaatan BMN RPMK"))
                {
                    pemanfaatanRpmk.strCari = kataCari;
                    pemanfaatanRpmk.getTindakLanjut();   
                }
                #endregion
                #region Pemindahtanganan BMN RPMK
                else if (namaModul.Equals("Pemindahtanganan BMN RPMK"))
                {
                    pemindahtanganRpmk.strCari = kataCari;
                    pemindahtanganRpmk.getTindakLanjut();   
                }
                #endregion
                #region Monitoring Insidentil
                else if (namaModul.Equals("Monitoring Insidentil"))
                {
                    monitoringInsidentil.strCari = kataCari;
                    monitoringInsidentil.getDataMonitoring(); 
                }
                #endregion
                #region Penertiban
                else if (namaModul.Equals("Penertiban BMN"))
                {
                    monitoringPenertiban.strCari = kataCari;
                    monitoringPenertiban.getDataMonitoring(); 
                }
                #endregion


            }
            
        }

        #endregion

        #region print grid
        public void print(string lap)
        {
            if (lap == "penggunaan")
                penggunaan1.ShowGridPreview();
            else
                pemanfaatan1.ShowGridPreview();
        }
        #endregion

        private void gvMWasdal_ColumnFilterChanged(object sender, EventArgs e)
        {
      
        }

        public string nameTab1 = "";
        public string nameTab2 = "";
        private void ucMtrWasdal_Load(object sender, EventArgs e)
        {
            tabSelect();
            xtpPenggunaan.Text = namaModul;
            setVisibleKolom2(false);
            setVisibleKolomNup(false);
        }

        private void xtbDetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabSelect();
        }

        private void xtcPSP_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabSelect();
        }

        private void xtbDetail2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabSelect();
        }

        #region PROGRESS BAR DAN BACKGROUNDWORKER
        public void startBackgroundWorker(object sender, EventArgs e)
        {
            layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }
        
        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
                    // Perform a time consuming operation and report progress.
                    //System.Threading.Thread.Sleep(10);
                    worker.ReportProgress(this.currentLoop);

        }

        #endregion

        public void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progresDescription.Text = "Inserting " + e.ProgressPercentage.ToString() + " data";
            progressBar.Position = e.ProgressPercentage;
        }

        public void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progresDescription.Text = "Completed!";
        }

        public void ExportToExcel()
        {
            try
            {
                string pathToSave = "";
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Excel files (*.xls)|*.xls";
                    dialog.FilterIndex = 2;
                    dialog.RestoreDirectory = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pathToSave = dialog.FileName;
                    }
                }


                if (pathToSave == "") return;
            
                switch (namaModul)
                {
                    case "Penggunaan BMN":
                        penggunaan1.ExportToExcel(pathToSave);
                        break;
                    case "Pemanfaatan BMN":
                        pemanfaatan1.ExportToExcel(pathToSave);
                        break;
                    case "Pemindahtanganan BMN":
                        pemindahtanganan1.ExportToExcel(pathToSave);
                        break;
                    case "Penghapusan BMN":
                        //penghapusan1.getTindakLanjut();
                        break;
                    case "Penggunaan BMN RPMK":
                       // penggunaanRpmk.getTindakLanjut();
                        break;
                    case  "Pemanfaatan BMN RPMK":
                        //pemanfaatanRpmk.getTindakLanjut();
                        break;
                    case "Pemindahtanganan BMN RPMK":
                        //pemindahtanganRpmk.getTindakLanjut();
                        break;
                    case "Monitoring Insidentil":
                        monitoringInsidentil.ExportToExcel(pathToSave);
                        break;
                    case "Penertiban BMN":
                        monitoringPenertiban.ExportToExcel(pathToSave);
                        break;
                }

                System.Diagnostics.Process.Start(pathToSave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }



}
