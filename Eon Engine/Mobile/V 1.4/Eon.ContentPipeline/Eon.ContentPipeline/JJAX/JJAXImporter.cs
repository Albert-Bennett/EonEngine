/* Created 07/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.JJAX
{
    /// <summary>
    /// Used to import J-JAX files.
    /// </summary>
    [ContentImporter(".JJAX", DisplayName = "J-Jax Importer - Eon Framework", DefaultProcessor = "JJAXProcessor")]
    public class JJAXImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
