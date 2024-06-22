using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace TE1
{
    public static class InsItems
    {
        public static InsItem H01  = new InsItem(1, InsType.H,   -7.75, +103.00, 6.5);
        public static InsItem H02  = new InsItem(1, InsType.H,  -15.10, +116.00, 6.5);
        public static InsItem H03  = new InsItem(2, InsType.H,   -7.75, -103.00, 6.5);
        public static InsItem H04  = new InsItem(2, InsType.H,  -15.10, -116.00, 6.5);
        public static InsItem H05  = new InsItem(2, InsType.H, -495.00, -179.50, 6.5);
        public static InsItem H06  = new InsItem(2, InsType.H, -480.10, -137.00, 6.5);
        public static InsItem H07  = new InsItem(2, InsType.H, -509.90, -137.00, 6.5);
        public static InsItem H08  = new InsItem(2, InsType.H, -495.00, -115.60, 6.5);
        public static InsItem H09  = new InsItem(2, InsType.H, -495.00,  -71.60, 6.5);
        public static InsItem H10  = new InsItem(2, InsType.H, -495.00,  -17.00, 6.5);
        public static InsItem H11  = new InsItem(2, InsType.H, -495.00,  +17.00, 6.5);
        public static InsItem H12  = new InsItem(1, InsType.H, -495.00,  +71.60, 6.5);
        public static InsItem H13  = new InsItem(1, InsType.H, -495.00, +115.60, 6.5);
        public static InsItem H14  = new InsItem(1, InsType.H, -480.10, +137.00, 6.5);
        public static InsItem H15  = new InsItem(1, InsType.H, -509.90, +137.00, 6.5);
        public static InsItem H16  = new InsItem(1, InsType.H, -495.00, +179.50, 6.5);
        public static InsItem H17  = new InsItem(2, InsType.H, -974.90, -116.00, 6.5);
        public static InsItem H18  = new InsItem(2, InsType.H, -982.25, -103.00, 6.5);
        public static InsItem H19  = new InsItem(1, InsType.H, -982.25, +103.00, 6.5);
        public static InsItem H20  = new InsItem(1, InsType.H, -974.90, +116.00, 6.5);
        public static InsItem H21  = new InsItem(1, InsType.H,  -79.70, +221.81, 9.0);
        public static InsItem H22  = new InsItem(1, InsType.H, -263.50, +221.81, 9.0);
        public static InsItem H23  = new InsItem(1, InsType.H, -343.50, +221.81, 9.0);
        public static InsItem H24  = new InsItem(1, InsType.H, -423.50, +221.81, 9.0);
        public static InsItem H25  = new InsItem(1, InsType.H, -586.50, +221.81, 9.0);
        public static InsItem H26  = new InsItem(1, InsType.H, -666.50, +221.81, 9.0);
        public static InsItem H27  = new InsItem(1, InsType.H, -746.50, +221.81, 9.0);
        public static InsItem H28  = new InsItem(1, InsType.H, -930.30, +221.81, 9.0);
        public static InsItem H37  = new InsItem(2, InsType.H, -982.25,  -63.00, 7.5);
        public static InsItem H38  = new InsItem(2, InsType.H,   -8.75,  -63.00, 7.5);
        public static InsItem R01  = new InsItem(1, InsType.R,  - 7.75,  +63.00, 9.5);
        public static InsItem T101 = new InsItem(2, InsType.Y, -933.30, -202.58);
        public static InsItem T103 = new InsItem(2, InsType.Y, -849.73, -202.58);
        public static InsItem T105 = new InsItem(2, InsType.Y, -749.78, -202.58);
        public static InsItem T107 = new InsItem(2, InsType.Y, -650.24, -202.58);
        public static InsItem T109 = new InsItem(2, InsType.Y, -550.01, -202.58);
        public static InsItem T111 = new InsItem(2, InsType.Y, -469.57, -202.58);
        public static InsItem T113 = new InsItem(2, InsType.Y, -350.02, -202.58);
        public static InsItem T115 = new InsItem(2, InsType.Y, -249.93, -202.58);
        public static InsItem T117 = new InsItem(2, InsType.Y, -149.98, -202.58);
        public static InsItem T119 = new InsItem(2, InsType.Y,  -56.65, -202.58);
        public static InsItem T201 = new InsItem(3, InsType.Y,  -57.71, +230.58);
        public static InsItem T203 = new InsItem(3, InsType.Y, -114.09, +230.58);
        public static InsItem T205 = new InsItem(3, InsType.Y, -224.35, +230.58);
        public static InsItem T207 = new InsItem(3, InsType.Y, -343.50, +230.58);
        public static InsItem T209 = new InsItem(3, InsType.Y, -430.06, +230.58);
        public static InsItem T210 = new InsItem(3, InsType.X, -446.50, +219.54);
        public static InsItem T211 = new InsItem(3, InsType.Y, -466.38, +205.58);
        public static InsItem T212 = new InsItem(3, InsType.Y, -533.90, +205.58);
        public static InsItem T213 = new InsItem(3, InsType.X, -553.50, +218.26);
        public static InsItem T214 = new InsItem(3, InsType.Y, -560.18, +230.58);
        public static InsItem T216 = new InsItem(3, InsType.Y, -625.03, +230.58);
        public static InsItem T218 = new InsItem(3, InsType.Y, -736.17, +230.58);
        public static InsItem T220 = new InsItem(3, InsType.Y, -838.91, +230.58);
        public static InsItem T222 = new InsItem(3, InsType.Y, -929.97, +230.58);
        public static InsItem T301 = new InsItem(3, InsType.X,  -47.90, +193.32);
        public static InsItem T302 = new InsItem(3, InsType.X,  -47.90, +151.44);
        public static InsItem T303 = new InsItem(3, InsType.Y,  -34.00, +130.18);
        public static InsItem T304 = new InsItem(3, InsType.Y,  -10.74, +130.18);
        public static InsItem T401 = new InsItem(3, InsType.Y,  -10.36, -130.18);
        public static InsItem T402 = new InsItem(3, InsType.Y,  -36.28, -130.18);
        public static InsItem T403 = new InsItem(3, InsType.X,  -47.90, -143.79);
        public static InsItem T404 = new InsItem(3, InsType.X,  -47.90, -189.84);
        public static InsItem T501 = new InsItem(3, InsType.X, -942.10, +221.95);
        public static InsItem T503 = new InsItem(3, InsType.X, -942.10, +144.86);
        public static InsItem T504 = new InsItem(3, InsType.Y, -952.99, +130.18);
        public static InsItem T505 = new InsItem(3, InsType.Y, -978.25, +130.18);
        public static InsItem T601 = new InsItem(3, InsType.Y, -978.51, -130.18);
        public static InsItem T602 = new InsItem(3, InsType.Y, -954.56, -130.18);
        public static InsItem T603 = new InsItem(3, InsType.X, -942.10, -143.50);
        public static InsItem T604 = new InsItem(3, InsType.X, -942.10, -193.46);
        public static InsItem T701 = new InsItem(3, InsType.X,    0.00, -120.84);
        public static InsItem T702 = new InsItem(3, InsType.X,    0.00,  -82.07);
        public static InsItem T703 = new InsItem(3, InsType.X,   -2.50,  -54.47);
        public static InsItem T705 = new InsItem(3, InsType.X,   -2.50,  +19.08);
        public static InsItem T706 = new InsItem(3, InsType.X,    0.00,  +66.39);
        public static InsItem T707 = new InsItem(3, InsType.X,    0.00, +113.02);
        public static InsItem T801 = new InsItem(3, InsType.X, -990.00, -121.58);
        public static InsItem T803 = new InsItem(3, InsType.X, -990.00,  -55.44);
        public static InsItem T804 = new InsItem(3, InsType.X, -987.50,  -18.13);
        public static InsItem T806 = new InsItem(3, InsType.X, -987.50,  +18.03);
        public static InsItem T807 = new InsItem(3, InsType.X, -990.00,  +82.86);
        public static InsItem T808 = new InsItem(3, InsType.X, -990.00, +121.40);
        public static InsItem F01  = new InsItem(0, InsType.F,  -95.50, -150.00);
        public static InsItem F02  = new InsItem(0, InsType.F,  -22.50,   +0.01);
        public static InsItem F03  = new InsItem(0, InsType.F,  -95.50, +150.00);
        public static InsItem F04  = new InsItem(0, InsType.F, -415.50,    0.00);
        public static InsItem F05  = new InsItem(0, InsType.F, -574.50,    0.00);
        public static InsItem F06  = new InsItem(0, InsType.F, -894.50, -150.00);
        public static InsItem F07  = new InsItem(0, InsType.F, -974.90,    0.00);
        public static InsItem F08  = new InsItem(0, InsType.F, -894.50, +150.00);

        public static InsItem GetItem(String name)
        {
            if (name.StartsWith("H") || name.StartsWith("R")) name = name.Substring(0, 3);
            FieldInfo field = typeof(InsItems).GetField(name, BindingFlags.Static | BindingFlags.Public);
            if (field == null) return new InsItem();
            return field.GetValue(null) as InsItem;
        }

        public static Dictionary<String, InsItem> GetItems(Int32 camera)
        {
            Dictionary<String, InsItem> items = new Dictionary<String, InsItem>();
            foreach (FieldInfo field in typeof(InsItems).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                InsItem p = field.GetValue(null) as InsItem;
                if (p == null || p.Camera != camera || Double.IsNaN(p.X) || Double.IsNaN(p.Y)) continue;
                items.Add(field.Name, p);
            }
            return items;
        }
    }

    public enum InsType
    {
        [Description("Hole")]      H,
        [Description("Rectangle")] R,
        [Description("TrimX")]     X,
        [Description("TrimY")]     Y,
        [Description("Flatness")]  F,
        [Description("Sheet")]     S,
    }

    public class InsItem
    {
        public Int32 Camera = 0;
        public InsType InsType = InsType.H;
        public Double X = Double.NaN;
        public Double Y = Double.NaN;
        public Double D = 0;
        public Double R => D / 2;

        public InsItem() {}
        public InsItem(Int32 cam, InsType ins) { Camera = cam; InsType = ins; }
        public InsItem(Int32 cam, InsType ins, Double x, Double y) : this(cam, ins, x, y, 0) {}
        public InsItem(Int32 cam, InsType ins, Double x, Double y, Double d) { Camera = cam; InsType = ins; X = x; Y = y; D = d; }
    }

    public class Result
    {
        public String K = String.Empty;
        public Double V = Double.NaN;

        public Result() { }
        public Result(String k) { K = k; }
        public Result(String k, Double v) { K = k; V = v; }
    }
}
