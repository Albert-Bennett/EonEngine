/* Created: 01/09/2014
 * Last Updated: 30/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;

namespace Eon.Particles.D2.RenderMethods
{
    /// <summary>
    /// Used to manage multiple AnimatedSprites.
    /// </summary>
    internal struct AnimatedSpriteCache
    {
        public int Row;
        public int Column;
        public int Frame;

        public TimeSpan currentTime;
    }
}
