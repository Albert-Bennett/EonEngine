/* Created: 15/12/2013
 * Last Updated: 06/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering3D.Culling;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Cameras
{
    /// <summary>
    /// Used to define a camera.
    /// </summary>
    public abstract class CameraComponent : ObjectComponent, IUpdate
    {
        #region Fields
        #region Positional

        internal OnCameraActiveChangedEvent onCameraActiveChanged;

        protected Vector3 pos;
        protected Vector3 prevPos;

        protected Vector3 rotation;
        protected Vector3 originalPos;
        protected Vector3 target;

        protected Vector3 movement;

        protected Matrix world;

        #region Shake

        bool hasShuck = false;

        Vector3 prevShake;
        Vector3 shakeMagnitude;

        TimeSpan currentShakeTime = TimeSpan.Zero;
        TimeSpan shakeTime;

        #endregion
        #endregion
        #region Culling

        protected Vector3[] frustumCorners = new Vector3[8];
        protected BoundingFrustum viewFrustum;
        protected ViewClippingFrustum clippingFrustum;
        protected Vector2 viewPoint;

        float[] lodDistances = new float[LODManager.LODFragments.Length];

        #endregion
        #region Rendering

        float farPlane;
        float nearPlane;

        protected Matrix view;
        protected Matrix proj;

        #endregion
        #endregion
        #region Properties

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The movement delta of the CameraComponent.
        /// </summary>
        public Vector3 Delta
        {
            get { return prevPos - pos; }
        }

        /// <summary>
        /// The position of the CameraComponent.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (Owner != null)
                    return Owner.World.Position + pos;

                return pos;
            }
            set
            {
                pos = value;
            }
        }

        /// <summary>
        /// The direction that the camera is facing.
        /// </summary>
        public Vector3 Direction
        {
            get { return Vector3.Normalize(target - pos); }
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
            get { return viewPoint; }
        }

        /// <summary>
        /// The projection matrix of the CameraComponent.
        /// </summary>
        public Matrix Projection
        {
            get { return proj; }
        }

        public Matrix World
        {
            get
            {
                return Matrix.CreateFromYawPitchRoll(rotation.Y,
                    rotation.X, rotation.Z) * Matrix.CreateTranslation(pos);
            }
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
        public ViewClippingFrustum ClippingFrustum
        {
            get { return clippingFrustum; }
        }

        /// <summary>
        /// The view frustum of the CameraComponent.
        /// </summary>
        public BoundingFrustum Frustum
        {
            get { return viewFrustum; }
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
        }

        protected override void Initialize()
        {
            CreateProjection();
            UpdateView();
            FinializeUpdate();

            GetLodDistances();

            CameraManager.AddCamera(this);

            base.Initialize();
        }

        void GetLodDistances()
        {
            for (int i = 0; i < LODManager.LODFragments.Length; i++)
                lodDistances[i] = (farPlane - nearPlane) * LODManager.LODFragments[i];
        }

        void CreateProjection()
        {
            float aspectRatio = Common.AspectRatio;

            proj = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, aspectRatio, nearPlane, farPlane);
        }

        #endregion
        #region Updating

        public void _Update()
        {
            prevPos = pos;

            UpdateView();
            FinializeUpdate();
        }

        protected virtual void UpdateView()
        {
            Matrix rot = Matrix.CreateFromYawPitchRoll(rotation.Y,
                rotation.X, rotation.Z);

            movement = Vector3.Transform(movement, rot);

            pos += movement;
            pos = Shake(pos);
            movement = Vector3.Zero;

            Vector3 forward = Vector3.Transform(Vector3.Forward, rot) * (nearPlane - farPlane);
            target = pos + forward;

            Vector3 up = Vector3.Transform(Vector3.Up, rot);

            Vector3 trans = Vector3.Zero;

            if (Owner != null)
                trans = Owner.World.Position;

            view = Matrix.CreateLookAt(trans + pos, target, up);
        }

        protected Vector3 Shake(Vector3 pos)
        {
            if (currentShakeTime < shakeTime)
            {
                currentShakeTime += Common.ElapsedTimeDelta;

                if (hasShuck)
                {
                    hasShuck = false;
                    pos -= prevShake;
                }
                else
                {
                    hasShuck = true;

                    prevShake = RandomHelper.GetRandom(-shakeMagnitude, shakeMagnitude);
                    pos += prevShake;
                }
            }

            return pos;
        }

        void FinializeUpdate()
        {
            viewFrustum = new BoundingFrustum(view * proj);

            viewFrustum.GetCorners(frustumCorners);
            clippingFrustum = ViewClippingFrustum.FromPoints(frustumCorners);

            viewPoint = new Vector2()
            {
                X = pos.X,
                Y = pos.Z
            };

            world = Matrix.CreateTranslation(pos) *
                    Matrix.CreateFromYawPitchRoll(rotation.Y,
                    rotation.X, rotation.Z);
        }

        #endregion
        #region Helpers

        public override void Enable()
        {
            if (onCameraActiveChanged != null)
                onCameraActiveChanged(ID);

            base.Enable();
        }

        public override void Disable()
        {
            if (onCameraActiveChanged != null)
                onCameraActiveChanged(ID);

            base.Disable();
        }

        internal void _Disable()
        {
            base.Disable();
        }

        internal void _Enable()
        {
            base.Enable();
        }

        /// <summary>
        /// Gets the lod level of an object.
        /// </summary>
        /// <param name="objectPosition">The position of the object.</param>
        /// <returns>The result of the calculation.</returns>
        public LODLevels GetLODLevel(Vector3 objectPosition)
        {
            float dist = Vector3.Distance(pos, objectPosition);

            bool found = false;
            int i = 0;

            while (!found && i < lodDistances.Length - 1)
            {
                if (dist >= lodDistances[i] && dist <= lodDistances[i + 1])
                    found = true;
                else
                    i++;
            }

            return (LODLevels)i;
        }

        /// <summary>
        /// Moves the CameraComponent.
        /// </summary>
        /// <param name="movement">The amount to move 
        /// the CameraComponent by.</param>
        public virtual void Move(Vector3 movement)
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

        /// <summary>
        /// A check to see if a position is inview of the CameraComponent.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>The result of the check.</returns>
        public bool IsInView(Vector3 position)
        {
            return viewFrustum.Contains(position) != ContainmentType.Disjoint;
        }

        /// <summary>
        /// Sets the magnituide of the CameraComponent's shake.
        /// </summary>
        /// <param name="magnitude">The magnitude of the shake.</param>
        public void SetShakeMagnitude(float magnitude)
        {
            shakeMagnitude = new Vector3(magnitude, magnitude, 0);
        }

        /// <summary>
        /// Shakes the CameraComponent.
        /// </summary>
        /// <param name="shakeTime">The amount of time that the shake will last for.</param>
        /// <param name="magnitude">The magnitude of the shake.</param>
        public void Shake(TimeSpan shakeTime, float magnitude)
        {
            currentShakeTime = TimeSpan.Zero;

            this.shakeTime = shakeTime;
            shakeMagnitude = new Vector3(magnitude, magnitude, 0);
        }

        public override void Destroy(bool remove)
        {
            CameraManager.Remove(this);

            base.Destroy(remove);
        }

        public void ScreenResolutionChanged()
        {
            CreateProjection();
        }

        #endregion
    }
}
