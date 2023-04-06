using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExtensions.NetCore.Types
{
    public static class Extensions
    {
        public static bool IsOnlyNumbers(this string s)
        {
            return s.All(char.IsNumber);
        }

        public static string NullThenEmpty(this string s)
        {
            return s ?? "";
        }

        public static int ConvertToInt(this string s)
        {
            int o = 0;
            if (int.TryParse(s, out o))
            {
                return o;
            }
            else
            {
                throw new ConversionException($"Cannot convert {s} to integer");
            }
        }

        public static decimal ConvertToDecimal(this string s, bool useCommaForDecimal = true)
        {
            decimal o = 0;
            var source = s;
            if (useCommaForDecimal) source = source.Replace('.', ',');
            if (decimal.TryParse(source, out o))
            {
                return o;
            }
            else
            {
                throw new ConversionException($"Cannot convert {s} to decimal");
            }
        }

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
        public static DateTime ConvertToDateTime(this string s, string format = "dd-mm-yyyy")
        {
            try
            {
                int d = DateTime.Now.Day;
                int m = DateTime.Now.Month;
                int j = DateTime.Now.Year;
                if (format == "dd-mm-yyyy")
                {

                    int.TryParse(s.Substring(0, 2), out d);
                    int.TryParse(s.Substring(3, 2), out m);
                    int.TryParse(s.Substring(6, 4), out j);
                }

                if (format == "yyyymmdd")
                {

                    int.TryParse(s.Substring(6, 2), out d);
                    int.TryParse(s.Substring(4, 2), out m);
                    int.TryParse(s.Substring(0, 4), out j);

                }

                return new DateTime(j, m, d);

            }
            catch { throw new ConversionException($"Error converting string {s} to DateTime"); }
        }

    }
    public class ConversionException : Exception
    {
        public ConversionException(string message) : base(message)
        {

        }
    }
}

