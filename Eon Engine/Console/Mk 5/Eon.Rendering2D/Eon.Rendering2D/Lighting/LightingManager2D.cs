/* Created: 01/06/2013
 * Last Updated: 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using System.Collections.Generic;

namespace Eon.Rendering2D.Lighting
{
    /// <summary>
    /// Derfines an engineComponent that is used to manage lights. 
    /// </summary>
    public sealed class LightingManager2D : EngineModule
    {
        static List<Light2D> lights = new List<Light2D>();

        /// <summary>
        /// The number of lights in the current scene.
        /// </summary>
        public static int LightCount
        {
            get { return lights.Count; }
        }

        /// <summary>
        /// Creates a new LightingManager.
        /// </summary>
        public LightingManager2D() : base("LightingManager") { }

        /// <summary>
        /// Adds a light to this.
        /// </summary>
        /// <param name="light">The light to be added.</param>
        public static void Add(Light2D light)
        {
            lights.Add(light);
        }

        /// <summary>
        /// Finds all of the linght that are of the given type.
        /// </summary>
        /// <typeparam name="T">The type of lights to be found.</typeparam>
        /// <param name="lightsFound">The lights found.</param>
        public static void GetLights<T>(out List<T> lightsFound) where T : Light2D
        {
            lightsFound = new List<T>();

            for (int i = 0; i < lights.Count; i++)
                if (lights[i].GetType() == typeof(T) && lights[i].Enabled)
                    lightsFound.Add(lights[i] as T);
        }

        /// <summary>
        /// Removes a light from this.
        /// </summary>
        /// <param name="id">The ID of the light to be removed.</param>
        public static void Remove(string id)
        {
            for (int i = 0; i < lights.Count; i++)
                if (lights[i].ID == id)
                    lights.Remove(lights[i]);
        }

        /// <summary>
        /// Removes all lights from this.
        /// </summary>
        public static void RemoveAll()
        {
            lights.Clear();
        }
    }
}
