/* Created: 20/05/2014
 * Last Updated: 23/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// used to define a SpotLight.
    /// </summary>
    public sealed class SpotLight : DirectionalLight
    {
        float outerConeAngle;
        float innerConeAngle;

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
        /// The direction of the SpotLight.
        /// </summary>
        public override Vector3 Direction
        {
            get {return base.Direction;}
            set
            {
                base.Direction = value;
                direction.Normalize();
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
                BoundingSphere sphere = new BoundingSphere(World.Position, fallOff);

                return CameraManager.CurrentCamera.IsInView(sphere);
            }
        }

        /// <summary>
        /// Creates a new SpotLight.
        /// </summary>
        /// <param name="id">The id of the SpotLight.</param>
        /// <param name="position">The position of the SpotLight.</param>
        /// <param name="colour">The colour of the SpotLight.</param>
        /// <param name="castShadows">Whether or not the SpotLight should cast shadows.</param>
        /// <param name="direction">The direction that the SpotLight is facing.</param>
        /// <param name="outerConeAngle">The outer angle of the cone used to render the SpotLight in degrees.</param>
        /// <param name="innerConeAngle">The inner angle of the cone used to render the SpotLight in degrees.</param>
        /// <param name="fallOff">The fall off of the cone used to render the SpotLight.</param>
        public SpotLight(string id, float x, float y, float z,
            int r, int g, int b, float intensity,
            float directX, float directY, float directZ,
            bool castsShadows, float outerConeAngle,
            float innerConeAngle, float fallOff)
            : base(id, r, g, b, intensity, directX, directY, directZ, castsShadows)
        {
            World.Position = new Vector3(x, y, z);

            if (outerConeAngle < innerConeAngle)
            {
                float a = outerConeAngle;

                outerConeAngle = innerConeAngle;
                innerConeAngle = a;
            }

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
        }

        protected override void UpdateRotation()
        {
            World.Rotational = Matrix.CreateLookAt(
                World.Position, direction, Vector3.Up);
        }

        void CreateViewProj(float min, float max,
            out Matrix view, out Matrix projection)
        {
            view = Matrix.CreateLookAt(World.Position, direction * (max - min), Vector3.Up);

            projection = Matrix.CreatePerspectiveFieldOfView(
                EonMathsHelper.QuaterPi, AspectRatio, min, max);
        }
    }
}
