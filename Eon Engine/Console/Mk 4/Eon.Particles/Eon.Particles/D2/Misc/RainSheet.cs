/* Created 11/08/2015
 * Last Updated: 13/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering2D.Drawing;
using Eon.System.States;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Particles.D2.Misc
{
    /// <summary>
    /// Defines a 2D tile sheet that is used
    /// to protray a particle effect.
    /// </summary>
    public sealed class RainSheet : GameObject, IDrawItem
    {
        Rectangle[] sourceRects;
        Texture2D texture;

        Vector2 speed;
        Vector2 size;

        List<SheetSet> sheets = new List<SheetSet>();

        int width;
        int height;

        Vector2 num;

        int drawLayer;

        bool postRender = false;
        bool renderDisabled = false;

        public int DrawLayer
        {
            get { return drawLayer; }
        }

        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

        /// <summary>
        /// Creates a new RainSheet.
        /// </summary>
        /// <param name="id">The ID of the RainSheet.</param>
        /// <param name="textureFilepath">The filepath of the texture to be used.</param>
        /// <param name="speedX">Speed of the particle set along the x-axis.</param>
        /// <param name="speedY">Speed of the particle set along the y-axis.</param>
        /// <param name="sizeX">Width of each of the particles in the RainSheet.</param>
        /// <param name="sizeY">Height of each of the particles in the RainSheet.</param>
        /// <param name="columns">The number of columns int he texture to be used.</param>
        /// <param name="rows">The number of rows int he texture to be used.</param>
        /// <param name="drawLayer">The layer to draw the RainSheet on.</param>
        /// <param name="presidence">When the RainSheet is active.</param>
        public RainSheet(string id, string textureFilepath,
            float speedX, float speedY, int sizeX, int sizeY,
            int columns, int rows, int drawLayer, bool postRender, string presidence)
            : base(id)
        {
            this.speed = new Vector2(speedX, speedY);
            this.size = new Vector2(sizeX, sizeY);

            this.drawLayer = drawLayer;
            this.postRender = postRender;

            try
            {
                texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                texture = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/Blank");
            }

            GetSourceRectangles(columns, rows);

            try
            {
                Presidence = (GameStates)Enum.Parse(typeof(GameStates), presidence);

                new Error("Invalid game state given: " + presidence +
                    " GameStates.None will be used.", Seriousness.Error);
            }
            catch
            {
                Presidence = GameStates.None;
            }

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        void GetSourceRectangles(int columns, int rows)
        {
            width = texture.Width / columns;
            height = texture.Height / rows;

            sourceRects = new Rectangle[columns * rows];

            int idx = 0;

            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                {
                    sourceRects[idx] = new Rectangle(
                        x * width, y * height, width, height);

                    idx++;
                }
        }

        protected override void Initialize()
        {
            num = Common.TextureQuality / size;
            num += Vector2.One;

            for (float x = 0; x < num.X; x++)
                for (float y = 0; y < num.Y; y++)
                {
                    Vector2 pos = new Vector2((x * width) - width, (y * height) - height);

                    sheets.Add(new SheetSet()
                    {
                        Index = RandomHelper.GetRandom(0, sourceRects.Length),
                        Position = pos,
                        Coordinate = new Vector2(x, y)
                    });
                }

            base.Initialize();
        }

        protected override void Update()
        {
            List<SheetSet> toRemove = new List<SheetSet>();

            for (int i = 0; i < sheets.Count; i++)
            {
                SheetSet sheet = sheets[i];

                sheet.Position += speed;

                if (sheet.Position.X < -width ||
                    sheet.Position.X > Common.TextureQuality.X ||
                    sheet.Position.Y < -height ||
                    sheet.Position.Y > Common.TextureQuality.Y)
                {
                    sheet.Position = new Vector2(sheet.Position.X, -height);

                    sheet.Index = RandomHelper.GetRandom(0, sourceRects.Length);
                }

                sheets[i] = sheet;
            }

            base.Update();
        }

        public void Draw(DrawingStage stage)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    {
                        Rectangle rect = new Rectangle(0, 0, width, height);

                        for (int i = 0; i < sheets.Count; i++)
                        {
                            rect.X = (int)sheets[i].Position.X;
                            rect.Y = (int)sheets[i].Position.Y;

                            Common.Batch.Draw(texture, rect,
                                sourceRects[sheets[i].Index], Color.White);
                        }
                    }
                    break;
            }
        }

        public override void Destroy()
        {
            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);

            base.Destroy();
        }
    }
}
