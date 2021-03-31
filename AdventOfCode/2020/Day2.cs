using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day2 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var entries = Input.Select(x => new Entry(x)).ToList();

            yield return ResultString(Step1(entries));
            yield return ResultString(Step2(entries));
        }

        private int Step1(List<Entry> entries)
        {
            return entries.Count(x => x.IsValidStep1());
        }

        private int Step2(List<Entry> entries)
        {
            return entries.Count(x => x.IsValidStep2());
        }

        private class Entry
        {
            private readonly string InputParseRgx = @"^(\d+)\-(\d+)\s(\w)\:\s(\w+)$";

            public Entry(string input)
            {
                var parsedRegex = Regex.Match(input, InputParseRgx);
                Policy = new Policy(int.Parse(parsedRegex.Groups[1].Value), int.Parse(parsedRegex.Groups[2].Value), char.Parse(parsedRegex.Groups[3].Value));
                Password = parsedRegex.Groups[4].Value;
            }

            public Policy Policy { get; set; }
            public string Password { get; set; }

            public bool IsValidStep1()
            {
                var charCount = Password.Count(x => x == Policy.Character);
                return charCount >= Policy.MinAmount && charCount <= Policy.MaxAmount;
            }

            public bool IsValidStep2()
            {
                return Password[Policy.FirstPosition] == Policy.Character ^ Password[Policy.SecondPosition] == Policy.Character;
            }
        }

        private class Policy
        {
            public Policy(int value1, int value2, char character)
            {
                Value1 = value1;
                Value2 = value2;
                Character = character;
            }

            private int Value1;
            private int Value2;

            public int MinAmount { get { return Value1; } }
            public int MaxAmount { get { return Value2; } }

            public int FirstPosition { get { return Value1 - 1; } } // The input is not zero-indexed, so I subtract one to get the zero-indexed position
            public int SecondPosition { get { return Value2 - 1; } }

            public char Character { get; private set; }
        }

    }
}
