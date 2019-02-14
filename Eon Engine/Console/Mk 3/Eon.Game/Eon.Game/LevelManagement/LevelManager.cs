/* Created 03/11/2013
 * Last Updated: 25/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Helpers;
using Eon.System.Management;
using System.Collections.Generic;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// An overridable engine component that 
    /// is used to manage Levels in a game.
    /// </summary>
    public sealed class LevelManager : EngineComponent
    {
        const string levelManagerInfoFilepath = "LevelManager.LevelManager";

        static Dictionary<string, string> levelFilepaths =
             new Dictionary<string, string>();

        static Level currentLevel = null;
        static LevelManagerInfo info;

        static string prevLevel;

        static Player player;

        public static StartLoadingEvent OnStartLoading;
        public static EndLoadingEvent OnEndLoading;

        /// <summary>
        /// Creates a new LevelManager.
        /// </summary>
        public LevelManager() : base("LevelManager") { }

        protected override void Initialize()
        {
            info = SerializationHelper.Deserialize<LevelManagerInfo>(levelManagerInfoFilepath, true, "");

            for (int i = 0; i < info.LevelName.Length; i++)
                levelFilepaths.Add(info.LevelName[i], info.LevelFilepaths[i]);

            base.Initialize();
        }

        /// <summary>
        /// Loads a Level.
        /// </summary>
        /// <param name="levelName">The name of the level to be loaded.</param>
        public static void LoadLevel(string levelName)
        {
            if (levelFilepaths.ContainsKey(levelName))
            {
                if (currentLevel != null)
                {
                    if (currentLevel.LevelName != levelName)
                    {
                        currentLevel.Dispose();
                        currentLevel = null;

                        AudioManager.DestroyListner();

                        if (player != null)
                            player.LevelTransitionOff(prevLevel);

                        currentLevel = new Level(levelFilepaths[levelName]);
                    }
                }
                else
                {
                    FirstLoading(levelName);

                    //Threading...

                    currentLevel.Initate();

                    if (player != null)
                        player.LevelTransitionOn(levelName);

                    prevLevel = levelName;

                    if (OnEndLoading != null)
                        OnEndLoading();
                }
            }
        }

        /// <summary>
        /// Sends a message to the current Level.
        /// </summary>
        /// <param name="objectID">The ID of the object to get the message.</param>
        /// <param name="message">The message.</param>
        /// <returns>Any return object.</returns>
        public static object SendMessage(string objectID, string message, params object[] args)
        {
            if (currentLevel != null)
                return currentLevel.SendMessage(objectID, message, args);

            return null;
        }

        /// <summary>
        /// Sends a message to the current Level.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>Any object to be returned.</returns>
        public static object SendMessage(Message message)
        {
            if (currentLevel != null)
                return currentLevel.SendMessage(message);

            return null;
        }

        static void FirstLoading(string levelName)
        {
            currentLevel = new Level(levelFilepaths[levelName] + ".Lvl");

            if (info.PlayerObject != null)
                player = AssemblyManager.CreateInstance(info.PlayerObject) as Player;
        }

        /// <summary>
        /// Finds a GameObject in the current Level.
        /// </summary>
        /// <param name="objectID">The ID of the GameObject to be found.</param>
        /// <returns>The result of the check.</returns>
        public static GameObject FindObject(string objectID)
        {
            if (currentLevel != null)
                return currentLevel.GetGameObject(objectID);

            return null;
        }

        /// <summary>
        /// Gets the player of the game.
        /// </summary>
        /// <returns>The player of the game.</returns>
        public static Player GetPlayer()
        {
            return player;
        }
    }
}
