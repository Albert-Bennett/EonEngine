/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.Particles2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Xml;

namespace Eon.ContentPipeline.Part
{
    /// <summary>
    /// Used to process particle system info files.
    /// </summary>
    [ContentProcessor(DisplayName = "Particle System Processor - Eon Framework")]
    public class ParticleSystemProcessor : ContentProcessor<XmlDocument, ParticleSystemInfo>
    {
        XmlDocument input;

        public override ParticleSystemInfo Process(XmlDocument input, ContentProcessorContext context)
        {
            this.input = input;
            ParticleSystemInfo output = new ParticleSystemInfo();
            TypeHelper.SetXmlDocument(input);

            if (input.GetElementsByTagName("ParticleSystem").Count > 0)
            {
                if (input.GetElementsByTagName("Info").Count > 0)
                {
                    int drawLyr = 0;
                    bool postRender = false;
                    int count = 0;

                    foreach (XmlNode node in input.GetElementsByTagName("Info")[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "PostRender":
                                bool.TryParse(node.InnerText, out postRender);
                                break;

                            case "DrawLayer":
                                int.TryParse(node.InnerText, out drawLyr);
                                break;

                            case "Count":
                                int.TryParse(node.InnerText, out count);
                                break;
                        }

                    output.DrawLayer = drawLyr;
                    output.PostDraw = postRender;

                    if (count > 0)
                    {
                        output.Emitters = new ParticleEmitterInfo[0];

                        for (int i = 0; i < count; i++)
                            output.Emitters = ArrayHelper.AddItem<ParticleEmitterInfo>(ProcessEmitter(i), output.Emitters);
                    }
                }
            }

            return output;
        }

        ParticleEmitterInfo ProcessEmitter(int index)
        {
            ParticleEmitterInfo output = new ParticleEmitterInfo();

            string key = "Emitter" + index;

            if (input.GetElementsByTagName(key).Count > 0)
            {
                int attachments = 0;

                float minLifeTime = 0;
                float maxLifeTime = 0;

                float mass = 0;

                foreach (XmlNode node in input.GetElementsByTagName(key + "Info")[0].ChildNodes)
                    switch (node.Name)
                    {
                        case "ID":
                            output.ID = node.InnerText;
                            break;

                        case "MinLifeTime":
                            float.TryParse(node.InnerText, out minLifeTime);
                            break;

                        case "MaxLifeTime":
                            float.TryParse(node.InnerText, out maxLifeTime);
                            break;

                        case "Mass":
                            float.TryParse(node.InnerText, out mass);
                            break;

                        case "Attachments":
                            int.TryParse(node.InnerText, out attachments);
                            break;
                    }

                output.MinLifeTime = minLifeTime;
                output.MaxLifeTime = maxLifeTime;
                output.Mass = mass;

                output.EmitterType = TypeHelper.GetSerializedObject(key + "EmitterType");
                output.ParticleRenderType = TypeHelper.GetSerializedObject(key + "ParticleRenderer");
                output.CycleType = TypeHelper.GetSerializedObject(key + "Cycle");

                if (attachments > 0)
                {
                    output.Attachments = new ParameterCollection[0];
   
                    for (int i = 0; i < attachments; i++)
                        output.Attachments = ArrayHelper.AddItem<ParameterCollection>(
                            TypeHelper.GetSerializedObject(key + "Attachment" + i), output.Attachments);
                }
            }

            return output;
        }
    }
}