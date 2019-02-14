/* Created: 23/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using System.Collections.Generic;

namespace Eon.Physics2D.Collision
{
    /// <summary>
    /// Defines an EngineComponent that is 
    /// used to manage collisions between objects.
    /// </summary>
    public sealed class CollisionManager : IUpdate
    {
        static List<CollisionComponent> collisionComps = new List<CollisionComponent>();

        public int Priority
        {
            get { return 0; }
        }

        public void _Update()
        {
            //int count = collisionComps.Count;

            //for (int x = 0; x < count; x++)
            //    for (int y = 0; y < count; y++)
            //    {
            //        switch (collisionComps[x].Shape)
            //        {
            //            case CollisionShapes.Circle:
            //                {
            //                    switch (collisionComps[y].Shape)
            //                    {
            //                        case CollisionShapes.Circle:
            //                            {
            //                                Vector2 poc = Vector2.Zero;

            //                                CollisionType collides = CollisionShapeHelper.Collides((BoundingCircle)(collisionComps[x].Bounds),
            //                                      (BoundingCircle)(collisionComps[y].Bounds), out poc);

            //                                if (collides != CollisionType.None)
            //                                {
            //                                    CollisionInfo info = new CollisionInfo()
            //                                    {
            //                                        Instigator = collisionComps[x],
            //                                        Collider = collisionComps[y],
            //                                        PointOfContact = poc
            //                                    };

            //                                    collisionComps[x].Collided(info);
            //                                }
            //                            }
            //                            break;

            //                        case CollisionShapes.Rectangle:
            //                            {
            //                                Vector2 poc = Vector2.Zero;

            //                                CollisionType collides = CollisionShapeHelper.Collides((Rectangle)(collisionComps[y].Bounds),
            //                                      (BoundingCircle)(collisionComps[x].Bounds), out poc);

            //                                if (collides != CollisionType.None)
            //                                {
            //                                    CollisionInfo info = new CollisionInfo()
            //                                    {
            //                                        Instigator = collisionComps[x],
            //                                        Collider = collisionComps[y],
            //                                        PointOfContact = poc
            //                                    };

            //                                    collisionComps[x].Collided(info);
            //                                }
            //                            }
            //                            break;
            //                    }
            //                }
            //                break;

            //            case CollisionShapes.Rectangle:
            //                {
            //                    switch (collisionComps[y].Shape)
            //                    {
            //                        case CollisionShapes.Circle:
            //                            {
            //                                Vector2 poc = Vector2.Zero;

            //                                CollisionType collides = CollisionShapeHelper.Collides((Rectangle)(collisionComps[x].Bounds),
            //                                      (BoundingCircle)(collisionComps[y].Bounds), out poc);

            //                                if (collides != CollisionType.None)
            //                                {
            //                                    CollisionInfo info = new CollisionInfo()
            //                                    {
            //                                        Instigator = collisionComps[x],
            //                                        Collider = collisionComps[y],
            //                                        PointOfContact = poc
            //                                    };

            //                                    collisionComps[x].Collided(info);
            //                                }
            //                            }
            //                            break;

            //                        case CollisionShapes.Rectangle:
            //                            {
            //                                Vector2 poc = Vector2.Zero;

            //                                CollisionType collides = CollisionShapeHelper.Collides((Rectangle)(collisionComps[x].Bounds),
            //                                      (Rectangle)(collisionComps[y].Bounds), out poc);

            //                                if (collides != CollisionType.None)
            //                                {
            //                                    CollisionInfo info = new CollisionInfo()
            //                                    {
            //                                        Instigator = collisionComps[x],
            //                                        Collider = collisionComps[y],
            //                                        PointOfContact = poc
            //                                    };

            //                                    collisionComps[x].Collided(info);
            //                                }
            //                            }
            //                            break;
            //                    }
            //                }
            //                break;
            //        }
            //    }
        }

        public void _PostUpdate() { }
    }
}
