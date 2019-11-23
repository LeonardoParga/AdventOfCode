using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2015
{
    public class Day3 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            yield return $"Step 1 - {Simulate(new List<Santa>() { new Santa() })}";
            yield return $"Step 2 - {Simulate(new List<Santa>() { new Santa(), new Santa() })}";
        }

        private string Simulate(List<Santa> santas)
        {
            Santa currentSanta = santas.First();

            var grid = new SortedDictionary<string, int>(); // id, count

            grid.Add(currentSanta.CurrentId, santas.Count);
            foreach (var move in Input[0])
            {
                currentSanta.ParseMove(move);

                if (grid.ContainsKey(currentSanta.CurrentId))
                    grid[currentSanta.CurrentId]++; // not really necessary to calculate how many each house gets but ¯\_(ツ)_/¯
                else
                    grid.Add(currentSanta.CurrentId, 1);

                if (santas.Count > 1)
                {
                    var curIndex = santas.IndexOf(currentSanta);
                    currentSanta = santas.ElementAt((curIndex + 1) == santas.Count ? 0 : curIndex + 1);
                }

            }

            return grid.Count().ToString();
        }
    }

    internal class Santa
    {
        public int CurX { get; private set; }

        public int CurY { get; private set; }

        public string CurrentId => $"{CurX}.{CurY}"; // needs a divisor, otherwise numbers like 120 could be either x=12;y=0 or x=1;y=20

        public void ParseMove(char move)
        {
            if (move == '>' || move == '<')
                CurX += move == '>' ? 1 : -1;
            else
                CurY += move == 'v' ? 1 : -1;
        }
    }
}
