using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AoC_2023.Helpers.MatrixHelper;

namespace AoC_2023.Logic
{
    public class Day11 : BasePuzzleLogic
    {
        public Day11() : base(11)
        {

        }

        protected override string LogicPart1(string data)
        {
            string[,] space = GetSpace(data);

            int[,] galaxiesLocations = GetGalaxies(space);

            int res = 0;

            int[] emptyRows = CountEmptyRows(space);
            int[] emptyCols = CountEmptyCols(space);

            for (int i = 0; i < galaxiesLocations.GetLength(0); i++)
            {
                for (int j = i + 1; j < galaxiesLocations.GetLength(0); j++)
                {
                    int[] p1 = new int[] { galaxiesLocations[i, 0], galaxiesLocations[i, 1] };
                    int[] p2 = new int[] { galaxiesLocations[j, 0], galaxiesLocations[j, 1] };
                    res += GetCostBetweenTwoPoints(p1, p2) + EmptyLinesInside(emptyRows, emptyCols, p1, p2);
                }
            }

            return res.ToString();
        }

        protected override string LogicPart2(string data)
        {
            string[,] space = GetSpace(data);

            int[,] galaxiesLocations = GetGalaxies(space);

            long res = 0;

            int[] emptyRows = CountEmptyRows(space);
            int[] emptyCols = CountEmptyCols(space);

            for (int i = 0; i < galaxiesLocations.GetLength(0); i++)
            {
                for (int j = i + 1; j < galaxiesLocations.GetLength(0); j++)
                {
                    int[] p1 = new int[] { galaxiesLocations[i, 0], galaxiesLocations[i, 1] };
                    int[] p2 = new int[] { galaxiesLocations[j, 0], galaxiesLocations[j, 1] };
                    res += GetCostBetweenTwoPoints(p1, p2) + (EmptyLinesInside(emptyRows, emptyCols, p1, p2) * 999999);
                }
            }

            return res.ToString();
        }

        private int[,] GetGalaxies(string[,] space)
        {
            int[,] galaxiesLocations = new int[1000, 2];

            int count = 0;

            MatrixHelper.MatrixElmOperation(space, (space, i, j) =>
            {
                if (space[i, j] == "#")
                {
                    galaxiesLocations[count, 0] = i;
                    galaxiesLocations[count, 1] = j;

                    count++;
                }
            });

            return MatrixHelper.ResizeMatrix(galaxiesLocations, 2, count);
        }

        private int EmptyLinesInside(int[] rows, int[] cols, int[] point1, int[] point2)
        {
            int r = 0;
            int c = 0;

            int maxR = 0;
            int minR = 0;

            int maxC = 0;
            int minC = 0;

            if (point1[0] > point2[0])
            {
                maxR = point1[0];
                minR = point2[0];
            }
            else
            {
                maxR = point2[0];
                minR = point1[0];
            }

            if (point1[1] > point2[1])
            {
                maxC = point1[1];
                minC = point2[1];
            }
            else
            {
                maxC = point2[1];
                minC = point1[1];
            }

            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i] > minR && rows[i] < maxR)
                {
                    r++;
                }
            }

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] > minC && cols[i] < maxC)
                {
                    c++;
                }
            }

            return r + c;
        }

        private int GetCostBetweenTwoPoints(int[] point1, int[] point2)
        {
            int iDiv = point1[0] >= point2[0] ? point1[0] - point2[0] : point2[0] - point1[0];
            int jDiv = point1[1] >= point2[1] ? point1[1] - point2[1] : point2[1] - point1[1];

            return iDiv + jDiv;
        }

        // line one rows
        // line two cols
        private int[] CountEmptyRows(string[,] space)
        {
            int[] empty = new int[100];

            int rowsCount = space.GetLength(0);
            int colsCount = space.GetLength(1);

            int emptyRowsCount = 0;

            for (int i = 0; i < rowsCount; i++)
            {
                bool justEmptySpaces = true;
                for (int j = 0; j < colsCount; j++)
                {
                    if (space[i, j] != ".")
                    {
                        justEmptySpaces = false;
                        break;
                    }
                }
                if (justEmptySpaces)
                {
                    empty[emptyRowsCount] = i;
                    emptyRowsCount++;
                }
            }

            return ArrayHelper.ResizeArray(empty, emptyRowsCount);
        }

        // line one rows
        // line two cols
        private int[] CountEmptyCols(string[,] space)
        {
            int[] empty = new int[100];

            int rowsCount = space.GetLength(0);
            int colsCount = space.GetLength(1);

            int emptyColsCount = 0;

            for (int j = 0; j < colsCount; j++)
            {
                bool justEmptySpaces = true;
                for (int i = 0; i < rowsCount; i++)
                {
                    if (space[i, j] != ".")
                    {
                        justEmptySpaces = false;
                        break;
                    }
                }
                if (justEmptySpaces)
                {
                    empty[emptyColsCount] = j;
                    emptyColsCount++;
                }
            }

            return ArrayHelper.ResizeArray(empty, emptyColsCount);
        }

        private string[,] GetSpace(string data)
        {
            return MatrixHelper.StringToMatrix(data);
        }

    }
}
