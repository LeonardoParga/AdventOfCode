using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public abstract class Day
    {
        protected string[] Input { get; set; }
        protected int CurrentStep { get; set; }

        public Day()
        {
            var split = this.GetType().Namespace.Split("_");
            var year = int.Parse(split.Last());

            this.Input = FileHandler.ReadFile(this.GetType().Name, year);
        }

        protected string ResultString(object result)
        {
            CurrentStep++;
            return $"Step {CurrentStep} - {result}";
        }
    }
}
