/* Created: 02/10/2013
 * Last Updated: 02/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Physics2D.Maths;

/// <summary>
/// A delegate used to signal that a collision has occoured.
/// </summary>
/// <param name="instigator">What caused the collision.</param>
/// <param name="mtv">The minimum traversal vector.</param>
public delegate void CollisionEvent(MTV mtv, GameObject instigator);
