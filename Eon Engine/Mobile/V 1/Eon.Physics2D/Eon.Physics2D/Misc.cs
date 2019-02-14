/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision;

/// <summary>
/// An enum used to define what type of collision has occourred. 
/// </summary>
public enum CollisionType
{
    /// <summary>
    /// Indicates no collision.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates a partial collision has occoured.
    /// </summary>
    Partial,

    /// <summary>
    /// Indicates that a full collsision has occoured.
    /// </summary>
    Full
}

/// <summary>
/// An enum used to define all opf the various collision shapes.
/// </summary>
public enum CollisionShapes
{
    /// <summary>
    /// Defines a rectangular collision shape.
    /// </summary>
    Rectangle,

    /// <summary>
    /// Defines a circular collision shape. 
    /// </summary>
    Circle
}

/// <summary>
/// A delegate used to signal that a collision has occoured.
/// </summary>
/// <param name="info">Information from the collision.</param>
public delegate void CollisionEvent(CollisionInfo info);
