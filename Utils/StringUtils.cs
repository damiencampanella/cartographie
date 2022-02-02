using System;
using System.Globalization;

namespace BlazorFlowly.Utils
{
    public class StringUtils
    {
        public static Single[] ExtractBoundsFromString(string input)
        {
            string boundsString = input.Replace("[", string.Empty).Replace("]", string.Empty);
            string[] bounds = boundsString.Split(',');
            Single[] result = new Single[bounds.Length];

            for (int i=0;i<bounds.Length;i++)
            {
                result[i] = Single.Parse(bounds[i], CultureInfo.InvariantCulture);
            }
            return result;
        }
    }
}
