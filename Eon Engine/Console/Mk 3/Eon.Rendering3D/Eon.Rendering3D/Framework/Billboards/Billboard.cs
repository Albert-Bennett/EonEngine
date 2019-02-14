/* Created: 19/06/2014
 * Last Updated: 15/03/2015
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
    public class Billboard : IRenderable3D
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

        RasterizerState rs = new RasterizerState()
        {
            CullMode = CullMode.None
        };

        BoundingSphere sphere;
        float alphaBias = 1.0f;

        RenderTypes renderType = RenderTypes.SemiLPP;

        string textureFilepath;
        protected string effectFilepath;

        bool enabled = true;

        #endregion
        #region Properties

        public RenderTypes RenderType
        {
            get { return renderType; }
            protected set { renderType = value; }
        }

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
        /// Is the ModelComponent visable to the current camera.
        /// </summary>
        public bool IsSeen
        {
            get
            {
                return CameraManager.CurrentCamera.IsInView(sphere);
            }
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

        /// <summary>
        /// The threshold for how transparent 
        /// the Billboard can be when rendered.
        /// </summary>
        public float AlphaBias
        {
            get { return alphaBias; }
            set 
            {
                alphaBias = MathHelper.Clamp(
                    value, 0.0f, 1.0f); 
            }
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

        protected Effect Effect
        {
            get { return effect; }
            set { effect = value; }
        }

        protected VertexBuffer VertexBuffer
        {
            get { return vb; }
        }

        protected IndexBuffer IndexBuffer
        {
            get { return ib; }
        }

        #endregion
        #region Ctor

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

            effectFilepath = "Eon/Shaders/Materials/Billboard";
            this.textureFilepath = textureFilepath;

            Initialize();

            ModelManager.Add(this);
        }

        public virtual void Initialize()
        {
            texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            effect = Common.ContentBuilder.Load<Effect>(effectFilepath);

            sphere = new BoundingSphere(position, scale);

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

        public void _Render(string technique)
        {
            RasterizerState prev = Common.Device.RasterizerState;
            Common.Device.RasterizerState = rs;

            Render(technique);

            Common.Device.RasterizerState = prev;
        }

        protected virtual void Render(string technique)
        {
            StartRender();

            effect.Parameters["Up"].SetValue(new Vector3(0, 1, 0));
            effect.Parameters["Scale"].SetValue(scale);
            effect.Parameters["Pos"].SetValue(position);
            effect.Parameters["Rot"].SetValue(rotMatrix);

            effect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);

            EndRender();
        }

        protected void StartRender()
        {
            Common.Device.SetVertexBuffer(vb);
            Common.Device.Indices = ib;

            effect.Parameters["Texture"].SetValue(texture);

            Effect.Parameters["World"].SetValue(World);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            Effect.Parameters["AlphaBias"].SetValue(AlphaBias);
        }

        protected void EndRender()
        {
            effect.CurrentTechnique.Passes[0].Apply();

            Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 0, vertices.Length, 0, 2);

            Common.Device.SetVertexBuffer(null);
            Common.Device.Indices = null;
        }

        #endregion
        #region Helpers

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="image">The parameter's value.</param>
        public void SetParameter(string parameterName, Texture2D image)
        {
            try
            {
                if (effect.Parameters[parameterName] != null)
                    effect.Parameters[parameterName].SetValue(image);
            }
            catch { }
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, float value)
        {
            try
            {
                if (effect.Parameters[parameterName] != null)
                    effect.Parameters[parameterName].SetValue(value);
            }
            catch { }
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, Vector2 value)
        {
            try
            {
                if (effect.Parameters[parameterName] != null)
                    effect.Parameters[parameterName].SetValue(value);
            }
            catch { }
        }

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

            sphere = new BoundingSphere(position, scale);
        }

        public void Destroy()
        {
            ModelManager.Remove(this);
        }

        #endregion
    }
}
