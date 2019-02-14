/* Created: 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Particles.Attachments.Base
{
    /// <summary>
    /// Used to define a set of properties.
    /// </summary>
    public class PropertySet
    {
        Color colour = Color.White;
        Vector3 rotation = Vector3.Zero;
        Vector3 position;

        float scale = 1.0f;

        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Matrix Transform
        {
            get
            {
                return Matrix.CreateScale(scale) *
                Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z) *
                Matrix.CreateTranslation(position);
            }
        }
    }
}
