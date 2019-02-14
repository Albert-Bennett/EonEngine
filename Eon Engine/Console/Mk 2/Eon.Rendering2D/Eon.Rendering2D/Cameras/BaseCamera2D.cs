﻿/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering2D.Cameras
{
    /// <summary>
    /// Defines a 2D camera.
    /// </summary>
    public class BaseCamera2D : ObjectComponent, IUpdate
    {
        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan shakeTime = TimeSpan.Zero;

        float magnitude;

        protected Rectangle view;

        protected float zoom;
        protected float speed;

        protected float maxSpeed = 6;
        protected float minSpeed = 4;

        protected float maxZoom = 1.25f;
        protected float minZoom = 0.5f;
        protected float zoomRate = 0.01f;
        protected Rectangle constrain;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The position of the camera (top corner).
        /// </summary>
        public Vector2 Pos
        {
            get { return new Vector2(view.X, view.Y); }
            set
            {
                view.X = EonMathsHelper.Round(value.X);
                view.Y = EonMathsHelper.Round(value.Y);

                LockCamera();
            }
        }

        /// <summary>
        /// The direction that the BaseCamera is moving in.
        /// </summary>
        public Vector2 Direction
        {
            get { return Vector2.Normalize(Pos); }
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
        /// How far the BaseCamera is zoomed in.
        /// </summary>
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = (float)MathHelper.Clamp(value, minZoom, maxZoom);
            }
        }

        /// <summary>
        /// The inverse zoom of the BaseCamera.
        /// </summary>
        public float InverseZoom
        {
            get { return maxZoom / zoom; }
        }

        /// <summary>
        /// The right co-ordinate of the BaseCamera.
        /// </summary>
        public float Right
        {
            get { return (float)view.Right * zoom; }
        }

        /// <summary>
        /// The left co-ordinate of the BaseCamera.
        /// </summary>
        public float Left
        {
            get { return (float)view.Left * zoom; }
        }

        /// <summary>
        /// The BaseCamera's view matrix. 
        /// </summary>
        public Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateScale(zoom) *
                     //Matrix.CreateTranslation(-new Vector3(view.Center.X, view.Center.Y, 0) * zoom);
                    Matrix.CreateTranslation(new Vector3(-Pos * zoom, 0));
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
        /// <param name="pos">The position of the BaseCamera.</param>
        public BaseCamera2D(string id, Vector2 pos)
            : base(id)
        {
            zoom = 1;
            this.Pos = pos;

            view = new Rectangle((int)pos.X, (int)pos.Y,
                (int)Common.DefaultScreenResolution.X, (int)Common.DefaultScreenResolution.Y);

            CameraManager2D.AddCamera(this);
        }

        /// <summary>
        /// Creates a new BaseCamera set at a default position.
        /// </summary>
        /// <param name="id">The ID of the BaseCamera.</param>
        public BaseCamera2D(string id) : this(id, new Vector2(0, -Common.DefaultScreenResolution.Y)) { }

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
            if ((float)view.Right * zoom > (float)rect.X || (float)view.X * zoom < (float)rect.Right &&
                (float)view.Bottom * zoom > (float)rect.Y || (float)view.Top * zoom < (float)rect.Bottom)
                return true;

            return false;
        }

        /// <summary>
        /// Used to move the BaseCamera.
        /// </summary>
        /// <param name="movement">The amount to move the BaseCamera by.</param>
        public void Move(Vector2 movement)
        {
            Pos = Pos + movement;

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

            //Vector2 newPos = Pos  / zoom;
            LockCamera();
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
                int minX = constrain.X - view.Width / 2;
                int maxX = minX - (constrain.Width - view.Width);

                view.X = (int)MathHelper.Clamp(View.X, maxX * zoom, minX * zoom);

                int minY = -constrain.Y + view.Height / 2;//Correct
                int maxY = minY + (constrain.Y - view.Height);//Correct :)
                minY = maxY + (constrain.Height - view.Height);

                view.Y = (int)MathHelper.Clamp(View.Y, maxY * zoom, minY * zoom);
            }
        }

        /// <summary>
        /// Locks the BaseCamera to a position.
        /// </summary>
        /// <param name="position">The position to lock on to.</param>
        /// <param name="size">The size of the object to be locked on to.</param>
        public void LockToGameObject(Vector2 position, Vector2 size)
        {
            view.X = (int)((position.X + size.X / 2) * zoom - (view.Width / 2));
            view.Y = (int)((position.Y + size.Y / 2) * zoom - (view.Height / 2));

            LockCamera();
        }

        /// <summary>
        /// Updates the BaseCamera.
        /// </summary>
        public void _Update()
        {
            if (currentTime <= shakeTime)
            {
                currentTime += Common.ElapsedTimeDelta;

                Pos += new Vector2(RandomHelper.GetRandom(-magnitude, magnitude),
                    RandomHelper.GetRandom(-magnitude, magnitude));

                LockCamera();
            }

            Update();
        }

        protected virtual void Update() { }
    }
}
