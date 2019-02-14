/* Created 05/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Rendering2D.Drawing
{
    /// <summary>
    /// Defines a class which is used to manage the
    /// drawing of multiple DrawItems on multiple layers.
    /// </summary>
    public sealed class DrawLayers
    {
        List<DrawLayerCollection> layers =
            new List<DrawLayerCollection>();

        int max = 0;

        /// <summary>
        /// The layer depth of the deepest IDrawItem.
        /// </summary>
        public int MaximumLayerDepth
        {
            get { return max; }
        }

        /// <summary>
        /// The number of layers present in this DrawLayers.
        /// </summary>
        public int Layers
        {
            get { return layers.Count; }
        }

        /// <summary>
        /// Adds a IDrawItem to this.
        /// </summary>
        /// <param name="item">The IDrawItem to be added.</param>
        public void AddDrawItem(IDrawItem item)
        {
            bool added = false;

            for (int i = 0; i < layers.Count; i++)
                if (layers[i]._drawLayer == item.DrawLayer)
                {
                    layers[i].Add(item);
                    added = true;
                }

            if (!added)
            {
                DrawLayerCollection drawn = new DrawLayerCollection(item.DrawLayer);
                drawn.Add(item);

                layers.Add(drawn);

                if (max < item.DrawLayer)
                    max = item.DrawLayer;
            }
        }

        /// <summary>
        /// Removes a IDrawItem from this.
        /// </summary>
        /// <param name="item">The IDrawItem to be removed.</param>
        public void Remove(IDrawItem item)
        {
            for (int i = 0; i < layers.Count; i++)
                if (layers[i]._drawLayer == item.DrawLayer)
                    layers[i].Remove(item);
        }

        /// <summary>
        /// Used to draw the various layers.
        /// </summary>
        /// <param name="stage">The current DrawingStage.</param>
        public void Draw(DrawingStage stage)
        {
            if (layers.Count > 0)
            {
                int layer = 0;

                while (layer <= max)
                {
                    for (int i = 0; i < layers.Count; i++)
                        if (layers[i]._drawLayer == layer)
                            layers[i].Draw(stage);

                    layer++;
                }
            }
        }
    }
}
