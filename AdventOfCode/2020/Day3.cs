using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day3 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var resultStep1 = Step1();
            yield return ResultString(resultStep1);
            yield return ResultString(Step2() * resultStep1);
        }

        private long Step1()
        {
            return CalculateSlope(3, 1);
        }

        private long Step2()
        {
            return CalculateSlope(1, 1) * CalculateSlope(5, 1) * CalculateSlope(7, 1) * CalculateSlope(1, 2);
        }

        private long CalculateSlope(int movesRight, int movesDown)
        {
            var currentHorizontalPosition = 0;
            var treeCount = 0L;

            for (int i = movesDown; i < Input.Length; i += movesDown)
            {
                currentHorizontalPosition = currentHorizontalPosition + movesRight;
                if (currentHorizontalPosition >= Input[i].Length)
                    currentHorizontalPosition = (currentHorizontalPosition - Input[i].Length);

                var nextPosition = Input[i][currentHorizontalPosition];

                if (nextPosition == '#')
                    treeCount++;
            }

            return treeCount;
        }
    }
}
