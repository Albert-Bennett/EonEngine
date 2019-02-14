/* Created 19/06/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Billboards
{
    /// <summary>
    /// Defines a 2D image to be rendered in 3D space. 
    /// </summary>
    public class Billboard
    {
        #region Fields

        short[] indices = new short[6];
        VertexPositionTexture[] vertices = new VertexPositionTexture[4];

        IndexBuffer ib;
        VertexBuffer vb;

        Effect effect;
        Texture2D texture;

        Vector3 position;
        float scale;
        Vector3 rot;
        Matrix rotMatrix;

        string textureFilepath;
        protected string effectFilepath;

        bool enabled = true;

        #endregion
        #region Properties

        /// <summary>
        /// Wheather or not the Billboard is enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                if (enabled)
                {
                    LODLevels lod = CameraManager.CurrentCamera.GetLODLevel(position);

                    switch (lod)
                    {
                        case LODLevels.Zero:
                        case LODLevels.One:
                            return true;

                        default:
                            return false;
                    }
                }
                else
                    return enabled;
            }
            set { enabled = value; }
        }

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

        protected Effect Effect
        {
            get { return effect; }
            set { effect = value; }
        }

        #endregion
        #region Ctor

        /// <summary>
        /// Creates a new Billboard.
        /// </summary>
        /// <param name="position">The position of the Billboard.</param>
        /// <param name="scale">The scale of the Billboard.</param>
        /// <param name="textureFilepath">The filepath for the Billboard's texture.</param>
        public Billboard(Vector3 position, float scale, Vector3 rotation, string textureFilepath)
        {
            this.position = position;
            this.scale = scale;
            this.rot = rotation;
            rotMatrix = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z);

            effectFilepath = "Eon/Shaders/Materials/Billboard";
            this.textureFilepath = textureFilepath;

            ModelManager.Add(this);
        }

        public virtual void Initialize()
        {
            texture = Common.ContentManager.Load<Texture2D>(textureFilepath);
            effect = Common.ContentManager.Load<Effect>(effectFilepath);

            Assemble();
        }

        /// <summary>
        /// Used to re-initialize the Billboard.
        /// </summary>
        /// <param name="position">The new position.</param>
        /// <param name="scale">The new scale.</param>
        /// <param name="rotation">The new rotation.</param>
        public virtual void ReInitialize(Vector3 position, float scale, Vector3 rotation)
        {
            this.position = position;
            this.scale = scale;
            this.rot = rotation;
            rotMatrix = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z);

            Assemble();
        }

        void Assemble()
        {
            vertices[0] = new VertexPositionTexture(position, new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(position, new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(position, new Vector2(1, 1));
            vertices[3] = new VertexPositionTexture(position, new Vector2(0, 1));

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            vb = new VertexBuffer(Common.Device,
                typeof(VertexPositionTexture), vertices.Length,
                BufferUsage.WriteOnly);

            vb.SetData<VertexPositionTexture>(vertices);

            ib = new IndexBuffer(Common.Device, IndexElementSize.SixteenBits,
                indices.Length, BufferUsage.WriteOnly);

            ib.SetData<short>(indices);
        }

        #endregion
        #region Rendering

        public virtual void Render()
        {
            StartRender();

            effect.Parameters["Up"].SetValue(new Vector3(0, 1, 0));
            effect.Parameters["Scale"].SetValue(scale);
            effect.Parameters["Pos"].SetValue(position);
            effect.Parameters["Rot"].SetValue(rotMatrix);

            EndRender();
        }

        protected void StartRender()
        {
            Common.Device.SetVertexBuffer(vb);
            Common.Device.Indices = ib;

            effect.Parameters["Texture"].SetValue(texture);

            effect.Parameters["ViewProj"].SetValue(CameraManager.CurrentCamera.View *
                CameraManager.CurrentCamera.Projection);
        }

        protected void EndRender()
        {
            effect.CurrentTechnique.Passes[0].Apply();

            Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, 2);

            Common.Device.SetVertexBuffer(null);
            Common.Device.Indices = null;
        }

        #endregion
        #region Helpers

        /// <summary>
        /// Moves the Billboard.
        /// </summary>
        /// <param name="position">The amount of movement to be added.</param>
        public void Move(Vector3 position)
        {
            this.position += position;
        }

        /// <summary>
        /// Rotates the Billboard.
        /// </summary>
        /// <param name="rotation">The rotation to be added.</param>
        public void Rotate(Vector3 rotation)
        {
            rot += rotation;
            rotMatrix = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z);
        }

        /// <summary>
        /// Used to resize the Billboard.
        /// </summary>
        /// <param name="scale">The value of scale to be added.</param>
        public void Resize(float scale)
        {
            this.scale += scale;
        }

        public void Destroy()
        {
            ModelManager.Remove(this);
        }

        #endregion
    }
}
