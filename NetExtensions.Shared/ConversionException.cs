using System;

namespace NetExtensions
{
    public class ConversionException : Exception
    {
        public ConversionException(string message) : base(message)
        {

        }

        public ConversionException(string message,Exception ex) : base(message,ex)
        {

        }
    }

    
}

