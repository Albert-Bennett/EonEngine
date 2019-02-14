/* Created 11/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Eon.Interfaces;
using Eon.Physics2D.Collision;
using Eon.Physics2D.Math;
using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Used to define a collection of CollisionObjects 
    /// that are used to check for collisions and respond to them.
    /// </summary>
    public sealed class CollisionSkeleton : ObjectComponent, IUpdate, IPostUpdate
    {
        List<CollisionObject> collisionBodies =
            new List<CollisionObject>();

        Vector2 prevParticlePos;
        Vector2 deltaPos;

        ParticleObject particle;

        Skeleton refferenceSkeleton;

        float mass;
        float area;
        string centerLimbID;

        /// <summary>
        /// Creates a new CollisionSkeleton.
        /// </summary>
        /// <param name="id">The ID of this CollisionSkeleton.</param>
        /// <param name="refferenceSkeleton">The Skeleton to use as a reference 
        /// on how to change the positioning of the CollisionSkeleton.</param>
        public CollisionSkeleton(string id, Skeleton refferenceSkeleton, string centerLimbID, float mass)
            : base(id)
        {
            this.refferenceSkeleton = refferenceSkeleton;

            this.centerLimbID = centerLimbID;
            this.mass = mass;
        }

        protected override void Initialize()
        {
            List<Limb> limbs = refferenceSkeleton.Limbs;

            for (int i = 0; i < limbs.Count; i++)
                collisionBodies.Add(new CollisionObject(ID +
                     limbs[i].Name + "Collide", GetBounds(limbs[i])));

            GetArea();

            particle = new ParticleObject(
                GetPosition(), Vector2.Zero, mass, area);

            prevParticlePos = particle.Position;

            base.Initialize();
        }

        Vector2 GetPosition()
        {
            int index = 0;

            Limb limb = refferenceSkeleton.FindLimb(centerLimbID, out index);

            if (index != -1)
                return ((BoundingCircle)
                    collisionBodies[index].Bounds).Center;

            return Vector2.Zero;
        }

        void GetArea()
        {
            area = 0;

            foreach (CollisionObject obj in collisionBodies)
                area += ((BoundingCircle)obj.Bounds).Area;
        }

        BoundingCircle GetBounds(Limb limb)
        {
            Vector2 center = new Vector2(limb.Transform.Position.X, limb.Transform.Position.Y);

            float radius = limb.Size.X + limb.Size.Y;
            radius /= 2;

            if (Owner != null)
            {
                center.X += Owner.World.Translation.X;
                center.Y += Owner.World.Translation.Y;

                Vector3 scl = Vector3.One;
                EonMathHelper.GetMatrixScale(Owner.World, out scl);

                radius *= (scl.X + scl.Y) / 2;
            }

            radius *= (limb.Transform.Scale.X + limb.Transform.Scale.Y) / 2;

            center += new Vector2(radius, radius);

            return new BoundingCircle(center, radius);
        }

        public void _Update()
        {
            List<Limb> limbs = refferenceSkeleton.Limbs;

            deltaPos = particle.Position - prevParticlePos;

            for (int i = 0; i < limbs.Count; i++)
            {
                BoundingCircle bounds = GetBounds(limbs[i]);
                bounds.Center += deltaPos;

                collisionBodies[i].SetBounds(bounds);
            }

            prevParticlePos = particle.Position;
        }

        public void _PostUpdate()
        {
            List<Limb> limbs = refferenceSkeleton.Limbs;

            for (int i = 0; i < collisionBodies.Count; i++)
            {
                Vector2 center = ((BoundingCircle)collisionBodies[i].Bounds).Center;

                Transformation transform = limbs[i].Transform;
                transform.Position += deltaPos;

                refferenceSkeleton.SetLimbTransformation(limbs[i].Name, transform);
            }
        }

        public override void Destroy()
        {
            foreach (CollisionObject obj in collisionBodies)
                obj.Destroy(); 

            collisionBodies.Clear();

            particle.Remove();

            base.Destroy();
        }
    }
}
