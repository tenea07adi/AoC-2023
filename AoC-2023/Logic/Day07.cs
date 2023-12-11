using AoC_2023.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day07 : BasePuzzleLogic
    {
        public Day07() : base(7)
        {

        }

        protected override string LogicPart1(string data)
        {
            string[] hands = GetHands(data);
            int[] bids = GetBids(data);

            int[] handsRanks = GetHandsRanks(hands, false);

            long rez = 0;

            for (int i = 0; i < hands.Length; i++)
            {
                rez += handsRanks[i] * bids[i];
            }

            return rez.ToString();
        }

        protected override string LogicPart2(string data)
        {
            string[] hands = GetHands(data);
            int[] bids = GetBids(data);

            int[] handsRanks = GetHandsRanks(hands, true);

            long rez = 0;

            for (int i = 0; i < hands.Length; i++)
            {
                rez += handsRanks[i] * bids[i];
            }

            return rez.ToString();
        }

        public int[] GetHandsRanks(string[] hands, bool useJockerRule)
        {
            int[] rank = new int[hands.Length];
            long[] handsPowers = new long[hands.Length];

            for (int i = 0; i < rank.Length; i++)
            {
                handsPowers[i] = GetHandPower(hands[i], useJockerRule);
            }

            for (int i = 0; i < rank.Length; i++)
            {
                int shorterThenCurrent = 0;

                for (int j = 0; j < handsPowers.Length; j++)
                {
                    if (i != j)
                    {
                        if (handsPowers[i] > handsPowers[j])
                        {
                            shorterThenCurrent++;
                        }
                        else if (handsPowers[i] == handsPowers[j] && i < j)
                        {
                            shorterThenCurrent++;
                        }
                    }
                }

                rank[i] = shorterThenCurrent + 1;
            }

            return rank;
        }

        // will return the power of the hand as a number with a specific place for each character. 0 00 00 00 00 00 where (the pair power) (the first character power) (the second character pwoer) ...
        public long GetHandPower(string hand, bool useJockerRule)
        {
            long handPower = 0;
            string foundCharacters = "";

            int biggestPairCardsCount = 1;

            if (useJockerRule)
            {
                foundCharacters += "J";
            }

            for (int i = 0; i < hand.Length; i++)
            {
                char currentCharacter = hand[i];
                int numberOfSameCards = 1;
                int characterPower = GetCharacterPower(currentCharacter, useJockerRule);

                if (!foundCharacters.Contains(currentCharacter)) // if the character was found meas that it was checked for pairs
                {
                    foundCharacters += currentCharacter;

                    for (int j = 0; j < hand.Length; j++)
                    {
                        if (j != i && hand[j] == hand[i])
                        {
                            numberOfSameCards++;
                        }
                    }
                }

                if (numberOfSameCards > 1)
                {
                    if (biggestPairCardsCount < numberOfSameCards)
                    {
                        biggestPairCardsCount = numberOfSameCards;
                    }

                    handPower += (Int64)Math.Pow(100, hand.Length) * GetPairPower(numberOfSameCards); // set the pairs part of the value
                }
                handPower += (Int64)Math.Pow(100, hand.Length - i - 1) * characterPower; // set each character power on a specific point
            }

            if (useJockerRule)
            {
                int jockersNumer = hand.Where(c => c == 'J').Count();

                if (jockersNumer > 0)
                {
                    int totalPairWithJocker = jockersNumer + biggestPairCardsCount;

                    handPower -= (Int64)Math.Pow(100, hand.Length) * GetPairPower(biggestPairCardsCount);

                    handPower += (Int64)Math.Pow(100, hand.Length) * GetPairPower(totalPairWithJocker);
                }
            }
            return handPower;
        }

        public int GetPairPower(int numberOfCards)
        {
            int powerOfPairInCalc = 0;

            switch (numberOfCards) // solve the two special cases "Two pair" and "Three of a kind". Without it will return the same value for many cases
            {
                case 2:
                    powerOfPairInCalc = 1;
                    break;
                case 3:
                    powerOfPairInCalc = 3;
                    break;
                case 4:
                    powerOfPairInCalc = 5;
                    break;
                case 5:
                    powerOfPairInCalc = 6;
                    break;
                case 6: // just for the joker case when we have 5 jokers
                    powerOfPairInCalc = 6;
                    break;
            }

            return powerOfPairInCalc;
        }

        public int GetCharacterPower(char character, bool useJockerRule)
        {
            string characterOrderedByPower = "AKQJT98765432";

            if (useJockerRule)
            {
                characterOrderedByPower = "AKQT98765432J"; ;
            }

            for (int i = 0; i < characterOrderedByPower.Length; i++)
            {
                if (characterOrderedByPower[i] == character)
                {
                    return characterOrderedByPower.Length - i + 1; // because is missing character 1
                }
            }

            return 0;
        }

        public string[] GetHands(string data)
        {
            return ExtractValues(data, 0);
        }

        public int[] GetBids(string data)
        {
            string[] bidsAsString = ExtractValues(data, 1);
            int[] rez = new int[bidsAsString.Length];

            for (int i = 0; i < bidsAsString.Length; i++)
            {
                rez[i] = Int32.Parse(bidsAsString[i]);
            }
            return rez;
        }

        private string[] ExtractValues(string data, int col)
        {
            List<string> lines = StringHelper.ExplodeStringToLines(data);
            string[] rez = new string[lines.Count];

            for (int i = 0; i < lines.Count; i++)
            {
                rez[i] = lines[i].Split(" ")[col];
            }

            return rez;
        }
    }
}
