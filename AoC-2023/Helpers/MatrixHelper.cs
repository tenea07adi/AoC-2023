using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Helpers
{
    public static class MatrixHelper
    {
        public delegate void MatrixOperation<T>(T[,] matrix, int i, int j);

        public static T[,] MatrixElmOperation<T>(T[,] matrix, MatrixOperation<T> matrixOperation)
        {
            int rowsCount = matrix.GetLength(0);
            int colsCount = matrix.GetLength(1);

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < colsCount; j++)
                {
                    matrixOperation.Invoke(matrix, i, j);
                }
            }

            return matrix;
        }

        public static T[,] ExpandMatrix<T>(T[,] original, int insertPoz, T valToInsert, bool insertAsColumn)
        {
            int count = insertAsColumn == false ? original.GetLength(0) : original.GetLength(1);

            T[] valuesToInsert = new T[count];

            for (int i = 0; i < count; i++)
            {
                valuesToInsert[i] = valToInsert;
            }

            return ExpandMatrix(original, insertPoz, valuesToInsert, insertAsColumn);
        }

        public static T[,] ExpandMatrix<T>(T[,] original, int insertPoz, T[] dataToInsert, bool insertAsColumn)
        {
            T[,] newMatrix = null;

            int origRowsCount = original.GetLength(0);
            int origColsCount = original.GetLength(1);

            int newRowsCount = origRowsCount;
            int newColsCount = origColsCount;

            if (insertAsColumn)
            {
                newColsCount++;
            }
            else
            {
                newRowsCount++;
            }

            newMatrix = new T[newRowsCount, newColsCount];

            int origI = 0;
            for (int i = 0; i < newRowsCount; i++)
            {
                int origJ = 0;
                for (int j = 0; j < newColsCount; j++)
                {
                    if (!insertAsColumn && i == insertPoz)
                    {
                        newMatrix[i, j] = dataToInsert[j];
                        origI = i - 1;
                    }
                    else if (insertAsColumn && j == insertPoz)
                    {
                        newMatrix[i, j] = dataToInsert[origJ];
                        origJ = j - 1;
                    }
                    else
                    {
                        newMatrix[i, j] = original[origI, origJ];
                    }
                    origJ++;
                }
                origI++;
            }

            return newMatrix;
        }

        public static T[,] ResizeMatrix<T>(T[,] original, int newColumnLenght, int newRowLenght)
        {
            T[,] newMatrix = new T[newRowLenght, newColumnLenght];

            for (int i = 0; i < newRowLenght; i++)
            {
                for (int j = 0; j < newColumnLenght; j++)
                {
                    newMatrix[i, j] = original[i, j];
                }
            }

            return newMatrix;
        }

        public static T[,] CopyMatrix<T>(T[,] original)
        {
            return ResizeMatrix(original, original.GetLength(1), original.GetLength(0));
        }

        public static string[,] StringToMatrix(string matrixString)
        {
            return StringToMatrix(StringHelper.ExplodeStringToLines(matrixString));
        }

        public static string[,] StringToMatrix(List<string> matrixStringLines)
        {
            string[,] matrix = new string[matrixStringLines.Count, matrixStringLines[0].Length];
            for (int i = 0; i < matrixStringLines.Count; i++)
            {
                for (int j = 0; j < matrixStringLines[i].Length; j++)
                {
                    matrix[i, j] = matrixStringLines[i][j].ToString();
                }
            }

            return matrix;
        }
    }
}
