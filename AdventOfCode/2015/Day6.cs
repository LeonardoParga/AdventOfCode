using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public class Day6 : Day, IChallenge
    {
        // matches the command and gets each value in a capture group
        private readonly Regex ValuesMatchRgx = new Regex(@"^.+\W(\d+)\,(\d+) through (\d+)\,(\d+)$");

        public IEnumerable<string> Run()
        {
            yield return ResultString(Step1());
            yield return ResultString(Step2());
        }

        private int Step1()
        {
            var grid = new bool[1000, 1000];

            foreach (var command in Input)
            {
                var commandValue = ParseCommand(command, out int startX, out int startY, out int endX, out int endY);

                for (int i = startX; i <= endX; i++)
                {
                    for (int j = startY; j <= endY; j++)
                    {
                        grid[i, j] = commandValue.HasValue ? commandValue.Value : !grid[i, j];
                    }
                }
            }

            int count = 0;
            foreach (var light in grid)
                if (light)
                    count++;
            return count;
        }

        private int Step2()
        {
            var grid = new int[1000, 1000];

            foreach (var command in Input)
            {
                var commandValue = ParseCommand(command, out int startX, out int startY, out int endX, out int endY);

                for (int i = startX; i <= endX; i++)
                {
                    for (int j = startY; j <= endY; j++)
                    {
                        if (commandValue.HasValue)
                        {
                            if (commandValue.Value) // true, increase by 1
                                grid[i, j]++;
                            else                    // false, decrease by 1, minimun of 0
                                grid[i, j] = grid[i, j] - 1 <= 0 ? 0 : grid[i, j] - 1;
                        }
                        else                        // null, increment by 2
                            grid[i, j] += 2;
                    }
                }
            }

            int count = 0;
            foreach (var brightness in grid)
                count += brightness;
            return count;
        }

        private bool? ParseCommand(string command, out int startX, out int startY, out int endX, out int endY)
        {
            // determines whether the command says to turn on or off. If it says toggle, the variable is null
            bool? commandValue = command.StartsWith("turn off") ? false : (command.StartsWith("turn on") ? true : (bool?)null);

            var match = ValuesMatchRgx.Match(command);
            startX = int.Parse(match.Groups[1].Value);
            startY = int.Parse(match.Groups[2].Value);
            endX = int.Parse(match.Groups[3].Value);
            endY = int.Parse(match.Groups[4].Value);

            return commandValue;
        }
    }
}
