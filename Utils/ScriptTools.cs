using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ender.Utils
{
    public class ScriptTools
    {
        public static string Cat(string file)
        {
            return Cat(file, Encoding.UTF8);
        }

        public static string Cat(string file, Encoding encoding)
        {
            if (!File.Exists(file))
                return string.Empty;

            TextReader reader = new StreamReader(file);
            var context = reader.ReadToEnd();
            reader.Close();
            return context;
        }
    }
}
