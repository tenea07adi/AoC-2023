using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Helpers
{
    public static class StringHelper
    {
        public static List<string> ExplodeStringToLines(string data)
        {
            return data.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ).ToList();
        }

        public static List<string> ExplodeStringByDoubleNewLine(string data)
        {
            return data.Split(
                new string[] { "\r\n\r\n", "\r\r", "\n\n" },
                StringSplitOptions.None
            ).ToList();
        }
    }
}
