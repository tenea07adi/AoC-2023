using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Helpers
{
    public static class MatrixHelper
    {
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
    }
}
