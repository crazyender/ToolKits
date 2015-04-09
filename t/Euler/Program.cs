using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Euler
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2 || args[0] != "-f")
            {
                Console.WriteLine("euler -f name");
                return;
            }

            string name = args[1];
            var t = Type.GetType("Euler.Program");
            var method = t.GetMethod(name);
            var obj = t.Assembly.CreateInstance("Euler.Program");
            method.Invoke(obj, new object[] { });
        }
    }
}
