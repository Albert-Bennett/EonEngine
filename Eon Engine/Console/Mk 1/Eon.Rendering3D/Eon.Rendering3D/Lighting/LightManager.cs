/* Created 15/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D.Lighting
{
    /// <summary>
    /// Defines a manager class for LightComponents.
    /// </summary>
    public sealed class LightManager:EngineComponent
    {
        static List<LightComponent3D> lights =
             new List<LightComponent3D>();

        /// <summary>
        /// The amount of lights in the current level.
        /// </summary>
        public static int Count
        {
            get { return lights.Count; }
        }

        public LightManager() : base("LightManager3D") { }

        internal static void Add(LightComponent3D light)
        {
            if (!lights.Contains(light))
                lights.Add(light);
        }

        internal static void Remove(LightComponent3D light)
        {
            if (lights.Contains(light))
                lights.Remove(light);
        }

        /// <summary>
        /// Finds a LightComponent.
        /// </summary>
        /// <param name="id">The LightComponent to be found.</param>
        /// <returns>A found LightComponent or null.</returns>
        public static LightComponent3D GetLight(string id)
        {
            LightComponent3D l = null;

            l = (from i in lights
                 where i.ID == id
                 select i).FirstOrDefault();

            return l;
        }

        /// <summary>
        /// Gets all of the LightComponents that are of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of lights to be found.</typeparam>
        /// <returns>The found lights or null.</returns>
        public static List<T> GetLights<T>() where T : LightComponent3D
        {
            List<T> l = null;

            l = (from i in lights
                 where i.GetType() == typeof(T)
                 select i) as List<T>;

            return l;
        }
    }
}
