/* Created 15/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D.Cameras
{
    /// <summary>
    /// Defines a manager for CameraComponents.
    /// </summary>
    public sealed class CameraManager : EngineComponent
    {
        static CameraComponent currentCamera = null;

        static List<CameraComponent> cameras =
            new List<CameraComponent>();

        /// <summary>
        /// The CameraComponent currently in use.
        /// </summary>
        public static CameraComponent CurrentCamera
        {
            get { return currentCamera; }
        }

        public CameraManager() : base("Camera3DManager") { }

        internal static void AddCamera(CameraComponent camera)
        {
            if (camera != null)
                if (!cameras.Contains(camera))
                {
                    if (currentCamera == null)
                        currentCamera = camera;

                    cameras.Add(camera);
                }
        }

        /// <summary>
        /// Sets the current camera to one with the given ID.
        /// </summary>
        /// <param name="id">The ID of the camera to switch to.</param>
        public static void ChangeCurrentCamera(string id)
        {
            CameraComponent cam = null;

            cam = (from c in cameras
                   where c.ID == id
                   select c).FirstOrDefault();

            if (cam != null)
                currentCamera = cam;
        }

        /// <summary>
        /// Locks the view matrix of the current Camera to a GameObject.
        /// </summary>
        /// <param name="position">The location of the GameObject.</param>
        public static void LockToGameObject(Vector3 position)
        {
            if (currentCamera != null)
                currentCamera.LockToGameObject(position);
        }

        internal static void Remove(CameraComponent camera)
        {
            if (cameras.Contains(camera))
                cameras.Remove(camera);
        }
    }
}
