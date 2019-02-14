/* Created: 02/10/2014
 * Last Updated: 02/10/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles;
using Eon.Particles.D2.Lightning;

/// <summary>
/// A delegate used to signal the death of a Particle.
/// </summary>
/// <param name="particle">The dead Particle.</param>
public delegate void OnDeadParticleEvent(string particleID);

/// <summary>
/// A delegate used to signal the end of a ParticleSystem's simulation.
/// </summary>
/// <param name="particleSystemID">The ParticleSystem that has fininshed it's simulation.</param>
public delegate void OnFinishedEvent(string particleSystemID);

/// <summary>
/// A delegate that is used to signal when it's time to spawn various amounts of objects.
/// </summary>
/// <param name="amount">The amount of particles to be spawned.</param>
public delegate void OnSpawnEvent(int amount);

/// <summary>
/// A delegate used to signal when the Cycle has stopped producing Particles.
/// </summary>
public delegate void OnSpawningCompleteEvent();

/// <summary>
/// A delegate used to signal when a LightningBolt has finished it's demonstration.
/// </summary>
/// <param name="index">The index of the completed LightningBolt.</param>
public delegate void OnLightningComplete(LightningBolt bolt);

/// <summary>
/// Used to signal when an object has been created at an index.
/// </summary>
/// <param name="index">The index of the created object.</param>
public delegate void OnCreatedEvent(int index);
