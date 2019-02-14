/* Created: 13/05/2014
 * Last Updated: 05/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Framework;
using Eon.System.Interfaces;
using Eon.System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D
{
    /// <summary>
    /// Defines a manager of 3D drawable objects.
    /// </summary>
    public sealed class ModelManager : EngineModule, IUpdate
    {
        #region Properties

        static List<IRenderable3D> objects = new List<IRenderable3D>();
        static List<int> enabled = new List<int>();

        static int lppCount = 0;
        static int semiLPPCount = 0;

        /// <summary>
        /// Returns the total number of object that can be rendered.
        /// </summary>
        public static int Count { get { return objects.Count; } }

        public int Priority
        {
            get { return 2; }
        }

        #endregion
        #region Ctor

        public ModelManager() : base("ModelManager") { }

        #endregion
        #region Updating/ Rendering

        public void _Update()
        {
            if (objects.Count > 0)
            {
                enabled = (from m in objects
                           where m.Enabled && m.IsSeen
                           select objects.IndexOf(m)).ToList<int>();
            }
        }

        public void _PostUpdate() { }

        internal static void Render(RenderTypes renderType, string technique)
        {
            for (int i = 0; i < enabled.Count; i++)
                if (objects[enabled[i]].RenderType == renderType)
                    objects[enabled[i]]._Render(technique);
        }

        internal static void PreRender()
        {
            for (int i = 0; i < enabled.Count; i++)
                objects[enabled[i]]._PreRender();
        }

        #endregion
        #region Adding / Removing

        internal static void Add(IRenderable3D obj)
        {
            switch (obj.RenderType)
            {
                case RenderTypes.SemiLPP:
                    semiLPPCount++;
                    break;

                default:
                    lppCount++;
                    break;
            }

            objects.Add(obj);
        }

        /// <summary>
        /// Removes an IRenderable3D object from the model manager.
        /// </summary>
        /// <param name="obj">The obj to be removed.</param>
        public static void Remove(IRenderable3D obj)
        {
            if (objects.Contains(obj))
            {
                switch (obj.RenderType)
                {
                    case RenderTypes.SemiLPP:
                        semiLPPCount--;
                        break;

                    default:
                        lppCount--;
                        break;
                }

                objects.Remove(obj);
            }
        }

        #endregion
        #region Helpers

        internal static int ModelCount(RenderTypes renderType)
        {
            switch (renderType)
            {
                case RenderTypes.LPP:
                    return lppCount;

                default:
                    return semiLPPCount;
            }
        }

        internal static void GetByRenderType(RenderTypes renderType,
            out List<IRenderable3D> renders)
        {
            renders = (from e in enabled
                       where objects[e].RenderType == renderType
                       select objects[e] as IRenderable3D).ToList<IRenderable3D>();
        }

        internal static void GetVisableModels(out List<MeshPart> visables)
        {
            visables = new List<MeshPart>();

            for (int i = 0; i < enabled.Count; i++)
                if (objects[enabled[i]] is MeshPart)
                    visables.Add(objects[enabled[i]] as MeshPart);
        }

        #endregion
    }
}
