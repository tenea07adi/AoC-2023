using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day1 : BasePuzzleLogic
    {
        public Day1() : base(1)
        {
        }

        protected override string LogicPart1(string data)
        {
            List<string> lines = StringHelper.ExplodeStringToLines(data);

            return GetSumFromLines(lines).ToString();
        }

        protected override string LogicPart2(string data)
        {
            List<string> lines = StringHelper.ExplodeStringToLines(data);

            lines = ConvertDigistsFromString(lines);

            return GetSumFromLines(lines).ToString();
        }

        private List<string> ConvertDigistsFromString(List<string> lines)
        {
            List<string> digitsAsText = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            for (int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < digitsAsText.Count; j++)
                {
                    lines[i] = lines[i].Replace(digitsAsText[j],
                        digitsAsText[j][0] +
                        j.ToString() +
                        digitsAsText[j][digitsAsText[j].Length - 1]
                        );
                }
            }

            return lines;
        }

        private int GetSumFromLines(List<string> lines)
        {
            int result = 0;

            foreach (string line in lines)
            {
                string charNumber = "";

                for (int i = 0; i < line.Length - 1; i++)
                {
                    if (line[i] >= '0' && line[i] <= '9')
                    {
                        charNumber += line[i];
                        break;
                    }
                }

                for (int i = line.Length - 1; i > 0; i--)
                {
                    if (line[i] >= '0' && line[i] <= '9')
                    {
                        charNumber += line[i];
                        break;
                    }
                }

                if (charNumber.Length < 2)
                {
                    if (charNumber.Length <= 0)
                    {
                        throw new Exception("Invalid line!");
                    }
                    else
                    {
                        charNumber += charNumber;
                    }
                }

                try
                {
                    result += Convert.ToInt32(charNumber);
                }
                catch
                {
                    throw new Exception("Can't parse the number!");
                }
            }
            return result;
        }
    }
}
