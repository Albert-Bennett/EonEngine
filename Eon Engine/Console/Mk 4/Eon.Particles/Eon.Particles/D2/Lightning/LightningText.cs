﻿/* Created: 22/09/2014
 * Last Updated: 05/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Particles.D2.Lightning
{
    /// <summary>
    /// Used to define a lightning effect that encompases text.
    /// </summary>
    public sealed class LightningText : ObjectComponent, IUpdate
    {
        #region Varibles

        const float min = 10;

        List<Vector2> points = new List<Vector2>();
        List<LightningBolt> bolts = new List<LightningBolt>();

        Color colour;

        string middleBoltFilepath;
        string endBoltFilepath;

        float thickness;
        float variance;

        int boltGenerationChance;
        int maxSegments;

        int minBolts = 20;
        int maxBolts = 50;

        int drawLayer;
        bool postRender;

        float max = Common.TextureQuality.X;

        #endregion
        #region Fields

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The colour of the LightningText.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// The minimum amount of LightningBolts generated.
        /// </summary>
        public int MinBolts
        {
            get { return minBolts; }
            set { minBolts = value; }
        }

        /// <summary>
        /// The maximum amount of LightningBolts generated.
        /// </summary>
        public int MaxBolts
        {
            get { return maxBolts; }
            set { maxBolts = value; }
        }

        #endregion
        #region Ctor

        public LightningText(string id, Vector2 position, float scale, int density, string fontFilepath,
            string text, int drawLayer, bool postRender, string endBoltFilepath, string middleBoltFilepath,
            int boltGenerationChance, int maxSegments, float variance, float thickness, Color colour)
            : base(id)
        {
            SpriteFont font = Common.ContentBuilder.Load<SpriteFont>(fontFilepath);

            points = TextureHelper.GenerateTextPoints(font, text, scale, density, position);

            this.drawLayer = drawLayer;
            this.postRender = postRender;

            this.boltGenerationChance = boltGenerationChance;

            this.endBoltFilepath = endBoltFilepath;
            this.middleBoltFilepath = middleBoltFilepath;

            this.maxSegments = maxSegments;
            this.variance = variance;
            this.thickness = thickness;
            this.colour = colour;

            max = font.MeasureString(text).Y;
        }

        #endregion
        #region Updating/ Destroy

        public void _Update()
        {
            DestroyBolts();

            for (int i = 0; i < points.Count; i++)
            {
                Vector2 point = points[i];

                int chance = RandomHelper.GetRandom(0, boltGenerationChance);

                if (chance == 0)
                {
                    Vector2 nearest = Vector2.Zero;
                    float dist = max;

                    int num = RandomHelper.GetRandom(minBolts, maxBolts);

                    int j = 0;

                    while (j < num)
                    {
                        Vector2 other = points[RandomHelper.GetRandom(0, points.Count)];
                        float d = Vector2.Distance(point, other);

                        if (d > min && d < dist)
                        {
                            dist = d;
                            nearest = other;
                        }

                        j++;
                    }

                    if (dist < max)
                    {
                        LightningBolt bolt = new LightningBolt(point, nearest, drawLayer, postRender, endBoltFilepath,
                            middleBoltFilepath, maxSegments, variance, thickness, 0, colour, 0);

                        bolt.Update();

                        bolts.Add(bolt);
                    }
                }
            }
        }

        public void _PostUpdate() { }

        void DestroyBolts()
        {
            for (int i = 0; i < bolts.Count; i++)
                bolts[i].Destroy();

            bolts.Clear();
        }

        public override void Destroy(bool remove)
        {
            DestroyBolts();

            base.Destroy(remove);
        }

        #endregion
    }
}