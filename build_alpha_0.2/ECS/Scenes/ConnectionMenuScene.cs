using build_alpha_0._2.Core;
using build_alpha_0._2.Core.Network;
using build_alpha_0._2.ECS.Components;
using build_alpha_0._2.UI;
using build_alpha_0._2.VFX;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace build_alpha_0._2.ECS.Scenes
{
    public class ConnectionMenuScene : Scene
    {
        Entity entityBackground;
        RectangleShape shapeBackground;

        private UIPanel panel1;
        private UIText ipHeader;
        private UITextBox ipTextBox;
        private UIText portHeader;
        private UITextBox portTextBox;
        private UIButton connectButton;

        Sprite cursorSprite;

        public ConnectionMenuScene(RenderWindow window) : base(window)
        {
        }

        public override void OnStart()
        {
            shapeBackground = new RectangleShape(new Vector2f(window.Size.X, window.Size.Y));

            entityBackground = systemManager.CreateEntity("bg_entity");

            Animation animation = new Animation();
            animation.SetAnimatable(shapeBackground);
            animation.SetSpeed(5);
            for (int i = 0; i < 34; i++)
                animation.AddFrame(ResourcesManager.GetTexture($"bg_texture_{i + 1}"));

            AnimationComponent component = new AnimationComponent(new Animator());
            component.AddAnimation("bg_animation", animation);
            component.SetCurrentAnimation("bg_animation");
            component.PlayCurrentAnimation();

            systemManager.AddComponent(entityBackground, component);

            panel1 = new UIPanel();
            panel1.Size = new Vector2f(160, 160);
            panel1.Position = new Vector2f(window.Size.X / 2 - panel1.Size.Y / 2, window.Size.Y / 2 - panel1.Size.Y);
            panel1.DefaultColor = new Color(255, 255, 255, 175);

            ipHeader = new UIText(panel1, "IP", 14, ResourcesManager.GetFont("ui_font"));
            ipHeader.Position = new Vector2f(10, 10);

            ipTextBox = new UITextBox(panel1, 12, ResourcesManager.GetFont("ui_font"));
            ipTextBox.Size = new Vector2f(140, 20);
            ipTextBox.Position = new Vector2f(10, 30);
            ipTextBox.MaximumLength = 32;
            ipTextBox.UsedColor = Color.Cyan;

            portHeader = new UIText(panel1, "Port", 14, ResourcesManager.GetFont("ui_font"));
            portHeader.Position = new Vector2f(10, 60);

            portTextBox = new UITextBox(panel1, 12, ResourcesManager.GetFont("ui_font"));
            portTextBox.Size = new Vector2f(140, 20);
            portTextBox.Position = new Vector2f(10, 90);
            portTextBox.MaximumLength = 32;
            portTextBox.UsedColor = Color.Cyan;

            connectButton = new UIButton(panel1);
            connectButton.Size = new Vector2f(140, 30);
            connectButton.Position = new Vector2f(10, 120);
            connectButton.ClickColor = Color.Cyan;
            connectButton.OnClicked += connectButton_Click;
            connectButton.SetText(new UIText(connectButton, "Connect", 14, ResourcesManager.GetFont("ui_font")));
            connectButton.OnUpdate();

            cursorSprite = new Sprite();
            cursorSprite.Texture = ResourcesManager.GetTexture("cursor");
        }

        private void connectButton_Click()
        {
            if (ipTextBox.GetText() != "" || ipTextBox.GetText() != null ||
                portTextBox.GetText() != "" || portTextBox.GetText() != null)
            {
                GlobalData.AddTextVariable("ip_address", ipTextBox.GetText());
                GlobalData.AddTextVariable("port", portTextBox.GetText());
                ChangeScene(6);
            }
        }

        public override void OnHandleKeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                ChangeScene(1);

            ipTextBox.IsTyping(e);
            portTextBox.IsTyping(e);
        }
        public override void OnHandleKeyReleased(KeyEventArgs e)
        {            
        }
        public override void OnHandleButtonPressed(MouseButtonEventArgs e)
        {
            connectButton.OnUpdate();
        }
        public override void OnHandleButtonReleased(MouseButtonEventArgs e)
        {            
        }
        public override void OnHandleMouseMoved(MouseMoveEventArgs e)
        {
            ipTextBox.IsHover(e);
            portTextBox.IsHover(e);
            connectButton.IsHover(e);

            cursorSprite.Position = new Vector2f(e.X, e.Y);
        }
        public override void OnHandleWheelScrolled(MouseWheelScrollEventArgs e)
        {            
        }

        public override void OnUpdate(float deltaTime)
        {
            panel1.OnUpdate();
            ipHeader.OnUpdate();
            ipTextBox.OnUpdate();
            portHeader.OnUpdate();
            portTextBox.OnUpdate();

            systemManager.OnUpdate(deltaTime);
        }
        public override void OnRender(RenderTarget target)
        {
            target.Draw(shapeBackground);

            panel1.OnRender(target);
            ipHeader.OnRender(target);
            ipTextBox.OnRender(target);
            portTextBox.OnRender(target);
            portHeader.OnRender(target);
            connectButton.OnRender(target);

            target.Draw(cursorSprite);
        }
    }
}
