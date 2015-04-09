using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ender.Utils
{
    public static class Algebra
    {

        public static bool IsPrime(this short num)
        {
            return IsPrime((long)num);
        }

        public static bool IsPrime(this int num)
        {
            return IsPrime((long)num);
        }

        public static bool IsPrime(this long num)
        {
            if (num < 2)
                return false;

            if (num == 2 || num == 3)
                return true;

            var p = (int)Math.Sqrt((double)num) + 1;
            for (var i = 2; i <= p; i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }

        private static char itoc(long i)
        {
            switch(i)
            {
                case 0:
                    return '0';
                case 1:
                    return '1';
                case 2:
                    return '2';
                case 3:
                    return '3';
                case 4:
                    return '4';
                case 5:
                    return '5';
                case 6:
                    return '6';
                case 7:
                    return '7';
                case 8:
                    return '8';
                case 9:
                    return '9';
                case 10:
                    return 'a';
                case 11:
                    return 'b';
                case 12:
                    return 'c';
                case 13:
                    return 'd';
                case 14:
                    return 'e';
                case 15:
                    return 'f';
            }
            return '?';
        }

        public static IEnumerable<char> ToCharList(this short i, int _base = 10)
        {
            return ToCharList((long)i, _base);
        }

        public static IEnumerable<char> ToCharList(this int i, int _base = 10)
        {
            return ToCharList((long)i, _base);
        }

        public static IEnumerable<char> ToCharList(this long i, int _base = 10)
        {
            if (_base > 16)
                throw new Exception();

            while (i != 0)
            {
                var c = i % _base;
                yield return itoc(c);
                i /= _base;
            }
        }

        public static short Gcd(short a, short b)
        {
            return (short)Gcd((long)a, (long)b);
        }

        public static int Gcd(int a, int b)
        {
            return (int)Gcd((long)a, (long)b);
        }

        public static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        
    }
}
