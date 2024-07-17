﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace TE1.Schemas
{
    public enum 검사항목 : Int32
    {
        [Result(), ListBindable(false)] None = 0,

        // 1. Hole
        [Result(검사그룹.치수, 장치구분.Cam02, "H01X")] H01X = 1011,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Y")] H01Y = 1012,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01D")] H01D = 1013,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01L")] H01L = 1014,

        [Result(검사그룹.치수, 장치구분.Cam02, "H02X")] H02X = 1021,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Y")] H02Y = 1022,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02D")] H02D = 1023,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02L")] H02L = 1024,

        [Result(검사그룹.치수, 장치구분.Cam02, "H03X")] H03X = 1031,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Y")] H03Y = 1032,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03D")] H03D = 1033,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03L")] H03L = 1034,

        [Result(검사그룹.치수, 장치구분.Cam02, "H04X")] H04X = 1041,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Y")] H04Y = 1042,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04D")] H04D = 1043,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04L")] H04L = 1044,

        [Result(검사그룹.치수, 장치구분.Cam02, "H05X")] H05X = 1051,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Y")] H05Y = 1052,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05D")] H05D = 1053,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05L")] H05L = 1054,

        [Result(검사그룹.치수, 장치구분.Cam02, "H06X")] H06X = 1061,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Y")] H06Y = 1062,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06D")] H06D = 1063,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06L")] H06L = 1064,

        [Result(검사그룹.치수, 장치구분.Cam02, "H07X")] H07X = 1071,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Y")] H07Y = 1072,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07D")] H07D = 1073,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07L")] H07L = 1074,

        [Result(검사그룹.치수, 장치구분.Cam02, "H08X")] H08X = 1081,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Y")] H08Y = 1082,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08D")] H08D = 1083,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08L")] H08L = 1084,

        [Result(검사그룹.치수, 장치구분.Cam02, "H09X")] H09X = 1091,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Y")] H09Y = 1092,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09D")] H09D = 1093,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09L")] H09L = 1094,

        [Result(검사그룹.치수, 장치구분.Cam02, "H10X")] H10X = 1101,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Y")] H10Y = 1102,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10D")] H10D = 1103,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10L")] H10L = 1104,

        [Result(검사그룹.치수, 장치구분.Cam02, "H11X")] H11X = 1111,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Y")] H11Y = 1112,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11D")] H11D = 1113,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11L")] H11L = 1114,

        [Result(검사그룹.치수, 장치구분.Cam02, "H12X")] H12X = 1121,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Y")] H12Y = 1122,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12D")] H12D = 1123,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12L")] H12L = 1124,

        [Result(검사그룹.치수, 장치구분.Cam02, "H13X")] H13X = 1131,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Y")] H13Y = 1132,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13D")] H13D = 1133,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13L")] H13L = 1134,

        [Result(검사그룹.치수, 장치구분.Cam02, "H14X")] H14X = 1141,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Y")] H14Y = 1142,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14D")] H14D = 1143,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14L")] H14L = 1144,

        [Result(검사그룹.치수, 장치구분.Cam02, "H15X")] H15X = 1151,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Y")] H15Y = 1152,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15D")] H15D = 1153,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15L")] H15L = 1154,

        [Result(검사그룹.치수, 장치구분.Cam02, "H16X")] H16X = 1161,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Y")] H16Y = 1162,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16D")] H16D = 1163,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16L")] H16L = 1164,

        [Result(검사그룹.치수, 장치구분.Cam02, "H17X")] H17X = 1171,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Y")] H17Y = 1172,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17D")] H17D = 1173,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17L")] H17L = 1174,

        [Result(검사그룹.치수, 장치구분.Cam02, "H18X")] H18X = 1181,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Y")] H18Y = 1182,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18D")] H18D = 1183,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18L")] H18L = 1184,

        [Result(검사그룹.치수, 장치구분.Cam02, "H19X")] H19X = 1191,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Y")] H19Y = 1192,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19D")] H19D = 1193,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19L")] H19L = 1194,

        [Result(검사그룹.치수, 장치구분.Cam02, "H20X")] H20X = 1201,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Y")] H20Y = 1202,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20D")] H20D = 1203,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20L")] H20L = 1204,

        [Result(검사그룹.치수, 장치구분.Cam02, "H21X")] H21X = 1211,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Y")] H21Y = 1212,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21D")] H21D = 1213,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21L")] H21L = 1214,

        [Result(검사그룹.치수, 장치구분.Cam02, "H22X")] H22X = 1221,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Y")] H22Y = 1222,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22D")] H22D = 1223,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22L")] H22L = 1224,

        [Result(검사그룹.치수, 장치구분.Cam02, "H23X")] H23X = 1231,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Y")] H23Y = 1232,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23D")] H23D = 1233,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23L")] H23L = 1234,

        [Result(검사그룹.치수, 장치구분.Cam02, "H24X")] H24X = 1241,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Y")] H24Y = 1242,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24D")] H24D = 1243,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24L")] H24L = 1244,

        [Result(검사그룹.치수, 장치구분.Cam02, "H25X")] H25X = 1251,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Y")] H25Y = 1252,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25D")] H25D = 1253,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25L")] H25L = 1254,

        [Result(검사그룹.치수, 장치구분.Cam02, "H26X")] H26X = 1261,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Y")] H26Y = 1262,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26D")] H26D = 1263,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26L")] H26L = 1264,

        [Result(검사그룹.치수, 장치구분.Cam02, "H27X")] H27X = 1271,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Y")] H27Y = 1272,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27D")] H27D = 1273,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27L")] H27L = 1274,

        [Result(검사그룹.치수, 장치구분.Cam02, "H28X")] H28X = 1281,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Y")] H28Y = 1282,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28D")] H28D = 1283,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28L")] H28L = 1284,

        // B Datum
        [Result(검사그룹.치수, 장치구분.Cam02, "H37X")] H37X = 1371,
        [Result(검사그룹.치수, 장치구분.Cam02, "H37Y")] H37Y = 1372,
        [Result(검사그룹.치수, 장치구분.Cam02, "H37D")] H37D = 1373,
        //[Result(검사그룹.치수, 장치구분.Cam02, "H37L")] H37L = 1374,
        //[Result(검사그룹.치수, 장치구분.Cam02, "H37P")] H37P = 1375, //위치도 사실 상 0

        // C Datum
        [Result(검사그룹.치수, 장치구분.Cam02, "H38X")] H38X = 1381,
        [Result(검사그룹.치수, 장치구분.Cam02, "H38Y")] H38Y = 1382,
        [Result(검사그룹.치수, 장치구분.Cam02, "H38D")] H38D = 1383,
        //[Result(검사그룹.치수, 장치구분.Cam02, "H38L")] H38L = 1384,

        // Rectangle
        [Result(검사그룹.치수, 장치구분.Cam02, "R01X")] R01X = 1801,
        [Result(검사그룹.치수, 장치구분.Cam02, "R01Y")] R01Y = 1802,
        [Result(검사그룹.치수, 장치구분.Cam02, "R01W")] R01W = 1803,
        [Result(검사그룹.치수, 장치구분.Cam02, "R01L")] R01L = 1804,

        // 2. Trim
        [Result(검사그룹.치수, 장치구분.Cam02, "T101")] T101 = 2101,
        [Result(검사그룹.치수, 장치구분.Cam02, "T103")] T103 = 2103,
        [Result(검사그룹.치수, 장치구분.Cam02, "T105")] T105 = 2105,
        [Result(검사그룹.치수, 장치구분.Cam02, "T107")] T107 = 2107,
        [Result(검사그룹.치수, 장치구분.Cam02, "T109")] T109 = 2109,
        [Result(검사그룹.치수, 장치구분.Cam02, "T111")] T111 = 2111,
        [Result(검사그룹.치수, 장치구분.Cam02, "T113")] T113 = 2113,
        [Result(검사그룹.치수, 장치구분.Cam02, "T115")] T115 = 2115,
        [Result(검사그룹.치수, 장치구분.Cam02, "T117")] T117 = 2117,
        [Result(검사그룹.치수, 장치구분.Cam02, "T119")] T119 = 2119,

        [Result(검사그룹.치수, 장치구분.Cam02, "T201", -1)] T201 = 2201,
        [Result(검사그룹.치수, 장치구분.Cam02, "T203", -1)] T203 = 2203,
        [Result(검사그룹.치수, 장치구분.Cam02, "T205", -1)] T205 = 2205,
        [Result(검사그룹.치수, 장치구분.Cam02, "T207", -1)] T207 = 2207,
        [Result(검사그룹.치수, 장치구분.Cam02, "T209", -1)] T209 = 2209,
        [Result(검사그룹.치수, 장치구분.Cam02, "T210")] T210 = 2210,
        [Result(검사그룹.치수, 장치구분.Cam02, "T211", -1)] T211 = 2211,
        [Result(검사그룹.치수, 장치구분.Cam02, "T212", -1)] T212 = 2212,
        [Result(검사그룹.치수, 장치구분.Cam02, "T213")] T213 = 2213,
        [Result(검사그룹.치수, 장치구분.Cam02, "T214", -1)] T214 = 2214,
        [Result(검사그룹.치수, 장치구분.Cam02, "T216", -1)] T216 = 2216,
        [Result(검사그룹.치수, 장치구분.Cam02, "T218", -1)] T218 = 2218,
        [Result(검사그룹.치수, 장치구분.Cam02, "T220", -1)] T220 = 2220,
        [Result(검사그룹.치수, 장치구분.Cam02, "T222", -1)] T222 = 2222,

        [Result(검사그룹.치수, 장치구분.Cam03, "T301")] T301 = 2301,
        [Result(검사그룹.치수, 장치구분.Cam02, "T302")] T302 = 2302,
        [Result(검사그룹.치수, 장치구분.Cam02, "T303", -1)] T303 = 2303,
        [Result(검사그룹.치수, 장치구분.Cam02, "T304", -1)] T304 = 2304,

        [Result(검사그룹.치수, 장치구분.Cam02, "T401")] T401 = 2401,
        [Result(검사그룹.치수, 장치구분.Cam02, "T402")] T402 = 2402,
        [Result(검사그룹.치수, 장치구분.Cam02, "T403")] T403 = 2403,
        [Result(검사그룹.치수, 장치구분.Cam02, "T404")] T404 = 2404,

        [Result(검사그룹.치수, 장치구분.Cam02, "T501")] T501 = 2501,
        [Result(검사그룹.치수, 장치구분.Cam02, "T503")] T503 = 2503,
        [Result(검사그룹.치수, 장치구분.Cam02, "T504", -1)] T504 = 2504,
        [Result(검사그룹.치수, 장치구분.Cam02, "T505", -1)] T505 = 2505,

        [Result(검사그룹.치수, 장치구분.Cam02, "T601")] T601 = 2601,
        [Result(검사그룹.치수, 장치구분.Cam02, "T602")] T602 = 2602,
        [Result(검사그룹.치수, 장치구분.Cam02, "T603")] T603 = 2603,
        [Result(검사그룹.치수, 장치구분.Cam02, "T604")] T604 = 2604,

        [Result(검사그룹.치수, 장치구분.Cam03, "T701", -1)] T701 = 2701,
        [Result(검사그룹.치수, 장치구분.Cam02, "T702", -1)] T702 = 2702,
        [Result(검사그룹.치수, 장치구분.Cam02, "T703")] T703 = 2703,
        [Result(검사그룹.치수, 장치구분.Cam02, "T705")] T705 = 2705,
        [Result(검사그룹.치수, 장치구분.Cam02, "T706", -1)] T706 = 2706,
        [Result(검사그룹.치수, 장치구분.Cam02, "T707")] T707 = 2707,

        [Result(검사그룹.치수, 장치구분.Cam03, "T801")] T801 = 2801,
        [Result(검사그룹.치수, 장치구분.Cam02, "T803")] T803 = 2803,
        [Result(검사그룹.치수, 장치구분.Cam02, "T804")] T804 = 2804,
        [Result(검사그룹.치수, 장치구분.Cam02, "T806")] T806 = 2806,
        [Result(검사그룹.치수, 장치구분.Cam02, "T807")] T807 = 2807,
        [Result(검사그룹.치수, 장치구분.Cam03, "T808")] T808 = 2808,

        // 3. Bolt
        [Result(검사그룹.치수, 장치구분.Cam03, "BTX")] BTX = 2901,
        [Result(검사그룹.치수, 장치구분.Cam03, "BTY")] BTY = 2902,
        [Result(검사그룹.치수, 장치구분.Cam03, "BBX")] BBX = 2911,
        [Result(검사그룹.치수, 장치구분.Cam03, "BBY")] BBY = 2912,

        // 4. Mica
        [Result(검사그룹.치수, 장치구분.Cam03, "M01X1", -1)] M01X1 = 4011,
        [Result(검사그룹.치수, 장치구분.Cam03, "M01Y2")] M01Y2 = 4012,
        [Result(검사그룹.치수, 장치구분.Cam03, "M01X3")] M01X3 = 4013,
        [Result(검사그룹.치수, 장치구분.Cam03, "M01Y4", -1)] M01Y4 = 4014,

        [Result(검사그룹.치수, 장치구분.Cam03, "M02X1", -1)] M02X1 = 4021,
        [Result(검사그룹.치수, 장치구분.Cam03, "M02Y2")] M02Y2 = 4022,
        [Result(검사그룹.치수, 장치구분.Cam03, "M02X3")] M02X3 = 4023,
        [Result(검사그룹.치수, 장치구분.Cam03, "M02Y4", -1)] M02Y4 = 4024,

        [Result(검사그룹.치수, 장치구분.Cam03, "M03X1", -1)] M03X1 = 4031,
        [Result(검사그룹.치수, 장치구분.Cam03, "M03Y2")] M03Y2 = 4032,
        [Result(검사그룹.치수, 장치구분.Cam03, "M03X3")] M03X3 = 4033,
        [Result(검사그룹.치수, 장치구분.Cam03, "M03Y4", -1)] M03Y4 = 4034,

        [Result(검사그룹.치수, 장치구분.Cam03, "M04X1", -1)] M04X1 = 4041,
        [Result(검사그룹.치수, 장치구분.Cam03, "M04Y2")] M04Y2 = 4042,
        [Result(검사그룹.치수, 장치구분.Cam03, "M04X3")] M04X3 = 4043,
        [Result(검사그룹.치수, 장치구분.Cam03, "M04Y4", -1)] M04Y4 = 4044,

        [Result(검사그룹.치수, 장치구분.Cam03, "M05X1", -1)] M05X1 = 4051,
        [Result(검사그룹.치수, 장치구분.Cam03, "M05Y2")] M05Y2 = 4052,
        [Result(검사그룹.치수, 장치구분.Cam03, "M05X3")] M05X3 = 4053,
        [Result(검사그룹.치수, 장치구분.Cam03, "M05Y4", -1)] M05Y4 = 4054,

        [Result(검사그룹.치수, 장치구분.Cam03, "M06X1", -1)] M06X1 = 4061,
        [Result(검사그룹.치수, 장치구분.Cam03, "M06Y2")] M06Y2 = 4062,
        [Result(검사그룹.치수, 장치구분.Cam03, "M06X3")] M06X3 = 4063,
        [Result(검사그룹.치수, 장치구분.Cam03, "M06Y4", -1)] M06Y4 = 4064,

        [Result(검사그룹.치수, 장치구분.Cam03, "M07X1", -1)] M07X1 = 4071,
        [Result(검사그룹.치수, 장치구분.Cam03, "M07Y2")] M07Y2 = 4072,
        [Result(검사그룹.치수, 장치구분.Cam03, "M07X3")] M07X3 = 4073,
        [Result(검사그룹.치수, 장치구분.Cam03, "M07Y4", -1)] M07Y4 = 4074,

        [Result(검사그룹.치수, 장치구분.Cam03, "M08X1", -1)] M08X1 = 4081,
        [Result(검사그룹.치수, 장치구분.Cam03, "M08Y2")] M08Y2 = 4082,
        [Result(검사그룹.치수, 장치구분.Cam03, "M08X3")] M08X3 = 4083,
        [Result(검사그룹.치수, 장치구분.Cam03, "M08Y4", -1)] M08Y4 = 4084,

        [Result(검사그룹.치수, 장치구분.Cam03, "M09X1", -1)] M09X1 = 4091,
        [Result(검사그룹.치수, 장치구분.Cam03, "M09Y2")] M09Y2 = 4092,
        [Result(검사그룹.치수, 장치구분.Cam03, "M09X3")] M09X3 = 4093,
        [Result(검사그룹.치수, 장치구분.Cam03, "M09Y4", -1)] M09Y4 = 4094,

        [Result(검사그룹.치수, 장치구분.Cam03, "M10X1", -1)] M10X1 = 4101,
        [Result(검사그룹.치수, 장치구분.Cam03, "M10Y2")] M10Y2 = 4102,
        [Result(검사그룹.치수, 장치구분.Cam03, "M10X3")] M10X3 = 4103,
        [Result(검사그룹.치수, 장치구분.Cam03, "M10Y4", -1)] M10Y4 = 4104,

        [Result(검사그룹.치수, 장치구분.Cam03, "M11X1", -1)] M11X1 = 4111,
        [Result(검사그룹.치수, 장치구분.Cam03, "M11Y2")] M11Y2 = 4112,
        [Result(검사그룹.치수, 장치구분.Cam03, "M11X3")] M11X3 = 4113,
        [Result(검사그룹.치수, 장치구분.Cam03, "M11Y4", -1)] M11Y4 = 4114,

        [Result(검사그룹.치수, 장치구분.Cam03, "M12X1", -1)] M12X1 = 4121,
        [Result(검사그룹.치수, 장치구분.Cam03, "M12Y2")] M12Y2 = 4122,
        [Result(검사그룹.치수, 장치구분.Cam03, "M12X3")] M12X3 = 4123,
        [Result(검사그룹.치수, 장치구분.Cam03, "M12Y4", -1)] M12Y4 = 4124,

        [Result(검사그룹.치수, 장치구분.Cam03, "M13X1", -1)] M13X1 = 4131,
        [Result(검사그룹.치수, 장치구분.Cam03, "M13Y2")] M13Y2 = 4132,
        [Result(검사그룹.치수, 장치구분.Cam03, "M13X3")] M13X3 = 4133,
        [Result(검사그룹.치수, 장치구분.Cam03, "M13Y4", -1)] M13Y4 = 4134,

        [Result(검사그룹.치수, 장치구분.Cam03, "M14X1", -1)] M14X1 = 4141,
        [Result(검사그룹.치수, 장치구분.Cam03, "M14Y2")] M14Y2 = 4142,
        [Result(검사그룹.치수, 장치구분.Cam03, "M14X3")] M14X3 = 4143,
        [Result(검사그룹.치수, 장치구분.Cam03, "M14Y4", -1)] M14Y4 = 4144,

        [Result(검사그룹.치수, 장치구분.Cam03, "M15X1", -1)] M15X1 = 4151,
        [Result(검사그룹.치수, 장치구분.Cam03, "M15Y2")] M15Y2 = 4152,
        [Result(검사그룹.치수, 장치구분.Cam03, "M15X3")] M15X3 = 4153,
        [Result(검사그룹.치수, 장치구분.Cam03, "M15Y4", -1)] M15Y4 = 4154,

        [Result(검사그룹.치수, 장치구분.Cam03, "M16X1", -1)] M16X1 = 4161,
        [Result(검사그룹.치수, 장치구분.Cam03, "M16Y2", -1)] M16Y2 = 4162,
        [Result(검사그룹.치수, 장치구분.Cam03, "M16X3")] M16X3 = 4163,
        [Result(검사그룹.치수, 장치구분.Cam03, "M16Y4")] M16Y4 = 4164,

        [Result(검사그룹.치수, 장치구분.Cam03, "M17X1", -1)] M17X1 = 4171,
        [Result(검사그룹.치수, 장치구분.Cam03, "M17Y2", -1)] M17Y2 = 4172,
        [Result(검사그룹.치수, 장치구분.Cam03, "M17X3")] M17X3 = 4173,
        [Result(검사그룹.치수, 장치구분.Cam03, "M17Y4")] M17Y4 = 4174,

        [Result(검사그룹.치수, 장치구분.Cam03, "M18X1", -1)] M18X1 = 4181,
        [Result(검사그룹.치수, 장치구분.Cam03, "M18Y2", -1)] M18Y2 = 4182,
        [Result(검사그룹.치수, 장치구분.Cam03, "M18X3")] M18X3 = 4183,
        [Result(검사그룹.치수, 장치구분.Cam03, "M18Y4")] M18Y4 = 4184,

        [Result(검사그룹.치수, 장치구분.Cam03, "M19X1", -1)] M19X1 = 4191,
        [Result(검사그룹.치수, 장치구분.Cam03, "M19Y2", -1)] M19Y2 = 4192,
        [Result(검사그룹.치수, 장치구분.Cam03, "M19X3")] M19X3 = 4193,
        [Result(검사그룹.치수, 장치구분.Cam03, "M19Y4")] M19Y4 = 4194,

        [Result(검사그룹.치수, 장치구분.Cam03, "M20X1", -1)] M20X1 = 4201,
        [Result(검사그룹.치수, 장치구분.Cam03, "M20Y2", -1)] M20Y2 = 4202,
        [Result(검사그룹.치수, 장치구분.Cam03, "M20X3")] M20X3 = 4203,
        [Result(검사그룹.치수, 장치구분.Cam03, "M20Y4")] M20Y4 = 4204,

        [Result(검사그룹.치수, 장치구분.Cam03, "M21X1", -1)] M21X1 = 4211,
        [Result(검사그룹.치수, 장치구분.Cam03, "M21Y2", -1)] M21Y2 = 4212,
        [Result(검사그룹.치수, 장치구분.Cam03, "M21X3")] M21X3 = 4213,
        [Result(검사그룹.치수, 장치구분.Cam03, "M21Y4")] M21Y4 = 4214,

        [Result(검사그룹.치수, 장치구분.Cam03, "M22X1", -1)] M22X1 = 4221,
        [Result(검사그룹.치수, 장치구분.Cam03, "M22Y2", -1)] M22Y2 = 4222,
        [Result(검사그룹.치수, 장치구분.Cam03, "M22X3")] M22X3 = 4223,
        [Result(검사그룹.치수, 장치구분.Cam03, "M22Y4")] M22Y4 = 4224,

        [Result(검사그룹.치수, 장치구분.Cam03, "M23X1", -1)] M23X1 = 4231,
        [Result(검사그룹.치수, 장치구분.Cam03, "M23Y2", -1)] M23Y2 = 4232,
        [Result(검사그룹.치수, 장치구분.Cam03, "M23X3")] M23X3 = 4233,
        [Result(검사그룹.치수, 장치구분.Cam03, "M23Y4")] M23Y4 = 4234,

        [Result(검사그룹.치수, 장치구분.Cam03, "M24X1", -1)] M24X1 = 4241,
        [Result(검사그룹.치수, 장치구분.Cam03, "M24Y2", -1)] M24Y2 = 4242,
        [Result(검사그룹.치수, 장치구분.Cam03, "M24X3")] M24X3 = 4243,
        [Result(검사그룹.치수, 장치구분.Cam03, "M24Y4")] M24Y4 = 4244,

        [Result(검사그룹.치수, 장치구분.Cam03, "M25X1", -1)] M25X1 = 4251,
        [Result(검사그룹.치수, 장치구분.Cam03, "M25Y2", -1)] M25Y2 = 4252,
        [Result(검사그룹.치수, 장치구분.Cam03, "M25X3")] M25X3 = 4253,
        [Result(검사그룹.치수, 장치구분.Cam03, "M25Y4")] M25Y4 = 4254,

        [Result(검사그룹.치수, 장치구분.Cam03, "M26X1", -1)] M26X1 = 4261,
        [Result(검사그룹.치수, 장치구분.Cam03, "M26Y2", -1)] M26Y2 = 4262,
        [Result(검사그룹.치수, 장치구분.Cam03, "M26X3")] M26X3 = 4263,
        [Result(검사그룹.치수, 장치구분.Cam03, "M26Y4")] M26Y4 = 4264,

        [Result(검사그룹.치수, 장치구분.Cam03, "M27X1", -1)] M27X1 = 4271,
        [Result(검사그룹.치수, 장치구분.Cam03, "M27Y2", -1)] M27Y2 = 4272,
        [Result(검사그룹.치수, 장치구분.Cam03, "M27X3")] M27X3 = 4273,
        [Result(검사그룹.치수, 장치구분.Cam03, "M27Y4")] M27Y4 = 4274,

        [Result(검사그룹.치수, 장치구분.Cam03, "M28X1", -1)] M28X1 = 4281,
        [Result(검사그룹.치수, 장치구분.Cam03, "M28Y2", -1)] M28Y2 = 4282,
        [Result(검사그룹.치수, 장치구분.Cam03, "M28X3")] M28X3 = 4283,
        [Result(검사그룹.치수, 장치구분.Cam03, "M28Y4")] M28Y4 = 4284,

        [Result(검사그룹.치수, 장치구분.Cam03, "M29X1")] M29X1 = 4291,
        [Result(검사그룹.치수, 장치구분.Cam03, "M29Y2")] M29Y2 = 4292,
        [Result(검사그룹.치수, 장치구분.Cam03, "M29X3")] M29X3 = 4293,
        [Result(검사그룹.치수, 장치구분.Cam03, "M29Y4")] M29Y4 = 4294,

        [Result(검사그룹.치수, 장치구분.Cam03, "M30X1", -1)] M30X1 = 4301,
        [Result(검사그룹.치수, 장치구분.Cam03, "M30Y2", -1)] M30Y2 = 4302,
        [Result(검사그룹.치수, 장치구분.Cam03, "M30X3")] M30X3 = 4303,
        [Result(검사그룹.치수, 장치구분.Cam03, "M30Y4")] M30Y4 = 4304,

        [Result(검사그룹.치수, 장치구분.Cam03, "M31X1", -1)] M31X1 = 4311,
        [Result(검사그룹.치수, 장치구분.Cam03, "M31Y2")] M31Y2 = 4312,
        [Result(검사그룹.치수, 장치구분.Cam03, "M31X3")] M31X3 = 4313,
        [Result(검사그룹.치수, 장치구분.Cam03, "M31Y4", -1)] M31Y4 = 4314,

        [Result(검사그룹.치수, 장치구분.Cam03, "M32X1", -1)] M32X1 = 4321,
        [Result(검사그룹.치수, 장치구분.Cam03, "M32Y2")] M32Y2 = 4322,
        [Result(검사그룹.치수, 장치구분.Cam03, "M32X3")] M32X3 = 4323,
        [Result(검사그룹.치수, 장치구분.Cam03, "M32Y4", -1)] M32Y4 = 4324,

        [Result(검사그룹.치수, 장치구분.Cam03, "M33X1", -1)] M33X1 = 4331,
        [Result(검사그룹.치수, 장치구분.Cam03, "M33Y2")] M33Y2 = 4332,
        [Result(검사그룹.치수, 장치구분.Cam03, "M33X3")] M33X3 = 4333,
        [Result(검사그룹.치수, 장치구분.Cam03, "M33Y4", -1)] M33Y4 = 4334,

        [Result(검사그룹.치수, 장치구분.Cam03, "M34X1", -1)] M34X1 = 4341,
        [Result(검사그룹.치수, 장치구분.Cam03, "M34Y2")] M34Y2 = 4342,
        [Result(검사그룹.치수, 장치구분.Cam03, "M34X3")] M34X3 = 4343,
        [Result(검사그룹.치수, 장치구분.Cam03, "M34Y4", -1)] M34Y4 = 4344,

        [Result(검사그룹.치수, 장치구분.Cam03, "M35X1", -1)] M35X1 = 4351,
        [Result(검사그룹.치수, 장치구분.Cam03, "M35Y2")] M35Y2 = 4352,
        [Result(검사그룹.치수, 장치구분.Cam03, "M35X3")] M35X3 = 4353,
        [Result(검사그룹.치수, 장치구분.Cam03, "M35Y4", -1)] M35Y4 = 4354,

        [Result(검사그룹.치수, 장치구분.Cam03, "M36X1", -1)] M36X1 = 4361,
        [Result(검사그룹.치수, 장치구분.Cam03, "M36Y2")] M36Y2 = 4362,
        [Result(검사그룹.치수, 장치구분.Cam03, "M36X3")] M36X3 = 4363,
        [Result(검사그룹.치수, 장치구분.Cam03, "M36Y4", -1)] M36Y4 = 4364,

        [Result(검사그룹.치수, 장치구분.Cam03, "M37X1", -1)] M37X1 = 4371,
        [Result(검사그룹.치수, 장치구분.Cam03, "M37Y2")] M37Y2 = 4372,
        [Result(검사그룹.치수, 장치구분.Cam03, "M37X3")] M37X3 = 4373,
        [Result(검사그룹.치수, 장치구분.Cam03, "M37Y4", -1)] M37Y4 = 4374,

        [Result(검사그룹.치수, 장치구분.Cam03, "M38X1", -1)] M38X1 = 4381,
        [Result(검사그룹.치수, 장치구분.Cam03, "M38Y2")] M38Y2 = 4382,
        [Result(검사그룹.치수, 장치구분.Cam03, "M38X3")] M38X3 = 4383,
        [Result(검사그룹.치수, 장치구분.Cam03, "M38Y4", -1)] M38Y4 = 4384,

        [Result(검사그룹.치수, 장치구분.Cam03, "M39X1", -1)] M39X1 = 4391,
        [Result(검사그룹.치수, 장치구분.Cam03, "M39Y2")] M39Y2 = 4392,
        [Result(검사그룹.치수, 장치구분.Cam03, "M39X3")] M39X3 = 4393,
        [Result(검사그룹.치수, 장치구분.Cam03, "M39Y4", -1)] M39Y4 = 4394,

        [Result(검사그룹.치수, 장치구분.Cam03, "M40X1", -1)] M40X1 = 4401,
        [Result(검사그룹.치수, 장치구분.Cam03, "M40Y2")] M40Y2 = 4402,
        [Result(검사그룹.치수, 장치구분.Cam03, "M40X3")] M40X3 = 4403,
        [Result(검사그룹.치수, 장치구분.Cam03, "M40Y4", -1)] M40Y4 = 4404,

        [Result(검사그룹.치수, 장치구분.Cam03, "M41X1", -1)] M41X1 = 4411,
        [Result(검사그룹.치수, 장치구분.Cam03, "M41Y2")] M41Y2 = 4412,
        [Result(검사그룹.치수, 장치구분.Cam03, "M41X3")] M41X3 = 4413,
        [Result(검사그룹.치수, 장치구분.Cam03, "M41Y4", -1)] M41Y4 = 4414,

        [Result(검사그룹.치수, 장치구분.Cam03, "M42X1", -1)] M42X1 = 4421,
        [Result(검사그룹.치수, 장치구분.Cam03, "M42Y2")] M42Y2 = 4422,
        [Result(검사그룹.치수, 장치구분.Cam03, "M42X3")] M42X3 = 4423,
        [Result(검사그룹.치수, 장치구분.Cam03, "M42Y4", -1)] M42Y4 = 4424,

        [Result(검사그룹.치수, 장치구분.Cam03, "M43X1", -1)] M43X1 = 4431,
        [Result(검사그룹.치수, 장치구분.Cam03, "M43Y2")] M43Y2 = 4432,
        [Result(검사그룹.치수, 장치구분.Cam03, "M43X3")] M43X3 = 4433,
        [Result(검사그룹.치수, 장치구분.Cam03, "M43Y4", -1)] M43Y4 = 4434,

        [Result(검사그룹.치수, 장치구분.Cam03, "M44X1", -1)] M44X1 = 4441,
        [Result(검사그룹.치수, 장치구분.Cam03, "M44Y2")] M44Y2 = 4442,
        [Result(검사그룹.치수, 장치구분.Cam03, "M44X3")] M44X3 = 4443,
        [Result(검사그룹.치수, 장치구분.Cam03, "M44Y4", -1)] M44Y4 = 4444,

        [Result(검사그룹.치수, 장치구분.Cam03, "M45X1", -1)] M45X1 = 4451,
        [Result(검사그룹.치수, 장치구분.Cam03, "M45Y2")] M45Y2 = 4452,
        [Result(검사그룹.치수, 장치구분.Cam03, "M45X3")] M45X3 = 4453,
        [Result(검사그룹.치수, 장치구분.Cam03, "M45Y4", -1)] M45Y4 = 4454,

        [Result(검사그룹.치수, 장치구분.Cam03, "M46X1", -1)] M46X1 = 4461,
        [Result(검사그룹.치수, 장치구분.Cam03, "M46Y2", -1)] M46Y2 = 4462,
        [Result(검사그룹.치수, 장치구분.Cam03, "M46X3")] M46X3 = 4463,
        [Result(검사그룹.치수, 장치구분.Cam03, "M46Y4")] M46Y4 = 4464,

        [Result(검사그룹.치수, 장치구분.Cam03, "M47X1", -1)] M47X1 = 4471,
        [Result(검사그룹.치수, 장치구분.Cam03, "M47Y2", -1)] M47Y2 = 4472,
        [Result(검사그룹.치수, 장치구분.Cam03, "M47X3")] M47X3 = 4473,
        [Result(검사그룹.치수, 장치구분.Cam03, "M47Y4")] M47Y4 = 4474,

        [Result(검사그룹.치수, 장치구분.Cam03, "M48X1", -1)] M48X1 = 4481,
        [Result(검사그룹.치수, 장치구분.Cam03, "M48Y2", -1)] M48Y2 = 4482,
        [Result(검사그룹.치수, 장치구분.Cam03, "M48X3")] M48X3 = 4483,
        [Result(검사그룹.치수, 장치구분.Cam03, "M48Y4")] M48Y4 = 4484,

        [Result(검사그룹.치수, 장치구분.Cam03, "M49X1", -1)] M49X1 = 4491,
        [Result(검사그룹.치수, 장치구분.Cam03, "M49Y2", -1)] M49Y2 = 4492,
        [Result(검사그룹.치수, 장치구분.Cam03, "M49X3")] M49X3 = 4493,
        [Result(검사그룹.치수, 장치구분.Cam03, "M49Y4")] M49Y4 = 4494,

        [Result(검사그룹.치수, 장치구분.Cam03, "M50X1", -1)] M50X1 = 4501,
        [Result(검사그룹.치수, 장치구분.Cam03, "M50Y2", -1)] M50Y2 = 4502,
        [Result(검사그룹.치수, 장치구분.Cam03, "M50X3")] M50X3 = 4503,
        [Result(검사그룹.치수, 장치구분.Cam03, "M50Y4")] M50Y4 = 4504,

        [Result(검사그룹.치수, 장치구분.Cam03, "M51X1", -1)] M51X1 = 4511,
        [Result(검사그룹.치수, 장치구분.Cam03, "M51Y2", -1)] M51Y2 = 4512,
        [Result(검사그룹.치수, 장치구분.Cam03, "M51X3")] M51X3 = 4513,
        [Result(검사그룹.치수, 장치구분.Cam03, "M51Y4")] M51Y4 = 4514,

        [Result(검사그룹.치수, 장치구분.Cam03, "M52X1", -1)] M52X1 = 4521,
        [Result(검사그룹.치수, 장치구분.Cam03, "M52Y2", -1)] M52Y2 = 4522,
        [Result(검사그룹.치수, 장치구분.Cam03, "M52X3")] M52X3 = 4523,
        [Result(검사그룹.치수, 장치구분.Cam03, "M52Y4")] M52Y4 = 4524,

        [Result(검사그룹.치수, 장치구분.Cam03, "M53X1", -1)] M53X1 = 4531,
        [Result(검사그룹.치수, 장치구분.Cam03, "M53Y2", -1)] M53Y2 = 4532,
        [Result(검사그룹.치수, 장치구분.Cam03, "M53X3")] M53X3 = 4533,
        [Result(검사그룹.치수, 장치구분.Cam03, "M53Y4")] M53Y4 = 4534,

        [Result(검사그룹.치수, 장치구분.Cam03, "M54X1", -1)] M54X1 = 4541,
        [Result(검사그룹.치수, 장치구분.Cam03, "M54Y2", -1)] M54Y2 = 4542,
        [Result(검사그룹.치수, 장치구분.Cam03, "M54X3")] M54X3 = 4543,
        [Result(검사그룹.치수, 장치구분.Cam03, "M54Y4")] M54Y4 = 4544,

        [Result(검사그룹.치수, 장치구분.Cam03, "M55X1", -1)] M55X1 = 4551,
        [Result(검사그룹.치수, 장치구분.Cam03, "M55Y2", -1)] M55Y2 = 4552,
        [Result(검사그룹.치수, 장치구분.Cam03, "M55X3")] M55X3 = 4553,
        [Result(검사그룹.치수, 장치구분.Cam03, "M55Y4")] M55Y4 = 4554,

        [Result(검사그룹.치수, 장치구분.Cam03, "M56X1", -1)] M56X1 = 4561,
        [Result(검사그룹.치수, 장치구분.Cam03, "M56Y2", -1)] M56Y2 = 4562,
        [Result(검사그룹.치수, 장치구분.Cam03, "M56X3")] M56X3 = 4563,
        [Result(검사그룹.치수, 장치구분.Cam03, "M56Y4")] M56Y4 = 4564,

        [Result(검사그룹.치수, 장치구분.Cam03, "M57X1", -1)] M57X1 = 4571,
        [Result(검사그룹.치수, 장치구분.Cam03, "M57Y2", -1)] M57Y2 = 4572,
        [Result(검사그룹.치수, 장치구분.Cam03, "M57X3")] M57X3 = 4573,
        [Result(검사그룹.치수, 장치구분.Cam03, "M57Y4")] M57Y4 = 4574,

        [Result(검사그룹.치수, 장치구분.Cam03, "M58X1", -1)] M58X1 = 4581,
        [Result(검사그룹.치수, 장치구분.Cam03, "M58Y2", -1)] M58Y2 = 4582,
        [Result(검사그룹.치수, 장치구분.Cam03, "M58X3")] M58X3 = 4583,
        [Result(검사그룹.치수, 장치구분.Cam03, "M58Y4")] M58Y4 = 4584,

        [Result(검사그룹.치수, 장치구분.Cam03, "M59X1", -1)] M59X1 = 4591,
        [Result(검사그룹.치수, 장치구분.Cam03, "M59Y2", -1)] M59Y2 = 4592,
        [Result(검사그룹.치수, 장치구분.Cam03, "M59X3")] M59X3 = 4593,
        [Result(검사그룹.치수, 장치구분.Cam03, "M59Y4")] M59Y4 = 4594,

        [Result(검사그룹.치수, 장치구분.Cam03, "M60X1", -1)] M60X1 = 4601,
        [Result(검사그룹.치수, 장치구분.Cam03, "M60Y2", -1)] M60Y2 = 4602,
        [Result(검사그룹.치수, 장치구분.Cam03, "M60X3")] M60X3 = 4603,
        [Result(검사그룹.치수, 장치구분.Cam03, "M60Y4")] M60Y4 = 4604,

        // 5. Flatness
        [Result(검사그룹.치수, 장치구분.Flatness, "F01")] F01 = 5010,
        [Result(검사그룹.치수, 장치구분.Flatness, "F02")] F02 = 5020,
        [Result(검사그룹.치수, 장치구분.Flatness, "F03")] F03 = 5030,
        [Result(검사그룹.치수, 장치구분.Flatness, "F04")] F04 = 5040,
        [Result(검사그룹.치수, 장치구분.Flatness, "F05")] F05 = 5050,
        [Result(검사그룹.치수, 장치구분.Flatness, "F06")] F06 = 5060,
        [Result(검사그룹.치수, 장치구분.Flatness, "F07")] F07 = 5070,
        [Result(검사그룹.치수, 장치구분.Flatness, "F08")] F08 = 5080,

        // 6. Thickness
        [Result(검사그룹.치수, 장치구분.Thickness, "PThickness")] PThickness = 6010,

        // 7. 각인
        //[Result(검사그룹.치수, 장치구분.Cam03, "ImTopPlus")] ImTopPlus = 7001,
        //[Result(검사그룹.표면, 장치구분.Cam03, "ImBottomPlus")] ImBottomPlus = 7002,
        //[Result(검사그룹.표면, 장치구분.Cam03, "ImTopMinus")] ImTopMinus = 7003,
        //[Result(검사그룹.표면, 장치구분.Cam03, "ImBottomMinus")] ImBottomMinus = 7004,
        //[Result(검사그룹.표면, 장치구분.Cam03, "ImMiddle1")] ImMiddle1 = 7005,
        //[Result(검사그룹.표면, 장치구분.Cam03, "ImMiddle2")] ImMiddle2 = 7006,
        //[Result(검사그룹.표면, 장치구분.Cam03, "ImMiddle3")] ImMiddle3 = 7007,
        // 8. Surface
        [Result(검사그룹.표면, 장치구분.Cameras)] Surface = 8010,
        // 9. Others
        [Result(검사그룹.표면, 장치구분.QrReader)] QrLegibility = 9010,
        [Result(검사그룹.표면, 장치구분.QrReader)] Imprinted = 9020,
    }
}
