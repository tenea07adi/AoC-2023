using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day6 : BasePuzzleLogic
    {
        public Day6() : base(6)
        {

        }

        protected override string LogicPart1(string data)
        {
            long[] times = GetTimes(data);
            long[] distances = GetDistances(data);
            long races = times.Length;

            long rez = 1;

            for (int i = 0; i < races; i++)
            {
                rez = rez * GetNumberOfRaceFavCases(times[i], distances[i]);
            }

            return rez.ToString();
        }

        protected override string LogicPart2(string data)
        {
            long[] times = GetTimes(data);
            long[] distances = GetDistances(data);

            long mergedTimes = 0;
            long mergedDistances = 0;

            for (int i = 0; i < times.Length; i++)
            {
                mergedTimes = Int64.Parse(mergedTimes.ToString() + times[i].ToString());
                mergedDistances = Int64.Parse(mergedDistances.ToString() + distances[i].ToString());
            }

            return GetNumberOfRaceFavCases(mergedTimes, mergedDistances).ToString();
        }

        private long GetNumberOfRaceFavCases(long curentTime, long lastWinnerDistance)
        {
            long halfTime = curentTime / 2;

            long favCases = 0;

            if (curentTime % 2 != 0)
            {
                // if is even needs to add 1 to the half
                halfTime++;
                favCases--; 
            }

            // search for fav cases from the half of the interval to 0
            for (int j = 0; j <= halfTime; j++)
            {
                long dist = DistanceForPressedTime(curentTime, halfTime - j);

                if (dist > lastWinnerDistance)
                {
                    favCases += 2; // duplicate beacuse the other half of the interval is in mirror
                }
                else
                {
                    break;
                }
            }

            favCases--;

            return favCases;
        }
       
        private long DistanceForPressedTime(long totalTime, long pressedTime)
        {
            return pressedTime * (totalTime - pressedTime); ;
        }

        private long[] GetTimes(string data)
        {
            return ExtractNumbers(data, 0);
        }

        private long[] GetDistances(string data)
        {
            return ExtractNumbers(data, 1);
        }

        private long[] ExtractNumbers(string data, int row)
        {
            var numbers = StringHelper.ExplodeStringToLines(data)[row].Split(" ").Where(c => c != "" && c != " " && c != "Time:" && c != "Distance:").ToList();

            long[] rez = new long[numbers.Count];

            for (int i = 0; i < numbers.Count; i++)
            {
                rez[i] = Int32.Parse(numbers[i]);
            }

            return rez;
        }
    }
}
