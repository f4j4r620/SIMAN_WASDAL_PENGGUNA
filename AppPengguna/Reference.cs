using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace AppPengguna
{
    public partial class Reference : DevExpress.XtraEditors.XtraForm
    {
        //---- var --------------------------------------------
        #region variables
        private Thread myThread;
        #endregion

        //---- service ----------------------------------------
        #region Services
        private SvcKondisiSelect.svcDsRKondisiSelect_pttClient svcCaller;
        private SvcKondisiSelect.InputParameters parInp;
        private SvcKondisiSelect.OutputParameters parOut;
        private SvcKondisiSelect.BPSIMANSROW_R_KONDISI rowData;

        private SvcJnsSertifikatSelect.call_pttClient svcCallerJnsSerti;
        private SvcJnsSertifikatSelect.InputParameters parInpJnsSerti;
        private SvcJnsSertifikatSelect.OutputParameters parOutJnsSerti;
        private SvcJnsSertifikatSelect.BPSIMANSROW_R_JNS_SERTI rowDataJnsSerti;

        private SvcSmilikSelect.svcDsRSmilikSelect_pttClient svcCallerDokMilik;
        private SvcSmilikSelect.InputParameters parInpDokMilik;
        private SvcSmilikSelect.OutputParameters parOutDokMilik;
        private SvcSmilikSelect.BPSIMANSROW_R_SMILIK rowDataDokMilik;

        private SvcAstJnsDok.dsRJnsDokSelect_pttClient svcCallerJnsDok;
        private SvcAstJnsDok.InputParameters parInpJnsDok;
        private SvcAstJnsDok.OutputParameters parOutJnsDok;
        private SvcAstJnsDok.BPSIMANSROW_R_JNS_DOK rowDataJnsDok;

        private SvcJenisDokumenBangunanSelect.call_pttClient svcCallerJnsDokBgn;
        private SvcJenisDokumenBangunanSelect.InputParameters parInpJnsDokBgn;
        private SvcJenisDokumenBangunanSelect.OutputParameters parOutJnsDokBgn;
        private SvcJenisDokumenBangunanSelect.BPSIMANSROW_R_JNS_DOK_BDG rowDataJnsDokBgn;

        private SvcRefAksesSelect.call_pttClient svcCallerAkses;
        private SvcRefAksesSelect.InputParameters parInpAkses;
        private SvcRefAksesSelect.OutputParameters parOutAkses;
        private SvcRefAksesSelect.BPSIMANSROW_R_LOK_AKSES rowDataAkses;

        private SvcRefBentukSelect.call_pttClient svcCallerBentuk;
        private SvcRefBentukSelect.InputParameters parInpBentuk;
        private SvcRefBentukSelect.OutputParameters parOutBentuk;
        private SvcRefBentukSelect.BPSIMANSROW_R_LOK_BENTUK rowDataBentuk;

        private SvcRefElevasiSelect.call_pttClient svcCallerElevasi;
        private SvcRefElevasiSelect.InputParameters parInpElevasi;
        private SvcRefElevasiSelect.OutputParameters parOutElevasi;
        private SvcRefElevasiSelect.BPSIMANSROW_R_LOK_ELEVASI rowDataElevasi;

        private SvcRefKonturSelect.call_pttClient svcCallerKontur;
        private SvcRefKonturSelect.InputParameters parInpKontur;
        private SvcRefKonturSelect.OutputParameters parOutKontur;
        private SvcRefKonturSelect.BPSIMANSROW_R_LOK_KONTUR rowDataKontur;

        private SvcRefPeruntukanSelect.call_pttClient svcCallerPeruntukkan;
        private SvcRefPeruntukanSelect.InputParameters parInpPeruntukkan;
        private SvcRefPeruntukanSelect.OutputParameters parOutPeruntukkan;
        private SvcRefPeruntukanSelect.BPSIMANSROW_R_LOK_PERUNTUKAN rowDataPeruntukkan;

        private SvcPelayananSelect.dsRPelayananSelect_pttClient svcCallerStatKelola;
        private SvcPelayananSelect.InputParameters parInpStatKelola;
        private SvcPelayananSelect.OutputParameters parOutStatKelola;
        private SvcPelayananSelect.BPSIMANSROW_R_PELAYANAN rowDataStatKelola;

        private SvcJnsPmkSelect.call_pttClient svcCallerJnsPemakai;
        private SvcJnsPmkSelect.InputParameters parInpJnsPemakai;
        private SvcJnsPmkSelect.OutputParameters parOutJnsPemakai;
        private SvcJnsPmkSelect.BPSIMANSROW_R_JNS_PMK rowDataJnsPemakai;

        private SvcStatusHukumSelect.call_pttClient svcCallerStatHukum;
        private SvcStatusHukumSelect.InputParameters parInpStatHukum;
        private SvcStatusHukumSelect.OutputParameters parOutStatHukum;
        private SvcStatusHukumSelect.BPSIMANSROW_STATUS_HUKUM rowDataStatHukum;

        private SvcSatkerSelect.dsRSatkerSelect_pttClient svcCallerSatker;
        private SvcSatkerSelect.InputParameters parInpSatker;
        private SvcSatkerSelect.OutputParameters parOutSatker;
        private SvcSatkerSelect.BPSIMANSROW_R_SATKER rowDataSatker;


        #endregion


        public Reference()
        {
            InitializeComponent(); Icon = Properties.Resources.logo_2016;
        }

        #region kondisi
        public void getinitKondisi()
        {
            progressBarControl1.PerformStep();

            parInp = new SvcKondisiSelect.InputParameters();
            parInp.P_COL = "";
            parInp.P_MAX = 100;
            parInp.P_MAXSpecified = true;
            parInp.P_MIN = 1;
            parInp.P_MINSpecified = true;
            parInp.P_SORT = "DESC";
            parInp.STR_WHERE = "";
            svcCaller = new SvcKondisiSelect.svcDsRKondisiSelect_pttClient();
            svcCaller.Beginexecute(parInp, new AsyncCallback(this.getKondisi), null);
        }

        public void getKondisi(IAsyncResult result)
        {
            try
            {
                parOut = svcCaller.Endexecute(result);
                svcCaller.Close();
                this.Invoke(new ShowData(this.showData), parOut);
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR Saat Load Data", "WARNING 1");
            }
        }

        public delegate void ShowData(SvcKondisiSelect.OutputParameters outData);

        public void showData(SvcKondisiSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_KONDISI.Count();

            DataRow dtRow;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow = kondisi.NewRow();
                dtRow["KD_KONDISI"] = serviceOut.SF_ROW_R_KONDISI[i].KD_KONDISI;
                dtRow["UR_KONDISI"] = serviceOut.SF_ROW_R_KONDISI[i].UR_KONDISI;
                kondisi.Rows.Add(dtRow);
                //MessageBox.Show(serviceOut.SF_ROW_R_KONDISI[i].UR_KONDISI, serviceOut.SF_ROW_R_KONDISI[i].KD_KONDISI);
                konfigApp.t_kondisi = kondisi;
            }

            this.getinitjnsserti();
        }
        #endregion

        #region jenis sertifikat
        public void getinitjnsserti() 
        {
            parInpJnsSerti = new SvcJnsSertifikatSelect.InputParameters();
            parInpJnsSerti.P_COL = "";
            parInpJnsSerti.P_MAX = 100;
            parInpJnsSerti.P_MAXSpecified = true;
            parInpJnsSerti.P_MIN = 0;
            parInpJnsSerti.P_MINSpecified = true;
            parInpJnsSerti.P_SORT = "ASC";
            parInpJnsSerti.STR_WHERE = "";
            svcCallerJnsSerti = new SvcJnsSertifikatSelect.call_pttClient();
            svcCallerJnsSerti.Beginexecute(parInpJnsSerti, new AsyncCallback(this.getdatajnsserti), null);
        }

        public void getdatajnsserti(IAsyncResult result)
        {
            parOutJnsSerti = svcCallerJnsSerti.Endexecute(result);
            svcCallerJnsSerti.Close();
            this.Invoke(new ShowDataJnsSerti(this.showDataJnsSerti), parOutJnsSerti);
        }

        public delegate void ShowDataJnsSerti(SvcJnsSertifikatSelect.OutputParameters dataOutJnsSerti);

        public void showDataJnsSerti(SvcJnsSertifikatSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_JNS_SERTI.Count();

            DataRow dtRow2;

            for (int i = 0; i < jmlData;i++ )
            {
                dtRow2 = TBL_R_JNS_SERTI.NewRow();
                dtRow2["KD_JNS_SERTI"] = serviceOut.SF_ROW_R_JNS_SERTI[i].KD_JNS_SERTI;
                dtRow2["NM_JNS_SERTI"] = serviceOut.SF_ROW_R_JNS_SERTI[i].NM_JNS_SERTI;
                TBL_R_JNS_SERTI.Rows.Add(dtRow2);

                konfigApp.VAR_DS_R_JNS_SERTI = TBL_R_JNS_SERTI;
            }
            this.getinitdokmilik();
        }
        #endregion

        #region dokumen kepemilikan
        public void getinitdokmilik()
        {
            parInpDokMilik = new SvcSmilikSelect.InputParameters();
            parInpDokMilik.P_COL = "";
            parInpDokMilik.P_MAX = 100;
            parInpDokMilik.P_MAXSpecified = true;
            parInpDokMilik.P_MIN = 0;
            parInpDokMilik.P_MINSpecified = true;
            parInpDokMilik.P_SORT = "ASC";
            parInpJnsSerti.STR_WHERE = "";
            svcCallerDokMilik = new SvcSmilikSelect.svcDsRSmilikSelect_pttClient();
            svcCallerDokMilik.Beginexecute(parInpDokMilik, new AsyncCallback(this.getdatadokmilik), null);
        }

        public void getdatadokmilik(IAsyncResult result)
        {
            parOutDokMilik = svcCallerDokMilik.Endexecute(result);
            svcCallerDokMilik.Close();
            this.Invoke(new ShowDataDokMilik(this.showDataDokMilik), parOutDokMilik);
        }

        public delegate void ShowDataDokMilik(SvcSmilikSelect.OutputParameters dataOutDokMilik);

        public void showDataDokMilik(SvcSmilikSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_SMILIK.Count();

            DataRow dtRow3;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow3 = TBL_R_SMILIK.NewRow();
                dtRow3["KD_SMILIK"] = serviceOut.SF_ROW_R_SMILIK[i].KD_SMILIK;
                dtRow3["UR_SMILIK"] = serviceOut.SF_ROW_R_SMILIK[i].UR_SMILIK;
                dtRow3["UR_DOK"] = serviceOut.SF_ROW_R_SMILIK[i].UR_DOK;
                TBL_R_SMILIK.Rows.Add(dtRow3);

                konfigApp.VAR_DS_R_SMILIK = TBL_R_SMILIK;
            }
            //getinitjnsdok();
            getinitjnsdokbgn();
        }
        #endregion

        #region jenis dokumen
        public void getinitjnsdok()
        {
            parInpJnsDok = new SvcAstJnsDok.InputParameters();
            parInpJnsDok.P_COL = "";
            parInpJnsDok.P_MAX = 100;
            parInpJnsDok.P_MAXSpecified = true;
            parInpJnsDok.P_MIN = 0;
            parInpJnsDok.P_MINSpecified = true;
            parInpJnsDok.P_SORT = "ASC";
            parInpJnsDok.STR_WHERE = "";
            svcCallerJnsDok = new SvcAstJnsDok.dsRJnsDokSelect_pttClient();
            svcCallerJnsDok.Beginexecute(parInpJnsDok, new AsyncCallback(this.getdatajnsdok), null);
        }

        public void getdatajnsdok(IAsyncResult result)
        {
            parOutJnsDok = svcCallerJnsDok.Endexecute(result);
            svcCallerJnsDok.Close();
            this.Invoke(new ShowDataJnsDok(this.showDataJnsDok), parOutJnsDok);
        }

        public delegate void ShowDataJnsDok(SvcAstJnsDok.OutputParameters dataOutJnsDok);

        public void showDataJnsDok(SvcAstJnsDok.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_JNS_DOK.Count();

            DataRow dtRow4;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow4 = TBL_R_JNS_DOK.NewRow();
                dtRow4["KD_DOK"] = serviceOut.SF_ROW_R_JNS_DOK[i].KD_DOK;
                dtRow4["NM_DOK"] = serviceOut.SF_ROW_R_JNS_DOK[i].NM_DOK;

                TBL_R_JNS_DOK.Rows.Add(dtRow4);

                konfigApp.VAR_DS_R_JNS_DOK = TBL_R_JNS_DOK;
            }
            getinitjnsdokbgn();
        }
        #endregion
        
        #region jenis dokumen bangunan
        public void getinitjnsdokbgn()
        {
            parInpJnsDokBgn = new SvcJenisDokumenBangunanSelect.InputParameters();
            parInpJnsDokBgn.P_COL = "";
            parInpJnsDokBgn.P_MAX = 100;
            parInpJnsDokBgn.P_MAXSpecified = true;
            parInpJnsDokBgn.P_MIN = 0;
            parInpJnsDokBgn.P_MINSpecified = true;
            parInpJnsDokBgn.P_SORT = "ASC";
            parInpJnsDokBgn.STR_WHERE = "";
            svcCallerJnsDokBgn = new SvcJenisDokumenBangunanSelect.call_pttClient();
            svcCallerJnsDokBgn.Beginexecute(parInpJnsDokBgn, new AsyncCallback(this.getdatajnsdokbgn), null);
        }

        public void getdatajnsdokbgn(IAsyncResult result)
        {
            parOutJnsDokBgn = svcCallerJnsDokBgn.Endexecute(result);
            svcCallerJnsDokBgn.Close();
            this.Invoke(new ShowDataJnsDokBgn(this.showDataJnsDokBgn), parOutJnsDokBgn);
        }

        public delegate void ShowDataJnsDokBgn(SvcJenisDokumenBangunanSelect.OutputParameters dataOutJnsDokBgn);

        public void showDataJnsDokBgn(SvcJenisDokumenBangunanSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_JNS_DOK_BDG.Count();

            DataRow dtRow5;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow5 = TBL_R_JNS_DOK_BDG.NewRow();
                dtRow5["KD_JNS_DOK_BDG"] = serviceOut.SF_ROW_R_JNS_DOK_BDG[i].KD_JNS_DOK_BDG;
                dtRow5["UR_JNS_DOK_BDG"] = serviceOut.SF_ROW_R_JNS_DOK_BDG[i].UR_JNS_DOK_BDG;

                TBL_R_JNS_DOK_BDG.Rows.Add(dtRow5);

                konfigApp.VAR_DS_R_JNS_DOK_BDG = TBL_R_JNS_DOK_BDG;
            }
            getinitakses();
        }
        #endregion

        #region lokasi akses
        public void getinitakses()
        {
            parInpAkses = new SvcRefAksesSelect.InputParameters();
            parInpAkses.P_COL = "";
            parInpAkses.P_MAX = 100;
            parInpAkses.P_MAXSpecified = true;
            parInpAkses.P_MIN = 0;
            parInpAkses.P_MINSpecified = true;
            parInpAkses.P_SORT = "ASC";
            parInpAkses.STR_WHERE = "";
            svcCallerAkses = new SvcRefAksesSelect.call_pttClient();
            svcCallerAkses.Beginexecute(parInpAkses, new AsyncCallback(this.getdataAkses), null);
        }

        public void getdataAkses(IAsyncResult result)
        {
            parOutAkses = svcCallerAkses.Endexecute(result);
            svcCallerAkses.Close();
            this.Invoke(new ShowDataAkses(this.showDataAkses), parOutAkses);
        }

        public delegate void ShowDataAkses(SvcRefAksesSelect.OutputParameters dataOutAkses);

        public void showDataAkses(SvcRefAksesSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_LOK_AKSES.Count();

            DataRow dtRow6;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow6 = TBL_R_LOK_AKSES.NewRow();
                dtRow6["KD_AKSES"] = serviceOut.SF_ROW_R_LOK_AKSES[i].KD_AKSES;
                dtRow6["UR_AKSES"] = serviceOut.SF_ROW_R_LOK_AKSES[i].UR_AKSES;

                TBL_R_LOK_AKSES.Rows.Add(dtRow6);

                konfigApp.VAR_DS_R_LOK_AKSES = TBL_R_LOK_AKSES;
            }
            getinitbentuk();
        }
        #endregion 

        #region lokasi bentuk
        public void getinitbentuk()
        {
            parInpBentuk = new SvcRefBentukSelect.InputParameters();
            parInpBentuk.P_COL = "";
            parInpBentuk.P_MAX = 100;
            parInpBentuk.P_MAXSpecified = true;
            parInpBentuk.P_MIN = 0;
            parInpBentuk.P_MINSpecified = true;
            parInpBentuk.P_SORT = "ASC";
            parInpBentuk.STR_WHERE = "";
            svcCallerBentuk = new SvcRefBentukSelect.call_pttClient();
            svcCallerBentuk.Beginexecute(parInpBentuk, new AsyncCallback(this.getdataBentuk), null);
        }

        public void getdataBentuk(IAsyncResult result)
        {
            parOutBentuk = svcCallerBentuk.Endexecute(result);
            svcCallerAkses.Close();
            this.Invoke(new ShowDataBentuk(this.showDataBentuk), parOutBentuk);
        }

        public delegate void ShowDataBentuk(SvcRefBentukSelect.OutputParameters dataOutBentuk);

        public void showDataBentuk(SvcRefBentukSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_LOK_BENTUK.Count();

            DataRow dtRow7;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow7 = TBL_R_LOK_BENTUK.NewRow();
                dtRow7["KD_BENTUK"] = serviceOut.SF_ROW_R_LOK_BENTUK[i].KD_BENTUK;
                dtRow7["UR_BENTUK"] = serviceOut.SF_ROW_R_LOK_BENTUK[i].UR_BENTUK;

                TBL_R_LOK_BENTUK.Rows.Add(dtRow7);

                konfigApp.VAR_DS_R_LOK_BENTUK = TBL_R_LOK_BENTUK;
            }
            getinitelevasi();
        }
        #endregion 
     
        #region lokasi elevasi
        public void getinitelevasi()
        {
            parInpElevasi = new SvcRefElevasiSelect.InputParameters();
            parInpElevasi.P_COL = "";
            parInpElevasi.P_MAX = 100;
            parInpElevasi.P_MAXSpecified = true;
            parInpElevasi.P_MIN = 0;
            parInpElevasi.P_MINSpecified = true;
            parInpElevasi.P_SORT = "ASC";
            parInpElevasi.STR_WHERE = "";
            svcCallerElevasi = new SvcRefElevasiSelect.call_pttClient();
            svcCallerElevasi.Beginexecute(parInpElevasi, new AsyncCallback(this.getdataElevasi), null);
        }

        public void getdataElevasi(IAsyncResult result)
        {
            parOutElevasi = svcCallerElevasi.Endexecute(result);
            svcCallerElevasi.Close();
            this.Invoke(new ShowDataElevasi(this.showDataElevasi), parOutElevasi);
        }

        public delegate void ShowDataElevasi(SvcRefElevasiSelect.OutputParameters dataOutElevasi);

        public void showDataElevasi(SvcRefElevasiSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_LOK_ELEVASI.Count();

            DataRow dtRow8;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow8 = TBL_R_LOK_ELEVASI.NewRow();
                dtRow8["KD_ELEVASI"] = serviceOut.SF_ROW_R_LOK_ELEVASI[i].KD_ELEVASI;
                dtRow8["UR_ELEVASI"] = serviceOut.SF_ROW_R_LOK_ELEVASI[i].UR_ELEVASI;

                TBL_R_LOK_ELEVASI.Rows.Add(dtRow8);

                konfigApp.VAR_DS_R_LOK_ELEVASI = TBL_R_LOK_ELEVASI;
            }

            getinitkontur();
        }
        #endregion

        #region lokasi kontur
        public void getinitkontur()
        {
            parInpKontur = new SvcRefKonturSelect.InputParameters();
            parInpKontur.P_COL = "";
            parInpKontur.P_MAX = 100;
            parInpKontur.P_MAXSpecified = true;
            parInpKontur.P_MIN = 0;
            parInpKontur.P_MINSpecified = true;
            parInpKontur.P_SORT = "ASC";
            parInpKontur.STR_WHERE = "";
            svcCallerKontur = new SvcRefKonturSelect.call_pttClient();
            svcCallerKontur.Beginexecute(parInpKontur, new AsyncCallback(this.getdataKontur), null);
        }

        public void getdataKontur(IAsyncResult result)
        {
            parOutKontur = svcCallerKontur.Endexecute(result);
            svcCallerKontur.Close();
            this.Invoke(new ShowDataKontur(this.showDataKontur), parOutKontur);
        }

        public delegate void ShowDataKontur(SvcRefKonturSelect.OutputParameters dataOutKontur);

        public void showDataKontur(SvcRefKonturSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_LOK_KONTUR.Count();

            DataRow dtRow9;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow9 = TBL_R_LOK_KONTUR.NewRow();
                dtRow9["KD_KONTUR"] = serviceOut.SF_ROW_R_LOK_KONTUR[i].KD_KONTUR;
                dtRow9["UR_KONTUR"] = serviceOut.SF_ROW_R_LOK_KONTUR[i].UR_KONTUR;

                TBL_R_LOK_KONTUR.Rows.Add(dtRow9);

                konfigApp.VAR_DS_R_LOK_KONTUR = TBL_R_LOK_KONTUR;
            }
            getinitperuntukkan();
        }
        #endregion

        #region lokasi peruntukkan
        public void getinitperuntukkan()
        {
            parInpPeruntukkan = new SvcRefPeruntukanSelect.InputParameters();
            parInpPeruntukkan.P_COL = "";
            parInpPeruntukkan.P_MAX = 100;
            parInpPeruntukkan.P_MAXSpecified = true;
            parInpPeruntukkan.P_MIN = 0;
            parInpPeruntukkan.P_MINSpecified = true;
            parInpPeruntukkan.P_SORT = "ASC";
            parInpPeruntukkan.STR_WHERE = "";
            svcCallerPeruntukkan = new SvcRefPeruntukanSelect.call_pttClient();
            svcCallerPeruntukkan.Beginexecute(parInpPeruntukkan, new AsyncCallback(this.getdataPeruntukkan), null);
        }

        public void getdataPeruntukkan(IAsyncResult result)
        {
            parOutPeruntukkan = svcCallerPeruntukkan.Endexecute(result);
            svcCallerPeruntukkan.Close();
            this.Invoke(new ShowDataPeruntukkan(this.showDataPeruntukkan), parOutPeruntukkan);
        }

        public delegate void ShowDataPeruntukkan(SvcRefPeruntukanSelect.OutputParameters dataOutPeruntukkan);

        public void showDataPeruntukkan(SvcRefPeruntukanSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_LOK_PERUNTUKAN.Count();

            DataRow dtRow10;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow10 = TBL_R_LOK_PERUNTUKAN.NewRow();
                dtRow10["KD_PERUNTUKAN"] = serviceOut.SF_ROW_R_LOK_PERUNTUKAN[i].KD_PERUNTUKAN;
                dtRow10["UR_PERUNTUKAN"] = serviceOut.SF_ROW_R_LOK_PERUNTUKAN[i].UR_PERUNTUKAN;

                TBL_R_LOK_PERUNTUKAN.Rows.Add(dtRow10);

                konfigApp.VAR_DS_R_LOK_PERUNTUKAN = TBL_R_LOK_PERUNTUKAN;
            }
            getinitstatkelola();
        }
        #endregion

        #region status pengeloaan
        public void getinitstatkelola()
        {
            parInpStatKelola = new SvcPelayananSelect.InputParameters();
            parInpStatKelola.P_COL = "";
            parInpStatKelola.P_MAX = 100;
            parInpStatKelola.P_MAXSpecified = true;
            parInpStatKelola.P_MIN = 0;
            parInpStatKelola.P_MINSpecified = true;
            parInpStatKelola.P_SORT = "ASC";
            parInpStatKelola.STR_WHERE = "";
            svcCallerStatKelola = new SvcPelayananSelect.dsRPelayananSelect_pttClient();
            svcCallerStatKelola.Beginexecute(parInpStatKelola, new AsyncCallback(this.getdataStatKelola), null);
        }

        public void getdataStatKelola(IAsyncResult result)
        {
            parOutStatKelola = svcCallerStatKelola.Endexecute(result);
            svcCallerStatKelola.Close();
            this.Invoke(new ShowDataStatKelola(this.showDataStatKelola), parOutStatKelola);
        }

        public delegate void ShowDataStatKelola(SvcPelayananSelect.OutputParameters dataOutStatKelola);

        public void showDataStatKelola(SvcPelayananSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_PELAYANAN.Count();

            DataRow dtRow11;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow11 = TBL_R_PELAYANAN.NewRow();
                dtRow11["KD_PELAYANAN"] = serviceOut.SF_ROW_R_PELAYANAN[i].KD_PELAYANAN;
                dtRow11["NM_PELAYANAN"] = serviceOut.SF_ROW_R_PELAYANAN[i].NM_PELAYANAN;

                TBL_R_PELAYANAN.Rows.Add(dtRow11);

                konfigApp.VAR_DS_R_PELAYANAN = TBL_R_PELAYANAN;
            }
            getinitjnspemakai();
        }
        #endregion

        #region jenis pemakai
        public void getinitjnspemakai()
        {
            parInpJnsPemakai = new SvcJnsPmkSelect.InputParameters();
            parInpJnsPemakai.P_COL = "";
            parInpJnsPemakai.P_MAX = 100;
            parInpJnsPemakai.P_MAXSpecified = true;
            parInpJnsPemakai.P_MIN = 0;
            parInpJnsPemakai.P_MINSpecified = true;
            parInpJnsPemakai.P_SORT = "ASC";
            parInpJnsPemakai.STR_WHERE = "";
            svcCallerJnsPemakai = new SvcJnsPmkSelect.call_pttClient();
            svcCallerJnsPemakai.Beginexecute(parInpJnsPemakai, new AsyncCallback(this.getdatajnspemakai), null);
        }

        public void getdatajnspemakai(IAsyncResult result)
        {
            parOutJnsPemakai= svcCallerJnsPemakai.Endexecute(result);
            svcCallerJnsPemakai.Close();
            this.Invoke(new ShowDataJnsPemakai(this.showDataJnsPemakai), parOutJnsPemakai);
        }

        public delegate void ShowDataJnsPemakai(SvcJnsPmkSelect.OutputParameters dataOutJnsPemakai);

        public void showDataJnsPemakai(SvcJnsPmkSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_JNS_PMK.Count();

            DataRow dtRow12;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow12 = TBL_R_JNS_PMK.NewRow();
                dtRow12["KD_PMK"] = serviceOut.SF_ROW_R_JNS_PMK[i].KD_PMK;
                dtRow12["JNS_PMK"] = serviceOut.SF_ROW_R_JNS_PMK[i].JNS_PMK;

                TBL_R_JNS_PMK.Rows.Add(dtRow12);

                konfigApp.VAR_DS_R_JNS_PMK = TBL_R_JNS_PMK;
            }
            getinitStatusHukum();
        }
        #endregion 

        #region status hukum
        public void getinitStatusHukum()
        {
            parInpStatHukum = new SvcStatusHukumSelect.InputParameters();
            parInpStatHukum.P_COL = "";
            parInpStatHukum.P_MAX = 100;
            parInpStatHukum.P_MAXSpecified = true;
            parInpStatHukum.P_MIN = 0;
            parInpStatHukum.P_MINSpecified = true;
            parInpStatHukum.P_SORT = "ASC";
            parInpStatHukum.STR_WHERE = "";
            svcCallerStatHukum = new SvcStatusHukumSelect.call_pttClient();
            svcCallerStatHukum.Beginexecute(parInpStatHukum, new AsyncCallback(this.getdataStatHukum), null);
        }

        public void getdataStatHukum(IAsyncResult result)
        {
            parOutStatHukum = svcCallerStatHukum.Endexecute(result);
            svcCallerStatHukum.Close();
            this.Invoke(new ShowDataStatHukum(this.showDataStatHukum), parOutStatHukum);
        }

        public delegate void ShowDataStatHukum(SvcStatusHukumSelect.OutputParameters dataOutStatHukum);

        public void showDataStatHukum(SvcStatusHukumSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_STATUS_HUKUM.Count();

            DataRow dtRow13;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow13 = TBL_R_STATUS_HUKUM.NewRow();
                dtRow13["JNS_STATUS_HUKUM"] = serviceOut.SF_ROW_R_STATUS_HUKUM[i].JNS_STATUS_HUKUM;
                dtRow13["KD_STATUS_HUKUM"] = serviceOut.SF_ROW_R_STATUS_HUKUM[i].KD_STATUS_HUKUM;
                dtRow13["STATUS_HUKUM"] = serviceOut.SF_ROW_R_STATUS_HUKUM[i].STATUS_HUKUM;

                TBL_R_STATUS_HUKUM.Rows.Add(dtRow13);

                konfigApp.VAR_DS_R_STATUS_HUKUM = TBL_R_STATUS_HUKUM;
            }
            getinitSatker();
        }
        #endregion 

        #region satker
        public void getinitSatker()
        {
            parInpSatker = new SvcSatkerSelect.InputParameters();
            parInpSatker.P_COL = "";
            parInpSatker.P_MAX = 100;
            parInpSatker.P_MAXSpecified = true;
            parInpSatker.P_MIN = 0;
            parInpSatker.P_MINSpecified = true;
            parInpSatker.P_SORT = "ASC";
            parInpSatker.STR_WHERE = "";
            svcCallerSatker = new SvcSatkerSelect.dsRSatkerSelect_pttClient();
            svcCallerSatker.Beginexecute(parInpSatker, new AsyncCallback(this.getdataSatker), null);
        }

        public void getdataSatker(IAsyncResult result)
        {
            parOutSatker = svcCallerSatker.Endexecute(result);
            svcCallerSatker.Close();
            this.Invoke(new ShowDataSatker(this.showDataSatker), parOutSatker);
        }

        public delegate void ShowDataSatker(SvcSatkerSelect.OutputParameters dataOutSatker);

        public void showDataSatker(SvcSatkerSelect.OutputParameters serviceOut)
        {
            int jmlData = serviceOut.SF_ROW_R_SATKER.Count();

            DataRow dtRow14;

            for (int i = 0; i < jmlData; i++)
            {
                dtRow14 = TBL_R_SATKER.NewRow();
                dtRow14["KD_SATKER"] = serviceOut.SF_ROW_R_SATKER[i].KD_SATKER;
                dtRow14["UR_SATKER"] = serviceOut.SF_ROW_R_SATKER[i].UR_SATKER;
                dtRow14["ID_SATKER"] = serviceOut.SF_ROW_R_SATKER[i].ID_SATKER;
                TBL_R_SATKER.Rows.Add(dtRow14);

                konfigApp.VAR_DS_R_SATKER = TBL_R_SATKER;
            }
            this.Hide();
        }
        #endregion
    }
}