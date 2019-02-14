/* Created 07/04/2015
 * Last Updated: 07/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;

namespace Eon.Game.Components
{
    /// <summary>
    /// Used to define a component that 
    /// fade a value to zero.
    /// </summary>
    public sealed class FadeComponent : ObjectComponent, IUpdate
    {
        float step;

        float current = 0;
        float maxValue;

        bool hasStarted = false;

        public float CurrentValue
        {
            get { return current; }
        }

        public bool HasStarted
        {
            get { return hasStarted; }
        }

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new FadeComponent.
        /// </summary>
        /// <param name="id">The ID of the FadeComponent.</param>
        /// <param name="maxValue">Value to start the fade at.</param>
        /// <param name="time">The time it takes the value to get to zero.</param>
        public FadeComponent(string id, float maxValue, float time)
            : base(id)
        {
            this.maxValue = maxValue;

            current = maxValue;
            step = maxValue / time;
        }

        public void _Update()
        {
            if (hasStarted)
                if (current != 0)
                {
                    current -= step;

                    if (current < 0)
                    {
                        current = 0;

                        hasStarted = false;
                    }
                }
        }

        public void _PostUpdate() { }

        public void Start()
        {
            hasStarted = true;
            current = maxValue;
        }

        public void PauseResume()
        {
            if (!hasStarted)
                hasStarted = true;
            else
                hasStarted = false;
        }

        public void Stop()
        {
            current = maxValue;
            hasStarted = false;
        }
    }
}
