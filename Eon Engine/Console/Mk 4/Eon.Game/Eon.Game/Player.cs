/* Created 23/10/2013
 * Last Updated: 07/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Microsoft.Xna.Framework;

namespace Eon.Game
{
    /// <summary>
    /// Creates a new Player object.
    /// </summary>
    public abstract class Player : GameObject, ILevelAsset
    {
        string checkPointID;

        /// <summary>
        /// The ID of the previously past checkpoint.
        /// </summary>
        public string CheckPointID
        {
            get { return checkPointID; }
            protected set { checkPointID = value; }
        }

        /// <summary>
        /// Creates a new Player object.
        /// </summary>
        public Player()
            : base("Player") { }

        /// <summary>
        /// Should be only called when the current Level is being trasitioned on.
        /// </summary>
        /// <param name="levelID">The name of the Level being transitioned on.</param>
        public void LevelTransitionOn(string levelID)
        {
            _LevelTransitionOn(levelID);
        }

        protected virtual void _LevelTransitionOn(string levelID) { }

        /// <summary>
        /// Should be only called when the current Level is being trasitioned off.
        /// </summary>
        /// <param name="levelID">The name of the Level being transitioned off.</param>
        public void LevelTransitionOff(string levelID)
        {
            _LevelTransitionOff(levelID);

            checkPointID = "";
        }

        protected virtual void _LevelTransitionOff(string levelID) { }

        internal void ReSpawn(Vector3 position)
        {
            World.Position = position;
        }
    }
}
