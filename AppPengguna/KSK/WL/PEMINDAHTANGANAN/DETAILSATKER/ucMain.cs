using System.Windows.Forms;

namespace AppPengguna.KSK.WL.PEMINDAHTANGAN.DETAILSATKER
{
    public partial class ucMain : UserControl
    {
        public ucMain()
        {
            InitializeComponent();
        }

    }

    public partial class SudahPSPItem 
    {
        public decimal? NUM { get; set; }
        private decimal? _kd_jenis_bmn;
        public decimal? KD_JENIS_BMN
        {
            get
            {
                return _kd_jenis_bmn;
            }
            set
            {
                _kd_jenis_bmn = value;

                switch (_kd_jenis_bmn.ToString())
                {
                    case "1":
                        NM_JENIS_BMN = "TANAH";
                        break;
                    case "2":
                        NM_JENIS_BMN = "BANGUNAN";
                        break;
                    case "41":
                        NM_JENIS_BMN = "STB YANG MEMILIKI BUKTI KEPEMILIKAN";
                        break;
                    case "42":
                        NM_JENIS_BMN = "STB YANG TIDAK MEMILIKI BUKTI KEPEMILIKAN";
                        break;
                }
            }
        }
        public string NM_JENIS_BMN{ get; set; }

        public decimal? ID_SATKER { get; set; }
        public string KD_SATKER { get; set; }
        public string UR_SATKER { get; set; }
        public decimal? KUANTITAS_PSP_Y { get; set; }
        public decimal? LUAS_PSP_Y { get; set; }
        public decimal? NIL_PERLH_Y { get; set; }

        public SudahPSPItem() { }

        public static SudahPSPItem[] GetPSP() {

            SudahPSPItem[] item = new SudahPSPItem[4];
            item[0] = new SudahPSPItem() { KD_JENIS_BMN = 1, KUANTITAS_PSP_Y = 0, LUAS_PSP_Y = 0, NIL_PERLH_Y = 0};
            item[1] = new SudahPSPItem() { KD_JENIS_BMN = 2, KUANTITAS_PSP_Y = 0, LUAS_PSP_Y = 0, NIL_PERLH_Y = 0};
            item[2] = new SudahPSPItem() { KD_JENIS_BMN = 41, KUANTITAS_PSP_Y = 0, LUAS_PSP_Y = 0, NIL_PERLH_Y = 0 };
            item[3] = new SudahPSPItem() { KD_JENIS_BMN = 42, KUANTITAS_PSP_Y = 0, LUAS_PSP_Y = 0, NIL_PERLH_Y = 0 };
            return item;
        }
    }

    public partial class BelumPSPItem
    {
        public decimal? NUM { get; set; }
        private decimal? _kd_jenis_bmn;
        public decimal? KD_JENIS_BMN
        {
            get
            {
                return _kd_jenis_bmn;
            }
            set
            {
                _kd_jenis_bmn = value;

                switch (_kd_jenis_bmn.ToString())
                {
                    case "1":
                        NM_JENIS_BMN = "TANAH";
                        break;
                    case "2":
                        NM_JENIS_BMN = "BANGUNAN";
                        break;
                    case "41":
                        NM_JENIS_BMN = "STB YANG MEMILIKI BUKTI KEPEMILIKAN";
                        break;
                    case "42":
                        NM_JENIS_BMN = "STB YANG TIDAK MEMILIKI BUKTI KEPEMILIKAN";
                        break;
                }
            }
        }
        public string NM_JENIS_BMN { get; set; }

        public decimal? ID_SATKER { get; set; }
        public string KD_SATKER { get; set; }
        public string UR_SATKER { get; set; }

        public decimal? PENGELOLA_KUANTITAS_PSP_N { get; set; }
        public decimal? PENGELOLA_LUAS_PSP_N { get; set; }
        public decimal? PENGELOLA_NIL_PERLH_PSP_N { get; set; }
        public decimal? PENGGUNA_KUANTITAS_PSP_N { get; set; }
        public decimal? PENGGUNA_LUAS_PSP_N { get; set; }
        public decimal? PENGGUNA_NIL_PERLH_PSP_N { get; set; }


        public BelumPSPItem() { }

        public static BelumPSPItem[] GetPSP()
        {

            BelumPSPItem[] item = new BelumPSPItem[4];
            item[0] = new BelumPSPItem() {   KD_JENIS_BMN = 1,PENGELOLA_KUANTITAS_PSP_N = 0,PENGELOLA_LUAS_PSP_N = 0, 
                                             PENGELOLA_NIL_PERLH_PSP_N = 0, PENGGUNA_KUANTITAS_PSP_N = 0, PENGGUNA_LUAS_PSP_N = 0,
                                             PENGGUNA_NIL_PERLH_PSP_N = 0, };
            item[1] = new BelumPSPItem() {   KD_JENIS_BMN = 2,PENGELOLA_KUANTITAS_PSP_N = 0,PENGELOLA_LUAS_PSP_N = 0, 
                                             PENGELOLA_NIL_PERLH_PSP_N = 0, PENGGUNA_KUANTITAS_PSP_N = 0, PENGGUNA_LUAS_PSP_N = 0,
                                             PENGGUNA_NIL_PERLH_PSP_N = 0, };
            item[2] = new BelumPSPItem() {   KD_JENIS_BMN = 41,PENGELOLA_KUANTITAS_PSP_N = 0,PENGELOLA_LUAS_PSP_N = 0, 
                                             PENGELOLA_NIL_PERLH_PSP_N = 0, PENGGUNA_KUANTITAS_PSP_N = 0, PENGGUNA_LUAS_PSP_N = 0,
                                             PENGGUNA_NIL_PERLH_PSP_N = 0, };
            item[3] = new BelumPSPItem() {   KD_JENIS_BMN = 42,PENGELOLA_KUANTITAS_PSP_N = 0,PENGELOLA_LUAS_PSP_N = 0, 
                                             PENGELOLA_NIL_PERLH_PSP_N = 0, PENGGUNA_KUANTITAS_PSP_N = 0, PENGGUNA_LUAS_PSP_N = 0,
                                             PENGGUNA_NIL_PERLH_PSP_N = 0, };
            return item;
        }
    }

    public partial class SengketaItem
    {
        public SengketaItem() { }

        public decimal? NUM { get; set; }
        private decimal? _kd_jenis_bmn;
        public decimal? KD_JENIS_BMN
        {
            get
            {
                return _kd_jenis_bmn;
            }
            set
            {
                _kd_jenis_bmn = value;

                switch (_kd_jenis_bmn.ToString())
                {
                    case "1":
                        NM_JENIS_BMN = "TANAH";
                        break;
                    case "2":
                        NM_JENIS_BMN = "BANGUNAN";
                        break;
                    case "41":
                        NM_JENIS_BMN = "STB YANG MEMILIKI BUKTI KEPEMILIKAN";
                        break;
                    case "42":
                        NM_JENIS_BMN = "STB YANG TIDAK MEMILIKI BUKTI KEPEMILIKAN";
                        break;
                }
            }
        }
        public string NM_JENIS_BMN { get; set; }

        public decimal? ID_SATKER { get; set; }
        public string KD_SATKER { get; set; }
        public string UR_SATKER { get; set; }

        public decimal? JML_TK_PERTAMA { get; set; }
        public decimal? JML_TK_BANDING { get; set; }
        public decimal? JML_TK_KASASI { get; set; }
        public decimal? JML_TK_PENINJAUAN_KEMBALI { get; set; }
        public static SengketaItem[] GetPSP()
        {

            SengketaItem[] item = new SengketaItem[4];
            item[0] = new SengketaItem()
            {
                KD_JENIS_BMN = 1,
                JML_TK_BANDING = 0,
                JML_TK_PENINJAUAN_KEMBALI = 0,
                JML_TK_KASASI = 0,
                JML_TK_PERTAMA = 0,
            };
            item[1] = new SengketaItem()
            {
                KD_JENIS_BMN = 2,
                JML_TK_BANDING = 0,
                JML_TK_PENINJAUAN_KEMBALI = 0,
                JML_TK_KASASI = 0,
                JML_TK_PERTAMA = 0,
            };
            item[2] = new SengketaItem()
            {
                KD_JENIS_BMN = 41,
                JML_TK_BANDING = 0,
                JML_TK_PENINJAUAN_KEMBALI = 0,
                JML_TK_KASASI = 0,
                JML_TK_PERTAMA = 0,
            };
            item[3] = new SengketaItem()
            {
                KD_JENIS_BMN = 42,
                JML_TK_BANDING = 0,
                JML_TK_PENINJAUAN_KEMBALI = 0,
                JML_TK_KASASI = 0,
                JML_TK_PERTAMA = 0,
            };
            return item;
        }

    }
}
