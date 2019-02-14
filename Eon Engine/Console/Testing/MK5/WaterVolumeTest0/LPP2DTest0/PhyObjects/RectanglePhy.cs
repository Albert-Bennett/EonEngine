/* Created: 19/09/2015
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Physics2D;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;

namespace LPP2DTest0.PhyObjects
{
    /// <summary>
    /// Creates a new rectangle phy object.
    /// </summary>
    public sealed class RectanglePhy : GameObject
    {
        Rectangle bounds;
        bool isStatic;

        Sprite sprite;
        PhysicsComponent phy;

        public RectanglePhy(string id,
            Rectangle bounds, bool isStatic)
            : base(id)
        {
            this.isStatic = isStatic;
            this.bounds = bounds;
        }

        protected override void Initialize()
        {
            sprite = new Sprite(ID + "Spr", Color.Red, false,
                new Vector2(bounds.X, bounds.Y), new Vector2(bounds.Width, bounds.Height));

            AttachComponent(sprite);

            phy = new PhysicsComponent(ID + "Phy",
                Eon.Physics2D.Maths.Shapes.Rectangle.FromRectangle(bounds),
                Vector2.Zero, 1.5f, isStatic);

            AttachComponent(phy);

            base.Initialize();
        }
    }
}
