using System;
using System.Text.RegularExpressions;
using Jammehcow.YosherBot.Command.ColorMe.Models;

namespace Jammehcow.YosherBot.Command.ColorMe.Helpers
{
    public static class HexColourHelper
    {
        private static readonly Regex HexColourPattern = new("^#?(([A-F]|\\d){6}|([A-F]|\\d){3})$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Try to get an RGB byte struct from a full or shorthand hex string
        /// </summary>
        /// <param name="hexValue">The hex string to convert (may start with a #)</param>
        /// <param name="model">The model to pass out to if conversion was successful</param>
        /// <returns>Whether the string was converted to a hex colour structure</returns>
        public static bool TryGetColorFromHexString(string hexValue, out HexColorModel model)
        {
            if (!HexColourPattern.IsMatch(hexValue))
            {
                model = default;
                return false;
            }

            var cleanHexValue = hexValue;

            // Remove leading hash if present
            if (cleanHexValue[0] == '#')
                cleanHexValue = cleanHexValue.Substring(1);

            // Expand out a shorthand hex code (e.g. #abc -> #aabbcc)
            if (cleanHexValue.Length == 3)
                ExpandShortenedHexCode(ref cleanHexValue);

            // If it's a short hex code (3 chars) we technically do this twice. Kinda weird but minimal allocation
            // Also no need to worry about Convert.ToByte throwing as the Regex match covers invalid chars
            model = new HexColorModel
            {
                R = Convert.ToByte(cleanHexValue.Substring(0, 2), 16),
                G = Convert.ToByte(cleanHexValue.Substring(2, 2), 16),
                B = Convert.ToByte(cleanHexValue.Substring(4, 2), 16)
            };

            return true;
        }

        private static void ExpandShortenedHexCode(ref string shortenedHex)
        {
            shortenedHex = string.Concat(
                new string(shortenedHex[0], 2),
                new string(shortenedHex[1], 2),
                new string(shortenedHex[2], 2));
        }
    }
}