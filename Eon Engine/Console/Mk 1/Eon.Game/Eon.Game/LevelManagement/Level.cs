/* Created 30/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Game.Errors;
using Eon.Game.Misc;
using Eon.Game.Misc.Interfaces;
using Eon.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// Defines a level in a game.
    /// </summary>
    public sealed class Level : GameObject
    {
        List<ILevelAsset> levelAssets = new List<ILevelAsset>();
        List<object> levelObjects = new List<object>();
        List<IExit> levelExits = new List<IExit>();
        List<IUpdate> updateables = new List<IUpdate>();

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

            for (int i = 0; i < info.LevelObjects.Count; i++)
            {
                object obj = AssemblyManager.CreateInstance(info.LevelObjects[i]);

                if (!(obj is PlayerStart) || !(obj is ILevelAsset) || !(obj is IExit))
                {
                    if (obj is ObjectComponent)
                        AttachComponent(obj as ObjectComponent);
                    else
                        levelObjects.Add(obj);
                }
                else
                {
                    bool added = false;

                    if (obj is ILevelAsset)
                    {
                        obj = (from a in levelAssets
                               where a.ID == (obj as ILevelAsset).ID
                               select a).FirstOrDefault();

                        if (obj == null)
                        {
                            levelAssets.Add(obj as ILevelAsset);
                            added = true;
                        }
                    }

                    if (obj is PlayerStart && added)
                    {
                        if (playerStart == null)
                        {
                            playerStart = obj as PlayerStart;
                        }
                        else
                            new DuplicateError("Their is already a PlayerStart object in this Level. The object: "
                                + (obj as PlayerStart).ID + " is invalid and will not be used.");
                    }

                    if (obj is IExit && added)
                        levelExits.Add(obj as IExit);

                    if (obj is IUpdate && added)
                        updateables.Add(obj as IUpdate);

                    if (obj is ObjectComponent)
                        AttachComponent(obj as ObjectComponent);
                }
            }

            foreach (ILevelAsset asset in levelAssets)
                asset.LevelTransitionOn(levelName);
        }

        protected override void Update()
        {
            for (int i = 0; i < updateables.Count; i++)
                updateables[i]._Update();

            base.Update();
        }

        public override void Dispose()
        {
            for (int i = 0; i < levelObjects.Count; i++)
                if (levelObjects[i] is GameObject)
                    ((GameObject)levelObjects[i]).Destroy();
                else if (levelObjects[i] is ObjectComponent)
                    ((ObjectComponent)levelObjects[i]).Destroy();

            levelObjects.Clear();

            levelExits.Clear();

            for (int i = 0; i < levelAssets.Count; i++)
                levelAssets[i].LevelTransitionOff(levelName);

            levelAssets.Clear();

            updateables.Clear();

            playerStart = null;

            base.Dispose();
        }
    }
}
