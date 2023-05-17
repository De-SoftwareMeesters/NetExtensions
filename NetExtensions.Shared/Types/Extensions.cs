using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExtensions
{
    public static class Types
    {
        public static bool IsOnlyNumbers(this string s)
        {
            return s.All(char.IsNumber);
        }

        public static string NullThenEmpty(this string s)
        {
            return s ?? "";
        }

        public static string TryTrim(this string s)
        {
            return s.NullThenEmpty().Trim();
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

        public static decimal TruncateAfter(this decimal value, int positions)
        {
            string _str = value.ToString();
            if (_str.Contains(",") || _str.Contains("."))
            {
                int _start = _str.IndexOf(",");
                if (_start == -1)
                {
                    _start = _str.IndexOf(".");
                }
                string _strTruncated = null;
                if (_str.Length > positions + _start)
                {
                    _strTruncated = _str.Remove(_start + positions + 1, _str.Length - (_start + positions) - 1);
                }
                else
                {
                    _strTruncated = value.ToString();
                }
                return Convert.ToDecimal(_strTruncated);
            }
            else
            {
                return Convert.ToDecimal(_str);
            }

        }

        public static decimal Round2(this decimal value)
        {
            return Convert.ToDecimal(Math.Round(value, 2));
        }

        public static decimal Round6(this decimal value)
        {
            return Convert.ToDecimal(Math.Round(value, 6));
        }

        public static decimal? Round2(this decimal? value)
        {
            if (value.HasValue)
            {
                return Math.Round(value.Value, 2);
            }
            else
            {
                return value;
            }
        }

        public static string ToArrayString(this string[] list)
        {
            string retval = "";

            foreach (var item in list)
            {
                retval += item + ",";
            }

            if (!string.IsNullOrEmpty(retval) && retval.Last() == ',') retval = retval.Remove(retval.Length - 1, 1);

            return retval;
        }

        public static void TrySet<T>(this T o, T value)
        {
            try
            {
                o = value;
            }
            catch (Exception)
            {

            }
        }

        public static string AddEol(this string s)
        {
            return $"{s}\r\n";
        }

      


    }
}

