/* Created: 12/06/2013
 * Last Updated: 09/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.Management.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Eon.System.Management
{
    /// <summary>
    /// Defines a class which is used to manage 
    /// the basic functions of every EngineComponent.
    /// </summary>
    public sealed class EngineComponentManager
    {
        #region Component Bases

        static List<EngineComponent> comps = new List<EngineComponent>();

        static List<IRenderComponent> renderComps = new List<IRenderComponent>();
        static List<IPostRenderComponent> postRenderComps = new List<IPostRenderComponent>();
        static List<IUpdate> updateComps = new List<IUpdate>();
        static List<IDispose> disposableComps = new List<IDispose>();
        static List<IPostGameDraw> postGame = new List<IPostGameDraw>();
        static List<IPostUpdate> postUpdate = new List<IPostUpdate>();

        #endregion
        #region Ctor

        public void Initialize(ManualResetEvent doneEvent)
        {
            for (int i = 0; i < comps.Count; i++)
                comps[i]._Init();

            doneEvent.Set();
        }

        public void PostInitialize(ManualResetEvent doneEvent)
        {
            for (int i = 0; i < comps.Count; i++)
                if (comps[i] is IPostInitialize)
                    ((IPostInitialize)comps[i]).PostInitialize(null);

            doneEvent.Set();
        }

        public void Update()
        {
            for (int i = 0; i < updateComps.Count; i++)
                updateComps[i]._Update();
        }

        public void PostUpdate()
        {
            for (int i = 0; i < postUpdate.Count; i++)
                postUpdate[i]._PostUpdate();
        }

        public void Render()
        {
            for (int i = 0; i < renderComps.Count; i++)
                if (((EngineComponent)renderComps[i]).Enabled)
                    renderComps[i].Render();

            for (int i = 0; i < postRenderComps.Count; i++)
                if (((EngineComponent)postRenderComps[i]).Enabled)
                    postRenderComps[i].PostRender();

            for (int i = 0; i < postGame.Count; i++)
                postGame[i].PostGameDraw();
        }

        public void Dispose()
        {
            for (int i = 0; i < disposableComps.Count; i++)
                disposableComps[i].Dispose(true);

            comps.Clear();
            updateComps.Clear();
            disposableComps.Clear();
            renderComps.Clear();
            postRenderComps.Clear();
            postGame.Clear();
            postUpdate.Clear();
        }

        #endregion
        #region Component Based Actions

        internal static void AddComp(EngineComponent comp)
        {
            EngineComponent c = (from com in comps
                                 where com.ID == comp.ID
                                 select com).FirstOrDefault();

            if (c == null)
            {
                comps.Add(comp);

                if (comp is IPostGameDraw)
                    postGame.Add(comp as IPostGameDraw);

                if (comp is IPostUpdate)
                    postUpdate.Add(comp as IPostUpdate);

                if (comp is IRenderComponent)
                {
                    renderComps.Add(comp as IRenderComponent);

                    renderComps = renderComps.OrderBy(r => r.Order).ToList();
                }

                if (comp is IPostRenderComponent)
                {
                    postRenderComps.Add(comp as IPostRenderComponent);

                    postRenderComps = postRenderComps.OrderBy(p => p.Order).ToList();
                }

                if (comp is IUpdate)
                {
                    updateComps.Add(comp as IUpdate);

                    updateComps = updateComps.OrderBy(u => u.Priority).ToList();
                }

                if (comp is IDispose)
                    disposableComps.Add(comp as IDispose);
            }
        }

        /// <summary>
        /// Finds an EngineComponent with the given id.
        /// </summary>
        /// <param name="id">The id of the engine component to be found.</param>
        /// <returns>The found EngineComponent or null.</returns>
        public static EngineComponent Find(string id)
        {
            EngineComponent e = (from en in comps
                                 where en.ID == id
                                 select en).FirstOrDefault();

            return e;
        }

        /// <summary>
        /// Finds an EngineComponent of the given type.
        /// </summary>
        /// <param name="type">The type of EngineComponent to be found.</param>
        /// <returns>The found EngineComponent or null.</returns>
        public static EngineComponent Find(Type type)
        {
            EngineComponent e = (from en in comps
                                 where en.GetType() == type
                                 select en).FirstOrDefault();

            return e;
        }

        /// <summary>
        /// A check to see if an EngineComponent of the given id has been Created:.
        /// </summary>
        /// <param name="id">The id of the EngineComponent to be found.</param>
        /// <returns>The result of the search.</returns>
        public static bool Contains(string id)
        {
            for (int i = 0; i < comps.Count; i++)
                if (comps[i].ID == id)
                    return true;

            return false;
        }

        /// <summary>
        /// Used to remove an EngineComponent from this.
        /// </summary>
        /// <param name="component">The EngineComponent to be removed.</param>
        internal static void RemoveComponent(EngineComponent component)
        {
            if (comps.Contains(component))
            {
                if (component is IDispose)
                {
                    ((IDispose)component).Dispose(true);
                    disposableComps.Remove(component as IDispose);
                }

                if (component is IUpdate)
                    updateComps.Remove(component as IUpdate);

                if (component is IRenderComponent)
                    renderComps.Remove(component as IRenderComponent);

                if (component is IPostRenderComponent)
                    postRenderComps.Remove(component as IPostRenderComponent);

                if (component is IPostGameDraw)
                    postGame.Remove(component as IPostGameDraw);

                if (component is IPostUpdate)
                    postUpdate.Remove(component as IPostUpdate);

                comps.Remove(component);
            }
        }

        public void SendMessage(string message, params object[] parameters)
        {
            MethodInfo info = GetType().GetMethod(message);

            if (info != null)
                info.Invoke(this, parameters);
            else
            {
                var cs = (from c in comps
                          where c.GetType().GetMethod(message) != null
                          select c).ToList();

                foreach (var c in cs)
                    c.GetType().GetMethod(message).Invoke(c, parameters);
            }
        }

        #endregion
    }
}
