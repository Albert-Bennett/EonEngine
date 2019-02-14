/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
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
    public sealed class GameObjectManager : EngineComponent, IUpdate, IDispose
    {
        static List<GameObject> objects = new List<GameObject>();
        static List<GameObject> uninit = new List<GameObject>();

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

        public void _Update()
        {
            InitializeObjects();

            for (int i = 0; i < objects.Count; i++)
                if (objects[i].Enabled)
                    objects[i]._Update();
        }

        void InitializeObjects()
        {
            if (uninit.Count > 0)
            {
                foreach (GameObject obj in uninit)
                    if (!obj._Initialized)
                        obj.ReInitialize();

                uninit.Clear();
            }
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
