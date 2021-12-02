using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public class Day1 : Day, IChallenge
    {
        private List<int> _depthMeasurements;

        public Day1()
        {
            _depthMeasurements = Input.Select(x => int.Parse(x)).ToList();
        }

        public IEnumerable<string> Run()
        {
            yield return ResultString(GetHowManyTimesDepthIncreases());
            yield return ResultString(GetHowManyTimesDepthIncreasesBySlidingWindows());
        }

        private int GetHowManyTimesDepthIncreases()
        {
            var count = 0;

            for (int i = 1; i < _depthMeasurements.Count; i++)
                count += _depthMeasurements[i] > _depthMeasurements[i - 1] ? 1 : 0;

            return count;
        }

        private int GetHowManyTimesDepthIncreasesBySlidingWindows()
        {
            var count = 0;
            var slidingWindows = CreateSlidingWindows(3);

            for (int i = 1; i < slidingWindows.Count; i++)
                count += slidingWindows[i] > slidingWindows[i - 1] ? 1 : 0;

            return count;
        }

        private List<SlidingWindow> CreateSlidingWindows(int measurementsPerWindow)
        {
            var windows = new List<SlidingWindow>();
            var nextAvailableWindow = 0;

            foreach (var measurement in _depthMeasurements)
            {
                for (int i = 0; i < measurementsPerWindow; i++)
                {
                    // going back to add measurements to previous windows
                    var currentWindowIndex = nextAvailableWindow - i < 0 ? nextAvailableWindow : nextAvailableWindow - i;

                    // if there are no previous windows, no need to continue
                    // if the current window wouldn't have enough measurements to create a full window, there's no reason to create it
                    if (i > 0 && currentWindowIndex == nextAvailableWindow ||
                        currentWindowIndex > _depthMeasurements.Count - measurementsPerWindow)
                        continue;

                    if (windows.Count <= currentWindowIndex)
                        windows.Add(new SlidingWindow());

                    windows[currentWindowIndex].Add(measurement);
                }

                nextAvailableWindow++;
            }

            return windows;
        }

        private class SlidingWindow
        {
            public SlidingWindow()
            {
                Measurements = new List<int>();
            }

            public List<int> Measurements { get; set; }

            public int Sum => Measurements.Sum();

            public void Add(int num) => Measurements.Add(num);

            public static bool operator <(SlidingWindow l, SlidingWindow r) => l.Sum < r.Sum;

            public static bool operator >(SlidingWindow l, SlidingWindow r) => l.Sum > r.Sum;
        }
    }
}
