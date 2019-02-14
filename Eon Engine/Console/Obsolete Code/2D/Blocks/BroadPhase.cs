/* Created: 05/11/2013
 * Last Updated: 24/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.System.Interfaces;
using Eon.System.Management;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Physics2D.Collision.Phases.BroadPhase
{
    /// <summary>
    /// Defines the BroadPhase in the collision pipeline.
    /// </summary>
    public sealed class BroadPhase : EngineModule, IUpdate
    {
        static BlockManager blockManager = new BlockManager();

        static List<SimplePhysicsComponent> simpleComps = new List<SimplePhysicsComponent>();
        static List<PhysicsComponent> collisionComps = new List<PhysicsComponent>();

        public int Priority
        {
            get { return 1; }
        }

        public BroadPhase() : base("BroadPhase") { }

        internal static List<PhysicsComponent> GetCollisionObjects()
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

        public void _PostUpdate() { }

        public static void SetCollidables(EonDictionary<string, string> collidables)
        {
            EonDictionary<string, string> collides = collidables;

            //Set the objects for collision for the narrow phase.
        }

        internal static void Add(PhysicsComponent obj)
        {
            PhysicsComponent comp = null;

            comp = (from c in collisionComps
                    where c.ID == obj.ID
                    select c).FirstOrDefault();

            if (comp == null)
                collisionComps.Add(obj);
        }

        internal static void Add(SimplePhysicsComponent obj)
        {
           SimplePhysicsComponent comp = null;

            comp = (from c in simpleComps
                    where c.ID == obj.ID
                    select c).FirstOrDefault();

            if (comp == null)
                simpleComps.Add(obj);
        }

        internal static void Remove(PhysicsComponent collision)
        {
            if (collisionComps.Contains(collision))
                collisionComps.Remove(collision);
        }

        internal static void Remove(SimplePhysicsComponent collision)
        {
            if (simpleComps.Contains(collision))
                simpleComps.Remove(collision);
        }

        protected override void Destroy()
        {
            blockManager.Dispose();

            collisionComps.Clear();
            collisionComps = null;

            simpleComps.Clear();
            simpleComps = null;

            base.Destroy();
        }
    }
}
