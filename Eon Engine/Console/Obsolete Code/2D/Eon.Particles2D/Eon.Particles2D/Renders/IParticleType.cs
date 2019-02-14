/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;

namespace Eon.Particles2D.Renders
{
    /// <summary>
    /// Used to define a method for rendering particles. 
    /// </summary>
    public interface IParticleType
    {
        void PreDraw(DrawingStage stage);
        void Draw(Vector2 position, float scale, float rotation, Color colour);
        void Dispose(bool finalize);
    }
}
