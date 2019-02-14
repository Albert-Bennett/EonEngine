/* Created: 05/01/2015
 * Last Updated: 05/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Eon.System.Tools
{
    /// <summary>
    /// Defines a manager of content managers. 
    /// </summary>
    public sealed class ContentBuilder
    {
        List<ContentTyper> typers = new List<ContentTyper>();

        IServiceProvider service;

        string rootFilepath = "Content\\";

        public string RootDirectory
        {
            get { return rootFilepath; }
            set
            {
                rootFilepath = value;

                foreach (ContentTyper t in typers)
                    t.SetContentRoot(value);
            }
        }

        public ContentBuilder(IServiceProvider serviceProvider)
        {
            this.service = serviceProvider;
        }

        /// <summary>
        /// Loads an object.
        /// </summary>
        /// <typeparam name="T">The type of object to be loaded.</typeparam>
        /// <param name="filepath">The object's filepath.</param>
        /// <returns>The loaded object.</returns>
        public T Load<T>(string filepath)
        {
            bool found = false;
            int i = 0;

            while (!found && i < typers.Count)
            {
                if (typers[i].ContentType == typeof(T))
                {
                    found = true;

                    return typers[i].Load<T>(filepath);
                }

                i++;
            }

            ContentTyper content = new ContentTyper(typeof(T).Name,
                typeof(T), service, rootFilepath, false);

            typers.Add(content);

            return content.Load<T>(filepath);
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

            while (!found && i < typers.Count)
            {
                if (typers[i].Name == contentTyperName)
                {
                    found = true;

                    return typers[i].Load<T>(filepath);
                }

                i++;
            }

            throw new ArgumentNullException("The ContentTyper: " + 
                contentTyperName + " doesn't exist");
        }

        /// <summary>
        /// Adds a ContentTyper to this.
        /// </summary>
        /// <param name="name">The name of the content typer.</param>
        /// <param name="isFixed">Does the ContentTyper unload after every level or 
        /// every play session.</param>
        public void AddContentTyper(string name, bool isFixed)
        {
            ContentTyper ty = (from t in typers
                               where t.Name == name
                               select t).FirstOrDefault();

            if (ty.Name != string.Empty)
                typers.Add(new ContentTyper(name, typeof(string),
                    service, rootFilepath, isFixed));
        }

        /// <summary>
        /// Unloads all content from all non fixed ContentTypers.
        /// </summary>
        public void UnloadAll()
        {
            for (int i = 0; i < typers.Count; i++)
                if (!typers[i].IsFixed)
                    typers[i].Unload(false);
        }

        internal void FinalizeUnload()
        {
            for (int i = 0; i < typers.Count; i++)
                typers[i].Unload(true);

            typers.Clear();
        }
    }
}
