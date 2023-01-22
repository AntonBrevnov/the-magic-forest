using System;
using System.Collections.Generic;

namespace build_alpha_0._2.ECS
{
    public class SystemManager
    {
        private List<Entity> entities;
        private List<Component> components;

        public SystemManager()
        {
            entities = new List<Entity>();
            components = new List<Component>();
        }

        public Entity CreateEntity(string name)
        {
            Entity newEntity = new Entity(name, (uint)(entities.Count + 1));
            entities.Add(newEntity);
            return newEntity;
        }
        public void AddEntity(Entity newEntity)
        {
            foreach (var entity in entities)
            {
                if (entity.ID == newEntity.ID)
                {
                    Console.WriteLine($"The entity manager has this entity [name: {entity.Name}; id: {entity.ID}");
                    return;
                }
            }
            string name = newEntity.Name;
            newEntity = new Entity(name, (uint)(entities.Count + 1));
            entities.Add(newEntity);
        }
        public void RemoveEntity(Entity oldEntity)
        {
            foreach (var entity in entities)
            {
                if (entity.ID == oldEntity.ID)
                {
                    entities.Remove(oldEntity);
                    return;
                }
            }
            Console.WriteLine($"The entity manager doesn't has this entity [name: {oldEntity.Name}; id: {oldEntity.ID}");
        }

        public void AddComponent(Entity entity, Component component)
        {
            foreach (var ent in entities)
            {
                if (ent.ID == entity.ID)
                {
                    component.EntityID = entity.ID;
                    components.Add(component);
                    return;
                }
            }
            Console.WriteLine($"The entity manager doesn't has this entity [name: {entity.Name}; id: {entity.ID}");
        }
        public T GetComponent<T>(Entity entity) where T : Component
        {
            foreach (var component in components)
            {
                if (component.EntityID == entity.ID)
                {
                    if (component.GetType().Equals(typeof(T)))
                        return (T)component;
                }
            }
            Console.WriteLine($"The component manager doesn't has this component [type: {typeof(T)}");
            return null;
        }
        public void RemoveComponent(Entity entity, Component component)
        {
            foreach (var comp in components)
            {
                if (comp.EntityID == entity.ID && comp.EntityID == component.EntityID)
                {
                    components.Remove(component);
                    return;
                }
            }
            Console.WriteLine($"The component manager doesn't has this component [type: {component.GetType().Name}");
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var component in components)
                component.OnUpdate(deltaTime);
        }
    }
}
