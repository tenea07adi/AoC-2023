using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day05 : BasePuzzleLogic
    {
        public Day05() : base(5)
        {

        }

        protected override string LogicPart1(string data)
        {
            long[] seeds = GetSeeds(data);
            Dictionary<string[], long[,]> mappings = GetMappings(data);

            long rez = -1;

            for (int i = 0; i < seeds.Length; i++)
            {
                long currentPoz = GetFinalMappingDestination(mappings, seeds[i]);

                if (rez == -1 || currentPoz < rez)
                {
                    rez = currentPoz;
                }
            }

            return rez.ToString();
        }

        protected override string LogicPart2(string data)
        {
            long[] seeds = GetSeeds(data);
            Dictionary<string[], long[,]> mappings = GetMappings(data);

            long rez = -1;

            for (int i = 0; i < seeds.Length; i = i + 2)
            {
                long currentNumber = seeds[i];
                long currentRange = seeds[i + 1];

                long r = RecurentHalfInterval(mappings, currentNumber, currentRange) - 1;

                if (rez == -1 || r < rez)
                {
                    rez = r;
                }
            }

            return rez.ToString();
        }

        // Recurent function which will break the interval in halfs to find the minimum mapping value
        private long RecurentHalfInterval(Dictionary<string[], long[,]> mappings, long startInterval, long rangeInterval)
        {
            long endInterval = startInterval + rangeInterval;

            long firstMappedResult = GetFinalMappingDestination(mappings, startInterval);
            long lastMappedResult = GetFinalMappingDestination(mappings, endInterval);

            if (rangeInterval < 1)
            {
                return startInterval;
            }
            else if (rangeInterval == 1)
            {
                if(firstMappedResult < lastMappedResult)
                {
                    return firstMappedResult;
                }
                else
                {
                    return lastMappedResult;
                }
            }
            else if (firstMappedResult == lastMappedResult - rangeInterval)
            {
                return firstMappedResult;
            }
            else if ( firstMappedResult == lastMappedResult)
            {
                return firstMappedResult;
            }
            else
            {
                long halfRange = rangeInterval / 2;

                long h1 = RecurentHalfInterval(mappings, startInterval, halfRange);
                long h2 = RecurentHalfInterval(mappings, startInterval + halfRange, halfRange);

                if(h1 < h2)
                {
                    return h1;
                }
                else
                {
                    return h2;
                }
            }
        }

        private long GetFinalMappingDestination(Dictionary<string[], long[,]> mappings, long seed)
        {
            long currentPoz = seed;

            foreach (var kvp in mappings)
            {
                currentPoz = GetMappedDestination(kvp.Value, currentPoz);
            }

            return currentPoz;
        }

        private long GetMappedDestination(long[,] mappMatrix, long sourcePoz)
        {
            int cols = mappMatrix.GetLength(1);
            int rows = mappMatrix.GetLength(0);

            long result = sourcePoz;

            for (int i = 0; i < rows; i++)
            {
                long source = mappMatrix[i, 1];
                long destination = mappMatrix[i, 0];
                long range = mappMatrix[i, 2];

                if (source < sourcePoz && source + range > sourcePoz)
                {
                    result = sourcePoz + (destination - source);
                    break;

                }
            }

            return result;
        }

        private long[] GetSeeds(string textData)
        {
            string seedsLine = StringHelper.ExplodeStringToLines(textData)[0];

            string[] seedsAsString = seedsLine.Split(" ");

            long[] seeds = new long[seedsAsString.Length - 1];

            // start at 1 to skip the first tesxt element ("seeds:")
            for (int i = 1; i < seedsAsString.Length; i++)
            {
                // used i-1 because the seedsAsString array is moved with 1 possition
                seeds[i - 1] = Int64.Parse(seedsAsString[i]);
            }

            return seeds;
        }

        private Dictionary<string[], long[,]> GetMappings(string textData)
        {
            Dictionary<string[], long[,]> result = new Dictionary<string[], long[,]>();

            List<string> dataLine = StringHelper.ExplodeStringToLines(textData);

            for (int i = 1; i < dataLine.Count(); i++)
            {
                long[,] resultMatrix = new long[100, 3];
                int mappLineCount = 2;

                if (dataLine[i] == "")
                {
                    while ((i + mappLineCount) < dataLine.Count() && dataLine[i + mappLineCount] != "")
                    {
                        string[] idsAsStrings = dataLine[i + mappLineCount].Split(" ");

                        for (int j = 0; j < idsAsStrings.Length; j++)
                        {
                            resultMatrix[mappLineCount - 2, j] = Int64.Parse(idsAsStrings[j]);
                        }

                        mappLineCount++;
                    }
                }

                resultMatrix = MatrixHelper.ResizeMatrix(resultMatrix, 3, mappLineCount - 2);

                string[] sourceDestination = new string[2];

                sourceDestination[0] = dataLine[i + 1].Split("-to-")[0];
                sourceDestination[1] = dataLine[i + 1].Split("-to-")[1].Split(" ")[0];

                result.Add(sourceDestination, resultMatrix);

                i += mappLineCount - 1;
            }

            return result;
        }
    }
}
