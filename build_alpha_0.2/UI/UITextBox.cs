using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace build_alpha_0._2.UI
{
    public class UITextBox : UIBase
    {
        private Text text;

        public int MaximumLength { get; set; }
                
        public bool IsUsed { get; private set; }
        public Color UsedColor { get; set; }

        private bool IsHovered;

        public UITextBox(uint charSize, Font font) : base()
        {
            text = new Text();
            text.CharacterSize = charSize;
            text.Font = font;
            text.FillColor = Color.Black;
            text.DisplayedString = "";
            MaximumLength = 0;
            UsedColor = Color.White;
            IsHovered = false;
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }
        public UITextBox(UIBase parent, uint charSize, Font font) : base(parent)
        {
            text = new Text();
            text.CharacterSize = charSize;
            text.Font = font;
            text.FillColor = Color.Black;
            text.DisplayedString = "";
            MaximumLength = 0;
            UsedColor = Color.White;
            IsHovered = false;
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }
        public UITextBox(uint charSize, Font font, Color color) : base()
        {
            text = new Text();
            text.CharacterSize = charSize;
            text.Font = font;
            text.FillColor = color;
            text.DisplayedString = "";
            MaximumLength = 0;
            UsedColor = Color.White;
            IsHovered = false;
            shape.OutlineColor = new Color(Convert.ToByte(DefaultColor.R - 40), Convert.ToByte(DefaultColor.G - 40), Convert.ToByte(DefaultColor.B - 40), DefaultColor.A);
        }
        public UITextBox(UIBase parent, uint charSize, Font font, Color color) : base(parent)
        {
            text = new Text();
            text.CharacterSize = charSize;
            text.Font = font;
            text.FillColor = color;
            text.DisplayedString = "";
            MaximumLength = 0;
            UsedColor = Color.White;
            IsHovered = false;
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
                shape.FillColor = UsedColor;
                IsUsed = true;
                return true;
            }
            if (!IsHovered && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                shape.FillColor = DefaultColor;
                IsUsed = false;
                return false;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                shape.FillColor = DefaultColor;
                IsUsed = false;
            }

            return false;
        }

        public bool IsTyping(KeyEventArgs e)
        {
            if (IsUsed)
            {
                if (text.DisplayedString.Length < MaximumLength)
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (e.Code == (Keyboard.Key)i)
                        {
                            text.DisplayedString += $"{(Keyboard.Key)i}";
                            return true;
                        }
                    }
                    for (int i = 26; i < 36; i++)
                    {
                        if (e.Code == (Keyboard.Key)i)
                        {
                            if (text.DisplayedString.Length < MaximumLength)
                            {
                                text.DisplayedString += $"{i - 26}";
                                return true;
                            }
                            else return true;
                        }
                    }
                    if (e.Code == Keyboard.Key.Equal) text.DisplayedString += '=';
                    if (e.Code == Keyboard.Key.LBracket) text.DisplayedString += '[';
                    if (e.Code == Keyboard.Key.RBracket) text.DisplayedString += ']';
                    if (e.Code == Keyboard.Key.SemiColon) text.DisplayedString += ';';
                    if (e.Code == Keyboard.Key.Comma) text.DisplayedString += ',';
                    if (e.Code == Keyboard.Key.Period) text.DisplayedString += '.';
                    if (e.Code == Keyboard.Key.Slash) text.DisplayedString += '/';
                    if (e.Code == Keyboard.Key.BackSlash) text.DisplayedString += '\\';
                    if (e.Code == Keyboard.Key.Quote) text.DisplayedString += '\'';
                    if (e.Code == Keyboard.Key.Tilde) text.DisplayedString += '`';
                    if (e.Code == Keyboard.Key.Dash) text.DisplayedString += '-';

                    if (e.Code == Keyboard.Key.Space) text.DisplayedString += ' ';

                    if (e.Shift)
                    {
                        if (e.Code == Keyboard.Key.Num0) text.DisplayedString += ')';
                        if (e.Code == Keyboard.Key.Num1) text.DisplayedString += '!';
                        if (e.Code == Keyboard.Key.Num2) text.DisplayedString += '@';
                        if (e.Code == Keyboard.Key.Num3) text.DisplayedString += '#';
                        if (e.Code == Keyboard.Key.Num4) text.DisplayedString += '$';
                        if (e.Code == Keyboard.Key.Num5) text.DisplayedString += '%';
                        if (e.Code == Keyboard.Key.Num6) text.DisplayedString += '^';
                        if (e.Code == Keyboard.Key.Num7) text.DisplayedString += '&';
                        if (e.Code == Keyboard.Key.Num8) text.DisplayedString += '*';
                        if (e.Code == Keyboard.Key.Num9) text.DisplayedString += '(';
                        if (e.Code == Keyboard.Key.Equal) text.DisplayedString += '+';
                        if (e.Code == Keyboard.Key.LBracket) text.DisplayedString += '{';
                        if (e.Code == Keyboard.Key.SemiColon) text.DisplayedString += ':';
                        if (e.Code == Keyboard.Key.Comma) text.DisplayedString += '<';
                        if (e.Code == Keyboard.Key.Period) text.DisplayedString += '>';
                        if (e.Code == Keyboard.Key.Slash) text.DisplayedString += '?';
                        if (e.Code == Keyboard.Key.BackSlash) text.DisplayedString += '|';
                        if (e.Code == Keyboard.Key.Quote) text.DisplayedString += '"';
                        if (e.Code == Keyboard.Key.Tilde) text.DisplayedString += '~';
                        if (e.Code == Keyboard.Key.Dash) text.DisplayedString += '_';
                        if (e.Code == Keyboard.Key.RBracket) text.DisplayedString += '}';
                    }
                }

                if (e.Code == Keyboard.Key.BackSpace)
                {
                    string newString = "";
                    for (int i = 0; i < text.DisplayedString.Length - 1; i++)
                        newString += text.DisplayedString[i];
                    text.DisplayedString = newString;
                    return true;
                }
                if (e.Code == Keyboard.Key.Delete) text.DisplayedString = "";
            }

            return false;
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
            {
                text.Position = new Vector2f(parent.Position.X + Position.X, parent.Position.Y + Position.Y);
                shape.Position = new Vector2f(parent.Position.X + Position.X + 1, parent.Position.Y + Position.Y + 1);
            }
            else
            {
                shape.Position = Position;
                text.Position = new Vector2f(Position.X + 1, Position.Y + 1);
            }

            IsClick();
        }
        public override void OnRender(RenderTarget target)
        {
            target.Draw(shape);
            target.Draw(text);
        }
    }
}
