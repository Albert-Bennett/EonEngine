/* Created: 01/09/2014
 * Last Updated: 11/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Particles.Base
{
    /// <summary>
    /// Defines a renderer that keeps track of various data 
    /// for each particle.
    /// </summary>
    public interface IUpdateableRenderer : IGenerateRenderer
    {
        void Update();
    }
}
