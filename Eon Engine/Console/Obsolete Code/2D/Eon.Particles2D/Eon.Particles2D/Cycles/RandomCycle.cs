/* Created 11/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using System;

namespace Eon.Particles2D.Cycles
{
    /// <summary>
    /// Used to define a Cycle object that is partially randomized. 
    /// </summary>
    public sealed class RandomCycle : Cycle
    {
        TimeSpan minTime;
        TimeSpan maxTime;

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
            this.minTime = TimeSpan.FromMilliseconds(minTime);
            this.maxTime = TimeSpan.FromMilliseconds(maxTime);
            this.minSpawnAmount = minSpawnAmount;
            this.maxSpawnAmount = maxSpawnAmount;

            timer = TimeSpan.FromMilliseconds(RandomHelper.GetRandom(
                (float)minTime, (float)maxTime));
        }

        protected override void DetermineNext()
        {
            timer = TimeSpan.FromSeconds(RandomHelper.GetRandom(
                (float)minTime.TotalSeconds, (float)maxTime.TotalSeconds));

            base.DetermineNext();

            spawnAmount = RandomHelper.GetRandom(minSpawnAmount, maxSpawnAmount);
        }
    }
}
