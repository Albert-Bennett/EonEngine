/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.AnimaticSystem;
using Eon.Collections;
using Eon.EngineComponents;
using Eon.Helpers;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Eon.ContentPipeline.Animatic
{
    /// <summary>
    /// Used to process Animatics (.AMOV).
    /// </summary>
    [ContentProcessor(DisplayName = "Animatic Processor - Eon Framework")]
    public class AnimaticProcessor : ContentProcessor<XmlDocument, AnimaticInfo>
    {
        AnimaticInfo output;

        public override AnimaticInfo Process(XmlDocument input, ContentProcessorContext context)
        {
            output = new AnimaticInfo();

            if (input.GetElementsByTagName("Animatic").Count > 0)
            {
                int streamCount = 0;
                GameStates activeState = GameStates.Game;

                if (input.GetElementsByTagName("Info").Count > 0)
                    foreach (XmlNode node in input.GetElementsByTagName("Info")[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "Streams":
                                int.TryParse(node.InnerText, out streamCount);
                                break;

                            case "ActiveInState":
                                activeState = SerializationHelper.GetEnumValue<GameStates>(node.InnerText);
                                break;
                        }

                output.ActiveInState = activeState;

                if (streamCount > 0)
                    for (int i = 0; i < streamCount; i++)
                    {
                        StreamInfo info = new StreamInfo();

                        int actionCount = 0;

                        if (input.GetElementsByTagName("Stream" + i).Count > 0)
                            foreach (XmlNode node in input.GetElementsByTagName("Stream" + i)[0].ChildNodes)
                                switch (node.Name)
                                {
                                    case "Actions":
                                        int.TryParse(node.InnerText, out actionCount);
                                        break;
                                }

                        if (actionCount > 0)
                        {
                            TypeHelper.SetXmlDocument(input);

                            for (int a = 0; a < actionCount; a++)
                                info.Actions = ArrayHelper.AddItem<ParameterCollection>(TypeHelper.GetSerializedObject(
                                    "Stream" + i + "Action" + a), info.Actions);
                        }

                        output.Streams = ArrayHelper.AddItem<StreamInfo>(info, output.Streams);
                    }
            }

            return output;
        }
    }
}