/* Created: 26/09/2014
 * Last Updated: 24/02/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Rendering3D.Framework.Shaders
{
    /// <summary>
    /// Used to define the animated texture shader. 
    /// At Eon/Shaders/Materials/AnimatedTexture.
    /// </summary>
    public sealed class AnimatedTextureMaterial : Shader
    {
        Texture2D colourMap;
        Texture2D normalMap;

        Vector2 speed = Vector2.Zero;

        /// <summary>
        /// The filepath of the texture to be used in the shader.
        /// </summary>
        public string TextureFilepath = "Eon\\Textures\\DefaultTexture";

        /// <summary>
        /// The filepath of the normal map to be used in the material.
        /// </summary>
        public string NormalMapFilepath = "Eon\\Textures\\DefaultNormalMap";

        /// <summary>
        /// The speed of the animation.
        /// </summary>
        public float SpeedX;

        /// <summary>
        /// The speed of the animation.
        /// </summary>
        public float SpeedY;

        /// <summary>
        /// The speed of the texture movement.
        /// </summary>
        public Vector2 Speed
        {
            get { return speed; }
        }

        protected override void _Load()
        {
            shaderFilepath = "Eon/Shaders/Materials/AnimatedTexture";

            speed = new Vector2(SpeedX, SpeedY);

            colourMap = LoadTexture(TextureFilepath);
            normalMap = LoadTexture(NormalMapFilepath);

            defaultTechnique = "Render";

            renderType = RenderTypes.SemiLPP;

            base._Load();
        }

        public override void SetParameters(Matrix worldMatrix)
        {
            Effect.Parameters["World"].SetValue(worldMatrix);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            Effect.Parameters["Speed"].SetValue(speed);
            Effect.Parameters["ColourMap"].SetValue(colourMap);
            Effect.Parameters["NormalMap"].SetValue(normalMap);
        }
    }
}
