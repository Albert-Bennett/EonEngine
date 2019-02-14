/* Created: 26/01/2015
 * Last Updated: 28/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths;

namespace EEDK.Gui.ModelViewer
{
    /// <summary>
    /// Used to define common aspects between
    /// the ModelViewer and it's MenuScreen.
    /// </summary>
    public sealed class CommonPass
    {
        Transformation modelTransform;

        bool updated = false;

        public SaveRequestEvent OnSave;
        public LoadRequestEvent OnLoad;

        /// <summary>
        /// The current ModelInfo file.
        /// </summary>
        public Transformation ModelTransform
        {
            get { return modelTransform; }
            set { modelTransform = value; }
        }

        /// <summary>
        /// Used to define if the ModelInfo
        /// has been edited in anyway.
        /// </summary>
        public bool Updated
        {
            get { return updated; }
            set { updated = value; }
        }

        /// <summary>
        /// Used to send messages to save files.
        /// </summary>
        /// <param name="filepath">The file path of the object to be saved.</param>
        internal void Save(string filepath)
        {
            if (OnSave != null)
                OnSave(filepath);
        }

        /// <summary>
        /// Used to send messages to load files.
        /// </summary>
        /// <param name="filepath">The file path of the object to be loaded.</param>
        internal void Load(string filepath)
        {
            if (OnLoad != null)
                OnLoad(filepath);
        }
    }
}
