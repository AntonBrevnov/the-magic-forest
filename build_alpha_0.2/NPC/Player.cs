using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace build_alpha_0._2.NPC
{
    using Core.Inventory;
    using Core;
    using Core.WorldLibrary;
    using ECS;
    using ECS.Components;
    using NPCOptions;
    using Physics;
    using VFX;

    public class Player
    {
        private Sprite sprite;

        public string Name { get; set; } = "player";

        private Healthable healtable;
        public Healthable Healthable
        {
            get { return healtable; }
        }
        private Fightable fightable;
        public Fightable Fightable
        {
            get { return fightable; }
        }

        private Entity playerEntity;
        public Entity Entity
        {
            get { return playerEntity; }
        }
        private RectangleShape playerShape;
        public RectangleShape Shape
        {
            get { return playerShape; }
        }
        private Hitbox playerHitbox;
        public Hitbox Hitbox
        {
            get { return playerHitbox; }
        }

        private Light playerLight;
        public Light PlayerLight => playerLight;

        private ParticleSystem particleSystem;

        private InventoryController inventory;
        public InventoryController Inventory => inventory;

        public Player(ref SystemManager manager, string name)
        {
            Name = name;

            healtable = new Healthable();
            healtable.SetMaximumValues((int)GlobalData.GetNumericValue("player_max_hp"), (int)GlobalData.GetNumericValue("player_max_hg"), (int)GlobalData.GetNumericValue("player_max_dp"));
            fightable = new Fightable();
            fightable.SetMaximumValues((int)GlobalData.GetNumericValue("player_max_mn"), (int)GlobalData.GetNumericValue("player_max_dm"), (int)GlobalData.GetNumericValue("player_max_udm"));

            playerEntity = manager.CreateEntity("player");

            playerShape = new RectangleShape();
            playerShape.Size = new Vector2f(14, 28);
            playerShape.Position = new Vector2f(10, 10);
            sprite = new Sprite();
            sprite.Texture = ResourcesManager.GetTexture("player");

            TransformComponent transformComponent = new TransformComponent(playerShape);
            manager.AddComponent(playerEntity, transformComponent);

            AnimationComponent animationComponent = new AnimationComponent(new Animator());
            animationComponent.AddAnimation("player_move_up_right", new Animation(sprite, 0, 28, 14, 28, 3, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("player_move_up_left", new Animation(sprite, 14, 28, 14, 28, 3, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("player_move_right", new Animation(sprite, 28, 28, 14, 28, 3, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("player_move_left", new Animation(sprite, 42, 28, 14, 28, 3, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("player_move_down", new Animation(sprite, 56, 28, 14, 28, 3, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("player_move_up", new Animation(sprite, 70, 28, 14, 28, 3, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("player_move_down_right", new Animation(sprite, 84, 28, 14, 28, 3, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("player_move_down_left", new Animation(sprite, 98, 28, 14, 28, 3, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("player_stand", new Animation(sprite, 56, 0, 14, 28, 0, 1, AnimationDirection.Vertival));
            animationComponent.SetCurrentAnimation("player_stand");
            animationComponent.PlayCurrentAnimation();
            manager.AddComponent(playerEntity, animationComponent);

            playerHitbox = new Hitbox(playerShape);
            HitboxComponent hitboxComponent = new HitboxComponent(playerHitbox);
            manager.AddComponent(playerEntity, hitboxComponent);

            playerLight = new Light();
            playerLight.AmbientColor = new Vector3f(0.02f, 0.02f, 0.02f);
            playerLight.LightColor = new Vector3f(1, 1, 1);
            playerLight.LightDistance = 1.5f;
            playerLight.LightIntensity = 0;

            particleSystem = new ParticleSystem();

            inventory = new InventoryController(25);
        }

        public void OnUpdate(ref SystemManager systemManager, ref WorldController world, ref DeerController deerController)
        {
            sprite.Position = playerShape.Position;
            healtable.CheckLifeState();

            // движение игроком
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                systemManager.GetComponent<TransformComponent>(Entity).SetVelocity(systemManager.GetComponent<TransformComponent>(Entity).Velocity.X, -45);
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                systemManager.GetComponent<TransformComponent>(Entity).SetVelocity(systemManager.GetComponent<TransformComponent>(Entity).Velocity.X, 45);
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                systemManager.GetComponent<TransformComponent>(Entity).SetVelocity(-45, systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y);
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                systemManager.GetComponent<TransformComponent>(Entity).SetVelocity(45, systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y);

            if (world.Location == WorldLocations.Forest)
            {
                foreach (var deer in deerController.Deers)
                {
                    if (systemManager.GetComponent<HitboxComponent>(Entity).CheckOnCollision(deer.Hitbox))
                    {
                        if (Mouse.IsButtonPressed(Mouse.Button.Left))
                            fightable.AttackHealthable(deer.Healthable);
                    }
                }
            }
            if (world.Location == WorldLocations.Forest || world.Location == WorldLocations.Mine)
            {
                if (playerShape.Position.Y < 0)
                    playerShape.Position = new Vector2f(playerShape.Position.X, playerShape.Position.Y + 2);
                if (playerShape.Position.Y + playerShape.Size.Y > 30 * 18)
                    playerShape.Position = new Vector2f(playerShape.Position.X, playerShape.Position.Y - 2);
                if (playerShape.Position.X < 0)
                    playerShape.Position = new Vector2f(playerShape.Position.X + 2, playerShape.Position.Y);
                if (playerShape.Position.X + playerShape.Size.X > 30 * 30)
                    playerShape.Position = new Vector2f(playerShape.Position.X - 2, playerShape.Position.Y);
            }
            if (world.Location == WorldLocations.House)
            {
                if (playerShape.Position.Y < 0)
                    playerShape.Position = new Vector2f(playerShape.Position.X, playerShape.Position.Y + 2);
                if (playerShape.Position.Y + playerShape.Size.Y > 6 * 18)
                    playerShape.Position = new Vector2f(playerShape.Position.X, playerShape.Position.Y - 2);
                if (playerShape.Position.X < 0)
                    playerShape.Position = new Vector2f(playerShape.Position.X + 2, playerShape.Position.Y);
                if (playerShape.Position.X + playerShape.Size.X > 6 * 30)
                    playerShape.Position = new Vector2f(playerShape.Position.X - 2, playerShape.Position.Y);
            }

            // смена анимации в зависимоти от движения игрока
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X > 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y < 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_move_up_right");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X < 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y < 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_move_up_left");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X > 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y > 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_move_down_right");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X < 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y > 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_move_down_left");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X > 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y == 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_move_right");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X < 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y == 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_move_left");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X == 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y > 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_move_down");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X == 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y < 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_move_up");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }
            if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X == 0 &&
                systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y == 0)
            {
                systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("player_stand");
                systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
            }

            if (world.Location == WorldLocations.Forest)
            {
                foreach (var tree in world.ForestTrees)
                {
                    // механизм рубки деревьев
                    if (systemManager.GetComponent<HitboxComponent>(Entity).CheckOnCollision(tree.Hitbox))
                    {
                        if (Mouse.IsButtonPressed(Mouse.Button.Left))
                        {
                            ResourcesManager.GetSound("wood_hit").Play();
                            particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_9"));
                            particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_10"));
                            fightable.AttackHealthable(tree);
                        }


                        if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                        {
                            if (Fightable.MN >= 40)
                            {
                                particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_1"));
                                particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_2"));
                                particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_3"));
                                particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_4"));
                                particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_5"));
                                particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_6"));
                                particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_7"));
                                particleSystem.SpawnParticles(playerShape.Position + new Vector2f(10, 10), 6, 50, ResourcesManager.GetTexture("particle_8"));
                                fightable.UltimateAttackHealthable(tree);
                            }
                        }
                    }
                    // столкновение с деревьями
                    if (systemManager.GetComponent<HitboxComponent>(Entity).CheckOnCollision(tree.Hitbox))
                    {
                        // столкновение слева
                        if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X > 0 && (int)tree.Position.X > (int)playerShape.Position.X)
                            playerShape.Position = new Vector2f(tree.Position.X - playerShape.Size.X + 1, playerShape.Position.Y);
                        else
                        // столкновение справа
                        if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X < 0 && ((int)tree.Position.X < (int)playerShape.Position.X &&
                            (int)tree.Position.X + (int)tree.Size.X < (int)playerShape.Position.X + (int)playerShape.Size.X))
                            playerShape.Position = new Vector2f(tree.Position.X + tree.Size.X - 1, playerShape.Position.Y);

                        // столкновение сверху
                        if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y > 0 && (int)tree.Position.Y > (int)playerShape.Position.Y)
                            playerShape.Position = new Vector2f(playerShape.Position.X, tree.Position.Y - playerShape.Size.Y + 1);
                        else
                        // столкновение снизу
                        if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y < 0 && ((int)tree.Position.Y < (int)playerShape.Position.Y &&
                            (int)tree.Position.Y + (int)tree.Size.Y < (int)playerShape.Position.Y + (int)playerShape.Size.Y))
                            playerShape.Position = new Vector2f(playerShape.Position.X, tree.Position.Y + tree.Size.Y - 1);
                    }
                }
            }
            if (world.Location == WorldLocations.Mine)
            {
                foreach (var ore in world.MineOres)
                {
                    // механизм рубки деревьев
                    if (systemManager.GetComponent<HitboxComponent>(Entity).CheckOnCollision(ore.Hitbox))
                    {
                        if (Mouse.IsButtonPressed(Mouse.Button.Left))
                            fightable.AttackHealthable(ore);
                    }
                }
            }

            playerLight.LightPosition = new Vector2f(playerShape.Position.X, playerShape.Position.Y);

            particleSystem.OnUpdate();
        }
        public void OnRender(RenderTarget target, Light light)
        {
            if (light != null)
                target.Draw(sprite, new RenderStates(light.LightShader));
            else
                target.Draw(sprite);
            particleSystem.OnRender(target);
        }
    }
}
