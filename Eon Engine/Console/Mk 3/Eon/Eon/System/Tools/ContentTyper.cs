/* Created: 05/01/2015
 * Last Updated: 05/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content;
using System;

namespace Eon.System.Tools
{
    /// <summary>
    /// Used to define a content manager that can 
    /// only load one type of content.
    /// </summary>
    internal struct ContentTyper
    {
        Type contentType;
        ContentManager content;

        string name;

        bool isFixed;

        /// <summary>
        /// The name of the ContentTyper.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Wheather or not the content loaded should be unloaded
        /// when an unload all method has been called.
        /// </summary>
        internal bool IsFixed
        {
            get { return isFixed; }
        }

        /// <summary>
        /// The type of content that this will only load.
        /// </summary>
        public Type ContentType
        {
            get { return contentType; }
        }

        /// <summary>
        /// Creates a new ContentTyper.
        /// </summary>
        /// <param name="name">The name of the ContentTyper.</param>
        /// <param name="loadingtype">Tye type of object that con only be loaded.</param>
        /// <param name="rootFilepath">The root directory from where content will be loaded.</param>
        public ContentTyper(string name,
            Type loadingtype, IServiceProvider service,
            string rootFilepath, bool isFixed)
        {
            this.name = name;
            this.contentType = loadingtype;
            this.isFixed = isFixed;

            content = new ContentManager(service, rootFilepath);
        }

        internal T Load<T>(string filepath)
        {
            return content.Load<T>(filepath);
        }

        internal void SetContentRoot(string root)
        {
            content.RootDirectory = root;
        }

        internal void Unload(bool final)
        {
            content.Unload();

            if (final)
                content.Dispose();
        }
    }
}
