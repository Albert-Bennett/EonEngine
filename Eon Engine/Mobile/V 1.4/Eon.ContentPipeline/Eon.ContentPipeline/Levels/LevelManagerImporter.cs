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
    /// Used to import level manager information files.
    /// </summary>
    [ContentImporter(".LvlM", DisplayName = "Level Manager Infomation Importer - Eon Framework", DefaultProcessor = "LevelManagerProcessor")]
    public class LevelManagerImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
