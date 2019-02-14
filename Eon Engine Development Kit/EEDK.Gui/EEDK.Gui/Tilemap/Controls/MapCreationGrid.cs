/* Created 17/08/2015
 * Last Updated: 21/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Game2D.TileEngine;
using Eon.Game2D.TileEngine.Management;
using Eon.Maths.Helpers;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Drawing;
using Eon.Rendering2D.Framework.Misc;
using Eon.System.Interfaces;
using Eon.UIApi.Controls;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EEDK.Gui.Tilemap.Controls
{
    /// <summary>
    /// Defines a grid that has a set of selectable cells.
    /// </summary>
    public sealed class MapCreationGrid : MenuItem, IDrawItem, IActive
    {
        List<Tile> cells = new List<Tile>();
        List<Rectangle> sourceRects = new List<Rectangle>();
        List<LayerChunk> chunks = new List<LayerChunk>();

        Texture2D tileSheet;

        OnActivatedEvent onActive;

        Grid grid;
        Rectangle bounds;
        TileMapDeffination tileMap;

        int layerIndex;
        int currentSourceIndex = 1;
        int tileIndex;
        bool active;
        bool loaded = false;

        public int DrawLayer
        {
            get { return 0; }
        }

        public bool RenderDisabled
        {
            get { return true; }
        }

        public OnActivatedEvent OnActivate
        {
            get { return onActive; }
            set { onActive = value; }
        }

        /// <summary>
        /// The index of the TileLayer that this 
        /// MapCreationGrid is used to create.
        /// </summary>
        public int LayerIndex
        {
            get { return layerIndex; }
        }

        public MapCreationGrid(string id, Screen menu,
            int layerIndex, ref TileMapDeffination tileMap)
            : base(id, menu)
        {
            bounds = new Rectangle(
                0, 0, 826, (int)Common.TextureQuality.Y);

            this.layerIndex = layerIndex;
            this.tileMap = tileMap;

            TileLayerDeffination layer = tileMap.GetByOrder(layerIndex);

            if (layer.Tiles == null)
            {
                layer.Tiles = new List<int[]>();
                layer.Tiles.Add(new int[] { -1 });

                layer.TileOffset = Vector2.One;
            }
            else
                loaded = true;

            grid = new Grid(ID + "Grid", Vector2.Zero, layer.Tiles[0].Length,
                layer.Tiles.Count, (int)layer.TileSize.X,
                (int)layer.TileSize.Y, 1, false);

            AttachComponent(grid);

            DrawingManager.Add(this);
        }

        protected override void Initialize()
        {
            CreateCells(true);
            CreateSourceRectangles();

            base.Initialize();
        }

        void CreateSourceRectangles()
        {
            sourceRects.Clear();
            currentSourceIndex = 0;

            TileLayerDeffination layer = tileMap.GetByOrder(layerIndex);

            int columns = layer.Columns;
            int rows = layer.Rows;

            if (tileSheet == null)
                tileSheet = Common.ContentBuilder.Load<Texture2D>(
                   layer.TileSheetFilepath);

            int width = tileSheet.Width / columns;
            int height = tileSheet.Height / rows;

            Rectangle rect = new Rectangle(0, 0, width, height);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    sourceRects.Add(rect);

                    rect.X += width;
                }

                rect.X = 0;
                rect.Y += height;
            }
        }

        void CreateCells(bool recreate)
        {
            if (recreate)
                cells.Clear();

            Vector2 initialPos = Vector2.Zero;
            int count = 0;

            TileLayerDeffination layer = tileMap.GetByOrder(layerIndex);

            for (int y = 0; y < layer.Tiles.Count; y++)
            {
                for (int x = 0; x < layer.Tiles[0].Length; x++)
                {
                    if (recreate)
                    {
                        Tile t = new Tile()
                        {
                            Bounds = new Rectangle()
                            {
                                X = (int)initialPos.X,
                                Y = (int)initialPos.Y,
                                Width = (int)layer.TileSize.X,
                                Height = (int)layer.TileSize.Y
                            },

                            Column = x,
                            Row = y
                        };

                        if (loaded)
                            t.Index = layer.Tiles[y][x];
                        else
                            t.Index = -1;

                        cells.Add(t);
                    }
                    else
                    {
                        Tile c = cells[count];
                        c.Bounds = new Rectangle()
                            {
                                X = (int)initialPos.X,
                                Y = (int)initialPos.Y,
                                Width = (int)layer.TileSize.X,
                                Height = (int)layer.TileSize.Y
                            };

                        cells[count] = c;
                    }

                    initialPos.X += layer.TileSize.X;

                    count++;
                }

                initialPos.X = 0;
                initialPos.Y += layer.TileSize.Y;
            }

            grid.CellSizeX = (int)layer.TileSize.X;
            grid.CellSizeY = (int)layer.TileSize.Y;

            grid.Columns = layer.Tiles[0].Length;
            grid.Rows = layer.Tiles.Count;

            CameraManager2D.CurrentCamera.SetConstraint(
                new Rectangle(0, 0, grid.Width, grid.Height));

            CreateChunks();
        }

        void CreateChunks()
        {
            chunks.Clear();

            int chunkRowLimit = CameraManager2D.CurrentCamera.View.Height / grid.CellSizeY;
            int chunkColLimit = CameraManager2D.CurrentCamera.View.Width / grid.CellSizeX;

            if (chunkRowLimit == 0)
                chunkRowLimit = 1;

            if (chunkColLimit == 0)
                chunkColLimit = 1;

            Vector2 initialPos = Vector2.Zero;
            float prevPosY = initialPos.Y;

            for (int y = 0; y < grid.Rows; y += chunkRowLimit)
            {
                for (int x = 0; x < grid.Columns; x += chunkColLimit)
                {
                    LayerChunk chunk = new LayerChunk()
                    {
                        Bounds = new Rectangle(
                            (int)initialPos.X,
                            (int)initialPos.Y,
                            grid.CellSizeX * chunkColLimit,
                            grid.CellSizeY * chunkRowLimit),

                        Indices = new List<int>()
                    };

                    for (int i = 0; i < cells.Count; i++)
                        if (cells[i].Column >= x &&
                            cells[i].Column < x + chunkColLimit &&
                            cells[i].Row >= y &&
                            cells[i].Row < y + chunkRowLimit)
                            chunk.Indices.Add(i);

                    chunks.Add(chunk);
                    initialPos.X += chunkColLimit * grid.CellSizeY;
                }

                initialPos.Y += chunkRowLimit * grid.CellSizeY;
                initialPos.X = 0;
            }
        }

        protected override void Update()
        {
            TileLayerDeffination layer = tileMap.GetByOrder(layerIndex);

            if (layer.Rows * layer.Columns != sourceRects.Count)
                CreateSourceRectangles();

            if ((int)layer.TileSize.X != cells[0].Bounds.Width ||
                (int)layer.TileSize.Y != cells[0].Bounds.Height)
                CreateCells(false);

            if (layer.Tiles.Count * layer.Tiles[0].Length != cells.Count)
                CreateCells(true);

            base.Update();
        }

        /// <summary>
        /// Changes the source index of the currently
        /// selected tiles.
        /// </summary>
        /// <param name="index">The source index of the tile sheet.</param>
        public void ChangeSourceIndex(int index)
        {
            currentSourceIndex = index;
        }

        /// <summary>
        /// Hides the grid.
        /// </summary>
        public void HideGrid()
        {
            if (grid.Enabled)
                grid.Disable();
            else
                grid.Enable();
        }

        protected override bool IsInside(Vector2 position)
        {
            if (Enabled)
            {
                bool isInside = EonMathsHelper.IsInsideOf(bounds, position);

                if (isInside)
                {
                    active = true;

                    if (onActive != null)
                        onActive(this);

                    Vector2 move = Vector2.Zero;
                    float speed = 5;

                    if (position.X <= 15)
                        move.X -= speed;
                    else if (position.X >= bounds.Width - 15)
                        move.X += speed;

                    if (position.Y <= 15)
                        move.Y -= speed;
                    else if (position.Y >= bounds.Height - 15)
                        move.Y += speed;

                    CameraManager2D.CurrentCamera.Move(move);

                    Vector2 p = Vector2.Transform(position,
                       Matrix.Invert(CameraManager2D.CurrentCamera.ViewMatrix));

                    bool found = false;
                    tileIndex = 0;

                    while (tileIndex < cells.Count && !found)
                    {
                        if (EonMathsHelper.IsInsideOf(cells[tileIndex].Bounds, p))
                            found = true;
                        else
                            tileIndex++;
                    }

                    if (tileIndex >= cells.Count)
                        tileIndex--;
                }

                return isInside;
            }

            return false;
        }

        public override void CheckClick()
        {
            TileLayerDeffination layer = tileMap.GetByOrder(layerIndex);

            Tile cel = cells[tileIndex];
            cel.Index = currentSourceIndex;

            cells[tileIndex] = cel;

            layer.SetIndex(currentSourceIndex, cel.Row, cel.Column);

            base.CheckClick();
        }

        /// <summary>
        /// Changes a Tile.Index to -1.
        /// </summary>
        public void Remove()
        {
            Tile cel = cells[tileIndex];
            cel.Index = -1;

            cells[tileIndex] = cel;

            TileLayerDeffination layer = tileMap.GetByOrder(layerIndex);
            layer.SetIndex(-1, cel.Row, cel.Column);
        }

        public void Draw(DrawingStage stage)
        {
            bool renderSingle = ((MainScreen)Owner).RenderSingle;

            if (Enabled || !renderSingle)
                switch (stage)
                {
                    case DrawingStage.Colour:
                        {
                            for (int i = 0; i < chunks.Count; i++)
                                if (CameraManager2D.CurrentCamera.IsInView(chunks[i].Bounds))
                                {
                                    LayerChunk chunk = chunks[i];

                                    for (int j = 0; j < chunk.Indices.Count; j++)
                                        if (cells[chunk.Indices[j]].Index != -1 && cells[chunk.Indices[j]].Index < sourceRects.Count)
                                            Common.Batch.Draw(tileSheet, cells[chunk.Indices[j]].Bounds,
                                            sourceRects[cells[chunk.Indices[j]].Index], Color.White);
                                }
                        }
                        break;
                }
        }

        public override void Destroy()
        {
            DrawingManager.Remove(this);

            base.Destroy();
        }

        public override void Enable()
        {
            if (grid != null)
                CameraManager2D.CurrentCamera.SetConstraint(
                    new Rectangle(0, 0, grid.Width, grid.Height));

            base.Enable();
        }

        public bool Activated
        {
            get { return active; }
        }

        public void ToogleActive()
        {
            active = !active;
        }
    }
}
