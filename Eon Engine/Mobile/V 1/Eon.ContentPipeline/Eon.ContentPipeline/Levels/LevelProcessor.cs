/* Created 04/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Game.LevelManagement;
using Eon.Helpers;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.Levels
{
    /// <summary>
    /// Used to process level info files.
    /// </summary>
    [ContentProcessor(DisplayName = "Level Processor - Eon Framework")]
    public class LevelProcessor : ContentProcessor<XmlDocument, LevelInfo>
    {
        public override LevelInfo Process(XmlDocument input, ContentProcessorContext context)
        {
            LevelInfo output = new LevelInfo();
            TypeHelper.SetXmlDocument(input);

            if (input.GetElementsByTagName("Level").Count > 0)
            {
                int count = 0;

                if (input.GetElementsByTagName("Info").Count > 0)
                    foreach (XmlNode node in input.GetElementsByTagName("Info")[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "Name":
                                output.LevelName = node.InnerText;
                                break;

                            case "ObjectCount":
                                int.TryParse(node.InnerText, out count);
                                break;
                        }

                if (count > 0)
                {
                    output.LevelObjects = new ParameterCollection[0];

                    for (int i = 0; i < count; i++)
                        output.LevelObjects = ArrayHelper.AddItem<ParameterCollection>(
                            TypeHelper.GetSerializedObject("Object" + i), output.LevelObjects);
                }
            }

            return output;
        }
    }
}