/* Created: 21/09/2014
 * Last Updated: 30/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths;
using Eon.Maths.Helpers;
using Eon.Rendering2D.Drawing;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Eon.Particles.D2.Lightning
{
    /// <summary>
    /// Used to define a bolt of lightning.
    /// </summary>
    public sealed class LightningBolt : IDrawItem
    {
        #region Variables
        #region Rendering

        Line2D[] lineSegments;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan genTime = TimeSpan.Zero;

        Texture2D boltEnd;
        Texture2D boltSegment;

        Vector2 startingPosition;
        Vector2 endPosition;

        Color colour;

        Vector2 boltEndOrigin;
        Vector2 boltMiddleOrigin;

        int drawLayer;
        bool postRender;

        #endregion
        #region Generation

        float[] positions;
        Vector2 prevStart;

        float decayRate;
        float thickness;
        float variance;
        float alpha;

        float jaggedness;

        int currentSegment = 1;
        float prevDisplace = 0;

        bool generating = true;

        #endregion
        #region Misc

        bool renderDisabled = false;
        bool enabled = true;

        #endregion
        #endregion
        #region Fields

        public OnLightningComplete OnComplete;
        public OnCreatedEvent OnCreated;

        public int DrawLayer
        {
            get { return drawLayer; }
        }

        public bool Enabled
        {
            get { return enabled; }
        }

        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

        /// <summary>
        /// The Line2D's the define the LightningBolt.
        /// </summary>
        public Line2D[] LineSegments
        {
            get { return lineSegments; }
        }

        /// <summary>
        /// The colour of the LightningBolt.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set
            {
                colour = value;
                alpha = colour.A;
            }
        }

        #endregion
        #region Ctor

        /// <summary>
        /// Creates a new LightningBolt.
        /// </summary>
        /// <param name="startPosition">The starting position of the LightningBolt.</param>
        /// <param name="endPosition">The ending position of the LightningBolt.</param>
        /// <param name="drawLayer">The layer that the LightningBolt will be drawn on.</param>
        /// <param name="postRender">Is the LightningBolt to be post rendered.</param>
        /// <param name="endBoltFilepath">The filepath for the texture at the end of the LightningBolt.</param>
        /// <param name="middleBoltFilepath">The filepath for the texture in the middle of the LightningBolt.</param>
        public LightningBolt(Vector2 startPosition, Vector2 endPosition,
            int drawLayer, bool postRender,
             string endBoltFilepath, string middleBoltFilepath)
            : this(startPosition, endPosition,
                drawLayer, postRender, endBoltFilepath,
            middleBoltFilepath, 20, 100, 6, 0, Color.White, 0.002f) { }

        /// <summary>
        /// Creates a new LightningBolt.
        /// </summary>
        /// <param name="startPosition">The starting position of the LightningBolt.</param>
        /// <param name="endPosition">The ending position of the LightningBolt.</param>
        /// <param name="drawLayer">The layer that the LightningBolt will be drawn on.</param>
        /// <param name="postRender">Is the LightningBolt to be post rendered.</param>
        /// <param name="endBoltFilepath">The filepath for the texture at the end of the LightningBolt.</param>
        /// <param name="middleBoltFilepath">The filepath for the texture in the middle of the LightningBolt.</param>
        /// <param name="maxSegments">Maximum number of segments in the LightningBolt.</param>
        /// <param name="variance">The positional variance of the line segments. 
        /// The greater the number the lower the variance.</param>
        /// <param name="thickness">The thickness of the line segments.</param>
        /// <param name="generateRate">The length of time it takes to generate a line segment.</param>
        /// <param name="colour">The colour of the LightningBolt.</param>
        /// <param name="decayRate">The decay rate of the LightningBolt.</param>
        public LightningBolt(Vector2 startPosition, Vector2 endPosition,
            int drawLayer, bool postRender,
             string endBoltFilepath, string middleBoltFilepath,
            int maxSegments, float variance, float thickness,
            float generateRate, Color colour, float decayRate)
        {
            this.startingPosition = prevStart = startPosition;
            this.endPosition = endPosition;

            this.drawLayer = drawLayer;
            this.postRender = postRender;

            this.variance = variance;
            this.thickness = thickness;

            boltEnd = LoadTexture(endBoltFilepath);
            boltSegment = LoadTexture(middleBoltFilepath);

            boltEndOrigin = new Vector2(boltEnd.Width, boltEnd.Height / 2);
            boltMiddleOrigin = new Vector2(0, boltSegment.Height / 2);

            lineSegments = new Line2D[maxSegments];

            genTime = TimeSpan.FromMilliseconds(generateRate);
            this.colour = colour;
            this.decayRate = decayRate;

            alpha = colour.A / 255;

            jaggedness = 1 / variance;

            positions = new float[maxSegments];

            for (int i = 0; i < maxSegments; i++)
                positions[i] = RandomHelper.GetRandom(0.0f, 1.0f);

            positions = positions.OrderBy(o => o).ToArray<float>();

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
                new Error("Unable to load texture: " + filepath, Seriousness.Error);
                return new Texture2D(Common.Device, 1, 1);
            }
        }

        #endregion
        #region Generating

        public void Update()
        {
            if (genTime == TimeSpan.Zero && generating)
            {
                for (int i = 1; i < positions.Length; i++)
                    CreateTrailSegment();

                generating = false;
            }
            else if (generating)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime >= genTime)
                {
                    currentTime -= genTime;

                    CreateTrailSegment();

                    if (lineSegments.Length == currentSegment)
                        generating = false;
                }
            }
            else
            {
                alpha -= decayRate;
                colour *= alpha;

                if (alpha <= 0.0f)
                    if (OnComplete != null)
                        OnComplete(this);
            }
        }

        void CreateTrailSegment()
        {
            Vector2 direct = endPosition - startingPosition;
            Vector2 normal = Vector2.Normalize(new Vector2(direct.Y, -direct.X));
            float length = direct.Length();

            float p = positions[currentSegment];
            float scale = (p - positions[currentSegment - 1]) * (length * jaggedness);

            float displace = RandomHelper.GetRandom(-variance, variance);
            displace -= (displace - prevDisplace) * (1 - scale);

            Vector2 point = startingPosition + ((p * direct) + (displace * normal));

            Line2D segment = new Line2D(prevStart, point, thickness);

            lineSegments[currentSegment] = segment;

            prevStart = point;
            prevDisplace = displace;

            if (OnCreated != null)
                OnCreated(currentSegment);

            currentSegment++;
        }

        #endregion
        #region Drawing

        public void Draw(DrawingStage stage)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    {
                        for (int i = 0; i < lineSegments.Length; i++)
                        {
                            Vector2 scale = new Vector2(
                                lineSegments[i].Direction().Length(), thickness);

                            float angle = lineSegments[i].Angle();

                            Common.Batch.Draw(boltEnd, lineSegments[i].StartPosition, null, colour, angle,
                                boltEndOrigin, thickness, SpriteEffects.None, 0f);

                            Common.Batch.Draw(boltEnd, lineSegments[i].EndPosition, null, colour, angle
                                    + MathHelper.Pi, boltEndOrigin, thickness, SpriteEffects.None, 0f);

                            Common.Batch.Draw(boltSegment, lineSegments[i].StartPosition, null, colour,
                                angle, boltMiddleOrigin, scale, SpriteEffects.None, 0f);
                        }
                    }
                    break;
            }
        }

        #endregion
        #region Misc

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

        #endregion
    }
}
