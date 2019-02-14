/* Created 24/09/2015
 * Last Updated: 15/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Graphics;

namespace Eon.Animation2D.StateManagement
{
    /// <summary>
    /// Defines a state an Animation can have. 
    /// [Serializable]
    /// </summary>
    public sealed class AnimState
    {
        public string State;
        public string Filepath;
        public bool Loop = true;

        public ImageOrentation Orentation;

        public object Animation;
    }
}
