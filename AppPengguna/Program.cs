using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Threading;

namespace AppPengguna
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static string[] term = new string[8];


        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CultureInfo cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "";
            cultureInfo.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            DevExpress.Utils.FormatInfo.AlwaysUseThreadFormat = true;

            #region Encrypt exe.config
            try{
                    Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath); 
                    ConfigurationSection sectionBinding = config.GetSection("system.serviceModel/bindings");
	                //ConfigurationSection sectionClient = config.GetSection("system.serviceModel/client");

	                if (!sectionBinding.SectionInformation.IsProtected)
	                {
                        /*Delete Key*/
                        RunProcess(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "aspnet_regiis.exe"), "-pz \"WasdalKeys\" ");
                        /*Create Key*/
                        RunProcess(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "aspnet_regiis.exe"), "-pc \"WasdalKeys\" ");
                        /*ACL Key*/
                        RunProcess(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "aspnet_regiis.exe"), "-pa \"WasdalKeys\" \"NT AUTHORITY\\NETWORK SERVICE\"");
		                /*Export Public Key*/
		                //RunProcess(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "aspnet_regiis.exe"), "-px \"RekonsiliasiKeys\" \"RekonsiliasiKeys.xml\" ");

		                sectionBinding.SectionInformation.ProtectSection("WasdalProvider");
		                config.Save();
	                }
                }
                catch(Exception ex)
                {
	                Console.WriteLine(ex.Message);
                }
            #endregion
            
            string temp2 = "";
            foreach (var temp in args)
            {
               temp2 = temp2.Trim() + temp.Trim();
            }
            string kodeLauncer = "CgAfsug3+JTtt/rkYJLoLw==";

            #region parameter konesi lokal
            string _paramSatker = "74579" + ";" + "500010199555501000KP" + ";" + "Setjen MPR" + ";" + "Jl Salemba Tengah No 1XXXX YYYY" + ";" + "KOTA JAKARTA PUSAT";
            //string _paramSatker = "37910" + ";" + "011010199403397000KP" + ";" + "SATKER KIRO" + ";" + "-" + ";" + "-";
            //_paramSatker = "53133" + ";" + "013090600408788000KD" + ";" + "DITJEN HAM PADA KANTOR WILAYAH KEMENTERIAN HUKUM DAN HAM ACEH" + ";" + "-" + ";" + "-";
            //_paramSatker = "6289" + ";" + "015080100527577000KD" + ";" + "KPPN MEULAOH" + ";" + "-" + ";" + "-";
            //_paramSatker = "63432" + ";" + "012010100683862000KP" + ";" + "BARANAHAN" + ";" + "-" + ";" + "-";
            _paramSatker = "10942" + ";" + "022040600413002000KD" + ";" + "BARANAHAN" + ";" + "-" + ";" + "-";
            _paramSatker = "30023" + ";" + "075010199436766000KP" + ";" + "SEKRETARIAT UTAMA BMKG" + ";" + "-" + ";" + "-";
            _paramSatker = "6299" + ";" + "015080200451562000KD" + ";" + "KPPN MEDAN II" + ";" + "-" + ";" + "-";
            _paramSatker = "45993" + ";" + "500040100888841000KD" + ";" + "SATKER LATIHAN 41" + ";" + "-" + ";" + "-";

            string _paramKorwil     = "28748" + ";" + "500010199" + ";" + "KORWIL32" + ";" + "jl. a. yani Jakarta Pusat  XA" + ";" + "JAKARTA PUSAT";
            string _paramEselon1    = "1702" + ";" + "50001" + ";" + "ESELON I 32" + ";" + "Jakarta";
            string _paramKl         = "181" + ";" + "500" + ";" + "KL 32" + ";" + "-";
            _paramKl                = "9" + ";" + "026" + ";" + "KEMENTERIAN PERTAHANAN" + ";" + "-"; // KEMENTERIAN PERTAHANAN
            string _paramKpknl      = "101" + ";" + "18001" + ";" + "KPKNL LATIHAN" + ";" + "jalan indonesia raya, kota latihan";
            string _paramKanwil     = "61" + ";" + "18 " + ";" + "KANTOR WILAYAH DJKN LATIHAN" + ";" + "JAKARTA";

            //satker
          //  string _paramUser = "40189" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "satker32" + ";" + "196501012001072001";
            //_paramUser = "134611" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "FAJAR" + ";" + "196501012001072001";
            //string _paramUser = "136859" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "013090600408788000KD" + ";" + "DITJEN HAM PADA KANTOR WILAYAH KEMENTERIAN HUKUM DAN HAM ACEH";
           //  _paramUser = "156770" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "500010199555501000KP" + ";" + "-";

            
            //korwil
            // _paramUser = "48773" + ";" + "3" + ";" + "KOORDINATOR UAPB-W" + ";" + "500040100KD" + ";" + "500040100KD";
            //ESELON1
            //_paramUser = "123134" + ";" + "11" + ";" + "KOORDINATOR UAPB-E1" + ";" + "50004" + ";" + "50004";
            //kl
            //_paramUser = "133959" + ";" + "4" + ";" + "KOORDINATOR UAPB" + ";" + "026" + ";" + "026";
            #endregion


            #region parameter konesi prod
           //  _paramSatker = "45507" + ";" + "500040100888832000TP" + ";" + "SATKER LATIHAN" + ";" + "-" + ";" + "-";
            //string _paramKorwil = "11793" + ";" + "500040100" + ";" + "KORWIL LATIHAN" + ";" + "-" + ";" + "-";
            //string _paramEselon1 = "321" + ";" + "50004" + ";" + "ESELON I LATIHAN" + ";" + "-";
            //string _paramKl = "181" + ";" + "500" + ";" + "Kementerian Lembaga Latihan" + ";" + "-";
            //string _paramKpknl = "101" + ";" + "18001" + ";" + "KANTOR PELAYANAN KEKAYAAN NEGARA DAN LELANG LATIHAN I" + ";" + "-";
            //string _paramKanwil = "61" + ";" + "18 " + ";" + "KANTOR WILAYAH DJKN LATIHAN" + ";" + "-";


            ////satker
            string _paramUser = "888832" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "500040100888832000KD" + ";" + "SATKER LATIHAN";
            //korwil
            //string _paramUser = "48773" + ";" + "3" + ";" + "KOORDINATOR UAPB-W" + ";" + "500040100KD" + ";" + "500040100KD";
            //ESELON1
            //string _paramUser = "97583" + ";" + "11" + ";" + "KOORDINATOR UAPB-E1" + ";" + "50004" + ";" + "50004";
            //kl
        //    string _paramUser = "48780" + ";" + "4" + ";" + "KOORDINATOR UAPB" + ";" + "500" + ";" + "500";
            #endregion

            #region user lain
            //koneksi ke djkn LATIHAN
            //string _paramSatker = "37489" + ";" + "500040100666670000KD" + ";" + "LATIHAN 70" + ";" + "-" + ";" + "-"; //djkn
            //string _paramKorwil = "10201" + ";" + "500040100" + ";" + "Koordinator Latihan" + ";" + "-" + ";" + "-"; //djkn
            //string _paramEselon1 = "321" + ";" + "50004" + ";" + "Eselon 1 Latihan" + ";" + "-"; //djkn
            //string _paramKl = "101" + ";" + "500" + ";" + " Kementerian Lembaga Latihan" + ";" + "-"; //djkn
            //string _paramKpknl = "500" + ";" + "18001" + ";" + "KPKNL LATIHAN" + ";" + "-"; //djkn
            //string _paramKanwil = "600" + ";" + "5000418" + ";" + "KANTOR WILAYAH DJKN LATIHAN" + ";" + "-"; //djkn
            //satker
            //string _paramUser = "40849" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "500040100666670000KD" + ";" + "500040100666670000KD"; //djkn

            //koneksi ke djkn2
            //string _paramSatker = "17245" + ";" + "025040500662330000KD" + ";" + "025040500662330000KD" + ";" + "-" + ";" + "-"; //djkn
            //string _paramKorwil = "9184" + ";" + "025040500" + ";" + "KANWIL KEMENAG PROV. JATIM (04)" + ";" + "-" + ";" + "-"; //djkn
            //string _paramEselon1 = "140" + ";" + "02504" + ";" + "DITJEN PENDIDIKAN ISLAM" + ";" + "-"; //djkn
            //string _paramKl = "19" + ";" + "025" + ";" + " KEMENTERIAN AGAMA RI" + ";" + "-"; //djkn
            //string _paramKpknl = "41" + ";" + "10103" + ";" + "KANTOR PELAYANAN KEKAYAAN NEGARA DAN LELANG MALANG" + ";" + "-"; //djkn
            //string _paramKanwil = "10" + ";" + "10 " + ";" + "KANTOR WILAYAH DJKN JAWA TIMUR" + ";" + "-"; //djkn
            //satker
            //string _paramUser = "93776" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "025040500662330000KD" + ";" + "025040500662330000KD"; //djkn

            //koneksi ke djkn3
            //string _paramSatker = "30848" + ";" + "082010199652669000KP" + ";" + "PUSAT TEKNOLOGI DAN DATA PENGINDERAAN JAUH" + ";" + "-" + ";" + "-"; //djkn
            //string _paramKorwil = "10696" + ";" + "082010199" + ";" + "KORWIL L A P A N" + ";" + "-" + ";" + "-"; //djkn
            //string _paramEselon1 = "258" + ";" + "02504" + ";" + "L A P A N" + ";" + "-"; //djkn
            //string _paramKl = "61" + ";" + "082" + ";" + " LEMBAGA PENERBANGAN DAN ANTARIKSA NASIONAL" + ";" + "-"; //djkn
            //string _paramKpknl = "26" + ";" + "07105" + ";" + "KPKNL JAKARTA V" + ";" + "-"; //djkn
            //string _paramKanwil = "7" + ";" + "19 " + ";" + "KANTOR WILAYAH DJKN LATIHAN2" + ";" + "-"; //djkn
            ////satker
            //string _paramUser = "114875" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "19880403201012200100" + ";" + "19880403201012200100"; //djkn

            ////koneksi ke djkn4
            //string _paramSatker = "13492" + ";" + "024120100632217000KD" + ";" + "POLITEKNIK KESEHATAN JAKARTA III" + ";" + "-" + ";" + "-"; //djkn
            //string _paramKorwil = "1063" + ";" + "024120100" + ";" + "POLITEKNIK KESEHATAN JAKARTA III" + ";" + "-" + ";" + "-"; //djkn
            //string _paramEselon1 = "136" + ";" + "02412" + ";" + "BADAN PENGEMBANGAN DAN PEMBERDAYAAN SDM KESEHATAN" + ";" + "-"; //djkn
            //string _paramKl = "18" + ";" + "024" + ";" + "KEMENTERIAN KESEHATAN" + ";" + "-"; //djkn
            //string _paramKpknl = "23" + ";" + "07102" + ";" + "KPKNL JAKARTA II" + ";" + "-"; //djkn
            //string _paramKanwil = "7" + ";" + "07" + ";" + "KANTOR WILAYAH DJKN DKI JAKARTA" + ";" + "-"; //djkn
            ////satker
            //string _paramUser = "108326" + ";" + "2" + ";" + "KOORDINATOR UAKPB" + ";" + "024120100632217000KD" + ";" + "024120100632217000KD"; //djkn
            #endregion

            term[0] = kodeLauncer;
            term[1] = _paramSatker;
            term[2] = _paramKorwil;
            term[3] = _paramEselon1;
            term[4] = _paramKl;
            term[5] = _paramKpknl;
            term[6] = _paramKanwil;
            term[7] = _paramUser;


            //buat test dibuka 22nya
         konfigApp.args = string.Join("#", term);
         string[] arg = term;

            //buat plugin
    //    konfigApp.args = temp2;
     //   string[] arg = temp2.Split(new char[] { '#' }); 


            //satker
            string[] paramSatker = arg[1].Split(new char[] { ';' });
            konfigApp.idSatker = Convert.ToDecimal(paramSatker[0]);
            konfigApp.kodeSatker = paramSatker[1];
            konfigApp.namaSatker = paramSatker[2];
            konfigApp.alamatsatker = paramSatker[3];
            konfigApp.kotasatker = paramSatker[4];

            //korwil
            string[] paramKorwil = arg[2].Split(new char[] { ';' });
            konfigApp.idKorwil = Convert.ToDecimal(paramKorwil[0]);
            konfigApp.kodeKorwil = paramKorwil[1];
            konfigApp.namaKorwil = paramKorwil[2];
            konfigApp.alamatkorwil = paramKorwil[3];
            konfigApp.kotakorwil = paramKorwil[4];


            //eselon
            string[] paramEselon = arg[3].Split(new char[] { ';' });
            konfigApp.idEselon1 = Convert.ToDecimal(paramEselon[0]);
            konfigApp.kodeEselon1 = paramEselon[1];
            konfigApp.namaEselon1 = paramEselon[2];
            konfigApp.alamateselon = paramEselon[3];

            //KL
            string[] paramKL = arg[4].Split(new char[] { ';' });
            konfigApp.idKl = Convert.ToDecimal(paramKL[0]);
            konfigApp.kodeKl = paramKL[1];
            konfigApp.namaKl = paramKL[2];
            konfigApp.alamatkl = paramKL[3];

            //KPKNL
            string[] paramKpknl = arg[5].Split(new char[] { ';' });
            konfigApp.idKpknl = Convert.ToDecimal(paramKpknl[0]);
            konfigApp.kodeKpknl = paramKpknl[1];
            konfigApp.namaKpknl = paramKpknl[2];
            konfigApp.alamatkpknl = paramKpknl[3];

            //KANWIL
            string[] paramKanwil = arg[6].Split(new char[] { ';' });
            konfigApp.idKanwil = Convert.ToDecimal(paramKanwil[0]);
            konfigApp.kodeKanwil = paramKanwil[1];
            konfigApp.namaKanwil = paramKanwil[2];
            

            //USER
            string[] paramUser = arg[7].Split(new char[] { ';' });
            konfigApp.idUser = Convert.ToDecimal(paramUser[0]);
            konfigApp.idGroup = Convert.ToDecimal(paramUser[1]);
            konfigApp.namaGroup = paramUser[2];
            konfigApp.namaUser = paramUser[3];
            konfigApp.nipPemohon = paramUser[4];


            //konfigApp.getDetailAset("1");
            
            //pengecekan kode launcher
            if (arg[0] != "CgAfsug3+JTtt/rkYJLoLw==")
            {
                Application.Exit();
            }
            else
            {
                if (konfigApp.idGroup == 2)
                {
                    konfigApp.levelUser = konfigApp.levelSatker;
                    Application.Run(new FrmKoorSatker());
                }
                else if (konfigApp.idGroup == 3)
                {
                    konfigApp.levelUser = konfigApp.levelKorwil;
                    Application.Run(new FrmKoorKorwil());
                }
                else if (konfigApp.idGroup == 11)
                {
                    konfigApp.levelUser = konfigApp.levelEselon1;
                    Application.Run(new FrmKoorEselon1());
                }
                else if (konfigApp.idGroup == 51)
                {
                    konfigApp.levelUser = konfigApp.levelKl;
                    Application.Run(new FrmKoorKL());
                }
                else if (konfigApp.idGroup == 4)
                {
                    konfigApp.levelUser = konfigApp.levelKl;
                    Application.Run(new FrmKoorKL());
                }
            }
        }

        private static void RunProcess(string processName, string arguments)
        {
            var newProcess = new ProcessStartInfo(processName);

            if (!string.IsNullOrEmpty(arguments))
                newProcess.Arguments = arguments;
            newProcess.CreateNoWindow = true;
            newProcess.ErrorDialog = true;
            newProcess.RedirectStandardError = true;
            newProcess.RedirectStandardInput = true;
            newProcess.RedirectStandardOutput = true;
            newProcess.UseShellExecute = false;
            using (var proc = new Process())
            {
                proc.StartInfo = newProcess;
                proc.Start();
                //Dialog.info(proc.StandardOutput.ReadToEnd());
                Console.WriteLine(proc.StandardOutput.ReadToEnd());
            }
        }


    }

}
