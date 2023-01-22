using build_alpha_0._2.Core;
using build_alpha_0._2.ECS.Components;
using build_alpha_0._2.UI;
using build_alpha_0._2.VFX;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace build_alpha_0._2.ECS.Scenes
{
    public class SettingsScene : Scene
    {
        Entity entityBackground;
        RectangleShape shapeBackground;

        UIPanel panel1;
        UIText graphicsHeader;

        UIButton firstSizeButton;
        UIButton secondSizeButton;
        UIButton thirdSizeButton;
        UIButton fouthSizeButton;

        UIButton fullscreenButton;
        UIButton windowedButton;

        UIText fpsTextHeader;
        UIText fpsIndicator;
        UIButton minusFpsValue;
        UIButton plusFpsValue;

        UIPanel panel2;
        UIText audioHeader;

        UIText musicTextHeader;
        UIText musicIndicator;
        UIButton minusMusicValue;
        UIButton plusMusicValue;

        UIText soundTextHeader;
        UIText soundIndicator;
        UIButton minusSoundValue;
        UIButton plusSoundValue;

        UIPanel panel3;
        UIText gameHeader;
        UIText localeHeader;
        UIButton engButton;
        UIButton rusButton;

        UIButton saveButton;

        Sprite cursorSprite;

        public SettingsScene(RenderWindow window) : base(window)
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

            // панель 1: настройки графики
            {
                panel1 = new UIPanel();
                panel1.Position = new Vector2f(10, 10);
                panel1.Size = new Vector2f(window.Size.X / 3.5f, window.Size.Y / 4.6f);
                panel1.DefaultColor = new Color(255, 255, 255, 175);

                graphicsHeader = new UIText(panel1, Localization.GetParametr("graphics_block_header"), 14, ResourcesManager.GetFont("ui_font"));
                graphicsHeader.Position = new Vector2f(1, 1);

                firstSizeButton = new UIButton(panel1);
                firstSizeButton.Size = new Vector2f(panel1.Size.X / 4.7f, 30);
                firstSizeButton.Position = new Vector2f(10, 30);
                firstSizeButton.ClickColor = Color.Cyan;
                firstSizeButton.SetText(new UIText(firstSizeButton, Localization.GetParametr("first_resolution_button"), 13, ResourcesManager.GetFont("ui_font")));

                secondSizeButton = new UIButton(panel1);
                secondSizeButton.Size = new Vector2f(panel1.Size.X / 4.7f, 30);
                secondSizeButton.Position = new Vector2f(10 + panel1.Size.X / 4.7f + 10, 30);
                secondSizeButton.ClickColor = Color.Cyan;
                secondSizeButton.SetText(new UIText(secondSizeButton, Localization.GetParametr("second_resolution_button"), 13, ResourcesManager.GetFont("ui_font")));

                thirdSizeButton = new UIButton(panel1);
                thirdSizeButton.Size = new Vector2f(panel1.Size.X / 4.7f, 30);
                thirdSizeButton.Position = new Vector2f(10 + (2 * (panel1.Size.X / 4.7f)) + (2 * 10), 30);
                thirdSizeButton.ClickColor = Color.Cyan;
                thirdSizeButton.SetText(new UIText(thirdSizeButton, Localization.GetParametr("third_resolution_button"), 13, ResourcesManager.GetFont("ui_font")));

                fouthSizeButton = new UIButton(panel1);
                fouthSizeButton.Size = new Vector2f(panel1.Size.X / 4.7f, 30);
                fouthSizeButton.Position = new Vector2f(10 + (3 * (panel1.Size.X / 4.7f)) + (3 * 10), 30);
                fouthSizeButton.ClickColor = Color.Cyan;
                fouthSizeButton.SetText(new UIText(fouthSizeButton, Localization.GetParametr("fouth_resolution_button"), 13, ResourcesManager.GetFont("ui_font")));

                fullscreenButton = new UIButton(panel1);
                fullscreenButton.Size = new Vector2f(panel1.Size.X / 2.1f, 30);
                fullscreenButton.Position = new Vector2f(10, 80);
                fullscreenButton.ClickColor = Color.Cyan;
                fullscreenButton.SetText(new UIText(fullscreenButton, Localization.GetParametr("is_fullscreen_button"), 13, ResourcesManager.GetFont("ui_font")));

                windowedButton = new UIButton(panel1);
                windowedButton.Size = new Vector2f(panel1.Size.X / 3.1f, 30);
                windowedButton.Position = new Vector2f(10 + fullscreenButton.Size.X + 10, 80);
                windowedButton.ClickColor = Color.Cyan;
                windowedButton.SetText(new UIText(windowedButton, Localization.GetParametr("is_windowed_button"), 13, ResourcesManager.GetFont("ui_font")));

                fpsTextHeader = new UIText(panel1, Localization.GetParametr("fps_header"), 14, ResourcesManager.GetFont("ui_font"));
                fpsTextHeader.Position = new Vector2f(10, 130);

                minusFpsValue = new UIButton(panel1);
                minusFpsValue.Size = new Vector2f(30, 30);
                minusFpsValue.Position = new Vector2f(120, 120);
                minusFpsValue.ClickColor = Color.Cyan;
                minusFpsValue.SetText(new UIText(minusFpsValue, "-", 15, ResourcesManager.GetFont("ui_font")));

                fpsIndicator = new UIText(panel1, GraphicsSettings.FrameRate.ToString(), 14, ResourcesManager.GetFont("ui_font"));
                fpsIndicator.Position = new Vector2f(160, 130);

                plusFpsValue = new UIButton(panel1);
                plusFpsValue.Size = new Vector2f(30, 30);
                plusFpsValue.Position = new Vector2f(200, 120);
                plusFpsValue.ClickColor = Color.Cyan;
                plusFpsValue.SetText(new UIText(plusFpsValue, "+", 15, ResourcesManager.GetFont("ui_font")));
            }
            // панекь 2: настройки звука
            {
                panel2 = new UIPanel();
                panel2.Position = new Vector2f(10, 10 + panel1.Size.Y + 10);
                panel2.Size = new Vector2f(window.Size.X / 3.5f, window.Size.Y / 7.5f);
                panel2.DefaultColor = new Color(255, 255, 255, 175);

                audioHeader = new UIText(panel2, Localization.GetParametr("audio_block_header"), 14, ResourcesManager.GetFont("ui_font"));
                audioHeader.Position = new Vector2f(1, 1);

                musicTextHeader = new UIText(panel2, Localization.GetParametr("music_header"), 14, ResourcesManager.GetFont("ui_font"));
                musicTextHeader.Position = new Vector2f(10, 30);

                minusMusicValue = new UIButton(panel2);
                minusMusicValue.Size = new Vector2f(30, 30);
                minusMusicValue.Position = new Vector2f(200, 20);
                minusMusicValue.ClickColor = Color.Cyan;
                minusMusicValue.SetText(new UIText(minusMusicValue, "-", 15, ResourcesManager.GetFont("ui_font")));

                musicIndicator = new UIText(panel2, "", 14, ResourcesManager.GetFont("ui_font"));
                musicIndicator.Position = new Vector2f(240, 30);

                plusMusicValue = new UIButton(panel2);
                plusMusicValue.Size = new Vector2f(30, 30);
                plusMusicValue.Position = new Vector2f(280, 20);
                plusMusicValue.ClickColor = Color.Cyan;
                plusMusicValue.SetText(new UIText(plusMusicValue, "+", 15, ResourcesManager.GetFont("ui_font")));

                soundTextHeader = new UIText(panel2, Localization.GetParametr("sound_header"), 14, ResourcesManager.GetFont("ui_font"));
                soundTextHeader.Position = new Vector2f(10, 70);

                minusSoundValue = new UIButton(panel2);
                minusSoundValue.Size = new Vector2f(30, 30);
                minusSoundValue.Position = new Vector2f(200, 60);
                minusSoundValue.ClickColor = Color.Cyan;
                minusSoundValue.SetText(new UIText(minusSoundValue, "-", 15, ResourcesManager.GetFont("ui_font")));

                soundIndicator = new UIText(panel2, "", 14, ResourcesManager.GetFont("ui_font"));
                soundIndicator.Position = new Vector2f(240, 70);

                plusSoundValue = new UIButton(panel2);
                plusSoundValue.Size = new Vector2f(30, 30);
                plusSoundValue.Position = new Vector2f(280, 60);
                plusSoundValue.ClickColor = Color.Cyan;
                plusSoundValue.SetText(new UIText(plusSoundValue, "+", 15, ResourcesManager.GetFont("ui_font")));
            }
            // панекь 3: настройки игры
            {
                panel3 = new UIPanel();
                panel3.Position = new Vector2f(10, 10 + panel1.Size.Y + 10 + panel2.Size.Y + 10);
                panel3.Size = new Vector2f(window.Size.X / 3.5f, window.Size.Y / 11);
                panel3.DefaultColor = new Color(255, 255, 255, 175);

                gameHeader = new UIText(panel3, Localization.GetParametr("game_block_header"), 14, ResourcesManager.GetFont("ui_font"));
                gameHeader.Position = new Vector2f(1, 1);

                localeHeader = new UIText(panel3, Localization.GetParametr("game_locale_header"), 14, ResourcesManager.GetFont("ui_font"));
                localeHeader.Position = new Vector2f(10, 30);

                engButton = new UIButton(panel3);
                engButton.Size = new Vector2f(70, 30);
                engButton.Position = new Vector2f(160, 20);
                engButton.ClickColor = Color.Cyan;
                engButton.SetText(new UIText(engButton, "English", 14, ResourcesManager.GetFont("ui_font")));

                rusButton = new UIButton(panel3);
                rusButton.Size = new Vector2f(70, 30);
                rusButton.Position = new Vector2f(240, 20);
                rusButton.ClickColor = Color.Cyan;
                rusButton.SetText(new UIText(rusButton, "Русский", 14, ResourcesManager.GetFont("ui_font")));
            }

            saveButton = new UIButton();
            saveButton.Size = new Vector2f(window.Size.X / 3.5f, 40);
            saveButton.Position = new Vector2f(10, 10 + panel1.Size.Y + 10 + panel2.Size.Y + 10 + panel3.Size.Y + 10);
            saveButton.ClickColor = Color.Cyan;
            saveButton.SetText(new UIText(saveButton, Localization.GetParametr("accept_button"), 14, ResourcesManager.GetFont("ui_font")));

            cursorSprite = new Sprite();
            cursorSprite.Texture = ResourcesManager.GetTexture("cursor");
        }

        public override void OnHandleKeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) ChangeScene(1);
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
            firstSizeButton.IsHover(e);
            secondSizeButton.IsHover(e);
            thirdSizeButton.IsHover(e);
            fouthSizeButton.IsHover(e);
            
            fullscreenButton.IsHover(e);
            windowedButton.IsHover(e);
            
            minusFpsValue.IsHover(e);
            plusFpsValue.IsHover(e);

            minusMusicValue.IsHover(e);
            plusMusicValue.IsHover(e);
            minusSoundValue.IsHover(e);
            plusSoundValue.IsHover(e);

            engButton.IsHover(e);
            rusButton.IsHover(e);

            saveButton.IsHover(e);

            cursorSprite.Position = new Vector2f(e.X, e.Y);
        }
        public override void OnHandleWheelScrolled(MouseWheelScrollEventArgs e)
        {
            
        }

        public override void OnUpdate(float deltaTime)
        {
            systemManager.OnUpdate(deltaTime);

            panel1.OnUpdate();
            graphicsHeader.OnUpdate();
            firstSizeButton.OnUpdate();
            if (firstSizeButton.IsClicked)
            {
                GraphicsSettings.WindowSize = new Vector2u(1280, 720);
            }
            secondSizeButton.OnUpdate();
            if (secondSizeButton.IsClicked)
            {
                GraphicsSettings.WindowSize = new Vector2u(1366, 768);
            }
            thirdSizeButton.OnUpdate();
            if (thirdSizeButton.IsClicked)
            {
                GraphicsSettings.WindowSize = new Vector2u(1600, 900);
            }
            fouthSizeButton.OnUpdate();
            if (fouthSizeButton.IsClicked)
            {
                GraphicsSettings.WindowSize = new Vector2u(1920, 1080);
            }
            fullscreenButton.OnUpdate();
            if (fullscreenButton.IsClicked)
            {
                GraphicsSettings.WindowStyle = Styles.Fullscreen;
            }
            windowedButton.OnUpdate();
            if (windowedButton.IsClicked)
            {
                GraphicsSettings.WindowStyle = Styles.Titlebar;
            }
            fpsTextHeader.OnUpdate();
            minusFpsValue.OnUpdate();
            if (minusFpsValue.IsClicked)
            {
                if (GraphicsSettings.FrameRate > 25)
                    GraphicsSettings.FrameRate--;
            }
            fpsIndicator.OnUpdate();
            fpsIndicator.SetText(GraphicsSettings.FrameRate.ToString());
            plusFpsValue.OnUpdate();
            if (plusFpsValue.IsClicked)
            {
                if (GraphicsSettings.FrameRate < 120)
                    GraphicsSettings.FrameRate++;
            }

            panel2.OnUpdate();
            audioHeader.OnUpdate();
            musicTextHeader.OnUpdate();
            minusMusicValue.OnUpdate();
            if (minusMusicValue.IsClicked)
            {
                if (AudioSettings.MusicVolume > 0)
                    AudioSettings.MusicVolume--;
            }
            musicIndicator.OnUpdate();
            musicIndicator.SetText(AudioSettings.MusicVolume.ToString());
            plusMusicValue.OnUpdate();
            if (plusMusicValue.IsClicked)
            {
                if (AudioSettings.MusicVolume < 100)
                    AudioSettings.MusicVolume++;
            }
            minusSoundValue.OnUpdate();
            if (minusSoundValue.IsClicked)
            {
                if (AudioSettings.SoundVolume > 0)
                    AudioSettings.SoundVolume--;
            }
            soundTextHeader.OnUpdate();
            soundIndicator.OnUpdate();
            soundIndicator.SetText(AudioSettings.SoundVolume.ToString());
            plusSoundValue.OnUpdate();
            if (plusSoundValue.IsClicked)
            {
                if (AudioSettings.SoundVolume < 100)
                    AudioSettings.SoundVolume++;
            }

            panel3.OnUpdate();
            gameHeader.OnUpdate();
            localeHeader.OnUpdate();
            engButton.OnUpdate();
            if (engButton.IsClicked)
                Localization.LocaleType = "eng";
            rusButton.OnUpdate();
            if (rusButton.IsClicked)
                Localization.LocaleType = "rus";

            saveButton.OnUpdate();
            if (saveButton.IsClicked)
            {
                DataWriter.SaveData("data/graphics_settings.cfg", "window_width", GraphicsSettings.WindowSize.X);
                DataWriter.SaveData("data/graphics_settings.cfg", "window_height", GraphicsSettings.WindowSize.Y);
                DataWriter.SaveData("data/graphics_settings.cfg", "window_style", (int)GraphicsSettings.WindowStyle);
                DataWriter.SaveData("data/graphics_settings.cfg", "framerate", (int)GraphicsSettings.FrameRate);
                DataWriter.SaveData("data/audio_settings.cfg", "music_volume", AudioSettings.MusicVolume);
                DataWriter.SaveData("data/audio_settings.cfg", "sound_volume", AudioSettings.SoundVolume);
                DataWriter.SaveData("data/locale_settings.cfg", "game_locale", Localization.LocaleType);
                window.Close();
            }
        }
        public override void OnRender(RenderTarget target)
        {
            target.Draw(shapeBackground);

            panel1.OnRender(target);
            graphicsHeader.OnRender(target);
            firstSizeButton.OnRender(target);
            secondSizeButton.OnRender(target);
            thirdSizeButton.OnRender(target);
            fouthSizeButton.OnRender(target);
            fullscreenButton.OnRender(target);
            windowedButton.OnRender(target);
            fpsTextHeader.OnRender(target);
            minusFpsValue.OnRender(target);
            fpsIndicator.OnRender(target);
            plusFpsValue.OnRender(target);

            panel2.OnRender(target);
            audioHeader.OnRender(target);
            musicTextHeader.OnRender(target);
            minusMusicValue.OnRender(target);
            musicIndicator.OnRender(target);
            plusMusicValue.OnRender(target);
            soundTextHeader.OnRender(target);
            minusSoundValue.OnRender(target);
            soundIndicator.OnRender(target);
            plusSoundValue.OnRender(target);

            panel3.OnRender(target);
            gameHeader.OnRender(target);
            localeHeader.OnRender(target);
            engButton.OnRender(target);
            rusButton.OnRender(target);

            saveButton.OnRender(target);

            target.Draw(cursorSprite);
        }
    }
}
