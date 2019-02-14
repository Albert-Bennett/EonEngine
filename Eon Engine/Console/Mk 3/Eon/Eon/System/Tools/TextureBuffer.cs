/* Created: 20/05/2014
 * Last Updated: 07/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.System.Tools
{
    /// <summary>
    /// Used to define a place where the 
    /// current set of textures are set.
    /// </summary>
    public sealed class TextureBuffer
    {
        EonDictionary<string, Texture2D> buffer =
             new EonDictionary<string, Texture2D>();

        string name;
        int order;

        string outputTextureID;

        /// <summary>
        /// The output texture for the TextureBuffer. 
        /// </summary>
        public Texture2D Output
        {
            get
            {
                Texture2D tex = buffer[outputTextureID];

                if (tex == null)
                    return new Texture2D(Common.Device, 1, 1);
                else
                    return tex;
            }
        }

        /// <summary>
        /// The name of the TextureBuffer.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The ID of the output texture.
        /// </summary>
        public string OutputTextureID
        {
            get { return outputTextureID; }
        }

        /// <summary>
        /// The order in which to draw the TextureBuffer.
        /// </summary>
        public int Order
        {
            get { return order; }
        }

        /// <summary>
        /// Creates a new TextureBuffer.
        /// </summary>
        /// <param name="name">The name of the TextureBuffer.</param>
        /// <param name="order">The order in which to draw the TextureBuffer.</param>
        /// <param name="outputTextureID">The name of the output texture.</param>
        public TextureBuffer(string name, int order, string outputTextureID)
        {
            this.name = name;
            this.order = order;
            this.outputTextureID = outputTextureID;

            TextureBufferManager.Add(this);
        }

        /// <summary>
        /// Sets the images on the TextureBuffer.
        /// </summary>
        /// <param name="textureName">The name of the texture to be set.</param>
        /// <param name="texture">The texture to be set.</param>
        public void SetBuffer(string textureName, Texture2D texture)
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
        public Texture2D GetTexture(string textureName)
        {
            if (buffer.Contains(textureName))
                return buffer[textureName];

            return null;
        }

        /// <summary>
        /// Removes a texture stored in the TextureBuffer.
        /// </summary>
        /// <param name="textureName">The name of the texture to be disposed.</param>
        public void Remove(string textureName)
        {
            if (buffer.Contains(textureName))
                buffer.Remove(textureName);
        }

        internal void Clear()
        {
            buffer.Clear();
        }
    }
}
