using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2020
{
    public class Day1 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var step1Nums = GetNumbersThatAddUpTo(2020, Input.Select(x => int.Parse(x)), 2);
            yield return ResultString(step1Nums.Aggregate(1, (x, y) => x * y));

            var step2Nums = GetNumbersThatAddUpTo(2020, Input.Select(x => int.Parse(x)), 3);
            yield return ResultString(step2Nums.Aggregate(1, (x, y) => x * y));
        }

        private List<int> GetNumbersThatAddUpTo(int targetNum, IEnumerable<int> numbers, int qtyNumbersToAdd, int startingIndex = 0)
        {
            var list = numbers.OrderBy(x => x).ToList();
            var resultList = new List<int>();

            for (int i = startingIndex; i < list.Count; i++)
            {
                if (qtyNumbersToAdd == 2)
                {
                    if (HasValidPair(i, list, targetNum, out int pair))
                    {
                        resultList.Add(list[i]);
                        resultList.Add(pair);
                        return resultList;
                    }
                }
                else
                {
                    var newTarget = targetNum - list[i];
                    var nums = GetNumbersThatAddUpTo(newTarget, list, qtyNumbersToAdd - 1, i + 1);
                    if (!nums.Any())
                        continue;
                    else if (nums.Sum() + list[i] == targetNum)
                    {
                        nums.Add(list[i]);
                        return nums;
                    }
                }
            }

            return resultList;
        }

        private bool HasValidPair(int outerIndex, List<int> list, int targetNum, out int pair)
        {
            pair = 0;
            for (int j = outerIndex + 1; j < list.Count; j++)
            {
                var sum = list[outerIndex] + list[j];
                if (sum > targetNum)
                    break;
                else if (sum == targetNum)
                {
                    pair = list[j];
                    return true;
                }
            }
            return false;
        }
    }
}
