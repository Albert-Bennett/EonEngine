/* Created: 15/12/2013
 * Last Updated: 28/10/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.Testing;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// Defines a manager class for LightComponents.
    /// </summary>
    public sealed class LightManager : EngineComponent
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
            LightComponent3D l = null;

            l = (from i in lights
                 where i.ID == light.ID
                 select i).FirstOrDefault();

            if (l == null)
                lights.Add(light);
            else
            {
                l.Destroy(true);

                new Error("A light of the same name already exists: " + l.ID, Seriousness.Error);
            }
        }

        internal static void Remove(string lightID)
        {
            bool found = false;
            int i = 0;

            while (!found && i < lights.Count)
            {
                if (lights[i].ID == lightID)
                {
                    found = true;
                    lights.Remove(lights[i]);
                }

                i++;
            }
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
        internal static List<T> GetLights<T>() where T : LightComponent3D
        {
            return (from l in LightManager.lights
                    where l.Enabled && l.IsInView && l.GetType() == typeof(T)
                    select l as T).ToList<T>();
        }
    }
}
