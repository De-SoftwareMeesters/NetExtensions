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

        public static int AsYYYYMMDD(this DateTime dt)
        {
            var y = dt.Year.ToString(); var m = dt.Month.ToString(); var d = dt.Day.ToString();
            var ymd = $"{y}{m.PadLeft(2, '0')}{d.PadLeft(2, '0')}";
            int result = 0;
            if (!int.TryParse(ymd, out result)) throw new ConversionException("Error converting date to integer");
            return result;
        }

    }


}

namespace NetExtensions
{
    public class ConversionException : Exception
    {
      public ConversionException(string message):base(message)
        {
            
        }
    }
}
