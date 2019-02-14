/* Created 03/06/2013
 * Last Updated: 19/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines a drawable set of images which 
    /// are used to represent an animation.
    /// </summary>
    public sealed class Skeleton : ObjectComponent, IDrawItem, IDispose
    {
        #region Varibles

        SkeletonInfo limbsDeff;

        List<Limb> limbs = new List<Limb>();
        Dictionary<string, Transformation> localTransforms = new Dictionary<string, Transformation>();

        List<OrderCache> orders = new List<OrderCache>();

        bool postDraw = false;
        int drawLayer;

        Color colour;

        #endregion
        #region Fields

        /// <summary>
        /// The layer that the Skeleton will be drawn on.
        /// </summary>
        public int DrawLayer { get { return drawLayer; } }

        /// <summary>
        /// The Limbs that make up the Skeleton.
        /// </summary>
        internal List<Limb> Limbs
        {
            get { return limbs; }
        }

        #endregion
        #region Ctors

        /// <summary>
        /// Creates a new Skeleton.
        /// </summary>
        /// <param name="id">The unique identification
        /// name to give to the Skeleton.</param>
        /// <param name="limbsDeffination">The file that contains the 
        /// information about the limbs for the Skeleton.</param>
        /// <param name="drawLayer">The layer to draw the Skeleton on.</param>
        /// <param name="colour">The colour to draw the Skeleton.</param>
        /// <param name="postDraw">Wheather or not the skeleton should 
        /// be drawn after everthing else.</param>
        public Skeleton(string id, SkeletonInfo limbsDeffination)
            : base(id)
        {
            this.limbsDeff = limbsDeffination;
            this.drawLayer = limbsDeffination.DrawLayer;

            this.postDraw = limbsDeffination.PostRender;

            colour = new Color(limbsDeffination.R,
                limbsDeffination.G,
                limbsDeffination.B,
                limbsDeffination.A);

            if (postDraw)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        /// <summary>
        /// Creates a new Skeleton.
        /// </summary>
        /// <param name="id">The unique identification 
        /// name to give to the Skeleton.</param>
        /// <param name="skeletonFilePath">The filepath of the 
        /// SkeletonDeff for the Skeleton.</param>
        /// <param name="drawLayer">The layer to draw the Skeleton on.</param>
        public Skeleton(string id, string skeletonFilePath) :
            this(id, Common.ContentBuilder.Load<SkeletonInfo>(skeletonFilePath)) { }

        protected override void Initialize()
        {
            foreach (Limb l in limbsDeff.Limbs)
                AddLimbTextures(l.Name, l.Order, l.TextureFilepath,
                    l.DistortionMapFilepath, l.NormalMapFilepath);

            Found(limbsDeff.Limbs, "");

            if (this.limbs.Count > 0)
            {
                CalculateTransforms();

                orders = orders.OrderBy(o => o.Order).ToList();
            }
            else
                new Error("Skeleton dosn't contain a Limb with no parent.", Seriousness.Error);

            base.Initialize();
        }

        void Found(Limb[] limbs, string parentName)
        {
            for (int i = 0; i < limbs.Length; i++)
            {
                if (limbs[i].ParentLimb == parentName)
                {
                    Transformation parentTransform = GetParentTransform(limbs[i].ParentLimb);
                    Transformation local = limbs[i].Transform.Translate(limbs[i].Offset);

                    Transformation final = Transformation.Compose(parentTransform, local);

                    limbs[i].SetTransform(final);

                    localTransforms.Add(limbs[i].Name, Transformation.Identity);

                    this.limbs.Add(limbs[i]);

                    Found(limbs, limbs[i].Name);
                }
            }
        }

        void AddLimbTextures(string limbName, int order, string textureFilepath,
            string distortionMapFilepath, string normalMapFilepath)
        {
            OrderCache cache = new OrderCache()
            {
                Order = order,
                LimbName = limbName
            };

            cache.Texture = GetTexture(textureFilepath);

            cache.Texture = GetTexture(textureFilepath);
            cache.NormalMap = GetTexture(normalMapFilepath);
            cache.DistortionMap = GetTexture(distortionMapFilepath);

            orders.Add(cache);
        }

        static Texture2D GetTexture(string filepath)
        {
            try
            {
                Texture2D tex = Common.ContentBuilder.Load<Texture2D>(filepath);
                return tex;
            }
            catch
            {
                new Error("Unable to load: " + filepath, Seriousness.Error);
                return new Texture2D(Common.Device, 1, 1);
            }
        }

        #endregion
        #region Updating

        /// <summary>
        /// Sets the Transformation of a Limb. (Post animation)
        /// </summary>
        /// <param name="limbName">The name of the Limb to be Transformed.</param>
        /// <param name="transform">The Transformation to set to.</param>
        internal void SetLimbTransformation(string limbName, Transformation transform)
        {
            for (int i = 0; i < limbs.Count; i++)
                if (limbs[i].Name == limbName)
                    limbs[i].SetTransform(transform);
        }

        /// <summary>
        /// Sets the Transformation of Limbs.
        /// </summary>
        /// <param name="limbAnimations">The individual Limb animations to be applied.</param>
        internal void SetLimbTransformation(LimbAnimationState[] limbAnimations)
        {
            for (int i = 0; i < limbAnimations.Length; i++)
                if (limbAnimations[i].CurrentTransformation != null)
                {
                    string limbName = limbAnimations[i].LimbName;
                    Transformation transform = limbAnimations[i].CurrentTransformation;

                    if (localTransforms.ContainsKey(limbName))
                        localTransforms[limbName] = transform;
                }
        } 

        internal void CalculateTransforms()
        {
            for (int i = 0; i < limbs.Count; i++)
            {
                Limb l = limbs[i];

                Transformation parentTransform = GetParentTransform(l.ParentLimb);
                Transformation local = localTransforms[l.Name].Translate(l.Offset);

                l.SetTransform(Transformation.Compose(parentTransform, local));

                limbs[i] = l;
            }
        }

        Transformation GetParentTransform(string parent)
        {
            Transformation transform = Transformation.Identity;

            transform = (from l in limbs
                         where l.Name == parent
                         select l.Transform).FirstOrDefault();

            if (transform == null)
                return Transformation.Identity;

            return transform;
        }

        #endregion
        #region Gibbing Limbs

        /// <summary>
        /// Finds a Limb in the Skeleton.
        /// </summary>
        /// <param name="limbName">The anme of the Limb to find.</param>
        /// <returns>The found Limb.</returns>
        public Limb FindLimb(string limbName)
        {
            Limb limb = (from l in limbs
                         where l.Name == limbName
                         select l).FirstOrDefault();

            return limb;
        }

        /// <summary>
        /// Finds a Limb in the Skeleton.
        /// </summary>
        /// <param name="limbName">The anme of the Limb to find.</param>
        /// <param name="index">The index of the Limb if found (else index returned = -1).</param>
        /// <returns>The found Limb.</returns>
        public Limb FindLimb(string limbName, out int index)
        {
            Limb l = new Limb();
            index = -1;

            for (int i = 0; i < limbs.Count; i++)
                if (limbs[i].Name == limbName)
                {
                    l = limbs[i];
                    index = i;
                }

            return l;
        }

        /// <summary>
        /// Used to remove a limb from this Skeleton.
        /// </summary>
        /// <param name="limbName">The name of the Limb to be removed.</param>
        public Skeleton RemoveLimb(string limbName, string newSkeletonID)
        {
            SkeletonInfo deff = new SkeletonInfo()
            {
                R = colour.R,
                G = colour.G,
                B = colour.B,
                A = colour.A,
                Limbs = FindGibbedLimbs(limbName),
                PostRender = postDraw,
                DrawLayer = drawLayer
            };

            Skeleton partial = new Skeleton(newSkeletonID, deff);

            return partial;
        }

        Limb[] FindGibbedLimbs(string limbName)
        {
            List<Limb> gibbed = new List<Limb>();

            Limb l = FindLimb(limbName);
            l.ParentLimb = "";

            gibbed.Add(l);
            RemoveLimb(l);

            string currentLimb = limbName;

            bool found = true;

            while (found)
            {
                found = false;

                for (int i = 0; i < limbs.Count; i++)
                    if (limbs[i].Name == currentLimb)
                    {
                        gibbed.Add(limbs[i]);

                        currentLimb = limbs[i].Name;
                        RemoveLimb(limbs[i]);

                        found = true;
                    }
            }

            return gibbed.ToArray();
        }

        void RemoveLimb(Limb limb)
        {
            limbs.Remove(limb);

            OrderCache order = (from ord in orders
                                where ord.LimbName == limb.Name
                                select ord).FirstOrDefault();

            orders.Remove(order);

            localTransforms.Remove(limb.Name);
        }

        #endregion
        #region Drawing

        /// <summary>
        /// Used to draw the Skeleton.
        /// </summary>
        /// <param name="stage">The DrawingStage that defines how
        /// this Skeleton should be drawn.</param>
        public void Draw(DrawingStage stage)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                Limb limb = FindLimb(orders[i].LimbName);

                switch (stage)
                {
                    case DrawingStage.Colour:
                        limb.CalculateBounds(Owner.World);
                        break;
                }

                switch (stage)
                {
                    case DrawingStage.Colour:
                        Common.Batch.Draw(orders[i].Texture, limb.Bounds, null, colour,
                            limb.Transform.Rotation, limb.RotationalPoint, SpriteEffects.None, 0);
                        break;

                    case DrawingStage.Normal:
                        Common.Batch.Draw(orders[i].NormalMap, limb.Bounds, null, Color.White,
                            limb.Transform.Rotation, limb.RotationalPoint, SpriteEffects.None, 0);
                        break;

                    case DrawingStage.Distortion:
                        Common.Batch.Draw(orders[i].DistortionMap, limb.Bounds, null, Color.White,
                            limb.Transform.Rotation, limb.RotationalPoint, SpriteEffects.None, 0);
                        break;
                }
            }
        }

        #endregion
        #region Misc

        /// <summary>
        /// Sets the colour of the Skeleton to be drawn in.
        /// </summary>
        /// <param name="colour">The new colour of the Skeleton.</param>
        public void SetColour(Color colour)
        {
            this.colour = colour;
        }

        /// <summary>
        /// Adds a limb to the Skeleton.
        /// </summary>
        /// <param name="limb">The limb to be added.</param>
        /// <param name="parent">The name of the parent limb.</param>
        public void AddLimb(Limb limb, string parent)
        {
            limb.ParentLimb = parent;

            AddLimbTextures(limb.Name, limb.Order, limb.TextureFilepath,
                limb.DistortionMapFilepath, limb.NormalMapFilepath);

            localTransforms.Add(limb.Name, limb.Transform);

            limbs.Add(limb);

            orders = orders.OrderBy(o => o.Order).ToList();
        }

        /// <summary>
        /// Disposes of the Skeleton.
        /// </summary>
        public void Dispose(bool finalize)
        {
            if (!Destroyed)
            {
                orders.Clear();
                orders = null;

                limbs.Clear();
                limbs = null;

                localTransforms.Clear();
                localTransforms = null;
            }
        }

        #endregion
    }
}
