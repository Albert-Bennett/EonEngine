/* Created: 05/01/2015
 * Last Updated: 04/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using System;

namespace Eon.System.Tools
{
    /// <summary>
    /// Defines a manager of content managers. 
    /// </summary>
    public sealed class ContentBuilder
    {
        EonDictionary<string, ContentTyper> loaders =
            new EonDictionary<string, ContentTyper>();

        IServiceProvider service;

        string rootFilepath = "Content\\";
        string currentTyperID = "Default";

        /// <summary>
        /// The current ContentTyper used by the game.
        /// </summary>
        public string CurrentTyperID
        {
            get { return currentTyperID; }
            set { currentTyperID = value; }
        }

        /// <summary>
        /// Sets the root directory for the ContentBuilder.
        /// </summary>
        public string RootDirectory
        {
            get { return rootFilepath; }
            set
            {
                rootFilepath = value;

                foreach (ContentTyper t in loaders.Values)
                    t.SetContentRoot(value);
            }
        }

        public ContentBuilder(IServiceProvider serviceProvider)
        {
            this.service = serviceProvider;

            loaders.Add("Default", new ContentTyper(service, rootFilepath, true));
        }

        /// <summary>
        /// Loads an object using the default ContentTyper (not the current one).
        /// </summary>
        /// <typeparam name="T">The type of object to be loaded.</typeparam>
        /// <param name="filepath">The object's filepath.</param>
        /// <returns>The loaded object.</returns>
        public T Load<T>(string filepath)
        {
            return loaders["Default"].Load<T>(filepath);
        }

        /// <summary>
        /// Loads an object.
        /// </summary>
        /// <typeparam name="T">The type of object to be loaded.</typeparam>
        /// <param name="filepath">The object's filepath.</param>
        /// <returns>The loaded object.</returns>
        /// <param name="contentTyperName">The name of the ContentTyper to be used.</param>
        public T Load<T>(string filepath, string contentTyperName)
        {
            bool found = false;
            int i = 0;

            while (!found && i < loaders.Count)
            {
                if (loaders[i].Key == contentTyperName)
                {
                    found = true;

                    return loaders[i].Value.Load<T>(filepath);
                }

                i++;
            }

            throw new ArgumentNullException("The ContentTyper: " +
                contentTyperName + " doesn't exist, using default ContentTyper");
        }

        /// <summary>
        /// Adds a ContentTyper to this.
        /// </summary>
        /// <param name="name">The name of the content typer.</param>
        /// <param name="isFixed">Does the ContentTyper unload after every level or 
        /// every play session.</param>
        public void AddContentTyper(string name, bool isFixed)
        {
            if (!loaders.Contains(name) && name != string.Empty)
                loaders.Add(name, new ContentTyper(service, rootFilepath, isFixed));
        }

        /// <summary>
        /// Unloads and removes a ContentTyper.
        /// Will not remove ContentTyper if IsFixed = true.
        /// </summary>
        public bool Unload(string name)
        {
            if (loaders.Contains(name))
            {
                loaders[name].Unload();

                if (!loaders[name].IsFixed)
                    loaders.Remove(name);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Unloads all content from all non fixed ContentTypers.
        /// </summary>
        public void UnloadAll(bool exitingGame)
        {
            for (int i = 0; i < loaders.Keys.Count; i++)
            {
                if (!loaders[i].Value.IsFixed || exitingGame)
                {
                    loaders[i].Value.Unload();
                    loaders.Remove(i);
                }
            }
        }
    }
}
