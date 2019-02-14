/* Created 04/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game.LevelManagement;
using Eon.Helpers;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Xml;

namespace Eon.ContentPipeline.Levels
{
    /// <summary>
    /// Used to process level manager files. 
    /// </summary>
    [ContentProcessor(DisplayName = "Level Manager Information Processor - Eon Framework")]
    public class LevelManagerProcessor : ContentProcessor<XmlDocument, LevelManagerInfo>
    {
        LevelManagerInfo output;
        XmlDocument input;

        public override LevelManagerInfo Process(XmlDocument input, ContentProcessorContext context)
        {
            output = new LevelManagerInfo();
            this.input = input;

            if (input.GetElementsByTagName("LevelManager").Count > 0)
            {
                int count = 0;

                if (input.GetElementsByTagName("Info").Count > 0)
                    foreach (XmlNode node in input.GetElementsByTagName("Info")[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "LevelsCount":
                                int.TryParse(node.InnerText, out count);
                                break;
                        }

                TypeHelper.SetXmlDocument(input);
                output.PlayerObject = TypeHelper.GetSerializedObject("Player");

                if (count > 0)
                {
                    output.LevelFilepaths = new string[0];
                    output.LevelName = new string[0];

                    for (int i = 0; i < count; i++)
                        GetLevelFilepath(i);
                }
            }

            return output;
        }

        void GetLevelFilepath(int index)
        {
            string key = "";
            string value = "";

            if (input.GetElementsByTagName("Level" + index).Count > 0)
                foreach (XmlNode node in input.GetElementsByTagName("Level" + index)[0].ChildNodes)
                    switch (node.Name)
                    {
                        case "Filepath":
                            value = node.InnerText;
                            break;

                        case "Name":
                            key = node.InnerText;
                            break;
                    }

            output.LevelName = ArrayHelper.AddItem<string>(key, output.LevelName);
            output.LevelFilepaths = ArrayHelper.AddItem<string>(value, output.LevelFilepaths);
        }
    }
}