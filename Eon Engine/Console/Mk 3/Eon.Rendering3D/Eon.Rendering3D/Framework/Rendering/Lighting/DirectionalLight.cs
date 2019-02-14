/* Created: 19/05/2014
 * Last Updated: 12/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Shadowing;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// Used to define a light that shines 
    /// infinatly in a single direction.
    /// </summary>
    public class DirectionalLight : LightComponent3D, ICastShadows
    {
        static float MIN = 0.00001f;

        bool castsShadows = false;

        protected float fallOff;
        float aspectRatio;

        protected Matrix[] splitVP = new Matrix[4];
        protected Matrix view;
        protected Matrix proj;

        protected Matrix rot;

        ShadowEntry shadow = null;

        public bool CastsShadows
        {
            get { return castsShadows; }
        }

        protected float AspectRatio
        {
            get { return aspectRatio; }
            set { aspectRatio = value; }
        }

        /// <summary>
        /// The view * proj matrix.
        /// </summary>
        public Matrix ViewProjection
        {
            get { return view * proj; }
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            protected set
            {
                base.Enabled = value;

                if (shadow != null)
                    shadow.IsUsed = value;
            }
        }

        /// <summary>
        /// The direction that the DirectionalLight is facing.
        /// </summary>
        public Vector3 Direction
        {
            get { return direct; }
            set
            {
                ZeroOr(Vector3.Normalize(value));
            }
        }

        /// <summary>
        /// The view * proj matrix of each split.
        /// </summary>
        public Matrix[] SplitViewProjection
        {
            get { return splitVP; }
        }

        /// <summary>
        /// The world matrix of the DirectionalLight.
        /// </summary>
        public override Matrix World
        {
            get
            {
                return rot;
            }
        }

        /// <summary>
        /// The fall off of the cone used to render the DirectionalLight.
        /// </summary>
        public float FallOff
        {
            get { return fallOff; }
            set
            {
                fallOff = value;
            }
        }

        /// <summary>
        /// Creates a new DirectionalLight.
        /// </summary>
        /// <param name="id">The id of the DirectionalLight.</param>
        /// <param name="position">The position of the DirectionalLight.</param>
        /// <param name="colour">The colour of the DirectionalLight.</param>
        /// <param name="intensity">The intensity of the DirectionalLight.</param>
        /// <param name="direction">The direction of the DirectionalLight.</param>
        /// <param name="castsShadows">Wheather or not this DirectionalLight can cast shadows.</param>
        public DirectionalLight(string id, Vector3 colour,
            float intensity, Vector3 direction, bool castsShadows)
            : base(id, colour, intensity)
        {
            this.castsShadows = castsShadows;
            ZeroOr(Vector3.Normalize(direction));

            fallOff = 99;

            aspectRatio = Common.Device.Viewport.AspectRatio;

            if (castsShadows)
                shadow = new ShadowEntry(this);
        }

        /// <summary>
        /// Creates a new DirectionalLight.
        /// </summary>
        /// <param name="id">The id of the DirectionalLight.</param>
        /// <param name="position">The position of the DirectionalLight.</param>
        /// <param name="colour">The colour of the DirectionalLight.</param>
        /// <param name="direction">The direction of the DirectionalLight.</param>
        /// <param name="castsShadows">Wheather or not this DirectionalLight can cast shadows.</param>
        public DirectionalLight(string id, Color colour,
             Vector3 direction, bool castsShadows)
            : base(id, colour)
        {
            this.castsShadows = castsShadows;
            ZeroOr(Vector3.Normalize(direction));

            fallOff = 99;

            aspectRatio = Common.Device.Viewport.AspectRatio;

            if (castsShadows)
                shadow = new ShadowEntry(this);
        }

        void ZeroOr(Vector3 value)
        {
            if (value.X == 0)
                value.X = MIN;

            if (value.Y == 0)
                value.Y = MIN;

            if (value.Z == 0)
                value.Z = MIN;

            direct = value;
        }

        public void CalcFrustum(float[] splitDepths)
        {
            _CalcFrustum(splitDepths);
        }

        protected virtual void _CalcFrustum(float[] splitDepths)
        {
            Vector3[] corners = CameraManager.CurrentCamera.Frustum.GetCorners();

            Vector3 centeroid = Vector3.Zero;

            for (int i = 0; i < corners.Length; i++)
                centeroid += corners[i];

            centeroid /= corners.Length;

            Vector3 d = direct * fallOff;

            view = Matrix.CreateLookAt(centeroid - d, centeroid, Vector3.Up);

            Vector3[] lightCorners = new Vector3[8];

            Vector3.Transform(corners, ref view, lightCorners);

            BoundingBox bounds = BoundingBox.CreateFromPoints(lightCorners);

            proj = Matrix.CreateOrthographicOffCenter(bounds.Min.X,
                  bounds.Max.X, bounds.Min.Y, bounds.Max.Y, -bounds.Max.Z, -bounds.Min.Z);

            rot = EonMathsHelper.RotateToFace(
                    CameraManager.CurrentCamera.Position,
                    direct, Vector3.Up);
        }

        public override void Disable()
        {
            if (shadow != null)
                shadow.IsUsed = false;

            base.Disable();
        }

        public override void Enable()
        {
            if (shadow != null)
                shadow.IsUsed = true;

            base.Enable();
        }
    }
}
