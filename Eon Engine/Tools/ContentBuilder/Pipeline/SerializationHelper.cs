/* Created 06/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Xml;

namespace Eon.ContentPipeline
{
    /// <summary>
    /// A helper for serialization.
    /// </summary>
    public static class SerializationHelper
    {
        public static XmlDocument LoadXmlFile(string filename, ContentImporterContext context)
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
