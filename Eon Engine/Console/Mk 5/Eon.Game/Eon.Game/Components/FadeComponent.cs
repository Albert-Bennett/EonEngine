/* Created 07/04/2015
 * Last Updated: 07/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces.Base;

namespace Eon.Game.Components
{
    /// <summary>
    /// Used to define a component that 
    /// fade a value to zero.
    /// </summary>
    public sealed class FadeComponent : ObjectComponent
    {
        float step;

        float current = 0;
        float maxValue;

        bool hasStarted = false;

        /// <summary>
        /// The current fade value.
        /// </summary>
        public float CurrentValue
        {
            get { return current; }
        }

        /// <summary>
        /// Has the fade started.
        /// </summary>
        public bool HasStarted
        {
            get { return hasStarted; }
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

        protected override void Update()
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

        /// <summary>
        /// Starts the fade effect.
        /// </summary>
        public void Start()
        {
            hasStarted = true;
            current = maxValue;
        }

        /// <summary>
        /// Pauses / resumes the fade effect.
        /// </summary>
        public void PauseResume()
        {
            if (!hasStarted)
                hasStarted = true;
            else
                hasStarted = false;
        }

        /// <summary>
        /// Ends the fade effect.
        /// </summary>
        public void Stop()
        {
            current = maxValue;
            hasStarted = false;
        }
    }
}
