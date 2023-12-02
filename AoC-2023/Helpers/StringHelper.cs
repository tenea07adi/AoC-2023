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
            return data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }
    }
}
