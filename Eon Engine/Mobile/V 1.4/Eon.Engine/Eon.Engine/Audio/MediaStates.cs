/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Engine.Audio
{
    /// <summary>
    /// The different states that media items can be in.
    /// </summary>
    public enum MediaStates
    {
        /// <summary>
        /// When a media object is playing.
        /// </summary>
        Playing,

        /// <summary>
        /// When the playing of a media object has been paused.
        /// </summary>
        Paused,

        /// <summary>
        /// When the media object has been stopped.
        /// </summary>
        Stopped
    }
}