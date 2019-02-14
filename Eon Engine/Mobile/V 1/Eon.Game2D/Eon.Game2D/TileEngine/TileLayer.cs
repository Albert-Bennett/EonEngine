/* Created 15/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game2D.TileEngine.Management;
using Eon.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Defines a set of Tiles that is
    /// used to define an image.
    /// </summary>
    public sealed class TileLayer:GameObject
    {
        List<LayerChunk> chunks = new List<LayerChunk>();
        Dictionary<int, Rectangle> sourceRects = new Dictionary<int, Rectangle>();

        Texture2D tileSheet;

        bool postRender;
        int drawLayer;
        Vector2 size;

        Vector2 offset;

        TileLayerDeffination tileDeff;

        /// <summary>
        /// The maximum size of this TileLayer.
        /// </summary>
        public Rectangle MaxSize
        {
            get
            {
                Rectangle r = chunks[chunks.Count - 1].RenderSize;

                int height = tileDeff.Tiles.Count * (int)size.Y;

                return new Rectangle((int)offset.X, (int)offset.Y, r.Right, r.Bottom);
            }
        }

        internal Dictionary<int, Rectangle> SourceRects
        {
            get { return sourceRects; }
        }

        internal TileLayerDeffination LayerDeff { get { return tileDeff; } }
        internal Vector2 Size { get { return size; } }
        internal Vector2 Offset { get { return offset; } }
        internal Texture2D TileSheet { get { return tileSheet; } }

        public TileLayer(string id, bool postRender,
            TileLayerDeffination tileLayerDeffination)
            : base(id)
        {
            this.drawLayer = tileLayerDeffination.DrawLayer;
            this.postRender = postRender;

            tileDeff = tileLayerDeffination;
            offset = tileDeff.TileOffset;
        }

        protected override void Initialize()
        {
            tileSheet = Common.ContentManager.Load<Texture2D>(tileDeff.TileSheetFilepath);

            CalculateChunks();

            sourceRects.Clear();

            base.Initialize();
        }

        void CalculateChunks()
        {
            size = tileDeff.TileSize;

            if (Common.PreviousScreenResolution != Vector2.One)
            {
                size = Common.ReCalibrateScreenSpaceVector(size);
                offset = Common.ReCalibrateScreenSpaceVector(tileDeff.TileOffset);
            }

            //Max amount of tiles along the x-axis
            int maxX = (int)(Common.ScreenResolution.X / (size.X + offset.X));
            maxX = EonMathHelper.Round(maxX);

            //Max amount of tiles along the y-axis
            int maxY = (int)(Common.ScreenResolution.Y / (size.Y + offset.Y));
            maxY = EonMathHelper.Round(maxY);

            //Maximum map size x
            int mapSizeX = (int)(size.X + offset.X) * tileDeff.Tiles[0].Count;

            //Maximum map size y
            int mapSizeY = (int)(size.Y + offset.Y) * tileDeff.Tiles.Count;

            float maxChunksX = (tileDeff.Tiles[0].Count / (float)maxX);

            if (EonMathHelper.GetDecimals(maxChunksX) > 0.0f)
                maxChunksX++;

            float maxChunksY = (tileDeff.Tiles.Count / (float)maxY);

            if (EonMathHelper.GetDecimals(maxChunksY) > 0.0f)
                maxChunksY++;

            int actualMaxChunksX = (int)maxChunksX;
            int actualMaxChunksY = (int)maxChunksY;

            //Starting pos for first chunk
            Vector2 initalPos = offset;

            int prevRow = 0;
            int prevCol = 0;

            int rowCount = tileDeff.Tiles.Count;

            for (int y = 0; y < actualMaxChunksY; y++)
            {
                float initialY = initalPos.Y;

                #region Get Max Rows in current chunk

                int maxCellsY = maxY;

                if (rowCount < maxY)
                {
                    maxCellsY = rowCount;
                    rowCount = 0;
                }
                else
                {
                    maxCellsY = maxY;
                    rowCount -= maxY;
                }

                #endregion

                int colCount = tileDeff.Tiles[0].Count;

                for (int x = 0; x < actualMaxChunksX; x++)
                {
                    #region Get Max Columns in current chunk

                    int maxCellsX = 0;

                    if (colCount < maxX)
                    {
                        maxCellsX = colCount;
                        colCount = 0;
                    }
                    else
                    {
                        maxCellsX = maxX;
                        colCount -= maxX;
                    }

                    #endregion

                    Rectangle rect = new Rectangle((int)initalPos.X,
                        (int)initalPos.Y, (int)(maxCellsX * size.X),
                        (int)(maxCellsY * size.Y));

                    int cellX = prevCol + maxCellsX;
                    int cellY = prevRow + maxCellsY;

                    List<Rectangle> bounds = new List<Rectangle>();
                    List<Rectangle> source = new List<Rectangle>();

                    float initialX = initalPos.X;

                    //Getting this chunk.
                    for (int y2 = prevRow; y2 < cellY; y2++)
                    {
                        initalPos.X = initialX;

                        for (int x2 = prevCol; x2 < cellX; x2++)
                        {
                            bounds.Add(new Rectangle((int)initalPos.X,
                                (int)initalPos.Y, (int)size.Y, (int)size.Y));

                            source.Add(GetSourceRect(tileDeff.Tiles[y2][x2]));

                            initalPos.X += (size.X + offset.X);
                        }

                        initalPos.Y += (size.Y + offset.Y);
                    }

                    LayerChunk chunk = new LayerChunk(ID + chunks.Count, tileDeff.DrawLayer,
                       rect, bounds.ToArray(), source.ToArray(), tileSheet, postRender);

                    chunks.Add(chunk);
                    AttachComponent(chunk);

                    prevCol += maxCellsX;

                    int tempX = actualMaxChunksX;

                    if (x != tempX - 1)
                        initalPos.Y = initialY;
                    if (x == tempX - 1)
                        initalPos.X = offset.X;
                }

                prevCol = 0;
                prevRow += maxCellsY;
            }
        }

        Rectangle GetSourceRect(int index)
        {
            if (sourceRects.ContainsKey(index))
                return sourceRects[index];
            else
            {
                int xOffset = tileSheet.Width / tileDeff.Columns;
                int yOffset = tileSheet.Height / tileDeff.Rows;

                int total = 0;

                for (int y = 0; y < tileDeff.Rows; y++)
                    for (int x = 0; x < tileDeff.Columns; x++)
                    {
                        Rectangle source = new Rectangle()
                        {
                            X = x * xOffset,
                            Y = y * yOffset,
                            Width = xOffset,
                            Height = yOffset
                        };

                        sourceRects.Add(total, source);

                        total++;
                    }

                return sourceRects[index];
            }
        }

        public void Dispose(bool finalize)
        {
            foreach (LayerChunk chunk in chunks)
                chunk.Dispose();

            chunks.Clear();

            if (tileSheet != null)
            {
                if (finalize)
                    tileSheet.Dispose();

                tileSheet = null;
            }

            if (sourceRects != null)
            {
                sourceRects.Clear();
                sourceRects = null;
            }
        }

        public void ScreenResolutionChanged()
        {
            for (int i = 0; i < chunks.Count; i++)
                chunks[i].ScreenResolutionChanged();
        }
    }
}
