using SFML.Graphics;
using SFML.System;

namespace build_alpha_0._2.UI
{
    public class UIText : UIBase
    {
        private Text text;

        public UIText(string text, uint charSize, Font font) : base()
        {
            this.text = new Text();
            this.text.CharacterSize = charSize;
            this.text.Font = font;
            DefaultColor = Color.Black;
            this.text.FillColor = DefaultColor;
            this.text.DisplayedString = text;
        }
        public UIText(UIBase parent, string text, uint charSize, Font font) : base(parent)
        {
            this.text = new Text();
            this.text.CharacterSize = charSize;
            this.text.Font = font;
            DefaultColor = Color.Black;
            this.text.FillColor = DefaultColor;
            this.text.DisplayedString = text;
        }
        public UIText(string text, uint charSize, Font font, Color color) : base()
        {
            this.text = new Text();
            this.text.CharacterSize = charSize;
            this.text.Font = font;
            DefaultColor = color;
            this.text.FillColor = color;
            this.text.DisplayedString = text;
        }
        public UIText(UIBase parent, string text, uint charSize, Font font, Color color) : base(parent)
        {
            this.text = new Text();
            this.text.CharacterSize = charSize;
            this.text.Font = font;
            DefaultColor = color;
            this.text.FillColor = color;
            this.text.DisplayedString = text;
        }

        public int GetTextLength()
        {
            return text.DisplayedString.Length;
        }
        public uint GetCharSize()
        {
            return text.CharacterSize;
        }

        public void SetText(string text)
        {
            this.text.DisplayedString = text;
        }
        public string GetText()
        {
            return text.DisplayedString;
        }

        public override void OnUpdate()
        {
            if (parent != null)
                text.Position = new Vector2f(parent.Position.X + Position.X, parent.Position.Y + Position.Y);
            else text.Position = Position;
            text.Color = DefaultColor;
        }

        public override void OnRender(RenderTarget target)
        {
            target.Draw(text);
        }
    }
}
