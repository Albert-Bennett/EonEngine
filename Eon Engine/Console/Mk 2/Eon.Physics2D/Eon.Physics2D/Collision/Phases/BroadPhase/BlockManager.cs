/* Created 05/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.Maths.Helpers;
using Eon.Physics2D.Maths;
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
        const int BLOCK_SIZE = 120;

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
            Vector2 maxBlocks = new Vector2(
                EonMathsHelper.Round(worldSize.Width / BLOCK_SIZE),
                EonMathsHelper.Round(worldSize.Height / BLOCK_SIZE));

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
            List<CollisionComponent> collisionObjs = BroadPhase.GetCollisionObjects();

            for(int i=0;i<collisionObjs.Count;i++)
                for (int b = 0; b < blocks.Count; b++)
                {
                    MTV mtv;

                    if (ConvexShapeCollisionHelper.CheckCollision(blocks[b].Bounds,
                        collisionObjs[i].Bounds, out mtv))
                    {
                        blocks[b].Add(collisionObjs[i]);
                    }
                }
        }

        internal void Dispose()
        {
            for (int i = 0; i < blocks.Count; i++)
                blocks[i].ClearBlock();

            blocks.Clear();
        }
    }
}
