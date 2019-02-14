/* Created: 07/05/2014
 * Last Updated: 26/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Rendering3D.Framework.Materials
{
    /// <summary>
    /// Used to define the shaders in a model.
    /// </summary>
    public sealed class ModelInfo
    {
        /// <summary>
        /// The rotational baseTransform of the ModelComponent.
        /// </summary>
        public float RotationX = 0;

        /// <summary>
        /// The rotational baseTransform of the ModelComponent.
        /// </summary>
        public float RotationY = 0;

        /// <summary>
        /// The rotational baseTransform of the ModelComponent.
        /// </summary>
        public float RotationZ = 0;

        /// <summary>
        /// The positional baseTransform of the ModelComponent.
        /// </summary>
        public float PositionX = 0;

        /// <summary>
        /// The positional baseTransform of the ModelComponent.
        /// </summary>
        public float PositionY = 0;

        /// <summary>
        /// The positional baseTransform of the ModelComponent.
        /// </summary>
        public float PositionZ = 0;

        /// <summary>
        /// The scale of the ModelComponent.
        /// </summary>
        public float Scale = 1.0f;

        /// <summary>
        /// Is the ModelComponent going to be a 
        /// static model or will it be moveable.
        /// </summary>
        public bool IsStatic;

        /// <summary>
        /// The model that make up a ModelComponent.
        /// </summary>
        public EonDictionary<int, LODModelInfo> Models =
              new EonDictionary<int, LODModelInfo>();
    }
}
