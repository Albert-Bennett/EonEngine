/* Created 11/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;

namespace Eon.Particles.Cycles
{
    /// <summary>
    /// Controls Particle spawning.
    /// </summary>
    public class IntervalCycle
    {
        public OnSpawnEvent OnSpawn;
        public OnSpawningCompleteEvent OnCompleted;

        TimeSpan currentTime = TimeSpan.Zero;
        protected TimeSpan timer;

        int currentAmount = 0;
        int totalSpawn;

        int spawnAmount;

        protected int TotalSpawn
        {
            get { return totalSpawn; }
        }

        protected int CurrentlySpawned
        {
            get { return currentAmount; }
            set { currentAmount = value; }
        }

        /// <summary>
        /// Creates a new Cycle.
        /// </summary>
        /// <param name="maxTime">The maximum amount of time between spawning.</param>
        /// <param name="totalToBeSpawned">The total amonunt of objects to be spawned.</param>
        /// <param name="spawnAmountPerCycle">The amount of objects to spawned per cycle.</param>
        public IntervalCycle(float maxTime,
            int totalToBeSpawned, int spawnAmountPerCycle)
        {
            this.timer = TimeSpan.FromMilliseconds(maxTime);

            totalSpawn = totalToBeSpawned;
            spawnAmount = spawnAmountPerCycle;
        }

        public void Update()
        {
            if (currentAmount < totalSpawn)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime > timer)
                {
                    currentTime = TimeSpan.Zero;

                    if (OnSpawn != null)
                        OnSpawn(Next());
                }
            }
            else
                if (OnCompleted != null)
                    OnCompleted();
        }

        protected virtual int Next()
        {
            int amount = spawnAmount;

            if (currentAmount + amount > totalSpawn)
            {
                amount = totalSpawn - currentAmount;
                currentAmount = totalSpawn;
            }
            else
                currentAmount += spawnAmount;

            return amount;
        }

        public void Reset()
        {
            currentAmount = 0;
            currentTime = TimeSpan.Zero;
        }
    }
}
