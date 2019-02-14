/* Created 23/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision;
using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Math;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines a collidable ObjectComponent.
    /// </summary>
    public sealed class CollisionComponent : ObjectComponent
    {
        CollisionShapes shape;
        object bounds;

        public CollisionEvent OnCollided;

        /// <summary>
        /// The collision space for this CollidableRectangle.
        /// </summary>
        public object Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// The collsion shape of the CollisionComponent.
        /// </summary>
        public CollisionShapes Shape
        {
            get { return shape; }
        }

        /// <summary>
        /// Creates a new CollisionObject.
        /// </summary>
        /// <param name="id">The id of this CollidableRectangle.</param>
        /// <param name="bounds">The bounding area of this CollidableRectangle.</param>
        public CollisionComponent(string id, Rectangle bounds)
            : base(id)
        {
            this.bounds = bounds;

            shape = CollisionShapes.Rectangle;

            BroadPhase.Add(this);
        }

        /// <summary>
        /// Creates a new CollisionObject.
        /// </summary>
        /// <param name="id">The id of this CollidableRectangle.</param>
        /// <param name="bounds">The bounding area of this CollidableRectangle.</param>
        public CollisionComponent(string id, BoundingCircle bounds)
            : base(id)
        {
            this.bounds = bounds;

            shape = CollisionShapes.Circle;

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
        public void SetBounds(object newBounds)
        {
            switch (shape)
            {
                case CollisionShapes.Circle:
                    {
                        if (newBounds is BoundingCircle)
                            bounds = (BoundingCircle)newBounds;
                    }
                    break;

                case CollisionShapes.Rectangle:
                    {
                        if (newBounds is Rectangle)
                            bounds = (Rectangle)newBounds;
                    }
                    break;
            }
        }

        /// <summary>
        /// Sets the center position of the Bounds.
        /// </summary>
        /// <param name="position">The position to set the
        /// center of the bounds to.</param>
        public void SetPosition(Vector2 position)
        {
            if (bounds is BoundingCircle)
                ((BoundingCircle)bounds).Center = position;
            else
            {
                Rectangle rect = ((Rectangle)bounds);

                rect.X = (int)position.X - (rect.Width / 2);
                rect.Y = (int)position.Y - (rect.Height / 2);

                bounds = rect;
            }
        }

        public override void Destroy(bool remove)
        {
            BroadPhase.Remove(this);

            base.Destroy(remove);
        }
    }
}
