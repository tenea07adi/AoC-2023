using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day3 : BasePuzzleLogic
    {
        public Day3() : base(3)
        {

        }

        protected override string LogicPart1(string data)
        {
            int total = 0;

            char[,] matrix = GetMatrixData(data);

            var numbersPozitions = GetNumbersPositions(matrix);
            var specialCharactersPozitions = GetCharacterPositions(matrix);

            foreach ( var np in numbersPozitions )
            {
                if(ExistSpecialCharactersArround(specialCharactersPozitions, np)) //|| ExistSpecialCharactersOnDiagonals(specialCharactersPozitions, np)
                {
                    string stringNumber = "";

                    for(int i = np.StartIntervalColumn; i <= np.EndIntervalColumn; i++) 
                    {
                        stringNumber += matrix[np.Line, i];
                    }
                    total += Int32.Parse(stringNumber);
                }
            }

            return total.ToString();
        }

        protected override string LogicPart2(string data)
        {
            int total = 0;

            char[,] matrix = GetMatrixData(data);

            var numbersPozitions = GetNumbersPositions(matrix);
            var gearsPozitions = GetSpecificCharacterGroupPositonInMatrix(matrix, '*', '*', new char[0], true);

            foreach(var gp in gearsPozitions)
            {
                var numbersFoundArround = GetSpecialCharactersArroundAGroup(numbersPozitions, gp);

                if (numbersFoundArround.Count() > 1)
                {
                    int ratio = 1;

                    numbersFoundArround.ForEach(nfa =>
                    {
                        string stringNumber = "";

                        for (int j = nfa.StartIntervalColumn; j <= nfa.EndIntervalColumn; j++)
                        {
                            stringNumber += matrix[nfa.Line, j];
                        }

                        ratio = ratio * Int32.Parse(stringNumber);
                    });

                    total += ratio;
                }
            }

            return total.ToString();
        }

        private bool ExistSpecialCharactersArround(List<PozitionInMatrix> specialCharactersPozitions, PozitionInMatrix currentSearchGroup)
        {
            if (GetSpecialCharactersArroundAGroup(specialCharactersPozitions, currentSearchGroup).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<PozitionInMatrix> GetSpecialCharactersArroundAGroup(List<PozitionInMatrix> specialCharactersPozitions, PozitionInMatrix currentSearchGroup)
        {
            return specialCharactersPozitions.Where(c =>
                    (c.Line >= currentSearchGroup.Line - 1 && c.Line <= currentSearchGroup.Line + 1)
                    && (c.EndIntervalColumn >= currentSearchGroup.StartIntervalColumn - 1 && c.StartIntervalColumn <= currentSearchGroup.EndIntervalColumn + 1)
                ).ToList();
        }
       
        private List<PozitionInMatrix> GetNumbersPositions(char[,] matrix)
        {
            return GetSpecificCharacterGroupPositonInMatrix(matrix, '0', '9', new char[0], false);
        }

        private List<PozitionInMatrix> GetCharacterPositions(char[,] matrix)
        {
            char[] exeptedChars = new char[] { '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            return GetSpecificCharacterGroupPositonInMatrix(matrix, '!', '~', exeptedChars, true);
        }

        private List<PozitionInMatrix> GetSpecificCharacterGroupPositonInMatrix(char[,] matrix, char startChAsciiCode, char endChAsciiCode, char[] exeptedChars, bool singleCharacterGrousOnly)
        {
            List<PozitionInMatrix> chPozitions = new List<PozitionInMatrix>();

            int linesCount = matrix.GetLength(0);
            int columnsCount = matrix.GetLength(1);

            for (int i = 0; i < linesCount; i++)
            {
                int startPoz = -1;
                int endPoz = -1;

                for (int j = 0; j < columnsCount; j++)
                {
                    if (!exeptedChars.Contains(matrix[i, j]) && matrix[i, j] >= startChAsciiCode && matrix[i, j] <= endChAsciiCode)
                    {
                        if (startPoz == -1)
                        {
                            startPoz = j;
                        }
                        else

                        if(j == columnsCount - 1)
                        {
                            endPoz = j;

                            chPozitions.Add(new PozitionInMatrix() { Line = i, StartIntervalColumn = startPoz, EndIntervalColumn = endPoz });

                            startPoz = -1;
                            endPoz = -1;
                        }

                        if (singleCharacterGrousOnly)
                        {
                            chPozitions.Add(new PozitionInMatrix() { Line = i, StartIntervalColumn = startPoz, EndIntervalColumn = startPoz });

                            startPoz = -1;
                            endPoz = -1;
                        }
                    }
                    else
                    {
                        if (startPoz != -1)
                        {
                            endPoz = j - 1;

                            chPozitions.Add(new PozitionInMatrix() { Line = i, StartIntervalColumn = startPoz, EndIntervalColumn = endPoz });

                            startPoz = -1;
                            endPoz = -1;
                        }
                    }
                }
            }

            return chPozitions;
        }

        private char[,] GetMatrixData(string data)
        {
            List<string> dataLines = StringHelper.ExplodeStringToLines(data);

            char[,] matrix = new char[dataLines.Count, dataLines[0].Length];

            for (int i = 0; i < dataLines.Count; i++)
            {
                for (int j = 0; j < dataLines[i].Length; j++)
                {
                    matrix[i, j] = dataLines[i][j];
                }
            }

            return matrix;
        }
    }

    public class PozitionInMatrix
    {
        public int Line { get; set; }
        public int StartIntervalColumn { get; set; }
        public int EndIntervalColumn { get; set; }
    }
}