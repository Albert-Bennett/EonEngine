/* Created 07/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Shaders
{
    /// <summary>
    /// Used to define the shaders in a model.
    /// </summary>
    public sealed class ModelDefination
    {
        /// <summary>
        /// Defines where the ModelComponent's collision model is. 
        /// </summary>
        public string CollisionModelFilepath = "NULL";

        /// <summary>
        /// The rotational offset of the ModelComponent.
        /// </summary>
        public Vector3 RotationalOffset = Vector3.Zero;

        /// <summary>
        /// The positional offset of the ModelComponent.
        /// </summary>
        public Vector3 PositionalOffset = Vector3.Zero;

        /// <summary>
        /// The scale of the ModelComponent.
        /// </summary>
        public float Scale = 1.0f;

        /// <summary>
        /// A dictionary of model mesh part names and shaders.
        /// </summary>
        public EonDictionary<string, Shader> Shaders =
              new EonDictionary<string, Shader>();
    }
}
