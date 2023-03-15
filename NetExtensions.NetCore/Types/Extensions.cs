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
    }
}
