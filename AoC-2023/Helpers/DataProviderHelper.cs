using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Helpers
{
    public static class DataProviderHelper
    {
        public static string defaultBasePath = @"../net6.0/Data/";

        public static List<string> GetTextLines(string fileName)
        {
            return GetTextLines(fileName, defaultBasePath);
        }

        public static List<string> GetTextLines(string fileName, string basePath)
        {
            string[] textLines = null;

            try
            {
                textLines = File.ReadAllLines(basePath + fileName);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't open the file!");
            }

            return textLines.ToList();
        }

        public static string GetText(string fileName)
        {
            return GetText(fileName, defaultBasePath);
        }

        public static string GetText(string fileName, string basePath)
        {
            string textLines = null;

            try
            {
                textLines = File.ReadAllText(basePath + fileName);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't open the file!");
            }

            return textLines;
        }
    }
}
