/* Created: 15/12/2013
 * Last Updated: 06/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.Testing;
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
                    camera.onCameraActiveChanged += new OnCameraActiveChangedEvent(CameraActiveChanged);

                    cameras.Add(camera);

                    if (currentCamera == null)
                        currentCamera = camera;
                    else
                        camera._Disable();
                }
        }

        static void CameraActiveChanged(string id)
        {
            CameraComponent cam = null;

            cam = (from c in cameras
                   where c.ID == id
                   select c).FirstOrDefault();

            if (cam.ID != currentCamera.ID)
                switch (cam.Enabled)
                {
                    case true:
                        {
                            bool found = false;
                            int idx = 0;

                            while (!found && idx < cameras.Count)
                            {
                                if (cameras[idx].ID == currentCamera.ID)
                                {
                                    cameras[idx]._Disable();
                                    found = true;
                                }

                                idx++;
                            }

                            currentCamera = cam;
                        }
                        break;
                }
        }

        /// <summary>
        /// Used to set the current camera that will be used.
        /// </summary>
        /// <param name="id">The ID of the CameraCompent to be made active.</param>
        public static void SetCurrentCamera(string id)
        {
            CameraComponent cam = null;

            cam = (from c in cameras
                   where c.ID == id
                   select c).FirstOrDefault();

            if (cam != null)
                if (cam.ID != currentCamera.ID)
                    cam.Enable();
                else
                    new Error("A CameraComponent with the id of: " + id + " dosn't exist", Seriousness.Warning);
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
