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
    /// Used to define a particle renderer of Billboards.
    /// </summary>
    public sealed class BBParticleRenderer : IGenerateRenderer, I3DParticleRenderer
    {
        List<Billboard> billboards = new List<Billboard>();

        string textureFilepath;

        public BBParticleRenderer(string textureFilepath)
        {
            this.textureFilepath = textureFilepath;
        }

        public void GenerateNew()
        {
            Billboard bb = new Billboard(Vector3.Zero, 1, Vector3.Zero,
                textureFilepath);

            billboards.Add(bb);
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
            foreach (Billboard b in billboards)
                b.Destroy();

            billboards.Clear();
        }
    }
}
