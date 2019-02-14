/* Created 11/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using System;

namespace Eon.Particles2D.Cycles
{
    /// <summary>
    /// Controls Particle spawning.
    /// </summary>
    public class Cycle : IUpdate
    {
        public OnSpawnEvent OnSpawn;
        public OnSpawnCompleteEvent OnComplete;

        TimeSpan currentTime = TimeSpan.Zero;
        protected TimeSpan timer;

        int currentAmount;
        int totalSpawn;
        protected int spawnAmount;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The amount of objects to be spawned.
        /// </summary>
        public int SpawnAmount
        {
            get { return spawnAmount; }
        }

        /// <summary>
        /// Creates a new Cycle.
        /// </summary>
        /// <param name="maxTime">The maximum amount of time between spawning.</param>
        /// <param name="totalToBeSpawned">The total amonunt of objects to be spawned.</param>
        /// <param name="spawnAmountPerCycle">The amount of objects to spawned per cycle.</param>
        public Cycle(float maxTime,
            int totalToBeSpawned, int spawnAmountPerCycle)
        {
            this.timer = TimeSpan.FromSeconds(maxTime);
            totalSpawn = totalToBeSpawned;
            spawnAmount = spawnAmountPerCycle;
        }

        public void _Update()
        {
            Update();
        }

        protected virtual void Update()
        {
            if (totalSpawn > currentAmount && totalSpawn != -1)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime > timer)
                {
                    currentTime = TimeSpan.Zero;

                    DetermineNext();

                    if (OnSpawn != null)
                        OnSpawn();
                }
            }
        }

        protected virtual void DetermineNext()
        {
            if (totalSpawn != -1)
            {
                if (spawnAmount > totalSpawn - currentAmount)
                    spawnAmount = totalSpawn - currentAmount;

                currentAmount += spawnAmount;

                if (currentAmount >= totalSpawn)
                    if (OnComplete != null)
                        OnComplete();
            }
        }
    }
}
