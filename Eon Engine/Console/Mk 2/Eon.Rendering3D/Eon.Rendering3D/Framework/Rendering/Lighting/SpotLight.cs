/* Created 20/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering3D.Cameras;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// used to define a SpotLight.
    /// </summary>
    public sealed class SpotLight : LightComponent3D, IUpdate, ICastShadows
    {
        float outerConeAngle;
        float innerConeAngle;
        float fallOff;

        bool castsShadows = false;

        Vector3 baseCenter;

        Matrix view;
        Matrix proj;
        BoundingFrustum viewFrustum;

        public override Vector3 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;

                CalculateView();
                GenerateFrustum();
            }
        }

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Wheather or not this SpotLight can cast shadows.
        /// </summary>
        public bool CastsShadows
        {
            get { return castsShadows; }
        }


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
        /// The fall off of the cone used to render the SpotLight.
        /// </summary>
        public float FallOff
        {
            get { return fallOff; }
            set
            {
                fallOff = value;

                CalculateView();
                GenerateProjection();
                GenerateFrustum();
            }
        }

        /// <summary>
        /// The direction that the SpotLight is facing.
        /// </summary>
        public override Vector3 Direction
        {
            get { return direct; }
            set
            {
                direct = value;
                direct.Normalize();

                CalculateView();
                GenerateFrustum();
            }
        }

        public override void ToogleEnable()
        {
            base.ToogleEnable();
        }

        /// <summary>
        /// The world matrix of the PointLight.
        /// </summary>
        public override Matrix World
        {
            get
            {
                Matrix offset = Matrix.Identity;

                if (Owner != null)
                    offset = Owner.World.Matrix;

                return Matrix.CreateTranslation(position + offset.Translation);
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
                    pos += Owner.World.Translation;

                BoundingSphere sphere = new BoundingSphere(pos, fallOff);

                return CameraManager.CurrentCamera.IsInView(sphere);
            }
        }

        public Matrix View
        {
            get { return view; }
        }

        public Matrix Proj
        {
            get { return proj; }
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
            : base(id, position, colour, intensity)
        {
            this.direct = direction;
            this.outerConeAngle = MathHelper.ToRadians(outerConeAngle / 2);
            this.innerConeAngle = MathHelper.ToRadians(innerConeAngle / 2);
            this.fallOff = fallOff;

            this.castsShadows = castsShadows;

            CalculateView();
            GenerateProjection();
            GenerateFrustum();
        }

        void CalculateView()
        {
            Vector3 fall = new Vector3(0, 0, fallOff);
            Vector3 target = Vector3.Transform(fall,
                Matrix.CreateFromYawPitchRoll(direct.Y, direct.X, direct.Z));

            view = Matrix.CreateLookAt(position, target, Vector3.Up);
        }

        void GenerateProjection()
        {
            float aspectRatio = Common.Device.Viewport.AspectRatio;

            proj = Matrix.CreatePerspectiveFieldOfView(EonMathsHelper.QuaterPi,
                aspectRatio, 0.001f, fallOff);
        }

        void GenerateFrustum()
        {
            viewFrustum = new BoundingFrustum(view * proj);
        }

        public void _Update()
        {
            baseCenter = position;
            baseCenter.Y -= fallOff;

            Vector3.Transform(baseCenter,
                Matrix.CreateFromYawPitchRoll(direct.Y, direct.X, direct.Z));
        }

        /// <summary>
        /// Wheather or not this SpotLight 
        /// can be used to cast shadows on a MeshPart.
        /// </summary>
        /// <param name="boundingSphere">The bounding sphere or the MeshPart to check.</param>
        /// <returns>The result of the check.</returns>
        public bool Shadows(BoundingSphere boundingSphere)
        {
            return viewFrustum.Contains(boundingSphere) != ContainmentType.Disjoint;
        }
    }
}
