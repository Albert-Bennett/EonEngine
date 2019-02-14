/* Created: 05/01/2015
 * Last Updated: 23/03/2015
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
        ContentManager content;

        bool isFixed;

        /// <summary>
        /// Whether or not the content loaded should be unloaded
        /// when an unload all method has been called.
        /// </summary>
        internal bool IsFixed
        {
            get { return isFixed; }
        }

        /// <summary>
        /// Creates a new ContentTyper.
        /// </summary>
        /// <param name="rootFilepath">The root directory from where content will be loaded.</param>
        public ContentTyper(IServiceProvider service,
            string rootFilepath, bool isFixed)
        {
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

        internal void Unload()
        {
            content.Unload();
        }
    }
}
