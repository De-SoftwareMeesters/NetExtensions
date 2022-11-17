using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetExtensions.NetCore.DateAndTime
{
    public static class Extensions
    {
        /// <summary>
        /// This method returns the YYYYMMDD equivalent of the date you put in it.
        /// It returns an integer
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        /// <exception cref="ConversionException"></exception>
        public static int ToYYYYMMDD(this DateTime dt)
        {
            var y = dt.Year.ToString(); var m = dt.Month.ToString(); var d = dt.Day.ToString();
            var ymd = $"{y}{m.PadLeft(2, '0')}{d.PadLeft(2, '0')}";
            int result = 0;
            if (!int.TryParse(ymd, out result)) throw new ConversionException($"Error converting date {dt.ToShortDateString()} to integer");
            return result;
        }

        /// <summary>
        /// This method tries to convert a string to a DateTime.
        /// I'm dutch, so I expect you to pass in a string in the Dutch date format (DD-MM-YYYY)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ConversionException"></exception>
        public static DateTime ToDateTime(this string s)
        {
            try
            {
                int d, m, j;
                int.TryParse(s.Substring(0, 2), out d);
                int.TryParse(s.Substring(3, 2), out m);
                int.TryParse(s.Substring(6, 4), out j);

                return new DateTime(j, m, d);
            }
            catch { throw new ConversionException($"Error converting string {s} to DateTime"); }
        }

    }


}

namespace NetExtensions
{
    public class ConversionException : Exception
    {
        public ConversionException(string message) : base(message)
        {

        }
    }
}
