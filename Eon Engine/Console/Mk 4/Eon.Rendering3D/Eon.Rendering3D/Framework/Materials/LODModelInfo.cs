/* Created: 28/09/2014
 * Last Updated: 28/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Rendering3D.Framework.Materials
{
    /// <summary>
    /// Used to hold relevent informnation about an LODModel.
    /// </summary>
    public sealed class LODModelInfo
    {
        /// <summary>
        /// Defines where the ModelComponent's collision model is. 
        /// </summary>
        public string CollisionModelFilepath = "NULL";

        /// <summary>
        /// The filepath for the model.
        /// </summary>
        public string ModelFilepath;

        /// <summary>
        /// A dictionary of model mesh part names and shaders.
        /// </summary>
        public EonDictionary<string, Material> Materials =
              new EonDictionary<string, Material>();
    }
}
