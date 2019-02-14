/* Created: 26/11/2014
 * Last Updated: 31/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Cameras
{
    /// <summary>
    /// Defines a CameraComponent that is mostly stationary.
    /// </summary>
    public sealed class TargetCamera : CameraComponent
    {
        bool locked = false;

        /// <summary>
        /// Wheather the TargetCamera is free to have it's target changed. 
        /// </summary>
        public bool Locked
        {
            get { return locked; }
        }

        /// <summary>
        /// Creates a new TargetCamera.
        /// </summary>
        /// <param name="id">The unique ID name to give to the TargetCamera.</param>
        /// <param name="nearPlane">The closest distance that 
        /// can be seen by the TargetCamera.</param>
        /// <param name="farPlane">The farthest distance that
        /// can be seen by the CameraComponent.</param>
        /// <param name="position">The position of the TargetCamera.</param>
        public TargetCamera(string id, float nearPlane,
            float farPlane, Vector3 position)
            : base(id, nearPlane, farPlane, position) { }

        /// <summary>
        /// Sets the yaw, pitch, roll of the TargetCamera.
        /// </summary>
        /// <param name="yaw">Rotation along the Y-axis.</param>
        /// <param name="pitch">Rotation along the X-axis.</param>
        /// <param name="roll">Rotation along the Z-axis.</param>
        public void SetYawPitchRoll(float yaw, float pitch, float roll)
        {
            rotation.Y = yaw;
            rotation.X = pitch;
            rotation.Z = roll;
        }

        protected override void UpdateView()
        {
            Matrix rot = Matrix.CreateFromYawPitchRoll(rotation.Y,
                rotation.X, rotation.Z);

            Vector3 trans = Vector3.Zero;
            Matrix parentRot = Matrix.Identity;

            if (Owner != null)
            {
                trans = Owner.World.Position;
                target = trans;

                parentRot = Owner.World.Rotational;
            }

            movement = Vector3.Transform(movement, rot + parentRot);
            pos += movement;
            pos = Shake(pos);
            movement = Vector3.Zero;

            Vector3 up = Vector3.Transform(Vector3.Up, rot);

            view = Matrix.CreateLookAt(trans + pos, target, up);
        }

        /// <summary>
        /// Changes the CameraComponent's target.
        /// </summary>
        /// <param name="target">The new CameraComponent target.</param>
        public void ChangeTarget(Vector3 target)
        {
            if (!locked)
                this.target = target;
        }

        /// <summary>
        /// Locks the TargetCamera so that the target can't be changed.
        /// </summary>
        public void LockCamera()
        {
            locked = true;
        }

        /// <summary>
        /// Un-Locks the TargetCamera so that the target can be changed.
        /// </summary>
        public void UnLockCamera()
        {
            locked = false;
        }
    }
}
