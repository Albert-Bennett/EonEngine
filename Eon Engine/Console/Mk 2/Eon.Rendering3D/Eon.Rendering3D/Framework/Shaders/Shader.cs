/* Created 07/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Testing.ErrorManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Rendering3D.Framework.Shaders
{
    /// <summary>
    /// Used to define a shader.
    /// </summary>
    public class Shader
    {
        Effect effect;

        /// <summary>
        /// Paremeters in the shader.
        /// </summary>
        public ShaderParameter[] Parameters;

        /// <summary>
        /// The effect that is managed by the Shader.
        /// </summary>
        public Effect Effect
        {
            get { return effect; }
        }

        /// <summary>
        /// The filepath for the shader.
        /// </summary>
        public string ShaderFilepath;

        /// <summary>
        /// The default technique for the Shader.
        /// </summary>
        public string DefaultTechnique = "NULL";

        /// <summary>
        /// Used to define how Eon Engine will
        /// render the MeshPart.
        /// </summary>
        public RenderTypes RenderType;

        /// <summary>
        /// Loads the Shader.
        /// </summary>
        public void Load()
        {
            effect = Common.ContentManager.Load<Effect>(ShaderFilepath);

            for (int i = 0; i < Parameters.Length; i++)
                Parameters[i] = LoadParameter(Parameters[i]);
        }

        /// <summary>
        /// Used to set the view and projection matrices of the effect
        /// and any other parameters of the effect.
        /// </summary>
        /// <param name="worldMatrix">The world matrix of ther mesh being rendered.</param>
        public void SetParameters(Matrix worldMatrix)
        {
            Effect.Parameters["World"].SetValue(worldMatrix);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            for (int i = 0; i < Parameters.Length; i++)
                switch (Parameters[i].ValueType)
                {
                    case ParameterTypes.Bool:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (bool)Parameters[i].Value);
                        break;

                    case ParameterTypes.Texture2D:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (Texture2D)Parameters[i].Value);
                        break;

                    case ParameterTypes.Texture3D:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (Texture3D)Parameters[i].Value);
                        break;

                    case ParameterTypes.Byte:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (byte)Parameters[i].Value);
                        break;

                    case ParameterTypes.Float:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (float)Parameters[i].Value);
                        break;

                    case ParameterTypes.Int:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (int)Parameters[i].Value);
                        break;

                    case ParameterTypes.Vector2:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                           (Vector2)Parameters[i].Value);
                        break;

                    case ParameterTypes.Vector3:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (Vector3)Parameters[i].Value);
                        break;

                    case ParameterTypes.Vector4:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (Vector4)Parameters[i].Value);
                        break;

                    case ParameterTypes.Matrix:
                        Effect.Parameters[Parameters[i].ParameterName].SetValue(
                            (Matrix)Parameters[i].Value);
                        break;
                }
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
                    ShaderFilepath, Seriousness.Error);

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
            if (DefaultTechnique != string.Empty)
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

        /// <summary>
        /// Changes the value of a parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to be changed.</param>
        /// <param name="value">The value to change the parameter to.</param>
        public void ChangeParameter(string parameterName, string value)
        {
            for (int i = 0; i < Parameters.Length; i++)
                if (Parameters[i].ParameterName == parameterName)
                {
                    Parameters[i].Value = value;
                    Parameters[i] = LoadParameter(Parameters[i]);
                }
        }

        ShaderParameter LoadParameter(ShaderParameter parameter)
        {
            switch (parameter.ValueType)
            {
                case ParameterTypes.Bool:
                    parameter.Value = bool.Parse(parameter.ValueString);
                    break;

                case ParameterTypes.Texture2D:
                    parameter.Value = Common.ContentManager.Load<Texture2D>(parameter.ValueString);
                    break;

                case ParameterTypes.Texture3D:
                    parameter.Value = Common.ContentManager.Load<Texture3D>(parameter.ValueString);
                    break;

                case ParameterTypes.Byte:
                    parameter.Value = byte.Parse(parameter.ValueString);
                    break;

                case ParameterTypes.Float:
                    parameter.Value = float.Parse(parameter.ValueString);
                    break;

                case ParameterTypes.Int:
                    parameter.Value = int.Parse(parameter.ValueString);
                    break;

                case ParameterTypes.Vector2:
                    {
                        string[] val = parameter.ValueString.Split(new char[]
                            {
                                ','
                            }, StringSplitOptions.RemoveEmptyEntries);

                        parameter.Value = new Vector2()
                        {
                            X = float.Parse(val[0]),
                            Y = float.Parse(val[1])
                        };
                    }
                    break;

                case ParameterTypes.Vector3:
                    {
                        string[] val = parameter.ValueString.Split(new char[]
                            {
                                ','
                            }, StringSplitOptions.RemoveEmptyEntries);

                        parameter.Value = new Vector3()
                        {
                            X = float.Parse(val[0]),
                            Y = float.Parse(val[1]),
                            Z = float.Parse(val[2])
                        };
                    }
                    break;

                case ParameterTypes.Vector4:
                    {
                        string[] val = parameter.ValueString.Split(new char[]
                            {
                                ','
                            }, StringSplitOptions.RemoveEmptyEntries);

                        parameter.Value = new Vector4()
                        {
                            X = float.Parse(val[0]),
                            Y = float.Parse(val[1]),
                            Z = float.Parse(val[2]),
                            W = float.Parse(val[3])
                        };
                    }
                    break;

                case ParameterTypes.Matrix:
                    {
                        string[] val = parameter.ValueString.Split(new char[]
                            {
                                ','
                            }, StringSplitOptions.RemoveEmptyEntries);

                        parameter.Value = new Matrix()
                        {
                            M11 = float.Parse(val[0]),
                            M12 = float.Parse(val[1]),
                            M13 = float.Parse(val[2]),
                            M14 = float.Parse(val[3]),

                            M21 = float.Parse(val[4]),
                            M22 = float.Parse(val[5]),
                            M23 = float.Parse(val[6]),
                            M24 = float.Parse(val[7]),

                            M31 = float.Parse(val[8]),
                            M32 = float.Parse(val[9]),
                            M33 = float.Parse(val[10]),
                            M34 = float.Parse(val[11]),

                            M41 = float.Parse(val[12]),
                            M42 = float.Parse(val[13]),
                            M43 = float.Parse(val[14]),
                            M44 = float.Parse(val[15])
                        };
                    }
                    break;
            }

            return parameter;
        }
    }
}
