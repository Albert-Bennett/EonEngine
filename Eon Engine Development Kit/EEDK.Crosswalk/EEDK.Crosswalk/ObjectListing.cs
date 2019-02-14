/* Created: 02/01/2015
 * Last Updated: 19/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace EEDK.Crosswalk
{
    /// <summary>
    /// Defines a class that defines what objects can be created. 
    /// </summary>
    public sealed class ObjectListing
    {
        /// <summary>
        /// Used to define what objects 
        /// can be created by the LevelEditor.
        /// </summary>
        public EonDictionary<string, string> Objects =
            new EonDictionary<string, string>();

        /// <summary>
        /// The various material shaders that can be created.
        /// </summary>
        public EonDictionary<string, string> Shaders =
            new EonDictionary<string, string>();

        public EonDictionary<string, string> ParticleAttachments =
            new EonDictionary<string, string>();

        public EonDictionary<string, string> ParticleCycles =
            new EonDictionary<string, string>();

        public EonDictionary<string, string> ParticleEmitters2D =
            new EonDictionary<string, string>();

        public EonDictionary<string, string> ParticleEmitters3D =
            new EonDictionary<string, string>();

        public EonDictionary<string, string> ParticleRenderMethods2D =
            new EonDictionary<string, string>();

        public EonDictionary<string, string> ParticleRenderMethods3D =
            new EonDictionary<string, string>();
    }
}
