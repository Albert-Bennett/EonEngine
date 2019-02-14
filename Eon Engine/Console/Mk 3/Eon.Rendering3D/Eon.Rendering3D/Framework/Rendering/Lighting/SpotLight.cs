/* Created: 20/05/2014
 * Last Updated: 31/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// used to define a SpotLight.
    /// </summary>
    public sealed class SpotLight : DirectionalLight
    {
        float outerConeAngle;
        float innerConeAngle;

        Vector3 position;

        /// <summary>
        /// The outer angle of the cone used to render the SpotLight in degrees. 
        /// </summary>
        public float OuterConeAngle
        {
            get { return outerConeAngle; }
        }

        /// <summary>
        /// The inner angle of the cone used to render the SpotLight in degrees. 
        /// </summary>
        public float InnerConeAngle
        {
            get { return innerConeAngle; }
        }

        /// <summary>
        /// The world matrix of the SpotLight.
        /// </summary>
        public override Matrix World
        {
            get
            {
                Matrix baseTransform = Matrix.Identity;
                Matrix rotation = Matrix.Identity;

                if (Owner != null)
                {
                    baseTransform = Owner.World.Matrix;
                    rotation = Owner.World.Rotational;
                }

                return (rotation * rot) * Matrix.CreateTranslation(position + baseTransform.Translation);
            }
            set
            {
                Vector3 t;
                Vector3 scale;
                Quaternion rot;

                value.Decompose(out scale, out rot, out t);

                fallOff = (scale.X + scale.Y + scale.Z) / 3;
                position = t;
                direct = Vector3.Transform(Vector3.Down, Matrix.CreateFromQuaternion(rot));
            }
        }

        /// <summary>
        /// The position of the PointLight.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (Owner != null)
                    return Owner.World.Position + position;

                return position;
            }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// A check to see if the SpotLight
        /// is in view of the current CameraComponent.
        /// </summary>
        public override bool IsInView
        {
            get
            {
                Vector3 pos = position;

                if (Owner != null)
                    pos += Owner.World.Position;

                BoundingSphere sphere = new BoundingSphere(pos, fallOff);

                return CameraManager.CurrentCamera.IsInView(sphere);
            }
        }

        /// <summary>
        /// Creates a new SpotLight.
        /// </summary>
        /// <param name="id">The id of the SpotLight.</param>
        /// <param name="position">The position of the SpotLight.</param>
        /// <param name="colour">The colour of the SpotLight.</param>
        /// <param name="intensity">The intensity of the SpotLight.</param>
        /// <param name="castShadows">Wheather or not the SpotLight should cast shadows.</param>
        /// <param name="direction">The direction that the SpotLight is facing.</param>
        /// <param name="outerConeAngle">The outer angle of the cone used to render the SpotLight in degrees.</param>
        /// <param name="innerConeAngle">The inner angle of the cone used to render the SpotLight in degrees.</param>
        /// <param name="fallOff">The fall off of the cone used to render the SpotLight.</param>
        public SpotLight(string id, Vector3 position,
            Vector3 colour, float intensity, bool castsShadows,
            Vector3 direction, float outerConeAngle,
            float innerConeAngle, float fallOff)
            : base(id, colour, intensity, direction, castsShadows)
        {
            this.position = position;

            this.outerConeAngle = MathHelper.ToRadians(outerConeAngle / 2);
            this.innerConeAngle = MathHelper.ToRadians(innerConeAngle / 2);

            this.fallOff = fallOff;
        }

        /// <summary>
        /// Creates a new SpotLight.
        /// </summary>
        /// <param name="id">The id of the SpotLight.</param>
        /// <param name="position">The position of the SpotLight.</param>
        /// <param name="colour">The colour of the SpotLight.</param>
        /// <param name="castShadows">Wheather or not the SpotLight should cast shadows.</param>
        /// <param name="direction">The direction that the SpotLight is facing.</param>
        /// <param name="outerConeAngle">The outer angle of the cone used to render the SpotLight in degrees.</param>
        /// <param name="innerConeAngle">The inner angle of the cone used to render the SpotLight in degrees.</param>
        /// <param name="fallOff">The fall off of the cone used to render the SpotLight.</param>
        public SpotLight(string id, Vector3 position,
            Color colour, bool castsShadows,
            Vector3 direction, float outerConeAngle,
            float innerConeAngle, float fallOff)
            : base(id, colour, direction, castsShadows)
        {
            this.position = position;

            this.outerConeAngle = MathHelper.ToRadians(outerConeAngle / 2);
            this.innerConeAngle = MathHelper.ToRadians(innerConeAngle / 2);

            this.fallOff = fallOff;
        }

        protected override void _CalcFrustum(float[] splitDepths)
        {
            for (int i = 0; i < splitDepths.Length - 1; i++)
            {
                float min = splitDepths[i];
                float max = splitDepths[i + 1];

                CreateViewProj(min, max, out view, out proj);

                splitVP[i] = view * proj;
            }

            CreateViewProj(splitDepths[0],
                splitDepths[splitDepths.Length - 1],
                out view, out proj);

            rot = EonMathsHelper.RotateToFace(
                    position, direct, Vector3.Up);
        }

        void CreateViewProj(float min, float max,
            out Matrix view, out Matrix projection)
        {
            view = Matrix.CreateLookAt(position, direct * (max - min), Vector3.Up);

            projection = Matrix.CreatePerspectiveFieldOfView(
                EonMathsHelper.QuaterPi, AspectRatio, min, max);
        }
    }
}
