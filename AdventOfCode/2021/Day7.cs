using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public class Day7 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var values = RawInput.Split(',').Select(x => int.Parse(x)).ToList();

            yield return ResultString(Step1(values));
            yield return ResultString(Step2(values));
        }

        private int Step1(List<int> values)
        {
            var median = GetMedian(values);

            return values.Select(x => Math.Abs(median - x)).Sum();
        }

        private int Step2(List<int> values)
        {
            var meanDouble = (double)values.Sum() / values.Count();

            // n == number of steps to midpoint == Math.Abs(mean - x)
            // formula to find fuel necessary is: n + ((n-1) * n)/2 // https://math.stackexchange.com/questions/593318/factorial-but-with-addition/593323
            // checking if number of steps is 1 to avoid dividing by zero
            var calculateFuelUsage = (int mean, int x) => Math.Abs(mean - x) == 1 ? 1 : Math.Abs(mean - x) + ((Math.Abs(mean - x) - 1) * Math.Abs(mean - x)) / 2;

            var mean = (int)Math.Floor(meanDouble);
            var firstFuelCount = values.Select(x => calculateFuelUsage(mean, x)).Sum();

            mean = (int)Math.Ceiling(meanDouble);
            var secondFuelCount = values.Select(x => calculateFuelUsage(mean, x)).Sum();
            return Math.Min(firstFuelCount, secondFuelCount);
        }

        private int GetMedian(List<int> values)
        {
            values.Sort();
            if (values.Count % 2 == 0) // even number of items in list, no exact center
                return (values[values.Count / 2] + values[values.Count / 2 - 1]) / 2;
            else return values[values.Count / 2];
        }
    }
}
