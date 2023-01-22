using build_alpha_0._2.Core;
using build_alpha_0._2.Core.WorldLibrary;
using build_alpha_0._2.ECS.Components;
using build_alpha_0._2.UI;
using build_alpha_0._2.VFX;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace build_alpha_0._2.ECS.Scenes
{
    public class ChooseMapScene : Scene
    {
        Entity entityBackground;
        RectangleShape shapeBackground;

        UIPanel panel1;
        UIText chooseHeader;
        List<UIButton> buttonsWorlds;

        UIPanel panel2;
        UIText createHeader;
        UIText nameHeader;
        UITextBox nameWorldTextBox;
        UIButton createButton;

        UIText createWorldIndicator;
        Thread createWorldThread;
        Mutex createWorldMutex;

        Sprite cursorSprite;

        public ChooseMapScene(RenderWindow window) : base(window)
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

            // панель 1: выбор мира
            {
                panel1 = new UIPanel();
                panel1.Size = new Vector2f(window.Size.X / 3, window.Size.Y / 1.5f);
                panel1.Position = new Vector2f(window.Size.X / 2 - panel1.Size.X / 2, window.Size.Y / 2 - panel1.Size.Y / 2 - panel1.Size.Y / 5.5f);
                panel1.DefaultColor = new Color(255, 255, 255, 175);

                chooseHeader = new UIText(panel1, Localization.GetParametr("choose_map_header"), 14, ResourcesManager.GetFont("ui_font"));
                chooseHeader.Position = new Vector2f(1, 1);

                buttonsWorlds = new List<UIButton>();

                int worldsCount = int.Parse(DataReader.LoadData("data/worlds_settings.cfg", "worlds_count"));
                for (int i = 0; i < worldsCount; i++)
                {
                    string worldName = DataReader.LoadData("data/worlds_settings.cfg", $"world_name_{i + 1}");

                    var button = new UIButton(panel1);
                    button.Size = new Vector2f(panel1.Size.X / 1.1f, 30);
                    button.Position = new Vector2f(10, 30 + buttonsWorlds.Count * 30 + 10 * buttonsWorlds.Count);
                    button.ClickColor = Color.Cyan;
                    button.SetText(new UIText(button, worldName, 14, ResourcesManager.GetFont("ui_font")));

                    buttonsWorlds.Add(button);
                }
            }
            // панель 2: создание мира
            {
                panel2 = new UIPanel();
                panel2.Size = new Vector2f(window.Size.X / 3, window.Size.Y / 7.5f);
                panel2.Position = new Vector2f(window.Size.X / 2 - panel2.Size.X / 2, window.Size.Y / 2 + panel2.Size.Y * 2.0f);
                panel2.DefaultColor = new Color(255, 255, 255, 175);

                createHeader = new UIText(panel2, Localization.GetParametr("create_map_header"), 14, ResourcesManager.GetFont("ui_font"));
                createHeader.Position = new Vector2f(1, 1);

                nameHeader = new UIText(panel2, Localization.GetParametr("name_world_header"), 14, ResourcesManager.GetFont("ui_font"));
                nameHeader.Position = new Vector2f(10, 30);

                nameWorldTextBox = new UITextBox(panel2, 12, ResourcesManager.GetFont("ui_font"));
                nameWorldTextBox.MaximumLength = 38;
                nameWorldTextBox.Size = new Vector2f(panel2.Size.X * 0.73f, 20);
                nameWorldTextBox.Position = new Vector2f(90, 30);
                nameWorldTextBox.UsedColor = Color.Cyan;

                createButton = new UIButton(panel2);
                createButton.Size = new Vector2f(100, 30);
                createButton.Position = new Vector2f(panel2.Position.X / 2 - createButton.Size.X / 2, 60);
                createButton.ClickColor = Color.Cyan;
                createButton.SetText(new UIText(createButton, Localization.GetParametr("create_button"), 14, ResourcesManager.GetFont("ui_font")));
                createButton.OnClicked += createButton_Click;
                createButton.OnUpdate();

                createWorldIndicator = new UIText(panel2, "", 12, ResourcesManager.GetFont("ui_text"), Color.Green);
                createWorldIndicator.Position = new Vector2f(panel2.Position.X / 2 - createButton.Size.X / 2, 100);
            }

            createWorldMutex = new Mutex();

            cursorSprite = new Sprite();
            cursorSprite.Texture = ResourcesManager.GetTexture("cursor");
        }

        private void createButton_Click()
        {
            createWorldIndicator.SetText("Generating the world...");
            createWorldMutex = new Mutex();  
            createWorldThread = new Thread(new ParameterizedThreadStart(CreateWorld));
            createWorldThread.Start(nameWorldTextBox.GetText());
        }

        public override void OnHandleKeyPressed(KeyEventArgs e)
        {
            nameWorldTextBox.IsTyping(e);
            if (e.Code == Keyboard.Key.Escape)
                ChangeScene(1);
        }
        public override void OnHandleKeyReleased(KeyEventArgs e)
        {
        }
        public override void OnHandleButtonPressed(MouseButtonEventArgs e)
        {
            createButton.OnUpdate();
        }
        public override void OnHandleButtonReleased(MouseButtonEventArgs e)
        {
            
        }
        public override void OnHandleMouseMoved(MouseMoveEventArgs e)
        {
            nameWorldTextBox.IsHover(e);
            createButton.IsHover(e);
            foreach (var button in buttonsWorlds)
                button.IsHover(e);
            cursorSprite.Position = new Vector2f(e.X, e.Y);
        }
        public override void OnHandleWheelScrolled(MouseWheelScrollEventArgs e)
        {
            
        }

        public override void OnUpdate(float deltaTime)
        {
            systemManager.OnUpdate(deltaTime);

            panel1.OnUpdate();
            chooseHeader.OnUpdate();
            foreach (var button in buttonsWorlds)
            {
                button.OnUpdate();
                if (button.IsClicked)
                {
                    GlobalData.AddTextVariable("load_wolrd_name", button.GetText());
                    ChangeScene(4);
                }
            }

            panel2.OnUpdate();
            createHeader.OnUpdate();
            nameHeader.OnUpdate();
            nameWorldTextBox.OnUpdate();
            createWorldIndicator.OnUpdate();
        }
        public override void OnRender(RenderTarget target)
        {
            target.Draw(shapeBackground);

            panel1.OnRender(target);
            chooseHeader.OnRender(target);
            foreach (var button in buttonsWorlds)
                button.OnRender(target);

            panel2.OnRender(target);
            createHeader.OnRender(target);
            nameHeader.OnRender(target);
            nameWorldTextBox.OnRender(target);
            createButton.OnRender(target);
            createWorldIndicator.OnRender(target);

            target.Draw(cursorSprite);
        }

        private void CreateWorld(object worldName)
        {
            bool isCanGenerate = false;

            createWorldMutex.WaitOne();

            if (buttonsWorlds.Count < 10)
                isCanGenerate = true;
            
            createWorldMutex.ReleaseMutex();

            if (isCanGenerate)
            {
                WorldController world = new WorldController();
                world.GenerateWorld();
                world.SaveWorld($"worlds/{(string)worldName}");

                createWorldMutex.WaitOne();

                var button = new UIButton(panel1);
                button.Size = new Vector2f(panel1.Size.X / 1.1f, 30);
                button.Position = new Vector2f(10, 30 + buttonsWorlds.Count * 30 + 10 * buttonsWorlds.Count);
                button.ClickColor = Color.Cyan;
                button.SetText(new UIText(button, (string)worldName, 14, ResourcesManager.GetFont("ui_font")));
                buttonsWorlds.Add(button);

                int playerMaxHP = 100 + 5 * (int)GlobalData.GetNumericValue("player_level");
                int playerMaxMN = 100 + 3 * (int)GlobalData.GetNumericValue("player_level");
                int playerMaxDM = 15 + 2 * (int)GlobalData.GetNumericValue("player_level");
                int playerMaxUDM = 30 + 3 * (int)GlobalData.GetNumericValue("player_level");

                using (StreamWriter file = new StreamWriter($"worlds/{(string)worldName}/world_player.cfg"))
                {
                    file.WriteLine("player_position_x 0");
                    file.WriteLine("player_position_y 0");
                    file.WriteLine($"player_hp {playerMaxHP}");
                    file.WriteLine($"player_hg {100}");
                    file.WriteLine($"player_dp {100}");
                    file.WriteLine($"player_mn {playerMaxMN}");
                    file.WriteLine($"player_dm {playerMaxDM}");
                    file.WriteLine($"player_udm {playerMaxUDM}");
                    file.Close();
                }
                using (StreamWriter file = new StreamWriter($"worlds/{(string)worldName}/world_inventory.cfg"))
                {
                    file.WriteLine("items_count 0");
                }

                    DataWriter.SaveData("data/worlds_settings.cfg", "worlds_count", buttonsWorlds.Count);
                DataWriter.SaveData("data/worlds_settings.cfg", $"world_name_{buttonsWorlds.Count}", (string)worldName);

                createWorldThread.Abort();

                createWorldMutex.ReleaseMutex();
            }
        }
    }
}
