using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace build_alpha_0._2.UI
{
    public class UIButton : UIBase
    {
        public event Action OnHovered;
        public event Action OnClicked;

        public Color ClickColor { get; set; }

        public bool IsHovered { get; private set; }
        public bool IsClicked { get; private set; }

        private UIText buttonText;

        public UIButton() : base()
        {
            ClickColor = Color.White;
            buttonText = new UIText(this, "Button", 12, null);
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }
        public UIButton(UIText text) : base()
        {
            ClickColor = Color.White;
            buttonText = text;
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }
        public UIButton(UIBase parent) : base(parent)
        {
            ClickColor = Color.White;
            buttonText = new UIText(this, "Button", 12, null);
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }
        public UIButton(UIText text, UIBase parent) : base(parent)
        {
            ClickColor = Color.White;
            buttonText = text;
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }

        public void SetText(UIText text)
        {
            buttonText = text;
        }
        public string GetText()
        {
            if (buttonText != null) return buttonText.GetText();
            else return "";
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
                if (OnHovered != null) OnHovered();
                return true;
            }
            else IsHovered = false;

            return false;
        }
        public bool IsClick()
        {
            if (IsHovered && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                shape.FillColor = ClickColor;
                IsClicked = true;
                if (OnClicked != null) OnClicked();
                return true;
            }
            else
            {
                shape.FillColor = DefaultColor;
                IsClicked = false;
            }

            return false;
        }

        public override void OnUpdate()
        {
            if (parent != null)
            {
                shape.Position = new Vector2f(parent.Position.X + Position.X, parent.Position.Y + Position.Y);

                buttonText.Position = new Vector2f(
                    parent.Position.X + ((shape.Size.X / 2) - (buttonText.GetTextLength() * (buttonText.GetCharSize() * 0.23f))),
                    parent.Position.Y + ((shape.Size.Y / 2) - (buttonText.GetCharSize() * 0.25f)));
                buttonText.OnUpdate();
            }
            else
            {
                buttonText.Position = new Vector2f(
                    (shape.Size.X / 2) - (buttonText.GetTextLength() * (buttonText.GetCharSize() * 0.23f)),
                    (shape.Size.Y / 2) - (buttonText.GetCharSize() * 0.25f));
                buttonText.OnUpdate();

                shape.Position = Position;
            }

            IsClick();
        }
        public override void OnRender(RenderTarget target)
        {
            target.Draw(shape);
            buttonText.OnRender(target);
        }
    }
}
