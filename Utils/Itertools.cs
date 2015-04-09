using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ender.Utils
{
    public static class Itertools
    {
        private static IEnumerable<T[]> Combinations_Helper<T>(List<T> l, int start, int num, List<T> result)
        {
            if (num == 1)
            {
                for (int i = start; i < l.Count(); i++)
                {
                    result.Add(l[i]);
                    yield return result.ToArray();
                    result.RemoveAt(result.Count()-1);
                }
            }
            else
            {
                for (int i = start; i <= (l.Count() - num); i++)
                {
                    result.Add(l[i]);
                    var e = Combinations_Helper<T>(l, i + 1, num - 1, result);
                    foreach (var p in e)
                    {
                        yield return p;
                    }
                    result.RemoveAt(result.Count() - 1);
                }

            }
        }

        private static IEnumerable<T[]> Permutations_Helper<T>(List<T> l, int start, int num, List<T> result)
        {
            if (num == 1)
            {
                for (int i = 0; i < l.Count(); i++)
                {
                    if (i == (start - 1))
                        continue;

                    result.Add(l[i]);
                    yield return result.ToArray();
                    result.RemoveAt(result.Count() - 1);
                }
            }
            else
            {
                for (int i = 0; i <= (l.Count() - num + 1); i++)
                {

                    result.Add(l[i]);
                    var e = Permutations_Helper<T>(l, i + 1, num - 1, result);
                    foreach (var p in e)
                    {
                        yield return p;
                    }
                    result.RemoveAt(result.Count() - 1);
                }

            }
        }

        private static IEnumerable<T[]> Variants_Helper<T>(List<T> l, int start, int num, List<T> result)
        {
            if (num == 1)
            {
                for (int i = 0; i < l.Count(); i++)
                {
                    result.Add(l[i]);
                    yield return result.ToArray();
                    result.RemoveAt(result.Count() - 1);
                }
            }
            else
            {
                for (int i = 0; i <= (l.Count() - num + 1); i++)
                {

                    result.Add(l[i]);
                    var e = Variants_Helper<T>(l, i + 1, num - 1, result);
                    foreach (var p in e)
                    {
                        yield return p;
                    }
                    result.RemoveAt(result.Count() - 1);
                }

            }
        }

        public static IEnumerable<T[]> Combinations<T>(this IEnumerable<T> l, int num)
        {
            if (num < 1)
                return null;

            return Combinations_Helper<T>(l.ToList(), 0, num, new List<T>());
        }

        public static IEnumerable<T[]> Permutations<T>(this IEnumerable<T> l, int num)
        {
            if (num < 1)
                return null;

            return Permutations_Helper<T>(l.ToList(), 0, num, new List<T>());
        }


        public static IEnumerable<T[]> Variants<T>(this IEnumerable<T> l, int num)
        {
            if (num < 1)
                return null;

            return Variants_Helper<T>(l.ToList(), 0, num, new List<T>());
        }

        public static IEnumerable<int> Range(int start, int end)
        {
            for(var i = start; i < end; i++)
            {
                yield return i;
            }
        }

        public static IEnumerable<long> Range(long start, long end)
        {
            for (var i = start; i < end; i++)
            {
                yield return i;
            }
        }

        public static IEnumerable<OUT> Map<IN, OUT>(this IEnumerable<IN> l, Func<IN, OUT> func)
        {
            foreach(var v in l)
            {
                yield return func(v);
            }
        }

        public static T Reduce<T>(this IEnumerable<T> l, T start, Func<T, T, T> func)
        {
            T result = start;
            foreach(var v in l)
            {
                result = func(result, v);
            }
            return result;
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> l, Func<T, bool> func)
        {
            foreach(var v in l)
            {
                if (func(v))
                    yield return v;
            }
        }

        public static IEnumerable<T[]> Group<T>(this IEnumerable<T> l)
        {
            IDictionary<T, int> dict = new Dictionary<T, int>();
            foreach(var v in l)
            {
                if (dict.ContainsKey(v))
                    dict[v]++;
                else
                    dict.Add(v, 1);
            }

            foreach(var pair in dict)
            {
                T[] r= new T[pair.Value];
                for (var i = 0; i < pair.Value; i++)
                    r[i] = pair.Key;
                yield return r;
            }
        }

        public static IEnumerable<T> NoDup<T>(this IEnumerable<T> l)
        {
            HashSet<T> dict = new HashSet<T>();
            foreach (var v in l)
            {
                if (!dict.Contains(v))
                    dict.Add(v);
            }

            return dict.AsEnumerable();
        }

        public static int GroupSize<T>(this IEnumerable<T> l)
        {
            HashSet<T> dict = new HashSet<T>();
            foreach (var v in l)
            {
                //if (!dict.Contains(v))
                    dict.Add(v);
            }

            return dict.Count();
        }

        public static int GroupSize(this short i, int _base = 10)
        {
            return GroupSize((long)i, _base);
        }

        public static int GroupSize(this int i, int _base = 10)
        {
            return GroupSize((long)i, _base);
        }

        public static int GroupSize(this long i, int _base = 10)
        {
            if (_base > 16)
                throw new Exception();

            int c0, c1, c2, c3, c4, c5, c6, c7, c8, c9, ca, cb, cc, cd, ce, cf;
            c0 = c1 = c2 = c3 = c4 = c5 = c6 = c7 = c8 = c9 
                = ca = cb = cc = cd = ce = cf = 0;
            while (i != 0)
            {
                var c = i % _base;
                switch(c)
                {
                    case 0:
                        c0 = 1;
                        break;
                    case 1:
                        c1 = 1;
                        break;
                    case 2:
                        c2 = 1;
                        break;
                    case 3:
                        c3 = 1;
                        break;
                    case 4:
                        c4 = 1;
                        break;
                    case 5:
                        c5 = 1;
                        break;
                    case 6:
                        c6 = 1;
                        break;
                    case 7:
                        c7 = 1;
                        break;
                    case 8:
                        c8 = 1;
                        break;
                    case 9:
                        c9 = 1;
                        break;
                    case 10:
                        ca = 1;
                        break;
                    case 11:
                        cb = 1;
                        break;
                    case 12:
                        cc = 1;
                        break;
                    case 13:
                        cd = 1;
                        break;
                    case 14:
                        ce = 1;
                        break;
                    case 15:
                        cf = 1;
                        break;
                }
                i /= _base;
            }
            return c0 + c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9 + 
                ca + cb + cc + cd + ce + cf;
        }
        
    }
}
