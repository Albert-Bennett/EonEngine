/* Created 11/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Interfaces;
using Eon.Physics2D;
using Eon.Physics2D.Math.Shapes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Used to define a collection of CollisionComponents 
    /// that are used to check for collisions and respond to them.
    /// </summary>
    public sealed class CollisionSkeleton : ObjectComponent, IUpdate, IPostUpdate
    {
        List<CollisionComponent> collisionBodies =
            new List<CollisionComponent>();

        List<Vector2> prevCollisionPositions;

        Vector2 prevParticlePos;
        Vector2 deltaPos;

        ParticleComponent particle;

        Skeleton refferenceSkeleton;

        float mass;
        float area;
        string centerLimbID;

        public int Priority
        {
            get { return 1; }
        }

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
            if (Owner != null)
            {
                List<Limb> limbs = refferenceSkeleton.Limbs;

                for (int i = 0; i < limbs.Count; i++)
                {
                    CollisionComponent collide = new CollisionComponent(ID +
                         limbs[i].Name + "Collide", GetBounds(limbs[i]));

                    collisionBodies.Add(collide);
                    Owner.AttachComponent(collide);
                }

                GetArea();

                particle = new ParticleComponent(ID + "Particle",
                    GetPosition(), Vector2.Zero, mass, area);

                Owner.AttachComponent(particle);

                prevParticlePos = particle.Position;
            }

            base.Initialize();
        }

        Vector2 GetPosition()
        {
            int index = 0;

            Limb limb = refferenceSkeleton.FindLimb(centerLimbID, out index);

            if (index != -1)
                return ((Circle)
                    collisionBodies[index].Bounds).Center;

            return Vector2.Zero;
        }

        void GetArea()
        {
            area = 0;

            foreach (CollisionComponent obj in collisionBodies)
                area += ((Circle)obj.Bounds).Area;
        }

        Circle GetBounds(Limb limb)
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

            return new Circle(center, radius);
        }

        public void _Update()
        {
            List<Limb> limbs = refferenceSkeleton.Limbs;

            deltaPos = particle.Position - prevParticlePos;

            for (int i = 0; i < limbs.Count; i++)
            {
                Transformation transform = limbs[i].Transform;
                transform.Position += deltaPos;

                refferenceSkeleton.SetLimbTransformation(limbs[i].Name, transform);

                Circle bounds = GetBounds(limbs[i]);
                collisionBodies[i].SetBounds(bounds);

                prevCollisionPositions[i] = ((Circle)collisionBodies[i].Bounds).Center;
            }

            prevParticlePos = particle.Position;
        }

        public void _PostUpdate()
        {
            List<Limb> limbs = refferenceSkeleton.Limbs;

            for (int i = 0; i < collisionBodies.Count; i++)
            {
                Vector2 center = ((Circle)collisionBodies[i].Bounds).Center;
                Vector2 delta = center - prevCollisionPositions[i];

                Transformation transform = limbs[i].Transform;
                transform.Position += delta;

                refferenceSkeleton.SetLimbTransformation(limbs[i].Name, transform);

                if (limbs[i].Name == centerLimbID)
                    particle.Position = limbs[i].Transform.Position;
            }
        }

        public override void Destroy(bool remove)
        {
            foreach (CollisionComponent obj in collisionBodies)
                obj.Destroy(remove);

            collisionBodies.Clear();

            particle.Destroy(remove);

            base.Destroy(remove);
        }
    }
}
