/* Created 26/11/2014
 * Last Updated: 30/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.AI.Paths
{
    /// <summary>
    /// Used to define a path which objects can follow.
    /// </summary>
    public sealed class Path : ObjectComponent, IUpdate
    {
        List<PathNode> nodes;

        float speed;

        bool active = false;
        bool forward;

        int currentNode = 0;
        int nextNode;

        public PathEndedEvent OnPathEnd;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The index of the current PathNode.
        /// </summary>
        public int CurrentNode
        {
            get { return currentNode; }
        }

        /// <summary>
        /// The position of the first PathNode. 
        /// </summary>
        public Vector3 StartNode
        {
            get { return nodes[0].Position; }
        }

        /// <summary>
        /// The position of the final PathNode.
        /// </summary>
        public Vector3 EndNode
        {
            get { return nodes[nodes.Count - 1].Position; }
        }

        /// <summary>
        /// The speed at which the Path is traversed.
        /// </summary>
        public float TarversalSpeed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(value, 0, 1); }
        }

        /// <summary>
        /// Creates a new path for an object to follow.
        /// </summary>
        /// <param name="nodes">The nodes along the Path.</param>
        /// <param name="traversalSpeed">The speed at which the Path is traversed at.</param>
        public Path(string id, List<PathNode> nodes, float traversalSpeed)
            : base(id)
        {
            this.nodes = nodes;
            this.speed = MathHelper.Clamp(traversalSpeed, 0, 1);
        }

        /// <summary>
        /// Creates a new path for an object to follow.
        /// </summary>
        public Path(string id, string pathFilePath)
            : base(id)
        {
            Type[] extraTypes = new Type[]
            {
                typeof(List<PathNode>),
                typeof(Vector3),
                typeof(float),
                typeof(string)
            };

            PathFile file = SerializationHelper.Deserialize<PathFile>(
                pathFilePath, true, ".Path", extraTypes);

            nodes = file.Nodes;
            speed = MathHelper.Clamp(file.TraversalSpeed, 0, 1);
        }

        protected override void Initialize()
        {
            Owner.World.Position = StartNode;

            base.Initialize();
        }

        /// <summary>
        /// Adds a PathNode to the Path. 
        /// </summary>
        /// <param name="node">The PathNode to be added.</param>
        public void AddNode(PathNode node)
        {
            nodes.Add(node);
        }

        /// <summary>
        /// Starts following the Path.
        /// </summary>
        /// <param name="forward">The direction to traverse the Path.</param>
        public void FollowPath(bool forward)
        {
            active = true;

            this.forward = forward;

            currentNode = 0;

            if (forward)
                nextNode = 1;
            else
                nextNode = nodes.Count - 1;
        }

        /// <summary>
        /// Stops following the Path.
        /// </summary>
        public void Stop()
        {
            active = false;
        }

        public void _Update()
        {
            if (active)
            {
                if (forward)
                    Owner.World.Position = Vector3.Lerp(
                        Owner.World.Position, nodes[nextNode].Position, speed);
                else
                    Owner.World.Position = Vector3.Lerp(
                        nodes[nextNode].Position, Owner.World.Position, speed);

                if (Owner.World.Position.Equals(nodes[nextNode].Position))
                {
                    currentNode = nextNode;

                    if (forward)
                    {
                        int temp = currentNode++;

                        if (temp > nodes.Count)
                        {
                            nextNode = 0;

                            if (OnPathEnd != null)
                                OnPathEnd();
                        }
                        else
                            nextNode = temp;
                    }
                    else
                    {
                        int temp = currentNode--;

                        if (temp < 0)
                        {
                            nextNode = nodes.Count - 1;

                            if (OnPathEnd != null)
                                OnPathEnd();
                        }
                        else
                            nextNode = temp;
                    }
                }
            }
        }
    }
}
