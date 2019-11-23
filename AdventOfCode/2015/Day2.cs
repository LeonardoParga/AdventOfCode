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
            long totalRibbon = 0;
            foreach (var box in Input)
            {
                var values = box.Split("x").Select(x => long.Parse(x)).ToList();
                values.Sort();
                var permutations = GetAllPermutations(values); // ordered ascending

                total += GetTotalLength(permutations);
                totalRibbon += GetTotalLengthRibbon(values);
            }
            yield return ResultString(1, total);
            yield return ResultString(2, totalRibbon);
        }

        private long GetTotalLength(List<long> permutations)
        {
            return permutations.Select(x => x * 2L).Sum() + permutations.First();
        }

        private long GetTotalLengthRibbon(List<long> values)
        {
            var total = 1L;
            foreach (var item in values)
                total *= item;
            total += (values[0] * 2) + (values[1] * 2);
            return total;
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
            permutations.Sort();
            return permutations;
        }

        //private long GetSmallestSide(List<long> permutations)
        //{
        //    var min = permutations.First();
        //    foreach (var perm in permutations)
        //        min = Math.Min(min, perm);
        //    return min;
        //}
    }
}
