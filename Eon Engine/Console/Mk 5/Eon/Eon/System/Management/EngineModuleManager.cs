/* Created: 12/06/2013
 * Last Updated: 15/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces.Base;
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
    public sealed class EngineModuleManager
    {
        #region Component Bases

        static List<EngineModule> comps = new List<EngineModule>();

        static List<IRenderComponent> renderComps = new List<IRenderComponent>();
        static List<IPostRenderComponent> postRenderComps = new List<IPostRenderComponent>();
        static List<IUpdate> updateComps = new List<IUpdate>();
        static List<IPostGameDraw> postGame = new List<IPostGameDraw>();

        #endregion
        #region Ctor

        public void Initialize()
        {
            for (int i = 0; i < comps.Count; i++)
                comps[i]._Init();

            updateComps = updateComps.OrderBy(u => u.Priority).ToList();
            postRenderComps = postRenderComps.OrderBy(p => p.Order).ToList();
            renderComps = renderComps.OrderBy(r => r.Order).ToList();
        }

        public void Update()
        {
            for (int i = 0; i < updateComps.Count; i++)
                updateComps[i]._Update();
        }

        public void PostUpdate()
        {
            for (int i = 0; i < updateComps.Count; i++)
                updateComps[i]._PostUpdate();
        }

        public void Render()
        {
            for (int i = 0; i < renderComps.Count; i++)
                if (((EngineModule)renderComps[i]).Enabled)
                    renderComps[i].Render();

            for (int i = 0; i < postRenderComps.Count; i++)
                if (((EngineModule)postRenderComps[i]).Enabled)
                    postRenderComps[i].PostRender();

            for (int i = 0; i < postGame.Count; i++)
                postGame[i].PostGameDraw();
        }

        public void Dispose()
        {
            comps.Clear();
            updateComps.Clear();
            renderComps.Clear();
            postRenderComps.Clear();
            postGame.Clear();
        }

        #endregion
        #region Component Based Actions

        internal static void AddComp(EngineModule comp)
        {
            EngineModule c = (from com in comps
                                 where com.ID == comp.ID
                                 select com).FirstOrDefault();

            if (c == null)
            {
                comps.Add(comp);

                if (comp is IPostGameDraw)
                    postGame.Add(comp as IPostGameDraw);

                if (comp is IRenderComponent)
                    renderComps.Add(comp as IRenderComponent);

                if (comp is IPostRenderComponent)
                    postRenderComps.Add(comp as IPostRenderComponent);

                if (comp is IUpdate)
                    updateComps.Add(comp as IUpdate);
            }
        }

        /// <summary>
        /// Finds an EngineComponent with the given id.
        /// </summary>
        /// <param name="id">The id of the engine component to be found.</param>
        /// <returns>The found EngineComponent or null.</returns>
        public static EngineModule Find(string id)
        {
            EngineModule e = (from en in comps
                                 where en.ID == id
                                 select en).FirstOrDefault();

            return e;
        }

        /// <summary>
        /// Finds an EngineComponent of the given type.
        /// </summary>
        /// <param name="type">The type of EngineComponent to be found.</param>
        /// <returns>The found EngineComponent or null.</returns>
        public static EngineModule Find(Type type)
        {
            EngineModule e = (from en in comps
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
        internal static void RemoveComponent(EngineModule component)
        {
            if (comps.Contains(component))
            {
                if (component is IUpdate)
                    updateComps.Remove(component as IUpdate);

                if (component is IRenderComponent)
                    renderComps.Remove(component as IRenderComponent);

                if (component is IPostRenderComponent)
                    postRenderComps.Remove(component as IPostRenderComponent);

                if (component is IPostGameDraw)
                    postGame.Remove(component as IPostGameDraw);

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

        public void SendMessage(Message message)
        {
            if (message.MethodName != "")
            {
                MethodInfo info = GetType().GetMethod(message.MethodName);

                if (info != null)
                    info.Invoke(this, message.Parameters);
                else
                {
                    if (message.TargetID == null)
                    {
                        var cs = (from c in comps
                                  where c.GetType().GetMethod(message.MethodName) != null
                                  select c).ToList();

                        foreach (var comp in cs)
                            comp.GetType().GetMethod(message.MethodName).Invoke(comp, message.Parameters);
                    }
                    else
                    {
                        EngineModule mod = Find(message.TargetID);

                        if (mod != null)
                            if (mod.GetType().GetMethod(message.MethodName) != null)
                                mod.GetType().GetMethod(message.MethodName).Invoke(mod, message.Parameters);
                    }
                }
            }
        }

        #endregion
    }
}
