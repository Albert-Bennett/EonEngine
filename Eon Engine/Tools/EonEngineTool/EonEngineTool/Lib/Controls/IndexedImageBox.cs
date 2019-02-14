using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EonEngineTool.Lib.Controls
{
    /// <summary>
    /// Defines a control that show the indexes of an image.
    /// </summary>
    public class IndexedImageBox : ImageBox
    {
        List<Label> indexes = new List<Label>();

        int rows;
        int cols;
        int totalFrames;

        Size cellSize;

        bool hidden = false;

        public bool Hidden
        {
            get { return hidden; }
        }

        public IndexedImageBox(FlowLayoutPanel parent, string filepath,
            int rows, int columns, int totalFrames)
            : base(parent, filepath)
        {
            this.rows = rows;
            this.cols = columns;
            this.totalFrames = totalFrames;

            CreateIndices();
        }

        public override void ChangeImage(string filepath)
        {
            rows = 0;
            cols = 0;
            totalFrames = 0;

            ClearIndexing();

            base.ChangeImage(filepath);
        }

        public void ChangeRows(int rows)
        {
            this.rows = rows;

            CreateIndices();
        }

        public void ChangeColumns(int columns)
        {
            this.cols = columns;

            CreateIndices();
        }

        public void ChangeTotalFrames(int frames)
        {
            totalFrames = frames;

            CreateIndices();
        }

        public void Hide()
        {
            if (!hidden)
            {
                hidden = true;

                for (int i = 0; i < indexes.Count; i++)
                    indexes[i].Hide();
            }
        }

        public void Show()
        {
            if (hidden)
            {
                hidden = false;

                for (int i = 0; i < indexes.Count; i++)
                    indexes[i].Show();
            }
        }

        void CreateIndices()
        {
            if (rows != 0 && cols != 0)
            {
                ClearIndexing();

                cellSize = new Size()
                {
                    Height = imagePanel.Size.Height / rows,
                    Width = imagePanel.Size.Width / cols
                };

                int count = 0;

                int halfX = cellSize.Width / 2;
                int halfY = cellSize.Height / 2;

                Point initialPos = imagePanel.Location;
                initialPos += new Size(halfX, halfY);

                Point pos = initialPos;

                for (int r = 0; r < rows; r++)
                {
                    if (r == 0)
                        pos.Y = initialPos.Y;
                    else
                        pos.Y += cellSize.Height;

                    for (int c = 0; c < cols; c++)
                        if (count < totalFrames)
                        {
                            Label lab = ControlCreator.CreateLabel("" + count, DockStyle.None);

                            if (c == 0)
                                pos.X = initialPos.Y;
                            else
                                pos.X += cellSize.Width;

                            lab.Location = pos;

                            lab.ForeColor = Color.Black;
                            lab.Font = new Font("Furore", cellSize.Height / 5);
                            lab.AutoSize = true;

                            if (hidden)
                                lab.Hide();

                            indexes.Add(lab);
                            imagePanel.Controls.Add(lab);

                            count++;
                        }
                }
            }
        }

        public override FlowLayoutPanel Destroy()
        {
            ClearIndexing();

            return base.Destroy();
        }

        void ClearIndexing()
        {
            for (int i = 0; i < indexes.Count; i++)
                imagePanel.Controls.Remove(indexes[i]);

            indexes.Clear();
        }
    }
}
