/* Created 13/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Billboards;
using Eon.System.Management;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D
{
    /// <summary>
    /// Defines a manager of 3D drawable objects.
    /// </summary>
    public sealed class ModelManager : EngineComponent
    {
        static List<ModelComponent> models = new List<ModelComponent>();
        static List<Billboard> billboards = new List<Billboard>();

        /// <summary>
        /// The number of models currently being managed.
        /// </summary>
        public static int ModelCount
        {
            get { return models.Count; }
        }

        /// <summary>
        /// Returns the total number of object that can be rendered.
        /// </summary>
        public static int Count
        {
            get
            {
                int count = 0;

                return models.Count + count + billboards.Count;
            }
        }

        internal static List<ModelComponent> Models
        {
            get { return models; }
        }

        public ModelManager() : base("ModelManager") { }

        internal static void Add(ModelComponent model)
        {
            ModelComponent mod = (from m in models
                                  where m.ID == model.ID
                                  select m).FirstOrDefault();

            if (mod == null)
                models.Add(model);
        }

        internal static void Add(Billboard billboard)
        {
            billboards.Add(billboard);
        }

        internal static void GetVisableModels(out List<ModelComponent> visables)
        {
            visables = new List<ModelComponent>();

            for (int i = 0; i < models.Count; i++)
                if (models[i].Enabled)
                    visables.Add(models[i]);
        }

        internal static void Remove(ModelComponent model)
        {
            models.Remove(model);
        }

        internal static void Remove(Billboard billboard)
        {
            billboards.Remove(billboard);
        }

        internal static void Render(RenderTypes renderType)
        {
            switch (renderType)
            {
                case RenderTypes.Transparency:
                    {
                        DepthSortRender();
                    }
                    break;

                case RenderTypes.ForwardRender:
                    {
                        for (int i = 0; i < models.Count; i++)
                            if (models[i].Enabled)
                                models[i].Render(renderType);
                    }
                    break;
            }
        }

        static void DepthSortRender()
        {
            List<DepthSortCache> distances =
                new List<DepthSortCache>();

            for (int i = 0; i < models.Count; i++)
                if (models[i].Enabled)
                {
                    float dist = Vector3.Distance(CameraManager.CurrentCamera.Position,
                        models[i].World.Translation);

                    distances.Add(new DepthSortCache()
                    {
                        Distance = dist,
                        Index = i,
                        Identifier = 'm'
                    });
                }

            for (int i = 0; i < billboards.Count; i++)
                if (billboards[i].Enabled)
                {
                    float dist = Vector3.Distance(CameraManager.CurrentCamera.Position,
                        billboards[i].Position);

                    distances.Add(new DepthSortCache()
                    {
                        Distance = dist,
                        Index = i,
                        Identifier = 'b'
                    });
                }

            distances = distances.OrderByDescending(d => d.Distance).ToList();

            for (int i = 0; i < distances.Count; i++)
                if (distances[i].Identifier == 'b')
                    billboards[distances[i].Index].Render();
                else
                    models[distances[i].Index].Render(RenderTypes.Transparency);
        }

        internal static void Render(RenderTypes renderType, string technique)
        {
            for (int i = 0; i < models.Count; i++)
                if (models[i].Enabled)
                    models[i].Render(renderType, technique);
        }
    }
}
