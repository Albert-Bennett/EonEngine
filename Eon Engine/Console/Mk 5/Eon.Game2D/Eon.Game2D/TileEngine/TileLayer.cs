/* Created 15/09/2013
 * Last Updated: 16/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game2D.TileEngine.Management;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Defines a set of Tiles that is
    /// used to define an image.
    /// </summary>
    public sealed class TileLayer : IDrawItem, IDestructable
    {
        List<LayerChunk> chunks = new List<LayerChunk>();
        List<Tile> tiles = new List<Tile>();
        List<int> tileIndices = new List<int>();

        List<Rectangle> sourceRects = new List<Rectangle>();

        Texture2D tileSheet;
        Texture2D normalMap = null;

        bool renderDisabled = false;
        bool postRender;
        int drawLayer;

        TileLayerDeffination layerInfo;

        bool enabled = true;
        bool destroyed = false;
        bool initialized = false;

        /// <summary>
        /// Has the TileLayer been destroyed.
        /// </summary>
        public bool IsDestroyed
        {
            get { return destroyed; }
        }

        public bool Enabled
        {
            get { return enabled; }
        }

        public int DrawLayer
        {
            get { return layerInfo.Order; }
        }

        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

        /// <summary>
        /// The maximum size of this TileLayer.
        /// </summary>
        public Rectangle MaxSize
        {
            get
            {
                return new Rectangle(0, 0,
                    layerInfo.Tiles[0].Length * (int)layerInfo.TileSize.X,
                    layerInfo.Tiles.Count * (int)layerInfo.TileSize.Y);
            }
        }

        public TileLayer(int drawLayer, bool postRender,
            TileLayerDeffination tileLayerDeffination)
        {
            this.drawLayer = drawLayer;
            this.postRender = postRender;

            layerInfo = tileLayerDeffination;

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        internal void Initialize()
        {
            CalculateTiles();
            CalculateChunks();

            tileSheet = Common.ContentBuilder.Load<Texture2D>(layerInfo.TileSheetFilepath);

            if (layerInfo.NormalMapFilepath != "null" && layerInfo.NormalMapFilepath != "")
                normalMap = Common.ContentBuilder.Load<Texture2D>(layerInfo.NormalMapFilepath);

            CalculateSourceRectangles();

            initialized = true;
        }

        void CalculateTiles()
        {
            Vector2 initialPos = Vector2.Zero;
            int count = 0;

            for (int y = 0; y < layerInfo.Tiles.Count; y++)
            {
                for (int x = 0; x < layerInfo.Tiles[0].Length; x++)
                {
                    if (layerInfo.Tiles[y][x] != -1)
                        tiles.Add(new Tile()
                        {
                            Bounds = new Rectangle()
                            {
                                X = (int)(initialPos.X),
                                Y = (int)(initialPos.Y),
                                Width = (int)layerInfo.TileSize.X,
                                Height = (int)layerInfo.TileSize.Y
                            },

                            Column = x,
                            Row = y,
                            Index = layerInfo.Tiles[y][x]
                        });

                    initialPos.X += layerInfo.TileSize.X;

                    count++;
                }

                initialPos.X = 0;
                initialPos.Y += layerInfo.TileSize.Y;
            }
        }

        void CalculateChunks()
        {
            chunks.Clear();

            int chunkColLimit = (int)(Common.TextureQuality.X / layerInfo.TileSize.X);
            int chunkRowLimit = (int)(Common.TextureQuality.Y / layerInfo.TileSize.Y);

            Vector2 initialPos = Vector2.Zero;

            int rows = layerInfo.Tiles.Count;
            int cols = layerInfo.Tiles[0].Length;

            for (int y = 0; y < rows; y += chunkRowLimit)
            {
                for (int x = 0; x < cols; x += chunkColLimit)
                {
                    LayerChunk chunk = new LayerChunk();
                    chunk.Indices = new List<int>();

                    for (int i = 0; i < tiles.Count; i++)
                    {
                        int maxX = x + chunkColLimit;
                        int maxY = y + chunkRowLimit;

                        if (tiles[i].Column >= x &&
                            tiles[i].Column < maxX &&
                            tiles[i].Row >= y &&
                            tiles[i].Row < maxY)
                            chunk.Indices.Add(i);
                    }

                    if (chunk.Indices.Count > 0)
                    {
                        chunk.Bounds = new Rectangle(
                            (int)initialPos.X,
                            (int)initialPos.Y,
                            (int)(layerInfo.TileSize.X * chunkColLimit),
                            (int)(layerInfo.TileSize.Y * chunkRowLimit));

                        chunks.Add(chunk);
                    }

                    initialPos.X += chunkColLimit * layerInfo.TileSize.X;
                }

                initialPos.Y += chunkRowLimit * layerInfo.TileSize.Y;
                initialPos.X = 0;
            }
        }

        void CalculateSourceRectangles()
        {
            sourceRects.Clear();

            int columns = layerInfo.Columns;
            int rows = layerInfo.Rows;

            int width = tileSheet.Width / columns;
            width -= (int)(layerInfo.TileOffset.X);

            int height = tileSheet.Height / rows;
            height -= (int)(layerInfo.TileOffset.Y);

            Rectangle rect = new Rectangle((int)layerInfo.TileOffset.X / 2,
                (int)layerInfo.TileOffset.Y / 2, width, height);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    sourceRects.Add(rect);

                    rect.X += width + (int)(layerInfo.TileOffset.X);
                }

                rect.X = (int)(layerInfo.TileOffset.X / 2);
                rect.Y += height + (int)(layerInfo.TileOffset.Y);
            }
        }

        public void Draw(DrawingStage stage)
        {
            if (initialized && !destroyed)
                switch (stage)
                {
                    case DrawingStage.Colour:
                        {
                            FindRenderableTiles();

                            Draw(tileSheet);
                        }
                        break;

                    case DrawingStage.Normal:
                        {
                            Draw(normalMap);
                        }
                        break;
                }
        }

        void Draw(Texture2D texture)
        {
            for (int i = 0; i < tileIndices.Count; i++)
                Common.Batch.Draw(texture, tiles[tileIndices[i]].Bounds,
                    sourceRects[tiles[tileIndices[i]].Index], Color.White);
        }

        void FindRenderableTiles()
        {
            tileIndices.Clear();

            if (CameraManager2D.CurrentCamera != null)
                for (int i = 0; i < chunks.Count; i++)
                    if (CameraManager2D.CurrentCamera.IsInView(chunks[i].Bounds))
                    {
                        LayerChunk chunk = chunks[i];

                        for (int j = 0; j < chunk.Indices.Count; j++)
                            if (CameraManager2D.CurrentCamera.IsInView(tiles[chunk.Indices[j]].Bounds))
                                tileIndices.Add(chunk.Indices[j]);
                    }
        }

        public void Disable()
        {
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }

        public void ToogleEnable()
        {
            enabled = !enabled;
        }

        public void Destroy()
        {
            destroyed = true;

            chunks.Clear();

            if (tileSheet != null)
                tileSheet = null;

            if (normalMap != null)
                normalMap = null;

            if (sourceRects != null)
                sourceRects.Clear();

            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);
        }
    }
}
