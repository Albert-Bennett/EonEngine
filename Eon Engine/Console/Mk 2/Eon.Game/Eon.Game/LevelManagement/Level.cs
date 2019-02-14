/* Created 30/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game.Misc;
using Eon.Game.Misc.Interfaces;
using Eon.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// Defines a level in a game.
    /// </summary>
    public sealed class Level : GameObject, IHoldReferences
    {
        List<ILevelAsset> levelAssets = new List<ILevelAsset>();
        List<IExit> levelExits = new List<IExit>();
        List<IUpdate> updateables = new List<IUpdate>();
        List<GameObject> gameObjects = new List<GameObject>();

        string levelName;

        PlayerStart playerStart = null;

        /// <summary>
        /// The name of the Level.
        /// </summary>
        public string LevelName
        {
            get { return levelName; }
        }

        /// <summary>
        /// Creates a new Level.
        /// </summary>
        /// <param name="id">The name of the Level.</param>
        public Level(string id) : base(id) { }

        /// <summary>
        /// Initializes the Level.
        /// </summary>
        /// <param name="levelInfoFilepath">The filepath for the 
        /// file where this Level is going to get it's information from.</param>
        internal void Initate(string levelInfoFilepath)
        {
            LevelInfo info = Common.ContentManager.Load<LevelInfo>(levelInfoFilepath);

            levelName = info.LevelName;

            for (int i = 0; i < info.LevelObjects.Length; i++)
            {
                object obj = AssemblyManager.CreateInstance(info.LevelObjects[i]);

                if (obj is ILevelAsset)
                {
                    if (levelAssets.Count > 0)
                    {
                        obj = (from a in levelAssets
                               where a.ID == (obj as ILevelAsset).ID
                               select a).FirstOrDefault();

                        if (obj == null)
                            levelAssets.Add(obj as ILevelAsset);
                    }
                    else
                        levelAssets.Add(obj as ILevelAsset);
                }

                if (obj is PlayerStart)
                {
                    if (playerStart == null)
                    {
                        playerStart = obj as PlayerStart;
                    }
                    else
                        throw new ArgumentException("Their is already a PlayerStart object in this Level. The object: "
                             + (obj as PlayerStart).ID + " is invalid and will not be used.");
                }

                if (obj is IExit)
                    levelExits.Add(obj as IExit);

                if (obj is IUpdate)
                    updateables.Add(obj as IUpdate);

                if (obj is GameObject)
                {
                    ((GameObject)obj).AddReference(this);
                    gameObjects.Add(obj as GameObject);
                }
            }

            foreach (ILevelAsset asset in levelAssets)
                if (asset != null)
                    asset.LevelTransitionOn(levelName);
        }

        /// <summary>
        /// Finds a GameObject with the given ID.
        /// </summary>
        /// <param name="id">The ID of the game object to get.</param>
        /// <returns>The result of the check.</returns>
        public GameObject GetGameObject(string id)
        {
            GameObject gob = (from g in gameObjects
                              where g.ID == id
                              select g).FirstOrDefault();

            return gob;
        }

        protected override void Update()
        {
            for (int i = 0; i < updateables.Count; i++)
                updateables[i]._Update();

            base.Update();
        }

        public override void Dispose()
        {
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Destroy();

            gameObjects.Clear();

            levelExits.Clear();

            for (int i = 0; i < levelAssets.Count; i++)
                if (levelAssets[i] != null)
                    levelAssets[i].LevelTransitionOff(levelName);

            levelAssets.Clear();

            updateables.Clear();

            playerStart = null;

            base.Dispose();
        }

        public void Remove(object obj)
        {
            if (obj is PlayerStart)
                playerStart.Destroy();

            if (obj is GameObject)
                gameObjects.Remove(obj as GameObject);

            if (obj is ILevelAsset)
                levelAssets.Remove(obj as ILevelAsset);

            if (obj is IExit)
                levelExits.Remove(obj as IExit);

            if (obj is IUpdate)
                updateables.Remove(obj as IUpdate);
        }
    }
}
