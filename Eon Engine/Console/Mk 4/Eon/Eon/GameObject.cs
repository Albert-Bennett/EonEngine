/* Created: 01/06/2013
 * Last Updated: 04/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths;
using Eon.System.Interfaces;
using Eon.System.Management;
using Eon.System.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eon
{
    /// <summary>
    /// Defines a GameObject. 
    /// </summary>
    public class GameObject : IID, IEnabled
    {
        List<ObjectComponent> comps = new List<ObjectComponent>();
        List<ObjectComponent> unInit = new List<ObjectComponent>();

        List<IUpdate> updateComps = new List<IUpdate>();
        List<IHoldReferences> objRefferences = new List<IHoldReferences>();

        Transformation world;

        string id;
        bool enabled = true;
        bool forceEnable = true;
        bool initialized = false;

        GameStates presidence = GameStates.Game;

        public bool Initialized
        {
            get { return initialized; }
        }

        /// <summary>
        /// A matrix defining the world of this GameObject.
        /// </summary>
        public Transformation World
        {
            get { return world; }
            set { world = value; }
        }

        /// <summary>
        /// What state the game needs to be in
        /// for the GameObject to Update.
        /// </summary>
        public GameStates Presidence
        {
            get { return presidence; }
            set { presidence = value; }
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
        /// Whether or not this GameObject is enabled.
        /// </summary>
        public bool Enabled { get { return enabled; } }

        /// <summary>
        /// Creates a new GameObject.
        /// </summary>
        /// <param name="id">The unique identification name 
        /// to give to this GameObject.</param>
        public GameObject(string id)
        {
            this.id = id;
            world = Transformation.Identity;
            GameObjectManager.AddGameObject(this);
            enabled = true;
        }

        /// <summary>
        /// Attaches an ObjectComponent to this GameObject.
        /// </summary>
        /// <param name="component">The ObjectComponent to be attached.</param>
        public void AttachComponent(ObjectComponent component)
        {
            try
            {
                ObjectComponent comp = null;

                comp = (from c in comps
                        where c.ID == component.ID
                        select c).FirstOrDefault();

                if (comp == null)
                {
                    component.Owner = this;

                    if (component is IUpdate)
                    {
                        updateComps.Add((IUpdate)component);

                        updateComps = updateComps.OrderBy(u => u.Priority).ToList();
                    }

                    AttachComponentAction(component);

                    comps.Add(component);
                    unInit.Add(component);
                }
                else
                    comp.Destroy(true);
            }
            catch (NullReferenceException)
            {
                throw new ArgumentException("Attempted to attach a null ObjectComponent to: " + id);
            }
        }

        /// <summary>
        /// Used to further define what happends
        /// when an ObjectComponent is attached to the GameObject.
        /// </summary>
        /// <param name="component">The ObjectComponent to be attached to this GameObject.</param>
        protected virtual void AttachComponentAction(ObjectComponent component) { }

        void InitializeComponents()
        {
            if (unInit.Count > 0)
            {
                for (int i = 0; i < unInit.Count; i++)
                    unInit[i]._Initialize();

                unInit.Clear();
            }
        }

        internal void _Initialize()
        {
            Initialize();
        }

        /// <summary>
        /// Used to initialize this GameObject.
        /// </summary>
        protected virtual void Initialize()
        {
            initialized = true;

            InitializeComponents();
        }

        internal void _Update()
        {
            if (initialized)
            {
                if (unInit.Count > 0)
                    InitializeComponents();

                if (forceEnable)
                    if (GameStateManager.CurrentState == presidence ||
                        presidence == GameStates.None)
                    {
                        if (!enabled)
                            Enable();
                    }
                    else
                        if (enabled)
                        {
                            bool fe = forceEnable;

                            Disable();

                            forceEnable = fe;
                        }

                if (enabled)
                    Update();
            }
        }

        /// <summary>
        /// Used to update this GameObject.
        /// </summary>
        protected virtual void Update()
        {
            for (int i = 0; i < updateComps.Count; i++)
                if (((IEnabled)updateComps[i]).Enabled)
                    updateComps[i]._Update();
        }

        internal void _PostUpdate()
        {
            if (initialized && enabled)
                PostUpdate();
        }

        /// <summary>
        /// Used to preform post update actions.
        /// </summary>
        protected virtual void PostUpdate()
        {
            for (int i = 0; i < updateComps.Count; i++)
                if (((IEnabled)updateComps[i]).Enabled)
                    updateComps[i]._PostUpdate();
        }

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
                if (!comps[i].Destroyed)
                    comps[i]._Destroy(false);

            comps.Clear();

            updateComps.Clear();
            unInit.Clear();

            for (int i = 0; i < objRefferences.Count; i++)
                objRefferences[i].Remove(this);

            objRefferences.Clear();

            GameObjectManager.DestroyGameObject(this);
        }

        /// <summary>
        /// Sends a message to the GameObject's components.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>The result of the methods execution or null if none was executed.</returns>
        public object SendMessage(Message message)
        {
            if (message.MethodName != "")
                if (message.TargetID == ID)
                {
                    MethodInfo info = GetType().GetMethod(message.MethodName);

                    if (info != null)
                        return info.Invoke(this, message.Parameters);
                }
                else
                {
                    if (message.TargetID == "")
                    {
                        var cs = (from c in comps
                                  where c.GetType().GetMethod(message.MethodName) != null
                                  select c).ToList();

                        foreach (var comp in cs)
                            comp.GetType().GetMethod(message.MethodName).Invoke(comp, message.Parameters);
                    }
                    else
                    {
                        ObjectComponent comp = FindComponent(message.TargetID);

                        object obj = null;

                        if (comp != null)
                            if (comp.GetType().GetMethod(message.MethodName) != null)
                                obj = comp.GetType().GetMethod(message.MethodName).Invoke(comp, message.Parameters);

                        return obj;
                    }
                }

            return null;
        }

        /// <summary>
        /// Used to send a message to Components.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="target">The target Component for the message (if any).</param>
        /// <param name="parameters">The parameters of the method to be executed.</param>
        /// <returns>The result of the methods execution or null if none was executed.</returns>
        public object SendMessage(string message, string target, params object[] parameters)
        {
            if (message != "")
            {
                MethodInfo info = GetType().GetMethod(message);

                if (info != null)
                    return info.Invoke(this, parameters);
                else
                {
                    if (target == null)
                    {
                        var cs = (from c in comps
                                  where c.GetType().GetMethod(message) != null
                                  select c).ToList();

                        foreach (var comp in cs)
                            comp.GetType().GetMethod(message).Invoke(comp, parameters);
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
            }

            return null;
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
            forceEnable = false;

            for (int i = 0; i < comps.Count; i++)
                comps[i].Disable();
        }

        /// <summary>
        /// Enables this GameObject.
        /// </summary>
        public virtual void Enable()
        {
            enabled = true;
            forceEnable = true;

            for (int i = 0; i < comps.Count; i++)
                comps[i].Enable();
        }

        internal void _TextureQualityChanged()
        {
            SendMessage("TextureQualityChanged", null, null);

            TextureQualityChanged();
        }

        /// <summary>
        /// Used to preform an action when the texture quality has been changed.
        /// </summary>
        protected virtual void TextureQualityChanged() { }

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
