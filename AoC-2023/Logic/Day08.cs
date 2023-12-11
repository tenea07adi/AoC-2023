using AoC_2023.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Logic
{
    public class Day08 : BasePuzzleLogic
    {
        public Day08() : base(8)
        {
            
        }

        protected override string LogicPart1(string data)
        {
            string directions = GetDirections(data);
            string[,] nodes = GetNodes(data);

            return CalculateCost(nodes, directions, "AAA", "ZZZ").ToString();
        }

        protected override string LogicPart2(string data)
        {
            string directions = GetDirections(data);
            string[,] nodes = GetNodes(data);

            List<string> startingPoints = new List<string>();
            List<string> endingPoints = new List<string>();


            for ( int i = 0; i < nodes.GetLength(0); i++)
            {
                if (nodes[i, 0][2] == 'A')
                {
                    startingPoints.Add(nodes[i, 0]);
                }

                if (nodes[i, 2][2] == 'Z')
                {
                    endingPoints.Add(nodes[i, 2]);
                }
            }

            return CalculateCostInParalel(nodes, directions, startingPoints.ToArray(), endingPoints.ToArray()).ToString();
        }

        // all search_nodes will move on the same step at hte same direction, it will count as 1 step
        // it will end when every search_node will pe on an end poz
        // if we calc the cost of every search_point, the calc in paralel will be equal with the 'least common multiple' of their single cost
        private long CalculateCostInParalel(string[,] nodes, string directions, string[] startPoints, string[] endPoint)
        {
            long[] counts = new long[startPoints.Length];

            for( int i = 0; i < startPoints.Length; i++)
            {
                counts[i] = CalculateCost(nodes, directions, startPoints[i], endPoint);
            }

            return MathHelper.LCM(counts);
        }

        private int CalculateCost(string[,] nodes, string directions, string startPoint, string endPoint)
        {
            string[] endPoints= new string[1];
            string[] startPoints = new string[1];

            endPoints[0] = endPoint;
            startPoints[0] = startPoint;

            //return CalculateCostInParalel(nodes, directions, startPoints, endPoints);

            return CalculateCost(nodes, directions, startPoint, endPoints);
        }

        private int CalculateCost(string[,] nodes, string directions, string startPoint, string[] endPoint)
        {
            int cost = 0;

            int startPointPoz = -1;

            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                if (nodes[i, 0] == startPoint)
                {
                    startPointPoz = i;
                    break;
                }
            }

            int currentPoz = startPointPoz;

            for (int i = 0; i < directions.Length; i++)
            {
                cost++;
                currentPoz = GetNextNodeArrayPoz(nodes, currentPoz, directions[i]);

                if (endPoint.Contains(nodes[currentPoz, 0]))
                {
                    break;
                }

                if (i + 1 == directions.Length)
                {
                    i = -1;
                }
            }

            return cost;
        }

        private int GetNextNodeArrayPoz(string[,] nodes, int currentPoz, char dir)
        {
            int dirPoz = dir == 'L' ? 1 : 2;
            string searchedPoz = nodes[currentPoz, dirPoz];

            for (int i = 0; i < nodes.GetLength(0); i++) 
            {
                if(nodes[i, 0] == searchedPoz)
                {
                    return i;
                }
            }
            return -1;
        }

        private string GetDirections(string data)
        {
            return StringHelper.ExplodeStringToLines(data)[0];
        }

        private string[,] GetNodes(string data)
        {
            List<string> lines = StringHelper.ExplodeStringToLines(data);

            string[,] map = new string[lines.Count()-2, 3];

            for(int i = 0; i < lines.Count - 2; i++)
            {
                int listCount = i + 2;

                lines[listCount] = lines[listCount].Replace("(", "");
                lines[listCount] = lines[listCount].Replace(")", "");
                lines[listCount] = lines[listCount].Replace(",", "");

                map[i,0] = lines[listCount].Split(" = ")[0];

                map[i, 1] = lines[listCount].Split(" = ")[1].Split(" ")[0];
                map[i, 2] = lines[listCount].Split(" = ")[1].Split(" ")[1];
            }

            return map;
        }
    }
}
