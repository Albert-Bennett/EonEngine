/* Created 14/08/2015
 * Last Updated: 21/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Game2D.TileEngine;
using Eon.Helpers;
using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Eon.Rendering2D.Framework.Misc;
using Eon.UIApi.Controls;
using Eon.UIApi.Controls._2D;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace EEDK.Gui.Tilemap.Controls
{
    /// <summary>
    /// Defines a grid based image selector.
    /// </summary>
    public sealed class ImageGrid : MenuItem
    {
        #region Astectics

        Sprite bg;
        Sprite hovered;
        Sprite selected;

        Rectangle bounds;

        Vector2 size;

        #endregion
        #region Cell Selection

        List<Tile> cells = new List<Tile>();
        Grid grid;

        int rows = 1;
        int cols = 1;

        int selectedCell = 0;
        int tempIndex = 0;

        #endregion
        #region Tile Sheet Selection

        Sprite tileSheet;
        Texture2D tiles;
        Texture2D normalMap;

        bool tilesSelected = true;
        Button2D loadBtn;

        System.Windows.Forms.OpenFileDialog openDia;

        string normalMapFilePath = "";
        string tileSheetFilePath = "";

        #endregion

        public SourceTileChangedEvent OnSourceChanged;
        public LoadedTilemapEvent OnLoadedTilemap;

        /// <summary>
        /// The tile index of the 
        /// currently selected Tile.
        /// </summary>
        public int SelectedCell
        {
            get { return cells[selectedCell].Index; }
        }

        /// <summary>
        /// The number of rows in the ImageGrid.
        /// </summary>
        public int Rows
        {
            get
            {
                if (grid != null)
                    return grid.Rows;

                return 0;
            }
            set
            {
                if (tilesSelected && tileSheet != null ||
                    !tilesSelected && normalMap != null)
                    if (value > 0)
                    {
                        size.Y = bounds.Height / value;

                        if (grid != null)
                        {
                            grid.CellSizeY = (int)size.Y;
                            grid.Rows = value;
                        }

                        rows = value;
                    }
            }
        }

        /// <summary>
        /// The number of columns in the ImageGrid.
        /// </summary>
        public int Columns
        {
            get
            {
                if (grid != null)
                    return grid.Columns;

                return 0;
            }
            set
            {
                if (tilesSelected && tileSheet != null ||
                    !tilesSelected && normalMap != null)
                    if (value > 0)
                    {
                        size.X = bounds.Width / value;

                        if (grid != null)
                        {
                            grid.CellSizeX = (int)size.X;
                            grid.Columns = value;
                        }

                        cols = value;
                    }
            }
        }

        public string TileSheetFilepath
        {
            get { return tileSheetFilePath; }
            set
            {
                if (value != "null")
                {
                    tileSheetFilePath = value;
                    tiles = Common.ContentBuilder.Load<Texture2D>(tileSheetFilePath);

                    if (tilesSelected && tileSheet != null)
                    {
                        tileSheet.Texture = tiles;

                        HideLoadingTools();
                    }
                }
                else
                {
                    tileSheetFilePath = "";
                    tiles = null;

                    if (tilesSelected)
                        ShowLoadingTools();
                }
            }
        }

        public string NormalMapFilepath
        {
            get { return normalMapFilePath; }
            set
            {
                if (value != "null")
                {
                    normalMapFilePath = value;
                    normalMap = Common.ContentBuilder.Load<Texture2D>(normalMapFilePath);

                    if (!tilesSelected && tileSheet != null)
                    {
                        tileSheet.Texture = tiles;

                        HideLoadingTools();
                    }
                }
                else
                {
                    normalMapFilePath = "";
                    normalMap = null;

                    if (!tilesSelected)
                        ShowLoadingTools();
                }
            }
        }

        public ImageGrid(string id, Screen menu, Vector2 position)
            : base(id, menu)
        {
            size = new Vector2(256, 256);

            loadBtn = new Button2D(ID + "btn", menu, new Rectangle(1132, (int)position.Y, 80, 30),
                "Eon/Textures/Pixel", "Eon/Fonts/Arial23", "Load", 4, true);

            loadBtn.HoveredOverColour = Color.Green;
            loadBtn.FontColour = Color.Black;
            loadBtn.Colour = Color.DarkGray;
            loadBtn.OnClicked += new OnClickedEvent(Load);

            openDia = new System.Windows.Forms.OpenFileDialog();
            openDia.Filter = ".xnb File (*.xnb)|*.xnb";

            bg = new Sprite(ID + "BG", Color.DarkGray, true, position, size);
            AttachComponent(bg);

            grid = new Grid(ID + "Grid", position, 1, 1,
                (int)size.X, (int)size.Y, 3, true);

            grid.Colour = Color.White;
            grid.LineThickness = 1;

            AttachComponent(grid);

            tileSheet = new Sprite(ID + "Sheet", 2,
                "Eon/Textures/Pixel", Color.White, true,
                new Rectangle((int)grid.Position.X,
                    (int)grid.Position.Y, grid.Width,
                    grid.Height));

            AttachComponent(tileSheet);

            bounds = new Rectangle((int)position.X,
                (int)position.Y, (int)size.X, (int)size.Y);

            this.size = new Vector2()
            {
                X = size.X / cols,
                Y = size.Y / rows
            };

            hovered = new Sprite(ID + "Hovered", 2, "Eon/Textures/Pixel",
                new Color(0, 0, 255, 255), true, position, this.size);

            AttachComponent(hovered);

            selected = new Sprite(ID + "Selected", 2, "Eon/Textures/Pixel",
                new Color(0, 255, 0, 200), true, position, this.size);

            AttachComponent(selected);

            CreateCells();
            ShowLoadingTools();
        }

        void Load(string controlID)
        {
            if (openDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SerializationHelper.CopyFile(openDia.FileName,
                    Environment.CurrentDirectory + "\\Content\\TileMaps", true);

                string[] split = openDia.FileName.Split(
                    new char[] { '\\', '/', '.' },
                    StringSplitOptions.RemoveEmptyEntries);

                switch (tilesSelected)
                {
                    case true:
                        {
                            tileSheetFilePath = "TileMaps\\" + split[split.Length - 2];

                            tiles = Common.ContentBuilder.Load<Texture2D>(tileSheetFilePath);

                            tileSheet.Texture = tiles;
                        }
                        break;

                    case false:
                        {
                            normalMapFilePath = "TileMaps\\" + split[split.Length - 2];

                            normalMap = Common.ContentBuilder.Load<Texture2D>(normalMapFilePath);

                            tileSheet.Texture = normalMap;
                        }
                        break;
                }

                HideLoadingTools();

                if (OnLoadedTilemap != null)
                    OnLoadedTilemap();
            }
        }

        void ShowLoadingTools()
        {
            grid.Disable();
            grid.Disable();
            tileSheet.Disable();
            hovered.Disable();
            selected.Disable();

            bg.Disable();

            loadBtn.Enable();
        }

        void HideLoadingTools()
        {
            grid.Enable();
            tileSheet.Enable();
            hovered.Enable();
            selected.Enable();

            bg.Enable();

            loadBtn.Disable();
        }

        protected override void Update()
        {
            if (cols * rows != cells.Count)
            {
                size = new Vector2()
                {
                    X = bounds.Width / cols,
                    Y = bounds.Height / rows
                };

                selected.Scale = size;
                hovered.Scale = size;

                CreateCells();
            }

            base.Update();
        }

        void CreateCells()
        {
            cells.Clear();

            selectedCell = 0;
            tempIndex = 0;

            Rectangle cellBounds = new Rectangle(bounds.Location.X,
                bounds.Location.Y, (int)size.X, (int)size.Y);

            int count = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    cells.Add(new Tile()
                    {
                        Bounds = cellBounds,
                        Index = count
                    });

                    count++;

                    cellBounds.X += (int)size.X;
                }

                cellBounds.Y += (int)size.Y;
                cellBounds.X = bounds.Location.X;
            }

            bg.Scale = new Vector2(grid.Width, grid.Height);
            tileSheet.Scale = new Vector2(grid.Width, grid.Height);
        }


        /// <summary>
        /// Sets the source index of the ImageGrid.
        /// </summary>
        /// <param name="index">The index of the ImageGrid.</param>
        public void SetSourceIndex(int index)
        {
            bool found = false;
            int idx = 0;

            while (idx < cells.Count && !found)
            {
                if (cells[idx].Index == index)
                    found = true;
                else
                    idx++;
            }

            selected.Offset = cells[idx].Bounds.Location.ToVector2();

            tempIndex = index;
            selectedCell = index;

            if (OnSourceChanged != null)
                OnSourceChanged(index);
        }

        /// <summary>
        /// Swaps between tile sheet and normal map.
        /// </summary>
        public void SwapImages()
        {
            tilesSelected = !tilesSelected;

            switch (tilesSelected)
            {
                case true:
                    {
                        if (tiles != null)
                        {
                            tileSheet.Texture = tiles;
                            HideLoadingTools();
                        }
                        else
                            ShowLoadingTools();
                    }
                    break;

                case false:
                    {
                        if (normalMap != null)
                        {
                            tileSheet.Texture = normalMap;
                            HideLoadingTools();
                        }
                        else
                            ShowLoadingTools();
                    }
                    break;
            }
        }

        protected override bool IsInside(Vector2 position)
        {
            if (grid.Enabled)
                if (EonMathsHelper.IsInsideOf(bounds, position))
                {
                    hovered.Enable();

                    bool found = false;
                    int idx = 0;

                    while (idx < cells.Count && !found)
                    {
                        if (EonMathsHelper.IsInsideOf(cells[idx].Bounds, position))
                        {
                            found = true;
                            hovered.Offset = cells[idx].Bounds.Location.ToVector2();
                            tempIndex = idx;
                        }
                        else
                            idx++;
                    }

                    return true;
                }
                else
                    hovered.Disable();

            return false;
        }

        public override void CheckClick()
        {
            selectedCell = tempIndex;
            selected.Offset = hovered.Offset;

            if (OnSourceChanged != null)
                OnSourceChanged(selectedCell);

            base.CheckClick();
        }
    }
}
