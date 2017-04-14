using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextInpact
{
    public static class JavaExtensions
    {
        public static string[] Split(this string str, string splitter)
        {
            return str.Split(new[] { splitter }, StringSplitOptions.None);
        }

        public static string JavaSubString(this string s, int start, int end)
        {
            return s.Substring(start, end - start);
        }
    }
}
