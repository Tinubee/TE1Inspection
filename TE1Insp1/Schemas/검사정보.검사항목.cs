using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace TE1.Schemas
{
    public enum 검사항목 : Int32
    {
        [Result(), ListBindable(false)] None = 0,

        // 1. Hole
        [Result(검사그룹.치수, 장치구분.Cam02, "H01X", Hosts.Measure)] H01X = 1011,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Y", Hosts.Measure)] H01Y = 1012,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01D", Hosts.Measure)] H01D = 1013,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01P", Hosts.Measure)] H01P = 1014,

        [Result(검사그룹.치수, 장치구분.Cam02, "H02X", Hosts.Measure)] H02X = 1021,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Y", Hosts.Measure)] H02Y = 1022,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02D", Hosts.Measure)] H02D = 1023,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02P", Hosts.Measure)] H02P = 1024,

        [Result(검사그룹.치수, 장치구분.Cam02, "H03X", Hosts.Measure)] H03X = 1031,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Y", Hosts.Measure)] H03Y = 1032,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03D", Hosts.Measure)] H03D = 1033,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03P", Hosts.Measure)] H03P = 1034,

        [Result(검사그룹.치수, 장치구분.Cam02, "H04X", Hosts.Measure)] H04X = 1041,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Y", Hosts.Measure)] H04Y = 1042,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04D", Hosts.Measure)] H04D = 1043,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04P", Hosts.Measure)] H04P = 1044,

        [Result(검사그룹.치수, 장치구분.Cam02, "H05X", Hosts.Measure)] H05X = 1051,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Y", Hosts.Measure)] H05Y = 1052,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05D", Hosts.Measure)] H05D = 1053,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05P", Hosts.Measure)] H05P = 1054,

        [Result(검사그룹.치수, 장치구분.Cam02, "H06X", Hosts.Measure)] H06X = 1061,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Y", Hosts.Measure)] H06Y = 1062,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06D", Hosts.Measure)] H06D = 1063,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06P", Hosts.Measure)] H06P = 1064,

        [Result(검사그룹.치수, 장치구분.Cam02, "H07X", Hosts.Measure)] H07X = 1071,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Y", Hosts.Measure)] H07Y = 1072,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07D", Hosts.Measure)] H07D = 1073,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07P", Hosts.Measure)] H07P = 1074,

        [Result(검사그룹.치수, 장치구분.Cam02, "H08X", Hosts.Measure)] H08X = 1081,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Y", Hosts.Measure)] H08Y = 1082,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08D", Hosts.Measure)] H08D = 1083,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08P", Hosts.Measure)] H08P = 1084,

        [Result(검사그룹.치수, 장치구분.Cam02, "H09X", Hosts.Measure)] H09X = 1091,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Y", Hosts.Measure)] H09Y = 1092,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09D", Hosts.Measure)] H09D = 1093,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09P", Hosts.Measure)] H09P = 1094,

        [Result(검사그룹.치수, 장치구분.Cam02, "H10X", Hosts.Measure)] H10X = 1101,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Y", Hosts.Measure)] H10Y = 1102,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10D", Hosts.Measure)] H10D = 1103,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10P", Hosts.Measure)] H10P = 1104,

        [Result(검사그룹.치수, 장치구분.Cam02, "H11X", Hosts.Measure)] H11X = 1111,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Y", Hosts.Measure)] H11Y = 1112,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11D", Hosts.Measure)] H11D = 1113,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11P", Hosts.Measure)] H11P = 1114,

        [Result(검사그룹.치수, 장치구분.Cam02, "H12X", Hosts.Measure)] H12X = 1121,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Y", Hosts.Measure)] H12Y = 1122,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12D", Hosts.Measure)] H12D = 1123,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12P", Hosts.Measure)] H12P = 1124,

        [Result(검사그룹.치수, 장치구분.Cam02, "H13X", Hosts.Measure)] H13X = 1131,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Y", Hosts.Measure)] H13Y = 1132,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13D", Hosts.Measure)] H13D = 1133,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13P", Hosts.Measure)] H13P = 1134,

        [Result(검사그룹.치수, 장치구분.Cam02, "H14X", Hosts.Measure)] H14X = 1141,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Y", Hosts.Measure)] H14Y = 1142,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14D", Hosts.Measure)] H14D = 1143,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14P", Hosts.Measure)] H14P = 1144,

        [Result(검사그룹.치수, 장치구분.Cam02, "H15X", Hosts.Measure)] H15X = 1151,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Y", Hosts.Measure)] H15Y = 1152,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15D", Hosts.Measure)] H15D = 1153,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15P", Hosts.Measure)] H15P = 1154,

        [Result(검사그룹.치수, 장치구분.Cam02, "H16X", Hosts.Measure)] H16X = 1161,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Y", Hosts.Measure)] H16Y = 1162,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16D", Hosts.Measure)] H16D = 1163,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16P", Hosts.Measure)] H16P = 1164,

        [Result(검사그룹.치수, 장치구분.Cam02, "H17X", Hosts.Measure)] H17X = 1171,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Y", Hosts.Measure)] H17Y = 1172,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17D", Hosts.Measure)] H17D = 1173,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17P", Hosts.Measure)] H17P = 1174,

        [Result(검사그룹.치수, 장치구분.Cam02, "H18X", Hosts.Measure)] H18X = 1181,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Y", Hosts.Measure)] H18Y = 1182,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18D", Hosts.Measure)] H18D = 1183,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18P", Hosts.Measure)] H18P = 1184,

        [Result(검사그룹.치수, 장치구분.Cam02, "H19X", Hosts.Measure)] H19X = 1191,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Y", Hosts.Measure)] H19Y = 1192,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19D", Hosts.Measure)] H19D = 1193,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19P", Hosts.Measure)] H19P = 1194,

        [Result(검사그룹.치수, 장치구분.Cam02, "H20X", Hosts.Measure)] H20X = 1201,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Y", Hosts.Measure)] H20Y = 1202,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20D", Hosts.Measure)] H20D = 1203,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20P", Hosts.Measure)] H20P = 1204,

        [Result(검사그룹.치수, 장치구분.Cam02, "H21X", Hosts.Measure)] H21X = 1211,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Y", Hosts.Measure)] H21Y = 1212,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21D", Hosts.Measure)] H21D = 1213,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21P", Hosts.Measure)] H21P = 1214,

        [Result(검사그룹.치수, 장치구분.Cam02, "H22X", Hosts.Measure)] H22X = 1221,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Y", Hosts.Measure)] H22Y = 1222,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22D", Hosts.Measure)] H22D = 1223,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22P", Hosts.Measure)] H22P = 1224,

        [Result(검사그룹.치수, 장치구분.Cam02, "H23X", Hosts.Measure)] H23X = 1231,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Y", Hosts.Measure)] H23Y = 1232,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23D", Hosts.Measure)] H23D = 1233,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23P", Hosts.Measure)] H23P = 1234,

        [Result(검사그룹.치수, 장치구분.Cam02, "H24X", Hosts.Measure)] H24X = 1241,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Y", Hosts.Measure)] H24Y = 1242,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24D", Hosts.Measure)] H24D = 1243,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24P", Hosts.Measure)] H24P = 1244,

        [Result(검사그룹.치수, 장치구분.Cam02, "H25X", Hosts.Measure)] H25X = 1251,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Y", Hosts.Measure)] H25Y = 1252,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25D", Hosts.Measure)] H25D = 1253,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25P", Hosts.Measure)] H25P = 1254,

        [Result(검사그룹.치수, 장치구분.Cam02, "H26X", Hosts.Measure)] H26X = 1261,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Y", Hosts.Measure)] H26Y = 1262,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26D", Hosts.Measure)] H26D = 1263,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26P", Hosts.Measure)] H26P = 1264,

        [Result(검사그룹.치수, 장치구분.Cam02, "H27X", Hosts.Measure)] H27X = 1271,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Y", Hosts.Measure)] H27Y = 1272,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27D", Hosts.Measure)] H27D = 1273,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27P", Hosts.Measure)] H27P = 1274,

        [Result(검사그룹.치수, 장치구분.Cam02, "H28X", Hosts.Measure)] H28X = 1281,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Y", Hosts.Measure)] H28Y = 1282,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28D", Hosts.Measure)] H28D = 1283,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28P", Hosts.Measure)] H28P = 1284,
        // B Datum
        [Result(검사그룹.치수, 장치구분.Cam02, "H37X", Hosts.Measure)] H37X = 1371,
        [Result(검사그룹.치수, 장치구분.Cam02, "H37Y", Hosts.Measure)] H37Y = 1372,
        [Result(검사그룹.치수, 장치구분.Cam02, "H37D", Hosts.Measure)] H37D = 1373,

        // C Datum
        [Result(검사그룹.치수, 장치구분.Cam02, "H38X", Hosts.Measure)] H38X = 1381,
        [Result(검사그룹.치수, 장치구분.Cam02, "H38Y", Hosts.Measure)] H38Y = 1382,
        [Result(검사그룹.치수, 장치구분.Cam02, "H38D", Hosts.Measure)] H38D = 1383,
        //[Result(검사그룹.치수, 장치구분.Cam02, "H38P", Hosts.Measure)] H38L = 1384,

        // Rectangle
        [Result(검사그룹.치수, 장치구분.Cam02, "R01X", Hosts.Measure)] R01X = 1801,
        [Result(검사그룹.치수, 장치구분.Cam02, "R01Y", Hosts.Measure)] R01Y = 1802,
        [Result(검사그룹.치수, 장치구분.Cam02, "R01W", Hosts.Measure)] R01W = 1803,
        [Result(검사그룹.치수, 장치구분.Cam02, "R01L", Hosts.Measure)] R01L = 1804,

        // 2. Trim
        [Result(검사그룹.치수, 장치구분.Cam02, "T001", Hosts.Measure)] T001 = 2001,
        [Result(검사그룹.치수, 장치구분.Cam02, "T002", Hosts.Measure)] T002 = 2002,
        [Result(검사그룹.치수, 장치구분.Cam02, "T003", Hosts.Measure)] T003 = 2003,
        [Result(검사그룹.치수, 장치구분.Cam02, "T004", Hosts.Measure)] T004 = 2004,
        [Result(검사그룹.치수, 장치구분.Cam02, "T005", Hosts.Measure)] T005 = 2005,
        [Result(검사그룹.치수, 장치구분.Cam02, "T006", Hosts.Measure)] T006 = 2006,
        [Result(검사그룹.치수, 장치구분.Cam02, "T007", Hosts.Measure)] T007 = 2007,
        [Result(검사그룹.치수, 장치구분.Cam02, "T008", Hosts.Measure)] T008 = 2008,
        [Result(검사그룹.치수, 장치구분.Cam02, "T009", Hosts.Measure)] T009 = 2009,
        [Result(검사그룹.치수, 장치구분.Cam02, "T010", Hosts.Measure)] T010 = 2010,
        [Result(검사그룹.치수, 장치구분.Cam02, "T011", Hosts.Measure)] T011 = 2011,
        [Result(검사그룹.치수, 장치구분.Cam02, "T012", Hosts.Measure)] T012 = 2012,
        [Result(검사그룹.치수, 장치구분.Cam02, "T013", Hosts.Measure)] T013 = 2013,
        [Result(검사그룹.치수, 장치구분.Cam02, "T014", Hosts.Measure)] T014 = 2014,
        [Result(검사그룹.치수, 장치구분.Cam02, "T015", Hosts.Measure)] T015 = 2015,
        [Result(검사그룹.치수, 장치구분.Cam02, "T016", Hosts.Measure)] T016 = 2016,
        [Result(검사그룹.치수, 장치구분.Cam02, "T017", Hosts.Measure)] T017 = 2017,
        [Result(검사그룹.치수, 장치구분.Cam02, "T018", Hosts.Measure)] T018 = 2018,
        [Result(검사그룹.치수, 장치구분.Cam02, "T019", Hosts.Measure)] T019 = 2019,
        [Result(검사그룹.치수, 장치구분.Cam02, "T020", Hosts.Measure)] T020 = 2020,
        [Result(검사그룹.치수, 장치구분.Cam02, "T021", Hosts.Measure)] T021 = 2021,
        [Result(검사그룹.치수, 장치구분.Cam02, "T022", Hosts.Measure)] T022 = 2022,
        [Result(검사그룹.치수, 장치구분.Cam02, "T023", Hosts.Measure)] T023 = 2023,
        [Result(검사그룹.치수, 장치구분.Cam02, "T024", Hosts.Measure)] T024 = 2024,
        [Result(검사그룹.치수, 장치구분.Cam02, "T025", Hosts.Measure)] T025 = 2025,
        [Result(검사그룹.치수, 장치구분.Cam02, "T026", Hosts.Measure)] T026 = 2026,
        [Result(검사그룹.치수, 장치구분.Cam02, "T027", Hosts.Measure)] T027 = 2027,
        [Result(검사그룹.치수, 장치구분.Cam02, "T028", Hosts.Measure)] T028 = 2028,
        [Result(검사그룹.치수, 장치구분.Cam02, "T029", Hosts.Measure)] T029 = 2029,
        [Result(검사그룹.치수, 장치구분.Cam02, "T030", Hosts.Measure)] T030 = 2030,
        [Result(검사그룹.치수, 장치구분.Cam02, "T031", Hosts.Measure)] T031 = 2031,
        [Result(검사그룹.치수, 장치구분.Cam02, "T032", Hosts.Measure)] T032 = 2032,
        [Result(검사그룹.치수, 장치구분.Cam02, "T033", Hosts.Measure)] T033 = 2033,
        [Result(검사그룹.치수, 장치구분.Cam02, "T034", Hosts.Measure)] T034 = 2034,
        [Result(검사그룹.치수, 장치구분.Cam02, "T035", Hosts.Measure)] T035 = 2035,
        [Result(검사그룹.치수, 장치구분.Cam02, "T036", Hosts.Measure)] T036 = 2036,
        [Result(검사그룹.치수, 장치구분.Cam02, "T037", Hosts.Measure)] T037 = 2037,
        [Result(검사그룹.치수, 장치구분.Cam02, "T038", Hosts.Measure)] T038 = 2038,
        [Result(검사그룹.치수, 장치구분.Cam03, "T039", Hosts.Measure)] T039 = 2039,
        [Result(검사그룹.치수, 장치구분.Cam02, "T040", Hosts.Measure)] T040 = 2040,
        [Result(검사그룹.치수, 장치구분.Cam02, "T041", Hosts.Measure)] T041 = 2041,
        [Result(검사그룹.치수, 장치구분.Cam02, "T042", Hosts.Measure)] T042 = 2042,
        [Result(검사그룹.치수, 장치구분.Cam02, "T043", Hosts.Measure)] T043 = 2043,
        [Result(검사그룹.치수, 장치구분.Cam02, "T044", Hosts.Measure)] T044 = 2044,
        [Result(검사그룹.치수, 장치구분.Cam02, "T045", Hosts.Measure)] T045 = 2045,
        [Result(검사그룹.치수, 장치구분.Cam02, "T046", Hosts.Measure)] T046 = 2046,
        [Result(검사그룹.치수, 장치구분.Cam02, "T047", Hosts.Measure)] T047 = 2047,
        [Result(검사그룹.치수, 장치구분.Cam02, "T048", Hosts.Measure)] T048 = 2048,
        [Result(검사그룹.치수, 장치구분.Cam02, "T049", Hosts.Measure)] T049 = 2049,
        [Result(검사그룹.치수, 장치구분.Cam02, "T050", Hosts.Measure)] T050 = 2050,
        [Result(검사그룹.치수, 장치구분.Cam02, "T051", Hosts.Measure)] T051 = 2051,
        [Result(검사그룹.치수, 장치구분.Cam02, "T052", Hosts.Measure)] T052 = 2052,
        [Result(검사그룹.치수, 장치구분.Cam02, "T053", Hosts.Measure)] T053 = 2053,
        [Result(검사그룹.치수, 장치구분.Cam02, "T054", Hosts.Measure)] T054 = 2054,
        [Result(검사그룹.치수, 장치구분.Cam02, "T055", Hosts.Measure)] T055 = 2055,
        [Result(검사그룹.치수, 장치구분.Cam02, "T056", Hosts.Measure)] T056 = 2056,

        // 3. Bolt
        [Result(검사그룹.치수, 장치구분.Cam03, "BoltTopX", Hosts.Measure)] BoltTopX = 2901,
        [Result(검사그룹.치수, 장치구분.Cam03, "BoltTopY", Hosts.Measure)] BoltTopY = 2902,
        [Result(검사그룹.치수, 장치구분.Cam03, "BoltTopTab", Hosts.Measure)] BoltTopTab = 2903,
        [Result(검사그룹.치수, 장치구분.Cam03, "BoltTopP", Hosts.Measure)] BoltTopP = 2904,

        [Result(검사그룹.치수, 장치구분.Cam03, "BoltBottomX", Hosts.Measure)] BoltBottomX = 2911,
        [Result(검사그룹.치수, 장치구분.Cam03, "BoltBottomY", Hosts.Measure)] BoltBottomY = 2912,
        [Result(검사그룹.치수, 장치구분.Cam03, "BoltBottomTab", Hosts.Measure)] BoltBottomTab = 2913,
        [Result(검사그룹.치수, 장치구분.Cam03, "BoltBottomP", Hosts.Measure)] BoltBottomP = 2914,

        // 4. Mica
        [Result(검사그룹.치수, 장치구분.Cam03, "M01X1", Hosts.Measure)] M01X1 = 4011,
        [Result(검사그룹.치수, 장치구분.Cam03, "M01Y2", Hosts.Measure)] M01Y2 = 4012,
        [Result(검사그룹.치수, 장치구분.Cam03, "M01X3", Hosts.Measure)] M01X3 = 4013,
        [Result(검사그룹.치수, 장치구분.Cam03, "M01Y4", Hosts.Measure)] M01Y4 = 4014,

        [Result(검사그룹.치수, 장치구분.Cam03, "M02X1", Hosts.Measure)] M02X1 = 4021,
        [Result(검사그룹.치수, 장치구분.Cam03, "M02Y2", Hosts.Measure)] M02Y2 = 4022,
        [Result(검사그룹.치수, 장치구분.Cam03, "M02X3", Hosts.Measure)] M02X3 = 4023,
        [Result(검사그룹.치수, 장치구분.Cam03, "M02Y4", Hosts.Measure)] M02Y4 = 4024,

        [Result(검사그룹.치수, 장치구분.Cam03, "M03X1", Hosts.Measure)] M03X1 = 4031,
        [Result(검사그룹.치수, 장치구분.Cam03, "M03Y2", Hosts.Measure)] M03Y2 = 4032,
        [Result(검사그룹.치수, 장치구분.Cam03, "M03X3", Hosts.Measure)] M03X3 = 4033,
        [Result(검사그룹.치수, 장치구분.Cam03, "M03Y4", Hosts.Measure)] M03Y4 = 4034,

        [Result(검사그룹.치수, 장치구분.Cam03, "M04X1", Hosts.Measure)] M04X1 = 4041,
        [Result(검사그룹.치수, 장치구분.Cam03, "M04Y2", Hosts.Measure)] M04Y2 = 4042,
        [Result(검사그룹.치수, 장치구분.Cam03, "M04X3", Hosts.Measure)] M04X3 = 4043,
        [Result(검사그룹.치수, 장치구분.Cam03, "M04Y4", Hosts.Measure)] M04Y4 = 4044,

        [Result(검사그룹.치수, 장치구분.Cam03, "M05X1", Hosts.Measure)] M05X1 = 4051,
        [Result(검사그룹.치수, 장치구분.Cam03, "M05Y2", Hosts.Measure)] M05Y2 = 4052,
        [Result(검사그룹.치수, 장치구분.Cam03, "M05X3", Hosts.Measure)] M05X3 = 4053,
        [Result(검사그룹.치수, 장치구분.Cam03, "M05Y4", Hosts.Measure)] M05Y4 = 4054,

        [Result(검사그룹.치수, 장치구분.Cam03, "M06X1", Hosts.Measure)] M06X1 = 4061,
        [Result(검사그룹.치수, 장치구분.Cam03, "M06Y2", Hosts.Measure)] M06Y2 = 4062,
        [Result(검사그룹.치수, 장치구분.Cam03, "M06X3", Hosts.Measure)] M06X3 = 4063,
        [Result(검사그룹.치수, 장치구분.Cam03, "M06Y4", Hosts.Measure)] M06Y4 = 4064,

        [Result(검사그룹.치수, 장치구분.Cam03, "M07X1", Hosts.Measure)] M07X1 = 4071,
        [Result(검사그룹.치수, 장치구분.Cam03, "M07Y2", Hosts.Measure)] M07Y2 = 4072,
        [Result(검사그룹.치수, 장치구분.Cam03, "M07X3", Hosts.Measure)] M07X3 = 4073,
        [Result(검사그룹.치수, 장치구분.Cam03, "M07Y4", Hosts.Measure)] M07Y4 = 4074,

        [Result(검사그룹.치수, 장치구분.Cam03, "M08X1", Hosts.Measure)] M08X1 = 4081,
        [Result(검사그룹.치수, 장치구분.Cam03, "M08Y2", Hosts.Measure)] M08Y2 = 4082,
        [Result(검사그룹.치수, 장치구분.Cam03, "M08X3", Hosts.Measure)] M08X3 = 4083,
        [Result(검사그룹.치수, 장치구분.Cam03, "M08Y4", Hosts.Measure)] M08Y4 = 4084,

        [Result(검사그룹.치수, 장치구분.Cam03, "M09X1", Hosts.Measure)] M09X1 = 4091,
        [Result(검사그룹.치수, 장치구분.Cam03, "M09Y2", Hosts.Measure)] M09Y2 = 4092,
        [Result(검사그룹.치수, 장치구분.Cam03, "M09X3", Hosts.Measure)] M09X3 = 4093,
        [Result(검사그룹.치수, 장치구분.Cam03, "M09Y4", Hosts.Measure)] M09Y4 = 4094,

        [Result(검사그룹.치수, 장치구분.Cam03, "M10X1", Hosts.Measure)] M10X1 = 4101,
        [Result(검사그룹.치수, 장치구분.Cam03, "M10Y2", Hosts.Measure)] M10Y2 = 4102,
        [Result(검사그룹.치수, 장치구분.Cam03, "M10X3", Hosts.Measure)] M10X3 = 4103,
        [Result(검사그룹.치수, 장치구분.Cam03, "M10Y4", Hosts.Measure)] M10Y4 = 4104,

        [Result(검사그룹.치수, 장치구분.Cam03, "M11X1", Hosts.Measure)] M11X1 = 4111,
        [Result(검사그룹.치수, 장치구분.Cam03, "M11Y2", Hosts.Measure)] M11Y2 = 4112,
        [Result(검사그룹.치수, 장치구분.Cam03, "M11X3", Hosts.Measure)] M11X3 = 4113,
        [Result(검사그룹.치수, 장치구분.Cam03, "M11Y4", Hosts.Measure)] M11Y4 = 4114,

        [Result(검사그룹.치수, 장치구분.Cam03, "M12X1", Hosts.Measure)] M12X1 = 4121,
        [Result(검사그룹.치수, 장치구분.Cam03, "M12Y2", Hosts.Measure)] M12Y2 = 4122,
        [Result(검사그룹.치수, 장치구분.Cam03, "M12X3", Hosts.Measure)] M12X3 = 4123,
        [Result(검사그룹.치수, 장치구분.Cam03, "M12Y4", Hosts.Measure)] M12Y4 = 4124,

        [Result(검사그룹.치수, 장치구분.Cam03, "M13X1", Hosts.Measure)] M13X1 = 4131,
        [Result(검사그룹.치수, 장치구분.Cam03, "M13Y2", Hosts.Measure)] M13Y2 = 4132,
        [Result(검사그룹.치수, 장치구분.Cam03, "M13X3", Hosts.Measure)] M13X3 = 4133,
        [Result(검사그룹.치수, 장치구분.Cam03, "M13Y4", Hosts.Measure)] M13Y4 = 4134,

        [Result(검사그룹.치수, 장치구분.Cam03, "M14X1", Hosts.Measure)] M14X1 = 4141,
        [Result(검사그룹.치수, 장치구분.Cam03, "M14Y2", Hosts.Measure)] M14Y2 = 4142,
        [Result(검사그룹.치수, 장치구분.Cam03, "M14X3", Hosts.Measure)] M14X3 = 4143,
        [Result(검사그룹.치수, 장치구분.Cam03, "M14Y4", Hosts.Measure)] M14Y4 = 4144,

        [Result(검사그룹.치수, 장치구분.Cam03, "M15X1", Hosts.Measure)] M15X1 = 4151,
        [Result(검사그룹.치수, 장치구분.Cam03, "M15Y2", Hosts.Measure)] M15Y2 = 4152,
        [Result(검사그룹.치수, 장치구분.Cam03, "M15X3", Hosts.Measure)] M15X3 = 4153,
        [Result(검사그룹.치수, 장치구분.Cam03, "M15Y4", Hosts.Measure)] M15Y4 = 4154,

        [Result(검사그룹.치수, 장치구분.Cam03, "M16X1", Hosts.Measure)] M16X1 = 4161,
        [Result(검사그룹.치수, 장치구분.Cam03, "M16Y2", Hosts.Measure)] M16Y2 = 4162,
        [Result(검사그룹.치수, 장치구분.Cam03, "M16X3", Hosts.Measure)] M16X3 = 4163,
        [Result(검사그룹.치수, 장치구분.Cam03, "M16Y4", Hosts.Measure)] M16Y4 = 4164,

        [Result(검사그룹.치수, 장치구분.Cam03, "M17X1", Hosts.Measure)] M17X1 = 4171,
        [Result(검사그룹.치수, 장치구분.Cam03, "M17Y2", Hosts.Measure)] M17Y2 = 4172,
        [Result(검사그룹.치수, 장치구분.Cam03, "M17X3", Hosts.Measure)] M17X3 = 4173,
        [Result(검사그룹.치수, 장치구분.Cam03, "M17Y4", Hosts.Measure)] M17Y4 = 4174,

        [Result(검사그룹.치수, 장치구분.Cam03, "M18X1", Hosts.Measure)] M18X1 = 4181,
        [Result(검사그룹.치수, 장치구분.Cam03, "M18Y2", Hosts.Measure)] M18Y2 = 4182,
        [Result(검사그룹.치수, 장치구분.Cam03, "M18X3", Hosts.Measure)] M18X3 = 4183,
        [Result(검사그룹.치수, 장치구분.Cam03, "M18Y4", Hosts.Measure)] M18Y4 = 4184,

        [Result(검사그룹.치수, 장치구분.Cam03, "M19X1", Hosts.Measure)] M19X1 = 4191,
        [Result(검사그룹.치수, 장치구분.Cam03, "M19Y2", Hosts.Measure)] M19Y2 = 4192,
        [Result(검사그룹.치수, 장치구분.Cam03, "M19X3", Hosts.Measure)] M19X3 = 4193,
        [Result(검사그룹.치수, 장치구분.Cam03, "M19Y4", Hosts.Measure)] M19Y4 = 4194,

        [Result(검사그룹.치수, 장치구분.Cam03, "M20X1", Hosts.Measure)] M20X1 = 4201,
        [Result(검사그룹.치수, 장치구분.Cam03, "M20Y2", Hosts.Measure)] M20Y2 = 4202,
        [Result(검사그룹.치수, 장치구분.Cam03, "M20X3", Hosts.Measure)] M20X3 = 4203,
        [Result(검사그룹.치수, 장치구분.Cam03, "M20Y4", Hosts.Measure)] M20Y4 = 4204,

        [Result(검사그룹.치수, 장치구분.Cam03, "M21X1", Hosts.Measure)] M21X1 = 4211,
        [Result(검사그룹.치수, 장치구분.Cam03, "M21Y2", Hosts.Measure)] M21Y2 = 4212,
        [Result(검사그룹.치수, 장치구분.Cam03, "M21X3", Hosts.Measure)] M21X3 = 4213,
        [Result(검사그룹.치수, 장치구분.Cam03, "M21Y4", Hosts.Measure)] M21Y4 = 4214,

        [Result(검사그룹.치수, 장치구분.Cam03, "M22X1", Hosts.Measure)] M22X1 = 4221,
        [Result(검사그룹.치수, 장치구분.Cam03, "M22Y2", Hosts.Measure)] M22Y2 = 4222,
        [Result(검사그룹.치수, 장치구분.Cam03, "M22X3", Hosts.Measure)] M22X3 = 4223,
        [Result(검사그룹.치수, 장치구분.Cam03, "M22Y4", Hosts.Measure)] M22Y4 = 4224,

        [Result(검사그룹.치수, 장치구분.Cam03, "M23X1", Hosts.Measure)] M23X1 = 4231,
        [Result(검사그룹.치수, 장치구분.Cam03, "M23Y2", Hosts.Measure)] M23Y2 = 4232,
        [Result(검사그룹.치수, 장치구분.Cam03, "M23X3", Hosts.Measure)] M23X3 = 4233,
        [Result(검사그룹.치수, 장치구분.Cam03, "M23Y4", Hosts.Measure)] M23Y4 = 4234,

        [Result(검사그룹.치수, 장치구분.Cam03, "M24X1", Hosts.Measure)] M24X1 = 4241,
        [Result(검사그룹.치수, 장치구분.Cam03, "M24Y2", Hosts.Measure)] M24Y2 = 4242,
        [Result(검사그룹.치수, 장치구분.Cam03, "M24X3", Hosts.Measure)] M24X3 = 4243,
        [Result(검사그룹.치수, 장치구분.Cam03, "M24Y4", Hosts.Measure)] M24Y4 = 4244,

        [Result(검사그룹.치수, 장치구분.Cam03, "M25X1", Hosts.Measure)] M25X1 = 4251,
        [Result(검사그룹.치수, 장치구분.Cam03, "M25Y2", Hosts.Measure)] M25Y2 = 4252,
        [Result(검사그룹.치수, 장치구분.Cam03, "M25X3", Hosts.Measure)] M25X3 = 4253,
        [Result(검사그룹.치수, 장치구분.Cam03, "M25Y4", Hosts.Measure)] M25Y4 = 4254,

        [Result(검사그룹.치수, 장치구분.Cam03, "M26X1", Hosts.Measure)] M26X1 = 4261,
        [Result(검사그룹.치수, 장치구분.Cam03, "M26Y2", Hosts.Measure)] M26Y2 = 4262,
        [Result(검사그룹.치수, 장치구분.Cam03, "M26X3", Hosts.Measure)] M26X3 = 4263,
        [Result(검사그룹.치수, 장치구분.Cam03, "M26Y4", Hosts.Measure)] M26Y4 = 4264,

        [Result(검사그룹.치수, 장치구분.Cam03, "M27X1", Hosts.Measure)] M27X1 = 4271,
        [Result(검사그룹.치수, 장치구분.Cam03, "M27Y2", Hosts.Measure)] M27Y2 = 4272,
        [Result(검사그룹.치수, 장치구분.Cam03, "M27X3", Hosts.Measure)] M27X3 = 4273,
        [Result(검사그룹.치수, 장치구분.Cam03, "M27Y4", Hosts.Measure)] M27Y4 = 4274,

        [Result(검사그룹.치수, 장치구분.Cam03, "M28X1", Hosts.Measure)] M28X1 = 4281,
        [Result(검사그룹.치수, 장치구분.Cam03, "M28Y2", Hosts.Measure)] M28Y2 = 4282,
        [Result(검사그룹.치수, 장치구분.Cam03, "M28X3", Hosts.Measure)] M28X3 = 4283,
        [Result(검사그룹.치수, 장치구분.Cam03, "M28Y4", Hosts.Measure)] M28Y4 = 4284,

        [Result(검사그룹.치수, 장치구분.Cam03, "M29X1", Hosts.Measure)] M29X1 = 4291,
        [Result(검사그룹.치수, 장치구분.Cam03, "M29Y2", Hosts.Measure)] M29Y2 = 4292,
        [Result(검사그룹.치수, 장치구분.Cam03, "M29X3", Hosts.Measure)] M29X3 = 4293,
        [Result(검사그룹.치수, 장치구분.Cam03, "M29Y4", Hosts.Measure)] M29Y4 = 4294,

        [Result(검사그룹.치수, 장치구분.Cam03, "M30X1", Hosts.Measure)] M30X1 = 4301,
        [Result(검사그룹.치수, 장치구분.Cam03, "M30Y2", Hosts.Measure)] M30Y2 = 4302,
        [Result(검사그룹.치수, 장치구분.Cam03, "M30X3", Hosts.Measure)] M30X3 = 4303,
        [Result(검사그룹.치수, 장치구분.Cam03, "M30Y4", Hosts.Measure)] M30Y4 = 4304,

        [Result(검사그룹.치수, 장치구분.Cam03, "M31X1", Hosts.Measure)] M31X1 = 4311,
        [Result(검사그룹.치수, 장치구분.Cam03, "M31Y2", Hosts.Measure)] M31Y2 = 4312,
        [Result(검사그룹.치수, 장치구분.Cam03, "M31X3", Hosts.Measure)] M31X3 = 4313,
        [Result(검사그룹.치수, 장치구분.Cam03, "M31Y4", Hosts.Measure)] M31Y4 = 4314,

        [Result(검사그룹.치수, 장치구분.Cam03, "M32X1", Hosts.Measure)] M32X1 = 4321,
        [Result(검사그룹.치수, 장치구분.Cam03, "M32Y2", Hosts.Measure)] M32Y2 = 4322,
        [Result(검사그룹.치수, 장치구분.Cam03, "M32X3", Hosts.Measure)] M32X3 = 4323,
        [Result(검사그룹.치수, 장치구분.Cam03, "M32Y4", Hosts.Measure)] M32Y4 = 4324,

        [Result(검사그룹.치수, 장치구분.Cam03, "M33X1", Hosts.Measure)] M33X1 = 4331,
        [Result(검사그룹.치수, 장치구분.Cam03, "M33Y2", Hosts.Measure)] M33Y2 = 4332,
        [Result(검사그룹.치수, 장치구분.Cam03, "M33X3", Hosts.Measure)] M33X3 = 4333,
        [Result(검사그룹.치수, 장치구분.Cam03, "M33Y4", Hosts.Measure)] M33Y4 = 4334,

        [Result(검사그룹.치수, 장치구분.Cam03, "M34X1", Hosts.Measure)] M34X1 = 4341,
        [Result(검사그룹.치수, 장치구분.Cam03, "M34Y2", Hosts.Measure)] M34Y2 = 4342,
        [Result(검사그룹.치수, 장치구분.Cam03, "M34X3", Hosts.Measure)] M34X3 = 4343,
        [Result(검사그룹.치수, 장치구분.Cam03, "M34Y4", Hosts.Measure)] M34Y4 = 4344,

        [Result(검사그룹.치수, 장치구분.Cam03, "M35X1", Hosts.Measure)] M35X1 = 4351,
        [Result(검사그룹.치수, 장치구분.Cam03, "M35Y2", Hosts.Measure)] M35Y2 = 4352,
        [Result(검사그룹.치수, 장치구분.Cam03, "M35X3", Hosts.Measure)] M35X3 = 4353,
        [Result(검사그룹.치수, 장치구분.Cam03, "M35Y4", Hosts.Measure)] M35Y4 = 4354,

        [Result(검사그룹.치수, 장치구분.Cam03, "M36X1", Hosts.Measure)] M36X1 = 4361,
        [Result(검사그룹.치수, 장치구분.Cam03, "M36Y2", Hosts.Measure)] M36Y2 = 4362,
        [Result(검사그룹.치수, 장치구분.Cam03, "M36X3", Hosts.Measure)] M36X3 = 4363,
        [Result(검사그룹.치수, 장치구분.Cam03, "M36Y4", Hosts.Measure)] M36Y4 = 4364,

        [Result(검사그룹.치수, 장치구분.Cam03, "M37X1", Hosts.Measure)] M37X1 = 4371,
        [Result(검사그룹.치수, 장치구분.Cam03, "M37Y2", Hosts.Measure)] M37Y2 = 4372,
        [Result(검사그룹.치수, 장치구분.Cam03, "M37X3", Hosts.Measure)] M37X3 = 4373,
        [Result(검사그룹.치수, 장치구분.Cam03, "M37Y4", Hosts.Measure)] M37Y4 = 4374,

        [Result(검사그룹.치수, 장치구분.Cam03, "M38X1", Hosts.Measure)] M38X1 = 4381,
        [Result(검사그룹.치수, 장치구분.Cam03, "M38Y2", Hosts.Measure)] M38Y2 = 4382,
        [Result(검사그룹.치수, 장치구분.Cam03, "M38X3", Hosts.Measure)] M38X3 = 4383,
        [Result(검사그룹.치수, 장치구분.Cam03, "M38Y4", Hosts.Measure)] M38Y4 = 4384,

        [Result(검사그룹.치수, 장치구분.Cam03, "M39X1", Hosts.Measure)] M39X1 = 4391,
        [Result(검사그룹.치수, 장치구분.Cam03, "M39Y2", Hosts.Measure)] M39Y2 = 4392,
        [Result(검사그룹.치수, 장치구분.Cam03, "M39X3", Hosts.Measure)] M39X3 = 4393,
        [Result(검사그룹.치수, 장치구분.Cam03, "M39Y4", Hosts.Measure)] M39Y4 = 4394,

        [Result(검사그룹.치수, 장치구분.Cam03, "M40X1", Hosts.Measure)] M40X1 = 4401,
        [Result(검사그룹.치수, 장치구분.Cam03, "M40Y2", Hosts.Measure)] M40Y2 = 4402,
        [Result(검사그룹.치수, 장치구분.Cam03, "M40X3", Hosts.Measure)] M40X3 = 4403,
        [Result(검사그룹.치수, 장치구분.Cam03, "M40Y4", Hosts.Measure)] M40Y4 = 4404,

        [Result(검사그룹.치수, 장치구분.Cam03, "M41X1", Hosts.Measure)] M41X1 = 4411,
        [Result(검사그룹.치수, 장치구분.Cam03, "M41Y2", Hosts.Measure)] M41Y2 = 4412,
        [Result(검사그룹.치수, 장치구분.Cam03, "M41X3", Hosts.Measure)] M41X3 = 4413,
        [Result(검사그룹.치수, 장치구분.Cam03, "M41Y4", Hosts.Measure)] M41Y4 = 4414,

        [Result(검사그룹.치수, 장치구분.Cam03, "M42X1", Hosts.Measure)] M42X1 = 4421,
        [Result(검사그룹.치수, 장치구분.Cam03, "M42Y2", Hosts.Measure)] M42Y2 = 4422,
        [Result(검사그룹.치수, 장치구분.Cam03, "M42X3", Hosts.Measure)] M42X3 = 4423,
        [Result(검사그룹.치수, 장치구분.Cam03, "M42Y4", Hosts.Measure)] M42Y4 = 4424,

        [Result(검사그룹.치수, 장치구분.Cam03, "M43X1", Hosts.Measure)] M43X1 = 4431,
        [Result(검사그룹.치수, 장치구분.Cam03, "M43Y2", Hosts.Measure)] M43Y2 = 4432,
        [Result(검사그룹.치수, 장치구분.Cam03, "M43X3", Hosts.Measure)] M43X3 = 4433,
        [Result(검사그룹.치수, 장치구분.Cam03, "M43Y4", Hosts.Measure)] M43Y4 = 4434,

        [Result(검사그룹.치수, 장치구분.Cam03, "M44X1", Hosts.Measure)] M44X1 = 4441,
        [Result(검사그룹.치수, 장치구분.Cam03, "M44Y2", Hosts.Measure)] M44Y2 = 4442,
        [Result(검사그룹.치수, 장치구분.Cam03, "M44X3", Hosts.Measure)] M44X3 = 4443,
        [Result(검사그룹.치수, 장치구분.Cam03, "M44Y4", Hosts.Measure)] M44Y4 = 4444,

        [Result(검사그룹.치수, 장치구분.Cam03, "M45X1", Hosts.Measure)] M45X1 = 4451,
        [Result(검사그룹.치수, 장치구분.Cam03, "M45Y2", Hosts.Measure)] M45Y2 = 4452,
        [Result(검사그룹.치수, 장치구분.Cam03, "M45X3", Hosts.Measure)] M45X3 = 4453,
        [Result(검사그룹.치수, 장치구분.Cam03, "M45Y4", Hosts.Measure)] M45Y4 = 4454,

        [Result(검사그룹.치수, 장치구분.Cam03, "M46X1", Hosts.Measure)] M46X1 = 4461,
        [Result(검사그룹.치수, 장치구분.Cam03, "M46Y2", Hosts.Measure)] M46Y2 = 4462,
        [Result(검사그룹.치수, 장치구분.Cam03, "M46X3", Hosts.Measure)] M46X3 = 4463,
        [Result(검사그룹.치수, 장치구분.Cam03, "M46Y4", Hosts.Measure)] M46Y4 = 4464,

        [Result(검사그룹.치수, 장치구분.Cam03, "M47X1", Hosts.Measure)] M47X1 = 4471,
        [Result(검사그룹.치수, 장치구분.Cam03, "M47Y2", Hosts.Measure)] M47Y2 = 4472,
        [Result(검사그룹.치수, 장치구분.Cam03, "M47X3", Hosts.Measure)] M47X3 = 4473,
        [Result(검사그룹.치수, 장치구분.Cam03, "M47Y4", Hosts.Measure)] M47Y4 = 4474,

        [Result(검사그룹.치수, 장치구분.Cam03, "M48X1", Hosts.Measure)] M48X1 = 4481,
        [Result(검사그룹.치수, 장치구분.Cam03, "M48Y2", Hosts.Measure)] M48Y2 = 4482,
        [Result(검사그룹.치수, 장치구분.Cam03, "M48X3", Hosts.Measure)] M48X3 = 4483,
        [Result(검사그룹.치수, 장치구분.Cam03, "M48Y4", Hosts.Measure)] M48Y4 = 4484,

        [Result(검사그룹.치수, 장치구분.Cam03, "M49X1", Hosts.Measure)] M49X1 = 4491,
        [Result(검사그룹.치수, 장치구분.Cam03, "M49Y2", Hosts.Measure)] M49Y2 = 4492,
        [Result(검사그룹.치수, 장치구분.Cam03, "M49X3", Hosts.Measure)] M49X3 = 4493,
        [Result(검사그룹.치수, 장치구분.Cam03, "M49Y4", Hosts.Measure)] M49Y4 = 4494,

        [Result(검사그룹.치수, 장치구분.Cam03, "M50X1", Hosts.Measure)] M50X1 = 4501,
        [Result(검사그룹.치수, 장치구분.Cam03, "M50Y2", Hosts.Measure)] M50Y2 = 4502,
        [Result(검사그룹.치수, 장치구분.Cam03, "M50X3", Hosts.Measure)] M50X3 = 4503,
        [Result(검사그룹.치수, 장치구분.Cam03, "M50Y4", Hosts.Measure)] M50Y4 = 4504,

        [Result(검사그룹.치수, 장치구분.Cam03, "M51X1", Hosts.Measure)] M51X1 = 4511,
        [Result(검사그룹.치수, 장치구분.Cam03, "M51Y2", Hosts.Measure)] M51Y2 = 4512,
        [Result(검사그룹.치수, 장치구분.Cam03, "M51X3", Hosts.Measure)] M51X3 = 4513,
        [Result(검사그룹.치수, 장치구분.Cam03, "M51Y4", Hosts.Measure)] M51Y4 = 4514,

        [Result(검사그룹.치수, 장치구분.Cam03, "M52X1", Hosts.Measure)] M52X1 = 4521,
        [Result(검사그룹.치수, 장치구분.Cam03, "M52Y2", Hosts.Measure)] M52Y2 = 4522,
        [Result(검사그룹.치수, 장치구분.Cam03, "M52X3", Hosts.Measure)] M52X3 = 4523,
        [Result(검사그룹.치수, 장치구분.Cam03, "M52Y4", Hosts.Measure)] M52Y4 = 4524,

        [Result(검사그룹.치수, 장치구분.Cam03, "M53X1", Hosts.Measure)] M53X1 = 4531,
        [Result(검사그룹.치수, 장치구분.Cam03, "M53Y2", Hosts.Measure)] M53Y2 = 4532,
        [Result(검사그룹.치수, 장치구분.Cam03, "M53X3", Hosts.Measure)] M53X3 = 4533,
        [Result(검사그룹.치수, 장치구분.Cam03, "M53Y4", Hosts.Measure)] M53Y4 = 4534,

        [Result(검사그룹.치수, 장치구분.Cam03, "M54X1", Hosts.Measure)] M54X1 = 4541,
        [Result(검사그룹.치수, 장치구분.Cam03, "M54Y2", Hosts.Measure)] M54Y2 = 4542,
        [Result(검사그룹.치수, 장치구분.Cam03, "M54X3", Hosts.Measure)] M54X3 = 4543,
        [Result(검사그룹.치수, 장치구분.Cam03, "M54Y4", Hosts.Measure)] M54Y4 = 4544,

        [Result(검사그룹.치수, 장치구분.Cam03, "M55X1", Hosts.Measure)] M55X1 = 4551,
        [Result(검사그룹.치수, 장치구분.Cam03, "M55Y2", Hosts.Measure)] M55Y2 = 4552,
        [Result(검사그룹.치수, 장치구분.Cam03, "M55X3", Hosts.Measure)] M55X3 = 4553,
        [Result(검사그룹.치수, 장치구분.Cam03, "M55Y4", Hosts.Measure)] M55Y4 = 4554,

        [Result(검사그룹.치수, 장치구분.Cam03, "M56X1", Hosts.Measure)] M56X1 = 4561,
        [Result(검사그룹.치수, 장치구분.Cam03, "M56Y2", Hosts.Measure)] M56Y2 = 4562,
        [Result(검사그룹.치수, 장치구분.Cam03, "M56X3", Hosts.Measure)] M56X3 = 4563,
        [Result(검사그룹.치수, 장치구분.Cam03, "M56Y4", Hosts.Measure)] M56Y4 = 4564,

        [Result(검사그룹.치수, 장치구분.Cam03, "M57X1", Hosts.Measure)] M57X1 = 4571,
        [Result(검사그룹.치수, 장치구분.Cam03, "M57Y2", Hosts.Measure)] M57Y2 = 4572,
        [Result(검사그룹.치수, 장치구분.Cam03, "M57X3", Hosts.Measure)] M57X3 = 4573,
        [Result(검사그룹.치수, 장치구분.Cam03, "M57Y4", Hosts.Measure)] M57Y4 = 4574,

        [Result(검사그룹.치수, 장치구분.Cam03, "M58X1", Hosts.Measure)] M58X1 = 4581,
        [Result(검사그룹.치수, 장치구분.Cam03, "M58Y2", Hosts.Measure)] M58Y2 = 4582,
        [Result(검사그룹.치수, 장치구분.Cam03, "M58X3", Hosts.Measure)] M58X3 = 4583,
        [Result(검사그룹.치수, 장치구분.Cam03, "M58Y4", Hosts.Measure)] M58Y4 = 4584,

        [Result(검사그룹.치수, 장치구분.Cam03, "M59X1", Hosts.Measure)] M59X1 = 4591,
        [Result(검사그룹.치수, 장치구분.Cam03, "M59Y2", Hosts.Measure)] M59Y2 = 4592,
        [Result(검사그룹.치수, 장치구분.Cam03, "M59X3", Hosts.Measure)] M59X3 = 4593,
        [Result(검사그룹.치수, 장치구분.Cam03, "M59Y4", Hosts.Measure)] M59Y4 = 4594,

        [Result(검사그룹.치수, 장치구분.Cam03, "M60X1", Hosts.Measure)] M60X1 = 4601,
        [Result(검사그룹.치수, 장치구분.Cam03, "M60Y2", Hosts.Measure)] M60Y2 = 4602,
        [Result(검사그룹.치수, 장치구분.Cam03, "M60X3", Hosts.Measure)] M60X3 = 4603,
        [Result(검사그룹.치수, 장치구분.Cam03, "M60Y4", Hosts.Measure)] M60Y4 = 4604,

        // 5. Flatness
        [Result(검사그룹.치수, 장치구분.Flatness, "F01", Hosts.Surface)] F01 = 5010,
        [Result(검사그룹.치수, 장치구분.Flatness, "F02", Hosts.Surface)] F02 = 5020,
        [Result(검사그룹.치수, 장치구분.Flatness, "F03", Hosts.Surface)] F03 = 5030,
        [Result(검사그룹.치수, 장치구분.Flatness, "F04", Hosts.Surface)] F04 = 5040,
        [Result(검사그룹.치수, 장치구분.Flatness, "F05", Hosts.Surface)] F05 = 5050,
        [Result(검사그룹.치수, 장치구분.Flatness, "F06", Hosts.Surface)] F06 = 5060,
        [Result(검사그룹.치수, 장치구분.Flatness, "F07", Hosts.Surface)] F07 = 5070,
        [Result(검사그룹.치수, 장치구분.Flatness, "F08", Hosts.Surface)] F08 = 5080,

        // 6. Thickness
        [Result(검사그룹.치수, 장치구분.Thickness, "PThickness")] PThickness = 6010,

        // 7. 각인
        [Result(검사그룹.치수, 장치구분.Cam03, "ImTopPlus", Hosts.Measure)] ImTopPlus = 7001,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImBottomPlus", Hosts.Measure)] ImBottomPlus = 7002,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImTopMinus", Hosts.Measure)] ImTopMinus = 7003,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImBottomMinus", Hosts.Measure)] ImBottomMinus = 7004,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImMiddle1", Hosts.Measure)] ImMiddle1 = 7005,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImMiddle2", Hosts.Measure)] ImMiddle2 = 7006,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImMiddle3", Hosts.Measure)] ImMiddle3 = 7007,

        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1TR", Hosts.Measure)] ImSheetM1TR = 7101,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1TRWidth", Hosts.Measure)] ImSheetM1TRWidth = 7102,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1BR", Hosts.Measure)] ImSheetM1BR = 7103,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1BRWidth", Hosts.Measure)] ImSheetM1BRWidth = 7104,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1TL", Hosts.Measure)] ImSheetM1TL = 7105,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1TLWidth", Hosts.Measure)] ImSheetM1TLWidth = 7106,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1TLHeight", Hosts.Measure)] ImSheetM1TLHeight = 7107,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1BL", Hosts.Measure)] ImSheetM1BL = 7108,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1BLWidth", Hosts.Measure)] ImSheetM1BLWidth = 7109,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM1BLHeight", Hosts.Measure)] ImSheetM1BLHeight = 7110,

        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2TR", Hosts.Measure)] ImSheetM2TR = 7201,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2TRWidth", Hosts.Measure)] ImSheetM2TRWidth = 7202,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2BR", Hosts.Measure)] ImSheetM2BR = 7203,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2BRWidth", Hosts.Measure)] ImSheetM2BRWidth = 7204,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2TL", Hosts.Measure)] ImSheetM2TL = 7205,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2TLWidth", Hosts.Measure)] ImSheetM2TLWidth = 7206,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2TLHeight", Hosts.Measure)] ImSheetM2TLHeight = 7207,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2BL", Hosts.Measure)] ImSheetM2BL = 7208,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2BLWidth", Hosts.Measure)] ImSheetM2BLWidth = 7209,
        [Result(검사그룹.치수, 장치구분.Cam03, "ImSheetM2BLHeight", Hosts.Measure)] ImSheetM2BLHeight = 7210,
        // 8. Surface
        [Result(검사그룹.표면, 장치구분.Cameras)] Surface = 8010,
        // 9. Others
        [Result(검사그룹.표면, 장치구분.QrReader)] QrLegibility = 9010,
        [Result(검사그룹.표면, 장치구분.QrReader)] Imprinted = 9020,

        //10. Hole Burr
        // H01Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Burr01", Hosts.Measure)] H01Burr01 = 15001,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Burr02", Hosts.Measure)] H01Burr02 = 15002,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Burr03", Hosts.Measure)] H01Burr03 = 15003,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Burr04", Hosts.Measure)] H01Burr04 = 15004,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Burr05", Hosts.Measure)] H01Burr05 = 15005,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Burr06", Hosts.Measure)] H01Burr06 = 15006,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Burr07", Hosts.Measure)] H01Burr07 = 15007,
        [Result(검사그룹.치수, 장치구분.Cam02, "H01Burr08", Hosts.Measure)] H01Burr08 = 15008,

        // H02Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Burr01", Hosts.Measure)] H02Burr01 = 15009,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Burr02", Hosts.Measure)] H02Burr02 = 15010,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Burr03", Hosts.Measure)] H02Burr03 = 15011,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Burr04", Hosts.Measure)] H02Burr04 = 15012,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Burr05", Hosts.Measure)] H02Burr05 = 15013,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Burr06", Hosts.Measure)] H02Burr06 = 15014,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Burr07", Hosts.Measure)] H02Burr07 = 15015,
        [Result(검사그룹.치수, 장치구분.Cam02, "H02Burr08", Hosts.Measure)] H02Burr08 = 15016,

        // H03Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Burr01", Hosts.Measure)] H03Burr01 = 15017,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Burr02", Hosts.Measure)] H03Burr02 = 15018,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Burr03", Hosts.Measure)] H03Burr03 = 15019,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Burr04", Hosts.Measure)] H03Burr04 = 15020,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Burr05", Hosts.Measure)] H03Burr05 = 15021,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Burr06", Hosts.Measure)] H03Burr06 = 15022,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Burr07", Hosts.Measure)] H03Burr07 = 15023,
        [Result(검사그룹.치수, 장치구분.Cam02, "H03Burr08", Hosts.Measure)] H03Burr08 = 15024,

        // H04Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Burr01", Hosts.Measure)] H04Burr01 = 15025,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Burr02", Hosts.Measure)] H04Burr02 = 15026,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Burr03", Hosts.Measure)] H04Burr03 = 15027,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Burr04", Hosts.Measure)] H04Burr04 = 15028,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Burr05", Hosts.Measure)] H04Burr05 = 15029,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Burr06", Hosts.Measure)] H04Burr06 = 15030,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Burr07", Hosts.Measure)] H04Burr07 = 15031,
        [Result(검사그룹.치수, 장치구분.Cam02, "H04Burr08", Hosts.Measure)] H04Burr08 = 15032,

        // H05Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Burr01", Hosts.Measure)] H05Burr01 = 15033,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Burr02", Hosts.Measure)] H05Burr02 = 15034,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Burr03", Hosts.Measure)] H05Burr03 = 15035,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Burr04", Hosts.Measure)] H05Burr04 = 15036,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Burr05", Hosts.Measure)] H05Burr05 = 15037,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Burr06", Hosts.Measure)] H05Burr06 = 15038,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Burr07", Hosts.Measure)] H05Burr07 = 15039,
        [Result(검사그룹.치수, 장치구분.Cam02, "H05Burr08", Hosts.Measure)] H05Burr08 = 15040,

        // H06Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Burr01", Hosts.Measure)] H06Burr01 = 15041,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Burr02", Hosts.Measure)] H06Burr02 = 15042,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Burr03", Hosts.Measure)] H06Burr03 = 15043,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Burr04", Hosts.Measure)] H06Burr04 = 15044,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Burr05", Hosts.Measure)] H06Burr05 = 15045,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Burr06", Hosts.Measure)] H06Burr06 = 15046,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Burr07", Hosts.Measure)] H06Burr07 = 15047,
        [Result(검사그룹.치수, 장치구분.Cam02, "H06Burr08", Hosts.Measure)] H06Burr08 = 15048,

        // H07Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Burr01", Hosts.Measure)] H07Burr01 = 15049,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Burr02", Hosts.Measure)] H07Burr02 = 15050,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Burr03", Hosts.Measure)] H07Burr03 = 15051,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Burr04", Hosts.Measure)] H07Burr04 = 15052,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Burr05", Hosts.Measure)] H07Burr05 = 15053,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Burr06", Hosts.Measure)] H07Burr06 = 15054,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Burr07", Hosts.Measure)] H07Burr07 = 15055,
        [Result(검사그룹.치수, 장치구분.Cam02, "H07Burr08", Hosts.Measure)] H07Burr08 = 15056,

        // H08Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Burr01", Hosts.Measure)] H08Burr01 = 15057,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Burr02", Hosts.Measure)] H08Burr02 = 15058,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Burr03", Hosts.Measure)] H08Burr03 = 15059,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Burr04", Hosts.Measure)] H08Burr04 = 15060,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Burr05", Hosts.Measure)] H08Burr05 = 15061,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Burr06", Hosts.Measure)] H08Burr06 = 15062,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Burr07", Hosts.Measure)] H08Burr07 = 15063,
        [Result(검사그룹.치수, 장치구분.Cam02, "H08Burr08", Hosts.Measure)] H08Burr08 = 15064,

        // H09Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Burr01", Hosts.Measure)] H09Burr01 = 15065,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Burr02", Hosts.Measure)] H09Burr02 = 15066,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Burr03", Hosts.Measure)] H09Burr03 = 15067,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Burr04", Hosts.Measure)] H09Burr04 = 15068,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Burr05", Hosts.Measure)] H09Burr05 = 15069,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Burr06", Hosts.Measure)] H09Burr06 = 15070,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Burr07", Hosts.Measure)] H09Burr07 = 15071,
        [Result(검사그룹.치수, 장치구분.Cam02, "H09Burr08", Hosts.Measure)] H09Burr08 = 15072,

        // H10Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Burr01", Hosts.Measure)] H10Burr01 = 15073,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Burr02", Hosts.Measure)] H10Burr02 = 15074,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Burr03", Hosts.Measure)] H10Burr03 = 15075,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Burr04", Hosts.Measure)] H10Burr04 = 15076,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Burr05", Hosts.Measure)] H10Burr05 = 15077,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Burr06", Hosts.Measure)] H10Burr06 = 15078,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Burr07", Hosts.Measure)] H10Burr07 = 15079,
        [Result(검사그룹.치수, 장치구분.Cam02, "H10Burr08", Hosts.Measure)] H10Burr08 = 15080,

        // H11Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Burr01", Hosts.Measure)] H11Burr01 = 15081,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Burr02", Hosts.Measure)] H11Burr02 = 15082,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Burr03", Hosts.Measure)] H11Burr03 = 15083,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Burr04", Hosts.Measure)] H11Burr04 = 15084,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Burr05", Hosts.Measure)] H11Burr05 = 15085,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Burr06", Hosts.Measure)] H11Burr06 = 15086,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Burr07", Hosts.Measure)] H11Burr07 = 15087,
        [Result(검사그룹.치수, 장치구분.Cam02, "H11Burr08", Hosts.Measure)] H11Burr08 = 15088,

        // H12Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Burr01", Hosts.Measure)] H12Burr01 = 15089,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Burr02", Hosts.Measure)] H12Burr02 = 15090,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Burr03", Hosts.Measure)] H12Burr03 = 15091,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Burr04", Hosts.Measure)] H12Burr04 = 15092,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Burr05", Hosts.Measure)] H12Burr05 = 15093,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Burr06", Hosts.Measure)] H12Burr06 = 15094,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Burr07", Hosts.Measure)] H12Burr07 = 15095,
        [Result(검사그룹.치수, 장치구분.Cam02, "H12Burr08", Hosts.Measure)] H12Burr08 = 15096,

        // H13Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Burr01", Hosts.Measure)] H13Burr01 = 15097,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Burr02", Hosts.Measure)] H13Burr02 = 15098,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Burr03", Hosts.Measure)] H13Burr03 = 15099,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Burr04", Hosts.Measure)] H13Burr04 = 15100,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Burr05", Hosts.Measure)] H13Burr05 = 15101,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Burr06", Hosts.Measure)] H13Burr06 = 15102,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Burr07", Hosts.Measure)] H13Burr07 = 15103,
        [Result(검사그룹.치수, 장치구분.Cam02, "H13Burr08", Hosts.Measure)] H13Burr08 = 15104,

        // H14Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Burr01", Hosts.Measure)] H14Burr01 = 15105,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Burr02", Hosts.Measure)] H14Burr02 = 15106,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Burr03", Hosts.Measure)] H14Burr03 = 15107,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Burr04", Hosts.Measure)] H14Burr04 = 15108,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Burr05", Hosts.Measure)] H14Burr05 = 15109,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Burr06", Hosts.Measure)] H14Burr06 = 15110,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Burr07", Hosts.Measure)] H14Burr07 = 15111,
        [Result(검사그룹.치수, 장치구분.Cam02, "H14Burr08", Hosts.Measure)] H14Burr08 = 15112,

        // H15Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Burr01", Hosts.Measure)] H15Burr01 = 15113,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Burr02", Hosts.Measure)] H15Burr02 = 15114,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Burr03", Hosts.Measure)] H15Burr03 = 15115,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Burr04", Hosts.Measure)] H15Burr04 = 15116,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Burr05", Hosts.Measure)] H15Burr05 = 15117,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Burr06", Hosts.Measure)] H15Burr06 = 15118,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Burr07", Hosts.Measure)] H15Burr07 = 15119,
        [Result(검사그룹.치수, 장치구분.Cam02, "H15Burr08", Hosts.Measure)] H15Burr08 = 15120,

        // H16Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Burr01", Hosts.Measure)] H16Burr01 = 15121,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Burr02", Hosts.Measure)] H16Burr02 = 15122,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Burr03", Hosts.Measure)] H16Burr03 = 15123,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Burr04", Hosts.Measure)] H16Burr04 = 15124,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Burr05", Hosts.Measure)] H16Burr05 = 15125,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Burr06", Hosts.Measure)] H16Burr06 = 15126,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Burr07", Hosts.Measure)] H16Burr07 = 15127,
        [Result(검사그룹.치수, 장치구분.Cam02, "H16Burr08", Hosts.Measure)] H16Burr08 = 15128,

        // H17Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Burr01", Hosts.Measure)] H17Burr01 = 15129,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Burr02", Hosts.Measure)] H17Burr02 = 15130,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Burr03", Hosts.Measure)] H17Burr03 = 15131,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Burr04", Hosts.Measure)] H17Burr04 = 15132,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Burr05", Hosts.Measure)] H17Burr05 = 15133,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Burr06", Hosts.Measure)] H17Burr06 = 15134,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Burr07", Hosts.Measure)] H17Burr07 = 15135,
        [Result(검사그룹.치수, 장치구분.Cam02, "H17Burr08", Hosts.Measure)] H17Burr08 = 15136,

        // H18Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Burr01", Hosts.Measure)] H18Burr01 = 15137,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Burr02", Hosts.Measure)] H18Burr02 = 15138,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Burr03", Hosts.Measure)] H18Burr03 = 15139,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Burr04", Hosts.Measure)] H18Burr04 = 15140,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Burr05", Hosts.Measure)] H18Burr05 = 15141,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Burr06", Hosts.Measure)] H18Burr06 = 15142,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Burr07", Hosts.Measure)] H18Burr07 = 15143,
        [Result(검사그룹.치수, 장치구분.Cam02, "H18Burr08", Hosts.Measure)] H18Burr08 = 15144,

        // H19Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Burr01", Hosts.Measure)] H19Burr01 = 15145,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Burr02", Hosts.Measure)] H19Burr02 = 15146,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Burr03", Hosts.Measure)] H19Burr03 = 15147,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Burr04", Hosts.Measure)] H19Burr04 = 15148,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Burr05", Hosts.Measure)] H19Burr05 = 15149,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Burr06", Hosts.Measure)] H19Burr06 = 15150,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Burr07", Hosts.Measure)] H19Burr07 = 15151,
        [Result(검사그룹.치수, 장치구분.Cam02, "H19Burr08", Hosts.Measure)] H19Burr08 = 15152,

        // H20Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Burr01", Hosts.Measure)] H20Burr01 = 15153,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Burr02", Hosts.Measure)] H20Burr02 = 15154,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Burr03", Hosts.Measure)] H20Burr03 = 15155,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Burr04", Hosts.Measure)] H20Burr04 = 15156,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Burr05", Hosts.Measure)] H20Burr05 = 15157,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Burr06", Hosts.Measure)] H20Burr06 = 15158,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Burr07", Hosts.Measure)] H20Burr07 = 15159,
        [Result(검사그룹.치수, 장치구분.Cam02, "H20Burr08", Hosts.Measure)] H20Burr08 = 15160,

        // H21Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Burr01", Hosts.Measure)] H21Burr01 = 15161,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Burr02", Hosts.Measure)] H21Burr02 = 15162,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Burr03", Hosts.Measure)] H21Burr03 = 15163,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Burr04", Hosts.Measure)] H21Burr04 = 15164,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Burr05", Hosts.Measure)] H21Burr05 = 15165,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Burr06", Hosts.Measure)] H21Burr06 = 15166,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Burr07", Hosts.Measure)] H21Burr07 = 15167,
        [Result(검사그룹.치수, 장치구분.Cam02, "H21Burr08", Hosts.Measure)] H21Burr08 = 15168,

        // H22Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Burr01", Hosts.Measure)] H22Burr01 = 15169,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Burr02", Hosts.Measure)] H22Burr02 = 15170,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Burr03", Hosts.Measure)] H22Burr03 = 15171,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Burr04", Hosts.Measure)] H22Burr04 = 15172,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Burr05", Hosts.Measure)] H22Burr05 = 15173,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Burr06", Hosts.Measure)] H22Burr06 = 15174,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Burr07", Hosts.Measure)] H22Burr07 = 15175,
        [Result(검사그룹.치수, 장치구분.Cam02, "H22Burr08", Hosts.Measure)] H22Burr08 = 15176,

        // H23Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Burr01", Hosts.Measure)] H23Burr01 = 15177,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Burr02", Hosts.Measure)] H23Burr02 = 15178,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Burr03", Hosts.Measure)] H23Burr03 = 15179,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Burr04", Hosts.Measure)] H23Burr04 = 15180,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Burr05", Hosts.Measure)] H23Burr05 = 15181,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Burr06", Hosts.Measure)] H23Burr06 = 15182,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Burr07", Hosts.Measure)] H23Burr07 = 15183,
        [Result(검사그룹.치수, 장치구분.Cam02, "H23Burr08", Hosts.Measure)] H23Burr08 = 15184,

        // H24Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Burr01", Hosts.Measure)] H24Burr01 = 15185,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Burr02", Hosts.Measure)] H24Burr02 = 15186,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Burr03", Hosts.Measure)] H24Burr03 = 15187,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Burr04", Hosts.Measure)] H24Burr04 = 15188,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Burr05", Hosts.Measure)] H24Burr05 = 15189,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Burr06", Hosts.Measure)] H24Burr06 = 15190,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Burr07", Hosts.Measure)] H24Burr07 = 15191,
        [Result(검사그룹.치수, 장치구분.Cam02, "H24Burr08", Hosts.Measure)] H24Burr08 = 15192,

        // H25Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Burr01", Hosts.Measure)] H25Burr01 = 15193,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Burr02", Hosts.Measure)] H25Burr02 = 15194,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Burr03", Hosts.Measure)] H25Burr03 = 15195,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Burr04", Hosts.Measure)] H25Burr04 = 15196,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Burr05", Hosts.Measure)] H25Burr05 = 15197,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Burr06", Hosts.Measure)] H25Burr06 = 15198,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Burr07", Hosts.Measure)] H25Burr07 = 15199,
        [Result(검사그룹.치수, 장치구분.Cam02, "H25Burr08", Hosts.Measure)] H25Burr08 = 15200,

        // H26Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Burr01", Hosts.Measure)] H26Burr01 = 15201,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Burr02", Hosts.Measure)] H26Burr02 = 15202,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Burr03", Hosts.Measure)] H26Burr03 = 15203,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Burr04", Hosts.Measure)] H26Burr04 = 15204,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Burr05", Hosts.Measure)] H26Burr05 = 15205,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Burr06", Hosts.Measure)] H26Burr06 = 15206,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Burr07", Hosts.Measure)] H26Burr07 = 15207,
        [Result(검사그룹.치수, 장치구분.Cam02, "H26Burr08", Hosts.Measure)] H26Burr08 = 15208,

        // H27Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Burr01", Hosts.Measure)] H27Burr01 = 15209,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Burr02", Hosts.Measure)] H27Burr02 = 15210,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Burr03", Hosts.Measure)] H27Burr03 = 15211,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Burr04", Hosts.Measure)] H27Burr04 = 15212,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Burr05", Hosts.Measure)] H27Burr05 = 15213,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Burr06", Hosts.Measure)] H27Burr06 = 15214,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Burr07", Hosts.Measure)] H27Burr07 = 15215,
        [Result(검사그룹.치수, 장치구분.Cam02, "H27Burr08", Hosts.Measure)] H27Burr08 = 15216,

        // H28Burr
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Burr01", Hosts.Measure)] H28Burr01 = 15217,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Burr02", Hosts.Measure)] H28Burr02 = 15218,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Burr03", Hosts.Measure)] H28Burr03 = 15219,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Burr04", Hosts.Measure)] H28Burr04 = 15220,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Burr05", Hosts.Measure)] H28Burr05 = 15221,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Burr06", Hosts.Measure)] H28Burr06 = 15222,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Burr07", Hosts.Measure)] H28Burr07 = 15223,
        [Result(검사그룹.치수, 장치구분.Cam02, "H28Burr08", Hosts.Measure)] H28Burr08 = 15224,
    }
}
