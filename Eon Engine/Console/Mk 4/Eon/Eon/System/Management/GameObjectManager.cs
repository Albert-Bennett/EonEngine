/* Created: 01/06/2013
 * Last Updated: 05/04/2015
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
    public sealed class GameObjectManager : EngineModule, IUpdate
    {
        static List<GameObject> objects = new List<GameObject>();
        static List<GameObject> uninit = new List<GameObject>();
        static List<IPostInitialize> postInit = new List<IPostInitialize>();

        public int Priority
        {
            get { return 0; }
        }

        public GameObjectManager() : base("GameObjectManager") { }

        internal static void AddGameObject(GameObject gameObject)
        {
            GameObject obj = (from o in objects
                              where o.ID.Equals(gameObject.ID)
                              select o).FirstOrDefault();

            if (obj == null)
            {
                objects.Add(gameObject);

                uninit.Add(gameObject);

                if (gameObject is IPostInitialize)
                    postInit.Add((IPostInitialize)gameObject);
            }
            else
                gameObject.Destroy();
        }

        protected override void Initialize()
        {
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
                if (!uninit[i].Initialized)
                    uninit[i]._Initialize();

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
                    objects[i]._Update();
        }

        public void _PostUpdate()
        {
            for (int i = 0; i < objects.Count; i++)
                if (objects[i].Enabled)
                    objects[i]._PostUpdate();
        }

        /// <summary>
        /// Destroys the GameObjectManager.
        /// </summary>
        protected override void Destroy()
        {
            for (int i = 0; i < objects.Count; i++)
                objects[i].Destroy();

            objects.Clear();
            uninit.Clear();
            postInit.Clear();
        }

        public void TextureQualityChanged()
        {
            for (int i = 0; i < objects.Count; i++)
                objects[i]._TextureQualityChanged();
        }
    }
}
