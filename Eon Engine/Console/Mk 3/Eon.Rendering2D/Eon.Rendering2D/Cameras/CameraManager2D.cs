/* Created: 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Rendering2D.Cameras
{
    /// <summary>
    /// Defines a class that is used to manage cameras.
    /// </summary>
    public sealed class CameraManager2D : EngineComponent
    {
        static List<BaseCamera2D> cameras =
            new List<BaseCamera2D>();

        static int idx = 0;

        /// <summary>
        /// Gets the curent camera in use.
        /// </summary>
        public static BaseCamera2D CurrentCamera
        {
            get
            {
                if (cameras.Count > 0)
                    return cameras[idx];
                else
                    return null;
            }
        }

        /// <summary>
        /// Creates a new CamerManager.
        /// </summary>
        public CameraManager2D() : base("Camera2DManager") { }

        internal static void AddCamera(BaseCamera2D camera)
        {
            cameras.Add(camera);
        }

        /// <summary>
        /// Locks the curent Camera to the GameObject at the location.
        /// </summary>
        /// <param name="position">The location of the GameObject.</param>
        /// <param name="size">The size of the GameObject.</param>
        public static void LockToGameObject(Vector3 position, Vector3 size)
        {
            if (CurrentCamera != null)
            {
                Vector2 pos = new Vector2()
                {
                    X = position.X,
                    Y = position.Y
                };

                Vector2 s = new Vector2()
                {
                    X = size.X,
                    Y = size.Y
                };

                CurrentCamera.LockToGameObject(pos, s);
            }
        }

        /// <summary>
        /// Changes the current camera to a diferent one. 
        /// </summary>
        /// <param name="id">The ID of the camera to be changed to.</param>
        /// <returns>Wheather or not the camera has changed.</returns>
        public static bool ChangeCamera(string id)
        {
            for (int i = 0; i < cameras.Count; i++)
                if (cameras[i].ID == id)
                {
                    idx = i;
                    return true;
                }

            return false;
        }
    }
}
