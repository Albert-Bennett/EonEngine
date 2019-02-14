/* Created: 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Particles.Attachments.Base
{
    /// <summary>
    /// Used to define a pool of Attachments.
    /// </summary>
    public sealed class AttachmentPool
    {
        List<IAttachment> attachments =
            new List<IAttachment>();

        List<PropertySet> properties = new List<PropertySet>();

        internal List<PropertySet> Properties
        {
            get { return properties; }
        }

        public AttachmentPool(ParameterCollection[] attachments)
        {
            if (attachments.Length > 0)
                for (int i = 0; i < attachments.Length; i++)
                {
                    object obj = AssemblyManager.CreateInstance(attachments[i]);

                    if (obj != null)
                        Attach(obj as IAttachment);
                }
        }

        void Attach(IAttachment attachment)
        {
            IAttachment attach = null;

            attach = (from a in attachments
                      where a.ID == attachment.ID
                      select a).FirstOrDefault();

            if (attach == null)
                attachments.Add(attachment);
        }

        internal PropertySet GeneratePropertySet()
        {
            PropertySet prop = new PropertySet();

            for (int i = 0; i < attachments.Count; i++)
            {
                switch (attachments[i].AttachmentType)
                {
                    case AttachmentTypes.Scale:
                        prop.Scale = (float)attachments[i].Generate();
                        break;

                    case AttachmentTypes.Colour:
                        prop.Colour = (Color)attachments[i].Generate();
                        break;

                    case AttachmentTypes.Rotation:
                        prop.Rotation = (Vector3)attachments[i].Generate();
                        break;
                }
            }

            properties.Add(prop);

            return prop;
        }

        internal void Update()
        {
            for (int i = 0; i < properties.Count; i++)
                for (int j = 0; j < attachments.Count; j++)
                    if (attachments[j] is IUpdateAttachment)
                    {
                        switch (attachments[j].AttachmentType)
                        {
                            case AttachmentTypes.Scale:
                                properties[i].Scale = (float)((IUpdateAttachment)attachments[j]).Generate(properties[i].Scale);
                                break;

                            case AttachmentTypes.Colour:
                                properties[i].Colour = (Color)((IUpdateAttachment)attachments[j]).Generate(properties[i].Colour);
                                break;

                            case AttachmentTypes.Rotation:
                                properties[i].Rotation = (Vector3)((IUpdateAttachment)attachments[j]).Generate(properties[i].Rotation);
                                break;
                        }
                    }
        }

        internal void Remove(int index)
        {
            properties.RemoveAt(index);
        }

        public void Reset()
        {
            properties.Clear();
        }
    }
}
