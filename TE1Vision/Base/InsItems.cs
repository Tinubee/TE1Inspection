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
        public static InsItem H01 = new InsItem(2, InsType.H, -7.75, +103.00, 6.5);
        public static InsItem H02 = new InsItem(2, InsType.H, -15.10, +116.00, 6.5);
        public static InsItem H03 = new InsItem(2, InsType.H, -7.75, -103.00, 6.5);
        public static InsItem H04 = new InsItem(2, InsType.H, -15.10, -116.00, 6.5);
        public static InsItem H05 = new InsItem(2, InsType.H, -495.00, -179.50, 6.5);
        public static InsItem H06 = new InsItem(2, InsType.H, -480.10, -137.00, 6.5);
        public static InsItem H07 = new InsItem(2, InsType.H, -509.90, -137.00, 6.5);
        public static InsItem H08 = new InsItem(2, InsType.H, -495.00, -115.60, 6.5);
        public static InsItem H09 = new InsItem(2, InsType.H, -495.00, -71.60, 6.5);
        public static InsItem H10 = new InsItem(2, InsType.H, -495.00, -17.00, 6.5);
        public static InsItem H11 = new InsItem(2, InsType.H, -495.00, +17.00, 6.5);
        public static InsItem H12 = new InsItem(2, InsType.H, -495.00, +71.60, 6.5);
        public static InsItem H13 = new InsItem(2, InsType.H, -495.00, +115.60, 6.5);
        public static InsItem H14 = new InsItem(2, InsType.H, -480.10, +137.00, 6.5);
        public static InsItem H15 = new InsItem(2, InsType.H, -509.90, +137.00, 6.5);
        public static InsItem H16 = new InsItem(2, InsType.H, -495.00, +179.50, 6.5);
        public static InsItem H17 = new InsItem(2, InsType.H, -974.90, -116.00, 6.5);
        public static InsItem H18 = new InsItem(2, InsType.H, -982.25, -103.00, 6.5);
        public static InsItem H19 = new InsItem(2, InsType.H, -982.25, +103.00, 6.5);
        public static InsItem H20 = new InsItem(2, InsType.H, -974.90, +116.00, 6.5);
        public static InsItem H21 = new InsItem(2, InsType.H, -79.70, +221.81, 9.0);
        public static InsItem H22 = new InsItem(2, InsType.H, -263.50, +221.81, 9.0);
        public static InsItem H23 = new InsItem(2, InsType.H, -343.50, +221.81, 9.0);
        public static InsItem H24 = new InsItem(2, InsType.H, -423.50, +221.81, 9.0);
        public static InsItem H25 = new InsItem(2, InsType.H, -586.50, +221.81, 9.0);
        public static InsItem H26 = new InsItem(2, InsType.H, -666.50, +221.81, 9.0);
        public static InsItem H27 = new InsItem(2, InsType.H, -746.50, +221.81, 9.0);
        public static InsItem H28 = new InsItem(2, InsType.H, -930.30, +221.81, 9.0);
        public static InsItem H37 = new InsItem(2, InsType.H, -982.25, -63.00, 7.5); //Datum
        public static InsItem H38 = new InsItem(2, InsType.H, -8.75, -63.00, 7.5); //Datum
        public static InsItem R01 = new InsItem(2, InsType.R, -7.75, +63.00, 9.5);
       
        public static InsItem T001 = new InsItem(2, InsType.Y, -56.65, -202.58);
        public static InsItem T002 = new InsItem(2, InsType.Y, -149.98, -202.58);
        public static InsItem T003 = new InsItem(2, InsType.Y, -249.93, -202.58);
        public static InsItem T004 = new InsItem(2, InsType.Y, -350.02, -202.58);
        public static InsItem T005 = new InsItem(2, InsType.Y, -469.57, -202.58);
        public static InsItem T006 = new InsItem(2, InsType.Y, -550.01, -202.58);
        public static InsItem T007 = new InsItem(2, InsType.Y, -650.24, -202.58);
        public static InsItem T008 = new InsItem(2, InsType.Y, -749.78, -202.58);
        public static InsItem T009 = new InsItem(2, InsType.Y, -849.73, -202.58);
        public static InsItem T010 = new InsItem(2, InsType.Y, -933.30, -202.58);
        public static InsItem T011 = new InsItem(2, InsType.X, -942.10, -193.46);
        public static InsItem T012 = new InsItem(2, InsType.X, -942.10, -143.50);
        public static InsItem T013 = new InsItem(2, InsType.Y, -954.56, -130.18);
        public static InsItem T014 = new InsItem(2, InsType.Y, -978.51, -130.18);
        public static InsItem T015 = new InsItem(2, InsType.X, -990.00, -121.58);
        public static InsItem T016 = new InsItem(2, InsType.X, -990.00, -55.44);
        public static InsItem T017 = new InsItem(2, InsType.X, -987.50, -18.13);
        public static InsItem T018 = new InsItem(2, InsType.X, -987.50, +18.03);
        public static InsItem T019 = new InsItem(2, InsType.X, -990.00, +82.86);
        public static InsItem T020 = new InsItem(2, InsType.X, -990.00, +121.40);
        public static InsItem T021 = new InsItem(2, InsType.Y, -978.25, +130.18);
        public static InsItem T022 = new InsItem(2, InsType.Y, -952.99, +130.18);
        public static InsItem T023 = new InsItem(2, InsType.X, -942.10, +144.86);
        public static InsItem T024 = new InsItem(2, InsType.X, -942.10, +221.95);
        public static InsItem T025 = new InsItem(2, InsType.Y, -929.97, +230.58);
        public static InsItem T026 = new InsItem(2, InsType.Y, -838.91, +230.58);
        public static InsItem T027 = new InsItem(2, InsType.Y, -736.17, +230.58);
        public static InsItem T028 = new InsItem(2, InsType.Y, -625.03, +230.58);
        public static InsItem T029 = new InsItem(2, InsType.Y, -560.18, +230.58);
        public static InsItem T030 = new InsItem(2, InsType.X, -553.50, +218.26);
        public static InsItem T031 = new InsItem(2, InsType.Y, -533.90, +205.58);
        public static InsItem T032 = new InsItem(2, InsType.Y, -466.38, +205.58);
        public static InsItem T033 = new InsItem(2, InsType.X, -446.50, +219.54);
        public static InsItem T034 = new InsItem(2, InsType.Y, -430.06, +230.58);
        public static InsItem T035 = new InsItem(2, InsType.Y, -343.50, +230.58);
        public static InsItem T036 = new InsItem(2, InsType.Y, -224.35, +230.58);
        public static InsItem T037 = new InsItem(2, InsType.Y, -114.09, +230.58);
        public static InsItem T038 = new InsItem(2, InsType.Y, -57.71, +230.58);
        public static InsItem T039 = new InsItem(3, InsType.X, -47.90, +193.32);
        public static InsItem T040 = new InsItem(2, InsType.X, -47.90, +151.44);
        public static InsItem T041 = new InsItem(2, InsType.Y, -34.00, +130.18);
        public static InsItem T042 = new InsItem(2, InsType.Y, -10.74, +130.18);
        public static InsItem T043 = new InsItem(2, InsType.X, 0.00, +113.02);
        public static InsItem T044 = new InsItem(2, InsType.X, 0.00, +66.39);
        public static InsItem T045 = new InsItem(2, InsType.X, -2.50, +19.08);
        public static InsItem T046 = new InsItem(2, InsType.X, -2.50, -54.47);
        public static InsItem T047 = new InsItem(2, InsType.X, 0.00, -82.07);
        public static InsItem T048 = new InsItem(2, InsType.X, 0.00, -120.84);
        public static InsItem T049 = new InsItem(2, InsType.Y, -10.36, -130.18);
        public static InsItem T050 = new InsItem(2, InsType.Y, -36.28, -130.18);
        public static InsItem T051 = new InsItem(2, InsType.X, -47.90, -143.79);
        public static InsItem T052 = new InsItem(2, InsType.X, -47.90, -189.84);

        public static InsItem F01 = new InsItem(0, InsType.F, -95.50, -150.00);
        public static InsItem F02 = new InsItem(0, InsType.F, -22.50, +0.01);
        public static InsItem F03 = new InsItem(0, InsType.F, -95.50, +150.00);
        public static InsItem F04 = new InsItem(0, InsType.F, -415.50, 0.00);
        public static InsItem F05 = new InsItem(0, InsType.F, -574.50, 0.00);
        public static InsItem F06 = new InsItem(0, InsType.F, -894.50, -150.00);
        public static InsItem F07 = new InsItem(0, InsType.F, -974.90, 0.00);
        public static InsItem F08 = new InsItem(0, InsType.F, -894.50, +150.00);

        public static InsItem BT = new InsItem(3, InsType.B, -12.83, 83.00, 3.7);
        public static InsItem BB = new InsItem(3, InsType.B, -977.17, -83.00, 3.7);

        public static InsItem M01X1 = new InsItem(3, InsType.S, -102.50, -181.90);
        public static InsItem M01Y2 = new InsItem(3, InsType.S, -152.20, -194.40);
        public static InsItem M01X3 = new InsItem(3, InsType.S, -201.80, -181.90);
        public static InsItem M01Y4 = new InsItem(3, InsType.S, -152.20, -169.40);
        public static InsItem M02X1 = new InsItem(3, InsType.S, -203.20, -181.90);
        public static InsItem M02Y2 = new InsItem(3, InsType.S, -250.00, -194.40);
        public static InsItem M02X3 = new InsItem(3, InsType.S, -296.80, -181.90);
        public static InsItem M02Y4 = new InsItem(3, InsType.S, -250.00, -169.40);
        public static InsItem M03X1 = new InsItem(3, InsType.S, -298.20, -181.90);
        public static InsItem M03Y2 = new InsItem(3, InsType.S, -347.90, -194.40);
        public static InsItem M03X3 = new InsItem(3, InsType.S, -397.50, -181.90);
        public static InsItem M03Y4 = new InsItem(3, InsType.S, -347.90, -169.40);
        public static InsItem M04X1 = new InsItem(3, InsType.S, -102.50, -148.90);
        public static InsItem M04Y2 = new InsItem(3, InsType.S, -175.90, -161.40);
        public static InsItem M04X3 = new InsItem(3, InsType.S, -249.30, -148.90);
        public static InsItem M04Y4 = new InsItem(3, InsType.S, -175.90, -136.40);
        public static InsItem M05X1 = new InsItem(3, InsType.S, -250.70, -148.90);
        public static InsItem M05Y2 = new InsItem(3, InsType.S, -324.10, -161.40);
        public static InsItem M05X3 = new InsItem(3, InsType.S, -397.50, -148.90);
        public static InsItem M05Y4 = new InsItem(3, InsType.S, -324.10, -136.40);
        public static InsItem M06X1 = new InsItem(3, InsType.S, -102.50, -115.80);
        public static InsItem M06Y2 = new InsItem(3, InsType.S, -152.20, -128.30);
        public static InsItem M06X3 = new InsItem(3, InsType.S, -201.80, -115.80);
        public static InsItem M06Y4 = new InsItem(3, InsType.S, -152.20, -103.30);
        public static InsItem M07X1 = new InsItem(3, InsType.S, -203.20, -115.80);
        public static InsItem M07Y2 = new InsItem(3, InsType.S, -250.00, -128.30);
        public static InsItem M07X3 = new InsItem(3, InsType.S, -296.80, -115.80);
        public static InsItem M07Y4 = new InsItem(3, InsType.S, -250.00, -103.30);
        public static InsItem M08X1 = new InsItem(3, InsType.S, -298.20, -115.80);
        public static InsItem M08Y2 = new InsItem(3, InsType.S, -347.90, -128.30);
        public static InsItem M08X3 = new InsItem(3, InsType.S, -397.50, -115.80);
        public static InsItem M08Y4 = new InsItem(3, InsType.S, -347.90, -103.30);
        public static InsItem M09X1 = new InsItem(3, InsType.S, -102.50, -82.70);
        public static InsItem M09Y2 = new InsItem(3, InsType.S, -175.90, -95.20);
        public static InsItem M09X3 = new InsItem(3, InsType.S, -249.30, -82.70);
        public static InsItem M09Y4 = new InsItem(3, InsType.S, -175.90, -70.20);
        public static InsItem M10X1 = new InsItem(3, InsType.S, -250.70, -82.70);
        public static InsItem M10Y2 = new InsItem(3, InsType.S, -324.10, -95.20);
        public static InsItem M10X3 = new InsItem(3, InsType.S, -397.50, -82.70);
        public static InsItem M10Y4 = new InsItem(3, InsType.S, -324.10, -70.20);
        public static InsItem M11X1 = new InsItem(3, InsType.S, -102.50, -49.60);
        public static InsItem M11Y2 = new InsItem(3, InsType.S, -152.20, -62.10);
        public static InsItem M11X3 = new InsItem(3, InsType.S, -201.80, -49.60);
        public static InsItem M11Y4 = new InsItem(3, InsType.S, -152.20, -37.10);
        public static InsItem M12X1 = new InsItem(3, InsType.S, -203.20, -49.60);
        public static InsItem M12Y2 = new InsItem(3, InsType.S, -250.00, -62.10);
        public static InsItem M12X3 = new InsItem(3, InsType.S, -296.80, -49.60);
        public static InsItem M12Y4 = new InsItem(3, InsType.S, -250.00, -37.10);
        public static InsItem M13X1 = new InsItem(3, InsType.S, -298.20, -49.60);
        public static InsItem M13Y2 = new InsItem(3, InsType.S, -347.90, -62.10);
        public static InsItem M13X3 = new InsItem(3, InsType.S, -397.50, -49.60);
        public static InsItem M13Y4 = new InsItem(3, InsType.S, -347.90, -37.10);
        public static InsItem M14X1 = new InsItem(3, InsType.S, -102.50, -16.50);
        public static InsItem M14Y2 = new InsItem(3, InsType.S, -175.90, -29.00);
        public static InsItem M14X3 = new InsItem(3, InsType.S, -249.30, -16.50);
        public static InsItem M14Y4 = new InsItem(3, InsType.S, -175.90, -4.00);
        public static InsItem M15X1 = new InsItem(3, InsType.S, -250.70, -16.50);
        public static InsItem M15Y2 = new InsItem(3, InsType.S, -324.10, -29.00);
        public static InsItem M15X3 = new InsItem(3, InsType.S, -397.50, -16.50);
        public static InsItem M15Y4 = new InsItem(3, InsType.S, -324.10, -4.00);

        public static InsItem M16X1 = new InsItem(3, InsType.S, -102.50, 16.50);
        public static InsItem M16Y2 = new InsItem(3, InsType.S, -152.20, 4.00);
        public static InsItem M16X3 = new InsItem(3, InsType.S, -201.80, 16.50);
        public static InsItem M16Y4 = new InsItem(3, InsType.S, -152.20, 29.00);
        public static InsItem M17X1 = new InsItem(3, InsType.S, -203.20, 16.50);
        public static InsItem M17Y2 = new InsItem(3, InsType.S, -250.00, 4.00);
        public static InsItem M17X3 = new InsItem(3, InsType.S, -296.80, 16.50);
        public static InsItem M17Y4 = new InsItem(3, InsType.S, -250.00, 29.00);
        public static InsItem M18X1 = new InsItem(3, InsType.S, -298.20, 16.50);
        public static InsItem M18Y2 = new InsItem(3, InsType.S, -347.90, 4.00);
        public static InsItem M18X3 = new InsItem(3, InsType.S, -397.50, 16.50);
        public static InsItem M18Y4 = new InsItem(3, InsType.S, -347.90, 29.00);
        public static InsItem M19X1 = new InsItem(3, InsType.S, -102.50, 49.60);
        public static InsItem M19Y2 = new InsItem(3, InsType.S, -175.90, 37.10);
        public static InsItem M19X3 = new InsItem(3, InsType.S, -249.30, 49.60);
        public static InsItem M19Y4 = new InsItem(3, InsType.S, -175.90, 62.10);
        public static InsItem M20X1 = new InsItem(3, InsType.S, -250.70, 49.60);
        public static InsItem M20Y2 = new InsItem(3, InsType.S, -324.10, 37.10);
        public static InsItem M20X3 = new InsItem(3, InsType.S, -397.50, 49.60);
        public static InsItem M20Y4 = new InsItem(3, InsType.S, -324.50, 62.10);
        public static InsItem M21X1 = new InsItem(3, InsType.S, -102.50, 82.70);
        public static InsItem M21Y2 = new InsItem(3, InsType.S, -152.20, 70.20);
        public static InsItem M21X3 = new InsItem(3, InsType.S, -201.80, 82.70);
        public static InsItem M21Y4 = new InsItem(3, InsType.S, -152.20, 95.20);
        public static InsItem M22X1 = new InsItem(3, InsType.S, -203.20, 82.70);
        public static InsItem M22Y2 = new InsItem(3, InsType.S, -250.00, 70.20);
        public static InsItem M22X3 = new InsItem(3, InsType.S, -296.80, 82.70);
        public static InsItem M22Y4 = new InsItem(3, InsType.S, -250.00, 95.20);
        public static InsItem M23X1 = new InsItem(3, InsType.S, -298.20, 82.70);
        public static InsItem M23Y2 = new InsItem(3, InsType.S, -347.90, 70.20);
        public static InsItem M23X3 = new InsItem(3, InsType.S, -397.50, 82.70);
        public static InsItem M23Y4 = new InsItem(3, InsType.S, -347.90, 95.20);
        public static InsItem M24X1 = new InsItem(3, InsType.S, -102.50, 115.80);
        public static InsItem M24Y2 = new InsItem(3, InsType.S, -175.90, 103.30);
        public static InsItem M24X3 = new InsItem(3, InsType.S, -249.30, 115.80);
        public static InsItem M24Y4 = new InsItem(3, InsType.S, -175.90, 128.30);
        public static InsItem M25X1 = new InsItem(3, InsType.S, -250.70, 115.80);
        public static InsItem M25Y2 = new InsItem(3, InsType.S, -324.10, 103.30);
        public static InsItem M25X3 = new InsItem(3, InsType.S, -397.50, 115.80);
        public static InsItem M25Y4 = new InsItem(3, InsType.S, -324.10, 128.30);
        public static InsItem M26X1 = new InsItem(3, InsType.S, -102.50, 148.90);
        public static InsItem M26Y2 = new InsItem(3, InsType.S, -152.20, 136.40);
        public static InsItem M26X3 = new InsItem(3, InsType.S, -201.80, 148.90);
        public static InsItem M26Y4 = new InsItem(3, InsType.S, -152.20, 161.40);
        public static InsItem M27X1 = new InsItem(3, InsType.S, -203.20, 148.90);
        public static InsItem M27Y2 = new InsItem(3, InsType.S, -250.00, 136.40);
        public static InsItem M27X3 = new InsItem(3, InsType.S, -296.80, 148.90);
        public static InsItem M27Y4 = new InsItem(3, InsType.S, -250.00, 161.40);
        public static InsItem M28X1 = new InsItem(3, InsType.S, -298.20, 148.90);
        public static InsItem M28Y2 = new InsItem(3, InsType.S, -347.90, 136.40);
        public static InsItem M28X3 = new InsItem(3, InsType.S, -397.50, 148.90);
        public static InsItem M28Y4 = new InsItem(3, InsType.S, -347.90, 161.40);
        public static InsItem M29X1 = new InsItem(3, InsType.S, -102.50, 181.90);
        public static InsItem M29Y2 = new InsItem(3, InsType.S, -175.90, 169.40);
        public static InsItem M29X3 = new InsItem(3, InsType.S, -249.30, 181.90);
        public static InsItem M29Y4 = new InsItem(3, InsType.S, -175.90, 194.40);
        public static InsItem M30X1 = new InsItem(3, InsType.S, -250.70, 181.90);
        public static InsItem M30Y2 = new InsItem(3, InsType.S, -324.10, 169.40);
        public static InsItem M30X3 = new InsItem(3, InsType.S, -397.50, 181.90);
        public static InsItem M30Y4 = new InsItem(3, InsType.S, -324.10, 194.40);

        public static InsItem M31X1 = new InsItem(3, InsType.S, -592.50, -181.90);
        public static InsItem M31Y2 = new InsItem(3, InsType.S, -665.90, -194.40);
        public static InsItem M31X3 = new InsItem(3, InsType.S, -739.30, -181.90);
        public static InsItem M31Y4 = new InsItem(3, InsType.S, -665.90, -169.40);
        public static InsItem M32X1 = new InsItem(3, InsType.S, -740.70, -181.90);
        public static InsItem M32Y2 = new InsItem(3, InsType.S, -814.10, -194.40);
        public static InsItem M32X3 = new InsItem(3, InsType.S, -887.50, -181.90);
        public static InsItem M32Y4 = new InsItem(3, InsType.S, -814.10, -169.40);
        public static InsItem M33X1 = new InsItem(3, InsType.S, -592.50, -148.90);
        public static InsItem M33Y2 = new InsItem(3, InsType.S, -642.20, -161.40);
        public static InsItem M33X3 = new InsItem(3, InsType.S, -691.80, -148.90);
        public static InsItem M33Y4 = new InsItem(3, InsType.S, -642.20, -136.40);
        public static InsItem M34X1 = new InsItem(3, InsType.S, -693.20, -148.90);
        public static InsItem M34Y2 = new InsItem(3, InsType.S, -740.00, -161.40);
        public static InsItem M34X3 = new InsItem(3, InsType.S, -786.80, -148.90);
        public static InsItem M34Y4 = new InsItem(3, InsType.S, -740.00, -136.40);
        public static InsItem M35X1 = new InsItem(3, InsType.S, -788.20, -148.90);
        public static InsItem M35Y2 = new InsItem(3, InsType.S, -837.90, -161.40);
        public static InsItem M35X3 = new InsItem(3, InsType.S, -887.50, -148.90);
        public static InsItem M35Y4 = new InsItem(3, InsType.S, -837.50, -136.40);
        public static InsItem M36X1 = new InsItem(3, InsType.S, -592.50, -115.80);
        public static InsItem M36Y2 = new InsItem(3, InsType.S, -665.90, -128.30);
        public static InsItem M36X3 = new InsItem(3, InsType.S, -739.30, -115.80);
        public static InsItem M36Y4 = new InsItem(3, InsType.S, -665.90, -103.30);
        public static InsItem M37X1 = new InsItem(3, InsType.S, -740.70, -115.80);
        public static InsItem M37Y2 = new InsItem(3, InsType.S, -814.10, -128.30);
        public static InsItem M37X3 = new InsItem(3, InsType.S, -887.50, -115.80);
        public static InsItem M37Y4 = new InsItem(3, InsType.S, -814.10, -103.30);
        public static InsItem M38X1 = new InsItem(3, InsType.S, -592.50, -82.70);
        public static InsItem M38Y2 = new InsItem(3, InsType.S, -642.20, -95.20);
        public static InsItem M38X3 = new InsItem(3, InsType.S, -691.80, -82.70);
        public static InsItem M38Y4 = new InsItem(3, InsType.S, -642.20, -70.20);
        public static InsItem M39X1 = new InsItem(3, InsType.S, -693.20, -82.70);
        public static InsItem M39Y2 = new InsItem(3, InsType.S, -740.00, -95.20);
        public static InsItem M39X3 = new InsItem(3, InsType.S, -786.80, -82.70);
        public static InsItem M39Y4 = new InsItem(3, InsType.S, -740.00, -70.20);
        public static InsItem M40X1 = new InsItem(3, InsType.S, -788.20, -82.70);
        public static InsItem M40Y2 = new InsItem(3, InsType.S, -837.90, -95.20);
        public static InsItem M40X3 = new InsItem(3, InsType.S, -887.50, -82.70);
        public static InsItem M40Y4 = new InsItem(3, InsType.S, -837.50, -70.20);
        public static InsItem M41X1 = new InsItem(3, InsType.S, -592.50, -49.60);
        public static InsItem M41Y2 = new InsItem(3, InsType.S, -665.90, -62.10);
        public static InsItem M41X3 = new InsItem(3, InsType.S, -739.30, -49.60);
        public static InsItem M41Y4 = new InsItem(3, InsType.S, -665.90, -37.10);
        public static InsItem M42X1 = new InsItem(3, InsType.S, -740.70, -49.60);
        public static InsItem M42Y2 = new InsItem(3, InsType.S, -814.10, -62.10);
        public static InsItem M42X3 = new InsItem(3, InsType.S, -887.50, -49.60);
        public static InsItem M42Y4 = new InsItem(3, InsType.S, -814.10, -37.10);
        public static InsItem M43X1 = new InsItem(3, InsType.S, -592.50, -16.50);
        public static InsItem M43Y2 = new InsItem(3, InsType.S, -642.20, -29.00);
        public static InsItem M43X3 = new InsItem(3, InsType.S, -691.80, -16.50);
        public static InsItem M43Y4 = new InsItem(3, InsType.S, -642.20, -4.00);
        public static InsItem M44X1 = new InsItem(3, InsType.S, -693.20, -16.50);
        public static InsItem M44Y2 = new InsItem(3, InsType.S, -740.00, -29.00);
        public static InsItem M44X3 = new InsItem(3, InsType.S, -786.80, -16.50);
        public static InsItem M44Y4 = new InsItem(3, InsType.S, -740.00, -4.00);
        public static InsItem M45X1 = new InsItem(3, InsType.S, -788.20, -16.50);
        public static InsItem M45Y2 = new InsItem(3, InsType.S, -837.90, -29.00);
        public static InsItem M45X3 = new InsItem(3, InsType.S, -887.50, -16.50);
        public static InsItem M45Y4 = new InsItem(3, InsType.S, -837.50, -4.00);

        public static InsItem M46X1 = new InsItem(3, InsType.S, -592.50, 16.50);
        public static InsItem M46Y2 = new InsItem(3, InsType.S, -665.90, 4.00);
        public static InsItem M46X3 = new InsItem(3, InsType.S, -739.30, 16.50);
        public static InsItem M46Y4 = new InsItem(3, InsType.S, -665.90, 29.00);
        public static InsItem M47X1 = new InsItem(3, InsType.S, -740.70, 16.50);
        public static InsItem M47Y2 = new InsItem(3, InsType.S, -814.10, 4.00);
        public static InsItem M47X3 = new InsItem(3, InsType.S, -887.50, 16.50);
        public static InsItem M47Y4 = new InsItem(3, InsType.S, -814.10, 29.00);
        public static InsItem M48X1 = new InsItem(3, InsType.S, -592.50, 49.60);
        public static InsItem M48Y2 = new InsItem(3, InsType.S, -642.20, 37.10);
        public static InsItem M48X3 = new InsItem(3, InsType.S, -691.80, 49.60);
        public static InsItem M48Y4 = new InsItem(3, InsType.S, -642.20, 62.10);
        public static InsItem M49X1 = new InsItem(3, InsType.S, -693.20, 49.60);
        public static InsItem M49Y2 = new InsItem(3, InsType.S, -740.00, 37.10);
        public static InsItem M49X3 = new InsItem(3, InsType.S, -786.80, 49.60);
        public static InsItem M49Y4 = new InsItem(3, InsType.S, -740.00, 62.10);
        public static InsItem M50X1 = new InsItem(3, InsType.S, -788.20, 49.60);
        public static InsItem M50Y2 = new InsItem(3, InsType.S, -837.90, 37.10);
        public static InsItem M50X3 = new InsItem(3, InsType.S, -887.50, 49.60);
        public static InsItem M50Y4 = new InsItem(3, InsType.S, -837.50, 62.10);
        public static InsItem M51X1 = new InsItem(3, InsType.S, -592.50, 82.70);
        public static InsItem M51Y2 = new InsItem(3, InsType.S, -665.90, 70.20);
        public static InsItem M51X3 = new InsItem(3, InsType.S, -739.30, 82.70);
        public static InsItem M51Y4 = new InsItem(3, InsType.S, -665.90, 95.20);
        public static InsItem M52X1 = new InsItem(3, InsType.S, -740.70, 82.70);
        public static InsItem M52Y2 = new InsItem(3, InsType.S, -814.10, 70.20);
        public static InsItem M52X3 = new InsItem(3, InsType.S, -887.50, 82.70);
        public static InsItem M52Y4 = new InsItem(3, InsType.S, -814.10, 95.20);
        public static InsItem M53X1 = new InsItem(3, InsType.S, -592.50, 115.80);
        public static InsItem M53Y2 = new InsItem(3, InsType.S, -642.20, 103.30);
        public static InsItem M53X3 = new InsItem(3, InsType.S, -691.80, 115.80);
        public static InsItem M53Y4 = new InsItem(3, InsType.S, -642.20, 128.30);
        public static InsItem M54X1 = new InsItem(3, InsType.S, -693.20, 115.80);
        public static InsItem M54Y2 = new InsItem(3, InsType.S, -740.00, 103.30);
        public static InsItem M54X3 = new InsItem(3, InsType.S, -786.80, 115.80);
        public static InsItem M54Y4 = new InsItem(3, InsType.S, -740.00, 128.30);
        public static InsItem M55X1 = new InsItem(3, InsType.S, -788.20, 115.80);
        public static InsItem M55Y2 = new InsItem(3, InsType.S, -837.90, 103.30);
        public static InsItem M55X3 = new InsItem(3, InsType.S, -887.50, 115.80);
        public static InsItem M55Y4 = new InsItem(3, InsType.S, -837.50, 128.30);
        public static InsItem M56X1 = new InsItem(3, InsType.S, -592.50, 148.90);
        public static InsItem M56Y2 = new InsItem(3, InsType.S, -665.90, 136.40);
        public static InsItem M56X3 = new InsItem(3, InsType.S, -739.30, 148.90);
        public static InsItem M56Y4 = new InsItem(3, InsType.S, -665.90, 161.40);
        public static InsItem M57X1 = new InsItem(3, InsType.S, -740.70, 148.90);
        public static InsItem M57Y2 = new InsItem(3, InsType.S, -814.10, 136.40);
        public static InsItem M57X3 = new InsItem(3, InsType.S, -887.50, 148.90);
        public static InsItem M57Y4 = new InsItem(3, InsType.S, -814.10, 161.40);
        public static InsItem M58X1 = new InsItem(3, InsType.S, -592.50, 181.90);
        public static InsItem M58Y2 = new InsItem(3, InsType.S, -642.20, 169.40);
        public static InsItem M58X3 = new InsItem(3, InsType.S, -691.80, 181.90);
        public static InsItem M58Y4 = new InsItem(3, InsType.S, -642.20, 194.40);
        public static InsItem M59X1 = new InsItem(3, InsType.S, -693.20, 181.90);
        public static InsItem M59Y2 = new InsItem(3, InsType.S, -740.00, 169.40);
        public static InsItem M59X3 = new InsItem(3, InsType.S, -786.80, 181.90);
        public static InsItem M59Y4 = new InsItem(3, InsType.S, -740.00, 194.40);
        public static InsItem M60X1 = new InsItem(3, InsType.S, -788.20, 181.90);
        public static InsItem M60Y2 = new InsItem(3, InsType.S, -837.90, 169.40);
        public static InsItem M60X3 = new InsItem(3, InsType.S, -887.50, 181.90);
        public static InsItem M60Y4 = new InsItem(3, InsType.S, -837.50, 194.40);

        public static InsItem ImTopPlus = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImBottomPlus = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImTopMinus = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImBottomMinus = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImMiddle1 = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImMiddle2 = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImMiddle3 = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImSheetM1TR = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImSheetM1BR = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImSheetM1TL = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImSheetM1BL = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImSheetM2TR = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImSheetM2BR = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImSheetM2TL = new InsItem(3, InsType.I, 0, 0);
        public static InsItem ImSheetM2BL = new InsItem(3, InsType.I, 0, 0);

        public static InsItem GetItem(String name)
        {
            if (name.StartsWith("H") || name.StartsWith("R")) name = name.Substring(0, 3);
            if (name.StartsWith("B")) name = name.Substring(0, 2);
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
        [Description("Hole")] H,
        [Description("Rectangle")] R,
        [Description("TrimX")] X,
        [Description("TrimY")] Y,
        [Description("Flatness")] F,
        [Description("Sheet")] S,
        [Description("Imprint")] I,
        [Description("Bolt")] B,
    }

    public class InsItem
    {
        public Int32 Camera = 0;
        public InsType InsType = InsType.H;
        public Double X = Double.NaN;
        public Double Y = Double.NaN;
        public Double D = 0;                 // Diameter
        public Double H = Double.NaN;        // Height
        public Double R => D / 2;            // Radius
        public Double CalX = Double.NaN;
        public Double CalY = Double.NaN;
        public Double L = Double.NaN; //위치도
        public Double offsetX1 = Double.NaN;
        public Double offsetY2 = Double.NaN;
        public Double offsetX3 = Double.NaN;
        public Double offsetY4 = Double.NaN;
        public List<Double> DList = new List<Double>();
        public Double FontSize { get; set; } = 5;
        public InsItem() { }
        public InsItem(Int32 cam, InsType ins) { Camera = cam; InsType = ins; }
        public InsItem(Int32 cam, InsType ins, Double x, Double y) : this(cam, ins, x, y, 0) { }
        public InsItem(Int32 cam, InsType ins, Double x, Double y, Double d) { Camera = cam; InsType = ins; X = x; Y = y; D = d; }

        public InsItem(Int32 cam, InsType ins, Double x, Double y, Double d, Double h, Double offx1 = 0, Double offy2 = 0, Double offx3 = 0, Double offy4 = 0) { Camera = cam; InsType = ins; X = x; Y = y; D = d; H = h; offsetX1 = offx1; offsetY2 = offy2; offsetX3 = offx3; offsetY4 = offy4; }
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
