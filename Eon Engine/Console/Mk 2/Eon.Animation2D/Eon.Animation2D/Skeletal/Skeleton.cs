/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

        SkeletonDeff limbsDeff;

        List<Limb> limbs = new List<Limb>();
        Dictionary<string, Transformation> localTransforms = new Dictionary<string, Transformation>();

        Dictionary<string, Texture2D> normalMaps = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> distortionMaps = new Dictionary<string, Texture2D>();

        SpriteEffects effect = SpriteEffects.None;

        bool postDraw = false;
        int drawLayer;

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

        /// <summary>
        /// The colour to draw the Skeleton.
        /// </summary>
        public Color Colour
        {
            get { return limbsDeff.Colour; }
        }

        /// <summary>
        /// The sprite effect to be applied 
        /// to the Skeleton whilst drawing.
        /// </summary>
        public SpriteEffects Effect
        {
            get { return effect; }
            set { effect = value; }
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
        public Skeleton(string id, SkeletonDeff limbsDeffination,
            int drawLayer, bool postDraw)
            : base(id)
        {
            this.limbsDeff = limbsDeffination;
            this.drawLayer = drawLayer;

            this.postDraw = postDraw;

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
        public Skeleton(string id, string skeletonFilePath, int drawLayer, Color colour, bool postDraw) :
            this(id, Common.ContentManager.Load<SkeletonDeff>(skeletonFilePath),
            drawLayer, postDraw) { }

        /// <summary>
        /// Creates a new Skeleton.
        /// </summary>
        /// <param name="id">The unique identification 
        /// name to give to the Skeleton.</param>
        /// <param name="skeletonFilePath">The filepath of the 
        /// SkeletonDeff for the Skeleton.</param>
        /// <param name="drawLayer">The layer to draw the Skeleton on.</param>
        public Skeleton(string id, string skeletonFilePath, int drawLayer) :
            this(id, Common.ContentManager.Load<SkeletonDeff>(skeletonFilePath),
            drawLayer, false) { }

        protected override void Initialize()
        {
            int curr = 0;
            int count = limbsDeff.Limbs.Length - 1;

            while (count > 0)
            {
                foreach (Limb l in limbsDeff.Limbs)
                    if (l.DrawOrder == curr)
                    {
                        AddLimb(l);
                        count--;
                    }

                curr++;
            }

            base.Initialize();
        }

        void AddLimb(Limb limb)
        {
            try
            {
                textures.Add(limb.Name, Common.ContentManager.Load<Texture2D>(limb.TextureFilepath));
                normalMaps.Add(limb.Name, Common.ContentManager.Load<Texture2D>(limb.NormalMapFilepath));
                distortionMaps.Add(limb.Name, Common.ContentManager.Load<Texture2D>(limb.DistortionMapFilepath));
            }
            catch
            {
                throw new NullReferenceException("One or more of the textures associated with Limb: " +
                    limb.Name + " couldn't be loaded.");
            }

            limbs.Add(limb);
        }

        #endregion
        #region Updating

        /// <summary>
        /// Sets the Transformation of Limbs.
        /// </summary>
        /// <param name="limbAnimations">The individual Limb animations to be applied.</param>
        internal void SetLimbTransformation(LimbAnimationState[] limbAnimations)
        {
            for (int i = 0; i < limbAnimations.Length; i++)
            {
                string limbName = limbAnimations[i].LimbName;
                Transformation transform = limbAnimations[i].CurrentTransformation;

                if (localTransforms.ContainsKey(limbName))
                    localTransforms[limbName] = transform;
                else
                    localTransforms.Add(limbName, transform);
            }
        }

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

        internal void CalculateTransforms()
        {
            for (int i = 0; i < limbs.Count; i++)
            {
                Limb l = limbs[i];

                l.SetTransform(Transformation.Compose(
                    GetParentTransform(l.ParentLimb),
                    localTransforms[l.Name].Translate(l.Offset)));

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
            SkeletonDeff deff = new SkeletonDeff()
            {
                Colour = Color.White,
                Limbs = FindGibbedLimbs(limbName)
            };

            Skeleton partial = new Skeleton(newSkeletonID, deff, drawLayer, postDraw);
            partial.Effect = effect;

            return partial;
        }

        Limb[] FindGibbedLimbs(string limbName)
        {
            List<Limb> gibbed = new List<Limb>();

            Limb l = FindLimb(limbName);

            gibbed.Add(l);
            RemoveLimb(l);

            string currentLimb = limbName;

            bool none = false;

            while (none == false)
            {
                int count = 0;

                for (int i = 0; i < limbs.Count; i++)
                    if (limbs[i].ParentLimb == currentLimb)
                    {
                        gibbed.Add(limbs[i]);

                        //Possibly problematic!!
                        currentLimb = limbs[i].Name;

                        RemoveLimb(limbs[i]);

                        count++;
                    }

                if (count == 0)
                    none = true;
            }

            return gibbed.ToArray();
        }

        void RemoveLimb(Limb limb)
        {
            limbs.Remove(limb);

            normalMaps.Remove(limb.Name);
            textures.Remove(limb.Name);
            distortionMaps.Remove(limb.Name);

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
            for (int i = 0; i < limbs.Count; i++)
            {
                Limb limb = limbs[i];

                switch (stage)
                {
                    case DrawingStage.Colour:
                        limb.CalculateBounds(Owner.World.Matrix);
                        break;
                }

                switch (stage)
                {
                    case DrawingStage.Colour:
                        Common.Batch.Draw(textures[limb.Name], limb.Bounds, null, Colour,
                            limb.Transform.Rotation, limb.RotationPoint, effect, 0);
                        break;

                    case DrawingStage.Normal:
                        Common.Batch.Draw(normalMaps[limb.Name], limb.Bounds, null, Color.White,
                            limb.Transform.Rotation, limb.RotationPoint, effect, 0);
                        break;

                    case DrawingStage.Distortion:
                        Common.Batch.Draw(distortionMaps[limb.Name], limb.Bounds, null, Color.White,
                            limb.Transform.Rotation, limb.RotationPoint, effect, 0);
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
            limbsDeff.Colour = colour;
        }

        /// <summary>
        /// Disposes of the Skeleton.
        /// </summary>
        public void Dispose(bool finalize)
        {
            if (textures != null)
            {
                if (finalize)
                    foreach (Texture2D tex in textures.Values)
                        tex.Dispose();

                textures.Clear();
                textures = null;
            }

            if (distortionMaps != null)
            {
                if (finalize)
                    foreach (Texture2D distort in distortionMaps.Values)
                        distort.Dispose();

                distortionMaps.Clear();
                distortionMaps = null;
            }

            if (normalMaps != null)
            {
                if (finalize)
                    foreach (Texture2D normal in normalMaps.Values)
                        normal.Dispose();

                normalMaps.Clear();
                normalMaps = null;
            }

            if (limbs != null)
            {
                limbs.Clear();
                limbs = null;
            }

            if (localTransforms != null)
            {
                localTransforms.Clear();
                localTransforms = null;
            }
        }

        #endregion
    }
}
