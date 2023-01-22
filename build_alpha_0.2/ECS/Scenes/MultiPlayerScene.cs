using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using build_alpha_0._2.NPC;
using build_alpha_0._2.Core.Network;
using build_alpha_0._2.Core;
using build_alpha_0._2.Core.WorldLibrary;
using build_alpha_0._2.VFX;
using SFML.System;
using System;
using build_alpha_0._2.UI;

namespace build_alpha_0._2.ECS.Scenes
{
    public class MultiPlayerScene : Scene
    {
        private WorldController world;

        private List<Player> playerList;
        private List<UIText> playerNamesList;
        private TMFClient client;
        private Camera camera;

        private Clock ableSpawnDeersClock;

        DeerController deerController;

        // пользовательский интерфейс
        private Camera uiCamera;

        private UIPanel infoHealthPanel;
        private Sprite heartPanel;
        private UIText HealthText;
        private Sprite foodPanel;
        private UIText FoodText;
        private Sprite waterPanel;
        private UIText WaterText;

        private UIPanel infoFightPanel;
        private Sprite manaPanel;
        private UIText ManaText;
        private Sprite damagePanel;
        private UIText DamageText;
        private UIText UltDamageText;

        private Sprite cursorSprite;

        public MultiPlayerScene(RenderWindow window) : base(window)
        {
        }

        public override void OnStart()
        {
            try
            {
                world = new WorldController();
                world.LoadWorld("worlds/SERVERWORLD");

                playerList = new List<Player>();
                playerNamesList = new List<UIText>();

                client = new TMFClient(GlobalData.GetTextValue("player_name"));
                if (!client.Connect(GlobalData.GetTextValue("ip_address"), int.Parse(GlobalData.GetTextValue("port"))))
                    ChangeScene(1);

                ableSpawnDeersClock = new Clock();

                deerController = new DeerController();

                GlobalData.RemoveTextVariable("ip_address");
                GlobalData.RemoveTextVariable("port");

                camera = new Camera();
                camera.SetViewport(new Vector2f(100, 100), new Vector2f(640, 360));
                camera.SetZoom(0.45f);
                 
                playerList.Add(new Player(ref systemManager, GlobalData.GetTextValue("player_name")));
                playerNamesList.Add(new UIText(GlobalData.GetTextValue("player_name"), 12, ResourcesManager.GetFont("names_font"), Color.Yellow));

                // иницализация пользовательского интерфейса
                uiCamera = new Camera();
                uiCamera.SetViewport(new Vector2f(window.Size.X / 2, window.Size.Y / 2), (Vector2f)window.Size);

                // инициализация панели здоровья
                {
                    infoHealthPanel = new UIPanel();
                    infoHealthPanel.Size = new Vector2f(160, 72);
                    infoHealthPanel.Position = new Vector2f(10, 10);
                    infoHealthPanel.DefaultColor = new Color(170, 170, 170, 210);

                    heartPanel = new Sprite();
                    heartPanel.Position = new Vector2f(14, 16);
                    heartPanel.Color = Color.White;
                    heartPanel.Scale = new Vector2f(1.1f, 1.1f);
                    heartPanel.Texture = ResourcesManager.GetTexture("ui_particles");
                    heartPanel.TextureRect = new IntRect(0, 0, 11, 11);

                    HealthText = new UIText(Localization.GetParametr("hp_text"), 16, ResourcesManager.GetFont("ui_font"));
                    HealthText.DefaultColor = Color.Red;
                    HealthText.Position = new Vector2f(28, 12);

                    foodPanel = new Sprite();
                    foodPanel.Position = new Vector2f(14, 36);
                    foodPanel.Color = Color.White;
                    foodPanel.Scale = new Vector2f(1.1f, 1.1f);
                    foodPanel.Texture = ResourcesManager.GetTexture("ui_particles");
                    foodPanel.TextureRect = new IntRect(11, 0, 11, 11);

                    FoodText = new UIText(Localization.GetParametr("hg_text"), 16, ResourcesManager.GetFont("ui_font"));
                    FoodText.DefaultColor = new Color(139, 69, 19);
                    FoodText.Position = new Vector2f(28, 32);

                    waterPanel = new Sprite();
                    waterPanel.Position = new Vector2f(14, 56);
                    waterPanel.Color = Color.White;
                    waterPanel.Scale = new Vector2f(1.1f, 1.1f);
                    waterPanel.Texture = ResourcesManager.GetTexture("ui_particles");
                    waterPanel.TextureRect = new IntRect(22, 0, 11, 11);

                    WaterText = new UIText(Localization.GetParametr("dp_text"), 16, ResourcesManager.GetFont("ui_font"));
                    WaterText.DefaultColor = Color.Blue;
                    WaterText.Position = new Vector2f(28, 52);
                }
                // инициализация боевой панели
                {
                    infoFightPanel = new UIPanel();
                    infoFightPanel.Size = new Vector2f(160, 72);
                    infoFightPanel.Position = new Vector2f(10, window.Size.Y - infoFightPanel.Size.Y - 10);
                    infoFightPanel.DefaultColor = new Color(170, 170, 170, 210);

                    manaPanel = new Sprite();
                    manaPanel.Position = new Vector2f(14, window.Size.Y - 72);
                    manaPanel.Color = Color.White;
                    manaPanel.Scale = new Vector2f(1.1f, 1.1f);
                    manaPanel.Texture = new Texture("resources/textures/ui/ui_particles.png");
                    manaPanel.TextureRect = new IntRect(44, 0, 11, 11);

                    ManaText = new UIText(Localization.GetParametr("mn_text"), 16, ResourcesManager.GetFont("ui_font"));
                    ManaText.DefaultColor = new Color(50, 0, 255);
                    ManaText.Position = new Vector2f(28, window.Size.Y - 76);

                    DamageText = new UIText(Localization.GetParametr("dm_text"), 16, ResourcesManager.GetFont("ui_font"));
                    DamageText.DefaultColor = new Color(50, 50, 50);
                    DamageText.Position = new Vector2f(32, window.Size.Y - 56);

                    UltDamageText = new UIText(Localization.GetParametr("udm_text"), 16, ResourcesManager.GetFont("ui_font"));
                    UltDamageText.DefaultColor = new Color(50, 50, 50);
                    UltDamageText.Position = new Vector2f(32, window.Size.Y - 36);

                    damagePanel = new Sprite();
                    damagePanel.Position = new Vector2f(14, window.Size.Y - 52);
                    damagePanel.Scale = new Vector2f(1.1f, 1.1f);
                    damagePanel.Color = Color.White;
                    damagePanel.Texture = ResourcesManager.GetTexture("ui_particles");
                    damagePanel.TextureRect = new IntRect(33, 0, 11, 11);
                }
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ошибка OnStart: " + exc.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                client.Exit();
                ChangeScene(1);
            }

            cursorSprite = new Sprite();
            cursorSprite.Texture = ResourcesManager.GetTexture("cursor");
        }

        public override void OnHandleKeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                client.Exit();
                ChangeScene(1);
            }
        }
        public override void OnHandleKeyReleased(KeyEventArgs e)
        {

        }
        public override void OnHandleButtonPressed(MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
                camera.StartShake(5, 0.5f);
        }
        public override void OnHandleButtonReleased(MouseButtonEventArgs e)
        {

        }
        public override void OnHandleMouseMoved(MouseMoveEventArgs e)
        {
            cursorSprite.Position = new Vector2f(e.X, e.Y);
        }
        public override void OnHandleWheelScrolled(MouseWheelScrollEventArgs e)
        {
            
        }

        public override void OnUpdate(float deltaTime)
        {
            try
            {
                world.OnUpdate(deltaTime, null);

                if (world.Location == WorldLocations.Forest)
                {
                    deerController.OnUpdate(ref systemManager);
                    if (ableSpawnDeersClock.ElapsedTime.AsSeconds() >= 10)
                    {
                        deerController.SpawnDeer(3, new Vector2f(30 * 30, 30 * 18), ref systemManager);
                        ableSpawnDeersClock.Restart();
                    }
                }

                client.CheckNewPlayers(ref playerList, ref playerNamesList, ref systemManager);
                for (int i = 0; i < playerList.Count; i++)
                {
                    if (playerList[i].Name != GlobalData.GetTextValue("player_name"))
                    {
                        Vector2f newPosition = new Vector2f();
                        client.GetPlayerData(playerList[i].Name, ref newPosition);
                        playerList[i].Shape.Position = newPosition;
                        playerNamesList[i].Position = new Vector2f(playerList[i].Shape.Position.X - 5, playerList[i].Shape.Position.Y - 10);
                        playerNamesList[i].OnUpdate();
                    }
                    else
                    {
                        playerList[i].OnUpdate(ref systemManager, ref world, ref deerController);
                        client.UpdatePlayerData(playerList[i]);
                        camera.OnUpdate(playerList[i].Shape.Position);
                        playerNamesList[i].Position = new Vector2f(playerList[i].Shape.Position.X - 5, playerList[i].Shape.Position.Y - 10);
                        playerNamesList[i].OnUpdate();
                        if (!playerList[i].Healthable.IsLife)
                        {
                            playerList.RemoveAt(i);
                            playerNamesList.RemoveAt(i);
                            client.Exit();
                            ChangeScene(1);
                        }

                        infoHealthPanel.OnUpdate();
                        HealthText.OnUpdate();
                        HealthText.SetText(Localization.GetParametr("hp_text") + " " + playerList[i].Healthable.HP);
                        FoodText.OnUpdate();
                        FoodText.SetText(Localization.GetParametr("hg_text") + " " + playerList[i].Healthable.HG);
                        WaterText.OnUpdate();
                        WaterText.SetText(Localization.GetParametr("dp_text") + " " + playerList[i].Healthable.DP);

                        infoFightPanel.OnUpdate();
                        ManaText.OnUpdate();
                        ManaText.SetText(Localization.GetParametr("mn_text") + " " + playerList[i].Fightable.MN);
                        DamageText.OnUpdate();
                        DamageText.SetText(Localization.GetParametr("dm_text") + " " + playerList[i].Fightable.DM);
                        UltDamageText.OnUpdate();
                        UltDamageText.SetText(Localization.GetParametr("udm_text") + " " + playerList[i].Fightable.UDM);
                    }
                }

                systemManager.OnUpdate(deltaTime);
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ошибка OnUpdate: " + exc.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                client.Exit();
                ChangeScene(1);
            }
        }

        public override void OnRender(RenderTarget target)
        {
            try
            {
                target.Clear(Color.Black);
                target.SetView(camera.GetCameraView());

                world.OnRender(target, null);
            
                foreach (var player in playerList)
                    player.OnRender(target, null);
                if (world.Location == WorldLocations.Forest)
                    deerController.OnRender(target, null);
                foreach (var names in playerNamesList)
                    names.OnRender(target);

                target.SetView(uiCamera.GetCameraView());

                infoHealthPanel.OnRender(target);
                HealthText.OnRender(target);
                target.Draw(heartPanel);
                FoodText.OnRender(target);
                target.Draw(foodPanel);
                WaterText.OnRender(target);
                target.Draw(waterPanel);

                infoFightPanel.OnRender(target);
                ManaText.OnRender(target);
                target.Draw(manaPanel);
                DamageText.OnRender(target);
                target.Draw(damagePanel);
                UltDamageText.OnRender(target);

                target.Draw(cursorSprite);
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ошибка OnRender: " + exc.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                client.Exit();
                ChangeScene(1);
            }
        }
    }
}
