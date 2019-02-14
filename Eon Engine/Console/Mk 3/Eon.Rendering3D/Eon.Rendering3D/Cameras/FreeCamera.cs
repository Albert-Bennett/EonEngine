/* Created: 15/12/2013
 * Last Updated: 26/11/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Cameras
{
    /// <summary>
    /// Defines a camera that can be moved freely.
    /// </summary>
    public sealed class FreeCamera : CameraComponent
    {
        /// <summary>
        /// Creates a new FreeCamera.
        /// </summary>
        /// <param name="id">The unique ID name to give to the FreeCamera.</param>
        /// <param name="nearPlane">The closest distance that 
        /// can be seen by the FreeCamera.</param>
        /// <param name="farPlane">The farthest distance that
        /// can be seen by the CameraComponent.</param>
        /// <param name="position">The position of the FreeCamera.</param>
        public FreeCamera(string id, float nearPlane,
            float farPlane, Vector3 position)
            : base(id, nearPlane, farPlane, position) { }

        /// <summary>
        /// Sets the yaw, pitch, roll of the FreeCamera.
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
    }
}
