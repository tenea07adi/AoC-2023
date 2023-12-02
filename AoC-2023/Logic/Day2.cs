using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day2 : BasePuzzleLogic
    {
        public Day2() : base(2)
        {
            
        }

        protected override string LogicPart1(string data)
        {
            List<GameData> gamesData = GetGameData(data);

            int total = 0;

            foreach (var gameData in gamesData)
            {
                if (IsPossibleExtraction(gameData, 12, 13, 14))
                {
                    total += gameData.GameId;
                }
            }

            return total.ToString();
        }

        protected override string LogicPart2(string data)
        {
            List<GameData> gamesData = GetGameData(data);

            int total = 0;

            foreach (var gameData in gamesData)
            {
                total += GetMinimumRequiredCubesForGame(gameData, "red") * GetMinimumRequiredCubesForGame(gameData, "green") * GetMinimumRequiredCubesForGame(gameData, "blue");
            }

            return total.ToString();
        }

        private bool IsPossibleExtraction(GameData gameData, int redCount, int greenCount, int blueCount)
        {
            bool isPossible = true;

            foreach (var ext in gameData.Extractions)
            {
                if (ext.ContainsKey("red") && ext["red"] > redCount)
                {
                    isPossible = false;
                    break;
                }

                if (ext.ContainsKey("green") && ext["green"] > greenCount)
                {
                    isPossible = false;
                    break;
                }

                if (ext.ContainsKey("blue") && ext["blue"] > blueCount)
                {
                    isPossible = false;
                    break;
                }
            }

            return isPossible;
        }

        private int GetMinimumRequiredCubesForGame(GameData gameData, string color)
        {
            int minNumberOfCubes = 1;

            foreach (var ext in gameData.Extractions)
            {
                if (ext.ContainsKey(color) && ext[color] > minNumberOfCubes)
                {
                    minNumberOfCubes = ext[color];
                }
            }

            return minNumberOfCubes;
        }

        private List<GameData> GetGameData(string data)
        {
            List<GameData> gamesData = new List<GameData>();

            List<string> dataLines = StringHelper.ExplodeStringToLines(data);

            foreach (string line in dataLines)
            {
                gamesData.Add(new GameData(line));
            }

            return gamesData;
        }
    }

    public class GameData
    {
        public GameData(string gameData)
        {
            Extractions = new List<Dictionary<string, int>>();
            ParseAndLoad(gameData);
        }

        public int GameId { get; set; }

        public List<Dictionary<string, int>> Extractions { get; set; }

        private void ParseAndLoad(string gameData)
        {
            int gameId = Int32.Parse(gameData.Split(":")[0].Replace("Game ", ""));

            List<string> brutExtractions = gameData.Split(":")[1].Split(";").ToList();

            foreach(var bex in brutExtractions)
            {
                Dictionary<string, int> colors = new Dictionary<string, int>();

                List<string> brutColors = bex.Split(",").ToList();

                foreach(var brutColor in brutColors)
                {
                    colors.Add(brutColor.Split(" ")[2], Int32.Parse(brutColor.Split(" ")[1]));
                }

                this.Extractions.Add(colors);
            }

            this.GameId = gameId;
        }
    }
}
