/* Created: 12/06/2015
 * Last Updated: 13/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Rendering2D.Framework.Primatives
{
    /// <summary>
    /// Defines a ring that is composed of Lines.
    /// </summary>
    public sealed class Ring : ObjectComponent, IDrawItem, ITextureQualityChanged
    {
        List<Vector2> points = new List<Vector2>();

        float angle = 0;

        int segments;
        int currentIndex = 0;

        bool autoGenerate = false;

        float thickness;

        int drawLayer;
        bool postRender;
        bool renderedDisabled = false;

        Line2D[] lineSegments;
        Texture2D lineTexture;
        Color colour = Color.White;
        Vector2 origin;

        Vector2 center;
        float radius;

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
        /// The colour to draw the Ring in.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// The current place in the generation of the Ring.
        /// </summary>
        public int CurrentIndex
        {
            get { return currentIndex; }
        }

        /// <summary>
        /// Is the Ring still being generated ?
        /// </summary>
        public bool IsGenerating
        {
            get { return !autoGenerate; }
        }

        /// <summary>
        /// Creates a new Ring.
        /// </summary>
        /// <param name="id">The ID of the Ring.</param>
        /// <param name="center">The center of the Ring.</param>
        /// <param name="radius">The radius of the Ring.</param>
        /// <param name="textureFilepath">Texture filepath of the Ring.</param>
        /// <param name="maxAngle">Maximum angle of the Ring (degrees).</param>
        /// <param name="segments">Number of segments in the Ring (minimum of 8).</param>
        /// <param name="thickness">Thickness of the line segments.</param>
        /// <param name="drawLayer">Layer to draw the Ring on.</param>
        /// <param name="postRender">Post render the Ring.</param>
        public Ring(string id, string textureFilepath, Vector2 center,
            float radius, float maxAngle, int segments,
            float thickness, int drawLayer, bool postRender)
            : this(id, textureFilepath, center, radius, maxAngle,
            segments, thickness, drawLayer, postRender, 1, true) { }

        /// <summary>
        /// Creates a new Ring.
        /// </summary>
        /// <param name="id">The ID of the Ring.</param>
        /// <param name="center">The center of the Ring.</param>
        /// <param name="radius">The radius of the Ring.</param>
        /// <param name="textureFilepath">Texture filepath of the Ring.</param>
        /// <param name="maxAngle">Maximum angle of the Ring (degrees).</param>
        /// <param name="segments">Number of segments in the Ring (minimum of 8).</param>
        /// <param name="thickness">Thickness of the line segments.</param>
        /// <param name="drawLayer">Layer to draw the Ring on.</param>
        /// <param name="postRender">Post render the Ring.</param>
        /// <param name="autoGenerate">Automatically generate the Segment lines of the Ring.</param>
        /// <param name="tesselation">The number of divisions that each line segment has.</param>
        public Ring(string id, string textureFilepath, Vector2 center,
            float radius, float maxAngle, int segments,
            float thickness, int drawLayer, bool postRender,
            int tesselation, bool autoGenerate)
            : base(id)
        {
            if (segments < 5)
                segments = 5;

            this.segments = segments;

            if (tesselation < 1)
                tesselation = 1;

            lineSegments = new Line2D[segments * tesselation];

            angle = MathHelper.ToRadians(maxAngle) / segments;

            this.radius = radius;
            this.center = center;

            this.thickness = thickness;

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

            GeneratePrimative();

            if (autoGenerate)
                while (currentIndex < lineSegments.Length)
                    ContinueGeneration();

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        void GeneratePrimative()
        {
            List<Vector2> p = new List<Vector2>();

            for (int i = 0; i < segments; i++)
                p.Add(CalculatePoint(i));

            int tesselation = lineSegments.Length / segments;

            for (int i = 0; i < segments; i++)
            {
                Vector2 start = new Vector2();
                Vector2 end = new Vector2();

                if (i == segments - 1)
                {
                    start = p[i];
                    end = p[0];
                }
                else
                {
                    start = p[i];
                    end = p[i + 1];
                }

                Vector2 direct = end - start;
                direct /= tesselation;

                for (int j = 0; j < tesselation; j++)
                    points.Add(start + (direct * j));
            }
        }

        Vector2 CalculatePoint(int index)
        {
            float theta = index * angle;

            Vector2 v = center + radius * new Vector2(
            (float)Math.Cos(theta), (float)Math.Sin(theta));

            return v;
        }

        /// <summary>
        /// Generates the Ring to a certain index.
        /// </summary>
        /// <param name="index">The index to generate the Ring to.</param>
        public void GenerateTo(int index)
        {
            if (!autoGenerate)
            {
                int difference = index - currentIndex;

                if (difference > 0 && difference + currentIndex <= lineSegments.Length)
                    for (int i = 0; i < difference; i++)
                        ContinueGeneration();
            }
        }

        /// <summary>
        /// Continue with the creation of the Ring.
        /// </summary>
        public void ContinueGeneration()
        {
            if (!autoGenerate)
            {
                Line2D l = new Line2D();

                if (currentIndex == lineSegments.Length - 1)
                {
                    l.StartPosition = points[currentIndex];
                    l.EndPosition = points[0];
                }
                else
                {
                    l.StartPosition = points[currentIndex];
                    l.EndPosition = points[currentIndex + 1];
                }

                l.Thickness = thickness;
                lineSegments[currentIndex] = l;

                currentIndex++;

                if (currentIndex >= lineSegments.Length)
                {
                    autoGenerate = true;

                    points.Clear();
                    points = null;
                }
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
    }
}
