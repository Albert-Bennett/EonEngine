/* Created 03/06/2013
 * Last Updated: 23/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Drawing;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines a Limb. ie an animatable part of a Skeleton.
    /// </summary>
    public class Limb : IDrawItem
    {
        string name;
        string parentLimb;

        bool enabled = true;
        bool renderDisabled = false;

        Rectangle bounds;

        Transformation initialTransform = Transformation.Identity;
        Transformation transform = Transformation.Identity;
        Transformation frameTransform = Transformation.Identity;

        Vector2 rotPoint;
        Vector2 offset;
        Vector2 size;

        int drawLayer = 0;
        bool postRender;

        Texture2D texture;
        Texture2D normalMap;
        Color colour;

        public int DrawLayer
        {
            get { return drawLayer; }
        }

        /// <summary>
        /// Render the object even if Disabled.
        /// </summary>
        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

        public string Name
        {
            get { return name; }
        }

        public bool Enabled
        {
            get { return enabled; }
            internal set { enabled = value; }
        }

        public string ParentLimb
        {
            get { return parentLimb; }
            set { parentLimb = value; }
        }

        /// <summary>
        /// The colour of the Limb.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        internal Transformation InitialTransform
        {
            get { return initialTransform; }
        }

        internal Transformation FrameTransform
        {
            get { return frameTransform; }
            set { frameTransform = value; }
        }

        protected Texture2D Texture { get { return texture; } set { texture = value; } }
        protected Texture2D NormalMap { get { return normalMap; } set { normalMap = value; } }

        /// <summary>
        /// The bounding area of the Limb.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// The offset of the Limb.
        /// </summary>
        protected Vector2 Offset
        {
            get { return offset; }
        }

        /// <summary>
        /// The rotational point of the Limb.
        /// </summary>
        protected Vector2 RotationalPoint
        {
            get { return rotPoint; }
        }

        /// <summary>
        /// The size of the Limb.
        /// </summary>
        protected Vector2 Size
        {
            get { return size; }
        }

        /// <summary>
        /// The Transformation of the Limb.
        /// </summary>
        public Transformation Transform
        {
            get { return transform; }
        }

        /// <summary>
        /// Creates a new Limb.
        /// </summary>
        /// <param name="info">The information bout the Limb.</param>
        /// <param name="postRender">Post render the Limb.</param>
        public Limb(LimbInfo info, bool postRender)
        {
            this.name = info.Name;
            this.parentLimb = info.ParentLimb;

            this.postRender = postRender;
            drawLayer = info.DrawLayer;

            texture = LoadTexture(info.TextureFilepath);
            normalMap = LoadTexture(info.NormalMapFilepath);

            rotPoint = new Vector2(info.RotationX, info.RotationY);
            offset = new Vector2(info.OffsetX, info.OffsetY);
            size = new Vector2(info.SizeX, info.SizeY);

            initialTransform = info.Transform;
            transform = initialTransform;

            colour = new Color(info.R, 
                info.G, info.B, info.A);

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        Texture2D LoadTexture(string filepath)
        {
            try
            {
                Texture2D tex = Common.ContentBuilder.Load<Texture2D>(filepath);
                return tex;
            }
            catch
            {
                new Error("Unable to load: " + filepath, Seriousness.Error);
                return new Texture2D(Common.Device, 1, 1);
            }
        }

        internal virtual void Update(Eon.Maths.Transformation parentWorld)
        {
            transform.Position *= transform.Scale;

            if (parentWorld != null)
            {
                transform.Position += new Vector2(
                    parentWorld.Position.X, parentWorld.Position.Y);

                transform.Scale *= new Vector2(
                    parentWorld.Size.X, parentWorld.Size.Y);
            }

            bounds = new Rectangle(
                (int)transform.Position.X,
                (int)transform.Position.Y,
                (int)(Size.X * transform.Scale.X),
                (int)(Size.Y * transform.Scale.Y));
        }

        public void Draw(DrawingStage stage)
        {
            _Draw(stage);
        }

        protected virtual void _Draw(DrawingStage stage)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    Common.Batch.Draw(texture, Bounds, null, Colour,
                       MathHelper.ToRadians(Transform.Rotation), RotationalPoint, SpriteEffects.None, 0);
                    break;

                case DrawingStage.Normal:
                    Common.Batch.Draw(normalMap, Bounds, null, Color.White,
                        MathHelper.ToRadians(Transform.Rotation), RotationalPoint, SpriteEffects.None, 0);
                    break;
            }
        }

        /// <summary>
        /// Destroys the Limb.
        /// </summary>
        internal void Destroy()
        {
            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);
        }

        /// <summary>
        /// Used to set the Transformation of this Limb.
        /// </summary>
        /// <param name="transform">The Transformation to use.</param>
        public void SetTransform(Transformation transform)
        {
            this.transform = transform;
        }

        /// <summary>
        /// Translates the Limb by a certain amount.
        /// </summary>
        /// <param name="translation">The amount to move the Limb by.</param>
        public void Translate(Vector2 translation)
        {
            transform.Position += translation;
        }

        public void Disable()
        {
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }

        public void ToogleEnable()
        {
            enabled = !enabled;
        }
    }
}
