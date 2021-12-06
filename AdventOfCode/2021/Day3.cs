using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public class Day3 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var numbers = Input.Select(x => Convert.ToUInt32(x, 2)).ToList();

            yield return ResultString(ParseReport(numbers, out uint lifeSupportRating));
            yield return ResultString(lifeSupportRating);
        }

        private uint ParseReport(List<uint> numbers, out uint lifeSupportRating)
        {
            var numOfDigits = Input[0].Length;
            uint gammaRate = 0;
            List<uint> oxygenCandidates = numbers.ToList();
            List<uint> CO2Candidates = numbers.ToList();

            for (int i = numOfDigits - 1; i >= 0; i--)
            {
                var temp = 0;

                foreach (var num in numbers)
                    temp += IsBitSet(num, i) ? 1 : -1;

                if (temp >= 0)
                    gammaRate |= (uint)1 << i; // set the bit at index i to 1, keep the others the same
                else
                    gammaRate &= ~((uint)1 << i); // set the bit at index i to 0, keep the others the same

                oxygenCandidates = FilterCandidates(oxygenCandidates, i, descending: true);
                CO2Candidates = FilterCandidates(CO2Candidates, i);
            }

            var epsilonRate = ~gammaRate & 0xFFF;
            lifeSupportRating = oxygenCandidates.Single() * CO2Candidates.Single();

            return gammaRate * epsilonRate;
        }

        private bool IsBitSet(uint num, int pos) => (num & (1 << pos)) != 0;

        private List<uint> FilterCandidates(List<uint> list, int pos, bool descending = false)
        {
            if (list.Count <= 1) return list;

            return descending ? list.GroupBy(x => IsBitSet(x, pos)).OrderByDescending(x => x.Count()).ThenByDescending(x => x.Key).First().ToList() :
                                list.GroupBy(x => IsBitSet(x, pos)).OrderBy(x => x.Count()).ThenBy(x => x.Key).First().ToList();
        }
    }
}
