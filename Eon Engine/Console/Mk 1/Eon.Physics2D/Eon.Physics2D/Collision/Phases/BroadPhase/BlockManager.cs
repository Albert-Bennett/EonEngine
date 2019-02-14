/* Created 05/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.Physics2D.Math;
using Eon.Physics2D.Math.Helpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Physics2D.Collision.Phases.BroadPhase
{
    /// <summary>
    /// Defines a manager class for Blocks.
    /// </summary>
    internal class BlockManager
    {
        const int BLOCK_SIZE = 64;

        List<Block> blocks = new List<Block>();

        /// <summary>
        /// Re-creates the block map with a new world size. 
        /// </summary>
        /// <param name="worldSize">The size of the world.</param>
        internal void ReInitialize(Rectangle worldSize)
        {
            if (blocks.Count > 0)
                blocks.Clear();

            CreateBlocks(worldSize);
        }

        void CreateBlocks(Rectangle worldSize)
        {
            Vector2 bottom = new Vector2(worldSize.X, worldSize.Bottom);

            Vector2 maxBlocks = new Vector2(
                EonMathHelper.Round(worldSize.Width / BLOCK_SIZE),
                EonMathHelper.Round(worldSize.Height / BLOCK_SIZE));

            for (int x = 0; x < maxBlocks.X; x++)
                for (int y = 0; y < maxBlocks.Y; y++)
                    blocks.Add(new Block(x, y, BLOCK_SIZE));
        }

        internal void Update()
        {
            foreach (Block b in blocks)
                b.ClearBlock();

            AddBlockContents();

            EonDictionary<string, string> collidesWith =
                new EonDictionary<string, string>();

            foreach (Block b in blocks)
                if (b.ObjectIDs.Count > 1)
                {
                    string instagator = b.ObjectIDs[0];

                    for (int i = 1; i < b.ObjectIDs.Count; i++)
                        collidesWith.Add(instagator, b.ObjectIDs[i]);
                }

            BroadPhase.SetCollidables(collidesWith);
        }

        void AddBlockContents()
        {
            List<CollisionObject> collisionObjs = BroadPhase.GetCollisionObjects();

            for (int i = 0; i < collisionObjs.Count; i++)
                foreach (Block b in blocks)
                    switch (collisionObjs[i].Shape)
                    {
                        case CollisionShapes.Rectangle:
                            {
                                Vector2 poc;

                                CollisionType collide = CollisionShapeHelper.Collides(
                                    (Rectangle)collisionObjs[i].Bounds, b.Bounds, out poc);

                                if (collide != CollisionType.None)
                                    b.AddID(collisionObjs[i].ID);
                            }
                            break;

                        case CollisionShapes.Circle:
                            {
                                Vector2 poc;

                                CollisionType collide = CollisionShapeHelper.Collides(
                                     b.Bounds, (BoundingCircle)collisionObjs[i].Bounds, out poc);

                                if (collide != CollisionType.None)
                                    b.AddID(collisionObjs[i].ID);
                            }
                            break;
                    }
        }

        internal void Dispose()
        {
            for (int i = 0; i < blocks.Count; i++)
                blocks[i].ObjectIDs.ClearAll();

            blocks.Clear();
        }
    }
}
