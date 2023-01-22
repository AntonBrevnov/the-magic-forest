using build_alpha_0._2.VFX;
using SFML.Graphics;
using SFML.System;

namespace build_alpha_0._2.Core.WorldLibrary
{
    public class Tile
    {
        private Sprite sprite;
        public Sprite Sprite => sprite;

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

        public Tile()
        {
            sprite = new Sprite();
            shape = new RectangleShape();
            animation = new Animation();
            animation.PauseAnimation();
        }
        public Tile(Animation animation)
        {
            sprite = new Sprite();
            shape = new RectangleShape();
            this.animation = animation;
            animation.PlayAnimation();
        }

        public void OnUpdate(float deltaTime)
        {
            sprite.Position = shape.Position;
            animation.OnUpdate(deltaTime);
        }

        public void OnRender(RenderTarget target, Light light)
        {
            if (light != null) 
                target.Draw(sprite, new RenderStates(light.LightShader));
            else 
                target.Draw(sprite);
        }
    }
}
