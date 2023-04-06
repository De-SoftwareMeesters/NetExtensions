using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetExtensions
{
    public static class DateAndTime
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
        /// Other supported formats are:
        /// yyyymmdd
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

        public static string ToDateString(this string s)
        {
            try
            {
                return string.Format("{0}-{1}-{2}", s.Substring(0, 4), s.Substring(4, 2), s.Substring(6, 2));

            }
            catch { return s; }

        }

        public static DateTime FirstMondayInYear(this int Year)
        {
            // get the date for the 4-Jan for this year
            DateTime dt = new DateTime(Year, 1, 4);

            // get the ISO day number for this date 1==Monday, 7==Sunday
            int dayNumber = (int)dt.DayOfWeek; // 0==Sunday, 6==Saturday
            if (dayNumber == 0)
            {
                dayNumber = 7;
            }

            // return the date of the Monday that is less than or equal
            // to this date
            return dt.AddDays(1 - dayNumber);
        }

        public static DateTime MondayInWeek(this DateTime dt)
        {
            // get the date for the 4-Jan for this year

            // get the ISO day number for this date 1==Monday, 7==Sunday
            int dayNumber = (int)dt.DayOfWeek; // 0==Sunday, 6==Saturday
            if (dayNumber == 0)
            {
                dayNumber = 7;
            }

            // return the date of the Monday that is less than or equal
            // to this date
            return dt.AddDays(1 - dayNumber);
        }

        public static YearWeek IsoWeek(this DateTime dt)
        {
            DateTime week1;
            int IsoYear = dt.Year;
            if (dt >= new DateTime(IsoYear, 12, 29))
            {
                week1 = (IsoYear + 1).FirstMondayInYear();
                if (dt < week1)
                {
                    week1 = FirstMondayInYear(IsoYear);
                }
                else
                {
                    IsoYear++;
                }
            }
            else
            {
                week1 = FirstMondayInYear(IsoYear);
                if (dt < week1)
                {
                    week1 = FirstMondayInYear(--IsoYear);
                }
            }

            return new YearWeek() { Year = (IsoYear), Week = ((dt - week1).Days / 7 + 1), Monday = dt.MondayInWeek() };
        }

        public static YearWeek IsoWeek(this int year, int week)
        {
            DateTime week1;
            week1 = FirstMondayInYear(year);
            var dt = week1.AddDays(7 * (week - 1));
            return new YearWeek() { Year = (year), Week = week, Monday = dt.MondayInWeek() };
        }

        public static YearWeek PreviousIsoWeek(this YearWeek wk)
        {
            return wk.Monday.AddDays(-7).IsoWeek(); 
        }

        public static YearWeek NextIsoWeek(this YearWeek wk)
        {
            return wk.Monday.AddDays(7).IsoWeek(); ;
        }

        public static DateTime FromYYYMMMDD(this string dt)
        {
            DateTime? retval = null;
            try
            {
                retval = String.Format("{0}-{1}-{2}", dt.Substring(0, 4), dt.Substring(4, 2), dt.Substring(6, 2)).ToDateTime();
            }
            catch (Exception ex)
            {
                throw new ConversionException("Conversion from " + dt + " to datetime is not supported", ex);
            }
            return retval.Value;
        }

        public static DateTime ToDateTime(this string s)
        {
            DateTime? retval = null;

            try
            {
                retval = Convert.ToDateTime(s);
            }
            catch (Exception ex)
            {
                throw new ConversionException("Conversion from " + s + " to datetime is not supported", ex);
            }

            return retval.Value;
        }

        public static string ToShortDateString(this DateTime? d, string Language = "NL")
        {
            if (d.HasValue)
            {
                return d.Value.ToShortDateString();
            }
            else
            {
                switch (Language)
                {
                    case "NL":
                        return "Geen datum bekend";
                    case "EN":
                        return "No date specified";
                    default:
                        return "No date specified";
                }

            }
        }

    }


}


