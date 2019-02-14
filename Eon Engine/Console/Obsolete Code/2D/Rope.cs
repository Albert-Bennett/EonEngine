/* Created 10/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game;
using Eon.Physics2D;
using Eon.Physics2D.Forces.LocalForces;
using Eon.Rendering2D;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Game2D.Objects
{
    /// <summary>
    /// Used to define a Rope in a game.
    /// </summary>
    public sealed class Rope : ObjectComponent, ILevelAsset, IUpdate
    {
        Spring[] springs;
        Sprite[] sprites;

        int drawLayer;

        string textureFilepath;
        string normalMapFilepath;

        float ropeThickness;
        float springMass;
        float springFriction;
        float springStiffness;

        Vector2 pointOfConnection;
        Vector2 lenght;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new Rope.
        /// </summary>
        /// <param name="id">The ID of the Rope.</param>
        /// <param name="drawLayer">The layer to draw the Rope on.</param>
        /// <param name="smoothness">The smoothnes of the Rope.</param>
        /// <param name="springsMass">The mass of each individual Spring in the Rope.</param>
        /// <param name="springsStiffness">The stiffness of each Spring.</param>
        /// <param name="springsFriction">The friction that each spring applies.</param>
        /// <param name="totalSpringLenght">The total lenght of the Rope.</param>
        /// <param name="pointOfConnection">The point from where the Rope will swing from.</param>
        /// <param name="textureFilepath">The texture filepath fgor each segment of the Rope.</param>
        /// <param name="normalMapFilepath">The normal map texture filepath for each segement of the Rope.</param>
        /// <param name="ropeThickness">The thickness of the Rope.</param>
        public Rope(string id, int drawLayer, int smoothness, float springsMass,
            float springsStiffness, float springsFriction,
            float totalSpringLenght, Vector2 pointOfConnection,
            string textureFilepath, string normalMapFilepath, float ropeThickness)
            : base(id)
        {
            this.drawLayer = drawLayer;

            this.textureFilepath = textureFilepath;
            this.normalMapFilepath = normalMapFilepath;
            this.ropeThickness = ropeThickness;

            this.springMass = springsMass;
            this.springFriction = springsFriction;
            this.springStiffness = springsStiffness;

            this.pointOfConnection = pointOfConnection;

            lenght = new Vector2(0, totalSpringLenght / smoothness);

            springs = new Spring[smoothness];
            sprites = new Sprite[smoothness];
        }

        protected override void Initialize()
        {
            if (Owner != null)
            {
                for (int i = 0; i < springs.Length; i++)
                {
                    Spring spring;

                    if (i == 0)
                    {
                        ParticleComponent start = new ParticleComponent(ID + i + "Start", pointOfConnection, Vector2.Zero, springMass);
                        Owner.AttachComponent(start);

                        ParticleComponent end = new ParticleComponent(ID + i + "End", pointOfConnection + lenght, Vector2.Zero, springMass);
                        Owner.AttachComponent(end);

                        spring = new Spring(start, end, springStiffness, lenght.Y, springFriction);
                    }
                    else
                    {
                        ParticleComponent end = new ParticleComponent(ID + i + "End",
                            springs[i - 1].EndParticle.Position + lenght, Vector2.Zero, springMass);

                        Owner.AttachComponent(end);

                        spring = new Spring(springs[i - 1].EndParticle, end, springStiffness, lenght.Y, springFriction);
                    }

                    springs[i] = spring;

                    Vector2 scale = new Vector2(ropeThickness, lenght.Y);
                    Vector2 xOffset = new Vector2(ropeThickness / 2, 0);

                    Vector2 pos = Vector2.Zero;

                    pos = springs[i].StartParticle.Position - xOffset;

                    Sprite spr = new Sprite(ID + "Sprite" + i, drawLayer,
                        textureFilepath, Color.White, false, normalMapFilepath,
                        "Eon/Textures/DefaultDistortionMap", pos, scale, 0, SpriteEffects.None);

                    sprites[i] = spr;
                    Owner.AttachComponent(spr);
                }
            }

            base.Initialize();
        }

        public void LevelTransitionOn(string levelID) { }

        public void LevelTransitionOff(string levelID)
        {
            Destroy(true);
        }

        public void _Update()
        {
            foreach (Spring s in springs)
                s.CalculateForce();

            for (int i = 0; i < sprites.Length; i++)
            {
                Vector2 diff = springs[i].StartParticle.Position -
                    springs[i].EndParticle.Position;

                float rotation = (float)Math.Atan2(diff.Y, diff.X);

                sprites[i].Rotation = rotation;
            }
        }

        public override void Destroy(bool remove)
        {
            for (int i = 0; i < springs.Length; i++)
            {
                springs[i].Destroy();
                sprites[i].Destroy(true);
            }

            base.Destroy(remove);
        }
    }
}
