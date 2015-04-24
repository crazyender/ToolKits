using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ender.Utils;
using System.Numerics;
namespace Euler
{
    partial class Program
    {
        
        public void test()
        {
            var l = new List<int>() { 5, 1, 1, 2, 2, 3, 4, 5, 5, 5, 5, 5, 5 };

            //l = l.Filter(v => v.IsPrime());

            //var p = l.Combinations(2).Filter(v => Math.Abs(v[0] - v[1]) == 3330);
            //var pp = p.Combinations(2)
            //    .Filter(v =>
            //    Math.Abs(v[0][0] - v[0][1]) == Math.Abs(v[1][0] - v[1][1]))
            //    .Filter(v => v[0][1] == v[1][0]);


            //var i = 0;
            //foreach( var v in pp)
            //{
            //    Console.Write("[({0} {1}), ({2}, {3})] ", v[0][0], v[0][1], v[1][0], v[1][1]);
            //    if ((i + 1) % 4 == 0)
            //        Console.WriteLine();
            //    i++;
            //}

            //var i = 1223534;
            //var cl = i.ToCharList(16);
            //var t = l.AsEnumerable().GroupBy(ii => ii).ToArray();
            //var merged = l.Group().ToArray() ;
            Fraction f = new Fraction(66, 78);
            Console.WriteLine("{0}/{1} == {2}/{3}", 
                f.N, f.D,
                f.Original.N, f.Original.D);

            f = new Fraction(0.33333333333333);
            Console.WriteLine("{0}/{1} == {2}",
                f.N, f.D,
                f.Decimals);

            var a = new Fraction(1, 2);
            var b = 0.6;
            var c = a + b;
            var d = new Fraction(3, 5);
            Console.WriteLine("{0} + {1} == {2}", a.ToString(), b.ToString(), c.ToString());

            c = a - b;
            Console.WriteLine("{0} - {1} == {2}", a.ToString(), b.ToString(), c.ToString());

            c = a * b;
            Console.WriteLine("{0} * {1} == {2}", a.ToString(), b.ToString(), c.ToString());

            c = a / b;
            Console.WriteLine("{0} / {1} == {2}", a.ToString(), b.ToString(), c.ToString());

            Console.WriteLine("test {0} == {1} : {2}", b.ToString(), d.ToString(), (b == d));


            BigInteger i = new BigInteger(10);
            BigInteger p = new BigInteger(3);
            Console.WriteLine("{0} ^ {1} = {2}", i, p, i.Power(p));



            i = new BigInteger(2);
            p = new BigInteger(4);
            Console.WriteLine("mode power {0}", BigInteger.ModPow(i, i, 3));
        }

        public void CodedTriangleNumbers()
        {
            string content = ScriptTools.Cat(@"C:\euler\p042_words.txt");
            var words = content.Replace("\"", "").Split(new char[]{','});
            var numbers = words.Map<string, int>(word => word.Map<char, int>(c => c - 'A' + 1).Sum());
            var triangles = Itertools.Range(1, 1000).Map<int, int>(i => i * (i + 1) / 2);
            var result = 0;
            foreach(var number in numbers)
            {
                if (triangles.Contains(number))
                    result++;
            }
            Console.WriteLine("result is {0}", result);

        }


        public void TriangularPentagonalAndHexagonal()
        {
            var triangulars = Itertools.Range(1, (long)100000).Map<long, long>(n => n * (n + 1) / 2);
            var pentagonals = Itertools.Range(1, (long)100000).Map<long, long>(n => n * (3 * n - 1) / 2);
            var hexagonals = Itertools.Range(1, (long)100000).Map<long, long>(n => n * (2 * n - 1));

            var zipped = triangulars
                .Concat(pentagonals)
                .Concat(hexagonals)
                .Group()
                .Filter(g => g.Length == 3);
            foreach(var item in zipped)
            {
                Console.WriteLine("{0}", item[0]);
            }
        }


        public void PermutedMultiples()
        {
            DateTime begin = DateTime.Now;
            for (var numSize = 1; numSize <= 10; numSize++)
            {
                long min = (long)(Math.Pow(10, numSize - 1));
                long max = (long)((10.0 / 6.0) * Math.Pow(10, numSize-1));
                for(long i = min; i <= max; i++)
                {
                    var c0 = i.GroupSize();
                    var c2 = (i * 2).GroupSize();

                    if (c0 != c2)
                        continue;
                    var c3 = (i * 3).GroupSize();
                    if (c0 != c3)
                        continue;
                    var c4 = (i * 4).GroupSize();
                    if (c0 != c4)
                        continue;
                    var c5 = (i * 5).GroupSize();
                    if (c0 != c5)
                        continue;
                    var c6 = (i * 6).GroupSize();
                    if (c0 != c6)
                        continue;

                    var x = i.ToCharList();
                    var x2 = (i * 2).ToCharList();
                    var x3 = (i * 3).ToCharList();
                    var x4 = (i * 4).ToCharList();
                    var x5 = (i * 5).ToCharList();
                    var x6 = (i * 6).ToCharList();

                    c0 = x.GroupSize();
                    var total = x
                        .Concat(x2);
                    if( total.GroupSize() != c0)
                        continue;
                    total = total
                        .Concat(x3);
                    if (total.GroupSize() != c0)
                        continue;
                    total = total
                        .Concat(x4);
                    if (total.GroupSize() != c0)
                        continue;
                    total = total
                        .Concat(x5);
                    if (total.GroupSize() != c0)
                        continue;
                    total = total
                        .Concat(x6);
                    if (total.GroupSize() != c0)
                        continue;

                    var time = (DateTime.Now - begin);
                    Console.WriteLine("[{6}.{7}]: {0} {1} {2} {3} {4} {5}",
                        i, i * 2, i * 3, i * 4, i * 5, i * 6,
                        time.TotalSeconds, time.Milliseconds);
                }
            }
        }

        public void CountingFractionsInRange()
        {
            //
            // beautiful but damn slow version
            //
            // var list = Itertools.Range(2, 10);
            // var numbers = list.Map<int, int[]>(x => Itertools.Range(1, x).ToArray());
            // var fractions = numbers
            //     .Map<int[], IEnumerable<Fraction>>(x =>
            //         x.Map<int, Fraction>(n => new Fraction(n, x.Length + 1))
            //     )
            //     .Reduce(new List<Fraction>().AsEnumerable(),
            //         (x, y) => x.Concat(y))
            //     .NoDup()
            //     .OrderBy(x => x.Decimals);
            // var start = new Fraction(1, 3);
            // var end = new Fraction(1, 2);
            // var count = 0;
            // foreach(var v in fractions)
            // {
            //     if ((v > start) && (v < end))
            //     {
            //         count++;
            //     }
            // }
            //Console.WriteLine("{0}", count);

            //
            // so much faster
            //
            List<Fraction> result = new List<Fraction>();
            for (var i = 2; i <= 12000; i++)
            {
                int min = (i / 3) + 1;
                int max = (i % 2 == 0) ? (i / 2 ) : (i / 2 + 1);
                for (var j = min; j < max; j++)
                {
                    result.Add(new Fraction(j, i));
                }
            }
            var fractions = result.NoDup();
            Console.WriteLine("{0}", fractions.Count());
        }

        public void PowerfulDigitCounts()
        {
            int n = 1;
            long count = 0;
            while(true)
            {
                // n digit number
                // test 10^(n-1) <= 2^n < 10^n
                var max = BigInteger.Pow(10, n);
                var min = BigInteger.Pow(10, n-1);
                var power_of_3 = BigInteger.Pow(3, n);
                if (power_of_3 >= max)
                    break;

                BigInteger idx = 1;
                var found = false;
                while(true)
                {
                    var r = BigInteger.Pow(idx, n);
                    if (r < min)
                    {
                        idx++;
                        continue;
                    }
                    else if (r < max)
                    {
                        // found
                        count++;
                        idx++;
                        found = true;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                if (!found)
                    break;
                n++;
            }
            Console.WriteLine("{0}", count);
        }

        public void LargeNonMersennePrime()
        {
            BigInteger b = BigInteger.Pow(2, 7830457);
            b *= 28433;
            b += 1;
            var result = b % 10000000000;
            Console.WriteLine("{0}", result);
        }

        public void LargestExponential()
        {
            string content = ScriptTools.Cat(@"c:\euler\p099_base_exp.txt");
            var original = content.Lines().ToArray();
            var lines = content.Lines()
                .Map<string, double>(
                    str =>
                    {
                        var numbers = str.Split(new char[] { ',' });
                        var a = int.Parse(numbers[0]);
                        var b = int.Parse(numbers[1]);
                        return Math.Log10((double)a) * (double)b;
                    }
                );
            double max = 0.0;
            int index = 1;
            int max_idx = 1;
            foreach (var line in lines)
            {
                if (max < line)
                {
                    max = line;
                    max_idx = index;
                }
                index++;
            }
            Console.WriteLine("Max line {0}: {1}", max_idx, original[max_idx]);
        }

        public void PrimePowerTriples()
        {
            // 7072
            // 369
            // 85
            var squres = Itertools
                .Range(2, 7072)
                .Filter(x => x.IsPrime())
                .Map<int, int>(x => x*x);
            var cubes = Itertools
                .Range(2, 369)
                .Filter(x => x.IsPrime())
                .Map<int, int>(x => x * x * x);
            var fourth = Itertools
                .Range(2, 85)
                .Filter(x => x.IsPrime())
                .Map<int, int>(x => x * x * x * x);
            var numbers = new HashSet<int>();
            foreach(var s in squres)
            {
                foreach(var c in cubes)
                {
                    foreach( var f in fourth)
                    {
                        var p = s + c + f;
                        if (p > 50000000)
                            continue;
                        if (!numbers.Contains(p))
                            numbers.Add(p);
                    }
                }
            }


            Console.WriteLine("{0}", numbers.Count());

        }

        public void OptimumPolynomial()
        {
            // U(n) = 1 − n + n^2 − n^3 + n^4 − n^5 + n^6 − n^7 + n^8 − n^9 + n^10

            var num = 1000000;
            var primes = Itertools.GenPrime().SubList(num).ElementAt(num - 1);

            //var correct = Itertools.Range(0, 12)
            //    .Map<int, BigInteger>(x =>
            //        {
            //            BigInteger bx = new BigInteger(x);
            //            BigInteger i = 1;
            //            i -= x;
            //            i += BigInteger.Pow(bx, 2);
            //            i -= BigInteger.Pow(bx, 3);
            //            i += BigInteger.Pow(bx, 4);
            //            i -= BigInteger.Pow(bx, 5);
            //            i += BigInteger.Pow(bx, 6);
            //            i -= BigInteger.Pow(bx, 7);
            //            i += BigInteger.Pow(bx, 8);
            //            i -= BigInteger.Pow(bx, 9);
            //            i += BigInteger.Pow(bx, 10);
            //            return i;
            //        }).ToArray();

        }
    }
}
