/* Created: 01/06/2013
 * Last Updated: 16/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering2D.Lighting
{
    /// <summary>
    /// Used to define a light that appers to 
    /// be emmitting light from everywhere.
    /// </summary>
    public class DominateLight2D : Light2D
    {
        Vector3 direct = Vector3.Zero;

        /// <summary>
        /// The direction of the DominateLight.
        /// </summary>
        public Vector3 Direction
        {
            get { return direct; }
            set { direct = Vector3.Normalize(value); }
        }

        /// <summary>
        /// Creates a new DominateLight.
        /// </summary>
        /// <param name="id">The ID of the DominateLight.</param>
        /// <param name="colour">The colour of the DominateLight.</param>
        /// <param name="intensity">The intensity of the light.</param>
        /// <param name="specPow">The specular power of the light.</param>
        /// <param name="direction">The direction of the light.</param>
        public DominateLight2D(string id, int r, int g, int b,
            float intensity, float specPow, float directX, float directY, float directZ)
            : base(id, r, g, b, intensity, specPow)
        {
            direct = new Vector3(directX, directY, directZ);
            direct.Normalize();
        }

        /// <summary>
        /// Creates a new DominateLight.
        /// </summary>
        /// <param name="id">The ID of the DominateLight.</param>
        /// <param name="colour">The colour of the DominateLight.</param>
        /// <param name="intensity">The intensity of the light.</param>
        /// <param name="specPow">The specular power of the light.</param>
        /// <param name="direction">The direction of the light.</param>
        public DominateLight2D(string id, Vector3 colour,
            float intensity, float specPow, Vector3 direction)
            : base(id,colour, intensity, specPow)
        {
            direct = direction;
            direct.Normalize();
        }

        /// <summary>
        /// Creates a new DominateLight2D from a formatted string.
        /// </summary>
        /// <param name="value">The formatted string that defines the PointLight2D.</param>
        /// <returns>The newly created DominateLight2D.</returns>
        public DominateLight2D FromFormattedString(string value)
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

            Vector3 direct = new Vector3()
            {
                X = float.Parse(split[6]),
                Y = float.Parse(split[7]),
                Z = float.Parse(split[8]),
            };

            return new DominateLight2D(id, colour, intensity, specPow, direct);
        }

        protected override string _GetFormattedString()
        {
            return base._GetFormattedString() + "," + direct.X +
                "," + direct.Y + "," + direct.Z;
        }
    }
}
