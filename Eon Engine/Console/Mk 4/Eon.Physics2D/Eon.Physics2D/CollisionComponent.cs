/* Created: 23/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision;
using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Maths.Shapes;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines a collidable ObjectComponent.
    /// </summary>
    public sealed class CollisionComponent : ObjectComponent
    {
        ConvexShape bounds;
        bool isStatic;

        public CollisionEvent OnCollided;

        /// <summary>
        /// The collision space for this CollidableRectangle.
        /// </summary>
        public ConvexShape Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// Wheather or not the CollisionComponent is moveable.
        /// </summary>
        public bool Static
        {
            get { return isStatic; }
        }

        /// <summary>
        /// Creates a new CollisionObject.
        /// </summary>
        /// <param name="id">The id of this CollidableRectangle.</param>
        /// <param name="bounds">The bounding area of this CollidableRectangle.</param>
        public CollisionComponent(string id, ConvexShape bounds)
            : base(id)
        {
            this.bounds = bounds;

            BroadPhase.Add(this);
        }

        /// <summary>
        /// Creates a new CollisionObject.
        /// </summary>
        /// <param name="id">The id of this CollidableRectangle.</param>
        /// <param name="bounds">The bounding area of this CollidableRectangle.</param>
        /// <param name="isStatic">Wheather or not the CollisionComponent is moveable.</param>
        public CollisionComponent(string id, ConvexShape bounds, bool isStatic)
            : base(id)
        {
            this.bounds = bounds;
            this.isStatic = isStatic;

            BroadPhase.Add(this);
        }

        internal void Collided(CollisionInfo info)
        {
            if (OnCollided != null)
                OnCollided(info);
        }

        /// <summary>
        /// Sets the collision area for this CollisionObject.
        /// </summary>
        /// <param name="newBounds">The new collision area.</param>
        public void SetBounds(ConvexShape newBounds)
        {
            bounds = newBounds;
        }

        /// <summary>
        /// Sets the center position of the Bounds.
        /// </summary>
        /// <param name="position">The position to set the
        /// center of the bounds to.</param>
        public void SetPosition(Vector2 position)
        {
            bounds.Vertices[0] = position;
        }

        public override void Destroy(bool remove)
        {
            BroadPhase.Remove(this);

            base.Destroy(remove);
        }
    }
}
