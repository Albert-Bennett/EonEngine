/* Created 12/12/2014
 * Last Updated: 07/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces.Base;
using System;

namespace Eon.Game.Components
{
    /// <summary>
    /// Used to define a Timer component.
    /// </summary>
    public sealed class TimerComponent : ObjectComponent
    {
        bool hasStarted = false;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan maxTime;

        public TimeOutEvent OnTimeUp;

        /// <summary>
        /// Has the Timer started.
        /// </summary>
        public bool HasStarted
        {
            get { return hasStarted; }
        }

        /// <summary>
        /// Creates a new TimerComponent.
        /// </summary>
        /// <param name="id">The id of the TimerComponent.</param>
        /// <param name="time">The time that is going to be monitored.</param>
        public TimerComponent(string id, int time)
            : base(id)
        {
            maxTime = TimeSpan.FromMilliseconds(time);
        }

        protected override void Update()
        {
            if (hasStarted)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime >= maxTime)
                {
                    currentTime = TimeSpan.Zero;

                    if (OnTimeUp != null)
                        OnTimeUp(ID);
                }
            }
        }

        public void SetTime(float milliseconds, bool reset)
        {
            if (reset)
                currentTime = TimeSpan.Zero;

            maxTime = TimeSpan.FromMilliseconds(milliseconds);
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            hasStarted = true;
        }

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        public void Pause()
        {
            hasStarted = false;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            hasStarted = false;
            currentTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Resets the timer.
        /// </summary>
        public void Reset()
        {
            currentTime = TimeSpan.Zero;
        }
    }
}
