using AoC_2023.Helpers;
using AoC_2023.Logic;
using System.Collections.Generic;

namespace AoC_2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, too warmed world! I'm Adrian and this is my AoC 2023 solution.");
            Console.WriteLine("AoC-2023 URL: " + @"https://adventofcode.com/2023/");
            Console.WriteLine("---------------------------------------------------------------------");

            List<BasePuzzleLogic> puzzles = new List<BasePuzzleLogic>();

            LoadPuzzles(puzzles);
            RunPuzzles(puzzles);
        }

        static void LoadPuzzles(List<BasePuzzleLogic> puzzles)
        {
            puzzles.Add(new Day1());
            puzzles.Add(new Day2());
            puzzles.Add(new Day3());
            puzzles.Add(new Day4());
            puzzles.Add(new Day5());
            puzzles.Add(new Day6());
            puzzles.Add(new Day7());
            puzzles.Add(new Day8());
        }

        static void RunPuzzles(List<BasePuzzleLogic> puzzles)
        {
            foreach (var puzzle in puzzles)
            {
                puzzle.Run();
                Console.WriteLine("---------------------------------------------------------------------");
            }
        }
    }
}