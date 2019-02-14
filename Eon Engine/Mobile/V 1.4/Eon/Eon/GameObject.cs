/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.EngineComponents;
using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eon
{
    /// <summary>
    /// Defines a GameObject. 
    /// </summary>
    public class GameObject : IID
    {
        string id;
        bool enabled = true;
        bool initialized = false;

        List<ObjectComponent> comps = new List<ObjectComponent>();
        List<ObjectComponent> unInit = new List<ObjectComponent>();

        List<IUpdate> updateComps = new List<IUpdate>();
        List<IPostUpdate> postUpdateComps = new List<IPostUpdate>();
        List<IDispose> disposableComps = new List<IDispose>();
        List<IHoldReferences> objRefferences = new List<IHoldReferences>();

        Matrix world;

        internal bool _Initialized
        {
            get { return initialized; }
        }

        /// <summary>
        /// A matrix defining the world of this GameObject.
        /// </summary>
        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        /// <summary>
        /// The unique identification 
        /// name given to this GameObject.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Wheather or not this GameObject is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
        }

        /// <summary>
        /// Creates a new GameObject.
        /// </summary>
        /// <param name="id">The unique identification name 
        /// to give to this GameObject.</param>
        public GameObject(string id)
        {
            this.id = id;
            world = Matrix.Identity;

            GameObjectManager.AddGameObject(this);
        }

        /// <summary>
        /// Attaches an ObjectComponent to this GameObject.
        /// </summary>
        /// <param name="component">The ObjectComponent to be attached.</param>
        public void AttachComponent(ObjectComponent component)
        {
            ObjectComponent comp = null;

            comp = (from c in comps
                    where c.ID == component.ID
                    select c).FirstOrDefault();

            if (comp == null)
            {
                component.Owner = this;

                if (component is IUpdate)
                    updateComps.Add((IUpdate)component);

                if (component is IPostUpdate)
                    postUpdateComps.Add((IPostUpdate)component);

                if (component is IDispose)
                    disposableComps.Add((IDispose)component);

                AttachComponentAction(component);

                comps.Add(component);
                unInit.Add(component);
            }
        }

        /// <summary>
        /// Used to further define what happends
        /// when an ObjectComponent is attached to this GameObject.
        /// </summary>
        /// <param name="component">The ObjectComponent to be attached to this GameObject.</param>
        protected virtual void AttachComponentAction(ObjectComponent component) { }

        /// <summary>
        /// Used to re-initialize this GameObject.
        /// Ergo, to initialize an already disposed GameObject.  
        /// </summary>
        public void ReInitialize()
        {
            Initialize();
            InitializeComponents();
        }

        void InitializeComponents()
        {
            if (unInit.Count > 0)
            {
                for (int i = 0; i < unInit.Count; i++)
                    unInit[i]._Initialize();

                unInit.Clear();
            }
        }

        /// <summary>
        /// Used to initialize this GameObject.
        /// </summary>
        protected virtual void Initialize()
        {
            initialized = true;
        }

        internal void _Update()
        {
            if (initialized)
            {
                if (unInit.Count > 0)
                    InitializeComponents();

                Update();
                UpdateComponents();

                PostUpdate();
                PostUpdateComponents();
            }
        }

        void UpdateComponents()
        {
            int priority = 0;
            int count = updateComps.Count;

            while (count > 0)
            {
                for (int i = 0; i < updateComps.Count; i++)
                    if (updateComps[i].Priority == priority &&
                        ((ObjectComponent)updateComps[i]).Enabled)
                    {
                        updateComps[i]._Update();
                        count--;
                    }

                priority++;
            }
        }

        void PostUpdateComponents()
        {
            for (int i = 0; i < postUpdateComps.Count; i++)
                if (((ObjectComponent)postUpdateComps[i]).Enabled)
                    postUpdateComps[i]._PostUpdate();
        }

        /// <summary>
        /// Used to preform post update actions.
        /// </summary>
        protected virtual void PostUpdate() { }

        /// <summary>
        /// Used to update this GameObject.
        /// </summary>
        protected virtual void Update() { }

        /// <summary>
        /// Adds a refernce object to this object.
        /// </summary>
        /// <param name="reference">The referencing object.</param>
        public void AddReference(IHoldReferences reference)
        {
            if (!objRefferences.Contains(reference))
                objRefferences.Add(reference);
        }

        /// <summary>
        /// Disposes of any resources held by this 
        /// GameObject and it's ObjectComponents.
        /// </summary>
        public virtual void Dispose()
        {
            for (int i = 0; i < disposableComps.Count; i++)
                disposableComps[i].Dispose(true);

            for (int i = 0; i < comps.Count; i++)
                comps[i].Destroy(false);

            comps.Clear();

            for (int i = 0; i < objRefferences.Count; i++)
                objRefferences[i].Remove(this);
        }

        /// <summary>
        /// Finds a ObjectComponent matching the given id.
        /// </summary>
        /// <param name="id">The id of the ObjectComponent to find.</param>
        /// <returns>The result of the search.</returns>
        public ObjectComponent FindComponent(string id)
        {
            ObjectComponent comp = null;

            comp = (from c in comps
                    where c.ID == id
                    select c).FirstOrDefault();

            return comp;
        }

        /// <summary>
        /// Finds a ObjectComponent matching the given type.
        /// </summary>
        /// <param name="type">The type of the ObjectComponent to find</param>
        /// <returns>The result of the search.</returns>
        public ObjectComponent FindComponent(Type type)
        {
            ObjectComponent comp = null;

            comp = (from c in comps
                    where c.GetType() == type
                    select c).FirstOrDefault();

            return comp;
        }

        /// <summary>
        /// Removes an ObjectComponent with the given id.
        /// </summary>
        /// <param name="type">The id of the ObjectComponent to be remove.</param>
        public void RemoveComponent(string id)
        {
            ObjectComponent comp = null;

            comp = (from c in comps
                    where c.ID == id
                    select c).FirstOrDefault();

            if (comp != null)
                RemoveComponent(comp);
        }

        /// <summary>
        /// Removes an ObjectComponent from this.
        /// </summary>
        /// <param name="component">The ObjectComponent to be removed.</param>
        public virtual void RemoveComponent(ObjectComponent component)
        {
            if (component != null && comps.Contains(component))
            {
                if (component is IUpdate)
                    updateComps.Remove(component as IUpdate);

                if (component is IPostUpdate)
                    postUpdateComps.Remove(component as IPostUpdate);

                if (component is IDispose)
                {
                    ((IDispose)component).Dispose(true);
                    disposableComps.Remove(component as IDispose);
                }

                if (unInit.Contains(component))
                    unInit.Remove(component);

                comps.Remove(component);
            }
        }

        /// <summary>
        /// Destroys this GameObject.
        /// </summary>
        public virtual void Destroy()
        {
            for (int i = 0; i < comps.Count; i++)
                comps[i].Destroy(false);

            comps.Clear();

            updateComps.Clear();
            unInit.Clear();
            disposableComps.Clear();
            postUpdateComps.Clear();

            for (int i = 0; i < objRefferences.Count; i++)
                objRefferences[i].Remove(this);

            objRefferences.Clear();

            GameObjectManager.DestroyGameObject(this);
        }

        /// <summary>
        /// Used to send a message to Components.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="target">The target Component for the message (if any).</param>
        public object SendMessage(string message, string target, params object[] parameters)
        {
            MethodInfo info = GetType().GetMethod(message);

            if (info != null)
                info.Invoke(this, parameters);
            else
            {
                if (target == null)
                {
                    var cs = (from c in comps
                              where c.GetType().GetMethod(message) != null
                              select c).ToList();

                    object obj = null;

                    foreach (var comp in cs)
                        obj = comp.GetType().GetMethod(message).Invoke(comp, parameters);

                    return obj;
                }
                else
                {
                    ObjectComponent comp = FindComponent(target);

                    if (comp == null)
                        comp = FindComponent(AssemblyManager.GetType(target));

                    object obj = null;

                    if (comp != null)
                        if (comp.GetType().GetMethod(message) != null)
                            obj = comp.GetType().GetMethod(message).Invoke(comp, parameters);

                    return obj;
                }
            }

            return null;
        }

        internal void _ScreenResolutionChanged()
        {
            Vector3 trans = world.Translation;

            if (trans.Z == 0)
            {
                Vector3 scale = Vector3.One;

                Eon.Helpers.EonMathHelper.GetMatrixScale(world, out scale);

                world -= Matrix.CreateScale(scale) * Matrix.CreateTranslation(trans);

                Vector2 pos = new Vector2(trans.X, trans.Y);
                pos = Common.ReCalibrateScreenSpaceVector(pos);

                Vector2 scl = new Vector2(scale.X, scale.Y);
                scl = Common.ReCalibrateScreenSpaceVector(scl);

                world += Matrix.CreateScale(scl.X, scl.Y, 0) *
                    Matrix.CreateTranslation(pos.X, pos.Y, 0);
            }

            SendMessage("ScreenResolutionChanged", null, null);
        }

        /// <summary>
        /// Used to disable or enable this GameObject.
        /// </summary>
        public void ToogleEnable()
        {
            if (!enabled)
                Enable();
            else
                Disable();
        }

        /// <summary>
        /// Disables this GameObject.
        /// </summary>
        public virtual void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// Enables this GameObject.
        /// </summary>
        public virtual void Enable()
        {
            enabled = true;
        }

        /// <summary>
        /// Finds base class for this ObjectComponent.
        /// </summary>
        /// <returns>The base class for this ObjectComponent.</returns>
        public Type BaseType()
        {
            return GetType().BaseType;
        }
    }
}
