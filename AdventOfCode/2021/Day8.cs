using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public class Day8 : Day, IChallenge
    {
        private readonly int[] _uniqueNumberOfSegments = { 2, 3, 4, 7 };
        private readonly char[] _segments = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
        private readonly Dictionary<string, int> _segmentsToDigits = new Dictionary<string, int>()
        {
            {"abcefg", 0 }, {"cf", 1 }, {"acdeg", 2 }, {"acdfg", 3 }, {"bcdf", 4 },
            {"abdfg", 5 }, {"abdefg", 6 }, {"acf", 7 }, {"abcdefg", 8 }, {"abcdfg", 9 }
        };

        public IEnumerable<string> Run()
        {
            var rgx = new Regex(@"^(.+?)\s\|\s(.+?)\s?$", RegexOptions.Multiline);
            var matches = rgx.Matches(RawInput);

            var countDigitsWithUniqueNumberOfSegments = 0;
            var sumDisplays = 0;

            foreach (Match match in matches)
            {
                var clues = match.Groups[1].Value.Split(" ").ToList();
                var numsToDecode = match.Groups[2].Value.Split(" ").ToList();

                countDigitsWithUniqueNumberOfSegments += GetCountDigitsWithUniqueNumberOfSegments(numsToDecode);

                sumDisplays += Decode(clues, numsToDecode);
            }

            yield return ResultString(countDigitsWithUniqueNumberOfSegments); // Step 1
            yield return ResultString(sumDisplays); // Step 2
        }

        private int GetCountDigitsWithUniqueNumberOfSegments(List<string> numsToDecode)
            => numsToDecode.Count(x => _uniqueNumberOfSegments.Contains(x.Length));

        private int Decode(List<string> clues, List<string> numsToDecode)
        {
            var lettersDict = GetLetterDictionary(clues);
            var numStr = string.Empty;

            foreach (var num in numsToDecode)
            {
                var translatedSegments = new string(num.Select(x => lettersDict[x]).OrderBy(x => x).ToArray());
                numStr += _segmentsToDigits[translatedSegments];
            }

            return int.Parse(numStr);
        }

        private Dictionary<char, char> GetLetterDictionary(List<string> clues)
        {
            var lettersDict = new Dictionary<char, char>();

            // I can assume that each number always appears once in the clues,
            // and in a seven segment display some segments appear a unique number of times across all numbers
            // segment e appears 4 times, segment b appears 6 times, and segment f appears 9 times
            // this way I can immediately solve those numbers

            var groupedLetters = clues.SelectMany(x => x).GroupBy(x => x).ToList();

            lettersDict.Add(groupedLetters.Single(x => x.Count() == 4).Key, 'e');
            lettersDict.Add(groupedLetters.Single(x => x.Count() == 6).Key, 'b');
            lettersDict.Add(groupedLetters.Single(x => x.Count() == 9).Key, 'f');

            // after that, I can figure out the segment c, since the digit 1 only uses c and f, and I already know f
            var segmentC = clues.Single(x => x.Count() == 2).Single(x => !lettersDict.ContainsKey(x));
            lettersDict.Add(segmentC, 'c');

            // now, I can figure out segment d, since it's the only one missing for digit 4
            var segmentD = clues.Single(x => x.Count() == 4).Single(x => !lettersDict.ContainsKey(x));
            lettersDict.Add(segmentD, 'd');

            // I can also figure out segment a, since it's the only one missing for digit 7
            var segmentA = clues.Single(x => x.Count() == 3).Single(x => !lettersDict.ContainsKey(x));
            lettersDict.Add(segmentA, 'a');

            // segment g is the only letter left
            lettersDict.Add(_segments.Except(lettersDict.Keys).Single(), 'g');

            return lettersDict;
        }
    }
}
