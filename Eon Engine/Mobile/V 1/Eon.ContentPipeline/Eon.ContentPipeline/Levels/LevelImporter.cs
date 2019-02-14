/* Created 04/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.Levels
{
    /// <summary>
    /// Used to import level files.
    /// </summary>
    [ContentImporter(".Lvl", DisplayName = "Level Importer - Eon Framework", DefaultProcessor = "LevelProcessor")]
    public class LevelImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
