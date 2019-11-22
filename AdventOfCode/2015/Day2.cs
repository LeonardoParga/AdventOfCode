using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2015
{
    public class Day2 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            long total = 0;
            foreach (var box in Input)
            {
                total += GetTotalLength(box);
            }
            yield return ($"Step 1 - {total}");
        }

        private long GetTotalLength(string box)
        {
            var values = box.Split("x").Select(x => long.Parse(x)).ToList();
            var permutations = GetAllPermutations(values);

            return permutations.Select(x => x * 2L).Sum() + GetSmallestSide(permutations);
        }

        private List<long> GetAllPermutations(List<long> values)
        {
            var permutations = new List<long>();
            for (int i = 0; i < values.Count; i++)
            {
                var j = i + 1;

                while (j < values.Count)
                {
                    permutations.Add(values[i] * values[j]);
                    j++;
                }
            }
            return permutations;
        }

        private long GetSmallestSide(List<long> permutations)
        {
            var min = permutations.First();
            foreach (var perm in permutations)
                min = Math.Min(min, perm);
            return min;
        }
    }
}
