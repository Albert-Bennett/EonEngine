/* Created 15/09/2013
 * Last Updated: 06/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game2D.TileEngine.Management;
using Eon.Helpers;
using Eon.Rendering2D.Cameras;
using Eon.System.States;
using Eon.Testing;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Used to define a grid that contains Tiles.
    /// </summary>
    public sealed class TileMap : GameObject
    {
        TileLayer[] layers;

        TileMapDeffination deff;

        /// <summary>
        /// Creates a new TileMap.
        /// </summary>
        /// <param name="ID">The ID of the TileMap.</param>
        /// <param name="mapFilepath">The filepath for the TileMap.</param>
        public TileMap(string ID, string mapFilepath)
            : this(ID, mapFilepath, GameStates.Game) { }

        /// <summary>
        /// Creates a new TileMap.
        /// </summary>
        /// <param name="ID">The ID of the TileMap.</param>
        /// <param name="mapFilepath">The filepath for the TileMap.</param>
        /// <param name="presidence">The GameState that the TileMap renders in.</param>
        public TileMap(string ID, string mapFilepath, string presidence)
            : this(ID, mapFilepath, (GameStates)Enum.Parse(typeof(GameStates), presidence)) { }

        /// <summary>
        /// Creates a new TileMap.
        /// </summary>
        /// <param name="ID">The ID of the TileMap.</param>
        /// <param name="mapFilepath">The filepath for the TileMap.</param>
        /// <param name="precidence">The GameState that the TileMap renders in.</param>
        public TileMap(string ID, string mapFilepath,
            GameStates precidence)
            : base(ID)
        {
            this.Presidence = precidence;

            try
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(List<int[]>),
                    typeof(TileMapDeffination),
                    typeof(TileMapDeffination[]),
                    typeof(bool),
                    typeof(Vector2)
                };

                deff = SerializationHelper.Deserialize<TileMapDeffination>(
                    mapFilepath, true, ".Tiles", extraTypes);
            }
            catch
            {
                new Error(mapFilepath + " dosn't exist", Seriousness.CriticalError);
                Destroy();
            }

            layers = new TileLayer[deff.TileLayers.Length];

            for (int i = 0; i < deff.TileLayers.Length; i++)
            {
                TileLayer lyr = new TileLayer(i,
                    deff.PostRender, deff.TileLayers[i]);

                lyr.Initialize();
                lyr.RenderDisabled = true;

                layers[i] = lyr;
            }
        }

        /// <summary>
        /// Used to render TileLayers even when disabled.
        /// </summary>
        /// <param name="state">The state of the action.</param>
        public void RenderDisabled(bool state)
        {
            for (int i = 0; i < layers.Length; i++)
                layers[i].RenderDisabled = state;
        }

        /// <summary>
        /// Constricts the Currently actice Camera2D 
        /// using the TileMaps's maximum co-ordinates.
        /// </summary>
        public void ConstrictCamera()
        {
            int maxWidth = int.MaxValue;
            int maxHeight = 0;
            int maxY = 0;

            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i].MaxSize.Height > maxHeight)
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
            }
        }

        /// <summary>
        /// Destroys the TileMap.
        /// </summary>
        public override void Destroy()
        {
            for (int i = 0; i < layers.Length; i++)
                layers[i].Destroy();

            layers = null;

            base.Destroy();
        }
    }
}
