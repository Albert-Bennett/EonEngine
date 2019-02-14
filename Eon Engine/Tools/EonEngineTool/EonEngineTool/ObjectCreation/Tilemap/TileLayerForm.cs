using Eon.Game2D.TileEngine.Management;
using Eon.Helpers;
using EonEngineTool.Lib;
using EonEngineTool.Lib.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Windows.Forms;

namespace EonEngineTool.ObjectCreation.Tilemap
{
    public partial class TileLayerForm : Form
    {
        IndexedImageBox image;

        TileMapDeffination map;

        int currentIdx = -1;

        public OnSendInfoEvent OnSend;
        public OnCloseEvent OnClose;

        public TileLayerForm()
        {
            InitializeComponent();

            openDia.InitialDirectory = Helper.InitialDirectory;
            openDia.DefaultExt = "";
            openDia.RestoreDirectory = true;
            openDia.Multiselect = false;
            openDia.FileName = "";
            openDia.Title = "Browse Image Files";

            string ext = "";
            string[] extentions = Helper.ImageExtentions;

            for (int i = 0; i < extentions.Length; i++)
            {
                string start = extentions[i] +
                    " Files (*" + extentions[i] + ")|*" + extentions[i];

                if (i != extentions.Length - 1)
                    start += "|";

                ext += start;
            }

            openDia.Filter = ext;
        }

        #region Utilities

        public void SetInfo(TileMapDeffination map)
        {
            this.map = map;

            if (map.TileLayers != null && map.TileLayers.Length > 0)
            {
                for (int i = 0; i < map.TileLayers.Length; i++)
                    lstLyrs.Items.Add("Draw Layer " + i);

                currentIdx = 0;
                lstLyrs.SelectedIndex = currentIdx;
            }
        }

        void DisplayLayerInfo()
        {
            if (map.TileLayers.Length > 0 && map.TileLayers != null)
            {
                txtCols.Text = "" + map.TileLayers[currentIdx].Columns;
                txtRows.Text = "" + map.TileLayers[currentIdx].Rows;
                txtTotal.Text = "" + map.TileLayers[currentIdx].TotalTileImages;

                string none = "None";

                if (map.TileLayers[currentIdx].NormalMapFilepath != none &&
                    map.TileLayers[currentIdx].NormalMapFilepath != string.Empty)
                    txtNormal.Text = map.TileLayers[currentIdx].NormalMapFilepath;
                else
                    txtNormal.Text = none;

                if (map.TileLayers[currentIdx].DistortionMapFilepath != none &&
                    map.TileLayers[currentIdx].DistortionMapFilepath != string.Empty)
                    txtDistortion.Text = map.TileLayers[currentIdx].DistortionMapFilepath;
                else
                    txtDistortion.Text = none;

                if (map.TileLayers[currentIdx].TileSheetFilepath != none &&
                    map.TileLayers[currentIdx].TileSheetFilepath != string.Empty)
                {
                    txtNum.Text = "" + currentIdx;

                    if (image != null)
                        image.ChangeImage(map.TileLayers[currentIdx].TileSheetFilepath);
                    else
                        image = new IndexedImageBox(panel, map.TileLayers[currentIdx].TileSheetFilepath,
                             map.TileLayers[currentIdx].Rows,
                             map.TileLayers[currentIdx].Columns,
                             map.TileLayers[currentIdx].TotalTileImages);
                }
                else
                    txtNum.Text = "Invalid Layer";

                if (map.TileLayers[currentIdx].TileOffset == null)
                    map.TileLayers[currentIdx].TileOffset = Vector2.Zero;

                txtOffsetX.Text = "" + map.TileLayers[currentIdx].TileOffset.X;
                txtOffsetY.Text = "" + map.TileLayers[currentIdx].TileOffset.Y;
            }
            else
                if (image != null)
                    image.Destroy();
        }

        void SetLayerInfo()
        {
            int temp = 0;

            if (int.TryParse(txtTotal.Text, out temp))
                map.TileLayers[currentIdx].TotalTileImages = temp;

            if (int.TryParse(txtCols.Text, out temp))
                map.TileLayers[currentIdx].Columns = temp;

            if (int.TryParse(txtRows.Text, out temp))
                map.TileLayers[currentIdx].Rows = temp;

            map.TileLayers[currentIdx].NormalMapFilepath = txtNormal.Text;
            map.TileLayers[currentIdx].DistortionMapFilepath = txtDistortion.Text;
        }

        private void tabExit_Click(object sender, EventArgs e)
        {
            if (OnClose != null)
                OnClose();

            this.Close();
        }

        private void tabSave_Click(object sender, EventArgs e)
        {
            if (OnSend != null)
                OnSend(map);

            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (image.Hidden)
                image.Show();
            else
                image.Hide();
        }

        #endregion
        #region Control Events

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == DialogResult.OK)
            {
                TileLayerDeffination deff = new TileLayerDeffination();
                deff.TileSheetFilepath = openDia.FileName;

                map.TileLayers = ArrayHelper.AddItem<TileLayerDeffination>(deff, map.TileLayers);

                string id = "Draw Layer " + (map.TileLayers.Length - 1);

                if (lstLyrs.Items.Contains(id))
                {
                    int idx = 1;

                    while (lstLyrs.Items.Contains(id + "_" + idx))
                        idx++;

                    lstLyrs.Items.Add(id + "_" + idx);
                }
                else
                    lstLyrs.Items.Add(id);
            }
        }

        void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstLyrs.SelectedIndex != -1)
            {
                map.TileLayers = ArrayHelper.RemoveAt<TileLayerDeffination>(currentIdx, map.TileLayers);
                lstLyrs.Items.RemoveAt(lstLyrs.SelectedIndex);

                if (map.TileLayers.Length == 0)
                {
                    ClearLayerInfo();

                    currentIdx = -1;
                    lstLyrs.SelectedIndex = -1;
                }
                else if (lstLyrs.SelectedIndex == currentIdx)
                {
                    int idx = currentIdx;

                    if (currentIdx == map.TileLayers.Length)
                        lstLyrs.SelectedIndex--;
                    else
                        lstLyrs.SelectedIndex++;
                }
            }
        }

        void lstLyrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLyrs.SelectedIndex != -1)
            {
                if (currentIdx < map.TileLayers.Length)
                    SetLayerInfo();

                currentIdx = lstLyrs.SelectedIndex;

                DisplayLayerInfo();
            }
        }

        void ClearLayerInfo()
        {
            txtCols.Text = "";
            txtRows.Text = "";
            txtTotal.Text = "";
            txtNormal.Text = "None";
            txtDistortion.Text = "None";
            txtNum.Text = "";

            if (image != null)
                image.Destroy();
        }

        private void txtNormal_Click(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == DialogResult.OK)
                txtNormal.Text = openDia.FileName;
        }

        private void txtDistortion_Click(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == DialogResult.OK)
                txtDistortion.Text = openDia.FileName;
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if (image != null)
            {
                int temp = 0;

                if (int.TryParse(txtTotal.Text, out temp))
                    image.ChangeTotalFrames(temp);
            }
        }

        private void txtRows_TextChanged(object sender, EventArgs e)
        {
            if (image != null)
            {
                int temp = 0;

                if (int.TryParse(txtRows.Text, out temp))
                    image.ChangeRows(temp);
            }
        }

        private void txtCols_TextChanged(object sender, EventArgs e)
        {
            if (image != null)
            {
                int temp = 0;

                if (int.TryParse(txtCols.Text, out temp))
                    image.ChangeColumns(temp);
            }
        }

        private void txtOffsetX_TextChanged(object sender, EventArgs e)
        {
            float f = 0;

            if (float.TryParse(txtOffsetX.Text, out f))
            {
                Vector2 vec = map.TileLayers[currentIdx].TileOffset;
                vec.X = f;

                map.TileLayers[currentIdx].TileOffset = vec;
            }
            else
                txtOffsetX.Text = "" + map.TileLayers[currentIdx].TileOffset.X;
        }

        private void txtOffsetY_TextChanged(object sender, EventArgs e)
        {
            float f = 0;

            if (float.TryParse(txtOffsetY.Text, out f))
            {
                Vector2 vec = map.TileLayers[currentIdx].TileOffset;
                vec.Y = f;

                map.TileLayers[currentIdx].TileOffset = vec;
            }
            else
                txtOffsetY.Text = "" + map.TileLayers[currentIdx].TileOffset.Y;
        }

        #endregion
    }
}
