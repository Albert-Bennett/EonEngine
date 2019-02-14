/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.EngineComponents.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Eon.PostProcessing
{
    /// <summary>
    /// This class defines a collection of post 
    /// processing effects for a certain manager.
    /// 
    /// This is so that a differnt set of effects
    /// can be applied to different dimentional renders.
    /// </summary>
    public class PostProcessingLocal : EngineComponent, IRenderComponent
    {
        Texture2D normalMap;
        Texture2D depthMap;
        Texture2D final;
        Texture2D distortionMap;

        List<PostProcess> processes = new List<PostProcess>();

        int priority;

        public int Priority
        {
            get { return priority; }
        }

        public int Processes
        {
            get { return processes.Count; }
        }

        public Texture2D SceneNormals
        {
            get { return normalMap; }
        }

        public Texture2D SceneDepth
        {
            get { return depthMap; }
        }

        public Texture2D Distortion
        {
            get { return distortionMap; }
        }

        public bool RenderFinal
        {
            get
            {
                if (Processes > 0)
                    return true;

                return false;
            }
        }

        public Texture2D FinalImage
        {
            get { return final; }
            internal set { final = value; }
        }

        public PostProcessingLocal(string id, int priority)
            : base(id)
        {
            this.priority = priority;
        }

        internal void AddProcess(PostProcess process)
        {
            PostProcess temp = null;

            temp = (from p in processes
                    where p.GetType() == process.GetType()
                    select p).FirstOrDefault();

            if (temp == null)
                processes.Add(process);
        }

        internal void RemoveProcess(PostProcess process)
        {
            if (processes.Contains(process))
                processes.Remove(process);
        }

        public void SetTextures(Texture2D scene, Texture2D normals,Texture2D depth, Texture2D distortion)
        {
            final = scene;
            normalMap = normals;
            depthMap = depth;
            distortionMap = distortion;
        }

        public void Render()
        {
            int currentPriotity = 0;
            int idx = 0;

            while (idx < processes.Count)
            {
                for (int i = 0; i < processes.Count; i++)
                    if (processes[i].Priority == currentPriotity)
                    {
                        processes[i]._Render();

                        idx++;
                    }

                currentPriotity++;
            }
        }

        public void ScreenResolutionChanged()
        {
            for (int i = 0; i < processes.Count; i++)
                processes[i].ScreenResolutionChanged();
        }
    }
}
