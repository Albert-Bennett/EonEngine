/* Created 30/10/2013
 * Last Updated: 01/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Game.Assets;
using Eon.Testing;
using System.Threading.Tasks;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// Defines a level in a game.
    /// </summary>
    public sealed class Level
    {
        LevelChunk[] activeChunks = new LevelChunk[2];

        string currentID = "";
        string name;

        string[] excludedIDs;

        /// <summary>
        /// Where the player starts the level.
        /// </summary>
        PlayerStart playerStart;

        /// <summary>
        /// The various parts thatt make up the Level.
        /// </summary>
        EonDictionary<string, LevelChunk> levelChunks;

        internal string[] ExcludedIDs
        {
            get { return excludedIDs; }
        }

        /// <summary>
        /// The name of the Level.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Initializes the Level.
        /// </summary>
        internal async Task Initate(LevelInfo info, params string[] excludedIDs)
        {
            this.name = info.Name;
            this.excludedIDs = excludedIDs;

            levelChunks = info.LevelChunks;

            //Problematic
            await LoadChunk(levelChunks[0].Value.ID);

            //Check List
            //1. The LevelChunk where the PlayerStart is (if it exists).
            //2. The player's current LevelChunk.
        }

        internal async Task LoadChunk(string id)
        {
            if (currentID != id)
                if (levelChunks.Contains(id))
                {
                    if (activeChunks[0] != null)
                    {
                        if (activeChunks[0].ID == id)
                        {
                            //LevelManager.GetPlayer().CurrentLevelChunk = activeChunks[0].ID;

                            LevelChunk c = activeChunks[1];

                            activeChunks[1] = activeChunks[0];
                            activeChunks[0] = c;
                        }
                    }
                    else
                    {
                        if (activeChunks[1] != null)
                        {
                            if (activeChunks[0] != null)
                                activeChunks[0].Destroy();

                            activeChunks[0] = activeChunks[1];
                        }

                        LevelChunk chunk = levelChunks[id];
                        await chunk.LoadChunk(this);

                        activeChunks[1] = chunk;
                        currentID = chunk.ID;

                        //LevelManager.GetPlayer().CurrentLevelChunk = chunk.ID;
                    }
                }
                else
                    new Error("Level Chunk: " + id +
                        ", dosn't exist.", Seriousness.Error);
        }

        internal void TransitOn()
        {
            activeChunks[1].TransitOn();
        }

        internal void TransitOff()
        {
            activeChunks[1].TransitOff();
        }

        internal object SendMessage(Message message)
        {
            return SendMessage(message.TargetID, message.MethodName, message.Parameters);
        }

        internal object SendMessage(string objectID, string message, params object[] args)
        {
            foreach (LevelChunk c in activeChunks)
                return c.SendMessage(objectID, message, args);

            object obj = null;

            int i = 0;
            bool found = false;

            while (!found && i < activeChunks.Length)
            {
                obj = activeChunks[i].SendMessage(objectID, message, args);

                if (obj != null)
                    found = true;
                else
                    i++;
            }

            return obj;
        }

        /// <summary>
        /// Finds a GameObject with the given ID.
        /// </summary>
        /// <param name="id">The ID of the game object to get.</param>
        /// <returns>The result of the check.</returns>
        public GameObject GetGameObject(string id)
        {
            bool found = false;
            int i = 0;

            GameObject obj = null;

            while (!found && i < activeChunks.Length)
            {
                if (activeChunks[i] != null)
                {
                    obj = activeChunks[i].GetGameObject(id);

                    if (obj == null)
                        i++;
                    else
                        found = true;
                }
                else
                    i++;
            }

            return obj;
        }

        public void Dispose()
        {
            for (int i = 0; i < activeChunks.Length; i++)
                if (activeChunks[i] != null)
                    activeChunks[i].Destroy();

            playerStart = null;
        }

        public void Remove(object obj)
        {
            if (obj is PlayerStart)
                playerStart.Destroy();
        }
    }
}
