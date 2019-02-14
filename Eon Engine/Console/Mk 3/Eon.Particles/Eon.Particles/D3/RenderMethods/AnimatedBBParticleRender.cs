/* Created: 11/09/2014
 * Last Updated: 19/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.Attachments.Base;
using Eon.Particles.Base;
using Eon.Rendering3D.Framework.Billboards;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles.D3.RenderMethods
{
    /// <summary>
    /// Used to define an animated Billboard renderer.
    /// </summary>
    public sealed class AnimatedBBParticleRender : IUpdateableRenderer, I3DParticleRenderer
    {
        List<AnimatedBillboard> billboards =
            new List<AnimatedBillboard>();

        string textureFilepath;
        int rows;
        int columns;
        int totalFrames;
        float timeBetweenFrames;

        public AnimatedBBParticleRender(string textureFilepath, int rows,
            int columns, int totalFrames, float timeBetweenFrames)
        {
            this.textureFilepath = textureFilepath;
            this.rows = rows;
            this.totalFrames = totalFrames;
            this.columns = columns;
            this.timeBetweenFrames = timeBetweenFrames;
        }

        public void GenerateNew()
        {
            AnimatedBillboard ani = new AnimatedBillboard(Vector3.Zero, 1, Vector3.Zero,
                textureFilepath, rows, columns, totalFrames, timeBetweenFrames);

            ani.StartRandom();

            billboards.Add(ani);
        }

        public void Update()
        {
            for (int i = 0; i < billboards.Count; i++)
                billboards[i].Update();
        }

        public void Render(List<PropertySet> properties)
        {
            for (int i = 0; i < billboards.Count; i++)
            {
                billboards[i].Scale = properties[i].Scale;
                billboards[i].Rotation = properties[i].Rotation;
                billboards[i].Position = properties[i].Position;
            }
        }

        public void Remove(int index)
        {
            billboards[index].Destroy();
            billboards.RemoveAt(index);
        }

        public void Reset()
        {
            foreach (AnimatedBillboard b in billboards)
                b.Destroy();

            billboards.Clear();
        }
    }
}
