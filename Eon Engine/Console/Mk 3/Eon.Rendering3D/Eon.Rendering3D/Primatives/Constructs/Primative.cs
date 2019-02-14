/* Created: 25/09/2014
 * Last Updated: 25/02/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.System.Interfaces;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives.Constructs
{
    /// <summary>
    /// Used to define the base class for 3D primatives.
    /// </summary>
    public abstract class Primative : IDispose
    {
        #region Variables

        protected PrimativeData data;

        BaseLOD parent;

        Texture2D texture;

        #endregion
        #region Properties

        /// <summary>
        /// The texture of the Plane.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// The parent that manages the Primative.
        /// </summary>
        public BaseLOD Parent
        {
            get { return parent; }
            internal set { parent = value; }
        }

        #endregion
        #region Ctor

        /// <summary>
        /// Creates a new Primative.
        /// </summary>
        /// <param name="texture">The texture of the Primative.</param>
        public Primative(Texture2D texture)
        {
            this.texture = texture;
        }

        /// <summary>
        /// Creates a new Primative.
        /// </summary>
        /// <param name="textureFilepath">The filepath of the texture of the Primative.</param>
        public Primative(string textureFilepath)
        {
            try
            {
                texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                new Error("The texture: " + textureFilepath + " couldn't be loaded.", Seriousness.Error);

                texture = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/DefaultTexture");
            }
        }

        protected abstract void CreateVertices();

        #endregion
        #region Rendering

        public void Render(Matrix world)
        {
            parent.Effect.CurrentTechnique = parent.Effect.Techniques["Simple"];

            parent.Effect.Parameters["World"].SetValue(world);
            parent.Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            parent.Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            parent.Effect.Parameters["ColourMap"].SetValue(texture);

            Common.Device.SetVertexBuffer(data.VertexBuffer);
            Common.Device.Indices = data.IndexBuffer;

            parent.Effect.CurrentTechnique.Passes[0].Apply();

            Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 0, data.VerticesCount, 0, data.IndexCount / 3);

            Common.Device.SetVertexBuffer(null);
            Common.Device.Indices = null;
        }

        #endregion
        #region Disposal

        public void Dispose(bool finalize)
        {
            data.Dispose(finalize);
        }

        #endregion
    }
}
