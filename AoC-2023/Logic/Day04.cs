using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day04 : BasePuzzleLogic
    {
        public Day04() : base(4)
        {

        }

        protected override string LogicPart1(string data)
        {
            int result = 0;

            foreach (var line in StringHelper.ExplodeStringToLines(data))
            {
                result += CalculateCardPoints(new Scratchcard(line));
            }

            return result.ToString();
        }

        protected override string LogicPart2(string data)
        {
            int result = 0;

            List<Scratchcard> cards = new List<Scratchcard>();

            foreach (var line in StringHelper.ExplodeStringToLines(data))
            {
                cards.Add(new Scratchcard(line));
            }

            result = CalculateHowManyCardPickUpForOneCard(cards);

            return result.ToString();
        }

        private int CalculateHowManyCardPickUpForOneCard(List<Scratchcard> cards)
        {
            int result = 0;

            int[] winnersCounts = new int[cards.Count];

            for (int i = 0; i < cards.Count; i++)
            {
                winnersCounts[i] = GetWinningNumbers(cards[i]).Count;
            }

            for (int i = 0; i < cards.Count; i++)
            {
                result = result + CalculateHowManyCardPickUpForOneCard_RecursivePart(winnersCounts, i) + 1;
            }

            return result;
        }

        private int CalculateHowManyCardPickUpForOneCard_RecursivePart(int[] winnersCounts, int currentCardPozition)
        {
            int result = 0;

            for (int i = currentCardPozition + 1; i <= winnersCounts[currentCardPozition] + currentCardPozition; i++)
            {
                result = result + CalculateHowManyCardPickUpForOneCard_RecursivePart(winnersCounts, i);
            }

            result = result + winnersCounts[currentCardPozition];

            return result;
        }

        private int CalculateCardPoints(Scratchcard card)
        {
            int winnersNumber = GetWinningNumbers(card).Count();

            if (winnersNumber > 0)
            {
                return (int)Math.Pow(2, winnersNumber - 1);
            }

            return 0;
        }

        private List<int> GetWinningNumbers(Scratchcard card)
        {
            List<int> winners = new List<int>();

            foreach (var cn in card.CurrentNumbers)
            {
                if (IsWinningNumber(card.WinningNumbers, cn))
                {
                    winners.Add(cn);
                }
            }

            return winners;
        }

        private bool IsWinningNumber(List<int> winningNumbers, int currentNumber)
        {
            bool isWinningNumber = false;

            foreach (var wn in winningNumbers)
            {
                if (wn == currentNumber)
                {
                    isWinningNumber = true;
                    break;
                }
            }

            return isWinningNumber;
        }
    }

    public class Scratchcard
    {
        public Scratchcard(string lineData)
        {
            this.Id = 0;
            this.WinningNumbers = new List<int>();
            this.CurrentNumbers = new List<int>();

            TransformTextToObject(lineData);
        }

        public int Id { get; set; }
        public List<int> WinningNumbers { get; set; }
        public List<int> CurrentNumbers { get; set; }

        private void TransformTextToObject(string lineData)
        {
            List<string> brutDataAsList = lineData.Split(' ').ToList();

            for (int i = 0; i < brutDataAsList.Count; i++)
            {
                if (brutDataAsList[i] == "")
                {
                    brutDataAsList.Remove(brutDataAsList[i]);
                    i--;
                }
            }

            this.Id = Int32.Parse(brutDataAsList[1].Replace(":", ""));

            bool foundDelimit = false;

            for (int i = 2; i < brutDataAsList.Count; i++)
            {
                if (!foundDelimit)
                {
                    if (brutDataAsList[i] == "|")
                    {
                        foundDelimit = true;
                    }
                    else
                    {
                        WinningNumbers.Add(Int32.Parse(brutDataAsList[i]));
                    }
                }
                else
                {
                    CurrentNumbers.Add(Int32.Parse(brutDataAsList[i]));
                }
            }
        }
    }

}
