using SFML.Graphics;
using SFML.System;

namespace build_alpha_0._2.ECS.Components
{
    public class TransformComponent : Component
    {
        private Vector2f velocity;
        public Vector2f Velocity
        {
            get { return velocity; }
        }

        private Transformable transformable;
        public Transformable Transformable
        {
            set { transformable = value; }
        }

        public TransformComponent() : base()
        {
            velocity = new Vector2f(0, 0);
            transformable = null;
        }
        public TransformComponent(uint id) : base(id)
        {
            velocity = new Vector2f(0, 0);
            transformable = null;
        }
        public TransformComponent(Transformable transformable) : base()
        {
            velocity = new Vector2f(0, 0);
            this.transformable = transformable;
        }
        public TransformComponent(Transformable transformable, uint id) : base(id)
        {
            velocity = new Vector2f(0, 0);
            this.transformable = transformable;
        }

        public void SetVelocity(float velocityX, float velocityY)
        {
            velocity.X = velocityX;
            velocity.Y = velocityY;
        }
        public void SetVelocity(Vector2f velocity)
        {
            this.velocity.X = velocity.X;
            this.velocity.Y = velocity.Y;
        }

        public override void OnUpdate(float deltaTime)
        {
            float oldPositionX = transformable.Position.X;
            float oldPositionY = transformable.Position.Y;
            transformable.Position = new Vector2f(
                oldPositionX + (this.velocity.X * deltaTime),
                oldPositionY + (this.velocity.Y * deltaTime));

            this.velocity = new Vector2f(0, 0);
        }
        public override void OnRender(RenderTarget target)
        {
        }
    }
}
