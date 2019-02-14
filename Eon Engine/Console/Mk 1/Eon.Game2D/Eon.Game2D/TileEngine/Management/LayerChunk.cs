/* Created 29/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Rendering2D;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine.Management
{
    /// <summary>
    /// Defines a group of Tiles which have a 
    /// total size equal to or slightly greater
    /// than the size of the screen.
    /// </summary>
    public sealed class LayerChunk : IDrawItem
    {
        #region Tiling variables

        TileLayer attachedTo;

        List<Tile> tiles;
        List<Tile> renderTiles = new List<Tile>();

        Vector2 initialPos;
        Rectangle bounds;

        /// <summary>
        /// The size of the LayerChunk.
        /// </summary>
        public Rectangle RenderSize
        {
            get { return bounds; }
        }

        #endregion
        #region Rendering variables

        int drawLayer;
        bool postRender;

        public int DrawLayer
        {
            get { return drawLayer; }
        }

        #endregion
        #region Ctors

        public LayerChunk(int drawLayer, Vector2 initialPos, TileLayer attachedTo)
        {
            this.initialPos = initialPos;
            this.attachedTo = attachedTo;

            this.drawLayer = drawLayer;
        }

        internal void Initialize(bool postRender, int startCol,
            int startRow, int maxCols, int maxRows,
            out int colStoppedAt, out int rowStoppedAt, out Vector2 finalPos)
        {
            this.postRender = postRender;

            tiles = new List<Tile>();

            int rowLimit = CalculateRowLimit(maxRows, startRow);
            int[] colLimits = new int[rowLimit];

            int colLimit = 0;

            finalPos = initialPos;

            for (int row = startRow; row < startRow + rowLimit; row++)
            {
                colLimit = CalculateColumnLimit(maxCols, startCol, row);
                colLimits[row - startRow] = colLimit;

                for (int column = startCol; column < startCol + colLimit; column++)
                {
                    int refNum = attachedTo.LayerDeff.Tiles[row][column];

                    if (refNum != -1)
                    {
                        Tile t = new Tile()
                        {
                            Size = attachedTo.Size,
                            _Position = finalPos,
                            RefferenceNumber = refNum,
                            Number = tiles.Count
                        };

                        tiles.Add(t);
                    }

                    finalPos.X += attachedTo.Size.X + attachedTo.Offset.X;
                }

                if (row + 1 < startRow + rowLimit)
                    finalPos.X = initialPos.X;

                finalPos.Y += -attachedTo.Size.Y + attachedTo.Offset.Y;
            }

            rowStoppedAt = startRow + rowLimit;
            colStoppedAt = startCol + CalculateColumnsLimitMax(colLimits);

            int c = CalculateColumnsLimitMax(colLimits);

            bounds = new Rectangle((int)initialPos.X, (int)(initialPos.Y - (c * attachedTo.Size.Y)),
               (int)( c * attachedTo.Size.X), (int)(rowLimit * attachedTo.Size.Y)); 

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        int CalculateRowLimit(int maxRows, int startRow)
        {
            int total = attachedTo.LayerDeff.Tiles.Count - startRow;

            if (total - maxRows >= 0)
                total = maxRows;

            return total;
        }

        int CalculateColumnsLimitMax(int[] colLimits)
        {
            int max = 0;

            for (int i = 0; i < colLimits.Length; i++)
                if (max < colLimits[i])
                    max = colLimits[i];

            return max;
        }

        int CalculateColumnLimit(int maxChunkCols, int startCol, int row)
        {
            int remainingColumns = attachedTo.LayerDeff.Tiles[row].Count - startCol;

            if (remainingColumns - maxChunkCols >= 0)
                remainingColumns = maxChunkCols;

            return remainingColumns;
        }

        #endregion
        #region Rendering

        public void Draw(DrawingStage stage)
        {
            if (ShouldRender())
            {
                if (stage == DrawingStage.Colour)
                    renderTiles.Clear();

                for (int i = 0; i < tiles.Count; i++)
                {
                    bool render = false;

                    if (stage == DrawingStage.Colour)
                        render = DrawTileColour(tiles[i]);
                    else
                        render = DrawTile(tiles[i]);

                    if (render)
                    {
                        Rectangle sourceRect = attachedTo.SourceRects[tiles[i].RefferenceNumber];

                        switch (stage)
                        {
                            case DrawingStage.Colour:
                                Common.Batch.Draw(attachedTo.TileSheet, tiles[i].Bounds, sourceRect, Color.White);
                                break;

                            case DrawingStage.Distortion:
                                if (attachedTo.DistortionMap != null)
                                    Common.Batch.Draw(attachedTo.DistortionMap, tiles[i].Bounds, sourceRect, Color.White);
                                break;

                            case DrawingStage.Normal:
                                if (attachedTo.NormalMap != null)
                                    Common.Batch.Draw(attachedTo.NormalMap, tiles[i].Bounds, sourceRect, Color.White);
                                break;
                        }
                    }
                }
            }
        }

        bool ShouldRender()
        {
            if (CameraManager.CurrentCamera != null)
                return CameraManager.CurrentCamera.IsInView(bounds);

            return true;
        }

        bool DrawTileColour(Tile tile)
        {
            if (CameraManager.CurrentCamera != null)
            {
                if (CameraManager.CurrentCamera.IsInView(tile.Bounds))
                {
                    renderTiles.Add(tile);
                    return true;
                }

                return false;
            }

            return true;
        }

        bool DrawTile(Tile tile)
        {
            foreach (Tile t in tiles)
                if (t.Number == tile.Number)
                    return true;

            return false;
        }

        #endregion
        #region Misc

        public void Dispose()
        {
            if (tiles != null)
            {
                tiles.Clear();
                tiles = null;
            }

            renderTiles.Clear();

            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);
        }

        public void ScreenResolutionChanged()
        {
            for (int i = 0; i < tiles.Count; i++)
                tiles[i].ScreenResolutionChanged();
        }

        #endregion
    }
}
