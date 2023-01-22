using SFML.Graphics;
using SFML.System;

namespace build_alpha_0._2.VFX
{
    public class Particle
    {
        private RectangleShape shape;

        private bool isLife;
        public bool IsLife 
        { 
            get { return isLife; } 
        }

        private float lifeTime;
        public float LifeTime
        {
            get { return lifeTime; }
        }

        private Vector2f direction;
        private float scaleSpeed;
        private float rotateSpeed;
        private float moveSpeed;

        public Particle()
        {
            shape = new RectangleShape();

            lifeTime = 0;
            scaleSpeed = 0;
            rotateSpeed = 0;
            moveSpeed = 0;
            direction = new Vector2f(0, 0);

            shape.Size = new Vector2f(4, 4);
            shape.Origin = new Vector2f(shape.Size.X / 2, shape.Size.Y / 2);
            shape.FillColor = Color.White;

            isLife = true;
        }
        public Particle(float lifeTime, float scaleSpeed, float rotateSpeed, float moveSpeed, Vector2f direction, Texture texture)
        {
            shape = new RectangleShape();

            this.lifeTime = lifeTime;
            this.scaleSpeed = scaleSpeed;
            this.rotateSpeed = rotateSpeed;
            this.moveSpeed = moveSpeed;
            this.direction = direction;

            shape.Size = new Vector2f(4, 4);
            shape.Origin = new Vector2f(shape.Size.X / 2, shape.Size.Y / 2);
            shape.FillColor = Color.White;
            shape.Texture = texture;

            isLife = true;
        }

        public void KillParticle()
        {
            isLife = false;
        }

        public void SetPosition(Vector2f position)
        {
            shape.Position = position;
        }
        public Vector2f GetPosition()
        {
            return shape.Position;
        }

        public void SetSize(Vector2f size)
        {
            shape.Size = size;
        }
        public Vector2f GetSize()
        {
            return shape.Size;
        }

        public void OnUpdate()
        {
            if (isLife)
            {
                if (shape.Size.X <= 1 || shape.Size.Y <= 1) isLife = false;

                shape.Position = new Vector2f(shape.Position.X + direction.X * moveSpeed, shape.Position.Y + direction.Y * moveSpeed);
                shape.Rotation += rotateSpeed;
                shape.Size = new Vector2f(shape.Size.X - scaleSpeed, shape.Size.Y - scaleSpeed);
            }
        }
        public void OnRender(RenderTarget target)
        {
            if (isLife)
                target.Draw(shape);
        }
    }
}
