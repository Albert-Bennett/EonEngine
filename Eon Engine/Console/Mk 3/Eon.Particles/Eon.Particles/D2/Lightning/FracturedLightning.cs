/* Created: 22/09/2014
 * Last Updated: 22/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles.D2.Lightning
{
    /// <summary>
    /// Used to define a FracturedLightning effect.
    /// </summary>
    public sealed class FracturedLightning : ObjectComponent, IUpdate
    {
        #region Varibles 

        List<LightningBolt> bolts = new List<LightningBolt>();
        List<int> indices = new List<int>();
        List<float> positions = new List<float>();

        int currentIndex = 0;

        string middleBoltFilepath;
        string endBoltFilepath;

        Color colour;
        Vector2 endPos;

        float thickness;
        float variance;
        float decay;

        float generateRate;
        int maxSegments;

        int drawLayer;
        bool postRender;

        #endregion
        #region Fields

        public int Priority
        {
            get { return 0; }
        }

        #endregion
        #region Ctor

        public FracturedLightning(string id, Vector2 startPosition, Vector2 endPosition,
            int drawLayer, bool postRender, string endBoltFilepath, string middleBoltFilepath,
            int maxSegments, float variance, float thickness, float generateRate,
            Color colour, float decayRate, int fractures, float deteriateRate)
            : base(id)
        {
            LightningBolt source = new LightningBolt(startPosition, endPosition, drawLayer,
                postRender, endBoltFilepath, middleBoltFilepath,
                maxSegments, variance, thickness, generateRate, colour, decayRate);

            source.OnComplete += new OnLightningComplete(SourceBoltFinished);
            source.OnCreated += new OnCreatedEvent(OnSegmentGenerated);

            bolts.Add(source);

            fractures = EonMathsHelper.Clamp(fractures, 0, maxSegments);

            for (int i = 0; i < fractures; i++)
            {
                indices.Add(RandomHelper.GetRandom(0, maxSegments));
                positions.Add(RandomHelper.GetRandom(0.0f, 1.0f));
            }

            positions.Sort();
            indices.Sort();

            endPos = endPosition;

            this.middleBoltFilepath = middleBoltFilepath;
            this.endBoltFilepath = endBoltFilepath;

            this.colour = colour;

            float deteriate = EonMathsHelper.Clamp(deteriateRate);

            this.thickness = thickness * deteriate;
            this.variance = variance / deteriate;

            this.decay = decayRate * deteriate;
            this.maxSegments = (int)((float)maxSegments * deteriate);
            this.generateRate = generateRate / deteriate;

            this.drawLayer = drawLayer;
            this.postRender = postRender;
        }

        void OnSegmentGenerated(int index)
        {
            if (currentIndex < indices.Count)
                if (indices[currentIndex] == index)
                {
                    Vector2 startPos = bolts[0].LineSegments[index - 1].StartPosition;

                    Quaternion rot = Quaternion.CreateFromAxisAngle(Vector3.UnitZ,
                        MathHelper.ToRadians(RandomHelper.GetRandom(-30, 30)));

                    Vector2 endPos = startPos + Vector2.Transform(
                        (this.endPos - bolts[0].LineSegments[0].StartPosition)
                        * (1 - positions[currentIndex]), rot);

                    bolts.Add(new LightningBolt(startPos, endPos, drawLayer, postRender,
                        endBoltFilepath, middleBoltFilepath, maxSegments, variance, thickness, 0, colour, decay));

                    currentIndex++;
                }
        }

        void SourceBoltFinished(LightningBolt bolt)
        {
            Destroy(false);
        }

        #endregion
        #region Update/ Dispose

        public void _Update()
        {
            for (int i = 0; i < bolts.Count; i++)
                bolts[i].Update();
        }

        public override void Destroy(bool remove)
        {
            for (int i = 0; i < bolts.Count; i++)
                bolts[i].Destroy();

            bolts.Clear();

            base.Destroy(remove);
        }

        #endregion
    }
}
