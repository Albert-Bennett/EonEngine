/* Created 06/04/2015
 * Last Updated: 06/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Engine.Media.Audio.Components
{
    /// <summary>
    /// Used to define a component that is specific to the audio manager.
    /// </summary>
    public abstract class AudioComponent
    {
        string name;

        /// <summary>
        /// The name of the AudioComponent.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Creates a new AudioComponent.
        /// </summary>
        /// <param name="name">The name of the AudioComponent.</param>
        public AudioComponent(string name)
        {
            this.name = name;

            AudioManager.Add(this);
        }

        internal void _Update()
        {
            Update();
        }

        protected virtual void Update() { }

        public void Destroy()
        {
            AudioManager.Destroy(this);
        }
    }
}
