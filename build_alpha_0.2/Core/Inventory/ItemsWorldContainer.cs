using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace build_alpha_0._2.Core.Inventory
{
    using ECS;
    using ECS.Components;
    using NPC;
    using VFX;

    public class ItemsWorldContainer
    {
        private List<Item> itemsTemplates;
        private List<Item> globalWorldItems;

        public ItemsWorldContainer()
        {
            itemsTemplates = new List<Item>();
            globalWorldItems = new List<Item>();
        }
        public ItemsWorldContainer(List<Item> templates)
        {
            itemsTemplates = templates;
            globalWorldItems = new List<Item>();
        }

        public void ClearWorld()
        {
            globalWorldItems.Clear();
        }

        public void AddItemTemplate(Item template)
        {
            foreach (var item in itemsTemplates)
            {
                if (item == template) return;
            }
            itemsTemplates.Add(template);
        }

        public void SpawnItems(FloatRect spawnArea, int count)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                int itemId = random.Next(0, itemsTemplates.Count);
                int posX = random.Next((int)spawnArea.Left, (int)spawnArea.Width);
                int posY = random.Next((int)spawnArea.Top, (int)spawnArea.Height);
                Item temp = itemsTemplates[itemId];
                temp.Position = new Vector2f(posX, posY);
                temp.Sprite.Scale = new Vector2f(0.3f, 0.3f);
                globalWorldItems.Add(temp);
            }
        }

        public void SpawnItem(Item template, FloatRect spawnArea, int count)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                int posX = random.Next((int)spawnArea.Left, (int)spawnArea.Left + (int)spawnArea.Width);
                int posY = random.Next((int)spawnArea.Top, (int)spawnArea.Top + (int)spawnArea.Height);
                Item temp = template;
                temp.Position = new Vector2f(posX, posY);
                temp.Sprite.Scale = new Vector2f(0.3f, 0.3f);
                globalWorldItems.Add(temp);
            }
        }

        public void CheckOnPlayerCollision(Player player, SystemManager manager)
        {
            for (int i = 0; i < globalWorldItems.Count; i++)
            {
                if (manager.GetComponent<HitboxComponent>(player.Entity).CheckOnCollision(globalWorldItems[i].Hitbox))
                {
                    globalWorldItems[i].Sprite.Scale = new Vector2f(0.3f, 0.3f);
                    if (player.Inventory.AddItem(globalWorldItems[i]))
                    {
                        globalWorldItems.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public void OnUpdate()
        {
            foreach (var item in globalWorldItems)
            {
                item.OnUpdate();
            }
        }
        public void OnRender(RenderTarget target, Light light)
        {
            foreach (var item in globalWorldItems)
            {
                item.OnRender(target, light);
            }
        }
    }
}
