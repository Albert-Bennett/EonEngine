using Eon.Engine;
using Eon.Game2D.TileEngine.Management;
using Eon.Helpers;
using EonEngineTool.Lib;
using EonEngineTool.Lib.Controls;
using EonEngineTool.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace EonEngineTool.ObjectCreation.Tilemap
{
    public partial class TileMapForm : Form
    {
        string filepath;
        ImageBox image;

        TileMapDeffination map;

        int currentIdx = -1;

        public TileMapForm()
        {
            InitializeComponent();

            saveDia.InitialDirectory = Helper.InitialDirectory;
            saveDia.DefaultExt = "";
            saveDia.RestoreDirectory = true;
            saveDia.FileName = "";
            saveDia.Title = "Browse Files";

            string ext = Helper.TileMapExtenion;

            saveDia.Filter = "Tile Map Files (*" + ext + ")|*" + ext;
        }

        #region Utilities

        public void SetInfo(TileMapDeffination map, string filepath)
        {
            this.map = map;
            this.filepath = filepath;

            chkPostRender.Checked = map.PostRender;

            if (map.TileLayers != null && map.TileLayers.Length > 0)
            {
                for (int i = 0; i < map.TileLayers.Length; i++)
                    lstlayers.Items.Add("Draw Layer " + i);

                lstlayers.SelectedIndex = 0;
            }

            FrameworkCreation create = new FrameworkCreation();

            create.EngineComponents = new string[]
                    {
                        Helper.Physics2DAssembly,
                        Helper.Render2DManagerAssembly
                    };

            create.AssemblyRefferences = new string[]
                    {
                        Helper.Physics2DAssemblyRef,
                        Helper.Render2DAssemblyRef
                    };

            //XmlHelper.Serialize<FrameworkCreation>(create, Helper.BuildingContentFilepath
            //    + Helper.EngineManagerFilename, "");

            //ProcessHelper.StartProcessInsideOf("Testing.exe", flowShow.Handle);
        }

        private void tabSave_Click(object sender, EventArgs e)
        {
            Type[] extraTypes = new Type[]
            {
                typeof(Vector2),
                typeof(int[,]),
                typeof(TileLayerDeffination[]),
                typeof(int),
                typeof(string)
            };

            if (filepath == "")
            {
                if (saveDia.ShowDialog() == DialogResult.OK)
                {
                    filepath = saveDia.FileName + Helper.TileMapExtenion;

                    XmlHelper.Serialize<TileMapDeffination>(map, filepath, extraTypes);
                }
            }
            else
                XmlHelper.Serialize<TileMapDeffination>(map, filepath, extraTypes);
        }

        private void tabExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        #region Control Events

        private void chkPostRender_CheckedChanged(object sender, EventArgs e)
        {
            map.PostRender = chkPostRender.Checked;
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            float f = 0;

            if (float.TryParse(txtWidth.Text, out f))
            {
                Vector2 vec = map.TileLayers[currentIdx].TileSize;
                vec.X = f;

                map.TileLayers[currentIdx].TileSize = vec;
            }
            else
                txtWidth.Text = "" + map.TileLayers[currentIdx].TileSize.X;
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            float f = 0;

            if (float.TryParse(txtHeight.Text, out f))
            {
                Vector2 vec = map.TileLayers[currentIdx].TileSize;
                vec.Y = f;

                map.TileLayers[currentIdx].TileSize = vec;
            }
            else
                txtHeight.Text = "" + map.TileLayers[currentIdx].TileSize.Y;
        }

        private void lstlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstlayers.SelectedIndex != -1)
            {
                currentIdx = lstlayers.SelectedIndex;

                if (image == null)
                    image = new ImageBox(panelImg, map.TileLayers[currentIdx].TileSheetFilepath);
                else
                    image.ChangeImage(map.TileLayers[currentIdx].TileSheetFilepath);

                if (map.TileLayers[currentIdx].TileSize == null)
                    map.TileLayers[currentIdx].TileSize = Vector2.Zero;

                txtWidth.Text = "" + map.TileLayers[currentIdx].TileSize.X;
                txtHeight.Text = "" + map.TileLayers[currentIdx].TileSize.Y;
            }
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            TileLayerForm f2 = new TileLayerForm();
            f2.OnSend += new OnSendInfoEvent(SentBack);
            f2.SetInfo(map);
            f2.OnClose += new OnCloseEvent(FormClose);

            f2.Show();
        }

        void FormClose()
        {
            Enabled = true;
        }

        void SentBack(TileMapDeffination map)
        {
            Enabled = true;

            lstlayers.Items.Clear();

            SetInfo(map, filepath);
        }

        void btnTextEdittor_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            TextEdittor txt = new TextEdittor();
            txt.OnSendLines += new OnSendBackLinesEvent(SentLines);
            txt.OnClose += new OnCloseEvent(FormClose);

            string[] keys = new string[]
            {
                "Insert ',' to seperate tile numbers.",
                "# = No tile to be placed there."
            };

            txt.Keys = keys;

            txt.Show();
        }

        void SentLines(string[] lines)
        {
            List<string[]> locations;

            int cols = FindMaxLength(lines, out locations);

            map.TileLayers[currentIdx].Tiles = new int[locations.Count, cols];

            int max = map.TileLayers[currentIdx].TotalTileImages;

            for (int l = 0; l < locations.Count; l++)
                for (int i = 0; i < cols; i++)
                {
                    try
                    {
                        if (locations[l][i].Contains("#"))
                            map.TileLayers[currentIdx].Tiles[l, i] = -1;
                        else
                        {
                            int num = 0;

                            if (int.TryParse(locations[l][i], out num))
                                if (num < max)
                                    map.TileLayers[currentIdx].Tiles[l, i] = num;
                        }
                    }
                    catch
                    {
                        map.TileLayers[currentIdx].Tiles[l, i] = -1;
                    }
                }

            //Show tiles
        }

        int FindMaxLength(string[] lines, out List<string[]> locations)
        {
            locations = new List<string[]>();
            int max = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(new char[]
                {
                    ','
                });

                locations.Add(line);

                if (line.Length > max)
                    max = line.Length;
            }

            return max;
        }

        #endregion
    }
}
