using SFML.Graphics;

namespace build_alpha_0._2.Physics
{
    public class Hitbox
    {
        private FloatRect hitboxBound;
        private RectangleShape hitBoxShape;

        public Hitbox()
        {
            hitBoxShape = new RectangleShape();
            hitboxBound = new FloatRect(hitBoxShape.Position, hitBoxShape.Size);
        }
        public Hitbox(RectangleShape shape)
        {
            hitBoxShape = shape;
            hitboxBound = new FloatRect(hitBoxShape.Position, hitBoxShape.Size);
        }

        public bool CheckOnCollision(Hitbox other)
        {
            if (hitboxBound.Intersects(other.hitboxBound))
                return true;
            else return false;
        }

        public void OnUpdate()
        {
            hitboxBound = new FloatRect(hitBoxShape.Position, hitBoxShape.Size);
        }
    }
}
