using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day13 : BasePuzzleLogic
    {
        public Day13() : base(13)
        {
            
        }

        protected override string LogicPart1(string data)
        {
            var groundData = GetGroundData(data);

            int res = 0;

            foreach (var ground in groundData) {
                List<string> rowsAsStringLines = GetGroundRowsAsStringLines(ground);
                List<string> colsAsStringLines = GetGroundColsAsStringLines(ground);

                int r = IdentifyPatternStartPoint(rowsAsStringLines);
                int c = IdentifyPatternStartPoint(colsAsStringLines);

                res += r > c ? r * 100 : c;
            }

            return res.ToString();
        }

        protected override string LogicPart2(string data)
        {
            throw new NotImplementedException();
        }

        private int IdentifyPatternStartPoint(List<string> lines)
        {
            int patternStartPoint = 0;

            int patternMidLine1 = -1;
            int patternMidLine2 = -1;

            for(int i = 1; i < lines.Count; i++)
            {
                if (lines[i] == lines[i - 1])
                {
                    patternMidLine1 = i - 1;
                    patternMidLine2 = i;
                    break;
                }
            }

            if (patternMidLine1 != -1 && patternMidLine2 != -1)
            {
                int count = 0;
                bool stillPattern = true;

                while (stillPattern)
                {
                    count++;

                    if (patternMidLine1 - count < 0 || patternMidLine2 + count >= lines.Count)
                    {
                        stillPattern = false;
                        break;
                    }

                    if (lines[patternMidLine1 - count] != lines[patternMidLine2 + count])
                    {
                        stillPattern = false;
                        break;
                    }
                }


                patternStartPoint = patternMidLine1 + 1;
            }
            else
            {
                return 0;
            }

            return patternStartPoint;
        }

        private List<string> GetGroundRowsAsStringLines(string[,] ground)
        {
            List<string> lines = new List<string>();

            for(int i = 0;  i < ground.GetLength(0); i++) 
            {
                string newLine = "";

                for (int j = 0; j < ground.GetLength(1); j++)
                {
                    newLine += ground[i, j];
                }

                lines.Add(newLine);
            }

            return lines;
        }

        private List<string> GetGroundColsAsStringLines(string[,] ground)
        {
            List<string> lines = new List<string>();

            for (int j = 0; j < ground.GetLength(1); j++)
            {
                string newLine = "";

                for (int i = 0; i < ground.GetLength(0); i++)
                {
                    newLine += ground[i, j];
                }

                lines.Add(newLine);
            }

            return lines;
        }

        private List<string[,]> GetGroundData(string data)
        {
            List<string[,]> groundParts = new List<string[,]>();

            foreach (string part in StringHelper.ExplodeStringByDoubleNewLine(data))
            {
                groundParts.Add(MatrixHelper.StringToMatrix(part));
            }

            return groundParts;
        }
    }
}
