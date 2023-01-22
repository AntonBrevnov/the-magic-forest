using SFML.Graphics;

namespace build_alpha_0._2.ECS
{
    public class Entity
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        private uint id;
        public uint ID
        {
            get { return id; }
        }        

        public Entity()
        {
            name = "empty entity";
            id = 0;
        }
        public Entity(string name)
        {
            this.name = name;
            id = 0;
        }
        public Entity(uint id)
        {
            name = "empty entity";
            this.id = id;
        }
        public Entity(string name, uint id)
        {
            this.name = name;
            this.id = id;
        }

        public virtual void OnUpdate(float deltaTime)
        {
            // TODO: updating an entity
        }
        public virtual void OnRender(RenderTarget target)
        {
            // TODO: rendering an entity
        }
    }
}
