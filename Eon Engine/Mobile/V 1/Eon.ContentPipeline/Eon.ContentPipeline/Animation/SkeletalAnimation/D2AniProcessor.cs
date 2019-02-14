/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal;
using Eon.Animation2D.Skeletal.Animating;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Xml;

namespace Eon.ContentPipeline.Animation.SkeletalAnimation
{
    /// <summary>
    /// Used to process .D2Ani files.
    /// </summary>
    [ContentProcessor(DisplayName = "2D Skeletal Animation Processor - Eon Framework")]
    public class D2AniProcessor : ContentProcessor<XmlDocument, D2SkeletalAnimation>
    {
        public override D2SkeletalAnimation Process(XmlDocument input, ContentProcessorContext context)
        {
            D2SkeletalAnimation output = new D2SkeletalAnimation();

            if (input.GetElementsByTagName("Animation").Count > 0)
            {
                int limbAnimations = 0;

                TimeSpan aniTime = TimeSpan.Zero;
                TimeSpan blendTime = TimeSpan.Zero;

                if (input.GetElementsByTagName("Data").Count > 0)
                    foreach (XmlNode node in input.GetElementsByTagName("Data")[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "Name":
                                output.Name = node.InnerText;
                                break;

                            case "LimbAnimations":
                                int.TryParse(node.InnerText, out limbAnimations);
                                break;

                            case "AnimationTime":
                                SerializationHelper.GetTimeSpan(node.InnerText, out aniTime);
                                break;

                            case "BlendTime":
                                SerializationHelper.GetTimeSpan(node.InnerText, out blendTime);
                                break;
                        }

                output.AnimationTime = (float)aniTime.TotalSeconds;
                output.BlendingTime = (float)blendTime.TotalSeconds;

                output.LimbAnimations = new LimbKeyFrameCollection[limbAnimations];

                for (int i = 0; i < limbAnimations; i++)
                {
                    LimbKeyFrameCollection limbAni = new LimbKeyFrameCollection();
                    int frameCount = 0;

                    TimeSpan frameRate = TimeSpan.Zero;

                    if (input.GetElementsByTagName("LimbAni" + i + "Data").Count > 0)
                        foreach (XmlNode node in input.GetElementsByTagName("LimbAni" + i + "Data")[0].ChildNodes)
                            switch (node.Name)
                            {
                                case "LimbName":
                                    limbAni.LimbName = node.InnerText;
                                    break;

                                case "FrameCount":
                                    int.TryParse(node.InnerText, out frameCount);
                                    break;

                                case "FrameRate":
                                    SerializationHelper.GetTimeSpan(node.InnerText, out frameRate);
                                    break;
                            }

                    limbAni.FrameRate = (float)frameRate.TotalSeconds;

                    if (frameCount > 0)
                        for (int j = 0; j < frameCount; j++)
                        {
                            LimbKeyFrame frame = new LimbKeyFrame();
                            //frame.FrameNumber = j;
                            int frameNum = 0;

                            Transformation transform = Transformation.Identity;

                            if (input.GetElementsByTagName("LimbAni" + i + "Frame" + j).Count > 0)
                                foreach (XmlNode node2 in input.GetElementsByTagName("LimbAni" + i + "Frame" + j)[0].ChildNodes)
                                    switch (node2.Name)
                                    {
                                        case "FrameNum":
                                            int.TryParse(node2.InnerText, out frameNum);
                                            break;

                                        case "Translation":
                                            SerializationHelper.GetVector2(node2.InnerText, out transform.Position);
                                            break;

                                        case "Rotation":
                                            float.TryParse(node2.InnerText, out transform.Rotation);
                                            break;

                                        case "Scale":
                                            SerializationHelper.GetVector2(node2.InnerText, out transform.Scale);
                                            break;
                                    }

                            frame.FrameNumber = frameNum;
                            frame.Transform = transform;

                            limbAni.AddFrame(frame);
                        }

                    output.LimbAnimations[i] = limbAni;
                }
            }

            return output;
        }
    }
}