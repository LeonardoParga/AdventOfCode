using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day6 : Day, IChallenge
    {
        private readonly string InputParseRgx = @"((.+\n)+)\n?";

        public IEnumerable<string> Run()
        {
            var matches = Regex.Matches(RawInput, InputParseRgx);

            yield return ResultString(Step1(matches));
            yield return ResultString(Step2(matches));
        }

        private int Step1(MatchCollection matches)
        {
            var count = 0;

            foreach (Match match in matches)
                count += match.Value.Distinct().Where(x => x != '\n').Count();

            return count;
        }

        private int Step2(MatchCollection matches)
        {
            var count = 0;

            foreach (Match match in matches)
            {
                var lines = match.Value.Split('\n').Where(x => !string.IsNullOrEmpty(x));

                count += GetCountCharactersInAllLines(lines);
            }

            return count;
        }

        private int GetCountCharactersInAllLines(IEnumerable<string> lines)
        {
            var distinctCharsPerLine = new List<List<char>>();
            var uniqueCharsInAllLines = new List<char>();

            foreach (var line in lines)
                distinctCharsPerLine.Add(line.Distinct().ToList());

            uniqueCharsInAllLines.AddRange(distinctCharsPerLine[0]);
            for (int i = 1; i < distinctCharsPerLine.Count; i++)
                uniqueCharsInAllLines = uniqueCharsInAllLines.Intersect(distinctCharsPerLine[i]).ToList();

            return uniqueCharsInAllLines.Count;
        }
    }
}
