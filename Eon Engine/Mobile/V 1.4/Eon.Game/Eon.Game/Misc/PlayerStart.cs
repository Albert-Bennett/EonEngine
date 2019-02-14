/* Created 30/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.EngineComponents;
using Eon.Game.LevelManagement;
using Microsoft.Xna.Framework;

namespace Eon.Game.Misc
{
    /// <summary>
    /// Defines a positional start for a player of the game.
    /// </summary>
    public class PlayerStart : GameObject, ILevelAsset
    {
        Vector3 position;
        Vector3 size = new Vector3(64, 64, 64);

        /// <summary>
        /// The position that the player will start at when spawned.
        /// </summary>
        public Vector3 StartingPosition { get { return position; } }

        /// <summary>
        /// The size of this PlayerStart.
        /// </summary>
        public Vector3 Size { get { return size; } }

        /// <summary>
        /// Creates a new PlayerStart.
        /// </summary>
        /// <param name="id">The id of this player start.</param>
        /// <param name="position">The position where the player will be spawned at.</param>
        public PlayerStart(string id, Vector3 position)
            : base(id)
        {
            this.position = position;
        }

        public void LevelTransitionOn(string levelID)
        {
            _LevelTransitionOn(levelID);

            Player p = LevelManager.GetPlayer();

            if (p != null)
                p.ReSpawn(position);
        }

        protected virtual void _LevelTransitionOn(string levelID)
        {
            EngineComponent comp = EngineComponentManager.Find("Camera2DManager");

            if (comp == null)
            {
                comp = EngineComponentManager.Find("Camera3DManager");

                if (comp != null)
                    comp.SendMessage("LockToGameObject", position);
            }
            else
                comp.SendMessage("LockToGameObject", position, size);
        }

        public void LevelTransitionOff(string levelID)
        {
            _LevelTransitionOff(levelID);
        }

        protected virtual void _LevelTransitionOff(string levelID) { }
    }
}
