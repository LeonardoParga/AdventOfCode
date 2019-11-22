using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2015
{
    public class Day1 : IChallenge
    {
        public IEnumerable<string> Run()
        {
            var input = FileHandler.OpenFile(nameof(Day1), 2015);

            var lastFloor = GetFloor(input[0]);

            yield return $"Part 1 - {lastFloor.ToString()}";

            yield return $"Part 2 - {lastFloor.ToString()}";
        }

        private int GetFloor(string input)
        {
            var valor = 0;
            foreach (var currentChar in input)
            {
                valor += currentChar == '(' ? 1 : -1;
            }
            return valor;
        }
    }
}
