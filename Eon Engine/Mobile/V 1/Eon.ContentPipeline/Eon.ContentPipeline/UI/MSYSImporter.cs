/* Created 28/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.UI
{
    /// <summary>
    /// Used to import the menu system setup file
    /// </summary>
    [ContentImporter(".MSYS", DisplayName = "Menu System Setup File Importer - Eon Framework", DefaultProcessor = "MSYSProcessor")]
    public class MSYSImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
