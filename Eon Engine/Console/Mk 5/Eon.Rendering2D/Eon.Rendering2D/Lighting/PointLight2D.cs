/* Created: 01/06/2013
 * Last Updated: 06/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering2D.Lighting
{
    /// <summary>
    /// Defines a light that is emitted
    /// within the limits of a radius.
    /// </summary>
    public class PointLight2D : Light2D
    {
        float radius = 2;

        /// <summary>
        /// The radius of the PoitnLight.
        /// </summary>
        public float Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                World.Size = new Vector3(radius);
            }
        }

        /// <summary>
        /// Creates a new PointLight.
        /// </summary>
        /// <param name="id">The ID of the PointLight.</param>
        /// <param name="colour">The colour of the PointLight.</param>
        /// <param name="intensity">The intensity of the light.</param>
        /// <param name="specPow">The specular power of the PointLight.</param>
        /// <param name="pos">The position of the center of the PointLight.</param>
        /// <param name="radius">The radius of the PointLight.</param>
        public PointLight2D(string id, int r, int g, int b,
            float intensity, float specPow, float x, float y, float z, float radius)
            : base(id, r, g, b, intensity, specPow)
        {
            World.Position = new Vector3(x, y, z);
            World.Size = new Vector3(radius);

            this.radius = radius;
        }

        /// <summary>
        /// Creates a new PointLight.
        /// </summary>
        /// <param name="id">The ID of the PointLight.</param>
        /// <param name="colour">The colour of the PointLight.</param>
        /// <param name="intensity">The intensity of the light.</param>
        /// <param name="specPow">The specular power of the PointLight.</param>
        /// <param name="position">The position of the center of the PointLight.</param>
        /// <param name="radius">The radius of the PointLight.</param>
        public PointLight2D(string id, Vector3 colour,
            float intensity, float specPow, Vector3 position, float radius)
            : base(id, colour, intensity, specPow)
        {
            World.Position = position;
            World.Size = new Vector3(radius);

            this.radius = radius;
        }

        /// <summary>
        /// Creates a new PointLight2D from a formatted string.
        /// </summary>
        /// <param name="value">The formatted string that defines the PointLight2D.</param>
        /// <returns>The newly created PointLight2D.</returns>
        public PointLight2D FromFormattedString(string value)
        {
            string[] split = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string id = split[0];

            Vector3 colour = new Vector3()
            {
                X = int.Parse(split[1]),
                Y = int.Parse(split[2]),
                Z = int.Parse(split[3]),
            };

            float intensity = float.Parse(split[4]);
            float specPow = float.Parse(split[5]);

            Vector3 pos = new Vector3()
            {
                X = float.Parse(split[6]),
                Y = float.Parse(split[7]),
                Z = float.Parse(split[8]),
            };

            float radius = float.Parse(split[9]);

            return new PointLight2D(id, colour, intensity, specPow, pos, radius);
        }

        protected override string _GetFormattedString()
        {
            return base._GetFormattedString() + "," + World.Position.X +
                "," + World.Position.Y + "," + World.Position.Z + "," + radius;
        }
    }
}
