/* Created: 27/09/2014
 * Last Updated: 05/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework;
using Eon.Rendering3D.Primatives.Constructs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives
{
    /// <summary>
    /// Used to define the base class for the
    /// LOD management of primatives.
    /// </summary>
    public abstract class BaseLOD : ILODRenderer
    {
        Primative[] primatives;

        Effect effect;

        LODLevels lod;

        bool enabled = true;

        Matrix world;

        /// <summary>
        /// Is the BaseLOD enabled?
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
        }

        /// <summary>
        /// The effect used to render the BaseLOD.
        /// </summary>
        public Effect Effect
        {
            get { return effect; }
        }

        /// <summary>
        /// Defines the way in which the BaseLOD will be rendered.
        /// </summary>
        public RenderTypes RenderType
        {
            get { return RenderTypes.SemiLPP; }
        }

        /// <summary>
        /// Is the BaseLOD visable to the current camera.
        /// </summary>
        public bool IsSeen
        {
            get { return IsInView() && GetLOD() != LODLevels.Three; }
        }

        /// <summary>
        /// The current LOD.
        /// </summary>
        public LODLevels CurrentLOD
        {
            get { return lod; }
        }

        /// <summary>
        /// The position of the BaseLOD.
        /// </summary>
        public virtual Vector3 Position
        {
            get { return world.Translation; }
            set { world.Translation = value; }
        }

        /// <summary>
        /// The world matrix of the PlaneLOD.
        /// </summary>
        public virtual Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        /// <summary>
        /// Creates a new BaseLOD.
        /// </summary>
        /// <param name="position">The position of the BaseLOD.</param>
        /// <param name="scale">The scale of the BaseLOD.</param>
        /// <param name="rotation">The rotation of the BaseLOD.</param>
        public BaseLOD(Vector3 position, Vector3 scale, Vector3 rotation)
        {
            World = Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z) *
                Matrix.CreateScale(scale.X, scale.Y, scale.Z) * Matrix.CreateTranslation(position);
        }

        /// <summary>
        /// Creates a new BaseLOD.
        /// </summary>
        /// <param name="world">The BaseLOD's world matrix.</param>
        public BaseLOD(Matrix world)
        {
            World = world;
        }

        public void _PreRender()
        {
            PreRender();
        }

        protected virtual void PreRender() { }

        public void _Render(string technique)
        {
            if (primatives.Length > (int)CurrentLOD)
                primatives[(int)CurrentLOD].Render(world);
            else
                primatives[0].Render(world);
        }

        /// <summary>
        /// Adds a set of Primatives to this to be managed.
        /// </summary>
        /// <param name="primatives">The primatives to be managed.</param>
        protected void SetPrimatives(Primative[] primatives)
        {
            effect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Materials/BasicEffect");

            this.primatives = primatives;

            for (int i = 0; i < primatives.Length; i++)
                primatives[i].Parent = this;
        }

        LODLevels GetLOD()
        {
            lod = CameraManager.CurrentCamera.GetLODLevel(Position);

            return lod;
        }

        protected abstract bool IsInView();

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
            }
            catch { }
        }

        /// <summary>
        /// Toogles enabled.
        /// </summary>
        public void ToogleEnabled()
        {
            enabled = !enabled;
        }
    }
}
