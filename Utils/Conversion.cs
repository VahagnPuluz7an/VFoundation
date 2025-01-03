using System;
using System.Globalization;

namespace Utils
{
    public static class Conversion
    {
        public static string ToStringInvariant(this float value, int decimals = 2, bool round = true)
        {
            if (round)
            {
                value = value.RoundToDecimal(decimals);
            }
            
            var format = decimals < 1
                ? "0"
                : $"0.{new string('0', decimals)}";

            return value.ToString(format, CultureInfo.InvariantCulture);
        }
        
        public static float RoundToDecimal(this float value, int decimals)
        {
            return (float)Math.Round(value, decimals);
        }
    }
}