using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline
{
    /// <summary>
    /// Used to import .ini files.
    /// </summary>
    [ContentImporter(".Engine", DisplayName = "Engine Importer - Eon Framework", DefaultProcessor = "EngineProcessor")]
    public class EngineImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
