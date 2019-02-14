/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.Part
{
    /// <summary>
    /// Used to import ParticleSystems. 
    /// </summary>
    [ContentImporter(".Part", DisplayName = "Particle System Importer - Eon Framework", DefaultProcessor = "ParticleSystemProcessor")]
    public class ParticleSystemImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
