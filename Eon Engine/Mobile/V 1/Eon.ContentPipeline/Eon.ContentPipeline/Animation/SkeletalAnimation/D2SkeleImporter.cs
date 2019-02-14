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
    /// Used to import 2D skeletons.
    /// </summary>
    [ContentImporter(".D2Skele", DisplayName = "2D Skeleton Importer - Eon Framework", DefaultProcessor = "D2SkeleProcessor")]
    public class D2SkeleImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
