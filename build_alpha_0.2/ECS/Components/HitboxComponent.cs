using build_alpha_0._2.Physics;
using SFML.Graphics;

namespace build_alpha_0._2.ECS.Components
{
    public class HitboxComponent : Component
    {
        private Hitbox hitbox;
        public Hitbox Hitbox
        {
            get { return hitbox; }
        }

        public HitboxComponent() : base()
        {
            hitbox = new Hitbox();
        }
        public HitboxComponent(Hitbox hitbox) : base()
        {
            this.hitbox = hitbox;
        }

        public bool CheckOnCollision(Hitbox other)
        {
            return hitbox.CheckOnCollision(other);
        }

        public override void OnUpdate(float deltaTime)
        {
            hitbox.OnUpdate();
        }
        public override void OnRender(RenderTarget target)
        {            
        }
    }
}
