/* Created: 07/05/2014
 * Last Updated: 05/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

namespace Eon.Rendering3D.Framework.Materials
{
    /// <summary>
    /// Used to define a shader.
    /// </summary>
    public abstract class Material
    {
        Effect effect;

        protected string shaderFilepath;
        protected string defaultTechnique;

        protected RenderTypes renderType;

        /// <summary>
        /// The effect that is managed by the Shader.
        /// </summary>
        public Effect Effect
        {
            get { return effect; }
        }

        /// <summary>
        /// The default technique of the Shader. 
        /// </summary>
        public string DefaultTechnique
        {
            get { return defaultTechnique; }
        }

        /// <summary>
        /// Used to define how EonEngine will
        /// render the MeshPart.
        /// </summary>
        public RenderTypes RenderType
        {
            get { return renderType; }
        }

        /// <summary>
        /// Loads the shader; 
        /// </summary>
        public void Load()
        {
            _Load();
        }

        /// <summary>
        /// Loads the Shader.
        /// </summary>
        protected virtual void _Load()
        {
            try
            {
                effect = Common.ContentBuilder.Load<Effect>(shaderFilepath);
            }
            catch
            {
                throw new ArgumentNullException("The effect: " + shaderFilepath + " was not found.");
            }
        }

        protected Texture2D LoadTexture(string textureFilepath)
        {
            Texture2D tex;

            try
            {
                tex = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                new Error("Texture not found: " + textureFilepath, Seriousness.Error);

                tex = new Texture2D(Common.Device, 2, 2);
            }

            return tex;
        }

        /// <summary>
        /// A pre rendering action to do on the material.
        /// </summary>
        public virtual void PreRender() { }

        /// <summary>
        /// Used to set the view and projection matrices of the effect
        /// and any other parameters of the effect.
        /// </summary>
        /// <param name="worldMatrix">The world matrix of ther mesh being rendered.</param>
        public virtual void SetParameters(Matrix worldMatrix) { }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="image">The parameter's value.</param>
        public void SetParameter(string parameterName, Texture2D image)
        {
            try
            {
                if (effect.Parameters[parameterName] != null)
                    effect.Parameters[parameterName].SetValue(image);
                else
                    this.GetType().GetProperty(parameterName).SetValue(this, image);
            }
            catch { }
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, float value)
        {
            try
            {
                if (effect.Parameters[parameterName] != null)
                    effect.Parameters[parameterName].SetValue(value);
                else
                    this.GetType().GetProperty(parameterName).SetValue(this, value);
            }
            catch { }
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, Vector2 value)
        {
            try
            {
                if (effect.Parameters[parameterName] != null)
                    effect.Parameters[parameterName].SetValue(value);
                else
                    this.GetType().GetProperty(parameterName).SetValue(this, value);
            }
            catch { }
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, Vector3 value)
        {
            try
            {
                if (effect.Parameters[parameterName] != null)
                    effect.Parameters[parameterName].SetValue(value);
                else
                    this.GetType().GetProperty(parameterName).SetValue(this, value);
            }
            catch { }
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, string value)
        {
            try
            {
                this.GetType().GetProperty(parameterName).SetValue(this, value);
            }
            catch { }
        }

        /// <summary>
        /// Sets the current technique of the Shader.
        /// </summary>
        /// <param name="technique">The name of the technique to be used.</param>
        public void SetCurrentTechnique(string technique)
        {
            try
            {
                effect.CurrentTechnique = effect.Techniques[technique];
            }
            catch
            {
                new Error("The technique: " + technique + " dosn't exist in the effect located at: " +
                    shaderFilepath, Seriousness.Error);

                for (int i = 0; i < effect.Techniques.Count; i++)
                    effect.CurrentTechnique = effect.Techniques[i];
            }
        }

        /// <summary>
        /// Gets the first technique in the shader.
        /// </summary>
        /// <returns>The name of the first technique.</returns>
        public string GetDefaultTechnique()
        {
            if (DefaultTechnique != "NULL")
                return DefaultTechnique;
            else
                return effect.Techniques[0].Name;
        }

        /// <summary>
        /// Used to check, to see if the 
        /// Shader contains a specific technique.
        /// </summary>
        /// <param name="technique">The technique to be searched for.</param>
        /// <returns>The result of the search.</returns>
        public bool ContainsTechnique(string technique)
        {
            for (int i = 0; i < effect.Techniques.Count; i++)
                if (effect.Techniques[i].Name == technique)
                    return true;

            return false;
        }

        /// <summary>
        /// Applies the passes of the current technique.
        /// </summary>
        public void ApplyPasses()
        {
            for (int i = 0; i < effect.CurrentTechnique.Passes.Count; i++)
                effect.CurrentTechnique.Passes[i].Apply();
        }
    }
}
