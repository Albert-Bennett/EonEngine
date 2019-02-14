/* Created 14/08/2015
 * Last Updated: 16/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Framework.Primatives;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Rendering2D.Framework.Misc
{
    /// <summary>
    /// Defines a graphical representation
    /// of a grid that is made up of cells.
    /// </summary>
    public sealed class Grid : ObjectComponent, IUpdate
    {
        int drawLayer;
        bool postRender;

        int cellSizeX;
        int cellSizeY;

        int columns = 1;
        int rows = 1;

        int lineThickness = 2;

        Vector2 initialPos;

        Color colour = Color.Black;

        bool renderDisabled = false;

        List<Line> lines = new List<Line>();

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Defines the thickness of each 
        /// line in that makes up the Grid.
        /// </summary>
        public int LineThickness
        {
            get { return lineThickness; }
            set 
            {
                lineThickness = value;

                for (int i = 0; i < lines.Count; i++)
                    lines[i].Thickness = value;
            }
        }

        /// <summary>
        /// Number of columns in the Grid.
        /// </summary>
        public int Columns
        {
            get { return columns; }
            set
            {
                if (value > 0)
                    columns = value;
            }
        }

        /// <summary>
        /// The width of each cell in the Grid.
        /// </summary>
        public int CellSizeX
        {
            get { return cellSizeX; }
            set { cellSizeX = value; }
        }

        /// <summary>
        /// The height of each cell in the Grid.
        /// </summary>
        public int CellSizeY
        {
            get { return cellSizeY; }
            set { cellSizeY = value; }
        }

        /// <summary>
        /// Number of rows in the Grid.
        /// </summary>
        public int Rows
        {
            get { return rows; }
            set
            {
                if (value > 0)
                    rows = value;
            }
        }

        /// <summary>
        /// The width of the Grid.
        /// </summary>
        public int Width
        {
            get { return columns * cellSizeX; }
        }

        /// <summary>
        /// The height of the Grid.
        /// </summary>
        public int Height
        {
            get { return rows * cellSizeY; }
        }

        /// <summary>
        /// The position of the Grid.
        /// </summary>
        public Vector2 Position
        {
            get { return initialPos; }
        }

        /// <summary>
        /// The colour of the Grid.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set
            { 
                colour = value;

                for (int i = 0; i < lines.Count; i++)
                    lines[i].Colour = value;
            }
        }

        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

        /// <summary>
        /// Creates a new Grid.
        /// </summary>
        /// <param name="id">The ID of the Grid.</param>
        /// <param name="position">The position of the Grid.</param>
        /// <param name="columns">The number of columns in the Grid.</param>
        /// <param name="rows">The number of rows in the Grid.</param>
        /// <param name="cellSizeX">The size of the Cells along the X-axis.</param>
        /// <param name="cellSizeY">The size of the Cells along the Y-axis.</param>
        public Grid(string id, Vector2 position,int columns,
            int rows, int cellSizeX, int cellSizeY, 
            int drawLayer, bool postRender)
            : base(id)
        {
            this.initialPos = position;

            this.columns = columns;
            this.rows = rows;

            this.cellSizeX = cellSizeX;
            this.cellSizeY = cellSizeY;

            this.drawLayer = drawLayer;
            this.postRender = postRender;
        }

        protected override void Initialize()
        {
            CreateCells();

            base.Initialize();
        }

        public void _Update()
        {
            if (columns * rows != lines.Count)
            {
                for (int i = 0; i < lines.Count; i++)
                    lines[i].Destroy();

                lines.Clear();

                CreateCells();
            }
        }

        public void _PostUpdate() { }

        void CreateCells()
        {
            int width = Width;
            int height = Height;

            Vector2 startPos = initialPos;
            Vector2 endPos = new Vector2(initialPos.X, initialPos.Y + height);

            for (int i = 0; i <= columns; i++)
            {
                Line l = new Line(startPos, endPos,
                    lineThickness, drawLayer, postRender);

                if (!Enabled)
                    l.Disable();

                lines.Add(l);

                startPos.X += cellSizeX;
                endPos.X += cellSizeX;
            }

            startPos.X = initialPos.X;
            endPos.X = initialPos.X + width;
            endPos.Y = startPos.Y;

            for (int i = 0; i <= rows; i++)
            {
                Line l = new Line(startPos, endPos,
                    lineThickness, drawLayer, postRender);

                if (!Enabled)
                    l.Disable();

                lines.Add(l);

                startPos.Y += cellSizeY;
                endPos.Y += cellSizeY;
            }
        }

        public override void Disable()
        {
            for (int i = 0; i < lines.Count; i++)
                lines[i].Disable();

            base.Disable();
        }

        public override void Enable()
        {
            for (int i = 0; i < lines.Count; i++)
                lines[i].Enable();

            base.Enable();
        }

        public override void Destroy(bool remove)
        {
            for (int i = 0; i < lines.Count; i++)
                lines[i].Destroy();

            lines.Clear();

            base.Destroy(remove);
        }
    }
}
