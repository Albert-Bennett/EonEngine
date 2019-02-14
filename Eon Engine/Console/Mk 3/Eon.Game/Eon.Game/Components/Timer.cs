/* Created 12/12/2014
 * Last Updated: 12/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eon.Game.Components
{
    /// <summary>
    /// Used to define a Timer component.
    /// </summary>
    public sealed class Timer : ObjectComponent, IUpdate
    {
        TimeSpan currentTime;
        TimeSpan timeOut;

        int minTime;
        int maxTime;

        public TimeOutEvent OnTimeUp;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new Timer.
        /// </summary>
        /// <param name="id">The ID of the Timer.</param>
        /// <param name="minTime">The minimum time(milliseconds) it takes for the Timer to elapse.</param>
        /// <param name="maxTime">The maximum time(milliseconds) it takes for the Timer to elapse.</param>
        public Timer(string id, int minTime, int maxTime)
            : base(id)
        {
            this.minTime = minTime;
            this.maxTime = maxTime;

            GetMaxTimer();
        }

        /// <summary>
        /// Creates a new Timer.
        /// </summary>
        /// <param name="id">The ID of the Timer.</param>
        /// <param name="timeOut">The time(milliseconds) it takes for the Timer to elapse.</param>
        public Timer(string id, int timeOut) : this(id, -1, timeOut) { }

        void GetMaxTimer()
        {
            currentTime = TimeSpan.Zero;

            if (minTime > -1)
                timeOut = TimeSpan.FromMilliseconds(
                    RandomHelper.GetRandom(minTime, maxTime));
        }

        public void _Update()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= timeOut)
            {
                GetMaxTimer();

                if (OnTimeUp != null)
                    OnTimeUp();
            }
        }
    }
}
