/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.EngineComponents;
using Eon.Helpers;
using Eon.Interfaces;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Animation2D.SpriteSheet
{
    /// <summary>
    /// Defines an animated sprite.
    /// </summary>
    public sealed class AnimatedSprite : ObjectComponent, IDrawItem, IUpdate, IDispose
    {
        #region Graphical Variables

        Texture2D texture;

        Rectangle rect;
        Vector2 offset = Vector2.Zero;
        Vector2 scale = Vector2.One;
        SpriteEffects spriteEffect;

        Color colour;
        float rotation;

        int drawLayer = -1;

        #endregion
        #region Animation Variables

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan framerate;

        AnimationStates currentState = AnimationStates.Paused;

        int totalFrames;

        int maxColumns;
        int maxRows;

        int currRow = 0;
        int currCol = 0;

        int xAmount, yAmount;

        Rectangle sourceRect;

        #endregion
        #region Misc Variables

        GameStates precidence = GameStates.Game;

        string textureFilepath;

        bool postRender;

        public GameStates Precidence
        {
            get { return precidence; }
            set { precidence = value; }
        }

        #endregion
        #region Graphical Fields

        public int DrawLayer
        {
            get
            {
                if (drawLayer == -1)
                    if (postRender && PostRenderManager.MaximumLayer > 0)
                        return PostRenderManager.MaximumLayer;
                    else if (DrawingManager.MaximumLayer > 0)
                        return DrawingManager.MaximumLayer;
                    else
                        return 0;
                else
                    return drawLayer;
            }
        }

        public Vector2 OffSet
        {
            get { return offset; }
            set { offset = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public SpriteEffects SpriteEffect
        {
            get { return spriteEffect; }
            set { spriteEffect = value; }
        }

        public Color Colour
        {
            get { return colour; }
        }

        public Rectangle Rect
        {
            get { return rect; }
        }

        #endregion
        #region Animation Fields

        public HasFinishedEvent OnFinished;

        public AnimationStates CurrentState
        {
            get { return currentState; }
        }

        /// <summary>
        /// Update order priority.
        /// </summary>
        public int Priority
        {
            get { return 1; }
        }

        #endregion
        #region Ctors

        public AnimatedSprite(string id, int drawLayer, string textureFilepath,
            Color colour, float framerate, int totalFrames, int columns, bool postRender,
            float rotation, Vector2 scale, Vector2 offset)
            : base(id)
        {
            this.textureFilepath = textureFilepath;

            maxColumns = columns;

            this.drawLayer = drawLayer;

            this.colour = colour;
            this.rotation = rotation;

            if (Common.PreviousScreenResolution != Vector2.One)
                ScreenResolutionChanged();

            this.scale = scale;
            this.offset = offset;

            spriteEffect = SpriteEffects.None;

            this.framerate = TimeSpan.FromSeconds(1 / framerate);
            this.totalFrames = totalFrames;

            maxRows = 0;

            while (totalFrames > 0)
            {
                maxRows++;
                totalFrames -= columns;
            }

            this.postRender = postRender;
        }

        public AnimatedSprite(string id, int drawLayer, string textureFilepath,
            Color colour, float framerate, int totalFrames, int columns, bool postRender)
            : this(id, drawLayer, textureFilepath, colour, framerate, totalFrames, columns, postRender,
             0, Vector2.One, Vector2.Zero) { }

        protected override void Initialize()
        {
            try
            {
                texture = Common.ContentManager.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                throw new NullReferenceException(textureFilepath);
            }

            xAmount = texture.Width / maxColumns;
            yAmount = texture.Height / maxRows;

            sourceRect = new Rectangle(0, 0, xAmount, yAmount);

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);

            base.Initialize();
        }

        #endregion
        #region Animation Methods

        public void _Update()
        {
            if (GameStateManager.CurrentState == Precidence)
            {
                rect = new Rectangle((int)offset.X,
                    (int)offset.Y, (int)scale.X, (int)scale.Y);

                if (Owner != null)
                {
                    rect.X += (int)Owner.World.Translation.X;
                    rect.Y += (int)Owner.World.Translation.Y;

                    if (scale == Vector2.One)
                    {
                        Vector3 pscale = Vector3.Zero;
                        Helpers.EonMathHelper.GetMatrixScale(Owner.World, out pscale);

                        rect.Width *= (int)pscale.X;
                        rect.Height *= (int)pscale.Y;
                    }
                }

                if (currentState == AnimationStates.Playing)
                    if (currentTime >= framerate)
                        ChangeFrame();
                    else
                        currentTime += Common.ElapsedTimeDelta;
            }
        }

        void ChangeFrame()
        {
            currentTime = TimeSpan.Zero;

            currCol++;

            if (currCol >= maxColumns)
            {
                currCol = 0;
                currRow++;

                if (currRow >= maxRows)
                {
                    currRow = 0;
                    Stop();
                }
            }

            sourceRect.X = currCol * xAmount;
            sourceRect.Y = currRow * yAmount;
        }

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <param name="randomStart">Weather or not the animation should 
        /// begin playing at a random frame.</param>
        public void Play(bool randomStart)
        {
            currentState = AnimationStates.Playing;

            currentTime = TimeSpan.Zero;

            if (randomStart)
            {
                currRow = RandomHelper.GetRandomInt(0, maxRows);

                int cols = (totalFrames - (currRow * maxColumns)) % maxColumns;

                currCol = RandomHelper.GetRandomInt(0, cols);

                sourceRect.X = currCol * xAmount;
                sourceRect.Y = currRow * yAmount;
            }
            else
            {
                sourceRect.X = 0;
                sourceRect.Y = 0;
            }
        }

        /// <summary>
        /// Pauses or resumes the animation.
        /// </summary>
        public void PauseResume()
        {
            if (currentState == AnimationStates.Playing)
                currentState = AnimationStates.Paused;
            else
                currentState = AnimationStates.Playing;
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            currentState = AnimationStates.Stopped;

            if (OnFinished != null)
                OnFinished(ID);
        }

        #endregion
        #region Misc

        public void Draw()
        {
                    Common.Batch.Draw(texture, rect, sourceRect, colour,
                        rotation, GetTextureCenter(), spriteEffect, 0);
        }

        public void Dispose(bool finalize)
        {
            if (texture != null)
            {
                if (finalize)
                    texture.Dispose();

                texture = null;
            }
        }

        public void SetColour(Color colour)
        {
            this.colour = colour;
        }

        Vector2 GetTextureCenter()
        {
            return new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        public void ScreenResolutionChanged()
        {
            scale = Common.ReCalibrateScreenSpaceVector(scale);
            offset = Common.ReCalibrateScreenSpaceVector(offset);
        }

        public override void Destroy(bool remove)
        {
            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);

            base.Destroy(remove);
        }

        #endregion
    }
}
