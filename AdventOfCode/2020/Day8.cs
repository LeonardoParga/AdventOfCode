using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day8 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            List<(string, int)> commands = Input.Select(x => (x.Substring(0, 3), int.Parse(x.Substring(4)))).ToList();

            yield return ResultString(Step1(commands, out bool breaks));
            yield return ResultString(Step2(commands));
        }

        private int Step1(List<(string, int)> commands, out bool breaks)
        {
            var accumulator = 0;
            var executedLines = new List<int>();
            breaks = false;

            for (int i = 0; i < commands.Count; i++)
            {
                if (executedLines.Contains(i))
                {
                    breaks = true;
                    break;
                }
                else
                    executedLines.Add(i);

                switch (commands[i].Item1)
                {
                    case "acc":
                        accumulator += commands[i].Item2;
                        break;
                    case "jmp":
                        i = i + commands[i].Item2 - 1; // the loop itself is going to add 1
                        break;
                    default:
                        break;
                }
            }

            return accumulator;
        }

        private int Step2(List<(string, int)> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].Item1 != "jmp" && commands[i].Item1 != "nop")
                    continue;

                var commandsCopy = commands.ToList();

                var tupleToChange = commandsCopy.ElementAt(i);
                tupleToChange.Item1 = tupleToChange.Item1 == "nop" ? "jmp" : "nop";

                commandsCopy.RemoveAt(i);
                commandsCopy.Insert(i, tupleToChange);

                var num = Step1(commandsCopy, out bool breaks);
                if (breaks)
                    continue;
                else
                    return num;
            }
            return -1;
        }
    }
}
