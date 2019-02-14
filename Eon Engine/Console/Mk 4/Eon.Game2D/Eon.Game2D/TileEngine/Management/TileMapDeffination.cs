/* Created 15/09/2013
 * Last Updated: 16/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;
using System.Linq;

namespace Eon.Game2D.TileEngine.Management
{
    /// <summary>
    /// Used to define a TileMap.
    /// </summary>
    public sealed class TileMapDeffination
    {
        /// <summary>
        /// Wheater or not the generated 
        /// TileMap should be post rendered.
        /// </summary>
        public bool PostRender;

        /// <summary>
        /// The TileLayerDeffinations that
        /// will make up the generate TileMap.
        /// </summary>
        public TileLayerDeffination[] TileLayers;

        /// <summary>
        /// Adds a new TileLayer to this TileMapDeffination.
        /// </summary>
        /// <param name="layer">The layer to be added.</param>
        public void AddTileLayer(TileLayerDeffination layer)
        {
            List<TileLayerDeffination> layers;

            if (TileLayers != null)
                layers = new List<TileLayerDeffination>(TileLayers);
            else
                layers = new List<TileLayerDeffination>();

            bool found = false;
            int idx = 0;

            while (!found && idx < layers.Count)
            {
                if (layers[idx].Order == layer.Order)
                {
                    layers.RemoveAt(idx);
                    layers.Add(layer);

                    found = true;
                }

                idx++;
            }

            if (!found)
                layers.Add(layer);

            layers.OrderBy(l => l.Order).ToList();

            TileLayers = layers.ToArray();
        }

        /// <summary>
        /// Find a TileLayer by the the order it 
        /// appears in in the TileMap.
        /// </summary>
        /// <param name="order">The order in which the layer appears at.</param>
        /// <returns>The found TileLayerDeffination.</returns>
        public TileLayerDeffination GetByOrder(int order)
        {
            TileLayerDeffination layer = null;

            bool found = false;
            int idx = 0;

            while (!found && idx < TileLayers.Length)
            {
                if (TileLayers[idx].Order == order)
                {
                    layer = TileLayers[idx];

                    found = true;
                }

                idx++;
            }

            return layer;
        }

        /// <summary>
        /// Deletes a TileLayer.
        /// </summary>
        /// <param name="layerOrder">The order of the TileLayer to be deleted.</param>
        /// <returns>If the TileLayer was deleted.</returns>
        public bool DeleteTileLayer(int layerOrder)
        {
            bool res = false;

            if (TileLayers != null)
            {
                List<TileLayerDeffination> tileLayers =
                    new List<TileLayerDeffination>(TileLayers);

                bool found = false;
                int idx = 0;

                while (!found && idx < tileLayers.Count)
                {
                    if (tileLayers[idx].Order == layerOrder)
                        found = true;
                    else
                        idx++;
                }

                res = found;

                if (found)
                {
                    tileLayers.Remove(tileLayers[idx]);

                    TileLayers = tileLayers.ToArray();
                }
            }

            return res;
        }

        /// <summary>
        /// Does a TileLayerDeffination exist at the given index.
        /// </summary>
        /// <param name="index">The index of the TileLayer.</param>
        /// <returns>The result of the search.</returns>
        public bool TileLayerExists(int layerOrder)
        {
            bool found = false;

            if (TileLayers != null)
            {
                int idx = 0;

                while (!found && idx < TileLayers.Length)
                {
                    if (TileLayers[idx].Order == layerOrder)
                        found = true;

                    idx++;
                }
            }

            return found;
        }
    }
}
