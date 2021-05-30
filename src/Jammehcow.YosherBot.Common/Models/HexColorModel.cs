using System;

namespace Jammehcow.YosherBot.Common.Models
{
    public struct HexColorModel
    {
        public byte R;
        public byte G;
        public byte B;

        public string HexCode => $"#{Convert.ToHexString(new[] {R, G, B})}";
    }
}
