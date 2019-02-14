/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines n attachment that lerps between two colours.
    /// </summary>
    public class ColourLerpAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();
        List<ColourLerpState> lerps = new List<ColourLerpState>();

        string id;

        Color colour;
        Color toLerpTo;

        float lerpMin;
        float lerpMax;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The unique identifaction name given to the Attachment.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// The properties of the objects editted by this ColourLerpAttachment.
        /// </summary>
        public List<PropertySet> Properties
        {
            get { return props; }
        }

        /// <summary>
        /// Creates a new ColourLerpAttachment.
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="minLerp">The minimum amount to lerp by.</param>
        /// <param name="maxLerp">The maximum amount to lerp by.</param>
        public ColourLerpAttachment(string id, float minLerp, float maxLerp,
            float r, float g, float b, float a,
            float lerpR, float lerpG, float lerpB, float lerpA)
        {
            this.id = id;
            this.lerpMin = minLerp;
            this.lerpMax = maxLerp;

            this.colour = new Color(r, g, b, a);
            this.toLerpTo = new Color(lerpR, lerpG, lerpB, lerpA);
        }

        public void _Update()
        {
            for (int i = 0; i < lerps.Count; i++)
            {
                ColourLerpState l = lerps[i];
                l.Update();

                lerps[i] = l;

                props[i] = new PropertySet()
                {
                    EffectColour = Color.White,
                    Colour = l.Colour
                };
            }
        }

        public void Generate()
        {
            lerps.Add(new ColourLerpState()
            {
                Colour = colour,
                LerpRate = RandomHelper.GetRandom(lerpMin, lerpMax),
                ToLerpTo = toLerpTo
            });

            props.Add(new PropertySet()
            {
                Scale = 1,
                Rotation = 0,
                Colour = colour,
                EffectColour = Color.White
            });
        }

        public void Remove(int index)
        {
            props.Remove(props[index]);
            lerps.Remove(lerps[index]);
        }

        public void ScreenResolutionChanged() { }

        public void Dispose()
        {
            lerps.Clear();
            lerps = null;

            props.Clear();
            props = null;
        }
    }
}
