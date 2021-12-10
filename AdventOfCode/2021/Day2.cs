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
                var num = int.Parse(line.Substring(line.IndexOf(" ")));

                if (line.StartsWith("forward"))
                    horizontalPos += num;
                else if (line.StartsWith("down"))
                    depth += num;
                else if (line.StartsWith("up"))
                    depth -= num;
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
                var num = int.Parse(line.Substring(line.IndexOf(" ")));

                if (line.StartsWith("forward"))
                {
                    horizontalPos += num;
                    depth += aim * num;
                }
                else if (line.StartsWith("down"))
                    aim += num;
                else if (line.StartsWith("up"))
                    aim -= num;
            }

            return depth * horizontalPos;
        }
    }
}
