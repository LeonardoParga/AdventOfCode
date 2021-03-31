using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public class Day5 : Day, IChallenge
    {
        private const int DesiredVowelCount = 3;
        private readonly string[] DisallowedCombinations = new string[] { "ab", "cd", "pq", "xy" };

        private readonly Regex rgx = new Regex(@"^(\w+)?(\w{2})(\w+)?(\2)(.+)?$", RegexOptions.Multiline); // any pair of two letters that repeat somewhere else, not overlapping
        private readonly Regex rgx2 = new Regex(@"(\w)\w\1"); // letters repeating anywhere with another letter in between

        public IEnumerable<string> Run()
        {
            var niceStrings1 = Input.Where(x => IsNicePart1(x)).ToList();
            yield return ResultString(niceStrings1.Count);

            var niceStrings2 = Input.Where(x => IsNicePart2(x)).ToList();
            yield return ResultString(niceStrings2.Count);
        }

        private bool IsNicePart1(string str)
        {
            char? lastLetter = null;

            foreach (var letter in str)
            {
                if (letter == lastLetter)
                    return str.Count(x => x.IsVowel()) >= DesiredVowelCount && !ContainsDisallowed(str);
                else
                    lastLetter = letter;
            }
            return false;
        }

        private bool IsNicePart2(string str)
        {
            return rgx.IsMatch(str) && rgx2.IsMatch(str);
        }

        private bool ContainsDisallowed(string str)
        {
            foreach (var combination in DisallowedCombinations)
                if (str.Contains(combination))
                    return true;
            return false;
        }
    }

    internal static class CharExtentions
    {
        public static bool IsVowel(this char letter)
        {
            return letter == 'a' || letter == 'e' || letter == 'i' || letter == 'o' || letter == 'u';
        }
    }
}
