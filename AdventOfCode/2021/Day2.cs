using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public class Day2 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            yield return ResultString(Step1());
            yield return ResultString(Step2());
        }

        private int Step1()
        {
            var depth = 0;
            var horizontalPos = 0;

            foreach (var line in Input)
            {
                if (line.StartsWith("forward"))
                    horizontalPos += int.Parse(line.Substring(line.IndexOf(" ")));
                else if (line.StartsWith("down"))
                    depth += int.Parse(line.Substring(line.IndexOf(" ")));
                else if (line.StartsWith("up"))
                    depth -= int.Parse(line.Substring(line.IndexOf(" ")));
            }

            return depth * horizontalPos;
        }

        private int Step2()
        {
            var depth = 0;
            var horizontalPos = 0;
            var aim = 0;

            foreach (var line in Input)
            {
                if (line.StartsWith("forward"))
                {
                    var number = int.Parse(line.Substring(line.IndexOf(" ")));
                    horizontalPos += number;
                    depth += aim * number;
                }
                else if (line.StartsWith("down"))
                    aim += int.Parse(line.Substring(line.IndexOf(" ")));
                else if (line.StartsWith("up"))
                    aim -= int.Parse(line.Substring(line.IndexOf(" ")));
            }

            return depth * horizontalPos;
        }
    }
}
