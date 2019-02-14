/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Eon.EngineComponents
{
    /// <summary>
    /// Defines an EngineComponent that is
    /// used to manage GameObjects.
    /// </summary>
    public sealed class GameObjectManager : EngineComponent, IUpdate, IDispose, IPostUpdate
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

        internal static void AddGameObject(GameObject actor)
        {
            GameObject act = (from a in objects
                              where a.ID == actor.ID
                              select a).FirstOrDefault();

            if (act == null)
            {
                objects.Add(actor);

                uninit.Add(actor);

                if (actor is IPostInitialize)
                    postInit.Add((IPostInitialize)actor);

                if (actor is IPostUpdate)
                    postUpdate.Add((IPostUpdate)actor);
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
                objects.Remove(obj);
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
                    uninit[i].ReInitialize();

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

        /// <summary>
        /// Used to indicte the change in screen 
        /// resoloution to all GameObjects being managed.
        /// </summary>
        public void ScreenResolutionChanged()
        {
            for (int i = 0; i < objects.Count; i++)
                objects[i]._ScreenResolutionChanged();
        }
    }
}
