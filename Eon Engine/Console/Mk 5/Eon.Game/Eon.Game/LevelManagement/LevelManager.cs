/* Created 03/11/2013
 * Last Updated: 01/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.System.Management;
using Eon.Testing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// An overridable engine component that 
    /// is used to manage Levels in a game.
    /// </summary>
    public sealed class LevelManager : EngineModule
    {
        const string levelManagerInfoFilepath = "LevelManager.Levels";

        static Dictionary<string, string> levelFilepaths =
             new Dictionary<string, string>();

        static Level currentLevel = null;
        static LevelManagerInfo info;

        static string prevLevel = "";
        static string levelName = "";

        static Player player;

        public static StartLoadingEvent OnStartLoading;
        public static EndLoadingEvent OnEndLoading;

        /// <summary>
        /// The name of the currently active Level.
        /// </summary>
        public static string CurrentLevel
        {
            get
            {
                if (currentLevel == null)
                    return "";
                else
                    return currentLevel.Name;
            }
        }

        /// <summary>
        /// Creates a new LevelManager.
        /// </summary>
        public LevelManager() : base("LevelManager") { }

        protected override void Initialize()
        {
            try
            {
                info = SerializationHelper.Deserialize<LevelManagerInfo>(levelManagerInfoFilepath, true, "");

                for (int i = 0; i < info.LevelNames.Length; i++)
                    levelFilepaths.Add(info.LevelNames[i], info.LevelFilepaths[i]);
            }
            catch
            {
                new Error("Unable to create LevelManager, LevelManager.Levels doesn't exist", Seriousness.CriticalError);
            }


            base.Initialize();
        }

        /// <summary>
        /// Loads a Level. (Primarally accessed by Messages).
        /// </summary>
        /// <param name="levelName">The name of the level to be loaded.</param>
        /// <param name="inform">Should the Game be informed about the loading operations.</param>
        /// <param name="excludedIDs">The various object ID's that are not to be loaded.</param>
        public static async Task LoadLevel(string levelName, bool inform, params string[] excludedIDs)
        {
            if (levelFilepaths.ContainsKey(levelName))
                if (currentLevel != null)
                {
                    if (currentLevel.Name != levelName)
                    {
                        Task.WaitAny(DisposePrevious());

                        await _LoadLevel(levelName, inform, excludedIDs);
                    }
                }
                else
                    await _LoadLevel(levelName, inform, excludedIDs);
        }

        async static Task _LoadLevel(string levelName, bool inform, params string[] excludedIDs)
        {
            LevelManager.levelName = levelName;

            if (OnStartLoading != null && inform)
                OnStartLoading();

            if (currentLevel != null)
                prevLevel = currentLevel.Name;

            await _LoadLevel(inform, excludedIDs);
        }

        static async Task _LoadLevel(bool inform, params string[] excludedIDs)
        {
            await LoadCurrent(excludedIDs);

            if (inform)
                await Task.Delay(4000).ConfigureAwait(true);

            if (OnEndLoading != null)
            {
                OnEndLoading();
                OnEndLoading = null;
            }

            currentLevel.TransitOn();
        }

        static async Task LoadCurrent(params string[] excludedIDs)
        {
            Type[] extraTypes = new Type[]
            {
                typeof(ParameterCollection),
                typeof(EonDictionary<string, LevelChunk>)
            };

            LevelInfo levelInfo = SerializationHelper.Deserialize<LevelInfo>(
                levelFilepaths[levelName], true, ".Lvl", extraTypes);

            //Problematic
            currentLevel = new Level();
            await currentLevel.Initate(levelInfo, excludedIDs);

            if (info.PlayerObject != null)
            {
                player = (Player)AssemblyManager.CreateInstance(info.PlayerObject);
                player.LevelTransitionOn(levelName);
            }

            //Check list
            //1. Can load the default starting position.
            //2. Can load the LevelChunk where the player had left off.

            //Needs updating when loading larger levels.
        }

        static async Task DisposePrevious()
        {
            if (currentLevel != null)
            {
                currentLevel.TransitOff();
                currentLevel.Dispose();
                currentLevel = null;

                AudioManager.DestroyListner();

                if (player != null)
                {
                    player.LevelTransitionOff(prevLevel);
                    player.Destroy();
                }
            }

            await Task.Delay(500).ConfigureAwait(true);
        }

        /// <summary>
        /// Sends a message to the current Level.
        /// </summary>
        /// <param name="objectID">The ID of the object to get the message.</param>
        /// <param name="message">The message.</param>
        /// <returns>Any return object.</returns>
        public static object SendMessage(string objectID, string message, params object[] args)
        {
            if (objectID.Equals(string.Empty))
            {
                EngineModule module = EngineModuleManager.Find("LevelManager");

                if (module != null)
                {
                    MethodInfo info = module.GetType().GetMethod(message);

                    if (info != null)
                        return info.Invoke(module, args);
                }
            }
            else if (currentLevel != null)
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
            if (message.TargetID.Equals(string.Empty))
            {
                EngineModule module = EngineModuleManager.Find("LevelManager");

                if (module != null)
                {
                    MethodInfo info = module.GetType().GetMethod(message.MethodName);

                    if (info != null)
                        return info.Invoke(module, message.Parameters);
                }
            }
            else if (currentLevel != null)
                return currentLevel.SendMessage(message);

            return null;
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
