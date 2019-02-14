/* Created: 11/09/2014
 * Last Updated: 11/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Particles.Base
{
    /// <summary>
    /// Used to define a particle renderer that is 
    /// used to generate particles types.
    /// </summary>
    public interface IGenerateRenderer : IParticleRenderer
    {
        void GenerateNew();
        void Remove(int index);
        void Reset();
    }
}
