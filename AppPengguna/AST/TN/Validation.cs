using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppPengguna.AST.TN
{
    class Validation
    {

        public static string convertDateTime(string dateTime, string format = null)
        {
            string[] splitDate = dateTime.Split('/');

            int cekDate = splitDate.Count();
            if (cekDate == 3)
            {
                dateTime = splitDate[1] + "/" + splitDate[0] + "/" + splitDate[2].Substring(0, 4);
            }
            else
            {
                dateTime = "";
            }

            if (dateTime == "11/11/2011") dateTime = "";


            return dateTime;
        }
    }
}
