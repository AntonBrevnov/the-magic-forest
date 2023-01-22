using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace build_alpha_0._2.UI
{
    public class UIPanel : UIBase
    {

        public event Action OnSelected;
        
        public Color SelectedColor { get; set; }

        public bool IsHovered { get; private set; }
        public bool IsSelected { get; set; }

        public UIPanel() : base()
        {
            SelectedColor = Color.White;
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }
        public UIPanel(UIBase parent) : base(parent)
        {
            SelectedColor = Color.White;
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }

        public bool IsHover(MouseMoveEventArgs e)
        {
            bool hoverX = false, hoverY = false;

            if (e.X > shape.Position.X && e.X < shape.Position.X + shape.Size.X)
                hoverX = true;
            else hoverX = false;

            if (e.Y > shape.Position.Y && e.Y < shape.Position.Y + shape.Size.Y)
                hoverY = true;
            else hoverY = false;

            if (hoverX && hoverY)
            {
                IsHovered = true;
                return true;
            }
            else IsHovered = false;

            return false;
        }
        public bool IsClick()
        {
            if (IsHovered && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                shape.FillColor = SelectedColor;
                IsSelected = true;
                return true;
            }
            if (!IsHovered && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                shape.FillColor = DefaultColor;
                IsSelected = false;
                if (OnSelected != null) OnSelected();
                return false;
            }

            shape.FillColor = DefaultColor;
            return false;
        }

        public override void OnUpdate()
        {       
            if (parent != null)
                shape.Position = new Vector2f(parent.Position.X + Position.X + 1, parent.Position.Y + Position.Y + 1);
            else
                shape.Position = Position;

            IsClick();
        }
        public override void OnRender(RenderTarget target)
        {
            target.Draw(shape);
        }
    }
}
