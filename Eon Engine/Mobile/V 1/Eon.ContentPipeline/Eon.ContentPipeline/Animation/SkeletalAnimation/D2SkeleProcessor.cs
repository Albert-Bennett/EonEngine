/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal;
using Eon.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Xml;

namespace Eon.ContentPipeline.Animation.SkeletalAnimation
{
    /// <summary>
    /// Used to process .D2Skele files
    /// </summary>
    [ContentProcessor(DisplayName = "2D Skeleton Processor - Eon Framework")]
    public class D2SkeleProcessor : ContentProcessor<XmlDocument, SkeletonDeff>
    {
        public override SkeletonDeff Process(XmlDocument input, ContentProcessorContext context)
        {
            SkeletonDeff output = new SkeletonDeff();

            if (input.GetElementsByTagName("Skeleton").Count > 0)
            {
                int limbCount = 0;
                Color colour = Color.White;

                if (input.GetElementsByTagName("Data").Count > 0)
                    foreach (XmlNode node in input.GetElementsByTagName("Data")[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "Limbs":
                                int.TryParse(node.InnerText, out limbCount);
                                break;

                            case "Colour":
                                SerializationHelper.GetColour(node.InnerText, out colour);
                                break;
                        }

                output.Colour = colour;
                output.Limbs = new Limb[0];

                if (limbCount > 0)
                    for (int i = 0; i < limbCount; i++)
                    {
                        Limb limb = new Limb();
                        limb.SetTransform(Transformation.Identity);

                        string parent = "_";

                        if (input.GetElementsByTagName("Limb" + i).Count > 0)
                            foreach (XmlNode node in input.GetElementsByTagName("Limb" + i)[0].ChildNodes)
                                switch (node.Name)
                                {
                                    case "Name":
                                        limb.Name = node.InnerText;
                                        break;

                                    case "Parent":
                                        parent = node.InnerText;
                                        break;

                                    case "DrawOrder":
                                        int.TryParse(node.InnerText, out limb.DrawOrder);
                                        break;

                                    case "Texture":
                                        limb.TextureFilepath = node.InnerText;
                                        break;

                                    case "RotationPoint":
                                        SerializationHelper.GetVector2(node.InnerText, out limb.RotationPoint);
                                        break;

                                    case "Offset":
                                        SerializationHelper.GetVector2(node.InnerText, out limb.Offset);
                                        break;

                                    case "Size":
                                        SerializationHelper.GetVector2(node.InnerText, out limb.Size);
                                        break;
                                }

                        limb.ParentLimb = parent;

                        output.Limbs = ArrayHelper.AddItem<Limb>(limb, output.Limbs);
                    }
                else
                    throw new ArgumentNullException("Their are no limbs in this skeleton.");
            }

            return output;
        }
    }
}