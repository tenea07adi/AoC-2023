using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Helpers
{
    public static class ArrayHelper
    {
        public static T[] ResizeArray<T>(T[] original, int newLength)
        {
            T[] newArray = new T[newLength];

            for (int i = 0; i < newLength; i++)
            {

                newArray[i] = original[i];
            }

            return newArray;
        }
    }
}
