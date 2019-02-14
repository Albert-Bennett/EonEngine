﻿/* Created 12/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.System.Interfaces;
using Eon.System.Management.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eon.System.Management
{
    /// <summary>
    /// Defines a class which is used to manage 
    /// the basic functions of every EngineComponent.
    /// </summary>
    public sealed class EngineComponentManager
    {
        static List<EngineComponent> comps = new List<EngineComponent>();

        static List<IRenderComponent> renderComps = new List<IRenderComponent>();
        static List<IPostRenderComponent> postRenderComps = new List<IPostRenderComponent>();
        static List<IUpdate> updateComps = new List<IUpdate>();
        static List<IDispose> disposableComps = new List<IDispose>();
        static List<IPostGameDraw> postGame = new List<IPostGameDraw>();
        static List<IPostUpdate> postUpdate = new List<IPostUpdate>();

        static bool initialized = false;

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

                    List<IRenderComponent> temp;
                    SortHelper.SortList<IRenderComponent>(ref renderComps, out temp);

                    renderComps = temp;
                }

                if (comp is IPostRenderComponent)
                {
                    postRenderComps.Add(comp as IPostRenderComponent);

                    List<IPostRenderComponent> temp;
                    SortHelper.SortList<IPostRenderComponent>(ref postRenderComps, out temp);

                    postRenderComps = temp;
                }

                if (comp is IUpdate)
                    updateComps.Add(comp as IUpdate);

                if (comp is IDispose)
                    disposableComps.Add(comp as IDispose);

                if (initialized)
                    comp._Init();
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
        /// A check to see if an EngineComponent of the given id has been created.
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

        public void Initialize()
        {
            initialized = true;

            for (int i = 0; i < comps.Count; i++)
                comps[i]._Init();
        }

        public void PostInitialize()
        {
            for (int i = 0; i < comps.Count; i++)
                if (comps[i] is IPostInitialize)
                    ((IPostInitialize)comps[i]).PostInitialize();
        }

        public void Update()
        {
            int priority = 0;
            int count = updateComps.Count;

            while (count > 0)
            {
                for (int i = 0; i < updateComps.Count; i++)
                    if (updateComps[i].Priority == priority && 
                        ((EngineComponent)updateComps[i]).Enabled)
                    {
                        updateComps[i]._Update();
                        count--;
                    }

                priority++;
            }
        }

        public void PostUpdate()
        {
            for (int i = 0; i < postUpdate.Count; i++)
                postUpdate[i]._PostUpdate();
        }

        public void Render()
        {
            List<IRenderComponent> renders = new List<IRenderComponent>();

            for (int i = 0; i < renderComps.Count; i++)
                if (((EngineComponent)renderComps[i]).Enabled)
                    renders.Add(renderComps[i]);

            int order = 0;
            int count = renders.Count;

            while (count > 0)
            {
                for (int i = 0; i < renders.Count; i++)
                    if (renders[i].Priority == order)
                    {
                        renders[i].Render();
                        count--;
                    }

                order++;
            }
            
            for (int i = 0; i < postRenderComps.Count; i++)
                if (((EngineComponent)postRenderComps[i]).Enabled)
                    postRenderComps[i].PostRender();

            Common.Batch.Begin();

            for (int i = 0; i < renderComps.Count; i++)
                if (renderComps[i].RenderFinal)
                    Common.Batch.Draw(renderComps[i].FinalImage, Common.ScreenResolution, Color.White);

            for (int i = 0; i < postRenderComps.Count; i++)
                Common.Batch.Draw(postRenderComps[i].FinalImage, Common.ScreenResolution, Color.White);

            for (int i = 0; i < postGame.Count; i++)
                postGame[i].PostGameDraw();

            Common.Batch.End();
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
    }
}
