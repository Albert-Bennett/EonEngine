/* Created 04/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Audio;
using Eon.Game.Audio;
using Eon.Physics2D.Collision;
using Eon.Physics2D.Forces.Volumes;
using Microsoft.Xna.Framework;

namespace Eon.Game2D.Audio
{
    /// <summary>
    /// Defines an object that only plays a sound
    /// when the player is inside of it.
    /// </summary>
    public sealed class SoundVolume : PhysicsVolume
    {
        string soundName;
        AudioTypes playType;
        Vector3 audioOrigin;

        /// <summary>
        /// Creates a new SoundVolume.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to give to the SoundVolume.</param>
        /// <param name="bounds">The bounds of the SoundVolume.</param>
        /// <param name="soundName">The name of the sound to be played.</param>
        public SoundVolume(string id, Rectangle bounds, string soundName)
            : base(id, bounds)
        {
            this.soundName = soundName;
            playType = AudioTypes.Ambient;

            audioOrigin = new Vector3()
            {
                X = bounds.Center.X,
                Y = bounds.Center.Y,
                Z = 0
            };
        }

        protected override void Collide(CollisionInfo info)
        {
            if (info.Collider.ID == "Player" || info.Instigator.ID == "Player")
                if (!AudioManager.IsPlaying(soundName))
                    switch (playType)
                    {
                        case AudioTypes.Ambient:
                            {
                                AudioManager.Play(soundName);
                            }
                            break;

                        case AudioTypes.World:
                            {
                                Vector3 pos = new Vector3(
                                    info.PointOfContact.X,
                                    info.PointOfContact.Y, 0);

                                AudioManager.PlaySound3D(soundName, audioOrigin);
                            }
                            break;
                    }
        }

        protected override Vector2 CalculateForce(Vector2 position)
        {
            return Vector2.Zero;
        }
    }
}
