/* Created: 24/09/2014
 * Last Updated: 05/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework
{
    /// <summary>
    /// Used to define an interface that is used to define a 3D renderable object.
    /// </summary>
    public interface IRenderable3D
    {
        bool Enabled { get; }
        bool IsSeen { get; }

        Vector3 Position { get; }

        RenderTypes RenderType { get; }

        void _Render(string technique);
        void _PreRender();

        void SetParameter(string parameterName, Texture2D image);
        void SetParameter(string parameterName, float value);
        void SetParameter(string parameterName, Vector2 value);
    }
}
