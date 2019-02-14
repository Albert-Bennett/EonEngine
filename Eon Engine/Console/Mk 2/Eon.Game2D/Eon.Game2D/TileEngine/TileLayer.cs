/* Created 15/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game2D.TileEngine.Management;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Defines a set of Tiles that is
    /// used to define an image.
    /// </summary>
    public sealed class TileLayer
    {
        List<LayerChunk> chunks = new List<LayerChunk>();

        Dictionary<int, Rectangle> sourceRects;

        Texture2D tileSheet;
        Texture2D normalMap = null;
        Texture2D distortionMap = null;

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

                int height = tileDeff.Tiles.Length * (int)size.Y;

                return new Rectangle(0, r.Y, r.Right, height);
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
        internal Texture2D NormalMap { get { return normalMap; } }
        internal Texture2D DistortionMap { get { return distortionMap; } }

        public TileLayer(int drawLayer, bool postRender,
            TileLayerDeffination tileLayerDeffination)
        {
            this.drawLayer = drawLayer;
            this.postRender = postRender;

            tileDeff = tileLayerDeffination;
            offset = tileDeff.TileOffset;
        }

        internal void Initialize()
        {
            CalculateChunks();

            tileSheet = Common.ContentManager.Load<Texture2D>(tileDeff.TileSheetFilepath);

            if (tileDeff.NormalMapFilepath != "None")
                normalMap = Common.ContentManager.Load<Texture2D>(tileDeff.NormalMapFilepath);

            if (tileDeff.DistortionMapFilepath != "None")
                distortionMap = Common.ContentManager.Load<Texture2D>(tileDeff.DistortionMapFilepath);

            CalculateSourceRectangles();
        }

        void CalculateChunks()
        {
            size = tileDeff.TileSize;

            int chunkRowLimit = (int)(Common.DefaultScreenResolution.Y / size.Y);
            int chunkColLimit = (int)(Common.DefaultScreenResolution.X / size.X);

            if (chunkColLimit == 0)
                chunkColLimit = 1;

            if (chunkRowLimit == 0)
                chunkRowLimit = 1;

            int currRow = 0;
            int currCol = 0;
            int prevRow = 0;

            int totalRows = tileDeff.Tiles.Length;
            int remainingRows = totalRows - chunkRowLimit;
            int maxColumns = GetMaxColumns();

            Vector2 initialPos = new Vector2(0, Common.DefaultScreenResolution.Y - size.Y) + offset;
            float prevPosY = initialPos.Y;

            LayerChunk chunk = new LayerChunk(drawLayer, initialPos, this);

            chunk.Initialize(postRender, 0, 0, chunkColLimit,
                chunkRowLimit, out currCol, out currRow, out initialPos);

            chunks.Add(chunk);

            while (remainingRows > 0 || currCol < maxColumns)
            {
                if (currCol < maxColumns)
                {
                    currRow = prevRow;
                    initialPos.Y = prevPosY;
                }
                else if (currCol >= maxColumns)
                {
                    currCol = 0;
                    initialPos.X = offset.X;

                    prevRow = currRow;

                    if (currRow >= totalRows)
                    {
                        currRow = totalRows;
                        remainingRows = 0;
                    }
                    else if (currRow < totalRows)
                        remainingRows -= chunkRowLimit;
                }

                prevPosY = initialPos.Y;

                LayerChunk chnk = new LayerChunk(drawLayer, initialPos, this);

                chnk.Initialize(postRender, currCol, currRow, chunkColLimit,
                    chunkRowLimit, out currCol, out currRow, out initialPos);

                chunks.Add(chnk);
            }
        }

        int GetMaxColumns()
        {
            //int max = 0;

            //for (int i = 0; i < tileDeff.Tiles.Length; i++)
            //    if (tileDeff.Tiles[i].Count > max)
            //        max = tileDeff.Tiles[i].Lenght;

            //return max--;

            return tileDeff.Tiles.GetLength(1);
        }

        void CalculateSourceRectangles()
        {
            int xOffset = tileSheet.Width / tileDeff.Columns;
            int yOffset = tileSheet.Height / tileDeff.Rows;

            sourceRects = new Dictionary<int, Rectangle>();

            sourceRects.Add(0, new Rectangle(0, 0, xOffset, yOffset));

            int count = 1;

            int r = 0;
            int c = 0;

            while (count < tileDeff.TotalTileImages)
            {
                c++;

                if (c >= tileDeff.Columns)
                {
                    c = 0;
                    r++;

                    if (r >= tileDeff.Rows)
                        r = 0;
                }

                Rectangle source = new Rectangle(
                    xOffset * c, yOffset * r, xOffset, yOffset);

                sourceRects.Add(count, source);

                count++;
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

            if (normalMap != null)
            {
                if (finalize)
                    normalMap.Dispose();

                normalMap = null;
            }

            if (distortionMap != null)
            {
                if (finalize)
                    distortionMap.Dispose();

                distortionMap = null;
            }

            if (sourceRects != null)
            {
                sourceRects.Clear();
                sourceRects = null;
            }
        }
    }
}
