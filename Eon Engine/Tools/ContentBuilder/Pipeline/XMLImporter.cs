using System;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace Aeon.Pipeline
{
    /// <summary>
    /// Used to import .XML files.
    /// </summary>
    [ContentImporter(".XML", DisplayName = "(.XML) XML Importer - Aeon Framework")]
    public class XMLImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            XmlDocument file = new XmlDocument();

            try
            {
                file.Load(filename);
            }
            catch (Exception e)
            {
                context.Logger.LogImportantMessage
                    ("The file " + filename + " is not valid: " + e.Message);

                throw e;
            }

            return file;
        }
    }
}
