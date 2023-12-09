using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day10 : BasePuzzleLogic
    {
        public Day10() : base(10)
        {
            
        }

        protected override string LogicPart1(string data)
        {
            string[,] pipesMap = GetPipesMap(data);
            int[] animalPoz = FindAnimalPoz(pipesMap);

            int res = 0;

            int minCost = -1;

            for(int i = 0; i< 4; i++)
            {
                int currentCost = FindLoopLenght(pipesMap, animalPoz, i, false);

                if((minCost == -1 || minCost > currentCost) && currentCost != -1)
                {
                    minCost = currentCost;
                }
            }
                
            if(minCost % 2 == 0)
            {
                res = minCost / 2;
            }
            else
            {
                res = minCost / 2 + 1;
            }

            return res.ToString();
        }

        protected unsafe override string LogicPart2(string data)
        {
            string[,] pipesMap = GetPipesMap(data);
            int[] animalPoz = FindAnimalPoz(pipesMap);

            int res = 0;

            int minCost = -1;
            int minLoop = -1;

            for (int i = 0; i < 4; i++)
            {
                int currentCost = FindLoopLenght(pipesMap, animalPoz, i, false);

                if ((minCost == -1 || minCost > currentCost) && currentCost != -1)
                {
                    minCost = currentCost;
                    minLoop = i;
                }
            }

            int b = 20;
            int* ptr1 = &b;

            var matrixMapPointer = &pipesMap;

            FindLoopLenght(*matrixMapPointer, animalPoz, minLoop, true);

            return res.ToString();
        }

        private int InteriorSpaces(string[,] pipesMap)
        {
            return 0;
        }

        // find if around the startPoz exist a loop
        // it is noit eval the startPoz
        // startDir can be: 0 - left; 1  bottom; 2 - right; 3 - top
        // replacePath for part two
        private int FindLoopLenght(string[,] pipesMap, int[] startPoz, int startDir, bool replacePath)
        {

            bool run = true;
            int maxCost = pipesMap.GetLength(0) * pipesMap.GetLength(1);

            int costCount = 0;

            int[] curentPoz = new int[2];

            switch (startDir)
            {
                case 0:
                    curentPoz = new int[2] { startPoz[0], startPoz[1] - 1 };
                    if (pipesMap[curentPoz[0], curentPoz[1]] == "7" || pipesMap[curentPoz[0], curentPoz[1]] == "|")
                    {
                        return -1;
                    }
                    break;
                case 1:
                    curentPoz = new int[2] { startPoz[0] + 1, startPoz[1] };
                    if (pipesMap[curentPoz[0], curentPoz[1]] == "7" || pipesMap[curentPoz[0], curentPoz[1]] == "-")
                    {
                        return -1;
                    }
                    break;
                case 2:
                    curentPoz = new int[2] { startPoz[0], startPoz[1] + 1 };
                    if (pipesMap[curentPoz[0], curentPoz[1]] == "F" || pipesMap[curentPoz[0], curentPoz[1]] == "|")
                    {
                        return -1;
                    }
                    break;
                case 3:
                    curentPoz = new int[2] { startPoz[0] - 1, startPoz[1] };
                    if (pipesMap[curentPoz[0], curentPoz[1]] == "J" || pipesMap[curentPoz[0], curentPoz[1]] == "-")
                    {
                        return -1;
                    }
                    break;
            }

            while (run)
            {

                int[] nextPoz = GetNextPoz(pipesMap, curentPoz, startPoz);

                if(nextPoz == null)
                {
                    costCount =  - 1;
                    break;
                }

                if (pipesMap[nextPoz[0], nextPoz[1]] == "S")
                {
                    run = false;
                }

                if (replacePath)
                {
                    pipesMap[nextPoz[0], nextPoz[1]] = "*";
                }

                startPoz = curentPoz;
                curentPoz = nextPoz;

                costCount++;

                if(costCount >= maxCost)
                {
                    run = false;
                }
            }

            return costCount;
        }

        private int[] GetNextPoz(string[,] pipesMap, int[] currentPoz, int[] lastPoz)
        {
            if (currentPoz[0] < 0 || currentPoz[0] > pipesMap.GetLength(0) || currentPoz[1] < 0 || currentPoz[1] > pipesMap.GetLength(1))
            {
                return null;
            }

            int[] nextPoz = new int[2];
            switch (pipesMap[currentPoz[0], currentPoz[1]])
            {
                case "|":
                    nextPoz[1] = currentPoz[1];
                    nextPoz[0] = currentPoz[0] + (currentPoz[0] - lastPoz[0]);
                    break;
                case "-":
                    nextPoz[0] = currentPoz[0];
                    nextPoz[1] = currentPoz[1] + (currentPoz[1] - lastPoz[1]);
                    break;
                case "L":
                    if(currentPoz[0] == lastPoz[0]) // from right to top
                    {
                        nextPoz[0] = currentPoz[0] - 1;
                        nextPoz[1] = currentPoz[1];
                    }
                    else
                    {
                        nextPoz[0] = currentPoz[0];
                        nextPoz[1] = currentPoz[1] + 1;
                    }
                    break;
                case "J":
                    if (currentPoz[0] == lastPoz[0]) // from left to top
                    {
                        nextPoz[0] = currentPoz[0] - 1;
                        nextPoz[1] = currentPoz[1];
                    }
                    else
                    {
                        nextPoz[0] = currentPoz[0];
                        nextPoz[1] = currentPoz[1] - 1;
                    }
                    break;
                case "7":
                    if (currentPoz[0] == lastPoz[0])  // from bottom to left
                    {
                        nextPoz[0] = currentPoz[0] + 1;
                        nextPoz[1] = currentPoz[1];
                    }
                    else
                    {
                        nextPoz[0] = currentPoz[0];
                        nextPoz[1] = currentPoz[1] - 1;
                    }
                    break;
                case "F":
                    if (currentPoz[0] == lastPoz[0])  // from bottom to left
                    {
                        nextPoz[0] = currentPoz[0] + 1;
                        nextPoz[1] = currentPoz[1];
                    }
                    else
                    {
                        nextPoz[0] = currentPoz[0];
                        nextPoz[1] = currentPoz[1] + 1;
                    }
                    break;
                default:
                    return null;
                    break;
            }

            return nextPoz;
        }

        private int[] FindAnimalPoz(string[,] pipesMap)
        {
            int[] animalPoz= new int[2];

            for(int i = 0; i < pipesMap.GetLength(0); i++)
            {
                for (int j = 0; j < pipesMap.GetLength(1); j++)
                {
                    if (pipesMap[i,j] == "S")
                    {
                        animalPoz[0] = i;
                        animalPoz[1] = j;
                        return animalPoz;
                    }
                }
            }

            return animalPoz;
        }

        private string[,] GetPipesMap(string data)
        {
            List<string> lines = StringHelper.ExplodeStringToLines(data);

            string[,] res = new string[lines.Count, lines[0].Length];

            for (int i = 0; i < lines.Count; i++)
            {

                for (int j = 0; j < lines[0].Length; j++)
                {
                    res[i, j] = lines[i][j].ToString();
                }
            }

            return res;
        }
    }
}
