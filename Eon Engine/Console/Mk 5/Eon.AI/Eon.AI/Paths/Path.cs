/* Created 26/11/2014
 * Last Updated: 27/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.AI.Paths
{
    /// <summary>
    /// Used to define a path which objects can follow.
    /// </summary>
    public sealed class Path : ObjectComponent
    {
        List<Vector3> nodes;
        List<Vector3> directions = new List<Vector3>();
        List<float> distances = new List<float>();

        float currentDistance = 0;
        float speed;

        bool active = false;

        int currentNode = 0;

        public PathEndedEvent OnPathEnd;

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
            get { return nodes[0]; }
        }

        /// <summary>
        /// The position of the final PathNode.
        /// </summary>
        public Vector3 EndNode
        {
            get { return nodes[nodes.Count - 1]; }
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
        public Path(string id, string pathFilePath, float speed)
            : base(id)
        {
            Type[] extraTypes = new Type[]
            {
                typeof(List<Vector3>)
            };

            PathFile file = SerializationHelper.Deserialize<PathFile>(
                pathFilePath, true, ".Path", extraTypes);

            nodes = file.Nodes;

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Vector3 direction = nodes[i + 1] - nodes[i];

                distances.Add(direction.Length());
                directions.Add(Vector3.Normalize(direction));
            }

            this.speed = speed;
        }

        /// <summary>
        /// Adds a PathNode to the Path. 
        /// </summary>
        /// <param name="node">The PathNode to be added.</param>
        public void AddNode(Vector3 node)
        {
            nodes.Add(node);

            Vector3 direction = node - directions[directions.Count - 1];

            distances.Add(direction.Length());
            directions.Add(Vector3.Normalize(direction));
        }

        /// <summary>
        /// Starts following the Path.
        /// </summary>
        /// <param name="forward">The direction to traverse the Path.</param>
        public void FollowPath()
        {
            active = true;

            currentNode = 0;
        }

        /// <summary>
        /// Stops following the Path.
        /// </summary>
        public void Stop()
        {
            active = false;

            currentNode = 0;
            currentDistance = 0;
        }

        protected override void Update()
        {
            if (active)
            {
                if (currentNode < nodes.Count - 1)
                {
                    Vector3 direction = directions[currentNode];

                    currentDistance += speed;
                    Owner.World.Position += direction * speed;

                    if (currentDistance >= distances[currentNode])
                    {
                        currentDistance -= distances[currentNode];
                        currentNode++;
                    }
                }
                else
                {
                    Stop();

                    if (OnPathEnd != null)
                        OnPathEnd();
                }
            }
        }
    }
}
