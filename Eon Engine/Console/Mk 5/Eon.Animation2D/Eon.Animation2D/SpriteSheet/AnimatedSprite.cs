/* Created 03/06/2013
 * Last Updated: 28/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Eon.System.Interfaces.Base;
using Eon.System.States;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Animation2D.SpriteSheet
{
    /// <summary>
    /// Defines an animated sprite.
    /// </summary>
    public sealed class AnimatedSprite : ObjectComponent, IDrawItem, ITextureQualityChanged
    {
        #region Varibles
        #region Graphical Variables

        TextureAtlas spriteSheet;

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

        MediaStates currentState = MediaStates.Paused;

        int currentFrame = 0;

        int currRow = 0;
        int currCol = 0;

        Rectangle sourceRect;

        #endregion
        #region Misc Variables

        bool postRender;
        bool renderDisabled = false;

        bool loop = false;

        #endregion
        #endregion
        #region Fields
        #region Graphical Fields

        /// <summary>
        /// Render the object even if Disabled.
        /// </summary>
        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

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

        public Vector2 Origin
        {
            get { return spriteSheet.Origin; }
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
        /// Should the AnimatedSprite loop when it 
        /// has finished playing the animation.
        /// </summary>
        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        #endregion
        #endregion
        #region Ctors

        /// <summary>
        /// Creates a new AnimatedSprite.
        /// </summary>
        /// <param name="id">The ID of the AnimatedSprite.</param>
        /// <param name="drawLayer">The draw layer of the AnimatedSprite.</param>
        /// <param name="colour">The colour to render the AnimatedSprite.</param>
        /// <param name="postRender">Render the AnimatedSprite after the main 2D render pass.</param>
        /// <param name="rotation">The rotation of the AnimatedSprite.</param>
        /// <param name="scale">The scale of the AnimatedSprite.</param>
        /// <param name="offset">The positional offset of the AnimatedSprite.</param>
        public AnimatedSprite(string id, int drawLayer, string spriteSheetFilepath,
            Color colour, bool postRender, float rotation, Vector2 scale, Vector2 offset)
            : base(id)
        {
            Priority = 1;

            this.drawLayer = drawLayer;

            this.colour = colour;
            this.rotation = rotation;

            this.scale = scale;
            this.offset = offset;

            if (spriteSheetFilepath != "")
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(Vector2),
                    typeof(int),
                    typeof(string)
                };

                spriteSheet = SerializationHelper.Deserialize<TextureAtlas>(
                    spriteSheetFilepath, true, ".SPR", extraTypes);

                spriteSheet.Initalize();
                sourceRect = spriteSheet.SourceRectangle;
            }

            spriteEffect = SpriteEffects.None;

            this.postRender = postRender;

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        #endregion
        #region Animation Methods

        protected override void Update()
        {
            if (spriteSheet != null)
            {
                rect = new Rectangle((int)offset.X + (int)spriteSheet.Origin.X,
                    (int)offset.Y + (int)spriteSheet.Origin.Y, (int)scale.X, (int)scale.Y);

                if (Owner != null)
                {
                    rect.X += (int)Owner.World.Position.X;
                    rect.Y += (int)Owner.World.Position.Y;

                    rect.Width *= (int)Owner.World.Size.X;
                    rect.Height *= (int)Owner.World.Size.Y;

                    rotation += Owner.World.Rotation.Z;
                }

                if (currentState == MediaStates.Playing)
                    if (currentTime > spriteSheet.FrameRate)
                        ChangeFrame();
                    else
                        currentTime += Common.ElapsedTimeDelta;
            }
        }

        void ChangeFrame()
        {
            currentTime -= spriteSheet.FrameRate;

            currCol++;
            currentFrame++;

            if (currentFrame >= spriteSheet.TotalFrames)
            {
                if (!loop)
                {
                    currentFrame--;
                    currCol--;

                    Stop();
                }
                else
                {
                    currRow = 0;
                    currCol = 0;
                    currentFrame = 0;
                }
            }
            else if (currCol >= spriteSheet.Columns)
            {
                currCol = 0;
                currRow++;
            }

            sourceRect.X = currCol * spriteSheet.FrameWidth;
            sourceRect.Y = currRow * spriteSheet.FrameHeight;
        }

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <param name="randomStart">Weather or not the animation should 
        /// begin playing at a random frame.</param>
        public void Play(bool randomStart)
        {
            if (spriteSheet != null)
                if (spriteSheet.TotalFrames > 1)
                {
                    if (currentState != MediaStates.Playing)
                    {
                        currentState = MediaStates.Playing;

                        currentTime = TimeSpan.Zero;

                        if (randomStart)
                        {
                            currRow = RandomHelper.GetRandom(0, spriteSheet.Rows);

                            int cols = (spriteSheet.TotalFrames - (currRow *
                                spriteSheet.Columns)) % spriteSheet.Columns;

                            currCol = RandomHelper.GetRandom(0, cols);

                            sourceRect.X = currCol * spriteSheet.FrameWidth;
                            sourceRect.Y = currRow * spriteSheet.FrameHeight;
                        }
                        else
                        {
                            sourceRect.X = 0;
                            sourceRect.Y = 0;
                        }
                    }
                }
        }

        /// <summary>
        /// Pauses or resumes the animation.
        /// </summary>
        public void PauseResume()
        {
            if(spriteSheet!=null)
            if (spriteSheet.TotalFrames > 1)
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
            if (spriteSheet != null)
            if (spriteSheet.TotalFrames > 1)
            {
                currentState = MediaStates.Stopped;

                if (OnFinished != null)
                    OnFinished(ID);
            }
        }

        #endregion
        #region Misc

        public void Draw(DrawingStage stage)
        {
            if (spriteSheet != null)
            switch (stage)
            {
                case DrawingStage.Colour:
                    Common.Batch.Draw(spriteSheet.Texture, rect, sourceRect, colour,
                        rotation, Origin, spriteEffect, 0);
                    break;

                default:
                    Common.Batch.Draw(spriteSheet.NormalMap, rect, sourceRect,
                        Color.White, rotation, Origin, spriteEffect, 0);
                    break;
            }
        }

        /// <summary>
        /// Changes the TextureAtlas used.
        /// </summary>
        /// <param name="spriteSheetFilepath">The filepath of the SpriteSheet.</param>
        public void ChangeSpriteSheet(string spriteSheetFilepath)
        {
            try
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(Vector2),
                    typeof(int),
                    typeof(string)
                };

                spriteSheet = SerializationHelper.Deserialize<TextureAtlas>(
                    spriteSheetFilepath, true, ".SPR", extraTypes);

                spriteSheet.Initalize();

                sourceRect = spriteSheet.SourceRectangle;

                currCol = 0;
                currentFrame = 0;
                currRow = 0;

                currentTime = TimeSpan.Zero;
            }
            catch
            {
                new Error("The SpriteSheet: " + spriteSheetFilepath +
                    ", dosen't exist.", Seriousness.CriticalError);
            }
        }

        /// <summary>
        /// Changes the TextureAtlas used.
        /// </summary>
        /// <param name="atlas">The TextureAtlas to be used.</param>
        public void ChangeSpriteSheet(TextureAtlas atlas)
        {
            if (atlas != null)
            {
                spriteSheet = atlas;
                spriteSheet.Initalize();

                sourceRect = spriteSheet.SourceRectangle;

                currCol = 0;
                currentFrame = 0;
                currRow = 0;

                currentTime = TimeSpan.Zero;
            }
            else
                new Error("Given TextureAtlas is null.", Seriousness.Error);
        }

        public void SetColour(Color colour)
        {
            this.colour = colour;
        }

        public void TextureQualityChanged()
        {
            offset = Common.GetReScaled(offset);
            scale = Common.GetReScaled(scale);
        }

        protected override void _Destroy()
        {
            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);

            base._Destroy();
        }

        #endregion
    }
}
