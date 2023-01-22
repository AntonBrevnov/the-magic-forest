using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace build_alpha_0._2.ECS.Scenes
{
    using Core.Inventory.Items;
    using Core;
    using Core.Inventory;
    using Core.WorldLibrary;
    using Components;
    using NPC;
    using Physics;
    using UI;
    using VFX;

    public class SinglePlayerScene : Scene
    {
        // игровой мир
        private WorldController world;
        private MultiLightManager worldLight;
        private ItemsWorldContainer itemsWorld;
        private Clock itemsSpawnClock;

        // вход в пещеру
        private Entity mineEnterEntity;
        private RectangleShape mineEnterShape;
        private Sprite mineEnterSprite;
        private Entity homeEnterEntity;
        private RectangleShape homeEnterShape;
        private Sprite homeEnterSprite;
        private Entity stairsEntity;
        private RectangleShape stairsShape;
        private Sprite stairsSprite;
        private Entity tableEntity;
        private RectangleShape tableShape;
        private Sprite tableSprite;
        private Entity bedEntity;
        private RectangleShape bedShape;
        private Sprite bedSprite;

        private Clock ableChangeLocationClock;
        private Clock ableSpawnDeersClock;

        private DeerController deerController;

        private Camera camera;
        private Player player;

        private ParticleSystem particleSystem;

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

        private PlayerInventoryUI inventoryUI;

        public SinglePlayerScene(RenderWindow window) : base(window)
        {
        }

        public override void OnStart()
        {
            // загрузка мира
            world = new WorldController();
            world.LoadWorld($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}");

            ableChangeLocationClock = new Clock();
            ableSpawnDeersClock = new Clock();

            deerController = new DeerController();

            // создание элементов мира
            {
                // вход в пещеру
                {
                    mineEnterShape = new RectangleShape(new Vector2f(50, 30));
                    mineEnterShape.Position = new Vector2f(100, 100);
                    mineEnterEntity = systemManager.CreateEntity("mine_enter_entity");
                    mineEnterSprite = new Sprite();
                    mineEnterSprite.Position = mineEnterShape.Position;
                    Animation animation = new Animation();
                    animation.SetAnimatable(mineEnterSprite);
                    animation.SetSpeed(0);
                    animation.AddFrame(ResourcesManager.GetTexture("mine_way"));

                    AnimationComponent component = new AnimationComponent(new Animator());
                    component.AddAnimation("mine_animation", animation);
                    component.SetCurrentAnimation("mine_animation");
                    component.PlayCurrentAnimation();
                    systemManager.AddComponent(mineEnterEntity, component);

                    HitboxComponent hitboxComponent = new HitboxComponent(new Hitbox(mineEnterShape));
                    systemManager.AddComponent(mineEnterEntity, hitboxComponent);
                }
                // вход в дом
                {
                    homeEnterShape = new RectangleShape(new Vector2f(250, 246));
                    homeEnterShape.Position = new Vector2f(250, 250);
                    homeEnterEntity = systemManager.CreateEntity("home_enter_entity");
                    homeEnterSprite = new Sprite();
                    homeEnterSprite.Texture = ResourcesManager.GetTexture("tree_house");
                    homeEnterSprite.Position = homeEnterShape.Position;
                    Animation animation = new Animation();
                    animation.SetAnimatable(homeEnterSprite);
                    animation.SetSpeed(3);
                    animation.AddFrame(new IntRect(0, 0, 250, 246));
                    animation.AddFrame(new IntRect(0, 246, 250, 246));

                    AnimationComponent component = new AnimationComponent(new Animator());
                    component.AddAnimation("home_animation", animation);
                    component.SetCurrentAnimation("home_animation");
                    component.PlayCurrentAnimation();
                    systemManager.AddComponent(homeEnterEntity, component);

                    HitboxComponent hitboxComponent = new HitboxComponent(new Hitbox(homeEnterShape));
                    systemManager.AddComponent(homeEnterEntity, hitboxComponent);
                }
                // лестница
                {
                    stairsShape = new RectangleShape(new Vector2f(80, 50));
                    stairsShape.Position = new Vector2f(20, 60);
                    stairsEntity = systemManager.CreateEntity("stairs_entity");
                    stairsSprite = new Sprite();
                    stairsSprite.Position = stairsShape.Position;
                    Animation animation = new Animation();
                    animation.SetAnimatable(stairsSprite);
                    animation.SetSpeed(0);
                    animation.AddFrame(ResourcesManager.GetTexture("stairs"));

                    AnimationComponent component = new AnimationComponent(new Animator());
                    component.AddAnimation("stairs_animation", animation);
                    component.SetCurrentAnimation("stairs_animation");
                    component.PlayCurrentAnimation();
                    systemManager.AddComponent(stairsEntity, component);

                    HitboxComponent hitboxComponent = new HitboxComponent(new Hitbox(stairsShape));
                    systemManager.AddComponent(stairsEntity, hitboxComponent);
                }
                // стол
                {
                    tableShape = new RectangleShape(new Vector2f(50, 45));
                    tableShape.Position = new Vector2f(20, 10);
                    tableEntity = systemManager.CreateEntity("table_entity");
                    Animation animation = new Animation();
                    tableSprite = new Sprite();
                    tableSprite.Position = tableShape.Position;
                    animation.SetAnimatable(tableSprite);
                    animation.SetSpeed(0);
                    animation.AddFrame(ResourcesManager.GetTexture("table"));

                    AnimationComponent component = new AnimationComponent(new Animator());
                    component.AddAnimation("table_animation", animation);
                    component.SetCurrentAnimation("table_animation");
                    component.PlayCurrentAnimation();
                    systemManager.AddComponent(tableEntity, component);

                    HitboxComponent hitboxComponent = new HitboxComponent(new Hitbox(tableShape));
                    systemManager.AddComponent(tableEntity, hitboxComponent);
                }
                // кровать
                {
                    bedShape = new RectangleShape(new Vector2f(80, 50));
                    bedShape.Position = new Vector2f(130, 10);
                    bedEntity = systemManager.CreateEntity("bed_entity");
                    Animation animation = new Animation();
                    bedSprite = new Sprite();
                    bedSprite.Position = bedShape.Position;
                    animation.SetAnimatable(bedSprite);
                    animation.SetSpeed(0);
                    animation.AddFrame(ResourcesManager.GetTexture("bed"));

                    AnimationComponent component = new AnimationComponent(new Animator());
                    component.AddAnimation("bed_animation", animation);
                    component.SetCurrentAnimation("bed_animation");
                    component.PlayCurrentAnimation();
                    systemManager.AddComponent(bedEntity, component);

                    HitboxComponent hitboxComponent = new HitboxComponent(new Hitbox(bedShape));
                    systemManager.AddComponent(bedEntity, hitboxComponent);
                }
            }

            worldLight = new MultiLightManager("resources/shaders/multi_light_shader.vert", "resources/shaders/multi_light_shader.frag");
            
            camera = new Camera();
            camera.SetViewport(new Vector2f(100, 100), new Vector2f(640, 360));
            camera.SetZoom(0.45f);

            // инициализация игрока
            player = new Player(ref systemManager, GlobalData.GetTextValue("playr_name"));
            player.Inventory.LoadInventory($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_inventory.cfg");
            worldLight.AddLight(player.PlayerLight);

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

            particleSystem = new ParticleSystem();

            itemsWorld = new ItemsWorldContainer();
            itemsWorld.AddItemTemplate(new LogItem());
            itemsWorld.AddItemTemplate(new StickItem());
            itemsWorld.AddItemTemplate(new BukketItem());
            itemsSpawnClock = new Clock();

            cursorSprite = new Sprite();
            cursorSprite.Texture = ResourcesManager.GetTexture("cursor");

            inventoryUI = new PlayerInventoryUI();
            inventoryUI.OnStart(window);

            ResourcesManager.GetSound("nature").Loop = true;
            ResourcesManager.GetSound("nature").Play();
        }

        public override void OnHandleKeyPressed(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                ResourcesManager.GetSound("nature").Stop();
                ChangeScene(1);
                world.SaveWorld($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}");
                DataWriter.SaveData($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_player.cfg", "player_position_x", player.Shape.Position.X);
                DataWriter.SaveData($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_player.cfg", "player_position_y", player.Shape.Position.Y);
                DataWriter.SaveData($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_player.cfg", "player_hp", player.Healthable.HP);
                DataWriter.SaveData($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_player.cfg", "player_hg", player.Healthable.HG);
                DataWriter.SaveData($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_player.cfg", "player_dp", player.Healthable.DP);
                DataWriter.SaveData($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_player.cfg", "player_mn", player.Fightable.MN);
                DataWriter.SaveData($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_player.cfg", "player_dm", player.Fightable.DM);
                DataWriter.SaveData($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_player.cfg", "player_udm", player.Fightable.UDM);
                player.Inventory.SaveInventory($"worlds/{GlobalData.GetTextValue("load_wolrd_name")}/world_inventory.cfg");
                GlobalData.RemoveTextVariable("load_wolrd_name");
            }

            if (world.Location == WorldLocations.Forest)
            {
                if (systemManager.GetComponent<HitboxComponent>(player.Entity).CheckOnCollision(systemManager.GetComponent<HitboxComponent>(mineEnterEntity).Hitbox))
                    if (e.Code == Keyboard.Key.E && ableChangeLocationClock.ElapsedTime.AsSeconds() > 1)
                    {
                        world.Location = WorldLocations.Mine;
                        player.Shape.Position = new Vector2f(200, 200);
                        ableChangeLocationClock.Restart();
                    }
                if (systemManager.GetComponent<HitboxComponent>(player.Entity).CheckOnCollision(systemManager.GetComponent<HitboxComponent>(homeEnterEntity).Hitbox))
                    if (e.Code == Keyboard.Key.E && ableChangeLocationClock.ElapsedTime.AsSeconds() > 1)
                    {
                        world.Location = WorldLocations.House;
                        player.Shape.Position = new Vector2f(30, 30);
                        ableChangeLocationClock.Restart();
                    }
            }
            if (world.Location == WorldLocations.Mine)
            {
                if (systemManager.GetComponent<HitboxComponent>(player.Entity).CheckOnCollision(systemManager.GetComponent<HitboxComponent>(stairsEntity).Hitbox))
                    if (e.Code == Keyboard.Key.E && ableChangeLocationClock.ElapsedTime.AsSeconds() > 1)
                    {
                        world.Location = WorldLocations.Forest;
                        player.Shape.Position = new Vector2f(200, 200);
                        ableChangeLocationClock.Restart();
                    }
            }
            if (world.Location == WorldLocations.House)
            {
                if (systemManager.GetComponent<HitboxComponent>(player.Entity).CheckOnCollision(systemManager.GetComponent<HitboxComponent>(stairsEntity).Hitbox))
                    if (e.Code == Keyboard.Key.E && ableChangeLocationClock.ElapsedTime.AsSeconds() > 1)
                    {
                        world.Location = WorldLocations.Forest;
                        player.Shape.Position = new Vector2f(530, 320);
                        ableChangeLocationClock.Restart();
                    }
                if (systemManager.GetComponent<HitboxComponent>(player.Entity).CheckOnCollision(systemManager.GetComponent<HitboxComponent>(bedEntity).Hitbox))
                    if (e.Code == Keyboard.Key.E)
                        worldLight.SetDay();
                if (systemManager.GetComponent<HitboxComponent>(player.Entity).CheckOnCollision(systemManager.GetComponent<HitboxComponent>(tableEntity).Hitbox))
                {
                    if (e.Code == Keyboard.Key.E)
                        inventoryUI.isShowCraft = !inventoryUI.isShowCraft;
                }
                else
                    inventoryUI.isShowCraft = false;
            }

            if (e.Code == Keyboard.Key.Q)
            {
                if (player.Fightable.MN >= 40)
                    camera.StartShake(8, 0.5f);
            }

            inventoryUI.OnHandleKeyPressed(e);
        }
        public override void OnHandleKeyReleased(KeyEventArgs e)
        {
            inventoryUI.OnHandleKeyReleased(e);
        }
        public override void OnHandleButtonPressed(MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                ResourcesManager.GetSound("plr_attack").Play();
                camera.StartShake(5, 0.5f);
            }

            inventoryUI.OnHandleButtonPressed(e);
        }
        public override void OnHandleButtonReleased(MouseButtonEventArgs e)
        {
            inventoryUI.OnHandleButtonReleased(e);
        }
        public override void OnHandleMouseMoved(MouseMoveEventArgs e)
        {
            cursorSprite.Position = new Vector2f(e.X, e.Y);
            inventoryUI.OnHandleMouseMoved(e);
        }
        public override void OnHandleWheelScrolled(MouseWheelScrollEventArgs e)
        {
            inventoryUI.OnHandleWheelScrolled(e);
        }

        public override void OnUpdate(float deltaTime)
        {
            // обновление игрока
            player.OnUpdate(ref systemManager, ref world, ref deerController);

            world.OnUpdate(deltaTime, itemsWorld);
            worldLight.OnUpdate();

            camera.OnUpdate(player.Shape.Position);

            if (world.Location == WorldLocations.Forest)
            {
                deerController.OnUpdate(ref systemManager);
                if (ableSpawnDeersClock.ElapsedTime.AsSeconds() >= 10)
                {
                    deerController.SpawnDeer(3, new Vector2f(30 * 30, 30 * 18), ref systemManager);
                    ableSpawnDeersClock.Restart();
                }
            }

            infoHealthPanel.OnUpdate();
            HealthText.OnUpdate();
            HealthText.SetText(Localization.GetParametr("hp_text") + " " + player.Healthable.HP);
            FoodText.OnUpdate();
            FoodText.SetText(Localization.GetParametr("hg_text") + " " + player.Healthable.HG);
            WaterText.OnUpdate();
            WaterText.SetText(Localization.GetParametr("dp_text") + " " + player.Healthable.DP);

            infoFightPanel.OnUpdate();
            ManaText.OnUpdate();
            ManaText.SetText(Localization.GetParametr("mn_text") + " " + player.Fightable.MN);
            DamageText.OnUpdate();
            DamageText.SetText(Localization.GetParametr("dm_text") + " " + player.Fightable.DM);
            UltDamageText.OnUpdate();
            UltDamageText.SetText(Localization.GetParametr("udm_text") + " " + player.Fightable.UDM);

            systemManager.OnUpdate(deltaTime);
            particleSystem.OnUpdate();

            if (world.Location == WorldLocations.Forest)
            {
                if (itemsSpawnClock.ElapsedTime.AsSeconds() >= 60)
                {
                    itemsSpawnClock.Restart();
                    itemsWorld.ClearWorld();
                    itemsWorld.SpawnItems(new FloatRect(10, 10, 890, 530), 10);
                }
            }

            itemsWorld.CheckOnPlayerCollision(player, systemManager);
            itemsWorld.OnUpdate();
            inventoryUI.OnUpdate(player);
        }
        public override void OnRender(RenderTarget target)
        {
            target.Clear(Color.Black);
            target.SetView(camera.GetCameraView());

            world.OnRender(target, worldLight.GetMultipleLight());

            if (world.Location == WorldLocations.Forest)
            {
                target.Draw(mineEnterSprite, new RenderStates(worldLight.GetMultipleLight().LightShader));
                itemsWorld.OnRender(target, worldLight.GetMultipleLight());
            }
            if (world.Location == WorldLocations.House)
            {
                target.Draw(tableSprite, new RenderStates(worldLight.GetMultipleLight().LightShader));
                target.Draw(bedSprite, new RenderStates(worldLight.GetMultipleLight().LightShader));
            }
            player.OnRender(target, worldLight.GetMultipleLight());
            if (world.Location == WorldLocations.Forest)
            {
                target.Draw(homeEnterSprite, new RenderStates(worldLight.GetMultipleLight().LightShader));
                deerController.OnRender(target, worldLight.GetMultipleLight());
            }
            if (world.Location == WorldLocations.Mine)
                target.Draw(stairsSprite, new RenderStates(worldLight.GetMultipleLight().LightShader));
            if (world.Location == WorldLocations.House)
                target.Draw(stairsSprite, new RenderStates(worldLight.GetMultipleLight().LightShader));

            particleSystem.OnRender(target);

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

            inventoryUI.OnRender(target);

            target.Draw(cursorSprite);
        }
    }
}
