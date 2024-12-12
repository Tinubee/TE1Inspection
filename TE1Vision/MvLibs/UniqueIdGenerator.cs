using System;
using System.Linq;

namespace MvLibs
{
    public static class UniqueIdGenerator
    {
        public const String AlphaChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const String BaseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";//abcdefghijklmnopqrstuvwxyz
        public static readonly Random Random = new Random();

        // 시간 기반의 짧은 유니크 ID 생성
        public static String TimeBasedId(Int32 length = 13)
        {
            // 자릿수를 10자리로 맞추고 중복을 피하기 위함
            DateTime start = new DateTime(Random.Next(2014, 2022), Random.Next(1, 13), Random.Next(1, 28), Random.Next(0, 24), Random.Next(0, 60), Random.Next(0, 60), Random.Next(0, 999));
            String timedId = ToBase(DateTime.UtcNow.Ticks - start.Ticks);
            if (timedId.Length >= length) return timedId;
            return RandomId(length - timedId.Length, AlphaChars) + timedId;
        }
        // 랜덤 문자열 기반의 유니크 ID 생성
        public static String RandomId(Int32 length = 8, String chars = BaseChars) =>
            new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());

        public static String ToBase(Int64 value, String chars = BaseChars)
        {
            if (value == 0) return RandomId(10, chars);
            Int32 length = chars.Length;
            String result = "";
            while (value > 0)
            {
                result = BaseChars[(Int32)(value % length)] + result;
                value /= length;
            }
            return result;
        }
    }
}
