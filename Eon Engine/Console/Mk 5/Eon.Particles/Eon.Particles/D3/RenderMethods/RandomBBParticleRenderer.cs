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
    /// Used to define a particle renderer for RandomTextureBillboards.
    /// </summary>
    public sealed class RandomBBParticleRenderer : IGenerateRenderer, I3DParticleRenderer
    {
        List<AnimatedBillboard> billboards =
            new List<AnimatedBillboard>();

        string textureFilepath;

        int rows;
        int columns;
        int totalFrames;

        public RandomBBParticleRenderer(string textureFilepath, int rows,
            int columns, int totalFrames)
        {
            this.textureFilepath = textureFilepath;
            this.rows = rows;
            this.totalFrames = totalFrames;
            this.columns = columns;
        }

        public void GenerateNew()
        {
            AnimatedBillboard ani = new AnimatedBillboard(Vector3.Zero, 1, Vector3.Zero,
                textureFilepath, rows, columns, totalFrames, 0);

            ani.Start();

            billboards.Add(ani);
        }

        public void Render(List<ParticlePropertySet> properties)
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
