/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Particles.D2.RenderTypes.Base
{
    /// <summary>
    /// Defines a renderer that keeps track of various data 
    /// for each particle.
    /// </summary>
    public interface IUpdateableRenderer : IRenderer2D
    {
        void GenerateNew();
        void Update();
        void Remove(int index);
        void Reset();
    }
}
