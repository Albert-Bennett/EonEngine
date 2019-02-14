/* Created: 05/11/2013
 * Last Updated: 25/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Physics2D.Collision.Phases.BroadPhase
{
    /// <summary>
    /// Defines a manager class for Blocks.
    /// </summary>
    internal class BlockManager
    {
        /// <summary>
        /// Size of blocks to check inside of.
        /// </summary>
        static int BLOCK_SIZE = 128;

        static List<Block> blocks = new List<Block>();

        internal static void SetWorldSize(Vector2 size, int blockSize)
        {
            BLOCK_SIZE = blockSize;

            if (blocks.Count > 0)
                blocks.Clear();

            CreateBlocks(size);
        }

        static void CreateBlocks(Vector2 worldSize)
        {
            Vector2 maxBlocks = new Vector2(
                EonMathsHelper.Round(worldSize.X / BLOCK_SIZE),
                EonMathsHelper.Round(worldSize.Y / BLOCK_SIZE));

            for (int x = 0; x < maxBlocks.X; x++)
                for (int y = 0; y < maxBlocks.Y; y++)
                    blocks.Add(new Block(x * BLOCK_SIZE, y * BLOCK_SIZE, BLOCK_SIZE));
        }

        internal void Update()
        {
            AddBlockContents();

            for (int i = 0; i < blocks.Count; i++)
                if (blocks[i].Collides.Count > 1)
                    blocks[i].Resolve();

            //EonDictionary<string, string> collidesWith =
            //    new EonDictionary<string, string>();

            //foreach (Block b in blocks)
            //    if (b.ObjectIDs.Count > 1)
            //    {
            //        string instagator = b.ObjectIDs[0];

            //        for (int i = 1; i < b.ObjectIDs.Count; i++)
            //            collidesWith.Add(instagator, b.ObjectIDs[i]);
            //    }

            //BroadPhase.SetCollidables(collidesWith);
        }

        void AddBlockContents()
        {
            GetSimpleContents();

            //List<PhysicsComponent> collisionObjs = BroadPhase.GetCollisionObjects();

            //for (int i = 0; i < collisionObjs.Count; i++)
            //    for (int b = 0; b < blocks.Count; b++)
            //    {
            //        MTV mtv;

            //        if (ConvexShapeCollisionHelper.CheckCollision(blocks[b].Bounds,
            //            collisionObjs[i].Bounds, out mtv))
            //        {
            //            blocks[b].Add(collisionObjs[i]);
            //        }
            //    }
        }

        void GetSimpleContents()
        {
            List<SimplePhysicsComponent> collisionObjs = BroadPhase.GetSimplCollisionObjects();

            if (collisionObjs.Count > 1)
                for (int i = 0; i < collisionObjs.Count; i++)
                    if (collisionObjs[i] != null)
                    {
                        Vector2 center = collisionObjs[i].Bounds.Center;

                        for (int j = 0; j < blocks.Count; j++)
                        {
                            float distance = Vector2.Distance(center, blocks[j].Center);

                            if (distance / 2 < BLOCK_SIZE)
                                blocks[j].Add(collisionObjs[i]);
                        }
                    }
        }

        internal void Dispose()
        {
            blocks.Clear();
        }
    }
}
