using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2019
{
    public class Day1 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var sum = Input.Select(x => GetFuelRequired(x)).Sum();
            yield return ResultString(sum);

            var sum2 = Input.Select(x => GetTotalFuel(long.Parse(x))).Sum();
            yield return ResultString(sum2);
        }

        public long GetTotalFuel(long mass)
        {
            var fuelRequired = GetFuelRequired(mass);
            if (fuelRequired <= 0)
                return 0;
            return fuelRequired + GetTotalFuel(fuelRequired);
        }

        public long GetFuelRequired(long mass) => (long)Math.Floor(mass / 3d) - 2;

        public long GetFuelRequired(string mass) => GetFuelRequired(long.Parse(mass));
    }
}
