/* Created 12/12/2014
 * Last Updated: 07/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using System;

namespace Eon.Game.Components
{
    /// <summary>
    /// Used to define a Timer component.
    /// </summary>
    public sealed class TimerComponent : ObjectComponent, IUpdate
    {
        bool hasStarted = false;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan maxTime;

        public TimeOutEvent OnTimeUp;

        public int Priority
        {
            get { return 0; }
        }

        public bool HasStarted
        {
            get { return hasStarted; }
        }

        public TimerComponent(string id, int time)
            : base(id)
        {
            maxTime = TimeSpan.FromMilliseconds(time);
        }

        public void _Update()
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

        public void _PostUpdate() { }

        public void SetTime(float milliseconds, bool reset)
        {
            if (reset)
                currentTime = TimeSpan.Zero;

            maxTime = TimeSpan.FromMilliseconds(milliseconds);
        }

        public void Start()
        {
            hasStarted = true;
        }

        public void Pause()
        {
            hasStarted = false;
        }

        public void Stop()
        {
            hasStarted = false;
            currentTime = TimeSpan.Zero;
        }

        public void Reset()
        {
            currentTime = TimeSpan.Zero;
        }
    }
}
