using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SolidEdge.Spy.Extensions
{
    public static class IntExtensions
    {
        public static Color ToColor(this int i)
        {
            byte[] rgb = BitConverter.GetBytes(i);
            return Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
        }
    }
}
