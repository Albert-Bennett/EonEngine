/* Created 11/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using System;

namespace Eon.Particles.Cycles
{
    /// <summary>
    /// Used to define a Cycle object that is partially randomized. 
    /// </summary>
    public sealed class RandomCycle : IntervalCycle
    {
        float minTime;
        float maxTime;

        int maxSpawnAmount;
        int minSpawnAmount;

        /// <summary>
        /// Creates a new RandomCycle.
        /// </summary>
        /// <param name="minTime">The minimum amount of time between spawning.</param>
        /// <param name="maxTime">The maximum amount of time between spawning.</param>
        /// <param name="totalToBeSpawned">The total amonunt of objects to be spawned.</param>
        /// <param name="minSpawnAmount">The minimum amount of objects to spawned per cycle.</param>
        /// <param name="maxSpawnAmount">The maximum amount of objects to spawned per cycle.</param>
        public RandomCycle(float minTime, float maxTime,
            int totalToBeSpawned, int minSpawnAmount, int maxSpawnAmount)
            : base(0, totalToBeSpawned, 0)
        {
            this.minTime = minTime;
            this.maxTime = maxTime;

            this.minSpawnAmount = minSpawnAmount;
            this.maxSpawnAmount = maxSpawnAmount;

            timer = TimeSpan.FromMilliseconds(
                RandomHelper.GetRandom(minTime, maxTime));
        }

        protected override int Next()
        {
            timer = TimeSpan.FromMilliseconds(
                RandomHelper.GetRandom(minTime, maxTime));         

           int spawnAmount = RandomHelper.GetRandom(minSpawnAmount, maxSpawnAmount);

           if (CurrentlySpawned + spawnAmount > TotalSpawn)
           {
               spawnAmount = TotalSpawn - CurrentlySpawned;
               CurrentlySpawned = TotalSpawn;
           }
           else
               CurrentlySpawned += spawnAmount;

           return spawnAmount;
        }
    }
}
