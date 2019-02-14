/* Created 10/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Game;
using Eon.Interfaces;
using Eon.Physics2D.Forces.LocalForces;
using Eon.Physics2D.Particles;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Game2D.Objects
{
    /// <summary>
    /// Used to define a Rope in a game.
    /// </summary>
    public sealed class Rope : ILevelAsset, IUpdate
    {
        Spring[] springs;
        Sprite[] sprites;

        string id;
        int drawLayer;

        string textureFilepath;
        string normalMapFilepath;

        float ropeThickness;

        Vector2 pointOfConnection;
        Vector2 lenght;

        /// <summary>
        /// The id of the Rope.
        /// </summary>
        public string ID
        {
            get { return id; }
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
        {
            this.id = id;
            this.drawLayer = drawLayer;

            this.textureFilepath = textureFilepath;
            this.normalMapFilepath = normalMapFilepath;
            this.ropeThickness = ropeThickness;

            this.pointOfConnection = pointOfConnection;

            lenght = new Vector2(0, totalSpringLenght / smoothness);

            springs = new Spring[smoothness];
            sprites = new Sprite[smoothness];

            for (int i = 0; i < springs.Length; i++)
            {
                Spring spring;

                if (i == 0)
                {
                    ParticleObject start = new ParticleObject(pointOfConnection, Vector2.Zero, springsMass);
                    ParticleObject end = new ParticleObject(pointOfConnection + lenght, Vector2.Zero, springsMass);

                    spring = new Spring(start, end, springsStiffness, lenght.Y, springsFriction);
                }
                else
                {
                    ParticleObject end = new ParticleObject(springs[i - 1].EndParticle.Position + lenght, Vector2.Zero, springsMass);

                    spring = new Spring(springs[i - 1].EndParticle, end, springsStiffness, lenght.Y, springsFriction);
                }

                springs[i] = spring;
            }
        }

        public void LevelTransitionOn(string levelID)
        {
            Vector2 scale = new Vector2(ropeThickness, lenght.Y);
            Vector2 xOffset = new Vector2(ropeThickness / 2, 0);

            for (int i = 0; i < springs.Length; i++)
            {
                Vector2 pos = Vector2.Zero;

                pos = springs[i].StartParticle.Position - xOffset;

                sprites[i] = new Sprite(id + "Sprite" + i, drawLayer,
                    textureFilepath, Color.White, false, normalMapFilepath,
                    "Eon/Textures/DefaultDistortionMap", pos, scale, 0, SpriteEffects.None);
            }
        }

        public void LevelTransitionOff(string levelID)
        {
            for (int i = 0; i < springs.Length; i++)
            {
                springs[i].Dispose();
                sprites[i].Destroy();
            }
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
    }
}
