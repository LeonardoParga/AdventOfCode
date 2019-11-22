using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2015
{
    public class Day1 : IChallenge
    {
        public string Run()
        {
            var input = FileHandler.OpenFile(nameof(Day1));

            var lastFloor = GetFloor(input[0]);

            return lastFloor.ToString();
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
