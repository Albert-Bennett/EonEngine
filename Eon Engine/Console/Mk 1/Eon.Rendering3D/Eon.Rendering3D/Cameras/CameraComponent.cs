/* Created 15/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;
using Eon.Rendering3D.Culling;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Cameras
{
    /// <summary>
    /// Used to define a camera.
    /// </summary>
    public abstract class CameraComponent : ObjectComponent, IUpdate
    {
        #region Fields
        #region Positional

        protected Vector3 pos;
        protected Vector3 pitchYawRoll;
        protected Vector3 originalPos;
        protected Vector3 target;

        Vector3 movement;

        #endregion
        #region Culling

        Vector3[] frustumCorners = new Vector3[8];
        BoundingFrustum viewFrustum;
        ClippingFrustum clippingFrustum;
        Vector2 viewPoint;

        #endregion
        #region Rendering

        float farPlane;
        float nearPlane;

        Matrix view;
        Matrix proj;

        #endregion
        #endregion
        #region Properties

        /// <summary>
        /// The position of the CameraComponent.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (Owner != null)
                    return Owner.World.Translation + pos;

                return pos;
            }
        }

        /// <summary>
        /// The view matrix of the CameraComponent.
        /// </summary>
        public Matrix View
        {
            get { return view; }
        }

        /// <summary>
        /// The view point of the CameraComponent.
        /// </summary>
        public Vector2 ViewPoint
        {
            get{return viewPoint;}
        }

        /// <summary>
        /// The projection matrix of the CameraComponent.
        /// </summary>
        public Matrix Projection
        {
            get { return proj; }
        }

        /// <summary>
        /// The closest distance that 
        /// can be seen by the CameraComponent.
        /// </summary>
        public float NearPlane
        {
            get { return nearPlane; }
        }

        /// <summary>
        /// The furthest distance that can be seen by the CameraComponent.
        /// </summary>
        public float FarPlane
        {
            get { return farPlane; }
        }

        /// <summary>
        /// The ClippingFrustum for the CameraComponent.
        /// </summary>
        public ClippingFrustum ClippingFrustum
        {
            get { return clippingFrustum; }
        }

        #endregion
        #region Ctor

        /// <summary>
        /// Creates a new CameraComponent.
        /// </summary>
        /// <param name="id">The unique ID name to give to the CameraComponent.</param>
        /// <param name="nearPlane">The closest distance that 
        /// can be seen by the CameraComponent.</param>
        /// <param name="farPlane">The farthest distamnce that
        /// can be seen by the CameraComponent.</param>
        public CameraComponent(string id, float nearPlane, float farPlane) : this(id, nearPlane, farPlane, Vector3.Zero) { }

        /// <summary>
        /// Creates a new CameraComponent.
        /// </summary>
        /// <param name="id">The unique ID name to give to the CameraComponent.</param>
        /// <param name="nearPlane">The closest distance that 
        /// can be seen by the CameraComponent.</param>
        /// <param name="farPlane">The farthest distance that
        /// can be seen by the CameraComponent.</param>
        /// <param name="position">The position of the CameraComponent.</param>
        public CameraComponent(string id, float nearPlane,
            float farPlane, Vector3 position)
            : base(id)
        {
            this.farPlane = farPlane;
            this.nearPlane = nearPlane;
            this.pos = position;

            UpdateView();
            CreateProjection();

            CameraManager.AddCamera(this);
        }

        void CreateProjection()
        {
            float ratio = Common.Device.Viewport.AspectRatio;

            proj = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, ratio, nearPlane, farPlane);
        }

        #endregion
        #region Updating

        public void _Update()
        {
            if (movement != Vector3.Zero)
                UpdateView();

            Update();
        }

        protected virtual void UpdateView()
        {
            Matrix rot = Matrix.CreateFromYawPitchRoll(pitchYawRoll.Y,
                pitchYawRoll.X, pitchYawRoll.Z);

            movement = Vector3.Transform(movement, rot);
            pos += movement;
            movement = Vector3.Zero;

            Vector3 forward = Vector3.Transform(Vector3.Forward, rot);
            target = pos + forward;

            Vector3 up = Vector3.Transform(Vector3.Up, rot);

            view = Matrix.CreateLookAt(pos, target, up);
            viewFrustum = new BoundingFrustum(view * proj);

            viewFrustum.GetCorners(frustumCorners);
            clippingFrustum = ClippingFrustum.FromPoints(frustumCorners);

            viewPoint = new Vector2()
            {
                X = pos.X,
                Y = pos.Z
            };
        }

        protected virtual void Update() { }

        #endregion
        #region Helpers

        /// <summary>
        /// Moves the CameraComponent.
        /// </summary>
        /// <param name="movement">The amount to move 
        /// the CameraComponent by.</param>
        public void Move(Vector3 movement)
        {
            this.movement += movement;
        }

        /// <summary>
        /// Locks the view matrix of the Camera to a GameObject.
        /// </summary>
        /// <param name="position">The location of the GameObject.</param>
        public void LockToGameObject(Vector3 position)
        {
            target = pos;
            view = Matrix.CreateLookAt(Position, target, Vector3.Up);
        }

        /// <summary>
        /// Check to see if a bounding volume is within the bounds of the view frustum
        /// </summary>
        /// <param name="box">BoundingBox to be checked</param>
        /// <returns>Result</returns>
        public bool IsInView(BoundingBox box)
        {
            return viewFrustum.Contains(box) != ContainmentType.Disjoint;
        }

        /// <summary>
        /// Check to see if a bounding volume is within the bounds of the view frustum
        /// </summary>
        /// <param name="sphere">BoundingSphere to be checked</param>
        /// <returns>Result</returns>
        public bool IsInView(BoundingSphere sphere)
        {
            return viewFrustum.Contains(sphere) != ContainmentType.Disjoint;
        }

        public override void Destroy()
        {
            CameraManager.Remove(this);

            base.Destroy();
        }

        #endregion
    }
}
