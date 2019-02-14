/* Created: 01/09/2014
 * Last Updated: 11/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Particles.D3.EmittionTypes.Base
{
    /// <summary>
    /// Used to define an means of emitting Particles.
    /// </summary>
    public interface IEmitter3D
    {
        Vector3 GeneratePosition();
        void SetPosition(Vector3 position);
    }
}
