/* Created: 30/12/2014
 * Last Updated: 30/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Engine.Media.Audio
{
    /// <summary>
    /// Used to define a JJaxCue that affects an area.
    /// </summary>
    public sealed class JJaxAreaSoundEffect : JJaxCue
    {
        float range;

        /// <summary>
        /// Creates a new JJaxAreaSoundEffect.
        /// </summary>
        /// <param name="soundName">The name of the sound to be played.</param>
        /// <param name="position">The origin of the sound.</param>
        /// <param name="range">The range of the sound.</param>
        public JJaxAreaSoundEffect(string soundName, Vector3 position, float range)
            : base(AudioManager.GetCue(soundName), position)
        {
            this.range = range;
        }

        protected override void SetSoundVolume()
        {
            if (AudioManager.ListnerExists())
            {
                float distance = Vector3.Distance(emitter.Position, AudioManager.Listener.Position);

                if (distance < range)
                    song.Volume = MathHelper.Clamp(Volume * (distance / range), 0, 1);
                else
                    song.Volume = 0;
            }
            else
                base.SetSoundVolume();
        }

        public override void _Update()
        {
            SetSoundVolume();

            base._Update();
        }
    }
}
