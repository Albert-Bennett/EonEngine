/* Created 29/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.Tile
{
    /// <summary>
    /// Used to import .Tile files.
    /// </summary>
    [ContentImporter(".Tile", DisplayName = "Tile Map Importer - Eon Framework", DefaultProcessor = "TileMapProcessor")]
    public class TileMapImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
