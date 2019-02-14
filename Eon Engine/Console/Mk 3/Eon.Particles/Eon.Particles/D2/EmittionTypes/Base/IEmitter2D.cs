/* Created: 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Particles.D2.EmittionTypes.Base
{
    /// <summary>
    /// Used to define an means of emitting Particles.
    /// </summary>
    public interface IEmitter2D
    {
        Vector2 GeneratePosition();

        void SetPosition(Vector2 position);
    }
}
