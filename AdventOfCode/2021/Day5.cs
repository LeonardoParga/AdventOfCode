using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public class Day5 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var rgx = new Regex(@"(\d+),(\d+) -> (\d+),(\d+)");
            var matches = rgx.Matches(RawInput);
            var values = matches.Select(m => (x1: int.Parse(m.Groups[1].Value), y1: int.Parse(m.Groups[2].Value), x2: int.Parse(m.Groups[3].Value), y2: int.Parse(m.Groups[4].Value))).ToList();

            yield return ResultString(GetNumOfPointsWhereLinesOverlap(values, onlyHorizontalAndVertical: true));
            yield return ResultString(GetNumOfPointsWhereLinesOverlap(values));
        }

        private int GetNumOfPointsWhereLinesOverlap(List<(int x1, int y1, int x2, int y2)> values, bool onlyHorizontalAndVertical = false)
        {
            var map = new List<(int, int)>();

            foreach (var value in values)
            {
                if (onlyHorizontalAndVertical && value.x1 != value.x2 && value.y1 != value.y2)
                    continue;

                var current = (value.x1, value.y1);
                var target = (value.x2, value.y2);

                while (current != target)
                {
                    map.Add(current);

                    // get next point in line, moving towards target
                    current = (x1: current.x1 == value.x2 ? current.x1 : current.x1 > value.x2 ? current.x1 - 1 : current.x1 + 1,
                               y1: current.y1 == value.y2 ? current.y1 : current.y1 > value.y2 ? current.y1 - 1 : current.y1 + 1);
                }

                map.Add(target);
            }

            return map.GroupBy(x => x).Where(x => x.Count() > 1).Count();
        }
    }
}
