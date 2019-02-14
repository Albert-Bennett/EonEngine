/* Created 05/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Engine.Audio
{
    /// <summary>
    /// Info about a sound cue. 
    /// </summary>
    public class CueInfo
    {
        public string Name;
        public string Filepath;
        public string CategoryName;

        public float Volume;
        public bool Loop;
        public bool SingleInstance;
    }
}
