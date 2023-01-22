using SFML.Graphics;

namespace build_alpha_0._2.ECS
{
    public abstract class Component
    {
        private uint entityID;
        public uint EntityID
        {
            get { return entityID; }
            set { entityID = value; }
        }

        public Component()
        {
            entityID = 0;
        }
        public Component(uint id)
        {
            entityID = id;
        }

        public abstract void OnUpdate(float deltaTime);
        public abstract void OnRender(RenderTarget target);
    }
}
