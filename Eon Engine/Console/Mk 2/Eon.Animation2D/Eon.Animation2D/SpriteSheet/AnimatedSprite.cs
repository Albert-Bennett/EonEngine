/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Eon.System.States;
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
        Texture2D normalMap;
        Texture2D distortion;

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

        MediaStates currentState = MediaStates.Paused;

        int totalFrames;
        int currentFrame = 0;

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
        string normalMapFilepath;
        string distortionMapFilepath;

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

        public MediaStates CurrentState
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
            string normalMapFilepath, string distortionMapFilepath, float rotation, Vector2 scale, Vector2 offset)
            : base(id)
        {
            this.textureFilepath = textureFilepath;
            this.normalMapFilepath = normalMapFilepath;
            this.distortionMapFilepath = distortionMapFilepath;

            maxColumns = columns;

            this.drawLayer = drawLayer;

            this.colour = colour;
            this.rotation = rotation;

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
            Color colour, float framerate, int totalFrames, int columns, bool postRender,
            float rotation, Vector2 scale, Vector2 offset) :
            this(id, drawLayer, textureFilepath, colour, framerate, totalFrames, columns, postRender,
             "Eon/Textures/DefaultNormalMap", "Eon/Textures/DefaultDistortionMap", rotation, scale, offset) { }

        public AnimatedSprite(string id, int drawLayer, string textureFilepath,
            Color colour, float framerate, int totalFrames, int columns, bool postRender)
            : this(id, drawLayer, textureFilepath, colour, framerate, totalFrames, columns, postRender,
            "Eon/Textures/DefaultNormalMap", "Eon/Textures/DefaultDistortionMap", 0, Vector2.One, Vector2.Zero) { }

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

            try
            {
                normalMap = Common.ContentManager.Load<Texture2D>(normalMapFilepath);
            }
            catch
            {
                throw new NullReferenceException(normalMapFilepath);
            }

            try
            {
                distortion = Common.ContentManager.Load<Texture2D>(distortionMapFilepath);
            }
            catch
            {
                throw new NullReferenceException(distortionMapFilepath);
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
            if (GameStateManager.CurrentState == Precidence && currentState != MediaStates.Stopped)
            {
                rect = new Rectangle((int)offset.X,
                    (int)offset.Y, (int)scale.X, (int)scale.Y);

                if (Owner != null)
                {
                    rect.X += (int)Owner.World.Translation.X;
                    rect.Y += (int)Owner.World.Translation.Y;

                    rect.Width *= (int)Owner.World.Scale.X;
                    rect.Height *= (int)Owner.World.Scale.Y;
                }

                if (currentState == MediaStates.Playing)
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
            currentFrame++;

            if (currentFrame >= totalFrames)
            {
                currRow = 0;
                currCol = 0;
                currentFrame = 0;

                Stop();
            }
            else if (currCol >= maxColumns)
            {
                currCol = 0;
                currRow++;
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
            currentState = MediaStates.Playing;

            currentTime = TimeSpan.Zero;

            if (randomStart)
            {
                currRow = RandomHelper.GetRandom(0, maxRows);

                int cols = (totalFrames - (currRow * maxColumns)) % maxColumns;

                currCol = RandomHelper.GetRandom(0, cols);

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
            if (currentState == MediaStates.Playing)
                currentState = MediaStates.Paused;
            else
                currentState = MediaStates.Playing;
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            currentState = MediaStates.Stopped;

            if (OnFinished != null)
                OnFinished(ID);
        }

        #endregion
        #region Misc

        public void Draw(DrawingStage stage)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    if (rotation != 0)
                        Common.Batch.Draw(texture, rect, sourceRect, colour,
                            rotation, GetTextureCenter(), spriteEffect, 0);
                    else
                        Common.Batch.Draw(texture, rect, sourceRect, colour,
                            0, Vector2.Zero, spriteEffect, 0);
                    break;

                case DrawingStage.Distortion:
                    if (rotation != 0)
                        Common.Batch.Draw(distortion, rect, sourceRect,
                            Color.White, rotation, GetTextureCenter(), spriteEffect, 0);
                    else
                        Common.Batch.Draw(distortion, rect, sourceRect,
                            Color.White, 0, Vector2.Zero, spriteEffect, 0);
                    break;

                default:
                    if (rotation != 0)
                        Common.Batch.Draw(normalMap, rect, sourceRect,
                            Color.White, rotation, GetTextureCenter(), spriteEffect, 0);
                    else
                        Common.Batch.Draw(normalMap, rect, sourceRect,
                            Color.White, 0, Vector2.Zero, spriteEffect, 0);
                    break;
            }
        }

        public void Dispose(bool finalize)
        {
            if (normalMap != null)
            {
                if (finalize)
                    normalMap.Dispose();

                normalMap = null;
            }

            if (distortion != null)
            {
                if (finalize)
                    distortion.Dispose();

                distortion = null;
            }

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
