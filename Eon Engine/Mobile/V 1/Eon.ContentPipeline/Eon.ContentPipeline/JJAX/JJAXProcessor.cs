/* Created 07/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Audio;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Eon.ContentPipeline.JJAX
{
    /// <summary>
    /// Used to process J-Jax files.
    /// </summary>
    [ContentProcessor(DisplayName = "J-Jax Processor - Eon Framework")]
    public class JJAXProcessor : ContentProcessor<XmlDocument, JJax>
    {
        JJax output = new JJax();
        XmlDocument input;

        public override JJax Process(XmlDocument input, ContentProcessorContext context)
        {
            this.input = input;
            TypeHelper.SetXmlDocument(input);

            if (input.GetElementsByTagName("JJax").Count > 0)
            {
                int cueCount = 0;

                if (input.GetElementsByTagName("Info").Count > 0)
                    foreach (XmlNode node in input.GetElementsByTagName("Info")[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "Cues":
                                int.TryParse(node.InnerText, out cueCount);
                                break;

                            case "Categories":
                                output.SoundCategories = GetList(node.InnerText);
                                break;
                        }

                if (cueCount > 0)
                    for (int i = 0; i < cueCount; i++)
                        output.Info.Add(GetCue(i));
            }

            return output;
        }

        CueInfo GetCue(int index)
        {
            CueInfo info = new CueInfo();

            if (input.GetElementsByTagName("Cue" + index).Count > 0)
                foreach (XmlNode node in input.GetElementsByTagName("Cue" + index)[0].ChildNodes)
                {
                    float vol = 0.5f;
                    bool single = false;
                    bool loop = false;

                    switch (node.Name)
                    {
                        case "Name":
                            info.Name = node.InnerText;
                            break;

                        case "Filepath":
                            info.Filepath = node.InnerText;
                            break;

                        case "Category":
                            info.CategoryName = node.InnerText;
                            break;

                        case "Volume":
                            float.TryParse(node.InnerText, out vol);
                            break;

                        case "SingleInstance":
                            bool.TryParse(node.InnerText, out single);
                            break;

                        case "Looping":
                            bool.TryParse(node.InnerText, out loop);
                            break;
                    }

                    info.Volume = vol;
                    info.SingleInstance = single;
                    info.Loop = loop;
                }

            return info;
        }

        List<string> GetList(string compressedList)
        {
            List<string> cate = new List<string>();

            string[] names = compressedList.Split(new char[]
            {
                ','
            }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < names.Length; i++)
                cate.Add(names[i]);

            return cate;
        }
    }
}