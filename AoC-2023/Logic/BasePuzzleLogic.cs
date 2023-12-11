using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public abstract class BasePuzzleLogic
    {
        private int CalendarDay = 0;
        private string PuzzleUrlBase = @"https://adventofcode.com/2023/day/";

        public BasePuzzleLogic(int calendarDay)
        {
            CalendarDay = calendarDay;
        }

        public void Run()
        {
            string calendarDayAsString = CalendarDay.ToString();
            calendarDayAsString = calendarDayAsString.Length == 1 ? "0" + calendarDayAsString : calendarDayAsString;

            string data = DataProviderHelper.GetText($"\\Day{calendarDayAsString}\\data.txt");

            string resultP1 = this.LogicPart1(data);
            string resultP2 = this.LogicPart2(data);

            LogResult($"Part 1: {resultP1} \n Part 2: {resultP2}");
        }

        private void LogResult(string result)
        {
            Console.WriteLine($"Day_{CalendarDay}: \n {PuzzleUrlBase}{CalendarDay} \n {result} \n");
        }

        protected abstract string LogicPart1(string data);
        protected abstract string LogicPart2(string data);

    }
}
