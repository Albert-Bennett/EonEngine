/* Created 14/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

public delegate void HasFinishedEvent(string id);

/// <summary>
/// An enum used to define the variouis 
/// states that an animation can have.
/// </summary>
public enum AnimationStates
{
    Playing,
    Paused,
    Stopped
}
