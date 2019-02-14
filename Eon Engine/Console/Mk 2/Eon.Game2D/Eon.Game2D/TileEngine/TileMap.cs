/* Created 15/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game2D.TileEngine.Management;
using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Rendering2D.Cameras;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Used to define a grid that contains Tiles.
    /// </summary>
    public class TileMap : ObjectComponent, IDispose
    {
        List<TileLayer> layers;

        TileMapDeffination deff;

        /// <summary>
        /// Creates a new TileMap.
        /// </summary>
        /// <param name="ID">The ID of the TileMap.</param>
        /// <param name="mapFilePath">The filepath for the TileMap.</param>
        public TileMap(string ID, string mapFilePath)
            : base(ID)
        {
            try
            {
                deff = Common.ContentManager.Load<TileMapDeffination>(mapFilePath);
            }
            catch
            {
                throw new ArgumentNullException(mapFilePath + " dosn't exist");
            }
        }

        protected override void Initialize()
        {
            layers = new List<TileLayer>();

            for (int i = 0; i < deff.TileLayers.Length; i++)
                layers.Add(new TileLayer(i, deff.PostRender, deff.TileLayers[i]));

            int maxWidth = int.MaxValue;
            int maxHeight = 0;
            int maxY = 0;

            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Initialize();

                if (layers[i].MaxSize.Height>maxHeight)
                    maxHeight = (int)layers[i].MaxSize.Height;

                if (layers[i].MaxSize.Y < maxY)
                    maxY = layers[i].MaxSize.Y;

                if (maxWidth > layers[i].MaxSize.Width)
                    maxWidth = (int)layers[i].MaxSize.Width;
            }

            if (CameraManager2D.CurrentCamera != null)
            {
                Rectangle rect = new Rectangle(0, maxY, maxWidth, maxHeight);

                CameraManager2D.CurrentCamera.SetConstraint(rect);
                BroadPhase.ReInitialize(rect);
            }

            base.Initialize();
        }

        /// <summary>
        /// Disposes of the TileMap.
        /// </summary>
        /// <param name="finalize">Finalize the deposition of the TileMap.</param>
        public void Dispose(bool finalize)
        {
            for (int i = 0; i < layers.Count; i++)
                layers[i].Dispose(finalize);

            layers.Clear();
            layers = null;
        }
    }
}
