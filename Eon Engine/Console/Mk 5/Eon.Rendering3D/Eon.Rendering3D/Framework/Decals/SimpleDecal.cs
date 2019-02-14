/* Created: 26/03/2015
 * Last Updated: 27/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Decals
{
    /// <summary>
    /// Used to define a texture that can 
    /// be projected into world space.
    /// [Self Managed].
    /// </summary>
    public class SimpleDecal : IDecal
    {
        Matrix world;
        BoundingSphere bounds;

        Texture2D texture;

        /// <summary>
        /// The world matrix of the Decal.
        /// </summary>
        public Matrix World { get { return world; } }

        /// <summary>
        /// The bounding area of the Decal.
        /// </summary>
        public BoundingSphere Bounds { get { return bounds; } }

        /// <summary>
        /// The Decals texture.
        /// </summary>
        public Texture2D Texture { get { return texture; } }

        /// <summary>
        /// Creates a new Decal. 
        /// </summary>
        /// <param name="pos">The position of the Decal.</param>
        /// <param name="scale">The scale of the Decal.</param>
        /// <param name="rot">The rotation of the Decal.</param>
        /// <param name="textureFilepath">The texture filepath for the Decal.</param>
        public SimpleDecal(Vector3 pos, Vector3 scale,
            Vector3 rot, string textureFilepath)
        {
            world = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z) *
                 Matrix.CreateScale(scale) *
                 Matrix.CreateTranslation(pos);

            texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);

            bounds = new BoundingSphere(pos, (scale.X + scale.Y + scale.Z) / 3);

            DecalManager.Add(this);
        }

        /// <summary>
        /// Destroys the SimpleDecal.
        /// </summary>
        public void Destroy()
        {
            DecalManager.Destroy(this);
        }
    }
}
