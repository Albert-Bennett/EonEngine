/* Created 23/03/2015
 * Last Updated: 17/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Game.Misc.Interfaces;
using Eon.System.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// Defines a chunk of a level.
    /// </summary>
    public sealed class LevelChunk : IID, IHoldReferences
    {
        string id;
        string contentTyperName;

        Level owner;

        List<ILevelAsset> levelAssets = new List<ILevelAsset>();
        List<IChunkExit> chunkExits = new List<IChunkExit>();
        List<GameObject> gameObjects = new List<GameObject>();
        List<object> chunkObjects = new List<object>();

        /// <summary>
        /// The ID of the LevelChunk.
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// The name of the ContentTyper used 
        /// by the LevelChunk to load content.
        /// </summary>
        public string ContentTyperName
        {
            get { return contentTyperName; }
            set { contentTyperName = value; }
        }

        /// <summary>
        /// The objects in the LevelChunk.
        /// </summary>
        public ParameterCollection[] LevelObjects;

        internal async Task LoadChunk(Level owner)
        {
            this.owner = owner;

            Common.ContentBuilder.AddContentTyper(contentTyperName, false);

            for (int i = 0; i < LevelObjects.Length; i++)
            {
                object obj = AssemblyManager.CreateInstance(LevelObjects[i]);

                if (obj != null)
                {
                    chunkObjects.Add(obj);

                    if (owner.ExcludedIDs != null)
                    {
                        if (obj is IID)
                            if (owner.ExcludedIDs.Contains(((IID)obj).ID))
                                Remove(obj);
                            else
                                await CheckObject(obj);
                        else
                            await CheckObject(obj);
                    }
                }
            }
        }

        async Task CheckObject(object obj)
        {
            if (obj is ILevelAsset)
            {
                if (levelAssets.Count > 0)
                {
                    obj = (from a in levelAssets
                           where a.ID == (obj as ILevelAsset).ID
                           select a).FirstOrDefault();

                    if (obj == null)
                    {
                        (obj as ILevelAsset).LevelTransitionOn(id);
                        levelAssets.Add(obj as ILevelAsset);
                    }
                }
                else
                {
                    (obj as ILevelAsset).LevelTransitionOn(id);
                    levelAssets.Add(obj as ILevelAsset);
                }
            }

            if (obj is IChunkExit)
            {
                ((IChunkExit)obj).OnLoadRequested +=
                    new NextChunkLoadEvent(LoadNextChunk);

                chunkExits.Add(obj as IChunkExit);
            }

            if (obj is GameObject)
            {
                ((GameObject)obj).AddReference(this);
                gameObjects.Add(obj as GameObject);
            }
        }

        void LoadNextChunk(string id)
        {
            owner.LoadChunk(id);
        }

        internal GameObject GetGameObject(string id)
        {
            GameObject gob = (from g in gameObjects
                              where g.ID == id
                              select g).FirstOrDefault();

            return gob;
        }

        internal object SendMessage(string objectID, string message, params object[] args)
        {
            bool found = false;
            int idx = 0;

            while (idx < gameObjects.Count && !found)
            {
                if (gameObjects[idx].ID == objectID)
                {
                    if (gameObjects[idx].GetType().GetMethod(message) != null)
                        return gameObjects[idx].GetType().GetMethod(message).Invoke(gameObjects[idx], args);
                    else if (gameObjects[idx].GetType().GetProperty(message) != null)
                        return gameObjects[idx].GetType().GetProperty(message).GetValue(gameObjects[idx]);
                }

                idx++;
            }

            return null;
        }

        public void Remove(object obj)
        {
            if (obj is GameObject)
                gameObjects.Remove(obj as GameObject);

            if (obj is ILevelAsset)
                levelAssets.Remove(obj as ILevelAsset);

            if (obj is IChunkExit)
                chunkExits.Remove(obj as IChunkExit);

            chunkObjects.Remove(obj);
        }

        internal void Destroy()
        {
            if (gameObjects.Count > 0)
            {
                int count = gameObjects.Count;

                while (count > 0)
                {
                    gameObjects[0].Destroy();

                    count--;
                }

                gameObjects.Clear();
            }

            chunkExits.Clear();

            for (int i = 0; i < levelAssets.Count; i++)
                if (levelAssets[i] != null)
                    levelAssets[i].LevelTransitionOff(id);

            levelAssets.Clear();

            Common.ContentBuilder.Unload(contentTyperName);
        }
    }
}
