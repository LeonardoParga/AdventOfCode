using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2015
{
    public class Day1 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            return GetFloor(Input[0]);
        }

        private IEnumerable<string> GetFloor(string input)
        {
            var value = 0;
            var part2Solved = false;
            var i = 0;

            foreach (var currentChar in input)
            {
                i++;

                value += currentChar == '(' ? 1 : -1;

                if (!part2Solved && value == -1)
                {
                    part2Solved = true;
                    yield return $"Part 2 - {i}";
                }
            }
            yield return $"Part 1 - {value}";
        }
    }
}
