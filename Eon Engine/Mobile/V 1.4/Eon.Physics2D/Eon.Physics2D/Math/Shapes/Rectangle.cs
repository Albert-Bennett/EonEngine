/* Created 14/04/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Math.Shapes
{
    /// <summary>
    /// Used to define a Rectangle ConvexShape.
    /// </summary>
    public sealed class Rectangle : ConvexShape
    {
        Vector2[] vertices;

        float x;
        float y;
        float width;
        float height;

        /// <summary>
        /// The Rectangle's X co-ordinate.
        /// </summary>
        public float X
        {
            get { return x; }
            set
            {
                x = value;

                CreateVertices();
            }
        }

        /// <summary>
        /// The Rectangle's Y co-ordinate.
        /// </summary>
        public float Y
        {
            get { return y; }
            set
            {
                y = value;

                CreateVertices();
            }
        }

        /// <summary>
        /// The width of the Rectangle.
        /// </summary>
        public float Width
        {
            get { return width; }
        }

        /// <summary>
        /// The height of the Rectangle.
        /// </summary>
        public float Height
        {
            get { return height; }
        }

        /// <summary>
        /// The position of the Rectangle.
        /// </summary>
        public Vector2 Position
        {
            get { return new Vector2(x, y); }
            set
            {
                x = value.X;
                y = value.Y;

                CreateVertices();
            }
        }

        /// <summary>
        /// The right co-ordinate of the Rectangle.
        /// </summary>
        public float Right
        {
            get { return vertices[3].X; }
        }

        /// <summary>
        /// The bottom co-ordinate of the Rectangle.
        /// </summary>
        public float Bottom
        {
            get { return vertices[3].Y; }
        }

        /// <summary>
        /// The vertices that make up the Rectangle.
        /// </summary>
        public override Vector2[] Vertices
        {
            get { return vertices; }
        }

        /// <summary>
        /// Creates a new Rectangle.
        /// </summary>
        /// <param name="x">The Rectangle's X co-ordinate.</param>
        /// <param name="y">The Rectangle's Y co-ordinate.</param>
        /// <param name="width">The width of the Rectangle.</param>
        /// <param name="height">The height of the Rectangle.</param>
        public Rectangle(float x, float y, float width, float height)
        {
            this.height = height;
            this.x = x;
            this.y = y;
            this.width = width;

            CreateVertices();
        }

        protected override void CreateVertices()
        {
            vertices = new Vector2[4];

            vertices[0] = new Vector2(X, Y);
            vertices[1] = new Vector2(X + Width, Y);
            vertices[2] = new Vector2(X, Y + Height);
            vertices[3] = new Vector2(X + Width, Y + Height);

            base.CreateVertices();
        }
    }
}
