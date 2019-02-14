/* Created 15/09/2013
 * Last Updated: 21/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game2D.TileEngine.Management;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Defines a set of Tiles that is
    /// used to define an image.
    /// </summary>
    public sealed class TileLayer : IDrawItem
    {
        List<LayerChunk> chunks = new List<LayerChunk>();
        List<Tile> tiles = new List<Tile>();
        List<int> tileIndices = new List<int>();

        List<Rectangle> sourceRects;

        Texture2D tileSheet;
        Texture2D normalMap = null;
        Texture2D distortionMap = null;

        bool renderDisabled = false;
        bool postRender;
        int drawLayer;

        TileLayerDeffination layerInfo;

        bool enabled = true;

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
                return new Rectangle((int)layerInfo.TileOffset.X,
                    (int)layerInfo.TileOffset.Y,
                    layerInfo.Tiles[0].Length * (int)(layerInfo.TileSize.X + layerInfo.TileOffset.X),
                    layerInfo.Tiles.Count * (int)(layerInfo.TileSize.Y + layerInfo.TileOffset.Y));
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

            if (layerInfo.NormalMapFilepath != "null")
                normalMap = Common.ContentBuilder.Load<Texture2D>(layerInfo.NormalMapFilepath);

            if (layerInfo.DistortionMapFilepath != "null")
                distortionMap = Common.ContentBuilder.Load<Texture2D>(layerInfo.DistortionMapFilepath);

            CalculateSourceRectangles();
        }

        void CalculateTiles()
        {
            Vector2 initialPos = layerInfo.TileOffset;
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
                                X = (int)(initialPos.X + layerInfo.TileOffset.X),
                                Y = (int)(initialPos.Y + layerInfo.TileOffset.Y),
                                Width = (int)layerInfo.TileSize.X,
                                Height = (int)layerInfo.TileSize.Y
                            },

                            Column = x,
                            Row = y,
                            Index = layerInfo.Tiles[y][x]
                        });

                    initialPos.X += layerInfo.TileSize.X + layerInfo.TileOffset.X;

                    count++;
                }

                initialPos.X = layerInfo.TileOffset.X;
                initialPos.Y += layerInfo.TileSize.Y + layerInfo.TileOffset.Y;
            }
        }

        void CalculateChunks()
        {
            chunks.Clear();

            int chunkColLimit = (int)(Common.TextureQuality.Y / layerInfo.TileSize.X);
            int chunkRowLimit = (int)(Common.TextureQuality.X / layerInfo.TileSize.Y);

            Vector2 initialPos = layerInfo.TileOffset;

            int rows = layerInfo.Tiles.Count;
            int cols = layerInfo.Tiles[0].Length;

            for (int y = 0; y < rows; y += chunkRowLimit)
            {
                for (int x = 0; x < cols; x += chunkColLimit)
                {
                    LayerChunk chunk = new LayerChunk()
                    {
                        Bounds = new Rectangle(
                            (int)initialPos.X,
                            (int)initialPos.Y,
                            (int)(layerInfo.TileSize.X + layerInfo.TileOffset.X) * chunkColLimit,
                            (int)(layerInfo.TileSize.Y + layerInfo.TileOffset.Y) * chunkRowLimit),

                        Indices = new List<int>()
                    };

                    for (int i = 0; i < tiles.Count; i++)
                        if (tiles[i].Column >= x &&
                            tiles[i].Column < x + chunkColLimit &&
                            tiles[i].Row >= y &&
                            tiles[i].Row < y + chunkRowLimit)
                            chunk.Indices.Add(i);

                    chunks.Add(chunk);
                    initialPos.X += chunkColLimit * (layerInfo.TileSize.X + layerInfo.TileOffset.X);
                }

                initialPos.Y += chunkRowLimit * (layerInfo.TileSize.Y + layerInfo.TileOffset.Y);
                initialPos.X = layerInfo.TileOffset.X;
            }
        }

        void CalculateSourceRectangles()
        {
            sourceRects.Clear();

            int columns = layerInfo.Columns;
            int rows = layerInfo.Rows;

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

        public void Draw(DrawingStage stage)
        {
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

                case DrawingStage.Distortion:
                    {
                        Draw(distortionMap);
                    }
                    break;
            }
        }

        void Draw(Texture2D texture)
        {
            for (int i = 0; i < tileIndices.Count; i++)
                Common.Batch.Draw(texture, tiles[i].Bounds,
                    sourceRects[tiles[i].Index], Color.White);
        }

        void FindRenderableTiles()
        {
            tileIndices.Clear();

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

        public void Dispose()
        {
            chunks.Clear();

            if (tileSheet != null)
                tileSheet = null;

            if (normalMap != null)
                normalMap = null;

            if (distortionMap != null)
                distortionMap = null;

            if (sourceRects != null)
            {
                sourceRects.Clear();
                sourceRects = null;
            }

            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);
        }
    }
}
