/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles2D;

/// <summary>
/// Used to define all of the possible states
/// that a ParticleEmitter can be in.
/// </summary>
public enum ParticleEmitterStates
{
    Paused,
    Playing,
    Finished
}

/// <summary>
/// A delegate used to signal the death of a Particle.
/// </summary>
/// <param name="particle">The dead Particle.</param>
public delegate void OnDeadParticleEvent(Particle particle);

/// <summary>
/// A delegate used to signal the end of a ParticleSystem's simulation.
/// </summary>
/// <param name="particleSystem">The ParticleSystem that has fininshed it's simulation.</param>
public delegate void OnFinishedEvent(ParticleSystem particleSystem);

/// <summary>
/// A delegate that is used to signal when it's time to spawn various amounts of objects.
/// </summary>
public delegate void OnSpawnEvent();

/// <summary>
/// A delegate used to signal the ending of a Cycle's spawning time.
/// </summary>
public delegate void OnSpawnCompleteEvent();

/// <summary>
/// A delegate used to signal the end of a ParticleEmitter's simulation.
/// </summary>
/// <param name="emitter">The ParticleEmitter that has finished it's simulation.</param>
public delegate void OnCompleteEvent(ParticleEmitter emitter);
