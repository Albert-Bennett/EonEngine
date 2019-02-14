/* Created: 01/09/2014
 * Last Updated: 11/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.Attachments.Base;
using System.Collections.Generic;

namespace Eon.Particles.Base
{
    /// <summary>
    /// Used to define a means of rendering particles.
    /// </summary>
    public interface IParticleRenderer
    {
        void Render(List<ParticlePropertySet> properties);
    }
}
