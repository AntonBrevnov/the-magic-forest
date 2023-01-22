using SFML.Graphics;
using SFML.System;
using System;

namespace build_alpha_0._2.UI
{
    public abstract class UIBase
    {
        protected RectangleShape shape;

        public Vector2f Position { get; set; }
        public Vector2f Size
        {
            get { return shape.Size; }
            set { shape.Size = value; }
        }       
        public Vector2f Origin
        {
            get { return shape.Origin; }
            set { shape.Origin = value; }
        }

        public Color DefaultColor { get; set; }

        public Texture Texture
        {
            get { return shape.Texture; }
            set { shape.Texture = value; }
        }
        public IntRect TextureRect
        {
            get { return shape.TextureRect; }
            set { shape.TextureRect = value; }
        }

        protected UIBase parent;

        public UIBase()
        {
            shape = new RectangleShape();
            shape.Position = new Vector2f(0, 0);
            shape.Size = new Vector2f(100, 100);
             
            shape.FillColor = Color.White;
            DefaultColor = Color.White;

            shape.OutlineThickness = 2;
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
            
            Position = new Vector2f(0, 0);

            parent = null;
        }
        public UIBase(UIBase parent)
        {
            shape = new RectangleShape();
            shape.Position = new Vector2f(0, 0);
            shape.Size = new Vector2f(100, 100);

            shape.FillColor = Color.White;
            DefaultColor = Color.White;

            this.parent = parent;
        }

        public abstract void OnUpdate();
        public abstract void OnRender(RenderTarget target);
    }
}
