/* Created 29/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine.Management
{
    /// <summary>
    /// Defines a group of Tiles which have a 
    /// total size equal to or slightly greater
    /// than the size of the screen.
    /// </summary>
    public sealed class LayerChunk : ObjectComponent, IDrawItem
    {
        #region Tiling variables

        Texture2D tileSheet;
        List<Tile> tiles;
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

        public LayerChunk(string id, int drawLayer, Rectangle area,
            Rectangle[] bounds, Rectangle[] source,
            Texture2D tileSheet, bool postRender)
            : base(id)
        {
            this.bounds = area;
            this.postRender = postRender;
            this.drawLayer = drawLayer;
            this.tileSheet = tileSheet;

            tiles = new List<Tile>();

            for (int i = 0; i < bounds.Length; i++)
                tiles.Add(new Tile(bounds[i], source[i]));

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        #endregion
        #region Rendering

        public void Draw()
        {
            if (CameraManager.CurrentCamera != null)
                if (CameraManager.CurrentCamera.IsInView(bounds))
                    for (int i = 0; i < tiles.Count; i++)
                        Common.Batch.Draw(tileSheet, tiles[i].Bounds,
                            tiles[i].SourceRect, Color.White);
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
