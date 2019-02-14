/* Created 03/06/2013
 * Last Updated: 10/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using Eon.Helpers;
using Eon.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines a drawable set of images which 
    /// are used to represent an animation.
    /// </summary>
    public sealed class Skeleton : ObjectComponent, IUpdate
    {
        #region Varibles

        SkeletonInfo limbsDeff;

        List<Limb> limbs = new List<Limb>();

        bool postDraw = false;
        bool renderDisabled = false;

        #endregion
        #region Fields

        /// <summary>
        /// The Limbs that make up the Skeleton.
        /// </summary>
        internal List<Limb> Limbs
        {
            get { return limbs; }
        }

        public int Priority
        {
            get { return 1; }
        }

        /// <summary>
        /// Does the Skeleton still render if disabled.
        /// </summary>
        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set
            {
                if (renderDisabled != value)
                {
                    for (int i = 0; i < limbs.Count; i++)
                        limbs[i].RenderDisabled = value;

                    renderDisabled = value;
                }
            }
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
            this.postDraw = limbsDeffination.PostRender;
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
            this(id, SerializationHelper.Deserialize<SkeletonInfo>(skeletonFilePath, true, ".Skel",
            new Type[] { typeof(string[]) })) { }

        /// <summary>
        /// Creates a new Skeleton from a sub set of Limbs.
        /// </summary>
        /// <param name="id">The ID of the Skeleton.</param>
        /// <param name="limbs">The Limbs in the Skeleton.</param>
        public Skeleton(string id, Limb[] limbs, bool postRender)
            : base(id)
        {
            this.limbs = new List<Limb>(limbs);
            this.postDraw = postRender;

            for (int i = 0; i < this.limbs.Count; i++)
                SetInitialTransforms("");
        }

        protected override void Initialize()
        {
            if (limbsDeff.Coordinate > -1)
                limbsDeff.Limbs = limbsDeff.Limbs.OrderBy(l => l.DrawLayer).ToArray();

            for (int i = 0; i < limbsDeff.Limbs.Length; i++)
            {
                LimbInfo limbInfo = limbsDeff.Limbs[i];

                if (limbsDeff.Coordinate > -1)
                    limbInfo.DrawLayer = limbsDeff.Coordinate;

                if (limbInfo.AnimateData != null)
                    limbs.Add(new AnimatedLimb(limbInfo, postDraw));
                else
                    limbs.Add(new Limb(limbInfo, postDraw));
            }

            SetInitialTransforms("");

            base.Initialize();
        }

        void SetInitialTransforms(string parentName)
        {
            for (int i = 0; i < limbs.Count; i++)
                if (limbs[i].ParentLimb == parentName)
                {
                    limbs[i].SetTransform(
                        Transformation.Compose(
                        limbs[i].InitialTransform,
                        FindTransform(parentName)));

                    SetInitialTransforms(limbs[i].Name);
                }
        }

        #endregion
        #region Updating

        /// <summary>
        /// Sets the Transformation of Limbs.
        /// </summary>
        /// <param name="limbAnimations">The individual Limb animations to be applied.</param>
        internal void SetLimbTransformation(LimbAnimationState[] limbAnimations)
        {
            _SetLimbTransforms("", limbAnimations);
        }

        void _SetLimbTransforms(string parentName, LimbAnimationState[] frames)
        {
            for (int i = 0; i < limbs.Count; i++)
                if (limbs[i].ParentLimb == parentName)
                {
                    Transformation transform = (from f in frames
                                                where f.LimbName == limbs[i].Name
                                                select f.CurrentTransformation).FirstOrDefault();

                    if (transform == null)
                        transform = Transformation.Identity;

                    Transformation parentTransform = FindTransform(parentName);

                    parentTransform = Transformation.Compose(
                        parentTransform, limbs[i].InitialTransform);

                    limbs[i].SetTransform(Transformation.Compose(
                        parentTransform, transform));

                    _SetLimbTransforms(limbs[i].Name, frames);
                }
        }

        Transformation FindTransform(string limbName)
        {
            Transformation transform = Transformation.Identity;

            transform = (from l in limbs
                         where l.Name == limbName
                         select l.Transform).FirstOrDefault();

            if (transform == null)
                return Transformation.Identity;

            return transform;
        }

        public void _Update()
        {
            for (int i = 0; i < limbs.Count; i++)
                    limbs[i].Update(Owner.World);
        }

        public void _PostUpdate()
        {
            if (Enabled != limbs[0].Enabled)
                for (int i = 0; i < limbs.Count; i++)
                    limbs[i].ToogleEnable();
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
            Limb l = null;
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
        public Skeleton GibSkeleton(string limbName, string newSkeletonID)
        {
            return new Skeleton(newSkeletonID,
                FindGibbedLimbs(limbName), postDraw);
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
            if (limbs.Contains(limb))
            {
                limb.Destroy();

                limbs.Remove(limb);
            }
        }

        #endregion
        #region Misc

        public override void Disable()
        {
            for (int i = 0; i < limbs.Count; i++)
                limbs[i].Enabled = false;

            base.Disable();
        }

        public override void Enable()
        {
            for (int i = 0; i < limbs.Count; i++)
                limbs[i].Enabled = true;

            base.Enable();
        }

        /// <summary>
        /// Adds a limb to the Skeleton.
        /// </summary>
        /// <param name="limb">The limb to be added.</param>
        /// <param name="parent">The name of the parent limb.</param>
        public void AddLimb(Limb limb, string parent)
        {
            limb.ParentLimb = parent;

            limbs.Add(limb);
        }

        public override void Destroy(bool remove)
        {
            if (!Destroyed)
            {
                for (int i = 0; i < limbs.Count; i++)
                    limbs[i].Destroy();

                limbs.Clear();
                limbs = null;
            }

            base.Destroy(remove);
        }

        #endregion
    }
}
