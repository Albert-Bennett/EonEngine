/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Eon.System.Management
{
    /// <summary>
    /// Defines an EngineComponent that is
    /// used to manage GameObjects.
    /// </summary>
    public sealed class GameObjectManager : EngineComponent, IUpdate, IDispose
    {
        static List<GameObject> objects = new List<GameObject>();
        static List<GameObject> uninit = new List<GameObject>();
        static List<IPostInitialize> postInit = new List<IPostInitialize>();
        static List<IPostUpdate> postUpdate = new List<IPostUpdate>();

        public int Priority
        {
            get { return 0; }
        }

        public GameObjectManager() : base("GameObjectManager") { }

        internal static void AddGameObject(GameObject gameObject)
        {
            GameObject obj = (from o in objects
                              where o.ID == gameObject.ID
                              select o).FirstOrDefault();

            if (obj == null)
            {
                objects.Add(gameObject);

                uninit.Add(gameObject);

                if (gameObject is IPostInitialize)
                    postInit.Add((IPostInitialize)gameObject);

                if (gameObject is IPostUpdate)
                    postUpdate.Add((IPostUpdate)gameObject);
            }
        }

        protected override void Initialize()
        {
            initialized = true;

            InitializeObjects();

            base.Initialize();
        }

        internal static void DestroyGameObject(GameObject obj)
        {
            if (obj != null)
            {
                if (obj is IPostUpdate)
                    postUpdate.Remove(obj as IPostUpdate);

                objects.Remove(obj);
            }
        }

        /// <summary>
        /// Finds a specific GameObject that matches the given id.
        /// </summary>
        /// <param name="id">The id of an GameObject.</param>
        /// <returns>The result of the search.</returns>
        public static GameObject FindGameObject(string id)
        {
            GameObject obj = (from a in objects
                              where a.ID == id
                              select a).FirstOrDefault();

            return obj;
        }

        void InitializeObjects()
        {
            for (int i = 0; i < uninit.Count; i++)
                if (!uninit[i]._Initialized)
                    uninit[i].Initialize();

            uninit.Clear();
        }

        void PostInitialize()
        {
            for (int i = 0; i < postInit.Count; i++)
                postInit[i].PostInitialize();

            postInit.Clear();
        }

        public void _Update()
        {
            if (uninit.Count > 0)
                InitializeObjects();

            if (postInit.Count > 0)
                PostInitialize();

            for (int i = 0; i < objects.Count; i++)
                if (objects[i].Enabled)
                    objects[i]._Update();
        }

        public void _PostUpdate()
        {
            for (int i = 0; i < postUpdate.Count; i++)
                postUpdate[i]._PostUpdate();
        }

        /// <summary>
        /// Disposes of the GameObjectManager.
        /// </summary>
        /// <param name="finalize">Finalize the deposition.</param>
        public void Dispose(bool finalize)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Dispose();
                objects[i].Destroy();
            }

            objects.Clear();
            uninit.Clear();
            postUpdate.Clear();
            postInit.Clear();
        }
    }
}
