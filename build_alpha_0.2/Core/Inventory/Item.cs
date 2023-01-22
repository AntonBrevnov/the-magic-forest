using SFML.Graphics;
using SFML.System;

namespace build_alpha_0._2.Core.Inventory
{
    using Physics;
    using VFX;

    public class Item
    {
        private Sprite sprite;
        public Sprite Sprite 
        {
            get
            {
                return sprite;
            }
            protected set
            {
                sprite = value;
            }
        }

        private Hitbox hitbox;
        public Hitbox Hitbox
        {
            get
            {
                return hitbox;
            }
            protected set
            {
                hitbox = value;
            }
        }

        private RectangleShape shape;
        public RectangleShape Shape
        {
            get { return shape; }
        }

        private Animation animation;
        public Animation Animation
        {
            get { return animation; }
            set
            {
                animation = value;
                animation.PlayAnimation();
            }
        }


        public Vector2f Position
        {
            get { return shape.Position; }
            set { shape.Position = value; }
        }
        public Vector2f Size
        {
            get { return shape.Size; }
            set { shape.Size = value; }
        }
        public Texture Texture
        {
            get { return sprite.Texture; }
            set { sprite.Texture = value; }
        }
        public IntRect TextureRect
        {
            get { return sprite.TextureRect; }
            set { sprite.TextureRect = value; }
        }

        protected ItemID itemId;
        public ItemID ItemId
        {
            get 
            { 
                return itemId; 
            }
        }

        public Item()
        {
            sprite = new Sprite();

            shape = new RectangleShape();
            shape.Size = new Vector2f(24 * sprite.Scale.X, 24 * sprite.Scale.Y);

            hitbox = new Hitbox(shape);

            itemId = ItemID.None;
        }

        public virtual void OnUpdate()
        {
            sprite.Position = shape.Position;
            hitbox.OnUpdate();
        }
        public virtual void OnRender(RenderTarget target, Light light)
        {
            if (light != null)
                target.Draw(sprite, new RenderStates(light.LightShader));
            else
                target.Draw(sprite);
        }
    }
}
