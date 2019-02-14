﻿/* Created: 13/06/2013
 * Last Updated: 08/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Eon.System.Tools;
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
        ScreenQuad quad;

        List<PostProcess> processes = new List<PostProcess>();

        string textureBufferID;
        string outputTextureID;

        int order;

        public int Processes
        {
            get { return processes.Count; }
        }

        public ScreenQuad ScreenQuad
        {
            get { return quad; }
        }

        public int Order
        {
            get { return order; }
        }

        public PostProcessingLocal(string id, string textureBufferID, 
            string outputTextureID, int order)
            : base(id)
        {
            quad = new ScreenQuad();

            this.outputTextureID = outputTextureID;
            this.textureBufferID = textureBufferID;

            this.order = order;
        }

        internal void AddProcess(PostProcess process)
        {
            PostProcess temp = null;

            temp = (from p in processes
                    where p.GetType() == process.GetType()
                    select p).FirstOrDefault();

            if (temp == null)
            {
                process.TextureQualityChanged(
                    (int)Common.TextureQuality.X,
                    (int)Common.TextureQuality.Y);

                process.TextureBufferID = textureBufferID;
                process.OutputTextureID = outputTextureID;

                processes.Add(process);
            }
        }

        internal void RemoveProcess(PostProcess process)
        {
            if (processes.Contains(process))
                processes.Remove(process);
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

        public void TextureQualityChanged()
        {
            for (int i = 0; i < processes.Count; i++)
                processes[i].TextureQualityChanged(
                    (int)Common.TextureQuality.X,
                    (int)Common.TextureQuality.Y);
        }
    }
}
