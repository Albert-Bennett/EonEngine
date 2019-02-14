/* Created: 21/05/2015
 * Last Updated: 14/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths;
using Eon.Rendering2D.Drawing;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering2D.Framework.Primatives
{
    /// <summary>
    /// Defines a texture stereached between several points.
    /// </summary>
    public sealed class Line : IDrawItem
    {
        int drawLayer;
        bool postRender;
        bool enabled = true;
        bool renderedDisabled = false;

        float thickness = 0.0f;

        Line2D[] lineSegments;
        Texture2D lineTexture;
        Color colour = Color.White;
        Vector2 origin;

        public bool Enabled
        {
            get { return enabled; }
        }

        public int DrawLayer
        {
            get { return drawLayer; }
        }

        public bool RenderDisabled
        {
            get { return renderedDisabled; }
            set { renderedDisabled = value; }
        }

        /// <summary>
        /// The colour to draw the Line in.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// The line segments that make up the Line.
        /// </summary>
        public Line2D[] LineSegments
        {
            get { return lineSegments; }
        }

        /// <summary>
        /// The thickness of each LineSegment that make up the Line.
        /// </summary>
        public float Thickness
        {
            get { return thickness; }
            set
            {
                thickness = value;

                for (int i = 0; i < lineSegments.Length; i++)
                    lineSegments[i].Thickness = value;
            }
        }

        /// <summary>
        /// Creates a new Line.
        /// </summary>
        /// <param name="id">The ID of the Line.</param>
        /// <param name="points">The points along the Line.</param>
        /// <param name="textureFilepath">The filepath for the Line's texture.</param>
        /// <param name="thickness">The thickness of the Line.</param>
        /// <param name="drawLayer">Layer to draw the Line on.</param>
        /// <param name="postRender">Post render the Line.</param>
        public Line(Vector2 startPoint, Vector2 endPoint,
            float thickness, int drawLayer, bool postRender)
        {
            lineSegments = new Line2D[1]
            {
                new Line2D(startPoint, endPoint, thickness)
            };

            this.drawLayer = drawLayer;
            this.postRender = postRender;

            lineTexture = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/Pixel");

            origin = new Vector2(0, lineTexture.Height / 2);

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        /// <summary>
        /// Creates a new Line.
        /// </summary>
        /// <param name="id">The ID of the Line.</param>
        /// <param name="points">The points along the Line.</param>
        /// <param name="textureFilepath">The filepath for the Line's texture.</param>
        /// <param name="thickness">The thickness of the Line.</param>
        /// <param name="drawLayer">Layer to draw the Line on.</param>
        /// <param name="postRender">Post render the Line.</param>
        public Line(List<Vector2> points, string textureFilepath,
            float thickness, int drawLayer, bool postRender)
        {
            if (points.Count >= 2)
            {
                lineSegments = new Line2D[points.Count - 1];

                for (int i = 0; i < points.Count - 1; i++)
                    lineSegments[i] = new Line2D(points[i], points[i + 1], thickness);

                this.drawLayer = drawLayer;
                this.postRender = postRender;

                try
                {
                    lineTexture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
                }
                catch
                {
                    lineTexture = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/Pixel");
                }

                origin = new Vector2(0, lineTexture.Height / 2);

                if (postRender)
                    PostRenderManager.Add(this);
                else
                    DrawingManager.Add(this);
            }
            else
            {
                new Error("Line contains < 2 points and will be destroyed as a result", Seriousness.Error);
            }
        }

        public void Draw(DrawingStage stage)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    {
                        for (int i = 0; i < lineSegments.Length; i++)
                        {
                            Vector2 scale = new Vector2(
                            lineSegments[i].Direction().Length(), lineSegments[i].Thickness) /
                            new Vector2(lineTexture.Bounds.Width, lineTexture.Bounds.Height);

                            float angle = lineSegments[i].Angle();

                            Common.Batch.Draw(lineTexture, lineSegments[i].StartPosition, null, colour,
                                angle, origin, scale, SpriteEffects.None, 0f);
                        }
                    }
                    break;
            }
        }

        public void TextureQualityChanged()
        {
            float thickness = Common.GetReScaled(lineSegments[0].Thickness);

            for (int i = 0; i < lineSegments.Length - 1; i++)
            {
                Line2D l = lineSegments[i];

                l.StartPosition = Common.GetReScaled(l.StartPosition);
                l.EndPosition = Common.GetReScaled(l.EndPosition);
                l.Thickness = thickness;

                lineSegments[i] = l;
            }
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

        public void Destroy()
        {
            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);
        }
    }
}
