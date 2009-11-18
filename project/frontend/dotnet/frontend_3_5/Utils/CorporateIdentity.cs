using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace frontend_3_5.Utils
{
    public static class CorporateIdentity
    {
        public static Font fntTag5 = new Font("Arial", 16, FontStyle.Bold | FontStyle.Underline);
        public static Color color5 = Color.FromArgb(0xff, 0x99, 0x0);

        public static Font fntTag4 = new Font("Arial", 14, FontStyle.Bold | FontStyle.Underline);
        public static Color color4 = Color.FromArgb(0x41, 0x69, 0xe1);

        public static Font fntTag3 = new Font("Arial", 12, FontStyle.Bold | FontStyle.Underline);
        public static Color color3 = Color.FromArgb(0x00, 0x9e, 0xff);

        public static Font fntTag2 = new Font("Arial", 10, FontStyle.Bold | FontStyle.Underline);
        public static Color color2 = Color.FromArgb(0x41, 0x88, 0xcf);

        public static Font fntTag1 = new Font("Arial", 9, FontStyle.Bold | FontStyle.Underline);
        public static Color color1 = Color.FromArgb(0x83, 0xbc, 0xd8);

        public static Font fntTag0 = new Font("Arial", 7, FontStyle.Underline);
        public static Color color0 = Color.FromArgb(0x33, 0x33, 0x33);
    }
}
