using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day4 : Day, IChallenge
    {
        private readonly string InputParseRgx = @"((.+\n)+)\n?";

        public IEnumerable<string> Run()
        {
            var passportsString = Regex.Matches(RawInput, InputParseRgx);
            var passports = passportsString.Select(x => new Passport(x.Captures.First().Value)).ToList();

            yield return ResultString(Step1(passports));
            yield return ResultString(Step2(passports));
        }

        private int Step1(List<Passport> passports)
        {
            return passports.Count(x => x.IsValidStep1());
        }

        private int Step2(List<Passport> passports)
        {
            return passports.Count(x => x.IsValidStep2());
        }

        private class Passport
        {
            private readonly string InputParseRgx = @"((\w{3})\:([\w\d#]+)[\s\n])";

            private static readonly Dictionary<string, Func<string, bool>> NecessaryFields = new Dictionary<string, Func<string, bool>>
            {
                { "byr", (input) =>
                    {
                        if(int.TryParse(input, out int num))
                            return num >= 1920 && num <= 2002;
                        else return false;
                    }
                },
                { "iyr", (input) =>
                    {
                        if(int.TryParse(input, out int num))
                            return num >= 2010 && num <= 2020;
                        else return false;
                    }
                },
                { "eyr", (input) =>
                    {
                        if(int.TryParse(input, out int num))
                            return num >= 2020 && num <= 2030;
                        else return false;
                    }
                },
                { "hgt", (input) =>
                    {
                        var rgxMatch = Regex.Match(input, @"^((\d+)(cm|in))$");

                        if(int.TryParse(rgxMatch.Groups[2].Value, out int num))
                        {
                            var units = rgxMatch.Groups[3].Value;
                            if (units == "cm")
                                return num >= 150 && num <= 193;
                            else if (units == "in")
                                return num >= 59 && num <= 76;
                        }
                        return false;
                    }
                },
                { "hcl", (input) => { return Regex.Match(input, @"^#[\w\d]{6}").Success; } },
                { "ecl", (input) => { return Regex.Match(input, @"^(amb|blu|brn|gry|grn|hzl|oth)$").Success; } } ,
                { "pid", (input) => { return Regex.Match(input, @"^\d{9}$").Success; } }
            };

            public Passport(string rawInput)
            {
                Dictionary = new Dictionary<string, string>();

                var fields = Regex.Matches(rawInput, InputParseRgx);
                for (int i = 0; i < fields.Count; i++)
                    Dictionary.Add(fields[i].Groups[2].Value, fields[i].Groups[3].Value);
            }

            public Dictionary<string, string> Dictionary { get; set; }

            public bool IsValidStep1()
            {
                foreach (var field in NecessaryFields)
                    if (!Dictionary.Any(x => x.Key == field.Key))
                        return false;

                return true;
            }

            public bool IsValidStep2()
            {
                foreach (var field in NecessaryFields)
                {
                    var dictField = Dictionary.FirstOrDefault(x => x.Key == field.Key);
                    if (dictField.Equals(default(KeyValuePair<string, string>)))
                        return false;
                    else if (!field.Value.Invoke(dictField.Value))
                        return false;
                }

                return true;
            }
        }
    }
}
