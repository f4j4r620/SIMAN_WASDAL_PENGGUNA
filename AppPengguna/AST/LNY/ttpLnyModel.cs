using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppPengguna.AST.LNY
{
    class ttpLnyModel
    {
        public decimal? totalData = 0;
        public decimal? sum_rph_aset = 0;
        public decimal? sum_rph_susut = 0;
        public decimal? sum_rph_mutasi = 0;
        public decimal? sum_nilai_sblm_susut = 0;

        public decimal? _totalData
        {
            get { return totalData; }
            set { totalData = value; }
        }

        public decimal? _sum_rph_aset
        {
            get { return sum_rph_aset; }
            set { sum_rph_aset = value; }
        }

        public decimal? _sum_rph_susut
        {
            get { return sum_rph_susut; }
            set { sum_rph_susut = value; }
        }

        public decimal? _sum_rph_mutasi
        {
            get { return sum_rph_mutasi; }
            set { sum_rph_mutasi = value; }
        }

        public decimal? _sum_nilai_sblm_susut
        {
            get { return sum_nilai_sblm_susut; }
            set { sum_nilai_sblm_susut = value; }
        }
    }
}
