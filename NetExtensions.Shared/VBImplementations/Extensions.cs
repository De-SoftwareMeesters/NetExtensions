using System;
using System.Collections.Generic;
using System.Text;

namespace NetExtensions
{
    public static class VBImplementations
    {
        public static T With<T>(this T item, Action<T> action)
        {
            action(item);
            return item;
        }
    }
}
