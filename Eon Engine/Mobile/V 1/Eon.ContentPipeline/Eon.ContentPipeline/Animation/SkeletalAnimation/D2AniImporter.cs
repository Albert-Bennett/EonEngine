/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.Animation.SkeletalAnimation
{
    /// <summary>
    /// Used to import 2D skeletal animations.
    /// </summary>
    [ContentImporter(".D2Ani", DisplayName = "2D Skeletal Animation Importer - Eon Framework", DefaultProcessor = "D2AniProcessor")]
    public class D2AniImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
