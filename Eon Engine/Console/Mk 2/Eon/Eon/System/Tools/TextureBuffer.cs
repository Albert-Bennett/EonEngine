/* Created 20/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.System.Interfaces;
using Eon.System.Management;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.System.Tools
{
    /// <summary>
    /// Used to define a place where the 
    /// current set of textures are set.
    /// </summary>
    public sealed class TextureBuffer : EngineComponent, IUpdate
    {
        static EonDictionary<string, Texture2D> buffer =
             new EonDictionary<string, Texture2D>();

        public int Priority
        {
            get { return 0; }
        }

        public TextureBuffer() : base("TextureBuffer") { }

        public void _Update()
        {
            buffer.Clear();
        }

        /// <summary>
        /// Sets the images on the TextureBuffer.
        /// </summary>
        /// <param name="textureName">The name of the texture to be set.</param>
        /// <param name="texture">The texture to be set.</param>
        public static void SetBuffer(string textureName, Texture2D texture)
        {
            if (buffer.Contains(textureName))
                buffer[textureName] = texture;
            else
                buffer.Add(textureName, texture);
        }

        /// <summary>
        /// Gets a specific texture.
        /// </summary>
        /// <param name="textureName">The name of the texture to get.</param>
        /// <returns>The found texture if any.</returns>
        public static Texture2D GetTexture(string textureName)
        {
            if (buffer.Contains(textureName))
                return buffer[textureName];

            return null;
        }

        /// <summary>
        /// Removes a texture stored in the TextureBuffer.
        /// </summary>
        /// <param name="textureName">The name of the texture to be disposed.</param>
        public static void Remove(string textureName)
        {
            if (buffer.Contains(textureName))
                buffer.Remove(textureName);
        }
    }
}
