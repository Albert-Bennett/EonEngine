/* Created 15/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game2D.TileEngine.Management;
using Eon.Interfaces;
using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Rendering2D.Cameras;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Used to define a grid that contains Tiles.
    /// </summary>
    public class TileMap : GameObject, IPostInitialize, IDispose
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
                layers.Add(new TileLayer(ID + i, deff.PostRender, deff.TileLayers[i]));

            base.Initialize();
        }

        public void PostInitialize()
        {
            int minX = 0;
            int minY = 0;
            int right = 0;
            int bottom = 0;

            for (int i = 0; i < layers.Count; i++)
            {
                Rectangle maxSize = layers[i].MaxSize;

                if (i == 0)
                {
                    minX = maxSize.X;
                    minY = maxSize.Y;
                }
                else
                {
                    if (maxSize.X < minX)
                        minX = maxSize.X;

                    if (maxSize.Y < minY)
                        minY = maxSize.Y;
                }

                if (right < maxSize.Right)
                    right = maxSize.Right;

                if (bottom < maxSize.Bottom)
                    bottom = maxSize.Bottom;
            }

            right += minX;
            bottom += minY;

            if (CameraManager.CurrentCamera != null)
            {
                Rectangle rect = new Rectangle(minX, minY, right, bottom);

                CameraManager.CurrentCamera.SetConstraint(rect);
                BroadPhase.ReInitialize(rect);
            }
        }

        /// <summary>
        /// Disposes of the TileMap.
        /// </summary>
        /// <param name="finalize">Finalize the deposition of the TileMap.</param>
        public void Dispose(bool finalize)
        {
            for (int i = 0; i < layers.Count; i++)
                layers[i].Destroy();

            layers.Clear();

            layers.Clear();
            layers = null;
        }
    }
}
