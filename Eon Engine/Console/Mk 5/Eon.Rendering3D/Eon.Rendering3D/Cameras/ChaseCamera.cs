/* Created: 15/05/2014
 * Last Updated: 26/11/2014
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
        Vector3 followingPos;
        Vector3 followingRot;

        Vector3 posOffset;
        Vector3 targetOffset;

        Vector3 relativeCameraRot;

        float springiness = 0.15f;

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
        /// <param name="posOffset">Camera position baseTransform.</param>
        /// <param name="targetOffset">Camera target baseTransform.</param>
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
            movement += newFollowPos * springiness;
        }

        public void Rotate(Vector3 rotChange)
        {
            RelativeCameraRot += rotChange;
        }

        protected override void UpdateView()
        {
            Vector3 totalRot = FollowingRot + RelativeCameraRot;

            Matrix rot = Matrix.CreateFromYawPitchRoll(totalRot.Y, totalRot.X, totalRot.Z);

            movement = Vector3.Transform(movement, rot);
            followingPos += movement;
            followingPos = Shake(followingPos);

            movement = Vector3.Zero;

            Vector3 desiredPos = FollowingPos + Vector3.Transform(PosOffset, rot);

            pos = Vector3.Lerp(pos, desiredPos, springiness);

            target = FollowingPos + Vector3.Transform(TargetOffSet, rot);

            Vector3 up = Vector3.Transform(Vector3.Up, rot);

            Vector3 trans = Vector3.Zero;

            if (Owner != null)
                trans = Owner.World.Position;

            view = Matrix.CreateLookAt(trans + pos, target, up);
        }
    }
}
