/* Created: 10/06/2013
 * Last Updated: 06/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering2D.Cameras
{
    /// <summary>
    /// Defines a class that is used to manage 2D cameras.
    /// </summary>
    public sealed class CameraManager2D : EngineModule, IHoldReferences
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
            EngineModule module = EngineModuleManager.Find("Camera2DManager");

            camera.AddReference(module as IHoldReferences);
            cameras.Add(camera);
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

        /// <summary>
        /// Finds a BaseCamera2D in the CameraManager2D.
        /// </summary>
        /// <param name="id">The ID of the BaseCamera2D to find.</param>
        /// <returns>The result of the search.</returns>
        public static BaseCamera2D FindCamera(string id)
        {
            BaseCamera2D cam = (from c in cameras
                                where c.ID.Equals(id)
                                select c).FirstOrDefault();

            return cam;
        }

        /// <summary>
        /// Used to remove a BaseCamera2D.
        /// </summary>
        /// <param name="obj">The BaseCamera2D to be removed.</param>
        public void Remove(object obj)
        {
            if (obj is BaseCamera2D)
                cameras.Remove(obj as BaseCamera2D);
        }
    }
}
