/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// An attachment that effects the colour of a Particle.
    /// </summary>
    public class ColourDecayAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();
        List<ColourState> colours = new List<ColourState>();

        string id;

        float minDecay;
        float maxDecay;

        Color colour;

        /// <summary>
        /// The unique identifaction name given to the Attachment.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// The properties of the objects editted by this ColourDecayAttachment.
        /// </summary>
        public List<PropertySet> Properties
        {
            get { return props; }
        }

        /// <summary>
        /// Creates a new ColourDecay attachment. 
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="minDecay">Minimum decay.</param>
        /// <param name="maxDecay">Maximum decay.</param>
        /// <param name="colour">Colour to render the Particles in.</param>
        public ColourDecayAttachment(string id, float minDecay, float maxDecay, Color colour)
        {
            this.id = id;

            this.minDecay = minDecay;
            this.maxDecay = maxDecay;
            this.colour = colour;
        }

        public void _Update()
        {
            for (int i = 0; i < colours.Count; i++)
            {
                ColourState colourState = colours[i];

                colourState.Update();

                props[i].Colour = colourState.Colour;
                props[i].EffectColour = colourState.EffectColour;

                colours[i] = colourState;
            }
        }

        public void Generate()
        {
            ColourState state = GenerateColourState();

            colours.Add(state);
            props.Add(new PropertySet()
            {
                Colour = state.Colour,
                EffectColour = state.EffectColour
            });
        }

        public void Recalculate(int index)
        {
            ColourState state = GenerateColourState();

            colours[index] = state;

            props[index].Colour = state.Colour;
            props[index].EffectColour = state.EffectColour;
        }

        ColourState GenerateColourState()
        {
            return new ColourState()
            {
                Colour = colour,
                EffectColour = Color.White,
                ColourDecay = (byte)RandomHelper.GetRandomFloat(minDecay, maxDecay)
            };
        }

        public void Remove(int index)
        {
            props.Remove(props[index]);
            colours.Remove(colours[index]);
        }

        public void Dispose()
        {
            props.Clear();
            props = null;

            colours.Clear();
            colours = null;
        }

        public void ScreenResolutionChanged() { }
    }
}
