/* Created 18/08/2015
 * Last Updated: 06/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Game2D.TileEngine.Management;
using Eon.Rendering2D.Cameras;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EEDK.Gui.Tilemap.Controls
{
    /// <summary>
    /// Defines a manager for MapCreationGrid's.
    /// </summary>
    public sealed class MapCreator : GameObject
    {
        Dictionary<int, MapCreationGrid> layerGrids =
            new Dictionary<int, MapCreationGrid>();

        MainScreen owner;
        TileMapDeffination tileMap;
        BaseCamera2D camera;

        int currentLayerGrid = -1;
        bool showGrids = true;

        /// <summary>
        /// The index of the currently active MapCreationGrid.
        /// </summary>
        public int CurrentTileLayer
        {
            get { return currentLayerGrid; }
        }

        /// <summary>
        /// Creates a new MapCreator.
        /// </summary>
        /// <param name="owner">The owner of the MapCreator.</param>
        public MapCreator(MainScreen owner, ref TileMapDeffination tileMap)
            : base("MapCreator")
        {
            this.owner = owner;
            this.tileMap = tileMap;

            camera = new BaseCamera2D("MapCam", new Rectangle(
                0, 0, 826, (int)Common.TextureQuality.Y));

            AttachComponent(camera);
        }

        /// <summary>
        /// Changes the current TileMapDeffination in use.
        /// </summary>
        /// <param name="tileMap">The new TileMapDeffination.</param>
        public void SetTileMap(TileMapDeffination tileMap)
        {
            this.tileMap = tileMap;
        }

        /// <summary>
        /// Creates a new MapCreator to handle the newly
        /// added TileLayerDeffination.
        /// </summary>
        /// <param name="index">The index of the layer to be added.</param>
        public void AddLayer(int index)
        {
            bool found = layerGrids.ContainsKey(index);

            if (!found)
            {
                layerGrids.Add(index, new MapCreationGrid("Layer" + index,
                    owner, index, ref tileMap));

                layerGrids.OrderBy(l => l.Key);
            }
        }

        /// <summary>
        /// Change the current MapCreationGrid.
        /// </summary>
        /// <param name="index">The index of the MapCreationGrid.</param>
        public void ChangeLayer(int index)
        {
            bool found = layerGrids.ContainsKey(index);

            if (found)
            {
                if (currentLayerGrid != -1)
                    layerGrids[currentLayerGrid].Disable();

                layerGrids[index].Enable();

                if (!showGrids)
                    layerGrids[index].HideGrid();

                currentLayerGrid = index;
            }
            else
                if (currentLayerGrid != -1)
                    layerGrids[currentLayerGrid].Disable();
        }

        /// <summary>
        /// Deletes a MapCreationGrid.
        /// </summary>
        /// <param name="index">The index of the 
        /// MapCreationGrid to be deleted.</param>
        public void DeleteLayer(int index)
        {
            layerGrids[currentLayerGrid].Destroy();
            layerGrids.Remove(currentLayerGrid);

            currentLayerGrid = -1;
        }

        /// <summary>
        /// Deletes all MapCreationGrids. 
        /// </summary>
        public void DeleteAll()
        {
            foreach (MapCreationGrid grid in layerGrids.Values)
                grid.Destroy();

            layerGrids.Clear();
            currentLayerGrid = -1;
        }

        /// <summary>
        /// Changes the index of the source rectangle to be used.
        /// </summary>
        /// <param name="sourceIndex">The new source index.</param>
        public void ChangeSourceIndex(int sourceIndex)
        {
            if (currentLayerGrid != -1)
            {
                if (sourceIndex == -1)
                    layerGrids[currentLayerGrid].Remove();
                else
                    layerGrids[currentLayerGrid].ChangeSourceIndex(sourceIndex);
            }
        }

        /// <summary>
        /// Hides the grid of the current MapCreationGrid.
        /// </summary>
        public void ToogleGrids()
        {
            if (currentLayerGrid != -1)
                layerGrids[currentLayerGrid].HideGrid();

            showGrids = !showGrids;
        }
    }
}
