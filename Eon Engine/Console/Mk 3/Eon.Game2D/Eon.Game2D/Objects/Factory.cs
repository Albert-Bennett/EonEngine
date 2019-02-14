/* Created 11/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Game;
using Eon.Particles.Cycles;
using Eon.Particles.D2.EmittionTypes.Base;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Game2D.Misc
{
    /// <summary>
    /// Used to define a factory for spawning objects.
    /// 
    /// A factory is the class that holds a reference for
    /// all of the objects spawned.
    /// </summary>
    public sealed class Factory : ILevelAsset, IUpdate, IDispose, IHoldReferences
    {
        string id;

        List<object> spawnedObjects = new List<object>();

        ParameterCollection spawnObj;
        IntervalCycle cycle;
        IEmitter2D emitter;
        
        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The unique identifaction name to given to the Factory.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new Factory. 
        /// </summary>
        /// <param name="id">The unique identifaction name to give the Factory.</param>
        public Factory(string id, ParameterCollection spawnType,
            ParameterCollection spawnObject, ParameterCollection cycleInfo)
        {
            this.id = id;

            spawnObj = spawnObject;

            object emit = AssemblyManager.CreateInstance(spawnType);

            if (emit != null)
                emitter = emit as IEmitter2D;

            object cyc = AssemblyManager.CreateInstance(cycleInfo);

            if (cyc != null)
            {
                cycle = cyc as IntervalCycle;

                cycle.OnSpawn += new OnSpawnEvent(Spawn);
            }
        }

        public void _Update()
        {
            cycle.Update();
        }

        public void Remove(object obj)
        {
            if (spawnedObjects.Contains(obj))
                spawnedObjects.Remove(obj);
        }

        public void Spawn(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                object obj = AssemblyManager.CreateInstance(spawnObj);

                if (obj != null)
                    if (obj is GameObject)
                    {
                        Vector2 vec = emitter.GeneratePosition();
                        Vector3 trans = new Vector3()
                        {
                            X = vec.X,
                            Y = vec.Y,
                            Z = 0
                        };

                        ((GameObject)obj).World.Position = trans;

                        ((GameObject)obj).AddReference(this);

                        spawnedObjects.Add(obj);
                    }
            }
        }

        public void LevelTransitionOn(string levelID) { }

        public void LevelTransitionOff(string levelID) { }

        public void Dispose(bool finalize)
        {
            for (int i = 0; i < spawnedObjects.Count; i++)
                ((GameObject)spawnedObjects[i]).Destroy();
        }
    }
}
