using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ender.Utils
{

    public class Fraction
    {
        private long original_n;
        private long original_d;
        private long simplified_n;
        private long simplified_d;

        public Fraction Original
        {
            get
            {
                return new Fraction(original_n, original_d);
            }
        }

        public long N 
        {  
            get 
            {
                return simplified_n; 
            } 
        }

        public long D
        {
            get
            {
                return simplified_d;
            }
        }

        public double Decimals { get; private set; }



        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException();

            original_n = numerator;
            original_d = denominator;
            Simplify();
            Decimals = (double)numerator / (double)denominator;
        }

        public Fraction(double decimals)
        {
            original_n = (long)(decimals * 1000000000);
            original_d = 1000000000;
            Simplify();
            original_n = simplified_n;
            original_d = simplified_d;
            Decimals = decimals;
        }


        public Fraction(Fraction number)
        {
            original_n = number.original_n;
            original_d = number.original_d;
            Simplify();
            Decimals = (double)N / (double)D;
        }

        private void Simplify()
        {
            bool neg = false;
            if ((original_n > 0 && original_d < 0) |
                (original_n < 0 && original_d > 0))
            {
                neg = true;
            }
            var abs_n = Math.Abs(original_n);
            var abs_d = Math.Abs(original_d);

            var gcd = Algebra.Gcd(abs_n, abs_d);
            simplified_n = abs_n / gcd;
            simplified_d = abs_d / gcd;
            if (neg)
                simplified_n *= -1;
        }

        public static Fraction operator+(Fraction a, Fraction b)
        {
            long n = a.N * b.D +
                a.D * b.N;
            long d = a.D * b.D;
            Fraction f = new Fraction(n, d);
            f.original_n = f.simplified_n;
            f.original_d = f.simplified_d;
            return f;
        }

        public static Fraction operator +(Fraction a, double c)
        {
            Fraction b = new Fraction(c);
            return (a + b);
        }

        public static Fraction operator +(double c, Fraction a)
        {
            Fraction b = new Fraction(c);
            return (a + b);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            long n = a.N * b.D -
                a.D * b.N;
            long d = a.D * b.D;
            Fraction f = new Fraction(n, d);
            f.original_n = f.simplified_n;
            f.original_d = f.simplified_d;
            return f;
        }

        public static Fraction operator -(Fraction a, double c)
        {
            Fraction b = new Fraction(c);
            return (a - b);
        }

        public static Fraction operator -(double c, Fraction a)
        {
            Fraction b = new Fraction(c);
            return (a - b);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            long n = a.N * b.N;
            long d = a.D * b.D;
            Fraction f = new Fraction(n, d);
            f.original_n = f.simplified_n;
            f.original_d = f.simplified_d;
            return f;
        }

        public static Fraction operator *(Fraction a, double c)
        {
            Fraction b = new Fraction(c);
            return (a * b);
        }

        public static Fraction operator *(double c, Fraction a)
        {
            Fraction b = new Fraction(c);
            return (a * b);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            long n = a.N * b.D;
            long d = a.D * b.N;
            Fraction f = new Fraction(n, d);
            f.original_n = f.simplified_n;
            f.original_d = f.simplified_d;
            return f;
        }

        public static Fraction operator /(Fraction a, double c)
        {
            Fraction b = new Fraction(c);
            return (a / b);
        }

        public static Fraction operator /(double c, Fraction a)
        {
            Fraction b = new Fraction(c);
            return (a / b);
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            return (a.N == b.N && a.D == b.D);
        }

        public static bool operator ==(Fraction a, double c)
        {
            Fraction b = new Fraction(c);
            return (a == b);
        }

        public static bool operator ==(double c, Fraction a)
        {
            Fraction b = new Fraction(c);
            return (a == b);
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return (a.N != b.N || a.D != b.D);
        }

        public static bool operator !=(Fraction a, double c)
        {
            Fraction b = new Fraction(c);
            return (a != b);
        }

        public static bool operator !=(double c, Fraction a)
        {
            Fraction b = new Fraction(c);
            return (a != b);
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return (a.N*b.D > b.N*a.D);
        }

        public static bool operator >(Fraction a, double c)
        {
            Fraction b = new Fraction(c);
            return (a > b);
        }

        public static bool operator >(double c, Fraction a)
        {
            Fraction b = new Fraction(c);
            return (a < b);
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            return (a.N * b.D < b.N * a.D);
        }

        public static bool operator <(Fraction a, double c)
        {
            Fraction b = new Fraction(c);
            return (a < b);
        }

        public static bool operator <(double c, Fraction a)
        {
            Fraction b = new Fraction(c);
            return (a > b);
        }


        public override string ToString()
        {
            return string.Format("{0}/{1}", N, D);
        }

        public override bool Equals(object obj)
        {
            if (obj is Fraction)
            {
                Fraction b = obj as Fraction;
                return (this == b);
            }
            else if (obj is double)
            {
                double b = (double)obj;
                return (this == b);
            }

            throw new Exception();
        }

        public override int GetHashCode()
        {
            return this.Decimals.GetHashCode();
        }
    }
}
