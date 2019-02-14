/* Created 23/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Engine.Audio;
using Microsoft.Xna.Framework;

namespace Eon.Game
{
    /// <summary>
    /// Creates a new Player object.
    /// </summary>
    public abstract class Player : GameObject, ILevelAsset
    {
        /// <summary>
        /// Creates a new Player object.
        /// </summary>
        public Player()
            : base("Player")
        {
            Disable();
        }

        /// <summary>
        /// Should be only called when the current Level is being trasitioned on.
        /// </summary>
        /// <param name="levelID">The name of the Level being transitioned on.</param>
        public void LevelTransitionOn(string levelID)
        {
            Enable();

            _LevelTransitionOn(levelID);
        }

        protected virtual void _LevelTransitionOn(string levelID) { }

        /// <summary>
        /// Should be only called when the current Level is being trasitioned off.
        /// </summary>
        /// <param name="levelID">The name of the Level being transitioned off.</param>
        public void LevelTransitionOff(string levelID)
        {
            Disable();
            _LevelTransitionOff(levelID);
        }

        protected virtual void _LevelTransitionOff(string levelID) { }

        internal void ReSpawn(Vector3 position)
        {
            //Possibly problematic 
            Matrix temp = World;

            temp -= Matrix.CreateTranslation(temp.Translation);
            temp += Matrix.CreateTranslation(position);

            World = temp;

            AudioManager.SetListnerPosition(World.Translation);
        }

        protected override void Update()
        {
            AudioManager.SetListnerPosition(World.Translation);

            base.Update();
        }
    }
}
