/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.Particles2D.Types
{
    /// <summary>
    /// Used to define a IParticleType which can 
    /// change depending on the ParticleEmitter 
    /// that it belongs to.
    /// </summary>
    public interface IChangeable
    {
        void Generate();
        void Remove(int index);
    }
}
