/* Created: 19/05/2014
 * Last Updated: 07/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Shadowing;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// Used to define a light that shines 
    /// infinatly in a single direction.
    /// </summary>
    public class DirectionalLight : LightComponent3D, ICastShadows
    {
        bool castsShadows = false;

        protected float fallOff;
        float aspectRatio;

        protected Matrix[] splitVP = new Matrix[4];
        protected Matrix view;
        protected Matrix proj;

        protected Vector3 direction;

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

        /// <summary>
        /// The direction that the DirectionalLight is facing.
        /// </summary>
        public virtual Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /// <summary>
        /// The view * proj matrix of each split.
        /// </summary>
        public Matrix[] SplitViewProjection
        {
            get { return splitVP; }
        }

        /// <summary>
        /// The fall off of the cone used to render the DirectionalLight.
        /// </summary>
        public float FallOff
        {
            get { return fallOff; }
            set { fallOff = value; }
        }

        /// <summary>
        /// Creates a new DirectionalLight.
        /// </summary>
        /// <param name="id">The id of the DirectionalLight.</param>
        /// <param name="colour">The colour of the DirectionalLight.</param>
        /// <param name="intensity">The intensity of the DirectionalLight.</param>
        /// <param name="direction">The direction of the DirectionalLight.</param>
        /// <param name="castsShadows">Whether or not this DirectionalLight can cast shadows.</param>
        public DirectionalLight(string id, int r, int g, int b,
            float intensity, float directX, float directY, float directZ, bool castsShadows)
            : base(id, r, g, b, intensity)
        {
            this.castsShadows = castsShadows;

            this.direction = new Vector3(directX, directY, directZ);
            direction.Normalize();

            fallOff = 100;

            aspectRatio = Common.Device.Viewport.AspectRatio;

            if (castsShadows)
                shadow = new ShadowEntry(this);
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

            Vector3 d = direction * fallOff;

            view = Matrix.CreateLookAt(centeroid - d, centeroid, Vector3.Up);

            Vector3[] lightCorners = new Vector3[8];

            Vector3.Transform(corners, ref view, lightCorners);

            BoundingBox bounds = BoundingBox.CreateFromPoints(lightCorners);

            proj = Matrix.CreateOrthographicOffCenter(bounds.Min.X,
                  bounds.Max.X, bounds.Min.Y, bounds.Max.Y, -bounds.Max.Z, -bounds.Min.Z);
        }

        protected override void Update()
        {
            UpdateRotation();

            base.Update();
        }

        protected virtual void UpdateRotation()
        {
            World.Rotational = Matrix.CreateLookAt(
                CameraManager.CurrentCamera.Position,
                direction, Vector3.Up);
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
