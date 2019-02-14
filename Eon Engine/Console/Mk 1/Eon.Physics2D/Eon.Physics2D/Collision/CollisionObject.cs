/* Created 23/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Math;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Collision
{
    /// <summary>
    /// Defines a collidable ObjectComponent.
    /// </summary>
    public sealed class CollisionObject
    {
        CollisionShapes shape;
        object bounds;
        string id;

        string attachedTo;

        public CollisionEvent OnCollided;

        /// <summary>
        /// The collision space for this CollidableRectangle.
        /// </summary>
        public object Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// The ID of an object that this is attached to.
        /// </summary>
        public string AttachedTo
        {
            get { return attachedTo; }
        }

        /// <summary>
        /// The identifaction name of this CollisionObject.
        /// </summary>
        public string ID { get { return id; } }

        internal CollisionShapes Shape
        {
            get { return shape; }
        }

        /// <summary>
        /// Creates a new CollisionObject.
        /// </summary>
        /// <param name="id">The id of this CollidableRectangle.</param>
        /// <param name="bounds">The bounding area of this CollidableRectangle.</param>
        public CollisionObject(string id, Rectangle bounds)
        {
            this.bounds = bounds;
            this.id = id;

            shape = CollisionShapes.Rectangle;

            BroadPhase.Add(this);
        }

        /// <summary>
        /// Creates a new CollisionObject.
        /// </summary>
        /// <param name="id">The id of this CollidableRectangle.</param>
        /// <param name="bounds">The bounding area of this CollidableRectangle.</param>
        public CollisionObject(string id, BoundingCircle bounds)
        {
            this.bounds = bounds;
            this.id = id;

            shape = CollisionShapes.Circle;

            BroadPhase.Add(this);
        }

        /// <summary>
        /// Changes which object that this is attached to.
        /// </summary>
        /// <param name="id">The ID of the attached object.</param>
        public void SetAttachedTo(string id)
        {
            this.attachedTo = id;
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

        public void Destroy()
        {
            BroadPhase.Remove(this);
        }
    }
}
