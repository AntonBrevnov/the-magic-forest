using build_alpha_0._2.Core;
using build_alpha_0._2.ECS.Components;
using build_alpha_0._2.UI;
using build_alpha_0._2.VFX;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace build_alpha_0._2.ECS.Scenes
{
    public class MainMenu : Scene
    {
        Entity entityBackground;
        RectangleShape shapeBackground;

        UIPanel panel1;
        UIText text1;
        UITextBox nameTextBox;
        UIButton saveNameButton;

        UIPanel panel2;
        UIButton spButton;
        UIButton mpButton;
        UIButton sButton;
        UIButton eButton;

        Sprite cursorSprite;

        public MainMenu(RenderWindow window) : base(window)
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
            panel1.Position = new Vector2f(10, 10);
            panel1.Size = new Vector2f(window.Size.X / 7, window.Size.Y / 12);
            panel1.DefaultColor = new Color(255, 255, 255, 175);

            text1 = new UIText(panel1, Localization.GetParametr("menu_name_block_header"), 12, ResourcesManager.GetFont("ui_font"));
            text1.Position = new Vector2f(2, 2);

            nameTextBox = new UITextBox(panel1, 12, ResourcesManager.GetFont("ui_font"));
            nameTextBox.MaximumLength = 15;
            nameTextBox.Position = new Vector2f(40, 2);
            nameTextBox.Size = new Vector2f(panel1.Size.X * 0.75f, 20);
            nameTextBox.UsedColor = Color.Cyan;
            nameTextBox.SetText(GlobalData.GetTextValue("player_name"));

            saveNameButton = new UIButton(panel1);
            saveNameButton.Position = new Vector2f(10, 30);
            saveNameButton.Size = new Vector2f(panel1.Size.X * 0.9f, 20);
            saveNameButton.ClickColor = Color.Cyan;
            saveNameButton.SetText(new UIText(saveNameButton, Localization.GetParametr("menu_name_block_save_button"), 12, ResourcesManager.GetFont("ui_font")));

            panel2 = new UIPanel();
            panel2.Position = new Vector2f(10, 10 + panel1.Size.Y + 10);
            panel2.Size = new Vector2f(window.Size.X / 7, window.Size.Y / 3.45f);
            panel2.DefaultColor = new Color(255, 255, 255, 175);

            spButton = new UIButton(panel1);
            spButton.Position = new Vector2f(10, 10 + panel1.Size.Y + 10);
            spButton.Size = new Vector2f(panel1.Size.X * 0.9f, 40);
            spButton.ClickColor = Color.Cyan;
            spButton.SetText(new UIText(spButton, Localization.GetParametr("menu_singleplayer_button"), 14, ResourcesManager.GetFont("ui_font")));

            mpButton = new UIButton(panel1);
            mpButton.Position = new Vector2f(10, 60 + panel1.Size.Y + 10);
            mpButton.Size = new Vector2f(panel1.Size.X * 0.9f, 40);
            mpButton.ClickColor = Color.Cyan;
            mpButton.SetText(new UIText(mpButton, Localization.GetParametr("menu_multiplayer_button"), 14, ResourcesManager.GetFont("ui_font")));

            sButton = new UIButton(panel1);
            sButton.Position = new Vector2f(10, 110 + panel1.Size.Y + 10);
            sButton.Size = new Vector2f(panel1.Size.X * 0.9f, 40);
            sButton.ClickColor = Color.Cyan;
            sButton.SetText(new UIText(sButton, Localization.GetParametr("menu_settings_button"), 14, ResourcesManager.GetFont("ui_font")));

            eButton = new UIButton(panel1);
            eButton.Position = new Vector2f(10, 160 + panel1.Size.Y + 10);
            eButton.Size = new Vector2f(panel1.Size.X * 0.9f, 40);
            eButton.ClickColor = Color.Cyan;
            eButton.SetText(new UIText(eButton, Localization.GetParametr("menu_exit_button"), 14, ResourcesManager.GetFont("ui_font")));

            cursorSprite = new Sprite();
            cursorSprite.Texture = ResourcesManager.GetTexture("cursor");
        }

        public override void OnHandleKeyPressed(KeyEventArgs e)
        {
            nameTextBox.IsTyping(e);
        }
        public override void OnHandleKeyReleased(KeyEventArgs e)
        {
        }
        public override void OnHandleButtonPressed(MouseButtonEventArgs e)
        {
        }
        public override void OnHandleButtonReleased(MouseButtonEventArgs e)
        {
        }
        public override void OnHandleMouseMoved(MouseMoveEventArgs e)
        {
            nameTextBox.IsHover(e);
            saveNameButton.IsHover(e);

            spButton.IsHover(e);
            mpButton.IsHover(e);
            sButton.IsHover(e);
            eButton.IsHover(e);

            cursorSprite.Position = new Vector2f(e.X, e.Y);
        }
        public override void OnHandleWheelScrolled(MouseWheelScrollEventArgs e)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            panel1.OnUpdate();
            text1.OnUpdate();
            nameTextBox.OnUpdate();
            saveNameButton.OnUpdate();
            if (saveNameButton.IsClicked)
            {
                DataWriter.SaveData("data/player_settings.cfg", "player_name", nameTextBox.GetText());
                GlobalData.ChangeTextValue("player_name", nameTextBox.GetText());
            }
            panel2.OnUpdate();
            spButton.OnUpdate();
            if (spButton.IsClicked) ChangeScene(3);
            mpButton.OnUpdate();
            if (mpButton.IsClicked) ChangeScene(5);
            sButton.OnUpdate();
            if (sButton.IsClicked) ChangeScene(2);
            eButton.OnUpdate();
            if (eButton.IsClicked) window.Close();

            systemManager.OnUpdate(deltaTime);
        }
        public override void OnRender(RenderTarget target)
        {
            target.SetView(new View(new FloatRect(0, 0, window.Size.X, window.Size.Y)));
            target.Draw(shapeBackground);

            panel1.OnRender(target);
            text1.OnRender(target);
            nameTextBox.OnRender(target);
            saveNameButton.OnRender(target);

            panel2.OnRender(target);
            spButton.OnRender(target);
            mpButton.OnRender(target);
            sButton.OnRender(target);
            eButton.OnRender(target);

            target.Draw(cursorSprite);
        }
    }
}
