﻿/* Created 23/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Collision
{
    /// <summary>
    /// A struct used to hold infomation about a particular collision. 
    /// </summary>
    public struct CollisionInfo
    {
        public Vector2 PointOfContact;
        public CollisionObject Instigator;
        public CollisionObject Collider;
    }
}
