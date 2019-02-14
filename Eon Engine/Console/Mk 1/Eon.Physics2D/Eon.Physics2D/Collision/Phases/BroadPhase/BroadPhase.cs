/* Created 05/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Collections;
using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Physics2D.Collision.Phases.BroadPhase
{
    /// <summary>
    /// Defines the BroadPhase in the collision pipeline.
    /// </summary>
    public class BroadPhase : IUpdate, IDispose
    {
        static BlockManager blockManager = new BlockManager();
        static List<CollisionObject> collisionComps = new List<CollisionObject>();

        internal static List<CollisionObject> GetCollisionObjects()
        {
            return collisionComps;
        }

        public static void ReInitialize(Rectangle worldSize)
        {
            blockManager.ReInitialize(worldSize);
        }

        public void _Update()
        {
            blockManager.Update();
        }

        public static void SetCollidables(EonDictionary<string, string> collidables)
        {
            EonDictionary<string, string> collides = collidables;

            //Set the objects for collision for the narrow phase.
        }

        internal static void Add(CollisionObject obj)
        {
            CollisionObject comp = null;

            comp = (from c in collisionComps
                    where c.ID == obj.ID
                    select c).FirstOrDefault();

            if (comp == null)
                collisionComps.Add(obj);
        }

        internal static void Remove(CollisionObject collision)
        {
            if (collisionComps.Contains(collision))
                collisionComps.Remove(collision);
        }

        /// <summary>
        /// Disposes of the BroadPhase.
        /// </summary>
        /// <param name="finalize">Finalize the deposition.</param>
        public void Dispose(bool finalize)
        {
            blockManager.Dispose();

            collisionComps.Clear();
            collisionComps = null;
        }
    }
}
