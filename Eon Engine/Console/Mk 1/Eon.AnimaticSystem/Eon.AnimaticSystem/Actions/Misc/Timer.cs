/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using System;

namespace Eon.AnimaticSystem.Actions.Misc
{
    /// <summary>
    /// Defines a timer.
    /// </summary>
    public sealed class Timer : Action
    {
        TimeSpan currentTime;
        TimeSpan maxTime;

        /// <summary>
        /// Creates a new Timer.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="time">The lenght of time 
        /// the timer is set for.</param>
        public Timer(string id, int streamNumber, TimeSpan time)
            : base(id, streamNumber)
        {
            maxTime = time;
        }

        public override void Execute()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= maxTime)
                FinishExecution();
        }
    }
}
