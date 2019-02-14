/* Created 15/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Culling;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Cameras
{
    /// <summary>
    /// Defines a camera that chases a target.
    /// </summary>
    public class ChaseCamera : CameraComponent
    {
        Vector3 viewVec;

        Vector3 followingPos;
        Vector3 followingRot;

        Vector3 posOffset;
        Vector3 targetOffset;

        Vector3 movement;

        Vector3 relativeCameraRot;

        float springiness = 0.15f;

        public Vector3 ViewVec
        {
            get { return viewVec; }
        }

        public Vector3 FollowingPos
        {
            get { return followingPos; }
            set { followingPos = value; }
        }

        public Vector3 FollowingRot
        {
            get { return followingRot; }
            set { followingRot = value; }
        }

        public Vector3 PosOffset
        {
            get { return posOffset; }
            set { posOffset = value; }
        }

        public Vector3 TargetOffSet
        {
            get { return targetOffset; }
            set { targetOffset = value; }
        }

        public Vector3 RelativeCameraRot
        {
            get { return relativeCameraRot; }
            set { relativeCameraRot = value; }
        }

        public float Springiness
        {
            get { return springiness; }
            set { springiness = MathHelper.Clamp(value, 0, 1); }
        }

        /// <summary>
        /// Craetes a new ChaseCamera.
        /// </summary>
        /// <param name="id">The id of the ChaseCamera.</param>
        /// <param name="nearPlane">Near plane value.</param>
        /// <param name="farPlane">Far plane value.</param>
        /// <param name="posOffset">Camera position offset.</param>
        /// <param name="targetOffset">Camera target offset.</param>
        /// <param name="relativeCameraRot">Relative camera rotation.</param>
        public ChaseCamera(string id, float nearPlane, float farPlane,
            Vector3 posOffset, Vector3 targetOffset, Vector3 relativeCameraRot) :
            base(id, nearPlane, farPlane)
        {
            PosOffset = posOffset;
            TargetOffSet = targetOffset;
            RelativeCameraRot = relativeCameraRot;
        }

        public void Move(Vector3 newFollowPos, Vector3 followTargetRot)
        {
            FollowingRot += followTargetRot;
            movement += newFollowPos * speed;
        }

        public void Rotate(Vector3 rotChange)
        {
            RelativeCameraRot += rotChange;
        }

        protected override void Update()
        {
            Vector3 totalRot = FollowingRot + RelativeCameraRot;

            Matrix rot = Matrix.CreateFromYawPitchRoll(totalRot.Y, totalRot.X, totalRot.Z);

            movement = Vector3.Transform(movement, rot);
            followingPos += movement;

            movement = Vector3.Zero;

            Vector3 desiredPos = FollowingPos + Vector3.Transform(PosOffset, rot);

            pos = Vector3.Lerp(pos, desiredPos, springiness);

            target = FollowingPos + Vector3.Transform(TargetOffSet, rot);

            Vector3 up = Vector3.Transform(Vector3.Up, rot);

            view = Matrix.CreateLookAt(pos, target, up);

            viewVec = Vector3.Transform(target - pos, Matrix.CreateRotationY(0));
            viewVec.Normalize();

            viewFrustum = new BoundingFrustum(view * proj);

            viewFrustum.GetCorners(frustumCorners);
            clippingFrustum = ClippingFrustum.FromPoints(frustumCorners);

            viewPoint = new Vector2()
            {
                X = pos.X,
                Y = pos.Z
            };
        }
    }
}
