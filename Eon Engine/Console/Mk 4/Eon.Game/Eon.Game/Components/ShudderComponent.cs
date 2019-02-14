/* Created 07/04/2015
 * Last Updated: 07/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.Game.Components
{
    /// <summary>
    /// Used to define an ObjectComponent that 
    /// can be used to show a shudder effect.
    /// </summary>
    public sealed class ShudderComponent : ObjectComponent
    {
        Vector2 defaultPos;

        float magnitude;
        bool shuddered = false;

        /// <summary>
        /// The default position of the shudder effect.
        /// </summary>
        public Vector2 DefaultPosition
        {
            get { return defaultPos; }
            set { defaultPos = value; }
        }

        /// <summary>
        /// Creates a new ShudderComponent.
        /// </summary>
        /// <param name="id">The ID of the ShudderComponent.</param>
        /// <param name="defaultPos">The default position for the effect.</param>
        public ShudderComponent(string id, Vector2 defaultPos) : this(id, defaultPos, 5.0f) { }

        /// <summary>
        /// Creates a new ShudderComponent.
        /// </summary>
        /// <param name="id">The ID of the ShudderComponent.</param>
        /// <param name="defaultPos">The default position for the effect.</param>
        /// <param name="magnitude">The magnitude of the shudder effect.</param>
        public ShudderComponent(string id, Vector2 defaultPos, float magnitude)
            : base(id)
        {
            this.defaultPos = defaultPos;
            this.magnitude = magnitude;
        }

        /// <summary>
        /// Generates the shudering effect.
        /// </summary>
        /// <returns>The calculated position.</returns>
        public Vector2 Shudder()
        {
            Vector2 shudder = defaultPos;

            if (shuddered)
                shuddered = false;
            else
            {
                shuddered = true;

                shudder += new Vector2()
                {
                    X = RandomHelper.GetRandom(-magnitude, magnitude),
                    Y = RandomHelper.GetRandom(-magnitude, magnitude)
                };
            }

            return shudder;
        }

        public void TextureQualityChanged()
        {
            magnitude = Common.GetReScaled(magnitude);
            defaultPos = Common.GetReScaled(defaultPos);
        }
    }    
}
