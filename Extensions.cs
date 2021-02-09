using System;
using System.Collections.Generic;

namespace RockPaperScissors
{
    public static class Extensions
    {
        public static string ToHex(this byte[] self)
        {
            return BitConverter.ToString(self).Replace("-", string.Empty);
        }

        public static int IndexOf<T>(this T[] self, T elem)
        {
            return Array.IndexOf(self, elem);
        }
    }
}