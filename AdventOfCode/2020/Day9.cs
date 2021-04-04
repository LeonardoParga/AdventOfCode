using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day9 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var preambleLength = 25;
            var numbers = Input.Select(x => ulong.Parse(x)).ToList();

            var step1Result = Step1(numbers, preambleLength);

            yield return ResultString(step1Result);
            yield return ResultString(Step2(numbers, step1Result));
        }

        private ulong Step1(List<ulong> numbers, int preambleLength)
        {
            var skipCount = 0;

            for (int i = preambleLength; i < numbers.Count; i++)
            {
                var numsPreamble = numbers.Skip(skipCount).Take(preambleLength).ToList();

                if (ListContainsValidSum(numsPreamble, numbers[i]))
                    skipCount++;
                else
                    return numbers[i];
            }

            return 0;
        }

        private bool ListContainsValidSum(List<ulong> list, ulong targetNum)
        {
            foreach (var num in list)
                if (list.Any(x => x + num == targetNum && x != num))
                    return true;
            return false;
        }

        private ulong Step2(List<ulong> numbers, ulong targetNum)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                var sum = numbers[i];
                var j = i + 1;
                var smallest = numbers[i];
                var largest = numbers[i];

                while (sum < targetNum)
                {
                    if (numbers[j] < smallest)
                        smallest = numbers[j];
                    if (numbers[j] > largest)
                        largest = numbers[j];

                    sum += numbers[j];

                    if (sum == targetNum)
                        return smallest + largest;

                    j++;
                }
            }

            return 0;
        }
    }
}
