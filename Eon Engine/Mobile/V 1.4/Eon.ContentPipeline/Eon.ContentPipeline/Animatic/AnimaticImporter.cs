/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.Animatic
{
    /// <summary>
    /// Used to import Animatics(.AMOV) into a game
    /// </summary>
    [ContentImporter(".AMOV", DisplayName = "Animatic Importer - Eon Framework", DefaultProcessor = "AnimaticProcessor")]
    public class AnimaticImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
