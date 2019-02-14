/* Created 15/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
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
            List<T> l = new List<T>();

            for (int i = 0; i < lights.Count; i++)
                if (lights[i].GetType() == typeof(T))
                    if (lights[i].Enabled && lights[i].IsInView)
                        l.Add(lights[i] as T);

            return l;
        }

        /// <summary>
        /// Used to find every light that can cast shadows.
        /// </summary>
        /// <returns>A list of LightComponent3D that can cast shadows.</returns>
        internal static List<ICastShadows> GetShadowCasters()
        {
            List<ICastShadows> casters = new List<ICastShadows>();

            for (int i = 0; i < lights.Count; i++)
                if(lights[i] is ICastShadows)
                if (lights[i].Enabled && ((ICastShadows)lights[i]).CastsShadows)
                    casters.Add(lights[i] as ICastShadows);

            return casters;
        }
    }
}
