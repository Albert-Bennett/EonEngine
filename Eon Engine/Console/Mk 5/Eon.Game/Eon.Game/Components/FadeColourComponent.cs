/* Created 07/04/2015
 * Last Updated: 28/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;

namespace Eon.Game.Components
{
    /// <summary>
    /// Used to define a component that 
    /// fades a colour to transparent.
    /// </summary>
    public sealed class FadeColourComponent : ObjectComponent
    {
        Color current;
        Color colour;

        Vector4 val;
        Vector4 step;

        bool hasStarted = false;

        /// <summary>
        /// Current colour.
        /// </summary>
        public Color Colour
        {
            get { return current; }
        }

        /// <summary>
        /// The current value of the fade.
        /// </summary>
        public float CurrentValue
        {
            get
            {
                float v = current.R + current.G + current.B + current.A;

                if (v > 0)
                    return v / 4;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Has the fad started?
        /// </summary>
        public bool HasStarted
        {
            get { return hasStarted; }
        }

        /// <summary>
        /// Creates a new FadeColourComponent.
        /// </summary>
        /// <param name="id">The ID of the FadeComponent.</param>
        /// <param name="colour">The colour to fade to transparent.</param>
        /// <param name="time">The time it takes the value to get to zero.</param>
        public FadeColourComponent(string id, Color colour, float time)
            : base(id)
        {
            this.colour = colour;

            val = colour.ToVector4();

            step = val / time;
        }

        protected override void Update()
        {
            if (hasStarted)
            {
                val -= step;

                current = new Color(val);

                if (CurrentValue == 0)
                    hasStarted = false;
            }
        }

        public void Start()
        {
            hasStarted = true;

            current = colour;
            val = current.ToVector4();
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
            current = colour;
            hasStarted = false;
        }
    }
}
