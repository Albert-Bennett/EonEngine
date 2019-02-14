/* Created: 10/06/2013
 * Last Updated: 17/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering2D.Cameras
{
    /// <summary>
    /// Defines a 2D camera.
    /// </summary>
    public class BaseCamera2D : ObjectComponent
    {
        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan shakeTime = TimeSpan.Zero;

        float magnitude;

        Rectangle view;
        protected Vector2 position;
        Vector2 origin;

        protected float zoom;
        protected float speed;
        protected float rotation;

        protected float maxSpeed = 6;
        protected float minSpeed = 4;

        float maxZoom = 1.25f;
        float minZoom = 0.5f;
        float zoomRate = 0.01f;

        protected Rectangle constrain;

        /// <summary>
        /// The position of the camera (top corner).
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value + new Vector2(view.Width, view.Height) / 2;

                LockCamera();
            }
        }

        /// <summary>
        /// The direction that the BaseCamera is moving in.
        /// </summary>
        public Vector2 Direction
        {
            get { return Vector2.Normalize(Position); }
        }

        /// <summary>
        /// The speed of the BaseCamera.
        /// </summary>
        public float Speed
        {
            get { return speed; }
            set
            {
                speed = (float)MathHelper.Clamp(value, 1, 16);
            }
        }

        /// <summary>
        /// The rotation of the BaseCamera2D.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// How far the BaseCamera is zoomed in.
        /// </summary>
        public float Zoom
        {
            get { return zoom; }
        }

        /// <summary>
        /// The rate at which the BaseCamera2D zooms at.
        /// </summary>
        public float ZoomRate
        {
            get { return zoomRate; }
            set { zoomRate = value; }
        }

        /// <summary>
        /// The maximum zoom amount.
        /// </summary>
        public float MaxZoom
        {
            get { return maxZoom; }
            set { maxZoom = value; }
        }

        /// <summary>
        /// The minimum zoom amount.
        /// </summary>
        public float MinZoom
        {
            get { return minZoom; }
            set { minZoom = value; }
        }

        /// <summary>
        /// The inverse zoom of the BaseCamera.
        /// </summary>
        public float InverseZoom
        {
            get { return maxZoom / zoom; }
        }

        /// <summary>
        /// The BaseCamera's view matrix. 
        /// </summary>
        public Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-position, 0)) *
                       Matrix.CreateTranslation(new Vector3(-origin, 0)) *
                       Matrix.CreateRotationZ(rotation) *
                       Matrix.CreateScale(zoom, zoom, 1) *
                       Matrix.CreateTranslation(new Vector3(origin, 0));
            }
        }

        /// <summary>
        /// The bounds of the view of the BaseCamera.
        /// </summary>
        public Rectangle View
        {
            get { return view; }
        }

        /// <summary>
        /// Returns the constraining rectangle for the camera.
        /// </summary>
        public Rectangle Constraint
        {
            get
            {
                if (constrain != Rectangle.Empty)
                    return constrain;
                else
                    return Rectangle.Empty;
            }
        }

        /// <summary>
        /// Creates a new BaseCamera.
        /// </summary>
        /// <param name="id">The ID of the BaseCamera.</param>
        /// <param name="bounds">The bounds of the BaseCamera2D.</param>
        public BaseCamera2D(string id, Rectangle bounds)
            : base(id)
        {
            zoom = 1;

            origin = bounds.Size.ToVector2() / 2;
            this.position = bounds.Location.ToVector2();

            view = bounds;
            view.X = (int)position.X;
            view.Y = (int)position.Y;

            CameraManager2D.AddCamera(this);
        }

        /// <summary>
        /// Creates a new BaseCamera.
        /// </summary>
        /// <param name="id">The ID of the BaseCamera.</param>
        /// <param name="position">The position of the BaseCamera.</param>
        public BaseCamera2D(string id, Vector2 position)
            : base(id)
        {
            zoom = 1;

            origin = Common.TextureQuality / 2;
            this.position = position;

            view = new Rectangle((int)position.X, (int)position.Y,
                (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y);

            CameraManager2D.AddCamera(this);
        }

        /// <summary>
        /// Creates a new BaseCamera set at a default position.
        /// </summary>
        /// <param name="id">The ID of the BaseCamera.</param>
        public BaseCamera2D(string id) : this(id, Vector2.Zero) { }

        /// <summary>
        /// Sets the constraining area of the BaseCamera2D.
        /// </summary>
        /// <param name="rect">The are in witch the BaseCamera2D can move inside of.</param>
        public void SetConstraint(Rectangle rect)
        {
            constrain = rect;

            LockCamera();
        }

        /// <summary>
        /// A check to see if an object 
        /// is in view of the BaseCamera.
        /// </summary>
        /// <param name="rect">The bounds of the object to check.</param>
        /// <returns>The result of the check.</returns>
        public bool IsInView(Rectangle rect)
        {
            Vector2[] points = new Vector2[]
            { 
                new Vector2(rect.X, rect.Y),
                new Vector2(rect.X, rect.Bottom),
                new Vector2(rect.Right, rect.Y),
                new Vector2(rect.Right, rect.Bottom)
            };

            bool isInside = false;
            int i = 0;

            //Rectangle bounds = view;
            //bounds.Width = (int)(bounds.Width * zoom);
            //bounds.Height = (int)(bounds.Height * zoom);

            while (i < points.Length && !isInside)
            {
                if (EonMathsHelper.IsInsideOf(view, points[i]))
                    isInside = true;

                i++;
            }

            return isInside;
        }

        /// <summary>
        /// Used to move the BaseCamera.
        /// </summary>
        /// <param name="movement">The amount to move the BaseCamera by.</param>
        public void Move(Vector2 movement)
        {
            position += movement;

            LockCamera();
        }

        /// <summary>
        /// Zooms the BaseCamera in.
        /// </summary>
        public void ZoomIn()
        {
            zoom += zoomRate;

            if (zoom > maxZoom)
                zoom = maxZoom;

            LockCamera();
        }

        /// <summary>
        /// Zooms the BaseCamera out.
        /// </summary>
        public void ZoomOut()
        {
            zoom -= zoomRate;

            if (zoom < minZoom)
                zoom = minZoom;

            LockCamera();
        }

        /// <summary>
        /// Resets the zoom of the BaseCamera2D to
        /// it's minimal value.
        /// </summary>
        public void ResetZoom()
        {
            zoom = 1;
        }

        /// <summary>
        /// Shakes the BaseCamera.
        /// </summary>
        /// <param name="time">The amount of time to shake the BaseCamera for.</param>
        /// <param name="magnitude">The amount of shake to apply to the BaseCamera.</param>
        /// <param name="shakeController">Wheather or not to shake the controller as well.</param>
        public void Shake(float time, float magnitude, bool shakeController)
        {
            currentTime = TimeSpan.Zero;

            shakeTime = TimeSpan.FromMilliseconds(time);
            this.magnitude = magnitude;
        }

        protected void LockCamera()
        {
            if (constrain != Rectangle.Empty)
            {
                position.X = MathHelper.Clamp(position.X, constrain.X, constrain.Right - view.Width);
                position.Y = MathHelper.Clamp(position.Y, constrain.Y, constrain.Bottom - view.Height);
            }

            view.X = (int)position.X;
            view.Y = (int)position.Y;
        }

        /// <summary>
        /// Centres the BaseCamera2D on to a position.
        /// </summary>
        /// <param name="position">The position to centre the BaseCamera2D.</param>
        /// <param name="size">The size of the object to be centred on.</param>
        public void LockOnTo(Vector2 position, Vector2 size)
        {
            this.position.X = (int)((position.X + size.X / 2) - (view.Width / 2));
            this.position.Y = (int)((position.Y + size.Y / 2) - (view.Height / 2));

            LockCamera();
        }

        /// <summary>
        /// Centres the BaseCamera2D on to a position.
        /// </summary>
        /// <param name="position">The position to centre the BaseCamera2D.</param>
        public void LockOnTo(Vector2 position)
        {
            this.position.X = EonMathsHelper.Round(position.X - (view.Width / 2));
            this.position.Y = EonMathsHelper.Round(position.Y - (view.Height / 2));

            LockCamera();
        }

        /// <summary>
        /// Updates the BaseCamera.
        /// </summary>
        protected override void Update()
        {
            if (currentTime <= shakeTime)
            {
                currentTime += Common.ElapsedTimeDelta;

                position += new Vector2(RandomHelper.GetRandom(-magnitude, magnitude),
                    RandomHelper.GetRandom(-magnitude, magnitude));

                LockCamera();
            }
        }
    }
}
