/* Created: 19/06/2014
 * Last Updated: 29/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Billboards
{
    /// <summary>
    /// Defines a 2D image to be rendered in 3D space. 
    /// </summary>
    public class Billboard : IBillboard
    {
        Texture2D texture;

        Vector3 position;
        float scale;
        Vector3 rot;
        Matrix rotMatrix;

        /// <summary>
        /// The scale of the Billboard.
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// The position of the Billboard.
        /// </summary>
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// The rotation of the Billboard.
        /// </summary>
        public Vector3 Rotation
        {
            get { return rot; }
            set
            {
                rot = value;

                rotMatrix = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z);
            }
        }

        /// <summary>
        /// The Billboard's rotation as a matrix.
        /// </summary>
        public Matrix RotationMatrix
        {
            get { return rotMatrix; }
        }

        /// <summary>
        /// The world matrix of the Billboard.
        /// </summary>
        public Matrix World
        {
            get
            {
                return Matrix.CreateScale(scale) *
                    rotMatrix * Matrix.CreateTranslation(position);
            }
        }

        /// <summary>
        /// The texture of the Billboard.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Creates a new Billboard.
        /// </summary>
        /// <param name="position">The position of the Billboard.</param>
        /// <param name="scale">The scale of the Billboard.</param>
        /// <param name="rotation">The rotation of the Billboard.</param>
        /// <param name="textureFilepath">The filepath for the Billboard's texture.</param>
        public Billboard(Vector3 position, float scale, Vector3 rotation, string textureFilepath)
        {
            this.position = position;
            this.scale = scale;
            this.rot = rotation;
            rotMatrix = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z);

            texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);


            BillboardManager.Add(this);
        }

        public void Destroy()
        {
            BillboardManager.Destroy(this);
        }
    }
}
