using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day9 : BasePuzzleLogic
    {
        public Day9() : base(9)
        {
            
        }

        protected override string LogicPart1(string data)
        {
            int[,] reports = GetReports(data);

            int[] reportsExtrapolations = ExtrapolateReports(reports, false);

            int res = 0;

            for(int i = 0; i < reportsExtrapolations.Length; i++)
            {
                res += reportsExtrapolations[i];
            }

            return res.ToString();
        }

        protected override string LogicPart2(string data)
        {
            int[,] reports = GetReports(data);

            int[] reportsExtrapolations = ExtrapolateReports(reports, true);

            int res = 0;

            for (int i = 0; i < reportsExtrapolations.Length; i++)
            {
                res += reportsExtrapolations[i];
            }

            return res.ToString();
        }

        private int[] ExtrapolateReports(int[,] reports, bool fromLeft)
        {
            int[] results = new int[reports.GetLength(0)];

            for (int i = 0; i < reports.GetLength(0); i++) {
                int[] reportLine = new int[reports.GetLength(1)];
                
                for(int j = 0; j < reports.GetLength(1); j++)
                {
                    reportLine[j] = reports[i, j];
                }
                results[i] = ExtrapolateReport(reportLine, fromLeft);
            }

            return results;
        }

        private int ExtrapolateReport(int[] reportLine, bool fromLeft)
        {
            int[] newreportLine = new int[reportLine.Length-1];

            bool allZero = true;

            for(int i = 0;i < reportLine.Length-1;i++)
            {
                newreportLine[i] =  reportLine[i + 1] - reportLine[i];

                if (newreportLine[i] != 0)
                {
                    allZero = false;
                }
            }

            if (allZero)
            {
                if (fromLeft)
                {
                    return reportLine[0];
                }
                else
                {
                    return reportLine[reportLine.Length - 1];
                }
            }
            else
            {
                if (fromLeft)
                {
                    return reportLine[0] - ExtrapolateReport(newreportLine, fromLeft);
                }
                else
                {
                    return reportLine[reportLine.Length - 1] + ExtrapolateReport(newreportLine, fromLeft);
                }
            }
        }

        private int[,] GetReports(string data)
        {
            List<string> lines = StringHelper.ExplodeStringToLines(data);

            int[,] res = new int[lines.Count, 100];
            int maxElm = 0;

            for(int i = 0; i < lines.Count; i++)
            {
                var elements = lines[i].Split(" ");
                maxElm = maxElm > elements.Length ? maxElm : elements.Length;

                for ( int j = 0; j< elements.Length; j++ )
                {
                    res[i, j] = Int32.Parse(elements[j]);
                }
            }

            res = MatrixHelper.ResizeMatrix(res, maxElm, res.GetLength(0));

            return res;
        }
    }
}
