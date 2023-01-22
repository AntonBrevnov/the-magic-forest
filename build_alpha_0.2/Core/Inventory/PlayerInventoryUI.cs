using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace build_alpha_0._2.Core.Inventory
{
    using Items;
    using NPC;
    using UI;

    public class PlayerInventoryUI
    {
        private Clock clock = new Clock();
        private Clock swapItemsClock = new Clock();

        private UIPanel backPanel;
        private UIPanel[] invenotyTiles;
        private UIPanel[] itemPanels;

        private UIPanel backHandPanel;
        private UIPanel[] invenotyHandTiles;
        private UIPanel[] itemHandPanels;

        private bool isShowInventory;

        private UIPanel backCraftPanel;
        private UIPanel[] craftPanels;
        private UIPanel[] craftAccessPanels;
        private Vector2f craftPosition;

        public bool isShowCraft { get; set; }

        public int SelectedItem { get; private set; } = 0;

        public void OnStart(RenderWindow window)
        {
            isShowInventory = false;
            // backpack inventory
            {
                backPanel = new UIPanel(null);
                backPanel.Size = new Vector2f(220, 220);
                backPanel.Position = new Vector2f(120, 320);
                backPanel.Origin = new Vector2f(110, 110);
                backPanel.DefaultColor = new Color(170, 170, 170, 140);
            }
            {
                invenotyTiles = new UIPanel[25];
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        invenotyTiles[y + x * 5] = new UIPanel(backPanel);
                        invenotyTiles[y + x * 5].Size = new Vector2f(32, 32);
                        invenotyTiles[y + x * 5].Position = new Vector2f((x * 42) - 84, (y * 42) - 84);
                        invenotyTiles[y + x * 5].Origin = new Vector2f(16, 16);
                        invenotyTiles[y + x * 5].SelectedColor = Color.Cyan;
                    }
                }
            }
            {
                itemPanels = new UIPanel[25];
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        itemPanels[y + x * 5] = new UIPanel(backPanel);
                        itemPanels[y + x * 5].Size = new Vector2f(24, 24);
                        itemPanels[y + x * 5].Position = new Vector2f((x * 42) - 84, (y * 42) - 84);
                        itemPanels[y + x * 5].Origin = new Vector2f(12, 12);
                    }
                }
            }
            // hand inventory
            {
                backHandPanel = new UIPanel(null);
                backHandPanel.DefaultColor = new Color(170, 170, 170, 140);
                backHandPanel.Size = new Vector2f(52, 220);
                backHandPanel.Position = new Vector2f(36, 320);
                backHandPanel.Origin = new Vector2f(26, 110);
            }
            {
                invenotyHandTiles = new UIPanel[5];
                for (int y = 0; y < 5; y++)
                {
                    invenotyHandTiles[y] = new UIPanel(backHandPanel);
                    invenotyHandTiles[y].Size = new Vector2f(32, 32);
                    invenotyHandTiles[y].Position = new Vector2f(0, (y * 42) - 84);
                    invenotyHandTiles[y].Origin = new Vector2f(16, 16);
                }
                invenotyHandTiles[0].DefaultColor = Color.Green;
            }
            {
                itemHandPanels = new UIPanel[5];
                for (int y = 0; y < 5; y++)
                {
                    itemHandPanels[y] = new UIPanel(backHandPanel);
                    itemHandPanels[y].Size = new Vector2f(24, 24);
                    itemHandPanels[y].Position = new Vector2f(0, (y * 42) - 84);
                    itemHandPanels[y].Origin = new Vector2f(12, 12);
                }
            }

            isShowCraft = false;
            // craft
            {
                backCraftPanel = new UIPanel(null);
                backCraftPanel.DefaultColor = new Color(225, 225, 225, 160);
                backCraftPanel.Size = new Vector2f(220, 220);
                backCraftPanel.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
                backCraftPanel.Origin = new Vector2f(110, 110);
            }
            {
                craftPanels = new UIPanel[5];
                for (int i = 0; i < craftPanels.Length; i++)
                {
                    craftPanels[i] = new UIPanel(backCraftPanel);
                    craftPanels[i].Size = new Vector2f(158, 32);
                    craftPanels[i].Position = new Vector2f(-22, i * 42 - 84);
                    craftPanels[i].Origin = new Vector2f(79, 16);
                    craftPanels[i].DefaultColor = Color.White;
                    craftPanels[i].SelectedColor = Color.Cyan;
                    craftPanels[i].Texture = ResourcesManager.GetTexture("craft_slots");
                    craftPanels[i].TextureRect = new IntRect(0, i * 24, 120, 24);
                }
            }
            {
                craftAccessPanels = new UIPanel[5];
                for (int i = 0; i < craftAccessPanels.Length; i++)
                {
                    craftAccessPanels[i] = new UIPanel(backCraftPanel);
                    craftAccessPanels[i].Size = new Vector2f(32, 32);
                    craftAccessPanels[i].Position = new Vector2f(82, i * 42 - 84);
                    craftAccessPanels[i].Origin = new Vector2f(16, 16);
                    craftAccessPanels[i].DefaultColor = Color.White;
                    craftAccessPanels[i].Texture = ResourcesManager.GetTexture("craft_access");
                    craftAccessPanels[i].TextureRect = new IntRect(11, 0, 11, 11);
                }
            }
            craftPosition = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
        }
        public void OnHandleKeyPressed(KeyEventArgs e)
        {
        }
        public void OnHandleKeyReleased(KeyEventArgs e)
        {

        }
        public void OnHandleButtonPressed(MouseButtonEventArgs e)
        {
        }
        public void OnHandleButtonReleased(MouseButtonEventArgs e)
        {

        }
        public void OnHandleMouseMoved(MouseMoveEventArgs e)
        {
            for (int i = 0; i < invenotyTiles.Length; i++)
            {
                invenotyTiles[i].IsHover(e);
            }
        }
        public void OnHandleWheelScrolled(MouseWheelScrollEventArgs e)
        {

        }

        public void OnUpdate(Player player)
        {
            if (isShowInventory)
            {
                backPanel.Position = new Vector2f(120, 320);
                backHandPanel.Position = new Vector2f(-26, -110);

                for (int i = 0; i < invenotyTiles.Length; i++)
                {
                    // если выделен какой-либо элемент в рюкзаке
                    if (invenotyTiles[i].IsSelected)
                    {
                        // если время ожидания превысило 0.95 секунды
                        if (swapItemsClock.ElapsedTime.AsSeconds() >= 0.95f)
                        {
                            // если нажата клавиша, соответствующая номеру элемента в руке, то
                            // меняем элементы местами и перезапускаем времяя ожидания
                            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                            {
                                itemHandPanels[0].TextureRect = itemPanels[i].TextureRect;
                                player.Inventory.SwapItems(0, i);
                                swapItemsClock.Restart();
                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                            {
                                itemHandPanels[1].TextureRect = itemPanels[i].TextureRect;
                                player.Inventory.SwapItems(1, i);
                                swapItemsClock.Restart();
                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                            {
                                itemHandPanels[2].TextureRect = itemPanels[i].TextureRect;
                                player.Inventory.SwapItems(2, i);
                                swapItemsClock.Restart();
                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.Num4))
                            {
                                itemHandPanels[3].TextureRect = itemPanels[i].TextureRect;
                                player.Inventory.SwapItems(3, i);
                                swapItemsClock.Restart();
                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.Num5))
                            {
                                itemHandPanels[4].TextureRect = itemPanels[i].TextureRect;
                                player.Inventory.SwapItems(4, i);
                                swapItemsClock.Restart();
                            }
                        }
                    }
                }
            }
            else
            {
                backPanel.Position = new Vector2f(-110, -110);
                backHandPanel.Position = new Vector2f(36, 320);
            }

            if (isShowCraft)
                backCraftPanel.Position = craftPosition;
            else
                backCraftPanel.Position = new Vector2f(-110, -110);

            // нажата клавиша I и время ожидания превысило 0.5 секунды, то
            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && clock.ElapsedTime.AsSeconds() >= 0.5f)
            {
                // изменяем состояние отображения инвентаря
                if (isShowInventory) isShowInventory = false; else isShowInventory = true;
                // перезапускаем время ожидания
                clock.Restart();
            }

            for (int i = 0; i < player.Inventory.MaxItemsCount; i++)
            {
                if (player.Inventory.GetItemAt(i) != null)
                {
                    itemPanels[i].Texture = player.Inventory.GetItemAt(i).Sprite.Texture;
                    itemPanels[i].TextureRect = player.Inventory.GetItemAt(i).Sprite.TextureRect;
                }
                else itemPanels[i].Texture = null;
            }
            for (int i = 0; i < 5; i++)
            {
                if (player.Inventory.GetItemAt(i) != null)
                {
                    itemHandPanels[i].Texture = player.Inventory.GetItemAt(i).Sprite.Texture;
                    itemHandPanels[i].TextureRect = player.Inventory.GetItemAt(i).Sprite.TextureRect;
                }
                else itemHandPanels[i].Texture = null;
            }

            backPanel.OnUpdate();
            for (int i = 0; i < 25; i++)
                invenotyTiles[i].OnUpdate();
            for (int i = 0; i < 25; i++)
                itemPanels[i].OnUpdate();

            backHandPanel.OnUpdate();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
            {
                invenotyHandTiles[0].DefaultColor = Color.Green;
                invenotyHandTiles[1].DefaultColor = Color.White;
                invenotyHandTiles[2].DefaultColor = Color.White;
                invenotyHandTiles[3].DefaultColor = Color.White;
                invenotyHandTiles[4].DefaultColor = Color.White;
                SelectedItem = 0;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
            {
                invenotyHandTiles[0].DefaultColor = Color.White;
                invenotyHandTiles[1].DefaultColor = Color.Green;
                invenotyHandTiles[2].DefaultColor = Color.White;
                invenotyHandTiles[3].DefaultColor = Color.White;
                invenotyHandTiles[4].DefaultColor = Color.White;
                SelectedItem = 1;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
            {
                invenotyHandTiles[0].DefaultColor = Color.White;
                invenotyHandTiles[1].DefaultColor = Color.White;
                invenotyHandTiles[2].DefaultColor = Color.Green;
                invenotyHandTiles[3].DefaultColor = Color.White;
                invenotyHandTiles[4].DefaultColor = Color.White;
                SelectedItem = 2;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num4))
            {
                invenotyHandTiles[0].DefaultColor = Color.White;
                invenotyHandTiles[1].DefaultColor = Color.White;
                invenotyHandTiles[2].DefaultColor = Color.White;
                invenotyHandTiles[3].DefaultColor = Color.Green;
                invenotyHandTiles[4].DefaultColor = Color.White;
                SelectedItem = 3;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num5))
            {
                invenotyHandTiles[0].DefaultColor = Color.White;
                invenotyHandTiles[1].DefaultColor = Color.White;
                invenotyHandTiles[2].DefaultColor = Color.White;
                invenotyHandTiles[3].DefaultColor = Color.White;
                invenotyHandTiles[4].DefaultColor = Color.Green;
                SelectedItem = 4;
            }
            for (int i = 0; i < 5; i++)
                invenotyHandTiles[i].OnUpdate();
            for (int i = 0; i < 5; i++)
            {
                itemHandPanels[i].Texture = itemPanels[i].Texture;
                itemHandPanels[i].TextureRect = itemPanels[i].TextureRect;
                itemHandPanels[i].OnUpdate();
            }

            {
                backCraftPanel.OnUpdate();
                // craft sword
                if (player.Inventory.HasItemOnCount(ItemID.Iron, 3) && player.Inventory.HasItem(ItemID.Log))
                {
                    craftAccessPanels[0].TextureRect = new IntRect(0, 0, 11, 11);
                    if (craftPanels[0].IsSelected)
                    {
                        for (int i = 0; i < 3; i++)
                            player.Inventory.RemoveItemByID(ItemID.Iron);
                        player.Inventory.RemoveItemByID(ItemID.Log);
                        player.Inventory.AddItem(new SwordItem());
                        craftPanels[0].IsSelected = false;
                    }
                }
                else craftAccessPanels[0].TextureRect = new IntRect(11, 0, 11, 11);
                // craft pickaxe
                if (player.Inventory.HasItemOnCount(ItemID.Iron, 3) && player.Inventory.HasItemOnCount(ItemID.Log, 2))
                {
                    craftAccessPanels[1].TextureRect = new IntRect(0, 0, 11, 11);
                    if (craftPanels[1].IsSelected)
                    {
                        for (int i = 0; i < 3; i++)
                            player.Inventory.RemoveItemByID(ItemID.Iron);
                        for (int i = 0; i < 3; i++)
                            player.Inventory.RemoveItemByID(ItemID.Log);
                        player.Inventory.AddItem(new PickaxeItem());
                        craftPanels[1].IsSelected = false;
                    }
                }
                else craftAccessPanels[1].TextureRect = new IntRect(11, 0, 11, 11);
                // craft bukket
                if (player.Inventory.HasItemOnCount(ItemID.Iron, 3))
                {
                    craftAccessPanels[2].TextureRect = new IntRect(0, 0, 11, 11);
                    if (craftPanels[2].IsSelected)
                    {
                        for (int i = 0; i < 3; i++)
                            player.Inventory.RemoveItemByID(ItemID.Iron);
                        player.Inventory.AddItem(new BukketItem());
                        craftPanels[2].IsSelected = false;
                    }
                }
                else craftAccessPanels[2].TextureRect = new IntRect(11, 0, 11, 11);
                // craft torch
                if (player.Inventory.HasItem(ItemID.Coal) && player.Inventory.HasItem(ItemID.Stick))
                {
                    craftAccessPanels[3].TextureRect = new IntRect(0, 0, 11, 11);
                    if (craftPanels[3].IsSelected)
                    {
                        player.Inventory.RemoveItemByID(ItemID.Coal);
                        player.Inventory.RemoveItemByID(ItemID.Stick);
                        player.Inventory.AddItem(new TorchItem());
                        craftPanels[3].IsSelected = false;
                    }
                }
                else craftAccessPanels[3].TextureRect = new IntRect(11, 0, 11, 11);
                // craft mana bottle
                if (player.Inventory.HasItemOnCount(ItemID.Crystal, 3))
                {
                    craftAccessPanels[4].TextureRect = new IntRect(0, 0, 11, 11);
                    if (craftPanels[4].IsSelected)
                    {
                        for (int i = 0; i < 3; i++)
                            player.Inventory.RemoveItemByID(ItemID.Crystal);
                        player.Inventory.AddItem(new ManaBottleItem());
                        craftPanels[4].IsSelected = false;
                    }
                }
                else craftAccessPanels[4].TextureRect = new IntRect(11, 0, 11, 11);

                for (int i = 0; i < craftPanels.Length; i++)
                    craftPanels[i].OnUpdate();
                for (int i = 0; i < craftAccessPanels.Length; i++)
                    craftAccessPanels[i].OnUpdate();
            }
        }
        public void OnRender(RenderTarget target)
        {
            backPanel.OnRender(target);
            for (int i = 0; i < 25; i++)
                invenotyTiles[i].OnRender(target);
            for (int i = 0; i < 25; i++)
                itemPanels[i].OnRender(target);

            backHandPanel.OnRender(target);
            for (int i = 0; i < 5; i++)
                invenotyHandTiles[i].OnRender(target);
            for (int i = 0; i < 5; i++)
                itemHandPanels[i].OnRender(target);

            backCraftPanel.OnRender(target);
            for (int i = 0; i < craftPanels.Length; i++)
                craftPanels[i].OnRender(target);
            for (int i = 0; i < craftAccessPanels.Length; i++)
                craftAccessPanels[i].OnRender(target);
        }
    }
}
