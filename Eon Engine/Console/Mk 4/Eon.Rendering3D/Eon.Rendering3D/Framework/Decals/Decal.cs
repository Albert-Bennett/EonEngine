/* Created: 29/03/2015
 * Last Updated: 29/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Decals
{
    /// <summary>
    /// Used to define a texture that can be projected
    /// into world space with normals and specular data.
    /// [Self Managed].
    /// </summary>
    public class Decal : IDecal
    {
        Matrix world;
        BoundingSphere bounds;

        Texture2D texture;
        Texture2D normalMap;
        Texture2D specularMap;

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
        /// The Decal's normal map.
        /// </summary>
        public Texture2D NormalMap { get { return normalMap; } }

        /// <summary>
        /// The Decal's specular map.
        /// </summary>
        public Texture2D SpecularMap { get { return specularMap; } }

        /// <summary>
        /// Creates a new Decal. 
        /// </summary>
        /// <param name="pos">The position of the Decal.</param>
        /// <param name="scale">The scale of the Decal.</param>
        /// <param name="rot">The rotation of the Decal.</param>
        /// <param name="textureFilepath">The texture filepath for the Decal.</param>
        /// <param name="normalMapFilepath">The normal map filepath for the Decal.</param>
        /// <param name="specularMapFilepath">The specular map filepath for the Decal.</param>
        public Decal(Vector3 pos, Vector3 scale,
            Vector3 rot, string textureFilepath,
            string normalMapFilepath, string specularMapFilepath)
        {
            world = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z) *
                 Matrix.CreateScale(scale) *
                 Matrix.CreateTranslation(pos);

            texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            normalMap = Common.ContentBuilder.Load<Texture2D>(normalMapFilepath);
            specularMap = Common.ContentBuilder.Load<Texture2D>(specularMapFilepath);

            bounds = new BoundingSphere(pos, (scale.X + scale.Y + scale.Z) / 3);

            DecalManager.Add(this);
        }

        /// <summary>
        /// Destroys the Decal.
        /// </summary>
        public void Destroy()
        {
            DecalManager.Destroy(this);
        }
    }
}
