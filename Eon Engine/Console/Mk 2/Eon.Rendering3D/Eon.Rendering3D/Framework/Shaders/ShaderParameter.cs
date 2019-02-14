/* Created 07/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;

namespace Eon.Rendering3D.Framework.Shaders
{
    /// <summary>
    /// Used to define a parameter in a shader.
    /// </summary>
    public struct ShaderParameter
    {
        /// <summary>
        /// The name of a parameter in an effect.
        /// </summary>
        public string ParameterName;

        /// <summary>
        /// The value type of the parameter.
        /// </summary>
        public ParameterTypes ValueType;

        /// <summary>
        /// The value of the parameter.
        /// </summary>
        public string ValueString;

        /// <summary>
        /// The actual value of parameter.
        /// </summary>
        public object Value;
    }
}
