using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public class Day6 : Day, IChallenge
    {
        private const int spawnTimer = 6;
        private const int newFishExtraTime = 2;

        private Dictionary<(int fishTimer, int currentDay), ulong> SolvedFishDictionary = new Dictionary<(int fishTimer, int currentDay), ulong>();

        public IEnumerable<string> Run()
        {
            var initialFishes = RawInput.Split(',').Select(x => int.Parse(x)).ToList();

            yield return ResultString(GetNumberOfFishAfterDays(initialFishes, days: 80));
            yield return ResultString(GetNumberOfFishAfterDays(initialFishes, days: 256));
        }

        private ulong GetNumberOfFishAfterDays(List<int> initialFishes, int days)
        {
            ulong fishSpawned = 0;
            SolvedFishDictionary = new Dictionary<(int fishTimer, int currentDay), ulong>();

            foreach (var fishTimer in initialFishes)
                fishSpawned += GetNumOfFishesSpawnedInDays(fishTimer, days);

            return fishSpawned + (ulong)initialFishes.Count;
        }

        private ulong GetNumOfFishesSpawnedInDays(int fishTimer, int days, int currentDay = 1)
        {
            ulong numOfFishSpawnedByOriginalFish = 0;
            ulong numOfRecursiveFishesSpawned = 0;
            var originalTimer = fishTimer;
            var originalDay = currentDay;

            // check if a fish with these parameters has been solved before
            if (SolvedFishDictionary.TryGetValue((fishTimer, currentDay), out ulong totalFishes))
                return totalFishes;

            while (currentDay <= days)
            {
                var previousTimer = fishTimer;
                fishTimer = (fishTimer - 1) < 0 ? spawnTimer : fishTimer - 1;

                if (fishTimer == spawnTimer && previousTimer == 0) // if the timer resets back to the spawn timer, spawn a new fish
                {
                    numOfFishSpawnedByOriginalFish++;
                    numOfRecursiveFishesSpawned += GetNumOfFishesSpawnedInDays(spawnTimer + newFishExtraTime, days, currentDay + 1); // plus one because the day the fish spawns, the timer doesn't decrease
                }

                currentDay++;
            }

            totalFishes = numOfFishSpawnedByOriginalFish + numOfRecursiveFishesSpawned;
            SolvedFishDictionary.Add((originalTimer, originalDay), totalFishes);
            return totalFishes;
        }
    }
}
