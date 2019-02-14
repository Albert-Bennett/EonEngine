/* Created: 13/06/2013
 * Last Updated: 03/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Eon.System.Tools;
using Eon.Testing;
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
    public class PostProcessingLocal : EngineModule, IRenderComponent
    {
        ScreenQuad quad;
        TextureBuffer buffer;

        List<PostProcess> processes = new List<PostProcess>();

        int order;

        public int Processes
        {
            get { return processes.Count; }
        }

        public ScreenQuad ScreenQuad
        {
            get { return quad; }
        }

        internal TextureBuffer Buffer
        {
            get { return buffer; }
        }

        public int Order
        {
            get { return order; }
        }

        /// <summary>
        /// Creates a new store for applying PostProcessing effects.
        /// </summary>
        /// <param name="id">The ID of the PostProcessingLocal.</param>
        /// <param name="buffer">The TextureBuffer to be used.</param>
        /// <param name="order">The order in which this PostProcessingLocal is to be rendered.</param>
        public PostProcessingLocal(string id, TextureBuffer buffer, int order)
            : base(id)
        {
            quad = new ScreenQuad();

            this.buffer = buffer;

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

                processes.Add(process);
            }
        }

        /// <summary>
        /// Removes a PostProcess.
        /// </summary>
        /// <param name="process">The PostProcess to be removed.</param>
        public void RemoveProcess(PostProcess process)
        {
            if (process != null)
                if (processes.Contains(process))
                    processes.Remove(process);
                else
                    new Error("The PostProcess to be removed dosen't exist.", Seriousness.Warning);
            else
                new Error("The PostProcess: " + process.ID + " is null.", Seriousness.Error);
        }

        /// <summary>
        /// Removes a PostProcess.
        /// </summary>
        /// <param name="process">The ID of the PostProcess to be removed.</param>
        public void RemoveProcess(string processID)
        {
            PostProcess pp = (from p in processes
                              where p.ID == processID
                              select p).FirstOrDefault();

            if (pp != null)
                processes.Remove(pp);
            else
                new Error("The PostProcess: " + processID + " dosen't exist.", Seriousness.Error);
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
