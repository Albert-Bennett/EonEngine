/* Created 13/08/2015
 * Last Updated: 06/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Gui.Tilemap.Controls;
using Eon;
using Eon.Engine.Input;
using Eon.Game2D.TileEngine.Management;
using Eon.Helpers;
using Eon.Rendering2D;
using Eon.Rendering2D.Text;
using Eon.System.States;
using Eon.UIApi.Controls._2D;
using Eon.UIApi.Cursors;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace EEDK.Gui.Tilemap
{
    /// <summary>
    /// Defines the main screen of the Tilemap Editor.
    /// </summary>
    public sealed class MainScreen : MenuScreen
    {
        #region Tile Map Creation Tools

        TileMapDeffination tileMap;
        MapCreator mapCreator;
        int currentLayer = 0;

        ImageGrid grid;

        TextBox txtRows;
        TextBox txtCols;
        TextBox txtTilesX;
        TextBox txtTilesY;
        TextBox txtSizeX;
        TextBox txtSizeY;

        TextItem lblRows;
        TextItem lblCols;
        TextItem lblTilesX;
        TextItem lblTilesY;
        TextItem lblSizeX;
        TextItem lblSizeY;

        #endregion
        #region IO Controls

        Button2D btnSave;
        Button2D btnLoad;

        System.Windows.Forms.SaveFileDialog saveDia;
        System.Windows.Forms.OpenFileDialog openDia;

        #endregion
        #region  Render Specific

        CheckBox chkGrid;
        CheckBox chkLayers;

        bool renderSingle = true;

        /// <summary>
        /// Should the displayed TileMap be rendered 
        /// per layer or all togeather.
        /// </summary>
        public bool RenderSingle
        {
            get { return renderSingle; }
        }

        #endregion

        public MainScreen() : base("MainScreen", GameStates.Game) 
        {
            saveDia = new System.Windows.Forms.SaveFileDialog();
            saveDia.Filter = "TileMap File (*.Tiles)|*.Tiles";

            openDia = new System.Windows.Forms.OpenFileDialog();
            openDia.Filter = "TileMap File (*.Tiles)|*.Tiles";
        }

        protected override void Initialize()
        {
            #region Tile map

            tileMap = new TileMapDeffination();
            tileMap.PostRender = false;

            mapCreator = new MapCreator(this, ref tileMap);

            #endregion
            #region Misc

            new Cursor("Cursor", "GUI/Cursors/Cursor", 16);

            float initX = 966;

            AttachComponent(new Sprite(ID + "BG0", 0, "Eon/Textures/Pixel", new Color(46, 46, 46, 255),
                true, new Vector2(initX, 0), new Vector2(Common.TextureQuality.X - initX, Common.TextureQuality.Y)));

            AttachComponent(new Sprite(ID + "BG1", 0, "Eon/Textures/Pixel", new Color(96, 96, 96, 255),
                true, new Vector2(initX - 140, 0), new Vector2(140, Common.TextureQuality.Y)));

            grid = new ImageGrid("ImgGrid", this, new Vector2(1056, 120));

            grid.OnSourceChanged += new SourceTileChangedEvent(SourceTileChanged);
            grid.OnLoadedTilemap += new LoadedTilemapEvent(AddLayer);

            CheckBox chk = new CheckBox(ID + "chkSwapImg", this,
                new Rectangle(1322, 120, 32, 32), 2, true);

            chk.CheckedImage = "GUI/NormalMapIcon";
            chk.UnCheckedImage = "GUI/PictureIcon";
            chk.OnChecked += new OnCheckToogledEvent(SwapImages);

            chkLayers = new CheckBox(ID + "chkLayers", this,
                new Rectangle(1322, 160, 32, 32), 2, true);

            chkLayers.Checked = true;
            chkLayers.CheckedImage = "GUI/SingleLayerIcon";
            chkLayers.UnCheckedImage = "GUI/AllLayersIcon";
            chkLayers.OnChecked += new OnCheckToogledEvent(ChangeRendered);

            chkGrid = new CheckBox(ID + "chkGrid", this,
                new Rectangle(1322, 200, 32, 32), 2, true);

            chkGrid.Checked = true;
            chkGrid.CheckedImage = "GUI/GridOnIcon";
            chkGrid.UnCheckedImage = "GUI/GridOffIcon";
            chkGrid.OnChecked += new OnCheckToogledEvent(ToogleGrids);

            Button2D btnDelete = new Button2D(ID + "btnDelete", this,
                new Rectangle(1322, 345, 32, 32), "GUI/BinIcon", "", "", 2, true);

            btnDelete.HoveredOverColour = new Color(0, 200, 0, 255);
            btnDelete.Colour = Color.White;
            btnDelete.OnClicked += new OnClickedEvent(DeleteCurrentLayer);

            #endregion
            #region IO

            initX -= 110;

            btnSave = new Button2D("btnSave", this, new Rectangle((int)initX, 120, 80, 35),
                "Eon/Textures/Pixel", "Eon/Fonts/Arial23", "Save", 1, true);

            btnSave.FontColour = Color.White;
            btnSave.Colour = new Color(46, 46, 46, 255);
            btnSave.HoveredOverColour = new Color(0, 200, 0, 255);
            btnSave.OnClicked += new OnClickedEvent(Save);

            btnLoad = new Button2D("btnLoad", this, new Rectangle((int)initX, 170, 80, 35),
                "Eon/Textures/Pixel", "Eon/Fonts/Arial23", "Load", 1, true);

            btnLoad.FontColour = Color.White;
            btnLoad.Colour = new Color(46, 46, 46, 255);
            btnLoad.HoveredOverColour = new Color(0, 200, 0, 255);
            btnLoad.OnClicked += new OnClickedEvent(Load);

            AttachComponent(new TextItem(ID + "lblLayers", 1, "Layers",
                "Eon/Fonts/Arial23", new Vector2(initX, 260), Color.White, true));

            initX += 24;
            int initY = 306;

            for (int i = 0; i < 10; i++)
            {
                CheckBox chkLayerBtn = new CheckBox(i + "", this,
                    new Rectangle((int)initX, initY, 34, 40), 1, true);

                chkLayerBtn.UnCheckedImage = "GUI/TileMap/" + i;
                chkLayerBtn.CheckedImage = "GUI/TileMap/" + i + "Select";

                if (i == 0)
                    chkLayerBtn.Checked = true;

                chkLayerBtn.OnChecked += new OnCheckToogledEvent(ChangeTileLayer);

                initY += 40;
            }

            #endregion
            #region Rows/ Columns

            lblRows = new TextItem(ID + "lblRows", 3, "Rows",
                "GUI/Verdana12pt", new Vector2(1056, 403), Color.White, true);

            AttachComponent(lblRows);

            txtRows = new TextBox("txtRows", this,
                new Vector2(1181, 403), new Vector2(130, 25));

            txtRows.Text = "1";
            txtRows.OnTextChanged += new OnTextChangedEvent(TextChanged);

            lblCols = new TextItem(ID + "lblCols", 3, "Columns",
                "GUI/Verdana12pt", new Vector2(1056, 446), Color.White, true);

            AttachComponent(lblCols);

            txtCols = new TextBox("txtCols", this,
                new Vector2(1181, 446), new Vector2(130, 25));

            txtCols.Text = "1";
            txtCols.OnTextChanged += new OnTextChangedEvent(TextChanged);

            #endregion
            #region Tiles

            lblTilesX = new TextItem(ID + "lblTilesX", 3, "Tiles X",
                "GUI/Verdana12pt", new Vector2(1056, 533), Color.White, true);

            AttachComponent(lblTilesX);

            txtTilesX = new TextBox("txtTilesX", this,
                new Vector2(1181, 533), new Vector2(130, 25));

            txtTilesX.Text = "1";
            txtTilesX.OnTextChanged += new OnTextChangedEvent(TextChanged);

            #region Plus / Minus Buttons

            Button2D btnTilesXMinus = new Button2D("btnTilesXMinus", this,
                new Rectangle(1148, 533, 25, 25), "GUI/MinusIcon", "", "", 3, true);

            btnTilesXMinus.HoveredOverColour = Color.Green;
            btnTilesXMinus.OnClicked += new OnClickedEvent(TilesChangeClicked);

            Button2D btnTilesXPlus = new Button2D("btnTilesXPlus", this,
                new Rectangle(1320, 533, 25, 25), "GUI/PlusIcon", "", "", 3, true);

            btnTilesXPlus.HoveredOverColour = Color.Green;
            btnTilesXPlus.OnClicked += new OnClickedEvent(TilesChangeClicked);

            #endregion

            lblTilesY = new TextItem(ID + "lblTilesY", 3, "Tiles Y",
                 "GUI/Verdana12pt", new Vector2(1056, 576), Color.White, true);

            AttachComponent(lblTilesY);

            txtTilesY = new TextBox("txtTilesY", this,
                new Vector2(1181, 576), new Vector2(130, 25));

            txtTilesY.Text = "1";
            txtTilesY.OnTextChanged += new OnTextChangedEvent(TextChanged);

            #region Plus / Minus Buttons

            Button2D btnTilesYMinus = new Button2D("btnTilesYMinus", this,
                new Rectangle(1148, 576, 25, 25), "GUI/MinusIcon", "", "", 3, true);

            btnTilesYMinus.HoveredOverColour = Color.Green;
            btnTilesYMinus.OnClicked += new OnClickedEvent(TilesChangeClicked);

            Button2D btnTilesYPlus = new Button2D("btnTilesYPlus", this,
                new Rectangle(1320, 576, 25, 25), "GUI/PlusIcon", "", "", 3, true);

            btnTilesYPlus.HoveredOverColour = Color.Green;
            btnTilesYPlus.OnClicked += new OnClickedEvent(TilesChangeClicked);

            #endregion

            #endregion
            #region Tile Size

            lblSizeX = new TextItem(ID + "lblSizeX", 3, "Tile Size X",
                "GUI/Verdana12pt", new Vector2(1056, 663), Color.White, true);

            AttachComponent(lblSizeX);

            txtSizeX = new TextBox("txtSizeX", this,
                new Vector2(1181, 663), new Vector2(130, 25));

            txtSizeX.Text = "128";
            txtSizeX.OnTextChanged += new OnTextChangedEvent(TextChanged);

            lblSizeY = new TextItem(ID + "lblSizeY", 3, "Tile Size Y",
                 "GUI/Verdana12pt", new Vector2(1056, 706), Color.White, true);

            AttachComponent(lblSizeY);

            txtSizeY = new TextBox("txtSizeY", this,
                new Vector2(1181, 706), new Vector2(130, 25));

            txtSizeY.Text = "128";
            txtSizeY.OnTextChanged += new OnTextChangedEvent(TextChanged);

            #endregion

            base.Initialize();
        }

        void TilesChangeClicked(string controlID)
        {
            TileLayerDeffination layer = tileMap.GetByOrder(currentLayer);

            switch (controlID)
            {
                case "btnTilesXPlus":
                    {
                        int parse = int.Parse(txtTilesX.Text);
                        parse++;

                        txtTilesX.Text = parse + "";
                        layer.UpdateTiles(parse, true);
                    }
                    break;

                case "btnTilesXMinus":
                    {
                        int parse = int.Parse(txtTilesX.Text);
                        parse--;

                        if (parse >= 0)
                        {
                            txtTilesX.Text = parse + "";
                            layer.UpdateTiles(parse, true);
                        }
                    }
                    break;

                case "btnTilesYPlus":
                    {
                        int parse = int.Parse(txtTilesY.Text);
                        parse++;

                        txtTilesY.Text = parse + "";
                        layer.UpdateTiles(parse, false);
                    }
                    break;

                case "btnTilesYMinus":
                    {
                        int parse = int.Parse(txtTilesY.Text);
                        parse--;

                        if (parse >= 0)
                        {
                            txtTilesY.Text = parse + "";
                            layer.UpdateTiles(parse, false);
                        }
                    }
                    break;
            }
        }

        void DeleteCurrentLayer(string controlID)
        {
            if (tileMap.DeleteTileLayer(currentLayer))
            {
                mapCreator.DeleteLayer(currentLayer);

                txtCols.Text = "1";
                txtRows.Text = "1";
                txtTilesX.Text = "1";
                txtTilesY.Text = "1";
                txtSizeX.Text = "128";
                txtSizeY.Text = "128";

                grid.Columns = 1;
                grid.Rows = 1;

                grid.TileSheetFilepath = "null";
                grid.NormalMapFilepath = "null";
            }
        }

        void ToogleGrids(string controlID, bool check)
        {
            mapCreator.ToogleGrids();
        }

        void ChangeRendered(string controlID, bool check)
        {
            renderSingle = check;
        }

        void AddLayer()
        {
            TileLayerDeffination layer = new TileLayerDeffination()
            {
                Order = currentLayer,
                Columns = grid.Columns,
                Rows = grid.Rows,
                TileSize = new Vector2(float.Parse(txtSizeX.Text),
                    float.Parse(txtSizeY.Text)),
                TileOffset = -new Vector2(2),
                TileSheetFilepath = grid.TileSheetFilepath,
                NormalMapFilepath = grid.NormalMapFilepath
            };

            tileMap.AddTileLayer(layer);

            mapCreator.AddLayer(currentLayer);
            mapCreator.ChangeLayer(currentLayer);
        }

        void SwapImages(string controlID, bool newValue)
        {
            grid.SwapImages();
        }

        void ChangeTileLayer(string controlID, bool check)
        {
            CheckBox chk = GetControl(currentLayer + "") as CheckBox;

            if (chk.Checked)
                chk.Toggle(false);

            currentLayer = int.Parse(controlID);

            if (tileMap.TileLayerExists(currentLayer))
            {
                mapCreator.ChangeLayer(currentLayer);

                SetControlData(currentLayer);
            }
            else
            {
                mapCreator.ChangeLayer(currentLayer);

                SetControlData(-1);
            }
        }

        void SetControlData(int index)
        {
            if (index != -1)
            {
                TileLayerDeffination layer = tileMap.GetByOrder(index);

                txtCols.Text = layer.Columns + "";
                txtRows.Text = layer.Rows + "";
                txtSizeX.Text = layer.TileSize.X + "";
                txtSizeY.Text = layer.TileSize.Y + "";

                if (layer.Tiles != null)
                {
                    txtTilesX.Text = layer.Tiles[0].Length + "";
                    txtTilesY.Text = layer.Tiles.Count + "";
                }
                else
                {
                    txtTilesX.Text = "1";
                    txtTilesY.Text = "1";
                }

                grid.Columns = layer.Columns;
                grid.Rows = layer.Rows;

                if (layer.NormalMapFilepath != "")
                    grid.NormalMapFilepath = layer.NormalMapFilepath;
                else
                    grid.NormalMapFilepath = "null";

                if (layer.TileSheetFilepath != "")
                    grid.TileSheetFilepath = layer.TileSheetFilepath;
                else
                    grid.TileSheetFilepath = "null";

                grid.SetSourceIndex(0);
            }
            else
            {
                txtCols.Text = "1";
                txtRows.Text = "1";

                txtSizeX.Text = "128";
                txtSizeY.Text = "128";

                txtTilesX.Text = "1";
                txtTilesY.Text = "1";

                grid.Columns = 1;
                grid.Rows = 1;

                grid.TileSheetFilepath = "null";
                grid.NormalMapFilepath = "null";

                grid.SetSourceIndex(0);
            }
        }

        void Load(string controlID)
        {
            if (openDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(List<int[]>),
                    typeof(TileMapDeffination),
                    typeof(TileMapDeffination[]),
                    typeof(bool),
                    typeof(Vector2)
                };

                tileMap = SerializationHelper.Deserialize<TileMapDeffination>(openDia.FileName, false, "");
                mapCreator.SetTileMap(tileMap);

                mapCreator.DeleteAll();
                currentLayer = -1;

                if (tileMap.TileLayers.Length > 0)
                {
                    for (int i = 0; i < tileMap.TileLayers.Length; i++)
                        mapCreator.AddLayer(tileMap.TileLayers[i].Order);

                    currentLayer = tileMap.TileLayers[0].Order;
                    mapCreator.ChangeLayer(currentLayer);

                    SetControlData(currentLayer);
                }
                else
                    SetControlData(-1);
            }
        }

        void Save(string controlID)
        {
            if (saveDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(List<int[]>),
                    typeof(TileMapDeffination),
                    typeof(TileMapDeffination[]),
                    typeof(bool),
                    typeof(Vector2)
                };

                SerializationHelper.Serialize<TileMapDeffination>(
                    tileMap, saveDia.FileName, "", extraTypes); 
            }
        }

        void SourceTileChanged(int index)
        {
            mapCreator.ChangeSourceIndex(index);
        }

        void TextChanged(string controlID, string text)
        {
            int value = 0;

            TileLayerDeffination layer = tileMap.GetByOrder(currentLayer);

            if (int.TryParse(text, out value))
            {
                switch (controlID)
                {
                    case "txtRows":
                        {
                            grid.Rows = value;

                            if (layer != null)
                                layer.Rows = value;
                        }
                        break;

                    case "txtCols":
                        {
                            grid.Columns = value;

                            if (layer != null)
                                layer.Columns = value;
                        }
                        break;

                    case "txtSizeX":
                        {

                            if (layer != null)
                                layer.TileSize.X = value;
                        }
                        break;


                    case "txtSizeY":
                        {
                            if (layer != null)
                                layer.TileSize.Y = value;
                        }
                        break;

                    case "txtTilesX":
                        {
                            if (layer != null)
                                layer.UpdateTiles(value, true);
                        }
                        break;

                    case "txtTilesY":
                        {
                            if (layer != null)
                                layer.UpdateTiles(value, false);
                        }
                        break;
                }
            }
        }

        protected override void HandleInput()
        {
            if (CurrentControl != null)
                if (InputManager.IsMouseButtonStroked(MouseButtons.Left))
                    CurrentControl.CheckClick();
                else
                    if (CurrentControl.ID == "Layer" + mapCreator.CurrentTileLayer)
                    {
                        if (InputManager.IsMouseButtonStroked(MouseButtons.Right))
                            mapCreator.ChangeSourceIndex(-1);

                        if (InputManager.IsMouseButtonPressed(MouseButtons.Left))
                            CurrentControl.CheckClick();
                    }

            if (InputManager.IsKeyStroked(Keys.H))
                chkGrid.Toggle(true);

            if (InputManager.IsKeyStroked(Keys.L))
                chkLayers.Toggle(true);

            base.HandleInput();
        }
    }
}
