using build_alpha_0._2.Core;
using build_alpha_0._2.ECS;
using build_alpha_0._2.ECS.Components;
using build_alpha_0._2.NPCOptions;
using build_alpha_0._2.Physics;
using build_alpha_0._2.VFX;
using SFML.Graphics;
using SFML.System;
using System;

namespace build_alpha_0._2.NPC
{
    public class Deer
    {
        private Sprite sprite;

        private Vector2f velocity;
        private Clock changeVelocityClock;

        private Healthable healtable;
        public Healthable Healthable
        {
            get { return healtable; }
        }

        private Entity deerEntity;
        public Entity Entity
        {
            get { return deerEntity; }
        }
        private RectangleShape deerShape;
        public RectangleShape Shape
        {
            get { return deerShape; }
        }
        private Hitbox deerHitbox;
        public Hitbox Hitbox
        {
            get { return deerHitbox; }
        }


        public Deer(ref SystemManager manager, int deerID)
        {
            changeVelocityClock = new Clock();

            healtable = new Healthable();
            healtable.SetMaximumValues(60, 0, 0);

            deerEntity = manager.CreateEntity($"deer{deerID}");

            deerShape = new RectangleShape();
            deerShape.Size = new Vector2f(30, 28);
            deerShape.Position = new Vector2f(10, 10);
            sprite = new Sprite();
            sprite.Texture = ResourcesManager.GetTexture("animal_1");

            TransformComponent transformComponent = new TransformComponent(deerShape);
            manager.AddComponent(deerEntity, transformComponent);

            AnimationComponent animationComponent = new AnimationComponent(new Animator());
            animationComponent.AddAnimation("move_right", new Animation(sprite, 0, 0, 30, 28, 0.1f, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("move_left", new Animation(sprite, 30, 0, 30, 28, 0.1f, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("move_down", new Animation(sprite, 60, 0, 30, 28, 0.1f, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("move_up", new Animation(sprite, 90, 0, 30, 28, 0.1f, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("move_left_down", new Animation(sprite, 120, 0, 30, 28, 0.1f, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("move_right_down", new Animation(sprite, 150, 0, 30, 28, 0.1f, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("move_right_up", new Animation(sprite, 180, 0, 30, 28, 0.1f, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("move_left_up", new Animation(sprite, 210, 0, 30, 28, 0.1f, 2, AnimationDirection.Vertival));
            animationComponent.AddAnimation("stand_down", new Animation(sprite, 60, 0, 30, 28, 0.1f, 1, AnimationDirection.Vertival));
            manager.AddComponent(deerEntity, animationComponent);

            deerHitbox = new Hitbox(deerShape);
            HitboxComponent hitboxComponent = new HitboxComponent(deerHitbox);
            manager.AddComponent(deerEntity, hitboxComponent);
        }

        public void OnUpdate(ref SystemManager systemManager)
        {
            sprite.Position = deerShape.Position;
            healtable.CheckLifeState();

            if (deerShape.Position.Y < 0)
                deerShape.Position = new Vector2f(deerShape.Position.X, deerShape.Position.Y + 2);
            if (deerShape.Position.Y + deerShape.Size.Y > 30 * 18)
                deerShape.Position = new Vector2f(deerShape.Position.X, deerShape.Position.Y - 2);
            if (deerShape.Position.X < 0)
                deerShape.Position = new Vector2f(deerShape.Position.X + 2, deerShape.Position.Y);
            if (deerShape.Position.X + deerShape.Size.X > 30 * 30)
                deerShape.Position = new Vector2f(deerShape.Position.X - 2, deerShape.Position.Y);

            if (changeVelocityClock.ElapsedTime.AsSeconds() <= 3)
            {
                systemManager.GetComponent<TransformComponent>(Entity).SetVelocity(velocity);

                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X > 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y < 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("move_right_up");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }
                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X < 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y < 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("move_left_up");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }
                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X > 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y > 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("move_right_down");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }
                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X < 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y > 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("move_left_down");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }
                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X > 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y == 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("move_right");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }
                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X < 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y == 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("move_left");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }
                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X == 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y > 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("move_down");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }
                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X == 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y < 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("move_up");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }
            }
            if (changeVelocityClock.ElapsedTime.AsSeconds() > 10)
            {
                if (systemManager.GetComponent<TransformComponent>(Entity).Velocity.X == 0 &&
                    systemManager.GetComponent<TransformComponent>(Entity).Velocity.Y == 0)
                {
                    systemManager.GetComponent<AnimationComponent>(Entity).SetCurrentAnimation("stand_down");
                    systemManager.GetComponent<AnimationComponent>(Entity).PlayCurrentAnimation();
                }

                var randomVelocity = new Random();

                int dirX = randomVelocity.Next(2);
                if (dirX == 1)
                    velocity.X = (float)randomVelocity.Next(20, 35);
                else
                    velocity.X = -(float)randomVelocity.Next(20, 35);
                int dirY = randomVelocity.Next(2);
                if (dirY == 1)
                    velocity.Y = (float)randomVelocity.Next(20, 35);
                else
                    velocity.Y = -(float)randomVelocity.Next(20, 35);
                changeVelocityClock.Restart();
            }
        }
        public void OnRender(RenderTarget target, Light light)
        {
            if (light != null)
                target.Draw(sprite, new RenderStates(light.LightShader));
            else 
                target.Draw(sprite);
        }
    }
}
