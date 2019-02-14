/* Created 14/09/2013
 * Last Updated: 19/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

/// <summary>
/// A delegate that is used to signal when a skeletal animation has finished.
/// </summary>
/// <param name="id">The ID of the finished Animation.</param>
public delegate void HasFinishedEvent(string id);

/// <summary>
/// A delegate that is used to signal when a LimbAnimation has finished.
/// </summary>
public delegate void LimbAnimationFinishedEvent();
