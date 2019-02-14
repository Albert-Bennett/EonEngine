/* Created: 07/09/2014
 * Last Updated: 10/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Resolution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eon.System.Tools
{
    /// <summary>
    /// Used to define the manager of TextureBuffers.
    /// </summary>
    public sealed class TextureBufferManager : EngineModule
    {
        static List<TextureBuffer> textureBuffers = new List<TextureBuffer>();

        public TextureBufferManager() : base("TextureBufferManager") { }

        internal static void Add(TextureBuffer buffer)
        {
            TextureBuffer tex = (from t in textureBuffers
                                 where t.Name == buffer.Name
                                 select t).FirstOrDefault();

            if (tex == null)
            {
                textureBuffers.Add(buffer);

                textureBuffers = textureBuffers.OrderBy(t => t.Order).ToList();
            }
        }

        public void Draw()
        {
            Common.Batch.Begin();
            Common.Device.Clear(Color.Black);

            if (textureBuffers.Count > 1)
            {
                for (int i = 0; i < textureBuffers.Count; i++)
                    Draw(textureBuffers[i].Output);
            }
            else
                Draw(textureBuffers[0].Output);

            Common.Batch.End();
        }

        static void Draw(Texture2D texture)
        {
            Common.Batch.Draw(texture, Common.Device.Viewport.Bounds, Color.White);
        }

        public void SaveSnapShot(string filepath)
        {
            RenderTarget2D target = new RenderTarget2D(Common.Device,
                (int)Common.TextureQuality.X, (int)Common.TextureQuality.Y);

            Common.Device.SetRenderTarget(target);

            Draw();

            Common.Device.SetRenderTarget(null);

            FileStream stream = new FileStream(filepath + ".jpg", FileMode.OpenOrCreate);

            target.SaveAsJpeg(stream, (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y);

            stream.Close();
        }

        /// <summary>
        /// Sets the texture of a TextureBuffer.
        /// </summary>
        /// <param name="textureBufferName">The name of the TextureBuffer.</param>
        /// <param name="textureName">The name of the texture to be added/ set.</param>
        /// <param name="texture">The texture.</param>
        public static void SetTexture(string textureBufferName, 
            string textureName, Texture2D texture)
        {
            TextureBuffer tex = (from t in textureBuffers
                                 where t.Name == textureBufferName
                                 select t).FirstOrDefault();

            if (tex != null)
                tex.SetBuffer(textureName, texture);
        }

        /// <summary>
        /// Gets a texture from a TextureBuffer.
        /// </summary>
        /// <param name="textureBufferName">The name of the TextureBuffer.</param>
        /// <param name="textureName">The name of the texture to get.</param>
        /// <returns>The texture with the given name.</returns>
        public static Texture2D GetTexture(
            string textureBufferName, string textureName)
        {
            TextureBuffer tex = (from t in textureBuffers
                                 where t.Name == textureBufferName
                                 select t).FirstOrDefault();

            if (tex != null)
                return tex.GetTexture(textureName);

            return null;
        }

        /// <summary>
        /// Gets the ID of the texture to be drawn to the screen. 
        /// </summary>
        /// <param name="textureBufferName">The name of the TextureBuffer.</param>
        /// <returns>The ID of the TextureBuffer's output texture.</returns>
        public static string GetOutputTextureID(string textureBufferName)
        {
            TextureBuffer tex = (from t in textureBuffers
                                 where t.Name == textureBufferName
                                 select t).FirstOrDefault();

            if (tex != null)
                return tex.OutputTextureID;

            return "";
        }

        /// <summary>
        /// Removes a texture from a TextureBuffer.
        /// </summary>
        /// <param name="textureBufferName">The name of the TextureBuffer.</param>
        /// <param name="textureID">The ID of the texture to be removed.</param>
        public static void RemoveTexture(string textureBufferName, string textureID)
        {
            TextureBuffer tex = (from t in textureBuffers
                                 where t.Name == textureBufferName
                                 select t).FirstOrDefault();

            if (tex != null)
                tex.Remove(textureID);
        }
    }
}
