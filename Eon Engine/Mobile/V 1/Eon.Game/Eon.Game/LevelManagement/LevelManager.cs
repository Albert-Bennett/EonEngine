/* Created 03/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.EngineComponents;
using Eon.Helpers;
using System.Collections.Generic;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// An overridable engine component that 
    /// is used to manage Levels in a game.
    /// </summary>
    public class LevelManager : EngineComponent
    {
        const string levelManagerInfoFilepath = "LevelManager";

        Dictionary<string, string> levelFilepaths =
            new Dictionary<string, string>();

        Level currentLevel = null;
        LevelManagerInfo info;

        string prevLevel;

        static Player player;

        public StartLoadingEvent OnStartLoading;
        public EndLoadingEvent OnEndLoading;

        /// <summary>
        /// Creates a new LevelManager.
        /// </summary>
        public LevelManager() : base("LevelManager") { }

        protected override void Initialize()
        {
            info = Common.ContentManager.Load<LevelManagerInfo>(levelManagerInfoFilepath);

            for (int i = 0; i < info.LevelName.Length; i++)
                levelFilepaths.Add(info.LevelName[i], info.LevelFilepaths[i]);

            base.Initialize();
        }

        /// <summary>
        /// Loads a Level.
        /// </summary>
        /// <param name="levelName">The name of the level to be loaded.</param>
        public void LoadLevel(string levelName)
        {
            if (OnStartLoading != null)
                OnStartLoading();

            GameStateManager.ChangeGameState(EngineComponents.GameStates.Game);

            if (currentLevel != null)
            {
                currentLevel.Destroy();
                currentLevel = null;

                if (player != null)
                    player.LevelTransitionOff(prevLevel);

                currentLevel = new Level(levelName);
            }
            else
                FirstLoading(levelName);

            LoadingLevel(levelName);

            currentLevel.Initate(levelFilepaths[levelName]);

            if (player != null)
                player.LevelTransitionOn(levelName);

            prevLevel = levelName;

            if (OnEndLoading != null)
                OnEndLoading();
        }

        void FirstLoading(string levelName)
        {
            currentLevel = new Level(levelName);

            if (info.PlayerObject != null)
                player = AssemblyManager.CreateInstance(info.PlayerObject) as Player;
        }

        /// <summary>
        /// Used to define what happends whene a Level is being loaded.
        /// </summary>
        /// <param name="nextLevelName">The name of the next Level.</param>
        protected virtual void LoadingLevel(string nextLevelName) { }

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
